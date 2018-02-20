namespace Library.Tools.Encryption
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	public static class AsymmetricEncryption
	{
		#region Public METHODS

		public static string GetEncryption(string input)
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		public static bool IsVerifyEncryption(string input, string hash)
		{
			// Hash the input.
			string hashOfInput = GetEncryption(input);

			// Create a StringComparer an compare the hashes.
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion Public METHODS
	}
}