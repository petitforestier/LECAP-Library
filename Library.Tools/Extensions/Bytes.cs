using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Library.Tools.Extensions
{
    public static class Bytes
    {

        /// <summary>
        /// Retourne l'image d'un tableau de byte
        /// </summary>
        public static Image ToImage(this Byte[] iBytesImage)
        {
            return (System.Drawing.Bitmap)((new ImageConverter()).ConvertFrom(iBytesImage));
        }
    }
}
