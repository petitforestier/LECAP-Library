namespace Library.Tools.Comparator
{
    using Library.Tools.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ListComparator<TOriginal, TNew>
    {
        #region Public PROPERTIES

        /// <summary>
        /// Obtient la liste des objects nouveaux en commun
        /// </summary>
        public List<TNew> CommonList { get; private set; }

        /// <summary>
        /// Obtient la liste de pair avec l'original en key et le nouveau en value
        /// </summary>
        public List<KeyValuePair<TOriginal, TNew>> CommonPairList { get; private set; }

        /// <summary>
        /// Obtient la liste des nouveaux objects
        /// </summary>
        public List<TNew> NewList { get; private set; }

        /// <summary>
        /// Obtient la liste des nouveaux supprimés
        /// </summary>
        public List<TOriginal> RemovedList { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        /// <summary>
        /// Retourne via les propriétés de la classe, les objects nouveaux, communs et supprimés.
        /// </summary>
        public ListComparator(IEnumerable<TOriginal> iOriginalList, Func<TOriginal, IComparable> iOriginalKey, IEnumerable<TNew> iNewList, Func<TNew, IComparable> iNewKey)
        {
            if (iOriginalKey == null ||
                iNewKey == null) { throw new ArgumentNullException(); };

            if (default(TOriginal) != null || default(TNew) != null)
                throw new Exception("Il n'est pas possible de comparer une liste de type non nullable, Veuillez transformer la liste pour effectuer le comparator");

            if (iOriginalList.IsNullOrEmpty())
            {
                NewList = (iNewList != null) ? iNewList.ToList() : null;
                return;
            }

            if (iNewList.IsNullOrEmpty())
            {
                RemovedList = (iOriginalList != null) ? iOriginalList.ToList() : null;
                return;
            }

            //Vérifier si la liste possède des doublons sur les clé à comparer
            if (iOriginalList.GetDuplicates(iOriginalKey, false).Count() !=0)
                throw new Exception("La liste originale possède des doublons sur la clé");

            //Vérifier si la liste possède des doublons sur les clé à comparer
            if (iNewList.GetDuplicates(iNewKey, false).Count() != 0)
                throw new Exception("La liste nouvelle possède des doublons sur la clé");


            NewList = (from tnew in iNewList.ToList()
                       join toriginal in iOriginalList.ToList()
                       on iNewKey(tnew) != null ? iNewKey(tnew).ToString() : null
                       equals iOriginalKey(toriginal) != null ? iOriginalKey(toriginal).ToString() : null into gj
                       from item in gj.DefaultIfEmpty()
                       where item == null
                       select tnew).ToList();

            CommonList = (from tnew in iNewList.ToList()
                          join toriginal in iOriginalList.ToList()
                          on iNewKey(tnew) != null ? iNewKey(tnew).ToString() : null
                          equals iOriginalKey(toriginal) != null ? iOriginalKey(toriginal).ToString() : null into gj
                          from item in gj.DefaultIfEmpty()
                          where item != null
                          select tnew).ToList();

            CommonPairList = (from tnew in iNewList.ToList()
                              join toriginal in iOriginalList.ToList()
                              on iNewKey(tnew) != null ? iNewKey(tnew).ToString() : null
                              equals iOriginalKey(toriginal) != null ? iOriginalKey(toriginal).ToString() : null into gj
                              from item in gj.DefaultIfEmpty()
                              where item != null
                              select new KeyValuePair<TOriginal, TNew>(item, tnew)).ToList();

            RemovedList = (from toriginal in iOriginalList.ToList()
                           join tnew in iNewList.ToList()
                           on iOriginalKey(toriginal) != null ? iOriginalKey(toriginal).ToString() : null
                           equals iNewKey(tnew) != null ? iNewKey(tnew).ToString() : null into gj
                           from item in gj.DefaultIfEmpty()
                           where item == null
                           select toriginal).ToList();
        }

        #endregion

        #region Public METHODS

        /// <summary>
        /// Retourne true si toutes les clés sont en commun
        /// </summary>
        /// <returns></returns>
        public bool IsOnlyCommon()
        {
            if (NewList.Count() == 0 && RemovedList.Count() == 0)
                return true;

            return false;
        }

        /// <summary>
        /// Retourne true si toutes les clés ne sont pas en commun
        /// </summary>
        /// <returns></returns>
        public bool IsNotOnlyCommon()
        {
            return !IsOnlyCommon();
        }

        #endregion
    }
}