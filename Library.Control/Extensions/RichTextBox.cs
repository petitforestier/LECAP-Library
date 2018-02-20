using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Library.Tools.Extensions;

namespace Library.Control.Extensions
{
	public static class MyRichTextBox
	{
		#region Public METHODS

		/// <summary>
		/// Ajout une ligne de text dans un richBox
		/// </summary>
		public static void AddText(this RichTextBox iRichTextBox, string iText, int iMaxLinesCount)
		{
			if (iRichTextBox.Lines.Count() >= iMaxLinesCount)
				iRichTextBox.Lines = iRichTextBox.Lines.Skip(1).ToArray();

			if (!iRichTextBox.IsDisposed)
			{
				iRichTextBox.AppendText(iText + Environment.NewLine);
				iRichTextBox.ScrollToCaret();
			}
		}

		#endregion Public METHODS
	}
}