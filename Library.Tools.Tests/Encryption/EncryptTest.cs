using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
	[TestClass]
	public class EncryptTest
	{

		[TestMethod]
		public void SymmetricEncrypt()
		{
			string password = "Test";

			Random random = new Random();

			byte[] key = new byte[16];		
			random.NextBytes(key);

			byte[] key2 = new byte[16];
			random.NextBytes(key2);

			byte[] vector = new byte[8];
			random.NextBytes(vector);

			string encryptPassword = Library.Tools.Encryption.SymmetricEncryption.Encrypt(password,key,vector);

			string trueDecryptPassword = Library.Tools.Encryption.SymmetricEncryption.Decrypt(encryptPassword, key, vector);
			string falseDecryptPassword = Library.Tools.Encryption.SymmetricEncryption.Decrypt(encryptPassword, key2, vector);

			if (trueDecryptPassword != password) throw new Exception();
			if (falseDecryptPassword == password) throw new Exception();
		}

	}
}
