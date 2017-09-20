using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using AstraInterface.DataStructure;
//using AstraFunctionOne.BridgeDesign.SteelTrussTables;


namespace AstraFunctionOne.BridgeDesign.SteelTruss
{

    public partial class frmCompleteDesign : Form
    {

        bool IsWarren2 = false;
        IApplication iApp = null;

        //TableRolledSteelBeams tbl_rolledSteelBeams = null;
        //TableRolledSteelChannels tbl_rolledSteelChannels = null;
        //TableRolledSteelAngles tbl_rolledSteelAngles = null;

        SteelTrussMemberAnalysis Truss_Analysis = null;
        CompleteDesign complete_design = null;
        List<LoadData> LoadList = null;
        List<LoadData> Live_Load_List = null;
        
        eMemberType design_member = eMemberType.AllMember;
        string ref_string = "";


        double DL, LL, IL, h, l, fy, fc, ft, d;
        double sigma_b, sigma_c;
        double a = 12;
        double top_plate_width;
        double top_plate_thickness;
        double bottom_plate_width;
        double bottom_plate_thickness;
        double side_plate_width;
        double side_plate_thickness;
        double vertical_stiff_plate_width;
        double vertical_stiff_plate_thickness;

        #region User file Handling Variables

        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";
        bool is_process = false;
        #endregion
        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelBeams;
                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelChannels;
                return iApp.Tables.IS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelAngles;
                return iApp.Tables.IS_SteelAngles;
            }
        }
        public frmCompleteDesign(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
            user_path = app.LastDesignWorkingFolder;
            MembersDesign complete_design = new MembersDesign();
            IsWarren2 = false;
           
        }
        public frmCompleteDesign(IApplication app, bool isWarren2)
        {
            InitializeComponent();
            this.iApp = app;
            user_path = app.LastDesignWorkingFolder;
            MembersDesign complete_design = new MembersDesign();
            IsWarren2 = isWarren2;
        }


        void Select_Member_Group(int indx)
        {
            //int indx = e.RowIndex;
            string memNo = "";
            try
            {
                memNo = dgv_mem_details[0, indx].Value.ToString();
                if (memNo != "")
                {
                    indx = Complete_Design.Members.IndexOf(memNo);
                    if (indx == -1) return;

                    cmb_mem_group.SelectedItem = memNo;
                    cmb_cd_mem_type.SelectedIndex = (int)Complete_Design.Members[indx].MemberType;
                }

                txt_cd_mem_no.Text = Complete_Design.Members[indx].Group.MemberNosText;


                cmb_sections_define.SelectedIndex = ((int)Complete_Design.Members[indx].SectionDetails.DefineSection);

                cmb_select_standard.SelectedIndex = Complete_Design.Members[indx].SectionDetails.SectionName.StartsWith("UK") ? 0 : 1;
                cmb_section_name.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionName;
                
                
                
                cmb_code1.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionCode;
                //if (cmb_sec_thk.Visible)
                cmb_sec_thk.SelectedItem = Complete_Design.Members[indx].SectionDetails.AngleThickness;

                txt_no_ele.Text = Complete_Design.Members[indx].SectionDetails.NoOfElements.ToString();

                if (!(chk_chng_mode.Checked && chk_chng_mode.Visible))
                {
                    txt_tp_width.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Width.ToString("0");
                    txt_tp_thk.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Thickness.ToString("0");
                    txt_bp_wd.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Width.ToString("0");
                    txt_bp_thk.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Thickness.ToString("0");

                    txt_sp_wd.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Width.ToString("0");
                    txt_sp_thk.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Thickness.ToString("0");
                    txt_vsp_wd.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Width.ToString("0");
                    txt_vsp_thk.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Thickness.ToString("0");

                    txt_sec_lat_spac.Text = Complete_Design.Members[indx].SectionDetails.LateralSpacing.ToString();
                    txt_sec_bolt_dia.Text = Complete_Design.Members[indx].SectionDetails.BoltDia.ToString();
                    txt_sec_nb.Text = Complete_Design.Members[indx].SectionDetails.NoOfBolts.ToString();
                }
            }
            catch (Exception ex) { }
        }
        void Select_Member_Group(string member_name)
        {
            int indx = -1;
            string memNo = member_name;
            try
            {
                if (memNo != "")
                {
                    indx = Complete_Design.Members.IndexOf(memNo);
                    if (indx == -1) return;

                    cmb_mem_group.SelectedItem = memNo;
                    cmb_cd_mem_type.SelectedIndex = (int)Complete_Design.Members[indx].MemberType;
                }

                txt_cd_mem_no.Text = Complete_Design.Members[indx].Group.MemberNosText;


                cmb_sections_define.SelectedIndex = ((int)Complete_Design.Members[indx].SectionDetails.DefineSection);
                cmb_section_name.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionName;
                cmb_code1.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionCode;
                if (cmb_sec_thk.Visible)
                    cmb_sec_thk.SelectedItem = Complete_Design.Members[indx].SectionDetails.AngleThickness;

                txt_no_ele.Text = Complete_Design.Members[indx].SectionDetails.NoOfElements.ToString();


                txt_tp_width.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Width.ToString("0");
                txt_tp_thk.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Thickness.ToString("0");
                txt_bp_wd.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Width.ToString("0");
                txt_bp_thk.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Thickness.ToString("0");

                txt_sp_wd.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Width.ToString("0");
                txt_sp_thk.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Thickness.ToString("0");
                txt_vsp_wd.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Width.ToString("0");
                txt_vsp_thk.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Thickness.ToString("0");

                txt_sec_lat_spac.Text = Complete_Design.Members[indx].SectionDetails.LateralSpacing.ToString();
                txt_sec_bolt_dia.Text = Complete_Design.Members[indx].SectionDetails.BoltDia.ToString();
                txt_sec_nb.Text = Complete_Design.Members[indx].SectionDetails.NoOfBolts.ToString();


                //dgv_mem_details.FirstDisplayedScrollingRowIndex = dgv_mem_details.RowCount - 1;

            }
            catch (Exception ex) { }
        }

        void SetWorkingFolder()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select your Working Folder";
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                    {
                        is_process = false;
                        FilePath = fbd.SelectedPath;
                        //btnProcess.Enabled = true;
                    }
                }
            }
        }
        public void SetDefaultDeadLoad()
        {
            try
            {
                dgv_SIDL.Rows.Clear();
                if (rbtn_road_bridge.Checked)
                {
                    complete_design.DeadLoads.DeckSlab.Length = Truss_Analysis.Analysis.Length + 1.0;
                    complete_design.DeadLoads.DeckSlab.Breadth = (Truss_Analysis.Analysis.Width / 2);
                    complete_design.DeadLoads.DeckSlab.Depth = 0.200;
                    complete_design.DeadLoads.DeckSlab.TotalNo = 2;
                    complete_design.DeadLoads.DeckSlab.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.DeckSlab);

                    complete_design.DeadLoads.Kerb.Length = complete_design.DeadLoads.DeckSlab.Length;
                    complete_design.DeadLoads.Kerb.Breadth = 0.3;
                    complete_design.DeadLoads.Kerb.Depth = 0.51;
                    complete_design.DeadLoads.Kerb.TotalNo = 2;
                    complete_design.DeadLoads.Kerb.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.Kerb);


                    complete_design.DeadLoads.FootPathSlab.Length = complete_design.DeadLoads.DeckSlab.Length;
                    complete_design.DeadLoads.FootPathSlab.Breadth = 1.88;
                    complete_design.DeadLoads.FootPathSlab.Depth = 0.1;
                    complete_design.DeadLoads.FootPathSlab.TotalNo = 2;
                    complete_design.DeadLoads.FootPathSlab.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.FootPathSlab);


                    complete_design.DeadLoads.OuterBeam.Length = complete_design.DeadLoads.DeckSlab.Length;
                    complete_design.DeadLoads.OuterBeam.Breadth = 0.15;
                    complete_design.DeadLoads.OuterBeam.Depth = 0.51;
                    complete_design.DeadLoads.OuterBeam.TotalNo = 2;
                    complete_design.DeadLoads.OuterBeam.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.OuterBeam);


                    complete_design.DeadLoads.WearingCoat.Length = complete_design.DeadLoads.DeckSlab.Length;
                    complete_design.DeadLoads.WearingCoat.Breadth = 3.75;
                    complete_design.DeadLoads.WearingCoat.Depth = 0.0;
                    complete_design.DeadLoads.WearingCoat.TotalNo = 2;
                    complete_design.DeadLoads.WearingCoat.Gamma = 2;

                    AddDeadLoadRow(complete_design.DeadLoads.WearingCoat);



                    complete_design.DeadLoads.Railing.Length = complete_design.DeadLoads.DeckSlab.Length;
                    complete_design.DeadLoads.Railing.Breadth = 0.0;
                    complete_design.DeadLoads.Railing.Depth = 0.0;
                    complete_design.DeadLoads.Railing.TotalNo = 2;
                    complete_design.DeadLoads.Railing.Gamma = 1.6;

                    AddDeadLoadRow(complete_design.DeadLoads.Railing);


                    complete_design.DeadLoads.LiveLoadOnFootPath.Length = complete_design.DeadLoads.DeckSlab.Length;
                    complete_design.DeadLoads.LiveLoadOnFootPath.Breadth = 1.98;
                    complete_design.DeadLoads.LiveLoadOnFootPath.Depth = 0.0;
                    complete_design.DeadLoads.LiveLoadOnFootPath.TotalNo = 2;
                    complete_design.DeadLoads.LiveLoadOnFootPath.Gamma = 1.92;

                    AddDeadLoadRow(complete_design.DeadLoads.LiveLoadOnFootPath);
                    complete_design.DeadLoads.SetLoads();
                }
                else
                {
                    complete_design.DeadLoads.RailPermanentLoadAsOpenFloor.Length = Truss_Analysis.Analysis.Length + 1.0;
                    complete_design.DeadLoads.RailPermanentLoadAsOpenFloor.Breadth = (Truss_Analysis.Analysis.Width / 2);
                    complete_design.DeadLoads.RailPermanentLoadAsOpenFloor.Depth = 0.300;
                    complete_design.DeadLoads.RailPermanentLoadAsOpenFloor.TotalNo = 2;
                    complete_design.DeadLoads.RailPermanentLoadAsOpenFloor.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.RailPermanentLoadAsOpenFloor);

                    complete_design.DeadLoads.RailBendingMoment.Length = Truss_Analysis.Analysis.Length + 1.0;
                    complete_design.DeadLoads.RailBendingMoment.Breadth = 0.3;
                    complete_design.DeadLoads.RailBendingMoment.Depth = 0.51;
                    complete_design.DeadLoads.RailBendingMoment.TotalNo = 2;
                    complete_design.DeadLoads.RailBendingMoment.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.RailBendingMoment);


                    complete_design.DeadLoads.RailShearForce.Length = Truss_Analysis.Analysis.Length + 1.0;
                    complete_design.DeadLoads.RailShearForce.Breadth = 1.88;
                    complete_design.DeadLoads.RailShearForce.Depth = 0.1;
                    complete_design.DeadLoads.RailShearForce.TotalNo = 2;
                    complete_design.DeadLoads.RailShearForce.Gamma = 24;

                    AddDeadLoadRow(complete_design.DeadLoads.RailShearForce);
                }

            }
            catch (Exception ex) { }
        }
        void Format_SIDL()
        {
            int indx = 0;

            //double L, B, D, N, vol, gama, to_wgt;
            SuperInposedDeadLoad dl = new SuperInposedDeadLoad("LL");
            try
            {
                for (int i = 0; i < dgv_SIDL.RowCount; i++)
                {
                    indx = i;
                    dl.Length = MyList.StringToDouble(dgv_SIDL[1, indx].Value.ToString(), 0.0);
                    dl.Breadth = MyList.StringToDouble(dgv_SIDL[2, indx].Value.ToString(), 0.0);
                    dl.Depth = MyList.StringToDouble(dgv_SIDL[3, indx].Value.ToString(), 0.0);
                    dl.TotalNo = MyList.StringToInt(dgv_SIDL[4, indx].Value.ToString(), 0);
                    dl.Gamma = MyList.StringToDouble(dgv_SIDL[6, indx].Value.ToString(), 0);




                    dgv_SIDL[1, indx].Value = dl.Length.ToString("0.00");
                    dgv_SIDL[2, indx].Value = dl.Breadth.ToString("0.000");
                    dgv_SIDL[3, indx].Value = dl.Depth.ToString("0.000");
                    dgv_SIDL[4, indx].Value = dl.TotalNo.ToString();
                    dgv_SIDL[5, indx].Value = dl.Volume.ToString("0.000");
                    dgv_SIDL[6, indx].Value = dl.Gamma.ToString("0.000");
                    dgv_SIDL[7, indx].Value = dl.Weight.ToString("0.000");
                }
            }
            catch (Exception ex) { }
        }

        void SetCompleteDesign(string file_name)
        {
            try
            {

                //complete_design = new CompleteDesign();
                //complete_design.DeadLoads.ReadFromFile(file_name);
                if (File.Exists(file_name))
                {
                    complete_design = new CompleteDesign();
                    complete_design.ReadFromFile(file_name);
                    SetMemberDetails();
                    dgv_mem_details.Rows.Clear();
                    if (complete_design.Members.Count > 0)
                    {
                        foreach (CMember mem in complete_design.Members)
                        {
                            AddMemberRow(mem);
                        }
                    }
                    if (complete_design.DeadLoads.IsRailLoad == false)
                    {
                        //dgv_SIDL.Visible = true;
                        //dgv_SIDL.Rows.Clear();
                        //foreach (DeadLoad dl in complete_design.DeadLoads.Load_List)
                        //{
                        //    AddDeadLoadRow(dl);

                        //}


                    }
                    else
                    {
                        dgv_SIDL.Visible = false;
                        txt_rail_eff_span.Text = complete_design.DeadLoads.Rail.EffectiveSpan.ToString();
                        txt_rail_permanent_load.Text = complete_design.DeadLoads.Rail.PermanentLoad.ToString();
                        txt_rail_bending_moment.Text = complete_design.DeadLoads.Rail.BendingMoment.ToString();
                        txt_rail_shear_force.Text = complete_design.DeadLoads.Rail.ShearForce.ToString();
                    }

                    List<string> lst = new List<string>(File.ReadAllLines(file_name));
                    string kStr = "";
                    MyList mlist = null;
                    int j = 0;
                    string entity_name = "";
                    double dd = 0.0;
                    bool flag = false;
                    bool flag2 = false;

                    for (int i = 0; i < lst.Count; i++)
                    {
                        kStr = MyList.RemoveAllSpaces(lst[i]).ToUpper();
                        mlist = new MyList(kStr, ' ');
                        if (kStr.StartsWith("NAME"))
                        {
                            flag2 = true; i++;
                            dgv_SIDL.Rows.Clear();
                            continue;
                        }
                        if (flag2)
                        {
                            if (kStr.Contains("------------------------------------------------"))
                            {
                                flag = true;
                                flag2 = false;
                                continue;
                            }
                        }

                        if (flag)
                        {
                            if (kStr.Contains("------------------------------------------------"))
                            {
                                flag = false;
                                break;
                            }

                            for (j = 0; j < mlist.Count; j++)
                            {
                                try
                                {
                                    dd = mlist.GetDouble(j);
                                    dgv_SIDL.Rows.Add(entity_name, mlist.GetDouble(j),
                               mlist.GetDouble(j + 1),
                               mlist.GetDouble(j + 2),
                               mlist.GetInt(j + 3),
                               mlist.GetDouble(j + 4),
                               mlist.GetDouble(j + 5),
                               mlist.GetDouble(j + 6));
                                    entity_name = "";
                                    break;
                                }
                                catch (Exception ex) { }
                                entity_name += mlist.StringList[j] + " ";
                            }
                        }
                    }
                    Format_SIDL();
                }
            }
            catch (Exception ex) { }
            Show_Total_Weight();
        }
        SetProgressValue spv;
        public delegate void SetProgressValue(ProgressBar pbr, int val);
        public void SetProgressBar(ProgressBar pbr, int val)
        {
            pbr.Value = val;
        }

        public void Read_User_Input()
        {

            //sw.WriteLine("ll_default_data={0}", rbtn_LL_fill_data.Checked);
            //sw.WriteLine("ll_default_data_load_type={0}", cmb_custom_LL_type.Text);
            //sw.WriteLine("ll_default_data_clearance={0}", txt_custom_LL_lat_clrns.Text);
            //sw.WriteLine("ll_default_data_lanes={0}", cmb_custom_LL_lanes.Text);
            //sw.WriteLine("ll_default_data_xincr={0}", txt_custom_LL_Xcrmt.Text);
            //sw.WriteLine("ll_default_data_load_gen={0}", txt_custom_LL_load_gen.Text);

            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            string file_name = "";
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
                        case "L":
                            txt_L.Text = mList.StringList[1];
                            break;
                        case "Lw":
                            txt_B.Text = mList.StringList[1];
                            break;

                        case "h":
                            txt_H.Text = mList.StringList[1];
                            break;

                        case "np":
                            txt_gd_np.Text = mList.StringList[1];
                            break;
                        case "fck":
                            txt_fck.Text = mList.StringList[1];
                            break;
                        case "m":
                            txt_m.Text = mList.StringList[1];
                            break;
                        case "w":
                            txt_w.Text = mList.StringList[1];
                            break;
                        case "fy":
                            txt_gd_fy.Text = mList.StringList[1];
                            break;
                        case "ft":
                            txt_gd_ft.Text = mList.StringList[1];
                            break;
                        case "fc":
                            txt_fc.Text = mList.StringList[1];
                            break;
                        case "sigma_b":
                            txt_sigma_b.Text = mList.StringList[1];
                            break;
                        case "sigma_c":
                            txt_sigma_c.Text = mList.StringList[1];
                            break;
                        case "input_file":
                            i++;
                            txt_analysis_file.Text = lst_content[i];
                            break;
                        case "lac_index":
                            cmb_lac.SelectedIndex = mList.GetInt(1);
                            break;
                        case "lac_ang":
                            txt_lac_ang.Text = mList.StringList[1];
                            break;
                        case "lac_bl":
                            cmb_lac_bl.SelectedItem = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "lac_tl":
                            cmb_lac_tl.SelectedItem = mList.GetDouble(1);
                            break;
                        case "lac_d2":
                            txt_lac_d2.Text = mList.StringList[1];
                            break;
                        case "lac_nr":
                            txt_lac_nr.Text = mList.StringList[1];
                            break;
                        case "lac_fs":
                            txt_lac_fs.Text = mList.StringList[1];
                            break;
                        case "lac_fb":
                            txt_lac_fb.Text = mList.StringList[1];
                            break;
                        case "lac_weld_strength":
                            txt_weld_strength.Text = mList.StringList[1];
                            break;
                        case "lac_weld_size":
                            txt_weld_size.Text = "";
                            txt_weld_size.Text = mList.StringList[1];
                            break;
                        case "lac_weld":
                            rbtn_weld.Checked = (mList.StringList[1].Trim().TrimEnd().TrimStart().ToLower() == "true");
                            break;
                        case "conn_d":
                            txt_conn_d.Text = mList.StringList[1];
                            break;
                        case "conn_nr":
                            txt_conn_nr.Text = mList.StringList[1];
                            break;
                        case "conn_bg":
                            txt_conn_bg.Text = mList.StringList[1];
                            break;
                        case "conn_tg":
                            txt_conn_tg.Text = mList.StringList[1];
                            break;
                        case "conn_fs":
                            txt_conn_fs.Text = mList.StringList[1];
                            break;
                        case "conn_fb":
                            txt_conn_fb.Text = mList.StringList[1];
                            break;
                        case "conn_ft":
                            txt_conn_ft.Text = mList.StringList[1];
                            break;
                        case "shr_section":
                            cmb_Shr_Con_Section_name.SelectedItem = mList.StringList[1];
                            break;
                        case "shr_code":
                            cmb_Shr_Con_Section_Code.SelectedItem = mList.StringList[1];
                            break;
                        case "ll_default_data":
                            rbtn_LL_fill_data.Checked = (mList.StringList[1].ToLower() == "true");
                            rbtn_custom_LL.Checked = !rbtn_LL_fill_data.Checked;
                            break;
                        case "ll_default_data_load_type":
                            cmb_custom_LL_type.Text = mList.StringList[1];
                            break;
                        case "ll_default_data_clearance":
                            txt_custom_LL_lat_clrns.Text = mList.StringList[1];
                            break;
                        case "ll_default_data_lanes":
                            cmb_custom_LL_lanes.Text = mList.StringList[1];
                            break;
                        case "ll_default_data_xincr":
                            txt_custom_LL_Xcrmt.Text = mList.StringList[1];
                            break;
                        case "ll_default_data_load_gen":
                            txt_custom_LL_load_gen.Text = mList.StringList[1];
                            break;
                    }
                    #endregion
                }
                file_name = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
                if (File.Exists(file_name))
                {
                    //SetCompleteDesign(file_name);
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
                sw.WriteLine("L={0}", txt_L.Text);
                sw.WriteLine("Lw={0}", txt_B.Text);
                sw.WriteLine("h={0}", txt_H.Text);
                sw.WriteLine("np={0}", txt_gd_np.Text);
                sw.WriteLine("fck={0}", txt_fck.Text);
                sw.WriteLine("fck={0}", txt_fck.Text);
                sw.WriteLine("m={0}", txt_m.Text);
                sw.WriteLine("w={0}", txt_w.Text);
                sw.WriteLine("fy={0}", txt_gd_fy.Text);
                sw.WriteLine("ft={0}", txt_gd_ft.Text);
                sw.WriteLine("fc={0}", txt_fc.Text);
                sw.WriteLine("sigma_b={0}", txt_sigma_b.Text);
                sw.WriteLine("sigma_c={0}", txt_sigma_c.Text);
                //sw.WriteLine("input_file");
                //sw.WriteLine("{0}", txt_analysis_file.Text);
                sw.WriteLine("lac_index={0}", cmb_lac.SelectedIndex);
                sw.WriteLine("lac_ang={0}", txt_lac_ang.Text);
                sw.WriteLine("lac_bl={0}", cmb_lac_bl.Text);
                sw.WriteLine("lac_tl={0}", cmb_lac_tl.Text);
                sw.WriteLine("lac_tl={0}", cmb_lac_tl.Text);
                sw.WriteLine("lac_d2={0}", txt_lac_d2.Text);
                sw.WriteLine("lac_nr={0}", txt_lac_nr.Text);
                sw.WriteLine("lac_fs={0}", txt_lac_fs.Text);
                sw.WriteLine("lac_fb={0}", txt_lac_fb.Text);
                sw.WriteLine("lac_weld_strength={0}", txt_weld_strength.Text);
                sw.WriteLine("lac_weld_size={0}", txt_weld_size.Text);
                sw.WriteLine("lac_weld={0}", rbtn_weld.Checked.ToString());

                sw.WriteLine("conn_d={0}", txt_conn_d.Text);
                sw.WriteLine("conn_nr={0}", txt_conn_nr.Text);
                sw.WriteLine("conn_bg={0}", txt_conn_bg.Text);
                sw.WriteLine("conn_tg={0}", txt_conn_tg.Text);
                sw.WriteLine("conn_fs={0}", txt_conn_fs.Text);
                sw.WriteLine("conn_fb={0}", txt_conn_fb.Text);
                sw.WriteLine("conn_ft={0}", txt_conn_ft.Text);

                sw.WriteLine("shr_section={0}", cmb_Shr_Con_Section_name.Text);
                sw.WriteLine("shr_code={0}", cmb_Shr_Con_Section_Code.Text);

                sw.WriteLine("ll_default_data={0}", rbtn_LL_fill_data.Checked);
                sw.WriteLine("ll_default_data_load_type={0}", cmb_custom_LL_type.Text);
                sw.WriteLine("ll_default_data_clearance={0}", txt_custom_LL_lat_clrns.Text);
                sw.WriteLine("ll_default_data_lanes={0}", cmb_custom_LL_lanes.Text);
                sw.WriteLine("ll_default_data_xincr={0}", txt_custom_LL_Xcrmt.Text);
                sw.WriteLine("ll_default_data_load_gen={0}", txt_custom_LL_load_gen.Text);

                //sw.WriteLine("member_load_file = {0}", cmb_Shr_Con_Section_Code.Text);
                //sw.WriteLine("{0}", Path.Combine();
                SaveMemberLoads(Path.Combine(system_path, "MEMBER_LOAD_DATA.txt"));

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
                //IL = MyList.StringToDouble(txt_gd_IL.Text, 0.0);
                h = MyList.StringToDouble(txt_H.Text, 0.0);
                l = MyList.StringToDouble(txt_L.Text, 0.0);
                fy = MyList.StringToDouble(txt_gd_fy.Text, 0.0);
                fc = MyList.StringToDouble(txt_fc.Text, 0.0);
                ft = MyList.StringToDouble(txt_gd_ft.Text, 0.0);
                sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);

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
            if (cmb_design_member.SelectedIndex == 0)
            {
                design_member = eMemberType.AllMember;
            }
            else if (cmb_design_member.SelectedIndex > 0)
            {
                design_member = (eMemberType)(cmb_design_member.SelectedIndex - 1);
            }
            else
                design_member = eMemberType.NoSelection;
        }
        private void Calculate_Program()
        {
            string file_path = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            //Truss_Analysis = new SteelTrussAnalysis(file_path, pbar);


            if (Truss_Analysis == null)
                Truss_Analysis = new SteelTrussMemberAnalysis(iApp, file_path);

            //string file_name = Path.Combine(user_path, "MembersDesign.txt");
            rep_file_name = Truss_Analysis.Analysis.Length.ToString("0") + "m Bridge " + ((cmb_design_member.Text.ToUpper() == "ALL") ? "Complete" : cmb_design_member.Text.Replace("Member", "")) + " Member Design Report.TXT";
            rep_file_name = Path.Combine(user_path, rep_file_name);



            rep_file_name = Path.Combine(user_path, "DESIGN_REP.TXT");
            if (File.Exists(rep_file_name))
            {
                if (MessageBox.Show(rep_file_name + " report file is already exist. Do you want to Overwrite this file?", "ASTRA", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }



            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));



            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 5.0           *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*        DESIGN OF STEEL TRUSS BRIDGE        *");
                sw.WriteLine("\t\t*          COMPLETE MEMBER DESIGN            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("GENERAL DATA ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Length / Span of each Truss [L] = {0} m", txt_L.Text);
                sw.WriteLine("Length of each Panel [l] = {0} m", txt_B.Text);
                sw.WriteLine("No of Panels in each Truss = {0} ", txt_gd_np.Text);
                sw.WriteLine("Height of Truss [h] = {0} m", txt_H.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} N/sq.mm", txt_gd_fy.Text);
                sw.WriteLine("Length of Span [l] = {0} kN/m", txt_L.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} kN/m", txt_gd_fy.Text);
                sw.WriteLine("Permissible stress in Axial comppression [fc] = {0} N/sq.mm", txt_fc.Text);
                sw.WriteLine("Permissible Tensile stress [ft] = {0} N/sq.mm", txt_gd_ft.Text);
                sw.WriteLine("Permissible Bending stress in steel [σ_b] = {0} N/sq.mm", txt_sigma_b.Text);
                sw.WriteLine("Permissible shear stress in steel [σ_c] = {0} N/sq.mm", txt_sigma_c.Text);
                sw.WriteLine();
                sw.WriteLine();
                int v = 0;
                iApp.Progress_ON("Design Members....");
                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    if (design_member == eMemberType.AllMember)
                    {
                        CMember mem = Complete_Design.Members[i];
                        TotalAnalysis(sw, ref mem);
                    }
                    else
                    {
                        if (design_member == Complete_Design.Members[i].MemberType)
                        {
                            CMember mem = Complete_Design.Members[i];
                            TotalAnalysis(sw, ref mem);
                            Complete_Design.Members[i] = mem;
                        }
                        else
                            continue;
                    }
                    if (Complete_Design.Members[i].MemberType == eMemberType.CrossGirder ||
                        Complete_Design.Members[i].MemberType == eMemberType.StringerBeam)
                    {
                        DesignShearConnector(Complete_Design.Members[i], sw);
                    }
                    iApp.SetProgressValue(i, Complete_Design.Members.Count);
                    //v = (int)(((double)(i + 1) / (double)Complete_Design.Members.Count) * 100.0);
                    //pbar.Invoke(spv, pbar, v);
                }
                iApp.Progress_OFF();

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
                Complete_Design.WriteForcesSummery(sw);
                Complete_Design.WriteGroupSummery(sw);
                string file_ds_frc = "";
                file_ds_frc = Path.Combine(user_path, "DESIGN_SECTION_SUMMARY.TXT");
                Complete_Design.WriteGroupSummery(file_ds_frc);
                file_ds_frc = Path.Combine(user_path, "DESIGN_FORCES_SUMMARY.TXT");
                Complete_Design.WriteForcesSummery(file_ds_frc);
                iApp.RunExe(file_ds_frc);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            btnReport.Enabled = true;
            btnDrawing.Enabled = true;


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
                user_path = value;
                //if (!File.Exists(Path.Combine(user_path, "ANALYSIS_REP.TXT")))
                //{
                //    MessageBox.Show(this, "ANALYSIS_REP.TXT   file not found!", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                this.Text = "DESIGN OF STEEL TRUSS MEMBERS : " + MyList.Get_Modified_Path(value);

                file_path = user_path;
                //file_path = GetAstraDirectoryPath(user_path);

                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);



                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                //rep_file_name = Path.Combine(file_path, "Bridge_Steel_Truss_Complete_Deign.TXT");
                rep_file_name = Path.Combine(file_path, "DESIGN_REP.TXT");
                user_input_file = Path.Combine(system_path, "Steel Truss Complete Deign Bridge.FIL");
                user_drawing_file = Path.Combine(system_path, "STEEL_TRUSS_OPEN_WEB_GIRDER_COMPLETE_DEIGN.FIL");

                btnProcess.Enabled = Directory.Exists(value);
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
                //return "STEEL_TRUSS_OPEN_WEB_COMPLETE_DESIGN";
                return "ANALYSIS OF STEEL TRUSS BRIDGE";
            }
        }

        public CompleteDesign Complete_Design
        {
            get
            {
                return complete_design;
            }
        }

        public CMember GetMemberData()
        {
            MyList mList = new MyList(txt_cd_mem_no.Text, ' ');

            CMember member = new CMember();
            member.Group.GroupName = cmb_mem_group.Text;
            member.Group.MemberNosText = txt_cd_mem_no.Text;
            member.MemberType = (eMemberType)cmb_cd_mem_type.SelectedIndex;
            member.SectionDetails.DefineSection = (eDefineSection)cmb_sections_define.SelectedIndex;
            member.SectionDetails.SectionName = cmb_section_name.Text;
            member.SectionDetails.SectionCode = cmb_code1.Text;
            member.SectionDetails.NoOfElements = MyList.StringToDouble(txt_no_ele.Text, 1);

            member.SectionDetails.AngleThickness = MyList.StringToDouble(cmb_sec_thk.Text, 0.0);


            member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(txt_cd_mem_no.Text);

            member.Force = Truss_Analysis.GetForce(ref member);



            //member.Weight = MyList.StringToDouble(txt_cd_wgt.Text, 0.0);
            member.WeightPerMetre = GetWeightPerMetre(member);
            member.SectionDetails.TopPlate.Width = MyList.StringToDouble(txt_tp_width.Text, 0.0);
            member.SectionDetails.TopPlate.Thickness = MyList.StringToDouble(txt_tp_thk.Text, 0.0);
            member.SectionDetails.TopPlate.Length = member.Length;
            member.SectionDetails.TopPlate.TotalPlates = 1;

            member.SectionDetails.BottomPlate.Width = MyList.StringToDouble(txt_bp_wd.Text, 0.0);
            member.SectionDetails.BottomPlate.Thickness = MyList.StringToDouble(txt_bp_thk.Text, 0.0);
            member.SectionDetails.BottomPlate.Length = member.Length;
            member.SectionDetails.BottomPlate.TotalPlates = 1;

            member.SectionDetails.SidePlate.Width = MyList.StringToDouble(txt_sp_wd.Text, 0.0);
            member.SectionDetails.SidePlate.Thickness = MyList.StringToDouble(txt_sp_thk.Text, 0.0);
            member.SectionDetails.SidePlate.Length = member.Length;
            member.SectionDetails.SidePlate.TotalPlates = 2;
            member.SectionDetails.VerticalStiffenerPlate.Width = MyList.StringToDouble(txt_vsp_wd.Text, 0.0);
            member.SectionDetails.VerticalStiffenerPlate.Thickness = MyList.StringToDouble(txt_vsp_thk.Text, 0.0);
            member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;
            member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;
            member.SectionDetails.LateralSpacing = MyList.StringToDouble(txt_sec_lat_spac.Text, 0.0);
            member.SectionDetails.BoltDia = MyList.StringToDouble(txt_sec_bolt_dia.Text, 0.0);
            member.SectionDetails.NoOfBolts = MyList.StringToInt(txt_sec_nb.Text, 0);

            return member;
        }
        public double GetWeightPerMetre(CMember m)
        {
            double wt_p_m = 0.0;

            if (m.SectionDetails.SectionName.EndsWith("B"))
            {
                wt_p_m = iApp.Tables.Get_BeamData_FromTable(m.SectionDetails.SectionName, m.SectionDetails.SectionCode).Weight;
            }
            else if (m.SectionDetails.SectionName.EndsWith("C")) // Channel
            {
                wt_p_m = iApp.Tables.Get_ChannelData_FromTable(m.SectionDetails.SectionName, m.SectionDetails.SectionCode).Weight;
            }
            else if (m.SectionDetails.SectionName.EndsWith("A"))
            {
                wt_p_m = iApp.Tables.Get_AngleData_FromTable(m.SectionDetails.SectionName, m.SectionDetails.SectionCode, m.SectionDetails.AngleThickness).Weight;
            }

            return wt_p_m * 0.001; // Convert Newton to
        }
        public double GetWeightPerMetre()
        {
            double wt_p_m = 0.0;
            if (cmb_sections_define.SelectedIndex == 0 ||
                cmb_sections_define.SelectedIndex == 4 ||
                cmb_sections_define.SelectedIndex == 5) // Beam
            {
                wt_p_m = iApp.Tables.Get_BeamData_FromTable(cmb_section_name.Text, cmb_code1.Text).Weight;
                //wt_p_m = wt_p_m * 0.001;
            }
            else if (cmb_sections_define.SelectedIndex == 1) // Channel
            {
                wt_p_m = iApp.Tables.Get_ChannelData_FromTable(cmb_section_name.Text, cmb_code1.Text).Weight;
                //wt_p_m = wt_p_m * 0.001;
            }
            else if (cmb_sections_define.SelectedIndex == 2 ||
                cmb_sections_define.SelectedIndex == 3 ||
                cmb_sections_define.SelectedIndex == 6 ||
                cmb_sections_define.SelectedIndex == 7)
            {
                wt_p_m = iApp.Tables.Get_AngleData_FromTable(cmb_section_name.Text, cmb_code1.Text, MyList.StringToDouble(cmb_sec_thk.Text, 0.0)).Weight;
                //wt_p_m = wt_p_m * 0.001;
            }
            return wt_p_m * 0.001;
        }
        public void ReadDeadLoadInputs()
        {
            int row_indx = 0;
            if (dgv_SIDL.RowCount == 0)
            {
                return;
            }
            if (rbtn_road_bridge.Checked)
            {
                complete_design.DeadLoads.IsRailLoad = false;
                row_indx = 0;
                complete_design.DeadLoads.Load_List.Clear();
                for (row_indx = 0; row_indx < dgv_SIDL.RowCount - 1; row_indx++)
                {
                    SuperInposedDeadLoad dl = new SuperInposedDeadLoad(dgv_SIDL[0, row_indx].Value.ToString());
                    dl.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                    dl.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 1.0);
                    dl.Depth = MyList.StringToDouble(dgv_SIDL[3, row_indx].Value.ToString(), 1.0);
                    dl.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                    dl.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);
                    complete_design.DeadLoads.Load_List.Add(dl);

                    if (dl.Name.Contains("DECK SLAB"))
                    {
                        d = dl.Depth * 1000.0;
                    }
                }


                //row_indx = 1;
                //complete_design.DeadLoads.Kerb.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.Kerb.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.Kerb.Depth = MyList.StringToDouble(dgv_SIDL[3, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.Kerb.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                //complete_design.DeadLoads.Kerb.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);

                //row_indx = 2;
                //complete_design.DeadLoads.FootPathSlab.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.FootPathSlab.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.FootPathSlab.Depth = MyList.StringToDouble(dgv_SIDL[3, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.FootPathSlab.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                //complete_design.DeadLoads.FootPathSlab.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);


                //row_indx = 3;
                //complete_design.DeadLoads.OuterBeam.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.OuterBeam.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.OuterBeam.Depth = MyList.StringToDouble(dgv_SIDL[3, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.OuterBeam.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                //complete_design.DeadLoads.OuterBeam.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);

                //row_indx = 4;
                //complete_design.DeadLoads.WearingCoat.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.WearingCoat.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 0.0);
                ////complete_design.DeadLoads.WearingCoat.Depth = MyList.StringToDouble(dgv_SIDL[3, col_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.WearingCoat.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                //complete_design.DeadLoads.WearingCoat.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);

                //row_indx = 5;
                //complete_design.DeadLoads.Railing.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.Railing.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 0.0);
                ////complete_design.DeadLoads.Railing.Depth = MyList.StringToDouble(dgv_SIDL[3, col_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.Railing.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                //complete_design.DeadLoads.Railing.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);


                //row_indx = 6;
                //complete_design.DeadLoads.LiveLoadOnFootPath.Length = MyList.StringToDouble(dgv_SIDL[1, row_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.LiveLoadOnFootPath.Breadth = MyList.StringToDouble(dgv_SIDL[2, row_indx].Value.ToString(), 0.0);
                ////complete_design.DeadLoads.Railing.Depth = MyList.StringToDouble(dgv_SIDL[3, col_indx].Value.ToString(), 0.0);
                //complete_design.DeadLoads.LiveLoadOnFootPath.TotalNo = MyList.StringToInt(dgv_SIDL[4, row_indx].Value.ToString(), 0);
                //complete_design.DeadLoads.LiveLoadOnFootPath.Gamma = MyList.StringToDouble(dgv_SIDL[6, row_indx].Value.ToString(), 0);
            }
            else
            {
                complete_design.DeadLoads.IsRailLoad = true;
                row_indx = 0;
                complete_design.DeadLoads.Rail.EffectiveSpan = MyList.StringToDouble(txt_rail_eff_span.Text, 0.0);
                complete_design.DeadLoads.Rail.PermanentLoad = MyList.StringToDouble(txt_rail_permanent_load.Text, 0.0);
                complete_design.DeadLoads.Rail.BendingMoment = MyList.StringToDouble(txt_rail_bending_moment.Text, 0.0);
                complete_design.DeadLoads.Rail.ShearForce = MyList.StringToDouble(txt_rail_shear_force.Text, 0.0);

            }

            complete_design.NoOfJointsAtTrussFloor = MyList.StringToInt(txt_cd_total_joints.Text, 0);
            complete_design.AddWeightPercent = MyList.StringToDouble(txt_cd_force_percent.Text, 0.0);

        }

        public void AddMemberRow(CMember member)
        {
            string kStr = "";
            try
            {
                member.Force = Truss_Analysis.GetForce(ref member);
                member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);
                member.WeightPerMetre = GetWeightPerMetre(member);

                bool flag = false;
                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    kStr = dgv_mem_details[0, i].Value.ToString();
                    if (kStr == member.Group.GroupName)
                    {
                        dgv_mem_details[0, i].Value = member.Group.GroupName;
                        dgv_mem_details[1, i].Value = (member.MemberType == eMemberType.NoSelection) ? "" : member.MemberType.ToString();
                        dgv_mem_details[2, i].Value = (member.SectionDetails.DefineSection == eDefineSection.NoSelection) ? "" : member.SectionDetails.DefineSection.ToString();
                        dgv_mem_details[3, i].Value = member.NoOfMember.ToString("0");
                        dgv_mem_details[4, i].Value = member.Length.ToString("0.000");
                        dgv_mem_details[5, i].Value = member.WeightPerMetre.ToString("0.0000");
                        dgv_mem_details[6, i].Value = member.Weight.ToString("0.0000");
                        dgv_mem_details[7, i].Value = member.Force;
                        return;
                    }
                }
                dgv_mem_details.Rows.Add(
                               member.Group.GroupName,
                               (member.MemberType == eMemberType.NoSelection) ? "" : member.MemberType.ToString(),
                               (member.SectionDetails.DefineSection == eDefineSection.NoSelection) ? "" : member.SectionDetails.DefineSection.ToString(),
                               member.NoOfMember.ToString("0"),
                               member.Length.ToString("0.000"),
                               member.WeightPerMetre.ToString("0.0000"),
                               member.Weight.ToString("0.0000"),
                               member.Force);
                dgv_mem_details.FirstDisplayedScrollingRowIndex = dgv_mem_details.RowCount - 1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(kStr + " :" + ex.Message);
            }
        }
        public void AddDeadLoadRow(SuperInposedDeadLoad dead_load)
        {
            dgv_SIDL.Rows.Add(
                           dead_load.Name,
                           dead_load.Length.ToString("0.000"),
                           dead_load.Breadth.ToString("0.000"),
                           dead_load.Depth.ToString("0.000"),
                           dead_load.TotalNo,
                           dead_load.Volume.ToString("0.000"),
                           dead_load.Gamma.ToString("0.000"),
                           dead_load.Weight.ToString("0.000"));
        }
        public void Add_SectionsData(ref CMember member)
        {
            member.SectionDetails.TopPlate.TotalPlates = 1;
            member.SectionDetails.BottomPlate.TotalPlates = 1;
            member.SectionDetails.SidePlate.TotalPlates = 2;
            member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;

            switch (member.Group.GroupName)
            {
                case "_L0L1":
                case "_L1L2":
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 600.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 0.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0.0;

                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    //mem.
                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";
                    //txt_sp_wd.Text = "600";
                    //txt_sp_thk.Text = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";
                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";

                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2L3":
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 600.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 10.0;

                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_vsp_wd.Text = "300";
                    //txt_vsp_thk.Text = "10";
                    //txt_sp_wd.Text = "600";
                    //txt_sp_thk.Text = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";
                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";

                    break;
                case "_L3L4":


                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 600.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_vsp_wd.Text = "300";
                    //txt_vsp_thk.Text = "25";
                    //txt_sp_wd.Text = "600";
                    //txt_sp_thk.Text = "10";
                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";
                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4L5":



                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 30.0;

                    member.SectionDetails.SidePlate.Width = 600.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_vsp_wd.Text = "300";
                    //txt_vsp_thk.Text = "30";
                    //txt_sp_wd.Text = "600";
                    //txt_sp_thk.Text = "10";
                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";
                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_U1U2":


                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;

                    member.SectionDetails.TopPlate.Width = 420.0;
                    member.SectionDetails.TopPlate.Thickness = 16.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 12.0d;

                    //txt_tp_width.Text = "420";
                    //txt_tp_thk.Text = "16";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_U2U3":

                    member.SectionDetails.NoOfElements = 4.0;

                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 420.0;
                    member.SectionDetails.TopPlate.Thickness = 16.0;

                    member.SectionDetails.SidePlate.Width = 350.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 12.0d;

                    //txt_tp_width.Text = "420";
                    //txt_tp_thk.Text = "16";

                    //txt_sp_wd.Text = "350";
                    //txt_sp_thk.Text = "16";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_U3U4":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 400.0;
                    member.SectionDetails.TopPlate.Thickness = 16.0;

                    member.SectionDetails.SidePlate.Width = 350.0;
                    member.SectionDetails.SidePlate.Thickness = 25.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 12.0d;

                    //txt_tp_width.Text = "400";
                    //txt_tp_thk.Text = "16";

                    //txt_sp_wd.Text = "350";
                    //txt_sp_thk.Text = "25";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_U4U5":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 420.0;
                    member.SectionDetails.TopPlate.Thickness = 16.0;

                    member.SectionDetails.SidePlate.Width = 350.0;
                    member.SectionDetails.SidePlate.Thickness = 30.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 12.0d;

                    //txt_tp_width.Text = "420";
                    //txt_tp_thk.Text = "16";

                    //txt_sp_wd.Text = "350";
                    //txt_sp_thk.Text = "30";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L1U1":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "200";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "200";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U2":

                    member.SectionDetails.NoOfElements = 2.0;

                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "400";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "400";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U3":

                    member.SectionDetails.NoOfElements = 2.0;

                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "300";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U4":
                    member.SectionDetails.NoOfElements = 2.0;

                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "200";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "200";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L5U5":

                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "150";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "150";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_ER":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 430.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "430";
                    //txt_sp_thk.Text = "12";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U1":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "400";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 320.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "400";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "320";
                    //txt_sp_thk.Text = "10";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U2":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 220.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "300";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "220";
                    //txt_sp_thk.Text = "10";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U3":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "300";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L5U4":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "200";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "200";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_TCS_ST":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section7;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 8.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 180.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section7;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 8.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "180";
                    break;
                case "_TCS_DIA":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section7;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 8.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 180.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section7;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 8.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "180";
                    break;
                case "_BCB":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_BCB1":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_BCB2":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_STRINGER":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section5;
                    member.SectionDetails.SectionName = "ISMB";
                    member.SectionDetails.SectionCode = "450";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 125.0;
                    member.SectionDetails.BottomPlate.Thickness = 10.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section5;
                    //cmb_code1.SelectedItem = "450";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "125";
                    //txt_bp_thk.Text = "10";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_IN":
                case "_XGIRDER":

                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section6;
                    member.SectionDetails.SectionName = "ISMB";
                    member.SectionDetails.SectionCode = "600";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 300.0;
                    member.SectionDetails.BottomPlate.Thickness = 20.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    //cmb_code1.SelectedItem = "600";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "300";
                    //txt_bp_thk.Text = "20";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_END":

                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section6;
                    member.SectionDetails.SectionName = "ISMB";
                    member.SectionDetails.SectionCode = "600";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 490.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 300.0;
                    member.SectionDetails.BottomPlate.Thickness = 20.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    //cmb_code1.SelectedItem = "600";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "490";
                    //txt_vsp_thk.Text = "12";

                    //txt_bp_wd.Text = "300";
                    //txt_bp_thk.Text = "20";
                    //txt_sec_lat_spac.Text = "0";
                    break;
            }
        }



        bool DeleteMember(string group_name)
        {
            int row_indx = Complete_Design.Members.IndexOf(group_name);
            if (row_indx != -1)
            {
                Complete_Design.Members.RemoveAt(row_indx);
                dgv_mem_details.Rows.RemoveAt(dgv_mem_details.CurrentCell.RowIndex);
                return true;
            }
            return false;
        }
        public bool SaveMemberLoads(string file_name)
        {
            complete_design.Is_Live_Load = chk_LL.Checked;
            complete_design.Is_Super_Imposed_Dead_Load = chk_SIDL.Checked;
            complete_design.Is_Dead_Load = chk_DL.Checked;
            bool sucess = false;
            //string title_line1, title_line2;
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                if (dgv_SIDL.Rows.Count != 0)
                    ReadDeadLoadInputs();
                complete_design.ToStream(sw);
                sucess = true;
                Write_DL_SIDL();
                Write_Live_Load();
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            return sucess;
        }

        void FillMemberGroup()
        {
            if (Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count == 0) return;

            string kStr = "";
            cmb_mem_group.Items.Clear();
            int i = 0;
            CMember member = null;
            for (i = 0; i < Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count; i++)
            {
                kStr = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].GroupName;

                member = new CMember();
                member.Group.GroupName = kStr;
                member.Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].MemberNosText;
                member.Group.SetMemNos();
                member.MemberType = Get_MemberType(kStr);
                member.WeightPerMetre = GetWeightPerMetre();


                member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);

                member.Force = Truss_Analysis.GetForce(ref member);

                Add_SectionsData(ref member);
                member.SectionDetails.TopPlate.Length = member.Length;
                member.SectionDetails.BottomPlate.Length = member.Length;
                member.SectionDetails.SidePlate.Length = member.Length;
                member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;


                complete_design.Members.Add(member);
                AddMemberRow(member);

                if (!cmb_mem_group.Items.Contains(kStr))
                    cmb_mem_group.Items.Add(kStr);

            }
        }
        void SetMemberLength()
        {
            if (Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count == 0) return;

            string kStr = "";
            cmb_mem_group.Items.Clear();
            int i = 0;
            CMember member = null;
            for (i = 0; i < Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count; i++)
            {
                kStr = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].GroupName;

                member = new CMember();
                member.Group.GroupName = kStr;
                member.Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].MemberNosText;
                member.Group.SetMemNos();
                member.MemberType = Get_MemberType(kStr);
                member.WeightPerMetre = GetWeightPerMetre();


                member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);

                member.Force = Truss_Analysis.GetForce(ref member);

                //Add_SectionsData(ref member);
                member.SectionDetails.TopPlate.Length = member.Length;
                member.SectionDetails.BottomPlate.Length = member.Length;
                member.SectionDetails.SidePlate.Length = member.Length;
                member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;


                complete_design.Members.Add(member);
                AddMemberRow(member);

                if (!cmb_mem_group.Items.Contains(kStr))
                    cmb_mem_group.Items.Add(kStr);

            }
        }


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
                    case 9:
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
                    case 8:
                    case 10:
                    case 11:
                    case 12:
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
        public void OpenAnalysisFile(string file_name)
        {
            string analysis_file = file_name;
            if (File.Exists(analysis_file))
            {
                btn_View_Structure.Enabled = true;

                FilePath = Path.GetDirectoryName(file_name);
                string rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
                if (File.Exists(rep_file))
                    Truss_Analysis = new SteelTrussMemberAnalysis(iApp, rep_file);
                else
                    Truss_Analysis = new SteelTrussMemberAnalysis(iApp, analysis_file);


                if (Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count == 0)
                {
                    MessageBox.Show(this, "Member Groups are not found in data file.\nPlease define Group Defination in data file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Process.Start(analysis_file);
                    return;
                }
                if (cmb_mem_group.Items.Count > 0)
                    cmb_mem_group.SelectedIndex = 0;
                //SetCompleteDesign(Path.Combine(Path.GetDirectoryName(analysis_file), "MEMBER_LOAD_DATA.txt"));
                txt_L.Text = Truss_Analysis.Analysis.Length.ToString("0.00");
                txt_X.Text = "-" + txt_L.Text;
                txt_B.Text = Truss_Analysis.Analysis.Width.ToString("0.00");
                txt_H.Text = Truss_Analysis.Analysis.Height.ToString("0.00");
                txt_cd_total_joints.Text = Truss_Analysis.Analysis.Joints.Get_Total_Joints_At_Truss_Floor().ToString();
                txt_gd_np.Text = (Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                txt_analysis_file.Visible = true;
                txt_analysis_file.Text = analysis_file;

                rbtn_rail_bridge.Checked = Complete_Design.DeadLoads.IsRailLoad;
                rbtn_road_bridge.Checked = !rbtn_rail_bridge.Checked;
                //SetDefaultDeadLoad();
                if (dgv_mem_details.RowCount == 0)
                {
                    FillMemberGroup();
                    SetDefaultDeadLoad();
                }

                //string kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
                string kFile = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");
                if (!File.Exists(kFile))
                {
                    kFile = Path.Combine(system_path, "MEMBER_LOAD_REP.txt");
                } 
                
                if (!File.Exists(kFile))
                {
                    kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
                }
                if (File.Exists(kFile))
                {
                    SetCompleteDesign(kFile);
                    ReadResult();
                    //Read_DL_SIDL();
                    Read_Live_Load();
                }

                MessageBox.Show(this, "File opened successfully.");
            }
            Format_SIDL();
            FillResultGridWithColor();
            Button_Enable_Disable();
            string ll_txt = Path.Combine(user_path, "LL.txt");

            Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Live_Load_List == null) return;

            cmb_load_type.Items.Clear();
            cmb_custom_LL_type.Items.Clear();
            for (int i = 0; i < Live_Load_List.Count; i++)
            {
                cmb_load_type.Items.Add(Live_Load_List[i].TypeNo + " : " + Live_Load_List[i].Code);
                cmb_custom_LL_type.Items.Add(Live_Load_List[i].TypeNo + " : " + Live_Load_List[i].Code);
            }
            if (cmb_load_type.Items.Count > 1)
            {
                //cmb_load_type.SelectedIndex = cmb_load_type.Items.Count - 1;
                cmb_load_type.SelectedIndex = 1;
                if (dgv_live_load.RowCount == 0)
                {
                    dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), "-16.9160", 0, 1.5, 0.5);
                    dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), "-16.9160", 0, 4.5, 0.5);
                    //Add_LiveLoad();
                }
            }
        }
        public void SetLoadSetting()
        {
            string file_name = txt_analysis_file.Text;
            if (!File.Exists(file_name)) return;

            file_name = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
            if (!File.Exists(file_name)) return;

            List<string> file_cont = new List<string>(File.ReadAllLines(file_name));
            MyList mList = null;
            for (int i = 0; i < file_cont.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(file_cont[i].ToUpper()), ' ');
                if (mList.Count == 2 && mList.StringList[0].Contains("TYPE"))
                {

                }
            }

        }
        void Button_Enable_Disable()
        {
            try
            {
                btn_View_Structure.Enabled = File.Exists(Truss_Analysis.Analysis_File);
                btn_input_open.Enabled = File.Exists(Truss_Analysis.Analysis_File);
                btn_open_analysis.Enabled = File.Exists(Path.Combine(user_path, "ANALYSIS_REP.TXT"));
                btn_open_member_load.Enabled = File.Exists(Path.Combine(user_path, "MEMBER_LOAD_DATA.txt"));
            }
            catch (Exception ex) { }
        }
        void SetMemberDetails()
        {
            for (int i = 0; i < complete_design.Members.Count; i++)
            {
                try
                {
                    complete_design.Members[i].Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(complete_design.Members[i].Group.GroupName).MemberNosText;
                }
                catch (Exception ex)
                {
                    complete_design.Members[i].Group.MemberNosText = "";

                }
                complete_design.Members[i].Group.SetMemNos();
                //Truss_Analysis.GetForce(ref complete_design.Members[i]);
            }
        }
        public void Set_Sections(string group_name)
        {

            switch (group_name)
            {
                case "_L0L1":
                case "_L1L2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";

                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2L3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "300";
                    txt_vsp_thk.Text = "10";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";

                    break;
                case "_L3L4":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "300";
                    txt_vsp_thk.Text = "25";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";
                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4L5":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "300";
                    txt_vsp_thk.Text = "30";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";
                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U1U2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "420";
                    txt_tp_thk.Text = "16";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U2U3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "420";
                    txt_tp_thk.Text = "16";

                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "16";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U3U4":

                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "400";
                    txt_tp_thk.Text = "16";

                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "25";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U4U5":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "420";
                    txt_tp_thk.Text = "16";

                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "30";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L1U1":

                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "200";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "400";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U4":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "200";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L5U5":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "150";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_ER":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "430";
                    txt_sp_thk.Text = "12";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U1":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "400";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "320";
                    txt_sp_thk.Text = "10";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "220";
                    txt_sp_thk.Text = "10";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L5U4":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "200";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_TCS_ST":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section7;
                    cmb_code1.SelectedItem = "9090";
                    cmb_sec_thk.SelectedItem = 8.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "180";
                    break;
                case "_TCS_DIA":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section7;
                    cmb_code1.SelectedItem = "9090";
                    cmb_sec_thk.SelectedItem = 8.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "180";
                    break;
                case "_BCB":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    cmb_code1.SelectedItem = "9090";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_STRINGER":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section5;
                    cmb_code1.SelectedItem = "450";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "125";
                    txt_bp_thk.Text = "10";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    cmb_code1.SelectedItem = "600";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_IN":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    cmb_code1.SelectedItem = "600";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_END":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    cmb_code1.SelectedItem = "600";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "490";
                    txt_vsp_thk.Text = "12";

                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sec_lat_spac.Text = "0";
                    break;
            }
        }
        public void TotalAnalysis(StreamWriter sw, ref CMember mem)
        {
            double Rxx, Cxx, a, Iyy, Ixx, Zxx, t, tf, D; // From Table

            double nb, n, S, bolt_dia;

            double tp, Bp, np; // Top Plate
            double tbp, Bbp, nbp; // Bottom Plate
            double ts, Bs, ns; // Side Plate
            double tss, Bss, nss; // Vertical Stiffener Plate
            double Z = 0.0;

            double Iy, Ix, A, Anet, ry;

            double M, F;
            RolledSteelAnglesRow tabAngle;
            RolledSteelBeamsRow tabBeam;
            RolledSteelChannelsRow tabChannel;


            string kStr = mem.Group.MemberNosText;
            MyList mList = new MyList(kStr, ' ');

            mem.Result = "OK";
            //AnalysisData ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mem.Group.GroupName];
            AnalysisData ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mList.GetInt(0)];
            //M = ana_data.MaxBendingMoment;
            //F = ana_data.MaxShearForce;


            M = mem.MaxMoment.Force;
            F = mem.MaxShearForce.Force;
            A = 0.0;
            Anet = 0.0;
            ry = 0.0;
            double ry_anet = 0.0;
            Iy = 0.0;
            //M = F = 0.0;


            nb = mem.SectionDetails.NoOfBolts;
            S = mem.SectionDetails.LateralSpacing;
            bolt_dia = mem.SectionDetails.BoltDia;
            n = mem.SectionDetails.NoOfElements;

            Bp = mem.SectionDetails.TopPlate.Width;
            tp = mem.SectionDetails.TopPlate.Thickness;
            np = mem.SectionDetails.TopPlate.TotalPlates;

            Bbp = mem.SectionDetails.BottomPlate.Width;
            tbp = mem.SectionDetails.BottomPlate.Thickness;
            nbp = mem.SectionDetails.BottomPlate.TotalPlates;

            Bs = mem.SectionDetails.SidePlate.Width;
            ts = mem.SectionDetails.SidePlate.Thickness;
            ns = mem.SectionDetails.SidePlate.TotalPlates;

            Bss = mem.SectionDetails.VerticalStiffenerPlate.Width;
            tss = mem.SectionDetails.VerticalStiffenerPlate.Thickness;
            nss = mem.SectionDetails.VerticalStiffenerPlate.TotalPlates;

            mem.DesignReport.Clear();

            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format("------------------------------------------------------------"));
            mem.DesignReport.Add(string.Format("DESIGN OF {0}", MemberString.GerMemberString(mem)));
            mem.DesignReport.Add(string.Format("------------------------------------------------------------"));
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format("MEMBER GROUP : {0}", mem.Group.GroupName));
            mem.DesignReport.Add(string.Format("-----------------------"));
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format("MEMBER NOS : {0}", mem.Group.MemberNosText));
            mem.DesignReport.Add(string.Format("-----------------------"));
            mem.DesignReport.Add(string.Format(""));

            if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
            {
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("AXIAL FORCE DATA"));
                mem.DesignReport.Add(string.Format("----------------"));
                mem.DesignReport.Add(string.Format(""));
                if (mem.MaxTensionForce.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN", mem.MaxTensionForce));
                }
                if (mem.MaxCompForce.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN", mem.MaxCompForce));
                    //mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE STRESS = {0:f3} kN/m^2", mem.MaxStress));
                }
                if (mem.MaxStress.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format("MAXIMUM STRESS = {0:f3} kN/m^2", mem.MaxStress));
                }

            }
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format("Length of Member = ly = {0:f3} m", mem.Length));
            mem.DesignReport.Add(string.Format("Diameter of Bolt = bolt_dia = {0} mm", bolt_dia));
            mem.DesignReport.Add(string.Format("No of Bolt in a Section = nb = {0} ", nb));
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));

            if (mem.SectionDetails.DefineSection != eDefineSection.Section11 &&
                mem.SectionDetails.DefineSection != eDefineSection.Section12 &&
                mem.SectionDetails.DefineSection != eDefineSection.Section13 &&
                mem.SectionDetails.DefineSection != eDefineSection.Section14)
            {
                #region Plate Details
                //mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                mem.DesignReport.Add(string.Format("---------------------"));
                mem.DesignReport.Add(string.Format(""));
                //for Ang
                if (mem.SectionDetails.SectionName.Contains("A"))
                {

                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                //for Beam
                else if (mem.SectionDetails.SectionName.Contains("B"))
                {
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                //for Channel
                else if (mem.SectionDetails.SectionName.Contains("C"))
                {
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                if ((Bp * tp) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                }
                if ((Bbp * tbp) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                }
                if ((Bs * ts) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    mem.DesignReport.Add(string.Format("No Of Side Plates = ns = {0}", ns));
                }
                if ((Bss * tss) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                }
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                #endregion Plate Details

            }



            #region Define Section
            switch (mem.SectionDetails.DefineSection)
            {
                case eDefineSection.Section1:
                    #region Section 1
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    mem.DesignReport.Add(string.Format("---------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));

                    //tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                   
                    
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));


                    //Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0);
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0)"));
                    //mem.DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + ({4} * {5} * {5} * {5} / 12.0)",
                    //    Iyy, a, S, n, tp, Bp));
                    //mem.DesignReport.Add(string.Format("                  = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));



                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (D / 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (D / 2)^2) * n"));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    // Top Plate
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3}  ", Iyy, a, D, n));
                    Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (D / 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]





                    A = n * a * 100 + (tp * Bp * np);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4})",
                        n, a, tp, Bp, np));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));


                    Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, tf, tp));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm ", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3}/ {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));


                    #endregion Section 1
                    break;
                case eDefineSection.Section2:
                    #region Section 2

                    //tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    
                    a = tabChannel.Area;
                    D = tabChannel.Depth;
                    Iyy = tabChannel.Iyy;
                    t = tabChannel.WebThickness;
                    tf = tabChannel.FlangeThickness;
                    Cxx = tabChannel.CentreOfGravity;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = np = {0}", nbp));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n 
                        + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp) 
                        + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;




                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]








                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {8}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
                    //    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns));

                    //mem.DesignReport.Add(string.Format("           = {0:E3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss) + (tbp * Bbp * nbp);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * (Math.Pow((S - t - (tp / 2.0)), 2))) * np;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp**3 / 12.0) * (S - t - (tp / 2.0))^2)) * np"));
                    //mem.DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + (({4} * {5}^3 / 12.0) * ({2} - {6} - ({4} / 2.0))^2)) * {7}",
                    //                                                              Iyy, a, S, n, tp, Bp, t, np));
                    //mem.DesignReport.Add(string.Format("                  = {0:f3} sq.sq.mm", Iy));
                    //mem.DesignReport.Add(string.Format(""));

                    //A = n * a * 100 + (tp * Bp * np));
                    //mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np)"));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4})", n, a, tp, Bp, np));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    //mem.DesignReport.Add(string.Format(""));
                    //Anet = A - nb * ((bolt_dia + 1.5) * (t + tp)));
                    //mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + tp))"));
                    //mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))", A, nb, bolt_dia, t, tp));
                    //mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    //mem.DesignReport.Add(string.Format(""));
                    ry_anet = Math.Sqrt(Iy / Anet);
                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / A)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:E3} / {1:f3})", Iy, A));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format("ry_Anet = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:E3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry_anet));

                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 2
                    break;
                case eDefineSection.Section3:
                    #region Section 3
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    //tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0}", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));

                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]




                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);;
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 3
                    break;
                case eDefineSection.Section4:
                    #region Section 4
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0}", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Centre of Gravity = Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + (tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    ////Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    //mem.DesignReport.Add(string.Format("Moment of Intertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("          Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns + (tss * Bss * (((S / 2) - t - (tss / 2.0))^2)) * nss"));
                    //mem.DesignReport.Add(string.Format("             = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10} + ({11} * {12} * ((({2} / 2) - {13} - ({11} / 2.0))^2)) * {14}",
                    //                                                                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns, tss, Bss, t, nss));


                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]
                   


                    //mem.DesignReport.Add(string.Format("             = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
                        n, a, tp, Bp, np, ts, Bs, ns, tss, Bss, nss));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));

                    #endregion Section 4
                    break;

                case eDefineSection.Section5: // Stringer
                    #region Section 5
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MOMENT & FORCE  DATA"));
                    mem.DesignReport.Add(string.Format("--------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxMoment.MemberNo, mem.MaxMoment.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN      [MemberNo = {1}, LoadNo = {2}]", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));
                    //mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m", M));
                    //mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN", F));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));

                    tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    Ixx = tabBeam.Ixx;
                    Zxx = tabBeam.Zxx;

                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = np = {0}", nbp));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm.", Iyy));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0}", tf));


                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));


                    // Chiranjit [2011 10 21] this formula is wrong
                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia

                    mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    // Top Plate
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0} * 10000 ", Ixx));
                    Ix = Ixx * 10000.0;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Ix += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Ix += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Ix += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Ix += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    
                  
                  
                    #endregion Sandiapan Goswami [2011 10 26]
                    
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + ((Bbp * tbp^3 / 12.0) + (tbp * Bbp) * ((((D / 2) + (tbp / 2))^2))) * nbp "));
                    //mem.DesignReport.Add(string.Format("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({2} / 2))^2))) * {4} ",
                    //                Ixx, Bbp, tbp, D, nbp));
                    //mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    mem.DesignReport.Add(string.Format(""));

                    //Ix = Ixx * 10000 + ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));
                    
                    A = n * a * 100 + (tbp * Bbp * nbp);

                    A = n * a * 100 + (tbp * Bbp * nbp) + (tss * Bss * nss);

                    Z = (M * 10e5) / sigma_b;
                    mem.DesignReport.Add(string.Format("Required Section Modulus = Zr = M/σ_b"));
                    mem.DesignReport.Add(string.Format("                         = {0}*10^6 / {1}", M, sigma_b));
                    mem.DesignReport.Add(string.Format("                         = {0:e3} cu.mm", Z));
                    mem.DesignReport.Add(string.Format(""));

                    double y = (D / 2) + tbp;
                    mem.DesignReport.Add(string.Format("Distance from Center to Bottom most edge of Section = y"));
                    mem.DesignReport.Add(string.Format("                    y = (D/2) + tbp"));
                    mem.DesignReport.Add(string.Format("                      = ({0}/2) + {1}", D, tbp));
                    mem.DesignReport.Add(string.Format("                      = {0:f3} mm", y));
                    mem.DesignReport.Add(string.Format(""));

                    double chk_Z = Ix / y;
                    mem.DesignReport.Add(string.Format("Section Modulus = Z = Ix/y"));
                    mem.DesignReport.Add(string.Format("                    = {0:e3}/{1}", Ix, y));
                    //mem.DesignReport.Add(string.Format("                    = {0:f3} cu.mm", chk_Z));
                    //mem.DesignReport.Add(string.Format(""));
                    mem.Capacity_SectionModulus = chk_Z;
                    mem.Required_SectionModulus = Z;

                    if (chk_Z < Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm < Zr ({1:e3} cu.mm) ,  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    else if (chk_Z > Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm > Zr ({1:e3} cu.mm)  ,  So, OK", chk_Z, Z));
                        //mem.Result = "OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm =  Zr ({1:e3} cu.mm) ,  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    mem.DesignReport.Add(string.Format(""));


                    //double shr_stress = (F * 1000.0) / (t * D);
                    //Chiranjit [2011 10 24] acording to Mr. S.Goswami this formula should be
                    //shr_stress = (F * 1000.0) / (t * D + tbp*bbp)
                    double shr_stress = (F * 1000.0) / (t * D + tbp * Bbp);
                    mem.DesignReport.Add(string.Format("Shear Stress = (F * 1000.0)/(t * D + tbp * Bbp)"));
                    mem.DesignReport.Add(string.Format("             = ({0} * 1000.0)/({1} * {2} + {3} * {4})", F, t, D, tbp , Bbp));
                    mem.Capacity_ShearStress = shr_stress;
                    mem.Required_ShearStress = sigma_c;

                    mem.MaxCompForce.Force = 0.0;
                    mem.MaxTensionForce.Force = 0.0;

                    if (shr_stress < sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm < Permissible Shear Stress (σ_c = {1} N/sq.mm)  , So, OK", shr_stress, sigma_c));
                        //mem.Result = "OK";
                    }
                    else if (shr_stress >= sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm > Permissible Shear Stress (σ_c = {1} N/sq.mm)  , So, NOT OK", shr_stress, sigma_c));
                        mem.Result = "NOT OK";
                    }
                    goto _SWWrite;
                    #endregion Section 5
                    return;
                    break;

                case eDefineSection.Section6: // Cross Girder
                    #region Section 6
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MOMENT & FORCE  DATA"));
                    mem.DesignReport.Add(string.Format("--------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxMoment.MemberNo, mem.MaxMoment.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN      [MemberNo = {1}, LoadNo = {2}]", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));

                    tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    Ixx = tabBeam.Ixx;
                    Zxx = tabBeam.Zxx;

                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Ixx = {0} sq.sq.mm", Ixx));


                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    // Chiranjit [2011 10 21] this formula is wrong
                    //Ix = Ixx * 10000 + ((tbp * Bbp * Bbp * Bbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));
                    Ix = Ixx * 10000 + ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));

                    Ix = Ixx * 10000.0;
                    Ix += ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0)));
                    Ix += (tss * Bss * Bss * Bss / 12.0) * nss;


                    #region Chiranjit [2011 10 26]
                    //According to Mr. Sandipan Goswami
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + ((Bbp * tbp^3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)))"));
                    //mem.DesignReport.Add(string.Format("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({2} / 2))^2))) * {4} + ({5} * ({6} * {7}) * (({8} / 2.0) + ({6} / 2.0))) ",
                    //                                                                                                            Ixx, Bbp, tbp, D, nbp, nss, tss, Bss, t));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    //mem.DesignReport.Add(string.Format(""));
                    #endregion Chiranjit [2011 10 26]
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + (Bbp * tbp^3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    //mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12.0) * nss"));
                    //mem.DesignReport.Add(string.Format("                       = {0} * 10000 + ({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + (tbp / 2))^2)) * {4}", Ixx, Bbp, tbp, D, nbp));
                    //mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    #region Sandiapan Goswami [2011 10 26]   Moment of Inertia

                    mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = {0} * 10000 ", Ixx));
                    Ix = Ixx * 10000.0;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Ix += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Ix += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Ix += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Ix += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));



                    #endregion Sandiapan Goswami [2011 10 26]

                    mem.DesignReport.Add(string.Format(""));
                   
                    Z = (M * 10e5) / sigma_b;
                    mem.DesignReport.Add(string.Format("Required Section Modulus = Zp = M/σ_b"));
                    mem.DesignReport.Add(string.Format("                         = {0}*10^6 / {1}", M, sigma_b));
                    mem.DesignReport.Add(string.Format("                         = {0:e3} cu.mm", Z));
                    mem.DesignReport.Add(string.Format(""));

                    y = (D / 2) + tbp;
                    mem.DesignReport.Add(string.Format("Distance from Center to Bottom most edge of Section = y"));
                    mem.DesignReport.Add(string.Format("                    y = (D/2) + tbp"));
                    mem.DesignReport.Add(string.Format("                      = ({0}/2) + {1}", D, tbp));
                    mem.DesignReport.Add(string.Format("                      = {0:f3} mm", y));
                    mem.DesignReport.Add(string.Format(""));

                    chk_Z = Ix / y;
                    mem.DesignReport.Add(string.Format("Section Modulus = Z = Ix/y"));
                    mem.DesignReport.Add(string.Format("                    = {0:e3}/{1}", Ix, y));
                    mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm", chk_Z));
                    mem.DesignReport.Add(string.Format(""));
                    mem.Required_SectionModulus = Z;
                    mem.Capacity_SectionModulus = chk_Z;

                    if (chk_Z > Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm > Zp ({1:e3} cu.mm),  So, OK", chk_Z, Z));
                        //mem.Result = "OK";
                    }
                    else if (chk_Z < Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm < Zp ({1:e3} cu.mm),  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm = Zp ({1:e3} cu.mm) ,  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    mem.DesignReport.Add(string.Format(""));


                    shr_stress = (F * 1000.0) / (t * D + tbp*Bbp);
                    mem.DesignReport.Add(string.Format("Shear Stress = (F * 1000.0) /(t * D + tbp * Bbp)"));
                    mem.DesignReport.Add(string.Format("             = ({0:f3} * 1000.0) / ({1} * {2} + {3} * {4})", F, t, D, tbp , Bbp));
                    mem.Capacity_ShearStress = shr_stress;
                    mem.Required_ShearStress = sigma_c;

                    mem.MaxCompForce.Force = 0.0;
                    mem.MaxTensionForce.Force = 0.0;
                    if (shr_stress < sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm <  Permissible Shear Stress (σ_c = {1} N/sq.mm) , So, OK", shr_stress, sigma_c));
                        //mem.Result = "OK";
                    }
                    else if (shr_stress >= sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm > Permissible Shear Stress (σ_c = {1} N/sq.mm)  , So, NOT OK", shr_stress, sigma_c));
                        mem.Result = "NOT OK";
                    }
                    goto _SWWrite;
                    #endregion Section 6
                    return;
                    break;
                case eDefineSection.Section7:
                    #region Section 7
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0} mm", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0} mm", ns));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0} mm", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));


                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy "));
                    //mem.DesignReport.Add(string.Format("      Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    //mem.DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
                    //                Iyy, a, S, Cxx, n));





                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]
                    




                    
                    
                    //mem.DesignReport.Add(string.Format("         = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    mem.DesignReport.Add(string.Format("A = n * a * 100"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100",
                        n, a));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * t)"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
                        A, nb, bolt_dia, t));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));

                    #endregion Section 7
                    break;
                case eDefineSection.Section8:
                    #region Section 8
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0} mm", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0} mm", ns));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0} mm", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy "));
                    //mem.DesignReport.Add(string.Format("    Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    //mem.DesignReport.Add(string.Format("       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
                    //                Iyy, a, S, Cxx, n));





                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]
                 







                    //mem.DesignReport.Add(string.Format("       = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    mem.DesignReport.Add(string.Format("A = n * a * 100"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100",
                        n, a));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * t)"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
                        A, nb, bolt_dia, t));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));

                    #endregion Section 8
                    break;
                case eDefineSection.Section9:
                    #region Section 9
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));
                    //mem.DesignReport.Add(string.Format("           = {0:f3} sq.sq.mm", Iy));




                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]
                   





                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 9
                    break;
                case eDefineSection.Section10:
                    #region Section 10

                    tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    a = tabChannel.Area;
                    D = tabChannel.Depth;
                    Iyy = tabChannel.Iyy;
                    t = tabChannel.WebThickness;
                    tf = tabChannel.FlangeThickness;
                    Cxx = tabChannel.CentreOfGravity;
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {9}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns));

                    //mem.DesignReport.Add(string.Format("           = {0:E3} sq.sq.mm", Iy));







                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]
                  







                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
                        n, a, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));

                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 10
                    break;
                case eDefineSection.Section11:
                    #region Section 11
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = 2 * Iyy * 10000;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = 2 * Iyy *10000"));
                    mem.DesignReport.Add(string.Format("                       = 2 * {0} *10000", Iyy));
                    mem.DesignReport.Add(string.Format("                       = {0} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));

                    //mem.DesignReport.Add(string.Format("           = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = 2 * a * 100;
                    Anet = A;
                    mem.DesignReport.Add(string.Format("A = 2 * a a * 100 = 2 * {0} a * 100 = {1} sq.mm", a, A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 11
                    break;
                case eDefineSection.Section12:
                    #region Section 12
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = 2 * Iyy * 10000;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = 2 * Iyy * 10000"));
                    mem.DesignReport.Add(string.Format("                       = 2 * {0} * 10000", Iyy));
                    mem.DesignReport.Add(string.Format("                       = {0} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = 2 * a * 100;
                    Anet = A;
                    mem.DesignReport.Add(string.Format("A = 2 * a * 100 = 2 * {0} * 100 = {1} sq.mm", a, A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;

                case eDefineSection.Section13:
                    #region Section 13
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    mem.DesignReport.Add(string.Format("---------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;


                    Bp = Bs;
                    tp = ts;



                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    mem.DesignReport.Add(string.Format("Central Plate Width = Bp = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format("Central Plate Thickness = tp = {0} mm", tp));
                    mem.DesignReport.Add(string.Format("No Of Central Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} mm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + tp / 2), 2.0)) + tp * (Bp * Bp * Bp / 12.0) * np;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np"));
                    mem.DesignReport.Add(string.Format("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5} * {5} * {5} / 12.0) * {6}", n,
                        Iyy, a, Cxx, tp, Bp, np));
                    mem.DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = a * n * 100 + tp * Bp * np;
                    mem.DesignReport.Add(string.Format("A = {0} * {1} * 100 + {2} * {3} * {4} = {5:f3} sq.mm", a, n, tp, Bp, np, A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));


                    Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp);
                    mem.DesignReport.Add(string.Format("Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp)"));
                    mem.DesignReport.Add(string.Format("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, tp));
                    mem.DesignReport.Add(string.Format("     = {0} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;
                case eDefineSection.Section14:
                    #region Section 14
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    mem.DesignReport.Add(string.Format("---------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;


                    ns = 1;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    mem.DesignReport.Add(string.Format("Central Plate Width = Bp = {0} mm", Bs));
                    mem.DesignReport.Add(string.Format("Central Plate Thickness = tp = {0} mm", ts));
                    mem.DesignReport.Add(string.Format("No Of Central Plate = np = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Top Plate Width = Bs = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format("Top Plate Thickness = ts = {0} mm", tp));
                    mem.DesignReport.Add(string.Format("No Of Top Plate = ns = {0}", np));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Bottom Plate Width = Bs = {0} mm", Bbp));
                    mem.DesignReport.Add(string.Format("Bottom Plate Thickness = ts = {0} mm", tbp));
                    mem.DesignReport.Add(string.Format("No Of Bottom Plate = ns = {0}", nbp));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} mm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + ts / 2), 2.0)) + ts * (Bs * Bs * Bs / 12.0) * ns + tp * (Bp * Bp * Bp / 12.0) * np + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np + ts * (Bs * Bs * Bs / 12.0) * ns + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp"));
                    mem.DesignReport.Add(string.Format("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5}^3 / 12.0) * {6} + {7} * ({8}^3 / 12.0) * {9} + {10} * ({11}^3 / 12.0) * {12}", n,
                        Iyy, a, Cxx, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp));
                    mem.DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns;
                    mem.DesignReport.Add(string.Format("A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + {2} * {3} * {4} + {5} * {6} * {7} + {8} * {9} * {10}", a, n, tp, Bp, np, tbp, Bbp, nbp, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));


                    Anet = A - ((bolt_dia + 1.5) * n * (2 * t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - (bolt_dia + 1.5) * n * (2 * t + ts)"));
                    mem.DesignReport.Add(string.Format("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:E3} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;
            }
            #endregion Define Section


            if (mem.MaxCompForce.Force == 0.0) mem.Compressive_Stress = 0.0;
            if (mem.MaxTensionForce.Force == 0.0) mem.Tensile_Stress = 0.0;
            if (mem.MaxCompForce.Force != 0.0)
            {
                #region Compression
                //double L = mem.Length;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("COMPRESSIVE LOAD CARRYING CAPACITY"));
                mem.DesignReport.Add(string.Format("-----------------------------------------------------------------------------"));
                mem.DesignReport.Add(string.Format(""));
                if (mem.MaxCompForce.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN    [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Force, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE STRESS = {0:f3} kN/m^2    [MemberNo = {1} , LoadNo = {2}]", mem.MaxStress.Force, mem.MaxStress.MemberNo, mem.MaxStress.Loadcase));
                }
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));

                double ly = 0.85 * mem.Length * 1000;
                //Chiranjit [2011 10 20]  
                //if (mem.MemberType == eMemberType.TopChord)
                //{
                //    ly = mem.Length * 1000;
                //    mem.DesignReport.Add(string.Format("Effective Length = ly = {0:f3} mm", ly));
                //}
                //else
                //{
                mem.DesignReport.Add(string.Format("Effective Length = 0.85*ly = 0.85 * {0:f3} = {1:f3} mm", mem.Length * 1000, ly));
                //}
                double lamda = ly / ry;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("   λ  = ly / ry"));
                mem.DesignReport.Add(string.Format("      = {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda));

                double sigma = Get_Table_1_Sigma_Value(lamda, fy);
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("From Table 4,   σ_ac = {0} N/mm^2", sigma));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));

                //double comp_load_cap = sigma * (Anet / 1000.0));
                double comp_load_cap = sigma * (A / 1000.0);


                //Chiranjit [2011 07 01] 
                //Store Compressive Stress
                //mem.Compressive_Stress = (mem.MaxCompForce * 1000) / (A);
                mem.Capacity_Compressive_Stress = sigma;



                mem.DesignReport.Add(string.Format("Compressive Load Carrying Capacity = σ_ac * A   N"));
                mem.DesignReport.Add(string.Format("                                   = ({0}*{1:f3})/1000   kN", sigma, A));
                mem.Capacity_CompForce = comp_load_cap;

                if (comp_load_cap > mem.MaxCompForce.Force)
                {
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Compressive Force OK", comp_load_cap, mem.MaxCompForce.Force, mem.Group.MemberNosText));
                    mem.Result = "OK";
                }
                else if (comp_load_cap < mem.MaxCompForce.Force)
                {

                    //mem.DesignReport.Add(string.Format("                                   = {0:f3} kN < {1:f3} kN, Design Compressive Force, NOT OK", comp_load_cap, mem.MaxCompForce));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN < {1:f3} kN", comp_load_cap, mem.MaxCompForce.Force));
                   
                }
                else
                {
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                }

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Compressive Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/mm^2", mem.MaxStress.Force, (mem.MaxStress.Force / 1000.0)));
                mem.DesignReport.Add(string.Format(""));
                if ((sigma) > (mem.MaxStress.Force / 1000.0))
                {
                    //mem.DesignReport.Add(string.Format("Maximum Compressive Stress = {0:f3} N/mm^2", mem.MaxStress/1000.0));
                    mem.Compressive_Stress = (mem.MaxStress.Force / 1000.0);
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Compressive Stress OK", mem.MaxStress.Force / 1000.0, sigma));
                    mem.Result = "OK";
                }
                else
                {
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress   NOT OK", mem.MaxStress.Force / 1000.0, sigma));
                    mem.Result = "NOT OK";
                }


                if (n > 1 || mem.SectionDetails.LateralSpacing > 0)
                    DesignLacing(sw, mem, lamda);
                #endregion Compression
            }
            if (mem.MaxTensionForce.Force != 0.0)
            {
                #region Tensile
                //mem.DesignReport.Add(string.Format(""));
                //mem.DesignReport.Add(string.Format("Tensile Load Carrying Capacity = "));
                double tensile_load_cap = (Anet * ft) / 1000.0;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("TENSILE LOAD CARRYING CAPACITY"));
                mem.DesignReport.Add(string.Format("------------------------------"));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.Capacity_TensionForce = tensile_load_cap;

                mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN    [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce.Force, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("MAXIMUM TENSILE STRESS = {0:f3} kN/m^2    [MemberNo = {1} , LoadNo = {2}]", mem.MaxStress, mem.MaxStress.MemberNo, mem.MaxStress.Loadcase));
                mem.DesignReport.Add(string.Format(""));


                //Chiranjit [2011 07 01] 
                //Store Tensile Stress
                mem.Capacity_Tensile_Stress = ft;
                //mem.Tensile_Stress = (mem.MaxTensionForce*1000) / (Anet);



                mem.DesignReport.Add(string.Format("Tensile Load Carrying Capacity = Anet * ft   N"));
                mem.DesignReport.Add(string.Format("                               = ({0:f3}*{1})/1000   kN", Anet, ft));
                bool flag = tensile_load_cap > mem.MaxTensionForce.Force;
                if (tensile_load_cap > mem.MaxTensionForce.Force)
                {

                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Tensile Force OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "OK";
                }
                else if (tensile_load_cap < mem.MaxTensionForce.Force)
                {
                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN < {1:f3} kN, Maximum Group [{2}] Tensile Force", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                }
                else
                {
                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Tensile Force NOT OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                }

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Tensile Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/mm^2", mem.MaxStress.Force, (mem.MaxStress.Force / 1000.0)));
                mem.DesignReport.Add(string.Format(""));
                if ((ft) > (mem.MaxStress.Force / 1000.0))
                {
                    //mem.DesignReport.Add(string.Format("Maximum Compressive Stress = {0:f3} N/mm^2", mem.MaxStress/1000.0));
                    mem.Tensile_Stress = (mem.MaxStress.Force / 1000.0);
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Tensile Stress  OK", mem.MaxStress.Force / 1000.0, ft));
                    mem.Result = "OK";
                }
                else
                {
                    if (flag)
                    {
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress", mem.MaxStress.Force / 1000.0, ft));
                    }

                    else
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress  NOT OK", mem.MaxStress.Force / 1000.0, ft));
                    mem.Result = flag ? "  OK" : "NOT OK";
                }

                DesignConnection(sw, mem);

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));


                #endregion Tensile
            }
           

            //sw.Write(mem.DesignReport.ToArray());

            _SWWrite :
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            foreach (string s in mem.DesignReport) sw.WriteLine(s);
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
        }

#region Chiranjit [2011_10_21]
        //public void TotalAnalysis_2011_10_21(StreamWriter sw, ref CMember mem)
        //{
        //    double Rxx, Cxx, a, Iyy, Ixx, Zxx, t, tf, D; // From Table

        //    double nb, n, S, bolt_dia;

        //    double tp, Bp, np; // Top Plate
        //    double tbp, Bbp, nbp; // Bottom Plate
        //    double ts, Bs, ns; // Side Plate
        //    double tss, Bss, nss; // Vertical Stiffener Plate
        //    double Z = 0.0;

        //    double Iy, Ix, A, Anet, ry;

        //    double M, F;
        //    RolledSteelAnglesRow tabAngle;
        //    RolledSteelBeamsRow tabBeam;
        //    RolledSteelChannelsRow tabChannel;


        //    string kStr = mem.Group.MemberNosText;
        //    MyList mList = new MyList(kStr, ' ');

        //    mem.Result = "OK";
        //    //AnalysisData ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mem.Group.GroupName];
        //    AnalysisData ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mList.GetInt(0)];
        //    M = ana_data.MaxBendingMoment.Force;
        //    F = ana_data.MaxShearForce.Force;
        //    A = 0.0;
        //    Anet = 0.0;
        //    ry = 0.0;
        //    double ry_anet = 0.0;
        //    Iy = 0.0;
        //    //M = F = 0.0;


        //    nb = mem.SectionDetails.NoOfBolts;
        //    S = mem.SectionDetails.LateralSpacing;
        //    bolt_dia = mem.SectionDetails.BoltDia;
        //    n = mem.SectionDetails.NoOfElements;

        //    Bp = mem.SectionDetails.TopPlate.Width;
        //    tp = mem.SectionDetails.TopPlate.Thickness;
        //    np = mem.SectionDetails.TopPlate.TotalPlates;

        //    Bbp = mem.SectionDetails.BottomPlate.Width;
        //    tbp = mem.SectionDetails.BottomPlate.Thickness;
        //    nbp = mem.SectionDetails.BottomPlate.TotalPlates;

        //    Bs = mem.SectionDetails.SidePlate.Width;
        //    ts = mem.SectionDetails.SidePlate.Thickness;
        //    ns = mem.SectionDetails.SidePlate.TotalPlates;

        //    Bss = mem.SectionDetails.VerticalStiffenerPlate.Width;
        //    tss = mem.SectionDetails.VerticalStiffenerPlate.Thickness;
        //    nss = mem.SectionDetails.VerticalStiffenerPlate.TotalPlates;


        //    sw.WriteLine();
        //    sw.WriteLine();
        //    sw.WriteLine();
        //    sw.WriteLine("------------------------------------------------------------");
        //    sw.WriteLine("DESIGN OF {0}", MemberString.GerMemberString(mem));
        //    sw.WriteLine("------------------------------------------------------------");
        //    sw.WriteLine();
        //    sw.WriteLine("MEMBER GROUP : {0}", mem.Group.GroupName);
        //    sw.WriteLine("-----------------------");
        //    sw.WriteLine();
        //    sw.WriteLine("MEMBER NOS : {0}", mem.Group.MemberNosText);
        //    sw.WriteLine("-----------------------");
        //    sw.WriteLine();

        //    if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
        //    {
        //        sw.WriteLine();
        //        sw.WriteLine("AXIAL FORCE DATA");
        //        sw.WriteLine("----------------");
        //        sw.WriteLine();
        //        if (mem.MaxTensionForce != 0.0)
        //        {
        //            sw.WriteLine("MAXIMUM TENSILE FORCE = {0:f3} kN", mem.MaxTensionForce);
        //            sw.WriteLine("Member No = {0} ", ana_data.TensileForce.MemberNo);
        //            sw.WriteLine("Loadcase = {0} ", ana_data.TensileForce.Loadcase);
        //        }
        //        if (mem.MaxCompForce != 0.0)
        //        {
        //            sw.WriteLine("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN", mem.MaxCompForce);
        //            sw.WriteLine("Member No = {0} ", ana_data.CompressionForce.MemberNo);
        //            sw.WriteLine("Loadcase = {0} ", ana_data.CompressionForce.Loadcase);


        //            sw.WriteLine("MAXIMUM COMPRESSIVE STRESS = {0:f3} kN/m^2", mem.MaxStress);
        //        }

        //    }
        //    sw.WriteLine();
        //    sw.WriteLine("Length of Member = ly = {0:f3} m", mem.Length);
        //    sw.WriteLine("Diameter of Bolt = bolt_dia = {0} mm", bolt_dia);
        //    sw.WriteLine("No of Bolt in a Section = nb = {0} ", nb);
        //    sw.WriteLine();
        //    #region Define Section
        //    switch (mem.SectionDetails.DefineSection)
        //    {
        //        case eDefineSection.Section1:
        //            #region Section 1
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);

        //            tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            a = tabBeam.Area;
        //            D = tabBeam.Depth;
        //            Iyy = tabBeam.Iyy;
        //            t = tabBeam.WebThickness;
        //            tf = tabBeam.FlangeThickness;

        //            sw.WriteLine();
        //            sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Web Thickness = t = {0} mm", t);
        //            sw.WriteLine("Flange Thickness = tf = {0} mm", tf);


        //            Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0);
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0)");
        //            sw.WriteLine("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + ({4} * {5} * {5} * {5} / 12.0)",
        //                Iyy, a, S, n, tp, Bp);
        //            sw.WriteLine("                  = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();


        //            A = n * a * 100 + (tp * Bp * np);
        //            sw.WriteLine("A = n * a * 100 + (tp * Bp * np)");
        //            sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4})",
        //                n, a, tp, Bp, np);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();


        //            Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp))");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
        //                A, nb, bolt_dia, tf, tp);
        //            sw.WriteLine("     = {0:f3} sq.mm ", Anet);
        //            sw.WriteLine();
        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:f3}/ {1:f3})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();


        //            #endregion Section 1
        //            break;
        //        case eDefineSection.Section2:
        //            #region Section 2

        //            tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            a = tabChannel.Area;
        //            D = tabChannel.Depth;
        //            Iyy = tabChannel.Iyy;
        //            t = tabChannel.WebThickness;
        //            tf = tabChannel.FlangeThickness;
        //            Cxx = tabChannel.CentreOfGravity;
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            sw.WriteLine();
        //            sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Bottom Plate Width = Bp = {0} mm", Bbp);
        //            sw.WriteLine("Bottom Plate Thickness = tp = {0} mm", tbp);
        //            sw.WriteLine("No Of Bottom Plate = np = {0}", nbp);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Web Thickness = t = {0} mm", t);
        //            sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            sw.WriteLine("Moment of Inertia = Iy");
        //            sw.WriteLine();
        //            sw.WriteLine("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns");
        //            sw.WriteLine("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {8}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
        //                //sw.WriteLine("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
        //                                        Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns);

        //            sw.WriteLine("           = {0:E3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);
        //            sw.WriteLine("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)");
        //            sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //                n, a, tp, Bp, np, ts, Bs, ns);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
        //                A, nb, bolt_dia, t, ts);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();

        //            //Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * (Math.Pow((S - t - (tp / 2.0)), 2))) * np;
        //            //sw.WriteLine("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp**3 / 12.0) * (S - t - (tp / 2.0))^2)) * np");
        //            //sw.WriteLine("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + (({4} * {5}^3 / 12.0) * ({2} - {6} - ({4} / 2.0))^2)) * {7}",
        //            //                                                              Iyy, a, S, n, tp, Bp, t, np);
        //            //sw.WriteLine("                  = {0:f3} sq.sq.mm", Iy);
        //            //sw.WriteLine();

        //            //A = n * a * 100 + (tp * Bp * np);
        //            //sw.WriteLine("A = n * a * 100 + (tp * Bp * np)");
        //            //sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4})", n, a, tp, Bp, np);
        //            //sw.WriteLine("  = {0:f3} sq.mm", A);
        //            //sw.WriteLine();
        //            //Anet = A - nb * ((bolt_dia + 1.5) * (t + tp));
        //            //sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (t + tp))");
        //            //sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))", A, nb, bolt_dia, t, tp);
        //            //sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            //sw.WriteLine();
        //            ry_anet = Math.Sqrt(Iy / Anet);
        //            ry = Math.Sqrt(Iy / A);
        //            sw.WriteLine("ry = SQRT(Iy / A)");
        //            sw.WriteLine("   = SQRT({0:E3} / {1:f3})", Iy, A);
        //            sw.WriteLine("   = {0:f3} mm", ry);

        //            sw.WriteLine();
        //            sw.WriteLine();

        //            sw.WriteLine("ry_Anet = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:E3} / {1:f3})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry_anet);

        //            sw.WriteLine();
        //            #endregion Section 2
        //            break;
        //        case eDefineSection.Section3:
        //            #region Section 3
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);


        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
        //            a = tabAngle.Area;
        //            //D = tabAngle.Depth;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            t = tabAngle.Thickness;

        //            sw.WriteLine();
        //            sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0}", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();

        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            sw.WriteLine("Moment of Inertia = Iy");
        //            sw.WriteLine();
        //            sw.WriteLine("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns");
        //            sw.WriteLine("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
        //                                        Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns);

        //            sw.WriteLine("           = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);
        //            sw.WriteLine("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)");
        //            sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //                n, a, tp, Bp, np, ts, Bs, ns);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
        //                A, nb, bolt_dia, t, ts);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();
        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0} / {1})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();
        //            #endregion Section 3
        //            break;
        //        case eDefineSection.Section4:
        //            #region Section 4
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

        //            a = tabAngle.Area;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            //n = 4;
        //            t = tabAngle.Thickness;
        //            sw.WriteLine();
        //            sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Vertical Stiffener Plate Width = Bss = {0} mm", Bss);
        //            sw.WriteLine("Vertical Stiffener Plate Thickness = tss = {0} mm", tss);
        //            sw.WriteLine("No Of Vertical Stiffener Plate = nss = {0}", nss);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Moment of Inertia = Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Centre of Gravity = Cxx = {0} cm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();

        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + (tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
        //            sw.WriteLine("Moment of Intertia = Iy");
        //            sw.WriteLine();
        //            sw.WriteLine("          Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns + (tss * Bss * (((S / 2) - t - (tss / 2.0))^2)) * nss");
        //            sw.WriteLine("             = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10} + ({11} * {12} * ((({2} / 2) - {13} - ({11} / 2.0))^2)) * {14}",
        //                                                                                        Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns, tss, Bss, t, nss);

        //            sw.WriteLine("             = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
        //            sw.WriteLine("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss)");
        //            sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
        //                n, a, tp, Bp, np, ts, Bs, ns, tss, Bss, nss);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
        //                A, nb, bolt_dia, t, ts);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();
        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:f3} / {1:f3})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();

        //            #endregion Section 4
        //            break;

        //        case eDefineSection.Section5: // Stringer
        //            #region Section 5
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("MOMENT & FORCE  DATA");
        //            sw.WriteLine("--------------------");
        //            sw.WriteLine();

        //            sw.WriteLine("Maximum Bending Moment = M = {0:f3} kN-m", M);
        //            sw.WriteLine("Maximum Shear Force = F = {0:f3} kN", F);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);

        //            tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            a = tabBeam.Area;
        //            D = tabBeam.Depth;
        //            Iyy = tabBeam.Iyy;
        //            Ixx = tabBeam.Ixx;
        //            Zxx = tabBeam.Zxx;

        //            t = tabBeam.WebThickness;
        //            tf = tabBeam.FlangeThickness;

        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Bottom Plate Width = Bp = {0} mm", Bbp);
        //            sw.WriteLine("Bottom Plate Thickness = tp = {0} mm", tbp);
        //            sw.WriteLine("No Of Bottom Plate = np = {0}", nbp);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Web Thickness = t = {0} mm", t);
        //            sw.WriteLine("Flange Thickness = tf = {0}", tf);


        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            Ix = Ixx * 10000 + ((tbp * Bbp * Bbp * Bbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp;
        //            sw.WriteLine("Moment of Inertia = Ix = Ixx * 10000 + ((tbp * Bbp**3 / 12.0) + (tbp * Bbp) * ((((D / 2) + (tbp / 2))^2))) * nbp ");
        //            sw.WriteLine("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({1} / 2))^2))) * {4} ",
        //                            Ixx, tbp, Bbp, D, nbp);
        //            sw.WriteLine("                       = {0:f3} sq.sq.mm", Ix);
        //            sw.WriteLine();

        //            Ix = Ixx * 10000 + ((tbp * Bbp * Bbp * Bbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));
        //            A = n * a * 100 + (tbp * Bbp * nbp);

        //            A = n * a * 100 + (tbp * Bbp * nbp) + (tss * Bss * nss);

        //            Z = (M * 10e5) / sigma_b;
        //            sw.WriteLine("Required Section Modulus = Zr = M/σ_b");
        //            sw.WriteLine("                         = {0}*10^6 / {1}", M, sigma_b);
        //            sw.WriteLine("                         = {0:f3} cu.mm", Z);
        //            sw.WriteLine();

        //            double y = (D / 2) + tbp;
        //            sw.WriteLine("Distance from Center to Bottom most edge of Section = y");
        //            sw.WriteLine("                    y = (D/2) + tbp");
        //            sw.WriteLine("                      = ({0}/2) + {1}", D, tbp);
        //            sw.WriteLine("                      = {0:f3} mm", y);
        //            sw.WriteLine();

        //            double chk_Z = Ix / y;
        //            sw.WriteLine("Section Modulus = Z = Ix/y");
        //            sw.WriteLine("                    = {0:f3}/{1}", Ix, y);
        //            //sw.WriteLine("                    = {0:f3} cu.mm", chk_Z);
        //            //sw.WriteLine();
        //            mem.Capacity_SectionModulus = chk_Z;
        //            mem.Required_SectionModulus = Z;

        //            if (chk_Z < Z)
        //            {
        //                sw.WriteLine("                    = {0:f3} cu.mm < {1:f3} cu.mm = Required Section Modulus ,  So, NOT OK", chk_Z, Z);
        //                mem.Result = "NOT OK";
        //            }
        //            else if (chk_Z > Z)
        //            {
        //                sw.WriteLine("                    = {0:f3} cu.mm > {1:f3} cu.mm = Required Section Modulus  ,  So, OK", chk_Z, Z);
        //                //mem.Result = "OK";
        //            }
        //            else
        //            {
        //                sw.WriteLine("                    = {0:f3} cu.mm = {1:f3} cu.mm = Required Section Modulus ,  So, NOT OK", chk_Z, Z);
        //                mem.Result = "NOT OK";
        //            }
        //            sw.WriteLine();


        //            double shr_stress = (F * 1000.0) / (t * D);
        //            sw.WriteLine("Shear Stress = (F * 1000.0)/(t * D)");
        //            sw.WriteLine("             = ({0} * 1000.0)/({1} * {2})", F, t, D);
        //            mem.Capacity_ShearStress = shr_stress;
        //            mem.Required_ShearStress = sigma_c;

        //            mem.MaxCompForce.Force = 0.0;
        //            mem.MaxTensionForce.Force = 0.0;

        //            if (shr_stress < sigma_c)
        //            {
        //                sw.WriteLine("             = {0:f3} N/sq.mm < {1} N/sq.mm = Permissible Shear Stress (σ_c) , So, OK", shr_stress, sigma_c);
        //                //mem.Result = "OK";
        //            }
        //            else if (shr_stress >= sigma_c)
        //            {
        //                sw.WriteLine("             = {0:f3} N/sq.mm > {1} N/sq.mm = Permissible Shear Stress (σ_c) , So, NOT OK", shr_stress, sigma_c);
        //                mem.Result = "NOT OK";
        //            }

        //            #endregion Section 5
        //            return;
        //            break;

        //        case eDefineSection.Section6: // Cross Girder
        //            #region Section 6
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("MOMENT & FORCE  DATA");
        //            sw.WriteLine("--------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("Maximum Bending Moment = M = {0:f3} kN-m", M);
        //            sw.WriteLine("Maximum Shear Force = F = {0:f3} kN", F);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);

        //            tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            a = tabBeam.Area;
        //            D = tabBeam.Depth;
        //            Iyy = tabBeam.Iyy;
        //            Ixx = tabBeam.Ixx;
        //            Zxx = tabBeam.Zxx;

        //            t = tabBeam.WebThickness;
        //            tf = tabBeam.FlangeThickness;

        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Bottom Plate Width = Bbp = {0} mm", Bbp);
        //            sw.WriteLine("Bottom Plate Thickness = tbp = {0} mm", tbp);
        //            sw.WriteLine("No Of Bottom Plate = nbp = {0}", nbp);
        //            sw.WriteLine();
        //            sw.WriteLine("Vertical Stiffener Plate Width = Bss = {0} mm", Bss);
        //            sw.WriteLine("Vertical Stiffener Plate Thickness = tss = {0} mm", tss);
        //            sw.WriteLine("No Of Vertical Stiffener Plates = nss = {0}", nss);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Web Thickness = t = {0} mm", t);
        //            sw.WriteLine("Flange Thickness = tf = {0} mm", tf);


        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            Ix = Ixx * 10000 + ((tbp * Bbp * Bbp * Bbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));
        //            sw.WriteLine("Moment of Inertia = Ix = Ixx * 10000 + ((tbp * Bbp**3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)))");
        //            sw.WriteLine("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({1} / 2))^2))) * {4} + ({5} * ({6} * {7}) * (({8} / 2.0) + ({6} / 2.0))) ",
        //                                                                                                                        Ixx, tbp, Bbp, D, nbp, nss, tss, Bss, t);
        //            sw.WriteLine("                       = {0:f3} sq.sq.mm", Ix);
        //            sw.WriteLine();

        //            Z = (M * 10e5) / sigma_b;
        //            sw.WriteLine("Required Section Modulus = Zp = M/σ_b");
        //            sw.WriteLine("                         = {0}*10^6 / {1}", M, sigma_b);
        //            sw.WriteLine("                         = {0:f3} cu.mm", Z);
        //            sw.WriteLine();

        //            y = (D / 2) + tbp;
        //            sw.WriteLine("Distance from Center to Bottom most edge of Section = y");
        //            sw.WriteLine("                    y = (D/2) + tbp");
        //            sw.WriteLine("                      = ({0}/2) + {1}", D, tbp);
        //            sw.WriteLine("                      = {0:f3} mm", y);
        //            sw.WriteLine();

        //            chk_Z = Ix / y;
        //            sw.WriteLine("Section Modulus = Z = Ix/y");
        //            sw.WriteLine("                    = {0}/{1}", Ix, y);
        //            sw.WriteLine("                    = {0:f3} cu.mm", chk_Z);
        //            sw.WriteLine();
        //            mem.Required_SectionModulus = Z;
        //            mem.Capacity_SectionModulus = chk_Z;

        //            if (chk_Z > Z)
        //            {
        //                sw.WriteLine("                    = {0:f3} cu.mm > {1:f3} cu.mm = Required Section Modulus ,  So, OK", chk_Z, Z);
        //                //mem.Result = "OK";
        //            }
        //            else if (chk_Z < Z)
        //            {
        //                sw.WriteLine("                    = {0:f3} cu.mm < {1:f3} cu.mm = Required Section Modulus ,  So, NOT OK", chk_Z, Z);
        //                mem.Result = "NOT OK";
        //            }
        //            else
        //            {
        //                sw.WriteLine("                    = {0:f3} cu.mm = {1:f3} cu.mm = Required Section Modulus ,  So, NOT OK", chk_Z, Z);
        //                mem.Result = "NOT OK";
        //            }
        //            sw.WriteLine();


        //            shr_stress = (F * 1000.0) / (t * D);
        //            sw.WriteLine("Shear Stress = (F * 1000.0) /(t * D)");
        //            sw.WriteLine("             = ({0:f3} * 1000.0) / ({1} * {2})", F, t, D);
        //            mem.Capacity_ShearStress = shr_stress;
        //            mem.Required_ShearStress = sigma_c;

        //            mem.MaxCompForce.Force = 0.0;
        //            mem.MaxTensionForce.Force = 0.0;
        //            if (shr_stress < sigma_c)
        //            {
        //                sw.WriteLine("             = {0:f3} N/sq.mm < {1} N/sq.mm  = Permissible Shear Stress (σ_c)  , So, OK", shr_stress, sigma_c);
        //                //mem.Result = "OK";
        //            }
        //            else if (shr_stress >= sigma_c)
        //            {
        //                sw.WriteLine("             = {0:f3} N/sq.mm > {1} N/sq.mm  = Permissible Shear Stress (σ_c)  , So, NOT OK", shr_stress, sigma_c);
        //                mem.Result = "NOT OK";
        //            }
        //            #endregion Section 6
        //            return;
        //            break;
        //        case eDefineSection.Section7:
        //            #region Section 7
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

        //            a = tabAngle.Area;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            //n = 4;
        //            t = tabAngle.Thickness;
        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            //sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            //sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            //sw.WriteLine("No Of Top Plate = np = {0} mm", np);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            //sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            //sw.WriteLine("No Of Side Plate = ns = {0} mm", ns);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Vertical Stiffener Plate Width = Bss = {0} mm", Bss);
        //            //sw.WriteLine("Vertical Stiffener Plate Thickness = tss = {0} mm", tss);
        //            //sw.WriteLine("No Of Vertical Stiffener Plate = nss = {0} mm", nss);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} cm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();


        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
        //            sw.WriteLine("Moment of Inertia = Iy ");
        //            sw.WriteLine("      Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n ");
        //            sw.WriteLine("                  = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
        //                            Iyy, a, S, Cxx, n);
        //            sw.WriteLine("         = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
        //            sw.WriteLine("A = n * a * 100");
        //            sw.WriteLine("  = {0} * {1} * 100",
        //                n, a);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * t)");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
        //                A, nb, bolt_dia, t);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();
        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:f3} / {1:f3})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();

        //            #endregion Section 7
        //            break;
        //        case eDefineSection.Section8:
        //            #region Section 8
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

        //            a = tabAngle.Area;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            //n = 4;
        //            t = tabAngle.Thickness;
        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            //sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            //sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            //sw.WriteLine("No Of Top Plate = np = {0} mm", np);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            //sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            //sw.WriteLine("No Of Side Plate = ns = {0} mm", ns);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Vertical Stiffener Plate Width = Bss = {0} mm", Bss);
        //            //sw.WriteLine("Vertical Stiffener Plate Thickness = tss = {0} mm", tss);
        //            //sw.WriteLine("No Of Vertical Stiffener Plate = nss = {0} mm", nss);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} cm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();

        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
        //            sw.WriteLine("Moment of Inertia = Iy ");
        //            sw.WriteLine("    Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n ");
        //            sw.WriteLine("       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
        //                            Iyy, a, S, Cxx, n);
        //            sw.WriteLine("       = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
        //            sw.WriteLine("A = n * a * 100");
        //            sw.WriteLine("  = {0} * {1} * 100",
        //                n, a);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * t)");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
        //                A, nb, bolt_dia, t);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();
        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:f3} / {1:f3})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();

        //            #endregion Section 8
        //            break;
        //        case eDefineSection.Section9:
        //            #region Section 9
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);


        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
        //            a = tabAngle.Area;
        //            //D = tabAngle.Depth;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            t = tabAngle.Thickness;

        //            sw.WriteLine();
        //            sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} cm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();

        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            sw.WriteLine("Moment of Inertia = Iy");
        //            sw.WriteLine();
        //            sw.WriteLine("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns");
        //            sw.WriteLine("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
        //                                        Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns);

        //            sw.WriteLine("           = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);
        //            sw.WriteLine("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)");
        //            sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //                n, a, tp, Bp, np, ts, Bs, ns);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
        //                A, nb, bolt_dia, t, ts);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();
        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0} / {1})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();
        //            #endregion Section 9
        //            break;
        //        case eDefineSection.Section10:
        //            #region Section 10

        //            tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            a = tabChannel.Area;
        //            D = tabChannel.Depth;
        //            Iyy = tabChannel.Iyy;
        //            t = tabChannel.WebThickness;
        //            tf = tabChannel.FlangeThickness;
        //            Cxx = tabChannel.CentreOfGravity;
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
        //            sw.WriteLine();
        //            sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Bottom Plate Width = Bbp = {0} mm", Bbp);
        //            sw.WriteLine("Bottom Plate Thickness = tbp = {0} mm", tbp);
        //            sw.WriteLine("No Of Bottom Plate = nbp = {0}", nbp);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Web Thickness = t = {0} mm", t);
        //            sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            sw.WriteLine("Moment of Inertia = Iy");
        //            sw.WriteLine();
        //            sw.WriteLine("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns");
        //            sw.WriteLine("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {9}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
        //                                        Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns);

        //            sw.WriteLine("           = {0:E3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns);
        //            sw.WriteLine("A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns)");
        //            sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
        //                n, a, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();

        //            Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
        //            sw.WriteLine("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))");
        //            sw.WriteLine("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
        //                A, nb, bolt_dia, t, ts);
        //            sw.WriteLine("     = {0:f3} sq.mm", Anet);
        //            sw.WriteLine();

        //            ry = Math.Sqrt(Iy / Anet);
        //            sw.WriteLine("ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:f3} / {1:f3})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);

        //            sw.WriteLine();
        //            #endregion Section 10
        //            break;
        //        case eDefineSection.Section11:
        //            #region Section 11
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);


        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
        //            a = tabAngle.Area;
        //            //D = tabAngle.Depth;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            t = tabAngle.Thickness;

        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            //sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            //sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            //sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            //sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            //sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Ixx = Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} cm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("For Combined Section :");
        //            sw.WriteLine("----------------------");
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            Iy = 2 * Iyy * 10000;
        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            sw.WriteLine("Moment of Inertia = Iy = 2 * Iyy *10000");
        //            sw.WriteLine("                       = 2 * {0} *10000", Iyy);
        //            sw.WriteLine("                       = {0} sq.sq.mm", Iy);
        //            sw.WriteLine();
        //            //sw.WriteLine("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns");
        //            //sw.WriteLine("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
        //            //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns);

        //            //sw.WriteLine("           = {0:f3} sq.sq.mm", Iy);
        //            sw.WriteLine();

        //            A = 2 * a * 100;
        //            Anet = A;
        //            sw.WriteLine("A = 2 * a a * 100 = 2 * {0} a * 100 = {1} sq.mm", a, A);
        //            //sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //            //    n, a, tp, Bp, np, ts, Bs, ns);
        //            //sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            ry = Math.Sqrt(Iy / A);
        //            sw.WriteLine("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0} / {1})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();
        //            l = (mem.Length * 1000);

        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            #endregion Section 11
        //            break;
        //        case eDefineSection.Section12:
        //            #region Section 12
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);


        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
        //            a = tabAngle.Area;
        //            //D = tabAngle.Depth;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            t = tabAngle.Thickness;

        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            //sw.WriteLine("Top Plate Width = Bp = {0} mm", Bp);
        //            //sw.WriteLine("Top Plate Thickness = tp = {0} mm", tp);
        //            //sw.WriteLine("No Of Top Plate = np = {0}", np);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            //sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            //sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Ixx = Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} cm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("For Combined Section :");
        //            sw.WriteLine("----------------------");
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            Iy = 2 * Iyy * 10000;
        //            //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
        //            sw.WriteLine("Moment of Inertia = Iy = 2 * Iyy * 10000");
        //            sw.WriteLine("                       = 2 * {0} * 10000", Iyy);
        //            sw.WriteLine("                       = {0} sq.sq.mm", Iy);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            A = 2 * a * 100;
        //            Anet = A;
        //            sw.WriteLine("A = 2 * a * 100 = 2 * {0} * 100 = {1} sq.mm", a, A);
        //            //sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //            //    n, a, tp, Bp, np, ts, Bs, ns);
        //            //sw.WriteLine("  = {0:f3} sq.mm", A);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            ry = Math.Sqrt(Iy / A);
        //            sw.WriteLine("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0} / {1})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();
        //            l = (mem.Length * 1000);

        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            #endregion Section 12
        //            break;

        //        case eDefineSection.Section13:
        //            #region Section 13
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);


        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
        //            a = tabAngle.Area;
        //            //D = tabAngle.Depth;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            t = tabAngle.Thickness;


        //            Bp = Bs;
        //            tp = ts;



        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Central Plate Width = Bp = {0} mm", Bp);
        //            sw.WriteLine("Central Plate Thickness = tp = {0} mm", tp);
        //            sw.WriteLine("No Of Central Plate = np = {0}", np);
        //            //sw.WriteLine();
        //            //sw.WriteLine("Side Plate Width = Bs = {0} mm", Bs);
        //            //sw.WriteLine("Side Plate Thickness = ts = {0} mm", ts);
        //            //sw.WriteLine("No Of Side Plate = ns = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Ixx = Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} mm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("For Combined Section :");
        //            sw.WriteLine("----------------------");
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + tp / 2), 2.0)) + tp * (Bp * Bp * Bp / 12.0) * np;
        //            sw.WriteLine("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np");
        //            sw.WriteLine("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5} * {5} * {5} / 12.0) * {6}", n,
        //                Iyy, a, Cxx, tp, Bp, np);
        //            sw.WriteLine("                       = {0:E3} sq.sq.mm", Iy);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            A = a * n * 100 + tp * Bp * np;
        //            sw.WriteLine("A = {0} * {1} * 100 + {2} * {3} * {4} = {5:f3} sq.mm", a, n, tp, Bp, np, A);
        //            //sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //            //    n, a, tp, Bp, np, ts, Bs, ns);
        //            //sw.WriteLine("  = {0:f3} sq.mm", A);


        //            Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp);
        //            sw.WriteLine("Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp)");
        //            sw.WriteLine("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, tp);
        //            sw.WriteLine("     = {0} sq.mm", Anet);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            ry = Math.Sqrt(Iy / A);
        //            sw.WriteLine("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0} / {1})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();
        //            l = (mem.Length * 1000);

        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            #endregion Section 12
        //            break;

        //        case eDefineSection.Section14:

        //            #region Section 14
        //            sw.WriteLine();
        //            sw.WriteLine("SELECTED SECTION DATA");
        //            sw.WriteLine("---------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("{0} x {1} {2} {3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);


        //            tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
        //            a = tabAngle.Area;
        //            //D = tabAngle.Depth;
        //            Iyy = tabAngle.Iyy;
        //            Cxx = tabAngle.Cxx;
        //            t = tabAngle.Thickness;


        //            ns = 1;
        //            sw.WriteLine();
        //            //sw.WriteLine("Spacing = S = {0} mm", S);
        //            sw.WriteLine("Central Plate Width = Bp = {0} mm", Bs);
        //            sw.WriteLine("Central Plate Thickness = tp = {0} mm", ts);
        //            sw.WriteLine("No Of Central Plate = np = {0}", ns);
        //            sw.WriteLine();
        //            sw.WriteLine("Top Plate Width = Bs = {0} mm", Bp);
        //            sw.WriteLine("Top Plate Thickness = ts = {0} mm", tp);
        //            sw.WriteLine("No Of Top Plate = ns = {0}", np);
        //            sw.WriteLine();
        //            sw.WriteLine("Bottom Plate Width = Bs = {0} mm", Bbp);
        //            sw.WriteLine("Bottom Plate Thickness = ts = {0} mm", tbp);
        //            sw.WriteLine("No Of Bottom Plate = ns = {0}", nbp);
        //            sw.WriteLine();
        //            sw.WriteLine("Area = a = {0} sq.cm", a);
        //            sw.WriteLine("Thickness = t = {0} mm", t);
        //            //sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
        //            sw.WriteLine("Ixx = Iyy = {0} sq.sq.cm", Iyy);
        //            sw.WriteLine("Cxx = {0} mm", Cxx);
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine("DESIGN CALCULATION");
        //            sw.WriteLine("------------------");
        //            sw.WriteLine();
        //            sw.WriteLine("For Combined Section :");
        //            sw.WriteLine("----------------------");
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + ts / 2), 2.0)) + ts * (Bs * Bs * Bs / 12.0) * ns + tp * (Bp * Bp * Bp / 12.0) * np + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp;
        //            sw.WriteLine("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np + ts * (Bs * Bs * Bs / 12.0) * ns + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp");
        //            sw.WriteLine("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5}^3 / 12.0) * {6} + {7} * ({8}^3 / 12.0) * {9} + {10} * ({11}^3 / 12.0) * {12}", n,
        //                Iyy, a, Cxx, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp);
        //            sw.WriteLine("                       = {0:E3} sq.sq.mm", Iy);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns;
        //            sw.WriteLine("A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns");
        //            sw.WriteLine("  = {0} * {1} * 100 + {2} * {3} * {4} + {5} * {6} * {7} + {8} * {9} * {10}", a, n, tp, Bp, np, tbp, Bbp, nbp, ts, Bs, ns);
        //            sw.WriteLine("  = {0:f3} sq.mm", A);
        //            //sw.WriteLine("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
        //            //    n, a, tp, Bp, np, ts, Bs, ns);
        //            //sw.WriteLine("  = {0:f3} sq.mm", A);


        //            Anet = A - ((bolt_dia + 1.5) * n * (2 * t + ts));
        //            sw.WriteLine("Anet = A - (bolt_dia + 1.5) * n * (2 * t + ts)");
        //            sw.WriteLine("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, ts);
        //            sw.WriteLine("     = {0} sq.mm", Anet);
        //            sw.WriteLine();
        //            sw.WriteLine();

        //            ry = Math.Sqrt(Iy / A);
        //            sw.WriteLine("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)");
        //            sw.WriteLine("   = SQRT({0:E3} / {1})", Iy, Anet);
        //            sw.WriteLine("   = {0:f3} mm", ry);
        //            sw.WriteLine();
        //            l = (mem.Length * 1000);

        //            sw.WriteLine();
        //            sw.WriteLine();
        //            sw.WriteLine();
        //            #endregion Section 12

        //            break;
        //    }
        //    #endregion Define Section

        //    if (mem.MaxCompForce != 0.0)
        //    {
        //        #region Compression
        //        //double L = mem.Length;
        //        sw.WriteLine();
        //        sw.WriteLine("COMPRESSIVE LOAD CARRYING CAPACITY");
        //        sw.WriteLine("-----------------------------------------------------------------------------");
        //        sw.WriteLine();
        //        if (mem.MaxCompForce != 0.0)
        //        {
        //            sw.WriteLine("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN", mem.MaxCompForce);
        //            sw.WriteLine("MAXIMUM COMPRESSIVE STRESS = {0:f3} kN/m^2", mem.MaxStress);
        //        }
        //        sw.WriteLine();
        //        sw.WriteLine();

        //        double ly = 0.85 * mem.Length * 1000;
        //        //Chiranjit [2011 10 20]  
        //        //if (mem.MemberType == eMemberType.TopChord)
        //        //{
        //        //    ly = mem.Length * 1000;
        //        //    sw.WriteLine("Effective Length = ly = {0:f3} mm", ly);
        //        //}
        //        //else
        //        //{
        //        sw.WriteLine("Effective Length = 0.85*ly = 0.85 * {0:f3} = {1:f3} mm", mem.Length * 1000, ly);
        //        //}
        //        double lamda = ly / ry;
        //        sw.WriteLine();
        //        sw.WriteLine("   λ  = ly / ry");
        //        sw.WriteLine("      = {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda);

        //        double sigma = Get_Table_1_Sigma_Value(lamda, fy);
        //        sw.WriteLine();
        //        sw.WriteLine("From Table 4,   σ_ac = {0} N/mm^2", sigma);
        //        sw.WriteLine();
        //        sw.WriteLine();

        //        //double comp_load_cap = sigma * (Anet / 1000.0);
        //        double comp_load_cap = sigma * (A / 1000.0);


        //        //Chiranjit [2011 07 01] 
        //        //Store Compressive Stress
        //        mem.Compressive_Stress = (mem.MaxCompForce * 1000) / (A);
        //        mem.Capacity_Compressive_Stress = sigma;



        //        sw.WriteLine("Compressive Load Carrying Capacity = σ_ac * A   N");
        //        sw.WriteLine("                                   = ({0}*{1:f3})/1000   kN", sigma, A);
        //        mem.Capacity_CompForce = comp_load_cap;

        //        if (comp_load_cap > mem.MaxCompForce)
        //        {
        //            sw.WriteLine("                                   = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Compressive Force OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText);
        //            mem.Result = "OK";
        //        }
        //        else if (comp_load_cap < mem.MaxCompForce)
        //        {

        //            //sw.WriteLine("                                   = {0:f3} kN < {1:f3} kN, Design Compressive Force, NOT OK", comp_load_cap, mem.MaxCompForce);
        //            sw.WriteLine("                                   = {0:f3} kN < {1:f3} kN", comp_load_cap, mem.MaxCompForce);

        //        }
        //        else
        //        {
        //            sw.WriteLine("                                   = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText);
        //            mem.Result = "NOT OK";
        //        }

        //        sw.WriteLine();
        //        sw.WriteLine();
        //        sw.WriteLine("Compressive Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/mm^2", mem.MaxStress, (mem.MaxStress / 1000.0));
        //        sw.WriteLine();
        //        if ((sigma) > (mem.MaxStress / 1000.0))
        //        {
        //            //sw.WriteLine("Maximum Compressive Stress = {0:f3} N/mm^2", mem.MaxStress/1000.0);
        //            mem.Compressive_Stress = (mem.MaxStress / 1000.0);
        //            sw.WriteLine("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Compressive Stress OK", mem.MaxStress / 1000.0, sigma);
        //            mem.Result = "OK";
        //        }
        //        else
        //        {
        //            sw.WriteLine("                                   = {0:f3} kN < {1:f3} kN, Allowable Compressive Stress NOT OK", comp_load_cap, mem.MaxCompForce);
        //            mem.Result = "NOT OK";
        //        }


        //        if (n > 1 || mem.SectionDetails.LateralSpacing > 0)
        //            DesignLacing(sw, mem, lamda);
        //        #endregion Compression
        //    }
        //    if (mem.MaxTensionForce != 0.0)
        //    {
        //        #region Tensile
        //        //sw.WriteLine();
        //        //sw.WriteLine("Tensile Load Carrying Capacity = ");
        //        double tensile_load_cap = (Anet * ft) / 1000.0;
        //        sw.WriteLine();
        //        sw.WriteLine("TENSILE LOAD CARRYING CAPACITY");
        //        sw.WriteLine("------------------------------");
        //        sw.WriteLine();
        //        sw.WriteLine();
        //        mem.Capacity_TensionForce = tensile_load_cap;

        //        sw.WriteLine("MAXIMUM TENSILE FORCE = {0:f3} kN", mem.MaxTensionForce);
        //        sw.WriteLine("MAXIMUM TENSILE STRESS = {0:f3} kN/m^", mem.MaxStress);
        //        sw.WriteLine();


        //        //Chiranjit [2011 07 01] 
        //        //Store Tensile Stress
        //        mem.Capacity_Tensile_Stress = ft;
        //        mem.Tensile_Stress = (mem.MaxTensionForce * 1000) / (Anet);



        //        sw.WriteLine("Tensile Load Carrying Capacity = Anet * ft   N");
        //        sw.WriteLine("                               = ({0:f3}*{1})/1000   kN", Anet, ft);

        //        if (tensile_load_cap > mem.MaxTensionForce)
        //        {
        //            sw.WriteLine("                               = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Tensile Force OK", tensile_load_cap, mem.MaxTensionForce, mem.Group.MemberNosText);
        //            mem.Result = "OK";
        //        }
        //        else if (tensile_load_cap < mem.MaxTensionForce)
        //        {
        //            sw.WriteLine("                               = {0:f3} kN < {1:f3} kN, Maximum Group [{2}] Tensile Force", tensile_load_cap, mem.MaxTensionForce, mem.Group.MemberNosText);
        //            mem.Result = "NOT OK";
        //        }
        //        else
        //        {
        //            sw.WriteLine("                               = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Tensile Force NOT OK", tensile_load_cap, mem.MaxTensionForce, mem.Group.MemberNosText);
        //            mem.Result = "NOT OK";
        //        }

        //        sw.WriteLine();
        //        sw.WriteLine();
        //        sw.WriteLine("Tensile Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/mm^2", mem.MaxStress, (mem.MaxStress / 1000.0));
        //        sw.WriteLine();
        //        if ((ft) > (mem.MaxStress / 1000.0))
        //        {
        //            //sw.WriteLine("Maximum Compressive Stress = {0:f3} N/mm^2", mem.MaxStress/1000.0);
        //            mem.Compressive_Stress = (mem.MaxStress / 1000.0);
        //            sw.WriteLine("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Tensile Stress  OK", mem.MaxStress / 1000.0, ft);
        //            mem.Result = "OK";
        //        }
        //        else
        //        {
        //            sw.WriteLine("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress  NOT OK", mem.MaxStress / 1000.0, ft);
        //            mem.Result = "NOT OK";
        //        }

        //        DesignConnection(sw, mem);

        //        sw.WriteLine();
        //        sw.WriteLine();
        //        sw.WriteLine();


        //        #endregion Tensile
        //    }
        //}
