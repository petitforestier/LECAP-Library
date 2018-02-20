using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Library.Control.Extensions
{
    public static class MyComboBox
    {
        #region Public METHODS

        /// <summary>
        /// Permet de remplir rapidement une combox via un dictionnaire
        /// </summary>
        public static ComboBox FillByDictionary<TKey, TValue>(this ComboBox iComboBox, Dictionary<TKey, TValue> iSourceDic)
        {
            iComboBox.ValueMember = "Key";
            iComboBox.DisplayMember = "Value";
            iComboBox.DataSource = new BindingSource(iSourceDic.OrderBy(a => a.Value).ToList(), null);
            return iComboBox;
        }

        /// <summary>
        /// Permet de remplir rapidement une toolStripcombox via un dictionnaire
        /// </summary>
        public static ToolStripComboBox FillByDictionary<TKey, TValue>(this ToolStripComboBox iComboBox, Dictionary<TKey, TValue> iSourceDic)
        {
            iComboBox.ComboBox.ValueMember = "Key";
            iComboBox.ComboBox.DisplayMember = "Value";
            iComboBox.ComboBox.DataSource = new BindingSource(iSourceDic.OrderBy(a => a.Value).ToList(), null);
            return iComboBox;
        }

        /// <summary>
        /// Dimensionne la zone de texte liste à la largeur du texte.
        /// </summary>
        public static void DropDownAutoWidth(this ComboBox iComboBox)
        {
            iComboBox.DropDownWidth = DropDownWidth(iComboBox);
        }

        #endregion

        #region Private METHODS

        private static int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();

            foreach (var obj in myCombo.Items)
            {
                label1.Text = obj.ToString();
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            label1.Dispose();
            return maxWidth;
        }

        #endregion
    }
}