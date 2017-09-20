using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LimitStateMethod.Steel_Truss
{
    public partial class frm_Arch_Input_Files : Form
    {
        public static int Indx = 0;
        public frm_Arch_Input_Files()
        {
            InitializeComponent();
        }

        private void frm_Arch_Input_Files_Load(object sender, EventArgs e)
        {
            Select_File_Index = Indx;
        }
        public int Select_File_Index
        {
            get
            {
                if (rbtn_1.Checked) return 0;
                if (rbtn_2.Checked) return 1;
                if (rbtn_3.Checked) return 2;
                if (rbtn_4.Checked) return 3;
                if (rbtn_5.Checked) return 4;

                return -1;
            }
            set
            {
                rbtn_1.Checked = (value == 0);
                rbtn_2.Checked = (value == 1);
                rbtn_3.Checked = (value == 2);
                rbtn_4.Checked = (value == 3);
                rbtn_5.Checked = (value == 4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Indx = Select_File_Index;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
