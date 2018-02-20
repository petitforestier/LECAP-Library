using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.Extensions
{
	public static class MyTimeSpan
	{
		#region Public METHODS

		/// <summary>
		/// Retourne une chaine de caractère donnant le durée dans l'unité appropriée (soit en heures, minutes, jours etc)
		/// </summary>
		public static string ToSmartString(this TimeSpan iTimeSpan)
		{
			if (iTimeSpan < new TimeSpan(0, 1, 0))
				return "{0} seconde(s)".FormatString(iTimeSpan.Seconds);
			else if (iTimeSpan < new TimeSpan(1, 0, 0))
				return "{0} minute(s)".FormatString(iTimeSpan.Minutes);
			else if (iTimeSpan < new TimeSpan(24, 0, 0))
				return "{0} heure(s)".FormatString(iTimeSpan.Hours);
			else if (iTimeSpan < new TimeSpan(31, 0, 0, 0))
				return "{0} jour(s)".FormatString(iTimeSpan.Days);
			else if (iTimeSpan < new TimeSpan(365, 0, 0, 0))
				return "{0} mois".FormatString((int)iTimeSpan.Days / 31);
			else
				return "{0} année(s)".FormatString((int)iTimeSpan.Days / 365);
		}

		#endregion Public METHODS
	}
}