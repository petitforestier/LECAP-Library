using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.MyType;


namespace Library.Tools.Tests.Type
{
    [TestClass]
    public class IsNullable
    {
        [TestMethod]
        public void IsNullableTest()
        {
            enumTest? nullable = enumTest.one;
            if (Library.Tools.MyType.MyType.IsNullable(nullable) == false) throw new Exception();

            enumTest nullable1 = enumTest.one;
            if (Library.Tools.MyType.MyType.IsNullable(nullable1) == true) throw new Exception();
        }


        private enum enumTest
        {
            one,
            two
        }
    }
}
