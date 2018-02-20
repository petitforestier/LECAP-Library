namespace Library.Tools.Extensions
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Security.Cryptography;

	public static class MyImage
	{
		#region Public METHODS

		/// <summary>
		/// Retourne le stream d'une image
		/// </summary>
		public static Stream ToStream(this Image iImage)
		{
			var stream = new System.IO.MemoryStream();
			iImage.Save(stream, iImage.RawFormat);
			stream.Position = 0;
			return stream;
		}

		/// <summary>
		/// Retourne le hashcode d'ume image. Permet de comparer deux images indirectement. Ne pas utiliser le GetHashCode pour les images (basé sur l'adressage mémoire), il faut utiliser le GetHashCode2.
		/// </summary>
		public static string GetHashSHA1(this Image iImage, bool iQuickCompute = false)
		{
			Image theImage = iImage;
			if (iQuickCompute)
				theImage = (Image)(new Bitmap(iImage, new Size(30, 30)));

			var converter = new ImageConverter();
			var imageByte = (byte[])converter.ConvertTo(theImage, typeof(byte[]));

			return Convert.ToBase64String(SHA1.Create().ComputeHash(imageByte)).Replace("-", string.Empty).String().Reduce(28);
		}

		/// <summary>
		/// Retourne le format de l'image. Ne peut utiliser directement la propriété rawFormat, ne fonctionne pas...
		/// </summary>
		public static ImageFormat GetFormat(this Image iImage)
		{
			if (ImageFormat.Bmp.Equals(iImage.RawFormat))
				return ImageFormat.Bmp;
			else if (ImageFormat.Emf.Equals(iImage.RawFormat))
				return ImageFormat.Emf;
			else if (ImageFormat.Exif.Equals(iImage.RawFormat))
				return ImageFormat.Exif;
			else if (ImageFormat.Gif.Equals(iImage.RawFormat))
				return ImageFormat.Gif;
			else if (ImageFormat.Icon.Equals(iImage.RawFormat))
				return ImageFormat.Icon;
			else if (ImageFormat.Jpeg.Equals(iImage.RawFormat))
				return ImageFormat.Jpeg;
			else if (ImageFormat.MemoryBmp.Equals(iImage.RawFormat))
				return ImageFormat.MemoryBmp;
			else if (ImageFormat.Png.Equals(iImage.RawFormat))
				return ImageFormat.Png;
			else if (ImageFormat.Tiff.Equals(iImage.RawFormat))
				return ImageFormat.Tiff;
			else if (ImageFormat.Wmf.Equals(iImage.RawFormat))
				return ImageFormat.Wmf;
			else
				throw new Exception("Format non supporté");
		}

		/// <summary>
		/// Redimensionne l'image à la taille désirée en respectant les proportions
		/// </summary>
		public static Image ScaleImage(this Image iImage, int iMaxWidth, int iMaxHeight)
		{
			if (iMaxHeight == 0 || iMaxWidth == 0) return iImage;
			if (iMaxWidth >= iImage.Width && iMaxHeight >= iImage.Height) return iImage;

			var ratioX = (double)iMaxWidth / iImage.Width;
			var ratioY = (double)iMaxHeight / iImage.Height;
			var ratio = Math.Min(ratioX, ratioY);

			var newWidth = (int)(iImage.Width * ratio);
			var newHeight = (int)(iImage.Height * ratio);

			var newImage = new Bitmap(newWidth, newHeight);
			Graphics.FromImage(newImage).DrawImage(iImage, 0, 0, newWidth, newHeight);
			
			Stream newStream = new MemoryStream();
			System.Drawing.Imaging.ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
			var encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality,100L);
			newImage.Save(newStream, info[1],encoderParameters);

			return Image.FromStream(newStream);
		}

		/// <summary>
		/// Compare deux images d'un point vue bitmap
		/// </summary>
		public static bool SameTo(this Image iFirstImage, Image iSecondImage, bool iQuickCheck = true)
		{
			return (iFirstImage.GetHashSHA1(iQuickCheck) == iSecondImage.GetHashSHA1(iQuickCheck));
		}

		/// <summary>
		/// Retourne l'image avec le format spécifié
		/// </summary>
		public static Image ConvertFormat(this Image iImage, ImageFormat iImageFormat)
		{
			if (iImage.GetFormat() == iImageFormat)
				return iImage;
			var streamNewFormat = new MemoryStream();
			iImage.Save(streamNewFormat, iImageFormat);
			return Image.FromStream(streamNewFormat);
		}

        /// <summary>
        /// Retourne une image sans le blanc autour
        /// </summary>
        /// <param name="iImage"></param>
        /// <returns></returns>
        public static Bitmap CropWhite(this Image iImage)
        {
            var bmp = (Bitmap)iImage;
            int w = bmp.Width;
            int h = bmp.Height;

            Func<int, bool> allWhiteRow = row =>
            {
                for (int i = 0; i < w; ++i)
                    if (bmp.GetPixel(i, row).R != 255)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = col =>
            {
                for (int i = 0; i < h; ++i)
                    if (bmp.GetPixel(col, i).R != 255)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (allWhiteRow(row))
                    topmost = row;
                else break;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (allWhiteRow(row))
                    bottommost = row;
                else break;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (allWhiteColumn(col))
                    leftmost = col;
                else
                    break;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (allWhiteColumn(col))
                    rightmost = col;
                else
                    break;
            }

            if (rightmost == 0) rightmost = w; // As reached left
            if (bottommost == 0) bottommost = h; // As reached top.

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;

            if (croppedWidth == 0) // No border on left or right
            {
                leftmost = 0;
                croppedWidth = w;
            }

            if (croppedHeight == 0) // No border on top or bottom
            {
                topmost = 0;
                croppedHeight = h;
            }

            try
            {
                var target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
                  ex);
            }
        }

        #endregion Public METHODS
    }
}