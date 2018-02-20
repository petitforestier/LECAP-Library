using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public partial class ucDataGridView : UserControl
    {
        #region Public EVENTS

        public event EventHandler LoadRequested = delegate { };

        #endregion

        #region Public CONSTRUCTORS

        public ucDataGridView()
        {
            InitializeComponent();
           
        }

        public void Initialize(int iDefaultSizePage, int iDefaultPage, bool iWithSortComboBox, bool iWithSearch)
        {
            DefaultSizePage = iDefaultSizePage;
            DefaultPage = iDefaultPage;
            txtPage.Text = DefaultPage.ToString();
            txtSize.Text = DefaultSizePage.ToString();
            cmdSortDirection.Tag = ListSortDirection.Ascending;
            cmdSortDirection.ToolTipText = "Rangé par ordre croissant, cliquer pour changer l'ordre";

            cboColumnSorts.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            if (iWithSortComboBox == false)
            {
                cboColumnSorts.Visible = false;
                cmdSortDirection.Visible = false;
                sepSort.Visible = false;

                cboColumnSorts.SelectedIndexChanged -= cboColumnSorts_SelectedIndexChanged;
                cmdSortDirection.Click -= cmdSortDirection_Click;
            }

            if (iWithSearch == false)
            {
                txtSearch.Visible = false;
                cmdSearch.Visible = false;
                sepSearch.Visible = false;
            }
        }

        #endregion

        #region Protected FIELDS

        protected bool IsLoading = false;

        #endregion

        #region Protected METHODS

        protected async virtual Task LoadDataAsync()
        {
            LoadRequested(this,null);
            await Task.Yield();
        }

        #endregion

        #region Private FIELDS

        private BoolLock IsControlLoading = new BoolLock();
        private BoolLock IsClickLoading = new BoolLock();
        private int DefaultPage;
        private int DefaultSizePage;

        #endregion

        #region Private METHODS

        private async void cboColumnSorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    await LoadDataAsync();
                    dgvData.Select();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void cmdFirstPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    txtPage.Text = 1.ToString();
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void cmdLastPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    txtPage.Text = lblPageCount.Text;
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void cmdNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    if (Convert.ToInt32(txtPage.Text) < Convert.ToInt32(lblPageCount.Text))
                    {
                        txtPage.Text = (Convert.ToInt32(txtPage.Text) + 1).ToString();
                        await LoadDataAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void cmdPreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    if (Convert.ToInt32(txtPage.Text) > 1)
                    {
                        txtPage.Text = (Convert.ToInt32(txtPage.Text) - 1).ToString();
                        await LoadDataAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void cmdSortDirection_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    switch ((ListSortDirection)cmdSortDirection.Tag)
                    {
                        case ListSortDirection.Ascending:
                            cmdSortDirection.Tag = ListSortDirection.Descending;
                            cmdSortDirection.Image = Properties.Resources.sort_descending_icon;
                            cmdSortDirection.ToolTipText = "Rangé par ordre décroissant, cliquer pour changer l'ordre";
                            break;

                        case ListSortDirection.Descending:
                            cmdSortDirection.Tag = ListSortDirection.Ascending;
                            cmdSortDirection.Image = Properties.Resources.sort_ascending_icon;
                            cmdSortDirection.ToolTipText = "Rangé par ordre croissant, cliquer pour changer l'ordre";
                            break;

                        default:
                            throw new Exception("Ordre de tri non supporté");
                    }
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtPage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (IsLoading) return;
                if (e.KeyCode == Keys.Enter)
                {
                    dgvData.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                const char Delete = (char)8;
                e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void txtPage_Validated(object sender, EventArgs e)
        {
            try
            {
                if (IsLoading) return;
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    if (Convert.ToInt32(txtPage.Text) > Convert.ToInt32(lblPageCount.Text))
                        txtPage.Text = lblPageCount.Text;
                    {
                        await LoadDataAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (IsLoading) return;
                if (e.KeyCode == Keys.Enter)
                    dgvData.Focus();
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                const char Delete = (char)8;
                e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private async void txtSize_Validated(object sender, EventArgs e)
        {
            try
            {
                if (IsControlLoading.Value) return;
                if (IsClickLoading.Value) return;
                using (new BoolLocker(ref IsClickLoading))
                {
                    txtPage.Text = 1.ToString();
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        #endregion
    }
}