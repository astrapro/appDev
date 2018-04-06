using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;


namespace BridgeAnalysisDesign.RCC_T_Girder
{
    public partial class frm_RCC_TBeam_LS : Form
    {
        const string Title = "ANALYSIS OF RCC T-GIRDER BRIDGE (LIMIT STATE METHOD)";
        IApplication iApp;
        TGirderAnalysis Bridge_Analysis = null;
        LongMainGirders LongGirders = null;
        CrossGirders XGirders = null;
        DeckSlab Deck = null;
        CantileverSlab Cant = null;

        TLong_SectionProperties long_inner_sec;
        TLong_SectionProperties long_out_sec;
        TCross_SectionProperties cross_sec;

        //Chiranjit [2012 06 08]
        RccPier rcc_pier = null;

        //Chiranjit [2012 05 27]
        RCC_AbutmentWall Abut = null;


        bool IsCreateData = true;
        //bool IsInnerGirder
        public frm_RCC_TBeam_LS(IApplication thisApp)
        {
            InitializeComponent();
            iApp = thisApp;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            Result = new List<string>();
        }

        #region Chiranjit [2012 06 20]
        //Chiranjit [2012 06 20]
        //Define Properties
        public double L { get { return MyList.StringToDouble(txt_Ana_L.Text, 26.0); } set { txt_Ana_L.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }
        public double CW { get { return MyList.StringToDouble(txt_Ana_CW.Text, 0.0); } set { txt_Ana_CW.Text = value.ToString("f3"); } }
        public double CL { get { return MyList.StringToDouble(txt_Ana_CL.Text, 0.0); } set { txt_Ana_CL.Text = value.ToString("f3"); } }
        public double CR { get { return MyList.StringToDouble(txt_Ana_CR.Text, 0.0); } set { txt_Ana_CR.Text = value.ToString("f3"); } }
        public double Ds { get { return MyList.StringToDouble(txt_Ana_Ds.Text, 0.0); } set { txt_Ana_Ds.Text = value.ToString("f3"); } }
        public double Y_c { get { return MyList.StringToDouble(txt_Ana_gamma_c.Text, 0.0); } set { txt_Ana_gamma_c.Text = value.ToString("f3"); } }
        public double Ang { get { return MyList.StringToDouble(txt_Ana_ang.Text, 0.0); } set { txt_Ana_ang.Text = value.ToString("f3"); } }
        public double NMG { get { return MyList.StringToDouble(txt_Ana_NMG.Text, 0.0); } set { txt_Ana_NMG.Text = value.ToString("f3"); } }
        public double DMG { get { return MyList.StringToDouble(txt_Ana_DMG.Text, 0.0); } set { txt_Ana_DMG.Text = value.ToString("f3"); } }
        public double Deff { get { return (DMG - 0.0500 - 0.016 - 6 * 0.032); } }
        public double BMG { get { return MyList.StringToDouble(txt_Ana_BMG.Text, 0.0); } set { txt_Ana_BMG.Text = value.ToString("f3"); } }
        public double NCG { get { return MyList.StringToDouble(txt_Ana_NCG.Text, 0.0); } set { txt_Ana_NCG.Text = value.ToString("f3"); } }
        public double DCG { get { return MyList.StringToDouble(txt_Ana_DCG.Text, 0.0); } set { txt_Ana_DCG.Text = value.ToString("f3"); } }
        public double BCG { get { return MyList.StringToDouble(txt_Ana_BCG.Text, 0.0); } set { txt_Ana_BCG.Text = value.ToString("f3"); } }
        public double Dw { get { return MyList.StringToDouble(txt_Ana_Dw.Text, 0.0); } set { txt_Ana_Dw.Text = value.ToString("f3"); } }
        public double Y_w { get { return MyList.StringToDouble(txt_Ana_gamma_w.Text, 0.0); } set { txt_Ana_gamma_w.Text = value.ToString("f3"); } }
        public double Hp { get { return MyList.StringToDouble(txt_Ana_Hp.Text, 0.0); } set { txt_Ana_Hp.Text = value.ToString("f3"); } }
        public double Wp { get { return MyList.StringToDouble(txt_Ana_Wp.Text, 0.0); } set { txt_Ana_Wp.Text = value.ToString("f3"); } }
        public double Bs { get { return MyList.StringToDouble(txt_Ana_Bs.Text, 0.0); } set { txt_Ana_Bs.Text = value.ToString("f3"); } }
        public double Hs { get { return MyList.StringToDouble(txt_Ana_Hs.Text, 0.0); } set { txt_Ana_Hs.Text = value.ToString("f3"); } }
        public double Wps { get { return MyList.StringToDouble(txt_Ana_Wps.Text, 0.0); } set { txt_Ana_Wps.Text = value.ToString("f3"); } }
        public double Hps { get { return MyList.StringToDouble(txt_Ana_Hps.Text, 0.0); } set { txt_Ana_Hps.Text = value.ToString("f3"); } }
        public double swf { get { return MyList.StringToDouble(txt_Ana_swf.Text, 0.0); } set { txt_Ana_swf.Text = value.ToString("f3"); } }

        #endregion Chiranjit [2012 06 20]


        //Chiranjit [2011 11 04] for Display Results
        public List<string> Result { get; set; }
        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
            }
        }
        public string user_path { get; set; }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
            }
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
        public string Design_Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(Drawing_Folder, "DESIGN DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(Drawing_Folder, "DESIGN DRAWINGS"));
                return Path.Combine(Drawing_Folder, "DESIGN DRAWINGS");
            }
        }
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

        #region Form Events
        private void frm_RCC_TBeam_Load(object sender, EventArgs e)
        {
            #region Analysis Data
            Bridge_Analysis = new TGirderAnalysis(iApp);
            Ana_Fill_Default_Moving_LoadData(dgv_live_load);

            #endregion Analysis Data

            #region Limit State Long Girder
            Limit_LongGirder = new LimitState_LongGirder(iApp);
            dgv_LS_long_layers.Rows.Add(1, 32, 4, 82);
            dgv_LS_long_layers.Rows.Add(2, 32, 4, 146);
            dgv_LS_long_layers.Rows.Add(3, 32, 4, 210);
            dgv_LS_long_layers.Rows.Add(4, 32, 4, 274);
            dgv_LS_long_layers.Rows.Add(5, 32, 0, 338);
            dgv_LS_long_layers.Rows.Add(6, 32, 0, 402);
            dgv_LS_long_layers.Rows.Add(7, 32, 0, 466);

            cmb_LS_long_Cgrade.SelectedIndex = 2;
            cmb_LS_long_Sgrade.SelectedIndex = 1;
            #endregion Limit State Long Girder


            Button_Enable_Disable();

            txt_Ana_B.Text = txt_Ana_B.Text + "";
            Text_Changed();
        }
        private void btn_WorkSheet_Design_Click(object sender, EventArgs e)
        {
            string excel_file_name = "";
            string copy_path = "";
            Button btn = sender as Button;
             
            //if (btn.Name == btn_WSD_Tee_Girders.Name)
            //{
            //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 2\04 TeeGirder Design.xls");
            //}
            //else if (btn.Name == btn_WSD_Open.Name)
            //{
            //    iApp.Open_WorkSheet_Design();
            //    return;
            //}

            copy_path = Path.Combine(user_path, Path.GetFileName(excel_file_name));

            if (File.Exists(excel_file_name))
            {
                iApp.OpenExcelFile(Worksheet_Folder, excel_file_name, "2011ap");
            }
        }
        private void btn_Drawings_Open_Click(object sender, EventArgs e)
        {
            if (lst_Drawings.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Select a Drawing from the above list."); return;
            }


            string dwg = lst_Drawings.SelectedItem.ToString();
            string User_Drawing_Folder = "";
            string drawing_command = "";

            switch (dwg)
            {
                case "Drawings T_Girder Bridge Span 11m":
                    drawing_command = "T_Girder_Span_11m";
                    User_Drawing_Folder = "RCC Superstructure Span 11m";
                    break;
                case "Drawings T_Girder Bridge Span 12m":
                    drawing_command = "T_Girder_Span_12m";
                    User_Drawing_Folder = "RCC Superstructure Span 12m";
                    break;
                case "Drawings T_Girder Bridge Span 13m":
                    drawing_command = "T_Girder_Span_13m";
                    User_Drawing_Folder = "RCC Superstructure Span 13m";
                    break;
                case "Drawings T_Girder Bridge Span 14_16m":
                    drawing_command = "T_Girder_Span_14_16m";
                    User_Drawing_Folder = "RCC Superstructure Span 14_16m";
                    break;
                case "Drawings T_Girder Bridge Span 17m":
                    drawing_command = "T_Girder_Span_17m";
                    User_Drawing_Folder = "RCC Superstructure Span 17m";
                    break;
                case "Drawings T_Girder Bridge Span 18_19m":
                    drawing_command = "T_Girder_Span_18_19m";
                    User_Drawing_Folder = "RCC Superstructure Span 18_19m";
                    break;
                case "Drawings T_Girder Bridge Span 20_26m":
                    drawing_command = "T_Girder_Span_20_26m";
                    User_Drawing_Folder = "RCC Superstructure Span 20_26m";
                    break;
                case "Drawings T_Girder Bridge Span 27m":
                    drawing_command = "T_Girder_Span_27m";
                    User_Drawing_Folder = "RCC Superstructure Span 27m";
                    break;
                case "Drawings T_Girder Bridge Span 28m":
                    User_Drawing_Folder = "RCC Superstructure Span 28m";
                    drawing_command = "T_Girder_Span_28m";
                    break;
            }

            User_Drawing_Folder = Path.Combine(Drawing_Folder, User_Drawing_Folder);
            if (!Directory.Exists(User_Drawing_Folder))
                Directory.CreateDirectory(User_Drawing_Folder);
            iApp.RunViewer(User_Drawing_Folder, drawing_command);

        }
        private void btn_dwg_long_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b.Name == btn_dwg_long.Name)
                iApp.SetDrawingFile_Path(LongGirders.drawing_path, "TBEAM_Long_Girder", Path.Combine(Drawing_Folder, "Long Girder Drawing"), "");
            if (b.Name == btn_dwg_cross.Name)
                iApp.SetDrawingFile_Path(XGirders.user_drawing_file, "TBEAM_Cross_Girder", Path.Combine(Drawing_Folder, "Cross Girder Drawing"), "");
            if (b.Name == btn_dwg_deck.Name)
                iApp.SetDrawingFile_Path(Deck.drawing_path, "TBEAM_Deck_Slab", Path.Combine(Drawing_Folder, "RCC Deck Slab Drawing"), "");
            if (b.Name == btn_dwg_cant.Name)
                iApp.SetDrawingFile_Path(Cant.user_drawing_file, "TBEAM_Cantilever", Path.Combine(Drawing_Folder, "RCC Cantilever Drawing"), "");
            if (b.Name == btn_dwg_rcc_TBEAM.Name)
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC T-BEAM Worksheet Drawings"), "TBEAM_Worksheet_Design");
            if (b.Name == btn_dwg_abutment.Name)
            {
                //Chiranjit [2012 11 08]
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "TBeam_Abutment");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "TBeam_Abutment");
            }
            if (b.Name == btn_dwg_rcc_pier.Name)
            {
                //Chiranjit [2012 11 08]
                //iApp.RunViewer(Path.GetDirectoryName(rcc_pier.rep_file_name), "RCC_Pier_Default_Drawings");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "TBeam_Pier");
            }
            //if (b.Name == btn_dwg_long.Name)
            //    iApp.SetDrawingFile_Path(LongGirders.drawing_path, "TBEAM_Long_Girder", "");
            //if (b.Name == btn_dwg_cross.Name)
            //    iApp.SetDrawingFile_Path(XGirders.user_drawing_file, "TBEAM_Cross_Girder", "");
            //if (b.Name == btn_dwg_deck.Name)
            //    iApp.SetDrawingFile_Path(Deck.drawing_path, "TBEAM_Deck_Slab", "");
            //if (b.Name == btn_dwg_cant.Name)
            //    iApp.SetDrawingFile_Path(Cant.user_drawing_file, "TBEAM_Cantilever", "");
            //if (b.Name == btn_dwg_rcc_TBEAM.Name)
            //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Worksheet Drawings"), "TBEAM_Worksheet_Design2");
            //if (b.Name == btn_dwg_cant_RW.Name)
            //    iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
            //if (b.Name == btn_dwg_rcc_pier.Name)
            //    iApp.RunViewer(Path.GetDirectoryName(rcc_pier.rep_file_name), "RCC_Pier_Default_Drawings");
        }
        #endregion Form Events

        #region Bridge Deck Analysis Form Events
        private void cmb_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                {
                    txt_Ana_X.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].Distance.ToString("f4"); // Chiranjit [2013 05 28] Kolkata
                    txt_Load_Impact.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].ImpactFactor.ToString("f3");
                }
            }
            catch (Exception ex) { }
        }
        private void btn_update_force_Click(object sender, EventArgs e)
        {
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (File.Exists(ana_rep_file))
            {
                //Bridge_Analysis.Girder_Analysis = null;
                //Bridge_Analysis.Girder_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file, GetForceType());

                Bridge_Analysis.Girder_Analysis.ForceType = GetForceType();
                Ana_Show_Moment_Shear();

                MessageBox.Show(this, "Force Data Updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;

            Button_Enable_Disable();
        }

        public void Open_Create_Data()
        {

            try
            {
                Ana_Initialize_Analysis_InputData();

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;
                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);

                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                     Bridge_Analysis.joints_list_for_load);

                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File);
                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);

                Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);

                Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;


            }
            catch (Exception ex) { }
        }
        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            Write_All_Data(true);

            user_path = IsCreateData ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;


            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                Ana_Initialize_Analysis_InputData();

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;
                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);

                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                     Bridge_Analysis.joints_list_for_load);

                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File);
                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);

                Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);

                Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                //Chiranjit [2012 11 07]
                //cmb_load_type.Items.Clear();
                //for (int i = 0; i < Bridge_Analysis.Live_Load_List.Count; i++)
                //{
                //    cmb_load_type.Items.Add(Bridge_Analysis.Live_Load_List[i].TypeNo + " : " + Bridge_Analysis.Live_Load_List[i].Code);
                //}
                //if (cmb_load_type.Items.Count > 0)
                //{
                //    cmb_load_type.SelectedIndex = cmb_load_type.Items.Count - 1;
                //}
                Button_Enable_Disable();
                //Write_All_Data();

               MessageBox.Show(this, "Analysis Input data is created as \"" + Title + "\\INPUT_DATA.TXT\" inside the working folder.",
                  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex) { }
        }
        private void btn_Ana_close_Click(object sender, EventArgs e)
        {
            Bridge_Analysis.Clear();

            this.Close();
        }
        private void btn_Ana_view_report_Click(object sender, EventArgs e)
        {

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
            //iApp.RunExe(Bridge_Analysis.Analysis_Report);
        }
        private void btn_Ana_view_data_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Path.Combine(user_path, "LL.txt"));
            iApp.RunExe(Bridge_Analysis.TotalAnalysis_Input_File);
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
                Write_All_Data(true);

                string flPath = Bridge_Analysis.Input_File;
                do
                {
                    if (i == 0)
                        flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                    else if (i == 1)
                    {
                        MessageBox.Show(this, "PROCESS ANALYSIS FOR LIVE LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flPath = Bridge_Analysis.LiveLoadAnalysis_Input_File;
                    }
                    else if (i == 2)
                    {
                        MessageBox.Show(this, "PROCESS ANALYSIS FOR DEAD LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;
                    }


                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                    System.Diagnostics.Process prs = new System.Diagnostics.Process();

                    System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                    System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                    prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                    if (prs.Start())
                        prs.WaitForExit();
                    i++;
                }
                while (i < 3);

                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                if (File.Exists(ana_rep_file))
                {


                    iApp.Progress_Works.Clear();
                    iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                    iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");
                    iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                    iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                    iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


                    Bridge_Analysis.Girder_Analysis = null;
                    Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                    Ana_Show_Moment_Shear();

                    string s1 = "";
                    string s2 = "";
                    try
                    {
                        for (i = 0; i < Bridge_Analysis.Girder_Analysis.Supports.Count; i++)
                        {
                            if (i < Bridge_Analysis.Girder_Analysis.Supports.Count / 2)
                            {
                                if (i == Bridge_Analysis.Girder_Analysis.Supports.Count / 2 - 1)
                                {
                                    s1 += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo;
                                }
                                else
                                    s1 += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo + ",";
                            }
                            else
                            {
                                if (i == Bridge_Analysis.Girder_Analysis.Supports.Count - 1)
                                {
                                    s2 += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo;
                                }
                                else
                                    s2 += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo + ", ";
                            }
                        }
                    }
                    catch (Exception ex) { }
                    double BB = MyList.StringToDouble(txt_Ana_B.Text, 8.5);

                    //Chiranjit [2013 06 28]
                    txt_node_displace.Text = Bridge_Analysis.Girder_Analysis.Node_Displacements.Get_Max_Deflection().ToString();


                    frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                    frm_ViewForces_Load();

                    frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                    frm_ViewDesign_Forces_Load();


                    //frm_ViewForces f = new frm_ViewForces(iApp, BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, s);
                    //f.Owner = this;
                    //f.Text = "Data to be used in RCC Abutment Design";
                    //f.Show();


                    //frm_Pier_ViewDesign_Forces fv = new frm_Pier_ViewDesign_Forces(iApp, Bridge_Analysis.Total_Analysis_Report, s, s1);
                    //fv.Owner = this;
                    //fv.Text = "Data to be used in RCC Pier Design";
                    //fv.Show();

                    //Chiranjit [2012 06 22]
                    txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                    txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                    txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                    txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
                    txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



                }

                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;



                Button_Enable_Disable();
                Write_All_Data(false);
                //Write_All_Data();


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
                dgv_live_load.Rows.Add(cmb_load_type.Text, txt_Ana_X.Text, txt_Y.Text, txt_Z.Text, txt_XINCR.Text, txt_Load_Impact.Text);
            }
            catch (Exception ex) { }
        }
        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = false;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;
            //btn_create_data.Enabled = rbtn_ana_create_analysis_file.Checked;

            //if (rbtn_ana_select_analysis_file.Checked)
            //{

            //    string chk_file = Path.Combine(Analysis_Path, "INPUT_DATA.TXT");

            //    if (File.Exists(chk_file))
            //    {
            //        Show_ReadMemberLoad(chk_file);
            //        Ana_OpenAnalysisFile(chk_file);
            //        Read_All_Data();
            //        iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
            //        #region Read Previous Record
            //        //Chiranjit [2013 01 03]
            //        iApp.Read_Form_Record(this, Analysis_Path);
            //    }
            //    #endregion

            //}
            Button_Enable_Disable();


        }
        public void Show_ReadMemberLoad_Old(string file_name)
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
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    isMoving_load = true;
                    if (mlist.Count == 3) txt_LL_load_gen.Text = mlist.StringList[2];
                    dgv_live_load.Rows.Clear();
                }

                if (kStr.Contains("DEFINE MOV"))
                {
                    mov_flag = false;
                    //continue;
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
            txt_member_load.Lines = list_member_load.ToArray();
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
            txt_member_load.Lines = list_member_load.ToArray();
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
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text File (*.txt)|*.txt";
                    ofd.InitialDirectory = Analysis_Path;
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        IsCreateData = false;

                        string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");

                        if (!File.Exists(chk_file)) chk_file = ofd.FileName;

                        //Show_ReadMemberLoad(chk_file);
                        Ana_OpenAnalysisFile(chk_file);
                        Read_All_Data();
                        iApp.LiveLoads.Fill_Combo(ref cmb_load_type);

                        #region Read Previous Record
                        //Chiranjit [2013 01 03]
                        iApp.Read_Form_Record(this, user_path);
                        txt_analysis_file.Text = chk_file;
                        #endregion

                        rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                        Open_Create_Data();//Chiranjit [2013 06 25]

                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                Button_Enable_Disable();

                grb_create_input_data.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");


            }
        }
        private void btn_Ana_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_live_load.Rows.RemoveAt(dgv_live_load.CurrentRow.Index);
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_all_Click(object sender, EventArgs e)
        {
            dgv_live_load.Rows.Clear();
        }
        private void btn_Ana_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, true);
        }
        private void txt_Ana_length_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txt_Ana_X.Text = "-" + txt_Ana_L.Text; //Chiranjit [2013 05 29]
                Text_Changed();

                try
                {
                    for (int i = 0; i < dgv_live_load.RowCount; i++)
                    {
                        dgv_live_load[4, i].Value = txt_XINCR.Text;
                        //dgv_live_load[1, i].Value = txt_Ana_X.Text; //Chiranjit [2013 05 29]
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception ex) { }
        }
        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            grb_SIDL.Enabled = chk_ana_active_SIDL.Checked;
            grb_LL.Enabled = chk_ana_active_LL.Checked;
        }
        #endregion Bridge Deck Analysis Form Events

        #region Bridge Deck Analysis Methods

        void Ana_Initialize_Analysis_InputData()
        {
            if (Bridge_Analysis == null)
                Bridge_Analysis = new TGirderAnalysis(iApp);

            double Bs = (B - CL - CR) / (NMG - 1);


            txt_sec_in_end_Ds.Text = (MyList.StringToDouble(txt_Ana_Ds.Text, 0.0) * 1000).ToString();
            txt_sec_in_end_D.Text = (MyList.StringToDouble(txt_Ana_DMG.Text, 0.0) * 1000).ToString();

            txt_sec_in_end_Bf.Text = (Bs * 1000).ToString("f0");
            txt_sec_in_end_Bb.Text = (MyList.StringToDouble(txt_Ana_Bb.Text, 0.0) * 1000).ToString();
            txt_sec_in_end_Bw.Text = (MyList.StringToDouble(txt_Ana_BMG.Text, 0.0) * 1000).ToString();
            txt_sec_in_end_D.Text = (MyList.StringToDouble(txt_Ana_DMG.Text, 0.0) * 1000).ToString();

            //txt_sec_in_end_Bf.Text = (MyList.StringToDouble(txt_Long_SMG.Text, 0.0) * 1000).ToString();



            //Bs = (B / 2.0 - CL) - (B / 2.0 - CR) / (NMG - 1);


            txt_sec_out_end_Ds.Text = (MyList.StringToDouble(txt_Ana_Ds.Text, 0.0) * 1000).ToString();
            txt_sec_out_end_D.Text = (MyList.StringToDouble(txt_Ana_DMG.Text, 0.0) * 1000).ToString();
            txt_sec_out_end_Gs.Text = (Bs * 1000).ToString("f0");
            txt_sec_out_end_Bc.Text = ((CL > CR) ? CL * 1000 : CR * 1000).ToString("f0");
            txt_sec_out_end_Bb.Text = (MyList.StringToDouble(txt_Ana_Bb.Text, 0.0) * 1000).ToString();
            txt_sec_out_end_Bw.Text = (MyList.StringToDouble(txt_Ana_BMG.Text, 0.0) * 1000).ToString();
            txt_sec_out_end_D.Text = (MyList.StringToDouble(txt_Ana_DMG.Text, 0.0) * 1000).ToString();
            //txt_sec_out_end_Bf.Text = (MyList.StringToDouble(txt_Long_SMG.Text, 0.0) * 1000).ToString();

            txt_sec_crs_end_D.Text = (MyList.StringToDouble(txt_Ana_DCG.Text, 0.0) * 1000).ToString();
            txt_sec_crs_b.Text = (MyList.StringToDouble(txt_Ana_BCG.Text, 0.0) * 1000).ToString();


            Bridge_Analysis.T_Long_Inner_Section = long_inner_sec;
            Bridge_Analysis.T_Long_Outer_Section = long_out_sec;
            Bridge_Analysis.T_Cross_Section = cross_sec;

            Bridge_Analysis.Length = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            Bridge_Analysis.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Bridge_Analysis.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
            Bridge_Analysis.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
            Bridge_Analysis.Skew_Angle = MyList.StringToDouble(txt_Ana_ang.Text, 0.0);
            Bridge_Analysis.Number_Of_Long_Girder = MyList.StringToInt(txt_Ana_NMG.Text, 4);
            Bridge_Analysis.Number_Of_Cross_Girder = MyList.StringToInt(txt_Ana_NCG.Text, 3);

            //Bridge_Analysis.Effective_Depth = MyList.StringToDouble(txt_Ana_eff_depth.Text, 0.0);
            Bridge_Analysis.Effective_Depth = Deff;
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
                load_lst.AddRange(txt_member_load.Lines);

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Bridge_Analysis.LiveLoad_File))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + Bridge_Analysis.Working_Folder);
                    }

                }
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                if (dgv_live_load.RowCount != 0)
                    load_lst.AddRange(Ana_Get_MovingLoad_Data(Bridge_Analysis.Live_Load_List));
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Ana_Show_Moment_Shear()
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Girder_Analysis.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Bridge_Analysis.Girder_Analysis.Analysis.Joints;

            double L = Bridge_Analysis.Length;
            double W = Bridge_Analysis.WidthBridge;
            double val = L / 2;
            int i = 0;

            List<int> _L2_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();

            List<int> _L4_out_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();

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

            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Bridge_Analysis.Girder_Analysis.Analysis.Effective_Depth;
            //if (_X_joints.Contains(val))
            //{
            //    Bridge_Analysis.Effective_Depth = val;
            //}
            //else
            //{
            //    Bridge_Analysis.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            //}
            //double eff_dep = ;

            //_L_2_joints.Clear();

            double cant_wi_left = Bridge_Analysis.Width_LeftCantilever;
            double cant_wi_right = Bridge_Analysis.Width_RightCantilever;
            //Bridge_Analysis.Width_LeftCantilever = cant_wi;
            //Bridge_Analysis.Width_RightCantilever = _Z_joints[_Z_joints.Count - 1] - _Z_joints[_Z_joints.Count - 3];



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


                    if ((jn_col[i].X.ToString("0.0") == (Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }



            if (_L2_inn_joints.Count > 2)
            {
                _L2_inn_joints.RemoveAt(0);
                _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            }
            if (_L4_inn_joints.Count > 2)
            {
                _L4_inn_joints.RemoveAt(0);
                _L4_inn_joints.RemoveAt(_L4_inn_joints.Count - 1);
            }
            if (_L4_out_joints.Count > 2)
            {
                _L4_out_joints.RemoveAt(0);
                _L4_out_joints.RemoveAt(_L4_out_joints.Count - 1);
            }
            if (_deff_inn_joints.Count > 2)
            {
                _deff_inn_joints.RemoveAt(0);
                _deff_inn_joints.RemoveAt(_deff_inn_joints.Count - 1);
            }
            if (_deff_out_joints.Count > 2)
            {
                _deff_out_joints.RemoveAt(0);
                _deff_out_joints.RemoveAt(_deff_out_joints.Count - 1);
            }




            _L4_inn_joints.AddRange(_L4_out_joints);
            _deff_inn_joints.AddRange(_deff_out_joints);



            _cross_joints.AddRange(_L2_inn_joints);
            _cross_joints.AddRange(_L4_inn_joints);
            _cross_joints.AddRange(_deff_inn_joints);



            Result.Clear();
            Result.Add("");
            Result.Add("");
            Result.Add("Analysis Result of RCC T-BEAM Bridge");
            Result.Add("");
            Result.Add("");
            Result.Add("INNER GIRDER");
            Result.Add("------------");
            MaxForce mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L2_inn_joints, true);
            txt_inner_long_L2_shear.Text = mfrc.ToString();
            txt_inner_long_L2_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_inner_long_L2_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_inner_long_L2_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata

            Result.AddRange(mfrc.GetDetails("L/2 :  MAX SHEAR FORCE", _L2_inn_joints, " Ton"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L2_inn_joints, true);
            txt_Ana_inner_long_L2_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_L2_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_inner_long_L2_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_inner_long_L2_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/2 :  MAX BENDING MOMENT", _L2_inn_joints, " Ton-m"));

            //_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata


            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L4_inn_joints, true);
            txt_Ana_inner_long_L4_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_L4_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_inner_long_L4_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_inner_long_L4_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX SHEAR SHEAR", _L4_inn_joints, " Ton"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L4_inn_joints, true);
            txt_Ana_inner_long_L4_moment.Text = mfrc.ToString();
            txt_Ana_inner_long_L4_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata

            txt_Ana_inner_long_L4_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_inner_long_L4_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX BENDING MOMENT", _L4_inn_joints, " Ton-m"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_deff_inn_joints, true);
            txt_Ana_inner_long_deff_shear.Text = mfrc.ToString();
            txt_Ana_inner_long_deff_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata

            txt_Ana_inner_long_deff_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_inner_long_deff_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX SHEAR SHEAR", _deff_inn_joints, " Ton"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_deff_inn_joints, true);
            txt_inner_long_deff_moment.Text = mfrc.ToString();
            txt_inner_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata

            txt_inner_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_inner_long_deff_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX BENDING MOMENT", _deff_inn_joints, " Ton-m"));

            _L2_inn_joints.Clear();
            _L4_inn_joints.Clear();
            _deff_inn_joints.Clear();

            _L4_out_joints.Clear();
            _deff_out_joints.Clear();


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


                    if ((jn_col[i].X.ToString("0.0") == (Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }




            if (_L2_inn_joints.Count > 2)
            {
                _L2_inn_joints.RemoveRange(1, _L2_inn_joints.Count - 2);

                _L4_inn_joints.RemoveRange(1, _L4_inn_joints.Count - 2);
                _L4_out_joints.RemoveRange(1, _L4_out_joints.Count - 2);
                _L4_out_joints.AddRange(_L4_inn_joints);

                _deff_inn_joints.RemoveRange(1, _deff_inn_joints.Count - 2);
                _deff_out_joints.RemoveRange(1, _deff_out_joints.Count - 2);
                _deff_out_joints.AddRange(_deff_inn_joints);

            }

            Result.Add("");
            Result.Add("");
            Result.Add("");
            Result.Add("OUTER GIRDER");
            Result.Add("------------");


            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L2_inn_joints, true);
            txt_outer_long_L2_shear.Text = mfrc.ToString();
            txt_outer_long_L2_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata

            txt_outer_long_L2_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_L2_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/2 :  MAX SHEAR FORCE", _L2_inn_joints, " Ton"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L2_inn_joints, true);
            txt_outer_long_L2_moment.Text = mfrc.ToString();
            txt_outer_long_L2_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata

            txt_outer_long_L2_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_L2_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/2 :  MAX BENDING MOMENT", _L2_inn_joints, " Ton-m"));






            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L4_out_joints, true);
            txt_outer_long_L4_shear.Text = mfrc.ToString();
            txt_outer_long_L4_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata

            txt_outer_long_L4_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_L4_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX SHEAR FORCE", _L4_out_joints, " Ton"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L4_out_joints, true);
            txt_outer_long_L4_moment.Text = mfrc.ToString();
            txt_outer_long_L4_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata

            txt_outer_long_L4_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_L4_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX BENDING MOMENT", _L4_out_joints, " Ton-m"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_deff_out_joints, true);
            txt_outer_long_deff_shear.Text = mfrc.ToString();
            txt_outer_long_deff_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_deff_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_deff_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX SHEAR FORCE", _deff_out_joints, " Ton"));


            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_deff_out_joints, true);
            txt_outer_long_deff_moment.Text = mfrc.ToString();
            txt_outer_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_outer_long_deff_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX BENDING MOMENT", _deff_out_joints, " Ton-m"));



            Result.Add("");
            Result.Add("");
            Result.Add("");
            Result.Add("");

            _deff_inn_joints = MyList.Get_Array_Intiger("1 TO " + Bridge_Analysis.Joints.Count);
            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_deff_inn_joints, false);
            Result.AddRange(mfrc.GetDetails("CROSS GIRDER :  MAX SHEAR FORCE", _deff_inn_joints, " Ton"));
            txt_Ana_cross_max_shear.Text = mfrc.ToString();
            txt_Ana_cross_max_shear_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata
            txt_Ana_cross_max_shear_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_cross_max_shear_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            Result.Add("");

            Result.Add("");
            Result.Add("");
            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_deff_inn_joints, false);
            Result.AddRange(mfrc.GetDetails("CROSS GIRDER :  MAX BENDING MOMENT", _deff_inn_joints, " Ton-m"));
            txt_Ana_cross_max_moment.Text = mfrc.ToString();
            txt_Ana_cross_max_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata

            txt_Ana_cross_max_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            txt_Ana_cross_max_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata

            //txt_outer_long_deff_moment_joint_no.Text = mfrc.NodeNo.ToString(); //Chiranjit [2013 06 03] Kolkata

            //txt_outer_long_deff_moment_mem_no.Text = mfrc.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
            //txt_outer_long_deff_moment_load_case.Text = mfrc.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
            //Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX BENDING MOMENT", _deff_out_joints, " Ton-m"));






            //mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_cross_joints);
            //txt_Ana_cross_max_shear.Text = mfrc.ToString();
            //Result.AddRange(mfrc.GetDetails("Cross Girder :  MAX SHEAR FORCE", _cross_joints, " Ton"));


            //mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_cross_joints);
            //txt_Ana_cross_max_moment.Text = mfrc.ToString();

            //Result.AddRange(mfrc.GetDetails("Cross Girder :  MAX BENDING MOMENT", _cross_joints, " Ton-m"));


            //Show_Cross_Girder_Forces();
            //Show_Long_Girder_Forces();


            #region Null All variables
            mc = null;


            jn_col = null;


            _L2_inn_joints = null;
            _L4_inn_joints = null;
            _deff_inn_joints = null;

            _L4_out_joints = null;
            _deff_out_joints = null;
            #endregion
            //Ana_Show_Cross_Girder();

            File.WriteAllLines(Path.Combine(user_path, "ANALYSIS_RESULT.TXT"), Result.ToArray());

            //iApp.RunExe(Path.Combine(user_path, "ANALYSIS_RESULT.TXT"));
        }
        void Ana_Show_Cross_Girder()
        {
            #region Cross Girder Forces
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Girder_Analysis.Analysis.Members);

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

            #endregion CC


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
                m.Force = Bridge_Analysis.Girder_Analysis.GetForce(ref m);




                List<int> joints = new List<int>();

                Result.Add("");
                Result.Add("");
                Result.Add("");
                Result.Add("");
                Result.AddRange(m.MaxShearForce.GetDetails("CROSS GIRDER :  MAX SHEAR FORCE", joints, " Ton"));
                txt_Ana_cross_max_shear.Text = m.MaxShearForce.ToString();
                txt_Ana_cross_max_shear_joint_no.Text = m.MaxShearForce.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata

                txt_Ana_cross_max_shear_mem_no.Text = m.MaxShearForce.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
                txt_Ana_cross_max_shear_load_case.Text = m.MaxShearForce.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata
                Result.Add("");
                Result.Add("");
                Result.Add("");
                Result.AddRange(m.MaxBendingMoment.GetDetails("CROSS GIRDER :  MAX BENDING MOMENT", joints, " Ton-m"));
                txt_Ana_cross_max_moment.Text = m.MaxBendingMoment.ToString();
                txt_Ana_cross_max_moment_joint_no.Text = m.MaxShearForce.NodeNo.ToString(); //Chiranjit [2013 06 04] Kolkata

                txt_Ana_cross_max_moment_mem_no.Text = m.MaxShearForce.MemberNo.ToString(); //Chiranjit [2013 06 03] Kolkata
                txt_Ana_cross_max_moment_load_case.Text = m.MaxShearForce.Loadcase.ToString(); //Chiranjit [2013 06 03] Kolkata




            }
            Ana_Write_Max_Moment_Shear();
       
        }
        //Chiranjit [2011 10 29] 

        void Ana_Write_Max_Moment_Shear()
        {
            List<string> list = new List<string>();
            //list.Add(string.Format("LONG_LENGTH={0}", span_length));
            list.Add(string.Format("LONG_INN_DEFF_MOM={0}", txt_inner_long_deff_moment.Text));
            list.Add(string.Format("LONG_INN_DEFF_SHR={0}", txt_Ana_inner_long_deff_shear.Text));
            list.Add(string.Format("LONG_INN_L2_MOM={0}", txt_Ana_inner_long_L2_moment.Text));
            list.Add(string.Format("LONG_INN_L2_SHR={0}", txt_inner_long_L2_shear.Text));
            list.Add(string.Format("LONG_INN_L4_MOM={0}", txt_Ana_inner_long_L4_moment.Text));
            list.Add(string.Format("LONG_INN_L4_SHR={0}", txt_Ana_inner_long_L4_shear.Text));

            list.Add(string.Format("LONG_OUT_DEFF_MOM={0}", txt_outer_long_deff_moment.Text));
            list.Add(string.Format("LONG_OUT_DEFF_SHR={0}", txt_outer_long_deff_shear.Text));
            list.Add(string.Format("LONG_OUT_L2_MOM={0}", txt_outer_long_L2_moment.Text));
            list.Add(string.Format("LONG_OUT_L2_SHR={0}", txt_outer_long_L2_shear.Text));
            list.Add(string.Format("LONG_OUT_L4_MOM={0}", txt_outer_long_L4_moment.Text));
            list.Add(string.Format("LONG_OUT_L4_SHR={0}", txt_outer_long_L4_shear.Text));

            list.Add(string.Format("CROSS_MOM={0}", txt_Ana_cross_max_moment.Text));
            list.Add(string.Format("CROSS_SHR={0}", txt_Ana_cross_max_shear.Text));

            string f_path = Path.Combine(Bridge_Analysis.Working_Folder, "FORCES.TXT");
            File.WriteAllLines(f_path, list.ToArray());
            Environment.SetEnvironmentVariable("TBEAM_ANALYSIS", f_path);
            list = null;
        }

        public void Button_Enable_Disable()
        {
            btn_view_data.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_view_structure.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_View_Moving_Load.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_view_report.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_process_analysis.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);


            //btn_dwg_long.Enabled = File.Exists(LongGirders.drawing_path);
            //btn_dwg_cross.Enabled = File.Exists(XGirders.user_drawing_file);
            //btn_dwg_deck.Enabled = File.Exists(Deck.drawing_path);
            //btn_dwg_cant.Enabled = File.Exists(Cant.user_drawing_file);


            //Write_All_Data();
            //Chiranjit [2013 07 18]
            if (Limit_LongGirder != null)
                btn_LS_long_report.Enabled = rbtn_LS_long_inner_girder.Checked ? File.Exists(Limit_LongGirder.Inner_File) : File.Exists(Limit_LongGirder.Outer_File);
        }
        public void Ana_OpenAnalysisFile(string file_name)
        {
            string analysis_file = Path.GetDirectoryName(file_name);


            if (Path.GetFileName(analysis_file).ToLower() == "dead load analysis")
            {
                //analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "DeadLoad_Analysis_Input_File.TXT");
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
                string rep_file = Path.Combine(Bridge_Analysis.Working_Folder, "ANALYSIS_REP.TXT");

                Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, analysis_file);

                if (Bridge_Analysis != null)
                {
                    if (Bridge_Analysis.Girder_Analysis.Analysis.Joints.Count > 1)
                    {
                        Bridge_Analysis.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Bridge_Analysis.Girder_Analysis.Analysis.Joints[1].X / Bridge_Analysis.Girder_Analysis.Analysis.Joints[1].Z)));
                        txt_Ana_ang.Text = Bridge_Analysis.Skew_Angle.ToString();
                    }
                }
                txt_Ana_L.Text = Bridge_Analysis.Girder_Analysis.Analysis.Length.ToString();
                txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                txt_Ana_B.Text = Bridge_Analysis.Girder_Analysis.Analysis.Width.ToString();
                //txt_gd_np.Text = (Bridge_Analysis.Girder_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                txt_analysis_file.Visible = true;
                txt_analysis_file.Text = analysis_file;

                txt_Ana_Ds.Text = Bridge_Analysis.Girder_Analysis.Analysis.Width_Cantilever.ToString();


            }

        _run:

            Bridge_Analysis.Input_File = analysis_file;
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;

            Button_Enable_Disable();

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Bridge_Analysis.LiveLoad_File);

            if (Bridge_Analysis.Live_Load_List == null) return;
        }

        public string[] Ana_Get_MovingLoad_Data(List<LoadData> lst_load_data)
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


            if (chk_ana_active_LL.Checked)
            {
                //Chiranjit [2012 11 07]
                //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");


                //Chiranjit [2011 10 08]
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");


                Bridge_Analysis.LoadReadFromGrid(dgv_live_load);
                foreach (LoadData ld in Bridge_Analysis.LoadList)
                {
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
        List<string> Ana_Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Girder_Analysis.Analysis.Members);

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


            z_min = Bridge_Analysis.Girder_Analysis.Analysis.Joints.MinZ;
            double z_max = Bridge_Analysis.Girder_Analysis.Analysis.Joints.MaxZ;


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
        public void Ana_Fill_Default_Moving_LoadData(DataGridView dgv_live_load)
        {

            //Button_Enable_Disable();
            //dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -13.0, 0, 2.75, 0.2);
            //dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -13.0, 0, 6.25, 0.2);

            iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
            dgv_live_load.Rows.Add(cmb_load_type.Text, iApp.LiveLoads[0].Distance, txt_Y.Text, "1.5", txt_XINCR.Text, txt_Load_Impact.Text);
            dgv_live_load.Rows.Add(cmb_load_type.Text, iApp.LiveLoads[0].Distance, txt_Y.Text, "4.5", txt_XINCR.Text, txt_Load_Impact.Text);

            //dgv_live_load.Rows.Add(cmb_load_type.Text, -L, txt_Y.Text, "1.5", txt_XINCR.Text, txt_Load_Impact.Text); //Chiranjit [2013 05 29]
            //dgv_live_load.Rows.Add(cmb_load_type.Text, -L, txt_Y.Text, "4.5", txt_XINCR.Text, txt_Load_Impact.Text); //Chiranjit [2013 05 29]

            txt_LL_load_gen.Text = (L / 0.2).ToString("0");
        }

        #endregion Bridge Deck Analysis Methods

        #region Long Girder Form Events

        private void btn_Long_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data(true);
            iApp.Save_Form_Record(this, user_path);


            LongGirders.As = (Math.PI * (LongGirders.bar_dia) * (LongGirders.bar_dia) / 4.0) * LongGirders.total_bars_L2;
            var v = (LongGirders.As * 100) / (LongGirders.D * LongGirders.bw);


            string str = "Required Steel Area minimum 2%\n\r\n\r";
            for (int ccc = 0; ccc < 3; ccc++)
            {


                if (ccc == 0)
                {
                    LongGirders.As = (Math.PI * (LongGirders.bar_dia_Deff) * (LongGirders.bar_dia_Deff) / 4.0) * LongGirders.total_bars_Deff;
                    str = "Required Steel Area minimum 2%\n\r\n\r";
                    str += string.Format("At Effective Depth        Bar Dia = {0} mm  ,  Total Bar = {1} \n\r\n\r",
                                LongGirders.bar_dia_Deff, LongGirders.total_bars_Deff);

                }
                else if (ccc == 1)
                {
                    LongGirders.As = (Math.PI * (LongGirders.bar_dia_L2) * (LongGirders.bar_dia_L2) / 4.0) * LongGirders.total_bars_L2;
                    str = "Required Steel Area minimum 2%\n\r\n\r";
                    str += string.Format("At L/2         Bar Dia = {0} mm  ,  Total Bar = {1} \n\r\n\r",
                                LongGirders.bar_dia_L2, LongGirders.total_bars_L2);
                }
                else if (ccc == 2)
                {
                    LongGirders.As = (Math.PI * (LongGirders.bar_dia_L4) * (LongGirders.bar_dia_L4) / 4.0) * LongGirders.total_bars_L4;
                    str = "Required Steel Area minimum 2%\n\r\n\r";
                    str += string.Format("At L/4         Bar Dia = {0} mm  ,  Total Bar = {1} \n\r\n\r",
                                LongGirders.bar_dia_L4, LongGirders.total_bars_L4);
                }

                v = (LongGirders.As * 100) / (LongGirders.D * LongGirders.bw);
                if (v < 2.0)
                {


                    //str = string.Format("TOTAL BARS [no] = 8   Too Low\n\r\n\r", Math.PI, LongGirders.total_bars_L2);
                    //str = string.Format("TOTAL Layers of Bars [nl] = 2 Insufficient \n\r\n\r", Math.PI, LongGirders.As);
                    ////str = string.Format("Area [As] = 3927 sq.mm Less than Minimum\n\r\n\r", Math.PI, LongGirders.As);
                    //str += "Required Steel Area minimum 2%\n\r\n\r";


                    str += string.Format("Steel Area = ({0:f4}*Dia^2/4) * Total Bar = {1:f3} sq.mm\n\r\n\r", Math.PI, LongGirders.As);

                    str += string.Format("Steel Area * 100 / (Web Depth * Web Thickness)\n\r\n\r");
                    str += string.Format("{0:f3} * 100 / ({1} * {2}) = {3:f3}%   <    2%",
                        LongGirders.As, LongGirders.D, LongGirders.bw, v);

                    MessageBox.Show(this, "Too Low....\n\r\n\r" + str, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }

            }




            if (LongGirders.design_moment_mid == 0.0 &&
                LongGirders.design_moment_quarter == 0.0 &&
                LongGirders.design_moment_deff == 0.0 &&
                LongGirders.v1 == 0.0 &&
                LongGirders.v2 == 0.0 &&
                LongGirders.v3 == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : Moment (L/2) = 389.0 Ton-m\n";
                msg += "            : Shear  (L/2) = 54.34 Ton\n";
                msg += "            : Moment (L/4) = 341.2 Ton-m\n";
                msg += "            : Shear  (L/4) = 101.4 Ton\n";
                msg += "            : Moment (L/4) = 175.6 Ton-m\n";
                msg += "            : Shear  (L/4) = 109.8 Ton\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {
                LongGirders.Write_User_Input();
                LongGirders.Calculate_Program(LongGirders.rep_file_name);
                LongGirders.Write_Drawing_File();
                LongGirders.is_process = true;
                LongGirders.FilePath = user_path;
                if (File.Exists(LongGirders.rep_file_name))
                {
                    MessageBox.Show(this, "Report file written in " + LongGirders.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iApp.View_Result(LongGirders.rep_file_name);
                }
            }
            Button_Enable_Disable();
        }

        private void btn_Long_Report_Click(object sender, EventArgs e)
        {
            //LongGirders.rep_file_name = rbtn_long_inner_girder.Checked ? LongGirders.Inner_File : LongGirders.Outer_File;


            if (!File.Exists(LongGirders.rep_file_name))
            {
                MessageBox.Show(this, "Analysis is not Processed.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            iApp.RunExe(LongGirders.rep_file_name);
        }
        private void btn_Long_Drawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(LongGirders.drawing_path, "TBEAM_Long_Girder", @"TBEAM_Worksheet_Design1");
            //iApp.SetDrawingFile_Path(drawing_path, "REINFORCEMENT_DETAILS_OF_LONG_GIRDERS", @"Bridge\T Beam");
        }
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion Long Girder

        #region Long Girders Methods

       
        #endregion Long Girders Methods


        #region Cross Form Event

        private void btn_Cross_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();


            if (XGirders.M_total_hogging_moment == 0.0 && XGirders.W_total_shear == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : Total Hogging Moment  = 58.4 Ton-m\n";
                msg += "            : Total Shear = 16.8 Ton\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {

                XGirders.Write_Cross_User_InputData();
                XGirders.CalculateProgram();
                XGirders.Write_Drawing_File();
                iApp.Save_Form_Record(this, user_path);

                MessageBox.Show(this, "Report file wriiten in " + XGirders.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iApp.View_Result(XGirders.rep_file_name);
                XGirders.is_process = true;
                XGirders.FilePath = user_path;
                Button_Enable_Disable();

            }
        }
        private void btn_Cross_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(XGirders.rep_file_name);
        }

        #endregion Cross Form Event

        #region Cross Girders Methods

        #endregion Cross Girders Methods



        #region Deck Slab Form Event

        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Deck.rep_file_name);
        }
        #endregion Deck Slab Form Event

        #region Cantilever Slab Form Event
        private void btn_Cant_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Cant.rep_file_name);
        }
        private void btn_Cant_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Cant.FilePath = user_path;
            Cant.Write_User_Input();
            Cant.Calculate_Program();
            Cant.Write_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(Cant.rep_file_name)) { MessageBox.Show(this, "Report file written in " + Cant.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(Cant.rep_file_name); }
            Cant.is_process = true;
            Button_Enable_Disable();
        }
        private void txt_concrete_grade_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion Cantilever Slab Form Event

        #region Design of RCC Pier

        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(rcc_pier.rep_file_name);
        }

        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            //iapp.SetDrawingFile(user_input_file, "PIER");

            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
            //iapp.RunViewer(drwg_path);
        }
        #endregion Design of RCC Pier


        #region Chiranjit [2012 06 20]
        
        void Text_Changed()
        {
            double SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);

            double Bb = MyList.StringToDouble(txt_Ana_Bb.Text, 0.65);
            double Db = MyList.StringToDouble(txt_Ana_Db.Text, 0.65);

            //Chiranjit [2012 12 26]
            DMG = L / 10.0;
            DCG = DMG - 0.4;

          

            txt_LS_long_Bb.Text = (Bb * 1000.0).ToString(); // Chiranjit [2013 07 18]
            txt_LS_long_Db.Text = (Db * 1000.0).ToString(); // Chiranjit [2013 07 18]
            txt_LS_long_Bs.Text = (SMG * 1000.0).ToString(); // Chiranjit [2013 07 18]


            Ana_Initialize_Analysis_InputData();
            Calculate_Load_Computation();
        }
        private void txt_Ana_width_TextChanged(object sender, EventArgs e)
        {
            //Calculate_Interactive_Values();
            Text_Changed();


            if (((TextBox)sender).Name == txt_Ana_B.Name)
                CW = B - 2.0;
        }

        //Chiranjit [2012 06 20]
        private void chk_CheckedChanged(object sender, EventArgs e)
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
            else if (chk.Name == chk_parapet.Name)
            {
                grb_ana_parapet.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_Hp.Text = "0.000";
                    txt_Ana_Wp.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hp.Text = "1.200";
                    txt_Ana_Wp.Text = "0.500";
                }
            }
            else if (chk.Name == chk_sw_fp.Name)
            {
                grb_ana_sw_fp.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_Bs.Text = "0.000";
                    txt_Ana_Hs.Text = "0.000";
                    txt_Ana_Wps.Text = "0.000";
                    txt_Ana_Hps.Text = "0.000";
                }
                else
                {
                    txt_Ana_Bs.Text = "1.000";
                    txt_Ana_Hs.Text = "0.250";
                    txt_Ana_Wps.Text = "0.500";
                    txt_Ana_Hps.Text = "1.000";
                }
            }
            else if (chk.Name == chk_swf.Name)
            {
                txt_Ana_swf.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_swf.Text = "1.000";
                }
                else
                {
                    txt_Ana_swf.Text = "1.400";
                }
            }
            else if (chk.Name == chk_ana_active_LL.Name)
            {
                grb_LL.Enabled = chk.Checked;
                if (chk.Checked)
                {
                    Ana_Fill_Default_Moving_LoadData(dgv_live_load);
                }
                else
                    dgv_live_load.Rows.Clear();
            }
        }
        public void Calculate_Load_Computation()
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();


            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for RCC T - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
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
            wi2 = SCG * BMG * DMG * Y_c;
            list.Add(string.Format("wi2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c, wi2));
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
            //member_load.Add(string.Format("MEMBER LOAD "));
            member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));

            //list.Add(string.Format("                MEMBER LOAD "));
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
            wo2 = SCG * BMG * DMG * Y_c;
            list.Add(string.Format("wo2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hp * Wp * Y_c;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hp, Wp, Y_c, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Bs * Hs * Y_c;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Bs, Hs, Y_c, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * Hps * Wps * Y_c;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Hps, Wps, Y_c, wo5));
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
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c, wc1));
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


            //member_load.Add(string.Format("LOAD 2"));
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

            txt_member_load.Lines = member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }
        //Chiranjit [2013 05 03]
        public void Calculate_Load_Computation(string outer_girders, string inner_girders, List<string> joints_nos)
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();


            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for RCC T - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
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
            wi2 = SCG * BMG * DMG * Y_c;
            list.Add(string.Format("wi2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c, wi2));
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
            //member_load.Add(string.Format("MEMBER LOAD "));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders , wiu));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", Bridge_Analysis.Cross_Girders_as_String, txt_ana_cross_girder_mem_load.Text)); //Chiranjit [2013 06 03]
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, txt_ana_long_girder_mem_load.Text)); //Chiranjit [2013 06 03]

            //list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wiu));
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
            wo2 = SCG * BMG * DMG * Y_c;
            list.Add(string.Format("wo2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hp * Wp * Y_c;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hp, Wp, Y_c, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Bs * Hs * Y_c;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Bs, Hs, Y_c, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * Hps * Wps * Y_c;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Hps, Wps, Y_c, wo5));
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
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wou)); //Chiranjit [2013 06 03]
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, txt_ana_long_girder_mem_load.Text)); //Chiranjit [2013 06 03]



            //member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wou));
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
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c, wc1));
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
            member_load.Add(string.Format("JOINT LOAD"));
            foreach (var item in joints_nos)
            {
                list.Add(string.Format("                {0} FY -{1:f4}", item, wjl));
                member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl));

            }
            //list.Add(string.Format("                24 TO 32 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                35 TO 43 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                46 TO 54 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                57 TO 65 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                68 TO 76 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                79 TO 87 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                90 TO 98 FZ -{0:f4}", wjl));
            //list.Add(string.Format("                101 TO 109 FZ -{0:f4}", wjl));


            //member_load.Add(string.Format("LOAD 2"));
            //member_load.Add(string.Format("JOINT LOAD"));
            //member_load.Add(string.Format("{0} FZ -{1:f4}", joints_nos, wjl));
            //member_load.Add(string.Format("24 TO 32 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("35 TO 43 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("46 TO 54 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("57 TO 65 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("68 TO 76 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("79 TO 87 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("90 TO 98 FZ -{0:f4}", wjl));
            //member_load.Add(string.Format("101 TO 109 FZ -{0:f4}", wjl));

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));



            //member_load.Add(string.Format(""));
            txt_member_load.Lines = member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

       
        #endregion Chiranjit [2012 06 10]

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





            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_max_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_max_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }
        #endregion frm_Pier_ViewDesign_Forces

        #endregion

        #region Chiranjit [2012 07 10]
        //Write All Data in a File
        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            if (showMessage) DemoCheck();
        }
        public void Read_All_Data()
        {
            if (iApp.IsDemo) return;

            string data_file = Bridge_Analysis.User_Input_Data;

            if (!File.Exists(data_file)) return;
            try
            {

                LongGirders.FilePath = user_path;
                XGirders.FilePath = user_path;
                Deck.FilePath = user_path;
                Cant.FilePath = user_path;
                Abut.FilePath = user_path;
                rcc_pier.FilePath = user_path;
                Limit_LongGirder.FilePath = user_path;
            }
            catch (Exception ex) { }
            Button_Enable_Disable();
        }
        #endregion Chiranjit [2012 07 10]

        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L.Text = "0.0";
                txt_Ana_L.Text = "19.2";
                txt_Ana_B.Text = "12.1";
                txt_Ana_CW.Text = "11.0";

                //string str = "ASTRA Pro USB Dongle not found at any port.\n\nThis is Unauthorized Version of ASTRA Pro.\n This will process the default Data only as sample input data.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\n";
                //str += "Email at : techsoft@consultant.com, dataflow@mail.com\n\n";
                //str += "Website  : http://www.headsview.com\n\n";
                //str += "Tel. No  : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                //str += "\n\nTechSOFT Engineering Services\n\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]

        private void txt_section_properties_TextChanged(object sender, EventArgs e)
        {
            if (long_inner_sec == null)
                long_inner_sec = new TLong_SectionProperties();

            long_inner_sec.Ds = MyList.StringToDouble(txt_sec_in_end_Ds.Text, 0.0);
            long_inner_sec.D = MyList.StringToDouble(txt_sec_in_end_D.Text, 0.0);
            long_inner_sec.Bw = MyList.StringToDouble(txt_sec_in_end_Bw.Text, 0.0);
            long_inner_sec.Gs = MyList.StringToDouble(txt_sec_in_end_Bf.Text, 0.0);
            long_inner_sec.Bb = MyList.StringToDouble(txt_sec_in_end_Bb.Text, 0.0);
            long_inner_sec.Db = MyList.StringToDouble(txt_sec_in_end_Bb.Text, 0.0);

            txt_sec_in_end_Ax.Text = long_inner_sec.Area_in_Sq_m.ToString("f4");
            txt_sec_in_end_Ixx.Text = long_inner_sec.Ixx_in_Sq_Sq_m.ToString("f4");
            txt_sec_in_end_Iyy.Text = long_inner_sec.Iyy_in_Sq_Sq_m.ToString("f4");
            txt_sec_in_end_Izz.Text = long_inner_sec.Izz_in_Sq_Sq_m.ToString("f4");
            txt_sec_in_end_Dw.Text = long_inner_sec.Dw.ToString();


            if (long_out_sec == null)
                long_out_sec = new TLong_SectionProperties();

            #region Chiranjit [2013 06 28]

            long_inner_sec.eff_depth = Deff * 1000;

            long_out_sec.modular_ratio = long_inner_sec.modular_ratio;
            long_out_sec.Ast = long_inner_sec.Ast;
            long_out_sec.eff_depth = long_inner_sec.eff_depth;

            #endregion Chiranjit [2013 06 28]


            long_out_sec.Ds = MyList.StringToDouble(txt_sec_out_end_Ds.Text, 0.0);
            long_out_sec.D = MyList.StringToDouble(txt_sec_out_end_D.Text, 0.0);
            long_out_sec.Bw = MyList.StringToDouble(txt_sec_out_end_Bw.Text, 0.0);
            long_out_sec.Gs = MyList.StringToDouble(txt_sec_out_end_Gs.Text, 0.0);
            long_out_sec.Bc = MyList.StringToDouble(txt_sec_out_end_Bc.Text, 0.0);
            long_out_sec.Gs = MyList.StringToDouble(txt_sec_out_end_Gs.Text, 0.0);
            long_out_sec.Bc = MyList.StringToDouble(txt_sec_out_end_Bc.Text, 0.0);
            long_out_sec.Bb = MyList.StringToDouble(txt_sec_out_end_Bb.Text, 0.0);
            long_out_sec.Db = MyList.StringToDouble(txt_sec_out_end_Bb.Text, 0.0);

            txt_sec_out_end_Dw.Text = long_out_sec.Dw.ToString();
            txt_sec_out_end_Bf.Text = long_out_sec.Bf.ToString();
            txt_sec_out_end_Ax.Text = long_out_sec.Area_in_Sq_m.ToString("f4");
            txt_sec_out_end_Ixx.Text = long_out_sec.Ixx_in_Sq_Sq_m.ToString("f4");
            txt_sec_out_end_Iyy.Text = long_out_sec.Iyy_in_Sq_Sq_m.ToString("f4");
            txt_sec_out_end_Izz.Text = long_out_sec.Izz_in_Sq_Sq_m.ToString("f4");

            if (cross_sec == null)
                cross_sec = new TCross_SectionProperties();

            cross_sec.D = MyList.StringToDouble(txt_sec_out_end_D.Text, 0.0);
            cross_sec.b = MyList.StringToDouble(txt_sec_out_end_Bw.Text, 0.0);


            txt_sec_crs_end_Ax.Text = cross_sec.Area_in_Sq_m.ToString("f4");
            txt_sec_crs_end_Ixx.Text = cross_sec.Ixx_in_Sq_Sq_m.ToString("f4");
            txt_sec_crs_end_Iyy.Text = cross_sec.Iyy_in_Sq_Sq_m.ToString("f4");
            txt_sec_crs_end_Izz.Text = cross_sec.Izz_in_Sq_Sq_m.ToString("f4");


            List<string> lis = new List<string>();

            lis.Add("");
            lis.Add("--------------------------------------------");
            lis.Add("Section Properties of Inner Long Main Girder");
            lis.Add("--------------------------------------------");
            lis.Add("");
            lis.AddRange(long_inner_sec.Results());
            lis.Add("");
            lis.Add("--------------------------------------------");
            lis.Add("Section Properties of Outer Long Main Girder");
            lis.Add("--------------------------------------------");
            lis.Add("");
            lis.AddRange(long_out_sec.Results());
            lis.Add("");
            lis.Add("----------------------------------");
            lis.Add("Section Properties of Cross Girder");
            lis.Add("----------------------------------");
            lis.Add("");
            lis.AddRange(cross_sec.Results());
            lis.Add("");
            rtb_calc_section.Lines = lis.ToArray();
            lis.Add("");
            lis.Add("");
        }

        private void cmb_LS_long_Cgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Limit_State_LongGirder_Values();
            }
            catch (Exception ex) { }
        }

        public void Limit_State_LongGirder_Values()
        {
            double CGrade = MyList.StringToDouble(cmb_LS_long_Cgrade.Text, 0.0);
            double SGrade = MyList.StringToDouble(cmb_LS_long_Sgrade.Text, 0.0);
            double Alpha = MyList.StringToDouble(txt_LS_long_alpha.Text, 0.0);
            double Gama_c = MyList.StringToDouble(txt_LS_long_gama_c.Text, 0.0);
            double Es = MyList.StringToDouble(txt_LS_long_Es.Text, 0.0);

            double lamda = 0.8;
            if (CGrade < 60)
                lamda = 0.8;
            else
                lamda = (0.8 - (CGrade - 60) / 500.0);

            txt_LS_long_lamda.Text = lamda.ToString("f3");

            double Eta = 1.0;
            if (CGrade < 60)
                Eta = 1.0;
            else
                Eta = (1.0 - (CGrade - 60) / 250.0);

            txt_LS_long_eta.Text = Eta.ToString("f3");


            double fcd = Alpha * CGrade / Gama_c;

            txt_LS_long_fcd.Text = fcd.ToString("f3");

            txt_LS_long_fck.Text = CGrade.ToString("f3");
            txt_LS_long_fyk.Text = SGrade.ToString("f3");


            double Ecm = 0.0;

            if (CGrade < 25.0)
                Ecm = 5000 * Math.Sqrt(CGrade);

            if (CGrade <= 25.0)
                Ecm = 30 * 1000;
            else if (CGrade <= 30.0)
                Ecm = 31 * 1000;
            else if (CGrade <= 35.0)
                Ecm = 32 * 1000;
            else if (CGrade <= 40.0)
                Ecm = 33 * 1000;
            else if (CGrade <= 45.0)
                Ecm = 34 * 1000;
            else if (CGrade <= 50.0)
                Ecm = 35 * 1000;
            else if (CGrade > 50.0)
                Ecm = 35 * 1000;
            else
                Ecm = 5000 * Math.Sqrt(CGrade);

            txt_LS_long_Ecm.Text = Ecm.ToString("f2");


            double m = Es / Ecm;



            txt_LS_long_m.Text = m.ToString("f3");



        }

        LimitState_LongGirder Limit_LongGirder { get; set; }

        public void LimitState_LongGirder_Input()
        {
            Limit_LongGirder = new LimitState_LongGirder(iApp);
            Limit_LongGirder.Cgrade = MyList.StringToDouble(cmb_LS_long_Cgrade.Text, 25.0);
            Limit_LongGirder.Sgrade = MyList.StringToDouble(cmb_LS_long_Sgrade.Text, 415.0);
            Limit_LongGirder.Lamda = MyList.StringToDouble(txt_LS_long_lamda.Text, 0.0);
            Limit_LongGirder.Eta = MyList.StringToDouble(txt_LS_long_eta.Text, 0.0);
            Limit_LongGirder.Gama_c = MyList.StringToDouble(txt_LS_long_gama_c.Text, 0.0);
            Limit_LongGirder.Alpha = MyList.StringToDouble(txt_LS_long_alpha.Text, 0.0);
            Limit_LongGirder.fcd = MyList.StringToDouble(txt_LS_long_fcd.Text, 0.0);
            Limit_LongGirder.cov = MyList.StringToDouble(txt_LS_long_cov.Text, 0.0);
            Limit_LongGirder.fck = MyList.StringToDouble(txt_LS_long_fck.Text, 0.0);
            Limit_LongGirder.Lc = MyList.StringToDouble(txt_LS_long_lsc.Text, 0.0);
            Limit_LongGirder.fyk = MyList.StringToDouble(txt_LS_long_fyk.Text, 0.0);
            Limit_LongGirder.gama_s = MyList.StringToDouble(txt_LS_long_gama_s.Text, 0.0);
            Limit_LongGirder.gama_a = MyList.StringToDouble(txt_LS_long_gama_a.Text, 0.0);

            Limit_LongGirder.Ecm = MyList.StringToDouble(txt_LS_long_Ecm.Text, 0.0);
            Limit_LongGirder.Es = MyList.StringToDouble(txt_LS_long_Es.Text, 0.0);

            Limit_LongGirder.dia = MyList.StringToDouble(txt_LS_long_dia.Text, 0.0);
            Limit_LongGirder.alpha_cw = MyList.StringToDouble(txt_LS_long_alpha_cw.Text, 0.0);
            //Limit_LongGirder.acw = MyList.StringToDouble(txt_LS_long_alpha_cw.Text, 0.0);
            Limit_LongGirder.V1 = MyList.StringToDouble(txt_LS_long_V1.Text, 0.0);
            Limit_LongGirder.b = MyList.StringToDouble(txt_LS_long_b.Text, 0.0);
            Limit_LongGirder.m = MyList.StringToDouble(txt_LS_long_m.Text, 0.0);
            Limit_LongGirder.mu = MyList.StringToDouble(txt_LS_long_mu.Text, 0.0);

            Limit_LongGirder.a = MyList.StringToDouble(txt_LS_long_a.Text, 0.0);

            Limit_LongGirder.ce = MyList.StringToDouble(txt_LS_long_ce.Text, 0.0);
            Limit_LongGirder.alpha_t = MyList.StringToDouble(txt_LS_long_alpha_t.Text, 0.0);

            //Limit_LongGirder.BM = MyList.StringToDouble(txt_LS_long_BM.Text, 0.0);

            Limit_LongGirder.Bs = MyList.StringToDouble(txt_LS_long_Bs.Text, 0.0);
            Limit_LongGirder.Ds = MyList.StringToDouble(txt_LS_long_Ds.Text, 0.0);
            //Limit_LongGirder.bf = MyList.StringToDouble(txt_LS_long_bf.Text, 0.0);
            Limit_LongGirder.Bb = MyList.StringToDouble(txt_LS_long_Bb.Text, 0.0);
            Limit_LongGirder.bw = MyList.StringToDouble(txt_LS_long_bw.Text, 0.0);
            //Limit_LongGirder.D1 = MyList.StringToDouble(txt_LS_long_D1.Text, 0.0);
            //Limit_LongGirder.D2 = MyList.StringToDouble(txt_LS_long_D2.Text, 0.0);
            //Limit_LongGirder.D3 = MyList.StringToDouble(txt_LS_long_D3.Text, 0.0);
            Limit_LongGirder.Db = MyList.StringToDouble(txt_LS_long_Db.Text, 0.0);
            Limit_LongGirder.D = MyList.StringToDouble(txt_LS_long_D.Text, 0.0);
            //Limit_LongGirder.d = MyList.StringToDouble(txt_LS_long_deff.Text, 0.0);
            //Limit_LongGirder.Xu = MyList.StringToDouble(txt_LS_long_Xu.Text, 0.0);


            Limit_LongGirder.BM = MyList.StringToDouble(txt_LS_long_BM.Text, 0.0);
            Limit_LongGirder.SF = MyList.StringToDouble(txt_LS_long_SF.Text, 0.0);


            Limit_LongGirder.K1 = MyList.StringToDouble(txt_LS_long_K1.Text, 0.0);
            Limit_LongGirder.K2 = MyList.StringToDouble(txt_LS_long_K2.Text, 0.0);


            Limit_LongGirder.SteelLayers = new List<SteelLayerData>();

            SteelLayerData sld = null;
            for (int i = 0; i < dgv_LS_long_layers.RowCount - 1; i++)
            {
                try
                {
                    sld = new SteelLayerData();
                    sld.Layer = dgv_LS_long_layers[0, i].Value.ToString();
                    sld.Bar_Dia = short.Parse(dgv_LS_long_layers[1, i].Value.ToString());
                    sld.Bars_No = short.Parse(dgv_LS_long_layers[2, i].Value.ToString());
                    sld.CG_from_bot = short.Parse(dgv_LS_long_layers[3, i].Value.ToString());
                    Limit_LongGirder.SteelLayers.Add(sld);

                }
                catch (Exception ex) { }
            }
        }

        private void btn_LS_long_process_Click(object sender, EventArgs e)
        {
            LimitState_LongGirder_Input();

            Limit_LongGirder.FilePath = user_path;
            Limit_LongGirder.rep_file_name = rbtn_LS_long_inner_girder.Checked ? Limit_LongGirder.Inner_File : Limit_LongGirder.Outer_File;
            Limit_LongGirder.Calculate_Problem();

            //iApp.View_Result(Limit_LongGirder.Inner_File);



            //LongGirders.Write_User_Input();
            //LongGirders.Calculate_Program(LongGirders.rep_file_name);
            //LongGirders.Write_Drawing_File();
            //LongGirders.is_process = true;
            //LongGirders.FilePath = user_path;

            //Limit_LongGirder.rep_file_name = rbtn_LS_long_inner_girder.Checked ? Limit_LongGirder.Inner_File : Limit_LongGirder.Outer_File;

            if (File.Exists(Limit_LongGirder.rep_file_name))
            {
                MessageBox.Show(this, "Report file written in " + Limit_LongGirder.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iApp.View_Result(Limit_LongGirder.rep_file_name);
            }

            Button_Enable_Disable();


        }

        private void txt_LS_long_cov_TextChanged(object sender, EventArgs e)
        {
            Steel_Layer_CG_Calculate();
        }

        private void Steel_Layer_CG_Calculate()
        {

            double cov = MyList.StringToDouble(txt_LS_long_cov.Text, 0.0);
            double dia = MyList.StringToDouble(txt_LS_long_dia.Text, 0.0);

            double val = 0.0;
            double prev_val = 0.0;


            try
            {
                for (int i = 0; i < dgv_LS_long_layers.RowCount - 1; i++)
                {
                    val = MyList.StringToDouble(dgv_LS_long_layers[1, i].Value.ToString(), 0.0);
                    dgv_LS_long_layers[3, i].Value = ((cov + dia) + prev_val + val / 2);
                    prev_val += val * 2;
                }
            }
            catch (Exception ex) { }
        }

        private void dgv_LS_long_layers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Steel_Layer_CG_Calculate();
        }

        private void btn_LS_long_report_Click(object sender, EventArgs e)
        {

                 Limit_LongGirder.rep_file_name = rbtn_LS_long_inner_girder.Checked ? Limit_LongGirder.Inner_File : Limit_LongGirder.Outer_File;
            if (File.Exists(Limit_LongGirder.rep_file_name))
            {
                //MessageBox.Show(this, "Report file written in " + Limit_LongGirder.Inner_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //iApp.View_Result(Limit_LongGirder.Inner_File);
                iApp.RunExe(Limit_LongGirder.rep_file_name);
            }
        }

        private void txt_Ana_inner_long_L2_moment_TextChanged(object sender, EventArgs e)
        {
            Limit_State_Force_Update();
        }


        public void Limit_State_Force_Update()
        {
            double bm = 0.0;
            double sf = 0.0;
            if (rbtn_LS_long_inner_girder.Checked)
            {
                bm = MyList.StringToDouble(txt_Ana_inner_long_L2_moment.Text, 0.0);
                sf = MyList.StringToDouble(txt_inner_long_L2_shear.Text, 0.0);

            }
            else if (rbtn_LS_long_outer_girder.Checked)
            {
                bm = MyList.StringToDouble(txt_outer_long_L2_moment.Text, 0.0);
                sf = MyList.StringToDouble(txt_outer_long_L2_shear.Text, 0.0);
            }

            txt_LS_long_BM.Text = (bm * 10).ToString();
            txt_LS_long_SF.Text = (sf * 10).ToString();
        }

        private void rbtn_LS_long_outer_girder_CheckedChanged(object sender, EventArgs e)
        {
            Limit_State_Force_Update();
        }

        private void btn_RccPier_Process_Click(object sender, EventArgs e)
        {

        }

        private void cmb_pier_2_k_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
