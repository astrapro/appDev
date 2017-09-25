using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
//using AstraFunctionOne.BridgeDesign.SteelTrussTables;

namespace AstraFunctionOne.BridgeDesign.SteelTruss
{
    public partial class frmStreelTrussMemberDesign : Form
    {
        IApplication iApp = null;
        TableRolledSteelBeams tbl_rolledSteelBeams = null;
        TableRolledSteelChannels tbl_rolledSteelChannels = null;
        TableRolledSteelAngles tbl_rolledSteelAngles = null;
        double DL, LL, IL, h, l, fy, fc, ft;
        double sigma_b, sigma_c;
        double a = 12;



        double top_chord_mf, top_chord_c, top_chord_RI, top_chord_phi_1, top_chord_dr, top_chord_S;

        double top_plate_width;
        double top_plate_thickness;
        double bottom_plate_width;
        double bottom_plate_thickness;
        double side_plate_width;
        double side_plate_thickness;
        double vertical_stiff_plate_width;
        double vertical_stiff_plate_thickness;

        #region User file Handling Variables
        string rep_tm_file = "";//Top Chord Member
        string rep_dm_file = "";//Diagonal Member
        string rep_vm_file = "";//Vertical Member
        string rep_bm_file = "";//Bottom Member
        string rep_sb_file = "";//Stringer Beam
        string rep_cg_file = "";//Cross Girder

        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";
        bool is_process = false;
        #endregion
        string ref_string = "";
        public frmStreelTrussMemberDesign(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
            string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");
            if (Directory.Exists(tab_path))
            {
                
                //tbl_rolledSteelBeams = new TableRolledSteelBeams(tab_path, eDesignStandard.IndianStandard);
                //tbl_rolledSteelChannels = new TableRolledSteelChannels(tab_path, eDesignStandard.IndianStandard);
                //tbl_rolledSteelAngles = new TableRolledSteelAngles(tab_path, eDesignStandard.IndianStandard);
                tbl_rolledSteelBeams = iApp.Tables.Get_SteelBeams();
                tbl_rolledSteelChannels = iApp.Tables.Get_SteelChannels();
                tbl_rolledSteelAngles = iApp.Tables.Get_SteelAngles(); ;
            }
        }

