using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Library.Tools.Extensions;

namespace Library.Control.UserControls
{
	public partial class ucPictures : UserControl
	{
		#region Public CONSTRUCTORS

        public ucPictures() : this(true)
        {
        }

		public ucPictures(bool iShowZoomButton)
		{
			InitializeComponent();
			picImage.SizeMode = PictureBoxSizeMode.Zoom;
			cmdZoom.Visible = iShowZoomButton;

			cmdZoom.Enabled = false;
			cmdNext.Enabled = false;
			cmdPrevious.Enabled = false;
			lblPagination.Text = "0/0";
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		/// <summary>
		/// Chargement des images
		/// </summary>
		public void LoadPictures(List<Image> iImageList)
		{
			ImageList = iImageList;
			LoadPicture(0);
		}

        public void ShowLast()
        {
            if (ImageList.IsNotNullAndNotEmpty())
                LoadPicture(ImageList.Count - 1);
        }

        public void ShowFirst()
        {
            if (ImageList.IsNotNullAndNotEmpty())
                LoadPicture(0);
        }

        public Image SelectedImage { get; set; }

		#endregion Public METHODS

		#region Private FIELDS

		private int DisplayedIndex = 0;
		private List<Image> ImageList;

		#endregion Private FIELDS

		#region Private METHODS

		private void cmdNext_Click(object sender, EventArgs e)
		{
			LoadPicture(DisplayedIndex + 1);
		}

		private void cmdPrevious_Click(object sender, EventArgs e)
		{
			LoadPicture(DisplayedIndex - 1);
		}

		private void cmdZoom_Click(object sender, EventArgs e)
		{
			var zoomForm = new Form();
			var zoomUC = new ucPictures(false);

			zoomForm.Size = new Size(800, 800);
			zoomUC.LoadPictures(ImageList);
			zoomForm.StartPosition = FormStartPosition.CenterScreen;
			zoomForm.Controls.Add(zoomUC);
			zoomUC.Dock = DockStyle.Fill;

			zoomForm.ShowDialog();
		}

		private void LoadPicture(int iImageIndex)
		{
            var imageCount = 0;
            var imageCounter = 0;
			if (ImageList.IsNullOrEmpty())
			{
                SelectedImage = null;
				picImage.Image = null;
				cmdPrevious.Enabled = false;
				cmdNext.Enabled = false;
				cmdZoom.Enabled = false;
			}
			else
			{
				Image theImage = ImageList.ElementAtOrDefault(iImageIndex);
				if (theImage != null)
				{
                    SelectedImage = theImage;
					picImage.Image = theImage;
					DisplayedIndex = iImageIndex;
					cmdZoom.Enabled = true;
				}

				if (DisplayedIndex == 0)
					cmdPrevious.Enabled = false;
				else
					cmdPrevious.Enabled = true;

				if (DisplayedIndex + 1 >= ImageList.Count())
					cmdNext.Enabled = false;
				else
					cmdNext.Enabled = true;

                imageCount = ImageList.Count();
                imageCounter = DisplayedIndex + 1;
			}

            lblPagination.Text = string.Format("{0}/{1}", imageCounter, imageCount);
		}

		#endregion Private METHODS
	}
}