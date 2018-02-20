using Library.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.Enums
{
    public enum StatusEnum
    {
        [Name("FR", "Nouveau")]
        New = 1,

        [Name("FR", "Modification")]
        Modified = 2,
    }
}