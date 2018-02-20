using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public partial class frmSplashScreen<T> : Form
    {
        #region Public PROPERTIES

        public T Result { get; set; }

        public Exception Exception { get; set; }

        #endregion

        #region Public CONSTRUCTORS

        public frmSplashScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void Initialize(Func<Task<T>> iTask, string iMessage)
        {
            lblMessage.Text = iMessage;
            _task = iTask;
        }

        public void ThrowIfExceptionRaised()
        {
            if (Exception != null)
                throw Exception;
        }

        #endregion

        #region Private FIELDS

        private Func<Task<T>> _task;

        #endregion

        #region Private METHODS

        private async void frmSplashScreen_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
                if (_task != null)
                    Result = await _task();
                else
                    throw new Exception("Il n'y a aucune action ou fonction de définie ou les deux sont définies");
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            finally
            {
                Close();
            }
        }

        #endregion
    }
}