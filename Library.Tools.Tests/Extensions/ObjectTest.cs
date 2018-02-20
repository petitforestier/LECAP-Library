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
	public class ObjectTest
	{
		[TestMethod]
		public void Serialization_Deserialization()
		{
			var newObject = new Product() ;
			newObject.Id = 12;
			newObject.Name= "nom";
			newObject.Attributs = new List<string>(){"item1","item2"};

			string serial = newObject.Serialize();
			var deserial = serial.Deserialize<Product>();

			if(deserial.Id != newObject.Id || deserial.Name != newObject.Name)
				throw new Exception();

			for(int a = 0; a<newObject.Attributs.Count;a++)
			{
				if(newObject.Attributs[a] != deserial.Attributs[a])
					throw new Exception();
			}

			for(int a = 0; a<deserial.Attributs.Count;a++)
			{
				if(newObject.Attributs[a] != deserial.Attributs[a])
					throw new Exception();
			}
		}

		public class Product
		{
			public long Id {get;set;}
			public string Name {get;set;}
			public List<string> Attributs{get;set;}
		}
	}
}
