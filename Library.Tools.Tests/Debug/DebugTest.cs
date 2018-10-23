using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Debug;

namespace Library.Tools.Tests
{
    [TestClass]
    public class DebugTest
    {
        [TestMethod]
        public void PrintInformation()
        {
            MyDebug.PrintInformation("test");



        }
    }
}
