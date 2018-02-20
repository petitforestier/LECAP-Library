using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Windows.Forms;
using Library.Tools.Debug;

namespace Library.IO.Tests
{
	[TestClass]
	public class StorageTest
	{
		[TestMethod]
		public void SaveImage()
		{
			Image theImage = Image.FromFile(ImagePath1);

			string savePath = @"C:\temp\pic";

			if (!Library.IO.Storage.SaveImageToFile(theImage, savePath)) throw new Exception();

			var getImage = Image.FromFile(savePath + ".jpeg");
			if (getImage == null) throw new Exception();
		}

        [TestMethod]
        public void FolderTree()
        {
            var rootNode = Library.IO.Storage.GetTreeFromDirectory(UnitTestFolder);
            MyDebug.PrintTreeNodeText(rootNode, 10);
        }

        //[TestMethod]
        //public void SaveFile()
        //{
        //    using( var stream = Library.IO.Storage.GetToFile(PDFPath2))
        //    {
        //        string fullPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
        //        Library.IO.Storage.SaveStreamToFile(stream, fullPath);
        //    }
        //}

        private string UnitTestFolder = @"E:\Développements\Logiciels\Library\_UnitTestData";
        private string ImagePath1 = @"E:\Développements\Logiciels\Library\_UnitTestData\o1.jpeg";
       // private string PDFPath2 = @"E:\Développements\Logiciels\Library\_UnitTestData\report1.pdf";
	}
}
