namespace Library.Tools.Extensions
{
	using System;

	public static class MyInt
	{
		#region Public METHODS

		/// <summary>
		/// Retourne si le nombre est un multiple du diviseur (si le résultat de la division est un nombre entier)
		/// </summary>
		public static bool IsDivisible(this int iNumber, int iDivisor)
		{
			return (iNumber % iDivisor) == 0;
		}

		public static bool IsNotNull(this int iText)
		{
			if (iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNotNull(this int? iText)
		{
			if (iText != null &&
				iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNotNull(this Int16 iText)
		{
			if (iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNotNull(this Int16? iText)
		{
			if (iText != null &&
				iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNotNull(this Int64 iText)
		{
			if (iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNotNull(this Int64? iText)
		{
			if (iText != null &&
				iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNull(this int iText)
		{
			return !iText.IsNotNull();
		}

		public static bool IsNull(this int? iText)
		{
			return !iText.IsNotNull();
		}

		public static bool IsNull(this Int16 iText)
		{
			return !iText.IsNotNull();
		}

		public static bool IsNull(this Int16? iText)
		{
			return !iText.IsNotNull();
		}

		public static bool IsNull(this Int64 iText)
		{
			return !iText.IsNotNull();
		}

		public static bool IsNull(this Int64? iText)
		{
			return !iText.IsNotNull();
		}

		#endregion Public METHODS
	}
}