using Library.Tools.Attributes;
using Library.Tools.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace Library.Control.Datagridview
{
    public static class MyDatagridview
    {
        #region Public METHODS

        /// <summary>
        /// Format les colonnes du datagridview via une classe où les propriétés sont habillées des attributs disponibles
        /// </summary>
        public static void FormatColumns<T>(this System.Windows.Forms.DataGridView iDgv, string iLang)
        {
            if (iDgv.DataSource != null)
            {
                iDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                var dgvColumnHeaderStyle = new DataGridViewCellStyle();
                dgvColumnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                iDgv.ColumnHeadersDefaultCellStyle = dgvColumnHeaderStyle;

                foreach (System.Windows.Forms.DataGridViewColumn itemColumn in iDgv.Columns)
                {
                    itemColumn.Visible = typeof(T).GetVisible(itemColumn.Name);
                    if (itemColumn.Visible == true)
                    {
                        itemColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        itemColumn.DefaultCellStyle.NullValue = null;

                        if (typeof(T).GetName(itemColumn.Name, iLang) != null)
                            itemColumn.HeaderText = typeof(T).GetName(itemColumn.Name, iLang);

                        itemColumn.ReadOnly = (bool)typeof(T).GetReadOnly(itemColumn.Name);

                        itemColumn.Frozen = (bool)typeof(T).GetFrozen(itemColumn.Name);

                        if(typeof(T).GetBackColor(itemColumn.Name) != null)
                            itemColumn.DefaultCellStyle.BackColor = (Color)typeof(T).GetBackColor(itemColumn.Name);

                        if (typeof(T).GetMultiLineText(itemColumn.Name))
                            itemColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    }
                }

                //Autoresize avant la largeur si spécifié pour tenir compte de la largeur de l'entete
                iDgv.AutoResizeColumns();

                foreach (System.Windows.Forms.DataGridViewColumn itemColumn in iDgv.Columns)
                {
                    itemColumn.DefaultCellStyle.Alignment = typeof(T).GetContentAlignment(itemColumn.Name);
                    itemColumn.Visible = typeof(T).GetVisible(itemColumn.Name);

                    if (itemColumn.Visible == true)
                    {
                        if (typeof(T).GetWidthColumn(itemColumn.Name) != null)
                        {
                            var width = (int)typeof(T).GetWidthColumn(itemColumn.Name);
                            if (width != 0)
                                itemColumn.Width = width;
                            else
                                itemColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }         

                        itemColumn.DefaultCellStyle.Alignment = typeof(T).GetContentAlignment(itemColumn.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Obtient un dictionnaire des propriétés triables de la classe demandée (via l'attribut sortable), key = nom de la propriété, value = nom dans la langue
        /// </summary>
        public static Dictionary<string, string> GetIsSortablePropertyDic<T>(this System.Windows.Forms.DataGridView iDgv, string iLangId)
        {
            Dictionary<string, string> resultDic = new Dictionary<string, string>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (typeof(T).GetSortable(property.Name))
                {
                    resultDic.Add(property.Name, typeof(T).GetName(property.Name, iLangId));
                }
            }
            return resultDic;
        }

        /// <summary>
        /// Cache une liste de colonne
        /// </summary>
        public static void HideColumns(this System.Windows.Forms.DataGridView iDgv, List<string> iColumnNameList)
        {
            if (iDgv.DataSource != null)
            {
                foreach (System.Windows.Forms.DataGridViewColumn itemColumn in iDgv.Columns)
                {
                    if (iColumnNameList.Contains(itemColumn.Name))
                        itemColumn.Visible = false; 
                }
            }
        }

        public static void SelectRowAfterDeleteByIndex(this System.Windows.Forms.DataGridView iDgv, int iIndex)
        {
            if (iDgv.RowCount != 0 &&
                iIndex.IsNotNull())
            {
                int index = 0;
                if (iDgv.RowCount == 1)
                    index = 0;
                else if (iIndex == iDgv.RowCount)
                    index = iIndex - 1;
                else
                    index = iIndex;

                iDgv.Rows[index].Selected = true;
                iDgv.CurrentCell = iDgv.Rows[index].Cells[GetFirstCellIndexVisible(iDgv)];
            }
        }

        public static void SelectCellAfterDeleteByIndex(this System.Windows.Forms.DataGridView iDgv, int iIndex)
        {
            if (iDgv.RowCount != 0 &&
                iIndex.IsNotNull())
            {
                int index = 0;
                if (iDgv.RowCount == 1)
                    index = 0;
                else if (iIndex == iDgv.RowCount)
                    index = iIndex - 1;
                else
                    index = iIndex;

                iDgv.Rows[index].Cells[0].Selected = true;
            }
        }

        /// <summary>
        /// Sélectionne les lignes depuis une liste d'index
        /// </summary>
        public static void SelectRowsByIndex(this System.Windows.Forms.DataGridView iDgv, List<int> iSelectedIndex)
        {
            if (iSelectedIndex.IsNullOrEmpty()) return;

            iDgv.ClearSelection();

            foreach (int Item in iSelectedIndex)
            {
                try
                {
                    iDgv.Rows[Item].Selected = true;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Sélectionne une ligne via le numéro d'index
        /// </summary>
        public static void SelectRowByIndex(this System.Windows.Forms.DataGridView iDgv, int iIndex)
        {
            if (iDgv.RowCount != 0 &&
                iIndex.IsNotNull())
            {
                iDgv.Rows[iIndex].Selected = true;
                iDgv.CurrentCell = iDgv.Rows[iIndex].Cells[GetFirstCellIndexVisible(iDgv)];
            }
        }

        ///// <summary>
        ///// Sélectionne une ligne via un objet qui à peuplé le datagridview
        ///// </summary>
        //public static void SelectRowByItem(this System.Windows.Forms.DataGridView iDgv, object iObject)
        //{
        //    if (iDgv.RowCount != 0 &&
        //        iObject != null)
        //    {
        //        CompareLogic compareLogic = new CompareLogic();

        //        foreach (DataGridViewRow rowItem in iDgv.Rows)
        //        {
        //            if (compareLogic.Compare(rowItem.DataBoundItem, iObject).AreEqual)
        //            {
        //                iDgv.Rows[rowItem.Index].Selected = true;
        //                iDgv.CurrentCell = iDgv.Rows[rowItem.Index].Cells[GetFirstCellIndexVisible(iDgv)];
        //                break;
        //            }
        //        }
        //    }
        //}

        public static bool SelectRowByPropertyValue<TRow>(this System.Windows.Forms.DataGridView iDgv, Func<TRow, string> iPropertyKey, string iPropertyValue)
        {
            if (iDgv.RowCount != 0 )
            {
                foreach (DataGridViewRow rowItem in iDgv.Rows)
                {
                    if (iPropertyKey((TRow)rowItem.DataBoundItem) == iPropertyValue)
                    {
                        iDgv.Rows[rowItem.Index].Selected = true;
                        //iDgv.FirstDisplayedScrollingRowIndex = rowItem.Index;
                        iDgv.CurrentCell = iDgv.Rows[rowItem.Index].Cells[GetFirstCellIndexVisible(iDgv)];
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Ordonne une colonne du datagridview
        /// </summary>
        public static void SortByColumnName<T>(this System.Windows.Forms.DataGridView iDgv, string iPropertyName, ListSortDirection iSortDirection)
        {
            if (iSortDirection == ListSortDirection.Ascending)
                iDgv.DataSource = ((List<T>)iDgv.DataSource).OrderBy(x => x.GetType().GetProperty(iPropertyName).GetValue(x, null)).ToList();
            else
                iDgv.DataSource = ((List<T>)iDgv.DataSource).OrderByDescending(x => x.GetType().GetProperty(iPropertyName).GetValue(x, null)).ToList();
        }

        /// <summary>
        /// Retourne la liste des index sélectionnés
        /// </summary>
        public static List<int> GetSelectedRowIndexList(this System.Windows.Forms.DataGridView iDgv)
        {
            var result = new List<int>();
            if (iDgv.SelectedRows.Count != 0)
                foreach (DataGridViewRow item in iDgv.SelectedRows)
                    result.Add(item.Index);

            return result;
        }

        /// <summary>
        /// Défini la première ligne affichée dans le datagridview
        /// </summary>
        /// <param name="iDgv"></param>
        /// <param name="iFirstIndex"></param>
        public static void SetFirstDisplayedScrollingRowIndex(this System.Windows.Forms.DataGridView iDgv, int iFirstIndex)
        {
            if (iDgv.RowCount != 0 && iFirstIndex != -1)
            {
                if (iFirstIndex + 1 > iDgv.RowCount)
                    iDgv.FirstDisplayedScrollingRowIndex = iDgv.RowCount - 1;
                else
                    iDgv.FirstDisplayedScrollingRowIndex = iFirstIndex;
            }
        }

        /// <summary>
        /// Retourne la cellule depuis le nom de l'entête de la colonne
        /// </summary>
        /// <param name="CellCollection"></param>
        /// <param name="HeaderText"></param>
        /// <returns></returns>
        public static object GetCellValueFromColumnHeader(this DataGridViewCellCollection CellCollection, string HeaderText)
        {
            return CellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value;
        }

        #endregion

        #region Private METHODS

        private static int GetFirstCellIndexVisible(System.Windows.Forms.DataGridView iDgv)
        {
            var index = 0;
            foreach (System.Windows.Forms.DataGridViewColumn itemColumn in iDgv.Columns)
            {
                if (itemColumn.Visible)
                    return index;
                index += 1;
            }
            return index;
        }

        #endregion
    }
}