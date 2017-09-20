using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LimitStateMethod.SuspensionBridge
{
    public partial class frm_OpenInput : Form
    {
        public int Option
        {
            get
            {
                if (rbtn_option2.Checked) return 2;
                return 1;
            }
            set
            {
                if (value == 1) rbtn_option2.Checked = true;
                else rbtn_option1.Checked = true;
            }
        }
        public frm_OpenInput()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {


            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
