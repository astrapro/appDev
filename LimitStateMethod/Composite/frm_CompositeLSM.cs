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
//using LimitStateMethod.LS_Progress;


namespace LimitStateMethod.Composite
{
    public partial class frm_CompositeLSM : Form
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



        Composite_LS_Analysis Bridge_Analysis = null;


        Composite_LS_StraightAnalysis Curve_Analysis = null;


        IApplication iApp = null;
        Composite_Girder_LS Deck = null;
        LS_DeckSlab_Analysis Deck_Analysis = null;

        //Chiranjit [2012 06 25]
        CantileverSlab Cant = null;
        //Chiranjit [2012 06 08]
        RccPier rcc_pier = null;

        //Chiranjit [2012 05 27]
        RCC_AbutmentWall Abut = null;


        Steel_Girder_Section steel_section;

        CompositeSection Comp_sections { get; set; }

        public List<string> Results { get; set; }
        public frm_CompositeLSM(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            IsCreate_Data = true;
            Results = new List<string>();
            steel_section = new Steel_Girder_Section();
            Comp_sections = new CompositeSection();
            //user_path = Path.Combine(user_path, Title);

            //if (!Directory.Exists(user_path))
            //    Directory.CreateDirectory(user_path);

            //Deck.tbl_rolledSteelAngles = new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES"));

        }

        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
            }
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

            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 2.75, 0.2, 1.179);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 6.25, 0.2, 1.179);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 9.75, 0.2, 1.179);
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

                string usp = Path.Combine(user_path, "Steel Girder Analysis");

                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);

                if (Bridge_Analysis == null)
                    Bridge_Analysis = new Composite_LS_Analysis(iApp);

                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");

                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);

                txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;

                Bridge_Analysis.Steel_Section = Comp_sections;


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);


                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);

                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File);
                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);

                Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);
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
                    //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    //if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);

                    //user_path = Path.Combine(user_path, txt_project_name.Text);

                }
                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);

                Write_All_Data();

                string inp_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                string usp = Path.Combine(user_path, "Steel Girder Analysis");

                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);


                //Calculate_Load_Computation();
                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;
                if (iApp.DesignStandard == eDesignStandard.IndianStandard ||
                    iApp.DesignStandard == eDesignStandard.LRFDStandard
                    )
                {

                    #region Write Straight Input File
                    #endregion  Composite Analysis Form Events


                    if (Curve_Radius > 0)
                    {

                        Bridge_Analysis.CreateData_Straight_Indian();




                        //Bridge_Analysis.WriteData_Orthotropic_Analysis("", false);



                        #region Chiranjit [2014 09 08] Indian Standard


                        Bridge_Analysis.Steel_Section = Comp_sections;


                        Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                            Bridge_Analysis.Inner_Girders_as_String,
                            Bridge_Analysis.joints_list_for_load);

                        Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.Straight_DL_File);
                        Ana_Write_Load_Data(Bridge_Analysis.Straight_DL_File, false, true);


                        Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Straight_LL_File);
                        Ana_Write_Load_Data(Bridge_Analysis.Straight_LL_File, true, false);


                        Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Straight_TL_File);
                        Ana_Write_Load_Data(Bridge_Analysis.Straight_TL_File, true, true);


                        #endregion

                        Bridge_Analysis.CreateData_Indian();
                    }
                    else
                    {
                        Bridge_Analysis.CreateData_Straight_Indian();
                    }

                    #region Chiranjit [2014 09 08] Indian Standard

                    Bridge_Analysis.WriteData(inp_file);
                    Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);

                    //Ana_Write_Load_Data();
                    txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;

                    Bridge_Analysis.Steel_Section = Comp_sections;


                    Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                        Bridge_Analysis.Inner_Girders_as_String,
                        Bridge_Analysis.joints_list_for_load);


                    Bridge_Analysis.WriteData_Total_Analysis(inp_file);
                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);

                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File);
                    Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);




                    Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                    Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                    Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                    Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);

                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                    string ll_txt = Bridge_Analysis.LiveLoad_File;

                    Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                    if (Bridge_Analysis.Live_Load_List == null) return;

                    Button_Enable_Disable();

                    MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion Chiranjit [2014 09 08]

                }
                else
                {
                    #region Chiranjit [2014 09 08] British Standard

                    Bridge_Analysis.HA_Lanes = HA_Lanes;
                    LONG_GIRDER_BRITISH_LL_TXT();



                    //Bridge_Analysis.CreateData_British();



                    if (Curve_Radius > 0)
                    {

                        Bridge_Analysis.CreateData_StraightBritish();

                        #region Chiranjit [2014 09 08] Indian Standard


                        Bridge_Analysis.Steel_Section = Comp_sections;


                        Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                            Bridge_Analysis.Inner_Girders_as_String,
                            Bridge_Analysis.joints_list_for_load);

                        Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.Straight_DL_File);
                        Ana_Write_Load_Data(Bridge_Analysis.Straight_DL_File, false, true);


                        Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Straight_LL_File);
                        Ana_Write_Load_Data(Bridge_Analysis.Straight_LL_File, true, false);


                        Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Straight_TL_File);
                        Ana_Write_Load_Data(Bridge_Analysis.Straight_TL_File, true, true);






                        if (rbtn_HA.Checked)
                        {
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_DL_File, false, true, 0);
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_TL_File, true, true, 0);
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_LL_File, true, false, 0);

                        }
                        else
                        {
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_DL_File, false, true, 1);
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_TL_File, true, true, 1);
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Straight_LL_File, true, false, 1);
                        }


                        for (int i = 0; i < all_loads.Count; i++)
                        {
                            Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), long_ll);
                            Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1, true), true, false, i + 1);
                        }
                        #endregion

                        Bridge_Analysis.CreateData_British();
                    }
                    else
                    {
                        Bridge_Analysis.CreateData_StraightBritish();
                    }

                    Bridge_Analysis.WriteData(inp_file);
                    //Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);
                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File, true, long_ll);

                    //Ana_Write_Load_Data();
                    txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;

                    Bridge_Analysis.Steel_Section = Comp_sections;


                    Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                        Bridge_Analysis.Inner_Girders_as_String,
                        Bridge_Analysis.joints_list_for_load);


                    Bridge_Analysis.WriteData_Total_Analysis(inp_file, false, true);
                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File, false, false);

                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File, true, long_ll);
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, long_ll);
                    Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);

                    Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);



                    //Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                    //Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                    //Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);


                    //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 1);
                    //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false, 1);

                    //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, false, true 1);



                    if (rbtn_HA.Checked)
                    {
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true, 0);
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 0);
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false, 0);

                    }
                    else
                    {
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true, 1);
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 1);
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false, 1);
                    }


                    for (int i = 0; i < all_loads.Count; i++)
                    {
                        Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1), long_ll);
                        Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i + 1), true, false, i + 1);
                    }
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                    string ll_txt = Bridge_Analysis.LiveLoad_File;

                    Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                    if (Bridge_Analysis.Live_Load_List == null) return;

                    Button_Enable_Disable();

                    MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion Chiranjit [2014 09 08] British Standard

                }
                cmb_long_open_file.SelectedIndex = 0;


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



            Button_Enable_Disable();
            Write_All_Data(false);

        }


        private void btn_Ana_view_report_Click(object sender, EventArgs e)
        {
            //frm_Result_Option frm = new frm_Result_Option(true);
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //        if (frm.Is_Full_Analysis_Report)
            //            iApp.RunExe(Bridge_Analysis.Analysis_Report);
            //        else
            //            iApp.RunExe(Result_Report);

            //}

            frm_Result_Option frm = new frm_Result_Option(true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //if (frm.Is_Full_Analysis_Report)
                //    iApp.RunExe(Bridge_Analysis.Total_Analysis_Report);
                //else
                //    iApp.RunExe(Result_Report);
                switch (frm.ResultOption)
                {
                    case frm_Result_Option.eResult_Option.Analysis_Result:
                        iApp.RunExe(Result_Report);
                        break;
                    case frm_Result_Option.eResult_Option.Dead_Load_Analysis_Report:
                        iApp.RunExe(Bridge_Analysis.DeadLoad_Analysis_Report);
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

            Button btn = sender as Button;

            #region Set File Name
            if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
            {
                file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);
            }
            else
            {
                //file_name = File_Long_Girder_Results;
            }
            #endregion Set File Name

            string st_file = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex, true);

            if (Curve_Radius > 0)
            {
                string rad_file = Path.Combine(Path.GetDirectoryName(file_name), "radius.fil");
                //Environment.SetEnvironmentVariable("MOVINGLOAD", Bridge_Analysis.Straight_LL_File);
                Environment.SetEnvironmentVariable("MOVINGLOAD", st_file);
                File.WriteAllText(rad_file, Curve_Radius.ToString());
                //Environment.SetEnvironmentVariable("MOVINGLOAD", Bridge_Analysis.Straight_LL_File);
                Environment.SetEnvironmentVariable("COMP_RAD", Curve_Radius.ToString());

            }
            else
            {
                string rad_file = Path.Combine(Path.GetDirectoryName(file_name), "radius.fil");

                if (File.Exists(rad_file)) File.Delete(rad_file);
                Environment.SetEnvironmentVariable("COMP_RAD", "");
            }
            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name)
            {
                iApp.View_Input_File(file_name);
                //if (File.Exists(ll_txt))
                //    iApp.RunExe(ll_txt);
                //if (File.Exists(file_name))
                //    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                //if (File.Exists(file_name))
                //    iApp.OpenWork(file_name, false);

                //Chiranjit [2017 10 22]
                //Open File with Text Data Workspace
                if (File.Exists(file_name))
                {
                    //if(cmb_ana)

                    Form f = null;
                    if (chk_curve.Checked)
                    {
                      f =  iApp.Form_ASTRA_TEXT_Data(file_name);
                    }
                    else
                    {
                        f = iApp.Form_ASTRA_TEXT_Data(file_name, false);
                    }
                    f.Owner = this;
                    f.Show();
                }

            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);


                frmCompositeResults fm = new frmCompositeResults();
                if (fm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    if (fm.ResultOption == frmCompositeResults.eCompositeResults.Open_Analysis_Reports)
                    {
                        if (File.Exists(file_name))
                            iApp.RunExe(file_name);
                    }
                    else if (fm.ResultOption == frmCompositeResults.eCompositeResults.Read_Analysis_Results)
                    {
                        Clear_All_Results();
                        Read_Analysis_Results();
                    }
                    else if (fm.ResultOption == frmCompositeResults.eCompositeResults.Open_Design_Forces)
                    {
                        if (File.Exists(Result_Report)) iApp.RunExe(Result_Report);
                    }
                }
            }
            else if (btn.Name == btn_View_Moving_Load.Name)
            {
                //file_name = MyList.Get_Analysis_Report_File(file_name);
                string ll = MyList.Get_LL_TXT_File(file_name);
                if (!File.Exists(MyList.Get_LL_TXT_File(file_name)))
                {
                    MessageBox.Show("Moving Load Data not found in the input file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (File.Exists(file_name))
                    {
                        //iApp.OpenWork(file_name, true);
                        //iApp.Form_ASTRA_Moving_Load(file_name).Show();


                        Form f = iApp.Form_ASTRA_Moving_Load(file_name);
                        f.Owner = this;
                        f.Show();
                    }
                }
            }
        }

        private void Read_Analysis_Results()
        {

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                Bridge_Analysis.HA_Lanes = HA_Lanes;
                LONG_GIRDER_BRITISH_LL_TXT();
            }

            int i = 0;
            iApp.Progress_Works.Clear();
            do
            {

                string flPath = "";
                if (i == 0)
                {
                    flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;
                }
                else if (i == 1)
                {
                    flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                }
                else if (i == 2)
                {
                    flPath = Bridge_Analysis.LiveLoadAnalysis_Input_File;
                }
                else if (i > 2)
                {
                    //flPath = Bridge_Analysis.GetAnalysis_Input_File(i);
                    flPath = Bridge_Analysis.Get_Live_Load_Analysis_Input_File(i - 2);
                }
                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                i++;
            }
            while (i < (3 + all_loads.Count));



            #region Read Analysis Result
            Bridge_Analysis.Structure = null;
            //Clear_All_Results();

            Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);

            BridgeMemberAnalysis LL_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.LiveLoad_Analysis_Report);
            BridgeMemberAnalysis DL_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);



            if (all_loads.Count > 0)
            {
                for (i = 0; i < all_loads.Count; i++)
                {
                    Bridge_Analysis.All_Analysis.Add(new BridgeMemberAnalysis(iApp,
                        MyList.Get_Analysis_Report_File(Bridge_Analysis.GetAnalysis_Input_File(i + 3))));

                }
            }
            else
            {
                Bridge_Analysis.All_Analysis.Add(Bridge_Analysis.Structure);
                Bridge_Analysis.All_Analysis.Add(LL_Analysis);
                Bridge_Analysis.All_Analysis.Add(DL_Analysis);
            }

            Bridge_Analysis.LL_Analysis = LL_Analysis;
            Bridge_Analysis.DL_Analysis = DL_Analysis;

            Show_Moment_Shear();
            Change_LSM_Data();

            string s1 = "";
            string s2 = "";



            s1 = Bridge_Analysis.support_left_joints;
            s2 = Bridge_Analysis.support_right_joints;

            //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
            double BB = B;


            NodeResultData nrd = Bridge_Analysis.Structure.Node_Displacements.Get_Max_Deflection();
            NodeResultData LL_nrd = LL_Analysis.Node_Displacements.Get_Max_Deflection();
            NodeResultData DL_nrd = DL_Analysis.Node_Displacements.Get_Max_Deflection();


            txt_LL_node_displace.Text = LL_nrd.ToString();
            txt_res_LL_node_trans.Text = LL_nrd.Max_Translation.ToString();
            txt_res_LL_node_trans_jn.Text = LL_nrd.NodeNo.ToString();
            txt_res_LL_node_trans_ld.Text = LL_nrd.LoadCase.ToString();

            txt_DL_node_displace.Text = DL_nrd.ToString();
            txt_res_DL_node_trans.Text = DL_nrd.Max_Translation.ToString();
            txt_res_DL_node_trans_jn.Text = DL_nrd.NodeNo.ToString();
            txt_res_DL_node_trans_ld.Text = DL_nrd.LoadCase.ToString();



            frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));


            frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);


            #region Print Results
            Results.Add(string.Format(""));
            Results.Add(string.Format("Support Reaction [DL]"));
            Results.Add(string.Format("---------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("(Forces are to be used in Design of Abutment)"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Maximum Vertical Reaction = {0} Ton", txt_max_vert_reac.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Maximum Bending Moment [Mx] = {0} Ton-m", txt_max_Mx.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Maximum Bending Moment [Mz] = {0} Ton-m", txt_max_Mz.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("(Forces are to be used in Design of Pier)"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Vertical Reaction = {0} Ton", txt_final_vert_reac.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Bending Moment [Mx] = {0} Ton-m", txt_final_Mx.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Bending Moment [Mz] = {0} Ton-m", txt_final_Mz.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Support Reaction [SIDL]"));
            Results.Add(string.Format("------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("(Forces are to be used in Design of Pier)"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Vertical Reaction = {0} Ton", txt_sidl_final_vert_reac.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Bending Moment [Mx] = {0} Ton-m", txt_sidl_final_Mx.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Bending Moment [Mz] = {0} Ton-m", txt_sidl_final_Mz.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Support Reaction [LL]"));
            Results.Add(string.Format("------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Vertical Reaction = {0} Ton", txt_live_vert_rec_Ton.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Support Reaction [DL AND LL]"));
            Results.Add(string.Format("-----------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Dead Load Support Reaction per unit width of Abutment/Pier = {0} kN/m", txt_ana_DLSR.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Live Load Support Reaction per unit width of Abutment/Pier = {0} kN/m", txt_ana_LLSR.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Bearing Forces"));
            Results.Add(string.Format("-----------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Maximum Vertical Reaction (DL + SIDL + LL) = {0} Ton", txt_brg_max_VR_Ton.Text));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Minimum Vertical Reaction (DL + SIDL) = {0} Ton", txt_brg_max_DL_Ton.Text));
            if (chk_curve.Checked)
            {
                Results.Add(string.Format(""));
                Results.Add(string.Format("Maximum Horizontal Reaction (Trans. Direction) = {0} Ton", txt_brg_max_HRT_Ton.Text));
                Results.Add(string.Format(""));
                Results.Add(string.Format("Maximum Horizontal Reaction (Long. Direction) = {0} Ton", txt_brg_max_HRL_Ton.Text));
                Results.Add(string.Format(""));
            }
            Results.Add(string.Format(""));


            File.WriteAllLines(Result_Report, Results.ToArray());
            #endregion

            txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
            txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

            txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
            txt_ana_MSLD.Text = txt_final_Mx_kN.Text;
            txt_ana_MSTD.Text = txt_final_Mz_kN.Text;

            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            txt_RCC_Pier_Mx1.Text = txt_final_Mx_kN.Text;
            txt_RCC_Pier_Mz1.Text = txt_final_Mz_kN.Text;

            txt_abut_w6.Text = Total_LiveLoad_Reaction;
            txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
            txt_abut_w6.ForeColor = Color.Red;

            txt_abut_w5.Text = Total_DeadLoad_Reaction;
            txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
            txt_abut_w5.ForeColor = Color.Red;


            Deck_Load_Analysis_Data();
            Deck_Initialize_InputData();
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

            txt_LL_node_displace.Text = "";
            txt_res_LL_node_trans.Text = "";
            txt_res_LL_node_trans_jn.Text = "";
            txt_res_LL_node_trans_ld.Text = "";

            txt_DL_node_displace.Text = "";
            txt_res_DL_node_trans.Text = "";
            txt_res_DL_node_trans_jn.Text = "";
            txt_res_DL_node_trans_ld.Text = "";

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

            txt_ana_TSRP.Text = "";
            txt_ana_MSLD.Text = "";
            txt_ana_MSTD.Text = "";

            


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



            txt_SUMM_I13.Text = "";
            txt_SUMM_J13.Text = "";
            txt_SUMM_K13.Text = "";
            txt_SUMM_L13.Text = "";
            txt_SUMM_M13.Text = "";
            txt_SUMM_N13.Text = "";



            txt_SUMM_I15.Text = "";
            txt_SUMM_J15.Text = "";
            txt_SUMM_K15.Text = "";
            txt_SUMM_L15.Text = "";
            txt_SUMM_M15.Text = "";
            txt_SUMM_N15.Text = "";



            txt_SUMM_I16.Text = "";
            txt_SUMM_J16.Text = "";
            txt_SUMM_K16.Text = "";
            txt_SUMM_L16.Text = "";
            txt_SUMM_M16.Text = "";
            txt_SUMM_N16.Text = "";


            txt_SUMM_I17.Text = "";
            txt_SUMM_J17.Text = "";
            txt_SUMM_K17.Text = "";
            txt_SUMM_L17.Text = "";
            txt_SUMM_M17.Text = "";
            txt_SUMM_N17.Text = "";



            txt_SUMM_I21.Text = "";
            txt_SUMM_J21.Text = "";
            txt_SUMM_K21.Text = "";
            txt_SUMM_L21.Text = "";
            txt_SUMM_M21.Text = "";
            txt_SUMM_N21.Text = "";


            txt_SUMM_I73.Text = "";
            txt_SUMM_J73.Text = "";
            txt_SUMM_K73.Text = "";
            txt_SUMM_L73.Text = "";
            txt_SUMM_M73.Text = "";
            txt_SUMM_N73.Text = "";


            txt_SUMM_I75.Text = "";
            txt_SUMM_J75.Text = "";
            txt_SUMM_K75.Text = "";
            txt_SUMM_L75.Text = "";
            txt_SUMM_M75.Text = "";
            txt_SUMM_N75.Text = "";



            txt_SUMM_I76.Text = "";
            txt_SUMM_J76.Text = "";
            txt_SUMM_K76.Text = "";
            txt_SUMM_L76.Text = "";
            txt_SUMM_M76.Text = "";
            txt_SUMM_N76.Text = "";


            txt_SUMM_I77.Text = "";
            txt_SUMM_J77.Text = "";
            txt_SUMM_K77.Text = "";
            txt_SUMM_L77.Text = "";
            txt_SUMM_M77.Text = "";
            txt_SUMM_N77.Text = "";



            txt_SUMM_I81.Text = "";
            txt_SUMM_J81.Text = "";
            txt_SUMM_K81.Text = "";
            txt_SUMM_L81.Text = "";
            txt_SUMM_M81.Text = "";
            txt_SUMM_N81.Text = "";

            txt_Ana_inner_long_L2_shear.Text = "";
            txt_Ana_inner_long_L2_shear_joint_no.Text = "";
            txt_Ana_inner_long_L2_shear_mem_no.Text = "";
            txt_Ana_inner_long_L2_shear_load_case.Text = "";

            txt_Ana_inner_long_L2_moment.Text = "";
            txt_Ana_inner_long_L2_moment_joint_no.Text = "";
            txt_Ana_inner_long_L2_moment_mem_no.Text = "";
            txt_Ana_inner_long_L2_moment_load_case.Text = "";


            txt_Ana_inner_long_L4_shear.Text = "";
            txt_Ana_inner_long_L4_shear_joint_no.Text = "";
            txt_Ana_inner_long_L4_shear_mem_no.Text = "";
            txt_Ana_inner_long_L4_shear_load_case.Text = "";

            txt_Ana_inner_long_L4_moment.Text = "";
            txt_Ana_inner_long_L4_moment_joint_no.Text = "";
            txt_Ana_inner_long_L4_moment_mem_no.Text = "";
            txt_Ana_inner_long_L4_moment_load_case.Text = "";

            txt_Ana_inner_long_deff_shear.Text = "";
            txt_Ana_inner_long_deff_shear_joint_no.Text = "";
            txt_Ana_inner_long_deff_shear_mem_no.Text = "";
            txt_Ana_inner_long_deff_shear_load_case.Text = "";

            txt_Ana_inner_long_deff_moment.Text = "";
            txt_Ana_inner_long_deff_moment_joint_no.Text = "";
            txt_inner_long_deff_moment_mem_no.Text = "";
            txt_inner_long_deff_moment_load_case.Text = "";



            txt_Ana_outer_long_L2_shear.Text = "";
            txt_Ana_outer_long_L2_shear_joint_no.Text = "";
            txt_Ana_outer_long_L2_shear_mem_no.Text = "";
            txt_Ana_outer_long_L2_shear_load_case.Text = "";

            txt_Ana_outer_long_L2_moment.Text = "";
            txt_Ana_outer_long_L2_moment_joint_no.Text = "";
            txt_Ana_outer_long_L2_moment_mem_no.Text = "";
            txt_Ana_outer_long_L2_moment_load_case.Text = "";

            txt_Ana_outer_long_L4_shear.Text = "";
            txt_Ana_outer_long_L4_shear_joint_no.Text = "";
            txt_Ana_outer_long_L4_shear_mem_no.Text = "";
            txt_Ana_outer_long_L4_shear_load_case.Text = "";

            txt_Ana_outer_long_L4_moment.Text = "";
            txt_Ana_outer_long_L4_moment_joint_no.Text = "";
            txt_Ana_outer_long_L4_moment_mem_no.Text = "";
            txt_Ana_outer_long_L4_moment_load_case.Text = "";

            txt_Ana_outer_long_deff_shear.Text = "";
            txt_Ana_outer_long_deff_shear_joint_no.Text = "";
            txt_Ana_outer_long_deff_shear_mem_no.Text = "";
            txt_Ana_outer_long_deff_shear_load_case.Text = "";

            txt_Ana_outer_long_deff_moment.Text = "";
            txt_Ana_outer_long_deff_moment_joint_no.Text = "";
            txt_Ana_outer_long_deff_moment_mem_no.Text = "";
            txt_Ana_outer_long_deff_moment_load_case.Text = "";

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
            try
            {
                if (!Check_Project_Folder()) return;

                #region Process
                int i = 0;
                //Chiranjit [2012 07 13]
                Write_All_Data();
                

                if(iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    LONG_GIRDER_BRITISH_LL_TXT();
                }
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
                        flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;

                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Straight_DL_File;


                    }
                    else if (i == 1)
                    {
                        flPath = Bridge_Analysis.TotalAnalysis_Input_File;

                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Straight_TL_File;

                    }
                    else if (i == 2)
                    {
                        flPath = Bridge_Analysis.LiveLoadAnalysis_Input_File;


                        pd.IS_Stage_File = true;
                        pd.Stage_File_Name = Bridge_Analysis.Straight_LL_File;

                    }
                    else if (i > 2)
                    {
                        //flPath = Bridge_Analysis.GetAnalysis_Input_File(i);
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
                        pd.IS_Stage_File = true;
                    }
                    //pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");

                    i++;


                }
                while (i < (3 + all_loads.Count));


                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                if (iApp.Show_and_Run_Process_List(pcol))
                    Read_Analysis_Results();


                if(false)
                {
                    //iApp.Progress_Works.Clear();
                    //iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File (ANALYSIS_REP.TXT)");

                    Bridge_Analysis.Structure = null;
                    //Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.Total_Analysis_Report);
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);

                    BridgeMemberAnalysis LL_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.LiveLoad_Analysis_Report);
                    BridgeMemberAnalysis DL_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);



                    if (all_loads.Count > 0)
                    {
                        for (i = 0; i < all_loads.Count; i++)
                        {
                            Bridge_Analysis.All_Analysis.Add(new BridgeMemberAnalysis(iApp,
                                MyList.Get_Analysis_Report_File(Bridge_Analysis.GetAnalysis_Input_File(i + 3))));

                        }
                    }
                    else
                    {
                        Bridge_Analysis.All_Analysis.Add(Bridge_Analysis.Structure);
                        Bridge_Analysis.All_Analysis.Add(LL_Analysis);
                        Bridge_Analysis.All_Analysis.Add(DL_Analysis);
                    }

                    Bridge_Analysis.LL_Analysis = LL_Analysis;
                    Bridge_Analysis.DL_Analysis = DL_Analysis;







                    Show_Moment_Shear();
                    Change_LSM_Data();

                    string s1 = "";
                    string s2 = "";
                    //for (i = 0; i < Bridge_Analysis.Structure.Supports.Count; i++)
                    //{
                    //    if (i < Bridge_Analysis.Structure.Supports.Count / 2)
                    //    {
                    //        if (i == Bridge_Analysis.Structure.Supports.Count / 2 - 1)
                    //        {
                    //            s1 += Bridge_Analysis.Structure.Supports[i].NodeNo;
                    //        }
                    //        else
                    //            s1 += Bridge_Analysis.Structure.Supports[i].NodeNo + ",";
                    //    }
                    //    else
                    //    {
                    //        if (i == Bridge_Analysis.Structure.Supports.Count - 1)
                    //        {
                    //            s2 += Bridge_Analysis.Structure.Supports[i].NodeNo;
                    //        }
                    //        else
                    //            s2 += Bridge_Analysis.Structure.Supports[i].NodeNo + ",";
                    //    }
                    //}


                    s1 = Bridge_Analysis.support_left_joints;
                    s2 = Bridge_Analysis.support_right_joints;

                    //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
                    double BB = B;


                    NodeResultData nrd = Bridge_Analysis.Structure.Node_Displacements.Get_Max_Deflection();//Chiranjit [2013 06 27]
                    NodeResultData LL_nrd = LL_Analysis.Node_Displacements.Get_Max_Deflection();//Chiranjit [2013 07 05]
                    NodeResultData DL_nrd = DL_Analysis.Node_Displacements.Get_Max_Deflection();//Chiranjit [2013 07 05]

                    //txt_TL_node_displace.Text = nrd.ToString();
                    //txt_res_TL_node_trans.Text = nrd.Max_Translation.ToString();
                    //txt_res_TL_node_trans_jn.Text = nrd.NodeNo.ToString();
                    //txt_res_TL_node_trans_ld.Text = nrd.LoadCase.ToString();


                    txt_LL_node_displace.Text = LL_nrd.ToString();
                    txt_res_LL_node_trans.Text = LL_nrd.Max_Translation.ToString();
                    txt_res_LL_node_trans_jn.Text = LL_nrd.NodeNo.ToString();
                    txt_res_LL_node_trans_ld.Text = LL_nrd.LoadCase.ToString();

                    txt_DL_node_displace.Text = DL_nrd.ToString();
                    txt_res_DL_node_trans.Text = DL_nrd.Max_Translation.ToString();
                    txt_res_DL_node_trans_jn.Text = DL_nrd.NodeNo.ToString();
                    txt_res_DL_node_trans_ld.Text = DL_nrd.LoadCase.ToString();



                    frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                    //frm_ViewForces_Load();

                    frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                    //frm_ViewDesign_Forces_Load();



                    txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                    txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                    txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                    txt_ana_MSLD.Text = txt_final_Mx_kN.Text;
                    txt_ana_MSTD.Text = txt_final_Mz_kN.Text;

                    txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                    txt_RCC_Pier_Mx1.Text = txt_final_Mx_kN.Text;
                    txt_RCC_Pier_Mz1.Text = txt_final_Mz_kN.Text;

                    txt_abut_w6.Text = Total_LiveLoad_Reaction;
                    txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                    txt_abut_w6.ForeColor = Color.Red;

                    txt_abut_w5.Text = Total_DeadLoad_Reaction;
                    txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                    txt_abut_w5.ForeColor = Color.Red;


                    //frm_ViewForces f = new frm_ViewForces(iApp, BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, s);
                    //f.Owner = this;
                    //f.Text = "Data to be used in RCC Abutment Design";
                    //f.Show();


                    //frm_Pier_ViewDesign_Forces fv = new frm_Pier_ViewDesign_Forces(iApp, Bridge_Analysis.Total_Analysis_Report, s, s1);
                    //fv.Owner = this;
                    //fv.Text = "Data to be used in RCC Pier Design";
                    //fv.Show();

                    //txt_ana_DLSR.Text = f.Total_DeadLoad_Reaction;
                    //txt_ana_LLSR.Text = f.Total_LiveLoad_Reaction;

                    //txt_ana_TSRP.Text = fv.txt_final_vert_rec_kN.Text;
                    //txt_ana_MSLD.Text = fv.txt_max_Mx_kN.Text;
                    //txt_ana_MSTD.Text = fv.txt_max_Mz_kN.Text;

                    //txt_RCC_Pier_W1_supp_reac.Text = fv.txt_final_vert_rec_kN.Text;
                    //txt_RCC_Pier_Mx1.Text = fv.txt_max_Mx_kN.Text;
                    //txt_RCC_Pier_Mz1.Text = fv.txt_max_Mz_kN.Text;

                    //txt_cnt_w6.Text = f.Total_LiveLoad_Reaction;
                    //txt_pier_2_P3.Text = f.Total_LiveLoad_Reaction;
                    //txt_cnt_w6.ForeColor = Color.Red;

                    //txt_cnt_w5.Text = f.Total_DeadLoad_Reaction;
                    //txt_pier_2_P2.Text = f.Total_DeadLoad_Reaction;
                    //txt_cnt_w5.ForeColor = Color.Red;
                }

                //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


                Deck_Load_Analysis_Data();
                Deck_Initialize_InputData();
                Button_Enable_Disable();
                Text_Changed_Forces();
                Calculate_Interactive_Values();

                Button_Enable_Disable();
                Write_All_Data(false);
                iApp.Save_Form_Record(this, user_path);

                iApp.Progress_Works.Clear();
                #endregion Process
            }
            catch (Exception ex) { }

        }

        private void btn_Ana_add_load_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, txt_Z.Text, txt_XINCR.Text, txt_Load_Impact.Text);
            }
            catch (Exception ex) { }
        }
        void Ana_Write_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad)
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
            s += (add_DeadLoad ? " + SIDL " : "");
            s += (add_LiveLoad ? " + LL " : "");


            if (add_DeadLoad)
            {
                load_lst.AddRange(txt_Ana_member_load.Lines);

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Bridge_Analysis.LiveLoad_File))
                    {
                        //MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + Path.GetDirectoryName(file_name));
                    }

                }
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
                if (dgv_live_load.RowCount != 0)
                    load_lst.AddRange(Get_MovingLoad_Data(Bridge_Analysis.Live_Load_List));
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

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Bridge_Analysis.LiveLoad_File))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                    }
                    //Bridge_Analysis.LoadReadFromGrid(dgv_Ana_live_load);
                }
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
            }
            //Chiranjit [2011 09 23]
            //Do not write Moving Load Data wheather user Remove all the data from the Data Grid Box
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (dgv_live_load.RowCount != 0)
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
                    dgv_live_load.Rows.Clear();
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
                        dgv_live_load.Rows.Add(ld.TypeNo + " : " + ld.Code,
                            ld.X.ToString("0.000"), ld.Y.ToString("0.000"), ld.Z.ToString("0.000"), ld.XINC.ToString("0.000"), ld.ImpactFactor.ToString("0.000"));

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
                        Deck_Load_Analysis_Data();
                        Deck_Initialize_InputData();
                        //txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;
                        iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
                        Read_All_Data();

                        //Chiranjit [2013 04 26]
                        iApp.Read_Form_Record(this, user_path);

                        rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
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
                dgv_live_load.Rows.RemoveAt(dgv_live_load.CurrentRow.Index);
                //chk_ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_all_Click(object sender, EventArgs e)
        {
            dgv_live_load.Rows.Clear();
            //chk_ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);

        }

        private void btn_Ana_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, true);
        }

        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            grb_LL.Enabled = true;
            //grb_SIDL.Enabled = chk_Ana_active_SIDL.Checked;
        }
        #region Composite Methods
        void Analysis_Initialize_InputData()
        {
            Bridge_Analysis.Length = L;

            Bridge_Analysis.Spans = new List<double>();
            try
            {
                MyList spans = new MyList(MyList.RemoveAllSpaces(txt_multiSpan.Text.Replace(',', ' ')), ' ');
                for (int i = 0; i < spans.StringList.Count; i++)
                {
                    Bridge_Analysis.Spans.Add(spans.GetDouble(i));
                }
            }
            catch (Exception exx) { }
            Bridge_Analysis.Length = Bridge_Analysis.Total_Length;
            Bridge_Analysis.WidthBridge = B;
            Bridge_Analysis.Width_LeftCantilever = CL;
            Bridge_Analysis.Width_RightCantilever = CR;
            Bridge_Analysis.Skew_Angle = Ang;
            Bridge_Analysis.Effective_Depth = Deff;
            Bridge_Analysis.NMG = NMG;
            Bridge_Analysis.NCG = NCG;
            Bridge_Analysis.Ds = Ds;

            Bridge_Analysis.Radius = MyList.StringToDouble(txt_curve_radius.Text, 0.0);

            if (!chk_curve.Checked) Bridge_Analysis.Radius = 0;


            


            if (Deck_Analysis == null)
                Deck_Analysis = new LS_DeckSlab_Analysis(iApp);

            //Deck_Analysis.T_Long_Inner_Section = long_inner_sec;
            //Deck_Analysis.T_Long_Outer_Section = long_out_sec;
            //Deck_Analysis.T_Cross_Section = cross_sec;

            //Deck_Analysis.Length = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Deck_Analysis.Length = MyList.StringToDouble(dgv_deck_user_input[1, 4].Value.ToString(), 0.0);
            //Deck_Analysis.WidthBridge = 6.0;

            Deck_Analysis.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
            Deck_Analysis.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
            Deck_Analysis.Skew_Angle = MyList.StringToDouble(dgv_deck_user_input[1, 5].Value.ToString(), 0.0);
            Deck_Analysis.Number_Of_Long_Girder = MyList.StringToInt(txt_Ana_NMG.Text, 4);
            Deck_Analysis.Number_Of_Cross_Girder = MyList.StringToInt(txt_Ana_NCG.Text, 3);
            Deck_Analysis.WidthBridge = L / (Deck_Analysis.Number_Of_Cross_Girder - 1);

            //Deck_Analysis.Lwv = MyList.StringToDouble(txt_Ana_Lwv.Text, 0.0);
            Deck_Analysis.Wkerb = MyList.StringToDouble(txt_Ana_Wc.Text, 0.0);

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


            if (Bridge_Analysis._L2_inn_joints.Count == 0)
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    Bridge_Analysis.CreateData_StraightBritish();
                else
                    Bridge_Analysis.CreateData_Straight_Indian();
            }
            List<int> _L2_inn_joints = Bridge_Analysis._L2_inn_joints;
            List<int> _L4_inn_joints = Bridge_Analysis._L4_inn_joints;
            List<int> _deff_inn_joints = Bridge_Analysis._deff_inn_joints;

            List<int> _L2_out_joints = Bridge_Analysis._L2_out_joints;
            List<int> _L4_out_joints = Bridge_Analysis._L4_out_joints;
            List<int> _deff_out_joints = Bridge_Analysis._deff_out_joints;


            List<int> _L6_out_joints = Bridge_Analysis._L6_out_joints;
            List<int> _L3_out_joints = Bridge_Analysis._L3_out_joints;
            List<int> _3L8_out_joints = Bridge_Analysis._3L8_out_joints;



            #region Read all
            List<double> lst_frc = new List<double>();
            //forces from Dry concrete
            MaxForce mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints, 1);
            //txt_SUMM_I13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L6_out_joints, 1);
            //txt_SUMM_J13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints, 1);
            //txt_SUMM_K13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L3_out_joints, 1);
            //txt_SUMM_L13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_3L8_out_joints, 1);
            //txt_SUMM_M13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints, 1);
            //txt_SUMM_N13.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));




            lst_frc.Sort();


            txt_SUMM_I13.Text = lst_frc[0].ToString();
            txt_SUMM_J13.Text = lst_frc[1].ToString();
            txt_SUMM_K13.Text = lst_frc[2].ToString();
            txt_SUMM_L13.Text = lst_frc[3].ToString();
            txt_SUMM_M13.Text = lst_frc[4].ToString();
            txt_SUMM_N13.Text = lst_frc[5].ToString();


            string frmt = "{0,-40} {1,10:f3} {2,10:f3} {3,10} {4,10} {5,10} {6,10} ";
            list_results.Add(string.Format(""));
            #region Print Results
            if (rbtn_multiSpan.Checked)
            {
                if (chk_curve.Checked)
                {
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("CURVED CONTINUOUS COMPOSITE BRIDGE"));
                    list_results.Add(string.Format("-----------------------------------"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Length = {0} m", txt_Ana_L.Text));
                    list_results.Add(string.Format("Width = {0} m", txt_Ana_B.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Spans = {0} m", txt_multiSpan.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Curve Radius = {0} m", txt_curve_radius.Text));
                    list_results.Add(string.Format("Curve Angle = {0} Degree", txt_curve_angle.Text));
                    list_results.Add(string.Format("Design Speed = {0} kmph = {1} m/s", txt_curve_des_spd_kph.Text, txt_curve_des_spd_mps.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                }
                else
                {
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("STRAIGHT CONTINUOUS COMPOSITE BRIDGE"));
                    list_results.Add(string.Format("-------------------------------------"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Length = {0} m", txt_Ana_L.Text));
                    list_results.Add(string.Format("Width = {0} m", txt_Ana_B.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Spans = {0} m", txt_multiSpan.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                }
            }
            else
            {
                if (chk_curve.Checked)
                {
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("CURVED COMPOSITE BRIDGE"));
                    list_results.Add(string.Format("------------------------"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Length = {0} m", txt_Ana_L.Text));
                    list_results.Add(string.Format("Width = {0} m", txt_Ana_B.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Curve Radius = {0} m", txt_curve_radius.Text));
                    list_results.Add(string.Format("Curve Angle = {0} Degree", txt_curve_angle.Text));
                    list_results.Add(string.Format("Design Speed = {0} kmph = {1} m/s", txt_curve_des_spd_kph.Text, txt_curve_des_spd_mps.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                }
                else
                {
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("STRAIGHT COMPOSITE BRIDGE"));
                    list_results.Add(string.Format("--------------------------"));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("Length = {0} m", txt_Ana_L.Text));
                    list_results.Add(string.Format("Width = {0} m", txt_Ana_B.Text));
                    list_results.Add(string.Format(""));
                    list_results.Add(string.Format("*********************************************************"));
                    list_results.Add(string.Format(""));
                }
            }


            list_results.Add(string.Format(""));
            list_results.Add(string.Format("BENDING MOMENTS (TON-M)"));
            list_results.Add(string.Format("----------------------------"));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(frmt, "", "deff", "L/6", "L/4", "L/3", "3L/8", "L/2"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------"));

            list_results.Add(string.Format(frmt, "Steel Girder Selfweight (Outer/ Inner)", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));

            #endregion Print Results




            //forces from Green concrete
            lst_frc.Clear();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints, 2);
            //txt_SUMM_I15.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L6_out_joints, 2);
            //txt_SUMM_J15.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints, 2);
            //txt_SUMM_K15.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L3_out_joints, 2);
            //txt_SUMM_L15.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_3L8_out_joints, 2);
            //txt_SUMM_M15.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints, 2);
            //txt_SUMM_N15.Text = mfc.Force.ToString();
            lst_frc.Add(Math.Abs(mfc.Force));


            lst_frc.Sort();
            txt_SUMM_I15.Text = lst_frc[0].ToString();
            txt_SUMM_J15.Text = lst_frc[1].ToString();
            txt_SUMM_K15.Text = lst_frc[2].ToString();
            txt_SUMM_L15.Text = lst_frc[3].ToString();
            txt_SUMM_M15.Text = lst_frc[4].ToString();
            txt_SUMM_N15.Text = lst_frc[5].ToString();


            #region Print Results
            list_results.Add(string.Format(frmt, "Green concrete Over Outer Girder", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results




            //forces from SIDL
            lst_frc.Clear();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I16.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L6_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J16.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K16.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L3_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L16.Text = mfc.Force.ToString();


            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_3L8_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M16.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N16.Text = mfc.Force.ToString();


            lst_frc.Sort();
            txt_SUMM_I16.Text = lst_frc[0].ToString();
            txt_SUMM_J16.Text = lst_frc[1].ToString();
            txt_SUMM_K16.Text = lst_frc[2].ToString();
            txt_SUMM_L16.Text = lst_frc[3].ToString();
            txt_SUMM_M16.Text = lst_frc[4].ToString();
            txt_SUMM_N16.Text = lst_frc[5].ToString();


            #region Print Results
            list_results.Add(string.Format(frmt, "SIDL ", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results



            //forces from sufacing


            lst_frc.Clear();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I17.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L6_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J17.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K17.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L3_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L17.Text = mfc.Force.ToString();


            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_3L8_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M17.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N17.Text = mfc.Force.ToString();



            lst_frc.Sort();
            txt_SUMM_I17.Text = lst_frc[0].ToString();
            txt_SUMM_J17.Text = lst_frc[1].ToString();
            txt_SUMM_K17.Text = lst_frc[2].ToString();
            txt_SUMM_L17.Text = lst_frc[3].ToString();
            txt_SUMM_M17.Text = lst_frc[4].ToString();
            txt_SUMM_N17.Text = lst_frc[5].ToString();



            #region Print Results
            list_results.Add(string.Format(frmt, "Surfacing", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results




            BridgeMemberAnalysis LL_Analysis = Bridge_Analysis.LL_Analysis;

            //forces Live Load
            lst_frc.Clear();

            mfc = LL_Analysis.GetJoint_MomentForce(_deff_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I21.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_MomentForce(_L6_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J21.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_MomentForce(_L4_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K21.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_MomentForce(_L3_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L21.Text = mfc.Force.ToString();


            mfc = LL_Analysis.GetJoint_MomentForce(_3L8_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M21.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_MomentForce(_L2_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N21.Text = mfc.Force.ToString();


            lst_frc.Sort();
            txt_SUMM_I21.Text = lst_frc[0].ToString();
            txt_SUMM_J21.Text = lst_frc[1].ToString();
            txt_SUMM_K21.Text = lst_frc[2].ToString();
            txt_SUMM_L21.Text = lst_frc[3].ToString();
            txt_SUMM_M21.Text = lst_frc[4].ToString();
            txt_SUMM_N21.Text = lst_frc[5].ToString();


            #region Print Results
            list_results.Add(string.Format(frmt, "Live Load", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results

            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------"));
            list_results.Add(string.Format(""));
            list_results.Add(string.Format("SHEAR FORCES (TON)"));
            list_results.Add(string.Format("-------------------"));
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------"));


            #endregion Read all


            lst_frc.Clear();

            #region Shear
            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints, 1);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I73.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L6_out_joints, 1);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J73.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints, 1);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K73.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L3_out_joints, 1);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L73.Text = mfc.Force.ToString();


            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_3L8_out_joints, 1);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M73.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints, 1);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N73.Text = mfc.Force.ToString();


            




            lst_frc.Sort();
            lst_frc.Reverse();
            txt_SUMM_I73.Text = lst_frc[0].ToString();
            txt_SUMM_J73.Text = lst_frc[1].ToString();
            txt_SUMM_K73.Text = lst_frc[2].ToString();
            txt_SUMM_L73.Text = lst_frc[3].ToString();
            txt_SUMM_M73.Text = lst_frc[4].ToString();
            txt_SUMM_N73.Text = lst_frc[5].ToString();



            #region Print Results
            list_results.Add(string.Format(frmt, "Steel Girder Selfweight (Outer/ Inner)", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results




            //forces from Green concrete
            lst_frc.Clear();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints, 2);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I75.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L6_out_joints, 2);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J75.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints, 2);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K75.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L3_out_joints, 2);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L75.Text = mfc.Force.ToString();


            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_3L8_out_joints, 2);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M75.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints, 2);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N75.Text = mfc.Force.ToString();






            lst_frc.Sort();
            lst_frc.Reverse();
            txt_SUMM_I75.Text = lst_frc[0].ToString();
            txt_SUMM_J75.Text = lst_frc[1].ToString();
            txt_SUMM_K75.Text = lst_frc[2].ToString();
            txt_SUMM_L75.Text = lst_frc[3].ToString();
            txt_SUMM_M75.Text = lst_frc[4].ToString();
            txt_SUMM_N75.Text = lst_frc[5].ToString();


            #region Print Results
            list_results.Add(string.Format(frmt, "Green concrete Over Outer Girder", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results



            //forces from SIDL
            lst_frc.Clear();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I76.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L6_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J76.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K76.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L3_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L76.Text = mfc.Force.ToString();


            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_3L8_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M76.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints, 3);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N76.Text = mfc.Force.ToString();






            lst_frc.Sort();
            lst_frc.Reverse();
            txt_SUMM_I76.Text = lst_frc[0].ToString();
            txt_SUMM_J76.Text = lst_frc[1].ToString();
            txt_SUMM_K76.Text = lst_frc[2].ToString();
            txt_SUMM_L76.Text = lst_frc[3].ToString();
            txt_SUMM_M76.Text = lst_frc[4].ToString();
            txt_SUMM_N76.Text = lst_frc[5].ToString();


            #region Print Results
            list_results.Add(string.Format(frmt, "SIDL", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results




            //forces from sufacing
            lst_frc.Clear();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I77.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L6_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J77.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K77.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L3_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L77.Text = mfc.Force.ToString();


            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_3L8_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M77.Text = mfc.Force.ToString();

            mfc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints, 4);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N77.Text = mfc.Force.ToString();





            lst_frc.Sort();
            lst_frc.Reverse();
            txt_SUMM_I77.Text = lst_frc[0].ToString();
            txt_SUMM_J77.Text = lst_frc[1].ToString();
            txt_SUMM_K77.Text = lst_frc[2].ToString();
            txt_SUMM_L77.Text = lst_frc[3].ToString();
            txt_SUMM_M77.Text = lst_frc[4].ToString();
            txt_SUMM_N77.Text = lst_frc[5].ToString();



            #region Print Results
            list_results.Add(string.Format(frmt, "Surfacing", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results




            //BridgeMemberAnalysis LL_Analysis = Bridge_Analysis.All_Analysis[1];

            //forces Live Load
            lst_frc.Clear();

            mfc = LL_Analysis.GetJoint_ShearForce(_deff_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_I81.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_ShearForce(_L6_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_J81.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_ShearForce(_L4_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_K81.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_ShearForce(_L3_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_L81.Text = mfc.Force.ToString();


            mfc = LL_Analysis.GetJoint_ShearForce(_3L8_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_M81.Text = mfc.Force.ToString();

            mfc = LL_Analysis.GetJoint_ShearForce(_L2_out_joints);
            lst_frc.Add(Math.Abs(mfc.Force));
            //txt_SUMM_N81.Text = mfc.Force.ToString();




            lst_frc.Sort();
            lst_frc.Reverse();
            txt_SUMM_I81.Text = lst_frc[0].ToString();
            txt_SUMM_J81.Text = lst_frc[1].ToString();
            txt_SUMM_K81.Text = lst_frc[2].ToString();
            txt_SUMM_L81.Text = lst_frc[3].ToString();
            txt_SUMM_M81.Text = lst_frc[4].ToString();
            txt_SUMM_N81.Text = lst_frc[5].ToString();



            #region Print Results
            list_results.Add(string.Format(frmt, "Live Load", lst_frc[0], lst_frc[1], lst_frc[2], lst_frc[3], lst_frc[4], lst_frc[5]));
            #endregion Print Results
            list_results.Add(string.Format("-----------------------------------------------------------------------------------------------------------"));



            #endregion











            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            iApp.Progress_ON("Read Forces....");
            iApp.SetProgressValue(10, 100);


            MaxForce mfrc = new MaxForce();
            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.AddRange(list_results.ToArray());
            Results.Add("");
            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            //mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_inn_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_Ana_inner_long_L2_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_L2_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE ", _L2_inn_joints, "Ton"));





            //mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_inn_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_Ana_inner_long_L2_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_L2_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT ", _L2_inn_joints, "Ton-m"));



            //mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_inn_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_inn_joints);
            txt_Ana_inner_long_L4_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_L4_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE ", _L4_inn_joints, "Ton"));


            //mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_inn_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_inn_joints);
            txt_Ana_inner_long_L4_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_L4_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT ", _L4_inn_joints, "Ton-m"));



            //mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_inn_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_inn_joints);
            txt_Ana_inner_long_deff_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_deff_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_deff_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_deff_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE ", _deff_inn_joints, "Ton"));


            //mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_inn_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_inn_joints);
            txt_Ana_inner_long_deff_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_inner_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_inner_long_deff_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT ", _deff_inn_joints, "Ton-m"));

            iApp.SetProgressValue(70, 100);


            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            //mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints);
            txt_Ana_outer_long_L2_shear.Text = mfrc.ToString();
            txt_Ana_outer_long_L2_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_out_joints, "Ton"));


            //mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints);
            txt_Ana_outer_long_L2_moment.Text = mfrc.ToString();
            txt_Ana_outer_long_L2_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_out_joints, "Ton-m"));

            //mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints);
            txt_Ana_outer_long_L4_shear.Text = mfrc.ToString();
            txt_Ana_outer_long_L4_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));

            //mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints);
            txt_Ana_outer_long_L4_moment.Text = mfrc.ToString();
            txt_Ana_outer_long_L4_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));

            //mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints);
            txt_Ana_outer_long_deff_shear.Text = mfrc.ToString();
            txt_Ana_outer_long_deff_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));

            //mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints, true);
            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints);
            txt_Ana_outer_long_deff_moment.Text = mfrc.ToString();
            txt_Ana_outer_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));
            iApp.SetProgressValue(99, 100);



            #region Null All variables
            mc = null;

            #endregion

            //list_results.AddRange(Results.ToArray());
            File.WriteAllLines(Result_Report, Results.ToArray());
            //File.WriteAllLines(Result_Report, list_results.ToArray());
            iApp.SetProgressValue(100, 100);
            iApp.Progress_OFF();
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

                txt_Ana_cross_max_moment.Text = (m.MaxBendingMoment).ToString();
                txt_Ana_cross_max_shear.Text = m.MaxShearForce.ToString();
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
            btn_view_structure.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_View_Moving_Load.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_view_report.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_Ana_process_analysis.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);


            Analysis_Button_Enabled();


            btnReport.Enabled = File.Exists(Deck.rep_file_name);
            //btn_dwg_deck_slab.Enabled = File.Exists(Deck.drawing_path);
            //btn_dwg_cant.Enabled = File.Exists(Cant.user_drawing_file);
            btn_abut_Report.Enabled = File.Exists(Abut.rep_file_name);
            //btn_dwg_abutment.Enabled = File.Exists(Abut.drawing_path);
            btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
            //btn_dwg_rcc_pier.Enabled = File.Exists(rcc_pier.rep_file_name);
            //Deck_Load_Analysis_Data();
            //Deck_Initialize_InputData();

            Deck_Buttons();
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

            Deck.FilePath = user_path;
            Abut.FilePath = user_path;
            rcc_pier.FilePath = user_path;


            string usp = Path.Combine(user_path, "Steel Girder Analysis");

            if (!Directory.Exists(usp))
                Directory.CreateDirectory(usp);

            if (Bridge_Analysis == null)
                Bridge_Analysis = new Composite_LS_Analysis(iApp);
            Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");


            usp = Path.Combine(user_path, "Deck Slab Analysis");

            if (!Directory.Exists(usp))
                Directory.CreateDirectory(usp);


            if (Deck_Analysis == null)
            {

                Deck_Analysis = new LS_DeckSlab_Analysis(iApp);

            }
            uC_Deckslab_IS1.user_path = user_path;

            Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");

            if (File.Exists(analysis_file))
            {
                btn_view_structure.Enabled = true;

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

        public void Ana_OpenAnalysisFile_2013_04_26(string file_name)
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
            if (File.Exists(analysis_file))
            {
                btn_view_structure.Enabled = true;

                Bridge_Analysis.Input_File = (analysis_file);
                string rep_file = Bridge_Analysis.Total_Analysis_Report;
                if (File.Exists(analysis_file))
                {
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, rep_file);
                    Show_Moment_Shear();
                }
                else
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, analysis_file);

                if (Bridge_Analysis != null)
                {
                    if (Bridge_Analysis.Structure.Analysis.Joints.Count > 1)
                    {
                        Bridge_Analysis.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Bridge_Analysis.Structure.Analysis.Joints[1].X / Bridge_Analysis.Structure.Analysis.Joints[1].Z)));
                        Ang = Bridge_Analysis.Skew_Angle;
                    }
                }
                txt_Ana_L.Text = Bridge_Analysis.Structure.Analysis.Length.ToString();
                txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                txt_Ana_B.Text = Bridge_Analysis.Structure.Analysis.Width.ToString();
                //txt_gd_np.Text = (Bridge_Analysis.Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                txt_Ana_analysis_file.Visible = true;
                txt_Ana_analysis_file.Text = analysis_file;


                //txt_Ana_eff_depth.Text = Bridge_Analysis.Truss_Analysis.Analysis.Effective_Depth.ToString();
                txt_Ana_CL.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();
                txt_Ana_CR.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();
                //if (File.Exists(kFile))
                //{
                //    //Read_DL_SIDL();
                //    //Read_Live_Load();
                //}

            }

            #region Chiranjit [2012 06 10]
        _run:

            Bridge_Analysis.Input_File = analysis_file;
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (File.Exists(ana_rep_file))
            {
                Bridge_Analysis.Structure = null;
                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, ana_rep_file);
                Show_Moment_Shear();

                string s = "";
                string s1 = "";
                int i = 0;
                for (i = 0; i < Bridge_Analysis.Structure.Supports.Count; i++)
                {
                    if (i < Bridge_Analysis.Structure.Supports.Count / 2)
                    {
                        if (i == Bridge_Analysis.Structure.Supports.Count / 2 - 1)
                        {
                            s += Bridge_Analysis.Structure.Supports[i].NodeNo;
                        }
                        else
                            s += Bridge_Analysis.Structure.Supports[i].NodeNo + ",";
                    }
                    else
                    {
                        if (i == Bridge_Analysis.Structure.Supports.Count - 1)
                        {
                            s1 += Bridge_Analysis.Structure.Supports[i].NodeNo;
                        }
                        else
                            s1 += Bridge_Analysis.Structure.Supports[i].NodeNo + ",";
                    }
                }
                double BB = MyList.StringToDouble(txt_abut_B.Text, 8.5);

                //frm_ViewForces f = new frm_ViewForces(iApp, BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, s);
                //f.Owner = this;
                //f.Text = "Data to be used in RCC Abutment Design";
                //f.Show();


                //frm_Pier_ViewDesign_Forces fv = new frm_Pier_ViewDesign_Forces(iApp, Bridge_Analysis.Total_Analysis_Report, s, s1);
                //fv.Owner = this;
                //fv.Text = "Data to be used in RCC Pier Design";
                //fv.Show();


                //txt_ana_DLSR.Text = f.Total_DeadLoad_Reaction;
                //txt_ana_LLSR.Text = f.Total_LiveLoad_Reaction;

                //txt_ana_TSRP.Text = fv.txt_final_vert_rec_kN.Text;
                //txt_ana_MSLD.Text = fv.txt_max_Mx_kN.Text;
                //txt_ana_MSTD.Text = fv.txt_max_Mz_kN.Text;


                //txt_RCC_Pier_W1_supp_reac.Text = fv.txt_final_vert_rec_kN.Text;
                //txt_RCC_Pier_Mx1.Text = fv.txt_max_Mx_kN.Text;
                //txt_RCC_Pier_Mz1.Text = fv.txt_max_Mz_kN.Text;

                //txt_abut_w6.Text = f.Total_LiveLoad_Reaction;
                //txt_pier_2_P3.Text = f.Total_LiveLoad_Reaction;
                //txt_abut_w6.ForeColor = Color.Red;

                //txt_abut_w5.Text = f.Total_DeadLoad_Reaction;
                //txt_pier_2_P2.Text = f.Total_DeadLoad_Reaction;
                //txt_abut_w5.ForeColor = Color.Red;


                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s + " " + s1));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s, s1);
                frm_ViewDesign_Forces_Load();

                txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                txt_ana_MSLD.Text = txt_final_Mx_kN.Text;
                txt_ana_MSTD.Text = txt_final_Mz_kN.Text;

                txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                txt_RCC_Pier_Mx1.Text = txt_final_Mx_kN.Text;
                txt_RCC_Pier_Mz1.Text = txt_final_Mz_kN.Text;

                txt_abut_w6.Text = Total_LiveLoad_Reaction;
                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                txt_abut_w6.ForeColor = Color.Red;

                txt_abut_w5.Text = Total_DeadLoad_Reaction;
                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                txt_abut_w5.ForeColor = Color.Red;
            }

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
                btn_view_structure.Enabled = true;

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
                            Ang = Bridge_Analysis.Skew_Angle;
                        }
                    }

                    txt_Ana_L.Text = Bridge_Analysis.Structure.Analysis.Length.ToString();
                    txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                    txt_Ana_B.Text = Bridge_Analysis.Structure.Analysis.Width.ToString();

                    //txt_Ana_Deff.Text = Bridge_Analysis.Truss_Analysis.Analysis.Effective_Depth.ToString();
                    txt_Ana_CL.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();
                    txt_Ana_CR.Text = Bridge_Analysis.Structure.Analysis.Width_Cantilever.ToString();


                    Ang = Bridge_Analysis.Structure.Analysis.Skew_Angle;


                    //txt_gd_np.Text = (Bridge_Analysis.Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                    txt_Ana_analysis_file.Visible = true;
                    txt_Ana_analysis_file.Text = analysis_file;
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

                Bridge_Analysis.LoadReadFromGrid(dgv_live_load);
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

        #endregion Composite Methods

        #region Deck Slab + Steel Girder Form Events
        private void btn_Deck_Process_Click(object sender, EventArgs e)
        {
            if (!Check_Project_Folder()) return;

            //Chiranjit [2012 07 13]
            Write_All_Data();
            //if (txt_des_mom.Text == "" && txt_des_shr.Text == "")
            //{
            //    string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
            //    msg += "Please enter the Design Forces manualy.\n\n";
            //    msg += "For Example : Design Moment  = 1500 kN-m\n";
            //    msg += "            : Design  Shear  = 600 kN\n";
            //    txt_des_mom.Text = "1500";
            //    txt_des_shr.Text = "600";
            //    MessageBox.Show(msg, "ASTRA");
            //}

            Deck.FilePath = user_path;
            DeckSlab_Initialize_InputData();
            Deck.Write_User_Input();
            Deck.Calculate_Program();
            Deck.Write_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(Deck.rep_file_name))
            {
                MessageBox.Show(this, "Design is done as \"" + (rbtn_sec_box.Checked ? " BOX" : "PLATE") + " Girder\".\n\n" +
                "Report file written in " + Deck.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(Deck.rep_file_name);
            }
            Deck.is_process = true;
            Button_Enable_Disable();

        }
        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {
            if (rbtn_inner_girder.Checked)
                iApp.RunExe(Deck.rep_file_name_inner);
            else
                iApp.RunExe(Deck.rep_file_name_outer);
        }
        private void btn_Deck_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Deck_tmrComp_Tick(object sender, EventArgs e)
        {
            Deck.flg = !Deck.flg;

            if (Deck.flg)
            {
                pic_deck.BackgroundImage = (Image)AstraFunctionOne.ImageCollection.CompoBridge;
            }
            else
            {
                pic_deck.BackgroundImage = (Image)AstraFunctionOne.ImageCollection.DCP_3935;
            }

        }
        private void txt_Deck_fck_TextChanged(object sender, EventArgs e)
        {
            DeckSlab_Initialize_InputData();
            //double fcc, j, Q, fcb, n;

            //fck = concrete_grade;

            //fcb = Deck.fck / 3;
            //fcc = Deck.fck / 4;

            //n = Deck.m * fcb / (Deck.m * fcb + Deck.sigma_st);

            //j = 1 - (n / 3.0);
            //Q = n * j * fcb / 2;

            //txt_sigma_b.Text = fcb.ToString("0.00");
            //txt_j.Text = j.ToString("0.000");
            //txt_Q.Text = Q.ToString("0.000");
        }
        private void Deck_rbtn_inner_girder_CheckedChanged(object sender, EventArgs e)
        {
            Deck_Load_Analysis_Data();
            Deck_Initialize_InputData();
            try
            {
                if (File.Exists(txt_Ana_analysis_file.Text))
                {
                    Deck.FilePath = user_path;
                }
                if (rbtn_inner_girder.Checked)
                    btnReport.Enabled = (File.Exists(Deck.rep_file_name_inner));
                else
                    btnReport.Enabled = (File.Exists(Deck.rep_file_name_outer));
            }
            catch (Exception ex) { }

        }
        #endregion Deck Slab + Steel Girder Form Events

        #region Deck Slab + Steel Girder Methods
        public void Deck_Load_Analysis_Data()
        {
            txt_deck_inner_L2_Moment.Text = MyList.StringToDouble(txt_Ana_inner_long_L2_moment.Text, 0.0) * 10.0 + "";
            txt_deck_inner_L4_Moment.Text = MyList.StringToDouble(txt_Ana_inner_long_L4_moment.Text, 0.0) * 10.0 + "";
            txt_deck_inner_Deff_Moment.Text = MyList.StringToDouble(txt_Ana_inner_long_deff_moment.Text, 0.0) * 10.0 + "";
            txt_deck_inner_Deff_Shear.Text = MyList.StringToDouble(txt_Ana_inner_long_deff_shear.Text, 0.0) * 10.0 + "";
            txt_deck_inner_L4_Shear.Text = MyList.StringToDouble(txt_Ana_inner_long_L4_shear.Text, 0.0) * 10.0 + "";
            txt_deck_inner_L2_Shear.Text = MyList.StringToDouble(txt_Ana_inner_long_L2_shear.Text, 0.0) * 10.0 + "";

            txt_deck_outer_L2_Moment.Text = MyList.StringToDouble(txt_Ana_outer_long_L2_moment.Text, 0.0) * 10.0 + "";
            txt_deck_outer_L4_Moment.Text = MyList.StringToDouble(txt_Ana_outer_long_L4_moment.Text, 0.0) * 10.0 + "";
            txt_deck_outer_Deff_Moment.Text = MyList.StringToDouble(txt_Ana_outer_long_deff_moment.Text, 0.0) * 10.0 + "";
            txt_deck_outer_Deff_Shear.Text = MyList.StringToDouble(txt_Ana_outer_long_deff_shear.Text, 0.0) * 10.0 + "";
            txt_deck_outer_L4_Shear.Text = MyList.StringToDouble(txt_Ana_outer_long_L4_shear.Text, 0.0) * 10.0 + "";
            txt_deck_outer_L2_Shear.Text = MyList.StringToDouble(txt_Ana_outer_long_L2_shear.Text, 0.0) * 10.0 + "";

            //txt_des_mom.ForeColor = Color.Red;
            //txt_des_shr.ForeColor = Color.Red;



            //if (rbtn_inner_girder.Checked)
            //{

            //    switch (cmb_deck_des_mom.SelectedIndex)
            //    {
            //        case 0:
            //            txt_des_mom.Text = txt_Ana_inner_long_deff_moment.Text;
            //            break;
            //        case 1:
            //            txt_des_mom.Text = txt_Ana_inner_long_L4_moment.Text;
            //            break;
            //        case 2:
            //            txt_des_mom.Text = txt_Ana_inner_long_L2_moment.Text;
            //            break;
            //    }
            //    switch (cmb_deck_shr.SelectedIndex)
            //    {
            //        case 0:
            //            txt_des_shr.Text = txt_Ana_inner_long_deff_shear.Text;
            //            break;
            //        case 1:
            //            txt_des_shr.Text = txt_Ana_inner_long_L4_shear.Text;
            //            break;
            //        case 2:
            //            txt_des_shr.Text = txt_Ana_inner_long_L2_shear.Text;
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (cmb_deck_des_mom.SelectedIndex)
            //    {
            //        case 0:
            //            txt_des_mom.Text = txt_Ana_outer_long_deff_moment.Text;
            //            break;
            //        case 1:
            //            txt_des_mom.Text = txt_Ana_outer_long_L4_moment.Text;
            //            break;
            //        case 2:
            //            txt_des_mom.Text = txt_Ana_outer_long_L2_moment.Text;
            //            break;
            //    }
            //    switch (cmb_deck_shr.SelectedIndex)
            //    {
            //        case 0:
            //            txt_des_shr.Text = txt_Ana_outer_long_deff_shear.Text;
            //            break;
            //        case 1:
            //            txt_des_shr.Text = txt_Ana_outer_long_L4_shear.Text;
            //            break;
            //        case 2:
            //            txt_des_shr.Text = txt_Ana_outer_long_L2_shear.Text;
            //            break;
            //    }
            //}
            //txt_des_mom.Text = MyList.StringToDouble(txt_des_mom.Text, 0.0) * 10.0 + "";
            //txt_des_shr.Text = MyList.StringToDouble(txt_des_shr.Text, 0.0) * 10.0 + "";
        }
        private void Deck_Initialize_InputData()
        {
            //Deck.tbl_rolledSteelAngles = new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES"));

            string kStr = "";
            try
            {
                if (cmb_L_2_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_L_2_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_2_ang_section_code.Items.Contains(item.SectionCode) == false)
                                cmb_L_2_ang_section_code.Items.Add(item.SectionCode);
                        }
                    }
                }
                if (cmb_L_2_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_L_2_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_2_ang_section_code.Items.Contains(item.SectionCode) == false)
                                cmb_L_2_ang_section_code.Items.Add(item.SectionCode);
                        }
                    }
                }

                if (cmb_L_4_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_4_ang_section_code.Items.Contains(item.SectionCode) == false)
                                cmb_L_4_ang_section_code.Items.Add(item.SectionCode);
                        }
                    }
                }
                if (cmb_L_4_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_4_ang_section_code.Items.Contains(item.SectionCode) == false)
                                cmb_L_4_ang_section_code.Items.Add(item.SectionCode);
                        }
                    }
                }

                if (cmb_Deff_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Deff_ang_section_code.Items.Contains(item.SectionCode) == false)
                                cmb_Deff_ang_section_code.Items.Add(item.SectionCode);
                        }
                    }
                }
                if (cmb_Deff_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Deff_ang_section_code.Items.Contains(item.SectionCode) == false)
                                cmb_Deff_ang_section_code.Items.Add(item.SectionCode);
                        }
                    }
                }

                //cmb_ang.SelectedItem = "100100";
                //cmb_ang_thk.SelectedItem = 10.0;
                //txt_na.SelectedItem = "4";



                //cmb_L_2_ang_section_code.SelectedIndex = 0;
                //cmb_L_2_ang_section_code.SelectedItem = "100X100";
                //cmb_L_2_ang_thk.SelectedItem = 10.0;
                //cmb_L_2_nos_ang.SelectedItem = "4";


                //cmb_L_4_ang_section_code.SelectedIndex = 0;
                //cmb_L_4_ang_section_code.SelectedItem = "100X100";
                //cmb_L_4_ang_thk.SelectedItem = 10.0;
                //cmb_L_4_nos_ang.SelectedItem = "4";


                //cmb_Deff_ang_section_code.SelectedIndex = 0;
                //cmb_Deff_ang_section_code.SelectedItem = "100X100";
                //cmb_Deff_ang_thk.SelectedItem = 10.0;
                //cmb_Deff_nos_ang.SelectedItem = "4";
            }
            catch (Exception ex) { }
        }

        public void DeckSlab_Initialize_InputData()
        {
            try
            {
                Deck.IsInnerGirder = rbtn_inner_girder.Checked;



                //Chiranjit [2012 12 17]
                Deck.Steel_Section = Steel_Composite_Section();


                Deck.S = MyList.StringToDouble(txt_Deck_L.Text, 0.0);
                Deck.B1 = MyList.StringToDouble(txt_Deck_CW.Text, 0.0);
                Deck.B2 = MyList.StringToDouble(txt_Deck_BS.Text, 0.0);
                Deck.B = MyList.StringToDouble(txt_Deck_SMG.Text, 0.0);
                Deck.fck = MyList.StringToDouble(cmb_deck_fck.Text, 0.0);
                Deck.fy = MyList.StringToDouble(cmb_deck_fy.Text, 0.0);
                Deck.m = MyList.StringToDouble(txt_deck_m.Text, 0.0);
                Deck.YS = MyList.StringToDouble(txt_YS.Text, 0.0);
                Deck.D = MyList.StringToDouble(txt_Deck_Ds.Text, 0.0);
                Deck.L = MyList.StringToDouble(txt_Deck_SCG.Text, 0.0);
                Deck.Dwc = MyList.StringToDouble(txt_Deck_DW.Text, 0.0);

                Deck.gamma_c = MyList.StringToDouble(txt_Deck_gamma_c.Text, 0.0);

                Deck.gamma_wc = MyList.StringToDouble(txt_Deck_gamma_wc.Text, 0.0);
                Deck.WL = MyList.StringToDouble(txt_Deck_WL.Text, 0.0);
                Deck.v = MyList.StringToDouble(txt_Deck_load_length.Text, 0.0);
                Deck.u = MyList.StringToDouble(txt_Deck_load_width.Text, 0.0);
                Deck.IF = MyList.StringToDouble(txt_IF.Text, 0.0);
                Deck.CF = MyList.StringToDouble(txt_CF.Text, 0.0);
                Deck.j = MyList.StringToDouble(txt_Deck_j.Text, 0.0);
                Deck.Q = MyList.StringToDouble(txt_Deck_Q.Text, 0.0);

                Deck.sigma_st = MyList.StringToDouble(txt_Deck_sigma_st.Text, 0.0);
                Deck.sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                Deck.tau = MyList.StringToDouble(txt_tau.Text, 0.0);
                Deck.sigma_tf = MyList.StringToDouble(txt_sigma_tf.Text, 0.0);

                Deck.K = MyList.StringToDouble(txt_K.Text, 0.0);
                Deck.sigma_p = MyList.StringToDouble(txt_sigma_p.Text, 0.0);


                //Deck.dw = MyList.StringToDouble(txt_Long_DMG.Text, 0.0);
                //Deck.tw = MyList.StringToDouble(txt_Long_tw_L2.Text, 0.0);
                //Deck.bf1 = MyList.StringToDouble(txt_Long_bf1_L2.Text, 0.0);
                //Deck.tf1 = MyList.StringToDouble(txt_Long_tf1_L2.Text, 0.0);
                //Deck.bf2 = MyList.StringToDouble(txt_Long_bf2_L2.Text, 0.0);
                //Deck.tf2 = MyList.StringToDouble(txt_Long_tf2_L2.Text, 0.0);

                Deck.ang = cmb_L_2_ang_section_code.Text;
                Deck.ang_thk = MyList.StringToDouble(cmb_L_2_ang_thk.Text, 0.0);
                Deck.off = MyList.StringToDouble(txt_off.Text, 0.0);

                //Deck.Steel_Section.Section_Long_Girder_at_Mid_Span.AngleSection = iApp.Tables
                //Deck.des_moment = MyList.StringToDouble(txt_des_mom.Text, 0.0);
                //Deck.des_shear = MyList.StringToDouble(txt_des_shr.Text, 0.0);


                //Deck.nw = MyList.StringToInt(txt_Long_nw_L2.Text, 0);
                Deck.nf = 1;
                Deck.na = MyList.StringToInt(cmb_L_2_nos_ang.Text, 0);



                //Chiranjit [2011 09 20]
                //Add Extra Property like Nos of Web PLates at L/2
                //Deck.L_2_nw = MyList.StringToDouble(txt_Long_nw_L2.Text, 0);
                //Deck.L_4_nw = MyList.StringToDouble(txt_Long_nw_L4.Text, 0);
                //Deck.Deff_nw = MyList.StringToDouble(txt_Long_nw_Deff.Text, 0);

                //Deck.L_2_dw = MyList.StringToDouble(txt_Long_DMG.Text, 0);
                //Deck.L_4_dw = MyList.StringToDouble(txt_Long_DMG.Text, 0);
                //Deck.Deff_dw = MyList.StringToDouble(txt_Long_DMG.Text, 0);

                //Deck.L_2_bf1 = MyList.StringToDouble(txt_Long_bf1_L2.Text, 0);
                //Deck.L_4_bf1 = MyList.StringToDouble(txt_Long_bf1_L4.Text, 0);
                //Deck.Deff_bf1 = MyList.StringToDouble(txt_Long_bf1_Deff.Text, 0);

                //Deck.L_2_bf2 = MyList.StringToDouble(txt_Long_bf2_L2.Text, 0);
                //Deck.L_4_bf2 = MyList.StringToDouble(txt_Long_bf2_L4.Text, 0);
                //Deck.Deff_bf2 = MyList.StringToDouble(txt_Long_bf2_Deff.Text, 0);


                Deck.L_2_nf = 1;
                Deck.L_4_nf = 1;
                Deck.Deff_nf = 1;


                //Deck.L_2_tf1 = MyList.StringToDouble(txt_Long_tf1_L2.Text, 0);
                //Deck.L_4_tf1 = MyList.StringToDouble(txt_Long_tf1_L4.Text, 0);
                //Deck.Deff_tf1 = MyList.StringToDouble(txt_Long_tf1_Deff.Text, 0);

                //Deck.L_2_tf2 = MyList.StringToDouble(txt_Long_tf2_L2.Text, 0);
                //Deck.L_4_tf2 = MyList.StringToDouble(txt_Long_tf2_L4.Text, 0);
                //Deck.Deff_tf2 = MyList.StringToDouble(txt_Long_tf2_Deff.Text, 0);



                //Deck.L_2_tw = MyList.StringToDouble(txt_Long_tw_L2.Text, 0);
                //Deck.L_4_tw = MyList.StringToDouble(txt_Long_tw_L4.Text, 0);
                //Deck.Deff_tw = MyList.StringToDouble(txt_Long_tw_Deff.Text, 0);



                Deck.L_2_ang_name = cmb_L_2_ang_section_name.Text;
                Deck.L_4_ang_name = cmb_L_4_ang_section_name.Text;
                Deck.Deff_ang_name = cmb_Deff_ang_section_name.Text;

                Deck.L_2_ang_thk = MyList.StringToDouble(cmb_L_2_ang_thk.Text, 0);
                Deck.L_4_ang_thk = MyList.StringToDouble(cmb_L_4_ang_thk.Text, 0);
                Deck.Deff_ang_thk = MyList.StringToDouble(cmb_Deff_ang_thk.Text, 0);

                Deck.L_2_nos_ang = MyList.StringToDouble(cmb_L_2_nos_ang.Text, 0);
                Deck.L_4_nos_ang = MyList.StringToDouble(cmb_L_4_nos_ang.Text, 0);
                Deck.Deff_nos_ang = MyList.StringToDouble(cmb_Deff_nos_ang.Text, 0);

                Deck.L_2_ang = cmb_L_2_ang_section_code.Text;
                Deck.L_4_ang = cmb_L_4_ang_section_code.Text;
                Deck.Deff_ang = cmb_Deff_ang_section_code.Text;
                Deck.IsPlateArrangement = rbtn_sec_plate.Checked;

                Deck.deff = Deck.dw / 1000.0;

                //if (rbtn_inner_girder.Checked)
                //{
                //    Deck.L_2_Moment = MyList.StringToDouble(txt_Ana_inner_long_L2_moment.Text, 0.0)*10;
                //    Deck.L_4_Moment = MyList.StringToDouble(txt_Ana_inner_long_L4_moment.Text, 0.0)*10;
                //    Deck.Deff_Moment = MyList.StringToDouble(txt_Ana_inner_long_deff_moment.Text, 0.0)*10;

                //    Deck.L_2_Shear = MyList.StringToDouble(txt_Ana_inner_long_L2_shear.Text, 0.0)*10;
                //    Deck.L_4_Shear = MyList.StringToDouble(txt_Ana_inner_long_L4_shear.Text, 0.0) * 10;
                //    Deck.Deff_Shear = MyList.StringToDouble(txt_Ana_inner_long_deff_shear.Text, 0.0) * 10;
                //}
                //else if (rbtn_inner_girder.Checked)
                //{
                //    Deck.L_2_Moment = MyList.StringToDouble(txt_Ana_outer_long_L2_moment.Text, 0.0)*10;
                //    Deck.L_4_Moment = MyList.StringToDouble(txt_Ana_outer_long_L4_moment.Text, 0.0)*10;
                //    Deck.Deff_Moment = MyList.StringToDouble(txt_Ana_outer_long_deff_moment.Text, 0.0)*10;

                //    Deck.L_2_Shear = MyList.StringToDouble(txt_Ana_outer_long_L2_shear.Text, 0.0)*10;
                //    Deck.L_4_Moment = MyList.StringToDouble(txt_Ana_outer_long_L4_shear.Text, 0.0)*10;
                //    Deck.Deff_Moment = MyList.StringToDouble(txt_Ana_outer_long_deff_shear.Text, 0.0)*10;
                //}



                if (rbtn_inner_girder.Checked)
                {
                    Deck.L_2_Moment = MyList.StringToDouble(txt_deck_inner_L2_Moment.Text, 0.0);
                    Deck.L_4_Moment = MyList.StringToDouble(txt_deck_inner_L4_Moment.Text, 0.0);
                    Deck.Deff_Moment = MyList.StringToDouble(txt_deck_inner_Deff_Moment.Text, 0.0);

                    Deck.L_2_Shear = MyList.StringToDouble(txt_deck_inner_L2_Shear.Text, 0.0);
                    Deck.L_4_Shear = MyList.StringToDouble(txt_deck_inner_L4_Shear.Text, 0.0);
                    Deck.Deff_Shear = MyList.StringToDouble(txt_deck_inner_Deff_Shear.Text, 0.0);
                }
                else
                {
                    Deck.L_2_Moment = MyList.StringToDouble(txt_deck_outer_L2_Moment.Text, 0.0);
                    Deck.L_4_Moment = MyList.StringToDouble(txt_deck_outer_L4_Moment.Text, 0.0);
                    Deck.Deff_Moment = MyList.StringToDouble(txt_deck_outer_Deff_Moment.Text, 0.0);

                    Deck.L_2_Shear = MyList.StringToDouble(txt_deck_outer_L2_Shear.Text, 0.0);
                    Deck.L_4_Shear = MyList.StringToDouble(txt_deck_outer_L4_Shear.Text, 0.0);
                    Deck.Deff_Shear = MyList.StringToDouble(txt_deck_outer_Deff_Shear.Text, 0.0);
                }



                Deck.NumberOfGirder = (int)NMG; // Chiranjit [2013 06 25]

                Deck.Node_Displacement_Data_LL = txt_LL_node_displace.Text; // Chiranjit [2013 06 27]
                Deck.Node_Displacement_Data_DL = txt_DL_node_displace.Text; // Chiranjit [2013 06 27]

                //Deck.L = NMG;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }
        }
        public void DeckSlab_Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(Deck.user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
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
                        case "S":
                            Deck.S = mList.GetDouble(1);
                            txt_Deck_L.Text = Deck.S.ToString();
                            break;
                        case "B1":
                            Deck.B1 = mList.GetDouble(1);
                            txt_Deck_CW.Text = Deck.B1.ToString();
                            break;
                        case "B2":
                            Deck.B2 = mList.GetDouble(1);
                            txt_Deck_BS.Text = Deck.B2.ToString();
                            break;
                        case "B":
                            Deck.B = mList.GetDouble(1);
                            txt_Deck_SMG.Text = Deck.B.ToString();
                            break;
                        case "fck":
                            Deck.fck = mList.GetDouble(1);
                            //txt_Deck_fck_TextChanged.Text = Deck.fck.ToString();
                            break;
                        case "fy":
                            Deck.fy = mList.GetDouble(1);
                            //txt_fy.Text = Deck.fy.ToString();
                            break;
                        case "m":
                            Deck.m = mList.GetDouble(1);
                            //txt_m.Text = Deck.m.ToString();
                            break;
                        case "YS":
                            Deck.YS = mList.GetDouble(1);
                            txt_YS.Text = Deck.YS.ToString();
                            break;
                        case "D":
                            Deck.D = mList.GetDouble(1);
                            txt_Deck_Ds.Text = Deck.D.ToString();
                            break;
                        case "L":
                            Deck.L = mList.GetDouble(1);
                            txt_Deck_SCG.Text = Deck.L.ToString();
                            break;
                        case "Dwc":
                            Deck.Dwc = mList.GetDouble(1);
                            txt_Deck_DW.Text = Deck.Dwc.ToString();
                            break;
                        case "gamma_c":
                            Deck.gamma_c = mList.GetDouble(1);
                            txt_Deck_gamma_c.Text = Deck.gamma_c.ToString();
                            break;
                        case "gamma_wc":
                            Deck.gamma_wc = mList.GetDouble(1);
                            txt_Deck_gamma_wc.Text = Deck.gamma_wc.ToString();
                            break;
                        case "WL":
                            Deck.WL = mList.GetDouble(1);
                            txt_Deck_WL.Text = Deck.WL.ToString();
                            break;
                        case "v":
                            Deck.v = mList.GetDouble(1);
                            txt_Deck_load_length.Text = Deck.v.ToString();
                            break;
                        case "u":
                            Deck.u = mList.GetDouble(1);
                            txt_Deck_load_width.Text = Deck.u.ToString();
                            break;
                        case "IF":
                            Deck.IF = mList.GetDouble(1);
                            txt_IF.Text = Deck.IF.ToString();
                            break;
                        case "CF":
                            Deck.CF = mList.GetDouble(1);
                            txt_CF.Text = Deck.CF.ToString();
                            break;
                        case "Q":
                            Deck.Q = mList.GetDouble(1);
                            txt_Deck_Q.Text = Deck.Q.ToString();
                            break;
                        case "j":
                            Deck.j = mList.GetDouble(1);
                            txt_Deck_j.Text = Deck.j.ToString();
                            break;
                        case "sigma_st":
                            Deck.sigma_st = mList.GetDouble(1);
                            txt_Deck_sigma_st.Text = Deck.sigma_st.ToString();
                            break;
                        case "sigma_b":
                            Deck.sigma_b = mList.GetDouble(1);
                            txt_sigma_b.Text = Deck.sigma_b.ToString();
                            break;
                        case "tau":
                            Deck.tau = mList.GetDouble(1);
                            txt_tau.Text = Deck.tau.ToString();
                            break;
                        case "sigma_tf":
                            Deck.sigma_tf = mList.GetDouble(1);
                            txt_sigma_tf.Text = Deck.sigma_tf.ToString();
                            break;
                        case "K":
                            Deck.K = mList.GetDouble(1);
                            txt_K.Text = Deck.K.ToString();
                            break;
                        case "sigma_p":
                            Deck.sigma_p = mList.GetDouble(1);
                            txt_sigma_p.Text = Deck.sigma_p.ToString();
                            break;
                        case "dw":
                            //txt_Long_DMG.Text = mList.StringList[1];
                            break;
                        case "tw":
                            //txt_tw.Text = mList.StringList[1];
                            break;
                        case "nw":
                            //txt_nw.Text = mList.StringList[1];
                            break;
                        case "bf":
                            //txt_bf.Text = mList.StringList[1];
                            break;
                        case "tf":
                            //txt_tf.Text = mList.StringList[1];
                            break;
                        case "nf":
                            //txt_nf.Text = mList.StringList[1];
                            break;
                        case "ang":
                            //cmb_ang.SelectedItem = mList.StringList[1];
                            break;
                        case "ang_thk":
                            //cmb_ang_thk.Text = mList.StringList[1];
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        #endregion Deck Slab + Steel Girder Methods

        private void frm_Composite_Load(object sender, EventArgs e)
        {

            uC_CompositeBridgeLSM1.iApp = iApp;
            #region Deckslab
            uC_Deckslab_BS1.iApp = iApp;
            uC_Deckslab_IS1.iApp = iApp;
            uC_Deckslab_IS1.Loading_Standard();
            #endregion Deckslab

            #region Bearings

            //Chiranjit [2016 03 1]
            uC_BRD1.iApp = iApp;
            uC_BRD1.Load_Default_Data();
            iApp.user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title); ;


            #endregion Bearings

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));

                grb_ll_indian.Visible = false;
                pic_diagram.Size = new Size(pic_diagram.Size.Width, 280);

                cmb_HB.SelectedIndex = 2;

                British_Interactive();

                txt_Ana_NMG.SelectedIndex = 0;

                tabControl1.TabPages.Remove(tab_deckslab_IS);

            }
            else
            {

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));

                tbc_girder.TabPages.Remove(tab_moving_data_british);
                tabControl1.TabPages.Remove(tab_deckslab_BS);

                tab_deckslab_IS.Text = "Deck Slab";
            }
            tabControl1.TabPages.Remove(tab_DeckSlab);





            #region Default Deck slab Limit State Analysis Data
            Default_Moving_LoadData(dgv_deck_liveloads);
            Default_Moving_Type_LoadData(dgv_deck_loads);
            Deckslab_User_Input();
            #endregion Default Deck slab Limit State Analysis Data


            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 0;


            pic_deck.BackgroundImage = AstraFunctionOne.ImageCollection.DCP_3935;
            Bridge_Analysis = new Composite_LS_Analysis(iApp);
            Deck = new Composite_Girder_LS(iApp);
            //Deck.tbl_rolledSteelAngles = new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES"));
            rbtn_sec_box.Checked = true;
            rbtn_sec_plate.Checked = true;

            //Deck.tbl_rolledSteelAngles = iApp.Tables.Get_SteelAngles();

            Deck_Load_Analysis_Data();
            //dgv_Ana_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 2.75, 0.2);
            //dgv_Ana_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 6.25, 0.2);

            iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                cmb_Ana_load_type.SelectedIndex = 6;
            }

            dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "1.50", txt_XINCR.Text, txt_Load_Impact.Text);
            dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "4.50", txt_XINCR.Text, txt_Load_Impact.Text);
            //dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "7.50", txt_XINCR.Text, txt_Load_Impact.Text);


            txt_LL_load_gen.Text = (46.0 / 0.2).ToString("0.0");



            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_L_2_ang_section_name, true);
                //iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_L_2_ang_section_name, false);


                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_L_4_ang_section_name, true);
                //iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_L_4_ang_section_name, false);


                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_Deff_ang_section_name, true);
                //iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_Deff_ang_section_name, false);


                iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
                //iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, false);
            }
            else if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_L_2_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_L_2_ang_section_name, false);


                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_L_4_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_L_4_ang_section_name, false);


                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_Deff_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_Deff_ang_section_name, false);

                iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, false);
            }

            else
            {
                iApp.Tables.AISC_SteelAngles.Read_Angle_Sections(ref cmb_L_2_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_L_2_ang_section_name, false);


                iApp.Tables.AISC_SteelAngles.Read_Angle_Sections(ref cmb_L_4_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_L_4_ang_section_name, false);


                iApp.Tables.AISC_SteelAngles.Read_Angle_Sections(ref cmb_Deff_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_Deff_ang_section_name, false);

                iApp.Tables.AISC_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, true);
                //iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_ana_ang_section_name, false);
            }


            if (cmb_L_2_ang_section_name.Items.Count > 0)
                cmb_L_2_ang_section_name.SelectedIndex = 0;
            if (cmb_L_4_ang_section_name.Items.Count > 0)
                cmb_L_4_ang_section_name.SelectedIndex = 0;
            if (cmb_Deff_ang_section_name.Items.Count > 0)
                cmb_Deff_ang_section_name.SelectedIndex = 0;
            if (cmb_ana_ang_section_name.Items.Count > 0)
                cmb_ana_ang_section_name.SelectedIndex = 0;


            Deck_Initialize_InputData();

            cmb_deck_shr.SelectedIndex = 0;
            cmb_deck_des_mom.SelectedIndex = 2;


            #region RCC Abutment
            Abut = new RCC_AbutmentWall(iApp);
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;

            uC_RCC_Abut1.Is_Individual = false;





            uC_AbutmentOpenLS1.SetIApplication(iApp);
            uC_AbutmentPileLS1.SetIApplication(iApp);


            tc_abutment.TabPages.Remove(tab_AbutmentOpenLSM);


            #endregion RCC Abutment



            #region IRC Abutment

            tabControl1.TabPages.Remove(tab_abutment);
            uC_RCC_Abut1.iApp = iApp;
            uC_RCC_Abut1.Load_Data();

            #endregion IRC Abutment



            #region RCC Pier
            cmb_pier_2_k.SelectedIndex = 1;
            rcc_pier = new RccPier(iApp);



            uC_PierDesignLSM1.iApp = iApp;
            uC_PierDesignLSM1.Show_Note = true;
            uC_PierDesignLSM1.Show_Title = true;

            uC_PierDesignWSM1.iApp = iApp;
            uC_PierDesignWSM1.Show_Note = true;
            uC_PierDesignWSM1.Show_Title = true;



            tc_Pier.TabPages.Remove(tab_PierWSM_Text);
            tc_Pier.TabPages.Remove(tab_PierWSM_Excel);




            uC_PierOpenLS1.SetIApplication(iApp);
            uC_PierPileLS1.SetIApplication(iApp);


            tc_Pier.TabPages.Remove(tab_PierPileLSM);



            #endregion RCC Pier


            tbc_girder.TabPages.Remove(tab_orthotropic);

            txt_Ana_NMG.SelectedIndex = 0;
            txt_curve_des_spd_kph.Text = "50";

            rbtn_multiSpan.Checked = true;
            chk_curve.Checked = true;

            //cmb_long_fck.SelectedIndex = 2;
            //cmb_long_fy.SelectedIndex = 1;


            cmb_deck_fck.SelectedIndex = 2;
            cmb_deck_fy.SelectedIndex = 1;

            cmb_abut_fck.SelectedIndex = 2;
            cmb_abut_fy.SelectedIndex = 1;

            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;


            Text_Changed();
            Button_Enable_Disable();
            Show_Steel_SectionProperties();

            uC_Orthotropic1.SetApplication(iApp);


            Set_Project_Name();
            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {

                    IsCreate_Data = false;



                    string chk_file = Path.Combine(Path.Combine(iApp.LastDesignWorkingFolder, Title), "INPUT_DATA.TXT");

                    //if (!File.Exists(chk_file)) chk_file = ofd.FileName;



                    Ana_OpenAnalysisFile(chk_file);
                    Show_ReadMemberLoad(Bridge_Analysis.TotalAnalysis_Input_File);
                    //Open_AnalysisFile(ofd.FileName);
                    Deck_Load_Analysis_Data();
                    Deck_Initialize_InputData();
                    //txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;
                    iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
                    Read_All_Data();

                    //Chiranjit [2013 10 10]
                    IsRead = true;
                    iApp.Read_Form_Record(this, user_path);
                    IsRead = false;

                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        British_Interactive();

                        Default_British_HB_Type_LoadData(dgv_british_loads);
                    }


                    rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                    Open_Create_Data();//Chiranjit [2013 06 25]

                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

                Button_Enable_Disable();
                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option


            //txt_Ana_NMG.SelectedIndex = 0;


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


                if (btn.Name == btn_dwg_deck_slab.Name)
                {
                    //iApp.SetDrawingFile_Path(Deck.drawing_path, "Composite_Bridge", "");
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Composite Deck Slab Drawing"), "COMPOSITE_DECK_LS");
                }
                else if (btn.Name == btn_dwg_steel_plate.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Steel Plate Girder Drawing"), "Composite_Bridge_Steel_Plate");
                    string rep_file = "";
                    if (rbtn_inner_girder.Checked)
                        rep_file = (Deck.rep_file_name_inner);
                    else
                        rep_file = (Deck.rep_file_name_outer);

                    iApp.Form_Drawing_Editor(eBaseDrawings.COMPOSITE_LS_STEEL_PLATE, Drawing_Folder, rep_file).ShowDialog();

                }
                else if (btn.Name == btn_dwg_steel_box.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Steel Box Girder Drawing"), "Composite_Bridge_Steel_Box");

                    string rep_file = "";
                    if (rbtn_inner_girder.Checked)
                        rep_file = (Deck.rep_file_name_inner);
                    else
                        rep_file = (Deck.rep_file_name_outer);

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


                if (btn.Name == btn_dwg_deck_slab.Name)
                {
                    //iApp.SetDrawingFile_Path(Deck.drawing_path, "Composite_Bridge", "");
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Composite Deck Slab Drawing"), "COMPOSITE_DECK_LS");
                }
                else if (btn.Name == btn_dwg_steel_plate.Name)
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

            //if (cmb.Text.Contains("IS")) Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;
            //else if (cmb.Text.Contains("UK")) Deck.tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;
            //else if (cmb.Text.Contains("L")) Deck.tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;

            //else Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;


            if (cmb.Name == cmb_L_2_ang_section_code.Name)
            {
                if (cmb_L_2_ang_section_name.Text.Contains("IS")) Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;
                else if (cmb_L_2_ang_section_name.Text.Contains("UK")) Deck.tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;
                else if (cmb_L_2_ang_section_name.Text.Contains("L")) Deck.tbl_rolledSteelAngles = iApp.Tables.AISC_SteelAngles;

                else Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;


                if (Deck.tbl_rolledSteelAngles.List_Table.Count > 0)
                {
                    cmb_L_2_ang_thk.Items.Clear();
                    for (int i = 0; i < Deck.tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        if (Deck.tbl_rolledSteelAngles.List_Table[i].SectionSize == cmb_L_2_ang_section_code.Text)
                        {
                            if (cmb_L_2_ang_thk.Items.Contains(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness) == false)
                                cmb_L_2_ang_thk.Items.Add(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness);
                        }
                    }
                    cmb_L_2_ang_thk.SelectedIndex = cmb_L_2_ang_thk.Items.Count > 0 ? 0 : -1;
                }
            }
            else if (cmb.Name == cmb_L_4_ang_section_code.Name)
            {
                //Deck.tbl_rolledSteelAngles = cmb_L_4_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;


                if (cmb_L_4_ang_section_name.Text.Contains("IS")) Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;
                else if (cmb_L_4_ang_section_name.Text.Contains("UK")) Deck.tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;
                else if (cmb_L_4_ang_section_name.Text.Contains("L")) Deck.tbl_rolledSteelAngles = iApp.Tables.AISC_SteelAngles;

                else Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;



                if (Deck.tbl_rolledSteelAngles.List_Table.Count > 0)
                {
                    cmb_L_4_ang_thk.Items.Clear();
                    for (int i = 0; i < Deck.tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        if (Deck.tbl_rolledSteelAngles.List_Table[i].SectionSize == cmb_L_4_ang_section_code.Text)
                        {
                            if (cmb_L_4_ang_thk.Items.Contains(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness) == false)
                                cmb_L_4_ang_thk.Items.Add(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness);
                        }
                    }
                }
                cmb_L_4_ang_thk.SelectedIndex = cmb_L_4_ang_thk.Items.Count > 0 ? 0 : -1;
            }
            else if (cmb.Name == cmb_Deff_ang_section_code.Name)
            {
                //Deck.tbl_rolledSteelAngles = cmb_Deff_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;

                if (cmb_Deff_ang_section_name.Text.Contains("IS")) Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;
                else if (cmb_Deff_ang_section_name.Text.Contains("UK")) Deck.tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;
                else if (cmb_Deff_ang_section_name.Text.Contains("L")) Deck.tbl_rolledSteelAngles = iApp.Tables.AISC_SteelAngles;

                else Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;


                if (Deck.tbl_rolledSteelAngles.List_Table.Count > 0)
                {
                    cmb_Deff_ang_thk.Items.Clear();
                    for (int i = 0; i < Deck.tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        if (Deck.tbl_rolledSteelAngles.List_Table[i].SectionSize == cmb_Deff_ang_section_code.Text)
                        {
                            if (cmb_Deff_ang_thk.Items.Contains(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness) == false)
                                cmb_Deff_ang_thk.Items.Add(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness);
                        }
                    }
                }
                cmb_Deff_ang_thk.SelectedIndex = cmb_Deff_ang_thk.Items.Count > 0 ? 0 : -1;
            }
            else if (cmb.Name == cmb_ana_ang_section_code.Name)
            {
                //Deck.tbl_rolledSteelAngles = cmb_Deff_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;

                if (cmb_Deff_ang_section_name.Text.Contains("IS")) Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;
                else if (cmb_Deff_ang_section_name.Text.Contains("UK")) Deck.tbl_rolledSteelAngles = iApp.Tables.BS_SteelAngles;
                else if (cmb_Deff_ang_section_name.Text.Contains("L")) Deck.tbl_rolledSteelAngles = iApp.Tables.AISC_SteelAngles;

                else Deck.tbl_rolledSteelAngles = iApp.Tables.IS_SteelAngles;

                if (Deck.tbl_rolledSteelAngles.List_Table.Count > 0)
                {
                    cmb_Deff_ang_thk.Items.Clear();
                    for (int i = 0; i < Deck.tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        if (Deck.tbl_rolledSteelAngles.List_Table[i].SectionSize == cmb_ana_ang_section_code.Text)
                        {
                            if (cmb_ana_ang_thk.Items.Contains(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness) == false)
                                cmb_ana_ang_thk.Items.Add(Deck.tbl_rolledSteelAngles.List_Table[i].Thickness);
                        }
                    }
                }
                cmb_ana_ang_thk.SelectedIndex = cmb_ana_ang_thk.Items.Count > 0 ? 0 : -1;
            }
        }

        private void cmb_L_2_ang_section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_L_2_ang_section_name.Name)
            {
                cmb_L_2_ang_section_code.Items.Clear();
                if (cmb_L_2_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_L_2_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_2_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_L_2_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_L_2_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_L_2_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_2_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_L_2_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_L_2_ang_section_name.Text.Contains("L"))
                {
                    foreach (var item in iApp.Tables.AISC_SteelAngles.List_Table)
                    {
                        if (cmb_L_2_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_2_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_L_2_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_L_2_ang_section_code.Items.Count > 0)
                {
                    cmb_L_2_ang_section_code.SelectedIndex = 1;
                    cmb_L_2_ang_section_code.SelectedIndex = 0;
                    cmb_L_2_ang_section_code.SelectedItem = "100X100";
                    cmb_L_2_ang_thk.SelectedIndex = cmb_L_2_ang_thk.Items.Contains(10.0) ? cmb_L_2_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_L_2_nos_ang.SelectedIndex = 1;
                }

            }

            if (cmb.Name == cmb_L_4_ang_section_name.Name)
            {
                cmb_L_4_ang_section_code.Items.Clear();
                if (cmb_L_4_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_4_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_L_4_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_L_4_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_4_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_L_4_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_L_4_ang_section_name.Text.Contains("L"))
                {
                    foreach (var item in iApp.Tables.AISC_SteelAngles.List_Table)
                    {
                        if (cmb_L_4_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_L_4_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_L_4_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_L_4_ang_section_code.Items.Count > 0)
                {
                    cmb_L_4_ang_section_code.SelectedIndex = 0;
                    cmb_L_4_ang_section_code.SelectedItem = "100X100";
                    cmb_L_4_ang_thk.SelectedIndex = cmb_L_4_ang_thk.Items.Contains(10.0) ? cmb_L_4_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_L_4_nos_ang.SelectedIndex = 1;
                }
            }
            if (cmb.Name == cmb_Deff_ang_section_name.Name)
            {
                cmb_Deff_ang_section_code.Items.Clear();
                if (cmb_Deff_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_Deff_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Deff_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Deff_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_Deff_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_Deff_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Deff_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Deff_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_Deff_ang_section_name.Text.Contains("L"))
                {
                    foreach (var item in iApp.Tables.AISC_SteelAngles.List_Table)
                    {
                        if (cmb_Deff_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Deff_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Deff_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_Deff_ang_section_code.Items.Count > 0)
                {
                    cmb_Deff_ang_section_code.SelectedIndex = 0;
                    cmb_Deff_ang_section_code.SelectedItem = "100X100";
                    cmb_Deff_ang_thk.SelectedIndex = cmb_Deff_ang_thk.Items.Contains(10.0) ? cmb_Deff_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_Deff_nos_ang.SelectedIndex = 1;
                }
            }

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
                    foreach (var item in iApp.Tables.AISC_SteelAngles.List_Table)
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
                    if (cmb_ana_ang_thk.Items.Count > 0)
                    {
                        cmb_ana_ang_thk.SelectedIndex = cmb_ana_ang_thk.Items.Contains(10.0) ? cmb_ana_ang_thk.Items.IndexOf(10.0) : 0;
                    }
                    cmb_ana_nos_ang.SelectedIndex = 0;
                }
            }

        }

        private void cmb_Ana_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iApp.LiveLoads.Count > 0)
            {
                txt_Ana_X.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].Distance.ToString("f4"); // Chiranjit [2013 05 28] Kolkata
                txt_Load_Impact.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].ImpactFactor.ToString("f3");
            }
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


                Deck_Load_Analysis_Data();
                Deck_Initialize_InputData();
                Button_Enable_Disable();
                //MessageBox.Show("Bending Moments & Shear Forces are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Analysis Result not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        //Chiranjit [2012 05 27]
        #region Abutment
        private void btn_Abutment_Process_Click(object sender, EventArgs e)
        {

            //Chiranjit [2012 07 13]
            Write_All_Data();

            if (Abut == null) Abut = new RCC_AbutmentWall(iApp);
            Abut.FilePath = user_path;
            Abutment_Initialize_InputData();
            Abut.Write_Cantilever__User_input();
            Abut.Calculate_Program(Abut.rep_file_name);
            Abut.Write_Cantilever_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            MessageBox.Show(this, "Report file written in " + Abut.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(Abut.rep_file_name);
            Abut.is_process = true;
            Button_Enable_Disable();

        }
        private void btn_Abutment_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Abut.rep_file_name);
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
            Abut.p = MyList.StringToDouble(txt_abut_p.Text, 0.0);
            Abut.f_ck = MyList.StringToDouble(cmb_abut_fck.Text, 0.0);
            Abut.f_y = MyList.StringToDouble(cmb_abut_fy.Text, 0.0);
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

            #endregion
        }
        #endregion Abutment

        //Chiranjit [2012 06 13]
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
                iApp.Save_Form_Record(this, user_path);
                if (File.Exists(rcc_pier.rep_file_name)) { MessageBox.Show(this, "Report file written in " + rcc_pier.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rcc_pier.rep_file_name); }
                rcc_pier.is_process = true;
            }
            Button_Enable_Disable();
        }
        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            //iapp.SetDrawingFile(user_input_file, "PIER");

            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
            //iapp.RunViewer(drwg_path);
        }

        private void btn_dwg_long_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Name == btn_dwg_abutment.Name)
            {
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "Composite_Abutment");
            }
            else if (b.Name == btn_dwg_rcc_pier.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "Composite_Pier");
            }
            else if (b.Name == btn_dwg_box_abutment.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Box Type Abutment Drawings"), "BOX_ABUTMENT");
            }
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
            rcc_pier.d2 = MyList.StringToDouble(txt_RCC_Pier_DS.Text, 0.0);
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

            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);
            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);
            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);

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
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }
        public double CW { get { return MyList.StringToDouble(txt_Ana_CW.Text, 0.0); } set { txt_Ana_CW.Text = value.ToString("f3"); } }
        public double CL { get { return MyList.StringToDouble(txt_Ana_CL.Text, 0.0); } set { txt_Ana_CL.Text = value.ToString("f3"); } }
        public double CR { get { return MyList.StringToDouble(txt_Ana_CR.Text, 0.0); } set { txt_Ana_CR.Text = value.ToString("f3"); } }
        public double Ds { get { return MyList.StringToDouble(txt_Ana_Ds.Text, 0.0); } set { txt_Ana_Ds.Text = value.ToString("f3"); } }
        public double Y_c { get { return MyList.StringToDouble(txt_Ana_gamma_c.Text, 0.0); } set { txt_Ana_gamma_c.Text = value.ToString("f3"); } }
        public double Y_c_Green { get { return MyList.StringToDouble(txt_Ana_gamma_c_green.Text, 0.0); } set { txt_Ana_gamma_c_green.Text = value.ToString("f3"); } }
        public double Ang { get { return MyList.StringToDouble(txt_Ana_ang.Text, 0.0); } set { txt_Ana_ang.Text = value.ToString("f3"); } }
        public double NMG { get { return MyList.StringToDouble(txt_Ana_NMG.Text, 0.0); } set { txt_Ana_NMG.Text = value.ToString("f3"); } }
        public double DMG { get { return MyList.StringToDouble(txt_sec_L2_Dw.Text, 0.0) / 1000.0; } set { txt_sec_L2_Dw.Text = (value * 1000.0).ToString(); } }
        public double Deff { get { return (DMG - 0.500 - (4 * 0.028 + 3 * 0.028) / 2.0); } }
        public double BMG { get { return MyList.StringToDouble(txt_sec_L2_Bw.Text, 0.0) / 1000.0; } set { txt_sec_L2_Bw.Text = (value * 1000.0).ToString(); } }
        public double NCG { get { return MyList.StringToDouble(txt_Ana_NCG.Text, 0.0); } set { txt_Ana_NCG.Text = value.ToString("f3"); } }
        public double DCG { get { return MyList.StringToDouble(txt_sec_cross_Dw.Text, 0.0) / 1000.0; } set { txt_sec_cross_Dw.Text = (value * 1000.0).ToString(); } }
        public double BCG { get { return MyList.StringToDouble(txt_sec_cross_Bw.Text, 0.0) / 1000.0; } set { txt_sec_cross_Bw.Text = (value * 1000.0).ToString(); } }
        public double Dw { get { return MyList.StringToDouble(txt_Ana_Dw.Text, 0.0); } set { txt_Ana_Dw.Text = value.ToString("f3"); } }
        public double Y_w { get { return MyList.StringToDouble(txt_Ana_gamma_w.Text, 0.0); } set { txt_Ana_gamma_w.Text = value.ToString("f3"); } }
        public double Hc { get { return MyList.StringToDouble(txt_Ana_Hc.Text, 0.0); } set { txt_Ana_Hc.Text = value.ToString("f3"); } }
        public double Wc { get { return MyList.StringToDouble(txt_Ana_Wc.Text, 0.0); } set { txt_Ana_Wc.Text = value.ToString("f3"); } }
        public double Wf { get { return MyList.StringToDouble(txt_Ana_wf.Text, 0.0); } set { txt_Ana_wf.Text = value.ToString("f3"); } }
        public double Hf { get { return MyList.StringToDouble(txt_Ana_hf.Text, 0.0); } set { txt_Ana_hf.Text = value.ToString("f3"); } }
        public double Wk { get { return MyList.StringToDouble(txt_Ana_Wk.Text, 0.0); } set { txt_Ana_Wk.Text = value.ToString("f3"); } }
        public double wr { get { return MyList.StringToDouble(txt_Ana_wr.Text, 0.0); } set { txt_Ana_wr.Text = value.ToString("f3"); } }
        public double swf { get { return 1.0; } }
        public double FMG { get { return MyList.StringToDouble(txt_sec_L2_Bft.Text, 0.0) / 1000.0; } set { txt_sec_L2_Bft.Text = (value * 1000.0).ToString(); } }
        public double TMG { get { return MyList.StringToDouble(txt_sec_L2_Dft.Text, 0.0) / 1000.0; } set { txt_sec_L2_Dft.Text = (value * 1000.0).ToString(); } }
        public double FCG { get { return MyList.StringToDouble(txt_sec_cross_Bft.Text, 0.0) / 1000.0; } set { txt_sec_cross_Dft.Text = (value * 1000.0).ToString(); } }
        public double TCG { get { return MyList.StringToDouble(txt_sec_cross_Dft.Text, 0.0) / 1000.0; } set { txt_sec_cross_Dft.Text = (value * 1000.0).ToString(); } }
        public double Y_S { get { return MyList.StringToDouble(txt_Ana_gamma_s.Text, 0.0); } set { txt_Ana_gamma_s.Text = value.ToString("f3"); } }


        public double Curve_Radius { get { return MyList.StringToDouble(txt_curve_radius.Text, 0.0); } set { txt_curve_radius.Text = value.ToString("f3"); } }


        #endregion Chiranjit [2012 06 20]
        #region Chiranjit [2012 06 20]
        void Text_Changed()
        {

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


            //Chiranjit [2014 09 10]
            txt_deck_width.Text = txt_Ana_B.Text;


            //double SMG = (B - CL - CR) / (NMG - 1);
            double SMG = (B - CL - CR - flange_wi) / (NMG - 1); //Chiranjit [2013 06 25]

            flange_wi = Comp_sections.Section_Cross_Girder.Bfb;


            if (flange_wi < Comp_sections.Section_Cross_Girder.Bft)
                flange_wi = Comp_sections.Section_Cross_Girder.Bft;
            flange_wi = flange_wi / 1000.0;


            double SCG = (L - flange_wi) / (NCG - 1);


            Comp_sections.Spacing_Long_Girder = SMG;
            Comp_sections.Spacing_Cross_Girder = SCG;


            //txt_Long_SMG.Text = SMG.ToString("f3");
            //txt_Long_SCG.Text = SCG.ToString("f3");

            //txt_Cross_SMG.Text = SMG.ToString("f3");
            //txt_Cross_SCG.Text = SCG.ToString("f3");

            txt_Deck_SMG.Text = SMG.ToString("f3");
            txt_Deck_SCG.Text = SCG.ToString("f3");

            //txt__SMG.Text = SMG.ToString("f3");
            //txt_Deck_SCG.Text = SCG.ToString("f3");

            double x_incr = MyList.StringToDouble(txt_XINCR.Text, 0.2);
            double x_dim = Math.Abs(MyList.StringToDouble(txt_Ana_X.Text, 0.2));

            txt_LL_load_gen.Text = ((L + x_dim) / x_incr).ToString("f0");


            //txt_Long_L.Text = L.ToString("f3");
            txt_Deck_L.Text = L.ToString("f3");
            txt_abut_L.Text = L.ToString("f3");
            txt_RCC_Pier_L.Text = L.ToString("f3");

            txt_abut_DMG.Text = (DMG + 0.2).ToString("f3");

            txt_abut_B.Text = B.ToString("f3");
            txt_RCC_Pier__B.Text = B.ToString("f3");
            txt_RCC_Pier___B.Text = B.ToString("f3");

            txt_Deck_CW.Text = B.ToString();
            txt_RCC_Pier_CW.Text = B.ToString();

            txt_Deck_Ds.Text = (Ds * 1000).ToString();
            txt_RCC_Pier_DS.Text = (Ds).ToString();

            txt_Deck_gamma_c.Text = Y_c.ToString();
            txt_abut_gamma_c.Text = Y_c.ToString();
            txt_RCC_Pier_gama_c.Text = Y_c.ToString();
            //txt_Cant_gamma_c.Text = Y_c.ToString();

            //txt_Cross_NMG.Text = NMG.ToString();
            //txt_Deck_NMG.Text = NMG.ToString();
            txt_RCC_Pier_NMG.Text = NMG.ToString();
            txt_RCC_Pier_NP.Text = NMG.ToString();

            //txt_Long_DMG.Text = (DMG * 1000).ToString();
            //txt_Abut_DMG.Text = (DMG + 0.2).ToString();
            //txt_RCC_Pier_DMG.Text = (DMG).ToString();

            //txt_Long_tw_L2.Text = (BMG * 1000).ToString();
            //txt_Cant_BMG.Text = (BMG).ToString();

            txt_Deck_DW.Text = (Dw * 1000).ToString();

            txt_Deck_gamma_wc.Text = Y_w.ToString();
            //txt_Cant_gamma_c.Text = Y_w.ToString();
            //txt_Abut_gamma_b.Text = Y_w.ToString();

            txt_RCC_Pier_Hp.Text = Hc.ToString();

            //txt_Cant_Wp.Text = Wp.ToString();
            txt_RCC_Pier_Wp.Text = Wc.ToString();


            //txt_Cant_a3.Text = (CL - Wp).ToString("f3");
            //txt_abut_DMG.Text = (DMG + TMG + TMG + Ds).ToString("f3");
            //txt_RCC_Pier_DMG.Text = (DMG + TMG + TMG + Ds).ToString("f3");



            txt_abut_DMG.Text = (DMG + Ds - MyList.StringToDouble(txt_abut_d3.Text, 0.0)).ToString("f3");
            txt_RCC_Pier_DMG.Text = DMG.ToString("f3");
            txt_RCC_Pier_DS.Text = Ds.ToString("f3");


            //if (Bs != 0.0)
            //    CW = B - Bs;
            //else
            //    CW = B - 2 * Wp;

            //CW = B - 2 * Wc - Wf;

            if (rbtn_crash_barrier.Checked)
                CW = B - 2 * Wc;
            else if (rbtn_footpath.Checked)
            {
                if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    CW = B - Wf - Wk;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    CW = B - Wf - Wk;
                else
                    CW = B - 2 * Wf;


            }

            #region Deck Slab Inputs
            if (uC_Deckslab_IS1.dgv_deck_user_input.RowCount >= 30)
            {
                uC_Deckslab_IS1.dgv_deck_user_input[1, 0].Value = txt_Ana_L.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 4].Value = txt_Ana_B.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 5].Value = txt_Ana_ang.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 6].Value = txt_Ana_CW.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 9].Value = txt_Ana_Wc.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 10].Value = SMG.ToString("f3");
                uC_Deckslab_IS1.dgv_deck_user_input[1, 11].Value = txt_Ana_Ds.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 15].Value = txt_Ana_Dw.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 16].Value = txt_Ana_NMG.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 20].Value = txt_Ana_NCG.Text;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 27].Value = (Y_c / 10.0).ToString("f3");
                uC_Deckslab_IS1.dgv_deck_user_input[1, 29].Value = (Y_w / 10.0).ToString("f3");


                uC_Deckslab_IS1.dgv_deck_user_input[1, 0].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 4].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 5].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 6].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 9].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 10].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 11].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 15].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 16].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 20].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 27].Style.ForeColor = Color.Red;
                uC_Deckslab_IS1.dgv_deck_user_input[1, 29].Style.ForeColor = Color.Red;


            }
            #endregion Deck Slab Inputs

            //txt_Long_tw_L2.Text = (BMG * 1000).ToString();
            //txt_Long_tw_L4.Text = (BMG * 1000).ToString();
            //txt_Long_tw_Deff.Text = (BMG * 1000).ToString();


            //txt_Long_bf1_L2.Text = (FMG * 1000).ToString();
            //txt_Long_bf2_L2.Text = (FMG * 1000).ToString();
            //txt_Long_bf1_L4.Text = (FMG * 1000).ToString();
            //txt_Long_bf2_L4.Text = (FMG * 1000).ToString();
            //txt_Long_bf1_Deff.Text = (FMG * 1000).ToString();
            //txt_Long_bf2_Deff.Text = (FMG * 1000).ToString();
            //txt_Long_tf2_Deff.Text = (TMG * 1000).ToString();
            //txt_Long_tf1_Deff.Text = (TMG * 1000).ToString();
            //txt_Long_tf2_L4.Text = (TMG * 1000).ToString();
            //txt_Long_tf1_L4.Text = (TMG * 1000).ToString();
            //txt_Long_tf2_L2.Text = (TMG * 1000).ToString();



            uC_Deckslab_BS1.b = B;

            uC_Deckslab_BS1.girder_no = NMG;

            uC_Deckslab_BS1.h = Ds * 1000;




            uC_RCC_Abut1.Length = L;
            uC_RCC_Abut1.Width = B;


            uC_PierDesignLSM1.Left_Span = L.ToString();
            uC_PierDesignLSM1.Right_Span = L.ToString();
            //uC_Abut1.Overhang = og;




            #region Chiranjit [2017 09 18]
            //Abutment Design with Open Foundation n Limit State Method
            #region Abutment Open Foundation
            uC_AbutmentOpenLS1.Span = txt_Ana_L.Text;
            //uC_AbutmentOpenLS1.Exp_Gap = txt_Ana_eg.Text;
            uC_AbutmentOpenLS1.Carriageway_width = txt_Ana_B.Text;


            uC_AbutmentOpenLS1.Railing = txt_Ana_wr.Text;
            uC_AbutmentOpenLS1.Crash_Barrier = txt_Ana_Wc.Text;
            uC_AbutmentOpenLS1.Foot_path = txt_Ana_wf.Text;


            //uC_AbutmentOpenLS1.Bridge_Type = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Girder_Depth = txt_Ana_DMG.Text;
            uC_AbutmentOpenLS1.Slab_Thickness = txt_Ana_Ds.Text;
            uC_AbutmentOpenLS1.Wearing_Coat_Thickness = txt_Ana_Dw.Text;

            uC_AbutmentOpenLS1.Girder_Nos = txt_Ana_NMG.Text;
            //uC_AbutmentOpenLS1.Girder_Spacing = txt_Ana_SMG.Text;

            uC_AbutmentOpenLS1.RCC_Density = (Y_c / 10).ToString("f2");
            //uC_AbutmentOpenLS1.Crash_Barrier_weight = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Wearing_coat_load = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Foot_Path_Live_Load = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Railing_weight = txt_Ana_B.Text;
            #endregion Abutment Open Foundation




            //Chiranjit [2017 09 18]
            //Abutment Design with Pile Foundation n Limit State Method
            #region Abutment Pile Foundation
            uC_AbutmentPileLS1.Span = txt_Ana_L.Text;
            //uC_AbutmentPileLS1.Exp_Gap = txt_Ana_eg.Text;
            uC_AbutmentPileLS1.Carriageway_width = txt_Ana_B.Text;


            uC_AbutmentPileLS1.Railing = txt_Ana_wr.Text;
            uC_AbutmentPileLS1.Crash_Barrier = txt_Ana_Wc.Text;
            uC_AbutmentPileLS1.Foot_path = txt_Ana_wf.Text;


            //uC_AbutmentOpenLS1.Bridge_Type = txt_Ana_B.Text;
            //uC_AbutmentPileLS1.Girder_Depth = txt_Ana_DMG.Text;
            uC_AbutmentPileLS1.Slab_Thickness = txt_Ana_Ds.Text;
            uC_AbutmentPileLS1.Wearing_Coat_Thickness = txt_Ana_Dw.Text;

            uC_AbutmentPileLS1.Girder_Nos = txt_Ana_NMG.Text;
            //uC_AbutmentPileLS1.Girder_Spacing = txt_Ana_SMG.Text;

            uC_AbutmentPileLS1.RCC_Density = (Y_c / 10).ToString("f2");
            //uC_AbutmentOpenLS1.Crash_Barrier_weight = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Wearing_coat_load = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Foot_Path_Live_Load = txt_Ana_B.Text;
            //uC_AbutmentOpenLS1.Railing_weight = txt_Ana_B.Text;
            #endregion Abutment Open Foundation



            //Chiranjit [2017 09 18]
            //Pier Design with Open Foundation n Limit State Method
            #region Pier Open Foundation


            double skw = MyList.Convert_Degree_To_Radian(MyList.StringToDouble(txt_Ana_ang.Text, 0.0));
            double cos_len = Math.Cos(skw);

            uC_PierOpenLS1.SkewAngle = txt_Ana_ang.Text;

            //uC_PierPileLS1.CC_Exp_Joint_Left_Skew = txt_Ana_L.Text * Skew
            uC_PierOpenLS1.CC_Exp_Joint_Left_Skew = (L / cos_len).ToString("f3");

            //uC_PierPileLS1.CC_Exp_Joint_CL_Brg_Left_Skew = txt_Ana_og * Skew
            //uC_PierOpenLS1.CC_Exp_Joint_CL_Brg_Left_Skew = (og / cos_len).ToString("f3");

            //uC_PierOpenLS1.CC_Exp_Gap_Left = txt_Ana_eg.Text;

            uC_PierOpenLS1.CarriageWidth_Left = txt_Ana_B.Text;

            //uC_PierPileLS1.CrashBarierWidth_Nos = 2

            uC_PierOpenLS1.CrashBarierWidth_Left = txt_Ana_Wc.Text;


            //uC_PierPileLS1.FootPathWidth_Nos = 
            //uC_PierOpenLS1.FootPathWidth_Left = txt_Ana_Wf.Text;

            uC_PierOpenLS1.RailingWidth_Left = txt_Ana_Wk.Text;

            uC_PierOpenLS1.CrashBarierHeight_Left = txt_Ana_Hc.Text;

            uC_PierOpenLS1.WearingCoatThickness_Left = txt_Ana_Dw.Text;

            //uC_PierOpenLS1.GirderDepth_Left = txt_Ana_DMG.Text;

            uC_PierOpenLS1.SlabDepth_Left = txt_Ana_Ds.Text;

            //uC_PierOpenLS1.TopFlangeWidth_Left = txt_sec_in_mid_lg_wtf.Text;

            //uC_PierOpenLS1.CrossGirderWidth_Left = txt_sec_int_cg_w.Text;


            uC_PierOpenLS1.Left_Equal_to_Right();

            //uC_PierPileLS1.RCC_Density = (Y_c_dry / 10).ToString("f2");

            #endregion Pier Open Foundation




            //Chiranjit [2017 09 18]
            //Pier Design with Open Foundation n Limit State Method
            #region Pier Pile Foundation


            //double skw = MyList.Convert_Degree_To_Radian(MyList.StringToDouble(txt_Ana_ang.Text, 0.0));
            //double cos_len = Math.Cos(skw);

            uC_PierPileLS1.SkewAngle = txt_Ana_ang.Text;

            //uC_PierPileLS1.CC_Exp_Joint_Left_Skew = txt_Ana_L.Text * Skew
            uC_PierPileLS1.CC_Exp_Joint_Left_Skew = (L / cos_len).ToString("f3");

            //uC_PierPileLS1.CC_Exp_Joint_CL_Brg_Left_Skew = txt_Ana_og * Skew
            //uC_PierPileLS1.CC_Exp_Joint_CL_Brg_Left_Skew = (og / cos_len).ToString("f3");

            //uC_PierPileLS1.CC_Exp_Gap_Left = txt_Ana_eg.Text;

            uC_PierPileLS1.CarriageWidth_Left = txt_Ana_B.Text;

            //uC_PierPileLS1.CrashBarierWidth_Nos = 2

            uC_PierPileLS1.CrashBarierWidth_Left = txt_Ana_Wc.Text;


            //uC_PierPileLS1.FootPathWidth_Nos = 
            //uC_PierPileLS1.FootPathWidth_Left = txt_Ana_Wf.Text;

            uC_PierPileLS1.RailingWidth_Left = txt_Ana_Wk.Text;

            uC_PierPileLS1.CrashBarierHeight_Left = txt_Ana_Hc.Text;

            uC_PierPileLS1.WearingCoatThickness_Left = txt_Ana_Dw.Text;

            //uC_PierPileLS1.GirderDepth_Left = txt_Ana_DMG.Text;

            uC_PierPileLS1.SlabDepth_Left = txt_Ana_Ds.Text;

            //uC_PierPileLS1.TopFlangeWidth_Left = txt_sec_in_mid_lg_wtf.Text;

            //uC_PierPileLS1.CrossGirderWidth_Left = txt_sec_int_cg_w.Text;

            uC_PierPileLS1.Left_Equal_to_Right();

            //uC_PierPileLS1.RCC_Density = (Y_c_dry / 10).ToString("f2");

            #endregion Pier Pile Foundation

            #endregion Chiranjit [2017 09 18]



            Change_LSM_Data();
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {


            //if (rbtn_singleSpan.Checked) txt_multiSpan.Text = txt_Ana_L.Text;

            #region // Calculate Curve Span

            double R = MyList.StringToDouble(txt_curve_radius.Text, 0.0);



            if (R != 0)
            {
                double theta = L * 180 / (R * Math.PI);

                txt_curve_angle.Text = theta.ToString("f2");

                double dvs = MyList.StringToDouble(txt_curve_divs.Text, 0.0);

                txt_curve_ang_incr.Text = (theta / dvs).ToString("f2");


                double V = MyList.StringToDouble(txt_curve_des_spd_kph.Text, 0.0);

                txt_curve_des_spd_mps.Text = (V * 1000 / (60.0 * 60.0)).ToString("f3");

            }
            else
            {
                txt_curve_angle.Text = "0.0";
                txt_curve_ang_incr.Text = "0.0";
                txt_curve_des_spd_mps.Text = "0.0";
            }
            #endregion
            //if (L > 30)
            //    rbtn_sec_box.Checked = true;
            //else
            //    rbtn_sec_plate.Checked = true;
            Text_Changed();
            //txt_Ana_X.Text = "-" + txt_Ana_L.Text; // Chiranjit [2013 05 30]

            double trans = MyList.StringToDouble(txt_res_LL_node_trans.Text, 0.0);
            lbl_displace_check.Visible = (trans != 0.0);
            if ((L / 800.0) < trans)
            {
                lbl_displace_check.Text = string.Format("Span / 800 = {0} / 800 = {1:f5} < {2:f5}, So, NOT OK", L, (L / 800.0), trans);
                lbl_displace_check.ForeColor = Color.Red;
            }
            else
            {
                lbl_displace_check.Text = string.Format("Span / 800 = {0} / 800 = {1:f5} > {2:f5}, So, OK", L, (L / 800.0), trans);
                lbl_displace_check.ForeColor = Color.Green;
            }



            try
            {
                if (((TextBox)sender).Name == txt_Ana_B.Name)
                    CW = B - 1.0;


                for (int i = 0; i < dgv_live_load.RowCount; i++)
                {
                    dgv_live_load[4, i].Value = txt_XINCR.Text;
                    //dgv_live_load[1, i].Value = txt_Ana_X.Text; // Chiranjit [2013 05 30]
                }
            }
            catch (Exception ex) { }
            Show_Steel_SectionProperties();
        }



        private void chk_CheckedChanged(object sender, EventArgs e)
        {
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
                    txt_Ana_Dw.Text = "0.000";
                    txt_Ana_gamma_w.Text = "0.000";
                }
                else
                {
                    txt_Ana_Dw.Text = "0.080";
                    txt_Ana_gamma_w.Text = "22.000";
                }
            }
            else if (rbtn.Name == rbtn_crash_barrier.Name)
            {
                grb_ana_parapet.Enabled = rbtn_crash_barrier.Checked;
                if (!rbtn_crash_barrier.Checked)
                {
                    txt_Ana_Hc.Text = "0.000";
                    txt_Ana_Wc.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hc.Text = "1.200";
                    txt_Ana_Wc.Text = "0.500";
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
                    txt_Ana_wf.Text = "1.000";
                    txt_Ana_hf.Text = "0.250";
                    txt_Ana_Wk.Text = "0.500";
                    txt_Ana_wr.Text = "0.100";
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

        }
        //Chiranjit [2012 06 20]
        private void chk_CheckedChanged_2014_02_25(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            if (chk.Name == chk_WC.Name)
            {
                grb_ana_wc.Enabled = chk.Checked;
                if (grb_ana_wc.Enabled == false)
                {
                    txt_Ana_Dw.Text = "0.000";
                    txt_Ana_gamma_w.Text = "0.000";
                }
                else
                {
                    txt_Ana_Dw.Text = "0.080";
                    txt_Ana_gamma_w.Text = "22.000";
                }
            }
            else if (chk.Name == chk_fp_left.Name)
            {
                grb_ana_parapet.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_Hc.Text = "0.000";
                    txt_Ana_Wc.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hc.Text = "1.200";
                    txt_Ana_Wc.Text = "0.500";
                }
            }
            else if (chk.Name == chk_fp_right.Name)
            {
                grb_ana_sw_fp.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_wf.Text = "0.000";
                    txt_Ana_hf.Text = "0.000";
                    txt_Ana_Wk.Text = "0.000";
                    txt_Ana_wr.Text = "0.000";
                }
                else
                {
                    txt_Ana_wf.Text = "1.000";
                    txt_Ana_hf.Text = "0.250";
                    txt_Ana_Wk.Text = "0.500";
                    txt_Ana_wr.Text = "1.000";
                }
            }

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
                return (MyList.StringToDouble(txt_Ana_m.Text, 10));
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
            double wi2_1 = 2 * SCG * Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_m * Y_S;


            double wi2_2 = 2 * SCG * Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_m * Y_S;
            double wi2_3 = 2 * SCG * Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m * Y_S;


            wi2 = wi2_1 + wi2_2 + wi2_3;

            list.Add(string.Format("wi2 = 2 * (SCG * AREA End Section) * Y_S +  2 * (SCG * AREA L4 Section) * Y_S +  2 * (SCG * AREA Mid Section) * Y_S"));

            list.Add(string.Format("    = 2 * ({0:f3} * {1:f3}) * {2:f3} +  2 * ({0:f3} * {3:f3}) * {2:f3}  +  2 * ({0:f3} * {4:f3}) * {2:f3}",
               SCG,
               Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
               Y_S,
               Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
               Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m));

            list.Add(string.Format(""));
            list.Add(string.Format("    = {0:f3} kN", wi2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders"));
            wi3 = wi1 + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kN.",
                wi1, wi2, wi3));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL"));
            wi4 = wi3 / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kN/m.",
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
            list.Add(string.Format("wiu = wi4*NIG/NIM = {0:f3} * {1}/70 = {2:f3} kN/m. = {3:f3} Ton/m.",
                wi4, NIG, wiu, (wiu = wiu / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));

            list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} Ton/m.",
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
            list.Add(string.Format("wou = wou*swf = {0:f3}*{1:f3} = {2:f4} Ton/m.",
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
            wc1 = Comp_sections.Section_Cross_Girder.Area_in_Sq_m * Y_S;

            //list.Add(string.Format("wc1 = SMG * (DCG * BCG + 2 * FCG * TCG) * Y_S = {0:f3} * ({1:f3} * {2:f3} + 2 * {3:f3} * {4:f3}) * {5:f3} = {6:f3} kN.",
            //    SMG, DCG, BCG, FCG, TCG, Y_S, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format("wc1 = Cross Section Area * Y_S"));
            list.Add(string.Format("    = {0:f6} * {1:f4}", Comp_sections.Section_Cross_Girder.Area_in_Sq_m, Y_S));
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

            if (rbtn_steel_deck.Checked)
            {

                Y_deck = Y_S;
                list.Add(string.Format("Steel Plate Deckslab used, Y = {0:f2}", Y_deck));
            }




            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));
            if (rbtn_steel_deck.Checked)
            {
                list.Add(string.Format("//Load from RCC Deck Slab (Steel Plate)"));

            }
            else
                list.Add(string.Format("//Load from RCC Deck Slab (Dry Concrete)"));


            wi1 = SMG * SCG * (Ds * Y_c + Dw * Y_deck);
            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c, Dw, Y_deck));
            list.Add(string.Format("   = {0:f3} kN.", wi1));
            list.Add(string.Format(""));


            if (rbtn_steel_deck.Checked)
                list.Add(string.Format("//Load from RCC Deck Slab (Green Concrete)"));
            else
                list.Add(string.Format("//Load from RCC Deck Slab (Steel Plate)"));


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
            double wi2_1 = 2 * SCG * Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_m * Y_S;
            double wi2_2 = 2 * SCG * Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_m * Y_S;
            double wi2_3 = 2 * SCG * Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m * Y_S;


            wi2 = wi2_1 + wi2_2 + wi2_3;

            list.Add(string.Format("wi2 = 2 * (SCG * AREA End Section) * Y_S +  2 * (SCG * AREA L4 Section) * Y_S +  2 * (SCG * AREA Mid Section) * Y_S"));

            list.Add(string.Format("    = 2 * ({0:f3} * {1:f3}) * {2:f3} +  2 * ({0:f3} * {3:f3}) * {2:f3}  +  2 * ({0:f3} * {4:f3}) * {2:f3}",
               SCG,
               Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
               Y_S,
               Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
               Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m));

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
            list.Add(string.Format("   = {0:f3} kN.", wo1));
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
            wc1 = Comp_sections.Section_Cross_Girder.Area_in_Sq_m * Y_S;

            //list.Add(string.Format("wc1 = SMG * (DCG * BCG + 2 * FCG * TCG) * Y_S = {0:f3} * ({1:f3} * {2:f3} + 2 * {3:f3} * {4:f3}) * {5:f3} = {6:f3} kN.",
            //    SMG, DCG, BCG, FCG, TCG, Y_S, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format("wc1 = Cross Section Area * Y_S"));
            list.Add(string.Format("    = {0:f6} * {1:f4}", Comp_sections.Section_Cross_Girder.Area_in_Sq_m, Y_S));
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
            //list.Add(string.Format("                JOINT LOAD"));
            //list.Add(string.Format("                13 TO 21 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                24 TO 32 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                35 TO 43 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                46 TO 54 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                57 TO 65 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                68 TO 76 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                79 TO 87 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                90 TO 98 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                101 TO 109 FZ -{0:f4}", wjl));


            //member_load.Add(string.Format("JOINT LOAD"));
            //member_load.Add(string.Format("13 TO 21 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("24 TO 32 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("35 TO 43 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("46 TO 54 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("57 TO 65 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("68 TO 76 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("79 TO 87 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("90 TO 98 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("101 TO 109 FZ -{0:f4}", wjl));

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
            member_load.Add(string.Format("LOAD 3 SIDL"));
            member_load.Add(string.Format("MEMBER LOAD"));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, sidl * 0.2 / 10)); //Chiranjit [2013 06 07]

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
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {
                        Bridge_Analysis.Live_Load_List = new List<LoadData>(iApp.LiveLoads.ToArray());
                    }
                    else
                    {
                        //Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(Bridge_Analysis.Straight_LL_File), "ll.txt"));
                        Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(long_ll);
                    }
                //}

                double dd = 0.0;
                List<double> max_lds = new List<double>();

                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {

                    foreach (var item in all_loads)
                    {
                        dd = 0.0;
                        foreach (var item2 in item)
                        {
                            if(item2.StartsWith("TYPE"))
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
                    Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

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





                    Transverse_load.Add(string.Format("{0} FX {1:f4}", Bridge_Analysis.support_inner_joints, (Math.Min(_FX, _FZ)*2 / tot_sup))); // 
                    Transverse_load.Add(string.Format("{0} FZ -{1:f4}", Bridge_Analysis.support_inner_joints, Math.Max(_FX, _FZ)*2 / tot_sup)); // 



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
            deck_member_load.Add(wo3 / 100);
            deck_member_load.Add(wo2 / 100);
            deck_member_load.Add(wo11);

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
        private void btn_Cant_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Cant.rep_file_name);
        }

        #endregion Chiranjit [2012 06 10]

        private void cmb_deck_select_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplied ld;

            ComboBox cmb = sender as ComboBox;

            if (cmb.Name == cmb_deck_select_load.Name)
            {
                ld = LoadApplied.Get_Applied_Load(cmb_deck_select_load.Text);

                txt_Deck_WL.Text = ld.Applied_Load.ToString();
                txt_Deck_load_length.Text = ld.LoadLength.ToString();
                txt_Deck_load_width.Text = ld.LoadWidth.ToString();
            }
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

        #region Chiranjit [2012 07 06]

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        //IApplication iApp = null;
        //double B = 0.0;
        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {

            try
            {
                //iApp = app;
                DL_Analysis_Rep = DL_Analysis_Report_file;
                LL_Analysis_Rep = LL_Analysis_Report_file;
                Supports = supports.Replace(",", " ");

                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);
                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }


            //B = abut_width;
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


            int _jnt_no = 0;
            for (int i = 0; i < mlist.Count / 2; i++)
            {
                try
                {
                    //sr = DL_support_reactions.Get_Data(mlist.GetInt(i));

                    _jnt_no = mlist.GetInt(i);

                    var im = Bridge_Analysis.DL_Analysis.GetJoint_ShearForce(_jnt_no, 1);

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
            //else if (txt.Name == txt_dead_kN_m.Name)
            //{
            //txt_abut_w5.Text = txt_dead_kN_m.Text;
            txt_pier_2_P2.Text = txt_dead_kN_m.Text;
            //}
            //else if (txt.Name == txt_live_kN_m.Name)
            //{
            //txt_abut_w6.Text = txt_live_kN_m.Text;
            txt_pier_2_P3.Text = txt_live_kN_m.Text;
            //}
            //else if (txt.Name == txt_final_vert_rec_kN.Name)
            //{
            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mx_kN.Name)
            //{
            txt_RCC_Pier_Mx1.Text = txt_final_Mx_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mz_kN.Name)
            //{
            txt_RCC_Pier_Mz1.Text = txt_final_Mz_kN.Text;


            txt_abut_B.Text = txt_RCC_Pier__B.Text = txt_RCC_Pier___B.Text = txt_Ana_B.Text;

            txt_RCC_Pier_L.Text = txt_abut_L.Text = txt_Ana_L.Text;


            uC_RCC_Abut1.Deadload_Reaction = txt_max_vert_reac_kN.Text;

            uC_PierDesignWSM1.Left_Span_Force = (MyList.StringToDouble(txt_left_total_vert_reac.Text, 0.0) * 10).ToString();
            uC_PierDesignWSM1.Right_Span_Force = (MyList.StringToDouble(txt_right_total_vert_reac.Text, 0.0) * 10).ToString();




            uC_PierDesignLSM1.Total_weight_of_superstructure = txt_final_vert_reac.Text;

            uC_PierDesignLSM1.Left_Span_Vertical_Load = txt_sidl_left_total_vert_reac.Text;
            uC_PierDesignLSM1.Right_Span_Vertical_Load = txt_sidl_right_total_vert_reac.Text;


            uC_PierDesignLSM1.Left_Span_Moment_Mx = txt_sidl_left_total_Mx.Text;
            uC_PierDesignLSM1.Right_Span_Moment_Mx = txt_sidl_right_total_Mx.Text;

            uC_PierDesignLSM1.Left_Span_Moment_Mz = txt_sidl_left_total_Mz.Text;
            uC_PierDesignLSM1.Right_Span_Moment_Mz = txt_sidl_right_total_Mz.Text;


            #region New Design for Limit state Method

            uC_AbutmentOpenLS1.DL_MTT = txt_max_Mz.Text;
            uC_AbutmentOpenLS1.DL_MLL = txt_max_Mx.Text;
            uC_AbutmentOpenLS1.SIDL_MTT = txt_sidl_final_Mz.Text;
            uC_AbutmentOpenLS1.SIDL_MLL = txt_sidl_final_Mx.Text;


            uC_AbutmentPileLS1.DL_MTT = txt_max_Mz.Text;
            uC_AbutmentPileLS1.DL_MLL = txt_max_Mx.Text;
            uC_AbutmentPileLS1.SIDL_MTT = txt_sidl_final_Mz.Text;
            uC_AbutmentPileLS1.SIDL_MLL = txt_sidl_final_Mx.Text;



            uC_PierOpenLS1.DL_Force = txt_max_vert_reac_kN.Text;
            uC_PierOpenLS1.SIDL_Force = txt_sidl_final_vert_rec_kN.Text;


            //uC_PierPileLS1.DL_Force = txt_max_vert_reac_kN.Text;
            //uC_PierPileLS1.SIDL_Force = txt_sidl_final_vert_rec_kN.Text;

            //uC_PierOpenLS1.DL




            #region Bearing Forces




            #region Calculate Bearing Forces
            double max_dl = 0.0;
            double min_dl = 0.0;
            double max_sidl = 0.0;
            double max_ll = 0.0;
            double val = 0.0;
            int i = 0;
            try
            {

                for (i = 0; i < dgv_left_des_frc.RowCount - 1; i++)
                {
                    val = MyList.StringToDouble(dgv_left_des_frc[1, i].Value.ToString(), 0.0);

                    if(i == 0)
                    {
                        max_dl = val;
                        min_dl = val;
                    }
                    if (val > max_dl) max_dl = val;
                    if (min_dl > val) min_dl = val;
                }
                for (i = 0; i < dgv_right_des_frc.RowCount - 1; i++)
                {
                    val = MyList.StringToDouble(dgv_right_des_frc[1, i].Value.ToString(), 0.0);
                    if (val > max_dl) max_dl = val;

                    if (i == 0)
                    {
                        max_dl = val;
                        min_dl = val;
                    }
                    if (val > max_dl) max_dl = val;
                    if (min_dl > val) min_dl = val;
                }


                val = 0.0;
                for (i = 0; i < dgv_sidl_left_des_frc.RowCount - 1; i++)
                {
                    val = MyList.StringToDouble(dgv_sidl_left_des_frc[1, i].Value.ToString(), 0.0);
                    if (val > max_sidl) max_sidl = val;
                }
                for (i = 0; i < dgv_sidl_right_des_frc.RowCount - 1; i++)
                {
                    val = MyList.StringToDouble(dgv_sidl_right_des_frc[1, i].Value.ToString(), 0.0);
                    if (val > max_sidl) max_sidl = val;
                }

                val = 0.0;
                for (i = 0; i < dgv_right_end_design_forces.RowCount-1; i++)
                {
                    val = MyList.StringToDouble(dgv_right_end_design_forces[1, i].Value.ToString(), 0.0);
                    if (val > max_ll) max_ll = val;
                }

                txt_brg_max_VR_Ton.Text = (max_dl + max_sidl + max_ll).ToString("f2");
                txt_brg_max_DL_Ton.Text = (max_dl + max_sidl).ToString("f2");

            }
            catch (Exception exx) { }
            #endregion Calculate Bearing Forces


            if (chk_curve.Checked)
            {
                //txt_max_vert_reac
                //txt_brg_max_VR_kN.Text = txt_max_vert_reac.Text;


                double VR = MyList.StringToDouble(txt_brg_max_VR_Ton.Text, 0.0) * 10;
                double DL = MyList.StringToDouble(txt_brg_max_DL_Ton.Text, 0.0) * 10;
                double HRT = MyList.StringToDouble(txt_brg_max_HRT_Ton.Text, 0.0) * 10;
                double HRL = MyList.StringToDouble(txt_brg_max_HRL_Ton.Text, 0.0) * 10;



                txt_brg_max_VR_kN.Text = VR.ToString("f3");
                txt_brg_max_DL_kN.Text = DL.ToString("f3");
                txt_brg_max_HRT_kN.Text = HRT.ToString("f3");
                txt_brg_max_HRL_kN.Text = HRL.ToString("f3");


                uC_BRD1.txt_1_Nnorm.Text = VR.ToString("f3");
                uC_BRD1.txt_1_Nmin.Text = DL.ToString("f3");
                uC_BRD1.txt_1_Hlatn.Text = HRT.ToString("f3");


                uC_BRD1.txt_VMABL_1_Nnorm.Text = VR.ToString("f3");
                uC_BRD1.txt_VMABL_1_Nmin.Text = DL.ToString("f3");
                uC_BRD1.txt_VMABL_1_Hingn.Text = HRL.ToString("f3");


                uC_BRD1.txt_VBAB_1_Nnorm.Text = VR.ToString("f3");
                uC_BRD1.txt_VBAB_1_Nmin.Text = DL.ToString("f3"); 
                uC_BRD1.txt_VBAB_1_Hlatn.Text = HRT.ToString("f3");
                uC_BRD1.txt_VBAB_1_Hingn.Text = HRL.ToString("f3");

                uC_BRD1.txt_VFB_1_Nnorm.Text = VR.ToString("f3");
                uC_BRD1.txt_VFB_1_Nmin.Text = DL.ToString("f3");
                uC_BRD1.txt_VFB_1_Hlatn.Text = HRT.ToString("f3");
                uC_BRD1.txt_VFB_1_Hingn.Text = HRL.ToString("f3");

            }

            #endregion SS









            #endregion New Design for Limit state Method





        }
        #endregion View Force

        #region frm_Pier_ViewDesign_Forces
        string analysis_rep = "";
        SupportReactionTable support_reactions = null;
        string Left_support = "";
        string Right_support = "";
        public void frm_Pier_ViewDesign_Forces(string Analysis_Report_file, string left_support, string right_support)
        {
            try
            {
                analysis_rep = Analysis_Report_file;

                Left_support = left_support.Replace(",", " ");
                Right_support = right_support.Replace(",", " ");

                support_reactions = new SupportReactionTable(iApp, analysis_rep);

                Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }

        private void frm_ViewDesign_Forces_Load()
        {
            support_reactions = new SupportReactionTable(iApp, analysis_rep);
            try
            {
                Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }


        void Show_and_Save_Data()
        {
            
            if (!File.Exists(analysis_rep)) return;
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

            MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

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

            if (Bridge_Analysis.DL_Analysis == null) return;

            BridgeMemberAnalysis DeadLoad_Analysis = Bridge_Analysis.DL_Analysis;

            double _vert_load, _mx, _mz;

            _vert_load = _mx = _mz = 0.0;
            int _jnt_no = 0;


            dgv_left_end_design_forces.Rows.Clear();// Clear All Rows


            double max_dl = 0.0;
            double max_sidl = 0.0;
            double max_ll = 0.0;

            double wsm_tot_dl = 0.0;
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = DeadLoad_Analysis.GetJoint_ShearForce(_jnt_no, 2);
                var mx = DeadLoad_Analysis.GetJoint_Torsion(_jnt_no, 2);
                var mz = DeadLoad_Analysis.GetJoint_MomentForce(_jnt_no, 2);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;

                if (Math.Abs(shr.Force) > max_dl)
                {
                    max_dl = Math.Abs(shr.Force);
                }

                //dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);
                dgv_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);
                dgv_left_end_design_forces.Rows.Add(_jnt_no, _vert_load);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }


            txt_dead_vert_reac_ton.Text = tot_left_vert_reac.ToString("f3");
            list_arr.Add("");


            //Chiranjit [2012 07 06]
            //Change unit kN to Ton
            //tot_left_vert_reac /= 10.0;
            //tot_left_Mx /= 10.0;
            //tot_left_Mz /= 10.0;

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
                _jnt_no = mlist.GetInt(i);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = DeadLoad_Analysis.GetJoint_ShearForce(_jnt_no, 2);
                var mx = DeadLoad_Analysis.GetJoint_Torsion(_jnt_no, 2);
                var mz = DeadLoad_Analysis.GetJoint_MomentForce(_jnt_no, 2);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;

                if (Math.Abs(shr.Force) > max_dl)
                {
                    max_dl = Math.Abs(shr.Force);
                }

                dgv_right_des_frc.Rows.Add(_jnt_no, Math.Abs(_vert_load), _mx, _mz);

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


            txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");



            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            //txt_final_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");



             
            txt_max_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_final_Mx_kN.Text + " kN-m");
            //txt_final_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");


             
            txt_max_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");
             


            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");

            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;



            dgv_sidl_left_des_frc.Rows.Clear();
            dgv_sidl_right_des_frc.Rows.Clear();

            mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);
                var shr = DeadLoad_Analysis.GetJoint_ShearForce(_jnt_no, 3);
                var mx = DeadLoad_Analysis.GetJoint_Torsion(_jnt_no, 3);
                var mz = DeadLoad_Analysis.GetJoint_MomentForce(_jnt_no, 3);


                _vert_load = shr;
                _mx = mx;
                _mz = mz;

                if (Math.Abs(shr.Force) > max_sidl)
                {
                    max_sidl = Math.Abs(shr.Force);
                }

                dgv_sidl_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            txt_sidl_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_sidl_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_sidl_left_total_Mz.Text = tot_left_Mz.ToString("0.000");


            tot_right_vert_reac = 0.0;
            tot_right_Mx = 0.0;
            tot_right_Mz = 0.0;

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);
                var shr = DeadLoad_Analysis.GetJoint_ShearForce(_jnt_no, 3);
                var mx = DeadLoad_Analysis.GetJoint_Torsion(_jnt_no, 3);
                var mz = DeadLoad_Analysis.GetJoint_MomentForce(_jnt_no, 3);


                _vert_load = shr;
                _mx = mx;
                _mz = mz;



                if (Math.Abs(shr.Force) > max_sidl)
                {
                    max_sidl = Math.Abs(shr.Force);
                }


                //if (_vert_load < shr.Force)
                //{
                //    _vert_load = shr.Force;
                //}
                //if (_mx < mx)
                //{
                //    _mx = mx;
                //}
                //if (_mz < mz)
                //{
                //    _mz = mz;
                //}

                dgv_sidl_right_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load); ;
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
            }

            txt_sidl_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_sidl_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_sidl_right_total_Mz.Text = tot_right_Mz.ToString("0.000");




            txt_sidl_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_sidl_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            txt_sidl_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_sidl_final_Mx_kN.Text = (MyList.StringToDouble(txt_sidl_final_Mx.Text, 0.0) * 10.0).ToString("f3");



            txt_sidl_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_sidl_final_Mz_kN.Text = (MyList.StringToDouble(txt_sidl_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            txt_brg_max_VR_Ton.Text = (max_dl + max_sidl).ToString("f2");



            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        void Show_and_Save_Data_2017_06_21()
        {
            if (!File.Exists(analysis_rep)) return;
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

            MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

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
            //tot_left_vert_reac /= 10.0;
            //tot_left_Mx /= 10.0;
            //tot_left_Mz /= 10.0;

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
            //tot_right_vert_reac /= 10.0;
            //tot_right_Mx /= 10.0;
            //tot_right_Mz /= 10.0;
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


            #region Chiranjit [2017 06 11]
            txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");
            #endregion Chiranjit [2017 06 11]



            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            //txt_final_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");





            #region Chiranjit [2017 06 11]
            txt_max_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");
            #endregion Chiranjit [2017 06 11]





            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_final_Mx_kN.Text + " kN-m");
            //txt_final_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            #region Chiranjit [2017 06 11]

            txt_max_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]



            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");





            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        void Show_and_Save_Data1_2017_06_11()
        {
            if (!File.Exists(analysis_rep)) return;
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

            MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

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
            //tot_left_vert_reac /= 10.0;
            //tot_left_Mx /= 10.0;
            //tot_left_Mz /= 10.0;

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
            //tot_right_vert_reac /= 10.0;
            //tot_right_Mx /= 10.0;
            //tot_right_Mz /= 10.0;
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

            txt_final_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_final_Mx_kN.Text + " kN-m");
            txt_final_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");





            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }
        #endregion frm_Pier_ViewDesign_Forces

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
                Deck.FilePath = user_path;
                //Cant.FilePath = user_path;
                Abut.FilePath = user_path;
                rcc_pier.FilePath = user_path;


                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {

                    uC_Deckslab_IS1.iApp = iApp;
                    uC_Deckslab_IS1.user_path = user_path;

                    uC_Deckslab_IS1.deck_member_load = deck_member_load;
                    uC_Deckslab_IS1.L = L;
                    uC_Deckslab_IS1.NMG = NMG;
                    uC_Deckslab_IS1.SMG = SMG;
                    //uC_Deckslab_IS1.os = os;
                    uC_Deckslab_IS1.CL = CL;
                    uC_Deckslab_IS1.Ds = Ds;
                    uC_Deckslab_IS1.B = B;


                    uC_Deckslab_IS1.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
                    uC_Deckslab_IS1.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
                    uC_Deckslab_IS1.Skew_Angle = MyList.StringToDouble(dgv_deck_user_input[1, 5].Value.ToString(), 0.0);
                    uC_Deckslab_IS1.Number_Of_Long_Girder = MyList.StringToInt(txt_Ana_NMG.Text, 4);
                    uC_Deckslab_IS1.Number_Of_Cross_Girder = MyList.StringToInt(txt_Ana_NCG.Text, 3);
                    uC_Deckslab_IS1.WidthBridge = L / (NCG - 1);
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
                if(rbtn_multiSpan.Checked)
                {
                    txt_multiSpan.Text = "22,22,22";
                    txt_Ana_L.Text = "66.0";
                }
                else
                    txt_Ana_L.Text = "36.0";


                txt_Ana_B.Text = "11.85";
                txt_Ana_CW.Text = "11.0";
                //txt_Ana_NMG.Text = "4";
                txt_Ana_NMG.Text = "3";
                txt_curve_des_spd_kph.Text = "50";


                Deckslab_User_Input();

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
            #region MyRegion

            //steel_section.Nb = MyList.StringToInt(txt_sec_Nb.Text, 1);
            //steel_section.S = MyList.StringToDouble(txt_sec_S.Text, 0.0);
            //steel_section.Bw = MyList.StringToDouble(txt_sec_Bw.Text, 0.0);
            //steel_section.Dw = MyList.StringToDouble(txt_sec_Dw.Text, 0.0);
            //steel_section.Bft = MyList.StringToDouble(txt_sec_Bft.Text, 0.0);
            //steel_section.Dft = MyList.StringToDouble(txt_sec_Dft.Text, 0.0);
            //steel_section.Bfb = MyList.StringToDouble(txt_sec_Bfb.Text, 0.0);
            //steel_section.Dfb = MyList.StringToDouble(txt_sec_Dfb.Text, 0.0);
            //steel_section.Bt = MyList.StringToDouble(txt_sec_Bt.Text, 0.0);
            //steel_section.Dt = MyList.StringToDouble(txt_sec_Dt.Text, 0.0);
            //steel_section.Bb = MyList.StringToDouble(txt_sec_Bb.Text, 0.0);
            //steel_section.Db = MyList.StringToDouble(txt_sec_Db.Text, 0.0);
            //steel_section.Bs1 = MyList.StringToDouble(txt_sec_Bs1.Text, 0.0);
            //steel_section.Ds1 = MyList.StringToDouble(txt_sec_Ds1.Text, 0.0);
            //steel_section.Bs2 = MyList.StringToDouble(txt_sec_Bs2.Text, 0.0);
            //steel_section.Ds2 = MyList.StringToDouble(txt_sec_Ds2.Text, 0.0);
            //steel_section.Bs3 = MyList.StringToDouble(txt_sec_Bs3.Text, 0.0);
            //steel_section.Ds3 = MyList.StringToDouble(txt_sec_Ds3.Text, 0.0);
            //steel_section.Bs4 = MyList.StringToDouble(txt_sec_Bs4.Text, 0.0);
            //steel_section.Ds4 = MyList.StringToDouble(txt_sec_Ds4.Text, 0.0);
            //steel_section.Ixbs = MyList.StringToDouble(txt_sec_Ixbs.Text, 0.0);
            //steel_section.Iybs = MyList.StringToDouble(txt_sec_Iybs.Text, 0.0);

            #endregion

            List<string> result = new List<string>();

            #region End Span

            Comp_sections.Section_Long_Girder_at_End_Span.Nb = MyList.StringToInt(txt_sec_end_Nb.Text, 1);
            Comp_sections.Section_Long_Girder_at_End_Span.S = MyList.StringToDouble(txt_sec_end_S.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bw = MyList.StringToDouble(txt_sec_end_Bw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dw = MyList.StringToDouble(txt_sec_end_Dw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bft = MyList.StringToDouble(txt_sec_end_Bft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dft = MyList.StringToDouble(txt_sec_end_Dft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bfb = MyList.StringToDouble(txt_sec_end_Bfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dfb = MyList.StringToDouble(txt_sec_end_Dfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bt = MyList.StringToDouble(txt_sec_end_Bt.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Dt = MyList.StringToDouble(txt_sec_end_Dt.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Bb = MyList.StringToDouble(txt_sec_end_Bb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Db = MyList.StringToDouble(txt_sec_end_Db.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_End_Span.Bs1 = MyList.StringToDouble(txt_sec_end_Bs1.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Ds1 = MyList.StringToDouble(txt_sec_end_Ds1.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_End_Span.Bs2 = MyList.StringToDouble(txt_sec_end_Bs2.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Ds2 = MyList.StringToDouble(txt_sec_end_Ds2.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_End_Span.Bs3 = MyList.StringToDouble(txt_sec_end_Bs3.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Ds3 = MyList.StringToDouble(txt_sec_end_Ds3.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_End_Span.Bs4 = MyList.StringToDouble(txt_sec_end_Bs4.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_End_Span.Ds4 = MyList.StringToDouble(txt_sec_end_Ds4.Text, 0.0);


            #region Error Message Set End Section


            steel_section = Comp_sections.Section_Long_Girder_at_End_Span;


            //if (rbtn_sec_box.Checked)
            //{
            //    if (steel_section.S < (steel_section.Dw - 200))
            //        erp_error.SetError(txt_sec_end_S, string.Format("Spacing cannot be less than (Web Depth - 200) = {0} mm", (steel_section.Dw - 200)));
            //    else if (steel_section.S < 200)
            //        erp_error.SetError(txt_sec_end_S, string.Format("Spacing cannot be less than 200 mm. Spacing = Web Depth - 200", (steel_section.Dw - 200)));
            //    else
            //        erp_error.SetError(txt_sec_end_S, "");
            //}



            //if (steel_section.Bt > (steel_section.Bft - 20))
            //    erp_error.SetError(txt_sec_end_Bt, string.Format("Value must be less than equal to (Bft - 20) = {0}", (steel_section.Bft - 20)));
            //else
            //    erp_error.SetError(txt_sec_end_Bt, "");

            //if (steel_section.Bb > (steel_section.Bfb - 20))
            //    erp_error.SetError(txt_sec_end_Bb, string.Format("Value must be less than equal to (Bfb - 20) = {0}", (steel_section.Bfb - 20)));
            //else
            //    erp_error.SetError(txt_sec_end_Bb, "");



            //if (steel_section.Ds1 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_end_Ds1, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_end_Ds1, "");

            //if (steel_section.Ds2 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_end_Ds2, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_end_Ds2, "");

            //if (steel_section.Ds3 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_end_Ds3, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_end_Ds3, "");

            //if (steel_section.Ds4 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_end_Ds4, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_end_Ds4, "");
            #endregion

            //Comp_sections.Section_Long_Girder_at_End_Span.Ixbs = MyList.StringToDouble(txt_sec_Ixbs.Text, 0.0);
            //Comp_sections.Section_Long_Girder_at_End_Span.Iybs = MyList.StringToDouble(txt_sec_Iybs.Text, 0.0);

            #endregion End Span

            #region L4_Span

            Comp_sections.Section_Long_Girder_at_L4_Span.Nb = MyList.StringToInt(txt_sec_L4_Nb.Text, 1);
            Comp_sections.Section_Long_Girder_at_L4_Span.S = MyList.StringToDouble(txt_sec_L4_S.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_L4_Span.Bw = MyList.StringToDouble(txt_sec_L4_Bw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dw = MyList.StringToDouble(txt_sec_L4_Dw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bft = MyList.StringToDouble(txt_sec_L4_Bft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dft = MyList.StringToDouble(txt_sec_L4_Dft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bfb = MyList.StringToDouble(txt_sec_L4_Bfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dfb = MyList.StringToDouble(txt_sec_L4_Dfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bt = MyList.StringToDouble(txt_sec_L4_Bt.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Dt = MyList.StringToDouble(txt_sec_L4_Dt.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bb = MyList.StringToDouble(txt_sec_L4_Bb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Db = MyList.StringToDouble(txt_sec_L4_Db.Text, 0.0);


            Comp_sections.Section_Long_Girder_at_L4_Span.Bs1 = MyList.StringToDouble(txt_sec_L4_Bs1.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Ds1 = MyList.StringToDouble(txt_sec_L4_Ds1.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bs2 = MyList.StringToDouble(txt_sec_L4_Bs2.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Ds2 = MyList.StringToDouble(txt_sec_L4_Ds2.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bs3 = MyList.StringToDouble(txt_sec_L4_Bs3.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Ds3 = MyList.StringToDouble(txt_sec_L4_Ds3.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Bs4 = MyList.StringToDouble(txt_sec_L4_Bs4.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_L4_Span.Ds4 = MyList.StringToDouble(txt_sec_L4_Ds4.Text, 0.0);
            //Comp_sections.Section_Long_Girder_at_L4_Span.Ixbs = MyList.StringToDouble(txt_sec_Ixbs.Text, 0.0);
            //Comp_sections.Section_Long_Girder_at_L4_Span.Iybs = MyList.StringToDouble(txt_sec_Iybs.Text, 0.0);





            #region Error Message Set L4

            steel_section = Comp_sections.Section_Long_Girder_at_L4_Span;



            //if (rbtn_sec_box.Checked)
            //{
            //    if (steel_section.S < (steel_section.Dw - 200))
            //        erp_error.SetError(txt_sec_L4_S, string.Format("Spacing cannot be less than (Web Depth - 200) = {0} mm", (steel_section.Dw - 200)));
            //    else if (steel_section.S < 200)
            //        erp_error.SetError(txt_sec_L4_S, string.Format("Spacing cannot be less than 200 mm. Spacing = Web Depth - 200", (steel_section.Dw - 200)));
            //    else
            //        erp_error.SetError(txt_sec_L4_S, "");
            //}


            //if (steel_section.Bt > (steel_section.Bft - 20))
            //    erp_error.SetError(txt_sec_L4_Bt, string.Format("Value must be less than equal to (Bft - 20) = {0}", (steel_section.Bft - 20)));
            //else
            //    erp_error.SetError(txt_sec_L4_Bt, "");

            //if (steel_section.Bb > (steel_section.Bfb - 20))
            //    erp_error.SetError(txt_sec_L4_Bb, string.Format("Value must be less than equal to (Bfb - 20) = {0}", (steel_section.Bfb - 20)));
            //else
            //    erp_error.SetError(txt_sec_L4_Bb, "");






            //if (steel_section.Ds1 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_L4_Ds1, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_L4_Ds1, "");

            //if (steel_section.Ds2 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_L4_Ds2, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_L4_Ds2, "");

            //if (steel_section.Ds3 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_L4_Ds3, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_L4_Ds3, "");

            //if (steel_section.Ds4 > (steel_section.Dw - 100))
            //    erp_error.SetError(txt_sec_L4_Ds4, string.Format("Value must be less than equal to(Dw - 50 - 50) = {0}", (steel_section.Dw - 100)));
            //else
            //    erp_error.SetError(txt_sec_L4_Ds4, "");
            #endregion

            #endregion L4_Span

            #region Mid_Span



            Comp_sections.Section_Long_Girder_at_Mid_Span.S = MyList.StringToDouble(txt_sec_L2_S.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_Mid_Span.Nb = MyList.StringToInt(txt_sec_L2_Nb.Text, 1);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bw = MyList.StringToDouble(txt_sec_L2_Bw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dw = MyList.StringToDouble(txt_sec_L2_Dw.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bft = MyList.StringToDouble(txt_sec_L2_Bft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dft = MyList.StringToDouble(txt_sec_L2_Dft.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bfb = MyList.StringToDouble(txt_sec_L2_Bfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dfb = MyList.StringToDouble(txt_sec_L2_Dfb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bt = MyList.StringToDouble(txt_sec_L2_Bt.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Dt = MyList.StringToDouble(txt_sec_L2_Dt.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bb = MyList.StringToDouble(txt_sec_L2_Bb.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Db = MyList.StringToDouble(txt_sec_L2_Db.Text, 0.0);

            Comp_sections.Section_Long_Girder_at_Mid_Span.Bs1 = MyList.StringToDouble(txt_sec_L2_Bs1.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Ds1 = MyList.StringToDouble(txt_sec_L2_Ds1.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bs2 = MyList.StringToDouble(txt_sec_L2_Bs2.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Ds2 = MyList.StringToDouble(txt_sec_L2_Ds2.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bs3 = MyList.StringToDouble(txt_sec_L2_Bs3.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Ds3 = MyList.StringToDouble(txt_sec_L2_Ds3.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Bs4 = MyList.StringToDouble(txt_sec_L2_Bs4.Text, 0.0);
            Comp_sections.Section_Long_Girder_at_Mid_Span.Ds4 = MyList.StringToDouble(txt_sec_L2_Ds4.Text, 0.0);
            //Comp_sections.Section_Long_Girder_at_Mid_Span.Ixbs = MyList.StringToDouble(txt_sec_Ixbs.Text, 0.0);
            //Comp_sections.Section_Long_Girder_at_Mid_Span.Iybs = MyList.StringToDouble(txt_sec_Iybs.Text, 0.0);



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
            Comp_sections.Section_Cross_Girder.Bt = MyList.StringToDouble(txt_sec_cross_Bt.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Dt = MyList.StringToDouble(txt_sec_cross_Dt.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bb = MyList.StringToDouble(txt_sec_cross_Bb.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Db = MyList.StringToDouble(txt_sec_cross_Db.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bs1 = MyList.StringToDouble(txt_sec_cross_Bs1.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Ds1 = MyList.StringToDouble(txt_sec_cross_Ds1.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bs2 = MyList.StringToDouble(txt_sec_cross_Bs2.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Ds2 = MyList.StringToDouble(txt_sec_cross_Ds2.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bs3 = MyList.StringToDouble(txt_sec_cross_Bs3.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Ds3 = MyList.StringToDouble(txt_sec_cross_Ds3.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Bs4 = MyList.StringToDouble(txt_sec_cross_Bs4.Text, 0.0);
            Comp_sections.Section_Cross_Girder.Ds4 = MyList.StringToDouble(txt_sec_cross_Ds4.Text, 0.0);
            //Comp_sections.Section_Cross_Girder.Ixbs = MyList.StringToDouble(txt_sec_cross_Ixbs.Text, 0.0);
            //Comp_sections.Section_Cross_Girder.Iybs = MyList.StringToDouble(txt_sec_cross_Iybs.Text, 0.0);

            Comp_sections.Section_Cross_Girder.Steel_Unit_Weight = Y_S / 10;
            Comp_sections.Section_Long_Girder_at_End_Span.Steel_Unit_Weight = Y_S / 10;
            Comp_sections.Section_Long_Girder_at_L4_Span.Steel_Unit_Weight = Y_S / 10;
            Comp_sections.Section_Long_Girder_at_Mid_Span.Steel_Unit_Weight = Y_S / 10;
            steel_section = Comp_sections.Section_Cross_Girder;

            #endregion Cross Girder

            Comp_sections.Angle_Section = iApp.Tables.Get_AngleData_FromTable(cmb_ana_ang_section_name.Text, cmb_ana_ang_section_code.Text, MyList.StringToDouble(cmb_ana_ang_thk.Text, 10));

            Comp_sections.Ds = Ds * 1000;
            Comp_sections.m = m;


            //BMG  = 
            //public double NCG { get { return MyList.StringToDouble(txt_Ana_NCG.Text, 0.0); } set { txt_Ana_NCG.Text = value.ToString("f3"); } }
            //public double DCG { get { return MyList.StringToDouble(txt_Ana_DCG.Text, 0.0); } set { txt_Ana_DCG.Text = value.ToString("f3"); } }
            //public double BCG { get { return MyList.StringToDouble(txt_Ana_BCG.Text, 0.0); } set { txt_Ana_BCG.Text = value.ToString("f3"); } }
            //public double Dw { get { return MyList.StringToDouble(txt_Ana_Dw.Text, 0.0); } set { txt_Ana_Dw.Text = value.ToString("f3"); } }
            //public double Y_w { get { return MyList.StringToDouble(txt_Ana_gamma_w.Text, 0.0); } set { txt_Ana_gamma_w.Text = value.ToString("f3"); } }
            //public double Hp { get { return MyList.StringToDouble(txt_Ana_Hp.Text, 0.0); } set { txt_Ana_Hp.Text = value.ToString("f3"); } }
            //public double Wp { get { return MyList.StringToDouble(txt_Ana_Wp.Text, 0.0); } set { txt_Ana_Wp.Text = value.ToString("f3"); } }
            //public double Bs { get { return MyList.StringToDouble(txt_Ana_Bs.Text, 0.0); } set { txt_Ana_Bs.Text = value.ToString("f3"); } }
            //public double Hs { get { return MyList.StringToDouble(txt_Ana_Hs.Text, 0.0); } set { txt_Ana_Hs.Text = value.ToString("f3"); } }
            //public double Wps { get { return MyList.StringToDouble(txt_Ana_Wps.Text, 0.0); } set { txt_Ana_Wps.Text = value.ToString("f3"); } }
            //public double Hps { get { return MyList.StringToDouble(txt_Ana_Hps.Text, 0.0); } set { txt_Ana_Hps.Text = value.ToString("f3"); } }
            //public double swf { get { return MyList.StringToDouble(txt_Ana_swf.Text, 0.0); } set { txt_Ana_swf.Text = value.ToString("f3"); } }
            //public double FMG { get { return MyList.StringToDouble(txt_Ana_FMG.Text, 0.0); } set { txt_Ana_FMG.Text = value.ToString("f3"); } }
            //public double TMG { get { return MyList.StringToDouble(txt_Ana_TMG.Text, 0.0); } set { txt_Ana_TMG.Text = value.ToString("f3"); } }
            //public double FCG { get { return MyList.StringToDouble(txt_ana_FCG.Text, 0.0); } set { txt_ana_FCG.Text = value.ToString("f3"); } }
            //public double TCG { get { return MyList.StringToDouble(txt_ana_TCG.Text, 0.0); } set { txt_ana_TCG.Text = value.ToString("f3"); } }






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
                Comp_sections.Section_Long_Girder_at_End_Span.Area_in_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_End_Span.Iyy_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m.ToString("f5"));



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
                Comp_sections.Section_Long_Girder_at_L4_Span.Area_in_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Iyy_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m.ToString("f5"));



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
                Comp_sections.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Iyy_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_m.ToString("f5"));



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
                Comp_sections.Section_Cross_Girder.Area_in_Sq_m.ToString("f5"),
                Comp_sections.Section_Cross_Girder.Ixx_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Cross_Girder.Iyy_in_Sq_Sq_m.ToString("f5"),
                Comp_sections.Section_Cross_Girder.Izz_in_Sq_Sq_m.ToString("f5"));



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
        public CompositeSection Steel_Composite_Section()
        {

            CompositeSection steel_composite_sections = new CompositeSection();

            #region End Span

            steel_composite_sections.Section_Long_Girder_at_End_Span.Nb = MyList.StringToInt(txt_steel_end_Nb.Text, 1);
            steel_composite_sections.Section_Long_Girder_at_End_Span.S = MyList.StringToDouble(txt_steel_end_S.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bw = MyList.StringToDouble(txt_steel_end_Bw.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Dw = MyList.StringToDouble(txt_steel_end_Dw.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bft = MyList.StringToDouble(txt_steel_end_Bft.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Dft = MyList.StringToDouble(txt_steel_end_Dft.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bfb = MyList.StringToDouble(txt_steel_end_Bfb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Dfb = MyList.StringToDouble(txt_steel_end_Dfb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bt = MyList.StringToDouble(txt_steel_end_Bt.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Dt = MyList.StringToDouble(txt_steel_end_Dt.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bb = MyList.StringToDouble(txt_steel_end_Bb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Db = MyList.StringToDouble(txt_steel_end_Db.Text, 0.0);

            steel_composite_sections.Section_Long_Girder_at_End_Span.Bs1 = MyList.StringToDouble(txt_steel_end_Bs1.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Ds1 = MyList.StringToDouble(txt_steel_end_Ds1.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bs2 = MyList.StringToDouble(txt_steel_end_Bs2.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Ds2 = MyList.StringToDouble(txt_steel_end_Ds2.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bs3 = MyList.StringToDouble(txt_steel_end_Bs3.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Ds3 = MyList.StringToDouble(txt_steel_end_Ds3.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Bs4 = MyList.StringToDouble(txt_steel_end_Bs4.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_End_Span.Ds4 = MyList.StringToDouble(txt_steel_end_Ds4.Text, 0.0);
            //Steel_sections.Section_Long_Girder_at_End_Span.Ixbs = MyList.StringToDouble(txt_steel_Ixbs.Text, 0.0);
            //Steel_sections.Section_Long_Girder_at_End_Span.Iybs = MyList.StringToDouble(txt_steel_Iybs.Text, 0.0);

            steel_composite_sections.Section_Long_Girder_at_End_Span.NumberOfGirder = (int)NMG;
            steel_composite_sections.Section_Long_Girder_at_End_Span.Length = L;

            #region Error Message Set End Section


            steel_section = steel_composite_sections.Section_Long_Girder_at_End_Span;



            #endregion



            #endregion End Span

            #region L4_Span

            steel_composite_sections.Section_Long_Girder_at_L4_Span.Nb = MyList.StringToInt(txt_steel_L4_Nb.Text, 1);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.S = MyList.StringToDouble(txt_steel_L4_S.Text, 0.0);

            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bw = MyList.StringToDouble(txt_steel_L4_Bw.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Dw = MyList.StringToDouble(txt_steel_L4_Dw.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bft = MyList.StringToDouble(txt_steel_L4_Bft.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Dft = MyList.StringToDouble(txt_steel_L4_Dft.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bfb = MyList.StringToDouble(txt_steel_L4_Bfb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Dfb = MyList.StringToDouble(txt_steel_L4_Dfb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bt = MyList.StringToDouble(txt_steel_L4_Bt.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Dt = MyList.StringToDouble(txt_steel_L4_Dt.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bb = MyList.StringToDouble(txt_steel_L4_Bb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Db = MyList.StringToDouble(txt_steel_L4_Db.Text, 0.0);


            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs1 = MyList.StringToDouble(txt_steel_L4_Bs1.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds1 = MyList.StringToDouble(txt_steel_L4_Ds1.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs2 = MyList.StringToDouble(txt_steel_L4_Bs2.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds2 = MyList.StringToDouble(txt_steel_L4_Ds2.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs3 = MyList.StringToDouble(txt_steel_L4_Bs3.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds3 = MyList.StringToDouble(txt_steel_L4_Ds3.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs4 = MyList.StringToDouble(txt_steel_L4_Bs4.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds4 = MyList.StringToDouble(txt_steel_L4_Ds4.Text, 0.0);
            //Steel_sections.Section_Long_Girder_at_L4_Span.Ixbs = MyList.StringToDouble(txt_steel_Ixbs.Text, 0.0);
            //Steel_sections.Section_Long_Girder_at_L4_Span.Iybs = MyList.StringToDouble(txt_steel_Iybs.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_L4_Span.NumberOfGirder = (int)NMG;
            steel_composite_sections.Section_Long_Girder_at_L4_Span.Length = L;

            #region Error Message Set L4

            steel_section = steel_composite_sections.Section_Long_Girder_at_L4_Span;


            #endregion

            #endregion L4_Span



            #region Mid_Span



            steel_composite_sections.Section_Long_Girder_at_Mid_Span.S = MyList.StringToDouble(txt_steel_L2_S.Text, 0.0);

            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Nb = MyList.StringToInt(txt_steel_L2_Nb.Text, 1);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bw = MyList.StringToDouble(txt_steel_L2_Bw.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dw = MyList.StringToDouble(txt_steel_L2_Dw.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bft = MyList.StringToDouble(txt_steel_L2_Bft.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dft = MyList.StringToDouble(txt_steel_L2_Dft.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bfb = MyList.StringToDouble(txt_steel_L2_Bfb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dfb = MyList.StringToDouble(txt_steel_L2_Dfb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bt = MyList.StringToDouble(txt_steel_L2_Bt.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dt = MyList.StringToDouble(txt_steel_L2_Dt.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bb = MyList.StringToDouble(txt_steel_L2_Bb.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Db = MyList.StringToDouble(txt_steel_L2_Db.Text, 0.0);

            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs1 = MyList.StringToDouble(txt_steel_L2_Bs1.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds1 = MyList.StringToDouble(txt_steel_L2_Ds1.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs2 = MyList.StringToDouble(txt_steel_L2_Bs2.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds2 = MyList.StringToDouble(txt_steel_L2_Ds2.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs3 = MyList.StringToDouble(txt_steel_L2_Bs3.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds3 = MyList.StringToDouble(txt_steel_L2_Ds3.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs4 = MyList.StringToDouble(txt_steel_L2_Bs4.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds4 = MyList.StringToDouble(txt_steel_L2_Ds4.Text, 0.0);
            //Steel_sections.Section_Long_Girder_at_Mid_Span.Ixbs = MyList.StringToDouble(txt_steel_Ixbs.Text, 0.0);
            //Steel_sections.Section_Long_Girder_at_Mid_Span.Iybs = MyList.StringToDouble(txt_steel_Iybs.Text, 0.0);
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.NumberOfGirder = (int)NMG;
            steel_composite_sections.Section_Long_Girder_at_Mid_Span.Length = L;

            #region Error Message Set Mid


            steel_section = steel_composite_sections.Section_Long_Girder_at_Mid_Span;

            #endregion

            #endregion

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
            txt_sec_end_Bt.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bt.ToString();
            txt_sec_end_Dt.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dt.ToString();
            txt_sec_end_Bb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bb.ToString();
            txt_sec_end_Db.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Db.ToString();

            txt_sec_end_Bs1.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs1.ToString();
            txt_sec_end_Ds1.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds1.ToString();
            txt_sec_end_Bs2.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs2.ToString();
            txt_sec_end_Ds2.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds2.ToString();
            txt_sec_end_Bs3.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs3.ToString();
            txt_sec_end_Ds3.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds3.ToString();
            txt_sec_end_Bs4.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs4.ToString();
            txt_sec_end_Ds4.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds4.ToString();
            txt_sec_Ixbs.Text = //steel_composite_sections.Section_Long_Girder_at_End_Span.Ixbs.ToString();
            txt_sec_Iybs.Text = //steel_composite_sections.Section_Long_Girder_at_End_Span.Iybs.ToString();

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
            txt_sec_L4_Bt.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bt.ToString();
            txt_sec_L4_Dt.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dt.ToString();
            txt_sec_L4_Bb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bb.ToString();
            txt_sec_L4_Db.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Db.ToString();


            txt_sec_L4_Bs1.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs1.ToString();
            txt_sec_L4_Ds1.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds1.ToString();
            txt_sec_L4_Bs2.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs2.ToString();
            txt_sec_L4_Ds2.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds2.ToString();
            txt_sec_L4_Bs3.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs3.ToString();
            txt_sec_L4_Ds3.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds3.ToString();
            txt_sec_L4_Bs4.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs4.ToString();
            txt_sec_L4_Ds4.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds4.ToString();
            txt_sec_Ixbs.Text = //steel_composite_sections.Section_Long_Girder_at_L4_Span.Ixbs.ToString();
            txt_sec_Iybs.Text = //steel_composite_sections.Section_Long_Girder_at_L4_Span.Iybs.ToString();

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
            txt_sec_L2_Bt.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bt.ToString();
            txt_sec_L2_Dt.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dt.ToString();
            txt_sec_L2_Bb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bb.ToString();
            txt_sec_L2_Db.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Db.ToString();

            txt_sec_L2_Bs1.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs1.ToString();
            txt_sec_L2_Ds1.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds1.ToString();
            txt_sec_L2_Bs2.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs2.ToString();
            txt_sec_L2_Ds2.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds2.ToString();
            txt_sec_L2_Bs3.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs3.ToString();
            txt_sec_L2_Ds3.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds3.ToString();
            txt_sec_L2_Bs4.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs4.ToString();
            txt_sec_L2_Ds4.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds4.ToString();

            #endregion




        }
        public void Set_Design_Composite_Section(CompositeSection steel_composite_sections)
        {
            #region End Span

            txt_steel_end_Nb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Nb.ToString();
            txt_steel_end_S.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.S.ToString();
            txt_steel_end_Bw.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bw.ToString();
            txt_steel_end_Dw.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dw.ToString();
            txt_steel_end_Bft.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bft.ToString();
            txt_steel_end_Dft.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dft.ToString();
            txt_steel_end_Bfb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bfb.ToString();
            txt_steel_end_Dfb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dfb.ToString();
            txt_steel_end_Bt.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bt.ToString();
            txt_steel_end_Dt.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Dt.ToString();
            txt_steel_end_Bb.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bb.ToString();
            txt_steel_end_Db.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Db.ToString();

            txt_steel_end_Bs1.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs1.ToString();
            txt_steel_end_Ds1.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds1.ToString();
            txt_steel_end_Bs2.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs2.ToString();
            txt_steel_end_Ds2.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds2.ToString();
            txt_steel_end_Bs3.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs3.ToString();
            txt_steel_end_Ds3.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds3.ToString();
            txt_steel_end_Bs4.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Bs4.ToString();
            txt_steel_end_Ds4.Text = steel_composite_sections.Section_Long_Girder_at_End_Span.Ds4.ToString();

            #endregion End Span

            #region L4_Span

            txt_steel_L4_Nb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Nb.ToString();
            txt_steel_L4_S.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.S.ToString();

            txt_steel_L4_Bw.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bw.ToString();
            txt_steel_L4_Dw.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dw.ToString();
            txt_steel_L4_Bft.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bft.ToString();
            txt_steel_L4_Dft.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dft.ToString();
            txt_steel_L4_Bfb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bfb.ToString();
            txt_steel_L4_Dfb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dfb.ToString();
            txt_steel_L4_Bt.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bt.ToString();
            txt_steel_L4_Dt.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Dt.ToString();
            txt_steel_L4_Bb.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bb.ToString();
            txt_steel_L4_Db.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Db.ToString();


            txt_steel_L4_Bs1.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs1.ToString();
            txt_steel_L4_Ds1.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds1.ToString();
            txt_steel_L4_Bs2.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs2.ToString();
            txt_steel_L4_Ds2.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds2.ToString();
            txt_steel_L4_Bs3.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs3.ToString();
            txt_steel_L4_Ds3.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds3.ToString();
            txt_steel_L4_Bs4.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Bs4.ToString();
            txt_steel_L4_Ds4.Text = steel_composite_sections.Section_Long_Girder_at_L4_Span.Ds4.ToString();

            #endregion L4_Span

            #region Mid_Span



            txt_steel_L2_S.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.S.ToString();

            txt_steel_L2_Nb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Nb.ToString();
            txt_steel_L2_Bw.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bw.ToString();
            txt_steel_L2_Dw.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dw.ToString();
            txt_steel_L2_Bft.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bft.ToString();
            txt_steel_L2_Dft.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dft.ToString();
            txt_steel_L2_Bfb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bfb.ToString();
            txt_steel_L2_Dfb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dfb.ToString();
            txt_steel_L2_Bt.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bt.ToString();
            txt_steel_L2_Dt.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Dt.ToString();
            txt_steel_L2_Bb.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bb.ToString();
            txt_steel_L2_Db.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Db.ToString();

            txt_steel_L2_Bs1.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs1.ToString();
            txt_steel_L2_Ds1.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds1.ToString();
            txt_steel_L2_Bs2.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs2.ToString();
            txt_steel_L2_Ds2.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds2.ToString();
            txt_steel_L2_Bs3.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs3.ToString();
            txt_steel_L2_Ds3.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds3.ToString();
            txt_steel_L2_Bs4.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Bs4.ToString();
            txt_steel_L2_Ds4.Text = steel_composite_sections.Section_Long_Girder_at_Mid_Span.Ds4.ToString();

            #endregion
        }

        private void rbtn_sec_plate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_sec_plate.Checked)
            {
                if (!tc_steel.TabPages.Contains(tab_SteelWorksheet))
                    tc_steel.TabPages.Insert(0, tab_SteelWorksheet);


                pic_section.BackgroundImage = Properties.Resources.Steel_Plate_Section;
                txt_sec_Nb.Text = "1";
                txt_sec_end_Nb.Text = "1";
                txt_sec_L4_Nb.Text = "1";
                txt_sec_L2_Nb.Text = "1";
                txt_sec_cross_Nb.Text = "1";
                txt_sec_end_S.Text = "0";
                txt_sec_L4_S.Text = "0";
                txt_sec_L2_S.Text = "0";
                txt_sec_cross_S.Text = "0";
                txt_sec_cross_S.Enabled = false;
                txt_sec_end_S.Enabled = false;
                txt_sec_L4_S.Enabled = false;
                txt_sec_L2_S.Enabled = false;
                txt_sec_cross_S.Enabled = false;


                if (IsCreate_Data)
                {
                    #region Chiranjit [2013 07 08] Defaulf Data for Plate Girders

                    //txt_Ana_L.Text = "19.58";
                    //txt_Ana_B.Text = "12.0";
                    //txt_Ana_CL.Text = "1.075";
                    //txt_Ana_CR.Text = "1.075";
                    //txt_Ana_NMG.Text = "4";
                    //txt_Ana_NCG.Text = "5";
                    //txt_Ana_ang.Text = "26";


                    txt_Ana_L.Text = "36.0";
                    txt_Ana_B.Text = "11.85";
                    txt_Ana_CL.Text = "1.5";
                    txt_Ana_CR.Text = "1.5";
                    txt_Ana_NMG.Text = "3";
                    txt_Ana_NCG.Text = "5";
                    txt_Ana_ang.Text = "0";

                    #region End Section
                    txt_sec_Nb.Text = "1";
                    txt_sec_end_S.Text = "0.0";
                    txt_sec_end_Bw.Text = "45";
                    txt_sec_end_Dw.Text = "1600";
                    txt_sec_end_Bft.Text = "500";
                    txt_sec_end_Dft.Text = "30";
                    txt_sec_end_Bfb.Text = "500";
                    txt_sec_end_Dfb.Text = "30";
                    txt_sec_end_Bt.Text = "480";
                    txt_sec_end_Dt.Text = "30";
                    txt_sec_end_Bb.Text = "480";
                    txt_sec_end_Db.Text = "30";

                    txt_sec_end_Bs1.Text = "40";
                    txt_sec_end_Ds1.Text = "1500";

                    txt_sec_end_Bs2.Text = "40";
                    txt_sec_end_Ds2.Text = "1500";

                    txt_sec_end_Bs3.Text = "40";
                    txt_sec_end_Ds3.Text = "1500";

                    txt_sec_end_Bs4.Text = "40";
                    txt_sec_end_Ds4.Text = "1500";
                    #endregion End Section


                    #region L4 Section
                    txt_sec_Nb.Text = "1";
                    txt_sec_L4_S.Text = "0.0";
                    txt_sec_L4_Bw.Text = "45";
                    txt_sec_L4_Dw.Text = "1600";
                    txt_sec_L4_Bft.Text = "500";
                    txt_sec_L4_Dft.Text = "30";
                    txt_sec_L4_Bfb.Text = "500";
                    txt_sec_L4_Dfb.Text = "30";
                    txt_sec_L4_Bt.Text = "480";
                    txt_sec_L4_Dt.Text = "30";
                    txt_sec_L4_Bb.Text = "480";
                    txt_sec_L4_Db.Text = "30";

                    txt_sec_L4_Bs1.Text = "40";
                    txt_sec_L4_Ds1.Text = "1500";

                    txt_sec_L4_Bs2.Text = "40";
                    txt_sec_L4_Ds2.Text = "1500";

                    txt_sec_L4_Bs3.Text = "40";
                    txt_sec_L4_Ds3.Text = "1500";

                    txt_sec_L4_Bs4.Text = "40";
                    txt_sec_L4_Ds4.Text = "1500";
                    #endregion End Section


                    #region L2 Section
                    txt_sec_Nb.Text = "1";
                    txt_sec_L2_S.Text = "0.0";
                    txt_sec_L2_Bw.Text = "45";
                    txt_sec_L2_Dw.Text = "1600";
                    txt_sec_L2_Bft.Text = "500";
                    txt_sec_L2_Dft.Text = "30";
                    txt_sec_L2_Bfb.Text = "500";
                    txt_sec_L2_Dfb.Text = "30";
                    txt_sec_L2_Bt.Text = "480";
                    txt_sec_L2_Dt.Text = "30";
                    txt_sec_L2_Bb.Text = "480";
                    txt_sec_L2_Db.Text = "30";

                    txt_sec_L2_Bs1.Text = "40";
                    txt_sec_L2_Ds1.Text = "1500";

                    txt_sec_L2_Bs2.Text = "40";
                    txt_sec_L2_Ds2.Text = "1500";

                    txt_sec_L2_Bs3.Text = "40";
                    txt_sec_L2_Ds3.Text = "1500";

                    txt_sec_L2_Bs4.Text = "40";
                    txt_sec_L2_Ds4.Text = "1500";
                    #endregion End Section



                    #endregion Chiranjit [2013 07 08]
                }

            }
            else
            {

                if (tc_steel.TabPages.Contains(tab_SteelWorksheet))
                    tc_steel.TabPages.Remove(tab_SteelWorksheet);


                pic_section.BackgroundImage = Properties.Resources.Steel_Box_Section;

                txt_sec_Nb.Text = "2";
                txt_sec_end_Nb.Text = "2";
                txt_sec_L4_Nb.Text = "2";
                txt_sec_L2_Nb.Text = "2";
                //txt_sec_cross_Nb.Text = "1";

                txt_sec_end_S.Text = (MyList.StringToDouble(txt_sec_end_Dw.Text, 0.0) - 200.0).ToString();
                txt_sec_L4_S.Text = (MyList.StringToDouble(txt_sec_L4_Dw.Text, 0.0) - 200.0).ToString();
                txt_sec_L2_S.Text = (MyList.StringToDouble(txt_sec_L2_Dw.Text, 0.0) - 200.0).ToString();
                txt_sec_cross_S.Text = (MyList.StringToDouble(txt_sec_cross_Dw.Text, 0.0) - 200.0).ToString();
                txt_sec_cross_S.Enabled = true;
                txt_sec_end_S.Enabled = true;
                txt_sec_L4_S.Enabled = true;
                txt_sec_L2_S.Enabled = true;
                txt_sec_cross_S.Enabled = true;



                double s = MyList.StringToDouble(txt_sec_end_S.Text, 0.0);
                double bft = MyList.StringToDouble(txt_sec_end_Bft.Text, 0.0);





                if (IsCreate_Data)
                {
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {
                        dgv_live_load.Rows.Clear();
                        dgv_live_load.Rows.Add(cmb_Ana_load_type.Items[1], txt_Ana_X.Text, txt_Y.Text, "1.50", txt_XINCR.Text, txt_Load_Impact.Text);
                        dgv_live_load.Rows.Add(cmb_Ana_load_type.Items[1], txt_Ana_X.Text, txt_Y.Text, "4.50", txt_XINCR.Text, txt_Load_Impact.Text);
                    }

                    #region Chiranjit [2013 07 08] Defaulf Data for Plate Girders

                    txt_Ana_L.Text = "46.0";
                    txt_Ana_B.Text = "12.5";
                    txt_Ana_CL.Text = "2.075";
                    txt_Ana_CR.Text = "2.075";
                    txt_Ana_NMG.Text = "3";
                    txt_Ana_NCG.Text = "5";

                    #region End Section
                    //txt_sec_Nb.Text = "2";
                    txt_sec_end_Bw.Text = "45";
                    txt_sec_end_Dw.Text = "1600";
                    txt_sec_end_S.Text = "1400";
                    txt_sec_end_Bft.Text = "500";
                    txt_sec_end_Dft.Text = "30";
                    txt_sec_end_Bfb.Text = "500";
                    txt_sec_end_Dfb.Text = "30";
                    txt_sec_end_Bt.Text = "480";
                    txt_sec_end_Dt.Text = "30";
                    txt_sec_end_Bb.Text = "480";
                    txt_sec_end_Db.Text = "30";

                    txt_sec_end_Bs1.Text = "30";
                    txt_sec_end_Ds1.Text = "1500";

                    txt_sec_end_Bs2.Text = "30";
                    txt_sec_end_Ds2.Text = "1500";

                    txt_sec_end_Bs3.Text = "30";
                    txt_sec_end_Ds3.Text = "1500";

                    txt_sec_end_Bs4.Text = "30";
                    txt_sec_end_Ds4.Text = "1500";
                    #endregion End Section


                    #region End Section
                    //txt_sec_Nb.Text = "2";
                    txt_sec_L4_Bw.Text = "45";
                    txt_sec_L4_Dw.Text = "1600";
                    txt_sec_L4_S.Text = "1400";
                    txt_sec_L4_Bft.Text = "500";
                    txt_sec_L4_Dft.Text = "30";
                    txt_sec_L4_Bfb.Text = "500";
                    txt_sec_L4_Dfb.Text = "30";
                    txt_sec_L4_Bt.Text = "480";
                    txt_sec_L4_Dt.Text = "30";
                    txt_sec_L4_Bb.Text = "480";
                    txt_sec_L4_Db.Text = "30";

                    txt_sec_L4_Bs1.Text = "30";
                    txt_sec_L4_Ds1.Text = "1500";

                    txt_sec_L4_Bs2.Text = "30";
                    txt_sec_L4_Ds2.Text = "1500";

                    txt_sec_L4_Bs3.Text = "30";
                    txt_sec_L4_Ds3.Text = "1500";

                    txt_sec_L4_Bs4.Text = "30";
                    txt_sec_L4_Ds4.Text = "1500";
                    #endregion End Section

                    #region End Section
                    //txt_sec_Nb.Text = "2";
                    txt_sec_L2_Bw.Text = "45";
                    txt_sec_L2_Dw.Text = "1600";
                    txt_sec_L2_S.Text = "1400";
                    txt_sec_L2_Bft.Text = "500";
                    txt_sec_L2_Dft.Text = "30";
                    txt_sec_L2_Bfb.Text = "500";
                    txt_sec_L2_Dfb.Text = "30";
                    txt_sec_L2_Bt.Text = "480";
                    txt_sec_L2_Dt.Text = "30";
                    txt_sec_L2_Bb.Text = "480";
                    txt_sec_L2_Db.Text = "30";

                    txt_sec_L2_Bs1.Text = "30";
                    txt_sec_L2_Ds1.Text = "1500";

                    txt_sec_L2_Bs2.Text = "30";
                    txt_sec_L2_Ds2.Text = "1500";

                    txt_sec_L2_Bs3.Text = "30";
                    txt_sec_L2_Ds3.Text = "1500";

                    txt_sec_L2_Bs4.Text = "30";
                    txt_sec_L2_Ds4.Text = "1500";
                    #endregion End Section


                    txt_sec_end_Bt.Text = (s + bft).ToString();
                    txt_sec_L4_Bt.Text = (s + bft).ToString();
                    txt_sec_L2_Bt.Text = (s + bft).ToString();


                    txt_sec_end_Bb.Text = (s + bft).ToString();
                    txt_sec_L4_Bb.Text = (s + bft).ToString();
                    txt_sec_L2_Bb.Text = (s + bft).ToString();


                    #endregion Chiranjit [2013 07 08]
                }
            }
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
            CompositeSection cm = Steel_Composite_Section();
        }

        #region Deck Slab Limit State Method

        public string File_DeckSlab_Results
        {
            get
            {
                return Path.Combine(user_path, "DECKSLAB_ANALYSIS_RESULT.TXT");
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
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 2, IRC70RTRACK"));
            list.Add(string.Format("AXLE LOAD IN TONS, 7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0"));
            list.Add(string.Format("AXLE SPACING IN METRES, 0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 2.900"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
            list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, IRC70RW40TBL"));
            list.Add(string.Format("AXLE LOAD IN TONS,10.0,10.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.93"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 5, IRC70RW40TBM"));
            list.Add(string.Format("AXLE LOAD IN TONS,5.0,5.0,5.0,5.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,0.795,0.38,0.795"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
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

            if (dgv_live_load.Name == dgv_deck_loads.Name)
            {
                #region Deck Slab
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,1.5 "));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,0,5.0"));
                list.Add(string.Format("z,1.5,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 4"));
                list.Add(string.Format("X,0.0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 5"));
                list.Add(string.Format("X,0.0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 5,TYPE 2"));
                list.Add(string.Format("X,0.0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 6,TYPE 3,TYPE 1"));
                list.Add(string.Format("X,0.0,5.0"));
                list.Add(string.Format("Z,1.5,1.5"));
                list.Add(string.Format(""));
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

                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }
        public void Deckslab_User_Input()
        {
            List<string> lst_input = new List<string>();

            #region user input
            lst_input.Add(string.Format("Distance between C/C Exp. Joint"));

            lst_input.Add(string.Format("Overhang of girder off the bearing"));
            lst_input.Add(string.Format("Overhang of slab off the bearing"));
            lst_input.Add(string.Format("Expansion Joint"));
            lst_input.Add(string.Format("Deck Width"));
            lst_input.Add(string.Format("Angle of skew"));
            lst_input.Add(string.Format("Clear carriage way"));
            lst_input.Add(string.Format("Width of outer railing"));
            lst_input.Add(string.Format("Width of Footpath"));
            lst_input.Add(string.Format("Width of Crash Barrier"));
            lst_input.Add(string.Format("Spacing of main girder c/c"));

            lst_input.Add(string.Format("Thk of deck slab"));
            lst_input.Add(string.Format("Thk of deck slab at overhang"));

            lst_input.Add(string.Format("Cantilever slab thk at fixed end"));
            lst_input.Add(string.Format("Cantilever slab thk at free end"));
            lst_input.Add(string.Format("Thk of wearing coat"));
            lst_input.Add(string.Format("No of main girder"));
            lst_input.Add(string.Format("Width of footh path"));
            lst_input.Add(string.Format("Width of raillings"));
            lst_input.Add(string.Format("Top Flange width of girder"));
            lst_input.Add(string.Format("No of Intermediate cross girder"));
            lst_input.Add(string.Format("Grade of concrete"));
            lst_input.Add(string.Format("Grade of reinforcement"));


            lst_input.Add(string.Format("Partial factor of safety  (Basic and seismic)"));
            lst_input.Add(string.Format("Partial factor of safety Accidental "));
            lst_input.Add(string.Format("Coefficient to consider the influence of the strength"));


            lst_input.Add(string.Format("Clear cover"));
            lst_input.Add(string.Format("Unit weight of dry concrete"));
            lst_input.Add(string.Format("Unit weight of wet concrete"));
            lst_input.Add(string.Format(" Unit Weight of wearing course"));
            lst_input.Add(string.Format("Weight of Crash Barrier"));
            lst_input.Add(string.Format("Weight of Railing"));
            lst_input.Add(string.Format("Intensity of Load for shuttering"));
            lst_input.Add(string.Format("Es"));

            #endregion



            List<string> lst_inp_vals = new List<string>();
            #region Value
            lst_inp_vals.Add(string.Format("19.58"));

            lst_inp_vals.Add(string.Format("0.500"));
            lst_inp_vals.Add(string.Format("0.759"));
            lst_inp_vals.Add(string.Format("40.0"));
            lst_inp_vals.Add(string.Format("12.0"));
            lst_inp_vals.Add(string.Format("26.00"));
            lst_inp_vals.Add(string.Format("11.1"));
            lst_inp_vals.Add(string.Format("0.30"));
            lst_inp_vals.Add(string.Format("1.50"));
            lst_inp_vals.Add(string.Format("0.45"));
            lst_inp_vals.Add(string.Format("3.00"));

            lst_inp_vals.Add(string.Format("0.21"));
            lst_inp_vals.Add(string.Format("0.40"));

            lst_inp_vals.Add(string.Format("0.21"));
            lst_inp_vals.Add(string.Format("0.21"));
            lst_inp_vals.Add(string.Format("0.065"));
            lst_inp_vals.Add(string.Format("4.00"));
            lst_inp_vals.Add(string.Format("0.00"));
            lst_inp_vals.Add(string.Format("0.00"));
            lst_inp_vals.Add(string.Format("0.800"));
            lst_inp_vals.Add(string.Format("1.00"));
            lst_inp_vals.Add(string.Format("M35"));
            lst_inp_vals.Add(string.Format("Fe500"));


            lst_inp_vals.Add(string.Format("1.50"));
            lst_inp_vals.Add(string.Format("1.20"));
            lst_inp_vals.Add(string.Format("0.67"));


            lst_inp_vals.Add(string.Format("40.0"));
            lst_inp_vals.Add(string.Format("2.50"));
            lst_inp_vals.Add(string.Format("2.60"));
            lst_inp_vals.Add(string.Format("2.200"));
            lst_inp_vals.Add(string.Format("1.00"));
            lst_inp_vals.Add(string.Format("0.50"));
            lst_inp_vals.Add(string.Format("0.50"));

            lst_inp_vals.Add(string.Format("200000.00"));
            #endregion

            #region Input Units
            List<string> lst_units = new List<string>();
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("deg."));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("Nos"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("nos"));
            lst_units.Add(string.Format("Mpa"));
            lst_units.Add(string.Format("Mpa"));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));


            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("t/m3"));
            lst_units.Add(string.Format("t/m3"));
            lst_units.Add(string.Format("t/m^3"));
            lst_units.Add(string.Format("t/m"));
            lst_units.Add(string.Format("t/m"));
            lst_units.Add(string.Format("t/m2"));
            lst_units.Add(string.Format("Mpa"));
            #endregion Input Units
            dgv_deck_user_input.Rows.Clear();
            for (int i = 0; i < lst_inp_vals.Count; i++)
            {
                dgv_deck_user_input.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);
            }

            #region Design Data
            lst_input.Clear();
            lst_input.Add(string.Format("Bar dia to be used"));
            lst_input.Add(string.Format("b"));
            lst_input.Add(string.Format("bottom bar dia"));
            lst_input.Add(string.Format("c/c distance"));
            lst_input.Add(string.Format("Co-efficient which takes account of the bond properties of the bonded reinforcement"));
            lst_input.Add(string.Format("Co-fficient which takes into account of the distribution of strain"));
            lst_input.Add(string.Format("Mean value of the tensile strength of the concrete "));

            lst_inp_vals.Clear();
            lst_inp_vals.Add(string.Format("16.0"));
            lst_inp_vals.Add(string.Format("1000"));
            lst_inp_vals.Add(string.Format("10"));
            lst_inp_vals.Add(string.Format("200"));
            lst_inp_vals.Add(string.Format("0.8"));
            lst_inp_vals.Add(string.Format("0.5"));
            lst_inp_vals.Add(string.Format("3.000"));


            lst_units.Clear();
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("Mpa"));

            //MessageBox.Show((DateTime.Now - DateTime.UtcNow).ToString());

            dgv_deck_design_input.Rows.Clear();
            for (int i = 0; i < lst_inp_vals.Count; i++)
            {
                dgv_deck_design_input.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);
            }
            #endregion Design Data



            #region Live Load Data
            lst_input.Clear();
            lst_input.Add(string.Format("alpha (Constant)"));

            lst_input.Add(string.Format("Distances  from start of deck"));

            lst_input.Add(string.Format("Max. tyre pressure "));
            lst_input.Add(string.Format("Distance between two loads across span "));
            lst_input.Add(string.Format("Impact factor "));

            //lst_input.Add(string.Format("1Lane Class - A"));
            lst_input.Add(string.Format("1Lane  Load Type 1"));
            lst_input.Add(string.Format("Contact width across span"));
            lst_input.Add(string.Format("Contact width along span"));
            lst_input.Add(string.Format("Distance between two vehicle c/c of  wheels (Traffic Direction)"));
            lst_input.Add(string.Format("Distance between two Wheels C/C (Transverse direction)"));
            lst_input.Add(string.Format("Minimum edge distance upto c/l of wheel"));

            //lst_input.Add(string.Format("1Lane 40T Boggie (L-Type)"));
            lst_input.Add(string.Format("1Lane  Load Type 4"));
            lst_input.Add(string.Format("Contact width along span"));
            lst_input.Add(string.Format("Distances between C/C of wheel Across the traffic"));

            //lst_input.Add(string.Format("1Lane 40T Boggie (M-Type)"));
            lst_input.Add(string.Format("1Lane  Load Type 5"));
            lst_input.Add(string.Format("Contact width along span"));

            //lst_input.Add(string.Format("1Lane 70R Track"));
            lst_input.Add(string.Format("1Lane  Load Type 2"));
            lst_input.Add(string.Format("Contact width along span"));
            lst_input.Add(string.Format("Distance between two loads across span "));
            lst_input.Add(string.Format("Contact width across span"));


            lst_inp_vals.Clear();
            lst_inp_vals.Add(string.Format("2.600"));
            lst_inp_vals.Add(string.Format("0"));
            lst_inp_vals.Add(string.Format("5.273"));
            lst_inp_vals.Add(string.Format("1.22"));
            lst_inp_vals.Add(string.Format("1.25"));

            lst_inp_vals.Add(string.Format(""));
            lst_inp_vals.Add(string.Format("250"));
            lst_inp_vals.Add(string.Format("500"));
            lst_inp_vals.Add(string.Format("1.200"));
            lst_inp_vals.Add(string.Format("1.800"));
            lst_inp_vals.Add(string.Format("0.400"));

            lst_inp_vals.Add(string.Format(""));
            lst_inp_vals.Add(string.Format("810"));
            lst_inp_vals.Add(string.Format("1.93"));

            lst_inp_vals.Add(string.Format(""));
            lst_inp_vals.Add(string.Format("360"));

            lst_inp_vals.Add(string.Format(""));
            lst_inp_vals.Add(string.Format("840"));
            lst_inp_vals.Add(string.Format("30"));
            lst_inp_vals.Add(string.Format("4570"));


            lst_units.Clear();
            lst_units.Add(string.Format("(Cont.)"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("kg/cm^2"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("mm"));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));


            //MessageBox.Show((DateTime.Now - DateTime.UtcNow).ToString());

            dgv_deck_user_live_loads.Rows.Clear();
            for (int i = 0; i < lst_inp_vals.Count; i++)
            {
                dgv_deck_user_live_loads.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);

                if (lst_inp_vals[i] == "")
                {
                    dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 9.0, FontStyle.Bold, GraphicsUnit.Pixel);
                    dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);
                }

            }
            #endregion Design Data

        }

        List<string> Result { get; set; }
        void Show_Deckslab_Moment_Shear()
        {
            Result = new List<string>();
            MemberCollection mc = new MemberCollection(Deck_Analysis.LiveLoad_2_Analysis.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis.LiveLoad_2_Analysis.Analysis.Joints;

            #region Deck Model

            double L = Deck_Analysis.Length;
            double W = Deck_Analysis.WidthBridge;
            double val = L / 2;
            int i = 0;

            Deck_Analysis.Effective_Depth = L / 16.0; ;


            List<int> _support_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();
            List<int> _L8_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _3L8_inn_joints = new List<int>();
            List<int> _L2_inn_joints = new List<int>();



            List<int> _3L16_inn_joints = new List<int>();
            List<int> _5L16_inn_joints = new List<int>();
            List<int> _7L16_inn_joints = new List<int>();



            List<int> _support_out_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _3L8_out_joints = new List<int>();
            List<int> _L2_out_joints = new List<int>();


            List<int> _3L16_out_joints = new List<int>();
            List<int> _5L16_out_joints = new List<int>();
            List<int> _7L16_out_joints = new List<int>();



            //List<int> _L4_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();

            List<int> _cross_joints = new List<int>();


            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Deff;

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
            #endregion Deck Model

            Deck_Analysis.TotalLoad_Analysis = Deck_Analysis.LiveLoad_2_Analysis;

            val = Deck_Analysis.LiveLoad_2_Analysis.Analysis.Effective_Depth;

            double cant_wi_left = 0.0;
            double cant_wi_right = 0.0;

            #region Find Joints
            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _3L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _3L8_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == (x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _support_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == (L + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _support_out_joints.Add(jn_col[i].NodeNo);
                    }

                    #region 3L/16


                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 16.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _3L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 16.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _3L16_out_joints.Add(jn_col[i].NodeNo);
                    }


                    #endregion 3L/16



                    #region 5L/16


                    if (jn_col[i].X.ToString("0.0") == ((5 * L / 16.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _5L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (5 * L / 16.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _5L16_out_joints.Add(jn_col[i].NodeNo);
                    }


                    #endregion 3L/16



                    #region 7L/16


                    if (jn_col[i].X.ToString("0.0") == ((7 * L / 16.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _7L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (7 * L / 16.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _7L16_out_joints.Add(jn_col[i].NodeNo);
                    }


                    #endregion 3L/16



                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }

            #endregion Find Joints

            Result.Clear();
            Result.Add("");
            Result.Add("");
            //Result.Add("Analysis Result of RCC T-BEAM Bridge");
            Result.Add("");


            #region Maximum Hogging

            List<double> Max_Hog_LL_1 = new List<double>();
            List<double> Max_Hog_LL_2 = new List<double>();
            List<double> Max_Hog_LL_3 = new List<double>();
            List<double> Max_Hog_LL_4 = new List<double>();
            List<double> Max_Hog_LL_5 = new List<double>();
            List<double> Max_Hog_LL_6 = new List<double>();


            List<double> Max_Hog_LL_7 = new List<double>();



            MaxForce f = null;

            List<int> tmp_jts = new List<int>();

            #region Live Load SHEAR Force

            #region Maximum Hogging Live Load 1

            Max_Hog_LL_1.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_deff_inn_joints[1]);
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_L8_inn_joints[1]);
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_3L16_inn_joints[1]);
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_L4_inn_joints[1]);
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_5L16_inn_joints[1]);
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_3L8_inn_joints[1]);
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_7L16_inn_joints[1]);
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L2_inn_joints[1]);
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_7L16_out_joints[1]);
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.AddRange(_3L8_out_joints.ToArray());
            tmp_jts.Add(_3L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.AddRange(_5L16_out_joints.ToArray());
            tmp_jts.Add(_5L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L4_out_joints[1]);
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_3L16_out_joints[1]);
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L8_out_joints[1]);
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_deff_out_joints[1]);
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 2

            Max_Hog_LL_2.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            #endregion Maximum Hogging LL 2


            Max_Hog_LL_3.Clear();
            #region Maximum Hogging Live Load 1

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);



            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 4

            Max_Hog_LL_4.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);



            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 5

            Max_Hog_LL_5.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            #endregion Maximum Hogging LL 5

            #region Maximum Hogging Live Load 6

            Max_Hog_LL_6.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);



            #endregion Maximum Hogging LL 6

            #endregion Live Load SHEAR Force

            MyList.Array_Multiply_With(ref Max_Hog_LL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_3, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_4, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_5, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_6, 10.0);


            #endregion Maximum Hogging

            #region Maximum Sagging

            List<double> Max_Sag_LL_1 = new List<double>();
            List<double> Max_Sag_LL_2 = new List<double>();
            List<double> Max_Sag_LL_3 = new List<double>();
            List<double> Max_Sag_LL_4 = new List<double>();
            List<double> Max_Sag_LL_5 = new List<double>();
            List<double> Max_Sag_LL_6 = new List<double>();


            List<double> Max_Sag_LL_7 = new List<double>();




            #region Live Load SHEAR Force


            #region Maximum Hogging Live Load 1

            Max_Sag_LL_1.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_1.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            #endregion Maximum Hogging LL 1


            #region Maximum Hogging Live Load 2

            Max_Sag_LL_2.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);



            #endregion Maximum Hogging LL 2


            #region Maximum Hogging Live Load 1

            Max_Sag_LL_3.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);





            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);



            #endregion Maximum Hogging LL 1


            #region Maximum Hogging Live Load 4

            Max_Sag_LL_4.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);



            #endregion Maximum Hogging LL 1


            #region Maximum Hogging Live Load 5

            Max_Sag_LL_5.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);



            #endregion Maximum Hogging LL 5



            #region Maximum Hogging Live Load 6

            Max_Sag_LL_6.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[1]);
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[1]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);



            #endregion Maximum Hogging LL 6



            #endregion Live Load SHEAR Force

            MyList.Array_Multiply_With(ref Max_Sag_LL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_3, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_4, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_5, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_6, 10.0);


            #endregion Maximum Hogging





            #region DL + SIDL + FPLL

            List<double> Max_Moment_DL = new List<double>();
            List<double> Max_Moment_SIDL_1 = new List<double>();
            List<double> Max_Moment_SIDL_2 = new List<double>();
            List<double> Max_Moment_FPLL = new List<double>();

            #region _deff_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_deff_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _deff_inn_joints

            #region _L8_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L8_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _L8_inn_joints

            #region _3L16_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_3L16_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _deff_inn_joints

            #region _L4_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L4_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _L4_inn_joints


            #region _5L16_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_5L16_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _deff_inn_joints


            #region _3L8_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_3L8_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _3L8_inn_joints


            #region _7L16_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_7L16_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _7L16_inn_joints


            #region _L2_inn_joints

            tmp_jts.Clear();


            tmp_jts.Add(_L2_inn_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _L2_inn_joints


            #region _7L16_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_7L16_out_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _7L16_out_joints


            #region _3L8_out_joints
            tmp_jts.Clear();

            tmp_jts.AddRange(_3L8_out_joints.ToArray());

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _3L8_out_joints


            #region _5L16_out_joints
            tmp_jts.Clear();


            tmp_jts.AddRange(_5L16_out_joints.ToArray());

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _5L16_out_joints


            #region _L4_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L4_out_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _L4_out_joints

            #region _3L16_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_3L16_out_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _3L16_out_joints

            #region _L8_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L8_out_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _L8_out_joints

            #region _deff_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_deff_out_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _deff_out_joints

            #region _support_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_support_out_joints[1]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _support_out_joints



            MyList.Array_Multiply_With(ref Max_Moment_DL, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_SIDL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_SIDL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_FPLL, 10.0);

            #endregion DL + SIDL + FPLL

            val = 0.0;


            //string format = "{0,-25} {1,12:f3} {2,10:f3} {3,15:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3} {12,10:f3} {13,10:f3} {14,10:f3} {15,10:f3} {16,10:f3}";
            string format = "{0,-25} {1,12:f3} {2,10:f3} {3,15:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3} {12,10:f3} {13,10:f3} {14,10:f3} {15,10:f3}";

            Result.Add(string.Format(""));

            #region Summary 1

            Result.Add(string.Format(""));
            Result.Add(string.Format("-------------------------------------------------------------"));
            Result.Add(string.Format("RCC T GIRDER (LIMIT STATE METHOD) DECK SLAB ANALYSIS RESULTS "));
            Result.Add(string.Format("--------------------------------------------------------------"));
            Result.Add(string.Format(""));
            Result.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            //Result.Add(string.Format("                                NODE 5     NODE 5          NODE 8     NODE 11    NODE 14    NODE 17    NODE 20    NODE 23    NODE 26   NODE 29    NODE 32    NODE 35    NODE 38    NODE 41    NODE 44    NODE 47"));
            Result.Add(string.Format("                                NODE 5     NODE 8          NODE 11    NODE 14    NODE 17    NODE 20    NODE 23    NODE 26   NODE 29    NODE 32    NODE 35    NODE 38    NODE 41    NODE 44    NODE 47"));
            Result.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format(""));
            int indx = 0;
            try
            {

                #region Max Dead Load

                indx = 0;
                Result.Add(string.Format(format,
                    "DL",
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++]
                    ));

                #endregion  Max Dead Load


                #region Max SIDL (Except surfacing)

                indx = 0;
                Result.Add(string.Format(format,
                    "SIDL (Except surfacing)",
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++]
                    ));

                #endregion  Max SIDL (Except surfacing)

                #region Max SIDL (Surfacing)

                indx = 0;
                Result.Add(string.Format(format,
                    "SIDL (Surfacing)",
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++]
                    ));

                #endregion  Max SIDL (Surfacing)


                #region Max FPLL

                indx = 0;
                Result.Add(string.Format(format,
                    "FPLL",
                    Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++]
                    ));

                #endregion  Max FPLL


                #region Max LL_1

                #region Max_Hog_LL_1
                indx = 0;
                Result.Add(string.Format(format,
                    "1CLA (Max. Hog)",
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++]
                    ));
                #endregion Max_Hog_LL_1

                #region Max_Sag_LL_1
                indx = 0;
                Result.Add(string.Format(format,
                    "1CLA (Max. Sag)",
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++]
                    ));
                #endregion Max_Hog_LL_1

                #endregion Max LL1

                #region Max LL_2
                #region Max_Hog_LL_2
                indx = 0;
                Result.Add(string.Format(format,
                    "2CLA (Max. Hog)",
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++]
                    ));
                #endregion Max_Hog_LL_2

                #region Max_Sag_LL_2
                indx = 0;
                Result.Add(string.Format(format,
                    "2CLA (Max. Sag)",
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++]
                    ));
                #endregion Max_Hog_LL_2

                #endregion Max LL1


                #region Max LL_3
                #region Max_Hog_LL_3
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-L (Max. Hog)",
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++]
                    ));
                #endregion Max_Hog_LL_3

                #region Max_Sag_LL_3
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-L (Max. Sag)",
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++]
                    ));
                #endregion Max_Hog_LL_3

                #endregion Max LL1

                #region Max LL_4
                #region Max_Hog_LL_4
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-M (Max. Hog)",
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++]
                    ));
                #endregion Max_Hog_LL_4

                #region Max_Sag_LL_4
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-M (Max. Sag)",
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++]
                    ));
                #endregion Max_Hog_LL_4

                #endregion Max LL1

                #region Max LL_5
                #region Max_Hog_LL_5
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 70R TR (Max. Hog)",
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++]
                    ));
                #endregion Max_Hog_LL_5

                #region Max_Sag_LL_5
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 70R TR (Max. Sag)",
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++]
                    ));
                #endregion Max_Hog_LL_5

                #endregion Max LL1

                #region Max LL_6
                #region Max_Hog_LL_6
                indx = 0;
                Result.Add(string.Format(format,
                    "1L70RW+1LCA(Max Hog)",
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++]
                    ));
                #endregion Max_Hog_LL_6

                #region Max_Sag_LL_6
                indx = 0;
                Result.Add(string.Format(format,
                    "1L70RW+1LCA(Max Sag)",
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++]
                    ));
                #endregion Max_Hog_LL_6

                #endregion Max LL1



                List<double> temp = new List<double>();
                for (i = 0; i < Max_Hog_LL_1.Count; i++)
                {
                    temp.Add(Max_Hog_LL_1[i]);
                    temp.Add(Max_Hog_LL_2[i]);
                    temp.Add(Max_Hog_LL_3[i]);
                    temp.Add(Max_Hog_LL_4[i]);
                    temp.Add(Max_Hog_LL_5[i]);
                    temp.Add(Max_Hog_LL_6[i]);

                    temp.Sort();
                    temp.Reverse();

                    Max_Hog_LL_7.Add(temp[0]);

                    temp.Clear();

                    temp.Add(Max_Sag_LL_1[i]);
                    temp.Add(Max_Sag_LL_2[i]);
                    temp.Add(Max_Sag_LL_3[i]);
                    temp.Add(Max_Sag_LL_4[i]);
                    temp.Add(Max_Sag_LL_5[i]);
                    temp.Add(Max_Sag_LL_6[i]);

                    temp.Sort();
                    //temp.Reverse();

                    Max_Sag_LL_7.Add(temp[0]);
                }



                #region Max LL_7
                #region Max_Hog_LL_7
                indx = 0;
                Result.Add(string.Format(format,
                    "LL (Max Hog)",
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++]
                    ));
                #endregion Max_Hog_LL_7

                #region Max_Sag_LL_7
                indx = 0;
                Result.Add(string.Format(format,
                    "LL (Max Sag)",
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++]
                    ));
                #endregion Max_Hog_LL_7

                #endregion Max LL1


            }
            catch (Exception exxx) { }
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            Result.Add(string.Format(""));

            #endregion Summary 1




            Result.Add(string.Format(""));
            //rtb_ana_result.Lines = Result.ToArray();

            File.WriteAllLines(File_DeckSlab_Results, Result.ToArray());

            iApp.RunExe(File_DeckSlab_Results);
            Result.Add(string.Format(""));
        }
        void Ana_Write_DeckSlab_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
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

            if (add_DeadLoad)
            {

                load_lst.Add("LOAD 1 DEAD LOAD");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Inner_Girders_as_String, deck_member_load[0]));

                load_lst.Add("LOAD 2 SIDL EXCEPT SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Inner_Girders_as_String, deck_member_load[1]));

                load_lst.Add("LOAD 3 SIDL SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Inner_Girders_as_String, deck_member_load[2]));

                load_lst.Add("LOAD 4 FPLL");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Inner_Girders_as_String, 0.0));

            }
            else
            {
                load_lst.Add("LOAD 1");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add("1 TO " + Deck_Analysis.MemColls.Count + " UNI GY -0.001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                List<string> list = new List<string>();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));

                if (load_no == 1)
                {
                    list.AddRange(deck_ll_1.ToArray());
                }
                if (load_no == 2)
                {
                    list.AddRange(deck_ll_2.ToArray());
                }
                if (load_no == 3)
                {
                    list.AddRange(deck_ll_3.ToArray());

                }
                if (load_no == 4)
                {
                    list.AddRange(deck_ll_4.ToArray());

                }
                if (load_no == 5)
                {
                    list.AddRange(deck_ll_5.ToArray());

                }
                if (load_no == 6)
                {
                    list.AddRange(deck_ll_6.ToArray());
                }
                load_lst.AddRange(list.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


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
            Button btn = sender as Button;

            string excel_file_name = "";
            string copy_path = "";
            if (btn.Name == btn_LS_deck_rep_open.Name)
            {
                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Deckslab));
                if (File.Exists(copy_path))
                    iApp.OpenExcelFile(copy_path, "2011ap");
            }
            else if (btn.Name == btn_LS_deck_ws.Name)
            {
                Write_All_Data(true);

                excel_file_name = Excel_Deckslab;

                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));
                File.Copy(excel_file_name, copy_path, true);
                RCC_Deckslab_Excel_Update rcc_excel = new RCC_Deckslab_Excel_Update();
                rcc_excel.Excel_File_Name = copy_path;
                rcc_excel.Report_File_Name = File_DeckSlab_Results;

                Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Deck_Analysis.LiveLoad_File);
                rcc_excel.llc = Deck_Analysis.Live_Load_List;
                rcc_excel.Deckslab_User_Inputs.Read_From_Grid(dgv_deck_user_input);
                rcc_excel.Deckslab_Design_Inputs.Read_From_Grid(dgv_deck_design_input);
                rcc_excel.Deckslab_User_Live_loads.Read_From_Grid(dgv_deck_user_live_loads);
                rcc_excel.Read_Update_Data();
                iApp.Excel_Open_Message();

                Button_Enable_Disable();
                return;
            }
            else
            {
                iApp.Open_WorkSheet_Design();
                return;
            }

            copy_path = Path.Combine(user_path, Path.GetFileName(excel_file_name));

            if (File.Exists(excel_file_name))
            {
                iApp.OpenExcelFile(Worksheet_Folder, excel_file_name, "2011ap");
            }
            Button_Enable_Disable();
            //Write_All_Data(false);
        }

        private void btn_Deck_Analysis_Click(object sender, EventArgs e)
        {
            //Write_All_Data(true);
            user_path = IsCreate_Data ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                DECKSLAB_LL_TXT();
                #region Process
                int i = 1;
                Write_All_Data(true);

                string flPath = Deck_Analysis.Input_File;

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                do
                {
                    if (i == 1)
                    {
                        flPath = Deck_Analysis.LL_Analysis_1_Input_File;
                    }
                    else if (i == 2)
                    {
                        flPath = Deck_Analysis.LL_Analysis_2_Input_File;
                    }
                    else if (i == 3)
                    {
                        flPath = Deck_Analysis.LL_Analysis_3_Input_File;
                    }
                    else if (i == 4)
                    {

                        flPath = Deck_Analysis.LL_Analysis_4_Input_File;
                    }
                    else if (i == 5)
                    {

                        flPath = Deck_Analysis.LL_Analysis_5_Input_File;
                    }
                    else if (i == 6)
                    {
                        flPath = Deck_Analysis.LL_Analysis_6_Input_File;
                    }
                    else if (i == 7)
                    {
                        flPath = Deck_Analysis.DeadLoadAnalysis_Input_File;
                    }


                    //MessageBox.Show(this, "PROCESS ANALYSIS FOR "
                    // + Path.GetFileNameWithoutExtension(flPath).ToUpper(), "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    //File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    //File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    ////System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                    //System.Diagnostics.Process prs = new System.Diagnostics.Process();

                    //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                    //System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                    //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                    //if (prs.Start())
                    //    prs.WaitForExit();


                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    i++;
                }
                while (i <= 7);




                //frm_LS_Process ff = new frm_LS_Process(pcol);
                //ff.Owner = this;
                ////ff.ShowDialog();
                //if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                if (!iApp.Show_and_Run_Process_List(pcol)) return;


                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                string ana_rep_file = Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File);



                if (File.Exists(ana_rep_file))
                {

                    iApp.Progress_Works.Clear();
                    //iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");


                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 1 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 2 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 3 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 4 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 5 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 6 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File (ANALYSIS_REP.TXT)");

                    //iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


                    //Deck_Analysis.TotalLoad_Analysis = null;
                    //Long_Girder_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, Long_Girder_Analysis.Total_Analysis_Report);
                    //Long_Girder_Analysis.LiveLoad_Analysis = new BridgeMemberAnalysis(iApp, Long_Girder_Analysis.LiveLoad_Analysis_Report);

                    Deck_Analysis.LiveLoad_1_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File));

                    Deck_Analysis.LiveLoad_2_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_2_Input_File));

                    Deck_Analysis.LiveLoad_3_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_3_Input_File));

                    Deck_Analysis.LiveLoad_4_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_4_Input_File));

                    Deck_Analysis.LiveLoad_5_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_5_Input_File));

                    Deck_Analysis.LiveLoad_6_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_6_Input_File));


                    Deck_Analysis.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis.DeadLoad_Analysis_Report);

                    if (!iApp.Is_Progress_Cancel)
                        Show_Deckslab_Moment_Shear();
                    else
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        return;

                    }
                    //string s1 = "";
                    //string s2 = "";
                    //try
                    //{
                    //    for (i = 0; i < Long_Girder_Analysis.TotalLoad_Analysis.Supports.Count; i++)
                    //    {
                    //        if (i < Long_Girder_Analysis.TotalLoad_Analysis.Supports.Count / 2)
                    //        {
                    //            if (i == Long_Girder_Analysis.TotalLoad_Analysis.Supports.Count / 2 - 1)
                    //            {
                    //                s1 += Long_Girder_Analysis.TotalLoad_Analysis.Supports[i].NodeNo;
                    //            }
                    //            else
                    //                s1 += Long_Girder_Analysis.TotalLoad_Analysis.Supports[i].NodeNo + ",";
                    //        }
                    //        else
                    //        {
                    //            if (i == Long_Girder_Analysis.TotalLoad_Analysis.Supports.Count - 1)
                    //            {
                    //                s2 += Long_Girder_Analysis.TotalLoad_Analysis.Supports[i].NodeNo;
                    //            }
                    //            else
                    //                s2 += Long_Girder_Analysis.TotalLoad_Analysis.Supports[i].NodeNo + ", ";
                    //        }
                    //    }
                    //}
                    //catch (Exception ex) { }
                    //double BB = MyList.StringToDouble(txt_Ana_B.Text, 8.5);





                }
                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;



                Button_Enable_Disable();
                Write_All_Data(false);
                iApp.Progress_Works.Clear();

                #endregion Process
                Write_All_Data(false);
            }
            catch (Exception ex) { }
        }
        private void btn_Deck_Create_Analysis_Data_Click(object sender, EventArgs e)
        {

            Write_All_Data(true);
            //user_path = IsCreate_Data ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                DECKSLAB_LL_TXT();

                #region Create Data

                Analysis_Initialize_InputData();

                string usp = Path.Combine(user_path, "Deck Slab Analysis");
                if (!Directory.Exists(usp))
                {
                    Directory.CreateDirectory(usp);
                }
                Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
                Deck_Analysis.CreateData();
                Deck_Analysis.WriteData_DeadLoad_Analysis(Deck_Analysis.Input_File);

                Calculate_Load_Computation(Deck_Analysis.Outer_Girders_as_String,
                    Deck_Analysis.Inner_Girders_as_String,
                     Deck_Analysis.joints_list_for_load);

                //Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LiveLoadAnalysis_Input_File);

                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_1_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_2_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_3_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_4_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_5_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_6_Input_File, deck_ll);

                Deck_Analysis.WriteData_DeadLoad_Analysis(Deck_Analysis.DeadLoadAnalysis_Input_File);

                //Ana_Write_DeckSlab_Load_Data(Deck_Analysis.Input_File, true, false);
                //Ana_Write_DeckSlab_Load_Data(Deck_Analysis.TotalAnalysis_Input_File, true, false);
                //Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LiveLoadAnalysis_Input_File, true, false);

                //Chiranjit [2013 09 24] Without Dead Load
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_1_Input_File, true, false, 1);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_2_Input_File, true, false, 2);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_3_Input_File, true, false, 3);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_4_Input_File, true, false, 4);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_5_Input_File, true, false, 5);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_6_Input_File, true, false, 6);

                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.DeadLoadAnalysis_Input_File, false, true, 1);

                //Deck_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Deck_Analysis.LiveLoad_File;

                Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Deck_Analysis.Live_Load_List == null) return;

                Button_Enable_Disable();

                MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
                   "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #endregion Create Data

                Write_All_Data(false);
                cmb_deck_input_files.SelectedIndex = 0;

            }
            catch (Exception ex) { }
        }

        private void cmb_deck_input_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deck_Buttons();
        }

        private void Deck_Buttons()
        {
            ComboBox cmb = cmb_deck_input_files;


            if (cmb.SelectedIndex < 0) cmb.SelectedIndex = 0;
            string filename = "";
            if (Deck_Analysis != null)
            {

                if (cmb.SelectedIndex == 0) filename = Deck_Analysis.DeadLoadAnalysis_Input_File;
                else if (cmb.SelectedIndex == 1) filename = Deck_Analysis.LL_Analysis_1_Input_File;
                else if (cmb.SelectedIndex == 2) filename = Deck_Analysis.LL_Analysis_2_Input_File;
                else if (cmb.SelectedIndex == 3) filename = Deck_Analysis.LL_Analysis_3_Input_File;
                else if (cmb.SelectedIndex == 4) filename = Deck_Analysis.LL_Analysis_4_Input_File;
                else if (cmb.SelectedIndex == 5) filename = Deck_Analysis.LL_Analysis_5_Input_File;
                else if (cmb.SelectedIndex == 6) filename = Deck_Analysis.LL_Analysis_6_Input_File;
                else if (cmb.SelectedIndex == 7) filename = File_DeckSlab_Results;
            }
            btn_deck_view_data.Enabled = File.Exists(filename);
            btn_deck_view_moving.Enabled = File.Exists(MyList.Get_LL_TXT_File(filename)) && File.Exists(MyList.Get_Analysis_Report_File(filename));
            btn_deck_view_struc.Enabled = File.Exists(filename) && cmb.SelectedIndex != 7;
            btn_deck_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(filename));

            btn_LS_deck_ws.Enabled = File.Exists(File_DeckSlab_Results);
            btn_LS_deck_rep_open.Enabled = File.Exists(Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Deckslab)));


        }

        private void btn_restore_ll_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (btn.Name == btn_deck_restore_ll.Name)
                    Default_Moving_LoadData(dgv_deck_liveloads);
            }
        }

        private void btn_deck_open_input_file_Click(object sender, EventArgs e)
        {
            ComboBox cmb = cmb_deck_input_files;

            Button btn = sender as Button;

            string filename = "";

            if (cmb.SelectedIndex == 0) filename = Deck_Analysis.DeadLoadAnalysis_Input_File;
            else if (cmb.SelectedIndex == 1) filename = Deck_Analysis.LL_Analysis_1_Input_File;
            else if (cmb.SelectedIndex == 2) filename = Deck_Analysis.LL_Analysis_2_Input_File;
            else if (cmb.SelectedIndex == 3) filename = Deck_Analysis.LL_Analysis_3_Input_File;
            else if (cmb.SelectedIndex == 4) filename = Deck_Analysis.LL_Analysis_4_Input_File;
            else if (cmb.SelectedIndex == 5) filename = Deck_Analysis.LL_Analysis_5_Input_File;
            else if (cmb.SelectedIndex == 6) filename = Deck_Analysis.LL_Analysis_6_Input_File;
            else if (cmb.SelectedIndex == 7) filename = File_DeckSlab_Results;
            if (!File.Exists(filename)) return;
            if (btn.Name == btn_deck_view_data.Name)
            {
                iApp.View_Input_File(filename);
                //string ll = Path.Combine(Path.GetDirectoryName(filename), "LL.TXT");
                //if (File.Exists(ll))
                //    iApp.RunExe(ll);
            }
            else if (btn.Name == btn_deck_view_report.Name)
                iApp.RunExe(MyList.Get_Analysis_Report_File(filename));
            else if (btn.Name == btn_deck_view_struc.Name)
                iApp.OpenWork(filename, false);
            else if (btn.Name == btn_deck_view_moving.Name)
                iApp.OpenWork(filename, true);

        }

        List<string> deck_ll = new List<string>();
        List<string> deck_ll_types = new List<string>();


        List<string> deck_ll_1 = new List<string>();
        List<string> deck_ll_2 = new List<string>();
        List<string> deck_ll_3 = new List<string>();
        List<string> deck_ll_4 = new List<string>();
        List<string> deck_ll_5 = new List<string>();
        List<string> deck_ll_6 = new List<string>();

        public void DECKSLAB_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            deck_ll.Clear();
            deck_ll_types.Clear();

            bool flag = false;
            for (i = 0; i < dgv_deck_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_deck_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_deck_liveloads[c, i].Value.ToString();

                    if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }
                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    deck_ll_types.Add(txt);
                }
                deck_ll.Add(txt);
            }
            deck_ll.Add(string.Format(""));
            deck_ll.Add(string.Format("TYPE 6 IRC40RWHEEL"));
            deck_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            deck_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            deck_ll.Add(string.Format("2.740"));
            i = 0;

            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            deck_ll_1 = new List<string>();
            deck_ll_2 = new List<string>();
            deck_ll_3 = new List<string>();
            deck_ll_4 = new List<string>();
            deck_ll_5 = new List<string>();
            deck_ll_6 = new List<string>();

            int fl = 0;
            double xinc = MyList.StringToDouble(txt_deck_xincr.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_deck_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_deck_loads[0, i].Value.ToString().ToUpper();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < deck_ll_types.Count; f++)
                        {
                            if (deck_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", deck_ll_types[f], imp_fact);
                                break;
                            }
                        }
                        list.Add(txt);
                    }
                    list.Add(string.Format("LOAD GENERATION {0:f0}", (1 + (L / xinc))));
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0.0 {2:f3} XINC {3:f3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    if (count == 1)
                    {
                        deck_ll_1.Clear();
                        deck_ll_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        deck_ll_2.Clear();
                        deck_ll_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        deck_ll_3.Clear();
                        deck_ll_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        deck_ll_4.Clear();
                        deck_ll_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        deck_ll_5.Clear();
                        deck_ll_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {
                        deck_ll_6.Clear();
                        deck_ll_6.AddRange(list.ToArray());
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
                for (c = 1; c < dgv_deck_loads.ColumnCount; c++)
                {
                    kStr = dgv_deck_loads[c, i].Value.ToString();

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
        }

        #endregion Deck Slab Limit State Method

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
            iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
        }

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
                cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
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
                cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            else if (rbtn_Rail_Load.Checked)
            {
                lbl_HB.Text = "BS RAIL LOADINGS";

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
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
            lgen = ((int)(L / incr)) + 1;

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


            grb_ha.Enabled = (rbtn_HA.Checked || rbtn_HA_HB.Checked);
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

        void Show_British_Moment_Shear()
        {

            MemberCollection mc = new MemberCollection(Bridge_Analysis.Structure.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Bridge_Analysis.Structure.Analysis.Joints;

            double supp_x_coor = Bridge_Analysis.Structure.Supports[0].X;

            double L = Bridge_Analysis.Structure.Analysis.Length;
            double W = Bridge_Analysis.Structure.Analysis.Width;
            double val = L / 2;
            int i = 0;

            List<int> _L2_inn_joints = Bridge_Analysis._L2_inn_joints;
            List<int> _L4_inn_joints = Bridge_Analysis._L4_inn_joints;
            List<int> _deff_inn_joints = Bridge_Analysis._deff_inn_joints;

            List<int> _L2_out_joints = Bridge_Analysis._L2_out_joints;
            List<int> _L4_out_joints = Bridge_Analysis._L4_out_joints;
            List<int> _deff_out_joints = Bridge_Analysis._deff_out_joints;




            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            iApp.Progress_ON("Read Forces....");
            iApp.SetProgressValue(10, 100);


            MaxForce mfrc = new MaxForce();
            Results.Clear();


            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_inn_joints, true);
            txt_Ana_inner_long_L2_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_L2_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_shear_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE ", _L2_inn_joints, "Ton"));





            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_inn_joints, true);
            txt_Ana_inner_long_L2_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_L2_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L2_moment_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT ", _L2_inn_joints, "Ton-m"));



            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_inn_joints, true);
            txt_Ana_inner_long_L4_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_L4_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_shear_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE ", _L4_inn_joints, "Ton"));


            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_inn_joints, true);
            txt_Ana_inner_long_L4_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_L4_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_L4_moment_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT ", _L4_inn_joints, "Ton-m"));



            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_inn_joints, true);
            txt_Ana_inner_long_deff_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_deff_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_deff_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_inner_long_deff_shear_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE ", _deff_inn_joints, "Ton"));


            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_inn_joints, true);
            txt_Ana_inner_long_deff_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_inner_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_inner_long_deff_moment_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT ", _deff_inn_joints, "Ton-m"));

            iApp.SetProgressValue(70, 100);


            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L2_out_joints, true);
            txt_Ana_outer_long_L2_shear.Text = mfrc.ToString();
            txt_Ana_outer_long_L2_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_shear_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_out_joints, "Ton"));


            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L2_out_joints, true);
            txt_Ana_outer_long_L2_moment.Text = mfrc.ToString();
            txt_Ana_outer_long_L2_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L2_moment_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_out_joints, "Ton-m"));




            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_L4_out_joints, true);
            txt_Ana_outer_long_L4_shear.Text = mfrc.ToString();
            txt_Ana_outer_long_L4_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_shear_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));


            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_L4_out_joints, true);
            txt_Ana_outer_long_L4_moment.Text = mfrc.ToString();
            txt_Ana_outer_long_L4_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_L4_moment_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));


            mfrc = Bridge_Analysis.Structure.GetJoint_ShearForce(_deff_out_joints, true);
            txt_Ana_outer_long_deff_shear.Text = mfrc.ToString();
            txt_Ana_outer_long_deff_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_shear_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));


            mfrc = Bridge_Analysis.Structure.GetJoint_MomentForce(_deff_out_joints, true);
            txt_Ana_outer_long_deff_moment.Text = mfrc.ToString();
            txt_Ana_outer_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_outer_long_deff_moment_load_case.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));
            iApp.SetProgressValue(99, 100);

            #region Null All variables
            mc = null;

            #endregion

            File.WriteAllLines(Result_Report, Results.ToArray());
            iApp.SetProgressValue(100, 100);
            iApp.Progress_OFF();
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
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (HA_Lanes.Count > 0)
                        {
                            //load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("*HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("MEMBER LOAD");
                            //if (chk_self_british.Checked)
                            //    load_lst.Add("SELFWEIGHT Y -1");

                            load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                            //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                            foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                            {
                                load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                            }
                        }
                    }

                }
                else
                    load_lst.AddRange(txt_Ana_member_load.Lines);
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
                        load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, P-2, BD 37/01]");
                        load_lst.Add("MEMBER LOAD");
                        //if (chk_self_british.Checked)
                        //    load_lst.Add("SELFWEIGHT Y -1");

                        load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                        //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                        foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                        {
                            load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                        }

                        if(Transverse_load.Count > 1)
                        {
                            load_lst.Add(string.Format("LOAD 2 TRANSVERSE LOAD"));
                            load_lst.AddRange(Transverse_load.ToArray());

                        }
                    }
                }
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                if ((rbtn_HB.Checked || rbtn_HA_HB.Checked || rbtn_Rail_Load.Checked)
                    || iApp.DesignStandard == eDesignStandard.IndianStandard)
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                //load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);


                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

                //if (load_no == 1)
                //    load_lst.AddRange(load_list_1.ToArray());
                //else if (load_no == 2)
                //    load_lst.AddRange(load_list_2.ToArray());
                //else if (load_no == 3)
                //    load_lst.AddRange(load_list_3.ToArray());
                //else if (load_no == 4)
                //    load_lst.AddRange(load_list_4.ToArray());
                //else if (load_no == 5)
                //    load_lst.AddRange(load_list_5.ToArray());
                //else if (load_no == 6)
                //    load_lst.AddRange(load_list_6.ToArray());
                //else if (load_no == 7)
                //    load_lst.AddRange(load_total_7.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {

            Analysis_Button_Enabled();

        }

        private void Analysis_Button_Enabled()
        {
            #region Set File Name

            string file_name = "";
            if (Bridge_Analysis != null)
            {
                if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
                {
                    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);
                }
                else
                {
                    file_name = Result_Report;
                }
            }

            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_View_Moving_Load.Enabled = File.Exists(MyList.Get_LL_TXT_File(file_name)) && File.Exists(MyList.Get_Analysis_Report_File(file_name));
            btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file.SelectedIndex != cmb_long_open_file.Items.Count - 1;
            btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
        }

        #endregion British Standard Loading

        private void uC_Deckslab_IS1_OnCreateData(object sender, EventArgs e)
        {

            Write_All_Data(true);

            uC_Deckslab_IS1.iApp = iApp;

            uC_Deckslab_IS1.user_path = user_path;



            Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                Bridge_Analysis.Inner_Girders_as_String,
                Bridge_Analysis.joints_list_for_load);


            uC_Deckslab_IS1.deck_member_load = deck_member_load;


            uC_Deckslab_IS1.L = L;
            uC_Deckslab_IS1.NMG = NMG;
            uC_Deckslab_IS1.SMG = SMG;
            //uC_Deckslab_IS1.os = os;
            uC_Deckslab_IS1.CL = CL;
            uC_Deckslab_IS1.Ds = Ds;
            uC_Deckslab_IS1.B = B;


            uC_Deckslab_IS1.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
            uC_Deckslab_IS1.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
            uC_Deckslab_IS1.Skew_Angle = MyList.StringToDouble(dgv_deck_user_input[1, 5].Value.ToString(), 0.0);
            uC_Deckslab_IS1.Number_Of_Long_Girder = MyList.StringToInt(txt_Ana_NMG.Text, 4);
            uC_Deckslab_IS1.Number_Of_Cross_Girder = MyList.StringToInt(txt_Ana_NCG.Text, 3);
            uC_Deckslab_IS1.WidthBridge = L / (NCG - 1);

        }

        private void uC_Deckslab_BS1_OnCreateData(object sender, EventArgs e)
        {
            Write_All_Data(false);

            uC_Deckslab_BS1.iApp = iApp;
            uC_Deckslab_BS1.user_path = user_path;
        }

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

                    uC_BRD1.user_path = user_path;
                    uC_BRD1.Set_VBAB_Input_Data();
                    uC_BRD1.Set_VFB_Input_Data();
                    uC_BRD1.Set_VMABL_Input_Data();
                    uC_BRD1.Set_VMABT_Input_Data();

                    uC_Deckslab_IS1_OnCreateData(sender, e);
                    Ana_OpenAnalysisFile(chk_file);
                    Show_ReadMemberLoad(Bridge_Analysis.TotalAnalysis_Input_File);
                    //Open_AnalysisFile(ofd.FileName);
                    Deck_Load_Analysis_Data();
                    Deck_Initialize_InputData();
                    //txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;
                    iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
                    //Chiranjit [2013 10 10]
                  


                    uC_RCC_Abut1.Modified_Cells();

                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        British_Interactive();
                        Default_British_HB_Type_LoadData(dgv_british_loads);

                        chk_HA_2L.Checked = true;
                        chk_HB_3L.Checked = true;
                    }

                    rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
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

        private void uC_RCC_Abut1_Abut_Counterfort_LS1_dead_load_CheckedChanged(object sender, EventArgs e)
        {
            if (uC_RCC_Abut1.uC_Abut_Counterfort_LS1.rbtn_dead_load.Checked)
            {
                uC_RCC_Abut1.uC_Abut_Counterfort_LS1.Reaction_A = txt_dead_vert_reac_kN.Text;
                uC_RCC_Abut1.uC_Abut_Counterfort_LS1.Reaction_B = txt_dead_vert_reac_kN.Text;
            }
        }

        private void uC_PierDesignLimitState1_OnProcess(object sender, EventArgs e)
        {
            Write_All_Data();
        }

        private void uC_RCC_Abut1_Load(object sender, EventArgs e)
        {

        }

        public void Change_LSM_Data()
        {
            uC_CompositeBridgeLSM1.txt_GEN_G2.Text = L.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G10.Text = B.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G11.Text = Wc.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G13.Text = Ds.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G15.Text = DMG.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G18.Text = NMG.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G9.Text = NCG.ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G20.Text = CL.ToString();


            uC_CompositeBridgeLSM1.txt_GEN_G37.Text = (Y_c / 10).ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G38.Text = (Y_c_Green / 10).ToString();
            uC_CompositeBridgeLSM1.txt_GEN_G39.Text = (Y_S / 10).ToString();




            uC_CompositeBridgeLSM1.txt_SUMM_I13.Text = txt_SUMM_I13.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I15.Text = txt_SUMM_I15.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I16.Text = txt_SUMM_I16.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I17.Text = txt_SUMM_I17.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I21.Text = txt_SUMM_I21.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I73.Text = txt_SUMM_I73.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I75.Text = txt_SUMM_I75.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I76.Text = txt_SUMM_I76.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I77.Text = txt_SUMM_I77.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_I81.Text = txt_SUMM_I81.Text;





            uC_CompositeBridgeLSM1.txt_SUMM_J13.Text = txt_SUMM_J13.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J15.Text = txt_SUMM_J15.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J16.Text = txt_SUMM_J16.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J17.Text = txt_SUMM_J17.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J21.Text = txt_SUMM_J21.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J73.Text = txt_SUMM_J73.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J75.Text = txt_SUMM_J75.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J76.Text = txt_SUMM_J76.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J77.Text = txt_SUMM_J77.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_J81.Text = txt_SUMM_J81.Text;






            uC_CompositeBridgeLSM1.txt_SUMM_K13.Text = txt_SUMM_K13.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K15.Text = txt_SUMM_K15.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K16.Text = txt_SUMM_K16.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K17.Text = txt_SUMM_K17.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K21.Text = txt_SUMM_K21.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K73.Text = txt_SUMM_K73.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K75.Text = txt_SUMM_K75.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K76.Text = txt_SUMM_K76.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K77.Text = txt_SUMM_K77.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_K81.Text = txt_SUMM_K81.Text;





            uC_CompositeBridgeLSM1.txt_SUMM_L13.Text = txt_SUMM_L13.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L15.Text = txt_SUMM_L15.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L16.Text = txt_SUMM_L16.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L17.Text = txt_SUMM_L17.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L21.Text = txt_SUMM_L21.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L73.Text = txt_SUMM_L73.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L75.Text = txt_SUMM_L75.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L76.Text = txt_SUMM_L76.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L77.Text = txt_SUMM_L77.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_L81.Text = txt_SUMM_L81.Text;



            uC_CompositeBridgeLSM1.txt_SUMM_M13.Text = txt_SUMM_M13.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M15.Text = txt_SUMM_M15.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M16.Text = txt_SUMM_M16.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M17.Text = txt_SUMM_M17.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M21.Text = txt_SUMM_M21.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M73.Text = txt_SUMM_M73.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M75.Text = txt_SUMM_M75.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M76.Text = txt_SUMM_M76.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M77.Text = txt_SUMM_M77.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_M81.Text = txt_SUMM_M81.Text;




            uC_CompositeBridgeLSM1.txt_SUMM_N13.Text = txt_SUMM_N13.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N15.Text = txt_SUMM_N15.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N16.Text = txt_SUMM_N16.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N17.Text = txt_SUMM_N17.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N21.Text = txt_SUMM_N21.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N73.Text = txt_SUMM_N73.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N75.Text = txt_SUMM_N75.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N76.Text = txt_SUMM_N76.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N77.Text = txt_SUMM_N77.Text;
            uC_CompositeBridgeLSM1.txt_SUMM_N81.Text = txt_SUMM_N81.Text;



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
                if (Curve_Radius == 0) txt_curve_radius.Text = "50";
            }
            else
            {
                txt_curve_radius.Text = "0";
            }
        }

        private void rbtn_singleSpan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_singleSpan.Checked)
            {
                //txt_multiSpan.Text = txt_Ana_L.Text;
            }
            else if (rbtn_multiSpan.Checked)
            {
                MyList ml = new MyList(txt_multiSpan.Text, ',');

                txt_Ana_L.Text = ml.SUM.ToString("f3");

                if(iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    txt_ll_british_incr.Text = "4.0";
                }
                else
                    txt_XINCR.Text = "4.0";

            }
        }

        private void txt_multiSpan_TextChanged(object sender, EventArgs e)
        {
            if (rbtn_multiSpan.Checked)
            {
                MyList ml = new MyList(txt_multiSpan.Text.Replace(","," "), ' ');
                txt_Ana_L.Text = ml.SUM.ToString("f3");
            }
        }

        private void lbl_orthotropic_Click(object sender, EventArgs e)
        {
            AstraAccess.ViewerFunctions.Form_OrthotropicEditor(iApp).Show();
        }
        void Write_Result_Summary()
        {

        }

        private void uC_Orthotropic1_OnDraw_Click(object sender, EventArgs e)
        {
            SectionElement elmt = new SectionElement();

            #region Draw Pen Ultimate Span


            var ES = Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span;

            elmt.Curve_Radius = Curve_Radius;
            elmt.L = L;
            elmt.Z = CL;
            elmt.Web_Thickness = ES.Bw / 1000;
            elmt.Web_Depth = ES.Dw / 1000;

            elmt.TF_THK = ES.Dft / 1000;
            elmt.TF_WD = ES.Bft / 1000;
            elmt.BF_THK = ES.Dfb / 1000;
            elmt.BF_WD = ES.Bfb / 1000;

            elmt.Lat_Spacing = ES.S / 1000;


            elmt.TP_WD = ES.Bt / 1000;
            elmt.TP_THK = ES.Dt / 1000;

            elmt.BP_WD = ES.Bb / 1000;
            elmt.BP_THK = ES.Db / 1000;

            elmt.SP_1_THK = ES.Bs1 / 1000;
            elmt.SP_2_THK = ES.Bs2 / 1000;
            elmt.SP_3_THK = ES.Bs3 / 1000;
            elmt.SP_4_THK = ES.Bs4 / 1000;

            elmt.SP_1_WD = ES.Ds1 / 1000;
            elmt.SP_2_WD = ES.Ds2 / 1000;
            elmt.SP_3_WD = ES.Ds3 / 1000;
            elmt.SP_4_WD = ES.Ds4 / 1000;



            uC_Orthotropic1.DrawElement(elmt);

            //return;



            elmt.Z = CL + SMG;


            elmt.L = L;
            elmt.Web_Thickness = ES.Bw / 1000;
            elmt.Web_Depth = ES.Dw / 1000;

            elmt.TF_THK = ES.Dft / 1000;
            elmt.TF_WD = ES.Bft / 1000;
            elmt.BF_THK = ES.Dfb / 1000;
            elmt.BF_WD = ES.Bfb / 1000;

            elmt.Lat_Spacing = ES.S / 1000;


            elmt.SP_1_THK = ES.Bs1 / 1000;
            elmt.SP_2_THK = ES.Bs2 / 1000;
            elmt.SP_3_THK = ES.Bs3 / 1000;
            elmt.SP_4_THK = ES.Bs4 / 1000;

            elmt.SP_1_WD = ES.Ds1 / 1000;
            elmt.SP_2_WD = ES.Ds2 / 1000;
            elmt.SP_3_WD = ES.Ds3 / 1000;
            elmt.SP_4_WD = ES.Ds4 / 1000;





            uC_Orthotropic1.DrawElement(elmt);




            elmt.X = 0;
            elmt.Y = 0;
            elmt.Z = CL + SMG * 2;


            elmt.L = L;
            elmt.Web_Thickness = ES.Bw / 1000;
            elmt.Web_Depth = ES.Dw / 1000;

            elmt.TF_THK = ES.Dft / 1000;
            elmt.TF_WD = ES.Bft / 1000;
            elmt.BF_THK = ES.Dfb / 1000;
            elmt.BF_WD = ES.Bfb / 1000;

            elmt.Lat_Spacing = ES.S / 1000;


            elmt.SP_1_THK = ES.Bs1 / 1000;
            elmt.SP_2_THK = ES.Bs2 / 1000;
            elmt.SP_3_THK = ES.Bs3 / 1000;
            elmt.SP_4_THK = ES.Bs4 / 1000;

            elmt.SP_1_WD = ES.Ds1 / 1000;
            elmt.SP_2_WD = ES.Ds2 / 1000;
            elmt.SP_3_WD = ES.Ds3 / 1000;
            elmt.SP_4_WD = ES.Ds4 / 1000;


            uC_Orthotropic1.DrawElement(elmt);


            #endregion Draw Pen Ultimate Span


            if (true)
            {

                #region Deck Slab


                elmt = new SectionElement();

                elmt.Curve_Radius = Curve_Radius;

                elmt.Y = ES.Dw / 1000 + ES.Dt / 1000 + ES.Dfb / 1000 + ES.Db / 1000 + ES.Dfb / 1000;
                elmt.Z = B / 2;
                elmt.L = L;
                elmt.Web_Thickness = B;
                elmt.Web_Depth = Ds;

                elmt.Color_Web_Plate = Color.White;
                uC_Orthotropic1.DrawElement(elmt);

                #endregion Deckslab
                ES = Bridge_Analysis.Steel_Section.Section_Cross_Girder;

                for (int i = 0; i <= 10; i++)
                {
                    #region Cross Girder
                    if (i == 0)
                    {
                        //elmt.X = L - (ES.Bfb / 2) / 1000;
                        //elmt.X = L - (ES.Bfb + ES.Dw / 2) / 1000;
                        elmt.X = L - (ES.Bfb / 2 + ES.Bw) / 1000;
                    }
                    else if (i == 10)
                    {
                        elmt.X = (ES.Bfb / 2) / 1000;
                    }
                    else
                    {
                        elmt.X = L - (L / 10) * i;
                    }
                    //elmt.Y = ((Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span.Dw - ES.Dw) / 2) / 1000;

                    var EL = Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span;

                    elmt.Y = (EL.Dw - ES.Dw + ES.Dt + ES.Dft) / 1000;

                    if (rbtn_sec_box.Checked)
                    {
                        elmt.Z = CL - (Bridge_Analysis.Steel_Section.Section_Long_Girder_at_End_Span.S / 2)/1000;
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
                    elmt.Web_Thickness = ES.Bw / 1000;
                    elmt.Web_Depth = ES.Dw / 1000;

                    elmt.TF_THK = ES.Dft / 1000;
                    elmt.TF_WD = ES.Bft / 1000;
                    elmt.BF_THK = ES.Dfb / 1000;
                    elmt.BF_WD = ES.Bfb / 1000;




                    elmt.TP_THK = ES.Dt / 1000;
                    elmt.TP_WD = ES.Bt / 1000;
                    elmt.BP_THK = ES.Db / 1000;
                    elmt.BP_WD = ES.Bb / 1000;



                    elmt.Lat_Spacing = ES.S / 1000;


                    elmt.SP_1_THK = ES.Bs1 / 1000;
                    elmt.SP_2_THK = ES.Bs2 / 1000;
                    elmt.SP_3_THK = ES.Bs3 / 1000;
                    elmt.SP_4_THK = ES.Bs4 / 1000;

                    elmt.SP_1_WD = ES.Ds1 / 1000;
                    elmt.SP_2_WD = ES.Ds2 / 1000;
                    elmt.SP_3_WD = ES.Ds3 / 1000;
                    elmt.SP_4_WD = ES.Ds4 / 1000;


                    uC_Orthotropic1.DrawElement(elmt);

                    #endregion

                }
            }


        }

        private void rbtn_steel_deck_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtn_steel_deck.Checked)
            {
                Ds = 0.01;
            }
            else
            {
                Ds = 0.21;
            }


        }

    }


}