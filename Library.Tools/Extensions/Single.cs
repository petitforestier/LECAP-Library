namespace Library.Tools.Extensions
{
	using System;

	public static class MySingle
	{
		#region Public METHODS

		public static bool IsNotNull(this Single iText)
		{
			if (iText != 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsNull(this Single iText)
		{
			return !iText.IsNotNull();
		}

		#endregion Public METHODS
	}
}