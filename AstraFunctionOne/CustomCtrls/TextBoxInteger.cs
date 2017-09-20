using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace AstraFunctionOne.CustomCtrls
{
    internal class TextBoxInteger : TextBox
    {
        int iMaxValue = int.MaxValue;
        int iMinValue = int.MinValue;
        string strFormat = "";
        int iInitialValue = 0;
        public TextBoxInteger()
            : base()
        {

        }
        #region Extended Properties
        public int MaximumValue
        {
            get
            {
                return this.iMaxValue;
            }
            set
            {
                this.iMaxValue = value;
            }
        }
        public int MinimumValue
        {
            get
            {
                return this.iMinValue;
            }
            set
            {
                this.iMinValue = value;
            }
        }
        public string MaskFormat
        {
            set
            {
                this.strFormat = value;
            }
            get
            {
                return this.strFormat;
            }
        }
        public int Value
        {
            get
            {
                int iVal = iInitialValue;
                if (!int.TryParse(base.Text, out iVal))
                {
                    iVal = iInitialValue;
                }
                return iVal;
            }
            set
            {
                base.Text = value.ToString(this.MaskFormat);
            }
        }
        #endregion
        protected override void OnLostFocus(EventArgs e)
        {
            ValidateText();
            base.OnLostFocus(e);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            iInitialValue = this.Value;
            base.OnGotFocus(e);
        }

        private void ValidateText()
        {
            int dValue = 0;
            if (!int.TryParse(base.Text, out dValue))
            {
                this.Value = iInitialValue;
            }
            else
            {
                if (dValue > this.MaximumValue) this.Value = this.MaximumValue;
                if (dValue < this.MinimumValue) this.Value = this.MinimumValue;
            }
        }
    }
}
