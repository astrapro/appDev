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
using AstraFunctionOne.BridgeDesign.Piers;

using AstraFunctionOne.BridgeDesign.SteelTruss;


using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;


namespace LimitStateMethod.Steel_Truss
{
    public partial class frm_Arch_Cable_Bridge : Form
    {
        bool isCreateData = true;

        bool IsWarren2 = false;
        IApplication iApp = null;



        //Chiranjit [2012 06 08]
        RccPier rcc_pier = null;

        //Chiranjit [2012 05 27]
        RCC_AbutmentWall Abut = null;

        SteelTrussDeckSlab Deck = null;

        //TableRolledSteelBeams tbl_rolledSteelBeams = null;
        //TableRolledSteelChannels tbl_rolledSteelChannels = null;
        //TableRolledSteelAngles tbl_rolledSteelAngles = null;

        BridgeMemberAnalysis Truss_Analysis = null;
        
        List<BridgeMemberAnalysis> List_Analysis = null;


        CompleteDesign complete_design = null;
        List<LoadData> LoadList = null;
        List<LoadData> Live_Load_List = null;
        //Steel_Truss_K_Type_Data truss_data = null;
        Arch_Cable_Suspension_Data truss_data = null;

        CreateSteel_Warren_2_TrussData truss_data2 = null;

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
        string user_path
        {
            get
            {
                return iApp.user_path;
            }
            set
            {
                iApp.user_path = value;
            }
        }
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";
        bool is_process = false;
        #endregion

        #region Chiranjit [2014 03 12] Support Input
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
        #endregion Chiranjit [2014 03 12] Support Input

