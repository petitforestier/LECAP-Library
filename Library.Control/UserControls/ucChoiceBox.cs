
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Control.Extensions;

namespace Library.Control.UserControls
{
    public partial class ucChoiceBox<T> : UserControl, IUcUserControl
    {
        #region Public FIELDS

        public event EventHandler Close = delegate { };

        #endregion

        #region Public PROPERTIES

        public T AnswerChoice { get; private set; }
        public DialogResult DialogResult { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        public ucChoiceBox(string iMessage, Dictionary<T,string> iChoiceDic)
        {
            InitializeComponent();
            lblMessage.Text = iMessage;
            cboChoice.DropDownStyle = ComboBoxStyle.DropDownList;
            cboChoice = cboChoice.FillByDictionary(iChoiceDic);
        }

        #endregion

        #region Private METHODS

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close(sender, e);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            AnswerChoice= (T)cboChoice.SelectedValue;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(sender, e);
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdOk_Click(sender, e);
        }

        public void Initialize()
        {
        }

        #endregion

    }
}