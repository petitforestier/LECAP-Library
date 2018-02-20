using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Control.UserControls
{
    using System.Windows.Forms;

    public enum IconEnum
    {
        GEAR
    }

    public partial class frmUserControl : Form
    {
        #region Public CONSTRUCTORS

        public frmUserControl(UserControl iUserControl, string iTitle, bool iAllowResize, bool iFullScreen = false, IconEnum iIcon = IconEnum.GEAR)
        {
            InitializeComponent();

            if (iUserControl != null && typeof(IUcUserControl).IsAssignableFrom(iUserControl.GetType()))
                ((IUcUserControl)iUserControl).Close += this.userControl_Closed;

            this.Controls.Add(iUserControl);
            this.Size = new Size(iUserControl.Size.Width + 16, iUserControl.Size.Height + 39);
            if (iAllowResize == false)
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
            iUserControl.Dock = DockStyle.Fill;
            this.StartPosition = FormStartPosition.CenterScreen;

            if (iIcon == IconEnum.GEAR)
                this.Icon = Properties.Resources.Hopstarter_Soft_Scraps_Gear;
            else
                throw new NotSupportedException();

            if (iFullScreen)
                this.WindowState = FormWindowState.Maximized;

            this.Text = iTitle;
        }

        private void userControl_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}