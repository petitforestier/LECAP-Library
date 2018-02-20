using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.IO
{
    public class Storage
    {
        #region Public METHODS

        /// <summary>
        /// Sauvergarder bytes dans un fichier
        /// </summary>
        /// <param name="iStream"></param>
        /// <param name="iFullPath"></param>
        public static void SaveBytesToFile(byte[] iBytes, string iFullPath)
        {
            using (var fileStream = new System.IO.FileStream(iFullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                fileStream.Write(iBytes, 0, iBytes.Length);
                fileStream.Close();
            }
        }

        /// <summary>
        /// Sauvegarder bytes dans le dossier temporaire avec un guid comme nom de fichier
        /// </summary>
        /// <param name="iBytes"></param>
        /// <param name="iExtension"></param>
        /// <returns></returns>
        public static string SaveBytesToTempFile(byte[] iBytes, string iExtension)
        {
            if (iExtension.IsNullOrEmpty()) throw new ArgumentException("L'extension ne doit pas être vide ou nulle");
            if (iExtension.Contains(".")) throw new ArgumentException("L'extension ne doit pas contenir de point");
            if (iBytes.IsNullOrEmpty()) throw new ArgumentException("Les données en bytes sont nulles");

            var fullPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + "." + iExtension;
            SaveBytesToFile(iBytes, fullPath);
            return fullPath;
        }

        /// <summary>
        /// Sauvegarde l'image dans un fichier
        /// </summary>
        public static bool SaveImageToFile(Image iImage, string iOutputFullPathFileName)
        {
            iOutputFullPathFileName = Path.ChangeExtension(iOutputFullPathFileName, iImage.GetFormat().ToString());

            try
            {
                iImage.Save(iOutputFullPathFileName, iImage.GetFormat());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sauvegarde l'image dans le dossier Async
        /// </summary>
        /// <param name="iImage"></param>
        /// <param name="iOutputFullPathFileName"></param>
        /// <returns></returns>
        public static async Task<bool> SaveImageToFileAsync(Image iImage, string iOutputFullPathFileName)
        {
            return await Task.Run(() => SaveImageToFile(iImage, iOutputFullPathFileName));
        }

        /// <summary>
        /// Obtient l'image dans le dossier
        /// </summary>
        public static Image GetImageFromFile(Uri iFullPathFileName)
        {
            try
            {
                return Image.FromFile(iFullPathFileName.LocalPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sauvegarde l'image dans le dossier Async
        /// </summary>
        /// <param name="iImage"></param>
        /// <param name="iOutputFullPathFileName"></param>
        /// <returns></returns>
        public static async Task<Image> GetImageFromFileAsync(Uri iFullPathFileName)
        {
            return await Task.Run(() => GetImageFromFile(iFullPathFileName));
        }

        /// <summary>
        /// Retourne un treenode de l'arborescence de dossier
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static TreeNode GetTreeFromDirectory(string iPath)
        {
            var rootNode = new TreeNode(iPath);

            DirectoryInfo directoryInfo = new DirectoryInfo(iPath);
            if (directoryInfo.Exists)
                BuildTree(directoryInfo, rootNode.Nodes);

            return rootNode;
        }

        #endregion

        #region Private METHODS

        private static void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            try
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                    curNode.Nodes.Add(file.FullName, file.Name);
            }
            catch { }

            try
            {
                foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
                    BuildTree(subdir, curNode.Nodes);
            }
            catch { }
        }

        #endregion
    }
}