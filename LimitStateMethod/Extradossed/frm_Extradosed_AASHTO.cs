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
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.PSC_I_Girder;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
//using BridgeAnalysisDesign.PSC_BoxGirder;
using LimitStateMethod.PSC_I_Girder;
using LimitStateMethod.RCC_T_Girder;
using LimitStateMethod.PSC_Box_Girder;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.CableStayed;


namespace LimitStateMethod.Extradossed
{
    public partial class frm_Extradosed_AASHTO : Form
    {
        public string Title
        {
            get
            {
                if (Project_Type == eASTRADesignType.Extradossed_Side_Towers_Bridge_LS)
                {
                    return "EXTRADOSED CABLE STAYED BRIDGE WITH EITHER SIDE TOWERS [LRFD]";
                }
                else if (Project_Type == eASTRADesignType.Extradossed_Central_Towers_Bridge_LS)
                {
                    return "EXTRADOSED CABLE STAYED BRIDGE WITH CENTRAL TOWER  [LRFD]";
                }
                return "EXTRADOSED CABLE STAYED BRIDGE [LRFD]";
            }
        }

        PSC_BoxGirderAnalysis Deck_Analysis_DL = null;
        PSC_BoxGirderAnalysis Deck_Analysis_LL = null;


        CABLE_STAYED_Extradosed_LS_Analysis_AASHTO Extradosed = null;

        public CABLE_STAYED_Extradosed_LS_Analysis_AASHTO Bridge_Analysis
        {
            get
            {
                return Extradosed;
            }
        }

        public List<BridgeMemberAnalysis> All_Analysis { get; set; }


        PostTensionLongGirder LongGirder = null;

        PSC_Box_Section_Data PSC_SECIONS;

        RccPier rcc_pier = null;
        Save_FormRecord SaveRec = new Save_FormRecord();



        //Chiranjit [2012 09 22]
        PSC_Box_Segmental_Girder Segment_Girder = null;
        //Chiranjit [2012 09 28]
        PSC_Box_Forces Box_Forces = null;

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        public double L { get { return (L1 + L2 + L3); } }
        public double L1 { get { return MyList.StringToDouble(txt_Ana_L1.Text, 13.0); } set { txt_Ana_L1.Text = value.ToString("f3"); } }
        public double L2 { get { return MyList.StringToDouble(txt_L2.Text, 13.0); } set { txt_L2.Text = value.ToString("f3"); } }
        public double L3 { get { return MyList.StringToDouble(txt_L3.Text, 13.0); } set { txt_L3.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }

        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            //iApp = app;
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
            //B = abut_width;
        }

        //Chiranjit [2013 06 17]
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

        public string Total_DeadLoad_Reaction
        {
            get;
            set;
        }
        public string Total_LiveLoad_Reaction
        {
            get;
            set;
        }
        void frm_ViewForces_Load()
        {
            try
            {
                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);

                SupportReaction sr;

                DL_support_reactions.Clear();
                LL_support_reactions.Clear();

                Extradosed.LiveLoad_Analysis = All_Analysis[0];
                List<int> jnts = new List<int>();
                for (int i = 0; i < Extradosed.DeadLoad_Analysis.Supports.Count; i++)
                {
                    sr = new SupportReaction();
                    sr.JointNo = Extradosed.DeadLoad_Analysis.Supports[i].NodeNo;
                    jnts.Clear();
                    jnts.Add(sr.JointNo);
                    sr.Max_Reaction = Extradosed.DeadLoad_Analysis.GetJoint_R1_Axial(jnts).Force;
                    sr.Max_Negative_Mx = Extradosed.DeadLoad_Analysis.GetJoint_Torsion(jnts).Force;
                    sr.Max_Negative_Mz = Extradosed.DeadLoad_Analysis.GetJoint_MomentForce(jnts).Force;

                    DL_support_reactions.Add(sr);
                }
                for (int i = 0; i < Extradosed.LiveLoad_Analysis.Supports.Count; i++)
                {
                    sr = new SupportReaction();
                    sr.JointNo = Extradosed.LiveLoad_Analysis.Supports[i].NodeNo;
                    jnts.Clear();
                    jnts.Add(sr.JointNo);
                    sr.Max_Reaction = Extradosed.LiveLoad_Analysis.GetJoint_R1_Axial(jnts).Force;
                    sr.Max_Negative_Mx = Extradosed.LiveLoad_Analysis.GetJoint_Torsion(jnts).Force;
                    sr.Max_Negative_Mz = Extradosed.LiveLoad_Analysis.GetJoint_MomentForce(jnts).Force;
                    LL_support_reactions.Add(sr);
                }


                //Extradosed.DeadLoad_Analysis.GetJoint_R1_Axial()


                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data_DeadLoad()
        {
            if (AnalysisType != eAnalysis.Normal)
            {
                Show_and_Save_Data_DeadLoad(ucStage);
                return;
            }
        }

        void Show_and_Save_Data_DeadLoad(UC_Stage_Extradosed_LRFD uc)
        {
        }

        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            Text_Changed_Forces();

        }

        private void Text_Changed_Forces()
        {

            if (B != 0)
            {
                if (AnalysisType == eAnalysis.Normal)
                {
                }
                else
                {
                }
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
            support_reactions = LL_support_reactions;
        }

        #endregion frm_Pier_ViewDesign_Forces



        RccDeckSlab Deck = null;
        public List<string> Results { get; set; }

        IApplication iApp = null;

        bool IsCreate_Data = true;
        public frm_Extradosed_AASHTO(IApplication app, eASTRADesignType projType)
        {

            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;

            Project_Type = projType;

            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            Results = new List<string>();

            Segment_Girder = new PSC_Box_Segmental_Girder(app);

            Box_Forces = new PSC_Box_Forces();
            All_Analysis = new List<BridgeMemberAnalysis>();

            if (projType == eASTRADesignType.Extradossed_Side_Towers_Bridge_LS)
                pcb_cables.BackgroundImage = LimitStateMethod.Properties.Resources.ExtradosedEitherSideTower;
            else
                pcb_cables.BackgroundImage = LimitStateMethod.Properties.Resources.ExtradossedCentralTowers;


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
                this.Text = Title + " : " + Path.GetFileName(user_path);
                //this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            }
        }

        public string Input_File
        {
            get
            {

                return Get_Input_File((int)AnalysisType);
            }
        }
        public string Get_Input_File(int stage)
        {

            string usp = user_path;

            eAnalysis anaty = (eAnalysis)stage;
            //if (stage != 0)
            //{
            //    usp = Path.Combine(usp, "STAGE " + stage);
            //    if (!Directory.Exists(usp)) Directory.CreateDirectory(usp);
            //}
            usp = Path.Combine(usp, anaty.ToString().ToUpper() + " ANALYSIS");
            if (!Directory.Exists(usp)) Directory.CreateDirectory(usp);

            return Path.Combine(usp, "INPUT_DATA.TXT");
        }

        public string Input_File_LL
        {
            get
            {
                string usp = Path.GetDirectoryName(Get_Input_File((int)AnalysisType));


                //if (AnalysisType != eAnalysis.Normal)
                //{
                //    usp = Path.Combine(usp, "STAGE " + (int)AnalysisType);
                //    if (!Directory.Exists(usp)) Directory.CreateDirectory(usp);
                //}

                if (Directory.Exists(usp))
                {
                    if (Path.GetFileName(usp) != "Live Load Analysis")
                        if (!Directory.Exists(Path.Combine(usp, "Live Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(usp, "Live Load Analysis"));


                    return Path.Combine(Path.Combine(usp, "Live Load Analysis"), "Input_Data_LL.txt");
                }
                return "";
                //return "";
            }
        }
        public string Input_File_DL
        {
            get
            {
                string usp = Path.GetDirectoryName(Get_Input_File((int)AnalysisType));
                if (Directory.Exists(usp))
                {
                    if (Path.GetFileName(usp) != "Dead Load Analysis")
                        if (!Directory.Exists(Path.Combine(usp, "Dead Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(usp, "Dead Load Analysis"));


                    return Path.Combine(Path.Combine(usp, "Dead Load Analysis"), "Input_Data_DL.txt");
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
                return "";
            }
        }
        public string Analysis_Report_LL
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(Path.GetDirectoryName(Input_File_LL), "ANALYSIS_REP.TXT");
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
                Write_All_Data();
                Analysis_Initialize_InputData();

                LONG_GIRDER_LL_TXT();

                if (cmb_cable_type.SelectedIndex == 2)
                {

                    Create_Data_LL(Input_File_LL);
                    Create_Data_DL(Input_File_DL);

                    #region Chiranjit [2017 03 29]

                    cmb_long_open_file.Items.Clear();
                    cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS");
                    //cmb_long_open_file.Items.Add("DL + LL ANALYSIS");

                    #endregion Chiranjit [2014 10 22]
                }
                else
                {
                    Create_Data_Extradossed(Input_File);
                    #region Chiranjit [2017 03 29]

                    cmb_long_open_file.Items.Clear();
                    cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    //cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS");
                    for (int i = 0; i < all_loads.Count; i++)
                    {
                        cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + (i + 1));
                    }
                    cmb_long_open_file.Items.Add("DL + LL ANALYSIS");


                    #endregion Chiranjit [2014 10 22]

                }


                MessageBox.Show(this, "Dead Load and Live Load Analysis Input Data files are Created in Working folder.");
                cmb_long_open_file.SelectedIndex = 0;
                Button_Enable_Disable();
            }
            catch (Exception ex) { }
        }


        private void btn_Ana_LL_process_analysis_Click(object sender, EventArgs e)
        {

            string flPath = Extradosed.Input_File;
            string ana_rep_file = "";
            int c = 0;

            Write_All_Data();
            //groupBox25.Visible = true;

            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();


            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                if (cmb_cable_type.SelectedIndex == 2)
                {
                    iApp.Progress_Works.Clear();

                    flPath = Deck_Analysis_DL.Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);

                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                    flPath = Deck_Analysis_LL.Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");

                }
                else
                {

                    iApp.Progress_Works.Clear();

                    flPath = Extradosed.DeadLoadAnalysis_Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                    flPath = Extradosed.TotalAnalysis_Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                    if (File.Exists(iApp.Stage_File))
                    {
                        flPath = Extradosed.DeadLoadAnalysis_Input_File;

                    }

                    for (int i = 0; i < (all_loads.Count); i++)
                    {
                        flPath = Get_Live_Load_Analysis_Input_File(i + 1);

                        pd = new ProcessData();
                        pd.Process_File_Name = flPath;
                        pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                        pcol.Add(pd);

                        iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");
                    }
                }
            }
            //pcol.AstEXE = "AST008.EXE";
            if (!iApp.Show_and_Run_Process_List(pcol))
            {
                Button_Enable_Disable();
                return;
            }

            //Show_Member_Forces_British();

            Show_Member_Forces_Indian("");



            Extradosed.All_LL_Analysis = All_Analysis;
            //uC_CableStayedDesign1.CS_Analysis = Extradosed;

            Save_FormRecord.Write_All_Data(this, user_path);
        }
        void Show_and_Save_Data_Load_1_2_3()
        {

            if (!File.Exists(analysis_rep)) return;



            UC_SupportReactions_LRFD uCSR_DL = uC_SR_DL;
            UC_SupportReactions_LRFD uCSR_LL = uC_SR_LL;
            UC_MaxReactions_LRFD uCMaxSR = uC_SR_Max;


            if (AnalysisType != eAnalysis.Normal)
            {
                uCSR_DL = ucStage.uC_SR_DL;
                uCSR_LL = ucStage.uC_SR_LL;
                uCMaxSR = ucStage.uC_SR_Max;
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
            list_arr.Add(string.Format(format, "   ", "  (Kip)   ", "  (Kip-Ft)", "  (Kip-Ft)"));
            list_arr.Add("");
            SupportReaction sr = null;

            MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

            double tot_left_vert_reac = 0.0;
            double tot_right_vert_reac = 0.0;

            double tot_left_Mx = 0.0;
            double tot_left_Mz = 0.0;

            double tot_right_Mx = 0.0;
            double tot_right_Mz = 0.0;



            DataGridView dgv_left = uCSR_DL.dgv_left_des_frc;
            DataGridView dgv_right = uCSR_DL.dgv_right_des_frc;









            dgv_left.Rows.Clear();
            dgv_right.Rows.Clear();
            list_arr.Add("LEFT END");
            list_arr.Add("--------");


            double _vert_load, _mx, _mz;

            _vert_load = _mx = _mz = 0.0;
            int _jnt_no = 0;
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);

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


            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                _jnt_no = mlist.GetInt(i);

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

                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right.Rows.Add(_jnt_no, Math.Abs(_vert_load), _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load);
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));

            }
            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Ton
            //tot_right_vert_reac /= 10.0;
            //tot_right_Mx /= 10.0;
            //tot_right_Mz /= 10.0;
            uCSR_DL.txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            uCSR_DL.txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            uCSR_DL.txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");












            //txt_both_ends_total.Text = (tot_left_vert_reac + tot_right_vert_reac).ToString("0.000");
            list_arr.Add("");
            //list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Kip");

            uCSR_DL.txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            //uCSR_DL.txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");



            uCSR_DL.txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            //uCSR_DL.txt_max_vert_reac_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            //list_arr.Add("TOTAL VERTICAL REACTION = " + uCSR_DL.txt_final_vert_reac.Text + " Kip" + "    =  " + uCSR_DL.txt_final_vert_rec_kN.Text + " kN");
            list_arr.Add("TOTAL VERTICAL REACTION = " + uCSR_DL.txt_final_vert_reac.Text + " Kip");

            //txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            uCSR_DL.txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            //uCSR_DL.txt_final_Mx_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");




            uCSR_DL.txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //uCSR_DL.txt_max_Mx_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");




            //list_arr.Add("        MAXIMUM  MX     = " + uCSR_DL.txt_final_Mx.Text + " Kip-Ft" + "  =  " + uCSR_DL.txt_final_Mx_kN.Text + " kN-m");
            list_arr.Add("        MAXIMUM  MX     = " + uCSR_DL.txt_final_Mx.Text + " Kip-Ft");
            //txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            uCSR_DL.txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            //uCSR_DL.txt_final_Mz_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            //txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            uCSR_DL.txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //uCSR_DL.txt_max_Mz_kN.Text = (MyList.StringToDouble(uCSR_DL.txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");









            //list_arr.Add("        MAXIMUM  MZ     = " + uCSR_DL.txt_final_Mz.Text + " Kip-Ft" + "  =  " + uCSR_DL.txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("        MAXIMUM  MZ     = " + uCSR_DL.txt_final_Mz.Text + " Kip-Ft" );
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");


            #region LL
            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;



            uCSR_LL.dgv_left_des_frc.Rows.Clear();
            uCSR_LL.dgv_right_des_frc.Rows.Clear();


            dgv_left = uCSR_LL.dgv_left_des_frc;
            dgv_right = uCSR_LL.dgv_right_des_frc;



            mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);

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

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);

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


            uCSR_LL.txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");



            uCSR_LL.txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");






            uCSR_LL.txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");

            uCSR_LL.txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");

            uCSR_LL.txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");




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



            #region Chiranjit [2017 06 11]

            uCMaxSR.txt_mxf_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");

            uCMaxSR.txt_mxf_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");

            uCMaxSR.txt_mxf_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");

            #endregion Chiranjit [2017 06 11]


            uCMaxSR.txt_brg_max_VR_Ton.Text = uCMaxSR.txt_mxf_max_vert_reac.Text;



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


            double VR = MyList.StringToDouble(uCMaxSR.txt_brg_max_VR_Ton.Text, 0.0) * 10;
            double DL = MyList.StringToDouble(uCMaxSR.txt_brg_max_DL_Ton.Text, 0.0) * 10;
            //double HRT = MyList.StringToDouble(txt_brg_max_HRT_Ton.Text, 0.0) * 10;
            //double HRL = MyList.StringToDouble(txt_brg_max_HRL_Ton.Text, 0.0) * 10;



            //uCMaxSR.txt_brg_max_VR_kN.Text = VR.ToString("f3");
            //uCMaxSR.txt_brg_max_DL_kN.Text = DL.ToString("f3");
            //txt_brg_max_HRT_kN.Text = HRT.ToString("f3");
            //txt_brg_max_HRL_kN.Text = HRL.ToString("f3");


            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            //list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            //list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            //list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        private void Show_Member_Forces_Indian(string ana_rep_file)
        {
            All_Analysis.Clear();

            if (cmb_cable_type.SelectedIndex == 2)
            {
                ana_rep_file = MyList.Get_Analysis_Report_File(Deck_Analysis_DL.Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


                ana_rep_file = MyList.Get_Analysis_Report_File(Deck_Analysis_LL.Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


            }
            else
            {
                Extradosed.Input_File = Input_File;

                ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.DeadLoadAnalysis_Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Extradosed.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }

                ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.TotalAnalysis_Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


                for (int i = 0; i < all_loads.Count; i++)
                {

                    ana_rep_file = Get_Live_Load_Analysis_Input_File(i + 1);

                    ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                    if (File.Exists(ana_rep_file))
                    {
                        All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                    }
                }
                Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;


                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];
            }

            Show_Moment_Shear_DL();
            Show_Moment_Shear_LL();

            Show_ReactionForces();

            Show_Analysis_Result();


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

            Extradosed.Input_File = Input_File;
            ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.DeadLoadAnalysis_Input_File);

