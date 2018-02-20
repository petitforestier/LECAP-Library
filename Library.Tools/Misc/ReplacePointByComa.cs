using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Tools.Misc
{
    public static class ReplacePointByComa
    {
        public static void ReplacePointByComaKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
                e.KeyChar = ',';
        }
    }
}
