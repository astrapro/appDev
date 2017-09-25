﻿using System;
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
//using LimitStateMethod.LS_Progress;


namespace LimitStateMethod.Composite
{


    public partial class frm_Composite_LS : Form
    {
        //const string Title = "ANALYSIS OF COMPOSITE BRIDGE (LIMIT STATE METHOD)";


        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "COMPOSITE BRIDGE LIMIT STATE [BS]";
                return "COMPOSITE BRIDGE LIMIT STATE [IRC]";
            }
        }



        Composite_LS_Analysis Bridge_Analysis = null;
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
        public frm_Composite_LS(IApplication app)
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

        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
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
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    Bridge_Analysis.CreateData();

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



                    Bridge_Analysis.CreateData_British();

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
                if (File.Exists(file_name))
                    iApp.OpenWork(file_name, false);
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_View_Moving_Load.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                {
                    iApp.OpenWork(file_name, true);
                }
            }
        }

        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, false);
        }

        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
            try
            {
                #region Process
                int i = 0;
                //Chiranjit [2012 07 13]
                Write_All_Data();

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();


                string flPath = Bridge_Analysis.Input_File;

                iApp.Progress_Works.Clear();
                do
                {
                    if (i == 0)
                        flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;
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
                        flPath = Bridge_Analysis.GetAnalysis_Input_File(i);
                    }


                   

                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");

                    i++;
                }
                while (i < (3 + all_loads.Count));


                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                if (iApp.Show_and_Run_Process_List(pcol))
                {
                    //iApp.Progress_Works.Clear();
                    //iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File (ANALYSIS_REP.TXT)");

                    Bridge_Analysis.Structure = null;
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.Total_Analysis_Report);

                    BridgeMemberAnalysis LL_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.LiveLoad_Analysis_Report);
                    BridgeMemberAnalysis DL_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);


                    for (i = 0; i < all_loads.Count;i++)
                    {
                        Bridge_Analysis.All_Analysis.Add(new BridgeMemberAnalysis(iApp,
                            MyList.Get_Analysis_Report_File(Bridge_Analysis.GetAnalysis_Input_File(i + 3))));

                    }




                    Show_Moment_Shear();

                    string s1 = "";
                    string s2 = "";
                    for (i = 0; i < Bridge_Analysis.Structure.Supports.Count; i++)
                    {
                        if (i < Bridge_Analysis.Structure.Supports.Count / 2)
                        {
                            if (i == Bridge_Analysis.Structure.Supports.Count / 2 - 1)
                            {
                                s1 += Bridge_Analysis.Structure.Supports[i].NodeNo;
                            }
                            else
                                s1 += Bridge_Analysis.Structure.Supports[i].NodeNo + ",";
                        }
                        else
                        {
                            if (i == Bridge_Analysis.Structure.Supports.Count - 1)
                            {
                                s2 += Bridge_Analysis.Structure.Supports[i].NodeNo;
                            }
                            else
                                s2 += Bridge_Analysis.Structure.Supports[i].NodeNo + ",";
                        }
                    }
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
                    frm_ViewForces_Load();

                    frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
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
                grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


                Deck_Load_Analysis_Data();
                Deck_Initialize_InputData();
                Button_Enable_Disable();

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

            load_lst.Add("LOAD    1   " + s);
            load_lst.Add("MEMBER LOAD");
            if (add_DeadLoad)
            {
                load_lst.AddRange(txt_Ana_member_load.Lines);

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Bridge_Analysis.LiveLoad_File))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + Path.GetDirectoryName(file_name));
                    }

                }
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
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
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;
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
        #endregion  Composite Analysis Form Events
        #region Composite Methods
        void Analysis_Initialize_InputData()
        {
            Bridge_Analysis.Length = L;
            Bridge_Analysis.WidthBridge = B;
            Bridge_Analysis.Width_LeftCantilever = CL;
            Bridge_Analysis.Width_RightCantilever = CR;
            Bridge_Analysis.Skew_Angle = Ang;
            Bridge_Analysis.Effective_Depth = Deff;
            Bridge_Analysis.NMG = NMG;
            Bridge_Analysis.NCG = NCG;
            Bridge_Analysis.Ds = Ds;






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

            grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

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

            grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

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
            txt_Ana_NMG.SelectedIndex = 1;

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

                cmb_HB.SelectedIndex = 0;

                British_Interactive();

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
            else
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

            #endregion RCC Pier





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




        }
        private void btn_dwg_copm_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            if (btn.Name == btn_dwg_construction.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Construction Drawings"), "COST_Composite_Bridges");
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

            if (cmb.Name == cmb_L_2_ang_section_code.Name)
            {
                Deck.tbl_rolledSteelAngles = cmb_L_2_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;


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
                Deck.tbl_rolledSteelAngles = cmb_L_4_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;
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
                Deck.tbl_rolledSteelAngles = cmb_Deff_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;
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
                Deck.tbl_rolledSteelAngles = cmb_Deff_ang_section_name.Text.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;
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
                if (cmb_L_2_ang_section_code.Items.Count > 0)
                {
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

                grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
                grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

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
            //uC_Abut1.Overhang = og;

        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
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

        public void Calculate_Load_Computation(string outer_girders, string inner_girders, List<string> joints_nos)
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
            member_load.Add(string.Format("LOAD 1 DL + SIDL"));
            member_load.Add(string.Format("MEMBER LOAD"));
            //if (wiu > wou)
            //{
            //    txt_ana_inner_long_girder_mem_load.Text = wou.ToString("f4");
            //    txt_ana_outer_long_girder_mem_load.Text = wiu.ToString("f4");
            //}
            //else
            //{
            //    txt_ana_inner_long_girder_mem_load.Text = wiu.ToString("f4");
            //    txt_ana_outer_long_girder_mem_load.Text = wou.ToString("f4");

            //}

            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, txt_ana_inner_long_girder_mem_load.Text));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, txt_ana_outer_long_girder_mem_load.Text)); //Chiranjit [2013 06 07]
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wiu));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wou)); //Chiranjit [2013 06 07]


            list.Add(string.Format("                JOINT LOAD"));
            member_load.Add(string.Format("JOINT LOAD"));
            foreach (var item in joints_nos)
            {
                list.Add(string.Format("                {0} FY -{1:f4}", item, wjl));
                member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl));

            }
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));




            //Chiranjit [2013 10 05]
            //Dead Load Value for DeckSlab analysis
            #region Dead Load Value for DeckSlab analysis

            deck_member_load.Clear();
            deck_member_load.Add(wo3/100);
            deck_member_load.Add(wo2/100);
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
            //iApp = app;
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
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

            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    sr = DL_support_reactions.Get_Data(mlist.GetInt(i));
                    dgv_left_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));

                    tot_dead_vert_reac += Math.Abs(sr.Max_Reaction); ;
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
                txt_Ana_L.Text = "0.0";
                txt_Ana_L.Text = "19.58";
                txt_Ana_B.Text = "12.0";
                txt_Ana_CW.Text = "11.0";
                txt_Ana_NMG.Text = "4";
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

                    txt_Ana_L.Text = "19.58";
                    txt_Ana_B.Text = "12.0";
                    txt_Ana_CL.Text = "1.075";
                    txt_Ana_CR.Text = "1.075";
                    txt_Ana_NMG.Text = "4";
                    txt_Ana_NCG.Text = "5";
                    txt_Ana_ang.Text = "26";

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
                    txt_Ana_NMG.Text = "4";
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
                grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;



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
                    //file_name = File_Long_Girder_Results;
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
                    IsRead = true;
                    //iApp.Read_Form_Record(this, user_path);
                    IsRead = false;


                    uC_RCC_Abut1.Modified_Cells();
 
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        British_Interactive();
                        Default_British_HB_Type_LoadData(dgv_british_loads);
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

    }


    public class Composite_LS_Analysis
    {
        IApplication iApp;

        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Structure = null;

        public BridgeMemberAnalysis DL_Analysis = null;
        public BridgeMemberAnalysis LL_Analysis = null;

        //public BridgeMemberAnalysis Structure = null;

        public List<BridgeMemberAnalysis> All_Analysis = null;

        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;

        public PreStressedConcrete_SectionProperties PSC_Mid_Span { get; set; }
        public PreStressedConcrete_SectionProperties PSC_End { get; set; }
        public PreStressedConcrete_SectionProperties PSC_Cross { get; set; }

        public string _DeckSlab { get; set; }
        public string _Inner_Girder_Mid { get; set; }
        public string _Inner_Girder_Support { get; set; }
        public string _Outer_Girder_Mid { get; set; }
        public string _Outer_Girder_Support { get; set; }
        public string _Cross_Girder_Inter { get; set; }
        public string _Cross_Girder_End { get; set; }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        public CompositeSection Steel_Section { get; set; }

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;


        //Chiranjit [2013 06 06]
        string list_envelop_outer = "";
        string list_envelop_inner = "";

        string input_file, working_folder, user_path;
        public Composite_LS_Analysis(IApplication thisApp)
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
                if(NCG % 2 == 0.0)
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

            if (Directory.Exists(working_folder))
            {
                string pd = Path.Combine(working_folder, "LL ANALYSIS LOAD " + analysis_no);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_LOAD_" + analysis_no + "_INPUT_FILE.txt");
            }
            return "";
        }


        //Chiranjit [2012 05 27]
        public string DeadLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(working_folder))
                {
                    string pd = Path.Combine(working_folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
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
        public string DeadLoad_Analysis_Report
        {
            get
            {
                if (File.Exists(DeadLoadAnalysis_Input_File))
                    return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
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
        public double L1 { get { return 0.0; } }
        public double L2 { get { return Effective_Depth; } }
        public double L3 { get { return Length / 5.3; } }
        public double L4 { get { return Length / 3.65; } }
        public double L5 { get { return Length / 3.05; } }
        public double L6 { get { return Length / 2.61; } }
        public double L7 { get { return Length / 2.28; } }
        public double L8 { get { return Length / 2.03; } }
        public double L9 { get { return Length / 2.00; } }
        public double NMG { get; set; }
        public double NCG { get; set; }

        public double Ds { get; set; }


        public double Mid_Span_Length { get { return Length / 2.0; } }
        public double Penultimate_Span_Length { get { return Length / 4.0; } }


        #region Chiranjit [2013 05 06]
        //Chiranjit [2013 05 06]
        public List<string> joints_list_for_load = new List<string>();

        //Chiranjit [2013 05 06]
        string support_left_joints = "";
        string support_right_joints = "";

        public string L2_Girders_as_String { get; set; }
        public string L4_Girders_as_String { get; set; }
        public string Deff_Girders_as_String { get; set; }
        public string Inner_Girders_as_String { get; set; }
        public string Outer_Girders_as_String { get; set; }

        public string Cross_Girders_as_String { get; set; }


        public List<int> _L2_inn_joints = new List<int>();
        public List<int> _L4_inn_joints = new List<int>();
        public List<int> _deff_inn_joints = new List<int>();


        public List<int> _L3_out_joints = new List<int>();
        public List<int> _L6_out_joints = new List<int>();
        public List<int> _3L8_out_joints = new List<int>();


        public List<int> _L3_inn_joints = new List<int>();
        public List<int> _L6_inn_joints = new List<int>();
        public List<int> _3L8_inn_joints = new List<int>();


        public List<int> _L2_out_joints = new List<int>();
        public List<int> _L4_out_joints = new List<int>();
        public List<int> _deff_out_joints = new List<int>();

        List<int> _HA_Joints = new List<int>();

        void Set_Inner_Outer_Cross_Girders()
        {

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();
            
            


            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else if (HA_distances.Contains(MemColls[i].StartNode.Z))
                {
                    //Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000")) ||
                    (MemColls[i].StartNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000")))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z == WidthBridge) &&
                    (MemColls[i].EndNode.Z == WidthBridge))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    Inner_Girder.Add(MemColls[i].MemberNo);
                }
            }
            Inner_Girder.Sort();
            Outer_Girder.Sort();
            Cross_Girder.Sort();


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            Inner_Girders_as_String = MyList.Get_Array_Text(Inner_Girder);
            Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);

        }

        void Set_Girders()
        {


            List<int> L2_Girders = new List<int>();
            List<int> L4_Girders = new List<int>();
            List<int> Deff_Girders = new List<int>();
            List<int> Cross_Girder = new List<int>();

            List<int> HA_Members = new List<int>();



            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    if (_L2_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L2_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L2_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L4_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L4_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L4_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_HA_Joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        HA_Members.Add(MemColls[i].MemberNo);
                    }
                    //else if (_deff_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                    //   _deff_inn_joints.Contains(MemColls[i].StartNode.NodeNo))
                    //{
                    //    Deff_Girders.Add(MemColls[i].MemberNo);
                    //}
                    else 
                    {
                        Deff_Girders.Add(MemColls[i].MemberNo);
                    }

                }
            }
            L2_Girders.Sort();
            L4_Girders.Sort();
            Deff_Girders.Sort();
     
            Cross_Girder.Sort();

            //HA_Loading_Members
            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            L2_Girders_as_String = MyList.Get_Array_Text(L2_Girders);
            L4_Girders_as_String = MyList.Get_Array_Text(L4_Girders);
            Deff_Girders_as_String = MyList.Get_Array_Text(Deff_Girders);

            HA_Loading_Members = MyList.Get_Array_Text(HA_Members);

            //Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);
            Set_Inner_Outer_Cross_Girders();
        }

        void Set_L2_L4_Deff_Girders()
        {
            double L = Length;
            double W = WidthBridge;
            double val = L / 2;
            int i = 0;

            _L2_inn_joints.Clear();;
            _L4_inn_joints.Clear();;
            _deff_inn_joints.Clear();;

            _L2_out_joints.Clear();;
            _L4_out_joints.Clear();;
            _deff_out_joints.Clear();


            _L3_inn_joints.Clear();
            _L6_inn_joints.Clear();
            _3L8_inn_joints.Clear();

            _L3_out_joints.Clear();
            _L6_out_joints.Clear();
            _3L8_out_joints.Clear();


            _HA_Joints.Clear(); ;





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

            for (i = 0; i < Joints.Count; i++)
            {
                try
                {
                    //if ((Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right)) == false) continue;


                    x_min = _X_min[_Z_joints.IndexOf(Joints[i].Z)];

                    if (HA_distances.Contains(Joints[i].Z))
                    {
                        //if (Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right))
                        _HA_Joints.Add(Joints[i].NodeNo);
                    }
                    else if ((Joints[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right))
                        _L2_inn_joints.Add(Joints[i].NodeNo);
                    }
                    else if (Joints[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                            _L4_inn_joints.Add(Joints[i].NodeNo);
                    }
                    else if (Joints[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                            _L4_out_joints.Add(Joints[i].NodeNo);
                    }

                    else if (Joints[i].X.ToString("0.0") == ((L / 3.0) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _L3_inn_joints.Add(Joints[i].NodeNo);
                    }
                    else if (Joints[i].X.ToString("0.0") == ((L - (L / 3.0)) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _L3_out_joints.Add(Joints[i].NodeNo);
                    }


                    else if (Joints[i].X.ToString("0.0") == ((L / 6.0) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _L6_inn_joints.Add(Joints[i].NodeNo);
                    }
                    else if (Joints[i].X.ToString("0.0") == ((L - (L / 6.0)) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _L6_out_joints.Add(Joints[i].NodeNo);
                    }
                    else if (Joints[i].X.ToString("0.0") == ((3*L / 8.0) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _3L8_inn_joints.Add(Joints[i].NodeNo);
                    }
                    else if (Joints[i].X.ToString("0.0") == ((L - (3*L / 8.0)) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _3L8_out_joints.Add(Joints[i].NodeNo);
                    }
                    else if ((Joints[i].X.ToString("0.0") == (Effective_Depth + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                            _deff_inn_joints.Add(Joints[i].NodeNo);
                    }
                    else if ((Joints[i].X.ToString("0.0") == (L - Effective_Depth + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                            _deff_out_joints.Add(Joints[i].NodeNo);
                    }
                }
                catch (Exception ex) {  }
            }

            #region L/2
            if (_L2_inn_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    _L2_out_joints.Add(_L2_inn_joints[0]);
                    _L2_inn_joints.RemoveAt(0);
                }
                _L2_out_joints.Add(_L2_inn_joints[0]);
                _L2_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {

                    _L2_out_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
                    _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
                }
                _L2_out_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
                _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            }


            #endregion

            List<int> temp_joints = new List<int>();


            #region L/4

            if (_L4_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L4_inn_joints[0]);
                    _L4_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L4_inn_joints[0]);
                _L4_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L4_inn_joints[_L4_inn_joints.Count - 1]);
                    _L4_inn_joints.RemoveAt(_L4_inn_joints.Count - 1);
                }
                temp_joints.Add(_L4_inn_joints[_L4_inn_joints.Count - 1]);
                _L4_inn_joints.RemoveAt(_L4_inn_joints.Count - 1);
            }

            if (_L4_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L4_out_joints[0]);
                    _L4_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L4_out_joints[0]);
                _L4_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L4_out_joints[_L4_out_joints.Count - 1]);
                    _L4_out_joints.RemoveAt(_L4_out_joints.Count - 1);
                }
                temp_joints.Add(_L4_out_joints[_L4_out_joints.Count - 1]);
                _L4_out_joints.RemoveAt(_L4_out_joints.Count - 1);
            }
            _L4_inn_joints.AddRange(_L4_out_joints.ToArray());

            _L4_out_joints.Clear();
            _L4_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion


            #region L/6


            if (_L6_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L6_inn_joints[0]);
                    _L6_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L6_inn_joints[0]);
                _L6_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L6_inn_joints[_L6_inn_joints.Count - 1]);
                    _L4_inn_joints.RemoveAt(_L6_inn_joints.Count - 1);
                }
                temp_joints.Add(_L6_inn_joints[_L6_inn_joints.Count - 1]);
                _L6_inn_joints.RemoveAt(_L6_inn_joints.Count - 1);
            }

            if (_L6_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L6_out_joints[0]);
                    _L6_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L6_out_joints[0]);
                _L6_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L6_out_joints[_L6_out_joints.Count - 1]);
                    _L6_out_joints.RemoveAt(_L6_out_joints.Count - 1);
                }
                temp_joints.Add(_L4_out_joints[_L6_out_joints.Count - 1]);
                _L6_out_joints.RemoveAt(_L6_out_joints.Count - 1);
            }
            _L6_inn_joints.AddRange(_L6_out_joints.ToArray());

            _L6_out_joints.Clear();
            _L6_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion L/6



            #region L/3


            if (_L3_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L3_inn_joints[0]);
                    _L3_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L3_inn_joints[0]);
                _L3_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L3_inn_joints[_L3_inn_joints.Count - 1]);
                    _L3_inn_joints.RemoveAt(_L3_inn_joints.Count - 1);
                }
                temp_joints.Add(_L6_inn_joints[_L3_inn_joints.Count - 1]);
                _L3_inn_joints.RemoveAt(_L3_inn_joints.Count - 1);
            }

            if (_L3_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L3_out_joints[0]);
                    _L3_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L3_out_joints[0]);
                _L3_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L3_out_joints[_L3_out_joints.Count - 1]);
                    _L3_out_joints.RemoveAt(_L3_out_joints.Count - 1);
                }
                temp_joints.Add(_L3_out_joints[_L3_out_joints.Count - 1]);
                _L3_out_joints.RemoveAt(_L3_out_joints.Count - 1);
            }
            _L3_inn_joints.AddRange(_L3_out_joints.ToArray());

            _L3_out_joints.Clear();
            _L3_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion L/6



            #region 3L/8


            if (_3L8_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_3L8_inn_joints[0]);
                    _3L8_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_3L8_inn_joints[0]);
                _3L8_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L3_inn_joints[_3L8_inn_joints.Count - 1]);
                    _3L8_inn_joints.RemoveAt(_3L8_inn_joints.Count - 1);
                }
                temp_joints.Add(_3L8_inn_joints[_3L8_inn_joints.Count - 1]);
                _3L8_inn_joints.RemoveAt(_3L8_inn_joints.Count - 1);
            }

            if (_3L8_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_3L8_out_joints[0]);
                    _3L8_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_3L8_out_joints[0]);
                _3L8_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_3L8_out_joints[_3L8_out_joints.Count - 1]);
                    _3L8_out_joints.RemoveAt(_3L8_out_joints.Count - 1);
                }
                temp_joints.Add(_3L8_out_joints[_3L8_out_joints.Count - 1]);
                _3L8_out_joints.RemoveAt(_3L8_out_joints.Count - 1);
            }
            _3L8_inn_joints.AddRange(_3L8_out_joints.ToArray());

            _3L8_out_joints.Clear();
            _3L8_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion 3L/8


            #region Deff

            if (_deff_inn_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_deff_inn_joints[0]);
                    _deff_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_deff_inn_joints[0]);
                _deff_inn_joints.RemoveAt(0);
                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_deff_inn_joints[_deff_inn_joints.Count - 1]);
                    _deff_inn_joints.RemoveAt(_deff_inn_joints.Count - 1);
                }
                temp_joints.Add(_deff_inn_joints[_deff_inn_joints.Count - 1]);
                _deff_inn_joints.RemoveAt(_deff_inn_joints.Count - 1);
            }
            if (_deff_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_deff_out_joints[0]);
                    _deff_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_deff_out_joints[0]);
                _deff_out_joints.RemoveAt(0);
                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_deff_out_joints[_deff_out_joints.Count - 1]);
                    _deff_out_joints.RemoveAt(_deff_out_joints.Count - 1);
                }
                temp_joints.Add(_deff_out_joints[_deff_out_joints.Count - 1]);
                _deff_out_joints.RemoveAt(_deff_out_joints.Count - 1);
            }

            _deff_inn_joints.AddRange(_deff_out_joints.ToArray());

            _deff_out_joints.Clear();
            _deff_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion Deff

            Set_Girders();
        }
       
        #endregion Chiranjit [2013 05 06]


        public void CreateData()
        {
            if (iApp.DesignStandard == eDesignStandard.IndianStandard) CreateData_Indian();
            else CreateData_British();

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

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();
            list_x.Add(0.0);
            last_x = Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            last_x = Length -  (Length / 4.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length - Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            if (NCG % 2 != 0)
                last_x = x_incr;
            else
                last_x = x_incr + Effective_Depth;

            int i = 0;
            bool flag = true;
            do
            {
                flag = false;
                for (i = 0; i < list_x.Count; i++)
                {
                    if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                if (x_incr == 0.0) break;

            }
            while (last_x <= Length);
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
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
                    #endregion Chiranjit [2013 05 06]
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

        public void CreateData_Indian()
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

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();
            list_x.Add(0.0);
            list_x.Add(0.5);
            list_x.Add(Length - 0.5);
            last_x = Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 6.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 3.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length * 3.0/8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - (Length * 3.0 / 8.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - Length / 3.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length - (Length / 4.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - (Length / 6.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length - Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            if (NCG % 2 != 0)
                last_x = x_incr;
            else
                last_x = x_incr + Effective_Depth;

            int i = 0;
            bool flag = true;
            do
            {
                flag = false;
                for (i = 0; i < list_x.Count; i++)
                {
                    if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                if (x_incr == 0.0) break;

            }
            while (last_x <= Length);
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
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
                    #endregion Chiranjit [2013 05 06]
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

        public void CreateData_British()
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

            last_x = 0.0;


            int i = 0;
            bool flag = true;

            if (false)
            {
                #region Chiranjit [2011 09 23] Correct Create Data

                list_x.Clear();
                list_x.Add(0.0);
                last_x = Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                last_x = Length / 4.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                last_x = Length / 2.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);



                last_x = Length - (Length / 4.0);
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                last_x = Length - Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);


                last_x = Length;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                if (NCG % 2 != 0)
                    last_x = x_incr;
                else
                    last_x = x_incr + Effective_Depth;

                 i = 0;
                 flag = true;
                do
                {
                    flag = false;
                    for (i = 0; i < list_x.Count; i++)
                    {
                        if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                        {
                            flag = true; break;
                        }
                    }

                    if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                        list_x.Add(last_x);
                    last_x += x_incr;
                    last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                    if (x_incr == 0.0) break;

                }
                while (last_x <= Length);
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


                //Chiranjit [2014 09 08]
                //List<double> HA_distances = new List<double>();
                HA_distances.Clear();
                if (HA_Lanes.Count > 0)
                {
                    double ha = 0.0;

                    for (i = 0; i < HA_Lanes.Count; i++)
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
            }
            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();
            list_x.Add(0.0);
            list_x.Add(0.5);
            list_x.Add(Length - 0.5);
            last_x = Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 6.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 3.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length * 3.0 / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - (Length * 3.0 / 8.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - Length / 3.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length - (Length / 4.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - (Length / 6.0);
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length - Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            if (NCG % 2 != 0)
                last_x = x_incr;
            else
                last_x = x_incr + Effective_Depth;

            //int i = 0;
            //bool flag = true;
            do
            {
                flag = false;
                for (i = 0; i < list_x.Count; i++)
                {
                    if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                if (x_incr == 0.0) break;

            }
            while (last_x <= Length);
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
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
                    #endregion Chiranjit [2013 05 06]
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

        public void CreateData_2013_05_03()
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

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();
            list_x.Add(0.0);
            last_x = Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = x_incr;

            int i = 0;
            bool flag = true;
            do
            {
                flag = false;
                for (i = 0; i < list_x.Count; i++)
                {
                    if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                if (x_incr == 0.0) break;

            }
            while (last_x <= Length);
            list_x.Sort();



            list_z.Add(0);
            last_z = Width_LeftCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = Width_LeftCantilever / 2;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = WidthBridge - Width_RightCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);
            last_z = WidthBridge - (Width_RightCantilever / 2.0);
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);


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

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

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
        }

        public void CreateData(bool isPSC_I_Girder)
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

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


            //double L1, L2, L3, L4, L5, L6, L7, L8, L9;



            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();
            list_x.Add(L1);
            last_x = L2;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L3;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L4;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L5;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L6;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L7;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L8;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L9;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length - L8;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = Length - L7;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            last_x = Length - L6;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);




            last_x = Length - L5;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            last_x = Length - L4;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            last_x = Length - L3;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);




            last_x = Length - L2;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);





            last_x = Length - L1;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);





            //last_x = Length - Effective_Depth;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);


            //last_x = Length;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = x_incr;

            int i = 0;
            bool flag = true;
            //do
            //{
            //    flag = false;
            //    //for (i = 0; i < list_x.Count; i++)
            //    //{
            //    //    last_x = list_x[i];
            //    //    //if (list_x.Contains(last_x))
            //    //    //{
            //    //    //    list_x.Remove(last_x);
            //    //    //    list_x.Add(last_x);
            //    //    //}
            //    //    //if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
            //    //    //{
            //    //    //    flag = true; break;
            //    //    //}
            //    //}

            //    //if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
            //    //    list_x.Add(last_x);
            //    //last_x += x_incr;
            //    //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //    //if (x_incr == 0.0) break;

            //}
            //while (last_x <= Length);
            list_x.Sort();



            list_z.Add(0);
            last_z = Width_LeftCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = Width_LeftCantilever / 2;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = WidthBridge - Width_RightCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);
            last_z = WidthBridge - (Width_RightCantilever / 2.0);
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);


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

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

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


        public List<int> HA_Lanes;
        List<double> HA_distances = new List<double>();

        public string HA_Loading_Members;

      
        public void WriteData_Total_Analysis(string file_name)
        {
            WriteData_Total_Analysis(file_name, false);
        }
        public void WriteData_Total_Analysis(string file_name, bool is_british, List<string> ll_data)
        {
            WriteData_Total_Analysis(file_name, false, is_british);

            string fn = Path.GetDirectoryName(file_name);
            fn = Path.Combine(fn, "LL.TXT");
            File.WriteAllLines(fn, ll_data.ToArray());


        }
        public void WriteData_Total_Analysis(string file_name, bool is_PSC_I_Girder, bool is_british)
        {

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
            if (HA_Lanes != null)
            {
                for (i = 0; i < HA_Lanes.Count; i++)
                {
                    HA_Dists.Add(1.75 + (HA_Lanes[i] - 1) * 3.5);
                }
            }

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
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
            if (is_PSC_I_Girder)
            {
                List<string> ll = new List<string>();
                if (PSC_Cross != null)
                {
                    ll.Add(string.Format("{0} TO {1} {2} TO {3} PRIS AX {4:f4} IX {5:f4} IZ {6:f4}",
                       Cross_Girder_Members_Array[0, 0].MemberNo,
                       Cross_Girder_Members_Array[_Rows - 2, 0].MemberNo,
                       Cross_Girder_Members_Array[0, _Columns - 1].MemberNo,
                       Cross_Girder_Members_Array[_Rows - 2, _Columns - 1].MemberNo,
                       PSC_End.AX_Sq_M, PSC_End.IX, PSC_End.IZ));


                    list.Add(string.Format("{0} TO {1} {2} TO {3} PRIS AX {4:f4} IX {5:f4} IZ {6:f4}",
                        Cross_Girder_Members_Array[0, 0].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, 0].MemberNo,
                        Cross_Girder_Members_Array[0, _Columns - 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 1].MemberNo,
                        PSC_End.AX_Sq_M, PSC_End.IX, PSC_End.IZ));
                }
                if (PSC_Cross != null)
                {
                    ll.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Cross_Girder_Members_Array[0, 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 2].MemberNo,
                        PSC_Cross.AX_Sq_M, PSC_Cross.IX, PSC_Cross.IZ));
                    list.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Cross_Girder_Members_Array[0, 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 2].MemberNo,
                        PSC_Cross.AX_Sq_M, PSC_Cross.IX, PSC_Cross.IZ));
                }
                if (PSC_Mid_Span != null)
                {
                    ll.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Long_Girder_Members_Array[0, 0].MemberNo,
                        Long_Girder_Members_Array[_Rows - 1, _Columns - 2].MemberNo,
                        PSC_Mid_Span.AX_Sq_M, PSC_Mid_Span.IX, PSC_Mid_Span.IZ));
                    list.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Long_Girder_Members_Array[0, 0].MemberNo,
                        Long_Girder_Members_Array[_Rows - 1, _Columns - 2].MemberNo,
                        PSC_Mid_Span.AX_Sq_M, PSC_Mid_Span.IX, PSC_Mid_Span.IZ));

                }
                if ((PSC_Cross == null) && (PSC_Mid_Span == null) && (PSC_End == null))
                    list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));
            }
            else if (Steel_Section != null)
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
            if (is_psc_I_Girder)
            {

                if (PSC_Cross != null)
                {
                    list.Add(string.Format("{0} TO {1} {2} TO {3} PRIS AX {4:f4} IX {5:f4} IZ {6:f4}",
                        Cross_Girder_Members_Array[0, 0].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, 0].MemberNo,
                        Cross_Girder_Members_Array[0, _Columns - 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 1].MemberNo,
                        PSC_End.AX_Sq_M, PSC_End.IX, PSC_End.IZ));
                }
                if (PSC_Cross != null)
                {
                    list.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Cross_Girder_Members_Array[0, 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 2].MemberNo,
                        PSC_Cross.AX_Sq_M, PSC_Cross.IX, PSC_Cross.IZ));
                }
                if (PSC_Mid_Span != null)
                {
                    list.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Long_Girder_Members_Array[0, 0].MemberNo,
                        Long_Girder_Members_Array[_Rows - 1, _Columns - 2].MemberNo,
                        PSC_Mid_Span.AX_Sq_M, PSC_Mid_Span.IX, PSC_Mid_Span.IZ));
                }
                if ((PSC_Cross == null) && (PSC_Mid_Span == null) && (PSC_End == null))
                    list.Add(string.Format("{0} TO {1}  PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));
            }
            else if (Steel_Section != null)
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
            if (is_PSC_I_Gider)
            {
                if (PSC_Cross != null)
                {
                    list.Add(string.Format("{0} TO {1} {2} TO {3} PRIS AX {4:f4} IX {5:f4} IZ {6:f4}",
                        Cross_Girder_Members_Array[0, 0].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, 0].MemberNo,
                        Cross_Girder_Members_Array[0, _Columns - 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 1].MemberNo,
                        PSC_End.AX_Sq_M, PSC_End.IX, PSC_End.IZ));
                }
                if (PSC_Cross != null)
                {
                    list.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Cross_Girder_Members_Array[0, 1].MemberNo,
                        Cross_Girder_Members_Array[_Rows - 2, _Columns - 2].MemberNo,
                        PSC_Cross.AX_Sq_M, PSC_Cross.IX, PSC_Cross.IZ));
                }
                if (PSC_Mid_Span != null)
                {
                    list.Add(string.Format("{0} TO {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                        Long_Girder_Members_Array[0, 0].MemberNo,
                        Long_Girder_Members_Array[_Rows - 1, _Columns - 2].MemberNo,
                        PSC_Mid_Span.AX_Sq_M, PSC_Mid_Span.IX, PSC_Mid_Span.IZ));
                }
                if ((PSC_Cross == null) && (PSC_Mid_Span == null) && (PSC_End == null))
                    list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));
            }
            else if (Steel_Section != null)
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
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                Cross_Girders_as_String,
                Steel_Section.Section_Cross_Girder.Area_in_Sq_m,
                Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Cross_Girder.Iyy_in_Sq_Sq_m,
                Steel_Section.Section_Cross_Girder.Izz_in_Sq_Sq_m));

            Steel_Section.Calculate_Composite_Section();

            double Ax = 0.0;



            double ixx = 0.0;
            double iyy = 0.0;
            double izz = 0.0;

            //List<string> l = Steel_Section.Section_Long_Girder_at_Mid_Span.Get_Composite_Section(ref Ax, ref ixx, ref iyy);
            //izz = ixx + iyy;


            if (Steel_Section.Section_Long_Girder_at_Mid_Span.Nb == 1)
            {
                Ax = Steel_Section.Section_Long_Girder_at_Mid_Span.AX_Comp;
                ixx = Steel_Section.Section_Long_Girder_at_Mid_Span.IX_Comp;
                iyy = Steel_Section.Section_Long_Girder_at_Mid_Span.IY_Comp;
            }
            else
            {
                Ax = Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m;
                ixx = Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m;
                iyy = Steel_Section.Section_Long_Girder_at_Mid_Span.Iyy_in_Sq_Sq_m;
            }




            izz = ixx + iyy;

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                L2_Girders_as_String,
                Ax, ixx, iyy, izz));




            if (Steel_Section.Section_Long_Girder_at_L4_Span.Nb == 1)
            {
                Ax = Steel_Section.Section_Long_Girder_at_L4_Span.AX_Comp;
                ixx = Steel_Section.Section_Long_Girder_at_L4_Span.IX_Comp;
                iyy = Steel_Section.Section_Long_Girder_at_L4_Span.IY_Comp;
            }
            else
            {
                Ax = Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m;
                ixx = Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m;
                iyy = Steel_Section.Section_Long_Girder_at_L4_Span.Iyy_in_Sq_Sq_m;
            }


            izz = ixx + iyy;


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                            L4_Girders_as_String,
                            Ax, ixx, iyy, izz));


           



            if (Steel_Section.Section_Long_Girder_at_End_Span.Nb == 1)
            {
                Ax = Steel_Section.Section_Long_Girder_at_End_Span.AX_Comp;
                ixx = Steel_Section.Section_Long_Girder_at_End_Span.IX_Comp;
                iyy = Steel_Section.Section_Long_Girder_at_End_Span.IY_Comp;
            }
            else
            {
                Ax = Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m;
                ixx = Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m;
                iyy = Steel_Section.Section_Long_Girder_at_End_Span.Iyy_in_Sq_Sq_m;
            }
            izz = ixx + iyy;


            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
                           Deff_Girders_as_String,
                             Ax, ixx, iyy, izz));


            if (HA_Loading_Members != "")
            {

                list.Add(string.Format("{0}  PRIS YD {1:f4} ZD 1.0", HA_Loading_Members, Ds));


            }

            #region Chiranjit [2013 07 02]
            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
            //    L2_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Iyy_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_m));

            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
            //                L4_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Iyy_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m));


            //list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IY {3:f4} IZ {4:f4}",
            //               Deff_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Iyy_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m));

            #endregion Chiranjit [2013 07 02]






            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //    Cross_Girders_as_String,
            //    Steel_Section.Section_Cross_Girder.Area_in_Sq_m,
            //    Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_m * 1000,
            //    Steel_Section.Section_Cross_Girder.Izz_in_Sq_Sq_m * 1000));


            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //    L2_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m * 1000,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_m * 1000));


            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //                L4_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m * 1000,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m * 1000));


            //list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //               Deff_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m * 1000,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m * 1000));




            #region Chiranjit [2013 05 27]

            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //    Cross_Girders_as_String,
            //    Steel_Section.Section_Cross_Girder.Area_in_Sq_m,
            //    Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Cross_Girder.Izz_in_Sq_Sq_m));


            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //    L2_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_m));


            //list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //                L4_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m));


            //list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
            //               Deff_Girders_as_String,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m,
            //    Steel_Section.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m));
            #endregion Chiranjit [2013 05 27]
        }
        private void Write_PSC_Short_Section_Properties(List<string> list)
        {
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                Cross_Girders_as_String,
                PSC_Cross.AX_Sq_M,
                PSC_Cross.IX,
                PSC_Cross.IZ));


            list.Add(string.Format("{0} {1} PRIS AX {2:f4} IX {3:f4} IZ {4:f4}",
                L2_Girders_as_String,
                           Deff_Girders_as_String,
                PSC_Mid_Span.AX_Sq_M,
                PSC_Mid_Span.IX,
                PSC_Mid_Span.IZ));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                            L4_Girders_as_String,
                PSC_End.AX_Sq_M,
                PSC_End.IX,
                PSC_End.IZ));
        }

        private void Write_Composite_Section_Properties_2013_05_06(List<string> list)
        {
            Set_Inner_Outer_Cross_Girders();
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                Cross_Girders_as_String,
                Steel_Section.Section_Cross_Girder.Area_in_Sq_m,
                Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Cross_Girder.Izz_in_Sq_Sq_m));

            //Mid Span
            int va1 = 115, va2 = 116;
            int va3 = 115, va4 = 116;
            for (int j = 0; j < 11; j++)
            {
                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                    Outer_Girders_as_String,
                    Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m,
                    Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m,
                    Steel_Section.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_m));
                va1 += 10;
                va2 += 10;
            }
            //L/4 Span
            va1 = 113;
            va2 = 114;
            va3 = 117;
            va4 = 118;
            for (int j = 0; j < 11; j++)
            {
                list.Add(string.Format("{0}  {1}  {2} {3} PRIS AX {4:f4} IX {5:f4} IZ {6:f4}",
                    va1, va2, va3, va4,
                    Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
                    Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m,
                    Steel_Section.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m));
                va1 += 10;
                va2 += 10;
                va3 += 10;
                va4 += 10;
            }
            //End Span
            va1 = 111;
            va2 = 112;
            va3 = 119;
            va4 = 120;
            for (int j = 0; j < 11; j++)
            {
                list.Add(string.Format("{0}  {1}  {2} {3} PRIS AX {4:f4} IX {5:f4} IZ {6:f4}",
                    va1, va2, va3, va4,
                    Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
                    Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m,
                    Steel_Section.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m));
                va1 += 10;
                va2 += 10;
                va3 += 10;
                va4 += 10;
            }
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
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
                return DeadLoadAnalysis_Input_File;
            else if (p == 1)
                return TotalAnalysis_Input_File;
            else if (p == 2)
                return LiveLoadAnalysis_Input_File;
            else if (p > 2)
            {
                return Get_Live_Load_Analysis_Input_File(p - 2);
            }

            return "";

        }
    }
    public class Composite_Girder_LS
    {
        public string rep_file_name = "";
        public string rep_file_name_inner = "";
        public string rep_file_name_outer = "";
        public string user_input_file = "";
        public string user_path = "";
        public string file_path = "";
        public string system_path = "";
        public string drawing_path = "";
        public bool is_process = false;
        IApplication iApp = null;

        public TableRolledSteelAngles tbl_rolledSteelAngles = null;


        //Chiranjit [2012 12 17]
        public CompositeSection Steel_Section { get; set; }


        public NodeResultData Max_Node_Displacement { get; set; }
        public string Node_Displacement_Data_DL { get; set; }
        public string Node_Displacement_Data_LL { get; set; }




        public double S, B1, B2, B, fck, fy, m, YS, D, L, Dwc, gamma_c, gamma_wc;
        public double WL, v, u, IF, CF, Q, j, sigma_st, sigma_b, tau, sigma_tf, K, sigma_p;
        public double dw, tw, bf1, tf1, ang_thk, off;
        public double bf2, tf2;


        bool isPlateArrangement = false;

        public double L_2_nw, L_2_dw, L_2_tw, L_2_nf, L_2_bf1, L_2_tf1, L_2_bf2, L_2_tf2, L_2_ang_thk, L_2_nos_ang;
        public double L_4_nw, L_4_dw, L_4_tw, L_4_nf, L_4_bf1, L_4_tf1, L_4_bf2, L_4_tf2, L_4_ang_thk, L_4_nos_ang;
        public double Deff_nw, Deff_dw, Deff_tw, Deff_nf, Deff_bf1, Deff_tf1, Deff_bf2, Deff_tf2, Deff_ang_thk, Deff_nos_ang;






        public double L_2_Moment, Deff_Moment, L_4_Moment;
        public double L_2_Shear, Deff_Shear, L_4_Shear;
        public double deff = 1.5;


        public double des_moment, des_shear;
        public int nw, nf, na;
        public string ang = "";
        public string L_2_ang = "";
        public string L_4_ang = "";
        public string Deff_ang = "";

        public string L_2_ang_name = "";
        public string L_4_ang_name = "";
        public string Deff_ang_name = "";


        string _A, _B, _C, _G, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;
        string _v, _u, _1, _2, _3, _4, _6, _7, _8, _10;


        public double inner_L2_moment = 0.0;
        public double outer_L2_moment = 0.0;

        public double inner_deff_shear = 0.0;
        public double outer_deff_shear = 0.0;
        public bool flg = false;

        public List<string> DesignSummery { get; set; }
        public List<string> DesignResult { get; set; }

        public bool IsPlateArrangement
        {
            get
            {
                return isPlateArrangement;
            }
            set
            {
                isPlateArrangement = value;
            }
        }
        public bool IsBoxArrangement
        {
            get
            {
                return !isPlateArrangement;
            }
            set
            {
                isPlateArrangement = !value;
            }
        }
        public Composite_Girder_LS(IApplication app)
        {
            iApp = app;
            _A = "";
            _B = "";
            _C = "";
            _D = "";
            _E = "";
            _F = "";
            _bd1 = "";
            _sp1 = "";
            _bd2 = "";
            _sp2 = "";


            DesignResult = new List<string>();
            DesignSummery = new List<string>();
            IsInnerGirder = true;
        }
        #region User Method



        //Chiranjit [2013 06 25]
        public int NumberOfGirder { get; set; }
        public string GirderType { get; set; }


        public void Calculate_Program()
        {
            string ref_string = "";
            frmCurve f_c = null;
            string ang_name = "";

            rep_file_name = IsInnerGirder ? rep_file_name_inner : rep_file_name_outer;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                DesignResult.Clear();
                DesignSummery.Clear();
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*         DESIGN OF COMPOSITE BRIDGE         *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("-----------------------------------------------------------------------------------------------");
                sw.WriteLine("                       MAIN LONG GIRDERS ARE DESIGNED AS STEEL {0} GIRDER", IsBoxArrangement ? "BOX" : "PLATE");
                sw.WriteLine("-----------------------------------------------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA FOR STEEL GIRDER");
                sw.WriteLine("------------------------------------------------------------");
              
                sw.WriteLine();
                sw.WriteLine("Bridge Span [S] = {0} m", S);
                sw.WriteLine("Bridge Deck Width [B1] = {0} m    Marked as (A) in the Drawing", B1);
                _A = B1 + " m.";

                sw.WriteLine("Footpath Width [B2] = {0} m", B2);
                sw.WriteLine("Spacing on either side of Main Girders [B] = {0} m     Marked as (B) in the Drawing", B);
                _B = B + " m.";


                sw.WriteLine();
                sw.WriteLine("Concrete Grade [fck] = M {0:f0} = {0:f0} N/sq.mm", fck);
                sw.WriteLine("Reinforcement Steel Frade [fy] = Fe {0:f0} = {0:f0} N/sq.mm", fy);
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Rolled Steel Section of Yield Stress [YS] = {0} N/sq.mm", YS);
                sw.WriteLine("SLAB Thickness [D] = {0} mm     Marked as (C) in the Drawing", D);
                _C = D / 1000.0 + " m.";



                sw.WriteLine("Panel Length [L] = {0} m        Marked as (D) in the Drawing", L);
                _D = L + " m.";


                sw.WriteLine("Thickness of wearing course [Dwc] = {0} mm     Marked as (G) in the Drawing", Dwc);
                _G = Dwc / 1000.0 + " m.";


                sw.WriteLine();
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine("Unit Weight of wearing cource [γ_wc] = {0} kN/cu.m", gamma_wc);
                sw.WriteLine("Tracked Vehicle Load [WL] = {0} kN", WL);
                sw.WriteLine("Length of Loaded area [v] = {0} m     Marked as (E) in the Drawing", v);
                _E = v + " m.";


                sw.WriteLine("Width of Loaded area [u] = {0} m      Marked as (F) in the Drawing", u);
                _F = u + " m.";

                sw.WriteLine();
                sw.WriteLine("Impact Factor [IF] = {0}", IF);
                sw.WriteLine("Continuity Factor [CF] = {0}", CF);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("[σ_st] = {0} N/sq.mm", sigma_st);
                sw.WriteLine("Permissible Bending Stress in Steel [σ_b] = {0} N/sq.mm", sigma_b);
                sw.WriteLine("Permissible Shear Stress in Steel [τ] = {0} N/sq.mm", tau);
                sw.WriteLine("Permissible Shear Stress through fillet Weld [σ_tf] = {0} N/sq.mm", sigma_tf);
                sw.WriteLine("Constant ‘K’ = {0}", K);
                sw.WriteLine("Permissible Bearing Stress [σ_p] = {0} N/sq.mm", sigma_p);
                sw.WriteLine();
                sw.WriteLine();
                //==================================================

                //sw.WriteLine("Flange Plates : nf =4, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");
                //sw.WriteLine("==================================================");
                //sw.WriteLine("For Quarter Span Section (L/4), ");
                //sw.WriteLine("Starting from Deff  to L/4 ");
                //sw.WriteLine("And  Deff  L-L/4  to  L-Deff, ");
                //sw.WriteLine("User given Sections are as follows:");

                //sw.WriteLine("Web Plates : nw =1, Dw=1600 mm, tw=20 mm");
                //sw.WriteLine("Flange Plates : nf =2, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");
                //sw.WriteLine("==================================================");
                //sw.WriteLine("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder, ");
                //sw.WriteLine("Starting from Deff  to L/4 ");
                //sw.WriteLine("And  Deff  L-L/4  to  L-Deff, ");
                //sw.WriteLine("User given Sections are as follows:");

                //sw.WriteLine("Web Plates : nw =1, Dw=1600 mm, tw=20 mm");
                //sw.WriteLine("Flange Plates : nf =2, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");



                //Steel_Section.Section_Long_Girder_at_Mid_Span.Length = S;
                //Steel_Section.Section_Long_Girder_at_Mid_Span.NumberOfGirder = NumberOfGirder;
                Steel_Section.Section_Long_Girder_at_End_Span.AngleSection = iApp.Tables.Get_AngleData_FromTable(Deff_ang_name, Deff_ang, Deff_ang_thk);
                Steel_Section.Section_Long_Girder_at_L4_Span.AngleSection = iApp.Tables.Get_AngleData_FromTable(L_4_ang_name, L_4_ang, L_4_ang_thk);
                Steel_Section.Section_Long_Girder_at_Mid_Span.AngleSection = iApp.Tables.Get_AngleData_FromTable(L_2_ang_name, L_2_ang, L_2_ang_thk);
                
                Steel_Section.Get_Weight_Calculation_Report(sw);

                    //RolledSteelAnglesRow tab_data = iApp.Tables.Get_AngleData_FromTable(ang_name, ang, ang_thk);

                #endregion


                int step = 1;
                string step_text = "";
                double u_by_B = 0.0;

                #region Report


                Steel_Section.Calculate_Composite_Section();//Chiranjit [2013 07 04]
                do
                {
                    if (step == 1)
                    {
                        //dw = L_2_dw;
                        //nw = (int)L_2_nw;
                        //tw = L_2_tw;

                        dw = Steel_Section.Section_Long_Girder_at_Mid_Span.Dw;
                        nw = Steel_Section.Section_Long_Girder_at_Mid_Span.Nb;
                        tw = Steel_Section.Section_Long_Girder_at_Mid_Span.Bw;

                        bf1 = Steel_Section.Section_Long_Girder_at_Mid_Span.Bft;
                        tf1 = Steel_Section.Section_Long_Girder_at_Mid_Span.Dft;
                        bf2 = Steel_Section.Section_Long_Girder_at_Mid_Span.Bfb;
                        tf2 = Steel_Section.Section_Long_Girder_at_Mid_Span.Dfb;
                        //bf1 = Steel_Section.Section_Long_Girder_at_Mid_Span.Bt;
                        //tf1 = Steel_Section.Section_Long_Girder_at_Mid_Span.Dt;
                        //bf2 = Steel_Section.Section_Long_Girder_at_Mid_Span.Bb;
                        //tf2 = Steel_Section.Section_Long_Girder_at_Mid_Span.Db;
                        nf = (int)L_2_nf;



                        des_moment = L_2_Moment;
                        des_shear = L_2_Shear;

                        na = (int)L_2_nos_ang;
                        ang = L_2_ang;
                        ang_name = L_2_ang_name;
                        ang_thk = L_2_ang_thk;
                        step_text = "MID SPAN (L/2)";
                    }
                    else if (step == 2)
                    {


                        dw = Steel_Section.Section_Long_Girder_at_L4_Span.Dw;
                        nw = Steel_Section.Section_Long_Girder_at_L4_Span.Nb;
                        tw = Steel_Section.Section_Long_Girder_at_L4_Span.Bw;

                        bf1 = Steel_Section.Section_Long_Girder_at_L4_Span.Bft;
                        tf1 = Steel_Section.Section_Long_Girder_at_L4_Span.Dft;
                        bf2 = Steel_Section.Section_Long_Girder_at_L4_Span.Bfb;
                        tf2 = Steel_Section.Section_Long_Girder_at_L4_Span.Dfb;
                        nf = (int)L_2_nf;


                        des_moment = L_4_Moment;
                        des_shear = L_4_Shear;

                        na = (int)L_4_nos_ang;
                        ang = L_4_ang;
                        ang_thk = L_4_ang_thk;
                        ang_name = L_4_ang_name;

                        step_text = "QUARTER SPAN (L/4)";
                    }
                    else if (step == 3)
                    {

                        dw = Steel_Section.Section_Long_Girder_at_End_Span.Dw;
                        nw = Steel_Section.Section_Long_Girder_at_End_Span.Nb;
                        tw = Steel_Section.Section_Long_Girder_at_End_Span.Bw;

                        bf1 = Steel_Section.Section_Long_Girder_at_End_Span.Bft;
                        tf1 = Steel_Section.Section_Long_Girder_at_End_Span.Dft;
                        bf2 = Steel_Section.Section_Long_Girder_at_End_Span.Bfb;
                        tf2 = Steel_Section.Section_Long_Girder_at_End_Span.Dfb;


                        des_moment = Deff_Moment;
                        des_shear = Deff_Shear;

                        na = (int)Deff_nos_ang;
                        ang_name = Deff_ang_name;
                        ang = Deff_ang;
                        ang_thk = Deff_ang_thk;

                        step_text = "BOTH END (Deff)";
                    }

                    #region STEP 1 : DESIGN OF STEEL PALTE GIRDER
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("----------------------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : DESIGN OF STEEL PLATE GIRDER for Section at {1}", step, step_text);
                    sw.WriteLine("----------------------------------------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("----------------------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.1 : Analysis forces and WEB & FLANGE section Details for Main Girders at {1}", step, step_text);
                    sw.WriteLine("----------------------------------------------------------------------------------------------");
                    sw.WriteLine();
                    double M = des_moment;
                    sw.WriteLine("Design Bending Moment = M = {0} kN-m", M);
                    double deg_sh_frc = des_shear;
                    sw.WriteLine("Design shear force = V = {0} kN", des_shear);
                    sw.WriteLine();
                    sw.WriteLine();
                    //DesignSummery.Add("==============================================================================");
                    if (step == 1)
                    {
                        DesignSummery.Add("==========================     AT MID SPAN     ===============================");
                        DesignSummery.Add(string.Format(""));
                        DesignSummery.Add(string.Format("For Mid Span Section (L/2), Starting from    (L/2–L/4) to (L/2+L/4),"));
                        DesignSummery.Add(string.Format("                                     from =  {0:f3}m  to {1:f3}m,", (S / 2.0 - S / 4.0), (S / 2.0 + S / 4.0)));
                        sw.WriteLine("For Mid Span Section (L/2), Starting from L/2 – L/4 to L/2+L/4,");



                        Steel_Section.Section_Long_Girder_at_Mid_Span.Input_Data_ToStream(sw);

                    }
                    else if (step == 2)
                    {
                        DesignSummery.Add("=====================  AT QUARTER SPAN ON BOTH SIDES  =========================");
                        DesignSummery.Add(string.Format(""));
                        DesignSummery.Add(string.Format("For Quarter Span Section (L/4),"));
                        DesignSummery.Add(string.Format("On the left side from   Deff  to   L/4  =  {0:f3}m. to  {1:f3}m. and", deff, (S / 4)));
                        DesignSummery.Add(string.Format("On the right side from L/2+L/4  to  L-Deff = from {0:f3}m. to {1:f3}m.", (3 * S / 4.0), (S - deff)));


                        //For Quarter Span Section (L/4),

                        //On the left side from   Deff  to   L/4  =  1.500m. to  11.500m. and  
                        //On the right side from L/2+L/4  to  L-Deff = from 34.500m. to 44.500m.


                        //DesignSummery.Add(string.Format("For Quarter Span Section (L/4),Starting from Deff  to L/4 And  Deff  L-L/4  to  L-Deff,"));
                        sw.WriteLine("For Quarter Span Section (L/4),Starting from Deff  to L/4 And  Deff  L-L/4  to  L-Deff,");
                        Steel_Section.Section_Long_Girder_at_L4_Span.Input_Data_ToStream(sw);
                    }
                    else if (step == 3)
                    {
                        DesignSummery.Add("=================     AT END SECTIONS ON BOTH SIDES   =========================");
                        DesignSummery.Add("");
                        DesignSummery.Add(string.Format("For Span Section (Deff)"));
                        DesignSummery.Add(string.Format("from either End up to distance equals to Effective Depth of Girder,"));
                        DesignSummery.Add(string.Format("On the left side from   0  to   Deff  =  from {0:f3}m. to  {1:f3}m. and ", 0, deff));
                        DesignSummery.Add(string.Format("On the right side from L - Deff  to  L = from {0:f3}m. to  {1:f3}m.", (S - deff), S));


                        //For Span Section (Deff) 

                        //from either End up to distance equals to Effective Depth of Girder,
                        //On the left side from   0  to   Deff  =  from 0.000m. to  1.500m. and  
                        //On the right side from L - Deff  to  L = from 44.500m. to  46.000m.




                        //DesignSummery.Add(string.Format("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder,"));
                        //DesignSummery.Add(string.Format("Starting from Deff  to L/4 and  Deff  L-L/4  to  L-Deff, "));
                        sw.WriteLine("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder,");
                        sw.WriteLine("Starting from Deff  to L/4 and  Deff  L-L/4  to  L-Deff, ");
                        Steel_Section.Section_Long_Girder_at_End_Span.Input_Data_ToStream(sw);
                    }
                    sw.WriteLine();
                    DesignSummery.Add("");

                    #region Chiranjit [2012 12 17
                    //DesignSummery.Add(string.Format("Web Plates : Number of Plates [nw] = {0}, Depth [dw] = {1} mm, Thickness [tw] = {2} mm", nw, dw, tw));
                    //DesignSummery.Add("");

                    //sw.WriteLine("Web Plates : Number of Plates [nw] = {0}, Depth [dw] = {1} mm, Thickness [tw] = {2} mm", nw, dw, tw);
                    //sw.WriteLine();
                    //DesignSummery.Add(string.Format("Flange Plates : Number of Plates [nf] = {0}, Breadth [bf] = {1} mm, Thickness [tf] = {2} mm", nf, bf1, tf1));
                    //DesignSummery.Add("");
                    ////sw.WriteLine("Top Flange Plates : Number of Plates [nf] = {0}, Breadth [bf] = {1} mm, Thickness [tf] = {2} mm", nf, bf1, tf1);
                    //sw.WriteLine("Top Flange Plates    :    Breadth [bf1] = {0} mm,   Thickness [tf1] = {1} mm", bf1, tf1);
                    //sw.WriteLine("Bottom Flange Plates :    Breadth [bf2] = {0} mm,   Thickness [tf2] = {1} mm", bf2, tf2);
                    #endregion Chiranjit [2012 12 17


                    //RolledSteelAnglesRow tab_data = tbl_rolledSteelAngles.GetDataFromTable("ISA", ang, ang_thk);
                    RolledSteelAnglesRow tab_data = iApp.Tables.Get_AngleData_FromTable(ang_name, ang, ang_thk);


                    //sw.WriteLine("Angles : Number of Angles = {0}, {1} x {2}", na, ang, ang_thk);
                    sw.WriteLine();
                    DesignSummery.Add("");
                    DesignSummery.Add(string.Format("Angles : Number of Angles [na] = {0}, Size: {1} {2}X{3}", na, tab_data.SectionName, tab_data.SectionSize, ang_thk));
                    sw.WriteLine("Angles : Number of Angles [na] = {0}, Size: {1} {2}X{3}", na, tab_data.SectionName, tab_data.SectionSize, ang_thk);
                    DesignSummery.Add("");
                    DesignSummery.Add("==============================================================================");
                    DesignSummery.Add("");
                    DesignSummery.Add("");
                    sw.WriteLine();
                    sw.WriteLine();

                    //Chiranjit [2013 07 02] Add Composite Section Section Properties 

                    #region Chiranjit [2013 07 02]
                    sw.WriteLine("------------------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.2 (A) : Calculations of Section Properties for Main Long Girders at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------------------------------------");
                    sw.WriteLine();

                    //sw.WriteLine("Approximate depth of Girder = S /10 = {0} / 10 = {1} m", S, (S / 10.0));
                    double dval = (S / 2) + 2;
                    double flan_over_hang = 0;
                    sw.WriteLine("Approximate Web depth of Girder  = S /{0} = {1}/ {0} = {2:f3} m", dval, S, (S / dval));
                    sw.WriteLine();

                    double eco_depth_girder = (M * 10E5) / sigma_b;

                    eco_depth_girder = Math.Pow(eco_depth_girder, (1.0 / 3.0));
                    eco_depth_girder = 5.0 * eco_depth_girder;


                    sw.WriteLine("Economical Web depth of Girder = 5 * (M / σb)^(1/3)");
                    sw.WriteLine("                               = 5 * ({0} * 10^6 / {1})^(1/3)", M, sigma_b);
                    eco_depth_girder = Double.Parse(eco_depth_girder.ToString("0"));
                    sw.WriteLine("                               = {0:f3} mm", eco_depth_girder);
                    sw.WriteLine();
                    sw.WriteLine("Thickness of web plate for shear considerations");
                    sw.WriteLine("Web depth by asuming thickness of plate = tw = {0} mm thick plate for shear considerations", tw);
                    //sw.WriteLine("asuming thickness of plate = tw");
                    //sw.WriteLine();
                    //sw.WriteLine("Web depth by {0} mm thick plate for shear considerations", tw);
                    sw.WriteLine();
                    double web_depth = (deg_sh_frc * 1000) / (tau * tw);
                    sw.WriteLine("    = V / (τ * {0})", tw);
                    sw.WriteLine("    = {0} * 1000 / ({1}*{2})", deg_sh_frc, tau, tw);
                    web_depth = Double.Parse(web_depth.ToString("0.00"));
                    sw.WriteLine("    = {0} mm", web_depth);
                    sw.WriteLine();
                    sw.WriteLine("Let us provide Web Depth = Dw = {0}mm  (user given value)", dw);
                    sw.WriteLine();
                    //
                    //double dw, tw;
                    //dw = 1000.0;
                    //tw = 10.0;
                    double Aw = nw * dw * tw;

                    dval = 16 * tw;
                    sw.WriteLine("Permissible overhang for flange =  16 * tw");
                    sw.WriteLine("                                =  16 * {0}", tw);
                    sw.WriteLine("                                =  {0} mm", dval);
                    sw.WriteLine();

                    flan_over_hang = (bf1 - tw) / 2.0;
                    sw.WriteLine("In present case the flange overhang  = (bf - tw) / 2.0");
                    sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf1, tw);
                    //sw.WriteLine("                                     = {0} mm", flan_over_hang);
                    IsBoxArrangement = flan_over_hang > dval;
                    if (IsBoxArrangement)
                    {
                        //sw.WriteLine("In present case the flange overhang  = (bw - tf) / 2.0");
                        //sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf, tw);
                        sw.WriteLine("                                     = {0:G3}mm > {1:G3}mm", flan_over_hang, dval);
                    }
                    else
                    {
                        //sw.WriteLine("In present case the flange overhang  = (bw - tf) / 2.0");
                        //sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf, tw);
                        sw.WriteLine("                                     = {0:G3}mm < {1:G3}mm", flan_over_hang, dval);
                        //sw.WriteLine("                                     = {0} mm", flan_over_hang);


                        sw.WriteLine();
                        //sw.WriteLine("Number of Flange Please will be equaly divided on Upper and Lower Side of the Box Girder");
                        //sw.WriteLine("It is suggested to go for Steel Box Girder with 2 webs suitable plates");
                        //sw.WriteLine();
                    }

                    sw.WriteLine();
                    sw.WriteLine("");

                    int sides = IsBoxArrangement ? 2 : 1;
                    if (IsBoxArrangement)
                    {
                        sw.WriteLine("So, a Box Girder arrangement is considered, ");
                        sw.WriteLine("");
                        sw.WriteLine("For Box Girder, sides = 2, and , ");
                        sw.WriteLine("");
                    }
                    else
                    {
                        sw.WriteLine("So, a Plate Girder arrangement is considered, ");
                        sw.WriteLine("");
                        sw.WriteLine("For Plate Girder, sides = 1, and , ");
                        sw.WriteLine();
                    }
                    sw.WriteLine("Let us provide the Web depth = {0}mm, thickness = {1}mm on either side,", dw, tw);
                    sw.WriteLine();
                    sw.WriteLine("Approximate Total Flange area required = Af = (M / (σ_b  x  dw)) - (dw  x  tw / 6)");

                    double Af = (M * 1000000 / (sigma_b * dw)) - (dw * tw / 6.0);


                    sw.WriteLine("                                            = ({0:E3} / ({1}  x  {2})) - ({2}  x  {3} / 6)",
                        (M * 1000000), sigma_b, dw, tw);
                    sw.WriteLine();
                    sw.WriteLine("                                            = {0:G3}  sq.mm", Af);
                    sw.WriteLine();

                     

                    double Bf = 0.0;
                    if ((S * 1000 / 40) > 500 && (S * 1000 / 45) <= 1500)
                    {
                        Bf = 1000.0;
                    }
                    else if ((S * 1000 / 40) > 0 && (S * 1000 / 45) <= 500)
                    {
                        Bf = 500;
                    }
                    else if ((S * 1000 / 40) > 1500 && (S * 1000 / 45) <= 2000)
                    {
                        Bf = 1500;
                    }

                    sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                
                    sw.WriteLine("                  = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);
                    sw.WriteLine("                  = {0:f0} to {1:f0}", (S * 1000 / 40), (S * 1000 / 45));
                    sw.WriteLine("                  = {0:f0} mm (Say)", Bf);
                    sw.WriteLine();
                     sw.WriteLine("Let us provide {0} Flange plates having Flange width={1} mm, thickness= {2} mm on the Bottom,", nf, bf1, tf1);
                    sw.WriteLine();
                    sw.WriteLine();
                     sw.WriteLine();
                    
                    _3 = string.Format("Size of Web Plate = {0} * {1} * {2} sq.mm", nw, dw, tw);


                    double i1 = sides * (nw * (tw * dw * dw * dw) / 12.0);


                    double i2 = (1 / 12.0) * (bf1 * Math.Pow((dw + (nf * tf1)), 3.0) - bf1 * dw * dw * dw);
                    double i4 = 0.0;
                    double i3 = ((tab_data.Ixx * 10000) + (tab_data.Area * 100) * (dw - (tab_data.Cxx * tab_data.Cxx * 100)));
                    i4 = sides * (na * (tab_data.Ixx * 10000 + tab_data.Area * 100 * ((dw / 2.0 - tab_data.Cxx * 10) * (dw / 2.0 - tab_data.Cxx * 10))));

                    double I = 0.0;


                    double Ax = 0;
                    double ixx = 0;
                    double iyy = 0;

                    List<string> list = null;

                    if (step == 1)
                    {
                        Steel_Section.Section_Long_Girder_at_Mid_Span.Area_Result_ToStream(sw);
                        Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_Result_ToStream(sw);

                        list = Steel_Section.Section_Long_Girder_at_Mid_Span.Composite_Results;

                        I = Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx;
                    }
                    else if (step == 2)
                    {
                        Steel_Section.Section_Long_Girder_at_L4_Span.Area_Result_ToStream(sw);
                        Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_Result_ToStream(sw);

                        list = Steel_Section.Section_Long_Girder_at_L4_Span.Composite_Results;
                        
                        I = Steel_Section.Section_Long_Girder_at_L4_Span.Ixx;
                    }

                    else if (step == 3)
                    {
                        Steel_Section.Section_Long_Girder_at_End_Span.Area_Result_ToStream(sw);
                        Steel_Section.Section_Long_Girder_at_End_Span.Ixx_Result_ToStream(sw);
                        list = Steel_Section.Section_Long_Girder_at_End_Span.Composite_Results;
                        I = Steel_Section.Section_Long_Girder_at_End_Span.Ixx;
                    }


                     
                    sw.WriteLine("Moment of Inertia of Four Connecting Angles ({0}x{1}x{2})", na, ang, ang_thk);
                    sw.WriteLine();
                    sw.WriteLine("i4 = sides * ( na * (Ixx + a * (dw / 2.0 - Cxx)^2)))");
                    sw.WriteLine("i4 = {0} * ({1} * ({2}*10^4 + {3} * ({4} / 2.0 - {5})^2)))", sides, na, tab_data.Ixx, tab_data.Area * 100, dw, tab_data.Cxx * 10);
                    sw.WriteLine();
                    sw.WriteLine("   = {0:e3} sq.sq.mm", i4);
                    sw.WriteLine();
                    sw.WriteLine("Moment of Inertia = I");
                    sw.WriteLine();
                    sw.WriteLine("I = MI of Section + MI of Four Connecting Angles ({0}x{1}x{2})", na, ang, ang_thk);
                    sw.WriteLine("  = {0:E3} + {1:E3}", I, i4);
                    I = I + i4;
                    sw.WriteLine("  = {0:e3} sq.sq.mm", I);
                    sw.WriteLine();

                    sw.WriteLine("");
                    sw.WriteLine();
 


                       sw.WriteLine();
                    sw.WriteLine("Note : Angles are to be provided at Top and Bottom on either side of each Web Plate ");
                    sw.WriteLine();


                    
                    sw.WriteLine();


                    sw.WriteLine();
                    sw.WriteLine("  = {0:e3} sq.sq.mm", I);

                    double y = dw / 2.0 + tf1;

                    sw.WriteLine();
                    sw.WriteLine();

                    
                    sw.WriteLine();

                   
                    Bf = bf1;
                    sw.WriteLine("So, Provided Size of Flange Plate = {0} x {1} x {2} x {3}     Marked as (4) in the Drawing ", sides, nf, Bf, tf1);
                     _4 = string.Format("Flange Size = {0} * {1} * {2} * {3} ", sides, nf, bf1, tf1);



                    #endregion Chiranjit [2013 07 02]


                     #region Chiranjit [2013 07 02] Add Composite Section Section Properties 
                    
                     sw.WriteLine("");
                     sw.WriteLine("------------------------------------------------------------------------------------------");
                     sw.WriteLine("STEP {0}.2 (B) : Calculations of Section Properties for ", step);
                     sw.WriteLine("               Composite Main Long Girders with RCC Deck Slab at {0}", step_text);
                     sw.WriteLine("------------------------------------------------------------------------------------------");



                     foreach (var item in list)
                     {
                         sw.WriteLine(item);

                     }


                     #endregion Chiranjit [2013 07 02]

                     sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.3 : Check for Maximum Stresses at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();
                   
                    y = (dw / 2.0 + (nf * tf2) / 2.0);
                    sw.WriteLine("  and   y = dw / 2 + tf2 / 2");
                    sw.WriteLine("          = {0} / 2 + {1} / 2", dw, tf2);
                    sw.WriteLine("          = {0:f3} mm", y);
                    sw.WriteLine();

                    double appl_stress = (M * 1000000 * y) / I;
                    //appl_stress = Double.Parse(appl_stress.ToString("0"));
                    sw.WriteLine("Applied Stress = M * y  / I");
                    sw.WriteLine("               = {0:G3} * 10^6 * {1:G3}  / {2:G3}", (M), y, I);
                    sw.WriteLine();

                    
                    sw.WriteLine();


                    
                    if (appl_stress < sigma_b)
                    {
                        sw.WriteLine("               = {0:G5} N/sq.mm < σ_b = {1:f5} N/sq.mm, OK", appl_stress, sigma_b);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.3 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("Applied Stress  = {0:f5} N/sq.mm > σ_b = {1:f5} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b));
                        sw.WriteLine("               = {0:f5} N/sq.mm > σ_b = {1:f5} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b);
                    }
                    sw.WriteLine();

                    u_by_B = deg_sh_frc;
                    double V = u_by_B;
                    deg_sh_frc = des_shear;
                    //v = u_by_B;

                    double tau1 = V * 1000 / (dw * tw);
                    //tau1 = double.Parse(tau1.ToString("0"));
                    sw.WriteLine("Average Shear Stress = τ1");
                    sw.WriteLine("                     = V * 1000 / (dw * tw)");
                    sw.WriteLine("                     = {0} * 1000 / ({1} * {2})", V, dw, tw);
                    sw.WriteLine("                     = {0} N/sq.mm", tau1);
                    sw.WriteLine();

                    double ratio = (dw / tw);
                    sw.WriteLine("Ratio dw / tw = {0} / {1} = {2}", dw, tw, ratio);
                    sw.WriteLine();
                    sw.WriteLine("Considering Stiffener Spacing = c = dw = {0} mm", dw);
                    sw.WriteLine();

                    // Calculate from Table 1
                    // **Problem How to calculate value from Table1 ?
                    double tau2 = Get_Table_1_Value(100, 1, ref ref_string);
                    //double tau2 = 87;
                    sw.WriteLine("From Table 1 (Given at the end of the Report) : {0} ", ref_string);
                    //sw.WriteLine("Allowable average Shear Stress = {0} N/Sq mm = t2", tau2);
                    sw.WriteLine();

                    if (tau2 > tau1)
                    {
                        sw.WriteLine("Allowable average Shear Stress = {0:f4} N/Sq. mm > {1:f4} N/Sq mm,    OK", tau2, tau1);
                        sw.WriteLine();
                        sw.WriteLine("So, Average shear stress is within Safe permissible Limits.", tau1, tau2);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.3 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("Allowable average Shear Stress = {0:f4} N/Sq. mm < {1:f4} N/Sq mm,   NOT OK", tau2, tau1));
                        sw.WriteLine("Allowable average Shear Stress = {0:f4} N/Sq. mm < {1:f4} N/Sq mm,   NOT OK", tau2, tau1);
                        sw.WriteLine();
                        sw.WriteLine("So, Average shear stress is not within Safe permissible limits.", tau1, tau2);
                    }
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.4 : Connection Between Flange and Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear Force at the junction of Flange and Web is given by");
                    sw.WriteLine();

                    sw.WriteLine("    τ  = V * a * y  / I");
                    double a = bf2 * tf2;
                    Bf = bf2;
                    y = dw / 2.0 + (tf2 / 2.0);
                    //I = double.Parse(I.ToString("0"));

                    sw.WriteLine("    a = bf2 * tf2 = {0} * {1} = {2:f2} sq.mm", bf2, tf2, a);
                    sw.WriteLine();
                    sw.WriteLine("    y = dw/2 + tf2/2 = {0}/2 + {1}/2 = {2} mm", dw, tf2, y);
                    sw.WriteLine();
                    sw.WriteLine("    I = {0:G5} sq.sq.mm", I);
                    sw.WriteLine();
                    sw.WriteLine("    V = {0} * 1000 N", V);
                    sw.WriteLine();

                    //tau = (V * 1000 * a * y) / (I * 10E6);
                    tau = (V * 1000 * a * y) / (I);
                    //tau = double.Parse(tau.ToString("0"));
                    //sw.WriteLine("τ = 548 * 1000 * 15000 * 515 / (879 * 107) = 483 N/mm");
                    sw.WriteLine("    τ  = {0} * 1000 * {1} * {2}  / ({3:G5})", V, a, y, I);
                    sw.WriteLine("       = {0:f5} N/mm", tau);
                    sw.WriteLine();

                    sw.WriteLine("Adopting Continuous weld on either side, strength of weld of size ");
                    sw.WriteLine();
                    sw.WriteLine("  ‘S’ = 2 * k * S * σ_tf");

                    double _S = 2 * K * sigma_tf;
                    _S = double.Parse(_S.ToString("f5"));
                    sw.WriteLine("      = 2 * {0} * S * {1}", K, sigma_tf);
                    sw.WriteLine("      = {0:G5} * S", _S);
                    sw.WriteLine();


                    sw.WriteLine("Equating, {0} * S = {1:f5},                S = {1:f5} / {0} = {2:G5} mm", _S, tau, (tau / _S));
                    sw.WriteLine();

                    _S = tau / +_S;

                    _S = (int)_S;
                    _S += 2;

                    sw.WriteLine("Use {0} mm Fillet Weld, continuous on either side.     Marked as (5) in the Drawing", _S);
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.5 : Intermediate Stiffeners at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    double val1, val2;

                    val1 = dw / tw;
                    if (val1 < 85)
                    {
                        sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                        sw.WriteLine("So, Vertical Stiffeners are required");
                        //sw.WriteLine("else, Vertical Stiffeners are not required.");
                    }
                    else
                    {
                        sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                        //sw.WriteLine("So, Vertical Stiffeners are required");
                        sw.WriteLine("So, Vertical Stiffeners are not required.");
                    }
                    sw.WriteLine();

                    double sp_stifn1 = 0.33 * dw;
                    double sp_stifn2 = 1.5 * dw;
                    sw.WriteLine("Spacing of Stiffeners = 0.33 * dw  to  1.5 * dw");
                    sw.WriteLine("            = 0.33 * {0}  to  1.5 * {0}", dw);
                    sw.WriteLine("            = {0} mm to {1} mm", sp_stifn1, sp_stifn2);
                    sw.WriteLine();

                    double c = 1000;

                    sw.WriteLine("Adopt Spacing = c = {0} mm", c);
                    sw.WriteLine();


                    sw.WriteLine("Required minimum Moment of Inertia of Stiffeners");
                    sw.WriteLine();


                    double _I = ((1.5 * dw * dw * dw * tw * tw * tw) / (c * c));
                    sw.WriteLine("I = 1.5 * dw**3 * tw**3 / c**2");
                    sw.WriteLine("  = 1.5 * {0}^3 * {1}^3 / {2}^2", dw, tw, c);
                    //_I = double.Parse(_I.ToString("0"));
                    sw.WriteLine("  = {0:E3} sq.sq.mm", _I);
                    sw.WriteLine();
                    _I = _I / 10E4;

                    double t = 10; // t is Constant?

                    sw.WriteLine("Use {0} mm thick plate, t = {0} mm", t);
                    sw.WriteLine();
                    sw.WriteLine("Maximum width of plate not to exceed = 12 * t = {0} mm", (12 * t));
                    sw.WriteLine();

                    // 80 ?
                    double h = 80;
                    sw.WriteLine("Use 80 mm size plate, h = 80 mm");
                    sw.WriteLine();
                    sw.WriteLine("Plate size is {0} mm * {1} mm      Marked as (6) in the Drawing", h, t);
                    _6 = string.Format("{0} mm x {1} mm", h, t);



                    sw.WriteLine();

                    double _I1 = (t * (h * h * h)) / 3.0;
                    //_I1 = _I1 / 10E4;
                    //_I1 = double.Parse(_I1.ToString("0"));

                    if (_I1 > _I)
                    {
                        sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm > {2:e2} sq.sq.mm,                OK", t, _I1, _I);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.5 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I));
                        DesignResult.Add("");
                        sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I);
                    }
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.6 : Connections of Vertical Stiffener to Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Shear on weld connecting stiffener to Web");
                    sw.WriteLine();

                    // 125 = constant ?
                    double sh_wld_wb = 125 * tw * tw / h;
                    sw.WriteLine("    = 125 * tw*tw / h");
                    sw.WriteLine("    = 125 * {0}*{0} / {1}", tw, h);

                    sh_wld_wb = double.Parse(sh_wld_wb.ToString("0.00"));
                    sw.WriteLine("    = {0} kN/m", sh_wld_wb);
                    sw.WriteLine("    = {0} N/mm", sh_wld_wb);
                    sw.WriteLine();

                    double sz_wld = sh_wld_wb / (K * sigma_tf);
                    sz_wld = double.Parse(sz_wld.ToString("0.00"));
                    sw.WriteLine("Size of welds = 156.25 / (K * σ_tf)");
                    sw.WriteLine("              = {0} / ({1} * {2})", sh_wld_wb, K, sigma_tf);
                    sw.WriteLine("              = {0} mm", sz_wld);
                    sw.WriteLine();

                    //sw.WriteLine("Size of welds = 156.25 / (K * σtf) = 156.25 / (0.7 * 102.5) = 2.17 mm");
                    // How come 100 and 5?
                    sw.WriteLine("Use 100 mm Long 5 mm Fillet Welds alternately on either side.     Marked as (7) in the Drawing");

                    //(7)  5-100-100 (weld)
                    _7 = string.Format("5-100-100 (weld)");
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.7 : End Bearing Stiffeners at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear Force = V = {0} kN", V);
                    sw.WriteLine();

                    val1 = (h / t);
                    if (val1 < 12)
                    {
                        sw.WriteLine("The end bearing Stiffeners is designed as a column h / t < 12");
                    }
                    else
                    {
                        sw.WriteLine("The end bearing Stiffeners is designed as a column h / t > 12");
                    }
                    sw.WriteLine();
                    h = 180;
                    sw.WriteLine("Use ‘h’ = outstand of stiffeners = {0} mm", h);
                    sw.WriteLine();
                    t = h / 12;
                    sw.WriteLine("t = {0} / 12 = {1} mm", h, (h / 12.0));
                    sw.WriteLine();
                    sw.WriteLine("Use plate of size 180 mm * 15 mm     Marked as (8) in the Drawing");

                    //(8)  180 x15 mm

                    _8 = string.Format("180 x 15 mm");
                    sw.WriteLine();

                    double brng_ar_req = V * 1000 / sigma_p;
                    sw.WriteLine();
                    sw.WriteLine("Bearing area required = V * 1000 / σ_p");
                    sw.WriteLine("                      = {0} * 1000 / {1} sq.mm", V, sigma_p);
                    brng_ar_req = double.Parse(brng_ar_req.ToString("0"));
                    sw.WriteLine("                      = {0} sq.mm", brng_ar_req);
                    sw.WriteLine();

                    double tot_area = 2 * h * t;
                    sw.WriteLine("If two plates are used,");
                    sw.WriteLine("     Total area = 2 * {0} * {1}", h, t);
                    if (tot_area > brng_ar_req)
                    {
                        sw.WriteLine("                = {0} sq.mm > {1} sq.mm", tot_area, brng_ar_req);
                    }
                    else
                    {
                        sw.WriteLine("                = {0} sq.mm < {1} sq.mm", tot_area, brng_ar_req);
                    }
                    sw.WriteLine();

                    sw.WriteLine("The length of Web plate which acts along with Stiffener ");
                    sw.WriteLine("plates in bearing the reaction = lw = 20 * tw");
                    sw.WriteLine("                               = 20 * {0}", tw);
                    double brng_reaction = 20 * tw;
                    double lw = brng_reaction;
                    sw.WriteLine("                               = {0} mm", lw);
                    sw.WriteLine();
                    _I = ((t * (2 * h + 10) * (2 * h + 10) * (2 * h + 10)) / 12) + (2 * lw * tw * tw * tw / 12);

                    //**lw = ?
                    sw.WriteLine("    I = t * (2 * h + 10)^3 / 12 + 2 * lw * tw**3 / 12");
                    sw.WriteLine("      = {0} * (2 * {1} + 10)^3 / 12 + 2 * {2} * {3}^3 / 12", t, h, 200, tw);
                    //sw.WriteLine("      = 15 * 3703 / 12 + 2 * 200 * 103 / 12");
                    _I = (_I / 10E3);
                    _I = double.Parse(_I.ToString("0"));

                    sw.WriteLine("      = {0} * 10E3 Sq Sq mm", _I);
                    sw.WriteLine();

                    double A = 2 * h * t + 2 * lw * tw;
                    A = double.Parse(A.ToString("0"));
                    sw.WriteLine("    Area = A = 2 * h * t + 2 * lw * tw");
                    sw.WriteLine("         = 2 * {0} * {1} + 2 * {2} * {3}", h, t, lw, tw);
                    sw.WriteLine("         = {0} sq.mm", A);

                    sw.WriteLine();

                    double r = (_I * 10E3) / A;
                    r = Math.Sqrt(r);
                    r = double.Parse(r.ToString("0"));
                    sw.WriteLine("    r = √(I / A) = √({0} * 10E3 / {1}) = {2} mm", _I, A, r);
                    sw.WriteLine();


                    // ** 0.7 = ?
                    double _L = 0.7 * dw;
                    double lamda = (_L / r);
                    lamda = double.Parse(lamda.ToString("0.00"));
                    sw.WriteLine("    λ = Slenderness ratio = L / r");
                    sw.WriteLine();
                    sw.WriteLine("    L = Effective Length of stiffeners");
                    sw.WriteLine("      = 0.7 * dw");
                    sw.WriteLine("      = 0.7 * {0}", tw);
                    sw.WriteLine("      = {0} mm", _L);
                    sw.WriteLine();
                    sw.WriteLine("    λ = {0} / {1}", _L, r);
                    sw.WriteLine("      = {0}", lamda);

                    sw.WriteLine();

                    double sigma_ac = Get_Table_2_Value(lamda, 1, ref ref_string);
                    sw.WriteLine("    From Table 2 (given at the end of the Report) : {0}", ref_string);

                    sigma_ac = double.Parse(sigma_ac.ToString("0"));
                    sw.WriteLine("    Permissible Stress in axial compression σ_ac = {0} N/sq.mm", sigma_ac);
                    sw.WriteLine();

                    double area_req = V * 1000 / sigma_ac;
                    area_req = double.Parse(area_req.ToString("0"));
                    sw.WriteLine("    Area required = V * 1000 / σ_ac ");
                    sw.WriteLine("                  = {0} * 1000 / {1}", V, sigma_ac);
                    if (area_req < A)
                    {
                        sw.WriteLine("                  = {0} sq.mm < {1} sq.mm,  Ok", area_req, A);
                    }
                    else
                    {
                        sw.WriteLine("                  = {0} sq.mm > {1} sq.mm,  NOT OK", area_req, A);
                    }
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.8 : Connection between Bearing Stiffener and Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    //** 40 = ?
                    double len_alt = 2 * (dw - 40);
                    sw.WriteLine("Length available for alternate intermittent weld");
                    sw.WriteLine("   = 2 * (dw - 40)");
                    sw.WriteLine("   = 2 * ({0} - 40)", dw);
                    sw.WriteLine("   = {0} mm", len_alt);
                    sw.WriteLine();

                    double req_strnth_wld = (v * 1000 / len_alt);
                    req_strnth_wld = double.Parse(req_strnth_wld.ToString("0"));
                    sw.WriteLine("Required strength of weld = v * 1000 / 1920");
                    sw.WriteLine("                          = {0} * 1000 / {1}", v, len_alt);
                    sw.WriteLine("                          = {0} N/mm", req_strnth_wld);
                    sw.WriteLine();

                    sz_wld = req_strnth_wld / (K * sigma_tf);
                    sz_wld = double.Parse(sz_wld.ToString("0.00"));


                    //** σ_ac =  138 but 102.5 = ?
                    //sw.WriteLine("Size of weld = 286 / (K * σ_ac) = 286 / (0.7 * 102.5) = 3.98 mm");
                    sw.WriteLine("Size of weld = 286 / (K * σ_tf)");
                    sw.WriteLine("             = {0} / ({1} * {2})", req_strnth_wld, K, sigma_tf);
                    sw.WriteLine("             = {0} mm", sz_wld);
                    sw.WriteLine();

                    if (sz_wld < 5)
                        sz_wld = 5;
                    else
                    {
                        sz_wld = (int)sz_wld;
                        sz_wld += 1;
                    }
                    sw.WriteLine("Use {0} mm Fillet Weld", sz_wld);
                    sw.WriteLine();

                    double len_wld = 10 * tw;

                    sw.WriteLine("Length of Weld >= 10 * tw = 10 * {0} = {1} mm", tw, len_wld);
                    sw.WriteLine();

                    sw.WriteLine("Use {0} mm Long, {1} mm Weld Alternately.     Marked as (9) in the Drawing", len_wld, sz_wld);
                    sw.WriteLine();


                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.9 : Properties of Composite Section at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    double Ace = B * 1000 * D / m;
                    Ace = double.Parse(Ace.ToString("0"));

                    sw.WriteLine("Ace = B * 1000 * D/m");
                    sw.WriteLine("    = {0} * 1000 * {1}/{2}", B, D, m);
                    sw.WriteLine("    = {0} sq.mm", Ace);
                    sw.WriteLine();
                    sw.WriteLine("The centroid of Composite Section (Neutral Axis) is determined");
                    sw.WriteLine("by first moment of the areas about axis xx,");

                    Bf = bf1;

                    double Axy = Ace * (dw + 2 * tf1 + D / 2) + Bf * tf1 * (dw + tf1 + tf1 / 2) + dw * tw * (dw / 2 + tf1) + Bf * tf1 * tf1 / 2;

                    sw.WriteLine();
                    sw.WriteLine("Axy  = Ace * (dw + 2 * tf1 + D/2) + bf1 * tf1 * (dw + tf1 + tf1/2) ");
                    sw.WriteLine("       + dw * tw * (dw/2 +tf1) + Bf * tf1 * tf1/2");
                    sw.WriteLine();
                    sw.WriteLine("     = {0} * ({1} + 2 * {2} + {3}/2) + {4} * {2} * ({1} + {2} + {2}/2) ", Ace, dw, tf1, D, Bf);
                    sw.WriteLine("       + {0} * {1} * ({0}/2 + {2}) + {3} * {2} * {2}/2", dw, tw, tf1, Bf);
                    sw.WriteLine();

                    Axy = double.Parse(Axy.ToString("0"));
                    sw.WriteLine("     = {0}", Axy);
                    //sw.WriteLine("= 77046340");
                    double _A_d = Ace + (dw / 2) * tf1 + dw * tw + (dw / 2.0) * tf1;
                    _A_d = double.Parse(_A_d.ToString("0"));
                    // ** formula ?
                    sw.WriteLine();
                    sw.WriteLine("A = Ace + (dw / 2) * tf1 + dw * tw + (dw / 2.0) * tf1");
                    sw.WriteLine("  = {0} + ({1} / 2) * {2} + {0} * {3} + ({1} / 2.0) * {2}", Ace, dw, tf1, tw);
                    sw.WriteLine("  = {0} sq.mm", _A_d);
                    sw.WriteLine();

                    y = Axy / _A_d;
                    // ** sign y bar ?
                    sw.WriteLine("  y = Axy / A = {0} / {1}", Axy, _A_d);
                    //sw.WriteLine("    = {0:f0}", y);

                    y = double.Parse(y.ToString("0"));
                    sw.WriteLine("    = {0} mm", y);
                    sw.WriteLine();

                    double yc = dw + 2 * tf1 + D / 2 - y;

                    sw.WriteLine("  yc = dw + 2 * tf1 + D/2 -  y");
                    sw.WriteLine("     = {0} + 2 * {1} + {2}/2 -  {3}", dw, tf1, D, y);
                    sw.WriteLine("     = {0} mm", yc);
                    sw.WriteLine();


                    double Icomp = Ace * yc * yc +
                        (Bf * (dw + (2 * tf1)) * (dw + (2 * tf1)) * (dw + (2 * tf1))) / 12.0
                        - ((Bf - tw) * dw * dw * dw) / 12.0 +
                        (_A_d - Ace) * (y - (dw / 2.0) - tf1) * (y - (dw / 2.0) - tf1);


                    sw.WriteLine("Icomp = distance from centre of Deck Slab to Centroid of Composite Section");

                    sw.WriteLine("      = Ace * yc * yc ");
                    sw.WriteLine("        + (bf1 * (dw + (2 * tf1))^3 ) / 12.0");
                    sw.WriteLine("        - ((bf1 - tw) * dw**3) / 12.0 ");
                    sw.WriteLine("        + (A - Ace) * (y - (dw / 2.0) - tf1) * (y - (dw / 2.0) - tf1)");
                    sw.WriteLine();

                    sw.WriteLine("      = {0} * {1} * {1} ", Ace, yc);
                    sw.WriteLine("        + ({0} * ({1} + (2 * {2}))^3 ) / 12.0", Bf, dw, tf1);
                    sw.WriteLine("        - (({0} - {1}) * {2}^3) / 12.0 ", Bf, tw, dw);
                    sw.WriteLine("        + ({0} - {1}) * ({2} - ({3} / 2.0) - {4}) * ({2} - ({3} / 2.0) - {4})", _A_d, Ace, y, dw, tf1);
                    sw.WriteLine();




                    Icomp = Icomp / 10E9;
                    Icomp = double.Parse(Icomp.ToString("0.000"));
                    sw.WriteLine("      = {0} * 10E9 sq.sq.mm", Icomp);
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear force at junction of Slab and Girder is obtained by");

                    tau = (v * 1000 * Ace * yc) / (Icomp * 10E9);
                    sw.WriteLine("τ = v * 1000 * Ace *  yc / Icomp");
                    sw.WriteLine("  = {0} * 1000 * {1} * {2} / {3} * 10E9", v, Ace, yc, Icomp);
                    tau = double.Parse(tau.ToString("0"));
                    sw.WriteLine("  = {0} N/mm", tau);
                    sw.WriteLine();

                    double Q1 = tau * Bf;
                    Q1 = double.Parse(Q1.ToString("0"));
                    sw.WriteLine("Total Shear force at junction Q1 =  τ * Bf1 ");
                    sw.WriteLine("                                 =  {0} * {1}", tau, Bf);
                    sw.WriteLine("                                 =  {0} N", Q1);
                    sw.WriteLine();

                    double _do = 20.0;
                    sw.WriteLine("Using do = {0} mm diameter mild steel studs,     Marked as (10) in the Drawing", _do);
                    _10 = string.Format("{0} Ø Studs", _do);

                    sw.WriteLine("capacity of one shear connector is given by,");
                    sw.WriteLine();
                    // 196 = ?
                    double Q2 = 196 * _do * _do * Math.Sqrt(fck);
                    Q2 = double.Parse(Q2.ToString("0"));
                    sw.WriteLine("    Q2 = 196 * do*do *  √fck");
                    sw.WriteLine("       = 196 * {0}*{0} *  √{1}", _do, fck);
                    sw.WriteLine("       = {0} N", Q2);
                    sw.WriteLine();

                    // 5 = ?
                    double H = 5 * 20;
                    sw.WriteLine("Height of each stud = H");
                    sw.WriteLine("                    = 5 * do");
                    sw.WriteLine("                    = 5 * {0}", _do);
                    sw.WriteLine("                    = {0} mm", H);
                    sw.WriteLine();

                    double no_std_row = (Q1 / Q2);
                    no_std_row = double.Parse(no_std_row.ToString("0.00"));
                    sw.WriteLine("Number of studs required in a row");
                    sw.WriteLine();
                    if (no_std_row < 1.0)
                    {
                        sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} < 1", Q1, Q2, no_std_row);
                    }
                    else
                    {
                        sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} > 1", Q1, Q2, no_std_row);
                    }
                    sw.WriteLine("So, Provide a minimum of 2 mild Steel Studs in a row");
                    sw.WriteLine();

                    double N = 2;
                    double fs = 2.0;
                    double p = N * Q2 / (fs * tau);
                    p = double.Parse(p.ToString("0"));
                    sw.WriteLine("Pitch of Shear Connectors = p = N * Q2 / (fs * τ)");

                    sw.WriteLine("N = Number of Shear Connectors in a row = 2");
                    sw.WriteLine();
                    sw.WriteLine("Fs = Factor of Safety = 2.0");
                    sw.WriteLine();
                    sw.WriteLine("p = 2 * {0} / (2 * {1})", Q2, tau);
                    sw.WriteLine("  = {0} mm", p);
                    sw.WriteLine();

                    sw.WriteLine("Maximum permissible pitch is the lowest value of:");
                    sw.WriteLine();
                    sw.WriteLine("(i)     3 * Thickness of Slab = 3 * {0} = {1:f0} mm", D, (3 * D));
                    sw.WriteLine("(ii)    4 * Height of Stud = 4 * (5 * do) = 4 * {0:f0} = {1:f0} mm", (5 * _do), (4 * 5 * _do));
                    sw.WriteLine("(iii)   600 mm");
                    sw.WriteLine();
                    sw.WriteLine("Hence provide the pitch of 400 mm in the longitudinal direction.    Marked as (11) in the Drawing");

                    #endregion
                    step++;
                }
                while (step <= 3);



                #region CHECK FOR LIVE LOAD DEFLECTION FOR LIVE LOAD
                sw.WriteLine();
                //NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-
                //NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION
                //  19     3       0.00000E+000  -2.71860E+000   0.00000E+000  -3.09210E-003   0.00000E+000   3.75020E-003


                sw.WriteLine("");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("STEP {0} : CHECK VERTICAL DEFLECTION FOR LIVE LOAD", step++);
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                sw.WriteLine("");
                sw.WriteLine("                ");
                sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                sw.WriteLine("");
                Max_Node_Displacement = NodeResultData.Parse(Node_Displacement_Data_LL);
                sw.WriteLine("");
                sw.WriteLine(Max_Node_Displacement.ToString());
                sw.WriteLine("");
                sw.WriteLine("");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                sw.WriteLine("");
                double val = S / 800.0;
                sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", S, val);
                sw.WriteLine("");
                if (Max_Node_Displacement.Max_Translation < val)
                    sw.WriteLine("MAXIMUM  VERTICAL DEFLECTION = {0:f5} M. < {1:f5} M.    OK.", Max_Node_Displacement.Max_Translation, val);
                else
                    sw.WriteLine("MAXIMUM  VERTICAL DEFLECTION = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                #region CHECK FOR LIVE LOAD DEFLECTION
                #endregion CHECK FOR LIVE LOAD DEFLECTION

                sw.WriteLine("");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("STEP {0} :  CHECK VERTICAL DEFLECTION FOR DEAD LOAD", step++);
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                sw.WriteLine("");
                sw.WriteLine("                ");
                sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                sw.WriteLine("");
                Max_Node_Displacement = NodeResultData.Parse(Node_Displacement_Data_DL);
                sw.WriteLine("");
                sw.WriteLine(Max_Node_Displacement.ToString());
                sw.WriteLine("");
                sw.WriteLine("");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                sw.WriteLine("");
                // val = S / 800.0;
                //sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", S, val);
                //sw.WriteLine("");
                //if (Max_Node_Displacement.Max_Translation < val)
                //    sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. < {1:f5} M.    OK.", Max_Node_Displacement.Max_Translation, val);
                //else
                //    sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                sw.WriteLine();
                sw.WriteLine("MAXIMUM  VERTICAL DEFLECTION = {0:f5} M.", Max_Node_Displacement.Max_Translation, val);
                sw.WriteLine();
                sw.WriteLine("The Deflection for Dead Load is to be controlled by providing longitudinal");
                sw.WriteLine("Camber along the length of the Main Girder between end to end supports.");
                sw.WriteLine();
                sw.WriteLine();
                #endregion CHECK FOR LIVE LOAD DEFLECTION FOR LIVE LOAD

                if (false)
                {
                    #region DESIGN OF RCC DECK SLAB

                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : DESIGN OF RCC DECK SLAB", step);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.1 : COMPUTATION OF PERMANENT LOAD", step);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();

                    double self_weight_deck_slab = (D / 1000.0) * gamma_c;

                    sw.WriteLine("Self weight of Deck Slab = (D/1000) * γ_c");
                    sw.WriteLine("                         = ({0:f2}) * {1:f0}", (D / 1000), gamma_c);
                    sw.WriteLine("                         = {0:f2} kN/sq.mm", self_weight_deck_slab);
                    sw.WriteLine();

                    double self_weight_wearing_course = (Dwc / 1000.0) * gamma_wc;
                    sw.WriteLine("Self weight of wearing course = (Dwc/1000) * γ_wc");
                    sw.WriteLine("                              = {0:f2} * {1}", (Dwc / 1000), gamma_wc);
                    sw.WriteLine("                              = {0:f2} kN/sq.mm", self_weight_wearing_course);
                    sw.WriteLine();
                    double DL = self_weight_deck_slab + self_weight_wearing_course;

                    sw.WriteLine("Total Load = DL ");
                    sw.WriteLine("           = {0:f2} + {1:f2}", self_weight_deck_slab, self_weight_wearing_course);
                    sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                    DL = (int)DL;
                    DL += 1.0;
                    sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                    #endregion

                    #region STEP 2.2 : BENDING MOMENT BY MOVING LOAD
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.2 : BENDING MOMENT BY MOVING LOAD", step);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();


                    sw.WriteLine("Load = WL = {0} kN", WL);
                    sw.WriteLine("Panel Dimension L = {0:f2} m,                B = {1:f2} m", L, B);
                    sw.WriteLine("Load Dimension v = {0:f2}, u = {1:f2} m", v, u);
                    sw.WriteLine();
                    sw.WriteLine("Considering 45° Load dispersion through wearing Course");
                    sw.WriteLine();

                    double _v = v + (2 * (Dwc / 1000.0));
                    sw.WriteLine("    v = {0:f2} + (2*{1:f2}) = {2:f2} m.", v, (Dwc / 1000.0), _v);
                    sw.WriteLine();
                    //sw.WriteLine("      = {0:f2} m.", _v);
                    double _u = u + (2 * (Dwc / 1000.0));
                    sw.WriteLine("    u = {0:f2} + (2*{1:f2}) = {2:f2} m.", u, (Dwc / 1000.0), _u);
                    sw.WriteLine();
                    //sw.WriteLine("      = {0:f2} m.", _u);


                    u_by_B = v;
                    v = _v;
                    _v = u_by_B;

                    u_by_B = u;
                    u = _u;
                    _u = u_by_B;



                    u_by_B = u / B;
                    sw.WriteLine("    u / B = {0:f2} / {1:f2} = {2:f3}", u, B, u_by_B);
                    sw.WriteLine();
                    double v_by_L = v / L;

                    sw.WriteLine("    v / L = {0:f2} / {1:f2} = {2:f3}", v, L, v_by_L);
                    sw.WriteLine();

                    double k = B / S;
                    sw.WriteLine("    K = B / S = {0:f2} / {1:f2} = {2:f3}", B, L, k);
                    sw.WriteLine();


                    k = Double.Parse(k.ToString("0.0"));
                    if (k < 0.4)
                        k = 0.4;
                    if (k > 1.0) k = 1.0;
                    f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);
                    //if (k == 0.4)
                    //{
                    f_c.txt_m1.Text = "0.085";
                    f_c.txt_m2.Text = "0.017";
                    //}
                    f_c.ShowDialog();
                    double m1, m2;
                    m1 = f_c.m1;
                    m2 = f_c.m2;

                    sw.WriteLine();
                    sw.WriteLine("From Pigeaud’s Curves, for K = {0:f1}", k);
                    sw.WriteLine("    m1 = {0}", m1);
                    sw.WriteLine("    m2 = {0}", m2);

                    double _MB = WL * (m1 + 0.15 * m2);
                    _MB = double.Parse(_MB.ToString("0"));
                    sw.WriteLine();
                    sw.WriteLine("Short Span Bending Moment = MB’");
                    sw.WriteLine("                          = WL * (m1 + 0.15 * m2)");
                    sw.WriteLine("                          = {0} * ({1} + 0.15 * {2})", WL, m1, m2);
                    sw.WriteLine("                          = {0} kN-m", _MB);
                    sw.WriteLine();

                    double MB1 = IF * CF * _MB;
                    MB1 = double.Parse(MB1.ToString("0"));

                    sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                    sw.WriteLine("  = MB1");
                    sw.WriteLine("  = IF * CF * MB’ ");
                    sw.WriteLine("  = {0} * {1:f2} * {0:f2} ", IF, CF, _MB);
                    sw.WriteLine("  = {0} kN-m", MB1);
                    sw.WriteLine();

                    double _ML = WL * (m2 + 0.15 * m1);

                    sw.WriteLine("Long Span Bending Moment = ML’ ");
                    sw.WriteLine("                         = WL * (m2 + 0.15 * m1) ");
                    sw.WriteLine("                         = {0} * ({1} + 0.15 * {2}) ", WL, m2, m1);
                    sw.WriteLine("                         = {0:f2} kN-m", _ML);
                    sw.WriteLine();

                    double ML1 = IF * CF * _ML;
                    sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                    sw.WriteLine("  = ML1");
                    sw.WriteLine("  = IF * CF * ML’ ");
                    sw.WriteLine("  = {0} * {1} * {2:f2} ", IF, CF, _ML);
                    sw.WriteLine("  = {0:f2} kN-m", ML1);
                    sw.WriteLine();
                    #endregion

                    #region STEP 2.3 : BENDING MOMENT BY Permanent Load
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.3 : BENDING MOMENT BY PERMANENT LOAD", step);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();

                    sw.WriteLine("Permanent Load of Deck Slab = DL = {0} kN/sq.mm", DL);

                    double WD = DL * B * L;
                    sw.WriteLine("Permanent Load per Panel = WD");
                    sw.WriteLine("                     = DL * B * L");
                    sw.WriteLine("                     = {0} * {1} * {2}", DL, B, L);
                    sw.WriteLine("                     = {0:f2} kN", WD);
                    sw.WriteLine();
                    sw.WriteLine("u / B = 1 and  v / L = 1");

                    k = B / L;
                    k = Double.Parse(k.ToString("0.000"));
                    sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f1}", B, L, k);
                    sw.WriteLine("1/k = 1 / {0} = {1:f2}", k, (1 / k));

                    f_c = new frmCurve(k, 1.0, 1.0, LoadType.FullyLoad);

                    k = Double.Parse(k.ToString("0.0"));
                    //if (k == 0.4)
                    //{
                    f_c.txt_m1.Text = "0.047";
                    f_c.txt_m2.Text = "0.006";
                    //}
                    f_c.ShowDialog();

                    m1 = f_c.m1;
                    m2 = f_c.m2;
                    double MB, ML;

                    sw.WriteLine();
                    sw.WriteLine("Using Pigeaud’s Curves, m1 = {0} and m2 = {1}", m1, m2);
                    sw.WriteLine();
                    _MB = WD * (m1 + 0.15 * m2);
                    sw.WriteLine("Short Span Bending Moment = MB’");
                    sw.WriteLine("                          = WD * (m1 + 0.15 * m2)");
                    sw.WriteLine("                          = {0:f2} * ({1} + 0.15 * {2})", WD, m1, m2);
                    sw.WriteLine("                          = {0:f2} kN-m", _MB);
                    sw.WriteLine();


                    sw.WriteLine("Short Span Bending Moment including Continuity Factor");
                    sw.WriteLine("  = MB2");

                    double MB2 = CF * _MB;
                    sw.WriteLine("  = CF * MB’");
                    sw.WriteLine("  = {0:f2} * {1:f2}", CF, _MB);
                    sw.WriteLine("  = {0:f2} kN-m", MB2);
                    sw.WriteLine();

                    _ML = WD * (m2 + 0.15 * m1);
                    sw.WriteLine("Long Span Bending Moment = ML’");
                    sw.WriteLine("                         = WD * (m2 + 0.15 * m1)");
                    sw.WriteLine("                         = {0:f2} * ({1} + 0.15 * {2})", WD, m2, m1);
                    sw.WriteLine("                         = {0:f2} kN-m", _ML);
                    sw.WriteLine();

                    double ML2 = CF * _ML;
                    sw.WriteLine("Long Span Bending Moment including Continuity Factor");
                    sw.WriteLine("  = ML2");
                    sw.WriteLine("  = CF * ML’");
                    sw.WriteLine("  = {0:f2} * {1:f2}", CF, _ML);
                    sw.WriteLine("  = {0:f2} kN-m", ML2);
                    sw.WriteLine();
                    sw.WriteLine("Design Bending Moments are:");
                    MB = MB1 + MB2;

                    sw.WriteLine("Along Short Span = MB");
                    sw.WriteLine("                 = MB1 + MB2");
                    sw.WriteLine("                 = {0:f2} + {1:f2}", MB1, MB2);
                    sw.WriteLine("                 = {0:f2} kN-m", MB);
                    sw.WriteLine();


                    ML = ML1 + ML2;
                    sw.WriteLine("Along Long Span = ML");
                    sw.WriteLine("                = ML1 + ML2");
                    sw.WriteLine("                = {0:f2} + {1:f2}", ML1, ML2);
                    sw.WriteLine("                = {0:f2} kN-m", ML);
                    sw.WriteLine();

                    #endregion

                    #region STEP 2.4 : DESIGN OF SECTION FOR RCC DECK SLAB
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.4 : STRUCTURAL DETAILING FOR RCC DECK SLAB", step);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();



                    double d = (MB * 10E5) / (Q * 1000.0);

                    d = Math.Sqrt(d);
                    d = double.Parse(d.ToString("0"));
                    sw.WriteLine("d = √((MB * 10E5) / (Q*b))");
                    sw.WriteLine("  = √(({0:f2} * 10E5) / ({1:f3}*1000))", MB, Q);
                    sw.WriteLine("  = {0} mm", d);
                    sw.WriteLine();

                    sw.WriteLine("The overall depth of RCC Deck Slab = {0} mm", D);

                    double _d = d;
                    d = D - 40.0;
                    sw.WriteLine();
                    sw.WriteLine("Effective Depth = {0} - 40 = {1} mm = d", D, d);


                    double Ast = MB * 10E5 / (sigma_st * j * d);
                    Ast = double.Parse(Ast.ToString("0"));
                    sw.WriteLine();
                    sw.WriteLine("Required steel along short span");
                    sw.WriteLine("    = Ast");
                    sw.WriteLine("    = (MB * 10E5) / (σ_st * j * d)");
                    sw.WriteLine("    = ({0:f2} * 10E5) / ({1} * {2} * {3})", MB, sigma_st, j, d);
                    sw.WriteLine("    = {0} sq.mm", Ast);

                    List<double> lst_dia = new List<double>();

                    lst_dia.Add(10);
                    lst_dia.Add(12);
                    lst_dia.Add(16);
                    lst_dia.Add(20);
                    lst_dia.Add(25);
                    lst_dia.Add(32);


                    int dia_indx = 0;
                    double dia = lst_dia[0];
                    double _ast = 0.0;
                    double no_bar = 0.0;
                    double spacing = 140;
                    do
                    {
                        dia = lst_dia[dia_indx];
                        no_bar = (1000.0 / spacing);
                        _ast = Math.PI * dia * dia / 4.0;
                        _ast = _ast * no_bar;
                        dia_indx++;
                    }
                    while (_ast < Ast);

                    sw.WriteLine();
                    sw.WriteLine("Provide T{0:f0} mm bars at {1:f0} mm c/c.     Marked as (1) in the Drawing", dia, spacing);
                    //(1) = T12 mm bars at 140 mm c/c.
                    _1 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                    sw.WriteLine();

                    sw.WriteLine("Effective depth for Long span using T10 mm bars");
                    sw.WriteLine();

                    double d1 = d - (dia / 2.0) - (10.0 / 2.0);
                    sw.WriteLine("    d1 = d - ({0:f0}/2) - (10/2)", dia);
                    sw.WriteLine("       = {0} - {1:f0} - 5", d, (dia / 2.0));
                    sw.WriteLine("       = {0:f0} mm", d1);
                    sw.WriteLine();

                    Ast = (ML * 10E5) / (sigma_st * j * d1);
                    Ast = double.Parse(Ast.ToString("0"));

                    sw.WriteLine();
                    sw.WriteLine("Required steel along long span");
                    sw.WriteLine("  = Ast");
                    sw.WriteLine("  = ML * 10E5 / (σ_st * j * d1)");
                    sw.WriteLine("  = {0:f2} * 10E5 / ({1} * {2} * {3})", ML, sigma_st, j, d1);
                    sw.WriteLine("  = {0} sq.mm", Ast);
                    sw.WriteLine();

                    spacing = 150;
                    dia_indx = 0;
                    do
                    {
                        dia = lst_dia[dia_indx];
                        no_bar = (1000.0 / spacing);
                        _ast = Math.PI * dia * dia / 4.0;
                        _ast = _ast * no_bar;
                        dia_indx++;
                    }
                    while (_ast < Ast);

                    sw.WriteLine("Provide T{0:f0} Bars at {1:f0} mm c/c.    Marked as (2) in the Drawing", dia, spacing);
                    //(2) = T10 Bars at 150 mm c/c.
                    _2 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                    #endregion
                }


                sw.WriteLine();
                sw.WriteLine();
                Write_Table_1(sw);
                Write_Table_2(sw);

                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
                //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
                //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
                //sw.WriteLine("-----------------------------------------------------------------------");

                //sw.WriteLine("110    87       87       87      87       87      87      87");
                //sw.WriteLine("130    87       87       87      87       87      84      82");
                //sw.WriteLine("150    87       87       87      85       80      77      75");
                //sw.WriteLine("170    87       87       83      80       76      72      70");
                //sw.WriteLine("190    87       87       79      75");
                //sw.WriteLine("200    87       85       77");
                //sw.WriteLine("220    87       80       73");
                //sw.WriteLine("240    87       77");
                //sw.WriteLine("-----------------------------------------------------------------------");


                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
                //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
                //sw.WriteLine("λ= (L/r)  ______________________________________");
                //sw.WriteLine("           236        299        331       362");
                //sw.WriteLine("---------------------------------------------------");
                //sw.WriteLine("0         140.0      171.2       191.5    210.0");
                //sw.WriteLine("20        136.0      167.0       186.0    204.0");
                //sw.WriteLine("40        130.0      157.0       174.0    190.0");
                //sw.WriteLine("60        118.0      139.0       151.6    162.0");
                //sw.WriteLine("80        101.0      113.5       120.3    125.5");
                //sw.WriteLine("100        80.5       87.0        90.2     92.7");
                //sw.WriteLine("120        63.0       66.2        68.0     69.0");
                //sw.WriteLine("140        49.4       51.2        52.0     52.6");
                //sw.WriteLine("160        39.0       40.1        40.7     41.1");
                //sw.WriteLine("---------------------------------------------------");





                if (DesignSummery.Count != 0)
                {
                    sw.WriteLine("---------------------------------------------------------------------------");
                    sw.WriteLine("---------------------       DESIGN SUMMARY       --------------------------");
                    sw.WriteLine("---------------------------------------------------------------------------");
                    sw.WriteLine();
                    //sw.WriteLine("BRIDGE SPAN = L = 46.000m. Depth of Girder = Deff = 1.5m.");
                    sw.WriteLine("BRIDGE SPAN = L = {0:f3}m. Depth of Girder = Deff = {1:f3}m.", S, deff);
                    if (IsBoxArrangement)
                    {
                        sw.WriteLine("A Steel Box Girder arrangement is selected,");
                        sw.WriteLine("Providing number of Web Plates as mentioned below on either side of the Box,");
                    }
                    else
                    {
                        sw.WriteLine("A Steel Plate Girder arrangement is selected,");
                        sw.WriteLine("Providing number of Web Plates as mentioned below at the Centre of the Girder, in between Flanges,");

                    }

                    sw.WriteLine();





                    foreach (string s in DesignSummery)
                    {
                        sw.WriteLine(s);
                    }
                    //sw.WriteLine();
                    //sw.WriteLine("---------------------------------------------------------------------------");
                    //sw.WriteLine("---------------------   END OF DESIGN SUMMARY    --------------------------");
                    //sw.WriteLine("---------------------------------------------------------------------------");
                    //sw.WriteLine();

                }
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine("---------------------        DESIGN RESULT       --------------------------");
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine();
                sw.WriteLine("DESIGN RESULT : ");

                if (DesignResult.Count != 0)
                {
                    foreach (string s in DesignResult)
                    {
                        sw.WriteLine(s);
                    }
                }
                else
                    sw.WriteLine("DESIGN IS FOUND OK");
                sw.WriteLine();
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine("---------------------    END OF DESIGN RESULT    --------------------------");
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine();

                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.Flush();
                sw.Close();
            }
        }

        //Chiranjit [2012 12 17]
        public void Calculate_Program_2012_12_17()
        {
            string ref_string = "";
            frmCurve f_c = null;
            string ang_name = "";

            rep_file_name = IsInnerGirder ? rep_file_name_inner : rep_file_name_outer;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                DesignResult.Clear();
                DesignSummery.Clear();
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*         DESIGN OF COMPOSITE BRIDGE         *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bridge Span [S] = {0} m", S);
                sw.WriteLine("Carriageway Width [B1] = {0} m    Marked as (A) in the Drawing", B1);
                _A = B1 + " m.";

                sw.WriteLine("Footpath Width [B2] = {0} m", B2);
                sw.WriteLine("Spacing on either side of Main Girders [B] = {0} m     Marked as (B) in the Drawing", B);
                _B = B + " m.";


                sw.WriteLine();
                sw.WriteLine("Concrete Grade [fck] = M {0:f0} = {0:f0} N/sq.mm", fck);
                sw.WriteLine("Reinforcement Steel Frade [fy] = Fe {0:f0} = {0:f0} N/sq.mm", fy);
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Rolled Steel Section of Yield Stress [YS] = {0} N/sq.mm", YS);
                sw.WriteLine("SLAB Thickness [D] = {0} mm     Marked as (C) in the Drawing", D);
                _C = D / 1000.0 + " m.";



                sw.WriteLine("Panel Length [L] = {0} m        Marked as (D) in the Drawing", L);
                _D = L + " m.";


                sw.WriteLine("Thickness of wearing course [Dwc] = {0} mm     Marked as (G) in the Drawing", Dwc);
                _G = Dwc / 1000.0 + " m.";


                sw.WriteLine();
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine("Unit Weight of wearing cource [γ_wc] = {0} kN/cu.m", gamma_wc);
                sw.WriteLine("Tracked Vehicle Load [WL] = {0} kN", WL);
                sw.WriteLine("Length of Loaded area [v] = {0} m     Marked as (E) in the Drawing", v);
                _E = v + " m.";


                sw.WriteLine("Width of Loaded area [u] = {0} m      Marked as (F) in the Drawing", u);
                _F = u + " m.";

                sw.WriteLine();
                sw.WriteLine("Impact Factor [IF] = {0}", IF);
                sw.WriteLine("Continuity Factor [CF] = {0}", CF);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("[σ_st] = {0} N/sq.mm", sigma_st);
                sw.WriteLine("Permissible Bending Stress in Steel [σ_b] = {0} N/sq.mm", sigma_b);
                sw.WriteLine("Permissible Shear Stress in Steel [τ] = {0} N/sq.mm", tau);
                sw.WriteLine("Permissible Shear Stress through fillet Weld [σ_tf] = {0} N/sq.mm", sigma_tf);
                sw.WriteLine("Constant ‘K’ = {0}", K);
                sw.WriteLine("Permissible Bearing Stress [σ_p] = {0} N/sq.mm", sigma_p);
                sw.WriteLine();
                sw.WriteLine();
                //==================================================

                //sw.WriteLine("Flange Plates : nf =4, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");
                //sw.WriteLine("==================================================");
                //sw.WriteLine("For Quarter Span Section (L/4), ");
                //sw.WriteLine("Starting from Deff  to L/4 ");
                //sw.WriteLine("And  Deff  L-L/4  to  L-Deff, ");
                //sw.WriteLine("User given Sections are as follows:");

                //sw.WriteLine("Web Plates : nw =1, Dw=1600 mm, tw=20 mm");
                //sw.WriteLine("Flange Plates : nf =2, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");
                //sw.WriteLine("==================================================");
                //sw.WriteLine("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder, ");
                //sw.WriteLine("Starting from Deff  to L/4 ");
                //sw.WriteLine("And  Deff  L-L/4  to  L-Deff, ");
                //sw.WriteLine("User given Sections are as follows:");

                //sw.WriteLine("Web Plates : nw =1, Dw=1600 mm, tw=20 mm");
                //sw.WriteLine("Flange Plates : nf =2, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");

                sw.WriteLine("");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion


                int step = 1;
                string step_text = "";
                double u_by_B = 0.0;

                #region Report

                do
                {
                    if (step == 1)
                    {
                        dw = L_2_dw;
                        nw = (int)L_2_nw;
                        tw = L_2_tw;
                        bf1 = L_2_bf1;
                        tf1 = L_2_tf1;
                        bf2 = L_2_bf2;
                        tf2 = L_2_tf2;
                        nf = (int)L_2_nf;



                        des_moment = L_2_Moment;
                        des_shear = L_2_Shear;

                        na = (int)L_2_nos_ang;
                        ang = L_2_ang;
                        ang_name = L_2_ang_name;
                        ang_thk = L_2_ang_thk;
                        step_text = "MID SPAN (L/2)";
                    }
                    else if (step == 2)
                    {
                        dw = L_4_dw;
                        nw = (int)L_4_nw;
                        tw = L_4_tw;
                        bf1 = L_4_bf1;
                        nf = (int)L_4_nf;
                        tf1 = L_4_tf1;

                        bf2 = L_4_bf2;
                        tf2 = L_4_tf2;


                        des_moment = L_4_Moment;
                        des_shear = L_4_Shear;

                        na = (int)L_4_nos_ang;
                        ang = L_4_ang;
                        ang_thk = L_4_ang_thk;
                        ang_name = L_4_ang_name;

                        step_text = "QUARTER SPAN (L/4)";
                    }
                    else if (step == 3)
                    {
                        dw = Deff_dw;
                        nw = (int)Deff_nw;
                        tw = Deff_tw;
                        bf1 = Deff_bf1;
                        nf = (int)Deff_nf;
                        tf1 = Deff_tf1;
                        bf2 = Deff_bf2;
                        tf2 = Deff_tf2;


                        des_moment = Deff_Moment;
                        des_shear = Deff_Shear;

                        na = (int)Deff_nos_ang;
                        ang_name = Deff_ang_name;
                        ang = Deff_ang;
                        ang_thk = Deff_ang_thk;

                        step_text = "BOTH END (Deff)";
                    }

                    #region STEP 1 : DESIGN OF STEEL PALTE GIRDER
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("--------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : DESIGN OF STEEL PLATE GIRDER for Section at {1}", step, step_text);
                    sw.WriteLine("--------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("-------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.1 : Analysis forces and User given section Details at {1}", step, step_text);
                    sw.WriteLine("-------------------------------------------------------------------------");
                    sw.WriteLine();
                    double M = des_moment;
                    sw.WriteLine("Design Bending Moment = M = {0} kN-m", M);
                    double deg_sh_frc = des_shear;
                    sw.WriteLine("Design shear force = V = {0} kN", des_shear);
                    sw.WriteLine();
                    sw.WriteLine();
                    //DesignSummery.Add("==============================================================================");
                    if (step == 1)
                    {
                        DesignSummery.Add("==========================     AT MID SPAN     ===============================");
                        DesignSummery.Add(string.Format(""));
                        DesignSummery.Add(string.Format("For Mid Span Section (L/2), Starting from    (L/2–L/4) to (L/2+L/4),"));
                        DesignSummery.Add(string.Format("                                     from =  {0:f3}m  to {1:f3}m,", (S / 2.0 - S / 4.0), (S / 2.0 + S / 4.0)));
                        sw.WriteLine("For Mid Span Section (L/2), Starting from L/2 – L/4 to L/2+L/4,");
                    }
                    else if (step == 2)
                    {
                        DesignSummery.Add("=====================  AT QUARTER SPAN ON BOTH SIDES  =========================");
                        DesignSummery.Add(string.Format(""));
                        DesignSummery.Add(string.Format("For Quarter Span Section (L/4),"));
                        DesignSummery.Add(string.Format("On the left side from   Deff  to   L/4  =  {0:f3}m. to  {1:f3}m. and", deff, (S / 4)));
                        DesignSummery.Add(string.Format("On the right side from L/2+L/4  to  L-Deff = from {0:f3}m. to {1:f3}m.", (3 * S / 4.0), (S - deff)));


                        //For Quarter Span Section (L/4),

                        //On the left side from   Deff  to   L/4  =  1.500m. to  11.500m. and  
                        //On the right side from L/2+L/4  to  L-Deff = from 34.500m. to 44.500m.


                        //DesignSummery.Add(string.Format("For Quarter Span Section (L/4),Starting from Deff  to L/4 And  Deff  L-L/4  to  L-Deff,"));
                        sw.WriteLine("For Quarter Span Section (L/4),Starting from Deff  to L/4 And  Deff  L-L/4  to  L-Deff,");
                    }
                    else if (step == 3)
                    {
                        DesignSummery.Add("=================     AT END SECTIONS ON BOTH SIDES   =========================");
                        DesignSummery.Add("");
                        DesignSummery.Add(string.Format("For Span Section (Deff)"));
                        DesignSummery.Add(string.Format("from either End up to distance equals to Effective Depth of Girder,"));
                        DesignSummery.Add(string.Format("On the left side from   0  to   Deff  =  from {0:f3}m. to  {1:f3}m. and ", 0, deff));
                        DesignSummery.Add(string.Format("On the right side from L - Deff  to  L = from {0:f3}m. to  {1:f3}m.", (S - deff), S));


                        //For Span Section (Deff) 

                        //from either End up to distance equals to Effective Depth of Girder,
                        //On the left side from   0  to   Deff  =  from 0.000m. to  1.500m. and  
                        //On the right side from L - Deff  to  L = from 44.500m. to  46.000m.




                        //DesignSummery.Add(string.Format("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder,"));
                        //DesignSummery.Add(string.Format("Starting from Deff  to L/4 and  Deff  L-L/4  to  L-Deff, "));
                        sw.WriteLine("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder,");
                        sw.WriteLine("Starting from Deff  to L/4 and  Deff  L-L/4  to  L-Deff, ");
                    }
                    sw.WriteLine();
                    DesignSummery.Add("");
                    DesignSummery.Add(string.Format("Web Plates : Number of Plates [nw] = {0}, Depth [dw] = {1} mm, Thickness [tw] = {2} mm", nw, dw, tw));
                    DesignSummery.Add("");

                    sw.WriteLine("Web Plates : Number of Plates [nw] = {0}, Depth [dw] = {1} mm, Thickness [tw] = {2} mm", nw, dw, tw);
                    sw.WriteLine();
                    DesignSummery.Add(string.Format("Flange Plates : Number of Plates [nf] = {0}, Breadth [bf] = {1} mm, Thickness [tf] = {2} mm", nf, bf1, tf1));
                    DesignSummery.Add("");
                    //sw.WriteLine("Top Flange Plates : Number of Plates [nf] = {0}, Breadth [bf] = {1} mm, Thickness [tf] = {2} mm", nf, bf1, tf1);
                    sw.WriteLine("Top Flange Plates    :    Breadth [bf1] = {0} mm,   Thickness [tf1] = {1} mm", bf1, tf1);
                    sw.WriteLine("Bottom Flange Plates :    Breadth [bf2] = {0} mm,   Thickness [tf2] = {1} mm", bf2, tf2);


                    //RolledSteelAnglesRow tab_data = tbl_rolledSteelAngles.GetDataFromTable("ISA", ang, ang_thk);
                    RolledSteelAnglesRow tab_data = iApp.Tables.Get_AngleData_FromTable(ang_name, ang, ang_thk);


                    //sw.WriteLine("Angles : Number of Angles = {0}, {1} x {2}", na, ang, ang_thk);
                    sw.WriteLine();
                    DesignSummery.Add("");
                    DesignSummery.Add(string.Format("Angles : Number of Angles [na] = {0}, Size: {1} {2}X{3}", na, tab_data.SectionName, tab_data.SectionSize, ang_thk));
                    sw.WriteLine("Angles : Number of Angles [na] = {0}, Size: {1} {2}X{3}", na, tab_data.SectionName, tab_data.SectionSize, ang_thk);
                    DesignSummery.Add("");
                    DesignSummery.Add("==============================================================================");
                    DesignSummery.Add("");
                    DesignSummery.Add("");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.2 : Size of Web Plate and Flange Plate at {1}", step, step_text);
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine();

                    //sw.WriteLine("Approximate depth of Girder = S /10 = {0} / 10 = {1} m", S, (S / 10.0));
                    double dval = (S / 2) + 2;
                    double flan_over_hang = 0;
                    sw.WriteLine("Approximate Web depth of Girder  = S /{0} = {1}/ {0} = {2} m", dval, S, (S / dval));
                    sw.WriteLine();

                    double eco_depth_girder = (M * 10E5) / sigma_b;

                    eco_depth_girder = Math.Pow(eco_depth_girder, (1.0 / 3.0));
                    eco_depth_girder = 5.0 * eco_depth_girder;


                    sw.WriteLine("Economical Web depth of Girder = 5 * (M / σb)^(1/3)");
                    sw.WriteLine("                               = 5 * ({0} * 10^6 / {1})^(1/3)", M, sigma_b);
                    eco_depth_girder = Double.Parse(eco_depth_girder.ToString("0"));
                    sw.WriteLine("                               = {0:f3} mm", eco_depth_girder);
                    sw.WriteLine();
                    sw.WriteLine("Thickness of web plate for shear considerations");
                    sw.WriteLine("Web depth by asuming thickness of plate = tw = {0} mm thick plate for shear considerations", tw);
                    //sw.WriteLine("asuming thickness of plate = tw");
                    //sw.WriteLine();
                    //sw.WriteLine("Web depth by {0} mm thick plate for shear considerations", tw);
                    sw.WriteLine();
                    double web_depth = (deg_sh_frc * 1000) / (tau * tw);
                    sw.WriteLine("    = V / (τ * {0})", tw);
                    sw.WriteLine("    = {0} * 1000 / ({1}*{2})", deg_sh_frc, tau, tw);
                    web_depth = Double.Parse(web_depth.ToString("0.00"));
                    sw.WriteLine("    = {0} mm", web_depth);
                    sw.WriteLine();
                    sw.WriteLine("Let us provide Web Depth = Dw = {0}mm  (user given value)", dw);
                    sw.WriteLine();
                    //
                    //double dw, tw;
                    //dw = 1000.0;
                    //tw = 10.0;
                    double Aw = nw * dw * tw;

                    dval = 16 * tw;
                    sw.WriteLine("Permissible overhang for flange =  16 * tw");
                    sw.WriteLine("                                =  16 * {0}", tw);
                    sw.WriteLine("                                =  {0} mm", dval);
                    sw.WriteLine();

                    flan_over_hang = (bf1 - tw) / 2.0;
                    sw.WriteLine("In present case the flange overhang  = (bf - tw) / 2.0");
                    sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf1, tw);
                    //sw.WriteLine("                                     = {0} mm", flan_over_hang);
                    IsBoxArrangement = flan_over_hang > dval;
                    if (IsBoxArrangement)
                    {
                        //sw.WriteLine("In present case the flange overhang  = (bw - tf) / 2.0");
                        //sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf, tw);
                        sw.WriteLine("                                     = {0:G3}mm > {1:G3}mm", flan_over_hang, dval);
                    }
                    else
                    {
                        //sw.WriteLine("In present case the flange overhang  = (bw - tf) / 2.0");
                        //sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf, tw);
                        sw.WriteLine("                                     = {0:G3}mm < {1:G3}mm", flan_over_hang, dval);
                        //sw.WriteLine("                                     = {0} mm", flan_over_hang);


                        sw.WriteLine();
                        //sw.WriteLine("Number of Flange Please will be equaly divided on Upper and Lower Side of the Box Girder");
                        //sw.WriteLine("It is suggested to go for Steel Box Girder with 2 webs suitable plates");
                        //sw.WriteLine();
                    }

                    sw.WriteLine();
                    sw.WriteLine("");

                    int sides = IsBoxArrangement ? 2 : 1;
                    if (IsBoxArrangement)
                    {
                        sw.WriteLine("So, a Box Girder arrangement is considered, ");
                        sw.WriteLine("");
                        sw.WriteLine("For Box Girder, sides = 2, and , ");
                        sw.WriteLine("");
                    }
                    else
                    {
                        sw.WriteLine("So, a Plate Girder arrangement is considered, ");
                        sw.WriteLine("");
                        sw.WriteLine("For Plate Girder, sides = 1, and , ");
                        sw.WriteLine();
                    }
                    sw.WriteLine("Let us provide the Web depth = {0}mm, thickness = {1}mm on either side,", dw, tw);
                    sw.WriteLine();
                    sw.WriteLine("Approximate Total Flange area required = Af = (M / (σ_b  x  dw)) - (dw  x  tw / 6)");

                    double Af = (M * 1000000 / (sigma_b * dw)) - (dw * tw / 6.0);


                    sw.WriteLine("                                            = ({0:E3} / ({1}  x  {2})) - ({2}  x  {3} / 6)",
                        (M * 1000000), sigma_b, dw, tw);
                    sw.WriteLine();
                    sw.WriteLine("                                            = {0:G3}  sq.mm", Af);
                    sw.WriteLine();

                    //So, a Box Girder arrangement is considered, 
                    //For Plate Girder, sides =1, and 
                    //For Box Girder, sides =2,     (Chiranjit, Hard Code this values)

                    //Let us provide the Web depth=1600 mm, thickness=20mm on either side, (User given values)

                    //Approximate Total Flange area required =
                    //Af = (M / (σ_b  x  dw)) - (dw  x  tw / 6)
                    //     = (30110  x  10^6) / (165  x  1500))  -  (1500  x  20) / 6
                    //     = 116656.6  sq.mm

                    //sw.WriteLine("Flange width = Bf = S /40 to S / 45");

                    double Bf = 0.0;
                    if ((S * 1000 / 40) > 500 && (S * 1000 / 45) <= 1500)
                    {
                        Bf = 1000.0;
                    }
                    else if ((S * 1000 / 40) > 0 && (S * 1000 / 45) <= 500)
                    {
                        Bf = 500;
                    }
                    else if ((S * 1000 / 40) > 1500 && (S * 1000 / 45) <= 2000)
                    {
                        Bf = 1500;
                    }

                    sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                    //Flange width = Bf = S /40 to S / 45
                    sw.WriteLine("                  = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);
                    sw.WriteLine("                  = {0:f0} to {1:f0}", (S * 1000 / 40), (S * 1000 / 45));
                    sw.WriteLine("                  = {0:f0} mm (Say)", Bf);
                    sw.WriteLine();
                    //             = (46 * 1000) / 40  to  (46 * 1000) / 45
                    //             = 1150  to  1022 mm
                    //             = 1000 mm (Say)

                    //sw.WriteLine("Let us provide {0} Flange plates having Flange width={1} mm, thickness= {2} mm on the Bottom,", nf, Bf, tf);
                    sw.WriteLine("Let us provide {0} Flange plates having Flange width={1} mm, thickness= {2} mm on the Bottom,", nf, bf1, tf1);
                    sw.WriteLine();
                    sw.WriteLine();
                    //sw.WriteLine("and {0} Flange plates having Flange width={1} mm, thickness={2}mm at the Bottom,", nf, bf, tf);
                    sw.WriteLine();
                    //Let us provide 4 Flange plates having Flange width=1000 mm, thickness=20mm on the top,
                    // And 4 Flange plates having Flange width=1000 mm, thickness=20mm at the Bottom, (User given values)

                    Aw = sides * nw * dw * tw;
                    sw.WriteLine("Finally Web Plates  :    sides x nw x Dw x tw, Area = Aw = {0} x {1} x {2} x {3} = {4:f3} Sq. mm", sides, nw, dw, tw, Aw);
                    //Finally Web Plates      sides x nw x Dw x tw, Area = Aw = 2 x 1 x 1600 x 20 = 64,000 Sq. mm
                    Af = bf1 * tf1;
                    double Af2 = bf2 * tf2;
                    sw.WriteLine();
                    sw.WriteLine("Top Flange Plates     :  bf1 x tf1, Area = Af1 = {0} x {1} = {2:f3} Sq. mm", bf1, tf1, Af);
                    sw.WriteLine();
                    sw.WriteLine("Bottom Flange Plates  :  bf2 x tf2, Area = Af2 = {0} x {1} = {2:f3} Sq. mm", bf2, tf2, Af2);
                    //            Flange Plates   sides x nf x Bf x tf, Area = Af = 2 x 4 x 1000 x 20 = 160,000 Sq. mm
                    sw.WriteLine();




                    //sw.WriteLine("Provided size of Web Plate is {0} * {1} * {2} = Aw       Marked as (3) in the Drawing", nw, dw, tw);
                    //sw.WriteLine("Let us try Web as {0} mm * {1} mm = dw * tw = Aw       Marked as (3) in the Drawing", dw, tw);

                    //(3) = Web depth x thickness = 1000 mm * 10 mm

                    _3 = string.Format("Size of Web Plate = {0} * {1} * {2} sq.mm", nw, dw, tw);


                    double i1 = sides * (nw * (tw * dw * dw * dw) / 12.0);


                    double i2 = (1 / 12.0) * (bf1 * Math.Pow((dw + (nf * tf1)), 3.0) - bf1 * dw * dw * dw);
                    double i4 = 0.0;
                    double i3 = ((tab_data.Ixx * 10000) + (tab_data.Area * 100) * (dw - (tab_data.Cxx * tab_data.Cxx * 100)));

                    double I = 0.0;





                    //sw.WriteLine();
                    //sw.WriteLine("Moment of Inertia  = I = (nw * (tw * dw^3) / 12.0) + ");
                    //sw.WriteLine("                         (1 / 12.0) * (bf * (dw + (nf * tf))^3) - bf * dw^3)");
                    //sw.WriteLine("                         (Ixx + a * (dw - (Cxx * Cxx)))");
                    //sw.WriteLine();
                    //sw.WriteLine("                       = ({0} * ({1} * {2}^3) / 12.0) + ", nw, tw, dw);
                    //sw.WriteLine("                         (1 / 12.0) * ({0} * ({1} + ({2} * {3}))^3) - {0} * {1}^3) + ",
                    //                                            bf, dw, nf, tf);
                    //sw.WriteLine("                         ({0} + {1} * ({2} - ({3} * {3})))", tab_data.Ixx * 10000, tab_data.Area * 100, dw, tab_data.Cxx * 10);
                    //sw.WriteLine();

                    sw.WriteLine();
                    sw.WriteLine("Moment of Inertia = I");
                    sw.WriteLine();
                    sw.WriteLine("I = MI of Web ({0}x{1}x{2}) + MI of both Flanges ({3}x{4}x{5}) + MI of Four Connecting Angles ({6}x{7}x{8})", nw, dw, tw, nf, bf1, tf1, na, ang, ang_thk);
                    sw.WriteLine();

                    sw.WriteLine("  = sides * (nw  *  (tw  *  dw^3 )/ 12.0)  ");
                    sw.WriteLine("    +  (bf1 * tf1^3 / 12.0) + (bf1 * tf1 * (dw / 2.0 + tf1 / 2.0)^2.0))");
                    sw.WriteLine("    +  (bf2 * tf2^3 / 12.0) + (bf2 * tf2 * (dw / 2.0 + tf2 / 2.0)^2.0))");
                    sw.WriteLine("    + sides * ( na * (Ixx + a * (dw / 2.0 - Cxx)^2)))");


                    sw.WriteLine();
                    sw.WriteLine("  =  {0} * ({1} *  ({2} * {3}^3 )/ 12.0)", sides, nw, tw, dw);
                    sw.WriteLine("    +  ({0} * {1}^3 / 12.0) + ({0} * {1} * ({2} / 2.0 + {1} / 2.0)^2.0))", bf1, tf1, dw);
                    sw.WriteLine("    +  ({0} * {1}^3 / 12.0) + ({0} * {1} * ({2} / 2.0 + {1} / 2.0)^2.0))", bf2, tf2, dw);
                    sw.WriteLine("    +  {0} * ({1} * ({2}*10^4 + {3} * ({4} / 2.0 - {5})^2)))", sides, na, tab_data.Ixx, tab_data.Area * 100, dw, tab_data.Cxx * 10);
                    sw.WriteLine();
                    sw.WriteLine("Note : Angles are to be provided at Top and Bottom on either side of each Web Plate ");
                    sw.WriteLine();


                    i1 = sides * (nw * (tw * dw * dw * dw) / 12.0);
                    i2 = (bf1 * tf1 * tf1 * tf1 / 12.0) + (bf1 * tf1 * Math.Pow((dw / 2.0 + tf1 / 2.0), 2.0));
                    i3 = (bf1 * tf1 * tf1 * tf1 / 12.0) + (bf1 * tf1 * Math.Pow((dw / 2.0 + tf1 / 2.0), 2.0));

                    i4 = sides * (na * (tab_data.Ixx * 10000 + tab_data.Area * 100 * ((dw / 2.0 - tab_data.Cxx * 10) * (dw / 2.0 - tab_data.Cxx * 10))));



                    I = i1 + i2 + i3 + i4;
                    sw.WriteLine();


                    sw.WriteLine();
                    sw.WriteLine("  = {0:e3} sq.sq.mm", I);

                    double y = dw / 2.0 + tf1;

                    sw.WriteLine();
                    sw.WriteLine();

                    //sw.WriteLine("Approximate Flange area required");
                    // Af = ((M * 10E5) / (sigma_b * dw)) - (Aw / 6);
                    //Af = Double.Parse(Af.ToString("0"));

                    //sw.WriteLine();
                    //sw.WriteLine("Af = (M / (σ_b * dw)) - (Aw / 6)");
                    //sw.WriteLine("   = ({0} * 10E5) / ({1} * {2})) - (dw * tw) / 6", M, sigma_b, dw);
                    //sw.WriteLine("   = ({0} * 10E5) / ({1} * {2})) - ({2} * {3}) / 6", M, sigma_b, dw, tw);
                    //sw.WriteLine("   = {0} sq.mm", Af);
                    sw.WriteLine();

                    //sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                    //sw.WriteLine("             = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);

                    //double Bf1 = S * 1000 / 40.0;
                    //double Bf2 = S * 1000 / 45.0;
                    // Bf = (Bf1 > Bf2) ? Bf1 : Bf2;

                    //Bf = (int)(Bf / 100.0);
                    //Bf += 1;
                    //Bf *= 100.0;

                    //sw.WriteLine("             = {0:f0}  to  {1:f0} mm", Bf1, Bf2);
                    //sw.WriteLine();
                    //sw.WriteLine();






                    //dval = 16 * tw * nw;
                    //sw.WriteLine("Permissible overhang for flange =  16 * tw * nw ");
                    //sw.WriteLine("                                =  16 * {0} * {1} ", tw, nw);
                    //sw.WriteLine("                                =  {0} mm", dval);
                    //sw.WriteLine();

                    //flan_over_hang = (bf - tw) / 2.0;
                    //sw.WriteLine("In present case the flange overhang = (bf - tw) / 2.0");
                    //sw.WriteLine("                                    = ({0} - {1}) / 2.0", bf, tw);
                    //sw.WriteLine("                                    = {0} mm", flan_over_hang);

                    //if (dval > flan_over_hang)
                    //{
                    //    sw.WriteLine();
                    //    sw.WriteLine("Number of Web Please will be equaly divided on Either Side of the Box Girder");
                    //    //sw.WriteLine("Number of Flange Please will be equaly divided on Upper and Lower Side the Box Girder");
                    //    sw.WriteLine("It is suggested to go for Steel Box Girder with 2 webs suitable plates");
                    //    sw.WriteLine();
                    //}
                    Bf = bf1;
                    sw.WriteLine("So, Provided Size of Flange Plate = {0} x {1} x {2} x {3}     Marked as (4) in the Drawing ", sides, nf, Bf, tf1);
                    //(4) = Flange width x thickness = 500 mm * 30 mm

                    _4 = string.Format("Flange Size = {0} * {1} * {2} * {3} ", sides, nf, bf1, tf1);

                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.3 : Check for Maximum Stresses at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();
                    //     and                 y = dw / 2 + (nf x tf)/2
                    //= 1000/2 + (2 x 20)/2
                    //       = 520 mm
                    y = (dw / 2.0 + (nf * tf2) / 2.0);
                    sw.WriteLine("  and   y = dw / 2 + tf2 / 2");
                    sw.WriteLine("          = {0} / 2 + {1} / 2", dw, tf2);
                    sw.WriteLine("          = {0:f3} mm", y);
                    sw.WriteLine();

                    double appl_stress = (M * 1000000 * y) / I;
                    appl_stress = Double.Parse(appl_stress.ToString("0"));
                    sw.WriteLine("Applied Stress = M * y  / I");
                    sw.WriteLine("               = {0:G3} * {1:G3}  / {2:G3}", (M * 1000000), y, I);
                    sw.WriteLine();

                    //sw.WriteLine("Where, I = (tw * dw**3 / 12) + 2 * (tf * Bf) * (dw / 2 + tf /2)^2");
                    //sw.WriteLine("         = ({0} * {1}^3 / 12) + 2 * ({2} * {3}) * ({1} / 2 + {2} /2)^2", tw, dw, tf, Bf);
                    //I = I / 10E6;
                    //I = Double.Parse(I.ToString("0"));
                    //sw.WriteLine("         = {0:G3} sq.sq.mm", I);
                    //sw.WriteLine();
                    //sw.WriteLine();
                    //sw.WriteLine(" and y = dw / 2 + tf");
                    //sw.WriteLine("       = {0} + {1}", (dw / 2.0), tf);
                    //sw.WriteLine("       = {0} mm", y);
                    sw.WriteLine();


                    //sw.WriteLine("So,  Applied Stress = {0} * 10E5 * {1} / ({2:G5})", M, y, I);
                    if (appl_stress < sigma_b)
                    {
                        sw.WriteLine("               = {0:G5} N/sq.mm < σ_b = {1:G5} N/sq.mm, OK", appl_stress, sigma_b);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.3 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("Applied Stress  = {0:G5} N/sq.mm > σ_b = {1:G5} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b));
                        sw.WriteLine("               = {0:G5} N/sq.mm > σ_b = {1:G5} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b);
                    }
                    sw.WriteLine();

                    u_by_B = deg_sh_frc;
                    double V = u_by_B;
                    deg_sh_frc = des_shear;
                    //v = u_by_B;

                    double tau1 = V * 1000 / (dw * tw);
                    tau1 = double.Parse(tau1.ToString("0"));
                    sw.WriteLine("Average Shear Stress = τ1");
                    sw.WriteLine("                     = V * 1000 / (dw * tw)");
                    sw.WriteLine("                     = {0} * 1000 / ({1} * {2})", V, dw, tw);
                    sw.WriteLine("                     = {0} N/sq.mm", tau1);
                    sw.WriteLine();

                    double ratio = (dw / tw);
                    sw.WriteLine("Ratio dw / tw = {0} / {1} = {2}", dw, tw, ratio);
                    sw.WriteLine();
                    sw.WriteLine("Considering Stiffener Spacing = c = dw = {0} mm", dw);
                    sw.WriteLine();

                    // Calculate from Table 1
                    // **Problem How to calculate value from Table1 ?
                    double tau2 = Get_Table_1_Value(100, 1, ref ref_string);
                    //double tau2 = 87;
                    sw.WriteLine("From Table 1 (Given at the end of the Report) : {0} ", ref_string);
                    //sw.WriteLine("Allowable average Shear Stress = {0} N/Sq mm = t2", tau2);
                    sw.WriteLine();

                    if (tau2 > tau1)
                    {
                        sw.WriteLine("Allowable average Shear Stress = {0} N/Sq. mm > {1} N/Sq mm,    OK", tau2, tau1);
                        sw.WriteLine();
                        sw.WriteLine("So, Average shear stress is within Safe permissible Limits.", tau1, tau2);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.3 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("Allowable average Shear Stress = {0} N/Sq. mm < {1} N/Sq mm,   NOT OK", tau2, tau1));
                        sw.WriteLine("Allowable average Shear Stress = {0} N/Sq. mm < {1} N/Sq mm,   NOT OK", tau2, tau1);
                        sw.WriteLine();
                        sw.WriteLine("So, Average shear stress is not within Safe permissible limits.", tau1, tau2);
                    }
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.4 : Connection Between Flange and Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear Force at the junction of Flange and Web is given by");
                    sw.WriteLine();

                    sw.WriteLine("    τ  = V * a * y  / I");
                    double a = bf2 * tf2;
                    Bf = bf2;
                    y = dw / 2.0 + (tf2 / 2.0);
                    //I = double.Parse(I.ToString("0"));

                    sw.WriteLine("    a = bf2 * tf2 = {0} * {1} = {2:f2} sq.mm", bf2, tf2, a);
                    sw.WriteLine();
                    sw.WriteLine("    y = dw/2 + tf2/2 = {0}/2 + {1}/2 = {2} mm", dw, tf2, y);
                    sw.WriteLine();
                    sw.WriteLine("    I = {0:G5} sq.sq.mm", I);
                    sw.WriteLine();
                    sw.WriteLine("    V = {0} * 1000 N", V);
                    sw.WriteLine();

                    //tau = (V * 1000 * a * y) / (I * 10E6);
                    tau = (V * 1000 * a * y) / (I);
                    //tau = double.Parse(tau.ToString("0"));
                    //sw.WriteLine("τ = 548 * 1000 * 15000 * 515 / (879 * 107) = 483 N/mm");
                    sw.WriteLine("    τ  = {0} * 1000 * {1} * {2}  / ({3:G5})", V, a, y, I);
                    sw.WriteLine("       = {0:G5} N/mm", tau);
                    sw.WriteLine();

                    sw.WriteLine("Adopting Continuous weld on either side, strength of weld of size ");
                    sw.WriteLine();
                    sw.WriteLine("  ‘S’ = 2 * k * S * σ_tf");

                    double _S = 2 * K * sigma_tf;
                    _S = double.Parse(_S.ToString("0"));
                    sw.WriteLine("      = 2 * {0} * S * {1}", K, sigma_tf);
                    sw.WriteLine("      = {0:G5} * S", _S);
                    sw.WriteLine();


                    sw.WriteLine("Equating, {0} * S = {1:G5},                S = {1:G5} / {0} = {2:G5} mm", _S, tau, (tau / _S));
                    sw.WriteLine();

                    _S = tau / +_S;

                    _S = (int)_S;
                    _S += 2;

                    sw.WriteLine("Use {0} mm Fillet Weld, continuous on either side.     Marked as (5) in the Drawing", _S);
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.5 : Intermediate Stiffeners at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    double val1, val2;

                    val1 = dw / tw;
                    if (val1 < 85)
                    {
                        sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                        sw.WriteLine("So, Vertical Stiffeners are required");
                        //sw.WriteLine("else, Vertical Stiffeners are not required.");
                    }
                    else
                    {
                        sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                        //sw.WriteLine("So, Vertical Stiffeners are required");
                        sw.WriteLine("So, Vertical Stiffeners are not required.");
                    }
                    sw.WriteLine();

                    double sp_stifn1 = 0.33 * dw;
                    double sp_stifn2 = 1.5 * dw;
                    sw.WriteLine("Spacing of Stiffeners = 0.33 * dw  to  1.5 * dw");
                    sw.WriteLine("            = 0.33 * {0}  to  1.5 * {0}", dw);
                    sw.WriteLine("            = {0} mm to {1} mm", sp_stifn1, sp_stifn2);
                    sw.WriteLine();

                    double c = 1000;

                    sw.WriteLine("Adopt Spacing = c = {0} mm", c);
                    sw.WriteLine();


                    sw.WriteLine("Required minimum Moment of Inertia of Stiffeners");
                    sw.WriteLine();


                    double _I = ((1.5 * dw * dw * dw * tw * tw * tw) / (c * c));
                    sw.WriteLine("I = 1.5 * dw**3 * tw**3 / c**2");
                    sw.WriteLine("  = 1.5 * {0}^3 * {1}^3 / {2}^2", dw, tw, c);
                    //_I = double.Parse(_I.ToString("0"));
                    sw.WriteLine("  = {0:E3} sq.sq.mm", _I);
                    sw.WriteLine();
                    _I = _I / 10E4;

                    double t = 10; // t is Constant?

                    sw.WriteLine("Use {0} mm thick plate, t = {0} mm", t);
                    sw.WriteLine();
                    sw.WriteLine("Maximum width of plate not to exceed = 12 * t = {0} mm", (12 * t));
                    sw.WriteLine();

                    // 80 ?
                    double h = 80;
                    sw.WriteLine("Use 80 mm size plate, h = 80 mm");
                    sw.WriteLine();
                    sw.WriteLine("Plate size is {0} mm * {1} mm      Marked as (6) in the Drawing", h, t);
                    _6 = string.Format("{0} mm x {1} mm", h, t);



                    sw.WriteLine();

                    double _I1 = (t * (h * h * h)) / 3.0;
                    //_I1 = _I1 / 10E4;
                    //_I1 = double.Parse(_I1.ToString("0"));

                    if (_I1 > _I)
                    {
                        sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm > {2:e2} sq.sq.mm,                OK", t, _I1, _I);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.5 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I));
                        DesignResult.Add("");
                        sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I);
                    }
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.6 : Connections of Vertical Stiffener to Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Shear on weld connecting stiffener to Web");
                    sw.WriteLine();

                    // 125 = constant ?
                    double sh_wld_wb = 125 * tw * tw / h;
                    sw.WriteLine("    = 125 * tw*tw / h");
                    sw.WriteLine("    = 125 * {0}*{0} / {1}", tw, h);

                    sh_wld_wb = double.Parse(sh_wld_wb.ToString("0.00"));
                    sw.WriteLine("    = {0} kN/m", sh_wld_wb);
                    sw.WriteLine("    = {0} N/mm", sh_wld_wb);
                    sw.WriteLine();

                    double sz_wld = sh_wld_wb / (K * sigma_tf);
                    sz_wld = double.Parse(sz_wld.ToString("0.00"));
                    sw.WriteLine("Size of welds = 156.25 / (K * σ_tf)");
                    sw.WriteLine("              = {0} / ({1} * {2})", sh_wld_wb, K, sigma_tf);
                    sw.WriteLine("              = {0} mm", sz_wld);
                    sw.WriteLine();

                    //sw.WriteLine("Size of welds = 156.25 / (K * σtf) = 156.25 / (0.7 * 102.5) = 2.17 mm");
                    // How come 100 and 5?
                    sw.WriteLine("Use 100 mm Long 5 mm Fillet Welds alternately on either side.     Marked as (7) in the Drawing");

                    //(7)  5-100-100 (weld)
                    _7 = string.Format("5-100-100 (weld)");
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.7 : End Bearing Stiffeners at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear Force = V = {0} kN", V);
                    sw.WriteLine();

                    val1 = (h / t);
                    if (val1 < 12)
                    {
                        sw.WriteLine("The end bearing Stiffeners is designed as a column h / t < 12");
                    }
                    else
                    {
                        sw.WriteLine("The end bearing Stiffeners is designed as a column h / t > 12");
                    }
                    sw.WriteLine();
                    h = 180;
                    sw.WriteLine("Use ‘h’ = outstand of stiffeners = {0} mm", h);
                    sw.WriteLine();
                    t = h / 12;
                    sw.WriteLine("t = {0} / 12 = {1} mm", h, (h / 12.0));
                    sw.WriteLine();
                    sw.WriteLine("Use plate of size 180 mm * 15 mm     Marked as (8) in the Drawing");

                    //(8)  180 x15 mm

                    _8 = string.Format("180 x 15 mm");
                    sw.WriteLine();

                    double brng_ar_req = V * 1000 / sigma_p;
                    sw.WriteLine();
                    sw.WriteLine("Bearing area required = V * 1000 / σ_p");
                    sw.WriteLine("                      = {0} * 1000 / {1} sq.mm", V, sigma_p);
                    brng_ar_req = double.Parse(brng_ar_req.ToString("0"));
                    sw.WriteLine("                      = {0} sq.mm", brng_ar_req);
                    sw.WriteLine();

                    double tot_area = 2 * h * t;
                    sw.WriteLine("If two plates are used,");
                    sw.WriteLine("     Total area = 2 * {0} * {1}", h, t);
                    if (tot_area > brng_ar_req)
                    {
                        sw.WriteLine("                = {0} sq.mm > {1} sq.mm", tot_area, brng_ar_req);
                    }
                    else
                    {
                        sw.WriteLine("                = {0} sq.mm < {1} sq.mm", tot_area, brng_ar_req);
                    }
                    sw.WriteLine();

                    sw.WriteLine("The length of Web plate which acts along with Stiffener ");
                    sw.WriteLine("plates in bearing the reaction = lw = 20 * tw");
                    sw.WriteLine("                               = 20 * {0}", tw);
                    double brng_reaction = 20 * tw;
                    double lw = brng_reaction;
                    sw.WriteLine("                               = {0} mm", lw);
                    sw.WriteLine();
                    _I = ((t * (2 * h + 10) * (2 * h + 10) * (2 * h + 10)) / 12) + (2 * lw * tw * tw * tw / 12);

                    //**lw = ?
                    sw.WriteLine("    I = t * (2 * h + 10)^3 / 12 + 2 * lw * tw**3 / 12");
                    sw.WriteLine("      = {0} * (2 * {1} + 10)^3 / 12 + 2 * {2} * {3}^3 / 12", t, h, 200, tw);
                    //sw.WriteLine("      = 15 * 3703 / 12 + 2 * 200 * 103 / 12");
                    _I = (_I / 10E3);
                    _I = double.Parse(_I.ToString("0"));

                    sw.WriteLine("      = {0} * 10E3 Sq Sq mm", _I);
                    sw.WriteLine();

                    double A = 2 * h * t + 2 * lw * tw;
                    A = double.Parse(A.ToString("0"));
                    sw.WriteLine("    Area = A = 2 * h * t + 2 * lw * tw");
                    sw.WriteLine("         = 2 * {0} * {1} + 2 * {2} * {3}", h, t, lw, tw);
                    sw.WriteLine("         = {0} sq.mm", A);

                    sw.WriteLine();

                    double r = (_I * 10E3) / A;
                    r = Math.Sqrt(r);
                    r = double.Parse(r.ToString("0"));
                    sw.WriteLine("    r = √(I / A) = √({0} * 10E3 / {1}) = {2} mm", _I, A, r);
                    sw.WriteLine();


                    // ** 0.7 = ?
                    double _L = 0.7 * dw;
                    double lamda = (_L / r);
                    lamda = double.Parse(lamda.ToString("0.00"));
                    sw.WriteLine("    λ = Slenderness ratio = L / r");
                    sw.WriteLine();
                    sw.WriteLine("    L = Effective Length of stiffeners");
                    sw.WriteLine("      = 0.7 * dw");
                    sw.WriteLine("      = 0.7 * {0}", tw);
                    sw.WriteLine("      = {0} mm", _L);
                    sw.WriteLine();
                    sw.WriteLine("    λ = {0} / {1}", _L, r);
                    sw.WriteLine("      = {0}", lamda);

                    sw.WriteLine();

                    double sigma_ac = Get_Table_2_Value(lamda, 1, ref ref_string);
                    sw.WriteLine("    From Table 2 (given at the end of the Report) : {0}", ref_string);

                    sigma_ac = double.Parse(sigma_ac.ToString("0"));
                    sw.WriteLine("    Permissible Stress in axial compression σ_ac = {0} N/sq.mm", sigma_ac);
                    sw.WriteLine();

                    double area_req = V * 1000 / sigma_ac;
                    area_req = double.Parse(area_req.ToString("0"));
                    sw.WriteLine("    Area required = V * 1000 / σ_ac ");
                    sw.WriteLine("                  = {0} * 1000 / {1}", V, sigma_ac);
                    if (area_req < A)
                    {
                        sw.WriteLine("                  = {0} sq.mm < {1} sq.mm,  Ok", area_req, A);
                    }
                    else
                    {
                        sw.WriteLine("                  = {0} sq.mm > {1} sq.mm,  NOT OK", area_req, A);
                    }
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.8 : Connection between Bearing Stiffener and Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    //** 40 = ?
                    double len_alt = 2 * (dw - 40);
                    sw.WriteLine("Length available for alternate intermittent weld");
                    sw.WriteLine("   = 2 * (dw - 40)");
                    sw.WriteLine("   = 2 * ({0} - 40)", dw);
                    sw.WriteLine("   = {0} mm", len_alt);
                    sw.WriteLine();

                    double req_strnth_wld = (v * 1000 / len_alt);
                    req_strnth_wld = double.Parse(req_strnth_wld.ToString("0"));
                    sw.WriteLine("Required strength of weld = v * 1000 / 1920");
                    sw.WriteLine("                          = {0} * 1000 / {1}", v, len_alt);
                    sw.WriteLine("                          = {0} N/mm", req_strnth_wld);
                    sw.WriteLine();

                    sz_wld = req_strnth_wld / (K * sigma_tf);
                    sz_wld = double.Parse(sz_wld.ToString("0.00"));


                    //** σ_ac =  138 but 102.5 = ?
                    //sw.WriteLine("Size of weld = 286 / (K * σ_ac) = 286 / (0.7 * 102.5) = 3.98 mm");
                    sw.WriteLine("Size of weld = 286 / (K * σ_tf)");
                    sw.WriteLine("             = {0} / ({1} * {2})", req_strnth_wld, K, sigma_tf);
                    sw.WriteLine("             = {0} mm", sz_wld);
                    sw.WriteLine();

                    if (sz_wld < 5)
                        sz_wld = 5;
                    else
                    {
                        sz_wld = (int)sz_wld;
                        sz_wld += 1;
                    }
                    sw.WriteLine("Use {0} mm Fillet Weld", sz_wld);
                    sw.WriteLine();

                    double len_wld = 10 * tw;

                    sw.WriteLine("Length of Weld >= 10 * tw = 10 * {0} = {1} mm", tw, len_wld);
                    sw.WriteLine();

                    sw.WriteLine("Use {0} mm Long, {1} mm Weld Alternately.     Marked as (9) in the Drawing", len_wld, sz_wld);
                    sw.WriteLine();


                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.9 : Properties of Composite Section at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    double Ace = B * 1000 * D / m;
                    Ace = double.Parse(Ace.ToString("0"));

                    sw.WriteLine("Ace = B * 1000 * D/m");
                    sw.WriteLine("    = {0} * 1000 * {1}/{2}", B, D, m);
                    sw.WriteLine("    = {0} sq.mm", Ace);
                    sw.WriteLine();
                    sw.WriteLine("The centroid of Composite Section (Neutral Axis) is determined");
                    sw.WriteLine("by first moment of the areas about axis xx,");

                    Bf = bf1;

                    double Axy = Ace * (dw + 2 * tf1 + D / 2) + Bf * tf1 * (dw + tf1 + tf1 / 2) + dw * tw * (dw / 2 + tf1) + Bf * tf1 * tf1 / 2;

                    sw.WriteLine();
                    sw.WriteLine("Axy  = Ace * (dw + 2 * tf1 + D/2) + bf1 * tf1 * (dw + tf1 + tf1/2) ");
                    sw.WriteLine("       + dw * tw * (dw/2 +tf1) + Bf * tf1 * tf1/2");
                    sw.WriteLine();
                    sw.WriteLine("     = {0} * ({1} + 2 * {2} + {3}/2) + {4} * {2} * ({1} + {2} + {2}/2) ", Ace, dw, tf1, D, Bf);
                    sw.WriteLine("       + {0} * {1} * ({0}/2 + {2}) + {3} * {2} * {2}/2", dw, tw, tf1, Bf);
                    sw.WriteLine();

                    Axy = double.Parse(Axy.ToString("0"));
                    sw.WriteLine("     = {0}", Axy);
                    //sw.WriteLine("= 77046340");
                    double _A_d = Ace + (dw / 2) * tf1 + dw * tw + (dw / 2.0) * tf1;
                    _A_d = double.Parse(_A_d.ToString("0"));
                    // ** formula ?
                    sw.WriteLine();
                    sw.WriteLine("A = Ace + (dw / 2) * tf1 + dw * tw + (dw / 2.0) * tf1");
                    sw.WriteLine("  = {0} + ({1} / 2) * {2} + {0} * {3} + ({1} / 2.0) * {2}", Ace, dw, tf1, tw);
                    sw.WriteLine("  = {0} sq.mm", _A_d);
                    sw.WriteLine();

                    y = Axy / _A_d;
                    // ** sign y bar ?
                    sw.WriteLine("  y = Axy / A = {0} / {1}", Axy, _A_d);
                    //sw.WriteLine("    = {0:f0}", y);

                    y = double.Parse(y.ToString("0"));
                    sw.WriteLine("    = {0} mm", y);
                    sw.WriteLine();

                    double yc = dw + 2 * tf1 + D / 2 - y;

                    sw.WriteLine("  yc = dw + 2 * tf1 + D/2 -  y");
                    sw.WriteLine("     = {0} + 2 * {1} + {2}/2 -  {3}", dw, tf1, D, y);
                    sw.WriteLine("     = {0} mm", yc);
                    sw.WriteLine();


                    double Icomp = Ace * yc * yc +
                        (Bf * (dw + (2 * tf1)) * (dw + (2 * tf1)) * (dw + (2 * tf1))) / 12.0
                        - ((Bf - tw) * dw * dw * dw) / 12.0 +
                        (_A_d - Ace) * (y - (dw / 2.0) - tf1) * (y - (dw / 2.0) - tf1);


                    sw.WriteLine("Icomp = distance from centre of Deck Slab to Centroid of Composite Section");

                    sw.WriteLine("      = Ace * yc * yc ");
                    sw.WriteLine("        + (bf1 * (dw + (2 * tf1))^3 ) / 12.0");
                    sw.WriteLine("        - ((bf1 - tw) * dw**3) / 12.0 ");
                    sw.WriteLine("        + (A - Ace) * (y - (dw / 2.0) - tf1) * (y - (dw / 2.0) - tf1)");
                    sw.WriteLine();

                    sw.WriteLine("      = {0} * {1} * {1} ", Ace, yc);
                    sw.WriteLine("        + ({0} * ({1} + (2 * {2}))^3 ) / 12.0", Bf, dw, tf1);
                    sw.WriteLine("        - (({0} - {1}) * {2}^3) / 12.0 ", Bf, tw, dw);
                    sw.WriteLine("        + ({0} - {1}) * ({2} - ({3} / 2.0) - {4}) * ({2} - ({3} / 2.0) - {4})", _A_d, Ace, y, dw, tf1);
                    sw.WriteLine();




                    Icomp = Icomp / 10E9;
                    Icomp = double.Parse(Icomp.ToString("0.000"));
                    sw.WriteLine("      = {0} * 10E9 sq.sq.mm", Icomp);
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear force at junction of Slab and Girder is obtained by");

                    tau = (v * 1000 * Ace * yc) / (Icomp * 10E9);
                    sw.WriteLine("τ = v * 1000 * Ace *  yc / Icomp");
                    sw.WriteLine("  = {0} * 1000 * {1} * {2} / {3} * 10E9", v, Ace, yc, Icomp);
                    tau = double.Parse(tau.ToString("0"));
                    sw.WriteLine("  = {0} N/mm", tau);
                    sw.WriteLine();

                    double Q1 = tau * Bf;
                    Q1 = double.Parse(Q1.ToString("0"));
                    sw.WriteLine("Total Shear force at junction Q1 =  τ * Bf1 ");
                    sw.WriteLine("                                 =  {0} * {1}", tau, Bf);
                    sw.WriteLine("                                 =  {0} N", Q1);
                    sw.WriteLine();

                    double _do = 20.0;
                    sw.WriteLine("Using do = {0} mm diameter mild steel studs,     Marked as (10) in the Drawing", _do);
                    _10 = string.Format("{0} Ø Studs", _do);

                    sw.WriteLine("capacity of one shear connector is given by,");
                    sw.WriteLine();
                    // 196 = ?
                    double Q2 = 196 * _do * _do * Math.Sqrt(fck);
                    Q2 = double.Parse(Q2.ToString("0"));
                    sw.WriteLine("    Q2 = 196 * do*do *  √fck");
                    sw.WriteLine("       = 196 * {0}*{0} *  √{1}", _do, fck);
                    sw.WriteLine("       = {0} N", Q2);
                    sw.WriteLine();

                    // 5 = ?
                    double H = 5 * 20;
                    sw.WriteLine("Height of each stud = H");
                    sw.WriteLine("                    = 5 * do");
                    sw.WriteLine("                    = 5 * {0}", _do);
                    sw.WriteLine("                    = {0} mm", H);
                    sw.WriteLine();

                    double no_std_row = (Q1 / Q2);
                    no_std_row = double.Parse(no_std_row.ToString("0.00"));
                    sw.WriteLine("Number of studs required in a row");
                    sw.WriteLine();
                    if (no_std_row < 1.0)
                    {
                        sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} < 1", Q1, Q2, no_std_row);
                    }
                    else
                    {
                        sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} > 1", Q1, Q2, no_std_row);
                    }
                    sw.WriteLine("So, Provide a minimum of 2 mild Steel Studs in a row");
                    sw.WriteLine();

                    double N = 2;
                    double fs = 2.0;
                    double p = N * Q2 / (fs * tau);
                    p = double.Parse(p.ToString("0"));
                    sw.WriteLine("Pitch of Shear Connectors = p = N * Q2 / (fs * τ)");

                    sw.WriteLine("N = Number of Shear Connectors in a row = 2");
                    sw.WriteLine();
                    sw.WriteLine("Fs = Factor of Safety = 2.0");
                    sw.WriteLine();
                    sw.WriteLine("p = 2 * {0} / (2 * {1})", Q2, tau);
                    sw.WriteLine("  = {0} mm", p);
                    sw.WriteLine();

                    sw.WriteLine("Maximum permissible pitch is the lowest value of:");
                    sw.WriteLine();
                    sw.WriteLine("(i)     3 * Thickness of Slab = 3 * {0} = {1:f0} mm", D, (3 * D));
                    sw.WriteLine("(ii)    4 * Height of Stud = 4 * (5 * do) = 4 * {0:f0} = {1:f0} mm", (5 * _do), (4 * 5 * _do));
                    sw.WriteLine("(iii)   600 mm");
                    sw.WriteLine();
                    sw.WriteLine("Hence provide the pitch of 400 mm in the longitudinal direction.    Marked as (11) in the Drawing");

                    #endregion
                    step++;
                }
                while (step <= 3);



                #region STEP 2 : COMPUTATION OF Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0} : DESIGN OF RCC DECK SLAB", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.1 : COMPUTATION OF PERMANENT LOAD", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double self_weight_deck_slab = (D / 1000.0) * gamma_c;

                sw.WriteLine("Self weight of Deck Slab = (D/1000) * γ_c");
                sw.WriteLine("                         = ({0:f2}) * {1:f0}", (D / 1000), gamma_c);
                sw.WriteLine("                         = {0:f2} kN/sq.mm", self_weight_deck_slab);
                sw.WriteLine();

                double self_weight_wearing_course = (Dwc / 1000.0) * gamma_wc;
                sw.WriteLine("Self weight of wearing course = (Dwc/1000) * γ_wc");
                sw.WriteLine("                              = {0:f2} * {1}", (Dwc / 1000), gamma_wc);
                sw.WriteLine("                              = {0:f2} kN/sq.mm", self_weight_wearing_course);
                sw.WriteLine();
                double DL = self_weight_deck_slab + self_weight_wearing_course;

                sw.WriteLine("Total Load = DL ");
                sw.WriteLine("           = {0:f2} + {1:f2}", self_weight_deck_slab, self_weight_wearing_course);
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                DL = (int)DL;
                DL += 1.0;
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                #endregion

                #region STEP 2.2 : BENDING MOMENT BY MOVING LOAD
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.2 : BENDING MOMENT BY MOVING LOAD", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Load = WL = {0} kN", WL);
                sw.WriteLine("Panel Dimension L = {0:f2} m,                B = {1:f2} m", L, B);
                sw.WriteLine("Load Dimension v = {0:f2}, u = {1:f2} m", v, u);
                sw.WriteLine();
                sw.WriteLine("Considering 45° Load dispersion through wearing Course");
                sw.WriteLine();

                double _v = v + (2 * (Dwc / 1000.0));
                sw.WriteLine("    v = {0:f2} + (2*{1:f2}) = {2:f2} m.", v, (Dwc / 1000.0), _v);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _v);
                double _u = u + (2 * (Dwc / 1000.0));
                sw.WriteLine("    u = {0:f2} + (2*{1:f2}) = {2:f2} m.", u, (Dwc / 1000.0), _u);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _u);


                u_by_B = v;
                v = _v;
                _v = u_by_B;

                u_by_B = u;
                u = _u;
                _u = u_by_B;



                u_by_B = u / B;
                sw.WriteLine("    u / B = {0:f2} / {1:f2} = {2:f3}", u, B, u_by_B);
                sw.WriteLine();
                double v_by_L = v / L;

                sw.WriteLine("    v / L = {0:f2} / {1:f2} = {2:f3}", v, L, v_by_L);
                sw.WriteLine();

                double k = B / S;
                sw.WriteLine("    K = B / S = {0:f2} / {1:f2} = {2:f3}", B, L, k);
                sw.WriteLine();


                k = Double.Parse(k.ToString("0.0"));
                if (k < 0.4)
                    k = 0.4;
                if (k > 1.0) k = 1.0;
                f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);
                //if (k == 0.4)
                //{
                f_c.txt_m1.Text = "0.085";
                f_c.txt_m2.Text = "0.017";
                //}
                f_c.ShowDialog();
                double m1, m2;
                m1 = f_c.m1;
                m2 = f_c.m2;

                sw.WriteLine();
                sw.WriteLine("From Pigeaud’s Curves, for K = {0:f1}", k);
                sw.WriteLine("    m1 = {0}", m1);
                sw.WriteLine("    m2 = {0}", m2);

                double _MB = WL * (m1 + 0.15 * m2);
                _MB = double.Parse(_MB.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WL * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0} * ({1} + 0.15 * {2})", WL, m1, m2);
                sw.WriteLine("                          = {0} kN-m", _MB);
                sw.WriteLine();

                double MB1 = IF * CF * _MB;
                MB1 = double.Parse(MB1.ToString("0"));

                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = MB1");
                sw.WriteLine("  = IF * CF * MB’ ");
                sw.WriteLine("  = {0} * {1:f2} * {0:f2} ", IF, CF, _MB);
                sw.WriteLine("  = {0} kN-m", MB1);
                sw.WriteLine();

                double _ML = WL * (m2 + 0.15 * m1);

                sw.WriteLine("Long Span Bending Moment = ML’ ");
                sw.WriteLine("                         = WL * (m2 + 0.15 * m1) ");
                sw.WriteLine("                         = {0} * ({1} + 0.15 * {2}) ", WL, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML1 = IF * CF * _ML;
                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = ML1");
                sw.WriteLine("  = IF * CF * ML’ ");
                sw.WriteLine("  = {0} * {1} * {2:f2} ", IF, CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML1);
                sw.WriteLine();
                #endregion

                #region STEP 2.3 : BENDING MOMENT BY Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.3 : BENDING MOMENT BY PERMANENT LOAD", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Permanent Load of Deck Slab = DL = {0} kN/sq.mm", DL);

                double WD = DL * B * L;
                sw.WriteLine("Permanent Load per Panel = WD");
                sw.WriteLine("                     = DL * B * L");
                sw.WriteLine("                     = {0} * {1} * {2}", DL, B, L);
                sw.WriteLine("                     = {0:f2} kN", WD);
                sw.WriteLine();
                sw.WriteLine("u / B = 1 and  v / L = 1");

                k = B / L;
                k = Double.Parse(k.ToString("0.000"));
                sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f1}", B, L, k);
                sw.WriteLine("1/k = 1 / {0} = {1:f2}", k, (1 / k));

                f_c = new frmCurve(k, 1.0, 1.0, LoadType.FullyLoad);

                k = Double.Parse(k.ToString("0.0"));
                //if (k == 0.4)
                //{
                f_c.txt_m1.Text = "0.047";
                f_c.txt_m2.Text = "0.006";
                //}
                f_c.ShowDialog();

                m1 = f_c.m1;
                m2 = f_c.m2;
                double MB, ML;

                sw.WriteLine();
                sw.WriteLine("Using Pigeaud’s Curves, m1 = {0} and m2 = {1}", m1, m2);
                sw.WriteLine();
                _MB = WD * (m1 + 0.15 * m2);
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WD * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0:f2} * ({1} + 0.15 * {2})", WD, m1, m2);
                sw.WriteLine("                          = {0:f2} kN-m", _MB);
                sw.WriteLine();


                sw.WriteLine("Short Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = MB2");

                double MB2 = CF * _MB;
                sw.WriteLine("  = CF * MB’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _MB);
                sw.WriteLine("  = {0:f2} kN-m", MB2);
                sw.WriteLine();

                _ML = WD * (m2 + 0.15 * m1);
                sw.WriteLine("Long Span Bending Moment = ML’");
                sw.WriteLine("                         = WD * (m2 + 0.15 * m1)");
                sw.WriteLine("                         = {0:f2} * ({1} + 0.15 * {2})", WD, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML2 = CF * _ML;
                sw.WriteLine("Long Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = ML2");
                sw.WriteLine("  = CF * ML’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine("Design Bending Moments are:");
                MB = MB1 + MB2;

                sw.WriteLine("Along Short Span = MB");
                sw.WriteLine("                 = MB1 + MB2");
                sw.WriteLine("                 = {0:f2} + {1:f2}", MB1, MB2);
                sw.WriteLine("                 = {0:f2} kN-m", MB);
                sw.WriteLine();


                ML = ML1 + ML2;
                sw.WriteLine("Along Long Span = ML");
                sw.WriteLine("                = ML1 + ML2");
                sw.WriteLine("                = {0:f2} + {1:f2}", ML1, ML2);
                sw.WriteLine("                = {0:f2} kN-m", ML);
                sw.WriteLine();

                #endregion

                #region STEP 2.4 : DESIGN OF SECTION FOR RCC DECK SLAB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.4 : STRUCTURAL DETAILING FOR RCC DECK SLAB", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();



                double d = (MB * 10E5) / (Q * 1000.0);

                d = Math.Sqrt(d);
                d = double.Parse(d.ToString("0"));
                sw.WriteLine("d = √((MB * 10E5) / (Q*b))");
                sw.WriteLine("  = √(({0:f2} * 10E5) / ({1:f3}*1000))", MB, Q);
                sw.WriteLine("  = {0} mm", d);
                sw.WriteLine();

                sw.WriteLine("The overall depth of RCC Deck Slab = {0} mm", D);

                double _d = d;
                d = D - 40.0;
                sw.WriteLine();
                sw.WriteLine("Effective Depth = {0} - 40 = {1} mm = d", D, d);


                double Ast = MB * 10E5 / (sigma_st * j * d);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Required steel along short span");
                sw.WriteLine("    = Ast");
                sw.WriteLine("    = (MB * 10E5) / (σ_st * j * d)");
                sw.WriteLine("    = ({0:f2} * 10E5) / ({1} * {2} * {3})", MB, sigma_st, j, d);
                sw.WriteLine("    = {0} sq.mm", Ast);

                List<double> lst_dia = new List<double>();

                lst_dia.Add(10);
                lst_dia.Add(12);
                lst_dia.Add(16);
                lst_dia.Add(20);
                lst_dia.Add(25);
                lst_dia.Add(32);


                int dia_indx = 0;
                double dia = lst_dia[0];
                double _ast = 0.0;
                double no_bar = 0.0;
                double spacing = 140;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0} mm bars at {1:f0} mm c/c.     Marked as (1) in the Drawing", dia, spacing);
                //(1) = T12 mm bars at 140 mm c/c.
                _1 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                sw.WriteLine();

                sw.WriteLine("Effective depth for Long span using T10 mm bars");
                sw.WriteLine();

                double d1 = d - (dia / 2.0) - (10.0 / 2.0);
                sw.WriteLine("    d1 = d - ({0:f0}/2) - (10/2)", dia);
                sw.WriteLine("       = {0} - {1:f0} - 5", d, (dia / 2.0));
                sw.WriteLine("       = {0:f0} mm", d1);
                sw.WriteLine();

                Ast = (ML * 10E5) / (sigma_st * j * d1);
                Ast = double.Parse(Ast.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Required steel along long span");
                sw.WriteLine("  = Ast");
                sw.WriteLine("  = ML * 10E5 / (σ_st * j * d1)");
                sw.WriteLine("  = {0:f2} * 10E5 / ({1} * {2} * {3})", ML, sigma_st, j, d1);
                sw.WriteLine("  = {0} sq.mm", Ast);
                sw.WriteLine();

                spacing = 150;
                dia_indx = 0;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine("Provide T{0:f0} Bars at {1:f0} mm c/c.    Marked as (2) in the Drawing", dia, spacing);
                //(2) = T10 Bars at 150 mm c/c.
                _2 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                #endregion



                sw.WriteLine();
                sw.WriteLine();
                Write_Table_1(sw);
                Write_Table_2(sw);

                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
                //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
                //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
                //sw.WriteLine("-----------------------------------------------------------------------");

                //sw.WriteLine("110    87       87       87      87       87      87      87");
                //sw.WriteLine("130    87       87       87      87       87      84      82");
                //sw.WriteLine("150    87       87       87      85       80      77      75");
                //sw.WriteLine("170    87       87       83      80       76      72      70");
                //sw.WriteLine("190    87       87       79      75");
                //sw.WriteLine("200    87       85       77");
                //sw.WriteLine("220    87       80       73");
                //sw.WriteLine("240    87       77");
                //sw.WriteLine("-----------------------------------------------------------------------");


                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
                //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
                //sw.WriteLine("λ= (L/r)  ______________________________________");
                //sw.WriteLine("           236        299        331       362");
                //sw.WriteLine("---------------------------------------------------");
                //sw.WriteLine("0         140.0      171.2       191.5    210.0");
                //sw.WriteLine("20        136.0      167.0       186.0    204.0");
                //sw.WriteLine("40        130.0      157.0       174.0    190.0");
                //sw.WriteLine("60        118.0      139.0       151.6    162.0");
                //sw.WriteLine("80        101.0      113.5       120.3    125.5");
                //sw.WriteLine("100        80.5       87.0        90.2     92.7");
                //sw.WriteLine("120        63.0       66.2        68.0     69.0");
                //sw.WriteLine("140        49.4       51.2        52.0     52.6");
                //sw.WriteLine("160        39.0       40.1        40.7     41.1");
                //sw.WriteLine("---------------------------------------------------");





                if (DesignSummery.Count != 0)
                {
                    sw.WriteLine("---------------------------------------------------------------------------");
                    sw.WriteLine("---------------------       DESIGN SUMMARY       --------------------------");
                    sw.WriteLine("---------------------------------------------------------------------------");
                    sw.WriteLine();
                    //sw.WriteLine("BRIDGE SPAN = L = 46.000m. Depth of Girder = Deff = 1.5m.");
                    sw.WriteLine("BRIDGE SPAN = L = {0:f3}m. Depth of Girder = Deff = {1:f3}m.", S, deff);
                    if (IsBoxArrangement)
                    {
                        sw.WriteLine("A Steel Box Girder arrangement is selected,");
                        sw.WriteLine("Providing number of Web Plates as mentioned below on either side of the Box,");
                    }
                    else
                    {
                        sw.WriteLine("A Steel Plate Girder arrangement is selected,");
                        sw.WriteLine("Providing number of Web Plates as mentioned below at the Centre of the Girder, in between Flanges,");

                    }

                    sw.WriteLine();





                    foreach (string s in DesignSummery)
                    {
                        sw.WriteLine(s);
                    }
                    //sw.WriteLine();
                    //sw.WriteLine("---------------------------------------------------------------------------");
                    //sw.WriteLine("---------------------   END OF DESIGN SUMMARY    --------------------------");
                    //sw.WriteLine("---------------------------------------------------------------------------");
                    //sw.WriteLine();

                }
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine("---------------------        DESIGN RESULT       --------------------------");
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine();
                sw.WriteLine("DESIGN RESULT : ");

                if (DesignResult.Count != 0)
                {
                    foreach (string s in DesignResult)
                    {
                        sw.WriteLine(s);
                    }
                }
                else
                    sw.WriteLine("DESIGN IS FOUND OK");
                sw.WriteLine();
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine("---------------------    END OF DESIGN RESULT    --------------------------");
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine();

                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.Flush();
                sw.Close();
            }
        }

        #region IReport Members

        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("S = {0}", S);
                sw.WriteLine("B1 = {0}", B1);
                sw.WriteLine("B2 = {0}", B2);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine("fy = {0}", fy);
                sw.WriteLine("m = {0}", m);
                sw.WriteLine("YS = {0}", YS);
                sw.WriteLine("D = {0}", D);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("Dwc = {0}", Dwc);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("gamma_wc = {0}", gamma_wc);
                sw.WriteLine("WL = {0}", WL);
                sw.WriteLine("v = {0}", v);
                sw.WriteLine("u = {0}", u);
                sw.WriteLine("IF = {0}", IF);
                sw.WriteLine("CF = {0}", CF);
                sw.WriteLine("Q = {0}", Q);
                sw.WriteLine("j = {0}", j);
                sw.WriteLine("sigma_st = {0}", sigma_st);
                sw.WriteLine("sigma_b = {0}", sigma_b);
                sw.WriteLine("tau = {0}", tau);
                sw.WriteLine("sigma_tf = {0}", sigma_tf);
                sw.WriteLine("K = {0}", K);
                sw.WriteLine("sigma_p = {0}", sigma_p);

                //Chiranjit [2011 07 21]
                sw.WriteLine("dw = {0}", dw);
                sw.WriteLine("tw = {0}", tw);
                sw.WriteLine("nw = {0}", nw);

                sw.WriteLine("bf = {0}", bf1);
                sw.WriteLine("tf = {0}", tf1);
                sw.WriteLine("nf = {0}", nf);

                sw.WriteLine("ang = {0}", ang);
                sw.WriteLine("ang_thk = {0}", ang_thk);

                #endregion
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
            kPath = Path.Combine(kPath, "Composite Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Deck Slab + Steel Girder");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }
        public bool IsInnerGirder { get; set; }
        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF COMPOSITE BRIDGE : " + value;
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of Composite Deck Slab & Steel Girder Bridge");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name_inner = Path.Combine(file_path, "Composite_Inner_Main_Girder.TXT");
                rep_file_name_outer = Path.Combine(file_path, "Composite_Outer_Main_Girder.TXT");

                if (IsInnerGirder)
                {
                    rep_file_name = rep_file_name_inner;
                }
                else
                {
                    rep_file_name = rep_file_name_outer;
                }


                user_input_file = Path.Combine(system_path, "COMPOSITE_BRIDGE.FIL");

                //btnReport.Enabled = File.Exists(rep_file_name);
                //btnDrawing.Enabled = File.Exists(rep_file_name);
                //btnProcess.Enabled = Directory.Exists(value);

                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Read_User_Input();
                //}

            }
        }

        #endregion
        public string Title
        {
            get
            {
                return "COMPOSITE_BRIDGE";
            }
        }

        public double Get_Table_1_Value(double d_by_t, double d_point, ref string ref_string)
        {
            return iApp.Tables.Allowable_Average_Shear_Stress(d_by_t, d_by_t, ref ref_string);


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
        public double Get_Table_2_Value(double lamda, int indx, ref string ref_string)
        {

            return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, indx, ref ref_string);
            //lamda = Double.Parse(lamda.ToString("0.000"));

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
            //    if (lamda < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == lamda)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > lamda)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }

        public void Write_Table_1(StreamWriter sw)
        {
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
            //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
            //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
            //sw.WriteLine("-----------------------------------------------------------------------");


            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Allowable_Average_Shear_Stress();


            sw.WriteLine();
            sw.WriteLine("TABLE 1 : Allowable Average Shear Stress in Stiffened Webs");
            sw.WriteLine("----------------------------------------------------------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();

            lst_content.Clear();
            sw.WriteLine("-----------------------------------------------------------------------");

        }
        public void Write_Table_2(StreamWriter sw)
        {
            sw.WriteLine();
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
            //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
            //sw.WriteLine("λ= (L/r)  ______________________________________");
            //sw.WriteLine("           236        299        331       362");
            //sw.WriteLine("---------------------------------------------------");


            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");
            List<string> lst_content = iApp.Tables.Get_Tables_Allowable_Working_Stress_Cross_Section();
            sw.WriteLine();
            sw.WriteLine("TABLE 2 : Allowable Working Stress σac in N/sq.mm on Effective");
            sw.WriteLine("--------------------------------------------------------------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }

            lst_content.Clear();
            sw.WriteLine("---------------------------------------------------");

        }
        public void Write_Drawing_File()
        {
            //drawing_path = Path.Combine(drawing_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            drawing_path = Path.Combine(system_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                //_A = spacing_main_girder - (width_long_girders / 1000.0);
                //_B = (width_long_girders / 1000.0);
                //_C = spacing_cross_girder;
                //_D = (width_cross_girders / 1000.0);
                //_E = Dwc / 1000.0;
                //_F = Ds / 1000.0;
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_G={0}", _G);


                //(v) = (E) + 2 x (G) = 3.6 + 2 x 0.08 = 3.76 m. = 3760 mm.

                double val1, val2, val3;

                val1 = 0.0;
                val2 = 0.0;
                val3 = 0.0;

                if (double.TryParse(_E.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val1))
                {
                    if (double.TryParse(_G.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val2))
                    {
                        val3 = val1 + 2 * val2;
                        _v = string.Format("(E) + 2 x (G) = {0:f3} + 2 x {1:f3} = {2:f3} m. = {3:f2} mm.", val1, val2, val3, (val3 * 1000.0));
                    }

                }

                //(u) = (F) + 2 x (G) = 0.850 + 2 x 0.08 = 1.0 m. = 1000 mm.
                if (double.TryParse(_F.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val1))
                {
                    if (double.TryParse(_G.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val2))
                    {
                        val3 = val1 + 2 * val2;
                        _u = string.Format("(F) + 2 x (G) = {0:f3} + 2 x {1:f3} = {2:f3} m. = {3:f2} mm.", val1, val2, val3, (val3 * 1000.0));
                    }

                }

                sw.WriteLine("_v={0}", _v);
                sw.WriteLine("_u={0}", _u);
                sw.WriteLine("_1={0}", _1);
                sw.WriteLine("_2={0}", _2);
                sw.WriteLine("_3={0}", _3);
                sw.WriteLine("_4={0}", _4);
                sw.WriteLine("_6={0}", _6);
                sw.WriteLine("_7={0}", _7);
                sw.WriteLine("_8={0}", _8);
                sw.WriteLine("_10={0}", _10);

            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        #endregion

    }


}
