using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public class ucTextBox : TextBox
    {
        #region Public ENUMS

        public enum TextBoxTypeEnum
        {
            None,
            Integer,
            IntegerPositiveOnly,
        }

        #endregion

        #region Public PROPERTIES

        public TextBoxTypeEnum ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                _valueType = value;
            }
        }

        #endregion

        #region Protected METHODS

        protected override void OnEnter(EventArgs e)
        {
            // Kick off SelectAll asyncronously so that it occurs after Click
            BeginInvoke((Action)delegate
            {
                this.SelectAll();
            });
            base.OnEnter(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (_valueType == TextBoxTypeEnum.Integer || _valueType == TextBoxTypeEnum.IntegerPositiveOnly)
            {
                if (e.KeyChar.Equals('.'))
                    e.KeyChar = ',';

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
                    e.Handled = true;

                // only allow one decimal point
                if ((e.KeyChar == ',') && (Text.IndexOf(',') > -1))
                    e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        #endregion

        #region Private FIELDS

        private TextBoxTypeEnum _valueType;

        #endregion
    }
}