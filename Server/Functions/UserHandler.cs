using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Server.Network;
using Server.Structures;

namespace Server.Functions
{
    public class UserHandler
    {
        protected bool debug = false;

        protected static UserHandler instance;
        public static UserHandler Instance { get { return (instance == null) ? new UserHandler() : instance; } }

        public static List<User> UserList = new List<User>();
        public static int UserCount { get { return UserList.Count; } }

        public UserHandler() { debug = OPT.GetBool("debug"); }

        public void OnUserRequestDesKey(Client client)
        {
            if (OPT.SettingExists("des.key"))
            {
                if (debug) { Output.Write(new Message() { Text = string.Format("Client [{0}] requested des.key...", client.Id) }); }

                string desKey = OPT.GetString("des.key");

                PacketStream stream = new PacketStream(0x9999);
                stream.WriteString(desKey);

                if (OPT.GetBool("debug")) { Output.Write(new Message() { Text = string.Format("[{0}] Sent!", desKey), AddBreak = true }); }

                ClientManager.Instance.Send(client, stream);
            }
            else { Output.Write(new Message() { Text = "Failed to find des.key in settings!", AddBreak = true }); }
        }

        public void OnValidateUser(Client client, string username, string password, string fingerprint)
        {
            if (debug)
            {
                Output.Write(new Message() { Text = string.Format("Client [{0}] requested login validation with the following credentials:", client.Id), AddBreak = true });
                Output.Write(new Message() { Text = string.Format("Username: {0}\nPassword: {1}\nFingerprint: {2}", username, password, fingerprint), AddBreak = true });
            }

            // Check if username / password exist
            using (SqlConnection sqlCon = Database.Connection)
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = string.Format("SELECT account_id FROM dbo.{0} WHERE login_name = @name AND password = @password", OPT.GetString("db.auth.table.alias"));
                sqlCmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = username;
                sqlCmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = PasswordCipher.CreateHash(OPT.GetString("md5.key"), password);

                if (debug) { Output.Write(new Message() { Text = "\t-Checking for Account..." }); }

                object result = Database.ExecuteStatement(sqlCmd, 1);

                if (debug) { Output.Write(new Message() { Text = ((int)result > 0) ? "[FOUND]" : "[NOT FOUND]", AddBreak = true }); }

                if ((int)result > 0) // Account exists
                {
                    int account_id = (int)result;

                    if (debug) { Output.Write(new Message() { Text = "\t-Checking Account ban status..." }); }

                    // Check if account is banned
                    sqlCmd.CommandText = string.Format("SELECT ban FROM dbo.{0} WHERE login_name = @name AND password = @password", OPT.GetString("db.auth.table.alias"));
                    result = Database.ExecuteStatement(sqlCmd, 1);

                    if (debug) { Output.Write(new Message() { Text = ((int)result == 0) ? "[NOT BANNED]" : "[BANNED]", AddBreak = true }); }

                    if ((int)result == 0) // Account is not banned
                    {
                        if (debug) { Output.Write(new Message() { Text = "\t-Checking for FingerPrint..." }); }

                        // Check for fingerprint
                        sqlCmd.CommandText = "SELECT COUNT(account_id) FROM dbo.FingerPrint WHERE account_id = @account_id";
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;

                        result = Database.ExecuteStatement(sqlCmd, 1);

                        if (debug) { Output.Write(new Message() { Text = ((int)result == 1) ? "[FOUND]" : "[NOT FOUND]", AddBreak = true }); }

                        if ((int)result == 1) // FingerPrint exists
                        {
                            if (debug) { Output.Write(new Message() { Text = "\t-Checking FingerPrint ban status..." }); }

                            // Check if FingerPrint is banned
                            sqlCmd.CommandText = "SELECT ban FROM dbo.FingerPrint WHERE account_id = @account_id";

                            result = Database.ExecuteStatement(sqlCmd, 1);

                            if (debug) { Output.Write(new Message() { Text = ((int)result == 0) ? "[NOT BANNED]" : "[BANNED]", AddBreak = true }); }

                            if ((int)result == 0) // FingerPrint is not banned
                            {
                                UserList.Add(new User() { Client_ID = client.Id, Account_ID = account_id, Login_Name = username });
                                Statistics.UpdateAuthenticatedCount(true);
                                setOTP(ref client, ref sqlCmd, account_id);
                            }
                            else // FingerPrint is banned
                            {
                                Statistics.UpdateBannedCount(true);

                                if (debug) { Output.Write(new Message() { Text = "\t-Checking if FingerPrint ban is expired..." }); }

                                // Get OTP Expiration Date
                                sqlCmd.CommandText = "SELECT expiration_date FROM dbo.FingerPrint WHERE account_id = @account_id";
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;

                                result = Database.ExecuteStatement(sqlCmd, 1);

                                if ((DateTime)result < DateTime.Now) // Ban is up
                                {
                                    if (debug) { Output.Write(new Message() { Text = "[EXPIRED]\n\t-Updating FingerPrint ban...", AddBreak = true }); }

                                    sqlCmd.CommandText = "UPDATE dbo.FingerPrint SET ban = 0 WHERE account_id = @account_id";

                                    result = Database.ExecuteStatement(sqlCmd, 0);

                                    if (debug) { Output.Write(new Message() { Text = ((int)result == 1) ? "[SUCCESS]" : "[FAIL]", AddBreak = true }); }

                                    Statistics.UpdateAuthenticatedCount(true);
                                    Statistics.UpdateBannedCount(false);
                                    setOTP(ref client, ref sqlCmd, account_id);
                                }
                                else { ClientPackets.Instance.SC_SendBanStatus(client, 1); }
                            }
                        }
                        else
                        {
                            if (debug) { Output.Write(new Message() { Text = string.Format("\t-Inserting FingerPrint: {0}...", fingerprint) }); }

                            sqlCmd.CommandText = "INSERT INTO dbo.FingerPrint (account_id, finger_print, ban, expiration_date) VALUES (@account_id, @finger_print, @ban, @expiration_date)";
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
                            sqlCmd.Parameters.Add("@finger_print", SqlDbType.NVarChar).Value = fingerprint;
                            sqlCmd.Parameters.Add("@ban", SqlDbType.Int).Value = 0;
                            sqlCmd.Parameters.Add("@expiration_date", SqlDbType.DateTime).Value = new DateTime(1999, 1, 1, 12, 0, 0, 0);

                            result = Database.ExecuteStatement(sqlCmd, 0);

                            if (debug) { Output.Write(new Message() { Text = ((int)result == 1) ? "[SUCCESS]" : "[FAIL]", AddBreak = true }); }

                            Statistics.UpdateAuthenticatedCount(true);
                            setOTP(ref client, ref sqlCmd, account_id);
                        }
                    }
                    else // Account is banned
                    {
                        Statistics.UpdateBannedCount(true);
                        ClientPackets.Instance.SC_SendBanStatus(client, 0);
                    } 
                }
                else // Account doesn't exist
                {
                    Statistics.UpdateRejectCount(true);
                    ClientPackets.Instance.SC_SendAccountNull(client);
                } 
            }
        }

