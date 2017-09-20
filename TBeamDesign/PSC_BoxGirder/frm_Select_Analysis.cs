using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_Select_Analysis : Form
    {
        public frm_Select_Analysis()
        {
            InitializeComponent();
        }
        public Select_Analysis_Type AnalysisType;
        private void btn_process_Click(object sender, EventArgs e)
        {
            AnalysisType.Analysis_DL_SIDL_3_CONTINUOUS_SPANS = chk_2.Checked;
            AnalysisType.Analysis_DL_SIDL_END_SPANS = chk_3.Checked;
            AnalysisType.Analysis_DL_SIDL_MID_SPAN = chk_4.Checked;
            AnalysisType.Analysis_DS_3_CONTINUOUS_SPANS = chk_5.Checked;
            AnalysisType.Analysis_FPLL_3_CONTINUOUS_SPANS = chk_6.Checked;
            AnalysisType.Analysis_LL_3_CONTINUOUS_SPANS = chk_7.Checked;

            Button btn = sender as Button;

            if (btn.Name == btn_process.Name)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
        
        
        }

        private void chk_1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.Name == chk_1.Name)
            {
                chk_2.Checked = chk_1.Checked;
                chk_3.Checked = chk_1.Checked;
                chk_4.Checked = chk_1.Checked;
                chk_5.Checked = chk_1.Checked;
                chk_6.Checked = chk_1.Checked;
                chk_7.Checked = chk_1.Checked;
            }
        }
    }

    public struct Select_Analysis_Type
    {
        public bool Analysis_DL_SIDL_3_CONTINUOUS_SPANS;
        public bool Analysis_DL_SIDL_END_SPANS;
        public bool Analysis_DL_SIDL_MID_SPAN;
        public bool Analysis_DS_3_CONTINUOUS_SPANS;
        public bool Analysis_FPLL_3_CONTINUOUS_SPANS;
        public bool Analysis_LL_3_CONTINUOUS_SPANS;
    }

}
