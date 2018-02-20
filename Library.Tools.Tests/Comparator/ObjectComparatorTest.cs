using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Comparator;
using System.Collections.Generic;

namespace Library.Tools.Tests.Comparator
{
	[TestClass]
	public class ObjectComparatorTest
	{
		[TestMethod]
		public void Compare()
		{
			var obj1 = new ObjectFake();
			obj1.name = "ee";
			obj1.theDecimal = 10;
			obj1.objectList = null;
			obj1.stringList = new List<string>() { "ee" };

			var obj2= new ObjectFake();
			obj2.name = "ee";
			obj2.theDecimal = 10;
			obj2.objectList = new List<object>() { new object() };
			obj2.stringList = new List<string>() { "ee" };

			var objectComparator = new ObjectComparator();

			if(objectComparator.AreEqual(obj1, obj2,true) == false) throw new Exception();
            if (objectComparator.AreEqual(obj1, obj2, false) == true) throw new Exception();

			var obj3 = new ObjectFake();
			obj3.name = "ee";
			obj3.theDecimal = 11;
			obj3.objectList = null;
			obj3.stringList = new List<string>() { "ee" };

			obj2.theDecimal = 11;
            if (objectComparator.AreEqual(obj1, obj2, true) == true) throw new Exception();
		}

		class ObjectFake
		{
			public string name { get; set; }
			public decimal theDecimal {get;set;}
			public List<string> stringList { get; set; }
			public virtual List<object> objectList { get; set; }
		}

	}
}
