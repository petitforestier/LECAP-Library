namespace Library.Tools.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Serialization;
	using System.IO;
    using System.Data;
    using System.ComponentModel;

    public static class MyIEnumerable
	{
		#region Public METHODS

		/// <summary>
		/// Retourne une liste un enum vide si l'enum en entré est null. A utilisé dans les foreach pour éviter les erreurs sur les liste null
		/// </summary>
		public static IEnumerable<T> Enum<T>(this IEnumerable<T> iEnum)
		{
			return iEnum ?? Enumerable.Empty<T>();
		}

		/// <summary>
		/// Retourne la liste des doublons par rapport à la propriété spécifié.
		/// </summary>
		public static List<T> GetDuplicates<T>(this  IEnumerable<T> iList, Func<T, IComparable> iUniqueProperty, bool iKeepFirstOnly)
		{
			var duplicateList = iList.GroupBy(iUniqueProperty).Where(x => x.Count() > 1);
			if (iKeepFirstOnly)
				return duplicateList.Select(x => x.First()).ToList();
			else
				return duplicateList.SelectMany(x => x).ToList();
		}

		/// <summary>
		/// Retourne la liste en gardant la première occurence de chaque doublon
		/// </summary>
		public static List<T> GetWithoutDuplicates<T>(this  IEnumerable<T> iList, Func<T, IComparable> iUniqueProperty)
		{
			return iList.GroupBy(iUniqueProperty).Select(x => x.First()).ToList();
		}

		/// <summary>
		/// Retourne si la list est non null ou count != 0
		/// </summary>
		public static bool IsNotNullAndNotEmpty<T>(this IEnumerable<T> iList)
		{
			if (iList != null)
			{
				if (iList.Count() != 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Retourne si la list est null ou count = 0
		/// </summary>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> iList)
		{
			return !iList.IsNotNullAndNotEmpty();
		}

		/// <summary>
		/// Retourne une chaine des éléments de la liste avec le séparateur entre chaques lignes
		/// </summary>
		public static string Concat(this IEnumerable<string> iList, string iSeparator)
		{
			return iList.Aggregate((i, j) => i + iSeparator + j);
		}

		/// <summary>
		/// Découpe une liste en un nombre de liste choisie. La taille des listes s'ajuste.
		/// </summary>
		public static IEnumerable<IEnumerable<T>> SplitByCount<T>(this IEnumerable<T> iList, int iCount)
		{
			int i = 0;
			var splits = from item in iList
						 group item by i++ % iCount into part
						 select part.AsEnumerable();
			return splits;
		}

		/// <summary>
		/// Découpe une liste en plusieurs listes de taille choisie. C'est le nombre qui s'ajuste.
		/// </summary>
		public static IEnumerable<IEnumerable<T>> SplitBySize<T>(this IEnumerable<T> source, int iSize)
		{
			if (iSize <= 0)
				throw new ArgumentOutOfRangeException("length");

			var section = new List<T>(iSize);

			foreach (var item in source)
			{
				section.Add(item);

				if (section.Count()== iSize)
				{
					yield return section.AsReadOnly();
					section = new List<T>(iSize);
				}
			}

			if (section.Count()> 0)
				yield return section.AsReadOnly();
		}    

        /// <summary>
        /// Retourne une liste cloner avec les mêmes Item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iSource"></param>
        /// <returns></returns>
        public static IEnumerable<T> CloneList<T>(this IEnumerable<T> iSource)
        {
            var newList = new List<T>();
            foreach (var item in iSource.Enum())
                newList.Add(item);
            return newList;
        }

        /// <summary>
        /// Retourne une liste avec le nombre de lignes max spécifié
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public static IEnumerable<T> RemoveByCount<T>(this IEnumerable<T> iSource, int iCount)
        {
            if (iSource.IsNullOrEmpty()) return null;

            var resultList = new List<T>();

            int counter = 0;
            foreach (var item in iSource.Enum())
            {
                if (counter < iCount)
                {
                    resultList.Add(item);
                    counter++;
                }       
            }
            return resultList;
        }

        /// <summary>
        /// Idem exists mais gère la source null;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iSource"></param>
        /// <param name="iMatch"></param>
        /// <returns></returns>
        public static bool Exists2<T>(this IEnumerable<T> iSource, Predicate<T> iMatch)
        {
            if(iSource.IsNullOrEmpty()) return false;

            return iSource.ToList().Exists(iMatch);
        }

        /// <summary>
        /// Idem exists mais gère la source null;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iSource"></param>
        /// <param name="iMatch"></param>
        /// <returns></returns>
        public static bool NotExists2<T>(this IEnumerable<T> iSource, Predicate<T> iMatch)
        {
            if (iSource.IsNullOrEmpty()) return true;

            return !iSource.ToList().Exists(iMatch);
        }

        /// <summary>
        ///  Sérialization d'une list of T. Les propriétés de la classe à sérializer doit posséder l'attribut [XmlAttribute]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static string SerializeList<T>(this List<T> iList)
        {

            using (var textWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T[]));

                serializer.Serialize(textWriter, iList.ToArray());
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Désérialization d'un string en list of T. Les propriétés de la classe à sérializer doit posséder l'attribut [XmlAttribute]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iSerialization"></param>
        /// <returns></returns>
        public static List<T> DeserializeList<T>(this string iSerialization)
        {

            using (var textWriter = new StringReader(iSerialization))
            {
                var serializer = new XmlSerializer(typeof(T[]));
                return ((T[])serializer.Deserialize(textWriter)).ToList();
            }

        }

        /// <summary>
        /// Retourne un tableau 2D d'une liste de type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object[,] ToStringArray<T>(this IEnumerable<T> iData)
        {
            if (iData == null)
                return null;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var array = new object[iData.Count(), properties.Count];

            var rowIndex = 0;
            foreach (T item in iData)
            {
                var columnIndex = 0;
                foreach (PropertyDescriptor prop in properties)
                {
                    array[rowIndex, columnIndex] = prop.GetValue(item) ?? DBNull.Value;
                    columnIndex++;
                }
                rowIndex++;
            }
            return array;

        }

        #endregion Public METHODS
    }
}