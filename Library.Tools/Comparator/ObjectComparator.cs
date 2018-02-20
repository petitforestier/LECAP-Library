namespace Library.Tools.Comparator
{
    using KellermanSoftware.CompareNetObjects;
    using System.Collections.Generic;
    using System;
    using Library.Tools.Debug;

    public class ObjectComparator
    {
        #region Public METHODS

        /// <summary>
        /// Retourne si deux objets sont identiques en comparant leurs propriétés. iOnlyPrimitiveProperties permet d'ignorer les propriétés objet, list etc...
        /// </summary>
        public bool AreEqual(object iObject1, object iObject2, bool iOnlyPrimitiveProperties, bool iPrintDifference = false)
        {
            var compareLogic = new CompareLogic();

            compareLogic.Config.CompareFields = false;
            compareLogic.Config.ComparePrivateFields = false;
            compareLogic.Config.ComparePrivateProperties = false;
            compareLogic.Config.CompareProperties = true;       

            if (iOnlyPrimitiveProperties)
                compareLogic.Config.CompareChildren = false;

            if (iPrintDifference)
                MyDebug.PrintInformation(compareLogic.Compare(iObject1, iObject2).DifferencesString);

            return compareLogic.Compare(iObject1, iObject2).AreEqual;
        }

        #endregion Public METHODS
    }
}