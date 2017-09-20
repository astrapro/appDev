using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;

namespace AstraFunctionOne
{
    public partial class frm_Drawing_Options : Form
    {
        eOpenDrawingOption options { get; set; }
        public frm_Drawing_Options()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (rbtn_design_drawings.Checked)
                options = eOpenDrawingOption.Design_Drawings;
            else
                options = eOpenDrawingOption.Sample_Drawings;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            options = eOpenDrawingOption.Cancel;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
        public static eOpenDrawingOption Drawing_Option()
        {
            frm_Drawing_Options frm = new frm_Drawing_Options();
            frm.ShowDialog();
            return frm.options;
        }
    }

}
