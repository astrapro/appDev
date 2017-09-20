using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne.BridgeDesign
{
    public partial class frmCurve : Form
    {
        double d_m1, d_m2,d_k;

        public frmCurve(double k, double u_B, double v_L, LoadType ltype)
        {
            InitializeComponent();
            txt_k.Text = k.ToString("0.0");
            k = double.Parse(txt_k.Text);
            txt_u_b.Text = u_B.ToString("0.0");
            txt_v_L.Text = v_L.ToString("0.0");
            if (ltype == LoadType.FullyLoad)
            {
                pbCurve.Image = AstraFunctionOne.Properties.Resources.Fully_Loaded;
                if (txt_k.Text == "0.6")
                {
                    txt_m1.Text = "0.049";
                    txt_m2.Text = "0.015";
                }
                lblChangeText.Text = "1 / k";
                txt_u_b.Text = "" + (1 / k).ToString("0.00");
            }
            else
            {

                //pbCurve.Image = AstraFunctionOne.Properties.Resources.F;
                lblChangeText.Text = "u / B";
                //txt_u_b.Text = "" + (1 / k).ToString("0.00");
                Selected_Image(k);
                if (txt_k.Text == "0.6")
                {
                    txt_m1.Text = "0.081";
                    txt_m2.Text = "0.022";
                }
            }
        }

        private void frmCurve_Load(object sender, EventArgs e)
        {
           //Selected_Image(k);
        }
        public double m1
        {
            get
            {
                if (!double.TryParse(txt_m1.Text, out d_m1)) d_m1 = 0.0;
                return d_m1;
            }
            set
            {
                d_m1 = value;
                txt_m1.Text = d_m1.ToString();
            }
        }
        public double m2
        {
            get
            {
                if (!double.TryParse(txt_m2.Text, out d_m2)) d_m2 = 0.0;
                return d_m2;
            }
            set
            {
                d_m2 = value;
                txt_m2.Text = d_m2.ToString();
            }
        }
        public double k
        {
            get
            {
                return d_k;
            }
            set
            {
                d_k = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Selected_Image(double k)
        {
            int index = (int)((k <= 1.0) ? (k * 10) : k);
            switch (index)
            {
                case -1:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.Fully_Loaded;
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_0_4;
                    break;
                case 5:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_0_5;
                    break;
                case 6:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_0_6;
                    break;
                case 7:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_0_7;
                    break;
                case 8:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_0_8;
                    break;
                case 9:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_0_9;
                    break;
                case 10:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_1_0;
                    break;
                default:
                    pbCurve.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.K_1_0;
                    break;
            }

        }

        private void txt_m1_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            double val = 0.0;
            if (!double.TryParse(txt.Text, out val))
            {
                errorProvider1.SetError(txt, "wrong input! text not allow");
            }
            else
            {
                if (val < 0.0 || val > 0.1)
                {
                    errorProvider1.SetError(txt, "wrong input! value must be >= 0.0 and <= 0.1");
                }
                else
                {
                    errorProvider1.SetError(txt, "");
                }
            }
        }

        private void txt_m1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (((TextBox)(sender)).Name == txt_m1.Name)
                    txt_m2.Focus();
                if (((TextBox)(sender)).Name == txt_m2.Name)
                    btnOK.Focus();
            }

        }
    }
    public enum LoadType
    {
        FullyLoad = 0,
        PartialLoad = 1
    }
}
