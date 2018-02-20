namespace Library.Tools.Attributes
{
	using System;
	using System.Linq;
	using System.Reflection;
    using System.ComponentModel;

	public static class Extensions
	{
		#region Public METHODS

		public static string GetName(this object iValue, string iLang)
		{
			Type type = iValue.GetType();
			FieldInfo fieldInfo = type.GetField(iValue.ToString());
			NameAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(NameAttribute), false) as NameAttribute[];
			NameAttribute name = (NameAttribute)attrs.Where(a => a is NameAttribute).Where(a => ((NameAttribute)a).lang == iLang).SingleOrDefault();
			return name?.GetName();
		}

		public static string GetName(this Type iType, string iPropertyString, string iLang)
		{
			NameAttribute[] attribs = iType.GetProperty(iPropertyString).GetCustomAttributes(typeof(NameAttribute), false) as NameAttribute[];
			NameAttribute name = (NameAttribute)attribs.Where(a => a is NameAttribute).Where(a => ((NameAttribute)a).lang == iLang).SingleOrDefault();
			return name?.GetName();
		}

        public static string GetDescription(this object iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            DescriptionAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            DescriptionAttribute name = (DescriptionAttribute)attrs.SingleOrDefault();
            return name?.Description;
        }

		#endregion Public METHODS
	}

	[System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
	public class NameAttribute : Attribute
	{
		#region Public FIELDS

		public string lang;

		#endregion Public FIELDS

		#region Public CONSTRUCTORS

		public NameAttribute(string iLang, string iValue)
		{
			this.name = iValue;
			lang = iLang;
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		public string GetName()
		{
			return name;
		}

		#endregion Public METHODS

		#region Private FIELDS

		private string name;

		#endregion Private FIELDS
	}
}