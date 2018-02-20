using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.Extensions
{
    public static class MyWebBrowser
    {
        /// <summary>
		/// Vide le webBrowser
		/// </summary>
		public static void Clear(this WebBrowser iWebBrowser)
		{
            iWebBrowser.Navigate("about:blank");
		}
        
    }
}
