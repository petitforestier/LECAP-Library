using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Tools.Extensions;

namespace Library.Control.UserControls
{
    public partial class ucDataGridViewWebBrowserCompare<TParent, TChild> : UserControl
    {
        #region Public CONSTRUCTORS

        public ucDataGridViewWebBrowserCompare()
        {
            InitializeComponent();
            wbbBrowser.ScriptErrorsSuppressed = true;
            dgvProduct.MultiSelect = false;
            dgvProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduct.CausesValidation = false;

            dgvVariant.MultiSelect = false;
            dgvVariant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVariant.CausesValidation = false;
        }

        #endregion Public CONSTRUCTORS

        #region Public METHODS

        public void LoadData(List<TParent> iList, Func<TParent, Uri> iUrlProperty, Func<TParent, List<TChild>> iVariantsProperty)
        {
            UrlProperty = iUrlProperty;
            VariantProperty = iVariantsProperty;

            if (iList.IsNotNullAndNotEmpty())
                dgvProduct.DataSource = iList;
        }

        #endregion Public METHODS

        #region Private FIELDS

        private Func<TParent, Uri> UrlProperty;
        private Func<TParent, List<TChild>> VariantProperty;

        #endregion Private FIELDS

        #region Private METHODS

        private void dgvProduct_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 1)
            {
                TParent selection = (TParent)dgvProduct.SelectedRows[0].DataBoundItem;
                Uri url = UrlProperty(selection);
                if (url.IsNotNullAndNotEmpty())
                {
                    wbbBrowser.Navigate(url);
                }

                List<TChild> variantList = VariantProperty(selection);
                dgvVariant.DataSource = variantList;
            }
        }

        #endregion Private METHODS
    }
}
