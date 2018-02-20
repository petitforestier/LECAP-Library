using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public partial class ucNavigator : UserControl
    {
        #region Public EVENTS

        public event EventHandler LoadRequested = delegate { };

        #endregion

        #region Public PROPERTIES

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                lblPageCount.Text = Math.Ceiling(decimal.Divide(value, Convert.ToInt32(txtSize.Text))).ToString();
            }
        }

        public int PageNumber
        {
            set
            {
                txtPage.Text = value.ToString();
            }
        }

        public int Take
        {
            get
            {
                return Convert.ToInt32(txtSize.Text);
            }
        }

        public int Skip
        {
            get
            {
                return Convert.ToInt32(txtSize.Text) * (Convert.ToInt32(txtPage.Text) - 1);
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public ucNavigator()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void Initialize(int iDefaultSizePage, int iDefaultPage)
        {
            _defaultSizePage = iDefaultSizePage;
            _defaultPage = iDefaultPage;
            txtPage.Text = _defaultPage.ToString();
            txtSize.Text = _defaultSizePage.ToString();
        }

        #endregion

        #region Protected METHODS

        protected void SizeLeaved()
        {
            txtPage.Text = 1.ToString();
            LoadRequested(this,null);
            this.ActiveControl = null;
        }

        protected void PageLeaved()
        {
            if (Count == 0)
                txtPage.Text = "1";
            else if (Convert.ToInt32(txtPage.Text) > Convert.ToInt32(lblPageCount.Text) && Convert.ToInt32(txtPage.Text) != 1)
                txtPage.Text = lblPageCount.Text;
            LoadRequested(this, null);
            this.ActiveControl = null;
        }

        #endregion

        #region Private FIELDS

        private Label _fakeLabel = new Label();

        private int _count;

        private BoolLock _isLoading = new BoolLock();

        private int _defaultPage;

        private int _defaultSizePage;

        #endregion

        #region Private METHODS

        private void cmdFirstPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                {
                    if (txtPage.Text != 1.ToString())
                    {
                        txtPage.Text = 1.ToString();
                        LoadRequested(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdLastPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                {
                    var currentPage = Convert.ToInt32(txtPage.Text);
                    var lastPage = Convert.ToInt32(lblPageCount.Text);
                    if (currentPage < lastPage)
                    {
                        txtPage.Text = lblPageCount.Text;
                        LoadRequested(sender,e);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                {
                    if (Convert.ToInt32(txtPage.Text) < Convert.ToInt32(lblPageCount.Text))
                    {
                        txtPage.Text = (Convert.ToInt32(txtPage.Text) + 1).ToString();
                        LoadRequested(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdPreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                {
                    if (Convert.ToInt32(txtPage.Text) > 1)
                    {
                        txtPage.Text = (Convert.ToInt32(txtPage.Text) - 1).ToString();
                        LoadRequested(sender,e);
                    }
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
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Back))
                        return;
                    else if (e.KeyChar == Convert.ToChar(Keys.Enter))
                        PageLeaved();
                    else
                        e.Handled = !Char.IsDigit(e.KeyChar);
                }
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
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Back))
                        return;
                    else if (e.KeyChar == Convert.ToChar(Keys.Enter))
                        SizeLeaved();
                    else
                        e.Handled = !Char.IsDigit(e.KeyChar);
                }
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtPage_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                    PageLeaved();
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtSize_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading.Value) return;
                using (new BoolLocker(ref _isLoading))
                    SizeLeaved();
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void txtPage_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtPage.Select(0, txtPage.Text.Length);
            });
        }

        private void txtSize_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtSize.Select(0, txtSize.Text.Length);
            });
        }

        #endregion
    }
}