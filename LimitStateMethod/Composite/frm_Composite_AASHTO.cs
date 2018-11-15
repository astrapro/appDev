using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.ProcessList;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign.PSC_I_Girder;
using BridgeAnalysisDesign.Composite;
using LimitStateMethod.RCC_T_Girder;
using AstraAccess.SAP_Classes;
using AstraAccess.ADOC;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace LimitStateMethod.Composite
{
    public partial class frm_Composite_AASHTO : Form
    {
        //const string Title = "ANALYSIS OF COMPOSITE BRIDGE (LIMIT STATE METHOD)";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "COMPOSITE BRIDGE LIMIT STATE [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "COMPOSITE BRIDGE LIMIT STATE [LRFD]";
                return "COMPOSITE BRIDGE LIMIT STATE [IRC]";
            }
        }

        Composite_AASHTO_Analysis Bridge_Analysis = null;

        IApplication iApp = null;
        Composite_Girder_LS Deck = null;

        Steel_Girder_Section_AASHTO steel_section;

        CompositeSection_AASHTO Comp_sections { get; set; }

        string Left_support = "";
        string Right_support = "";
        public List<string> Results { get; set; }
        public frm_Composite_AASHTO(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            IsCreate_Data = true;
            Results = new List<string>();
            steel_section = new Steel_Girder_Section_AASHTO();
            Comp_sections = new CompositeSection_AASHTO();
            Bridge_Analysis = new Composite_AASHTO_Analysis(iApp);

        }

        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "Process\\ANALYSIS_RESULT.TXT");
            }
        }


        public string Worksheet_Folder
        {
            get
            {
                //if (Path.GetFileName(user_path) == Project_Name)
                //{
                //    if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                //        Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                //}
                //return Path.Combine(user_path, "Worksheet_Design");
                return user_path;
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
        public bool IsCreate_Data { get; set; }
        public string Analysis_Path
        {
            get
            {

                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, Title)))
                    return Path.Combine(iApp.LastDesignWorkingFolder, Title);

                return iApp.LastDesignWorkingFolder;

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

        #region Composite Analysis Form Events
        #endregion
        private void Default_Input_Data(object sender, EventArgs e)
        {
            Bridge_Analysis.Joints = new JointNodeCollection();
            Bridge_Analysis.MemColls = new MemberCollection();
            Button_Enable_Disable();

            //dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 2.75, 0.2, 1.179);
            //dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 6.25, 0.2, 1.179);
            //dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 9.75, 0.2, 1.179);


        }

        //Chiranjit [2013 06 26]
        public void Open_Create_Data()
        {
            Analysis_Initialize_InputData();
            try
            {
                //if (IsCreate_Data)
                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                //return;

                string usp = Path.Combine(user_path, "ANALYSIS PROCESS");

                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);


                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");

                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);


                Bridge_Analysis.Steel_Section = Comp_sections;




                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);


                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);

                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File);
                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File);
                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File);

                Ana_Write_Deck_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Deck_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Deck_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Deck_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File, false, true);
                Ana_Write_Deck_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File, false, true);

                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);
                string ll_txt = Bridge_Analysis.LiveLoad_File;
                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);
                if (Bridge_Analysis.Live_Load_List == null) return;
            }
            catch (Exception ex) { }

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
        void Create_Data()
        {
            Analysis_Initialize_InputData();

            Write_All_Data(false);


            string usp = Path.Combine(user_path, "ANALYSIS PROCESS");

            if (!Directory.Exists(usp))
                Directory.CreateDirectory(usp);

            string inp_file = Path.Combine(usp, "INPUT_DATA.TXT");

            //Calculate_Load_Computation();
            Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
            Bridge_Analysis.Start_Support = Start_Support_Text;
            Bridge_Analysis.End_Support = END_Support_Text;
            if (iApp.DesignStandard == eDesignStandard.IndianStandard || iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                LONG_GIRDER_LL_TXT();
                if (Curve_Radius > 0)
                {
                    Bridge_Analysis.CreateData_Straight();

                    //Bridge_Analysis.WriteData_Orthotropic_Analysis("", false);

                    #region Chiranjit [2014 09 08] Indian Standard


                    Bridge_Analysis.Steel_Section = Comp_sections;


                    Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                        Bridge_Analysis.Inner_Girders_as_String,
                        Bridge_Analysis.joints_list_for_load);

                    Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.Straight_Deck_DL_File);
                    Ana_Write_Deck_Load_Data(Bridge_Analysis.Straight_Deck_DL_File, false, true);

                    Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.Straight_Girder_DL_File);
                    Ana_Write_Girder_Load_Data(Bridge_Analysis.Straight_Girder_DL_File, false, true);


                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Straight_LL_File);
                    Ana_Write_Deck_Load_Data(Bridge_Analysis.Straight_LL_File, true, false);


                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Straight_TL_File);
                    Ana_Write_Deck_Load_Data(Bridge_Analysis.Straight_TL_File, true, true);

                    #region Chiranjit [2014 10 2]

                    if (long_ll.Count > 0)
                    {
                        //Bridge_Analysis.Live_Load_List = long_ll;
                        Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(long_ll);
                    }
                    for (int i = 0; i < all_loads.Count; i++)
                    {
                        Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), long_ll);
                        //Ana_Write_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), true, false);
                        //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), true, false, i+1);
                        //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_DL_File, false, true, 0);
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), true, false, i + 1);
                    }

                    #endregion Chiranjit [2014 10 2]




                    #endregion

                    Bridge_Analysis.CreateData_AASHTO();
                }
                else
                {
                    Bridge_Analysis.Skew_Angle = Skew;
                    Bridge_Analysis.CreateData_Straight();
                }

           

                Bridge_Analysis.WriteData(inp_file);
                Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);

                //Ana_Write_Load_Data();

                Bridge_Analysis.Steel_Section = Comp_sections;


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);


                Bridge_Analysis.WriteData_Total_Analysis(inp_file);
                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);

                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File);
                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File);



                Ana_Write_Deck_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Deck_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                //Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Deck_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File, false, true);
                Ana_Write_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File, false, true);
                Ana_Write_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);

                Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 2);


                cmb_long_open_file_process.Items.Clear();
                cmb_long_open_file_process.Items.Add("DEAD LOAD DECK ANALYSIS");
                cmb_long_open_file_process.Items.Add("DEAD LOAD GIRDER ANALYSIS");
                cmb_long_open_file_process.Items.Add("LIVE LOAD GIRDER ANALYSIS");

                #region Chiranjit [2014 10 2]


                for (int i = 0; i < all_loads.Count; i++)
                {
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1), long_ll);
                    //Ana_Write_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1), true, false);
                    //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), true, false, i + 1);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, false), true, false, i + 1);


                    if (ll_comb.Count == all_loads.Count)
                    {
                        cmb_long_open_file_process.Items.Add("LIVE LOAD DECK ANALYSIS (" + ll_comb[i] + ")");
                    }
                    else
                        cmb_long_open_file_process.Items.Add("LIVE LOAD DECK ANALYSIS " + (i + 1));
                }

                //cmb_long_open_file_process.Items.Add("GIRDER ANALYSIS RESULTS");
                cmb_long_open_file_process.Items.Add("DL + LL ANALYSIS");

                #endregion Chiranjit [2014 10 2]





                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                Button_Enable_Disable();

                //MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

         

            }

            cmb_long_open_file_process.SelectedIndex = 0;

            cmb_long_open_file_analysis.Items.Clear();
            for (int i = 0; i < cmb_long_open_file_process.Items.Count; i++)
            {
                cmb_long_open_file_analysis.Items.Add(cmb_long_open_file_process.Items[i].ToString());
            }

            cmb_long_open_file_analysis.SelectedIndex = 0;

            //Create Orthotropic Data
            Bridge_Analysis.CreateData_Orthotropic();
            Bridge_Analysis.WriteData_Orthotropic_Analysis(Bridge_Analysis.Orthotropic_Input_File);
            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Orthotropic_Input_File, true, true, 1);

        }
        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            if (!Check_Project_Folder()) return;
            Analysis_Initialize_InputData();

            //Chiranjit [2012 07 13]
            try
            {

                if (IsCreate_Data)
                {
                    if (Path.GetFileName(user_path) != Project_Name) Create_Project();
                }
                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);

                Create_Data();

                MessageBox.Show(this, "Input Data Files for various Analysis Processes are created within the folder  \"" + Project_Name + "\".",
                  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Save_Input_Data();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            Button_Enable_Disable();
            Write_All_Data(false);

        }

        private void btn_Ana_view_report_Click(object sender, EventArgs e)
        {

            frm_Result_Option frm = new frm_Result_Option(true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                switch (frm.ResultOption)
                {
                    case frm_Result_Option.eResult_Option.Analysis_Result:
                        iApp.RunExe(Result_Report);
                        break;
                    case frm_Result_Option.eResult_Option.Dead_Load_Analysis_Report:
                        iApp.RunExe(Bridge_Analysis.DeadLoad_Deck_Analysis_Report);
                        break;
                    case frm_Result_Option.eResult_Option.Live_Load_Analysis_Report:
                        iApp.RunExe(Bridge_Analysis.LiveLoad_Analysis_Report);
                        break;
                    case frm_Result_Option.eResult_Option.Total_Load_Analysis_Report:
                    case frm_Result_Option.eResult_Option.Full_Analysis_Report:
                        iApp.RunExe(Bridge_Analysis.Total_Analysis_Report);
                        break;
                }
            }
        }

        private void btn_Ana_view_data_Click(object sender, EventArgs e)
        {
            string file_name = "";
            string ll_txt = "";
            ComboBox cmb = cmb_long_open_file_process;


            Button btn = sender as Button;


            if (btn == btn_view_data_1)
            {
                cmb = cmb_long_open_file_analysis;
            }



            #region Set File Name

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
            }
            else
            {
                if (cmb.SelectedIndex < cmb.Items.Count - 1)
                {
                    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
                }
                else
                {
                    file_name = Bridge_Analysis.TotalAnalysis_Input_File;
                }
            }
            #endregion Set File Name

            string st_file = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file_process.SelectedIndex, true);

            if (File.Exists(file_name))
            {
                if (Curve_Radius > 0 && File.Exists(file_name))
                {
                    string rad_file = Path.Combine(Path.GetDirectoryName(file_name), "radius.fil");
                    Environment.SetEnvironmentVariable("MOVINGLOAD", st_file);
                    File.WriteAllText(rad_file, Curve_Radius.ToString());
                    Environment.SetEnvironmentVariable("COMP_RAD", Curve_Radius.ToString());
                }
                else
                {
                    string rad_file = Path.Combine(Path.GetDirectoryName(file_name), "radius.fil");
                    if (File.Exists(rad_file)) File.Delete(rad_file);
                    Environment.SetEnvironmentVariable("COMP_RAD", "");
                }
            }
            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name || btn.Name == btn_view_data_1.Name)
            {
                iApp.View_Input_File(file_name);
            }
            else if (btn.Name == btn_view_preprocess.Name)
            {
                if (File.Exists(file_name))
                {
                    iApp.View_PreProcess(file_name);
                }
                //    Form f = null;
                //    if (chk_curve.Checked)
                //    {
                //        f = iApp.Form_ASTRA_TEXT_Data(file_name);
                //    }
                //    else
                //    {
                //        f = iApp.Form_ASTRA_TEXT_Data(file_name, false);
                //    }
                //    f.Owner = this;
                //    f.Show();
                //    btn_update_forces.Enabled = true;
                //}
            }
            else if (btn.Name == btn_view_postprocess.Name)
            {
                if (File.Exists(file_name))
                {
                    iApp.View_PostProcess(file_name);
                }
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);

                if (File.Exists(file_name))
                    iApp.RunExe(file_name);

            }
            else if (btn.Name == btn_View_Moving_Load.Name)
            {
                if (File.Exists(FILE_SUMMARY_RESULTS)) iApp.RunExe(FILE_SUMMARY_RESULTS);
            }
        }

        private void Read_Analysis_Results()
        {


            int i = 0;
            iApp.Progress_Works.Clear();
            do
            {

                string flPath = "";
                if (i == 0)
                {
                    flPath = Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File;
                }
                else if (i == 0)
                {
                    flPath = Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File;
                }
                else if (i == 2)
                {
                    flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                }
                else if (i > 0)
                {

                    flPath = cmb_long_open_file_process.Items[i].ToString().ToUpper();
                }


                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                i++;
            }
            while (i < (3 + all_loads.Count));



            #region Read Analysis Result
            Bridge_Analysis.Structure = null;

            Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Deck_Analysis_Report);

            BridgeMemberAnalysis DL_Analysis_Deck = Bridge_Analysis.Structure;

            BridgeMemberAnalysis DL_Analysis_Girder = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Girder_Analysis_Report); ;

            BridgeMemberAnalysis TL_Analysis_Girder = new BridgeMemberAnalysis(iApp, Bridge_Analysis.Total_Analysis_Report); ;


            Bridge_Analysis.All_Analysis.Clear();
            if (all_loads.Count > 0)
            {
                for (i = 0; i < all_loads.Count; i++)
                {
                    //Bridge_Analysis.All_Analysis.Add(new BridgeMemberAnalysis(iApp,
                    //    MyList.Get_Analysis_Report_File(Bridge_Analysis.GetAnalysis_Input_File(i + 3))));

                    Bridge_Analysis.All_Analysis.Add(new BridgeMemberAnalysis(iApp,
                        MyList.Get_Analysis_Report_File(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1))));

                }
            }

            BridgeMemberAnalysis LL_Analysis = Bridge_Analysis.All_Analysis[0];


            Bridge_Analysis.DL_Analysis_Deck = DL_Analysis_Deck;
            Bridge_Analysis.DL_Analysis_Girder = DL_Analysis_Girder;
            Bridge_Analysis.LL_Analysis = TL_Analysis_Girder;
            if(Curve_Radius > 0)
            {

                Bridge_Analysis.DL_Analysis_Deck.IsCurve = true;
                Bridge_Analysis.DL_Analysis_Girder.IsCurve = true;
                Bridge_Analysis.LL_Analysis.IsCurve = true;
            }
            else
            {
                Bridge_Analysis.DL_Analysis_Deck.IsCurve = false;
                Bridge_Analysis.DL_Analysis_Girder.IsCurve = false;
                Bridge_Analysis.LL_Analysis.IsCurve = false;
            }

            Show_Moment_Shear();

            string s1 = "";
            string s2 = "";



            s1 = Bridge_Analysis.support_left_joints;
            s2 = Bridge_Analysis.support_right_joints;

            //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
            double BB = B;


            NodeResultData nrd = Bridge_Analysis.Structure.Node_Displacements.Get_Max_Deflection();
            NodeResultData LL_nrd = LL_Analysis.Node_Displacements.Get_Max_Deflection();
            NodeResultData DL_nrd = DL_Analysis_Deck.Node_Displacements.Get_Max_Deflection();


            DL_Analysis_Rep = Bridge_Analysis.DeadLoad_Deck_Analysis_Report;
            LL_Analysis_Rep = Bridge_Analysis.LiveLoad_Analysis_Report;
            Supports = (s1 + " " + s2).Replace(",", " ");

            DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
            LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);
            Show_and_Save_Data_DeadLoad();



            #region Print Results


            Results.AddRange(rtb_load_cal.Lines);

            if (chk_curve.Checked)
            {
                Results.Add(string.Format(""));
                Results.Add(string.Format("Maximum Horizontal Reaction (Trans. Direction) = {0} Ton", txt_brg_max_HRT_Ton.Text));
                Results.Add(string.Format(""));
                Results.Add(string.Format("Maximum Horizontal Reaction (Long. Direction) = {0} Ton", txt_brg_max_HRL_Ton.Text));
                Results.Add(string.Format(""));
            }
            Results.Add(string.Format(""));


            #endregion

            txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
            txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

            Button_Enable_Disable();
            Text_Changed_Forces();
            Calculate_Interactive_Values();

            Button_Enable_Disable();
            Write_All_Data(false);
            iApp.Save_Form_Record(this, user_path);

            iApp.Progress_Works.Clear();

            #endregion
        }

        public void Clear_All_Results()
        {


            txt_max_vert_reac.Text = "";
            txt_max_Mx.Text = "";
            txt_max_Mz.Text = "";

            txt_final_vert_reac.Text = "";
            txt_final_Mx.Text = "";
            txt_final_Mz.Text = "";

            txt_sidl_final_vert_reac.Text = "";
            txt_sidl_final_Mx.Text = "";
            txt_sidl_final_Mz.Text = "";


            txt_dead_vert_reac_ton.Text = "";
            txt_live_vert_rec_Ton.Text = "";


            txt_ana_DLSR.Text = "";
            txt_ana_LLSR.Text = "";

            txt_brg_max_VR_Ton.Text = "";
            txt_brg_max_DL_Ton.Text = "";



            txt_brg_max_HRT_Ton.Text = "";
            txt_brg_max_HRL_Ton.Text = "";




            txt_ana_DLSR.Text = "";
            txt_ana_LLSR.Text = "";




            dgv_left_end_design_forces.Rows.Clear();
            dgv_right_end_design_forces.Rows.Clear();

            txt_dead_vert_reac_ton.Text = "";
            txt_live_vert_rec_Ton.Text = "";




            dgv_left_des_frc.Rows.Clear();
            dgv_right_des_frc.Rows.Clear();


            txt_final_vert_reac.Text = "";
            txt_final_vert_rec_kN.Text = "";


            txt_max_vert_reac.Text = "";
            txt_max_vert_reac_kN.Text = "";

            this.Refresh();
        }

        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, false);
        }

        string Get_Straight_File(int stage_no)
        {

            string strght_path = Path.Combine(user_path, "TempAnalysis");

            if (Directory.Exists(strght_path)) Directory.CreateDirectory(strght_path);

            strght_path = Path.Combine(strght_path, "Straight.txt");

            return strght_path;


        }
        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            try
            {
                if (!Check_Project_Folder()) return;

                #region Process
                int i = 0;
                //Chiranjit [2012 07 13]
                Write_All_Data();


                LONG_GIRDER_LL_TXT();
                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();


                string flPath = Bridge_Analysis.Input_File;

                iApp.Progress_Works.Clear();


                //flPath = Bridge_Analysis.Straight_DL_File;

                //pd = new ProcessData();
                //pd.Process_File_Name = flPath;
                //pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                //pcol.Add(pd);

                //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");


                string straightPath = flPath;

                do
                {
                    pd = new ProcessData();


                    if (i == 0)
                    {
                        flPath = Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File;
                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Straight_Deck_DL_File;
                    }
                    else if (i == 1)
                    {
                        flPath = Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File;
                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Straight_Girder_DL_File;
                    }
                    else if (i == 2)
                    {
                        flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Straight_TL_File;
                    }

                    else if (i > 0)
                    {
                        flPath = Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i - 2);
                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i - 2, true);
                    }


                    if (Curve_Radius == 0)
                    {
                        pd.IS_Stage_File = false;
                        pd.Stage_File_Name = "";
                    }
                    else
                    {
                    }
                    //pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    if (i > 0)
                    {
                        if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                        {
                            pd.Process_Text = "PROCESS ANALYSIS FOR " + cmb_long_open_file_process.Items[i - 0].ToString().ToUpper();
                            pcol.Add(pd);
                            iApp.Progress_Works.Add("Reading Analysis Data from " + cmb_long_open_file_process.Items[i - 0].ToString().ToUpper() + " (ANALYSIS_REP.TXT)");
                        }
                        else
                        {
                            pd.Process_Text = "PROCESS ANALYSIS FOR " + cmb_long_open_file_process.Items[i].ToString().ToUpper();
                            pcol.Add(pd);
                            iApp.Progress_Works.Add("Reading Analysis Data from " + cmb_long_open_file_process.Items[i].ToString().ToUpper() + " (ANALYSIS_REP.TXT)");
                        }
                    }
                    else
                    {
                        pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                        pcol.Add(pd);
                        iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                    }
                    i++;
                }
                while (i < (3 + all_loads.Count));


                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                if (btn == btn_Ana_process_analysis) if (!iApp.Show_and_Run_Process_List(pcol)) return;


                Read_Analysis_Results();

                Button_Enable_Disable();
                Text_Changed_Forces();
                Calculate_Interactive_Values();

                //cmb_long_open_file_process.SelectedIndex = 1;
                //cmb_long_open_file_process.SelectedIndex = 0;

                Button_Enable_Disable();
                Write_All_Data(false);
                iApp.Save_Form_Record(this, user_path);

                iApp.Progress_Works.Clear();
                #endregion Process
            }
            catch (Exception ex) { }

        }
        void Ana_Write_Deck_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad)
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
            s += (add_DeadLoad ? " + SIDL " : "");
            s += (add_LiveLoad ? " + LL " : "");


            if (add_DeadLoad)
            {
                //load_lst.AddRange(txt_Ana_member_load.Lines);
                #region Deck Dead Loads
                load_lst.Add("LOAD 1 SLAB DEAD LOAD");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0}  UNI GY -0.014", Bridge_Analysis.Inner_Girders_as_String));
                load_lst.Add(string.Format("{0}  UNI GY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                load_lst.Add("LOAD 2 PARAPET DEAD LOAD");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0}  UNI GY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                load_lst.Add("LOAD 3 FUTURE WEARING SURFACE DEAD LOAD");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0}  UNI GY -0.014", Bridge_Analysis.Inner_Girders_as_String));
                load_lst.Add(string.Format("{0}  UNI GY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                #endregion Steel Girder Dead Loads
            }
            else
            {
                if (Transverse_load.Count > 1 && Curve_Radius > 0)
                {
                    load_lst.Add(string.Format("LOAD 1 TRANSVERSE LOAD"));
                    load_lst.AddRange(Transverse_load.ToArray());
                }
                else
                {
                    load_lst.Add("LOAD    1   " + s);
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add("1 TO 220 UNI GY -0.001");
                }

            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                //if (dgv_live_load.RowCount != 0)
                //    load_lst.AddRange(Get_MovingLoad_Data(Bridge_Analysis.Live_Load_List));
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        void Ana_Write_Girder_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad)
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
            s += (add_DeadLoad ? " + SIDL " : "");
            s += (add_LiveLoad ? " + LL " : "");


            if (add_DeadLoad)
            {
                #region Steel Girder Dead Loads
                load_lst.Add("LOAD 1 STEEL GIRDER");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY  -0.290", Bridge_Analysis.Inner_Girders_as_String));
                load_lst.Add(string.Format("{0} UNI GY  -0.290", Bridge_Analysis.Outer_Girders_as_String));
                load_lst.Add("LOAD 2 CONCRETE DECK AND HAUNCHES");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY  -1.14", Bridge_Analysis.Inner_Girders_as_String));
                load_lst.Add(string.Format("{0} UNI GY  -1.14", Bridge_Analysis.Outer_Girders_as_String));
                load_lst.Add("LOAD 3 OTHER DEAD LOADS ACTING ON GIRDER ALONE");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY  -0.214", Bridge_Analysis.Inner_Girders_as_String));
                load_lst.Add(string.Format("{0} UNI GY  -0.214", Bridge_Analysis.Outer_Girders_as_String));
                load_lst.Add("LOAD 4 CONCRETE PARAPETS");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY  -0.414", Bridge_Analysis.Outer_Girders_as_String));
                load_lst.Add("LOAD 5 FUTURE WEARING SURFACE");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY  -0.314", Bridge_Analysis.Inner_Girders_as_String));
                load_lst.Add(string.Format("{0} UNI GY  -0.314", Bridge_Analysis.Outer_Girders_as_String));
                #endregion Steel Girder Dead Loads

            }
            else
            {
                if (Transverse_load.Count > 1 && Curve_Radius > 0)
                {
                    load_lst.Add(string.Format("LOAD 1 TRANSVERSE LOAD"));
                    load_lst.AddRange(Transverse_load.ToArray());
                }
                else
                {
                    load_lst.Add("LOAD    1   " + s);
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add("1 TO 220 UNI GY -0.001");
                }

            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                //if (dgv_live_load.RowCount != 0)
                //    load_lst.AddRange(Get_MovingLoad_Data(Bridge_Analysis.Live_Load_List));
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Ana_Write_Load_Data()
        {
            string file_name = Bridge_Analysis.Input_File;

            if (!File.Exists(file_name)) return;

            List<string> list_member_load = new List<string>();
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
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
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
                    list_member_load.Add(inp_file_cont[i]);
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL + SIDL + LL";

            if (!load_lst.Contains("LOAD    1   " + s)) load_lst.Add("LOAD    1   " + s);
            if (!load_lst.Contains("MEMBER LOAD")) load_lst.Add("MEMBER LOAD");

            if (true)
            {
                load_lst.AddRange(txt_Ana_member_load.Lines);

                //if (dgv_live_load.RowCount != 0)
                //{
                //    if (!File.Exists(Bridge_Analysis.LiveLoad_File))
                //    {
                //        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                //    }
                //    //Bridge_Analysis.LoadReadFromGrid(dgv_Ana_live_load);
                //}
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
            }
            //Chiranjit [2011 09 23]
            //Do not write Moving Load Data wheather user Remove all the data from the Data Grid Box
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            //if (dgv_live_load.RowCount != 0)
            load_lst.AddRange(Get_MovingLoad_Data(Bridge_Analysis.Live_Load_List));
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public void Show_ReadMemberLoad(string file_name)
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
                    if (mlist.Count == 3) txt_LL_load_gen.Text = mlist.StringList[2];
                    //dgv_live_load.Rows.Clear();
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
                        //dgv_live_load.Rows.Add(ld.TypeNo + " : " + ld.Code,
                        //    ld.X.ToString("0.000"), ld.Y.ToString("0.000"), ld.Z.ToString("0.000"), ld.XINC.ToString("0.000"), ld.ImpactFactor.ToString("0.000"));

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
            txt_Ana_member_load.Lines = list_member_load.ToArray();


        }
        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;
            //btn_Ana_create_data.Enabled = rbtn_create_analysis_file.Checked;
            Button_Enable_Disable();
        }
        private void txt_Ana_custom_LL_Xcrmt_TextChanged(object sender, EventArgs e)
        {
        }
        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text File (*.txt)|*.txt"; ofd.InitialDirectory = user_path;
                    //ofd.InitialDirectory = iApp.LastDesignWorkingFolder;
                    ofd.InitialDirectory = Analysis_Path;

                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        IsCreate_Data = false;

                        string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");

                        if (!File.Exists(chk_file)) chk_file = ofd.FileName;

                        Ana_OpenAnalysisFile(chk_file);
                        Show_ReadMemberLoad(Bridge_Analysis.TotalAnalysis_Input_File);
                        //Open_AnalysisFile(ofd.FileName);

                        //txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;
                        //iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
                        Read_All_Data();

                        //Chiranjit [2013 04 26]
                        iApp.Read_Form_Record(this, user_path);

                        Open_Create_Data();//Chiranjit [2013 06 25]



                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Button_Enable_Disable();
                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
        }

        private void dgv_Ana_SIDL_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btn_Ana_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
                //dgv_live_load.Rows.RemoveAt(dgv_live_load.CurrentRow.Index);
                //chk_ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_all_Click(object sender, EventArgs e)
        {
            //dgv_live_load.Rows.Clear();
            //chk_ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);

        }

        private void btn_Ana_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, true);
        }

        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            //grb_LL.Enabled = true;
            //grb_SIDL.Enabled = chk_Ana_active_SIDL.Checked;
        }
        void Analysis_Initialize_InputData()
        {
            Bridge_Analysis.Length = L;

            Bridge_Analysis.Spans = new List<double>();
            try
            {

                if (rbtn_multiSpan.Checked)
                {
                    MyList spans = new MyList(MyList.RemoveAllSpaces(txt_multiSpan.Text.Replace(',', ' ')), ' ');
                    for (int i = 0; i < spans.StringList.Count; i++)
                    {
                        Bridge_Analysis.Spans.Add(spans.GetDouble(i));
                    }
                }
                else
                {
                    Bridge_Analysis.Spans.Add(L);
                }
            }
            catch (Exception exx) { }
            Bridge_Analysis.Length = Bridge_Analysis.Total_Length;
            Bridge_Analysis.WidthBridge = B;
            Bridge_Analysis.Width_LeftCantilever = CL;
            Bridge_Analysis.Width_RightCantilever = CR;
            Bridge_Analysis.Skew_Angle = Skew;
            Bridge_Analysis.Effective_Depth = Deff;
            Bridge_Analysis.NMG = NMG;
            Bridge_Analysis.NCG = NCG;
            Bridge_Analysis.Ds = Ds;

            Bridge_Analysis.Radius = MyList.StringToDouble(txt_curve_radius.Text, 0.0);

            if (!chk_curve.Checked) Bridge_Analysis.Radius = 0;



            //Deck_Analysis.T_Long_Outer_Section = long_out_sec;
            //Deck_Analysis.T_Cross_Section = cross_sec;

            //Deck_Analysis.Length = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            //Deck_Analysis.WidthBridge = 6.0;


        }
        void Show_Moment_Shear()
        {

            
            List<string> list_results = new List<string>();


            MemberCollection mc = new MemberCollection(Bridge_Analysis.Structure.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Bridge_Analysis.Structure.Analysis.Joints;

            double supp_x_coor = Bridge_Analysis.Structure.Supports[0].X;

            //double L = Bridge_Analysis.Structure.Analysis.Length;
            double W = Bridge_Analysis.Structure.Analysis.Width;
            double val = L / 2;
            int i = 0;


            if (Bridge_Analysis._L2_inn_joints.Count == 0 || Skew > 0)
            {
                Bridge_Analysis.CreateData_Straight();
            }
            List<int> _L0_inn_joints = Bridge_Analysis._L0_inn_joints;
            List<int> _L1_inn_joints = Bridge_Analysis._L1_inn_joints;
            List<int> _L2_inn_joints = Bridge_Analysis._L2_inn_joints;
            List<int> _L3_inn_joints = Bridge_Analysis._L3_inn_joints;
            List<int> _L4_inn_joints = Bridge_Analysis._L4_inn_joints;
            List<int> _L5_inn_joints = Bridge_Analysis._L5_inn_joints;
            List<int> _L6_inn_joints = Bridge_Analysis._L6_inn_joints;
            List<int> _L7_inn_joints = Bridge_Analysis._L7_inn_joints;
            List<int> _L8_inn_joints = Bridge_Analysis._L8_inn_joints;
            List<int> _L9_inn_joints = Bridge_Analysis._L9_inn_joints;
            List<int> _L10_inn_joints = Bridge_Analysis._L10_inn_joints;


            List<int> _L0_out_joints = Bridge_Analysis._L0_out_joints;
            List<int> _L1_out_joints = Bridge_Analysis._L1_out_joints;
            List<int> _L2_out_joints = Bridge_Analysis._L2_out_joints;
            List<int> _L3_out_joints = Bridge_Analysis._L3_out_joints;
            List<int> _L4_out_joints = Bridge_Analysis._L4_out_joints;
            List<int> _L5_out_joints = Bridge_Analysis._L5_out_joints;
            List<int> _L6_out_joints = Bridge_Analysis._L6_out_joints;
            List<int> _L7_out_joints = Bridge_Analysis._L7_out_joints;
            List<int> _L8_out_joints = Bridge_Analysis._L8_out_joints;
            List<int> _L9_out_joints = Bridge_Analysis._L9_out_joints;
            List<int> _L10_out_joints = Bridge_Analysis._L10_out_joints;



            List<double> lst_frc = new List<double>();
            MaxForce mfc;


            string frmt = "{0,-10} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}";

            int loadcase = 1;
            list_results.Clear();



            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("Table 2-1 : Unfactored Dead Load Moments (K-FT/FT)"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));


            //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            //list_results.Add(string.Format("SLAB DEAD LOAD"));

            for (int j = 1; j < 4; j++)
            {

                loadcase = j;
                if (loadcase == 1)
                {

                    list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    list_results.Add(string.Format("SLAB DEAD LOAD"));
                }
                else if (loadcase == 2)
                {
                    list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    list_results.Add(string.Format("PARAPET DEAD LOAD"));

                }
                else if (loadcase == 3)
                {

                    list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    list_results.Add(string.Format("FWS DEAD LOAD"));
                }

                list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));

                list_results.Add(string.Format(frmt, "DISTANCE"
                      , "0.0"
                      , "0.1"
                      , "0.2"
                      , "0.3"
                      , "0.4"
                      , "0.5"
                      , "0.6"
                      , "0.7"
                      , "0.8"
                      , "0.9"
                      , "1.0"
                      ));
                list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));

                #region Read all
                //forces from Dry concrete
                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L0_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L1_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L2_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L3_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));


                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L4_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L5_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L6_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L7_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L8_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));


                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L9_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L10_out_joints, loadcase);
                //lst_frc.Add(Math.Abs(mfc.Force));
                lst_frc.Add((mfc.Force));



                #region Bay 1
                list_results.Add(string.Format(frmt, "BAY 1"
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Bay 1

                for (i = 0; i < _L10_inn_joints.Count; i++)
                {
                    #region Bay 2
                    lst_frc.Clear();

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L0_inn_joints[i], loadcase);
                    //txt_SUMM_I13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L1_inn_joints[i], loadcase);
                    //txt_SUMM_J13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L2_inn_joints[i], loadcase);
                    //txt_SUMM_K13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L3_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));


                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L4_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L5_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L6_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L7_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L8_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));


                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L9_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));

                    mfc = Bridge_Analysis.DL_Analysis_Deck.GetJoint_MomentForce(_L10_inn_joints[i], loadcase);
                    //txt_SUMM_L13.Text = mfc.Force.ToString();
                    lst_frc.Add(Math.Abs(mfc.Force));


                    //lst_frc.Sort();



                    list_results.Add(string.Format(frmt, "BAY " + (i + 2)
                        , lst_frc[0]
                        , lst_frc[1]
                        , lst_frc[2]
                        , lst_frc[3]
                        , lst_frc[4]
                        , lst_frc[5]
                        , lst_frc[6]
                        , lst_frc[7]
                        , lst_frc[8]
                        , lst_frc[9]
                        , lst_frc[10]

                        ));
                    #endregion Bay 2
                }



                list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                #endregion Read all

            }
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));


            var LL1 = Bridge_Analysis.All_Analysis[0];

            LL1.IsCurve = (Curve_Radius > 0);

            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("Table 2-2 Unfactored Live Load Moments (Excluding Dynamic Load Allowance) (K-FT)"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(""));

            #region Live Loads


            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("SINGLE TRUCK (MULTIPLE PRESENCE FACTOR OF 1.20 INCLUDED"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("MAXIMUM MOMENT"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));

            list_results.Add(string.Format(frmt, "DISTANCE"
                  , "0.0"
                  , "0.1"
                  , "0.2"
                  , "0.3"
                  , "0.4"
                  , "0.5"
                  , "0.6"
                  , "0.7"
                  , "0.8"
                  , "0.9"
                  , "1.0"
                  ));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));


            lst_frc.Clear();


            #region Read all Max Moment
            //forces from Dry concrete
            mfc = LL1.GetJoint_Max_Hogging(_L0_out_joints, false);
            //txt_SUMM_I13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L1_out_joints, false);
            //txt_SUMM_J13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L2_out_joints, false);
            //txt_SUMM_K13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L3_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Hogging(_L4_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L5_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L6_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L7_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L8_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Hogging(_L9_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L10_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            #region Bay 1
            list_results.Add(string.Format(frmt, "BAY 1"
                , lst_frc[0]
                , lst_frc[1]
                , lst_frc[2]
                , lst_frc[3]
                , lst_frc[4]
                , lst_frc[5]
                , lst_frc[6]
                , lst_frc[7]
                , lst_frc[8]
                , lst_frc[9]
                , lst_frc[10]

                ));
            #endregion Bay 1

            for (i = 0; i < _L10_inn_joints.Count; i++)
            {
                #region Bay 2
                lst_frc.Clear();

                mfc = LL1.GetJoint_Max_Hogging(_L0_inn_joints[i], false);
                //txt_SUMM_I13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Hogging(_L1_inn_joints[i], false);
                //txt_SUMM_J13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L2_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L3_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_MomentForce(_L4_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L5_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L6_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L7_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L8_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_MomentForce(_L9_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L10_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));


                //lst_frc.Sort();



                list_results.Add(string.Format(frmt, "BAY " + (i + 2)
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Bay 2
            }



            #endregion Read all  Max Moment

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("MINIMUM MOMENT"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));



            #region Read all Min Moment
            //forces from Dry concrete
            mfc = LL1.GetJoint_Max_Sagging(_L0_out_joints, false);
            //txt_SUMM_I13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L1_out_joints, false);
            //txt_SUMM_J13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L2_out_joints, false);
            //txt_SUMM_K13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L3_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Sagging(_L4_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L5_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L6_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L7_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L8_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Sagging(_L9_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L10_out_joints, false);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            #region Bay 1
            list_results.Add(string.Format(frmt, "BAY 1"
                , lst_frc[0]
                , lst_frc[1]
                , lst_frc[2]
                , lst_frc[3]
                , lst_frc[4]
                , lst_frc[5]
                , lst_frc[6]
                , lst_frc[7]
                , lst_frc[8]
                , lst_frc[9]
                , lst_frc[10]

                ));
            #endregion Bay 1

            for (i = 0; i < _L10_inn_joints.Count; i++)
            {
                #region Bay 2
                lst_frc.Clear();

                mfc = LL1.GetJoint_Max_Sagging(_L0_inn_joints[i], false);
                //txt_SUMM_I13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L1_inn_joints[i], false);
                //txt_SUMM_J13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L2_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L3_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_Max_Sagging(_L4_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L5_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L6_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L7_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L8_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_Max_Sagging(_L9_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L10_inn_joints[i], false);
                lst_frc.Add(Math.Abs(mfc.Force));


                //lst_frc.Sort();



                list_results.Add(string.Format(frmt, "BAY " + (i + 2)
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Bay 2
            }



            #endregion Read all  Min Moment



            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));

            #endregion Live Loads
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("TWO TRUCKS (MULTIPLE PRESENCE FACTOR OF 1.00 INCLUDED)"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("MAXIMUM MOMENT"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));


            LL1 = Bridge_Analysis.All_Analysis[1];
            LL1.IsCurve = Curve_Radius > 0;

            #region Live Loads

            list_results.Add(string.Format(frmt, "DISTANCE"
                  , "0.0"
                  , "0.1"
                  , "0.2"
                  , "0.3"
                  , "0.4"
                  , "0.5"
                  , "0.6"
                  , "0.7"
                  , "0.8"
                  , "0.9"
                  , "1.0"
                  ));


            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));


            lst_frc.Clear();



            #region Read all Max Moment
            //forces from Dry concrete
            mfc = LL1.GetJoint_Max_Hogging(_L0_out_joints, true);
            //txt_SUMM_I13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L1_out_joints, true);
            //txt_SUMM_J13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L2_out_joints, true);
            //txt_SUMM_K13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L3_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Hogging(_L4_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L5_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L6_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L7_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L8_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Hogging(_L9_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Hogging(_L10_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            #region Bay 1
            list_results.Add(string.Format(frmt, "BAY 1"
                , lst_frc[0]
                , lst_frc[1]
                , lst_frc[2]
                , lst_frc[3]
                , lst_frc[4]
                , lst_frc[5]
                , lst_frc[6]
                , lst_frc[7]
                , lst_frc[8]
                , lst_frc[9]
                , lst_frc[10]

                ));
            #endregion Bay 1

            for (i = 0; i < _L10_inn_joints.Count; i++)
            {
                #region Bay 2
                lst_frc.Clear();

                mfc = LL1.GetJoint_Max_Hogging(_L0_inn_joints[i], true);
                //txt_SUMM_I13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Hogging(_L1_inn_joints[i], true);
                //txt_SUMM_J13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L2_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L3_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_MomentForce(_L4_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L5_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L6_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L7_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L8_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_MomentForce(_L9_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_MomentForce(_L10_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                //lst_frc.Sort();



                list_results.Add(string.Format(frmt, "BAY " + (i + 2)
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Bay 2
            }



            #endregion Read all  Max Moment

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("MINIMUM MOMENT"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));

            #region Read all Min Moment
            //forces from Dry concrete
            mfc = LL1.GetJoint_Max_Sagging(_L0_out_joints, true);
            //txt_SUMM_I13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L1_out_joints, true);
            //txt_SUMM_J13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L2_out_joints, true);
            //txt_SUMM_K13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L3_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Sagging(_L4_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L5_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L6_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L7_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L8_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = LL1.GetJoint_Max_Sagging(_L9_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = LL1.GetJoint_Max_Sagging(_L10_out_joints, true);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            #region Bay 1
            list_results.Add(string.Format(frmt, "BAY 1"
                , lst_frc[0]
                , lst_frc[1]
                , lst_frc[2]
                , lst_frc[3]
                , lst_frc[4]
                , lst_frc[5]
                , lst_frc[6]
                , lst_frc[7]
                , lst_frc[8]
                , lst_frc[9]
                , lst_frc[10]

                ));
            #endregion Bay 1

            for (i = 0; i < _L10_inn_joints.Count; i++)
            {
                #region Bay 2
                lst_frc.Clear();

                mfc = LL1.GetJoint_Max_Sagging(_L0_inn_joints[i], true);
                //txt_SUMM_I13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L1_inn_joints[i], true);
                //txt_SUMM_J13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L2_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L3_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_Max_Sagging(_L4_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L5_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L6_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L7_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L8_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = LL1.GetJoint_Max_Sagging(_L9_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = LL1.GetJoint_Max_Sagging(_L10_inn_joints[i], true);
                lst_frc.Add(Math.Abs(mfc.Force));


                //lst_frc.Sort();



                list_results.Add(string.Format(frmt, "BAY " + (i + 2)
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Bay 2
            }



            #endregion Read all  Min Moment

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Live Loads



            //frmt = "{0,-40} {1,7:f3} {2,7:f3} {3,7:f3} {4,7:f3} {5,7:f3} {6,7:f3} {7,7:f3} {8,7:f3} {9,7:f3} {10,7:f3} {11,7:f3}";
            frmt = "{0,-40} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}";

            #region Dead Load Shears
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("Table 3-7 Dead Load Moments (Kip-feet)"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(frmt, "Dead Load Component"
                  , "0.0L"
                  , "0.1L"
                  , "0.2L"
                  , "0.3L"
                  , "0.4L"
                  , "0.5L"
                  , "0.6L"
                  , "0.7L"
                  , "0.8L"
                  , "0.9L"
                  , "1.0L"
                  ));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            for (int j = 1; j < 6; j++)
            {


                loadcase = j;

                lst_frc.Clear();


                #region Read all
                //forces from Dry concrete
                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L0_out_joints, loadcase);
                //txt_SUMM_I13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L1_out_joints, loadcase);
                //txt_SUMM_J13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L2_out_joints, loadcase);
                //txt_SUMM_K13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L3_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L4_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L5_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L6_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L7_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L8_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L9_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_MomentForce(_L10_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));



                string kStr = "";

                if (loadcase == 1)
                {

                    kStr = (string.Format("STEEL GIRDER"));
                }
                else if (loadcase == 2)
                {
                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    kStr = (string.Format("CONCRETE DECK & HAUNCHES"));
                    //list_results.Add(string.Format("CONCRETE DECK & HAUNCHES"));

                }
                else if (loadcase == 3)
                {

                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    //list_results.Add(string.Format("OTHER DEAD LOADS ACTING ON GIRDER ALONE"));
                    kStr = (string.Format("OTHER DEAD LOADS ACTING ON GIRDER ALONE"));
                }
                else if (loadcase == 4)
                {

                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    //list_results.Add(string.Format("CONCRETE PARAPETS"));
                    kStr = (string.Format("CONCRETE PARAPETS"));
                }
                else if (loadcase == 5)
                {

                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    //list_results.Add(string.Format("FUTURE WEARING SURFACE"));
                    kStr = (string.Format("FUTURE WEARING SURFACE"));
                }
                //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));




                #region Steel girder

                list_results.Add(string.Format(frmt, kStr
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Steel girder
                #endregion Read all

            }

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            #endregion Dead Load Shears

            #region Dead Load Shears
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("Table 3-8 Dead Load Shears (Kips)"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(frmt, "Dead Load Component"
                  , "0.0L"
                  , "0.1L"
                  , "0.2L"
                  , "0.3L"
                  , "0.4L"
                  , "0.5L"
                  , "0.6L"
                  , "0.7L"
                  , "0.8L"
                  , "0.9L"
                  , "1.0L"
                  ));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            for (int j = 1; j < 6; j++)
            {

                loadcase = j;
                lst_frc.Clear();

                #region Read all
                //forces from Dry concrete
                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L0_out_joints, loadcase);
                //txt_SUMM_I13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L1_out_joints, loadcase);
                //txt_SUMM_J13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L2_out_joints, loadcase);
                //txt_SUMM_K13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L3_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L4_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L5_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L6_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L7_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L8_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));


                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L9_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));

                mfc = Bridge_Analysis.DL_Analysis_Girder.GetJoint_ShearForce(_L10_out_joints, loadcase);
                //txt_SUMM_L13.Text = mfc.Force.ToString();
                lst_frc.Add(Math.Abs(mfc.Force));


                //lst_frc.Sort();





                string kStr = "";

                if (loadcase == 1)
                {

                    kStr = (string.Format("STEEL GIRDER"));
                }
                else if (loadcase == 2)
                {
                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    kStr = (string.Format("CONCRETE DECK & HAUNCHES"));
                    //list_results.Add(string.Format("CONCRETE DECK & HAUNCHES"));

                }
                else if (loadcase == 3)
                {

                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    //list_results.Add(string.Format("OTHER DEAD LOADS ACTING ON GIRDER ALONE"));
                    kStr = (string.Format("OTHER DEAD LOADS ACTING ON GIRDER ALONE"));
                }
                else if (loadcase == 4)
                {

                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    //list_results.Add(string.Format("CONCRETE PARAPETS"));
                    kStr = (string.Format("CONCRETE PARAPETS"));
                }
                else if (loadcase == 5)
                {

                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    //list_results.Add(string.Format("FUTURE WEARING SURFACE"));
                    kStr = (string.Format("FUTURE WEARING SURFACE"));
                }
                //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));




                #region Steel girder

                list_results.Add(string.Format(frmt, kStr
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Steel girder
                #endregion Read all

            }

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            #endregion Dead Load Shears



            #region Live Load Effects
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format("Table 3-10 Live Load Effects"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format(""));

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(frmt, "Live Load Effects"
                  , "0.0L"
                  , "0.1L"
                  , "0.2L"
                  , "0.3L"
                  , "0.4L"
                  , "0.5L"
                  , "0.6L"
                  , "0.7L"
                  , "0.8L"
                  , "0.9L"
                  , "1.0L"
                  ));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));

            LL1 = Bridge_Analysis.LL_Analysis;
            for (int j = 1; j < 5; j++)
            {

                #region READ MAXIMUM POSITIVE MOMENTS
                #endregion  READ MAXIMUM POSITIVE MOMENTS

                //Bridge_Analysis.L0_Girders_as_String    

                #region Read all

                loadcase = j;
                string kStr = "";
                lst_frc.Clear();
                if (loadcase == 1)
                {

                    kStr = (string.Format("MAXIMUM POSITIVE MOMENT (K-FT)"));

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L0_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L1_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L2_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L3_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L4_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L5_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L6_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L7_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L8_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Moment(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                }
                else if (loadcase == 2)
                {
                    //list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
                    kStr = (string.Format("MAXIMUM NEGATIVE MOMENT (K-FT)"));

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L0_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L1_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L2_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L3_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L4_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L5_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L6_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L7_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L8_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Moment(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                }
                else if (loadcase == 3)
                {

                    kStr = (string.Format("MAXIMUM POSITIVE SHEAR (Kips)"));

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L0_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L1_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L2_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L3_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L4_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L5_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L6_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L7_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L8_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);


                    mfc = LL1.GetMember_Max_Positive_Shear(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                }
                else if (loadcase == 4)
                {
                    kStr = (string.Format("MAXIMUM NEGATIVE SHEAR (Kips)"));

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L0_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L1_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L2_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L3_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L4_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L5_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L6_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L7_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L8_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                    mfc = LL1.GetMember_Max_Negative_Shear(Bridge_Analysis.L9_Girders_as_String);
                    lst_frc.Add(mfc.Force);

                }

                #region Steel girder

                list_results.Add(string.Format(frmt, kStr
                    , lst_frc[0]
                    , lst_frc[1]
                    , lst_frc[2]
                    , lst_frc[3]
                    , lst_frc[4]
                    , lst_frc[5]
                    , lst_frc[6]
                    , lst_frc[7]
                    , lst_frc[8]
                    , lst_frc[9]
                    , lst_frc[10]

                    ));
                #endregion Steel girder
                #endregion Read all

            }

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------"));
            #endregion Dead Load Shears

            File.WriteAllLines(FILE_SUMMARY_RESULTS, list_results.ToArray());

            rtb_analysis_result.Lines = list_results.ToArray();

            list_results.Clear();
        }

        void Ana_Show_Cross_Girder()
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Structure.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            double z_min = double.MaxValue;
            double x = double.MaxValue;
            int indx = -1;

            int i = 0;
            int j = 0;

            List<double> list_z = new List<double>();

            List<MemberCollection> list_mc = new List<MemberCollection>();

            double last_z = 0.0;
            //double z_min = double.MaxValue;

            while (mc.Count != 0)
            {
                indx = -1;
                for (i = 0; i < mc.Count; i++)
                {
                    if (z_min > mc[i].StartNode.Z)
                    {
                        z_min = mc[i].StartNode.Z;
                        indx = i;
                    }
                }
                if (indx != -1)
                {

                    if (!list_z.Contains(z_min))
                        list_z.Add(z_min);

                    sort_membs.Add(mc[indx]);
                    mc.Members.RemoveAt(indx);
                    z_min = double.MaxValue;
                }
            }



            List<string> list_arr = new List<string>();


            last_z = -1.0;



            //Outer Long Girder
            MemberCollection outer_long = new MemberCollection();
            MemberCollection inner_long = new MemberCollection();
            MemberCollection inner_cross = new MemberCollection();


            //z_min = Truss_Analysis.Analysis.Joints.MinZ;
            //double z_max = Truss_Analysis.Analysis.Joints.MaxZ;

            //Chiranjit [2011 07 09]
            //Store Outer Girder Members
            int count = 0;
            z_min = 0.0;
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (z_min < sort_membs[i].StartNode.Z)
                {
                    z_min = sort_membs[i].StartNode.Z;
                    count++;
                }
                if (z_min < sort_membs[i].EndNode.Z)
                {
                    z_min = sort_membs[i].EndNode.Z;
                    count++;
                }
                //For Outer Girder
                if (count == 2) break;
                //if (count == 0) break;
            }

            //z_min = WidthCantilever;
            double z_max = z_min;


            //Store inner and outer Long Girder
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (((sort_membs[i].StartNode.Z == z_min) || (sort_membs[i].StartNode.Z == z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    outer_long.Add(sort_membs[i]);
                }
                else if (((sort_membs[i].StartNode.Z != z_min) && (sort_membs[i].StartNode.Z != z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    inner_long.Add(sort_membs[i]);
                }
            }

            //Store Cross Girders
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (outer_long.Contains(sort_membs[i]) == false &&
                    inner_long.Contains(sort_membs[i]) == false)
                {
                    inner_cross.Add(sort_membs[i]);
                }
            }




            //Find X MIN    X MAX   for outer long girder
            double x_min, x_max;

            List<double> list_outer_xmin = new List<double>();
            List<double> list_inner_xmin = new List<double>();
            List<double> list_inner_cur_z = new List<double>();
            List<double> list_outer_cur_z = new List<double>();

            List<double> list_outer_xmax = new List<double>();
            List<double> list_inner_xmax = new List<double>();


            x_min = double.MaxValue;
            x_max = double.MinValue;

            last_z = outer_long[0].StartNode.Z;
            for (i = 0; i < outer_long.Count; i++)
            {
                if (last_z == outer_long[i].StartNode.Z)
                {
                    if (x_min > outer_long[i].StartNode.X)
                    {
                        x_min = outer_long[i].StartNode.X;
                    }
                    if (x_max < outer_long[i].EndNode.X)
                    {
                        x_max = outer_long[i].EndNode.X;
                    }
                }
                else
                {
                    list_outer_xmax.Add(x_max);
                    list_outer_xmin.Add(x_min);
                    list_outer_cur_z.Add(last_z);

                    x_min = outer_long[i].StartNode.X;
                    x_max = outer_long[i].EndNode.X;


                }
                last_z = outer_long[i].StartNode.Z;
            }

            list_outer_xmax.Add(x_max);
            list_outer_xmin.Add(x_min);
            list_outer_cur_z.Add(last_z);

            x_min = double.MaxValue;
            x_max = double.MinValue;

            last_z = inner_long.Count > 0 ? inner_long[0].StartNode.Z : 0.0;

            for (i = 0; i < inner_long.Count; i++)
            {
                if (last_z == inner_long[i].StartNode.Z)
                {
                    if (x_min > inner_long[i].StartNode.X)
                    {
                        x_min = inner_long[i].StartNode.X;
                    }
                    if (x_max < inner_long[i].EndNode.X)
                    {
                        x_max = inner_long[i].EndNode.X;
                    }
                }
                else
                {
                    list_inner_xmax.Add(x_max);
                    list_inner_xmin.Add(x_min);
                    list_inner_cur_z.Add(last_z);

                    x_min = inner_long[i].StartNode.X;
                    x_max = inner_long[i].EndNode.X;

                }
                last_z = inner_long[i].StartNode.Z;
            }

            list_inner_xmax.Add(x_max);
            list_inner_xmin.Add(x_min);

            list_inner_cur_z.Add(last_z);

            List<int> _deff_joints = new List<int>();
            List<int> _L_4_joints = new List<int>();
            List<int> _L_2_joints = new List<int>();
            //Member Forces from Report for Inner girder


            //int cur_node = -1;
            int cur_member = -1;
            // FOR L/2
            string curr_membs_L2_text = "";
            // FOR L/4
            string curr_membs_L4_text = "";
            //FOR Effective Depth
            string curr_membs_Deff_text = "";


            double cur_z = 0.0;
            double cur_y = 0.0;

            double curr_L2_x = 0.0;
            double curr_L4_x = 0.0;
            double curr_Deff_x = 0.0;

            curr_membs_L2_text = "";
            curr_membs_L4_text = "";
            curr_membs_Deff_text = "";
            cur_member = -1;

            if (outer_long.Count > 0)
                Bridge_Analysis.Effective_Depth = outer_long[0].Length;

            for (i = 0; i < list_inner_xmax.Count; i++)
            {
                x_max = list_inner_xmax[i];
                x_min = list_inner_xmin[i];

                cur_z = list_inner_cur_z[i];

                curr_L2_x = (x_max + x_min) / 2.0;
                curr_L4_x = (curr_L2_x + x_min) / 2.0;
                curr_Deff_x = (Bridge_Analysis.Effective_Depth + x_min);

                cur_y = 0.0;

                for (j = 0; j < inner_long.Count; j++)
                {
                    if ((inner_long[j].EndNode.Y.ToString("0.0") == cur_y.ToString("0.0")) &&
                        (inner_long[j].EndNode.Z.ToString("0.0") == cur_z.ToString("0.0")))
                    {
                        if ((inner_long[j].EndNode.X.ToString("0.0") == curr_L2_x.ToString("0.0")))
                        {
                            cur_member = inner_long[j].MemberNo;
                            curr_membs_L2_text += cur_member + " ";
                            _L_2_joints.Add(inner_long[j].EndNode.NodeNo);
                        }
                        else if ((inner_long[j].EndNode.X.ToString("0.0") == curr_L4_x.ToString("0.0")))
                        {
                            cur_member = inner_long[j].MemberNo;
                            curr_membs_L4_text += cur_member + " ";
                            _L_4_joints.Add(inner_long[j].EndNode.NodeNo);
                        }
                        else if ((inner_long[j].EndNode.X.ToString("0.0") == curr_Deff_x.ToString("0.0")))
                        {
                            cur_member = inner_long[j].MemberNo;
                            curr_membs_Deff_text += cur_member + " ";
                            _deff_joints.Add(inner_long[j].EndNode.NodeNo);
                        }
                    }
                }
            }


            _L_2_joints.Remove(64);
            _L_4_joints.Remove(42);
            _deff_joints.Remove(20);

            //For Outer Long Girder
            curr_membs_L2_text = "";
            curr_membs_L4_text = "";
            curr_membs_Deff_text = "";
            cur_member = -1;
            _deff_joints.Clear();
            _L_2_joints.Clear();
            _L_4_joints.Clear();
            //Creating X Coordinates at every Z level
            for (i = 0; i < list_outer_xmax.Count; i++)
            {
                x_max = list_outer_xmax[i];
                x_min = list_outer_xmin[i];

                cur_z = list_outer_cur_z[i];

                curr_L2_x = (x_max + x_min) / 2.0;
                curr_L4_x = (curr_L2_x + x_min) / 2.0;
                curr_Deff_x = (Bridge_Analysis.Effective_Depth + x_min);

                cur_y = 0.0;

                for (j = 0; j < outer_long.Count; j++)
                {
                    if ((outer_long[j].EndNode.Y.ToString("0.0") == cur_y.ToString("0.0")) &&
                        (outer_long[j].EndNode.Z.ToString("0.0") == cur_z.ToString("0.0")))
                    {
                        if ((outer_long[j].EndNode.X.ToString("0.0") == curr_L2_x.ToString("0.0")))
                        {
                            cur_member = outer_long[j].MemberNo;
                            curr_membs_L2_text += cur_member + " ";
                            _L_2_joints.Add(outer_long[j].EndNode.NodeNo);

                        }
                        else if ((outer_long[j].EndNode.X.ToString("0.0") == curr_L4_x.ToString("0.0")))
                        {
                            cur_member = outer_long[j].MemberNo;
                            curr_membs_L4_text += cur_member + " ";
                            _L_4_joints.Add(outer_long[j].EndNode.NodeNo);
                        }
                        else if ((outer_long[j].EndNode.X.ToString("0.0") == curr_Deff_x.ToString("0.0")))
                        {
                            cur_member = outer_long[j].MemberNo;
                            curr_membs_Deff_text += cur_member + " ";
                            _deff_joints.Add(outer_long[j].EndNode.NodeNo);
                        }
                    }
                }
            }



            //Cross Girder
            string cross_text = "";
            if (inner_cross.Count == 0)
                MessageBox.Show(this, "No Cross Girder was found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {

                for (j = 0; j < inner_cross.Count; j++)
                {

                    cur_member = inner_cross[j].MemberNo;
                    cross_text += cur_member + " ";
                }

                CMember m = new CMember();
                m.Group.MemberNosText = cross_text;
                m.Force = Bridge_Analysis.Structure.GetForce(ref m);

            }
            //Ana_Write_Max_Moment_Shear();
            //Show_Cross_Girder_Forces();
            //Show_Long_Girder_Forces();
        }
        void Write_Max_Moment_Shear()
        {
            //List<string> list = new List<string>();
            //list.Add(string.Format("LONG_INN_DEFF_SHR={0}", txt_Ana_inner_long_deff_shear.Text));
            //list.Add(string.Format("LONG_INN_L2_MOM={0}", txt_Ana_inner_long_L2_moment.Text));


            //list.Add(string.Format("LONG_OUT_DEFF_SHR={0}", txt_Ana_outer_long_deff_shear.Text));
            //list.Add(string.Format("LONG_OUT_L2_MOM={0}", txt_Ana_outer_long_L2_moment.Text));

            //string f_path = Path.Combine(user_path, "FORCES.TXT");
            //File.WriteAllLines(f_path, list.ToArray());
            //Environment.SetEnvironmentVariable("TBEAM_ANALYSIS", f_path);
            //list = null;
        }
        public void Button_Enable_Disable()
        {
            btn_view_data.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_view_preprocess.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_View_Moving_Load.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_view_report.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_Ana_process_analysis.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);

            Analysis_Button_Enabled();
        }
        public void Ana_OpenAnalysisFile(string file_name)
        {
            string analysis_file = Path.GetDirectoryName(file_name);


            if (Path.GetFileName(analysis_file).ToLower() == "dead load analysis")
            {
                analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
                goto _run;
            }
            else if (Path.GetFileName(analysis_file).ToLower() == "live load analysis")
            {
                analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
                goto _run;
            }
            else if (Path.GetFileName(analysis_file).ToLower() == "total analysis")
            {
                analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
                goto _run;
            }
            else
                analysis_file = file_name;



            user_path = Path.GetDirectoryName(analysis_file);


            string usp = Path.Combine(user_path, "ANALYSIS PROCESS");

            if (!Directory.Exists(usp))
                Directory.CreateDirectory(usp);

            Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");




            if (File.Exists(analysis_file))
            {
                btn_view_preprocess.Enabled = true;

                //Bridge_Analysis.Input_File = (analysis_file);
                string rep_file = Bridge_Analysis.Total_Analysis_Report;

                //Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, analysis_file);

                //if (Bridge_Analysis != null)
                //{
                //    if (Bridge_Analysis.Structure.Analysis.Joints.Count > 1)
                //    {
                //        Bridge_Analysis.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Bridge_Analysis.Structure.Analysis.Joints[1].X / Bridge_Analysis.Structure.Analysis.Joints[1].Z)));
                //        Ang = Bridge_Analysis.Skew_Angle;
                //    }
                //}
                //txt_Ana_L.Text = Bridge_Analysis.Structure.Analysis.Length.ToString();
                //txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                //txt_Ana_B.Text = Bridge_Analysis.Structure.Analysis.Width.ToString();
                //txt_Ana_analysis_file.Visible = true;
                //txt_Ana_analysis_file.Text = analysis_file;


                //txt_Ana_CL.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();
                //txt_Ana_CR.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();


            }

            #region Chiranjit [2012 06 10]
        _run:

            //Bridge_Analysis.Input_File = analysis_file;
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;

            //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_select_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_select_analysis_file.Checked;

            Calculate_Interactive_Values();

            Button_Enable_Disable();
            #endregion


            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Bridge_Analysis.LiveLoad_File);

            if (Bridge_Analysis.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();
        }
        public void Open_AnalysisFile(string file_name)
        {
            string analysis_file = file_name;


            //if (Path.GetFileName(analysis_file).ToLower() == "dead load analysis")
            //{
            //    analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
            //    goto _run;
            //}
            //else if (Path.GetFileName(analysis_file).ToLower() == "live load analysis")
            //{
            //    analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
            //    goto _run;
            //}
            //else if (Path.GetFileName(analysis_file).ToLower() == "total analysis")
            //{
            //    analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
            //    goto _run;
            //}
            //else
            //    analysis_file = file_name;






            user_path = Path.GetDirectoryName(file_name);


            if (File.Exists(analysis_file))
            {
                btn_view_preprocess.Enabled = true;

                Bridge_Analysis.Input_File = (file_name);
                string rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
                if (File.Exists(rep_file))
                {
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, rep_file);
                    Show_Moment_Shear();
                }
                else
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, analysis_file);

                try
                {

                    if (Bridge_Analysis != null)
                    {
                        if (Bridge_Analysis.Structure.Analysis.Joints.Count > 1)
                        {
                            Bridge_Analysis.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Bridge_Analysis.Structure.Analysis.Joints[1].X / Bridge_Analysis.Structure.Analysis.Joints[1].Z)));
                            Skew = Bridge_Analysis.Skew_Angle;
                        }
                    }

                    txt_Ana_L.Text = Bridge_Analysis.Structure.Analysis.Length.ToString();
                    //txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                    txt_Ana_wdeck.Text = Bridge_Analysis.Structure.Analysis.Width.ToString();

                    //txt_Ana_Deff.Text = Bridge_Analysis.Truss_Analysis.Analysis.Effective_Depth.ToString();
                    txt_Ana_Soverhang.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();
                    txt_Ana_Soverhang.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();


                    Skew = Bridge_Analysis.Structure.Analysis.Skew_Angle;


                    //txt_gd_np.Text = (Bridge_Analysis.Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                }
                catch (Exception ex) { }
                MessageBox.Show(this, "File opened successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            string ll_txt = Path.Combine(user_path, "LL.txt");

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Bridge_Analysis.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();
        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("UNIT KN ME");

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = null;


            if (true)
            {
                //if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT")) load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //if (!load_lst.Contains("TYPE 1 CLA 1.179")) load_lst.Add("TYPE 1 CLA 1.179");
                //if (!load_lst.Contains("TYPE 2 CLB 1.188")) load_lst.Add("TYPE 2 CLB 1.188");
                //if (!load_lst.Contains("TYPE 3 A70RT 1.10")) load_lst.Add("TYPE 3 A70RT 1.10");
                //if (!load_lst.Contains("TYPE 4 CLAR 1.179")) load_lst.Add("TYPE 4 CLAR 1.179");
                //if (!load_lst.Contains("TYPE 5 A70RR 1.188")) load_lst.Add("TYPE 5 A70RR 1.188");
                //if (!load_lst.Contains("TYPE 6 IRC24RTRACK 1.188")) load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //if (!load_lst.Contains("TYPE 7 RAILBG 1.25")) load_lst.Add("TYPE 7 RAILBG 1.25");
                //if (!load_lst.Contains("")) load_lst.Add("");
                //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");

                //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                foreach (LoadData ld in Bridge_Analysis.LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }
                load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);

                foreach (LoadData ld in Bridge_Analysis.LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }



            }
            if (calc_width > Bridge_Analysis.WidthBridge)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Bridge_Analysis.WidthBridge;

                str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }

        #region Composite Methods
        #endregion Composite Methods

        private void frm_Composite_Load(object sender, EventArgs e)
        {

            cmb_long_open_file_process.Items.Clear();
            cmb_long_open_file_process.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
            cmb_long_open_file_process.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
            cmb_long_open_file_process.Items.Add(string.Format("LIVE LOAD ANALYSIS"));

            #region Initialise default input data
            AASHTO_Design.Input_Positive_Moment_Region_Section_Properties(dgv_positive_moment_properties);
            AASHTO_Design.Input_Negative_Moment_Region_Section_Properties(dgv_negative_moment_properties);


            AASHTO_Design.Input_Deck_Data(dgv_deck_input_data);
            AASHTO_Design.Input_Steel_Girder_Data(dgv_steel_girder_input_data);
            AASHTO_Design.Input_Bolted_Splice_Data(dgv_bolted_field_splice_input_data);
            AASHTO_Design.Input_Misc_Steel_Data(dgv_misc_steel_input_data);
            AASHTO_Design.Input_Abutment_Data(dgv_abutment_input_data);
            AASHTO_Design.Input_Pier_Data(dgv_pier_input_data);
            AASHTO_Design.Input_Foundation_Data(dgv_foundation_input_data);
            AASHTO_Design.Input_Bearing_Data(dgv_bearing_input_data);


            #endregion Initialise default input data


            Default_Moving_LoadData(dgv_long_liveloads);

            Default_Moving_Type_LoadData(dgv_long_loads);


            rbtn_sec_box.Checked = true;
            rbtn_sec_plate.Checked = true;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
            }
            else if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
            }
            else
            {
                iApp.Tables.USCS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, false);
            }

            if (cmb_ana_ang_section_name.Items.Count > 0)
                cmb_ana_ang_section_name.SelectedIndex = 0;

            //tbc_girder.TabPages.Remove(tab_orthotropic);

            cmb_Ana_NMG.SelectedIndex = 2;
            txt_curve_des_spd_kph.Text = "50";

            //rbtn_multiSpan.Checked = true;
            //chk_curve.Checked = true;

            rbtn_singleSpan.Checked = true;
            chk_curve.Checked = false;

            Text_Changed();
            Button_Enable_Disable();

            Show_Steel_SectionProperties();
            tabControl5.TabPages.Remove(tabPage4);
            tabControl5.TabPages.Remove(tabPage10);
            tabControl5.TabPages.Remove(tabPage6);
            tabControl5.TabPages.Remove(tabPage1);

            //uC_Orthotropic1.SetApplication(iApp);
            Set_Project_Name();


            chk_crash_barrier.Checked = true;

            chk_cb_right.Checked = false;
            chk_cb_left.Checked = true;


            chk_footpath.Checked = false;

            Select_Moving_Load_Combo(dgv_long_loads, cmb_irc_view_moving_load);

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

        private void btn_dwg_copm_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (!Check_Project_Folder()) return;

            if (btn.Name == btn_dwg_construction.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Construction Drawings"), "COST_Composite_Bridges");
                return;
            }
            if (btn.Name == btn_dwg_worksheet.Name)
            {
                if (iApp.DesignStandard != eDesignStandard.IndianStandard)
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings for Composite Bridge Worksheet Design LSM_BS"), "COMPOSITE_LSM_BS");
                else
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings for Composite Bridge Worksheet Design LSM_IRC"), "COMPOSITE_LSM_IRC");
                return;
            }
            eOpenDrawingOption opt = iApp.Open_Drawing_Option();

            if (opt == eOpenDrawingOption.Cancel) return;



            string draw = Drawing_Folder;


            string copy_path = Path.Combine(Worksheet_Folder, "");

            if (opt == eOpenDrawingOption.Design_Drawings)
            {
                #region Design Drawings


                //if (btn.Name == btn_dwg_deck_slab.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Deck.drawing_path, "Composite_Bridge", "");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Composite Deck Slab Drawing"), "COMPOSITE_DECK_LS");
                //}
                //else if (btn.Name == btn_dwg_steel_plate.Name)
                if (btn.Name == btn_dwg_steel_plate.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Steel Plate Girder Drawing"), "Composite_Bridge_Steel_Plate");
                    string rep_file = "";
                    iApp.Form_Drawing_Editor(eBaseDrawings.COMPOSITE_LS_STEEL_PLATE, Drawing_Folder, rep_file).ShowDialog();

                }
                else if (btn.Name == btn_dwg_steel_box.Name)
                {
                    string rep_file = "";
                    //rep_file = (Deck.rep_file_name_outer);
                    iApp.Form_Drawing_Editor(eBaseDrawings.COMPOSITE_LS_STEEL_BOX, Drawing_Folder, rep_file).ShowDialog();
                }
                else if (btn.Name == btn_dwg_construction.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Construction Drawings"), "COST_Composite_Bridges");
                }
                else if (btn.Name == btn_dwg_open_Deckslab.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_PIER, Title, draw, copy_path).ShowDialog();
                }
                #endregion Design Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {
                #region Design Drawings


                //if (btn.Name == btn_dwg_deck_slab.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Deck.drawing_path, "Composite_Bridge", "");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Composite Deck Slab Drawing"), "COMPOSITE_DECK_LS");
                //}
                if (btn.Name == btn_dwg_steel_plate.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Steel Plate Girder Drawing"), "Composite_Bridge_Steel_Plate");
                }
                else if (btn.Name == btn_dwg_steel_box.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Steel Box Girder Drawing"), "Composite_Bridge_Steel_Box");
                }
                else if (btn.Name == btn_dwg_open_Deckslab.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Composite Deck Slab Drawing"), "COMPOSITE_DECK_LS");
                    //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Counterfort Abutment Drawings"), "BOX_ABUTMENT");
                }
                else if (btn.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Canlilever Abutment Drawings"), "TBeam_Abutment");
                }
                else if (btn.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "TBeam_Pier");
                }
                #endregion Design Drawings
            }

        }

        private void btn_worksheet_comp_Click(object sender, EventArgs e)
        {

            string source_path = Path.Combine(Application.StartupPath, @"DESIGN\Composite\Composite Worksheet Design 1\Composit_Worksheet_Design.xls");

            if (File.Exists(source_path))
            {
                try
                {
                    iApp.OpenExcelFile(Worksheet_Folder, source_path, "2011ap");
                }
                catch (Exception ex) { }
            }
            else
            {
                MessageBox.Show(source_path, " file not found.", MessageBoxButtons.OK);
            }
        }

        private void btn_Ana_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void cmb_ang_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            TableRolledSteelAngles tbl_rolledSteelAngles = null;

            if (cmb_ana_ang_section_name.Text.Contains("IS")) tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;
            else if (cmb_ana_ang_section_name.Text.Contains("UK")) tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;
            else if (cmb_ana_ang_section_name.Text.Contains("L")) tbl_rolledSteelAngles = iApp.Tables.USCS_SteelAngles;


            if (cmb.Name == cmb_ana_ang_section_code.Name)
            {
                //Deck.tbl_rolledSteelAngles = cmb_Deff_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;
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
                else if (cmb_ana_ang_section_name.Text.Contains("L"))
                {
                    foreach (var item in iApp.Tables.USCS_SteelAngles.List_Table)
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
                    //cmb_ana_ang_section_code.SelectedItem = "100X100";
                    if (cmb_ana_ang_thk.Items.Count > 0)
                    {
                        cmb_ana_ang_thk.SelectedIndex = cmb_ana_ang_thk.Items.Contains(10.0) ? cmb_ana_ang_thk.Items.IndexOf(10.0) : 0;
                    }
                    cmb_ana_nos_ang.SelectedIndex = 0;
                }
            }

        }


        #region Chiranjit [2012 02 08]
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            rft.M2 = true;
            rft.M3 = true;
            rft.R3 = true;
            rft.R2 = true;
            return rft;
        }

        private void btn_update_force_Click(object sender, EventArgs e)
        {
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (File.Exists(ana_rep_file))
            {
                //Bridge_Analysis.Truss_Analysis = null;
                //Bridge_Analysis.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Bridge_Analysis.Structure.ForceType = GetForceType();
                Show_Moment_Shear();

                //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


                Button_Enable_Disable();
                //MessageBox.Show("Bending Moments & Shear Forces are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Analysis Result not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion




        //Chiranjit [2012 06 10]
        void Calculate_Interactive_Values()
        {

            //double eff_dp = MyList.StringToDouble(txt_Ana_eff_depth.Text, 0.0);
            //double cover = MyList.StringToDouble(txt_Long_cover.Text, 0.0);
            //double bdia = MyList.StringToDouble(txt_Long_bar_dia_Deff.Text, 0.0);
            //double lg_spa = MyList.StringToDouble(txt_Long_Gs.Text, 0.0);
            //double cg_spa = MyList.StringToDouble(txt_Long_space_cross_girder.Text, 0.0);
            //double len = MyList.StringToDouble(txt_Ana_length.Text, 0.0);
            //double wd = MyList.StringToDouble(txt_Ana_width.Text, 0.0);
            //double cant_wd = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);
            //double long_bw = MyList.StringToDouble(txt_Long_bw.Text, 0.0);



            ////Long Girder
            ////Effective Depth+50+[4x28+(4-1)x28]/2
            //txt_Long_D.Text = "" + (eff_dp * 1000 + cover + ((4 * bdia + 3 * bdia) / 2.0)).ToString("f3");
            //txt_Long_span_girders.Text = len.ToString("f3");

            ////Cross Girders

            //int lng_no = (int)((wd - 2 * cant_wd) / lg_spa);
            //int cg_no = (int)((len) / lg_spa);
            //lng_no++;
            //cg_no++;

            //txt_Cross_spacing_longitudinal_girder.Text = lg_spa.ToString("f3");
            //txt_Cross_number_longitudinal_girder.Text = lng_no.ToString();

            //txt_Cross_spacing_cross_girders.Text = cg_spa.ToString("f3");
            //txt_Cross_number_cross_girder.Text = cg_no.ToString();

            //txt_Cross_grade_concrete.Text = cmb_Long_concrete_grade.Text;
            //txt_Cross_grade_steel.Text = cmb_Long_Steel_Grade.Text;



            //// Deck Slab

            //txt_Deck_WidthCarrageWay.Text = (wd - 0.5).ToString("f3");
            //txt_Deck_effective_span_Tee_beam.Text = (len).ToString("f3");

            //txt_Deck_L.Text = cg_spa.ToString("f3");
            //txt_Deck_B.Text = lg_spa.ToString("f3");
            //txt_Deck_no_of_main_girders.Text = lng_no.ToString("f0");

            //txt_Deck_Ds.Text = txt_Long_Db.Text;

            //txt_Deck_concrete_grade.Text = cmb_Long_concrete_grade.Text;
            //txt_Deck_steel_grade.Text = cmb_Long_Steel_Grade.Text;

            //txt_Deck_width_of_cross_girders.Text = (MyList.StringToDouble(txt_Cross_web_thickness_cross_girder.Text, 0.0) * 1000).ToString();
            //txt_Deck_width_of_long_girders.Text = txt_Long_bw.Text;

            ////Cantilever Slab
            //txt_Cant_a1.Text = (long_bw / 1000.0).ToString("f3");
            //txt_Cant_a3.Text = (cant_wd - (long_bw / (2 * 1000.0))).ToString("f3");

            //txt_Cant_w1.Text = txt_Deck_imposed_load.Text;
            //txt_Cant_a4.Text = txt_Deck_width.Text;
            //txt_Cant_concrete_grade.Text = cmb_Long_concrete_grade.Text;
            //txt_Cant_steel_grade.Text = cmb_Long_Steel_Grade.Text;
            //txt_Cant_gamma_c.Text = txt_Deck_gamma_c.Text;

            ////Abutment
            //txt_cnt_d1.Text = ((MyList.StringToDouble(txt_Long_D.Text, 0.0) / 1000) + 0.2).ToString("f3");
            //txt_cnt_B.Text = wd.ToString("f3");
            //txt_cnt_concrete_grade.Text = cmb_Long_concrete_grade.Text;
            //txt_cnt_steel_grade.Text = cmb_Long_Steel_Grade.Text;
            //txt_cnt_gamma_c.Text = txt_Deck_gamma_c.Text;
            //txt_cnt_L.Text = len.ToString("f3");



            ////RCC Pier Form 1
            //txt_RCC_Pier_L1.Text = len.ToString("f3");
            //txt_RCC_Pier_w1.Text = txt_Deck_WidthCarrageWay.Text;
            //txt_RCC_Pier_w2.Text = wd.ToString("f3");
            //txt_RCC_Pier_NB.Text = lng_no.ToString("f0");
            //txt_RCC_Pier_NP.Text = lng_no.ToString("f0");
            //txt_RCC_Pier_B14.Text = (wd + 2.0).ToString("f3");


            //double B9 = MyList.StringToDouble(txt_RCC_Pier_B9.Text, 0.0);
            //double B10 = MyList.StringToDouble(txt_RCC_Pier_B10.Text, 0.0);
            //double B11 = MyList.StringToDouble(txt_RCC_Pier_B11.Text, 0.0);
            //double B12 = MyList.StringToDouble(txt_RCC_Pier_B12.Text, 0.0);
            //double B14 = MyList.StringToDouble(txt_RCC_Pier_B14.Text, 0.0);

            //txt_RCC_Pier_D.Text = ((B10 + B12) / 2.0).ToString("f3");
            //txt_RCC_Pier_b.Text = ((B9 + B11) / 2.0).ToString("f3");



            //txt_pier_2_SBC.Text = txt_cnt_p_bearing_capacity.Text;
            //txt_pier_2_SC.Text = txt_cnt_sc.Text;
            //txt_pier_2_B16.Text = ((B14 - B12) / 2.0).ToString("f3");
        }
        private void txt_Ana_width_TextChanged(object sender, EventArgs e)
        {
            Calculate_Interactive_Values();
        }

        #region Chiranjit [2012 06 20]
        //Chiranjit [2012 06 20]
        //Define Properties
        public double L { get { return MyList.StringToDouble(txt_Ana_L.Text, 13.0); } set { txt_Ana_L.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_wdeck.Text, 0.0); } set { txt_Ana_wdeck.Text = value.ToString("f3"); } }
        public double CW { get { return MyList.StringToDouble(txt_Ana_wroadway.Text, 0.0); } set { txt_Ana_wroadway.Text = value.ToString("f3"); } }
        public double CL { get { return MyList.StringToDouble(txt_Ana_Soverhang.Text, 0.0); } set { txt_Ana_Soverhang.Text = value.ToString("f3"); } }
        public double CR { get { return MyList.StringToDouble(txt_Ana_Soverhang.Text, 0.0); } set { txt_Ana_Soverhang.Text = value.ToString("f3"); } }
        public double Ds { get { return MyList.StringToDouble(txt_Ana_tdeck.Text, 0.0); } set { txt_Ana_tdeck.Text = value.ToString("f3"); } }
        public double Y_c { get { return MyList.StringToDouble(txt_Ana_Wc.Text, 0.0); } set { txt_Ana_Wc.Text = value.ToString("f3"); } }
        public double Skew { get { return MyList.StringToDouble(txt_Ana_Skew.Text, 0.0); } set { txt_Ana_Skew.Text = value.ToString("f3"); } }
        public double NMG { get { return MyList.StringToDouble(cmb_Ana_NMG.Text, 0.0); } set { cmb_Ana_NMG.Text = value.ToString("f3"); } }
        public double DMG { get { return MyList.StringToDouble(txt_sec_L2_Dw.Text, 0.0) / 1000.0; } set { txt_sec_L2_Dw.Text = (value * 1000.0).ToString(); } }
        public double Deff { get { return (DMG - 0.500 - (4 * 0.028 + 3 * 0.028) / 2.0); } }
        public double BMG { get { return MyList.StringToDouble(txt_sec_L2_Bw.Text, 0.0) / 1000.0; } set { txt_sec_L2_Bw.Text = (value * 1000.0).ToString(); } }
        public double NCG { get { return 11.0; } }
        //public double NCG { get { return MyList.StringToDouble(txt_Ana_NCG.Text, 0.0); } set { txt_Ana_NCG.Text = value.ToString("f3"); } }
        public double DCG { get { return MyList.StringToDouble(txt_sec_cross_Dw.Text, 0.0) / 1000.0; } set { txt_sec_cross_Dw.Text = (value * 1000.0).ToString(); } }
        public double BCG { get { return MyList.StringToDouble(txt_sec_cross_Bw.Text, 0.0) / 1000.0; } set { txt_sec_cross_Bw.Text = (value * 1000.0).ToString(); } }
        public double Dw { get { return MyList.StringToDouble(txt_Ana_tfws.Text, 0.0); } set { txt_Ana_tfws.Text = value.ToString("f3"); } }
        public double Y_w { get { return MyList.StringToDouble(txt_Ana_Wfws.Text, 0.0); } set { txt_Ana_Wfws.Text = value.ToString("f3"); } }
        public double Hc { get { return MyList.StringToDouble(txt_Ana_Hpar.Text, 0.0); } set { txt_Ana_Hpar.Text = value.ToString("f3"); } }
        public double Wc { get { return MyList.StringToDouble(txt_Ana_wbase.Text, 0.0); } set { txt_Ana_wbase.Text = value.ToString("f3"); } }
        public double Wf { get { return MyList.StringToDouble(txt_Ana_wf_LHS.Text, 0.0); } set { txt_Ana_wf_LHS.Text = value.ToString("f3"); } }
        public double Hf { get { return MyList.StringToDouble(txt_Ana_hf_LHS.Text, 0.0); } set { txt_Ana_hf_LHS.Text = value.ToString("f3"); } }
        public double Wk { get { return MyList.StringToDouble(txt_Ana_Wk.Text, 0.0); } set { txt_Ana_Wk.Text = value.ToString("f3"); } }
        public double wr { get { return MyList.StringToDouble(txt_Ana_wr.Text, 0.0); } set { txt_Ana_wr.Text = value.ToString("f3"); } }
        public double swf { get { return 1.0; } }
        public double FMG { get { return MyList.StringToDouble(txt_sec_L2_Bft.Text, 0.0) / 1000.0; } set { txt_sec_L2_Bft.Text = (value * 1000.0).ToString(); } }
        public double TMG { get { return MyList.StringToDouble(txt_sec_L2_Dft.Text, 0.0) / 1000.0; } set { txt_sec_L2_Dft.Text = (value * 1000.0).ToString(); } }
        public double FCG { get { return MyList.StringToDouble(txt_sec_cross_Bft.Text, 0.0) / 1000.0; } set { txt_sec_cross_Dft.Text = (value * 1000.0).ToString(); } }
        public double TCG { get { return MyList.StringToDouble(txt_sec_cross_Dft.Text, 0.0) / 1000.0; } set { txt_sec_cross_Dft.Text = (value * 1000.0).ToString(); } }
        public double Y_S { get { return MyList.StringToDouble(txt_Ana_Ws.Text, 0.0); } set { txt_Ana_Ws.Text = value.ToString("f3"); } }


        public double Curve_Radius { get { return MyList.StringToDouble(txt_curve_radius.Text, 0.0); } set { txt_curve_radius.Text = value.ToString("f3"); } }


        #endregion Chiranjit [2012 06 20]
        #region Chiranjit [2012 06 20]


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
            train_length = veh_len;
            double eff = L;
            bool fl = false;
            while (train_length <= eff)
            {
                fl = !fl;
                if (fl)
                {
                    train_length += veh_gap;
                    if (train_length > L)
                    {
                        train_length = train_length - veh_gap;
                    }
                }
                else
                {
                    train_length += veh_len;
                }
            }
            //return mvl;
            return train_length;
        }
        void Text_Changed()
        {
            Set_Composite_Inputs();

            #region Chiranjit [2013 06 25]

            double flange_wi = Comp_sections.Section_Long_Girder_at_Mid_Span.Bfb;


            if (flange_wi < Comp_sections.Section_Long_Girder_at_Mid_Span.Bft)
                flange_wi = Comp_sections.Section_Long_Girder_at_Mid_Span.Bft;

            if (flange_wi < Comp_sections.Section_Long_Girder_at_L4_Span.Bft)
                flange_wi = Comp_sections.Section_Long_Girder_at_L4_Span.Bft;

            if (flange_wi < Comp_sections.Section_Long_Girder_at_End_Span.Bft)
                flange_wi = Comp_sections.Section_Long_Girder_at_End_Span.Bft;
            flange_wi = flange_wi / 1000.0;

            #endregion Chiranjit [2013 06 25]

            //double SMG = (B - CL - CR) / (NMG - 1);
            double SMG = (B - CL - CR - flange_wi) / (NMG - 1); //Chiranjit [2013 06 25]

            flange_wi = Comp_sections.Section_Cross_Girder.Bfb;


            if (flange_wi < Comp_sections.Section_Cross_Girder.Bft)
                flange_wi = Comp_sections.Section_Cross_Girder.Bft;
            flange_wi = flange_wi / 1000.0;


            double SCG = (L - flange_wi) / (NCG - 1);


            Comp_sections.Spacing_Long_Girder = SMG;
            Comp_sections.Spacing_Cross_Girder = SCG;


            //txt__SMG.Text = SMG.ToString("f3");
            //txt_Deck_SCG.Text = SCG.ToString("f3");

            double x_incr = MyList.StringToDouble(txt_XINCR.Text, 0.2);
            //double x_dim = Math.Abs(MyList.StringToDouble(txt_Ana_X.Text, 0.2));
            double x_dim = 0.0;

            txt_LL_load_gen.Text = ((L + x_dim + Get_Max_Vehicle_Length()) / x_incr).ToString("f0");

             

            #region Chiranjit [2017 09 18]


            #endregion Chiranjit [2017 09 18]
        }


        private void TextBox_TextChanged(object sender, EventArgs e)
        {


            #region // Calculate Curve Span

            double R = MyList.StringToDouble(txt_curve_radius.Text, 0.0);



            if (R != 0)
            {
                double theta = L * 180 / (R * Math.PI);

                txt_curve_angle.Text = theta.ToString("f2");

                double dvs = MyList.StringToDouble(txt_curve_divs.Text, 0.0);

                txt_curve_ang_incr.Text = (theta / dvs).ToString("f2");


                double V = MyList.StringToDouble(txt_curve_des_spd_kph.Text, 0.0);

                //txt_curve_des_spd_mps.Text = (V * 1000 / (60.0 * 60.0)).ToString("f3");
                txt_curve_des_spd_mps.Text = (V * 0.9113).ToString("f3");

            }
            else
            {
                txt_curve_angle.Text = "0.0";
                txt_curve_ang_incr.Text = "0.0";
                txt_curve_des_spd_mps.Text = "0.0";
            }
            #endregion

            Text_Changed();


            Show_Steel_SectionProperties();
            DataChange();
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

            #region SS
            /*
            Control rbtn = sender as Control;

            if (rbtn.Name == chk_fp_left.Name)
            {
                if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                    chk_fp_right.Checked = true;
            }
            else if (rbtn.Name == chk_fp_right.Name)
            {
                if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                    chk_fp_left.Checked = true;
            }

            if (rbtn.Name == chk_WC.Name)
            {
                grb_ana_wc.Enabled = chk_WC.Checked;
                if (grb_ana_wc.Enabled == false)
                {
                    txt_Ana_tfws.Text = "0.000";
                    txt_Ana_Wfws.Text = "0.000";
                }
                else
                {
                    //txt_Ana_Dw.Text = "0.080";
                    //txt_Ana_gamma_w.Text = "22.000";
                }
            }
            else if (rbtn.Name == rbtn_crash_barrier.Name)
            {
                grb_ana_parapet.Enabled = rbtn_crash_barrier.Checked;
                if (!rbtn_crash_barrier.Checked)
                {
                    txt_Ana_Hpar.Text = "0.000";
                    txt_Ana_wbase.Text = "0.000";
                }
                else
                {
                    //txt_Ana_Hc.Text = "1.200";
                    //txt_Ana_Wc.Text = "0.500";
                }
            }
            else if (rbtn.Name == rbtn_footpath.Name)
            {
                grb_ana_sw_fp.Enabled = rbtn_footpath.Checked;
                if (!rbtn_footpath.Checked)
                {
                    txt_Ana_wf.Text = "0.000";
                    txt_Ana_hf.Text = "0.000";
                    txt_Ana_Wk.Text = "0.000";
                    txt_Ana_wr.Text = "0.000";
                }
                else
                {
                    //txt_Ana_wf.Text = "1.000";
                    //txt_Ana_hf.Text = "0.250";
                    //txt_Ana_Wk.Text = "0.500";
                    //txt_Ana_wr.Text = "0.100";
                }
            }

            if (rbtn_crash_barrier.Checked)
            {
                pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.comp_dia_1;

            }
            else if (rbtn_footpath.Checked)
            {
                if (chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.comp_dia_2;
                else if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.comp_dia_3;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.comp_dia_4;
            }

            Text_Changed();
            */
            #endregion SS


        }
        public double SMG
        {
            get
            {
                return (B - CL - CR) / (NMG - 1);
            }
        }
        public double SCG
        {
            get
            {
                return (L / (NCG - 1));
            }
        }
        public double m  //Chiranjit [2013 07 02]
        {
            get
            {
                return (MyList.StringToDouble(txt_Ana_n.Text, 10));
            }
        }

        public void Calculate_Load_Computation()
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();




            double wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for Composite Bridge"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            //SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab"));
            wi1 = SMG * SCG * (Ds * Y_c + Dw * Y_w);
            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} kN.", wi1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));

            //wi2 = SCG * BMG * DMG * Y_S + 2 * FMG * TMG * Y_S;
            //list.Add(string.Format("wi2 = (SCG * BMG * DMG * Y_s) + (2*FMG*TMG*Y_S) = ({0:f3}*{1:f3}*{2:f3}*{3:f3}) + (2*{4:f3}*{5:f3}*{3:f3}) = {6:f3} kN.",
            //    SCG, BMG, DMG, Y_S, FMG, TMG, wi2));



            //Chiranjit [2012 12 14]
            double wi2_1 = 2 * SCG * Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_FT * Y_S;


            double wi2_2 = 2 * SCG * Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_FT * Y_S;
            double wi2_3 = 2 * SCG * Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_FT * Y_S;


            wi2 = wi2_1 + wi2_2 + wi2_3;

            list.Add(string.Format("wi2 = 2 * (SCG * AREA End Section) * Y_S +  2 * (SCG * AREA L4 Section) * Y_S +  2 * (SCG * AREA Mid Section) * Y_S"));

            list.Add(string.Format("    = 2 * ({0:f3} * {1:f3}) * {2:f3} +  2 * ({0:f3} * {3:f3}) * {2:f3}  +  2 * ({0:f3} * {4:f3}) * {2:f3}",
               SCG,
               Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_FT,
               Y_S,
               Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_FT,
               Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_FT));

            list.Add(string.Format(""));
            list.Add(string.Format("    = {0:f3} kN", wi2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders"));
            wi3 = wi1 + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kip.",
                wi1, wi2, wi3));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL"));
            wi4 = wi3 / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kip/ft.",
                wi3, SCG, wi4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders"));
            NIG = NMG * (NCG - 1);
            list.Add(string.Format("NIG = NMG*(NCG - 1) = {0:f0} * ({1:f0}-1) = {2:f0} nos.",
                NMG, NCG, NIG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            NIM = 70;
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            list.Add(string.Format("NIM = 70 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wiu = wi4 * NIG / NIM;
            list.Add(string.Format("wiu = wi4*NIG/NIM = {0:f3} * {1}/70 = {2:f3}  = {3:f3} kip/ft.",
                wi4, NIG, wiu, (wiu = wiu / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));

            list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} kip/ft.",
                wiu, swf, (wiu = wiu * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            member_load.Add(string.Format("MEMBER LOAD "));
            member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));

            //txt_ana_inner_long_girder_mem_load.Text = wiu.ToString("f4"); //Chiranjit [2013 06 07]

            list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("                131 TO 200 UNI GY -{0:f4}", wiu));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Outer Girder members"));
            list.Add(string.Format(""));
            //list.Add(string.Format("if(CL > CR) then (C=CL) else (C=CR)"));
            if (CL > CR) C = CL; else C = CR;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab and Wearing Course"));
            wo1 = ((SMG / 2) + C) * SCG * (Ds * Y_c + Dw * Y_w);
            list.Add(string.Format("wo1 = [(SMG/2) + C]*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} kN.", wo1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));

            //Chiranjit [2012 12 14]
            //wo2 = (SCG * BMG * DMG * Y_S) + (2 * FMG * TMG * Y_S);
            wo2 = wi2_1 + wi2_2 + wi2_3;
            list.Add(string.Format("wo2 = wi2_1 + wi2_2 + wi2_3"));
            list.Add(string.Format("    = {0:f4} + {1:f4} + {2:f4}", wi2_1, wi2_2, wi2_3));
            list.Add(string.Format("    = {0:f4}", wo2));
            //wo2 = wi2;
            //list.Add(string.Format("wo2 = (SCG*BMG*DMG*Y_c) + (2*FMG*TMG*Y_S) = ({0:f3}*{1:f3}*{2:f3}*{3:f3}) + (2*{4:f3}*{5:f3}*{3:f3}) = {6:f3} kN.",
            //    SCG, BMG, DMG, Y_c, FMG, TMG, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hc * Wc * Y_c;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} KIP.",
               SCG, Hc, Wc, Y_c, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Wf * Hf * Y_c;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wf, Hf, Y_c, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * wr * Wk * Y_c;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, wr, Wk, Y_c, wo5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders ")); wo6 = wo1 + wo2 + wo3 + wo4 + wo5;
            list.Add(string.Format("wo6 = wo1 + wo2 + wo3 + wo4 + wo5 "));
            list.Add(string.Format("   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} ",
                wo1, wo2, wo3, wo4, wo5));
            list.Add(string.Format("   = {0:f3} kN.", wo6));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL")); wo7 = wo6 / SCG;
            list.Add(string.Format("wo7 = wo6/SCG = {0:f3}/{1:f3} = {2:f3} kN/m.", wo6, SCG, wo7));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total outer members segments of main long girders")); NOG = 2 * (NCG - 1);
            list.Add(string.Format("NOG = 2*(NCG - 1) = 2*({0:f0}-1) = {1:f0} nos.", NCG, NOG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            NOM = 20;
            list.Add(string.Format("NOM = 20 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wou = wo7 * NOG / NOM;
            list.Add(string.Format("wou = wo7*NOG/NOM = {0:f3}*{1:f0}/{2} = {3:f3} kN/m. = {4:f3} Ton/m.",
                wo7, NOG, NOM, wou, (wou = wou / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));
            list.Add(string.Format("wou = wou*swf = {0:f3}*{1:f3} = {2:f4} Kip/ft.",
                wou, swf, (wou = wou * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format("                MEMBER LOAD "));
            member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));

            //txt_ana_outer_long_girder_mem_load.Text = wou.ToString("f4"); //Chiranjit [2013 07 03]



            list.Add(string.Format("                121 TO 130 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //double FCG
            //list.Add(string.Format("Web Depth of cross girders = {0:f3} m. [DCG]", DCG));
            //list.Add(string.Format("Web thickness of cross girders = {0:f3} m. [BCG]", BCG));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Flange Width of cross girders = {0:f3} m. [FCG]", FCG));
            //list.Add(string.Format("Flange thickness of cross girders = {0:f3} m. [TCG]", TCG));

            Comp_sections.Section_Cross_Girder.Get_Input_Data(ref list);
            Comp_sections.Section_Cross_Girder.Get_Area_Result(ref list);

            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format(""));
            //wc1 = SMG * (DCG * BCG + 2 * FCG * TCG) * Y_S;
            wc1 = Comp_sections.Section_Cross_Girder.Area_in_Sq_FT * Y_S;

            //list.Add(string.Format("wc1 = SMG * (DCG * BCG + 2 * FCG * TCG) * Y_S = {0:f3} * ({1:f3} * {2:f3} + 2 * {3:f3} * {4:f3}) * {5:f3} = {6:f3} kN.",
            //    SMG, DCG, BCG, FCG, TCG, Y_S, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format("wc1 = Cross Section Area * Y_S"));
            list.Add(string.Format("    = {0:f6} * {1:f4}", Comp_sections.Section_Cross_Girder.Area_in_Sq_FT, Y_S));
            list.Add(string.Format("    = {0:f6} kN", wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            list.Add(string.Format("                13 TO 21 FZ -{0:f4}", wjl));
            list.Add(string.Format("                24 TO 32 FZ -{0:f4}", wjl));
            list.Add(string.Format("                35 TO 43 FZ -{0:f4}", wjl));
            list.Add(string.Format("                46 TO 54 FZ -{0:f4}", wjl));
            list.Add(string.Format("                57 TO 65 FZ -{0:f4}", wjl));
            list.Add(string.Format("                68 TO 76 FZ -{0:f4}", wjl));
            list.Add(string.Format("                79 TO 87 FZ -{0:f4}", wjl));
            list.Add(string.Format("                90 TO 98 FZ -{0:f4}", wjl));
            list.Add(string.Format("                101 TO 109 FZ -{0:f4}", wjl));


            member_load.Add(string.Format("JOINT LOAD"));
            member_load.Add(string.Format("13 TO 21 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("24 TO 32 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("35 TO 43 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("46 TO 54 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("57 TO 65 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("68 TO 76 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("79 TO 87 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("90 TO 98 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("101 TO 109 FZ -{0:f4}", wjl));

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));

            txt_Ana_member_load.Lines = member_load.ToArray();


            rtb_load_cal.Lines = list.ToArray();
            //File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }


        List<double> deck_member_load = new List<double>();

        List<string> Transverse_load = new List<string>();

        //string Left_support = "";
        //string Right_support = "";
        public void Calculate_Load_Computation(string outer_girders, string inner_girders, List<string> joints_nos)
        {

            Analysis_Initialize_InputData();

            List<string> list = new List<string>();
            List<string> member_load = new List<string>();




            double wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;


            double inner_dl1, outer_dl1, inner_dl2, outer_dl2, sidl, sufacing;


            inner_dl1 = 0.0;
            outer_dl1 = 0.0;
            inner_dl2 = 0.0;
            outer_dl2 = 0.0;
            sidl = 0.0;
            sufacing = 0.0;


            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for Composite Bridge"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            //SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double Y_deck = Y_w;

         



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));
          

            wi1 = SMG * SCG * (Ds * Y_c + Dw * Y_deck);
            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c, Dw, Y_deck));
            list.Add(string.Format("   = {0:f3} kN.", wi1));
            list.Add(string.Format(""));



            double Y_c_Green = 0.15;
            double wi1_green = SMG * SCG * (Ds * Y_c_Green + Dw * Y_deck);

            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c_Green, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} kN.", wi1_green));
            list.Add(string.Format(""));


            list.Add(string.Format("//Load of self weight of main long girder"));

            //wi2 = SCG * BMG * DMG * Y_S + 2 * FMG * TMG * Y_S;
            //list.Add(string.Format("wi2 = (SCG * BMG * DMG * Y_s) + (2*FMG*TMG*Y_S) = ({0:f3}*{1:f3}*{2:f3}*{3:f3}) + (2*{4:f3}*{5:f3}*{3:f3}) = {6:f3} kN.",
            //    SCG, BMG, DMG, Y_S, FMG, TMG, wi2));



            //Chiranjit [2012 12 14]
            double wi2_1 = 2 * SCG * Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_FT * Y_S;
            double wi2_2 = 2 * SCG * Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_FT * Y_S;
            double wi2_3 = 2 * SCG * Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_FT * Y_S;


            wi2 = wi2_1 + wi2_2 + wi2_3;

            list.Add(string.Format("wi2 = 2 * (SCG * AREA End Section) * Y_S +  2 * (SCG * AREA L4 Section) * Y_S +  2 * (SCG * AREA Mid Section) * Y_S"));

            list.Add(string.Format("    = 2 * ({0:f3} * {1:f3}) * {2:f3} +  2 * ({0:f3} * {3:f3}) * {2:f3}  +  2 * ({0:f3} * {4:f3}) * {2:f3}",
               SCG,
               Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_FT,
               Y_S,
               Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_FT,
               Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_FT));

            list.Add(string.Format(""));
            list.Add(string.Format("    = {0:f3} kN", wi2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders"));
            wi3 = wi1 + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kN.",
                wi1, wi2, wi3));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders (Green concrete Deckslab)"));
            double wi3_green = wi1_green + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kN.",
                wi1_green, wi2, wi3_green));
            list.Add(string.Format(""));








            list.Add(string.Format("//UDL (Dry concrete)"));
            wi4 = wi3 / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kN/m.",
                wi3, SCG, wi4));

            list.Add(string.Format("//UDL (Green concrete)"));
            double wi4_green = wi3_green / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kN/m.",
                wi3_green, SCG, wi4_green));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders"));
            NIG = NMG * (NCG - 1);
            list.Add(string.Format("NIG = NMG*(NCG - 1) = {0:f0} * ({1:f0}-1) = {2:f0} nos.",
                NMG, NCG, NIG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            NIM = 70;
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            list.Add(string.Format("NIM = 70 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wiu = wi4 * NIG / NIM;
            list.Add(string.Format("wiu = wi4*NIG/NIM = {0:f3} * {1}/70 = {2:f3} kN/m. = {3:f3} Ton/m.",
                wi4, NIG, wiu, (wiu = wiu / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));




            list.Add(string.Format("//Factored UDL"));

            list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} Ton/m.",
                wiu, swf, (wiu = wiu * swf)));


            inner_dl1 = wiu;
            inner_dl2 = (wi4_green * NIG / NIM) * swf / 10.0;

            outer_dl1 = wiu;
            outer_dl2 = (wi4_green * NIG / NIM) * swf / 10.0;





            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            member_load.Add(string.Format("MEMBER LOAD "));
            //member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wiu));

            list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("                {0} UNI GY -{1:f4}", inner_girders, wiu));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Outer Girder members"));
            list.Add(string.Format(""));
            //list.Add(string.Format("if(CL > CR) then (C=CL) else (C=CR)"));
            if (CL > CR) C = CL; else C = CR;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab and Wearing Course"));
            wo1 = ((SMG / 2) + C) * SCG * (Ds * Y_c + Dw * Y_w);
            list.Add(string.Format("wo1 = [(SMG/2) + C]*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} KIP.", wo1));
            list.Add(string.Format(""));




            list.Add(string.Format("//Load from Wearing Course"));
            double wo11 = C * SCG * (Dw * Y_w);



            list.Add(string.Format("wo1 = C *SCG*(Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} kN.", wo11));
            list.Add(string.Format(""));
            wo11 = wo11 / 10;
            list.Add(string.Format("   = {0:f3} T.", wo11));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main girder = wo1 / {0:f3} = {1:f3}/{0:f3} = {2:f3} Ton", SCG, wo11, (wo11 = wo11 / SCG)));
            list.Add(string.Format(""));

            sufacing = wo11;

            list.Add(string.Format("//Load of self weight of main long girder"));

            //Chiranjit [2012 12 14]
            //wo2 = (SCG * BMG * DMG * Y_S) + (2 * FMG * TMG * Y_S);
            wo2 = wi2_1 + wi2_2 + wi2_3;
            list.Add(string.Format("wo2 = wi2_1 + wi2_2 + wi2_3"));
            list.Add(string.Format("    = {0:f4} + {1:f4} + {2:f4}", wi2_1, wi2_2, wi2_3));
            list.Add(string.Format("    = {0:f4}", wo2));


            //outer_dl1 = wi1 wo2;

            //wo2 = wi2;
            //list.Add(string.Format("wo2 = (SCG*BMG*DMG*Y_c) + (2*FMG*TMG*Y_S) = ({0:f3}*{1:f3}*{2:f3}*{3:f3}) + (2*{4:f3}*{5:f3}*{3:f3}) = {6:f3} kN.",
            //    SCG, BMG, DMG, Y_c, FMG, TMG, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hc * Wc * Y_c;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hc, Wc, Y_c, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Wf * Hf * Y_c;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wf, Hf, Y_c, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * wr * Wk * Y_c;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, wr, Wk, Y_c, wo5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders ")); wo6 = wo1 + wo2 + wo3 + wo4 + wo5;
            list.Add(string.Format("wo6 = wo1 + wo2 + wo3 + wo4 + wo5 "));
            list.Add(string.Format("   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} ",
                wo1, wo2, wo3, wo4, wo5));
            list.Add(string.Format("   = {0:f3} kN.", wo6));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL")); wo7 = wo6 / SCG;
            list.Add(string.Format("wo7 = wo6/SCG = {0:f3}/{1:f3} = {2:f3} kN/m.", wo6, SCG, wo7));
            list.Add(string.Format(""));

            sidl = wo7 / 10;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total outer members segments of main long girders")); NOG = 2 * (NCG - 1);
            list.Add(string.Format("NOG = 2*(NCG - 1) = 2*({0:f0}-1) = {1:f0} nos.", NCG, NOG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            //NOM = MyList.Get_Array_Intiger(inner_girders).Count;
            NOM = 41;
            list.Add(string.Format("NOM = {0} nos.", NOM));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));

            wou = wo7 * NOG / NOM;
            list.Add(string.Format("wou = wo7*NOG/NOM = {0:f3}*{1:f0}/{2} = {3:f3} kN/m. = {4:f3} Ton/m.",
                wo7, NOG, NOM, wou, (wou = wou / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));


            list.Add(string.Format("wou = wou*swf = {0:f3}*{1:f3} = {2:f4} Ton/m.",
                wou, swf, (wou = wou * swf)));

            //outer_dl1 = wou;


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format("                MEMBER LOAD "));
            //member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            //member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                {0} UNI GY -{1:f4}", outer_girders, wou));
            //list.Add(string.Format("                121 TO 130 UNI GY -{0:f4}", wou));
            //list.Add(string.Format("                201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //double FCG
            //list.Add(string.Format("Web Depth of cross girders = {0:f3} m. [DCG]", DCG));
            //list.Add(string.Format("Web thickness of cross girders = {0:f3} m. [BCG]", BCG));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Flange Width of cross girders = {0:f3} m. [FCG]", FCG));
            //list.Add(string.Format("Flange thickness of cross girders = {0:f3} m. [TCG]", TCG));

            Comp_sections.Section_Cross_Girder.Get_Input_Data(ref list);
            Comp_sections.Section_Cross_Girder.Get_Area_Result(ref list);

            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format(""));
            //wc1 = SMG * (DCG * BCG + 2 * FCG * TCG) * Y_S;
            wc1 = Comp_sections.Section_Cross_Girder.Area_in_Sq_FT * Y_S;

            //list.Add(string.Format("wc1 = SMG * (DCG * BCG + 2 * FCG * TCG) * Y_S = {0:f3} * ({1:f3} * {2:f3} + 2 * {3:f3} * {4:f3}) * {5:f3} = {6:f3} kN.",
            //    SMG, DCG, BCG, FCG, TCG, Y_S, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format("wc1 = Cross Section Area * Y_S"));
            list.Add(string.Format("    = {0:f6} * {1:f4}", Comp_sections.Section_Cross_Girder.Area_in_Sq_FT, Y_S));
            list.Add(string.Format("    = {0:f6} kN", wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));

            list.Add(string.Format(""));

            NIMJ = 81;
            //NIMJ = MyList.Get_Array_Intiger(inner_girders).Count * 2;


            list.Add(string.Format("NIMJ = {0}", NIMJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));

            member_load.Clear();
            //member_load.Add(string.Format("LOAD 1 DL + SIDL"));
            member_load.Add(string.Format("LOAD 1 DEADLOAD"));
            member_load.Add(string.Format("MEMBER LOAD"));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, inner_dl1 * 0.4));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, outer_dl1 * 0.4)); //Chiranjit [2013 06 07]


            member_load.Add(string.Format("LOAD 2 DEADLOAD FROM GREEN CONCRETE "));
            member_load.Add(string.Format("MEMBER LOAD"));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, inner_dl2 * 0.6));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, outer_dl2 * 0.6)); //Chiranjit [2013 06 07]
            //member_load.Add(string.Format("JOINT LOAD"));
            //foreach (var item in joints_nos)
            //{
            //    list.Add(string.Format("                {0} FY -{1:f4}", item, wjl * 0.5));
            //    member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl * 0.5));

            //}
            //member_load.Add(string.Format("LOAD 3 SIDL"));
            //member_load.Add(string.Format("MEMBER LOAD"));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, sidl * 0.2 / 10)); //Chiranjit [2013 06 07]



            #region Weight of Crash Barrier Footpath Parapet

            double pp_width = MyList.StringToDouble(txt_Ana_Wpar.Text, 0.0);
            double pp_height = MyList.StringToDouble(txt_Ana_Hpar.Text, 0.0);
            double pp_weight = pp_width * pp_height * Y_c_Green / 10;

            double cb_width_LHS = MyList.StringToDouble(txt_Ana_Wc_LHS.Text, 0.0);
            double cb_height_LHS = MyList.StringToDouble(txt_Ana_Hc_LHS.Text, 0.0);

            double cb_width_RHS = MyList.StringToDouble(txt_Ana_Wc_RHS.Text, 0.0);
            double cb_height_RHS = MyList.StringToDouble(txt_Ana_Hc_RHS.Text, 0.0);

            double cb_weight_LHS = cb_width_LHS * cb_height_LHS * Y_c_Green / 10;
            double cb_weight_RHS = cb_width_RHS * cb_height_RHS * Y_c_Green / 10;


            double fp_width_LHS = MyList.StringToDouble(txt_Ana_wf_LHS.Text, 0.0);
            double fp_height_LHS = MyList.StringToDouble(txt_Ana_hf_LHS.Text, 0.0);
            double fp_width_RHS = MyList.StringToDouble(txt_Ana_wf_RHS.Text, 0.0);
            double fp_height_RHS = MyList.StringToDouble(txt_Ana_hf_RHS.Text, 0.0);

            double fp_weight_LHS = fp_width_LHS * fp_height_LHS * Y_c_Green / 10;
            double fp_weight_RHS = fp_width_RHS * fp_height_RHS * Y_c_Green / 10;

            sidl = pp_weight + cb_weight_LHS + fp_weight_LHS + cb_weight_RHS + fp_weight_RHS;




            #endregion Weight of Crash Barrier Footpath Parapet

            member_load.Add(string.Format("LOAD 3 SIDL"));
            member_load.Add(string.Format("MEMBER LOAD"));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, sidl * 0.2 / 10)); //Chiranjit [2013 06 07]

            string LHS_outer = Bridge_Analysis.Get_LHS_Outer_Girder();
            string RHS_outer = Bridge_Analysis.Get_RHS_Outer_Girder();


            if (pp_weight != 0.0)
            {
                member_load.Add(string.Format("*Parapet Load {0} Ton/m", pp_weight));
                member_load.Add(string.Format("{0} UNI GY -{1:f4}", LHS_outer, pp_weight));
                member_load.Add(string.Format("{0} UNI GY -{1:f4}", RHS_outer, pp_weight));
            }

            if (cb_weight_LHS != 0.0)
            {
                member_load.Add(string.Format("*Crash Barier Load LHS {0} Ton/m", cb_weight_LHS));
                member_load.Add(string.Format("{0} UNI GY -{1:f4}", LHS_outer, cb_weight_LHS));
            }

            if (cb_weight_RHS != 0.0)
            {
                member_load.Add(string.Format("*Crash Barier Load RHS {0} Ton/m", cb_weight_RHS));
                member_load.Add(string.Format("{0} UNI GY -{1:f4}", RHS_outer, cb_weight_RHS));
            }

            if (fp_weight_LHS != 0.0)
            {
                member_load.Add(string.Format("*Footpath Load RHS {0} Ton/m", fp_weight_LHS));
                member_load.Add(string.Format("{0} UNI GY -{1:f4}", LHS_outer, fp_weight_LHS));
            }
            if (fp_weight_RHS != 0.0)
            {
                member_load.Add(string.Format("*Footpath Load RHS {0} Ton/m", fp_weight_RHS));
                member_load.Add(string.Format("{0} UNI GY -{1:f4}", RHS_outer, fp_weight_RHS));
            }




            member_load.Add(string.Format("LOAD 4 SURFACINGS"));
            member_load.Add(string.Format("MEMBER LOAD"));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, sufacing * 0.5)); //Chiranjit [2013 06 07]
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, sufacing * 0.5)); //Chiranjit [2013 06 07]

            list.Add(string.Format(""));


            if (Curve_Radius > 0)
            {
                Transverse_load.Clear();
                #region Transverse Load




                //Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(Bridge_Analysis.Input_File), "ll.txt"));

                //if (Bridge_Analysis.Live_Load_List == null)
                //{
                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(long_ll);

                double dd = 0.0;
                List<double> max_lds = new List<double>();

                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)


                if (true)
                {

                    foreach (var item in all_loads)
                    {
                        dd = 0.0;
                        foreach (var item2 in item)
                        {
                            if (item2.StartsWith("TYPE"))
                            {
                                MyList ml = new MyList(item2, ' ');
                                if (ml.Count == 7)
                                {
                                    foreach (var item3 in Bridge_Analysis.Live_Load_List)
                                    {
                                        if (item3.TypeNo == (ml[0] + " " + ml[1]))
                                        {
                                            dd += item3.Total_Loads;
                                        }
                                    }
                                }

                            }

                        }
                        max_lds.Add(dd);

                    }

                    if (max_lds.Count > 0)
                    {
                        max_lds.Sort();
                        max_lds.Reverse();

                        dd = max_lds[0];
                    }
                }
                else
                {
                    //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

                    foreach (var item in Bridge_Analysis.LoadList)
                    {
                        foreach (var item2 in iApp.LiveLoads)
                        {
                            if (item2.TypeNo == item.TypeNo)
                            {
                                dd += item2.Total_Loads;
                            }
                        }
                    }
                }


                //foreach (LoadData ld in Bridge_Analysis.LoadList)



                double _V = MyList.StringToDouble(txt_curve_des_spd_mps.Text, 0.0);


                double _L = dd;

                double _M = _L / 9.81;

                double _R = MyList.StringToDouble(txt_curve_radius.Text, 0.0);
                double theta = (L / _R);


                Left_support = Bridge_Analysis.support_left_joints;
                Right_support = Bridge_Analysis.support_right_joints;
                if (_R != 0.0)
                {
                    double _F = _M * _V * _V / _R;

                    double _FX = _F * Math.Cos(theta);
                    double _FZ = _F * Math.Sin(theta);


                    int tot_sup = MyList.Get_Array_Intiger(Left_support).Count * 2;


                    if (Bridge_Analysis.Joints_Array == null) return;

                    List<int> out_jont = new List<int>();
                    for (int _i = 0; _i < Bridge_Analysis.Total_Columns; _i++)
                    {
                        out_jont.Add(Bridge_Analysis.Joints_Array[0, _i].NodeNo);
                    }

                    //Apply Load on Each Joints of Outer Girder

                    //Transverse_load.Add(string.Format("LOAD 1 TRANSVERSE LOAD"));
                    //Transverse_load.Add(string.Format("JOINT LOAD"));
                    //Transverse_load.Add(string.Format("{0} FX -{1:f4}", MyList.Get_Array_Text(out_jont), Math.Min(_FX, _FZ)/ )); // 
                    //Transverse_load.Add(string.Format("{0} FZ -{1:f4}", MyList.Get_Array_Text(out_jont), Math.Max(_FX, _FZ))); // 

                    //Apply Load on Each Joints of both End Supports
                    //Transverse_load.Add(string.Format("LOAD 1 TRANSVERSE LOAD"));
                    Transverse_load.Add(string.Format("JOINT LOAD"));
                    Transverse_load.Add(string.Format("{0} {1} FX {2:f4}", Left_support, Right_support, Math.Min(_FX, _FZ) / tot_sup)); // 
                    Transverse_load.Add(string.Format("{0} {1} FZ -{2:f4}", Left_support, Right_support, Math.Max(_FX, _FZ) / tot_sup)); // 





                    Transverse_load.Add(string.Format("{0} FX {1:f4}", Bridge_Analysis.support_inner_joints, (Math.Min(_FX, _FZ) * 2 / tot_sup))); // 
                    Transverse_load.Add(string.Format("{0} FZ -{1:f4}", Bridge_Analysis.support_inner_joints, Math.Max(_FX, _FZ) * 2 / tot_sup)); // 



                    txt_brg_max_HRL_Ton.Text = (Math.Min(_FX, _FZ) / tot_sup).ToString("f3");
                    txt_brg_max_HRT_Ton.Text = (Math.Max(_FX, _FZ) / tot_sup).ToString("f3");
                }


                list.Add(string.Format(""));
                list.Add(string.Format("                ***********************************"));
                list.Add(string.Format(""));

                #endregion Transverse Load
            }

            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));




            //Chiranjit [2013 10 05]
            //Dead Load Value for DeckSlab analysis
            #region Dead Load Value for DeckSlab analysis

            deck_member_load.Clear();
            deck_member_load.Add(outer_dl2 * 0.6);
            deck_member_load.Add(sidl * 0.01);
            deck_member_load.Add(sidl * 0.01 + sufacing * 0.3);

            #endregion Dead Load Value for DeckSlab analysis


            txt_Ana_member_load.Lines = member_load.ToArray();
            rtb_load_cal.Lines = list.ToArray();
            //File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        private void cmb_deck_applied_load_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, true);
        }


        #endregion Chiranjit [2012 06 10]

        private void cmb_deck_select_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplied ld;

            ComboBox cmb = sender as ComboBox;

        }


        #region Chiranjit [2012 07 04]
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;



        }
        #endregion Chiranjit [2012 07 04]

        #region Chiranjit [2012 07 06]

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        //IApplication iApp = null;
        //double B = 0.0;

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
                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);
                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data_DeadLoad()
        {

            dgv_left_end_design_forces.Rows.Clear();
            dgv_right_end_design_forces.Rows.Clear();

            SupportReaction sr = null;
            MyList mlist = new MyList(MyList.RemoveAllSpaces(Supports), ' ');

            double tot_dead_vert_reac = 0.0;
            double tot_live_vert_reac = 0.0;
            Bridge_Analysis.DL_Analysis_Deck.IsCurve = chk_curve.Checked;

            int _jnt_no = 0;
            for (int i = 0; i < mlist.Count / 2; i++)
            {
                try
                {
                    _jnt_no = mlist.GetInt(i);

                    var im = Bridge_Analysis.DL_Analysis_Deck.GetJoint_ShearForce(_jnt_no, 1);

                    dgv_left_end_design_forces.Rows.Add(_jnt_no, Math.Abs(im.Force).ToString("f3"));

                    tot_dead_vert_reac += Math.Abs(im.Force); ;
                }
                catch (Exception ex)
                {
                }

            }

            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    sr = LL_support_reactions.Get_Data(mlist.GetInt(i));
                    dgv_right_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));
                    tot_live_vert_reac += Math.Abs(sr.Max_Reaction);
                }
                catch (Exception ex)
                {
                }

            }
            txt_dead_vert_reac_ton.Text = (tot_dead_vert_reac).ToString("f3");
            txt_live_vert_rec_Ton.Text = (tot_live_vert_reac).ToString("f3");
        }


        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            //if (txt.Name == txt_dead_vert_reac_ton.Name)
            //{
            Text_Changed_Forces();
            //}

        }

        private void Text_Changed_Forces()
        {
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

        }
        #endregion View Force


        #endregion

        #region Chiranjit [2012 07 13]
        //Write All Data in a File
        public void Write_All_Data()
        {
            //  Save_FormRecord.Write_All_Data(this, user_path);

            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            if (showMessage) DemoCheck();

            if (user_path != iApp.LastDesignWorkingFolder)
                iApp.Save_Form_Record(this, user_path);
            return;

        }
        public void Read_All_Data()
        {
            //    Save_FormRecord.Read_All_Data(this, user_path);
            //    return;


            //if (iApp.IsDemo) return;

            string data_file = Bridge_Analysis.User_Input_Data;

            try
            {

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                }


            }
            catch (Exception ex) { }
            if (!File.Exists(data_file)) return;
        }
        #endregion Chiranjit [2012 07 13]

        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                //txt_Ana_L.Text = "0.0";
                //txt_Ana_L.Text = "19.58";
                //txt_Ana_B.Text = "12.0";
                //txt_Ana_CW.Text = "11.0";
                //txt_Ana_NMG.Text = "4";


                txt_Ana_L.Text = "0.0";
                if (rbtn_multiSpan.Checked)
                {
                    txt_multiSpan.Text = "120,120";
                    txt_Ana_L.Text = "240";
                }
                else
                    txt_Ana_L.Text = "120";


                //txt_Ana_wdeck.Text = "11.85";
                //txt_Ana_wroadway.Text = "11.0";
                ////txt_Ana_NMG.Text = "4";
                //cmb_Ana_NMG.Text = "3";
                //txt_curve_des_spd_kph.Text = "50";

                //string str = "This is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "Website  : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "Email at : techsoft@consultant.com, dataflow@mail.com\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]

        private void txt_section_TextChanged(object sender, EventArgs e)
        {
            Show_Steel_SectionProperties();
        }

        private void Show_Steel_SectionProperties()
        {


            List<string> result = new List<string>();

            #region 0.1 L

            Comp_sections.Section_Long_Girder_at_End_Span.Nb = MyList.StringToInt(txt_sec_end_Nb.Text, 1);
            Comp_sections.Section_Long_Girder_at_End_Span.S = MyList.StringToDouble(txt_sec_end_S.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bw = MyList.StringToDouble(txt_sec_end_Bw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dw = MyList.StringToDouble(txt_sec_end_Dw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bft = MyList.StringToDouble(txt_sec_end_Bft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dft = MyList.StringToDouble(txt_sec_end_Dft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bfb = MyList.StringToDouble(txt_sec_end_Bfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dfb = MyList.StringToDouble(txt_sec_end_Dfb.Text, 0.0);



            steel_section = Comp_sections.Section_Long_Girder_at_End_Span;


            #endregion 0.1 L

            #region 0.2 L and 0.3 L


            Comp_sections.Section_Long_Girder_at_L4_Span.Nb = MyList.StringToInt(txt_sec_L4_Nb.Text, 1);
            Comp_sections.Section_Long_Girder_at_L4_Span.S = MyList.StringToDouble(txt_sec_L4_S.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_L4_Span.Bw = MyList.StringToDouble(txt_sec_L4_Bw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dw = MyList.StringToDouble(txt_sec_L4_Dw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bft = MyList.StringToDouble(txt_sec_L4_Bft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dft = MyList.StringToDouble(txt_sec_L4_Dft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bfb = MyList.StringToDouble(txt_sec_L4_Bfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dfb = MyList.StringToDouble(txt_sec_L4_Dfb.Text, 0.0);


            steel_section = Comp_sections.Section_Long_Girder_at_L4_Span;



            #endregion region 0.2 L and 0.3 L

            #region 0.4L  to 1.0 L



            Comp_sections.Section_Long_Girder_at_Mid_Span.S = MyList.StringToDouble(txt_sec_L2_S.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_Mid_Span.Nb = MyList.StringToInt(txt_sec_L2_Nb.Text, 1);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bw = MyList.StringToDouble(txt_sec_L2_Bw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dw = MyList.StringToDouble(txt_sec_L2_Dw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bft = MyList.StringToDouble(txt_sec_L2_Bft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dft = MyList.StringToDouble(txt_sec_L2_Dft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bfb = MyList.StringToDouble(txt_sec_L2_Bfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dfb = MyList.StringToDouble(txt_sec_L2_Dfb.Text, 0.0);

            steel_section = Comp_sections.Section_Long_Girder_at_Mid_Span;


            #endregion


            #region Cross Girder


            Comp_sections.Section_Cross_Girder.Nb = MyList.StringToInt(txt_sec_cross_Nb.Text, 1);
            Comp_sections.Section_Cross_Girder.S = MyList.StringToDouble(txt_sec_cross_S.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bw = MyList.StringToDouble(txt_sec_cross_Bw.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Dw = MyList.StringToDouble(txt_sec_cross_Dw.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bft = MyList.StringToDouble(txt_sec_cross_Bft.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Dft = MyList.StringToDouble(txt_sec_cross_Dft.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bfb = MyList.StringToDouble(txt_sec_cross_Bfb.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Dfb = MyList.StringToDouble(txt_sec_cross_Dfb.Text, 0.0);

            Comp_sections.Section_Cross_Girder.Steel_Unit_Weight = Y_S / 10;
            Comp_sections.Section_Long_Girder_at_End_Span.Steel_Unit_Weight = Y_S / 10;
            Comp_sections.Section_Long_Girder_at_L4_Span.Steel_Unit_Weight = Y_S / 10;
            Comp_sections.Section_Long_Girder_at_Mid_Span.Steel_Unit_Weight = Y_S / 10;
            steel_section = Comp_sections.Section_Cross_Girder;

            #endregion Cross Girder

            Comp_sections.Angle_Section = iApp.Tables.Get_AngleData_FromTable(cmb_ana_ang_section_name.Text, cmb_ana_ang_section_code.Text, MyList.StringToDouble(cmb_ana_ang_thk.Text, 10));

            //Comp_sections.Ds = Ds * 1000;
            Comp_sections.Ds = Ds * 12;
            Comp_sections.m = m;



            result.Clear();

            result.Add("---------------------------------");
            result.Add("Calculation of Moment of Inertia");
            result.Add("---------------------------------");
            result.Add("");
            result.AddRange(Comp_sections.Section_Long_Girder_at_End_Span.Get_Result("Section Long Girder at End Span"));
            result.AddRange(Comp_sections.Section_Long_Girder_at_L4_Span.Get_Result("Section Long Girder at Penultimate_Span Section"));
            result.AddRange(Comp_sections.Section_Long_Girder_at_Mid_Span.Get_Result("Section Long Girder at Mid Span Section"));
            result.AddRange(Comp_sections.Section_Cross_Girder.Get_Result("Section Cross Girder Section"));
            rtb_section.Lines = result.ToArray();


            dgv_sec_res.Rows.Clear();

            dgv_sec_res_details.Rows.Clear();


            dgv_sec_res.Rows.Add("End Span Section (Simple Section) ",
                Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyy_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_FT.ToString("f5"));



            if (rbtn_sec_plate.Checked)
            {
                dgv_sec_res.Rows.Add("End Span Section (Composite Section) ",
                    Comp_sections.Section_Long_Girder_at_End_Span.AX_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_End_Span.IX_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_End_Span.IY_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_End_Span.IZ_Comp.ToString("f5"));
            }




            dgv_sec_res_details.Rows.Add("End Span ",
                Comp_sections.Section_Long_Girder_at_End_Span.Ixb.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixtp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixbp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixp1.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixp2.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixp3.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixp4.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyb.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iytp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iybp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyp1.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyp2.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyp3.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyp4.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixx.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyy.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_End_Span.Izz.ToString("E3"));



            dgv_sec_res.Rows.Add("Penultimate Span Section (L/4) (Simple Section) ",
                Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyy_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_FT.ToString("f5"));



            if (rbtn_sec_plate.Checked)
            {
                dgv_sec_res.Rows.Add("Penultimate Span Section (L/4) (Composite Section) ",
                    Comp_sections.Section_Long_Girder_at_L4_Span.AX_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_L4_Span.IX_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_L4_Span.IY_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_L4_Span.IZ_Comp.ToString("f5"));
            }


            dgv_sec_res_details.Rows.Add("Penultimate Span (L/4) ",
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixb.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixtp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixbp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixp1.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixp2.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixp3.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixp4.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyb.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iytp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iybp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyp1.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyp2.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyp3.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyp4.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixx.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyy.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Izz.ToString("E3"));



            dgv_sec_res.Rows.Add("Mid Span Section (L/2) (Simple Section) ",
                Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyy_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_FT.ToString("f5"));



            if (rbtn_sec_plate.Checked)
            {
                dgv_sec_res.Rows.Add("Mid Span Section (L/2) (Composite Section) ",
                    Comp_sections.Section_Long_Girder_at_Mid_Span.AX_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_Mid_Span.IX_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_Mid_Span.IY_Comp.ToString("f5"),
                    Comp_sections.Section_Long_Girder_at_Mid_Span.IZ_Comp.ToString("f5"));
            }

            dgv_sec_res_details.Rows.Add("Mid Span (L/2) ",
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixb.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixtp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixbp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixp1.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixp2.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixp3.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixp4.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyb.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iytp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iybp.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyp1.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyp2.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyp3.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyp4.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixx.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyy.ToString("E3"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Izz.ToString("E3"));


            dgv_sec_res.Rows.Add("Cross Section ",
                Comp_sections.Section_Cross_Girder.Area_in_Sq_FT.ToString("f5"),
                Comp_sections.Section_Cross_Girder.Ixx_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Cross_Girder.Iyy_in_Sq_Sq_FT.ToString("f5"),
                Comp_sections.Section_Cross_Girder.Izz_in_Sq_Sq_FT.ToString("f5"));



            dgv_sec_res_details.Rows.Add("Cross Section ",
                Comp_sections.Section_Cross_Girder.Ixb.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixtp.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixbp.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixp1.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixp2.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixp3.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixp4.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iyb.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iytp.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iybp.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iyp1.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iyp2.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iyp3.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iyp4.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Ixx.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Iyy.ToString("E3"),
                Comp_sections.Section_Cross_Girder.Izz.ToString("E3"));


            Set_Design_Composite_Section(Comp_sections);
            Calculate_Load_Computation();


            //txt_sec_Ixb.Text = steel_section.Ixb.ToString("E3");
            //txt_sec_Ixtp.Text = steel_section.Ixtp.ToString("E3");
            //txt_sec_Ixbp.Text = steel_section.Ixbp.ToString("E3");
            //txt_sec_Ixp1.Text = steel_section.Ixp1.ToString("E3");
            //txt_sec_Ixp2.Text = steel_section.Ixp2.ToString("E3");
            //txt_sec_Ixp3.Text = steel_section.Ixp3.ToString("E3");
            //txt_sec_Ixp4.Text = steel_section.Ixp4.ToString("E3");
            //txt_sec_Ixx.Text = steel_section.Ixx.ToString("E3");
            //txt_sec_Iyb.Text = steel_section.Iyb.ToString("E3");
            //txt_sec_Iytp.Text = steel_section.Iytp.ToString("E3");
            //txt_sec_Iybp.Text = steel_section.Iybp.ToString("E3");
            //txt_sec_Iyp1.Text = steel_section.Iyp1.ToString("E3");
            //txt_sec_Iyp2.Text = steel_section.Iyp2.ToString("E3");
            //txt_sec_Iyp3.Text = steel_section.Iyp3.ToString("E3");
            //txt_sec_Iyp4.Text = steel_section.Iyp4.ToString("E3");
            //txt_sec_Iyy.Text = steel_section.Iyy.ToString("E3");
            //txt_sec_Izz.Text = steel_section.Izz.ToString("E3");
            //txt_sec_Ax.Text = steel_section.Ax.ToString("f3");




            //steel_long_girder_section = new Steel_Girder_Section(steel_section);
            //steel_long_girder_section.Dw = DMG * 1000.0;
            //steel_long_girder_section.Bw = BMG * 1000.0;
            //steel_long_girder_section.Bft = FMG * 1000.0;
            //steel_long_girder_section.Dft = TMG * 1000.0;
            //steel_long_girder_section.Bfb = FMG * 1000.0;
            //steel_long_girder_section.Dfb = TMG * 1000.0;

            //steel_cross_girder_section = new Steel_Girder_Section(steel_section);
            //steel_cross_girder_section.Dw = DCG * 1000.0;
            //steel_cross_girder_section.Bw = BCG * 1000.0;
            //steel_cross_girder_section.Bft = FCG * 1000.0;
            //steel_cross_girder_section.Dft = TCG * 1000.0;
            //steel_cross_girder_section.Bfb = FCG * 1000.0;
            //steel_cross_girder_section.Dfb = TCG * 1000.0;

        }
        public CompositeSection_AASHTO Steel_Composite_Section()
        {

            CompositeSection_AASHTO steel_composite_sections = new CompositeSection_AASHTO();

            //Comp_sections.Spacing_Cross_Girder = SCG;
            //Comp_sections.Spacing_Long_Girder = SMG;

            steel_composite_sections.Spacing_Cross_Girder = Comp_sections.Spacing_Cross_Girder;
            steel_composite_sections.Spacing_Long_Girder = Comp_sections.Spacing_Long_Girder;

            //steel_composite_sections.Bs = Comp_sections.Spacing_Long_Girder * 1000;
            steel_composite_sections.Ds = Ds * 1000;
            steel_composite_sections.m = m;

            Comp_sections.Section_Cross_Girder.Length = B;
            Comp_sections.Section_Cross_Girder.NumberOfGirder = (int)NCG;

            steel_composite_sections.Section_Cross_Girder = Comp_sections.Section_Cross_Girder;



            steel_composite_sections.Section_Cross_Girder.Steel_Unit_Weight = Y_S / 10;
            steel_composite_sections.Section_Long_Girder_at_End_Span.Steel_Unit_Weight = Y_S / 10;
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Steel_Unit_Weight = Y_S / 10;
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Steel_Unit_Weight = Y_S / 10;


            return steel_composite_sections;

        }


        public void Set_Analysis_Composite_Section(CompositeSection steel_composite_sections)
        {
            #region End Span

            txt_sec_end_Nb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Nb.ToString();
            txt_sec_end_S.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.S.ToString();
            txt_sec_end_Bw.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bw.ToString();
            txt_sec_end_Dw.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dw.ToString();
            txt_sec_end_Bft.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bft.ToString();
            txt_sec_end_Dft.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dft.ToString();
            txt_sec_end_Bfb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bfb.ToString();
            txt_sec_end_Dfb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dfb.ToString();
           
            #endregion End Span

            #region L4_Span

            txt_sec_L4_Nb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Nb.ToString();
            txt_sec_L4_S.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.S.ToString();

            txt_sec_L4_Bw.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bw.ToString();
            txt_sec_L4_Dw.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dw.ToString();
            txt_sec_L4_Bft.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bft.ToString();
            txt_sec_L4_Dft.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dft.ToString();
            txt_sec_L4_Bfb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bfb.ToString();
            txt_sec_L4_Dfb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dfb.ToString();

            #endregion L4_Span

            #region Mid_Span



            txt_sec_L2_S.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.S.ToString();

            txt_sec_L2_Nb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Nb.ToString();
            txt_sec_L2_Bw.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bw.ToString();
            txt_sec_L2_Dw.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dw.ToString();
            txt_sec_L2_Bft.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bft.ToString();
            txt_sec_L2_Dft.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dft.ToString();
            txt_sec_L2_Bfb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bfb.ToString();
            txt_sec_L2_Dfb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dfb.ToString();
          

            #endregion
        }
        public void Set_Design_Composite_Section(CompositeSection_AASHTO steel_composite_sections)
        {
        }

        private void rbtn_sec_plate_CheckedChanged(object sender, EventArgs e)
        {
            return;
            
        }

        frmCompositeDiagram fcom;
        private void btn_sec_view_Click(object sender, EventArgs e)
        {
            try
            {
                fcom.Close();
            }
            catch (Exception ex) { }
            fcom = new frmCompositeDiagram();
            fcom.Owner = this;
            fcom.Show();
        }

        private void txt_steel_end_Nb_TextChanged(object sender, EventArgs e)
        {
            CompositeSection_AASHTO cm = Steel_Composite_Section();
        }

        #region Deck Slab Limit State Method




        public void Default_Moving_LoadData(DataGridView dgv_live_load)
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
        }

        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();

            if (dgv_live_load.Name == dgv_long_loads.Name)
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
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
                else
                {
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
                }
            }

            Default_Moving_Type_LoadData(dgv_live_load, list);
        }
        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load, List<string> list)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
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


        List<string> Result { get; set; }


        #region Excel Files

        public string Excel_Deckslab
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\RCC T Girder BS\RCC Deck Slab BS.xls");

                return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\RCC T Girder IS\RCC Deck Slab IS.xlsx");
            }
        }

        #endregion Excel Files
        private void btn_LS_Click(object sender, EventArgs e)
        {

        }





        #endregion Deck Slab Limit State Method



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
            iApp.Show_LL_Dialog();
            //iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
        }

        #region British Standard Loading



        public bool IsRead = false;


        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();

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


        public void LONG_GIRDER_LL_TXT()
        {


            Store_LL_Combinations(dgv_long_liveloads, dgv_long_loads);

            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();

            bool flag = false;
            for (i = 0; i < dgv_long_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_long_liveloads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        //if (long_ll_impact.Contains(kStr) == false)
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
            //long_ll.Add(string.Format("TYPE 6 IRC40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            List<string> files = new List<string>();


            List<string> load_list_1 = new List<string>();
            List<string> load_list_2 = new List<string>();
            List<string> load_list_3 = new List<string>();
            List<string> load_list_4 = new List<string>();
            List<string> load_list_5 = new List<string>();
            List<string> load_list_6 = new List<string>();
            List<string> load_total_7 = new List<string>();


            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);

            double imp_fact = 1.179;

            int count = 0;
            for (i = 0; i < dgv_long_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_long_loads[0, i].Value.ToString();

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
                    list.Add("LOAD GENERATION " + txt_LL_load_gen.Text);

                    string fn = "";
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);

                        fn = fn + " " + def_load[j];
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
                for (c = 1; c < dgv_long_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_loads[c, i].Value.ToString();

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

        void Ana_Write_Long_Girder_Load_Data1(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
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
                    foreach (var item in txt_Ana_member_load.Lines)
                    {

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
                    load_lst.AddRange(txt_Ana_member_load.Lines);
            }
            else
            {
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    load_lst.Add("LOAD 1 STEEL GIRDER");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY  -0.290", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} UNI GY  -0.290", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 2 CONCRETE DECK AND HAUNCHES");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY  -1.14", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} UNI GY  -1.14", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 3 OTHER DEAD LOADS ACTING ON GIRDER ALONE");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY  -0.214", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} UNI GY  -0.214", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 4 CONCRETE PARAPETS");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY  -0.414", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 5 FUTURE WEARING SURFACE");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY  -0.314", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} UNI GY  -0.314", Bridge_Analysis.Outer_Girders_as_String));

                    //foreach (var item in txt_Ana_member_load.Lines)
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
                }
                else
                {

                    #region Deck Dead Loads
                    load_lst.Add("LOAD 1 SLAB DEAD LOAD");
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 4 PARAPET DEAD LOAD");
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 5 FUTURE WEARING SURFACE DEAD LOAD");
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                    #endregion Steel Girder Dead Loads
                }
            }

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Ana_Write_Deck_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
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
                    foreach (var item in txt_Ana_member_load.Lines)
                    {

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
                {
                    #region Deck Dead Loads
                    load_lst.Add("LOAD 1 SLAB DEAD LOAD");
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 4 PARAPET DEAD LOAD");
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                    load_lst.Add("LOAD 5 FUTURE WEARING SURFACE DEAD LOAD");
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Inner_Girders_as_String));
                    load_lst.Add(string.Format("{0} FY -0.014", Bridge_Analysis.Outer_Girders_as_String));
                    #endregion Steel Girder Dead Loads
                }
            }

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;


            #region Set File Name

            string file_name = "";
            if (Bridge_Analysis != null)
            {
                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    if (cmb.SelectedIndex < cmb.Items.Count - 1)
                    {
                        file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
                    }
                    else
                    {
                        file_name = Bridge_Analysis.TotalAnalysis_Input_File;
                    }
                }
                //else
                //{
                //    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
                //}
            }

            #endregion Set File Name


            if (cmb == cmb_long_open_file_process)
            {

                btn_view_data.Enabled = File.Exists(file_name);
                btn_View_Moving_Load.Enabled = File.Exists(FILE_SUMMARY_RESULTS);
                btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
            }
        }

        private void Analysis_Button_Enabled()
        {
            #region Set File Name

            string file_name = "";
            if (Bridge_Analysis != null)
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    if (cmb_long_open_file_process.SelectedIndex < cmb_long_open_file_process.Items.Count - 1)
                    {
                        file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file_process.SelectedIndex);
                    }
                    else
                    {
                        file_name = Bridge_Analysis.TotalAnalysis_Input_File;
                    }
                }
                else
                {
                    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file_process.SelectedIndex);
                    //if (cmb_long_open_file_process.SelectedIndex < cmb_long_open_file_process.Items.Count - 1)
                    //{
                    //    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file_process.SelectedIndex);
                    //}
                    //else
                    //{
                    //    file_name = Result_Report;
                    //}
                }
            }

            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_View_Moving_Load.Enabled = File.Exists(MyList.Get_LL_TXT_File(file_name)) && File.Exists(MyList.Get_Analysis_Report_File(file_name));
            //btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file_process.SelectedIndex != cmb_long_open_file_process.Items.Count - 1;
            btn_view_preprocess.Enabled = File.Exists(file_name);
            btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
        }

        #endregion British Standard Loading



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text_Changed();
        }

        private void btn_comp_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_comp_browse.Name)
            {

                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    //user_path = Path.Combine(user_path, Project_Name);
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);



                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                        //Write_All_Data();
                    }
                    #endregion Save As



                    string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                    //if (!File.Exists(chk_file)) chk_file = ofd.FileName;



                    IsRead = true;
                    iApp.Read_Form_Record(this, user_path);
                    IsRead = false;


                    Analysis_Initialize_InputData();
                    Create_Data();

                    Ana_OpenAnalysisFile(chk_file);
                    Show_ReadMemberLoad(Bridge_Analysis.TotalAnalysis_Input_File);




                    //dgv_british_loads

                    Open_Create_Data();//Chiranjit [2013 06 25]







                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btn.Name == btn_comp_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_comp_project.Text != "")
                //    frm.Project_Name = txt_comp_project.Text;
                //else
                //    frm.Project_Name = "Design of Composite Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_comp_project.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    IsCreate_Data = true;
                //}
                IsCreate_Data = true;
                Create_Project();
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
                return eASTRADesignType.Composite_Bridge_LS;
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

            iApp.user_path = user_path;
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



        private void uC_PierDesignLimitState1_OnProcess(object sender, EventArgs e)
        {
            Write_All_Data();
        }

        private void uC_RCC_Abut1_Load(object sender, EventArgs e)
        {

        }


        private void uC_PierDesignLSM1_OnProcess(object sender, EventArgs e)
        {
            if (!Check_Project_Folder()) return;

            Write_All_Data(false);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            grb_curve.Enabled = chk_curve.Checked;

            if (grb_curve.Enabled)
            {
                if (Curve_Radius == 0) txt_curve_radius.Text = "250";

                Skew = 0;
            }
            else
            {
                txt_curve_radius.Text = "0";
            }
            txt_Ana_Skew.Enabled = !chk_curve.Checked;
        }

        private void rbtn_singleSpan_CheckedChanged(object sender, EventArgs e)
        {
            //return;
            if (rbtn_singleSpan.Checked)
            {
                txt_Ana_L.Text = "120";
                txt_multiSpan.Text = txt_Ana_L.Text;
                //cmb_Ana_NMG.SelectedIndex = 1;
                txt_XINCR.Text = "0.5";
            }
            else if (rbtn_multiSpan.Checked)
            {
                //cmb_Ana_NMG.SelectedIndex = 0;
                txt_multiSpan.Text = "120,120";
                MyList ml = new MyList(txt_multiSpan.Text, ',');
                txt_Ana_L.Text = ml.SUM.ToString();
                //txt_XINCR.Text = "4.0";
            }
        }

        private void txt_multiSpan_TextChanged(object sender, EventArgs e)
        {
            MyList ml = new MyList(txt_multiSpan.Text.Replace(",", " "), ' ');
            txt_Ana_L.Text = ml.SUM.ToString();
        }

        private void lbl_orthotropic_Click(object sender, EventArgs e)
        {
            AstraAccess.ViewerFunctions.Form_OrthotropicEditor(iApp).Show();
        }


        private void uC_Orthotropic1_OnDraw_Click(object sender, EventArgs e)
        {
            SectionElement elmt = new SectionElement();
            var ES = Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span;

            var cgrds = (int)(Bridge_Analysis.Spans.Count * NCG - 3);

            for (int r = 0; r < (int)NMG; r++)
            {


                elmt.Curve_Radius = Curve_Radius;
                elmt.L = L;
                elmt.Z = CL + SMG * r;


                elmt._Columns = cgrds;

                elmt.Web_Thickness = ES.Bw/12;
                elmt.Web_Depth = ES.Dw / 12;

                elmt.TF_THK = ES.Dft / 12;
                elmt.TF_WD = ES.Bft / 12;
                elmt.BF_THK = ES.Dfb / 12;
                elmt.BF_WD = ES.Bfb / 12;

                elmt.Lat_Spacing = ES.S / 12;


                elmt.TP_WD = ES.Bt / 12;
                elmt.TP_THK = ES.Dt / 12;

                elmt.BP_WD = ES.Bb / 12;
                elmt.BP_THK = ES.Db / 12;

                elmt.SP_1_THK = ES.Bs1 / 12;
                elmt.SP_2_THK = ES.Bs2 / 12;
                elmt.SP_3_THK = ES.Bs3 / 12;
                elmt.SP_4_THK = ES.Bs4 / 12;

                elmt.SP_1_WD = ES.Ds1 / 12;
                elmt.SP_2_WD = ES.Ds2 / 12;
                elmt.SP_3_WD = ES.Ds3 / 12;
                elmt.SP_4_WD = ES.Ds4 / 12;

                //uC_Orthotropic1.DrawElement(elmt);

            }

            if (true)
            {

                #region Deck Slab


                elmt = new SectionElement();

                elmt.Curve_Radius = Curve_Radius;

                //elmt.Y = ES.Dw / 1000 + ES.Dt / 1000 + ES.Dfb / 1000 + ES.Db / 1000 + ES.Dfb / 1000;
                elmt.Y = ES.Dw / 12 + ES.Dt / 12 + ES.Dfb / 12 + ES.Db / 12 + ES.Dfb / 12;
                elmt.Z = B / 2;
                elmt.L = L;
                elmt.Web_Thickness = B;
                elmt.Web_Depth = Ds/12;

                elmt.Color_Web_Plate = Color.White;
                elmt._Columns = cgrds;
                //uC_Orthotropic1.DrawElement(elmt);

                #endregion Deckslab
                ES = Bridge_Analysis.Steel_Section.Section_Cross_Girder;
                //for (int i = 0; i <= 10; i++)
                for (int i = 0; i <= cgrds; i++)
                {
                    #region Cross Girder
                    if (i == 0)
                    {
                        //elmt.X = L - (ES.Bfb / 2) / 1000;
                        //elmt.X = L - (ES.Bfb + ES.Dw / 2) / 1000;
                        elmt.X = L - (ES.Bfb / 2 + ES.Bw) / 12;
                    }
                    else if (i == cgrds)
                    {
                        elmt.X = (ES.Bfb / 2) / 12;
                    }
                    else
                    {
                        elmt.X = L - (L / cgrds) * i;
                    }
                    //elmt.Y = ((Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span.Dw - ES.Dw) / 2) / 1000;

                    var EL = Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span;

                    elmt.Y = (EL.Dw - ES.Dw + ES.Dt + ES.Dft) / 12;

                    if (rbtn_sec_box.Checked)
                    {
                        elmt.Z = CL - (Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span.S / 2) / 12;
                    }
                    else
                        elmt.Z = CL;


                    elmt.Is_Cross_Girder = true;
                    if (rbtn_sec_box.Checked)
                    {
                        elmt.L = B - elmt.Z * 2;
                    }
                    else
                        elmt.L = B - CL - CR;
                    elmt.Web_Thickness = ES.Bw / 12;
                    elmt.Web_Depth = ES.Dw / 12;

                    elmt.TF_THK = ES.Dft / 12;
                    elmt.TF_WD = ES.Bft / 12;
                    elmt.BF_THK = ES.Dfb / 12;
                    elmt.BF_WD = ES.Bfb / 12;




                    elmt.TP_THK = ES.Dt / 12;
                    elmt.TP_WD = ES.Bt / 12;
                    elmt.BP_THK = ES.Db / 12;
                    elmt.BP_WD = ES.Bb / 12;



                    elmt.Lat_Spacing = ES.S / 12;


                    elmt.SP_1_THK = ES.Bs1 / 12;
                    elmt.SP_2_THK = ES.Bs2 / 12;
                    elmt.SP_3_THK = ES.Bs3 / 12;
                    elmt.SP_4_THK = ES.Bs4 / 12;

                    elmt.SP_1_WD = ES.Ds1 / 12;
                    elmt.SP_2_WD = ES.Ds2 / 12;
                    elmt.SP_3_WD = ES.Ds3 / 12;
                    elmt.SP_4_WD = ES.Ds4 / 12;


                    //uC_Orthotropic1.DrawElement(elmt);

                    #endregion
                }
            }

            //Orthotropic_Initialize_InputData();
        }

        private void rbtn_steel_deck_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void btn_IRC_Loadings_Click(object sender, EventArgs e)
        {
            string load_help = Path.Combine(Application.StartupPath, "ASTRAHelp\\AASHTO DESIGN LRFD Truck Load.pdf");
            if (File.Exists(load_help)) System.Diagnostics.Process.Start(load_help);

            //Default_Moving_Type_LoadData(dgv_long_loads, iApp.IRC_6_2014_Load_Combinations(CW));
        }

        private void txt_Ana_length_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txt_Ana_X.Text = "-" + txt_Ana_L.Text; //Chiranjit [2013 05 29]
                Text_Changed();
            }
            catch (Exception ex) { }
        }


        Composite_Orthotropic_Analysis Ortho_Analysis;

        void Orthotropic_Initialize_InputData()
        {

            Ortho_Analysis = new Composite_Orthotropic_Analysis(iApp);

            Ortho_Analysis.Length = L;

            Ortho_Analysis.Spans = new List<double>();
            try
            {

                if (rbtn_multiSpan.Checked)
                {
                    MyList spans = new MyList(MyList.RemoveAllSpaces(txt_multiSpan.Text.Replace(',', ' ')), ' ');
                    for (int i = 0; i < spans.StringList.Count; i++)
                    {
                        Ortho_Analysis.Spans.Add(spans.GetDouble(i));
                    }
                }
                else
                {
                    Ortho_Analysis.Spans.Add(L);
                }
            }
            catch (Exception exx) { }
            Ortho_Analysis.Length = Bridge_Analysis.Total_Length;
            Ortho_Analysis.WidthBridge = B;
            Ortho_Analysis.Width_LeftCantilever = CL;
            Ortho_Analysis.Width_RightCantilever = CR;
            Ortho_Analysis.Skew_Angle = Skew;
            Ortho_Analysis.Effective_Depth = Deff;
            Ortho_Analysis.NMG = NMG;
            Ortho_Analysis.NCG = NCG;
            Ortho_Analysis.Ds = Ds;

            Ortho_Analysis.Radius = MyList.StringToDouble(txt_curve_radius.Text, 0.0);

            if (!chk_curve.Checked) Ortho_Analysis.Radius = 0;


            Ortho_Analysis.Input_File = Bridge_Analysis.Input_File;


            Ortho_Analysis.Start_Support = Start_Support_Text;
            Ortho_Analysis.End_Support = END_Support_Text;

            //Ortho_Analysis.Steel_Section = Bridge_Analysis.Steel_Section;
            //Create Orthotropic Data
            Ortho_Analysis.CreateData_Orthotropic();
            Ortho_Analysis.WriteData_Orthotropic_Analysis(Ortho_Analysis.Orthotropic_Input_File);
            //Ana_Write_Long_Girder_Load_Data(Ortho_Analysis.Orthotropic_Input_File, false, true, 1);

            //uC_Orthotropic1.Input_Data_File = Ortho_Analysis.Orthotropic_Input_File;

            MessageBox.Show("Orthotropic Input Data created as \n\n" + Ortho_Analysis.Orthotropic_Input_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void uC_Orthotropic1_OnRunAnalysis_Click(object sender, EventArgs e)
        {

            //uC_Orthotropic1.Input_Data_File = Bridge_Analysis.Orthotropic_Input_File;
        }

        private void uC_Orthotropic1_OnCreateData_Click(object sender, EventArgs e)
        {
            Orthotropic_Initialize_InputData();
        }

        private void tc_AnaProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_AnaProcess.SelectedTab == tab_ana_process)
            {
                //if (!ucPreProcess1.IsFlag)
                //{
                //ucPreProcess1.FilePath = Get_LongGirder_File(cmb_long_open_file_process.SelectedIndex);
                //ucPreProcess1.Load_Initials();
                //ucPreProcess1.IsFlag = true;
                if (cmb_long_open_file_process.SelectedIndex == -1)
                {
                    if (cmb_long_open_file_process.Items.Count > 0)
                        cmb_long_open_file_process.SelectedIndex = 0;
                    //ucPostProcess1.Load_Initials(ucPreProcess1.FilePath);
                }

                //}
            }
        }

        private void btn_edit_load_combs_IRC_Click(object sender, EventArgs e)
        {
            //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            //{
                LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
                ff.Owner = this;
                ff.ShowDialog();
            //}
        }

        private void btn_long_restore_ll_IRC_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (btn.Name == btn_long_restore_ll_IRC.Name)
                    Default_Moving_LoadData(dgv_long_liveloads);
            }
        }

        public void Save_Input_Data()
        {
            List<string> list = new List<string>();



            list.Add(string.Format(""));
            list.Add(string.Format("BASIC INPUT DATA FOR {0}", Title));
            list.Add(string.Format("-------------------------------------------------------------------------------------------"));

            #region Input Data
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Deck Span (along X-direction)  [L]= {0} m", txt_Ana_L.Text));
            if (rbtn_multiSpan.Checked)
            {
                list.Add(string.Format("Spans (Separated by comma ',')= {0} m", txt_multiSpan.Text));
                list.Add(string.Format(""));
            }

            if (chk_curve.Checked)
            {
                list.Add(string.Format("Curved Span"));
                list.Add(string.Format("-----------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Radius = {0} m", txt_curve_radius.Text));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Central Angle = {0} degree", txt_curve_angle.Text));
                list.Add(string.Format(""));
                list.Add(string.Format("Design Speed = {0} km/h = {1} m/s", txt_curve_des_spd_kph.Text, txt_curve_des_spd_mps.Text));
                list.Add(string.Format(""));
            }
            list.Add(string.Format("Total number of Long Main girders [NMG] = {0} m", cmb_Ana_NMG.Text));
            list.Add(string.Format("Total number of Cross girders [NCG]= {0} m", NCG));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Bridge Deck  (along Z-direction) [B]= {0} m", txt_Ana_wdeck.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Carriageway Width [CW] (must be < Width of Bridge Deck)= {0} m", txt_Ana_wroadway.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Left Cantilever part of Deck Slab [CL] (must be < Width of Bridge Deck/3)= {0} m", txt_Ana_Soverhang.Text));
            list.Add(string.Format("Width of Right Cantilever part of Deck Slab [CR] (must be < Width of Bridge Deck/3)= {0} m", txt_Ana_Soverhang.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Thickness of Deck Slab [Ds]= {0} m", txt_Ana_tdeck.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Skew Angle [Ang]= {0} degree", txt_Ana_Skew.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Unit weight of Concrete [Y_c]= {0} kN/Cu.m", txt_Ana_Wc.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Unit weight of Steel [Y_s]= {0} kN/Cu.m", txt_Ana_Ws.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Modular Ratio [m]= {0}", txt_Ana_n.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("WEARING COURSE"));
            list.Add(string.Format("-----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Thickness of Wearing Course  [Dw]= {0} m", txt_Ana_tfws.Text));
            list.Add(string.Format("Unit weight of Wearing Course [Y_w]= {0} kN/Cu.m", txt_Ana_Wfws.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("CRASH BARRIER ON BOTH SIDES / NO FOOTPATH"));
            list.Add(string.Format("------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Crash Barrier [wc]= {0} m", txt_Ana_wbase.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Height of Crash Barrier [hc]= {0} m", txt_Ana_Hpar.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("SIDE WALK/FOOTPATH"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Footpath including Kerb [wf]= {0} m", txt_Ana_wf_LHS.Text));
            list.Add(string.Format("Height of Footpath  [hf]= {0} m", txt_Ana_hf_LHS.Text));
            list.Add(string.Format("Width of Kerb [wk]= {0} m", txt_Ana_Wk.Text));
            list.Add(string.Format("Width of Outer Railing [wr]= {0} m", txt_Ana_wr.Text));
            list.Add(string.Format(""));

            #endregion Input Data




            #region Section Properties
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Angle & Angle Thickness"));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format("{0} {1} {2} X {3}", cmb_ana_nos_ang.Text, cmb_ana_ang_section_name.Text, cmb_ana_ang_section_code.Text, cmb_ana_ang_thk.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #region For End Span Section of Main Girder

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("For End Span Section of Main Girder"));
            list.Add(string.Format("-------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Web Plates"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Total Nos.[Nb]= {0} nos", txt_sec_end_Nb.Text));
            list.Add(string.Format("Web Breadth[Bw]= {0} mm", txt_sec_end_Bw.Text));
            list.Add(string.Format("Web Depth  [Dw]= {0} mm", txt_sec_end_Dw.Text));
            if (rbtn_sec_box.Checked)
                list.Add(string.Format("Spacing between two Webs    [S]= {0} mm", txt_sec_end_S.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Top Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bft]= {0} mm", txt_sec_end_Bft.Text));
            list.Add(string.Format("Depth [Dft]= {0} mm", txt_sec_end_Dft.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bfb]= {0} mm", txt_sec_end_Bfb.Text));
            list.Add(string.Format("Depth [Dfb]= {0} mm", txt_sec_end_Dfb.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
          

            #endregion For End Span Section of Main Girder

            #region For Penultimate Span Section of Main Girder (L/4)

            list.Add(string.Format("For Penultimate Span Section of Main Girder (L/4)"));
            list.Add(string.Format("--------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Web Plates"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Total Nos.[Nb]= {0} nos", txt_sec_L4_Nb.Text));
            list.Add(string.Format("Web Breadth[Bw]= {0} mm", txt_sec_L4_Bw.Text));
            list.Add(string.Format("Web Depth  [Dw]= {0} mm", txt_sec_L4_Dw.Text));
            if (rbtn_sec_box.Checked)
                list.Add(string.Format("Spacing between two Webs    [S]= {0} mm", txt_sec_L4_S.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Top Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bft]= {0} mm", txt_sec_L4_Bft.Text));
            list.Add(string.Format("Depth [Dft]= {0} mm", txt_sec_L4_Dft.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bfb]= {0} mm", txt_sec_L4_Bfb.Text));
            list.Add(string.Format("Depth [Dfb]= {0} mm", txt_sec_L4_Dfb.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
           
            list.Add(string.Format(""));

            #endregion For End Span Section of Main Girder

            #region For Mid Span Section of Main Girder (L/2)

            list.Add(string.Format("For Mid Span Section of Main Girder (L/2)"));
            list.Add(string.Format("-----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Web Plates"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Total Nos.[Nb]= {0} nos", txt_sec_L2_Nb.Text));
            list.Add(string.Format("Web Breadth[Bw]= {0} mm", txt_sec_L2_Bw.Text));
            list.Add(string.Format("Web Depth  [Dw]= {0} mm", txt_sec_L2_Dw.Text));
            if (rbtn_sec_box.Checked)
                list.Add(string.Format("Spacing between two Webs    [S]= {0} mm", txt_sec_L2_S.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Top Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bft]= {0} mm", txt_sec_L2_Bft.Text));
            list.Add(string.Format("Depth [Dft]= {0} mm", txt_sec_L2_Dft.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bfb]= {0} mm", txt_sec_L2_Bfb.Text));
            list.Add(string.Format("Depth [Dfb]= {0} mm", txt_sec_L2_Dfb.Text));
            list.Add(string.Format(""));
           
            list.Add(string.Format(""));

            #endregion For Mid Span Section of Main Girder (L/2)

            #region For Section of Cross Girder

            list.Add(string.Format("For Section of Cross Girder"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Web Plates"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Total Nos.[Nb]= {0} nos", txt_sec_cross_Nb.Text));
            list.Add(string.Format("Web Breadth[Bw]= {0} mm", txt_sec_cross_Bw.Text));
            list.Add(string.Format("Web Depth  [Dw]= {0} mm", txt_sec_cross_Dw.Text));
            if (rbtn_sec_box.Checked)
                list.Add(string.Format("Spacing between two Webs    [S]= {0} mm", txt_sec_cross_S.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Top Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bft]= {0} mm", txt_sec_cross_Bft.Text));
            list.Add(string.Format("Depth [Dft]= {0} mm", txt_sec_cross_Dft.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Flange"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("Breadth [Bfb]= {0} mm", txt_sec_cross_Bfb.Text));
            list.Add(string.Format("Depth [Dfb]= {0} mm", txt_sec_cross_Dfb.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
           

            #endregion For Section of Cross Girder
            #endregion Section Properties



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("MOVING LOAD / LIVE LOAD DATA"));
            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(long_ll.ToArray());
            list.Add(string.Format(""));

            for (int i = 0; i < ll_comb.Count; i++)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("LIVE LOAD COMBINATION {0} : {1}", (i + 1), ll_comb[i]));
                list.Add(string.Format("------------------------------------------------------------------------------"));
                list.AddRange(all_loads[i].ToArray());
                list.Add(string.Format(""));
            }

            File.WriteAllLines(FILE_BASIC_INPUT_DATA, list.ToArray());
            //System.Diagnostics.Process.Start(FILE_BASIC_INPUT_DATA);
        }


        string FILE_SUPPORT_REACTIONS { get { return Path.Combine(user_path, "Process\\SUPPORT_REACTIONS.TXT"); } }
        string FILE_SUMMARY_RESULTS { get { return Path.Combine(user_path, "Process\\SUMMARY_RESULTS.TXT"); } }
        string FILE_BASIC_INPUT_DATA { get { return Path.Combine(user_path, "Process\\Analysis_User_Data.TXT"); } }

        private void btn_process_deck_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_process_deck)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Deck Design.xlsx");
                AASHTO_Design.Process_Deck_Design(iApp, dgv_deck_input_data, Excel_File);
            }
            else if (btn == btn_process_steel_section)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Steel Section Design.xlsx");
                AASHTO_Design.Process_Steel_Section_Design(iApp, dgv_steel_girder_input_data, Excel_File);
            }
            else if (btn == btn_process_bolted_splice)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bolted Splice Design.xlsx");
                AASHTO_Design.Process_Bolted_Field_Splice_Design(iApp, dgv_bolted_field_splice_input_data, Excel_File);
            }
            else if (btn == btn_process_misc_steel)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Misc Steel Design.xlsx");
                AASHTO_Design.Process_Miscellaneous_Steel_Design(iApp, dgv_misc_steel_input_data, Excel_File);
            }
            else if (btn == btn_process_abutment)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
                AASHTO_Design.Process_Abutment_and_Wingwall_Design(iApp, dgv_abutment_input_data, Excel_File);
            }
            else if (btn == btn_process_pier)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
                AASHTO_Design.Process_Pier_Design(iApp, dgv_pier_input_data, Excel_File);
            }
            else if (btn == btn_process_Foundation)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pile Foundation Design.xlsx");
                AASHTO_Design.Process_Pile_Foundation_Design_Design(iApp, dgv_foundation_input_data, Excel_File);
            }
            else if (btn == btn_process_bearing)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
                AASHTO_Design.Process_Bearing_Design(iApp, dgv_bearing_input_data, Excel_File);
            }
        }
        public void DataChange()
        {
            try
            {
                DataChange(dgv_deck_input_data);
                DataChange(dgv_steel_girder_input_data);
                DataChange(dgv_bolted_field_splice_input_data);
                DataChange(dgv_misc_steel_input_data);
                DataChange(dgv_abutment_input_data);
                DataChange(dgv_pier_input_data);
                DataChange(dgv_foundation_input_data);
                DataChange(dgv_bearing_input_data);
            }
            catch (Exception ex) { }
        }
        public void DataChange(DataGridView dgv)
        {
            Set_Composite_Inputs();
            //DataGridView dgv = dgv_deck_input_data;

            if (dgv == dgv_deck_input_data)
            {
                #region For Deck Slab

                //1	Deck width:	Wdeck	46.875	ft
                dgv[2, 1].Value = Inputs.wdeck.ToString(); //Deck width:
                //2	Roadway width:	Wroadway	44	ft
                dgv[2, 2].Value = Inputs.wroadway.ToString(); //Roadway width:
                //3	Bridge length:	Ltotal	240	ft
                dgv[2, 3].Value = Inputs.LTotal.ToString(); // //Bridge length:
                //4	Skew Angle	Skew	0	degree
                dgv[2, 4].Value = Inputs.Skew.ToString(); //Skew Angle


                //9	Steel density	Ws	0.49	kcf
                //10	Concrete density	Wc	0.15	kcf
                //11	Parapet weight (each)	Wpar	0.53	K/ft
                //12	Future wearing surface	Wfws	0.14	kcf
                //13	Future wearing surface thickness	tfws	2.5	in


                //15	Girder spacing	S	9.75	ft
                var S = Inputs.S;
                dgv[2, 15].Value = S.ToString();

                //16	Number of girders	N	5	nos
                var N = Inputs.N;
                dgv[2, 16].Value = N.ToString();
                //17	Deck top cover	Covert	2.5	in
                var Covert = Inputs.Covert;
                dgv[2, 17].Value = Covert.ToString();
                //18	Deck bottom cover	Coverb	1	in
                var Coverb = Inputs.Coverb;
                dgv[2, 18].Value = Coverb.ToString();
                //19	Concrete density	Wc	0.15	kcf
                var Wc = Inputs.Wc;
                dgv[2, 18].Value = Wc.ToString();


                //20	Concrete 28day compressive strength	f'c	4	ksi
                var Fc = Inputs.fc;
                dgv[2, 20].Value = Fc.ToString();

                //24	Weight per foot:	Wpar	0.53	K/ft
                var Wpar = Inputs.Wpar;
                dgv[2, 24].Value = Wpar.ToString();
                //25	Width at base:	Wbase	1.4375	ft
                var Wbase = Inputs.wbase;
                dgv[2, 25].Value = Wbase.ToString();
                //27	Parapet height:	Hpar	3.5	ft
                var Hpar = Inputs.Hpar;
                dgv[2, 27].Value = Hpar.ToString();

                //33	Assume slab thicknesses	ts	8.5	in
                var ts = Inputs.tdeck;
                dgv[2, 33].Value = ts.ToString();
                //34	Assume overhang thicknesses	to	9	in
                //var _to = 9;
                //dgv[2, 34].Value = _to.ToString();


                #endregion For Deck Slab
            }
            else if (dgv == dgv_steel_girder_input_data)
            {
                #region For Steel Girder Design
                //1	Number of spans:	Nspans =	2	
                var Nspans = Inputs.Nspans;
                dgv[2, 1].Value = Nspans;

                //2	Span length:	Lspan =	120	ft
                dgv[2, 2].Value = Inputs.Lspan.ToString();

                //3	Skew angle:	Skew =	0	deg
                dgv[2, 3].Value = Inputs.Skew.ToString();

                //4	Number of girders:	Ngirders =	5	
                dgv[2, 4].Value = Inputs.N.ToString();
                //5	Girder spacing:	S =	9.75	ft
                dgv[2, 5].Value = Inputs.S.ToString();
                //6	Deck overhang:	Soverhang =	3.9375	ft
                var Soverhang = Inputs.Soverhang;
                dgv[2, 6].Value = Soverhang.ToString();

                #endregion For Steel Girder Design
            }
            else if (dgv == dgv_bolted_field_splice_input_data)
            {
                #region For bolted_field_splice Design
                //1	Yield Strength:	Fy =	50	ksi
                var Fy = Inputs.Fy;
                dgv[2, 1].Value = Fy;
                //2	Tensile Strength:	Fu =	65	ksi
                var Fu = Inputs.Fu;
                dgv[2, 2].Value = Fu;
                //3	Flange Yield Strength:	Fyf =	50	ksi
                var Fyf = 50;
                dgv[2, 3].Value = Fyf;

                //5	Web Thickness:	tw =	0.5	in
                var tw = 0.5;
                dgv[2, 5].Value = tw;
                //6	Web Depth:	D =	54	in
                var D = 54;
                dgv[2, 6].Value = D;
                //7	Top Flange Width:	bfltL =	14	in
                var bfltL = 14;
                dgv[2, 7].Value = bfltL;
                //8	Top Flange Thickness:	tfltL =	0.625	in
                var tfltL = 0.625;
                dgv[2, 8].Value = tfltL;
                //9	Bottom Flange Width:	bflbL =	14	in
                var bflbL = 14;
                dgv[2, 9].Value = bflbL;
                //10	Bottom Flange Thickness:	tflbL =	0.875	in
                var tflbL = 0.875;
                dgv[2, 10].Value = tflbL;

                //12	Web Thickness:	tw =	0.5	in
                dgv[2, 12].Value = tw;

                //13	Web Depth:	D =	54	in
                D = 54;
                dgv[2, 13].Value = D;
                //14	Top Flange Width:	bfltR =	14	in
                var bfltR = 14;
                dgv[2, 14].Value = bfltR;
                //15	Top Flange Thickness:	tfltR =	1.125	in
                var tfltR = 1.125;
                dgv[2, 15].Value = tfltR;
                //16	Bottom Flange Width:	bflbR =	14	in
                var bflbR = 14;
                dgv[2, 16].Value = bflbR;
                //17	Bottom Flange Thickness:	tflbR =	1.375	in
                var tflbR = 1.375;
                dgv[2, 17].Value = tflbR;

                #endregion For Miscelenious Steel Girder Design
            }

            else if (dgv == dgv_misc_steel_input_data)
            {
                #region For Miscelenious Steel Girder Design

                //3	Concrete 28day compressive strength	f'c	4	ksi
                var Fc = Inputs.fc;
                dgv[2, 3].Value = Fc;
                //4		b =	103	in
                //var b = 103;
                //dgv[2, 4].Value = b;
                //5	Minimum deck overhang thickness	ts =	8	in
                //var ts = 8;
                //dgv[2, 4].Value = ts;
                //6	Web yield strength:	Fyw =	50	ksi
                //var Fyw = 50;
                //dgv[2, 6].Value = Fyw;
                //7	Web Depth:	D =	54	in
                var D = 54.0;
                dgv[2, 7].Value = D;
                //8	Web Thickness:	tw =	0.5	in
                var tw = 0.5;
                dgv[2, 8].Value = tw;
                //9	Structural steel yield strength:	Fyt =	50	ksi
                var Fyt = 50;
                dgv[2, 9].Value = Fyt;
                //10		bt =	14	in
                var bt = 14;
                dgv[2, 10].Value = bt;

                //16	Girder spacing:	S =	9.75	ft
                var S = Inputs.S;
                dgv[2, 16].Value = S;
                //17	Girder depth:	D =	4.9375	ft
                //D = 4.9375;
                //dgv[2, 17].Value = D;


                #endregion For Miscelenious Steel Girder Design
            }
            else if (dgv == dgv_abutment_input_data)
            {
                #region abutment Design
                //2	Concrete density:	Wc =	0.15	kcf
                var Wc = Inputs.Wc;
                dgv[2, 2].Value = Wc;
                //3	Concrete 28-day compressive strength:	f'c =	4	ksi
                var Fc = Inputs.fc;
                dgv[2, 3].Value = Fc;
                //4	Reinforcement strength:	fy =	60	ksi
                var fy = Inputs.fys;
                dgv[2, 4].Value = fy;

                //11	Girder spacing:	S =	9.75	ft
                var S = Inputs.S;
                dgv[2, 11].Value = S;
                //12	Number of girders:	N =	5	nos
                var N = Inputs.N;
                dgv[2, 12].Value = S;
                //13	Span length:	Lspan =	120	ft
                var Lspan = Inputs.Lspan;
                dgv[2, 13].Value = Lspan;
                //14	Parapet height:	Hpar =	3.5	ft
                var Hpar = Inputs.Hpar;
                dgv[2, 14].Value = Hpar;
                //15	Parapet weight (each):	Wpar =	0.53	K/ft
                var Wpar = Inputs.Wpar;
                dgv[2, 15].Value = Wpar;
                //16	Out-to-out deck width:	Wdeck =	46.875	ft
                var Wdeck = Inputs.wdeck;
                dgv[2, 16].Value = Wdeck;

                //21	Abutment length:	Labut =	46.875	ft
                var Labut = Inputs.wdeck;
                dgv[2, 21].Value = Labut;


                #endregion For abutment Design
            }
            else if (dgv == dgv_pier_input_data)
            {
                #region pier Design
                //2	Concrete density:	Wc =	0.15	kcf

                var Wc = Inputs.Wc;
                dgv[2, 2].Value = Wc;
                //3	Concrete 28-day	f'c =	4	ksi
                var Fc = Inputs.fc;
                dgv[2, 3].Value = Fc;
                //5	Reinforcement	fy =	60	ksi
                var fy = Inputs.fys;
                dgv[2, 5].Value = fy;

                //13	Girder spacing:	S =	9.75	ft
                var S = Inputs.S;
                dgv[2, 13].Value = S;
                //14	Number of girders:	N =	5	
                var N = Inputs.N;
                dgv[2, 14].Value = N;
                //15	Deck overhang:	DOH =	3.9375	ft
                var DOH = Inputs.Soverhang;
                dgv[2, 15].Value = DOH;
                //16	Span length:	Lspan =	120	ft
                var Lspan = Inputs.Lspan;
                dgv[2, 16].Value = Lspan;
                //17	Parapet height:	Hpar =	3.5	ft
                var Hpar = Inputs.Hpar;
                dgv[2, 17].Value = Hpar;
                //18	Deck overhang thickness:	to =	9	ft
                //var _to = 9;
                //dgv[2, 18].Value = _to;
                //19	Haunch thickness:	Hhnch =	3.5	in
                //var Hhnch = 3.5;
                //dgv[2, 14].Value = Hhnch;
                //20	Web depth:	Do =	66	in
                //var _Do = 66;
                //dgv[2, 14].Value = _Do;



                #endregion For abutment Design
            }
            else if (dgv == dgv_foundation_input_data)
            {

                #region pier Design
                //5	Maximum possible length of footing	L =	46.875	ft
                dgv[2, 5].Value = Inputs.Lspan.ToString();
                #endregion For abutment Design

            }
            else if (dgv == dgv_bearing_input_data)
            {

                #region pier Design
                //5	Maximum possible length of footing	L =	46.875	ft
                //var L = 46.875;
                //dgv[2, 5].Value = Wc;
                #endregion For abutment Design

            }
        }
        Composite_Inputs Inputs = new Composite_Inputs();
        public void Set_Composite_Inputs()
        {

            Inputs.txt_LSpan = txt_Ana_L;
            Inputs.txt_LTotal = txt_multiSpan;
            Inputs.txt_Coverb = txt_Ana_coverb;
            Inputs.txt_Covert = txt_Ana_covert;
            Inputs.txt_Ec = txt_Ana_Ec;
            Inputs.txt_Es = txt_Ana_Es;
            Inputs.txt_fc = txt_Ana_fc;
            Inputs.txt_Fu = txt_Ana_Fu;
            Inputs.txt_Fy = txt_Ana_Fy;
            Inputs.txt_fys = txt_Ana_fys;
            Inputs.txt_Hpar = txt_Ana_Hpar;
            Inputs.txt_LSpan = txt_Ana_L;
            Inputs.cmb_N = cmb_Ana_NMG;
            Inputs.txt_n = txt_Ana_n;
            Inputs.txt_S = txt_Ana_S;
            Inputs.txt_Skew = txt_Ana_Skew;
            Inputs.txt_Soverhang = txt_Ana_Soverhang;
            Inputs.txt_tdeck = txt_Ana_tdeck;
            Inputs.txt_tfws = txt_Ana_tfws;
            Inputs.txt_wbase = txt_Ana_wbase;
            Inputs.txt_wdeck = txt_Ana_wdeck;
            Inputs.txt_wfws = txt_Ana_Wfws;
            Inputs.txt_Wmisc = txt_Ana_Wmisc;
            Inputs.txt_Wpar = txt_Ana_Wpar;
            Inputs.txt_wroadway = txt_Ana_wroadway;
            Inputs.txt_Ws = txt_Ana_Ws;
            Inputs.txt_Wc = txt_Ana_Wc;


            Inputs.wbase = (Inputs.wdeck - Inputs.wroadway) / 2;
            Inputs.txt_wbase.ForeColor = Color.Blue;



            Inputs.S = (Inputs.wdeck - 2 * Inputs.Soverhang) / (Inputs.N - 1);
            Inputs.txt_S.ForeColor = Color.Blue;


            Inputs.Ec = 33000* (Math.Pow(Inputs.Wc, 1.5) * Math.Sqrt(Inputs.fc));
            Inputs.txt_Ec.ForeColor = Color.Blue;

            
            Inputs.n = Math.Round((Inputs.Es/Inputs.Ec), 0);
            Inputs.txt_n.ForeColor = Color.Blue;

            
        }

        private void btn_deck_open_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string Excel_File = "";

            if (btn == btn_deck_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Deck Design.xlsx");
            }
            else if (btn == btn_steel_section_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Steel Section Design.xlsx");
            }
            else if (btn == btn_bolted_splice_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bolted Splice Design.xlsx");
            }
            else if (btn == btn_misc_steel_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Misc Steel Design.xlsx");
            }
            else if (btn == btn_abutment_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
            }
            else if (btn == btn_pier_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
            }
            else if (btn == btn_foundation_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pile Foundation Design.xlsx");

            }
            else if (btn == btn_bearing_open)
            {
                 Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
            }
            if (File.Exists(Excel_File)) iApp.OpenExcelFile(Excel_File, "2011ap");

        }

        private void btn_deck_ws_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void btn_irc_view_moving_load_Click(object sender, EventArgs e)
        {

            if (!Check_Project_Folder()) return;
            Analysis_Initialize_InputData();

            string ll_file = "";
            try
            {

                if (IsCreate_Data)
                {
                    if (Path.GetFileName(user_path) != Project_Name) Create_Project();
                }
                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);


                Analysis_Initialize_InputData();

                Write_All_Data(false);


                string usp = Path.Combine(user_path, "ANALYSIS PROCESS");

                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);

                string inp_file = Path.Combine(usp, "INPUT_DATA.TXT");

                //Calculate_Load_Computation();
                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;
                if (iApp.DesignStandard == eDesignStandard.IndianStandard || iApp.DesignStandard == eDesignStandard.LRFDStandard)
                {
                    LONG_GIRDER_LL_TXT();
                    if (Curve_Radius > 0)
                    {
                        Bridge_Analysis.CreateData_Straight();

                        #region Chiranjit [2014 09 08] Indian Standard


                        Bridge_Analysis.Steel_Section = Comp_sections;


                        Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                            Bridge_Analysis.Inner_Girders_as_String,
                            Bridge_Analysis.joints_list_for_load);

                        Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.Straight_Deck_DL_File);
                        Ana_Write_Deck_Load_Data(Bridge_Analysis.Straight_Deck_DL_File, false, true);

                        Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.Straight_Girder_DL_File);
                        Ana_Write_Girder_Load_Data(Bridge_Analysis.Straight_Girder_DL_File, false, true);


                        Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Straight_LL_File);
                        Ana_Write_Deck_Load_Data(Bridge_Analysis.Straight_LL_File, true, false);


                        Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Straight_TL_File);
                        Ana_Write_Deck_Load_Data(Bridge_Analysis.Straight_TL_File, true, true);

                        #region Chiranjit [2014 10 2]

                        if (long_ll.Count > 0)
                        {
                            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(long_ll);
                        }
                        //for (int i = 0; i < all_loads.Count; i++)
                        //{
                        //    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), long_ll);
                        //    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), true, false, i + 1);
                        //}

                        #endregion Chiranjit [2014 10 2]

                        #endregion

                        Bridge_Analysis.CreateData_AASHTO();
                    }
                    else
                    {
                        Bridge_Analysis.Skew_Angle = Skew;
                        Bridge_Analysis.CreateData_Straight();
                    }

                    Bridge_Analysis.WriteData(inp_file);
                    Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);

                    Bridge_Analysis.Steel_Section = Comp_sections;

                    Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                        Bridge_Analysis.Inner_Girders_as_String,
                        Bridge_Analysis.joints_list_for_load);


                    Bridge_Analysis.WriteData_Total_Analysis(inp_file);
                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);


                    //Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                    //Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File);
                    //Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File);


                    //Ana_Write_Deck_Load_Data(Bridge_Analysis.Input_File, true, true);
                    //Ana_Write_Deck_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                    //Ana_Write_Deck_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Deck_Input_File, false, true);
                    //Ana_Write_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Girder_Input_File, false, true);
                    //Ana_Write_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                    //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 2);


                    #region Chiranjit [2014 10 2]


                    //for (int i = 0; i < all_loads.Count; i++)
                    //{
                    //    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1), long_ll);
                    //    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, false), true, false, i + 1);
                    //}

                    #endregion Chiranjit [2014 10 2]



                    ll_file = Bridge_Analysis.Get_Live_Load_Analysis_Input_File(cmb_irc_view_moving_load.SelectedIndex + 1, false);
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(cmb_irc_view_moving_load.SelectedIndex + 1), long_ll);
                    Ana_Write_Long_Girder_Load_Data(ll_file, true, false, cmb_irc_view_moving_load.SelectedIndex + 1);
                    //iApp.View_MovingLoad(ll_file);
                    iApp.View_MovingLoad(ll_file, Curve_Radius, MyList.StringToDouble(txt_irc_vehicle_gap.Text));


                    Button_Enable_Disable();
                }

               

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            Button_Enable_Disable();
            Write_All_Data(false);
        }
    }

    public class Composite_AASHTO_Analysis
    {
        IApplication iApp;

        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Structure = null;

        public BridgeMemberAnalysis DL_Analysis_Deck = null;
        public BridgeMemberAnalysis DL_Analysis_Girder = null;

        public BridgeMemberAnalysis LL_Analysis = null;

        //public BridgeMemberAnalysis Structure = null;

        public List<BridgeMemberAnalysis> All_Analysis = null;

        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;


        public string _DeckSlab { get; set; }
        public string _Inner_Girder_Mid { get; set; }
        public string _Inner_Girder_Support { get; set; }
        public string _Outer_Girder_Mid { get; set; }
        public string _Outer_Girder_Support { get; set; }
        public string _Cross_Girder_Inter { get; set; }
        public string _Cross_Girder_End { get; set; }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        public CompositeSection_AASHTO Steel_Section { get; set; }

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;


        //Chiranjit [2013 06 06]
        string list_envelop_outer = "";
        string list_envelop_inner = "";

        string input_file, working_folder, user_path;
        public Composite_AASHTO_Analysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = working_folder = "";
            //Total_Rows = 0; Total_Columns = 0;
            NMG = 7;
            Total_Columns = 11;
            Total_Rows = 11;

            All_Analysis = new List<BridgeMemberAnalysis>();
        }

        #region Properties

        public double Length { get; set; }

        public List<double> Spans { get; set; }

        public double Total_Length
        {
            get
            {

                if (Spans.Count > 0) return MyList.Get_Array_Sum(Spans);
                return Length;
            }
        }

        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }

        public int Total_Rows { get; set; }
        public int Total_Columns { get; set; }

        public double Skew_Angle { get; set; }

        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }

        public double Spacing_Long_Girder
        {
            get
            {
                //return Math.Abs(MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / (NMG - 1)).ToString("0.000"), 0.0));
                //Chiranjit [2013 05 05]
                return Math.Abs(MyList.StringToDouble(((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (NMG - 1)).ToString("0.000"), 0.0));
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //return MyList.StringToDouble(txt_cross_girder_spacing.Text, 0.0);
                //Chiranjit [2012 10 11]
                //return MyList.StringToDouble(((Length) / 16.0).ToString("0.000"), 0.0);
                //return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);

                //Chiranjit [2013 05 05]
                double val = 0.0;
                if (NCG % 2 == 0.0)
                    val = (Length - 2 * Effective_Depth) / (NCG - 1);
                else if (NCG > 6)
                    val = (Length / 8.0);
                else
                    val = (Length / 4.0);




                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }


        #endregion Properties
        //Chiranjit [2012 07 13]
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(working_folder)) return "";
                return Path.Combine(working_folder, "ASTRA_DATA_FILE.TXT");

            }
        }

        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(working_folder, "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(working_folder, "ANALYSIS_REP.TXT");
            }
        }
        //public string Input_File
        //{
        //    get
        //    {
        //        return input_file;
        //    }
        //    set
        //    {
        //        input_file = value;
        //        working_folder = Path.GetDirectoryName(input_file);
        //        user_path = working_folder;
        //    }
        //}

        #region Analysis Input File
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                input_file = value;
                //if (File.Exists(value))
                user_path = Path.GetDirectoryName(input_file);
                working_folder = user_path;
            }
        }
        //Chiranjit [2012 05 27]
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "TOTAL DL + LL ANALYSIS");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "Total_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string TempAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "TempAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "Temp_Input_File.txt");
                }
                return "";
            }
        }
        public string Orthotropic_Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "ORTHOTROPIC ANALYSIS");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "Orthotropic_Input_File.txt");
                }
                return "";
            }
        }
        public string Straight_Deck_DL_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "TempAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    pd = Path.Combine(pd, "Deck_DLAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);


                    return Path.Combine(pd, "DeckDLAnalysis.txt");
                }
                return "";
            }
        }
        public string Straight_Girder_DL_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "TempAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    pd = Path.Combine(pd, "Girder_DLAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);


                    return Path.Combine(pd, "GirderDLAnalysis.txt");
                }
                return "";
            }
        }

        public string Straight_LL_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "TempAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    pd = Path.Combine(pd, "LLAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);


                    return Path.Combine(pd, "LLAnalysis.txt");
                }
                return "";
            }
        }

        public string Straight_TL_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "TempAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    pd = Path.Combine(pd, "TLAnalysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);


                    return Path.Combine(pd, "TLAnalysis.txt");
                }
                return "";
            }
        }
        //Chiranjit [2012 05 27]
        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(working_folder))
                {
                    string pd = Path.Combine(working_folder, "LIVE LOAD ANALYSIS");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        //Chiranjit [2014 09 08] for Vritish Standard
        public string Get_Live_Load_Analysis_Input_File(int analysis_no)
        {
            if (analysis_no < 0) return "";
            if (Directory.Exists(working_folder))
            {
                string pd = Path.Combine(working_folder, "LL ANALYSIS LOAD " + analysis_no);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_LOAD_" + analysis_no + "_INPUT_FILE.txt");
            }
            return "";
        }
        public string Get_Live_Load_Analysis_Input_File(int analysis_no, bool IsStageFile)
        {
            if (IsStageFile)
            {
                string pd = Path.Combine(working_folder, "TempAnalysis");
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                pd = Path.Combine(pd, "LL ANALYSIS LOAD " + analysis_no);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_LOAD_" + analysis_no + "_INPUT_FILE.txt");
            }
            else
            {
                return Get_Live_Load_Analysis_Input_File(analysis_no);
            }

            return "";
        }

        //Chiranjit [2012 05 27]
        public string DeadLoadAnalysis_Deck_Input_File
        {
            get
            {
                if (Directory.Exists(working_folder))
                {
                    string pd = Path.Combine(working_folder, "Dead Load Deck Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Deck_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string DeadLoadAnalysis_Girder_Input_File
        {
            get
            {
                if (Directory.Exists(working_folder))
                {
                    string pd = Path.Combine(working_folder, "Dead Load Girder Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Girder_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string Total_Analysis_Report
        {
            get
            {
                if (File.Exists(TotalAnalysis_Input_File))
                    return Path.Combine(Path.GetDirectoryName(TotalAnalysis_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }
        public string LiveLoad_Analysis_Report
        {
            get
            {
                if (File.Exists(LiveLoadAnalysis_Input_File))
                    return Path.Combine(Path.GetDirectoryName(LiveLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }
        public string DeadLoad_Deck_Analysis_Report
        {
            get
            {
                if (File.Exists(DeadLoadAnalysis_Deck_Input_File))
                    return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Deck_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }
        public string DeadLoad_Girder_Analysis_Report
        {
            get
            {
                if (File.Exists(DeadLoadAnalysis_Girder_Input_File))
                    return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Girder_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }

        #endregion Analysis Input File

        public int NoOfInsideJoints
        {
            get
            {
                //return MyList.StringToInt(txt_cd_total_joints.Text, 0);
                return 1;
            }
        }
       
        public double NMG { get; set; }
        public double NCG { get; set; }

        public double Ds { get; set; }


        public double Mid_Span_Length { get { return Length / 2.0; } }
        public double Penultimate_Span_Length { get { return Length / 4.0; } }


        #region Chiranjit [2013 05 06]
        //Chiranjit [2013 05 06]
        public List<string> joints_list_for_load = new List<string>();

        //Chiranjit [2013 05 06]
        public string support_left_joints = "";
        public string support_right_joints = "";


        public string support_inner_joints = "";

        public string L0_Girders_as_String { get; set; }
        public string L1_Girders_as_String { get; set; }
        public string L2_Girders_as_String { get; set; }
        public string L3_Girders_as_String { get; set; }
        public string L4_Girders_as_String { get; set; }
        public string L5_Girders_as_String { get; set; }
        public string L6_Girders_as_String { get; set; }
        public string L7_Girders_as_String { get; set; }
        public string L8_Girders_as_String { get; set; }
        public string L9_Girders_as_String { get; set; }




        public string Inner_Girders_as_String { get; set; }
        public string Outer_Girders_as_String { get; set; }

        public string Cross_Girders_as_String { get; set; }


        public List<int> _L0_inn_joints = new List<int>();
        public List<int> _L1_inn_joints = new List<int>();
        public List<int> _L2_inn_joints = new List<int>();
        public List<int> _L3_inn_joints = new List<int>();
        public List<int> _L4_inn_joints = new List<int>();
        public List<int> _L5_inn_joints = new List<int>();
        public List<int> _L6_inn_joints = new List<int>();
        public List<int> _L7_inn_joints = new List<int>();
        public List<int> _L8_inn_joints = new List<int>();
        public List<int> _L9_inn_joints = new List<int>();
        public List<int> _L10_inn_joints = new List<int>();

        public List<int> _L0_out_joints = new List<int>();
        public List<int> _L1_out_joints = new List<int>();
        public List<int> _L2_out_joints = new List<int>();
        public List<int> _L3_out_joints = new List<int>();
        public List<int> _L4_out_joints = new List<int>();
        public List<int> _L5_out_joints = new List<int>();
        public List<int> _L6_out_joints = new List<int>();
        public List<int> _L7_out_joints = new List<int>();
        public List<int> _L8_out_joints = new List<int>();
        public List<int> _L9_out_joints = new List<int>();
        public List<int> _L10_out_joints = new List<int>();


        List<int> _HA_Joints = new List<int>();

        void Set_Inner_Outer_Cross_Girders()
        {

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();











            for (int r = 0; r < _Rows; r++)
            {
                for (int c = 0; c < _Columns - 1; c++)
                {
                    if (r < 2 || r > _Rows - 3)
                    {
                        Outer_Girder.Add(Long_Girder_Members_Array[r, c].MemberNo);
                    }
                    else
                    {
                        Inner_Girder.Add(Long_Girder_Members_Array[r, c].MemberNo);
                    }
                }
            }



            for (int r = 0; r < _Rows - 1; r++)
            {
                for (int c = 0; c < _Columns; c++)
                {
                    Cross_Girder.Add(Cross_Girder_Members_Array[r, c].MemberNo);
                }
            }




            //for (int i = 0; i < MemColls.Count; i++)
            //{
            //    if ((MemColls[i].StartNode.X.ToString("0.000") == MemColls[i].EndNode.X.ToString("0.000")))
            //    {
            //        Cross_Girder.Add(MemColls[i].MemberNo);
            //    }
            //    else if (HA_distances.Contains(MemColls[i].StartNode.Z))
            //    {
            //        //Outer_Girder.Add(MemColls[i].MemberNo);
            //    }
            //    else if ((MemColls[i].StartNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000") &&
            //        MemColls[i].EndNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000")) ||
            //        (MemColls[i].StartNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000") &&
            //        MemColls[i].EndNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000")))
            //    {
            //        Outer_Girder.Add(MemColls[i].MemberNo);
            //    }
            //    else if ((MemColls[i].StartNode.Z == 0.0 &&
            //        MemColls[i].EndNode.Z == 0.0) ||
            //        (MemColls[i].StartNode.Z == WidthBridge) &&
            //        (MemColls[i].EndNode.Z == WidthBridge))
            //    {
            //        Outer_Girder.Add(MemColls[i].MemberNo);
            //    }
            //    else
            //    {
            //        Inner_Girder.Add(MemColls[i].MemberNo);
            //    }
            //}
            Inner_Girder.Sort();
            Outer_Girder.Sort();
            Cross_Girder.Sort();




            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            Inner_Girders_as_String = MyList.Get_Array_Text(Inner_Girder);
            Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);


            //Outer_Girders_as_String = string.Format("{0} TO {1} {2} TO {3}",
            //    Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo,
            //    Long_Girder_Members_Array[5, 0].MemberNo, Long_Girder_Members_Array[5, _Columns - 2].MemberNo
            //    );


            //Inner_Girders_as_String = string.Format("{0} TO {1}",
            //    Long_Girder_Members_Array[1, 0].MemberNo, Long_Girder_Members_Array[4, _Columns - 2].MemberNo
            //    );
        }

        void Set_Girders()
        {

            //List<int> L2_Girders = new List<int>();
            //List<int> L4_Girders = new List<int>();
            //List<int> Deff_Girders = new List<int>();




            List<int> L0_Girders = new List<int>();
            List<int> L1_Girders = new List<int>();
            List<int> L2_Girders = new List<int>();
            List<int> L3_Girders = new List<int>();
            List<int> L4_Girders = new List<int>();
            List<int> L5_Girders = new List<int>();
            List<int> L6_Girders = new List<int>();
            List<int> L7_Girders = new List<int>();
            List<int> L8_Girders = new List<int>();
            List<int> L9_Girders = new List<int>();
            List<int> L10_Girders = new List<int>();












            List<int> Cross_Girder = new List<int>();

            List<int> HA_Members = new List<int>();



            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.X.ToString("0.000") == MemColls[i].EndNode.X.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    if (_L0_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L0_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L0_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L1_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L1_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L1_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L2_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L2_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L2_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L3_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L3_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L3_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L4_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L4_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L4_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L5_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L5_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L5_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L6_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L6_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L6_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L7_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L7_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L7_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L8_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L8_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L8_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L9_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L9_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L9_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L10_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L10_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L10_Girders.Add(MemColls[i].MemberNo);
                    }
                }
            }
            L0_Girders.Sort();
            L1_Girders.Sort();
            L2_Girders.Sort();
            L3_Girders.Sort();
            L4_Girders.Sort();
            L5_Girders.Sort();
            L6_Girders.Sort();
            L7_Girders.Sort();
            L8_Girders.Sort();
            L9_Girders.Sort();
            L10_Girders.Sort();


            Cross_Girder.Sort();



            //HA_Loading_Members
            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            L0_Girders_as_String = MyList.Get_Array_Text(L0_Girders);
            L1_Girders_as_String = MyList.Get_Array_Text(L1_Girders);
            L2_Girders_as_String = MyList.Get_Array_Text(L2_Girders);
            L3_Girders_as_String = MyList.Get_Array_Text(L3_Girders);
            L4_Girders_as_String = MyList.Get_Array_Text(L4_Girders);
            L5_Girders_as_String = MyList.Get_Array_Text(L5_Girders);
            L6_Girders_as_String = MyList.Get_Array_Text(L6_Girders);
            L7_Girders_as_String = MyList.Get_Array_Text(L7_Girders);
            L8_Girders_as_String = MyList.Get_Array_Text(L8_Girders);
            L9_Girders_as_String = MyList.Get_Array_Text(L9_Girders);


            //Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);
            Set_Inner_Outer_Cross_Girders();
        }

        void Set_L2_L4_Deff_Girders()
        {
            double L = Length;
            double W = WidthBridge;
            double val = L / 2;
            int i = 0;

            _L0_inn_joints.Clear();
            _L1_inn_joints.Clear();
            _L2_inn_joints.Clear();
            _L3_inn_joints.Clear();
            _L4_inn_joints.Clear();
            _L5_inn_joints.Clear();
            _L6_inn_joints.Clear();
            _L7_inn_joints.Clear();
            _L8_inn_joints.Clear();
            _L9_inn_joints.Clear();
            _L10_inn_joints.Clear();

            _L0_out_joints.Clear();
            _L1_out_joints.Clear();
            _L2_out_joints.Clear();
            _L3_out_joints.Clear();
            _L4_out_joints.Clear();
            _L5_out_joints.Clear();
            _L6_out_joints.Clear();
            _L7_out_joints.Clear();
            _L8_out_joints.Clear();
            _L9_out_joints.Clear();
            _L10_out_joints.Clear();







            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < Joints.Count; i++)
            {
                if (_X_joints.Contains(Joints[i].X) == false)
                    _X_joints.Add(Joints[i].X);
                if (_Z_joints.Contains(Joints[i].Z) == false)
                    _Z_joints.Add(Joints[i].Z);
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Effective_Depth;


            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            iApp.SetProgressValue(20, 100);
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {

                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < Joints.Count; i++)
                {
                    //if (_X_joints.Contains(Joints[i].X) == false) _X_joints.Add(Joints[i].X);
                    //if (_Z_joints.Contains(Joints[i].Z) == false) _Z_joints.Add(Joints[i].Z);

                    if (_Z_joints[zc] == Joints[i].Z)
                    {
                        if (x_min > Joints[i].X)
                            x_min = Joints[i].X;
                        if (x_max < Joints[i].X)
                            x_max = Joints[i].X;
                    }

                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }
            //HA_distances
            val = Effective_Depth;

            double cant_wi_left = Width_LeftCantilever;
            double cant_wi_right = Width_RightCantilever;


            MyList.Array_Format_With(ref _X_joints, "f3");

            for (i = 0; i < Joints.Count; i++)
            {
                try
                {

                    if (Skew_Angle > 0)
                    {
                        x_min = _X_min[_Z_joints.IndexOf(Joints[i].Z)];

                        if ((Joints[i].X.ToString("0.0") == ((L * 0.0) + x_min).ToString("0.0")))
                        {
                            _L0_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.1) + x_min).ToString("0.0")))
                        {
                            _L1_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.2) + x_min).ToString("0.0")))
                        {
                            _L2_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.3) + x_min).ToString("0.0")))
                        {
                            _L3_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.4) + x_min).ToString("0.0")))
                        {
                            _L4_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.5) + x_min).ToString("0.0")))
                        {
                            _L5_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.6) + x_min).ToString("0.0")))
                        {
                            _L6_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.7) + x_min).ToString("0.0")))
                        {
                            _L7_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.8) + x_min).ToString("0.0")))
                        {
                            _L8_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 0.9) + x_min).ToString("0.0")))
                        {
                            _L9_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((L * 1) + x_min).ToString("0.0")))
                        {
                            _L10_inn_joints.Add(Joints[i].NodeNo);
                        }
                    }
                    else
                    {
                        x_min = 0.0;
                        if ((Joints[i].X.ToString("0.0") == ((_X_joints[0]) + x_min).ToString("0.0")))
                        {
                            _L0_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[1]) + x_min).ToString("0.0")))
                        {
                            _L1_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[2]) + x_min).ToString("0.0")))
                        {
                            _L2_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[3]) + x_min).ToString("0.0")))
                        {
                            _L3_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[4]) + x_min).ToString("0.0")))
                        {
                            _L4_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[5]) + x_min).ToString("0.0")))
                        {
                            _L5_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[6]) + x_min).ToString("0.0")))
                        {
                            _L6_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[7]) + x_min).ToString("0.0")))
                        {
                            _L7_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[8]) + x_min).ToString("0.0")))
                        {
                            _L8_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[9]) + x_min).ToString("0.0")))
                        {
                            _L9_inn_joints.Add(Joints[i].NodeNo);
                        }
                        else if ((Joints[i].X.ToString("0.0") == ((_X_joints[10]) + x_min).ToString("0.0")))
                        {
                            _L10_inn_joints.Add(Joints[i].NodeNo);
                        }
                    }
                }
                catch (Exception ex) { }
            }

            List<int> temp_joints = new List<int>();
            var inn_jnts = _L0_inn_joints;

            #region L0
            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L0_out_joints.Clear();
            _L0_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion


            #region L1
            inn_jnts = _L1_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L1_out_joints.Clear();
            _L1_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion


            #region L2
            inn_jnts = _L2_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L2_out_joints.Clear();
            _L2_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion

            #region L3
            inn_jnts = _L3_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L3_out_joints.Clear();
            _L3_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion

            #region L4
            inn_jnts = _L4_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L4_out_joints.Clear();
            _L4_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion

            #region L5
            inn_jnts = _L5_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L5_out_joints.Clear();
            _L5_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion

            #region L6
            inn_jnts = _L6_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L6_out_joints.Clear();
            _L6_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion

            #region L7
            inn_jnts = _L7_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L7_out_joints.Clear();
            _L7_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion


            #region L8
            inn_jnts = _L8_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L8_out_joints.Clear();
            _L8_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion


            #region L9
            inn_jnts = _L9_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L9_out_joints.Clear();
            _L9_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion

            #region L10
            inn_jnts = _L10_inn_joints;
            temp_joints.Clear();

            if (inn_jnts.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[0]);
                    inn_jnts.RemoveAt(0);
                }

                temp_joints.Add(inn_jnts[0]);
                inn_jnts.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                    inn_jnts.RemoveAt(inn_jnts.Count - 1);
                }
                temp_joints.Add(inn_jnts[inn_jnts.Count - 1]);
                inn_jnts.RemoveAt(inn_jnts.Count - 1);
            }
            _L10_out_joints.Clear();
            _L10_out_joints.AddRange(temp_joints.ToArray());

            temp_joints.Clear();

            #endregion



            _L0_inn_joints.Sort();
            _L1_inn_joints.Sort();
            _L2_inn_joints.Sort();
            _L3_inn_joints.Sort();
            _L4_inn_joints.Sort();
            _L5_inn_joints.Sort();
            _L6_inn_joints.Sort();
            _L7_inn_joints.Sort();
            _L8_inn_joints.Sort();
            _L9_inn_joints.Sort();
            _L10_inn_joints.Sort();

            _L0_out_joints.Sort();
            _L1_out_joints.Sort();
            _L2_out_joints.Sort();
            _L3_out_joints.Sort();
            _L4_out_joints.Sort();
            _L5_out_joints.Sort();
            _L6_out_joints.Sort();
            _L7_out_joints.Sort();
            _L8_out_joints.Sort();
            _L9_out_joints.Sort();
            _L10_out_joints.Sort();




            Set_Girders();
        }

        #endregion Chiranjit [2013 05 06]


        public void CreateData()
        {
            CreateData_AASHTO();
        }

        public double Radius = 50;

        public void CreateData_Straight()
        {

            double x_incr, z_incr;

            //x_incr = (Length / (Total_Columns - 1));
            //z_incr = (WidthBridge / (Total_Rows - 1));
            //NMG = 7;
            x_incr = Spacing_Cross_Girder;
            z_incr = Spacing_Long_Girder;

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = 12.1;
            double val2 = val1 * skew_length;



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



            int i = 0;
            bool flag = true;

            last_x = 0.0;
            if (Spans.Count > 1)
            {
                Hashtable x_tbl = new Hashtable();

                List<double> x_tmp = new List<double>();
                double len = 0.0;
                for (int j = 0; j < Spans.Count; j++)
                {
                    Length = Spans[j];
                    list_x = new List<double>();
                    x_tbl.Add(j, list_x);


                    #region Create X Coordinate Data

                    list_x.Clear();
                    list_x.Add(0.0 * Length);
                    list_x.Add(0.1 * Length);
                    list_x.Add(0.2 * Length);
                    list_x.Add(0.3 * Length);
                    list_x.Add(0.4 * Length);
                    list_x.Add(0.5 * Length);
                    list_x.Add(0.6 * Length);
                    list_x.Add(0.7 * Length);
                    list_x.Add(0.8 * Length);
                    list_x.Add(0.9 * Length);
                    list_x.Add(Length);


                    list_x.Sort();



                    list_z.Add(0);

                    if (Width_LeftCantilever != 0.0)
                    {
                        last_z = Width_LeftCantilever;
                        last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                        list_z.Add(last_z);
                    }

                    if (Width_RightCantilever != 0.0)
                    {
                        last_z = WidthBridge - Width_RightCantilever;
                        last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                        list_z.Add(last_z);
                    }


                    last_z = WidthBridge;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);

                    last_z = Width_LeftCantilever + z_incr;
                    do
                    {
                        flag = false;
                        for (i = 0; i < list_z.Count; i++)
                        {
                            if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                            {
                                flag = true; break;
                            }
                        }

                        if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                            list_z.Add(last_z);
                        last_z += z_incr;
                        last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

                        if (z_incr == 0.0) break;

                    } while (last_z <= WidthBridge);

                    list_z.Sort();
                    #endregion Chiranjit [2011 09 23] Correct Create Data

                    if (j > 0)
                    {
                        foreach (var item in list_x)
                        {
                            if (!x_tmp.Contains(len + item))
                                x_tmp.Add(len + item);
                        }
                        len += Spans[j];
                    }
                    else
                    {
                        len = Spans[j];
                        foreach (var item in list_x)
                        {
                            x_tmp.Add(item);
                        }
                    }
                }


                list_x.Clear();
                list_x.AddRange(x_tmp.ToArray());

                #region Create Z-Coordinate Data
                list_z.Clear();

                list_z.Add(0.0);


                if (Width_LeftCantilever != 0.0)
                {
                    last_z = Width_LeftCantilever;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);
                }

                if (Width_RightCantilever != 0.0)
                {
                    last_z = WidthBridge - Width_RightCantilever;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);
                }


                last_z = WidthBridge;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);

                last_z = Width_LeftCantilever + z_incr;
                do
                {
                    flag = false;
                    for (i = 0; i < list_z.Count; i++)
                    {
                        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                        {
                            flag = true; break;
                        }
                    }

                    if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                    {
                        if (!list_z.Contains(last_z))
                            list_z.Add(last_z);
                    }
                    last_z += z_incr;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

                    if (z_incr == 0.0) break;

                } while (last_z <= WidthBridge);

                list_z.Sort();
                #endregion Create Z-Coordinate Data
            }
            else
            {
                #region Chiranjit [2011 09 23] Correct Create Data

                list_x.Clear();
                list_x.Add(0.0 * Length);
                list_x.Add(0.1 * Length);
                list_x.Add(0.2 * Length);
                list_x.Add(0.3 * Length);
                list_x.Add(0.4 * Length);
                list_x.Add(0.5 * Length);
                list_x.Add(0.6 * Length);
                list_x.Add(0.7 * Length);
                list_x.Add(0.8 * Length);
                list_x.Add(0.9 * Length);
                list_x.Add(Length);


                list_x.Sort();




                #endregion Chiranjit [2011 09 23] Correct Create Data


                list_z.Add(0);

                if (Width_LeftCantilever != 0.0)
                {
                    last_z = Width_LeftCantilever;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);
                }

                if (Width_RightCantilever != 0.0)
                {
                    last_z = WidthBridge - Width_RightCantilever;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);
                }


                last_z = WidthBridge;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);

                last_z = Width_LeftCantilever + z_incr;
                do
                {
                    flag = false;
                    for (i = 0; i < list_z.Count; i++)
                    {
                        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                        {
                            flag = true; break;
                        }
                    }

                    if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                        list_z.Add(last_z);
                    last_z += z_incr;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

                    if (z_incr == 0.0) break;

                } while (last_z <= WidthBridge);

                list_z.Sort();
            }


            _Columns = list_x.Count;
            _Rows = list_z.Count;
            Total_Rows = _Rows;
            Total_Columns = _Columns;

            //int i = 0;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                if (!z_table.Contains(list_z[iRows]))
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

                    //nd.X = list_x[iCols] + (skew_length * list_z[iRows]);
                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }
            int nodeNo = 0;
            Joints.Clear();

            support_left_joints = "";
            support_right_joints = "";
            support_inner_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();


            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);


                    #region Finiding Support Joints
                    //if (iCols == 1 && iRows > 0 && iRows < _Rows - 1)
                    //    support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    //else if (iCols == _Columns - 2 && iRows >= 1 && iRows <= _Rows - 2)
                    //    support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    if (iCols == 0 && iRows > 0 && iRows < _Rows - 1)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1 && iRows >= 1 && iRows <= _Rows - 2)
                        support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";

                    else
                    {

                        if (Spans.Count > 1)
                        {
                            if (iRows > 0 && iRows < _Rows - 1)
                            {
                                double len = 0.0;
                                for (int k = 0; k < Spans.Count - 1; k++)
                                {
                                    len += Spans[k];
                                    if (Joints_Array[iRows, iCols].X == len)
                                    {
                                        support_inner_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                                        break;
                                    }
                                }

                                //if (Joints_Array[iRows, iCols].X == Spans[0] ||
                                //    Joints_Array[iRows, iCols].X == Spans[0] + Spans[1])
                                //    support_inner_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                            }
                        }
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }

                    #endregion Finiding Support Joints
                }
                if (list_nodes.Count > 0)
                {
                    joints_list_for_load.Add(MyList.Get_Array_Text(list_nodes));
                    list_nodes.Clear();
                }
            }


            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
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
                }
            }
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
                }
            }

            #region Chiranjit [2013 06 06]

            if (Width_LeftCantilever > 0)
            {
                list_envelop_outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
            }
            else
            {
                list_envelop_outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]


            Set_L2_L4_Deff_Girders();

        }

        List<string> Orthotropic_Data = new List<string>();


        JointNode[,] Bottom_Joints_Array;
        JointNode[,] Bottom_Cross_Joints_Array;

        List<JointNode> Bottom_Joints;

        public void CreateData_Orthotropic()
        {


            List<string> list = new List<string>();


            Bottom_Joints_Array = new JointNode[_Rows - 2, _Columns];
            Bottom_Cross_Joints_Array = new JointNode[_Rows - 2, _Columns];
            Bottom_Joints = new List<JointNode>();

            int i = 0;
            int r = 0;

            int jntNos = Joints.Count + 1;
            for (i = 1; i < _Rows - 1; i++)
            {
                for (r = 0; r < _Columns; r++)
                {

                    JointNode jn = new JointNode();
                    jn.NodeNo = jntNos++;
                    jn.X = Joints_Array[i, r].X;
                    //jn.Y = Joints_Array[i, r].Y + Steel_Section.Section_Long_Girder_at_Mid_Span.Dw / 1000;
                    jn.Y = Joints_Array[i, r].Y;
                    jn.Z = Joints_Array[i, r].Z;
                    Bottom_Joints_Array[i - 1, r] = jn;
                    Bottom_Joints.Add(jn);



                    jn = new JointNode();
                    jn.NodeNo = jntNos++;
                    jn.X = Joints_Array[i, r].X;
                    jn.Y = Joints_Array[i, r].Y + Steel_Section.Section_Cross_Girder.Dw / 1000;
                    //jn.Y = Joints_Array[i, r].Y;
                    jn.Z = Joints_Array[i, r].Z;
                    Bottom_Cross_Joints_Array[i - 1, r] = jn;
                    Bottom_Joints.Add(jn);



                }
            }


            int nc = 1;


            #region Concrete Deck Structure
            for (i = 1; i < _Rows; i++)
            {
                for (r = 1; r < _Columns; r++)
                {
                    //Joints_Array
                    list.Add(string.Format("{0} {1,10} {2,10} {3,10} {4,10}",
                        nc++,
                        Joints_Array[i - 1, r - 1].NodeNo,
                        Joints_Array[i - 1, r].NodeNo,
                        Joints_Array[i, r].NodeNo,
                        Joints_Array[i, r - 1].NodeNo
                        ));
                }
            }

            #endregion Concrete Deck Structure



            #region Long Web Structure
            for (i = 1; i < _Rows - 1; i++)
            {
                for (r = 1; r < _Columns; r++)
                {
                    //Joints_Array
                    //list.Add(string.Format("{0} {1,10} {2,10} {3,10} {4,10}",
                    //    nc++,
                    //    Joints_Array[i, r - 1].NodeNo,
                    //    Joints_Array[i, r].NodeNo,

                    //    Bottom_Joints_Array[i - 1, r].NodeNo,
                    //    Bottom_Joints_Array[i - 1, r - 1].NodeNo
                    //    ));


                    //Joints_Array
                    list.Add(string.Format("{0} {1,10} {2,10} {3,10} {4,10}",
                        nc++,
                        Joints_Array[i, r - 1].NodeNo,
                        Joints_Array[i, r].NodeNo,

                        Bottom_Cross_Joints_Array[i - 1, r].NodeNo,
                        Bottom_Cross_Joints_Array[i - 1, r - 1].NodeNo
                        ));


                    //Joints_Array
                    list.Add(string.Format("{0} {1,10} {2,10} {3,10} {4,10}",
                        nc++,
                        Bottom_Joints_Array[i - 1, r - 1].NodeNo,
                        Bottom_Cross_Joints_Array[i - 1, r - 1].NodeNo,
                        Bottom_Cross_Joints_Array[i - 1, r].NodeNo,
                        Bottom_Joints_Array[i - 1, r].NodeNo
                        ));



                }
            }

            #endregion Web Structure


            //if (false)
            if (true)
            {
                #region Cross Web Structure
                for (i = 1; i < _Rows - 2; i++)
                {
                    for (r = 0; r < _Columns; r++)
                    {
                        //Joints_Array
                        //list.Add(string.Format("{0} {1,10} {2,10} {3,10} {4,10}",
                        //    nc++,
                        //    Joints_Array[i, r - 1].NodeNo,
                        //    Joints_Array[i, r].NodeNo,

                        //    Bottom_Joints_Array[i - 1, r].NodeNo,
                        //    Bottom_Joints_Array[i - 1, r - 1].NodeNo
                        //    ));


                        //Joints_Array
                        list.Add(string.Format("{0} {1,10} {2,10} {3,10} {4,10}",
                            nc++,
                            Joints_Array[i, r].NodeNo,
                            Joints_Array[i + 1, r].NodeNo,
                            Bottom_Cross_Joints_Array[i, r].NodeNo,
                            Bottom_Cross_Joints_Array[i - 1, r].NodeNo
                            ));


                    }
                }

                #endregion Web Structure
            }
            Orthotropic_Data = list;



        }
        public void CreateData_AASHTO()
        {
            //Length
            double x_incr, z_incr;

            //x_incr = (Length / (Total_Columns - 1));
            //z_incr = (WidthBridge / (Total_Rows - 1));
            //NMG = 7;
            x_incr = Spacing_Cross_Girder;
            z_incr = Spacing_Long_Girder;

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = 12.1;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();


            int i = 0;
            bool flag = true;


            #region X cood

            double R = Radius;
            ////double R = 38;
            double L = Length;

            double theta = L * 180 / (Math.PI * R);
            //double theta = L * Math.PI / (180 * R);


            theta = L / (R);


            //xP2=xP1+rsin⁡θ; 
            //zP2=zP1−r(1−cos⁡θ);
            double xP1, xP2, zP1, Zp2;

            xP1 = 0.0;
            zP1 = 0.0;

            xP2 = xP1 + R * Math.Sin(theta);
            Zp2 = zP1 + R * (1 - Math.Cos(theta));

            double ang_incr = theta / 10;

            //for (int j = 0; j <= 10; j++)
            //{

            //    //list_x.Add(R * Math.Cos(ang_incr * j));
            //    //list_z.Add(R * Math.Sin(ang_incr * j));


            //    //xP2=xP1+rsin⁡θ; 
            //    //zP2=zP1−r(1−cos⁡θ);


            //    //list_x.Add(R * Math.Sin(ang_incr * j));
            //    //list_z.Add(R - R * Math.Cos(ang_incr * j));

            //}



            list_x.Clear();
            list_z.Clear();

            //    (ref int i, ref List<double> list_x, ref List<double> list_z, ref List<double> list_y, ref List<double> list_end_z)
            //{


            #endregion X cood


            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d;
            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            //if (false)


            #region Chiranjit [2011 09 23] Correct Create Data

            if (Spans.Count >= 1)
            {
                Hashtable x_tbl = new Hashtable();

                List<double> x_tmp = new List<double>();
                double len = 0.0;
                for (int j = 0; j < Spans.Count; j++)
                {
                    Length = Spans[j];
                    list_x = new List<double>();
                    x_tbl.Add(j, list_x);
                    #region Chiranjit [2011 09 23] Correct Create Data


                    list_x.Clear();
                    list_x.Add(0.0 * Length);
                    list_x.Add(0.1 * Length);
                    list_x.Add(0.2 * Length);
                    list_x.Add(0.3 * Length);
                    list_x.Add(0.4 * Length);
                    list_x.Add(0.5 * Length);
                    list_x.Add(0.6 * Length);
                    list_x.Add(0.7 * Length);
                    list_x.Add(0.8 * Length);
                    list_x.Add(0.9 * Length);
                    list_x.Add(Length);

                    list_x.Sort();



                    list_z.Add(0);

                    if (Width_LeftCantilever != 0.0)
                    {
                        last_z = Width_LeftCantilever;
                        last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                        list_z.Add(last_z);
                    }

                    if (Width_RightCantilever != 0.0)
                    {
                        last_z = WidthBridge - Width_RightCantilever;
                        last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                        list_z.Add(last_z);
                    }


                    last_z = WidthBridge;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);

                    last_z = Width_LeftCantilever + z_incr;
                    do
                    {
                        flag = false;
                        for (i = 0; i < list_z.Count; i++)
                        {
                            if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                            {
                                flag = true; break;
                            }
                        }

                        if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                            list_z.Add(last_z);
                        last_z += z_incr;
                        last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

                        if (z_incr == 0.0) break;

                    } while (last_z <= WidthBridge);

                    list_z.Sort();
                    #endregion Chiranjit [2011 09 23] Correct Create Data

                    if (j > 0)
                    {
                        foreach (var item in list_x)
                        {
                            if (!x_tmp.Contains(len + item))
                                x_tmp.Add(len + item);
                        }
                        len += Spans[j];
                    }
                    else
                    {
                        len = Spans[j];
                        foreach (var item in list_x)
                        {
                            x_tmp.Add(item);
                        }
                    }
                }


                list_x.Clear();
                list_x.AddRange(x_tmp.ToArray());


                list_z.Clear();

                list_z.Add(0.0);

                //bool IsBox_Girder = true;


                double web_space = Steel_Section.Section_Long_Girder_at_End_Span.S / 1000 / 2;
                double _zVals = 0.0;

                if (Width_LeftCantilever != 0.0)
                {
                    last_z = Width_LeftCantilever;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);


                }

                if (Width_RightCantilever != 0.0)
                {
                    last_z = WidthBridge - Width_RightCantilever;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                    list_z.Add(last_z);
                }


                last_z = WidthBridge;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);

                last_z = Width_LeftCantilever + z_incr;


                do
                {
                    flag = false;
                    for (i = 0; i < list_z.Count; i++)
                    {
                        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                        {
                            flag = true; break;
                        }
                    }
                    if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                    {
                        if (!list_z.Contains(last_z)) list_z.Add(last_z);
                    }
                    last_z += z_incr;
                    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

                    if (z_incr == 0.0) break;

                } while (last_z <= WidthBridge);

                list_z.Sort();
            }

            #endregion Chiranjit [2011 09 23] Correct Create Data


            List<double> lxt = new List<double>();

            for (int j = 0; j < list_x.Count; j++)
            {

                lxt.Add(R * Math.Sin(list_x[j] / Radius));
            }


            _Columns = list_x.Count;
            _Rows = list_z.Count;
            Total_Rows = _Rows;
            Total_Columns = _Columns;

            //int i = 0;

            List<double> list = new List<double>();

            z_table.Clear();

            #region 22
            Hashtable x_table = new Hashtable();
            List<double> lst_x = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                lst_x = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    ang_incr = list_x[iCols] / Radius;

                    var _r = Radius - list_z[iRows];

                    list.Add(_r * Math.Cos(ang_incr));
                    lst_x.Add(_r * Math.Sin(ang_incr));
                }
                z_table.Add(list_z[iRows], list);
                x_table.Add(list_z[iRows], lst_x);
            }
            #endregion 22

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];



            for (iRows = 0; iRows < _Rows; iRows++)
            {
                //list_x = z_table[list_z[iRows]] as List<double>;
                var li_z = z_table[list_z[iRows]] as List<double>;
                var li_x = x_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    //nd.Z = list_z[iRows];
                    nd.Z = Radius - li_z[iCols];

                    //nd.X = list_x[iCols] + (skew_length * list_z[iRows]);
                    //nd.X = list_x[iCols];
                    nd.X = li_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }
            int nodeNo = 0;
            Joints.Clear();

            support_left_joints = "";
            support_right_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();


            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                    #region Chiranjit [2013 05 06]
                    if (iCols == 1 && iRows > 0 && iRows < _Rows - 1)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 2 && iRows >= 1 && iRows <= _Rows - 2)
                        support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else
                    {
                        //if (iRows > 0 && iRows < _Rows - 1)
                        //    list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
                    #endregion Chiranjit [2013 05 06]
                }
            }


            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
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
                }
            }
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
                }
            }

            #region Chiranjit [2013 06 06]

            if (Width_LeftCantilever > 0)
            {
                list_envelop_outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
            }
            else
            {
                list_envelop_outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]

            try
            {

            }
            catch (Exception exx) { }

        }

        public void WriteData(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR COMPOSITE BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
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

            list.Add("SECTION PROPERTIES");
            list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
            list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
            list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
            list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
            list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
            list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
            list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
            list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
            list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
            list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
            list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add("3 5 7 9  PINNED");
            list.Add("113 115 117 119  PINNED");
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("LOAD GENERATION 191");
            //list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));

        }


        public void WriteData_Total_Analysis(string file_name)
        {
            WriteData_Total_Analysis(file_name, false);
        }
        public void WriteData_Total_Analysis(string file_name, bool is_british, List<string> ll_data)
        {
            //WriteData_Total_Analysis(file_name, false, is_british);

            //string fn = Path.GetDirectoryName(file_name);
            //fn = Path.Combine(fn, "LL.TXT");
            //File.WriteAllLines(fn, ll_data.ToArray());


        }

        public void WriteData_Orthotropic_Analysis(string file_name, bool is_british)
        {
            if (file_name == "")
            {
                string pd = Path.Combine(working_folder, "TempAnalysis");
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                pd = Path.Combine(pd, "OrthoAnalysis");
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                file_name = Path.Combine(pd, "OrthoAnalysis.txt");
            }

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            List<int> DeckSlab = new List<int>();

            List<int> Inner_Girder_Mid = new List<int>();
            List<int> Inner_Girder_Support = new List<int>();

            List<int> Outer_Girder_Mid = new List<int>();
            List<int> Outer_Girder_Support = new List<int>();

            List<int> Cross_Girder_Inter = new List<int>();
            List<int> Cross_Girder_End = new List<int>();


            List<int> HA_Members = new List<int>();

            List<double> HA_Dists = new List<double>();
            HA_Dists = new List<double>();
            //if (HA_Lanes != null)
            //{
            //    for (i = 0; i < HA_Lanes.Count; i++)
            //    {
            //        HA_Dists.Add(1.75 + (HA_Lanes[i] - 1) * 3.5);
            //    }
            //}

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT KIP FT");
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

            #region Element Connectibity
            list.Add("ELEMENT CONNECTIVITY");

            int ele_count = 1;
            for (int iColumn = 1; iColumn < _Columns; iColumn++)
            {
                for (int iRows = 1; iRows < _Rows; iRows++)
                {
                    list.Add(string.Format("{0} {1} {2} {3} {4}",
                        ele_count++,
                        Joints_Array[iRows - 1, iColumn - 1].NodeNo,
                        Joints_Array[iRows, iColumn - 1].NodeNo,
                        Joints_Array[iRows, iColumn].NodeNo,
                        Joints_Array[iRows - 1, iColumn].NodeNo
                        ));

                }
            }
            #endregion Element Connectibity




            ele_count--;







            int index = 2;

            for (int c = 0; c < _Rows; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (i <= 1 || i >= (_Columns - 3))
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Support.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {
                            var item = Long_Girder_Members_Array[c, i];

                            if (HA_Dists.Contains(item.EndNode.Z) && HA_Dists.Contains(item.StartNode.Z))
                                HA_Members.Add(item.MemberNo);
                            else
                                Inner_Girder_Support.Add(item.MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {

                            var item = Long_Girder_Members_Array[c, i];

                            if (HA_Dists.Contains(item.EndNode.Z) && HA_Dists.Contains(item.StartNode.Z))
                                HA_Members.Add(item.MemberNo);
                            else
                                Inner_Girder_Mid.Add(item.MemberNo);


                            //Inner_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                }
            }

            Outer_Girder_Mid.Sort();
            Outer_Girder_Support.Sort();


            Inner_Girder_Mid.Sort();
            Inner_Girder_Support.Sort();
            DeckSlab.Sort();
            index = 2;
            List<int> lst_index = new List<int>();
            for (int n = 1; n <= NCG - 2; n++)
            {
                for (i = 0; i < _Columns; i++)
                {
                    if (Cross_Girder_Members_Array[0, i].StartNode.X.ToString("0.00") == (Spacing_Cross_Girder * n + Effective_Depth).ToString("0.00"))
                    {
                        index = i;
                        lst_index.Add(i);
                    }
                }
            }
            for (int c = 0; c < _Rows - 1; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (lst_index.Contains(i))
                        Cross_Girder_Inter.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    else if (i == 1 || i == _Columns - 2)
                    {
                        Cross_Girder_End.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                        DeckSlab.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                }
            }

            DeckSlab.Sort();
            Cross_Girder_Inter.Sort();
            Cross_Girder_End.Sort();


            //_Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder);
            //_Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder);
            //_Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder);




            //string _DeckSlab = "";
            //string _Inner_Girder_Mid = "";
            //string _Inner_Girder_Support = "";
            //string _Outer_Girder_Mid = "";
            //string _Outer_Girder_Support = "";
            //string _Cross_Girder_Inter = "";
            //string _Cross_Girder_End = "";




            _DeckSlab = MyList.Get_Array_Text(DeckSlab);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder_Mid);
            _Inner_Girder_Support = MyList.Get_Array_Text(Inner_Girder_Support);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder_Mid);
            _Outer_Girder_Support = MyList.Get_Array_Text(Outer_Girder_Support);
            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder_Inter);
            _Cross_Girder_End = MyList.Get_Array_Text(Cross_Girder_End);



            //HA_Loading_Members = MyList.Get_Array_Text(HA_Members);

            list.Add("SECTION PROPERTIES");
            if (Steel_Section != null)
            {
                Write_Composite_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }


            list.Add("ELEMENT PROPERTIES");
            if (Steel_Section != null)
            {
                list.Add(string.Format("1 TO {0} TH {1}", ele_count, Steel_Section.Ds / 1000));
            }

            list.Add("MATERIAL CONSTANTS");
            list.Add("E 2.85E6 ALL");
            //list.Add("E " + Ecm * 100 + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");


            list.Add("SUPPORT");

            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));


            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }

        public void WriteData_Total_Analysis(string file_name, bool is_PSC_I_Girder)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR COMPOSITE BRIDGE DECK ANALYSIS");
            list.Add("UNIT KIP FT");
            //list.Add("UNIT METER MTON");
            //list.Add("UNIT MTON METRE");
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
            //list.Add("UNIT KIP FT");
            list.Add("SECTION PROPERTIES");

            if (Steel_Section != null)
            {
                Write_Composite_Section_Properties(list);
            }
            else
            {
                list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
                list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            }
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            //list.Add("DENSITY CONCRETE ALL");
            //list.Add("POISSON CONCRETE ALL");
            list.Add("E STEEL ALL");
            list.Add("DENSITY STEEL ALL");
            list.Add("POISSON STEEL ALL");
            list.Add("SUPPORT");
            if (is_PSC_I_Girder)
            {
                Total_Rows = _Rows;

                string k = "";
                for (int c = 1; c < Joints[_Rows].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, Start_Support));
                k = "";
                for (int c = Joints[Joints.Count - _Rows].NodeNo; c <= Joints[Joints.Count - 1].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0}  {1}", k, End_Support));
            }
            else
            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
                list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));
                if (Spans.Count > 1)
                {
                    list.Add(string.Format("{0}  PINNED", support_inner_joints));
                }
            }
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }
        public void WriteData_LiveLoad_Analysis(string file_name)
        {
            WriteData_LiveLoad_Analysis(file_name, false);
        }
        public void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_data)
        {
            WriteData_LiveLoad_Analysis(file_name, false);

            string fn = Path.GetDirectoryName(file_name);
            fn = Path.Combine(fn, "LL.TXT");
            File.WriteAllLines(fn, ll_data.ToArray());


        }
        public void WriteData_LiveLoad_Analysis(string file_name, bool is_psc_I_Girder)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR COMPOSITE BRIDGE DECK ANALYSIS WITH LIVE LOAD");
            list.Add("UNIT KIP FT");
            //list.Add("UNIT METER MTON");
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

            //list.Add("UNIT KIP FT");
            list.Add("SECTION PROPERTIES");
            
            if (Steel_Section != null)
            {
                Write_Composite_Section_Properties(list);
            }
            else
            {
                list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
                list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E STEEL ALL");
            list.Add("DENSITY STEEL ALL");
            list.Add("POISSON STEEL ALL");
            list.Add("SUPPORT");
            if (is_psc_I_Girder)
            {
                Total_Rows = _Rows;

                string k = "";
                for (int c = 1; c < Joints[_Rows].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} PINNED", k));
                k = "";
                for (int c = Joints[Joints.Count - _Rows].NodeNo; c <= Joints[Joints.Count - 1].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} PINNED", k));

            }
            else
            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                list.Add(string.Format("{0} PINNED", support_left_joints));
                list.Add(string.Format("{0} PINNED", support_right_joints));

                //list.Add(string.Format("{0} PINNED", support_left_joints));
                //list.Add(string.Format("{0} PINNED", support_right_joints));

                if (Spans.Count > 1)
                {
                    list.Add(string.Format("{0}  PINNED", support_inner_joints));
                }
            }
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("1 TO 220 UNI GY -0.0001");
            //list.Add("151 160 171 180 UNI GY -2.66888");
            //list.Add("152 159 172 179 UNI GY -1.68024");
            //list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            //list.Add("131 140 191 200 UNI GY -2.97768");
            //list.Add("132 139 192 199 UNI GY -1.89528");
            //list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            //list.Add("** SIDL");
            //list.Add("MEMBER LOAD");
            //list.Add("** WEARING COAT");
            //list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            //list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            //list.Add("**CRASH BARRIER");
            //list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            //list.Add("**** OUTER GIRDER *********");
            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }
        public void WriteData_DeadLoad_Analysis(string file_name)
        {
            WriteData_DeadLoad_Analysis(file_name, false);
        }
        public void WriteData_DeadLoad_Analysis(string file_name, bool is_PSC_I_Gider)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR COMPOSITE BRIDGE DECK ANALYSIS WITH DEAD LOAD");
            list.Add("UNIT KIP FT");
            //list.Add("UNIT METER MTON");
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

            //list.Add("UNIT KIP FT");
            list.Add("SECTION PROPERTIES");
            if (Steel_Section != null)
            {
                Write_Composite_Section_Properties(list);
            }
            else
            {
                list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
                list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            }
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            //list.Add("DENSITY CONCRETE ALL");
            //list.Add("POISSON CONCRETE ALL");

            list.Add("E STEEL ALL");
            list.Add("DENSITY STEEL ALL");
            list.Add("POISSON STEEL ALL");
            list.Add("SUPPORT");
            if (is_PSC_I_Gider)
            {
                Total_Rows = _Rows;

                string k = "";
                for (int c = 1; c < Joints[_Rows].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} PINNED", k));
                k = "";
                for (int c = Joints[Joints.Count - _Rows].NodeNo; c <= Joints[Joints.Count - 1].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} PINNED", k));

            }
            else
            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                //list.Add(string.Format("{0} PINNED", support_left_joints));
                //list.Add(string.Format("{0} PINNED", support_right_joints));
                //Chiranjit [2013 05 06]
                list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
                list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));

                if (Spans.Count > 1)
                {
                    list.Add(string.Format("{0}  PINNED", support_inner_joints));
                }
            }




            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("LOAD GENERATION 191");
            //list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }

        public void WriteData_Orthotropic_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR COMPOSITE BRIDGE DECK ANALYSIS ORTHOTROPIC");
            list.Add("UNIT KIP FT");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                var jn = new JointNode();
                jn.NodeNo = Joints[i].NodeNo;
                jn.X = Joints[i].X;
                //jn.Y = Joints[i].Y + Bottom_Joints[0].Y;
                jn.Y = Joints[i].Y + Steel_Section.Section_Long_Girder_at_Mid_Span.Dw / 1000;
                jn.Z = Joints[i].Z;

                //list.Add(Joints[i].ToString());
                list.Add(jn.ToString());
            }
            for (i = 0; i < Bottom_Joints.Count; i++)
            {
                list.Add(Bottom_Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            list.Add("ELEMENT CONNECTIVITY");

            for (i = 0; i < Orthotropic_Data.Count; i++)
            {
                list.Add(Orthotropic_Data[i].ToString());
            }
            list.Add("SECTION PROPERTIES");
            if (Steel_Section != null)
            {
                Write_Composite_Section_Properties(list);
            }
            else
            {
                list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
                list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            }
            list.Add("ELEMENT PROP");
            list.Add(string.Format("1 TO {0} TH 0.5", Orthotropic_Data.Count));
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            //list.Add("DENSITY CONCRETE ALL");
            //list.Add("POISSON CONCRETE ALL");

            list.Add("E STEEL ALL");
            list.Add("DENSITY STEEL ALL");
            list.Add("POISSON STEEL ALL");
            list.Add("SUPPORT");

            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                //list.Add(string.Format("{0} PINNED", support_left_joints));
                //list.Add(string.Format("{0} PINNED", support_right_joints));
                //Chiranjit [2013 05 06]
                list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
                list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));

                if (Spans.Count > 1)
                {
                    list.Add(string.Format("{0}  PINNED", support_inner_joints));
                }
            }




            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("LOAD GENERATION 191");
            //list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }

        private void Write_Composite_Section_Properties(List<string> list)
        {
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:E4} IY {3:E4} IZ {4:E4}",
                Cross_Girders_as_String,
                Steel_Section.Section_Cross_Girder.Area_in_Sq_FT,
                Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_FT,
                Steel_Section.Section_Cross_Girder.Iyy_in_Sq_Sq_FT,
                Steel_Section.Section_Cross_Girder.Izz_in_Sq_Sq_FT));

            Steel_Section.Calculate_Composite_Section();

            double Ax = 0.0;

            double ixx = 0.0;
            double iyy = 0.0;
            double izz = 0.0;



            if (Steel_Section.Section_Long_Girder_at_End_Span.Nb == 1)
            {
                Ax = Steel_Section.Section_Long_Girder_at_End_Span.AX_Comp;
                ixx = Steel_Section.Section_Long_Girder_at_End_Span.IX_Comp;
                iyy = Steel_Section.Section_Long_Girder_at_End_Span.IY_Comp;
            }
            else
            {
                Ax = Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_FT;
                ixx = Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_FT;
                iyy = Steel_Section.Section_Long_Girder_at_End_Span.Iyy_in_Sq_Sq_FT;
            }
            Ax = (Ax / 12);

            izz = ixx + iyy;


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                L0_Girders_as_String,
                Ax, ixx, iyy, izz));




            if (Steel_Section.Section_Long_Girder_at_L4_Span.Nb == 1)
            {
                Ax = Steel_Section.Section_Long_Girder_at_L4_Span.AX_Comp;
                ixx = Steel_Section.Section_Long_Girder_at_L4_Span.IX_Comp;
                iyy = Steel_Section.Section_Long_Girder_at_L4_Span.IY_Comp;
            }
            else
            {
                Ax = Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_FT;
                ixx = Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_FT;
                iyy = Steel_Section.Section_Long_Girder_at_L4_Span.Iyy_in_Sq_Sq_FT;
            }

            Ax = (Ax / 12);

            izz = ixx + iyy;

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                            L1_Girders_as_String,
                            Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L2_Girders_as_String,
                             Ax, ixx, iyy, izz));

            if (Steel_Section.Section_Long_Girder_at_Mid_Span.Nb == 1)
            {
                Ax = Steel_Section.Section_Long_Girder_at_Mid_Span.AX_Comp;
                ixx = Steel_Section.Section_Long_Girder_at_Mid_Span.IX_Comp;
                iyy = Steel_Section.Section_Long_Girder_at_Mid_Span.IY_Comp;
            }
            else
            {
                Ax = Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_FT;
                ixx = Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_FT;
                iyy = Steel_Section.Section_Long_Girder_at_Mid_Span.Iyy_in_Sq_Sq_FT;
            }

            Ax = (Ax / 12);
            izz = ixx + iyy;

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L3_Girders_as_String,
                             Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L4_Girders_as_String,
                             Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L5_Girders_as_String,
                             Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L6_Girders_as_String,
                             Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L7_Girders_as_String,
                             Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L8_Girders_as_String,
                             Ax, ixx, iyy, izz));

            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           L9_Girders_as_String,
                             Ax, ixx, iyy, izz));


        }

        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {

            if (WidthBridge == 0) return;
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
                        LoadList.Add(ld);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        List<string> Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Structure.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            double z_min = double.MaxValue;
            double x = double.MaxValue;
            int indx = -1;

            int i = 0;
            int j = 0;

            List<double> list_z = new List<double>();
            List<string> list_arr = new List<string>();

            List<MemberCollection> list_mc = new List<MemberCollection>();

            double last_z = 0.0;

            while (mc.Count != 0)
            {
                indx = -1;
                for (i = 0; i < mc.Count; i++)
                {
                    if (z_min > mc[i].StartNode.Z)
                    {
                        z_min = mc[i].StartNode.Z;
                        indx = i;
                    }
                }
                if (indx != -1)
                {

                    if (!list_z.Contains(z_min))
                        list_z.Add(z_min);

                    sort_membs.Add(mc[indx]);
                    mc.Members.RemoveAt(indx);
                    z_min = double.MaxValue;
                }
            }

            last_z = -1.0;

            //Inner & Outer Long Girder
            MemberCollection outer_long = new MemberCollection();
            MemberCollection inner_long = new MemberCollection();
            MemberCollection inner_cross = new MemberCollection();


            z_min = Structure.Analysis.Joints.MinZ;
            double z_max = Structure.Analysis.Joints.MaxZ;


            //Store inner and outer Long Girder
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (((sort_membs[i].StartNode.Z == z_min) || (sort_membs[i].StartNode.Z == z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    outer_long.Add(sort_membs[i]);
                }
                else if (((sort_membs[i].StartNode.Z != z_min) && (sort_membs[i].StartNode.Z != z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    inner_long.Add(sort_membs[i]);
                }
            }

            List<int> Outer_Joints = new List<int>();
            List<int> Inner_Joints = new List<int>();

            for (i = 0; i < outer_long.Count; i++)
            {
                if (Outer_Joints.Contains(outer_long[i].EndNode.NodeNo) == false)
                    Outer_Joints.Add(outer_long[i].EndNode.NodeNo);
                if (Outer_Joints.Contains(outer_long[i].StartNode.NodeNo) == false)
                    Outer_Joints.Add(outer_long[i].StartNode.NodeNo);
            }

            for (i = 0; i < inner_long.Count; i++)
            {
                if (Inner_Joints.Contains(inner_long[i].EndNode.NodeNo) == false)
                    Inner_Joints.Add(inner_long[i].EndNode.NodeNo);
                if (Inner_Joints.Contains(inner_long[i].StartNode.NodeNo) == false)
                    Inner_Joints.Add(inner_long[i].StartNode.NodeNo);
            }
            Outer_Joints.Sort();
            Inner_Joints.Sort();


            string inner_long_text = "";
            string outer_long_text = "";
            int last_val = 0;
            int to_val = 0;
            int from_val = 0;

            last_val = Outer_Joints[0];
            from_val = last_val;
            bool flag_1 = false;
            for (i = 0; i < Outer_Joints.Count; i++)
            {
                if (i < Outer_Joints.Count - 1)
                {
                    if ((Outer_Joints[i] + 1) == (Outer_Joints[i + 1]))
                    {
                        if (flag_1 == false)
                        {
                            from_val = Outer_Joints[i];
                        }
                        flag_1 = true;
                        to_val = Outer_Joints[i + 1];
                    }
                    else
                    {
                        if (flag_1)
                        {
                            outer_long_text = from_val + " TO " + to_val + " ";
                            flag_1 = false;
                        }
                        else
                        {
                            outer_long_text = outer_long_text + " " + last_val;
                        }
                    }
                    last_val = Outer_Joints[i];
                }
                else
                {
                    if (flag_1)
                    {
                        outer_long_text += from_val + " TO " + to_val + " ";
                        flag_1 = false;
                    }
                    else
                    {
                        outer_long_text = outer_long_text + " " + last_val;
                    }
                }
            }

            for (i = 0; i < Inner_Joints.Count; i++)
            {
                if (i < Inner_Joints.Count - 1)
                {
                    if ((Inner_Joints[i] + 1) == (Inner_Joints[i + 1]))
                    {
                        if (flag_1 == false)
                        {
                            from_val = Inner_Joints[i];
                        }
                        flag_1 = true;
                        to_val = Inner_Joints[i + 1];
                    }
                    else
                    {
                        if (flag_1)
                        {
                            inner_long_text = from_val + " TO " + to_val + " ";
                            flag_1 = false;
                        }
                        else
                        {
                            inner_long_text = inner_long_text + " " + last_val;
                        }
                    }
                    last_val = Inner_Joints[i];
                }
                else
                {
                    if (flag_1)
                    {
                        inner_long_text += from_val + " TO " + to_val + " ";
                        flag_1 = false;
                    }
                    else
                    {
                        inner_long_text = inner_long_text + " " + last_val;
                    }
                }
            }
            list_arr.Add(inner_long_text + " FY  -" + load.ToString("0.000"));
            list_arr.Add(outer_long_text + " FY  -" + (load / 2.0).ToString("0.000"));

            return list_arr;
        }

        internal string GetAnalysis_Input_File(int p)
        {
            if (p == 0)
                return DeadLoadAnalysis_Deck_Input_File;
            else if (p == 1)
                return DeadLoadAnalysis_Girder_Input_File;
            else if (p == 2)
                return TotalAnalysis_Input_File;
            else
            {
                return Get_Live_Load_Analysis_Input_File(p - 2);
            }
            return "";

        }

        public string GetAnalysis_Input_File(int p, bool IsStageFile)
        {
            if (p == 0)
                return Straight_Deck_DL_File;
            if (p == 1)
                return Straight_Girder_DL_File;
            else if (p == 2)
                return Straight_TL_File;
            //else if (p == 2)
            //    return Straight_LL_File;
            else if (p > 2)
            {
                return Get_Live_Load_Analysis_Input_File(p - 2, true);
            }
            return "";
        }

        public string Get_LHS_Outer_Girder()
        {
            string LHS = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, _Columns - 2].MemberNo;
            return LHS;

        }
        public string Get_RHS_Outer_Girder()
        {
            string RHS = Long_Girder_Members_Array[_Rows - 2, 0].MemberNo + " TO " + Long_Girder_Members_Array[_Rows - 2, _Columns - 2].MemberNo;
            return RHS;
        }
    }

    public class AASHTO_Design
    {
      
        #region Set_Input Data

        public static void Input_Positive_Moment_Region_Section_Properties(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Positive_Moment_Region_Section_Properties

            list.Add(string.Format("Girder only:$$$$$$"));
            list.Add(string.Format("Top flange$8.75$55.188$482.9$0.3$7530.2$7530.5"));
            list.Add(string.Format("Web$27$27.875$752.6$6561$110.5$6671.5"));
            list.Add(string.Format("Bottom Flange$12.25$0.438$5.4$0.8$7912$7912.7"));
            list.Add(string.Format("Total$48$25.852$1240.9$6562.1$15552.7$22114.7"));
            list.Add(string.Format("Composite (3n):$$$$$$"));
            list.Add(string.Format("Girder$48$25.852$1240.9$22114.8$11134.4$33249.2"));
            list.Add(string.Format("Slab$34.333$62.375$2141.5$183.1$15566.5$15749.6"));
            list.Add(string.Format("Total$82.333$41.082$3382.4$22297.9$26700.9$48998.8"));
            list.Add(string.Format("Composite (n):$$$$$$"));
            list.Add(string.Format("Girder$48$25.852$1240.9$22114.8$29792.4$51907.2"));
            list.Add(string.Format("Slab$103$62.375$6424.6$549.3$13883.8$14433.2"));
            list.Add(string.Format("Total$151$50.765$7665.5$22664.1$43676.2$66340.4"));
            //list.Add(string.Format("Section$ybotgdr$ytopgdr$ytopslab$Sbotgdr$Stopgdr$Stopslab"));
            //list.Add(string.Format("$(Inches)$(Inches)$(Inches)$(Inches3)$(Inches3)$(Inches3)"));
            //list.Add(string.Format("Girder only$25.852$29.648$---$855.5$745.9$---"));
            //list.Add(string.Format("Composite (3n)$41.082$14.418$25.293$1192.7$3398.4$1937.2"));
            //list.Add(string.Format("Composite (n)$50.765$4.735$15.61$1306.8$14010.3$4249.8"));

            #endregion Positive_Moment_Region_Section_Properties

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);

            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        }

        public static void Input_Negative_Moment_Region_Section_Properties(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Negative_Moment_Region_Section_Properties


            list.Add(string.Format("Girder only:$$$$$$"));
            list.Add(string.Format("Top flange$35$58$2030$18.2$30009.7$30027.9"));
            list.Add(string.Format("Web$27$29.75$803.3$6561$28.7$6589.7"));
            list.Add(string.Format("Bottom flange$38.5$1.375$52.9$24.3$28784.7$28809"));
            list.Add(string.Format("Total$100.5$28.718$2886.2$6603.5$58823.1$65426.6"));
            list.Add(string.Format("Composite (deck concrete using 3n):$$$$$$"));
            list.Add(string.Format("Girder$100.5$28.718$2886.2$65426.6$8226.9$73653.5"));
            list.Add(string.Format("Slab$34.333$64.25$2205.9$183.1$24081.6$24264.7"));
            list.Add(string.Format("Total$134.833$37.766$5092.1$65609.7$32308.5$97918.2"));
            list.Add(string.Format("Composite (deck concrete using n):$$$$$$"));
            list.Add(string.Format("Girder$100.5$28.718$2886.2$65426.6$32504.5$97931.2"));
            list.Add(string.Format("Slab$103$64.25$6617.8$549.3$31715.6$32264.9"));
            list.Add(string.Format("Total$203.5$46.703$9504$65975.9$64220.1$130196.1"));
            list.Add(string.Format("Composite (deck reinforcement only):$$$$$$"));
            list.Add(string.Format("Girder$100.5$28.718$2886.2$65426.6$1568.1$66994.7"));
            list.Add(string.Format("Deck reinf.$12.772$63.75$814.2$0$12338.7$12338.7"));
            list.Add(string.Format("Total$113.272$32.668$3700.4$65426.6$13906.8$79333.4"));


            //list.Add(string.Format("Section$ybotgdr$ytopgdr$ytopslab$Sbotgdr$Stopgdr$Stopslab"));
            //list.Add(string.Format("$(Inches)$(Inches)$(Inches)$(Inches3)$(Inches3)$(Inches3)"));
            //list.Add(string.Format("Girder only$28.718$30.532$---$2278.2$2142.9$---"));
            //list.Add(string.Format("Composite (3n)$37.766$21.484$30.484$2592.8$4557.7$3212.1"));
            //list.Add(string.Format("Composite (n)$46.702$12.548$21.548$2787.8$10376.2$6042.3"));
            //list.Add(string.Format("Composite (rebar)$32.668$26.582$31.082$2428.5$2984.5$2552.4"));

            #endregion Negative_Moment_Region_Section_Properties

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);

            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        }

        public static void Input_Deck_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Deck Inputs

           
            list.Add(string.Format("Deck properties:$$$"));
            list.Add(string.Format("Deck width:$Wdeck$46.875$ft"));
            list.Add(string.Format("Roadway width:$Wroadway$44$ft"));
            list.Add(string.Format("Bridge length:$Ltotal$240$ft"));
            list.Add(string.Format("Skew Angle$Skew$0$degree"));
            list.Add(string.Format("Structural steel yield strength:$Fy$50$ksi"));
            list.Add(string.Format("Structural steel tensile strength:$Fu$65$ksi"));
            list.Add(string.Format("Concrete 28day compressive strength$f'c$4$ksi"));
            list.Add(string.Format("Reinforcement strength$fy$60$ksi"));
            list.Add(string.Format("Steel density$Ws$0.49$kcf"));
            list.Add(string.Format("Concrete density$Wc$0.15$kcf"));
            list.Add(string.Format("Parapet weight (each)$Wpar$0.53$K/ft"));
            list.Add(string.Format("Future wearing surface$Wfws$0.14$kcf"));
            list.Add(string.Format("Future wearing surface thickness$tfws$2.5$in"));
            list.Add(string.Format("Girder Properties:$$$"));


            
            list.Add(string.Format("Girder spacing$S$9.75$ft"));
            list.Add(string.Format("Number of girders$N$5$nos"));
            list.Add(string.Format("Deck top cover$Covert$2.5$in"));
            list.Add(string.Format("Deck bottom cover$Coverb$1$in"));
            list.Add(string.Format("Concrete density$Wc$0.15$kcf"));
            list.Add(string.Format("Concrete 28day compressive strength$f'c$4$ksi"));
            list.Add(string.Format("Reinforcement strength:$Fy$60$ksi"));
            list.Add(string.Format("Future wearing surface:$Wfws$0.14$kcf"));
            list.Add(string.Format("Parapet properties:$$$"));
            list.Add(string.Format("Weight per foot:$Wpar$0.53$K/ft"));
            list.Add(string.Format("Width at base:$Wbase$1.4375$ft"));
            list.Add(string.Format("Moment capacity at base*:$Mco$-28.21$K.ft/ft"));
            list.Add(string.Format("Parapet height:$Hpar$3.5$ft"));
            list.Add(string.Format("Critical length of yield line failure pattern*:$Lc$12.84$ft"));
            list.Add(string.Format("Total transverse resistance of the parapet*:$Rw$117.36$K"));
            list.Add(string.Format("Determine Minimum Overhang Thickness$$$"));
            list.Add(string.Format("Minimum deck overhang thickness$to$8$in"));
            list.Add(string.Format("Select  Slab and Overhang Thickness$$$"));
            list.Add(string.Format("Assume slab thicknesses$ts$8.5$in"));
            list.Add(string.Format("Assume overhang thicknesses$to$9$in"));
            list.Add(string.Format("Compute Live Load Effects$$$"));
            list.Add(string.Format("Dynamic load allowance, $IM$0.33$"));
            list.Add(string.Format("Load factor for live load - Strength I   $YLL$1.75$"));
            list.Add(string.Format("Strength limit state$fstr$0.9$"));
            list.Add(string.Format("Service limit state$fserv$1$"));
            list.Add(string.Format("Extreme event limit state$fext$1$"));
            list.Add(string.Format("Design for Positive Flexure in Deck$$$"));
            list.Add(string.Format("Assume Nos of Bars$bar_nos$5$nos"));
            list.Add(string.Format("Bar Diameter$bar_diam =$0.625$in"));
            
            list.Add(string.Format("$Es $29000$ksi"));
            list.Add(string.Format("$Ec $3640$ksi"));
            #endregion Deck Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);

            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        }
        public static void Input_Steel_Girder_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design criteria:$$$"));
            
            list.Add(string.Format("Number of spans:$Nspans =$2$"));
            list.Add(string.Format("Span length:$Lspan =$120$ft"));
            list.Add(string.Format("Skew angle:$Skew =$0$deg"));
            list.Add(string.Format("Number of girders:$Ngirders =$5$"));
            list.Add(string.Format("Girder spacing:$S =$9.75$ft"));
            list.Add(string.Format("Deck overhang:$Soverhang =$3.9375$ft"));
            list.Add(string.Format("Cross-frame spacing:$Lb =$20$ft"));
            list.Add(string.Format("Web yield strength:$Fyw =$50$ksi"));
            list.Add(string.Format("Flange yield strength:$Fyf =$51$ksi"));
            list.Add(string.Format("Concrete 28-day compressive strength:$f'c =$4$ksi"));
            list.Add(string.Format("Reinforcement strength:$fy =$60$ksi"));
            list.Add(string.Format("Dia of bar$Bar dia=$0.625$in"));
            //list.Add(string.Format("Area of bar$Bar area=$0.31$in2"));
            
            list.Add(string.Format("Total deck thickness:$tdeck =$8.5$in"));
            list.Add(string.Format("Effective deck thickness:$teffdeck =$8$in"));
            list.Add(string.Format("Total overhang thickness:$toverhang =$9$in"));
            list.Add(string.Format("Effective overhang thickness:$teffoverhang =$8.5$"));
            list.Add(string.Format("Steel density:$Ws =$0.49$kcf"));
            list.Add(string.Format("Concrete density:$Wc =$0.15$kcf"));
            list.Add(string.Format("Additional miscellaneous dead load (per girder):$Wmisc =$0.015$K/ft"));
            list.Add(string.Format("Stay-in-place deck form weight:$Wdeckforms =$0.015$ksf"));
            list.Add(string.Format("Parapet weight (each):$Wpar =$0.53$K/ft"));
            list.Add(string.Format("Future wearing surface:$Wfws =$0.14$kcf"));
            list.Add(string.Format("Future wearing surface thickness:$tfws =$2.5$in"));
            list.Add(string.Format("Deck width:$wdeck =$46.875$ft"));
            list.Add(string.Format("Roadway width:$wroadway =$44$ft"));
            list.Add(string.Format("Haunch depth (from top of web):$dhaunch =$3.5$in"));
            list.Add(string.Format("Average Daily Truck Traffic (Single-Lane):$ADTTSL =$3000$"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Bolted_Splice_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step 4.1 - Obtain Design Criteria$$$"));
            list.Add(string.Format("Yield Strength:$Fy =$50$ksi"));
            list.Add(string.Format("Tensile Strength:$Fu =$65$ksi"));
            list.Add(string.Format("Flange Yield Strength:$Fyf =$50$ksi"));
            list.Add(string.Format("Plate Dimensions of the Left Girder (reference Design Step 3.18):$$$"));
            list.Add(string.Format("Web Thickness:$tw =$0.5$in"));
            list.Add(string.Format("Web Depth:$D =$54$in"));
            list.Add(string.Format("Top Flange Width:$bfltL =$14$in"));
            list.Add(string.Format("Top Flange Thickness:$tfltL =$0.625$in"));
            list.Add(string.Format("Bottom Flange Width:$bflbL =$14$in"));
            list.Add(string.Format("Bottom Flange Thickness:$tflbL =$0.875$in"));
            list.Add(string.Format("Plate Dimensions of the Right Girder (reference Design Step 3.18):$$$"));
            list.Add(string.Format("Web Thickness:$tw =$0.5$in"));
            list.Add(string.Format("Web Depth:$D =$54$in"));
            list.Add(string.Format("Top Flange Width:$bfltR =$14$in"));
            list.Add(string.Format("Top Flange Thickness:$tfltR =$1.125$in"));
            list.Add(string.Format("Bottom Flange Width:$bflbR =$14$in"));
            list.Add(string.Format("Bottom Flange Thickness:$tflbR =$1.375$in"));
            list.Add(string.Format("Splice Bolt Properties:$$$"));
            list.Add(string.Format("Bolt Diameter:$dbolt =$0.875$in"));
            list.Add(string.Format("Bolt Hole Diameter:$dhole =$1.0$in"));
            list.Add(string.Format("Bolt Tensile Strength:$Fubolt =$120$ksi"));
            list.Add(string.Format("Concrete Deck Properties (reference Design Step 3.3):$$$"));
            list.Add(string.Format("Effective Slab Thickness:$tseff =$8$in"));
            list.Add(string.Format("Modular Ratio:$n =$8$nos"));
            list.Add(string.Format("Haunch Depth:$dhaunch =$3.5$in"));
            list.Add(string.Format("(measured from top of web)$$$"));
            
            list.Add(string.Format("Effective Flange Width:$Weff =$103$in"));
            
            list.Add(string.Format("$bar_diam =$0.625$in"));
            
            list.Add(string.Format("Resistance Factors:$$$"));
            
            list.Add(string.Format("Flexure:$ff =$1.00$"));
            list.Add(string.Format("Shear:$fv =$1.00$"));
            list.Add(string.Format("Axial Compression:$fc =$0.90$"));
            list.Add(string.Format("Tension, fracture in net section:$fu =$0.80$"));
            list.Add(string.Format("Tension, yielding in gross section:$fy =$0.95$"));
            list.Add(string.Format("Bolts bearing on material:$fbb =$0.80$"));
            list.Add(string.Format("A325 and A490 bolts in shear:$fs =$0.80$"));
            list.Add(string.Format("Block shear:$fbs =$0.80$"));
            
            list.Add(string.Format("Design Step 4.4 - Design Bottom Flange Splice$$$"));
            
            list.Add(string.Format("Thickness of the inside splice plate:$tin =$0.5$in"));
            list.Add(string.Format("Width of the inside splice plate:$bin =$6$in"));
            list.Add(string.Format("Thickness of the outside splice plate:$tout =$0.4375$in"));
            list.Add(string.Format("Width of the outside splice plate:$bout =$14$in"));
            list.Add(string.Format("Thickness of the fill plate:$tfill =$0.5$in"));
            list.Add(string.Format("Width of the fill plate:$bfill =$14$in"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Misc_Steel_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step 5.1 - Design Shear Connectors$$$"));
            list.Add(string.Format("height of a stud shear connector$Height_stud =$6$in"));
            list.Add(string.Format("diameter of a stud shear connector$Diameter_stud =$0.875$in"));
            list.Add(string.Format("Concrete 28day compressive strength$f'c$4$ksi"));
            list.Add(string.Format("the effective flange width$b =$103$in"));
            list.Add(string.Format("Minimum deck overhang thickness$ts =$8$in"));
            list.Add(string.Format("Web yield strength:$Fyw =$50$ksi"));
            list.Add(string.Format("Web Depth:$D =$54$in"));
            list.Add(string.Format("Web Thickness:$tw =$0.5$in"));
            list.Add(string.Format("Structural steel yield strength:$Fyt =$50$ksi"));
            list.Add(string.Format("Top Flange Width:$bt =$14$in"));
            list.Add(string.Format("Top Flange Thickness:$tt =$0.875$ft"));
            list.Add(string.Format("Structural steel yield strength:$Fyc =$50$ksi"));
            list.Add(string.Format("Bottom Flange Width:$bf =$14$in"));
            list.Add(string.Format("Bottom Flange Thickness:$tf =$0.625$ft"));
            list.Add(string.Format("Design Step 5.4 - Design Cross-frames$$$"));
            list.Add(string.Format("Girder spacing:$S =$9.75$ft"));
            list.Add(string.Format("Girder depth:$D =$4.9375$ft"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Abutment_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step 7.1 - Obtain Design Criteria$$$"));
            
            list.Add(string.Format("Material properties:$$$"));
            
            list.Add(string.Format("Concrete density:$Wc =$0.15$kcf"));
            
            
            list.Add(string.Format("Concrete 28-day compressive strength:$f'c =$4$ksi"));
            
            
            
            
            list.Add(string.Format("Reinforcement strength:$fy =$60$ksi"));
            
            
            
            list.Add(string.Format("Reinforcing steel cover requirements:$$$"));
            
            list.Add(string.Format("Backwall back cover:$Coverb =$2.50$in"));
            list.Add(string.Format("Stem back cover:$Covers =$2.50$in"));
            list.Add(string.Format("Footing top cover:$Coverft =$2.00$in"));
            list.Add(string.Format("Footing bottom cover:$Coverfb =$3.00$in"));
            
            list.Add(string.Format("Relevant superstructure data:$$$"));
            
            list.Add(string.Format("Girder spacing:$S =$9.75$ft"));
            list.Add(string.Format("Number of girders:$N =$5$nos"));
            list.Add(string.Format("Span length:$Lspan =$120$ft"));
            list.Add(string.Format("Parapet height:$Hpar =$3.5$ft"));
            list.Add(string.Format("Parapet weight (each):$Wpar =$0.53$K/ft"));
            list.Add(string.Format("Out-to-out deck width:$Wdeck =$46.875$ft"));
            
            list.Add(string.Format("Abutment and wingwall height$$$"));
            
            list.Add(string.Format("Abutment stem height:$hstem =$22$ft"));
            
            list.Add(string.Format("Wingwall stem design height :$hwwstem =$20.75$ft"));
            
            list.Add(string.Format("Abutment and wingwall length$$$"));
            
            list.Add(string.Format("Abutment length:$Labut =$46.875$ft"));
            list.Add(string.Format("Wingwall length:$Lwing =$20.5$ft"));
            
            list.Add(string.Format("Design Step 7.4 - Compute Dead Load Effects$$$"));
            list.Add(string.Format("Fascia girder:$$$"));
            
            list.Add(string.Format("$RDCfascia =$69.25$K"));
            list.Add(string.Format("$RDWfascia =$11.24$K"));
            
            list.Add(string.Format("Interior girder:$$$"));
            list.Add(string.Format("$RDCinterior =$73.51$K"));
            list.Add(string.Format("$RDWinterior =$11.24$K"));
            
            
            list.Add(string.Format("Earth dead load:$Ys =$0.12$kcf"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Pier_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step 8.1 - Obtain Design Criteria$$$"));
            
            list.Add(string.Format("Material Properties:$$$"));
            
            list.Add(string.Format("Concrete density:$Wc =$0.15$kcf"));
            
            list.Add(string.Format("Concrete 28-day$f'c =$4$ksi"));
            list.Add(string.Format("compressive strength:$$$"));
            
            list.Add(string.Format("Reinforcement$fy =$60$ksi"));
            list.Add(string.Format("strength:$$$"));
            
            list.Add(string.Format("Reinforcing steel cover requirements (assume non-epoxy rebars):$$$"));
            
            list.Add(string.Format("Pier cap:$Covercp =$2.5$in"));
            
            list.Add(string.Format("Pier column:$Coverco =$2.5$in"));
            
            list.Add(string.Format("Footing top cover:$Coverft =$2$in"));
            
            list.Add(string.Format("Footing bottom cover:$Coverfb =$3$in"));
            
            list.Add(string.Format("Relevant superstructure data:$$$"));
            
            list.Add(string.Format("Girder spacing:$S =$9.75$ft"));
            
            list.Add(string.Format("Number of girders:$N =$5$"));
            
            list.Add(string.Format("Deck overhang:$DOH =$3.9375$ft"));
            
            list.Add(string.Format("Span length:$Lspan =$120$ft"));
            
            list.Add(string.Format("Parapet height:$Hpar =$3.5$ft"));
            
            list.Add(string.Format("Deck overhang thickness:$to =$9$ft"));
            
            list.Add(string.Format("Haunch thickness:$Hhnch =$3.5$in"));
            
            list.Add(string.Format("Web depth:$Do =$66$in"));
            
            list.Add(string.Format("Bot. flange thickness:$tbf =$2.25$in"));
            
            list.Add(string.Format("Bearing height:$Hbrng =$5$in"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Bearing_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step 6.1 - Obtain Design Criteria$$$"));
            list.Add(string.Format("Service I limit state dead load$DLserv =$78.4$K"));
            list.Add(string.Format("Service I limit state live load (including dynamic load allowance)$LLserv =$110.4$K"));
            list.Add(string.Format("Service I limit state total rotation about the transverse axis$Ysx =$0.0121$rad"));
            list.Add(string.Format("Strength limit state minimum vertical force due to permanent loads$Psd =$67.8$K"));
            list.Add(string.Format("Design Step 6.3 - Select Preliminary Bearing Properties$$$"));
            list.Add(string.Format("Bearing Pad Configuration$$$"));
            list.Add(string.Format("Pad length (bridge longitudinal direction):$Lpad =$14$in"));
            list.Add(string.Format("Pad width (bridge transverse direction):$Wpad =$15$in"));
            list.Add(string.Format("Elastomer cover thickness:$hrcover =$0.25$in"));
            list.Add(string.Format("Elastomer internal layer thickness:$hrinternal =$0.375$in"));
            list.Add(string.Format("Number of steel reinforcement layers:$Nstlayers =$9$"));
            list.Add(string.Format("Steel reinforcement thickness:$hreinf =$0.1196$in"));
            list.Add(string.Format("Material Properties$$$"));
            list.Add(string.Format("Elastomer hardness:$HshoreA =$50$"));
            list.Add(string.Format("Elastomer shear modulus:$G =$0.095$ksi"));
            list.Add(string.Format("Elastomer creep deflection at 25 years$$$"));
            list.Add(string.Format("divided by the instantaneous deflection:$Cd =$0.25$"));
            list.Add(string.Format("Steel reinforcement yield strength:$Fy =$50$ksi"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Foundation_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step P.1 - Define Subsurface Conditions and Any Geometric Constraints$$$"));
            list.Add(string.Format("Dry unit weight:$Ydry =$90$PCF"));
            list.Add(string.Format("Wet unit weight:$Ywet =$110$PCF"));
            list.Add(string.Format("Unit weight of water:$Ywater =$62.4$PCF"));
            list.Add(string.Format("Design Step P.4 - Verify Need for a Pile Foundation$$$"));
            list.Add(string.Format("Maximum possible length of footing$L =$120$ft"));
            list.Add(string.Format("Preliminary minimum required width$Bmin =$12.736$ft"));
            list.Add(string.Format("Width of the footing:$Bi =$10.25$TSF"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Process_Deck_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 2 Introduction and Deck Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            //Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["1.0 Input"];
            Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Introduction"];

            //Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(0);


            //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
            //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
            //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

            Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

            eui.Read_From_Grid(dgv);
 
            List<double> data = new List<double>();

            List<string> list = new List<string>();
            #region Ref Cell 
            //list.Add("Introduction");
            list.Add("E142");
            list.Add("E143");
            list.Add("E144");
            list.Add("E145");
            list.Add("E146");
            list.Add("E147");
            list.Add("E148");
            list.Add("E149");
            list.Add("E150");
            list.Add("E151");
            list.Add("E152");
            list.Add("E153");
            list.Add("E154");
            //list.Add("Concrete Deck Design");
            list.Add("E45");
            list.Add("E46");
            list.Add("E47");
            list.Add("E48");
            list.Add("E49");
            list.Add("E50");
            list.Add("E51");
            list.Add("E52");
            list.Add("E56");
            list.Add("E57");
            list.Add("E58");
            list.Add("E59");
            list.Add("E60");
            list.Add("E61");
            list.Add("D97");
            list.Add("D104");
            list.Add("D105");
            list.Add("D161");
            list.Add("D163");
            list.Add("D178");
            list.Add("D180");
            list.Add("D182");
            list.Add("B493");
            list.Add("B494");
            list.Add("B590");
            list.Add("B591");
            #endregion Ref Cells
            string val = "";
            for(int i = 0; i < 13; i++)
            {
                try
                {
                    val = EXL_INP.get_Range(list[i]).Formula.ToString();

                    EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                    //EXL_INP.get_Range(kStr).Formula = item.Text;
                }
                catch (Exception ex)
                {
                }
            }

            EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Concrete Deck Design"];

            for (int i = 13; i < list.Count; i++)
            {
                try
                {
                    val = EXL_INP.get_Range(list[i]).Formula.ToString();

                    EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                    //EXL_INP.get_Range(kStr).Formula = item.Text;
                }
                catch (Exception ex)
                {
                }
            }
            try
            {
                //string kStr = "";
                //foreach (var item in eui)
                //{
                //    try
                //    {

                //        //EXL_INP.get_Range(item.Excel_Cell_Reference).Formula = item.Input_Value;
                //        //EXL_INP.get_Range(kStr).Formula = item.Text;
                //    }
                //    catch(Exception ex)
                //    {
                //    }
                //}
            }
            catch (Exception exx) { }
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_Steel_Section_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 3 Steel Section.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 3.1"];

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];


                #region Ref Cells
                List<string> list = new List<string>();
                //list.Add("STEP 3.1");
                list.Add("G107");
                list.Add("G108");
                list.Add("G109");
                list.Add("G110");
                list.Add("G111");
                list.Add("G112");
                list.Add("G113");
                list.Add("G114");
                list.Add("G115");
                list.Add("G116");
                list.Add("G117");
                list.Add("G118");
                //list.Add("G119");
                list.Add("G124");
                list.Add("G125");
                list.Add("G126");
                list.Add("G127");
                list.Add("G128");
                list.Add("G129");
                list.Add("G130");
                list.Add("G131");
                list.Add("G132");
                list.Add("G133");
                list.Add("G134");
                list.Add("G135");
                list.Add("G136");
                list.Add("G137");
                list.Add("G138");
                #endregion Ref Cells

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();
                if (dgv != null)
                    eui.Read_From_Grid(dgv);

                List<double> data = new List<double>();
                try
                {
                    string kStr = "";
                    for (int i = 0; i < eui.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception ex111) { }
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_Bolted_Field_Splice_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 4 Bolted Field Splice Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);


            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 4.1"];

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                if (dgv != null)
                    eui.Read_From_Grid(dgv);


                #region Ref Cells
                List<string> list = new List<string>();
                //list.Add("STEP 4.1");
                list.Add("C72");
                list.Add("C73");
                list.Add("C77");
                list.Add("C82");
                list.Add("C83");
                list.Add("C84");
                list.Add("C85");
                list.Add("C86");
                list.Add("C87");
                list.Add("C91");
                list.Add("C92");
                list.Add("C93");
                list.Add("C94");
                list.Add("C95");
                list.Add("C96");
                list.Add("C100");
                list.Add("C101");
                list.Add("C104");
                list.Add("C108");
                list.Add("C109");
                list.Add("C110");
                list.Add("C113");
                list.Add("B119");
                list.Add("C132");
                list.Add("C133");
                list.Add("C134");
                list.Add("C135");
                list.Add("C136");
                list.Add("C137");
                list.Add("C138");
                list.Add("C139");
                //list.Add("STEP 4.4");
                list.Add("E29");
                list.Add("E30");
                list.Add("E31");
                list.Add("E32");
                list.Add("E33");
                list.Add("E34");

                #endregion Ref Cells
                List<double> data = new List<double>();
                try
                {

                    string kStr = "";
                    for (int i = 0; i < 31; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 4.4"];

                    for (int i = 31; i < eui.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception ex111) { }
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_Miscellaneous_Steel_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 5 Miscellaneous Steel Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }

            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 5.1"];

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                if (dgv != null)
                    eui.Read_From_Grid(dgv);

                List<string> list = new List<string>();

                #region Ref Cells
                //list.Add("STEP 5.1");
                list.Add("B70");
                list.Add("B71");
                list.Add("B228");
                list.Add("B266");
                list.Add("B267");
                list.Add("B268");
                list.Add("B269");
                list.Add("B270");
                list.Add("B271");
                list.Add("B272");
                list.Add("B273");
                list.Add("B274");
                list.Add("B275");
                list.Add("B276");
                //list.Add("STEP 5.4");
                list.Add("C80");
                list.Add("C81");
                #endregion Ref Cells
                try
                {
                    string kStr = "";
                    for (int i = 0; i < 14; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 5.4"];
                    
                    for (int i = 14; i < list.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception exx) { }
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_Bearing_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {

            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 6 Bearing Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            try
            {
                Excel.Worksheet EXL_INP;

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                if (dgv != null)
                    eui.Read_From_Grid(dgv);

                List<string> list = new List<string>();
                //list.Add("STEP 6.1");
                list.Add("B26");
                list.Add("B27");
                list.Add("B30");
                list.Add("B33");
                //list.Add("STEP 6.3");
                list.Add("E10");
                list.Add("E12");
                list.Add("E14");
                list.Add("E16");
                list.Add("E18");
                list.Add("E20");
                list.Add("E24");
                list.Add("E25");
                list.Add("E28");
                list.Add("E32");
                try
                {
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 6.1"];

                    string kStr = "";
                    for (int i = 0; i < 4; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 6.3"];

                    for (int i = 4; i < list.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
             }
            catch (Exception ex) { }



            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
       
        }
        public static void Process_Abutment_and_Wingwall_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {

            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 7 Abutment and Wingwall Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            try
            {
                myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                try
                {
                    //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                    Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 7.1"];

                    //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                    //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                    //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                    Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                    eui.Read_From_Grid(dgv);

                    List<string> list = new List<string>();
                    //list.Add("STEP 7.1");
                    list.Add("B42");
                    list.Add("B46");
                    list.Add("B51");
                    list.Add("D55");
                    list.Add("D56");
                    list.Add("D57");
                    list.Add("D58");
                    list.Add("D76");
                    list.Add("D77");
                    list.Add("D78");
                    list.Add("D79");
                    list.Add("D80");
                    list.Add("D81");
                    list.Add("E88");
                    list.Add("E90");
                    list.Add("D95");
                    list.Add("D96");
                    //list.Add("STEP 7.4");
                    list.Add("B16");
                    list.Add("B17");
                    list.Add("B21");
                    list.Add("B22");
                    list.Add("B52");
                    try
                    {
                        string kStr = "";
                        for (int i = 0; i < 17; i++)
                        {
                            try
                            {
                                kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                                double d = double.Parse(kStr);
                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 7.4"];

                        for (int i = 17; i < 22; i++)
                        {
                            try
                            {
                                kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                                double d = double.Parse(kStr);
                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception exx) { }
                }
                catch (Exception exx1) { }

                iApp.Excel_Close_Message();
                myExcelWorkbook.Save();
                MyList.releaseObject(myExcelWorkbook);
            }
            catch (Exception exx) { }
        }
        public static void Process_Pier_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {

            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 8 Pier Design Example.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 8.1"];

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                eui.Read_From_Grid(dgv);

                List<string> list = new List<string>();
                //list.Add("Step 8.1");
                list.Add("E28");
                list.Add("E30");
                list.Add("E33");
                list.Add("E43");
                list.Add("E45");
                list.Add("E47");
                list.Add("E49");
                list.Add("E64");
                list.Add("E66");
                list.Add("E68");
                list.Add("E70");
                list.Add("E72");
                list.Add("E74");
                list.Add("E76");
                list.Add("E78");
                list.Add("E80");
                list.Add("E82");
                try
                {
                    string kStr = "";
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            var ss = eui[i].Input_Value;
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception exx) { }

            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_Pile_Foundation_Design_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {

            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step P Pile Foundation Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP;

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                eui.Read_From_Grid(dgv);

                List<string> list = new List<string>();
                //list.Add("Step P.1");
                list.Add("E114");
                list.Add("E117");
                list.Add("E120");
                //list.Add("Step P.4");
                list.Add("D33");
                list.Add("D37");
                list.Add("D61");
                try
                {
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step P.1"];
                    string kStr = "";
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            var ss = eui[i].Input_Value;
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step P.4"];
                    for (int i = 3; i < list.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception exx) { }
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        #endregion Set_Input Data


    }

    public class Composite_Inputs
    {
        public Composite_Inputs() { }
        public TextBox txt_LSpan;
        public double Lspan { get { return MyList.StringToDouble(txt_LSpan); } set { txt_LSpan.Text = value.ToString(); } }
        public TextBox txt_LTotal;

        public double Nspans;
        public double LTotal
        {
            get
            {
                string kStr = txt_LTotal.Text.Replace(","," ");
                kStr = MyList.RemoveAllSpaces(kStr);
                MyList ml = new MyList(kStr, ' ');
                Nspans = ml.Count;
                return ml.SUM;
            }
            set
            {
                //txt_LTotal.Text = value.ToString();
            }
        }
        public TextBox txt_wdeck;
        public double wdeck { get { return MyList.StringToDouble(txt_wdeck); } set { txt_wdeck.Text = value.ToString(); } }

        public TextBox txt_wroadway;
        public double wroadway { get { return MyList.StringToDouble(txt_wroadway); } set { txt_wroadway.Text = value.ToString(); } }

        public ComboBox cmb_N;
        public double N { get { return MyList.StringToDouble(cmb_N.Text, 5.0); } set { cmb_N.Text = value.ToString(); } }

        public TextBox txt_Soverhang;
        public double Soverhang { get { return MyList.StringToDouble(txt_Soverhang); } set { txt_Soverhang.Text = value.ToString(); } }

        public TextBox txt_Skew;
        public double Skew { get { return MyList.StringToDouble(txt_Skew); } set { txt_Skew.Text = value.ToString(); } }
        public TextBox txt_S;
        public double S { get { return MyList.StringToDouble(txt_S); } set { txt_S.Text = value.ToString(); } }
        public TextBox txt_tdeck;
        public double tdeck { get { return MyList.StringToDouble(txt_tdeck); } set { txt_tdeck.Text = value.ToString(); } }
        public TextBox txt_Covert;
        public double Covert { get { return MyList.StringToDouble(txt_Covert); } set { txt_Covert.Text = value.ToString(); } }
        public TextBox txt_Coverb;
        public double Coverb { get { return MyList.StringToDouble(txt_Coverb); } set { txt_Coverb.Text = value.ToString(); } }
        public TextBox txt_Fy;
        public double Fy { get { return MyList.StringToDouble(txt_Fy); } set { txt_Fy.Text = value.ToString(); } }
        public TextBox txt_Fu;
        public double Fu { get { return MyList.StringToDouble(txt_Fu); } set { txt_Fu.Text = value.ToString(); } }
        public TextBox txt_fc;
        public double fc { get { return MyList.StringToDouble(txt_fc); } set { txt_fc.Text = value.ToString(); } }
        public TextBox txt_fys;
        public double fys { get { return MyList.StringToDouble(txt_fys); } set { txt_fys.Text = value.ToString(); } }
        public TextBox txt_Ws;
        public double Ws { get { return MyList.StringToDouble(txt_Ws); } set { txt_Ws.Text = value.ToString(); } }
        public TextBox txt_Wc;
        public double Wc { get { return MyList.StringToDouble(txt_Wc); } set { txt_Wc.Text = value.ToString(); } }
       
        public TextBox txt_Wpar;
        public double Wpar { get { return MyList.StringToDouble(txt_Wpar); } set { txt_Wpar.Text = value.ToString(); } }
        public TextBox txt_Wmisc;
        public double Wmisc { get { return MyList.StringToDouble(txt_Wmisc); } set { txt_Wmisc.Text = value.ToString(); } }
        public TextBox txt_Ec;
        public double Ec { get { return MyList.StringToDouble(txt_Ec); } set { txt_Ec.Text = value.ToString(); } }

        public TextBox txt_Es;
        public double Es { get { return MyList.StringToDouble(txt_Es); } set { txt_Es.Text = value.ToString(); } }
        public TextBox txt_n;
        public double n { get { return MyList.StringToDouble(txt_n); } set { txt_n.Text = value.ToString(); } }
        public TextBox txt_tfws;
        public double tfws { get { return MyList.StringToDouble(txt_tfws); } set { txt_tfws.Text = value.ToString(); } }
        public TextBox txt_wfws;
        public double wfws { get { return MyList.StringToDouble(txt_wfws); } set { txt_wfws.Text = value.ToString(); } }
        public TextBox txt_wbase;
        public double wbase { get { return MyList.StringToDouble(txt_wbase); } set { txt_wbase.Text = value.ToString(); } }
        public TextBox txt_Hpar;
        public double Hpar { get { return MyList.StringToDouble(txt_Hpar); } set { txt_Hpar.Text = value.ToString(); } }


    }
    public class Excel_AASHTO_Data : List<Excel_User_Input_Data>
    {
        public Excel_AASHTO_Data()
            : base()
        {

        }
       
        public void Read_From_Grid(DataGridView dgv)
        {
            this.Clear();

            Excel_User_Input_Data di = new Excel_User_Input_Data();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    di = new Excel_User_Input_Data();

                    di.Input_Text = dgv[0, i].Value.ToString() + "=" + dgv[1, i].Value.ToString() + "=";
                    di.Input_Value = dgv[2, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", "");
                    di.Input_Unit = dgv[3, i].Value.ToString();
                    if (di.Input_Value != "")
                        this.Add(di);
                }
                catch (Exception ex) { }
            }
        }
    }
}
