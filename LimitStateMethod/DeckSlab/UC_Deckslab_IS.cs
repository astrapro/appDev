using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
//using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
//using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.PSC_I_Girder;


using LimitStateMethod.RCC_T_Girder;
using LimitStateMethod.LS_Progress;
using LimitStateMethod.DeckSlab;


using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;



namespace LimitStateMethod.DeckSlab
{
    public partial class UC_Deckslab_IS : UserControl
    {
        public IApplication iApp;
        public UC_Deckslab_IS()
        {
            InitializeComponent();
            OnCreateData += UC_Deckslab_IS_OnCreateData;
            OnButtonClick += UC_Deckslab_IS_OnCreateData;
        }

        void UC_Deckslab_IS_OnCreateData(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Loading_Standard()
        {
            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                grb_title.Text = "Deckclab Design [as per AASHTO LRFD code]";



                txt_deck_ll6a_lw1.Text = "120";

                txt_deck_ll6a_lw2.Text = "120";

                txt_deck_ll6a_max_wl.Text = "120";

                //txt_deck_ll6a_wacc.Text = "";

                txt_deck_ll6_lw1.Text = "52.5";

                txt_deck_ll6_lw2.Text = "52.5";

                //txt_deck_ll6_dTrans.Text = "";




                txt_deck_ll5_lw1.Text = "240";

                txt_deck_ll5_lw2.Text = "240";

                txt_deck_ll5_wacc.Text = "240";

                //txt_deck_ll5_dTrans.Text = "";



                txt_deck_ll4_lw1.Text = "40";

                txt_deck_ll4_lw2.Text = "40";


                txt_deck_ll4_max_wl.Text = "40";

                //txt_deck_ll4_dTrans.Text = "";

                txt_deck_ll4_lw3.Text = "40";

                txt_deck_ll4_lw4.Text = "40";



                txt_deck_ll3_lw1.Text = "80";

                txt_deck_ll3_lw2.Text = "80";


                txt_deck_ll3_max_wl.Text = "80";


                txt_deck_ll3_dTrans.Text = "1.8";


                txt_deck_ll2_lw1.Text = "52.5";

                txt_deck_ll2_lw2.Text = "52.5";

                txt_deck_ll2_dTrans.Text = "1.8";

                txt_deck_ll2_lw3.Text = "52.5";

                txt_deck_ll2_lw4.Text = "52.5";


                txt_deck_ll1_lw1.Text = "52.5";

                txt_deck_ll1_lw2.Text = "52.5";

                txt_deck_ll1_dTrans.Text = "1.8";

            }
        }

        public string user_path { get; set; }
        public string Worksheet_Folder
        {
            get
            {
                if (user_path == null) return "";
                if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
            }
        }
        DeckSlab_Analysis_Extend Deck_Analysis = null;


        // The delegate procedure we are assigning to our object
        public delegate void CreateData(object sender,
                                             EventArgs e);

        public event CreateData OnCreateData;


        public delegate void ButtonClick(object sender, EventArgs e);

        public event ButtonClick OnButtonClick;


        private void btn_Deck_Analysis_Click(object sender, EventArgs e)
        {
            //Write_All_Data(true);
            //user_path = IsCreateData ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;

          


            OnCreateData(sender, e);
            OnButtonClick(sender, e);

            if (user_path == null) return;
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                //DECKSLAB_LL_TXT();
                #region Process
                int i = 1;
                //Write_All_Data(true);

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

                //if (!iApp.Show_and_Run_Process_List(pcol)) return;


                //while (i < 3) ;

                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                //string ana_rep_file = Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File);

                if (iApp.Show_and_Run_Process_List(pcol))
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

                }

                ////grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                ////grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;

                //Button_Enable_Disable();
                //Write_All_Data(false);
                iApp.Progress_Works.Clear();
                Button_Enable_Disable();

                #endregion Process
                //Write_All_Data(false);
            }
            catch (Exception ex) { }

