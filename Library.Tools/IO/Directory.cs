using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Library.Tools.IO
{
    public static class MyDirectory
    {
        #region Public METHODS

        public static string GetSolutionDirectory()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDi‌​rectory, "..\\..\\"));
        }

        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static void Cut(string sourceDirectory, string targetDirectory)
        {
            Copy(sourceDirectory, targetDirectory);
            Directory.Delete(sourceDirectory, true);
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        /// <summary>
        /// Comparaison de fichiers dans 2 dossiers
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetDirectory"></param>
        /// <returns></returns>
        public static bool IsSameContentFolders(string iSourceDirectory, string iTargetDirectory)
        {
            if (!Directory.Exists(iSourceDirectory))
                throw new Exception("Le chemin source '{0}' n'existe pas".FormatString(iSourceDirectory));

            if (!Directory.Exists(iTargetDirectory))
                throw new Exception("Le chemin cible '{0}' n'existe pas".FormatString(iTargetDirectory));

            var sourceFiles = System.IO.Directory.GetFiles(iSourceDirectory, "*.*", System.IO.SearchOption.AllDirectories).Enum().ToList();
            var targetFiles = System.IO.Directory.GetFiles(iTargetDirectory, "*.*", System.IO.SearchOption.AllDirectories).Enum().ToList();

            var sourceFilesHash = new List<string>();
            var targetFilesHash = new List<string>();
            
            foreach(var filePathItem in sourceFiles.Enum())
            {
                var fileInfo = new FileInfo(filePathItem);
                sourceFilesHash.Add(fileInfo.Name + CalculateMD5(filePathItem));
            }

            foreach (var filePathItem in targetFiles.Enum())
            {
                var fileInfo = new FileInfo(filePathItem);
                targetFilesHash.Add(fileInfo.Name + CalculateMD5(filePathItem));
            }

            var comparaisonHash = new Library.Tools.Comparator.ListComparator<string, string>(sourceFilesHash, x => x, targetFilesHash, x => x);

            if (comparaisonHash.NewList.Count != 0)
                return false;
            if (comparaisonHash.RemovedList.Count != 0)
                return false;

            return true;
        }

        private static string CalculateMD5(string iFilePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(iFilePath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }


        #endregion

        #region Private METHODS

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary>
        /// Retourne un chemin d'un dossier temporaire qui n'existe pas, sans le créer
        /// </summary>
        /// <param name="iExtension"></param>
        /// <returns></returns>
        public static string GenerateTempDirectoryPath()
        { 
            var directoryPath = System.IO.Path.GetTempPath() + Guid.NewGuid() + @"\";

            if (System.IO.Directory.Exists(directoryPath))
                throw new Exception("Le chemin de dossier sensé être unique, ne l'est pas");

            return directoryPath;
        }

        #endregion
    }
}