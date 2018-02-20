using System;

namespace Library.Tools.Extensions
{
    public static class MyDateTime
    {
        #region Public METHODS

        /// <summary>
        /// A Appeler pour arrondir une date avec écriture en base qui fera un arrondit de toute façon, il permet d'assurer un cohérence de valeur en cas de comparaison
        /// </summary>
        public static DateTime Round(this DateTime iDateTime)
        {
            return new DateTime(iDateTime.Year, iDateTime.Month, iDateTime.Day, iDateTime.Hour, iDateTime.Minute, iDateTime.Second);
        }

        /// <summary>
        /// A appeler pour mettre la date dans un nom de fichier, séparateur -, année-mois-jour
        /// </summary>
        public static string ToStringYMD(this DateTime iDateTime)
        {
            return iDateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// A appeler pour mettre la date dans un nom de fichier, séparateur -, année-mois-jour-heure-minute-seconde
        /// </summary>
        public static string ToStringYMDHMS(this DateTime iDateTime)
        {
            return iDateTime.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        /// <summary>
        ///Date format francais
        /// </summary>
        public static string ToStringDMY(this DateTime iDateTime)
        {
            return iDateTime.ToString("dd/MM/yyyy");
        }

        /// <summary>
        ///Date et heure format francais
        /// </summary>
        public static string ToStringDMYHMS(this DateTime iDateTime)
        {
            return iDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }


        /// <summary>
        /// Retourne la date en string en acceptant que la date soit null
        /// </summary>
        /// <param name="iDateTime"></param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTime? iDateTime)
        {
            if (iDateTime == null) return null;
            return ((DateTime)iDateTime).ToShortDateString();
        }

        /// <summary>
        /// Gets the 12:00:00 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteStart(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteEnd(this DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
        }

        #endregion
    }
}