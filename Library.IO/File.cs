using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.IO
{
    public static class MyFile
    {
        #region Public METHODS

        public static void MoveFile(string iSourceFilePath, string iDestinationFilePath, bool iCreateFolderIfDoesntExists, bool iOverRideFile)
        {
            var directory = Path.GetDirectoryName(iDestinationFilePath);

            if (iCreateFolderIfDoesntExists == false)
            {
                if (!Directory.Exists(directory))
                    throw new Exception("Le chemin du dossier '{0}' est inexistant et la création du dossier n'est pas autorisé".FormatString(iDestinationFilePath));
            }
            else
            {
                Directory.CreateDirectory(directory);
            }

            if (iOverRideFile && File.Exists(iDestinationFilePath))
            {
                File.SetAttributes(iDestinationFilePath, FileAttributes.Normal);
                File.Delete(iDestinationFilePath);
            }
               
            File.Move(iSourceFilePath, iDestinationFilePath);
        }

        /// <summary>
        /// Move file avec attente que le fichier soit délocké
        /// </summary>  
        /// <param name="iSourceFilePath"></param>
        /// <param name="iDestinationFilePath"></param>
        /// <param name="iCreateFolderIfDoesntExists"></param>
        /// <param name="iOverRideFile"></param>
        /// <param name="iMaxWait"></param>
        public static void MoveFile(string iSourceFilePath, string iDestinationFilePath, bool iCreateFolderIfDoesntExists, bool iOverRideFile,bool iCreateNewVersionIfNotAvailable, TimeSpan iMaxWait)
        {
            //Vérification que le fichier source est bien disponible
            WaitUnlockFile(iSourceFilePath, iMaxWait);

            //Vérification que le fichier destination est bien disposé à être écrasé s'il existe.
            var isFileNotAvailable = false;
            if (File.Exists(iDestinationFilePath))
            {
                try
                {
                    WaitUnlockFile(iDestinationFilePath, iMaxWait);
                }
                catch (TimeoutException)
                {
                    isFileNotAvailable = true;
                }
            }
           
            //Détermination du nouveau nom
            if(isFileNotAvailable)
                iDestinationFilePath = GetNextAvailableFilename(iDestinationFilePath);

            MoveFile(iSourceFilePath, iDestinationFilePath, iCreateFolderIfDoesntExists, iOverRideFile);
        }

        public static string GetNextAvailableFilename(string iFilename)
        {
            if (!System.IO.File.Exists(iFilename)) return iFilename;

            string alternateFilename;
            int fileNameIndex = 0;
            do
            {
                fileNameIndex += 1;
                alternateFilename = CreateNumberedFilename(iFilename, fileNameIndex);
            } while (System.IO.File.Exists(alternateFilename));

            return alternateFilename;
        }

        private static string CreateNumberedFilename(string iFilename, int iNumber)
        {
            string directoryPath = System.IO.Path.GetDirectoryName(iFilename);
            string plainName = System.IO.Path.GetFileNameWithoutExtension(iFilename);
            string extension = System.IO.Path.GetExtension(iFilename);
            return string.Format("{0}\\{1}_{2}{3}", directoryPath, plainName, iNumber, extension);
        }


        public static bool IsFileLocked(string iFilePath)
        {
            if (!File.Exists(iFilePath))
                throw new Exception("Le fichier '{0}' est inexistant".FormatString(iFilePath));

            FileStream stream = null;

            try
            {
                stream = File.Open(iFilePath, FileMode.Open, FileAccess.Read, FileShare.Write);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static void WaitUnlockFile(string iFilePath, TimeSpan iMaxWait)
        {
            if (iMaxWait == null)
                throw new Exception();

            if (iMaxWait.TotalMilliseconds == 0)
                throw new Exception("Le timeOut ne peut pas être de zéro");

            var startWait = DateTime.Now;

            while (IsFileLocked(iFilePath))
            {
                if (DateTime.Now - startWait > iMaxWait)
                    throw new TimeoutException("Durée d'attente de libération du fichier {0} atteinte".FormatString(iFilePath));

                System.Threading.Thread.Sleep(2000);              
            }
        }

        #endregion
    }
}