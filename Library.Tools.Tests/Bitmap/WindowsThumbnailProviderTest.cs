using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Bitmap;
using System.Drawing;

namespace Library.Tools.Tests.Bitmap
{
    [TestClass]
    public class WindowsThumbnailProviderTest
    {
        [TestMethod]
        public void GetThumbnail()
        {
            try
            {
                int THUMB_SIZE = 500;
                var thumbnail = WindowsThumbnailProvider.GetThumbnail(Library.Tools.IO.MyDirectory.GetSolutionDirectory() + PARTPATH, THUMB_SIZE, THUMB_SIZE, ThumbnailOptions.None);
                if (thumbnail == null)
                    throw new Exception();

                var thumbnail2 = WindowsThumbnailProvider.GetThumbnail(Library.Tools.IO.MyDirectory.GetSolutionDirectory() + ASMPPATH, THUMB_SIZE, THUMB_SIZE, ThumbnailOptions.None);
                if (thumbnail2 == null)
                    throw new Exception();

                //var tempFile = Library.Tools.IO.MyFile.GenerateTempFilePath("bmp");
                //thumbnail.Save(tempFile);

                //System.Diagnostics.Process.Start(tempFile);

                //var PDMTEst = @"C:\_LECAPITAINE_DEV\T1-Test\SheetMetalPart.SLDPRT";
                //var thumbnail3 = WindowsThumbnailProvider.GetThumbnail(PDMTEst, THUMB_SIZE, THUMB_SIZE, ThumbnailOptions.None);
                //if (thumbnail3 == null)
                //    throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private const string PARTPATH = @"_UnitTestData\ASM1.SLDASM";
        private const string ASMPPATH = @"_UnitTestData\Part1.SLDPRT";

    }
}
