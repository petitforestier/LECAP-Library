using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Extensions;
using System.Collections.Generic;

namespace Library.Tools.Tests.Extensions
{
    [TestClass]
    public class EnumTest
    {
        [TestMethod]
        public void ToStringWithEnumName()
        {
            var value = MyEnum.non;
            if (value.ToStringWithEnumName().IsNullOrEmpty()) throw new Exception();

            MyEnum? value2 = null;
            if (value2.ToStringWithEnumName().IsNullOrEmpty()) throw new Exception();
        }

        [TestMethod]
        public void Exists2()
        {
            var list = new List<object>();
            list = null;

            if (list.Exists2(x => x.ToString() == "sefzef") != false) throw new Exception();


        }

        private enum MyEnum
        {
            oui = 1,
            non = 2
        }
    }
}
