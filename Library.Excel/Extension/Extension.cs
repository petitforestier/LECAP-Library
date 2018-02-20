using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Library.Excel.Extension
{
    public static class Extension
    {
         public static bool IsNullOrEmpty(this ICell iCell)
         {
             if (iCell == null)
                 return true;
             else if (iCell.ToString() == string.Empty)
                 return true;
             else
                 return false;
         }

         public static bool IsNotNullOrNotEmpty(this ICell iCell)
         {
             return !IsNullOrEmpty(iCell);
         }
    }
}
