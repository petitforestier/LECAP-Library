using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;
using Library.Tools.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tools.Tests.Extensions
{
	[TestClass]
	public class DictionaryTest
	{
		[TestMethod]
		public void Serialization_Deserialization()
		{
			var dic = new Dictionary<string, string>();
			dic.Add("key1", "Value1");
			dic.Add("key2", "Value2");

			string serial = dic.SerializeDictionary();
			var dicDeserial = serial.DeserializeDictionary<string, string>();

			if(dicDeserial.Count != dic.Count) throw new Exception();
			foreach(var pairItem in dic)
				if(dicDeserial[pairItem.Key]!= pairItem.Value) 
					throw new Exception();


			foreach (var pairItem in dicDeserial)
				if (dic[pairItem.Key] != pairItem.Value)
					throw new Exception();

		}

	}
}
