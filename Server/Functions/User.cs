using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Functions
{
    public class User
    {
        /// <summary>
        /// Validates a set of login credentials
        /// Note:
        /// 0 = Success
        /// 1 = Failure (bad creds)
        /// 2 = Account ban
        /// 3 = FingerPrint Ban
        /// 4 = Remote FingerPrint Ban
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fingerprint"></param>
        /// <returns></returns>
        public static int ValidateCredentials(string username, string password, string fingerprint)
        {
            return 0;
            using (SqlConnection sqlCon = Database.CreateConnection(Database.ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = sqlCon;

                    // Check for matching account entry
                    sqlCmd.CommandText = string.Format("SELECT TOP(1) account_id FROM {0} WHERE login_name = @name AND password = @password", OPT.SettingsList["db.auth.table_alias"]);
                    sqlCmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = username;
                    sqlCmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = PasswordCipher.CreateHash(OPT.SettingsList["md5.key"], password);

                    int result = Convert.ToInt32(Database.ExecuteStatement(sqlCmd, 1));
                    
                    // if exists check for ban
                    if (result > 0)
                    {
                        int accId = result;

                        sqlCmd.CommandText = string.Format("SELECT TOP(1) block FROM {0} WHERE login_name = @name", OPT.SettingsList["db.auth.table_alias"]);
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = username;

                        result = Convert.ToInt32(Database.ExecuteStatement(sqlCmd, 1));

                        // if exists and no ban, check for fingerprint
                        if (result == 0)
                        {
                            sqlCmd.CommandText = "SELECT TOP(1) * FROM dbo.FingerPrint WHERE finger_print = @fingerprint";
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add("@fingerprint", SqlDbType.NVarChar).Value = fingerprint;

                            result = Convert.ToInt32(Database.ExecuteStatement(sqlCmd, 1));


                            if (result == 1)
                            {
                                // if exists and no ban and fingerprint, check for fingerprint ban
                                sqlCmd.CommandText = "SELECT TOP(1) block FROM dbo.FingerPrint WHERE finger_print = @fingerprint";
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.Add("@fingerprint", SqlDbType.NVarChar).Value = fingerprint;

                                result = Convert.ToInt32(Database.ExecuteStatement(sqlCmd, 1));

                                // if exists and no ban and fingerprint and no ban, return good
                                if (result == 0) { return 0; }
                                else { return 3; } // return fingerprint ban
                            }
                            else
                            {
                                sqlCmd.CommandText = "SELECT COUNT(account_id) FROM dbo.FingerPrint WHERE account_id = @accountId AND block = 1";
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.Add("@accountId", SqlDbType.Int).Value = accId;

                                result = Convert.ToInt32(Database.ExecuteStatement(sqlCmd, 1));

                                int block = 0;

                                if (result == 1)
                                {
                                    block = 1;
                                }

                                // if exists and no ban and no fingerprint, add fingerprint
                                sqlCmd.CommandText = "INSERT INTO dbo.FingerPrint (account_id, finger_print, block) VALUES (@accountId, @fingerprint, @block)";
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.Add("@accountId", SqlDbType.Int).Value = accId;
                                sqlCmd.Parameters.Add("@fingerprint", SqlDbType.NVarChar).Value = fingerprint;
                                sqlCmd.Parameters.Add("@block", SqlDbType.Int).Value = block;
                                Database.ExecuteStatement(sqlCmd, 0);

                                if (block == 1) { return 4; }
                            }
                        }
                        else { return 2; }
                    }
                }
            }

                return 1;
        }
    }
}
