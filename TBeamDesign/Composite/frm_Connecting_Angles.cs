using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstraInterface.DataStructure;
using AstraInterface.Interface;

namespace BridgeAnalysisDesign.Composite
{
    public partial class frm_Connecting_Angles : Form
    {
        IApplication iApp;
        public frm_Connecting_Angles(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        public RolledSteelAnglesRow Angle_Section { get; set; }
 
        private void cmb_L_2_ang_section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
           
            if (cmb.Name == cmb_ana_ang_section_name.Name)
            {
                cmb_ana_ang_section_code.Items.Clear();
                if (cmb_ana_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_ana_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_ana_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_ana_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_ana_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_ana_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_ana_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_ana_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_ana_ang_section_code.Items.Count > 0)
                {
                    cmb_ana_ang_section_code.SelectedIndex = 0;
                    cmb_ana_ang_section_code.SelectedItem = "100X100";
                    cmb_ana_ang_thk.SelectedIndex = cmb_ana_ang_thk.Items.Contains(10.0) ? cmb_ana_ang_thk.Items.IndexOf(10.0) : 0;
                    //cmb_Deff_nos_ang.SelectedIndex = 1;
                }
            }

        }
        private void cmb_ang_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
           
            TableRolledSteelAngles tbl_rolledSteelAngles;
             if (cmb.Name == cmb_ana_ang_section_code.Name)
            {
                tbl_rolledSteelAngles = cmb_ana_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;
                if (tbl_rolledSteelAngles.List_Table.Count > 0)
                {
                    cmb_ana_ang_thk.Items.Clear();
                    for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        if (tbl_rolledSteelAngles.List_Table[i].SectionSize == cmb_ana_ang_section_code.Text)
                        {
                            if (cmb_ana_ang_thk.Items.Contains(tbl_rolledSteelAngles.List_Table[i].Thickness) == false)
                                cmb_ana_ang_thk.Items.Add(tbl_rolledSteelAngles.List_Table[i].Thickness);
                        }
                    }
                }
                cmb_ana_ang_thk.SelectedIndex = cmb_ana_ang_thk.Items.Count > 0 ? 0 : -1;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            double thk = MyList.StringToDouble(cmb_ana_ang_thk.Text, 10.0);

            Angle_Section = iApp.Tables.Get_AngleData_FromTable(cmb_ana_ang_section_name.Text, cmb_ana_ang_section_code.Text, thk);

            this.Close();
        }

        private void frm_Connecting_Angles_Load(object sender, EventArgs e)
        {
            cmb_ana_nos_ang.SelectedIndex = 0;
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, false);
            }
            else
            {
                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, false);
            }

            cmb_ana_ang_section_name.SelectedIndex = 0;

        }

    }
}
