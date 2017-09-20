using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.CableStayed
{
    public partial class frm_CSB_Result_option : Form
    {
        public frm_CSB_Result_option()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public bool Is_Full_Analysis_Report
        {
            get
            {
                return rbtn_Full_Analysis_report.Checked;
            }
        }
        public bool Is_Analysis_Result
        {
            get
            {
                return rbtn_Analysis_result.Checked;
            }
        }
        public bool Is_Cable_Analysis_Result
        {
            get
            {
                return rbtn_cbl_Analysis_result.Checked;
            }
        }
    }
}
