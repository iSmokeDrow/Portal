using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Server.Functions
{
    class PasswordCipher
    {
        /// <summary>
        /// Creates an MD5 Hash from a key and an input string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CreateHash(string key, string input)
        {
            MD5 md5 = MD5.Create();
            byte[] rawBytes = Encoding.Default.GetBytes(string.Format("{0}{1}", key, input));
            byte[] hashedBytes = md5.ComputeHash(rawBytes);
            StringBuilder sb = new StringBuilder();
            for (int currentByte = 0; currentByte < hashedBytes.Length; currentByte++)
            {
                sb.Append(hashedBytes[currentByte].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