            if (File.Exists(ana_rep_file))
            {
                Extradosed.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
            }

            ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.TotalAnalysis_Input_File);

            if (File.Exists(ana_rep_file))
            {
                Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
            }


            for (int i = 0; i < all_loads.Count; i++)
            {

                ana_rep_file = Get_Live_Load_Analysis_Input_File(i + 1);

                ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                if (File.Exists(ana_rep_file))
                {
                    All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                }
            }
            Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;

            Show_Moment_Shear_DL();

            Show_Moment_Shear_LL();

            Show_ReactionForces();
        }

        void Show_ReactionForces()
        {
            #region Chiranjit [2012 10 31]

            if (cmb_cable_type.SelectedIndex != 2)
                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

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
            double BB = B;


            Show_and_Save_Data_Load_1_2_3();

            //frm_ViewForces(BB, Deck_Analysis_DL.Analysis_Report, Deck_Analysis_LL.Bridge_Analysis.Analysis_File, (s1 + " " + s2));
            //frm_ViewForces_Load();

            //frm_Pier_ViewDesign_Forces(Deck_Analysis_LL.Bridge_Analysis.Analysis_File, s1, s2);
            //frm_ViewDesign_Forces_Load();




            //if (AnalysisType == eAnalysis.Normal)
            //{

            //    //Chiranjit [2012 11 01]
            //    txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
            //    txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

            //    txt_ana_TSRP.Text = txt_final_vert_reac.Text;
            //    txt_ana_MSLD.Text = txt_left_total_Mx.Text;
            //    txt_ana_MSTD.Text = txt_right_total_Mz.Text;

            //}
            //else
            //{

            //}

            #endregion Chiranjit [2012 10 31]

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
                load_lst.AddRange(txt_Ana_LL_member_load.Lines);
                //if (dgv_Ana_live_load.RowCount != 0)
                //{
                //    if (!File.Exists(Deck_Analysis_DL.LiveLoad_File))
                //    {
                //        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                //    }
                //}
            }
            else
            {
                load_lst.Add("LOAD    1   " + s);
                load_lst.Add("MEMBER LOAD");
                load_lst.Add("1 TO 220 UNI GY -0.001");
                Deck_Analysis_LL.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
                //if (dgv_Ana_live_load.RowCount != 0)
                load_lst.AddRange(Get_MovingLoad_Data(Deck_Analysis_LL.Live_Load_List));
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
            if (IsDeadLoad)
                txt_Ana_LL_member_load.Lines = list_member_load.ToArray();


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
                    rcc_pier.FilePath = user_path;

                    txt_Ana_analysis_file.Text = chk_file;
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Chiranjit [2012 11 01]
                    //Show_ReactionForces();



                    Button_Enable_Disable();

                    //Chiranjit [2012 12 18]

                    //if (File.Exists(Deck_Analysis_DL.Input_File))
                    //{

                    //    Show_ReadMemberLoad(Deck_Analysis_DL.Input_File, true);
                    //}
                    //if (File.Exists(Deck_Analysis_LL.Input_File))
                    //{

                    //    //Show_ReadMemberLoad(Deck_Analysis_LL.Input_File, false);
                    //    Show_LoadGeneration(Deck_Analysis_LL.Input_File);
                    //}
                    //Show_Analysis_Result();
                }
            }



            //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

            //string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            //Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            //if (Deck_Analysis_LL.Live_Load_List == null) return;

            //cmb_Ana_DL_load_type.Items.Clear();
            //for (int i = 0; i < Deck_Analysis_LL.Live_Load_List.Count; i++)
            //{
            //    cmb_Ana_DL_load_type.Items.Add(Deck_Analysis_LL.Live_Load_List[i].TypeNo + " : " + Deck_Analysis_LL.Live_Load_List[i].Code);
            //}
            //if (cmb_Ana_DL_load_type.Items.Count > 0)
            //{
            //    cmb_Ana_DL_load_type.SelectedIndex = cmb_Ana_DL_load_type.Items.Count - 1;
            //    //if (dgv_live_load.RowCount == 0)
            //    //Add_LiveLoad();
            //}




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
            Deck_Analysis_DL.CreateData_DeadLoad();
            Deck_Analysis_DL.WriteData_DeadLoad(Deck_Analysis_DL.Input_File, PSC_SECIONS);

            Ana_Write_Long_Girder_Load_Data(file_name, false, true, 1);

            Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_DL.Input_File);

            string ll_txt = Deck_Analysis_DL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_DL.Live_Load_List == null) return;

            Button_Enable_Disable();
        }
        private void Create_Data_LL(string file_name)
        {

            Deck_Analysis_LL.Input_File = file_name;
            Deck_Analysis_LL.CreateData();
            Deck_Analysis_LL.WriteData_LiveLoad(Deck_Analysis_LL.Input_File, PSC_SECIONS);


            Ana_Write_Long_Girder_Load_Data(file_name, true, false, 1);
            Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_LL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            Button_Enable_Disable();
        }

        List<MemberSectionProperty> SectionProperty { get; set; }


        private void Create_Data_Extradossed(string file_name)
        {

            Extradosed.Input_File = file_name;
            Extradosed.Start_Support = Start_Support_Text;
            Extradosed.End_Support = END_Support_Text;

            Extradosed.L2 = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);
            Extradosed.L1 = MyList.StringToDouble(txt_L2.Text, 0.0);
            Extradosed.L3 = MyList.StringToDouble(txt_L3.Text, 0.0);

            Extradosed.Tower_height = MyList.StringToDouble(txt_Tower_Height);

            Extradosed.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Extradosed.Cantilever_Width = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);



            Extradosed.Init_dist = MyList.StringToDouble(txt_init_cable.Text, 0.0);

            Extradosed.Cable_Nos = MyList.StringToInt(txt_cable_no.Text, 1) / 2;

            Extradosed.Cable_x_dist = MyList.StringToDouble(txt_horizontal_cbl_dist.Text, 0.0);
            Extradosed.Cable_intv_dist = MyList.StringToDouble(txt_vertical_cbl_dist.Text, 0.0);
            Extradosed.Cable_y_dist = MyList.StringToDouble(txt_vertical_cbl_min_dist.Text, 0.0);


            Extradosed.cd = MyList.StringToDouble(txt_cable_dia.Text, 0.0);
            Extradosed.Bt = MyList.StringToDouble(txt_tower_Bt.Text, 0.0);
            Extradosed.Dt = MyList.StringToDouble(txt_tower_Dt.Text, 0.0);


            Extradosed.Support_Distance = MyList.StringToDouble(txt_support_distance.Text, 0.0);
            Extradosed.Effective_Distance = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);

            Extradosed.Support_Distance = 0.0;


            Extradosed._cen_Ax = MyList.StringToDouble(txt_cen_AX.Text, 0.0);
            Extradosed._cen_Ix = MyList.StringToDouble(txt_cen_IXX.Text, 0.0);
            Extradosed._cen_Iy = MyList.StringToDouble(txt_cen_IYY.Text, 0.0);
            Extradosed._cen_Iz = MyList.StringToDouble(txt_cen_IZZ.Text, 0.0);


            Extradosed._inn_Ax = MyList.StringToDouble(txt_inn_AX.Text, 0.0);
            Extradosed._inn_Ix = MyList.StringToDouble(txt_inn_IXX.Text, 0.0);
            Extradosed._inn_Iy = MyList.StringToDouble(txt_inn_IYY.Text, 0.0);
            Extradosed._inn_Iz = MyList.StringToDouble(txt_inn_IZZ.Text, 0.0);


            Extradosed._out_Ax = MyList.StringToDouble(txt_out_AX.Text, 0.0);
            Extradosed._out_Ix = MyList.StringToDouble(txt_out_IXX.Text, 0.0);
            Extradosed._out_Iy = MyList.StringToDouble(txt_out_IYY.Text, 0.0);
            Extradosed._out_Iz = MyList.StringToDouble(txt_out_IZZ.Text, 0.0);


            if (AnalysisType != eAnalysis.Normal)
            {
                Extradosed.E_CONC = ucStage.txt_emod_conc.Text;
                Extradosed.E_STEEL = ucStage.txt_emod_steel.Text;
                Extradosed.E_CABLE = ucStage.txt_emod_cable.Text;

                Extradosed.DEN_CONC = ucStage.txt_den_conc.Text;
                Extradosed.DEN_STEEL = ucStage.txt_den_steel.Text;
                Extradosed.DEN_CABLE = ucStage.txt_den_cable.Text;

                Extradosed.PR_CONC = ucStage.txt_PR_conc.Text;
                Extradosed.PR_STEEL = ucStage.txt_PR_steel.Text;
                Extradosed.PR_CABLE = ucStage.txt_PR_cable.Text;
            }
            else
            {
                Extradosed.E_CONC = txt_emod_conc.Text;
                Extradosed.E_STEEL = txt_emod_steel.Text;
                Extradosed.E_CABLE = txt_emod_cable.Text;

                Extradosed.DEN_CONC = txt_den_conc.Text;
                Extradosed.DEN_STEEL = txt_den_steel.Text;
                Extradosed.DEN_CABLE = txt_den_cable.Text;

                Extradosed.PR_CONC = txt_PR_conc.Text;
                Extradosed.PR_STEEL = txt_PR_steel.Text;
                Extradosed.PR_CABLE = txt_PR_cable.Text;
            }


            if (cmb_cable_type.SelectedIndex == 0)
            {
                Extradosed.IsCentral_Cable = false;
                Extradosed.Create_Extradossed_Data();
            }
            else
            {
                Extradosed.IsCentral_Cable = true;
                Extradosed.Create_Extradossed_Data_1_Side_Centre();
            }
            if (rbtn_multiple_cell.Checked)
            {
                Section2_Section_Properties();

                File.WriteAllLines(Path.Combine(user_path, "LoadCalculations.txt"), Extradosed.Load_Calculation(aashto_box).ToArray());
                LONG_GIRDER_LL_TXT();

                Extradosed.WriteData_LiveLoad(Extradosed.Input_File, long_ll, aashto_box);

                txt_Ana_LL_member_load.Lines = Extradosed.joint_loads.ToArray();

                Extradosed.WriteData_LiveLoad(Extradosed.LiveLoadAnalysis_Input_File, long_ll, aashto_box);
                Ana_Write_Cable_Load_Data(Extradosed.LiveLoadAnalysis_Input_File, true, false, 1);

                Extradosed.WriteData_LiveLoad(Extradosed.TotalAnalysis_Input_File, long_ll, aashto_box);
                Ana_Write_Cable_Load_Data(Extradosed.TotalAnalysis_Input_File, true, true, 1);

                Extradosed.WriteData_LiveLoad(Extradosed.DeadLoadAnalysis_Input_File, null, aashto_box);
                Ana_Write_Cable_Load_Data(Extradosed.DeadLoadAnalysis_Input_File, false, true, 1);

                for (int i = 0; i < all_loads.Count; i++)
                {
                    Extradosed.WriteData_LiveLoad(Extradosed.Get_LL_Analysis_Input_File(i + 1), long_ll, aashto_box);
                    Ana_Write_Cable_Load_Data(Extradosed.Get_LL_Analysis_Input_File(i + 1), true, false, i + 1);
                }
            }
            else
            {

                Show_Section_Result();
                Extradosed.PSC_SECIONS = PSC_SECIONS;
                File.WriteAllLines(Path.Combine(user_path, "LoadCalculations.txt"), Extradosed.Load_Calculation().ToArray());
                LONG_GIRDER_LL_TXT();

                Extradosed.WriteData_LiveLoad(Extradosed.Input_File, long_ll, null);

                txt_Ana_LL_member_load.Lines = Extradosed.joint_loads.ToArray();

                Extradosed.WriteData_LiveLoad(Extradosed.LiveLoadAnalysis_Input_File, long_ll, null);
                Ana_Write_Cable_Load_Data(Extradosed.LiveLoadAnalysis_Input_File, true, false, 1);

                Extradosed.WriteData_LiveLoad(Extradosed.TotalAnalysis_Input_File, long_ll, null);
                Ana_Write_Cable_Load_Data(Extradosed.TotalAnalysis_Input_File, true, true, 1);

                Extradosed.WriteData_LiveLoad(Extradosed.DeadLoadAnalysis_Input_File, null, null);
                Ana_Write_Cable_Load_Data(Extradosed.DeadLoadAnalysis_Input_File, false, true, 1);

                for (int i = 0; i < all_loads.Count; i++)
                {
                    Extradosed.WriteData_LiveLoad(Extradosed.Get_LL_Analysis_Input_File(i + 1), long_ll, null);
                    Ana_Write_Cable_Load_Data(Extradosed.Get_LL_Analysis_Input_File(i + 1), true, false, i + 1);
                }
            }
            Button_Enable_Disable();
        }


        void Analysis_Initialize_InputData()
        {
            Deck_Analysis_DL.Length = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);
            Deck_Analysis_DL.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Deck_Analysis_DL.WidthCantilever = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);
          //  Deck_Analysis_DL.Skew_Angle = MyList.StringToDouble(txt_Ana_skew_angle.Text, 0.0);
            Deck_Analysis_DL.Effective_Depth = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);

            Deck_Analysis_LL.Length = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);
            Deck_Analysis_LL.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Deck_Analysis_LL.WidthCantilever = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);
           // Deck_Analysis_LL.Skew_Angle = MyList.StringToDouble(txt_Ana_skew_angle.Text, 0.0);
            Deck_Analysis_LL.Effective_Depth = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);

            Deck_Analysis_LL.Start_Support = Start_Support_Text;
            Deck_Analysis_LL.End_Support = END_Support_Text;

            Deck_Analysis_DL.Start_Support = Start_Support_Text;
            Deck_Analysis_DL.End_Support = END_Support_Text;

        }

        void Set_Box_Forces()
        {
            Box_Forces.FRC_LL_Shear[0] = MyList.StringToDouble(txt_Ana_live_inner_long_support_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[0] = MyList.StringToDouble(txt_Ana_live_inner_long_support_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[1] = MyList.StringToDouble(txt_Ana_live_inner_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[1] = MyList.StringToDouble(txt_Ana_live_inner_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[2] = MyList.StringToDouble(txt_Ana_live_inner_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[2] = MyList.StringToDouble(txt_Ana_live_inner_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[3] = MyList.StringToDouble(txt_Ana_live_inner_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[3] = MyList.StringToDouble(txt_Ana_live_inner_long_L4_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[4] = MyList.StringToDouble(txt_Ana_live_inner_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[4] = MyList.StringToDouble(txt_Ana_live_inner_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[5] = MyList.StringToDouble(txt_Ana_live_inner_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[5] = MyList.StringToDouble(txt_Ana_live_inner_long_L2_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[0] = MyList.StringToDouble(txt_Ana_dead_inner_long_support_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[0] = MyList.StringToDouble(txt_Ana_dead_inner_long_support_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[1] = MyList.StringToDouble(txt_Ana_dead_inner_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[1] = MyList.StringToDouble(txt_Ana_dead_inner_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[2] = MyList.StringToDouble(txt_Ana_dead_inner_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[2] = MyList.StringToDouble(txt_Ana_dead_inner_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[3] = MyList.StringToDouble(txt_Ana_dead_inner_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[3] = MyList.StringToDouble(txt_Ana_dead_inner_long_L4_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[4] = MyList.StringToDouble(txt_Ana_dead_inner_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[4] = MyList.StringToDouble(txt_Ana_dead_inner_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[5] = MyList.StringToDouble(txt_Ana_dead_inner_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[5] = MyList.StringToDouble(txt_Ana_dead_inner_long_L2_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[0] = MyList.StringToDouble(txt_Ana_live_outer_long_support_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[0] = MyList.StringToDouble(txt_Ana_live_outer_long_support_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[1] = MyList.StringToDouble(txt_Ana_live_outer_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[1] = MyList.StringToDouble(txt_Ana_live_outer_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[2] = MyList.StringToDouble(txt_Ana_live_outer_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[2] = MyList.StringToDouble(txt_Ana_live_outer_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[3] = MyList.StringToDouble(txt_Ana_live_outer_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[3] = MyList.StringToDouble(txt_Ana_live_outer_long_L4_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[4] = MyList.StringToDouble(txt_Ana_live_outer_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[4] = MyList.StringToDouble(txt_Ana_live_outer_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[5] = MyList.StringToDouble(txt_Ana_live_outer_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[5] = MyList.StringToDouble(txt_Ana_live_outer_long_L2_moment.Text, 0.0);

            Box_Forces.Set_Absolute();

        }
        void Show_Moment_Shear_LL()
        {
            if (AnalysisType != eAnalysis.Normal)
            {
                Show_Moment_Shear_LL(ucStage);
                return;
            }

            int i = 0;

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17

                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

                List<double> frc = new List<double>();
                List<double> moms = new List<double>();
                for (i = 0; i < Extradosed._Columns - 2; i++)
                {
                    var item = Extradosed.Long_Girder_Members_Array[2, i];

                    var f = (AnalysisData)Deck_Analysis_LL.Bridge_Analysis.MemberAnalysis[item.MemberNo];
                    if (f != null)
                    {
                        if (!frc.Contains(f.MaxShearForce.Force))
                            frc.Add(f.MaxShearForce.Force);
                        if (!moms.Contains(f.MaxBendingMoment.Force))
                            moms.Add(f.MaxBendingMoment.Force);
                    }
                }

                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                moms.Reverse();


                double dval = 0.0;

                int idx = frc.Count - 1;
                dval = frc[idx];
                txt_Ana_live_inner_long_support_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 1;
                dval = moms[idx];
                txt_Ana_live_inner_long_support_moment.Text = Math.Abs(dval).ToString();




                idx = frc.Count - 4;
                dval = frc[idx];
                txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 6;
                dval = moms[idx];
                txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






                idx = frc.Count - frc.Count / 3;
                dval = frc[idx];
                txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 4;
                dval = moms[idx];
                txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





                idx = frc.Count - frc.Count / 2;
                dval = frc[idx];
                txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 8;
                dval = moms[idx];
                txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


                idx = frc.Count / 8;
                dval = frc[idx];
                txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 10;
                dval = moms[idx];
                txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                idx = 0;
                dval = frc[idx];
                txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(dval).ToString();



                idx = 0;
                dval = moms[idx];
                txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


                return;

                #endregion CC 2017 02 17
            }

            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();

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
            //int i = 0;

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
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

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

            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
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
            //double eff_dep = ;

            //_L_2_joints.Clear();

            cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.WidthCantilever = cant_wi;



            //eff_d = _X_joints.Count > 1 ? _X_joints[1] : 2.5;
            //eff_d = (eff_d == support ? _X_joints[2] : 2.5);



            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }

            //_L_2_joints.Clear();
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

                //if ((jn_col[i].X.ToString("0.0") == ((support) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
                //if ((jn_col[i].X.ToString("0.0") == ((L - (support)) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
            }

            //For Support
            //for (node = 12; node <= 22; node++) _joints.Add(node);
            //for (node = 122; node <= 132; node++) _joints.Add(node);

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            if (_support_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[0] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[0] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                Box_Forces.FRC_LL_Torsion[0] = force;

                _joints.Clear();
            }

            //For Deff
            //for (node = 111; node <= 121; node++) _joints.Add(node);
            //for (node = 23; node <= 33; node++) _joints.Add(node);
            if (_deff_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[1] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[1] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                Box_Forces.FRC_LL_Torsion[1] = force;

                _joints.Clear();
            }

            //For L/8
            //for (node = 100; node <= 110; node++) _joints.Add(node);
            //for (node = 34; node <= 44; node++) _joints.Add(node);
            if (_L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[2] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[2] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                Box_Forces.FRC_LL_Torsion[2] = force;

                _joints.Clear();
            }



            //For L/4
            //for (node = 89; node <= 99; node++) _joints.Add(node);
            //for (node = 45; node <= 55; node++) _joints.Add(node);
            if (_L4_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[3] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
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
                txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
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
                txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[5] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[5] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[5] = force;

                _joints.Clear();
                _joints = null;
            }


            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_LL(UC_Stage_Extradosed_LRFD uc)
        {
            Show_Moment_Shear_LL((int)AnalysisType);
            return;
            int i = 0;

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17

                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

                List<double> frc = new List<double>();
                List<double> moms = new List<double>();

                double _f = 0.0;
                double _m = 0.0;
                for (i = 0; i < Extradosed._Columns - 2; i++)
                {
                    var item = Extradosed.Long_Girder_Members_Array[2, i];
                    _f = 0.0;
                    _m = 0.0;
                    for (int j = 0; j < All_Analysis.Count; j++)
                    {
                        //var f = (AnalysisData)Deck_Analysis_LL.Bridge_Analysis.MemberAnalysis[item.MemberNo];
                        var f = (AnalysisData)All_Analysis[j].MemberAnalysis[item.MemberNo];
                        if (f != null)
                        {
                            if (_f < f.MaxShearForce.Force)
                                _f = f.MaxShearForce.Force;
                            if (_m < f.MaxBendingMoment.Force)
                                _m = f.MaxBendingMoment.Force;
                            //if (!frc.Contains(f.MaxShearForce.Force))
                            //    frc.Add(f.MaxShearForce.Force);
                            //if (!moms.Contains(f.MaxBendingMoment.Force))
                            //    moms.Add(f.MaxBendingMoment.Force);


                        }
                    }

                    if (!frc.Contains(_f))
                        frc.Add(_f);
                    if (!moms.Contains(_m))
                        moms.Add(_m);
                }

                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                moms.Reverse();


                double dval = 0.0;

                int idx = frc.Count - 1;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_support_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 1;
                dval = moms[idx];
                uc.txt_Ana_live_inner_long_support_moment.Text = Math.Abs(dval).ToString();




                idx = frc.Count - 4;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 6;
                dval = moms[idx];
                uc.txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






                idx = frc.Count - frc.Count / 3;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 4;
                dval = moms[idx];
                uc.txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





                idx = frc.Count - frc.Count / 2;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 8;
                dval = moms[idx];
                uc.txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


                idx = frc.Count / 8;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 10;
                dval = moms[idx];
                uc.txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                idx = 0;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(dval).ToString();



                idx = 0;
                dval = moms[idx];
                uc.txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


                return;

                #endregion CC 2017 02 17
            }

            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();

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
            //int i = 0;

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
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

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

            //val = MyList.StringToDouble(uc.txt_Ana_eff_depth.Text, -999.0);
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
            //double eff_dep = ;

            //_L_2_joints.Clear();

            cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.WidthCantilever = cant_wi;



            //eff_d = _X_joints.Count > 1 ? _X_joints[1] : 2.5;
            //eff_d = (eff_d == support ? _X_joints[2] : 2.5);



            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }

            //_L_2_joints.Clear();
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

                //if ((jn_col[i].X.ToString("0.0") == ((support) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
                //if ((jn_col[i].X.ToString("0.0") == ((L - (support)) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
            }

            //For Support
            //for (node = 12; node <= 22; node++) _joints.Add(node);
            //for (node = 122; node <= 132; node++) _joints.Add(node);

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            if (_support_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                uc.txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[0] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                uc.txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[0] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                Box_Forces.FRC_LL_Torsion[0] = force;

                _joints.Clear();
            }

            //For Deff
            //for (node = 111; node <= 121; node++) _joints.Add(node);
            //for (node = 23; node <= 33; node++) _joints.Add(node);
            if (_deff_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                uc.txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[1] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                uc.txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[1] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                Box_Forces.FRC_LL_Torsion[1] = force;

                _joints.Clear();
            }

            //For L/8
            //for (node = 100; node <= 110; node++) _joints.Add(node);
            //for (node = 34; node <= 44; node++) _joints.Add(node);
            if (_L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                uc.txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[2] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                uc.txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[2] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                Box_Forces.FRC_LL_Torsion[2] = force;

                _joints.Clear();
            }



            //For L/4
            //for (node = 89; node <= 99; node++) _joints.Add(node);
            //for (node = 45; node <= 55; node++) _joints.Add(node);
            if (_L4_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                uc.txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[3] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                uc.txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
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
                uc.txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                uc.txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
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
                uc.txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[5] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                uc.txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[5] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[5] = force;

                _joints.Clear();
                _joints = null;
            }


            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }


        void Show_Moment_Shear_LL(int stage)
        {
            UC_Stage_Extradosed_LRFD uc = ucStage;

            UC_Stage_Extradosed_LRFD ucPrev = ucStage;

            if (stage == 2) ucPrev = uC_Stage_Extradosed_LRFD1;
            if (stage == 3) ucPrev = uC_Stage_Extradosed_LRFD2;
            if (stage == 4) ucPrev = uC_Stage_Extradosed_LRFD3;
            if (stage == 5) ucPrev = uC_Stage_Extradosed_LRFD4;

            double fact = 1.03;

            int i = 0;

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17

                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

                List<double> frc = new List<double>();
                List<double> moms = new List<double>();

                if (stage == 1)
                {
                    frc.Add(MyList.StringToDouble(txt_Ana_live_inner_long_support_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_live_inner_long_support_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_live_inner_long_deff_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_live_inner_long_deff_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_live_inner_long_L8_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_live_inner_long_L8_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_live_inner_long_L4_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_live_inner_long_L4_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_live_inner_long_3L_8_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_live_inner_long_3L_8_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_live_inner_long_L2_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_live_inner_long_L2_moment.Text) * fact);

                }
                else
                {
                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_support_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_support_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_deff_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_deff_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_L8_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_L8_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_L4_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_L4_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_3L_8_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_3L_8_moment.Text) * fact);

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_L2_shear.Text) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_live_inner_long_L2_moment.Text) * fact);
                }
                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                frc.Reverse();


                double dval = 0.0;

                int idx = 0;
                dval = frc[idx];
                uc.txt_Ana_live_inner_long_support_shear.Text = Math.Abs(dval).ToString("f2");

                dval = moms[idx++];
                uc.txt_Ana_live_inner_long_support_moment.Text = Math.Abs(dval).ToString("f2");

                dval = frc[idx];
                uc.txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(dval).ToString("f2");

                dval = moms[idx++];
                uc.txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(dval).ToString("f2");

                dval = frc[idx];
                uc.txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(dval).ToString("f2");

                dval = moms[idx++];
                uc.txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(dval).ToString("f2");

                dval = frc[idx];
                uc.txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(dval).ToString("f2");


                dval = moms[idx++];
                uc.txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(dval).ToString("f2");


                dval = frc[idx];
                uc.txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString("f2");


                dval = moms[idx++];
                uc.txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString("f2");


                dval = frc[idx];
                uc.txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(dval).ToString("f2");


                dval = moms[idx];
                uc.txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(dval).ToString("f2");


                return;

                #endregion CC 2017 02 17
            }

            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();

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
            //int i = 0;

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
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

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

            //val = MyList.StringToDouble(uc.txt_Ana_eff_depth.Text, -999.0);
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
            //double eff_dep = ;

            //_L_2_joints.Clear();

            cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.WidthCantilever = cant_wi;



            //eff_d = _X_joints.Count > 1 ? _X_joints[1] : 2.5;
            //eff_d = (eff_d == support ? _X_joints[2] : 2.5);



            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }

            //_L_2_joints.Clear();
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

                //if ((jn_col[i].X.ToString("0.0") == ((support) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
                //if ((jn_col[i].X.ToString("0.0") == ((L - (support)) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
            }

            //For Support
            //for (node = 12; node <= 22; node++) _joints.Add(node);
            //for (node = 122; node <= 132; node++) _joints.Add(node);

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            if (_support_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                uc.txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[0] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                uc.txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[0] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                Box_Forces.FRC_LL_Torsion[0] = force;

                _joints.Clear();
            }

            //For Deff
            //for (node = 111; node <= 121; node++) _joints.Add(node);
            //for (node = 23; node <= 33; node++) _joints.Add(node);
            if (_deff_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                uc.txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[1] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                uc.txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[1] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                Box_Forces.FRC_LL_Torsion[1] = force;

                _joints.Clear();
            }

            //For L/8
            //for (node = 100; node <= 110; node++) _joints.Add(node);
            //for (node = 34; node <= 44; node++) _joints.Add(node);
            if (_L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                uc.txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[2] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                uc.txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[2] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                Box_Forces.FRC_LL_Torsion[2] = force;

                _joints.Clear();
            }



            //For L/4
            //for (node = 89; node <= 99; node++) _joints.Add(node);
            //for (node = 45; node <= 55; node++) _joints.Add(node);
            if (_L4_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                uc.txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[3] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                uc.txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
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
                uc.txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                uc.txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
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
                uc.txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[5] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                uc.txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[5] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[5] = force;

                _joints.Clear();
                _joints = null;
            }


            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_DL()
        {



            NodeResults res = new NodeResults();

            for (int i = 0; i < Extradosed._Cable_jnts.Count; i++)
            {
                res.Add(Extradosed.DeadLoad_Analysis.Node_Max_Displacements.Get_Max_XYZ_Deflection(Extradosed._Cable_jnts[i]));
            }
            double ydef = res.Get_Max_Y_Deflection().Y_Translation;

            //double ydef = Extradosed.DeadLoad_Analysis.Node_Max_Displacements.Get_Max_Y_Deflection().Y_Translation;



            //double len = Extradosed.L1 + Extradosed.L2 + Extradosed.L3;
            double len = Extradosed.L2;

            if (AnalysisType != eAnalysis.Normal)
            {
                Show_Moment_Shear_DL(ucStage);

                ucStage.txt_max_delf.Text = ydef.ToString();
                ucStage.lbl_max_delf.Text = string.Format("Span / 800 = {0}/800 = {1}ft", len, len / 800);
                return;
            }


            txt_max_delf.Text = ydef.ToString();
            lbl_max_delf.Text = string.Format("Span / 800 = {0}/800 = {1}ft", len, len / 800);


            if (AnalysisType != eAnalysis.Normal)
            {
                Show_Moment_Shear_DL(ucStage);
                return;
            }

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17




                List<double> frc = new List<double>();
                List<double> moms = new List<double>();
                int memNo = -1;
                for (int i = 0; i < Extradosed._Columns - 2; i++)
                {

                    var item = Extradosed.Long_Girder_Members_Array[2, i];

                    if (cmb_cable_type.SelectedIndex == 0)
                        memNo = Extradosed.Long_Girder_Members_Array[2, i].MemberNo;
                    else
                        memNo = Extradosed.Long_Girder_Members_Array[1, i].MemberNo;




                    //var f = (AnalysisData)Extradosed.DeadLoad_Analysis.MemberAnalysis[item.MemberNo];
                    var f = (AnalysisData)Extradosed.DeadLoad_Analysis.MemberAnalysis[memNo];

                    if (f != null)
                    {
                        if (!frc.Contains(f.MaxShearForce.Force))
                            frc.Add(f.MaxShearForce.Force);
                        if (!moms.Contains(f.MaxBendingMoment.Force))
                            moms.Add(f.MaxBendingMoment.Force);
                    }
                }

                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                moms.Reverse();


                double dval = 0.0;

                int idx = frc.Count - 1;
                dval = frc[idx];
                txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 1;
                dval = moms[idx];
                txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(dval).ToString();




                idx = frc.Count - 4;
                dval = frc[idx];
                txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 6;
                dval = moms[idx];
                txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






                idx = frc.Count - frc.Count / 3;
                dval = frc[idx];
                txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 4;
                dval = moms[idx];
                txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





                idx = frc.Count - frc.Count / 2;
                dval = frc[idx];
                txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 8;
                dval = moms[idx];
                txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


                idx = frc.Count / 8;
                dval = frc[idx];
                txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 10;
                dval = moms[idx];
                txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                idx = 0;
                dval = frc[idx];
                txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(dval).ToString();



                idx = 0;
                dval = moms[idx];
                txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


                return;

                #endregion CC 2017 02 17
            }


            List<int> _joints = new List<int>();
            MaxForce force = new MaxForce();

            //Support
            _joints.Add(2);
            _joints.Add(12);


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



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[0] = force;


            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);






            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[1] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[1] = force;


            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[2] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[2] = force;


            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[3] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[3] = force;



            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);





            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[4] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[4] = force;



            //L/2
            _joints.Clear();
            _joints.Add(7);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[5] = force;



            _joints.Clear();


            Results.Add("");
            Results.Add("");
            Results.Add("SUPER IMPOSED DEAD LOAD");
            Results.Add("-----------------------");
            Results.Add("");
            Results.Add("");
            //Support
            _joints.Add(2);
            _joints.Add(12);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_support_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_support_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[0] = force;




            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[1] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[1] = force;



            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[2] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[2] = force;




            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[3] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[3] = force;

            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[4] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[4] = force;

            //L/2
            _joints.Clear();
            _joints.Add(7);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[5] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[5] = force;

            File.WriteAllLines(Result_Report_DL, Results.ToArray());

            //iApp.RunExe(Result_Report_DL);
        }

        void Show_Moment_Shear_DL(UC_Stage_Extradosed_LRFD uc)
        {

            //Show_Moment_Shear_DL((int)AnalysisType);
            //return;

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17




                List<double> frc = new List<double>();
                List<double> moms = new List<double>();
                int memNo = -1;
                for (int i = 0; i < Extradosed._Columns - 2; i++)
                {

                    var item = Extradosed.Long_Girder_Members_Array[2, i];

                    if (cmb_cable_type.SelectedIndex == 0)
                        memNo = Extradosed.Long_Girder_Members_Array[2, i].MemberNo;
                    else
                        memNo = Extradosed.Long_Girder_Members_Array[1, i].MemberNo;




                    var f = (AnalysisData)Extradosed.DeadLoad_Analysis.MemberAnalysis[item.MemberNo];
                    if (f != null)
                    {
                        if (!frc.Contains(f.MaxShearForce.Force))
                            frc.Add(f.MaxShearForce.Force);
                        if (!moms.Contains(f.MaxBendingMoment.Force))
                            moms.Add(f.MaxBendingMoment.Force);
                    }
                }

                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                moms.Reverse();


                double dval = 0.0;

                int idx = frc.Count - 1;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 1;
                dval = moms[idx];
                uc.txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(dval).ToString();




                idx = frc.Count - 4;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 6;
                dval = moms[idx];
                uc.txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






                idx = frc.Count - frc.Count / 3;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 4;
                dval = moms[idx];
                uc.txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





                idx = frc.Count - frc.Count / 2;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 8;
                dval = moms[idx];
                uc.txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


                idx = frc.Count / 8;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 10;
                dval = moms[idx];
                uc.txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                idx = 0;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(dval).ToString();



                idx = 0;
                dval = moms[idx];
                uc.txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


                return;

                #endregion CC 2017 02 17
            }


            List<int> _joints = new List<int>();
            MaxForce force = new MaxForce();

            //Support
            _joints.Add(2);
            _joints.Add(12);


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



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[0] = force;


            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);






            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[1] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[1] = force;


            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[2] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[2] = force;


            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[3] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[3] = force;



            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);





            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[4] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[4] = force;



            //L/2
            _joints.Clear();
            _joints.Add(7);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[5] = force;



            _joints.Clear();


            Results.Add("");
            Results.Add("");
            Results.Add("SUPER IMPOSED DEAD LOAD");
            Results.Add("-----------------------");
            Results.Add("");
            Results.Add("");
            //Support
            _joints.Add(2);
            _joints.Add(12);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_support_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_support_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[0] = force;




            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[1] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[1] = force;



            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[2] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[2] = force;




            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[3] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[3] = force;

            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[4] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[4] = force;

            //L/2
            _joints.Clear();
            _joints.Add(7);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[5] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[5] = force;

            File.WriteAllLines(Result_Report_DL, Results.ToArray());

            //iApp.RunExe(Result_Report_DL);
        }
        void Show_Moment_Shear_DL(int stage)
        {

            UC_Stage_Extradosed_LRFD uc = ucStage;

            UC_Stage_Extradosed_LRFD ucPrev = ucStage;

            if (stage == 2) ucPrev = uC_Stage_Extradosed_LRFD1;
            if (stage == 3) ucPrev = uC_Stage_Extradosed_LRFD2;
            if (stage == 4) ucPrev = uC_Stage_Extradosed_LRFD3;
            if (stage == 5) ucPrev = uC_Stage_Extradosed_LRFD4;


            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17


                List<double> frc = new List<double>();
                List<double> moms = new List<double>();
                Random snd = new Random(stage);
                double fact = 1.05 + (snd.NextDouble() / 10);

                if (stage == 1)
                {
                    frc.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_support_shear) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_support_moment) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_deff_shear) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_deff_moment) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_L8_shear) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_L8_moment) * fact);

                    frc.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_L4_shear) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_L4_moment) * fact);


                    frc.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_3L_8_shear) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_3L_8_moment) * fact);


                    frc.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_L2_shear) * fact);
                    moms.Add(MyList.StringToDouble(txt_Ana_dead_inner_long_L2_moment) * fact);

                }
                else
                {
                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_support_shear) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_support_moment) * fact);


                    //uc.txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(dval).ToString();
                    //uc.txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(dval).ToString();


                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_deff_shear) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_deff_moment) * fact);

                    //uc.txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(dval).ToString();
                    //uc.txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(dval).ToString();

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_L8_shear) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_L8_moment) * fact);

                    //uc.txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(dval).ToString();
                    //uc.txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(dval).ToString();


                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_L4_shear) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_L4_moment) * fact);

                    //uc.txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(dval).ToString();
                    //uc.txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(dval).ToString();

                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_3L_8_shear) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_3L_8_moment) * fact);

                    //uc.txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();
                    //uc.txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                    frc.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_L2_shear) * fact);
                    moms.Add(MyList.StringToDouble(ucPrev.txt_Ana_dead_inner_long_L2_moment) * fact);

                    //uc.txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(dval).ToString();
                    //uc.txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(dval).ToString();
                }





                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                frc.Reverse();


                double dval = 0.0;

                int idx = 0;
                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(dval).ToString("f2");

                dval = moms[idx++];
                uc.txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(dval).ToString("f2");


                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(dval).ToString("f2");

                dval = moms[idx++];
                uc.txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(dval).ToString("f2");

                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(dval).ToString("f2");

                dval = moms[idx++];
                uc.txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(dval).ToString("f2");

                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(dval).ToString("f2");


                dval = moms[idx++];
                uc.txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(dval).ToString("f2");


                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString("f2");


                dval = moms[idx++];
                uc.txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString("f2");


                dval = frc[idx];
                uc.txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(dval).ToString("f2");



                dval = moms[idx++];
                uc.txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(dval).ToString("f2");


                return;

                #endregion CC 2017 02 17
            }


            List<int> _joints = new List<int>();
            MaxForce force = new MaxForce();

            //Support
            _joints.Add(2);
            _joints.Add(12);


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



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[0] = force;


            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);






            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[1] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[1] = force;


            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[2] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[2] = force;


            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[3] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[3] = force;



            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);





            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[4] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[4] = force;



            //L/2
            _joints.Clear();
            _joints.Add(7);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[5] = force;



            _joints.Clear();


            Results.Add("");
            Results.Add("");
            Results.Add("SUPER IMPOSED DEAD LOAD");
            Results.Add("-----------------------");
            Results.Add("");
            Results.Add("");
            //Support
            _joints.Add(2);
            _joints.Add(12);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_support_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_support_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[0] = force;




            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[1] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[1] = force;



            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[2] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[2] = force;




            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[3] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[3] = force;

            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[4] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[4] = force;

            //L/2
            _joints.Clear();
            _joints.Add(7);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            uc.txt_Ana_live_outer_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            uc.txt_Ana_live_outer_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[5] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[5] = force;

            File.WriteAllLines(Result_Report_DL, Results.ToArray());

            //iApp.RunExe(Result_Report_DL);
        }
        public string stg_file
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT_SUMMARY.TXT");
            }
        }
        public string Result_Report
        {
            get
            {

                if (AnalysisType == eAnalysis.Stage1) return Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_1.TXT");
                else if (AnalysisType == eAnalysis.Stage2) return Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_2.TXT");
                else if (AnalysisType == eAnalysis.Stage3) return Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_3.TXT");
                else if (AnalysisType == eAnalysis.Stage4) return Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_4.TXT");
                else if (AnalysisType == eAnalysis.Stage5) return Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_5.TXT");

                return Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_PRIMARY.TXT");

            }
        }
        public string Result_Report_LL
        {
            get
            {
                if (AnalysisType == eAnalysis.Stage1) return Path.Combine(user_path, "ANALYSIS_RESULT_LL_STAGE_1.TXT");
                else if (AnalysisType == eAnalysis.Stage2) return Path.Combine(user_path, "ANALYSIS_RESULT_LL_STAGE_2.TXT");
                else if (AnalysisType == eAnalysis.Stage3) return Path.Combine(user_path, "ANALYSIS_RESULT_LL_STAGE_3.TXT");
                else if (AnalysisType == eAnalysis.Stage4) return Path.Combine(user_path, "ANALYSIS_RESULT_LL_STAGE_4.TXT");
                else if (AnalysisType == eAnalysis.Stage5) return Path.Combine(user_path, "ANALYSIS_RESULT_LL_STAGE_5.TXT");


                return Path.Combine(user_path, "ANALYSIS_RESULT_LL.TXT");
            }
        }
        public string Result_Report_DL
        {
            get
            {
                if (AnalysisType == eAnalysis.Stage1) return Path.Combine(user_path, "ANALYSIS_RESULT_DL_STAGE_1.TXT");
                else if (AnalysisType == eAnalysis.Stage2) return Path.Combine(user_path, "ANALYSIS_RESULT_DL_STAGE_2.TXT");
                else if (AnalysisType == eAnalysis.Stage3) return Path.Combine(user_path, "ANALYSIS_RESULT_DL_STAGE_3.TXT");
                else if (AnalysisType == eAnalysis.Stage4) return Path.Combine(user_path, "ANALYSIS_RESULT_DL_STAGE_4.TXT");
                else if (AnalysisType == eAnalysis.Stage5) return Path.Combine(user_path, "ANALYSIS_RESULT_DL_STAGE_5.TXT");

                return Path.Combine(user_path, "ANALYSIS_RESULT_DL.TXT");
            }
        }

        private void Show_Analysis_Result()
        {

            List<string> list = new List<string>();

            string frmt = "{0,-15} {1,12} {2,12}";

            if (AnalysisType == eAnalysis.Normal)
            {
                list.Clear();
                list.Add("");
                list.Add("ANALYSIS RESULTS FOR DEAD LOAD");
                list.Add("--------------------------------");
                list.Add("");

                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "SECTION", "MOMENT", "SHEAR"));
                list.Add(string.Format(frmt, "", "(Ton-m)", "(Ton)"));
                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "Support", txt_Ana_dead_inner_long_support_moment.Text, txt_Ana_dead_inner_long_support_shear.Text));
                list.Add(string.Format(frmt, "Deff", txt_Ana_dead_inner_long_deff_moment.Text, txt_Ana_dead_inner_long_deff_shear.Text));
                list.Add(string.Format(frmt, "L/8", txt_Ana_dead_inner_long_L8_moment.Text, txt_Ana_dead_inner_long_L8_shear.Text));
                list.Add(string.Format(frmt, "L/4", txt_Ana_dead_inner_long_L4_moment.Text, txt_Ana_dead_inner_long_L4_shear.Text));
                list.Add(string.Format(frmt, "3L/8", txt_Ana_dead_inner_long_3L_8_moment.Text, txt_Ana_dead_inner_long_3L_8_shear.Text));
                list.Add(string.Format(frmt, "L/2", txt_Ana_dead_inner_long_L2_moment.Text, txt_Ana_dead_inner_long_L2_shear.Text));
                list.Add("---------------------------------------------------------------------");

                list.Add("");
                list.Add("ANALYSIS RESULTS FOR LIVE LOAD");
                list.Add("--------------------------------");

                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "SECTION", "MOMENT", "SHEAR"));
                list.Add(string.Format(frmt, "", "(Ton-m)", "(Ton)"));
                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "Support", txt_Ana_live_inner_long_support_moment.Text, txt_Ana_live_inner_long_support_shear.Text));
                list.Add(string.Format(frmt, "Deff", txt_Ana_live_inner_long_deff_moment.Text, txt_Ana_live_inner_long_deff_shear.Text));
                list.Add(string.Format(frmt, "L/8", txt_Ana_live_inner_long_L8_moment.Text, txt_Ana_live_inner_long_L8_shear.Text));
                list.Add(string.Format(frmt, "L/4", txt_Ana_live_inner_long_L4_moment.Text, txt_Ana_live_inner_long_L4_shear.Text));
                list.Add(string.Format(frmt, "3L/8", txt_Ana_live_inner_long_3L_8_moment.Text, txt_Ana_live_inner_long_3L_8_shear.Text));
                list.Add(string.Format(frmt, "L/2", txt_Ana_live_inner_long_L2_moment.Text, txt_Ana_dead_inner_long_L2_shear.Text));
                list.Add("---------------------------------------------------------------------");

            }
            else
            {
                list.Clear();
                list.Add("");
                list.Add("ANALYSIS RESULTS FOR DEAD LOAD");
                list.Add("--------------------------------");
                list.Add("");
                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "SECTION", "MOMENT", "SHEAR"));
                list.Add(string.Format(frmt, "", "(Ton-m)", "(Ton)"));
                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "Support", ucStage.txt_Ana_dead_inner_long_support_moment.Text, ucStage.txt_Ana_dead_inner_long_support_shear.Text));
                list.Add(string.Format(frmt, "Deff", ucStage.txt_Ana_dead_inner_long_deff_moment.Text, ucStage.txt_Ana_dead_inner_long_deff_shear.Text));
                list.Add(string.Format(frmt, "L/8", ucStage.txt_Ana_dead_inner_long_L8_moment.Text, ucStage.txt_Ana_dead_inner_long_L8_shear.Text));
                list.Add(string.Format(frmt, "L/4", ucStage.txt_Ana_dead_inner_long_L4_moment.Text, ucStage.txt_Ana_dead_inner_long_L4_shear.Text));
                list.Add(string.Format(frmt, "3L/8", ucStage.txt_Ana_dead_inner_long_3L_8_moment.Text, ucStage.txt_Ana_dead_inner_long_3L_8_shear.Text));
                list.Add(string.Format(frmt, "L/2", ucStage.txt_Ana_dead_inner_long_L2_moment.Text, ucStage.txt_Ana_dead_inner_long_L2_shear.Text));
                list.Add("---------------------------------------------------------------------");

                list.Add("");
                list.Add("ANALYSIS RESULTS FOR LIVE LOAD");
                list.Add("--------------------------------");

                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "SECTION", "MOMENT", "SHEAR"));
                list.Add(string.Format(frmt, "", "(Ton-m)", "(Ton)"));
                list.Add("---------------------------------------------------------------------");
                list.Add(string.Format(frmt, "Support", ucStage.txt_Ana_live_inner_long_support_moment.Text, ucStage.txt_Ana_live_inner_long_support_shear.Text));
                list.Add(string.Format(frmt, "Deff", ucStage.txt_Ana_live_inner_long_deff_moment.Text, ucStage.txt_Ana_live_inner_long_deff_shear.Text));
                list.Add(string.Format(frmt, "L/8", ucStage.txt_Ana_live_inner_long_L8_moment.Text, ucStage.txt_Ana_live_inner_long_L8_shear.Text));
                list.Add(string.Format(frmt, "L/4", ucStage.txt_Ana_live_inner_long_L4_moment.Text, ucStage.txt_Ana_live_inner_long_L4_shear.Text));
                list.Add(string.Format(frmt, "3L/8", ucStage.txt_Ana_live_inner_long_3L_8_moment.Text, ucStage.txt_Ana_live_inner_long_3L_8_shear.Text));
                list.Add(string.Format(frmt, "L/2", ucStage.txt_Ana_live_inner_long_L2_moment.Text, ucStage.txt_Ana_dead_inner_long_L2_shear.Text));
                list.Add("---------------------------------------------------------------------");

            }


            File.WriteAllLines(Result_Report, list.ToArray());



            List<string> stg_files = new List<string>();


            stg_files.Add(Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_PRIMARY.TXT"));
            stg_files.Add(Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_1.TXT"));
            stg_files.Add(Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_2.TXT"));
            stg_files.Add(Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_3.TXT"));
            stg_files.Add(Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_4.TXT"));
            stg_files.Add(Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_5.TXT"));

            int stg = (int)AnalysisType;
            list.Clear();
            for (int i = 0; i <= stg; i++)
            {
                if (File.Exists(stg_files[i]))
                {
                    if (i == 0)
                    {
                        list.Add(string.Format(""));
                        list.Add(string.Format("------------------------------------------"));
                        list.Add(string.Format("PRIMARY ANALYSIS RESULTS"));
                        list.Add(string.Format("------------------------------------------"));
                        list.Add(string.Format(""));
                        list.AddRange(File.ReadAllLines(stg_files[i]));
                    }
                    else
                    {

                        list.Add(string.Format(""));
                        list.Add(string.Format("------------------------------------------"));
                        list.Add(string.Format("STAGE {0} ANALYSIS RESULTS", stg));
                        list.Add(string.Format("------------------------------------------"));
                        list.Add(string.Format(""));
                        list.AddRange(File.ReadAllLines(stg_files[i]));
                    }
                }
            }
            File.WriteAllLines(stg_file, list.ToArray());


        }




        void Write_Max_Moment_Shear()
        {
            List<string> list = new List<string>();
            list.Add(string.Format("LONG_INN_DEFF_SHR={0}", txt_Ana_DL_inner_long_deff_shear.Text));
            list.Add(string.Format("LONG_INN_L2_MOM={0}", txt_Ana_DL_inner_long_L2_moment.Text));


            list.Add(string.Format("LONG_OUT_DEFF_SHR={0}", txt_Ana_DL_outer_long_deff_shear.Text));
            list.Add(string.Format("LONG_OUT_L2_MOM={0}", txt_Ana_DL_outer_long_L2_moment.Text));

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
                if (cmb_cable_type.SelectedIndex == 2)
                    btn_Process_LL_Analysis.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
                else
                    btn_Process_LL_Analysis.Enabled = File.Exists(Extradosed.Input_File);
            }
            else
            {
                ucStage.btn_Process_LL_Analysis.Enabled = File.Exists(Extradosed.Input_File);
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

                Deck_Analysis_DL.Input_File = analysis_file;
                dl_file = Deck_Analysis_DL.Input_File;
                //dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
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
                Deck_Analysis_DL.Input_File = dl_file;
                //dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                //dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }
            }
            else
            {
                //wrkg_folder = Path.GetDirectoryName(analysis_file);
                //wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");


                //dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                //Deck_Analysis_DL.Input_File = dl_file;
                //Create_Data_Extradossed(Input_File);


                //if (File.Exists(dl_file))
                //{
                //    flag = true;
                //    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                //    Show_Moment_Shear_DL();
                //}

                //wrkg_folder = Path.GetDirectoryName(analysis_file);
                //wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");



                //ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");

                //if (!File.Exists(ll_file))
                //    ll_file = Get_Live_Load_Analysis_Input_File(1);

                //Deck_Analysis_LL.Input_File = ll_file;



                //if (File.Exists(ll_file))
                //{
                //    Deck_Analysis_LL.Bridge_Analysis = null;
                //    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                //    Show_Moment_Shear_LL();
                //}

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
                               // txt_Ana_skew_angle.Text = Deck_Analysis_LL.Skew_Angle.ToString();
                            }
                        }

                        txt_Ana_L1.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length.ToString();
                        txt_Ana_B.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();
                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();

                        txt_support_distance.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                       // txt_Ana_skew_angle.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Skew_Angle.ToString();

                        txt_gd_np.Text = (Deck_Analysis_LL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");
                    }
                    else
                    {
                        //txt_Ana_L1.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Length.ToString();
                        //txt_Ana_B.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width.ToString();
                        //txt_gd_np.Text = (Deck_Analysis_DL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");

                        //txt_support_distance.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        //txt_Ana_DL_eff_depth.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        //txt_Ana_width_cantilever.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        //txt_Ana_analysis_file.Visible = true;
                        //txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");

                        //Extradosed.DeadLoad_Analysis.Analysis.Length
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
                load_lst.Add("LOAD GENERATION " + txt_IRC_LL_load_gen.Text);
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

        private void frm_Extradosed_AASHTO_Load(object sender, EventArgs e)
        {



            uC_Stage_Extradosed_LRFD1.txt_cable_pcnt.Text = "90";
            uC_Stage_Extradosed_LRFD2.txt_cable_pcnt.Text = "80";
            uC_Stage_Extradosed_LRFD3.txt_cable_pcnt.Text = "70";
            uC_Stage_Extradosed_LRFD4.txt_cable_pcnt.Text = "60";
            uC_Stage_Extradosed_LRFD5.txt_cable_pcnt.Text = "50";


            uC_Stage_Extradosed_LRFD1.txt_steel_pcnt.Text = "90";
            uC_Stage_Extradosed_LRFD2.txt_steel_pcnt.Text = "80";
            uC_Stage_Extradosed_LRFD3.txt_steel_pcnt.Text = "70";
            uC_Stage_Extradosed_LRFD4.txt_steel_pcnt.Text = "60";
            uC_Stage_Extradosed_LRFD5.txt_steel_pcnt.Text = "50";

            uC_Stage_Extradosed_LRFD1.txt_conc_pcnt.Text = "90";
            uC_Stage_Extradosed_LRFD2.txt_conc_pcnt.Text = "80";
            uC_Stage_Extradosed_LRFD3.txt_conc_pcnt.Text = "70";
            uC_Stage_Extradosed_LRFD4.txt_conc_pcnt.Text = "60";
            uC_Stage_Extradosed_LRFD5.txt_conc_pcnt.Text = "50";


            cmb_long_open_file.Items.Clear();
            cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
            cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));


            //txt_Ana_B.Text = "9.750";

            Section2_Section_Properties();

            Deck_Analysis_DL = new PSC_BoxGirderAnalysis(iApp);
            Deck_Analysis_LL = new PSC_BoxGirderAnalysis(iApp);

            Extradosed = new CABLE_STAYED_Extradosed_LS_Analysis_AASHTO(iApp);

            LongGirder = new PostTensionLongGirder(iApp);
            Deck = new RccDeckSlab(iApp);

            Button_Enable_Disable();
            Load_Tab2_Tab3_Box_Segment_Data();
            Update_Tab3_Data();


            //Chiranjit [2017 02 20]
            if (Project_Type == eASTRADesignType.Extradossed_Central_Towers_Bridge_LS)
                cmb_cable_type.SelectedIndex = 1;
            else
                cmb_cable_type.SelectedIndex = 0;

            Set_Project_Name();


            cmb_design_stage.SelectedIndex = 0;



            Default_LRFD_Moving_LoadData(dgv_long_liveloads);
            Default_LRFD_Moving_Type_LoadData(dgv_long_loads);



            #region Initialise default input data

            AASHTO_Design_PC_Box_Girder.Input_PSC_Box_Girder_Data(dgv_steel_girder_input_data);
            AASHTO_Design_PC_Box_Girder.Input_Abutment_Data(dgv_abutment_input_data);
            AASHTO_Design_PC_Box_Girder.Input_Pier_Data(dgv_pier_input_data);
            AASHTO_Design_PC_Box_Girder.Input_Bearing_Data(dgv_bearing_input_data);

            #endregion Initialise default input data


            tc_stage.TabPages.Remove(tab_designSage);
            tbc_girder.TabPages.Add(tab_designSage);
            Select_Moving_Load_Combo(dgv_long_loads, cmb_irc_view_moving_load);
            MovingLoad_Increment();

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
        void Cable_Structure_Load()
        {

        }
        private void Open_Project()
        {
            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {

                IsCreate_Data = false;

                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                Segment_Girder.FilePath = user_path;
                Show_Section_Result();

                Analysis_Initialize_InputData();


                Set_Segment_Data();

                Create_Data_Extradossed(Input_File);

                IsRead = true;
                iApp.Read_Form_Record(this, user_path);
                IsRead = false;

                Set_Box_Forces();
                Set_Segment_Data();
                Segment_Girder.FilePath = user_path;

                txt_Ana_analysis_file.Text = chk_file;


                if (iApp.IsDemo)
                    MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

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

        private void btn_dwg_main_girder_Click(object sender, EventArgs e)
        {

            iApp.SetDrawingFile_Path(LongGirder.user_drawing_file, "PreStressed_Main_Girder", "");
        }

        private void btn_dwg_rcc_deck_Click(object sender, EventArgs e)
        {

        }

        //Chiranjit [2012 10 30]
        #region Design of RCC Pier

        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rcc_pier.rep_file_name);

        }
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder_Pier";
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), draw_cmd);

        }

        void Text_Changed()
        {

            double len = L1;
            lbl_max_delf.Text = string.Format("Span / 800 = {0}/800 = {1}m", len, len / 800);
            uC_Stage_Extradosed_LRFD1.lbl_max_delf.Text = lbl_max_delf.Text;
            uC_Stage_Extradosed_LRFD2.lbl_max_delf.Text = lbl_max_delf.Text;
            uC_Stage_Extradosed_LRFD3.lbl_max_delf.Text = lbl_max_delf.Text;
            uC_Stage_Extradosed_LRFD4.lbl_max_delf.Text = lbl_max_delf.Text;
            uC_Stage_Extradosed_LRFD5.lbl_max_delf.Text = lbl_max_delf.Text;

            txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");
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
                    //iApp.OpenExcelFile(excel_file, "2011ap");
                    //iApp.OpenExcelFile(
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder";
            //iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), draw_cmd);

            Button b = sender as Button;

            string draw = Drawing_Folder;



            //eOpenDrawingOption opt = iApp.Open_Drawing_Option();
            //if (opt == eOpenDrawingOption.Cancel) return;

            eOpenDrawingOption opt = eOpenDrawingOption.Sample_Drawings;

            string excel_file = "PSC Box Girder\\" + "New Design Of PSC Box Girder.xls";
            //excel_file = Path.Combine(excel_path, excel_file);

            string copy_path = Path.Combine(Worksheet_Folder, excel_file);


            if (opt == eOpenDrawingOption.Design_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");

                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");


                    iApp.Form_Drawing_Editor(eBaseDrawings.PSC_BOX_Girder_GAD, Title, Drawing_Folder, copy_path).ShowDialog();

                }
                //if (b.Name == btn_dwg_open_consts.Name)
                //{
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                //}
                //if (b.Name == btn_dwg_rcc_abut.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                //}
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                }
                //else if (b.Name == btn_dwg_open_Deckslab.Name)
                //{
                //    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                //}
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
                if (b.Name == btn_construction_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Editable Construction Drawings"), "EXTRADOSSED_BRIDGE");
                }
                if (b.Name == btn_open_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), "PSC_Box_Girder");
                }
                if (b.Name == btn_cable_stayed_drawing.Name)
                {
                    iApp.RunViewer(Drawing_Folder, "Cable_Stayed_Bridge");
                }
                //if (b.Name == btn_dwg_open_consts.Name)
                //{
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                //}
                //if (b.Name == btn_dwg_rcc_abut.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                //}
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                //else if (b.Name == btn_dwg_open_Deckslab.Name)
                //{
                //    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                //}
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
            rft.M2 = chk_M2.Checked;
            rft.M3 = chk_M3.Checked;
            rft.R3 = chk_R3.Checked;
            rft.R2 = chk_R2.Checked;
            return rft;
        }

        private void btn_update_force_Click(object sender, EventArgs e)
        {
            string ana_rep_file = File.Exists(Deck_Analysis_LL.Analysis_Report) ? Deck_Analysis_LL.Analysis_Report : Analysis_Report_LL;
            //string ana_rep_file = Deck_Analysis_LL.Analysis_Report;
            iApp.Progress_ON("Read forces...");
            iApp.SetProgressValue(9, 100);
            if (File.Exists(ana_rep_file))
            {
                //Deck_Analysis_LL.Truss_Analysis = null;
                //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_LL.Bridge_Analysis.ForceType = GetForceType();
                iApp.SetProgressValue(19, 100);
                Show_Moment_Shear_LL();
                iApp.SetProgressValue(29, 100);

                grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
                grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

                grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
                grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

                Button_Enable_Disable();
                Show_Analysis_Result();
                iApp.SetProgressValue(49, 100);
            }
            //else
            //    MessageBox.Show("Analysis Result not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



            ana_rep_file = File.Exists(Deck_Analysis_DL.Analysis_Report) ? Deck_Analysis_DL.Analysis_Report : Analysis_Report_DL;
            iApp.SetProgressValue(59, 100);
            if (File.Exists(ana_rep_file))
            {
                //Deck_Analysis_DL.Truss_Analysis = null;
                //Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_DL.Bridge_Analysis.ForceType = GetForceType();
                iApp.SetProgressValue(69, 100);
                Show_Moment_Shear_DL();
            }
            iApp.SetProgressValue(89, 100);

            grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            Button_Enable_Disable();
            Show_Analysis_Result();
            iApp.SetProgressValue(99, 100);
            iApp.Progress_OFF();
        }
        #endregion
        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L1.Text = "0";
                txt_Ana_L1.Text = "100.0";
                txt_Ana_B.Text = "15.6";
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
                LongGirder.FilePath = user_path;
                Deck.FilePath = user_path;
                rcc_pier.FilePath = user_path;
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


        double Get_Max_Vehicle_Length()
        {
            double mvl = 13.4;

            List<double> lst_mvl = new List<double>();
            DataGridView dgv = dgv_long_liveloads;

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



            double veh_len, veh_gap, train_length;

            veh_len = mvl;
            veh_gap = mvl;
            //train_length = veh_len;
            train_length = 0.0;
            double eff = L;
            bool fl = false;
            while (train_length <= eff)
            {
                fl = !fl;
                if (fl)
                {
                    train_length += veh_gap;
                    //if (train_length > L)
                    //{
                    //    train_length = train_length - veh_gap;
                    //}
                }
                else
                {
                    train_length += veh_len;
                }
            }



            //return mvl;

            return train_length;


        }


        private void txt_Ana_DL_length_TextChanged(object sender, EventArgs e)
        {
            MovingLoad_Increment();
            ChangeData();
        }
        void ChangeData()
        {


            double exp = MyList.StringToDouble(txt_exp_gap);
            double ovg = MyList.StringToDouble(txt_overhang_gap);
            txt_support_distance.Text = (exp / 2 + ovg).ToString("f4");



            //Steel Girder

            dgv_steel_girder_input_data[2, 1].Value = txt_L2.Text;
            dgv_steel_girder_input_data[2, 1].Style.ForeColor = Color.Red;

			dgv_steel_girder_input_data[2, 2].Value = txt_Ana_L1.Text;
			dgv_steel_girder_input_data[2, 2].Style.ForeColor = Color.Red;

			
			dgv_steel_girder_input_data[2, 3].Value = txt_Ana_B.Text;
			dgv_steel_girder_input_data[2, 3].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 4].Value = txt_Ana_Road_Width.Text;
			dgv_steel_girder_input_data[2, 4].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 5].Value = txt_Ana_Superstructure_depth.Text;
			dgv_steel_girder_input_data[2, 5].Style.ForeColor = Color.Red;
			
			
			dgv_steel_girder_input_data[2, 6].Value = txt_Ana_Web_Spacing.Text;
			dgv_steel_girder_input_data[2, 6].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 7].Value = txt_Ana_Web_Thickness.Text;
			dgv_steel_girder_input_data[2, 7].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 8].Value = txt_Ana_Top_Slab_Thickness.Text;
			dgv_steel_girder_input_data[2, 8].Style.ForeColor = Color.Red;

			
			dgv_steel_girder_input_data[2, 9].Value = txt_Ana_Bottom_Slab_Thickness.Text;
			dgv_steel_girder_input_data[2, 9].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 10].Value = txt_Ana_Deck_Overhang.Text;
			dgv_steel_girder_input_data[2, 10].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 19].Value = txt_Ana_Deckslab_Thickness.Text;
			dgv_steel_girder_input_data[2, 19].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 20].Value = txt_Ana_Deck_Top_Cover.Text;
			dgv_steel_girder_input_data[2, 20].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 21].Value = txt_Ana_Deck_Bottom_Cover.Text;
			dgv_steel_girder_input_data[2, 21].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 22].Value = txt_Ana_Deck_Wearing_Surface.Text;
			dgv_steel_girder_input_data[2, 22].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2,29].Value = txt_Ana_Strand_Area.Text;
			dgv_steel_girder_input_data[2, 29].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2,30].Value = txt_Ana_Strand_Fpu.Text;
			dgv_steel_girder_input_data[2, 30].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 31].Value = txt_Ana_Strand_Fpy.Text;
			dgv_steel_girder_input_data[2, 31].Style.ForeColor = Color.Red;
		
			dgv_steel_girder_input_data[2, 32].Value = txt_Ana_Strand_Ep.Text;
			dgv_steel_girder_input_data[2, 32].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 33].Value = txt_Ana_Concrete_Ec.Text;
			dgv_steel_girder_input_data[2, 33].Style.ForeColor = Color.Red;
			
			dgv_steel_girder_input_data[2, 34].Value = txt_Ana_Concrete_DL_Calculation.Text;
			dgv_steel_girder_input_data[2, 34].Style.ForeColor = Color.Red;
			
			
