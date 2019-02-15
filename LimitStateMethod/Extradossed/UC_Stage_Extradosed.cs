using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.DataStructure;
using AstraFunctionOne;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.Interface;

using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;

using BridgeAnalysisDesign;
using LimitStateMethod.LS_Progress;
using LimitStateMethod.Bearing;

namespace LimitStateMethod.Extradossed
{
    public partial class UC_Stage_Extradosed : UserControl
    {
        public event EventHandler OnButtonClick;
        public event EventHandler OnComboboxSelectedIndexChanged;
        public event EventHandler OnTextBoxTextChanged;
        public event EventHandler OnEmodTextChanged;
        IApplication iApp;

        public UC_Stage_Extradosed()
        {
            InitializeComponent();
            SupportChanged();
        }
        public string Start_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (rbtn_ssprt_pinned.Checked)
                    kStr = "PINNED";
                else if (rbtn_ssprt_fixed.Checked)
                {
                    kStr = "FIXED";


                    if (chk_ssprt_fixed_FX.Checked
                        || chk_ssprt_fixed_FY.Checked
                        || chk_ssprt_fixed_FZ.Checked
                        || chk_ssprt_fixed_MX.Checked
                        || chk_ssprt_fixed_MY.Checked
                        || chk_ssprt_fixed_MZ.Checked)
                        kStr += " BUT";

                    if (chk_ssprt_fixed_FX.Checked) kStr += " FX";
                    if (chk_ssprt_fixed_FY.Checked) kStr += " FY";
                    if (chk_ssprt_fixed_FZ.Checked) kStr += " FZ";
                    if (chk_ssprt_fixed_MX.Checked) kStr += " MX";
                    if (chk_ssprt_fixed_MY.Checked) kStr += " MY";
                    if (chk_ssprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
        }
        public string END_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (rbtn_esprt_pinned.Checked)
                    kStr = "PINNED";
                else if (rbtn_esprt_fixed.Checked)
                {
                    kStr = "FIXED";
                    if (chk_esprt_fixed_FX.Checked
                        || chk_esprt_fixed_FY.Checked
                        || chk_esprt_fixed_FZ.Checked
                        || chk_esprt_fixed_MX.Checked
                        || chk_esprt_fixed_MY.Checked
                        || chk_esprt_fixed_MZ.Checked)
                        kStr += " BUT";
                    if (chk_esprt_fixed_FX.Checked) kStr += " FX";
                    if (chk_esprt_fixed_FY.Checked) kStr += " FY";
                    if (chk_esprt_fixed_FZ.Checked) kStr += " FZ";
                    if (chk_esprt_fixed_MX.Checked) kStr += " MX";
                    if (chk_esprt_fixed_MY.Checked) kStr += " MY";
                    if (chk_esprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
        }

        private void rbtn_ssprt_pinned_CheckedChanged(object sender, EventArgs e)
        {
            SupportChanged();
        }

        private void SupportChanged()
        {
            chk_esprt_fixed_FX.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_FY.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_FZ.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MX.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MY.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MZ.Enabled = rbtn_esprt_fixed.Checked;
            chk_ssprt_fixed_FX.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_FY.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_FZ.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MX.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MY.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MZ.Enabled = rbtn_ssprt_fixed.Checked;
        }
        private void btn_create_data_Click(object sender, EventArgs e)
        {
            if (OnButtonClick != null)
            {
                OnButtonClick(sender, e);
            }
        }
        private void cmb_long_open_file_process_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnComboboxSelectedIndexChanged != null)
            {
                OnComboboxSelectedIndexChanged(sender, e);
            }
        }

        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_steel_prct_TextChanged(object sender, EventArgs e)
        {
            if (OnEmodTextChanged != null)
                OnEmodTextChanged(sender, e);
            
        }
    }
}
