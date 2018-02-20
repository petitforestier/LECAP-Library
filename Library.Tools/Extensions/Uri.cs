namespace Library.Tools.Extensions
{
	using System;
	using System.Security.Cryptography;

	public static class MyUri
	{
		#region Public METHODS

		public static bool IsNotNullAndNotEmpty(this Uri iText)
		{
			if (iText != null && iText.AbsoluteUri.IsNotNullAndNotEmpty())
			{
				return true;
			}
			return false;
		}

		public static bool IsNullOrEmpty(this Uri iText)
		{
			return !iText.IsNotNullAndNotEmpty();
		}

		/// <summary>
		/// Retourne le hashcode de l'uri
		/// </summary>
		public static string GetHashSHA1(this Uri iUri)
		{
			if (iUri == null) return null;
			return iUri.AbsoluteUri.GetHashSHA1();
		}

        /// <summary>
        /// Retourne si une uri est Http ou Https
        /// </summary>
        /// <param name="iUri"></param>
        /// <returns></returns>
        public static bool IsHTTP (this Uri iUri)
        {
            return (iUri.Scheme == Uri.UriSchemeHttp || iUri.Scheme == Uri.UriSchemeHttps);    
        }

        /// <summary>
        /// Retourne si le string peut être utilisé comme uri
        /// </summary>
        /// <param name="iUri"></param>
        /// <returns></returns>
        public static bool IsUri(this string iUri)
        {
            try
            {
                var theUri = new Uri(iUri);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

		#endregion Public METHODS
	}
}