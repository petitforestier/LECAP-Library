using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.Bitmap
{
    public class ImageToPictureDispConverter : System.Windows.Forms.AxHost
    {
        #region Public CONSTRUCTORS

        public ImageToPictureDispConverter() : base("{63109182-966B-4e3c-A8B2-8BC4A88D221C}")
        {
        }

        #endregion

        #region Public METHODS

        public Image GetImageFromIPictureDisp(stdole.IPictureDisp iIPictureDisp)
        {
            return (System.Drawing.Image)System.Windows.Forms.AxHost.GetPictureFromIPicture(iIPictureDisp);
        }

        #endregion
    }
}