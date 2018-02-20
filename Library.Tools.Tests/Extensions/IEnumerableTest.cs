using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;
using Library.Tools.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;

namespace Library.Tools.Tests.Extensions
{
	[TestClass]
	public class IEnumerableTest
	{
		#region Public METHODS

		[TestMethod]
		public void Concat()
		{
			var list = new List<string>();
			list.Add("prem");
			list.Add("sec");
			list.Add("trm");

			if (list.Concat(" ") != "prem sec trm")
				throw new Exception();

			if(list.Concat(Environment.NewLine) != "prem" + Environment.NewLine +"sec" + Environment.NewLine + "trm")
				throw new Exception();
		}


		[TestMethod]
		public void Split()
		{
			var list = new List<string>();
			list.Add("prem");
			list.Add("sec");
			list.Add("trm");
			list.Add("prem");
			list.Add("sec");
			list.Add("trm");
			list.Add("prem");
			list.Add("sec");
			list.Add("trm");
			list.Add("prem");
			list.Add("sec");
			list.Add("trm");

			if (list.SplitByCount(3).Count() != 3) throw new Exception();
			if (list.SplitBySize(3).Count() != 4) throw new Exception();
		}

        [TestMethod]
        public void RemoveByCount()
        {
            var list = new List<string>();
            list.Add("dezdz");
            list.Add("dezdz");
            list.Add("dezdz");
            list.Add("dezdz");

            var result = list.RemoveByCount(2);
            if (result.Count() != 2) throw new Exception();

        }


		[TestMethod]
		public void SerializeDeserialize()
		{
			var list = new List<ConstantState>();

			list.Add(new ConstantState() { Name = "zef", Value = "éd" });
			list.Add(new ConstantState() { Name = "zef", Value = "zefz" });
			list.Add(new ConstantState() { Name = "zefzedzed", Value = "éd" });

			var serializeString = list.SerializeList();

			var resultList = serializeString.DeserializeList<ConstantState>();
            if (resultList.Count() != list.Count())
                throw new Exception();

		}

		public class ConstantState
		{
			[XmlAttribute]
			public string Name { get; set; }

			[XmlAttribute]
			public string Value { get; set; }
		}



		#endregion Public METHODS
	}
}