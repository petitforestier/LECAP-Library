using Library.Tools.Extensions;
using Library.Tools.Tasks;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Library.Tools.Debug
{
    public static class MyDebug
    {
        #region Public METHODS

        /// <summary>
        /// A Appeler pour faire une fonction fictive ou le break point est apposable
        /// </summary>
        public static void BreakForDebug()
        {
        }

        /// <summary>
        /// Ecrire ligne [Error] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintError(string iMessage)
        {
            string fullMessage = string.Format("[Error] {0}  : {1}", Time.MyTime.GetParisDateTimeNow(), iMessage);
            Trace.WriteLine(fullMessage);
        }

        /// <summary>
        /// Ecrire ligne [Error] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintAsError(this string iMessage)
        {
            string fullMessage = string.Format("[Error] {0}  : {1}", Time.MyTime.GetParisDateTimeNow(), iMessage);
            Trace.WriteLine(fullMessage);
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintError(string iMessage, Exception iEx)
        {
            string message = string.Format("[Error]  {0} {1}: {2}", Time.MyTime.GetParisDateTimeNow(), iMessage, iEx.GetMessage());
            Trace.WriteLine(message);
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintAsError(this string iMessage, Exception iEx)
        {
            string message = string.Format("[Error]  {0} {1}: {2}", Time.MyTime.GetParisDateTimeNow(), iMessage, iEx.GetMessage());
            Trace.WriteLine(message);
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintInformation(NotifierProgress iProgress)
        {
            if (iProgress == null) throw new ArgumentNullException();
            Trace.WriteLine(iProgress.FullMessage);
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintInformation(string iMessage)
        {
            PrintInformation(new NotifierProgress() { Message = iMessage });
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintAsInformation(this string iMessage)
        {
            PrintInformation(new NotifierProgress() { Message = iMessage });
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintInformation(string iMessage, int iProgressCount, int iProgressCounter)
        {
            PrintInformation(new NotifierProgress() { Message = iMessage, LevelProgressCount = iProgressCount, LevelProgressCounter = iProgressCounter });
        }

        /// <summary>
        /// Ecrire ligne [info] log dans le fichier de log et sortie du debugguer
        /// </summary>
        public static void PrintAsInformation(this string iMessage, int iProgressCount, int iProgressCounter)
        {
            PrintInformation(new NotifierProgress() { Message = iMessage, LevelProgressCount = iProgressCount, LevelProgressCounter = iProgressCounter });
        }

        /// <summary>
        /// Ecrire dans le debugguer la mise à plat d'un treenode
        /// </summary>
        public static void PrintTreeNodeText(TreeNode iNode, int iLevel = 1)
        {
            string text = "";
            for (int i = 1; i <= iLevel; i++)
                text = text + "   ";

            MyDebug.PrintInformation(text + iNode.Text);

            foreach (TreeNode nodeItem in iNode.Nodes)
                PrintTreeNodeText(nodeItem, iLevel + 1);
        }

        #endregion
    }
}