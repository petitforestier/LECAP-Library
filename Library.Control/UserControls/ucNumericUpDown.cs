using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Control.UserControls
{
    public class ucNumericUpDown : NumericUpDown
    {
        #region Public ENUMS

        public enum NumericUpDownTypeEnum
        {
            None,
            Percentage,
            Integer,
            IntegerPositiveOnly,
            Price,
            PricePositiveOnly
        }

        #endregion

        #region Public PROPERTIES

        public NumericUpDownTypeEnum ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                _valueType = value;

                this.ThousandsSeparator = true;

                if (value == NumericUpDownTypeEnum.None)
                {
                }
                else if (value == NumericUpDownTypeEnum.Integer)
                {
                    this.Minimum = -1000000;
                    this.Maximum = 1000000;
                    this.DecimalPlaces = 0;
                    this.Increment = 1;
                }
                else if (value == NumericUpDownTypeEnum.IntegerPositiveOnly)
                {
                    this.Minimum = 0;
                    this.Maximum = 1000000;
                    this.DecimalPlaces = 0;
                    this.Increment = 1;
                }
                else if (value == NumericUpDownTypeEnum.Percentage)
                {
                    this.Minimum = 0;
                    this.Maximum = 100;
                    this.DecimalPlaces = 2;
                    this.Increment = 1;
                }
                else if (value == NumericUpDownTypeEnum.Price)
                {
                    this.Minimum = -1000000;
                    this.Maximum = 1000000;
                    this.DecimalPlaces = 2;
                    this.Increment = 1;
                }
                else if (value == NumericUpDownTypeEnum.PricePositiveOnly)
                {
                    this.Minimum = 0;
                    this.Maximum = 1000000;
                    this.DecimalPlaces = 2;
                    this.Increment = 1;
                }
                else
                    throw new NotSupportedException(value.ToStringWithEnumName());
            }
        }

        #endregion

        #region Public METHODS

        public void SelectAll()
        {
            this.Select(0, this.Text.Length);
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
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
                e.KeyChar = ',';

            base.OnKeyPress(e);
        }

        #endregion

        #region Private FIELDS

        private NumericUpDownTypeEnum _valueType;

        #endregion
    }
}