using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Topping.Core.Data.SQL
{
	using System;
	using System.Globalization;
	using System.Security.Cryptography;
	using System.Text;

	public static class Password
	{
		public static string GetHash(string password)
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5 md5Hasher = MD5.Create();

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(password));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2", CultureInfo.InvariantCulture));

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		// Verify a hash against a string.
		public static bool VerifyHash(string password, string hash)
		{
			// Hash the input.
			string hashOfInput = GetHash(password);

			// Return true if they are the same
			return String.Compare(hashOfInput, hash, StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
}