            OnButtonClick(sender, e);
        }

        private void btn_Deck_Create_Analysis_Data_Click(object sender, EventArgs e)
        {

            OnCreateData(sender, e);

            if (user_path == null) return;
            if (user_path == "") return;

            #region Chiranjit [2015 01 07]



            L = MyList.StringToDouble(dgv_deck_user_input[1, 0].Value.ToString(), 0.0);
            NMG = MyList.StringToDouble(dgv_deck_user_input[1, 16].Value.ToString(), 0.0);
            SMG = MyList.StringToDouble(dgv_deck_user_input[1, 10].Value.ToString(), 0.0);
            os = MyList.StringToDouble(dgv_deck_user_input[1, 2].Value.ToString(), 0.0); ;
            Ds = MyList.StringToDouble(dgv_deck_user_input[1, 11].Value.ToString(), 0.0); ;
            B = MyList.StringToDouble(dgv_deck_user_input[1, 4].Value.ToString(), 0.0); ;


            CL = (B - SMG * (NMG - 1)) / 2.0;
            Width_LeftCantilever = CL;
            Width_RightCantilever = CL;

            if (Width_LeftCantilever == 0.0)
            {
                CL = MyList.StringToDouble(dgv_deck_user_input[1, 8].Value.ToString(), 0.0); ;
                Width_LeftCantilever = MyList.StringToDouble(dgv_deck_user_input[1, 8].Value.ToString(), 0.0);
                Width_RightCantilever = MyList.StringToDouble(dgv_deck_user_input[1, 8].Value.ToString(), 0.0);
            }
            Skew_Angle = MyList.StringToDouble(dgv_deck_user_input[1, 6].Value.ToString(), 0.0);
            Number_Of_Long_Girder = MyList.StringToInt(dgv_deck_user_input[1, 16].Value.ToString(), 4);
            //Number_Of_Cross_Girder = MyList.StringToInt(dgv_deck_user_input[0, 1].Value.ToString(), 3);
            //WidthBridge = L / (Number_Of_Cross_Girder - 1);



            #endregion Chiranjit [2015 01 07]

            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                //DECKSLAB_LL_TXT();
                #region Create Data

                Ana_Initialize_Analysis_InputData();

               

                Deckslab_Moving_Loads();

                Deck_Analysis.WriteData_DeadLoad_Analysis(Deck_Analysis.Input_File);

                //Calculate_Load_Computation(Deck_Analysis.Outer_Girders_as_String,
                //    Deck_Analysis.Outer_Girders_as_String,
                //     Deck_Analysis.joints_list_for_load);

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


                //if (Deck_Analysis.Live_Load_List == null) return;


                Button_Enable_Disable();


                MessageBox.Show(this, "All Analysis Input data files are created inside the working folder.", 
                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_Deck_Analysis.Enabled = true;
                cmb_deck_input_files.SelectedIndex = 0;

                #endregion Create Data

                //Write_All_Data(false);


            }
            catch (Exception ex) { }
            OnButtonClick(sender, e);
        }

        private void btn_deck_ll_Click(object sender, EventArgs e)
        {
            Deckslab_Moving_Loads();
        }

        List<DeckSlabLoads> Deck_LL_Loads { get; set; }

        public double CL, NMG, Ds, L, os;

        private void Deckslab_Moving_Loads()
        {
            if (Deck_LL_Loads == null)
                Deck_LL_Loads = new List<DeckSlabLoads>();

            Deck_LL_Loads.Clear();

            DeckSlabLoads dslds = new DeckSlabLoads();

            //Deck_Analysis

            List<string> list = new List<string>();



            Member mbr;


            double pos = 0;

            int i = 0;

            #region Load 1



            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll1_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll1_lw2.Text, 0.0);

            dslds.lw3 = 0.0;
            dslds.lw4 = 0.0;


            dslds.wacc = MyList.StringToDouble(txt_deck_ll1_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll1_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll1_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll1_dTrans.Text, 0.0);
            dslds.min_ed = MyList.StringToDouble(txt_deck_ll1_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll1_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll1_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll1_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll1_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll1_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll1_impf.Text, 0.0);
        
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_1 = list;

            #endregion Load 1
            Deck_LL_Loads.Add(dslds);

            #region Load 2
            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll2_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll2_lw2.Text, 0.0);

            dslds.lw3 = MyList.StringToDouble(txt_deck_ll2_lw3.Text, 0.0);
            dslds.lw4 = MyList.StringToDouble(txt_deck_ll2_lw4.Text, 0.0);


            dslds.wacc = MyList.StringToDouble(txt_deck_ll2_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll2_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll2_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll2_dTrans.Text, 0.0);
            dslds.min_ed = MyList.StringToDouble(txt_deck_ll2_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll2_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll2_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll2_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll2_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll2_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll2_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_2 = list;

            #endregion Load 2
            Deck_LL_Loads.Add(dslds);


            #region Load 3
            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll3_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll3_lw2.Text, 0.0);

            //dslds.lw3 = MyList.StringToDouble(txt_deck_ll3_lw3.Text, 0.0);
            //dslds.lw4 = MyList.StringToDouble(txt_deck_ll3_lw4.Text, 0.0);

            dslds.max_tyre_pressure = MyList.StringToDouble(txt_deck_ll3_max_prs.Text, 0.0);
            dslds.contact_area = MyList.StringToDouble(txt_deck_ll3_cnt_area.Text, 0.0);

            dslds.wacc = MyList.StringToDouble(txt_deck_ll3_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll3_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll3_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll3_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll3_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll3_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll3_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll3_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll3_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll3_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll3_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_3 = list;

            #endregion Load 3
            Deck_LL_Loads.Add(dslds);


            #region Load 4

            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll4_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll4_lw2.Text, 0.0);

            dslds.lw3 = MyList.StringToDouble(txt_deck_ll4_lw3.Text, 0.0);
            dslds.lw4 = MyList.StringToDouble(txt_deck_ll4_lw4.Text, 0.0);


            dslds.max_tyre_pressure = MyList.StringToDouble(txt_deck_ll4_max_prs.Text, 0.0);
            dslds.contact_area = MyList.StringToDouble(txt_deck_ll4_cnt_area.Text, 0.0);


            dslds.wacc = MyList.StringToDouble(txt_deck_ll4_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll4_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll4_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll4_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll4_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll4_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll4_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll4_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll4_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll4_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll4_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_4 = list;

            #endregion Load 4

            Deck_LL_Loads.Add(dslds);

            #region Load 5

            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll5_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll5_lw2.Text, 0.0);

            dslds.lw3 = 0.0;
            dslds.lw4 = 0.0;


            dslds.wacc = MyList.StringToDouble(txt_deck_ll5_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll5_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll5_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll5_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll5_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll5_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll5_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll5_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll5_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll5_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll5_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_5 = list;

            #endregion Load 5
            Deck_LL_Loads.Add(dslds);


            #region Load 6

            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll6a_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll6a_lw2.Text, 0.0);


            dslds.max_tyre_pressure = MyList.StringToDouble(txt_deck_ll6a_max_prs.Text, 0.0);
            dslds.contact_area = MyList.StringToDouble(txt_deck_ll6a_cnt_area.Text, 0.0);

            //dslds.lw3 = MyList.StringToDouble(txt_deck_ll6a_lw3.Text, 0.0);
            //dslds.lw4 = MyList.StringToDouble(txt_deck_ll6a_lw4.Text, 0.0);


            dslds.wacc = MyList.StringToDouble(txt_deck_ll6a_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll6a_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll6a_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll6a_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll6a_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll6a_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll6a_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll6a_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll6a_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll6a_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll6a_impf.Text, 0.0);
            dslds.Calculate_Data();
            //Set_Deckslab_Comb_Wheel_Data(dslds, list);
            //deck_ll_4 = list;



            //list = new List<string>();
            DeckSlabLoads data2 = new DeckSlabLoads();
            data2.SMG = SMG;
            data2.Width = B;
            data2.CL = CL;
            data2.NMG = NMG;
            data2.Alpha = 2.6;
            data2.Wct = Wc;
            data2.Ds = Ds;
            data2.b_by_lo = (L - 2 * os) / SMG / 2;

            data2.lw1 = MyList.StringToDouble(txt_deck_ll6_lw1.Text, 0.0);
            data2.lw2 = MyList.StringToDouble(txt_deck_ll6_lw2.Text, 0.0);

            //data2.lw3 = MyList.StringToDouble(txt_deck_ll6_lw3.Text, 0.0);
            //data2.lw4 = MyList.StringToDouble(txt_deck_ll6_lw4.Text, 0.0);


            data2.wacc = MyList.StringToDouble(txt_deck_ll6_wacc.Text, 0.0);

            data2.walg = MyList.StringToDouble(txt_deck_ll6_walg.Text, 0.0);
            data2.dTraffic = MyList.StringToDouble(txt_deck_ll6_dTraffic.Text, 0.0);
            data2.dTrans = MyList.StringToDouble(txt_deck_ll6_dTrans.Text, 0.0);
            //data2.min_ed = MyList.StringToDouble(txt_deck_ll6_min_ed.Text, 0.0);
            data2.b1 = MyList.StringToDouble(txt_deck_ll6_b1.Text, 0.0);
            data2.initpos = MyList.StringToDouble(txt_deck_ll6_initpos.Text, 0.0);
            data2.finalpos = MyList.StringToDouble(txt_deck_ll6_finalpos.Text, 0.0);
            data2.nlg = MyList.StringToDouble(txt_deck_ll6_nlg.Text, 0.0);
            data2.incr = MyList.StringToDouble(txt_deck_ll6_incr.Text, 0.0);
            data2.impf = MyList.StringToDouble(txt_deck_ll6_impf.Text, 0.0);
            data2.Calculate_Data();
            Set_Deckslab_Comb_Wheel_Data(dslds, data2, list);
            deck_ll_6 = list;
            #endregion Load 6
            Deck_LL_Loads.Add(dslds);
            Deck_LL_Loads.Add(data2);


        }

        

        private void Set_Deckslab_Wheel_Data(DeckSlabLoads dslds, List<string> list)
        {
            Member mbr;

            for (int i = 0; i < dslds.WH_1_data.Count; i++)
            {
                list.Add(string.Format("LOAD {0}", (i + 1)));
                list.Add(string.Format("MEMBER LOAD"));

                var item = dslds.WH_1_data[i];
                mbr = Get_Deck_Member(item.Position_X);

                if (mbr != null)
                {
                    //list.Add(string.Format("{0} CON GY -{1:f3} D {2:f3}", mbr.MemberNo, item.P_with_Impact, (item.Position_X - mbr.StartNode.X)));
                    list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                }

                if (dslds.WH_2_data.Count > 0)
                {
                    item = dslds.WH_2_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (dslds.WH_3_data.Count > 0)
                {
                    item = dslds.WH_3_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }
                if (dslds.WH_4_data.Count > 0)
                {
                    item = dslds.WH_4_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }
            }
            //pos = 0.0;
        }
        void Show_Deckslab_Moment_Shear()
        {
            MemberCollection mc = new MemberCollection(Deck_Analysis.LiveLoad_2_Analysis.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis.LiveLoad_2_Analysis.Analysis.Joints;

            #region Deck Model

            double L = Deck_Analysis.Length;
            double W = Deck_Analysis.WidthBridge;
            double val = L / 2;
            int i = 0;

            Deck_Analysis.Effective_Depth = L / 16.0; ;


            //List<int> _support_inn_joints = new List<int>();
            //List<int> _deff_inn_joints = new List<int>();
            //List<int> _L8_inn_joints = new List<int>();
            //List<int> _L4_inn_joints = new List<int>();
            //List<int> _3L8_inn_joints = new List<int>();
            //List<int> _L2_inn_joints = new List<int>();



            //List<int> _3L16_inn_joints = new List<int>();
            //List<int> _5L16_inn_joints = new List<int>();
            //List<int> _7L16_inn_joints = new List<int>();



            //List<int> _support_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();
            //List<int> _L8_out_joints = new List<int>();
            //List<int> _L4_out_joints = new List<int>();
            //List<int> _3L8_out_joints = new List<int>();
            //List<int> _L2_out_joints = new List<int>();


            //List<int> _3L16_out_joints = new List<int>();
            //List<int> _5L16_out_joints = new List<int>();
            //List<int> _7L16_out_joints = new List<int>();



            //List<int> _L4_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();

            List<int> _cross_joints = new List<int>();


            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            //for (i = 0; i < jn_col.Count; i++)
            //{
            //    if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
            //    if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            //}
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
                //try
                //{
                //    if ((jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right)) == false) continue;
                //    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                //    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                //            _L2_inn_joints.Add(jn_col[i].NodeNo);
                //    }

                //    if (jn_col[i].X.ToString("0.0") == ((3 * L / 8.0) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _3L8_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 8.0)) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _3L8_out_joints.Add(jn_col[i].NodeNo);
                //    }

                //    if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _L8_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _L8_out_joints.Add(jn_col[i].NodeNo);
                //    }

                //    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _L4_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _L4_out_joints.Add(jn_col[i].NodeNo);
                //    }

                //    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis.Effective_Depth + x_min).ToString("0.0")))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _deff_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis.Effective_Depth + x_min).ToString("0.0")))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _deff_out_joints.Add(jn_col[i].NodeNo);
                //    }

                //    if (jn_col[i].X.ToString("0.0") == (x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _support_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == (L + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _support_out_joints.Add(jn_col[i].NodeNo);
                //    }

                //    #region 3L/16


                //    if (jn_col[i].X.ToString("0.0") == ((3 * L / 16.0) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _3L16_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 16.0)) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _3L16_out_joints.Add(jn_col[i].NodeNo);
                //    }


                //    #endregion 3L/16



                //    #region 5L/16


                //    if (jn_col[i].X.ToString("0.0") == ((5 * L / 16.0) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _5L16_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == ((L - (5 * L / 16.0)) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _5L16_out_joints.Add(jn_col[i].NodeNo);
                //    }


                //    #endregion 3L/16



                //    #region 7L/16


                //    if (jn_col[i].X.ToString("0.0") == ((7 * L / 16.0) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z >= cant_wi_left)
                //            _7L16_inn_joints.Add(jn_col[i].NodeNo);
                //    }
                //    if (jn_col[i].X.ToString("0.0") == ((L - (7 * L / 16.0)) + x_min).ToString("0.0"))
                //    {
                //        if (jn_col[i].Z <= (W - cant_wi_right))
                //            _7L16_out_joints.Add(jn_col[i].NodeNo);
                //    }


                //    #endregion 3L/16



                //}
                //catch (Exception ex) 
                //{ 
                //    //MessageBox.Show(this, ""); 
                //}
            }

            #endregion Find Joints

            if (Result == null) Result = new List<string>();
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

            int index = 0;

            MaxForce f = null;

            List<int> tmp_jts = new List<int>();

            #region Live Load SHEAR Force

            #region Maximum Hogging Live Load 1

            Max_Hog_LL_1.Clear();

            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
                val = f.Force;
                Max_Hog_LL_1.Add(val);
            }
            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 2

            Max_Hog_LL_2.Clear();


            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
                val = f.Force;
                Max_Hog_LL_2.Add(val);
            }

            #endregion Maximum Hogging LL 2

            #region Maximum Hogging Live Load 3
            Max_Hog_LL_3.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
                val = f.Force;
                Max_Hog_LL_3.Add(val);
            }
            #endregion Maximum Hogging LL 3

            #region Maximum Hogging Live Load 4
            Max_Hog_LL_4.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
                val = f.Force;
                Max_Hog_LL_4.Add(val);
            }
            #endregion Maximum Hogging LL 4

            #region Maximum Hogging Live Load 5
            Max_Hog_LL_5.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
                val = f.Force;
                Max_Hog_LL_5.Add(val);
            }
            #endregion Maximum Hogging LL 5

            #region Maximum Hogging Live Load 6
            Max_Hog_LL_6.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
                val = f.Force;
                Max_Hog_LL_6.Add(val);
            }
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
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
                val = f.Force;
                Max_Sag_LL_1.Add(val);
            }

            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 2

            Max_Sag_LL_2.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
                val = f.Force;
                Max_Sag_LL_2.Add(val);
            }

            #endregion Maximum Hogging LL 2

            #region Maximum Hogging Live Load 3

            Max_Sag_LL_3.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
                val = f.Force;
                Max_Sag_LL_3.Add(val);
            }

            #endregion Maximum Hogging LL 3

            #region Maximum Hogging Live Load 4
            Max_Sag_LL_4.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
                val = f.Force;
                Max_Sag_LL_4.Add(val);
            }
            #endregion Maximum Hogging LL 4

            #region Maximum Hogging Live Load 5
            Max_Sag_LL_5.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
                val = f.Force;
                Max_Sag_LL_5.Add(val);
            }
            #endregion Maximum Hogging LL 5

            #region Maximum Hogging Live Load 6
            Max_Sag_LL_6.Clear();
            for (i = 1; i < jn_col.Count - 1; i++)
            {
                tmp_jts.Clear();
                tmp_jts.Add(jn_col[i].NodeNo);
                f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
                val = f.Force;
                Max_Sag_LL_6.Add(val);
            }
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




            for (i = 1; i < jn_col.Count - 1; i++)
            {

                #region Joint Forces
                tmp_jts.Clear();


                tmp_jts.Add(jn_col[i].NodeNo);

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

                #endregion Joint Forces

            }

            MyList.Array_Multiply_With(ref Max_Moment_DL, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_SIDL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_SIDL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_FPLL, 10.0);

            #endregion DL + SIDL + FPLL

            val = 0.0;

            //string format = "{0,-25} {1,12:f3} {2,10:f3} {3,15:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3} {12,10:f3} {13,10:f3} {14,10:f3} {15,10:f3} {16,10:f3}";
            string format = "{0,-25} {1,12:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3} {12,10:f3} {13,10:f3} {14,10:f3} {15,10:f3}";

            Result.Add(string.Format(""));

            #region Summary 1

            Result.Add(string.Format(""));
            Result.Add(string.Format("-------------------------------------------------------------"));
            Result.Add(string.Format("RCC T GIRDER (LIMIT STATE METHOD) DECK SLAB ANALYSIS RESULTS "));
            Result.Add(string.Format("--------------------------------------------------------------"));
            Result.Add(string.Format(""));
            //Result.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            //Result.Add(string.Format("                                NODE 2     NODE 3     NODE 4     NODE 5     NODE 6     NODE 7     NODE 8     NODE 9     NODE 10   NODE 11    NODE 12    NODE 13    NODE 14    NODE 15    NODE 16  "));
            ////Result.Add(string.Format("                                NODE 5     NODE 8          NODE 11    NODE 14    NODE 17    NODE 20    NODE 23    NODE 26   NODE 29    NODE 32    NODE 35    NODE 38    NODE 41    NODE 44    NODE 47"));
            //Result.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format(""));
            int indx = 0;
            try
            {

                //Result.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
                string kStr = string.Format("{0,-25} ", "");

                for (i = 0; i < Max_Moment_DL.Count; i++) kStr += string.Format(" {0,10}", "NODE " + (i + 2));
                //Result.Add(kStr);

                string sp_text = "".PadLeft(kStr.Length, '-');
                Result.Add(sp_text);
                Result.Add(kStr);
                Result.Add(sp_text);

                //Result.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));


                kStr = string.Format("{0,-25} ", "DL");

                for (i = 0; i < Max_Moment_DL.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Moment_DL[i]);
                Result.Add(kStr);

                kStr = string.Format("{0,-25} ", "SIDL (Except surfacing)");

                for (i = 0; i < Max_Moment_SIDL_1.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Moment_SIDL_1[i]);
                Result.Add(kStr);

                kStr = string.Format("{0,-25} ", "SIDL (Surfacing)");

                for (i = 0; i < Max_Moment_SIDL_2.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Moment_SIDL_2[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "FPLL");

                for (i = 0; i < Max_Moment_FPLL.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Moment_FPLL[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1CLA (Max. Hog)");

                for (i = 0; i < Max_Hog_LL_1.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_1[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1CLA (Max. Sag)");

                for (i = 0; i < Max_Sag_LL_1.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_1[i]);
                Result.Add(kStr);



                kStr = string.Format("{0,-25} ", "2CLA (Max. Hog)");

                for (i = 0; i < Max_Hog_LL_2.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_2[i]);
                Result.Add(kStr);



                kStr = string.Format("{0,-25} ", "2CLA (Max. Sag)");

                for (i = 0; i < Max_Sag_LL_2.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_2[i]);
                Result.Add(kStr);



                kStr = string.Format("{0,-25} ", "1L 40T-L (Max. Hog)");

                for (i = 0; i < Max_Hog_LL_3.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_3[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1L 40T-L (Max. Sag)");

                for (i = 0; i < Max_Sag_LL_3.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_3[i]);
                Result.Add(kStr);



                kStr = string.Format("{0,-25} ", "1L 40T-M (Max. Hog)");

                for (i = 0; i < Max_Hog_LL_4.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_4[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1L 40T-M (Max. Sag)");

                for (i = 0; i < Max_Sag_LL_4.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_4[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1L 70R TR (Max. Hog)");

                for (i = 0; i < Max_Hog_LL_5.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_5[i]);
                Result.Add(kStr);

                kStr = string.Format("{0,-25} ", "1L 70R TR (Max. Sag)");

                for (i = 0; i < Max_Sag_LL_5.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_5[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1L70RW+1LCA(Max Hog)");

                for (i = 0; i < Max_Hog_LL_6.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_6[i]);
                Result.Add(kStr);


                kStr = string.Format("{0,-25} ", "1L70RW+1LCA(Max Sag)");

                for (i = 0; i < Max_Sag_LL_6.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_6[i]);
                Result.Add(kStr);


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


                if (false)
                {
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

                kStr = string.Format("{0,-25} ", "LL (Max Hog)");

                for (i = 0; i < Max_Hog_LL_7.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Hog_LL_7[i]);


                kStr = string.Format("{0,-25} ", "LL (Max Sag)");

                for (i = 0; i < Max_Sag_LL_7.Count; i++) kStr += string.Format(" {0,10:f3}", Max_Sag_LL_7[i]);
                Result.Add(sp_text);
                Result.Add(sp_text);


            }
            catch (Exception exxx) { }
            //Result.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            //Result.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            Result.Add(string.Format(""));

            #endregion Summary 1

            Result.Add(string.Format(""));
            //rtb_ana_result.Lines = Result.ToArray();

            File.WriteAllLines(File_DeckSlab_Results, Result.ToArray());

            iApp.RunExe(File_DeckSlab_Results);
            Result.Add(string.Format(""));
        }

        void Show_Deckslab_Moment_Shear_2015_01_02()
        {
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

            if (Result == null) Result = new List<string>();
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

            int index = 0;

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
            //tmp_jts.Add(_deff_inn_joints[index]);
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_L8_inn_joints[index]);
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_3L16_inn_joints[index]);
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_L4_inn_joints[index]);
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_5L16_inn_joints[index]);
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_3L8_inn_joints[index]);
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_7L16_inn_joints[index]);
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L2_inn_joints[index]);
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_7L16_out_joints[index]);
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.AddRange(_3L8_out_joints.ToArray());
            tmp_jts.Add(_3L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.AddRange(_5L16_out_joints.ToArray());
            tmp_jts.Add(_5L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L4_out_joints[index]);
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_3L16_out_joints[index]);
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L8_out_joints[index]);
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_deff_out_joints[index]);
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);





            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
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
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
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


            tmp_jts.Add(_deff_inn_joints[index]);

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


            tmp_jts.Add(_L8_inn_joints[index]);

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


            tmp_jts.Add(_3L16_inn_joints[index]);

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


            tmp_jts.Add(_L4_inn_joints[index]);

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


            tmp_jts.Add(_5L16_inn_joints[index]);

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


            tmp_jts.Add(_3L8_inn_joints[index]);

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


            tmp_jts.Add(_7L16_inn_joints[index]);

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


            tmp_jts.Add(_L2_inn_joints[index]);

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


            tmp_jts.Add(_7L16_out_joints[index]);

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


            tmp_jts.Add(_L4_out_joints[index]);

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


            tmp_jts.Add(_3L16_out_joints[index]);

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


            tmp_jts.Add(_L8_out_joints[index]);

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


            tmp_jts.Add(_deff_out_joints[index]);

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


            tmp_jts.Add(_support_out_joints[index]);

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
            Result.Add(string.Format("                                NODE 2     NODE 3          NODE 4     NODE 5     NODE 6     NODE 7     NODE 8     NODE 9     NODE 10   NODE 11    NODE 12    NODE 13    NODE 14    NODE 15    NODE 16  "));
            //Result.Add(string.Format("                                NODE 5     NODE 8          NODE 11    NODE 14    NODE 17    NODE 20    NODE 23    NODE 26   NODE 29    NODE 32    NODE 35    NODE 38    NODE 41    NODE 44    NODE 47"));
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

        public string File_DeckSlab_Results
        {
            get
            {
                return Path.Combine(user_path, "DECKSLAB_ANALYSIS_RESULT.TXT");
            }
        }

        public List<string> Result { get; set; }

        private void Set_Deckslab_Comb_Wheel_Data(DeckSlabLoads data1, DeckSlabLoads data2, List<string> list)
        {

            Member mbr;

            for (int i = 0; i < data1.WH_1_data.Count; i++)
            {
                list.Add(string.Format("LOAD {0}", (i + 1)));
                list.Add(string.Format("MEMBER LOAD"));

                var item = data1.WH_1_data[i];
                mbr = Get_Deck_Member(item.Position_X);

                if (mbr != null)
                {
                    //list.Add(string.Format("{0} CON GY -{1:f3} D {2:f3}", mbr.MemberNo, item.P_with_Impact, (item.Position_X - mbr.StartNode.X)));
                    list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                }

                if (data2.WH_1_data.Count > 0)
                {
                    item = data2.WH_1_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }



                if (data1.WH_2_data.Count > 0)
                {
                    item = data1.WH_2_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }


                if (data2.WH_2_data.Count > 0)
                {
                    item = data2.WH_2_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }


                if (data1.WH_3_data.Count > 0)
                {
                    item = data1.WH_3_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (data2.WH_3_data.Count > 0)
                {
                    item = data2.WH_3_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (data1.WH_4_data.Count > 0)
                {
                    item = data1.WH_4_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (data2.WH_4_data.Count > 0)
                {
                    item = data2.WH_4_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }
            }
            //pos = 0.0;
        }

        public Member Get_Deck_Member(double pos)
        {
            int i = 0;
            Member mbr;
            for (i = 0; i < Deck_Analysis.MemColls.Count; i++)
            {
                mbr = Deck_Analysis.MemColls[i];
                if (pos >= mbr.StartNode.X && pos <= mbr.EndNode.X)
                    return mbr;
            }
            return null;
        }

        private void txt_deck_ll_lw1_TextChanged(object sender, EventArgs e)
        {
            Deckslab_Interactives();
        }

        public double Dw { get; set; }
        public double Wc { get; set; }
        public double Wf { get; set; }
        public double B { get; set; }
        public double SMG { get; set; }

        public double Deff { get; set; }


        List<string> deck_ll = new List<string>();
        List<string> deck_ll_types = new List<string>();


        List<string> deck_ll_1 = new List<string>();
        List<string> deck_ll_2 = new List<string>();
        List<string> deck_ll_3 = new List<string>();
        List<string> deck_ll_4 = new List<string>();
        List<string> deck_ll_5 = new List<string>();
        List<string> deck_ll_6 = new List<string>();

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
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[0]));

                load_lst.Add("LOAD 2 SIDL EXCEPT SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[1]));

                load_lst.Add("LOAD 3 SIDL SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[2]));

                if (deck_member_load.Count > 3)
                {
                    load_lst.Add("LOAD 4 FPLL");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[3]));
                }
                else
                {
                    load_lst.Add("LOAD 4 FPLL");
                    load_lst.Add("MEMBER LOAD");
                    load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, 0.0));
                }
            }
            else
            {
                //load_lst.Add("LOAD 1");
                //load_lst.Add("MEMBER LOAD");
                //load_lst.Add("1 TO " + Deck_Analysis.MemColls.Count + " UNI GY -0.001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                List<string> list = new List<string>();
                //list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));

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
        public List<double> deck_member_load = new List<double>();

        public void Button_Enable_Disable()
        {
            try
            {
                Deck_Buttons();

                btn_LS_deck_rep_open.Enabled = File.Exists(Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Deckslab)));
                btn_LS_deck_ws.Enabled = File.Exists(File_DeckSlab_Results);

                if (!btn_Deck_Analysis.Enabled)
                    btn_Deck_Analysis.Enabled = File.Exists(File_DeckSlab_Results);
            }
            catch (Exception ex) { }
        }
        private void Deck_Buttons()
        {
            ComboBox cmb = cmb_deck_input_files;
            if(Deck_Analysis == null)
            {
                Ana_Initialize_Analysis_InputData();
            }


            string usp = Path.Combine(user_path, "Deck Slab Analysis");
           

            Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");

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

        }

        public string Excel_Deckslab
        {
            get
            {

                string e_fn = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Deckslab\Design_of_Deckslab_IRC_112.xlsx");

                if(Number_Of_Long_Girder != 4)
                {
                    e_fn = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Deckslab\" + Number_Of_Long_Girder +  " Girder Design_of_Deckslab_IRC_112.xlsx");
                }

                if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    e_fn = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Deckslab\Design_of_Deckslab_AASHTO_LRFD.xlsx");



                if (File.Exists(e_fn)) return e_fn;


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder_Deck Slab BS.xlsx");
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Deckslab\Design_of_Deckslab_AASHTO_LRFD.xlsx");

                return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder_Deck Slab IS.xlsx");
            }
        }

        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }
        public double Skew_Angle { get; set; }
        public int Number_Of_Long_Girder { get; set; }
        public int Number_Of_Cross_Girder { get; set; }
        public double WidthBridge { get; set; }

        void Ana_Initialize_Analysis_InputData()
        {

            //if (Deck_Analysis == null)
            Deck_Analysis = new DeckSlab_Analysis_Extend(iApp);


            double Bs = (B - Width_LeftCantilever - Width_RightCantilever) / (NMG - 1);

            Deck_Analysis.Length = MyList.StringToDouble(dgv_deck_user_input[1, 4].Value.ToString(), 0.0);
            //Deck_Analysis.WidthBridge = 6.0;

            Deck_Analysis.Width_LeftCantilever = Width_LeftCantilever;
            Deck_Analysis.Width_RightCantilever = Width_RightCantilever;
            Deck_Analysis.Skew_Angle = Skew_Angle;
            Deck_Analysis.Number_Of_Long_Girder = Number_Of_Long_Girder;
            Deck_Analysis.Number_Of_Cross_Girder = Number_Of_Cross_Girder;
            Deck_Analysis.WidthBridge = WidthBridge;

            //Deck_Analysis.Lwv = MyList.StringToDouble(txt_Ana_Lwv.Text, 0.0);
            //Deck_Analysis.Wkerb = MyList.StringToDouble(txt_Ana_Wkerb.Text, 0.0);



            string usp = Path.Combine(user_path, "Deck Slab Analysis");
            if (!Directory.Exists(usp))
            {
                Directory.CreateDirectory(usp);
            }


            Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
            Deck_Analysis.CreateData();
        }

        private void Deckslab_Interactives()
        {

            Excel_User_Inputs eui = new Excel_User_Inputs();

            eui.Read_From_Grid(dgv_deck_user_input);

            double wacc = MyList.StringToDouble(txt_deck_ll1_wacc.Text, 0.0);
            double walg = MyList.StringToDouble(txt_deck_ll1_walg.Text, 0.0);
            double min_ed = MyList.StringToDouble(txt_deck_ll1_min_ed.Text, 0.0);
            double dTrans = MyList.StringToDouble(txt_deck_ll1_dTrans.Text, 0.0);
            double dTraffic = MyList.StringToDouble(txt_deck_ll2_dTraffic.Text, 0.0);
            double nlg = MyList.StringToDouble(txt_deck_ll1_nlg.Text, 0.0);

            double b1 = (wacc / 1000) + 2 * Dw;
            double initpos = Wc + Wf + min_ed;
            double finalpos = B - initpos - dTrans;
            double incr = (finalpos - initpos) / nlg;
            double impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll1_b1.Text = b1.ToString("f3");
            txt_deck_ll1_initpos.Text = initpos.ToString("f3");
            txt_deck_ll1_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll1_incr.Text = incr.ToString("f4");
            txt_deck_ll1_impf.Text = impf.ToString("f2");

            #region Deck Slab Load 1


            wacc = MyList.StringToDouble(txt_deck_ll1_wacc.Text, 0.0);
            walg = MyList.StringToDouble(txt_deck_ll1_walg.Text, 0.0);
            min_ed = MyList.StringToDouble(txt_deck_ll1_min_ed.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll1_dTrans.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll1_nlg.Text, 0.0);

            b1 = (wacc / 1000) + 2 * Dw;
            initpos = Wc + Wf + min_ed;
            finalpos = B - initpos - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll1_b1.Text = b1.ToString("f3");
            txt_deck_ll1_initpos.Text = initpos.ToString("f3");
            txt_deck_ll1_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll1_incr.Text = incr.ToString("f4");
            //txt_deck_ll1_impf.Text = impf.ToString("f2");


            #endregion Deck Slab Load 1

            #region Deck Slab Load 2

            wacc = MyList.StringToDouble(txt_deck_ll2_wacc.Text, 0.0);
            walg = MyList.StringToDouble(txt_deck_ll2_walg.Text, 0.0);
            min_ed = MyList.StringToDouble(txt_deck_ll2_min_ed.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll2_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll1_dTraffic.Text, 0.0) + walg / 1000;
            nlg = MyList.StringToDouble(txt_deck_ll2_nlg.Text, 0.0);

            b1 = (wacc / 1000) + 2 * Dw;
            initpos = Wc + Wf + min_ed;
            finalpos = B - initpos - dTrans - dTraffic - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll2_dTraffic.Text = dTraffic.ToString("f3");
            txt_deck_ll2_b1.Text = b1.ToString("f3");
            txt_deck_ll2_initpos.Text = initpos.ToString("f3");
            txt_deck_ll2_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll2_incr.Text = incr.ToString("f4");
            //txt_deck_ll2_impf.Text = impf.ToString("f2");


            #endregion Deck Slab Load 2

            #region Deck Slab Load 3

            wacc = MyList.StringToDouble(txt_deck_ll3_wacc.Text, 0.0);
            //walg = MyList.StringToDouble(txt_deck_ll3_walg.Text, 0.0);

            double max_wl = MyList.StringToDouble(txt_deck_ll3_max_wl.Text, 0.0);
            double max_prs = MyList.StringToDouble(txt_deck_ll3_max_prs.Text, 0.0);
            double cnt_area = max_wl * 100.0 / max_prs;


            walg = cnt_area * 100 / wacc;

            //initpos = MyList.StringToDouble(txt_deck_ll3_initpos.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll3_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll3_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll3_nlg.Text, 0.0);


            b1 = (walg / 1000) + 2 * Dw;
            initpos = Wc + Wf + dTraffic;
            finalpos = B - initpos - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll3_walg.Text = walg.ToString("f3");
            txt_deck_ll3_b1.Text = b1.ToString("f3");
            txt_deck_ll3_initpos.Text = initpos.ToString("f3");
            txt_deck_ll3_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll3_incr.Text = incr.ToString("f4");
            txt_deck_ll3_cnt_area.Text = cnt_area.ToString("f2");


            #endregion Deck Slab Load 4

            #region Deck Slab Load 4

            wacc = MyList.StringToDouble(txt_deck_ll4_wacc.Text, 0.0);
            //walg = MyList.StringToDouble(txt_deck_ll4_walg.Text, 0.0);

            max_wl = MyList.StringToDouble(txt_deck_ll4_max_wl.Text, 0.0);
            max_prs = MyList.StringToDouble(txt_deck_ll4_max_prs.Text, 0.0);
            cnt_area = max_wl * 100.0 / max_prs;


            walg = cnt_area * 100 / wacc;

            initpos = MyList.StringToDouble(txt_deck_ll4_initpos.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll4_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll4_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll4_nlg.Text, 0.0);


            b1 = (walg / 1000) + 2 * Dw;
            //initpos = Wc + Wf + dTraffic;
            finalpos = B - initpos - dTrans - dTraffic - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll4_walg.Text = walg.ToString("f3");
            txt_deck_ll4_b1.Text = b1.ToString("f3");
            txt_deck_ll4_initpos.Text = initpos.ToString("f3");
            txt_deck_ll4_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll4_incr.Text = incr.ToString("f4");
            txt_deck_ll4_cnt_area.Text = cnt_area.ToString("f2");


            #endregion Deck Slab Load 4


            #region Deck Slab Load 5


            wacc = MyList.StringToDouble(txt_deck_ll5_wacc.Text, 0.0);
            walg = MyList.StringToDouble(txt_deck_ll5_walg.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll5_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll5_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll5_nlg.Text, 0.0);

            b1 = (wacc / 1000) + 2 * Dw;
            initpos = Wc + Wf + dTraffic + walg / 2 / 1000;
            finalpos = B - initpos - dTrans;
            incr = (finalpos - initpos) / nlg;

            txt_deck_ll5_b1.Text = b1.ToString("f3");
            txt_deck_ll5_initpos.Text = initpos.ToString("f3");
            txt_deck_ll5_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll5_incr.Text = incr.ToString("f4");
            //txt_deck_ll5_impf.Text = impf.ToString("f2");


            #endregion Deck Slab Load 5


            #region Deck Slab Load 6


            #region Deck Slab Load 6 Type 1

            wacc = MyList.StringToDouble(txt_deck_ll6a_wacc.Text, 0.0);
            //walg = MyList.StringToDouble(txt_deck_ll6a_walg.Text, 0.0);

            max_wl = MyList.StringToDouble(txt_deck_ll6a_max_wl.Text, 0.0);
            max_prs = MyList.StringToDouble(txt_deck_ll6a_max_prs.Text, 0.0);
            cnt_area = max_wl * 100.0 / max_prs;


            walg = cnt_area * 100 / wacc;

            initpos = MyList.StringToDouble(txt_deck_ll6a_initpos.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll6a_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll6a_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll6a_nlg.Text, 0.0);


            double wacc1 = MyList.StringToDouble(txt_deck_ll6_wacc.Text, 0.0);
            double walg1 = MyList.StringToDouble(txt_deck_ll6_walg.Text, 0.0);
            double dTrans1 = MyList.StringToDouble(txt_deck_ll6_dTrans.Text, 0.0);
            double dTraffic1 = MyList.StringToDouble(txt_deck_ll6_dTraffic.Text, 0.0);
            double nlg1 = MyList.StringToDouble(txt_deck_ll6_nlg.Text, 0.0);


            //=$G$376+1.93+(0.86/2+1.2+0.5/2+1.8)

            double val = initpos + dTrans + dTraffic + dTrans1 + walg1 / 2 / 1000 - 0.43;

            double val2 = (B - walg1 / 1000 - min_ed - val);

            b1 = (walg / 1000) + 2 * Dw;


            double initpos1 = initpos + dTrans + dTraffic;



            double finalpos1 = B - dTraffic1 - walg1 / 1000 - min_ed - 0.42;
            incr = (finalpos1 - initpos1) / nlg1;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll6a_walg.Text = walg.ToString("f3");
            txt_deck_ll6a_b1.Text = b1.ToString("f3");
            txt_deck_ll6a_initpos.Text = initpos.ToString("f3");
            txt_deck_ll6a_finalpos.Text = (initpos + val2).ToString("f3");
            txt_deck_ll6a_incr.Text = incr.ToString("f4");
            txt_deck_ll6a_cnt_area.Text = cnt_area.ToString("f2");


            #endregion Deck Slab Load 6



            b1 = (wacc / 1000) + 2 * Dw;
            //initpos = Wc + Wf + dTraffic + walg / 2 / 1000;
            //finalpos = B - initpos - dTrans;
            incr = (finalpos1 - initpos1) / nlg;

            txt_deck_ll6_b1.Text = b1.ToString("f3");
            txt_deck_ll6_initpos.Text = initpos1.ToString("f3");
            txt_deck_ll6_finalpos.Text = (initpos1 + val2).ToString("f3");
            txt_deck_ll6_incr.Text = incr.ToString("f4");
            //txt_deck_ll6_impf.Text = impf.ToString("f2");





            #endregion Deck Slab Load 6


        }

        private void cmb_deck_input_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Deck_Buttons();
            Button_Enable_Disable();
        }

        private void btn_deck_view_data_Click(object sender, EventArgs e)
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

        private void txt_deck_ll1_lw1_TextChanged(object sender, EventArgs e)
        {
            Deckslab_Interactives();

        }

        private void btn_LS_deck_ws_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            string excel_file_name = "";
            string copy_path = "";
            //if (btn.Name == btn_LS_long_ws.Name)
            if (btn.Name == btn_LS_deck_rep_open.Name)
            {
                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Deckslab));
                if (File.Exists(copy_path))
                    iApp.OpenExcelFile(copy_path, "2011ap");
            }
            else if (btn.Name == btn_LS_deck_ws.Name)
            {

                Deckslab_Moving_Loads();

                excel_file_name = Excel_Deckslab;

                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                try
                {
                    copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));
                    File.Copy(excel_file_name, copy_path, true);
                    RCC_Deckslab_Excel_Update_Extend rcc_excel = new RCC_Deckslab_Excel_Update_Extend();
                    rcc_excel.Excel_File_Name = copy_path;
                    rcc_excel.Report_File_Name = File_DeckSlab_Results;

                    Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Deck_Analysis.LiveLoad_File);
                    rcc_excel.llc = Deck_Analysis.Live_Load_List;
                    rcc_excel.Deckslab_User_Inputs.Read_From_Grid(dgv_deck_user_input);
                    rcc_excel.Deckslab_Design_Inputs.Read_From_Grid(dgv_deck_design_input);
                    //rcc_excel.Deckslab_User_Live_loads.Read_From_Grid(dgv_deck_user_live_loads);

                    rcc_excel.Deck_Loads = Deck_LL_Loads;

                    iApp.Excel_Open_Message();
                    rcc_excel.Read_Update_Data();
                }
                catch (Exception ex) { }
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
        }

        private void UC_Deckslab_IS_Load(object sender, EventArgs e)
        {
            Deckslab_User_Input();
            Deckslab_Interactives();
            Button_Enable_Disable();
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
            lst_inp_vals.Add(string.Format("4"));
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
            //lst_input.Clear();
            //lst_input.Add(string.Format("alpha (Constant)"));

            //lst_input.Add(string.Format("Distances  from start of deck"));

            //lst_input.Add(string.Format("Max. tyre pressure "));
            //lst_input.Add(string.Format("Distance between two loads across span "));
            //lst_input.Add(string.Format("Impact factor "));

            ////lst_input.Add(string.Format("1Lane Class - A"));
            //lst_input.Add(string.Format("1Lane  Load Type 1"));
            //lst_input.Add(string.Format("Contact width across span"));
            //lst_input.Add(string.Format("Contact width along span"));
            //lst_input.Add(string.Format("Distance between two vehicle c/c of  wheels (Traffic Direction)"));
            //lst_input.Add(string.Format("Distance between two Wheels C/C (Transverse direction)"));
            //lst_input.Add(string.Format("Minimum edge distance upto c/l of wheel"));

            ////lst_input.Add(string.Format("1Lane 40T Boggie (L-Type)"));
            //lst_input.Add(string.Format("1Lane  Load Type 4"));
            //lst_input.Add(string.Format("Contact width along span"));
            //lst_input.Add(string.Format("Distances between C/C of wheel Across the traffic"));

            ////lst_input.Add(string.Format("1Lane 40T Boggie (M-Type)"));
            //lst_input.Add(string.Format("1Lane  Load Type 5"));
            //lst_input.Add(string.Format("Contact width along span"));

            ////lst_input.Add(string.Format("1Lane 70R Track"));
            //lst_input.Add(string.Format("1Lane  Load Type 2"));
            //lst_input.Add(string.Format("Contact width along span"));
            //lst_input.Add(string.Format("Distance between two loads across span "));
            //lst_input.Add(string.Format("Contact width across span"));


            //lst_inp_vals.Clear();
            //lst_inp_vals.Add(string.Format("2.600"));
            //lst_inp_vals.Add(string.Format("0"));
            //lst_inp_vals.Add(string.Format("5.273"));
            //lst_inp_vals.Add(string.Format("1.22"));
            //lst_inp_vals.Add(string.Format("1.25"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("250"));
            //lst_inp_vals.Add(string.Format("500"));
            //lst_inp_vals.Add(string.Format("1.200"));
            //lst_inp_vals.Add(string.Format("1.800"));
            //lst_inp_vals.Add(string.Format("0.400"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("810"));
            //lst_inp_vals.Add(string.Format("1.93"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("360"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("840"));
            //lst_inp_vals.Add(string.Format("30"));
            //lst_inp_vals.Add(string.Format("4570"));


            //lst_units.Clear();
            //lst_units.Add(string.Format("(Cont.)"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("kg/cm^2"));
            //lst_units.Add(string.Format("m"));

            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("m"));


            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("m"));


            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));


            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("mm"));


            ////MessageBox.Show((DateTime.Now - DateTime.UtcNow).ToString());

            ////dgv_deck_user_live_loads.Rows.Clear();
            ////for (int i = 0; i < lst_inp_vals.Count; i++)
            ////{
            ////    dgv_deck_user_live_loads.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);

            ////    if (lst_inp_vals[i] == "")
            ////    {
            ////        dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
            ////        //dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 9.0, FontStyle.Bold, GraphicsUnit.Pixel);
            ////        dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);
            ////    }

            ////}
            #endregion Design Data
        }

     
 
    }


    public class DeckSlabLoads
    {
        // inputs from Analysis
        public double SMG, Width, CL, NMG, Alpha, Wct, Ds;

        // inputs 
        public double lw1, lw2, lw3, lw4, wacc, walg, dTraffic, dTrans, min_ed;
        public double max_tyre_pressure;
        public double contact_area;


        // auto calculated
        public double b1, initpos, finalpos, nlg, incr, impf;

        public double b_by_lo;

        public List<double> L { get; set; }
        public DeckSlabLoads()
        {
            SMG = 3.0; //m
            Width = 12.0; // m
            CL = 1.5; // m
            NMG = 4;  // nos
            Alpha = 2.6; //
            Wct = 75; //mm
            Ds = 0.2;

            b_by_lo = 3.887; // m

            L = new List<double>();


            lw1 = 57.0;
            lw2 = 57.0;

            lw3 = 0.0;
            lw4 = 0.0;


            wacc = 250.0;
            walg = 500.0;
            dTraffic = 1.2;
            dTrans = 1.8;
            min_ed = 0.4;

            // auto calculated
            b1 = 0.4;
            initpos = 0.9;
            finalpos = 9.3;
            nlg = 58;
            incr = 0.1448;
            impf = 1.5;

        }

        public void Calculate_Data1()
        {
            //initpos = 0.9;
            //finalpos = 9.3;
            //nlg = 58;
            incr = (finalpos - initpos) / nlg;


            L.Clear();
            double dVal = 0;

            dVal = 0.0;
            L.Add(dVal);

            dVal = CL;
            do
            {
                L.Add(dVal);
                dVal += SMG;
            }
            while (dVal <= (Width - CL));

            L.Add(Width);


            int i = 0;
            #region Calculated Position

            WheelData WH_1_data = new WheelData();


            double pos, dist, beff, befmod, p;
            for (i = 0; i <= nlg; i++)
            {
                pos = initpos + (i * incr);

                dist = Get_Nearest_Distance_from_Support(pos);

                //=IF(OR(B44<$I$9,B44>$N$24),1.2*C44+$L$35,C44*$I$12*(1-(C44/$I$7))+$L$35)
                if (pos < CL || pos > (Width - CL))
                    beff = 1.2 * dist + b1;
                else
                    beff = dist * Alpha * (1 - (dist / SMG)) + b1;

                //=+IF(D44>$L$32,(D44+$L$32)/2,D44)
                if (beff > dTraffic)
                    befmod = (beff + dTraffic) / 2;
                else
                    befmod = beff;

                //=+IF(E44=0,0,$L$28/E44)*$L$40
                if (befmod == 0.0)
                    p = 0.0;
                else
                    p = (lw1 / befmod) * impf;

                //WH_1_data.LoadCases.Add((i + 1));
                //WH_1_data.Position_X.Add(pos);
                //WH_1_data.Dist_nearest_Support.Add(dist);
                //WH_1_data.beff.Add(beff);
                //WH_1_data.beff_mod.Add(befmod);
                //WH_1_data.P_with_Impact.Add(p);

                WH_1_data.Add(pos, dist, beff, befmod, p);



            }





            //for (i = 0; i < WH_1_data.LoadCases.Count; i++)
            //{
            //}






            #endregion Calculated

        }


        public WheelData WH_1_data = new WheelData();
        public WheelData WH_2_data = new WheelData();
        public WheelData WH_3_data = new WheelData();
        public WheelData WH_4_data = new WheelData();

        public void Calculate_Data()
        {
            //initpos = 0.9;
            //finalpos = 9.3;
            //nlg = 58;
            incr = (finalpos - initpos) / nlg;


            L.Clear();
            double dVal = 0;

            dVal = 0.0;
            L.Add(dVal);

            dVal = CL;
            do
            {
                if (SMG == 0) break;
                L.Add(dVal);
                dVal += SMG;

            }
            while (dVal <= (Width - CL));

            L.Add(Width);


            int i = 0;
            #region Calculated Position

            WH_1_data = new WheelData();
            WH_2_data = new WheelData();
            WH_3_data = new WheelData();
            WH_4_data = new WheelData();



            double pos, dist, beff, befmod, p;
            for (i = 0; i <= nlg; i++)
            {
                if (lw1 != 0.0)
                {
                    #region Wheel 1 Data

                    pos = initpos + (i * incr);
                    Get_Loadcase_Data(pos, out dist, out beff, out befmod, out p);
                    WH_1_data.Add(pos, dist, beff, befmod, p);

                    #endregion Wheel 1 Data
                }
                if (lw2 != 0.0)
                {
                    #region Wheel 2 Data

                    pos = initpos + (i * incr) + dTrans;
                    Get_Loadcase_Data(pos, out dist, out beff, out befmod, out p);

                    WH_2_data.Add(pos, dist, beff, befmod, p);

                    #endregion Wheel 2 Data
                }
                if (lw3 != 0.0)
                {
                    #region Wheel 3 Data

                    pos = initpos + (i * incr) + dTrans + dTraffic;
                    Get_Loadcase_Data(pos, out dist, out beff, out befmod, out p);

                    WH_3_data.Add(pos, dist, beff, befmod, p);

                    #endregion Wheel 3 Data
                }
                if (lw4 != 0.0)
                {
                    #region Wheel 4 Data

                    pos = initpos + (i * incr) + dTrans + dTraffic + dTrans;
                    Get_Loadcase_Data(pos, out dist, out beff, out befmod, out p);

                    WH_4_data.Add(pos, dist, beff, befmod, p);

                    #endregion Wheel 4 Data
                }
            }

            //for (i = 0; i < WH_1_data.Count; i++)
            //{

            //}

            #endregion Calculated

        }

        private void Get_Loadcase_Data(double pos, out double dist, out double beff, out double befmod, out double p)
        {

            dist = Get_Nearest_Distance_from_Support(pos);

            //=IF(OR(B44<$I$9,B44>$N$24),1.2*C44+$L$35,C44*$I$12*(1-(C44/$I$7))+$L$35)
            if (pos < CL || pos > (Width - CL))
                beff = 1.2 * dist + b1;
            else
                beff = dist * Alpha * (1 - (dist / SMG)) + b1;

            //=+IF(D44>$L$32,(D44+$L$32)/2,D44)
            if (beff > dTraffic)
                befmod = (beff + dTraffic) / 2;
            else
                befmod = beff;

            //=+IF(E44=0,0,$L$28/E44)*$L$40
            if (befmod == 0.0)
                p = 0.0;
            else
                p = (lw1 / befmod) * impf;
        }

        public double Get_Nearest_Distance_from_Support(double position)
        {
            double dval = 0.0;

            if (L.Count == 0) return dval;

            for (int i = 1; i < L.Count; i++)
            {
                if (position >= L[i - 1] && position <= L[i])
                    dval = Math.Min((position - L[i - 1]), (L[i] - position));
            }

            return dval;
        }


    }

    public class WheelData : List<WheelLoadcase>
    {
        //public List<int> LoadCases { get; set; }
        //public List<double> Position_X { get; set; }
        //public List<double> Dist_nearest_Support { get; set; }
        //public List<double> beff { get; set; }
        //public List<double> beff_mod { get; set; }
        //public List<double> P_with_Impact { get; set; }
        public WheelData()
            : base()
        {
            //LoadCases = new List<int>();
            //Position_X = new List<double>();
            //Dist_nearest_Support = new List<double>();
            //beff = new List<double>();
            //beff_mod = new List<double>();
            //P_with_Impact = new List<double>();
        }
        public void Add(double pos, double dist, double beff, double befmod, double p)
        {


            //LoadCases.Add(LoadCases.Count + 1);
            //Position_X.Add(pos);
            //Dist_nearest_Support.Add(dist);
            //this.beff.Add(beff);
            //beff_mod.Add(befmod);
            //P_with_Impact.Add(p);

            WheelLoadcase wh = new WheelLoadcase();

            wh.LoadCase = this.Count + 1;
            wh.Position_X = (pos);
            wh.Dist_nearest_Support = (dist);
            wh.beff = (beff);
            wh.beff_mod = (befmod);
            wh.P_with_Impact = (p);

            this.Add(wh);
        }


        public List<string> Results
        {
            get
            {
                List<string> list = new List<string>();

                foreach (var item in this)
                {
                    list.Add(string.Format(""));
                    list.Add(item.ToString());
                    list.Add(string.Format(""));
                }
                return list;

            }
        }
    }
    public class WheelLoadcase
    {
        public WheelLoadcase()
        {
            LoadCase = -1;
            Position_X = -1;
            Dist_nearest_Support = -1;
            beff = -1;
            beff_mod = -1;
            P_with_Impact = -1;
        }
        public int LoadCase { get; set; }
        public double Position_X { get; set; }
        public double Dist_nearest_Support { get; set; }
        public double beff { get; set; }
        public double beff_mod { get; set; }
        public double P_with_Impact { get; set; }
        public override string ToString()
        {
            return string.Format("{0,-5} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}",
                LoadCase, Position_X, Dist_nearest_Support, beff, beff_mod, P_with_Impact);
        }
    }

    public class DeckSlab_Analysis_Extend
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        JointNode[,] Joints_Array;
        Member[,] Long_Girder_Members_Array;
        Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis TotalLoad_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_Analysis = null;

        public BridgeMemberAnalysis LiveLoad_1_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_2_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_3_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_4_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_5_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_6_Analysis = null;


        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;



        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        public double span_length = 0.0;

        public double cl = 1.5;
        public double spc = 3.0;

        //Chiranjit [2013 06 06] Kolkata

        string list_envelop_inner = "";
        string list_envelop_outer = "";


        string input_file, user_path;
        public DeckSlab_Analysis_Extend(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();

        }

        #region Properties

        public double Length { get; set; }
        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }
        public int Total_Rows
        {
            get
            {
                //return (int)(((WidthBridge - (WidthCantilever)) / Spacing_Long_Girder) + 1);
                return 11;
            }
        }
        public int Total_Columns
        {
            get
            {
                //return (int)(((Length - (Effective_Depth)) / Spacing_Cross_Girder) + 5);
                //return (int)(((Length) / Spacing_Cross_Girder) + 2);
                return 11;
            }
        }
        public double Skew_Angle { get; set; }
        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }




        public double Spacing_Long_Girder
        {

            get
            {
                //Chiranjit [2013 05 02]
                //return MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / 6.0).ToString("0.000"), 0.0);

                //double val = ((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (Number_Of_Long_Girder - 1));
                double val = ((WidthBridge) / (3 - 1));




                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //chiranji [2013 05 03]
                //return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);

                //double val = (Length - 2 * Effective_Depth) / (Number_Of_Cross_Girder - 1);
                double val = (Length / 16.0);
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(LL_Analysis_1_Input_File), "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(Working_Folder, "ANALYSIS_REP.TXT");
            }
        }


        //Chiranjit [2013 05 02]
        public int Number_Of_Long_Girder { get; set; }
        public int Number_Of_Cross_Girder { get; set; }

        public double Lwv { get; set; }
        public double Wkerb { get; set; }

        #endregion Properties

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
                    string pd = Path.Combine(Working_Folder, "Total Analysis");
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
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Live Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        //Chiranjit [2013 09 24]
        public string LL_Analysis_1_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 1");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_1_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_2_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 2");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_2_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_3_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 3");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_3_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_4_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 4");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_4_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_5_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 5");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_5_Input_File.txt");
                }
                return "";
            }
        }
        public string LL_Analysis_6_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 6");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_6_Input_File.txt");
                }
                return "";
            }
        }


        //Chiranjit [2012 05 27]
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
        public string Total_Analysis_Report
        {
            get
            {
                if (!File.Exists(TotalAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(TotalAnalysis_Input_File), "ANALYSIS_REP.TXT");

            }
        }
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(Working_Folder)) return "";
                return Path.Combine(Working_Folder, "ASTRA_DATA_FILE.TXT");

            }
        }
        public string LiveLoad_Analysis_Report
        {
            get
            {
                if (!File.Exists(LiveLoadAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(LiveLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
            }
        }
        public string DeadLoad_Analysis_Report
        {
            get
            {
                if (!File.Exists(DeadLoadAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
            }
        }


        public string Get_Analysis_Report_File(string input_path)
        {

            if (!File.Exists(input_path)) return "";

            return Path.Combine(Path.GetDirectoryName(input_path), "ANALYSIS_REP.TXT");


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
        public int NoOfInsideJoints
        {
            get
            {
                //return MyList.StringToInt(txt_cd_total_joints.Text, 0);
                return 1;
            }
        }
        #endregion Analysis Input File


        //Chiranjit [2013 05 02]
        string support_left_joints = "";
        string support_right_joints = "";

        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();


        //Chiranjit [2011 08 01]
        //Create Bridge Input Data by user's given values.
        //Long Girder Spacing, Cross Girder Spacing, Cantilever Width
        public void CreateData1()
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

            //double val1 = 12.1;
            double val1 = WidthBridge;
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

            Effective_Depth = Lwv;

            list_x.Clear();
            list_x.Add(0.0);

            last_x = Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = 3 * Length / 16.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = 5 * Length / 16.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = 3 * Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = 7 * Length / 16.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            int i = 0;
            for (i = 7; i >= 0; i--)
            {
                last_x = Length - list_x[i];
                list_x.Add(last_x);
            }




            last_x = x_incr + Effective_Depth;

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

            }
            while (last_x <= Length);
            list_x.Sort();


            list_z.Clear();
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

            } while (last_z <= WidthBridge);

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

            //support_left_joints = Joints_Array[0, 0].NodeNo + " TO " + Joints_Array[0, iCols - 1].NodeNo;
            //support_right_joints = Joints_Array[iRows - 1, 0].NodeNo + " TO " + Joints_Array[iRows - 1, iCols - 1].NodeNo;

            support_left_joints = "";
            support_right_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();

            //string str_joints = "";

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                    if (iCols == 0)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1)
                        support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else
                    {
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
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
            #region Chiranjit [2013 05 30]
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
            #endregion Chiranjit [2013 05 30]

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
        public void CreateData()
        {
            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;


            #region Chiranjit [2015 01 02]
            List<double> ls_x = new List<double>();
            double xx = 0.0;




            cl = Width_LeftCantilever;

            spc = (Length - Width_RightCantilever - Width_LeftCantilever) / (Number_Of_Long_Girder - 1);

            double _a = 0.5;
            double _b = (spc - 1.0) / 2.0;
            ls_x.Add(xx);
            ls_x.Add(cl - 0.5);
            ls_x.Add(cl);
            ls_x.Add(cl + 0.5);

            //ls_x.Add(Length);


            ls_x.Add(Length - (cl - 0.5));
            ls_x.Add(Length - cl);
            ls_x.Add(Length - (cl + 0.5));
            ls_x.Add(Length);

            xx = cl + 0.5 + _b;
            if (!ls_x.Contains(xx))
                ls_x.Add(xx);
            for (int f = 1; f < Number_Of_Long_Girder - 1; f++)
            {
                //if(f)
                //ls_x.Add(xx);

                xx += _b;
                if (!ls_x.Contains(xx))
                    ls_x.Add(xx);

                xx += _a;
                if (!ls_x.Contains(xx))
                    ls_x.Add(xx);

                xx += _a;
                if (!ls_x.Contains(xx))
                    ls_x.Add(xx);

                xx += _b;
                if (!ls_x.Contains(xx))
                    ls_x.Add(xx);

            }
            //ls_x.Add(xx);
            #endregion Chiranjit [2015 01 02]


            //ls_x.Sort();
            ls_x.Sort();

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            //int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            List<double> supp_dist = new List<double>();
            
            for (int v = 0; v < ls_x.Count; v++)
            {
                nd = new JointNode();
                nd.NodeNo = v + 1;
                nd.X = ls_x[v];
                Joints.Add(nd);

                //if(nd.NodeNo == 3 ||
                //    nd.NodeNo == 7 ||
                //    nd.NodeNo == 11 ||
                //    nd.NodeNo == 15 ||
                //    nd.NodeNo == 19 ||
                //    nd.NodeNo == 23 ||
                //    nd.NodeNo == 27 ||
                //    nd.NodeNo == 31 ||
                //    nd.NodeNo == 35 ||
                //    nd.NodeNo == 39)
                //{
                //    supp_dist.Add(nd.X);
                //}
                if ((nd.NodeNo % 4) == 3)
                {
                    supp_dist.Add(nd.X);
                }
            }





            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));
            skew_length = 0;
            //double val1 = 12.1;
            double val1 = WidthBridge;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();


            val1 = (Length - (Width_LeftCantilever + Width_RightCantilever)) / (Number_Of_Long_Girder - 1);


            val1 = Spacing_Cross_Girder;
            //supp_dist.Clear();
            //supp_dist.Add(Spacing_Cross_Girder * 2);
            //supp_dist.Add(Spacing_Cross_Girder * 6);
            //supp_dist.Add(Spacing_Cross_Girder * 10);
            //supp_dist.Add(Spacing_Cross_Girder * 14);

            //for (int c = 0; c < supp_dist.Count; c++)
            //{
            //    supp_dist[c] = MyList.StringToDouble(supp_dist[c].ToString("f3"), 0.0);
            //}


            //for (int c = 0; c < Number_Of_Long_Girder; c++)
            //{
            //    last_x = Width_LeftCantilever + c * val1;

            //    last_x = MyList.StringToDouble(last_x.ToString("f3"), 0.0);
            //    supp_dist.Add(last_x);
            //}

            supp_dist.Sort();
            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data
            bool flag = true;

            int i = 0;


            //list_x.AddRange(supp_dist.ToArray());
            //for (i = 0; i < 17; i++)
            //{
            //    last_x = Length * (i / 16.0);
            //    flag = true;
            //    for (int j = 0; j < list_x.Count; j++)
            //    {
            //        if (list_x[j].ToString("f2") == last_x.ToString("f2"))
            //        {
            //            flag = false; break;
            //        }
            //    }
            //    if (flag)
            //        list_x.Add(last_x);
            //}

            //last_x = x_incr + Effective_Depth;

            //list_x.Sort();

            //list_z.Clear();
            //list_z.Add(0);

            //last_z = WidthBridge;
            //last_z = 0.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //if (list_z.Contains(last_z) == false)
            //    list_z.Add(last_z);

            //last_z = z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true; break;
            //        }
            //    }

            //    if (!flag)
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            //} while (last_z <= WidthBridge);

            //list_z.Sort();
            #endregion Chiranjit [2011 09 23] Correct Create Data



            _Columns = list_x.Count;
            _Rows = list_z.Count;

            //int i = 0;

            List<double> list = new List<double>();

            //for (iRows = 0; iRows < _Rows; iRows++)
            //{
            //    list = new List<double>();
            //    for (iCols = 0; iCols < _Columns; iCols++)
            //    {
            //        list.Add(list_x[iCols] + list_z[iRows] * skew_length);
            //    }
            //    z_table.Add(list_z[iRows], list);
            //}

            //Joints_Array = new JointNode[_Rows, _Columns];
            //Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            //Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];



            //for (iRows = 0; iRows < _Rows; iRows++)
            //{
            //    list_x = z_table[list_z[iRows]] as List<double>;
            //    for (iCols = 0; iCols < _Columns; iCols++)
            //    {
            //        nd = new JointNode();
            //        nd.Y = 0;
            //        nd.Z = list_z[iRows];

            //        //nd.X = list_x[iCols] + (skew_length * list_z[iRows]);
            //        nd.X = list_x[iCols];

            //        nd.NodeNo = Joints.JointNodes.Count + 1;
            //        Joints.Add(nd);

            //        Joints_Array[iRows, iCols] = nd;

            //        last_x = nd.X;
            //    }
            //}
            int nodeNo = 0;
            //Joints.Clear();

            //support_left_joints = Joints_Array[0, 0].NodeNo + " TO " + Joints_Array[0, iCols - 1].NodeNo;
            //support_right_joints = Joints_Array[iRows - 1, 0].NodeNo + " TO " + Joints_Array[iRows - 1, iCols - 1].NodeNo;

            support_left_joints = "";
            support_right_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();

            //string str_joints = "";

            //for (iCols = 0; iCols < _Columns; iCols++)
            //{
            //    for (iRows = 0; iRows < _Rows; iRows++)
            //    {
            //        nodeNo++;
            //        Joints_Array[iRows, iCols].NodeNo = nodeNo;
            //        Joints.Add(Joints_Array[iRows, iCols]);

            //        if (iCols == 0)
            //            support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
            //        else if (iCols == _Columns - 1)
            //            support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
            //        else
            //        {
            //            if (iRows > 0 && iRows < _Rows - 1)
            //                list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
            //        }
            //    }
            //    if (list_nodes.Count > 0)
            //    {
            //        joints_list_for_load.Add(MyList.Get_Array_Text(list_nodes));
            //        list_nodes.Clear();
            //    }
            //}

            support_left_joints = "";
            support_right_joints = "";

            for (i = 0; i < Joints.Count; i++)
            {
                if (supp_dist.Contains(Joints[i].X))
                {
                    if (i > Joints.Count / 2)
                        support_right_joints += Joints[i].NodeNo + " ";
                    else
                        support_left_joints += Joints[i].NodeNo + " ";
                }
            }



            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();

            for (iCols = 1; iCols < Joints.Count; iCols++)
            {
                mem = new Member();
                mem.StartNode = Joints[iCols - 1];
                mem.EndNode = Joints[iCols];
                mem.MemberNo = iCols;
                MemColls.Add(mem);
            }


            #region Chiranjit [2013 05 30]
            //for (iCols = 0; iCols < _Columns; iCols++)
            //{
            //    for (iRows = 1; iRows < _Rows; iRows++)
            //    {
            //        mem = new Member();
            //        mem.StartNode = Joints_Array[iRows - 1, iCols];
            //        mem.EndNode = Joints_Array[iRows, iCols];
            //        mem.MemberNo = MemColls.Count + 1;
            //        MemColls.Add(mem);
            //        //Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
            //    }
            //}
            //for (iRows = 0; iRows < _Rows; iRows++)
            //{
            //    for (iCols = 1; iCols < _Columns; iCols++)
            //    {
            //        mem = new Member();
            //        mem.StartNode = Joints_Array[iRows, iCols - 1];
            //        mem.EndNode = Joints_Array[iRows, iCols];
            //        mem.MemberNo = MemColls.Count + 1;
            //        MemColls.Add(mem);
            //        //Long_Girder_Members_Array[iRows, iCols - 1] = mem;

            //    }
            //}
            #endregion Chiranjit [2013 05 30]

            #region Chiranjit [2013 06 06]

            //if (Width_LeftCantilever > 0)
            //{
            //    //list_envelop_outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            //    //list_envelop_inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
            //}
            //else
            //{
            //    //list_envelop_outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
            //    //list_envelop_inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            //}

            //list_envelop_outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;

            #endregion Chiranjit [2013 06 06]

            Set_Inner_Outer_Cross_Girders();
        }

        public void CreateData_DeadLoad()
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
            double L_2, L_4, eff_d, L_8;
            double x_max, x_min;

            last_x = 0.0;


            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();

            int i = 0;

            for (i = 0; i < 17; i++)
            {
                list_x.Add(Length * (i / 16.0));
            }



            bool flag = true;

            list_x.Sort();


            list_z.Add(0);
            last_z = Width_LeftCantilever;

            list_z.Sort();
            #endregion Chiranjit [2011 09 23] Correct Create Data

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

        public string Inner_Girders_as_String { get; set; }
        public string Outer_Girders_as_String { get; set; }
        public string Cross_Girders_as_String { get; set; }
        //Chiranjit [2011 07 11]
        //Write Default data as given Astra Examples 8


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


        public void WriteData_Total_Analysis(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
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

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
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

            list.Add("SECTION PROPERTIES");
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));

            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));
            //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
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
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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
            //1 TO 82 PRIS YD 0.2 ZD 1
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));

            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add(string.Format("{0}  PINNED", support_left_joints));
            list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("1 TO 220 UNI GY -0.0001");

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);

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
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }
        public void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_loads)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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
            //1 TO 82 PRIS YD 0.2 ZD 1
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));

            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  {1} FIXED BUT FX MZ", support_left_joints, support_right_joints));

            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("1 TO 220 UNI GY -0.0001");

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);

            list.Add("TYPE 1 IRCCLASSA 1.179");
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
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());

            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));

            if (ll_loads.Count > 0)
            {
                string fn = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
                File.WriteAllLines(fn, ll_loads.ToArray());
            }
            list.Clear();
        }

        public void WriteData_DeadLoad_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
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
            //1 TO 82 PRIS YD 0.2 ZD 1
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));


            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));
            list.Add(string.Format("{0} {1}  FIXED BUT FX MZ", support_left_joints, support_right_joints));
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD");
            list.Add("MEMBER LOAD");
            list.Add(string.Format("{0} UNI GY -2.754", Outer_Girders_as_String));
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }

        public void Clear()
        {
            Joints_Array = null;
            Long_Girder_Members_Array = null;
            Cross_Girder_Members_Array = null;
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
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ld = new LoadData(Live_Load_List[6]);

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

            ld = new LoadData(Live_Load_List[6]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);



            //70 RW + 1 Lane Class A 
            ld = new LoadData(Live_Load_List[6]);
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
    }

    public class RCC_Deckslab_Excel_Update_Extend : List<Deck_Data>
    {
        public string Excel_File_Name { get; set; }
        public string Report_File_Name { get; set; }
        public Excel_User_Inputs Deckslab_User_Inputs { get; set; }
        public Excel_User_Inputs Deckslab_User_Live_loads { get; set; }
        public Excel_User_Inputs Deckslab_Design_Inputs { get; set; }

        public List<LoadData> llc = null;

        public List<DeckSlabLoads> Deck_Loads { get; set; }
        public RCC_Deckslab_Excel_Update_Extend()
            : base()
        {
            Excel_File_Name = "";
            Report_File_Name = "";

            Deckslab_User_Inputs = new Excel_User_Inputs();
            Deckslab_User_Live_loads = new Excel_User_Inputs();
            Deckslab_Design_Inputs = new Excel_User_Inputs();
        }

        public void Read_Update_Data()
        {
            Clear();
            if (!File.Exists(Report_File_Name)) return;

            List<string> list = new List<string>(File.ReadAllLines(Report_File_Name));
            MyList mlist = null;

            int i = 0;
            string kStr = "";
            Deck_Data dc = null;
            int indx = 0;
            for (i = 0; i < list.Count; i++)
            {

                kStr = MyList.RemoveAllSpaces(list[i]);
                if (kStr == "" || kStr.StartsWith("--------------")) continue;

                mlist = new MyList(kStr, ' ');
                dc = new Deck_Data();
                try
                {
                    indx = mlist.Count - 1;

                    //for (int j = 0; j < 15; j++)
                    //{
                    //    dc.Add(mlist.GetDouble(indx--));
                    //}


                    //for (int j = 0; j < ; j++)
                    //{
                    //    dc.Add(mlist.GetDouble(indx--));
                    //}


                    for (int j = mlist.Count - 1; j > 0; j--)
                    {
                        //try
                        dc.Add(mlist.GetDouble(j));
                    }

                }
                catch (Exception ex) { }


                if (dc.Count > 1)
                {
                    dc.Reverse();
                    this.Add(dc);
                }
            }
            kStr = "";



            #region Update_ExcelData

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["5.Design"];

            if(((this[0].Count / 4) + 1) == 3)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["3Girder_Design"];
            }
            else if (((this[0].Count / 4) + 1) == 5)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["5Girder_Design"];

            }
            else if (((this[0].Count / 4) + 1) == 6)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["6Girder_Design"];

            }

            String cellFormulaAsString = myExcelWorksheet.get_Range("A1", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            //int cel_index = 30;
            //int cel_index = 32;
            int cel_index = 26;

            int ci = (int)('B');


            char c = (char)ci;
            #region DL_Self_Weight
            for (i = 0; i < Count; i++)
            {
                ci = (int)('B');
                for (int j = 0; j < (this[i].Count); j++)
                {
                    c = (char)ci++;
                    myExcelWorksheet.get_Range(c.ToString() + (cel_index + i), misValue).Formula = this[i][j].ToString();

                }

                //myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                //myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                //myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                //myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                //myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                //myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion DL_Self_Weight



            #region User Inputs

            if (Deckslab_User_Inputs != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["1.Input"];



                List<int> lst_F = new List<int>();

                int cnt = 0;
                for (i = 4; i <= 49; i++)
                {
                    if (i == 5 ||
                        i == 16 ||
                        i == 19 ||
                        i == 30 ||
                        i == 31 ||
                        i == 36 ||
                        i == 37 ||
                        (i > 43 && i < 49))
                    {
                        continue;
                    }
                    lst_F.Add(i);

                    Deckslab_User_Inputs[cnt++].Excel_Cell_Reference = "F" + i;
                }


                Excel.Range ran = myExcelWorksheet.get_Range("F4:F50", misValue);
                //myExcelWorksheet.Range["F4:F46"].Locked = false;
                //if ((bool)ran.Locked)
                //{
                //    MessageBox.Show("");
                //}
                myExcelWorksheet.Unprotect("2011ap");
                ran.Locked = false;
                for (i = 0; i < Deckslab_User_Inputs.Count; i++)
                {
                    //myExcelWorksheet.get_Range("F" + lst_F[i].ToString(), misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                    myExcelWorksheet.get_Range(Deckslab_User_Inputs[i].Excel_Cell_Reference, misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                }
                ran.Locked = true;
                myExcelWorksheet.Protect("2011ap");

            }


            #endregion User Inputs

            Deckslab_User_Live_loads.Clear();


            #region Live Loads COmbinations

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["2.LiveLoad"];

            //double max_val = llc[0].Get_Maximum_Load();


            double max_val = 0.0;

            double val = (max_val * 10 / 2.0);

            List<string> lst_xls = new List<string>();


            //lst_xls.Add(string.Format("L28"));
            //lst_xls.Add(string.Format("L29"));
            //lst_xls.Add(string.Format("F105"));
            //lst_xls.Add(string.Format("F106"));
            //lst_xls.Add(string.Format("M105"));
            //lst_xls.Add(string.Format("M106"));
            //lst_xls.Add(string.Format("G369"));
            //lst_xls.Add(string.Format("M369"));
            //lst_xls.Add(string.Format("G374"));
            //lst_xls.Add(string.Format("M374"));

            if (Deck_Loads.Count >= 1)
            {
                var dsl = Deck_Loads[0];

                myExcelWorksheet.get_Range("L28", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("L29", misValue).Formula = dsl.lw2;
                myExcelWorksheet.get_Range("L30", misValue).Formula = dsl.wacc;
                myExcelWorksheet.get_Range("L31", misValue).Formula = dsl.walg;


                myExcelWorksheet.get_Range("L32", misValue).Formula = dsl.dTraffic;
                myExcelWorksheet.get_Range("L33", misValue).Formula = dsl.dTrans;
                myExcelWorksheet.get_Range("L34", misValue).Formula = dsl.min_ed;
                myExcelWorksheet.get_Range("L34", misValue).Formula = dsl.min_ed;

                myExcelWorksheet.get_Range("L40", misValue).Formula = dsl.impf;
            }
            if (Deck_Loads.Count >= 2)
            {
                var dsl = Deck_Loads[1];

                myExcelWorksheet.get_Range("F105", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M105", misValue).Formula = dsl.lw2;
                myExcelWorksheet.get_Range("F106", misValue).Formula = dsl.lw3;
                myExcelWorksheet.get_Range("M106", misValue).Formula = dsl.lw4;
                myExcelWorksheet.get_Range("M107", misValue).Formula = dsl.dTraffic;
                myExcelWorksheet.get_Range("M108", misValue).Formula = dsl.dTrans;
            }
            if (Deck_Loads.Count >= 3)
            {
                var dsl = Deck_Loads[2];

                myExcelWorksheet.get_Range("G161", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M161", misValue).Formula = dsl.lw2;
                myExcelWorksheet.get_Range("G163", misValue).Formula = dsl.max_tyre_pressure;
                myExcelWorksheet.get_Range("G164", misValue).Formula = dsl.wacc;
                myExcelWorksheet.get_Range("G165", misValue).Formula = dsl.dTraffic;
                myExcelWorksheet.get_Range("I165", misValue).Formula = dsl.dTrans;
                myExcelWorksheet.get_Range("I167", misValue).Formula = dsl.impf;
            }

            if (Deck_Loads.Count >= 4)
            {
                var dsl = Deck_Loads[3];
                myExcelWorksheet.get_Range("G229", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M229", misValue).Formula = dsl.lw2;
                myExcelWorksheet.get_Range("G230", misValue).Formula = dsl.lw3;
                myExcelWorksheet.get_Range("M230", misValue).Formula = dsl.lw4;
                myExcelWorksheet.get_Range("G232", misValue).Formula = dsl.max_tyre_pressure;
                myExcelWorksheet.get_Range("G233", misValue).Formula = dsl.wacc;
                myExcelWorksheet.get_Range("G234", misValue).Formula = dsl.dTraffic;
                myExcelWorksheet.get_Range("G235", misValue).Formula = dsl.impf;
            }
            if (Deck_Loads.Count >= 5)
            {
                var dsl = Deck_Loads[4];
                myExcelWorksheet.get_Range("G297", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M297", misValue).Formula = dsl.lw2;
                myExcelWorksheet.get_Range("G298", misValue).Formula = dsl.wacc;
                myExcelWorksheet.get_Range("M298", misValue).Formula = dsl.walg;
                myExcelWorksheet.get_Range("G299", misValue).Formula = dsl.dTrans;
                myExcelWorksheet.get_Range("G300", misValue).Formula = dsl.impf;
            }
            if (Deck_Loads.Count >= 6)
            {
                var dsl = Deck_Loads[5];
                myExcelWorksheet.get_Range("G363", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M363", misValue).Formula = dsl.lw2;
                myExcelWorksheet.get_Range("G364", misValue).Formula = dsl.max_tyre_pressure;

                myExcelWorksheet.get_Range("G365", misValue).Formula = dsl.wacc;
                myExcelWorksheet.get_Range("G366", misValue).Formula = dsl.dTrans;
                myExcelWorksheet.get_Range("G367", misValue).Formula = dsl.impf;
            }
            if (Deck_Loads.Count >= 7)
            {
                var dsl = Deck_Loads[6];
                myExcelWorksheet.get_Range("G369", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M369", misValue).Formula = dsl.lw2;


                myExcelWorksheet.get_Range("G374", misValue).Formula = dsl.lw1;
                myExcelWorksheet.get_Range("M374", misValue).Formula = dsl.lw2;

                myExcelWorksheet.get_Range("G370", misValue).Formula = dsl.wacc;
                myExcelWorksheet.get_Range("M370", misValue).Formula = dsl.walg;
                myExcelWorksheet.get_Range("G371", misValue).Formula = dsl.dTraffic;
                myExcelWorksheet.get_Range("G372", misValue).Formula = dsl.min_ed;
                myExcelWorksheet.get_Range("G375", misValue).Formula = dsl.impf;
            }
            #endregion Live Loads

            #region Comment
            //#region Live Loads

            //myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["2.LiveLoad"];

            ////double max_val = llc[0].Get_Maximum_Load();


            // max_val = llc[0].Get_Maximum_Load();

            // val = (max_val * 10 / 2.0);

            //lst_xls = new List<string>();

            ////lst_xls.Add(string.Format("57"));
            //lst_xls.Add(string.Format("L28"));
            //lst_xls.Add(string.Format("L29"));
            //lst_xls.Add(string.Format("F105"));
            //lst_xls.Add(string.Format("F106"));
            //lst_xls.Add(string.Format("M105"));
            //lst_xls.Add(string.Format("M106"));
            //lst_xls.Add(string.Format("G369"));
            //lst_xls.Add(string.Format("M369"));
            //lst_xls.Add(string.Format("G374"));
            //lst_xls.Add(string.Format("M374"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            //}

            //lst_xls.Clear();
            //max_val = llc[3].Get_Maximum_Load();
            //val = (max_val * 10);
            ////val = (max_val * 10 / 2.0);

            ////lst_xls.Add(string.Format("100"));
            //lst_xls.Add(string.Format("G161"));
            //lst_xls.Add(string.Format("M161"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            //}
            ////lst_xls.Add(string.Format(""));
            ////lst_xls.Add(string.Format(""));
            ////lst_xls.Add(string.Format(""));
            ////lst_xls.Add(string.Format(""));
            //lst_xls.Clear();
            //max_val = llc[4].Get_Maximum_Load();
            //val = (max_val * 10);
            ////val = (max_val * 10 / 2.0);
            ////lst_xls.Add(string.Format("50"));
            //lst_xls.Add(string.Format("G229"));
            //lst_xls.Add(string.Format("G230"));
            //lst_xls.Add(string.Format("M229"));
            //lst_xls.Add(string.Format("M230"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            //}
            ////lst_xls.Add(string.Format(""));
            //lst_xls.Clear();
            //max_val = llc[1].Total_Loads / 2.0; ;
            //val = (max_val * 10);
            ////lst_xls.Add(string.Format("35"));
            //lst_xls.Add(string.Format("G297"));
            //lst_xls.Add(string.Format("M297"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            //}
            ////lst_xls.Add(string.Format(""));
            ////lst_xls.Add(string.Format(""));
            //lst_xls.Clear();
            //max_val = llc[2].Get_Maximum_Load();
            //val = (max_val * 10 / 2.0);
            ////lst_xls.Add(string.Format("85"));
            //lst_xls.Add(string.Format("G363"));
            //lst_xls.Add(string.Format("M363"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();
            //}

            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("2.600"));
            //lst_xls.Add(string.Format("I12"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[0].Input_Value;
            //}

            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("0"));
            //lst_xls.Add(string.Format("B24"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[1].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("250"));
            //lst_xls.Add(string.Format("L30"));
            //lst_xls.Add(string.Format("G370"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[5].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("500"));
            //lst_xls.Add(string.Format("L31"));
            //lst_xls.Add(string.Format("M370"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[6].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("1.200"));
            //lst_xls.Add(string.Format("L32"));
            //lst_xls.Add(string.Format("G371"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[7].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("1.800"));
            //lst_xls.Add(string.Format("L33"));
            //lst_xls.Add(string.Format("M108"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[8].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("0.400"));
            //lst_xls.Add(string.Format("L34"));
            //lst_xls.Add(string.Format("G372"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[9].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("5.273"));
            //lst_xls.Add(string.Format("G166"));
            //lst_xls.Add(string.Format("G232"));
            //lst_xls.Add(string.Format("G364"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[2].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("810"));
            //lst_xls.Add(string.Format("G164"));
            //lst_xls.Add(string.Format("G365"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[10].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("1.22"));
            //lst_xls.Add(string.Format("G165"));
            //lst_xls.Add(string.Format("G234"));
            //lst_xls.Add(string.Format("G366"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[3].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("1.93"));
            //lst_xls.Add(string.Format("I166"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[11].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("1.25"));
            //lst_xls.Add(string.Format("I167"));
            //lst_xls.Add(string.Format("G235"));
            //lst_xls.Add(string.Format("G300"));
            //lst_xls.Add(string.Format("G367"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[4].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("360"));
            //lst_xls.Add(string.Format("G233"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[12].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("840"));
            //lst_xls.Add(string.Format("G298"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[13].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("30"));
            //lst_xls.Add(string.Format("G299"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[14].Input_Value;
            //}


            //lst_xls.Clear();
            ////lst_xls.Add(string.Format("4570"));
            //lst_xls.Add(string.Format("M298"));
            //for (i = 0; i < lst_xls.Count; i++)
            //{
            //    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[15].Input_Value;
            //}
            //#endregion Live Loads

            #endregion


            #region Design User Inputs

            if (Deckslab_Design_Inputs != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["5.Design"];



                lst_xls.Clear();
                //lst_xls.Add(string.Format("16"));
                lst_xls.Add(string.Format("G87"));
                lst_xls.Add(string.Format("E111"));
                lst_xls.Add(string.Format("E115"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[0].Input_Value;
                }

                lst_xls.Clear();
                //lst_xls.Add(string.Format("1000"));
                lst_xls.Add(string.Format("B92"));
                lst_xls.Add(string.Format("N205"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[1].Input_Value;
                }


                lst_xls.Clear();
                //lst_xls.Add(string.Format("10"));
                lst_xls.Add(string.Format("I111"));
                lst_xls.Add(string.Format("I115"));
                lst_xls.Add(string.Format("F134"));
                lst_xls.Add(string.Format("F141"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[2].Input_Value;
                }


                lst_xls.Clear();
                //lst_xls.Add(string.Format("200"));
                lst_xls.Add(string.Format("G111"));
                lst_xls.Add(string.Format("G115"));
                lst_xls.Add(string.Format("K111"));
                lst_xls.Add(string.Format("K115"));
                lst_xls.Add(string.Format("H134"));
                lst_xls.Add(string.Format("H141"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[3].Input_Value;
                }


                lst_xls.Clear();
                //lst_xls.Add(string.Format("0.8"));
                lst_xls.Add(string.Format("N188"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[4].Input_Value;
                }

                lst_xls.Clear();
                //lst_xls.Add(string.Format("0.5"));
                lst_xls.Add(string.Format("N191"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[5].Input_Value;
                }

                lst_xls.Clear();
                //lst_xls.Add(string.Format("0.5"));
                lst_xls.Add(string.Format("N216"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[6].Input_Value;
                }

            }


            #endregion User Inputs

            try
            {
                myExcelWorkbook.Save();
                //myExcelWorkbook.Close(true, fileName, null);
                Marshal.ReleaseComObject(myExcelWorkbook);
            }
            catch (Exception ex) { }
            #endregion Update_ExcelData
        }
    }

}
