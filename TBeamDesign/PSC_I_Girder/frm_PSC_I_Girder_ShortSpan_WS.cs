using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;

namespace BridgeAnalysisDesign.PSC_I_Girder
{
    public partial class frm_PSC_I_Girder_ShortSpan_WS : Form
    { 
        //const string Title = "ANALYSIS OF PSC I-GIRDER BRIDGE SHORT SPAN";

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "PSC I-GIRDER BRIDGE WORKING STRESS SHORT SPAN [BS]";
                return "PSC I-GIRDER BRIDGE WORKING STRESS SHORT SPAN [IRC]";
            }
        }


        PSC_I_Girder_Short_Analysis_WS Deck_Analysis_DL = null;
        PSC_I_Girder_Short_Analysis_WS Deck_Analysis_LL = null;

        PSC_I_Girder_Short_Analysis_WS Bridge_Analysis = null;
        PreStressedConcrete_Forces PSC_Shear_Forces;
        PreStressedConcrete_Forces PSC_Moment_Forces;

        //Chiranjit [2012 06 22]
        CantileverSlab Cant = null;

        //Chiranjit [2012 06 13]
        RccPier rcc_pier = null;

        //Chiranjit [2012 06 13]
        RCC_AbutmentWall Abut = null;

        //PostTensionLongGirder LongGirder = null;
        PostTensionLongGirder_ShortSpan LongGirder = null;
        RccDeckSlab Deck = null;

        IApplication iApp = null;

        //Chiranjit [2012 07 31]

        PreStressedConcrete_SectionProperties sec_1 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_2 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_3 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_4 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_5 = new PreStressedConcrete_SectionProperties();
        PSC_Cable_Data pcd;
        PSC_Reinforcement_Data prd;
        PSC_EndCrossGirder_Data ecgd;


        Save_FormRecord SaveRec = new Save_FormRecord();

        bool IsCreate_Data = true;
        public List<string> Results { get; set; }
        public frm_PSC_I_Girder_ShortSpan_WS(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            Results = new List<string>();
        }
        public string Worksheet_Folder
        {
            get
            {
                if(user_path != iApp.LastDesignWorkingFolder)
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

        public string Input_File_LL
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    if (Path.GetFileName(user_path) != "Live Load Analysis")
                        if (!Directory.Exists(Path.Combine(user_path, "Live Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(user_path, "Live Load Analysis"));



                    return Path.Combine(Path.Combine(user_path, "Live Load Analysis"), "Input_Data_LL.txt");
                    //return Path.Combine(user_path, "Input_Data_LL.txt");
                }
                return "";
            }
        }
        public string Input_File_DL
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    if (Path.GetFileName(user_path) != "Dead Load Analysis")
                        if (!Directory.Exists(Path.Combine(user_path, "Dead Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(user_path, "Dead Load Analysis"));


                    return Path.Combine(Path.Combine(user_path, "Dead Load Analysis"), "Input_Data_DL.txt");
                    //return Path.Combine(user_path, "Input_Data_DL.txt");
                }

                //if (Directory.Exists(user_path))
                //    return Path.Combine(user_path, "Input_Data_DL.txt");
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

            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 1.5, 0.2);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 4.5, 0.2);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 7.5, 0.2);
        }

        public void Open_Create_Data()
        {
            try
            {
                Analysis_Initialize_InputData();

                PSC_Section_Properties(true);

                Bridge_Analysis.PSC_Mid_Span = sec_1;
                Bridge_Analysis.PSC_End = sec_2;
                Bridge_Analysis.PSC_Cross = sec_3;

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;
                //Bridge_Analysis.CreateData(true);
                Bridge_Analysis.CreateData();


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);


                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File, false);
                txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File, false);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, false);
                Deck_Analysis_LL.Input_File = Bridge_Analysis.LiveLoadAnalysis_Input_File;

                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File, false);
                Deck_Analysis_DL.Input_File = Bridge_Analysis.DeadLoadAnalysis_Input_File;

                Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);

                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                PSC_Section_Properties();


                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                Analysis_Initialize_InputData();
                //if (IsCreate_Data)
                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);

                //Calculate_Load_Computation();
                PSC_Section_Properties(true);

                Bridge_Analysis.PSC_Mid_Span = sec_1;
                Bridge_Analysis.PSC_End = sec_2;
                Bridge_Analysis.PSC_Cross = sec_3;

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;

                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;
                //Bridge_Analysis.CreateData(true);
                Bridge_Analysis.CreateData();


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);


                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File, false);
                txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File, false);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, false);
                Deck_Analysis_LL.Input_File = Bridge_Analysis.LiveLoadAnalysis_Input_File;

                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File, false);
                Deck_Analysis_DL.Input_File = Bridge_Analysis.DeadLoadAnalysis_Input_File;

                Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);

                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                //cmb_Ana_load_type.Items.Clear();
                //for (int i = 0; i < Bridge_Analysis.Live_Load_List.Count; i++)
                //{
                //    cmb_Ana_load_type.Items.Add(Bridge_Analysis.Live_Load_List[i].TypeNo + " : " + Bridge_Analysis.Live_Load_List[i].Code);
                //}
                //if (cmb_Ana_load_type.Items.Count > 0)
                //{
                //    cmb_Ana_load_type.SelectedIndex = cmb_Ana_load_type.Items.Count - 1;
                //}
                Button_Enable_Disable();
                Write_All_Data();


                PSC_Section_Properties();


               MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
                "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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

        private void btn_Ana_view_report_Click(object sender, EventArgs e)
        {
            //frm_Result_Option frm = new frm_Result_Option(true);

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    //if (((Button)sender).Name == btn_Ana_DL_view_report.Name)
            //    //{
            //    //    if (frm.Is_Full_Analysis_Report)
            //    //        iApp.RunExe(Analysis_Report_DL);
            //    //    else
            //    //        iApp.RunExe(Result_Report_DL);
            //    //}
            //    //else
            //    {
            //        if (frm.Is_Full_Analysis_Report)
            //            iApp.RunExe(Analysis_Report_LL);
            //        else
            //            iApp.RunExe(Result_Report_LL);
            //    }
            //}

            frm_Result_Option frm = new frm_Result_Option(true);

            frm.rbtn_ana_res.Visible = false;
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
            if (((Button)sender).Name == btn_view_data.Name)
            {
                //iApp.RunExe(Path.Combine(user_path, "LL.txt"));
                iApp.View_Input_File(Bridge_Analysis.TotalAnalysis_Input_File);
            }
            else
                iApp.RunExe(Deck_Analysis_LL.Input_File);
        }

        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name == btn_view_structure.Name)
            {
                if (File.Exists(Bridge_Analysis.LiveLoadAnalysis_Input_File))
                    iApp.OpenWork(Bridge_Analysis.LiveLoadAnalysis_Input_File, false);
            }
            else
            {
                if (File.Exists(Deck_Analysis_LL.Input_File))
                    iApp.OpenWork(Deck_Analysis_LL.Input_File, false);
            }
        }
        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
            #region Process
            //Chiranjit [2012 07 13]
            Write_All_Data(true);
            int i = 0;
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


                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);


                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");

                i++;
            }
            while (i < 3);

            //string ana_rep_file = Bridge_Analysis.Analysis_Report;
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (iApp.Show_and_Run_Process_List(pcol))
            {


                //List<string> Work_List = new List<string>();

                //Work_List.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                ////Work_List.Add("Set Structure Geometry for Total Load Analysis");
                ////Work_List.Add("Reading Bending Moment & Shear Force from Total Load Analysis Result");

                //Work_List.Add("Reading Analysis Data from Dead Load Analysis Report File");
                ////Work_List.Add("Set Structure Geometry for Dead Load Analysis");
                ////Work_List.Add("Reading Bending Moment & Shear Force from Dead Load Analysis Result");


                //Work_List.Add("Reading Analysis Data from Live Load Analysis Report File");
                ////Work_List.Add("Set Structure Geometry for Live Load Analysis");
                ////Work_List.Add("Reading Bending Moment & Shear Force from Live Load Analysis Result");


                ////Work_List.Add("Reading support reaction forces from Total Load Analysis Report");
                ////Work_List.Add("Reading support reaction forces from Live Load Analysis Report");
                ////Work_List.Add("Reading support reaction forces from Dead Load Analysis Report");


                //iApp.Progress_Works = new ProgressList(Work_List);

                Bridge_Analysis.Structure = null;
                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);
                Deck_Analysis_LL.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.LiveLoad_Analysis_Report);
                Show_Moment_Shear_DL();
                Show_Moment_Shear_LL();

                string s1 = "";
                string s2 = "";
                try
                {
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
                }
                catch (Exception ex) { }
                //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
                double BB = B;


                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                frm_ViewDesign_Forces_Load();






                //Chiranjit [2012 06 22]
                txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
                txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



                txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
                txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

                //txt_abut_w6.Text = Total_LiveLoad_Reaction;
                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                //txt_abut_w6.ForeColor = Color.Red;

                //txt_abut_w5.Text = Total_DeadLoad_Reaction;
                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                //txt_abut_w5.ForeColor = Color.Red;



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

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;


            //Deck_Load_Analysis_Data();
            Deck_Initialize_InputData();
            Button_Enable_Disable();

            Calculate_Interactive_Values();

            Button_Enable_Disable();
            Write_All_Data(false);
            #endregion Process

            //Chiranjit [2013 04 26]
            iApp.Save_Form_Record(this, user_path);

            iApp.Progress_Works.Clear();

        }

        private void btn_Ana_DL_process_analysis_Click(object sender, EventArgs e)
        {
            string flPath = Deck_Analysis_DL.Input_File;
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (prs.Start())
                prs.WaitForExit();
            //if (File.Exists(Deck_Analysis_DL.Analysis_Report))
            //    File.Copy(Deck_Analysis_DL.Analysis_Report, Analysis_Report_DL, true);


            string ana_rep_file = File.Exists(Deck_Analysis_DL.Analysis_Report) ? Deck_Analysis_DL.Analysis_Report : Analysis_Report_DL;
            if (File.Exists(ana_rep_file))
            {
                Deck_Analysis_DL.Structure = null;
                Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, ana_rep_file);
                Show_Moment_Shear_DL();
            }

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_ana_create_analysis_file.Checked;

      
            Button_Enable_Disable();
            Show_Analysis_Result();
        }
        private void btn_Ana_LL_process_analysis_Click(object sender, EventArgs e)
        {

            string flPath = Deck_Analysis_LL.Input_File;

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (prs.Start())
                prs.WaitForExit();
            //if (File.Exists(Deck_Analysis_DL.Analysis_Report))
            //    File.Copy(Deck_Analysis_DL.Analysis_Report, Analysis_Report_LL, true);



            string ana_rep_file = File.Exists(Deck_Analysis_LL.Analysis_Report) ? Deck_Analysis_LL.Analysis_Report : Analysis_Report_LL;
            //string ana_rep_file = Deck_Analysis_LL.Analysis_Report;
            if (File.Exists(ana_rep_file))
            {
                Deck_Analysis_LL.Structure = null;
                Deck_Analysis_LL.Structure = new BridgeMemberAnalysis(iApp, ana_rep_file);
                Show_Moment_Shear_LL();
            }

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            //grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            Button_Enable_Disable();
            Show_Analysis_Result();
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
            //load_lst.Add("MEMBER LOAD");
            if (add_DeadLoad)
            {
                load_lst.AddRange(txt_member_load.Lines);

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

        private void Write_Ana_Load_Data()
        {
            string file_name = Bridge_Analysis.Input_File;

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
            s += (chk_ana_active_SIDL.Checked ? " + SIDL " : "");
            s += (chk_ana_active_LL.Checked ? " + LL " : "");

            load_lst.Add("LOAD    1   " + s);
            load_lst.Add("MEMBER LOAD");
            if (chk_ana_active_SIDL.Checked)
            {
                load_lst.AddRange(txt_member_load.Lines);

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Deck_Analysis_DL.LiveLoad_File))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                    }

                    Deck_Analysis_DL.LoadReadFromGrid(dgv_live_load);
                }
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
            }
            load_lst.AddRange(Get_MovingLoad_Data(Deck_Analysis_DL.Live_Load_List));
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            s += (IsLiveLoad ? " + LL " : "");

            load_lst.Add("LOAD    1   " + s);
            load_lst.Add("MEMBER LOAD");
            if (!IsLiveLoad)
            {
                load_lst.AddRange(txt_member_load.Lines);

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Deck_Analysis_DL.LiveLoad_File))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                    }

                }
            }
            else
            {

                load_lst.Add("1 TO 220 UNI GY -0.001");

                Deck_Analysis_LL.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
                Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
                //Deck_Analysis_LL.LoadReadFromGrid(dgv_Ana_DL_live_load);

                if (dgv_live_load.RowCount != 0)
                    load_lst.AddRange(Get_MovingLoad_Data(Deck_Analysis_LL.Live_Load_List));
                //inp_file_cont.InsertRange(indx, );
            }
            inp_file_cont.InsertRange(indx, load_lst);
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;
            Button_Enable_Disable();
        }
        private void txt_Ana_custom_LL_Xcrmt_TextChanged(object sender, EventArgs e)
        {
        }
        public void Show_ReadMemberLoad_OLD(string file_name)
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
                    continue;
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
                            ld.X.ToString("0.000"), ld.Y.ToString("0.000"), ld.Z.ToString("0.000"), ld.XINC.ToString("0.000"));
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
            list_member_load.Remove("MEMBER LOAD");

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

        
        public void Show_LoadGeneration(string file_name)
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
                    continue;
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
                ofd.InitialDirectory =  Analysis_Path;
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    IsCreate_Data = false;
                    string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");

                    if (!File.Exists(chk_file)) chk_file = ofd.FileName;

                    Open_AnalysisFile(chk_file);

                    LongGirder.FilePath = user_path;
                    Deck.FilePath = user_path;
                    Cant.FilePath = user_path;
                    Abut.FilePath = user_path;
                    rcc_pier.FilePath = user_path;
                    txt_Ana_analysis_file.Text = ofd.FileName;
                    if (File.Exists(Deck_Analysis_DL.Input_File))
                    {
                        Show_ReadMemberLoad(Deck_Analysis_DL.Input_File);
                    }
                    if (File.Exists(Deck_Analysis_LL.Input_File))
                    {

                        Show_LoadGeneration(Deck_Analysis_LL.Input_File);
                    }
                    Show_Forces();
                    iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);


                    //Chiranjit [2012 07 13]

                    //Chiranjit [2013 04 26]
                    iApp.Read_Form_Record(this, user_path);
                    rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                    Open_Create_Data();//Chiranjit [2013 06 25]
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            Button_Enable_Disable();
            grb_create_input_data.Enabled = true;
        }

        private void dgv_Ana_SIDL_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
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
            if (File.Exists(Deck_Analysis_LL.Input_File))
                iApp.OpenWork(Deck_Analysis_LL.Input_File, true);
        }


        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            //grb_Ana_DL_LL.Enabled = chk_ana_active_LL.Checked;
            //grb_SIDL.Enabled = chk_Ana_DL_active_SIDL.Checked;
        }
        #endregion  Composite Analysis Form Events

        #region Deck Methods

        private void Create_Data_DL(string file_name)
        {

            Deck_Analysis_DL.Input_File = file_name;
            Deck_Analysis_DL.CreateData();
            Deck_Analysis_DL.WriteData(Deck_Analysis_DL.Input_File);
            Write_Ana_Load_Data(false);
            Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, Deck_Analysis_DL.Input_File);

            string ll_txt = Deck_Analysis_DL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_DL.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();
            //for (int i = 0; i < Deck_Analysis_DL.Live_Load_List.Count; i++)
            //{
            //    cmb_Ana_load_type.Items.Add(Deck_Analysis_DL.Live_Load_List[i].TypeNo + " : " + Deck_Analysis_DL.Live_Load_List[i].Code);
            //}
            //if (cmb_Ana_load_type.Items.Count > 0)
            //{
            //    cmb_Ana_load_type.SelectedIndex = cmb_Ana_load_type.Items.Count - 1;
            //    //if (dgv_live_load.RowCount == 0)
            //    //Add_LiveLoad();
            //}
            Button_Enable_Disable();
        }
        private void Create_Data_LL(string file_name)
        {


            Deck_Analysis_LL.Input_File = Input_File_LL;
            Deck_Analysis_LL.CreateData();
            Deck_Analysis_LL.WriteData(Deck_Analysis_LL.Input_File);
            Write_Ana_Load_Data(true);
            Deck_Analysis_LL.Structure = new BridgeMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();
            //for (int i = 0; i < Deck_Analysis_LL.Live_Load_List.Count; i++)
            //{
            //    cmb_Ana_load_type.Items.Add(Deck_Analysis_LL.Live_Load_List[i].TypeNo + " : " + Deck_Analysis_LL.Live_Load_List[i].Code);
            //}
            //if (cmb_Ana_load_type.Items.Count > 0)
            //{
            //    cmb_Ana_load_type.SelectedIndex = cmb_Ana_load_type.Items.Count - 1;
            //    //if (dgv_live_load.RowCount == 0)
            //    //Add_LiveLoad();
            //}
            Button_Enable_Disable();
        }


        void Analysis_Initialize_InputData()
        {
            Deck_Analysis_DL.Length = L;
            Deck_Analysis_DL.WidthBridge = B;
            Deck_Analysis_DL.Width_LeftCantilever = CL;
            Deck_Analysis_DL.Width_RightCantilever = CR;
            Deck_Analysis_DL.Skew_Angle = Ang;
            Deck_Analysis_DL.Effective_Depth = Deff;
            Deck_Analysis_DL.NMG = NMG;

            Deck_Analysis_LL.Length = L;
            Deck_Analysis_LL.WidthBridge = B;
            Deck_Analysis_LL.Width_LeftCantilever = CL;
            Deck_Analysis_LL.Width_RightCantilever = CR;
            Deck_Analysis_LL.Skew_Angle = Ang;
            Deck_Analysis_LL.Effective_Depth = Deff;
            Deck_Analysis_LL.NMG = NMG;

            Bridge_Analysis.Length = L;
            Bridge_Analysis.WidthBridge = B;
            Bridge_Analysis.Width_LeftCantilever = CL;
            Bridge_Analysis.Width_RightCantilever = CR;
            Bridge_Analysis.Skew_Angle = Ang;
            Bridge_Analysis.Effective_Depth = Deff;
            Bridge_Analysis.NMG = NMG;
            Bridge_Analysis.NCG = NCG;

        }

        void Show_Moment_Shear_LL()
        {
            double L = Deck_Analysis_LL.Structure.Analysis.Length;
            double W = Deck_Analysis_LL.Structure.Analysis.Width;
            double val = L / 2;
          
            List<int> _L2_inn_joints = Bridge_Analysis._L2_inn_joints;
            List<int> _L4_inn_joints = Bridge_Analysis._L4_inn_joints;
            List<int> _deff_inn_joints = Bridge_Analysis._deff_inn_joints;

            List<int> _L2_out_joints = Bridge_Analysis._L2_out_joints;
            List<int> _L4_out_joints = Bridge_Analysis._L4_out_joints;
            List<int> _deff_out_joints = Bridge_Analysis._deff_out_joints;

            MaxForce mfrc = new MaxForce();

            Results.Clear();
            Results.Add("Analysis Result of Live Loads of Pre Stressed Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_inn_joints, true);
            txt_LL_IG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_inn_joints, true);
            txt_LL_IG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_inn_joints, true);
            txt_LL_IG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_inn_joints, "Ton"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_inn_joints, true);
            txt_LL_IG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_inn_joints, true);
            txt_LL_IG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_inn_joints, "Ton"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_deff_inn_joints, true);
            txt_LL_IG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_inn_joints, "Ton-m"));


             
            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_out_joints, true);
            txt_LL_OG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_out_joints, "Ton"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_out_joints, true);
            txt_LL_OG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_out_joints, "Ton-m"));








            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_out_joints, true);
            txt_LL_OG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_out_joints, true);
            txt_LL_OG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_out_joints, true);
            txt_LL_OG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_deff_out_joints, true);
            txt_LL_OG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));

            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_DL()
        {
            //Chiranjit [2012 07 26]
     //       try
     //       {
     //           Show_Moment_Shear_DL(true);
     //       }
     //       catch (Exception ex) { }
     //return;

            //MemberCollection mc = new MemberCollection(Deck_Analysis_DL.Truss_Analysis.Analysis.Members);

            //MemberCollection sort_membs = new MemberCollection();

            //JointNodeCollection jn_col = Deck_Analysis_DL.Truss_Analysis.Analysis.Joints;

            double L = Deck_Analysis_DL.Structure.Analysis.Length;
            double W = Deck_Analysis_DL.Structure.Analysis.Width;
            
            
            
            double val = L / 2;
            int i = 0;

            List<int> _L2_inn_joints = Bridge_Analysis._L2_inn_joints;
            List<int> _L4_inn_joints = Bridge_Analysis._L4_inn_joints;
            List<int> _deff_inn_joints = Bridge_Analysis._deff_inn_joints;

            List<int> _L2_out_joints = Bridge_Analysis._L2_out_joints;
            List<int> _L4_out_joints = Bridge_Analysis._L4_out_joints;
            List<int> _deff_out_joints = Bridge_Analysis._deff_out_joints;

            MaxForce mfrc = new MaxForce();

            Results.Clear();
            Results.Add("");
            Results.Add("Analysis Result of Dead Loads of Pre Stressed Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints, true);
            txt_DL_IG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints, true);
            txt_DL_IG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_inn_joints, true);
            txt_DL_IG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_inn_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints, true);
            txt_DL_IG_M2.Text = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints).ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_inn_joints, "Ton-m"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_inn_joints, true);
            txt_DL_IG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_inn_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_inn_joints, true);
            txt_DL_IG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_inn_joints, "Ton-m"));

            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_out_joints, true);
            txt_DL_OG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_out_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_out_joints, true);
            txt_DL_OG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_out_joints, "Ton-m"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints, true);
            txt_DL_OG_S2.Text = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints).ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints, true);
            txt_DL_OG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_out_joints, true);
            txt_DL_OG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_out_joints, true);
            txt_DL_OG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));

            File.WriteAllLines(Result_Report_DL, Results.ToArray());
            #region Null All variables
            //mc = null;


            //jn_col = null;


            //_L2_inn_joints = null;
            //_L4_inn_joints = null;
            //_deff_inn_joints = null;

            //_L4_out_joints = null;
            //_deff_out_joints = null;
            #endregion

        }
        void Show_Moment_Shear_DL(bool is_PSC_I_Girder)
        {

            MemberCollection mc = new MemberCollection(Deck_Analysis_DL.Structure.Analysis.Members);
            Deck_Analysis_DL.Length = Deck_Analysis_DL.Structure.Analysis.Length;

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis_DL.Structure.Analysis.Joints;

            double L = Deck_Analysis_DL.Structure.Analysis.Length;
            double W = Deck_Analysis_DL.Structure.Analysis.Width;
            double val = L / 2;
            int i = 0;

            List<int> _L1_inn_joints = new List<int>(); //0
            List<int> _L2_inn_joints = new List<int>(); //Effective Depth
            List<int> _L3_inn_joints = new List<int>(); //  L / 5.3
            List<int> _L4_inn_joints = new List<int>(); //  L / 3.65
            List<int> _L5_inn_joints = new List<int>(); //  L / 3.05
            List<int> _L6_inn_joints = new List<int>();//  L / 2.61
            List<int> _L7_inn_joints = new List<int>();//  L / 2.28
            List<int> _L8_inn_joints = new List<int>();//  L / 2.03
            List<int> _L9_inn_joints = new List<int>();//  L / 2.

            List<int> _L1_out_joints = new List<int>();
            List<int> _L2_out_joints = new List<int>();
            List<int> _L3_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _L5_out_joints = new List<int>();
            List<int> _L6_out_joints = new List<int>();
            List<int> _L7_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L9_out_joints = new List<int>();

            List<int> _deff_inn_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();


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
            val = Deck_Analysis_DL.Structure.Analysis.Effective_Depth;
            if (_X_joints.Contains(val))
            {
                Deck_Analysis_DL.Effective_Depth = val;
            }
            else
            {
                Deck_Analysis_DL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }
            //double eff_dep = ;

            //_L_2_joints.Clear();

            double cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.Width_LeftCantilever = cant_wi;


            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L1).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L1_inn_joints.Add(jn_col[i].NodeNo);

                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L1_out_joints.Add(jn_col[i].NodeNo);

                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L1_inn_joints.Add(jn_col[i].NodeNo);

                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L1_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L2).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);

                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L2_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L2).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L2_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L3).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L3_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L3_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L3).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L3_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L3_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L4).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L4).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L5).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L5_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L5_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L5).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L5_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L5_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L6).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L6_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L6_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L6).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L6_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L6_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L7).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L7_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L7_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L7).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L7_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L7_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L8).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L8).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L9).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L9_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L9_out_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }

            MaxForce mfrc = new MaxForce();


            Results.Clear();
            PSC_Shear_Forces = new PreStressedConcrete_Forces();
            //Inner Girder ShearForce
            PSC_Shear_Forces.DL_INNER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L1_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L3_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L5_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L6_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L7_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L8_inn_joints).Force;
            PSC_Shear_Forces.DL_INNER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L9_inn_joints).Force;


            //Outer Girder ShearForce
            PSC_Shear_Forces.DL_OUTER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L1_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L3_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L5_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L6_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L7_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L8_out_joints).Force;
            PSC_Shear_Forces.DL_OUTER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L9_out_joints).Force;


            txt_DL_IG_S1.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F1.ToString("E2");
            txt_DL_IG_S2.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F2.ToString("E2");
            txt_DL_IG_S3.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F3.ToString("E2");
            //txt_DL_IG_S4.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F4.ToString("E2");
            //txt_DL_IG_S5.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F5.ToString("E2");
            //txt_DL_IG_S6.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F6.ToString("E2");
            //txt_DL_IG_S7.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F7.ToString("E2");
            //txt_DL_IG_S8.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F8.ToString("E2");
            //txt_DL_IG_S9.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F9.ToString("E2");

            txt_DL_OG_S1.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F1.ToString("E2");
            txt_DL_OG_S2.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F2.ToString("E2");
            txt_DL_OG_S3.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F3.ToString("E2");
            //txt_DL_OG_S4.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F4.ToString("E2");
            //txt_DL_OG_S5.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F5.ToString("E2");
            //txt_DL_OG_S6.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F6.ToString("E2");
            //txt_DL_OG_S7.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F7.ToString("E2");
            //txt_DL_OG_S8.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F8.ToString("E2");
            //txt_DL_OG_S9.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F9.ToString("E2");







            //Inner Girder Moment Forces
            PSC_Moment_Forces = new PreStressedConcrete_Forces();
            PSC_Moment_Forces.DL_INNER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L3_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L5_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L6_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L7_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L8_inn_joints).Force;
            PSC_Moment_Forces.DL_INNER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L9_inn_joints).Force;


            //Outer Girder Moment Forces
            PSC_Moment_Forces.DL_OUTER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L3_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L5_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L6_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L7_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L8_out_joints).Force;
            PSC_Moment_Forces.DL_OUTER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L9_out_joints).Force;


            txt_DL_IG_M1.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F1.ToString("E2");
            txt_DL_IG_M2.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F2.ToString("E2");
            txt_DL_IG_M3.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F3.ToString("E2");
            //txt_DL_IG_M4.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F4.ToString("E2");
            //txt_DL_IG_M5.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F5.ToString("E2");
            //txt_DL_IG_M6.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F6.ToString("E2");
            //txt_DL_IG_M7.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F7.ToString("E2");
            //txt_DL_IG_M8.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F8.ToString("E2");
            //txt_DL_IG_M9.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F9.ToString("E2");



            txt_DL_OG_M1.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F1.ToString("E2");
            txt_DL_OG_M2.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F2.ToString("E2");
            txt_DL_OG_M3.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F3.ToString("E2");
            //txt_DL_OG_M4.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F4.ToString("E2");
            //txt_DL_OG_M5.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F5.ToString("E2");
            //txt_DL_OG_M6.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F6.ToString("E2");
            //txt_DL_OG_M7.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F7.ToString("E2");
            //txt_DL_OG_M8.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F8.ToString("E2");
            //txt_DL_OG_M9.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F9.ToString("E2");





            Results.Add("");

            return;
            Results.Add("Analysis Result of Dead Loads of Pre Stressed Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_DL_IG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));




            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_DL_IG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_inn_joints);
            txt_DL_IG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_inn_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints);
            txt_DL_IG_M2.Text = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints).ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_inn_joints, "Ton-m"));





            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_inn_joints);
            txt_DL_IG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_inn_joints, "Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_inn_joints);
            txt_DL_IG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_inn_joints, "Ton-m"));
            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_DL_OG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_DL_OG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints);
            txt_DL_OG_S2.Text = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints).ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints);
            txt_DL_OG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_out_joints);
            txt_DL_OG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_out_joints);
            txt_DL_OG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));



            File.WriteAllLines(Result_Report_DL, Results.ToArray());
            //iApp.RunExe(Result_Report_DL);

            #region Null All variables
            mc = null;


            jn_col = null;


            _L2_inn_joints = null;
            _L4_inn_joints = null;
            _deff_inn_joints = null;

            _L4_out_joints = null;
            _deff_out_joints = null;
            #endregion

        }
        void Show_Moment_Shear_LL(bool is_PSC_I_Girder)
        {

            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Structure.Analysis.Members);
            Deck_Analysis_DL.Length = Deck_Analysis_DL.Structure.Analysis.Length;

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis_DL.Structure.Analysis.Joints;

            double L = Deck_Analysis_DL.Structure.Analysis.Length;
            double W = Deck_Analysis_DL.Structure.Analysis.Width;
            double val = L / 2;
            int i = 0;

            List<int> _L1_inn_joints = new List<int>(); //0
            List<int> _L2_inn_joints = new List<int>(); //Effective Depth
            List<int> _L3_inn_joints = new List<int>(); //  L / 5.3
            List<int> _L4_inn_joints = new List<int>(); //  L / 3.65
            List<int> _L5_inn_joints = new List<int>(); //  L / 3.05
            List<int> _L6_inn_joints = new List<int>();//  L / 2.61
            List<int> _L7_inn_joints = new List<int>();//  L / 2.28
            List<int> _L8_inn_joints = new List<int>();//  L / 2.03
            List<int> _L9_inn_joints = new List<int>();//  L / 2.

            List<int> _L1_out_joints = new List<int>();
            List<int> _L2_out_joints = new List<int>();
            List<int> _L3_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _L5_out_joints = new List<int>();
            List<int> _L6_out_joints = new List<int>();
            List<int> _L7_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L9_out_joints = new List<int>();

            List<int> _deff_inn_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();


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
            val = Deck_Analysis_DL.Structure.Analysis.Effective_Depth;
            if (_X_joints.Contains(val))
            {
                Deck_Analysis_DL.Effective_Depth = val;
            }
            else
            {
                Deck_Analysis_DL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }
            //double eff_dep = ;

            //_L_2_joints.Clear();

            double cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.Width_LeftCantilever = cant_wi;


            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L1).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L1_inn_joints.Add(jn_col[i].NodeNo);

                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L1_out_joints.Add(jn_col[i].NodeNo);

                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L1_inn_joints.Add(jn_col[i].NodeNo);

                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L1_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L2).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);

                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L2_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L2).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L2_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L3).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L3_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L3_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L3).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L3_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L3_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L4).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L4).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L5).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L5_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L5_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L5).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L5_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L5_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L6).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L6_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi && jn_col[i].Z == (W - cant_wi))
                            _L6_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L6).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L6_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L6_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L7).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L7_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L7_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L7).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L7_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L7_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.L8).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L8).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    else if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Length - Deck_Analysis_DL.L9).ToString("0.0")))
                    {
                        if (jn_col[i].Z > cant_wi && jn_col[i].Z < (W - cant_wi))
                            _L9_inn_joints.Add(jn_col[i].NodeNo);
                        if (jn_col[i].Z == cant_wi || jn_col[i].Z == (W - cant_wi))
                            _L9_out_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }

            MaxForce mfrc = new MaxForce();


            Results.Clear();

            //Inner Girder ShearForce
            PSC_Shear_Forces.LL_INNER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L1_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L3_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L5_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L6_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L7_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L8_inn_joints).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L9_inn_joints).Force;


            //Outer Girder ShearForce
            PSC_Shear_Forces.LL_OUTER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L1_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L3_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L5_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L6_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L7_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L8_out_joints).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L9_out_joints).Force;


            txt_LL_IG_S1.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F1.ToString("E2");
            txt_LL_IG_S2.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F2.ToString("E2");
            txt_LL_IG_S3.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F3.ToString("E2");
            //txt_LL_IG_S4.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F4.ToString("E2");
            //txt_LL_IG_S5.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F5.ToString("E2");
            //txt_LL_IG_S6.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F6.ToString("E2");
            //txt_LL_IG_S7.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F7.ToString("E2");
            //txt_LL_IG_S8.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F8.ToString("E2");
            //txt_LL_IG_S9.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F9.ToString("E2");

            txt_LL_OG_S1.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F1.ToString("E2");
            txt_LL_OG_S2.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F2.ToString("E2");
            txt_LL_OG_S3.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F3.ToString("E2");
            //txt_LL_OG_S4.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F4.ToString("E2");
            //txt_LL_OG_S5.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F5.ToString("E2");
            //txt_LL_OG_S6.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F6.ToString("E2");
            //txt_LL_OG_S7.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F7.ToString("E2");
            //txt_LL_OG_S8.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F8.ToString("E2");
            //txt_LL_OG_S9.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F9.ToString("E2");







            //Inner Girder Moment Forces
            PSC_Moment_Forces.LL_INNER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L1_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L1_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L3_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L5_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L6_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L7_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L8_inn_joints).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L9_inn_joints).Force;


            //Outer Girder Moment Forces
            PSC_Moment_Forces.LL_OUTER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L1_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L3_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L5_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L6_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L7_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L8_out_joints).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L9_out_joints).Force;


            txt_LL_IG_M1.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F1.ToString("E2");
            txt_LL_IG_M2.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F2.ToString("E2");
            txt_LL_IG_M3.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F3.ToString("E2");
            //txt_LL_IG_M4.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F4.ToString("E2");
            //txt_LL_IG_M5.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F5.ToString("E2");
            //txt_LL_IG_M6.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F6.ToString("E2");
            //txt_LL_IG_M7.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F7.ToString("E2");
            //txt_LL_IG_M8.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F8.ToString("E2");
            //txt_LL_IG_M9.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F9.ToString("E2");



            txt_LL_OG_M1.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F1.ToString("E2");
            txt_LL_OG_M2.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F2.ToString("E2");
            txt_LL_OG_M3.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F3.ToString("E2");
            //txt_LL_OG_M4.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F4.ToString("E2");
            //txt_LL_OG_M5.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F5.ToString("E2");
            //txt_LL_OG_M6.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F6.ToString("E2");
            //txt_LL_OG_M7.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F7.ToString("E2");
            //txt_LL_OG_M8.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F8.ToString("E2");
            //txt_LL_OG_M9.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F9.ToString("E2");





            Results.Add("");

            return;
            Results.Add("Analysis Result of Dead Loads of Pre Stressed Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_DL_IG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));




            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_DL_IG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_inn_joints);
            txt_DL_IG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_inn_joints, "Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints);
            txt_DL_IG_M2.Text = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints).ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_inn_joints, "Ton-m"));





            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_inn_joints);
            txt_DL_IG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_inn_joints, "Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_inn_joints);
            txt_DL_IG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_inn_joints, "Ton-m"));
            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_DL_OG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_DL_OG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints);
            txt_DL_OG_S2.Text = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints).ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints);
            txt_DL_OG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_out_joints);
            txt_DL_OG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_out_joints);
            txt_DL_OG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));



            File.WriteAllLines(Result_Report_DL, Results.ToArray());
            //iApp.RunExe(Result_Report_DL);

            #region Null All variables
            mc = null;


            jn_col = null;


            _L2_inn_joints = null;
            _L4_inn_joints = null;
            _deff_inn_joints = null;

            _L4_out_joints = null;
            _deff_out_joints = null;
            #endregion

        }


        void Show_Moment_Shear_LL_2011_10_29()
        {
            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Structure.Analysis.Members);
            //MemberCollection mc = new MemberCollection(Deck_Analysis_DL.Truss_Analysis.Analysis.Members);

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
            iApp.Progress_ON("Sorting Members...");
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
                iApp.SetProgressValue(sort_membs.Count, (sort_membs.Count + mc.Count));
            }
            iApp.Progress_OFF();



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
            iApp.Progress_ON("Finding Inner & Outer Girders");
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
                iApp.SetProgressValue(i, sort_membs.Count);
            }
            iApp.Progress_OFF();

            //Store Cross Girders
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (outer_long.Contains(sort_membs[i]) == false &&
                    inner_long.Contains(sort_membs[i]) == false)
                {
                    inner_cross.Add(sort_membs[i]);
                }
            }


            //Print
            //OUTER LONG GIRDER
            list_arr.Add("");
            list_arr.Add("OUTER LONG GIRDERS");
            list_arr.Add("");
            list_arr.Add("");
            for (j = 0; j < outer_long.Count; j++)
            {
                //mc = outer_long[i];
                list_arr.Add(string.Format("{0,-10} {1} {2}",
             outer_long[j].MemberNo, outer_long[j].StartNode, outer_long[j].EndNode));
            }

            list_arr.Add("");
            list_arr.Add("INNER LONG GIRDERS");
            list_arr.Add("");
            for (j = 0; j < inner_long.Count; j++)
            {
                //mc = inner_long[i];
                list_arr.Add(string.Format("{0,-10} {1} {2}",
             inner_long[j].MemberNo, inner_long[j].StartNode, inner_long[j].EndNode));
            }


            list_arr.Add("");
            list_arr.Add("ALL CROSS GIRDERS");
            list_arr.Add("");
            for (j = 0; j < inner_cross.Count; j++)
            {
                //mc = inner_cross[i];

                list_arr.Add(string.Format("{0,-10} {1} {2}",
             inner_cross[j].MemberNo, inner_cross[j].StartNode, inner_cross[j].EndNode));
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

            last_z = inner_long[0].StartNode.Z;

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
                Deck_Analysis_DL.Effective_Depth = outer_long[0].Length;

            for (i = 0; i < list_inner_xmax.Count; i++)
            {
                x_max = list_inner_xmax[i];
                x_min = list_inner_xmin[i];

                cur_z = list_inner_cur_z[i];

                curr_L2_x = (x_max + x_min) / 2.0;
                curr_L4_x = (curr_L2_x + x_min) / 2.0;
                curr_Deff_x = (Deck_Analysis_DL.Effective_Depth + x_min);

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

            // FOR Inner Long Girder
            _L_2_joints.Remove(64);

            MaxForce val = new MaxForce();
            val = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L_2_joints);
            txt_Ana_LL_inner_long_L2_moment.Text = val.ToString();

            txt_LL_IG_M1.Text = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L_2_joints).ToString();
            txt_LL_IG_M2.Text = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L_4_joints).ToString();
            txt_LL_IG_M3.Text = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_deff_joints).ToString();




            //txt_LL_BM_IG.Text = (val.Force * 10.0).ToString();
            //txt_LL_BM_IG.ForeColor = Color.Red;



            _deff_joints.Remove(20);
            val = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_joints);
            txt_Ana_LL_inner_long_deff_shear.Text = val.ToString();

            txt_LL_IG_S3.Text = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_joints).ToString();
            txt_LL_IG_S1.Text = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L_2_joints).ToString();
            txt_LL_IG_S2.Text = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L_4_joints).ToString();




            //txt_LL_SF_IG.Text = (val.Force * 10.0).ToString();
            //txt_LL_SF_IG.ForeColor = Color.Red;

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
                curr_Deff_x = (Deck_Analysis_DL.Effective_Depth + x_min);

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

            _L_2_joints.Add(64);
            val = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L_2_joints);
            txt_Ana_LL_outer_long_L2_moment.Text = val.ToString();

            txt_LL_OG_M1.Text = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L_2_joints).ToString();
            txt_LL_OG_M2.Text = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L_4_joints).ToString();
            txt_LL_OG_M3.Text = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_deff_joints).ToString();




            //txt_LL_BM_OG.Text = (val.Force * 10.0).ToString();
            //txt_LL_BM_OG.ForeColor = Color.Red;

            _deff_joints.Add(20);
            val = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_joints);
            txt_Ana_LL_outer_long_deff_shear.Text = val.ToString();

            txt_LL_OG_S3.Text = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_joints).ToString();
            txt_LL_OG_S1.Text = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L_2_joints).ToString();
            txt_LL_OG_S2.Text = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L_4_joints).ToString();




            //txt_LL_SF_OG.Text = (10.0 * val.Force).ToString();
            //txt_LL_SF_OG.ForeColor = Color.Red;


            //Cross Girder
            //string cross_text = "";
            //for (j = 0; j < inner_cross.Count; j++)
            //{

            //    cur_member = inner_cross[j].MemberNo;
            //    cross_text += cur_member + " ";
            //}
            //try
            //{
            //    CMember m = new CMember();
            //    m.Group.MemberNosText = cross_text;
            //    m.Force = Deck_Analysis_LL.Truss_Analysis.GetForce(ref m);
            //    txt_Ana_live_cross_max_moment.Text = (m.MaxMoment).ToString();
            //    txt_Ana_live_cross_max_shear.Text = m.MaxShearForce.ToString();
            //}
            //catch (Exception ex) { }
            //Write_Max_Moment_Shear();

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
            try
            {
                //btn_Ana_DL_view_data.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
                btn_view_structure.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
                btn_View_Moving_Load.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
                //btn_Ana_DL_view_report.Enabled = File.Exists(Deck_Analysis_DL.Analysis_Report);

                btn_view_data.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
                //btn_Ana_LL_View_Moving_Load.Enabled = File.Exists(Deck_Analysis_LL.Analysis_Report);
                btn_view_report.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);


                btn_process_analysis.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
                //btn_Ana_Moving_Load.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);



                grb_create_input_data.Enabled = true;

                btn_long_Report.Enabled = File.Exists(LongGirder.rep_file_name);
                btnReport.Enabled = File.Exists(Deck.rep_file_name);
                btn_dwg_rcc_deck.Enabled = File.Exists(Deck.rep_file_name);
                btn_dwg_main_girder.Enabled = File.Exists(LongGirder.user_drawing_file);
                btn_dwg_cant.Enabled = File.Exists(Cant.user_drawing_file);

                btn_Cant_Report.Enabled = File.Exists(Cant.rep_file_name);
                btn_abut_Report.Enabled = File.Exists(Abut.rep_file_name);
                //btn_dwg_rcc_abut.Enabled = File.Exists(Abut.rep_file_name);
                btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
                //btn_dwg_pier.Enabled = File.Exists(rcc_pier.rep_file_name);

                Show_Forces();
            }
            catch (Exception ex) { }
        }


        public void Open_AnalysisFile(string file_name)
        {
            string analysis_file = Path.GetDirectoryName(file_name);

            if ((Path.GetFileName(analysis_file).ToLower() == "dead load analysis") ||
                (Path.GetFileName(analysis_file).ToLower() == "total analysis") ||
                (Path.GetFileName(analysis_file).ToLower() == "live load analysis"))
            {
                 
                analysis_file = Path.GetDirectoryName(analysis_file);
                analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
            }
            else
                analysis_file = file_name;

            user_path = Path.GetDirectoryName(analysis_file);

            Bridge_Analysis.Input_File = analysis_file;

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            Deck_Initialize_InputData();
            Button_Enable_Disable();

            Calculate_Interactive_Values();

            Button_Enable_Disable();
        }

        public void Open_AnalysisFile_2013_04_26(string file_name)
        {
            string analysis_file = Path.GetDirectoryName(file_name);

         

            if ((Path.GetFileName(analysis_file).ToLower() == "dead load analysis") ||
                (Path.GetFileName(analysis_file).ToLower() == "total analysis") ||
                (Path.GetFileName(analysis_file).ToLower() == "live load analysis"))
            {
                //analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "DeadLoad_Analysis_Input_File.TXT");
                //    //goto _run;
                //}
                //else if (Path.GetFileName(analysis_file).ToLower() == "live load analysis")
                //{
                //    analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "LiveLoad_Analysis_Input_File.TXT");
                //    //goto _run;
                //}
                //else if (Path.GetFileName(analysis_file).ToLower() == "total analysis")
                //{
                analysis_file = Path.GetDirectoryName(analysis_file);
                analysis_file = Path.Combine(Path.GetDirectoryName(analysis_file), "INPUT_DATA.TXT");
                //goto _run;
            }
            else
                analysis_file = file_name;


            Bridge_Analysis.Input_File = analysis_file;
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (File.Exists(ana_rep_file))
            {
                Bridge_Analysis.Structure = null;
                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);
                Deck_Analysis_LL.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.LiveLoad_Analysis_Report);
                Show_Moment_Shear_DL();
                Show_Moment_Shear_LL();

                string s = "";
                string s1 = "";
                for (int i = 0; i < Bridge_Analysis.Structure.Supports.Count; i++)
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
                double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);

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

                //Chiranjit [2012 06 22]
                txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
                txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



                txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
                txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

                //txt_abut_w6.Text = Total_LiveLoad_Reaction;
                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                //txt_abut_w6.ForeColor = Color.Red;

                //txt_abut_w5.Text = Total_DeadLoad_Reaction;
                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
            }

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;


            //Deck_Load_Analysis_Data();
            Deck_Initialize_InputData();
            Button_Enable_Disable();

            Calculate_Interactive_Values();

            Button_Enable_Disable();
        }


        public void Open_AnalysisFile_2011_10_18(string file_name)
        {
            string analysis_file = file_name;
            if (File.Exists(analysis_file))
            {
                btn_view_structure.Enabled = true;

                Deck_Analysis_DL.Input_File = (file_name);
                string rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
                if (File.Exists(rep_file))
                {
                    Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, rep_file);
                    Show_Moment_Shear_DL();
                }
                else
                    Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, analysis_file);


                txt_Ana_L.Text = Deck_Analysis_DL.Structure.Analysis.Length.ToString();
                txt_Ana_X.Text = "-" + txt_Ana_L.Text;
                txt_Ana_B.Text = Deck_Analysis_DL.Structure.Analysis.Width.ToString();
                //txt_gd_np.Text = (Deck_Analysis_DL.Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                txt_Ana_analysis_file.Visible = true;
                txt_Ana_analysis_file.Text = analysis_file;

                MessageBox.Show(this, "File opened successfully.");
            }

            string ll_txt = Path.Combine(user_path, "LL.txt");

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_DL.Live_Load_List == null) return;

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

            if (chk_ana_active_LL.Checked)
            {
                //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");


                //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);

                Bridge_Analysis.LoadReadFromGrid(dgv_live_load);
                if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
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

        #region Pre Stressed Post Tension Long Girder Form Events

        private void btn_long_Process_Click(object sender, EventArgs e)
        {

            //Chiranjit [2012 07 13]
            PSC_Section_Properties();
            Write_All_Data();


            //if (txt_DL_BM_IG.Text == "" || txt_DL_BM_OG.Text == "" ||
            //    txt_DL_SF_IG.Text == "" || txt_DL_SF_OG.Text == "" ||
            //    txt_LL_BM_IG.Text == "" || txt_LL_BM_OG.Text == "" ||
            //    txt_LL_SF_IG.Text == "" || txt_LL_SF_OG.Text == "")
            //{
            //    string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
            //    msg += "Please enter the Design Forces manualy.\n\n";
            //    msg += "For Example : Dead Load Bending Moment for Outer Girder = 23070  kN-m\n";
            //    msg += "            : Dead Load Bending Moment for Inner Girder = 22060  kN-m\n";
            //    msg += "            : Live Load Bending Moment for Outer Girder = 20970 kN-m\n";
            //    msg += "            : Live Load Bending Moment for Inner Girder = 20070 kN-m\n";
            //    msg += "            : Dead Load Shear Force for Outer Girder = 1721.0  kN\n";
            //    msg += "            : Dead Load Shear Force for Inner Girder = 1569.0  kN\n";
            //    msg += "            : Live Load Shear Force for Outer Girder = 1617.0 kN\n";
            //    msg += "            : Live Load Shear Force for Inner Girder = 1492.0 kN\n";
            //    MessageBox.Show(msg, "ASTRA"); return;
            //}

            LongGirder.FilePath = user_path;
            Long_Girder_Initialize_InputData();
            Write_Long_Girder_User_Input();
            //LongGirder.Calculate_Long_Girder_Program(PSC_Moment_Forces, PSC_Shear_Forces, pcd, prd, ecgd, sec_1, sec_2, sec_3, sec_4, sec_5);
            LongGirder.Calculate_Long_Girder_Program();
            LongGirder.Write_Long_Girder_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(LongGirder.rep_file_name)) { MessageBox.Show(this, "Report file written in " + LongGirder.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(LongGirder.rep_file_name); }
            LongGirder.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_long_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(LongGirder.rep_file_name);

        }
        private void txt_Long_Girder_fck_TextChanged(object sender, EventArgs e)
        {
            //Long_Girder_Initialize_InputData();
            //double fcc, j, Q, fcb, n;


            //fcb = LongGirder.fck / 3;
            //fcc = LongGirder.fck / 4;

            //n = LongGirder.m * fcb / (LongGirder.m * fcb + LongGirder.sigma_st);

            //j = 1 - (n / 3.0);
            //Q = n * j * fcb / 2;

            //txt_main_girder_sigma_cb.Text = fcb.ToString("0.00");
            //txt_main_girder_j.Text = j.ToString("0.000");
            //txt_main_girder_Q.Text = Q.ToString("0.000");

        }

        #endregion Pre Stressed Post Tension Long Girder Form Events

        #region Pre Stressed Post Tension Long Girder Methods
        public void Write_Long_Girder_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(LongGirder.user_input_file, FileMode.Create));

            try
            {
                #region USER DATA INPUT
                try
                {
                    //sw.WriteLine("L = {0}", txt_Long_L.Text);
                    ////sw.WriteLine("a = {0}", txt_long_a.Text);
                    ////sw.WriteLine("d = {0}", txt_Long_d.Text);
                    ////sw.WriteLine("b = {0}", txt_Long_Dim_d.Text);
                    ////sw.WriteLine("bw = {0}", txt_Long_bw.Text);
                    ////sw.WriteLine("d1 = {0}", txt_Long_d1.Text);
                    ////sw.WriteLine("d2 = {0}", txt_Long_d2.Text);
                    //sw.WriteLine("fck = {0}", cmb_long_fck.Text);
                    //sw.WriteLine("doc = {0}", txt_Long_doc.Text);
                    //sw.WriteLine("fci = {0}", txt_Long_fci.Text);
                    //sw.WriteLine("NS = {0}", txt_Long_NS.Text);
                    //sw.WriteLine("fy = {0}", cmb_long_fy.Text);
                    //sw.WriteLine("dos = {0}", txt_Long_dos.Text);
                    //sw.WriteLine("sigma_cb = {0}", txt_long_sigma_c.Text);
                    //sw.WriteLine("sigma_st = {0}", txt_long_sigma_st.Text);
                    //sw.WriteLine("SF = {0}", txt_Long_SF.Text);
                    //sw.WriteLine("m = {0}", txt_long_m.Text);
                    //sw.WriteLine("Q = {0}", txt_long_Q.Text);
                    //sw.WriteLine("j = {0}", txt_long_j.Text);
                    //sw.WriteLine("eta = {0}", txt_long_eta.Text);
                    //sw.WriteLine("FS = {0}", txt_Long_FS.Text);
                    //sw.WriteLine("fp = {0}", txt_Long_fp.Text);

                    ////sw.WriteLine("DL_BM_OG = {0}", txt_DL_BM_OG.Text);
                    ////sw.WriteLine("DL_BM_IG = {0}", txt_DL_BM_IG.Text);
                    ////sw.WriteLine("LL_BM_OG = {0}", txt_LL_BM_OG.Text);
                    ////sw.WriteLine("LL_BM_IG = {0}", txt_LL_BM_IG.Text);
                    ////sw.WriteLine("DL_SF_OG = {0}", txt_DL_SF_OG.Text);
                    ////sw.WriteLine("DL_SF_IG = {0}", txt_DL_SF_IG.Text);
                    ////sw.WriteLine("LL_SF_OG = {0}", txt_LL_SF_OG.Text);
                    ////sw.WriteLine("LL_SF_IG = {0}", txt_LL_SF_IG.Text);

                    //sw.WriteLine("space_long = {0}", txt_Long_SMG.Text);
                    //sw.WriteLine("space_cross = {0}", txt_Long_SCG.Text);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
                }

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Read_Long_Girder_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(LongGirder.user_input_file));
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
                        case "L":
                            txt_Long_L.Text = mList.StringList[1].Trim().TrimEnd().TrimStart().Trim().TrimEnd().TrimStart();
                            break;
                        //case "a":
                        //    txt_long_a.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "d":
                        //    txt_Long_d.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "b":
                        //    txt_Long_Dim_d.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "bw":
                        //    txt_Long_bw.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "d1":
                        //    txt_Long_d1.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "d2":
                        //    txt_Long_d2.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        case "fck":
                            //txt_main_girder_fck.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "doc":
                            txt_main_girder_doc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fci":
                            txt_Long_fci.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "NS":
                            txt_main_girder_NS.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fy":
                            //txt_main_girder_fy.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dos":
                            txt_main_girder_dos.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_cb":
                            //txt_main_girder_sigma_cb.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_st":
                            //txt_main_girder_sigma_st.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "SF":
                            txt_Long_SF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "m":
                            //txt_main_girder_m.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Q":
                            //txt_main_girder_Q.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "j":
                            //txt_main_girder_j.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "eta":
                            txt_long_eta.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "FS":
                            txt_main_girder_FS.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        //case "DL_BM_OG":
                        //    txt_DL_BM_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "DL_BM_IG":
                        //    txt_DL_BM_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "LL_BM_OG":
                        //    txt_LL_BM_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "LL_BM_IG":
                        //    txt_LL_BM_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "DL_SF_OG":
                        //    txt_DL_SF_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "DL_SF_IG":
                        //    txt_DL_SF_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "LL_SF_OG":
                        //    txt_LL_SF_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        //case "LL_SF_IG":
                        //    txt_LL_SF_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                        //    break;
                        case "fp":
                            txt_main_girder_fp.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;


                        case "space_long":
                            txt_Long_SMG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "space_cross":
                            txt_Long_SCG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex) { }
            lst_content.Clear();
        }
        
        #endregion Pre Stressed Post Tension Long Girder Methods

        #region RCC DECK SLAB

        public void Deck_Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(Deck.user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("S = {0}", txt_Deck_S.Text);
                sw.WriteLine("CW = {0}", txt_Deck_CW.Text);
                sw.WriteLine("Wk = {0}", txt_Deck_Wk.Text);
                sw.WriteLine("tk = {0}", txt_Deck_tk.Text);
                sw.WriteLine("Fw = {0}", txt_Deck_Fw.Text);
                sw.WriteLine("twc = {0}", txt_Deck_twc.Text);
                sw.WriteLine("Wt = {0}", txt_Deck_Wt.Text);
                sw.WriteLine("Ltl = {0}", txt_Deck_Ltl.Text);
                sw.WriteLine("Wtl = {0}", txt_Deck_Wtl.Text);
                sw.WriteLine("L = {0}", txt_Deck_L.Text);
                sw.WriteLine("bl = {0}", txt_Deck_bl.Text);
                sw.WriteLine("B = {0}", txt_Deck_B.Text);
                sw.WriteLine("bc = {0}", txt_Deck_bc.Text);
                sw.WriteLine("_Do = {0}", txt_Deck_Do.Text);
                sw.WriteLine("fck = {0}", cmb_deck_fck.Text);
                sw.WriteLine("fci = {0}", txt_Deck_fci.Text);
                sw.WriteLine("sigma_cb = {0}", txt_deck_sigma_c.Text);
                sw.WriteLine("sigma_st = {0}", txt_Deck_sigma_st.Text);
                sw.WriteLine("m = {0}", txt_deck_m.Text);
                sw.WriteLine("Q = {0}", txt_Deck_Q.Text);
                sw.WriteLine("j = {0}", txt_Deck_j.Text);
                sw.WriteLine("fy = {0}", cmb_deck_fy.Text);
                sw.WriteLine("gamma_c = {0}", txt_Deck_gamma_c.Text);
                sw.WriteLine("gamma_wc = {0}", txt_Deck_gamma_w.Text);
                sw.WriteLine("_IF = {0}", txt_Deck_IF.Text);
                sw.WriteLine("CF = {0}", txt_Deck_CF.Text);
                sw.WriteLine("dm = {0}", txt_Deck_dm.Text);
                sw.WriteLine("dd = {0}", txt_Deck_dd.Text);

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Deck_Read_User_Input()
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
                            txt_Deck_S.Text = mList.StringList[1].Trim().TrimEnd().TrimStart().Trim().TrimEnd().TrimStart();
                            break;
                        case "CW":
                            txt_Deck_CW.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Wk":
                            txt_Deck_Wk.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Fw":
                            txt_Deck_Fw.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "twc":
                            txt_Deck_twc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Wt":
                            txt_Deck_Wt.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Ltl":
                            txt_Deck_Ltl.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Wtl":
                            txt_Deck_Wtl.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "L":
                            txt_Deck_L.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "bl":
                            txt_Deck_bl.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "B":
                            txt_Deck_B.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "bc":
                            txt_Deck_bc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "_Do":
                            txt_Deck_Do.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fck":
                            //txt_fck.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fci":
                            txt_Deck_fci.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_cb":
                            //txt_sigma_cb.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_st":
                            //txt_sigma_st.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "m":
                            //txt_m.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Q":
                            //txt_Q.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "j":
                            //txt_j.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fy":
                            //txt_fy.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "gamma_c":
                            txt_Deck_gamma_c.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "gamma_wc":
                            txt_Deck_gamma_w.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "_IF":
                            txt_Deck_IF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "CF":
                            txt_Deck_CF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dm":
                            txt_Deck_dm.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dd":
                            txt_Deck_dd.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
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
        public void Deck_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                Deck.S = MyList.StringToDouble(txt_Deck_S.Text, 0.0);
                Deck.CW = MyList.StringToDouble(txt_Deck_CW.Text, 0.0);
                Deck.Wk = MyList.StringToDouble(txt_Deck_Wk.Text, 0.0);
                Deck.tk = MyList.StringToDouble(txt_Deck_tk.Text, 0.0);
                Deck.Fw = MyList.StringToDouble(txt_Deck_Fw.Text, 0.0);
                Deck.twc = MyList.StringToDouble(txt_Deck_twc.Text, 0.0);
                Deck.Wt = MyList.StringToDouble(txt_Deck_Wt.Text, 0.0);
                Deck.Ltl = MyList.StringToDouble(txt_Deck_Ltl.Text, 0.0);
                Deck.Wtl = MyList.StringToDouble(txt_Deck_Wtl.Text, 0.0);
                Deck.L = MyList.StringToDouble(txt_Deck_L.Text, 0.0);
                Deck.bl = MyList.StringToDouble(txt_Deck_bl.Text, 0.0);
                Deck.B = MyList.StringToDouble(txt_Deck_B.Text, 0.0);
                Deck.bc = MyList.StringToDouble(txt_Deck_bc.Text, 0.0);
                Deck._Do = MyList.StringToDouble(txt_Deck_Do.Text, 0.0);
                Deck.fci = MyList.StringToDouble(txt_Deck_fci.Text, 0.0);

                Deck.fck = MyList.StringToDouble(cmb_deck_fck.Text, 0.0);
                Deck.fy = MyList.StringToDouble(cmb_deck_fy.Text, 0.0);
                Deck.sigma_cb = MyList.StringToDouble(txt_deck_sigma_c.Text, 0.0);
                Deck.sigma_st = MyList.StringToDouble(txt_Deck_sigma_st.Text, 0.0);
                Deck.m = MyList.StringToDouble(txt_deck_m.Text, 0.0);
                Deck.Q = MyList.StringToDouble(txt_Deck_Q.Text, 0.0);
                Deck.j = MyList.StringToDouble(txt_Deck_j.Text, 0.0);

                Deck.gamma_c = MyList.StringToDouble(txt_Deck_gamma_c.Text, 0.0);
                Deck.gamma_wc = MyList.StringToDouble(txt_Deck_gamma_w.Text, 0.0);
                Deck._IF = MyList.StringToDouble(txt_Deck_IF.Text, 0.0);
                Deck.CF = MyList.StringToDouble(txt_Deck_CF.Text, 0.0);
                Deck.dm = MyList.StringToDouble(txt_Deck_dm.Text, 0.0);
                Deck.dd = MyList.StringToDouble(txt_Deck_dd.Text, 0.0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }

        private void btn_Deck_Process_Click(object sender, EventArgs e)
        {


            //Chiranjit [2012 07 13]
            Write_All_Data();


            Deck_Initialize_InputData();
            Deck.FilePath = user_path;
            Deck_Write_User_Input();
            Deck.Calculate_Program();
            Deck.Write_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(Deck.rep_file_name)) { MessageBox.Show(this, "Report file written in " + Deck.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(Deck.rep_file_name); }
            Deck.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Deck.rep_file_name);
        }

        private void txt_Deck_fck_TextChanged(object sender, EventArgs e)
        {
            Deck_Initialize_InputData();
            double fcc, j, Q, fcb, n;


            fcb = Deck.fck / 3;
            fcc = Deck.fck / 4;

            n = Deck.m * fcb / (Deck.m * fcb + Deck.sigma_st);

            j = 1 - (n / 3.0);
            Q = n * j * fcb / 2;

            //txt_sigma_cb.Text = fcb.ToString("0.00");
            //txt_j.Text = j.ToString("0.000");
            //txt_Q.Text = Q.ToString("0.000");
        }
        #endregion  RCC DECK SLAB

        private void frm_PreStressed_Load(object sender, EventArgs e)
        {
            pic_rcc_deck.BackgroundImage = AstraFunctionOne.ImageCollection.Pre_Stressed_Bridge;
            pic_rcc_deck_example.BackgroundImage = AstraFunctionOne.ImageCollection.prestressed_bridge_image01;

            Deck_Analysis_DL = new PSC_I_Girder_Short_Analysis_WS(iApp);
            Deck_Analysis_LL = new PSC_I_Girder_Short_Analysis_WS(iApp);
            Bridge_Analysis = new PSC_I_Girder_Short_Analysis_WS(iApp);
            LongGirder = new PostTensionLongGirder_ShortSpan(iApp);
            Deck = new RccDeckSlab(iApp);
            //dgv_Ana_DL_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -30.0, 0, 2.75, 0.2);
            //dgv_Ana_DL_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -30.0, 0, 6.25, 0.2);

            iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
            dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "1.50", txt_XINCR.Text, txt_Load_Impact.Text);
            dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "4.50", txt_XINCR.Text, txt_Load_Impact.Text);
            dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "7.50", txt_XINCR.Text, txt_Load_Impact.Text);
            txt_LL_load_gen.Text = (L / 0.2).ToString("0");


            //cmb_main_DL_BM.SelectedIndex = 2;
            //cmb_main_LL_BM.SelectedIndex = 2;
            cmb_long_fck.SelectedIndex = 2;
            cmb_long_fy.SelectedIndex = 1;



            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 0;

            //cmb_cant_select_load.Items.AddRange(LoadApplied.Get_All_LoadName().ToArray());
            //cmb_cant_select_load.SelectedIndex = 0;

            #region RCC Abutment
            Abut = new RCC_AbutmentWall(iApp);
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;
            #endregion RCC Abutment

            #region RCC Pier
            cmb_pier_2_k.SelectedIndex = 1;
            rcc_pier = new RccPier(iApp);
            pic_pier_interactive_diagram.BackgroundImage = AstraFunctionOne.ImageCollection.Pier_drawing;
            #endregion RCC Pier

            #region Cantilever Slab Data
            Cant = new CantileverSlab(iApp);
            cant_pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.T_Beam_Slab_Long_Cross_Girders;
            cant_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.Pre_Stressed_Bridge;


            cmb_cant_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_cant_select_load.SelectedIndex = 0;

            #endregion Cantilever Slab Data

            //cmb_long_fck.SelectedIndex = 6;
            //cmb_long_fy.SelectedIndex = 2;

            cmb_cant_fck.SelectedIndex = 2;
            cmb_cant_fy.SelectedIndex = 1;

            cmb_deck_fck.SelectedIndex = 2;
            cmb_deck_fy.SelectedIndex = 1;

            cmb_abut_fck.SelectedIndex = 2;
            cmb_abut_fy.SelectedIndex = 1;

            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;


            //cmb_rd_dsc_fy.SelectedIndex = 1;

            //cmb_rd_dtsr_fy.SelectedIndex = 2;
            //cmb_rd_dtsr_fck.SelectedIndex = 7;




            PSC_Section_Properties();
            Text_Changed();
            Button_Enable_Disable();


            rbtn_2_lane.Checked = true;


            Set_Project_Name();



        }

        private void Open_Project()
        {


            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{

                    IsCreate_Data = false;


                    //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");


                    Open_AnalysisFile(chk_file);

                    LongGirder.FilePath = user_path;
                    Deck.FilePath = user_path;
                    Cant.FilePath = user_path;
                    Abut.FilePath = user_path;
                    rcc_pier.FilePath = user_path;
                    txt_Ana_analysis_file.Text = chk_file;
                    if (File.Exists(Deck_Analysis_DL.Input_File))
                    {
                        Show_ReadMemberLoad(Deck_Analysis_DL.Input_File);
                    }
                    if (File.Exists(Deck_Analysis_LL.Input_File))
                    {

                        Show_LoadGeneration(Deck_Analysis_LL.Input_File);
                    }
                    Show_Forces();
                    iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);


                    //Chiranjit [2012 07 13]

                    //Chiranjit [2013 04 26]
                    iApp.Read_Form_Record(this, user_path);
                    rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                    Open_Create_Data();//Chiranjit [2013 06 25]

                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data....", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            iApp.SetDrawingFile_Path(LongGirder.user_drawing_file, "PSC_Main_I_Girder", Path.Combine(Drawing_Folder, "PSC Main Girder Short Span Drawing"), "");
            //iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC I Girder Super Structure Drawings"), "PSC_Main_I_Girder");
        }

        private void btn_dwg_rcc_deck_Click(object sender, EventArgs e)
        {

            Button b = sender as Button;
            if (b.Name == btn_dwg_rcc_abut.Name)
            {
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
            }
            else if (b.Name == btn_dwg_pier.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
            }
            else if (b.Name == btn_dwg_cant.Name)
            {
                iApp.SetDrawingFile_Path(Cant.user_drawing_file, "TBEAM_Cantilever", Path.Combine(Drawing_Folder, "RCC Cantilever Drawing"), "");
            }
            else if (b.Name == btn_dwg_rcc_deck.Name)
            {
                iApp.SetDrawingFile_Path(Deck.drawing_path, "PSC_I_Girder_Deck_Slab", Path.Combine(Drawing_Folder, "RCC Deck Slab Drawing"),"");
            }
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void cmb_main_DL_BM_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show_Forces();
        }

        private void Show_Forces()
        {
            //txt_DL_BM_IG.Text = PSC_Moment_Forces.DL_INNER_GIRDER[cmb_main_DL_BM.SelectedIndex].ToString("f4");


            //txt_DL_BM_IG.Text = PSC_Moment_Forces.DL_INNER_GIRDER.Get_Force_Data(cmb_main_DL_BM.SelectedIndex).ToString("f4");
            //txt_DL_BM_OG.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.Get_Force_Data(cmb_main_DL_BM.SelectedIndex).ToString("f4");

            //txt_LL_BM_IG.Text = PSC_Moment_Forces.LL_INNER_GIRDER.Get_Force_Data(cmb_main_LL_BM.SelectedIndex).ToString("f4");
            //txt_LL_BM_OG.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.Get_Force_Data(cmb_main_LL_BM.SelectedIndex).ToString("f4");


            //txt_DL_SF_IG.Text = PSC_Shear_Forces.DL_INNER_GIRDER.Get_Force_Data(cmb_main_DL_SF.SelectedIndex).ToString("f4");
            //txt_DL_SF_OG.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.Get_Force_Data(cmb_main_DL_SF.SelectedIndex).ToString("f4");


            //txt_LL_SF_IG.Text = PSC_Shear_Forces.LL_INNER_GIRDER.Get_Force_Data(cmb_main_LL_SF.SelectedIndex).ToString("f4");
            //txt_LL_SF_OG.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.Get_Force_Data(cmb_main_LL_SF.SelectedIndex).ToString("f4");


            //txt_DL_SF_IG.Text = (MyList.StringToDouble(txt_DL_SF_IG.Text, 0.0) * 10.0).ToString("f4");
            //txt_LL_SF_IG.Text = (MyList.StringToDouble(txt_LL_SF_IG.Text, 0.0) * 10.0).ToString("f4");
            //txt_DL_SF_OG.Text = (MyList.StringToDouble(txt_DL_SF_OG.Text, 0.0) * 10.0).ToString("f4");
            //txt_LL_SF_OG.Text = (MyList.StringToDouble(txt_LL_SF_OG.Text, 0.0) * 10.0).ToString("f4");

            //txt_DL_BM_IG.Text = (MyList.StringToDouble(txt_DL_BM_IG.Text, 0.0) * 10.0).ToString("f4");
            //txt_LL_BM_IG.Text = (MyList.StringToDouble(txt_LL_BM_IG.Text, 0.0) * 10.0).ToString("f4");
            //txt_DL_BM_OG.Text = (MyList.StringToDouble(txt_DL_BM_OG.Text, 0.0) * 10.0).ToString("f4");
            //txt_LL_BM_OG.Text = (MyList.StringToDouble(txt_LL_BM_OG.Text, 0.0) * 10.0).ToString("f4");
        }

        private void cmb_Ana_DL_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                {
                    txt_Ana_X.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].Distance.ToString("f3");
                    txt_Load_Impact.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].Default_ImpactFactor.ToString("f3");
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

        private void btn_update_force_Click(object sender, EventArgs e)
        {
            string ana_rep_file = File.Exists(Bridge_Analysis.LiveLoad_Analysis_Report) ? Bridge_Analysis.LiveLoad_Analysis_Report : Analysis_Report_LL;
            //string ana_rep_file = Deck_Analysis_LL.Analysis_Report;
            iApp.Progress_ON("Read forces...");
            iApp.SetProgressValue(9, 100);
            if (File.Exists(ana_rep_file))
            {
                //Deck_Analysis_LL.Truss_Analysis = null;
                //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_LL.Structure.ForceType = GetForceType();
                iApp.SetProgressValue(19, 100);
                Show_Moment_Shear_LL();
                iApp.SetProgressValue(29, 100);

                grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
                //grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

                Button_Enable_Disable();
                Show_Analysis_Result();
                iApp.SetProgressValue(49, 100);
            }
            //else
            //    MessageBox.Show("Analysis Result not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



            ana_rep_file = File.Exists(Bridge_Analysis.DeadLoad_Analysis_Report) ? Bridge_Analysis.DeadLoad_Analysis_Report : Analysis_Report_DL;
            iApp.SetProgressValue(59, 100);
            if (File.Exists(ana_rep_file))
            {
                //Deck_Analysis_DL.Truss_Analysis = null;
                //Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_DL.Structure.ForceType = GetForceType();
                iApp.SetProgressValue(69, 100);
                Show_Moment_Shear_DL();
            }
            iApp.SetProgressValue(89, 100);

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            //grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            Button_Enable_Disable();
            Show_Analysis_Result();
            iApp.SetProgressValue(99, 100);
            iApp.Progress_OFF();
        }
        #endregion

        //Chiranjit [2012 05 27]
        #region Abutment
        private void btn_Abutment_Process_Click(object sender, EventArgs e)
        {

            //Chiranjit [2012 07 13]
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

            Abut.d1 = MyList.StringToDouble(txt_Abut_d1.Text, 0.0);
            Abut.t = MyList.StringToDouble(txt_Abut_t.Text, 0.0);
            Abut.H = MyList.StringToDouble(txt_Abut_H.Text, 0.0);
            Abut.a = MyList.StringToDouble(txt_Abut_a.Text, 0.0);
            Abut.gamma_b = MyList.StringToDouble(txt_abut_gamma_b.Text, 0.0);
            Abut.gamma_c = MyList.StringToDouble(txt_Abut_gamma_c.Text, 0.0);
            Abut.phi = MyList.StringToDouble(txt_abut_phi.Text, 0.0);
            Abut.p = MyList.StringToDouble(txt_abut_p.Text, 0.0);

            Abut.w6 = MyList.StringToDouble(txt_abut_w6.Text, 0.0);
            Abut.w5 = MyList.StringToDouble(txt_abut_w5.Text, 0.0);
            Abut.F = MyList.StringToDouble(txt_abut_F.Text, 0.0);
            Abut.d2 = MyList.StringToDouble(txt_abut_d2.Text, 0.0);
            Abut.d3 = MyList.StringToDouble(txt_abut_d3.Text, 0.0);
            Abut.B = MyList.StringToDouble(txt_Abut_B.Text, 0.0);
            Abut.theta = MyList.StringToDouble(txt_abut_theta.Text, 0.0);
            Abut.delta = MyList.StringToDouble(txt_abut_delta.Text, 0.0);
            Abut.z = MyList.StringToDouble(txt_abut_z.Text, 0.0);
            Abut.mu = MyList.StringToDouble(txt_abut_mu.Text, 0.0);
            Abut.L1 = MyList.StringToDouble(txt_abut_L1.Text, 0.0);
            Abut.L2 = MyList.StringToDouble(txt_abut_L2.Text, 0.0);
            Abut.L3 = MyList.StringToDouble(txt_abut_L3.Text, 0.0);
            Abut.L4 = MyList.StringToDouble(txt_abut_L4.Text, 0.0);
            Abut.h1 = MyList.StringToDouble(txt_abut_h1.Text, 0.0);
            Abut.L = MyList.StringToDouble(txt_Abut_L.Text, 0.0);
            Abut.d4 = MyList.StringToDouble(txt_abut_d4.Text, 0.0);
            Abut.cover = MyList.StringToDouble(txt_abut_cover.Text, 0.0);
            Abut.factor = MyList.StringToDouble(txt_abut_fact.Text, 0.0);
            Abut.sc = MyList.StringToDouble(txt_abut_sc.Text, 0.0);



            Abut.f_ck = MyList.StringToDouble(cmb_abut_fck.Text, 0.0);
            Abut.f_y = MyList.StringToDouble(cmb_abut_fy.Text, 0.0);
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

            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);

            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);
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

        }
        private void txt_Ana_width_TextChanged(object sender, EventArgs e)
        {

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
        public double DMG { get { return MyList.StringToDouble(txt_Ana_DMG.Text, 0.0); } set { txt_Ana_DMG.Text = value.ToString("f3"); } }
        public double Deff { get { return (DMG - 0.500 - (4 * 0.028 + 3 * 0.028) / 2.0); } }
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
        #region Chiranjit [2012 06 20]
        void Text_Changed()
        {

            double SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);


            double Es = MyList.StringToDouble(txt_Long_Es.Text, 0.0);
            double Ec = MyList.StringToDouble(txt_Long_Ec.Text, 0.0);

            //txt_long_m.Text = (Es / Ec).ToString("f0");

            txt_Long_SMG.Text = SMG.ToString("f3");
            txt_Long_SCG.Text = SCG.ToString("f3");
            txt_Long_B.Text = B.ToString("f3");

            //txt_Cross_SMG.Text = SMG.ToString("f3");
            //txt_Cross_SCG.Text = SCG.ToString("f3");

            txt_Deck_L.Text = SMG.ToString("f3");
            txt_Deck_B.Text = SCG.ToString("f3");

            //txt__SMG.Text = SMG.ToString("f3");
            //txt_Deck_SCG.Text = SCG.ToString("f3");


            txt_LL_load_gen.Text = (L / MyList.StringToDouble(txt_XINCR.Text, 0.0)).ToString("f0");


            txt_Long_L.Text = L.ToString("f3");
            txt_Deck_S.Text = L.ToString("f3");
            txt_Abut_L.Text = L.ToString("f3");
            txt_RCC_Pier_L.Text = L.ToString("f3");

            txt_Abut_d1.Text = (DMG + 0.2).ToString("f3");

            txt_Abut_B.Text = B.ToString("f3");
            txt_RCC_Pier__B.Text = B.ToString("f3");
            txt_RCC_Pier___B.Text = B.ToString("f3");

            txt_Deck_CW.Text = CW.ToString();
            txt_RCC_Pier_CW.Text = CW.ToString();

            //txt_Long_d1.Text = (Ds * 1000).ToString();
            txt_Deck_Do.Text = (Ds * 1000).ToString();
            txt_RCC_Pier_DS.Text = (Ds).ToString();

            txt_Deck_gamma_c.Text = Y_c.ToString();
            txt_Abut_gamma_c.Text = Y_c.ToString();
            txt_RCC_Pier_gama_c.Text = Y_c.ToString();
            txt_Cant_gamma_c.Text = Y_c.ToString();

            //txt_Cross_NMG.Text = NMG.ToString();
            //txt_Deck_NMG.Text = NMG.ToString();
            txt_RCC_Pier_NMG.Text = NMG.ToString();
            txt_RCC_Pier_NP.Text = NMG.ToString();

            //txt_Long_d.Text = (DMG * 1000).ToString();
            //txt_Abut_DMG.Text = (DMG + 0.2).ToString();
            txt_RCC_Pier_DMG.Text = (DMG).ToString();

            //txt_Long_bw.Text = (BMG * 1000).ToString();
            txt_Deck_bl.Text = (BMG * 1000).ToString();
            txt_Cant_BMG.Text = (BMG).ToString();

            txt_Deck_bc.Text = (BCG * 1000).ToString();

            //txt_Deck_Dwc.Text = (Dw * 1000).ToString();

            txt_Deck_gamma_w.Text = Y_w.ToString();
            //txt_Cant_gamma_c.Text = Y_w.ToString();
            //txt_Abut_gamma_b.Text = Y_w.ToString();

            txt_RCC_Pier_Hp.Text = Hp.ToString();

            txt_Cant_Wp.Text = Wp.ToString();
            //txt_Cant_a3.Text = (CL - Wp).ToString();
            //txt_Cant_a3.Text = (CL).ToString();

            txt_RCC_Pier_Wp.Text = Wp.ToString();






            txt_Abut_d1.Text = (DMG + Ds - MyList.StringToDouble(txt_abut_d3.Text, 0.0)).ToString("f3");
            //txt_RCC_Pier_DMG.Text = DMG.ToString("f3");
            txt_RCC_Pier_DS.Text = Ds.ToString("f3");



            //if (Bridge_Analysis.Length == 0)
            //{
            Bridge_Analysis.Length = L;
            Bridge_Analysis.Effective_Depth = Deff;
            //}
            txt_L1.Text = Deff.ToString("f3");
            txt_L2.Text = (L/4.0).ToString("f3");
            txt_L3.Text = (L / 2.0).ToString("f3");
            //txt_L4.Text = Bridge_Analysis.L4.ToString("f3");
            //txt_L5.Text = Bridge_Analysis.L5.ToString("f3");
            //txt_L6.Text = Bridge_Analysis.L6.ToString("f3");
            //txt_L7.Text = Bridge_Analysis.L7.ToString("f3");
            //txt_L8.Text = Bridge_Analysis.L8.ToString("f3");
            //txt_L9.Text = Bridge_Analysis.L9.ToString("f3");


        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();
            //txt_Ana_X.Text = "-" + txt_Ana_L.Text;

            //if (((TextBox)sender).Name == txt_Ana_B.Name)
            //    CW = B - 2.0;

            CW = B - 2.0* Wp - Bs;


            try
            {
                for (int i = 0; i < dgv_live_load.RowCount; i++)
                {
                    dgv_live_load[4, i].Value = txt_XINCR.Text;
                    dgv_live_load[1, i].Value = txt_Ana_X.Text;
                }
            }
            catch (Exception ex) { }
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
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "1.50", txt_XINCR.Text);
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "4.50", txt_XINCR.Text);
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "7.50", txt_XINCR.Text);
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


            //list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("                131 TO 210 UNI GY -{0:f4}", wiu));
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
            member_load.Add(string.Format("MEMBER LOAD "));
            member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));
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

            if (user_path != iApp.LastDesignWorkingFolder)
                File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        public void Calculate_Load_Computation(string outer_girders, string inner_girders, List<string> joints_nos)
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();




            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

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
            member_load.Add(string.Format("MEMBER LOAD "));
            //member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wiu));

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
            member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wou));
            //member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            //member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                {0} UNI GY -{1:f4}", outer_girders, wou));
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

            list.Add(string.Format("                JOINT LOAD"));
            member_load.Add(string.Format("JOINT LOAD"));
            foreach (var item in joints_nos)
            {
                list.Add(string.Format("                {0} FY -{1:f4}", item, wjl));
                member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl));

            }

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

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));

            txt_member_load.Lines = member_load.ToArray();

            if (user_path != iApp.LastDesignWorkingFolder)
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

                txt_Deck_Wt.Text = ld.Applied_Load.ToString();
                txt_Deck_Ltl.Text = ld.LoadLength.ToString();
                txt_Deck_Wtl.Text = ld.LoadWidth.ToString();
            }
            else if (cmb.Name == cmb_cant_select_load.Name)
            {
                ld = LoadApplied.Get_Applied_Load(cmb_cant_select_load.Text);

                txt_Cant_w1.Text = ld.Applied_Load.ToString();
                txt_Cant_a4.Text = ld.LoadWidth.ToString();
            }
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
        private void btn_Cant_Process_Click(object sender, EventArgs e)
        {

            //Chiranjit [2012 07 13]
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
        #endregion Chiranjit [2012 06 10]


        #region Chiranjit [2012 07 04]
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_long") ||
                ctrl.Name.ToLower().StartsWith("txt_long"))
            {
                astg = new ASTRAGrade(cmb_long_fck.Text, cmb_long_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_long_m.Text, 10.0);
                txt_long_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_long_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
                txt_long_j.Text = astg.j.ToString("f3");
                txt_long_Q.Text = astg.Q.ToString("f3");
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

            //txt_abut
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


            txt_Abut_B.Text = txt_RCC_Pier__B.Text = txt_RCC_Pier___B.Text = txt_Ana_B.Text;

            txt_RCC_Pier_L.Text = txt_Abut_L.Text = txt_Ana_L.Text;


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

        #region Chiranjit [2012 07 13]
        //Write All Data in a File
        public void Write_All_Data()
        {

            Save_FormRecord.Write_All_Data(this, user_path); return;
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            if (showMessage) DemoCheck();
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
            //"LoadType", "X", "Y", "Z", "    XINCR"));
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

            #region MAIN GIRDER USER INPUTS

            file_content.Add(string.Format("MAIN GIRDER USER INPUTS"));
            file_content.Add(string.Format("-----------------------"));
            file_content.Add(string.Format(kFormat, "Span of Girder", "L", txt_Long_L.Text));
            //file_content.Add(string.Format(kFormat, "Width of Girder", "B", txt_Long_bw.Text));
            //file_content.Add(string.Format(kFormat, "Dimension", "a", txt_long_a.Text));
            //file_content.Add(string.Format(kFormat, "Overall Depth of Girder", "d", txt_Long_d.Text));
            //file_content.Add(string.Format(kFormat, "Dimension", "b", txt_Long_Dim_d.Text));
            //file_content.Add(string.Format(kFormat, "Dimension", "bw", txt_Long_bw.Text));
            //file_content.Add(string.Format(kFormat, "Dimension", "d1", txt_Long_d1.Text));
            //file_content.Add(string.Format(kFormat, "Dimension", "d2", txt_Long_d2.Text));
            file_content.Add(string.Format(kFormat, "Spacing of Main Long Girders", "SMG", txt_Long_SMG.Text));
            file_content.Add(string.Format(kFormat, "Spacing of Cross Girders", "SCG", txt_Long_SCG.Text));
            file_content.Add(string.Format(kFormat, "Concrete Grade", "fck", cmb_long_fck.Text));
            file_content.Add(string.Format(kFormat, "Post Tension Cable Diameter", "doc", txt_main_girder_doc.Text));
            file_content.Add(string.Format(kFormat, "Cube Strength at Transfer", "fci", txt_Long_fci.Text));
            file_content.Add(string.Format(kFormat, "Freyssinet Anchorable Number of Strands", "NS", txt_main_girder_NS.Text));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_long_fy.Text));
            file_content.Add(string.Format(kFormat, "Diameter of Strands", "dos", txt_main_girder_dos.Text));
            file_content.Add(string.Format(kFormat, "Permissible compressive stress in concrete ", "σ_cb", txt_long_sigma_c.Text));
            file_content.Add(string.Format(kFormat, "Permissible tensile stress in steel", "σ_st", txt_long_sigma_st.Text));
            file_content.Add(string.Format(kFormat, "Strand Factor ", "SF", txt_Long_SF.Text));
            file_content.Add(string.Format(kFormat, "Moment Factor ", "Q", txt_long_Q.Text));
            file_content.Add(string.Format(kFormat, "Lever Arm Factor", "j", txt_long_j.Text));
            file_content.Add(string.Format(kFormat, "Loss Ratio", "n", txt_long_eta.Text));
            file_content.Add(string.Format(kFormat, "Force per Strand", "FS", txt_main_girder_FS.Text));

            file_content.Add(string.Format(kFormat, "Elastic Modulus of Steel", "Es", txt_Long_Es.Text));
            file_content.Add(string.Format(kFormat, "Elastic Modulus of Concrete", "Ec", txt_Long_Ec.Text));
            file_content.Add(string.Format(kFormat, "Modular Ratio", "m", txt_long_m.Text));
            file_content.Add(string.Format(kFormat, "Creep Coefficient", "phi", txt_Long_phi.Text));
            file_content.Add(string.Format(kFormat, "Ultimate Creep Strain", "ecc", txt_Long_ecc.Text));
            file_content.Add(string.Format(kFormat, "Age of Concrete at Transfer", "Atr", txt_Long_Atr.Text));

            #endregion LONG GIRDER USER INPUT

            #region   DECK SLAB USER INPUT
            file_content.Add(string.Format(""));
            file_content.Add(string.Format("--------------------"));
            file_content.Add(string.Format("DECK SLAB USER INPUT"));
            file_content.Add(string.Format("--------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Effective Span ", "S", txt_Deck_S.Text));
            file_content.Add(string.Format(kFormat, "Width of Carrage way ", "CW", txt_Deck_CW.Text));
            file_content.Add(string.Format(kFormat, "Width of Kerb ", "Wk", txt_Deck_Wk.Text));
            file_content.Add(string.Format(kFormat, "Thickness of Kerb ", "tk", txt_Deck_tk.Text));
            file_content.Add(string.Format(kFormat, "Width of Footpath ", "Fw", txt_Deck_Fw.Text));
            file_content.Add(string.Format(kFormat, "Thickness of Wearing Course ", "twc", txt_Deck_twc.Text));
            file_content.Add(string.Format(kFormat, "Select Load ", "SL", cmb_deck_select_load.Text));
            file_content.Add(string.Format(kFormat, "Track Load ", "Wt", txt_Deck_Wt.Text));
            file_content.Add(string.Format(kFormat, "Track Loading Length ", "Ltl", txt_Deck_Ltl.Text));
            file_content.Add(string.Format(kFormat, "Track Loading Width ", "Wtl", txt_Deck_Wtl.Text));
            file_content.Add(string.Format(kFormat, "Spacing of Main Long Girders ", "L", txt_Deck_L.Text));
            file_content.Add(string.Format(kFormat, "Width of Long Girder ", "bl", txt_Deck_bl.Text));
            file_content.Add(string.Format(kFormat, "Spacing of Cross Girders ", "B", txt_Deck_B.Text));
            file_content.Add(string.Format(kFormat, "Width of Cross Girders ", "bc", txt_Deck_bc.Text));
            file_content.Add(string.Format(kFormat, "Thickness of Deck Slab ", "Do", txt_Deck_Do.Text));
            file_content.Add(string.Format(kFormat, "Concrete Grade ", "fck", cmb_deck_fck.Text));
            file_content.Add(string.Format(kFormat, "Concrete Cube strength at transfer ", "fci", txt_Deck_fci.Text));
            file_content.Add(string.Format(kFormat, "Permissible compressive stress in concrete ", "σ_cb", txt_deck_sigma_c.Text));
            file_content.Add(string.Format(kFormat, "Permissible tensile stress in steel ", "σ_st", txt_Deck_sigma_st.Text));
            file_content.Add(string.Format(kFormat, "Modular Ratio ", "m", txt_deck_m.Text));
            file_content.Add(string.Format(kFormat, "Moment Factor ", "Q", txt_Deck_Q.Text));
            file_content.Add(string.Format(kFormat, "Lever Arm Factor ", "j", txt_Deck_j.Text));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_deck_fy.Text));
            file_content.Add(string.Format(kFormat, "Unit Weight of Concrete ", "γ_c", txt_Deck_gamma_c.Text));
            file_content.Add(string.Format(kFormat, "Unit Weight of Wearing Course ", "γ_wc", txt_Deck_gamma_w.Text));
            file_content.Add(string.Format(kFormat, "Impact Factor ", "IF", txt_Deck_IF.Text));
            file_content.Add(string.Format(kFormat, "Continuity Factor ", "CF", txt_Deck_CF.Text));
            file_content.Add(string.Format(kFormat, "Diameter of Main Reinforcement Bars ", "dm", txt_Deck_dm.Text));
            file_content.Add(string.Format(kFormat, "Diameter of Distribution Reinforcement Bars ", "dd", txt_Deck_dd.Text));
            file_content.Add(string.Format(""));
            #endregion DECK SLAB USER INPUT

            #region CANTILEVER USER INPUT

            file_content.Add(string.Format(""));
            file_content.Add(string.Format("CANTILEVER USER INPUT"));
            file_content.Add(string.Format("---------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Girder", "a1", txt_Cant_BMG.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Kerb ", "Wp", txt_Cant_Wp.Text));
            file_content.Add(string.Format(""));
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
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance of Load Centre from Girder Face", "a5", txt_Cant_a5.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance of Edge of Load from Kerb Face", "a6", txt_Cant_a6.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of wearing Course", "d4", txt_Cant_d4.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Load from Hand Rails", "w2", txt_Cant_w2.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Distance from Post Edge to Free End", "a7", txt_Cant_a7.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "RCC Posts Width", "RCC_X", txt_Cant_RCC_X.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "RCC Posts Height", "RCC_Y", txt_Cant_RCC_Y.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of Hand Railing", "WHR", txt_Cant_width_of_hand_rail.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit Weight of Concrete", "γ_c", txt_Cant_gamma_c.Text));
            file_content.Add(string.Format(""));
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
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));

            #endregion CANTILEVER USER INPUT

            #region ABUTMENT USER INPUT

            file_content.Add(string.Format(""));
            file_content.Add(string.Format("ABUTMENT USER INPUT"));
            file_content.Add(string.Format("--------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Depth of Girder Seat", "DMG", txt_Abut_d1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Thickness of Main wall", "t", txt_Abut_t.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Retained Earth", "H", txt_Abut_H.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Height of Earth at front", "a", txt_Abut_a.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Width of wall", "B", txt_Abut_B.Text));
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
            file_content.Add(string.Format(kFormat, "Span of Longitudinal Girder", "L", txt_Abut_L.Text));
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
            file_content.Add(string.Format(kFormat, "Bearing Capacity", "p", txt_abut_p.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Vehicle Break is applied at a height", "h1", txt_abut_h1.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Bending Moment and Shear Force Factor", "Fact", txt_abut_fact.Text));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "Unit weight of Concrete", "γ_c", txt_Abut_gamma_c.Text));
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
            file_content.Add(string.Format(kFormat, "Depth of Deck Slab", "d2", txt_RCC_Pier_DS.Text));
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

            file_content.Add(string.Format(""));
            file_content.Add(string.Format("RCC PIER FORM2 USER INPUT DATA"));
            file_content.Add(string.Format("------------------------------"));
            file_content.Add(string.Format(""));
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


            #region Simple Section Data


            SaveRec.Clear();
            SaveRec.AddControls(tab_section_data);
            SaveRec.AddControls(tab_Post_Tension_Main_Girder);

            string str = "";

            file_content.Add(string.Format("NEW DATA"));
            file_content.Add(string.Format("--------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            foreach (var item in SaveRec)
            {
                file_content.Add(string.Format("{0} = {1}", item.Name, item.Text));
                
            }
            file_content.Add(string.Format("NEW DATA"));


            #endregion Simple Section Data
            
            //tab_Analysis_DL.Controls[
            
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
            if (iApp.IsDemo) return;
            


            string data_file = Bridge_Analysis.User_Input_Data;
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            Save_FormRecord.Read_All_Data(this, user_path); return;

            try
            {
                LongGirder.FilePath = user_path;
                Deck.FilePath = user_path;
                Cant.FilePath = user_path;
                Abut.FilePath = user_path;
                rcc_pier.FilePath = user_path;
            }
            catch (Exception ex) { }


            if (!File.Exists(data_file)) return;
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
                                if (c.Name.StartsWith("cmb"))
                                {
                                    //ComboBox cmb = (c as ComboBox);
                                    (c as ComboBox).SelectedItem = mlist.StringList[1];
                                }

                            }
                        }
                        catch (Exception ex) { }
                    }



                    #region Select Option
                    switch (kStr)
                    {
                        case "ANALYSIS OF BRIDGE DECK":
                            TOpt = ePSC_I_GirderOption.Analysis;
                            break;
                        case "MAIN GIRDER USER INPUTS":
                            TOpt = ePSC_I_GirderOption.MainGirder;
                            break;
                        case "DECK SLAB USER INPUT":
                        case "DECK SLAB USER INPUTS":
                            TOpt = ePSC_I_GirderOption.DeckSlab;
                            break;
                        case "CANTILEVER USER INPUT":
                            TOpt = ePSC_I_GirderOption.CantileverSlab;
                            break;
                        case "ABUTMENT USER INPUT":
                            TOpt = ePSC_I_GirderOption.Abutment;
                            break;
                        case "RCC PIER FORM1 USER INPUT DATA":
                            TOpt = ePSC_I_GirderOption.RCCPier_1;
                            break;
                        case "RCC PIER FORM2 USER INPUT DATA":
                            TOpt = ePSC_I_GirderOption.RCCPier_2;
                            break;
                        case "MOVING LOAD INPUT":
                            TOpt = ePSC_I_GirderOption.MovingLoad;
                            dgv_live_load.Rows.Clear();
                            break;
                    }
                    #endregion Select Option

                    if (TOpt == ePSC_I_GirderOption.MovingLoad)
                    {
                        mlist_mov_ll = new MyList(kStr, ',');
                    }

                    if (mlist.Count == 3 || TOpt == ePSC_I_GirderOption.MovingLoad)
                    {
                        if (TOpt != ePSC_I_GirderOption.MovingLoad)
                            kStr = MyList.RemoveAllSpaces(mlist.StringList[1].Trim().TrimEnd().TrimStart());
                        try
                        {
                            switch (TOpt)
                            {
                                #region Chiranjit Select Data
                                case ePSC_I_GirderOption.Analysis:
                                    #region ANALYSIS OF BRIDGE DECK
                                    switch (kStr)
                                    {
                                        case "L":
                                            L = mlist.GetDouble(2);
                                            break;
                                        case "B":
                                            B = mlist.GetDouble(2);
                                            break;
                                        case "CW":
                                            CW = mlist.GetDouble(2);
                                            break;
                                        case "CL":
                                            CL = mlist.GetDouble(2);
                                            break;
                                        case "CR":
                                            CR = mlist.GetDouble(2);
                                            break;
                                        case "Ds":
                                            Ds = mlist.GetDouble(2);
                                            break;
                                        case "Y_c":
                                            Y_c = mlist.GetDouble(2);
                                            break;
                                        case "Ang":
                                            Ang = mlist.GetDouble(2);
                                            break;
                                        case "NMG":
                                            NMG = mlist.GetDouble(2);
                                            break;
                                        case "DMG":
                                            DMG = mlist.GetDouble(2);
                                            break;
                                        case "BMG":
                                            BMG = mlist.GetDouble(2);
                                            break;
                                        case "NCG":
                                            NCG = mlist.GetDouble(2);
                                            break;
                                        case "DCG":
                                            DCG = mlist.GetDouble(2);
                                            break;
                                        case "BCG":
                                            BCG = mlist.GetDouble(2);
                                            break;
                                        case "Dw":
                                            Dw = mlist.GetDouble(2);
                                            break;
                                        case "Y_w":
                                            Y_w = mlist.GetDouble(2);
                                            break;
                                        case "Wp":
                                            Wp = mlist.GetDouble(2);
                                            break;
                                        case "Hp":
                                            Hp = mlist.GetDouble(2);
                                            break;
                                        case "Bs":
                                            Bs = mlist.GetDouble(2);
                                            break;
                                        case "Hs":
                                            Hs = mlist.GetDouble(2);
                                            break;
                                        case "Wps":
                                            Wps = mlist.GetDouble(2);
                                            break;
                                        case "Hps":
                                            Hps = mlist.GetDouble(2);
                                            break;
                                        case "swf":
                                            swf = mlist.GetDouble(2);
                                            break;
                                    }
                                    #endregion
                                    break;
                                case ePSC_I_GirderOption.MovingLoad:
                                    #region MOVING LOAD INPUT
                                    if (mlist_mov_ll.Count == 5)
                                    {
                                        dgv_live_load.Rows.Add(mlist_mov_ll.StringList.ToArray());
                                    }
                                    if (mlist.Count == 3)
                                        if (mlist.StringList[1] == "LG")
                                            txt_LL_load_gen.Text = mlist.StringList[2];
                                    #endregion ANALYSIS OF BRIDGE DECK
                                    break;
                                case ePSC_I_GirderOption.MainGirder:
                                    #region LONG GIRDER USER INPUT
                                    switch (kStr)
                                    {
                                        case "L":
                                            txt_Long_L.Text = mlist.StringList[2];
                                            break;
                                        //case "B":
                                        //    txt_Long_bw.Text = mlist.StringList[2];
                                        //    break;
                                        //case "a":
                                        //    txt_long_a.Text = mlist.StringList[2];
                                        //    break;
                                        case "d":
                                            txt_Long_d.Text = mlist.StringList[2];
                                            break;
                                        //case "b":
                                        //    txt_Long_Dim_d.Text = mlist.StringList[2];
                                        //    break;
                                        //case "bw":
                                        //    txt_Long_bw.Text = mlist.StringList[2];
                                        //    break;
                                        //case "d1":
                                        //    txt_Long_d1.Text = mlist.StringList[2];
                                        //    break;
                                        //case "d2":
                                        //    txt_Long_d2.Text = mlist.StringList[2];
                                        //    break;
                                        case "SMG":
                                            txt_Long_SMG.Text = mlist.StringList[2];
                                            break;
                                        case "SCG":
                                            txt_Long_SCG.Text = mlist.StringList[2];
                                            break;
                                        case "fck":
                                            cmb_long_fck.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "doc":
                                            txt_main_girder_doc.Text = mlist.StringList[2];
                                            break;
                                        case "fci":
                                            txt_Long_fci.Text = mlist.StringList[2];
                                            break;
                                        case "NS":
                                            txt_main_girder_NS.Text = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_long_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "dos":
                                            txt_main_girder_dos.Text = mlist.StringList[2];
                                            break;
                                        case "σ_cb":
                                            txt_long_sigma_c.Text = mlist.StringList[2];
                                            break;
                                        case "σ_st":
                                            txt_long_sigma_st.Text = mlist.StringList[2];
                                            break;
                                        case "SF":
                                            txt_Long_SF.Text = mlist.StringList[2];
                                            break;
                                        case "Q":
                                            txt_long_Q.Text = mlist.StringList[2];
                                            break;
                                        case "j":
                                            txt_long_j.Text = mlist.StringList[2];
                                            break;
                                        case "n":
                                            txt_long_eta.Text = mlist.StringList[2];
                                            break;
                                        case "FS":
                                            txt_main_girder_FS.Text = mlist.StringList[2];
                                            break;
                                        case "Es":
                                            txt_Long_Es.Text = mlist.StringList[2];
                                            break;
                                        case "Ec":
                                            txt_Long_Ec.Text = mlist.StringList[2];
                                            break;
                                        case "m":
                                            txt_long_m.Text = mlist.StringList[2];
                                            break;
                                        case "phi":
                                            txt_Long_phi.Text = mlist.StringList[2];
                                            break;
                                        case "ecc":
                                            txt_Long_ecc.Text = mlist.StringList[2];
                                            break;
                                        case "Atr":
                                            txt_Long_Atr.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion LONG GIRDER USER INPUT
                                    break;
                                case ePSC_I_GirderOption.DeckSlab:
                                    #region DECK SLAB USER INPUT
                                    switch (kStr)
                                    {
                                        case "S":
                                            txt_Deck_S.Text = mlist.StringList[2];
                                            break;
                                        case "CW":
                                            txt_Deck_CW.Text = mlist.StringList[2];
                                            break;
                                        case "Wk":
                                            txt_Deck_Wk.Text = mlist.StringList[2];
                                            break;
                                        case "tk":
                                            txt_Deck_tk.Text = mlist.StringList[2];
                                            break;
                                        case "Fw":
                                            txt_Deck_Fw.Text = mlist.StringList[2];
                                            break;
                                        case "twc":
                                            txt_Deck_twc.Text = mlist.StringList[2];
                                            break;
                                        case "Wt":
                                            txt_Deck_Wt.Text = mlist.StringList[2];
                                            break;
                                        case "SL":
                                            cmb_deck_select_load.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "Ltl":
                                            txt_Deck_Ltl.Text = mlist.StringList[2];
                                            break;
                                        case "Wtl":
                                            txt_Deck_Wtl.Text = mlist.StringList[2];
                                            break;
                                        case "L":
                                            txt_Deck_L.Text = mlist.StringList[2];
                                            break;
                                        case "bl":
                                            txt_Deck_bl.Text = mlist.StringList[2];
                                            break;
                                        case "B":
                                            txt_Deck_B.Text = mlist.StringList[2];
                                            break;
                                        case "bc":
                                            txt_Deck_bc.Text = mlist.StringList[2];
                                            break;
                                        case "Do":
                                            txt_Deck_Do.Text = mlist.StringList[2];
                                            break;
                                        case "fck":
                                            cmb_deck_fck.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "fci":
                                            txt_Deck_fci.Text = mlist.StringList[2];
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
                                        case "Q":
                                            txt_Deck_Q.Text = mlist.StringList[2];
                                            break;
                                        case "j":
                                            txt_Deck_j.Text = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_deck_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_Deck_gamma_c.Text = mlist.StringList[2];
                                            break;
                                        case "γ_wc":
                                            txt_Deck_gamma_w.Text = mlist.StringList[2];
                                            break;
                                        case "IF":
                                            txt_Deck_IF.Text = mlist.StringList[2];
                                            break;
                                        case "CF":
                                            txt_Deck_CF.Text = mlist.StringList[2];
                                            break;
                                        case "dm":
                                            txt_Deck_dm.Text = mlist.StringList[2];
                                            break;
                                        case "dd":
                                            txt_Deck_dd.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion DECK SLAB USER INPUT
                                    break;
                                case ePSC_I_GirderOption.CantileverSlab:
                                    #region CANTILEVER USER INPUT
                                    switch (kStr)
                                    {
                                        case "a1":
                                            txt_Cant_BMG.Text = mlist.StringList[2];
                                            break;
                                        case "Wp":
                                            txt_Cant_Wp.Text = mlist.StringList[2];
                                            break;
                                        case "d1":
                                            txt_Cant_d1.Text = mlist.StringList[2];
                                            break;
                                        case "a3":
                                            txt_Cant_a3.Text = mlist.StringList[2];
                                            break;
                                        case "d2":
                                            txt_Cant_d2.Text = mlist.StringList[2];
                                            break;
                                        case "d3":
                                            txt_Cant_d3.Text = mlist.StringList[2];
                                            break;
                                        case "SL":
                                            cmb_cant_select_load.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "AL":
                                            txt_Cant_w1.Text = mlist.StringList[2];
                                            break;
                                        case "LW":
                                            txt_Cant_a4.Text = mlist.StringList[2];
                                            break;
                                        case "IF":
                                            txt_Cant_fact.Text = mlist.StringList[2];
                                            break;
                                        case "a5":
                                            txt_Cant_a5.Text = mlist.StringList[2];
                                            break;
                                        case "a6":
                                            txt_Cant_a6.Text = mlist.StringList[2];
                                            break;
                                        case "d4":
                                            txt_Cant_d4.Text = mlist.StringList[2];
                                            break;
                                        case "w2":
                                            txt_Cant_w2.Text = mlist.StringList[2];
                                            break;
                                        case "a7":
                                            txt_Cant_a7.Text = mlist.StringList[2];
                                            break;
                                        case "RCC_X":
                                            txt_Cant_RCC_X.Text = mlist.StringList[2];
                                            break;
                                        case "RCC_Y":
                                            txt_Cant_RCC_Y.Text = mlist.StringList[2];
                                            break;
                                        case "WHR":
                                            txt_Cant_width_of_hand_rail.Text = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_Cant_gamma_c.Text = mlist.StringList[2];
                                            break;
                                        case "γ_wc":
                                            txt_Cant_gamma_wc.Text = mlist.StringList[2];
                                            break;
                                        case "cc":
                                            txt_Cant_cover.Text = mlist.StringList[2];
                                            break;
                                        case "fck":
                                            cmb_cant_fck.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_c":
                                            txt_cant_sigma_c.Text = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_cant_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "σ_st":
                                            txt_cant_sigma_st.Text = mlist.StringList[2];
                                            break;
                                        case "m":
                                            txt_cant_m.Text = mlist.StringList[2];
                                            break;
                                        case "j":
                                            txt_cant_j.Text = mlist.StringList[2];
                                            break;
                                        case "Q":
                                            txt_cant_Q.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion CANTILEVER USER INPUT
                                    break;
                                case ePSC_I_GirderOption.Abutment:
                                    #region ABUTMENT USER INPUT


                                    switch (kStr)
                                    {
                                        case "DMG":
                                            txt_Abut_d1.Text = mlist.StringList[2];
                                            break;
                                        case "t":
                                            txt_Abut_t.Text = mlist.StringList[2];
                                            break;
                                        case "H":
                                            txt_Abut_H.Text = mlist.StringList[2];
                                            break;
                                        case "a":
                                            txt_Abut_a.Text = mlist.StringList[2];
                                            break;
                                        case "B":
                                            txt_Abut_B.Text = mlist.StringList[2];
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
                                            txt_Abut_L.Text = mlist.StringList[2];
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
                                            txt_abut_p.Text = mlist.StringList[2];
                                            break;
                                        case "h1":
                                            txt_abut_h1.Text = mlist.StringList[2];
                                            break;
                                        case "Fact":
                                            txt_abut_fact.Text = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_Abut_gamma_c.Text = mlist.StringList[2];
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
                                case ePSC_I_GirderOption.RCCPier_1:
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
                                            txt_RCC_Pier_DS.Text = mlist.StringList[2];
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
                                case ePSC_I_GirderOption.RCCPier_2:
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
        #endregion Chiranjit [2012 07 13]

        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L.Text = "0";
                txt_Ana_L.Text = "36.0";
                txt_Ana_B.Text = "13.4";
                txt_Ana_CW.Text = "10.75";

                //string str = "This is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "Email at: techsoft@consultant.com, dataflow@mail.com\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]

        private void txt_SD_1_W_TextChanged(object sender, EventArgs e)
        {
            PSC_Section_Properties();
        }

        private void PSC_Section_Properties()
        {
            PSC_Section_Properties(false);
        }
        private void PSC_Section_Properties(bool IsFileWrite)
        {


            //sec_1.Title = "1. At Simple Section at Mid Span";
            //sec_2.Title = "2. At Simple Section at End Span";
            //sec_3.Title = "3. Cross/Transverse Members";

            sec_1.Title = "Dimensions for Simple Mid Span Sections (From 3-3 to 9-9)";
            sec_2.Title = "Dimensions for Simple End Span Sections (1-1 & 2-2)";
            sec_3.Title = "Dimensions Cross/Transverse Sections";
            sec_4.Title = "Dimensions for Composite (Girder + Deck Slab) Mid Span Sections (From 3-3 to 9-9)";
            sec_5.Title = "Dimensions for Composite (Girder + Deck Slab) End Span Sections (1-1 & 2-2)";



            sec_1.W = MyList.StringToDouble(txt_SD_1_W.Text, 0.0);
            sec_1.H = MyList.StringToDouble(txt_SD_1_H.Text, 0.0);
            sec_1.b1 = MyList.StringToDouble(txt_SD_1_b1.Text, 0.0);
            sec_1.b2 = MyList.StringToDouble(txt_SD_1_b2.Text, 0.0);
            sec_1.d1 = MyList.StringToDouble(txt_SD_1_d1.Text, 0.0);
            sec_1.d2 = MyList.StringToDouble(txt_SD_1_d2.Text, 0.0);
            sec_1.d3 = MyList.StringToDouble(txt_SD_1_d3.Text, 0.0);
            sec_1.d4 = MyList.StringToDouble(txt_SD_1_d4.Text, 0.0);
            sec_1.d5 = MyList.StringToDouble(txt_SD_1_d5.Text, 0.0);

            txt_SD_1_A.Text = sec_1.A.ToString("E3");
            txt_SD_1_Ax.Text = sec_1.AX.ToString("E3");
            txt_SD_1_Axx.Text = sec_1.AXX.ToString("E3");
            txt_SD_1_Iself.Text = sec_1.Iself.ToString("E3");
            txt_SD_1_Yt.Text = sec_1.Yt.ToString("F3");
            txt_SD_1_Yb.Text = sec_1.Yb.ToString("F3");
            txt_SD_1_Izz.Text = sec_1.Izz.ToString("E3");
            txt_SD_1_Ixx.Text = sec_1.Ixx.ToString("E3");
            txt_SD_1_Zt.Text = sec_1.Zt.ToString("E3");
            txt_SD_1_Zb.Text = sec_1.Zb.ToString("E3");



            sec_2.W = MyList.StringToDouble(txt_SD_2_W.Text, 0.0);
            sec_2.H = MyList.StringToDouble(txt_SD_2_H.Text, 0.0);
            sec_2.b1 = MyList.StringToDouble(txt_SD_2_b1.Text, 0.0);
            sec_2.b2 = MyList.StringToDouble(txt_SD_2_b2.Text, 0.0);
            sec_2.d1 = MyList.StringToDouble(txt_SD_2_d1.Text, 0.0);
            sec_2.d2 = MyList.StringToDouble(txt_SD_2_d2.Text, 0.0);
            sec_2.d3 = MyList.StringToDouble(txt_SD_2_d3.Text, 0.0);
            sec_2.d4 = MyList.StringToDouble(txt_SD_2_d4.Text, 0.0);
            sec_2.d5 = MyList.StringToDouble(txt_SD_2_d5.Text, 0.0);



            txt_SD_2_A.Text = sec_2.A.ToString("E3");
            txt_SD_2_Ax.Text = sec_2.AX.ToString("E3");
            txt_SD_2_Axx.Text = sec_2.AXX.ToString("E3");
            txt_SD_2_Iself.Text = sec_2.Iself.ToString("E3");
            txt_SD_2_Yt.Text = sec_2.Yt.ToString("F3");
            txt_SD_2_Yb.Text = sec_2.Yb.ToString("F3");
            txt_SD_2_Izz.Text = sec_2.Izz.ToString("E3");
            txt_SD_2_Ixx.Text = sec_2.Ixx.ToString("E3");
            txt_SD_2_Zt.Text = sec_2.Zt.ToString("E3");
            txt_SD_2_Zb.Text = sec_2.Zb.ToString("E3");


            sec_3.W = MyList.StringToDouble(txt_SD_3_W.Text, 0.0);
            sec_3.H = MyList.StringToDouble(txt_SD_3_H.Text, 0.0);
            sec_3.b1 = MyList.StringToDouble(txt_SD_3_b1.Text, 0.0);
            sec_3.b2 = MyList.StringToDouble(txt_SD_3_b2.Text, 0.0);
            sec_3.d1 = MyList.StringToDouble(txt_SD_3_d1.Text, 0.0);
            sec_3.d2 = MyList.StringToDouble(txt_SD_3_d2.Text, 0.0);
            sec_3.d3 = MyList.StringToDouble(txt_SD_3_d3.Text, 0.0);
            sec_3.d4 = MyList.StringToDouble(txt_SD_3_d4.Text, 0.0);
            sec_3.d5 = MyList.StringToDouble(txt_SD_3_d5.Text, 0.0);


            txt_SD_3_A.Text = sec_3.A.ToString("E3");
            txt_SD_3_Ax.Text = sec_3.AX.ToString("E3");
            txt_SD_3_Axx.Text = sec_3.AXX.ToString("E3");
            txt_SD_3_Iself.Text = sec_3.Iself.ToString("E3");
            txt_SD_3_Yt.Text = sec_3.Yt.ToString("F3");
            txt_SD_3_Yb.Text = sec_3.Yb.ToString("F3");
            txt_SD_3_Izz.Text = sec_3.Izz.ToString("E3");
            txt_SD_3_Ixx.Text = sec_3.Ixx.ToString("E3");
            txt_SD_3_Zt.Text = sec_3.Zt.ToString("E3");
            txt_SD_3_Zb.Text = sec_3.Zb.ToString("E3");


            sec_4.W = MyList.StringToDouble(txt_SD_4_W.Text, 0.0);
            sec_4.H = MyList.StringToDouble(txt_SD_4_H.Text, 0.0);
            sec_4.b1 = MyList.StringToDouble(txt_SD_4_b1.Text, 0.0);
            sec_4.b2 = MyList.StringToDouble(txt_SD_4_b2.Text, 0.0);
            sec_4.d1 = MyList.StringToDouble(txt_SD_4_d1.Text, 0.0);
            sec_4.d2 = MyList.StringToDouble(txt_SD_4_d2.Text, 0.0);
            sec_4.d3 = MyList.StringToDouble(txt_SD_4_d3.Text, 0.0);
            sec_4.d4 = MyList.StringToDouble(txt_SD_4_d4.Text, 0.0);
            sec_4.d5 = MyList.StringToDouble(txt_SD_4_d5.Text, 0.0);
            sec_4.ds = MyList.StringToDouble(txt_SD_4_ds.Text, 0.0);


            txt_SD_4_A.Text = sec_4.A.ToString("E3");
            txt_SD_4_Ax.Text = sec_4.AX.ToString("E3");
            txt_SD_4_Axx.Text = sec_4.AXX.ToString("E3");
            txt_SD_4_Iself.Text = sec_4.Iself.ToString("E3");
            txt_SD_4_Yt.Text = sec_4.Yt.ToString("F3");
            txt_SD_4_Yb.Text = sec_4.Yb.ToString("F3");
            txt_SD_4_Izz.Text = sec_4.Izz.ToString("E3");
            txt_SD_4_Ixx.Text = sec_4.Ixx.ToString("E3");
            txt_SD_4_Zt.Text = sec_4.Zt.ToString("E3");
            txt_SD_4_Zb.Text = sec_4.Zb.ToString("E3");


            sec_5.W = MyList.StringToDouble(txt_SD_5_W.Text, 0.0);
            sec_5.H = MyList.StringToDouble(txt_SD_5_H.Text, 0.0);
            sec_5.b1 = MyList.StringToDouble(txt_SD_5_b1.Text, 0.0);
            sec_5.b2 = MyList.StringToDouble(txt_SD_5_b2.Text, 0.0);
            sec_5.d1 = MyList.StringToDouble(txt_SD_5_d1.Text, 0.0);
            sec_5.d2 = MyList.StringToDouble(txt_SD_5_d2.Text, 0.0);
            sec_5.d3 = MyList.StringToDouble(txt_SD_5_d3.Text, 0.0);
            sec_5.d4 = MyList.StringToDouble(txt_SD_5_d4.Text, 0.0);
            sec_5.d5 = MyList.StringToDouble(txt_SD_5_d5.Text, 0.0);
            sec_5.ds = MyList.StringToDouble(txt_SD_5_ds.Text, 0.0);


            txt_SD_5_A.Text = sec_5.A.ToString("E3");
            txt_SD_5_Ax.Text = sec_5.AX.ToString("E3");
            txt_SD_5_Axx.Text = sec_5.AXX.ToString("E3");
            txt_SD_5_Iself.Text = sec_5.Iself.ToString("E3");
            txt_SD_5_Yt.Text = sec_5.Yt.ToString("F3");
            txt_SD_5_Yb.Text = sec_5.Yb.ToString("F3");
            txt_SD_5_Izz.Text = sec_5.Izz.ToString("E3");
            txt_SD_5_Ixx.Text = sec_5.Ixx.ToString("E3");
            txt_SD_5_Zt.Text = sec_5.Zt.ToString("E3");
            txt_SD_5_Zb.Text = sec_5.Zb.ToString("E3");






            //txt_SP_A1.Text = sec_1.A1.ToString("f3");
            //txt_SP_X1.Text = sec_1.X1.ToString("f3");
            //txt_SP_Iself1.Text = sec_1.Iself1.ToString("E2");
            //txt_SP_A2.Text = sec_1.A2.ToString("f3");
            //txt_SP_X2.Text = sec_1.X2.ToString("f3");
            //txt_SP_Iself2.Text = sec_1.Iself2.ToString("E2");



            List<string> ll = new List<string>();

            ll.Add("\t\t***********************************************");
            ll.Add("\t\t*            ASTRA Pro Release 22             *");
            ll.Add("\t\t*        TechSOFT Engineering Services        *");
            ll.Add("\t\t*                                             *");
            ll.Add("\t\t*          POST TENSIONED MAIN GIRDER         *");
            ll.Add("\t\t*             SECTION PROPERTIES              *");
            ll.Add("\t\t***********************************************");
            ll.Add("\t\t----------------------------------------------");
            ll.Add("\t\tTHIS REPORT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            ll.Add("\t\t----------------------------------------------");
            ll.AddRange(sec_1.Get_Results());
            ll.AddRange(sec_2.Get_Results());
            ll.AddRange(sec_3.Get_Results());
            ll.AddRange(sec_4.Get_Results());
            ll.AddRange(sec_5.Get_Results());
            ll.Add(string.Format("FINAL TABLE"));
            ll.Add(string.Format("-----------"));
            ll.Add("        " + string.Format(sec_1.Table_HEAD));
            ll.Add("        " + string.Format(sec_1.Table_Unit));
            ll.Add("");
            ll.Add(string.Format("CONCRETE SECTION"));
            ll.Add(string.Format("----------------"));
            ll.Add("Mid Span" + string.Format(sec_1.Table_Row));
            ll.Add("End Span" + string.Format(sec_2.Table_Row));
            ll.Add("Cross   " + string.Format(sec_3.Table_Row));
            ll.Add("");
            ll.Add(string.Format("COMPOSITE SECTION"));
            ll.Add(string.Format("-----------------"));
            ll.Add("Mid Span" + string.Format(sec_4.Table_Row));
            ll.Add("End Span" + string.Format(sec_5.Table_Row));
            ll.Add(string.Format(""));
            ll.Add(string.Format(""));
            ll.Add(string.Format("---------------------------------------------------------------"));
            ll.Add(string.Format("-----------            END OF REPORT           ----------------"));
            ll.Add(string.Format("---------------------------------------------------------------"));
            ll.Add(string.Format(""));
            ll.Add(string.Format(""));
            ll.Add(string.Format(""));
            ll.Add(string.Format(""));
            ll.Add(string.Format("---------------------------------------------------------------------------"));
            ll.Add(string.Format("---------            Thank you for using ASTRA Pro          ---------------"));
            ll.Add(string.Format("---------------------------------------------------------------------------"));
            rtb_cal_sections.Lines = ll.ToArray();
            if (IsFileWrite)
            {
                string fn = Path.Combine(user_path, "PSC_Sections.TXT");
                File.WriteAllLines(fn, ll.ToArray());
            }
            Set_Section_Properties();
        }
        public void Set_Section_Properties()
        {
            string Long_G = "";
            string END_Long_G = "";
            string cross_dd = "";

            //SteelTrussDeckSlab

            //SteelTrussMemberAnalysis mem_ana = new SteelTrussMemberAnalysis(txt_Ana_analysis_file.Text
        }

        private void txt_CD_AB_1_TextChanged(object sender, EventArgs e)
        {

            Long_Girder_Initialize_InputData();
            //double ab1 = MyList.StringToDouble(txt_CD_AB_1.Text, 0.0);
            //double ab2 = MyList.StringToDouble(txt_CD_AB_2.Text, 0.0);
            //double dba = MyList.StringToDouble(txt_CD_dba.Text, 0.0);
            //double vwc = MyList.StringToDouble(txt_CD_vwc.Text, 0.0);
            //double vfc = MyList.StringToDouble(txt_CD_vfc.Text, 0.0);
            //double idac = MyList.StringToDouble(txt_CD_idac.Text, 0.0);
            //double abcd_nspc = MyList.StringToDouble(txt_CD_abcd_nspc.Text, 0.0);
            //double abcd_uss = MyList.StringToDouble(txt_CD_abcd_uss.Text, 0.0);
            //double abcd_sf = MyList.StringToDouble(txt_CD_abcd_sf.Text, 0.0);
            //double abcd_fss = MyList.StringToDouble(txt_CD_abcd_fss.Text, 0.0);
            //double abcd_fc = MyList.StringToDouble(txt_CD_abcd_fc.Text, 0.0);
            //double abcd_csac = MyList.StringToDouble(txt_CD_abcd_csac.Text, 0.0);


            //double e_nspc = MyList.StringToDouble(txt_CD_e_nspc.Text, 0.0);
            //double e_uss = MyList.StringToDouble(txt_CD_e_uss.Text, 0.0);
            //double e_sf = MyList.StringToDouble(txt_CD_e_sf.Text, 0.0);
            //double e_fss = MyList.StringToDouble(txt_CD_e_fss.Text, 0.0);
            //double e_fc = MyList.StringToDouble(txt_CD_e_fpc.Text, 0.0);
            //double e_csac = MyList.StringToDouble(txt_CD_e_csac.Text, 0.0);
        }

        //Chiranjit [2012 09 21]
        private void rbtn_2_lane_CheckedChanged(object sender, EventArgs e)
        {
            //Defaulf
            if (txt_Ana_analysis_file.Text == "")
            {
                dgv_live_load.Rows.Clear();

                if (rbtn_2_lane.Checked)
                {
                    txt_Ana_B.Text = "12.0";
                    txt_Ana_CW.Text = "7.5";
                    txt_Ana_NMG.Text = "6";
                    txt_Long_SMG.Text = "2.0";
                    txt_Ana_CL.Text = "1.0";
                    txt_Ana_CR.Text = "1.0";
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "2.50", txt_XINCR.Text, txt_Load_Impact.Text);
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "4.50", txt_XINCR.Text, txt_Load_Impact.Text);
                    txt_Ana_Bs.Text = "1.5";
                    txt_Ana_Hs.Text = "0.0";

                }
                else if (rbtn_3_lane.Checked)
                {
                    txt_Ana_B.Text = "13.4";
                    txt_Ana_CW.Text = "10.75";
                    txt_Ana_NMG.Text = "5";
                    txt_Long_SMG.Text = "2.725";
                    txt_Ana_CL.Text = "1.25";
                    txt_Ana_CR.Text = "1.25";

                    txt_Ana_Bs.Text = "1.75";
                    txt_Ana_Hs.Text = "0.0";


                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "1.50", txt_XINCR.Text, txt_Load_Impact.Text);
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "4.50", txt_XINCR.Text, txt_Load_Impact.Text);
                    dgv_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, "7.50", txt_XINCR.Text, txt_Load_Impact.Text);

                }
            }
        }
        public void Long_Girder_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                LongGirder.L = MyList.StringToDouble(txt_Long_L.Text, 0.0);
                LongGirder.B = MyList.StringToDouble(txt_Long_B.Text, 0.0);
                LongGirder.a = MyList.StringToDouble(txt_main_girder_a.Text, 0.0);
                LongGirder.d = MyList.StringToDouble(txt_Long_d.Text, 0.0);
                LongGirder.b = MyList.StringToDouble(txt_Long_B.Text, 0.0);
                LongGirder.bw = MyList.StringToDouble(txt_main_girder_bw.Text, 0.0);
                LongGirder.d1 = MyList.StringToDouble(txt_main_girder_d1.Text, 0.0);
                LongGirder.d2 = MyList.StringToDouble(txt_main_girder_d2.Text, 0.0);
                LongGirder.fck = MyList.StringToDouble(cmb_long_fck.Text, 0.0);
                LongGirder.doc = MyList.StringToDouble(txt_main_girder_doc.Text, 0.0);
                LongGirder.fci = MyList.StringToDouble(txt_Long_fci.Text, 0.0);
                LongGirder.NS = MyList.StringToDouble(txt_main_girder_NS.Text, 0.0);
                LongGirder.fy = MyList.StringToDouble(cmb_long_fy.Text, 0.0);
                LongGirder.dos = MyList.StringToDouble(txt_main_girder_dos.Text, 0.0);
                LongGirder.sigma_cb = MyList.StringToDouble(txt_long_sigma_c.Text, 0.0);
                LongGirder.sigma_st = MyList.StringToDouble(txt_long_sigma_st.Text, 0.0);
                LongGirder.SF = MyList.StringToDouble(txt_Long_SF.Text, 0.0);
                LongGirder.m = MyList.StringToDouble(txt_long_m.Text, 0.0);
                LongGirder.Q = MyList.StringToDouble(txt_long_Q.Text, 0.0);
                LongGirder.j = MyList.StringToDouble(txt_long_j.Text, 0.0);
                LongGirder.eta = MyList.StringToDouble(txt_long_eta.Text, 0.0);
                LongGirder.FS = MyList.StringToDouble(txt_main_girder_FS.Text, 0.0);
                LongGirder.DL_BM_OG = MyList.StringToDouble(txt_DL_BM_OG.Text, 0.0);
                LongGirder.DL_BM_IG = MyList.StringToDouble(txt_DL_BM_IG.Text, 0.0);
                LongGirder.LL_BM_OG = MyList.StringToDouble(txt_LL_BM_OG.Text, 0.0);

                LongGirder.LL_BM_IG = MyList.StringToDouble(txt_LL_BM_IG.Text, 0.0);

                LongGirder.DL_SF_OG = MyList.StringToDouble(txt_DL_SF_OG.Text, 0.0);
                LongGirder.DL_SF_IG = MyList.StringToDouble(txt_DL_SF_IG.Text, 0.0);

                LongGirder.LL_SF_OG = MyList.StringToDouble(txt_LL_SF_OG.Text, 0.0);
                LongGirder.LL_SF_IG = MyList.StringToDouble(txt_LL_SF_IG.Text, 0.0);
                LongGirder.fp = MyList.StringToDouble(txt_main_girder_fp.Text, 0.0);


                LongGirder.space_long = MyList.StringToDouble(txt_Long_SMG.Text, 0.0);
                LongGirder.space_cross = MyList.StringToDouble(txt_Long_SMG.Text, 0.0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }

        private void btn_ana_view_section_Click(object sender, EventArgs e)
        {
            PSC_Section_Properties();
        }

        private void txt_DL_IG_M1_TextChanged(object sender, EventArgs e)
        {
            double d1, d3, d2, max;

            #region Bending Moment
            
            d1 = MyList.StringToDouble(txt_LL_IG_M1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_LL_IG_M2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_LL_IG_M3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_LL_BM_IG.Text = (max * 10.0).ToString("f3");



            d1 = MyList.StringToDouble(txt_LL_OG_M1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_LL_OG_M2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_LL_OG_M3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_LL_BM_OG.Text = (max * 10.0).ToString("f3");



            d1 = MyList.StringToDouble(txt_DL_IG_M1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_DL_IG_M2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_DL_IG_M3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_DL_BM_IG.Text = (max * 10.0).ToString("f3");



            d1 = MyList.StringToDouble(txt_DL_OG_M1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_DL_OG_M2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_DL_OG_M3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_DL_BM_OG.Text = (max * 10.0).ToString("f3");
            #endregion

            #region Shear Force


            d1 = MyList.StringToDouble(txt_LL_IG_S1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_LL_IG_S2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_LL_IG_S3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_LL_SF_IG.Text = (max * 10.0).ToString("f3");



            d1 = MyList.StringToDouble(txt_LL_OG_S1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_LL_OG_S2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_LL_OG_S3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_LL_SF_OG.Text = (max * 10.0).ToString("f3");


            d1 = MyList.StringToDouble(txt_DL_IG_S1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_DL_IG_S2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_DL_IG_S3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_DL_SF_IG.Text = (max * 10.0).ToString("f3");



            d1 = MyList.StringToDouble(txt_DL_OG_S1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_DL_OG_S2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_DL_OG_S3.Text, 0.0);

            max = (d1 > d2 && d1 > d3) ? d1 : (d2 > d1 && d2 > d3) ? d2 : d3;
            txt_DL_SF_OG.Text = (max * 10.0).ToString("f3");

            #endregion  Shear Force
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
            iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
        }



        #region Create Project / Open Project

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
                IsCreate_Data = true;
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
                return eASTRADesignType.PSC_I_Girder_Bridge_WS_Short_Span;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]


    }

    public class PostTensionLongGirder_ShortSpan
    {
        IApplication iApp = null;
        public string rep_file_name = "";
        public string file_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_drawing_file = "";
        public string user_path = "";
        public bool is_process = false;

        public double B, L, a, d, b, bw, d1, d2, fck, doc, fci, NS, fy, dos, sigma_cb, sigma_st;
        public double SF, m, Q, j, FS, fp;
        public double eta; // η

        public double DL_BM_OG, DL_BM_IG;
        public double LL_BM_OG, LL_BM_IG;
        public double DL_SF_OG, DL_SF_IG;
        public double LL_SF_OG, LL_SF_IG;

        public double space_long, space_cross;


        string _A, _B, _C, _D, _E, _F, _G, _H, _I1, _I2, _J, _K, _L;



        public PostTensionLongGirder_ShortSpan(IApplication app)
        {
            iApp = app;
        }



        #region IReport Members

        public void Calculate_Long_Girder_Program()
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 5.0            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*       DESIGN OF PRESTRESSED BRIDGE          *");
                sw.WriteLine("\t\t*     POST TENSIONED RCC LONG. GIRDER         *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                #endregion

                #region USER'S DATA
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Outer Girder Dead Load Bending Moment  = {0} kN-m", DL_BM_OG);
                sw.WriteLine("Inner Girder Dead Load Bending Moment  = {0} kN-m", DL_BM_IG);

                sw.WriteLine("Outer Girder Live Load Bending Moment  = {0}kN-m", LL_BM_OG);
                sw.WriteLine("Inner Girder Live Load Bending Moment  = {0}kN-m", LL_BM_IG);

                sw.WriteLine("Outer Girder Dead Load Shear Force = {0} kN", DL_SF_OG);
                sw.WriteLine("Inner Girder Dead Load Shear Force = {0} kN", DL_SF_IG);

                sw.WriteLine("Outer Girder Live Load Shear Force = {0} kN", LL_SF_OG);
                sw.WriteLine("Inner Girder Live Load Shear Force = {0} kN", LL_SF_IG);

                sw.WriteLine("Span of Girder [L] = {0} m", L);
                sw.WriteLine("Width of Girder [B] = {0} m", B);
                sw.WriteLine("Dimension [a] = {0} mm                 Marked as (A) in the Drawing", a);
                _A = string.Format("{0} mm ", a);

                sw.WriteLine("Overall Depth of Girder [d] = {0} mm   Marked as (B) in the Drawing", d);
                _B = string.Format("{0} mm ", d);

                sw.WriteLine("Dimension [b] = {0} mm                 Marked as (C) in the Drawing", b);
                _C = string.Format("{0} mm ", b);


                sw.WriteLine("Dimension [bw] = {0} mm                Marked as (D) in the Drawing", bw);
                _D = string.Format("{0} mm ", bw);

                sw.WriteLine("Dimension [d1] = {0} mm                Marked as (E) in the Drawing", d1);
                _E = string.Format("{0} mm ", d1);

                sw.WriteLine("Dimension [d2] = {0} mm                Marked as (F) in the Drawing", d2);
                _F = string.Format("{0} mm ", d2);

                sw.WriteLine("Spacing of Main Long Girders = {0} mm  Marked as (G) in the Drawing", space_long * 1000);
                //(G) = Spacing of Main Girders 2500 mm.
                _G = string.Format("Spacing of Main Girders {0} mm ", space_long * 1000);

                sw.WriteLine("Spacing of Cross Girders = {0} mm      Marked as (H) in the Drawing", space_cross * 1000);
                //(H) = Spacing of Cross Girders 5000 mm.
                _H = string.Format("Spacing of Cross Girders {0} mm ", space_cross * 1000);


                sw.WriteLine("Concrete Grade [fck] = M {0} = {0} N/sq.mm", fck);
                sw.WriteLine("Post Tension Cable Diameter [doc] = {0} mm", doc);
                sw.WriteLine("Cube Strength at Transfer [fci] = {0} N/sq.mm", fci);
                sw.WriteLine("Freyssinet Anchorable Number of Strands [NS] = {0}", NS);
                sw.WriteLine("Steel Grade [fy] = Fe {0} = {0}  N/sq.mm", fy);
                sw.WriteLine("Diameter of Strands [dos] = {0} mm", dos);
                sw.WriteLine("Permissible compressive stress in concrete [σ_cb] = {0}  N/sq.mm", sigma_cb);
                sw.WriteLine("Permissible tensile stress in steel [σ_st] = {0}  N/sq.mm", sigma_st);
                sw.WriteLine("Strand Factor [SF] = {0}", SF);
                sw.WriteLine("Moduler Ratio [m] = {0}", m);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("Loss Ratio [n] = {0}", eta);
                sw.WriteLine("Force per Strand [FS] = {0} kN", FS);
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion


                #region STEP 1 : CROSS SECTION OF DECK SLAB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : PROPERTIES OF MAIN GIRDER SECTION ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double _x = ((bw / 2.0) * (d - d1 - d2) * (d - d1 - d2) + (b * d2) * (d - d1 - d2 + (d2 / 2.0)) - (b * d1 * (d1 / 2.0))) / ((a * d1) + (bw * (d - d1 - d2)) + (b * d2));

                double x1 = (bw / 2.0) * (d - d1 - d2) * (d - d1 - d2);
                double x2 = (a * d2) * (d - d1 - d2 + (d2 / 2.0));
                double x3 = (b * d1 * (d1 / 2.0));
                double x4 = ((b * d1) + (bw * (d - d1 - d2)) + (a * d2));


                _x = (x1 + x2 - x3) / x4;

                double x = (int)(_x / 10.0);
                x += 1;
                x *= 10;


                sw.WriteLine("Depth of Neutral Axis = x = (x1 + x2 + x3) / x4");
                sw.WriteLine();

                x1 = double.Parse(x1.ToString("0.000"));
                sw.WriteLine("where x1 = (w / 2.0) * (d - d1 - d2) * (d - d1 - d2)", x1);
                sw.WriteLine("         = ({0} / 2.0) * ({1} - {2} - {3})^2", bw, d, d1, d2);
                sw.WriteLine("         = {0:E2} ", x1);
                sw.WriteLine();


                x2 = double.Parse(x2.ToString("0.000"));
                sw.WriteLine(" and  x2 = (b * d2) * (d - d1 - d2 + (d2 / 2.0))");
                sw.WriteLine("         = ({0} * {1}) * ({2} - {3} - {1} + ({1} / 2.0))", b, d2, d, d1);
                sw.WriteLine("         = {0:E2} ", x2);
                sw.WriteLine();

                x3 = double.Parse(x3.ToString("0.000"));
                sw.WriteLine(" and  x3 = (a * d1 * (d1 / 2.0))");
                sw.WriteLine("         = ({0} * {1} * ({1} / 2.0)) ", a, d1);
                sw.WriteLine("         = {0:E2}", x3);
                sw.WriteLine();


                x4 = double.Parse(x4.ToString("0.000"));
                sw.WriteLine(" and  x4 = ((a * d1) + (w * (d - d1 - d2)) + (b * d2))");
                sw.WriteLine("         = (({0} * {1}) + ({2} * ({3} - {1} - {4})) + ({5} * {4}))", a, d1, bw, d, d2, b);
                sw.WriteLine("         = {0:E2}", x4);
                sw.WriteLine();

                _x = double.Parse(_x.ToString("0.000"));
                sw.WriteLine("So, Depth of Neutral Axis = x = (x1 + x2 + x3) / x4");
                sw.WriteLine("                         = ({0:E2} + {1:E2} + {2:E2}) / {3:E2}", x1, x2, x3, x4);
                sw.WriteLine("                         = {0} mm", _x);
                sw.WriteLine("                         ≈ {0} mm", x);
                sw.WriteLine();


                double yt = x + d1;
                sw.WriteLine("yt = x + d1 = {0} + {1} = {2}", x, d1, yt);
                sw.WriteLine();
                double yb = d - yt;
                sw.WriteLine("yb = d - yt = {0} + {1} = {2}", d, yt, yb);
                sw.WriteLine();

                double I = ((b * d1) * (x + (d1 / 2.0)) * (x + (d1 / 2.0)))
                     + ((bw * x * x * x) / 3.0)
                     + ((bw * ((d - d1 - d2 - x) * (d - d1 - d2 - x) * (d - d1 - d2 - x))) / 3.0)
                     + (a * d2 * ((d - d1 - d2 - x + (d2 / 2.0)) * (d - d1 - d2 - x + (d2 / 2.0))));

                double I1 = ((a * d1) * (x + (d1 / 2.0)) * (x + (d1 / 2.0)));
                double I2 = ((bw * x * x * x) / 3.0);
                double I3 = ((bw * ((d - d1 - d2 - x) * (d - d1 - d2 - x) * (d - d1 - d2 - x))) / 3.0);
                double I4 = (b * d2 * ((d - d1 - d2 - x + (d2 / 2.0)) * (d - d1 - d2 - x + (d2 / 2.0))));


                I = (int)(I / 10E7);

                I1 = double.Parse(I1.ToString("0.00"));
                I2 = double.Parse(I2.ToString("0.00"));
                I3 = double.Parse(I3.ToString("0.00"));
                I4 = double.Parse(I4.ToString("0.00"));

                sw.WriteLine("Moment of Inertia = I = I1 + I2 + I3 + I4");
                sw.WriteLine();
                sw.WriteLine("where I1 = ((a * d1) * (x + (d1 / 2.0))^2)");
                sw.WriteLine("         = (({0} * {1}) * ({2} + ({1} / 2.0))^2)))", a, d1, x);
                sw.WriteLine("         = {0:E2} ", I1);
                sw.WriteLine();
                sw.WriteLine("  and I2 = ((bw * x**3) / 3.0)");
                sw.WriteLine("         = (({0} * {1}^3) / 3.0)", bw, x);
                sw.WriteLine("         = {0:E2}", I2);
                sw.WriteLine();
                sw.WriteLine("  and I3 = ((bw * (d - d1 - d2 - x)^3) / 3.0)");
                sw.WriteLine("         = (({0} * ({1} - {2} - {3} - {4})^3) / 3.0)", bw, d, d1, d2, x);
                sw.WriteLine("         = {0:E2} ", I3);
                sw.WriteLine();
                sw.WriteLine("  and I4 = (b * d2 * (d - d1 - d2 - x + (d2 / 2.0))^3)");
                sw.WriteLine("         = ({0} * {1} * ({2} - {3} - {1} - {4} + ({1} / 2.0))^3)", b, d2, d, d1, x);
                sw.WriteLine("         = {0:E2} ", I4);
                sw.WriteLine();
                sw.WriteLine(" I = I1 + I2 + I3 + I4");
                sw.WriteLine("   = {0:E2} + {1:E2} + {2:E2} + {3:E2}", I1, I2, I3, I4);
                sw.WriteLine("   = {0} * 10E7", I);
                sw.WriteLine();


                double A = b * d1 + bw * (d - d1 - d2) + (a * d2);

                A = (int)(A / 10E3);
                sw.WriteLine();
                sw.WriteLine("A = a * d1 + bw * (d - d1 - d2) + (b * d2)");
                sw.WriteLine("  = {0} * {1} + {2} * ({3} - {1} - {4}) + ({5} * {4})", a, d1, bw, d, d2, b);
                sw.WriteLine("  = {0} * 10E3", A);
                sw.WriteLine();


                double Zt = ((I * 10E7) / yt);
                double Zb = ((I * 10E7) / yb);
                sw.WriteLine("Zt = (I * 10E7) / yt");
                sw.WriteLine("   = ({0} * 10E7) / {1}", I, yt);
                sw.WriteLine("   = {0:E2}", Zt);
                sw.WriteLine();
                sw.WriteLine("Zb = (I * 10E7) / yb");
                sw.WriteLine("   = ({0} * 10E7) / {1}", I, yb);
                sw.WriteLine("   = {0:E2}", Zb);


                #endregion

                #region STEP 2 : PERMISSIBLE STRESSES
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : PERMISSIBLE STRESSES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("σ_cb = {0} N/sq.mm", sigma_cb);
                sw.WriteLine();
                sw.WriteLine("σ_st = {0}", sigma_st);
                sw.WriteLine();
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine();
                sw.WriteLine("fci = {0}", fci);
                sw.WriteLine();

                double fct = 0.45 * fci;
                fct = double.Parse(fct.ToString("0"));
                sw.WriteLine("fct = 0.45 * fci = 0.45 * {0} = {1} N/sq.mm", fci, fct);
                sw.WriteLine();


                // *** Problem fck = 20, but here use 50 why?
                double _fck = fck;

                //fck = 50;
                double fcw = 0.33 * fck;
                fcw = double.Parse(fcw.ToString("0.00"));
                sw.WriteLine("fcw = 0.33 * fck = 0.33 * {0} = {1} N/sq.mm", fck, fcw);
                sw.WriteLine();

                double ftt = 0;
                double ftw = 0;
                sw.WriteLine("ftt = 0 (Member class type = 1)");
                sw.WriteLine();
                sw.WriteLine("ftw = 0 (Member class type = 1)");
                sw.WriteLine();

                double Ec = 5700.0 * Math.Sqrt(fck);

                Ec = double.Parse(Ec.ToString("0"));
                sw.WriteLine("Ec = 5700 * √fck = 5700 * √{0} = {1} N/sq.mm = {2} kN/sq.mm", fck, Ec, (int)(Ec / 1000.0));
                sw.WriteLine();
                sw.WriteLine("η = {0}", eta);
                sw.WriteLine();


                double fbr = eta * fct - ftw;
                sw.WriteLine("fbr = η * fct - ftw");
                sw.WriteLine("    = {0} * {1} - {2}", eta, fct, ftw);
                sw.WriteLine("    = {0} N/sq.mm", fbr);
                sw.WriteLine();

                double ftr = fcw - eta * ftt;
                sw.WriteLine("ftr = fcw - η * ftt");
                sw.WriteLine("    = {0} - {1} * {2}", fcw, eta, ftt);
                sw.WriteLine("    = {0} N/sq.mm", ftr);
                sw.WriteLine();

                double Mg = DL_BM_OG;
                double Mq = LL_BM_OG;

                double Md = Mg + Mq;
                sw.WriteLine("Mg = {0} kN-m", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m", Mq);
                sw.WriteLine();
                sw.WriteLine("Md = Mg + Mq = {0} + {1} = {2} kN-m", Mg, Mq, Md);
                sw.WriteLine();

                double finf = (ftw / eta) + (Md * 10e5) / (eta * Zb);
                finf = double.Parse(finf.ToString("0.00"));
                sw.WriteLine("finf = (ftw / η) + Md / (η * Zb)");
                sw.WriteLine("     = ({0} / {1}) + {2}*10E5 / ({1} * {3:E2})", ftw, eta, Md, Zb);
                sw.WriteLine("     = {0:E2} N/sq.mm", finf);
                sw.WriteLine();

                double _Zb = (Mq * 10E5 + (1 - eta) * Mg * 10E5) / fbr;
                sw.WriteLine("Zb = [Mq + (1 - η) * Mg] / fbr");
                sw.WriteLine("   = [{0}*10E5  + (1 - {1}) * {2}*10E5 ] / {3}", Mq, eta, Mg, fbr);

                //_Zb = (_Zb / 10E7);
                //Zb = (Zb / 10E7);

                if (_Zb < Zb)
                {
                    sw.WriteLine("   = {0:E2} Cu.mm < {1:E2} Cu.mm", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine("So, the girder is adequate.");
                }
                else
                {
                    sw.WriteLine("   = {0} * 10E7 Cu.mm > {1} * 10E7 Cu.mm", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine("So, the girder is not adequate.");
                }

                #endregion

                #region STEP 3 : PRESTRESSING FORCE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : PRESTRESSING FORCE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double cover = 200;
                sw.WriteLine("Allowing for two rows of Cables, required Cover = 200 mm");
                sw.WriteLine();

                double e = (d - yt - cover);
                sw.WriteLine("Maximum Possible eccentricity = e = (d - yt - cover) ");
                sw.WriteLine("                              = ({0} - {1} - {2})", d, yt, cover);
                sw.WriteLine("                              = {0} mm", e);
                sw.WriteLine();
                sw.WriteLine("Prestressing Force is obtained as");
                sw.WriteLine();


                double p = (A * 10E3 * finf * Zb) / ((Zb) + (A * 10E3 * e));
                sw.WriteLine("p = (A * finf * Zb) / (Zb  + A * e)");
                sw.WriteLine("  = ({0}*10E3 * {1} * {2:E2}) / ({2:E2} + {0}*10E3 * {3})", A, finf, Zb, e);
                sw.WriteLine("  = {0:E2} N", p);
                p = p / 1000.0;
                sw.WriteLine("  = {0:E2} kN", p);
                sw.WriteLine();

                double Facts = SF;

                double Pk = NS * Facts * FS;
                Pk = double.Parse(Pk.ToString("0"));
                sw.WriteLine("Force in each Cable = Ns * Facts * Fs");
                sw.WriteLine("                    = {0} * {1} * {2}", NS, Facts, FS);
                sw.WriteLine("                    = {0} kN", Pk);
                sw.WriteLine();


                double Nc = p / Pk;
                sw.WriteLine("Required Number of Cables = Nc = {0:E2} / {1}", p, Pk);
                sw.WriteLine("                          = {0:f2} ", Nc);

                Nc = (int)Nc;

                Nc += 1;
                sw.WriteLine("                          = {0} ", Nc);
                sw.WriteLine();


                double ar_ech_strnd = Facts * Math.PI * dos * dos / 4.0;
                ar_ech_strnd = double.Parse(ar_ech_strnd.ToString("0"));
                sw.WriteLine("Area of each Strand = (Fact * π * dos*dos)/4.0");
                sw.WriteLine("                    = ({0} * π * {1}*{1}) / 4.0", Facts, dos);
                sw.WriteLine("                    = {0:f0} sq.mm", ar_ech_strnd);
                sw.WriteLine();
                sw.WriteLine("A cable contains NS = {0} strands,", NS);

                double total_area1 = NS * ar_ech_strnd;
                total_area1 = double.Parse(total_area1.ToString("0"));
                sw.WriteLine("Total Area = {0} * {1}", NS, ar_ech_strnd);
                sw.WriteLine("           = {0} sq.mm", total_area1);
                sw.WriteLine();

                double total_area2 = Nc * total_area1;
                sw.WriteLine("For {0} Cables, Total Area = {0} * {1} = {2} sq.mm        Marked as (I) in the Drawing", Nc, total_area1, total_area2);
                //(I) = Total 5 nos. Prestressing Cables.
                _I1 = string.Format("Total {0:f0} nos. Prestressing Cables.", Nc);
                // 7 nos. Strands per Cable, Are of each = 141 sq.mm
                _I2 = string.Format("{0:f0} nos. Strands per Cable, Are of each = {1} sq.mm", NS, ar_ech_strnd);



                sw.WriteLine();
                sw.WriteLine("The arrangement of Cables are shown in the Drawing.");



                #endregion

                #region STEP 4 : PERMISSIBLE TENDON ZONE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : PERMISSIBLE TENDON ZONE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At support Section,");
                sw.WriteLine();


                double e_less_value = ((Zb * fct) / (p * 1000)) - (Zb / (A * 10E3));
                e_less_value = double.Parse(e_less_value.ToString("0"));
                sw.WriteLine("e <= ((Zb*fct)/p) - (Zb/A)");
                sw.WriteLine("  <= (({0:E2} * {1})/{2:E2}) - ({0:E2}/{4}*10E5)", Zb, fct, p, _Zb, A);
                sw.WriteLine("  <= {0} mm", e_less_value);
                sw.WriteLine();

                double e_greater_value = (Zb * ftw / (eta * p * 1000)) - (Zb / (A * 10E3));
                e_greater_value = double.Parse(e_greater_value.ToString("0"));
                sw.WriteLine("and e >= (Zb*ftw/(η*p)) - (Zb/A)");
                sw.WriteLine("      >= ({0:E2} * {1}/({2}*{3:E2})) - ({0:E2}/{4}*10E5)", Zb, ftw, eta, p, A);
                sw.WriteLine("      >= {0} mm", e_greater_value);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the {0} Cables are arranged to follow a parabolic Profile with", Nc);

                double Ecg = 180;
                sw.WriteLine("the resultant force having an eccentricity of Ecg = {0} mm towards", Ecg);
                sw.WriteLine("the soffit at the support section. The position of Cables at Support");
                sw.WriteLine("Section is shown in the drawing.");
                sw.WriteLine();

                #endregion

                #region STEP 5 : CHECK FOR STRESSES
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : CHECK FOR STRESSES ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For section at centre of span we have, ");
                sw.WriteLine();
                sw.WriteLine("p = {0:E2} kN", p);
                sw.WriteLine();
                sw.WriteLine("e = {0} mm", e);
                sw.WriteLine();

                A = (A / 100);
                sw.WriteLine("A = {0}*10E5 sq.mm", A);
                sw.WriteLine();
                sw.WriteLine("Zt = {0:E2}", Zt);
                sw.WriteLine();
                sw.WriteLine("Zb = {0:E2}", Zb);
                sw.WriteLine();
                sw.WriteLine("η = {0}", eta);
                sw.WriteLine();
                sw.WriteLine("Mg = {0} kN-m", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m", Mq);
                sw.WriteLine();


                double p_by_A = (p * 1000) / (A * 10E5);
                p_by_A = double.Parse(p_by_A.ToString("0.00"));
                sw.WriteLine("p/A = {0:f0} * 1000 / ({1} * 10E5) = {2:f2} N/sq.mm", p, A, p_by_A);
                sw.WriteLine();

                double val1, val2;

                val1 = p * 1000 * e / (Zt);
                val1 = double.Parse(val1.ToString("0.00"));
                sw.WriteLine("p*e/Zt = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zt);
                sw.WriteLine("       = {0:f2} N/sq.mm", val1);
                sw.WriteLine();

                val2 = p * 1000 * e / (Zb);
                val2 = double.Parse(val2.ToString("0.00"));
                sw.WriteLine("p*e/Zb = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zb);
                sw.WriteLine("       = {0:f2} N/sq.mm", val2);
                sw.WriteLine();

                double Mg_by_Zt = Mg * 10E5 / (Zt);
                Mg_by_Zt = double.Parse(Mg_by_Zt.ToString("0.00"));
                sw.WriteLine("Mg/Zt = {0} * 10E5 / ({1:E2})", Mg, Zt);
                sw.WriteLine("      = {0:f2}  N/sq.mm", Mg_by_Zt);
                sw.WriteLine();

                double Mg_by_Zb = Mg * 10E5 / (Zb);
                Mg_by_Zb = double.Parse(Mg_by_Zb.ToString("0.00"));
                sw.WriteLine("Mg/Zb = {0} * 10E5 / ({1:E2})", Mg, Zb);
                sw.WriteLine("      = {0:f2} N/sq.mm ", Mg_by_Zb);
                sw.WriteLine();

                double Mq_by_Zt = Mq * 10E5 / (Zt);
                Mq_by_Zt = double.Parse(Mq_by_Zt.ToString("0.00"));
                sw.WriteLine("Mq/Zt = {0} * 10E5 / ({1:E2})", Mg, Zt);
                sw.WriteLine("      = {0:f2} N/sq.mm ", Mq_by_Zt);
                sw.WriteLine();

                double Mq_by_Zb = Mq * 10E5 / (Zb);
                Mq_by_Zb = double.Parse(Mq_by_Zb.ToString("0.00"));
                sw.WriteLine("Mq/Zb = {0} * 10E5 / ({1:E2})", Mg, Zb);
                sw.WriteLine("      = {0:f2} N/sq.mm ", Mq_by_Zb);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("At transfer stage :");
                sw.WriteLine();

                //double sigma_t = (p / A) - (p * e / Zt) + (Mg / Zt);
                double sigma_t = p_by_A - val1 + Mg_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = (p / A) - (p * e / Zt) + (Mg / Zt)");
                sw.WriteLine("    = {0} - {1} + {2}", p_by_A, val1, Mg_by_Zt);
                sw.WriteLine("    = {0} N/sq.mm", sigma_t);
                sw.WriteLine();

                double sigma_b = p_by_A + val2 - Mg_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.00"));
                sw.WriteLine("σ_b = (p / A) + (p * e / Zb) - (Mg / Zb)");
                sw.WriteLine("    = {0} + {1} - {2}", p_by_A, val2, Mg_by_Zb);
                sw.WriteLine("    = {0} N/sq.mm", sigma_b);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("At working load Stage :");
                sw.WriteLine();

                sigma_t = eta * p_by_A - eta * val1 + Mg_by_Zt + Mq_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = η*(p / A) - η*(p * e / Zt) + (Mg / Zt) + (Mq / Zt)");
                sw.WriteLine("    = {0}*{1} - {0}*{2} + {3} + {4}", eta, p_by_A, val1, Mg_by_Zt, Mq_by_Zt);
                sw.WriteLine("    = {0} N/sq.mm  (+ve, so, Compression)", sigma_t);
                sw.WriteLine();


                sigma_b = eta * p_by_A + eta * val2 - Mg_by_Zb - Mq_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.000"));
                sw.WriteLine("σ_t = η*(p / A) + η*(p * e / Zb) - (Mg / Zb) - (Mq / Zb)");
                sw.WriteLine("    = {0}*{1} + {0}*{2} - {3} - {4}", eta, p_by_A, val2, Mg_by_Zb, Mq_by_Zb);
                sw.WriteLine("    = {0} N/sq.mm  (-ve, so, Tension)", sigma_b);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the stresses at top and bottom fibres at Transfer and Service");
                sw.WriteLine("Loads are well within the Safe Permissible Limits.");
                sw.WriteLine();
                #endregion

                #region STEP 6 : CHECK FOR ULTIMATE FLEXURAL STRENGTH
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : CHECK FOR ULTIMATE FLEXURAL STRENGTH ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For centre of span section");
                sw.WriteLine();

                double Ap = (Math.PI * dos * dos / 4.0) * Facts * NS * Nc;
                //Ap = double.Parse(Ap.ToString("0"));
                sw.WriteLine("Ap = (π * dos * dos / 4.0) * Facts * NS * Nc");
                sw.WriteLine("   = (π * {0} * {0} / 4.0) * {1} * {2} * {3}", dos, Facts, NS, Nc);
                sw.WriteLine("   = {0:E3} sq.mm", Ap);
                sw.WriteLine();
                sw.WriteLine("b = {0} mm, cover = {1} mm", b, cover);
                double dc = d - cover;
                sw.WriteLine();
                sw.WriteLine("Effective Depth = dc = d - cover = {0} - {1} = {2} mm", d, cover, dc);
                sw.WriteLine();
                sw.WriteLine("bw = {0}, fck = {1} N/sq.mm, fp = {2} N/sq.mm", bw, fck, fp);
                sw.WriteLine();

                double Df = d1;
                sw.WriteLine("Df = d1 = {0} mm", d1);
                sw.WriteLine();
                sw.WriteLine();


                double Mu = 1.5 * Mg + 2.5 * Mq;
                Mu = double.Parse(Mu.ToString("0.00"));
                sw.WriteLine("Mu = 1.5 * Mg + 2.5 * Mq");
                sw.WriteLine("   = 1.5 * {0} + 2.5 * {1}", Mg, Mq);
                sw.WriteLine("   = {0} kN-m", Mu);
                sw.WriteLine();
                sw.WriteLine("Ultimate Flexural Strength is computed as follows :");
                sw.WriteLine();
                sw.WriteLine("(i)   Failure by Yielding of steel :");

                double Mu1 = 0.9 * dc * Ap * fp;
                Mu1 = double.Parse(Mu1.ToString("0"));
                sw.WriteLine("      Mu1 = 0.9 * dc * Ap * fp");
                sw.WriteLine("          = 0.9 * {0} * {1} * {2}", dc, Ap, fp);
                sw.WriteLine();
                sw.WriteLine("          = {0:E3} N-mm", Mu1);
                Mu1 = (int)(Mu1 / 1.0E+6);
                sw.WriteLine("          = {0:E3} kN-m", Mu1);
                sw.WriteLine();

                if (Mu < Mu1)
                {
                    sw.WriteLine("Mu < Mu1, Hence, OK");
                }
                else
                {
                    sw.WriteLine("Mu > Mu1, Hence, NOT OK");
                }

                sw.WriteLine();
                sw.WriteLine("(ii)   Failure by crushing of Concrete :");

                double Mu2 = (0.176 * bw * dc * dc * fck) + ((2.0 / 3.0) * Facts * (b - bw) * (dc - (Df / 2.0)) * Df * fck);

                //Mu2 = (Mu2 / 1.0E+6);
                //Mu2 = double.Parse(Mu2.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Mu2 = (0.176 * bw * dc * dc * fck)");
                sw.WriteLine("       + ((2.0 / 3.0) * Facts * (b - bw) * (dc - (Df / 2.0)) * Df * fck)");
                sw.WriteLine();
                sw.WriteLine("    = (0.176 * {0} * {1} * {1} * {2})", bw, dc, fck);
                sw.WriteLine("       + ((2.0 / 3.0) * {0} * ({1} - {2}) * ({3} - ({4} / 2.0)) * {4} * {5})", Facts, b, bw, dc, Df, fck);
                //Mu2 = (Mu2 / 10E5);
                sw.WriteLine();
                sw.WriteLine("    = {0:E3} N-mm", Mu2);
                Mu2 = (Mu2 / 1.0E+6);
                sw.WriteLine();
                sw.WriteLine("    = {0:E3} kN-m", Mu2);
                sw.WriteLine();
                //if (Mu < Mu2)
                //{
                //    sw.WriteLine("Mu < Mu2, Hence, OK");
                //}
                //else
                //{
                //    sw.WriteLine("Mu > Mu2, Hence, OK");
                //}

                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("(iii)  Flexural Strength at centre span section:");
                sw.WriteLine();
                sw.WriteLine("Ap = Apw + Apt");
                sw.WriteLine();

                double Apt = 0.45 * fck * (b - bw) * (Df / fp);
                Apt = double.Parse(Apt.ToString("0"));
                sw.WriteLine("where, Apt = 0.45 * fck * (b-bw) * (Df/fp)");
                sw.WriteLine("           = 0.45 * {0} * ({1}-{2}) * ({3}/{4})", fck, b, bw, Df, fp);
                sw.WriteLine("           = {0} sq.mm", Apt);
                sw.WriteLine();

                double Apw = Ap - Apt;
                sw.WriteLine("So, Apw = Ap - Apt = {0:E3} - {1:E3} = {2:E3} sq.mm", Ap, Apt, Apw);
                sw.WriteLine();

                double ratio = (Apw * fp / (bw * dc * fck));
                ratio = double.Parse(ratio.ToString("0.000"));
                sw.WriteLine("Ratio = (Apw * fp / (bw * dc * fck))");
                sw.WriteLine("      = ({0:E3} * {1} / ({2} * {3} * {4}))", Apw, fp, bw, dc, fck);
                sw.WriteLine("      = {0} ", ratio);
                sw.WriteLine();
                sw.WriteLine();

                double post_tension = Get_Table_2_Value(ratio, 2, ref ref_string);

                double Xu_by_dc = Get_Table_2_Value(2, 4, ref ref_string);

                double fpu = post_tension * 0.87 * fp;

                sw.WriteLine("From Table 2, for post Tensioned Beams with effective bond : {0}", ref_string);
                sw.WriteLine();
                fpu = double.Parse(fpu.ToString("0.000"));

                double Xu = Xu_by_dc * dc;
                Xu = double.Parse(Xu.ToString("0.000"));
                sw.WriteLine("fpu / (0.87*fp) = {0:f2}    and      Xu/dc = {1:f2}", post_tension, Xu_by_dc);
                sw.WriteLine();
                sw.WriteLine("fpu = {0:f2} * 0.87 * fp    and      Xu = {1:f2} * dc", post_tension, Xu_by_dc);
                sw.WriteLine();
                sw.WriteLine("fpu = {0}  and  Xu = {1} mm", fpu, Xu);
                sw.WriteLine();

                double Mu3 = fpu * Apw * (dc - 0.42 * Xu) + 0.45 * fck * (b - bw) * Df * (dc - 0.5 * Df);

                sw.WriteLine("Mu3 = fpu * Apw * (dc - 0.42 * Xu) ");
                sw.WriteLine("      + 0.45 * fck * (b - bw) * Df * (dc - 0.5 * Df)");
                sw.WriteLine();
                sw.WriteLine("    = {0} * {1} * ({2} - 0.42 * {3}) ", fpu, Apw, dc, Xu);
                sw.WriteLine("      + 0.45 * {0} * ({1} - {2}) * {3} * ({4} - 0.5 * {3})", fck, b, bw, Df, dc);
                sw.WriteLine();

                sw.WriteLine("    = {0:E3} N-mm ", Mu3);
                Mu3 = (int)(Mu3 / 1.0E+6);
                sw.WriteLine("    = {0:E3} kN-m ", Mu3);

                if (Mu < Mu3)
                {
                    sw.WriteLine("Mu < Mu3, Hence, OK");
                }
                else
                {
                    sw.WriteLine("Mu > Mu3, Hence, NOT OK");
                }
                sw.WriteLine();
                #endregion

                #region STEP 7 : Check for Ultimate Shear Strength
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : CHECK FOR ULTIMATE SHEAR STRENGTH ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double Vg = DL_SF_IG;
                double Vq = LL_SF_IG;

                double Vu = 1.5 * Vg + 2.5 * Vq;
                sw.WriteLine("Ultimate Shear Force = Vu = 1.5 * Vg + 2.5 * Vq");
                sw.WriteLine("                     = 1.5 * {0} + 2.5 * {1}", Vg, Vq);
                sw.WriteLine("                     = {0:E3} kN", Vu);
                sw.WriteLine();


                sw.WriteLine("Ultimate Shear resistance of Support Section uncracked in Flexure");
                sw.WriteLine("is obtained by, Vcw = 0.67*bw*d*√(ft*ft+0.8*Fcp*ft) + η.p.Sinθ");

                double ft = 0.24 * Math.Sqrt(fck);
                ft = double.Parse(ft.ToString("0.00"));
                double fcp = (eta * p * 1000) / (A * 10E5);
                fcp = double.Parse(fcp.ToString("0.00"));

                // 600 + x + 300 + x + x = 2*(1800-300-250-2*300-x)
                // 3x + 600+300 = 2*(650 -x)
                // 3x + 2x = 1300 - 600 - 300
                // 5x = 400
                // x = 80

                // x' = 250 + 300 + 300 + 80  (x = 80)
                // x' = 850 + 80 = 930
                // x' = 930 - 750 = 180
                //

                double _e = e;

                double x_dash = 180;

                e = _e - x_dash;
                double theta = 4 * e / (L * 1000);
                theta = double.Parse(theta.ToString("0.000"));
                double Vcw = 0.67 * bw * d * Math.Sqrt((ft * ft + 0.8 * fcp * ft)) + eta * p * 1000 * theta;
                //Vcw = double.Parse(Vcw.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("ft = 0.24 * √(fck) = 0.24 * √({0}) = {1} N/sq.mm", fck, ft);
                sw.WriteLine();
                sw.WriteLine("fcp = (η * p) / A ");
                sw.WriteLine("    = ({0} * {1:f0} * 1000) / ({2} * 10E5)", eta, p, A);
                sw.WriteLine("    = {0} sq.mm", fcp);
                sw.WriteLine();


                sw.WriteLine("Eccentricity of Cables at Centre of span = {0} mm", _e);
                sw.WriteLine();
                sw.WriteLine("Eccentricity of Cables at Support = {0} mm", x_dash);
                sw.WriteLine();
                sw.WriteLine("Net eccentricity = e = {0} - {1} = {2} mm", _e, x_dash, e);
                sw.WriteLine();
                sw.WriteLine("Slope of Cable = θ = 4 * e / (L * 1000)");
                sw.WriteLine("               = 4 * {0} / ({1} * 1000)", e, L);
                sw.WriteLine("               = {0} ", theta);
                sw.WriteLine();
                //double Vcw = 0.67 * bw * d * Math.Sqrt((ft * ft + 0.8 * Fcp * ft)) + n * p * 1000 * theta;
                sw.WriteLine("Vcw = 0.67 * {0} * {1} * √(({2} * {2} + 0.8 * {3} * {{2}}))", bw, d, ft, fcp);
                sw.WriteLine("      + {0} * {1:f0} * 1000 * {2}", eta, p, theta);
                sw.WriteLine();
                sw.WriteLine("    = {0:E3} N", Vcw);
                sw.WriteLine();
                Vcw = (int)(Vcw / 1000.0);
                sw.WriteLine("    = {0:E3} kN", Vcw);
                sw.WriteLine();
                sw.WriteLine("Required Shear resistance = {0:E3} kN", Vu);
                sw.WriteLine();
                sw.WriteLine("Available Shear capacity of Section = {0:E3} kN", Vcw);
                sw.WriteLine();

                double V = Vu - Vcw;
                sw.WriteLine("Balance Shear = V = {0:E3} kN", V);
                sw.WriteLine();
                sw.WriteLine("Using 10 mm diameter 2-legged Stirrups HYSD bars ");
                sw.WriteLine("the spacing Sv is calculated as :");
                sw.WriteLine();


                // Asv = 79 = ?

                double Asv = Math.PI * 10.0 * 10.0 / 4.0;
                Asv = double.Parse(Asv.ToString("0"));

                double Sv = (0.87 * fy * 2 * Asv * (d - 50)) / (V * 1000);
                Sv = double.Parse(Sv.ToString("0"));

                sw.WriteLine("Sv = (0.87 * fy * 2 * Asv * (d - 50)) / (V * 1000)");
                sw.WriteLine("   = (0.87 * {0} * 2 * {1} * ({2} - 50)) / ({3:E3} * 1000)", fy, Asv, d, V);
                sw.WriteLine();
                sw.WriteLine("   = {0} mm", Sv);
                sw.WriteLine();

                double spacing = 0;

                if (Sv > 150)
                    spacing = 150;
                sw.WriteLine("Provide 10 mm diameter  stirrups at {0} mm Centre to Centre", spacing);
                sw.WriteLine("spacing near support and gradually increased to 300 mm towards");
                sw.WriteLine("the centre of span.                 Marked as (J) in the Drawing");
                //(J) = Provide 10 mm diameter  stirrups at 150 mm c/c.
                _J = string.Format("Provide 10 mm diameter  stirrups at {0} mm c/c", spacing);




                sw.WriteLine();
                #endregion

                #region STEP 8 : SUPPLEMENTARY REINFORCEMENTS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : SUPPLEMENTARY REINFORCEMENTS ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Longitudinal reinforcement of not less than 0.15 per cent");
                sw.WriteLine("of gross cross sectional area is to be provided to Safeguard");
                sw.WriteLine("against shrinkage cracking,");
                sw.WriteLine();

                double Ast = (0.15 * A * 10E5) / 100.0;
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Ast = (0.15 * {0} * 10E5) / 100.0", A);
                sw.WriteLine("    = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine("Provide 20 mm diameter bars with distribution in the compression");
                sw.WriteLine("Flange as Shown in the drawing.          Marked as (K) in the Drawing");
                //(K) = Provide 20 mm diameter bars
                _K = string.Format("Provide 20 mm diameter bars");


                sw.WriteLine();
                #endregion

                #region STEP 9 : DESIGN OF THE END BLOCK
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 9 : DESIGN OF THE END BLOCK ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Provide Solid end Sections over a length of 1.5 m at either end");
                sw.WriteLine("Typical equivalent prisms on which the anchorage forces are apply");
                sw.WriteLine("are described in Drawing. The Bursting Tension force is calculated");
                sw.WriteLine("using the data given in Table 3 at the end of this Report.");
                sw.WriteLine();
                sw.WriteLine("We have the following values in the Horizontal Plane");
                sw.WriteLine();

                //double Pk = 1459;
                sw.WriteLine("Pk = {0} kN", Pk);
                sw.WriteLine();

                double Ypo = 225.0 / 2.0;
                sw.WriteLine("2Ypo = 225 mm");
                sw.WriteLine();

                double Yo = 900.0 / 2.0;
                sw.WriteLine("2Yo = 900 mm");
                sw.WriteLine();

                val1 = Ypo / Yo;
                ratio = val1;
                sw.WriteLine("Ypo / Yo = {0} / {1} = {2}", Ypo, Yo, val1);
                sw.WriteLine();

                val1 = Get_Table_3_Value(ratio, ref ref_string);
                sw.WriteLine("{0}", ref_string);
                sw.WriteLine();
                double Fbst = val1 * Pk;
                sw.WriteLine("Bursting Tension Force = Fbst = {0} * Pk", val1);
                sw.WriteLine("                       = {0} * {1}", val1, Pk);
                sw.WriteLine("                       = {0} kN", Fbst);
                sw.WriteLine();
                sw.WriteLine("Area of steel required to resist the tension is obtained by:");
                sw.WriteLine();

                Ast = Fbst * 1000 / (0.87 * fy);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Ast = Fbst * 1000 / (0.87 * fy)");
                sw.WriteLine("    = {0} * 1000 / (0.87 * {1})", Fbst, fy);
                sw.WriteLine("    = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();


                double pro_ast = 0.0;
                spacing = 60;

                do
                {
                    spacing += 20;
                    pro_ast = (Math.PI * 10 * 10 / 4.0) * (1000.0 / spacing);
                }
                while (pro_ast > Ast);


                sw.WriteLine("Provide 10 mm diameter bars at {0} mm c/c spacing in Horizontal         Marked as (L) in the Drawing", spacing);
                sw.WriteLine("and Vertical direction in form of a mesh as shown in the drawing.");
                //(L) = Provide 10 mm diameter bars at 100 mm c/c.
                _L = string.Format("Provide 10 mm diameter bars at {0} mm c/c.", spacing);


                sw.WriteLine();
                sw.WriteLine();
                #endregion

                Write_Table_2(sw);
                sw.WriteLine();
                Write_Table_3(sw);
                sw.WriteLine();

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Write_Long_Girder_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_G={0}", _G);
                sw.WriteLine("_H={0}", _H);
                sw.WriteLine("_I={0}", _I1);
                sw.WriteLine("_J={0}", _J);
                sw.WriteLine("_K={0}", _K);
                sw.WriteLine("_L={0}", _L);
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

        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF PRESTRESSED POST TENSIONED RCC GIRDER BRIDGE : " + value;
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of Pre Stressed Post Tensioned Long Girder");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_PS_Girder.TXT");
                user_input_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER.FIL");
                user_drawing_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER_DRAWING.FIL");

                if (File.Exists(user_input_file) && !is_process)
                {
                    //string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    //msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    //if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    //    ;//Longg Read_Long_Girder_User_Input();
                }
            }
        }

        #endregion


        public double Get_Table_2_Value(double ratio, int indx, ref string ref_string)
        {
            return iApp.Tables.Pre_Post_Tensioning_with_EffectiveBond(ratio, indx, ref ref_string);


            //ratio = Double.Parse(ratio.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");


            //ratio = 0.218;
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
            //    mList = new MyList(MyList.RemoveAllSpaces(lst_content[i].Trim()), ' ');
            //    find = (double.TryParse(mList.StringList[0], out a2) && mList.Count == 5);
            //    if (find)
            //    {
            //        //mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);
            //    if (ratio < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (ratio > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == ratio)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > ratio)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (ratio - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_3_Value(double ratio, ref string ref_string)
        {
            return iApp.Tables.Bursting_Tensile_Force(ratio, ref  ref_string);

            //ratio = Double.Parse(ratio.ToString("0.000"));
            //int indx = 1;
            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_3.txt");


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
            //    mList = new MyList(MyList.RemoveAllSpaces(lst_content[i].Trim()), ' ');
            //    find = (double.TryParse(mList.StringList[0], out a2) && mList.Count == 2);
            //    if (find)
            //    {
            //        //mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);
            //    if (ratio < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (ratio > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == ratio)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > ratio)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (ratio - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }

        public void Write_Table_2(StreamWriter sw)
        {
            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Pre_Post_Tensioning_with_EffectiveBond();
            string kStr = "";
            sw.WriteLine();
            sw.WriteLine("TABLE 1 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
        }
        public void Write_Table_3(StreamWriter sw)
        {
            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "PreStressedBridge_Table_3.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Bursting_Tensile_Force();
            string kStr = "";
            sw.WriteLine();
            sw.WriteLine("TABLE 2 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
        }


    }

    public class PSC_I_Girder_Short_Analysis_WS
    {

        IApplication iApp;


        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Structure = null;
        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;

        public PreStressedConcrete_SectionProperties PSC_Mid_Span { get; set; }
        public PreStressedConcrete_SectionProperties PSC_End { get; set; }
        public PreStressedConcrete_SectionProperties PSC_Cross { get; set; }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }
        public CompositeSection Steel_Section { get; set; }

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;


        string input_file, working_folder, user_path;
        public PSC_I_Girder_Short_Analysis_WS(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = working_folder = "";
            //Total_Rows = 0; Total_Columns = 0;
            NMG = 7;
            Total_Columns = 11;
            Total_Rows = 11;
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
                    string pd = Path.Combine(working_folder, "Total Analysis");
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
                    string pd = Path.Combine(working_folder, "Live Load Analysis");
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

        public List<int> _L2_out_joints = new List<int>();
        public List<int> _L4_out_joints = new List<int>();
        public List<int> _deff_out_joints = new List<int>();

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

        void Set_Girders()
        {


            List<int> L2_Girders = new List<int>();
            List<int> L4_Girders = new List<int>();
            List<int> Deff_Girders = new List<int>();
            List<int> Cross_Girder = new List<int>();


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


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            L2_Girders_as_String = MyList.Get_Array_Text(L2_Girders);
            L4_Girders_as_String = MyList.Get_Array_Text(L4_Girders);
            Deff_Girders_as_String = MyList.Get_Array_Text(Deff_Girders);
            //Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);
            Set_Inner_Outer_Cross_Girders();
        }

        void Set_L2_L4_Deff_Girders()
        {
            double L = Length;
            double W = WidthBridge;
            double val = L / 2;
            int i = 0;

            _L2_inn_joints.Clear(); ;
            _L4_inn_joints.Clear(); ;
            _deff_inn_joints.Clear(); ;

            _L2_out_joints.Clear(); ;
            _L4_out_joints.Clear(); ;
            _deff_out_joints.Clear(); ;





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

            val = Effective_Depth;

            double cant_wi_left = Width_LeftCantilever;
            double cant_wi_right = Width_RightCantilever;

            for (i = 0; i < Joints.Count; i++)
            {
                try
                {
                    //if ((Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right)) == false) continue;


                    x_min = _X_min[_Z_joints.IndexOf(Joints[i].Z)];

                    if ((Joints[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right))
                        _L2_inn_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _L4_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _L4_out_joints.Add(Joints[i].NodeNo);
                    }


                    if ((Joints[i].X.ToString("0.0") == (Effective_Depth + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _deff_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if ((Joints[i].X.ToString("0.0") == (L - Effective_Depth + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _deff_out_joints.Add(Joints[i].NodeNo);
                    }
                }
                catch (Exception ex) { }
            }


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

            List<int> temp_joints = new List<int>();


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
            Set_Girders();
        }

        #endregion Chiranjit [2013 05 06]


        public void CreateData()
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
                    if (iCols == 0 && iRows >= 1 && iRows <= _Rows - 2)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1 && iRows >= 1 && iRows <= _Rows - 2)
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
        }

        public void WriteData(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

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
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);

        }
        public void WriteData_Total_Analysis(string file_name)
        {
            WriteData_Total_Analysis(file_name, false);
        }
        public void WriteData_Total_Analysis(string file_name, bool is_PSC_Long_Girder)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

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

            list.Add("SECTION PROPERTIES");
            if (is_PSC_Long_Girder)
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
            else
            {
                Write_PSC_Short_Section_Properties(list);
            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            if (is_PSC_Long_Girder)
            {
                Total_Rows = _Rows;

                string k = "";
                for (int c = 2; c < Joints[_Rows].NodeNo - 1; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, Start_Support));
                k = "";
                for (int c = Joints[Joints.Count - _Rows + 1].NodeNo; c <= Joints[Joints.Count - 1 - 1].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, End_Support));
            }
            else
            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
                list.Add(string.Format("{0} {1}", support_right_joints, End_Support));
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
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
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
        public void WriteData_LiveLoad_Analysis(string file_name, bool is_psc_I_Girder)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

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
            else 
            {
                Write_PSC_Short_Section_Properties(list);

            }
            //else
            //{
            //    list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
            //    list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
            //    list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
            //    list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
            //    list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
            //    list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
            //    list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            //    list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            //    list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
            //    list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
            //    list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
            //    list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
            //    list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            //}
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            if (is_psc_I_Girder)
            {
                Total_Rows = _Rows;

                string k = "";
                for (int c = 2; c < Joints[_Rows].NodeNo - 1; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, Start_Support));
                k = "";
                for (int c = Joints[Joints.Count - _Rows + 1].NodeNo; c <= Joints[Joints.Count - 1 - 1].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, End_Support));
            }
            else
            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
                list.Add(string.Format("{0} {1}", support_right_joints, End_Support));
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
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
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

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
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
            else 
            {
                Write_PSC_Short_Section_Properties(list);
            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            if (is_PSC_I_Gider)
            {
                Total_Rows = _Rows;

                string k = "";
                for (int c = 1; c < Joints[_Rows].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, Start_Support));
                k = "";
                for (int c = Joints[Joints.Count - _Rows].NodeNo; c <= Joints[Joints.Count - 1].NodeNo; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, End_Support));

            }
            else
            {
                //list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
                //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");

                //Chiranjit [2013 05 06]
                //list.Add(string.Format("{0} PINNED", support_left_joints));
                //list.Add(string.Format("{0} PINNED", support_right_joints));



                list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
                list.Add(string.Format("{0} {1}", support_right_joints, End_Support));



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
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }
        private void Write_Composite_Section_Properties(List<string> list)
        {
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                Cross_Girders_as_String,
                Steel_Section.Section_Cross_Girder.Area_in_Sq_m,
                Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Cross_Girder.Izz_in_Sq_Sq_m));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                L2_Girders_as_String,
                Steel_Section.Section_Long_Girder_at_Mid_Span.Area_in_Sq_m,
                Steel_Section.Section_Long_Girder_at_Mid_Span.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Long_Girder_at_Mid_Span.Izz_in_Sq_Sq_m));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                            L4_Girders_as_String,
                Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
                Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m));


            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                           Deff_Girders_as_String,
                Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
                Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m));

        }
        private void Write_PSC_Short_Section_Properties(List<string> list)
        {
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                Cross_Girders_as_String,
                PSC_Cross.AX_Sq_M,
                PSC_Cross.IX,
                PSC_Cross.IZ));

            string kStr = L2_Girders_as_String + " " + L4_Girders_as_String;

            kStr = MyList.Get_Array_Text( MyList.Get_Array_Intiger(kStr));
            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                kStr,
                PSC_Mid_Span.AX_Sq_M,
                PSC_Mid_Span.IX,
                PSC_Mid_Span.IZ));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                           Deff_Girders_as_String,
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
    }

}