//Abutment			
			dgv_abutment_input_data[2, 1].Value = txt_Ana_Superstructure_depth.Text;
			dgv_abutment_input_data[2, 1].Style.ForeColor = Color.Red;
			
			
			dgv_abutment_input_data[2, 3].Value = txt_Ana_Deckslab_Thickness.Text;
			dgv_abutment_input_data[2, 3].Style.ForeColor = Color.Red;
			
			
			
//Pier			
			dgv_pier_input_data[2, 8].Value = txt_Ana_Deckslab_Thickness.Text;
			dgv_pier_input_data[2, 8].Style.ForeColor = Color.Red;
			
			dgv_pier_input_data[2, 10].Value = txt_Ana_Superstructure_depth.Text;
			dgv_pier_input_data[2, 10].Style.ForeColor = Color.Red;




        }
        private void MovingLoad_Increment()
        {
            txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");


            try
            {

                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    //dgv_seg_tab3_1[i, 0].Value = txt_Ana_DL_eff_depth.Text;
                    dgv_seg_tab3_1[i, 1].Value = txt_Ana_B.Text;
                    dgv_seg_tab3_1[i, 3].Value = txt_Ana_width_cantilever.Text;



                    //dgv_seg_tab3_1[i, 0].Value = txt_Ana_DL_eff_depth.Text;
                    dgv_seg_tab3_1[i, 10].Value = (MyList.StringToDouble(txt_Ana_B.Text) * (6.75 / 9.75)).ToString("f3");
                    dgv_seg_tab3_1[i, 17].Value = (MyList.StringToDouble(txt_Ana_B.Text) * (1.85 / 9.75)).ToString("f3");

                }
            }
            catch (Exception ex) { }

            Text_Changed();
            ChangeData();
        }

        public void Load_Tab2_Tab3_Box_Segment_Data()
        {

            List<string> list = new List<string>();
            //if (true)
            //{
            //    list.Add(string.Format("D$4.5$4.0$3.5$3.0$2.5$2.0"));
            //    list.Add(string.Format("Dw$15.6$15.6$15.6$15.6$15.6$15.6"));
            //    list.Add(string.Format("Td$0.275$0.275$0.275$0.275$0.275$0.275"));
            //    list.Add(string.Format("C1$1.925$1.925$1.925$1.925$1.925$1.925"));
            //    list.Add(string.Format("C2$0$0$0$0$0$0"));
            //    list.Add(string.Format("Tip$0.2$0.2$0.2$0.2$0.2$0.2"));
            //    list.Add(string.Format("Tf$0.3$0.3$0.3$0.3$0.3$0.3"));
            //    list.Add(string.Format("Iw$0.7$0.7$0.7$0.7$0.7$0.7"));
            //    list.Add(string.Format("D1$3.45$3.45$3.45$3.45$3.45$3.45"));
            //    list.Add(string.Format("Tw$0.35$0.35$0.35$0.35$0.35$0.35"));
            //    list.Add(string.Format("SW$6.5$6.5$6.5$6.5$6.5$6.5"));
            //    list.Add(string.Format("Ts$0.275$0.275$0.275$0.275$0.275$0.275"));
            //    list.Add(string.Format("D2$0.250$0.250$0.250$0.250$0.250$0.250"));
            //    list.Add(string.Format("K1$0.051$0.051$0.051$0.051$0.051$0.051"));
            //    list.Add(string.Format("K2$0.056$0.056$0.056$0.056$0.056$0.056"));
            //    list.Add(string.Format("HW1$0.409$0.485$0.582$0.75$0.75$0.75"));
            //    list.Add(string.Format("HH1$0.083$0.097$0.116$0.15$0.15$0.15"));
            //    list.Add(string.Format("CH1$1.85$1.85$1.85$1.85$1.85$1.85"));
            //    list.Add(string.Format("HW2$0$0$0$0$0$0"));
            //    list.Add(string.Format("HH2$0$0$0$0$0$0"));
            //    list.Add(string.Format("HW3$0.75$0.75$0.75$0.75$0.75$0.75"));
            //    list.Add(string.Format("HH3$0.25$0.25$0.25$0.25$0.25$0.15"));
            //}

            #region AASHTO DATA
            list.Add(string.Format("D$5.500$5.500$5.500$0.208$5.500$5.500"));
            list.Add(string.Format("Dw$45.167$45.167$45.167$45.167$45.167$45.167"));
            list.Add(string.Format("Td$0.667$0.667$0.667$0.667$0.667$0.667"));
            list.Add(string.Format("C1$2.625$2.625$2.625$2.625$2.625$2.625"));
            list.Add(string.Format("C2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("Tip$0.750$0.750$0.750$0.750$0.750$0.750"));
            list.Add(string.Format("Tf$1.000$1.000$1.000$1.000$1.000$1.000"));
            list.Add(string.Format("Iw$1.792$1.792$1.792$1.792$1.792$1.792"));
            list.Add(string.Format("D1$4.000$4.000$4.000$4.000$4.000$4.000"));
            list.Add(string.Format("Tw$1.000$1.000$1.000$1.000$1.000$1.000"));
            list.Add(string.Format("SW$36.333$36.333$36.333$36.333$36.333$36.333"));
            list.Add(string.Format("Ts$0.500$0.500$0.500$0.500$0.500$0.500"));
            list.Add(string.Format("D2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("K1$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("K2$0.015$0.007$0.007$0.007$0.007$0.007"));
            list.Add(string.Format("HW1$0.333$0.333$0.333$0.333$0.333$0.333"));
            list.Add(string.Format("HH1$0.333$0.333$0.333$0.333$0.333$0.333"));
            list.Add(string.Format("CH1$19.375$19.375$19.375$19.375$19.375$19.375"));
            list.Add(string.Format("HW2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("HH2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("HW3$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("HH3$0.000$0.000$0.000$0.000$0.000$0.000"));
            #endregion AASHTO DATA


            MyList mlist = null;


            for (int i = 0; i < list.Count; i++)
            {
                //mlist = new MyList(list[i], ' ');
                if (list[i].Contains("$"))
                    mlist = new MyList(list[i], '$');
                else
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

            Segment_Girder.L_Deff = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


            #endregion Variable Declaration
        }

        private void btn_segment_process_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            //Segment_Girder.FilePath = user_path;
            //Segment_Girder_Initialize_Data();
            //Set_Box_Forces();

            //Box_Forces.Set_Absolute();
            //Segment_Girder.Calculate_Program(Segment_Girder.rep_file_name, Box_Forces);
            //iApp.Save_Form_Record(this, user_path);
            //if (File.Exists(Segment_Girder.rep_file_name))
            //{
            //    MessageBox.Show(this, "Report file written in " + Segment_Girder.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    frm_BoxGirder_Msg.Msg_Showw();
            //    iApp.View_Result(Segment_Girder.rep_file_name, true);
            //}
            Segment_Girder.is_process = true;
            Button_Enable_Disable();
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
                }

            }
            catch (Exception ex) { }
        }

        private void btn_Show_Section_Result_Click(object sender, EventArgs e)
        {
            Show_Section_Result();
        }

        private void Show_Section_Result()
        {

            //Segment_Girder.FilePath = user_path;
            Segment_Girder_Initialize_Data();
            rtb_sections.Lines = Segment_Girder.Get_Step_1(ref PSC_SECIONS).ToArray();

            if(rbtn_single_cell.Checked)
            {


                double a = (MyList.Get_Array_Sum(PSC_SECIONS.Area) / PSC_SECIONS.Area.Count);
                double ix = (MyList.Get_Array_Sum(PSC_SECIONS.Ixx) / PSC_SECIONS.Area.Count);
                double iy = (MyList.Get_Array_Sum(PSC_SECIONS.Iyy) / PSC_SECIONS.Area.Count);
                double iz = (MyList.Get_Array_Sum(PSC_SECIONS.Izz) / PSC_SECIONS.Area.Count);
                txt_tot_AX.Text = a.ToString("f3");
                txt_tot_IXX.Text = ix.ToString("f3");
                txt_tot_IYY.Text = iy.ToString("f3");
                txt_tot_IZZ.Text = (ix + iy).ToString("f3");
            }
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
            Text_Changed_Forces();
        }


        private void cmb_tab1_Fcu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Concrete_Grade();
        }

        private void Set_Concrete_Grade()
        {

        }


        #region Chiranjit [2014 03 12] Support Input
        public string Start_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (AnalysisType == eAnalysis.Normal)
                {
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
                }
                else
                {
                    if (ucStage.rbtn_ssprt_pinned.Checked)
                        kStr = "PINNED";
                    else if (ucStage.rbtn_ssprt_fixed.Checked)
                    {
                        kStr = "FIXED";


                        if (!ucStage.chk_ssprt_fixed_FX.Checked
                            || !ucStage.chk_ssprt_fixed_FY.Checked
                            || !ucStage.chk_ssprt_fixed_FZ.Checked
                            || !ucStage.chk_ssprt_fixed_MX.Checked
                            || !ucStage.chk_ssprt_fixed_MY.Checked
                            || !ucStage.chk_ssprt_fixed_MZ.Checked)
                            kStr += " BUT";

                        if (!ucStage.chk_ssprt_fixed_FX.Checked) kStr += " FX";
                        if (!ucStage.chk_ssprt_fixed_FY.Checked) kStr += " FY";
                        if (!ucStage.chk_ssprt_fixed_FZ.Checked) kStr += " FZ";
                        if (!ucStage.chk_ssprt_fixed_MX.Checked) kStr += " MX";
                        if (!ucStage.chk_ssprt_fixed_MY.Checked) kStr += " MY";
                        if (!ucStage.chk_ssprt_fixed_MZ.Checked) kStr += " MZ";
                    }
                }
                return kStr;
            }
        }
        public string END_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (AnalysisType == eAnalysis.Normal)
                {
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
                }
                else
                {
                    if (ucStage.rbtn_esprt_pinned.Checked)
                        kStr = "PINNED";
                    else if (ucStage.rbtn_esprt_fixed.Checked)
                    {
                        kStr = "FIXED";
                        if (!ucStage.chk_esprt_fixed_FX.Checked
                            || !ucStage.chk_esprt_fixed_FY.Checked
                            || !ucStage.chk_esprt_fixed_FZ.Checked
                            || !ucStage.chk_esprt_fixed_MX.Checked
                            || !ucStage.chk_esprt_fixed_MY.Checked
                            || !ucStage.chk_esprt_fixed_MZ.Checked)
                            kStr += " BUT";
                        if (!ucStage.chk_esprt_fixed_FX.Checked) kStr += " FX";
                        if (!ucStage.chk_esprt_fixed_FY.Checked) kStr += " FY";
                        if (!ucStage.chk_esprt_fixed_FZ.Checked) kStr += " FZ";
                        if (!ucStage.chk_esprt_fixed_MX.Checked) kStr += " MX";
                        if (!ucStage.chk_esprt_fixed_MY.Checked) kStr += " MY";
                        if (!ucStage.chk_esprt_fixed_MZ.Checked) kStr += " MZ";
                    }
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
        #endregion Chiranjit [2014 03 12] Support Input

        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {
            iApp.Show_LL_Dialog();
        }



        #region Chiranjit [2014 09 10]
        #region British Standard Loading
        private void txt_deck_width_TextChanged(object sender, EventArgs e)
        {
        }
        public bool IsRead = false;

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();


        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Set File Name

            string file_name = "";
            if (Extradosed != null)
            {

                if (cmb_cable_type.SelectedIndex == 2)
                {

                    if (cmb_long_open_file.SelectedIndex == 0)
                        file_name = Deck_Analysis_DL.Input_File;
                    else if (cmb_long_open_file.SelectedIndex == 1)
                        file_name = Deck_Analysis_LL.Input_File;
                }
                else
                {
                    if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count)
                    {
                        if (cmb_long_open_file.SelectedIndex == 0)
                            file_name = Extradosed.DeadLoadAnalysis_Input_File;
                        else
                            file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                    }
                    else
                    {
                        file_name = Result_Report_LL;
                    }
                }
            }

            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_view_postProcess.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
            btn_view_preProcess.Enabled = File.Exists(file_name);

            btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));

        }

        #endregion British Standard Loading

        #endregion Chiranjit [2014 09 10]

        public string Get_Live_Load_Analysis_Input_File(int analysis_no)
        {


            string working_folder = Path.GetDirectoryName(Get_Input_File((int)AnalysisType));

            ////if (AnalysisType != eAnalysis.Normal)
            ////{
            ////    working_folder = Path.Combine(working_folder, "STAGE " + (int)AnalysisType);
            ////}
            ////if (!Directory.Exists(working_folder)) Directory.CreateDirectory(working_folder);

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
            string working_folder = Path.GetDirectoryName(Get_Input_File(stage)); ;

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
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
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
                    foreach (var item in txt_Ana_LL_member_load.Lines)
                    {

                        if (item == "") continue;
                        if (item.ToUpper().StartsWith("LOAD"))
                        {
                            if (fl == false)
                            {
                                fl = true;
                                load_lst.Add(item);
                            }
                            else
                                load_lst.Add("*" + item);
                        }
                        else
                        {
                            if (!load_lst.Contains(item))
                                load_lst.Add(item);
                            else
                                load_lst.Add("*" + item);
                        }
                    }

                }
                else
                    load_lst.AddRange(txt_Ana_LL_member_load.Lines);
            }
            else
            {
            }

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

            }
            if (indx != -1) inp_file_cont.InsertRange(indx, load_lst);
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
        }

        void Ana_Write_Cable_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
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

                if (AnalysisType == eAnalysis.Normal)
                {
                    if (chk_selfweight.Checked)
                        load_lst.Add("SELFWEIGHT Y -1.0");
                }
                else
                {
                    if (ucStage.chk_selfweight.Checked)
                        load_lst.Add("SELFWEIGHT Y -1.0");
                }
                if (add_LiveLoad)
                {
                    foreach (var item in txt_Ana_LL_member_load.Lines)
                    {
                        if (item == "") continue;
                        if (item.ToUpper().StartsWith("LOAD"))
                        {
                            if (fl == false)
                            {
                                fl = true;
                                load_lst.Add(item);
                            }
                            else
                                load_lst.Add("*" + item);
                        }
                        else
                        {
                            if (!load_lst.Contains(item))
                                load_lst.Add(item);
                            else
                                load_lst.Add("*" + item);
                        }
                    }

                }
                else
                    load_lst.AddRange(txt_Ana_LL_member_load.Lines);

            }
            else
            {
            }

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

            }
            inp_file_cont.InsertRange(indx, load_lst);
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
        }

        string Get_Long_Girder_File(int index)
        {

            string file_name = "";


            if (cmb_cable_type.SelectedIndex == 2)
            {

                if (index == 0)
                    file_name = Deck_Analysis_DL.Input_File;
                else if (index == 1)
                    file_name = Deck_Analysis_LL.Input_File;
            }
            else
            {
                #region Set File Name

                int flCnt = cmb_long_open_file.Items.Count;

                if (index < cmb_long_open_file.Items.Count)
                {
                    if (index == 0)
                        file_name = Extradosed.DeadLoadAnalysis_Input_File;
                    else if (index == cmb_long_open_file.Items.Count - 1)
                        file_name = Extradosed.TotalAnalysis_Input_File;
                    else
                        file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                }
                else
                {
                    file_name = Result_Report_LL;
                }
                #endregion Set File Name
            }
            return file_name;
        }

        string Get_Long_Girder_File(int index, int stage)
        {

            string file_name = Get_Input_File(stage);

            Extradosed.Input_File = file_name;

            if (cmb_cable_type.SelectedIndex == 2)
            {

                if (index == 0)
                    file_name = Deck_Analysis_DL.Input_File;
                else if (index == 1)
                    file_name = Deck_Analysis_LL.Input_File;
            }
            else
            {
                #region Set File Name
                if (index < ucStage.cmb_long_open_file.Items.Count)
                {
                    if (index == 0)
                        file_name = Extradosed.DeadLoadAnalysis_Input_File;
                    else if (index == ucStage.cmb_long_open_file.Items.Count - 1)
                        file_name = Extradosed.TotalAnalysis_Input_File;
                    else
                    {

                        file_name = Get_Live_Load_Analysis_Input_File(index, stage);
                    }
                }
                else
                {
                    file_name = Extradosed.TotalAnalysis_Input_File;
                }
                #endregion Set File Name
            }
            return file_name;
        }

        private void btn_view_data_Click(object sender, EventArgs e)
        {
            string file_name = "";
            string ll_txt = "";
            int stg = (int)AnalysisType;
            Button btn = sender as Button;

            file_name = Get_Long_Girder_File(cmb_long_open_file.SelectedIndex, stg);
            if (cmb_cable_type.SelectedIndex == 2)
            {

                if (cmb_long_open_file.SelectedIndex == 0)
                    file_name = Deck_Analysis_DL.Input_File;
                else if (cmb_long_open_file.SelectedIndex == 1)
                    file_name = Deck_Analysis_LL.Input_File;
            }
            else
            {

                file_name = Get_Long_Girder_File(cmb_long_open_file.SelectedIndex, stg);

            }
            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name)
            {
                iApp.View_Input_File(file_name);
            }
            else if (btn.Name == btn_view_preProcess.Name)
            {
                if (File.Exists(file_name))
                {
                    iApp.View_PreProcess(file_name);
                }
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.RunExe(file_name);

                if (File.Exists(stg_file))
                {
                    iApp.Open_TextFile(stg_file);
                }
            }
            else if (btn.Name == btn_view_postProcess.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                {
                    iApp.View_PostProcess(file_name);
                }
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
                    iApp.Read_Form_Record(this, user_path);

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
                    iApp.user_path = user_path;
                    txt_project_name.Text = Path.GetFileName(user_path);

                    //Write_All_Data();

                    #endregion Save As

                }
            }
            else if (btn.Name == btn_psc_new_design.Name)
            {
                IsCreate_Data = true;
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }
        #region Chiranjit [2016 09 07
        public void All_Button_Enable(bool flag)
        {

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
        eASTRADesignType Project_Type { get; set; }
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

        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                return iApp.Tables.USCS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                return iApp.Tables.USCS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                return iApp.Tables.USCS_SteelAngles;
            }
        }

        public double Cable_D
        {
            get { return MyList.StringToDouble(txt_cable_dia.Text, 0.0); }
            set { txt_cable_dia.Text = value.ToString("E3"); }
        }
        public double Cable_Ax
        {
            get { return (Math.PI * Cable_D * Cable_D) / 4.0; }
        }
        public double Cable_Gamma
        {
            get { return MyList.StringToDouble(txt_cbl_des_gamma.Text, 0.0); }
            set { txt_cbl_des_gamma.Text = value.ToString("E3"); }
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

            mem.CalculatedStress = Fx / Ax;
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
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add("");
            list.Add("");

            mem.DesignResult.Clear();
            mem.DesignResult.AddRange(list.ToArray());

        }

        public void Read_Angle_Sections(ComboBox cmb)
        {
            if (tbl_rolledSteelAngles.List_Table.Count > 0)
            {
                cmb.Items.Clear();
                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }
            }
        }
        #endregion


        public eDesignStandard DesignStandard { get { return iApp.DesignStandard; } }

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
                                //txt = string.Format("{0} {1:f3}", long_ll_types[f], imp_fact);
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
        #region LRFD Loading
        public void Default_LRFD_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                list.Add(string.Format("TYPE 1, LRFD_HL_93"));
                list.Add(string.Format("AXLE LOAD IN KIP,8.0,32.0,32.0 "));
                list.Add(string.Format("AXLE SPACING IN FT,14.1,29.52"));
                list.Add(string.Format("AXLE WIDTH IN FT,5.9"));
                list.Add(string.Format("IMPACT FACTOR,1.10"));
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

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
            dgv_live_load.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_live_load.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void Default_LRFD_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            list.Clear();

            #region Long Girder
            list.Add(string.Format("LOAD 1,TYPE 1"));
            list.Add(string.Format("X,0"));
            list.Add(string.Format("Z,6.5"));
            list.Add(string.Format(""));
            list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1,"));
            list.Add(string.Format("X,0,0,"));
            list.Add(string.Format("Z,6.5,16.5,"));
            list.Add(string.Format(""));
            #endregion

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


            dgv_live_load.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_live_load.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        #endregion LRFD LOading

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

                Text_Changed();
                LONG_GIRDER_LL_TXT();

                if (cmb_cable_type.SelectedIndex == 2)
                {
                    Create_Data_LL(Input_File_LL);
                    Create_Data_DL(Input_File_DL);
                }
                else
                {
                    Create_Data_Extradossed(Input_File);

                    string ll_file = Extradosed.Get_LL_Analysis_Input_File(cmb_irc_view_moving_load.SelectedIndex + 1);
                    iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_irc_vehicle_gap.Text));
                }

            }
            catch (Exception ex) { }
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

                LONG_GIRDER_LL_TXT();

                if (cmb_cable_type.SelectedIndex == 2)
                {
                    Create_Data_LL(Input_File_LL);
                    Create_Data_DL(Input_File_DL);
                }
                else
                {
                    Create_Data_Extradossed(Input_File);

                    string ll_file = Extradosed.Get_LL_Analysis_Input_File(cmb_irc_view_moving_load.SelectedIndex + 1);
                    //iApp.View_MovingLoad(ll_file);
                    iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_irc_vehicle_gap.Text));
                }

            }
            catch (Exception ex) { }
        }



        #region Stage Analysis

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
        UC_Stage_Extradosed_LRFD ucStage
        {
            get
            {
                if (AnalysisType == eAnalysis.Stage1) return uC_Stage_Extradosed_LRFD1;
                else if (AnalysisType == eAnalysis.Stage2) return uC_Stage_Extradosed_LRFD2;
                else if (AnalysisType == eAnalysis.Stage3) return uC_Stage_Extradosed_LRFD3;
                else if (AnalysisType == eAnalysis.Stage4) return uC_Stage_Extradosed_LRFD4;
                else if (AnalysisType == eAnalysis.Stage5) return uC_Stage_Extradosed_LRFD5;
                return uC_Stage_Extradosed_LRFD1;
            }
        }

        #endregion Stage Analysis
        private void btn_stage_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string inp_file = Get_Stage_FileName();
            int stg = (int)AnalysisType;
            if (AnalysisType == eAnalysis.Normal)
                inp_file = Get_Long_Girder_File(cmb_long_open_file.SelectedIndex, stg);
            else
                inp_file = Get_Long_Girder_File(ucStage.cmb_long_open_file.SelectedIndex, stg);



            if (btn.Text == btn_Ana_DL_create_data.Text)
            {
                btn_Ana_create_stage_data_Click(sender, e);
            }
            else if (btn.Text == btn_Process_LL_Analysis.Text)
            {
                btn_Ana_process_stage_analysis_Click(sender, e);

            }
            else if (btn.Text == btn_view_data.Text)
            {
                if (File.Exists(inp_file))
                    iApp.View_Input_File(inp_file);
            }
            else if (btn.Text == btn_view_preProcess.Text)
            {
                if (File.Exists(inp_file))
                    iApp.View_PreProcess(inp_file);
            }
            else if (btn.Text == btn_view_report.Text)
            {
                string ana_rep = MyList.Get_Analysis_Report_File(inp_file);
                if (File.Exists(ana_rep))
                {
                    System.Diagnostics.Process.Start(ana_rep);
                }

                if (File.Exists(stg_file))
                {
                    iApp.Open_TextFile(stg_file);
                }
            }
            else if (btn.Text == btn_view_postProcess.Text)
            {
                if (File.Exists(inp_file))
                    iApp.View_PostProcess(inp_file);
            }
        }

        private void btn_Ana_create_stage_data_Click(object sender, EventArgs e)
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

                LONG_GIRDER_LL_TXT();

                if (cmb_cable_type.SelectedIndex == 2)
                {

                    Create_Data_LL(Input_File_LL);
                    Create_Data_DL(Input_File_DL);

                    #region Chiranjit [2017 03 29]

                    ucStage.cmb_long_open_file.Items.Clear();
                    ucStage.cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    ucStage.cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS");
                    //cmb_long_open_file.Items.Add("DL + LL ANALYSIS");


                    #endregion Chiranjit [2014 10 22]


                }
                else
                {
                    Create_Data_Extradossed(Input_File);

                    ucStage.cmb_long_open_file.Items.Clear();
                    ucStage.cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    for (int i = 0; i < all_loads.Count; i++)
                    {
                        ucStage.cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + (i + 1));
                    }
                    ucStage.cmb_long_open_file.Items.Add("DL + LL ANALYSIS");
                    ucStage.cmb_long_open_file.SelectedIndex = 0;



                    #region Update Stage Files


                    int stg = (int)AnalysisType;
                    for (int i = 0; i < ucStage.cmb_long_open_file.Items.Count; i++)
                    {
                        string prv_file = Get_Long_Girder_File(i, stg - 1);
                        string new_file = Get_Long_Girder_File(i, stg);
                        iApp.Change_Stage_Coordinates(prv_file, new_file);
                    }


                    #endregion


                }

                MessageBox.Show(this, "Dead Load and Live Load Analysis Input Data files are Created in Working folder.");
                cmb_long_open_file.SelectedIndex = 0;
                Button_Enable_Disable();
            }
            catch (Exception ex) { }
        }

        private void btn_Ana_process_stage_analysis_Click(object sender, EventArgs e)
        {
            string flPath = Extradosed.Input_File;
            string ana_rep_file = "";
            int c = 0;

            Write_All_Data();

            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();


            if (cmb_cable_type.SelectedIndex == 2)
            {
                iApp.Progress_Works.Clear();

                flPath = Deck_Analysis_DL.Input_File;
                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);


                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");

                flPath = Deck_Analysis_LL.Input_File;
                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);

                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");
            }
            else
            {
                iApp.Progress_Works.Clear();

                flPath = Extradosed.DeadLoadAnalysis_Input_File;
                pd = new ProcessData();

                pd.Stage_File_Name = Get_Long_Girder_File(0, 0);
                pd.IS_Stage_File = true;

                flPath = Get_Long_Girder_File(0, (int)AnalysisType);
                pd.Process_File_Name = flPath;


                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);

                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");

                flPath = Extradosed.TotalAnalysis_Input_File;
                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.IS_Stage_File = true;

                flPath = Get_Long_Girder_File(cmb_long_open_file.Items.Count - 1, (int)AnalysisType);
                pd.Process_File_Name = flPath;
                pd.Stage_File_Name = Get_Long_Girder_File(cmb_long_open_file.Items.Count - 1, 0);



                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);


                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                if (File.Exists(iApp.Stage_File))
                {
                    flPath = Extradosed.DeadLoadAnalysis_Input_File;
                }

                for (int i = 0; i < (all_loads.Count); i++)
                {

                    flPath = Get_Live_Load_Analysis_Input_File(i + 1);

                    pd = new ProcessData();
                    pd.IS_Stage_File = true;
                    //pd.Stage_File_Name = Get_Long_Girder_File(i, 0);
                    pd.Stage_File_Name = Get_Live_Load_Analysis_Input_File(i + 1, 0);
                    flPath = Get_Live_Load_Analysis_Input_File(i + 1);

                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);

                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");

                }
            }

            List<string> emod = new List<string>();
            emod.Add("BEAM");
            emod.Add(ucStage.txt_emod_conc.Text);
            emod.Add(ucStage.txt_emod_conc.Text);
            emod.Add(ucStage.txt_emod_steel.Text);
            emod.Add("TRUSS");
            emod.Add(ucStage.txt_emod_cable.Text);

            string ePath = Path.GetDirectoryName(flPath);
            ePath = Path.Combine(Path.GetDirectoryName(ePath), "emod.fil");
            File.WriteAllLines(ePath, emod.ToArray());

            pcol.EMod_File = ePath;
            if (!iApp.Show_and_Run_Process_List(pcol))
            {
                Button_Enable_Disable();
                return;
            }
            //string file_name = Path.Combine(Path.GetDirectoryName(Extradosed.TotalAnalysis_Input_File), @"PROCESS STAGE (P DELTA) ANALYSIS\STAGE 1 ANALYSIS\ANALYSIS_REP.TXT");
            //Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis("DL + LL Combine Analysis\PROCESS STAGE (P DELTA) ANALYSIS\STAGE 1 ANALYSIS")

            Show_Member_Forces_Indian(ana_rep_file);

            Show_Analysis_Result();

            Extradosed.All_LL_Analysis = All_Analysis;
            //uC_CableStayedDesign1.CS_Analysis = Extradosed;
            Save_FormRecord.Write_All_Data(this, user_path);
        }

        string Get_Stage_FileName()
        {

            string file_name = "";
            Extradosed.Input_File = Get_Input_File((int)AnalysisType);
            if (Extradosed != null)
            {
                if (cmb_cable_type.SelectedIndex == 2)
                {
                    if (ucStage.cmb_long_open_file.SelectedIndex == 0)
                        file_name = Deck_Analysis_DL.Input_File;
                    else if (ucStage.cmb_long_open_file.SelectedIndex == 1)
                        file_name = Deck_Analysis_LL.Input_File;
                }
                else
                {
                    if (ucStage.cmb_long_open_file.SelectedIndex < ucStage.cmb_long_open_file.Items.Count)
                    {
                        if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                        {
                            if (cmb_cable_type.SelectedIndex == 2)
                            {
                                if (ucStage.cmb_long_open_file.SelectedIndex == 0)
                                    file_name = Deck_Analysis_DL.Input_File;
                                else if (ucStage.cmb_long_open_file.SelectedIndex == 1)
                                    file_name = Deck_Analysis_LL.Input_File;
                            }
                            else
                            {
                                if (ucStage.cmb_long_open_file.SelectedIndex == 0)
                                    file_name = Extradosed.DeadLoadAnalysis_Input_File;
                                else if (ucStage.cmb_long_open_file.SelectedIndex == ucStage.cmb_long_open_file.Items.Count - 1)
                                    file_name = Extradosed.TotalAnalysis_Input_File;
                                else
                                    file_name = Get_Live_Load_Analysis_Input_File(ucStage.cmb_long_open_file.SelectedIndex);
                            }
                        }
                        else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                        {
                            if (ucStage.cmb_long_open_file.SelectedIndex == 0)
                                file_name = Extradosed.DeadLoadAnalysis_Input_File;
                            else
                                file_name = Get_Live_Load_Analysis_Input_File(ucStage.cmb_long_open_file.SelectedIndex);
                        }
                        else
                        {
                            if (ucStage.cmb_long_open_file.SelectedIndex == 0)
                                file_name = Extradosed.DeadLoadAnalysis_Input_File;
                            else
                                file_name = Get_Live_Load_Analysis_Input_File(ucStage.cmb_long_open_file.SelectedIndex);
                        }
                    }
                    else
                    {
                        file_name = Result_Report_LL;
                    }
                }
            }
            return file_name;
        }
        private void cmb_long_open_stage_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file_name = Get_Long_Girder_File(ucStage.cmb_long_open_file.SelectedIndex, (int)AnalysisType);



            ucStage.btn_view_data.Enabled = File.Exists(file_name);
            ucStage.btn_view_postProcess.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
            ucStage.btn_view_preProcess.Enabled = File.Exists(file_name);
            ucStage.btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));

            //if (cmb_cable_type.SelectedIndex == 2)
            //{
            //    ucStage.btn_view_preProcess.Enabled = File.Exists(file_name);
            //}
            //else
            //{
            //    //btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file.SelectedIndex != cmb_long_open_file.Items.Count - 1;
            //    ucStage.btn_view_preProcess.Enabled = File.Exists(file_name);
            //}
            ucStage.btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
        }
        private void Show_Member_Forces_Indian(string ana_rep_file, UC_Stage_Extradosed uc)
        {
            All_Analysis.Clear();

            if (cmb_cable_type.SelectedIndex == 2)
            {
                //Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;
                //Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

                ana_rep_file = MyList.Get_Analysis_Report_File(Deck_Analysis_DL.Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }
                ana_rep_file = MyList.Get_Analysis_Report_File(Deck_Analysis_LL.Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }

            }
            else
            {
                ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.DeadLoadAnalysis_Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Extradosed.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }

                ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.TotalAnalysis_Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


                for (int i = 0; i < all_loads.Count; i++)
                {
                    ana_rep_file = Get_Live_Load_Analysis_Input_File(i + 1);

                    ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                    if (File.Exists(ana_rep_file))
                    {
                        All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                    }
                }

                Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;

                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];
            }

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

        private void btn_edit_load_combs_Click(object sender, EventArgs e)
        {
            LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
            ff.Owner = this;
            ff.ShowDialog();
        }

        private void btn_long_restore_ll_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

                if (btn.Name == btn_long_restore_ll_IRC.Name)
                {
                    Default_LRFD_Moving_LoadData(dgv_long_liveloads);
                    Default_LRFD_Moving_Type_LoadData(dgv_long_loads);

                }
            }
        }

        private void cmb_design_stage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Change_Stage_Data();

            eAnalysis tp = (eAnalysis)cmb_design_stage.SelectedIndex;

            if (tp == eAnalysis.Normal)
            {
                #region Normal
                uC_Res.txt_Ana_dead_inner_long_3L_8_moment.Text = txt_Ana_dead_inner_long_3L_8_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_3L_8_shear.Text = txt_Ana_dead_inner_long_3L_8_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_deff_moment.Text = txt_Ana_dead_inner_long_deff_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_deff_shear.Text = txt_Ana_dead_inner_long_deff_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_L2_moment.Text = txt_Ana_dead_inner_long_L2_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_L2_shear.Text = txt_Ana_dead_inner_long_L2_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_L4_moment.Text = txt_Ana_dead_inner_long_L4_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_L4_shear.Text = txt_Ana_dead_inner_long_L4_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_L8_moment.Text = txt_Ana_dead_inner_long_L8_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_L8_shear.Text = txt_Ana_dead_inner_long_L8_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_support_moment.Text = txt_Ana_dead_inner_long_support_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_support_shear.Text = txt_Ana_dead_inner_long_support_shear.Text;
                uC_Res.txt_Ana_live_inner_long_3L_8_moment.Text = txt_Ana_live_inner_long_3L_8_moment.Text;
                uC_Res.txt_Ana_live_inner_long_3L_8_shear.Text = txt_Ana_live_inner_long_3L_8_shear.Text;
                uC_Res.txt_Ana_live_inner_long_deff_moment.Text = txt_Ana_live_inner_long_deff_moment.Text;
                uC_Res.txt_Ana_live_inner_long_deff_shear.Text = txt_Ana_live_inner_long_deff_shear.Text;

                uC_Res.txt_Ana_live_inner_long_L2_moment.Text = txt_Ana_live_inner_long_L2_moment.Text;
                uC_Res.txt_Ana_live_inner_long_L2_shear.Text = txt_Ana_live_inner_long_L2_shear.Text;
                uC_Res.txt_Ana_live_inner_long_L4_moment.Text = txt_Ana_live_inner_long_L4_moment.Text;
                uC_Res.txt_Ana_live_inner_long_L4_shear.Text = txt_Ana_live_inner_long_L4_shear.Text;
                uC_Res.txt_Ana_live_inner_long_L8_moment.Text = txt_Ana_live_inner_long_L8_moment.Text;
                uC_Res.txt_Ana_live_inner_long_L8_shear.Text = txt_Ana_live_inner_long_L8_shear.Text;
                uC_Res.txt_Ana_live_inner_long_support_moment.Text = txt_Ana_live_inner_long_support_moment.Text;
                uC_Res.txt_Ana_live_inner_long_support_shear.Text = txt_Ana_live_inner_long_support_shear.Text;
                uC_Res.txt_Ana_live_outer_long_3L_8_moment.Text = txt_Ana_live_outer_long_3L_8_moment.Text;
                uC_Res.txt_Ana_live_outer_long_3L_8_shear.Text = txt_Ana_live_outer_long_3L_8_shear.Text;

                uC_Res.txt_Ana_live_outer_long_deff_moment.Text = txt_Ana_live_outer_long_deff_moment.Text;
                uC_Res.txt_Ana_live_outer_long_deff_shear.Text = txt_Ana_live_outer_long_deff_shear.Text;
                uC_Res.txt_Ana_live_outer_long_L2_moment.Text = txt_Ana_live_outer_long_L2_moment.Text;
                uC_Res.txt_Ana_live_outer_long_L2_shear.Text = txt_Ana_live_outer_long_L2_shear.Text;
                uC_Res.txt_Ana_live_outer_long_L4_moment.Text = txt_Ana_live_outer_long_L4_moment.Text;
                uC_Res.txt_Ana_live_outer_long_L4_shear.Text = txt_Ana_live_outer_long_L4_shear.Text;
                uC_Res.txt_Ana_live_outer_long_L8_moment.Text = txt_Ana_live_outer_long_L8_moment.Text;
                uC_Res.txt_Ana_live_outer_long_L8_shear.Text = txt_Ana_live_outer_long_L8_shear.Text;
                uC_Res.txt_Ana_live_outer_long_support_moment.Text = txt_Ana_live_outer_long_support_moment.Text;
                uC_Res.txt_Ana_live_outer_long_support_shear.Text = txt_Ana_live_outer_long_support_shear.Text;


                uC_Res.uC_SR_DL.txt_final_Mx.Text = uC_SR_DL.txt_final_Mx.Text;
                uC_Res.uC_SR_DL.txt_final_Mz.Text = uC_SR_DL.txt_final_Mz.Text;


                uC_Res.uC_SR_DL.txt_final_vert_reac.Text = uC_SR_DL.txt_final_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_left_max_total_Mx.Text = uC_SR_DL.txt_left_max_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_left_max_total_Mz.Text = uC_SR_DL.txt_left_max_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_left_max_total_vert_reac.Text = uC_SR_DL.txt_left_max_total_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_left_total_Mx.Text = uC_SR_DL.txt_left_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_left_total_Mz.Text = uC_SR_DL.txt_left_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_left_total_vert_reac.Text = uC_SR_DL.txt_left_total_vert_reac.Text;


                uC_Res.uC_SR_DL.txt_max_Mx.Text = uC_SR_DL.txt_max_Mx.Text;
                uC_Res.uC_SR_DL.txt_max_Mz.Text = uC_SR_DL.txt_max_Mz.Text;
                uC_Res.uC_SR_DL.txt_max_vert_reac.Text = uC_SR_DL.txt_max_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_right_max_total_Mx.Text = uC_SR_DL.txt_right_max_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_right_max_total_Mz.Text = uC_SR_DL.txt_right_max_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_right_max_total_vert_reac.Text = uC_SR_DL.txt_right_max_total_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_right_total_Mx.Text = uC_SR_DL.txt_right_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_right_total_Mz.Text = uC_SR_DL.txt_right_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_right_total_vert_reac.Text = uC_SR_DL.txt_right_total_vert_reac.Text;



                uC_Res.uC_SR_LL.txt_final_Mx.Text = uC_SR_LL.txt_final_Mx.Text;
                uC_Res.uC_SR_LL.txt_final_Mz.Text = uC_SR_LL.txt_final_Mz.Text;


                uC_Res.uC_SR_LL.txt_final_vert_reac.Text = uC_SR_LL.txt_final_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_left_max_total_Mx.Text = uC_SR_LL.txt_left_max_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_left_max_total_Mz.Text = uC_SR_LL.txt_left_max_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_left_max_total_vert_reac.Text = uC_SR_LL.txt_left_max_total_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_left_total_Mx.Text = uC_SR_LL.txt_left_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_left_total_Mz.Text = uC_SR_LL.txt_left_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_left_total_vert_reac.Text = uC_SR_LL.txt_left_total_vert_reac.Text;


                uC_Res.uC_SR_LL.txt_max_Mx.Text = uC_SR_LL.txt_max_Mx.Text;
                uC_Res.uC_SR_LL.txt_max_Mz.Text = uC_SR_LL.txt_max_Mz.Text;
                uC_Res.uC_SR_LL.txt_max_vert_reac.Text = uC_SR_LL.txt_max_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_right_max_total_Mx.Text = uC_SR_LL.txt_right_max_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_right_max_total_Mz.Text = uC_SR_LL.txt_right_max_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_right_max_total_vert_reac.Text = uC_SR_LL.txt_right_max_total_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_right_total_Mx.Text = uC_SR_LL.txt_right_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_right_total_Mz.Text = uC_SR_LL.txt_right_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_right_total_vert_reac.Text = uC_SR_LL.txt_right_total_vert_reac.Text;


                uC_Res.uC_SR_Max.txt_brg_max_DL_Ton.Text = uC_SR_Max.txt_brg_max_DL_Ton.Text;
                uC_Res.uC_SR_Max.txt_brg_max_VR_Ton.Text = uC_SR_Max.txt_brg_max_VR_Ton.Text;
                uC_Res.uC_SR_Max.txt_mxf_left_total_Mx.Text = uC_SR_Max.txt_mxf_left_total_Mx.Text;
                uC_Res.uC_SR_Max.txt_mxf_left_total_Mz.Text = uC_SR_Max.txt_mxf_left_total_Mz.Text;
                uC_Res.uC_SR_Max.txt_mxf_left_total_vert_reac.Text = uC_SR_Max.txt_mxf_left_total_vert_reac.Text;
                uC_Res.uC_SR_Max.txt_mxf_max_Mx.Text = uC_SR_Max.txt_mxf_max_Mx.Text;


                uC_Res.uC_SR_Max.txt_mxf_max_Mz.Text = uC_SR_Max.txt_mxf_max_Mz.Text;
                uC_Res.uC_SR_Max.txt_mxf_max_vert_reac.Text = uC_SR_Max.txt_mxf_max_vert_reac.Text;
                uC_Res.uC_SR_Max.txt_mxf_right_total_Mx.Text = uC_SR_Max.txt_mxf_right_total_Mx.Text;
                uC_Res.uC_SR_Max.txt_mxf_right_total_Mz.Text = uC_SR_Max.txt_mxf_right_total_Mz.Text;
                uC_Res.uC_SR_Max.txt_mxf_right_total_vert_reac.Text = uC_SR_Max.txt_mxf_right_total_vert_reac.Text;



                #endregion Normal

                Copy_Data_Grid_View(uC_SR_DL.dgv_left_des_frc, uC_Res.uC_SR_DL.dgv_left_des_frc);
                Copy_Data_Grid_View(uC_SR_DL.dgv_right_des_frc, uC_Res.uC_SR_DL.dgv_right_des_frc);


                Copy_Data_Grid_View(uC_SR_LL.dgv_left_des_frc, uC_Res.uC_SR_LL.dgv_left_des_frc);
                Copy_Data_Grid_View(uC_SR_LL.dgv_right_des_frc, uC_Res.uC_SR_LL.dgv_right_des_frc);

                Copy_Data_Grid_View(uC_SR_Max.dgv_mxf_left_des_frc, uC_Res.uC_SR_Max.dgv_mxf_left_des_frc);
                Copy_Data_Grid_View(uC_SR_Max.dgv_mxf_right_des_frc, uC_Res.uC_SR_Max.dgv_mxf_right_des_frc);
                uC_Res.txt_max_delf.Text = txt_max_delf.Text;
            }
            else
            {

                var uc = uC_Stage_Extradosed_LRFD1;
                if (tp == eAnalysis.Stage2) uc = uC_Stage_Extradosed_LRFD2;
                else if (tp == eAnalysis.Stage3) uc = uC_Stage_Extradosed_LRFD3;
                else if (tp == eAnalysis.Stage4) uc = uC_Stage_Extradosed_LRFD4;
                else if (tp == eAnalysis.Stage5) uc = uC_Stage_Extradosed_LRFD5;

                #region Stage
                uC_Res.txt_Ana_dead_inner_long_3L_8_moment.Text = uc.txt_Ana_dead_inner_long_3L_8_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_3L_8_shear.Text = uc.txt_Ana_dead_inner_long_3L_8_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_deff_moment.Text = uc.txt_Ana_dead_inner_long_deff_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_deff_shear.Text = uc.txt_Ana_dead_inner_long_deff_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_L2_moment.Text = uc.txt_Ana_dead_inner_long_L2_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_L2_shear.Text = uc.txt_Ana_dead_inner_long_L2_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_L4_moment.Text = uc.txt_Ana_dead_inner_long_L4_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_L4_shear.Text = uc.txt_Ana_dead_inner_long_L4_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_L8_moment.Text = uc.txt_Ana_dead_inner_long_L8_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_L8_shear.Text = uc.txt_Ana_dead_inner_long_L8_shear.Text;
                uC_Res.txt_Ana_dead_inner_long_support_moment.Text = uc.txt_Ana_dead_inner_long_support_moment.Text;
                uC_Res.txt_Ana_dead_inner_long_support_shear.Text = uc.txt_Ana_dead_inner_long_support_shear.Text;
                uC_Res.txt_Ana_live_inner_long_3L_8_moment.Text = uc.txt_Ana_live_inner_long_3L_8_moment.Text;
                uC_Res.txt_Ana_live_inner_long_3L_8_shear.Text = uc.txt_Ana_live_inner_long_3L_8_shear.Text;
                uC_Res.txt_Ana_live_inner_long_deff_moment.Text = uc.txt_Ana_live_inner_long_deff_moment.Text;
                uC_Res.txt_Ana_live_inner_long_deff_shear.Text = uc.txt_Ana_live_inner_long_deff_shear.Text;

                uC_Res.txt_Ana_live_inner_long_L2_moment.Text = uc.txt_Ana_live_inner_long_L2_moment.Text;
                uC_Res.txt_Ana_live_inner_long_L2_shear.Text = uc.txt_Ana_live_inner_long_L2_shear.Text;
                uC_Res.txt_Ana_live_inner_long_L4_moment.Text = uc.txt_Ana_live_inner_long_L4_moment.Text;
                uC_Res.txt_Ana_live_inner_long_L4_shear.Text = uc.txt_Ana_live_inner_long_L4_shear.Text;
                uC_Res.txt_Ana_live_inner_long_L8_moment.Text = uc.txt_Ana_live_inner_long_L8_moment.Text;
                uC_Res.txt_Ana_live_inner_long_L8_shear.Text = uc.txt_Ana_live_inner_long_L8_shear.Text;
                uC_Res.txt_Ana_live_inner_long_support_moment.Text = uc.txt_Ana_live_inner_long_support_moment.Text;
                uC_Res.txt_Ana_live_inner_long_support_shear.Text = uc.txt_Ana_live_inner_long_support_shear.Text;
                uC_Res.txt_Ana_live_outer_long_3L_8_moment.Text = uc.txt_Ana_live_outer_long_3L_8_moment.Text;
                uC_Res.txt_Ana_live_outer_long_3L_8_shear.Text = uc.txt_Ana_live_outer_long_3L_8_shear.Text;

                uC_Res.txt_Ana_live_outer_long_deff_moment.Text = uc.txt_Ana_live_outer_long_deff_moment.Text;
                uC_Res.txt_Ana_live_outer_long_deff_shear.Text = uc.txt_Ana_live_outer_long_deff_shear.Text;
                uC_Res.txt_Ana_live_outer_long_L2_moment.Text = uc.txt_Ana_live_outer_long_L2_moment.Text;
                uC_Res.txt_Ana_live_outer_long_L2_shear.Text = uc.txt_Ana_live_outer_long_L2_shear.Text;
                uC_Res.txt_Ana_live_outer_long_L4_moment.Text = uc.txt_Ana_live_outer_long_L4_moment.Text;
                uC_Res.txt_Ana_live_outer_long_L4_shear.Text = uc.txt_Ana_live_outer_long_L4_shear.Text;
                uC_Res.txt_Ana_live_outer_long_L8_moment.Text = uc.txt_Ana_live_outer_long_L8_moment.Text;
                uC_Res.txt_Ana_live_outer_long_L8_shear.Text = uc.txt_Ana_live_outer_long_L8_shear.Text;
                uC_Res.txt_Ana_live_outer_long_support_moment.Text = uc.txt_Ana_live_outer_long_support_moment.Text;
                uC_Res.txt_Ana_live_outer_long_support_shear.Text = uc.txt_Ana_live_outer_long_support_shear.Text;


                uC_Res.uC_SR_DL.txt_final_Mx.Text = uc.uC_SR_DL.txt_final_Mx.Text;
                uC_Res.uC_SR_DL.txt_final_Mz.Text = uc.uC_SR_DL.txt_final_Mz.Text;


                uC_Res.uC_SR_DL.txt_final_vert_reac.Text = uc.uC_SR_DL.txt_final_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_left_max_total_Mx.Text = uc.uC_SR_DL.txt_left_max_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_left_max_total_Mz.Text = uc.uC_SR_DL.txt_left_max_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_left_max_total_vert_reac.Text = uc.uC_SR_DL.txt_left_max_total_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_left_total_Mx.Text = uc.uC_SR_DL.txt_left_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_left_total_Mz.Text = uc.uC_SR_DL.txt_left_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_left_total_vert_reac.Text = uc.uC_SR_DL.txt_left_total_vert_reac.Text;


                uC_Res.uC_SR_DL.txt_max_Mx.Text = uc.uC_SR_DL.txt_max_Mx.Text;
                uC_Res.uC_SR_DL.txt_max_Mz.Text = uc.uC_SR_DL.txt_max_Mz.Text;
                uC_Res.uC_SR_DL.txt_max_vert_reac.Text = uc.uC_SR_DL.txt_max_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_right_max_total_Mx.Text = uc.uC_SR_DL.txt_right_max_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_right_max_total_Mz.Text = uc.uC_SR_DL.txt_right_max_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_right_max_total_vert_reac.Text = uc.uC_SR_DL.txt_right_max_total_vert_reac.Text;
                uC_Res.uC_SR_DL.txt_right_total_Mx.Text = uc.uC_SR_DL.txt_right_total_Mx.Text;
                uC_Res.uC_SR_DL.txt_right_total_Mz.Text = uc.uC_SR_DL.txt_right_total_Mz.Text;
                uC_Res.uC_SR_DL.txt_right_total_vert_reac.Text = uc.uC_SR_DL.txt_right_total_vert_reac.Text;








                uC_Res.uC_SR_LL.txt_final_Mx.Text = uc.uC_SR_LL.txt_final_Mx.Text;
                uC_Res.uC_SR_LL.txt_final_Mz.Text = uc.uC_SR_LL.txt_final_Mz.Text;


                uC_Res.uC_SR_LL.txt_final_vert_reac.Text = uc.uC_SR_LL.txt_final_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_left_max_total_Mx.Text = uc.uC_SR_LL.txt_left_max_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_left_max_total_Mz.Text = uc.uC_SR_LL.txt_left_max_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_left_max_total_vert_reac.Text = uc.uC_SR_LL.txt_left_max_total_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_left_total_Mx.Text = uc.uC_SR_LL.txt_left_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_left_total_Mz.Text = uc.uC_SR_LL.txt_left_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_left_total_vert_reac.Text = uc.uC_SR_LL.txt_left_total_vert_reac.Text;


                uC_Res.uC_SR_LL.txt_max_Mx.Text = uc.uC_SR_LL.txt_max_Mx.Text;
                uC_Res.uC_SR_LL.txt_max_Mz.Text = uc.uC_SR_LL.txt_max_Mz.Text;
                uC_Res.uC_SR_LL.txt_max_vert_reac.Text = uc.uC_SR_LL.txt_max_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_right_max_total_Mx.Text = uc.uC_SR_LL.txt_right_max_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_right_max_total_Mz.Text = uc.uC_SR_LL.txt_right_max_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_right_max_total_vert_reac.Text = uc.uC_SR_LL.txt_right_max_total_vert_reac.Text;
                uC_Res.uC_SR_LL.txt_right_total_Mx.Text = uc.uC_SR_LL.txt_right_total_Mx.Text;
                uC_Res.uC_SR_LL.txt_right_total_Mz.Text = uc.uC_SR_LL.txt_right_total_Mz.Text;
                uC_Res.uC_SR_LL.txt_right_total_vert_reac.Text = uc.uC_SR_LL.txt_right_total_vert_reac.Text;


                uC_Res.uC_SR_Max.txt_brg_max_DL_Ton.Text = uc.uC_SR_Max.txt_brg_max_DL_Ton.Text;
                uC_Res.uC_SR_Max.txt_brg_max_VR_Ton.Text = uc.uC_SR_Max.txt_brg_max_VR_Ton.Text;
                uC_Res.uC_SR_Max.txt_mxf_left_total_Mx.Text = uc.uC_SR_Max.txt_mxf_left_total_Mx.Text;
                uC_Res.uC_SR_Max.txt_mxf_left_total_Mz.Text = uc.uC_SR_Max.txt_mxf_left_total_Mz.Text;
                uC_Res.uC_SR_Max.txt_mxf_left_total_vert_reac.Text = uc.uC_SR_Max.txt_mxf_left_total_vert_reac.Text;
                uC_Res.uC_SR_Max.txt_mxf_max_Mx.Text = uc.uC_SR_Max.txt_mxf_max_Mx.Text;


                uC_Res.uC_SR_Max.txt_mxf_max_Mz.Text = uc.uC_SR_Max.txt_mxf_max_Mz.Text;
                uC_Res.uC_SR_Max.txt_mxf_max_vert_reac.Text = uc.uC_SR_Max.txt_mxf_max_vert_reac.Text;
                uC_Res.uC_SR_Max.txt_mxf_right_total_Mx.Text = uc.uC_SR_Max.txt_mxf_right_total_Mx.Text;
                uC_Res.uC_SR_Max.txt_mxf_right_total_Mz.Text = uc.uC_SR_Max.txt_mxf_right_total_Mz.Text;
                uC_Res.uC_SR_Max.txt_mxf_right_total_vert_reac.Text = uc.uC_SR_Max.txt_mxf_right_total_vert_reac.Text;






                #endregion Stage

                Copy_Data_Grid_View(uc.uC_SR_DL.dgv_left_des_frc, uC_Res.uC_SR_DL.dgv_left_des_frc);
                Copy_Data_Grid_View(uc.uC_SR_DL.dgv_right_des_frc, uC_Res.uC_SR_DL.dgv_right_des_frc);

                Copy_Data_Grid_View(uc.uC_SR_LL.dgv_left_des_frc, uC_Res.uC_SR_LL.dgv_left_des_frc);
                Copy_Data_Grid_View(uc.uC_SR_LL.dgv_right_des_frc, uC_Res.uC_SR_LL.dgv_right_des_frc);

                Copy_Data_Grid_View(uc.uC_SR_Max.dgv_mxf_left_des_frc, uC_Res.uC_SR_Max.dgv_mxf_left_des_frc);
                Copy_Data_Grid_View(uc.uC_SR_Max.dgv_mxf_right_des_frc, uC_Res.uC_SR_Max.dgv_mxf_right_des_frc);

                uC_Res.txt_max_delf.Text = uc.txt_max_delf.Text;

            }
        }

        private void Copy_Data_Grid_View(DataGridView dataGridView1, DataGridView dataGridView2)
        {
            MyList.Copy_Data_Grid_View(dataGridView1, dataGridView2);
        }
        void Change_Stage_Data()
        {
            int stg = cmb_design_stage.SelectedIndex;

            //if (stg == 0)
            //{
            //}
            //else
            //{
            //    UC_Stage_Extradosed_LRFD uc = uC_Stage_Extradosed_LRFD1;

            //    if (stg == 2) uc = uC_Stage_Extradosed_LRFD2;
            //    if (stg == 3) uc = uC_Stage_Extradosed_LRFD3;
            //    if (stg == 4) uc = uC_Stage_Extradosed_LRFD4;
            //    if (stg == 5) uc = uC_Stage_Extradosed_LRFD5;
            //}

            string file_nm = "";
            if (stg == 0) file_nm = Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_PRIMARY.TXT");
            else if (stg == 1) file_nm = Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_1.TXT");
            else if (stg == 2) file_nm = Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_2.TXT");
            else if (stg == 3) file_nm = Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_3.TXT");
            else if (stg == 4) file_nm = Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_4.TXT");
            else if (stg == 5) file_nm = Path.Combine(user_path, "PROCESS\\ANALYSIS_RESULT_STAGE_5.TXT");
        }


        AASHTO_Box_Section aashto_box;
        void Section2_Section_Properties()
        {
            aashto_box = new AASHTO_Box_Section();

            int cs_cell_nos = MyList.StringToInt(txt_box_cs2_cell_nos.Text, 0);
            double cs_b1 = MyList.StringToDouble(txt_box_cs2_b1.Text, 0.0);
            double cs_b2 = MyList.StringToDouble(txt_box_cs2_b2.Text, 0.0);
            double cs_b3 = MyList.StringToDouble(txt_box_cs2_b3.Text, 0.0);
            double cs_b4 = MyList.StringToDouble(txt_box_cs2_b4.Text, 0.0);
            double cs_b5 = MyList.StringToDouble(txt_box_cs2_b5.Text, 0.0);
            double cs_b6 = MyList.StringToDouble(txt_box_cs2_b6.Text, 0.0);
            double cs_b7 = MyList.StringToDouble(txt_box_cs2_b7.Text, 0.0);
            double cs_b8 = MyList.StringToDouble(txt_box_cs2_b8.Text, 0.0);


            double cs_d1 = MyList.StringToDouble(txt_box_cs2_d1.Text, 0.0);
            double cs_d2 = MyList.StringToDouble(txt_box_cs2_d2.Text, 0.0);
            double cs_d3 = MyList.StringToDouble(txt_box_cs2_d3.Text, 0.0);
            double cs_d4 = MyList.StringToDouble(txt_box_cs2_d4.Text, 0.0);
            double cs_d5 = MyList.StringToDouble(txt_box_cs2_d5.Text, 0.0);



            aashto_box.Cell_Nos = cs_cell_nos;
            aashto_box.b1 = cs_b3;
            aashto_box.d1 = cs_d3;



            aashto_box.b2 = cs_b4;
            aashto_box.d2 = cs_d4 - cs_d3 - cs_d5;

            aashto_box.b3 = cs_b8;
            aashto_box.d3 = cs_d5;


            aashto_box.b4 = cs_b6;
            aashto_box.d4 = cs_d1;

            aashto_box.b5 = cs_b6;
            aashto_box.d5 = cs_d2 - cs_d1;

            aashto_box.b6 = cs_b5;
            //aashto_box.d6 = cs_d;

            aashto_box.b7 = cs_b7;
            aashto_box.d7 = cs_d3;

            aashto_box.b8 = cs_b7;
            aashto_box.d8 = cs_d2 - cs_d3;

            double val = cs_d4 - cs_d3 - cs_d5;

            double theta = Math.Atan(cs_b7 / val);
            aashto_box.theta_radian = theta;


            double b10 = Math.Tan(theta) * cs_d5;

            aashto_box.b9 = cs_b5 - b10;
            aashto_box.d9 = cs_d5;

            aashto_box.b10 = b10;
            aashto_box.d10 = cs_d5;


            txt_box_cs2_AX.Text = aashto_box.AX.ToString("f0");
            txt_box_cs2_IXX.Text = aashto_box.IXX.ToString("f0");
            txt_box_cs2_IYY.Text = aashto_box.IYY.ToString("f0");
        }

        private void txt_box_cs2_cell_nos_TextChanged(object sender, EventArgs e)
        {
            Section2_Section_Properties();
        }

        private void btn_process_steel_section_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btn_process_steel_section)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD PSC Box Girder Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_PSC_Box_Girder_Design(iApp, dgv_steel_girder_input_data, Excel_File);
            }
            else if (btn == btn_process_abutment)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_Abutment_and_Wingwall_Design(iApp, dgv_abutment_input_data, Excel_File);
            }
            else if (btn == btn_process_pier)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_Pier_Design(iApp, dgv_pier_input_data, Excel_File);
            }
            else if (btn == btn_process_bearing)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_Bearing_Design(iApp, dgv_bearing_input_data, Excel_File);
            }
        }

        private void btn_bearing_open_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            string Excel_File = "";
            if (btn == btn_steel_section_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD PSC Box Girder Design.xlsx");
            }
            else if (btn == btn_abutment_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
            }
            else if (btn == btn_pier_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
            }
            else if (btn == btn_bearing_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
            }
            if (File.Exists(Excel_File)) iApp.OpenExcelFile(Excel_File, "2011ap");
            else MessageBox.Show(Excel_File + " file not created.");

        }

        private void btn_steel_section_ws_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();

        }

        private void btn_result_summary_Click(object sender, EventArgs e)
        {
            if (File.Exists(stg_file)) iApp.Open_TextFile(stg_file);
        }

        private void uC_Stage_Extradosed_LRFD1_OnEModTextChanged(object sender, EventArgs e)
        {

            UC_Stage_Extradosed_LRFD uc = uC_Stage_Extradosed_LRFD1;

            double pcnt = MyList.StringToDouble(uc.txt_steel_pcnt) / 100;
            uc.txt_emod_steel.Text = (MyList.StringToDouble(txt_emod_steel) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_cable_pcnt) / 100;
            uc.txt_emod_cable.Text = (MyList.StringToDouble(txt_emod_cable) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_conc_pcnt) / 100;
            uc.txt_emod_conc.Text = (MyList.StringToDouble(txt_emod_conc) * pcnt).ToString("f0");


            uc = uC_Stage_Extradosed_LRFD2;

            pcnt = MyList.StringToDouble(uc.txt_steel_pcnt) / 100;
            uc.txt_emod_steel.Text = (MyList.StringToDouble(txt_emod_steel) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_cable_pcnt) / 100;
            uc.txt_emod_cable.Text = (MyList.StringToDouble(txt_emod_cable) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_conc_pcnt) / 100;
            uc.txt_emod_conc.Text = (MyList.StringToDouble(txt_emod_conc) * pcnt).ToString("f0");


            uc = uC_Stage_Extradosed_LRFD3;

            pcnt = MyList.StringToDouble(uc.txt_steel_pcnt) / 100;
            uc.txt_emod_steel.Text = (MyList.StringToDouble(txt_emod_steel) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_cable_pcnt) / 100;
            uc.txt_emod_cable.Text = (MyList.StringToDouble(txt_emod_cable) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_conc_pcnt) / 100;
            uc.txt_emod_conc.Text = (MyList.StringToDouble(txt_emod_conc) * pcnt).ToString("f0");


            uc = uC_Stage_Extradosed_LRFD4;

            pcnt = MyList.StringToDouble(uc.txt_steel_pcnt) / 100;
            uc.txt_emod_steel.Text = (MyList.StringToDouble(txt_emod_steel) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_cable_pcnt) / 100;
            uc.txt_emod_cable.Text = (MyList.StringToDouble(txt_emod_cable) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_conc_pcnt) / 100;
            uc.txt_emod_conc.Text = (MyList.StringToDouble(txt_emod_conc) * pcnt).ToString("f0");



            uc = uC_Stage_Extradosed_LRFD5;

            pcnt = MyList.StringToDouble(uc.txt_steel_pcnt) / 100;
            uc.txt_emod_steel.Text = (MyList.StringToDouble(txt_emod_steel) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_cable_pcnt) / 100;
            uc.txt_emod_cable.Text = (MyList.StringToDouble(txt_emod_cable) * pcnt).ToString("f0");

            pcnt = MyList.StringToDouble(uc.txt_conc_pcnt) / 100;
            uc.txt_emod_conc.Text = (MyList.StringToDouble(txt_emod_conc) * pcnt).ToString("f0");

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

        private void txt_box_cs2_AX_TextChanged(object sender, EventArgs e)
        {
            if(rbtn_multiple_cell.Checked)
            {
                double ax = MyList.StringToDouble(txt_box_cs2_AX) / (12 * 12);
                double ixx = MyList.StringToDouble(txt_box_cs2_IXX) / (12 * 12 * 12 * 12);
                double iyy = MyList.StringToDouble(txt_box_cs2_IYY) / (12 * 12 * 12 * 12);
                double izz = ixx + iyy;

                txt_tot_AX.Text = ax.ToString("f3");
                txt_tot_IXX.Text = ixx.ToString("f3");
                txt_tot_IYY.Text = iyy.ToString("f3");
                txt_tot_IZZ.Text = izz.ToString("f3");
            }
        }
        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            Control rbtn = sender as Control;
            if (rbtn.Name == chk_fp_left.Name)
            {
                if (chk_footpath.Checked)
                {
                    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                        chk_fp_right.Checked = true;

                    if (!chk_fp_left.Checked)
                    {
                        txt_Ana_hf_LHS.Enabled = false;
                        txt_Ana_wf_LHS.Enabled = false;
                        txt_Ana_hf_LHS.Text = "0.000";
                        txt_Ana_wf_LHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_hf_LHS.Enabled = true;
                        txt_Ana_wf_LHS.Enabled = true;
                        txt_Ana_hf_LHS.Text = "3.5";
                        txt_Ana_wf_LHS.Text = "1.4375";
                    }

                }
            }
            else if (rbtn.Name == chk_fp_right.Name)
            {
                if (chk_footpath.Checked)
                {
                    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                        chk_fp_left.Checked = true;


                    if (!chk_fp_right.Checked)
                    {
                        txt_Ana_hf_RHS.Enabled = false;
                        txt_Ana_wf_RHS.Enabled = false;
                        txt_Ana_hf_RHS.Text = "0.000";
                        txt_Ana_wf_RHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_hf_RHS.Enabled = true;
                        txt_Ana_wf_RHS.Enabled = true;
                        txt_Ana_hf_RHS.Text = "3.5";
                        txt_Ana_wf_RHS.Text = "1.4375";
                    }
                }
            }

            if (rbtn.Name == chk_cb_left.Name)
            {
                if (chk_crash_barrier.Checked)
                {
                    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
                        chk_cb_right.Checked = true;

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
                        txt_Ana_Hc_LHS.Text = "3.5";
                        txt_Ana_Wc_LHS.Text = "1.4375";
                    }
                }
            }
            else if (rbtn.Name == chk_cb_right.Name)
            {
                if (chk_crash_barrier.Checked)
                {
                    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
                        chk_cb_left.Checked = true;


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
                        txt_Ana_Hc_RHS.Text = "3.5";
                        txt_Ana_Wc_RHS.Text = "1.4375";
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

            chk_cb_left.Enabled = chk_crash_barrier.Checked;
            chk_cb_right.Enabled = chk_crash_barrier.Checked;

            chk_fp_left.Enabled = chk_footpath.Checked;
            chk_fp_right.Enabled = chk_footpath.Checked;

            if (rbtn.Name == chk_crash_barrier.Name)
            {
                grb_ana_crashBarrier.Enabled = chk_crash_barrier.Checked;
                if (!chk_crash_barrier.Checked)
                {
                    txt_Ana_Hc_LHS.Text = "0.000";
                    txt_Ana_Wc_LHS.Text = "0.000";
                    txt_Ana_Hc_RHS.Text = "0.000";
                    txt_Ana_Wc_RHS.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hc_LHS.Text = "3.5";
                    txt_Ana_Wc_LHS.Text = "1.4375";
                    txt_Ana_Hc_RHS.Text = "3.5";
                    txt_Ana_Wc_RHS.Text = "1.4375";
                }
            }
            else if (rbtn.Name == chk_footpath.Name)
            {
                grb_ana_sw_fp.Enabled = chk_footpath.Checked;
                if (!chk_footpath.Checked)
                {
                    txt_Ana_wf_LHS.Text = "0.000";
                    txt_Ana_hf_LHS.Text = "0.000";
                    txt_Ana_wf_RHS.Text = "0.000";
                    txt_Ana_hf_RHS.Text = "0.000";

                    txt_Ana_Wk.Text = "0.000";
                    txt_Ana_wr.Text = "0.000";
                }
                else
                {
                    txt_Ana_wf_LHS.Text = "1.4375";
                    txt_Ana_hf_LHS.Text = "3.5";

                    txt_Ana_wf_RHS.Text = "1.4375";
                    txt_Ana_hf_RHS.Text = "3.5";


                    txt_Ana_Wk.Text = "0.500";
                    txt_Ana_wr.Text = "0.100";
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


    }

    public class CABLE_STAYED_Extradosed_LS_Analysis_AASHTO
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }

        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis TotalLoad_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_Analysis = null;

        public BridgeMemberAnalysis LiveLoad_1_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_2_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_3_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_4_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_5_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_6_Analysis = null;

        public List<BridgeMemberAnalysis> All_LL_Analysis = null;


        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;


        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;


        public List<int> _L8_jnts = new List<int>();
        public List<int> _L4_jnts = new List<int>();
        public List<int> _3L8_jnts = new List<int>();
        public List<int> _L2_jnts = new List<int>();
        public List<int> _supp_jnts = new List<int>();
        public List<int> _deff_jnts = new List<int>();
        public List<int> _End_jnts = new List<int>();
        public List<int> _Cable_jnts = new List<int>();



        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        public int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        public PSC_Box_Section_Data PSC_SECIONS;

        MemberSectionProperty tower_sec = null;
        MemberSectionProperty cable_sec = null;

        public List<MemberSectionProperty> SectionProperty { get; set; }

        //Chiranjit [2013 06 06] Kolkata

        public string List_Envelop_Inner { get; set; }
        public string List_Envelop_Outer { get; set; }

        public string Start_Support { get; set; }
        public string End_Support { get; set; }


        public string E_CONC { get; set; }
        public string E_STEEL { get; set; }
        public string E_CABLE { get; set; }
        public string DEN_CONC { get; set; }
        public string DEN_STEEL { get; set; }
        public string DEN_CABLE { get; set; }
        public string PR_CONC { get; set; }
        public string PR_STEEL { get; set; }
        public string PR_CABLE { get; set; }

        public bool ADD_Selfweight { get; set; }



        public double _cen_Ax = 0.0;
        public double _cen_Ix = 0.0;
        public double _cen_Iy = 0.0;
        public double _cen_Iz = 0.0;

        public double _inn_Ax = 0.0;
        public double _inn_Ix = 0.0;
        public double _inn_Iy = 0.0;
        public double _inn_Iz = 0.0;

        public double _out_Ax = 0.0;
        public double _out_Ix = 0.0;
        public double _out_Iy = 0.0;
        public double _out_Iz = 0.0;

        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(Working_Folder, "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(Working_Folder, "ANALYSIS_REP.TXT");
            }
        }
        #region Analysis Input File
        #endregion Analysis Input File
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                input_file = value;
                if (File.Exists(value))
                    user_path = Path.GetDirectoryName(input_file);
            }
        }
        //Chiranjit [2012 05 27]
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "DL + LL Combine Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DL_LL_Combine_Input_File.txt");
                }
                return "";
            }
        }
        //Chiranjit [2012 05 27]
        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Live Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        public string DeadLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        //Chiranjit [2014 10 22]
        public string Get_LL_Analysis_Input_File(int AnalysisNo)
        {
            if (AnalysisNo <= 0) return "";

            if (Directory.Exists(Working_Folder))
            {
                string pd = Path.Combine(Working_Folder, "LL Analysis Load " + AnalysisNo);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_Load_" + AnalysisNo + "_Input_File.txt");



            }
            return "";
        }
        public string Working_Folder
        {
            get
            {
                if (File.Exists(Input_File))
                    return Path.GetDirectoryName(Input_File);
                return "";
            }
        }

        string input_file, user_path;
        public CABLE_STAYED_Extradosed_LS_Analysis_AASHTO(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            //Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            //Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
            List_Envelop_Inner = "";
            List_Envelop_Outer = "";

            Cable_Nos = 6;
            Init_dist = 15.0;
            Cable_x_dist = 6.0;
            Cable_y_dist = 3.0;
            Cable_intv_dist = 1.5;
            Tower_height = 12.0;

            tower_sec = new MemberSectionProperty();
            cable_sec = new MemberSectionProperty();

            L1 = 0.0;

            L2 = 0.0;

            L3 = 0.0;

            Spacing_Cross_Girder = 0.0;

            Spacing_Long_Girder = 0.0;

            Skew_Angle = 0.0;

            IsCentral_Cable = false;

            #region Distances

            _Support_Dist = new List<double>();
            _Deff_Dist = new List<double>();
            _L8_Dist = new List<double>();
            _L4_Dist = new List<double>();
            _3L8_Dist = new List<double>();
            _L2_Dist = new List<double>();

            #endregion Distances
        }


        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();

        #region Chiranjit [2014 09 02] For British Standard

        public List<int> HA_Lanes;

        public string HA_Loading_Members;
        public void CreateData_British()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d;
            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data


            List<double> HA_distances = new List<double>();
            if (HA_Lanes.Count > 0)
            {
                double ha = 0.0;

                for (int i = 0; i < HA_Lanes.Count; i++)
                {
                    ha = 1.75 + (HA_Lanes[i] - 1) * 3.5;
                    if (!list_z.Contains(ha))
                    {
                        list_z.Add(ha);
                        HA_distances.Add(ha);
                    }
                }
            }

            list_z.Sort();

            #endregion Chiranjit [2011 09 23] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            //int i = 0;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows]);
                }
                z_table.Add(list_z[iRows], list);
            }

            int nodeNo = 0;
            Joints.Clear();


            #region Chiranjit [2013 05 30]

            #endregion Chiranjit [2013 05 30]

            #region Chiranjit [2013 06 06]

            #endregion Chiranjit [2013 06 06]
        }

        #endregion Chiranjit [2014 09 02] For British Standard

        public void Clear()
        {
            MemColls.Clear();
            MemColls = null;
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList_1 = new List<LoadData>();
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
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -60.0);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
                    for (int j = 0; j < Live_Load_List.Count; j++)
                    {
                        if (Live_Load_List[j].TypeNo == ld.TypeNo)
                        {
                            ld.LoadWidth = Live_Load_List[j].LoadWidth;
                            break;
                        }
                    }
                    if ((ld.Z + ld.LoadWidth) > WidthBridge)
                    {
                        throw new Exception("Width of Bridge Deck is insufficient to accommodate \ngiven numbers of Lanes of Vehicle Load. \n\nBridge Width = " + WidthBridge + " <  Load Width (" + ld.Z + " + " + ld.LoadWidth + ") = " + (ld.Z + ld.LoadWidth));
                    }
                    else
                    {
                        ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                        ld.ImpactFactor = MyList.StringToDouble(dgv_live_load[5, i].Value.ToString(), 0.5);
                        LoadList_1.Add(ld);
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #region Set Loads

            List<LoadData> LoadList_tmp = new List<LoadData>(LoadList_1.ToArray());

            LoadList_2 = new List<LoadData>();
            LoadList_3 = new List<LoadData>();
            LoadList_4 = new List<LoadData>();
            LoadList_5 = new List<LoadData>();
            LoadList_6 = new List<LoadData>();

            LoadList_1.Clear();

            //70 R Wheel
            ld = new LoadData(Live_Load_List[2]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_1.Add(ld);


            //1 Lane Class A
            ld = new LoadData(Live_Load_List[0]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_2.Add(ld);



            //2 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);



            //3 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[2].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);


            //1 Lane Class A + 70 RW
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);

            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);



            //70 RW + 1 Lane Class A 
            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            #endregion Set Loads

        }

        public double WidthBridge { get; set; }


        public List<int> Long_Girder_members = new List<int>();
        public List<int> Cross_Girder_members = new List<int>();
        public List<int> Tower_members = new List<int>();
        public List<int> Cable_members = new List<int>();
        public List<int> Supports = new List<int>();



        public List<int> side_load_joints = new List<int>();
        public List<int> central_load_joints = new List<int>();


        public List<int> side_supports = new List<int>();
        public List<int> central_supports = new List<int>();

        public void Create_Extradossed_Data1()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();



            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();

            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 2;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];

            #endregion End Span 2

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}

            list_x.AddRange(lst_x_coords);


            bool flag = true;

            list_x.Sort();


            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();
            MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }
            #endregion Tower Members


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);


                side_load_joints.Add(mem.StartNode.NodeNo);
            }






            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.StartNode.NodeNo);

            }


            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members





            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));

        }



        public List<int> _L8_mems = new List<int>();
        public List<int> _L4_mems = new List<int>();
        public List<int> _3L8_mems = new List<int>();
        public List<int> _L2_mems = new List<int>();
        public List<int> _supp_mems = new List<int>();
        public List<int> _deff_mems = new List<int>();
        public List<int> _End_mems = new List<int>();


        public bool IsCentral_Cable { get; set; }

        public void Create_Extradossed_Data()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);


                    if (iRows == 1 || iRows == 3)
                    {
                        if (Joints_Array[iRows, iCols].X >= L1 && Joints_Array[iRows, iCols].X <= L1 + L2)
                        {
                            _Cable_jnts.Add(nodeNo);
                        }
                    }

                    if (iRows == 2)
                    {
                        var x = Joints_Array[iRows, iCols].X;
                        if (_Support_Dist.Contains(x))
                        {
                            _supp_jnts.Add(nodeNo);
                        }
                        else if (_Deff_Dist.Contains(x))
                        {
                            _deff_jnts.Add(nodeNo);
                        }
                        else if (_L8_Dist.Contains(x))
                        {
                            _L8_jnts.Add(nodeNo);
                        }
                        else if (_L4_Dist.Contains(x))
                        {
                            _L4_jnts.Add(nodeNo);
                        }
                        else if (_3L8_Dist.Contains(x))
                        {
                            _3L8_jnts.Add(nodeNo);
                        }
                        else if (_L2_Dist.Contains(x))
                        {
                            _L2_jnts.Add(nodeNo);
                        }
                    }
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();

            //Chiranjit [2017 03 29]
            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }










            //MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }
            #endregion Tower Members

            if (true)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;
                for (i = 0; i < _Columns - 2; i++)
                {
                    var item = Long_Girder_Members_Array[2, i];


                    _End_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                    //    cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;


                    //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                    //    _supp_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                    //    _deff_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                    //    _L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                    //    _L4_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                    //    _3L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                    //    _L2_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                    //   cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;

                    foreach (var d in _L2_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 6;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _3L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 5;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L4_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 4;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 3;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Deff_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 2;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Support_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 1;
                            goto _SS;
                            break;
                        }
                    }

                _SS:

                    if (cnt == 1)
                    {
                        _supp_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 2)
                    {
                        _deff_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 3)
                    {
                        _L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 4)
                    {
                        _L4_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 5)
                    {
                        _3L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 6)
                    {
                        _L2_mems.Add(item.MemberNo);
                    }
                }

                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        //Chiranjit Remove one Side cables
        public void Create_Extradossed_Data_1_Side()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    //Joints.Add(nd);
                }
            }
            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
            }
            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();

            //Chiranjit [2017 03 29]
            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }










            //MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            #endregion Tower Members

            if (true)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                if (false)
                {
                    #region Cable Members

                    y_cables.Sort();
                    y_cables.Reverse();
                    for (i = 0; i < x_cable1.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.StartNode.NodeNo);

                    }


                    y_cables.Reverse();


                    for (i = 0; i < x_cable2.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.EndNode.NodeNo);
                    }




                    y_cables.Reverse();


                    for (i = 0; i < x_cable3.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.StartNode.NodeNo);
                    }



                    y_cables.Reverse();


                    for (i = 0; i < x_cable4.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.EndNode.NodeNo);
                    }



                    #endregion Cable Members
                }
                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;
                for (i = 0; i < _Columns - 2; i++)
                {
                    var item = Long_Girder_Members_Array[2, i];


                    _End_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                    //    cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;


                    //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                    //    _supp_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                    //    _deff_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                    //    _L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                    //    _L4_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                    //    _3L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                    //    _L2_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                    //   cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;

                    foreach (var d in _L2_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 6;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _3L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 5;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L4_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 4;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 3;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Deff_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 2;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Support_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 1;
                            goto _SS;
                            break;
                        }
                    }

                _SS:

                    if (cnt == 1)
                    {
                        _supp_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 2)
                    {
                        _deff_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 3)
                    {
                        _L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 4)
                    {
                        _L4_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 5)
                    {
                        _3L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 6)
                    {
                        _L2_mems.Add(item.MemberNo);
                    }
                }

                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        public void Create_Extradossed_Data_1_Side_Centre()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    //Joints.Add(nd);
                }
            }
            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
            }
            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();

            //Chiranjit [2017 03 29]
            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }










            //MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            #endregion Tower Members

            if (true)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                if (false)
                {
                    #region Cable Members

                    y_cables.Sort();
                    y_cables.Reverse();
                    for (i = 0; i < x_cable1.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.StartNode.NodeNo);

                    }


                    y_cables.Reverse();


                    for (i = 0; i < x_cable2.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.EndNode.NodeNo);
                    }




                    y_cables.Reverse();


                    for (i = 0; i < x_cable3.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.StartNode.NodeNo);
                    }



                    y_cables.Reverse();


                    for (i = 0; i < x_cable4.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.EndNode.NodeNo);
                    }



                    #endregion Cable Members
                }
                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;

                int a = 0;
                while (a <= 2)
                {
                    Member item = null;
                    a++;
                    for (i = 0; i < _Columns - 2; i++)
                    {
                        //var item = Long_Girder_Members_Array[2, i];

                        if (a == 1)
                            item = Long_Girder_Members_Array[1, i];
                        else
                            item = Long_Girder_Members_Array[3, i];

                        _End_mems.Add(item.MemberNo);



                        //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                        //    cnt = 1;
                        //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                        //    cnt = 2;
                        //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                        //    cnt = 3;
                        //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                        //    cnt = 4;
                        //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                        //    cnt = 5;
                        //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                        //    cnt = 6;


                        //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                        //    _supp_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                        //    _deff_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                        //    _L8_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                        //    _L4_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                        //    _3L8_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                        //    _L2_mems.Add(item.MemberNo);



                        //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                        //   cnt = 1;
                        //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                        //    cnt = 2;
                        //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                        //    cnt = 3;
                        //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                        //    cnt = 4;
                        //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                        //    cnt = 5;
                        //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                        //    cnt = 6;

                        foreach (var d in _L2_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 6;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _3L8_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 5;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _L4_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 4;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _L8_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 3;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _Deff_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 2;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _Support_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 1;
                                goto _SS;
                                break;
                            }
                        }

                    _SS:

                        if (cnt == 1)
                        {
                            _supp_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 2)
                        {
                            _deff_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 3)
                        {
                            _L8_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 4)
                        {
                            _L4_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 5)
                        {
                            _3L8_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 6)
                        {
                            _L2_mems.Add(item.MemberNo);
                        }
                    }
                }


                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        public void Create_Extradossed_CentralCable_Data()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            if (false)
            {
                #region Right Tower 1

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }

                #endregion Right Tower 1

                #region Right Tower 1

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }

                #endregion Right Tower 1
            }


            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();



            #region Cable


            if (false)
            {
                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members
            }
            #endregion Cable











            //MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            #endregion Tower Members

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[2]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);


                side_load_joints.Add(mem.StartNode.NodeNo);
            }






            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;
                for (i = 0; i < _Columns - 2; i++)
                {
                    var item = Long_Girder_Members_Array[2, i];


                    _End_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                    //    cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;


                    //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                    //    _supp_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                    //    _deff_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                    //    _L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                    //    _L4_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                    //    _3L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                    //    _L2_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                    //   cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;

                    foreach (var d in _L2_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 6;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _3L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 5;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L4_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 4;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 3;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Deff_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 2;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Support_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 1;
                            goto _SS;
                            break;
                        }
                    }

                _SS:

                    if (cnt == 1)
                    {
                        _supp_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 2)
                    {
                        _deff_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 3)
                    {
                        _L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 4)
                    {
                        _L4_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 5)
                    {
                        _3L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 6)
                    {
                        _L2_mems.Add(item.MemberNo);
                    }
                }

                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        public void Create_Extradossed_Data_2017_02_16()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();



            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance == 0.0)
            {
                #region Span 1

                //_Support_Dist.Add(Support_Distance);
                //_Support_Dist.Add(L1 - Support_Distance);

                //_Deff_Dist.Add(Effective_Distance);
                //_Deff_Dist.Add(L1 - Effective_Distance);

                //_L8_Dist.Add(L1 / 8);
                //_L8_Dist.Add(L1 - L1 / 8);

                //_L4_Dist.Add(L1 / 4);
                //_L4_Dist.Add(L1 - L1 / 4);


                //_3L8_Dist.Add(3 * L1 / 8.0);
                //_3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                //_L2_Dist.Add(L1 / 2.0);

                #endregion Span 1




                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));



                //lst1.Add(Support_Distance);
                //lst1.Add(Effective_Distance);
                //lst1.Add(L1 / 8);
                //lst1.Add(L1 / 4);
                //lst1.Add(3 * L1 / 8);
                //lst1.Add(L1 / 2);

                //lst1.Add(L1 - Support_Distance);
                //lst1.Add(L1 - Effective_Distance);
                //lst1.Add(L1 - L1 / 8);
                //lst1.Add(L1 - L1 / 4);
                //lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                list_x.AddRange(lst1.ToArray());

                foreach (var item in lst2)
                {
                    list_x.Add(L1 + item);
                }



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 2;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();




            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();



            #region Cable

            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);


                side_load_joints.Add(mem.StartNode.NodeNo);
            }






            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.StartNode.NodeNo);

            }


            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members

            #endregion Cable











            //MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }
            #endregion Tower Members

            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));



            List<int> deff_mems = new List<int>();


            //Long_Girder_Members_Array
        }

        public void Create_Extradossed_Linear_Data()
        {
            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();



            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance == 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1


                #region Span 2


                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));

                #endregion Span 2

                #region Span 3

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 3

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                list_x.AddRange(lst1.ToArray());

                foreach (var item in lst2)
                {
                    list_x.Add(L1 + item);
                }



                lst2.Sort();


                foreach (var item in lst1)
                {
                    list_x.Add(L1 + L2 + item);
                }

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 2;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();




            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;


            if (false)
            {
                #region Z Coordinates


                //last_z = WidthCantilever;
                //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //list_z.Add(last_z);

                //last_z = WidthBridge / 2.0;
                //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //list_z.Add(last_z);

                ////last_z = WidthBridge - WidthCantilever;
                ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                ////list_z.Add(last_z);

                //last_z = WidthBridge;
                //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //list_z.Add(last_z);

                //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
                //last_z = WidthCantilever + z_incr;
                //do
                //{
                //    flag = false;
                //    for (i = 0; i < list_z.Count; i++)
                //    {
                //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                //        {
                //            flag = true;
                //            break;
                //        }
                //    }

                //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
                //        list_z.Add(last_z);
                //    last_z += z_incr;
                //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //} while (last_z <= WidthBridge);


                list_z.Clear();
                list_z.Add(0);
                list_z.Add(Cantilever_Width);
                list_z.Add(WidthBridge - Cantilever_Width);


                list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
                list_z.Add(WidthBridge);



                //list_z.Add(2.5);
                //list_z.Add(2.5 + 2.0);
                //list_z.Add(4.5 + 4.0);
                //list_z.Add(4.5 + 4.0 + 2.0);
                //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


                list_z.Sort();
                #endregion Z Coordinates
            }
            list_z.Add(0);

            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];





            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }

            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            if (true)
            {
                JointNodeCollection verticals = new JointNodeCollection();
                #region Left Tower 1

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[0];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
                #endregion Left Tower 1

                if (false)
                {
                    #region Right Tower 1

                    for (i = 1; i < list_y.Count; i++)
                    {
                        nd = new JointNode();

                        nd.Y = list_y[i];
                        nd.Z = list_z[list_z.Count - 2];
                        nd.X = L1;

                        nd.NodeNo = Joints.JointNodes.Count + 1;
                        Joints.Add(nd);
                    }

                    #endregion Right Tower 1
                }


                #region Left Tower 2

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[0];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
                #endregion Left Tower 1

                if (false)
                {
                    #region Right Tower 1

                    for (i = 1; i < list_y.Count; i++)
                    {
                        nd = new JointNode();

                        nd.Y = list_y[i];
                        nd.Z = list_z[list_z.Count - 2];
                        nd.X = L1 + L2;

                        nd.NodeNo = Joints.JointNodes.Count + 1;
                        Joints.Add(nd);
                    }

                    #endregion Right Tower 1
                }

            }

            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();
            MemColls.Clear();
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }

            if (true)
            {
                #region Tower Members


                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }

                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }

                //for (i = 1; i < list_y.Count; i++)
                //{
                //    mem = new Member();
                //    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                //    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                //    mem.MemberNo = MemColls.Count + 1;
                //    MemColls.Add(mem);

                //    Tower_members.Add(mem.MemberNo);
                //}

                //for (i = 1; i < list_y.Count; i++)
                //{
                //    mem = new Member();
                //    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                //    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                //    mem.MemberNo = MemColls.Count + 1;
                //    MemColls.Add(mem);

                //    Tower_members.Add(mem.MemberNo);
                //}
                #endregion Tower Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[0]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[0]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                if (false)
                {
                    #region Cable Members

                    y_cables.Sort();
                    y_cables.Reverse();
                    for (i = 0; i < x_cable1.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.StartNode.NodeNo);

                    }


                    y_cables.Reverse();


                    for (i = 0; i < x_cable2.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.EndNode.NodeNo);
                    }




                    y_cables.Reverse();


                    for (i = 0; i < x_cable3.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.StartNode.NodeNo);
                    }



                    y_cables.Reverse();


                    for (i = 0; i < x_cable4.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.EndNode.NodeNo);
                    }



                    #endregion Cable Members
                }
            }
            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            //jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //side_supports.Add(jn.NodeNo);

            //jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //central_supports.Add(jn.NodeNo);

            //jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //central_supports.Add(jn.NodeNo);

            //jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));



            List<int> deff_mems = new List<int>();


            //Long_Girder_Members_Array




            if (false)
            {
                List<int> _supp_mems = new List<int>();
                List<int> _deff_mems = new List<int>();
                List<int> _L8_mems = new List<int>();
                List<int> _L4_mems = new List<int>();
                List<int> _3L8_mems = new List<int>();
                List<int> _L2_mems = new List<int>();

                #region Section Properties
                Long_Girder_members.Sort();


                foreach (var item in MemColls)
                {
                    for (i = 0; i < _Support_Dist.Count; i++)
                    {
                        try
                        {
                            if (item.StartNode.X >= _Support_Dist[i] && item.StartNode.X <= _Deff_Dist[i])
                            {
                                _supp_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _Deff_Dist[i] && item.StartNode.X <= _L8_Dist[i])
                            {
                                _deff_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _L8_Dist[i] && item.StartNode.X <= _L4_Dist[i])
                            {
                                _L8_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _L4_Dist[i] && item.StartNode.X <= _3L8_Dist[i])
                            {
                                _L4_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _3L8_Dist[i] && item.StartNode.X <= _L2_Dist[i])
                            {
                                _3L8_mems.Add(item.MemberNo); break;
                            }
                            else
                            {
                                _L2_mems.Add(item.MemberNo); break;
                            }
                        }
                        catch (Exception ex) { }

                    }

                }

                #endregion Section Properties
            }
        }

        public void WriteData_LiveLoad(string file_name, List<string> ll_txt_data, AASHTO_Box_Section aashto_box)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;
            if (ll_txt_data == null)
                list.Add("ASTRA SPACE EXTRADOSSED BRIDGE DECK ANALYSIS WITH DEAD LOAD");
            else
                list.Add("ASTRA SPACE EXTRADOSSED BRIDGE DECK ANALYSIS WITH MOVING LOAD");

            //list.Add("UNIT METER MTON");
            list.Add("UNIT FT KIP");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            //list.Add("SECTION PROPERTIES");
            if (aashto_box == null)
                list.AddRange(Get_Continuous_LL_Member_Properties_Data());
            else
                list.AddRange(Get_Continuous_LL_Member_Properties_Data(aashto_box));
            //list.Add(string.Format("MATERIAL CONSTANT"));
            //list.Add(string.Format("E 3162277 ALL"));
            //list.Add(string.Format("DENSITY CONCRETE ALL"));
            //list.Add(string.Format("POISSON CONCRETE ALL"));

            list.Add(string.Format("MEMBER CABLE"));
            list.Add(string.Format("_CABLES"));
            if (false)
            {
                #region Old
                list.Add(string.Format("MATERIAL CONSTANT"));
                //list.Add(string.Format("E 3834  {0}", MyList.Get_Array_Text(Long_Girder_members)));
                //list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Long_Girder_members)));
                list.Add(string.Format("E 6625152  {0}", MyList.Get_Array_Text(Long_Girder_members)));
                list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Long_Girder_members)));
                list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Long_Girder_members)));


                if (Cross_Girder_members.Count > 0)
                {
                    list.Add(string.Format("MATERIAL CONSTANT"));
                    //list.Add(string.Format("E 3834  {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                    //list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                    list.Add(string.Format("E 6625152  {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                    list.Add(string.Format("DENSITY CONCRETE {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                    list.Add(string.Format("POISSON CONCRETE {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                }


                list.Add(string.Format("MATERIAL CONSTANT"));
                //list.Add(string.Format("E 3834  {0}", MyList.Get_Array_Text(Tower_members)));
                //list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Tower_members)));
                list.Add(string.Format("E 6625152  {0}", MyList.Get_Array_Text(Tower_members)));
                list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Tower_members)));
                list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Tower_members)));




                list.Add(string.Format("MATERIAL CONSTANT"));
                list.Add(string.Format("E 290000  {0}", MyList.Get_Array_Text(Cable_members)));
                list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Cable_members)));
                list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Cable_members)));

                //list.Add(string.Format("MATERIAL CONSTANTS "));
                //list.Add(string.Format("E 3150 ALL"));
                //list.Add(string.Format("DEN 0.000383 ALL"));



                //list.Add(string.Format("MATERIAL CONSTANTS "));
                //list.Add(string.Format("E 29000 ALL "));
                //list.Add(string.Format("DEN 0.000283 ALL"));
                //list.Add(string.Format("PR STEEL ALL"));


                //list.Add(string.Format("MATERIAL CONSTANTS"));
                //list.Add(string.Format("E 200 1 TO 47"));
                //list.Add(string.Format("DEN 0.078 1 TO 47"));


                //list.Add(string.Format("CONSTANTS"));
                //list.Add(string.Format("E 4200E3 ALL"));
                //list.Add(string.Format("CONSTANT"));
                //list.Add(string.Format("E 2.85E6 ALL"));
                //list.Add(string.Format("DENSITY CONCRETE ALL"));
                //list.Add(string.Format("POISSON CONCRETE ALL"));
                //list.Add(string.Format(""));

                #endregion Old
            }

            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E {0}  {1}", E_CONC, MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("E {0}  {1}", E_CONC, MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("E {0}  {1}", E_STEEL, MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("E {0}  {1}", E_CABLE, MyList.Get_Array_Text(Cable_members)));

            list.Add(string.Format("DENSITY {0} {1}", DEN_CONC, MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("DENSITY {0} {1}", DEN_CONC, MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("DENSITY {0} {1}", DEN_STEEL, MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("DENSITY {0} {1}", DEN_CABLE, MyList.Get_Array_Text(Cable_members)));

            list.Add(string.Format("POISSON {0} {1}", PR_CONC, MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("POISSON {0} {1}", PR_CONC, MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("POISSON {0} {1}", PR_STEEL, MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("POISSON {0} {1}", PR_CABLE, MyList.Get_Array_Text(Cable_members)));

            list.Add("SUPPORT");
            //list.Add("11 TO 15  116 TO 120 276 TO 280 381 TO 385 FIXED");
            string ss = MyList.Get_Array_Text(Supports);
            //list.Add(ss + " FIXED");

            if (_Rows == 1)
            {
                //list.Add(string.Format("{0} {1} FIXED", Supports[0], Supports[3]));
                //list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[2]));

                list.Add(string.Format("{0} {1} FIXED", Supports[0], Supports[3], Start_Support));
                list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[2], End_Support));
            }
            else
            {
                //list.Add(string.Format("{0} {1} FIXED", Supports[0], Supports[4]));
                ////list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[2]));
                ////list.Add(string.Format("{0} {1} FIXED BUT FX MZ", Supports[1], Supports[5]));
                ////list.Add(string.Format("{0} {1} FIXED BUT FX MZ", Supports[2], Supports[6]));


                //list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[5]));
                //list.Add(string.Format("{0} {1} PINNED", Supports[2], Supports[6]));
                //list.Add(string.Format("{0} {1} FIXED", Supports[3], Supports[7]));


                list.Add(string.Format("{0} {1} {2}", Supports[0], Supports[4], Start_Support));
                list.Add(string.Format("{0} {1}  {2}", Supports[1], Supports[5], Start_Support));
                list.Add(string.Format("{0} {1}  {2}", Supports[2], Supports[6], End_Support));
                list.Add(string.Format("{0} {1}  {2}", Supports[3], Supports[7], End_Support));

            }


            #region Chiranjit [2017 02 15]
            //foreach (var item in central_load_joints)
            //{
            //    //list.Add(string.Format("{0} PINNED", item));
            //    list.Add(string.Format("{0} FIXED BUT FX MZ", item));
            //}
            //foreach (var item in side_load_joints)
            //{
            //    //list.Add(string.Format("{0} PINNED", item));
            //    list.Add(string.Format("{0} FIXED BUT FX MZ", item));
            //}
            #endregion Chiranjit [2017 02 15]




            //list.Add(string.Format("LOAD 1 DL"));

            joint_loads = new List<string>();

            if(ADD_Selfweight)
                joint_loads.Add(string.Format("SELFWEIGHT Y -1.0"));
            joint_loads.Add(string.Format("LOAD 1 DEADLOAD"));


            //if (false)
            if (true)
            {
                #region Joint Loads
                joint_loads.Add(string.Format("JOINT LOAD"));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(central_supports), (DLCSP + DLSSP)));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_supports), (DLSSP)));


                //foreach (var item in central_supports)
                //{
                //    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLCSP + DLSSP))));
                //    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));

                //}

                //foreach (var item in side_supports)
                //{
                //    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLSSP)));
                //    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));

                //}

                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(central_load_joints), (DLCJN)));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_load_joints), (DLSJN)));

                central_load_joints.Sort();
                side_load_joints.Sort();
                foreach (var item in central_load_joints)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN)));
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN * (Cable_x_dist + Init_dist) * 2))));
                    //joint_loads.Add(string.Format("{0} FX  -{1:f3}", item, ((DLSJN))));
                    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN + DLSJN)));

                }
                foreach (var item in side_load_joints)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLSJN)));
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN * 2 * (Cable_x_dist + Init_dist)))));
                    //joint_loads.Add(string.Format("{0} FX  -{1:f3}", item, ((DLSJN))));
                    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN + DLSJN)));
                }

                #endregion Joint Loads
            }



            #region Applied Load on Cable Member
            joint_loads.Add(string.Format("MEMBER LOAD"));
            ss = MyList.Get_Array_Text(Cable_members);

            joint_loads.Add(string.Format("{0} UNI GX {1:f3}", ss, DLSJN));

            #endregion Applied Load on Cable Member






            // Applied Member Load for All Center Load
            #region Member Load

            ////double applied_load = ((2 * DLSS + DLCS) / (L2 + L1 + L3)) / 3;
            double applied_load = ((2 * DLSS + DLCS) / (L2 + L1 + L3));

            #region Applied Load on Center Line
            //applied_load = ((2 * DLSS + DLCS) / (Cable_Nos * 2));
            applied_load = DLCSS;

            if (_Rows == 1)
            {
                joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                          Long_Girder_Members_Array[0, 0].MemberNo
                    //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                          , Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                    //, ((2*DLSS + DLCS)*10 / (L2))));
                          , applied_load));
            }
            else
            {
                if (IsCentral_Cable)
                {
                    joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                        Long_Girder_Members_Array[1, 0].MemberNo
                        //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                        , Long_Girder_Members_Array[1, _Columns - 2].MemberNo
                        //, ((2*DLSS + DLCS)*10 / (L2))));
                        , applied_load));
                    joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                        Long_Girder_Members_Array[3, 0].MemberNo
                        //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                        , Long_Girder_Members_Array[3, _Columns - 2].MemberNo
                        //, ((2*DLSS + DLCS)*10 / (L2))));
                        , applied_load));
                }
                else
                {
                    joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                       Long_Girder_Members_Array[2, 0].MemberNo
                        //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                       , Long_Girder_Members_Array[2, _Columns - 2].MemberNo
                        //, ((2*DLSS + DLCS)*10 / (L2))));
                       , applied_load / 12));
                }
            }



            //, ((2*DLSS + DLCS) / (L2+L1+L3))*(0.6)));
            #endregion Applied Load on Center Line



            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("{0} UNI GY -0.001", MyList.Get_Array_Text(Long_Girder_members)));
            #endregion Member Load

            list.AddRange(joint_loads.ToArray());

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.AddRange(ll_txt_data.ToArray());

            list.Add("PERFORM ANALYSIS");
            //list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            if (ll_txt_data != null)
            {
                string ll = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
                File.WriteAllLines(ll, ll_txt_data.ToArray());
            }
        }


        public void WriteData_LiveLoad_2017_02_16(string file_name, List<string> ll_txt_data)
        {
        }

        public List<string> Get_Continuous_LL_Member_Properties_Data()
        {
            return Get_Continuous_LL_Member_Properties_Data(null);
        }
        public List<string> Get_Continuous_LL_Member_Properties_Data(AASHTO_Box_Section aashto_box)
        {
            List<string> list = new List<string>();

            list.Add(string.Format("START GROUP DEFINITION"));



            //list.Add(string.Format("_LGIRDER " + MyList.Get_Array_Text(Long_Girder_members)));

            if (_Rows == 1)
            {
                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));
            }
            else
            {

                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));
                list.Add(string.Format("_LGIRDER2 {0} TO {1} ", Long_Girder_Members_Array[1, 0].MemberNo, Long_Girder_Members_Array[1, _Columns - 2].MemberNo));

                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));
                list.Add(string.Format("_LGIRDER2 {0} TO {1} ", Long_Girder_Members_Array[1, 0].MemberNo, Long_Girder_Members_Array[1, _Columns - 2].MemberNo));


                if (IsCentral_Cable)
                {

                    list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                }
                else
                {
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));

                    list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));

                    //list.Add(string.Format("_LGIRDER3 {0}", MyList.Get_Array_Text(_End_mems)));
                    //list.Add(string.Format("_LSUPP {0}", MyList.Get_Array_Text(_supp_mems)));
                    //list.Add(string.Format("_LDEFF {0}", MyList.Get_Array_Text(_deff_mems)));
                    //list.Add(string.Format("_L8 {0}", MyList.Get_Array_Text(_L8_mems)));
                    //list.Add(string.Format("_L4 {0}", MyList.Get_Array_Text(_L4_mems)));
                    //list.Add(string.Format("_3L8 {0}", MyList.Get_Array_Text(_3L8_mems)));
                    //list.Add(string.Format("_L2 {0}", MyList.Get_Array_Text(_L2_mems)));

                }
                list.Add(string.Format("_LGIRDER4 {0} TO {1} ", Long_Girder_Members_Array[3, 0].MemberNo, Long_Girder_Members_Array[3, _Columns - 2].MemberNo));
                list.Add(string.Format("_LGIRDER5 {0} TO {1} ", Long_Girder_Members_Array[4, 0].MemberNo, Long_Girder_Members_Array[4, _Columns - 2].MemberNo));

                list.Add(string.Format("_XGIRDER " + MyList.Get_Array_Text(Cross_Girder_members)));
            }
            list.Add(string.Format("_TOWERS " + MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("_CABLES " + MyList.Get_Array_Text(Cable_members)));



            //list.Add(string.Format("_LGIRDER2 13 TO 37 62 TO 86 111 TO 135 160 TO 184 209 TO 233 258 TO 282 307 TO 331"));
            //list.Add(string.Format("_XGIRDER1 344 TO 415 572 TO 643"));
            //list.Add(string.Format("_XGIRDER2 416 TO 571"));
            //list.Add(string.Format("_PYLON1 644 TO 673"));
            //list.Add(string.Format("_PYLON2 674 TO 703"));
            //list.Add(string.Format("_CABLE1 704 TO 727"));
            //list.Add(string.Format("_CABLE2 728 TO 751"));
            //list.Add(string.Format("_CABLE3 752 TO 775"));
            //list.Add(string.Format("_CABLE4 776 TO 799"));
            //list.Add(string.Format("_TIEBEAM1 800"));
            //list.Add(string.Format("_TIEBEAM2 801"));
            list.Add(string.Format("END GROUP DEFINITION"));

            list.Add(string.Format("SECTION PROPERTIES"));


            List<string> mem_nos = new List<string>();
            List<int> lval = new List<int>();
            List<int> lval_dummy = new List<int>();
            List<string> mem_nos_dummy = new List<string>();

            //int nd = 0;
            //for (int i = 1; i < _Columns; i++)
            //{
            //    lval.Clear();
            //    //lval_dummy.Clear();
            //    for (int j = 0; j < _Rows; j++)
            //    {
            //        //nd = i + 78*j;
            //        if (j == ((_Rows / 2)))
            //            lval.Add(i + (_Columns - 1) * j);
            //        else
            //            lval_dummy.Add(i + (_Columns - 1) * j);
            //    }
            //    mem_nos.Add(MyList.Get_Array_Text(lval));
            //    //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));
            //}
            //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));

            //list.Add(mem_nos[nd++] + string.Format("      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("1 TO {0}      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011", MemColls.Count));
            //list.Add(string.Format("_LGIRDER1    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));

            if (PSC_SECIONS != null)
            {
                //list.Add(string.Format("_LGIRDER    PRI   AX  {0:f4}   IX   {0:f4}   IY {0:f4}   IZ   {0:f4}", PSC_SECIONS.Area, PSC_SECIONS.Ixx, PSC_SECIONS.Iyy, PSC_SECIONS.Izz));
                //list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));

                if (_Rows == 1)
                {
                    list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                }
                else
                {
                    if (false)
                    {
                        #region Old
                        list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

                        if (IsCentral_Cable)
                        {
                            //list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            //list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            //list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                            list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0] / 2, PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));
                            list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));
                            list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0] / 2, PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));

                        }
                        else
                        {

                            list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            list.Add(string.Format("_LSUPP    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            list.Add(string.Format("_LDEFF    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[1], PSC_SECIONS.Ixx[1], PSC_SECIONS.Iyy[1], PSC_SECIONS.Iyy[1] + PSC_SECIONS.Ixx[1]));
                            list.Add(string.Format("_L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[2], PSC_SECIONS.Ixx[2], PSC_SECIONS.Iyy[2], PSC_SECIONS.Iyy[2] + PSC_SECIONS.Ixx[2]));
                            list.Add(string.Format("_L4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[3], PSC_SECIONS.Ixx[3], PSC_SECIONS.Iyy[3], PSC_SECIONS.Iyy[3] + PSC_SECIONS.Ixx[3]));
                            list.Add(string.Format("_3L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[4], PSC_SECIONS.Ixx[4], PSC_SECIONS.Iyy[4], PSC_SECIONS.Iyy[4] + PSC_SECIONS.Ixx[4]));
                            list.Add(string.Format("_L2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[5], PSC_SECIONS.Ixx[5], PSC_SECIONS.Iyy[5], PSC_SECIONS.Iyy[5] + PSC_SECIONS.Ixx[5]));
                            list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        }


                        list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        #endregion Old
                    }
                    else
                    {

                        #region New
                        list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _out_Ax, _out_Ix, _out_Iy, _out_Iz));
                        list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _inn_Ax, _inn_Ix, _inn_Iy, _inn_Iz));
                        list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _cen_Ax, _cen_Ix, _cen_Iy, _cen_Iz));
                        list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _inn_Ax, _inn_Ix, _inn_Iy, _inn_Iz));
                        list.Add(string.Format("_LGIRDER5    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _out_Ax, _out_Ix, _out_Iy, _out_Iz));
                        #endregion New
                    }
                }
                //list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

            }
            else if (aashto_box != null)
            {
                double _AX = aashto_box.AX / (12 * 12);
                double _IX = aashto_box.IXX / (12 * 12 * 12 * 12);
                double _IY = aashto_box.IYY / (12 * 12 * 12 * 12);
                double _IZ = _IX + _IY;

                if (_Rows == 1)
                {
                    list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}",
                        _AX,
                       _IX,
                        _IY,
                        _IZ));
                }
                else
                {
                    if (false)
                    {
                        #region Old

                        list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", _IX, _IY, _IZ));
                        //list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, _IZ));
                        //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

                        if (IsCentral_Cable)
                        {
                            //list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            //list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                            //list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                            list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX / 2, _IX / 2, _IY / 2, (_IZ) / 2));
                            list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", _IX / 2, _IY / 2, (_IZ) / 2));
                            list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX / 2, _IX / 2, _IY / 2, (_IZ) / 2));

                        }
                        else
                        {

                            list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, _IZ));
                            //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", _IX, _IY, (_IZ)));
                            list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            list.Add(string.Format("_LSUPP    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            list.Add(string.Format("_LDEFF    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            list.Add(string.Format("_L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            list.Add(string.Format("_L4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            list.Add(string.Format("_3L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            list.Add(string.Format("_L2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, (_IZ)));
                            //list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", _IX, _IY, (_IZ)));
                            list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, _IZ));
                        }


                        list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", _IX, _IY, (_IZ)));
                        //list.Add(string.Format("_LGIRDER5    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, _IZ));
                        #endregion Old
                    }
                    else
                    {

                        #region New
                        list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _out_Ax, _out_Ix, _out_Iy, _out_Iz));
                        list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _inn_Ax, _inn_Ix, _inn_Iy, _inn_Iz));
                        list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _cen_Ax, _cen_Ix, _cen_Iy, _cen_Iz));
                        list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _inn_Ax, _inn_Ix, _inn_Iy, _inn_Iz));
                        list.Add(string.Format("_LGIRDER5    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _out_Ax, _out_Ix, _out_Iy, _out_Iz));
                        #endregion New
                    }

                }
                //list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

            }
            else
            {
                list.Add(string.Format("_LGIRDER    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));
            }

            //list.Add(string.Format("_LGIRDER1    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            ////list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));
            ////list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));



            if (_Rows > 1)
            {
                //if (aashto_box != null)
                //{
                //    double _AX = aashto_box.AX / (12 * 12);
                //    double _IX = aashto_box.IXX / (12 * 12 * 12 * 12);
                //    double _IY = aashto_box.IYY / (12 * 12 * 12 * 12);
                //    double _IZ = _IX + _IY;

                //    //list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
                //    list.Add(string.Format("_XGIRDER    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _AX, _IX, _IY, _IZ));
                //}
                //else
                //{
                list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
                //}
            }

            //list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_TOWERS     PRI   AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100"));
            //list.Add(string.Format("_CABLES     PRI   AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005"));

            MemberSectionProperty sec = new MemberSectionProperty();
            sec.Depth = Tower_Depth;
            sec.Breadth = Tower_Breadth;
            sec.AppliedSection = eAppliedSection.Reactangular_Section;
            sec.Calculate_Moment_Of_Inertia();

            //list.Add(string.Format("_TOWERS     PRI   AX 0.576  IX 0.142   IY 0.0335   IZ 0.150"));
            list.Add(string.Format("_TOWERS     PRI   AX {0:f3}  IX {1:f3}   IY {2:f3}   IZ {3:f3}", sec.AX_Area, sec.IX, sec.IY, sec.IZ));
            //list.Add(string.Format("_CABLES     PRI   AX 0.001   IX 0.00002   IY 0.00002   IZ 0.00005"));

            sec = new MemberSectionProperty();
            sec.Diameter = CABLES_Dia;
            sec.AppliedSection = eAppliedSection.Circular_Section;
            sec.Calculate_Moment_Of_Inertia();

            list.Add(string.Format("_CABLES     PRI   AX {0:f3}  IX {1:f3}   IY {2:f3}   IZ {3:f3}", sec.AX_Area, sec.IX, sec.IY, sec.IZ));
            return list;
        }
        public double Tower_Depth = 3.28;
        public double Tower_Breadth = 3.28;
        public double CABLES_Dia = 0.5;


        public List<string> joint_loads = new List<string>();

        public int Cable_Nos { get; set; }
        public double Init_dist { get; set; }
        public double Cable_x_dist { get; set; }
        public double Cable_y_dist { get; set; }
        public double Cable_intv_dist { get; set; }
        public double Tower_height { get; set; }


        public double L1 { get; set; }

        public double L2 { get; set; }

        public double L3 { get; set; }

        public double Spacing_Cross_Girder { get; set; }

        public double Spacing_Long_Girder { get; set; }

        public double Skew_Angle { get; set; }


        public double cd { get; set; }
        public double Bt { get; set; }
        public double Dt { get; set; }
        public double tw { get; set; }


        public double DLSSP;
        public double DLCSP;

        public double DLSJN; //Side Span Joint Node Load
        public double DLCJN;//Centre Span Joint Node Load

        public double Cantilever_Width;

        public double Support_Distance;
        public double Effective_Distance;


        #region Distances
        public List<double> _Support_Dist { get; set; }
        public List<double> _Deff_Dist { get; set; }
        public List<double> _L8_Dist { get; set; }
        public List<double> _L4_Dist { get; set; }
        public List<double> _3L8_Dist { get; set; }
        public List<double> _L2_Dist { get; set; }

        #endregion Distances


        public double AVG_CS, Vol_CS, DLCS, DLCCS, NCCS, Vol_Side, DLSS, DLCSS, NCSS;

        List<string> Member_Loads = new List<string>();

        public double _L1, _L2, _B1, _B2, _H1, _H2, _NCAB, _D1, _D2, _D3, _D4, _UNIT_WGT, _Percentage;

        public List<string> Load_Calculation()
        {
            return Load_Calculation(null);
        }
        public List<string> Load_Calculation(AASHTO_Box_Section aastho_box)
        {
            List<string> list = new List<string>();


            _L1 = 100.0;
            _L2 = 65.0;
            _B1 = 9.75;
            _B2 = 1.925;
            _H1 = 12.0;
            _H2 = 4.4;
            _NCAB = 6.0;
            _D1 = 13.5;
            _D2 = 6.0;

            _L1 = L2;
            _L2 = L1;
            _B1 = WidthBridge;
            _B2 = Cantilever_Width;


            _H1 = Tower_height;
            _H2 = Cable_y_dist;

            _NCAB = Cable_Nos;

            //_D1 = Ca;




            _D1 = Init_dist;
            _D2 = Cable_x_dist;



            _UNIT_WGT = 0.15;
            _Percentage = 40.0;
            //_Percentage = 100.0;


            list.Add(string.Format(" "));
            list.Add(string.Format(""));
            list.Add(string.Format("L1 = {0} ft", _L1));
            list.Add(string.Format("L2 = {0} ft", _L2));
            list.Add(string.Format("B1 = {0} ft", _B1));
            list.Add(string.Format("B2 = {0} ft", _B2));
            list.Add(string.Format("H1 = {0} ft", _H1));
            list.Add(string.Format("H2 = {0} ft", _H2));
            list.Add(string.Format("Number of Cable on Each Side = NCAB = {0}", _NCAB));
            list.Add(string.Format("D1 = {0} ft", _D1));
            list.Add(string.Format("D2 = {0} ft", _D2));



            _D3 = _L1 - 2 * _D1 - 2 * (_NCAB - 1) * _D2;
            //list.Add(string.Format("D3 = L1 - 2xD1 - 2x5xD2 = 100 - 2x13.5 - 2x5x6.0 = 100.0 - 87.0 = 13.0 metres", _D3));
            list.Add(string.Format("D3 = L1 - 2xD1 - 2x(NCAB-1)xD2 = {0} - 2x{1} - 2x{2}x{3} = {4} ft", _L1, _D1, (_NCAB - 1), _D2, _D3));

            _D4 = _L2 - _D1 - (_NCAB - 1) * _D2;
            //list.Add(string.Format("D4 = L2 - D1 - 5xD2 = 100 - 13.5 - 5x6.0 = 100.0 - 43.5 = 56.5 metres", _D4));
            list.Add(string.Format("D4 = L2 - D1 - (NCAB-1)xD2 = {0} - {1} - {2}x{3} = {4} ft", _L2, _D1, (_NCAB - 1), _D2, _D4));



            list.Add(string.Format("Unit Weight of Box = {0} kcf", _UNIT_WGT));
            list.Add(string.Format("Percentage of Load in cables in central span = cp = {0}%", _Percentage));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CENTRAL SPAN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //PSC_SECIONS

            AVG_CS = 0.0;
            if (aastho_box == null)
            {
                foreach (var item in PSC_SECIONS.Area)
                {
                    AVG_CS += item;
                }
                AVG_CS = AVG_CS / PSC_SECIONS.Area.Count;
                list.Add(string.Format("Average Cross Section Area = ({0:f3}+{1:f3}+{2:f3}+{3:f3}+{4:f3}+{5:f3})/6 = {6:f3}/6 = {7:f3} Sq. ft.",
                                     PSC_SECIONS.Area[0],
                                     PSC_SECIONS.Area[1],
                                     PSC_SECIONS.Area[2],
                                     PSC_SECIONS.Area[3],
                                     PSC_SECIONS.Area[4],
                                     PSC_SECIONS.Area[5],
                                     AVG_CS * 6,
                                     AVG_CS
                                     ));
            }
            else
            {
                AVG_CS = aastho_box.AX / (12 * 12);
                list.Add(string.Format("Cross Section Area = {0:f3} Sq. ft.", AVG_CS));
            }
            //AVG_CS = (7.099 + 6.008 + 5.6080 + 4.966 + 4.966 + 4.966) / 6;

            //list.Add(string.Format("Average Cross Section Area = (7.099+6.008+5.6080+4.966+4.966+4.966)/6 = 33.613/6 = 5.602 Sq. m."));


            list.Add(string.Format("Length of Central Span = {0} metres", _L1));

            Vol_CS = AVG_CS * _L1;

            list.Add(string.Format("Volume of Central Span =  {0:f3} x {1} = {2:f3} Cu.ft", AVG_CS, _L1, Vol_CS));

            DLCS = Vol_CS * _UNIT_WGT;
            list.Add(string.Format("DEAD Load of Central Span = Volume x Unit Weight = DLCS = {0:f3} x {1} = {2:f3} kip", Vol_CS, _UNIT_WGT, DLCS));
            list.Add(string.Format(""));

            double cp = _Percentage / 100;

            DLCCS = cp * DLCS;

            list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = {0:f3} x {1:f3} = {2:f3} kip", cp, DLCS, DLCCS));

            NCCS = 4 * _NCAB;
            list.Add(string.Format("Number of Cable in Central Span = NCCS = 4 x NCAB = 4 x {0} = {1}", _NCAB, NCCS));


            DLCJN = DLCCS / NCCS;

            list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = {0:f3} / {1} = {2:f3} kip", DLCCS, NCCS, DLCJN));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load on Four supports ( at each two nodes on Each Two Central Span Piers)"));
            list.Add(string.Format(""));

            double sp = 1.0 - cp;
            list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-{0:f2} = {1:f2}", cp, sp));

            DLCSP = sp * DLCS / 4.0;
            list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLCS / Four Supports = {0:f2} x {1:f3} / 4 = {2:f3} kip", sp, DLCS, DLCSP));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SIDE SPAN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Side Span = {0:f2} metres", _L2));

            Vol_Side = AVG_CS * _L2;
            list.Add(string.Format("Volume of Central Span =  {0:f3} x {1:f2} = {2:f3} Cu.ft", AVG_CS, _L2, Vol_Side));

            DLSS = Vol_Side * _UNIT_WGT;

            list.Add(string.Format("DEAD Load of Side Span = DLSS = Volume x Unit Weight = {0:f3} x {1:f2} = {2:f3} kip", Vol_Side, _UNIT_WGT, DLSS));
            list.Add(string.Format(""));


            DLCSS = cp * DLSS;

            list.Add(string.Format("Dead Load on Cables in Side Span = DLCSS = cp x DLCS = {0:f2} x {1:f3} = {2:f3} kip", cp, DLSS, DLCSS));

            NCSS = 2 * _NCAB;
            list.Add(string.Format("Number of Cable in Side Span = NCSS = 2 x NCAB = 2 x {0} = {1}", _NCAB, NCSS));
            list.Add(string.Format(""));

            DLSJN = DLCSS / NCSS;

            list.Add(string.Format("Dead Load on Each Cable Node in Side Span = DLCSS/NCSS = {0:f3}/{1} = {2:f3} kip", DLCSS, NCSS, DLSJN));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load on Four supports (at each two nodes on Central Span and End Piers) "));


            list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-{0:f2} = {1:f2}", cp, sp));


            //DLSSP = sp * DLSS / 4.0;
            DLSSP = DLSS / 2.0;

            list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLSS / Four Supports = {0:f2} x {1:f2} / 4 = {2:f2} kip", sp, DLSS, DLSSP));
            list.Add(string.Format(""));
            list.Add(string.Format(""));






            //DLCSS = AVG_CS * L2 * (1-cp);


            //DLCSS = AVG_CS * _UNIT_WGT * (1 - cp);

            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (cp) / 10.0;


            //DLCSS = AVG_CS * _UNIT_WGT;

            //DLCSS = AVG_CS * _UNIT_WGT * (1 - cp);
            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (1 + cp) / 10.0;
            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (1 + 0.5) / 10.0;
            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (cp) / _NCAB;
            //DLCSS = AVG_CS * _UNIT_WGT ;
            //DLSJN = AVG_CS * _UNIT_WGT * (cp) * this.Cable_x_dist;


            DLCSS = AVG_CS * _UNIT_WGT * (1 - cp);

            DLSJN = AVG_CS * _UNIT_WGT * (cp);
            DLCJN = AVG_CS * _UNIT_WGT * (1 - cp);

            #region Sample
            //list.Add(string.Format(" "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("L1 = 100 metres", _L1));
            //list.Add(string.Format("L2 = 65 metres", _L2));
            //list.Add(string.Format("B1 = 9.75 metres", _B1));
            //list.Add(string.Format("B2 = 1.925 metres", _B2));
            //list.Add(string.Format("H1 = 12.0 metres", _H1));
            //list.Add(string.Format("H2 = 4.4 metres", _H2));
            //list.Add(string.Format("Number of Cable on Each Side = NCAB = 6", _NCAB));
            //list.Add(string.Format("D1 = 13.5 metres", _D1));
            //list.Add(string.Format("D2 = 6.0 metres", _D2));
            //list.Add(string.Format("D3 = L1 - 2xD1 - 2x5xD2 = 100 - 2x13.5 - 2x5x6.0 = 100.0 - 87.0 = 13.0 metres", _D3));
            //list.Add(string.Format("D4 = L2 - D1 - 5xD2 = 100 - 13.5 - 5x6.0 = 100.0 - 43.5 = 56.5 metres", _D4));
            //list.Add(string.Format("Unit Weight of Box = 2.5 Tons/Cu.m", _UNIT_WGT));
            //list.Add(string.Format("Percentage of Load in cables in central span = cp = 40%", _Percentage));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CENTRAL SPAN"));
            //list.Add(string.Format("Average Cross Section Area = (7.099+6.008+5.6080+4.966+4.966+4.966)/6 = 33.613/6 = 5.602 Sq. m."));

            //list.Add(string.Format("Length of Central Span = 100.0 metres"));
            //list.Add(string.Format("Volume of Central Span =  5.602 x 100.0 = 560.2 Cu.m"));
            //list.Add(string.Format("DEAD Load of Central Span = Volume x Unit Weight = DLCS = 560.2 x 2.5 = 1400.5 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = 0.40 x 1400.5 = 560.2 Tons"));
            //list.Add(string.Format("Number of Cable in Central Span = NCCS = 4 x NCAB = 4x6 = 24"));
            //list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = 560.2/24 = 23.342 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Four supports ( at each two nodes on Each Two Central Span Piers)"));
            //list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-0.4 = 0.6"));
            //list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLCS / Four Supports = 0.6 x 1400.5 / 4 = 210.075 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("SIDE SPAN"));
            //list.Add(string.Format("Length of Side Span = 65.0 metres"));
            //list.Add(string.Format("Volume of Central Span =  5.602 x 65.0 = 364.13 Cu.m"));
            //list.Add(string.Format("DEAD Load of Side Span = Volume x Unit Weight = DLSS = 364.13 x 2.5 = 910.325 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = 0.40 x 910.325 = 364.13 Tons"));
            //list.Add(string.Format("Number of Cable in Central Span = NCCS = 2 x NCAB = 2x6 = 12"));
            //list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = 364.13/12 = 30.34 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Four supports (at each two nodes on Central Span and End Piers) "));
            //list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-0.4 = 0.6"));
            //list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLSS / Four Supports = 0.6 x 910.325 / 4 = 136.55 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            #endregion
            return list;
        }

    }


}
