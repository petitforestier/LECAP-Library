using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

namespace Library.Tools.Extensions
{
	public static class MyPath
	{
		#region Public METHODS

		/// <summary>
		/// Retourne le chemin du dossier avec l'ajout d'un séparateur à la fin si manquant
		/// </summary>
		public static string CorrectFolderPath(this string iFolderPath)
		{
			if (iFolderPath.Last().ToString() != @"\") 
				iFolderPath += @"\";
			return iFolderPath;
		}

		#endregion Public METHODS
	}
}