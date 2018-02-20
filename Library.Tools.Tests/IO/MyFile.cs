using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tools.Tests.IO
{
    [TestClass]
    public class MyFile
    {
        [TestMethod]
        public void ReplaceExtensionFilePath()
        {
            try
            {
                var originalPath = @"C:\toto\tas.doc";
                var newPath = @"C:\toto\tas.xlsx";
                var newExtension = System.IO.Path.GetExtension(newPath);
                var replacePath = Library.Tools.IO.MyFile.ReplaceExtensionFilePath(originalPath, newExtension);
                if (newPath != replacePath)
                    throw new Exception("Chemin différent");
            }
            catch (Exception  ex)
            {
                throw ex;
            }

        }
    }
}
