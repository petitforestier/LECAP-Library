namespace Library.Tools.Encryption
{
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;

	public static class SymmetricEncryption
	{
		#region Public METHODS

		/// <summary>
		/// Décrypte le text avec une clé et une vector qui doivent être identique au cryptage
		/// </summary>
		public static string Decrypt(string iText, byte[] iKey, byte[] iVector)
		{
			try
			{
				return Transform(iText, _cryptoService.CreateDecryptor(iKey, iVector));
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Crypte le text avec une clé et une vector
		/// </summary>
		public static string Encrypt(string iText, byte[] iKey, byte[] iVector)
		{
			return Transform(iText, _cryptoService.CreateEncryptor(iKey, iVector));
		}

		#endregion Public METHODS

		#region Private FIELDS

		private static SymmetricAlgorithm _cryptoService = new TripleDESCryptoServiceProvider();

		#endregion Private FIELDS

		#region Private METHODS

		private static string Transform(string iText, ICryptoTransform iCryptoTransform)
		{
			MemoryStream stream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(stream, iCryptoTransform, CryptoStreamMode.Write);

			byte[] input = Encoding.Default.GetBytes(iText);

			cryptoStream.Write(input, 0, input.Length);
			cryptoStream.FlushFinalBlock();

			return Encoding.Default.GetString(stream.ToArray());
		}

		#endregion Private METHODS
	}
}