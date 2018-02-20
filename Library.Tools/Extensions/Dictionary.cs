namespace Library.Tools.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public static class MyDictionary
    {
        #region Public METHODS

        /// <summary>
        /// Ajouter un item si la clé n'est pas existante
        /// </summary>
        public static void AddIfNoExistingKey<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey iKey, TValue iValue)
        {
            if (!source.ContainsKey(iKey))
                source.Add(iKey, iValue);
        }

        /// <summary>
        /// Ajouter un dictionnaire complet dans un autre, les clé en doublons seront ignorées
        /// </summary>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection is null");
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                    source.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// retourne un dictionnaire vide s'il est null. A utiliser dans les foreach pour éviter les erreurs
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="iDic"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue>> Enum<TKey, TValue>(this Dictionary<TKey, TValue> iDic)
        {
            return iDic ?? Enumerable.Empty<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        /// Retourne le KeyValuePair of dictionary by key
        /// </summary>
        public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
        }

        /// <summary>
        /// Retourne si le dictionnaire est non null and count != 0
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="iDic"></param>
        /// <returns></returns>
        public static bool IsNotNullAndNotEmpty<TKey, TValue>(this Dictionary<TKey, TValue> iDic)
        {
            if (iDic != null)
            {
                if (iDic.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Retourne si le dictionnaire est null or count = 0
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="iDic"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> iDic)
        {
            return !iDic.IsNotNullAndNotEmpty();
        }

        /// <summary>
        /// Retourne le dictionnaire sous forme xml (sérialisation)
        /// </summary>
        public static string SerializeDictionary<TKey, TValue>(this Dictionary<TKey, TValue> iDic)
        {
            using (var textWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Item<TKey, TValue>[]));

                serializer.Serialize(textWriter, iDic.Select(kv => new Item<TKey, TValue>() { Key = kv.Key, Value = kv.Value }).ToArray());
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Retourne le dictionnaire
        /// </summary>
        public static Dictionary<TKey, TValue> DeserializeDictionary<TKey, TValue>(this string iSerialization)
        {
            //prise en charge de l'ancienne version de serialization
            if (iSerialization.StartsWith("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<items "))
                return new Dictionary<TKey, TValue>();
           
            using (var textWriter = new StringReader(iSerialization))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Item<TKey, TValue>[]));
                return ((Item<TKey, TValue>[])serializer.Deserialize(textWriter)).ToDictionary(i => i.Key, i => i.Value);
            }
        }

        /// <summary>
        /// Compare 2 dictionnaire entre eux. Comparaison des clés et des valeurs
        /// </summary>
        public static bool DictionaryEqual<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            if (first == second) return true;
            if (first.IsNullOrEmpty() && second.IsNullOrEmpty()) return true;
            if ((first == null) || (second == null)) return false;
            if (first.Count() != second.Count()) return false;

            var comparer = EqualityComparer<TValue>.Default;

            foreach (KeyValuePair<TKey, TValue> kvp in first)
            {
                TValue secondValue;
                if (!second.TryGetValue(kvp.Key, out secondValue)) return false;
                if (!comparer.Equals(kvp.Value, secondValue)) return false;
            }
            return true;
        }

        #endregion

        #region Public CLASSES

        public class Item<TKey, TValue>
        {
            #region Public FIELDS

            [XmlAttribute]
            public TKey Key;

            [XmlAttribute]
            public TValue Value;

            #endregion
        }

        #endregion
    }
}