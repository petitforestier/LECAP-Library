using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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