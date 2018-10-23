using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;
using Library.Tools.Extensions;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public enum InputTypeAllowEnum
    {
        All,
        Numeric
    }

    public partial class ucInputBox : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close = delegate { };

        #endregion

        #region Public PROPERTIES

        public string AnswerString { get; private set; }
        public DialogResult DialogResult { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        public ucInputBox(string iMessage, InputTypeAllowEnum iInputTypeAllow)
        {
            InitializeComponent();

            if (iMessage.IsNullOrEmpty())
                throw new Exception("Le message est requis");

            lblMessage.Text = iMessage;

            InputTypeAllow = iInputTypeAllow;
        }

        #endregion

        #region Public METHODS

        public void Initialize()
        {
        }

        #endregion

        #region Private FIELDS

        private InputTypeAllowEnum InputTypeAllow;

        #endregion

        #region Private METHODS

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close(sender, e);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            AnswerString = txtValue.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(sender, e);
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (InputTypeAllow == InputTypeAllowEnum.Numeric)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
            else if (InputTypeAllow == InputTypeAllowEnum.All)
            {
                //ne rien faire
            }
            else
                throw new NotSupportedException(InputTypeAllow.ToStringWithEnumName());
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdOk_Click(sender, e);
            }
        }

        #endregion
    }
}