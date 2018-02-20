using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Library.Control.Datagridview
{
    public static class ImageHelper
    {
        public static Image GetProgressionBarImage(int iPercentage, int iRowHeight, int iColumnWidth, bool iWithText)
        {
            iRowHeight -= 1;
            iColumnWidth -= 2;

            if (iPercentage < 0 || iPercentage > 100)
                throw new Exception("La valeur de pourcentage n'est pas valide");

            if (iWithText && iRowHeight < 8)
                throw new Exception("La hauteur de l'image est trop petite");

            var textFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular, GraphicsUnit.Point);
            var textString = iPercentage.ToString() + "%";

            Bitmap Bmp = new Bitmap(iColumnWidth, iRowHeight);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            {
                var columnWidth = (int)Math.Round(decimal.Multiply(decimal.Divide(iPercentage, 100), iColumnWidth));

                gfx.FillRectangle(Brushes.ForestGreen, 0, 0, columnWidth, iRowHeight);
                using (var thePen = new Pen(Brushes.White, 2))
                    gfx.DrawRectangle(thePen, 0, 0, columnWidth, iRowHeight);

                SizeF textSize = gfx.MeasureString(textString, textFont);
                var textLocation = new PointF((iColumnWidth - textSize.Width) / 2, (iRowHeight - textSize.Height) / 2);
                gfx.DrawString(textString, textFont, Brushes.Black, textLocation);
            }

            return Bmp;
        }

    }
}
