
using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public partial class ucMessageEditableBox : UserControl, IUcUserControl
    {
        #region Public FIELDS

        public event EventHandler Close = delegate { };

        #endregion

        #region Public PROPERTIES

        public DialogResult DialogResult { get; private set; }
        public string ResultText
        {
            get
            {
                return txtText.Text;
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public ucMessageEditableBox(string iMessage, string iText)
        {
            InitializeComponent();
            lblMessage.Text = iMessage;
            txtText.Text = iText;
        }

        #endregion

        #region Private METHODS

        private void cmdSend_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = System.Windows.Forms.DialogResult.Yes;
                Close(sender, e);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close(sender, e);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        public void Initialize()
        {
        }

        #endregion
    }
}