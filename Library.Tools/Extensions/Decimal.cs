using System;

namespace Library.Tools.Extensions
{
	public static class MyDecimal
	{
		#region Public METHODS

		/// <summary>
		/// Arroundi à 2 decimal
		/// </summary>
		public static decimal Round2(this decimal iDecimal)
		{
            return decimal.Round(iDecimal, 2);
		}

        /// <summary>
        /// Arroundi
        /// </summary>
        public static decimal Round(this decimal iDecimal, int iDecimals)
        {
            return decimal.Round(iDecimal, iDecimals);
        }

		#endregion Public METHODS
	}
}