        internal void OnAuthenticationTypeRequest(Client client)
        {
            ClientPackets.Instance.SC_SendAuthenticationType(client, OPT.GetInt("imbc.login"));
        }

        internal void OnUserRequestArguments(Client client)
        {
            string arguments = string.Format("/auth_ip:{0} /auth_port:{1} /locale:? /country:? /use_nprotect:0 /cash /commercial_shop /allow_double_exec:{2}", OPT.GetString("auth.io.ip"), OPT.GetString("auth.io.port"), OPT.GetString("double.execute"));

            if (OPT.GetBool("imbc.login")) { arguments += "/imbclogin /account:? /password:?"; }

            ClientPackets.Instance.SC_SendArguments(client, arguments, OPT.GetInt("sframe.bypass"), OPT.GetBool("maintenance"));
        }

        internal void OnUserRequestDisconnect(Client client)
        {
            if (debug) { Output.Write(new Message() { Text = string.Format("Removed Client [{0}] from ClientList", client.Id) }); }
            ClientManager.Instance.Remove(client);
        }

        protected void setOTP(ref Client client, ref SqlCommand sqlCmd, int account_id)
        {
            // Formulate an OTP
            string otpHash = OTP.GenerateRandomPassword(26);

            if (debug) { Output.Write(new Message() { Text = string.Format("\t-Generated OTP: {0}", otpHash) }); }

            // Check if OTP account_id already exists
            sqlCmd.CommandText = "SELECT COUNT(account_id) FROM dbo.OTP WHERE account_id = @account_id";

            object result = Database.ExecuteStatement(sqlCmd, 1);

            if ((int)result == 1) // OTP account_id exists, update OTP
            {
                if (debug) { Output.Write(new Message() { Text = "\t-Updating OTP..." }); }

                sqlCmd.CommandText = "UPDATE dbo.OTP SET otp = @OTP, expiration = @expiration WHERE account_id = @account_id";
                sqlCmd.Parameters.Add("@OTP", SqlDbType.NVarChar).Value = otpHash;
                sqlCmd.Parameters.Add("@expiration", SqlDbType.DateTime).Value = DateTime.Now.AddMinutes(5);

                result = Database.ExecuteStatement(sqlCmd, 0);

                if (debug) { Output.Write(new Message() { Text = ((int)result == 1) ? "[SUCCESS]" : "[FAIL]", AddBreak = true }); }
            }
            else // OTP account_id doesn't exist, write new OTP
            {
                if (debug) { Output.Write(new Message() { Text = "\t-Inserting OTP..." }); }

                sqlCmd.CommandText = "INSERT INTO dbo.OTP (account_id, otp, expiration) VALUES (@account_id, @OTP, @expiration)";
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
                sqlCmd.Parameters.Add("@OTP", SqlDbType.NVarChar).Value = otpHash;
                sqlCmd.Parameters.Add("@expiration", SqlDbType.DateTime).Value = DateTime.Now.AddMinutes(5);

                result = Database.ExecuteStatement(sqlCmd, 0);

                if (debug) { Output.Write(new Message() { Text = ((int)result == 1) ? "[SUCCESS]" : "[FAIL]", AddBreak = true }); }
            }

            ClientPackets.Instance.SC_SendOTP(client, otpHash);
        }
    }
}
