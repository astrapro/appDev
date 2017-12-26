using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraInterface.TrussBridge;
using AstraFunctionOne.BridgeDesign;

using BridgeAnalysisDesign;
using AstraAccess.SAP_Classes;

using BridgeAnalysisDesign.CableStayed;
using LimitStateMethod.SuspensionBridge;

using LimitStateMethod.CableStayed;


namespace LimitStateMethod.Extradossed
{
    public partial class UC_CableStayedDesign : UserControl
    {
        public IApplication iApp = null;

       //public CABLE_STAYED_LS_Analysis CS_Analysis { get; set; }
        public event EventHandler OnButtonClick;

       public CABLE_STAYED_Extradosed_LS_Analysis CS_Analysis { get; set; }

        List<MemberSectionProperty> SectionProperty { get; set; }
        List<LoadData> LoadList = new List<LoadData>();



        CableStayedDeckSlab Deck = null;



        List<string> list_member_group = new List<string>();
        //string Title = "ANALYSIS OF CABLE STAYED BRIDGE";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "CABLE STAYED BRIDGE [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "CABLE STAYED BRIDGE [LRFD]";
                return "CABLE STAYED BRIDGE [IRC]";
            }
        }



        BridgeMemberAnalysis Bridge_Analysis = null;
        bool IsCreateData = true;
        public int img_counter = 0;

        public List<string> Results { get; set; }
        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(user_path, "LL.TXT");
            }
        }

        public string Cable_Nos_Text { get { return txt_cbl_des_mem_nos.Text; } set { txt_cbl_des_mem_nos.Text = value; } }
 

        List<LoadData> Live_Load_List { get; set; }
        public string CSB_DATA_File
        {
            get
            {
                
                return Path.Combine(user_path, "CSB_DATA.TXT");
            }
        }

        #region Properties
       


        public string Analysis_Report
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "ANALYSIS_REP.TXT");
                return "";
            }

        }
        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "CSB_DRAWINGS");
                return "";
            }

        }
        public string Cable_Design_Report
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "CABLE_DESIGN_REP.TXT");
                return "";
            }

        }
        public string Analysis_Result_Report
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
                return "";
            }

        }
        public string user_path
        {
            get
            {
                if (iApp == null) return "";
                return iApp.user_path;
            }
            set
            {

                if (iApp != null)
                iApp.user_path = value;
            }
        }


        public double Cable_D
        {
            get { return MyList.StringToDouble(txt_cbl_des_d.Text, 0.0); }
            set { txt_cbl_des_d.Text = value.ToString("f3"); }
        }
        public double Cable_Ax
        {
            get { return (Math.PI * Cable_D * Cable_D) / 4.0; }
        }
        public double Cable_Gamma
        {
            get { return MyList.StringToDouble(txt_cbl_des_gamma.Text, 0.0); }
            set { txt_cbl_des_gamma.Text = value.ToString("f3"); }
        }
        public double Cable_E
        {
            get { return MyList.StringToDouble(txt_cbl_des_E.Text, 0.0); }
            set { txt_cbl_des_E.Text = value.ToString("E3"); }
        }
        public double Cable_f
        {
            get { return MyList.StringToDouble(txt_cbl_des_f.Text, 0.0); }
            set { txt_cbl_des_f.Text = value.ToString("f3"); }
        }

      
        #endregion Properties

        #region Members
        MemberCollection long_girders = null;
        MemberCollection cross_girders = null;
        MemberCollection cables = null;
        MemberCollection pylon = null;
        MemberCollection tie_beam = null;

        CableMemberCollection Cable_Members { get; set; }
        #endregion Members

        #region User Events
        public UC_CableStayedDesign(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = app.LastDesignWorkingFolder;
            SectionProperty = new List<MemberSectionProperty>();
            long_girders = new MemberCollection();
            cross_girders = new MemberCollection();
            cables = new MemberCollection();
            pylon = new MemberCollection();
            tie_beam = new MemberCollection();

            Cable_Members = new CableMemberCollection();

            Deck = new CableStayedDeckSlab(iApp);
        }
        public UC_CableStayedDesign()
        {
            InitializeComponent();
            //iApp = app;
            //user_path = app.LastDesignWorkingFolder;
            SectionProperty = new List<MemberSectionProperty>();
            long_girders = new MemberCollection();
            cross_girders = new MemberCollection();
            cables = new MemberCollection();
            pylon = new MemberCollection();
            tie_beam = new MemberCollection();

            Cable_Members = new CableMemberCollection();

            Deck = new CableStayedDeckSlab(iApp);



            tc_main.TabPages.Remove(tab_non_linear);
        }

        List<int> HA_Loading_Members = new List<int>();
        List<double> HA_Dists = new List<double>();

        public int Get_Index(List<double> list, double val)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Math.Abs(list[i] - val) < 0.9) return i;
                if (list[i].ToString("f2") == val.ToString("f2")) return i;
            }
            return -1;
        }

        public void Button_Enable_Disable()
        {
        }

        public void Design_Cables(ref CableMember mem)
        {
            double Ax = Cable_Ax;
            double gamma = Cable_Gamma;
            double alpha_x = mem.InclinationAngle * (Math.PI / 180);
            double na = 0.0;
            double n = 0.0;
            double a = 0.0;
            double E = Cable_E;
            double Fx = 0.0;
            double f = Cable_f;

            double x1, y1, z1;
            double x2, y2, z2;
            double x3, y3, z3;

            Fx = mem.AnalysisForce.Force;

            x1 = mem.MemberDetails.StartNode.X;
            y1 = mem.MemberDetails.StartNode.Y;
            z1 = mem.MemberDetails.StartNode.Z;

            x2 = mem.MemberDetails.EndNode.X;
            y2 = mem.MemberDetails.EndNode.Y;
            z2 = mem.MemberDetails.EndNode.Z;


            x3 = x1;
            y3 = y2;
            z3 = z2;

            List<string> list = new List<string>();
            //list.Add(string.Format("Cross Sectional Area of Cable [Ax] = {0} sq.mm", Ax));
            //list.Add(string.Format("Angle of Inclination of Cable [alpha_x] = {0} ", alpha_x));
            //list.Add(string.Format("Horizontal Projection of Cable Length [na] = {0} ", na));
            //list.Add(string.Format("Number of Panels up to the Cable = {0} ", n));
            //list.Add(string.Format("Length of Each Panel = {0} "));
            //list.Add(string.Format("Elastic Modulus of Cable Material = {0} "));
            //list.Add(string.Format("Force in the Cable [Fx] = {0} ", Fx));

            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User's Cable No : {0}", mem.User_MemberNo));
            list.Add(string.Format("ASTRA's Cable No : {0} ", mem.ASTRA_MemberNo));
            list.Add(string.Format("Joint Coorinate at Upper End : {0} [x1 : {1:f3}, y1 : {2:f3}, z1 : {3:f3}]", mem.StartJointNo, mem.StartJoint_X, mem.StartJoint_Y, mem.StartJoint_Z));
            list.Add(string.Format("Joint Coorinate at Lower End : {0} [x2 : {1:f3}, y2 : {2:f3}, z2 : {3:f3}]", mem.EndJointNo, mem.EndJoint_X, mem.EndJoint_Y, mem.EndJoint_Z));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Cable Cross Section Diameter = d = {0:f3} m", Cable_D));
            list.Add(string.Format("Provided Cable Cross Section Area     = π*d^2/4 = {0:E3} sq.m", Cable_Ax));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Permissible Shear Stress [f] = {0} N/sq.mm = {1} Ton/sq.m", f, (f = f * 100)));
            list.Add(string.Format("Specific Weight of Cable Material [γ] = {0} Ton/cu.m", gamma));
            list.Add(string.Format(""));


            list.Add(string.Format("Specific Weight of Cable [γ] = {0} Ton/cu.m", gamma));
            list.Add(string.Format(""));
            list.Add(string.Format("Analysis Force :  {0:E3} Ton           [Member No :{1}, LoadCase : {2}]", mem.AnalysisForce, mem.AnalysisForce.MemberNo, mem.AnalysisForce.Loadcase));
            list.Add(string.Format("Analysis Stress : {0:E3} Ton/sq.m      [Member No :{1}, LoadCase : {2}]", mem.AnalysisStress, mem.AnalysisStress.MemberNo, mem.AnalysisStress.Loadcase));
            list.Add(string.Format(""));
            list.Add(string.Format("Pylon Coordinate at Deck : {0} m", mem.StartJoint_Y));
            list.Add(string.Format(""));
            double Lx = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
            double Lh = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3) + (z2 - z3) * (z2 - z3));
            double Lv = Math.Sqrt((x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3) + (z1 - z3) * (z1 - z3));
            //list.Add(string.Format("Length of Cable = Lx = SQRT((x1 - x2)^2 + (y1 - y2)^2 + (z1 - z2)^2) = {0:f3} m", Lx));
            list.Add(string.Format("Length of Cable = Lx = {0:f3} m", Lx));
            list.Add(string.Format(""));
            //list.Add(string.Format("Length of Horizontal Projection = Lh = SQRT((x2 - x3)^2 + (y2 - y3)^2 + (z2 - z3)^2) = {0:f3} m", Lh));
            list.Add(string.Format("Length of Horizontal Projection = Lh = {0:f3} m", Lh));
            list.Add(string.Format(""));
            //list.Add(string.Format("Length of Horizontal Projection = Lv = SQRT((x1 - x3)^2 + (y1 - y3)^2 + (z1 - z3)^2) = {0:f3} m", Lv));
            list.Add(string.Format("Length of Vertical Projection = Lv = {0:f3} m", Lv));
            list.Add(string.Format(""));
            alpha_x = mem.MemberDetails.InclinationAngle_in_Radian;
            list.Add(string.Format("Inclination Angle of Cable = α_x"));
            list.Add(string.Format("                           = tan^(-1)[(y2-y1)/(x2-x1)]", Lv));
            list.Add(string.Format("                           = {0:f0}° (degree)", mem.InclinationAngle));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double Pu = Fx * Math.Sin(alpha_x);
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Component of Force = Pu = Fx * Sin α_x"));
            list.Add(string.Format(""));
            list.Add(string.Format("                                 = {0:E3} * {1:F3}", Fx, Math.Sin(alpha_x)));
            list.Add(string.Format("                                 = {0:E3} Ton", Pu));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Ax = Fx / f;
            list.Add(string.Format("Required Cross Section Area = Ax = Fx / f"));
            list.Add(string.Format("                                 = {0:E3} / {1}", Fx, f));
            list.Add(string.Format("                                 = {0:E3} sq.m", Ax));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Cable Cross Section Area = {0:E3} sq.m", Cable_Ax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double W = Cable_Ax * Lx * gamma;
            list.Add(string.Format("Weight of Cable = W = Ax * Lx * γ "));
            list.Add(string.Format("                = {0:E3} * {1:f3} * {2}", Cable_Ax, Lx, gamma));
            list.Add(string.Format("                = {0:E3} Ton", W));
            list.Add(string.Format(""));









            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Calculated Force = Fx"));
            //list.Add(string.Format("                 = Pu/Sin α_x"));
            //list.Add(string.Format("                 = {0:E3}/{1:f3}", Pu, Math.Sin(alpha_x)));
            //list.Add(string.Format("                 = {0:E3} Ton", Fx));
            //list.Add(string.Format(""));

            mem.CalculatedStress = Fx / Ax;
            //list.Add(string.Format("Calculated Stress = Fx / Ax "));
            //list.Add(string.Format("                  =  Pu/(Sin α_x * Ax)"));
            //list.Add(string.Format("                  =  {0:E3}/({1:F3} * {2:E3})", Pu, Math.Sin(alpha_x), Ax));
            //list.Add(string.Format("                  =  {0:E3} Ton/sq.m", mem.CalculatedStress));
            list.Add(string.Format(""));
            list.Add(string.Format("E = Stress / Strain"));
            mem.Strain = (Fx / Cable_Ax) / E;
            list.Add(string.Format("Strain = Stress / E  "));
            list.Add(string.Format("       = (Fx / Ax) / E  "));
            list.Add(string.Format("       = ({0:E3}/{1:E3}) / {2:E3}", Fx, Cable_Ax, E));
            list.Add(string.Format("       = {0:E3} ", mem.Strain));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Strain  = Elongation / Lx"));

            mem.Elongation = mem.Strain * Lx;
            list.Add(string.Format(""));

            list.Add(string.Format("Elongation in Cable = δx = Strain * Lx"));
            list.Add(string.Format("                         = {0:E3} * {1:E3} ", mem.Strain, Lx));
            list.Add(string.Format("                         = {0:E3}  m", mem.Elongation));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Deflection of Deck at joint x2 = δhn = δx / Sin α_x"));
            list.Add(string.Format("                                              = {0:E3} / {1:f3}", mem.Elongation, Math.Sin(alpha_x)));
            list.Add(string.Format("                                              = {0:E3} m", mem.Vertical_Deflection_at_Deck));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Deflection at Pylon Top      = δhx = δx/Cos α_x "));
            list.Add(string.Format("                                              = {0:E3} / {1:f3}", mem.Elongation, Math.Cos(alpha_x)));
            list.Add(string.Format("                                              = {0:E3} m", mem.Horizontal_Deflection_at_Pylon_Top));
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add("");
            list.Add("");

            mem.DesignResult.Clear();
            mem.DesignResult.AddRange(list.ToArray());

        }

        public void Read_Cable_Member()
        {
            //Input_Data

            if(CS_Analysis == null)
            {
                return;
            }
            //Results.Clear();
            List<BridgeMemberAnalysis> lst_ana = new List<BridgeMemberAnalysis>();

            if (CS_Analysis.DeadLoad_Analysis != null) lst_ana.Add(CS_Analysis.DeadLoad_Analysis);

            lst_ana.AddRange(CS_Analysis.All_LL_Analysis);

            //if (CS_Analysis.TotalLoad_Analysis != null) lst_ana.Add(CS_Analysis.TotalLoad_Analysis);



            //if (CS_Analysis.TotalLoad_Analysis == null)
                CS_Analysis.TotalLoad_Analysis = lst_ana[0];
            Bridge_Analysis = CS_Analysis.TotalLoad_Analysis;


            if(Bridge_Analysis.MemberAnalysis == null)
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            List<int> list_mem = null;

            string kStr = MyList.RemoveAllSpaces(txt_cbl_des_mem_nos.Text.Trim().ToUpper());
            List<int> non_cable_mem = new List<int>();
            if (kStr == "ALL")
            {
                kStr = "1 TO " + Bridge_Analysis.Analysis.Members.Count;

            }
            list_mem = MyList.Get_Array_Intiger(kStr);

            CableMember cbl = new CableMember(); dgv_cable_design.Rows.Clear();
            Cable_Members.Clear();
            foreach (var item in list_mem)
            {
                cbl = new CableMember();
                cbl.User_MemberNo = item;

                AnalysisData ana = (AnalysisData)Bridge_Analysis.MemberAnalysis[item];
                if (ana != null)
                {
                    cbl.ASTRA_MemberNo = ana.AstraMemberNo;
                }

                cbl.MemberDetails = Bridge_Analysis.Analysis.Members.GetMember(item);

                List<int> l_int = new List<int>();

                l_int.Add(cbl.StartJointNo);
                l_int.Add(cbl.EndJointNo);


                CMember mem = new CMember();

                mem.Group.MemberNos.Add(item);

                mem.Result = Bridge_Analysis.GetForce(ref mem);
                if (mem.MaxTensionForce != null)
                {
                    cbl.AnalysisForce = (mem.MaxTensionForce.Force == 0.0) ? mem.MaxCompForce : mem.MaxTensionForce;
                }
                if (mem.MaxStress != null)
                {
                    cbl.AnalysisStress = mem.MaxStress;
                }
                if (cbl.MemberDetails != null)
                {
                    if (cbl.InclinationAngle > 0 && cbl.InclinationAngle < 90)
                    {
                        Cable_Members.Add(cbl);
                        dgv_cable_design.Rows.Add(cbl.ToArray_Extradosed());

                    }
                    else
                        non_cable_mem.Add(item);
                }
            }
            if (kStr == "")
            {
                MessageBox.Show(this, "Please put member nos.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_cbl_des_mem_nos.Focus();
            }
            else if (non_cable_mem.Count != 0)
            {
                kStr = MyList.Get_Array_Text(non_cable_mem);


                //kStr = kStr.Replace(" ", ", ");
                MessageBox.Show(this, "Member No(s) : " + kStr + " are not Cable Member", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dgv_cable_design.RowCount == 0)
            {
                MessageBox.Show(this, "Member No(s) : " + kStr + " are not Cable Member", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            grb_members.Text = "Total " + dgv_cable_design.RowCount + " Members in the List.";
        }
       
        #endregion User Events


        #region Deck Slab + Steel Girder Form Events
       
        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {

            iApp.RunExe(Deck.rep_file_name);
        }


        private void txt_Deck_fck_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion Deck Slab + Steel Girder Form Events

        #region Deck Slab + Steel Girder Methods
        private void Deck_Initialize_InputData()
        {
            //Deck.tbl_rolledSteelAngles = new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES"));

            string kStr = "";
            try
            {
                if (cmb_Long_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_Long_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }

                if (cmb_cross_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_cross_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        #endregion Deck Slab + Steel Girder Methods


        #region Form Events

      

        private void frm_Cable_Stayed_Load(object sender, EventArgs e)
        {
        }

        private string Get_LongGirder_File(int index)
        {
            string file_name = "";

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                if (index == 0)
                {
                    file_name = CS_Analysis.DeadLoadAnalysis_Input_File;
                }
                else if (index == all_loads.Count + 1)
                {
                    file_name = CS_Analysis.TotalAnalysis_Input_File;
                }
                else
                {
                    file_name = CS_Analysis.Get_LL_Analysis_Input_File(index);
                }
            }
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                if (index == 0)
                {
                    file_name = CS_Analysis.DeadLoadAnalysis_Input_File;
                }
                else if (index == all_loads.Count + 1)
                {
                    file_name = CS_Analysis.TotalAnalysis_Input_File;
                }
                else
                {
                    file_name = CS_Analysis.Get_LL_Analysis_Input_File(index);
                }

                //if (index == -1) return "";
                //string item = cmb_long_open_file.Items[index].ToString();

                //if (item.StartsWith("DL + LL") || item.StartsWith("TOTAL"))
                //{
                //    file_name = CS_Analysis.TotalAnalysis_Input_File;
                //}
                //else if (item.StartsWith("DEAD LOAD"))
                //{
                //    file_name = CS_Analysis.DeadLoadAnalysis_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 1"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_1_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 2"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_2_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 3"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_3_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 4"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_4_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 5"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_5_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS"))
                //{
                //    file_name = CS_Analysis.LiveLoadAnalysis_Input_File;
                //}
                //else if (item.StartsWith("LONGITUDINAL GIRDER ANALYSIS RESULTS"))
                //{
                //    file_name = File_Long_Girder_Results;
                //}
            }
            return file_name;
        }


        
       
        private void btn_mem_ana_Click(object sender, EventArgs e)
        {
            CableMember cbl = null;

            Results = new List<string>();

            Results.Clear();
            #region TechSOFT Banner
            Results.Add("");
            Results.Add("");
            Results.Add("\t\t**********************************************");
            Results.Add("\t\t*                 ASTRA Pro                  *");
            Results.Add("\t\t*        TechSOFT Engineering Services       *");
            Results.Add("\t\t*                                            *");
            Results.Add("\t\t*        DESIGN OF STAY CABLE MEMBERS        *");
            Results.Add("\t\t*                                            *");
            Results.Add("\t\t**********************************************");
            Results.Add("\t\t----------------------------------------------");
            Results.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            Results.Add("\t\t----------------------------------------------");

            #endregion




            Read_Cable_Member();
            dgv_cable_design.Rows.Clear();
            for (int i = 0; i < Cable_Members.Count; i++)
            {
                cbl = Cable_Members[i];
                Design_Cables(ref cbl);
                Results.AddRange(cbl.DesignResult.ToArray());
                dgv_cable_design.Rows.Add(cbl.ToArray_Extradosed());
            }

            #region Table Report


            List<string> list = new List<string>();


            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Central Span  [L1] = 100.0 m"));
            list.Add(string.Format("Length of Side Span 1   [L2] = 65.0 m"));
            list.Add(string.Format("Length of Side Span 2   [L3] = 65.0 m"));
            list.Add(string.Format(""));
            list.Add(string.Format("Width Along Z-direction [B] = 9.75 m"));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Cantilever Slab [B1] = 1.975 m"));
            list.Add(string.Format(""));
            list.Add(string.Format("Height of Tower [H1] = 12.0 m"));
            list.Add(string.Format("Nos of Cables [NCAB] = 6 nos"));
            list.Add(string.Format(""));
            list.Add(string.Format("Initial Cable Distance from Tower [D1] = 13.5 m"));
            list.Add(string.Format("Horizontal Distance between Two Cables [D2] = 6.0 m"));
            list.Add(string.Format("Cable Circular Diameter [cd] = 0.15 m"));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("User's    Start     End     Length    Calculated     Calculated      Tensile         Allowable       Remarks         Vertical      Horizontal "));
            list.Add(string.Format("Member    Joint    Joint                Force          Stress        Capacity     Tensile Capacity                  Deflection     Deflection"));
            list.Add(string.Format(" No         No       No                                              of Cable        of Cable                        at Deck       at Pylon Top"));
            list.Add(string.Format("                             (m)        (Ton)        (Ton/Sq.m)     (N/Sq.mm)        (N/Sq.mm)                         (m)            (m)"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------"));

            string frmt = "{0,7} {1,7} {2,7} {3,10} {4,12} {5,14} {6,14} {7,16} {8,12} {9,18} {10,15} ";
            for (int i = 0; i < dgv_cable_design.RowCount; i++)
            {
                list.Add(string.Format(frmt
                    , dgv_cable_design[1, i].Value.ToString()
                    , dgv_cable_design[2, i].Value.ToString()
                    , dgv_cable_design[6, i].Value.ToString()
                    , dgv_cable_design[10, i].Value.ToString()
                    , dgv_cable_design[12, i].Value.ToString()
                    , dgv_cable_design[13, i].Value.ToString()
                    , dgv_cable_design[14, i].Value.ToString()
                    , dgv_cable_design[15, i].Value.ToString()
                    , dgv_cable_design[16, i].Value.ToString()
                    , dgv_cable_design[19, i].Value.ToString()
                    , dgv_cable_design[20, i].Value.ToString()
                    ));

            }
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            Results.InsertRange(15, list.ToArray());


            #endregion Table Report

            Results.Add("\t\t----------------------------------------------");
            Results.Add("\t\tTHIS RESULT ENDED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            Results.Add("\t\t----------------------------------------------");

            File.WriteAllLines(Cable_Design_Report, Results.ToArray());
            Results.Clear();

            iApp.View_Result(Cable_Design_Report);
            //try
            //{
            Button_Enable_Disable();

        }
        private void btn_cbl_des_read_data_Click(object sender, EventArgs e)
        {
            Read_Cable_Member();
        }
        private void txt_cbl_des_mem_nos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Read_Cable_Member();
        }

        private void dgv_cable_design_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string s = dgv_cable_design[1, e.RowIndex].Value.ToString();
                int i = MyList.StringToInt(s, -1);
                if (i != -1)
                {
                    int indx = Cable_Members.IndexOf(i);

                    if (indx != -1)
                    {
                        if (Cable_Members[indx].DesignResult.Count > 0)
                        {
                            File.WriteAllLines(Path.Combine(user_path, "cbl.tmp"), Cable_Members[indx].DesignResult.ToArray());
                            iApp.View_Result(Path.Combine(user_path, "cbl.tmp"));
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void txt_cbl_des_d_TextChanged(object sender, EventArgs e)
        {
            txt_cbl_des_Ax.Text = Cable_Ax.ToString("f5");
        }


      
        #endregion Form Events


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

     

        private void cmb_Long_ang_section_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmb = sender as ComboBox;
            TableRolledSteelAngles tbl = iApp.Tables.IS_SteelAngles;
            if (cmb.Name == cmb_Long_ang_section_code.Name)
            {
                cmb_Long_ang_thk.Items.Clear();
                cmb_Long_ang_thk.Visible = true;


                tbl = cmb_Long_ang_section_name.Items.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;


                foreach (var item in tbl.List_Table)
                {
                    if (item.SectionSize == cmb_Long_ang_section_code.Text)
                    {
                        cmb_Long_ang_thk.Items.Add(item.Thickness);
                    }
                }
                cmb_Long_ang_thk.SelectedIndex = cmb_Long_ang_thk.Items.Count > 0 ? 0 : -1;
            }

            if (cmb.Name == cmb_cross_ang_section_code.Name)
            {
                cmb_Long_ang_thk.Items.Clear();
                cmb_Long_ang_thk.Visible = true;
                tbl = cmb_cross_ang_section_name.Items.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;

                //foreach (var item in tbl_rolledSteelAngles.List_Table)
                //{
                //    if (item.SectionSize == cmb_cross_ang_section_code.Text)
                //    {
                //        cmb_cross_ang_thk.Items.Add(item.Thickness);
                //    }
                //}
                //cmb_cross_ang_thk.SelectedIndex = cmb_cross_ang_thk.Items.Count > 0 ? 0 : -1;
            }
        }


        private void cmb_ang_section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_Long_ang_section_name.Name)
            {
                cmb_Long_ang_section_code.Items.Clear();
                if (cmb_Long_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_Long_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_Long_ang_section_code.Items.Count > 0)
                {
                    cmb_Long_ang_section_code.SelectedIndex = 0;
                    cmb_Long_ang_section_code.SelectedItem = "100X100";
                    cmb_Long_ang_thk.SelectedIndex = cmb_Long_ang_thk.Items.Contains(10.0) ? cmb_Long_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_Long_nos_ang.SelectedIndex = 1;
                }

            }

            if (cmb.Name == cmb_cross_ang_section_name.Name)
            {
                cmb_cross_ang_section_code.Items.Clear();
                if (cmb_cross_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_cross_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_cross_ang_section_code.Items.Count > 0)
                {
                    cmb_cross_ang_section_code.SelectedIndex = 0;
                    cmb_cross_ang_section_code.SelectedItem = "100X100";
                    cmb_cross_ang_thk.SelectedIndex = cmb_cross_ang_thk.Items.Contains(10.0) ? cmb_cross_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_cross_nos_ang.SelectedIndex = 1;
                }

            }
        }

        private void btn__Click(object sender, EventArgs e)
        {
            frm_ProblemDescription f = new frm_ProblemDescription();
            //f.Owner = this;
            f.Show();
        }


        public bool Process_Data(string flPath)
        {
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");

            if (!File.Exists(prs.StartInfo.FileName))
            {
                MessageBox.Show("AST001.EXE module not found in the Application Folder \n\r\n\r \"" + Application.StartupPath + "\".", "ASTRA", MessageBoxButtons.OK);
                return false;
            }
            if (prs.Start())
                prs.WaitForExit();

            return true;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            frm_CSB_Technical ft = new frm_CSB_Technical();
            ft.Show();
        }

        public string Working_Folder
        {
            get
            {
                //return Path.Combine(iApp.LastDesignWorkingFolder, Title);
                return iApp.user_path;
            }

        }

        public string Analysis_File
        {
            get
            {

                if (!Directory.Exists(Working_Folder)) Directory.CreateDirectory(Working_Folder);

                string fpath = Path.Combine(Working_Folder, "Tower Analysis");

                if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

                return Path.Combine(fpath, "Tower_Analysis.txt");


            }
        }

        public string Seismic_File
        {
            get
            {
                string seis_inp_file = Path.Combine(Path.GetDirectoryName(Analysis_File), @"Seismic_Analysis\Seismic_Analysis.txt");
                return seis_inp_file;
            }
        }
       
      
        public void Default_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            list.Add(string.Format("TYPE 1, IRCCLASSA"));
            list.Add(string.Format("AXLE LOAD IN TONS , 2.7,2.7,11.4,11.4,6.8,6.8,6.8,6.8"));
            list.Add(string.Format("AXLE SPACING IN METRES, 1.10,3.20,1.20,4.30,3.00,3.00,3.00"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.800"));
            list.Add(string.Format("IMPACT FACTOR, 1.179"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 2, IRC70RTRACK"));
            list.Add(string.Format("AXLE LOAD IN TONS, 7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0"));
            list.Add(string.Format("AXLE SPACING IN METRES, 0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.25"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
            list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.25"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, IRC70RW40TBL"));
            list.Add(string.Format("AXLE LOAD IN TONS,10.0,10.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.93"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format("IMPACT FACTOR, 1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 5, IRC70RW40TBM"));
            list.Add(string.Format("AXLE LOAD IN TONS,5.0,5.0,5.0,5.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,0.795,0.38,0.795"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format("IMPACT FACTOR, 1.10"));
            list.Add(string.Format(""));


            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();


        List<string> load_list_1 = new List<string>();
        List<string> load_list_2 = new List<string>();
        List<string> load_list_3 = new List<string>();
        List<string> load_list_4 = new List<string>();
        List<string> load_list_5 = new List<string>();
        List<string> load_list_6 = new List<string>();
        List<string> load_total_7 = new List<string>();


        #region Chiranjit [2016 07 11]
        List<string> ll_comb = new List<string>();

        public void Store_LL_Combinations(DataGridView dgv_live_loads, DataGridView dgv_loads)
        {
            ll_comb.Clear();
            List<string> com = new List<string>();
            Hashtable ht_cmb = new Hashtable();

            string kStr = "";
            string txt = "";
            int i = 0;
            for (i = 0; i < dgv_live_loads.RowCount; i++)
            {
                kStr = dgv_live_loads[0, i].Value.ToString();
                txt = dgv_live_loads[1, i].Value.ToString();

                if (kStr.StartsWith("TYPE"))
                {
                    try
                    {
                        ht_cmb.Add(kStr, txt);
                    }
                    catch (Exception exx) { }
                }
            }

            dgv_live_loads.Tag = ht_cmb;


            com.Clear();


            for (i = 0; i < dgv_loads.RowCount; i++)
            {

                kStr = dgv_loads[0, i].Value.ToString();


                if (kStr.StartsWith("LOAD"))
                {
                    com.Clear();

                    for (int c = 1; c < dgv_loads.ColumnCount; c++)
                    {
                        txt = dgv_loads[c, i].Value.ToString();

                        if (txt == "") break;
                        kStr = ht_cmb[txt] as string;
                        com.Add(kStr);
                    }

                    //List<string> lst_lane1 = new List<string>();
                    //List<string> lst_lane2 = new List<string>();


                    //lst_lane1.AddRange(com);


                    //lst_lane2.Add(lst_lane1[0]);


                    //lst_lane1.RemoveAt(0);


                    //for(int k = 0; )




                    if (com.Count > 0)
                    {
                        txt = com[0];

                        int lane = 1;

                        for (int j = 1; j < com.Count; j++)
                        {
                            if (txt == com[j]) lane++;
                        }
                        if (lane > 1)
                        {
                            txt = lane + " Lane " + txt;
                        }
                        else
                        {
                            txt = lane + " Lane " + txt;

                            for (int j = 1; j < com.Count; j++)
                            {
                                txt += " + 1 Lane " + com[j];
                            }
                        }
                        ll_comb.Add(txt);
                    }

                }
            }
            dgv_loads.Tag = ll_comb;
        }

        #endregion Chiranjit [2016 07 11]




        #region British Standard Loading
      

        public bool IsRead = false;

      
        #endregion British Standard Loading

    }

}