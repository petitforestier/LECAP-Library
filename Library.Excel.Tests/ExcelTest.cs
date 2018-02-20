using Library.Tools.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Library.Excel.Tests
{
    [TestClass]
    public class ExcelTest
    {
        #region Public METHODS

        [TestMethod]
        public void SendGetFile()
        {
            var liste = new List<FakeClass>();
            liste.Add(new FakeClass() { Tata = "efze1", Toto = "efe", Date = DateTime.Now, Decimal = 1.89m, Int = 10 });
            liste.Add(new FakeClass() { Tata = "efze2", Toto = "efe" });
            liste.Add(new FakeClass() { Tata = "efzef3", Toto = "efe", Date = DateTime.Now, Decimal = 1.89m, Int = 10 });
            liste.Add(new FakeClass() { Tata = "efzef4", Toto = "efe" });
            liste.Add(new FakeClass() { Toto = "efe" });
            liste.Add(new FakeClass() { Toto = "efe" });

            var liste2 = new List<FakeClass2>();
            liste2.Add(new FakeClass2() { Tata = "efze1", Toto = "efe" });
            liste2.Add(new FakeClass2() { Tata = "julien", Toto = "efe" });
            liste2.Add(new FakeClass2() { Tata = "efzef3", Toto = "efe" });
            liste2.Add(new FakeClass2() { Tata = "efzef4", Toto = "efe" });
            liste2.Add(new FakeClass2() { Toto = "efe" });
            liste2.Add(new FakeClass2() { Toto = "efe" });

            var excel = new ExcelTools(new CancellationTokenSource());
            //écriture du fichier

            var excelList = new List<Library.Excel.Object.ExcelSheet<object>>();
            var listeObject = new List<object>();

            foreach (var item in liste)
                listeObject.Add(item);

            var listeObject2 = new List<object>();
            foreach (var item in liste2)
                listeObject2.Add(item);

            excelList.Add(new Library.Excel.Object.ExcelSheet<object>() { DataList = listeObject, SheetName = "1" });
            excelList.Add(new Library.Excel.Object.ExcelSheet<object>() { DataList = listeObject2, SheetName = "2" });

            var fullFilePath = excel.SendListToNewExcelFile(excelList);

            var columnIndexList = new List<int>() { 1, 2, 3, 4, 5 };

            //Lecture du fichier
            var dataList = excel.GetListFromExcelFile(fullFilePath, columnIndexList, 1, true, 1);
            Assert.AreEqual(7, dataList.Count);

            var dataList2 = excel.GetListFromExcelFile(@"E:\Développements\Logiciels\Library\_UnitTestData\test.xlsx", columnIndexList, 1, true, 1);
            Assert.AreEqual(4, dataList2.Count);
        }

        #endregion

        #region Private CLASSES

        private class FakeClass
        {
            #region Public PROPERTIES

            public string Tata { get; set; }
            public string Toto { get; set; }
            public DateTime? Date { get; set; }
            public decimal Decimal { get; set; }
            public int Int { get; set; }

            #endregion
        }

        private class FakeClass2
        {
            #region Public PROPERTIES

            public string Tata { get; set; }
            public string Toto { get; set; }

            #endregion
        }

        #endregion
    }
}