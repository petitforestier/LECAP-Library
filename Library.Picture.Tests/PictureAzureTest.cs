using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Extensions;
using System.IO;

namespace Library.Data.Picture.Tests
{
	[TestClass]
	public class PictureAzureTest
	{

		[TestMethod]
		public void SaveImageToFolder()
		{
			string imagePath1 = @"E:\Développements\Données\Saphir\Catalogue\Images\o1.jpeg";
			string imagePath2 = @"E:\Développements\Données\Saphir\Catalogue\Images\o1";


			var test = Path.ChangeExtension(imagePath2, Image.FromFile(imagePath1).GetFormat().ToString());

		}
		
	}
}