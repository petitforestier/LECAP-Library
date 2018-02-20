using System;
using System.Text.RegularExpressions;
using System.Web;
using Library.Tools.Extensions;
using System.Collections.Generic;

namespace Library.Web
{
	public static class HtmlTools
	{
		#region Public METHODS

        private const string BREAKSTRING = "{{break}}";

        /// <summary>
        /// Nettoye les balises html, remplace les accents ( balise html) en vrai accent et supprime les caractère ascii inutiles
        /// </summary>
        /// <param name="iExpression"></param>
        /// <returns></returns>
		public static string CleanHtmlExpression(string iExpression, bool iKeepBreaks)
		{
			if (iExpression != null)
			{
				string expression = iExpression;

				try
				{
                    //Remplace les breaks
                    if (iKeepBreaks)
                    {
                        var replaceDic = new Dictionary<string, string>();
                        replaceDic.Add("<br>", BREAKSTRING);
                        replaceDic.Add("<br />", BREAKSTRING);
                        replaceDic.Add("<br/>", BREAKSTRING);
                        replaceDic.Add("</p>", BREAKSTRING);
                        replaceDic.Add(Environment.NewLine, BREAKSTRING);
                        expression = expression.Replace(replaceDic, false, false);
                    }                      

					//Balise Html
					expression = Regex.Replace(expression, "<[^>]*>", "");

					//Remplacement des accents
					expression = HttpUtility.HtmlDecode(expression);
                   
					//Caractère ASCII
					foreach (char caract in expression)
						if (Convert.ToInt32(caract) < 32 | Convert.ToInt32(caract) > 255)
							expression = expression.Replace(Convert.ToString(caract), "");

                     //Remplace les breaks
                    if (iKeepBreaks)
                        expression = expression.Replace(BREAKSTRING, Environment.NewLine);

                    //Supprime les saut de ligne double
                    expression = expression.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);

					if (expression == null)
						expression = "";
					else
						expression = expression.Trim();

					return expression;
				}
				catch (Exception)
				{
					throw;
				}
			}
			return null;
		}

		#endregion Public METHODS
	}
}