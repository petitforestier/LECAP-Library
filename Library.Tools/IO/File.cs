using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.IO
{
    public static class MyFile
    {
        #region Public METHODS

        /// <summary>
        /// Retourne si le fichier est libre, pour une suppression par exemple
        /// </summary>
        /// <param name="iFilename"></param>
        /// <returns></returns>
        public static bool IsFileReady(string iFilename)
        {
            try
            {
                using (FileStream inputStream = System.IO.File.Open(iFilename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Permet attendre la libération d'un fichier avec la spécification d'un timeout
        /// </summary>
        /// <param name="iFilename"></param>
        /// <param name="iTimeOut"></param>
        /// <returns></returns>
        public static bool WaitUntilFileIsReady(string iFilename, TimeSpan iTimeOut)
        {
            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < iTimeOut)
            {
                if (IsFileReady(iFilename))
                    return true;
                else
                    System.Threading.Thread.Sleep(500);
            }

            return false;
        }

        /// <summary>
        /// Permet attendre l'existance d'un fichier avec la spécification d'un timeout
        /// </summary>
        /// <param name="iFilename"></param>
        /// <param name="iTimeOut"></param>
        /// <returns></returns>
        public static bool WaitUntilFileExists(string iFilename, TimeSpan iTimeOut)
        {
            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < iTimeOut)
            {
                if (File.Exists(iFilename))
                    return true;
                else
                {
                   // Logger.WriteLog("Attente de l'existance du fichier '{0}'".FormatString(iFilename));
                    System.Threading.Thread.Sleep(500);
                }
            }

            return false;
        }

        /// <summary>
        /// Retourne le chemin avec la nouvelle extension
        /// </summary>
        /// <param name="iFilePath"></param>
        /// <param name="iNewExtension"></param>
        /// <returns></returns>
        public static string ReplaceExtensionFilePath(string iFilePath, string iNewExtension)
        {
            if (iFilePath.IsNullOrEmpty())
                throw new Exception("Le chemin est null");

            if (iNewExtension.IsNullOrEmpty())
                throw new Exception("La nouvelle extension est nulle");

            var currentExtension = System.IO.Path.GetExtension(iFilePath);
            if (currentExtension.IsNullOrEmpty())
                throw new Exception("L'extension actuelle est invalide");

            var path = iFilePath.Substring(0, iFilePath.Length - currentExtension.Length);

            if (!iNewExtension.Contains("."))
                iNewExtension = "." + iNewExtension;

            return path + iNewExtension;
        }

        /// <summary>
        /// Retourne un chemin de fichier temporaire qui n'existe pas avec l'extension demandée.
        /// </summary>
        /// <param name="iExtension"></param>
        /// <returns></returns>
        public static string GenerateTempFilePath(string iExtension)
        {
            if (!iExtension.Contains("."))
                iExtension = "." + iExtension;

            var filePath = System.IO.Path.GetTempPath() + Guid.NewGuid() + iExtension;

            if (System.IO.File.Exists(filePath))
                throw new Exception("Le fichier sensé être unique, ne l'est pas");

            return filePath;
        }

        #endregion
    }
}