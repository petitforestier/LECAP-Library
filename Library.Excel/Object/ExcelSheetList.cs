using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Excel.Object
{
    public class ExcelSheet<T>
    {
        public List<T> DataList {get;set;}
        public List<string> iMessageList {get;set;}
        public string SheetName {get;set;}
        public string Lang {get;set;}
    }


}
