using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_Cont_Open_File : Form
    {
        public frm_Cont_Open_File()
        {
            InitializeComponent();
        }
        public int SelectedIndex
        {
            get
            {
                if (rbtn_1.Checked) return 1;
                if (rbtn_2.Checked) return 2;
                if (rbtn_3.Checked) return 3;
                if (rbtn_4.Checked) return 4;
                if (rbtn_5.Checked) return 5;
                if (rbtn_6.Checked) return 6;
                return -1;
            }
        }
        private void btn_open_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_open.Name)
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
