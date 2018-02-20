using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Control.Extensions
{
    public static class MyControl
    {
        #region Public METHODS

        /// <summary>
        /// Validation control
        /// </summary>
        public static void Validator<T>(this T iControl, Func<T, bool> iRule, string iMessage, ref string iErrorMessages) where T : System.Windows.Forms.Control
        {
            if (iRule(iControl) == true)
            {
                iControl.ForeColor = Color.White;
                iControl.BackColor = Color.Red;
                iErrorMessages = iErrorMessages.AddLine(iMessage);
            }
            else
            {
                iControl.ForeColor = Color.Black;
                iControl.BackColor = Color.White;
            }
        }

        #endregion
    }
}