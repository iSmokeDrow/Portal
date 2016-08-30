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

        protected static bool debug = OPT.GetBool("debug");

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

        public static void HandleOTP()
        {
            int expiredOTP = 0;

            Task.Run(() =>
            {
                using (SqlConnection sqlCon = Database.Connection)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SELECT account_id, otp, expiration FROM dbo.otp WHERE expiration != '1999-01-01 12:00:00'", sqlCon))
                    {
                        DataTable otpTable = (DataTable)Database.ExecuteStatement(sqlCmd, 2);
                        if (otpTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in otpTable.Rows)
                            {
                                DateTime currentDateTime = DateTime.Now;
                                DateTime otpDateTime = DateTime.Parse(row["expiration"].ToString());

                                if (currentDateTime > otpDateTime)
                                {
                                    expiredOTP++;

                                    //update the otp and default the otp_end
                                    sqlCmd.CommandText = "UPDATE dbo.otp SET otp = '0', expiration = '1999-01-01 12:00:00' WHERE account_id = @account_id AND otp != '0'";
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
