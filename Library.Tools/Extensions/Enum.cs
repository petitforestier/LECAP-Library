namespace Library.Tools.Extensions
{
    using Library.Tools.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

	public static class MyEnum
	{
		#region Public METHODS

		/// <summary>
		/// Return un dictionnaire de l'enum, utilisation :"new Enum().ToDictionary()"
		/// </summary>
		public static Dictionary<int, string> ToDictionary(this Enum iEnum)
		{
			var type = iEnum.GetType();
			return Enum.GetValues(type).Cast<int>().ToDictionary(e => e, e => Enum.GetName(type, e));
		}

		/// <summary>
		/// Retourne un dictionnaire d'une énumération avec l'enum en clé et le nom dans la langue demandée
		/// </summary>
		public static Dictionary<TEnum, string> ToDictionary<TEnum>(this TEnum iEnum, string iLang)
		{
			var dico = new Dictionary<TEnum, string>();

			foreach (TEnum key in iEnum.GetType().GetEnumValues())
				dico.Add(key, key.GetName(iLang));

			return dico;
		}

		/// <summary>
		/// Retourne une liste des valeurs de l'enumération
		/// </summary>
		public static List<string> ToList(this Enum iEnum)
		{
			return Enum.GetNames(iEnum.GetType()).ToList();
		}

		/// <summary>
		/// Retourne la liste des noms données par l'attribut name
		/// </summary>
		/// <param name="iEnum"></param>
		/// <param name="iLang"></param>
		/// <returns></returns>
		public static List<string> ToNameList(this Enum iEnum, string iLang)
		{
			var list = new List<string>();

			foreach (Enum key in iEnum.GetType().GetEnumValues())
				list.Add(key.GetName(iLang));

			return list;
		}

		/// <summary>
		/// Retourne l'enum parser depuis un string
		/// </summary>
		public static TEnum ParseEnumFromValue<TEnum>(this Enum iEnum, string value)
		{
			return (TEnum)Enum.Parse(typeof(TEnum), value, true);
		}

		/// <summary>
		/// Retourne le nom de l'enum et la valeur
		/// </summary>
		/// <param name="iEnum"></param>
		/// <returns></returns>
		public static string ToStringWithEnumName(this Enum iEnum)
		{
			if (iEnum == null)
				return "Null";
			else
				return "Nom enum : {0} // valeur : {1}".FormatString(iEnum.GetType().Name, iEnum.ToString());
		}

		/// <summary>
		/// Retourne l'enum depuis le nom renseigné dans l'attribut
		/// </summary>
		/// <param name="iName"></param>
		/// <returns></returns>
		public static Enum ParseEnumFromName<Enum>(this Enum iEnum, string iName, string iLang)
        {
			var dic = new Dictionary<Enum, string>();
			foreach (Enum key in iEnum.GetType().GetEnumValues())
				dic.Add(key, key.GetName(iLang));

			if (dic.Exists2(x => x.Value == iName))
				return dic.Single(x => x.Value == iName).Key;
			else
				return default(Enum);
		}
       

        #endregion
    }
}