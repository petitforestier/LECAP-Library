using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public partial class ucMessageBox : UserControl
    {
        public ucMessageBox(string iMessage)
        {
            InitializeComponent();
            lblMessage.Text = iMessage;
        }

        public void SetMessage(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetMessage), new object[] { value });
                return;
            }
            lblMessage.Text = value;
        }
    }
}
