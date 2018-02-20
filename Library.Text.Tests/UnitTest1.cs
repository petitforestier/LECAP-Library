using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Text.Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region Public METHODS

        [TestMethod]
        public void GetListFromCSV()
        {
            var excelTools = new Library.Text.TextTools();
            var list = excelTools.GetListFromCSVFile(TESTFILEPATH);
        }

        #endregion

        #region Private FIELDS

        private const string TESTFILEPATH = @"T:\Système d'Information\Développement\Library\Library.Text.Tests\DataTest\D2180425";

        #endregion
    }
}