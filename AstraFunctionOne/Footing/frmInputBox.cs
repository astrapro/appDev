using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;
namespace AstraFunctionOne.Footing
{
    public partial class frmInputBox : Form
    {
        public frmInputBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OK_Click();
        }

        private void OK_Click()
        {
            if (RequiredBaseLength < MinimumLength)
            {
                if (txt_Req_Base_Len.Text == "")
                {
                    MessageBox.Show(this, "PLEASE ENTER REQUIRED BASE LENGTH", "ASTRA", MessageBoxButtons.OK);
                    //MessageBox.Show(this, "Please Enter Required Base Length", "ASTRA", MessageBoxButtons.OK);
                    txt_Req_Base_Len.Focus();
                }
                else
                {
                    MessageBox.Show(this, "CHOSEN LENGTH TOO SMALL", "ASTRA", MessageBoxButtons.OK);
                    txt_Req_Base_Len.Focus();
                }
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public double MinimumLength
        {
            get
            {
                return MyList.StringToDouble(txt_Min_Base_Len.Text, 0.0);
            }
            set
            {
                txt_Min_Base_Len.Text = value.ToString("0.000");
            }
        }
        public double CompressionOver
        {
            get
            {
                return MyList.StringToDouble(txt_Com_Over.Text, 0.0);
            }
            set
            {
                txt_Com_Over.Text = value.ToString();
            }
        }
        public double RequiredBaseLength
        {
            get
            {
                return MyList.StringToDouble(txt_Req_Base_Len.Text, 0.0);
            }
            set
            {
                txt_Req_Base_Len.Text = value.ToString();
            }
        }

        private void txt_Req_Base_Len_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OK_Click();
            }
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {

        }
    }
}