        //Chiranjit [2012 07 14]
        public string ASTRA_DATA_FILE
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "ASTRA_DATA_FILE.TXT");
                return "";
            }
        }

        //Chiranjit [2014 01 23]
        public string Get_User_Input_File_Name(int indx)
        {
            string str = "";
            string dir = "";
            if (Directory.Exists(user_path))
            {
                //switch (indx)
                //{
                //    case 0:
                //        dir = Path.Combine(user_path, "Main_Analysis");
                //        str = "Main_Analysis.txt";
                //        break;
                //    case 1:
                //        dir = Path.Combine(user_path, "Analysis_Removing_Left_Inner_Cables");
                //        str = "Analysis_Removing_Left_Inner_Cables.txt";
                //        break;
                //    case 2:
                //        dir = Path.Combine(user_path, "Analysis_Removing_Left_Outer_Cables");
                //        str = "Analysis_Removing_Left_Outer_Cables.txt";
                //        break;
                //    case 3:
                //        dir = Path.Combine(user_path, "Analysis_Removing_Both_Inner_Cables");
                //        str = "Analysis_Removing_Both_Inner_Cables.txt";
                //        break;
                //    case 4:
                //        dir = Path.Combine(user_path, "Analysis_Removing_Both_Outer_Cables");
                //        str = "Analysis_Removing_Both_Outer_Cables.txt";
                //        break;
                //}


                switch (indx)
                {
                    case 0:
                        dir = Path.Combine(user_path, "Main_Analysis");
                        str = "Main_Analysis.txt";
                        break;
                    case 1:
                        dir = Path.Combine(user_path, "Analysis_Removing_One_Side_Inner_Cables");
                        str = "Analysis_Removing_One_Side_Inner_Cables.txt";
                        break;
                    case 2:
                        dir = Path.Combine(user_path, "Analysis_Removing_One_Side_Outer_Cables");
                        str = "Analysis_Removing_One_Side_Outer_Cables.txt";
                        break;
                    case 3:
                        dir = Path.Combine(user_path, "Analysis_Removing_Both_Side_Inner_Cables");
                        str = "Analysis_Removing_Both_Side_Inner_Cables.txt";
                        break;
                    case 4:
                        dir = Path.Combine(user_path, "Analysis_Removing_Both_Side_Outer_Cables");
                        str = "Analysis_Removing_Both_Side_Outer_Cables.txt";
                        break;
                }
            }

            if (dir == "") return "";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Path.Combine(dir, str);
        }

        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
            }
        }

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
        public frm_Arch_Cable_Bridge(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
            user_path = app.LastDesignWorkingFolder;
            MembersDesign complete_design = new MembersDesign();
            IsWarren2 = false;
        }
        public frm_Arch_Cable_Bridge(IApplication app, bool isWarren2)
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

                    cmb_mem_group.SelectedItem = Complete_Design.Members[indx].Group.GroupName;
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


                txt_tp_width.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Width.ToString("0");
                txt_tp_thk.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Thickness.ToString("0");
                txt_bp_wd.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Width.ToString("0");
                txt_bp_thk.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Thickness.ToString("0");

                txt_sp_wd.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Width.ToString("0");
                txt_sp_thk.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Thickness.ToString("0");
                txt_vsp_wd.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Width.ToString("0");
                txt_vsp_thk.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Thickness.ToString("0");

                txt_cab_dia.Text = Complete_Design.Members[indx].SectionDetails.Cables_Strands.Diameter.ToString("0");
                txt_cab_thk.Text = Complete_Design.Members[indx].SectionDetails.Cables_Strands.Thickness.ToString("0");


                txt_sec_lat_spac.Text = Complete_Design.Members[indx].SectionDetails.LateralSpacing.ToString();
                txt_sec_bolt_dia.Text = Complete_Design.Members[indx].SectionDetails.BoltDia.ToString();
                txt_sec_nb.Text = Complete_Design.Members[indx].SectionDetails.NoOfBolts.ToString();
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
                    //if (complete_design.Members.Count == 0)
                    //{
                        complete_design = new CompleteDesign();
                        complete_design.ReadFromFile(file_name);

                        foreach (var item in complete_design.Members)
                        {
                            item.Calculate_Area_Properties(iApp);

                        }
                    //}
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
        public delegate void SetProgressValue(ProgressBar pbr, int val);

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
                sw.WriteLine("w={0}", txt_cgw.Text);
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
                design_member = (eMemberType)(cmb_design_member.SelectedIndex);
            }
            else
                design_member = eMemberType.NoSelection;
        }
        private void Calculate_Program()
        {
            string file_path = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            //Truss_Analysis = new SteelTrussAnalysis(file_path, pbar);


            if (Truss_Analysis == null)
                Truss_Analysis = new BridgeMemberAnalysis(iApp, file_path);

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
                sw.WriteLine("\t\t*                 ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*        DESIGN OF STEEL TRUSS BRIDGE        *");
                sw.WriteLine("\t\t*          COMPLETE MEMBER DESIGN            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                sw.WriteLine();
                sw.WriteLine("-------------------------");
                sw.WriteLine("USER'S GENERAL INPUT DATA ");
                sw.WriteLine("-------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Length / Span of Bridge [L] = {0} m", txt_L.Text);
                sw.WriteLine("Width of Bridge [B] = {0} m", txt_B.Text);
                sw.WriteLine("Length of each Panel [l] = {0} m", txt_Panel_Length.Text);
                sw.WriteLine("No of Panels = {0} ", txt_gd_np.Text);
                sw.WriteLine("Height of Bridge [h] = {0} m", txt_H.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} N/sq.mm", txt_gd_fy.Text);
                sw.WriteLine("Length of Span [l] = {0} kN/m", txt_L.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} kN/m", txt_gd_fy.Text);
                sw.WriteLine("Permissible stress in Axial comppression [fc] = {0} N/sq.mm", txt_fc.Text);
                sw.WriteLine("Permissible Tensile stress [ft] = {0} N/sq.mm", txt_gd_ft.Text);
                sw.WriteLine("Permissible Bending stress in steel [σ_b] = {0} N/sq.mm", txt_sigma_b.Text);
                sw.WriteLine("Permissible shear stress in steel [σ_c] = {0} N/sq.mm", txt_sigma_c.Text);
                sw.WriteLine();

                //Chiranjit [2013 08 07] Add Weight calculation into Design Report
                Complete_Design.ToStream(sw);

                sw.WriteLine();
                sw.WriteLine();
                int v = 0;
                iApp.Progress_ON("Design Members....");

                int step = 0;
                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    iApp.SetProgressValue(i, Complete_Design.Members.Count);
                    if (iApp.Is_Progress_Cancel)
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        break;
                    }
                    if (design_member == eMemberType.AllMember)
                    {
                        CMember mem = Complete_Design.Members[i];
                        TotalAnalysis(sw, ref mem, (++step).ToString());
                    }
                    else
                    {
                        if (design_member == Complete_Design.Members[i].MemberType)
                        {
                            CMember mem = Complete_Design.Members[i];
                            TotalAnalysis(sw, ref mem, (++step).ToString());
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
                    //v = (int)(((double)(i + 1) / (double)Complete_Design.Members.Count) * 100.0);
                    //pbar.Invoke(spv, pbar, v);
                }




                string file_load_def = Path.Combine(Analysis_Path, "MAX_LOAD_DEFLECTION.TXT");
                //File.WriteAllLines(file_load_def, list_node.ToArray());
                if (File.Exists(file_load_def))
                {
                    List<string> list_node = new List<string>(File.ReadAllLines(file_load_def));

                    #region CHECK FOR LIVE LOAD DEFLECTION

                    sw.WriteLine("");
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : CHECK FOR LIVE LOAD DEFLECTION", ++step);
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                    sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                    sw.WriteLine("");


                    string Node_Displacement_Data_DL = list_node[0];
                    NodeResultData Max_Node_Displacement = NodeResultData.Parse(Node_Displacement_Data_DL);
                    sw.WriteLine("");
                    sw.WriteLine(Max_Node_Displacement.ToString());
                    sw.WriteLine("");

                    sw.WriteLine("");
                    //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                    sw.WriteLine("");
                    double val = Truss_Analysis.Analysis.Length / 800.0;
                    sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", Truss_Analysis.Analysis.Length, val);
                    sw.WriteLine("");
                    if (Max_Node_Displacement.Max_Translation < val)
                        sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. < {1:f5} M.    OK.", Max_Node_Displacement.Max_Translation, val);
                    else
                    {
                        sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M. ", Max_Node_Displacement.Max_Translation, val);
                        //sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                    }
                   
                    #endregion CHECK FOR LIVE LOAD DEFLECTION

                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();

                    #region CHECK FOR DEAD LOAD DEFLECTION

                    string kStr = Truss_Analysis.Analysis.Supports[0].NodeNo + " TO "
                                            + Truss_Analysis.Analysis.Supports[3].NodeNo;


                    List<int> jnts = MyList.Get_Array_Intiger(kStr);


                    List<NodeResultData> dead_load_results = new List<NodeResultData>();

                    sw.WriteLine("");
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : CHECK FOR DEAD LOAD DEFLECTION", ++step);
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                    sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                    sw.WriteLine("");


                    for (int i = 1; i < list_node.Count; i++)
                    {
                        dead_load_results.Add(NodeResultData.Parse(list_node[i]));
                        sw.WriteLine(list_node[i]);
                    }


                    sw.WriteLine("");
                    //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                    sw.WriteLine("");


                    val = Truss_Analysis.Analysis.Length / 800.0;
                    sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", Truss_Analysis.Analysis.Length, val);
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("-----------------------------------------------------------------------------");
                    sw.WriteLine("MAXIMUM NODE DISPLACEMENTS FOR RIGHT SIDE BOTTOM CHORD");
                    sw.WriteLine("-----------------------------------------------------------------------------");

                    for (int i = 0; i < dead_load_results.Count; i++)
                    {


                        var item = dead_load_results[i];

                        if (i == (dead_load_results.Count / 2))
                        {
                            sw.WriteLine("");
                            sw.WriteLine("");
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("MAXIMUM NODE DISPLACEMENTS FOR LEFT SIDE BOTTOM CHORD");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                        }


                        if (item.Max_Translation < val)
                            sw.WriteLine("DISPLACEMENT AT NODE {0}  = {1:f5} M. < {2:f5} M.    OK.", item.NodeNo, item.Max_Translation, val);
                        else
                        {
                            sw.WriteLine("DISPLACEMENT AT NODE {0}  = {1:f5} M. > {2:f5} M. .", item.NodeNo, item.Max_Translation, val);
                            //sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M. ", Max_Node_Displacement.Max_Translation, val);
                            //sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                        }
                    }

                    sw.WriteLine();


                    #endregion CHECK FOR DEAD LOAD DEFLECTION

                    sw.WriteLine();
                    sw.WriteLine("To be adjusted by providing Longitudinal Camber.");
                    sw.WriteLine();
                    sw.WriteLine();
                }



                iApp.Progress_OFF();

                //WriteTable1(sw);
                //WriteTable2(sw);
                //WriteTable3(sw);
                //WriteTable4(sw);
                //Complete_Design.WriteForcesSummery(sw);
                Complete_Design.WriteForces_Capacity_Summery(sw);
                Complete_Design.WriteGroupSummery(sw);
                string file_ds_frc = "";
                file_ds_frc = Path.Combine(user_path, "DESIGN_SECTION_SUMMARY.TXT");
                Complete_Design.WriteGroupSummery(file_ds_frc);
                file_ds_frc = Path.Combine(user_path, "DESIGN_FORCES_SUMMARY.TXT");



                //Complete_Design.WriteForcesSummery(file_ds_frc);
                Complete_Design.WriteForces_Capacity_Summery(file_ds_frc);




                //Chiranjit [2013 08 16] Kolkata Write Tables at the end of the report
                WriteTable1(sw);
                WriteTable2(sw);
                WriteTable3(sw);
                WriteTable4(sw);
                WriteTable5(sw);
                WriteTable6(sw);
                #region End of Report
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion
                
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
        public string FilePath
        {
            set
            {
                user_path = value;
             
                this.Text = "DESIGN OF ARCH CABLE SUSPENSION BRIDGE : " + MyList.Get_Modified_Path(value);

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
                    //if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    //    Read_User_Input();
                }
            }
        }
        //public string Title
        //{
        //    get
        //    {
        //        //return "STEEL_TRUSS_OPEN_WEB_COMPLETE_DESIGN";
        //        //if (IsWarren2)
        //        //    return "ANALYSIS OF STEEL TRUSS BRIDGE WARREN 2";
        //        //else
        //        //    return "ANALYSIS OF STEEL TRUSS BRIDGE WARREN 1";

        //        return "ANALYSIS OF ARCH CABLE SUSPENSION BRIDGE";
        //    }
        //}
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "ARCH CABLE SUSPENSION BRIDGE [BS]";
                return "ARCH CABLE SUSPENSION BRIDGE [IRC]";
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

            CMember member = new CMember(iApp);
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

            if (member.SectionDetails.DefineSection == eDefineSection.Section8
                || member.SectionDetails.DefineSection == eDefineSection.Section11
                || member.SectionDetails.DefineSection == eDefineSection.Section12
                || member.SectionDetails.DefineSection == eDefineSection.Section13
                || member.SectionDetails.DefineSection == eDefineSection.Section14
                
                )
            {
                member.SectionDetails.SidePlate.TotalPlates = 1;
            }
            else
                member.SectionDetails.SidePlate.TotalPlates = 2;
            
            member.SectionDetails.VerticalStiffenerPlate.Width = MyList.StringToDouble(txt_vsp_wd.Text, 0.0);
            member.SectionDetails.VerticalStiffenerPlate.Thickness = MyList.StringToDouble(txt_vsp_thk.Text, 0.0);
            member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;


            if (member.SectionDetails.DefineSection == eDefineSection.Section8)
                member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 1;
            else
                member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;


            if (member.SectionDetails.DefineSection == eDefineSection.Section16 ||
                member.SectionDetails.DefineSection == eDefineSection.Section17)
            {
                member.SectionDetails.Cables_Strands.Diameter = MyList.StringToDouble(txt_cab_dia.Text, 0.0);
                member.SectionDetails.Cables_Strands.Thickness = MyList.StringToDouble(txt_cab_thk.Text, 0.0);
            }


            member.SectionDetails.LateralSpacing = MyList.StringToDouble(txt_sec_lat_spac.Text, 0.0);
            member.SectionDetails.BoltDia = MyList.StringToDouble(txt_sec_bolt_dia.Text, 0.0);
            member.SectionDetails.NoOfBolts = MyList.StringToInt(txt_sec_nb.Text, 0);


            member.WeightPerMetre = GetWeightPerMetre(member);

            return member;
        }
        public double GetWeightPerMetre(CMember m)
        {
            double wt_p_m = 0.0;
            try
            {
                if(m.SectionDetails.DefineSection == eDefineSection.Section16 ||
                    m.SectionDetails.DefineSection == eDefineSection.Section17)
                {
                    wt_p_m = m.SectionDetails.Cables_Strands.WeightPerMetre * 1000.0;
                    //wt_p_m = m.SectionDetails.Cables_Strands.WeightPerMetre;
                }
                else if (m.SectionDetails.SectionName.EndsWith("B"))
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
            }
            catch (Exception ex) { }
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

            //complete_design.NoOfJointsAtTrussFloor = MyList.StringToInt(txt_cd_total_joints.Text, 0);

            complete_design.NoOfJointsAtTrussFloor = Truss_Analysis.Analysis.Joints.Get_Total_Joints_At_Truss_Floor() + 4;
            complete_design.AddWeightPercent = MyList.StringToDouble(txt_cd_force_percent.Text, 0.0);

        }

        public void AddMemberRow(CMember member)
        {
            if (member.Group.GroupName == "") return;
            string kStr = "";
            try
            {
                //member.Force = Truss_Analysis.GetForce(ref member);
                //member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);


                member.Group.SetMemNos();
                member.WeightPerMetre = GetWeightPerMetre(member);
                bool flag = false;
                string mem_type = "";
                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    kStr = dgv_mem_details[0, i].Value.ToString();

                    if (member.SectionDetails.DefineSection == eDefineSection.Section17)
                    {
                        member.Group.GroupType = MemberGroup.eMType.CABLE;
                    }
                    else if (member.SectionDetails.DefineSection == eDefineSection.Section6 ||
                             member.SectionDetails.DefineSection == eDefineSection.Section5 ||
                             member.SectionDetails.DefineSection == eDefineSection.Section16)
                    {
                        member.Group.GroupType = MemberGroup.eMType.BEAM;
                    }
                    else
                    {
                        member.Group.GroupType = MemberGroup.eMType.TRUSS;
                    }


                    if (kStr == member.Group.GroupName)
                    {
                        dgv_mem_details[0, i].Value = member.Group.GroupName;
                        dgv_mem_details[1, i].Value = member.Group.GroupType.ToString();
                        dgv_mem_details[2, i].Value = (member.MemberType == eMemberType.NoSelection) ? "" : member.MemberType.ToString();
                        dgv_mem_details[3, i].Value = (member.SectionDetails.DefineSection == eDefineSection.NoSelection) ? "" : member.SectionDetails.DefineSection.ToString();
                        dgv_mem_details[4, i].Value = member.NoOfMember.ToString("0");
                        dgv_mem_details[5, i].Value = member.Length.ToString("0.000");
                        dgv_mem_details[6, i].Value = member.WeightPerMetre.ToString("0.0000");
                        dgv_mem_details[7, i].Value = member.Weight.ToString("0.0000");
                        dgv_mem_details[8, i].Value = member.Force;
                        return;
                    }
                }
                dgv_mem_details.Rows.Add(
                               member.Group.GroupName,
                               member.Group.GroupType.ToString(),
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

            #region For Arch Cables Chiranjit [2014 01 20]
            if (member.Group.GroupName.StartsWith("_TRANS"))
            {

                member.SectionDetails.DefineSection = eDefineSection.Section16;

                member.SectionDetails.Cables_Strands.Diameter = MyList.StringToDouble(txt_D2.Text, 1000.0);
                member.SectionDetails.Cables_Strands.Thickness = 12.0;

                member.SectionDetails.LateralSpacing = 0.0;
                member.SectionDetails.NoOfBolts = 2;
                member.SectionDetails.BoltDia = 20;
                member.SectionDetails.NoOfElements = 4.0;


            }
            else if (member.Group.GroupName.StartsWith("_AR"))
            {

                member.SectionDetails.DefineSection = eDefineSection.Section16;

                member.SectionDetails.Cables_Strands.Diameter = MyList.StringToDouble(txt_D1.Text, 1000.0);
                member.SectionDetails.Cables_Strands.Thickness = 16.0;

                member.SectionDetails.LateralSpacing = 0.0;
                member.SectionDetails.NoOfBolts = 2;
                member.SectionDetails.BoltDia = 20;
                member.SectionDetails.NoOfElements = 4.0;
            }
            else if (member.Group.GroupName.StartsWith("_IC") || member.Group.GroupName.StartsWith("_OC"))
            {

                member.SectionDetails.DefineSection = eDefineSection.Section17;

                member.SectionDetails.Cables_Strands.Diameter = MyList.StringToDouble(txt_ds.Text, 42.0);
                member.SectionDetails.Cables_Strands.Thickness = 0.0;

                member.SectionDetails.LateralSpacing = 0.0;
                member.SectionDetails.NoOfBolts = 2;
                member.SectionDetails.BoltDia = 20;
                member.SectionDetails.NoOfElements = 4.0;
            }
            else if (member.Group.GroupName.StartsWith("_STRIN"))
            {
                
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section5;
                    member.SectionDetails.SectionName = "ISMB";
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
                    //member.SectionDetails.BottomPlate.Width = 150.0;
                    //member.SectionDetails.BottomPlate.Thickness = 40.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;

            }
            else if (member.Group.GroupName.StartsWith("_XGIR"))
            {

                member.SectionDetails.NoOfElements = 1.0;
                member.SectionDetails.DefineSection = eDefineSection.Section6;
                member.SectionDetails.SectionName = "ISMB";
                member.SectionDetails.SectionCode = "400";
                //mem.SectionDetails.AngleThickness = 10.0d;

                member.SectionDetails.TopPlate.Width = 0;
                member.SectionDetails.TopPlate.Thickness = 0.0;

                member.SectionDetails.SidePlate.Width = 0.0;
                member.SectionDetails.SidePlate.Thickness = 0.0;


                member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                member.SectionDetails.BottomPlate.Width = 0.0;
                member.SectionDetails.BottomPlate.Thickness = 0.0;

                //member.SectionDetails.BottomPlate.Width = 350.0;
                //member.SectionDetails.BottomPlate.Thickness = 32.0;


                member.SectionDetails.LateralSpacing = 0.0;
                member.SectionDetails.NoOfBolts = 0;
                member.SectionDetails.BoltDia = 0;

            }
            else if (member.Group.GroupName.StartsWith("_BCB"))
            {
                member.SectionDetails.NoOfElements = 2.0;
                member.SectionDetails.DefineSection = eDefineSection.Section8;
                member.SectionDetails.SectionName = "ISA";
                member.SectionDetails.SectionCode = "200200";
                member.SectionDetails.AngleThickness = 12.0d;

                member.SectionDetails.TopPlate.Width = 0;
                member.SectionDetails.TopPlate.Thickness = 0.0;

                member.SectionDetails.SidePlate.Width = 0.0;
                member.SectionDetails.SidePlate.Thickness = 0.0;
                //member.SectionDetails.SidePlate.TotalPlates = 1;


                member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;
                //member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 1;

                member.SectionDetails.BottomPlate.Width = 0.0;
                member.SectionDetails.BottomPlate.Thickness = 0.0;


                member.SectionDetails.LateralSpacing = 0.0;
                member.SectionDetails.NoOfBolts = 0;
                member.SectionDetails.BoltDia = 0;

            }
            #endregion Chiranjit [2014 01 20]
            
            switch (member.Group.GroupName)
            {
                #region Bottom Chord
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

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;


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

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;


                    break;
                case "_L3L4":


                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 480.0;
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
                case "_L5L6":
                case "_L6L7":
                case "_L7L8":
                case "_L8L9":
                case "_L9L10":
                case "_L10L11":
                case "_L11L12":
                case "_L12L13":
                case "_L13L14":
                case "_L14L15":
                case "_L15L16":
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    break;
                #endregion Bottom Chord

                #region Top Chord

                case "_U1U2":


                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    break;
                case "_U2U3":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_U3U4":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.SidePlate.Width = 400.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_U4U5":
                case "_U5U6":
                case "_U6U7":
                case "_U7U8":
                case "_U8U9":
                case "_U9U10":
                case "_U10U11":
                case "_U11U12":
                case "_U12U13":
                case "_U13U14":
                case "_U14U15":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


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
                #endregion Top Chord

                #region Vertical

                case "_L1U1":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 240.0;
                    //member.SectionDetails.SidePlate.Thickness = 10.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0; //Chiranjit [2013 05 31] Kolkata


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
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 240.0;
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

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

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
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 240.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_L5U5":
                case "_L6U6":
                case "_L7U7":
                case "_L8U8":
                case "_L9U9":
                case "_L10U10":
                case "_L11U11":
                case "_L12U12":
                case "_L13U13":
                case "_L14U14":
                case "_L15U15":
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
                case "_V21V22":
                case "_V23V24":
                case "_V25V26":
                case "_V27V28":
                case "_V29V30":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 100.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 8.0;

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


                case "_L1M1":
                case "_L2M2":
                case "_L3M3":
                case "_L4M4":
                case "_L5M5":
                case "_L6M6":
                case "_L7M7":
                case "_L8M8":
                case "_L9M9":
                case "_M1L1":
                case "_M2L2":
                case "_M3L3":
                case "_M4L4":
                case "_M5L5":
                case "_M6L6":
                case "_M7L7":
                case "_M8L8":
                case "_M9L9":

                case "_U1M1":
                case "_U2M2":
                case "_U3M3":
                case "_U4M4":
                case "_U5M5":
                case "_U6M6":
                case "_U7M7":
                case "_U8M8":
                case "_U9M9":
                case "_M1U1":
                case "_M2U2":
                case "_M3U3":
                case "_M4U4":
                case "_M5U5":
                case "_M6U6":
                case "_M7U7":
                case "_M8U8":
                case "_M9U9":

                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 100.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 8.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    
                    break;
                #endregion Vertical

                #region End Racker


                case "_ER":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "200200";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 400.0;
                    member.SectionDetails.TopPlate.Thickness = 12.0;

                    member.SectionDetails.SidePlate.Width = 430.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 200.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 12.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    break;
                #endregion End Racker

                #region Diagonal


                case "_L2U1":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 350;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 120;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    break;
                //case "_L3U2":
                //    member.SectionDetails.NoOfElements = 2.0;
                //    member.SectionDetails.DefineSection = eDefineSection.Section2;
                //    member.SectionDetails.SectionName = "ISMC";
                //    member.SectionDetails.SectionCode = "300";
                //    //mem.SectionDetails.AngleThickness = 10.0d;

                //    member.SectionDetails.TopPlate.Width = 0;
                //    member.SectionDetails.TopPlate.Thickness = 0.0;

                //    member.SectionDetails.SidePlate.Width = 220.0;
                //    member.SectionDetails.SidePlate.Thickness = 12.0;


                //    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                //    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                //    member.SectionDetails.BottomPlate.Width = 0.0;
                //    member.SectionDetails.BottomPlate.Thickness = 0.0;


                //    member.SectionDetails.LateralSpacing = 400.0;
                //    member.SectionDetails.NoOfBolts = 2;
                //    member.SectionDetails.BoltDia = 20;


                //    break;
                //case "_L4U3":
                //member.SectionDetails.NoOfElements = 2.0;
                //member.SectionDetails.DefineSection = eDefineSection.Section2;
                //member.SectionDetails.SectionName = "ISMC";
                //member.SectionDetails.SectionCode = "300";
                ////mem.SectionDetails.AngleThickness = 10.0d;

                //member.SectionDetails.TopPlate.Width = 0;
                //member.SectionDetails.TopPlate.Thickness = 0.0;

                //member.SectionDetails.SidePlate.Width = 0.0;
                //member.SectionDetails.SidePlate.Thickness = 0.0;


                //member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                //member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                //member.SectionDetails.BottomPlate.Width = 0.0;
                //member.SectionDetails.BottomPlate.Thickness = 0.0;


                //member.SectionDetails.LateralSpacing = 400.0;
                //member.SectionDetails.NoOfBolts = 2;
                //member.SectionDetails.BoltDia = 20;


                //break;
                case "_L3U2":
                case "_L4U3":
                case "_L5U4":
                case "_L6U5":
                case "_L7U6":
                case "_L8U7":
                case "_L9U8":
                case "_L10U9":
                case "_L11U10":
                case "_L12U11":
                case "_L13U12":
                case "_L14U13":
                case "_L15U14":
                case "_L16U15":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 350;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 120;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    break;



                case "_M1U2":
                case "_M2U3":
                case "_M3U4":
                case "_M4U5":
                case "_M5U6":
                case "_M6U7":
                case "_M7U8":
                case "_M8U9":
                case "_M9U10":

                case "_U2M1":
                case "_U3M2":
                case "_U4M3":
                case "_U5M4":
                case "_U6M5":
                case "_U7M6":
                case "_U8M7":
                case "_U9M8":
                case "_U10M9":



                case "_M1L2":
                case "_M2L3":
                case "_M3L4":
                case "_M4L5":
                case "_M5L6":
                case "_M6L7":
                case "_M7L8":
                case "_M8L9":
                case "_M9L10":

                case "_L2M1":
                case "_L3M2":
                case "_L4M3":
                case "_L5M4":
                case "_L6M5":
                case "_L7M6":
                case "_L8M7":
                case "_L9M8":
                case "_L10M9":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 350;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 120;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    break;



                #endregion Diagonal

                #region Bracings

                case "_TCS_ST":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section7;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 8.0d;

                    member.SectionDetails.TopPlate.Width = 150.0;
                    member.SectionDetails.TopPlate.Thickness = 16.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 180.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


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

                    break;
                case "_BCB":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "200200";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;
                    //member.SectionDetails.SidePlate.TotalPlates = 1;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;
                    //member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 1;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


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
                case "_BCB3":
                case "_BCB4":
                case "_BCB5":
                case "_BCB6":
                case "_BCB7":
                case "_BCB8":
                case "_BCB9":
                case "_BCB10":
                case "_BOTTOM_CHORD_BRACING":
                case "_BOTTOM_CHORD_BRACING1":
                case "_BOTTOM_CHORD_BRACING2":
                case "_BOTTOM_CHORD_BRACING3":
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
                #endregion Bracings

                #region _STRINGER & Cross Girder

                case "_STRINGER":
                case "_STRINGER1":
                case "_STRINGER2":
                case "_STRINGER3":
                case "_STRINGER4":
                case "_STRINGER5":
                case "_STRINGER6":
                case "_STRINGER7":
                case "_STRINGER8":
                case "_STRINGER_START":
                case "_STRINGER_END":
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

                    member.SectionDetails.BottomPlate.Width = 150.0;
                    member.SectionDetails.BottomPlate.Thickness = 40.0;


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
                case "_XGIR":
                case "_XGIR1":
                case "_XGIR2":
                case "_XGIR3":
                case "_XGIRDER":
                case "_XGIRDERS":
                case "_CROSSGIRDERS":
                case "_CROSSGIRDER":

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

                    member.SectionDetails.BottomPlate.Width = 350.0;
                    member.SectionDetails.BottomPlate.Thickness = 32.0;


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
                case "_XGIRDER_EDGE":

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

                    member.SectionDetails.BottomPlate.Width = 350.0;
                    member.SectionDetails.BottomPlate.Thickness = 32.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    break;
                #endregion _STRINGER & Cross Girder
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

                member = new CMember(iApp);
                member.Group = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i];
                //member.Group.GroupName = kStr;
                //member.Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].MemberNosText;
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

            if (cmb_mem_group.Items.Count > 0) cmb_mem_group.SelectedIndex = 0; //Chiranjit [2013 06 07] Kolkata

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

                member = new CMember(iApp);
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
                string rep_file = MyList.Get_Analysis_Report_File(Get_User_Input_File_Name(0));
                if (File.Exists(rep_file) && !isCreateData)
                {

                    truss_data = new Arch_Cable_Suspension_Data();

                    truss_data.Length = MyList.StringToDouble(txt_L.Text, 50.0);

                    truss_data.Height = MyList.StringToDouble(txt_H.Text, 6.0);

                    truss_data.Breadth = MyList.StringToDouble(txt_B.Text, 5.4);

                    truss_data.Footpath_Width = MyList.StringToDouble(txt_lfw.Text, 1.0);

                    truss_data.NoOfPanel = (int)MyList.StringToDouble(cmb_panel_nos.Text, 10);

                    truss_data.NoOfStringerBeam = (int)MyList.StringToDouble(txt_stringers_nos.Text, 3);

                    truss_data._L = MyList.StringToDouble(txt_L.Text, 0.0);
                    truss_data._B = MyList.StringToDouble(txt_B.Text, 0.0);
                    truss_data._aw = MyList.StringToDouble(txt_aw.Text, 0.0);
                    truss_data._lfw = MyList.StringToDouble(txt_lfw.Text, 0.0);
                    truss_data._rfw = MyList.StringToDouble(txt_rfw.Text, 0.0);
                    truss_data._hrw = MyList.StringToDouble(txt_hrw.Text, 0.0);
                    truss_data._cw = MyList.StringToDouble(txt_cw.Text, 0.0);
                    truss_data._rw = MyList.StringToDouble(txt_rw.Text, 0.0);
                    truss_data._NP = MyList.StringToDouble(cmb_panel_nos.Text, 12.0);
                    truss_data._NS = MyList.StringToDouble(txt_stringers_nos.Text, 4.0);
                    truss_data._cgw = MyList.StringToDouble(txt_cgw.Text, 4.0);

                    truss_data.D1 = MyList.StringToDouble(txt_D1.Text, 1000.0);
                    truss_data.D2 = MyList.StringToDouble(txt_D2.Text, 800.0);
                    truss_data.ds = MyList.StringToDouble(txt_ds.Text, 42.0);


                    if (truss_data.CreateData(ASTRA_DATA_FILE)) ;


                    #region Chiranjit [2013 05 16]

                    List<string> Work_List = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        Work_List.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(i).Replace("_", " ")));
                    }
                    iApp.Progress_Works = new ProgressList(Work_List);
                    #endregion Chiranjit [2013 05 16]

                    List_Analysis = new List<BridgeMemberAnalysis>();
                    for (int i = 0; i < 5; i++)
                    {
                        rep_file = MyList.Get_Analysis_Report_File(Get_User_Input_File_Name(i));
                        List_Analysis.Add(new BridgeMemberAnalysis(iApp, rep_file, GetForceType()));
                        if (iApp.Is_Progress_Cancel)
                        {
                            iApp.Progress_Works.Clear();
                            iApp.Progress_OFF();
                            return;
                        }
                    }
                    if (iApp.Is_Progress_Cancel) return;

                    Truss_Analysis = List_Analysis[0];
                    List<string> list_node = new List<string>();

                    if (Truss_Analysis.Node_Displacements != null)
                        list_node.Add(Truss_Analysis.Node_Displacements.Get_Max_Deflection().ToString());

                    string kStr = Truss_Analysis.Analysis.Supports[0].NodeNo + " TO "
                                            + Truss_Analysis.Analysis.Supports[3].NodeNo;

                    List<int> jnts = MyList.Get_Array_Intiger(kStr);
                    if (Truss_Analysis.Node_Displacements != null)
                    {
                        for (int i = 0; i < jnts.Count; i++)
                        {
                            foreach (var item in Truss_Analysis.Node_Displacements)
                            {
                                if (item.NodeNo == jnts[i] && item.LoadCase == 1)
                                {
                                    list_node.Add(item.ToString());
                                }
                            }
                        }
                        string file_load_def = Path.Combine(Analysis_Path, "MAX_LOAD_DEFLECTION.TXT");
                        File.WriteAllLines(file_load_def, list_node.ToArray());
                        txt_node_displace.Lines = list_node.ToArray();
                    }
                    iApp.Progress_Works.Clear();
                    //Truss_Analysis = new BridgeMemberAnalysis(iApp, rep_file);
                }
                else
                    Truss_Analysis = new BridgeMemberAnalysis(iApp, analysis_file);

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
                cmb_panel_nos.Text = (Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                //txt_stringers_nos.Text = (Truss_Analysis.Analysis.NoOfStringers).ToString("0");

                //txt_cd_total_joints.Text = Truss_Analysis.Analysis.Joints.Get_Total_Joints_At_Truss_Floor().ToString();
                txt_gd_np.Text = (Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                txt_analysis_file.Visible = true;
                txt_analysis_file.Text = analysis_file;

                rbtn_rail_bridge.Checked = Complete_Design.DeadLoads.IsRailLoad;
                rbtn_road_bridge.Checked = !rbtn_rail_bridge.Checked;
                //SetDefaultDeadLoad();
                if (isCreateData) dgv_mem_details.Rows.Clear();

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
                    if (!isCreateData)
                        SetCompleteDesign(kFile);
                    Read_Results_others();
                    Read_Results_Arches_cables();
                    //Read_DL_SIDL();
                    Read_Live_Load();
                }

                if (!isCreateData)
                {
                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(this, "Analysis Input Data file opened successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            Format_SIDL();
            FillResultGridWithColor();
            FillResultGridWithColor(dgv_member_Results_arch_cables);

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
                cmb_load_type.SelectedIndex = 0;
                if (dgv_live_load.RowCount == 0)
                {
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), iApp.LiveLoads[0].Distance, 0, 1.5, 0.5, iApp.LiveLoads[0].ImpactFactor);
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), iApp.LiveLoads[0].Distance, 0, 4.5, 0.5, iApp.LiveLoads[0].ImpactFactor);
                        dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), txt_X.Text, 0, 1.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]
                        dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), txt_X.Text, 0, 4.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]
              
                    }
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[1].ToString(), iApp.LiveLoads[1].Distance, 0, 1.5, 0.5, iApp.LiveLoads[1].ImpactFactor);
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[1].ToString(), iApp.LiveLoads[1].Distance, 0, 4.5, 0.5, iApp.LiveLoads[1].ImpactFactor);


                        //dgv_live_load.Rows.Add(cmb_load_type.Items[1].ToString(), txt_X.Text, 0, 1.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[1].ToString(), txt_X.Text, 0, 4.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]

                        //Chiranjit [2013 08 16]
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[1].ToString(), txt_X.Text, 0, 2.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]
                        //dgv_live_load.Rows.Add(cmb_load_type.Items[1].ToString(), txt_X.Text, 0, 5.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]

                        //Chiranjit [2013 08 19]
                        dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), txt_X.Text, 0, 2.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]
                        dgv_live_load.Rows.Add(cmb_load_type.Items[0].ToString(), txt_X.Text, 0, 5.5, 0.5, iApp.LiveLoads[0].ImpactFactor); //Chiranjit [2013 05 31]
                  
                    
                    
                    }
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

                btn_View_Moving_Load.Enabled = File.Exists(MyList.Get_LL_TXT_File(Truss_Analysis.Analysis_File)) &&
                                            File.Exists(MyList.Get_Analysis_Report_File(Truss_Analysis.Analysis_File));

            
                
                btn_input_open.Enabled = File.Exists(Truss_Analysis.Analysis_File);

                btn_open_analysis.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Get_User_Input_File_Name(0)));


                btn_open_member_load.Enabled = File.Exists(Path.Combine(user_path, "MEMBER_LOAD_DATA.txt"));



                //btn_Deck_Report.Enabled = File.Exists(Deck.rep_file_name);
                //btn_Abut_Report.Enabled = File.Exists(Abut.rep_file_name);
                //btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
                btnDrawing.Enabled = true;
                btn_Deck_Drawing.Enabled = true;

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
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "240";
                    txt_bp_thk.Text = "10";
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

                    txt_bp_wd.Text = "150";
                    txt_bp_thk.Text = "40";
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

                    txt_bp_wd.Text = "350";
                    txt_bp_thk.Text = "32";
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
        public void TotalAnalysis(StreamWriter sw, ref CMember mem, string step)
        {
            double Rxx, Cxx, a, Iyy, Ixx, Zxx, t, tf, D; // From Table

            double nb, n, S, bolt_dia;

            double tp, Bp, np; // Top Plate
            double tbp, Bbp, nbp; // Bottom Plate
            double ts, Bs, ns; // Side Plate
            double tss, Bss, nss; // Vertical Stiffener Plate

            double cab_dia, cab_thk; // Cable Strands / Steel Beam Tube

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


            M = mem.MaxBendingMoment.Force;
            F = mem.MaxShearForce.Force;
            A = 0.0;
            Anet = 0.0;
            ry = 0.0;
            double ry_anet = 0.0;
            Iy = 0.0;
            //M = F = 0.0;


            //Chiranjit [2012 02 08]
            bool Check_Tens = false;
            bool Check_Comp = false;

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

            cab_dia = mem.SectionDetails.Cables_Strands.Diameter;
            cab_thk = mem.SectionDetails.Cables_Strands.Thickness;

            mem.DesignReport.Clear();
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            string str = "";


            if (step != "")
                str = (string.Format("STEP {0} : DESIGN OF {1}, [GROUP : {2}]", step, MemberString.GerMemberString(mem), mem.Group.GroupName));
            else
                str = (string.Format("DESIGN OF {0}", MemberString.GerMemberString(mem)));
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));
            mem.DesignReport.Add(str);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));

            //if (step != "")
            //    mem.DesignReport.Add(string.Format("STEP {0} : DESIGN OF {1}", step, MemberString.GerMemberString(mem)));
            //else
            //    mem.DesignReport.Add(string.Format("DESIGN OF {0}", MemberString.GerMemberString(mem)));

            mem.DesignReport.Add("");

            List<CMember> lst_mbr = Get_Members_All_Analysis(mem.Group.GroupName, lst_all_groups);

            str = string.Format("MEMBER GROUP : {0}", mem.Group.GroupName);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));
            mem.DesignReport.Add(str);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));

            //mem.DesignReport.Add(string.Format("MEMBER GROUP : {0}", mem.Group.GroupName));
            //mem.DesignReport.Add(string.Format("-----------------------"));
            mem.DesignReport.Add(string.Format(""));


            for (int i = 0; i < lst_mbr.Count; i++)
            {
                //if (lst_mbr[i].Group.GroupName.StartsWith("_IC") && i == 3)
                //    str = string.Format("MEMBER NOS : {0} in [{1}]", lst_mbr[i].Group.MemberNosText, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(4)));
                //else

                if (lst_mbr[i].Group.GroupName == mem.Group.GroupName)
                {
                    str = string.Format("MEMBER NOS : {0} in [{1}]", lst_mbr[i].Group.MemberNosText, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(i)));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(str);
                }
            }




            //mem.DesignReport.Add("".PadLeft(str.Length, '-'));
            //mem.DesignReport.Add(str);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));

            //mem.DesignReport.Add(string.Format("MEMBER NOS : {0}", mem.Group.MemberNosText));
            //mem.DesignReport.Add(string.Format("-----------------------"));
            mem.DesignReport.Add(string.Format(""));
            if (mem.MemberType == eMemberType.CrossGirder)
            {
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("NOTE: Forces from Member at Supported Edges are not taken."));
                mem.DesignReport.Add(string.Format(""));
            }

            if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
            {
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("AXIAL FORCE DATA"));
                mem.DesignReport.Add(string.Format("----------------"));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Obtained from Analysis Report File."));
                mem.DesignReport.Add(string.Format(""));
                if (mem.MaxTensionForce.Force != 0.0)
                {   
                    //[MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Force, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                    mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN      [MemberNo = {1} , LoadNo = {2}  in {3}]", mem.MaxTensionForce,
                        mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(mem.MaxBendingMoment.File_Index))));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("CORRESPONDING TENSILE STRESS = {0:f3} kN/sq.m       [MemberNo = {1} , LoadNo = {2}]", 
                        mem.MaxTensionForce.Stress, mem.MaxTensionForce.MemberNo,
                        mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));

                }
                if (mem.MaxCompForce.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN   [MemberNo = {1} , LoadNo = {2} in {3}] ",
                        mem.MaxCompForce, mem.MaxCompForce.MemberNo,
                        mem.MaxCompForce.Loadcase, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(mem.MaxCompForce.File_Index))));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("CORRESPONDING COMPRESSIVE STRESS = {0:f3} kN/sq.m  [MemberNo = {1} , LoadNo = {2}]", 
                        mem.MaxCompForce.Stress, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                }
                if (mem.MaxStress.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("MAXIMUM STRESS = {0:f3} kN/sq.m", mem.MaxStress));
                }

            }

            mem.DesignReport.Add(string.Format(""));
            if (mem.Length != 0.0)
                mem.DesignReport.Add(string.Format("Length of Member = ly = {0:f3} m", mem.Length));
            if(bolt_dia != 0.0)
            mem.DesignReport.Add(string.Format("Diameter of Bolt = bolt_dia = {0} mm", bolt_dia));
            if (nb != 0.0)
                mem.DesignReport.Add(string.Format("No of Bolt in a Section = nb = {0} ", nb));
            mem.DesignReport.Add(string.Format(""));

            //Chiranjit
            //if (mem.SectionDetails.DefineSection != eDefineSection.Section11 &&
            //    mem.SectionDetails.DefineSection != eDefineSection.Section12 &&
            //    mem.SectionDetails.DefineSection != eDefineSection.Section13 &&
            //    mem.SectionDetails.DefineSection != eDefineSection.Section14)
            //{
            if (true)
            {
                #region Plate Details
                //mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                mem.DesignReport.Add(string.Format("---------------------"));
                mem.DesignReport.Add(string.Format(""));
                //for Ang
                if (mem.SectionDetails.SectionName.Contains("A"))
                {

                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));
                    mem.DesignReport.Add(string.Format(""));
                    if (S != 0.0)
                        mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                //for Beam
                else if (mem.SectionDetails.SectionName.Contains("B"))
                {
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    mem.DesignReport.Add(string.Format(""));
                    if (S != 0.0)
                        mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                //for Channel
                else if (mem.SectionDetails.SectionName.Contains("C"))
                {
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    mem.DesignReport.Add(string.Format(""));
                    if (S != 0.0)
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
                    mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                }
                if ((Bs * ts) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    mem.DesignReport.Add(string.Format("No Of Side Plates = ns = {0}", ns));
                }
                if ((Bss * tss) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                }
                if ((Bss * tss) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                }
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

                    //mem.DesignReport.Add(string.Format(""));
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
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    //mem.DesignReport.Add(string.Format(""));
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
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
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
                    //mem.DesignReport.Add(string.Format(""));
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

                    //mem.DesignReport.Add(string.Format(""));
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

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns); ;
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
                    mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxBendingMoment.MemberNo, mem.MaxBendingMoment.Loadcase));
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
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
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
                    mem.DesignReport.Add(string.Format("             = ({0} * 1000.0)/({1} * {2} + {3} * {4})", F, t, D, tbp, Bbp));
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
                    mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxBendingMoment.MemberNo, mem.MaxBendingMoment.Loadcase));
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
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
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


                    shr_stress = (F * 1000.0) / (t * D + tbp * Bbp);
                    mem.DesignReport.Add(string.Format("Shear Stress = (F * 1000.0) /(t * D + tbp * Bbp)"));
                    mem.DesignReport.Add(string.Format("             = ({0:f3} * 1000.0) / ({1} * {2} + {3} * {4})", F, t, D, tbp, Bbp));
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
                    if ((tp * Bp * np) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("    + (tp * Bp * np)"));
                    }
                    if ((ts * Bs * ns) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("    + (ts * Bs * ns)"));
                    }
                    if ((tss * Bss * nss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("    + (tss * Bss * nss)"));
                    }
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100", n, a));

                    if ((tp * Bp * np) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("    + ({0} * {1}* {2})", tp, Bp, np));
                    }
                    if ((ts * Bs * ns) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("    + ({0} * {1}* {2})", ts, Bs, ns));
                    }
                    if ((tss * Bss * nss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("    + ({0} * {1}* {2})", tss, Bss, nss));
                    }




                    mem.DesignReport.Add(string.Format(""));
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
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
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

                case eDefineSection.Section16:
                    #region Section 16

                    double st_grade = 350.0;
                    double req_thk = cab_dia / 120.0;

                    double La = mem.Length;
                    double Le = 0.85 * La;

                    A = mem.SectionDetails.Cables_Strands.Area;
                    Ixx = mem.SectionDetails.Cables_Strands.Ixx;
                    Zxx = mem.SectionDetails.Cables_Strands.Zxx;
                    Rxx = mem.SectionDetails.Cables_Strands.rxx;

                    double lamda = Le / Rxx;


                    if (mem.MemberType == eMemberType.ArchMembers)
                    {
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Diameter of Arch Section = D1 = {0} mm", cab_dia));
                        mem.DesignReport.Add(string.Format("Thickness of Arch Section = t = {0} mm", cab_thk));
                        mem.DesignReport.Add(string.Format("Grade of Steel of Arch Section Fe490 / E{0:f0} = {0:f3} N/Sq.mm", st_grade));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Check for Thickness"));
                        mem.DesignReport.Add(string.Format("-------------------"));
                        mem.DesignReport.Add(string.Format(""));



                        if (req_thk < cab_thk)
                            mem.DesignReport.Add(string.Format("Required thickness = D1 / 120 = {0} / 120 = {1:f3} mm < {2} mm,   OK", cab_dia, req_thk, cab_thk));
                        else
                            mem.DesignReport.Add(string.Format("Required thickness = D1 / 120 = {0} / 120 = {1:f3} mm > {2} mm,   NOT OK", cab_dia, req_thk, cab_thk));

                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Section Properties "));
                        mem.DesignReport.Add(string.Format("---------------------"));
                        mem.DesignReport.Add(string.Format(""));


                        mem.DesignReport.Add(string.Format("Area of Cross Section = A = (π x D1^2 / 4) - (π x (D1 - 2 x t)^2 / 4)"));
                        mem.DesignReport.Add(string.Format("                          = ({0:f4} x {1}^2 / 4) - ({0:f4} x ({1} - 2 x {2})^2 / 4)", Math.PI, cab_thk, cab_thk));
                        mem.DesignReport.Add(string.Format("                          = {0:f4} Sq.mm", A));



                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Ixx = (π/64) x (D1^4 - (D1 - 2 x t)^4)"));
                        mem.DesignReport.Add(string.Format("    = ({0:f4}/64) x ({1}^4 - ({1} - 2 x {2})^4)", Math.PI, cab_dia, cab_thk));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Sq.Sq.mm", Ixx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Iyy = (π/64) x (D1^4 - (D1 - 2 x t)^4)"));
                        mem.DesignReport.Add(string.Format("    = ({0:f4}/64) x ({1}^4 - ({1} - 2 x {2})^4)", Math.PI, cab_dia, cab_thk));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Sq.Sq.mm", Ixx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format("Zxx = Ixx / (D1 / 2) "));
                        mem.DesignReport.Add(string.Format("    = {0:f4} / ({1} / 2) ", Ixx, cab_dia));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Cu.mm", Zxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Zyy = Iyy / (D1 / 2) "));
                        mem.DesignReport.Add(string.Format("    = {0:f4} / ({1} / 2) ", Ixx, cab_dia));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Cu.mm", Zxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format("rxx = SQRT(Ixx / A) "));
                        mem.DesignReport.Add(string.Format("    = SQRT({0:f4} / {1:f4}) ", Ixx, A));
                        mem.DesignReport.Add(string.Format("    = {0:f4} mm", Rxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("ryy = SQRT(Iyy / A) "));
                        mem.DesignReport.Add(string.Format("    = SQRT({0:f4} / {1:f4}) ", Ixx, A));
                        mem.DesignReport.Add(string.Format("    = {0:f4} mm", Rxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Permissible Stresses"));
                        mem.DesignReport.Add(string.Format("--------------------"));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format("Length of Arch Member = La = {0} m", La));
                        mem.DesignReport.Add(string.Format(""));


                        mem.DesignReport.Add(string.Format("Effective Length of Arch Member = Le = 0.85 x La"));
                        mem.DesignReport.Add(string.Format("                                     = 0.85 x {0:f4}", La));
                        mem.DesignReport.Add(string.Format("                                     = {0:f4} m", Le));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        Rxx = (Rxx / 1000.0);
                        mem.DesignReport.Add(string.Format("Slenderness Ratio = λ = Le / ryy"));
                        mem.DesignReport.Add(string.Format("                      = {0:f3} / {1:f4}", Le, Rxx));

                        if (lamda < 120.0)
                            mem.DesignReport.Add(string.Format("                      = {0:f3} < 120.0,   OK", lamda));
                        else
                            mem.DesignReport.Add(string.Format("                      = {0:f3} > 120.0,   NOT OK", lamda));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));

                    }
                    if (mem.MemberType == eMemberType.TransverseMember)
                    {
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Diameter of Steel Beam Tube = D2 = {0} mm", cab_dia));
                        mem.DesignReport.Add(string.Format("Thickness of Arch Section = t = {0}", cab_thk));
                        mem.DesignReport.Add(string.Format("Grade of Steel of Arch Section Fe490 / E{0:f0} = {0:f3} N/Sq.mm", st_grade));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Check for Thickness"));
                        mem.DesignReport.Add(string.Format("-------------------"));
                        mem.DesignReport.Add(string.Format(""));


                        if (req_thk < cab_thk)
                            mem.DesignReport.Add(string.Format("Required thickness = D2 / 120 = {0} / 120 = {1:f3} mm < {2} mm,   OK", cab_dia, req_thk, cab_thk));
                        else
                            mem.DesignReport.Add(string.Format("Required thickness = D2 / 120 = {0} / 120 = {1:f3} mm > {2} mm,   NOT OK", cab_dia, req_thk, cab_thk));

                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Section Properties "));
                        mem.DesignReport.Add(string.Format("---------------------"));
                        mem.DesignReport.Add(string.Format(""));


                        mem.DesignReport.Add(string.Format("Area of Cross Section = A = (π x D2^2 / 4) - (π x (D2 - 2 x t)^2 / 4)"));
                        mem.DesignReport.Add(string.Format("                          = ({0:f4} x {1}^2 / 4) - ({0:f4} x ({1} - 2 x {2})^2 / 4)", Math.PI, cab_thk, cab_thk));
                        mem.DesignReport.Add(string.Format("                          = {0:f4} Sq.mm", A));



                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Ixx = (π/64) x (D2^4 - (D2 - 2 x t)^4)"));
                        mem.DesignReport.Add(string.Format("    = ({0:f4}/64) x ({1}^4 - ({1} - 2 x {2})^4)", Math.PI, cab_dia, cab_thk));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Sq.Sq.mm", Ixx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Iyy = (π/64) x (D2^4 - (D2 - 2 x t)^4)"));
                        mem.DesignReport.Add(string.Format("    = ({0:f4}/64) x ({1}^4 - ({1} - 2 x {2})^4)", Math.PI, cab_dia, cab_thk));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Sq.Sq.mm", Ixx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format("Zxx = Ixx / (D2 / 2) "));
                        mem.DesignReport.Add(string.Format("    = {0:f4} / ({1} / 2) ", Ixx, cab_dia));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Cu.mm", Zxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Zyy = Iyy / (D2 / 2) "));
                        mem.DesignReport.Add(string.Format("    = {0:f4} / ({1} / 2) ", Ixx, cab_dia));
                        mem.DesignReport.Add(string.Format("    = {0:f4} Cu.mm", Zxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format("rxx = SQRT(Ixx / A) "));
                        mem.DesignReport.Add(string.Format("    = SQRT({0:f4} / {1:f4}) ", Ixx, A));
                        mem.DesignReport.Add(string.Format("    = {0:f4} mm", Rxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("ryy = SQRT(Iyy / A) "));
                        mem.DesignReport.Add(string.Format("    = SQRT({0:f4} / {1:f4}) ", Ixx, A));
                        mem.DesignReport.Add(string.Format("    = {0:f4} mm", Rxx));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Permissible Stresses"));
                        mem.DesignReport.Add(string.Format("--------------------"));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format("Length of Transverse Beam Member = B = {0} m", La));
                        mem.DesignReport.Add(string.Format(""));


                        mem.DesignReport.Add(string.Format("Effective Length of Arch Member = Be = 0.85 x B"));
                        mem.DesignReport.Add(string.Format("                                     = 0.85 x {0:f4}", La));
                        mem.DesignReport.Add(string.Format("                                     = {0:f4} m", Le));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));
                        Rxx = (Rxx / 1000.0);
                        mem.DesignReport.Add(string.Format("Slenderness Ratio = λ = Be / ryy"));
                        mem.DesignReport.Add(string.Format("                      = {0:f3} / {1:f4}", Le, Rxx));

                        if (lamda < 120.0)
                            mem.DesignReport.Add(string.Format("                      = {0:f3} < 120.0,   OK", lamda));
                        else
                            mem.DesignReport.Add(string.Format("                      = {0:f3} > 120.0,   NOT OK", lamda));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format(""));

                    }

                    double sigma_ac = 188.5; //from Table 1
                    double sigma_bc = 217.0; //from Table 2


                    sigma_ac = iApp.Tables.Permissible_Stress_Axial_Compression(fy, lamda, ref kStr);
                    sigma_bc = iApp.Tables.Permissible_Bending_Compressive_Stresses((cab_dia / cab_thk), lamda, ref kStr);

                    mem.DesignReport.Add(string.Format("Permissible Axial Compressive Stress = σ_ac = {0:f3} N/Sq.mm,  Refer to Table 5", sigma_ac));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Permissible Bending Stress = σ_bc = {0:f3} N/Sq.mm,  Refer to Table 6", sigma_bc));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Forces from Analysis for Service Condition"));
                    mem.DesignReport.Add(string.Format("-------------------------------------------"));

                    F = mem.MaxAxialForce;

                    if (F.ToString("f3") != "0.000")
                    {
                        mem.DesignReport.Add(string.Format("Maximum Axial Force = F = {0:f3} kN  [MemberNo = {1}, LoadNo = {2} in {3}]", F, mem.MaxAxialForce.MemberNo,
                                                 mem.MaxAxialForce.Loadcase, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(mem.MaxAxialForce.File_Index))));
                    }
                    else
                    {

                        F = mem.MaxShearForce.Force;
                        mem.DesignReport.Add(string.Format("Maximum Axial Force = F = {0:f3} kN  [MemberNo = {1}, LoadNo = {2} in {3}]", F, mem.MaxShearForce.MemberNo,
                                                 mem.MaxShearForce.Loadcase, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(mem.MaxShearForce.File_Index))));
                    }
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format("In Plane Bending Moment = Mz = {0:f3} kN-m  [MemberNo = {1}, LoadNo = {2} in {3}]", M, mem.MaxBendingMoment.MemberNo,
                                          mem.MaxBendingMoment.Loadcase, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(mem.MaxBendingMoment.File_Index))));
                    mem.DesignReport.Add(string.Format(""));

                    double Mx = mem.MaxTorsion.Force;
                    mem.DesignReport.Add(string.Format("Out Plane Bending Moment = Mx = {0:f3} kN-m [MemberNo = {1}, LoadNo = {2} in {3}]", Mx, mem.MaxTorsion.MemberNo,
                                          mem.MaxTorsion.Loadcase, Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(mem.MaxTorsion.File_Index))));


                    mem.DesignReport.Add(string.Format(""));
                    if (mem.MemberType == eMemberType.TransverseMember)
                    {
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("Transverse Shear = 5.0 % of Axial Force in the Member"));
                        mem.DesignReport.Add(string.Format("                 = {0:f3} * 0.05", F));
                        mem.DesignReport.Add(string.Format("                 = {0:f3} kN", (F * 0.05)));
                        mem.DesignReport.Add(string.Format(""));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Calculation for developed stresses"));
                    mem.DesignReport.Add(string.Format("------------------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    double fa = (F * 1000) / A;
                    mem.DesignReport.Add(string.Format("Primary Stress = fa = F / A"));
                    mem.DesignReport.Add(string.Format("                    = ({0:f3} x 1000) / {1:f3}", F, A));
                    mem.DesignReport.Add(string.Format("                    = {0:f3} N/Sq.mm", fa));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));


                    double ss_in = (M * 1000000.0) / Zxx;

                    mem.DesignReport.Add(string.Format("Secondary Stress due to In Plane Moment = Mz / Z"));
                    mem.DesignReport.Add(string.Format("                                        = ({0:f3} x 10^6)/ {1:f3}", M, Zxx));
                    mem.DesignReport.Add(string.Format("                                        = {0:f3} N/Sq.mm", ss_in));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    double ss_out = (Mx * 1000000.0) / Zxx;
                    mem.DesignReport.Add(string.Format("Secondary Stress due to Out Plane Moment = Mx / Z"));
                    mem.DesignReport.Add(string.Format("                                         = ({0:f3} x 10^6)/ {1:f3}", Mx, Zxx));
                    mem.DesignReport.Add(string.Format("                                         = {0:f3} N/Sq.mm", ss_out));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    double fb = ss_in + ss_out;

                    mem.DesignReport.Add(string.Format("Total Secondary Stress = fb = {0:f3} + {1:f3} = {2:f3} N/Sq.mm", ss_in, ss_out, fb));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Check for Stresses against permissible Values"));
                    mem.DesignReport.Add(string.Format("----------------------------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Primary Permissible Stress for Axial Compression = {0:F3} N/sq.mm", sigma_ac));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    double Fa = sigma_ac * 1.16;
                    mem.DesignReport.Add(string.Format("Secondary Permissible Stress for Axial Compression = Fa = {0:F3} x 1.16 = {1:f3} N/sq.mm", sigma_ac, Fa));

                    mem.DesignReport.Add(string.Format(""));
                    double Fb = sigma_bc * 1.16;
                    mem.DesignReport.Add(string.Format("Secondary Permissible Stress for Bending = Fb = {0:F3} x 1.16 = {1:f3} N/sq.mm", sigma_bc, Fb));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Primary Stress Ratio = {0:f3} / {1:f3} = {2:f3}", fa, sigma_ac, (fa / sigma_ac)));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ss_out = (fa / Fa) + (fb / Fb);

                    mem.Secondary_Stress_Ratio = ss_out;
                    mem.Secondary_Stress_Ratio_Capacity = 1.0;
                    
                    mem.DesignReport.Add(string.Format("Secondary Stress Ratio = (fa / Fa) + (fb / Fb)"));
                    mem.DesignReport.Add(string.Format("                       = ({0:f3} / {1:f3}) + ({2:f3} / {3:f3})", fa, Fa, fb, Fb));
                    mem.DesignReport.Add(string.Format("                       = {0:f3} + {1:f3}", (fa / Fa), (fb / Fb)));
                    if (ss_out < 1.0)
                    {
                        mem.DesignReport.Add(string.Format("                       = {0:f3}  <  1.0 ,   So, OK ", ss_out));
                        mem.Result = "OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                       = {0:f3}  >  1.0 ,   So, NOT OK ", ss_out));
                        mem.Result = "NOT OK";
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    goto _SWWrite;
                    #endregion Section 6
                    return;
                case eDefineSection.Section17:
                    #region Design of Inner & Outer Suspension Cables
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("STEP {0}.1 : Design of CABLES / STRANDS", step));
                    mem.DesignReport.Add(string.Format("-------------------------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    double ds = cab_dia;
                    mem.DesignReport.Add(string.Format("Diameter of the Strands = ds = {0} mm", ds));
                    mem.DesignReport.Add(string.Format(""));

                    A = mem.SectionDetails.Cables_Strands.Area;
                    mem.DesignReport.Add(string.Format("Area of the Strands = A = (π x ds^2 / 4) = {0} mm", ds));
                    mem.DesignReport.Add(string.Format("                        = ({0:f4} x {1}^2 / 4)", Math.PI, ds));
                    mem.DesignReport.Add(string.Format("                        = {0:f4} Sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));


                    //double UTS = 1570.0; // User Input
                    double UTS = MyList.StringToDouble(txt_UTS.Text, 1570.0); // User Input


                    mem.DesignReport.Add(string.Format("Ultimate Tensile Strength of  I x 91J Spiral Strand = UTS = {0} N/Sq.mm", UTS));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum Breaking Force of Strand = 90 % of UTS"));
                    mem.DesignReport.Add(string.Format("                                 = 0.90 x {0:f3}", UTS));

                    double max_brk_frc = 0.9 * UTS;
                    F = mem.MaxTensionForce;

                    mem.Breaking_Force_Capacity = max_brk_frc;
                    if (max_brk_frc > F)
                    {
                        mem.DesignReport.Add(string.Format("                                 = {0:f3} kN > {1:f3} kN,   OK", max_brk_frc, F));
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                                 = {0:f3} kN < {1:f3} kN,   NOT OK", max_brk_frc, F));
                        mem.Result = "NOT OK";

                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Permissible Breaking Force of Strand = 50 % of UTS"));
                    mem.DesignReport.Add(string.Format("                                     = 0.50 x {0:f3}", UTS));


                    double prm_brk_frc = 0.5 * UTS;
                    mem.Permissible_Breaking_Force_Capacity = prm_brk_frc;
                    if (max_brk_frc > F)
                        mem.DesignReport.Add(string.Format("                                     = {0:f3} kN > {1:f3} kN,   OK", prm_brk_frc, F));
                    else
                    {
                        mem.DesignReport.Add(string.Format("                                     = {0:f3} kN < {1:f3} kN,   NOT OK", prm_brk_frc, F));
                        mem.Result = "NOT OK";
                    }


                    double conn_d, conn_nr, conn_bg, conn_tg, conn_fs, conn_fb, conn_ft;

                    conn_d = MyList.StringToDouble(txt_conn_d.Text, 0.0);
                    conn_nr = MyList.StringToDouble(txt_conn_nr.Text, 0.0);
                    conn_bg = MyList.StringToDouble(txt_conn_bg.Text, 0.0);
                    conn_tg = MyList.StringToDouble(txt_conn_tg.Text, 0.0);
                    conn_fs = MyList.StringToDouble(txt_conn_fs.Text, 0.0);
                    conn_fb = MyList.StringToDouble(txt_conn_fb.Text, 0.0);
                    conn_ft = MyList.StringToDouble(txt_conn_ft.Text, 0.0);


                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("STEP {0}.2 : Design of Connection Plates & Bolts", step));
                    mem.DesignReport.Add(string.Format("----------------------------------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Check for Strength of Connection Plate with the Arch member"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Thickness of the plate = {0} mm", conn_tg));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Diameter of the Bolt / Strands = d = {0} mm", conn_d));

                    double blt_hole = conn_d + 2.0;
                    mem.DesignReport.Add(string.Format("Diameter of the Bolt Hole = {0} + 2.0 = {1} mm", conn_d, blt_hole));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Number of Bolt Holes in the Plate = {0}", conn_nr));
                    mem.DesignReport.Add(string.Format(""));

                    //Bp = 500.0; //User Input
                    Bp = MyList.StringToDouble(txt_conn_Bp.Text, 500.0);
                    mem.DesignReport.Add(string.Format("Minimum width of Plate at Bolt Locations = Bp = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    double aps = Bp * conn_tg;
                    mem.DesignReport.Add(string.Format("Area of Plate Side = aps = {0} x {1} = {2:f3} Sq.mm", Bp, conn_tg, aps));
                    mem.DesignReport.Add(string.Format(""));

                    double ah = conn_nr * blt_hole * conn_tg;
                    mem.DesignReport.Add(string.Format("Area of {0} Holes = ah = {0} x {1} x {2} = {3:f3} Sq.mm", conn_nr, blt_hole, conn_tg, ah));
                    mem.DesignReport.Add(string.Format(""));
                    double apnet = aps - ah;
                    mem.DesignReport.Add(string.Format("Net Area of Plate Side = apnet = {0:f3} - {1:f3} = {2:f3} Sq.mm", aps, ah, apnet));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Yield Stress of the Plate = fy = {0:f3} N/Sq.mm", fy));
                    mem.DesignReport.Add(string.Format(""));

                    double pst = 0.6 * fy;
                    mem.DesignReport.Add(string.Format("Permissible Stress for Tearing of the Plate = pst = 60 % of fy "));
                    mem.DesignReport.Add(string.Format("                                                  = 0.60 x {0:f3}", fy));
                    mem.DesignReport.Add(string.Format("                                                  = {0:f3} N/Sq.mm", pst));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    double spt = apnet * pst;
                    mem.DesignReport.Add(string.Format("Strength of the Plate for Tearing = apnet x ft"));
                    mem.DesignReport.Add(string.Format("                                  = {0:f3} x {1:f3}", apnet, ft));
                    mem.DesignReport.Add(string.Format("                                  = {0:f3} N", spt));
                    spt = spt / 1000.0;
                    mem.DesignReport.Add(string.Format("                                  = {0:f3} kN", spt));
                    mem.DesignReport.Add(string.Format(""));

                    if (max_brk_frc < spt)
                        mem.DesignReport.Add(string.Format("Maximum Breaking Force in Strand through the Plate = {0:f3} kN < {1:f3} kN,    OK", max_brk_frc, spt));
                    else
                        mem.DesignReport.Add(string.Format("Maximum Breaking Force in Strand through the Plate = {0:f3} kN > {1:f3} kN,  NOT OK", max_brk_frc, spt));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("STEP {0}.3 : Check for Strength of Bolt / Cable", step));
                    mem.DesignReport.Add(string.Format("--------------------------------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum force in the Cable / Strand = {0:f3} kN", F));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Thickness of the plate = {0} mm", conn_tg));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Diameter of the Bolt / Strands = d = {0} mm", conn_d));
                    mem.DesignReport.Add(string.Format(""));


                    double ar_bolt = Math.PI * conn_d * conn_d / 4.0;
                    mem.DesignReport.Add(string.Format("Cross Section Area of Bolt / Cable = π x {0}^2 / 4", conn_d));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} x {1}^2 / 4", Math.PI, conn_d));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} Sq.mm", ar_bolt));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    double ex_ar = 0.78 * ar_bolt;

                    mem.DesignReport.Add(string.Format("   78 % of above Area = 0.78 * {0:f3} = {1:f3} Sq.mm", ar_bolt, ex_ar));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Diameter of the Bolt Hole = {0} + 2.0 = {1} mm", conn_d, blt_hole));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    //double Bolt_Grade = 10.9; //user input
                    double Bolt_Grade = MyList.StringToDouble(txt_conn_Bolt_Grade.Text, 10.9);
                    mem.DesignReport.Add(string.Format("Bolt Grade = {0:f3}", Bolt_Grade));

                  
                    mem.DesignReport.Add(string.Format("Permissible Shear Strength of Bolt = {0} N/Sq.mm", conn_fs));
                    mem.DesignReport.Add(string.Format(""));

                    double sb_ss = conn_fs * ex_ar;
                    mem.DesignReport.Add(string.Format("Strength of Bolt in Single Shear = {0} x {1:f3}", conn_fs, ex_ar));
                    mem.DesignReport.Add(string.Format("                                 = {0:f3} N", sb_ss));
                    mem.DesignReport.Add(string.Format("                                 = {0:f3} kN", (sb_ss = sb_ss / 1000.0)));
                    mem.DesignReport.Add(string.Format(""));

                    double sb_ds = 2 * sb_ss;

                    mem.DesignReport.Add(string.Format("Strength of Bolt in Double Shear = 2 x {0:f3} = {1:f3} kN", sb_ss, sb_ds));
                    mem.DesignReport.Add(string.Format(""));

                    //double conn_ts = 310.0;
                    double conn_ts = MyList.StringToDouble(txt_conn_ts.Text, 310.0);
                    mem.DesignReport.Add(string.Format("Permissible Tensile Strength of Bolt = {0} N/Sq.mm", conn_ts));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Permissible Bearing Strength of Bolt = {0} N/Sq.mm", conn_fb));
                    mem.DesignReport.Add(string.Format(""));

                    //double conn_fps = 340.0;
                    double conn_fps = MyList.StringToDouble(txt_conn_fps.Text, 340.0);
                    mem.DesignReport.Add(string.Format("Permissible Bearing Strength of Plate = {0} N/Sq.mm", conn_fps));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    double sbb = conn_ts * conn_d * conn_fb;
                    mem.DesignReport.Add(string.Format("Strength of Bolt in Bearing = {0} x {1} x {2}", conn_ts, conn_d, conn_fb));
                    mem.DesignReport.Add(string.Format("                            = {0:f3} N", sbb));
                    mem.DesignReport.Add(string.Format("                            = {0:f3} kN", (sbb = sbb / 1000.0)));
                    mem.DesignReport.Add(string.Format(""));

                    double spb = conn_ts * blt_hole * conn_fps;
                    mem.DesignReport.Add(string.Format("Strength of Plate in Bearing = {0} x {1} x {2}", conn_ts, blt_hole, conn_fps));
                    mem.DesignReport.Add(string.Format("                             = {0:f3} N", spb));
                    mem.DesignReport.Add(string.Format("                             = {0:f3} kN", (spb = spb / 1000.0)));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    double least = Math.Min(sb_ds, sbb);
                    least = Math.Min(least, spb);

                    mem.DesignReport.Add(string.Format("Least value for strengths {0:f3} kN, {1:f3} kN  &  {2:f3} kN  = {3:f3} kN", sb_ds, sbb, spb, least));
                    mem.DesignReport.Add(string.Format(""));

                    double bolt_req = F / least;
                    mem.DesignReport.Add(string.Format("Number of Bolts Required = {0:f3} / {1:f3} = {2:f3} Nos", F, least, bolt_req));
                    mem.DesignReport.Add(string.Format(""));
                    if (nb > bolt_req)
                        mem.DesignReport.Add(string.Format("Number of Bolts Provided = {0} nos  >  {1:f3} nos,   So, OK",nb , bolt_req));
                    else
                        mem.DesignReport.Add(string.Format("Number of Bolts Provided = {0} nos  <  {1:f3} nos,   So, NOT OK, change Bolt Grade", nb, bolt_req));


                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("STEP {0}.4 : Connection of Strands with Cross Girders", step));
                    mem.DesignReport.Add(string.Format("--------------------------------------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Maximum Force in the Inner Strand = 90 % of UTS"));
                    mem.DesignReport.Add(string.Format("                                  = 0.90 x {0:f3}", UTS));
                    mem.DesignReport.Add(string.Format("                                  = {0:f3}", max_brk_frc));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    double weld_strng = MyList.StringToDouble(txt_weld_strength.Text, 108.0);
                    double weld_sz = MyList.StringToDouble(txt_weld_size.Text, 10.0);
                    double weld_thk = MyList.StringToDouble(txt_throat_size.Text, 10.0);
                    //double weld_thk = 7.07;
                    //double weld_thk = MyList.StringToDouble(txt_weld_size.Text, 10.0);
                    mem.DesignReport.Add(string.Format("Permissible Shear Stress in the Weld = σat = {0} N/Sq.mm", weld_strng));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Size of the Weld = {0} mm", weld_sz));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Thickness of the Weld = {0} mm", weld_thk));
                    mem.DesignReport.Add(string.Format(""));

                    double weld_len = 2 * Bp;
                    mem.DesignReport.Add(string.Format("Length of the Weld on both Sides of the plate = 2 x Bp = 2 x {0:f3} = {1:f3} mm", Bp, weld_len));
                    mem.DesignReport.Add(string.Format(""));

                    double ld_wcc = weld_strng * weld_sz * weld_thk;
                    mem.DesignReport.Add(string.Format("Load Carrying Capacity of the Weld = σat x {0:f3} x {1:f3}", weld_thk, weld_len));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} x {1:f3} x {2:f3}", weld_strng, weld_thk, weld_len));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N", ld_wcc));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN", ld_wcc = ld_wcc / 1000.0));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    if (F < ld_wcc)
                    mem.DesignReport.Add(string.Format("Actual Load through the Weld = {0:f3} kN  <  {1:f3} kN,   So,  OK", F, ld_wcc));
                    else
                        mem.DesignReport.Add(string.Format("Actual Load through the Weld = {0:f3} kN  >  {1:f3} kN,   So,  NOT OK", F, ld_wcc));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    #endregion Design of Inner & Outer Suspension Cables
                    goto _SWWrite;

            }
            #endregion Define Section

            if (mem.MaxCompForce.Force == 0.0) mem.Compressive_Stress = 0.0;
            if (mem.MaxTensionForce.Force == 0.0) mem.Tensile_Stress = 0.0;
            if (mem.MaxCompForce.Force != 0.0)
            {
                #region Compression
                //double L = mem.Length;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("-----------------------------------"));
                mem.DesignReport.Add(string.Format("COMPRESSIVE LOAD CARRYING CAPACITY"));
                mem.DesignReport.Add(string.Format("-----------------------------------"));
                mem.DesignReport.Add(string.Format(""));

                if (mem.MaxCompForce.Stress == 0.0)
                {
                    mem.MaxCompForce.Stress = mem.MaxCompForce.Force / (Anet / 1000000);
                }

                if (mem.MaxCompForce.MemberNo != 0 && mem.MaxCompForce.MemberNo != 0)
                {

                    mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN    [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Force, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("COMPRESSIVE STRESS = {0:f3} kN/m^2    [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Stress, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                }
                else
                {
                    mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN", mem.MaxCompForce.Force));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("COMPRESSIVE STRESS = {0:f3} kN/m^2", mem.MaxCompForce.Stress));
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
                mem.DesignReport.Add(string.Format("From Table 4,   σ_ac = {0} N/sq.mm", sigma));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));

                //double comp_load_cap = sigma * (Anet / 1000.0));
                double comp_load_cap = sigma * (A / 1000.0);


                //Chiranjit [2011 07 01] 
                //Store Compressive Stress
                //mem.Compressive_Stress = (mem.MaxCompForce * 1000) / (A);
                mem.Capacity_Compressive_Stress = sigma;


                mem.Capacity_CompForce = comp_load_cap;
                mem.DesignReport.Add(string.Format("Compressive Load Carrying Capacity = σ_ac * A   N"));
                mem.DesignReport.Add(string.Format("                                   = ({0}*{1:f3})/1000   kN", sigma, A));

                if (comp_load_cap > mem.MaxCompForce.Force)
                {

                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Compressive Force OK", comp_load_cap, mem.MaxCompForce.Force, mem.Group.MemberNosText));
                    mem.Result = "OK";
                    Check_Comp = true;
                }
                else if (comp_load_cap.ToString("f3") == mem.MaxCompForce.Force.ToString("f3"))
                {
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Comp = false;
                }
                else if (comp_load_cap < mem.MaxCompForce.Force)
                {
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN < {1:f3} kN,  Maximum Group [{2}] Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Comp = false;
                }

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Compressive Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/sq.mm", Math.Abs(mem.MaxCompForce.Stress), Math.Abs(mem.MaxCompForce.Stress / 1000.0)));
                mem.DesignReport.Add(string.Format(""));
                if ((sigma) > Math.Abs(mem.MaxCompForce.Stress / 1000.0))
                {
                    //mem.DesignReport.Add(string.Format("Maximum Compressive Stress = {0:f3} N/sq.mm", mem.MaxStress/1000.0));
                    mem.Compressive_Stress = Math.Abs(mem.MaxCompForce.Stress / 1000.0);
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Compressive Stress OK", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                    //mem.Result = "OK";
                    //Check_Comp = true;
                }
                else
                {
                    //mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress   NOT OK", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                    //mem.Result = "NOT OK";


                    if (!Check_Comp)
                    {
                        Check_Comp = false;
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress   NOT OK", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                        mem.Result = "NOT OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                    }
                }

                if (n > 1 || mem.SectionDetails.LateralSpacing > 0)
                    DesignLacing(sw, mem, lamda);
                #endregion Compression
            }
            else
            {
                Check_Comp = true;
            }
            if (mem.MaxTensionForce.Force != 0.0)
            {
                #region Tensile
                //mem.DesignReport.Add(string.Format(""));
                //mem.DesignReport.Add(string.Format("Tensile Load Carrying Capacity = "));
                double tensile_load_cap = (Anet * ft) / 1000.0;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("------------------------------"));
                mem.DesignReport.Add(string.Format("TENSILE LOAD CARRYING CAPACITY"));
                mem.DesignReport.Add(string.Format("------------------------------"));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.Capacity_TensionForce = tensile_load_cap;


                if (mem.MaxTensionForce.Stress == 0.0)
                {
                    mem.MaxTensionForce.Stress = mem.MaxTensionForce.Force / (Anet / 1000000);
                }
                if (mem.MaxTensionForce.MemberNo != 0 && mem.MaxTensionForce.MemberNo != 0)
                {

                    mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN    [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce.Force, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("TENSILE STRESS = {0:f3} kN/m^2    [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce.Stress, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                }
                else
                {
                    mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN ", mem.MaxTensionForce.Force, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("TENSILE STRESS = {0:f3} kN/m^2 ", mem.MaxTensionForce.Stress, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                }


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
                    Check_Tens = true;
                }
                else if (tensile_load_cap.ToString("f3") == mem.MaxTensionForce.Force.ToString("f3"))
                {
                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Tensile Force NOT OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Tens = false;
                }
                else if (tensile_load_cap < mem.MaxTensionForce.Force)
                {
                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN < {1:f3} kN, Maximum Group [{2}] Tensile Force, NOT OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Tens = false;
                }

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Tensile Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/sq.mm", mem.MaxTensionForce.Stress, (mem.MaxTensionForce.Stress / 1000.0)));
                mem.DesignReport.Add(string.Format(""));
                if ((ft) > (mem.MaxTensionForce.Stress / 1000.0))
                {
                    //mem.DesignReport.Add(string.Format("Maximum Compressive Stress = {0:f3} N/sq.mm", mem.MaxStress/1000.0));
                    mem.Tensile_Stress = (mem.MaxTensionForce.Stress / 1000.0);
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Tensile Stress  OK", mem.MaxTensionForce.Stress / 1000.0, ft));
                    //mem.Result = "OK";

                    //Check_Tens = true;
                }
                else
                {
                    if (flag)
                    {
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress", mem.MaxTensionForce.Stress / 1000.0, ft));
                    }
                    else
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress  NOT OK", mem.MaxTensionForce.Stress / 1000.0, ft));


                    Check_Tens = flag;
                    mem.Result = flag ? "  OK" : "NOT OK";
                }


                //if (!Check_Tens && !Check_Comp)
                //{
                //    mem.Result = "NOT OK";
                //}
                //else
                //    mem.Result = "OK";


                DesignConnection(sw, mem);

                mem.DesignReport.Add(string.Format(""));


                #endregion Tensile
            }
            else
            {
                Check_Tens = true;
            }

            if (Check_Tens && Check_Comp)
            {
                mem.Result = "OK";
            }
            else
                mem.Result = "NOT OK";

            //sw.Write(mem.DesignReport.ToArray());

            _SWWrite:
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            foreach (string s in mem.DesignReport) sw.WriteLine(s);
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
        }


        void ShowMemberNos(string group_name)
        {

            string kStr = "";
            MemberGroup mGrp = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(group_name);
            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                
            if (mGrp != null)
            {
                txt_cd_mem_no.Text = mGrp.MemberNosText;
            }
        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //load_lst.Add("TYPE 1 CLA 1.179");
            //load_lst.Add("TYPE 2 CLB 1.188");
            //load_lst.Add("TYPE 3 A70RT 1.10");
            //load_lst.Add("TYPE 4 CLAR 1.179");
            //load_lst.Add("TYPE 5 A70RR 1.188");
            //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
            //load_lst.Add("TYPE 7 AASHTO_LFRD_HL93_H20_TRUCK 1.25");
            //load_lst.Add("TYPE 8 AASHTO_LFRD_HL93_HS20_TRUCK 1.25");
            //load_lst.Add("TYPE 9 AASHTO_LFRD_HTL57_TRUCK 1.25");
            //load_lst.Add("TYPE 10 BG_RAIL_1 1.9");
            //load_lst.Add("TYPE 11 BG_RAIL_2 1.90");
            //load_lst.Add("TYPE 12 MG_RAIL_1 1.90");
            //load_lst.Add("TYPE 13 MG_RAIL_2 1.90");
            //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = new MyList(MyList.RemoveAllSpaces(cmb_custom_LL_type.Text.ToUpper()), ':');
            string load_type = mlist.StringList[0].Trim().TrimEnd();

            foreach (var item in lst_load_data)
            {
                if (item.TypeNo == load_type)
                {
                    vehicle_width = item.LoadWidth;
                    break;
                }
            }


            if (rbtn_custom_LL.Checked == false)
            {


                load_lst.Add("LOAD GENERATION " + txt_custom_LL_load_gen.Text);
                lat_clrns = MyList.StringToDouble(txt_custom_LL_lat_clrns.Text, 0.5);
                total_lanes = MyList.StringToInt(cmb_custom_LL_lanes.Text, 1);
                xincr = MyList.StringToDouble(txt_custom_LL_Xcrmt.Text, 0.5);
                z = lat_clrns;

                for (int i = 0; i < total_lanes; i++)
                {
                    x = -Truss_Analysis.Analysis.Length;
                    y = 0;
                    z = (i + 1) * lat_clrns + i * vehicle_width;

                    //TYPE 6  -60.000 0 1.000 XINC 0.5
                    //load_lst.Add(string.Format("TYPE 6  -60.000 0 1.000 XINC 0.5"));
                    load_lst.Add(string.Format("{0}  {1} 0 {2} XINC {3}", load_type, x, z, xincr));
                }


                calc_width = lat_clrns * (total_lanes + 1) + vehicle_width * total_lanes;
            }
            else
            {


                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                LoadReadFromGrid();

                foreach (LoadData ld in LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }

                load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
                //load_lst.Add("TYPE 7  -69.500 0 1.000 XINC 0.5");

                foreach (LoadData ld in LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }

            }
            if (calc_width > Truss_Analysis.Analysis.Width)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Truss_Analysis.Analysis.Width;

                str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }

        public eMemberType Get_MemberType(string groupName)
        {
            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                
            #region For Arch Members Chiranjit [2014 01 20]
            if (groupName.StartsWith("_AR"))
                return eMemberType.ArchMembers;
            if (groupName.StartsWith("_ICAB") || groupName.StartsWith("_OCAB"))
                return eMemberType.SuspensionCables;
            if (groupName.StartsWith("_TRANS") || groupName.StartsWith("_TRNS"))
                return eMemberType.TransverseMember;
            if (groupName.StartsWith("_STRIN"))
                return eMemberType.StringerBeam;
            if (groupName.StartsWith("_XGIR"))
                return eMemberType.CrossGirder;
            if (groupName.StartsWith("_BCB"))
                return eMemberType.BottomChordBracings;



            #endregion Chiranjit [2014 01 20]

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
                case "_L5L6":
                case "_L6L7":
                case "_L7L8":
                case "_L8L9":
                case "_L9L10":
                case "_L10L11":
                case "_L11L12":
                case "_L12L13":
                case "_L13L14":
                case "_L14L15":
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
                case "_U5U6":
                case "_U6U7":
                case "_U7U8":
                case "_U8U9":
                case "_U9U10":
                case "_U11U12":
                case "_U12U13":
                case "_U13U14":
                case "_U14U15":
                case "_U15U16":
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
                case "_L10U10":
                case "_L11U11":
                case "_L12U12":
                case "_L13U13":
                case "_L14U14":
                case "_L15U15":
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

                case "_M1U1":
                case "_M2U2":
                case "_M3U3":
                case "_M4U4":
                case "_M5U5":
                case "_M6U6":
                case "_M7U7":
                case "_M8U8":
                case "_M9U9":
                case "_M10U10":
                case "_U1M1":
                case "_U2M2":
                case "_U3M3":
                case "_U4M4":
                case "_U5M5":
                case "_U6M6":
                case "_U7M7":
                case "_U8M8":
                case "_U9M9":
                case "_U10M10":
                    return eMemberType.TopVerticalMember;


                case "_M1L1":
                case "_M2L2":
                case "_M3L3":
                case "_M4L4":
                case "_M5L5":
                case "_M6L6":
                case "_M7L7":
                case "_M8L8":
                case "_M9L9":
                case "_M10L10":
                case "_L1M1":
                case "_L2M2":
                case "_L3M3":
                case "_L4M4":
                case "_L5M5":
                case "_L6M6":
                case "_L7M7":
                case "_L8M8":
                case "_L9M9":
                case "_L10M10":
                    return eMemberType.BottomVerticalMember;



                case "_M1L2":
                case "_M2L3":
                case "_M3L4":
                case "_M4L5":
                case "_M5L6":
                case "_M6L7":
                case "_M7L8":
                case "_M8L9":
                case "_M9L10":
                case "_M10L11":
                case "_M11L12":
                case "_M12L13":
                case "_M13L14":

                case "_L2M1":
                case "_L3M2":
                case "_L4M3":
                case "_L5M4":
                case "_L6M5":
                case "_L7M6":
                case "_L8M7":
                case "_L9M8":
                case "_L10M9":
                case "_L11M10":
                case "_L12M11":
                case "_L13M12":
                case "_L14M13":
                    return eMemberType.BottomDiagonalMember;

                case "_M1U2":
                case "_M2U3":
                case "_M3U4":
                case "_M4U5":
                case "_M5U6":
                case "_M6U7":
                case "_M7U8":
                case "_M8U9":
                case "_M9U10":
                case "_M10U11":
                case "_M11U12":
                case "_M12U13":
                case "_M13U14":

                case "_U2M1":
                case "_U3M2":
                case "_U4M3":
                case "_U5M4":
                case "_U6M5":
                case "_U7M6":
                case "_U8M7":
                case "_U9M8":
                case "_U10M9":
                case "_U11M10":
                case "_U12M11":
                case "_U13M12":
                case "_U14M13":
                    return eMemberType.TopDiagonalMember;


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
                case "_L5U4":
                case "_L6U5":
                case "_L7U6":
                case "_L8U7":
                case "_L9U8":
                case "_L10U9":
                case "_L11U10":
                case "_L12U11":
                case "_L13U12":
                case "_L14U13":
                case "_L15U14":
                case "_L16U15":

                    return eMemberType.DiagonalMember;
                    break;
                case "_TCS_ST":
                case "_TCS_ST1":
                case "_TCS_ST2":
                case "_TCS_ST3":
                case "_TCS_ST4":
                case "_TCS_ST5":
                case "_TCS_ST6":
                    return eMemberType.TopChordBracings;
                case "_TCS_DIA":
                case "_TCS_DIA1":
                case "_TCS_DIA2":
                case "_TCS_DIA3":
                case "_TCS_DIA4":
                case "_TCS_DIA5":
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
                case "_STRINGERS":
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
                case "_CROSSGIRDER":
                case "_CROSSGIRDERS":
                case "_XGIRDER":
                case "_XGIRDERS":
                case "_XGIR1":
                case "_XGIR2":
                case "_XGIR3":
                case "_XGIR4":
                case "_XGIR5":
                case "_XGIR6":
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

                w = MyList.StringToDouble(txt_cgw.Text, 0.0);
                //d = MyList.StringToDouble(txt_d.Text, 0.0);
                fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                m = MyList.StringToDouble(txt_m.Text, 0.0);

                sw.WriteLine();
                sw.WriteLine("-------------------------");
                sw.WriteLine("DESIGN OF SHEAR CONNECTOR");
                sw.WriteLine("-------------------------");
                sw.WriteLine();
                sw.WriteLine("INPUT DATA");
                sw.WriteLine("-----------");
                sw.WriteLine();

                sw.WriteLine("Shear Force = V = {0} kN = {0} * 10^3 N", V);
                sw.WriteLine("Moment of Inertia about X-X axis  = Ixx = {0:f3} sq.sq.cm", Ixx);
                if (a != 0.0)
                    sw.WriteLine("Area = a =  {0:f3} sq.cm", a);
                if (D != 0.0)
                    sw.WriteLine("Depth of Girder = D = {0} mm", D);
                if (B != 0.0)
                    sw.WriteLine("Flange Width = B = {0} mm", B);
                if (tw != 0.0)
                    sw.WriteLine("Web Thickness = tw = {0} mm", tw);
                if (tf != 0.0)
                    sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
                if ((bs * ts) != 0.0)
                    sw.WriteLine("Side Plates = bs x ts = {0} x {1}", bs, ts);
                if ((bp * tp) != 0.0)
                    sw.WriteLine("Bottom Plate = bp x tp = {0} x {1}", bp, tp);
                sw.WriteLine();
                if (w != 0.0)
                    sw.WriteLine("Spacing of Cross Girders = w = {0} mm", w);
                if (d != 0.0)
                    sw.WriteLine("Thickness of Deck Slab = d = {0} mm", d);
                if (fck != 0.0)
                    sw.WriteLine("Concrete Grade = fck = M {0}", fck);
                if (m != 0.0)
                    sw.WriteLine("Modular Ratio = m = {0}", m);
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

                double sr = Qu / Ty;
                sw.WriteLine("Spacing Required = Qu / τy = {0:f3} / {1:f2} = {2:f2} mm", Qu, Ty, sr);
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
            sw.WriteLine("Design of Lacing for Compressive force");
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            sw.WriteLine("INPUT DATA");
            sw.WriteLine("-----------");
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
                sw.WriteLine("       Iy = {0:f3} sq.sq.mm.", Iy);
                sw.WriteLine();
            }
            else
            {
                sw.WriteLine("Area     a = bl * tl = {0} * {1} = {2} sq.mm.", lac_bl, lac_tl, a);
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
                B = tabData.Length_1;
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
            double rss = 0.0, rvt_val = 0.0, safe_load = 0.0;
            //bool flag = false;
            double[] rvt_dia = new double[] { 16, 18, 20, 22, 24, 25, 26, 30, 32, 36, 38, 40 };
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
                    sw.WriteLine("two values = {0:f3} N. < Load in the Lacing Plate = T = {1:f3} N   So, NOT OK, Larger Diameter Rivet is Required.", safe_load, T);
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
            sw.WriteLine("Design of Connection for Tensile force");
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("INPUT DATA");
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
                sw.WriteLine("TABLE 1 : E.U.D.L., C.D.A. and longitudinal Loads for Modified B.G. Loading");
                sw.WriteLine("---------------------------------------------------------------------------");
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
                sw.WriteLine("TABLE 2 : Allowable Working Stress σbc for different Values of Critical Stress");
                sw.WriteLine("-------------------------------------------------------------------------------");
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
                sw.WriteLine("TABLE 3 : Allowable Average Shear Stress in Stiffened Webs of Steel");
                sw.WriteLine("-------------------------------------------------------------------");
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
                sw.WriteLine("TABLE 4 : Allowable Working Stress σac in N/mm2 on Effective Cross Section for Axial Compression");
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
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

        void WriteTable5(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "Truss_Bridge_4.txt");

            List<string> file_cont = iApp.Tables.Get_Permissible_Stress_Axial_Compression();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 5 : Permissible Stress sigma_ac (MPa) in Axial Compression for Steel with Various Yield Stess");
                sw.WriteLine("----------------------------------------------------------------------------------------------------");
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
        void WriteTable6(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "Truss_Bridge_4.txt");

            List<string> file_cont = iApp.Tables.Get_Permissible_Bending_Compressive_Stresses();

            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("TABLE 6 : Maximum Permissible Compressive Bending Stresses, sigma_bc (MPa) in Equal Flange I-Beams of Channels");
                sw.WriteLine("----------------------------------------------------------------------------------------------------------------");
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
                dgv_live_load.Rows.Add(cmb_load_type.Text, txt_X.Text, txt_Y.Text, txt_Z.Text, txt_XINCR.Text, txt_Load_Impact.Text);
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
                    ld.Code = mlist.StringList[1];
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -18.8);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
                    ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                    ld.ImpactFactor = MyList.StringToDouble(dgv_live_load[5, i].Value.ToString(), 0.5);
                    LoadList.Add(ld);
                }
                catch (Exception ex) { }
            }
        }

        public void FillMemberResult()
        {
            try
            {
                dgv_member_Results_arch_cables.Rows.Clear();
                dgv_member_Results_others.Rows.Clear();
                //design_member = (eMemberType)(cmb_design_member)
                double tens = 0.0, comp = 0.0;
                double axial = 0.0;

                design_member = (eMemberType)(cmb_design_member.SelectedIndex);
                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    if (design_member == Complete_Design.Members[i].MemberType || design_member == eMemberType.AllMember)
                    {
                        if ((Complete_Design.Members[i].MemberType == eMemberType.CrossGirder) ||
                            (Complete_Design.Members[i].MemberType == eMemberType.StringerBeam) ||
                            (Complete_Design.Members[i].MemberType == eMemberType.ArchMembers) ||
                            (Complete_Design.Members[i].MemberType == eMemberType.TransverseMember))
                        {
                            comp = 0.0;
                            tens = 0.0;
                        }
                        else
                        {
                            comp = Complete_Design.Members[i].MaxCompForce.Force;
                            tens = Complete_Design.Members[i].MaxTensionForce.Force;
                        }

                        string kStr = "";

                        if (Complete_Design.Members[i].MemberType == eMemberType.ArchMembers ||
                            Complete_Design.Members[i].MemberType == eMemberType.TransverseMember)
                        {

                            axial = Complete_Design.Members[i].MaxAxialForce;

                            if(axial.ToString("f3") == "0.000")
                                axial = Complete_Design.Members[i].MaxShearForce;

                            dgv_member_Results_arch_cables.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                                Complete_Design.Members[i].MemberType.ToString(),
                                axial.ToString("f3"),
                                Complete_Design.Members[i].MaxBendingMoment.ToString(),
                                Complete_Design.Members[i].MaxTorsion.ToString(),
                                Complete_Design.Members[i].Secondary_Stress_Ratio.ToString("f3"),
                                Complete_Design.Members[i].Secondary_Stress_Ratio_Capacity.ToString("f3"),
                                Complete_Design.Members[i].Result_Secondary_Stress_Ratio,
                                "",
                               "",
                                "",
                                "",
                                "",

                                //0.0, //Secondary Stress Ratio
                                //1.0,//Secondary Stress Ratio Capacity
                                //"OK",//Secondary Stress Ratio Resul

                                //tens.ToString("f3"),
                                //0.0, //Breaking Force Capacity
                                //1.0,//Breaking Force Result
                                //1.0,//Permissible Breaking Force
                                //1.0,//Permissible Breaking Force Result
                                //"OK",//Secondary Stress Ratio Resul
                                Complete_Design.Members[i].Result);
                        }
                        else if (Complete_Design.Members[i].MemberType == eMemberType.SuspensionCables)
                        {

                            dgv_member_Results_arch_cables.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                                Complete_Design.Members[i].MemberType.ToString(),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                tens.ToString("f3"),
                                Complete_Design.Members[i].Breaking_Force_Capacity.ToString("f3"),
                                Complete_Design.Members[i].Result_Breaking_Force,
                                Complete_Design.Members[i].Permissible_Breaking_Force_Capacity.ToString("f3"),
                                Complete_Design.Members[i].Result_Permissible_Breaking_Force,

                                //0.0, //Secondary Stress Ratio
                                //1.0,//Secondary Stress Ratio Capacity
                                //"OK",//Secondary Stress Ratio Resul

                                //tens.ToString("f3"),
                                //0.0, //Breaking Force Capacity
                                //1.0,//Breaking Force Result
                                //1.0,//Permissible Breaking Force
                                //1.0,//Permissible Breaking Force Result
                                //"OK",//Secondary Stress Ratio Resul
                                Complete_Design.Members[i].Result);
                        }
                        else
                        {
                            dgv_member_Results_others.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                                Complete_Design.Members[i].MemberType.ToString(),

                                comp.ToString("f3"),
                                Complete_Design.Members[i].Capacity_CompForce.ToString("f3"),

                                Complete_Design.Members[i].Result_Compressive,

                                tens.ToString("f3"),
                                Complete_Design.Members[i].Capacity_TensionForce.ToString("f3"),
                                Complete_Design.Members[i].Result_Tensile,
                                Complete_Design.Members[i].MaxBendingMoment.Force.ToString("f3"),
                                Complete_Design.Members[i].Required_SectionModulus.ToString("E3"),
                                Complete_Design.Members[i].Capacity_SectionModulus.ToString("E3"),
                                Complete_Design.Members[i].MaxShearForce.Force.ToString("f3"),
                                Complete_Design.Members[i].Capacity_ShearStress.ToString("f3"),
                                Complete_Design.Members[i].Required_ShearStress.ToString("f3"),
                                Complete_Design.Members[i].Result);
                        }
                    }
                }
                Write_Results_others();
                Write_Results_Arches_cables();
                FillResultGridWithColor();
                FillResultGridWithColor(dgv_member_Results_arch_cables);
            }
            catch (Exception ex) { }


        }
        public void FillResultGridWithColor()
        {
            try
            {
                
                for (int i = 0; i < dgv_member_Results_others.RowCount; i++)
                {
                    for (int j = 0; j < dgv_member_Results_others.ColumnCount; j++)
                    {
                        if (dgv_member_Results_others[j, i].Value.ToString() == "0.000" ||
                            dgv_member_Results_others[j, i].Value.ToString() == "0.000E+000")
                            dgv_member_Results_others[j, i].Value = "";
                    }
                     
                    if (dgv_member_Results_others[dgv_member_Results_others.ColumnCount -1, i].Value.ToString().ToUpper() == "NOT OK")
                    {
                        SetGroupResultColor(dgv_member_Results_others[0, i].Value.ToString(), Color.Red);
                        dgv_member_Results_others.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    if (dgv_member_Results_others[dgv_member_Results_others.ColumnCount - 1, i].Value.ToString().ToUpper().Trim().TrimEnd().TrimStart() == "OK")
                    {
                        SetGroupResultColor(dgv_member_Results_others[0, i].Value.ToString(), Color.Green);
                        dgv_member_Results_others.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception xe) { }
        }
        public void FillResultGridWithColor(DataGridView dgv)
        {
            try
            {

                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        if (dgv[j, i].Value.ToString() == "0.000" ||
                            dgv[j, i].Value.ToString() == "0.000E+000")
                            dgv[j, i].Value = "";
                    }

                    if (dgv[dgv.ColumnCount - 1, i].Value.ToString().ToUpper() == "NOT OK")
                    {
                        SetGroupResultColor(dgv[0, i].Value.ToString(), Color.Red);
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    if (dgv[dgv.ColumnCount - 1, i].Value.ToString().ToUpper().Trim().TrimEnd().TrimStart() == "OK")
                    {
                        SetGroupResultColor(dgv[0, i].Value.ToString(), Color.Green);
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
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

        public void Read_Results_others()
        {

            string kFile = Path.Combine(system_path, "result_othr.csv");
            if (!File.Exists(kFile))
            {
                kFile = Path.Combine(system_path, "result_othr.txt");
            }
            if (!File.Exists(kFile)) return;
            List<string> list = new List<string>(File.ReadAllLines(kFile));
            MyList mlist = null;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_member_Results_others.Rows.Add(mlist.StringList.ToArray());
            }
        }
        public void Write_Results_others()
        {

            string kFile = Path.Combine(system_path, "result_othr.csv");
            string kStr = "";

            StreamWriter sw = new StreamWriter(new FileStream(kFile, FileMode.Create));
            try
            {
                for (int i = 0; i < dgv_member_Results_others.RowCount; i++)
                {

                    kStr = "";
                    try
                    {
                        for (int j = 0; j < dgv_member_Results_others.ColumnCount; j++)
                        {
                            if (j == dgv_member_Results_others.ColumnCount - 1)
                                kStr += dgv_member_Results_others[j, i].Value.ToString();
                            else
                                kStr += dgv_member_Results_others[j, i].Value.ToString() + ",";
                        }
                    }
                    catch (Exception eee) { }
                    sw.WriteLine(kStr);


                    //sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                    //    dgv_member_Result[0, i].Value.ToString(),//Group
                    //    dgv_member_Result[1, i].Value.ToString(),//Type
                    //    dgv_member_Result[2, i].Value.ToString(),//Comp
                    //    dgv_member_Result[3, i].Value.ToString(),//Tens
                    //    dgv_member_Result[4, i].Value.ToString(),//Moment
                    //    dgv_member_Result[5, i].Value.ToString(),//Shear
                    //    dgv_member_Result[6, i].Value.ToString(),//Shear
                    //    dgv_member_Result[7, i].Value.ToString(),//Shear
                    //    dgv_member_Result[8, i].Value.ToString(),//Shear
                    //    dgv_member_Result[9, i].Value.ToString(),//Shear
                    //    dgv_member_Result[10, i].Value.ToString(),//Shear
                    //    dgv_member_Result[11, i].Value.ToString(),//Shear
                    //    dgv_member_Result[12, i].Value.ToString());//Result
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Read_Results_Arches_cables()
        {

            string kFile = Path.Combine(system_path, "result_arch_cab.csv");
            if (!File.Exists(kFile))
            {
                kFile = Path.Combine(system_path, "result_arch_cab.txt");
            }
            if (!File.Exists(kFile)) return;
            List<string> list = new List<string>(File.ReadAllLines(kFile));
            MyList mlist = null;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_member_Results_arch_cables.Rows.Add(mlist.StringList.ToArray());
            }
        }
        public void Write_Results_Arches_cables()
        {

            string kFile = Path.Combine(system_path, "result_arch_cab.csv");
            string kStr = "";

            StreamWriter sw = new StreamWriter(new FileStream(kFile, FileMode.Create));
            try
            {
                for (int i = 0; i < dgv_member_Results_arch_cables.RowCount; i++)
                {

                    kStr = "";
                    try
                    {
                        for (int j = 0; j < dgv_member_Results_arch_cables.ColumnCount; j++)
                        {
                            if (j == dgv_member_Results_arch_cables.ColumnCount - 1)
                                kStr += dgv_member_Results_arch_cables[j, i].Value.ToString();
                            else
                                kStr += dgv_member_Results_arch_cables[j, i].Value.ToString() + ",";
                        }
                    }
                    catch (Exception eee) { }
                    sw.WriteLine(kStr);
                     
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

                txt_XINCR.Text = mlist.StringList[4];
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
                string s = "";
                for (int i = 0; i < dgv_live_load.RowCount; i++)
                {

                    sw.WriteLine("{0},{1},{2},{3},{4},{5}",
                        dgv_live_load[0, i].Value.ToString(),//Load Type
                        dgv_live_load[1, i].Value.ToString(),//X
                        dgv_live_load[2, i].Value.ToString(),//Y
                        dgv_live_load[3, i].Value.ToString(),//Z
                        dgv_live_load[4, i].Value.ToString(),//XINC
                        dgv_live_load[5, i].Value.ToString());//Impact Factor
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section1;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    break;
                case 1:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section2;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    break;
                case 2:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section3;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 3:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section4;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 4:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section5;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 5:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section6;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 6:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section7;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Top_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 7:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section8;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 8:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section9;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section9;
                    cmb_sec_thk.Visible = true;
                    cmb_section_name.Items.Clear();
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    break;
                case 9:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section10;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section10;
                    cmb_sec_thk.Visible = false;

                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";
                    break;
                case 10:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section11;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section111;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "2";
                    break;
                case 11:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section12;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section12;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";
                    break;
                //Chiranjit [2011 05 17]
                case 12:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section13;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section13;
                    cmb_sec_thk.Visible = true;

                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    break;
                //Chiranjit [2011 05 18]
                case 13:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section14;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section14;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                //Chiranjit [2011 05 18]
                case 14:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section15;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
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
            string drwg_path = "Arch_Suspension_Drawings";

            //if (Directory.Exists(drwg_path))
            //{
            iApp.RunViewer(Path.Combine(Drawing_Folder, "Arch Bridge Drawings"), drwg_path);
            //iApp.RunViewer(drwg_path);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (File.Exists(rep_file_name))
                iApp.RunExe(rep_file_name);
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 07 13]
            Write_All_Data();
            //Chiranjit [2012 07 06]
            if (!File.Exists(txt_analysis_file.Text))
            {
                MessageBox.Show(this, "The Analysis Input data File is not created. \n\n" +
                                    "In Tab 'Structure Geometry' the button 'Create Analysis Input data File' " +
                                    "is to be used for creating the Analysis Input data\",",
                                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //tabControl1.SelectedTab = tab_GD;
                return;
            }

            string mem_grp = "";



            try
            {
                mem_grp = dgv_member_Results_others[0, dgv_member_Results_others.CurrentRow.Index].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Analysis Report was not found.", "ASTRA");
                return;
            }
            if (dgv_mem_details.RowCount == 0)
            {
                MessageBox.Show(this, "No Member found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(this, "This Process might take few minuites.\n Do you want to continue ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                iApp.Progress_Works.Clear();
                iApp.Progress_Works.Add("Member Analysis and Design");

                Write_User_Input();
                InitializeData();
                ReadDeadLoadInputs();
                if (chk_edit_forces.Checked)
                    Update_Forces();
                Calculate_Program();
                FillMemberResult();
                Set_Force_Input_Color();
                if (File.Exists(rep_file_name))
                {
                    if (MessageBox.Show(this, "Report file written in file " + rep_file_name + "\n\n Do you want to open the report file?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        iApp.View_Result(rep_file_name);
                }
                iApp.Progress_Works.Clear();
            }
            string kStr = "";
            for (int i = 0; i < dgv_member_Results_others.RowCount; i++)
            {
                kStr = dgv_member_Results_others[0, i].Value.ToString();
                if (kStr == mem_grp)
                {
                    dgv_member_Results_others.Rows[i].Selected = true;
                }
            }
            prss = 4;
            Button_Enable_Disable();
        }

        private void frm_Arch_Cable_Bridge_Load(object sender, EventArgs e)
        {
            //pic_pier_interactive_diagram.BackgroundImage = ImageCollection.Pier_drawing;
            txt_fck.SelectedIndex = 3;

            IsWarren2 = false;
            pnl_extra_input.Visible = !IsWarren2;

            pic_pier_interactive_diagram.BackgroundImage = global::AstraFunctionOne.ImageCollection.Pier_Box_drawing;
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;
            cmb_panel_nos.SelectedIndex = 3;

            rbtn_create_analysis_file.Checked = false;
            rbtn_create_analysis_file.Checked = true;
            rbtn_create_analysis_file.Location = new Point(rbtn_select_analysis_file.Location.X, rbtn_create_analysis_file.Location.Y);

            SetComboSections();
            cmb_sections_define.SelectedIndex = 0;
            cmb_design_member.SelectedIndex = (int)eMemberType.AllMember;
            complete_design = new CompleteDesign();
            cmb_Shr_Con_Section_name.Items.Clear();

            if(iApp.DesignStandard == eDesignStandard.IndianStandard)
                cmb_select_standard.SelectedIndex = 1;
            else
                cmb_select_standard.SelectedIndex = 0;

            cmb_Shr_Con_Section_name.Items.AddRange(tbl_rolledSteelChannels.Get_Channels().ToArray());


            if (cmb_Shr_Con_Section_name.Items.Count > 0)
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    cmb_Shr_Con_Section_name.SelectedItem = "ISMC";
                    cmb_Shr_Con_Section_Code.SelectedItem = "150";
                }
                else
                {
                    cmb_Shr_Con_Section_name.SelectedIndex = 0;
                    cmb_Shr_Con_Section_Code.SelectedIndex = 0;
                }
            }


            cmb_lac.SelectedIndex = 0;

            if (iApp.LiveLoads.Count > 0)
                iApp.LiveLoads.Fill_Combo(ref cmb_load_type);

            cmb_deck_fck.SelectedIndex = 2;
            cmb_deck_fy.SelectedIndex = 1;

            cmb_abut_fck.SelectedIndex = 2;
            cmb_abut_fy.SelectedIndex = 1;

            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;

            cmb_pier_2_k.SelectedIndex = 1;

            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 1;

            tmr_blink.Start();

            //Open_Project();

            Set_Project_Name();

        }

        private void Open_Project()
        {

            //Chiranjit [2014 10 06]
            #region Chiranjit Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                isCreateData = false;

                Deck = new SteelTrussDeckSlab(iApp);
                Abut = new RCC_AbutmentWall(iApp);
                rcc_pier = new RccPier(iApp);

                Abut.FilePath = user_path;
                Deck.FilePath = user_path;
                rcc_pier.FilePath = user_path;
                    string chk_file = "";


                    //Read_All_Data();
                    dgv_member_Results_arch_cables.Rows.Clear();
                    dgv_member_Results_others.Rows.Clear();
                    dgv_mem_details.Rows.Clear();

                    txt_analysis_file.Text = Path.Combine(user_path, "INPUT_DATA.TXT");
                    OpenAnalysisFile(txt_analysis_file.Text);

                    prss = 1;
                //}


                Button_Enable_Disable();
                Show_Total_Weight();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option
        }

        private void cmb_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
            grb_bottom_plate.Visible = true;
            grb_Top_plate.Visible = true;
            grb_vertical_stiffener_plate.Visible = true;

            if (cmb_sections_define.SelectedIndex == 12 || cmb_sections_define.SelectedIndex == 11)
            {
                grb_side_plate.Text = "WEB PLATE";
                grb_bottom_plate.Visible = false;
                grb_Top_plate.Visible = false;
                grb_vertical_stiffener_plate.Visible = false;
            }
            else if (cmb_sections_define.SelectedIndex == 13)
            {
                grb_side_plate.Text = "WEB PLATE";
                grb_bottom_plate.Visible = false;
                grb_Top_plate.Visible = false;
                grb_vertical_stiffener_plate.Visible = false;
            }
            else
            {
                grb_side_plate.Text = "SIDE PLATE";

            }
            if (cmb_sections_define.SelectedIndex == 15)
            {
                pnl_truss.Visible = false;
                grb_cables.Visible = true;
                grb_cables.Location = new Point(pnl_truss.Location.X, pnl_truss.Location.Y);
                lbl_cab_thk.Visible = true;
                txt_cab_thk.Visible = true;
            }
            else if (cmb_sections_define.SelectedIndex > 15)
            {
                pnl_truss.Visible = false;
                grb_cables.Visible = true;
                grb_cables.Location = new Point(pnl_truss.Location.X, pnl_truss.Location.Y);
                lbl_cab_thk.Visible = false;
                txt_cab_thk.Visible = false;
            }
            else
            {
               
                pnl_truss.Visible = true;
                grb_cables.Visible = false;
            }
            SetComboSections();


            switch (cmb_sections_define.SelectedIndex)
            {
                #region Truss Section
                case 0:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section1;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section2;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section3;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section4;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section5;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section6;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section7;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Top_Chord_Bracing;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section8;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section9;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section9;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section10;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section10;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section11;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section111;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section12;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section12;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section13;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section13;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section14;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section14;
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
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section15;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
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
                //Chiranjit [2011 05 18]
                case 15:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Cable_Section16;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISMC");
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_cab_dia.Text = "1100";
                    txt_cab_thk.Text = "16";
                    break;
                case 16:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Cable_Section17;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISMC");
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    
                    txt_cab_dia.Text = "42";
                    txt_cab_thk.Text = "0";
                    break;
                #endregion Truss Section
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
                int le = MyList.StringToInt(cmb_code1.Text, 0);
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
            if ((sender as DataGridView).Name == dgv_member_Results_others.Name)
            {

                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    if (dgv_mem_details[0, i].Value.ToString().ToUpper() == (dgv_member_Results_others[0, e.RowIndex].Value.ToString().ToUpper()))
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
        private void btn_update_Click(object sender, EventArgs e)
        {
            //Data_Convert_and_Update_IS_TO_BS();
            //return;

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
                //Show_Total_Weight();
                //MessageBox.Show(m.Group.GroupName + " updated.", "ASTRA");
                timer1.Stop();
                MessageBox.Show(this, "All Member Sections are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                prss = 1;
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

        //Chiranjit [2012 02 09]
        private void Data_Convert_and_Update_IS_TO_BS()
        {
            try
            {
                #region Convert Data
                SectionData secData = null;

                for (int i = 0; i < complete_design.Members.Count; i++)
                {
                    secData = complete_design.Members[i].SectionDetails;

                    if (secData.SectionName.StartsWith("IS"))
                    {
                        iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref secData);

                        //Chiranjit [2012 02 09]
                        switch (complete_design.Members[i].Group.GroupName.ToUpper())
                        {
                            case "_STRINGER":
                                {
                                    secData.SectionName = "UKB";
                                    secData.SectionCode = "533X312X150";
                                    secData.BottomPlate.Width = 180.0;
                                    secData.BottomPlate.Thickness = 12.0;
                                    secData.VerticalStiffenerPlate.Width = 490.0;
                                    secData.VerticalStiffenerPlate.Thickness = 12.0;

                                    secData.SidePlate.Width = 0.0;
                                    secData.SidePlate.Thickness = 0.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                }
                                break;
                            case "_U1U2":
                                {
                                    secData.SectionName = "UKA";
                                    secData.SectionCode = " 150X150";
                                    secData.AngleThickness = 12.0;
                                    secData.SidePlate.Width = 220.0;
                                    secData.SidePlate.Thickness = 12.0;


                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                            case "_L3U2":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "300X90X41";
                                    secData.SidePlate.Width = 220.0;
                                    secData.SidePlate.Thickness = 12.0;


                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;

                                }
                                break;
                            case "_L4U3":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "300X90X41";

                                    secData.SidePlate.Width = 0.0;
                                    secData.SidePlate.Thickness = 0.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                            case "_L5U4":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "200X75X23";

                                    secData.SidePlate.Width = 0.0;
                                    secData.SidePlate.Thickness = 0.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                            case "_L5U5":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "200X75X23";

                                    secData.SidePlate.Width = 180.0;
                                    secData.SidePlate.Thickness = 32.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 200.0;
                                    secData.BottomPlate.Thickness = 10.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                        }
                    }
                    AddMemberRow(complete_design.Members[i]);
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
                //Show_Total_Weight();
                //MessageBox.Show(m.Group.GroupName + " updated.", "ASTRA");
                timer1.Stop();
                //MessageBox.Show(this, "All Member Sections are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            txt_cd_mem_type.Text = cmb_cd_mem_type.Text;
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
                frm_Arch_Input_Files fin = new frm_Arch_Input_Files();
                fin.ShowDialog();
                string fn = Get_User_Input_File_Name(fin.Select_File_Index);

                iApp.View_Input_File(fn);

                //System.Diagnostics.Process.Start(fn);
                //iApp.RunExe(Path.Combine(Path.GetDirectoryName(fn), "LL.TXT"));
                
                //System.Diagnostics.Process.Start(Inpu);
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


        public string Analysis_Path
        {
            get
            {

                //if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, Title)))
                //    return Path.Combine(iApp.LastDesignWorkingFolder, Title);

                return iApp.user_path;

            }
        }

        private void btn_browse_analysis_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                ofd.InitialDirectory =  Analysis_Path;
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    isCreateData = false;
                    user_path = Path.GetDirectoryName(ofd.FileName);

                    Read_All_Data();
                    dgv_member_Results_arch_cables.Rows.Clear();
                    dgv_member_Results_others.Rows.Clear();
                    dgv_mem_details.Rows.Clear();
                    OpenAnalysisFile(ofd.FileName);

                    txt_analysis_file.Text = Path.Combine(user_path, "INPUT_DATA.TXT");
                    //FillMemberGroup();

                    //return;

                    //OpenAnalysisFile(ofd.FileName);
                    prss = 1;
                    //try
                    //{
                    //    string s1, s2;
                    //    s1 = s2 = "";
                    //    for (int j = 0; j < Truss_Analysis.Analysis.Supports.Count; j++)
                    //    {
                    //        if (j < Truss_Analysis.Analysis.Supports.Count / 2)
                    //            s1 += Truss_Analysis.Analysis.Supports[j].NodeNo + ",";
                    //        else
                    //            s2 += Truss_Analysis.Analysis.Supports[j].NodeNo + ",";
                    //    }

                    //    frm_ViewForces(Truss_Analysis.Analysis.Width, DL_Report_Analysis_File, LL_Report_Analysis_File, (s1 + s2));
                    //    frm_ViewForces_Load();

                    //    frm_Pier_ViewDesign_Forces(Total_Report_Analysis_File, s1, s2);
                    //    frm_ViewDesign_Forces_Load();

                    //}
                    //catch (Exception ex) { }
                }


            }
            //Read_All_Data();
            Button_Enable_Disable();
            Show_Total_Weight();
            //Chiranjit [2012 07 13]


        }

        List<List<CMember>> lst_all_groups = new List<List<CMember>>();

        private void btn_open_Analysis_Report_Click(object sender, EventArgs e)
        {
            string flPath = txt_analysis_file.Text;
            //Chiranjit [2012 07 13]
            Write_All_Data();


            if (!File.Exists(flPath))
            {
                MessageBox.Show(this, "The Analysis Input data File is not created. \n\n" +
                                    "In Tab 'Structure Geometry' the button 'Create Analysis Input data File' " +
                                    "is to be used for creating the Analysis Input data\",",
                                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //tabControl1.SelectedTab = tab_GD;
                return;
            }
            string load_file = Path.Combine(Path.GetDirectoryName(flPath), "MEMBER_LOAD_DATA.txt");

            if (!File.Exists(load_file))
            {
                MessageBox.Show(this, "The Load Data is not saved in the Analysis Input data File. \n\nThe button 'Save Section + Load data' is to be used to save Load Data in the Analysis Input data File",
                                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //tabControl1.SelectedTab = tab_GD;
                return;
            }

            //File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            ////System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            //System.Diagnostics.Process prs = new System.Diagnostics.Process();

            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            //System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            


            //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            //if (File.Exists(prs.StartInfo.FileName))
            //{
            //    if (prs.Start())
            //        prs.WaitForExit();
            //}
            //else
            //{
            //    MessageBox.Show(prs.StartInfo.FileName + " not found."); return;
            //}



            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();

            for (int i = 0; i < 5; i++)
            {
                flPath = Get_User_Input_File_Name(i);
                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);
            }




            string ana_rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            if (iApp.Show_and_Run_Process_List(pcol))
            {
                #region Chiranjit [2013 05 16]

                List<string> Work_List = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    Work_List.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(Get_User_Input_File_Name(i).Replace("_", " ")));
                }
                iApp.Progress_Works = new ProgressList(Work_List);
                #endregion Chiranjit [2013 05 16]

                List_Analysis = new List<BridgeMemberAnalysis>();
                for (int i = 0; i < 5; i++)
                {
                    ana_rep_file = MyList.Get_Analysis_Report_File(Get_User_Input_File_Name(i));
                    List_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file, GetForceType()));
                    if (iApp.Is_Progress_Cancel)
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        return;
                    }

                }
                if (iApp.Is_Progress_Cancel) return;

                Truss_Analysis = List_Analysis[0];
                List<string> list_node = new List<string>();

                if (Truss_Analysis.Node_Displacements != null)
                    list_node.Add(Truss_Analysis.Node_Displacements.Get_Max_Deflection().ToString());

                string kStr = Truss_Analysis.Analysis.Supports[0].NodeNo + " TO "
                                        + Truss_Analysis.Analysis.Supports[3].NodeNo;
                List<int> jnts = new List<int>();



                for (int j = 1; j < truss_data.Bottom_Chord1.Count; j++)
                {
                    //truss_data.
                    jnts.Add(truss_data.Bottom_Chord1[j].StartNode.NodeNo);
                    jnts.Add(truss_data.Bottom_Chord2[j].StartNode.NodeNo);
                }

                if (Truss_Analysis.Node_Displacements != null)
                {
                    for (int i = 0; i < jnts.Count; i++)
                    {
                        foreach (var item in Truss_Analysis.Node_Displacements)
                        {
                            if (item.NodeNo == jnts[i] && item.LoadCase == 1)
                            {
                                list_node.Add(item.ToString());
                            }
                        }
                    }
                    string file_load_def = Path.Combine(Analysis_Path, "MAX_LOAD_DEFLECTION.TXT");
                    File.WriteAllLines(file_load_def, list_node.ToArray());
                    txt_node_displace.Lines = list_node.ToArray();
                }
                iApp.Progress_Works.Clear();
            }
            else
                return;


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
                Read_Results_others();
                Read_Results_Arches_cables();
            }
            dgv_mem_details.Rows.Clear();
            #region Chiranjit [2014 01 25]
            if (true)
            {
                List<List<CMember>> lst_all_mbrs = new List<List<CMember>>();
                List<CMember> lst_mbrs = new List<CMember>();
                CMember mbrs = null;

                for (int f = 0; f < List_Analysis.Count; f++)
                {
                    lst_mbrs = new List<CMember>();
                    for (int i = 0; i < List_Analysis[f].Analysis.MemberGroups.GroupCollection.Count; i++)
                    {
                        mbrs = new CMember();
                        if (List_Analysis[f] != null)
                        {
                            //mbrs.Group = List_Analysis[f].Analysis.MemberGroups.GetMemberGroup(mbrs.Group.GroupName);
                            mbrs.Group = List_Analysis[f].Analysis.MemberGroups.GroupCollection[i];
                        }
                        mbrs.Force = List_Analysis[f].GetForce(ref mbrs);
                        lst_mbrs.Add(mbrs);
                        //AddMemberRow(member);
                    }
                    lst_all_mbrs.Add(lst_mbrs);
                }
                lst_all_groups = lst_all_mbrs;

                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    CMember member = Complete_Design.Members[i];
                    Set_Max_Result(ref member, lst_all_mbrs);

                    member.Force = member.Force;
                    AddMemberRow(member);
                }
            }

            #endregion Chiranjit [2014 01 25]


            //for (int i = 0; i < Complete_Design.Members.Count; i++)
            //{
                //CMember member = Complete_Design.Members[i];
                //if (Truss_Analysis != null)
                //{
                //    member.Group = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(member.Group.GroupName);
                //}
                //member.Force = Truss_Analysis.GetForce(ref member);
                //AddMemberRow(member);
            //}




            FillMemberResult();


            prss = 3;


            Button_Enable_Disable();

            Write_All_Data();

        }

        public List<CMember> Get_Members_All_Analysis(string group_name, List<List<CMember>> lst_mbrs)
        {
            List<CMember> mbrs = new List<CMember>();
            bool flag = false;
            for (int f = 0; f < lst_mbrs.Count; f++)
            {
                flag = false;
                for (int i = 0; i < lst_mbrs[f].Count; i++)
                {
                    if (lst_mbrs[f][i].Group.GroupName == group_name)
                    {
                        flag = true;
                        mbrs.Add(lst_mbrs[f][i]);
                    }
                }
                if(!flag)
                    mbrs.Add(new CMember());

            }
            return mbrs;
        }

        public void Set_Max_Result(ref CMember mbr, List<List<CMember>> lst_mbrs)
        {
            List<CMember> mbrs = new List<CMember>();

            CMember m = new CMember();

            AnalysisData ana_data = null;
            int file_index = -1;
            bool flag = false;
            for (int f = 0; f < lst_mbrs.Count; f++)
            {
                flag = false;
                for (int i = 0; i < lst_mbrs[f].Count; i++)
                {
                    if(lst_mbrs[f][i].Group.GroupName == mbr.Group.GroupName)
                    {
                        flag = true;
                        mbrs.Add(lst_mbrs[f][i]);

                        ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mbr.Group.MemberNos[0]];
                    }
                }
                if (flag == false)
                {
                    mbrs.Add(new CMember());
                }
            }

            string kStr = "";

            kStr = "";


            for(int i = 0; i < mbrs.Count;i++)
            {
                if (i == 0)
                {
                    m.MaxCompForce = mbrs[i].MaxCompForce;
                    m.MaxTorsion = mbrs[i].MaxTorsion;
                    m.MaxBendingMoment = mbrs[i].MaxBendingMoment;
                    m.MaxAxialForce = mbrs[i].MaxAxialForce;
                    m.MaxShearForce = mbrs[i].MaxShearForce;
                    m.MaxStress = mbrs[i].MaxStress;
                    m.MaxTensionForce = mbrs[i].MaxTensionForce;
                    continue;
                }
                else
                {
                    if (Math.Abs(m.MaxCompForce.Force) < Math.Abs(mbrs[i].MaxCompForce.Force))
                    {
                        m.MaxCompForce = mbrs[i].MaxCompForce;
                        m.MaxCompForce.File_Index = i;
                    }
                    if (Math.Abs(m.MaxBendingMoment.Force) < Math.Abs(mbrs[i].MaxBendingMoment.Force))
                    {
                        m.MaxBendingMoment = mbrs[i].MaxBendingMoment;
                        m.MaxBendingMoment.File_Index = i;
                    }
                    if (Math.Abs(m.MaxTorsion.Force) < Math.Abs(mbrs[i].MaxTorsion.Force))
                    {
                        m.MaxTorsion = mbrs[i].MaxTorsion;
                        m.MaxTorsion.File_Index = i;
                    }
                    if (Math.Abs(m.MaxAxialForce.Force) < Math.Abs(mbrs[i].MaxAxialForce.Force))
                    {
                        m.MaxAxialForce = mbrs[i].MaxAxialForce;
                        m.MaxAxialForce.File_Index = i;
                    }
                    if (Math.Abs(m.MaxShearForce.Force) < Math.Abs(mbrs[i].MaxShearForce.Force))
                    {
                        m.MaxShearForce = mbrs[i].MaxShearForce;
                        m.MaxShearForce.File_Index = i;
                    }
                    if (Math.Abs(m.MaxStress.Force) < Math.Abs(mbrs[i].MaxStress.Force))
                    {
                        m.MaxStress = mbrs[i].MaxStress;
                        m.MaxStress.File_Index = i;
                    }
                    if (Math.Abs(m.MaxTensionForce.Force) < Math.Abs(mbrs[i].MaxTensionForce.Force))
                    {
                        m.MaxTensionForce = mbrs[i].MaxTensionForce;
                        m.MaxTensionForce.File_Index = i;
                    }
                }
            }

            
            mbr.MaxCompForce = m.MaxCompForce;
            mbr.MaxTorsion = m.MaxTorsion;
            mbr.MaxBendingMoment = m.MaxBendingMoment;
            mbr.MaxAxialForce = m.MaxAxialForce;
            mbr.MaxShearForce = m.MaxShearForce;
            mbr.MaxStress = m.MaxStress;
            mbr.MaxTensionForce = m.MaxTensionForce;

            if(mbr.Group.GroupName.StartsWith("_ICAB") ||
                mbr.Group.GroupName.StartsWith("_OCAB"))
            {
                mbr.MaxCompForce = 0.0;
            }
            if (ana_data.AstraMemberType == eAstraMemberType.BEAM)
            {

                if (mbr.Group.GroupName.StartsWith("_AR") ||
                    mbr.Group.GroupName.StartsWith("_TR"))
                {

                    if (mbr.MaxBendingMoment.Force != 0.0)
                    {
                        kStr = "In Plane Bending Moment [Mz] = " + mbr.MaxBendingMoment.Force.ToString("0.000") + " kN-m";
                        kStr += ",  Out Plane Bending Moment [Mx] = " + mbr.MaxTorsion.Force.ToString("0.000") + " kN-m";
                    }
                    if (mbr.MaxShearForce.Force != 0.0)
                    {
                        if (kStr != "")
                            kStr += ", Axial Force = " + mbr.MaxAxialForce.Force.ToString("0.000") + " kN";
                        else
                            kStr += " Axial Force = " + mbr.MaxShearForce.Force.ToString("0.000") + " kN";
                    }
                }
                else
                {
                    if (mbr.MaxBendingMoment.Force != 0.0)
                    {
                        kStr = "Bending Moment = " + mbr.MaxBendingMoment.Force.ToString("0.000") + " kN-m";
                    }
                    if (mbr.MaxShearForce.Force != 0.0)
                    {
                        if (kStr != "")
                            kStr += ", Shear = " + mbr.MaxShearForce.Force.ToString("0.000") + " kN";
                        else
                            kStr += " Shear = " + mbr.MaxShearForce.Force.ToString("0.000") + " kN";
                    }
                }
            }
            else if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
            {
                if (mbr.MaxCompForce.Force != 0.0)
                {
                    kStr += " Compression = " + mbr.MaxCompForce.Force.ToString("0.000") + " kN";
                }
                if (mbr.MaxTensionForce.Force != 0.0)
                {
                    if (kStr != "")
                        kStr += ", Tension = " + mbr.MaxTensionForce.Force.ToString("0.000") + " kN";
                    else
                        kStr += " Tension = " + mbr.MaxTensionForce.Force.ToString("0.000") + " kN";
                }
            }
            else if (ana_data.AstraMemberType == eAstraMemberType.CABLE)
            {
                //if (max_comp.Force != 0.0)
                //{
                //    str += " Compression = " + max_comp.Force.ToString("0.000") + " kN";
                //}
                if (mbr.MaxTensionForce.Force != 0.0)
                {
                    if (kStr != "")
                        kStr += ", Tension = " + mbr.MaxTensionForce.Force.ToString("0.000") + " kN";
                    else
                        kStr += " Tension = " + mbr.MaxTensionForce.Force.ToString("0.000") + " kN";
                }
            }
            mbr.Force = kStr;
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
            //Chiranjit [2012 07 13]
            Write_All_Data();
            //Get_User_Input_Data
            string file_name = txt_analysis_file.Text;

            //file_name = Path.Combine(user_path, "LL.TXT");
            //if (!File.Exists(file_name))
            //{
            //    MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
            //    return;
            //}


            file_name = txt_analysis_file.Text;


            if (!File.Exists(file_name))
            {
                MessageBox.Show(this, "The Analysis Input data File is not created. \n\n" +
                                    "In Tab 'Structure Geometry' the button 'Create Analysis Input data File' " +
                                    "is to be used for creating the Analysis Input data\",",
                                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //tabControl1.SelectedTab = tab_GD;
                return;
            }

            string load_file = Path.Combine(Path.GetDirectoryName(file_name), "MEMBER_LOAD_DATA.txt");

            if (!SaveMemberLoads(load_file)) return;

            load_file = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");

            if (!SaveMemberLoads(load_file)) return;

            iApp.LiveLoads.Save_LL_TXT(Path.GetDirectoryName(file_name), false);

            #region Chiranjit [2014 01 23]

            //List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            //string kStr = "";
            //int indx = -1;
            //bool flag = false;
            //MyList mlist = null;
            //int i = 0;

            //bool isMoving_load = false;
            //for (i = 0; i < inp_file_cont.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
            //    mlist = new MyList(kStr, ' ');

            //    //if (kStr.Contains("LOAD GEN"))
            //    //    isMoving_load = true;

            //    List<string> mem_lst = new List<string>();

            //    if (kStr.StartsWith("MEMBER PROPER"))
            //    {
            //        i++;
            //        for (int j = 0; j < Complete_Design.Members.Count; j++)
            //        {
            //            Complete_Design.Members[j].iApp = iApp;
            //            //mem_lst.Add(string.Format("_L0L1       PRI    AX    0.0362    IX    0.00001    IY    0.000741    IZ    0.001"));
            //            mem_lst.Add(string.Format("{0}       PRI    AX    {1:f6}     IX    0.00001    IY    {2:f6}    IZ    0.001",
            //                Complete_Design.Members[j].Group.GroupName,
            //                Complete_Design.Members[j].Area,
            //                Complete_Design.Members[j].IYY));

            //            inp_file_cont[i + j] = mem_lst[j];
            //        }
            //    }

            //    if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
            //    {
            //        if (indx == -1)
            //            indx = i;
            //        flag = true;
            //    }
            //    if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
            //    {
            //        flag = false;
            //    }
            //    if (flag)
            //    {
            //        inp_file_cont.RemoveAt(i);
            //        i--;
            //    }

            //}

            //List<string> load_lst = new List<string>();

            //string s = "DL ";

            //s = chk_DL.Checked ? "DL " : "";
            //s += chk_SIDL.Checked ? chk_DL.Checked ? " + SIDL " : "SIDL" : "";
            //s += chk_LL.Checked ? (chk_DL.Checked || chk_SIDL.Checked) ? " + LL " : "LL" : "";



            ////if (complete_design.DeadLoads.Weight > 0)
            ////{
            ////    s = s + "+ SIDL";
            ////}
            ////s = s + "+ LL";
            //load_lst.Add("LOAD 1 " + s);
            //load_lst.Add("JOINT LOAD");
            //List<int> lst = new List<int>();
            //for (int j = 1; j < truss_data.Bottom_Chord1.Count; j++)
            //{
            //    //truss_data.
            //    lst.Add(truss_data.Bottom_Chord1[j].StartNode.NodeNo);
            //    lst.Add(truss_data.Bottom_Chord2[j].StartNode.NodeNo);
            //}
            ////load_lst.Add(string.Format("2   TO  10  13   TO  21      FY  -392.556", Truss_Analysis. ));
            //load_lst.Add(string.Format("{0}      FY  -{1:f3}", MyList.Get_Array_Text(lst), complete_design.ForceEachInsideJoints));
            //load_lst.Add(string.Format("{0}  {1}  {2}  {3}     FY  -{4:f4}", Truss_Analysis.Analysis.Supports[0].NodeNo
            //    , Truss_Analysis.Analysis.Supports[1].NodeNo, Truss_Analysis.Analysis.Supports[2].NodeNo, 
            //    Truss_Analysis.Analysis.Supports[3].NodeNo, complete_design.ForceEachEndJoint));


            ////List<string> lst = Truss_Analysis.Analysis.Joints.Get_Joints_Load_as_String(complete_design.ForceEachInsideJoints, complete_design.ForceEachEndJoint);
            ////truss_data.Joint_Load_Edge
            ////load_lst.AddRange(lst.ToArray());
            ////load_lst.Add("1                11                12                22                FY                -49.831                ");
            ////load_lst.Add("2                TO                10                13                TO                21                FY                -99.661");
            ////load_lst.Add("1                11                12                22                FY                -49.831                ");

            ////if (isMoving_load && dgv_live_load.RowCount != 0)
            //if (dgv_live_load.RowCount != 0)
            //{
            //    if (!File.Exists(load_file))
            //    {
            //        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
            //        //return;
            //    }

            //    //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //    //load_lst.Add("TYPE 1 CLA 1.179");
            //    //load_lst.Add("TYPE 2 CLB 1.188");
            //    //load_lst.Add("TYPE 3 A70RT 1.10");
            //    //load_lst.Add("TYPE 4 CLAR 1.179");
            //    //load_lst.Add("TYPE 5 A70RR 1.188");
            //    //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
            //    ////load_lst.Add("**** 3 LANE CLASS A *****");
            //    //load_lst.Add("LOAD GENERATION 60");



            //    //Chiranjit [2011 03 28]
            //    //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //    //load_lst.Add("TYPE 1 CLA 1.179");
            //    //load_lst.Add("TYPE 2 CLB 1.188");
            //    //load_lst.Add("TYPE 3 A70RT 1.10");
            //    //load_lst.Add("TYPE 4 CLAR 1.179");
            //    //load_lst.Add("TYPE 5 A70RR 1.188");
            //    //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
            //    //load_lst.Add("TYPE 7 RAILBG 1.25");
            //    //load_lst.Add("LOAD GENERATION 100");
            //    //load_lst.Add("TYPE 7  -69.500 0 1.000 XINC 0.5");

            //    LoadReadFromGrid();
            //    //foreach (LoadData ld in LoadList)
            //    //{
            //    //    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
            //    //    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
            //    //}
            //}
            //if (complete_design.Is_Live_Load && Live_Load_List != null)
            //    load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));
            //if (indx != -1)
            //    inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            //File.WriteAllLines(file_name, inp_file_cont.ToArray());
            #endregion Chiranjit [2014 01 23]



            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;
            List<string> load_lst = new List<string>();
            string s = "DL ";

            bool isMoving_load = false;
                int grp_count = 0;

            for (int f = 0; f < 5; f++)
            {
                #region Chiranjit [2014 01 23]

                iApp.LiveLoads.Save_LL_TXT(Path.GetDirectoryName(Get_User_Input_File_Name(f)), false);

                inp_file_cont = new List<string>(File.ReadAllLines(Get_User_Input_File_Name(f)));
                kStr = "";
                indx = -1;
                flag = false;
                mlist = null;
                i = 0;
                isMoving_load = false;
                grp_count = -1;
                for (i = 0; i < inp_file_cont.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                    mlist = new MyList(kStr, ' ');
                    if(i > 434)
                        mlist = new MyList(kStr, ' ');
                    //if (kStr.Contains("LOAD GEN"))
                    //    isMoving_load = true;
                    if(kStr.StartsWith("START GROUP"))
                    {
                        do
                        {
                            grp_count++;
                            kStr = MyList.RemoveAllSpaces(inp_file_cont[++i].ToUpper());
                        }
                        while (!kStr.StartsWith("END"));
                    }


                    List<string> mem_lst = new List<string>();

                    if (kStr.StartsWith("MEMBER PROPER"))
                    {
                        i++;
                        for (int j = 0; j < grp_count; j++, i++)
                        {
                            mlist = new MyList(MyList.RemoveAllSpaces( inp_file_cont[i]), ' ');
                            CMember cm = Complete_Design.Members.Get_Member(mlist[0]);
                            if (cm != null)
                            {
                                //mem_lst.Add(string.Format("_L0L1       PRI    AX    0.0362    IX    0.00001    IY    0.000741    IZ    0.001"));
                                inp_file_cont[i] = (string.Format("{0}       PRI    AX    {1:f6}     IX    0.00001    IY    {2:E3}    IZ    0.001",
                                    cm.Group.GroupName,
                                    cm.Area,
                                    cm.IYY));

                                //inp_file_cont[i] = (string.Format("{0}       PRI    AX    {1:f6}     IX    {2:f6}    IY    {3:E3}    IZ    0.001",
                                // cm.Group.GroupName,
                                // cm.Area,
                                // cm.IXX == 0.0 ? cm.IYY : cm.IXX,
                                // cm.IYY*10));
                            }
                            
                        }

                    }

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


                if (f == 0)
                {
                    load_lst = new List<string>();

                    s = "DL ";

                    s = chk_DL.Checked ? "DL " : "";
                    s += chk_SIDL.Checked ? chk_DL.Checked ? " + SIDL " : "SIDL" : "";
                    //s += chk_LL.Checked ? (chk_DL.Checked || chk_SIDL.Checked) ? " + LL " : "LL" : "";


                    load_lst.Add("LOAD 1 " + s);
                    load_lst.Add("JOINT LOAD");
                    List<int> lst = new List<int>();
                    for (int j = 1; j < truss_data.Bottom_Chord1.Count; j++)
                    {
                        //truss_data.
                        lst.Add(truss_data.Bottom_Chord1[j].StartNode.NodeNo);
                        lst.Add(truss_data.Bottom_Chord2[j].StartNode.NodeNo);
                    }
                    //load_lst.Add(string.Format("2   TO  10  13   TO  21      FY  -392.556", Truss_Analysis. ));
                    load_lst.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(lst), complete_design.ForceEachInsideJoints));
                    load_lst.Add(string.Format("{0}  {1}  {2}  {3}     FY  -{4:f4}", Truss_Analysis.Analysis.Supports[0].NodeNo
                        , Truss_Analysis.Analysis.Supports[1].NodeNo, Truss_Analysis.Analysis.Supports[2].NodeNo,
                        Truss_Analysis.Analysis.Supports[3].NodeNo, complete_design.ForceEachEndJoint));


                    if (dgv_live_load.RowCount != 0)
                    {
                        if (!File.Exists(load_file))
                        {
                            MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                        }


                        LoadReadFromGrid();

                    }
                    if (complete_design.Is_Live_Load && Live_Load_List != null)
                        load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));
                    if (indx != -1)
                        inp_file_cont.InsertRange(indx, load_lst);
                    //inp_file_cont.InsertRange(indx, );
                }
                else
                {
                    if (indx != -1)
                        inp_file_cont.InsertRange(indx, load_lst);
                }
                File.WriteAllLines(Get_User_Input_File_Name(f), inp_file_cont.ToArray());
                Button_Enable_Disable();
                #endregion Chiranjit [2014 01 23]
            }

            Button_Enable_Disable();


            prss = 2;
            Write_All_Data();

            MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_open_analysis_file_Click(object sender, EventArgs e)
        {
            frm_Arch_Input_Files faif = new frm_Arch_Input_Files();

            faif.ShowDialog();

            string kFile = MyList.Get_Analysis_Report_File(Get_User_Input_File_Name(faif.Select_File_Index));
            if (File.Exists(kFile))
                System.Diagnostics.Process.Start(kFile); ;

        }
        private void btn_open_member_load_Click(object sender, EventArgs e)
        {
            string kFile = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");
            if (File.Exists(kFile))
                iApp.View_Result(kFile, true);
            //System.Diagnostics.Process.Start(kFile);

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
                cmb_lac_tl.Text = "16";
                cmb_lac_bl.Text = "60";
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
            //Chiranjit [2013 05 28] Kolkata, Try Catch Statement
            try
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
            catch (Exception ex) { }
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

                if (isCreateData)
                {
                    //txt_L.Text = "80.0";
                    //txt_B.Text = "11.5";
                    //txt_H.Text = "13.5";
                    //txt_fw.Text = "1.5";
                    //cmb_panel_nos.Text = "12";
                    //txt_stringers_nos.Text = "4";


                    txt_L.Text = "80.0";
                    txt_B.Text = "15.5";
                    txt_aw.Text = "12.5";
                    txt_H.Text = "13.5";
                    txt_lfw.Text = "1.5";
                    txt_rfw.Text = "1.5";
                    txt_hrw.Text = "0.5";
                    txt_cw.Text = "7.5";
                    txt_rw.Text = "2.0";
                    cmb_panel_nos.Text = "12";
                    txt_stringers_nos.Text = "4";
                }
            }
        }
        private void btn_Create_Data_Click(object sender, EventArgs e)
        {

            if (Path.GetFileName(user_path) != Project_Name) Create_Project();

            //if (Path.GetFileName(user_path) != Project_Name)
            //{
            //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            //    if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);

            //    user_path = Path.Combine(user_path, txt_project_name.Text);

            //    if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);
            //}
            //Warren 1
            try
            {
                dgv_mem_details.Rows.Clear();
                dgv_member_Results_others.Rows.Clear();
                dgv_member_Results_arch_cables.Rows.Clear();
                complete_design = null;
                complete_design = new CompleteDesign();
                string kStr = Path.Combine(user_path, "MEMBER_LOAD_DATA.TXT");
                if (File.Exists(kStr))
                    File.Delete(kStr);
                system_path = Path.Combine(user_path, "AstraSys");
                kStr = Path.Combine(system_path, "MEMBER_LOAD_DATA.TXT");
                if (File.Exists(kStr))
                    File.Delete(kStr);

            }
            catch (Exception ex) { }

            string input_file = Path.Combine(user_path, "INPUT_DATA.TXT");
            //string input_file = ASTRA_DATA_FILE;





            FilePath = Path.GetDirectoryName(input_file);

            truss_data = new Arch_Cable_Suspension_Data();

            truss_data.Length = MyList.StringToDouble(txt_L.Text, 50.0);

            truss_data.Height = MyList.StringToDouble(txt_H.Text, 6.0);

            truss_data.Breadth = MyList.StringToDouble(txt_B.Text, 5.4);

            truss_data.Footpath_Width = MyList.StringToDouble(txt_lfw.Text, 1.0);

            truss_data.NoOfPanel = (int)MyList.StringToDouble(cmb_panel_nos.Text, 10);

            truss_data.NoOfStringerBeam = (int)MyList.StringToDouble(txt_stringers_nos.Text, 3);

            truss_data._L = MyList.StringToDouble(txt_L.Text, 0.0);
            truss_data._B = MyList.StringToDouble(txt_B.Text, 0.0);
            truss_data._aw = MyList.StringToDouble(txt_aw.Text, 0.0);
            truss_data._lfw = MyList.StringToDouble(txt_lfw.Text, 0.0);
            truss_data._rfw = MyList.StringToDouble(txt_rfw.Text, 0.0);
            truss_data._hrw = MyList.StringToDouble(txt_hrw.Text, 0.0);
            truss_data._cw = MyList.StringToDouble(txt_cw.Text, 0.0);
            truss_data._rw = MyList.StringToDouble(txt_rw.Text, 0.0);
            truss_data._NP = MyList.StringToDouble(cmb_panel_nos.Text, 12.0);
            truss_data._NS = MyList.StringToDouble(txt_stringers_nos.Text, 4.0);
            truss_data._cgw = MyList.StringToDouble(txt_cgw.Text, 4.0);

            truss_data.D1 = MyList.StringToDouble(txt_D1.Text, 1000.0);
            truss_data.D2 = MyList.StringToDouble(txt_D2.Text, 800.0);
            truss_data.ds = MyList.StringToDouble(txt_ds.Text, 42.0);



            truss_data.Start_Support = Start_Support_Text;
            truss_data.End_Support = END_Support_Text;

            truss_data.Is_Add_BCB = chk_bcb.Checked;

            if (truss_data.CreateData(input_file))
            {
                for (int i = 0; i < 5; i++)
                {
                    truss_data.CreateData(Get_User_Input_File_Name(i), i);
                    iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(Get_User_Input_File_Name(i)), false, iApp.DesignStandard);
                }

                isCreateData = true;

                iApp.LiveLoads.Save_LL_TXT(Path.GetDirectoryName(input_file), false);
                //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(input_file), false, iApp.DesignStandard);

                string src_file = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Steel Truss Warren 1\MEMBER_LOAD_DATA.txt");
                string des_file = Path.Combine(Path.GetDirectoryName(input_file), @"MEMBER_LOAD_DATA.txt");

                rbtn_custom_LL.Checked = true;
                MessageBox.Show(this, "Analysis Input data is created as \"" + Title + "\\INPUT_DATA.TXT\" inside the working folder. \n\r\n\rNote :\n\r" +
                "User has to observe the data displayed in the Tabs " +
                "'Steel Strsucture Load [DL]', 'Super Imposed Dead Load [SIDL]' and 'Moving Load [LL]'." +
                " User may modify the data if so desired." +
                " Next, User has to open the tab 'Analysis + Design' and Process the buttons" +
                "'Save Section + Load Data', 'Process Analysis' and 'Process Design' in sequence." +
                " This will complete the Design Process.", Title);
            }

            try
            {
                txt_analysis_file.Text = input_file;
                OpenAnalysisFile(input_file);
                Show_Total_Weight();


                prss = 1;


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    Data_Convert_and_Update_IS_TO_BS();
            }
            catch (Exception ex) { }

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
            Button btn = sender as Button;
            frm_Arch_Input_Files faif = new frm_Arch_Input_Files();
            faif.ShowDialog();
            int indx = faif.Select_File_Index;
            string fn = Get_User_Input_File_Name(indx);
            if (btn.Name == btn_View_Structure.Name)
            {


                if (File.Exists(fn))
                    iApp.OpenWork(fn, false);
            }
            else if (btn.Name == btn_View_Moving_Load.Name)
            {
                if (File.Exists(fn))
                    iApp.OpenWork(fn, true);
            }
            //if (btn.Name == btn_View_Structure.Name)
            //{


            //    if (File.Exists(txt_analysis_file.Text))
            //        iApp.OpenWork(txt_analysis_file.Text, false);
            //}
            //else if (btn.Name == btn_View_Moving_Load.Name)
            //{
            //    if (File.Exists(txt_analysis_file.Text))
            //        iApp.OpenWork(txt_analysis_file.Text, true);
            //}
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
            Show_Total_Weight();
        }


        private void txt_cd_force_percent_TextChanged(object sender, EventArgs e)
        {
            Show_Total_Weight();
        }
        private void cmb_custom_LL_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void dgv_member_Result_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmp = Path.Combine(user_path, "mem.txt");

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
                            iApp.View_Result(tmp);
                            //iApp.RunExe(tmp);
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

        private void txt_panel_nos_TextChanged(object sender, EventArgs e)
        {

            double _L, _B, _aw, _lfw, _rfw, _hrw, _cw, _rw, _cgw, _NS, _NP;




            _L = MyList.StringToDouble(txt_L.Text, 0.0);
            _B = MyList.StringToDouble(txt_B.Text, 0.0);
            _aw = MyList.StringToDouble(txt_aw.Text, 0.0);
            _lfw = MyList.StringToDouble(txt_lfw.Text, 0.0);
            _rfw = MyList.StringToDouble(txt_rfw.Text, 0.0);
            _hrw = MyList.StringToDouble(txt_hrw.Text, 0.0);
            _cw = MyList.StringToDouble(txt_cw.Text, 0.0);
            _rw = MyList.StringToDouble(txt_rw.Text, 0.0);
            _NP = MyList.StringToDouble(cmb_panel_nos.Text, 12.0);
             _NS= MyList.StringToDouble(txt_stringers_nos.Text, 4.0);


             txt_panel_Width.Text = (_cw / (_NS - 1)).ToString("f3");

             _cgw = (int)(_L / _NP);
            
            txt_cgw.Text = (_cgw).ToString("f3");


            double _intr_width = (_L - (2 * _cgw)) / (_NP - 2);
            txt_Panel_Length.Text = (_intr_width).ToString("f3");







            txt_LL_load_gen.Text = (_L / MyList.StringToDouble(txt_XINCR.Text, 0.5)).ToString("f0");
            
            //txt_X.Text = "-" + txt_L.Text; //Chiranjit  [2013 05 28]


            
            //double 


            for (int i = 0; i < dgv_SIDL.RowCount - 1; i++)
            {
                try
                {

                    dgv_SIDL[1, i].Value = txt_L.Text;
                }
                catch (Exception ex) { }
            }
            Format_SIDL();
            Text_Changed();
        }

        private void cmb_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                {
                    txt_X.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].Distance.ToString("f4"); // Chiranjit [2013 05 28] Kolkata
                    txt_Load_Impact.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].ImpactFactor.ToString("f3");
                }
            }
            catch (Exception ex) { }
        }

        #region Chiranjit [2012 02 08]
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            rft.M2 = chk_M2.Checked;
            rft.M3 = chk_M3.Checked;
            rft.R3 = chk_R3.Checked;
            rft.R2 = chk_R2.Checked;
            return rft;
        }
        #endregion

        private void btn_update_force_Click(object sender, EventArgs e)
        {
            string ana_rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            if (File.Exists(ana_rep_file))
            {
                Truss_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file, GetForceType());
                Truss_Analysis.ForceType = GetForceType();
            }
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
                Read_Results_others();
                Read_Results_Arches_cables();
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

        private void cmb_panel_nos_Leave(object sender, EventArgs e)
        {
            int d = MyList.StringToInt(cmb_panel_nos.Text, 1);
            if ((d % 2) != 0)
            {
                MessageBox.Show("Please enter an Even number.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //cmb_panel_nos.Text = "6";
                cmb_panel_nos.Focus();
            }
        }
        #endregion Form Events


        #region Chiranjit [2012 06 29]

        private void btn_Deck_Process_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 07 13]
            Write_All_Data();


            Deck_Initialize_InputData();
            Deck.Calculate_Program(Deck.rep_file_name);
            Deck.Write_Drawing_File();
            if (File.Exists(Deck.rep_file_name)) { MessageBox.Show(this, "Report file written in " + Deck.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(Deck.rep_file_name); }
            Deck.is_process = true;
            Deck.FilePath = user_path;
            Button_Enable_Disable();
        }
        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {
            if (Deck != null)
            {
                if (File.Exists(Deck.rep_file_name))
                    iApp.RunExe(Deck.rep_file_name);
            }
        }
        private void btn_Deck_Drawing_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Deck Slab Drawing"), "SteelTruss_Deck_Slab");
            //iApp.SetDrawingFile_Path(Deck.drawing_path, "TBEAM_Deck_Slab", "");
        }
        public void Deck_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                if (Deck == null)
                    Deck = new SteelTrussDeckSlab(iApp);
                Deck.FilePath = user_path;
                Deck.L = MyList.StringToDouble(txt_Deck_L.Text, 0.0);
                Deck.B = MyList.StringToDouble(txt_Deck_B.Text, 0.0);
                Deck.Ds = MyList.StringToDouble(txt_Deck_Ds.Text, 0.0);
                Deck.Dwc = MyList.StringToDouble(txt_Deck_Dwc.Text, 0.0);
                Deck.gamma_c = MyList.StringToDouble(txt_Deck_gamma_c.Text, 0.0);
                Deck.gamma_wc = MyList.StringToDouble(txt_Deck_gamma_wc.Text, 0.0);

                Deck.concrete_grade = MyList.StringToDouble(cmb_deck_fck.Text, 0.0);
                Deck.steel_grade = MyList.StringToDouble(cmb_deck_fy.Text, 0.0);
                Deck.sigma_cb = MyList.StringToDouble(txt_deck_sigma_c.Text, 0.0);
                Deck.sigma_st = MyList.StringToDouble(txt_Deck_sigma_st.Text, 0.0);

                Deck.m = MyList.StringToDouble(txt_deck_m.Text, 0.0);
                Deck.j = MyList.StringToDouble(txt_Deck_j.Text, 0.0);
                Deck.Q = MyList.StringToDouble(txt_Deck_Q.Text, 0.0);

                Deck.minimum_cover = MyList.StringToDouble(txt_Deck_minimum_cover.Text, 0.0);

                Deck.load = MyList.StringToDouble(txt_Deck_applied_load.Text, 0.0);
                Deck.width = MyList.StringToDouble(txt_Deck_load_width.Text, 0.0);
                Deck.length = MyList.StringToDouble(txt_Deck_load_length.Text, 0.0);
                Deck.impact_factor = MyList.StringToDouble(txt_Deck_impact_factor.Text, 0.0);
                Deck.continuity_factor = MyList.StringToDouble(txt_Deck_continuity_factor.Text, 0.0);
                Deck.mu = MyList.StringToDouble(txt_Deck_mu.Text, 0.0);


                Deck.self_weight_slab = (Deck.Ds / 1000) * Deck.gamma_c;
                Deck.self_weight_wearing_cource = (Deck.Dwc / 1000) * Deck.gamma_wc;
                Deck.tw = (Deck.self_weight_slab + Deck.self_weight_wearing_cource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }

        private void cmb_deck_select_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplied ld;

            ComboBox cmb = sender as ComboBox;

            if (cmb.Name == cmb_deck_select_load.Name)
            {
                ld = LoadApplied.Get_Applied_Load(cmb_deck_select_load.Text);

                txt_Deck_applied_load.Text = ld.Applied_Load.ToString();
                txt_Deck_load_length.Text = ld.LoadLength.ToString();
                txt_Deck_load_width.Text = ld.LoadWidth.ToString();
            }
        }


        //Chiranjit [2012 06 29]
        #region Abutment
        private void btn_Abutment_Process_Click(object sender, EventArgs e)
        {

            //Chiranjit [2012 07 13]
            Write_All_Data();


            if (Abut == null)
                Abut = new RCC_AbutmentWall(iApp);
            Abut.FilePath = user_path;
            Abutment_Initialize_InputData();
            Abut.Write_Cantilever__User_input();
            Abut.Calculate_Program(Abut.rep_file_name);
            Abut.Write_Cantilever_Drawing_File();
            MessageBox.Show(this, "Report file written in " + Abut.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(Abut.rep_file_name);
            Abut.is_process = true;
            Button_Enable_Disable();

        }
        private void btn_Abutment_Report_Click(object sender, EventArgs e)
        {
            if (Abut != null)
            {
                if (File.Exists(Abut.rep_file_name))
                    iApp.RunExe(Abut.rep_file_name);
            }
        }
        private void Abutment_Initialize_InputData()
        {
            #region Variables Initialize with default data

            Abut.d1 = MyList.StringToDouble(txt_abut_DMG.Text, 0.0);
            Abut.t = MyList.StringToDouble(txt_abut_t.Text, 0.0);
            Abut.H = MyList.StringToDouble(txt_abut_H.Text, 0.0);
            Abut.a = MyList.StringToDouble(txt_abut_a.Text, 0.0);
            Abut.gamma_b = MyList.StringToDouble(txt_abut_gamma_b.Text, 0.0);
            Abut.gamma_c = MyList.StringToDouble(txt_abut_gamma_c.Text, 0.0);
            Abut.phi = MyList.StringToDouble(txt_abut_phi.Text, 0.0);
            Abut.p = MyList.StringToDouble(txt_abut_p_bearing_capacity.Text, 0.0);
  
            Abut.w6 = MyList.StringToDouble(txt_abut_w6.Text, 0.0);
            Abut.w5 = MyList.StringToDouble(txt_abut_w5.Text, 0.0);
            Abut.F = MyList.StringToDouble(txt_abut_F.Text, 0.0);
            Abut.d2 = MyList.StringToDouble(txt_abut_d2.Text, 0.0);
            Abut.d3 = MyList.StringToDouble(txt_abut_d3.Text, 0.0);
            Abut.B = MyList.StringToDouble(txt_abut_B.Text, 0.0);
            Abut.theta = MyList.StringToDouble(txt_abut_theta.Text, 0.0);
            Abut.delta = MyList.StringToDouble(txt_abut_delta.Text, 0.0);
            Abut.z = MyList.StringToDouble(txt_abut_z.Text, 0.0);
            Abut.mu = MyList.StringToDouble(txt_abut_mu.Text, 0.0);
            Abut.L1 = MyList.StringToDouble(txt_abut_L1.Text, 0.0);
            Abut.L2 = MyList.StringToDouble(txt_abut_L2.Text, 0.0);
            Abut.L3 = MyList.StringToDouble(txt_abut_L3.Text, 0.0);
            Abut.L4 = MyList.StringToDouble(txt_abut_L4.Text, 0.0);
            Abut.h1 = MyList.StringToDouble(txt_abut_h1.Text, 0.0);
            Abut.L = MyList.StringToDouble(txt_abut_L.Text, 0.0);
            Abut.d4 = MyList.StringToDouble(txt_abut_d4.Text, 0.0);
            Abut.cover = MyList.StringToDouble(txt_abut_cover.Text, 0.0);
            Abut.factor = MyList.StringToDouble(txt_abut_fact.Text, 0.0);
            Abut.sc = MyList.StringToDouble(txt_abut_sc.Text, 0.0);

            Abut.f_ck = MyList.StringToDouble(cmb_abut_fck.Text, 0.0);
            Abut.f_y = MyList.StringToDouble(cmb_abut_fy.Text, 0.0);
            #endregion
        }
        #endregion Abutment

        //Chiranjit [2012 06 29]
        #region Design of RCC Pier

        private void cmb_pier_2_k_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_pier_2_k.SelectedIndex)
            {
                case 0: txt_pier_2_k.Text = "1.50"; break;
                case 1: txt_pier_2_k.Text = "0.66"; break;
                case 2: txt_pier_2_k.Text = "0.50"; break;
                case 3: txt_pier_2_k.Text = ""; txt_pier_2_k.Focus(); break;
            }
        }

        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(rcc_pier.rep_file_name);
        }

        private void btn_RccPier_Process_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 07 13]
            Write_All_Data();


            double MX1, MY1, W1;

            MX1 = MY1 = W1 = 0.0;

            MX1 = MyList.StringToDouble(txt_RCC_Pier_Mx1.Text, 0.0);
            MY1 = MyList.StringToDouble(txt_RCC_Pier_Mz1.Text, 0.0);
            W1 = MyList.StringToDouble(txt_RCC_Pier_W1_supp_reac.Text, 0.0);

            if (MX1 == 0.0 && MY1 == 0.0 && W1 == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : W1  = 6101.1 kN\n";
                msg += "            : MX1 = 274.8 kN-m\n";
                msg += "            : MZ1 = 603.1 kN-m\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {
                if (rcc_pier == null) rcc_pier = new RccPier(iApp);
                rcc_pier.FilePath = user_path;
                RCC_Pier_Initialize_InputData();
                rcc_pier.Calculate_Program();
                //rcc_pier.Write_User_Input();
                rcc_pier.Write_Drawing_File();
                if (File.Exists(rcc_pier.rep_file_name)) { MessageBox.Show(this, "Report file written in " + rcc_pier.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rcc_pier.rep_file_name); }
                rcc_pier.is_process = true;
            }
            Button_Enable_Disable();
        }
        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Name == btn_dwg_abut.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "SteelTruss_Abutment");
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
            }
            else if (b.Name == btn_dwg_pier.Name)
            {
                //iApp.RunViewer(Path.GetDirectoryName(rcc_pier.rep_file_name), "RCC_Pier_Default_Drawings");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "SteelTruss_Pier");
            }
            ////iapp.SetDrawingFile(user_input_file, "PIER");

            //string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            ////System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            //iApp.RunViewer(drwg_path, "RCC_Pier_Worksheet_Design_1");
            ////iapp.RunViewer(drwg_path);
        }
        public void RCC_Pier_Initialize_InputData()
        {
            rcc_pier.L1 = 0.0d;
            rcc_pier.W1 = 0.0d;
            rcc_pier.W2 = 0.0d;
            rcc_pier.W3 = 0.0d;
            rcc_pier.W4 = 0.0d;
            rcc_pier.W5 = 0.0d;
            rcc_pier.total_vehicle_load = 0.0d;
            rcc_pier.D1 = 0.0d;
            rcc_pier.D2 = 0.0d;
            rcc_pier.D3 = 0.0d;

            rcc_pier.RL6 = 0.0d;
            rcc_pier.RL5 = 0.0d;
            rcc_pier.RL4 = 0.0d;
            rcc_pier.RL3 = 0.0d;
            rcc_pier.RL2 = 0.0d;
            rcc_pier.RL1 = 0.0d;
            rcc_pier.H1 = 0.0d;
            rcc_pier.H2 = 0.0d;
            rcc_pier.H3 = 0.0d;
            rcc_pier.H4 = 0.0d;
            rcc_pier.H5 = 0.0d;
            rcc_pier.H6 = 0.0d;
            rcc_pier.H7 = 0.0d;
            rcc_pier.H8 = 0.0d;
            rcc_pier.B1 = 0.0d;
            rcc_pier.B2 = 0.0d;
            rcc_pier.B3 = 0.0d;
            rcc_pier.B4 = 0.0d;
            rcc_pier.B5 = 0.0d;
            rcc_pier.B6 = 0.0d;
            rcc_pier.B7 = 0.0d;
            rcc_pier.B8 = 0.0d;
            rcc_pier.B9 = 0.0d;
            rcc_pier.B10 = 0.0d;
            rcc_pier.B11 = 0.0d;
            rcc_pier.B12 = 0.0d;
            rcc_pier.B13 = 0.0d;
            rcc_pier.B14 = 0.0d;
            rcc_pier.B15 = 1.078d;
            rcc_pier.B16 = 0.0d;
            rcc_pier.NR = 0.0d;
            rcc_pier.NP = 0.0d;
            rcc_pier.gama_c = 0.0d;
            rcc_pier.MX1 = 0.0d;
            rcc_pier.MY1 = 0.0d;
            rcc_pier.sigma_s = 0.0d;

            #region Data Input Form 1 Variables
            rcc_pier.L1 = MyList.StringToDouble(txt_RCC_Pier_L.Text, 0.0);
            rcc_pier.w1 = MyList.StringToDouble(txt_RCC_Pier_CW.Text, 0.0);
            rcc_pier.w2 = MyList.StringToDouble(txt_RCC_Pier__B.Text, 0.0);
            rcc_pier.w3 = MyList.StringToDouble(txt_RCC_Pier_Wp.Text, 0.0);


            rcc_pier.a1 = MyList.StringToDouble(txt_RCC_Pier_Hp.Text, 0.0);
            rcc_pier.NB = MyList.StringToDouble(txt_RCC_Pier_NMG.Text, 0.0);
            rcc_pier.d1 = MyList.StringToDouble(txt_RCC_Pier_DMG.Text, 0.0);
            rcc_pier.d2 = MyList.StringToDouble(txt_RCC_Pier_Ds.Text, 0.0);
            rcc_pier.gama_c = MyList.StringToDouble(txt_RCC_Pier_gama_c.Text, 0.0);
            rcc_pier.B1 = MyList.StringToDouble(txt_RCC_Pier_B1.Text, 0.0);
            rcc_pier.B2 = MyList.StringToDouble(txt_RCC_Pier_B2.Text, 0.0);
            rcc_pier.H1 = MyList.StringToDouble(txt_RCC_Pier_H1.Text, 0.0);
            rcc_pier.B3 = MyList.StringToDouble(txt_RCC_Pier_B3.Text, 0.0);
            rcc_pier.B4 = MyList.StringToDouble(txt_RCC_Pier_B4.Text, 0.0);
            rcc_pier.H2 = MyList.StringToDouble(txt_RCC_Pier_H2.Text, 0.0);
            rcc_pier.B5 = MyList.StringToDouble(txt_RCC_Pier_B5.Text, 0.0);
            rcc_pier.B6 = MyList.StringToDouble(txt_RCC_Pier_B6.Text, 0.0);
            rcc_pier.RL1 = MyList.StringToDouble(txt_RCC_Pier_RL1.Text, 0.0);
            rcc_pier.RL2 = MyList.StringToDouble(txt_RCC_Pier_RL2.Text, 0.0);
            rcc_pier.RL3 = MyList.StringToDouble(txt_RCC_Pier_RL3.Text, 0.0);
            rcc_pier.RL4 = MyList.StringToDouble(txt_RCC_Pier_RL4.Text, 0.0);
            rcc_pier.RL5 = MyList.StringToDouble(txt_RCC_Pier_RL5.Text, 0.0);
            rcc_pier.form_lev = MyList.StringToDouble(txt_RCC_Pier_Form_Lev.Text, 0.0);
            rcc_pier.B7 = MyList.StringToDouble(txt_RCC_Pier_B7.Text, 0.0);
            rcc_pier.H3 = MyList.StringToDouble(txt_RCC_Pier_H3.Text, 0.0);
            rcc_pier.H4 = MyList.StringToDouble(txt_RCC_Pier_H4.Text, 0.0);
            rcc_pier.B8 = MyList.StringToDouble(txt_RCC_Pier_B8.Text, 0.0);
            rcc_pier.H5 = MyList.StringToDouble(txt_RCC_Pier_H5.Text, 0.0);
            rcc_pier.H6 = MyList.StringToDouble(txt_RCC_Pier_H6.Text, 0.0);
            rcc_pier.H7 = MyList.StringToDouble(txt_RCC_Pier_H7.Text, 0.0);
            rcc_pier.B9 = MyList.StringToDouble(txt_RCC_Pier_B9.Text, 0.0);
            rcc_pier.B10 = MyList.StringToDouble(txt_RCC_Pier_B10.Text, 0.0);
            rcc_pier.B11 = MyList.StringToDouble(txt_RCC_Pier_B11.Text, 0.0);
            rcc_pier.B12 = MyList.StringToDouble(txt_RCC_Pier_B12.Text, 0.0);
            rcc_pier.B13 = MyList.StringToDouble(txt_RCC_Pier_B13.Text, 0.0);
            rcc_pier.B14 = MyList.StringToDouble(txt_RCC_Pier___B.Text, 0.0);
            rcc_pier.over_all = rcc_pier.H7 + rcc_pier.H5 + rcc_pier.H6;
            //rcc_pier.B15 = MyList.StringToDouble(txt_RCC_Pier_B15.Text, 0.0);


            rcc_pier.p1 = MyList.StringToDouble(txt_RCC_Pier_p1.Text, 0.0);
            rcc_pier.p2 = MyList.StringToDouble(txt_RCC_Pier_p2.Text, 0.0);
            rcc_pier.d_dash = MyList.StringToDouble(txt_RCC_Pier_d_dash.Text, 0.0);
            rcc_pier.D = MyList.StringToDouble(txt_RCC_Pier_D.Text, 0.0);
            rcc_pier.b = MyList.StringToDouble(txt_RCC_Pier_b.Text, 0.0);

            //rcc_pier.Pu = MyList.StringToDouble(txt_Pu.Text, 0.0);
            //rcc_pier.Mux = MyList.StringToDouble(txt_Mux.Text, 0.0);
            //rcc_pier.Muy = MyList.StringToDouble(txt_Muy.Text, 0.0);
            rcc_pier.NP = MyList.StringToDouble(txt_RCC_Pier_NP.Text, 0.0);
            rcc_pier.NR = MyList.StringToDouble(txt_RCC_Pier_NR.Text, 0.0);
            rcc_pier.MX1 = MyList.StringToDouble(txt_RCC_Pier_Mx1.Text, 0.0);
            rcc_pier.MY1 = MyList.StringToDouble(txt_RCC_Pier_Mz1.Text, 0.0);
            rcc_pier.total_vehicle_load = MyList.StringToDouble(txt_RCC_Pier_vehi_load.Text, 0.0);
            rcc_pier.W1 = MyList.StringToDouble(txt_RCC_Pier_W1_supp_reac.Text, 0.0);

            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);
            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);



            //Chiranjit [2012 06 16]
            rcc_pier.NB = rcc_pier.NP;
            #endregion Data Input Form 1 Variables

            #region Data Input Form 2 Variables
            rcc_pier.P2 = MyList.StringToDouble(txt_pier_2_P2.Text, 0.0);
            rcc_pier.P3 = MyList.StringToDouble(txt_pier_2_P3.Text, 0.0);

            rcc_pier.B16 = MyList.StringToDouble(txt_pier_2_B16.Text, 0.0);
            //rcc_pier.total_pairs = MyList.StringToDouble(txt_pier_2_total_pairs.Text, 0.0);
            rcc_pier.PL = MyList.StringToDouble(txt_pier_2_PL.Text, 0.0);
            rcc_pier.PML = MyList.StringToDouble(txt_pier_2_PML.Text, 0.0);
            rcc_pier.APD = txt_pier_2_APD.Text;
            rcc_pier.PD = txt_pier_2_PD.Text;
            rcc_pier.SC = MyList.StringToDouble(txt_pier_2_SC.Text, 0.0);
            rcc_pier.HHF = MyList.StringToDouble(txt_pier_2_HHF.Text, 0.0);
            rcc_pier.V = MyList.StringToDouble(txt_pier_2_V.Text, 0.0);
            rcc_pier.K = MyList.StringToDouble(txt_pier_2_k.Text, 0.0);
            rcc_pier.CF = MyList.StringToDouble(txt_pier_2_CF.Text, 0.0);
            rcc_pier.LL = MyList.StringToDouble(txt_pier_2_LL.Text, 0.0);
            rcc_pier.Vr = MyList.StringToDouble(txt_pier_2_Vr.Text, 0.0);
            rcc_pier.Itc = MyList.StringToDouble(txt_pier_2_Itc.Text, 0.0);
            rcc_pier.sdia = MyList.StringToDouble(txt_pier_2_sdia.Text, 0.0);
            rcc_pier.sleg = MyList.StringToDouble(txt_pier_2_slegs.Text, 0.0);
            rcc_pier.ldia = MyList.StringToDouble(txt_pier_2_ldia.Text, 0.0);
            rcc_pier.SBC = MyList.StringToDouble(txt_pier_2_SBC.Text, 0.0);

            #endregion Data Input Form 2 Variables




            rcc_pier.rdia = MyList.StringToDouble(txt_RCC_Pier_rdia.Text, 0.0);
            rcc_pier.tdia = MyList.StringToDouble(txt_RCC_Pier_tdia.Text, 0.0);



            rcc_pier.hdia = MyList.StringToDouble(txt_pier_2_hdia.Text, 0.0);
            rcc_pier.hlegs = MyList.StringToDouble(txt_pier_2_hlegs.Text, 0.0);
            rcc_pier.vdia = MyList.StringToDouble(txt_pier_2_vdia.Text, 0.0);
            rcc_pier.vlegs = MyList.StringToDouble(txt_pier_2_vlegs.Text, 0.0);
            rcc_pier.vspc = MyList.StringToDouble(txt_pier_2_vspc.Text, 0.0);


        }
        #endregion Design of RCC Pier


        #region View Force
        string DL_ANALYSIS_REP = "";
        string LL_ANALYSIS_REP = "";

        SupportReactionCollection DL_support_reactions = null;
        SupportReactionCollection LL_support_reactions = null;
        string Supports = "";
        //IApplication iApp = null;
        double B = 0.0;
        public void frm_ViewForces(double abut_width, string DL_ANALYSIS_REPort_file, string LL_ANALYSIS_REPort_file, string supports)
        {
            //iApp = app;
            DL_ANALYSIS_REP = DL_ANALYSIS_REPort_file;
            LL_ANALYSIS_REP = LL_ANALYSIS_REPort_file;
            Supports = supports.Replace(",", " ");
            B = abut_width;
        }
        public string Total_DeadLoad_Reaction
        {
            get
            {
                return txt_dead_kN_m.Text;
            }
            set
            {
                txt_dead_kN_m.Text = value;
            }
        }
        public string Total_LiveLoad_Reaction
        {
            get
            {
                return txt_live_kN_m.Text;
            }
            set
            {
                txt_live_kN_m.Text = value;
            }
        }
        void frm_ViewForces_Load()
        {
            try
            {
                DL_support_reactions = new SupportReactionCollection(iApp, DL_ANALYSIS_REP);
                LL_support_reactions = new SupportReactionCollection(iApp, LL_ANALYSIS_REP);
                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data_DeadLoad()
        {

            dgv_left_end_design_forces.Rows.Clear();
            dgv_right_end_design_forces.Rows.Clear();

            SupportReactionData sr = null;
            MyList mlist = new MyList(MyList.RemoveAllSpaces(Supports), ' ');

            double tot_dead_vert_reac = 0.0;
            double tot_live_vert_reac = 0.0;

            for (int i = 0; i < mlist.Count; i++)
            {
                sr = DL_support_reactions.Get_Data(mlist.GetInt(i));
                dgv_left_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));

                tot_dead_vert_reac += Math.Abs(sr.Max_Reaction); ;
            }

            for (int i = 0; i < mlist.Count; i++)
            {
                sr = LL_support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));
                tot_live_vert_reac += Math.Abs(sr.Max_Reaction);
            }

            txt_dead_vert_reac_ton.Text = (tot_dead_vert_reac/10).ToString("f3");
            txt_live_vert_rec_Ton.Text = (tot_live_vert_reac/10).ToString("f3");
        }


        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            //if (txt.Name == txt_dead_vert_reac_ton.Name)
            //{
            Text_Changed();
            //}

        }

        private void Text_Changed()
        {
            try
            {
                txt_LL_load_gen.Text = ((MyList.StringToDouble(txt_L.Text, 1) / MyList.StringToDouble(txt_XINCR.Text, 1)) + 1).ToString("f0");

                for (int i = 0; i < dgv_live_load.RowCount; i++)
                {
                    dgv_live_load[4, i].Value = txt_XINCR.Text;
                    dgv_live_load[1, i].Value = txt_X.Text;
                }
            }
            catch (Exception ex) { }


            if (B != 0)
            {
                txt_dead_vert_reac_kN.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10)).ToString("f3");
                txt_dead_kN_m.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10) / B).ToString("f3");
                //}
                //else if (txt.Name == txt_live_vert_rec_Ton.Name)
                //{
                txt_live_vert_rec_kN.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10)).ToString("f3");
                txt_live_kN_m.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10) / B).ToString("f3");
                //}
            }
            //else if (txt.Name == txt_dead_kN_m.Name)
            //{
            txt_abut_w5.Text = txt_dead_kN_m.Text;
            txt_pier_2_P2.Text = txt_dead_kN_m.Text;
            //}
            //else if (txt.Name == txt_live_kN_m.Name)
            //{
            txt_abut_w6.Text = txt_live_kN_m.Text;
            txt_pier_2_P3.Text = txt_live_kN_m.Text;
            //}
            //else if (txt.Name == txt_final_vert_rec_kN.Name)
            //{
            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mx_kN.Name)
            //{
            txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mz_kN.Name)
            //{
            txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;


            txt_abut_B.Text = txt_RCC_Pier__B.Text = txt_RCC_Pier___B.Text = txt_B.Text;

            txt_RCC_Pier_L.Text = txt_abut_L.Text = txt_L.Text;

            txt_Deck_B.Text = txt_panel_Width.Text;
            txt_Deck_L.Text = txt_Panel_Length.Text;

        }
        #endregion View Force

        #region frm_Pier_ViewDesign_Forces
        string ANALYSIS_REP = "";
        SupportReactionCollection support_reactions = null;
        string Left_support = "";
        string Right_support = "";
        public void frm_Pier_ViewDesign_Forces(string ANALYSIS_REPort_file, string left_support, string right_support)
        {
           
            ANALYSIS_REP = ANALYSIS_REPort_file;
            
            Left_support = left_support.Replace(",", " ");
            Right_support = right_support.Replace(",", " ");
        }

        private void frm_ViewDesign_Forces_Load()
        {
            support_reactions = new SupportReactionCollection(iApp, ANALYSIS_REP);
            try
            {
                Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }

        void Show_and_Save_Data()
        {
            if (!File.Exists(ANALYSIS_REP)) return;
            string format = "{0,27} {1,10:f3} {2,10:f3} {3,10:f3}";
            List<string> list_arr = new List<string>(File.ReadAllLines(ANALYSIS_REP));
            list_arr.Add("");
            list_arr.Add("                   =====================================");
            list_arr.Add("                     DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                   =====================================");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add(string.Format(""));
            list_arr.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list_arr.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list_arr.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list_arr.Add("");
            SupportReactionData sr = null;

            MyList mlist = new MyList(MyList.RemoveAllSpaces( Left_support), ' ');

            double tot_left_vert_reac = 0.0;
            double tot_right_vert_reac = 0.0;

            double tot_left_Mx = 0.0;
            double tot_left_Mz = 0.0;

            double tot_right_Mx = 0.0;
            double tot_right_Mz = 0.0;


            dgv_left_des_frc.Rows.Clear();
            dgv_right_des_frc.Rows.Clear();
            list_arr.Add("LEFT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);

                tot_left_vert_reac += Math.Abs(sr.Max_Reaction); ;
                tot_left_Mx += sr.Max_Mx;
                tot_left_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }

            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Ton
            tot_left_vert_reac /= 10.0;
            tot_left_Mx /= 10.0;
            tot_left_Mz /= 10.0;

            txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");
            list_arr.Add(string.Format(format, "TOTAL", tot_left_vert_reac, tot_left_Mx, tot_left_Mz));
            list_arr.Add("");

             mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_des_frc.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz);

                tot_right_vert_reac += Math.Abs(sr.Max_Reaction);
                tot_right_Mx += sr.Max_Mx;
                tot_right_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }
            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Ton
            tot_right_vert_reac /= 10.0;
            tot_right_Mx /= 10.0;
            tot_right_Mz /= 10.0;
            txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");


            //txt_both_ends_total.Text = (tot_left_vert_reac + tot_right_vert_reac).ToString("0.000");
            list_arr.Add("");
            //list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Ton");

            txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_max_Mx_kN.Text + " kN-m");
            txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_max_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");
            




            File.WriteAllLines(ANALYSIS_REP, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_max_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_max_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(ANALYSIS_REP), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }
        #endregion frm_Pier_ViewDesign_Forces


        #endregion Chiranjit [2012 06 29]


        private void Write_Load(string file_name, bool IsLiveLoad, bool IsDeadLoad)
        {
            //string file_name = txt_analysis_file.Text;

            //file_name = Path.Combine(user_path, "LL.TXT");
            //if (!File.Exists(file_name))
            //{
            //    MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
            //    return;
            //}


            //file_name = txt_analysis_file.Text;


            if (!File.Exists(file_name)) return;

            string load_file = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");

            //if (!SaveMemberLoads(load_file)) return;

            //load_file = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");

            //if (!SaveMemberLoads(load_file)) return;

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

            string s = "DL + SIDL";

            s = IsDeadLoad ? "DL + SIDL" : "";
            //s += IsLiveLoad ? IsDeadLoad ? " + LL " : "LL" : "";



            //if (complete_design.DeadLoads.Weight > 0)
            //{
            //    s = s + "+ SIDL";
            //}
            //s = s + "+ LL";
            load_lst.Add("LOAD  1  " + s);
            load_lst.Add("JOINT LOAD");

            List<string> lst = Truss_Analysis.Analysis.Joints.Get_Joints_Load_as_String(complete_design.ForceEachInsideJoints, complete_design.ForceEachEndJoint);
            //truss_data.Joint_Load_Edge

            if (IsDeadLoad)
                load_lst.AddRange(lst.ToArray());
            else
                load_lst.Add(string.Format("{0} TO {1} FY -0.0001", Truss_Analysis.Analysis.Joints[0].NodeNo,
                     Truss_Analysis.Analysis.Joints[Truss_Analysis.Analysis.Joints.Count - 1].NodeNo));
           
            //load_lst.Add("1                11                12                22                FY                -49.831                ");
            //load_lst.Add("2                TO                10                13                TO                21                FY                -99.661");
            //load_lst.Add("1                11                12                22                FY                -49.831                ");

            if (dgv_live_load.RowCount != 0 && IsLiveLoad)
            {
                LoadReadFromGrid();
            }
            if (IsLiveLoad)
                if (complete_design.Is_Live_Load && Live_Load_List != null)
                    load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));

            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());


            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public string DL_Input_Analysis_File
        {
            get
            {
                string kstr = File.Exists(user_path) ? Path.GetDirectoryName(user_path) : user_path;
                kstr = Path.Combine(user_path, "DL AND LL ANALYSIS");
                if (!Directory.Exists(kstr)) Directory.CreateDirectory(kstr);

                kstr = Path.Combine(kstr, "Dead Load Analysis");
                if (!Directory.Exists(kstr)) Directory.CreateDirectory(kstr);

                return Path.Combine(kstr, "DL_INPUT_DATA.TXT");
            }
        }
        public string DL_Report_Analysis_File
        {
            get
            {
                if (!File.Exists(DL_Input_Analysis_File)) return "";
                return Path.Combine(Path.GetDirectoryName(DL_Input_Analysis_File), "ANALYSIS_REP.TXT");
            }
        }
        public string LL_Input_Analysis_File
        {
            get
            {
                string kstr = File.Exists(user_path) ? Path.GetDirectoryName(user_path) : user_path;
                  kstr = Path.Combine(user_path, "DL AND LL ANALYSIS");
                if (!Directory.Exists(kstr)) Directory.CreateDirectory(kstr);


                kstr = Path.Combine(kstr, "Live Load Analysis");
                if (!Directory.Exists(kstr)) Directory.CreateDirectory(kstr);

                return Path.Combine(kstr, "LL_INPUT_DATA.TXT");

            }
        }
        public string LL_Report_Analysis_File
        {
            get
            {
                if (!File.Exists(LL_Input_Analysis_File)) return "";
                return Path.Combine(Path.GetDirectoryName(LL_Input_Analysis_File), "ANALYSIS_REP.TXT");
            }
        }
        public string Total_Input_Analysis_File
        {
            get
            {
                string kstr = File.Exists(user_path) ? Path.GetDirectoryName(user_path) : user_path;
                kstr = Path.Combine(user_path, "DL AND LL ANALYSIS");
                if (!Directory.Exists(kstr)) Directory.CreateDirectory(kstr);

                kstr = Path.Combine(kstr, "Total Load Analysis");
                if (!Directory.Exists(kstr)) Directory.CreateDirectory(kstr);

                return Path.Combine(kstr, "TOTAL_INPUT_DATA.TXT");
            }
        }
        public string Total_Report_Analysis_File
        {
            get
            {
                if (!File.Exists(LL_Input_Analysis_File)) return "";
                return Path.Combine(Path.GetDirectoryName(Total_Input_Analysis_File), "ANALYSIS_REP.TXT");
            }
        }

        private void btn_DL_LL_ana_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 07 13]
            Write_All_Data();

            try
            {


                string kStr = txt_analysis_file.Text;
                string file_name = txt_analysis_file.Text;

                kStr = Get_User_Input_File_Name(0);
                file_name = Get_User_Input_File_Name(0);

                if (!File.Exists(file_name))
                {



                    MessageBox.Show(this, "In Tab \"Analysis & Design of Steel Truss Bridge\" >> \"Structure Geometry\"\n\n" + 
                                        "the button \"Create Analysis Input Data File\" is to be used to Create the \n\n"+
                                        "Load Data file before Processing the present option",
                        "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //MessageBox.Show(this, "In Tab [Analysis+Design\n\n" +
                    //    "In Tab 'Analysis+Design' the button 'Process Analysis' is to be used for " +
                    //    "creating the Analysis Results for the Total Load.",
                    //    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //tabControl1.SelectedTab = tab_GD;
                    return;
                }
                //if (!File.Exists(file_name)) return;

                string load_file = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");

                //if (!SaveMemberLoads(load_file)) return;
                if(!File.Exists(load_file))
                load_file = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");

                if (!SaveMemberLoads(load_file)) return;

                //List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
                ////string kStr = "";
                //int indx = -1;
                //bool flag = false;
                //MyList mlist = null;
                //int i = 0;

                //bool isMoving_load = false;
                //for (i = 0; i < inp_file_cont.Count; i++)
                //{
                //    kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                //    mlist = new MyList(kStr, ' ');

                //    if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                //    {
                //        if (indx == -1)
                //            indx = i;
                //        flag = true;
                //    }
                //    if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                //    {
                //        flag = false;
                //    }
                //    if (flag)
                //    {
                //        inp_file_cont.RemoveAt(i);
                //        i--;
                //    }
                //}
                
                //if (dgv_live_load.RowCount != 0)
                //{
                //    LoadReadFromGrid();
                //}




                if (File.Exists(file_name))
                {
                    File.Copy(kStr, Total_Input_Analysis_File, true);
                    iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(Total_Input_Analysis_File), false, iApp.DesignStandard);
                    Write_Load(Total_Input_Analysis_File, true, true);

                    File.Copy(kStr, LL_Input_Analysis_File, true);
                    iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(LL_Input_Analysis_File), false, iApp.DesignStandard);
                    Write_Load(LL_Input_Analysis_File, true, false);

                    File.Copy(kStr, DL_Input_Analysis_File, true);
                    Write_Load(DL_Input_Analysis_File, false, true);
                    
                    string flPath = txt_analysis_file.Text;

                    int c = 0;



                    ProcessData pd = new ProcessData();
                    ProcessCollection pcol = new ProcessCollection();




                    do
                    {
                        if (c == 0)
                        {
                            flPath = Total_Input_Analysis_File;
                            //MessageBox.Show(this, "Process Total Load Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (c == 1)
                        {
                            flPath = LL_Input_Analysis_File;
                            //MessageBox.Show(this, "Process Live Load Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (c == 2)
                        {
                            //MessageBox.Show(this, "Process Dead Load Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flPath = DL_Input_Analysis_File;
                        }


                        //File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                        //File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                        ////System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                        //System.Diagnostics.Process prs = new System.Diagnostics.Process();

                        //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                        //System.Environment.SetEnvironmentVariable("ASTRA", flPath);


                        //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                        //if (File.Exists(prs.StartInfo.FileName))
                        //{
                        //    if (prs.Start())
                        //        prs.WaitForExit();
                        //}
                        //else
                        //{
                        //    MessageBox.Show(prs.StartInfo.FileName + " not found."); return;
                        //}


                        pd = new ProcessData();
                        pd.Process_File_Name = flPath;
                        pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                        pcol.Add(pd);

                        c++;
                    }
                    while (c < 3);

                    string ana_rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
                    //if (File.Exists(ana_rep_file))
                    //    Truss_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file, GetForceType());
                    if (iApp.Show_and_Run_Process_List(pcol))
                    {
                        string s1, s2;
                        s1 = s2 = "";
                        for (int j = 0; j < Truss_Analysis.Analysis.Supports.Count; j++)
                        {
                            if (j < Truss_Analysis.Analysis.Supports.Count / 2)
                                s1 += Truss_Analysis.Analysis.Supports[j].NodeNo + ",";
                            else
                                s2 += Truss_Analysis.Analysis.Supports[j].NodeNo + ",";
                        }

                        frm_ViewForces(Truss_Analysis.Analysis.Width, DL_Report_Analysis_File, LL_Report_Analysis_File, (s1 + s2));
                        frm_ViewForces_Load();

                        frm_Pier_ViewDesign_Forces(Total_Report_Analysis_File, s1, s2);
                        frm_ViewDesign_Forces_Load();
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Input File : " + ex.Message);
            }
        }

        private void btn_dwg_abut_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            //if (b.Name == btn_dwg_deck.Name)
            //    iApp.SetDrawingFile_Path(Deck.drawing_path, "TBEAM_Deck_Slab", "");
            if (b.Name == btn_dwg_abut.Name)
                iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
            if (b.Name == btn_dwg_pier.Name)
                iApp.RunViewer(Path.GetDirectoryName(rcc_pier.rep_file_name), "RCC_Pier_Default_Drawings");
        }

        #region Chiranjit [2012 07 04]
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_deck") ||
                ctrl.Name.ToLower().StartsWith("txt_deck"))
            {
                astg = new ASTRAGrade(cmb_deck_fck.Text, cmb_deck_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_deck_m.Text, 10.0);
                txt_deck_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_Deck_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
                txt_Deck_j.Text = astg.j.ToString("f3");
                txt_Deck_Q.Text = astg.Q.ToString("f3");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_abut") ||
                ctrl.Name.ToLower().StartsWith("txt_abut"))
            {
                astg = new ASTRAGrade(cmb_abut_fck.Text, cmb_abut_fy.Text);
                txt_abut_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_abut_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }

            else if (ctrl.Name.ToLower().StartsWith("cmb_rcc_pier") ||
                ctrl.Name.ToLower().StartsWith("txt_rcc_pier"))
            {
                astg = new ASTRAGrade(cmb_rcc_pier_fck.Text, cmb_rcc_pier_fy.Text);
                txt_rcc_pier_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_rcc_pier_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }


        }
        #endregion Chiranjit [2012 07 04]

        #region Chiranjit [2012 07 10]
        //Write All Data in a File
        public void Write_All_Data()
        {

            iApp.Save_Form_Record(this, user_path);
            return;

            List<string> file_content = new List<string>();

            string kFormat = "{0} = {1} = {2}";

            #region   DECK SLAB USER INPUT


            file_content.Add(string.Format("DECK SLAB USER INPUT"));
            file_content.Add(string.Format("--------------------"));
            file_content.Add(string.Format(kFormat, "Panel Length", "L", txt_Deck_L.Text));
            file_content.Add(string.Format(kFormat, "Spacing of main Girders", "B", txt_Deck_B.Text));

            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_deck_fck.Text));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_deck_fy.Text));
            file_content.Add(string.Format(kFormat,"Permissible Stress in Concrete","σ_cb", txt_deck_sigma_c.Text));
            file_content.Add(string.Format(kFormat,"Permissible Stress in Steel","σ_st",txt_Deck_sigma_st.Text));
            file_content.Add(string.Format(kFormat,"Modular ratio","m", txt_deck_m.Text));
            file_content.Add(string.Format(kFormat,"Lever arm factor","j", txt_Deck_j.Text));
            file_content.Add(string.Format(kFormat, "Moment factor", "Q", txt_Deck_Q.Text));

            file_content.Add(string.Format(kFormat,"Minimum Cover","mc", txt_Deck_minimum_cover.Text));

            file_content.Add(string.Format(kFormat, "Select Load", "SL", cmb_deck_select_load.Text));
            file_content.Add(string.Format(kFormat, "Applied Load", "AL", txt_Deck_applied_load.Text));
            file_content.Add(string.Format(kFormat,"Width of Load","a", txt_Deck_load_width.Text));
            file_content.Add(string.Format(kFormat,"Length of Load","b", txt_Deck_load_length.Text));
            file_content.Add(string.Format(kFormat,"Impact Factor","IF", txt_Deck_impact_factor.Text));
            file_content.Add(string.Format(kFormat,"Continuity Factor","CF", txt_Deck_continuity_factor.Text));
            file_content.Add(string.Format(kFormat,"Constant [µ]","mu", txt_Deck_mu.Text));

            file_content.Add(string.Format(kFormat,"Thickness of concrete Deck Slab","Ds", txt_Deck_Ds.Text));
            file_content.Add(string.Format(kFormat,"Unit weight of concrete Deck Slab","γ_c", txt_Deck_gamma_c.Text));
            file_content.Add(string.Format(kFormat,"Thickness of Asphalt Wearing Course","Dwc", txt_Deck_Dwc.Text));
            file_content.Add(string.Format(kFormat, "Unit weight of Asphalt Wearing Course", "γ_wc", txt_Deck_gamma_wc.Text));


            #endregion DECK SLAB USER INPUT

            #region ABUTMENT USER INPUT

            file_content.Add(string.Format(""));
            file_content.Add(string.Format("ABUTMENT USER INPUT"));
            file_content.Add(string.Format( "--------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Depth of Girder Seat", "DMG", txt_abut_DMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Main wall", "t", txt_abut_t.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Retained Earth", "H", txt_abut_H.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Earth at front", "a", txt_abut_a.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of wall", "B", txt_abut_B.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Equivalent height of earth for Live Load Surcharge ", "d2", txt_abut_d2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Approach Slab", "d3", txt_abut_d3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Base", "d4", txt_abut_d4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Length of base in back of wall", "L1", txt_abut_L1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Length of base in wall Location", "L2", txt_abut_L2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Length of Base at front of wall", "L3", txt_abut_L3.Text));
            file_content.Add(string.Format(kFormat, "Thickness of Dirt wall at Girder Seat at the Top", "L4", txt_abut_L4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Span of Longitudinal Girder", "L", txt_abut_L.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Angle between wall and Horizontal base on Earth Side", "θ", txt_abut_theta.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Angle Internal Friction (Repose) of Back fill", "φ", txt_abut_phi.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Angle of friction between Earth and wall", "z", txt_abut_z.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Inclination of Earth fill with the Horizontal", "δ", txt_abut_delta.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Coefficient of friction between Earth and wall", "µ", txt_abut_mu.Text));
            file_content.Add(string.Format(kFormat, "Reinf. Clear Cover", "cc", txt_abut_cover.Text));
            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_abut_fck.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Allowable Flexural Stress in Concrete", "σ_c", txt_abut_sigma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_abut_fy.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permissible Stress in Steel", "σ_st", txt_abut_sigma_st.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bearing Capacity", "p", txt_abut_p_bearing_capacity.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Vehicle Break is applied at a height", "h1", txt_abut_h1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bending Moment and Shear Force Factor", "Fact", txt_abut_fact.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit weight of Concrete", "γ_c", txt_abut_gamma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Seismic Coefficient", "sc", txt_abut_sc.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Live Load from vehicles", "w6", txt_abut_w6.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permanent Load from Super Structure", "w5", txt_abut_w5.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Vehicle Braking Force", "F", txt_abut_F.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit Weight of Backfill Earth", "γ_b", txt_abut_gamma_b.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));


            #endregion ABUTMENT USER INPUT

            #region RCC PIER FORM1 USER INPUT DATA

            file_content.Add(string.Format("RCC PIER FORM1 USER INPUT DATA"));
            file_content.Add(string.Format("------------------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "C/C Distance between Piers [L]", "L", txt_RCC_Pier_L.Text));
            file_content.Add(string.Format(kFormat, "Carriageway width", "w1", txt_RCC_Pier_CW.Text));
            file_content.Add(string.Format(kFormat, "Overall width of Deck", "w2", txt_RCC_Pier__B.Text));
            file_content.Add(string.Format(kFormat, "Width of Crash Barrier", "w3", txt_RCC_Pier_Wp.Text));
            file_content.Add(string.Format(kFormat, "Height of Crash Barrier", "a1", txt_RCC_Pier_Hp.Text));
            file_content.Add(string.Format(kFormat, "Number of Bearings", "NB", txt_RCC_Pier_NMG.Text));
            file_content.Add(string.Format(kFormat, "Depth of Girder", "d1", txt_RCC_Pier_DMG.Text));
            file_content.Add(string.Format(kFormat, "Depth of Deck Slab", "d2", txt_RCC_Pier_Ds.Text));
            file_content.Add(string.Format(kFormat, "Unit Weight of Concrete", "γ_c", txt_RCC_Pier_gama_c.Text));
            file_content.Add(string.Format(kFormat, "Pedestal Width", "B1", txt_RCC_Pier_B1.Text));
            file_content.Add(string.Format(kFormat, "Pedestal Thickness", "B2", txt_RCC_Pier_B2.Text));
            file_content.Add(string.Format(kFormat, "Pedestal Height", "H1", txt_RCC_Pier_H1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Nos. of Pedestals per Row", "NP", txt_RCC_Pier_NP.Text));
            file_content.Add(string.Format(kFormat, "Nos. of Row", "NR", txt_RCC_Pier_NR.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bearing Width", "B3", txt_RCC_Pier_B3.Text));
            file_content.Add(string.Format(kFormat, "Bearing Thickness", "B4", txt_RCC_Pier_B4.Text));
            file_content.Add(string.Format(kFormat, "Bearing Height", "H2", txt_RCC_Pier_H2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance Between Girders", "B5", txt_RCC_Pier_B5.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Length of Footing", "B6", txt_RCC_Pier_B6.Text));
            file_content.Add(string.Format(kFormat, "R.L. at Pier Cap Top", "RL1", txt_RCC_Pier_RL1.Text));
            file_content.Add(string.Format(kFormat, "High Flood Level (HFL)", "RL2", txt_RCC_Pier_RL2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Existing Ground Level", "RL3", txt_RCC_Pier_RL3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "R.L. at Footing Top", "RL4", txt_RCC_Pier_RL4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "R.L. at Footing Bottom ", "RL5", txt_RCC_Pier_RL5.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Formation Level [RL1+d1+d2+H1+H2]", "FL", txt_RCC_Pier_Form_Lev.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Straight Depth of Footing", "H3", txt_RCC_Pier_H3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Varying Depth of Footing", "H4", txt_RCC_Pier_H4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Straight depth of Pier Cap", "H5", txt_RCC_Pier_H5.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Varying Depth of Pier Cap", "H6", txt_RCC_Pier_H6.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Height of Pier", "H7", txt_RCC_Pier_H7.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Overall Height of Substructure [H7 + H5 + H6]", "OHS", txt_RCC_Pier_overall_height.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Footing", "B7", txt_RCC_Pier_B7.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "P.C.C. Projection under  Footing on either side", "B8", txt_RCC_Pier_B8.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Longitudinal width of Pier at Base", "B9", txt_RCC_Pier_B9.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Transverse width of Pier at Base", "B10", txt_RCC_Pier_B10.Text));
            file_content.Add(string.Format(kFormat, "Longitudinal width of Pier at Top", "B11", txt_RCC_Pier_B11.Text));
            file_content.Add(string.Format(kFormat, "Transverse width of Pier at Top", "B12", txt_RCC_Pier_B12.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Pier Cap width in Longitudinal Direction", "B13", txt_RCC_Pier_B13.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Pier Cap width in Transverse Direction", "B14", txt_RCC_Pier___B.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_rcc_pier_fck.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Allowable Flexural Stress in Concrete", "σ_c", txt_rcc_pier_sigma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_rcc_pier_fy.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permissible Stress in Steel", "σ_st", txt_rcc_pier_sigma_st.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Modular Ratio", "m", txt_rcc_pier_m.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Standard Minimum Reinforcement", "p1", txt_RCC_Pier_p1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Design Trial Reinforcement", "p2", txt_RCC_Pier_p2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Reinforcement Cover", "d’", txt_RCC_Pier_d_dash.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Pier in Transverse direction", "D", txt_RCC_Pier_D.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Pier in Longitudinal direction", "b", txt_RCC_Pier_b.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Support Reaction on The Pier ", "W1", txt_RCC_Pier_W1_supp_reac.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Moment at Supports in Longitudinal Direction", "Mx1", txt_RCC_Pier_Mx1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Moment at Supports in Transverse Direction", "Mz1", txt_RCC_Pier_Mz1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Vehicle Live Load", "TVLL", txt_RCC_Pier_vehi_load.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));

            #endregion  RCC PIER FORM1 USER INPUT DATA

            #region RCC PIER FORM2 USER INPUT DATA

            file_content.Add(string.Format("RCC PIER FORM2 USER INPUT DATA"));
            file_content.Add(string.Format("------------------------------"));
            file_content.Add(string.Format(kFormat, "Dead Load Support Reaction for all Supports", "P2", txt_pier_2_P2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Live Load Support Reaction for all Supports", "P3", txt_pier_2_P3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance from Left Edge Pier Cap Edge to Left face of Pier", "B16", txt_pier_2_B16.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distances from Left Edge of Pier Cap to Centre of Each  pair of Pedestals ", "APD", txt_pier_2_APD.Text));
            //file_content.Add(string.Format(kFormat, "(seperated by comma ',' or space ' ')", "", txt_pier_2_P3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "(Get Distances of each pairs of pedestals  within the distance of B16)", "PD", txt_pier_2_PD.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "(Get Load Reactions on each pair of Pedestals =   Total Load Reaction / total Pairs )", "PL", txt_pier_2_PL.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "(Get Moments on each  Pedestal = Total Moment / total Pairs)", "PML", txt_pier_2_PML.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Seismic Coefficient", "SC", txt_pier_2_SC.Text));
            //file_content.Add(string.Format(kFormat, " (put value 0 if not required)", "", txt_pier_2_P3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Water from River Bed at High Flood", "HHF", txt_pier_2_HHF.Text));
            //file_content.Add(string.Format(kFormat, "(put value 0 if not required)", "", txt_pier_2_P3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Observed Velocity of water at High Flood", "V", txt_pier_2_V.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Pier Shape Constant", "k", txt_pier_2_k.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Coefficient of Friction between Concrete and River Bed", "CF", txt_pier_2_CF.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Breaking Force 20% of Live Load", "LL", txt_pier_2_LL.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Temperature Force on Each Bearing", "Vr", txt_pier_2_Vr.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Shirnkage Force on Each Bearing", "Itc", txt_pier_2_Itc.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Diameter of Reinforcement Bar", "sdia", txt_pier_2_sdia.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Shear Reinforcement Legs Nos.", "slegs", txt_pier_2_slegs.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Diameter of Longitudinal reinforcement Bars", "ldia", txt_pier_2_ldia.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Safe Bearing Capacity of River Bed Soil ", "SBC", txt_pier_2_SBC.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            #endregion  RCC PIER FORM2 USER INPUT DATA
            try
            {
                File.WriteAllLines(ASTRA_DATA_FILE, file_content.ToArray());
            }
            catch
            {
            }
        }
        public void Read_All_Data()
        {
            if (iApp.IsDemo) return;



            iApp.Read_Form_Record(this, user_path);
            return;



            string data_file = ASTRA_DATA_FILE;
            try
            {
                user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                //Deck.FilePath = user_path;
                Abut.FilePath = user_path;
                rcc_pier.FilePath = user_path;
            }
            catch (Exception ex) { }

            if (!File.Exists(data_file)) return;
            List<string> file_content = new List<string>(File.ReadAllLines(data_file));

            eSteelTrussOption TOpt = eSteelTrussOption.None;

            MyList mlist = null;
            MyList mlist_mov_ll = null;
            string kStr = "";
            for (int i = 0; i < file_content.Count; i++)
            {
                try
                {
                    kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                    mlist = new MyList(kStr, '=');
                    if (kStr.Contains("---")) continue;

                    #region Select Option
                    switch (kStr)
                    {
                        case "DECK SLAB USER INPUT":
                            TOpt = eSteelTrussOption.RCCDeckSlab;
                            break;
                        case "ABUTMENT USER INPUT":
                            TOpt = eSteelTrussOption.Abutment;
                            break;
                        case "RCC PIER FORM1 USER INPUT DATA":
                            TOpt = eSteelTrussOption.RCCPier_1;
                            break;
                        case "RCC PIER FORM2 USER INPUT DATA":
                            TOpt = eSteelTrussOption.RCCPier_2;
                            break;
                    }
                    #endregion Select Option

                 

                    if (mlist.Count == 3 )
                    {
                        try
                        {

                            kStr = mlist.StringList[1].Trim().TrimStart();
                            switch (TOpt)
                            {
                                #region Chiranjit Select Data
                       
                                case eSteelTrussOption.RCCDeckSlab:
                                    #region DECK SLAB USER INPUT
                                    switch (kStr)
                                    {
                                        case "L":
                                            txt_Deck_L.Text = mlist.StringList[2];
                                            break;
                                        case "B":
                                            txt_Deck_B.Text = mlist.StringList[2];
                                            break;
                                        case "fck":
                                            cmb_deck_fck.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_deck_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_cb":
                                            txt_deck_sigma_c.Text = mlist.StringList[2];
                                            break;
                                        case "σ_st":
                                            txt_Deck_sigma_st.Text = mlist.StringList[2];
                                            break;
                                        case "m":
                                            txt_deck_m.Text = mlist.StringList[2];
                                            break;
                                        case "j":
                                            txt_Deck_j.Text = mlist.StringList[2];
                                            break;
                                        case "Q":
                                            txt_Deck_Q.Text = mlist.StringList[2];
                                            break;
                                        case "mc":
                                            txt_Deck_minimum_cover.Text = mlist.StringList[2];
                                            break;
                                        case "SL":
                                            cmb_deck_select_load.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "AL":
                                            txt_Deck_applied_load.Text = mlist.StringList[2];
                                            break;
                                        case "a":
                                            txt_Deck_load_width.Text = mlist.StringList[2];
                                            break;
                                        case "b":
                                            txt_Deck_load_length.Text = mlist.StringList[2];
                                            break;
                                        case "IF":
                                            txt_Deck_impact_factor.Text = mlist.StringList[2];
                                            break;
                                        case "CF":
                                            txt_Deck_continuity_factor.Text = mlist.StringList[2];
                                            break;
                                        case "mu":
                                            txt_Deck_mu.Text = mlist.StringList[2];
                                            break;
                                        case "Ds":
                                            txt_Deck_Ds.Text = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_Deck_gamma_c.Text = mlist.StringList[2];
                                            break;
                                        case "Dwc":
                                            txt_Deck_Dwc.Text = mlist.StringList[2];
                                            break;
                                        case "γ_wc":
                                            txt_Deck_gamma_wc.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion DECK SLAB USER INPUT
                                    break;
                                case eSteelTrussOption.Abutment:
                                    #region ABUTMENT USER INPUT


                                    switch (kStr)
                                    {
                                        case "DMG":
                                            txt_abut_DMG.Text = mlist.StringList[2];
                                            break;
                                        case "t":
                                            txt_abut_t.Text = mlist.StringList[2];
                                            break;
                                        case "H":
                                            txt_abut_H.Text = mlist.StringList[2];
                                            break;
                                        case "a":
                                            txt_abut_a.Text = mlist.StringList[2];
                                            break;
                                        case "B":
                                            txt_abut_B.Text = mlist.StringList[2];
                                            break;
                                        case "d2":
                                            txt_abut_d2.Text = mlist.StringList[2];
                                            break;
                                        case "d3":
                                            txt_abut_d3.Text = mlist.StringList[2];
                                            break;
                                        case "d4":
                                            txt_abut_d4.Text = mlist.StringList[2];
                                            break;
                                        case "L1":
                                            txt_abut_L1.Text = mlist.StringList[2];
                                            break;
                                        case "L2":
                                            txt_abut_L2.Text = mlist.StringList[2];
                                            break;
                                        case "L3":
                                            txt_abut_L3.Text = mlist.StringList[2];
                                            break;
                                        case "L4":
                                            txt_abut_L4.Text = mlist.StringList[2];
                                            break;
                                        case "L":
                                            txt_abut_L.Text = mlist.StringList[2];
                                            break;
                                        case "θ":
                                            txt_abut_theta.Text = mlist.StringList[2];
                                            break;
                                        case "φ":
                                            txt_abut_phi.Text = mlist.StringList[2];
                                            break;
                                        case "z":
                                            txt_abut_z.Text = mlist.StringList[2];
                                            break;
                                        case "δ":
                                            txt_abut_delta.Text = mlist.StringList[2];
                                            break;
                                        case "µ":
                                            txt_abut_mu.Text = mlist.StringList[2];
                                            break;
                                        case "cc":
                                            txt_abut_cover.Text = mlist.StringList[2];
                                            break;
                                        case "fck":
                                            cmb_abut_fck.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_c":
                                            txt_abut_sigma_c.Text = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_abut_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_st":
                                            txt_abut_sigma_st.Text = mlist.StringList[2];
                                            break;
                                        case "p":
                                            txt_abut_p_bearing_capacity.Text = mlist.StringList[2];
                                            break;
                                        case "h1":
                                            txt_abut_h1.Text = mlist.StringList[2];
                                            break;
                                        case "Fact":
                                            txt_abut_fact.Text = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_abut_gamma_c.Text = mlist.StringList[2];
                                            break;
                                        case "sc":
                                            txt_abut_sc.Text = mlist.StringList[2];
                                            break;
                                        case "w6":
                                            txt_abut_w6.Text = mlist.StringList[2];
                                            break;
                                        case "w5":
                                            txt_abut_w5.Text = mlist.StringList[2];
                                            break;
                                        case "F":
                                            txt_abut_F.Text = mlist.StringList[2];
                                            break;
                                        case "γ_b":
                                            txt_abut_gamma_b.Text = mlist.StringList[2];
                                            break;

                                    }
                                    #endregion ABUTMENT USER INPUT
                                    break;
                                case eSteelTrussOption.RCCPier_1:
                                    #region RCC PIER FORM1 USER INPUT DATA

                                    switch (kStr)
                                    {
                                        case "L":
                                            txt_RCC_Pier_L.Text = mlist.StringList[2];
                                            break;
                                        case "w1":
                                            txt_RCC_Pier_CW.Text = mlist.StringList[2];
                                            break;
                                        case "w2":
                                            txt_RCC_Pier__B.Text = mlist.StringList[2];
                                            break;
                                        case "w3":
                                            txt_RCC_Pier_Wp.Text = mlist.StringList[2];
                                            break;
                                        case "a1":
                                            txt_RCC_Pier_Hp.Text = mlist.StringList[2];
                                            break;
                                        case "NB":
                                            txt_RCC_Pier_NMG.Text = mlist.StringList[2];
                                            break;
                                        case "d1":
                                            txt_RCC_Pier_DMG.Text = mlist.StringList[2];
                                            break;
                                        case "d2":
                                            txt_RCC_Pier_Ds.Text = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_RCC_Pier_gama_c.Text = mlist.StringList[2];
                                            break;
                                        case "B1":
                                            txt_RCC_Pier_B1.Text = mlist.StringList[2];
                                            break;
                                        case "B2":
                                            txt_RCC_Pier_B2.Text = mlist.StringList[2];
                                            break;
                                        case "H1":
                                            txt_RCC_Pier_H1.Text = mlist.StringList[2];
                                            break;
                                        case "NP":
                                            txt_RCC_Pier_NP.Text = mlist.StringList[2];
                                            break;
                                        case "NR":
                                            txt_RCC_Pier_NR.Text = mlist.StringList[2];
                                            break;
                                        case "B3":
                                            txt_RCC_Pier_B3.Text = mlist.StringList[2];
                                            break;
                                        case "B4":
                                            txt_RCC_Pier_B4.Text = mlist.StringList[2];
                                            break;
                                        case "H2":
                                            txt_RCC_Pier_H2.Text = mlist.StringList[2];
                                            break;
                                        case "B5":
                                            txt_RCC_Pier_B5.Text = mlist.StringList[2];
                                            break;
                                        case "B6":
                                            txt_RCC_Pier_B6.Text = mlist.StringList[2];
                                            break;
                                        case "RL1":
                                            txt_RCC_Pier_RL1.Text = mlist.StringList[2];
                                            break;
                                        case "RL2":
                                            txt_RCC_Pier_RL2.Text = mlist.StringList[2];
                                            break;
                                        case "RL3":
                                            txt_RCC_Pier_RL3.Text = mlist.StringList[2];
                                            break;
                                        case "RL4":
                                            txt_RCC_Pier_RL4.Text = mlist.StringList[2];
                                            break;
                                        case "RL5":
                                            txt_RCC_Pier_RL5.Text = mlist.StringList[2];
                                            break;
                                        case "FL":
                                            txt_RCC_Pier_Form_Lev.Text = mlist.StringList[2];
                                            break;
                                        case "H3":
                                            txt_RCC_Pier_H3.Text = mlist.StringList[2];
                                            break;
                                        case "H4":
                                            txt_RCC_Pier_H4.Text = mlist.StringList[2];
                                            break;
                                        case "H5":
                                            txt_RCC_Pier_H5.Text = mlist.StringList[2];
                                            break;
                                        case "H6":
                                            txt_RCC_Pier_H6.Text = mlist.StringList[2];
                                            break;
                                        case "H7":
                                            txt_RCC_Pier_H7.Text = mlist.StringList[2];
                                            break;
                                        case "OHS":
                                            txt_RCC_Pier_overall_height.Text = mlist.StringList[2];
                                            break;
                                        case "B7":
                                            txt_RCC_Pier_B7.Text = mlist.StringList[2];
                                            break;
                                        case "B8":
                                            txt_RCC_Pier_B8.Text = mlist.StringList[2];
                                            break;
                                        case "B9":
                                            txt_RCC_Pier_B9.Text = mlist.StringList[2];
                                            break;
                                        case "B10":
                                            txt_RCC_Pier_B10.Text = mlist.StringList[2];
                                            break;
                                        case "B11":
                                            txt_RCC_Pier_B11.Text = mlist.StringList[2];
                                            break;
                                        case "B12":
                                            txt_RCC_Pier_B12.Text = mlist.StringList[2];
                                            break;
                                        case "B13":
                                            txt_RCC_Pier_B13.Text = mlist.StringList[2];
                                            break;
                                        case "B14":
                                            txt_RCC_Pier___B.Text = mlist.StringList[2];
                                            break;
                                        case "fck":
                                            cmb_rcc_pier_fck.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_c":
                                            txt_rcc_pier_sigma_c.Text = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_rcc_pier_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_st":
                                            txt_rcc_pier_sigma_st.Text = mlist.StringList[2];
                                            break;
                                        case "m":
                                            txt_rcc_pier_m.Text = mlist.StringList[2];
                                            break;
                                        case "p1":
                                            txt_RCC_Pier_p1.Text = mlist.StringList[2];
                                            break;
                                        case "p2":
                                            txt_RCC_Pier_p2.Text = mlist.StringList[2];
                                            break;
                                        case "d’":
                                            txt_RCC_Pier_d_dash.Text = mlist.StringList[2];
                                            break;
                                        case "D":
                                            txt_RCC_Pier_D.Text = mlist.StringList[2];
                                            break;
                                        case "b":
                                            txt_RCC_Pier_b.Text = mlist.StringList[2];
                                            break;
                                        case "W1":
                                            txt_RCC_Pier_W1_supp_reac.Text = mlist.StringList[2];
                                            break;
                                        case "Mx1":
                                            txt_RCC_Pier_Mx1.Text = mlist.StringList[2];
                                            break;
                                        case "Mz1":
                                            txt_RCC_Pier_Mz1.Text = mlist.StringList[2];
                                            break;
                                        case "TVLL":
                                            txt_RCC_Pier_vehi_load.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion RCC PIER FORM1 USER INPUT DATA
                                    break;
                                case eSteelTrussOption.RCCPier_2:
                                    #region RCC PIER FORM2 USER INPUT DATA
                                    switch (kStr)
                                    {
                                        case "P2":
                                            txt_pier_2_P2.Text = mlist.StringList[2];
                                            break;

                                        case "P3":
                                            txt_pier_2_P3.Text = mlist.StringList[2];
                                            break;

                                        case "B16":
                                            txt_pier_2_B16.Text = mlist.StringList[2];
                                            break;

                                        case "APD":
                                            txt_pier_2_APD.Text = mlist.StringList[2];
                                            break;

                                        case "PD":
                                            txt_pier_2_PD.Text = mlist.StringList[2];
                                            break;

                                        case "PL":
                                            txt_pier_2_PL.Text = mlist.StringList[2];
                                            break;

                                        case "PML":
                                            txt_pier_2_PML.Text = mlist.StringList[2];
                                            break;

                                        case "SC":
                                            txt_pier_2_SC.Text = mlist.StringList[2];
                                            break;

                                        case "HHF":
                                            txt_pier_2_HHF.Text = mlist.StringList[2];
                                            break;

                                        case "V":
                                            txt_pier_2_V.Text = mlist.StringList[2];
                                            break;

                                        case "k":
                                            txt_pier_2_k.Text = mlist.StringList[2];
                                            break;

                                        case "CF":
                                            txt_pier_2_CF.Text = mlist.StringList[2];
                                            break;

                                        case "LL":
                                            txt_pier_2_LL.Text = mlist.StringList[2];
                                            break;

                                        case "Vr":
                                            txt_pier_2_Vr.Text = mlist.StringList[2];
                                            break;

                                        case "Itc":
                                            txt_pier_2_Itc.Text = mlist.StringList[2];
                                            break;

                                        case "sdia":
                                            txt_pier_2_sdia.Text = mlist.StringList[2];
                                            break;

                                        case "slegs":
                                            txt_pier_2_slegs.Text = mlist.StringList[2];
                                            break;

                                        case "ldia":
                                            txt_pier_2_ldia.Text = mlist.StringList[2];
                                            break;

                                        case "SBC":
                                            txt_pier_2_SBC.Text = mlist.StringList[2];
                                            break;

                                    }
                                    #endregion RCC PIER FORM2 USER INPUT DATA
                                    break;
                                #endregion Chiranjit Select Data
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR : " + kStr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + kStr);
                }

            }

        }
        #endregion Chiranjit [2012 07 10]

        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_L.Text = "0.0";
                if (IsWarren2)
                {
                    txt_L.Text = "50.0";
                    txt_B.Text = "9.1";
                    txt_H.Text = "6.5";
                }
                else
                {
                    if (isCreateData)
                    {
                        txt_B.Text = "8.43";
                        txt_L.Text = "60.0";
                        txt_H.Text = "6.35";
                        cmb_panel_nos.Text = "10";
                        txt_stringers_nos.Text = "4";
                    }
                }
                //string str = "ASTRA USB Lock not found at any port.\n\nThis is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "  Website  : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "  Email at : techsoft@consultant.com, dataflow@mail.com\n\n";
                //str += "Contact No : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]

        private void chk_inverted_CheckedChanged(object sender, EventArgs e)
        {
            Button_Enable_Disable();
        }

        bool blink = false;
        short prss = 0;
        private void tmr_blink_Tick(object sender, EventArgs e)
        {
            tmr_blink.Stop();
            return;
            Panel pnl = null;

            //pnl_create_data.BackColor = Color.White;
            //pnl_save_section.BackColor = Color.White;
            //pnl_process_analysis.BackColor = Color.White;
            //pnl_process_design.BackColor = Color.White;

            if (rbtn_create_analysis_file.Checked)
                pnl_browse.BackColor = Color.White;
            else
                pnl_create_data.BackColor = Color.White;



            if (prss == 0)
            {
                if (rbtn_create_analysis_file.Checked)
                {
                    pnl = pnl_create_data;
                }
                else
                {
                    pnl = pnl_browse;
                }



                //pnl_create_data.BackColor = Color.White;
                pnl_save_section.BackColor = Color.White;
                pnl_process_analysis.BackColor = Color.White;
                pnl_process_design.BackColor = Color.White;

            }
            else if (prss == 1)
            {
                pnl = pnl_save_section;
                pnl_create_data.BackColor = Color.White;
                //pnl_save_section.BackColor = Color.White;
                pnl_process_analysis.BackColor = Color.White;
                pnl_process_design.BackColor = Color.White;
            }
            else if (prss == 2)
            {
                pnl = pnl_process_analysis;
                pnl_create_data.BackColor = Color.White;
                pnl_save_section.BackColor = Color.White;
                //pnl_process_analysis.BackColor = Color.White;
                pnl_process_design.BackColor = Color.White;
            }
            else if (prss == 3)
            {
                pnl = pnl_process_design;
                pnl_create_data.BackColor = Color.White;
                pnl_save_section.BackColor = Color.White;
                pnl_process_analysis.BackColor = Color.White;
                //pnl_process_design.BackColor = Color.White;
            }
            else
            {
                pnl_create_data.BackColor = Color.White;
                pnl_save_section.BackColor = Color.White;
                pnl_process_analysis.BackColor = Color.White;
                pnl_process_design.BackColor = Color.White;
            }


            if (prss < 4)
            {
                pnl.BackColor = blink ? Color.White : Color.Red;
                blink = !blink;
            }
        }

        private void txt_pier_2_APD_TextChanged(object sender, EventArgs e)
        {

            txt_pier_2_APD.TextAlign = HorizontalAlignment.Left;
            txt_pier_2_APD.WordWrap = true;

            double b16 = MyList.StringToDouble(txt_pier_2_B16.Text, 0.0);

            string kStr = txt_pier_2_APD.Text.Replace(",", " ").Trim().TrimEnd().TrimStart();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            kStr = "";
            try
            {
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (mlist.GetDouble(i) < b16)
                    {
                        kStr += mlist.StringList[i] + ",";
                    }
                }
                kStr = kStr.Substring(0, kStr.Length - 1);
            }
            catch (Exception ex) { }

            txt_pier_2_PD.Text = kStr;
        }

        private void rbtn_ssprt_pinned_CheckedChanged(object sender, EventArgs e)
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

        private void chk_edit_forces_CheckedChanged(object sender, EventArgs e)
        {
            Set_Force_Input_Color();
        }
        public void Update_Forces()
        {
            string kStr = "";
            CMember mbr;
            for (int i = 0; i < dgv_member_Results_others.RowCount; i++)
            {
                kStr = dgv_member_Results_others[0, i].Value.ToString();

                if (null == dgv_member_Results_others[2, i].Value)
                    dgv_member_Results_others[2, i].Value = "";
                if (null == dgv_member_Results_others[5, i].Value)
                    dgv_member_Results_others[5, i].Value = "";
                if (null == dgv_member_Results_others[8, i].Value)
                    dgv_member_Results_others[8, i].Value = "";
                if (null == dgv_member_Results_others[11, i].Value)
                    dgv_member_Results_others[11, i].Value = "";

                mbr = Complete_Design.Members.Get_Member(kStr);

                mbr.MaxCompForce = MyList.StringToDouble(dgv_member_Results_others[2, i].Value.ToString(), 0.0);
                mbr.MaxTensionForce = MyList.StringToDouble(dgv_member_Results_others[5, i].Value.ToString(), 0.0);
                mbr.MaxBendingMoment = MyList.StringToDouble(dgv_member_Results_others[8, i].Value.ToString(), 0.0);
                mbr.MaxShearForce = MyList.StringToDouble(dgv_member_Results_others[11, i].Value.ToString(), 0.0);
            }


            for (int i = 0; i < dgv_member_Results_arch_cables.RowCount; i++)
            {
                kStr = dgv_member_Results_arch_cables[0, i].Value.ToString();

                if (null == dgv_member_Results_arch_cables[2, i].Value)
                    dgv_member_Results_arch_cables[2, i].Value = "";
                if (null == dgv_member_Results_arch_cables[3, i].Value)
                    dgv_member_Results_arch_cables[3, i].Value = "";
                if (null == dgv_member_Results_arch_cables[4, i].Value)
                    dgv_member_Results_arch_cables[4, i].Value = "";
                if (null == dgv_member_Results_arch_cables[8, i].Value)
                    dgv_member_Results_arch_cables[8, i].Value = "";

                mbr = Complete_Design.Members.Get_Member(kStr);

                mbr.MaxAxialForce = MyList.StringToDouble(dgv_member_Results_arch_cables[2, i].Value.ToString(), 0.0);
                mbr.MaxBendingMoment = MyList.StringToDouble(dgv_member_Results_arch_cables[3, i].Value.ToString(), 0.0);
                mbr.MaxTorsion = MyList.StringToDouble(dgv_member_Results_arch_cables[4, i].Value.ToString(), 0.0);
                mbr.MaxTensionForce = MyList.StringToDouble(dgv_member_Results_arch_cables[8, i].Value.ToString(), 0.0);
            }

        }

        private void Set_Force_Input_Color()
        {
            dgv_member_Results_arch_cables.ReadOnly = !chk_edit_forces.Checked;
            dgv_member_Results_others.ReadOnly = !chk_edit_forces.Checked;
            if (dgv_member_Results_others.ReadOnly) return;
            for (int i = 0; i < dgv_member_Results_others.RowCount; i++)
            {

                if (dgv_member_Results_others[1, i].Value.ToString().ToUpper().StartsWith("CROSS") ||
                    dgv_member_Results_others[1, i].Value.ToString().ToUpper().StartsWith("STRING"))
                {
                    dgv_member_Results_others[2, i].Style.BackColor = Color.White;
                    dgv_member_Results_others[5, i].Style.BackColor = Color.White;
                    dgv_member_Results_others[8, i].Style.BackColor = Color.Yellow;
                    dgv_member_Results_others[11, i].Style.BackColor = Color.Yellow;
                }
                else
                {
                    dgv_member_Results_others[2, i].Style.BackColor = Color.Yellow;
                    dgv_member_Results_others[5, i].Style.BackColor = Color.Yellow;
                    dgv_member_Results_others[8, i].Style.BackColor = Color.White;
                    dgv_member_Results_others[11, i].Style.BackColor = Color.White;
                }


                if (!dgv_member_Results_others.ReadOnly)
                {
                    dgv_member_Results_others[2, i].Style.ForeColor = Color.Red;
                    dgv_member_Results_others[5, i].Style.ForeColor = Color.Red;
                    dgv_member_Results_others[8, i].Style.ForeColor = Color.Red;
                    dgv_member_Results_others[11, i].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv_member_Results_others[2, i].Style.ForeColor = Color.Black;
                    dgv_member_Results_others[5, i].Style.ForeColor = Color.Black;
                    dgv_member_Results_others[8, i].Style.ForeColor = Color.Black;
                    dgv_member_Results_others[11, i].Style.ForeColor = Color.Black;
                }
            }

            for (int i = 0; i < dgv_member_Results_arch_cables.RowCount; i++)
            {

                if (dgv_member_Results_arch_cables[1, i].Value.ToString().ToUpper().StartsWith("ARCH") ||
                    dgv_member_Results_arch_cables[1, i].Value.ToString().ToUpper().StartsWith("TRANS"))
                {
                    dgv_member_Results_arch_cables[2, i].Style.BackColor = Color.Yellow;
                    dgv_member_Results_arch_cables[3, i].Style.BackColor = Color.Yellow;
                    dgv_member_Results_arch_cables[4, i].Style.BackColor = Color.Yellow;
                    dgv_member_Results_arch_cables[8, i].Style.BackColor = Color.White;
                }
                else
                {
                    dgv_member_Results_arch_cables[2, i].Style.BackColor = Color.White;
                    dgv_member_Results_arch_cables[3, i].Style.BackColor = Color.White;
                    dgv_member_Results_arch_cables[4, i].Style.BackColor = Color.White;
                    dgv_member_Results_arch_cables[8, i].Style.BackColor = Color.Yellow;
                }


                if (!dgv_member_Results_arch_cables.ReadOnly)
                {
                    dgv_member_Results_arch_cables[2, i].Style.ForeColor = Color.Red;
                    dgv_member_Results_arch_cables[3, i].Style.ForeColor = Color.Red;
                    dgv_member_Results_arch_cables[4, i].Style.ForeColor = Color.Red;
                    dgv_member_Results_arch_cables[8, i].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv_member_Results_arch_cables[2, i].Style.ForeColor = Color.Black;
                    dgv_member_Results_arch_cables[3, i].Style.ForeColor = Color.Black;
                    dgv_member_Results_arch_cables[4, i].Style.ForeColor = Color.Black;
                    dgv_member_Results_arch_cables[8, i].Style.ForeColor = Color.Black;
                }
            }
        }

        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {

            iApp.Show_LL_Dialog();
            iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
        }

        private void btn_arch_new_design_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_arch_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, user_path);
                    txt_project_name.Text = Path.GetFileName(user_path);
                    //Open_Project();


                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;
                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As



                    Open_Project();
                    iApp.user_path = user_path;

                    txt_project_name.Text = Path.GetFileName(user_path);

                    Write_All_Data();

                }
            }
            else if (btn.Name == btn_arch_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_project_name.Text != "")
                //    frm.Project_Name = txt_project_name.Text;
                //else
                //    frm.Project_Name = "Design of Arch Cable Suspension Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_project_name.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    isCreateData = true;
                //}

                isCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }

        #region Chiranjit [2016 09 07]

        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Arch_Cable_Suspension_Bridge;
            }
        }

        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }
        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                    "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            Write_All_Data();

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            txt_project_name.Text = prj_name;

        }

        public void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

        #endregion Chiranjit [2016 09 07]

    }
}
