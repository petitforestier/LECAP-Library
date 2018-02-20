using System;
using System.Windows.Forms;
using System.Drawing;

namespace Library.Control.Datagridview
{
	public static class Extensions
	{
		#region Public METHODS

		public static DataGridViewContentAlignment GetContentAlignment(this Type iType, string iPropertyString)
		{
			ContentAlignmentAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(ContentAlignmentAttribute), false) as ContentAlignmentAttribute[];
			return attribs.Length > 0 ? attribs[0].ContentAlignment : DataGridViewContentAlignment.MiddleLeft;
		}

		public static bool GetReadOnly(this Type iType, string iPropertyString)
		{
			ReadOnlyAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(ReadOnlyAttribute), false) as ReadOnlyAttribute[];
			return attribs.Length > 0 ? true : false;
		}

		public static bool GetSortable(this Type iType, string iPropertyString)
		{
			SortableAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(SortableAttribute), false) as SortableAttribute[];
			return attribs.Length > 0 ? true : false;
		}

		public static bool GetVisible(this Type iType, string iPropertyString)
		{
			VisibleAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(VisibleAttribute), false) as VisibleAttribute[];
			return attribs.Length > 0 ? true : false;
		}

		public static bool GetFrozen(this Type iType, string iPropertyString)
		{
			FrozenAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(FrozenAttribute), false) as FrozenAttribute[];
			return attribs.Length > 0 ? true : false;
		}

		public static int? GetWidthColumn(this Type iType, string iPropertyString)
		{
			WidthColumnAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(WidthColumnAttribute), false) as WidthColumnAttribute[];
			return attribs.Length > 0 ? (int?)attribs[0].WidthColumn : null;
		}

		public static bool GetMultiLineText(this Type iType, string iPropertyString)
		{
			MultiLineTextAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(MultiLineTextAttribute), false) as MultiLineTextAttribute[];
			return attribs.Length > 0 ? true : false;
		}

        public static Color? GetBackColor(this Type iType, string iPropertyString)
        {
            BackColorAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(BackColorAttribute), false) as BackColorAttribute[];
            KnownColor? knowColor = attribs.Length > 0 ? (KnownColor?)attribs[0].Color : null;
            if (knowColor == null)
                return null;
            else
               return Color.FromKnownColor((KnownColor)knowColor);
        }

		#endregion Public METHODS
	}

	public class CellStyleAttribute : Attribute
	{
		#region Public PROPERTIES

		public DataGridViewCellStyle CellStyle { get; protected set; }

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		public CellStyleAttribute(DataGridViewCellStyle value)
		{
			this.CellStyle = value;
		}

		#endregion Public CONSTRUCTORS
	}

	public class ContentAlignmentAttribute : Attribute
	{
		#region Public PROPERTIES

		public DataGridViewContentAlignment ContentAlignment { get; protected set; }

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		public ContentAlignmentAttribute(DataGridViewContentAlignment value)
		{
			this.ContentAlignment = value;
		}

		#endregion Public CONSTRUCTORS
	}

	public class ReadOnlyAttribute : Attribute
	{
		#region Public CONSTRUCTORS

		public ReadOnlyAttribute()
		{
		}

		#endregion Public CONSTRUCTORS
	}

	public class SortableAttribute : Attribute
	{
		#region Public CONSTRUCTORS

		public SortableAttribute()
		{
		}

		#endregion Public CONSTRUCTORS
	}

	public class VisibleAttribute : Attribute
	{
		#region Public CONSTRUCTORS

		public VisibleAttribute()
		{
		}

		#endregion Public CONSTRUCTORS
	}

	public class MultiLineTextAttribute : Attribute
	{
		#region Public CONSTRUCTORS

		public MultiLineTextAttribute()
		{
		}

		#endregion Public CONSTRUCTORS
	}

	public class FrozenAttribute : Attribute
	{
		#region Public CONSTRUCTORS

		public FrozenAttribute()
		{
		}

		#endregion Public CONSTRUCTORS
	}

	public class WidthColumnAttribute : Attribute
	{
		#region Public PROPERTIES

		public int WidthColumn { get; protected set; }

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		public WidthColumnAttribute(int value)
		{
			this.WidthColumn = value;
		}

		#endregion Public CONSTRUCTORS
	}

    public class BackColorAttribute : Attribute
    {
        #region Public PROPERTIES

        public KnownColor Color { get; protected set; }

        #endregion Public PROPERTIES

        #region Public CONSTRUCTORS

        public BackColorAttribute(KnownColor value)
        {
            this.Color = value;
        }

        #endregion Public CONSTRUCTORS
    }

}