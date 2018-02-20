using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;

namespace Library.Control.UserControls
{
    public partial class ucWebBrowser : UserControl
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        [DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        public ucWebBrowser()
        {
            InitializeComponent();   
        }

        /// <summary>
        /// Ouvre la page demandé dans le browser avec les cookies demandés
        /// </summary>
        /// <param name="iUrl"></param>
        /// <param name="iCookieContainer"></param>
        public void NavigateWithCookie(Uri iUrl, CookieContainer iCookieContainer)
        {
            CookieCollection cookies = iCookieContainer.GetCookies(iUrl);

            if( cookies.Count != 0)
            {
                //clean cookies
                ClearCookies();
                
                for (int i = 0; i < cookies.Count; i++)
                {
                    Cookie c = cookies[i];
                    InternetSetCookie(iUrl.AbsoluteUri, c.Name, c.Value);
                }
                wbbWebBrowser.Navigate(iUrl);
            }
            else
            {
                wbbWebBrowser.Navigate(iUrl);
            }
        }

        private unsafe void ClearCookies()
        {
            int option = (int)3;
            int* optionPtr = &option;

            bool success = InternetSetOption(0, 81, new IntPtr(optionPtr), sizeof(int));
        }

    }
}
