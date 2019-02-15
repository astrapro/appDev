using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.PSC_I_Girder;
//using BridgeAnalysisDesign.PSC_BoxGirder;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign;


namespace LimitStateMethod.PSC_Box_Girder
{
    public partial class frm_PSC_Box_Girder_Stage : Form
    {
        //const string Title = "ANALYSIS OF PSC BOX GIRDER BRIDGE";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "PSC BOX GIRDER BRIDGE LIMIT STATE [BS]";
                return "PSC BOX GIRDER BRIDGE LIMIT STATE [IRC]";
            }
        }

        //CompositAnalysis Deck_Analysis_DL = null;
        //CompositAnalysis Deck_Analysis_LL = null;
        //PostTensionLongGirder LongGirder = null;
        //RccDeckSlab Deck = null;

        //PSC_BoxGirderAnalysis Deck_Analysis_DL = null;
        PSC_BoxGirderAnalysis Deck_Analysis_LL = null;

        public PSC_BoxGirderAnalysis Bridge_Analysis
        {
            get
            {
                return Deck_Analysis_LL;
            }
            set
            {
                Deck_Analysis_LL = value;
            }
        }

        public PSC_BoxGirderAnalysis Deck_Analysis_DL
        {
            get
            {
                return Deck_Analysis_LL;
            }
        }

        public List<BridgeMemberAnalysis> All_Analysis { get; set; }

        PSC_Box_Section_Data PSC_SECIONS;

        Save_FormRecord SaveRec = new Save_FormRecord();



        PSC_Box_Segmental_Girder Segment_Girder = null;

        PSC_Box_Forces Box_Forces = null;

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        public double L { get { return MyList.StringToDouble(txt_Ana_L.Text, 13.0); } set { txt_Ana_L.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }

        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
        }

        public double DL_Factor
        {
            get
            {
                return MyList.StringToDouble(txt_Ana_DL_factor.Text, 1.0);
            }
        }
        public double LL_Factor
        {
            get
            {
                return MyList.StringToDouble(txt_Ana_LL_factor.Text, 1.0);
            }
        }

        //public string Total_DeadLoad_Reaction
        //{
        //    get
        //    {
        //        return txt_dead_kN_m.Text;
        //    }
        //    set
        //    {
        //        txt_dead_kN_m.Text = value;
        //    }
        //}
        //public string Total_LiveLoad_Reaction
        //{
        //    get
        //    {
        //        return txt_live_kN_m.Text;
        //    }
        //    set
        //    {
        //        txt_live_kN_m.Text = value;
        //    }
        //}
        void frm_ViewForces_Load()
        {
            try
            {
                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);
                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data_DeadLoad()
        {

        }


        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            Text_Changed_Forces();


        }

        private void Text_Changed_Forces()
        {

            //double DL_Factor = MyList.StringToDouble(txt_Ana_DL_Factor.Text, 1.0);
            //double LL_Factor = MyList.StringToDouble(txt_Ana_LL_Factor.Text, 1.0);

            //lbl_DL_factor.Text = "Factor = " + txt_Ana_DL_Factor.Text;
            //lbl_LL_factor.Text = "Factor = " + txt_Ana_LL_Factor.Text;

            if (B != 0)
            {
                //Chiranjit [2013 06 16]
                ////txt_dead_vert_reac_ton_factor.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * DL_Factor)).ToString("f3");
                //uc_Res.txt_dead_vert_reac_kN.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10)).ToString("f3");
                //txt_dead_kN_m.Text = ((MyList.StringToDouble(txt_dead_vert_reac_kN.Text, 0.0)) / B).ToString("f3");


                //txt_live_vert_rec_Ton_factor.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * LL_Factor)).ToString("f3");


                //txt_live_vert_rec_kN.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10)).ToString("f3");
                //txt_live_kN_m.Text = ((MyList.StringToDouble(txt_live_vert_rec_kN.Text, 0.0)) / B).ToString("f3");

                //txt_final_vert_reac.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * DL_Factor) +
                //    (MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * LL_Factor)).ToString("f3");
                //txt_final_vert_rec_kN.Text = (MyList.StringToDouble(txt_final_vert_reac.Text, 0.0) * 10).ToString("f3");
            }


        }
        #endregion View Force

        #region frm_Pier_ViewDesign_Forces
        string analysis_rep = "";
        SupportReactionTable support_reactions = null;
        string Left_support = "";
        string Right_support = "";
        public void frm_Pier_ViewDesign_Forces(string Analysis_Report_file, string left_support, string right_support)
        {
            analysis_rep = Analysis_Report_file;
            Left_support = left_support.Replace(",", " ");
            Right_support = right_support.Replace(",", " ");
        }

        private void frm_ViewDesign_Forces_Load()
        {
            support_reactions = new SupportReactionTable(iApp, analysis_rep);
            try
            {
                //Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }

        #endregion frm_Pier_ViewDesign_Forces



        RccDeckSlab Deck = null;
        public List<string> Results { get; set; }

        IApplication iApp = null;

        bool IsCreate_Data = true;
        public frm_PSC_Box_Girder_Stage(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            Results = new List<string>();

            Segment_Girder = new PSC_Box_Segmental_Girder(app);

            Box_Forces = new PSC_Box_Forces();
            All_Analysis = new List<BridgeMemberAnalysis>();

        }

        public string Worksheet_Folder
        {
            get
            {
                if (Path.GetFileName(user_path) == Project_Name)
                {
                    if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                }
                return Path.Combine(user_path, "Worksheet_Design");
            }
        }
        public string Drawing_Folder
        {
            get
            {
                if (Path.GetFileName(user_path) == Project_Name)
                {
                    if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                }
                return Path.Combine(user_path, "DRAWINGS");
            }
        }

        public string user_path
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


        eAnalysis AnalysisType
        {
            get
            {
                if (tbc_girder.SelectedTab == tab_analysis)
                {
                    return eAnalysis.Normal;
                }
                else if (tbc_girder.SelectedTab == tab_stage)
                {
                    if (tc_stage.SelectedTab == tab_stage1)
                        return eAnalysis.Stage1;
                    if (tc_stage.SelectedTab == tab_stage2)
                        return eAnalysis.Stage2;
                    if (tc_stage.SelectedTab == tab_stage3)
                        return eAnalysis.Stage3;
                    if (tc_stage.SelectedTab == tab_stage4)
                        return eAnalysis.Stage4;
                    if (tc_stage.SelectedTab == tab_stage5)
                        return eAnalysis.Stage5;
                }
                return eAnalysis.Normal;
            }
        }
        UC_BoxGirder_Stage ucStage
        {
            get
            {
                if (AnalysisType == eAnalysis.Stage1) return uC_BoxGirder_Stage1;
                else if (AnalysisType == eAnalysis.Stage2) return uC_BoxGirder_Stage2;
                else if (AnalysisType == eAnalysis.Stage3) return uC_BoxGirder_Stage3;
                else if (AnalysisType == eAnalysis.Stage4) return uC_BoxGirder_Stage4;
                else if (AnalysisType == eAnalysis.Stage5) return uC_BoxGirder_Stage5;
                return uC_BoxGirder_Stage1;
            }
        }

        UC_BoxGirder_Results uc_Res
        {
            get
            {
                if (AnalysisType == eAnalysis.Normal)
                    return uC_Res_Normal;
                else
                    return ucStage.uC_Res;


            }
        }
        public string Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    return Get_Input_File(AnalysisType);
                }
                return "";
            }
        }

        public string Get_Input_File(eAnalysis aType)
        {

            string usp = user_path;

            usp = Path.Combine(usp, aType.ToString().ToUpper() + " ANALYSIS");
            if (!Directory.Exists(usp)) Directory.CreateDirectory(usp);

            return Path.Combine(usp, "INPUT_DATA.TXT");
        }

        public string Input_File_LL
        {

            get
            {
                string usp = Path.GetDirectoryName(Get_Input_File(AnalysisType));
                if (Directory.Exists(usp))
                {
                    if (Path.GetFileName(usp) != "Live Load Analysis")
                        if (!Directory.Exists(Path.Combine(usp, "Live Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(usp, "Live Load Analysis"));
                    return Path.Combine(Path.Combine(usp, "Live Load Analysis"), "Input_Data_LL.txt");
                }
                return "";
            }
        }
        public string Input_File_DL
        {
            get
            {
                string usp = Path.GetDirectoryName(Get_Input_File(AnalysisType));
                if (Directory.Exists(usp))
                {
                    if (Path.GetFileName(usp) != "Dead Load Analysis")
                        if (!Directory.Exists(Path.Combine(usp, "Dead Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(usp, "Dead Load Analysis"));

                    return Path.Combine(Path.Combine(usp, "Dead Load Analysis"), "Dead_Load_Analysis.txt");
                }
                return "";
            }
        }

        public string Analysis_Report_DL
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(Path.GetDirectoryName(Input_File_DL), "ANALYSIS_REP.TXT");
                //return Path.Combine(Path.GetDirectoryName(Input_File_DL), "ANALYSIS_REP_DL.TXT");
                return "";
            }
        }
        public string Analysis_Report_LL
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(Path.GetDirectoryName(Input_File_LL), "ANALYSIS_REP.TXT");
                //return Path.Combine(Path.GetDirectoryName(Input_File_LL), "ANALYSIS_REP_LL.TXT");
                return "";
            }
        }


        #region Deck Analysis Form Events
        private void Default_Input_Data(object sender, EventArgs e)
        {
            Deck_Analysis_DL.Joints = new JointNodeCollection();
            Deck_Analysis_DL.MemColls = new MemberCollection();
            Button_Enable_Disable();
        }


        private bool Check_Project_Folder()
        {

            if (Path.GetFileName(user_path) != Project_Name)
            {
                MessageBox.Show(this, "New Project is not created. Please create New Project.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }



        void Initialize_Data()
        {

        }

        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;
                Show_Section_Result();

                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }
                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }
                Analysis_Initialize_InputData();

                Create_Data_DL(Input_File_DL);
                Create_Data_LL_Indian(Input_File);
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    Create_Data_LL_British(Input_File_LL);
                }
                if (AnalysisType == eAnalysis.Normal)
                {
                    cmb_long_open_file.Items.Clear();
                    cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");

                    for (int i = 1; i < all_loads.Count + 1; i++)
                    {
                        cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + i);
                    }
                    Write_All_Data();

                    cmb_long_open_file.SelectedIndex = 0;
                }
                else
                {
                    ucStage.cmb_long_open_file.Items.Clear();
                    ucStage.cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    for (int i = 1; i < all_loads.Count + 1; i++)
                    {
                        ucStage.cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + i);
                    }
                    Write_All_Data();

                    ucStage.cmb_long_open_file.SelectedIndex = 0;

                    Update_Stage();
                }

                MessageBox.Show(this, "Dead Load and Live Load Analysis Input Data files are Created in Working folder.");

                Button_Enable_Disable();


            }
            catch (Exception ex) { }
        }



        private void btn_Ana_LL_process_analysis_Click(object sender, EventArgs e)
        {

            Bridge_Analysis.Input_File = Get_Input_File(AnalysisType);
            string flPath = Deck_Analysis_LL.Input_File;
            string ana_rep_file = "";
            int c = 0;

            Write_All_Data();

            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();


            iApp.Progress_Works.Clear();
            for (int i = 0; i < (all_loads.Count + 1); i++)
            {
                if (i == 0)
                    flPath = Input_File_DL;
                else
                    flPath = Get_Live_Load_Analysis_Input_File(i);

                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();

                if(AnalysisType != eAnalysis.Normal)
                {
                    pd.Stage_File_Name = Get_Girder_File(i, ((int)AnalysisType) - 1);
                    pd.IS_Stage_File = true;
                }
                pcol.Add(pd);
                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");
            }


            if (!iApp.Show_and_Run_Process_List(pcol))
            {
                Button_Enable_Disable();
                return;
            }


            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                Show_Member_Forces_Indian(ana_rep_file);
            else
            {
                Show_Member_Forces_Indian(ana_rep_file);
                //Show_Member_Forces_British();
            }

            Save_FormRecord.Write_All_Data(this, user_path);


            #region Load BM-SF
            uC_BoxGirder1.txt_BM_DL_Supp.Text = uc_Res.txt_Ana_dead_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Deff.Text = uc_Res.txt_Ana_dead_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L8.Text = uc_Res.txt_Ana_dead_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L4.Text = uc_Res.txt_Ana_dead_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_DL_3L8.Text = uc_Res.txt_Ana_dead_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Mid.Text = uc_Res.txt_Ana_dead_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_DL_Supp.Text = uc_Res.txt_Ana_dead_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Deff.Text = uc_Res.txt_Ana_dead_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L8.Text = uc_Res.txt_Ana_dead_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L4.Text = uc_Res.txt_Ana_dead_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_DL_3L8.Text = uc_Res.txt_Ana_dead_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Mid.Text = uc_Res.txt_Ana_dead_inner_long_L2_shear.Text;


            uC_BoxGirder1.txt_BM_LL_Supp.Text = uc_Res.txt_Ana_live_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Deff.Text = uc_Res.txt_Ana_live_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L8.Text = uc_Res.txt_Ana_live_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L4.Text = uc_Res.txt_Ana_live_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_LL_3L8.Text = uc_Res.txt_Ana_live_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Mid.Text = uc_Res.txt_Ana_live_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_LL_Supp.Text = uc_Res.txt_Ana_live_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Deff.Text = uc_Res.txt_Ana_live_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L8.Text = uc_Res.txt_Ana_live_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L4.Text = uc_Res.txt_Ana_live_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_LL_3L8.Text = uc_Res.txt_Ana_live_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Mid.Text = uc_Res.txt_Ana_live_inner_long_L2_shear.Text;


            #endregion Load BM-SF

        }

        private void Show_Member_Forces_Indian(string ana_rep_file)
        {

            All_Analysis.Clear();
            for (int i = 0; i < all_loads.Count + 1; i++)
            {
                if (i == 0)
                    ana_rep_file = Input_File_DL;
                else
                    ana_rep_file = Get_Live_Load_Analysis_Input_File(i);

                ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                if (File.Exists(ana_rep_file))
                {
                    All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                }
            }



            //List<string> Work_List = new List<string>();


            Show_Moment_Shear_DL();
            Show_Moment_Shear_LL();


            Show_ReactionForces();



            grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            iApp.Save_Form_Record(this, user_path);

            Button_Enable_Disable();


            iApp.Progress_Works.Clear();
        }

        private void Show_Member_Forces_British()
        {
            string ana_rep_file = "";

            All_Analysis.Clear();
            for (int i = 0; i < all_loads.Count + 1; i++)
            {
                if (i == 0)
                    ana_rep_file = Deck_Analysis_DL.Input_File;
                else
                    ana_rep_file = Get_Live_Load_Analysis_Input_File(i);

                ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                if (File.Exists(ana_rep_file))
                {
                    All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                }
            }
            Deck_Analysis_DL.Bridge_Analysis = All_Analysis[0];
            Show_Moment_Shear_DL();

            Show_Moment_Shear_LL_British();


            Show_ReactionForces();
        }

        void Show_ReactionForces()
        {
            #region Chiranjit [2012 10 31]

            string s1 = "";
            string s2 = "";
            try
            {
                for (int i = 0; i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count; i++)
                {
                    if (i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count / 2)
                    {
                        if (i == Deck_Analysis_LL.Bridge_Analysis.Supports.Count / 2 - 1)
                        {
                            s1 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo;
                        }
                        else
                            s1 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo + ",";
                    }
                    else
                    {
                        if (i == Deck_Analysis_LL.Bridge_Analysis.Supports.Count - 1)
                        {
                            s2 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo;
                        }
                        else
                            s2 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo + ",";
                    }
                }
            }
            catch (Exception ex) { }
            //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
            //double BB = B;


            //frm_ViewForces(BB, Deck_Analysis_DL.Analysis_Report, Deck_Analysis_LL.Bridge_Analysis.Analysis_File, (s1 + " " + s2));
            //frm_ViewForces_Load();

            //frm_Pier_ViewDesign_Forces(Deck_Analysis_LL.Bridge_Analysis.Analysis_File, s1, s2);
            //frm_ViewDesign_Forces_Load();



            Show_and_Save_Data_Load_1_2_3();




            #endregion Chiranjit [2012 10 31]

        }


        private void btn_Ana_add_load_Click(object sender, EventArgs e)
        {
            try
            {
                //dgv_Ana_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Ana_DL_Y.Text, txt_Ana_DL_Z.Text, txt_XINCR.Text, txt_Load_Impact.Text);
            }
            catch (Exception ex) { }
        }

        private void Write_Ana_Load_Data(bool IsLiveLoad)
        {

            string file_name = (IsLiveLoad) ? Deck_Analysis_LL.Input_File : Deck_Analysis_DL.Input_File;

            if (!File.Exists(file_name)) return;


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

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

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

            string s = " DL";
            s += (!IsLiveLoad ? " + SIDL " : "");
            //s += (IsLiveLoad ? " + LL " : "");

            if (!IsLiveLoad)
            {
                //load_lst.AddRange(txt_Ana_LL_member_load.Lines);
                load_lst.Add(string.Format("LOAD 1 DEAD LOAD"));
                load_lst.Add(string.Format("MEMBER LOAD"));
                //load_lst.Add(string.Format("1 TO 12 UNI GY -16.0 "));
                load_lst.Add(string.Format("{0} TO {1} UNI GY -{2:f3} ", Bridge_Analysis.Long_Girder_Members_Array[0, 0].MemberNo,
                    Bridge_Analysis.MemColls[Bridge_Analysis.MemColls.Count - 1].MemberNo,
                     MyList.StringToDouble(txt_tot_AX) * 2.5
                    ));


                var Col = Bridge_Analysis.Total_Columns;
                var Row = Bridge_Analysis.Total_Rows;
                //load_lst.AddRange(txt_Ana_LL_member_load.Lines);

                load_lst.Add(string.Format("LOAD 2 SIDL+FPLL"));
                load_lst.Add(string.Format("MEMBER LOAD"));


                double sidl = MyList.StringToDouble(txt_SIDL);
                double fpll = MyList.StringToDouble(txt_FPLL);
                //load_lst.Add(string.Format("1 TO 12 UNI GY -2.5"));
                //load_lst.Add(string.Format("1 TO 12 UNI GY -0.98"));
                load_lst.Add(string.Format("{0} TO {1} UNI GY -{2:f3} ", Bridge_Analysis.Long_Girder_Members_Array[1, 0].MemberNo,
                    Bridge_Analysis.Long_Girder_Members_Array[1, Col - 2].MemberNo, sidl));

                load_lst.Add(string.Format("{0} TO {1} UNI GY -{2:f3} ", Bridge_Analysis.Long_Girder_Members_Array[3, 0].MemberNo,
                    Bridge_Analysis.Long_Girder_Members_Array[3, Col - 2].MemberNo, sidl));

                load_lst.Add(string.Format("{0} TO {1} UNI GY -{2:f3} ", Bridge_Analysis.Long_Girder_Members_Array[1, 0].MemberNo,
                    Bridge_Analysis.Long_Girder_Members_Array[1, Col - 2].MemberNo, fpll));

                load_lst.Add(string.Format("{0} TO {1} UNI GY -{2:f3} ", Bridge_Analysis.Long_Girder_Members_Array[3, 0].MemberNo,
                    Bridge_Analysis.Long_Girder_Members_Array[3, Col - 2].MemberNo, fpll));

            }
            else
            {
                load_lst.Add("LOAD    1   " + s);
                load_lst.Add("MEMBER LOAD");
                load_lst.Add("1 TO 220 UNI GY -0.001");
                Deck_Analysis_LL.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
                //Deck_Analysis_LL.LoadReadFromGrid(dgv_Ana_live_load);
                //if (dgv_Ana_live_load.RowCount != 0)
                //    load_lst.AddRange(Get_MovingLoad_Data(Deck_Analysis_LL.Live_Load_List));
                //inp_file_cont.InsertRange(indx, );
            }
            inp_file_cont.InsertRange(indx, load_lst);

            inp_file_cont.Remove("");

            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;
            btn_Ana_DL_create_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            Button_Enable_Disable();
        }
        public void Show_ReadMemberLoad(string file_name, bool IsDeadLoad)
        {

            if (!File.Exists(file_name)) return;


            List<LoadData> lds = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "LL.txt"));

            List<string> list_member_load = new List<string>();
            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;
            bool mov_flag = false;
            bool isMoving_load = false;

            Hashtable ht_impact = new Hashtable();
            bool is_def_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    is_def_load = false;
                    isMoving_load = true;
                    //if (mlist.Count == 3) txt_LL_load_gen.Text = mlist.StringList[2];
                    //dgv_Ana_live_load.Rows.Clear();
                    continue;
                }

                if (kStr.Contains("DEFINE MOV"))
                {
                    is_def_load = true;
                    mov_flag = false;
                    //continue;
                }
                if (kStr.StartsWith("PRINT") || kStr.StartsWith("PERFO"))
                {
                    is_def_load = false;
                    mov_flag = false;
                    isMoving_load = false;
                    //continue;
                }
                else if (is_def_load)
                {
                    try
                    {
                        ht_impact.Add(mlist[2], mlist.GetDouble(3));
                    }
                    catch (Exception ex) { }
                }


                if (isMoving_load)
                {
                    try
                    {
                        LoadData ld = LoadData.Parse(kStr);
                        for (int c = 0; c < lds.Count; c++)
                        {
                            if (lds[c].TypeNo == ld.TypeNo)
                            {
                                ld.Code = lds[c].Code;
                                break;
                            }
                        }
                        try
                        {
                            ld.ImpactFactor = (double)ht_impact[ld.Code];
                        }
                        catch (Exception ex) { }
                    }
                    catch (Exception ex) { }
                }

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    i++;
                    continue;
                }
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    continue;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                    mov_flag = false;
                }
                if (flag)
                {
                    if (mov_flag)
                    {
                        list_member_load.Add(inp_file_cont[i]);
                    }
                    inp_file_cont.RemoveAt(i);
                    i--;
                }
            }
            //if (IsDeadLoad)
                //txt_Ana_LL_member_load.Lines = list_member_load.ToArray();


        }


        public void Show_LoadGeneration(string file_name)
        {

            if (!File.Exists(file_name)) return;
            List<LoadData> lds = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "LL.txt"));

            //iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);

            List<string> list_member_load = new List<string>();
            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;
            bool mov_flag = false;
            bool isMoving_load = false;

            Hashtable ht_impact = new Hashtable();
            bool is_def_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    isMoving_load = true;
                    is_def_load = false;
                    //if (mlist.Count == 3) txt_LL_load_gen.Text = mlist.StringList[2];
                    //dgv_Ana_live_load.Rows.Clear();
                    continue;
                }


                if (kStr.Contains("DEFINE MOV"))
                {
                    mov_flag = false;
                    is_def_load = true;

                    //continue;
                }
                if (kStr.StartsWith("PRINT") || kStr.StartsWith("PERFO"))
                {
                    is_def_load = false;
                    mov_flag = false;
                    isMoving_load = false;
                    //continue;
                }
                else if (is_def_load)
                {
                    try
                    {
                        ht_impact.Add(mlist[2], mlist.GetDouble(3));
                    }
                    catch (Exception ex) { }
                }

                if (isMoving_load)
                {
                    try
                    {
                        LoadData ld = LoadData.Parse(kStr);
                        for (int c = 0; c < lds.Count; c++)
                        {
                            if (lds[c].TypeNo == ld.TypeNo)
                            {
                                ld.Code = lds[c].Code;
                                break;
                            }
                        }
                        try
                        {
                            ld.ImpactFactor = (double)ht_impact[ld.Code];
                        }
                        catch (Exception ex) { }
                    }
                    catch (Exception ex) { }
                }

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    continue;
                }
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    continue;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                    mov_flag = false;
                }
                if (flag)
                {
                    if (mov_flag)
                    {
                        list_member_load.Add(inp_file_cont[i]);
                    }
                    inp_file_cont.RemoveAt(i);
                    i--;
                }
            }
        }
        public string Analysis_Path
        {
            get
            {

                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, Title)))
                    return Path.Combine(iApp.LastDesignWorkingFolder, Title);

                return iApp.LastDesignWorkingFolder;

            }
        }

        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                ofd.InitialDirectory = Analysis_Path;
                //ofd.InitialDirectory = user_path;
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    IsCreate_Data = false;
                    //Show_ReadMemberLoad(ofd.FileName);

                    string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");

                    if (!File.Exists(chk_file)) chk_file = ofd.FileName;
                    //Read_All_Data();
                    Set_Segment_Data();
                    Open_AnalysisFile(chk_file);

                    //Chiranjit [2013 04 26]
                    iApp.Read_Form_Record(this, user_path);

                    Set_Box_Forces();
                    Set_Segment_Data();
                    Segment_Girder.FilePath = user_path;


                    txt_Ana_analysis_file.Text = chk_file;
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Button_Enable_Disable();

                }
            }
        }

        private void btn_Ana_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Deck_Analysis_DL.Input_File))
                iApp.OpenWork(Deck_Analysis_DL.Input_File, true);
        }

        #endregion  Composite Analysis Form Events

        #region Deck Methods

        private void Create_Data_DL(string file_name)
        {

            Deck_Analysis_DL.Input_File = file_name;
            //Deck_Analysis_DL.CreateData_DeadLoad();
            Deck_Analysis_DL.CreateData();
            Deck_Analysis_DL.WriteData_DeadLoad(Deck_Analysis_DL.Input_File, PSC_SECIONS);
            // Deck_Analysis_DL.WriteData_LiveLoad(Deck_Analysis_DL.Input_File, PSC_SECIONS);
            Write_Ana_Load_Data(false);
            Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_DL.Input_File);

            string ll_txt = Deck_Analysis_DL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_DL.Live_Load_List == null) return;

            Button_Enable_Disable();
        }
        private void Create_Data_LL_Indian(string file_name)
        {
            //txt_LL_load_gen.Text = ((MyList.StringToDouble(txt_Ana_L.Text, 0.0) + Math.Abs(MyList.StringToDouble(txt_Ana_X.Text, 0.0))) / MyList.StringToDouble(txt_XINCR.Text, 0.2)).ToString("f0");
            LONG_GIRDER_LL_TXT();
            Deck_Analysis_LL.Input_File = file_name;
            Deck_Analysis_LL.CreateData();


            int i = 0;
            string ll_file = "";
            for (i = 0; i < all_loads.Count; i++)
            {
                ll_file = Get_Live_Load_Analysis_Input_File(i + 1);
                Deck_Analysis_LL.WriteData_LiveLoad(ll_file, PSC_SECIONS, long_ll);
                Ana_Write_Long_Girder_Load_Data(ll_file, true, false, (i + 1));
            }

            Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            Button_Enable_Disable();



            //Deck_Analysis_LL.WriteData_LiveLoad(Deck_Analysis_LL.Input_File, PSC_SECIONS);
            //Write_Ana_Load_Data(true);
            //Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

            //string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            //Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            //if (Deck_Analysis_LL.Live_Load_List == null) return;

            //Button_Enable_Disable();
        }





        void Analysis_Initialize_InputData()
        {
            Bridge_Analysis.Input_File = Get_Input_File(AnalysisType);
            Bridge_Analysis.Length = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            Bridge_Analysis.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Bridge_Analysis.WidthCantilever = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);
            //Deck_Analysis_LL.Skew_Angle = MyList.StringToDouble(txt_Ana_skew_angle.Text, 0.0);
            Bridge_Analysis.Effective_Depth = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);

            if (AnalysisType != eAnalysis.Normal)
            {
                Bridge_Analysis.Start_Support = ucStage.Start_Support_Text;
                Bridge_Analysis.End_Support = ucStage.END_Support_Text;

                Bridge_Analysis.E_CONC = ucStage.txt_emod_conc.Text;
                Bridge_Analysis.DEN_CONC = ucStage.txt_den_conc.Text;
                Bridge_Analysis.PR_CONC = ucStage.txt_PR_conc.Text;
            }
            else
            {
                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;

                Bridge_Analysis.E_CONC = txt_emod_conc.Text;
                Bridge_Analysis.DEN_CONC = txt_den_conc.Text;
                Bridge_Analysis.PR_CONC = txt_PR_conc.Text;
            }

            Bridge_Analysis._cen_Ax = MyList.StringToDouble(txt_cen_AX.Text, 0.0);
            Bridge_Analysis._cen_Ix = MyList.StringToDouble(txt_cen_IXX.Text, 0.0);
            Bridge_Analysis._cen_Iy = MyList.StringToDouble(txt_cen_IYY.Text, 0.0);
            Bridge_Analysis._cen_Iz = MyList.StringToDouble(txt_cen_IZZ.Text, 0.0);

            Bridge_Analysis._inn_Ax = MyList.StringToDouble(txt_inn_AX.Text, 0.0);
            Bridge_Analysis._inn_Ix = MyList.StringToDouble(txt_inn_IXX.Text, 0.0);
            Bridge_Analysis._inn_Iy = MyList.StringToDouble(txt_inn_IYY.Text, 0.0);
            Bridge_Analysis._inn_Iz = MyList.StringToDouble(txt_inn_IZZ.Text, 0.0);

            Bridge_Analysis._out_Ax = MyList.StringToDouble(txt_out_AX.Text, 0.0);
            Bridge_Analysis._out_Ix = MyList.StringToDouble(txt_out_IXX.Text, 0.0);
            Bridge_Analysis._out_Iy = MyList.StringToDouble(txt_out_IYY.Text, 0.0);
            Bridge_Analysis._out_Iz = MyList.StringToDouble(txt_out_IZZ.Text, 0.0);
        }



        void Set_Box_Forces()
        {
            Box_Forces.FRC_LL_Shear[0] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_support_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[0] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_support_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[1] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[1] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[2] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[2] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[3] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[3] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_L4_moment.Text, 0.0);



            Box_Forces.FRC_LL_Shear[4] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[4] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_3L_8_moment.Text, 0.0);


            Box_Forces.FRC_LL_Shear[5] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[5] = MyList.StringToDouble(uc_Res.txt_Ana_live_inner_long_L2_moment.Text, 0.0);




            Box_Forces.FRC_DL_Shear[0] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_support_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[0] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_support_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[1] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[1] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[2] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[2] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[3] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[3] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_L4_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[4] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[4] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[5] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[5] = MyList.StringToDouble(uc_Res.txt_Ana_dead_inner_long_L2_moment.Text, 0.0);


            Box_Forces.FRC_SIDL_Shear[0] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_support_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[0] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_support_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[1] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[1] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[2] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[2] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_L8_moment.Text, 0.0);


            Box_Forces.FRC_SIDL_Shear[3] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[3] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_L4_moment.Text, 0.0);


            Box_Forces.FRC_SIDL_Shear[4] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[4] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[5] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[5] = MyList.StringToDouble(uc_Res.txt_Ana_live_outer_long_L2_moment.Text, 0.0);


            Box_Forces.Set_Absolute();

        }


        void Show_Moment_Shear_LL()
        {

            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();

            Deck_Analysis_LL.Bridge_Analysis = All_Analysis[1];
            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Bridge_Analysis.Analysis.Members);
            JointNodeCollection jn_col = Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints;


            double L = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length;
            double W = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width;
            double cant_wi = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever;

            //double support = Deck_Analysis_LL.Bridge_Analysis.Supports.Count > 0 ? Deck_Analysis_LL.Bridge_Analysis.Supports[0].X : 0.5;
            double support = Deck_Analysis_LL.Support_Distance;
            support = Deck_Analysis_LL.Support_Distance;


            double eff_d = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


            double val = L / 2;
            int i = 0;

            List<int> _L2_joints = new List<int>();
            List<int> _L4_joints = new List<int>();
            List<int> _deff_joints = new List<int>();

            List<int> _L8_joints = new List<int>();
            List<int> _3L8_joints = new List<int>();
            List<int> _support_joints = new List<int>();


            for (i = 0; i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count; i++)
            {
                _support_joints.Add(Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo);
                //_support_joints.Add(item.
            }



            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            val = -999;


            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {

                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < jn_col.Count; i++)
                {

                    if (_Z_joints[zc] == jn_col[i].Z)
                    {
                        if (x_min > jn_col[i].X)
                            x_min = jn_col[i].X;
                        if (x_max < jn_col[i].X)
                            x_max = jn_col[i].X;
                    }

                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }

            eff_d = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth;
            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0;

                eff_d = Deck_Analysis_LL.Effective_Depth;
            }

            cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.WidthCantilever = cant_wi;


            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }

            for (i = 0; i < jn_col.Count; i++)
            {
                if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];



                if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                {
                    _L2_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }



                if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }
                if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }



                if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }


                if ((jn_col[i].X.ToString("0.0") == ((3.0 * L / 8.0) + x_min).ToString("0.0")))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }
                if ((jn_col[i].X.ToString("0.0") == ((L - (3.0 * L / 8.0)) + x_min).ToString("0.0")))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }

            }

            //For Support
            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            if (_support_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                uc_Res.txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[0] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                uc_Res.txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[0] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                Box_Forces.FRC_LL_Torsion[0] = force;

                _joints.Clear();
            }

            //For Deff
            if (_deff_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                uc_Res.txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[1] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                uc_Res.txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[1] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                Box_Forces.FRC_LL_Torsion[1] = force;

                _joints.Clear();
            }

            if (_L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                uc_Res.txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[2] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                uc_Res.txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[2] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                Box_Forces.FRC_LL_Torsion[2] = force;

                _joints.Clear();
            }



            if (_L4_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                uc_Res.txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[3] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                uc_Res.txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _L4_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[3] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L4_joints);
                Box_Forces.FRC_LL_Torsion[3] = force;

                _joints.Clear();
            }

            //For 3L/8
            //for (node = 78; node <= 88; node++) _joints.Add(node);
            //for (node = 56; node <= 66; node++) _joints.Add(node);
            if (_3L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_3L8_joints);
                uc_Res.txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                uc_Res.txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _3L8_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[4] = force;

                _joints.Clear();
            }

            //For L/2
            //for (node = 67; node <= 77; node++) _joints.Add(node);
            if (_L2_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L2_joints);
                uc_Res.txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[5] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                uc_Res.txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[5] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[5] = force;

                _joints.Clear();
                _joints = null;
            }

            File.WriteAllLines(Result_Report_LL, Results.ToArray());
        }

        void Show_Moment_Shear_LL_British()
        {
            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();


            Deck_Analysis_LL.Bridge_Analysis = All_Analysis[1];
            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Bridge_Analysis.Analysis.Members);
            JointNodeCollection jn_col = Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints;


            double L = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length;
            double W = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width;
            double cant_wi = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever;

            double support = Deck_Analysis_LL.Support_Distance;
            support = Deck_Analysis_LL.Support_Distance;


            double eff_d = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


            double val = L / 2;
            int i = 0;

            List<int> _L2_joints = new List<int>();
            List<int> _L4_joints = new List<int>();
            List<int> _deff_joints = new List<int>();

            List<int> _L8_joints = new List<int>();
            List<int> _3L8_joints = new List<int>();
            List<int> _support_joints = new List<int>();


            for (i = 0; i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count; i++)
            {
                _support_joints.Add(Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo);
            }



            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            val = -999;


            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {

                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < jn_col.Count; i++)
                {
                    if (_Z_joints[zc] == jn_col[i].Z)
                    {
                        if (x_min > jn_col[i].X)
                            x_min = jn_col[i].X;
                        if (x_max < jn_col[i].X)
                            x_max = jn_col[i].X;
                    }

                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }

            eff_d = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth;
            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0;

                eff_d = Deck_Analysis_LL.Effective_Depth;
            }
            cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.WidthCantilever = cant_wi;



            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }

            for (i = 0; i < jn_col.Count; i++)
            {
                if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];



                if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                {
                    _L2_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }



                if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }
                if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }



                if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }


                if ((jn_col[i].X.ToString("0.0") == ((3.0 * L / 8.0) + x_min).ToString("0.0")))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }
                if ((jn_col[i].X.ToString("0.0") == ((L - (3.0 * L / 8.0)) + x_min).ToString("0.0")))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }

            }

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            for (i = 1; i < All_Analysis.Count; i++)
            {
                Results.Add("");
                Results.Add("----------------------------------------------------------------");
                Results.Add(string.Format("STEP {0} : ANALYSIS RESULT OF {1}", i, all_loads[i - 1][0]));
                Results.Add("----------------------------------------------------------------");
                Results.Add("");
                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[i];

                #region Write Data to List

                #region _support_joints

                if (_support_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                    uc_Res.txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[0] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                    uc_Res.txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[0] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                    Box_Forces.FRC_LL_Torsion[0] = force;

                    _joints.Clear();
                }
                #endregion _support_joints


                #region _deff_joints

                //For Deff
                //for (node = 111; node <= 121; node++) _joints.Add(node);
                //for (node = 23; node <= 33; node++) _joints.Add(node);
                if (_deff_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                    uc_Res.txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[1] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                    uc_Res.txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[1] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                    Box_Forces.FRC_LL_Torsion[1] = force;

                    _joints.Clear();
                }
                #endregion _deff_joints



                #region _L8_joints

                //For L/8
                //for (node = 100; node <= 110; node++) _joints.Add(node);
                //for (node = 34; node <= 44; node++) _joints.Add(node);
                if (_L8_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                    uc_Res.txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[2] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                    uc_Res.txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[2] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                    Box_Forces.FRC_LL_Torsion[2] = force;

                    _joints.Clear();
                }

                #endregion _L8_joints


                #region _L4_joints

                //For L/4
                //for (node = 89; node <= 99; node++) _joints.Add(node);
                //for (node = 45; node <= 55; node++) _joints.Add(node);
                if (_L4_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                    uc_Res.txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[3] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                    uc_Res.txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _L4_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[3] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L4_joints);
                    Box_Forces.FRC_LL_Torsion[3] = force;

                    _joints.Clear();
                }
                #endregion _L4_joints


                #region _3L8_joints
                //For 3L/8
                //for (node = 78; node <= 88; node++) _joints.Add(node);
                //for (node = 56; node <= 66; node++) _joints.Add(node);
                if (_3L8_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_3L8_joints);
                    uc_Res.txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[4] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                    uc_Res.txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _3L8_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[4] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                    Box_Forces.FRC_LL_Torsion[4] = force;

                    _joints.Clear();
                }
                #endregion _3L8_joints

                #region _L2_joints

                //For L/2
                //for (node = 67; node <= 77; node++) _joints.Add(node);
                if (_L2_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L2_joints);
                    uc_Res.txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[5] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                    uc_Res.txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[5] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                    Box_Forces.FRC_LL_Torsion[5] = force;

                    _joints.Clear();
                    //_joints = null;
                }
                #endregion _L2_joints

                #endregion Write Data to List
            }
            File.WriteAllLines(Result_Report_LL, Results.ToArray());
        }

        void Show_Moment_Shear_DL()
        {
            List<int> _joints = new List<int>();
            MaxForce force = new MaxForce();

            //force = All_Analysis[0].GetJoint_ShearForce(_joints, 1);
            Deck_Analysis_DL.Bridge_Analysis = All_Analysis[0];

            Deck_Analysis_DL.Set_Joints();
            //Support
            //_joints.Add(2);
            //_joints.Add(12);

            _joints = Bridge_Analysis._supp_jnts;


            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Dead Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("SELF WEIGHT");
            Results.Add("-----------");
            Results.Add("");
            Results.Add("");
            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[0] = force;


            //Deff
            //_joints.Clear();
            //_joints.Add(3);
            //_joints.Add(11);
            _joints = Bridge_Analysis._deff_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[1] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[1] = force;


            //L/8
            //_joints.Clear();
            //_joints.Add(4);
            //_joints.Add(10);
            _joints = Bridge_Analysis._L8_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[2] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[2] = force;


            //L/4
            //_joints.Clear();
            //_joints.Add(5);
            //_joints.Add(9);
            _joints = Bridge_Analysis._L4_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[3] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[3] = force;



            //3L/8
            //_joints.Clear();
            //_joints.Add(6);
            //_joints.Add(8);
            _joints = Bridge_Analysis._3L8_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[4] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[4] = force;



            //L/2
            //_joints.Clear();
            //_joints.Add(7);
            _joints = Bridge_Analysis._L2_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[5] = force;



            //_joints.Clear();


            Results.Add("");
            Results.Add("");
            Results.Add("SUPER IMPOSED DEAD LOAD");
            Results.Add("-----------------------");
            Results.Add("");
            Results.Add("");
            //Support
            //_joints.Add(2);
            //_joints.Add(12);
            _joints = Bridge_Analysis._supp_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_live_outer_long_support_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_live_outer_long_support_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[0] = force;




            //Deff
            //_joints.Clear();
            //_joints.Add(3);
            //_joints.Add(11);
            _joints = Bridge_Analysis._deff_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_live_outer_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_live_outer_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[1] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[1] = force;



            //L/8
            //_joints.Clear();
            //_joints.Add(4);
            //_joints.Add(10);
            _joints = Bridge_Analysis._L8_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_live_outer_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[2] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_live_outer_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[2] = force;




            //L/4
            //_joints.Clear();
            //_joints.Add(5);
            //_joints.Add(9);
            _joints = Bridge_Analysis._L4_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_live_outer_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_live_outer_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[3] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[3] = force;

            //3L/8
            //_joints.Clear();
            //_joints.Add(6);
            //_joints.Add(8);
            _joints = Bridge_Analysis._3L8_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_live_outer_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_live_outer_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[4] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[4] = force;

            //L/2
            //_joints.Clear();
            //_joints.Add(7);
            _joints = Bridge_Analysis._L2_jnts;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            uc_Res.txt_Ana_live_outer_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc_Res.txt_Ana_live_outer_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[5] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[5] = force;

            File.WriteAllLines(Result_Report_DL, Results.ToArray());

        }
        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
            }
        }
        public string Result_Report_LL
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT_LL.TXT");
            }
        }
        public string Result_Report_DL
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT_DL.TXT");
            }
        }
        private void Show_Analysis_Result()
        {

            List<string> list = new List<string>();
            if (File.Exists(Result_Report_DL))
            {
                list.AddRange(File.ReadAllLines(Result_Report_DL));
            }
            if (File.Exists(Result_Report_LL))
            {
                list.AddRange(File.ReadAllLines(Result_Report_LL));
            }

            File.WriteAllLines(Result_Report, list.ToArray());
            //iApp.RunExe(Result_Report);
        }

        void Write_Max_Moment_Shear()
        {
            List<string> list = new List<string>();
            //list.Add(string.Format("LONG_INN_DEFF_SHR={0}", txt_Ana_DL_inner_long_deff_shear.Text));
            //list.Add(string.Format("LONG_INN_L2_MOM={0}", txt_Ana_DL_inner_long_L2_moment.Text));


            //list.Add(string.Format("LONG_OUT_DEFF_SHR={0}", txt_Ana_DL_outer_long_deff_shear.Text));
            //list.Add(string.Format("LONG_OUT_L2_MOM={0}", txt_Ana_DL_outer_long_L2_moment.Text));

            string f_path = Path.Combine(user_path, "FORCES.TXT");
            File.WriteAllLines(f_path, list.ToArray());
            Environment.SetEnvironmentVariable("TBEAM_ANALYSIS", f_path);
            list = null;
        }
        public void Button_Enable_Disable()
        {
            if (AnalysisType == eAnalysis.Normal)
            {
                btn_Ana_DL_create_data.Enabled = true;
                //btn_Ana_DL_view_data.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
                //btn_Ana_DL_view_structure.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
                //btn_Ana_LL_view_structure.Enabled = File.Exists(Deck_Analysis_LL.Input_File);
                //btn_Ana_DL_view_report.Enabled = File.Exists(Deck_Analysis_DL.Analysis_Report);

                //btn_Ana_LL_view_data.Enabled = File.Exists(Deck_Analysis_LL.Input_File);
                //btn_Ana_LL_view_report.Enabled = File.Exists(Deck_Analysis_LL.Analysis_Report);


                btn_Process_LL_Analysis.Enabled = File.Exists(Input_File_DL);
            }
            else
            {
                ucStage.btn_Process_LL_Analysis.Enabled = File.Exists(Input_File_DL);

            }


        }
        public void Open_AnalysisFile(string file_name)
        {
            string analysis_file = file_name;
            bool flag = false;


            if (!File.Exists(analysis_file)) return;
            user_path = Path.GetDirectoryName(file_name);

            string wrkg_folder = Path.GetDirectoryName(analysis_file);
            string dl_file, ll_file;

            if (Path.GetFileName(wrkg_folder) == "Dead Load Analysis")
            {
                user_path = Path.GetDirectoryName(user_path);

                ////dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                //if (File.Exists(dl_file))
                //{
                //    Show_Moment_Shear_DL();
                //    //Show_ReadMemberLoad(analysis_file);
                //}

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");

                ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");
                Deck_Analysis_LL.Input_File = ll_file;
                //ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                //ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ll_file);

                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }
            }
            else if (Path.GetFileName(wrkg_folder) == "Live Load Analysis")
            {
                ll_file = analysis_file;

                Deck_Analysis_LL.Input_File = analysis_file;
                ll_file = analysis_file;
                //ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");

                dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                //dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                //dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }
            }
            else
            {
                wrkg_folder = Path.GetDirectoryName(analysis_file);
                wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");


                dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");


                if (File.Exists(dl_file))
                {
                    flag = true;
                    Show_Moment_Shear_DL();
                }

                wrkg_folder = Path.GetDirectoryName(analysis_file);
                wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");



                ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");

                if (!File.Exists(ll_file))
                    ll_file = Get_Live_Load_Analysis_Input_File(1);

                Deck_Analysis_LL.Input_File = ll_file;



                if (File.Exists(ll_file))
                {
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }

            }

            if (File.Exists(analysis_file))
            {
                //btn_Ana_DL_view_structure.Enabled = true;

                try
                {

                    if (flag)
                    {
                        if (Deck_Analysis_LL.Bridge_Analysis != null)
                        {
                            if (Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints.Count > 1)
                            {
                                Deck_Analysis_LL.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints[1].X / Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints[1].Z)));
                                //txt_Ana_skew_angle.Text = Deck_Analysis_LL.Skew_Angle.ToString();
                            }
                        }

                        txt_Ana_L.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length.ToString();
                        //txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                        txt_Ana_B.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();
                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();

                        txt_support_distance.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        //txt_Ana_skew_angle.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Skew_Angle.ToString();

                        txt_gd_np.Text = (Deck_Analysis_LL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");
                    }
                    else
                    {
                        txt_Ana_L.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Length.ToString();
                        txt_Ana_B.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_gd_np.Text = (Deck_Analysis_DL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");

                        txt_support_distance.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            string ll_txt = Path.Combine(user_path, "LL.txt");
            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);
            if (Deck_Analysis_DL.Live_Load_List == null) return;
        }


        public void Open_AnalysisFile_2013_04_27(string file_name)
        {
            string analysis_file = file_name;
            bool flag = false;


            if (!File.Exists(analysis_file)) return;
            user_path = Path.GetDirectoryName(file_name);

            if (File.Exists(analysis_file))
            {
                List<string> list = new List<string>(File.ReadAllLines(analysis_file));
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ToUpper().Contains("LOAD GENE"))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            string ff = Path.Combine(Path.GetDirectoryName(analysis_file), "Live Load Analysis\\Input_Data_LL.txt");

            if (File.Exists(ff))
            {
                analysis_file = ff;
            }

            string wrkg_folder = Path.GetDirectoryName(analysis_file);
            string dl_file, ll_file;
            if (Path.GetFileName(wrkg_folder) == "Dead Load Analysis")
            {
                user_path = Path.GetDirectoryName(user_path);

                Deck_Analysis_DL.Input_File = analysis_file;
                dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");

                ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");
                Deck_Analysis_LL.Input_File = ll_file;
                ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ll_file);

                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }
            }
            else if (Path.GetFileName(wrkg_folder) == "Live Load Analysis")
            {
                ll_file = analysis_file;

                Deck_Analysis_LL.Input_File = analysis_file;
                ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");

                dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                Deck_Analysis_DL.Input_File = dl_file;
                dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }
            }
            else
            {

                analysis_file = Deck_Analysis_LL.Input_File;



                ll_file = analysis_file;

                Deck_Analysis_LL.Input_File = analysis_file;
                ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");

                dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                Deck_Analysis_DL.Input_File = dl_file;
                dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }
            }
            if (File.Exists(analysis_file))
            {
                //btn_Ana_DL_view_structure.Enabled = true;

                try
                {

                    if (flag)
                    {
                        if (Deck_Analysis_LL != null)
                        {
                            if (Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints.Count > 1)
                            {
                                Deck_Analysis_LL.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints[1].X / Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints[1].Z)));
                                //txt_Ana_skew_angle.Text = Deck_Analysis_LL.Skew_Angle.ToString();
                            }
                        }

                        txt_Ana_L.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length.ToString();
                        txt_Ana_B.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();
                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();

                        txt_support_distance.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        //txt_Ana_skew_angle.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Skew_Angle.ToString();



                        txt_gd_np.Text = (Deck_Analysis_LL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        MessageBox.Show(this, "File opened successfully.");
                    }
                    else
                    {
                        txt_Ana_L.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Length.ToString();
                        //txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                        txt_Ana_B.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_gd_np.Text = (Deck_Analysis_DL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");

                        txt_support_distance.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        MessageBox.Show(this, "File opened successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            string ll_txt = Path.Combine(user_path, "LL.txt");
            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);
            if (Deck_Analysis_DL.Live_Load_List == null) return;
        }

        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("UNIT kN ME");

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = null;


            if (true)
            {
                if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                foreach (LoadData ld in Deck_Analysis_LL.LoadList)
                {
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }
                foreach (LoadData ld in Deck_Analysis_LL.LoadList)
                {
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }

            }
            if (calc_width > Deck_Analysis_DL.WidthBridge)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Deck_Analysis_DL.WidthBridge;

                str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }

        #endregion Composite Methods


        private void frm_PSC_Box_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            //list.Add(string.Format("****SELF WEIGHT-16.0 T/M*****"));
            //list.Add(string.Format("LOAD 1 DEAD LOAD"));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("1 TO 12 UNI GY -16.0 "));
            //list.Add(string.Format("**** SUPERIMPOSED DEAD LOAD -8.2 T/M ****"));
            //list.Add(string.Format("**** FootPath Liveload 490 kg/m^2 Per Footpat*****"));
            //list.Add(string.Format("LOAD 2 SIDL+FPLL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 12 UNI GY -2.5"));
            //list.Add(string.Format("1 TO 12 UNI GY -0.98"));

            //txt_Ana_LL_member_load.Lines = list.ToArray();
            //Deck_Analysis_DL = new PSC_BoxGirderAnalysis(iApp);
            Bridge_Analysis = new PSC_BoxGirderAnalysis(iApp);
            //LongGirder = new PostTensionLongGirder(iApp);
            Deck = new RccDeckSlab(iApp);

            Load_Tab2_Tab3_Box_Segment_Data();
            Update_Tab3_Data();
            cmb_tab1_Fcu.SelectedIndex = 6;
            cmb_tab1_Fy.SelectedIndex = 1;



            //Chiranjit [2013 06 19]
            cmb_tab2_strand_data.SelectedIndex = 3;
            cmb_tab2_nc.SelectedIndex = 5;

            //Open_Project();
            Set_Project_Name();

            uC_BoxGirder1.iApp = iApp;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                Select_Moving_Load_Combo(dgv_british_loads, cmb_bs_view_moving_load);
            }



            txt_Ana_B.Text = "9.750";

            uC_BoxGirder_Stage1.txt_conc_pcnt.Text = "90";
            uC_BoxGirder_Stage2.txt_conc_pcnt.Text = "80";
            uC_BoxGirder_Stage3.txt_conc_pcnt.Text = "70";
            uC_BoxGirder_Stage4.txt_conc_pcnt.Text = "60";
            uC_BoxGirder_Stage5.txt_conc_pcnt.Text = "50";

            Emod_Changed(uC_BoxGirder_Stage1);
            Emod_Changed(uC_BoxGirder_Stage2);
            Emod_Changed(uC_BoxGirder_Stage3);
            Emod_Changed(uC_BoxGirder_Stage4);
            Emod_Changed(uC_BoxGirder_Stage5);



            tc_main.TabPages.Remove(tab_Segment);
            //tc_bridge_deck.TabPages.Add(tab_Segment);
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                cmb_HB.SelectedIndex = 0;

                British_Interactive();
                tbc_girder.TabPages.Remove(tab_moving_data_indian);

            }
            else
            {
                MovingLoad_Increment();

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                tbc_girder.TabPages.Remove(tab_moving_data_british);

            }


            //LongGirder = new PostTensionLongGirder(iApp);
            Deck = new RccDeckSlab(iApp);

            uC_PierDesignLSM1.iApp = iApp;

            #region Abutment & Pier

            //tc_limit_design.TabPages.Remove(tab_abutment);
            uC_RCC_Abut1.iApp = iApp;
            uC_RCC_Abut1.Load_Data();
            uC_RCC_Abut1.Is_Individual = false;

            uC_AbutmentPileLS1.SetIApplication(iApp);
            uC_PierDesignLSM1.iApp = iApp;
            uC_PierOpenLS1.SetIApplication(iApp);


            #endregion Abutment & Pier

            #region Crash Barrier and Footpath
            chk_crash_barrier.Checked = false;
            chk_crash_barrier.Checked = true;
            chk_cb_right.Checked = false;
            chk_footpath.Checked = false;
            chk_footpath.Checked = true;
            chk_fp_right.Checked = false;
            #endregion Crash Barrier and Footpath

            Load_Tab2_Tab3_Box_Segment_Data();
            Update_Tab3_Data();
            MovingLoad_Increment();
            cmb_tab1_Fcu.SelectedIndex = 6;
            cmb_tab1_Fy.SelectedIndex = 1;

            cmb_tab2_strand_data.SelectedIndex = 3;
            cmb_tab2_nc.SelectedIndex = 5;

            Set_Project_Name();

            uC_BoxGirder1.iApp = iApp;

            cmb_design_stage.SelectedIndex = 0;


            #region Add Limit State Method Live Loads

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                Default_Moving_LoadData(dgv_long_liveloads);
                Default_Moving_Type_LoadData(dgv_long_loads);
            }
            else
            {
                cmb_HB.SelectedIndex = 0;
                British_Interactive();
            }

            #endregion Add Limit State Method Live Loads


            tc_stage.TabPages.Remove(tab_designSage);
            tbc_girder.TabPages.Add(tab_designSage);

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                Select_Moving_Load_Combo(dgv_british_loads, cmb_bs_view_moving_load);
            }
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                Select_Moving_Load_Combo(dgv_british_loads, cmb_bs_view_moving_load);
            }
            else
                Select_Moving_Load_Combo(dgv_long_loads, cmb_irc_view_moving_load);
            SupportChanged();
            Button_Enable_Disable();

        }

        private void MovingLoad_Increment()
        {
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");
            }



            txt_deck_width.Text = txt_Ana_B.Text;

            double exp = MyList.StringToDouble(txt_exp_gap);
            double ovg = MyList.StringToDouble(txt_overhang_gap);
            txt_support_distance.Text = (exp / 2 + ovg).ToString("f4");

            try
            {
                double d = MyList.StringToDouble(txt_Ana_DL_eff_depth);
                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    dgv_seg_tab3_1[i, 0].Value = d - (0.3 * (i - 1));
                    dgv_seg_tab3_1[i, 1].Value = txt_Ana_B.Text;
                    dgv_seg_tab3_1[i, 3].Value = txt_Ana_width_cantilever.Text;


                    //dgv_seg_tab3_1[i, 0].Value = txt_Ana_DL_eff_depth.Text;
                    dgv_seg_tab3_1[i, 10].Value = (MyList.StringToDouble(txt_Ana_B.Text) * (6.75 / 9.75)).ToString("f3");
                    dgv_seg_tab3_1[i, 17].Value = (MyList.StringToDouble(txt_Ana_B.Text) * (1.85 / 9.75)).ToString("f3");


                }
                Update_Tab3_Data();
            }
            catch (Exception ex) { }


            //Text_Changed();

            if (uC_BoxGirder1.dgv_user_input.RowCount == 0)
            {
                uC_BoxGirder1.Load_Data();
            }
            uC_BoxGirder1.dgv_user_input[1, 0].Value = txt_Ana_L.Text;
            uC_BoxGirder1.dgv_user_input[1, 0].Style.ForeColor = Color.Red;

            uC_BoxGirder1.dgv_user_input[1, 1].Value = txt_support_distance.Text;
            uC_BoxGirder1.dgv_user_input[1, 1].Style.ForeColor = Color.Red;

            uC_BoxGirder1.dgv_user_input[1, 3].Value = B;
            uC_BoxGirder1.dgv_user_input[1, 3].Style.ForeColor = Color.Red;

            uC_BoxGirder1.dgv_user_input[1, 2].Value = txt_exp_gap.Text;
            uC_BoxGirder1.dgv_user_input[1, 2].Style.ForeColor = Color.Red;

            uC_BoxGirder1.dgv_user_input[1, 4].Value = txt_Ana_DL_eff_depth.Text;
            uC_BoxGirder1.dgv_user_input[1, 4].Style.ForeColor = Color.Red;


            uC_BoxGirder1.dgv_temp[1, 0].Value = txt_Ana_DL_eff_depth.Text;
            uC_BoxGirder1.dgv_temp[1, 0].Style.ForeColor = Color.Red;


            Change_Abutment_Pier_Input_Data();
        }
        void Change_Abutment_Pier_Input_Data()
        {
            DataGridView dgv = uC_RCC_Abut1.DGV_Input_Open;
            #region Abutment with Open Foundation
            //dgv[1, 1].Value = L1 + L2 + L3;
            dgv[1, 1].Value = txt_Ana_L.Text;
            dgv[1, 1].Style.ForeColor = Color.Red;

            dgv[1, 2].Value = txt_support_distance.Text;
            dgv[1, 2].Style.ForeColor = Color.Red;


            dgv[1, 3].Value = txt_overhang_gap.Text;
            dgv[1, 3].Style.ForeColor = Color.Red;

            dgv[1, 4].Value = txt_exp_gap.Text;
            dgv[1, 4].Style.ForeColor = Color.Red;


            dgv[1, 9].Value = txt_Ana_DL_eff_depth.Text;
            dgv[1, 9].Style.ForeColor = Color.Red;

            dgv[1, 10].Value = txt_Ana_Dw.Text;
            dgv[1, 10].Style.ForeColor = Color.Red;



            dgv[1, 84].Value = Math.Max(MyList.StringToDouble(txt_Ana_Wf_LHS), MyList.StringToDouble(txt_Ana_Wf_RHS));
            dgv[1, 84].Style.ForeColor = Color.Red;

            dgv[1, 86].Value = txt_Ana_Wk.Text;
            dgv[1, 86].Style.ForeColor = Color.Red;

            dgv[1, 87].Value = txt_Ana_Wr.Text;
            dgv[1, 87].Style.ForeColor = Color.Red;


          

            //dgv[1, 10].Value = txt_ana_wc.Text;
            #endregion Abutment with Open Foundation

            #region Abutment with Pile Foundation
            //uC_AbutmentPileLS1.txt_xls_inp_E13.Text = (L1 + L2 + L3).ToString();
            uC_AbutmentPileLS1.txt_xls_inp_E13.Text = txt_Ana_L.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E13.ForeColor = Color.Red;



            uC_AbutmentPileLS1.txt_xls_inp_E15.Text = txt_exp_gap.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E15.ForeColor = Color.Red;

            uC_AbutmentPileLS1.txt_xls_inp_E18.Text = txt_Ana_B.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E18.ForeColor = Color.Red;



            uC_AbutmentPileLS1.txt_xls_inp_E28.Text = txt_Ana_DL_eff_depth.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E28.ForeColor = Color.Red;


            //uC_AbutmentPileLS1.txt_xls_inp_E29.Text = txt_Ana_deck.Text;
            //uC_AbutmentPileLS1.txt_xls_inp_E29.ForeColor = Color.Red;

            //uC_AbutmentPileLS1.txt_xls_inp_E30.Text = txt_Ana_wearing.Text;
            //uC_AbutmentPileLS1.txt_xls_inp_E30.ForeColor = Color.Red;

            //uC_AbutmentPileLS1.txt_xls_inp_E33.Text = txt_Ana_NMG.Text;
            //uC_AbutmentPileLS1.txt_xls_inp_E33.ForeColor = Color.Red;

            //uC_AbutmentPileLS1.txt_xls_inp_E34.Text = txt_Ana_CG.Text;
            //uC_AbutmentPileLS1.txt_xls_inp_E34.ForeColor = Color.Red;


            uC_AbutmentPileLS1.txt_xls_inp_E14.Text = txt_support_distance.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E14.ForeColor = Color.Red;

            uC_AbutmentPileLS1.txt_xls_inp_E15.Text = (MyList.StringToDouble(txt_exp_gap.Text) * 1000).ToString();
            uC_AbutmentPileLS1.txt_xls_inp_E15.ForeColor = Color.Red;


            uC_AbutmentPileLS1.txt_xls_inp_E18.Text = txt_carriageway_width.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E18.ForeColor = Color.Red;

            uC_AbutmentPileLS1.txt_xls_inp_E22.Text = txt_Ana_Wc_LHS.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E22.ForeColor = Color.Red;

            uC_AbutmentPileLS1.txt_xls_inp_E23.Text = txt_Ana_Wf_LHS.Text;
            uC_AbutmentPileLS1.txt_xls_inp_E23.ForeColor = Color.Red;


            uC_AbutmentPileLS1.txt_xls_inp_E30.Text = (MyList.StringToDouble(txt_Ana_Dw.Text) * 1000).ToString();
            uC_AbutmentPileLS1.txt_xls_inp_E30.ForeColor = Color.Red;






            uC_AbutmentPileLS1.textBox4.Text = txt_Ana_B.Text;
            uC_AbutmentPileLS1.textBox4.ForeColor = Color.Red;


            #endregion Abutment with Open Foundation

            #region Pier with Open Foundation

            uC_PierOpenLS1.txt_xls_inp_G7.Text = txt_Ana_L.Text;
            uC_PierOpenLS1.txt_xls_inp_G7.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_H7.Text = txt_Ana_L.Text;
            uC_PierOpenLS1.txt_xls_inp_H7.ForeColor = Color.Red;



            uC_PierOpenLS1.txt_xls_inp_I7.Text = txt_Ana_L.Text;
            uC_PierOpenLS1.txt_xls_inp_I7.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_J7.Text = txt_Ana_L.Text;
            uC_PierOpenLS1.txt_xls_inp_J7.ForeColor = Color.Red;




            uC_PierOpenLS1.txt_xls_inp_G8.Text = txt_support_distance.Text;
            uC_PierOpenLS1.txt_xls_inp_G8.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_H8.Text = txt_support_distance.Text;
            uC_PierOpenLS1.txt_xls_inp_H8.ForeColor = Color.Red;



            uC_PierOpenLS1.txt_xls_inp_I8.Text = txt_support_distance.Text;
            uC_PierOpenLS1.txt_xls_inp_I8.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_J8.Text = txt_support_distance.Text;
            uC_PierOpenLS1.txt_xls_inp_J8.ForeColor = Color.Red;



            uC_PierOpenLS1.txt_xls_inp_G9.Text = (MyList.StringToDouble(txt_exp_gap.Text) * 1000).ToString();
            uC_PierOpenLS1.txt_xls_inp_G9.ForeColor = Color.Red;

            uC_PierOpenLS1.txt_xls_inp_H9.Text = (MyList.StringToDouble(txt_exp_gap.Text) * 1000).ToString();
            uC_PierOpenLS1.txt_xls_inp_H9.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_I9.Text = uC_PierOpenLS1.txt_xls_inp_H9.Text;
            uC_PierOpenLS1.txt_xls_inp_I9.ForeColor = Color.Red;

            uC_PierOpenLS1.txt_xls_inp_J9.Text = uC_PierOpenLS1.txt_xls_inp_H9.Text;
            uC_PierOpenLS1.txt_xls_inp_J9.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_G15.Text = txt_carriageway_width.Text;
            uC_PierOpenLS1.txt_xls_inp_G15.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_I15.Text = txt_carriageway_width.Text;
            uC_PierOpenLS1.txt_xls_inp_I15.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_G16.Text = txt_Ana_Wc_LHS.Text;
            uC_PierOpenLS1.txt_xls_inp_G16.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_I16.Text = txt_Ana_Wc_LHS.Text;
            uC_PierOpenLS1.txt_xls_inp_I16.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_G17.Text = txt_Ana_Wf_LHS.Text;
            uC_PierOpenLS1.txt_xls_inp_G17.ForeColor = Color.Red;

            uC_PierOpenLS1.txt_xls_inp_I17.Text = txt_Ana_Wf_LHS.Text;
            uC_PierOpenLS1.txt_xls_inp_I17.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_G19.Text = txt_Ana_Hc_LHS.Text;
            uC_PierOpenLS1.txt_xls_inp_G19.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_I19.Text = txt_Ana_Hc_LHS.Text;
            uC_PierOpenLS1.txt_xls_inp_I19.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_G22.Text = (MyList.StringToDouble(txt_Ana_Dw.Text) * 1000).ToString();
            uC_PierOpenLS1.txt_xls_inp_G22.ForeColor = Color.Red;

            uC_PierOpenLS1.txt_xls_inp_I22.Text = (MyList.StringToDouble(txt_Ana_Dw.Text) * 1000).ToString();
            uC_PierOpenLS1.txt_xls_inp_I22.ForeColor = Color.Red;


            uC_PierOpenLS1.txt_xls_inp_G25.Text = txt_Ana_DL_eff_depth.Text;
            uC_PierOpenLS1.txt_xls_inp_G25.ForeColor = Color.Red;

            uC_PierOpenLS1.txt_xls_inp_I25.Text = txt_Ana_DL_eff_depth.Text;
            uC_PierOpenLS1.txt_xls_inp_I25.ForeColor = Color.Red;

            //uC_PierOpenLS1.txt_xls_inp_G26.Text = txt_Ana_deck_depth.Text;
            //uC_PierOpenLS1.txt_xls_inp_G26.ForeColor = Color.Red;

            //uC_PierOpenLS1.txt_xls_inp_I26.Text = txt_Ana_deck_depth.Text;
            //uC_PierOpenLS1.txt_xls_inp_I26.ForeColor = Color.Red;


            #endregion Abutment with Open Foundation

            #region Pier with Pile Foundation


            uC_PierDesignLSM1.txt_GEN_G3.Text = txt_Ana_L.Text;
            uC_PierDesignLSM1.txt_GEN_G3.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_I3.Text = txt_Ana_L.Text;
            uC_PierDesignLSM1.txt_GEN_I3.ForeColor = Color.Red;




            //uC_PierDesignLSM1.txt_GEN_G5.Text = txt_exp_gap.Text;
            //uC_PierDesignLSM1.txt_GEN_G5.ForeColor = Color.Red;

            //uC_PierDesignLSM1.txt_GEN_I5.Text = txt_exp_gap.Text;
            //uC_PierDesignLSM1.txt_GEN_I5.ForeColor = Color.Red;



            //uC_PierDesignLSM1.txt_GEN_G3.Text = (L1).ToString();
            //uC_PierDesignLSM1.txt_GEN_G3.ForeColor = Color.Red;




            uC_PierDesignLSM1.txt_GEN_G4.Text = txt_support_distance.Text;
            uC_PierDesignLSM1.txt_GEN_G4.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_I4.Text = txt_support_distance.Text;
            uC_PierDesignLSM1.txt_GEN_I4.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_G5.Text = (MyList.StringToDouble(txt_exp_gap.Text) * 1000).ToString();
            uC_PierDesignLSM1.txt_GEN_G5.ForeColor = Color.Red;

            uC_PierDesignLSM1.txt_GEN_I5.Text = (MyList.StringToDouble(txt_exp_gap.Text) * 1000).ToString();
            uC_PierDesignLSM1.txt_GEN_I5.ForeColor = Color.Red;



            uC_PierDesignLSM1.txt_GEN_G6.Text = (MyList.StringToDouble(txt_Ana_L.Text) + 2 * MyList.StringToDouble(txt_overhang_gap.Text)).ToString();
            uC_PierDesignLSM1.txt_GEN_G6.ForeColor = Color.Red;

            uC_PierDesignLSM1.txt_GEN_I6.Text = (MyList.StringToDouble(txt_Ana_L.Text) + 2 * MyList.StringToDouble(txt_overhang_gap.Text)).ToString();
            uC_PierDesignLSM1.txt_GEN_I6.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_G8.Text = txt_Ana_DL_eff_depth.Text;
            uC_PierDesignLSM1.txt_GEN_G8.ForeColor = Color.Red;

            uC_PierDesignLSM1.txt_GEN_I8.Text = txt_Ana_DL_eff_depth.Text;
            uC_PierDesignLSM1.txt_GEN_I8.ForeColor = Color.Red;



            uC_PierDesignLSM1.txt_GEN_G9.Text = (MyList.StringToDouble(txt_Ana_Dw.Text) * 1000).ToString();
            uC_PierDesignLSM1.txt_GEN_G9.ForeColor = Color.Red;



            uC_PierDesignLSM1.txt_GEN_I9.Text = (MyList.StringToDouble(txt_Ana_Dw.Text) * 1000).ToString();
            uC_PierDesignLSM1.txt_GEN_I9.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_G12.Text = txt_Ana_B.Text;
            uC_PierDesignLSM1.txt_GEN_G12.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_G13.Text = txt_carriageway_width.Text;
            uC_PierDesignLSM1.txt_GEN_G13.ForeColor = Color.Red;


            uC_PierDesignLSM1.txt_GEN_G13.Text = txt_carriageway_width.Text;
            uC_PierDesignLSM1.txt_GEN_G13.ForeColor = Color.Red;



            #endregion Abutment with Open Foundation
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

        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();

            if (dgv_live_load.Name == dgv_long_loads.Name)
            {
                #region Long Girder
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 3"));
                list.Add(string.Format("X,-13.4"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1"));
                list.Add(string.Format("X,-18.8"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 1,TYPE 1,TYPE 1"));


                list.Add(string.Format("X,-18.8,-18.8,-18.8"));

                if (Project_Type == eASTRADesignType.Extradossed_Central_Towers_Bridge_LS)
                    list.Add(string.Format("Z,1.5,4.5,10.5"));
                else
                    list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 5,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-13.4"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 6,TYPE 3,TYPE 1"));
                list.Add(string.Format("X,-13.4,-18.8"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                //list.Add(string.Format("TOTAL LOAD,TYPE 1,TYPE 1,TYPE 1"));
                //list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                //list.Add(string.Format("Z,1.5,4.5,7.5"));
                //list.Add(string.Format(""));
                #endregion
            }

            for (i = 0; i < list.Count; i++)
            {

                dgv_live_load.Rows.Add(lst_spcs.ToArray());
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


        public void Select_Moving_Load_Combo(DataGridView dgv, ComboBox cmb)
        {
            string load = "";
            cmb.Items.Clear();
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                load = dgv[0, i].Value.ToString();
                if (load.StartsWith("LOAD"))
                {
                    if (!cmb.Items.Contains(load))
                        cmb.Items.Add(load);
                }
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;

        }
        private void Open_Project()
        {


            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                IsCreate_Data = false;


                string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                Segment_Girder.FilePath = user_path;
                uC_RCC_Abut1.iApp = iApp;


                Set_Segment_Data();
                Open_AnalysisFile(chk_file);

                IsRead = true;
                iApp.Read_Form_Record(this, user_path);
                IsRead = false;


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    British_Interactive();
                    Default_British_HB_Type_LoadData(dgv_british_loads);
                }

                Set_Box_Forces();
                Set_Segment_Data();
                Segment_Girder.FilePath = user_path;


                txt_Ana_analysis_file.Text = chk_file;


                uC_RCC_Abut1.Modified_Cells();



                if (iApp.IsDemo)
                    MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Button_Enable_Disable();
                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option

        }

        private void btn_Stressing_Graph_Click(object sender, EventArgs e)
        {
            try
            {
                string excel_path = Path.Combine(Application.StartupPath, @"DESIGN\Pre Stressed\Stressing Graph");
                string excel_file = "TECHSOFT_STRESSING_GRAPH.xls";
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_dwg_rcc_deck_Click(object sender, EventArgs e)
        {

        }

        //Chiranjit [2012 10 30]
        #region Design of RCC Pier


        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder_Pier";
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), draw_cmd);
        }

        void Text_Changed()
        {
            double L = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            double B = MyList.StringToDouble(txt_Ana_B.Text, 0.0);


            txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");

            txt_tab1_Lo.Text = L.ToString("f3");
            txt_tab1_DW.Text = B.ToString("f3");

            uC_RCC_Abut1.Length = L;
            uC_RCC_Abut1.Width = B;
            uC_RCC_Abut1.Overhang = MyList.StringToDouble(txt_support_distance.Text, 0.0);

            MovingLoad_Increment();
            Change_Abutment_Pier_Input_Data();
        }

        #endregion Design of RCC Pier



        private void btn_Open_Worksheet_Design_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = sender as Button;
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");
                //string excel_file = "PSC Box Girder\\" + b.Text + ".xls";
                //string excel_file = "PSC Box Girder\\New Design of PSC BOX Girder.xls";
                string excel_file = "PSC Box Girder\\" + b.Tag.ToString() + ".xls";
                excel_file = Path.Combine(excel_path, excel_file);
                //excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder";

            Button b = sender as Button;

            string draw = Drawing_Folder;



            eOpenDrawingOption opt = iApp.Open_Drawing_Option();
            if (opt == eOpenDrawingOption.Cancel) return;



            string excel_file = "PSC Box Girder\\" + "New Design Of PSC Box Girder.xls";
            //excel_file = Path.Combine(excel_path, excel_file);

            string copy_path = Path.Combine(Worksheet_Folder, excel_file);


            if (opt == eOpenDrawingOption.Design_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.PSC_BOX_Girder_GAD, Title, Drawing_Folder, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                }
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_PIER, Title, draw, copy_path).ShowDialog();
                }
                #endregion Design_Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), "PSC_Box_Girder");
                }
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                #endregion Design_Drawings
            }
        }

        private void btn_design_of_anchorage_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = sender as Button;
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");
                string excel_file = "PSC Box Girder\\" + "Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS";
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        #region Chiranjit [2012 02 08]
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            //rft.M2 = chk_M2.Checked;
            //rft.M3 = chk_M3.Checked;
            //rft.R3 = chk_R3.Checked;
            //rft.R2 = chk_R2.Checked;
            return rft;
        }

        #endregion
        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L.Text = "0";
                txt_Ana_L.Text = "48.75";
                txt_Ana_B.Text = "9.75";
                //txt_Ana_CW.Text = "10.75";txt_tab1_Lo

                //string str = "This is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "Email at: techsoft@consultant.com, dataflow@mail.com\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]
        public void Write_All_Data()
        {
            DemoCheck();

            iApp.Save_Form_Record(this, user_path);
        }
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(user_path)) return "";
                return Path.Combine(user_path, "ASTRA_DATA_FILE.TXT");

            }
        }

        public void Read_All_Data()
        {
            if (iApp.IsDemo) return;

            string data_file = User_Input_Data;

            if (!File.Exists(data_file)) return;
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            try
            {
                //LongGirder.FilePath = user_path;
                Deck.FilePath = user_path;

            }
            catch (Exception ex) { }

            List<string> file_content = new List<string>(File.ReadAllLines(data_file));

            ePSC_I_GirderOption TOpt = ePSC_I_GirderOption.None;

            MyList mlist = null;
            MyList mlist_mov_ll = null;
            string kStr = "";

            SaveRec.Clear();
            SaveRec.AddControls(this);

            for (int i = 0; i < file_content.Count; i++)
            {
                try
                {
                    kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                    mlist = new MyList(kStr, '=');
                    if (kStr.Contains("---")) continue;

                    if (mlist.Count == 2)
                    {
                        try
                        {
                            Control c = SaveRec.Controls[mlist.StringList[0]] as Control;
                            if (c != null)
                            {
                                if (c.Name.StartsWith("txt"))
                                    c.Text = mlist.StringList[1];
                                else if (c.Name.StartsWith("cmb"))
                                {
                                    //ComboBox cmb = (c as ComboBox);
                                    (c as ComboBox).SelectedItem = mlist.StringList[1];
                                }
                                else if (c.Name.StartsWith("dgv"))
                                {

                                    DataGridView dgv = c as DataGridView;
                                    int row = mlist.GetInt(1);
                                    dgv.Rows.Clear();
                                    i++;
                                    for (int j = 0; j < row; j++, i++)
                                    {
                                        kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                                        mlist = new MyList(kStr, '$');
                                        dgv.Rows.Add(mlist.StringList.ToArray());

                                    }

                                }

                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + kStr);
                }
            }
        }

        private void txt_Ana_DL_length_TextChanged(object sender, EventArgs e)
        {

            txt_deck_width.Text = txt_Ana_B.Text;
            try
            {

                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    dgv_seg_tab3_1[i, 0].Value = txt_Ana_DL_eff_depth.Text;
                    dgv_seg_tab3_1[i, 1].Value = txt_Ana_B.Text;
                    dgv_seg_tab3_1[i, 3].Value = txt_Ana_width_cantilever.Text;


                    //dgv_seg_tab3_1[i, 0].Value = txt_Ana_DL_eff_depth.Text;
                    dgv_seg_tab3_1[i, 10].Value = (MyList.StringToDouble(txt_Ana_B.Text) * (6.75 / 9.75)).ToString("f3");
                    dgv_seg_tab3_1[i, 17].Value = (MyList.StringToDouble(txt_Ana_B.Text) * (1.85 / 9.75)).ToString("f3");

                }
            }
            catch (Exception ex) { }



            Text_Changed();



            uC_BoxGirder1.txt_BM_DL_Supp.Text = uc_Res.txt_Ana_dead_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Deff.Text = uc_Res.txt_Ana_dead_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L8.Text = uc_Res.txt_Ana_dead_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L4.Text = uc_Res.txt_Ana_dead_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_DL_3L8.Text = uc_Res.txt_Ana_dead_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Mid.Text = uc_Res.txt_Ana_dead_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_DL_Supp.Text = uc_Res.txt_Ana_dead_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Deff.Text = uc_Res.txt_Ana_dead_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L8.Text = uc_Res.txt_Ana_dead_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L4.Text = uc_Res.txt_Ana_dead_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_DL_3L8.Text = uc_Res.txt_Ana_dead_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Mid.Text = uc_Res.txt_Ana_dead_inner_long_L2_shear.Text;


            uC_BoxGirder1.txt_BM_LL_Supp.Text = uc_Res.txt_Ana_live_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Deff.Text = uc_Res.txt_Ana_live_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L8.Text = uc_Res.txt_Ana_live_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L4.Text = uc_Res.txt_Ana_live_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_LL_3L8.Text = uc_Res.txt_Ana_live_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Mid.Text = uc_Res.txt_Ana_live_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_LL_Supp.Text = uc_Res.txt_Ana_live_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Deff.Text = uc_Res.txt_Ana_live_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L8.Text = uc_Res.txt_Ana_live_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L4.Text = uc_Res.txt_Ana_live_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_LL_3L8.Text = uc_Res.txt_Ana_live_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Mid.Text = uc_Res.txt_Ana_live_inner_long_L2_shear.Text;





        }

        public void Load_Tab2_Tab3_Box_Segment_Data()
        {

            List<string> list = new List<string>();
            list.Add(string.Format("D 2.5 2.5 2.5 2.5 2.5 2.5"));
            list.Add(string.Format("Dw 9.75 9.75 9.75 9.75 9.75 9.75"));
            list.Add(string.Format("Td 0.225 0.225 0.225 0.225 0.225 0.225"));
            list.Add(string.Format("C1 1.925 1.925 1.925 1.925 1.925 1.925"));
            list.Add(string.Format("C2 0 0 0 0 0 0"));
            list.Add(string.Format("Tip 0.2 0.2 0.2 0.2 0.2 0.2"));
            list.Add(string.Format("Tf 0.3 0.3 0.3 0.3 0.3 0.3"));
            list.Add(string.Format("Iw 0.7 0.7 0.7 0.7 0.7 0.7"));
            list.Add(string.Format("D1 2.2 2.2 2.2 2.2 2.2 2.2"));
            list.Add(string.Format("Tw 0.6 0.579 0.48 0.31 0.31 0.31"));
            list.Add(string.Format("SW 4.5 4.5 4.5 4.5 4.5 4.5"));
            list.Add(string.Format("Ts 0.55 0.26 0.26 0.26 0.26 0.26"));
            list.Add(string.Format("D2 0 0 0 0 0 0"));
            list.Add(string.Format("K1 0 0 0 0 0 0"));
            list.Add(string.Format("K2 0.175 0.0827 0.0827 0.0827 0.0827 0.0827"));
            list.Add(string.Format("HW1 0.409 0.485 0.582 0.75 0.75 0.75"));
            list.Add(string.Format("HH1 0.083 0.097 0.116 0.15 0.15 0.15"));
            list.Add(string.Format("CH1 1.85 1.85 1.85 1.85 1.85 1.85"));
            list.Add(string.Format("HW2 0 0 0 0 0 0"));
            list.Add(string.Format("HH2 0 0 0 0 0 0"));
            list.Add(string.Format("HW3 0 0 0.088 0.3 0.3 0.3"));
            list.Add(string.Format("HH3 0 0 0.044 0.15 0.15 0.15"));


            MyList mlist = null;


            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ' ');
                dgv_seg_tab3_1.Rows.Add(mlist.StringList.ToArray());
            }

            Set_Segment_Data();

        }

        private void Set_Segment_Data()
        {


            for (int i = 0; i < dgv_seg_tab3_1.Rows.Count; i++)
            {

                if (i == 0)
                    dgv_seg_tab3_1.Rows[i].Height = 26;
                else if (i > 13)
                    dgv_seg_tab3_1.Rows[i].Height = 16;
                else
                    dgv_seg_tab3_1.Rows[i].Height = 15;

                if (dgv_seg_tab3_1[0, i].Value.ToString() == "D2" ||
                    dgv_seg_tab3_1[0, i].Value.ToString() == "K1" ||
                    dgv_seg_tab3_1[0, i].Value.ToString() == "K2")
                {
                    dgv_seg_tab3_1.Rows[i].ReadOnly = true;
                    dgv_seg_tab3_1.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    dgv_seg_tab3_1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
        }

        public void Segment_Girder_Initialize_Data()
        {
            #region Variable Declaration
            //Chiranjit [2012 10 26]
            Segment_Girder.Area_Zone1_Outer = MyList.StringToDouble(txt_zn1_out.Text, 1480.0);
            Segment_Girder.Area_Zone2_Outer = MyList.StringToDouble(txt_zn2_out.Text, 1480.0);
            Segment_Girder.Area_Zone3_Outer = MyList.StringToDouble(txt_zn3_out.Text, 1480.0);
            Segment_Girder.Area_Zone1_Inner = MyList.StringToDouble(txt_zn1_inn.Text, 1480.0);
            Segment_Girder.Area_Zone2_Inner = MyList.StringToDouble(txt_zn2_inn.Text, 1480.0);
            Segment_Girder.Area_Zone3_Inner = MyList.StringToDouble(txt_zn3_inn.Text, 1480.0);


            //Chiranjit [2012 10 17]
            Segment_Girder.rss_56 = MyList.StringToDouble(txt_tab2_rss_56.Text, 0.00019);
            Segment_Girder.rss_14 = MyList.StringToDouble(txt_tab2_rss_14.Text, 0.00025);
            Segment_Girder.Resh56 = MyList.StringToDouble(txt_tab2_Resh56.Text, 0.00025);
            Segment_Girder.Crst56 = MyList.StringToDouble(txt_tab2_Crst56.Text, 0.00025);


            //Chiranjit [2012 10 18]
            Segment_Girder.fc_temp14 = MyList.StringToDouble(txt_fc_temp14.Text, 0.00025);
            Segment_Girder.ft_temp14 = MyList.StringToDouble(txt_ft_temp14.Text, 0.00025);
            Segment_Girder.fc_temp28 = MyList.StringToDouble(txt_fc_temp28.Text, 0.00025);
            Segment_Girder.ft_temp28 = MyList.StringToDouble(txt_ft_temp28.Text, 0.00025);
            Segment_Girder.ttv = MyList.StringToDouble(txt_ttv.Text, 0.00025);
            Segment_Girder.fc_serv = MyList.StringToDouble(txt_fc_serv.Text, 0.00025);
            Segment_Girder.Modrup = MyList.StringToDouble(txt_Mod_rup.Text, 0.00025);
            Segment_Girder.fc_fact = MyList.StringToDouble(txt_fc_factor.Text, 0.00025);
            Segment_Girder.tv = MyList.StringToDouble(txt_tv.Text, 0.00025);
            Segment_Girder.ttu = MyList.StringToDouble(txt_ttu.Text, 0.00025);





            Segment_Girder.Lo = MyList.StringToDouble(txt_tab1_Lo.Text, 48.750);

            Segment_Girder.L1 = MyList.StringToDouble(txt_tab1_L1.Text, 0.500);
            Segment_Girder.L2 = MyList.StringToDouble(txt_tab1_L2.Text, 0.500);
            Segment_Girder.exg = MyList.StringToDouble(txt_tab1_exg.Text, 0.040);

            Segment_Girder.Dw = MyList.StringToDouble(txt_tab1_DW.Text, 9.750);
            Segment_Girder.D = MyList.StringToDouble(txt_tab1_D.Text, 2.500);
            Segment_Girder.Fcu = MyList.StringToDouble(cmb_tab1_Fcu.Text, 40);
            Segment_Girder.Tab1_Fy = MyList.StringToDouble(cmb_tab1_Fy.Text, 415);
            Segment_Girder.act = MyList.StringToDouble(txt_tab1_act.Text, 14);
            Segment_Girder.mct = MyList.StringToDouble(txt_tab1_Mct.Text, 87);
            Segment_Girder.sct = MyList.StringToDouble(txt_tab1_sctt.Text, 34.8);
            Segment_Girder.acsidl = MyList.StringToDouble(txt_tab1_agt_SIDL.Text, 56);
            Segment_Girder.mtcsidl = MyList.StringToDouble(txt_tab1_Mct_SIDL.Text, 100);
            Segment_Girder.T_loss = MyList.StringToDouble(txt_tab1_T_loss.Text, 20);
            Segment_Girder.wct = MyList.StringToDouble(txt_tab1_wct.Text, 0.065);
            Segment_Girder.ds = MyList.StringToDouble(txt_tab1_ds.Text, 0.225);
            Segment_Girder.df = MyList.StringToDouble(txt_tab1_df.Text, 0.175);
            Segment_Girder.bt = MyList.StringToDouble(txt_tab1_bt.Text, 1.000);

            Segment_Girder.FactDL = MyList.StringToDouble(txt_tab1_FactDL.Text, 1.250);
            Segment_Girder.FactSIDL = MyList.StringToDouble(txt_tab1_FactSIDL.Text, 2.000);
            Segment_Girder.FactLL = MyList.StringToDouble(txt_tab1_FactLL.Text, 2.500);

            Segment_Girder.alpha = MyList.StringToDouble(txt_tab1_alpha.Text, 0.0000117);


            Segment_Girder.Tr1 = MyList.StringToDouble(txt_tab1_Tr1.Text, 17.8);
            Segment_Girder.Tr2 = MyList.StringToDouble(txt_tab1_Tr2.Text, 4.0);
            Segment_Girder.Tr3 = MyList.StringToDouble(txt_tab1_Tr3.Text, 2.1);


            Segment_Girder.Tf1 = MyList.StringToDouble(txt_tab1_Tf1.Text, 10.6);
            Segment_Girder.Tf2 = MyList.StringToDouble(txt_tab1_Tf2.Text, 0.7);
            Segment_Girder.Tf3 = MyList.StringToDouble(txt_tab1_Tf3.Text, 0.8);
            Segment_Girder.Tf4 = MyList.StringToDouble(txt_tab1_Tf4.Text, 6.6);


            //Prestressing Input Data:   [Tab 2]

            //A)                 Construction Schedule and Prestressing Stages

            //    Job                                                                                                 Day after casting                 fcj   N/sq.mm (Mpa)
            //(i)                Completion of casting of Box Girder                                0                day
            //(ii)                First Stage Prestress                                                                14                day                                fcj14 = 34.80
            //(iii)                Completion of Wearing Course & Crash Barrier                56                day                                fcj56 = 40.00


            Segment_Girder.ccbg_day = MyList.StringToDouble(txt_tab2_ccbg_day.Text, 10.6);
            Segment_Girder.ccbg_fcj = MyList.StringToDouble(txt_tab2_ccbg_fcj.Text, 0.7);

            Segment_Girder.fsp_day = MyList.StringToDouble(txt_tab2_fsp_day.Text, 10.6);
            Segment_Girder.fsp_fcj = MyList.StringToDouble(txt_tab2_fsp_fcj.Text, 0.7);

            Segment_Girder.cwccb_day = MyList.StringToDouble(txt_tab2_cwccb_day.Text, 10.6);
            Segment_Girder.cwccb_fcj = MyList.StringToDouble(txt_tab2_cwccb_fcj.Text, 0.7);



            //B)                Cable and Prestressing Data

            //D                = 15.200 mm.                                   
            //A = 140.000 Sq.mm.
            //Pu = 1.100 Kg/m
            //Fy = 1670.000 N/Sq.mm. (Mpa)
            //Fu = 1860.000 N/Sq.mm. (Mpa)
            //Pn = 260.700 kN
            //Eps = 195 Gpa
            //Pj = 76.5 %
            //s = 6 mm.
            //µ = 0.17 per radian
            //k = 0.002 per metre
            //Re1 = 35.0 N/Sq.mm (Mpa)
            //Re2 = 0.0 N/Sq.mm (Mpa)
            //td1 = 14 days                     (Taken From Tab 1)
            //qd = 110 mm.
            //Fcu = 40 N/Sq.mm (Mpa)                                                  (Taken  From Tab 1)
            //Ec = 5000 x Sqrt(40) = 31622.8 N/Sq.mm (Mpa)


            Segment_Girder.ND = MyList.StringToDouble(txt_tab2_D.Text, 15.200);
            Segment_Girder.NA = MyList.StringToDouble(txt_tab2_A.Text, 140.000);
            Segment_Girder.Pu = MyList.StringToDouble(txt_tab2_Pu.Text, 1.100);
            Segment_Girder.Tab2_Fy = MyList.StringToDouble(txt_tab2_Fy.Text, 1670.000);
            Segment_Girder.Fu = MyList.StringToDouble(txt_tab2_Fu.Text, 1860.000);
            Segment_Girder.Pn = MyList.StringToDouble(txt_tab2_Pn.Text, 260.700);
            Segment_Girder.Eps = MyList.StringToDouble(txt_tab2_Eps.Text, 195);
            Segment_Girder.Pj = MyList.StringToDouble(txt_tab2_Pj.Text, 76.5);
            Segment_Girder.s = MyList.StringToDouble(txt_tab2_s.Text, 6);
            Segment_Girder.mu = MyList.StringToDouble(txt_tab2_mu.Text, 0.17);
            Segment_Girder.k = MyList.StringToDouble(txt_tab2_k.Text, 0.002);
            Segment_Girder.Re1 = MyList.StringToDouble(txt_tab2_Re1.Text, 35.0);
            Segment_Girder.Re2 = MyList.StringToDouble(txt_tab2_Re2.Text, 0.0);
            Segment_Girder.td1 = MyList.StringToDouble(txt_tab2_td1.Text, 14);
            Segment_Girder.qd = MyList.StringToDouble(txt_tab2_qd.Text, 110);
            Segment_Girder.cover1 = MyList.StringToDouble(txt_tab2_cover1.Text, 110);
            Segment_Girder.cover2 = MyList.StringToDouble(txt_tab2_cover2.Text, 110);
            Segment_Girder.Ns = MyList.StringToDouble(txt_tab2_Ns.Text, 19);
            Segment_Girder.Nc_Left = MyList.StringToInt(txt_tab2_nc_left.Text, 7);
            Segment_Girder.Nc_Right = MyList.StringToInt(txt_tab2_nc_right.Text, 7);
            Segment_Girder.Cable_Area = MyList.StringToInt(txt_tab2_cable_area.Text, 7);


            Segment_Girder.Section_Theta.Clear();
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_support.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_d.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_L_8.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_L_4.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_3L_8.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_L_2.Text, 0.0));


            PSC_Force_Data list_va = new PSC_Force_Data(0);
            for (int i = 0; i < dgv_seg_tab3_1.RowCount; i++)
            {
                list_va = new PSC_Force_Data(0);
                for (int c = 1; c < dgv_seg_tab3_1.ColumnCount; c++)
                {
                    list_va.Add(MyList.StringToDouble(dgv_seg_tab3_1[c, i].Value.ToString(), 0.0));
                }

                if (i == 0)
                    Segment_Girder.Section_D = list_va;

                else if (i == 1)
                    Segment_Girder.Section_Dw = list_va;

                else if (i == 2)
                    Segment_Girder.Section_Td = list_va;

                else if (i == 3)
                    Segment_Girder.Section_C1 = list_va;

                else if (i == 4)
                    Segment_Girder.Section_C2 = list_va;

                else if (i == 5)
                    Segment_Girder.Section_Tip = list_va;

                else if (i == 6)
                    Segment_Girder.Section_Tf = list_va;

                else if (i == 7)
                    Segment_Girder.Section_lw = list_va;

                else if (i == 8)
                    Segment_Girder.Section_D1 = list_va;

                else if (i == 9)
                    Segment_Girder.Section_Tw = list_va;

                else if (i == 10)
                    Segment_Girder.Section_SW = list_va;

                else if (i == 11)
                    Segment_Girder.Section_Ts = list_va;

                else if (i == 12)
                    Segment_Girder.Section_D2 = list_va;

                else if (i == 13)
                    Segment_Girder.Section_K1 = list_va;

                else if (i == 14)
                    Segment_Girder.Section_K2 = list_va;

                else if (i == 15)
                    Segment_Girder.Section_HW1 = list_va;

                else if (i == 16)
                    Segment_Girder.Section_HH1 = list_va;

                else if (i == 17)
                    Segment_Girder.Section_CH1 = list_va;

                else if (i == 18)
                    Segment_Girder.Section_HW2 = list_va;

                else if (i == 19)
                    Segment_Girder.Section_HH2 = list_va;

                else if (i == 20)
                    Segment_Girder.Section_HW3 = list_va;

                else if (i == 21)
                    Segment_Girder.Section_HH3 = list_va;
            }

            //Segment_Girder.Fcu = 40;
            //Ec = 31622.8;
            //double Ec = 5000 x rt(40) = 31622.8 N/.mm (Mpa)


            //Chiranjit [2013 06 19]
            Segment_Girder.Cable_Type = cmb_tab2_strand_data.Text;


            //Chiranjit [2013 06 19]
            Segment_Girder.L_Deff = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);



            #endregion Variable Declaration
        }

        private void btn_segment_process_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Segment_Girder.FilePath = user_path;
            Segment_Girder_Initialize_Data();
            Box_Forces.Set_Absolute();
            //Segment_Girder.Calculate_Program(Segment_Girder.rep_file_name, Box_Forces);
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(Segment_Girder.rep_file_name))
            {
                MessageBox.Show(this, "Report file written in " + Segment_Girder.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BridgeAnalysisDesign.PSC_BoxGirder.frm_BoxGirder_Msg.Msg_Showw();
                iApp.View_Result(Segment_Girder.rep_file_name, true);
            }
            Segment_Girder.is_process = true;
            Button_Enable_Disable();
        }

        private void txt_tab2_Fcu_TextChanged(object sender, EventArgs e)
        {
            txt_tab2_Ec.Text = (5000.0 * Math.Sqrt(MyList.StringToDouble(txt_tab2_Fcu.Text, 0.0))).ToString("f2");
        }

        private void dgv_seg_tab3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Update_Tab3_Data();
        }

        private void Update_Tab3_Data()
        {
            try
            {
                double theta = 0.0;
                double D2 = 0.0;
                double K1 = 0.0;
                double K2 = 0.0;
                double va = 5;
                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    theta = Math.Atan((MyList.StringToDouble(dgv_seg_tab3_1[i, 7].Value.ToString(), 0.0) /
                            MyList.StringToDouble(dgv_seg_tab3_1[i, 8].Value.ToString(), 0.0)));

                    va = (180.0 / Math.PI) * theta;

                    //Web Inclination = θ (degrees) = atan (Iw / D1) = atan (0.7 / 2.200) = atan(0.3182) = 17.650
                    //and  D2 = D – Tf – D1 = 2.500 – 0.300 – 2.200 = 0.0
                    D2 = (MyList.StringToDouble(dgv_seg_tab3_1[i, 0].Value.ToString(), 0.0) -
                            MyList.StringToDouble(dgv_seg_tab3_1[i, 6].Value.ToString(), 0.0) -
                            MyList.StringToDouble(dgv_seg_tab3_1[i, 8].Value.ToString(), 0.0));

                    K1 = D2 * Math.Tan(theta);
                    K2 = MyList.StringToDouble(dgv_seg_tab3_1[i, 11].Value.ToString(), 0.0) * Math.Tan(theta);


                    dgv_seg_tab3_1[i, 12].Value = D2.ToString("f3");
                    dgv_seg_tab3_1[i, 13].Value = K1.ToString("f3");
                    dgv_seg_tab3_1[i, 14].Value = K2.ToString("f3");
                    //K1 = D2 x tan(θ) = 0.0 x tan(17.6501) = 0.0
                    //K2 = Ts x tan(θ) = 0.550 x tan(17.6501) = 0.1750
                    theta = (180.0 / Math.PI) * theta;
                    if (i == 1) txt_tab3_support.Text = theta.ToString("f3");
                    if (i == 2) txt_tab3_d.Text = theta.ToString("f3");
                    if (i == 3) txt_tab3_L_8.Text = theta.ToString("f3");
                    if (i == 4) txt_tab3_L_4.Text = theta.ToString("f3");
                    if (i == 5) txt_tab3_3L_8.Text = theta.ToString("f3");
                    if (i == 6) txt_tab3_L_2.Text = theta.ToString("f3");



                    txt_tab1_ds.Text = dgv_seg_tab3_1[i, 2].Value.ToString();

                }


                if (uC_BoxGirder1.dgv_seg_tab3.RowCount == 0)
                {
                    uC_BoxGirder1.Load_Data();
                }
                for (int i = 0; i < dgv_seg_tab3_1.RowCount; i++)
                {
                    for (int c = 0; c < uC_BoxGirder1.dgv_seg_tab3.ColumnCount; c++)
                    {
                        uC_BoxGirder1.dgv_seg_tab3[c, i].Value = dgv_seg_tab3_1[c, i].Value;
                    }
                }
                uC_BoxGirder1.Update_Tab3_Data();

            }
            catch (Exception ex) { }
        }

        private void btn_Show_Section_Result_Click(object sender, EventArgs e)
        {
            Show_Section_Result();
        }
        private void txt_tot_AX_TextChanged(object sender, EventArgs e)
        {
            double tot_Ax = MyList.StringToDouble(txt_tot_AX);
            double tot_Ix = MyList.StringToDouble(txt_tot_IXX);
            double tot_Iy = MyList.StringToDouble(txt_tot_IYY);
            double tot_Iz = MyList.StringToDouble(txt_tot_IZZ);

            double cen_Pcnt = MyList.StringToDouble(txt_cen_pcnt) / 100;
            double inn_Pcnt = MyList.StringToDouble(txt_inn_pcnt) / 100;
            double out_Pcnt = MyList.StringToDouble(txt_out_pcnt) / 100;

            txt_cen_AX.Text = (tot_Ax * cen_Pcnt).ToString("f3");
            txt_cen_IXX.Text = (tot_Ix * cen_Pcnt).ToString("f3");
            txt_cen_IYY.Text = (tot_Iy * cen_Pcnt).ToString("f3");
            txt_cen_IZZ.Text = (tot_Iz * cen_Pcnt).ToString("f3");

            txt_inn_AX.Text = (tot_Ax * inn_Pcnt).ToString("f3");
            txt_inn_IXX.Text = (tot_Ix * inn_Pcnt).ToString("f3");
            txt_inn_IYY.Text = (tot_Iy * inn_Pcnt).ToString("f3");
            txt_inn_IZZ.Text = (tot_Iz * inn_Pcnt).ToString("f3");

            txt_out_AX.Text = (tot_Ax * out_Pcnt).ToString("f3");
            txt_out_IXX.Text = (tot_Ix * out_Pcnt).ToString("f3");
            txt_out_IYY.Text = (tot_Iy * out_Pcnt).ToString("f3");
            txt_out_IZZ.Text = (tot_Iz * out_Pcnt).ToString("f3");
        }

        private void Show_Section_Result()
        {

            //Segment_Girder.FilePath = user_path;
            Segment_Girder_Initialize_Data();
            rtb_sections.Lines = Segment_Girder.Get_Step_1(ref PSC_SECIONS).ToArray();


            double a = (MyList.Get_Array_Sum(PSC_SECIONS.Area) / PSC_SECIONS.Area.Count);
            double ix = (MyList.Get_Array_Sum(PSC_SECIONS.Ixx) / PSC_SECIONS.Area.Count);
            double iy = (MyList.Get_Array_Sum(PSC_SECIONS.Iyy) / PSC_SECIONS.Area.Count);
            double iz = (MyList.Get_Array_Sum(PSC_SECIONS.Izz) / PSC_SECIONS.Area.Count);
            txt_tot_AX.Text = a.ToString("f3");
            txt_tot_IXX.Text = ix.ToString("f3");
            txt_tot_IYY.Text = iy.ToString("f3");
            txt_tot_IZZ.Text = (ix + iy).ToString("f3");

        }


        private void btn_segment_report_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Segment_Girder.rep_file_name))
                    iApp.View_Result(Segment_Girder.rep_file_name);
            }
            catch (Exception ex) { }
        }

        private void txt_Ana_DL_factor_TextChanged(object sender, EventArgs e)
        {
            //lbl_factor.Text = "DL X " + DL_Factor + " + LL X " + LL_Factor;
            Text_Changed_Forces();

        }

        private void txt_tab1_Lo_TextChanged(object sender, EventArgs e)
        {
            txt_tab1_L.Text = (MyList.StringToDouble(txt_tab1_Lo.Text, 0.0) - 2 * MyList.StringToDouble(txt_tab1_L1.Text, 0.0)).ToString("f3");
        }

        private void cmb_tab1_Fcu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Concrete_Grade();
        }

        private void Set_Concrete_Grade()
        {

            double mct_14 = MyList.StringToDouble(txt_tab1_Mct.Text, 0.0);
            double mct_SIDL = MyList.StringToDouble(txt_tab1_Mct_SIDL.Text, 0.0);

            double fcu = MyList.StringToDouble(cmb_tab1_Fcu.Text, 0.0);

            //txt_tab1_Mct.Text = "";
            txt_tab1_sctt.Text = (mct_14 / 100.0 * fcu).ToString("f3");
            txt_tab2_fsp_fcj.Text = txt_tab1_sctt.Text;
            txt_tab2_cwccb_fcj.Text = (mct_SIDL / 100.0 * fcu).ToString("f3");



            txt_tab2_Fcu.Text = cmb_tab1_Fcu.Text;


            //txt_tab1_act.Text = cmb_tab1_Fcu.Text;

            txt_tab2_fsp_day.Text = txt_tab1_act.Text;


            //txt_tab1_agt_SIDL.Text = cmb_tab1_Fcu.Text;


            txt_tab2_cwccb_day.Text = txt_tab1_agt_SIDL.Text;




        }

        private void cmb_tab2_strand_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            double D = 0.0, A = 0.0, Pu = 0.0, Fy = 0.0, Fu = 0.0, Pn = 0.0;

            if (cmb_tab2_strand_data.SelectedIndex == 0)
            {
                D = 12.9;
                A = 100.0;
                Pu = 0.785;
                Fy = 1580.0;
                Fu = 1860.0;
                Pn = 186.0;
            }
            else if (cmb_tab2_strand_data.SelectedIndex == 1)
            {
                D = 12.7;
                A = 98.7;
                Pu = 0.775;
                Fy = 1670.0;
                Fu = 1860.0;
                Pn = 183.7;
            }
            else if (cmb_tab2_strand_data.SelectedIndex == 2)
            {
                D = 15.7;
                A = 150;
                Pu = 1.18;
                Fy = 1500;
                Fu = 1770;
                Pn = 265.0;
            }
            else if (cmb_tab2_strand_data.SelectedIndex == 3)
            {
                D = 15.2;
                A = 140;
                Pu = 1.10;
                Fy = 1670.0;
                Fu = 1860.0;
                Pn = 260.7;
            }

            txt_tab2_D.Text = D.ToString();
            txt_tab2_A.Text = A.ToString();
            txt_tab2_Pu.Text = Pu.ToString();
            txt_tab2_Fy.Text = Fy.ToString();
            txt_tab2_Fu.Text = Fu.ToString();
            txt_tab2_Pn.Text = Pn.ToString();
        }

        private void cmb_tab2_nc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_tab2_nc_left.Text = (cmb_tab2_nc.SelectedIndex + 2).ToString();
            txt_tab2_nc_right.Text = txt_tab2_nc_left.Text;

        }

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


                    if (!chk_ssprt_fixed_FX.Checked
                        || !chk_ssprt_fixed_FY.Checked
                        || !chk_ssprt_fixed_FZ.Checked
                        || !chk_ssprt_fixed_MX.Checked
                        || !chk_ssprt_fixed_MY.Checked
                        || !chk_ssprt_fixed_MZ.Checked)
                        kStr += " BUT";

                    if (!chk_ssprt_fixed_FX.Checked) kStr += " FX";
                    if (!chk_ssprt_fixed_FY.Checked) kStr += " FY";
                    if (!chk_ssprt_fixed_FZ.Checked) kStr += " FZ";
                    if (!chk_ssprt_fixed_MX.Checked) kStr += " MX";
                    if (!chk_ssprt_fixed_MY.Checked) kStr += " MY";
                    if (!chk_ssprt_fixed_MZ.Checked) kStr += " MZ";
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
                    if (!chk_esprt_fixed_FX.Checked
                        || !chk_esprt_fixed_FY.Checked
                        || !chk_esprt_fixed_FZ.Checked
                        || !chk_esprt_fixed_MX.Checked
                        || !chk_esprt_fixed_MY.Checked
                        || !chk_esprt_fixed_MZ.Checked)
                        kStr += " BUT";
                    if (!chk_esprt_fixed_FX.Checked) kStr += " FX";
                    if (!chk_esprt_fixed_FY.Checked) kStr += " FY";
                    if (!chk_esprt_fixed_FZ.Checked) kStr += " FZ";
                    if (!chk_esprt_fixed_MX.Checked) kStr += " MX";
                    if (!chk_esprt_fixed_MY.Checked) kStr += " MY";
                    if (!chk_esprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
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
        #endregion Chiranjit [2014 03 12] Support Input

        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;

                Show_Section_Result();

                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }


                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }
                Write_All_Data();
                Text_Changed();
                Analysis_Initialize_InputData();


                Deck_Analysis_LL.Input_File = Input_File_LL;
                Deck_Analysis_LL.CreateData();
                Deck_Analysis_LL.WriteData_LiveLoad(Deck_Analysis_LL.Input_File, PSC_SECIONS);
                Write_Ana_Load_Data(true);
                Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

                string ll_txt = Deck_Analysis_LL.LiveLoad_File;

                Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                //iApp.View_MovingLoad();
                iApp.View_MovingLoad(Input_File_LL, 0.0, MyList.StringToDouble(txt_bs_vehicle_gap.Text));

            }
            catch (Exception ex) { }
        }



        #region Chiranjit [2014 09 10]
        #region British Standard Loading
        private void txt_deck_width_TextChanged(object sender, EventArgs e)
        {
            British_Interactive();

        }


        private void rbtn_HA_HB_CheckedChanged(object sender, EventArgs e)
        {
            British_Interactive();
            spc_HB.Visible = !rbtn_HA.Checked;

            if (rbtn_HA_HB.Checked || rbtn_HB.Checked)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));


                if (IsRead) return;

                Default_British_HB_LoadData(dgv_long_british_loads);
                Default_British_HB_Type_LoadData(dgv_british_loads);

                lbl_HB.Text = "HB LOADINGS";

                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            else if (rbtn_HA.Checked)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            else if (rbtn_Rail_Load.Checked)
            {
                lbl_HB.Text = "BS RAIL LOADINGS";

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));


                if (IsRead) return;

                Default_British_Rail_LoadData(dgv_long_british_loads);
                Default_British_Rail_Type_LoadData(dgv_british_loads);

                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
        }
        public bool IsRead = false;


        double Get_Max_Vehicle_Length()
        {
            double mvl = 13.4;

            List<double> lst_mvl = new List<double>();
            DataGridView dgv = dgv_long_british_loads;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value.ToString().StartsWith("AXLE SPACING"))
                    {
                        mvl = 0;
                        for (int c = 1; c < dgv.ColumnCount; c++)
                        {
                            try
                            {
                                mvl += MyList.StringToDouble(dgv[c, i].Value.ToString(), 0.0);
                            }
                            catch (Exception exx)
                            {

                            }
                        }
                        lst_mvl.Add(mvl);
                    }
                }
                catch (Exception ex1) { }

            }
            if (lst_mvl.Count > 0)
            {
                lst_mvl.Sort();
                lst_mvl.Reverse();
                mvl = lst_mvl[0];
            }
            return mvl;
        }
        public void British_Interactive()
        {
            if (IsRead) return;

            double d, lane_width, impf, lf;

            double incr, lgen;

            //txt_ll_british_incr
            int nos_lane, i;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);
            incr = MyList.StringToDouble(txt_ll_british_incr.Text, 0.0);

            if (incr == 0) incr = 1;
            lgen = ((int)((L + Get_Max_Vehicle_Length()) / incr)) + 1;

            nos_lane = (int)(d / lane_width);

            txt_no_lanes.Text = nos_lane.ToString();
            txt_no_lanes.Enabled = false;

            txt_ll_british_lgen.Text = lgen.ToString();
            txt_ll_british_lgen.Enabled = false;


            chk_HA_1L.Enabled = (nos_lane >= 1);
            chk_HA_2L.Enabled = (nos_lane >= 2);
            chk_HA_3L.Enabled = (nos_lane >= 3);
            chk_HA_4L.Enabled = (nos_lane >= 4);
            chk_HA_5L.Enabled = (nos_lane >= 5);
            chk_HA_6L.Enabled = (nos_lane >= 6);
            chk_HA_7L.Enabled = (nos_lane >= 7);
            chk_HA_8L.Enabled = (nos_lane >= 8);
            chk_HA_9L.Enabled = (nos_lane >= 9);
            chk_HA_10L.Enabled = (nos_lane >= 10);


            chk_HB_1L.Enabled = (nos_lane >= 1);
            chk_HB_2L.Enabled = (nos_lane >= 2);
            chk_HB_3L.Enabled = (nos_lane >= 3);
            chk_HB_4L.Enabled = (nos_lane >= 4);
            chk_HB_5L.Enabled = (nos_lane >= 5);
            chk_HB_6L.Enabled = (nos_lane >= 6);
            chk_HB_7L.Enabled = (nos_lane >= 7);
            chk_HB_8L.Enabled = (nos_lane >= 8);
            chk_HB_9L.Enabled = (nos_lane >= 9);
            chk_HB_10L.Enabled = (nos_lane >= 10);


            grb_ha.Enabled = (rbtn_HA.Checked || rbtn_HA_HB.Checked || chk_HA.Checked);
            grb_hb.Enabled = (rbtn_HB.Checked || rbtn_HA_HB.Checked);

            if (rbtn_HA.Checked)
            {
                chk_HA_1L.Checked = chk_HA_1L.Enabled;
                chk_HA_2L.Checked = chk_HA_2L.Enabled;
                chk_HA_3L.Checked = chk_HA_3L.Enabled;
                chk_HA_4L.Checked = chk_HA_4L.Enabled;
                chk_HA_5L.Checked = chk_HA_5L.Enabled;
                chk_HA_6L.Checked = chk_HA_6L.Enabled;
                chk_HA_7L.Checked = chk_HA_7L.Enabled;
                chk_HA_8L.Checked = chk_HA_8L.Enabled;
                chk_HA_9L.Checked = chk_HA_9L.Enabled;
                chk_HA_10L.Checked = chk_HA_10L.Enabled;
            }

            if (rbtn_HB.Checked || rbtn_Rail_Load.Checked)
            {
                chk_HB_1L.Checked = chk_HB_1L.Enabled;
                chk_HB_2L.Checked = chk_HB_2L.Enabled;
                chk_HB_3L.Checked = chk_HB_3L.Enabled;
                chk_HB_4L.Checked = chk_HB_4L.Enabled;
                chk_HB_5L.Checked = chk_HB_5L.Enabled;
                chk_HB_6L.Checked = chk_HB_6L.Enabled;
                chk_HB_7L.Checked = chk_HB_7L.Enabled;
                chk_HB_8L.Checked = chk_HB_8L.Enabled;
                chk_HB_9L.Checked = chk_HB_9L.Enabled;
                chk_HB_10L.Checked = chk_HB_10L.Enabled;
            }

            //if(rbtn_HA_HB.Checked)
            //{

            //    chk_HB_1L.Checked = !chk_HA_1L.Checked;
            //    chk_HB_2L.Checked = !chk_HA_2L.Checked;
            //    chk_HB_3L.Checked = !chk_HA_3L.Checked;
            //    chk_HB_4L.Checked = !chk_HA_4L.Checked;
            //    chk_HB_5L.Checked = !chk_HA_5L.Checked;
            //    chk_HB_6L.Checked = !chk_HA_6L.Checked;
            //    chk_HB_7L.Checked = !chk_HA_7L.Checked;
            //    chk_HB_8L.Checked = !chk_HA_8L.Checked;
            //    chk_HB_9L.Checked = !chk_HA_9L.Checked;
            //    chk_HB_10L.Checked = !chk_HA_10L.Checked;
            //}


        }

        public void Default_British_HB_LoadData(DataGridView dgv_live_load)
        {

            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();

            string load = cmb_HB.Text;
            int i = 0;
            list.Clear();
            int typ_no = 1;

            double ll = MyList.StringToDouble(load.Replace("HB_", ""), 1.0);


            for (i = 6; i <= 26; i += 5)
            {
                list.Add(string.Format("TYPE {0}, {1}_{2}", typ_no++, load, i));
                list.Add(string.Format("AXLE LOAD IN TONS ,{0:f1}, {0:f1}, {0:f1}, {0:f1}", ll));
                list.Add(string.Format("AXLE SPACING IN METRES, 1.8,{0:f1},1.8", i));
                list.Add(string.Format("AXLE WIDTH IN METRES, 1.0"));
                list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
                list.Add(string.Format(""));
            }


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

                //for (int j = 0; j < mlist.Count; j++)
                //{
                //    dgv_live_load[j, i].Value = mlist[j];
                //}

                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Default_British_HB_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            if (chk_HB_1L.Checked) lanes.Add(1);
            if (chk_HB_2L.Checked) lanes.Add(2);
            if (chk_HB_3L.Checked) lanes.Add(3);
            if (chk_HB_4L.Checked) lanes.Add(4);
            if (chk_HB_5L.Checked) lanes.Add(5);
            if (chk_HB_6L.Checked) lanes.Add(6);
            if (chk_HB_7L.Checked) lanes.Add(7);
            if (chk_HB_8L.Checked) lanes.Add(8);
            if (chk_HB_9L.Checked) lanes.Add(9);
            if (chk_HB_10L.Checked) lanes.Add(10);


            #region Long Girder
            list.Clear();


            double d, lane_width, impf, lf;
            int nos_lane;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);



            string load = "LOAD 1";
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 5; ld++)
            {
                load = "LOAD " + ld;
                x = "X";
                z = "Z";

                for (i = 0; i < lanes.Count; i++)
                {
                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25);

                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0 + 1.0);
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }
            #endregion Load 1

            //list.Add(string.Format("LOAD 1,TYPE 1"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 2,TYPE 2"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 3,TYPE 3"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,5.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 4,TYPE 4"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 5,TYPE 5"));
            //list.Add(string.Format("X,0,0"));
            //list.Add(string.Format("Z,1.5,4.5"));
            //list.Add(string.Format(""));
            #endregion




            dgv_live_load.Columns.Clear();

            for (i = 0; i <= lanes.Count * 2; i++)
            {
                if (i == 0)
                {
                    dgv_live_load.Columns.Add("col_brts" + i, "Load Data");
                    dgv_live_load.Columns[i].Width = 70;
                    dgv_live_load.Columns[i].ReadOnly = true;
                }
                else
                {
                    dgv_live_load.Columns.Add("col_brts" + i, i.ToString());
                    dgv_live_load.Columns[i].Width = 50;
                }
            }


            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }


        public void Default_British_Rail_LoadData(DataGridView dgv_live_load)
        {

            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();

            string load = cmb_HB.Text;
            int i = 0;
            list.Clear();

            list.Add(string.Format("TYPE 1, BS_RAIL_1"));
            list.Add(string.Format("AXLE LOAD IN TONS , 24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52"));
            list.Add(string.Format("AXLE SPACING IN METRES,2.05,1.95,5.56,1.95,2.05,5.94,2.05,1.95,5.56,1.95,2.05", i));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.676"));
            list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
            list.Add(string.Format(""));


            list.Add(string.Format("TYPE 2, BS_RAIL_2"));
            list.Add(string.Format("AXLE LOAD IN TONS , 22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06"));
            list.Add(string.Format("AXLE SPACING IN METRES, 1.65,1.65,6.4,1.65,1.65,3.0,1.65,1.65,6.4,1.65,1.65"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.676"));
            list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
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
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Default_British_Rail_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            //if (chk_HB_1L.Checked) lanes.Add(1);
            //if (chk_HB_2L.Checked) lanes.Add(2);
            //if (chk_HB_3L.Checked) lanes.Add(3);
            //if (chk_HB_4L.Checked) lanes.Add(4);
            //if (chk_HB_5L.Checked) lanes.Add(5);
            //if (chk_HB_6L.Checked) lanes.Add(6);
            //if (chk_HB_7L.Checked) lanes.Add(7);
            //if (chk_HB_8L.Checked) lanes.Add(8);
            //if (chk_HB_9L.Checked) lanes.Add(9);
            //if (chk_HB_10L.Checked) lanes.Add(10);


            #region Long Girder
            list.Clear();


            double d, lane_width, impf, lf;
            int nos_lane;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);

            for (i = 1; i <= nos_lane; i++)
            {
                lanes.Add(i);
            }

            string load = "LOAD 1";
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 2; ld++)
            {
                load = "LOAD " + ld;
                x = "X";
                z = "Z";

                for (i = 0; i < lanes.Count; i++)
                {
                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0);

                    //load += ",TYPE " + ld;
                    //x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    //z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0 + 1.0);
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }
            #endregion Load 1

            #endregion




            dgv_live_load.Columns.Clear();

            for (i = 0; i <= lanes.Count; i++)
            {
                if (i == 0)
                {
                    dgv_live_load.Columns.Add("col_brts" + i, "Load Data");
                    dgv_live_load.Columns[i].Width = 70;
                    dgv_live_load.Columns[i].ReadOnly = true;
                }
                else
                {
                    dgv_live_load.Columns.Add("col_brts" + i, i.ToString());
                    dgv_live_load.Columns[i].Width = 50;
                }
            }


            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }


        private void cmb_HB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRead) return;

            Default_British_HB_LoadData(dgv_long_british_loads);
            Default_British_HB_Type_LoadData(dgv_british_loads);
        }

        private void chk_HA_1L_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            chk_HB_1L.Checked = (!chk_HA_1L.Checked && chk_HB_1L.Enabled);
            chk_HB_2L.Checked = !chk_HA_2L.Checked && chk_HB_2L.Enabled;
            chk_HB_3L.Checked = !chk_HA_3L.Checked && chk_HB_3L.Enabled;
            chk_HB_4L.Checked = !chk_HA_4L.Checked && chk_HB_4L.Enabled;
            chk_HB_5L.Checked = !chk_HA_5L.Checked && chk_HB_5L.Enabled;
            chk_HB_6L.Checked = !chk_HA_6L.Checked && chk_HB_6L.Enabled;
            chk_HB_7L.Checked = !chk_HA_7L.Checked && chk_HB_7L.Enabled;
            chk_HB_8L.Checked = !chk_HA_8L.Checked && chk_HB_8L.Enabled;
            chk_HB_9L.Checked = !chk_HA_9L.Checked && chk_HB_9L.Enabled;
            chk_HB_10L.Checked = !chk_HA_10L.Checked && chk_HB_10L.Enabled;
            Default_British_HB_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;

        }

        private void chk_HB_1L_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            chk_HA_1L.Checked = !chk_HB_1L.Checked && chk_HB_1L.Enabled;
            chk_HA_2L.Checked = !chk_HB_2L.Checked && chk_HB_2L.Enabled;
            chk_HA_3L.Checked = !chk_HB_3L.Checked && chk_HB_3L.Enabled;
            chk_HA_4L.Checked = !chk_HB_4L.Checked && chk_HB_4L.Enabled;
            chk_HA_5L.Checked = !chk_HB_5L.Checked && chk_HB_5L.Enabled;
            chk_HA_6L.Checked = !chk_HB_6L.Checked && chk_HB_6L.Enabled;
            chk_HA_7L.Checked = !chk_HB_7L.Checked && chk_HB_7L.Enabled;
            chk_HA_8L.Checked = !chk_HB_8L.Checked && chk_HB_8L.Enabled;
            chk_HA_9L.Checked = !chk_HB_9L.Checked && chk_HB_9L.Enabled;
            chk_HA_10L.Checked = !chk_HB_10L.Checked && chk_HB_10L.Enabled;

            Default_British_HB_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;
        }

        private void chk_HA_3L_EnabledChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            CheckBox chk = sender as CheckBox;
            if (!chk.Enabled) chk.Checked = false;
        }

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();

        public void LONG_GIRDER_BRITISH_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();

            if (rbtn_HA.Checked) return;

            List<string> long_ll_impact = new List<string>();
            //long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format(""));


            bool flag = false;
            for (i = 0; i < dgv_long_british_loads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_british_loads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        long_ll_impact.Add(kStr);
                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format("TYPE 6 40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            List<string> load_list_1 = new List<string>();
            List<string> load_list_2 = new List<string>();
            List<string> load_list_3 = new List<string>();
            List<string> load_list_4 = new List<string>();
            List<string> load_list_5 = new List<string>();
            List<string> load_list_6 = new List<string>();
            List<string> load_total_7 = new List<string>();



            int fl = 0;
            double xinc = MyList.StringToDouble(txt_ll_british_incr.Text, 0.5);
            //double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_british_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_british_loads[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_ll_british_lgen.Text);
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                    if (count == 1)
                    {
                        load_list_1.Clear();
                        load_list_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        load_list_2.Clear();
                        load_list_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        load_list_3.Clear();
                        load_list_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        load_list_4.Clear();
                        load_list_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        load_list_5.Clear();
                        load_list_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {

                        load_list_6.Clear();
                        load_list_6.AddRange(list.ToArray());

                    }
                    else if (count == 7)
                    {
                        load_total_7.Clear();
                        load_total_7.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_british_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

            //Long_Girder_Analysis.LoadList_1 = 
        }

        public List<int> HA_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HA_1L.Checked) lanes.Add(1);
                if (chk_HA_2L.Checked) lanes.Add(2);
                if (chk_HA_3L.Checked) lanes.Add(3);
                if (chk_HA_4L.Checked) lanes.Add(4);
                if (chk_HA_5L.Checked) lanes.Add(5);
                if (chk_HA_6L.Checked) lanes.Add(6);
                if (chk_HA_7L.Checked) lanes.Add(7);
                if (chk_HA_8L.Checked) lanes.Add(8);
                if (chk_HA_9L.Checked) lanes.Add(9);
                if (chk_HA_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }
        public List<int> HB_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HB_1L.Checked) lanes.Add(1);
                if (chk_HB_2L.Checked) lanes.Add(2);
                if (chk_HB_3L.Checked) lanes.Add(3);
                if (chk_HB_4L.Checked) lanes.Add(4);
                if (chk_HB_5L.Checked) lanes.Add(5);
                if (chk_HB_6L.Checked) lanes.Add(6);
                if (chk_HB_7L.Checked) lanes.Add(7);
                if (chk_HB_8L.Checked) lanes.Add(8);
                if (chk_HB_9L.Checked) lanes.Add(9);
                if (chk_HB_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }



        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Set File Name

            string file_name = "";
            if (Bridge_Analysis != null)
            {
                Bridge_Analysis.Input_File = Get_Input_File(AnalysisType);


                if (AnalysisType == eAnalysis.Normal)
                {
                    if (cmb_long_open_file.SelectedIndex == 0)
                        file_name = Input_File_DL;
                    else
                        file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);

                    btn_view_data.Enabled = File.Exists(file_name);
                    btn_View_Post_Process.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
                    btn_view_structure.Enabled = File.Exists(file_name);
                    btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));

                }
                else
                {
                    if (ucStage.cmb_long_open_file.SelectedIndex == 0)
                        file_name = Input_File_DL;
                    else
                        file_name = Get_Live_Load_Analysis_Input_File(ucStage.cmb_long_open_file.SelectedIndex);

                    ucStage.btn_view_data.Enabled = File.Exists(file_name);
                    ucStage.btn_View_Post_Process.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
                    ucStage.btn_view_structure.Enabled = File.Exists(file_name);
                    ucStage.btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));

                }
                //if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
                //{

                //    if (cmb_long_open_file.SelectedIndex == 0)
                //        file_name = Input_File_DL;
                //    else if (cmb_long_open_file.SelectedIndex == 1 && iApp.DesignStandard == eDesignStandard.IndianStandard)
                //        file_name = Input_File_LL;
                //    else
                //        file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                //}
                //else
                //{
                //    file_name = Result_Report_LL;
                //}
            }

            #endregion Set File Name

        }

        #endregion British Standard Loading

        #endregion Chiranjit [2014 09 10]

        public string Get_Live_Load_Analysis_Input_File(int analysis_no)
        {
            string working_folder = Path.GetDirectoryName(Get_Input_File(AnalysisType));

            if (Directory.Exists(working_folder))
            {
                string pd = Path.Combine(working_folder, "LL ANALYSIS LOAD " + analysis_no);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_LOAD_" + analysis_no + "_INPUT_FILE.txt");
            }
            return "";
        }

        public string Get_Live_Load_Analysis_Input_File(int analysis_no, int stage)
        {
            string working_folder = Path.GetDirectoryName(Get_Input_File((eAnalysis)stage));

            if (Directory.Exists(working_folder))
            {
                string pd = Path.Combine(working_folder, "LL ANALYSIS LOAD " + analysis_no);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_LOAD_" + analysis_no + "_INPUT_FILE.txt");
            }
            return "";
        }

        void Ana_Write_Long_Girder_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
        {
            if (!File.Exists(file_name)) return;

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

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

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

            string s = " DL";
            bool fl = false;
            if (add_DeadLoad)
            {

                if (add_LiveLoad)
                {
                    //foreach (var item in txt_Ana_LL_member_load.Lines)
                    //{

                    //    if (item.ToUpper().StartsWith("LOAD"))
                    //    {
                    //        if (fl == false)
                    //        {
                    //            fl = true;
                    //            load_lst.Add(item);
                    //        }
                    //        else
                    //            load_lst.Add("*" + item);
                    //    }
                    //    else
                    //    {
                    //        if (!load_lst.Contains(item))
                    //            load_lst.Add(item);
                    //        else
                    //            load_lst.Add("*" + item);
                    //    }
                    //}
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (HA_Lanes.Count > 0)
                        {
                            load_lst.Add("*HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("MEMBER LOAD");
                        }
                    }

                }
                //else
                //    //load_lst.AddRange(txt_Ana_LL_member_load.Lines);
            }
            else
            {

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    //if (chk_self_indian.Checked)
                    //    load_lst.Add("SELFWEIGHT Y -1");
                }
                else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    if (HA_Lanes.Count > 0)
                    {
                        load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                        load_lst.Add("MEMBER LOAD");
                    }
                }
            }

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                if ((rbtn_HB.Checked || rbtn_HA_HB.Checked || rbtn_Rail_Load.Checked)
                    || iApp.DesignStandard == eDesignStandard.IndianStandard)
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());

        }

        private void Create_Data_LL_British(string file_name)
        {
            //Input_File_LL
            Deck_Analysis_LL.Input_File = file_name;
            LONG_GIRDER_BRITISH_LL_TXT();

            //Bridge_Analysis.HA_Lanes = HA_Lanes;

            Deck_Analysis_LL.CreateData();

            //Bridge_Analysis.HA_Loading_Members = "191 TO 202";

            //if (rbtn_HA.Checked)
            //{
            //    //Deck_Analysis_LL.WriteData_LiveLoad(Deck_Analysis_LL.Input_File, PSC_SECIONS);
            //    //Ana_Write_Long_Girder_Load_Data(Deck_Analysis_LL.Input_File, true, false, 1);
            //}
            int i = 0;
            string ll_file = "";
            for (i = 0; i < all_loads.Count; i++)
            {
                ll_file = Get_Live_Load_Analysis_Input_File(i + 1);
                Deck_Analysis_LL.WriteData_LiveLoad(ll_file, PSC_SECIONS, long_ll);
                Ana_Write_Long_Girder_Load_Data(ll_file, true, false, (i + 1));
            }

            Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            Button_Enable_Disable();



        }
        private void btn_view_data_Click(object sender, EventArgs e)
        {
            string file_name = "";
            string ll_txt = "";

            Button btn = sender as Button;

            #region Set File Name

            //file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);

            if (cmb_long_open_file.SelectedIndex == 0)
                file_name = Input_File_DL;
            else
                file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
            //file_name = Result_Report_LL;


            #endregion Set File Name

            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name)
            {
                if (cmb_long_open_file.SelectedIndex == cmb_long_open_file.Items.Count - 1)
                    iApp.View_Result(file_name);
                else
                    iApp.View_Input_File(file_name);
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                if (File.Exists(file_name))
                    iApp.OpenWork(file_name, false);
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_View_Post_Process.Name)
            {
                //file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.View_PostProcess(file_name);
            }
        }
        #region Chiranjit [2015 08 31]
        BridgeAnalysisDesign.PSC_BoxGirder.frm_Box_Girder_Diagra_ f_diagram;
        private void btn_open_diagram_Click(object sender, EventArgs e)
        {
            try
            {
                f_diagram.Close();
            }
            catch (Exception ex) { }


            f_diagram = new BridgeAnalysisDesign.PSC_BoxGirder.frm_Box_Girder_Diagra_();
            f_diagram.Owner = this;
            f_diagram.Show();
        }
        #endregion Chiranjit [2015 08 31]

        private void btn_psc_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_psc_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;

                    txt_project_name.Text = Path.GetFileName(user_path);


                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                    }


                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);

                    //Write_All_Data();

                    #endregion Save As

                }
            }
            else if (btn.Name == btn_psc_new_design.Name)
            {
                IsCreate_Data = true;
                Create_Project();
                Show_Section_Result();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }
        #region Chiranjit [2016 09 07
        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
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
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.PSC_Box_Girder_Bridge_WS;
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

        private void uC_RCC_Abut1_Abut_Counterfort_LS1_dead_load_CheckedChanged(object sender, EventArgs e)
        {

            if (uC_RCC_Abut1.uC_Abut_Counterfort_LS1.rbtn_dead_load.Checked)
            {
                //uC_RCC_Abut1.uC_Abut_Counterfort_LS1.Reaction_A = txt_dead_vert_reac_kN.Text;
                //uC_RCC_Abut1.uC_Abut_Counterfort_LS1.Reaction_B = txt_dead_vert_reac_kN.Text;
            }
        }

        private void btn_bs_view_moving_load_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;
                Show_Section_Result();
                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }


                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }
                Write_All_Data();
                Analysis_Initialize_InputData();
                Create_Data_LL_Indian(Input_File);

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    Create_Data_LL_Indian(Input_File_LL);
                else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    Deck_Analysis_LL.Input_File = Input_File_LL;
                    LONG_GIRDER_BRITISH_LL_TXT();

                    //Bridge_Analysis.HA_Lanes = HA_Lanes;

                    //Deck_Analysis_LL.CreateData_British();

                    //Bridge_Analysis.HA_Loading_Members = "191 TO 202";

                    int i = 0;
                    string ll_file = "";
                    //for (i = 0; i < all_loads.Count; i++)
                    //{
                    ll_file = Get_Live_Load_Analysis_Input_File(cmb_bs_view_moving_load.SelectedIndex + 1);
                    Deck_Analysis_LL.WriteData_LiveLoad(ll_file, PSC_SECIONS, long_ll);
                    Ana_Write_Long_Girder_Load_Data(ll_file, true, false, (cmb_bs_view_moving_load.SelectedIndex + 1));
                    //}

                    //iApp.View_MovingLoad(ll_file);
                    iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_bs_vehicle_gap.Text));


                }
                Button_Enable_Disable();
            }
            catch (Exception ex) { }
        }

        private void chk_crash_barrier_CheckedChanged(object sender, EventArgs e)
        {

            Control rbtn = sender as Control;
            if (rbtn.Name == chk_fp_left.Name)
            {
                if (chk_footpath.Checked)
                {
                    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                    {
                        chk_footpath.Checked = false;
                    }

                    if (!chk_fp_left.Checked)
                    {
                        txt_Ana_Hf_LHS.Enabled = false;
                        txt_Ana_Wf_LHS.Enabled = false;
                        txt_Ana_Hf_LHS.Text = "0.000";
                        txt_Ana_Wf_LHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hf_LHS.Enabled = true;
                        txt_Ana_Wf_LHS.Enabled = true;
                        txt_Ana_Hf_LHS.Text = "1.000";
                        txt_Ana_Wf_LHS.Text = "0.250";
                    }

                }
            }
            else if (rbtn.Name == chk_fp_right.Name)
            {
                if (chk_footpath.Checked)
                {
                    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                    {
                        chk_footpath.Checked = false;
                        //chk_fp_left.Checked = true;
                    }


                    if (!chk_fp_right.Checked)
                    {
                        txt_Ana_Hf_RHS.Enabled = false;
                        txt_Ana_Wf_RHS.Enabled = false;
                        txt_Ana_Hf_RHS.Text = "0.000";
                        txt_Ana_Wf_RHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hf_RHS.Enabled = true;
                        txt_Ana_Wf_RHS.Enabled = true;
                        txt_Ana_Hf_RHS.Text = "1.000";
                        txt_Ana_Wf_RHS.Text = "0.250";
                    }
                }
            }

            if (rbtn.Name == chk_cb_left.Name)
            {
                if (chk_crash_barrier.Checked)
                {
                    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
                    {
                        //chk_cb_right.Checked = true;
                        chk_crash_barrier.Checked = false;
                    }

                    if (!chk_cb_left.Checked)
                    {
                        txt_Ana_Hc_LHS.Enabled = false;
                        txt_Ana_Wc_LHS.Enabled = false;
                        txt_Ana_Hc_LHS.Text = "0.000";
                        txt_Ana_Wc_LHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hc_LHS.Enabled = true;
                        txt_Ana_Wc_LHS.Enabled = true;
                        txt_Ana_Hc_LHS.Text = "1.200";
                        txt_Ana_Wc_LHS.Text = "0.500";
                    }
                }
            }
            else if (rbtn.Name == chk_cb_right.Name)
            {
                if (chk_crash_barrier.Checked)
                {
                    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
                    {
                        //chk_cb_left.Checked = true;
                        chk_crash_barrier.Checked = false;
                    }


                    if (!chk_cb_right.Checked)
                    {
                        txt_Ana_Hc_RHS.Enabled = false;
                        txt_Ana_Wc_RHS.Enabled = false;
                        txt_Ana_Hc_RHS.Text = "0.000";
                        txt_Ana_Wc_RHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hc_RHS.Enabled = true;
                        txt_Ana_Wc_RHS.Enabled = true;
                        txt_Ana_Hc_RHS.Text = "1.200";
                        txt_Ana_Wc_RHS.Text = "0.500";
                    }
                }
            }
            else if (rbtn.Name == chk_crash_barrier.Name)
            {
                chk_cb_left.Checked = chk_crash_barrier.Checked;
                chk_cb_right.Checked = chk_crash_barrier.Checked;
            }
            else if (rbtn.Name == chk_footpath.Name)
            {
                chk_fp_left.Checked = chk_footpath.Checked;
                chk_fp_right.Checked = chk_footpath.Checked;
            }

            //if (rbtn.Name == chk_crash_barrier.Name)
            //{
            //    if (chk_crash_barrier.Checked)
            //        chk_footpath.Checked = false;
            //}
            //else if (rbtn.Name == chk_footpath.Name)
            //{
            //    if (chk_footpath.Checked)
            //        chk_crash_barrier.Checked = false;
            //}

            chk_cb_left.Enabled = chk_crash_barrier.Checked;
            chk_cb_right.Enabled = chk_crash_barrier.Checked;

            chk_fp_left.Enabled = chk_footpath.Checked;
            chk_fp_right.Enabled = chk_footpath.Checked;

            if (rbtn.Name == chk_crash_barrier.Name)
            {
                grb_ana_crash_barrier.Enabled = chk_crash_barrier.Checked;
                if (!chk_crash_barrier.Checked)
                {
                    txt_Ana_Hc_LHS.Text = "0.000";
                    txt_Ana_Wc_LHS.Text = "0.000";
                    txt_Ana_Hc_RHS.Text = "0.000";
                    txt_Ana_Wc_RHS.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hc_LHS.Text = "1.200";
                    txt_Ana_Wc_LHS.Text = "0.500";
                    txt_Ana_Hc_RHS.Text = "1.200";
                    txt_Ana_Wc_RHS.Text = "0.500";
                }
            }
            else if (rbtn.Name == chk_footpath.Name)
            {
                grb_ana_footpath.Enabled = chk_footpath.Checked;
                if (!chk_footpath.Checked)
                {
                    txt_Ana_Wf_LHS.Text = "0.000";
                    txt_Ana_Hf_LHS.Text = "0.000";
                    txt_Ana_Wf_RHS.Text = "0.000";
                    txt_Ana_Hf_RHS.Text = "0.000";

                    txt_Ana_Wk.Text = "0.000";
                    txt_Ana_Wr.Text = "0.000";
                }
                else
                {
                    txt_Ana_Wf_LHS.Text = "0.250";
                    txt_Ana_Hf_LHS.Text = "1.000";
                    txt_Ana_Wf_RHS.Text = "0.250";
                    txt_Ana_Hf_RHS.Text = "1.000";



                    txt_Ana_Wk.Text = "0.500";
                    txt_Ana_Wr.Text = "0.100";
                }
            }


            if (chk_crash_barrier.Checked && chk_footpath.Checked)
            {

                if (chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_4_;

                if (chk_cb_left.Checked && !chk_cb_right.Checked && chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_5_;

                if (!chk_cb_left.Checked && chk_cb_right.Checked && !chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_6_;

                if (chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_7_;

                if (chk_cb_left.Checked && chk_cb_right.Checked && !chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_8_;

                if (!chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_9_;

                if (chk_cb_left.Checked && !chk_cb_right.Checked && chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_10_;


                if (chk_cb_left.Checked && !chk_cb_right.Checked && !chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_11_;

                if (!chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_12_;
            }
            else if (chk_crash_barrier.Checked)
            {
                if (chk_cb_left.Checked && chk_cb_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_1_;
                if (chk_cb_left.Checked && !chk_cb_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_2_;
                if (!chk_cb_left.Checked && chk_cb_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_3_;

            }
            else if (chk_footpath.Checked)
            {
                if (chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_2__Both_Footpaths;
                else if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_3__LHS_Footpath;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_4__LHS_Footpath;
            }
            else
                pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_1__No_Footpath_;


            Refresh();


            Text_Changed();
        }

        private void uC_BoxGirder_Stage1_OnEmodTextChanged(object sender, EventArgs e)
        {
            if (ucStage != null)
            {
                Emod_Changed(ucStage);
            }
        }
        private void Emod_Changed(UC_BoxGirder_Stage uc)
        {
            double pcnt = MyList.StringToDouble(uc.txt_conc_pcnt) / 100;
            uc.txt_emod_conc.Text = (MyList.StringToDouble(txt_emod_conc) * pcnt).ToString("f0");
        }

        private void cmb_design_stage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UC_BoxGirder_Results uc = uC_Res_Normal;
            UC_BoxGirder_Results ur = uC_BoxGirder_Results1;

            if (cmb_design_stage.SelectedIndex == 1) uc = uC_BoxGirder_Stage1.uC_Res;
            else if (cmb_design_stage.SelectedIndex == 2) uc = uC_BoxGirder_Stage2.uC_Res;
            else if (cmb_design_stage.SelectedIndex == 3) uc = uC_BoxGirder_Stage3.uC_Res;
            else if (cmb_design_stage.SelectedIndex == 4) uc = uC_BoxGirder_Stage4.uC_Res;
            else if (cmb_design_stage.SelectedIndex == 5) uc = uC_BoxGirder_Stage5.uC_Res;

            Save_FormRecord.Copy_All_Control_Data(uc, ur);
        }


        public void LONG_GIRDER_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();


            DataGridView dgv = dgv_long_liveloads;



            bool flag = false;
            for (i = 0; i < dgv.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv.ColumnCount; c++)
                {
                    kStr = dgv[c, i].Value.ToString();


                    if (flag)
                    {
                        long_ll_impact.Add(kStr);

                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            List<string> files = new List<string>();


            int fl = 0;
            double xinc = MyList.StringToDouble(txt_IRC_XINCR.Text, 0.0);
            double imp_fact = 1.179;


            dgv = dgv_long_loads;

            int count = 0;
            for (i = 0; i < dgv.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (!list.Contains(txt))
                            list.Add(txt);
                    }

                    list.Add("LOAD GENERATION " + txt_IRC_LL_load_gen.Text);


                    string fn = "";
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} {2:f3} {3:f3} XINC {4}", def_load[j], def_x[j], 0.0, def_z[j], xinc);
                        list.Add(txt);

                        fn = fn + " " + def_load[j];
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv.ColumnCount; c++)
                {
                    kStr = dgv[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
            }
            fl = 3;
        }

        void Show_and_Save_Data_Load_1_2_3()
        {
            analysis_rep = MyList.Get_Analysis_Report_File(Input_File_DL);
            if (!File.Exists(analysis_rep)) return;


            Left_support = Bridge_Analysis.support_left_joints;
            Right_support = Bridge_Analysis.support_right_joints;
            UC_SupportReactions uCSR_DL = uc_Res.uC_SR_DL;
            UC_SupportReactions uCSR_SIDL = uc_Res.uC_SR_SIDL;
            UC_SupportReactions uCSR_LL = uc_Res.uC_SR_LL;
            UC_MaxReactions uCMaxSR = uc_Res.uC_MaxSR;


            if (AnalysisType != eAnalysis.Normal)
            {
                uCSR_DL = ucStage.uC_Res.uC_SR_DL;
                uCSR_LL = ucStage.uC_Res.uC_SR_LL;
                uCMaxSR = ucStage.uC_Res.uC_MaxSR;
            }



            string format = "{0,27} {1,10:f3} {2,10:f3} {3,10:f3}";
            List<string> list_arr = new List<string>(File.ReadAllLines(analysis_rep));
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
            SupportReaction sr = null;

            //MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

            List<int> supp = MyList.Get_Array_Intiger(Left_support);

            double tot_left_vert_reac = 0.0;
            double tot_right_vert_reac = 0.0;

            double tot_left_Mx = 0.0;
            double tot_left_Mz = 0.0;

            double tot_right_Mx = 0.0;
            double tot_right_Mz = 0.0;


            #region DL
            DataGridView dgv_left = uCSR_DL.dgv_left_des_frc;
            DataGridView dgv_right = uCSR_DL.dgv_right_des_frc;




            Deck_Analysis_DL.Bridge_Analysis = All_Analysis[0];




            dgv_left.Rows.Clear();
            dgv_right.Rows.Clear();
            list_arr.Add("LEFT END");
            list_arr.Add("--------");


            double _vert_load, _mx, _mz;

            _vert_load = _mx = _mz = 0.0;
            int _jnt_no = 0;
            for (int i = 0; i < supp.Count; i++)
            {

                _jnt_no = supp[i];

                var lst = new List<int>();

                lst.Add(_jnt_no);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_jnt_no, 1);
                //var shr = Long_Girder_Analysis.DeadLoad_Analysis.GetJoint_R2_Shear(lst, 4);
                var mx = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_jnt_no, 1);
                var mz = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_jnt_no, 1);



                //Add Load 1 + Load 2 + Load 3
                //_vert_load += shr.Force;
                //_mx += mx.Force;
                //_mz += mz.Force;


                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;




                //dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);
                dgv_left.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            list_arr.Add("");



            uCSR_DL.txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            uCSR_DL.txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            uCSR_DL.txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");
            list_arr.Add(string.Format(format, "TOTAL", tot_left_vert_reac, tot_left_Mx, tot_left_Mz));
            list_arr.Add("");


            supp = MyList.Get_Array_Intiger(Right_support);
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < supp.Count; i++)
            {
                _jnt_no = supp[i];

                var lst = new List<int>();

                lst.Add(_jnt_no);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_jnt_no, 1);
                //var shr = Long_Girder_Analysis.DeadLoad_Analysis.GetJoint_R2_Shear(lst, 4);
                var mx = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_jnt_no, 1);
                var mz = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_jnt_no, 1);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;

                //sr = support_reactions.Get_Data(_jnt_no);
                dgv_right.Rows.Add(_jnt_no, Math.Abs(_vert_load), _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load);
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));

            }
            list_arr.Add("");

            uCSR_DL.txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            uCSR_DL.txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            uCSR_DL.txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");












            //txt_both_ends_total.Text = (tot_left_vert_reac + tot_right_vert_reac).ToString("0.000");
            list_arr.Add("");
            //list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Ton");

            uCSR_DL.txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            uCSR_DL.txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");



            uCSR_DL.txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            uCSR_DL.txt_max_vert_reac_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + uCSR_DL.txt_final_vert_reac.Text + " Ton" + "    =  " + uCSR_DL.txt_final_vert_rec_kN.Text + " kN");

            //txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            uCSR_DL.txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            uCSR_DL.txt_final_Mx_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");




            uCSR_DL.txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            uCSR_DL.txt_max_Mx_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");




            list_arr.Add("        MAXIMUM  MX     = " + uCSR_DL.txt_final_Mx.Text + " Ton-M" + "  =  " + uCSR_DL.txt_final_Mx_kN.Text + " kN-m");
            //txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            uCSR_DL.txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            uCSR_DL.txt_final_Mz_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            //txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            uCSR_DL.txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            uCSR_DL.txt_max_Mz_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");









            list_arr.Add("        MAXIMUM  MZ     = " + uCSR_DL.txt_final_Mz.Text + " Ton-M" + "  =  " + uCSR_DL.txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");

            #endregion DL





            #region SIDL

            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;

            dgv_left = uCSR_SIDL.dgv_left_des_frc;
            dgv_right = uCSR_SIDL.dgv_right_des_frc;
            dgv_left.Rows.Clear();
            dgv_right.Rows.Clear();
            list_arr.Add("LEFT END");
            list_arr.Add("--------");



            _vert_load = _mx = _mz = 0.0;
            supp = MyList.Get_Array_Intiger(Left_support);
            for (int i = 0; i < supp.Count; i++)
            {

                _jnt_no = supp[i];

                var lst = new List<int>();

                lst.Add(_jnt_no);

                var shr = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_jnt_no, 2);
                var mx = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_jnt_no, 2);
                var mz = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_jnt_no, 2);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;


                dgv_left.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            list_arr.Add("");



            uCSR_SIDL.txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            uCSR_SIDL.txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            uCSR_SIDL.txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");
            list_arr.Add(string.Format(format, "TOTAL", tot_left_vert_reac, tot_left_Mx, tot_left_Mz));
            list_arr.Add("");


            tot_right_vert_reac = 0.0;
            tot_right_Mx = 0.0;
            tot_right_Mz = 0.0;


            supp = MyList.Get_Array_Intiger(Right_support);
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < supp.Count; i++)
            {
                _jnt_no = supp[i];

                var lst = new List<int>();

                lst.Add(_jnt_no);

                var shr = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_jnt_no, 2);
                var mx = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_jnt_no, 2);
                var mz = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_jnt_no, 2);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;

                dgv_right.Rows.Add(_jnt_no, Math.Abs(_vert_load), _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load);
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));

            }
            list_arr.Add("");

            uCSR_SIDL.txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            uCSR_SIDL.txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            uCSR_SIDL.txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");

            list_arr.Add("");

            uCSR_SIDL.txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            uCSR_SIDL.txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");



            uCSR_SIDL.txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            uCSR_SIDL.txt_max_vert_reac_kN.Text = (MyList.StringToDouble(uCSR_SIDL.txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + uCSR_SIDL.txt_final_vert_reac.Text + " Ton" + "    =  " + uCSR_SIDL.txt_final_vert_rec_kN.Text + " kN");


            uCSR_SIDL.txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            uCSR_SIDL.txt_final_Mx_kN.Text = (MyList.StringToDouble(uCSR_SIDL.txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");




            uCSR_SIDL.txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            uCSR_SIDL.txt_max_Mx_kN.Text = (MyList.StringToDouble(uCSR_SIDL.txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");




            list_arr.Add("        MAXIMUM  MX     = " + uCSR_SIDL.txt_final_Mx.Text + " Ton-M" + "  =  " + uCSR_SIDL.txt_final_Mx_kN.Text + " kN-m");

            uCSR_SIDL.txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            uCSR_SIDL.txt_final_Mz_kN.Text = (MyList.StringToDouble(uCSR_SIDL.txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            uCSR_SIDL.txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            uCSR_SIDL.txt_max_Mz_kN.Text = (MyList.StringToDouble(uCSR_SIDL.txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");






            list_arr.Add("        MAXIMUM  MZ     = " + uCSR_SIDL.txt_final_Mz.Text + " Ton-M" + "  =  " + uCSR_SIDL.txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");

            #endregion SIDL


            #region LL
            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;



            uCSR_LL.dgv_left_des_frc.Rows.Clear();
            uCSR_LL.dgv_right_des_frc.Rows.Clear();


            dgv_left = uCSR_LL.dgv_left_des_frc;
            dgv_right = uCSR_LL.dgv_right_des_frc;



            supp = MyList.Get_Array_Intiger(Left_support);
            for (int i = 0; i < supp.Count; i++)
            {

                _jnt_no = supp[i];

                var jnt = new List<int>();
                jnt.Add(_jnt_no);

                _vert_load = 0;
                _mx = 0;
                _mz = 0;



                for (int j = 1; j < all_loads.Count; j++)
                {

                    #region Get Node results from Dead load analysis
                    //Get Node results from Dead load analysis
                    //var mxf = Long_Girder_Analysis.All_LL_Analysis[j].GetJoint_R2_Shear(jnt);
                    var mxf = All_Analysis[j].GetJoint_ShearForce(jnt);
                    if (_vert_load < Math.Abs(mxf.Force))
                    {
                        _vert_load = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = All_Analysis[j].GetJoint_Torsion(jnt);
                    if (_mx < Math.Abs(mxf.Force))
                    {
                        _mx = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = All_Analysis[j].GetJoint_MomentForce(jnt);
                    if (_mz < Math.Abs(mxf.Force))
                    {
                        _mz = Math.Abs(mxf.Force);
                    }

                    #endregion Get Forces LL ANALYSIS

                }


                dgv_left.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            uCSR_LL.txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            uCSR_LL.txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            uCSR_LL.txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");


            tot_right_vert_reac = 0.0;
            tot_right_Mx = 0.0;
            tot_right_Mz = 0.0;

            supp = MyList.Get_Array_Intiger(Right_support);
            for (int i = 0; i < supp.Count; i++)
            {

                _jnt_no = supp[i];

                var jnt = new List<int>();
                jnt.Add(_jnt_no);

                _vert_load = 0;
                _mx = 0;
                _mz = 0;
                for (int j = 0; j < all_loads.Count; j++)
                {

                    #region Get Node results from Dead load analysis
                    //Get Node results from Dead load analysis
                    //var mxf = Long_Girder_Analysis.All_LL_Analysis[j].GetJoint_R2_Shear(jnt);
                    var mxf = All_Analysis[j].GetJoint_ShearForce(jnt);
                    if (_vert_load < Math.Abs(mxf.Force))
                    {
                        _vert_load = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = All_Analysis[j].GetJoint_Torsion(jnt);
                    if (_mx < Math.Abs(mxf.Force))
                    {
                        _mx = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = All_Analysis[j].GetJoint_MomentForce(jnt);
                    if (_mz < Math.Abs(mxf.Force))
                    {
                        _mz = Math.Abs(mxf.Force);
                    }

                    #endregion Get Forces LL ANALYSIS

                }


                dgv_right.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load); ;
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
            }

            uCSR_LL.txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            uCSR_LL.txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            uCSR_LL.txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");


            uCSR_LL.txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            uCSR_LL.txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            uCSR_LL.txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            uCSR_LL.txt_final_Mx_kN.Text = (MyList.StringToDouble(uCSR_LL.txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");



            uCSR_LL.txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            uCSR_LL.txt_final_Mz_kN.Text = (MyList.StringToDouble(uCSR_LL.txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");






            uCSR_LL.txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            uCSR_LL.txt_max_vert_reac_kN.Text = (MyList.StringToDouble(uCSR_LL.txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");

            uCSR_LL.txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            uCSR_LL.txt_max_Mx_kN.Text = (MyList.StringToDouble(uCSR_LL.txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");

            uCSR_LL.txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            uCSR_LL.txt_max_Mz_kN.Text = (MyList.StringToDouble(uCSR_LL.txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");





            #endregion LL

            tot_right_vert_reac = 0.0;
            tot_left_vert_reac = 0.0;

            tot_left_Mx = 0.0;
            tot_right_Mx = 0.0;

            tot_left_Mz = 0.0;
            tot_right_Mz = 0.0;

            #region DL



            uCMaxSR.dgv_mxf_left_des_frc.Rows.Clear();
            uCMaxSR.dgv_mxf_right_des_frc.Rows.Clear();



            dgv_left = uCMaxSR.dgv_mxf_left_des_frc;
            dgv_right = uCMaxSR.dgv_mxf_right_des_frc;

            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            double v1 = 0.0;
            double v2 = 0.0;
            double v3 = 0.0;
            for (int i = 0; i < uCSR_DL.dgv_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(uCSR_DL.dgv_left_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load))
                {
                    _vert_load = v1;
                }

                v2 = MyList.StringToDouble(uCSR_DL.dgv_left_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx))
                {
                    _mx = v2;
                }

                v3 = MyList.StringToDouble(uCSR_DL.dgv_left_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }

            uCMaxSR.dgv_mxf_left_des_frc.Rows.Add("DL", _vert_load, _mx, _mz);


            uCSR_DL.txt_left_max_total_vert_reac.Text = _vert_load.ToString();
            uCSR_DL.txt_left_max_total_Mx.Text = _mx.ToString();
            uCSR_DL.txt_left_max_total_Mz.Text = _mz.ToString();


            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;


            for (int i = 0; i < uCSR_DL.dgv_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(uCSR_DL.dgv_right_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(uCSR_DL.dgv_right_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(uCSR_DL.dgv_right_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            uCSR_DL.txt_right_max_total_vert_reac.Text = _vert_load.ToString();
            uCSR_DL.txt_right_max_total_Mx.Text = _mx.ToString();
            uCSR_DL.txt_right_max_total_Mz.Text = _mz.ToString();


            uCMaxSR.dgv_mxf_right_des_frc.Rows.Add("DL", _vert_load, _mx, _mz);

            #endregion DL


            #region DL


            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            for (int i = 0; i < uCSR_SIDL.dgv_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(uCSR_SIDL.dgv_left_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load))
                {
                    _vert_load = v1;
                }

                v2 = MyList.StringToDouble(uCSR_SIDL.dgv_left_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx))
                {
                    _mx = v2;
                }

                v3 = MyList.StringToDouble(uCSR_SIDL.dgv_left_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }

            uCMaxSR.dgv_mxf_left_des_frc.Rows.Add("SIDL", _vert_load, _mx, _mz);


            uCSR_SIDL.txt_left_max_total_vert_reac.Text = _vert_load.ToString();
            uCSR_SIDL.txt_left_max_total_Mx.Text = _mx.ToString();
            uCSR_SIDL.txt_left_max_total_Mz.Text = _mz.ToString();


            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;


            for (int i = 0; i < uCSR_SIDL.dgv_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(uCSR_SIDL.dgv_right_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(uCSR_SIDL.dgv_right_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(uCSR_SIDL.dgv_right_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            uCSR_SIDL.txt_right_max_total_vert_reac.Text = _vert_load.ToString();
            uCSR_SIDL.txt_right_max_total_Mx.Text = _mx.ToString();
            uCSR_SIDL.txt_right_max_total_Mz.Text = _mz.ToString();


            uCMaxSR.dgv_mxf_right_des_frc.Rows.Add("SIDL", _vert_load, _mx, _mz);

            #endregion DL




            #region LL

            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < uCSR_LL.dgv_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(uCSR_LL.dgv_left_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(uCSR_LL.dgv_left_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(uCSR_LL.dgv_left_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            uCSR_LL.txt_left_max_total_vert_reac.Text = _vert_load.ToString();
            uCSR_LL.txt_left_max_total_Mx.Text = _mx.ToString();
            uCSR_LL.txt_left_max_total_Mz.Text = _mz.ToString();


            uCMaxSR.dgv_mxf_left_des_frc.Rows.Add("LL", _vert_load, _mx, _mz);



            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < uCSR_LL.dgv_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(uCSR_LL.dgv_right_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(uCSR_LL.dgv_right_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(uCSR_LL.dgv_right_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            //uCSR_LL.txt_ll_right_max_total_vert_reac.Text = _vert_load.ToString();
            uCSR_LL.txt_right_max_total_Mx.Text = _mx.ToString();
            uCSR_LL.txt_right_max_total_Mz.Text = _mz.ToString();



            uCMaxSR.dgv_mxf_right_des_frc.Rows.Add("LL", _vert_load, _mx, _mz);


            #endregion LL


            #region Max Force

            dgv_left = uCMaxSR.dgv_mxf_left_des_frc;
            dgv_right = uCMaxSR.dgv_mxf_right_des_frc;

            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_left.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_left[1, i].Value.ToString(), 0.0);
                _vert_load += v1;

                v2 = MyList.StringToDouble(dgv_left[2, i].Value.ToString(), 0.0);
                _mx += v2;

                v3 = MyList.StringToDouble(dgv_left[3, i].Value.ToString(), 0.0);
                _mz += v3;

            }



            tot_left_vert_reac = _vert_load;
            tot_left_Mx = _mx;
            tot_left_Mz = _mz;


            uCMaxSR.txt_mxf_left_total_vert_reac.Text = _vert_load.ToString();
            uCMaxSR.txt_mxf_left_total_Mx.Text = _mx.ToString();
            uCMaxSR.txt_mxf_left_total_Mz.Text = _mz.ToString();



            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_right.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_right[1, i].Value.ToString(), 0.0);
                _vert_load += v1;

                v2 = MyList.StringToDouble(dgv_right[2, i].Value.ToString(), 0.0);
                _mx += v2;

                v3 = MyList.StringToDouble(dgv_right[3, i].Value.ToString(), 0.0);
                _mz += v3;

            }


            tot_right_vert_reac = _vert_load;
            tot_right_Mx = _mx;
            tot_right_Mz = _mz;

            uCMaxSR.txt_mxf_right_total_vert_reac.Text = _vert_load.ToString();
            uCMaxSR.txt_mxf_right_total_Mx.Text = _mx.ToString();
            uCMaxSR.txt_mxf_right_total_Mz.Text = _mz.ToString();




            uCMaxSR.txt_mxf_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            uCMaxSR.txt_mxf_max_vert_reac_kN.Text = (MyList.StringToDouble(uCMaxSR.txt_mxf_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");

            uCMaxSR.txt_mxf_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            uCMaxSR.txt_mxf_max_Mx_kN.Text = (MyList.StringToDouble(uCMaxSR.txt_mxf_max_Mx.Text, 0.0) * 10.0).ToString("f3");

            uCMaxSR.txt_mxf_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            uCMaxSR.txt_mxf_max_Mz_kN.Text = (MyList.StringToDouble(uCMaxSR.txt_mxf_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]


            uCMaxSR.txt_brg_max_VR_Ton.Text = uCMaxSR.txt_mxf_max_vert_reac.Text;
            uCMaxSR.txt_brg_max_VR_kN.Text = uCMaxSR.txt_mxf_max_vert_reac_kN.Text;



            tot_left_vert_reac = 0.0;
            tot_right_vert_reac = 0.0;


            for (int i = 0; i < uCMaxSR.dgv_mxf_right_des_frc.RowCount - 2; i++)
            {
                v1 = MyList.StringToDouble(uCMaxSR.dgv_mxf_left_des_frc[1, i].Value.ToString(), 0.0);
                tot_left_vert_reac += v1;

                v1 = MyList.StringToDouble(uCMaxSR.dgv_mxf_right_des_frc[1, i].Value.ToString(), 0.0);
                tot_right_vert_reac += v1;
            }



            _vert_load = Math.Max(tot_left_vert_reac, tot_right_vert_reac);
            uCMaxSR.txt_brg_max_DL_Ton.Text = _vert_load.ToString();
            uCMaxSR.txt_brg_max_DL_kN.Text = (_vert_load * 10).ToString();


            double VR = MyList.StringToDouble(uCMaxSR.txt_brg_max_VR_Ton.Text, 0.0) * 10;
            double DL = MyList.StringToDouble(uCMaxSR.txt_brg_max_DL_Ton.Text, 0.0) * 10;
            //double HRT = MyList.StringToDouble(txt_brg_max_HRT_Ton.Text, 0.0) * 10;
            //double HRL = MyList.StringToDouble(txt_brg_max_HRL_Ton.Text, 0.0) * 10;



            uCMaxSR.txt_brg_max_VR_kN.Text = VR.ToString("f3");
            uCMaxSR.txt_brg_max_DL_kN.Text = DL.ToString("f3");


            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            //list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            //list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            //list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        private void uC_BoxGirder_Stage_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_Ana_DL_create_data.Name)
            {
                btn_Ana_create_data_Click(sender, e);
            }
            else if (btn.Name == btn_Process_LL_Analysis.Name)
            {
                btn_Ana_LL_process_analysis_Click(sender, e);

            }
            else if (btn.Name == btn_view_data.Name)
            {

                btn_view_data_Click(sender, e);
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                btn_view_data_Click(sender, e);

            }
            else if (btn.Name == btn_view_report.Name)
            {
                btn_view_data_Click(sender, e);

            }
            else if (btn.Name == btn_View_Post_Process.Name)
            {
                btn_view_data_Click(sender, e);

            }
        }

        private void btn_irc_view_moving_load_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Check_Project_Folder()) return;
                Show_Section_Result();

                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }


                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }
                Write_All_Data();
                Analysis_Initialize_InputData();

                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    British_Interactive();

                    LONG_GIRDER_BRITISH_LL_TXT();
                }
                else
                {
                    Text_Changed();
                    LONG_GIRDER_LL_TXT();
                }

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    Create_Data_LL_Indian(Input_File_LL);
                }
                else
                {
                    Create_Data_LL_British(Input_File_LL);
                }
                Create_Data_DL(Input_File_DL);

            }
            catch (Exception ex) { }
        }

        private void txt_IRC_XINCR_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();
        }

        private void btn_edit_load_combs_Click(object sender, EventArgs e)
        {
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
                ff.Owner = this;
                ff.ShowDialog();
            }
            else
            {
                LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_british_loads, dgv_british_loads);
                ff.Owner = this;
                ff.ShowDialog();

            }
        }

        private void btn_long_restore_ll_IRC_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                //if (btn.Name == btn_deck_restore_ll.Name)
                //    Default_Moving_LoadData(dgv_deck_liveloads);
                //else 
                if (btn.Name == btn_long_restore_ll_IRC.Name)
                {
                    //Default_Moving_LoadData(dgv_long_liveloads);
                    Default_Moving_LoadData(dgv_long_liveloads);
                    Default_Moving_Type_LoadData(dgv_long_loads);

                }
                //else if (btn.Name == btn_long_restore_ll_BS.Name)
                //{
                //    cmb_HB.SelectedIndex = 2;
                //    British_Interactive();
                //    Default_British_HB_Type_LoadData(dgv_british_loads);
                //}
            }
        }

        void Update_Stage()
        {
            #region Update Stage Files

            int stg = (int)AnalysisType;
            for (int i = 0; i < ucStage.cmb_long_open_file.Items.Count; i++)
            {
                string org_file = Get_Girder_File(i, 0);
                string prv_file = Get_Girder_File(i, stg - 1);
                string new_file = Get_Girder_File(i, stg);
                iApp.Change_Stage_Coordinates(prv_file, new_file);
                //iApp.Change_Stage_Coordinates(org_file, prv_file, new_file);
            }


            #endregion
        }


        string Get_Girder_File(int index, int stage)
        {
            string file_name = Get_Input_File((eAnalysis)stage);

            Bridge_Analysis.Input_File = file_name;

            if (index < ucStage.cmb_long_open_file.Items.Count)
            {

                if (index == 0)
                {
                    string usp = Path.GetDirectoryName(file_name);
                    file_name = Path.Combine(Path.Combine(usp, "Dead Load Analysis"), "Dead_Load_Analysis.txt");
                }
                else
                {
                    file_name = Get_Live_Load_Analysis_Input_File(index, stage);
                }

            }
            return file_name;
        }


    }

}
