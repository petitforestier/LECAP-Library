using System;
using System.Reflection;

namespace Library.Excel
{
	public static class Extensions
	{
		#region Public METHODS

		public static int GetExcelColumnWidth(this object value)
		{
			Type type = value.GetType();
			FieldInfo fieldInfo = type.GetField(value.ToString());
			ExcelColumnWidthAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(ExcelColumnWidthAttribute), false) as ExcelColumnWidthAttribute[];
			return attribs.Length > 0 ? attribs[0].ExcelColumnWidth : 0;
		}

		#endregion Public METHODS
	}

	public class ExcelColumnWidthAttribute : Attribute
	{
		#region Public PROPERTIES

		public int ExcelColumnWidth { get; protected set; }

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		public ExcelColumnWidthAttribute(int value)
		{
			this.ExcelColumnWidth = value;
		}

		#endregion Public CONSTRUCTORS
	}
}