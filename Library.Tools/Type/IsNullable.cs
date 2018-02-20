using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.MyType
{
    public static class MyType
    {
        #region Public METHODS

        public static bool IsNullable<T>(T obj)
        {
            if (obj == null) return true; // obvious
            var type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        #endregion
    }
}