using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;
namespace AstraFunctionOne.BridgeDesign.Piers
{
    public partial class frmUserInput : Form
    {
        public frmUserInput()
        {
            InitializeComponent();
        }

        private void frmUserInput_Load(object sender, EventArgs e)
        {

        }
        public string SetText1
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        public string SetText2
        {
            get
            {
                return label2.Text;
            }
            set
            {
                label2.Text = value;
            }
        }
        public string SetText3
        {
            get
            {
                return label3.Text;
            }
            set
            {
                label3.Text = value;
            }
        }
        public double InputValue
        {
            get
            {
                return MyList.StringToDouble(textBox1.Text, 0.05);
            }
            set
            {
                textBox1.Text = value.ToString("0.0000");
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
