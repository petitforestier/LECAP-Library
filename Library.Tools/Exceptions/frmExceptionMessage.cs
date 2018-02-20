using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Tools.Exceptions
{
    public partial class frmExceptionMessage : Form
    {
        #region Public CONSTRUCTORS

        public frmExceptionMessage(Exception iException)
        {
            InitializeComponent();

            lblMessage.Text = iException.Message;

            if (iException.GetType().BaseType == typeof(FaultException))
            {
                var faultException = ((FaultException<System.ServiceModel.ExceptionDetail>)iException);
                txtExceptionDetails.Text = GetDetailExceptionMessageRecursive(faultException.Detail);
            }
            else if (iException.InnerException != null)
            {
                txtExceptionDetails.Text = GetInnerExceptionMessageRecursive(iException);
            }
            else
            {
                txtExceptionDetails.Text = "Emplacement exception : " + iException.TargetSite.ToString() + Environment.NewLine + Environment.NewLine +
                    "Pile : " + iException.StackTrace;
            }
        }

        private int _DetailsHeight; 

        #endregion

        #region Private METHODS

        private void chkShowExceptionDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkShowExceptionDetails.Checked)
                    ShowDetails();
                else
                    HideDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowDetails()
        {
            this.Height += _DetailsHeight;
            this.tlpMain.RowStyles[3].Height = _DetailsHeight;
        }

        private void HideDetails()
        {
            this.Height -= _DetailsHeight;
            this.tlpMain.RowStyles[3].Height = 0;
        }

        private string GetInnerExceptionMessageRecursive(Exception iException)
        {
            if (iException.InnerException != null)
                return GetInnerExceptionMessageRecursive(iException.InnerException);
            else
                return iException.Message;
        }

        private string GetDetailExceptionMessageRecursive(System.ServiceModel.ExceptionDetail iException)
        {
            if (iException.InnerException != null)
                return GetDetailExceptionMessageRecursive(iException.InnerException);
            else
                return iException.Message;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExceptionMessage_Load(object sender, EventArgs e)
        {
            _DetailsHeight = (int)this.tlpMain.RowStyles[3].Height;
            HideDetails();
        }

        #endregion
    }
}