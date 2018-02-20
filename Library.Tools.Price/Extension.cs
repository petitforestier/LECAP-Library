using Library.Tools.Extensions;
using Library.Tools.Tasks;
using Library.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Library.Tools.Price
{
	
	public static class MyPrice
	{
		#region Public METHODS

        /// <summary>
        /// Retourne une mise en forme du prix avec la devise et le taux de TVA;
        /// </summary>
        public static string ToString(this decimal iPrice, string iCurrency, VATEnum iVATEnum)
        {
            if (iCurrency == null) throw new ArgumentNullException("Currency");

            string taxValue = string.Empty;
           
            if (iVATEnum == VATEnum.TTC)
                taxValue = "TTC";
            else if (iVATEnum == VATEnum.HT)
                taxValue = "HT";
            else if (iVATEnum == VATEnum.EMPTY)
                taxValue = "";
            else
                throw new NotSupportedException();
           
            string amountString = "0";
            if (iPrice != 0)
            {
                var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                nfi.NumberDecimalSeparator = ",";
                amountString = iPrice.ToString("#,#.00", nfi);

                //ajout le 0 avant la virgule si besoin
                if (amountString[0] == ',')
                    amountString = "0" + amountString;
            }

            return "{0} {1} {2}".FormatString(amountString, iCurrency, taxValue).Trim();
        }

	

		#endregion Public METHODS
	}
}