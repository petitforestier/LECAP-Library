using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.Text;

namespace Library.Tools.Extensions
{
   
    public static class MyObject
    {
        #region Public METHODS

        /// <summary>
        /// Retourne la valeur d'une propriété depuis son nom string
        /// </summary>
        public static object CallByName<T>(this T iObject, string iPropertyName)
        {
            return iObject.GetType().GetProperty(iPropertyName).GetValue(iObject, null);
        }

        /// <summary>
        /// Clone un objet, par copie des propriétés par serialization. L'objet doit être serializable
        /// </summary>
        public static T Clone<T>(this T iSource)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("The type must be serializable.", "source");

            if (System.Object.ReferenceEquals(iSource, null))
                return default(T);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, iSource);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Retourne l'objet sous forme xml (sérialisation)
        /// </summary>
        public static string Serialize<T>(this T iObject)
        {
            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(iObject.GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, iObject, emptyNamepsaces);
                return stream.ToString();
            }
        }

        /// <summary>
        /// Retourne l'objet
        /// </summary>
        public static T Deserialize<T>(this string iString)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (var textWriter = new StringReader(iString))
                    return (T)xmlSerializer.Deserialize(textWriter);
            }
            catch (Exception)
            {
                return default(T);
            }
        }     

        public static Dictionary<string, string> GetPropertiesValue<T>(this object iObject)
        {
            var dic = new Dictionary<string, string>();


            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.CanRead)
                    dic.Add(propertyInfo.Name, propertyInfo.GetValue(iObject, null).ToString());
            }
            return dic;
        }

        #endregion Public METHODS
    }
}