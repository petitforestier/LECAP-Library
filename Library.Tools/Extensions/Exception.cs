namespace Library.Tools.Extensions
{
    using System;
    using System.ServiceModel;
    using System.Windows.Forms;

    public static class MyException
    {
        #region Public METHODS

        /// <summary>
        /// Retourne une string avec tous les messages d'erreur dans une seule chaine de caractère
        /// </summary>
        public static string GetMessage(this Exception theException)
        {
            string message = "Erreur interne : " + Environment.NewLine;

            int errorCounter = 1;
            while (theException != null)
            {
                message += Environment.NewLine + "Erreur {0} : ".FormatString(errorCounter.ToString()) + theException.Message;
                errorCounter++;
                theException = theException.InnerException;
            }
            return message;
        }

        /// <summary>
        /// Ouvre une message box avec le message d'erreur
        /// </summary>
        /// <param name="iException"></param>
        public static void ShowInMessageBox(this Exception iException)
        {
            var frmException = new Library.Tools.Exceptions.frmExceptionMessage(iException);
            frmException.TopMost = true;
            frmException.Show();
        }

        #endregion

        #region Private METHODS


        #endregion
    }
}