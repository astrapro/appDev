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


namespace LimitStateMethod.Composite
{
    public partial class UC_Composite_Orthotropic : UserControl
    {
        public event EventHandler OnButtonClick;
        public event EventHandler OnComboboxSelectedIndexChanged;
        IApplication iApp;

        public UC_Composite_Orthotropic()
        {
            InitializeComponent();
            Support_Changed();
        }

        private void rbtn_ssprt_pinned_CheckedChanged(object sender, EventArgs e)
        {
            Support_Changed();
        }

        private void Support_Changed()
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

    }
}
