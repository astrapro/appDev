using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace AstraFunctionOne.CustomCtrls
{
    internal class TextBoxDouble : TextBox
    {
        double dMaxValue = double.MaxValue;
        double dMinValue = double.MinValue;
        string strFormat = "";
        double dInitialValue = 0;
        public TextBoxDouble()
            : base()
        {

        }
        #region Extended Properties
        public double MaximumValue
        {
            get
            {
                return this.dMaxValue;
            }
            set
            {
                this.dMaxValue = value;
            }
        }
        public double MinimumValue
        {
            get
            {
                return this.dMinValue;
            }
            set
            {
                this.dMinValue = value;
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
        public double Value
        {
            get
            {
                double dVal = dInitialValue;
                if (!double.TryParse(base.Text, out dVal))
                {
                    dVal = dInitialValue;
                }
                return dVal;
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
            dInitialValue = this.Value;
            base.OnGotFocus(e);
        }

        private void ValidateText()
        {
            double dValue = 0;
            if (!double.TryParse(base.Text, out dValue))
            {
                this.Value = dInitialValue;
            }
            else
            {
                if (dValue > this.MaximumValue) this.Value = this.MaximumValue;
                if (dValue < this.MinimumValue) this.Value = this.MinimumValue;
            }
        }
    }
}