#endregion Chiranjit [2011_10_21]


        void ShowMemberNos(string group_name)
        {

            string kStr = "";
            MemberGroup mGrp = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(group_name);
            //START	GROUP	DEFINITION		
            //_L0L1	1	10	11	20
            //_L1L2	2	9	12	19
            //_L2L3	3	8	13	18
            //_L3L4	4	7	14	17
            //_L4L5	5	6	15	16
            //_U1U2	59	66	67	74
            //_U2U3	60	65	68	73
            //_U3U4	61	64	69	72
            //_U4U5	62	63	70	71
            //_L1U1	21	29	30	38
            //_L2U2	22	28	31	37
            //_L3U3	23	27	32	36
            //_L4U4	24	26	33	35
            //_L5U5	25	34		
            //_ER	39	43	49	53
            //_L2U1	40	44	50	54
            //_L3U2	41	45	51	55
            //_L4U3	42	46	52	56
            //_L5U4	47	48	57	58
            //_TCS_ST	170	TO	178	
            //_TCS_DIA	179	TO	194	
            //_BCB	195	TO	214	
            //_STRINGER	75	TO	114	
            //_XGIRDER	115	TO	169						
            //END	
            if (mGrp != null)
            {
                txt_cd_mem_no.Text = mGrp.MemberNosText;
            }
        }

        public eMemberType Get_MemberType(string groupName)
        {
            //START	GROUP	DEFINITION		
            //_L0L1	1	10	11	20
            //_L1L2	2	9	12	19
            //_L2L3	3	8	13	18
            //_L3L4	4	7	14	17
            //_L4L5	5	6	15	16
            //_U1U2	59	66	67	74
            //_U2U3	60	65	68	73
            //_U3U4	61	64	69	72
            //_U4U5	62	63	70	71
            //_L1U1	21	29	30	38
            //_L2U2	22	28	31	37
            //_L3U3	23	27	32	36
            //_L4U4	24	26	33	35
            //_L5U5	25	34		
            //_ER	39	43	49	53
            //_L2U1	40	44	50	54
            //_L3U2	41	45	51	55
            //_L4U3	42	46	52	56
            //_L5U4	47	48	57	58
            //_TCS_ST	170	TO	178	
            //_TCS_DIA	179	TO	194	
            //_BCB	195	TO	214	
            //_STRINGER	75	TO	114	
            //_XGIRDER	115	TO	169						
            //END	


            switch (groupName)
            {
                case "_L0L1":
                    return eMemberType.BottomChord;
                    break;
                case "_L1L2":
                    return eMemberType.BottomChord;
                    break;
                case "_L2L3":
                    return eMemberType.BottomChord;
                    break;
                case "_L3L4":
                    return eMemberType.BottomChord;
                    break;
                case "_L4L5":
                    return eMemberType.BottomChord;
                    break;
                case "_U1U2":
                    return eMemberType.TopChord;
                    break;
                case "_U2U3":
                    return eMemberType.TopChord;
                    break;
                case "_U3U4":
                    return eMemberType.TopChord;
                    break;
                case "_U4U5":
                    return eMemberType.TopChord;
                    break;
                case "_L1U1":
                    return eMemberType.VerticalMember;
                    break;
                case "_L2U2":
                    return eMemberType.VerticalMember;
                    break;
                case "_L3U3":
                    return eMemberType.VerticalMember;
                    break;
                case "_L4U4":
                    return eMemberType.VerticalMember;
                    break;
                case "_L5U5":
                    return eMemberType.VerticalMember;
                    break;
                case "_L6U6":
                    return eMemberType.VerticalMember;
                    break;
                case "_L7U7":
                    return eMemberType.VerticalMember;
                    break;
                case "_L8U8":
                    return eMemberType.VerticalMember;
                    break;
                case "_L9U9":
                    return eMemberType.VerticalMember;
                    break;
                case "_V1V2":
                case "_V3V4":
                case "_V5V6":
                case "_V7V8":
                case "_V9V10":
                case "_V11V12":
                case "_V13V14":
                case "_V15V16":
                case "_V17V18":
                case "_V19V20":
                    return eMemberType.VerticalMember;
                    break;
                case "_ER":
                    return eMemberType.EndRakers;
                    break;
                case "_L2U1":
                    return eMemberType.DiagonalMember;
                    break;
                case "_L3U2":
                    return eMemberType.DiagonalMember;
                    break;
                case "_L4U3":
                    return eMemberType.DiagonalMember;
                    break;
                case "_L5U4":
                case "_L6U5":
                case "_L7U6":
                case "_L8U7":
                    return eMemberType.DiagonalMember;
                    break;
                case "_TCS_ST":
                    return eMemberType.TopChordBracings;
                case "_TCS_DIA":
                    return eMemberType.TopChordBracings;
                    break;
                case "_BCB":
                case "_BCB1":
                case "_BCB2":
                case "_BCB3":
                case "_BCB4":
                case "_BCB5":
                case "_BCB6":
                case "_BCB7":
                case "_BCB8":
                case "_BCB9":
                    return eMemberType.BottomChordBracings;
                case "_STRINGER":
                case "_STRINGER1":
                case "_STRINGER2":
                case "_STRINGER3":
                case "_STRINGER4":
                case "_STRINGER5":
                case "_STRINGER6":
                case "_STRINGER7":
                case "_STRINGER8":
                case "_STRINGER9":
                    return eMemberType.StringerBeam;
                case "_XGIRDER_IN":
                case "_XGIRDER1":
                case "_XGIRDER2":
                case "_XGIRDER3":
                case "_XGIRDER4":
                case "_XGIRDER5":
                case "_XGIRDER6":
                case "_XGIRDER7":
                case "_XGIRDER8":
                case "_XGIRDER_END":
                    return eMemberType.CrossGirder;
                    break;
            }
            return eMemberType.NoSelection;
        }


        public void DesignShearConnector(CMember mem, StreamWriter sw)
        {
            try
            {


                //List<string> sw = new List<string>();

                //sw.Add(string.Format(""));
                //sw.Add(string.Format(""));
                double V = mem.MaxShearForce.Force;
                double Ixx, a, D, B, tw, tf;
                double bs, ts, bp, tp, w, fck, m, L;
                //double bs, ts, bp, tp, w, d, fck, m, L;
                //top_plate_thickness


                //D = MyList.StringToDouble(txt_
                RolledSteelBeamsRow tabData = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);

                Ixx = tabData.Ixx;
                a = tabData.Area;
                D = tabData.Depth;
                tw = tabData.WebThickness;
                tf = tabData.FlangeThickness;
                B = tabData.FlangeWidth;

                L = mem.Length;

                bs = mem.SectionDetails.SidePlate.Width;
                ts = mem.SectionDetails.SidePlate.Thickness;
                //bp = mem.SectionDetails.TopPlate.Width;
                //tp = mem.SectionDetails.TopPlate.Thickness;
                bp = mem.SectionDetails.BottomPlate.Width;
                tp = mem.SectionDetails.BottomPlate.Thickness;

                w = MyList.StringToDouble(txt_w.Text, 0.0);
                //d = MyList.StringToDouble(txt_d.Text, 0.0);
                fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                m = MyList.StringToDouble(txt_m.Text, 0.0);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("DESIGN OF SHEAR CONNECTOR");
                sw.WriteLine("-------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("USER'S INPUT DATA");
                sw.WriteLine("-----------------");
                sw.WriteLine();

                sw.WriteLine("Shear Force = V = {0} kN = {0} * 10^3 N", V);
                sw.WriteLine("Ixx = {0:f3} sq.sq.cm", Ixx);
                sw.WriteLine("a =  {0:f3} sq.cm", a);
                sw.WriteLine("Depth of Girder = D = {0} mm", D);
                sw.WriteLine("Flange Width = B = {0} mm", B);
                sw.WriteLine("tw = {0} mm", tw);
                sw.WriteLine("tf = {0} mm", tf);
                sw.WriteLine("Side Plates = bs x ts = {0} x {1}", bs, ts);
                sw.WriteLine("Bottom Plate = bp x tp = {0} x {1}", bp, tp);
                sw.WriteLine("Spacing of Cross Girders = w = {0} mm", w);
                sw.WriteLine("Thickness of Deck Slab = d = {0} mm", d);
                sw.WriteLine("Concrete Grade = fck = {0}", fck);
                sw.WriteLine("Modular Ratio = m = {0}", m);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Design Calculation");
                sw.WriteLine("------------------");
                sw.WriteLine();
                sw.WriteLine();

                double Ay = (w * d / m) * (D + tp + (d / 2)) + a * 100 * ((D / 2) + tp) + bp * tp * (tp / 2) + 2 * bs * ts * ((D / 2) + tp);

                sw.WriteLine(" Ay = (w * d / m) * (D + tp + (d / 2)) + a * 100 * ((D / 2) + tp) + bp * tp * (tp / 2) + 2 * bs * ts * ((D / 2) + tp)");
                sw.WriteLine("    = ({0} * {1} / {2}) * ({3} + {4} + ({1} / 2)) + {5} * 100 * (({3} / 2) + {4}) + {6} * {4} * ({4} / 2) + 2 * {7} * {8} * (({3} / 2) + {4})",
                                w, d, m, D, tp, a, bp, bs, ts);

                sw.WriteLine("    = {0:f3} ", Ay);
                sw.WriteLine();


                double Ay1 = (w * d / m) + a * 100 + bp * tp + 2 * bs * ts;

                sw.WriteLine("Ay = (w * d / m) + a * 100 + bp * tp + 2 * bs * ts");
                sw.WriteLine("   = ({0} * {1} / {2}) + {3} * 100 + {4} * {5} + 2 * {6} * {7}",
                    w, d, m, a, bp, tp, bs, ts);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} ", Ay1);
                sw.WriteLine();

                double y = Ay / Ay1;
                sw.WriteLine(" y' = {0:f3} / {1:f3} = {2:f3} mm", Ay, Ay1, y);
                sw.WriteLine("   = Distance of Neutral Axis from bottom edge of Composite Section");
                sw.WriteLine();

                double y1 = y - ((D / 2) + tp);

                sw.WriteLine("y  = y' - ((D/2) + tp)");
                sw.WriteLine("   = {0:f3} - (({1}/2) + {2})", y, D, tp);
                sw.WriteLine("   = {0:f3} ", y1);
                sw.WriteLine("   = Distance between NA of Composite Section and C.G. of Cross Girder");
                sw.WriteLine();

                double yd = d + D + tp - y;
                sw.WriteLine("  yd = Distance of NA from top of Composite Section");
                sw.WriteLine("     = d + D + tp - y'");
                sw.WriteLine("     = {0} + {1} + {2} - {3:f3}", d, D, tp, y);
                sw.WriteLine("     = {0:f3} mm", yd);
                sw.WriteLine();

                double Iz = ((w * d) / m) * Math.Pow((yd - (d / 2)), 2) + a * 100 * Math.Pow((y - ((d / 2) + tp)), 2) + bp * tp * Math.Pow((y - (tp / 2)), 2);

                sw.WriteLine(" Iz = ((w * d) / m) * ((yd - (d / 2))^2) + a * 100 * ((y - ((d / 2) + tp))^2) + bp * tp * ((y - (tp / 2))^2)");
                sw.WriteLine();

                sw.WriteLine("    = {0:f3} sq.sq.mm", Iz);
                sw.WriteLine();

                double Ty = (V * Ay * yd) / Iz;
                sw.WriteLine("Shear Stress at top Flange Level = τy = (V x Ay x yd) / Iz");
                sw.WriteLine("                                 = ({0:f3} * {1:f3} * {2:f3}) / {3:f3}", V, Ay, yd, Iz);
                sw.WriteLine("                                 = {0:f3}", Ty);
                sw.WriteLine();

                double lamda = 0.85d;
                RolledSteelChannelsRow tabChn = tbl_rolledSteelChannels.GetDataFromTable(cmb_Shr_Con_Section_name.Text, cmb_Shr_Con_Section_Code.Text);
                tw = tabChn.WebThickness;
                tf = tabChn.FlangeThickness;
                B = tabChn.FlangeWidth;

                L = B - 2 * 10;
                double Qu = 45 * lamda * (tf + 0.5 * tw) * L * Math.Sqrt(fck);

                sw.WriteLine("Shear Capacity of H.T. Channel = Qu = 45 * λ * (tf + 0.5 * tw) * L * √fck");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Try {0} {1} ", cmb_Shr_Con_Section_name.Text, cmb_Shr_Con_Section_Code.Text);
                sw.WriteLine("Web Thickness = tw = {0} ", tw);
                sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
                sw.WriteLine("Flange Width = B = {0} mm", B);

                sw.WriteLine();
                sw.WriteLine("L = B – 2 x 10 = {0} – 20 = {1}, λ = Load Reduction Factor = 0.85", B, L);
                sw.WriteLine();

                sw.WriteLine("   Qu = 45 x 0.85 x ({0} + 0.5 * {1}) x {2} x √{3}", tf, tw, L, fck);
                sw.WriteLine("      = {0:f3} N", Qu);
                sw.WriteLine("      = {0:f3} kN", Qu / 1000.0);
                sw.WriteLine("      = {0:f3} MTon", Qu / 10000.0);
                sw.WriteLine();
                sw.WriteLine();

                double sr = Qu / Ty;
                sw.WriteLine("Spacing Required = Qu / τy = {0:f3} / {1:f2} = {2:f2} mm", Qu, Ty, sr);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
            }
            catch (Exception ex) { }

        }
        public void DesignLacing(StreamWriter sw, CMember mbr, double lamda)
        {
            string section_name = mbr.SectionDetails.SectionName;
            string section_code = mbr.SectionDetails.SectionCode;
            double rad = (Math.PI / 180.0);
            double lac_ang, lac_bl, lac_tl, lac_d2, lac_nr, lac_fs, lac_fb, weld_strength;


            //if (cmb_lac.SelectedIndex == 0)
            //{

            lac_ang = MyList.StringToDouble(txt_lac_ang.Text, 0.0);
            lac_bl = MyList.StringToDouble(cmb_lac_bl.Text, 0.0);
            lac_tl = MyList.StringToDouble(cmb_lac_tl.Text, 0.0);
            lac_d2 = MyList.StringToDouble(txt_lac_d2.Text, 0.0);
            lac_nr = MyList.StringToDouble(txt_lac_nr.Text, 0.0);
            lac_fs = MyList.StringToDouble(txt_lac_fs.Text, 0.0);
            lac_fb = MyList.StringToDouble(txt_lac_fb.Text, 0.0);
            weld_strength = MyList.StringToDouble(txt_weld_strength.Text, 108);

            //}
            if (cmb_lac.SelectedIndex == 1)
            {
                string kStr = cmb_lac_bl.Text.Trim();
                string kStr2 = "";
                int cnt = kStr.Length;
                if (cnt % 2 != 0)
                    cnt = cnt / 2 + 1;
                else
                    cnt = cnt / 2;
                kStr = cmb_lac_bl.Text.Trim().Substring(0, cnt);
                kStr2 = cmb_lac_bl.Text.Trim().Substring(cnt, cmb_lac_bl.Text.Trim().Length - cnt);

                lac_ang = MyList.StringToDouble(txt_lac_ang.Text, 0.0);

                lac_bl = (double.Parse(kStr) > double.Parse(kStr2)) ? double.Parse(kStr) : double.Parse(kStr2);

                //lac_bl = MyList.StringToDouble(cmb_lac_bl.Text, 0.0);
                lac_tl = MyList.StringToDouble(cmb_lac_tl.Text, 0.0);
                lac_d2 = MyList.StringToDouble(txt_lac_d2.Text, 0.0);
                lac_nr = MyList.StringToDouble(txt_lac_nr.Text, 0.0);
                lac_fs = MyList.StringToDouble(txt_lac_fs.Text, 0.0);
                lac_fb = MyList.StringToDouble(txt_lac_fb.Text, 0.0);

            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design of Lacing for Compressive force");
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("USER'S DATA");
            sw.WriteLine("-----------");
            sw.WriteLine();
            sw.WriteLine();
            //double s = MyList.StringToDouble(mbr.SectionDetails.SectionCode, 400);


            //RolledSteelChannelsRow tab = tbl_rolledSteelChannels.GetDataFromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode);
            //double B = tab.FlangeWidth;
            //sw.WriteLine("Clear spacing between two {0} {1} = s = {2} mm.",mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode, s);
            //sw.WriteLine("Flange width of {0} {1} = B = {2} mm", mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode, B);
            //sw.WriteLine("Centre to centre distance between members = s + 2 x B/2");
            //sw.WriteLine("                                          = {0} + 2 x {1}/2", s, B);
            //sw.WriteLine("                                          = {0} mm.", cc_dis);
            //sw.WriteLine();
            sw.WriteLine("Lacing Angle = {0}˚(Degrees)", lac_ang);

            if (cmb_lac.SelectedIndex == 0)
            {
                sw.WriteLine("Lacing Plate width = bl = {0} mm", lac_bl);
                sw.WriteLine("Thickness = tl = {0} mm.", lac_tl);
            }
            else if (cmb_lac.SelectedIndex == 1)
            {
                sw.WriteLine();
                sw.WriteLine("Lacing Angle  1 x ISA {0} x {1}", cmb_lac_bl.Text, cmb_lac_tl.Text);
                sw.WriteLine();
            }
            double weld_size = MyList.StringToDouble(txt_weld_size.Text, lac_tl - 1.6);
            double throat_size = weld_size * 0.7; ;

            if (rbtn_weld.Checked)
            {


                if (weld_size > (lac_tl - 1.5))
                {
                    MessageBox.Show(this, "Weld Size = " + weld_size + " mm     must be Less than = Thickness - 1.5 = " + lac_tl + " - 1.5 = " + (lac_tl - 1.5) + " mm", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception("Weld Size greater than thickness - 1.5");
                }
                sw.WriteLine("WELD size = {0} mm  < {1} (Thickness) - 1.5mm.", weld_size, lac_tl);
                sw.WriteLine("Throat size = 0.7 x {0} = {1} mm", weld_size, throat_size);

                sw.WriteLine();
                sw.WriteLine("Welding Strength = {0} N/sq.mm", weld_strength);


            }
            else
            {
                sw.WriteLine("Bolt/Rivet Diameter = d2 = {0} mm", lac_d2);
                sw.WriteLine("Number per row = nr = {0}", lac_nr);
                sw.WriteLine("Rivet/Bolt    Shear strength = fs = {0} N/Sq. mm ", lac_fs);
                sw.WriteLine("Bearing strength = fb = {0} N/Sq.mm", lac_fb);
            }
            sw.WriteLine();
            //double s = MyList.StringToDouble(mbr.SectionDetails.SectionCode, 0.0);
            double s = mbr.SectionDetails.LateralSpacing;
            sw.WriteLine("Spacing of Member components = s = {0} mm.", s);
            sw.WriteLine();
            //double cc_dis = s + 2 * B / 2.0;

            //sw.WriteLine("Clear spacing between two ISMC 400 = s = 400 mm.");

            double B = 0.0;
            double cc_dis = 0.0;
            double ryy = 0.0;
            double Iy = lac_bl * lac_tl * lac_tl * lac_tl / 12.0;
            a = lac_tl * lac_bl;

            if (cmb_lac.SelectedIndex == 1)
            {
                RolledSteelAnglesRow tabData1 = iApp.Tables.Get_AngleData_FromTable("ISA", cmb_lac_bl.Text, lac_tl);
                a = tabData1.Area * 100;
                Iy = tabData1.Iyy * 10000;
                sw.WriteLine("Area    a = {0} sq.mm.", a);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("       Iy = {0:f3} sq.sq.mm.", Iy);
                sw.WriteLine();
            }
            else
            {
                sw.WriteLine("Area     a = bl * tl = {0} * {1} = {2} sq.mm.", lac_bl, lac_tl, a);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("       Iy  = bl * tl**3 / 12.0");
                sw.WriteLine("           = {0} * {1}^3 / 12.0", lac_bl, lac_tl);
                sw.WriteLine("           = {0:f3} sq.sq.mm.", Iy);
                sw.WriteLine();
            }
            if (mbr.SectionDetails.SectionName.EndsWith("A"))
            {
                RolledSteelAnglesRow tabData = iApp.Tables.Get_AngleData_FromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode, mbr.SectionDetails.AngleThickness);

                ryy = Math.Sqrt(tabData.Iyy / tabData.Area);

                sw.WriteLine("Flange width of {0} {1}  = B = {2} mm.",
                    mbr.SectionDetails.SectionName,
                    mbr.SectionDetails.SectionCode, B);
                //double cc_dis = s + (2.0 * B / 2.0);

                //will be opened when sections are back to back
                sw.WriteLine("Centre to centre distance between members = s + 2 * B/2");
                sw.WriteLine("                                          = {0} + 2 * {1}/2", s, B);
                sw.WriteLine("                                          = {0:f3} mm.", cc_dis);
            }
            else if (mbr.SectionDetails.SectionName.EndsWith("C"))
            {
                RolledSteelChannelsRow tabData = iApp.Tables.Get_ChannelData_FromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode);

                B = tabData.FlangeWidth;

                RolledSteelAnglesRow aa;

                ryy = tabData.Ryy;

                //sw.WriteLine("Flange width of {0} {1}  = B = {2} mm.",
                //    mbr.SectionDetails.SectionName,
                //    mbr.SectionDetails.SectionCode, B);
                //double cc_dis = s + (2.0 * B / 2.0);
                //will be opened when sections are back to back
                //sw.WriteLine("Centre to centre distance between members = s + 2 * B/2");
                //sw.WriteLine("                                          = {0} + 2 * {1}/2", s, B);
                //sw.WriteLine("                                          = {0:f3} mm.", cc_dis);
            }
            else if (mbr.SectionDetails.SectionName.EndsWith("B"))
            {
                RolledSteelBeamsRow tabData = iApp.Tables.Get_BeamData_FromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode);

                //B = tabData.FlangeWidth;

                ryy = tabData.Ryy;

                //sw.WriteLine("Flange width of {0} {1}  = B = {2} mm.",
                //    mbr.SectionDetails.SectionName,
                //    mbr.SectionDetails.SectionCode, B);
                ////double cc_dis = s + (2.0 * B / 2.0);

                //will be opened when sections are back to back
                //sw.WriteLine("Centre to centre distance between members = s + 2 * B/2");
                //sw.WriteLine("                                          = {0} + 2 * {1}/2", s, B);
                //sw.WriteLine("                                          = {0:f3} mm.", cc_dis);
            }

            sw.WriteLine("Length of the member between two successive Lacing connections = l1");
            cc_dis = s;

            //double l1 = 2 * cc_dis * Math.Tan((lac_ang / 2.0) * rad);
            double l1 = cc_dis * Math.Tan((lac_ang) * rad);

            sw.WriteLine("   = {0:f3} * tan({1}) = {2:f3} mm", cc_dis, lac_ang, l1);
            sw.WriteLine();



            ryy = Math.Sqrt(Iy / a);
            sw.WriteLine("Radius of gyration = ryy = SQRT(Iy/a)");
            sw.WriteLine("                   = SQRT({0:f3}/{1:f2}) mm.", Iy, a);
            sw.WriteLine("                   = {0:f3} mm.", ryy);
            sw.WriteLine();

            //sw.WriteLine("Slenderness ratio for one {0} {1} between two successive lacing connections", section_name, section_code);
            sw.WriteLine("Slenderness ratio between two successive lacing connections");

            double lamda1 = l1 / ryy;

            sw.WriteLine(" λ1 = l1 / ryy = {0:f3} / {1:f3} = {2:f3}", l1, ryy, lamda1);
            sw.WriteLine();

            sw.WriteLine("Slenderness Ratio of the Member under compressive force ");
            //double lamda = 24.874;
            sw.WriteLine(" = λ  = {0:f3}", lamda);
            sw.WriteLine();
            //Chiranjit [2011 04 13]
            //if (lamda1 <= (lamda * 0.7))
            //if (lamda1 <= (lamda * 0.7))
            //{
            //    sw.WriteLine(" λ1 = {0:f3} < 0.7 * λ = 0.7 * {1:f3} ", lamda1, lamda);
            //    sw.WriteLine("                    = {0:f3} So, Lacing geometry is OK .", (lamda * 0.7));
            //}
            //else
            //{
            //    sw.WriteLine(" λ1 = {0:f3} > 0.7 * λ = 0.7 * {1:f3} ", lamda1, lamda);
            //    sw.WriteLine("                    = {0:f3} So, Lacing geometry is NOT OK .", (lamda * 0.7));
            //}

            if (lamda1 <= 145.0)
            {
                sw.WriteLine(" λ1 = {0:f3} < 145.0  So, Lacing geometry is OK .", lamda1);
            }
            else
            {
                sw.WriteLine(" λ1 = {0:f3} > 145.0 So, Lacing geometry is NOT OK .", lamda1);
            }

            sw.WriteLine();

            sw.WriteLine("Horizontal force for which the Lacings are to be designed");
            sw.WriteLine("    = 2.5% of Compressive Load in the member");
            sw.WriteLine("    = 0.025 * {0:f3} kN", mbr.MaxCompForce.Force);
            double hor_force = 0.025 * mbr.MaxCompForce.Force;
            sw.WriteLine("    = {0:f3} kN", hor_force);
            hor_force = hor_force * 1000;
            sw.WriteLine("    = {0:f0} N", hor_force);
            sw.WriteLine();

            sw.WriteLine("Thrust in each Lacing Plate = T ");
            sw.WriteLine();
            sw.WriteLine(" 2 * T * cos ({0}/2) = {1:f0}", lac_ang, hor_force);
            double T = (hor_force / ((2 * Math.Cos((lac_ang / 2) * rad))));
            sw.WriteLine();
            sw.WriteLine(" T = {0:f0} / (2 * cos ({1}/2) = {2:f3} N", hor_force, lac_ang, T);
            sw.WriteLine();

            double pl_wd, pl_thk, pl_area;
            //pl_wd = 50.0;
            //pl_thk = 12.0;
            //pl_area = pl_wd * pl_thk;
            //sw.WriteLine("Try, {0} mm * {1} mm Plates,", pl_wd, pl_thk);
            //sw.WriteLine("Area of the Plate = a = {0} * {1} = {2} Sq.mm.", pl_wd, pl_thk, pl_area);

            //double I = pl_wd * pl_thk * pl_thk * pl_thk / 12.0;
            //sw.WriteLine("Moment of Inertia = I = {0} x {1}^3 / 12 = {2:f3} Sq.Sq.mm", pl_wd, pl_thk, I);
            //sw.WriteLine();

            //sw.WriteLine("Length of between successive connections = l1 = {0:f3} mm", l1);
            //double r = Math.Sqrt(I / pl_area);
            //sw.WriteLine("Radius of gyration = r = √(I / a) =  √ ({0:f3} / {1}) = {2:f3} mm.", I, pl_area, r);
            sw.WriteLine();
            double lamda2 = l1 / ryy;
            sw.WriteLine("Slenderness ratio = λ2 = l1/r = {0:f3} / {1:f3} = {2:f3}", l1, ryy, lamda2);
            sw.WriteLine();
            double sigma_ac = Get_Table_1_Sigma_Value(lamda2, fy);
            sw.WriteLine("From Table 4,   σ_c = {0}  N / Sq. mm.", sigma_ac);
            sw.WriteLine();
            double app_com = T / a;

            sw.WriteLine("Applied Compressive stress = T / a");
            sw.WriteLine("                           = {0:f3} / {1}", T, a);
            if (app_com <= sigma_ac)
            {
                sw.WriteLine("                           = {0:f3} N/Sq.mm. < {1:f3}(σ_c),    So, OK", app_com, sigma_ac);
            }
            else
            {
                sw.WriteLine("                           = {0:f3} N/Sq.mm. > {1:f3}(σ_c),    So, NOT OK", app_com, sigma_ac);
            }






            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            double rss = 0.0, rvt_val = 0.0, safe_load = 0.0;
            //bool flag = false;
            double[] rvt_dia = new double[] { 16, 18, 20, 22, 24, 25, 26, 30, 32 };
            foreach (var item in rvt_dia)
            {
                lac_d2 = item;
                rss = lac_fs * Math.PI * (1.5 + lac_d2) * (1.5 + lac_d2) / 4.0;
                rvt_val = lac_fb * (1.5 + lac_d2) * lac_tl;
                safe_load = (rss > rvt_val) ? rvt_val : rss;
                if (safe_load >= T)
                {
                    break;
                }
            }

            //while ();


            //double rss = lac_fs * Math.PI * (1.5 + lac_d2) * (1.5 + lac_d2) / 4.0;
            //double rvt_val = lac_fb * (1.5 + lac_d2) * lac_tl;
            //double safe_load = (rss > rvt_val) ? rvt_val : rss;
            double tot_len = T / (throat_size * weld_strength);
            if (rbtn_weld.Checked)
            {
                sw.WriteLine("Thrust  = T = {0:f3} N ", T);
                sw.WriteLine("Welding Strength = ws = {0:f3} N/sq.mm ", weld_strength);
                sw.WriteLine("Throat size = ts = {0:f3} mm ", throat_size);
                sw.WriteLine();

                sw.WriteLine("Total Length of Weld Required = T / (ts * ws)");
                sw.WriteLine("                              = {0:f3} / ({1:f3} * {2:f3}) ", T, throat_size, weld_strength);
                sw.WriteLine("                              = {0:f3} mm", tot_len);
            }
            else
            {

                sw.WriteLine("Using {0} mm. diameter rivets, ", lac_d2);
                sw.WriteLine();

                sw.WriteLine("Rivet value in single shear = (fs * π * (d2+1.5)^2) / 4 ");
                sw.WriteLine("                            = ({0} * π * ({1}+1.5)^2) / 4 ", lac_fs, lac_d2);
                sw.WriteLine("                            = {0:f3} N", rss);


                sw.WriteLine("Rivet value in bearing = fb * d2 * t");
                sw.WriteLine("                       = {0} * ({1}+1.5) * {2}", lac_fb, lac_d2, lac_tl);
                sw.WriteLine("                       = {0} N", rvt_val);
                sw.WriteLine();
                sw.WriteLine("Safe Load in each rivet is the minimum of the above two values");


                if (safe_load >= T)
                {
                    sw.WriteLine("  = {0:f3} N. > Load in the Lacing Plate = T = {1:f3} N   So, OK.", safe_load, T);
                    sw.WriteLine();
                }
                else
                {
                    sw.WriteLine("two values = {0:f3} N. < Load in the Lacing Plate = T = {1:f3} N   So, NOT OK.", safe_load, T);
                    sw.WriteLine();
                }
            }
            sw.WriteLine();

            /**/
        }
        public void DesignConnection(StreamWriter sw, CMember mbr)
        {
            double conn_d, conn_nr, conn_bg, conn_tg, conn_fs, conn_fb, conn_ft;

            conn_d = MyList.StringToDouble(txt_conn_d.Text, 0.0);
            conn_nr = MyList.StringToDouble(txt_conn_nr.Text, 0.0);
            conn_bg = MyList.StringToDouble(txt_conn_bg.Text, 0.0);
            conn_tg = MyList.StringToDouble(txt_conn_tg.Text, 0.0);
            conn_fs = MyList.StringToDouble(txt_conn_fs.Text, 0.0);
            conn_fb = MyList.StringToDouble(txt_conn_fb.Text, 0.0);
            conn_ft = MyList.StringToDouble(txt_conn_ft.Text, 0.0);


            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Design of Connection for Tensile force");
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("USER,S DATA");
            sw.WriteLine("-----------");
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Bolt/Rivet Diameter = d = {0} mm.", conn_d);
            sw.WriteLine("Minimum Number of rivets/bolts per row = nr = {0}", conn_nr);
            sw.WriteLine();
            sw.WriteLine("Width of Gusset Plate = bg = {0} mm", conn_bg);
            sw.WriteLine("Thickness of Gusset Plate = tg = {0} mm", conn_tg);
            sw.WriteLine("Shear strength = fs = {0} N/Sq.mm", conn_fs);
            sw.WriteLine("Bearing strength = fb = {0} N/Sq.mm", conn_fb);
            sw.WriteLine("Tearing strength = ft = {0} N/Sq.mm.", conn_ft);
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Design Calculation");
            sw.WriteLine("------------------");
            sw.WriteLine();
            sw.WriteLine();
            double rvt_strength_shr = (conn_fs * Math.PI * (conn_d + 1.5) * (conn_d + 1.5) / 4.0);

            sw.WriteLine("Rivet strength in single shear = (fs * π * d * d) / 4 ");
            sw.WriteLine("                               = ({0} * π * ({1}+1.5) * ({1}+1.5)) / 4 ", conn_fs, conn_d);
            sw.WriteLine("                               = {0:f3} N", rvt_strength_shr);
            rvt_strength_shr = rvt_strength_shr / 1000.0;
            sw.WriteLine("                               = {0:f3} kN.", rvt_strength_shr);
            sw.WriteLine();
            double rvt_strength_brn = conn_fb * (conn_d + 1.5) * conn_tg;

            sw.WriteLine("Rivet strength in bearing = fb * d * tg");
            sw.WriteLine("                          = {0} * ({1}+1.5) * {2}", conn_fb, conn_d, conn_tg);
            sw.WriteLine("                          = {0:f3} N.", rvt_strength_brn);
            rvt_strength_brn = rvt_strength_brn / 1000.0;
            sw.WriteLine("                          = {0:f3} kN.", rvt_strength_brn);
            sw.WriteLine();
            double safe_load = (rvt_strength_shr > rvt_strength_brn) ? rvt_strength_brn : rvt_strength_shr;

            sw.WriteLine("Safe Load in each rivet is the minimum of the above two values = {0:f3} kN.", safe_load);
            sw.WriteLine();
            int no_rvt = (int)(mbr.MaxTensionForce.Force / safe_load);
            no_rvt++;

            sw.WriteLine("Number of Rivets/Bolts required ");
            sw.WriteLine("     = Tensile Force / Safe Load in each Rivet/Bolt");
            sw.WriteLine("     = {0} / {1:f3}", mbr.MaxTensionForce, safe_load);
            sw.WriteLine("     = {0} nos.", no_rvt);
            sw.WriteLine();
            sw.WriteLine();

            int no_end_rvt = (int)((double)no_rvt / 2.0);
            no_end_rvt++;

            sw.WriteLine("Use Rivets/Bolts at both sides of both ends of the member = {0} / 2 = {1} = {2} nos.", no_rvt, (no_rvt / 2.0), no_end_rvt);
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Design of Gusset Plate for Tensile force");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double net_width = conn_bg - conn_nr * (conn_d + 1.5);

            sw.WriteLine("Net width of plate for effective section ");
            sw.WriteLine("    = bg – nr * (d+1.5) ");
            sw.WriteLine("    = {0} – {1} * ({2}+1.5)", conn_bg, conn_nr, conn_d);
            sw.WriteLine("    = {0:f3} mm", net_width);
            sw.WriteLine();

            sw.WriteLine("One Gusset plate is required at both sides and at both ends of each member");
            sw.WriteLine("So, 2 Gusset plates are to support the Tensile load = {0:f3} kN", mbr.MaxTensionForce);
            sw.WriteLine();

            double safe_tens_load = 2 * 150 * conn_bg * 12.0;
            sw.WriteLine("Safe Tensile Load carrying capacity ");
            sw.WriteLine("  = Tensile strength * Net area of effective section of 2 Gusset Plates");
            sw.WriteLine("  = 2 * 150 * bg * 12");
            //sw.WriteLine("  = 4 * 150 * {0} * 12", conn_bg);
            sw.WriteLine();

            double bg = (mbr.MaxTensionForce.Force * 1000.0 / (2d * 150.0 * 12.0));
            sw.WriteLine("Net width of each plate required = bg");
            sw.WriteLine("                                 = ({0:f3} * 1000) / (2 x 150 x 12)", mbr.MaxTensionForce);
            sw.WriteLine("                                 = {0:f3} mm.", bg);
            sw.WriteLine();
            double grs_wd = bg + conn_nr * (conn_d + 1.5);
            sw.WriteLine("Gross width of plate for effective section ");
            sw.WriteLine("        = bg + nr * (d+1.5)");
            sw.WriteLine("        = {0:f3} + {1} * ({2}+1.5)", bg, conn_nr, conn_d);
            sw.WriteLine("        = {0:f3} mm", grs_wd);
            grs_wd = (int)(grs_wd / 10.0);
            grs_wd = (grs_wd + 1) * 10;
            sw.WriteLine("        = {0:f0} mm", grs_wd);
            sw.WriteLine();

            sw.WriteLine("Use Gusset Plates of size {0}mm x 12mm at both sides of both ends of the member.", grs_wd);
            sw.WriteLine();

        }

        #region Table Functions
        void WriteTable1(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "SteelTruss_Table1.txt");

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
            //List<string> file_cont = new List<string>(File.ReadAllLines(tab_file));

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
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "Truss_Bridge_3.txt");

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
                sw.WriteLine("TABLE 4 :");
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
        public double Get_Table_1_Sigma_Value(double lamda, double fy)
        {
            return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, fy, ref ref_string);

            //lamda = Double.Parse(lamda.ToString("0.000"));

            //int indx = 5;

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");

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
            //    kStr = kStr.Replace("Fy = ", "");
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
            //a1 = 0.0;
            //a2 = 0.0;
            //b1 = 0.0;
            //b2 = 0.0;
            //int _col_index = -1;
            //for (int i = 1; i < lst_list[0].Count; i++)
            //{
            //    if (fy >= lst_list[0].GetDouble(i - 1) && fy <= lst_list[0].GetDouble(i))
            //    {
            //        a1 = lst_list[0].GetDouble(i - 1);
            //        a2 = lst_list[0].GetDouble(i);
            //        _col_index = i+1;
            //        break;
            //    }
            //}
            //double v1, v2, v3, v4, val1, val2, val3;
            //v1 = 0.0; v2 = 0.0; v3 = 0.0; v4 = 0.0;
            //for (int i = 2; i < lst_list.Count; i++)
            //{
            //    b1 = lst_list[i - 1].GetDouble(0);
            //    b2 = lst_list[i].GetDouble(0);

            //    if (lamda >= b1 && lamda <= b2)
            //    {
            //        v1 = lst_list[i - 1].GetDouble(_col_index - 1);
            //        v2 = lst_list[i - 1].GetDouble(_col_index);

            //        v3 = lst_list[i].GetDouble(_col_index - 1);
            //        v4 = lst_list[i].GetDouble(_col_index);
            //        break;
            //    }
            //}
            //if (v1 == 0.0 && v2 == 0.0 && v3 == 0.0 && v4 == 0.0)
            //{
            //    v1 = lst_list[lst_list.Count - 1].GetDouble(_col_index - 1);
            //    v2 = lst_list[lst_list.Count - 1].GetDouble(_col_index);

            //    v3 = lst_list[lst_list.Count - 1].GetDouble(_col_index - 1);
            //    v4 = lst_list[lst_list.Count - 1].GetDouble(_col_index);
            //}
            ////a1 = 0.0; a2 = 0.0; b1 = 0.0; 
            ////b2 = 0.0; v1 = 0.0; 
            ////v2 = 0.0; v3 = 0.0; v4 = 0.0; 
            //val1 = 0.0; val2 = 0.0; val3 = 0.0;


            //val1 = v1 + ((v2 - v1) / (a2 - a1)) * (fy - a1);
            //val2 = v3 + ((v4 - v3) / (a2 - a1)) * (fy - a1);

            //if (v1 == v3) val1 = v1;
            //if (v2 == v4) val2 = v2;

            //returned_value = val1 + ((val2 - val1) / (b2 - b1)) * (lamda - b1);



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



        public void Add_LiveLoad()
        {
            try
            {
                dgv_live_load.Rows.Add(cmb_load_type.Text, txt_X.Text, txt_Y.Text, txt_Z.Text, txt_XINCR.Text);
            }
            catch (Exception ex) { }

        }

        void LoadReadFromGrid()
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList = new List<LoadData>();
            //LoadList.Clear();
            MyList mlist = null;
            for (i = 0; i < dgv_live_load.RowCount; i++)
            {
                try
                {
                    ld = new LoadData();
                    mlist = new MyList(MyList.RemoveAllSpaces(dgv_live_load[0, i].Value.ToString().ToUpper()), ':');
                    ld.TypeNo = mlist.StringList[0];
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -18.8);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
                    ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                    LoadList.Add(ld);
                }
                catch (Exception ex) { }
            }
        }

        public void FillMemberResult()
        {
            try
            {
                dgv_member_Result.Rows.Clear();
                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    if (design_member == Complete_Design.Members[i].MemberType || design_member == eMemberType.AllMember)
                    {
                        dgv_member_Result.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                            Complete_Design.Members[i].MemberType.ToString(),
                            Complete_Design.Members[i].MaxCompForce.Force.ToString("0.00"),
                            Complete_Design.Members[i].Capacity_CompForce.ToString("0.00"),
                            Complete_Design.Members[i].MaxTensionForce.Force.ToString("0.00"),
                            Complete_Design.Members[i].Capacity_TensionForce.ToString("0.00"),
                            Complete_Design.Members[i].MaxMoment.Force.ToString("0.00"),
                            Complete_Design.Members[i].Required_SectionModulus.ToString("E3"),
                            Complete_Design.Members[i].Capacity_SectionModulus.ToString("E3"),
                            Complete_Design.Members[i].MaxShearForce.Force.ToString("0.00"),
                            Complete_Design.Members[i].Required_ShearStress.ToString("0.00"),
                            Complete_Design.Members[i].Capacity_ShearStress.ToString("0.00"),
                            Complete_Design.Members[i].Result);
                    }
                }
                WriteResult();
                FillResultGridWithColor();
            }
            catch (Exception ex) { }


        }
        public void FillResultGridWithColor()
        {
            try
            {
                for (int i = 0; i < dgv_member_Result.RowCount; i++)
                {
                    if (dgv_member_Result[12, i].Value.ToString().ToUpper() == "NOT OK")
                    {
                        SetGroupResultColor(dgv_member_Result[0, i].Value.ToString(), Color.Red);
                        dgv_member_Result.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    if (dgv_member_Result[12, i].Value.ToString().ToUpper().Trim().TrimEnd().TrimStart() == "OK")
                    {
                        SetGroupResultColor(dgv_member_Result[0, i].Value.ToString(), Color.Green);
                        dgv_member_Result.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception xe) { }
        }
        void SetGroupResultColor(string mem_grp, Color clr)
        {
            for (int i = 0; i < dgv_mem_details.RowCount; i++)
            {
                if (dgv_mem_details[0, i].Value.ToString().ToUpper() == mem_grp.ToUpper())
                {
                    dgv_mem_details.Rows[i].DefaultCellStyle.ForeColor = clr;
                    return;
                }
                //if (dgv_mem_details[12, i].Value.ToString().ToUpper() == "OK")
                //{
                //    dgv_mem_details.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                //}
            }
        }

        public void ReadResult()
        {

            string kFile = Path.Combine(system_path, "result.csv");
            if (!File.Exists(kFile))
            {
                kFile = Path.Combine(system_path, "result.txt");
            }
            if (!File.Exists(kFile)) return;
            List<string> list = new List<string>(File.ReadAllLines(kFile));
            MyList mlist = null;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_member_Result.Rows.Add(mlist.StringList.ToArray());
            }
        }
        public void WriteResult()
        {

            string kFile = Path.Combine(system_path, "result.csv");

            StreamWriter sw = new StreamWriter(new FileStream(kFile, FileMode.Create));
            try
            {
                for (int i = 0; i < dgv_member_Result.RowCount; i++)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                        dgv_member_Result[0, i].Value.ToString(),//Group
                        dgv_member_Result[1, i].Value.ToString(),//Type
                        dgv_member_Result[2, i].Value.ToString(),//Comp
                        dgv_member_Result[3, i].Value.ToString(),//Tens
                        dgv_member_Result[4, i].Value.ToString(),//Moment
                        dgv_member_Result[5, i].Value.ToString(),//Shear
                        dgv_member_Result[6, i].Value.ToString(),//Shear
                        dgv_member_Result[7, i].Value.ToString(),//Shear
                        dgv_member_Result[8, i].Value.ToString(),//Shear
                        dgv_member_Result[9, i].Value.ToString(),//Shear
                        dgv_member_Result[10, i].Value.ToString(),//Shear
                        dgv_member_Result[11, i].Value.ToString(),//Shear
                        dgv_member_Result[12, i].Value.ToString());//Result
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Read_DL_SIDL()
        {

            string kFile = Path.Combine(system_path, "DL_SIDL.csv");
            if (!File.Exists(kFile))
            {
                kFile = Path.Combine(system_path, "DL_SIDL.txt");
            }
            if (!File.Exists(kFile)) return;
            List<string> list = new List<string>(File.ReadAllLines(kFile));
            MyList mlist = null;
            dgv_SIDL.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_SIDL.Rows.Add(mlist.StringList.ToArray());
            }
            if (dgv_SIDL.Rows.Count > 0)
            {
                dgv_SIDL.Rows[0].Selected = true;
            }
        }
        public void Write_DL_SIDL()
        {

            string kFile = Path.Combine(system_path, "DL_SIDL.csv");

            StreamWriter sw = new StreamWriter(new FileStream(kFile, FileMode.Create));
            try
            {
                for (int i = 0; i < dgv_SIDL.RowCount; i++)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                        dgv_SIDL[0, i].Value.ToString(),//Name
                        dgv_SIDL[1, i].Value.ToString(),//Length
                        dgv_SIDL[2, i].Value.ToString(),//B
                        dgv_SIDL[3, i].Value.ToString(),//D
                        dgv_SIDL[4, i].Value.ToString(),//N
                        dgv_SIDL[5, i].Value.ToString(),//VOL
                        dgv_SIDL[6, i].Value.ToString(),//Gama
                        dgv_SIDL[7, i].Value.ToString());//Weight
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }


        public void Read_Live_Load()
        {

            string kFile = Path.Combine(system_path, "Live_load.csv");
            if (!File.Exists(kFile))
            {
                kFile = Path.Combine(system_path, "Live_load.txt");
            }

            if (!File.Exists(kFile)) return;
            List<string> list = new List<string>(File.ReadAllLines(kFile));
            MyList mlist = null;
            dgv_live_load.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_live_load.Rows.Add(mlist.StringList.ToArray());
            }
            //if (dgv_SIDL.Rows.Count > 0)
            //{
            //    dgv_SIDL.Rows[0].Selected = true;
            //}
        }
        public void Write_Live_Load()
        {

            string kFile = Path.Combine(system_path, "Live_load.csv");
            StreamWriter sw = new StreamWriter(new FileStream(kFile, FileMode.Create));
            try
            {
                for (int i = 0; i < dgv_live_load.RowCount; i++)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4}",
                        dgv_live_load[0, i].Value.ToString(),//Load Type
                        dgv_live_load[1, i].Value.ToString(),//X
                        dgv_live_load[2, i].Value.ToString(),//Y
                        dgv_live_load[3, i].Value.ToString(),//Z
                        dgv_live_load[4, i].Value.ToString());//XINC
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }


        void Fill_Angles_in_Combobox(ComboBox cmb)
        {
            string sec_code, sec_name;
            for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
            {
                sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                if (sec_name == "ISA" && sec_name != "")
                {
                    if (cmb.Items.Contains(sec_code) == false)
                    {
                        cmb.Items.Add(sec_code);
                    }
                }
            }
        }
        void Show_Total_Weight()
        {
            Complete_Design.AddWeightPercent = MyList.StringToDouble(txt_cd_force_percent.Text, 0.0);
            txt_steel_structure_weight.Text = (Complete_Design.TotalSteelWeight / 10.0).ToString("0.00");
            txt_total_structure_weight.Text = ((Complete_Design.Members.Weight + Complete_Design.GussetAndLacingWeight) / 10.0).ToString("0.00");
        }
        private void Set_Sections_Standard()
        {
            grb_bottom_plate.Enabled = true;
            grb_Top_plate.Enabled = true;
            grb_vertical_stiffener_plate.Enabled = true;

            if (cmb_sections_define.SelectedIndex == 12 || cmb_sections_define.SelectedIndex == 13)
            {
                grb_side_plate.Text = "WEB PLATE";
                grb_bottom_plate.Enabled = false;
                grb_Top_plate.Enabled = false;
                grb_vertical_stiffener_plate.Enabled = false;
            }
            else
            {
                grb_side_plate.Text = "SIDE PLATE";

            }
            SetComboSections();

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section11;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    break;
                case 1:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    break;
                case 2:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 3:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 4:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 5:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 6:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Top_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 7:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 8:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section9;
                    cmb_sec_thk.Visible = true;
                    cmb_section_name.Items.Clear();
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    break;
                case 9:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section10;
                    cmb_sec_thk.Visible = false;

                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";
                    break;
                case 10:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section111;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "2";
                    break;
                case 11:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section12;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";
                    break;
                //Chiranjit [2011 05 17]
                case 12:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section13;
                    cmb_sec_thk.Visible = true;

                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    break;
                //Chiranjit [2011 05 18]
                case 13:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section14;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                //Chiranjit [2011 05 18]
                case 14:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
            }

            cmb_code1.Width = cmb_sec_thk.Visible ? 93 : (cmb_select_standard.SelectedIndex == 0 ? 144 : 93);
        }


        #region Form Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            string drwg_path = "SteelTruss_Warren1";
            if (IsWarren2)
                drwg_path = "SteelTruss_Warren2";

            //if (Directory.Exists(drwg_path))
            //{
            iApp.RunViewer(Path.Combine(user_path,"Steel Truss Drawings"), drwg_path);
            //iApp.RunViewer(drwg_path);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            string mem_grp = dgv_member_Result[0, dgv_member_Result.CurrentRow.Index].Value.ToString();

            if (dgv_mem_details.RowCount == 0)
            {
                MessageBox.Show(this, "No Member found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(this, "This Process might take few minuites.\n Do you want to continue ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Write_User_Input();
                InitializeData();
                ReadDeadLoadInputs();
                Calculate_Program();
                FillMemberResult();
                if (File.Exists(rep_file_name))
                {
                    if (MessageBox.Show(this, "Report file written in file " + rep_file_name + "\n\n Do you want to open the report file?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        iApp.View_Result(rep_file_name);
                }
            }
            string kStr = "";
            for (int i = 0; i < dgv_member_Result.RowCount; i++)
            {
                kStr = dgv_member_Result[0, i].Value.ToString();
                if (kStr == mem_grp)
                {
                    dgv_member_Result.Rows[i].Selected = true;
                }
            }
            Button_Enable_Disable();
        }

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            SetWorkingFolder();
        }
        private void frmCompleteDesign_Load(object sender, EventArgs e)
        {
            if (IsWarren2)
            {
                lbl_sample_file.Text = "Sample file (Analysis Input Data [Warren2].txt) given in Examples folder \n [16] Steel Warren [2] Truss Bridge Data";
                lbl_sample_load.Text = "Sample Section + Load Data file (MEMBER_LOAD_DATA.txt) given in Examples folder \n [16] Steel Warren [2] Truss Bridge Data";
            }
            else
            {
                lbl_sample_file.Text = "Sample file (Analysis Input Data [Warren1].txt) given in Examples folder \n [15] Steel Warren [1] Truss Bridge Data";
                lbl_sample_load.Text = "Sample Section + Load Data file (MEMBER_LOAD_DATA.txt) given in Examples folder \n [15] Steel Warren [1] Truss Bridge Data";
            }
            rbtn_create_analysis_file.Checked = false;
            rbtn_create_analysis_file.Checked = true;
            rbtn_create_analysis_file.Location = new Point(rbtn_select_analysis_file.Location.X, rbtn_create_analysis_file.Location.Y);
            


            SetComboSections();
            cmb_sections_define.SelectedIndex = 0;
            cmb_design_member.SelectedIndex = 0;
            complete_design = new CompleteDesign();
            cmb_Shr_Con_Section_name.Items.Clear();
            cmb_Shr_Con_Section_name.Items.AddRange(tbl_rolledSteelChannels.Get_Channels().ToArray());
            if (cmb_Shr_Con_Section_name.Items.Count > 0)
            {
                cmb_Shr_Con_Section_name.SelectedItem = "ISMC";
                cmb_Shr_Con_Section_Code.SelectedItem = "150";
            }
            cmb_select_standard.SelectedIndex = 1;
            cmb_lac.SelectedIndex = 0;

            iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
        }

        private void cmb_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
            grb_bottom_plate.Enabled = true;
            grb_Top_plate.Enabled = true;
            grb_vertical_stiffener_plate.Enabled = true;

            if (cmb_sections_define.SelectedIndex == 12 || cmb_sections_define.SelectedIndex == 13)
            {
                grb_side_plate.Text = "WEB PLATE";
                grb_bottom_plate.Enabled = false;
                grb_Top_plate.Enabled = false;
                grb_vertical_stiffener_plate.Enabled = false;
            }
            else
            {
                grb_side_plate.Text = "SIDE PLATE";

            }
            SetComboSections();

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section11;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();

                    //cmb_section_name.Items.Add("ISLB");
                    //cmb_section_name.Items.Add("ISJB");
                    //cmb_section_name.Items.Add("ISMB");
                    //cmb_section_name.Items.Add("ISWB");
                    //cmb_section_name.Items.Add("ISHB");

                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    cmb_code1.Text = "600";
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "500";
                    txt_tp_thk.Text = "22";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 1:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "400";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISJC");
                    //cmb_section_name.Items.Add("ISLC");
                    //cmb_section_name.Items.Add("ISMC");


                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);


                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "320";
                    txt_sp_thk.Text = "10";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 2:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");

                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "350";
                    txt_tp_thk.Text = "25";
                    txt_sp_wd.Text = "420";
                    txt_sp_thk.Text = "16";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 3:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    txt_tp_width.Text = "350";
                    txt_tp_thk.Text = "25";
                    txt_sp_wd.Text = "420";
                    txt_sp_thk.Text = "16";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_vsp_wd.Text = "120";
                    txt_vsp_thk.Text = "25";
                    break;
                case 4:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "600";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISLB");
                    //cmb_section_name.Items.Add("ISJB");
                    //cmb_section_name.Items.Add("ISMB");
                    //cmb_section_name.Items.Add("ISWB");
                    //cmb_section_name.Items.Add("ISHB");
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);


                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "1";

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
                    //cmb_section_name.Items.Add("ISLB");
                    //cmb_section_name.Items.Add("ISJB");
                    //cmb_section_name.Items.Add("ISMB");
                    //cmb_section_name.Items.Add("ISWB");
                    //cmb_section_name.Items.Add("ISHB");
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "1";


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
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "4";


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
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "1";


                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 8:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section9;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "10";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 9:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section10;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "400";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISJC");
                    //cmb_section_name.Items.Add("ISLC");
                    //cmb_section_name.Items.Add("ISMC");
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "320";
                    txt_sp_thk.Text = "10";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 10:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section111;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "110";
                    cmb_sec_thk.Text = "10";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 11:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section12;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "110";
                    cmb_sec_thk.Text = "10";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                //Chiranjit [2011 05 17]
                case 12:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section13;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.Text = "12";
                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "30";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                //Chiranjit [2011 05 18]
                case 13:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section14;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                   
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.Text = "12";
                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "320";
                    txt_tp_thk.Text = "16";
                    txt_bp_wd.Text = "320";
                    txt_bp_thk.Text = "16";
                    txt_sp_wd.Text = "700";
                    txt_sp_thk.Text = "16";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                //Chiranjit [2011 05 18]
                case 14:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISMC");
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    
                    cmb_code1.SelectedItem = "300";
                    cmb_sec_thk.Text = "0";
                    txt_no_ele.Text = "4";

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
            cmb_code1.Width = cmb_sec_thk.Visible ? 93 : 144;

            //SetComboSections();
        }

        private void tab_page_Enter(object sender, EventArgs e)
        {
            TabPage tab_p = sender as TabPage;

            grb_def_sec.Enabled = true;

            if (tab_p == null) return;
            if (tab_p.Name == tab_GD.Name)
            {
                if (IsWarren2)
                    pcb_images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Warren2;
                else
                    pcb_images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Warren1;
                pcb_images.Visible = true;
                grb_def_sec.Visible = false;
                dgv_SIDL.Visible = true;
            }
            else
            {
                if (rbtn_rail_bridge.Checked)
                {
                    dgv_SIDL.Visible = false;
                    //dgv_SIDL.Rows.Clear();
                }

                grb_def_sec.Visible = true;
                pcb_images.Visible = false;
                //dgv_SIDL.Rows.Clear();
                //if (rbtn_road_bridge.Checked && dgv_SIDL.Rows.Count <= 1)
                //    SetDefaultDeadLoad();
            }
        }
        private void cmb_sec_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
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
                case 9:
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
                case 8:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
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
            timer1.Start();
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


            if (cmb_section_name.Text == "ISMC")
            {
                int le = MyList.StringToInt(cmb_code1.Text  ,0);
                switch (le)
                {
                    case 100:
                        txt_sp_wd.Text = "" + (le - 18 * 2);
                        break;
                    case 125:
                        txt_sp_wd.Text = "" + (le - 20 * 2);
                        break;
                    case 150:
                        txt_sp_wd.Text = "" + (le - 22 * 2);
                        break;
                    case 175:
                        txt_sp_wd.Text = "" + (le - 24 * 2);
                        break;
                    case 200:
                        txt_sp_wd.Text = "" + (le - 25 * 2);
                        break;
                    case 225:
                        txt_sp_wd.Text = "" + (le - 28 * 2);
                        break;
                    case 250:
                        txt_sp_wd.Text = "" + (le - 30 * 2);
                        break;
                    case 300:
                        txt_sp_wd.Text = "" + (le - 30 * 2);
                        break;
                    case 350:
                        txt_sp_wd.Text = "" + (le - 32 * 2);
                        break;
                    case 400:
                        txt_sp_wd.Text = "" + (le - 35 * 2);
                        break;
                }
            }

        }

        private void dgv_mem_details_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Stop();
            btn_add_to_list.ForeColor = Color.Black;

            int indx = e.RowIndex;
            //Select_Member_Group(e.RowIndex);
            //Select_Member_Group(dgv_mem_details[e.ColumnIndex, e.RowIndex].Value.ToString());
            if ((sender as DataGridView).Name == dgv_member_Result.Name)
            {

                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    if (dgv_mem_details[0, i].Value.ToString().ToUpper() == (dgv_member_Result[0,e.RowIndex].Value.ToString().ToUpper()))
                    {
                        dgv_mem_details.Rows[i].Selected = true;
                        dgv_mem_details.FirstDisplayedScrollingRowIndex = i;

                        indx = i;
                        break;
                    }
                }
            }
            Select_Member_Group(indx);
        }



        private void btn_cd_upd_mem_Click(object sender, EventArgs e)
        {
            dgv_mem_details.Rows.Clear();
            Complete_Design.Members.Clear();
        }
        private void btn_add_to_list_Click(object sender, EventArgs e)
        {
            try
            {
                #region Convert Data
                SectionData secData = null;
                if (cmb_convert_standard.SelectedIndex == 0)
                {
                    for (int i = 0; i < complete_design.Members.Count; i++)
                    {
                        secData = complete_design.Members[i].SectionDetails;

                        if (secData.SectionName.StartsWith("IS"))
                        {
                            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref secData);
                        }
                        AddMemberRow(complete_design.Members[i]);
                    }
                }
                else if (cmb_convert_standard.SelectedIndex == 1)
                {
                    for (int i = 0; i < complete_design.Members.Count; i++)
                    {
                        secData = complete_design.Members[i].SectionDetails;

                        if (secData.SectionName.StartsWith("UK"))
                        {
                            iApp.Tables.Steel_Convert.Convert_BS_to_IS(ref secData);
                        }
                        AddMemberRow(complete_design.Members[i]);
                    }
                }
                #endregion Convert Data
            }
            catch (Exception ex) { }


            try
            {
                string str = MyList.RemoveAllSpaces(txt_cd_mem_no.Text).ToUpper();
                CMember m = null;
                str = str.Replace(',', ' ');
                str = MyList.RemoveAllSpaces(str);
                MyList mList = new MyList(str, ' ');

                m = GetMemberData();
                int indx = complete_design.Members.IndexOf(m);
                if (indx != -1)
                {
                    //dgv_mem_details.Rows.RemoveAt(dgv_mem_details.CurrentRow.Index);
                    //DeleteMember(m.Group.GroupName);
                    complete_design.Members[indx] = m;
                }
                else
                    complete_design.Members.Add(m);

                AddMemberRow(m);
                Show_Total_Weight();
                //MessageBox.Show(m.Group.GroupName + " updated.", "ASTRA");
                timer1.Stop();
                MessageBox.Show(this, "All Member Sections are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Show_Total_Weight();
            }


        }


        private void cmb_cd_mem_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grb_Shr_Con.Visible = (((eMemberType)cmb_cd_mem_type.SelectedIndex) == eMemberType.CrossGirder);
        }
        private void btn_open_load_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    SetCompleteDesign(ofd.FileName);
                    txt_member_load_file.Text = ofd.FileName;
                }
            }
            Format_SIDL();
        }
        private void btn_cd_open_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(txt_analysis_file.Text);
            }
            catch (Exception ex) { }
            
        }


        private void btn_Memeber_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int row_indx = dgv_mem_details.CurrentCell.RowIndex;
                string memGrp = dgv_mem_details[0, row_indx].Value.ToString();
                DeleteMember(memGrp);
            }
            catch (Exception ex)
            {
            }
        }


        private void dgv_SIDL_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Format_SIDL();

        }



        private void cmb_mem_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string mem_name = cmb_mem_group.SelectedItem.ToString();
                ShowMemberNos(mem_name);
                cmb_cd_mem_type.SelectedIndex = (int)Get_MemberType(cmb_mem_group.SelectedItem.ToString());
                Set_Sections(mem_name);

                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    dgv_mem_details.Rows[i].Selected = false;
                }
                
                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    if (dgv_mem_details[0, i].Value.ToString().ToUpper() == (cmb_mem_group.SelectedItem.ToString().ToUpper()))
                    {
                        dgv_mem_details.Rows[i].Selected = true;
                        //dgv_mem_details.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex) { }

        }



        private void btn_browse_analysis_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    OpenAnalysisFile(ofd.FileName);
                }
            }

            Button_Enable_Disable();
        }
        

        private void btn_open_analysis_report_Click(object sender, EventArgs e)
        {
            string flPath = txt_analysis_file.Text;
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (prs.Start())
                prs.WaitForExit();


            
            string ana_rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            if (File.Exists(ana_rep_file))
                Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file );
           


            if (cmb_mem_group.Items.Count > 0)
                cmb_mem_group.SelectedIndex = 0;
            if (dgv_mem_details.RowCount == 0)
            {
                FillMemberGroup();
            }

            string kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
            if (File.Exists(kFile))
            {
                SetCompleteDesign(kFile);
                ReadResult();
            }
            dgv_mem_details.Rows.Clear();

            for (int i = 0; i < Complete_Design.Members.Count; i++)
            {
                CMember member = Complete_Design.Members[i];
                member.Force = Truss_Analysis.GetForce(ref member);
                AddMemberRow(member);
            }
            FillMemberResult();
            Button_Enable_Disable();
        }
        private void btn_remove_all_Click(object sender, EventArgs e)
        {
            dgv_SIDL.Rows.Clear();
            complete_design.DeadLoads = new TotalDeadLoad();
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                //complete_design.DeadLoads.Load_List.RemoveAt(dgv_SIDL.CurrentCell.RowIndex);
                dgv_SIDL.Rows.RemoveAt(dgv_SIDL.CurrentCell.RowIndex);

            }
            catch (Exception ex) { }
        }
        private void cmb_Shr_Con_Section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_Shr_Con_Section_Code.Items.Clear();

            cmb_Shr_Con_Section_Code.Items.AddRange(tbl_rolledSteelChannels.Get_SectionCodes(cmb_Shr_Con_Section_name.Text).ToArray());
            if (cmb_Shr_Con_Section_Code.Items.Count > 0)
                cmb_Shr_Con_Section_Code.SelectedIndex = 0;
        }
        private void btn_add_load_Click(object sender, EventArgs e)
        {
            if (LoadList == null)
            {
                LoadList = new List<LoadData>();
            }
            //LoadData ld = new LoadData();

            //ld.TypeNo = cmd_load_type.Text;
            //ld.X = MyList.StringToDouble(txt_X.Text, -60.0);
            //ld.Y = MyList.StringToDouble(txt_Y.Text, 0.0);
            //ld.Z = MyList.StringToDouble(txt_Z.Text, 1.0);
            //ld.XINC = MyList.StringToDouble(txt_XINCR.Text, 0.5);

            Add_LiveLoad();
        }


        
        
        private void btn_write_load_Click(object sender, EventArgs e)
        {
            string file_name = txt_analysis_file.Text;

            //file_name = Path.Combine(user_path, "LL.TXT");
            //if (!File.Exists(file_name))
            //{
            //    MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
            //    return;
            //}


            file_name = txt_analysis_file.Text;


            if (!File.Exists(file_name)) return;

            string load_file = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");

            if (!SaveMemberLoads(load_file)) return;

            load_file = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");

            if (!SaveMemberLoads(load_file)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                //if (kStr.Contains("LOAD GEN"))
                //    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = "DL ";

            s = chk_DL.Checked ? "DL " : "";
            s += chk_SIDL.Checked ? chk_DL.Checked ? " + SIDL " : "SIDL" : "";
            s += chk_LL.Checked ? (chk_DL.Checked || chk_SIDL.Checked) ? " + LL " : "LL" : "";



            //if (complete_design.DeadLoads.Weight > 0)
            //{
            //    s = s + "+ SIDL";
            //}
            //s = s + "+ LL";
            load_lst.Add("LOAD	1	" + s);
            load_lst.Add("JOINT	LOAD");
            List<string> lst = Truss_Analysis.Analysis.Joints.Get_Joints_Load_as_String(complete_design.ForceEachInsideJoints, complete_design.ForceEachEndJoint);

            load_lst.AddRange(lst.ToArray());
            //load_lst.Add("1	11	12	22	FY	-49.831	");
            //load_lst.Add("2	TO	10	13	TO	21	FY	-99.661");
            //load_lst.Add("1	11	12	22	FY	-49.831	");

            //if (isMoving_load && dgv_live_load.RowCount != 0)
            if (dgv_live_load.RowCount != 0)
            {
                if (!File.Exists(Path.Combine(user_path, "LL.TXT")))
                {
                    MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                    //return;
                }

                //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                ////load_lst.Add("**** 3 LANE CLASS A *****");
                //load_lst.Add("LOAD GENERATION 60");



                //Chiranjit [2011 03 28]
                //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");
                //load_lst.Add("LOAD GENERATION 100");
                //load_lst.Add("TYPE 7  -69.500 0 1.000 XINC 0.5");

                LoadReadFromGrid();
                //foreach (LoadData ld in LoadList)
                //{
                //    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                //    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                //}
            }
            if (complete_design.Is_Live_Load && Live_Load_List != null)
                load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());


            MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btn_open_analysis_file_Click(object sender, EventArgs e)
        {
            string kFile = Path.Combine(user_path, "Analysis_rep.txt");
            if (File.Exists(kFile))
                System.Diagnostics.Process.Start(kFile); ;

        }
        private void btn_open_member_load_Click(object sender, EventArgs e)
        {
            string kFile = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");
            if (File.Exists(kFile))
                System.Diagnostics.Process.Start(kFile); ;

        }

        private void dgv_member_Result_DoubleClick(object sender, EventArgs e)
        {
            dgv_mem_details.Focus();
        }
        private void dgv_mem_details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void cmb_lac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_lac.SelectedIndex == 0)
            {
                lbl_lac_wd.Text = "Width [bl]";
                lbl_lac_thk.Text = "Thickness [tl]";
                lbl_ISA.Visible = false;
                cmb_lac_tl.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_lac_bl.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_lac_tl.Items.Clear();
                cmb_lac_bl.Items.Clear();
                cmb_lac_tl.Text = "10";
                cmb_lac_bl.Text = "50";
            }
            else if (cmb_lac.SelectedIndex == 1)
            {

                cmb_lac_tl.Items.Clear();
                cmb_lac_bl.Items.Clear();
                Fill_Angles_in_Combobox(cmb_lac_bl);
                lbl_lac_wd.Text = "Select Angle";
                lbl_lac_thk.Text = "Thickness [tl]";
                lbl_ISA.Visible = true;
                cmb_lac_tl.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb_lac_bl.DropDownStyle = ComboBoxStyle.DropDownList;
                if (cmb_lac_bl.Items.Count > 0) cmb_lac_bl.SelectedIndex = 0;
            }
        }
        private void cmb_lac_bl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sec_code, sec_name, code2;
            double thk = 0.0;
            cmb_lac_tl.Items.Clear();
            for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
            {
                sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                thk = tbl_rolledSteelAngles.List_Table[i].Thickness;
                if (sec_name == "ISA" && sec_code == cmb_lac_bl.Text)
                {
                    if (cmb_lac_tl.Items.Contains(thk) == false)
                    {
                        cmb_lac_tl.Items.Add(thk);
                    }
                }
            }
            if (cmb_lac_tl.Items.Count > 0)
            {
                cmb_lac_tl.SelectedIndex = 0;
            }
        }
        private void txt_weld_size_TextChanged(object sender, EventArgs e)
        {
            lbl_throat_size.Text = "= 0.7 * " + txt_weld_size.Text;
            txt_throat_size.Text = (0.7 * MyList.StringToDouble(txt_weld_size.Text, 0.0)).ToString("0.00");
            
        }
        private void rbtn_bolt_CheckedChanged(object sender, EventArgs e)
        {
            grb_bolt.Enabled = rbtn_bolt.Checked;
            grb_weld.Enabled = rbtn_weld.Checked;
        }
        private void dgv_mem_details_SelectionChanged(object sender, EventArgs e)
        {
            //Select_Member_Group(dgv_mem_details.CurrentCell.RowIndex);

            //cmb_mem_group.SelectedItem = dgv_mem_details[0,dgv_mem_details.CurrentCell.RowIndex].Value.ToString();
        }
        private void cmb_mem_group_DropDownClosed(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_mem_details.RowCount; i++)
            {
                if (dgv_mem_details[0, i].Value.ToString().Contains(cmb_mem_group.SelectedItem.ToString()))
                {
                    dgv_mem_details.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }
        private void cmb_lac_tl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_weld_size.Text = (MyList.StringToDouble(cmb_lac_tl.Text, 3.0) - 1.6).ToString();
        }
        private void rbtn_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            grb_create_analysis.Enabled = rbtn_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;
            
            grb_create_analysis.Enabled = !rbtn_select_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_select_analysis_file.Checked;

            if (rbtn_create_analysis_file.Checked)
            {
                //if (!Directory.Exists(user_path))
                //{
                //    SetWorkingFolder();
                //}
                if (IsWarren2)
                {
                    txt_B.Text = "9.1";
                    txt_L.Text = "50.0";
                    txt_H.Text = "6.5";
                }
                else
                {
                    txt_B.Text = "8.43";
                    txt_L.Text = "60.0";
                    txt_H.Text = "6.35";
                }
            }
            
            
            
        }
        private void btn_Create_Data_Click(object sender, EventArgs e)
        {
            user_path = Path.Combine(user_path, Title);

            if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);
            //Warren 1
            string input_file = Path.Combine(user_path, "INPUT_Data.txt");

            //using (SaveFileDialog sfd = new SaveFileDialog())
            //{
            //    sfd.Filter = "Text Files (*.txt)|*.txt";
            //    if (sfd.ShowDialog() != DialogResult.Cancel)
            //    {
            //        input_file = sfd.FileName;
            //        FilePath = Path.GetDirectoryName(sfd.FileName);
            //    }
            //}
            FilePath = Path.GetDirectoryName(input_file);

            if (IsWarren2 == false)
            {
                CreateSteel_Warren_1_TrussData truss_data = new CreateSteel_Warren_1_TrussData();

                truss_data.Length = MyList.StringToDouble(txt_L.Text, 50.0);
                truss_data.Height = MyList.StringToDouble(txt_H.Text, 6.0);
                truss_data.Breadth = MyList.StringToDouble(txt_B.Text, 5.4);
                if (truss_data.CreateData(input_file))
                {
                    iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(input_file), false, true);
                    string src_file = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Steel Truss Warren 1\MEMBER_LOAD_DATA.txt");
                    string des_file = Path.Combine(Path.GetDirectoryName(input_file), @"MEMBER_LOAD_DATA.txt");

                    if (File.Exists(src_file))
                        File.Copy(src_file, des_file, true);

                    rbtn_custom_LL.Checked = true;
                    //MessageBox.Show(this, "Analysis Input Data Created in file " + input_file);
                    MessageBox.Show(this, "Analysis Input Data Created in file " + input_file);
                }
            }
            //Warren 2
            else
            {
                CreateSteel_Warren_2_TrussData truss_data2 = new CreateSteel_Warren_2_TrussData();

                truss_data2.Length = MyList.StringToDouble(txt_L.Text, 50.0);
                truss_data2.Height = MyList.StringToDouble(txt_H.Text, 6.5);
                truss_data2.Breadth = MyList.StringToDouble(txt_B.Text, 9.1);
                if (truss_data2.CreateData(input_file))
                {
                    iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(input_file), false, true);
                    string src_file = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Steel Truss Warren 2\MEMBER_LOAD_DATA.txt");
                    string des_file = Path.Combine(Path.GetDirectoryName(input_file), @"MEMBER_LOAD_DATA.txt");

                    if (File.Exists(src_file))
                        File.Copy(src_file, des_file, true);

                    rbtn_custom_LL.Checked = true;
                    chk_SIDL.Checked = false;
                    MessageBox.Show(this, "Analysis Input Data Created in file " + input_file);
                }

                //txt_analysis_file.Text = input_file;
            }
            OpenAnalysisFile(input_file);

        }
        private void btn_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_live_load.Rows.RemoveAt(dgv_live_load.CurrentRow.Index);
            }
            catch (Exception ex) { }
        }
        private void btn_live_load_remove_all_Click(object sender, EventArgs e)
        {
            dgv_live_load.Rows.Clear();

        }
        private void btn_View_Structure_Click(object sender, EventArgs e)
        {
            if (File.Exists(txt_analysis_file.Text))
                iApp.OpenWork(txt_analysis_file.Text, false);
        }
        private void rbtn_LL_fill_data_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            if (rbtn.Name == rbtn_custom_LL.Name && rbtn.Checked)
            {
                grb_load.Enabled = true;
                grb_custom.Enabled = false;
            }
            else if (rbtn.Name == rbtn_LL_fill_data.Name && rbtn.Checked)
            {
                
                grb_load.Enabled = false;
                grb_custom.Enabled = true;
                cmb_custom_LL_type.Items.Clear();
                for (int i = 0; i < Live_Load_List.Count; i++)
                {
                    cmb_custom_LL_type.Items.Add(Live_Load_List[i].TypeNo + " : " + Live_Load_List[i].Code);
                }
                if (cmb_custom_LL_type.Items.Count > 0)
                {
                    cmb_custom_LL_type.SelectedIndex = 0;
                    cmb_custom_LL_lanes.Text = "2";
                }
                txt_custom_LL_Xcrmt.Text = "";
                txt_custom_LL_Xcrmt.Text = "0.5";
            }

        }
        private void btn_Show_MovingLoad_Click(object sender, EventArgs e)
        {
            txt_LL_Input_data.Lines = Get_MovingLoad_Data(Live_Load_List);
        }
        private void txt_custom_LL_Xcrmt_TextChanged(object sender, EventArgs e)
        {
            txt_custom_LL_load_gen.Text = ((int)(Truss_Analysis.Analysis.Length / MyList.StringToDouble(txt_custom_LL_Xcrmt.Text, 0.0))).ToString();
        }
        private void btn_open_UG_Click(object sender, EventArgs e)
        {
            string ug_path = Path.Combine(Application.StartupPath, "ASTRAHelp\\SteelTrussBridgeUsersGuide.pdf");

            if (File.Exists(ug_path))
            {
                System.Diagnostics.Process.Start(ug_path);
            }
        }
        private void txt_steel_structure_weight_TextChanged(object sender, EventArgs e)
        {

        }


        private void txt_cd_force_percent_TextChanged(object sender, EventArgs e)
        {
            Show_Total_Weight();
        }
        private void cmb_custom_LL_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion Form Events 

        private void label123_DoubleClick(object sender, EventArgs e)
        {
            chk_chng_mode.Visible = !chk_chng_mode.Visible;
        }

        private void dgv_member_Result_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmp = Path.Combine(user_path,"mem.txt");

            DataGridView dgv = sender as DataGridView; 
            try
            {
                string mem_grp = dgv[0, e.RowIndex].Value.ToString();

                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    if (Complete_Design.Members[i].Group.GroupName == mem_grp)
                    {
                        if (Complete_Design.Members[i].DesignReport.Count > 0)
                        {
                            File.WriteAllLines(tmp, Complete_Design.Members[i].DesignReport.ToArray());
                            //iApp.View_Result(tmp);
                            iApp.RunExe(tmp);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void cmb_select_standard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region Convert Data
                //SectionData secData = null;
                //if (cmb_convert_standard.SelectedIndex == 0)
                //{
                //    for (int i = 0; i < complete_design.Members.Count; i++)
                //    {
                //        secData = complete_design.Members[i].SectionDetails;

                //        if (secData.SectionName.StartsWith("IS"))
                //        {
                //            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref secData);
                //        }
                //        //AddMemberRow(complete_design.Members[i]);
                //    }
                //}
                //else if (cmb_convert_standard.SelectedIndex == 1)
                //{
                //    for (int i = 0; i < complete_design.Members.Count; i++)
                //    {
                //        secData = complete_design.Members[i].SectionDetails;

                //        if (secData.SectionName.StartsWith("UK"))
                //        {
                //            iApp.Tables.Steel_Convert.Convert_BS_to_IS(ref secData);
                //        }
                //        //AddMemberRow(complete_design.Members[i]);
                //    }
                //}
                #endregion Convert Data
                Set_Sections_Standard();
            }
            catch (Exception ex) { }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 300;
            btn_add_to_list.ForeColor = (btn_add_to_list.ForeColor == Color.Red) ? Color.Black : Color.Red;
            //btn_add_to_list.BackColor = (btn_add_to_list.BackColor == Color.Black) ? Color.Red : Color.Black;
        
        }

        private void dgv_live_load_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgv_live_load.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv_live_load.ColumnCount; j++)
                    {
                        try
                        {
                            dgv_live_load[j, i].Value = MyList.RemoveAllSpaces(dgv_live_load[j, i].Value.ToString());
                        }
                        catch (Exception ex) { }
                    }

                }
            }
            catch (Exception ex) { }
        }

        private void cmb_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                    txt_X.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].Distance.ToString("f4");
            }
            catch (Exception ex) { }
        }
    }


}
// Chiranjit [2011 10 21] Moment of Inertia formula is wrong
//Sandiapan Goswami [2011 10 26] Calculate Moment of Inertia, Kolkata
