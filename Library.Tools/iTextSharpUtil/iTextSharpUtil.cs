using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Library.Tools.iTextSharpUtil
{
    public static class iTextSharpUtil
    {
        #region Public METHODS

        /// <summary>
        /// Retourne un style de texte en fonction d'un string 
        /// </summary>
        /// <param name="iStyleName"></param>
        /// <returns></returns>
        public static int GetiTextStyleByName(string iStyleName)
        {
            int style = iTextSharp.text.Font.NORMAL;
            switch (iStyleName.ToUpper())
            {
                case "BOLD":
                    style = iTextSharp.text.Font.BOLD;
                    break;
                case "ITALIC":
                    style = iTextSharp.text.Font.ITALIC;
                    break;
                case "BOLDITALIC":
                    style = iTextSharp.text.Font.BOLDITALIC;
                    break;
                case "UNDERLINE":
                    style = iTextSharp.text.Font.UNDERLINE;
                    break;
                default:
                    style = iTextSharp.text.Font.NORMAL;
                    break;
            }
            return style;
        }

        /// <summary>
        /// Retourne une couleur de type BaseColor en fonction d'un string
        /// </summary>
        /// <param name="iColorName"></param>
        /// <returns></returns>
        public static BaseColor GetiTextColorByName(string iColorName)
        {
            BaseColor color = BaseColor.BLACK;
            switch (iColorName.ToUpper())
            {
                case "BLUE":
                    color = BaseColor.BLUE;
                    break;
                case "BLACK":
                    color = BaseColor.BLACK;
                    break;
                case "CYAN":
                    color = BaseColor.CYAN;
                    break;
                case "DARK_GRAY":
                    color = BaseColor.DARK_GRAY;
                    break;
                case "GRAY":
                    color = BaseColor.GRAY;
                    break;
                case "GREEN":
                    color = BaseColor.GREEN;
                    break;
                case "LIGHT_GRAY":
                    color = BaseColor.LIGHT_GRAY;
                    break;
                case "MAGENTA":
                    color = BaseColor.MAGENTA;
                    break;
                case "ORANGE":
                    color = BaseColor.ORANGE;
                    break;
                case "PINK":
                    color = BaseColor.PINK;
                    break;
                case "RED":
                    color = BaseColor.RED;
                    break;
                case "YELLOW":
                    color = BaseColor.YELLOW;
                    break;
                case "WHITE":
                    color = BaseColor.WHITE;
                    break;
                default:
                    color = BaseColor.BLACK;
                    break;
            }
            return color;
        }

        #endregion


    }
}