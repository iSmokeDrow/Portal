using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Server.Functions
{
    public class OTP
    {
        public static bool HasErrors = false;

        /// <summary>
        /// Possible characters combined to create random password
        /// </summary>
        static readonly char[] AvailableCharacters =
        { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
          'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
          'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
          'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
          '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// Generate a random password to be combined with md5 salt to make an md5 hashed password
        /// </summary>
        /// <param name="length">Length of Random password to be created</param>
        /// <returns>Random password of AvailableCharacters</returns>
        public static string GenerateRandomPassword(int length)
        {
            char[] identifier = new char[length];
            byte[] randomData = new byte[length];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            for (int idx = 0; idx < identifier.Length; idx++)
            {
                int pos = randomData[idx] % AvailableCharacters.Length;
                identifier[idx] = AvailableCharacters[pos];
            }

            return new string(identifier);
        }

        /// <summary>
        /// Generate a salted md5 to be used as the temporary OTP
        /// </summary>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GenerateMD5OTP(string salt, string input)
        {
            MD5 md5 = MD5.Create();
            string passToHash = string.Format("{0}{1}", salt, input);
            byte[] passToHashBytes = Encoding.Default.GetBytes(passToHash);
            byte[] hashedPassToHashBytes = md5.ComputeHash(passToHashBytes, 0, passToHashBytes.Length);
            StringBuilder sb = new StringBuilder();
            for (int currentByte = 0; currentByte < hashedPassToHashBytes.Length; currentByte++)
            {
                sb.Append(hashedPassToHashBytes[currentByte].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string SetOTP(string username)
        {
            string outOTP = null;
            string otpHash = null;

            // Get username account_id
            using (SqlConnection sqlCon = Database.CreateConnection)
            {
                using (SqlCommand sqlCmd = new SqlCommand(string.Format("SELECT account_id FROM dbo.{0} WHERE login_name = @username", OPT.GetString("db.auth.table_alias")), sqlCon))
                {
                    sqlCmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                    int result = (int)Database.ExecuteStatement(sqlCmd, 1);
                    if (result > 0)
                    {
                        int account_id = result;
                        outOTP = OTP.GenerateRandomPassword(22);
                        otpHash = OTP.GenerateMD5OTP(OPT.GetString("md5.key"), outOTP);

                        // Check if an OTP bearing this account_id exists
                        sqlCmd.CommandText = "SELECT COUNT(*) FROM dbo.otp WHERE account_id = @account_id";
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;

                        result = (int)Database.ExecuteStatement(sqlCmd, 1);
                        if (result > 0)
                        {
                            // Update original entry
                            sqlCmd.CommandText = "UPDATE dbo.OTP SET otp = @otp, otp_end = @otp_end WHERE account_id = @account_id";
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add("@otp", SqlDbType.NVarChar).Value = otpHash;
                            sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
                            sqlCmd.Parameters.Add("@otp_end", SqlDbType.DateTime).Value = DateTime.Now.AddMinutes(2.00);

                            Database.ExecuteStatement(sqlCmd, 0);
                        }
                        else
                        {
                            // Create new entry
                            sqlCmd.CommandText = "INSERT INTO dbo.otp (account_id, otp, otp_end) VALUES (@account_id, @otp, @otp_end)";
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
                            sqlCmd.Parameters.Add("@otp", SqlDbType.NVarChar).Value = otpHash;
                            sqlCmd.Parameters.Add("@otp_end", SqlDbType.DateTime).Value = DateTime.Now.AddMinutes(2.00);

                            Database.ExecuteStatement(sqlCmd, 0);
                        }
                    }
                }
            }

            return outOTP;
        }

        public static void HandleOTP()
        {
            Task.Run(() =>
            {
                using (SqlConnection sqlCon = Database.CreateConnection)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SELECT account_id, otp, otp_end FROM dbo.otp WHERE otp_end != '1999-01-01 12:00:00'", sqlCon))
                    {
                        DataTable otpTable = (DataTable)Database.ExecuteStatement(sqlCmd, 2);
                        if (otpTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in otpTable.Rows)
                            {
                                DateTime currentDateTime = DateTime.Now;
                                DateTime otpDateTime = DateTime.Parse(row["otp_end"].ToString());

                                if (currentDateTime > otpDateTime)
                                {
                                    //update the otp and default the otp_end
                                    sqlCmd.CommandText = "UPDATE dbo.otp SET otp = '0', otp_end = '1999-01-01 12:00:00' WHERE account_id = @account_id AND otp != '0'";
                                    sqlCmd.Parameters.Add("@account_id", SqlDbType.Int).Value = (int)row["account_id"];

                                    Database.ExecuteStatement(sqlCmd, 0);
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
