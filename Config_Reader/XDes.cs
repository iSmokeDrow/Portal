using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Config_Reader
{
	// Made smaller changes
	// Code from: http://stackoverflow.com/questions/6808652/des-ecb-encryption-and-decryption
	public class XDes
	{
		private byte[] Key;
		
		public XDes(string pKey)
		{
			Key = new byte[8];
			byte[] temp = Encoding.ASCII.GetBytes(pKey);
			for (int i = 0; i < 8; i++)
			{
				if (temp.Length <= i) Key[i] = 0x0;
				else Key[i] = temp[i];
			}
		}

		public string Decrypt(string encryptedString)
		{
			return Decrypt(Convert.FromBase64String(encryptedString));
		}

		public string Decrypt(byte[] encryptedData)
		{
			DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
			desProvider.Mode = CipherMode.ECB;
			desProvider.Padding = PaddingMode.Zeros;
			desProvider.Key = Key;
			using (MemoryStream stream = new MemoryStream(encryptedData))
			{
				using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateDecryptor(), CryptoStreamMode.Read))
				{
					using (StreamReader sr = new StreamReader(cs))
					{
						return sr.ReadToEnd();
					}
				}
			}
		}

		public byte[] Encrypt(string decryptedString)
		{
			DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
			desProvider.Mode = CipherMode.ECB;
			desProvider.Padding = PaddingMode.Zeros;
			desProvider.Key = Key;
			using (MemoryStream stream = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateEncryptor(), CryptoStreamMode.Write))
				{
					byte[] data = Encoding.Default.GetBytes(decryptedString);
					cs.Write(data, 0, data.Length);
					cs.FlushFinalBlock();
					Console.WriteLine(BitConverter.ToString(stream.ToArray()));
					return stream.ToArray();
				}
			}
		}
	}
}
