using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Extensions;
using System.IO;

namespace Library.Tools.Tests.Extensions
{
	[TestClass]
	public class ImageTest
	{

		[TestMethod]
		public void GetHashCode_SameTo()
		{
			Image image1 = Image.FromFile(GetUnitTestData() + ImagePath1);
			Image image2 = Image.FromFile(GetUnitTestData() + ImagePath2);
			Image image3 = Image.FromFile(GetUnitTestData() + ImagePath3);

			Library.Tools.Debug.MyDebug.PrintInformation(image1.GetHashSHA1());
			Library.Tools.Debug.MyDebug.PrintInformation(image2.GetHashSHA1());
			Library.Tools.Debug.MyDebug.PrintInformation(image3.GetHashSHA1());

			if (image1.GetHashSHA1() != image2.GetHashSHA1())
				throw new Exception();

			if (!image1.SameTo(image2))
				throw new Exception();

			if (image1.GetHashSHA1() == image3.GetHashSHA1())
				throw new Exception();

			if (image1.SameTo(image3))
				throw new Exception();
		}

		[TestMethod]
		public void ToStream_GetFormat()
		{
			Image image1 = Image.FromFile(GetUnitTestData() + ImagePath3);

			if (image1.GetFormat() != ImageFormat.Jpeg) throw new Exception();
			if (image1.GetFormat() == ImageFormat.Png) throw new Exception();

			Stream theStream = image1.ToStream();
			Image image2 = Image.FromStream(theStream);
			if (image2.GetFormat() != ImageFormat.Jpeg) throw new Exception();
		}

		[TestMethod]
		public void Convert()
		{
			Image image1 = Image.FromFile(GetUnitTestData() + ImagePath4);
			if (image1.GetFormat() != ImageFormat.Bmp) throw new Exception();

			Image image2 = image1.ConvertFormat(ImageFormat.Jpeg);
			if (image2.GetFormat() != ImageFormat.Jpeg) throw new Exception();
			image2.Save(@"C:\temp\test.jpeg");		
		}

		[TestMethod]
		public void ScaleImage()
		{
			Image image1 = Image.FromFile(GetUnitTestData() + ImagePathSmall);
			var image2 = image1.ScaleImage(10, 10);
			image2.Save(@"C:\temp\test10.jpeg");

			var image3 = image1.ScaleImage(500, 500);
			image3.Save(@"C:\temp\test500.jpeg");

			Stream theStream = image2.ToStream();
			Image image4 = Image.FromStream(theStream);
			if (image2.GetFormat() != ImageFormat.Jpeg) throw new Exception();

		}

		[TestMethod]
		public void ScaleImage2()
		{
			Image image1 = Image.FromFile(GetUnitTestData() + ImagePath1);
			var image2 = image1.ScaleImage(1000, 1000);
			image1.Save(@"C:\temp\ori.jpeg");
			image2.Save(@"C:\temp\sca.jpeg");
		}

        private string GetUnitTestData()
        {
            var dir = Library.Tools.IO.MyDirectory.GetSolutionDirectory() + @"_UnitTestData\";
            if (!System.IO.Directory.Exists(dir))
                throw new Exception("Le dossier de test est inexistant");
            return dir;
        }

		#region Private FIELDS

		private string ImagePath1 = @"o1.jpeg";
		private string ImagePath2 = @"o1 - Copie.jpeg";
		private string ImagePath3 = @"o2(1).JPG";
		private string ImagePath4 = @"Image 176.bmp";
		private string ImagePathSmall = @"small.jpg";

		#endregion Private FIELDS
	}
}
