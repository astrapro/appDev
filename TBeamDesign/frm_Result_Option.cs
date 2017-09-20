using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace BridgeAnalysisDesign
{
    public partial class frm_Result_Option : Form
    {
        public enum eResult_Option
        {
            Full_Analysis_Report = 0,
            Total_Load_Analysis_Report = 1,
            Live_Load_Analysis_Report = 2,
            Dead_Load_Analysis_Report = 3,
            Analysis_Result = 4,
        }

        bool IsNewFormat = true;
        public frm_Result_Option(bool is_new_format)
        {
            InitializeComponent();
            IsNewFormat = is_new_format;

            grb_1.Visible = !is_new_format;
            grb_2.Visible = is_new_format;
        }
        public eResult_Option ResultOption
        {
            get
            {
                if (IsNewFormat)
                {
                    if (rbtn_ana_res.Checked) return eResult_Option.Analysis_Result;
                    else if (rbtn_dead_load.Checked) return eResult_Option.Dead_Load_Analysis_Report;
                    else if (rbtn_live_load.Checked) return eResult_Option.Live_Load_Analysis_Report;
                    else if (rbtn_total_load.Checked) return eResult_Option.Total_Load_Analysis_Report;
                }
                else
                {
                    if (rbtn_Analysis_result.Checked) return eResult_Option.Analysis_Result;
                    else if (rbtn_Full_Analysis_report.Checked) return eResult_Option.Full_Analysis_Report;
                }

                return eResult_Option.Analysis_Result;
            }
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
        private void frm_Result_Option_Load(object sender, EventArgs e)
        {

        }
    }
}
