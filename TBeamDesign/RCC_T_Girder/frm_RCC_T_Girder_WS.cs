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
    public partial class frm_RCC_T_Girder_WS : Form
    {
        //const string Title = "ANALYSIS OF RCC T-GIRDER BRIDGE (WORKING STRESS METHOD)";

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC T-GIRDER BRIDGE WORKING STRESS [BS]";
                return "DESIGN OF RCC T-GIRDER BRIDGE WORKING STRESS [IRC]";
            }
        }



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
        public frm_RCC_T_Girder_WS(IApplication thisApp)
        {
            InitializeComponent();
            iApp = thisApp;

            user_path = iApp.LastDesignWorkingFolder;


            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);


            long_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Secion_for_Dimensions;
            long_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.TBeam_Main_Girder_Bottom_Flange;

            cross_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Slab_Long_Cross_Girders;
            cross_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.TBeam_Main_Girder_Bottom_Flange;

            //deck_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.;
            deck_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.TBeam_Main_Girder_Bottom_Flange;

            cant_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.Cantilever_Slab;
            cant_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.TBeam_Main_Girder_Bottom_Flange;

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
                if(user_path != iApp.LastDesignWorkingFolder)
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

            #region Long Girder Data
            LongGirders = new LongMainGirders(iApp);
            cmb_Long_concrete_grade.SelectedIndex = 2;
            cmb_Long_Steel_Grade.SelectedIndex = 1;
            long_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Secion_for_Dimensions;
            long_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.RCC_T_Beam_Bridge;

            //pcb_limit_long.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Secion_for_Dimensions;


            #endregion Long Girder Data

            #region Cross Girder Data
            XGirders = new CrossGirders(iApp);
            cross_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Slab_Long_Cross_Girders;
            cross_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.RCC_T_Beam_Bridge;
            #endregion Cross Girder Data

            #region Deck Slab Data
            Deck = new DeckSlab(iApp);
            deck_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Slab_Long_Cross_Girders;
            deck_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.RCC_T_Beam_Bridge;

            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 0;

            #endregion Deck Slab Data

            #region Cantilever Slab Data
            Cant = new CantileverSlab(iApp);
            cant_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Slab_Long_Cross_Girders;
            cant_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.RCC_T_Beam_Bridge;


            cmb_cant_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_cant_select_load.SelectedIndex = 0;

            #endregion Cantilever Slab Data


            #region RCC Abutment
            Abut = new RCC_AbutmentWall(iApp);
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;
            #endregion RCC Abutment
            #region RCC Pier
            cmb_pier_2_k.SelectedIndex = 1;
            rcc_pier = new RccPier(iApp);
            #endregion RCC Pier

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



            cmb_Long_concrete_grade.SelectedIndex = 2;
            cmb_Long_Steel_Grade.SelectedIndex = 1;

            cmb_cross_fck.SelectedIndex = 2;
            cmb_cross_fy.SelectedIndex = 1;

            cmb_cant_fck.SelectedIndex = 2;
            cmb_cant_fy.SelectedIndex = 1;

            cmb_deck_fck.SelectedIndex = 2;
            cmb_deck_fy.SelectedIndex = 1;

            cmb_abut_fck.SelectedIndex = 2;
            cmb_abut_fy.SelectedIndex = 1;

            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;

            //Chiranjit [2013 07 11] Add 2 new tab as Working Stress Design and Limit State Design

            #region Chiranjit [2013 07 11] Remove Tab

            tab_working_design.TabPages.Remove(tab_Worksheet_Design);
            tab_working_design.TabPages.Remove(tab_Interactive_Drawings);

            #endregion Chiranjit [2013 07 11] Remove Tab


            Calculate_Interactive_Values();
            Button_Enable_Disable();

            txt_Ana_B.Text = txt_Ana_B.Text + "";

            tab_main.TabPages.Remove(tab_limit_state);

            //Chiranjit [2013 11 02]
            lst_Drawings.SelectedIndex = 2;
            Text_Changed();


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
                    //IsCreateData = false;
                    string chk_file = "";

                    //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                    IsCreateData = false;


                    chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                    //if (!File.Exists(chk_file)) chk_file = ofd.FileName;

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

                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}

                Button_Enable_Disable();

                grb_create_input_data.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option
        }
        private void btn_WorkSheet_Design_Click(object sender, EventArgs e)
        {
            string excel_file_name = "";
            string copy_path = "";
            Button btn = sender as Button;

            if (btn.Name == btn_WSD_RCC_TGirder.Name)
            {
                excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 1\T_Girder_Bridge_ASTRA.xls");
            }
            else if (btn.Name == btn_WSD_Section_Props.Name)
            {
                excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 2\01 SectionPROP.XLS");
            }
            else if (btn.Name == btn_WSD_SIDL.Name)
            {
                excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 2\02 SIDL.xls");
            }
            else if (btn.Name == btn_WSD_Cross_Girders_Deck_Slab.Name)
            {
                excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 2\03 CrossGirder+Slab Design.xls");
            }
            else if (btn.Name == btn_WSD_Tee_Girders.Name)
            {
                excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 2\04 TeeGirder Design.xls");
            }
            else if (btn.Name == btn_WSD_Open.Name)
            {
                iApp.Open_WorkSheet_Design();
                return;
            }

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

            Write_All_Data(true);

            //user_path = IsCreateData ? Path.Combine(iApp.user_path, Title) : user_path;
            if (!Directory.Exists(user_path))
            user_path = IsCreateData ? Path.Combine(iApp.user_path, Title) : user_path;

            if (Path.GetFileName(user_path) != Project_Name)
            {

                Create_Project();

            }


            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                Ana_Initialize_Analysis_InputData();

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT");
                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;

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

                MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
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
            //iApp.RunExe(Path.Combine(user_path, "LL.txt"));
            iApp.View_Input_File(Bridge_Analysis.TotalAnalysis_Input_File);
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

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                string flPath = Bridge_Analysis.Input_File;
                do
                {
                    if (i == 0)
                        flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                    else if (i == 1)
                    {
                        //MessageBox.Show(this, "PROCESS ANALYSIS FOR LIVE LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flPath = Bridge_Analysis.LiveLoadAnalysis_Input_File;
                    }
                    else if (i == 2)
                    {
                        //MessageBox.Show(this, "PROCESS ANALYSIS FOR DEAD LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;
                    }


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
                while (i < 3);

                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                if (iApp.Show_and_Run_Process_List(pcol))
                {


                    iApp.Progress_Works.Clear();
                    iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


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



                    txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                    txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
                    txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

                    txt_abut_w6.Text = Total_LiveLoad_Reaction;
                    txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                    txt_abut_w6.ForeColor = Color.Red;

                    txt_abut_w5.Text = Total_DeadLoad_Reaction;
                    txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                    txt_abut_w5.ForeColor = Color.Red;
                }

                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


                Calculate_Interactive_Values();

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
                Calculate_Interactive_Values();
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
            txt_sec_in_end_Bf.Text = (MyList.StringToDouble(txt_Long_SMG.Text, 0.0) * 1000).ToString();

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



            LongGirder_Load_Analysis_Data();

            Ana_Write_Max_Moment_Shear();
            Show_Cross_Girder_Forces();
            Show_Long_Girder_Forces();

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
            Show_Cross_Girder_Forces();
            Show_Long_Girder_Forces();
        }
        //Chiranjit [2011 10 29] 
        void Show_Long_Girder_Forces()
        {
            //try
            //{
            //    LongGirders.isInner = rbtn_long_inner_girder.Checked;
            //    LongGirder_Load_Analysis_Data();
            //    LongGirders.FilePath = user_path;
            //    if (rbtn_long_inner_girder.Checked)
            //    {
            //        LongGirders.rep_file_name = Path.Combine(LongGirders.file_path, "T-Beam_Inner_Long_Girder.txt");
            //    }
            //    else if (rbtn_Long_outer_girder.Checked)
            //    {
            //        LongGirders.rep_file_name = Path.Combine(LongGirders.file_path, "T-Beam_Outer_Long_Girder.txt");
            //    }
            //    btn_Long_Report.Enabled = File.Exists(LongGirders.rep_file_name);
            //}
            //catch (Exception ex) { }

            bool isInner = rbtn_long_inner_girder.Checked;

            txt_Long_design_moment_mid.Text = (isInner) ? txt_Ana_inner_long_L2_moment.Text : txt_outer_long_L2_moment.Text;
            txt_Long_design_moment_mid.ForeColor = Color.Red;
            txt_Long_design_shear_mid.Text = (isInner) ? txt_inner_long_L2_shear.Text : txt_outer_long_L2_shear.Text;
            txt_Long_design_shear_mid.ForeColor = Color.Red;

            txt_Long_design_moment_quarter.Text = (isInner) ? txt_Ana_inner_long_L4_moment.Text : txt_outer_long_L4_moment.Text;
            txt_Long_design_moment_quarter.ForeColor = Color.Red;
            txt_Long_design_shear_quarter.Text = (isInner) ? txt_Ana_inner_long_L4_shear.Text : txt_outer_long_L4_shear.Text;
            txt_Long_design_shear_quarter.ForeColor = Color.Red;

            txt_Long_design_moment_deff.Text = (isInner) ? txt_inner_long_deff_moment.Text : txt_outer_long_deff_moment.Text;
            txt_Long_design_moment_deff.ForeColor = Color.Red;
            txt_Long_design_shear_deff.Text = (isInner) ? txt_Ana_inner_long_deff_shear.Text : txt_outer_long_deff_shear.Text;
            txt_Long_design_shear_deff.ForeColor = Color.Red;
        }
        void Show_Cross_Girder_Forces()
        {
            txt_Cross_total_hogging_moment.Text = txt_Ana_cross_max_moment.Text;
            txt_Cross_total_hogging_moment.ForeColor = Color.Red;
            txt_Cross_total_shear.Text = txt_Ana_cross_max_shear.Text;
            txt_Cross_total_shear.ForeColor = Color.Red;
        }

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
            btn_Long_Report.Enabled = rbtn_long_inner_girder.Checked ? File.Exists(LongGirders.Inner_File) : File.Exists(LongGirders.Outer_File);
            btn_cross_report.Enabled = File.Exists(XGirders.rep_file_name);
            btnReport.Enabled = File.Exists(XGirders.rep_file_name);
            btn_Deck_Report.Enabled = File.Exists(Deck.rep_file_name);
            btn_Cant_Report.Enabled = File.Exists(Cant.rep_file_name);
            btn_cnt_Report.Enabled = File.Exists(Abut.rep_file_name);


            btn_dwg_long.Enabled = File.Exists(LongGirders.drawing_path);
            btn_dwg_cross.Enabled = File.Exists(XGirders.user_drawing_file);
            btn_dwg_deck.Enabled = File.Exists(Deck.drawing_path);
            btn_dwg_cant.Enabled = File.Exists(Cant.user_drawing_file);

            //btn_dwg_abutment.Enabled = File.Exists(Abut.drawing_path);
            btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
            //btn_dwg_rcc_pier.Enabled = File.Exists(rcc_pier.rep_file_name);


            btn_Cant_Report.Enabled = File.Exists(Cant.rep_file_name);

            //Write_All_Data();
            //Chiranjit [2013 07 18]
            btn_LS_long_report.Enabled = rbtn_long_inner_girder.Checked ? File.Exists(Limit_LongGirder.Inner_File) : File.Exists(Limit_LongGirder.Outer_File);

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

            Calculate_Interactive_Values();

            Button_Enable_Disable();

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Bridge_Analysis.LiveLoad_File);

            if (Bridge_Analysis.Live_Load_List == null) return;
        }


        public void Ana_OpenAnalysisFile_2013_04_26(string file_name)
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
                if (File.Exists(rep_file))
                {
                    Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, rep_file);
                    Ana_Show_Moment_Shear();
                }
                else
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


                //txt_Ana_eff_depth.Text = Bridge_Analysis.Girder_Analysis.Analysis.Effective_Depth.ToString();
                txt_Ana_Ds.Text = Bridge_Analysis.Girder_Analysis.Analysis.Width_Cantilever.ToString();
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
                Bridge_Analysis.Girder_Analysis = null;
                Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                Ana_Show_Moment_Shear();

                string s = "";
                string s1 = "";
                int i = 0;
                for (i = 0; i < Bridge_Analysis.Girder_Analysis.Supports.Count; i++)
                {
                    if (i < Bridge_Analysis.Girder_Analysis.Supports.Count / 2)
                    {
                        if (i == Bridge_Analysis.Girder_Analysis.Supports.Count / 2 - 1)
                        {
                            s += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo;
                        }
                        else
                            s += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo + ",";
                    }
                    else
                    {
                        if (i == Bridge_Analysis.Girder_Analysis.Supports.Count - 1)
                        {
                            s1 += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo;
                        }
                        else
                            s1 += Bridge_Analysis.Girder_Analysis.Supports[i].NodeNo + ",";
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

                //txt_ana_TSRP.Text = fv.txt_final_vert_rec_kN.Text;
                //txt_ana_MSLD.Text = fv.txt_max_Mx_kN.Text;
                //txt_ana_MSTD.Text = fv.txt_max_Mz_kN.Text;
                //txt_ana_DLSR.Text = f.Total_DeadLoad_Reaction;
                //txt_ana_LLSR.Text = f.Total_LiveLoad_Reaction;

                //txt_RCC_Pier_W1_supp_reac.Text = fv.txt_final_vert_rec_kN.Text;
                //txt_RCC_Pier_Mx1.Text = fv.txt_max_Mx_kN.Text;
                //txt_RCC_Pier_Mz1.Text = fv.txt_max_Mz_kN.Text;

                //txt_abut_w6.Text = f.Total_LiveLoad_Reaction;
                //txt_pier_2_P3.Text = f.Total_LiveLoad_Reaction;
                //txt_abut_w6.ForeColor = Color.Red;

                //txt_abut_w5.Text = f.Total_DeadLoad_Reaction;
                //txt_pier_2_P2.Text = f.Total_DeadLoad_Reaction;
                //txt_abut_w5.ForeColor = Color.Red;

                //Chiranjit [2012 07 10]

                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s + " " + s1));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s, s1);
                frm_ViewDesign_Forces_Load();

                txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
                txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



                txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
                txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

                txt_abut_w6.Text = Total_LiveLoad_Reaction;
                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                txt_abut_w6.ForeColor = Color.Red;

                txt_abut_w5.Text = Total_DeadLoad_Reaction;
                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                txt_abut_w5.ForeColor = Color.Red;
            }

            //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;

            Calculate_Interactive_Values();

            Button_Enable_Disable();
            #endregion


            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Bridge_Analysis.LiveLoad_File);

            if (Bridge_Analysis.Live_Load_List == null) return;

            //cmb_load_type.Items.Clear();
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

            Long_Initialize_LongGirder_InputData();



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
            LongGirders.rep_file_name = rbtn_long_inner_girder.Checked ? LongGirders.Inner_File : LongGirders.Outer_File;


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
        private void rbtn_Long_inner_girder_CheckedChanged(object sender, EventArgs e)
        {
            LongGirders.isInner = rbtn_long_inner_girder.Checked;
            LongGirder_Load_Analysis_Data();
            LongGirders.FilePath = user_path;
            if (rbtn_long_inner_girder.Checked)
            {
                LongGirders.rep_file_name = Path.Combine(LongGirders.file_path, "T-Beam_Inner_Long_Girder.txt");
                txt_Long_SMG.Text = (long_inner_sec.Bf / 1000.0).ToString();
            }
            else if (rbtn_Long_outer_girder.Checked)
            {
                LongGirders.rep_file_name = Path.Combine(LongGirders.file_path, "T-Beam_Outer_Long_Girder.txt");

                txt_Long_SMG.Text = (long_out_sec.Bf / 1000.0).ToString();

            }
            btn_Long_Report.Enabled = File.Exists(LongGirders.rep_file_name);

        }
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_long"))
            {
                astg = new ASTRAGrade(cmb_Long_concrete_grade.Text, cmb_Long_Steel_Grade.Text);
                //txt_Long_allowable_stress_concrete_grade.Text = astg.sigma_c_kg_sq_cm.ToString("f2");
                txt_Long_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_Long_sigma_sv.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_cross"))
            {
                astg = new ASTRAGrade(cmb_cross_fck.Text, cmb_cross_fy.Text);
                txt_cross_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_cross_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_deck") ||
                ctrl.Name.ToLower().StartsWith("txt_deck"))
            {
                astg = new ASTRAGrade(cmb_deck_fck.Text, cmb_deck_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_deck_m.Text, 10.0);
                txt_deck_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_Deck_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
                txt_Deck_j.Text = astg.j.ToString("f3");
                txt_Deck_Q.Text = astg.Q.ToString("f3");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_cant") ||
                ctrl.Name.ToLower().StartsWith("txt_cant"))
            {
                astg = new ASTRAGrade(cmb_cant_fck.Text, cmb_cant_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_cant_m.Text, 10.0);
                txt_cant_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_cant_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
                txt_cant_j.Text = astg.j.ToString("f3");
                txt_cant_Q.Text = astg.Q.ToString("f3");
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



            //if (cmb.Name == cmb_Long_concrete_grade.Name)
            //{
            //    switch (cmb_Long_concrete_grade.Text)
            //    {
            //        case "15":
            //            txt_Long_allowable_stress_concrete_grade.Text = "50.0";
            //            break;
            //        case "20":
            //            txt_Long_allowable_stress_concrete_grade.Text = "66.7";
            //            break;
            //        case "25":
            //            txt_Long_allowable_stress_concrete_grade.Text = "83.3";
            //            break;
            //        case "30":
            //            txt_Long_allowable_stress_concrete_grade.Text = "100.0";
            //            break;
            //        case "35":
            //            txt_Long_allowable_stress_concrete_grade.Text = "116.7";
            //            break;
            //        case "40":
            //            txt_Long_allowable_stress_concrete_grade.Text = "133.3";
            //            break;
            //        case "45":
            //            txt_Long_allowable_stress_concrete_grade.Text = "150.0";
            //            break;
            //        case "50":
            //            txt_Long_allowable_stress_concrete_grade.Text = "166.7";
            //            break;
            //        case "55":
            //            txt_Long_allowable_stress_concrete_grade.Text = "183.0";
            //            break;
            //        case "60":
            //            txt_Long_allowable_stress_concrete_grade.Text = "200.0";
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (cmb_Long_Steel_Grade.Text)
            //    {
            //        case "240":
            //            txt_Long_sigma_sv.Text = "125";
            //            break;
            //        case "415":
            //            txt_Long_sigma_sv.Text = "200";
            //            break;
            //        case "500":
            //            txt_Long_sigma_sv.Text = "240";
            //            break;
            //    }
            //}
        }

        #endregion Long Girder

        #region Long Girders Methods

        public void Long_Initialize_LongGirder_InputData()
        {
            LongGirders.FilePath = user_path;


            if (rbtn_long_inner_girder.Checked)
                LongGirders.rep_file_name = Path.Combine(LongGirders.file_path, "T-Beam_Inner_Long_Girder.txt");
            else if (rbtn_Long_outer_girder.Checked)
                LongGirders.rep_file_name = Path.Combine(LongGirders.file_path, "T-Beam_Outer_Long_Girder.txt");



            LongGirders.Ds = MyList.StringToDouble(txt_Long_Ds.Text, 0.0);
            LongGirders.D = MyList.StringToDouble(txt_Long_DMG.Text, 0.0);
            LongGirders.bw = MyList.StringToDouble(txt_Long_BMG.Text, 0.0);
            LongGirders.L = MyList.StringToDouble(txt_Long_L.Text, 0.0);
            LongGirders.Bf = MyList.StringToDouble(txt_Long_SMG.Text, 0.0);

            LongGirders.design_moment_mid = MyList.StringToDouble(txt_Long_design_moment_mid.Text, 0.0);
            LongGirders.design_moment_quarter = MyList.StringToDouble(txt_Long_design_moment_quarter.Text, 0.0);
            LongGirders.design_moment_deff = MyList.StringToDouble(txt_Long_design_moment_deff.Text, 0.0);

            LongGirders.v1 = MyList.StringToDouble(txt_Long_design_shear_mid.Text, 0.0);
            LongGirders.v2 = MyList.StringToDouble(txt_Long_design_shear_quarter.Text, 0.0);
            LongGirders.v3 = MyList.StringToDouble(txt_Long_design_shear_deff.Text, 0.0);



            LongGirders.concrete_grade = MyList.StringToDouble(cmb_Long_concrete_grade.Text, 0.0);
            LongGirders.allow_stress_concrete = MyList.StringToDouble(txt_Long_sigma_c.Text, 0.0);

            LongGirders.steel_grade = MyList.StringToDouble(cmb_Long_Steel_Grade.Text, 0.0);
            LongGirders.modular_ratio = MyList.StringToDouble(txt_Long_modular_ratio.Text, 0.0);
            LongGirders.bar_dia = MyList.StringToDouble(txt_Long_bar_dia_L2.Text, 0.0);//cm
            LongGirders.total_bars_L2 = MyList.StringToDouble(txt_Long_total_bar_L2.Text, 0.0);

            LongGirders.bar_dia_L2 = MyList.StringToDouble(txt_Long_bar_dia_L2.Text, 0.0);//cm
            LongGirders.total_bars_L2 = MyList.StringToDouble(txt_Long_total_bar_L2.Text, 0.0);

            LongGirders.bar_dia_L4 = MyList.StringToDouble(txt_Long_bar_dia_L4.Text, 0.0);//cm
            LongGirders.total_bars_L4 = MyList.StringToDouble(txt_Long_total_bar_L4.Text, 0.0);

            LongGirders.bar_dia_Deff = MyList.StringToDouble(txt_Long_bar_dia_Deff.Text, 0.0);//cm
            LongGirders.total_bars_Deff = MyList.StringToDouble(txt_Long_total_bar_Deff.Text, 0.0);



            LongGirders.top_bar_dia = MyList.StringToDouble(txt_Long_side_bar_dia.Text, 0.0);

            LongGirders.cover = MyList.StringToDouble(txt_Long_cover.Text, 0.0);
            LongGirders.sigma_sv = MyList.StringToDouble(txt_Long_sigma_sv.Text, 0.0);

            LongGirders.isInner = rbtn_long_inner_girder.Checked;

            //space_main = MyList.StringToDouble(txt_space_main_girder.Text, 0.0);
            LongGirders.space_cross = MyList.StringToDouble(txt_Long_SCG.Text, 0.0);


            //Bs = MyList.StringToDouble(txt_Bs.Text, 0.0);
            LongGirders.Bb = MyList.StringToDouble(txt_Long_Bb.Text, 0.0);
            LongGirders.Db = MyList.StringToDouble(txt_Long_Db.Text, 0.0);
            LongGirders.Db = MyList.StringToDouble(txt_Long_Db.Text, 0.0);
            LongGirders.nl = MyList.StringToDouble(txt_Long_nl.Text, 0.0);

            LongGirders.As = (Math.PI * (LongGirders.bar_dia) * (LongGirders.bar_dia) / 4.0) * LongGirders.total_bars_L2;
            LongGirders.Dw = LongGirders.D - LongGirders.Ds - LongGirders.Db;

            //Steel Area * 100 / (Web Depth * Web Thickness)
            //= (3927 x 100) / (1500 x 300) = 0.87% < 2%)

            //Chiranjit [2013 06 28]
            LongGirders.Node_Displacement_Data = txt_node_displace.Text;

        }
        void Long_Read_Max_Moment_Shear_from_Analysis()
        {

            //string f_path = Environment.GetEnvironmentVariable("TBEAM_ANALYSIS");
            string f_path = Path.Combine(user_path, "FORCES.TXT");
            if (File.Exists(f_path))
            {
                txt_Long_design_shear_quarter.ForeColor = Color.Red;
                txt_Long_design_shear_mid.ForeColor = Color.Red;
                txt_Long_design_shear_deff.ForeColor = Color.Red;
                txt_Long_design_moment_quarter.ForeColor = Color.Red;
                txt_Long_design_moment_mid.ForeColor = Color.Red;
                txt_Long_design_moment_deff.ForeColor = Color.Red;

                List<string> list = new List<string>(File.ReadAllLines(f_path));

                MyList mlist = null;
                string kStr = "";
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i].ToUpper();

                    mlist = new MyList(kStr, '=');
                    if (mlist.StringList.Count > 1)
                    {
                        switch (mlist.StringList[0])
                        {
                            case "LONG_INN_DEFF_MOM":
                                LongGirders.inner_deff_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_DEFF_SHR":
                                LongGirders.inner_deff_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L2_MOM":
                                LongGirders.inner_L2_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L2_SHR":
                                LongGirders.inner_L2_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L4_MOM":
                                LongGirders.inner_L4_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L4_SHR":
                                LongGirders.inner_L4_shear = mlist.GetDouble(1);
                                break;

                            case "LONG_OUT_DEFF_MOM":
                                LongGirders.outer_deff_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_DEFF_SHR":
                                LongGirders.outer_deff_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L2_MOM":
                                LongGirders.outer_L2_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L2_SHR":
                                LongGirders.outer_L2_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L4_MOM":
                                LongGirders.outer_L4_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L4_SHR":
                                LongGirders.outer_L4_shear = mlist.GetDouble(1);
                                break;
                        }
                    }
                }
                LongGirder_Load_Analysis_Data();
            }
        }
        public void LongGirder_Load_Analysis_Data()
        {
            Show_Long_Girder_Forces();
        }
        #endregion Long Girders Methods


        #region Cross Form Event

        private void btn_Cross_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Cross_Initialize_InputData();

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
                Cross_Initialize_InputData();

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
        public void Cross_Initialize_InputData()
        {
            XGirders.FilePath = user_path;
            XGirders.M_total_hogging_moment = MyList.StringToDouble(txt_Cross_total_hogging_moment.Text, 0.0);
            XGirders.L_spacing_longitudinal_girder = MyList.StringToDouble(txt_Cross_SMG.Text, 0.0);
            XGirders.number_longitudinal_girder = MyList.StringToDouble(txt_Cross_NMG.Text, 0.0);
            XGirders.spacing_cross_girder = MyList.StringToDouble(txt_Cross_SCG.Text, 0.0);


            XGirders.number_cross_girder = MyList.StringToDouble(txt_Cross_NCG.Text, 0.0);
            XGirders.D_depth_cross_girder = MyList.StringToDouble(txt_Cross_DCG.Text, 0.0);
            XGirders.b_web_thickness_cross_girder = MyList.StringToDouble(txt_Cross_BCG.Text, 0.0);

            XGirders.W_total_shear = MyList.StringToDouble(txt_Cross_total_shear.Text, 0.0);

            XGirders.clear_cover = MyList.StringToDouble(txt_Cross_clear_cover.Text, 0.0);




            XGirders.grade_concrete = MyList.StringToDouble(cmb_cross_fck.Text, 0.0);
            XGirders.grade_steel = MyList.StringToDouble(cmb_cross_fy.Text, 0.0);
            XGirders.fc_stress_concrete = MyList.StringToDouble(txt_cross_sigma_c.Text, 0.0);
            XGirders.fs_stress_steel = MyList.StringToDouble(txt_cross_sigma_st.Text, 0.0);
            XGirders.m_modular_ratio = MyList.StringToDouble(txt_Cross_modular_ratio.Text, 0.0);

            XGirders.top_bar_dia = MyList.StringToDouble(txt_Cross_top_bar_dia.Text, 0.0);
            XGirders.bottom_bar_dia = MyList.StringToDouble(txt_Cross_bottom_bar_dia.Text, 0.0);
            XGirders.side_bar_dia = MyList.StringToDouble(txt_Cross_side_bar_dia.Text, 0.0);
            XGirders.vertical_bar_dia = MyList.StringToDouble(txt_Cross_vertical_bar_dia.Text, 0.0);

        }
        public void Read_Max_Moment_Shear_from_Analysis()
        {
            //string f_path = Environment.GetEnvironmentVariable("TBEAM_ANALYSIS");
            string f_path = Path.Combine(user_path, "FORCES.TXT");
            if (File.Exists(f_path))
            {
                txt_Cross_total_hogging_moment.ForeColor = Color.Red;
                txt_Cross_total_shear.ForeColor = Color.Red;

                List<string> list = new List<string>(File.ReadAllLines(f_path));

                MyList mlist = null;
                string kStr = "";
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i].ToUpper();

                    mlist = new MyList(kStr, '=');
                    if (mlist.StringList.Count > 1)
                    {
                        if (mlist.StringList[0] == "CROSS_MOM")
                            txt_Cross_total_hogging_moment.Text = mlist.StringList[1];
                        if (mlist.StringList[0] == "CROSS_SHR")
                            txt_Cross_total_shear.Text = mlist.StringList[1];
                    }
                }
            }
        }

        #endregion Cross Girders Methods



        #region Deck Slab Form Event

        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Deck.rep_file_name);
        }
        private void btn_Deck_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();
            Deck.FilePath = user_path;
            Deckslab_Initialize_InputData();
            Deck.Write_User_Input();
            Deck.Calculate_Program(Deck.rep_file_name);
            Deck.Write_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(Deck.rep_file_name)) { MessageBox.Show(this, "Report file written in " + Deck.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(Deck.rep_file_name); }
            Deck.is_process = true;
            Button_Enable_Disable();
        }
        private void txt_Deck_concrete_grade_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion Deck Slab Form Event

        #region Deck Slab Methods
        public void Deckslab_Initialize_InputData()
        {
            try
            {
                Deck.width_carrage_way = MyList.StringToDouble(txt_Deck_CW.Text, 0.0);
                Deck.effe_span = MyList.StringToDouble(txt_Deck_L.Text, 0.0);
                Deck.concrete_grade = MyList.StringToDouble(cmb_deck_fck.Text, 0.0);
                Deck.steel_grade = MyList.StringToDouble(cmb_deck_fy.Text, 0.0);
                Deck.sigma_cb = MyList.StringToDouble(txt_deck_sigma_c.Text, 0.0);
                Deck.sigma_st = MyList.StringToDouble(txt_Deck_sigma_st.Text, 0.0);
                Deck.L = MyList.StringToDouble(txt_Deck_SCG.Text, 0.0);
                Deck.no_main_girder = MyList.StringToDouble(txt_Deck_NMG.Text, 0.0);
                Deck.B = MyList.StringToDouble(txt_B.Text, 0.0);
                Deck.width_cross_girders = MyList.StringToDouble(txt_Deck_BCG.Text, 0.0);
                Deck.width_long_girders = MyList.StringToDouble(txt_Deck_BMG.Text, 0.0);
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

                Deck.Ds = MyList.StringToDouble(txt_Deck_Ds.Text, 0.0);
                Deck.gamma_c = MyList.StringToDouble(txt_Deck_gamma_c.Text, 0.0);
                Deck.Dwc = MyList.StringToDouble(txt_Deck_Dwc.Text, 0.0);
                Deck.gamma_wc = MyList.StringToDouble(txt_Deck_gamma_wc.Text, 0.0);

                Deck.self_weight_slab = (Deck.Ds / 1000) * Deck.gamma_c;
                Deck.self_weight_wearing_cource = (Deck.Dwc / 1000) * Deck.gamma_wc;
                Deck.total_weight = (Deck.self_weight_slab + Deck.self_weight_wearing_cource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }
        }
        #endregion Deck Slab Methods

        #region Cantilever Slab Form Event
        private void btn_Cant_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Cant.rep_file_name);
        }
        private void btn_Cant_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Cant.FilePath = user_path;
            Cantilever_Initialize_InputData();
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

        #region Cantilever Slab Methods
        private void Cantilever_Initialize_InputData()
        {
            #region Variable Initialization
            Cant.a1 = MyList.StringToDouble(txt_Cant_BMG.Text, 0.0);
            Cant.a2 = MyList.StringToDouble(txt_Cant_Wp.Text, 0.0);
            Cant.d1 = MyList.StringToDouble(txt_Cant_d1.Text, 0.0);
            Cant.a3 = MyList.StringToDouble(txt_Cant_a3.Text, 0.0);
            Cant.d2 = MyList.StringToDouble(txt_Cant_d2.Text, 0.0);
            Cant.d3 = MyList.StringToDouble(txt_Cant_d3.Text, 0.0);
            Cant.w1 = MyList.StringToDouble(txt_Cant_w1.Text, 0.0);
            Cant.a4 = MyList.StringToDouble(txt_Cant_a4.Text, 0.0);
            Cant.fact = MyList.StringToDouble(txt_Cant_fact.Text, 0.0);
            Cant.a5 = MyList.StringToDouble(txt_Cant_a5.Text, 0.0);
            Cant.a6 = MyList.StringToDouble(txt_Cant_a6.Text, 0.0);
            Cant.d4 = MyList.StringToDouble(txt_Cant_d4.Text, 0.0);
            Cant.w2 = MyList.StringToDouble(txt_Cant_w2.Text, 0.0);
            Cant.a7 = MyList.StringToDouble(txt_Cant_a7.Text, 0.0);
            Cant.rcc_x = MyList.StringToDouble(txt_Cant_RCC_X.Text, 0.0);
            Cant.rcc_y = MyList.StringToDouble(txt_Cant_RCC_Y.Text, 0.0);
            Cant.wid_hnd_rail = MyList.StringToDouble(txt_Cant_width_of_hand_rail.Text, 0.0);


            Cant.concrete_grade = MyList.StringToDouble(cmb_cant_fck.Text, 0.0);
            Cant.steel_grade = MyList.StringToDouble(cmb_cant_fy.Text, 0.0);
            Cant.sigma_cb = MyList.StringToDouble(txt_cant_sigma_c.Text, 0.0);
            Cant.sigma_st = MyList.StringToDouble(txt_cant_sigma_st.Text, 0.0);
            Cant.m = MyList.StringToDouble(txt_cant_m.Text, 0.0);
            Cant.j = MyList.StringToDouble(txt_cant_j.Text, 0.0);
            Cant.Q = MyList.StringToDouble(txt_cant_Q.Text, 0.0);
            Cant.gamma_c = MyList.StringToDouble(txt_Cant_gamma_c.Text, 0.0);
            Cant.gamma_wc = MyList.StringToDouble(txt_Cant_gamma_wc.Text, 0.0);
            Cant.cover = MyList.StringToDouble(txt_Cant_cover.Text, 0.0);

            #endregion
        }

        #endregion Cantilever Slab Methods

        //Chiranjit [2012 05 27]
        #region Abutment
        private void btn_Abutment_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();
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
            Abut.p = MyList.StringToDouble(txt_abut_p_bearing_capacity.Text, 0.0);
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

            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);
            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);

            //rcc_pier.sigma_s = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
            //rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);



            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
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


        #region Chiranjit [2012 06 20]
        void Calculate_Interactive_Values()
        {
            //return;
            //double eff_dp = MyList.StringToDouble(txt_Ana_eff_depth.Text, 0.0);
            double eff_dp = Deff;
            double cover = MyList.StringToDouble(txt_Long_cover.Text, 0.0);
            double bdia = MyList.StringToDouble(txt_Long_bar_dia_Deff.Text, 0.0);
            double lg_spa = MyList.StringToDouble(txt_Long_SMG.Text, 0.0);
            double cg_spa = MyList.StringToDouble(txt_Long_SCG.Text, 0.0);
            double len = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            double wd = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            double cant_wd = MyList.StringToDouble(txt_Ana_Ds.Text, 0.0);
            double long_bw = MyList.StringToDouble(txt_Long_BMG.Text, 0.0);



            //Long Girder
            //Effective Depth+50+[4x28+(4-1)x28]/2
            //txt_Long_DMG.Text = "" + (eff_dp * 1000 + cover + ((4 * bdia + 3 * bdia) / 2.0)).ToString("f3");
            txt_Long_L.Text = len.ToString("f3");

            //Cross Girders

            int lng_no = (int)((wd - 2 * cant_wd) / lg_spa);
            int cg_no = (int)((len) / lg_spa);
            lng_no++;
            cg_no++;

            //txt_Cross_SMG.Text = lg_spa.ToString("f3");
            //txt_Cross_NMG.Text = lng_no.ToString();

            //txt_Cross_SCG.Text = cg_spa.ToString("f3");
            //txt_Cross_NCG.Text = cg_no.ToString();

            //txt_Cross_grade_concrete.Text = cmb_Long_concrete_grade.Text;
            //txt_Cross_grade_steel.Text = cmb_Long_Steel_Grade.Text;



            // Deck Slab

            //txt_Deck_CW.Text = (wd - 0.5).ToString("f3");
            //txt_Deck_L.Text = (len).ToString("f3");

            //txt_Deck_SCG.Text = cg_spa.ToString("f3");
            //txt_Deck_SMG.Text = lg_spa.ToString("f3");
            //txt_Deck_NMG.Text = lng_no.ToString("f0");

            txt_Deck_Ds.Text = txt_Long_Db.Text;

            cmb_cross_fck.SelectedIndex = cmb_Long_concrete_grade.SelectedIndex;
            cmb_cross_fy.SelectedIndex = cmb_Long_Steel_Grade.SelectedIndex;

            txt_Deck_BCG.Text = (MyList.StringToDouble(txt_Cross_BCG.Text, 0.0) * 1000).ToString();
            txt_Deck_BMG.Text = txt_Long_BMG.Text;

            //Cantilever Slab
            txt_Cant_BMG.Text = (long_bw / 1000.0).ToString("f3");
            txt_Cant_a3.Text = (cant_wd - (long_bw / (2 * 1000.0))).ToString("f3");

            txt_Cant_w1.Text = txt_Deck_applied_load.Text;
            txt_Cant_a4.Text = txt_Deck_load_width.Text;
            //txt_Cant_concrete_grade.Text = cmb_Long_concrete_grade.Text;
            //txt_Cant_steel_grade.Text = cmb_Long_Steel_Grade.Text;
            txt_Cant_gamma_c.Text = txt_Deck_gamma_c.Text;

            //Abutment
            //txt_abut_DMG.Text = ((MyList.StringToDouble(txt_Long_DMG.Text, 0.0) / 1000) + 0.2).ToString("f3");
            txt_abut_DMG.Text = (DMG + Ds - MyList.StringToDouble(txt_abut_d3.Text, 0.0)).ToString("f3");
            txt_abut_B.Text = wd.ToString("f3");
            //txt_abut_concrete_grade.Text = cmb_Long_concrete_grade.Text;
            //txt_abut_steel_grade.Text = cmb_Long_Steel_Grade.Text;
            txt_abut_gamma_c.Text = txt_Deck_gamma_c.Text;
            txt_abut_L.Text = len.ToString("f3");


            //RCC Pier Form 1
            txt_RCC_Pier_L.Text = len.ToString("f3");
            txt_RCC_Pier_CW.Text = txt_Deck_CW.Text;
            txt_RCC_Pier_DMG.Text = DMG.ToString("f3");
            txt_RCC_Pier_Ds.Text = Ds.ToString("f3");

            txt_RCC_Pier__B.Text = wd.ToString("f3");
            txt_RCC_Pier_NMG.Text = lng_no.ToString("f0");
            txt_RCC_Pier_NP.Text = lng_no.ToString("f0");
            txt_RCC_Pier___B.Text = (wd + 2.0).ToString("f3");


            double B9 = MyList.StringToDouble(txt_RCC_Pier_B9.Text, 0.0);
            double B10 = MyList.StringToDouble(txt_RCC_Pier_B10.Text, 0.0);
            double B11 = MyList.StringToDouble(txt_RCC_Pier_B11.Text, 0.0);
            double B12 = MyList.StringToDouble(txt_RCC_Pier_B12.Text, 0.0);
            double B14 = MyList.StringToDouble(txt_RCC_Pier___B.Text, 0.0);

            //txt_RCC_Pier_D.Text = ((B10 + B12) / 2.0).ToString("f3");
            //txt_RCC_Pier_b.Text = ((B9 + B11) / 2.0).ToString("f3");



            txt_pier_2_SBC.Text = txt_abut_p_bearing_capacity.Text;
            txt_pier_2_SC.Text = txt_abut_sc.Text;
            txt_pier_2_B16.Text = ((B14 - B12) / 2.0).ToString("f3");
        }
        void Text_Changed()
        {
            double SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);

            double Bb = MyList.StringToDouble(txt_Ana_Bb.Text, 0.65);
            double Db = MyList.StringToDouble(txt_Ana_Db.Text, 0.65);

            //Chiranjit [2012 12 26]
            //DMG = L / 10.0;
            DCG = DMG - 0.4;

            CW = B - Bs - 2 * Wp;


            txt_Long_Bb.Text = (Bb * 1000.0).ToString();
            txt_Long_Db.Text = (Db * 1000.0).ToString();


            txt_LS_long_Bb.Text = (Bb * 1000.0).ToString(); // Chiranjit [2013 07 18]
            txt_LS_long_Db.Text = (Db * 1000.0).ToString(); // Chiranjit [2013 07 18]
            txt_LS_long_Bs.Text = (SMG * 1000.0).ToString(); // Chiranjit [2013 07 18]



            txt_Long_SMG.Text = SMG.ToString("f3");
            txt_Long_SCG.Text = SCG.ToString("f3");

            txt_Cross_SMG.Text = SMG.ToString("f3");
            txt_Cross_SCG.Text = SCG.ToString("f3");

            txt_Deck_SMG.Text = SMG.ToString("f3");
            txt_Deck_SCG.Text = SCG.ToString("f3");

            //txt__SMG.Text = SMG.ToString("f3");
            //txt_Deck_SCG.Text = SCG.ToString("f3");


            txt_Deck_L.Text = txt_Ana_L.Text;


            txt_LL_load_gen.Text = ((L + Math.Abs(MyList.StringToDouble(txt_Ana_X.Text, 0.0))) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");
            //txt_LL_load_gen.Text = (L  / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");

            txt_abut_L.Text = txt_Ana_L.Text;


            txt_Long_L.Text = L.ToString("f3");
            txt_Deck_L.Text = L.ToString("f3");
            txt_abut_L.Text = L.ToString("f3");
            txt_RCC_Pier_L.Text = L.ToString("f3");

            txt_abut_B.Text = B.ToString("f3");
            txt_RCC_Pier__B.Text = B.ToString("f3");
            txt_RCC_Pier___B.Text = B.ToString("f3");

            txt_Deck_CW.Text = CW.ToString();
            txt_RCC_Pier_CW.Text = CW.ToString();

            txt_Long_Ds.Text = (Ds * 1000).ToString();
            txt_Deck_Ds.Text = (Ds * 1000).ToString();
            txt_RCC_Pier_Ds.Text = (Ds).ToString();
            txt_LS_long_Ds.Text = (Ds * 1000.0).ToString(); // Chiranjit [2013 07 18]

            txt_Deck_gamma_c.Text = Y_c.ToString();
            txt_abut_gamma_c.Text = Y_c.ToString();
            txt_RCC_Pier_gama_c.Text = Y_c.ToString();
            txt_Cant_gamma_c.Text = Y_c.ToString();

            txt_Cross_NMG.Text = NMG.ToString();
            txt_Deck_NMG.Text = NMG.ToString();
            txt_RCC_Pier_NMG.Text = NMG.ToString();
            txt_RCC_Pier_NP.Text = NMG.ToString();

            txt_Long_DMG.Text = (DMG * 1000).ToString();
            txt_abut_DMG.Text = (DMG + 0.2).ToString();
            txt_RCC_Pier_DMG.Text = (DMG).ToString();
            txt_LS_long_D.Text = (DMG * 1000.0).ToString(); // Chiranjit [2013 07 18]

            txt_Long_BMG.Text = (BMG * 1000).ToString();
            txt_Deck_BMG.Text = (BMG * 1000).ToString();
            txt_Cant_BMG.Text = (BMG).ToString();
            txt_LS_long_bw.Text = (BMG * 1000.0).ToString(); // Chiranjit [2013 07 18]

            txt_Cross_NCG.Text = NCG.ToString();

            txt_Cross_DCG.Text = DCG.ToString();

            txt_Cross_BCG.Text = BCG.ToString();
            txt_Deck_BCG.Text = (BCG * 1000).ToString();

            txt_Deck_Dwc.Text = (Dw * 1000).ToString();

            txt_Deck_gamma_wc.Text = Y_w.ToString();
            txt_Cant_gamma_wc.Text = Y_w.ToString();
            //txt_cnt_gamma_b.Text = Y_w.ToString();

            txt_RCC_Pier_Hp.Text = Hp.ToString();

            txt_Cant_Wp.Text = Wp.ToString();
            txt_Cant_a3.Text = (CL - Wp).ToString();

            txt_RCC_Pier_Wp.Text = Wp.ToString();


            Ana_Initialize_Analysis_InputData();
            Calculate_Load_Computation();
        }
        private void txt_Ana_width_TextChanged(object sender, EventArgs e)
        {
            //Calculate_Interactive_Values();
            Text_Changed();


            //if (((TextBox)sender).Name == txt_Ana_B.Name)
            //    CW = B - 2.0;
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
            if (user_path != iApp.LastDesignWorkingFolder)
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

        private void cmb_deck_applied_load_SelectedIndexChanged(object sender, EventArgs e)
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
            else if (cmb.Name == cmb_cant_select_load.Name)
            {
                ld = LoadApplied.Get_Applied_Load(cmb_cant_select_load.Text);

                txt_Cant_w1.Text = ld.Applied_Load.ToString();
                txt_Cant_a4.Text = ld.LoadWidth.ToString();
            }
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


            txt_abut_B.Text = txt_RCC_Pier__B.Text = txt_RCC_Pier___B.Text = txt_Ana_B.Text;

            txt_RCC_Pier_L.Text = txt_abut_L.Text = txt_Ana_L.Text;



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

            txt_final_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_max_Mx_kN.Text + " kN-m");
            txt_final_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
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

            iApp.Save_Form_Record(this, user_path);
            return;


            List<string> file_content = new List<string>();

            string kFormat = "{0} = {1} = {2}";

            #region ANALYSIS OF BRIDGE DECK
            file_content.Add(string.Format("ANALYSIS OF BRIDGE DECK"));
            file_content.Add(string.Format("-----------------------"));

            file_content.Add(string.Format(kFormat,
                "Length of Deck Span (along X-direction)",
                "L",
                L));
            file_content.Add(string.Format(kFormat,
                "Width of Bridge Deck  (along Z-direction)",
                "B",
                B));
            file_content.Add(string.Format(kFormat,
                "Carriageway Width ",
                "CW",
                CW));
            file_content.Add(string.Format(kFormat,
                "Width of Left Cantilever part of Deck Slab",
                "CL",
                CL));
            file_content.Add(string.Format(kFormat,
                "Width of Right Cantilever part of Deck Slab",
                "CR",
                CR));
            //file_content.Add(string.Format(kFormat,
            //    "",
            //    "_",
            //    ));

            file_content.Add(string.Format(kFormat, "Thickness of Deck Slab", "Ds", Ds));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit weight of Concrete ", "Y_c", Y_c));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Skew Angle ", "Ang", Ang));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total number of main long girders (must be >= 3)", "NMG", NMG));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Depth of main long girders ", "DMG", DMG));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Web thickness of main long girders", "BMG", BMG));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total number of Cross girders (must be >= 3)", "NCG", NCG));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Depth of cross girders", "DCG", DCG));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Web thickness of cross girders", "BCG", BCG));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Wearing Course", "Dw", Dw));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit weight of Wearing Course", "Y_w", Y_w));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Parapet Wall", "Wp", Wp));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Parapet Wall", "Hp", Hp));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Side walk width", "Bs", Bs));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Side walk height", "Hs", Hs));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Side walk Parapet Wall width", "Wps", Wps));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Side walk Parapet Wall height", "Hps", Hps));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load Factor", "swf", swf));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));

            #endregion ANALYSIS OF BRIDGE DECK

            #region MOVING LOAD INPUT

            file_content.Add(string.Format("MOVING LOAD INPUT"));
            file_content.Add(string.Format("-----------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format("LoadType                           X       Y    Z   XINCR"));
            //"LoadType","X","Y","Z","    XINCR"));
            for (int i = 0; i < dgv_live_load.Rows.Count; i++)
            {
                try
                {
                    file_content.Add(string.Format("{0,-30}, {1:20}, {2:30}, {3:30}, {4:30}, {5:30}",
                    dgv_live_load[0, i].Value.ToString(),
                    dgv_live_load[1, i].Value.ToString(),
                    dgv_live_load[2, i].Value.ToString(),
                    dgv_live_load[3, i].Value.ToString(),
                    dgv_live_load[4, i].Value.ToString(),
                    dgv_live_load[5, i].Value.ToString()));
                }
                catch (Exception ex) { }
            }
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load Generation", "LG", txt_LL_load_gen.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));

            #endregion  MOVING LOAD INPUT

            #region LONG GIRDER USER INPUT

            file_content.Add(string.Format("LONG GIRDER USER INPUT"));
            file_content.Add(string.Format("----------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Deck Slab", "Ds", txt_Long_Ds.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Depth of Longitudinal Girder", "D", txt_Long_DMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Web Thickness of Longitudinal Girders", "Bw", txt_Long_BMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Span of Girders", "L", txt_Long_L.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Spacing of main Girders", "SMG", txt_Long_SMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Spacing of Cross Girders", "SCG", txt_Long_SCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Bottom Flange", "Bb", txt_Long_Bb.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Bottom Flange", "Db", txt_Long_Db.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Concrete Grade [fck]", "fck", cmb_Long_concrete_grade.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Allowable Flexural Stress in Concrete [s_c] ", "s_c", txt_Long_sigma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_Long_Steel_Grade.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permissible Stress in Steel", "s_sv", txt_Long_sigma_sv.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Modular Ratio ", "m", txt_Long_modular_ratio.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "", "", ""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Bars at L/2", "TB_L_2", txt_Long_total_bar_L2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Bars at L/4", "TB_L_4", txt_Long_total_bar_L4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Bars at Deff.", "TB_Deff", txt_Long_total_bar_Deff.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bar Dia", "", ""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bar Dia at L/2", "BD_L_2", txt_Long_bar_dia_L2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bar Dia at L/4", "BD_L_4", txt_Long_bar_dia_L4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bar Dia at Deff.", "BD_Deff", txt_Long_bar_dia_Deff.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Layers of Bars [nl]", "nl", txt_Long_nl.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Cover", "cv", txt_Long_cover.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));

            #endregion LONG GIRDER USER INPUT

            #region  CROSS GIRDER USER INPUT

            file_content.Add(string.Format("CROSS GIRDER USER INPUT"));
            file_content.Add(string.Format("-----------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Hogging Moment", "THM", txt_Cross_total_hogging_moment.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Total Shear", "TS", txt_Cross_total_shear.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Spacing of Longitudinal Main Girders", "SMG", txt_Cross_SMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Number of Longitudinal Girders", "NMG", txt_Cross_NMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Spacing of Cross Girders", "SCG", txt_Cross_SCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Number of Cross Girders", "NCG", txt_Cross_NCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Depth of Cross Girders", "DCG", txt_Cross_DCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Web thickness of Cross Girders", "BCG", txt_Cross_BCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Clear Cover", "cc", txt_Cross_clear_cover.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_cross_fck.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Allowable Flexural Stress in Concrete", "s_c", txt_cross_sigma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_cross_fy.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permissible Stress in Steel", "s_st", txt_cross_sigma_st.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Modular Ratio", "m", txt_Cross_modular_ratio.Text));
            file_content.Add(string.Format(""));

            #endregion CROSS GIRDER USER INPUT

            #region   DECK SLAB USER INPUT


            file_content.Add(string.Format(""));
            file_content.Add(string.Format("DECK SLAB USER INPUT"));
            file_content.Add(string.Format("--------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Carrage way", "CW", txt_Deck_CW.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Effective Span of Tee Beam", "L", txt_Deck_L.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Spacing of Cross Girders ", "SCG", txt_Deck_SCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Nos. Of Main Girders", "NMG", txt_Deck_NMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Spacing of Main Girders", "SMG", txt_Deck_SMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of concrete Deck Slab", "Ds", txt_Deck_Ds.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Asphalt Wearing Cource", "Dwc", txt_Deck_Dwc.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit Weight of Concrete Deck Slab", "Y_c", txt_Deck_gamma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit weight of Asphalt Wearing Cource", "Y_wc", txt_Deck_gamma_wc.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Cross Girders", "BCG", txt_Deck_BCG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Long Girders", "BMG", txt_Deck_BMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Minimum Cover", "mc", txt_Deck_minimum_cover.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Select Load ", "SL", cmb_deck_select_load.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Applied Load ", "AL", txt_Deck_applied_load.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load Width", "LW", txt_Deck_load_width.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load Length", "LL", txt_Deck_load_length.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Impact Factor", "IM", txt_Deck_impact_factor.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Continuity Factor", "CF", txt_Deck_continuity_factor.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Constant [µ]", "mu", txt_Deck_mu.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_deck_fck.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Allowable Flexural Stress in Concrete ", "s_c", txt_deck_sigma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_deck_fy.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permissible Stress in Steel", "s_st", txt_Deck_sigma_st.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Modular Ratio", "m", txt_deck_m.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Lever arm factor", "j", txt_Deck_j.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Moment factor", "Q", txt_Deck_Q.Text));
            file_content.Add(string.Format(""));

            #endregion DECK SLAB USER INPUT

            #region    CANTILEVER USER INPUT

            file_content.Add(string.Format(""));
            file_content.Add(string.Format("CANTILEVER USER INPUT"));
            file_content.Add(string.Format("---------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Girder", "a1", txt_Cant_BMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Kerb ", "Wp", txt_Cant_Wp.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Kerb", "d1", txt_Cant_d1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance from Girder Centre to Kerb", "a3", txt_Cant_a3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Cantilever Slab at Girder face", "d2", txt_Cant_d2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Cantilever Slab at Free End", "d3", txt_Cant_d3.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Select Load ", "SL", cmb_cant_select_load.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Applied Load", "AL", txt_Cant_w1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load Width", "LW", txt_Cant_a4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Impact Factor", "IF", txt_Cant_fact.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance of Load Centre from Girder Face", "a5", txt_Cant_a5.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance of Edge of Load from Kerb Face", "a6", txt_Cant_a6.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of wearing Course", "d4", txt_Cant_d4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load from Hand Rails", "w2", txt_Cant_w2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance from Post Edge to Free End", "a7", txt_Cant_a7.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "RCC Posts Width", "RCC_X", txt_Cant_RCC_X.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "RCC Posts Height", "RCC_Y", txt_Cant_RCC_Y.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Hand Railing", "WHR", txt_Cant_width_of_hand_rail.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit Weight of Concrete", "γ_c", txt_Cant_gamma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit Weight of Wearing Coat", "γ_wc", txt_Cant_gamma_wc.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Clear Cover", "cc", txt_Cant_cover.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_cant_fck.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Allowable Flexural Stress in Concrete", "σ_c", txt_cant_sigma_c.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_cant_fy.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Permissible Stress in Steel", "σ_st", txt_cant_sigma_st.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Modular Ratio", "m", txt_cant_m.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Lever arm factor ", "j", txt_cant_j.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Moment factor", "Q", txt_cant_Q.Text));
            file_content.Add(string.Format(""));

            #endregion CANTILEVER USER INPUT

            #region ABUTMENT USER INPUT

            file_content.Add(string.Format(""));
            file_content.Add(string.Format("ABUTMENT USER INPUT"));
            file_content.Add(string.Format(kFormat, "--------------------", "", ""));
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
                File.WriteAllLines(Bridge_Analysis.User_Input_Data, file_content.ToArray());
            }
            catch
            {
            }
        }
        public void Read_All_Data()
        {
            //if (iApp.IsDemo) return;

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

            long_inner_sec.modular_ratio = MyList.StringToDouble(txt_Long_modular_ratio.Text, 10.0);
            long_inner_sec.Ast = MyList.StringToDouble(txt_Long_total_bar_L2.Text, 10.0) * Math.PI * Math.Pow(MyList.StringToDouble(txt_Long_bar_dia_L2.Text, 10.0), 2.0) / 4;
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
            iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
        }



        #region Create Project / Open Project

        public void Create_Project()
        {
            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            //if (!Directory.Exists(user_path))
            //{
            //    Directory.CreateDirectory(user_path);
            //}

            //user_path = Path.Combine(user_path, Project_Name);
            //if (!Directory.Exists(user_path))
            //{
            //    Directory.CreateDirectory(user_path);
            //}

            //string fname = Path.Combine(user_path, Project_Name + ".apr");

            //int ty = (int)Project_Type;
            //File.WriteAllText(fname, ty.ToString());


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

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
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

                    txt_project_name.Text = Path.GetFileName(user_path);

                    Write_All_Data();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                IsCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }

      

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
                return eASTRADesignType.RCC_T_Girder_Bridge_WS;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]

        private void btn_dwg_open_GAD_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            eOpenDrawingOption opt = iApp.Open_Drawing_Option();

            if (opt == eOpenDrawingOption.Cancel) return;



            //string draw = Path.Combine(Drawing_Folder, "Drawings of RCC T Girder Bridge");
            string draw = Drawing_Folder;


            string copy_path = LongGirders.rep_file_name;

            //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_LONG_GIRDER, draw, copy_path).ShowDialog();

            //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_CROSS_GIRDER, draw, copy_path).ShowDialog();
            //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, draw, copy_path).ShowDialog();

            if (opt == eOpenDrawingOption.Design_Drawings)
            {
                #region Design Drawings
                if (b.Name == btn_dwg_open_GAD.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_GAD, draw, copy_path).ShowDialog();
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of RCC T Girder Bridge"), "RCC_T_GIRDER_LS");

                }
                else if (b.Name == btn_dwg_open_LongGirder.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_LONG_GIRDER, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_CrossGirder.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_CROSS_GIRDER, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Deckslab.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_PIER, draw, copy_path).ShowDialog();
                }
                #endregion Design Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {
                #region Sample Drawings
                if (b.Name == btn_dwg_open_GAD.Name)
                {
                    //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_GAD, draw, copy_path).ShowDialog();
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of RCC T Girder Bridge"), "RCC_T_GIRDER_LS");
                }
                else if (b.Name == btn_dwg_open_LongGirder.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of RCC T Girder Bridge"), "RCC_T_GIRDER_LS");
                    //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_LONG_GIRDER, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_CrossGirder.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_CROSS_GIRDER, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Deckslab.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Counterfort Abutment Drawings"), "BOX_ABUTMENT");
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Canlilever Abutment Drawings"), "TBeam_Abutment");
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "TBeam_Pier");
                }
                #endregion Sample Drawings
            }
        }


    }


    public class TGirderAnalysis
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        JointNode[,] Joints_Array;
        Member[,] Long_Girder_Members_Array;
        Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Girder_Analysis = null;
        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        //Chiranjit [2012 12 18]
        public TLong_SectionProperties T_Long_Inner_Section { get; set; }
        public TLong_SectionProperties T_Long_Outer_Section { get; set; }
        public TCross_SectionProperties T_Cross_Section { get; set; }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        //Chiranjit [2013 06 06] Kolkata

        string list_envelop_inner = "";
        string list_envelop_outer = "";


        string input_file, user_path;
        public TGirderAnalysis(IApplication thisApp)
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

                double val = ((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (Number_Of_Long_Girder - 1));
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //chiranji [2013 05 03]
                //return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);

                double val = (Length - 2 * Effective_Depth) / (Number_Of_Cross_Girder - 1);
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
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
        public int Number_Of_Long_Girder { get; set; }
        public int Number_Of_Cross_Girder { get; set; }


        #endregion Properties



        //Chiranjit [2013 05 02]
        string support_left_joints = "";
        string support_right_joints = "";

        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();


        //Chiranjit [2011 08 01]
        //Create Bridge Input Data by user's given values.
        //Long Girder Spacing, Cross Girder Spacing, Cantilever Width
        public void CreateData()
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

                    if (iCols == 0 && iRows >= 1 && iRows <= _Rows - 2)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1 && iRows >= 1 && iRows <= _Rows - 2)
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

        public void CreateData_2013_05_02()
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

            }
            while (last_x <= Length);
            list_x.Sort();


            //list_z.Clear();
            //list_z.Add(0);
            //list_z.Add(WidthCantilever);
            //list_z.Add(WidthCantilever / 2);
            //list_z.Add(WidthBridge - WidthCantilever);
            //list_z.Add(WidthBridge - WidthCantilever / 2);
            //list_z.Add(WidthBridge);
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    if (!list_z.Contains(last_z) && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;

            //} while (last_z <= WidthBridge);
            list_z.Clear();
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

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS WITH MOVING LOAD BUT NO FIXED LOAD");
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
                else if ((MemColls[i].StartNode.Z  == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z ==  WidthBridge ) &&
                    (MemColls[i].EndNode.Z  ==  WidthBridge ))
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
            if (T_Long_Inner_Section != null)
            {

                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    Cross_Girders_as_String,
                    T_Cross_Section.Area_in_Sq_m,
                    T_Cross_Section.Ixx_in_Sq_Sq_m,
                    T_Cross_Section.Izz_in_Sq_Sq_m));

                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    Outer_Girders_as_String,
                    T_Long_Outer_Section.Area_in_Sq_m,
                    T_Long_Outer_Section.Ixx_in_Sq_Sq_m,
                    T_Long_Outer_Section.Izz_in_Sq_Sq_m));


                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                     Inner_Girders_as_String,
                    T_Long_Inner_Section.Area_in_Sq_m,
                    T_Long_Inner_Section.Ixx_in_Sq_Sq_m,
                    T_Long_Inner_Section.Izz_in_Sq_Sq_m));

                //list.Add(string.Format("191 TO 220 PRIS AX {0:f4} IX {1:f4} IZ {2:f4} ",
                //    T_Long_Outer_Section.Area_in_Sq_m,
                //    T_Long_Outer_Section.Ixx_in_Sq_Sq_m,
                //    T_Long_Outer_Section.Izz_in_Sq_Sq_m));
                //list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                //list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                //list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                //list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                //list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                //list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                //list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                //list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                //list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                //list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                //list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                //list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
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
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0} {1}", support_right_joints, End_Support));

            
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

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS WITH MOVING LOAD BUT NO FIXED LOAD");
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

            if (T_Long_Inner_Section != null)
            {

                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    Cross_Girders_as_String,
                    T_Cross_Section.Area_in_Sq_m,
                    T_Cross_Section.Ixx_in_Sq_Sq_m,
                    T_Cross_Section.Izz_in_Sq_Sq_m));

                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    Outer_Girders_as_String,
                    T_Long_Outer_Section.Area_in_Sq_m,
                    T_Long_Outer_Section.Ixx_in_Sq_Sq_m,
                    T_Long_Outer_Section.Izz_in_Sq_Sq_m));


                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                     Inner_Girders_as_String,
                    T_Long_Inner_Section.Area_in_Sq_m,
                    T_Long_Inner_Section.Ixx_in_Sq_Sq_m,
                    T_Long_Inner_Section.Izz_in_Sq_Sq_m));
                //list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                //list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                //list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                //list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                //list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                //list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                //list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                //list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                //list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                //list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                //list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                //list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
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
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}   {1}", support_right_joints, End_Support));


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

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS WITH MOVING LOAD BUT NO FIXED LOAD");
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

            if (T_Long_Inner_Section != null)
            {

                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    Cross_Girders_as_String,
                    T_Cross_Section.Area_in_Sq_m,
                    T_Cross_Section.Ixx_in_Sq_Sq_m,
                    T_Cross_Section.Izz_in_Sq_Sq_m));

                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    Outer_Girders_as_String,
                    T_Long_Outer_Section.Area_in_Sq_m,
                    T_Long_Outer_Section.Ixx_in_Sq_Sq_m,
                    T_Long_Outer_Section.Izz_in_Sq_Sq_m));


                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                     Inner_Girders_as_String,
                    T_Long_Inner_Section.Area_in_Sq_m,
                    T_Long_Inner_Section.Ixx_in_Sq_Sq_m,
                    T_Long_Inner_Section.Izz_in_Sq_Sq_m));
                    //list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
                //list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
                //list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
                //list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
                //list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
                //list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                //list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
                //list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
                //list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
                //list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
                //list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
                //list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
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
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}   {1}", support_right_joints, End_Support));


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
    }

    public class LongMainGirders
    {
        #region Variables

        public string rep_file_name = "";
        public string file_path = "";
        public string user_input_file = "";
        public string system_path = "";
        public string user_path = "";
        public string drawing_path = "";
        public bool is_process = false;

        public double Ds;
        public double total_bars_L2, total_bars_L4, total_bars_Deff;
        public double D;
        public double Bb, Bf, Db, Dw, As, nl;

        public double bw;
        public double L;
        public double Gs;
        public double design_moment_mid;
        public double design_moment_quarter;
        public double design_moment_deff;
        public double concrete_grade;
        public double steel_grade;
        public double modular_ratio;
        public double bar_dia;
        public double v1, v2, v3;
        public double cover;
        public double sigma_sv;
        public double deff;
        public double space_main, space_cross;
        public double allow_stress_concrete;


        //chiranjit [2016 07 01]
        public double top_bar_dia;


        public bool isInner;
        IApplication iApp = null;

        public double bar_dia_L2, bar_dia_L4, bar_dia_Deff;


        public double inner_deff_moment = 0.0;
        public double outer_deff_moment = 0.0;

        public double inner_L4_moment = 0.0;
        public double outer_L4_moment = 0.0;

        public double inner_L2_moment = 0.0;
        public double outer_L2_moment = 0.0;

        public double inner_deff_shear = 0.0;
        public double outer_deff_shear = 0.0;

        public double inner_L4_shear = 0.0;
        public double outer_L4_shear = 0.0;

        public double inner_L2_shear = 0.0;
        public double outer_L2_shear = 0.0;

        #endregion Variables


        #region Drawing Variable
        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7, _bd8, _bd9, _bd10;
        double _n1, _n2, _n3, _n4, _n5, _n6, _n7, _n8, _n9, _n10;
        double _sp7, _sp8, _sp9, _sp10;
        #endregion

        public LongMainGirders(IApplication App)
        {
            this.iApp = App;

            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;
            _bd8 = 0.0;
            _bd9 = 0.0;
            _bd10 = 0.0;
            _n1 = 0.0;
            _n2 = 0.0;
            _n3 = 0.0;
            _n4 = 0.0;
            _n5 = 0.0;
            _n6 = 0.0;
            _n7 = 0.0;
            _n8 = 0.0;
            _n9 = 0.0;
            _n10 = 0.0;
            _sp7 = 0.0;
            _sp8 = 0.0;
            _sp9 = 0.0;
            _sp10 = 0.0;
        }
        public string FilePath
        {
            set
            {
                user_path = value;

                file_path = user_path;
                //file_path = Path.Combine(value, "Working Stress Design"); //Chiranjit [2013 07 22]
                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                file_path = Path.Combine(file_path, "Design of Long Main Girders");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                //if (isInner)
                //    rep_file_name = Path.Combine(file_path, "T_Beam_Inner_Long_Girder.TXT");
                //else
                //    rep_file_name = Path.Combine(file_path, "T_Beam_Outer_Long_Girder.TXT");

                user_input_file = Path.Combine(system_path, "DESIGN_OF_LONGITUDINAL_GIRDERS_WITH_BOTTOM_FLANGE.FIL");
                drawing_path = Path.Combine(system_path, "LONG_GIRDERS_DRAWING.FIL");

               
            }
        }
        public string Inner_File
        {
            get
            {
                return Path.Combine(file_path, "T-Beam_Inner_Long_Girder.TXT");
            }
        }
        public string Outer_File
        {
            get
            {
                return Path.Combine(file_path, "T-Beam_Outer_Long_Girder.TXT");
            }
        }


        //Chiranjit [2013 06 28]
        public string Node_Displacement_Data { get; set; }



        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "LONG_GIRDERS_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));

            try
            {
                sw.WriteLine("_L1={0}", L);
                sw.WriteLine("_L2={0:f2}", (L / 2));
                sw.WriteLine("_L3={0:f2}", (L / 4));
                sw.WriteLine("_deff={0:f4}", (deff / 1000));
                sw.WriteLine("_D={0}", D / 1000);
                sw.WriteLine("_Ds={0}", Ds / 1000);
                sw.WriteLine("_Bw={0}", bw / 1000);

                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_bd3={0}", _bd3);
                sw.WriteLine("_bd4={0}", _bd4);
                sw.WriteLine("_bd5={0}", _bd5);
                sw.WriteLine("_bd6={0}", _bd6);
                sw.WriteLine("_bd7={0}", _bd7);
                sw.WriteLine("_bd8={0}", _bd8);
                sw.WriteLine("_bd9={0}", _bd9);
                sw.WriteLine("_bd10={0}", _bd10);

                sw.WriteLine("_sp7={0}", _sp7);
                sw.WriteLine("_sp8={0}", _sp8);
                sw.WriteLine("_sp9={0}", _sp9);
                sw.WriteLine("_sp10={0}", _sp10);


                sw.WriteLine("_n1={0}", _n1);
                sw.WriteLine("_n2={0}", _n2);
                sw.WriteLine("_n3={0}", _n3);
                sw.WriteLine("_n4={0}", _n4);
                sw.WriteLine("_n5={0}", _n5);
                sw.WriteLine("_n6={0}", _n6);
                sw.WriteLine("_n7={0}", _n7);
                sw.WriteLine("_n8={0}", _n8);
                sw.WriteLine("_n9={0}", _n9);
                sw.WriteLine("_n10={0}", _n10);


            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Calculate_Program(string file_name)
        {

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));

            #region TechSOFT Banner
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*       DESIGN OF LONGITUDINAL GIRDER         *");
            sw.WriteLine("\t\t*  FOR T-BEAM RCC BRIDGE WITH BOTTOM FLANGE   *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine();

            #endregion

            try
            {
                #region USER DATA
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Thickness of Deck Slab [Ds] = {0} mm                  Marked as (Ds) in the Drawing", Ds);
                sw.WriteLine("Depth of Longitudinal Girder [D]= {0} mm              Marked as (D) in the Drawing", D);
                sw.WriteLine("Web Thickness of Longitudinal Girder [bw] = {0} mm    Marked as (Bw) in the Drawing", bw);
                sw.WriteLine("Span of Girder [L] = {0} m                            Marked as (L) in the Drawing", L);
                sw.WriteLine("c/c distance of Longitudinal Girder [Bf] = {0} m", Bf);
                //sw.WriteLine("Spacing of Cross Girders = {0} m", txt_space_cross_girder.Text);




                sw.WriteLine();
                //sw.WriteLine("Width of Top Flange [Bf] = {0} mm", Bf);
                sw.WriteLine("Width of Bottom Flange [Bb] = {0} mm", Bb);
                sw.WriteLine("Depth of Bottom Flange [Db] = {0} mm", Db);
                sw.WriteLine("Depth of Web           [Dw] = D - Ds - Db ");
                sw.WriteLine("                            = {0} - {1} - {2}", D, Ds, Db);
                sw.WriteLine("                            = {0} mm", Dw);
                sw.WriteLine("                            = {0} m", (Dw / 1000.0));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At Mid Span (L/2) DESIGN_MOMENT [M1] = {0}  t-m", design_moment_mid);
                sw.WriteLine("At Quarter Span (L/4) DESIGN_MOMENT [M2] = {0}  t-m", design_moment_quarter);
                sw.WriteLine("At Effective Depth Distance (Deff) DESIGN_MOMENT [M3] = {0} t-m", design_moment_deff);

                sw.WriteLine();
                sw.WriteLine("At Mid Span (L/2) DESIGN_SHEAR [V1]= {0} t", v1);
                sw.WriteLine("At Quarter Span (L/4) DESIGN_SHEAR [V2] = {0} t", v2);
                sw.WriteLine("At Effective Depth Distance (Deff) DESIGN_SHEAR [V3] = {0} t", v3);


                sw.WriteLine();
                sw.WriteLine("CONCRETE GRADE = M {0} ", concrete_grade);
                sw.WriteLine("STEEL GRADE = Fe {0} ", steel_grade);
                sw.WriteLine();
                sw.WriteLine("MODULAR RATIO [m] = {0} ", modular_ratio);
                sw.WriteLine("BAR DIA [Do] = {0} mm", bar_dia);
                sw.WriteLine("TOTAL BARS [no] = {0} ", total_bars_L2);
                sw.WriteLine("TOTAL Layers of Bars [nl] = {0} ", nl);
                sw.WriteLine("Area [As] = {0:f0} sq.mm", As);
                sw.WriteLine();

                //sw.WriteLine("Diameter of Top reinforcement bar = side_Bar_dia = {0} mm", top_bar_dia);

                sw.WriteLine("COVER [cover] = {0} mm", cover);
                sw.WriteLine("Permissible Stress in Steel [σ_sv] = {0} N/sq.mm.", sigma_sv);
                sw.WriteLine("Permissible Stress in Concrete [σ_c] = {0} N/sq.mm. = {1} kg/sq.cm.", allow_stress_concrete, (allow_stress_concrete = allow_stress_concrete * 10.0));
                sw.WriteLine();
                //sw.WriteLine("Spacing of main Girders = {0} m", txt_space_main_girder.Text);
                sw.WriteLine();


                sw.WriteLine("Diameter of Side reinforcement bar = {0} mm.", top_bar_dia);
                sw.WriteLine("");
                //cm
                #endregion

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();



                _n1 = total_bars_L2;
                _bd1 = bar_dia;

                _n2 = total_bars_L2 - 2;
                _bd2 = bar_dia;

                _n3 = total_bars_L2 - 4;
                _bd3 = bar_dia;

                _n4 = 2;
                _bd4 = bar_dia;
                _n5 = 2;
                _bd5 = bar_dia;
                _n6 = 3;
                _bd6 = 20;
                //_bd6 = bar_dia * 10;
                bar_dia = bar_dia_L2;

                int step = 1;
                Gs = Bf;
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------");
                sw.WriteLine("STEP 1 : DESIGN OF GIRDER AT MID SPAN (L/2) FOR BENDING MOMENT");
                sw.WriteLine("--------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("DESIGN MOMENT = {0} t-m", design_moment_mid);

                Write_Moment_Program(sw, design_moment_mid, total_bars_L2, 1, 0, step++);
                Bf = Gs;
                if (design_moment_quarter != 0)
                {
                    sw.WriteLine("------------------------------------------------------------------");
                    sw.WriteLine("STEP 2 : DESIGN OF GIRDER AT QUARTER SPAN (L/4) FOR BENDING MOMENT");
                    sw.WriteLine("------------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("DESIGN MOMENT = {0:E3} t-m", design_moment_quarter);
                    sw.WriteLine();
                    
                    bar_dia = bar_dia_L4;
                    Write_Moment_Program(sw, design_moment_quarter, total_bars_L4, 2, 4, step++);
                }
                Bf = Gs;
                if (design_moment_deff != 0)
                {
                    sw.WriteLine("------------------------------------------------------------------------");
                    sw.WriteLine("STEP 3 : DESIGN OF GIRDER AT EFFECTIVE DEPTH DISTANCE FOR BENDING MOMENT");
                    sw.WriteLine("------------------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("DESIGN MOMENT = {0:E3} t-m", design_moment_deff);
                    sw.WriteLine();
                    bar_dia = bar_dia_Deff;
                    Write_Moment_Program(sw, design_moment_deff, total_bars_Deff, 3, 5, step++);
                }
                Bf = Gs;
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : DESIGN OF GIRDER AT MID SPAN (L/2) FOR SHEAR FORCE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("DESIGN SHEAR = {0:E3} t-m", v1);
                sw.WriteLine();
                Write_Shear_Program(sw, v1, ref _bd7, ref _sp7, ref _n7, 7);
                sw.WriteLine();

                if (v2 != 0)
                {
                    sw.WriteLine("----------------------------------------------------------------");
                    sw.WriteLine("STEP 5 : DESIGN OF GIRDER AT QUARTER SPAN (L/4) FOR SHEAR FORCE");
                    sw.WriteLine("----------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("DESIGN SHEAR = {0:E3} t-m", v2);
                    sw.WriteLine();
                    Write_Shear_Program(sw, v2, ref _bd8, ref _sp8, ref _n8, 8);
                }
                if (v3 != 0)
                {
                    sw.WriteLine();
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine("STEP 6 : DESIGN OF GIRDER AT EFFECTIVE DEPTH DISTANCE FOR SHEAR FORCE");
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("DESIGN SHEAR = {0:E3} t-m", v3);
                    sw.WriteLine();
                    Write_Shear_Program(sw, v3, ref _bd9, ref _sp9, ref _n9, 9);
                }

                #region Chiranjit [2013 06 28]

                sw.WriteLine("");
                sw.WriteLine("---------------------------------------------------");
                sw.WriteLine("STEP 7 : CHECK FOR LIVE LOAD DEFLECTION", step++);
                sw.WriteLine("---------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                sw.WriteLine("");
                sw.WriteLine("Taking Values from Analysis Report :");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                sw.WriteLine("");
                NodeResultData Max_Node_Displacement = NodeResultData.Parse(Node_Displacement_Data);
                sw.WriteLine("");
                sw.WriteLine(Max_Node_Displacement.ToString());
                sw.WriteLine("");
                sw.WriteLine("");
                //Chiranjit [2014 01 12]
                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                //    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                sw.WriteLine("");

                double val = L / 800.0;

                sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", L, val);
                sw.WriteLine("");
                if (Max_Node_Displacement.Max_Translation < val)
                    sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. < {1:f5} M.    OK.", Max_Node_Displacement.Max_Translation, val);
                else
                    sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                sw.WriteLine();



                #endregion Chiranjit [2013 06 28]




                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : DESIGN OF SIDE REINFORCEMENTS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Providing 0.1% of area of each side face of the Long Girders");
                sw.WriteLine("with spacing not more than 450 mm, ");

                double req_st = 0.001 * 1000 * D;
                sw.WriteLine();
                sw.WriteLine("Required Steel = 0.001 * b * D");
                sw.WriteLine("               = 0.001 * 1000 * {0:f2}", (D));
                sw.WriteLine("               = {0:f2} sq.mm", req_st);

                double ast = Math.PI * top_bar_dia * top_bar_dia / 4.0;

                double no_bar = (int)(req_st / ast);
                no_bar += 1;

                ast = ast * no_bar;

                sw.WriteLine("Provide {0} mm dia bars, {1:f0} nos, Ast = {2:f2} sq.mm.       Marked as (10) in the Drawing", top_bar_dia, no_bar, ast);
                sw.WriteLine();

                double spacing = (D - Ds - 300.0) / no_bar;
                sw.WriteLine("Spacing = (D - Ds - 300)/ {0:f0}", no_bar);
                sw.WriteLine("        = ({0:f2} - {1:f2} - 300) / {2:f0}", D, Ds, no_bar);
                sw.WriteLine("        = {0:f2} mm ", spacing);

                spacing = (int)(spacing / 10.0);
                spacing *= 10;
                sw.WriteLine("        = {0:f2} mm ", spacing);

                _bd10 = 16;
                _sp10 = spacing;
                _n10 = no_bar;


                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 9 : DESIGN OF TOP REINFORCEMENTS");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Required steel = 0.25% of girder Sectional Area.");

                //req_st = (0.25 / 100.0) * bw * deff * 10.0;
                //sw.WriteLine("=(0.25/100) * {0:f2} * {1:f2}", bw, deff * 10);
                //sw.WriteLine("={0:f2} sq.mm.", req_st);


                //ast = Math.PI * top_bar_dia * 20 / 4.0;

                //no_bar = (int)(req_st / ast);
                //no_bar += 1;

                //ast = ast * no_bar;

                //_bd7 = 10;
                //_n6 = no_bar;
                //sw.WriteLine();
                //sw.WriteLine("Let us provide {0:f0} T20 bars at Top.      Marked as (6) in the Drawing", no_bar);

                //sw.WriteLine();
                //sw.WriteLine("Provided Steel = {0:f2} sq.mm ", ast);
                //sw.WriteLine();


                #region corrections


                req_st = (0.25 / 100.0) * bw * deff;
                sw.WriteLine("               = (0.25/100) * {0:f2} * {1:f2}", bw, deff * 10);
                sw.WriteLine("               = {0:f2} sq.mm.", req_st);


                //sw.WriteLine("               = (0.25/100) * 300.00 * 1397.500");
                //sw.WriteLine("               = 1048.125 sq.mm.");
                sw.WriteLine("");


                ast = Math.PI * top_bar_dia * top_bar_dia / 4.0;
                sw.WriteLine("Area of each side reinforcement Bar = 3.1416 * {0} * {0}/4 = {1:f3} Sq.mm ", top_bar_dia, ast);

                no_bar = (int)(req_st / ast);
                no_bar += 1;

                sw.WriteLine("Number of Bars required = {0:f3} / {1:f3} = {2:f3} = {3} nos.", req_st, ast, (req_st / ast), no_bar);


                _bd7 = top_bar_dia;
                _n6 = no_bar;


                sw.WriteLine("");
                sw.WriteLine("Let us provide {0} nos. T{1} bars at Top.      Marked as (6) in the Drawing", no_bar, top_bar_dia);

                sw.WriteLine("");
                sw.WriteLine("Provided Steel = {0} * {1:f3} = {2:f3} sq.mm", no_bar, ast, ast * no_bar);

                ast = ast * no_bar;

                sw.WriteLine("");
                #endregion corrections




                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }

        public void Write_Moment_Program(StreamWriter sw, double moment_value, double bar_no, int mark, int bent_mark, int step)
        {
            double M = moment_value;

            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("--------------------------------------");
            sw.WriteLine("OBTAINED THE EFFECTIVE WIDTH OF FLANGE", step);
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            double thickness_slab = Ds;
            sw.WriteLine("Thickness of Deck Slab = {0} mm", thickness_slab);
            sw.WriteLine();
            sw.WriteLine("Effective width of top compression flage");
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                sw.WriteLine("(Referring to clause no 305.12.2 of IRC : 21-1987 ,  it should be the least of the following)");

            //sw.WriteLine();
            sw.WriteLine("");
            sw.WriteLine();

            double one_fourth_L;
            one_fourth_L = L / 4;
            sw.WriteLine("(i)   1/4 th of Effective span of the beam = L/4 = {0}/4 = {1:f3} m = {2} mm",
                L,
                one_fourth_L,
                one_fourth_L*1000);
            //if (!isInner)
            //{
            //    Gs = Gs / 2.0;
            //}
            sw.WriteLine("(ii)  The c/c distance of webs of beam = {0:f3} m = {1:f0} mm", Bf, Bf*1000);

            double bf;
            bf = 12 * Ds*0.001 + bw*0.001;

            sw.WriteLine("(iii) 12 * ds + bw = 12 * {0} + {1} = {2} m = {3:f0} mm.", Ds * 0.001, bw * 0.001, bf, (bf* 1000));
            sw.WriteLine();
            Bf = (bf < Bf && bf < one_fourth_L) ? bf : (Bf < bf && Bf < one_fourth_L) ? Bf : one_fourth_L;
            sw.WriteLine("Therefore, The Width of Top Flange = Bf = {0:f3} m = {1:f0} mm", Bf, (Bf*1000));

            double d1, d2;
            d1 = bar_dia;

            sw.WriteLine();

            //double mark, bent_mark;
            //mark = 2;
            //bent_mark = 4;

            sw.WriteLine("--------------------------------------------------------------");
            sw.WriteLine("STEP {0}.1 : MAIN REINFORCEMENTS (BOTTOM) AND EFFECTIVE DEPTH", step);
            sw.WriteLine("--------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Let us provide {0:f0} nos T{1:f0} bars             Marked as ({2}) in the Drawing", bar_no, d1, mark);
            if (bent_mark != 0)
                sw.WriteLine("                         Two Bars are bent up, Marked as ({0}) in the Drawing", bent_mark);



            double Ast = (Math.PI * bar_dia * bar_dia * bar_no) / 4;
            sw.WriteLine();
            Ast = double.Parse(Ast.ToString("0"));
            sw.WriteLine("Ast = {0} sq.mm", Ast);
            //Ast = Ast / 100;
            sw.WriteLine();
            sw.WriteLine("    = {0} sq.cm", Ast / 100);

            double eff_depth;
            //eff_depth = (D * 100) - (cover / 10) - (bar_dia / 2) - (nl * bar_dia);
            //Chiranjit [2011 06 06]
            eff_depth = (D) - (cover) - (bar_dia / 2) - (nl * bar_dia);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Effective Depth = D -  cover  - (bar_dia / 2) - (nl * bar_dia)");
            sw.WriteLine("                = {0:f2} - {1:f0} - {2:f1} - {3:f0} * {4:f1} ", D, (cover), (bar_dia / 2.0), nl, bar_dia, eff_depth);
            sw.WriteLine("                = {0:f2}  mm",eff_depth);
            deff = eff_depth;

            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP {0}.2 : DEPTH OF NEUTRAL AXIS", step);
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            #region Draw Figure
            sw.WriteLine("        |--------------Bf-------------|");
            sw.WriteLine("      _  _____________________________   _");
            sw.WriteLine("      | |                      |      |  |");
            sw.WriteLine("      | |______________________|______| _| Ds  _");
            sw.WriteLine("      |             |    |     |               |");
            sw.WriteLine("      |             |    |     |n              |");
            sw.WriteLine("      |  -----------|----|---------- NA        |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      D             |-Bw-|                     Dw");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |          ___|    |___  _              _|");
            sw.WriteLine("      |         |            |  |");
            sw.WriteLine("      |         |            |  | Db");
            sw.WriteLine("      |_        |____________| _|");
            sw.WriteLine("");
            sw.WriteLine("                |-----Bb-----|");
            sw.WriteLine();
            sw.WriteLine();
            #endregion Draw Figure

            sw.WriteLine("Let 'n' be the depth of Neutral Axis from the top of Deck slab");


            sw.WriteLine();
            double a1;
            Bf = Bf * 1000.0;
            a1 = (Ds * (Bf - bw));
            //d1 = 

            sw.WriteLine("a1 = Ds * (Bf - bw)");
            sw.WriteLine("   = {0} * ({1} - {2})", Ds, Bf, bw);
            sw.WriteLine("   = {0} sq.mm", a1.ToString("f3"));
            sw.WriteLine();
            double _d1 = Ds / 2;
            sw.WriteLine("d1 = n - Ds/2 = n - {0}/2 = n - {1}", Ds, _d1);



            double a2 = 1;
            //a2 = bw
            sw.WriteLine();
            sw.WriteLine("a2 = Bw * n = {0} * n", bw);
            sw.WriteLine("d2 = n/2");




            double a3 = 1;
            //a3 = (Bb * Db)/100.0;
            sw.WriteLine();
            //Chiranjit [2011 07 08]
            //For Checking
            //sw.WriteLine("a3 =  Bb * Db;");
            //sw.WriteLine("   = {0} * {1}", Bb/10.0, Db/10.0);
            //sw.WriteLine("   = {0} sq.cm", a3);
            sw.WriteLine();
            double _d3 = 1;
            //double _d3 = (D - (Db / 2)) / 10.0;
            //sw.WriteLine("d3 = D-n-Db/2 = {0} - n - {1}/2 = {2:f2} - n", D/10.0, Db/10.0, _d3);

            double a4 = 1;
            a4 = modular_ratio * (Ast);

            sw.WriteLine();
            sw.WriteLine("a4 = m * Ast");
            sw.WriteLine("   = {0} * {1}", modular_ratio, Ast);
            sw.WriteLine("   = {0} sq.mm", a4);
            sw.WriteLine();
            sw.WriteLine("d4 = deff - n = {0:f2} - n", eff_depth);



            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Now from equation");
            sw.WriteLine();
            //Chiranjit [2011 07 08]
            //sw.WriteLine("a1 * d1 + a2 * d2   =   a3 * d3 + a4 * d4");
            sw.WriteLine("a1 * d1 + a2 * d2   =   a4 * d4");
            sw.WriteLine();
            //Chiranjit [2011 07 08]
            //sw.WriteLine("{0:f3} * (n - {1:f3}) + ({2:f3} * n) * (n/2)   =  {3:f3} * ({4:f3} - n) + {5:f3} * ({6:f3} - n)",
            //    a1, _d1, bw / 10.0, a3, _d3, a4, eff_depth / 10.0);
            sw.WriteLine("{0} * (n - {1}) + ({2} * n) * (n/2)   =  {3} * ({4} - n)",
                a1, _d1, bw, a4, eff_depth);
            sw.WriteLine();
            sw.WriteLine();

            double _v1, _v2, _v3, _v4, _v5, _v6, _v7;

            _v1 = a1;
            _v2 = (Ds / 20.0) * a1;
            _v3 = (bw / 20.0);
            //Chiranjit [2011 07 08]
            //_v4 = a3 * _d3;
            //_v5 = a3;
            _v6 = a4 * eff_depth;
            _v7 = a4;

            //Chiranjit [2011 07 08]
            //sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n + {5} - {6} * n",
            //    _v1, _v2, _v3, _v4, _v5, _v6, _v7);
            sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n",
                _v1, _v2, _v3, _v6, _v7);
            sw.WriteLine();


            //Chiranjit [2011 07 08]
            //sw.WriteLine("{0} * n*n + {1}*n + {2}*n + {3}*n = {4} + {5} + {6}",
            //    _v3, _v1, _v5, _v7, _v2, _v4, _v6);
            sw.WriteLine("{0} * n*n + {1}*n + {2}*n = {3} + {4}",
                _v3, _v1, _v7, _v2, _v6);

            double _a, _b, _c;
            //Chiranjit [2011 07 08]
            //_b = ((a1 + a5) / a3);
            //_c = ((a1 * a2 + a4) / a3);

            _a = _v3;
            //Chiranjit [2011 07 08]
            //_b = (_v1 + _v5 + _v7);
            //_c = (_v2 + _v4 + _v6);

            _b = (_v1 + _v7);
            _c = (_v2 + _v6);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("    {0} * n*n + {1:f4}*n  = {2} ", _a, _b, _c);
            sw.WriteLine();
            _b = _b / _a;
            _c = (_c / _a);
            sw.WriteLine("or,   n*n + {0:f0}*n  - {1:f0} = 0", _b, _c);
            sw.WriteLine();

            sw.WriteLine();

            double n;

            double root_a;
            root_a = Math.Sqrt((_b * _b + 4 * _c));

            n = (root_a - _b) / 2;
            sw.WriteLine("n = {0:f4} mm = {1:f4} m = Depth of Neutral Axis from Top Edge", n, (n/1000.0));

            n /= 1000.00;
            Bf /= 1000.0;
            bw /= 1000.0;
            Ds /= 1000.0;
            D /= 1000.0;
            Db /= 1000.0;
            eff_depth /= 1000.0;
            Bb /= 1000.0;

            n = MyList.StringToDouble(n.ToString("f5"), n);
          

            //Chiranjit [2011 07 08]
            //double ina = ((Bf * n * n * n) / 3.0) -
            //    (((Bf - bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
            //    (Bb * (D - n) * (D - n) * (D - n) / 3.0) -
            //    (((Bb - bw) * (D - n - Db) * (D - n - Db) * (D - n - Db)) / 3.0) +
            //    (modular_ratio * Ast / 10E4 * (eff_depth - n) * (eff_depth - n));
            double ina = ((Bf * n * n * n) / 3.0) -
                         (((Bf - bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
                         (modular_ratio * Ast / 10E3 * (eff_depth - n) * (eff_depth - n));

            sw.WriteLine();
            sw.WriteLine(@"      ");
            sw.WriteLine(@"If(n > Ds) (Depth of Neutral Axis is more than Slab Thickness)");
            sw.WriteLine(@"       Ina = ((Bf * n^3) / 3.0) - (((Bf - bw) * (n - Ds)^3) / 3.0) +  ");
            sw.WriteLine(@"        (m * Ast / 10^4 * (eff_depth - n)^2)");
            sw.WriteLine(@"");
            sw.WriteLine(@"");
            sw.WriteLine(@"If(n < Ds) (Depth of Neutral Axis is less than Slab Thickness)");
            sw.WriteLine(@"       Ina = (Bf * n^3 / 3.0) ");
            sw.WriteLine(@"             + (Bf * (Ds - n)^3 / 3.0) ");
            sw.WriteLine(@"             + (bw * Dw * ((Dw / 2) + Ds - n)^2");
            sw.WriteLine(@"             + m * Ast / 10^4 * (eff_depth - n)^2)");
            sw.WriteLine(@"");
            //Comment this condition
            //Sandipan Sir [2012 09 06] Delhi
            //Chiranjit [2012 09 06] Kolkata
            if (n >= Ds)
            {
                sw.WriteLine(@"here, n >= Ds, ({0:f3} >= {1:f3})", n, Ds);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Moment of Inertia about Neutral Axis [Neutral Axis is lying outside the Top Flange of the Girder]");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("       Ina = ((Bf * n^3) / 3.0) - (((Bf - bw) * (n - Ds)^3) / 3.0) +  ");
                sw.WriteLine("             (m * Ast / 10^4 * (eff_depth - n)^2)");
                sw.WriteLine();
                
                ina = ((Bf * n * n * n) / 3.0) -
                         (((Bf - bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
                         (modular_ratio * Ast / 10E3 * (eff_depth - n) * (eff_depth - n));


                sw.WriteLine("           = (({0} * {1:f4}^3) / 3.0) - ((({0} - {2}) * ({1:f4} - {3})^3) / 3.0) +  ",
                    Bf, n, bw, Ds);
                sw.WriteLine("             ({0} * {1} / 10^4 * ({2} - {3:f4})^2)",
                    modular_ratio, Ast, eff_depth, n);
            }
            #region Chiranjit [2012 09 06] Kolkata
            else
            {

                sw.WriteLine(@"here, n < Ds, ({0:f3} < {1:f3})", n, Ds);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Moment of Inertia about Neutral Axis [Neutral Axis lies within the Top Flange of the Girder]");
                sw.WriteLine();
                //sw.WriteLine(@"If(n < Ds) (Depth of Neutral Axis is less than Slab Thickness)");
                sw.WriteLine(@"       Ina = (Bf * n^3 / 3.0) ");
                sw.WriteLine(@"             + (Bf * (Ds - n)^3 / 3.0) ");
                sw.WriteLine(@"             + (bw * Dw * ((Dw / 2) + Ds - n)^2");
                sw.WriteLine(@"             + m * Ast / 10^4 * (eff_depth - n)^2)");
                sw.WriteLine();


                Dw /= 1000.0;
                ina = (Bf * n * n * n / 3.0)
                      + (Bf * (Ds - n) * (Ds - n) * (Ds - n) / 3.0)
                      + (bw * Dw * ((Dw / 2) + Ds - n) * ((Dw / 2) + Ds - n))
                      + (modular_ratio * Ast / 10000.0 * (eff_depth - n) * (eff_depth - n));
                sw.WriteLine(@"           = ({0} * {1}^3 / 3.0) ", Bf, n);
                sw.WriteLine(@"             + ({0} * ({1} - {2})^3 / 3.0) ", Bf, Ds, n);
                sw.WriteLine(@"             + ({0} * {1} * (({1} / 2) + {2} - {3})^2", bw, Dw, Ds, n);
                sw.WriteLine(@"             + {0} * {1} / 10^4 * ({2} - {3})^2)", modular_ratio, Ast, eff_depth, n);


               
                Dw *= 1000.0;
            }
            #endregion Chiranjit [2012 09 06] Kolkata
            sw.WriteLine();




            sw.WriteLine();
            sw.WriteLine("           = {0:E3} sq.sq.m.", ina);


            double Zt;
            Zt = (ina / (n));
            sw.WriteLine();
            sw.WriteLine("Zt = Ina / n = {0:E3}/{1:F4} = {2:E3} cu.m", ina, n, Zt);


            double Zb;
            Zb = (ina / ((eff_depth - n)));
            sw.WriteLine("Zb = Ina / (deff - n) = {0:E3} / ({1} - {2:F4}) = {3:E3} cu.m", ina, eff_depth, n, Zb);


            n *= 100.0;
            Bf *= 1000.0;
            bw *= 1000.0;
            Bb *= 1000.0;
            Ds *= 1000.0;
            D *= 1000.0;
            Db *= 1000.0;
            eff_depth *= 1000.0;


            double fc;
            fc = (M * 10E4) / (Zt * 10E5);
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP {0}.3 : CHECK FOR STRESSES", step);
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Stress in concrete = fc = (M * 10^5)/(Zt * 10^6)");
            sw.WriteLine("                        = ({0:F3} * 10^5)/({1:E3} * 10^6)",
                (M), (Zt));


            sw.WriteLine();
            if (fc < allow_stress_concrete)
            {
                sw.WriteLine("                        = {0:f3} kg/sq.cm. < (σ_c) {1}  kg/sq.cm,   OK",
                    fc, allow_stress_concrete);
            }
            else
            {
                sw.WriteLine("                        = {0:f3} kg/sq.cm. > (σ_c) {1} kg/sq.cm,  NOT OK",
                    fc, allow_stress_concrete);

            }

            double fs;
            fs = (M * 10E4) / (Zb * 10E5);
            fs = fs * 10;
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Allowable Stress in Fe {0} grade of steel = {1} kg/sq.cm", steel_grade, sigma_sv * 10);
            sw.WriteLine();
            sw.WriteLine("Stress in Steel = fs = (M * 10^5)/(Zb * 10^6)");
            sw.WriteLine("                     = ({0:F3} * 10^5)/({1:E3} * 10^6)", (M), (Zb));

            sw.WriteLine();
            double chk_val = sigma_sv * 10;
            if (fs < chk_val)
            {
                sw.WriteLine("                     = {0} kg/sq.cm < (σ_sv) {1} kg/sq.cm,  OK", (fs > 1000000.0 ? fs.ToString("E3") : fs.ToString("F3")), chk_val);
            }
            else
            {
                sw.WriteLine("                     = {0} kg/sq.cm > (σ_sv) {1} kg/sq.cm,  NOT OK", (fs > 1000000.0 ? fs.ToString("E3") : fs.ToString("F3")), chk_val);
            }

            //double lever_arm = (M * 10E4) / (fs * Ast);
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("Therefore, Lever Arm = (M * 10^5)/(fs*Ast)");
            //sw.WriteLine("                     = ({0:F3} * 10^5)/({1:E4} * {2})",
            //    (M),
            //    fs,
            //    Ast);
            //sw.WriteLine("                     = {0} cm", lever_arm.ToString("0.000"));
            //lever_arm = lever_arm / 100;
            //sw.WriteLine("                     = {0} m", lever_arm.ToString("0.000"));

            sw.WriteLine();
            sw.WriteLine();
        }
        public void Write_Shear_Program(StreamWriter sw, double shear_value, ref double bd, ref double sp, ref double no, int mark)
        {
            double rad = Math.PI / 180.0;
            //sw.WriteLine();
            //sw.WriteLine("STEP 5");
            //sw.WriteLine("Design of Shear Reinforcement");
            //sw.WriteLine("-----------------------------");

            double shear = (shear_value * 10E3);

            sw.WriteLine();
            sw.WriteLine("Design Shear = {0:E3} t", shear_value);
            sw.WriteLine("             = {0:E3} N", shear);

            //deff = (D * 100.0) - (cover / 10) - (bar_dia / 2) - (3 * bar_dia);
            //Chiranjit [2011 06 06]
            //deff = (D / 10) - (cover / 10) - (bar_dia / 2) - (3 * bar_dia);
            //deff = ;
            double tau_v = (shear) / (bw * deff * 10);

            sw.WriteLine();
            sw.WriteLine("Nominal Shear Stress τ_v = V/(b * deff)");
            sw.WriteLine("                         = {0:E3}/({1:f3} * {2:f3})",
                shear, bw, deff * 10);
            sw.WriteLine("                         = {0:E3} N/sq.mm", tau_v);
            sw.WriteLine();

            double ck_val = 0.07 * concrete_grade;
            if (ck_val > tau_v)
            {
                sw.WriteLine("0.07 * {0} = {1:f3} N/sq.mm > {2:f3} N/sq.mm , OK",
                    concrete_grade, ck_val, tau_v);
            }
            else
            {
                sw.WriteLine("0.07 * {0} = {1:f3} N/sq.mm < {2:f3} N/sq.mm , NOT OK",
                    concrete_grade, ck_val, tau_v);
                sw.WriteLine("Increase size of Girder, and redesign.");
            }

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Assuming 2 bars {0:f0} mm dia are bent up at the section,", (bar_dia * 10));
            sw.WriteLine("Shear force resisted by bent up bars is given by, ");

            double Asv = Math.PI * (bar_dia * 10.0) * (bar_dia * 10.0) / 4;

            double Vs = sigma_sv * 2 * Asv * Math.Sin(45 * rad) / 10000.0;

            sw.WriteLine("      Vs = σ_sv * Asv * Sin 45°");
            sw.WriteLine("         = ({0:E2} * 2 * {1:E2} * 1)/(10000.0 * √2)", sigma_sv, Asv);
            sw.WriteLine("         = {0:E2} t", Vs);
            sw.WriteLine();
            //Chiranjit [2014 01 12]
            //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            //    sw.WriteLine("Refer to Cl. A4.6.1  IRC 112:2011");

            sw.WriteLine();
            


            double balance_shear = shear - Vs;
            sw.WriteLine("Balance Shear = {0:E3} - {1:E3} = {2:E3} N", shear, Vs, balance_shear);
            sw.WriteLine();
            sw.WriteLine("Using 10 mm diameter, 4 legged Stirrups, ");
            sw.WriteLine();

            Asv = Math.PI * 10.0 * 10.0 / 4.0;
            //double Sv = (sigma_sv * 4 * Asv * deff * 10) / (balance_shear * 10E3);
            double Sv = (sigma_sv * 4 * Asv * deff) / (balance_shear);
            sw.WriteLine("Spacing = Sv = (σ_sv * Asv * deff)/V");
            sw.WriteLine("             = ({0:f3} * 4 * {1:f3} * {2:f3})/({3:E2})",
                sigma_sv, Asv, deff, balance_shear);
            sw.WriteLine("             = {0:f3} mm", Sv);
            sw.WriteLine();

            if (Sv > 50 && Sv < 200)
            {
                Sv = (int)Sv / 10;
                Sv = Sv * 10;
            }
            else if (Sv > 200)
            {
                Sv = 200;
            }
            else
            {
                Sv = 0;
            }


            if (Sv == 0)
            {
                sw.WriteLine("Increase size of section and redesign.");
            }
            else
            {
                sw.WriteLine("Provide 10 diameter, 4 legged stirrups at {0:f0} mm. Marked as ({1}) in the Drawing", Sv, mark);
                sw.WriteLine("centre to centre distance.");
            }

            bd = 10;
            sp = Sv;
            no = 4;

        }
        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion

            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    indx = lst_content[i].LastIndexOf(" ");
                    if (indx > 0)
                        kStr = MyList.RemoveAllSpaces(lst_content[i].Substring(0, indx));
                    else
                        kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "D_S":
                            Ds = mList.GetDouble(1);
                            break;
                        case "D":
                            D = mList.GetDouble(1);
                            break;
                        case "Bb":
                            Bb = mList.GetDouble(1);

                            break;
                        //case "Bf":
                        //    // Bf  = mList.GetDouble(1);
                        //    break;
                        case "Db":
                            Db = mList.GetDouble(1);
                            break;
                        case "nl":
                            nl = mList.GetDouble(1);
                            break;
                        case "BW":
                            bw = mList.GetDouble(1);
                            break;
                        case "L":
                            L = mList.GetDouble(1);
                            break;
                        case "GS":
                            Bf = mList.GetDouble(1);
                            break;
                        case "DESIGN_MOMENT_MID":
                            design_moment_mid = mList.GetDouble(1);
                            break;



                        case "DESIGN_MOMENT_QUARTER":
                            design_moment_quarter = mList.GetDouble(1);
                            break;


                        case "DESIGN_MOMENT_DEFF":
                            design_moment_deff = mList.GetDouble(1);
                            break;

                        case "V1":
                            v1 = mList.GetDouble(1);
                            break;

                        case "V2":
                            v2 = mList.GetDouble(1);
                            break;


                        case "V3":
                            v3 = mList.GetDouble(1);
                            break;

                        case "CONCRETE_GRADE":
                            concrete_grade = mList.GetDouble(1);
                            break;
                        case "ALLOW_CONC_STRESS":
                            allow_stress_concrete = mList.GetDouble(1);
                            break;
                        case "STEEL_GRADE":
                            steel_grade = mList.GetDouble(1);
                            break;
                        case "MODULAR_RATIO":
                            modular_ratio = mList.GetDouble(1);
                            break;
                        case "BAR_DIA":
                            bar_dia = mList.GetDouble(1);
                            break;
                        case "TOTAL_BARS":
                            total_bars_L2 = mList.GetDouble(1);
                            break;
                        case "COVER":
                            cover = mList.GetDouble(1);
                            break;
                        case "SIGMA_SV":
                            sigma_sv = mList.GetDouble(1);
                            break;
                        case "SPACE_MAIN":
                            space_main = mList.GetDouble(1);
                            break;
                        case "SPACE_CROSS":
                            space_cross = mList.GetDouble(1);
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
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region USER DATA
                sw.WriteLine();
                sw.WriteLine("USER DATA");
                sw.WriteLine("---------");
                sw.WriteLine("D_S ={0} m", Ds);
                sw.WriteLine("D ={0} m", D);
                sw.WriteLine("Bb ={0} m", Bb);
                sw.WriteLine("Bf ={0} m", Bf);
                sw.WriteLine("Db ={0} m", Db);
                sw.WriteLine("Dw ={0} m", Dw);
                sw.WriteLine("nl ={0} m", nl);



                sw.WriteLine("BW ={0} cm", bw);
                sw.WriteLine("L ={0} m", L);
                sw.WriteLine("GS ={0} cm", Bf);

                sw.WriteLine("DESIGN_MOMENT_MID ={0} ", design_moment_mid);
                sw.WriteLine("DESIGN_MOMENT_QUARTER ={0} ", design_moment_quarter);
                sw.WriteLine("DESIGN_MOMENT_DEFF ={0} ", design_moment_deff);

                sw.WriteLine("V1 ={0} ", v1);
                sw.WriteLine("V2 ={0} ", v2);
                sw.WriteLine("V3 ={0} ", v3);

                sw.WriteLine("CONCRETE_GRADE ={0} ", concrete_grade);
                sw.WriteLine("ALLOW_CONC_STRESS ={0} ", allow_stress_concrete);
                sw.WriteLine("STEEL_GRADE ={0} ", steel_grade);
                sw.WriteLine("MODULAR_RATIO ={0} ", modular_ratio);
                sw.WriteLine("BAR_DIA ={0} cm", bar_dia);
                sw.WriteLine("TOTAL_BARS ={0} ", total_bars_L2);
                sw.WriteLine("COVER ={0} ", cover);
                sw.WriteLine("SIGMA_SV ={0} ", sigma_sv);
                sw.WriteLine("SPACE_MAIN ={0} ", space_main);
                sw.WriteLine("SPACE_CROSS ={0} ", space_cross);
                sw.WriteLine();

                ////cm
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }


    public class CrossGirders
    {
        public string rep_file_name = "";
        public string file_path = "";
        public string user_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_drawing_file = "";
        public bool is_process = false;

        #region Variable Declaration
        //double hogging_moment;
        //double max_hogging_moment;
        public double M_total_hogging_moment;
        public double L_spacing_longitudinal_girder;
        public double number_longitudinal_girder;
        public double spacing_cross_girder;
        public double number_cross_girder;
        public double D_depth_cross_girder;
        public double b_web_thickness_cross_girder;
        //double shear_DL_SIDL;
        //double max_shear;
        public double W_total_shear;
        public double grade_concrete;
        public double grade_steel;
        public double clear_cover;
        public double fc_stress_concrete;
        public double fs_stress_steel;
        public double m_modular_ratio;


        public double top_bar_dia;
        public double bottom_bar_dia;
        public double side_bar_dia;
        public double vertical_bar_dia;




        public double L_by_D;
        public double Z_lever_arm;
        public double Ast1;
        public double Ast2;
        public double located_within_depth;
        public double bar_dia;
        public double development_length;
        public double required_steel_hanging_reinf;
        public double provided_steel_per_meter_length;
        public double required_steel;

        #endregion

        #region Drawing Variable
        string _1, _2, _3, _4, _5, _6, _A, _B, _C;
        #endregion

        IApplication iApp = null;

        public CrossGirders(IApplication app)
        {
            this.iApp = app;
        }



        public void CalculateProgram()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));

            #region TechSOFT Banner
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*          DESIGN  OF  CROSS  GIRDER          *");
            sw.WriteLine("\t\t*            FOR T-BEAM RCC BRIDGE            *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            try
            {
                #region USER INPUT DATA
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("Collecting Bending Moment Values from ASTRA Pro Moving Load Analysis");
                sw.WriteLine();
                //sw.WriteLine("Hogging Moment ( DL + SIDL ) = {0} t-m", hogging_moment);
                //sw.WriteLine("Maximum Hogging Moment due to Moving Load = {0} t-m", max_hogging_moment);
                M_total_hogging_moment = Math.Abs(M_total_hogging_moment);

                sw.WriteLine("Design Max. Bending Moment [M] = {0} t-m", M_total_hogging_moment);

                sw.WriteLine("Spacing of Longitudinal Girders [L] = {0} m    Marked as (A) in the Drawing", L_spacing_longitudinal_girder);
                _A = "(A) " + (L_spacing_longitudinal_girder * 1000) + " mm";


                sw.WriteLine("Number of Longitudinal Girders = {0:f0} ", number_longitudinal_girder);
                sw.WriteLine("Spacing of Cross Girders = {0} m", spacing_cross_girder);
                sw.WriteLine("Number of Cross Girders = {0:f0} ", number_cross_girder);
                sw.WriteLine("Depth of Cross Girders [D] = {0} m    Marked as (B) in the Drawing", D_depth_cross_girder);
                _B = "(B) " + D_depth_cross_girder * 1000 + " mm";



                sw.WriteLine("Web thickness of Cross Girders [b] = {0} m    Marked as (C) in the Drawing", b_web_thickness_cross_girder);
                _C = "(C) " + b_web_thickness_cross_girder * 1000 + " mm";


                //sw.WriteLine("Shear ( DL + SIDL ) = {0} t", shear_DL_SIDL);
                //sw.WriteLine("Maximum Shear due to Moving Load = {0} t", max_shear);
                sw.WriteLine("Total Shear = {0} t", W_total_shear);

                sw.WriteLine("Grade of Concrete = M {0:f0} ", grade_concrete);
                sw.WriteLine("Grade of Steel = Fe {0:f0} ", grade_steel);
                sw.WriteLine("Clear Cover = {0} m", clear_cover);
                sw.WriteLine("Allowable Flexural Stress in Concrete [fc] = {0} N/sq.mm.", fc_stress_concrete);
                sw.WriteLine("Stress in Steel [fs] = {0} N/sq.mm.", fs_stress_steel);
                sw.WriteLine("Modular Ratio [m] = {0}", m_modular_ratio);

                sw.WriteLine("");
                sw.WriteLine("Top Reinforcement Bar Dia = Top_Bar_Dia = {0} mm.", top_bar_dia);
                sw.WriteLine("Bottom Reinforcement Bar Dia = Bottom_Bar_Dia = 25 mm.", bottom_bar_dia);
                sw.WriteLine("Side Reinforcement Bar Dia = Side_Bar_Dia = 16 mm.", side_bar_dia);
                sw.WriteLine("Vertical Stirrup Reinforcement Bar Dia = Vs_Bar_Dia = 16 mm.", vertical_bar_dia);
                sw.WriteLine("");

                #endregion

                #region DESIGN

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF CROSS GIRDER");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double eff_d = (D_depth_cross_girder * 1000) - 50 - 11 - 22;
                sw.WriteLine("Effective Depth = {0} - 50 - 11 - 22 = {1:f2} mm", (D_depth_cross_girder * 1000), eff_d);
                //Step 1
                L_by_D = L_spacing_longitudinal_girder / D_depth_cross_girder;
                #endregion

                #region STEP 1: LEVER ARM

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1: LEVER ARM");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    sw.WriteLine("By Clause 28.2, IS 456-2000");

                if (L_by_D >= 1)
                {
                    sw.WriteLine(" L/D = {0}/{1} = {2} >= 1 , OK for continuous Beam.",
                        L_spacing_longitudinal_girder.ToString("0.000"),
                        D_depth_cross_girder.ToString("0.000"),
                        L_by_D.ToString("0.000"));
                }
                else
                {
                    sw.WriteLine("Now, L/D = {0}/{1} = {2} < 1 , NOT OK for continuous Beam.",
                        L_spacing_longitudinal_girder.ToString("0.000"),
                        D_depth_cross_girder.ToString("0.000"),
                        L_by_D.ToString("0.000"));

                }
                Z_lever_arm = 0.2 * (L_spacing_longitudinal_girder + (1.5 * D_depth_cross_girder));
                sw.WriteLine();
                sw.WriteLine("Lever Arm = Z = 0.2 * (L + 1.5 * D) = {0}", Z_lever_arm.ToString("0.0000"));


                #endregion

                #region Step 2

                //Step 2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : TOP STEEL BARS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //sw.WriteLine("Total Moment from ASTRA Moving Load analysis = {0:f2} t-m", M_total_hogging_moment);
                Ast1 = (M_total_hogging_moment * 10E3) / (Z_lever_arm * fs_stress_steel * m_modular_ratio);

                //sw.WriteLine();
                //sw.WriteLine("Required steel at Top = Ast1 = (M * 10E3)/(Z * fs)");
                //sw.WriteLine("                      = ({0} * 10E3)/({1} * {2})",
                //    M_total_hogging_moment,
                //    Z_lever_arm,
                //    fs_stress_steel);

                //sw.WriteLine("                      = {0:f0} sq.cm.", Ast1);

                double d1 = 2.5;
                double d1_ast = (Math.PI * d1 * d1 / 4);

                int no_d1_ast = (int)(Ast1 / d1_ast);

                double d1_ast_2 = no_d1_ast * d1_ast;

                double d2 = 2.0;
                double d2_ast = (Math.PI * d2 * d2 / 4);

                double diff_ast;
                int no_d2_ast = 0;

                //sw.WriteLine();
                //if (Ast1 > d1_ast_2)
                //{
                //    no_d1_ast -= 1;
                //    d1_ast_2 = no_d1_ast * d1_ast;
                //    diff_ast = Ast1 - d1_ast_2;
                //    no_d2_ast = (int)(diff_ast / d2_ast);
                //    no_d2_ast++;

                //    sw.WriteLine("Provide {0}-T{1} + {2}-T{3}    Marked as (2) in the Drawing",
                //        no_d1_ast,
                //        (d1 * 10),
                //        no_d2_ast,
                //        (d2 * 10));

                //    //(2)  5 Nos. T25 + 2 Nos. T20
                //    _2 = no_d1_ast + " Nos. T" + (d1 * 10) + " + " + no_d2_ast + " Nos. T" + (d2 * 10);



                //    sw.WriteLine();
                //    sw.WriteLine("Provided Ast1 = {0} sq.cm",
                //        (d1_ast_2 + (no_d2_ast * d2_ast)).ToString("0.00"));


                //}
                //else
                //{
                //    sw.WriteLine("Provide {0}-T{1}    Marked as (2) in the Drawing",
                //    no_d1_ast,
                //    (d1 * 10).ToString("0"));

                //    _2 = no_d1_ast + " Nos. T" + (d1 * 10);


                //    sw.WriteLine();
                //    sw.WriteLine("Provided Ast1 = {0} sq.cm",
                //                           d1_ast_2.ToString("0.00"));

                //}

                ////Step 3
                //Ast2 = 0.002 * b_web_thickness_cross_girder * 100 * D_depth_cross_girder * 100;


                 

                #region Corrections

                Ast2 = 0.0025 * b_web_thickness_cross_girder * 100 * D_depth_cross_girder * 100;

                sw.WriteLine(@"Required Steel at top  = Ast2");
                sw.WriteLine(@"                       = 0.25 % (b*100 * D*100)");
                sw.WriteLine(@"                       = 0.25 % ({0}*100 * {1}*100)", b_web_thickness_cross_girder, D_depth_cross_girder);
                sw.WriteLine(@"                       = {0:f3} sq.cm", Ast2);
                sw.WriteLine();
                Ast2 = Ast2 * 100;
                sw.WriteLine(@"                       = {0} sq.mm", Ast2);
                sw.WriteLine();
                sw.WriteLine(@"Diameter of Top Reinforcement bar = {0} mm.", top_bar_dia);

                double ar = Math.PI * top_bar_dia * top_bar_dia / 4.0;
                sw.WriteLine(@"Area of each top bar = 3.1416 * {0} * {0} /4 = {1:f3} sq.mm ", top_bar_dia, ar);



                double ns = Ast2 / ar;

                ns = (int)(Ast2 / ar);
                ns = ns + 1;


                sw.WriteLine(@"Nos. of top bar = {0} / {1:f3} = {2:f3} = {3} nos.", Ast2, ar, (Ast2 / ar), ns);



                _2 = ns + " Nos. T" + (top_bar_dia);


                //sw.WriteLine(@"Provide 4-T16    Marked as (2) in the Drawing, ");
                sw.WriteLine(@"Provide {0}-T{1}    Marked as (2) in the Drawing, ",ns, top_bar_dia);

                Ast2 = ns * (Math.PI * top_bar_dia * top_bar_dia / 4.0);
                sw.WriteLine(@"");
                //sw.WriteLine(@"Provided Ast2 = 4 * 3.1416 * 16 * 16 / 4 = 804.25 Sq. mm.");
                sw.WriteLine(@"Provided Ast2 = {0} * 3.1416 * {1} * {1} / 4 = {2:f3} Sq. mm.", ns, top_bar_dia, Ast2);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                #endregion Corrections






                #endregion

                #region Step 3
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : BOTTOM STEEL BAR");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("Required Steel at bottom (minimum) = Ast2");
                //sw.WriteLine("                                   = 0.2 % (b*100 * D*100)");
                //sw.WriteLine("                                   = 0.2 % ({0}*100 * {1}*100)",
                //    b_web_thickness_cross_girder,
                //    D_depth_cross_girder);
                //sw.WriteLine("                                   = {0} sq.cm.", Ast2.ToString("0.000"));

                //d1 = 1.6;
                //d1_ast = Math.PI * d1 * d1 / 4;

                //no_d1_ast = (int)(Ast2 / d1_ast);
                //no_d1_ast++;
                //d1_ast_2 = no_d1_ast * d1_ast;

                //sw.WriteLine();
                //sw.WriteLine("Provide {0}-T{1:f0}    Marked as (1) in the Drawing", no_d1_ast, (d1 * 10));

                ////(1)  (2 + 3) Nos. 22 Ø


                //_1 = no_d1_ast + " Nos. 22 Ø";
                ////(6)  5 Nos. 22 Ø
                //_6 = no_d1_ast + " Nos. 22 Ø";

                //sw.WriteLine();
                //sw.WriteLine("Provided Ast2 = {0} sq.cm.", d1_ast_2.ToString("0.000"));



                #region [2016 07 01] Corrections

                double _D = D_depth_cross_girder * 1000;
                sw.WriteLine("");
                sw.WriteLine("Total Bending Moment = {0:f3} t-m = {0:f3} x 10^7 N-mm", M_total_hogging_moment);
                sw.WriteLine("");
                sw.WriteLine("D = {0} m. = {1} mm.", D_depth_cross_girder, _D);
                sw.WriteLine("");


                Ast1 = (M_total_hogging_moment * 1E7) / (Z_lever_arm * fs_stress_steel * _D);

                sw.WriteLine("Required steel at Bottom = Ast1  = (M * 10^7)/(Z * fs * D)");
                sw.WriteLine("                                 = ({0:f3} * 10E7)/({1:f3} * {2} * {3})", M_total_hogging_moment, Z_lever_arm, fs_stress_steel, _D);
                sw.WriteLine("                                 = {0:f3} Sq.mm.", Ast1);

                Ast1 = Ast1 / 100;
                sw.WriteLine("                                 = {0:f3} sq.cm.", Ast1);
                sw.WriteLine("");


                sw.WriteLine("Diameter of Bottom Reinforcement bar = {0} mm.", bottom_bar_dia);

                ar = Math.PI * bottom_bar_dia * bottom_bar_dia / 4.0;

                sw.WriteLine("Area of each Bottom Bar  = 3.1416 * {0} * {0} / 4 = {1:f3} Sq.mm. = {2:f3} Sq.cm.", bottom_bar_dia, ar, (ar/100));

                ns = (int)(Ast1 / (ar/100));
                sw.WriteLine("");
                ns = ns + 1;
                sw.WriteLine("No. of Bottom Bars required  = {0:f3} / {1:f3} = {2:f3} nos. = {3} nos.", Ast1, ar / 100, (Ast1 / (ar / 100)), ns);
                sw.WriteLine("");
                //sw.WriteLine("Steel provided = 4 nos. 25 mm. dia Marked as (1) in the Drawing");
                sw.WriteLine("Steel provided = {0} nos. {1} mm. dia Marked as (1) in the Drawing", ns, bottom_bar_dia);


                _1 = ns + " Nos. " + bottom_bar_dia + " Ø";
                _6 = ns + " Nos. " + bottom_bar_dia + " Ø";



                Ast1 = ns * ar;
                //sw.WriteLine("Steel provided = 4 * 490.875 = 1964 Sq.mm. = 19.64 Sq.cm.");
                sw.WriteLine("Steel provided = {0} * {1:f2} = {2:f2} Sq.mm. = {3:f2} Sq.mm.", ns, ar, Ast1, Ast1/100);
                sw.WriteLine("");
                sw.WriteLine("");
                #endregion Corrections





                located_within_depth = 0.25 * D_depth_cross_girder - 0.05 * L_spacing_longitudinal_girder;

                sw.WriteLine();
                sw.WriteLine("Located within a depth of  (0.25 * D - 0.05 * L)");
                sw.WriteLine("                         = (0.25 * {0} - 0.05 * {1})",
                    D_depth_cross_girder,
                    L_spacing_longitudinal_girder);

                sw.WriteLine("                         = {0} m. from bottom face.",
                    located_within_depth.ToString("0.000"));



                bar_dia = bottom_bar_dia;
                development_length = 0.8 * 35 * bar_dia;

                sw.WriteLine();
                sw.WriteLine("Development Length = 0.8 * 35 * Bar Diameter");
                sw.WriteLine("                   = 0.8 * 35 * {0} = {1} mm", bar_dia, development_length);
                //Step 4
                sw.WriteLine();





                #endregion

                #region Step 4
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : PROVIDED STEEL");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    sw.WriteLine("Hogging Reinforcements,By Clause 28.33 IS 456 - 2000");


                sw.WriteLine("Total Shear from ASTRA moving Load analysis = {0} t", W_total_shear);
                required_steel_hanging_reinf = W_total_shear * 10000 / 20000; // TO BE CHECKED
                sw.WriteLine();
                sw.WriteLine("Required Steel Hanging Reinforcement = {0} * 10000/20000",
                    W_total_shear);
                sw.WriteLine("                                     = {0} sq.cm.",
                    required_steel_hanging_reinf);

                provided_steel_per_meter_length = required_steel_hanging_reinf / (L_spacing_longitudinal_girder / 2);
                sw.WriteLine();
                sw.WriteLine("Required Steel per meter length = {0} / (L/2) = {0} / {1} = {2} sq.cm/m.",
                    required_steel_hanging_reinf,
                    (L_spacing_longitudinal_girder / 2).ToString("0.000"),
                    provided_steel_per_meter_length.ToString("0.00"));


                d1 = vertical_bar_dia/10;
                d1_ast = Math.PI * d1 * d1 / 4;

                double spacing = 220;
                do
                {

                    spacing -= 20;
                    no_d1_ast = (int)(1000.0 / spacing);
                    d1_ast_2 = no_d1_ast * d1_ast * 2;

                    if (no_d1_ast == 0) break;
                }
                while (d1_ast_2 < required_steel_hanging_reinf);

                sw.WriteLine();
                sw.WriteLine("Provided 2-Legged T{0} {1} mm c/c stirrups as vertical Reinforcement  Marked as (3) in the Drawing", vertical_bar_dia,spacing);

                //(3)  2-Legged T12 200 mm c/c stirrups
                _3 = "2-Legged T" + vertical_bar_dia + " " + spacing + " mm c/c stirrups";

                sw.WriteLine();
                sw.WriteLine("Provided steel = {0} sq.cm/m", d1_ast_2.ToString("0.000"));
                //Step 5
                sw.WriteLine();

                #endregion

                #region Step 5
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : PROVIDED STEEL OF EACH SIDE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Side Face Reinforcements,By Clause 31.4  IS 456 2000");
                sw.WriteLine("0.1% of Web area on each face with spacing not more than 450 mm");

                required_steel = 0.001 * D_depth_cross_girder * 100 * b_web_thickness_cross_girder * 100;

                sw.WriteLine("Required steel = 0.001 * D * 100 * b * 100");
                sw.WriteLine("               = 0.001 * {0} * 100 * {1} * 100",
                    D_depth_cross_girder,
                    b_web_thickness_cross_girder);
                sw.WriteLine("               = {0} sq.cm.", required_steel.ToString("0.00"));


                d1 = side_bar_dia/10;
                d1_ast = Math.PI * d1 * d1 / 4;
                spacing = 320;
                do
                {
                    spacing -= 20;
                    no_d1_ast = (int)(1000.0 / spacing);
                    d1_ast_2 = no_d1_ast * d1_ast;
                }
                while (d1_ast_2 < required_steel);

                sw.WriteLine();
                sw.WriteLine("Diameter of Side Reinforcement bar = {0} mm.", side_bar_dia);
                sw.WriteLine();

                
                sw.WriteLine("Provide 3-T{0} bars @ {1} mm c/c     Marked as (4) in the Drawing",side_bar_dia, spacing);

                //(4)  6 Nos. 6 Ø Side face reinforcements
                //(5)  3-T16 bars @ 300 mm c/c 
                _4 = "6 Nos. 6 Ø Side face reinforcements";
                _5 = "3-T" + side_bar_dia + " bars @ " + spacing + "mm c/c";

                sw.WriteLine("Provided steel each side = {0:f2} sq.cm", d1_ast_2);
                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
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
            kPath = Path.Combine(kPath, "RCC T-Beam Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Cross Girders");

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
                //this.Text = "DESIGN OF CROSS GIRDERS : " + value;
                user_path = value;

                file_path = user_path;
                //file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "Working Stress Design"); //Chiranjit [2013 07 22]
                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                file_path = Path.Combine(file_path, "Design of Cross Girders");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_T_Beam_Cross_Girder.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_CROSS_GIRDERS.FIL");
                user_drawing_file = Path.Combine(system_path, "CROSS_GIRDERS_DRAWING.FIL");

                //btn_crProcess.Enabled = Directory.Exists(value);
                //btnReport.Enabled = File.Exists(rep_file_name);
                //btnDrawing.Enabled = File.Exists(rep_file_name);

                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Read_User_Input();
                //}
            }
        }

        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    //indx = lst_content[i].LastIndexOf(" ");
                    //if (indx > 0)
                    //    kStr = MyList.RemoveAllSpaces(lst_content[i].Substring(0, indx));
                    //else
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        //case "HOGGING_MOMENT":
                        //    hogging_moment = mList.GetDouble(1);
                        //    txt_hogging_moment.Text = hogging_moment.ToString();
                        //    break;
                        //case "MAX_HOGGING_MOMENT":
                        //    max_hogging_moment = mList.GetDouble(1);
                        //    txt_max_hogging_moment.Text = max_hogging_moment.ToString();
                        //    break;
                        case "TOTAL_HOGGING_MOMENT":
                            M_total_hogging_moment = mList.GetDouble(1);
                            //txt_total_hogging_moment.Text = M_total_hogging_moment.ToString();
                            break;
                        case "SPACING_LONGITUDINAL_GIRDER":
                            L_spacing_longitudinal_girder = mList.GetDouble(1);
                            //txt_spacing_longitudinal_girder.Text = L_spacing_longitudinal_girder.ToString();
                            break;
                        case "NUMBER_LONGITUDINAL_GIRDER":
                            number_longitudinal_girder = mList.GetDouble(1);
                            //txt_number_longitudinal_girder.Text = number_longitudinal_girder.ToString();
                            break;
                        case "SPACING_CROSS_GIRDER":
                            spacing_cross_girder = mList.GetDouble(1);
                            //txt_spacing_cross_girders.Text = spacing_cross_girder.ToString();
                            break;
                        case "NUMBER_CROSS_GIRDER":
                            number_cross_girder = mList.GetDouble(1);
                            //txt_number_cross_girder.Text = number_cross_girder.ToString();
                            break;
                        case "DEPTH_CROSS_GIRDER":
                            D_depth_cross_girder = mList.GetDouble(1);
                            //txt_depth_cross_girder.Text = D_depth_cross_girder.ToString();
                            break;
                        case "WEB_THICKNESS_CROSS_GIRDER":
                            b_web_thickness_cross_girder = mList.GetDouble(1);
                            //txt_web_thickness_cross_girder.Text = b_web_thickness_cross_girder.ToString();
                            break;
                        //case "SHEAR_DL_SIDL":
                        //    shear_DL_SIDL = mList.GetDouble(1);
                        //    txt_shear.Text = shear_DL_SIDL.ToString();
                        //    break;
                        //case "MAX_SHEAR":
                        //    max_shear = mList.GetDouble(1);
                        //    txt_maximum_shear.Text = max_shear.ToString();
                        //    break;
                        case "TOTAL_SHEAR":
                            W_total_shear = mList.GetDouble(1);
                            //txt_total_shear.Text = W_total_shear.ToString();
                            break;
                        case "GRADE_CONCRETE":
                            grade_concrete = mList.GetDouble(1);
                            //txt_grade_concrete.Text = grade_concrete.ToString();
                            break;
                        case "GRADE_STEEL":
                            grade_steel = mList.GetDouble(1);
                            //txt_grade_steel.Text = grade_concrete.ToString();
                            break;
                        case "CLEAR_COVER":
                            clear_cover = mList.GetDouble(1);
                            //txt_clear_cover.Text = clear_cover.ToString();
                            break;
                        case "STRESS_CONCRETE":
                            fc_stress_concrete = mList.GetDouble(1);
                            //txt_stress_concrete.Text = fc_stress_concrete.ToString();
                            break;
                        case "STRESS_STEEL":
                            fs_stress_steel = mList.GetDouble(1);
                            //txt_stress_steel.Text = fs_stress_steel.ToString();
                            break;
                        case "MODULAR_RATIO":
                            m_modular_ratio = mList.GetDouble(1);
                            //txt_modular_ratio.Text = m_modular_ratio.ToString();
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
        public void Write_Cross_User_InputData()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {

                #region SWITCH
                //sw.WriteLine("HOGGING_MOMENT = {0}", txt_hogging_moment.Text);
                //sw.WriteLine("MAX_HOGGING_MOMENT = {0}", txt_max_hogging_moment.Text);
                sw.WriteLine("TOTAL_HOGGING_MOMENT = {0}", M_total_hogging_moment);
                sw.WriteLine("SPACING_LONGITUDINAL_GIRDER = {0}", L_spacing_longitudinal_girder);
                sw.WriteLine("NUMBER_LONGITUDINAL_GIRDER = {0}", number_longitudinal_girder);
                sw.WriteLine("SPACING_CROSS_GIRDER = {0}", spacing_cross_girder);
                sw.WriteLine("NUMBER_CROSS_GIRDER = {0}", number_cross_girder);
                sw.WriteLine("DEPTH_CROSS_GIRDER = {0}", D_depth_cross_girder);
                sw.WriteLine("WEB_THICKNESS_CROSS_GIRDER = {0}", b_web_thickness_cross_girder);
                //sw.WriteLine("SHEAR_DL_SIDL = {0}", txt_shear.Text);
                //sw.WriteLine("MAX_SHEAR = {0}", txt_maximum_shear.Text);
                sw.WriteLine("TOTAL_SHEAR = {0}", W_total_shear);
                sw.WriteLine("GRADE_CONCRETE = {0}", grade_concrete);
                sw.WriteLine("GRADE_STEEL = {0}", grade_steel);
                sw.WriteLine("CLEAR_COVER = {0}", clear_cover);
                sw.WriteLine("STRESS_CONCRETE = {0}", fc_stress_concrete);
                sw.WriteLine("STRESS_STEEL = {0}", fs_stress_steel);
                sw.WriteLine("MODULAR_RATIO = {0}", m_modular_ratio);
                #endregion
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_1=(1) {0}", _1);
                sw.WriteLine("_2=(2) {0}", _2);
                sw.WriteLine("_3=(3) {0}", _3);
                sw.WriteLine("_4=(4) {0}", _4);
                sw.WriteLine("_5=(5) {0}", _5);
                sw.WriteLine("_6=(6) {0}", _6);
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
    public class DeckSlab
    {
        public string rep_file_name = "";
        public string file_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_path = "";
        public string drawing_path = "";
        public bool is_process = false;


        public double Ds, gamma_c;
        public double self_weight_slab, self_weight_wearing_cource, total_weight;
        public double Dwc, gamma_wc;
        public double width_carrage_way, effe_span, concrete_grade, steel_grade, sigma_cb, sigma_st;
        public double m, j, Q, minimum_cover, L, no_main_girder, width_long_girders;
        public double B, width_cross_girders;
        public double load, width, length, impact_factor, continuity_factor, mu;



        double _A, _B, _C, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;
        IApplication iApp = null;

        public DeckSlab(IApplication app)
        {
            this.iApp = app;
            _A = 0.0;
            _B = 0.0;
            _C = 0.0;
            _D = 0.0;
            _E = 0.0;
            _F = 0.0;
            _bd1 = 0.0;
            _sp1 = 0.0;
            _bd2 = 0.0;
            _sp2 = 0.0;

            B = 0d;
            L = 0d;
        }

        public void Calculate_Program(string file_name)
        {
            frmCurve f_c = null;

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));


            #region TechSOFT Banner
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("\t\t**********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21            *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
            sw.WriteLine("\t\t*                                            *");
            sw.WriteLine("\t\t*           DESIGN OF DECK SLAB              *");
            sw.WriteLine("\t\t*          FOR T-BEAM RCC BRIDGE             *");
            sw.WriteLine("\t\t**********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion


            try
            {
                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine(" Width of Carriage way = {0:f3} m", width_carrage_way);
                sw.WriteLine(" Effective Span of Tee Beam = {0} m", effe_span);
                sw.WriteLine(" Concrete Grade = M {0:f0} ", concrete_grade);
                sw.WriteLine(" Steel Grade = Fe {0:f0} ", steel_grade);
                sw.WriteLine(" Permissible Stress in Concrete [σ_cb] = {0} N/sq.m", sigma_cb);
                sw.WriteLine(" Permissible Stress in Steel [σ_st] = {0} N/sq.m", sigma_st);

                sw.WriteLine(" Spacing of Cross Girders [L]= {0} m {1,40}", L, "Marked as (C) in the Drawing");

                sw.WriteLine(" No. Of Main girders = {0:f0} ", no_main_girder);
                sw.WriteLine(" Spacing of main Girders [B] = {0} m {1,40}", B, "Marked as (A) in the Drawing");
                sw.WriteLine(" Width of Cross Girders = {0} mm {1,40}", width_cross_girders, "Marked as (D) in the Drawing");
                sw.WriteLine(" Width of Long Girders = {0} mm {1,40}", width_long_girders, "Marked as (B) in the Drawing");
                sw.WriteLine(" Modular ratio [m] = {0} ", m);
                sw.WriteLine(" Lever arm factor [j] = {0} ", j);
                sw.WriteLine(" Moment factor [Q] = {0} ", Q);

                sw.WriteLine(" Minimum Cover = {0} mm", minimum_cover);

                sw.WriteLine(" Load = {0:f3} kN", load);
                sw.WriteLine(" Width of Load [a]= {0:f3} m", width);
                sw.WriteLine(" Length of Load [b]= {0:f3} m", length);
                sw.WriteLine(" Impact Factor [IF]= {0:f3} ", impact_factor);
                sw.WriteLine(" Continuity Factor [CF]= {0:f3} ", continuity_factor);
                sw.WriteLine(" Constant [µ] = {0:f3} ", mu);

                sw.WriteLine(" Thickness of concrete Deck Slab [Ds] = {0} mm {1,40}", Ds, "Marked as (F) in the Drawing");
                sw.WriteLine(" Unit weight of concrete Deck Slab [γ_c]= {0} kN/cu.m", gamma_c);
                sw.WriteLine(" Thickness of Asphalt Wearing Course [Dwc] = {0} mm {1,40}", Dwc, "Marked as (E) in the Drawing");
                sw.WriteLine(" Unit weight of Asphalt Wearing Course [γ_wc] = {0} kN/cu.m", gamma_wc);

                sw.WriteLine();
                sw.WriteLine();
                #endregion

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Calculations for Bending Moments for Permanent Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Self weight of slab = Ds * γ_c = {0} * {1} = {2} kN/sq.m.",
                    (Ds / 1000),
                    gamma_c,
                    self_weight_slab);
                sw.WriteLine();
                sw.WriteLine("Self weight of wearing course = Dwc * γ_wc ");

                sw.WriteLine("                              = {0} * {1}", (Dwc / 1000), gamma_wc);
                sw.WriteLine("                              = {0} kN/sq.m.", self_weight_wearing_cource);

                sw.WriteLine();
                sw.WriteLine("                 Total weight = {0} + {1}", self_weight_slab, self_weight_wearing_cource);
                sw.WriteLine("                              = {0} kN/sq.m.", total_weight);

                double w1;

                w1 = B * no_main_girder * total_weight;
                sw.WriteLine();
                sw.WriteLine("Total Permanent Load on Slab Panel = W1");
                sw.WriteLine("                                   = {0} * {1} * {2}",
                                  B,
                                  no_main_girder,
                                  total_weight);
                sw.WriteLine("                                   = {0} kN", w1);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Full slab Panel is Loaded with uniformly distributed load");
                double k;
                //k = B / no_main_girder;

                k = B / L;


                sw.WriteLine("k = B / L = {0:f2} / {1} = {2:f2}",
                    B,
                    L,
                    k);
                sw.WriteLine("1/k = 1 / {0:f3} = {1:f3}",
                                    k,
                                    (1 / k));


                f_c = new frmCurve(k, 0.0, 0.0, LoadType.FullyLoad);
                f_c.m1 = 0.015;
                f_c.m2 = 0.045;
                f_c.ShowDialog();
                double m1, m2, MB, ML;

                m1 = f_c.m1;
                m2 = f_c.m2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Select m1 and m2 from Pigeaud's curve");
                sw.WriteLine(" m1 = {0}    and      m2 = {1}", m1, m2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Permanent Load Bending Moments are obtained by");
                sw.WriteLine("applying continuity Factor (CF) as, ");
                sw.WriteLine();
                sw.WriteLine("MB = (CF * w1) * (m1 + µm2)");

                sw.WriteLine("   = ({0:f2} * {1:f2}) * ({2:f4} + {3:f4} * {4:f4})",
                    continuity_factor,
                    w1,
                    m1,
                    mu,
                    m2);
                MB = (continuity_factor * w1) * (m1 + (mu * m2));
                sw.WriteLine("   = {0:f3} kN-m", MB);


                sw.WriteLine();
                sw.WriteLine("ML = (CF * w1) * (m2 + µm1)");

                sw.WriteLine("   = ({0:f2} * {1:f2}) * ({2:f4} + {3:f4} * {4:f4})",
                    continuity_factor,
                    w1,
                    m2,
                    mu,
                    m1);
                ML = (continuity_factor * w1) * (m2 + mu * m1);
                sw.WriteLine("   = {0:f3} kN-m", ML);

                //sw.WriteLine();
                //sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Calculations for Bending Moments for Imposed Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //double B = 2.5;
                //double L = 4.0;

                //m1 0.0049  0.081  m2 0.015  0.022
                //m1 0.049  0.081  m2 0.015  0.022
                double _k = B / L;
                sw.WriteLine(" B = {0:f3} m.", B);
                sw.WriteLine(" L = {0:f3} m.", L);
                sw.WriteLine();
                sw.WriteLine(" k = B / L = {0:f2} / {1} = {2:f2} m.",
                    B,
                    L,
                    _k);
                double a = 0.84;
                double b = 4.57;

                double u, v;
                u = a + 2.0 * (Dwc / 1000.0);
                sw.WriteLine();
                sw.WriteLine("u = a + 2 * Dwc = {0} + 2 * {1} = {2:f2} m",
                    a,
                    (Dwc / 1000),
                    u);

                v = b + 2.0 * (Dwc / 1000.0);
                sw.WriteLine("v = b + 2 * Dwc = {0} + 2 * {1} = {2:f2} m",
                    b,
                    (Dwc / 1000),
                    v);
                double _v = 0.0;
                _v = v;
                if (v > L) v = L;

                double u_by_B = u / B;
                double v_by_L = v / L;

                if (k < 0.4)
                    k = 0.4;
                if (k > 1.0) k = 1.0;
                f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);

                f_c.m1 = 0.015;
                f_c.m2 = 0.045;

                f_c.ShowDialog();
                double _m1, _m2;
                _m1 = f_c.m1;
                _m2 = f_c.m2;

                sw.WriteLine();
                sw.WriteLine("u/B = {0} / {1} = {2:f2}",
                    u,
                    B,
                    u_by_B);
                sw.WriteLine("v/L = {0} / {1} = {2:f2}",
                                    v,
                                    L,
                                    v_by_L);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Select m1 & m2 from Pigeaud's curves");
                sw.WriteLine("Corresponding to k = {0:f3}, u/B = {1:f2} and v/L = {2:f2}",
                    _k,
                    u_by_B,
                    v_by_L);
                sw.WriteLine("m1 = {0} and m2 = {1}",
                    _m1,
                    _m2);

                double total_impact_load;
                total_impact_load = impact_factor * load;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Load per track including impact");
                sw.WriteLine("   = IF * load");
                sw.WriteLine("   = {0:f2} * {1:f2}", impact_factor, load);
                sw.WriteLine("   = {0:f2} N", total_impact_load);

                double w2 = total_impact_load * (L / _v);
                sw.WriteLine();
                sw.WriteLine("Effective load on slab Panel = w2");
                sw.WriteLine("    = {0:f2} * ({1:f2}/{2:f2})", total_impact_load, L, b);
                sw.WriteLine("    = {0:f2} kN", w2);
                sw.WriteLine();
                sw.WriteLine("Moment along Shorter span ");

                double _MB = w2 * (_m1 + mu * _m2);
                sw.WriteLine("       = MB = w2 * (m1 + µ*m2)");
                sw.WriteLine("       = {0:f3} * ({1:f3} + {2:f3}*{3:f3}", w2, _m1, mu, _m2);
                sw.WriteLine("       = {0:f3} kN", _MB);

                double _ML = w2 * (_m2 + mu * _m1);
                sw.WriteLine();
                sw.WriteLine("       = ML = w2 * (m2 + µ*m1)");
                sw.WriteLine("       = {0:f3} * ({1:f3} + {2:f3} * {3:f3}", w2, m2, mu, m1);
                sw.WriteLine("       = {0:f3} kN", _ML);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Slab is designed as Continuous,");
                sw.WriteLine("The Imposed Load Bending Moment are obtained");
                sw.WriteLine("by applying Continuity Factor(CF) as");
                sw.WriteLine();
                sw.WriteLine("MB = CF * MB = {0:f3} * {1:f3} = {2:f3} kN-m",
                    continuity_factor, _MB,
                    (continuity_factor * _MB));
                _MB = continuity_factor * _MB;

                sw.WriteLine("ML = CF * ML = {0:f3} * {1:f3} = {2:f3} kN-m",
                                    continuity_factor, _ML,
                                    (continuity_factor * _ML));
                _ML = continuity_factor * _ML;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Final Design Bending Moments for the Slab ");
                sw.WriteLine("Short Span B.N. = MB = {0:f3} + {1:f3} = {2:f3} kN-m",
                    MB, _MB,
                    (MB + _MB));
                MB += _MB;
                sw.WriteLine("Long Span B.M. = ML = {0:f3} + {1:f3} = {2:f3} kN-m",
                    ML, _ML,
                    (ML + _ML));
                ML += _ML;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Calculations for Effective Depth ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double d;

                d = (MB * 10E5) / (Q * 1000);
                d = Math.Sqrt(d);

                sw.WriteLine("d = √((MB * 10E+5)/(Q * 1000))");
                sw.WriteLine("  = √(({0:f3} * 10E+5)/({1:f3} * 1000)) ", MB, Q);
                sw.WriteLine("  = {0:f3} mm", d);

                double d1, d2;
                d1 = 12;

                sw.WriteLine();
                sw.WriteLine("Using {0:f0} mm dia bars", d1);
                double overall_depth = 0.0;
                overall_depth = d + minimum_cover + d1 / 2;

                sw.WriteLine();
                sw.WriteLine("Overall depth of Deck slab = {0:f3} + {1:f3} + {2:f3}", d, minimum_cover, (d1 / 2));

                sw.WriteLine("                           = {0:f3} mm", overall_depth);

                double _over_dep;
                _over_dep = overall_depth / 100;
                _over_dep = (double)(int)_over_dep;
                sw.WriteLine();
                sw.WriteLine("{0:f3} / 100 = {1:f3} = {2:f0}",
                    overall_depth,
                    (overall_depth / 100),
                    _over_dep);

                double _o_depth;
                _o_depth = _over_dep * 100 + 50;
                sw.WriteLine();
                if (_o_depth > overall_depth)
                {
                    sw.WriteLine("{0:f0} * 100 + 50 = {1:f0} > {2:f2} OK", _over_dep, _o_depth, overall_depth);
                }
                else
                {
                    _o_depth += 50;
                    sw.WriteLine("{0:f0} * 100 + 50 + 50 = {1:f0} > {2:f2} OK", _over_dep, _o_depth, overall_depth);
                }

                sw.WriteLine();

                if (_o_depth < overall_depth)
                    _o_depth += 50;
                double eff_depth;
                eff_depth = _o_depth - minimum_cover - (d1 / 2);
                sw.WriteLine();
                sw.WriteLine("Effective depth d = {0:f3} - {1:f3} - {2:f3} = {3:f3} mm",
                    _o_depth,
                    minimum_cover,
                    (d1 / 2), eff_depth);

                double adopt_eff_depth;
                adopt_eff_depth = (int)(eff_depth / 10);
                adopt_eff_depth *= 10;

                sw.WriteLine();
                sw.WriteLine("Adopt Eff. Depth  = {0:f3} mm", adopt_eff_depth);

                double Ast1 = (MB * 10E5) / (sigma_st * j * adopt_eff_depth);
                //S = S / 10;
                //S = (int)S;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Calculations for Reinforcement along shorter span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Ast1 = (Mb * 10E5)/(σ_st * j * d)");
                sw.WriteLine("     = ({0:f3} * 10E5)/({1:f3} * {2:f3}* {3:f3})", MB, sigma_st, j, adopt_eff_depth);
                sw.WriteLine("     = {0:f0} sq.mm", Ast1);
                double S = (1000 * (Math.PI * d1 * d1 / 4)) / Ast1;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("spacing of Bars = S = (1000 * (π * 12 * 12/4))/ Ast1");
                sw.WriteLine("                    = {0:f0} mm", S);
                sw.WriteLine("                    = {0:f0} /10 = {1:f3} = {1:f0}", S, (S / 10.0));

                if (S > 145)
                {
                    S = 150;
                }
                else
                {
                    S = (int)(S / 10.0);
                    S = (S * 10.0);
                }
                sw.WriteLine("                    = {0:f0} * 10 = {1:f0} mm", (S / 10.0), S);

                sw.WriteLine();
                sw.WriteLine("Adopt T12 bars @{0:f0} mm c/c {1,40}", S, "Marked as (1) in the Drawing");

                _sp1 = S;
                _bd1 = 12;



                d2 = 10;
                double res_eff_depth;
                res_eff_depth = adopt_eff_depth - (d2 / 2);
                double Ast2;
                Ast2 = (ML * 10E5) / (sigma_st * j * res_eff_depth);

                //S = (1000.0 * (Math.PI * 12.0 * 12.0 / 4.0)) / Ast2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Calculations for Reinforcement along longer span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Use 10 mm dia bars,");
                sw.WriteLine("Respective effective depth = {0} - {1} = {2} mm",
                    adopt_eff_depth,
                    (d2 / 2).ToString("0"),
                    res_eff_depth);

                sw.WriteLine();
                sw.WriteLine("Ast2 = (ML * 10E5)/(σ_st * j* d)");
                sw.WriteLine("     = ({0:f3} * 10E5)/({1:f2} * {2:f2}* {3:f2})",
                    ML, sigma_st, j, res_eff_depth);
                sw.WriteLine("     = {0:f2} sq.mm", Ast2);


                double spacing_bars;
                spacing_bars = (1000 * Math.PI * d2 * d2) / (4 * Ast2);



                sw.WriteLine();
                sw.WriteLine("spacing of Bars = S = (1000 * (π * 10 * 10/4))/ Ast2");
                sw.WriteLine("                    = {0:f3} mm", spacing_bars);
                sw.WriteLine("                    = {0:f3}/10 = {1:f3} = {1:f0} ", spacing_bars,
                    (spacing_bars / 10));

                double _spacing_bars = (int)(spacing_bars / 10);
                _spacing_bars = _spacing_bars * 10;

                sw.WriteLine("                    = {0:f0} * 10 = {1:f0} mm", (_spacing_bars / 10), _spacing_bars);



                if (_spacing_bars > 200)
                    _spacing_bars = 200;
                else
                {
                    _spacing_bars = (int)(_spacing_bars / 10.0);
                    _spacing_bars = _spacing_bars * 10;
                }

                _sp2 = _spacing_bars;
                _bd2 = 10;
                sw.WriteLine();
                sw.WriteLine("Adopt T10 bars @{0:f0} mm c/c {1,40}", _spacing_bars, "Marked as (2) in the Drawing");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }

        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "DECK_SLAB_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                _A = B;
                _B = (width_long_girders / 1000.0);
                _C = L;
                _D = (width_cross_girders / 1000.0);
                _E = Dwc / 1000.0;
                _F = Ds / 1000.0;
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_bd1=BAR DIA  {0:f0}", _bd1);
                sw.WriteLine("_sp1=SPCG. {0:f0} MM C/C", _sp1);
                sw.WriteLine("_bd2=BAR DIA  {0}", _bd2);
                sw.WriteLine("_sp2=SPCG. {0:f0} MM C/C", _sp2);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Read_User_Input(string file_name)
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    //indx = lst_content[i].LastIndexOf(" ");
                    //if (indx > 0)
                    //    kStr = MyList.RemoveAllSpaces(lst_content[i].Substring(0, indx));
                    //else
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "WIDTH_CARRAGE_WAY":
                            width_carrage_way = mList.GetDouble(1);
                            break;
                        case "EFFE_SPAN":
                            effe_span = mList.GetDouble(1);
                            break;
                        case "CONCRETE_GRADE":
                            concrete_grade = mList.GetDouble(1);
                            break;
                        case "STEEL_GRADE":
                            steel_grade = mList.GetDouble(1);
                            break;
                        case "SIGMA_CB":
                            sigma_cb = mList.GetDouble(1);
                            break;
                        case "SIGMA_ST":
                            sigma_st = mList.GetDouble(1);
                            break;
                        case "SPACING_CROSS_GIRDER":
                            L = mList.GetDouble(1);
                            break;
                        case "NO_MAIN_GIRDER":
                            no_main_girder = mList.GetDouble(1);
                            break;
                        case "SPACING_MAIN_GIRDER":
                            B = mList.GetDouble(1);
                            break;
                        case "WIDTH_CROSS_GIRDERS":
                            width_cross_girders = mList.GetDouble(1);
                            break;
                        case "WIDTH_LONG_GIRDERS":
                            width_long_girders = mList.GetDouble(1);
                            break;
                        case "M":
                            m = mList.GetDouble(1);
                            break;
                        case "J":
                            j = mList.GetDouble(1);
                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            break;
                        case "MINIMUM_COVER":
                            minimum_cover = mList.GetDouble(1);
                            break;
                        case "LOAD":
                            load = mList.GetDouble(1);
                            break;
                        case "WIDTH":
                            width = mList.GetDouble(1);
                            break;
                        case "LENGTH":
                            length = mList.GetDouble(1);
                            break;
                        case "IMPACT_FACTOR":
                            impact_factor = mList.GetDouble(1);
                            break;
                        case "CONTINUITY_FACTOR":
                            continuity_factor = mList.GetDouble(1);
                            break;
                        case "MU":
                            mu = mList.GetDouble(1);
                            break;
                        case "DS":
                            Ds = mList.GetDouble(1);
                            break;
                        case "GAMMA_C":
                            gamma_c = mList.GetDouble(1);
                            break;
                        case "DWC":
                            Dwc = mList.GetDouble(1);
                            break;
                        case "GAMMA_WC":
                            gamma_wc = mList.GetDouble(1);
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
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {


                #region User Data

                sw.WriteLine("WIDTH_CARRAGE_WAY = {0}", width_carrage_way);
                sw.WriteLine("EFFE_SPAN = {0}", effe_span);
                sw.WriteLine("CONCRETE_GRADE = {0}", concrete_grade);
                sw.WriteLine("STEEL_GRADE = {0}", steel_grade);
                sw.WriteLine("SIGMA_CB = {0}", sigma_cb);
                sw.WriteLine("SIGMA_ST = {0}", sigma_st);
                sw.WriteLine("SPACING_CROSS_GIRDER = {0}", L);
                sw.WriteLine("NO_MAIN_GIRDER = {0}", no_main_girder);
                sw.WriteLine("SPACING_MAIN_GIRDER = {0}", B);
                sw.WriteLine("WIDTH_CROSS_GIRDERS = {0}", width_cross_girders);
                sw.WriteLine("WIDTH_LONG_GIRDERS = {0}", width_long_girders);
                sw.WriteLine("M = {0}", m);
                sw.WriteLine("J = {0}", j);
                sw.WriteLine("Q = {0}", Q);
                sw.WriteLine("MINIMUM_COVER = {0}", minimum_cover);
                sw.WriteLine("LOAD = {0}", load);
                sw.WriteLine("WIDTH = {0}", width);
                sw.WriteLine("LENGTH = {0}", length);
                sw.WriteLine("IMPACT_FACTOR = {0}", impact_factor);
                sw.WriteLine("CONTINUITY_FACTOR = {0}", continuity_factor);
                sw.WriteLine("MU = {0}", mu);
                sw.WriteLine("DS = {0}", Ds);
                sw.WriteLine("GAMMA_C = {0}", gamma_c);
                sw.WriteLine("DWC = {0}", Dwc);
                sw.WriteLine("GAMMA_WC = {0}", gamma_wc);

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
            kPath = Path.Combine(kPath, "RCC T-Beam Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Deck Slab");

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
                //this.Text = "DESIGN OF DECK SLAB : " + value;
                user_path = value;

                file_path = user_path;
                //file_path = Path.Combine(user_path, "Working Stress Design"); //Chiranjit [2013 07 22]
                ////file_path = GetAstraDirectoryPath(user_path);
                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                file_path = Path.Combine(file_path, "Design of Deck Slab");
                //file_path = GetAstraDirectoryPath(user_path);
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);


                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_T_Beam_Deck_Slab.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_DECK_SLAB.FIL");

                drawing_path = Path.Combine(system_path, "DECK_SLAB_DRAWING.FIL");

                //btnReport.Enabled = File.Exists(rep_file_name);
                ////btnDrawing.Enabled = File.Exists(rep_file_name);
                //btnProcess.Enabled = Directory.Exists(value);

                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Read_User_Input(user_input_file);
                //}

            }
        }

    }
    public class CantileverSlab
    {
        public string rep_file_name = "";
        public string file_path = "";
        public string user_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_drawing_file = "";

        public bool is_process = false;

        #region Variable Declaration

        public double a1, a2, d1, a3, d2, d3, w1, a4, fact, a5, a6, d4, w2, a7, rcc_x, rcc_y;
        public double concrete_grade, steel_grade, sigma_cb, sigma_st, m, j, Q, gamma_c, gamma_wc, cover, wid_hnd_rail;

        #endregion

        #region Drawing Variable

        double _bd1, _bd2, _sp1, _sp2;


        #endregion

        IApplication iApp = null;
        public CantileverSlab(IApplication app)
        {
            this.iApp = app;
        }
        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF CANTILEVER SLAB :" + value;
                user_path = value;

                file_path = user_path;
                //file_path = Path.Combine(user_path, "Working Stress Design");
                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                file_path = Path.Combine(file_path, "Design of RCC Cantilever Slab");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Design_RCC_Cantilever_Slab.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_CANTILEVER_SLAB.FIL");
                user_drawing_file = Path.Combine(system_path, "CANTILEVER_SLAB_DRAWING.FIL");



                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Read_User_Input();
                //}
            }
        }

        public void Calculate_Program()
        {

            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            #region TechSOFT Banner
            //sw.WriteLine("****************************************************************");
            ////sw.WriteLine("DESIGN OF SINGLE SPAN ONE WAY RCC SLAB BY WORKING STRESS METHOD");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*          DESIGN OF CANTILEVER SLAB          *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            #region USER INPUT DATA
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("USER'S DATA");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Width of Girder [a1] = {0:f3} m     Marked as (C) in the Drawing", a1);
            //sw.WriteLine();
            sw.WriteLine("Width of Kerb [a2] = {0:f3} m       Marked as (B) in the Drawing", a2);
            //sw.WriteLine();
            sw.WriteLine("Height of Kerb [d1] = {0:f3} m      Marked as (K) in the Drawing", d1);
            //sw.WriteLine();
            sw.WriteLine("Distance from Girder Centre to Kerb [a3] = {0:f3} m   Marked as (A) in the Drawing", a3);
            //sw.WriteLine();
            sw.WriteLine("Thickness of Cantilever Slab at Girder face [d2] = {0:f3} m  Marked as (E) in the Drawing", d2);
            //sw.WriteLine();
            sw.WriteLine("Thickness of Cantilever Slab at Free End [d3] = {0:f3} m   Marked as (L) in the Drawing", d3);
            //sw.WriteLine();
            sw.WriteLine("Applied Wheel Load [w1] = {0:f3} kN    Marked as (M) in the Drawing", w1);
            //sw.WriteLine();
            sw.WriteLine("Load Width [a4] = {0:f3} m    Marked as (G) in the Drawing", a4);
            //sw.WriteLine();
            sw.WriteLine("Impact Factor [fact] = {0:f3} ", fact);
            //sw.WriteLine();
            sw.WriteLine("Distance of Load Centre from Girder Face [a5] = {0:f3} m   Marked as (D) in the Drawing", a5);
            //sw.WriteLine();
            sw.WriteLine("Distance of Edge of Load from Kerb Face [a6] = {0:f3}m     Marked as (H) in the Drawing", a6);
            //sw.WriteLine();
            sw.WriteLine("Thickness of wearing Course [d4] = {0:f3} m     Marked as (F) in the Drawing", d4);
            //sw.WriteLine();
            sw.WriteLine("Load from Hand Rail [w2] = {0:f3} kN", w2);
            //sw.WriteLine();
            sw.WriteLine("Distance from Post Edge to Free End [a7] = {0:f3} m    Marked as (J) in the Drawing", a7);
            //sw.WriteLine();
            sw.WriteLine("Width of Hand Railing = {0:f3} m    Marked as (I) in the Drawing", wid_hnd_rail);
            //sw.WriteLine();
            sw.WriteLine("RCC Post Size = {0:f3} m X {1:f3} m ", rcc_x, rcc_y);
            //sw.WriteLine();
            //sw.WriteLine("rcc_y = {0:f3} ",rcc_y); 
            sw.WriteLine("Concrete Grade = M {0:f0}  f_ck = {0:f0} N/sq.mm  ", concrete_grade);
            //sw.WriteLine();
            sw.WriteLine("Steel Grade = Fe {0:f0}    f_y  = {0:f0} N/sq.mm", steel_grade);
            //sw.WriteLine();
            sw.WriteLine("Permissible Stress [σ_cb] = {0:f3} N/sq.m", sigma_cb);
            //sw.WriteLine();
            sw.WriteLine("Permissible Stress [σ_st] = {0:f3} N/sq.m", sigma_st);
            //sw.WriteLine();
            sw.WriteLine("Lever Arm Factor [j] = {0:f3} ", j);
            sw.WriteLine("Moment Factor [Q] = {0:f3} ", Q);
            sw.WriteLine("Unit Weight of Concrete [γ_c] = {0:f3} kN/cu.m. ", gamma_c);
            sw.WriteLine("Unit Weight of Wearing Course [γ_wc] = {0:f3} kN/cu.m.", gamma_wc);
            sw.WriteLine("Clear Cover [cover] = {0:f3} m", cover);
            sw.WriteLine("Modular Ratio [m] = {0:f2} ", m);
            sw.WriteLine();
            sw.WriteLine();

            #endregion

            try
            {
                #region STEP 1
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Permanent Loads and Moments");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,14}{4,15}",
                    "SL.N.",
                    "Structural",
                    "Load",
                    "Distance",
                    "Bending Moment");
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,14}{4,15}",
                                   "",
                                   " Element",
                                   "(kN)",
                                   "(m)  ",
                                   "(kN-m)");

                //1
                double dist = a3 + a2 - (a1 / 2) - (rcc_x / 2 + a7);
                double load = w2;
                double bend_mom_1 = dist * load;

                sw.WriteLine();
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                    "1.",
                    "Hand Rail",
                    load.ToString("0.000"),
                    dist.ToString("0.000"),
                    bend_mom_1.ToString("0.000"));

                //2
                dist = a3 + a2 - (a1 / 2) - (rcc_x / 2 + a7);
                load = rcc_x * rcc_y * gamma_c;
                double bend_mom_2 = dist * load;

                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                    "2.",
                    "RCC Post",
                    load.ToString("0.000"),
                    dist.ToString("0.000"),
                    bend_mom_2.ToString("0.000"));

                //3
                dist = a3 - (a1 / 2) + (a2 / 2);
                load = a2 * d1 * gamma_c;
                double bend_mom_3 = dist * load;

                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                                    "3.",
                                    "Kerb",
                                    load.ToString("0.000"),
                                    dist.ToString("0.000"),
                                    bend_mom_3.ToString("0.000"));

                //4
                dist = (a3 - (a1 / 2)) / 2;
                load = (a3 - (a1 / 2)) * d4 * gamma_wc;
                double bend_mom_4 = dist * load;
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                                    "4.",
                                    "Wearing Course",
                                    load.ToString("0.000"),
                                    dist.ToString("0.000"),
                                    bend_mom_4.ToString("0.000"));

                //5
                dist = (a3 - (a1 / 2) + a2) / 2;
                load = (a3 - a1 / 2 + a2) * (d2 + d3) / 2 * gamma_c;
                double bend_mom_5 = dist * load;
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                                    "5.",
                                    "R.C Slab",
                                    load.ToString("0.000"),
                                    dist.ToString("0.000"),
                                    bend_mom_5.ToString("0.000"));

                double total_fixed_load_bend_mom = 0.0;
                total_fixed_load_bend_mom = bend_mom_1 + bend_mom_2 + bend_mom_3 +
                    bend_mom_4 + bend_mom_5;
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("{0,-45}{1,15}",
                                    "TOTAL Permanent Load Bending Moment",
                                    total_fixed_load_bend_mom.ToString("0.000"));

                sw.WriteLine("------------------------------------------------------------");


                #endregion
                #region STEP 2

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Design Live Load Bending Moment");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double x = a5; // ?
                sw.WriteLine("For wheel Load placed at a6 = {0:f2} m  ", a6);
                sw.WriteLine("distance between Kerb and Load and at a5 = {0:f2} m distance", a5);
                sw.WriteLine("between Load centre to face of girder = x");

                double bw = (a4 / 2) + 2 * d4;
                sw.WriteLine();
                sw.WriteLine("bw = ((a4/2) + 2 * d4) = {0:f2} + 2 * {1:f2} = {2:f2} m",
                    (a4 / 2), d4, bw);
                sw.WriteLine();
                sw.WriteLine("be = effective width of dispersion perpendicular to Span ");
                double be = 1.2 * x + bw;

                sw.WriteLine("   = 1.2 * x + bw = 1.2 * {0:f2} + {1:f2} = {2:f2} m", x, bw, be);
                double w3 = w1 * fact / be;

                sw.WriteLine();
                sw.WriteLine("Live Load per meter width including impact = w3");
                sw.WriteLine("     = (Applied Wheel Load * Fact)/be");
                sw.WriteLine("     = ({0:f3} * {1:f2}) / {2:f2} = {3:f2} kN",
                    w1, fact, be, w3);
                double w4 = w3 * a5;
                sw.WriteLine();
                sw.WriteLine("Design Live Load Bending Moment = w3 * a5 = {0:f2} * {1:f2}", w3, a5);
                sw.WriteLine("                                = {0:f2} kN-m", w4);





                #endregion

                //double total_fixed_load_bend_mom = 21.519;

                #region STEP 3

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Design Bending Moment");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double M = total_fixed_load_bend_mom + w4;
                sw.WriteLine("Design Bending Moment = M ");
                sw.WriteLine("  = Permanent Load Bending Moment + Live Load Bending Moment");
                sw.WriteLine("  = {0:f2} + {1:f2}", total_fixed_load_bend_mom, w4);
                sw.WriteLine("  = {0:f2} kN-m", M);

                #endregion

                #region STEP 4

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Required Distribution Steel ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double deff = Math.Sqrt((M * 10E5 / Q * 10E2)) / 1000.0;
                sw.WriteLine("Effective Depth required = √((M * 10^6)/(Q*1000))");
                sw.WriteLine("                         = √(({0:f2} * 10^6)/({1:f2}*1000))",
                    M,
                    Q);
                sw.WriteLine("                         = {0:f2} mm", deff);

                double deff_pro = (d2 - cover) * 1000;

                sw.WriteLine();
                sw.WriteLine("Effective Depth Provided = d2 - cover");
                sw.WriteLine("                         = {0:f2} - {1:f2}", d2 * 1000, cover * 1000);
                //sw.WriteLine("                         = {0:f2}", deff_pro);
                if (deff_pro > deff)
                {
                    sw.WriteLine("                         = {0:f2} mm > {1:f2} mm ,OK", deff_pro, deff);
                    deff = deff_pro;
                    sw.WriteLine("                So, deff = {0:f2} mm", deff);
                }
                else
                {
                    sw.WriteLine("                         = {0:f2} mm < {1:f2} mm ,NOT OK", deff_pro, deff);
                }


                sw.WriteLine();
                double ast = (M * 10E5) / (sigma_st * j * deff);
                sw.WriteLine("Steel Reinforcements = Ast = (M * 10^6)/(σ_st*j*deff)");
                sw.WriteLine("                     = ({0:f2} * 10^6)/({1:f2}*{2:f2}*{3:f2})",
                    M,
                    sigma_st,
                    j,
                    deff);
                sw.WriteLine("                     = {0:f0} sq.mm / m", ast);

                double bar_dia = 16;

                List<double> lst_Bar = new List<double>();
                lst_Bar.Add(10);
                lst_Bar.Add(12);
                lst_Bar.Add(16);
                lst_Bar.Add(20);
                lst_Bar.Add(25);
                lst_Bar.Add(32);

                double _ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 150.0);
                int indx = -1;
                do
                {
                    indx++;
                    bar_dia = lst_Bar[indx];
                    _ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 150.0);
                }
                while (_ast < ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @ 150 mm c/c, Ast = {1:f0} sq.mm     Marked as (1) in the Drawing",
                    lst_Bar[indx],
                    _ast);

                _bd1 = bar_dia;
                _sp1 = 150;
                double req_dist_steel = (0.12 / 100) * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("Required Distribution Steel = (0.12*1000*{0:f2})/100", deff);
                sw.WriteLine("                            = {0:f2} sq.mm", req_dist_steel);

                indx = 0;
                do
                {
                    indx++;
                    bar_dia = lst_Bar[indx];
                    _ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 150.0);
                }
                while (_ast < req_dist_steel);
                _bd2 = bar_dia;
                _sp2 = 150;
                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @ 150 mm c/c, Ast = {1:f0} sq.mm     Marked as (2) in the Drawing",
                    lst_Bar[indx],
                    _ast);

                #endregion

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------");
                sw.WriteLine("-----------------        END OF REPORT         -------------------------");
                sw.WriteLine("------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }


        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
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
                        case "a1":
                            a1 = mList.GetDouble(1);
                            break;
                        case "a2":
                            a2 = mList.GetDouble(1);
                            break;
                        case "d1":
                            d1 = mList.GetDouble(1);
                            break;
                        case "a3":
                            a3 = mList.GetDouble(1);
                            break;
                        case "d3":
                            d3 = mList.GetDouble(1);
                            break;
                        case "w1":
                            w1 = mList.GetDouble(1);
                            break;
                        case "a4":
                            a4 = mList.GetDouble(1);
                            break;
                        case "fact":
                            fact = mList.GetDouble(1);
                            break;
                        case "a5":
                            a5 = mList.GetDouble(1);
                            break;
                        case "a6":
                            a6 = mList.GetDouble(1);
                            break;
                        case "d4":
                            d4 = mList.GetDouble(1);
                            break;
                        case "w2":
                            w2 = mList.GetDouble(1);
                            break;
                        case "a7":
                            a7 = mList.GetDouble(1);
                            break;
                        case "width_of_hand_rail":
                            wid_hnd_rail = mList.GetDouble(1);
                            break;
                        case "rcc_x":
                            rcc_x = mList.GetDouble(1);
                            break;
                        case "rcc_y":
                            rcc_y = mList.GetDouble(1);
                            break;
                        case "concrete_grade":
                            concrete_grade = mList.GetDouble(1);
                            break;
                        case "steel_grade":
                            steel_grade = mList.GetDouble(1);
                            break;
                        case "sigma_cb":
                            sigma_cb = mList.GetDouble(1);
                            break;
                        case "sigma_st":
                            sigma_st = mList.GetDouble(1);
                            break;
                        case "m":
                            m = mList.GetDouble(1);
                            break;
                        case "j":
                            j = mList.GetDouble(1);

                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            break;
                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            break;
                        case "gamma_wc":
                            gamma_wc = mList.GetDouble(1);
                            break;
                        case "cover":
                            cover = mList.GetDouble(1);
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex) { }
            lst_content.Clear();
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("a1 = {0}", a1);
                sw.WriteLine("a2 = {0}", a2);
                sw.WriteLine("d1 = {0}", d1);
                sw.WriteLine("a3 = {0}", a3);
                sw.WriteLine("d3 = {0}", d3);
                sw.WriteLine("w1 = {0}", w1);
                sw.WriteLine("a4 = {0}", a4);
                sw.WriteLine("fact = {0}", fact);
                sw.WriteLine("a5 = {0}", a5);
                sw.WriteLine("a6 = {0}", a6);
                sw.WriteLine("d4 = {0}", d4);
                sw.WriteLine("w2 = {0}", w2);
                sw.WriteLine("a7 = {0}", a7);
                sw.WriteLine("width_of_hand_rail = {0}", wid_hnd_rail);
                sw.WriteLine("rcc_x = {0}", rcc_x);
                sw.WriteLine("rcc_y = {0}", rcc_y);
                sw.WriteLine("concrete_grade = {0}", concrete_grade);
                sw.WriteLine("steel_grade = {0}", steel_grade);
                sw.WriteLine("sigma_cb = {0}", sigma_cb);
                sw.WriteLine("sigma_st = {0}", sigma_st);
                sw.WriteLine("m = {0}", m);
                sw.WriteLine("j = {0}", j);
                sw.WriteLine("Q = {0}", Q);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("gamma_wc = {0}", gamma_wc);
                sw.WriteLine("cover = {0}", cover);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_A={0}", a3 * 1000);
                sw.WriteLine("_B={0}", a2 * 1000);
                sw.WriteLine("_C={0}", a1 * 1000);
                sw.WriteLine("_D={0}", a5 * 1000);
                sw.WriteLine("_E={0}", d2 * 1000);
                sw.WriteLine("_F={0}", d4 * 1000);
                sw.WriteLine("_G={0}", a4 * 1000);
                sw.WriteLine("_H={0}", a6 * 1000);
                sw.WriteLine("_I={0}", wid_hnd_rail * 1000);
                sw.WriteLine("_J={0}", a7 * 1000);
                sw.WriteLine("_K={0}", d1 * 1000);
                sw.WriteLine("_L={0}", d3 * 1000);
                sw.WriteLine("_M={0} kN", w1);
                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_sp2={0}", _sp2);

            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
    public enum eTBeamOption
    {
        None = -1,
        Analysis = 0,
        LongMainGirder = 1,
        CrossGirder = 2,
        RCCDeckSlab = 3,
        CantileverSlab = 4,
        Abutment = 5,
        RCCPier_1 = 6,
        RCCPier_2 = 7,
        MovingLoad = 8,
    }

    //Chiranjit [2012 12 18]
    //Add Section Properties
    public class T_Girder_Section
    {
        public int Nb;
        public double S, Bw, Dw, Bft, Dft, Bfb, Dfb, Bt, Dt, Bb, Db, Bs1, Ds1, Bs2, Ds2, Bs3, Ds3, Bs4, Ds4, Ixbs, Iybs;

        public T_Girder_Section()
        {
            Nb = 0;
            S = 0.0;
            Bw = 0.0;
            Dw = 0.0;
            Bft = 0.0;
            Dft = 0.0;
            Bfb = 0.0;
            Dfb = 0.0;
            Bt = 0.0;
            Dt = 0.0;
            Bb = 0.0;
            Db = 0.0;
            Bs1 = 0.0;
            Ds1 = 0.0;
            Bs2 = 0.0;
            Ds2 = 0.0;
            Bs3 = 0.0;
            Ds3 = 0.0;
            Bs4 = 0.0;
            Ds4 = 0.0;
            Ixbs = 0.0;
            Iybs = 0.0;

        }
        public T_Girder_Section(T_Girder_Section obj)
        {
            Nb = obj.Nb;
            S = obj.S;
            Bw = obj.Bw;
            Dw = obj.Dw;
            Bft = obj.Bft;
            Dft = obj.Dft;
            Bfb = obj.Bfb;
            Dfb = obj.Dfb;
            Bt = obj.Bt;
            Dt = obj.Dt;
            Bb = obj.Bb;
            Db = obj.Db;
            Bs1 = obj.Bs1;
            Ds1 = obj.Ds1;
            Bs2 = obj.Bs2;
            Ds2 = obj.Ds2;
            Bs3 = obj.Bs3;
            Ds3 = obj.Ds3;
            Bs4 = obj.Bs4;
            Ds4 = obj.Ds4;
            Ixbs = obj.Ixbs;
            Iybs = obj.Iybs;

        }

        public int Total_Plate
        {
            get
            {
                return Nb;
            }
            set
            {
                Nb = value;
            }
        }
        public double Area_in_Sq_m
        {
            get
            {
                return (Ax / 10E5);
            }
        }
        public double Ixx_in_Sq_Sq_m
        {
            get
            {
                return (Ixx / 10E11);
            }
        }
        public double Iyy_in_Sq_Sq_m
        {
            get
            {
                return (Iyy / 10E11);
            }
        }
        public double Izz_in_Sq_Sq_m
        {
            get
            {
                return (Izz / 10E11);
            }
        }


        public double Ax
        {
            get
            {
                return (Nb * (Bw * Dw) +
                    (Bft * Dft) +
                    (Bfb * Dfb) +
                    (Bt * Dt) +
                    (Bb * Db) +
                    (Bs1 * Ds1) +
                    (Bs2 * Ds2) +
                    (Bs3 * Ds3) +
                    (Bs4 * Ds4));
            }
        }
        public double Ixb
        {
            get
            {

                return (Nb * (Bw * Math.Pow(Dw, 3.0)) / 12.0 + (Bft * Dft) * Math.Pow((Dw / 2 + Dft / 2), 2.0) + (Bfb * Math.Pow(Dfb, 3.0)) / 12 + (Bfb * Dfb) * Math.Pow((Dw / 2 + Dfb / 2), 2.0));
            }
        }
        public double Ixtp
        {
            get
            {
                return (Bt * Math.Pow(Dt, 3.0)) / 12 + (Bt * Dt) * Math.Pow((Dt / 2 + Dft + Dw / 2), 2.0);
            }
        }
        public double Ixbp
        {
            get
            {
                return (Bb * Math.Pow(Db, 3.0)) / 12.0 + (Bb * Db) * Math.Pow((Db / 2 + Dfb + Dw / 2), 2.0);
            }
        }
        public double Ixp1
        {
            get
            {
                return (Bs1 * Math.Pow(Ds1, 3.0)) / 12.0;
            }
        }

        public double Ixp2
        {
            get
            {
                return (Bs2 * Math.Pow(Ds2, 3.0)) / 12.0;
            }
        }

        public double Ixp3
        {
            get
            {
                return (Bs3 * Math.Pow(Ds3, 3.0)) / 12.0;
            }
        }

        public double Ixp4
        {
            get
            {
                return (Bs4 * Math.Pow(Ds4, 3.0)) / 12.0;
            }
        }

        public double Ixx
        {
            get
            {
                return Ixb + Ixtp + Ixbp + Ixp1 + Ixp2 + Ixp3 + Ixp4;
            }
        }

        public double Iyb
        {
            get
            {
                return Nb * (Dw * Math.Pow(Bw, 3.0)) / 12 + Nb * (Bw * Dw) * Math.Pow((S / 2), 2.0) + Nb * (Dft * Math.Pow(Bft, 3.0)) / 12.0 + Nb * (Dft * Bft) * Math.Pow((S / 2), 2) + Nb * (Dfb * Math.Pow(Bfb, 3)) / 12 + Nb * (Dfb * Bfb) * Math.Pow((S / 2), 2);
            }
        }
        public double Iytp
        {
            get
            {
                return (Dt * Math.Pow(Bt, 3.0)) / 12.0;
            }
        }

        public double Iybp
        {
            get
            {
                return (Db * Math.Pow(Bb, 3.0)) / 12.0;
            }
        }


        public double Iyp1
        {
            get
            {
                return (Ds1 * Math.Pow(Bs1, 3.0)) / 12.0 + (Ds1 * Bs1) * Math.Pow((Bs1 / 2 + Bw / 2 + S / 2), 2.0);
            }
        }
        public double Iyp2
        {
            get
            {
                return (Ds2 * Math.Pow(Bs2, 3.0)) / 12.0 + (Ds2 * Bs2) * Math.Pow((S / 2 - Dw / 2 - Bs2 / 2), 2.0);
            }
        }
        public double Iyp3
        {
            get
            {
                return (Ds3 * Math.Pow(Bs3, 3.0)) / 12.0 + (Ds3 * Bs3) * Math.Pow((S / 2 - Dw / 2 - Bs3 / 2), 2.0);
            }
        }
        public double Iyp4
        {
            get
            {
                return (Ds4 * Math.Pow(Bs4, 3.0)) / 12.0 + (Ds4 * Bs4) * Math.Pow((S / 2 + Bw / 2 + Bs4 / 2), 2.0);
            }
        }

        public double Iyy
        {
            get
            {
                return Iyb + Iytp + Iybp + Iyp1 + Iyp2 + Iyp3 + Iyp4;
            }
        }

        public double Izz
        {
            get
            {
                return Ixx + Iyy;
            }
        }


        public double Area_Top_Plate
        {
            get
            {
                return (Bt * Dt);
            }
        }
        public double Area_Bottom_Plate
        {
            get
            {
                return (Bb * Db);
            }
        }
        public double Area_Top_Flange_Plate
        {
            get
            {
                return (Bft * Dft);
            }
        }
        public double Area_Bottom_Flange_Plate
        {
            get
            {
                return (Bfb * Dfb);
            }
        }
        public double Area_Web_Plate
        {
            get
            {
                return (Nb * Bw * Dw);
            }
        }
        public double Area_Side_Plate
        {
            get
            {
                return (Bs1 * Ds1 + Bs2 * Ds2 + Bs3 * Ds3 + Bs4 * Ds4);
            }
        }

        public double Area_Total_Plate
        {
            get
            {
                return (Area_Top_Plate + Area_Bottom_Plate +
                    Area_Top_Flange_Plate + Area_Bottom_Flange_Plate + Area_Side_Plate);
            }
        }


        public List<string> Get_Result(string res_text)
        {
            List<string> list = new List<string>();
            list.Add(string.Format(""));
            list.Add("".PadLeft(res_text.Length,'-'));
            list.Add(string.Format(res_text));
            list.Add("".PadLeft(res_text.Length, '-'));
            list.Add(string.Format(""));
            Get_Input_Data(ref list);
            list.Add(string.Format(""));
            Get_Area_Result(ref list);
            list.Add(string.Format(""));
            Get_Ixx_Result(ref list);
            list.Add(string.Format(""));
            Get_Iyy_Result(ref list);
            list.Add(string.Format(""));
            Get_Izz_Result(ref list);
            list.Add(string.Format(""));

            return list;

        }

        public void Get_Izz_Result(ref List<string> list)
        {

            list.Add(string.Format("Izz = Ixx + Iyy "));
            list.Add(string.Format(""));
            list.Add(string.Format("    = {0:f6} sq.sq.m", Izz_in_Sq_Sq_m));
            //list.Add(string.Format(" = 4324246133 "));
        }
        public void Izz_Result_ToStream(StreamWriter sw)
        {
            try
            {
                List<string> list = new List<string>();
                Get_Izz_Result(ref list);
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
        }
        public void Get_Iyy_Result(ref List<string> list)
        {
            if (Iyb != 0.0)
            {
                if ((Dw * Bw) > 0.0)
                    list.Add(string.Format("Iyb  = Nb * (Dw*Bw^3)/12 + Nb*(Bw*Dw)*(S/2)^2 "));
                if ((Dft * Bft) > 0.0)
                    list.Add(string.Format("       + Nb*(Dft*Bft^3)/12 + Nb*(Dft*Bft)*(S/2)^2 "));
                if ((Dfb * Bfb) > 0.0)
                    list.Add(string.Format("       + Nb*(Dfb*Bfb^3)/12 + Nb*(Dfb*Bfb)* (S/2)^2 "));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                if ((Dw * Bw) > 0.0)
                    list.Add(string.Format("     = {0} * ({1}*{2}^3)/12 + {0}*({2}*{1})*({3}/2)^2 ", Nb, Dw, Bw, S));
                if ((Dft * Bft) > 0.0)
                    list.Add(string.Format("       + {0} * ({1}*{2}^3)/12 + {0}*({2}*{1})*({3}/2)^2 ", Nb, Dft, Bft, S));
                if ((Dfb * Bfb) > 0.0)
                    list.Add(string.Format("       + {0} * ({1}*{2}^3)/12 + {0}*({2}*{1})*({3}/2)^2 ", Nb, Dfb, Bfb, S));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iyb));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            if (Iytp != 0.0)
            {
                list.Add(string.Format("Iytp = (Dt*Bt^3)/12 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 ", Dt, Bt));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iytp));
                list.Add(string.Format(""));
            }
            if (Iybp != 0.0)
            {
                list.Add(string.Format("Iybp = (Db*Bb^3)/12 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 ", Db, Bb));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iybp));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            if (Iyp1 != 0.0)
            {
                list.Add(string.Format("Iyp1 = (Ds1*Bs1^3)/12 + (Ds1*Bs1)*(Bs1/2 + Bw/2 + S/2)^2 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 + ({0}*{1})*({1}/2 + {2}/2 + {3}/2)^2 ", Ds1, Bs1, Bw, S));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iyp1));
                //list.Add(string.Format(" = 107422200 "));
                list.Add(string.Format(""));
            }
            if (Iyp2 != 0.0)
            {
                list.Add(string.Format("Iyp2 = (Ds2*Bs2^3)/12 + (Ds2*Bs2)*( S/2 - Dw/2 - Bs2/2)^2 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 + ({0}*{1})*({2}/2 + {3}/2 + {1}/2)^2 ", Ds2, Bs2, S, Dw));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iyp2));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            if (Iyp3 != 0.0)
            {
                list.Add(string.Format("Iyp3 = (Ds3*Bs3^3)/12 + (Ds3*Bs3)*( S/2 - Dw/2 - Bs3/2)^2 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 + ({0}*{1})*({2}/2 + {3}/2 + {1}/2)^2 ", Ds3, Bs3, S, Dw));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iyp3));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            if (Iyp4 != 0.0)
            {
                list.Add(string.Format("Iyp4 = (Ds4*Bs4^3)/12 + (Ds4*Bs4)*( S/2 + Bw/2 + Bs4/2)^2 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 + ({0}*{1})*({2}/2 + {3}/2 + {1}/2)^2 ", Ds4, Bs4, S, Bw));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iyp4));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            if (Iyy != 0.0)
            {
                string s = "";
                if (Iyb != 0.0) s += " Iyb";
                if (Iytp != 0.0) s += " + Iytp";
                if (Iybp != 0.0) s += " + Iybp";
                if (Iyp1 != 0.0) s += " + Iyp1";
                if (Iyp2 != 0.0) s += " + Iyp2";
                if (Iyp3 != 0.0) s += " + Iyp3";
                if (Iyp4 != 0.0) s += " + Iyp4";
                list.Add(string.Format("Iyy = {0}", s));
                s = "";
                if (Iyb != 0.0) s += string.Format(" {0:E3}", Iyb);
                if (Iytp != 0.0) s += string.Format(" {0:E3}", Iytp);
                if (Iybp != 0.0) s += string.Format(" {0:E3}", Iybp);
                if (Iyp1 != 0.0) s += string.Format(" {0:E3}", Iyp1);
                if (Iyp2 != 0.0) s += string.Format(" {0:E3}", Iyp2);
                if (Iyp3 != 0.0) s += string.Format(" {0:E3}", Iyp3);
                if (Iyp4 != 0.0) s += string.Format(" {0:E3}", Iyp4);

                list.Add(string.Format("    = {0}", s));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Iyy));
                //list.Add(string.Format(" = 1433479467 "));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:f6} sq.sq.m", Iyy_in_Sq_Sq_m));
                list.Add(string.Format(""));
            }
        }
        public void Iyy_Result_ToStream(StreamWriter sw)
        {
            try
            {
                List<string> list = new List<string>();
                Get_Iyy_Result(ref list);
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
        }

        public void Get_Ixx_Result(ref List<string> list)
        {

            list.Add(string.Format(""));

            if (Ixb != 0.0)
            {
                if (((Bw * Dw) > 0.0))
                    list.Add(string.Format("Ixb = Nb x (Bw x Dw^3)/12"));

                if (((Bft * Dft) > 0.0))
                    list.Add(string.Format("       + (Bft*Dft)*(Dw/2 + Dft/2)^2"));
                if (((Bfb * Dfb) > 0.0))
                    list.Add(string.Format("       + (Bfb*Dfb^3)/12"));
                if (((Bfb * Dfb) > 0.0))
                    list.Add(string.Format("       + (Bfb*Dfb)*(Dw/2 + Dfb/2)^2"));
                list.Add(string.Format(""));
                if (((Bw * Dw) > 0.0))
                    list.Add(string.Format("    = {0} x ({1} x {2}^3)/12", Nb, Bw, Dw));
                if (((Bft * Dft) > 0.0))
                    list.Add(string.Format("      + ({0} x {1})*({2}/2 + {1}/2)^2", Bft, Dft, Dw));
                if (((Bfb * Dfb) > 0.0))
                    list.Add(string.Format("      + ({0} x {1}^3)/12", Bfb, Dfb));
                if (((Bfb * Dfb) > 0.0))
                    list.Add(string.Format("      + ({0} x {1})*({2}/2 + {1}/2)^2", Bfb, Dfb, Dw));

                //list.Add(string.Format("     + ({0}*{1}^3)/12 + ({0}*{1})*({2}/2 + {1}/2)^2 ", Bfb, Dfb, Dw));
                list.Add(string.Format(""));
                list.Add(string.Format("    = {0:E3} sq.sq.mm", Ixb));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            if (Ixtp != 0.0)
            {
                if (Bt * Dt > 0.0)
                {
                    list.Add(string.Format("Ixtp = (Bt*Dt^3)/12 + (Bt*Dt)*(Dt/2 + Dft + Dw/2)^2 "));
                    list.Add(string.Format("     = ({0}*{1}^3)/12 + ({0}*{1})*({1}/2 + {2} + {3}/2)^2 ", Bt, Dt, Dft, Dw));
                    list.Add(string.Format(""));
                    list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixtp));
                    list.Add(string.Format(""));
                }
            }
            if (Ixbp != 0.0)
            {
                if (Bb * Db > 0.0)
                {
                    list.Add(string.Format("Ixbp = (Bb*Db^3)/12 + (Bb*Db)*(Db/2 + Dfb + Dw/2)^2 "));
                    list.Add(string.Format("     = ({0}*{1}^3)/12 + ({0}*{1})*({1}/2 + {2} + {3}/2)^2 ", Bb, Db, Dfb, Dw));
                    list.Add(string.Format(""));
                    list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixbp));
                    list.Add(string.Format(""));
                }
            }
            if (Ixp1 != 0.0)
            {
                list.Add(string.Format("Ixp1 = (Bs1*Ds1^3)/12 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 ", Bs1, Ds1));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixp1));
                list.Add(string.Format(""));
            }
            if (Ixp2 != 0.0)
            {
                list.Add(string.Format("Ixp2 = (Bs2*Ds2^3)/12 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 ", Bs2, Ds2));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixp2));
                list.Add(string.Format(""));
                //list.Add(string.Format(" = 91125000 "));
                list.Add(string.Format(""));
            }
            if (Ixp3 != 0.0)
            {
                list.Add(string.Format("Ixp3 = (Bs3*Ds3^3)/12 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 ", Bs3, Ds3));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixp3));
                list.Add(string.Format(""));
                //list.Add(string.Format(" = 91125000 "));
                list.Add(string.Format(""));
            }
            if (Ixp4 != 0.0)
            {
                list.Add(string.Format("Ixp4 = (Bs4*Ds4^3)/12 "));
                list.Add(string.Format("     = ({0}*{1}^3)/12 ", Bs4, Ds4));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixp4));
                list.Add(string.Format(""));
                //list.Add(string.Format(" = 91125000 "));
            }
            if (Ixx != 0.0)
            {
                list.Add(string.Format(""));

                string s = "";
                if (Ixb != 0.0) s += " Ixb";
                if (Ixtp != 0.0) s += " + Ixtp";
                if (Ixbp != 0.0) s += " + Ixbp";
                if (Ixp1 != 0.0) s += " + Ixp1";
                if (Ixp2 != 0.0) s += " + Ixp2";
                if (Ixp3 != 0.0) s += " + Ixp3";
                if (Ixp4 != 0.0) s += " + Ixp4";
                list.Add(string.Format("Ixx = {0}", s));
                s = "";
                if (Ixb != 0.0) s += string.Format(" {0:E3}", Ixb);
                if (Ixtp != 0.0) s += string.Format(" {0:E3}", Ixtp);
                if (Ixbp != 0.0) s += string.Format(" {0:E3}", Ixbp);
                if (Ixp1 != 0.0) s += string.Format(" {0:E3}", Ixp1);
                if (Ixp2 != 0.0) s += string.Format(" {0:E3}", Ixp2);
                if (Ixp3 != 0.0) s += string.Format(" {0:E3}", Ixp3);
                if (Ixp4 != 0.0) s += string.Format(" {0:E3}", Ixp4);

                list.Add(string.Format("    = {0}", s));
                //list.Add(string.Format("    = {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3} + {5:E3} + {6:E3} ",
                //    Ixb, Ixtp, Ixbp, Ixp1, Ixp2, Ixp3, Ixp4));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E3} sq.sq.mm", Ixx));

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:f6} sq.sq.m", Ixx_in_Sq_Sq_m));
                list.Add(string.Format(""));
            }
        }
        public void Ixx_Result_ToStream(StreamWriter sw)
        {
            try
            {
                List<string> list = new List<string>();
                Get_Ixx_Result(ref list);
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
        }

        public void Get_Area_Result(ref List<string> list)
        {
            if (Ax != 0.0)
            {
                if ((Bw * Dw) > 0.0)
                    list.Add(string.Format("Section Area = {0}(Bw * Dw)", (Nb == 1) ? "" : Nb.ToString() + " * "));
                if ((Bft * Dft) > 0.0)
                    list.Add(string.Format("                + (Bft * Dft)"));
                if ((Bfb * Dfb) > 0.0)
                    list.Add(string.Format("                + (Bfb * Dfb)"));
                if ((Bt * Dt) > 0.0)
                    list.Add(string.Format("                + (Bt * Dt)"));
                if ((Bb * Db) > 0.0)
                    list.Add(string.Format("                + (Bb * Db) "));
                if ((Bs1 * Bs1) > 0.0)
                    list.Add(string.Format("                + (Bs1 * Bs1) "));
                if ((Bs2 * Bs2) > 0.0)
                    list.Add(string.Format("                + (Bs2 * Bs2) "));
                if ((Bs3 * Bs3) > 0.0)
                    list.Add(string.Format("                + (Bs3 * Bs3) "));
                if ((Bs4 * Ds4) > 0.0)
                    list.Add(string.Format("                + (Bs4 * Ds4) "));
                list.Add(string.Format(""));

                if ((Bw * Dw) > 0.0)
                    list.Add(string.Format("             = {0}({1} * {2})", (Nb == 1) ? "" : Nb.ToString() + " * ", Bw, Dw));
                if ((Bft * Dft) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bft, Dft));
                if ((Bfb * Dfb) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bfb, Dfb));
                if ((Bt * Dt) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bt, Dt));
                if ((Bb * Db) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bb, Db));
                if ((Bs1 * Bs1) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bs1, Bs1));
                if ((Bs2 * Bs2) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bs2, Bs2));
                if ((Bs3 * Bs3) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bs3, Bs3));
                if ((Bs4 * Ds4) > 0.0)
                    list.Add(string.Format("                + ({0} * {1})", Bs4, Ds4));


                list.Add(string.Format(""));
                list.Add(string.Format("             = {0:f3}  sq.mm", Ax));
                list.Add(string.Format(""));
                list.Add(string.Format("Section Area = {0:f6}  sq.m", Area_in_Sq_m));
                list.Add(string.Format(""));
            }

        }
        public void Area_Result_ToStream(StreamWriter sw)
        {
            try
            {
                List<string> list = new List<string>();
                Get_Area_Result(ref list);
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
        }

        public void Get_Input_Data(ref List<string> list)
        {

            list.Add(string.Format(""));
            if (Nb > 0)
                list.Add(string.Format("Total Number of Web = Nb = {0} ", Nb));
            if (S > 0)
                list.Add(string.Format("Spacing between two Web = S = {0} mm", S));
            if (Bw > 0)
                list.Add(string.Format("Web Breadth = Bw = {0} mm", Bw));
            if (Dw > 0)
                list.Add(string.Format("Web Depth = Dw = {0} mm ", Dw));
            if (Area_Web_Plate > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Web Area = {0:f3} sq.mm ", Area_Web_Plate));
                list.Add(string.Format(""));
            }
            if (Bft > 0)
                list.Add(string.Format("Top Flange Breadth = Bft = {0} mm", Bft));
            if (Dft > 0)
                list.Add(string.Format("Top Flange Depth = Dft = {0} mm", Dft));
           
            if (Area_Top_Flange_Plate > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Top Flange Area = {0:f3} sq.mm ", Area_Top_Flange_Plate));
                list.Add(string.Format(""));
            }
            if (Bfb > 0)
                list.Add(string.Format("Bottom Flange Breadth = Bfb = {0} mm ", Bfb));
            if (Dfb > 0)
                list.Add(string.Format("Bottom Flange Depth = Dfb = {0} mm ", Dfb));
            if (Area_Bottom_Flange_Plate > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Bottom Flange Area = {0:f3} sq.mm ", Area_Bottom_Flange_Plate));
                list.Add(string.Format(""));
            }
            if (Dt > 0)
                list.Add(string.Format("Flange Plate at Top Breadth = Bt = {0} mm ", Bt));
            if (Bt > 0)
                list.Add(string.Format("Flange Plate at Top Depth = Dt = {0} mm", Dt));
            if (Area_Top_Plate > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Flange Plate at Top Area = {0:f3} sq.m ", Area_Top_Plate));
                list.Add(string.Format(""));
            }
            if (Bb > 0)
                list.Add(string.Format("Flange Plate at Bottom Breadth = Bb = {0} mm ", Bb));
            if (Db > 0)
                list.Add(string.Format("Flange Plate at Bottom Depth = Db = {0} mm ", Db));
            if (Area_Bottom_Plate > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Flange Plate at Bottom Area = {0:f3} sq.mm ", Area_Bottom_Plate));
                list.Add(string.Format(""));
            }
            if (Bs1 > 0)
                list.Add(string.Format("Side Plate 1 Breadth = Bs1 = {0} mm  ", Bs1));
            if (Ds1 > 0)
                list.Add(string.Format("Side Plate 1 Depth = Ds1 = {0} mm   ", Ds1));
            if (Bs2 > 0)
                list.Add(string.Format("Side Plate 2 Breadth = Bs2 = {0} mm   ", Bs2));
            if (Ds2 > 0)
                list.Add(string.Format("Side Plate 2 Depth = Ds2 = {0} mm   ", Ds2));
            if (Bs3 > 0)
                list.Add(string.Format("Side Plate 3 Breadth = Bs3 = {0} mm   ", Bs3));
            if (Ds3 > 0)
                list.Add(string.Format("Side Plate 3 Depth = Ds3 = {0} mm   ", Ds3));
            if (Bs4 > 0)
                list.Add(string.Format("Side Plate 4 Breadth = Bs4 = {0} mm   ", Bs4));
            if (Ds4 > 0)
                list.Add(string.Format("Side Plate 4 Depth = Ds4 = {0} mm   ", Ds4));
            if (Area_Side_Plate > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Total Side Plate Area = {0:f3} sq.mm ", Area_Side_Plate));
                list.Add(string.Format(""));
            }
        }
        public void Input_Data_ToStream(StreamWriter sw)
        {
            try
            {
                List<string> list = new List<string>();
                Get_Input_Data(ref list);
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
        }
    }

    //Chiranjit [2012 12 27]
    public class TLong_SectionProperties
    {
        public TLong_SectionProperties()
        {
            Ds = 0.0;
            D = 0.0;
            Bw = 0.0;
            L = 0.0;
            Gs = 0.0;
            Bc = 0.0;
            Bb = 0.0;
            Db = 0.0;
        }
        public double Ds { get; set; }
        public double D { get; set; }
        public double Bw { get; set; }
        public double L { get; set; }

        public double Bf
        {
            get
            {

                //if (Gs != 0.0 && Bc != 0.0)
                //    return (Gs / 2.0 + Bc);
                //if (Gs != 0.0)
                //    return (Gs * 2.0);
                return Gs;
            }
        }


        //Girder Spacing
        public double Gs { get; set; }
        //Cantilever Breadth
        public double Bc { get; set; }
        public double Bb { get; set; }
        public double Db { get; set; }
        public double Dw
        {
            get
            {
                return D - Ds - Db;
            }
        }

        public double Ixx
        {
            get
            {
                //return ((Bf * n * n * n) / 3.0) - (((Bf - Bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0); //Chiranjit [2013 06 28]




                double ina = ((Bf * n * n * n) / 3.0) -
                       (((Bf - Bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
                       (modular_ratio * Ast / 10E3 * (eff_depth - n) * (eff_depth - n));


                ina = ((Bf / 1000 * n / 1000 * n / 1000 * n / 1000) / 3.0) -
                       (((Bf / 1000 - Bw / 1000) * (n / 1000 - Ds / 1000) * (n / 1000 - Ds / 1000) * (n / 1000 - Ds / 1000)) / 3.0) +
                       (modular_ratio * Ast / 10E3 * (eff_depth / 1000 - n / 1000) * (eff_depth / 1000 - n / 1000));

              
                
       //Ina = ((Bf * n^3) / 3.0) - (((Bf - bw) * (n - Ds)^3) / 3.0) +  
       //      (m * Ast / 10^4 * (eff_depth - n)^2)

       //    = ((2.65 * 0.4171^3) / 3.0) - (((2.65 - 0.3) * (0.4171 - 0.25)^3) / 3.0) +  
             //(10 * 19302 / 10^4 * (1.662 - 0.4171)^2)


       //    = 2.998E+001 sq.sq.m.



                //if (n >= Ds)
                //{


                //    ina = ((Bf / 1000 * n / 1000 * n / 1000 * n / 1000) / 3.0) -
                //             (((Bf / 1000 - Bw / 1000) * (n / 1000 - Ds / 1000) * (n / 1000 - Ds / 1000) * (n / 1000 - Ds / 1000)) / 3.0) +
                //             (modular_ratio * Ast / 10E3 * (eff_depth / 1000 - n / 1000) * (eff_depth / 1000 - n / 1000));

                //}
                return ina * 10E11;
            }
        }

        public double Iyy
        {
            get
            {
                return (Ds * Bf * Bf * Bf / 12.0) + (Dw * Bw * Bw * Bw / 12.0) + (Db * Bb * Bb * Bb / 12.0);
            }
        }
        public double Izz
        {
            get
            {
                return (Ixx + Iyy);
            }
        }

        public double Ax
        {
            get
            {
                return (Ds * Bf) + (Dw * Bw) + (Db * Bb);
            }
        }


        public List<string> Results()
        {
            List<string> list = new List<string>();

            //list.Add(string.Format("RCC 'T' Girder Bridge, Section Properties."));
            //list.Add(string.Format("Main Long Girders:"));
            list.Add(string.Format("---------------"));
            list.Add(string.Format("USER INPUT DATA"));
            list.Add(string.Format("---------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            if (Ds != 0.0)
                list.Add(string.Format("Thickness of Deck Slab [Ds] = {0} mm", Ds));
            if (D != 0.0)
                list.Add(string.Format("Depth of Longitudinal Girder [D]= {0} mm", D));
            if (Bw != 0.0)
                list.Add(string.Format("Web Thickness of Longitudinal Girder [Bw] = {0} mm ", Bw));
            if (L != 0.0)
                list.Add(string.Format("Span of Girder [L] = {0} m", L));
            list.Add(string.Format(""));

            if (Gs != 0.0 && Bc != 0.0)
            {
                list.Add(string.Format("Girder Spacing [Gs] = {0} mm", Gs));
                list.Add(string.Format("Cantilever Breadth [Bc] = {0} mm", Bc));
                list.Add(string.Format("c/c distance of Longitudinal Girder [Bf] = Gs/2 + Bc = {0:f0} + {1:f0} = {2:f0} mm", Gs/2.0,Bc, Bf));
            }
            else
                list.Add(string.Format("c/c distance of Longitudinal Girder [Bf] = {0} mm", Bf));
            //if ()
            //{
            //    list.Add(string.Format("Cantilever Breadth [Bc] = {0} mm", Bc));
            //    //Bf = Gs / 2.0 + Bc / 2.0;
            //}






            if (Bb != 0.0)
                list.Add(string.Format("Width of Bottom Flange [Bb] = {0} mm", Bb));
            if (Db != 0.0)
                list.Add(string.Format("Depth of Bottom Flange [Db] = {0} mm", Db));
            list.Add(string.Format(""));
            if (Dw != 0.0)
            {
                list.Add(string.Format("Depth of Web [Dw] = D - Ds - Db "));
                list.Add(string.Format("                  = {0} - {1} - {2}", D, Ds, Db));
                list.Add(string.Format("                  = {0} mm", Dw));
                list.Add(string.Format("                  = {0:f3} m", (Dw / 1000.0)));
                list.Add(string.Format(""));
            }
            if (n != 0.0)
            list.Add(string.Format("Neutral Axis = n = {0:f4} mm.", n));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment of Inertia about Neutral Axis [Neutral Axis is lying outside the Top Flange of the Girder]"));
            list.Add(string.Format(""));
            if (Ixx != 0.0)
            {
                //list.Add(string.Format("Ixx = ((Bf * n^3) / 3.0) - (((Bf - Bw) * (n - Ds)^3) / 3.0)"));
                //list.Add(string.Format("    = (({0} * {1:f4}^3) / 3.0) - ((({0} - {2}) * ({1:f4} - {3})^3) / 3.0)", Bf, n, Bw, Ds));
             

                list.Add(string.Format("Moment of Inertia about Neutral Axis [Neutral Axis is lying outside the Top Flange of the Girder]"));
                list.Add(string.Format(""));
                list.Add(string.Format("Ixx = ((Bf * n^3) / 3.0) - (((Bf - bw) * (n - Ds)^3) / 3.0) +  "));
                list.Add(string.Format("      (m * Ast / 10^4 * (eff_depth - n)^2)"));
                list.Add(string.Format(""));

                list.Add(string.Format("    = (({0} * {1:f4}^3) / 3.0) - ((({0} - {2}) * ({1:f4} - {3})^3) / 3.0) +  ",
                    Bf / 1000, n / 1000, Bw / 1000, Ds / 1000));
                list.Add(string.Format("      ({0} * {1} / 10^4 * ({2} - {3:f4})^2)",
                    modular_ratio, Ast, eff_depth / 1000, n / 1000));

                //list.Add(string.Format("    = {0:E3} sq.sq.mm.", Ixx));
                //list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("    = {0:f4} sq.sq.m.", Ixx_in_Sq_Sq_m));
                list.Add(string.Format(""));


            }
            if (Iyy != 0.0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Iyy = (Ds x Bf^3 / 12) + (Dw x Bw^3 / 12) + (Db x Bb^3 / 12)"));
                list.Add(string.Format("    = ({0}  x {1}^3 / 12) + ({2} x {3}^3 / 12) + ({4} x {5}^3 / 12)", Ds, Bf, Dw, Bw, Db, Bb));
                list.Add(string.Format("    = {0:E3} sq.sq.mm.", Iyy));
                list.Add(string.Format(""));
                list.Add(string.Format("    = {0:f4} sq.sq.m.", Iyy_in_Sq_Sq_m));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            if (Izz != 0.0)
            {
                list.Add(string.Format("Izz = Ixx + Iyy = {0:E3} + {1:E3} = {2:E3} sq.sq.mm.", Ixx, Iyy, Izz));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("    = {0:f4} sq.sq.m.", Izz_in_Sq_Sq_m));
                list.Add(string.Format(""));
            }
            if (Area_in_Sq_m != 0.0)
            {
                list.Add(string.Format("Area = Ax = (Ds x Bf) + (Dw x Bw) + (Db x Bb)"));
                list.Add(string.Format("          = ({0} x {1}) + ({2} x {3}) + ({4} x {5})", Ds, Bf, Dw, Bw, Db, Bb));
                list.Add(string.Format("          = {0:f4} sq. mm.", Ax));
                list.Add(string.Format(""));
                list.Add(string.Format("    = {0:f5} sq.m.", Area_in_Sq_m));
            }
            list.Add(string.Format(""));

            return list;
        }
        public double n1
        {
            get
            {
                double a1;
                a1 = (Ds * (Bf - Bw));
                double _d1 = Ds / 2;
                double a4 = 1;
                double _v1, _v2, _v3, _v6, _v7;

                _v1 = a1;
                _v2 = (Ds / 2.0) * a1;
                _v3 = (Bw / 2.0);
                _v7 = a4;

                double _a, _b, _c;

                _a = _v3;
                _b = (_v1 + _v7);
                _c = (_v2);
                _b = _b / _a;
                _c = (_c / _a);

                double root_a;
                root_a = Math.Sqrt((_b * _b + 4 * _c));

                return ((root_a - _b) / 2.0);
            }

        }



        #region Chiranjit [2013 06 28]
        public double Ast;
        public double modular_ratio;
        public double eff_depth;

        public double n
        {
            get
            {
                Ast = (int)Ast;

                if (Ast == 0.0)
                    Ast = 19302.0;
                if (modular_ratio == 0.0)
                    modular_ratio = 10.0;
                if (eff_depth == 0.0)
                    eff_depth = 1662.00;


                //sw.WriteLine("Let 'n' be the depth of Neutral Axis from the top of Deck slab");


                //sw.WriteLine();
                double a1;
                //Bf = Bf * 1000.0;
                a1 = (Ds * (Bf - Bw));
                //d1 = 

                //sw.WriteLine("a1 = Ds * (Bf - bw)");
                //sw.WriteLine("   = {0} * ({1} - {2})", Ds, Bf, bw);
                //sw.WriteLine("   = {0} sq.mm", a1.ToString("f3"));
                //sw.WriteLine();
                double _d1 = Ds / 2;
                //sw.WriteLine("d1 = n - Ds/2 = n - {0}/2 = n - {1}", Ds, _d1);



                double a2 = 1;
                ////a2 = bw
                //sw.WriteLine();
                //sw.WriteLine("a2 = Bw * n = {0} * n", bw);
                //sw.WriteLine("d2 = n/2");




                double a3 = 1;
                //a3 = (Bb * Db)/100.0;
                //sw.WriteLine();
                //Chiranjit [2011 07 08]
                //For Checking
                //sw.WriteLine("a3 =  Bb * Db;");
                //sw.WriteLine("   = {0} * {1}", Bb/10.0, Db/10.0);
                //sw.WriteLine("   = {0} sq.cm", a3);
                //sw.WriteLine();
                double _d3 = 1;
                //double _d3 = (D - (Db / 2)) / 10.0;
                //sw.WriteLine("d3 = D-n-Db/2 = {0} - n - {1}/2 = {2:f2} - n", D/10.0, Db/10.0, _d3);

                double a4 = 1;

                //double modular_ratio = 10.0;
                //double Ast = 19302.0;

                a4 = modular_ratio * (Ast);

                //sw.WriteLine();
                //sw.WriteLine("a4 = m * Ast");
                //sw.WriteLine("   = {0} * {1}", modular_ratio, Ast);
                //sw.WriteLine("   = {0} sq.mm", a4);
                //sw.WriteLine();
                //sw.WriteLine("d4 = deff - n = {0:f2} - n", eff_depth);



                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("Now from equation");
                //sw.WriteLine();
                //Chiranjit [2011 07 08]
                //sw.WriteLine("a1 * d1 + a2 * d2   =   a3 * d3 + a4 * d4");
                //sw.WriteLine("a1 * d1 + a2 * d2   =   a4 * d4");
                //sw.WriteLine();
                //Chiranjit [2011 07 08]
                //sw.WriteLine("{0:f3} * (n - {1:f3}) + ({2:f3} * n) * (n/2)   =  {3:f3} * ({4:f3} - n) + {5:f3} * ({6:f3} - n)",
                //    a1, _d1, bw / 10.0, a3, _d3, a4, eff_depth / 10.0);
                //sw.WriteLine("{0} * (n - {1}) + ({2} * n) * (n/2)   =  {3} * ({4} - n)",
                //    a1, _d1, bw, a4, eff_depth);
                //sw.WriteLine();
                //sw.WriteLine();

                double _v1, _v2, _v3, _v4, _v5, _v6, _v7;

                _v1 = a1;
                _v2 = (Ds / 20.0) * a1;
                _v3 = (Bw / 20.0);
                //Chiranjit [2011 07 08]
                //_v4 = a3 * _d3;
                //_v5 = a3;
                _v6 = a4 * eff_depth;
                _v7 = a4;

                //Chiranjit [2011 07 08]
                //sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n + {5} - {6} * n",
                //    _v1, _v2, _v3, _v4, _v5, _v6, _v7);
                //sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n",
                //    _v1, _v2, _v3, _v6, _v7);
                //sw.WriteLine();


                //Chiranjit [2011 07 08]
                //sw.WriteLine("{0} * n*n + {1}*n + {2}*n + {3}*n = {4} + {5} + {6}",
                //    _v3, _v1, _v5, _v7, _v2, _v4, _v6);
                //sw.WriteLine("{0} * n*n + {1}*n + {2}*n = {3} + {4}",
                //    _v3, _v1, _v7, _v2, _v6);

                double _a, _b, _c;
                //Chiranjit [2011 07 08]
                //_b = ((a1 + a5) / a3);
                //_c = ((a1 * a2 + a4) / a3);

                _a = _v3;
                //Chiranjit [2011 07 08]
                //_b = (_v1 + _v5 + _v7);
                //_c = (_v2 + _v4 + _v6);

                _b = (_v1 + _v7);
                _c = (_v2 + _v6);

                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("    {0} * n*n + {1:f4}*n  = {2} ", _a, _b, _c);
                //sw.WriteLine();
                _b = _b / _a;
                _c = (_c / _a);
                //sw.WriteLine("or,   n*n + {0:f0}*n  - {1:f0} = 0", _b, _c);
                //sw.WriteLine();

                //sw.WriteLine();

                double n2;

                double root_a;
                root_a = Math.Sqrt((_b * _b + 4 * _c));

                n2 = (root_a - _b) / 2;
                //sw.WriteLine("n = {0:f4} mm = {1:f4} m = Depth of Neutral Axis from Top Edge", n, (n / 1000.0));
                return n2;

            }

        }
        #endregion Chiranjit [2013 06 28]


        public double Area_in_Sq_m
        {
            get
            {
                return (Ax / Math.Pow(10.0, 6.0));
            }
        }
        public double Ixx_in_Sq_Sq_m
        {
            get
            {
                return (Ixx / Math.Pow(10.0, 12.0));
            }
        }
        public double Iyy_in_Sq_Sq_m
        {
            get
            {
                return (Iyy / Math.Pow(10.0, 12.0));
            }
        }
        public double Izz_in_Sq_Sq_m
        {
            get
            {
                return (Izz / Math.Pow(10.0, 12.0));
            }
        }
    }
    public class TCross_SectionProperties
    {
        public TCross_SectionProperties()
        {
            D = 0.0;
            b = 0.0;
        }
        public double D { get; set; }
        public double b { get; set; }

        public double Ixx
        {
            get
            {
                return (b * D * D * D  / 12.0);
            }
        }

        public double Iyy
        {
            get
            {
                return (D * b * b * b/ 12.0);
            }
        }
        public double Izz
        {
            get
            {
                return (Ixx + Iyy);
            }
        }

        public double Ax
        {
            get
            {
                return (b * D);
            }
        }

        public double Area_in_Sq_m
        {
            get
            {
                return (Ax / Math.Pow(10.0, 6.0));
            }
        }
        public double Ixx_in_Sq_Sq_m
        {
            get
            {
                return (Ixx / Math.Pow(10.0, 12.0));
            }
        }
        public double Iyy_in_Sq_Sq_m
        {
            get
            {
                return (Iyy / Math.Pow(10.0, 12.0));
            }
        }
        public double Izz_in_Sq_Sq_m
        {
            get
            {
                return (Izz / Math.Pow(10.0, 12.0));
            }
        }
        
        public List<string> Results()
        {
            List<string> list = new List<string>();
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Depth of Cross Girders [D] = {0} mm", D));
            list.Add(string.Format(""));
            list.Add(string.Format("Web thickness of Cross Girders [b] = {0} mm", b));
            list.Add(string.Format(""));
            list.Add(string.Format("Ixx = b x D^3 / 12 = {0} x {1}^3 / 12 = {2:E3} sq.sq.mm. = {3:f4} sq.sq.m.", b, D, Ixx, Ixx_in_Sq_Sq_m));
            list.Add(string.Format(""));
            list.Add(string.Format("Iyy = D x b^3 / 12 = {0} x {1}^3 / 12 = {2:E3} sq.sq.mm. = {3:f4} sq.sq.m.", D, b, Iyy, Iyy_in_Sq_Sq_m));
            list.Add(string.Format(""));
            list.Add(string.Format("Izz = Ixx + Iyy = {0:E3} + {1:E3} =  {2:E3} sq.sq.mm. = {3:f4} sq.sq.m.", Ixx, Iyy, Izz, Izz_in_Sq_Sq_m));
            list.Add(string.Format(""));
            list.Add(string.Format("Area = Ax = b x D = {0} x {1} = {2} sq.mm. = {3} sq.m.", b, D, Ax, Area_in_Sq_m));
            list.Add(string.Format(""));
            return list;
        }
    }
    //Chiranjit [2013 07 12] Long Girder Limit
    
}
//Chiranjit [2013 07 22] Add Working Stress Folder
//Chiranjit [2013 07 11] Add 2 new tab as Working Stress Design and Limit State Design
// Chiranjit [2011 09 23] Correct Create Data
//Chiranjit [2011 10 29] 
//Chiranjit [2013 01 01]