        #region Form Events
        private void btnClose_Click(object sender, EventArgs e)
        {

        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {

        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btnReport.Name)
            {
                iApp.View_Result(rep_tm_file);
            }
            else if (btn.Name == btn_dm_rep.Name)
            {
                iApp.View_Result(rep_dm_file);
            }
            else if (btn.Name == btn_vm_rep.Name)
            {
                iApp.View_Result(rep_vm_file);
            }
            else if (btn.Name == btn_bm_rep.Name)
            {
                iApp.View_Result(rep_bm_file);
            }
            else if (btn.Name == btn_sb_rep.Name)
            {
                iApp.View_Result(rep_sb_file);
            }
            else if (btn.Name == btn_cg_rep.Name)
            {
                iApp.View_Result(rep_cg_file);
            }
        }

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                    {
                        is_process = false;
                        FilePath = fbd.SelectedPath;

                        btn_bm_design.Enabled = true;
                        btn_top_chd_design.Enabled = true;
                        btn_vm_design.Enabled = true;
                        btn_dgm_design.Enabled = true;
                        btn_sb_des.Enabled = true;
                        btn_cg_des.Enabled = true;

                        btn_bm_rep.Enabled = true;
                        btnReport.Enabled = true;
                        btn_vm_rep.Enabled = true;
                        btn_dm_rep.Enabled = true;
                        btn_sb_rep.Enabled = true;
                        btn_cg_rep.Enabled = true;
                    }
                }
            }
        }

        private void frmStreelTrussMemberDesign_Load(object sender, EventArgs e)
        {
            //complete_design.Members = new MembersDesign();
            SetComboSections();
            cmb_sections_define.SelectedIndex = 0;
        }
        private void cmb_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section11;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "600";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISLB");
                    cmb_section_name.Items.Add("ISJB");
                    cmb_section_name.Items.Add("ISMB");
                    cmb_section_name.Items.Add("ISWB");
                    cmb_section_name.Items.Add("ISHB");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "2";

                    txt_top_chd_S.Text = "500";
                    txt_dgm_S.Text = "500";
                    txt_vm_S.Text = "500";

                    txt_tp_width.Text = "500.0";
                    txt_tp_thk.Text = "22.0";
                    txt_bp_wd.Text = "0.0";
                    txt_bp_thk.Text = "0.0";
                    txt_sp_wd.Text = "0.0";
                    txt_sp_thk.Text = "0.0";
                    txt_vsp_wd.Text = "0.0";
                    txt_vsp_thk.Text = "0.0";
                    break;
                case 1:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "400";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISJC");
                    cmb_section_name.Items.Add("ISLC");
                    cmb_section_name.Items.Add("ISMC");
                    cmb_section_name.SelectedIndex = 2;
                    txt_no_ele.Text = "2";

                    txt_dgm_S.Text = "400";
                    txt_vm_S.Text = "400";

                    txt_top_chd_S.Text = "400";
                    txt_tp_width.Text = "0.0";
                    txt_tp_thk.Text = "0.0";
                    txt_bp_wd.Text = "0.0";
                    txt_bp_thk.Text = "0.0";
                    txt_sp_wd.Text = "320.0";
                    txt_sp_thk.Text = "10.0";
                    txt_vsp_wd.Text = "0.0";
                    txt_vsp_thk.Text = "0.0";
                    break;
                case 2:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISA");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "4";

                    txt_dgm_S.Text = "350";
                    txt_vm_S.Text = "350";


                    txt_top_chd_S.Text = "350";
                    txt_tp_width.Text = "350.0";
                    txt_tp_thk.Text = "25.0";
                    txt_sp_wd.Text = "420.0";
                    txt_sp_thk.Text = "16.0";
                    txt_bp_wd.Text = "0.0";
                    txt_bp_thk.Text = "0.0";
                    txt_vsp_wd.Text = "0.0";
                    txt_vsp_thk.Text = "0.0";
                    break;
                case 3:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISA");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "4";
                    txt_dgm_S.Text = "350";
                    txt_vm_S.Text = "350";
                    txt_top_chd_S.Text = "350";
                    txt_tp_width.Text = "350.0";
                    txt_tp_thk.Text = "25.0";
                    txt_sp_wd.Text = "420.0";
                    txt_sp_thk.Text = "16.0";
                    txt_bp_wd.Text = "0.0";
                    txt_bp_thk.Text = "0.0";
                    txt_vsp_wd.Text = "120.0";
                    txt_vsp_thk.Text = "25.0";
                    break;
                case 4:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "600";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISLB");
                    cmb_section_name.Items.Add("ISJB");
                    cmb_section_name.Items.Add("ISMB");
                    cmb_section_name.Items.Add("ISWB");
                    cmb_section_name.Items.Add("ISHB");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "1";

                    txt_top_chd_S.Text = "500";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "125";
                    txt_bp_thk.Text = "10";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 5:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISLB");
                    cmb_section_name.Items.Add("ISJB");
                    cmb_section_name.Items.Add("ISMB");
                    cmb_section_name.Items.Add("ISWB");
                    cmb_section_name.Items.Add("ISHB");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "1";

                    txt_top_chd_S.Text = "500";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "490";
                    txt_vsp_thk.Text = "12";
                    break;
                case 6:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Top_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISA");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "4";

                    txt_top_chd_S.Text = "500";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 7:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    cmb_section_name.Items.Add("ISA");
                    cmb_section_name.SelectedIndex = 0;
                    txt_no_ele.Text = "1";

                    txt_top_chd_S.Text = "500";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
            }
            SetComboSections();
        }

        private void tab_page_Enter(object sender, EventArgs e)
        {
            TabPage tab_p = sender as TabPage;

            grb_def_sec.Enabled = true;

            if (tab_p == null) return;
            if (tab_p.Name == tab_GD.Name)
            {
                pcb_images.BackgroundImage = global::AstraFunctionOne.Properties.Resources._01_General;
                pcb_images.Visible = true;
                grb_def_sec.Visible = false;
            }
            else if (tab_p.Name == tab_stringer_beam.Name)
            {
                pcb_images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                pcb_images.Visible = true;
                grb_def_sec.Visible = false;
            }
            else if (tab_p.Name == tab_cross_gorder.Name)
            {
                pcb_images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
                pcb_images.Visible = true;
                grb_def_sec.Visible = false;
            }
            else
            {
                grb_def_sec.Visible = true;
                pcb_images.Visible = false;
            }
        }
        private void tab_stringer_beam_Enter(object sender, EventArgs e)
        {
            pcb_images.Visible = false;

        }
        private void btn_cg_des_Click(object sender, EventArgs e)
        {
            Calculate_Program("CG");
        }
        private void btn_sb_des_Click(object sender, EventArgs e)
        {
            Calculate_Program("SB");
        }
        private void cmb_sec_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_code1.Items.Clear();
            cmb_sec_thk.Items.Clear();
            string sec_name, sec_code;

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                case 4:
                case 5:
                    for (int i = 0; i < tbl_rolledSteelBeams.List_Table.Count; i++)
                    {
                        sec_code = tbl_rolledSteelBeams.List_Table[i].SectionCode;
                        sec_name = tbl_rolledSteelBeams.List_Table[i].SectionName;
                        if (sec_name == cmb_section_name.Text && sec_name != "")
                        {
                            if (cmb_code1.Items.Contains(sec_code) == false)
                            {
                                cmb_code1.Items.Add(sec_code);
                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < tbl_rolledSteelChannels.List_Table.Count; i++)
                    {
                        sec_code = tbl_rolledSteelChannels.List_Table[i].SectionCode;
                        sec_name = tbl_rolledSteelChannels.List_Table[i].SectionName;
                        if (sec_name == cmb_section_name.Text && sec_name != "")
                        {
                            if (cmb_code1.Items.Contains(sec_code) == false)
                            {
                                cmb_code1.Items.Add(sec_code);
                            }
                        }
                    }
                    break;
                case 2:
                case 3:
                case 6:
                case 7:
                    for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                        sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                        if (sec_name == cmb_section_name.Text && sec_name != "")
                        {
                            if (cmb_code1.Items.Contains(sec_code) == false)
                            {
                                cmb_code1.Items.Add(sec_code);
                            }
                        }
                    }
                    break;
            }
            if (cmb_code1.Items.Count > 0) cmb_code1.SelectedIndex = 0;

        }
        private void cmb_code1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sec_code, sec_name, code2;
            double thk = 0.0;
            cmb_sec_thk.Items.Clear();
            for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
            {
                sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                thk = tbl_rolledSteelAngles.List_Table[i].Thickness;
                if (sec_name == cmb_section_name.Text && sec_code == cmb_code1.Text)
                {
                    if (cmb_sec_thk.Items.Contains(thk) == false)
                    {
                        cmb_sec_thk.Items.Add(thk);
                    }
                }
            }
            if (cmb_sec_thk.Items.Count > 0) cmb_sec_thk.SelectedIndex = 0;

            //        break;
            //}
        }


        
        private void btn_bm_design_Click(object sender, EventArgs e)
        {
            Calculate_Program("BM");
        }
        private void btn_vm_design_Click(object sender, EventArgs e)
        {
            Calculate_Program("VM");

        }
        private void btn_dgm_design_Click(object sender, EventArgs e)
        {
            Calculate_Program("DM");

        }
        private void btn_top_chd_design_Click(object sender, EventArgs e)
        {
            Calculate_Program("TM");
            btn_bm_rep.Enabled = true;
            btn_dm_rep.Enabled = true;
            btn_vm_rep.Enabled = true;
        }

        #endregion


        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        //case "L":
                        //    L = mList.GetDouble(1);
                        //    txt_L.Text = L.ToString();
                        //    break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
            #endregion
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                //sw.WriteLine("L = {0}", L);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void InitializeData()
        {
            //DL = 15;
            //LL = 175;
            //IL = 0.15;
            //h = 4.0;
            //l = 24;
            //fy = 250;
            //fc = 110;
            #region USER DATA INPUT
            try
            {
                //L = MyList.StringToDouble(txt_L.Text, 0.0);
                //top_chord_mf, top_chord_c, top_chord_RI, top_chord_phi_1, top_chord_dr;

                //DL = MyList.StringToDouble(txt_DL.Text, 0.0);
                //LL = MyList.StringToDouble(txt_LL.Text, 0.0);
                IL = MyList.StringToDouble(txt_IL_stringer_beam.Text, 0.0);
                h = MyList.StringToDouble(txt_gd_h.Text, 0.0);
                l = MyList.StringToDouble(txt_L.Text, 0.0);
                fy = MyList.StringToDouble(txt_gd_fy.Text, 0.0);
                fc = MyList.StringToDouble(txt_fc.Text, 0.0);
                ft = MyList.StringToDouble(txt_gd_ft.Text, 0.0);
                sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);

                top_chord_mf = MyList.StringToDouble(txt_top_chd_mf.Text, 0.0);
                top_chord_c = MyList.StringToDouble(txt_top_chd_c.Text, 0.0);
                top_chord_RI = MyList.StringToDouble(txt_top_chd_RI.Text, 0.0);
                top_chord_phi_1 = MyList.StringToDouble(txt_top_chord_phi1.Text, 0.0);
                top_chord_dr = MyList.StringToDouble(txt_top_chd_dr.Text, 0.0);
                top_chord_S = MyList.StringToDouble(txt_top_chd_S.Text, 0.0);

                top_plate_width = MyList.StringToDouble(txt_tp_width.Text, 0.0);
                top_plate_thickness = MyList.StringToDouble(txt_tp_thk.Text, 0.0);
                bottom_plate_width = MyList.StringToDouble(txt_bp_wd.Text, 0.0);
                bottom_plate_thickness = MyList.StringToDouble(txt_bp_thk.Text, 0.0);
                side_plate_width = MyList.StringToDouble(txt_sp_wd.Text, 0.0);
                side_plate_thickness = MyList.StringToDouble(txt_sp_thk.Text, 0.0);
                vertical_stiff_plate_width = MyList.StringToDouble(txt_vsp_wd.Text, 0.0);
                vertical_stiff_plate_thickness = MyList.StringToDouble(txt_vsp_thk.Text, 0.0);


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }
        public void Calculate_Program(string des)
        {
            InitializeData();
            string file_name = "";
            switch (des)
            {
                case "TM":
                    file_name = rep_tm_file;
                    break;
                case "VM":
                    file_name = rep_vm_file;
                    break;
                case "DM":
                    file_name = rep_dm_file;
                    break;
                case "BM":
                    file_name = rep_bm_file;
                    break;
                case "SB":
                    file_name = rep_sb_file;
                    break;
                case "CG":
                    file_name = rep_cg_file;
                    break;
            }

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));

            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21            *");
                sw.WriteLine("\t\t*      TechSOFT Engineering Services         *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*   DESIGN OF STEEL TRUSS OPEN WEB GIRDER    *");
                sw.WriteLine("\t\t*        FOR HIGHWAY/RAILWAY BRIDGES         *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                #endregion


                #region User'a Data
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("USER'S DATA");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                #endregion

                #region DESIGN CALCULATIONS
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("DESIGN CALCULATIONS");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();

                #endregion

                #region STEP
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("STEP  : ");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                #endregion

                #region STEP GENERAL DATA
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : GENERAL DATA ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("USER'S DATA");
                //sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Length / Span of each Truss [L] = {0} m", txt_L.Text);
                sw.WriteLine("Length of each Panel [l] = {0} m", txt_gd_l.Text);
                sw.WriteLine("No of Panels = {0} ", txt_gd_np.Text);
                sw.WriteLine("Height of Truss [h] = {0} m", txt_gd_h.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} N/sq.mm", txt_gd_fy.Text);
                sw.WriteLine("Length of Span [l] = {0} kN/m", txt_L.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} kN/m", txt_gd_fy.Text);
                sw.WriteLine("Permissible stress in Axial comppression [fc] = {0} N/sq.mm", txt_fc.Text);
                sw.WriteLine("Permissible Tensile stress [ft] = {0} N/sq.mm", txt_gd_ft.Text);
                sw.WriteLine();
                if (chk_type1.Checked)
                {
                    sw.WriteLine("{0,8}  {1,8} {2,10}  {3,10} {4,6}",
                        "TYPE 1",
                        "LOAD :",
                        txt_load_1.Text,
                        "IMPACT FACTOR :",
                        txt_type_1.Text);



                    //sw.WriteLine("TYPE 1  LOAD : {0}  IMPACT FACTOR : {1}", txt_load_1.Text, txt_type_1.Text);
                }
                if (chk_type2.Checked)
                {
                    sw.WriteLine("{0,8}  {1,8} {2,10}  {3,10} {4,6}",
                                 "TYPE 2",
                                 "LOAD :",
                                 txt_load_2.Text,
                                 "IMPACT FACTOR :",
                                 txt_type_2.Text);

                }
                if (chk_type3.Checked)
                {
                    sw.WriteLine("{0,8}  {1,8} {2,10}  {3,10} {4,6}",
                                "TYPE 3",
                                "LOAD :",
                                txt_load_3.Text,
                                "IMPACT FACTOR :",
                                txt_type_3.Text);

                }
                if (chk_type4.Checked)
                {
                    sw.WriteLine("{0,8}  {1,8} {2,10}  {3,10} {4,6}",
                                 "TYPE 4",
                                 "LOAD :",
                                 txt_load_4.Text,
                                 "IMPACT FACTOR :",
                                 txt_type_4.Text);

                }
                if (chk_type5.Checked)
                {
                    sw.WriteLine("{0,8}  {1,8} {2,10}  {3,10} {4,6}",
                                 "TYPE 5",
                                 "LOAD :",
                                 txt_load_5.Text,
                                 "IMPACT FACTOR :",
                                 txt_type_5.Text);

                }
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                switch (des)
                {
                    case "TM":
                        TopChordMember(sw);
                        break;
                    case "DM":
                        DiagonalMember(sw);
                        break;
                    case "VM":
                        VerticalMember(sw);
                        break;
                    case "BM":
                        BottomChordMember(sw);
                        break;
                    case "SB":
                        StringerBeam(sw);
                        break;
                    case "CG":
                        CrossGirder(sw);
                        break;
                }

                //DiagonalMember(sw);
                //VerticalMember(sw);
                //BottomChordMember(sw);
                #region End of Report
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion
                WriteTable1(sw);
                WriteTable2(sw);
                WriteTable3(sw);
                WriteTable4(sw);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            finally
            {
                sw.Flush();
                sw.Close();
            }

            MessageBox.Show(this, "Report file written in " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                //sw.WriteLine("_A={0}", _A);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Bridge Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Steel Truss Bridge");

            //if (!Directory.Exists(kPath))
            //{
            //    Directory.CreateDirectory(kPath);
            //}
            //kPath = Path.Combine(kPath, "Design of Steel Truss Open Web Girder Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }
        public string FilePath
        {
            set
            {
                this.Text = "DESIGN OF STEEL TRUSS OPEN WEB GIRDER BRIDGE : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Steel_Truss_Open_Web_Girder.TXT");
                rep_tm_file = Path.Combine(file_path, "Bridge_Steel_Truss_Top_Chord.TXT");
                rep_dm_file = Path.Combine(file_path, "Bridge_Steel_Truss_Diagonal.TXT");
                rep_vm_file = Path.Combine(file_path, "Bridge_Steel_Truss_Vertical.TXT");
                rep_bm_file = Path.Combine(file_path, "Bridge_Steel_Truss_Bottom_Chord.TXT");
                rep_sb_file = Path.Combine(file_path, "Bridge_Steel_Truss_Stringer_Beam.TXT");
                rep_cg_file = Path.Combine(file_path, "Bridge_Steel_Truss_Cross_Girder.TXT");

                user_input_file = Path.Combine(system_path, "Steel Truss Open Web Girder Bridge.FIL");
                user_drawing_file = Path.Combine(system_path, "STEEL_TRUSS_OPEN_WEB_GIRDER_DRAWING.FIL");

                //btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }
            }
        }
        public string Title
        {
            get
            {
                return "STEEL_TRUSS_OPEN_WEB_MEMBER_DESIGN";
            }
        }

        #region Member Design
        #region TOP CHORD
        public void TopChordMember(StreamWriter sw)
        {

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    TopChord_Section1(sw);
                    break;
                case 1:
                    TopChord_Section2(sw);
                    break;
                case 2:
                    TopChord_Section3(sw);
                    break;
                case 3:
                    TopChord_Section4(sw);
                    break;
            }

        }
        public void TopChord_Section1(StreamWriter sw)
        {
            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF TOP CHORD MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Design Compressive Force = {0} kN ", top_chord_mf);
            sw.WriteLine();
            top_chord_c = top_chord_c * 1000.0;
            sw.WriteLine("Length of Member = C = {0} m = {1} mm", txt_top_chd_c.Text, top_chord_c);
            sw.WriteLine("fc = {0} kN/m", fc);
            sw.WriteLine();

            double Ar = (top_chord_mf * 1000.0) / fc;
            sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelBeamsRow tabData = tbl_rolledSteelBeams.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();

            int n = 2;
            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("Flange Width = B = {0:f3} ", tabData.FlangeWidth);
            sw.WriteLine("Ixx = {0:f3} ", tabData.Ixx);
            sw.WriteLine("Iyy = {0:f3} ", tabData.Iyy);
            sw.WriteLine();
            sw.WriteLine("Top plate width = {0} mm", top_plate_width);
            sw.WriteLine("Top plate Thickness = {0} mm", top_plate_thickness);
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area *
                100 * Math.Pow((top_chord_S / 2.0)
                - (tabData.FlangeWidth / 2.0), 2)) + ((top_plate_thickness
                * top_plate_width * top_plate_width * top_plate_width) / 12.0);

            sw.WriteLine();
            //sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();

            sw.WriteLine("Iyy = n * (Iyy * 10^4 + A * 100 * (S/2 - B/2)^2) + (tp * Bp * Bp * Bp)/12");
            sw.WriteLine();
            sw.WriteLine("    = {0} * ({1:f3} * 10^4 + {2:f3} * 100 * ({3:f3}/2 - {4:f3}/2)^2) + ({5:f3} * {6:f3} * {6:f3} * {6:f3})/12",
                n, tabData.Iyy, tabData.Area, top_chord_S, tabData.FlangeWidth,
                top_plate_thickness, top_plate_width);
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + top_plate_width * top_plate_thickness;
            sw.WriteLine("Area = n * A * 100 + Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3}", n, tabData.Area, top_plate_width, top_plate_thickness);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iyy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            sw.WriteLine("Distance Between Rackers = {0} m", top_chord_dr);
            double ly = top_chord_dr * 1000;
            sw.WriteLine();
            sw.WriteLine("ly = {0} * 1000 = {1} mm", top_chord_dr, ly);
            sw.WriteLine();

            double lamda = ly / ry;
            sw.WriteLine("λ = ly/ry = {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                                   = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                                   = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > top_chord_mf)
            {
                sw.WriteLine("                                   = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }
            else if (load_capt < top_chord_mf)
            {
                sw.WriteLine("                                   = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, top_chord_mf);
            }
            else
            {
                sw.WriteLine("                                   = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }

            #endregion
        }
        public void TopChord_Section2(StreamWriter sw)
        {
            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF TOP CHORD MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Design Compressive Force in Member = {0} kN (Compression)", top_chord_mf);
            sw.WriteLine();
            top_chord_c = top_chord_c * 1000.0;
            sw.WriteLine("Length of Member = C = {0} m = {1} mm", txt_top_chd_c.Text, top_chord_c);
            sw.WriteLine("fc = {0} kN/m", fc);
            sw.WriteLine();

            double Ar = (top_chord_mf * 1000.0) / fc;
            sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            sw.WriteLine();
            sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();


            RolledSteelChannelsRow tabData = tbl_rolledSteelChannels.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);

            int n = 2;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine("Iyy = {0:f3} ", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("n = 2");
            sw.WriteLine("t = {0:f3} ", tabData.WebThickness);
            sw.WriteLine();
            double Bp = side_plate_width;
            double tp = side_plate_thickness;

            sw.WriteLine("Plates 2 * {0} * {1} ", Bp, tp);

            sw.WriteLine("Side plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Side plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("Spacing = S = {0} mm", top_chord_S);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();

            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area *
                100 * Math.Pow((top_chord_S / 2.0), 2.0)) + n * (Bp * tp + Math.Pow(((top_chord_S / 2.0) - tabData.WebThickness + (tp / 2.0)), 2.0));

            sw.WriteLine("Iyy = n * (Iyy * 10^4 + a * 100 * (S/2)^2) + 2 * (Bp*tp + (S/2 - t + tp/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2)^2) + 2 * ({4}*{5} + ({3}/2 - {6} + {5}/2)^2)",
                n, tabData.Iyy, a, top_chord_S,
                Bp, tp, tabData.WebThickness);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + n * side_plate_width * side_plate_thickness;
            sw.WriteLine("Area = n * A * 100 + n * Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {0} * {2} * {3}", n, tabData.Area, side_plate_width, side_plate_thickness);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iyy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            sw.WriteLine("Distance Between Rackers = {0} m", top_chord_dr);
            double ly = top_chord_dr * 1000;
            sw.WriteLine();
            sw.WriteLine("ly = {0} * 1000 = {1} mm", top_chord_dr, ly);
            sw.WriteLine();

            double lamda = ly / ry;
            sw.WriteLine("λ = ly/ry = {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }
            else if (load_capt < top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, top_chord_mf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }

            #endregion
        }
        public void TopChord_Section3(StreamWriter sw)
        {
            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF TOP CHORD MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Design Force in Member = {0} kN (Compression)", top_chord_mf);
            sw.WriteLine();
            top_chord_c = top_chord_c * 1000.0;
            sw.WriteLine("Length of Member = C = {0} m = {1} mm", txt_top_chd_c.Text, top_chord_c);
            sw.WriteLine("Raker Distance = RI = {0} m", top_chord_RI);
            //sw.WriteLine("fc = {0} kN/m", fc);
            sw.WriteLine();
            sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();


            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                cmb_code1.Text,
                MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);

            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();

            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double S = top_chord_S;

            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Spacing = S = {0} mm", S);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
                (tp * Bp * Bp * Bp / 12.0) * np +
                ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0));

            sw.WriteLine("Iy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2)",
                        n,
                        tabData.Iyy,
                        tabData.Area,
                        S,
                        tabData.Cxx,
                        tp, Bp, np,
                        ns, Bs, ts);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            sw.WriteLine("Distance Between Rackers = {0} m", top_chord_dr);
            double ly = top_chord_dr * 1000;
            sw.WriteLine();
            sw.WriteLine("ly = {0} * 1000 = {1} mm", top_chord_dr, ly);
            sw.WriteLine();

            double lamda = ly / ry;
            sw.WriteLine("λ = ly/ry = {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design force OK", load_capt, top_chord_mf);
            }
            else if (load_capt < top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design force NOT OK", load_capt, top_chord_mf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design force OK", load_capt, top_chord_mf);
            }

            #endregion
        }
        public void TopChord_Section4(StreamWriter sw)
        {

            #region STEP 1 : DESIGN CALCULATION
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF TOP CHORD MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design Compressive Force in Member = {0} kN ", top_chord_mf);
            sw.WriteLine();
            top_chord_c = top_chord_c * 1000.0;
            sw.WriteLine("Length of Member = C = {0} mm", top_chord_c);
            sw.WriteLine("Raker Distance = RI = {0} m", top_chord_RI);
            //sw.WriteLine("fc = {0} kN/m", fc);
            sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();


            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                cmb_code1.Text,
                MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);

            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();

            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double Bss = vertical_stiff_plate_width;
            double tss = vertical_stiff_plate_thickness;
            int nss = 2;
            double S = top_chord_S;

            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Vertical Stiffener plate width = Bss = {0} mm", Bss);
            sw.WriteLine("Vertical Stiffener plate Thickness = tss = {0} mm", tss);
            sw.WriteLine("No of Vertical Stiffener plate = nss = {0}", nss);
            sw.WriteLine("Spacing = S = {0} mm", S);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
                (tp * Bp * Bp * Bp / 12.0) * np +
                ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) +
                nss * ((Bss * tss * tss * tss / 12) + Bss * tss * Math.Pow(((S / 2.0) - tabData.Thickness - (tss / 2.0)), 2.0));


            //double Iy1 = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0));
            //double Iy2 = (tp * Bp * Bp * Bp / 12.0) * np;
            //double Iy3 = ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0));
            //double Iy4 = nss * ((Bss * tss * tss * tss / 12) + Bss * tss * Math.Pow(((S / 2.0) - tabData.Thickness - (tss / 2.0)), 2.0));


            //double Iy5 = Iy1 + Iy2 + Iy3 + Iy4;

            sw.WriteLine("Iy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2) + {11}*(({12}*{13}*{13}*{13}/12) + {12}*{13} * ({3}/2 - {14} - {13}/2)^2)",
                        n,
                        tabData.Iyy,
                        tabData.Area,
                        S,
                        tabData.Cxx,
                        tp, Bp, np,
                        ns, Bs, ts,
                        nss, Bss, tss, tabData.Thickness);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns, Bss, tss, nss);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            sw.WriteLine("Distance Between Rackers = {0} m", top_chord_dr);
            double ly = top_chord_dr * 1000;
            sw.WriteLine();
            sw.WriteLine("Effective length = ly = {0} mm", ly);
            sw.WriteLine();

            double lamda = ly / ry;
            sw.WriteLine("λ = ly/ry = {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            //sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }
            else if (load_capt < top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, top_chord_mf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }

            #endregion
        }
        #endregion TOP CHORD

        #region DIAGONAL MEMBER
        public void DiagonalMember(StreamWriter sw)
        {

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    DiagonalMember_Section1(sw);
                    break;
                case 1:
                    DiagonalMember_Section2(sw);
                    break;
                case 2:
                    DiagonalMember_Section3(sw);
                    break;
                case 3:
                    DiagonalMember_Section4(sw);
                    break;
            }

        }
        public void DiagonalMember_Section1(StreamWriter sw)
        {

            double dgm_cf, dgm_TF, dgm_ly, dgm_ft, dgm_S, dgm_bolt_dia, top_chord_dr, top_chord_S;
            int dgm_nb;

            dgm_cf = MyList.StringToDouble(txt_dgm_cf.Text, 0.0);
            dgm_TF = MyList.StringToDouble(txt_dgm_TF.Text, 0.0);
            dgm_ft = MyList.StringToDouble(txt_dgm_ft.Text, 0.0);
            dgm_ly = MyList.StringToDouble(txt_dgm_ly.Text, 0.0);
            dgm_S = MyList.StringToDouble(txt_dgm_S.Text, 0.0);
            dgm_bolt_dia = MyList.StringToDouble(txt_dgm_bolt_dia.Text, 0.0);
            dgm_nb = MyList.StringToInt(txt_dgm_nb1.Text, 0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF DIAGONAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", dgm_TF);
            sw.WriteLine("Design Compressive Force = cf = {0} kN", dgm_cf);
            sw.WriteLine();
            dgm_ly = dgm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", dgm_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", dgm_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", dgm_nb);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();

            RolledSteelBeamsRow tabData = tbl_rolledSteelBeams.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();

            int n = 2;
            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine("Spacing = S = {0}", dgm_S);
            sw.WriteLine("Top plate width = Bp = {0} mm", top_plate_width);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", top_plate_thickness);
            sw.WriteLine();
            sw.WriteLine("Ixx = {0:f3} ", tabData.Ixx);
            sw.WriteLine("Iyy = {0:f3} ", tabData.Iyy);

            double a, D, t, B, tf;

            a = tabData.Area;
            D = tabData.Depth;
            B = tabData.FlangeWidth;
            t = tabData.WebThickness;
            tf = tabData.FlangeThickness;

            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("Web Thickness = t = {0:f3} mm.", tabData.WebThickness);
            sw.WriteLine("Flange Width = B = {0:f3} ", tabData.FlangeWidth);
            sw.WriteLine("Flange Thickness = tf = {0:f3} ", tabData.FlangeThickness);
            sw.WriteLine();

            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            double Iy = (tabData.Iyy * 10000 + a * 100 * (Math.Pow(((dgm_S / 2) - (B / 2.0)), 2.0))) * dgm_nb +
                ((tp * Bp * Bp * Bp) / 12.0);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Iy = (Iyy * 10000 + a * 100 * (((S / 2) - (B / 2.0))^2))) * nb + ((tp * Bp * Bp * Bp) / 12.0)");
            sw.WriteLine();
            sw.WriteLine("   = ({0} * 10000 + {1} * 100 * ((({2} / 2) - ({3} / 2.0))^2))) * {4} + (({5} * {6} * {6} * {6}) / 12.0)",
                tabData.Iyy, a, dgm_S, B, dgm_nb, tp, Bp);
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iy);
            sw.WriteLine();

            //double Ix = tabData.Ixx + tabData.Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * Math.Pow((tp / 2.0 + D / 2.0), 2.0);
            //sw.WriteLine("Ix = Ixx + Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * ((tp / 2.0 + D / 2.0)^2)");
            //sw.WriteLine("   = {0} + {0} + ({1} * {2} * {2} * {2} / 12.0) + {3} * {2} * (({2} / 2.0 + {3} / 2.0)^2)",
            //    tabData.Ixx, Bp, tp, D);

            double Area = n * a * 100 + Bp * tp;
            sw.WriteLine("Area = n * A * 100 + Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3}", n, tabData.Area, top_plate_width, top_plate_thickness);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            //sw.WriteLine("fy = {0} N/sq.mm.", fy);
            sw.WriteLine();
            double ly = dgm_ly;
            sw.WriteLine();
            sw.WriteLine("Effective Length = ly = {0} mm", ly);
            sw.WriteLine();

            double lamda = (0.85 * ly) / ry;
            sw.WriteLine("λ = 0.85 * ly / ry = 0.85 * {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("Fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }
            else if (load_capt < top_chord_mf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, top_chord_mf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, top_chord_mf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Force");
            sw.WriteLine();
            double net_area = Area - n * (dgm_bolt_dia + 1.5) * (tf + tp);
            sw.WriteLine("Net Area = A - nb*(dia+1.5) * (tf + tp)");
            sw.WriteLine("         = {0} - {1}*({2}+1.5) * ({3} + {4})",
                a, dgm_nb, dgm_bolt_dia, tf, tp);
            sw.WriteLine("         = {0:f3} sq.mm", net_area);
            sw.WriteLine();

            load_capt = net_area * dgm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, dgm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            if (load_capt > dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN,Design Tensile force OK", load_capt, dgm_TF);
            }
            else if (load_capt < dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN,Design  Tensile force NOT OK", load_capt, dgm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN,Design  Tensile force OK", load_capt, dgm_TF);
            }
            #endregion
        }
        public void DiagonalMember_Section2(StreamWriter sw)
        {
            double dgm_cf, dgm_TF, dgm_ft, dgm_ly, dgm_S, dgm_bolt_dia, top_chord_dr, top_chord_S;
            int dgm_nb;

            dgm_cf = MyList.StringToDouble(txt_dgm_cf.Text, 0.0);
            dgm_TF = MyList.StringToDouble(txt_dgm_TF.Text, 0.0);
            dgm_ft = MyList.StringToDouble(txt_dgm_ft.Text, 0.0);
            dgm_ly = MyList.StringToDouble(txt_dgm_ly.Text, 0.0);
            dgm_S = MyList.StringToDouble(txt_dgm_S.Text, 0.0);
            dgm_bolt_dia = MyList.StringToDouble(txt_dgm_bolt_dia.Text, 0.0);
            dgm_nb = MyList.StringToInt(txt_dgm_nb1.Text, 0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF DIAGONAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", dgm_TF);
            sw.WriteLine("Design Compressive Force = cf = {0} kN", dgm_cf);
            sw.WriteLine();
            dgm_ly = dgm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly mm", dgm_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", dgm_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", dgm_nb);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();

            RolledSteelChannelsRow tabData = tbl_rolledSteelChannels.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);

            int np = 2;
            int n = 2;

            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine();
            double a, D, t, B, tf;
            a = tabData.Area;
            D = tabData.Depth;
            B = tabData.FlangeWidth;
            t = tabData.WebThickness;
            tf = tabData.FlangeThickness;

            //sw.WriteLine("Ixx = {0:f3} ", tabData.Ixx);
            sw.WriteLine("Iyy = {0:f3} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("Web Thickness = t = {0:f3} mm.", tabData.WebThickness);
            //sw.WriteLine("Flange Width = B = {0:f3} ", tabData.FlangeWidth);
            //sw.WriteLine("Flange Thickness = tf = {0:f3} ", tabData.FlangeThickness);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = side_plate_width;
            double tp = side_plate_thickness;
            sw.WriteLine("Plates {0} * {1} * {2} ", n, Bp, tp);
            sw.WriteLine("Spacing = S = {0}", dgm_S);
            sw.WriteLine("Side plate width = Bp = {0} mm", side_plate_width);
            sw.WriteLine("Side plate Thickness = tp = {0} mm", side_plate_thickness);
            sw.WriteLine("No of Side plate = np = {0} mm", n);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();


            double Iy = n * (tabData.Iyy * 10000 + a * 100 * (dgm_S / 2) * (dgm_S / 2)) +
                np * (Bp * tp + Math.Pow(((dgm_S / 2.0) - t + (tp / 2.0)), 2.0));


            sw.WriteLine("Iy = n * (Iyy * 10^4 + a * 100 * (S / 2)^2) + np * (Bp * tp + ((dgm_S / 2.0) - t + (tp / 2.0))^2)");
            sw.WriteLine();
            sw.WriteLine("   = {0} * ({1} * 10^4 + {2} * 100 * ({3} / 2)^2) + {4} * ({5} * {6} + (({3} / 2.0) - {7} + ({6} / 2.0))^2)",
                n, tabData.Iyy, a, dgm_S, np, Bp, tp, t);
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iy);
            sw.WriteLine();

            //double Ix = tabData.Ixx + tabData.Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * Math.Pow((tp / 2.0 + D / 2.0), 2.0);
            //sw.WriteLine("Ix = Ixx + Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * ((tp / 2.0 + D / 2.0)^2)");
            //sw.WriteLine("   = {0} + {0} + ({1} * {2} * {2} * {2} / 12.0) + {3} * {2} * (({2} / 2.0 + {3} / 2.0)^2)",
            //    tabData.Ixx, Bp, tp, D);

            double Area = n * a * 100 + np * Bp * tp;
            sw.WriteLine("Area = n * A * 100 + Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3}", n, tabData.Area, top_plate_width, top_plate_thickness);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            //sw.WriteLine("fy = {0} N/sq.mm.", fy);
            sw.WriteLine();
            double ly = dgm_ly;
            sw.WriteLine();
            sw.WriteLine("Length of Member = ly = {0} * 1000 = {1} mm", dgm_ly, ly);
            sw.WriteLine();

            double lamda = (0.85 * ly) / ry;
            sw.WriteLine("λ = 0.85 * ly / ry = 0.85 * {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > dgm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, dgm_cf);
            }
            else if (load_capt < dgm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, dgm_cf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, dgm_cf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Force");
            sw.WriteLine();
            double net_area = Area - n * (dgm_bolt_dia + 1.5) * (tf + tp);
            sw.WriteLine("Net Area = A - nb*(dia+1.5) * (tf + tp)");
            sw.WriteLine("         = {0} - {1}*({2}+1.5) * ({3} + {4})",
                a, dgm_nb, dgm_bolt_dia, tf, tp);
            sw.WriteLine("         = {0:f3} sq.mm", net_area);
            sw.WriteLine();

            load_capt = net_area * dgm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, dgm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN,Design Tensile force OK", load_capt, dgm_TF);
            }
            else if (load_capt < dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN,Design Tensile force NOT OK", load_capt, dgm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN,Design Tensile force OK", load_capt, dgm_TF);
            }

            #endregion
        }
        public void DiagonalMember_Section3(StreamWriter sw)
        {
            double dgm_cf, dgm_TF, dgm_ft, dgm_ly, dgm_S, dgm_bolt_dia, top_chord_dr, top_chord_S;
            int dgm_nb1, dgm_nb2;

            dgm_cf = MyList.StringToDouble(txt_dgm_cf.Text, 0.0);
            dgm_TF = MyList.StringToDouble(txt_dgm_TF.Text, 0.0);
            dgm_ft = MyList.StringToDouble(txt_dgm_ft.Text, 0.0);
            dgm_ly = MyList.StringToDouble(txt_dgm_ly.Text, 0.0);
            dgm_S = MyList.StringToDouble(txt_dgm_S.Text, 0.0);
            dgm_bolt_dia = MyList.StringToDouble(txt_dgm_bolt_dia.Text, 0.0);
            dgm_nb1 = MyList.StringToInt(txt_dgm_nb1.Text, 0);
            dgm_nb2 = MyList.StringToInt(txt_dgm_nb2.Text, 0);
            //int nb1 = 2;
            //int nb2 = 4;


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF DIAGONAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", dgm_TF);
            sw.WriteLine("Design Compressive Force = cf = {0} kN", dgm_cf);
            sw.WriteLine("Permissible Tensile Force = ft = {0} N/sq.mm", dgm_ft);
            sw.WriteLine();
            dgm_ly = dgm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", dgm_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", dgm_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", dgm_nb1);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();

            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                 cmb_code1.Text,
                 MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);
            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double S = dgm_S;
            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Spacing = S = {0} mm", S);


            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
                (tp * Bp * Bp * Bp / 12.0) * np +
                ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0));

            sw.WriteLine("Iyy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2)",
                        n,
                        tabData.Iyy,
                        tabData.Area,
                        S,
                        tabData.Cxx,
                        tp, Bp, np,
                        ns, Bs, ts);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iyy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            double ly = dgm_ly;
            sw.WriteLine();
            sw.WriteLine("Effective Length = ly = {0} mm", ly);
            sw.WriteLine();

            double lamda = 0.85 * ly / ry;
            //double lamda = ly / ry;
            sw.WriteLine("λ = 0.85 * ly/ry = 0.85 * {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressing Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > dgm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressing force OK", load_capt, dgm_TF);
            }
            else if (load_capt < dgm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressing force NOT OK", load_capt, dgm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressing force OK", load_capt, dgm_TF);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Load Carrying Capasity");
            sw.WriteLine();

            double net_area = Area - dgm_nb1 * (dgm_bolt_dia + 1.5) * (tabData.Thickness + tp) - dgm_nb2 * (dgm_bolt_dia + 1.5) * (tabData.Thickness + ts);
            sw.WriteLine("Net Area of Section = A - nb1*(dia+1.5)*(t+tp)-nb2*(dia+1.5)*(t+ts)");
            sw.WriteLine("                    = {0} - {1}*({2}+1.5)*({3}+{4})-{5}*({2}+1.5)*({3}+{6})",
                Area, dgm_nb1, dgm_bolt_dia, tabData.Thickness, tp, dgm_nb2, ts);


            load_capt = net_area * dgm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, dgm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN,Design Tensile force OK", load_capt, dgm_TF);
            }
            else if (load_capt < dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN,Design  Tensile force NOT OK", load_capt, dgm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN,Design  Tensile force OK", load_capt, dgm_TF);
            }
            #endregion
        }
        public void DiagonalMember_Section4(StreamWriter sw)
        {
            double dgm_cf, dgm_TF, dgm_ft, dgm_ly, dgm_S, dgm_bolt_dia;
            int dgm_nb1, dgm_nb2;

            dgm_cf = MyList.StringToDouble(txt_dgm_cf.Text, 0.0);
            dgm_TF = MyList.StringToDouble(txt_dgm_TF.Text, 0.0);
            dgm_ft = MyList.StringToDouble(txt_dgm_ft.Text, 0.0);
            dgm_ly = MyList.StringToDouble(txt_dgm_ly.Text, 0.0);
            dgm_S = MyList.StringToDouble(txt_dgm_S.Text, 0.0);
            dgm_bolt_dia = MyList.StringToDouble(txt_dgm_bolt_dia.Text, 0.0);
            dgm_nb1 = MyList.StringToInt(txt_dgm_nb1.Text, 0);
            dgm_nb2 = MyList.StringToInt(txt_dgm_nb2.Text, 0);

            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF DIAGONAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", dgm_TF);
            sw.WriteLine("Design Compressive Force = cf = {0} kN", dgm_cf);
            sw.WriteLine("Permissible Tensile Force = ft = {0} N/sq.mm", dgm_ft);
            sw.WriteLine();
            dgm_ly = dgm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", dgm_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", dgm_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", dgm_nb1);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();

            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                 cmb_code1.Text,
                 MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);
            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double Bss = vertical_stiff_plate_width;
            double tss = vertical_stiff_plate_thickness;
            int nss = 2;



            double S = dgm_S;
            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Vertical Stiffener plate width = Bss = {0} mm", Bss);
            sw.WriteLine("Vertical Stiffener plate Thickness = tss = {0} mm", tss);
            sw.WriteLine("No of Vertical Stiffener plate = nss = {0}", nss);
            sw.WriteLine("Spacing = S = {0} mm", S);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
                (tp * Bp * Bp * Bp / 12.0) * np +
                ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) +
                nss * ((Bss * tss * tss * tss / 12) + Bss * tss * Math.Pow(((S / 2.0) - tabData.Thickness - (tss / 2.0)), 2.0));

            sw.WriteLine("Iy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2) + {11}*(({12}*{13}*{13}*{13}/12) + {12}*{13} * ({3}/2 - {14} - {13}/2)^2)",
                        n,
                        tabData.Iyy,
                        tabData.Area,
                        S,
                        tabData.Cxx,
                        tp, Bp, np,
                        ns, Bs, ts,
                        nss, Bss, tss, tabData.Thickness);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns, Bss, tss, nss);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iyy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            double ly = dgm_ly;
            sw.WriteLine();
            sw.WriteLine("ly = {0} mm", ly);
            sw.WriteLine();

            double lamda = 0.85 * ly / ry;
            sw.WriteLine("λ = ly/ry = 0.85 * {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > dgm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, dgm_cf);
            }
            else if (load_capt < dgm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, dgm_cf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, dgm_cf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Load Carrying Capasity");
            sw.WriteLine();

            double net_area = Area - dgm_nb1 * (dgm_bolt_dia + 1.5) * (tabData.Thickness + tp) - dgm_nb2 * (dgm_bolt_dia + 1.5) * (tabData.Thickness + ts + tss);
            sw.WriteLine("Net Area of Section = A - nb1*(dia+1.5)*(t+tp)-nb2*(dia+1.5)*(t+ts+tss)");
            sw.WriteLine("                    = {0} - {1}*({2}+1.5)*({3}+{4})-{5}*({2}+1.5)*({3}+{6}+{7})",
                Area, dgm_nb1, dgm_bolt_dia, tabData.Thickness, tp, dgm_nb2, ts, tss);


            load_capt = net_area * dgm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, dgm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, dgm_TF);
            }
            else if (load_capt < dgm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, dgm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, dgm_TF);
            }
            #endregion
        }

        #endregion DIAGONAL MEMBER

        #region VERTICAL MEMBER
        public void VerticalMember(StreamWriter sw)
        {
            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    VerticalMember_Section1(sw);
                    break;
                case 1:
                    VerticalMember_Section2(sw);
                    break;
                case 2:
                    VerticalMember_Section3(sw);
                    break;
                case 3:
                    VerticalMember_Section4(sw);
                    break;
            }
        }

        public void VerticalMember_Section1(StreamWriter sw)
        {

            double vm_cf, vm_TF, vm_ly, vm_ft, vm_S, vm_bolt_dia;
            int vm_nb;

            vm_cf = MyList.StringToDouble(txt_vm_cf.Text, 0.0);
            vm_TF = MyList.StringToDouble(txt_vm_tf.Text, 0.0);
            vm_ft = MyList.StringToDouble(txt_vm_ft.Text, 0.0);
            vm_ly = MyList.StringToDouble(txt_vm_ly.Text, 0.0);
            vm_S = MyList.StringToDouble(txt_vm_S.Text, 0.0);
            vm_bolt_dia = MyList.StringToDouble(txt_vm_bolt_dia.Text, 0.0);
            vm_nb = MyList.StringToInt(txt_vm_nb1.Text, 0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF VERTICAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", vm_TF);
            sw.WriteLine("Design Compressive Force = cf = {0} kN", vm_cf);
            sw.WriteLine();
            vm_ly = vm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", vm_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", vm_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", vm_nb);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelBeamsRow tabData = tbl_rolledSteelBeams.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);

            int n = 2;
            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine("Spacing = S = {0}", vm_S);
            sw.WriteLine("Top plate width = Bp = {0} mm", top_plate_width);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", top_plate_thickness);
            sw.WriteLine();
            sw.WriteLine("Ixx = {0:f3} ", tabData.Ixx);
            sw.WriteLine("Iyy = {0:f3} ", tabData.Iyy);

            double a, D, t, B, tf;

            a = tabData.Area;
            D = tabData.Depth;
            B = tabData.FlangeWidth;
            t = tabData.WebThickness;
            tf = tabData.FlangeThickness;

            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("Web Thickness = t = {0:f3} mm.", tabData.WebThickness);
            sw.WriteLine("Flange Width = B = {0:f3} ", tabData.FlangeWidth);
            sw.WriteLine("Flange Thickness = tf = {0:f3} ", tabData.FlangeThickness);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();


            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            double Iy = (tabData.Iyy * 10000 + a * 100 * (Math.Pow(((vm_S / 2) - (B / 2.0)), 2.0))) * vm_nb +
                ((tp * Bp * Bp * Bp) / 12.0);

            sw.WriteLine("Iy = (Iyy * 10000 + a * 100 * (((S / 2) - (B / 2.0))^2))) * nb + ((tp * Bp * Bp * Bp) / 12.0)");
            sw.WriteLine();
            sw.WriteLine("   = ({0} * 10000 + {1} * 100 * ((({2} / 2) - ({3} / 2.0))^2))) * {4} + (({5} * {6} * {6} * {6}) / 12.0)",
                tabData.Iyy, a, vm_S, B, vm_nb, tp, Bp);
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iy);
            sw.WriteLine();

            double Ix = tabData.Ixx + tabData.Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * Math.Pow((tp / 2.0 + D / 2.0), 2.0);
            sw.WriteLine("Ix = Ixx + Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * ((tp / 2.0 + D / 2.0)^2)");
            sw.WriteLine("   = {0} + {0} + ({1} * {2} * {2} * {2} / 12.0) + {3} * {2} * (({2} / 2.0 + {3} / 2.0)^2)",
                tabData.Ixx, Bp, tp, D);

            double Area = n * a * 100 + Bp * tp;
            sw.WriteLine("Area = n * A * 100 + Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3}", n, tabData.Area, top_plate_width, top_plate_thickness);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            //sw.WriteLine("fy = {0} N/sq.mm.", fy);
            sw.WriteLine();
            double ly = vm_ly;
            sw.WriteLine();
            sw.WriteLine("ly = {0} * 1000 = {1} mm", vm_ly, ly);
            sw.WriteLine();

            double lamda = (0.85 * ly) / ry;
            sw.WriteLine("λ = 0.85 * ly / ry = 0.85 * {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, vm_cf);
            }
            else if (load_capt < vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, vm_cf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, vm_cf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Force");
            sw.WriteLine();
            double net_area = Area - n * (vm_bolt_dia + 1.5) * (tf + tp);
            sw.WriteLine("Net Area = A - nb*(dia+1.5) * (tf + tp)");
            sw.WriteLine("         = {0} - {1}*({2}+1.5) * ({3} + {4})",
                Area, vm_nb, vm_bolt_dia, tf, tp);
            sw.WriteLine("         = {0:f3} sq.mm", net_area);
            sw.WriteLine();

            load_capt = net_area * vm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, vm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }
            else if (load_capt < vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, vm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }




            #endregion
        }
        public void VerticalMember_Section2(StreamWriter sw)
        {
            double vm_cf, vm_TF, vm_ft, vm_ly, vm_S, vm_bolt_dia;
            int vm_nb1, vm_nb2;

            vm_cf = MyList.StringToDouble(txt_vm_cf.Text, 0.0);
            vm_TF = MyList.StringToDouble(txt_vm_tf.Text, 0.0);
            vm_ft = MyList.StringToDouble(txt_vm_ft.Text, 0.0);
            vm_ly = MyList.StringToDouble(txt_vm_ly.Text, 0.0);
            vm_S = MyList.StringToDouble(txt_vm_S.Text, 0.0);
            vm_bolt_dia = MyList.StringToDouble(txt_vm_bolt_dia.Text, 0.0);
            vm_nb1 = MyList.StringToInt(txt_vm_nb1.Text, 0);
            vm_nb2 = MyList.StringToInt(txt_vm_nb2.Text, 0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF VERTICAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Tensile Force = TF = {0} kN", vm_TF);
            sw.WriteLine("Compressive Force = cf = {0} kN", vm_cf);
            sw.WriteLine();
            vm_ly = vm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", vm_ly);
            sw.WriteLine("Diameter of Bolt = dia = {0} mm", vm_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", vm_nb1);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelChannelsRow tabData = tbl_rolledSteelChannels.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);

            int np = 2;
            int n = 2;

            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine();
            double a, D, t, B, tf;
            a = tabData.Area;
            D = tabData.Depth;
            B = tabData.FlangeWidth;
            t = tabData.WebThickness;
            tf = tabData.FlangeThickness;

            //sw.WriteLine("Ixx = {0:f3} ", tabData.Ixx);
            sw.WriteLine("Iyy = {0:f3} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("Web Thickness = t = {0:f3} mm.", tabData.WebThickness);
            //sw.WriteLine("Flange Width = B = {0:f3} ", tabData.FlangeWidth);
            //sw.WriteLine("Flange Thickness = tf = {0:f3} ", tabData.FlangeThickness);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = side_plate_width;
            double tp = side_plate_thickness;
            sw.WriteLine("Plates {0} * {1} * {2} ", n, Bp, tp);
            sw.WriteLine("Spacing = S = {0}", vm_S);
            sw.WriteLine("Side plate width = Bp = {0} mm", side_plate_width);
            sw.WriteLine("Side plate Thickness = tp = {0} mm", side_plate_thickness);
            sw.WriteLine("No of Side plate = np = {0} mm", n);
            sw.WriteLine();

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();

            double Iy = n * (tabData.Iyy * 10000 + a * 100 * (vm_S / 2) * (vm_S / 2)) +
                np * (Bp * tp + Math.Pow(((vm_S / 2.0) - t + (tp / 2.0)), 2.0));


            sw.WriteLine("Iy = n * (Iyy * 10^4 + a * 100 * (S / 2)^2) + np * (Bp * tp + ((dgm_S / 2.0) - t + (tp / 2.0))^2)");
            sw.WriteLine();
            sw.WriteLine("   = {0} * ({1} * 10^4 + {2} * 100 * ({3} / 2)^2) + {4} * ({5} * {6} + (({3} / 2.0) - {7} + ({6} / 2.0))^2)",
                n, tabData.Iyy, a, vm_S, np, Bp, tp, t);
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iy);
            sw.WriteLine();

            //double Ix = tabData.Ixx + tabData.Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * Math.Pow((tp / 2.0 + D / 2.0), 2.0);
            //sw.WriteLine("Ix = Ixx + Ixx + (Bp * tp * tp * tp / 12.0) + Bp * tp * ((tp / 2.0 + D / 2.0)^2)");
            //sw.WriteLine("   = {0} + {0} + ({1} * {2} * {2} * {2} / 12.0) + {3} * {2} * (({2} / 2.0 + {3} / 2.0)^2)",
            //    tabData.Ixx, Bp, tp, D);

            double Area = n * a * 100 + np * Bp * tp;
            sw.WriteLine("Area = n * A * 100 + np * Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4}", n, tabData.Area, np, Bp, tp);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            //sw.WriteLine("fy = {0} N/sq.mm.", fy);
            sw.WriteLine();
            double ly = vm_ly;
            sw.WriteLine();
            sw.WriteLine("Length of Member = ly = {0} * 1000 = {1} mm", vm_ly, ly);
            sw.WriteLine();

            double lamda = (0.85 * ly) / ry;
            sw.WriteLine("λ = 0.85 * ly / ry = 0.85 * {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, vm_cf);
            }
            else if (load_capt < vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, vm_cf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, vm_cf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Force");
            sw.WriteLine();
            double net_area = Area - n * (vm_bolt_dia + 1.5) * (tf + tp);
            sw.WriteLine("Net Area = A - nb*(dia+1.5) * (tf + tp)");
            sw.WriteLine("         = {0} - {1}*({2}+1.5) * ({3} + {4})",
                Area, vm_nb1, vm_bolt_dia, tf, tp);
            sw.WriteLine("         = {0:f3} sq.mm", net_area);
            sw.WriteLine();

            load_capt = net_area * vm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, vm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }
            else if (load_capt < vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, vm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }

            #endregion
        }
        public void VerticalMember_Section3(StreamWriter sw)
        {
            double vm_cf, vm_TF, vm_ft, vm_ly, vm_S, vm_bolt_dia;
            int vm_nb1, vm_nb2;

            vm_cf = MyList.StringToDouble(txt_vm_cf.Text, 0.0);
            vm_TF = MyList.StringToDouble(txt_vm_tf.Text, 0.0);
            vm_ft = MyList.StringToDouble(txt_vm_ft.Text, 0.0);
            vm_ly = MyList.StringToDouble(txt_vm_ly.Text, 0.0);
            vm_S = MyList.StringToDouble(txt_vm_S.Text, 0.0);
            vm_bolt_dia = MyList.StringToDouble(txt_vm_bolt_dia.Text, 0.0);
            vm_nb1 = MyList.StringToInt(txt_vm_nb1.Text, 0);
            vm_nb2 = MyList.StringToInt(txt_vm_nb2.Text, 0);
            //int nb1 = 2;
            //int nb2 = 4;


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF VERTICAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Tensile Force = TF = {0} kN", vm_TF);
            sw.WriteLine("Compressive Force = cf = {0} kN", vm_cf);
            sw.WriteLine("Permissible Tensile Force = ft = {0} N/sq.mm", vm_ft);
            sw.WriteLine();
            vm_ly = vm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", vm_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", vm_bolt_dia);
            sw.WriteLine("No of Bolt in Top Section = nb = {0}", vm_nb1);
            sw.WriteLine("No of Bolt in Side Section = nb = {0}", vm_nb2);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                 cmb_code1.Text,
                 MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);
            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double S = top_chord_S;
            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Spacing = S = {0} mm", S);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
                (tp * Bp * Bp * Bp / 12.0) * np +
                ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Iyy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2)",
                         n,
                        tabData.Iyy,
                        tabData.Area,
                        S,
                        tabData.Cxx,
                        tp, Bp, np,
                        ns, Bs, ts);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iyy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            double ly = vm_ly;
            sw.WriteLine();
            sw.WriteLine("Effective Length = ly = {0} mm", ly);
            sw.WriteLine();

            double lamda = (0.85 * ly) / ry;
            sw.WriteLine("λ = 0.85 * ly/ry = 0.85 * {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressing Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressing force OK", load_capt, vm_cf);
            }
            else if (load_capt < vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressing force NOT OK", load_capt, vm_cf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressing force OK", load_capt, vm_cf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Load Carrying Capasity");
            sw.WriteLine();

            double net_area = Area - vm_nb1 * (vm_bolt_dia + 1.5) * (tabData.Thickness + tp) - vm_nb2 * (vm_bolt_dia + 1.5) * (tabData.Thickness + ts);
            sw.WriteLine("Net Area of Section = A - nb1*(dia+1.5)*(t+tp)-nb2*(dia+1.5)*(t+ts)");
            sw.WriteLine("                    = {0} - {1}*({2}+1.5)*({3}+{4})-{5}*({2}+1.5)*({3}+{6})",
                Area, vm_nb1, vm_bolt_dia, tabData.Thickness, tp, vm_nb2, ts);


            load_capt = net_area * vm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, vm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }
            else if (load_capt < vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, vm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }
            #endregion
        }
        public void VerticalMember_Section4(StreamWriter sw)
        {
            double vm_cf, vm_TF, vm_ft, vm_ly, vm_S, vm_bolt_dia;
            int vm_nb1, vm_nb2;

            vm_cf = MyList.StringToDouble(txt_vm_cf.Text, 0.0);
            vm_TF = MyList.StringToDouble(txt_vm_tf.Text, 0.0);
            vm_ft = MyList.StringToDouble(txt_vm_ft.Text, 0.0);
            vm_ly = MyList.StringToDouble(txt_vm_ly.Text, 0.0);
            vm_S = MyList.StringToDouble(txt_vm_S.Text, 0.0);
            vm_bolt_dia = MyList.StringToDouble(txt_vm_bolt_dia.Text, 0.0);
            vm_nb1 = MyList.StringToInt(txt_vm_nb1.Text, 0);
            vm_nb2 = MyList.StringToInt(txt_vm_nb2.Text, 0);

            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF VERTICAL MEMBER    SELECTED SECTION - {0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", vm_TF);
            sw.WriteLine("Design Compressive Force = cf = {0} kN", vm_cf);
            sw.WriteLine("Permissible Tensile Force = ft = {0} N/sq.mm", vm_ft);
            sw.WriteLine();
            vm_ly = vm_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", txt_dgm_ly.Text, vm_ly);
            sw.WriteLine("Diameter of Bolt = dia = {0} mm", vm_bolt_dia);
            sw.WriteLine("No of Bolt in Top Section = nb1 = {0}", vm_nb1);
            sw.WriteLine("No of Bolt in Side Section = nb2 = {0}", vm_nb2);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                 cmb_code1.Text,
                 MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);
            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double Bss = vertical_stiff_plate_width;
            double tss = vertical_stiff_plate_thickness;
            int nss = 2;



            double S = vm_S;
            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Vertical Stiffener plate width = Bss = {0} mm", Bss);
            sw.WriteLine("Vertical Stiffener plate Thickness = tss = {0} mm", tss);
            sw.WriteLine("No of Vertical Stiffener plate = nss = {0}", nss);
            sw.WriteLine("Spacing = S = {0} mm", S);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
                (tp * Bp * Bp * Bp / 12.0) * np +
                ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) +
                nss * ((Bss * tss * tss * tss / 12) + Bss * tss * Math.Pow(((S / 2.0) - tabData.Thickness - (tss / 2.0)), 2.0));
            sw.WriteLine("Iy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2) + {11}*(({12}*{13}*{13}*{13}/12) + {12}*{13} * ({3}/2 - {14} - {13}/2)^2)",
                        n,
                        tabData.Iyy,
                        tabData.Area,
                        S,
                        tabData.Cxx,
                        tp, Bp, np,
                        ns, Bs, ts,
                        nss, Bss, tss, tabData.Thickness);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns, Bss, tss, nss);
            sw.WriteLine("     = {0:f3} sq.mm", Area);

            double ry = Math.Sqrt(Iyy / Area);
            sw.WriteLine();
            sw.WriteLine("ry = SQRT(Iyy/Area)");
            sw.WriteLine("   = SQRT({0:f3}/{1:f3})", Iyy, Area);
            sw.WriteLine("   = {0:f3} mm", ry);

            sw.WriteLine();
            //sw.WriteLine("Distance Between Rakers = {0} m", );
            double ly = vm_ly;
            sw.WriteLine();
            sw.WriteLine("ly = {0} * 1000 = {1} mm", top_chord_dr, ly);
            sw.WriteLine();

            double lamda = (0.85 * ly) / ry;
            sw.WriteLine("λ = 0.85 * ly/ry = 0.85 * {0:f3}/{1:f3} = {2:f3}", ly, ry, lamda);
            sw.WriteLine();
            sw.WriteLine("fy = {0} N/sq.mm", fy);
            sw.WriteLine();
            //double sigma_ac = 128.026;
            double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            sw.WriteLine();

            double load_capt = sigma_ac * Area;

            sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", sigma_ac, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressive force OK", load_capt, vm_cf);
            }
            else if (load_capt < vm_cf)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressive force NOT OK", load_capt, vm_cf);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressive force OK", load_capt, vm_cf);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Load Carrying Capasity");
            sw.WriteLine();

            double net_area = Area - vm_nb1 * (vm_bolt_dia + 1.5) * (tabData.Thickness + tp) - vm_nb2 * (vm_bolt_dia + 1.5) * (tabData.Thickness + ts + tss);
            sw.WriteLine("Net Area of Section = A - nb1*(dia+1.5)*(t+tp)-nb2*(dia+1.5)*(t+ts+tss)");
            sw.WriteLine("                    = {0} - {1}*({2}+1.5)*({3}+{4})-{5}*({2}+1.5)*({3}+{6}+{7})",
                Area, vm_nb1, vm_bolt_dia, tabData.Thickness, tp, vm_nb2, ts, tss);
            sw.WriteLine("                    = {0} sq.mm", net_area);


            load_capt = net_area * vm_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, vm_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }
            else if (load_capt < vm_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, vm_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, vm_TF);
            }
            #endregion
        }

        #endregion VERTICAL MEMBER

        #region BOTTOM CHORD MEMBER
        public void BottomChordMember(StreamWriter sw)
        {
            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    BottomChordMember_Section1(sw);
                    break;
                case 1:
                    BottomChordMember_Section2(sw);
                    break;
                case 2:
                    BottomChordMember_Section3(sw);
                    break;
                case 3:
                    BottomChordMember_Section4(sw);
                    break;
            }

        }
        public void BottomChordMember_Section1(StreamWriter sw)
        {

            double bc_TF, bc_ly, bc_ft, bc_bolt_dia;
            int bc_nb;

            bc_TF = MyList.StringToDouble(txt_bc_tf.Text, 0.0);
            bc_ft = MyList.StringToDouble(txt_bc_ft.Text, 0.0);
            bc_ly = MyList.StringToDouble(txt_bc_ly.Text, 0.0);
            bc_bolt_dia = MyList.StringToDouble(txt_bc_bolt_dia.Text, 0.0);
            bc_nb = MyList.StringToInt(txt_bc_nb1.Text, 0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF BOTTOM CHORD MEMBER   SELECTED SECTION-{0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = tf = {0} kN", bc_TF);
            sw.WriteLine();
            bc_ly = bc_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", bc_ly);
            sw.WriteLine("Diameter of Bolt = dia = {0} mm", bc_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", bc_nb);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelBeamsRow tabData = tbl_rolledSteelBeams.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);

            int n = 2;
            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine("Top plate width = Bp = {0} mm", top_plate_width);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", top_plate_thickness);
            sw.WriteLine();
            double a, D, t, B, tf;

            a = tabData.Area;
            D = tabData.Depth;
            B = tabData.FlangeWidth;
            t = tabData.WebThickness;
            tf = tabData.FlangeThickness;

            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Flange Thickness = tf = {0:f3} ", tabData.FlangeThickness);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();


            double Bp = top_plate_width;
            double tp = top_plate_thickness;

            double Area = n * a * 100 + Bp * tp - bc_nb * (bc_bolt_dia + 1.5) * (tf + tp);
            sw.WriteLine("Area = n * a * 100 + Bp * tp - nb * (dia + 1.5) * (tf * tp)");
            sw.WriteLine("     = {0} * {1} * 100 + {2} * {3} - {4} * ({5} + 1.5) * ({6} * {3})",
                n, a, Bp, tp, bc_nb, bc_bolt_dia, tf);
            sw.WriteLine("     = {0:f3} sq.mm", Area);



            double load_capt = Area * bc_ft;

            sw.WriteLine("Tensile Load Carrying Capacity = Area * ft");
            sw.WriteLine("                       = {0} * {1}", Area, bc_ft);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, bc_TF);
            }
            else if (load_capt < bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, bc_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, bc_TF);
            }
            #endregion
        }
        public void BottomChordMember_Section2(StreamWriter sw)
        {
            double bc_TF, bc_ft, bc_ly, bc_S, bc_bolt_dia;
            int bc_nb1, bc_nb2;

            bc_TF = MyList.StringToDouble(txt_bc_tf.Text, 0.0);
            bc_ft = MyList.StringToDouble(txt_bc_ft.Text, 0.0);
            bc_ly = MyList.StringToDouble(txt_bc_ly.Text, 0.0);
            bc_bolt_dia = MyList.StringToDouble(txt_bc_bolt_dia.Text, 0.0);
            bc_nb1 = MyList.StringToInt(txt_bc_nb1.Text, 0);
            bc_nb2 = MyList.StringToInt(txt_bc_nb2.Text, 0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF BOTTOM CHORD MEMBER   SELECTED SECTION-{0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design Tensile Force = TF = {0} kN", bc_TF);
            sw.WriteLine();
            bc_ly = bc_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", bc_ly);
            sw.WriteLine("Diameter of Bolt = bdia = {0} mm", bc_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", bc_nb1);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelChannelsRow tabData = tbl_rolledSteelChannels.GetDataFromTable(cmb_section_name.Text, cmb_code1.Text);

            int np = 2;
            int n = 2;

            sw.WriteLine("{0} X {1} {2}", n, cmb_section_name.Text, cmb_code1.Text);

            sw.WriteLine();
            sw.WriteLine();
            double a, D, t, B, tf;
            a = tabData.Area;
            D = tabData.Depth;
            B = tabData.FlangeWidth;
            t = tabData.WebThickness;
            tf = tabData.FlangeThickness;

            //sw.WriteLine("Ixx = {0:f3} ", tabData.Ixx);
            sw.WriteLine("Iyy = {0:f3} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = A = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Depth = D = {0:f3} mm.", tabData.Depth);
            sw.WriteLine("Web Thickness = t = {0:f3} mm.", tabData.WebThickness);
            //sw.WriteLine("Flange Width = B = {0:f3} ", tabData.FlangeWidth);
            //sw.WriteLine("Flange Thickness = tf = {0:f3} ", tabData.FlangeThickness);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = side_plate_width;
            double tp = side_plate_thickness;
            sw.WriteLine("Plates {0} * {1} * {2} ", n, Bp, tp);
            sw.WriteLine("Side plate width = Bp = {0} mm", side_plate_width);
            sw.WriteLine("Side plate Thickness = tp = {0} mm", side_plate_thickness);
            sw.WriteLine("No of Side plate = np = {0} mm", n);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();


            double Area = n * a * 100 + np * Bp * tp;
            sw.WriteLine("Area = n * A * 100 + Bp * tp");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3}", n, tabData.Area, Bp, tp);
            sw.WriteLine("     = {0:f3} sq.mm", Area);


            sw.WriteLine();
            //sw.WriteLine("fy = {0} N/sq.mm.", fy);
            sw.WriteLine();
            double ly = bc_ly;
            sw.WriteLine();
            sw.WriteLine("Length of Member = ly = {0} mm", ly);
            sw.WriteLine();

            double load_capt = bc_ft * Area;
            sw.WriteLine("Check for Tensile Force");
            sw.WriteLine();
            double net_area = Area - bc_nb1 * (bc_bolt_dia + 1.5) * (tf + tp);
            sw.WriteLine("Net Area = A - nb*(dia+1.5) * (tf + tp)");
            sw.WriteLine("         = {0} - {1}*({2}+1.5) * ({3} + {4})",
                Area, bc_nb1, bc_bolt_dia, tf, tp);
            sw.WriteLine("         = {0:f3} sq.mm", net_area);
            sw.WriteLine();

            load_capt = net_area * bc_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, bc_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, bc_TF);
            }
            else if (load_capt < bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, bc_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, bc_TF);
            }

            #endregion
        }
        public void BottomChordMember_Section3(StreamWriter sw)
        {
            double bc_TF, bc_ft, bc_ly, bc_bolt_dia;
            int bc_nb1, bc_nb2;

            bc_TF = MyList.StringToDouble(txt_bc_tf.Text, 0.0);
            bc_ft = MyList.StringToDouble(txt_bc_ft.Text, 0.0);
            bc_ly = MyList.StringToDouble(txt_bc_ly.Text, 0.0);
            bc_bolt_dia = MyList.StringToDouble(txt_bc_bolt_dia.Text, 0.0);
            bc_nb1 = MyList.StringToInt(txt_bc_nb1.Text, 0);
            bc_nb2 = MyList.StringToInt(txt_bc_nb2.Text, 0);
            //int nb1 = 2;
            //int nb2 = 4;


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF BOTTOM CHORD MEMBER   SELECTED SECTION-{0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Tensile Force = TF = {0} kN", bc_TF);
            sw.WriteLine("Permissible Tensile Force = ft = {0} N/sq.mm", bc_ft);
            sw.WriteLine();
            bc_ly = bc_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", bc_ly);
            sw.WriteLine("Diameter of Bolt = dia = {0} mm", bc_bolt_dia);
            sw.WriteLine("No of Bolt in Top Section = nb1 = {0}", bc_nb1);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                 cmb_code1.Text,
                 MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);
            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double S = top_chord_S;
            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Spacing = S = {0} mm", S);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            //double Iyy = n * (tabData.Iyy * 10000 + tabData.Area * 100 * Math.Pow((S / 2.0) - ((tabData.Cxx * 10 / 2.0)), 2.0)) +
            //    (tp * Bp * Bp * Bp / 12.0) * np +
            //    ns * ((Bs * ts * ts * ts / 12) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0));

            //sw.WriteLine("Iyy = n * (Iyy * 10^4 + A * 100 * (S/2 - (Cxx*10/2))^2) + (tp*Bp*Bp*Bp/12)*np + ns*((Bs*ts*ts*ts/12) + Bs*ts * (S/2 + ts/2)^2)");
            //sw.WriteLine("    = {0} * ({1} * 10^4 + {2} * 100 * ({3}/2 - ({4}*10/2))^2) + ({5}*{6}*{6}*{6}/12)*{7} + {8}*(({9}*{10}*{10}*{10}/12) + {9}*{10} * ({3}/2 + {10}/2)^2)",
            //                                                                                                                                ns, Bs, ts);
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("  = {0:f3} sq.sq.mm", Iyy);
            //sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns);
            sw.WriteLine("     = {0:f3} sq.mm", Area);


            sw.WriteLine();
            double ly = bc_ly;
            sw.WriteLine();
            sw.WriteLine("Effective Length = ly = {0} mm", ly);
            sw.WriteLine();


            double load_capt = bc_ft * Area;

            sw.WriteLine("Compressing Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", bc_ft, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Compressing force OK", load_capt, bc_TF);
            }
            else if (load_capt < bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Compressing force NOT OK", load_capt, bc_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Compressing force OK", load_capt, bc_TF);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Load Carrying Capasity");
            sw.WriteLine();

            double net_area = Area - bc_nb1 * (bc_bolt_dia + 1.5) * (tabData.Thickness + tp) - bc_nb2 * (bc_bolt_dia + 1.5) * (tabData.Thickness + ts);
            sw.WriteLine("Net Area of Section = A - nb1*(dia+1.5)*(t+tp)-nb2*(dia+1.5)*(t+ts)");
            sw.WriteLine("                    = {0} - {1}*({2}+1.5)*({3}+{4})-{5}*({2}+1.5)*({3}+{6})",
                Area, bc_nb1, bc_bolt_dia, tabData.Thickness, tp, bc_nb2, ts);


            load_capt = net_area * bc_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, bc_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design Tensile force OK", load_capt, bc_TF);
            }
            else if (load_capt < bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design Tensile force NOT OK", load_capt, bc_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design Tensile force OK", load_capt, bc_TF);
            }
            #endregion
        }
        public void BottomChordMember_Section4(StreamWriter sw)
        {
            double bc_cf, bc_TF, bc_ft, bc_ly, bc_S, bc_bolt_dia;
            int bc_nb1, bc_nb2;

            bc_TF = MyList.StringToDouble(txt_bc_tf.Text, 0.0);
            bc_ft = MyList.StringToDouble(txt_bc_ft.Text, 0.0);
            bc_ly = MyList.StringToDouble(txt_bc_ly.Text, 0.0);
            bc_bolt_dia = MyList.StringToDouble(txt_bc_bolt_dia.Text, 0.0);
            bc_nb1 = MyList.StringToInt(txt_bc_nb1.Text, 0);
            bc_nb2 = MyList.StringToInt(txt_bc_nb2.Text, 0);

            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF BOTTOM CHORD MEMBER   SELECTED SECTION-{0}, FIG {0}", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2 : FORCE DATA");
            sw.WriteLine("-------------------");
            sw.WriteLine();
            sw.WriteLine("Tensile Force = TF = {0} kN", bc_TF);
            sw.WriteLine("Permissible Tensile Force = ft = {0} N/sq.mm", bc_ft);
            sw.WriteLine();
            bc_ly = bc_ly * 1000.0;
            sw.WriteLine("Length of Member = ly = {0} mm", txt_bc_ly.Text, bc_ly);
            sw.WriteLine("Diameter of Bolt = dia = {0} mm", bc_bolt_dia);
            sw.WriteLine("No of Bolt in a Section = nb = {0}", bc_nb1);
            sw.WriteLine();

            //double Ar = (top_chord_mf * 1000.0) / fc;
            //sw.WriteLine("Area Require = Ar = ({0} * 1000.0) / {1}", top_chord_mf, fc);
            //sw.WriteLine("                  = {0:f2} sq.mm.", Ar);
            //sw.WriteLine();
            //sw.WriteLine("Selected Section Fig {0} ", (cmb_sections_define.SelectedIndex + 1));
            sw.WriteLine();

            RolledSteelAnglesRow tabData = tbl_rolledSteelAngles.GetDataFromTable(cmb_section_name.Text,
                 cmb_code1.Text,
                 MyList.StringToDouble(cmb_sec_thk.Text, 0));

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3 : SELECTED SECTION DATA");
            sw.WriteLine("------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} X {1} {2} {3}", n, cmb_section_name.Text,
                cmb_code1.Text,
                cmb_sec_thk.Text);
            sw.WriteLine();
            sw.WriteLine("n = 4");
            sw.WriteLine("Iyy = {0} sq.sq.cm", tabData.Iyy);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("t = {0} mm", tabData.Thickness);
            sw.WriteLine("Cxx = {0:f3} ", tabData.Cxx);
            sw.WriteLine();
            sw.WriteLine();
            double Bp = top_plate_width;
            double tp = top_plate_thickness;
            int np = 1;
            double Bs = side_plate_width;
            double ts = side_plate_thickness;
            int ns = 2;
            double Bss = vertical_stiff_plate_width;
            double tss = vertical_stiff_plate_thickness;
            int nss = 2;



            sw.WriteLine("Top plate width = Bp = {0} mm", Bp);
            sw.WriteLine("Top plate Thickness = tp = {0} mm", tp);
            sw.WriteLine("No of Top plate = np = {0}", np);
            sw.WriteLine("Side plate width = Bs = {0} mm", Bs);
            sw.WriteLine("Side plate Thickness = ts = {0} mm", ts);
            sw.WriteLine("No of Side plate = ns = {0}", ns);
            sw.WriteLine("Vertical Stiffener plate width = Bss = {0} mm", Bss);
            sw.WriteLine("Vertical Stiffener plate Thickness = tss = {0} mm", tss);
            sw.WriteLine("No of Vertical Stiffener plate = nss = {0}", nss);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : DESIGN CALCULATION");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine();

            double Area = n * tabData.Area * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss;
            sw.WriteLine("Area = n * A * 100 + Bp * tp * np + Bs * ts * ns + Bss * tss * nss");
            sw.WriteLine("     = {0} * {1:f3} * 100 + {2} * {3} * {4} +  {5} * {6} * {7} ", n, tabData.Area, Bp, tp, np, Bs, ts, ns, Bss, tss, nss);
            sw.WriteLine("     = {0:f3} sq.mm", Area);


            sw.WriteLine();
            //sw.WriteLine("Distance Between Rakers = {0} m", );
            double ly = bc_ly;
            sw.WriteLine();
            sw.WriteLine("ly = {0} * 1000 = {1} mm", top_chord_dr, ly);
            sw.WriteLine();

            //double sigma_ac = 128.026;
            //double sigma_ac = Get_Table_1_Sigma_Value(lamda, fy);
            //sw.WriteLine("From Table 1, σ_ac =  {0:f3} N/sq.mm", sigma_ac);
            //sw.WriteLine();

            double load_capt = bc_ft * Area;

            sw.WriteLine("Load Carrying Capacity = σ_ac * Area ");
            sw.WriteLine("                       = {0} * {1}", bc_ft, Area);
            sw.WriteLine("                       = {0:f3} N", load_capt);

            load_capt = load_capt / 1000.0;
            if (load_capt > bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN, Design force OK", load_capt, bc_TF);
            }
            else if (load_capt < bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN, Design force NOT OK", load_capt, bc_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN, Design force OK", load_capt, bc_TF);
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Check for Tensile Load Carrying Capasity");
            sw.WriteLine();

            double net_area = Area - bc_nb1 * (bc_bolt_dia + 1.5) * (tabData.Thickness + tp) - bc_nb2 * (bc_bolt_dia + 1.5) * (tabData.Thickness + ts + tss);
            sw.WriteLine("Net Area of Section = A - nb1*(dia+1.5)*(t+tp)-nb2*(dia+1.5)*(t+ts+tss)");
            sw.WriteLine("                    = {0} - {1}*({2}+1.5)*({3}+{4})-{5}*({2}+1.5)*({3}+{6}+{7})",
                Area, bc_nb1, bc_bolt_dia, tabData.Thickness, tp, bc_nb2, ts, tss);


            load_capt = net_area * bc_ft;
            sw.WriteLine("Tensile Load Carrying Capasity  = {0} * {1}", net_area, bc_ft);
            sw.WriteLine("                                = {0} N", load_capt);
            load_capt = load_capt / 1000.0;
            if (load_capt > bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN > {1:f3} kN,Design Tensile force OK", load_capt, bc_TF);
            }
            else if (load_capt < bc_TF)
            {
                sw.WriteLine("                       = {0:f3} kN < {1:f3} kN,Design  Tensile force NOT OK", load_capt, bc_TF);
            }
            else
            {
                sw.WriteLine("                       = {0:f3} kN = {1:f3} kN,Design  Tensile force OK", load_capt, bc_TF);
            }
            #endregion
        }

        #endregion VERTICAL MEMBER

        #region Stringer Beam

        public void StringerBeam(StreamWriter sw)
        {
            double M1, F1, M2, F2, IL, sigma_b, sigma_c;

            M1 = MyList.StringToDouble(txt_M1_stringer_beam.Text, 0.0);
            F1 = MyList.StringToDouble(txt_F1_stringer_beam.Text, 0.0);
            M2 = MyList.StringToDouble(txt_M2_stringer_beam.Text, 0.0);
            F2 = MyList.StringToDouble(txt_F2_stringer_beam.Text, 0.0);
            IL = MyList.StringToDouble(txt_IL_stringer_beam.Text, 1.0);
            sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
            sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);


            #region STEP 1 : General Calculation
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("DESIGN OF STRINGER BEAM");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("STEP 2: MOMENT & FORCE  DATA");
            sw.WriteLine("----------------------------");
            sw.WriteLine();
            sw.WriteLine("Fixed Load Bending Moment = M1 = {0} kN-m", M1);
            sw.WriteLine("Fixed Load Shear Force = F1 = {0} kN", F1);
            sw.WriteLine("Maximum Live Load Bending Moment = M2 = {0} kN-m", M2);
            sw.WriteLine("Maximum Live Load Shear Force = F2 = {0} kN", F2);
            sw.WriteLine("Impact Load = IL = {0}%", IL);
            sw.WriteLine("Permissible Bending Stress in Steel = σ_b = {0} N/sq.mm", sigma_b);
            sw.WriteLine("Permissible Shear Stress in Steel  = σ_c = {0} N/sq.mm", sigma_c);
            sw.WriteLine();
            sw.WriteLine("STEP 3: SELECTED SECTION DATA");
            sw.WriteLine("-----------------------------");
            sw.WriteLine();

            RolledSteelBeamsRow tabData = tbl_rolledSteelBeams.GetDataFromTable(cmb_sb_section_name.Text, txt_sb_section_code.Text);

            int n = 4;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0} {1}", cmb_sb_section_name.Text, txt_sb_section_code.Text);
            sw.WriteLine();
            sw.WriteLine("t = {0} mm", tabData.WebThickness);
            sw.WriteLine("D = {0} mm", tabData.Depth);
            sw.WriteLine("Area = a = {0:f3} sq.cm.", tabData.Area);
            sw.WriteLine("Zxx = {0:f3} cu.cm", tabData.Zxx);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4: DESIGN CALCULATION");
            sw.WriteLine("--------------------------");
            sw.WriteLine();
            sw.WriteLine();

            double M = M1 + M2 + (IL / 100.0) * M2;
            sw.WriteLine("Design Bending Moment = M = M1 + M2 + (IL/100)*M2");
            sw.WriteLine("                      = {0} + {1} + ({2}/100)*{1}", M1, M2, IL);
            sw.WriteLine("                      = {0:f3} kN-m", M);
            sw.WriteLine();
            sw.WriteLine();

            double F = F1 + F2 + (IL / 100.0) * F2;
            sw.WriteLine("Design Shear Force = F = F1 + F2 + (IL / 100.0) * F2");
            sw.WriteLine("                   = {0} + {1} + ({2} / 100.0) * {1}", F1, F2, IL);
            sw.WriteLine("                   = {0:f3} kN", F);
            sw.WriteLine();
            sw.WriteLine();

            double Zr = (M * 10E5) / sigma_b;
            sw.WriteLine("Required Section Modulus = Zr = M/σ_b");
            sw.WriteLine("                         = {0:f3}*10^6/{1}", M, sigma_b);
            sw.WriteLine("                         = {0:f3} cu.mm", M, sigma_b);
            sw.WriteLine();
            double Z = tabData.Zxx * 1000;
            if (Z > Zr)
            {
                sw.WriteLine("For {0} {1} Section Modulus = z = {2:f3} cu.mm > {3:f3} cu.mm (Zr), So, OK",
                    cmb_sb_section_name.Text, txt_sb_section_code.Text, Z, Zr);
            }
            else
            {
                sw.WriteLine("For {0} {1} Section Modulus = z = {2:f3} cu.mm < {3:f3} cu.mm (Zr), So, NOT OK",
                    cmb_sb_section_name.Text, txt_sb_section_code.Text, Z, Zr);
            }

            double app_SF = (F * 1000.0) / (tabData.WebThickness * tabData.Depth);
            sw.WriteLine();
            sw.WriteLine("Applied Shear Force = F*1000/(t*D)");
            sw.WriteLine("                    = {0:f3}*1000/({1:f3}*{2:f3})", F, tabData.WebThickness, tabData.Depth);
            if (app_SF < sigma_c)
            {
                sw.WriteLine("                    = {0:f3} N/sq.mm < {1:f3} N/sq.mm, So, OK", app_SF, sigma_c);
            }
            else
            {
                sw.WriteLine("                    = {0:f3} N/sq.mm > {1:f3} N/sq.mm, So, NOT OK", app_SF, sigma_c);
            }
            #endregion

        }
        #endregion Stringer Beam

        #region Cross Grider
        public void CrossGirder(StreamWriter sw)
        {
            double Lc, M1, V1, M2, V2, IF, sigma_b, T, tw, tf;

            Lc = MyList.StringToDouble(txt_cg_L.Text, 0.0);
            M1 = MyList.StringToDouble(txt_cg_M1.Text, 0.0);
            V1 = MyList.StringToDouble(txt_cg_V1.Text, 0.0);
            M2 = MyList.StringToDouble(txt_cg_M2.Text, 0.0);
            V2 = MyList.StringToDouble(txt_cg_V2.Text, 0.0);
            IF = MyList.StringToDouble(txt_cg_IF.Text, 1.0);
            sigma_b = MyList.StringToDouble(txt_cg_sigma_b.Text, 0.0);
            T = MyList.StringToDouble(txt_cg_sigma_c.Text, 0.0);
            tw = MyList.StringToDouble(txt_cg_tw.Text, 0.0);
            tf = MyList.StringToDouble(txt_cg_tf.Text, 0.0);

            sw.WriteLine("STEP 2: MOMENT & FORCE DATA");
            sw.WriteLine("---------------------------");
            sw.WriteLine();
            sw.WriteLine("Legth of Cross Girder = Lc = {0} m", Lc);
            sw.WriteLine("Fixed/Dead Load Bending Moment = M1 = {0} kN-m", M1);
            sw.WriteLine("Fixed/Dead Load Shear Force = V1 = {0} kN", V1);
            sw.WriteLine("Live/Moving Load Bending Moment = M2 = {0} kN-m", M2);
            sw.WriteLine("Live/Moving Load Shear Force = V2 = {0} kN", V2);
            sw.WriteLine("Impact Factor = IF = {0}% ", IF);
            sw.WriteLine("Permissible Bending Stress = σ_b = {0} N/sq.mm", sigma_b);
            sw.WriteLine("Permissible Shear Stress = T = {0} N/sq.mm", T);
            sw.WriteLine("Thickness of Web Plate = tw = {0} mm", tw);
            sw.WriteLine("Thickness of Flange = tf = {0} mm", tf);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 3: TRIAL SECTION OF PLATE GIRDER");
            sw.WriteLine("-------------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            double M = M1 + (1 + (IF / 100.0)) * M2;
            sw.WriteLine("Total Design Bending Moment = M = M1 + {0:f2} * M2", (1 + (IF / 100.0)));
            sw.WriteLine("                            = {0} + {1} * {2}", M1, (1 + (IF / 100.0)), M2);
            sw.WriteLine("                            = {0:f3} kN-m", M);
            sw.WriteLine();
            double V = V1 + (1 + (IF / 100.0)) * V2;
            sw.WriteLine("Total Design Shear Moment = V = V1 + {0:f2} * V2", (1 + (IF / 100.0)));
            sw.WriteLine("                            = {0} + {1} * {2}", V1, (1 + (IF / 100.0)), V2);
            sw.WriteLine("                            = {0:f3} kN", V);
            sw.WriteLine();

            double eco_dep = 5 * Math.Pow(((M * 10E5) / 150.0), (1.0 / 3.0));
            sw.WriteLine("Economic depth = 5 * ((M * 10^6 )/σ_b)^(1/3)");
            sw.WriteLine("               = 5 * (({0} * 10^6 )/{1})^(1/3)", M, sigma_b);
            sw.WriteLine("               = {0:f2} mm", eco_dep);
            sw.WriteLine();
            sw.WriteLine("Web depth based on shear considerations assuming {0} mm thick plate ", tw);

            double ass_shr = (V * 1000) / (T * tw);
            sw.WriteLine("        = (V x 1000)/(T x t) ");
            sw.WriteLine("        = ({0} x 1000)/({1} x {2}) ", V, T, tw);
            sw.WriteLine("        = {0:f2}  mm.", ass_shr);
            sw.WriteLine();
            sw.WriteLine();

            double dw;
            dw = 1000.0;
            sw.WriteLine("Let us adopt the Web as {0} mm. x {1} mm.", dw, tw);
            sw.WriteLine();
            sw.WriteLine("where, dw = {0} mm. tw = {1} mm.", dw, tw);
            double Aw = dw * tw;
            sw.WriteLine("Aw = {0:f2} sq.mm.", Aw);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 4 : FLANGE PLATES");
            sw.WriteLine("----------------------");
            sw.WriteLine();

            double Af = ((M * 10E5) / (sigma_b * dw)) - Aw / 6.0;
            sw.WriteLine("Area of Flange Plates = Af = ((M  * 10^6 )/ (σ_b * dw)) - Aw/6 ");
            sw.WriteLine("                           = ({0} * 10^6 )/({1} * {2}) - ({3})/6",
                                                        M, sigma_b, dw, Aw);
            sw.WriteLine("                           = {0:f3} sq.mm.", Af);
            sw.WriteLine();
            sw.WriteLine();

            double bf = (Lc * 1000.0) / 40.0;

            //sw.WriteLine("Flange width = bf = L/40 to L/45 = 8.7 x 1000 / 40 = 217 mm.");
            sw.WriteLine("Flange width = bf = L/40");
            sw.WriteLine("             = {0} * 1000 / 40", Lc);
            sw.WriteLine("             = {0} mm.", bf);
            sw.WriteLine();
            if (bf < 400)
            {
                bf = 400.0;
            }
            else
            {
                bf = (int)(bf / 100);
                bf = bf + 1;
                bf = (bf * 100);
            }

            sw.WriteLine("Let us adopt the Flange as {0} mm. x {1} mm.", bf, tf);
            sw.WriteLine("Where, bf = {0} mm. tf = {1} mm.", bf, tf);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 5 : CHECK FOR MAXIMUM STRESSES");
            sw.WriteLine("-----------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double I = (bf * Math.Pow((dw + tf + tf), 3.0) / 12.0) - ((bf - tw) * dw * dw * dw / 12.0);
            sw.WriteLine("                    3                     3");
            sw.WriteLine("I = (bf * (dw+tf+tf)^3 / 12) - ((bf-tw) * dw * dw * dw / 12)");
            sw.WriteLine("  = ({0} * ({1}+{2}+{2})^3 / 12) - (({0}-{3}) * {1} * {1} * {1} / 12)",
                bf, dw, tf, tw);
            sw.WriteLine("  = {0:f3}  sq.sq.mm", I);
            sw.WriteLine();
            sw.WriteLine();

            double sigma = M / I;
            sw.WriteLine("SIGMA = M / I");
            sw.WriteLine("      = {0}/{1}", M, I);
            if (sigma < sigma_b)
            {
                sw.WriteLine("      = {0:f3} N/sq.mm. < {1:f3} N/sq.mm = σ_b ", sigma, sigma_b);
            }
            else if (sigma > sigma_b)
            {
                sw.WriteLine("      = {0:f3} N/sq.mm. > {1:f3} N/sq.mm = σ_b ", sigma, sigma_b);
            }
            else
            {
                sw.WriteLine("      = {0:f3} N/sq.mm. = {1:f3} N/sq.mm = σ_b ", sigma, sigma_b);
            }
            sw.WriteLine();
            sw.WriteLine();

            double avg_shr_stress = (V * 1000) / (dw * tw);
            sw.WriteLine("Average Shear Stress = (V * 1000)/ (dw * tw)");
            sw.WriteLine("                     = ({0} * 1000) / ({1} * {2})", V, dw, tw);
            sw.WriteLine("                     = ({0} N/sq.mm", avg_shr_stress);
            sw.WriteLine();
            sw.WriteLine();
            double Tv1 = dw / tw;
            sw.WriteLine("Permissible Average Shear Stress depends on the ration = Tv1 = (dw/tw) ");
            sw.WriteLine("                                                       = ({0}/{1})", dw, tw);
            sw.WriteLine("                                                       = {0:f2} ", Tv1);
            sw.WriteLine();
            sw.WriteLine();
            double C = dw;
            sw.WriteLine("Stiffener spacing = C = dw = {0} mm.", C);
            sw.WriteLine();
            sw.WriteLine();
            double Tv2 = Get_Table_3_Value(Tv1, 0.9);
            if (Tv2 < Tv1)
                sw.WriteLine("From Table 2, Tv2 = {0} N/sq.mm < Tv1 So OK. ", Tv2);
            else if (Tv2 > Tv1)
                sw.WriteLine("From Table 2, Tv2 = {0} N/sq.mm > Tv1 So NOT OK. ", Tv2);
            else if (Tv2 == Tv1)
                sw.WriteLine("From Table 2, Tv2 = {0} N/sq.mm = Tv1 So OK. ", Tv2);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 5 : CONNECTIONS BETWEEN FLANGE AND WEB");
            sw.WriteLine("-------------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double max_shr_frc = ((V * 1000) * (bf * tf) * ((dw / 2) + (tf / 2))) / I;

            sw.WriteLine("Maximum Shear Force at the junction of Web and Flange is given by,");
            sw.WriteLine("      T = ((V * 1000) * (bf * tf) * ((dw / 2) + (tf / 2))) / I");
            sw.WriteLine("        = (({0} * 1000) * ({1} * {2}) * (({3} / 2) + ({2} / 2))) / {4}",
                V, bf, tf, dw, I);
            sw.WriteLine("        = {0:f2} N/sq.mm.", max_shr_frc);
            sw.WriteLine();
            sw.WriteLine();

            double s = max_shr_frc / 145.0;
            sw.WriteLine("Assuming continuous weld on either side, strength of weld of size 's',");
            sw.WriteLine("                         = 2 x 0.7 x s x 102.5");
            sw.WriteLine("                         = 145 x s = T = {0:f2}", max_shr_frc);
            sw.WriteLine();
            sw.WriteLine("                       s = {0:f3} / 145 = {1:f3} mm.", max_shr_frc, s);
            sw.WriteLine();
            sw.WriteLine();

            if (s < 5.0)
                s = 5.0;
            else
            {
                s = (int)s;
                s = s + 1;
            }
            sw.WriteLine("Let us adopt {0} mm. fillet weld on either side.", s);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 6 : INTERMEDIATE STIFFENERS");
            sw.WriteLine("--------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            if ((dw / tw) >= T)
                sw.WriteLine("dw/tw = {0}/{1} = {2:f3} > {3}, So Vertical Stiffeners are Required,", dw, tw, (dw / tw), T);
            else if ((dw / tw) < T)
                sw.WriteLine("dw/tw = {0}/{1} = {2:f3} > {3}, So Vertical Stiffeners are not Required,", dw, tw, (dw / tw), T);
            sw.WriteLine();
            sw.WriteLine();

            double less_spc = 0.333 * dw;
            double greater_spc = 1.5 * dw;
            sw.WriteLine("Spacing of Stiffeners = 0.33 * dw = 0.33 * {0} = {1} mm.", dw, less_spc);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("                      to 1.5 * dw = 1.50 * {0} = {1} mm.", dw, greater_spc);
            sw.WriteLine();
            sw.WriteLine();
            C = 1000;
            sw.WriteLine("Let us adopt Spacing = C = {0} mm. as, {1} < C < {2}", C, less_spc, greater_spc);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("The Intermediate Stiffeners are designed to have a minimum Moment of Inertia of,");
            sw.WriteLine();
            double I1 = (1.5 * dw * dw * dw * tw * tw * tw) / (C * C);

            sw.WriteLine("I1 = 1.5 * dw**3 * tw**3 / C*C");
            sw.WriteLine("   = 1.5 * {0}^3 * {1}^3 / {2}*{2}", dw, tw, C);
            sw.WriteLine("   = {0:f3} sq.sq.mm", I1);
            sw.WriteLine();
            sw.WriteLine();

            double ts = 10;
            sw.WriteLine("Where ts = {0} mm thick Stiffeners plate is used.", ts);
            sw.WriteLine();

            double bs = 16 * ts;
            sw.WriteLine("Maximum width of plate = bs = 16 * {0}", ts);
            sw.WriteLine("                       = {0} mm.", bs);
            sw.WriteLine();
            bs = (int)((bs / 2.0) / 10);
            bs = bs * 10;

            sw.WriteLine("Let us adopt width of Stiffeners plate = bs = {0} mm.", bs);
            sw.WriteLine();
            sw.WriteLine("So, Stiffeners plates of size {0} mm * {1} mm are used.", bs, ts);
            sw.WriteLine();
            double I2 = ts * bs * bs * bs / 3.0;
            //sw.WriteLine("                                3            5                            5");
            //sw.WriteLine("Moment of Inertia = I2 = ts x bs /3 = 17 x 10 Sq. (Sq. mm.) > I1 = 15 x 10 Sq. (Sq. mm.)");
            sw.WriteLine("Moment of Inertia = I2 = ts * bs**3 / 3");
            sw.WriteLine("                  = {0} * {1}^3 ", ts, bs);
            if (I2 > I1)
                sw.WriteLine("                  = {0:f3} > I1 = {1:f3} sq.sq.mm", I2, I1);
            else if (I2 < I1)
                sw.WriteLine("                  = {0:f3} < I1 = {1:f3} sq.sq.mm", I2, I1);
            else if (I2 == I1)
                sw.WriteLine("                  = {0:f3} = I1 = {1:f3} sq.sq.mm", I2, I1);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("STEP 7 : CONNECTIONS OF VERTICAL STIFFENER TO WEB PLATE:");
            sw.WriteLine("-------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine();

            double shr = 125 * ts * ts / bs;

            sw.WriteLine("Shear on welds connecting stiffener to the web plate = (125 * ts * ts / bs)");
            sw.WriteLine("                                                     = 125 * {0}*{0} / {1}", ts, bs);
            sw.WriteLine("                                                     = {0} kN/m.", shr);
            sw.WriteLine("                                                     = {0} N/mm.", shr);
            sw.WriteLine();

            double sw1 = shr / (0.7 * 102.5);

            //sw.WriteLine("Size of weld =  156 / (0.7 x 102.5) = 2.17 mm.");
            sw.WriteLine("Size of weld =  {0} / (0.7 * 102.5)", sw1);
            sw.WriteLine();
            if (sw1 < 5)
            {
                sw1 = 5.0;
            }
            else
            {
                sw1 = (int)sw1;
                sw1 += 1.0;
            }
            sw.WriteLine("Let us adopt {0} mm. alternate welds on both sides.", sw1);
            sw.WriteLine();
            sw.WriteLine("Also, provide additinal stffeners under Stringer Beams.");
            sw.WriteLine();
        }
        #endregion Cross Grider
        #endregion Member Design

        public void SetComboSections()
        {
            //string tab_path = Path.Combine(Application.StartupPath, "Tables");
            int i = 0;
            string sec_name, code1, code2, code3;
            try
            {
                cmb_section_name.Items.Clear();
                switch (cmb_sections_define.SelectedIndex)
                {
                    case 0:
                    case 4:
                    case 5:
                        for (i = 0; i < tbl_rolledSteelBeams.List_Table.Count; i++)
                        {
                            sec_name = tbl_rolledSteelBeams.List_Table[i].SectionName;
                            if (!cmb_section_name.Items.Contains(sec_name) && sec_name != "")
                            {
                                cmb_section_name.Items.Add(sec_name);
                            }
                        }
                        break;
                    case 1:
                        for (i = 0; i < tbl_rolledSteelChannels.List_Table.Count; i++)
                        {
                            sec_name = tbl_rolledSteelChannels.List_Table[i].SectionName;
                            if (!cmb_section_name.Items.Contains(sec_name) && sec_name != "")
                            {
                                cmb_section_name.Items.Add(sec_name);
                            }
                        }
                        break;
                    case 2:
                    case 3:
                    case 6:
                    case 7:
                        for (i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                        {
                            sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                            if (!cmb_section_name.Items.Contains(sec_name))
                            {
                                cmb_section_name.Items.Add(sec_name);
                            }
                        }
                        break;
                }
                if (cmb_section_name.Items.Count > 0) cmb_section_name.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }

        #region Table Functions
        void WriteTable1(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "SteelTruss_Table1.txt");

            //List<string> file_cont = new List<string>(File.ReadAllLines(tab_file));
            List<string> file_cont = iApp.Tables.Get_Tables_EUDL_CDA();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 1 :");
                sw.WriteLine("---------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        void WriteTable2(StreamWriter sw)
        {
            string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            string tab_file = Path.Combine(tab_path, "Truss_Bridge_2.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_Allowable_Working_Stress_Critical();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 2 :");
                sw.WriteLine("---------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        void WriteTable3(StreamWriter sw)
        {
            string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            string tab_file = Path.Combine(tab_path, "Truss_Bridge_3.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_Allowable_Average_Shear_Stress();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 3 :");
                sw.WriteLine("---------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        void WriteTable4(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "Truss_Bridge_4.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_Allowable_Working_Stress_Cross_Section();

            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        public double Get_Table_1_Sigma_Value(double lamda, double fy)
        {
            return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, fy, ref ref_string);

            //lamda = Double.Parse(lamda.ToString("0.000"));

            //int indx = 5;

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "SteelTruss_Table1.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, b1, a2, b2, returned_value;

            //a1 = 0.0;
            //b1 = 0.0;
            //a2 = 0.0;
            //b2 = 0.0;
            //returned_value = 0.0;

            //List<MyList> lst_list = new List<MyList>();

            //for (int i = 0; i < lst_content.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(lst_content[i]);
            //    kStr = kStr.Replace("<=", "");
            //    //if (kStr.StartsWith("--------------"))
            //    //{
            //    //    find = !find; continue;
            //    //}
            //    //if (find)
            //    //{

            //    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //    try
            //    {
            //        double d = mList.GetDouble(0);
            //        lst_list.Add(mList);
            //    }
            //    catch (Exception ex) { }
            //    //}
            //}
            //indx = lst_list[0].StringList.IndexOf(fy.ToString());
            //for (int i = 1; i < lst_list.Count; i++)
            //{
            //    try
            //    {
            //        a1 = lst_list[i].GetDouble(0);
            //        if (lamda < lst_list[1].GetDouble(0))
            //        {
            //            returned_value = lst_list[0].GetDouble(indx);
            //            break;
            //        }
            //        else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //        {
            //            returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //            break;
            //        }

            //        if (a1 == lamda)
            //        {
            //            returned_value = lst_list[i].GetDouble(indx);
            //            break;
            //        }
            //        else if (a1 > lamda)
            //        {
            //            indx += 1;
            //            a2 = a1;
            //            b2 = lst_list[i].GetDouble(indx);

            //            a1 = lst_list[i - 1].GetDouble(0);
            //            b1 = lst_list[i - 1].GetDouble(indx);

            //            returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
            //            break;
            //        }
            //    }
            //    catch (Exception ex) { }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_3_Value(double d_by_t, double d_point)
        {
            return iApp.Tables.Allowable_Average_Shear_Stress(d_by_t, d_point, ref ref_string);

            //int indx = -1;

            //if (d_point >= 0.4 && d_point < 0.6)
            //    indx = 1;
            //else if (d_point >= 0.6 && d_point < 0.8)
            //    indx = 2;
            //else if (d_point >= 0.8 && d_point < 1.0)
            //    indx = 3;
            //else if (d_point >= 1.0 && d_point < 1.2)
            //    indx = 4;
            //else if (d_point >= 1.2 && d_point < 1.4)
            //    indx = 5;
            //else if (d_point >= 1.4 && d_point < 1.5)
            //    indx = 6;
            //else if (d_point >= 1.5)
            //    indx = 7;


            //d_by_t = Double.Parse(d_by_t.ToString("0.0"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, returned_value;
            ////double  b1, a2, b2, returned_value;

            ////a1 = 0.0;
            ////b1 = 0.0;
            ////a2 = 0.0;
            ////b2 = 0.0;
            //returned_value = 0.0;

            //List<MyList> lst_list = new List<MyList>();

            //for (int i = 0; i < lst_content.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(lst_content[i]);
            //    kStr = kStr.Replace("<=", "");
            //    if (kStr.StartsWith("--------------"))
            //    {
            //        find = !find; continue;
            //    }
            //    if (find)
            //    {
            //        mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);

            //    if (d_by_t < a1)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        #endregion Table Functions


    }
}
