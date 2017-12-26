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
    public partial class frm_PSC_I_Girder_LongSpan_WS : Form
    {
        
        //const string Title = "ANALYSIS OF PSC I-GIRDER BRIDGE (WORKING STRESS)";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "PSC I-GIRDER BRIDGE WORKING STRESS [BS]";
                return "PSC I-GIRDER BRIDGE WORKING STRESS [IRC]";
            }
        }

        PSC_I_Girder_Long_Analysis Deck_Analysis_DL = null;
        PSC_I_Girder_Long_Analysis Deck_Analysis_LL = null;

        PSC_I_Girder_Long_Analysis Bridge_Analysis = null;


        PreStressedConcrete_Forces PSC_Shear_Forces;
        PreStressedConcrete_Forces PSC_Moment_Forces;

        //Chiranjit [2012 06 22]
        CantileverSlab Cant = null;

        //Chiranjit [2012 06 13]
        RccPier rcc_pier = null;

        //Chiranjit [2012 06 13]
        RCC_AbutmentWall Abut = null;

        PostTensionLongGirder LongGirder = null;
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
        public frm_PSC_I_Girder_LongSpan_WS(IApplication app)
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
                if (Path.GetFileName(user_path) == Project_Name)  Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
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
                //if (IsCreate_Data)
                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                //if (!Directory.Exists(user_path))
                //    Directory.CreateDirectory(user_path);
 
                PSC_Section_Properties(true);

                Bridge_Analysis.PSC_Mid_Span = sec_1;
                Bridge_Analysis.PSC_End = sec_2;
                Bridge_Analysis.PSC_Cross = sec_3;

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;
                Bridge_Analysis.CreateData(true);


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File, true);
                txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File, true);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, true);
                Deck_Analysis_LL.Input_File = Bridge_Analysis.LiveLoadAnalysis_Input_File;

                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File, true);
                Deck_Analysis_DL.Input_File = Bridge_Analysis.LiveLoadAnalysis_Input_File;

                Ana_Write_Load_Data(Bridge_Analysis.Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true);
                Ana_Write_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, false);
                Ana_Write_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true);

                Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                PSC_Section_Properties();


                //MessageBox.Show(this, MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
                //"ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }

                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);

                //Calculate_Load_Computation();

                //Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                //    Bridge_Analysis.Inner_Girders_as_String,
                //    Bridge_Analysis.joints_list_for_load);
                
                PSC_Section_Properties(true);

                Bridge_Analysis.PSC_Mid_Span = sec_1;
                Bridge_Analysis.PSC_End = sec_2;
                Bridge_Analysis.PSC_Cross = sec_3;

                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;


                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;
                Bridge_Analysis.CreateData(true);


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);
                


                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File, true);
                txt_Ana_analysis_file.Text = Bridge_Analysis.Input_File;



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File, true);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, true);
                Deck_Analysis_LL.Input_File = Bridge_Analysis.LiveLoadAnalysis_Input_File;

                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File, true);
                Deck_Analysis_DL.Input_File = Bridge_Analysis.LiveLoadAnalysis_Input_File;

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
                iApp.View_Input_File(Deck_Analysis_LL.Input_File);
        }

        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name == btn_view_structure.Name)
            {
                if (File.Exists(Deck_Analysis_DL.Input_File))
                    iApp.OpenWork(Deck_Analysis_DL.Input_File, false);
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
                {
                    flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                }
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

                //iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                //iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Total Load Analysis Result");

                //iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File");
                //iApp.Progress_Works.Add("Set Structure Geometry for Dead Load Analysis");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Dead Load Analysis Result");


                //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File");
                //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Live Load Analysis Result");


                //iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                //iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                //iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


                //iApp.Progress_Works = new ProgressList(Work_List);

                Bridge_Analysis.Structure = null;

                try
                {
                    Bridge_Analysis.Structure = new BridgeMemberAnalysis(iApp, ana_rep_file);
                    Deck_Analysis_LL.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.LiveLoad_Analysis_Report);
                    Deck_Analysis_DL.Structure = new BridgeMemberAnalysis(iApp, Bridge_Analysis.DeadLoad_Analysis_Report);
                }
                catch(Exception exx)
                {
                    MessageBox.Show(exx.Message);
                }
                if (iApp.Is_Progress_Cancel) return;
              

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


                txt_node_displacement.Text = Bridge_Analysis.Structure.Node_Displacements.Get_Max_Deflection().ToString();


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
            Deck_Analysis_DL.NCG = NCG;

            Deck_Analysis_LL.Length = L;
            Deck_Analysis_LL.WidthBridge = B;
            Deck_Analysis_LL.Width_LeftCantilever = CL;
            Deck_Analysis_LL.Width_RightCantilever = CR;
            Deck_Analysis_LL.Skew_Angle = Ang;
            Deck_Analysis_LL.Effective_Depth = Deff;
            Deck_Analysis_LL.NMG = NMG;
            Deck_Analysis_LL.NCG = NCG;

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
            Show_Moment_Shear_LL(true); return;
            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Structure.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis_LL.Structure.Analysis.Joints;

            double L = Deck_Analysis_LL.Structure.Analysis.Length;
            double W = Deck_Analysis_LL.Structure.Analysis.Width;
            double val = L / 2;
            int i = 0;

            List<int> _L2_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();

            List<int> _L4_out_joints = new List<int>();
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
            val = Deck_Analysis_LL.Structure.Analysis.Effective_Depth;
            if (_X_joints.Contains(val))
            {
                Deck_Analysis_LL.Effective_Depth = val;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }
            //double eff_dep = ;

            //_L_2_joints.Clear();

            double cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.Width_LeftCantilever = cant_wi;




            //if (_X_joints.Contains(val))
            //{
            //    Deck_Analysis_LL.Effective_Depth = val;
            //}
            //else
            //{
            //    Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            //}
            ////double eff_dep = ;

            ////_L_2_joints.Clear();

            //double cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            //Deck_Analysis_LL.WidthCantilever = cant_wi;
            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }


                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }


                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
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


            MaxForce mfrc = new MaxForce();


            Results.Add("");
            Results.Add("Analysis Result of Live Loads of Pre Stressed Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("INNER GIRDER");
            Results.Add("------------");

            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_LL_IG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_LL_IG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_inn_joints);
            txt_LL_IG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_inn_joints, "Ton"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_inn_joints);
            txt_LL_IG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_inn_joints);
            txt_LL_IG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_inn_joints, "Ton"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_deff_inn_joints);
            txt_LL_IG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_inn_joints, "Ton-m"));





            _L2_inn_joints.Clear();
            _L4_inn_joints.Clear();
            _deff_inn_joints.Clear();

            _L4_out_joints.Clear();
            _deff_out_joints.Clear();

            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }


                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }


                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { }
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

            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_inn_joints);
            txt_LL_OG_S1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_inn_joints);
            txt_LL_OG_M1.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));








            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_out_joints);
            txt_LL_OG_S2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_out_joints);
            txt_LL_OG_M2.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));


            mfrc = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_deff_out_joints);
            txt_LL_OG_S3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));



            mfrc = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_deff_out_joints);
            txt_LL_OG_M3.Text = mfrc.ToString();
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));



            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);


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
        void Show_Moment_Shear_DL()
        {
            //Chiranjit [2012 07 26]
            try
            {
                Show_Moment_Shear_DL(true);
            }
            catch (Exception ex) { }
 return;

            MemberCollection mc = new MemberCollection(Deck_Analysis_DL.Structure.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis_DL.Structure.Analysis.Joints;

            double L = Deck_Analysis_DL.Structure.Analysis.Length;
            double W = Deck_Analysis_DL.Structure.Analysis.Width;
            double val = L / 2;
            int i = 0;

            List<int> _L2_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();

            List<int> _L4_out_joints = new List<int>();
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
                Deck_Analysis_LL.Effective_Depth = val;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
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

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }


                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }


                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
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



            MaxForce mfrc = new MaxForce();


            Results.Clear();
            Results.Add("");
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





            _L2_inn_joints.Clear();
            _L4_inn_joints.Clear();
            _deff_inn_joints.Clear();

            _L4_out_joints.Clear();
            _deff_out_joints.Clear();

            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }


                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }


                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_DL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_DL.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { }
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




            List<int> _L1_inn_joints =Bridge_Analysis._L1_inn_joints; //0
            List<int> _L2_inn_joints =Bridge_Analysis._L2_inn_joints; //Effective Depth
            List<int> _L3_inn_joints =Bridge_Analysis._L3_inn_joints; //  L / 5.3
            List<int> _L4_inn_joints =Bridge_Analysis._L4_inn_joints; //  L / 3.65
            List<int> _L5_inn_joints =Bridge_Analysis._L5_inn_joints; //  L / 3.05
            List<int> _L6_inn_joints =Bridge_Analysis._L6_inn_joints;//  L / 2.61
            List<int> _L7_inn_joints =Bridge_Analysis._L7_inn_joints;//  L / 2.28
            List<int> _L8_inn_joints =Bridge_Analysis._L8_inn_joints;//  L / 2.03
            List<int> _L9_inn_joints =Bridge_Analysis._L9_inn_joints;//  L / 2.

            List<int> _L1_out_joints =Bridge_Analysis._L1_out_joints;
            List<int> _L2_out_joints =Bridge_Analysis._L2_out_joints;
            List<int> _L3_out_joints =Bridge_Analysis._L3_out_joints;
            List<int> _L4_out_joints =Bridge_Analysis._L4_out_joints;
            List<int> _L5_out_joints =Bridge_Analysis._L5_out_joints;
            List<int> _L6_out_joints =Bridge_Analysis._L6_out_joints;
            List<int> _L7_out_joints =Bridge_Analysis._L7_out_joints;
            List<int> _L8_out_joints =Bridge_Analysis._L8_out_joints;
            List<int> _L9_out_joints =Bridge_Analysis._L9_out_joints;

            List<int> _deff_inn_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();


            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();
             
            MaxForce mfrc = new MaxForce();


            Results.Clear();
            PSC_Shear_Forces = new PreStressedConcrete_Forces();


            //Inner Girder ShearForce
            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L1_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F1 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L1 :  MAX SHEAR FORCE", _L1_inn_joints, " Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F2 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L2 :  MAX SHEAR FORCE", _L2_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L3_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F3 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L3 :  MAX SHEAR FORCE", _L3_inn_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F4 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L4 :  MAX SHEAR FORCE", _L4_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L5_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F5 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L5 :  MAX SHEAR FORCE", _L5_inn_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L6_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F6 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L6 :  MAX SHEAR FORCE", _L6_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L7_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F7 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L7 :  MAX SHEAR FORCE", _L7_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L8_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F8 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L8 :  MAX SHEAR FORCE", _L8_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L9_inn_joints, true);
            PSC_Shear_Forces.DL_INNER_GIRDER.F9 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L9 :  MAX SHEAR FORCE", _L9_inn_joints, " Ton"));




            //PSC_Shear_Forces.DL_INNER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L1_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L3_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L5_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L6_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L7_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L8_inn_joints, true).Force;
            //PSC_Shear_Forces.DL_INNER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L9_inn_joints, true).Force;


            //Outer Girder ShearForce





            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L1_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F1 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L1 :  MAX SHEAR FORCE", _L1_out_joints, " Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F2 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L2 :  MAX SHEAR FORCE", _L2_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L3_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F3 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L3 :  MAX SHEAR FORCE", _L3_out_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F4 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L4 :  MAX SHEAR FORCE", _L4_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L5_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F5 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L5 :  MAX SHEAR FORCE", _L5_out_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L6_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F6 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L6 :  MAX SHEAR FORCE", _L6_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L7_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F7 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L7 :  MAX SHEAR FORCE", _L7_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L8_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F8 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L8 :  MAX SHEAR FORCE", _L8_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L9_out_joints, true);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F9 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L9 :  MAX SHEAR FORCE", _L9_out_joints, " Ton"));










            //PSC_Shear_Forces.DL_OUTER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L1_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L3_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L5_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L6_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L7_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L8_out_joints, true).Force;
            //PSC_Shear_Forces.DL_OUTER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L9_out_joints, true).Force;


            txt_DL_IG_S1.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F1.ToString("f3");
            txt_DL_IG_S2.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F2.ToString("f3");
            txt_DL_IG_S3.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F3.ToString("f3");
            txt_DL_IG_S4.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F4.ToString("f3");
            txt_DL_IG_S5.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F5.ToString("f3");
            txt_DL_IG_S6.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F6.ToString("f3");
            txt_DL_IG_S7.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F7.ToString("f3");
            txt_DL_IG_S8.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F8.ToString("f3");
            txt_DL_IG_S9.Text = PSC_Shear_Forces.DL_INNER_GIRDER.F9.ToString("f3");

            txt_DL_OG_S1.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F1.ToString("f3");
            txt_DL_OG_S2.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F2.ToString("f3");
            txt_DL_OG_S3.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F3.ToString("f3");
            txt_DL_OG_S4.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F4.ToString("f3");
            txt_DL_OG_S5.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F5.ToString("f3");
            txt_DL_OG_S6.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F6.ToString("f3");
            txt_DL_OG_S7.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F7.ToString("f3");
            txt_DL_OG_S8.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F8.ToString("f3");
            txt_DL_OG_S9.Text = PSC_Shear_Forces.DL_OUTER_GIRDER.F9.ToString("f3");





            Results.AddRange(mfrc.GetDetails("L/4 :  MAX BENDING MOMENT", _L4_out_joints, " Ton-m"));

            //Inner Girder Moment Forces
            PSC_Moment_Forces = new PreStressedConcrete_Forces();





            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F1 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L1 :  MAX SHEAR FORCE", _L1_inn_joints, " Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F2 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L2 :  MAX SHEAR FORCE", _L2_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L3_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F3 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L3 :  MAX SHEAR FORCE", _L3_inn_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F4 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L4 :  MAX SHEAR FORCE", _L4_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L5_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F5 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L5 :  MAX SHEAR FORCE", _L5_inn_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L6_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F6 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L6 :  MAX SHEAR FORCE", _L6_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L7_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F7 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L7 :  MAX SHEAR FORCE", _L7_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L8_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F8 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L8 :  MAX SHEAR FORCE", _L8_inn_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L9_inn_joints, true);
            PSC_Moment_Forces.DL_INNER_GIRDER.F9 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L9 :  MAX SHEAR FORCE", _L9_inn_joints, " Ton"));






            //PSC_Moment_Forces.DL_INNER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L3_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L5_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L6_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L7_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L8_inn_joints).Force;
            //PSC_Moment_Forces.DL_INNER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L9_inn_joints).Force;


            //Outer Girder Moment Forces

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F1 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L1 :  MAX SHEAR FORCE", _L1_out_joints, " Ton"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F2 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L2 :  MAX SHEAR FORCE", _L2_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L3_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F3 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L3 :  MAX SHEAR FORCE", _L3_out_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F4 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L4 :  MAX SHEAR FORCE", _L4_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L5_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F5 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L5 :  MAX SHEAR FORCE", _L5_out_joints, " Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L6_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F6 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L6 :  MAX SHEAR FORCE", _L6_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L7_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F7 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L7 :  MAX SHEAR FORCE", _L7_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L8_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F8 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L8 :  MAX SHEAR FORCE", _L8_out_joints, " Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L9_out_joints, true);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F9 = mfrc.Force;
            Results.AddRange(mfrc.GetDetails("L9 :  MAX SHEAR FORCE", _L9_out_joints, " Ton"));




            //PSC_Moment_Forces.DL_OUTER_GIRDER.F1 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L1_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F2 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F3 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L3_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F4 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F5 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L5_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F6 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L6_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F7 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L7_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F8 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L8_out_joints).Force;
            //PSC_Moment_Forces.DL_OUTER_GIRDER.F9 = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L9_out_joints).Force;







            txt_DL_IG_M1.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F1.ToString("f3");
            txt_DL_IG_M2.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F2.ToString("f3");
            txt_DL_IG_M3.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F3.ToString("f3");
            txt_DL_IG_M4.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F4.ToString("f3");
            txt_DL_IG_M5.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F5.ToString("f3");
            txt_DL_IG_M6.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F6.ToString("f3");
            txt_DL_IG_M7.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F7.ToString("f3");
            txt_DL_IG_M8.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F8.ToString("f3");
            txt_DL_IG_M9.Text = PSC_Moment_Forces.DL_INNER_GIRDER.F9.ToString("f3");



            txt_DL_OG_M1.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F1.ToString("f3");
            txt_DL_OG_M2.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F2.ToString("f3");
            txt_DL_OG_M3.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F3.ToString("f3");
            txt_DL_OG_M4.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F4.ToString("f3");
            txt_DL_OG_M5.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F5.ToString("f3");
            txt_DL_OG_M6.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F6.ToString("f3");
            txt_DL_OG_M7.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F7.ToString("f3");
            txt_DL_OG_M8.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F8.ToString("f3");
            txt_DL_OG_M9.Text = PSC_Moment_Forces.DL_OUTER_GIRDER.F9.ToString("f3");




            Results.Add("");

            File.WriteAllLines(Result_Report_DL, Results.ToArray());
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
 
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_inn_joints, "Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_inn_joints);
        
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_inn_joints, "Ton-m"));
            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("OUTER GIRDER");
            Results.Add("------------");
            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L2_inn_joints);
 
            Results.AddRange(mfrc.GetDetails("L/2 : MAX SHEAR FORCE", _L2_inn_joints, "Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L2_inn_joints);
    
            Results.AddRange(mfrc.GetDetails("L/2 : MAX BENDING MOMENT", _L2_inn_joints, "Ton-m"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_L4_out_joints);
          
            Results.AddRange(mfrc.GetDetails("L/4 : MAX SHEAR FORCE", _L4_out_joints, "Ton"));



            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_L4_out_joints);
 
            Results.AddRange(mfrc.GetDetails("L/4 : MAX BENDING MOMENT", _L4_out_joints, "Ton-m"));

            mfrc = Deck_Analysis_DL.Structure.GetJoint_ShearForce(_deff_out_joints);
 
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_out_joints, "Ton"));


            mfrc = Deck_Analysis_DL.Structure.GetJoint_MomentForce(_deff_out_joints);
   
            Results.AddRange(mfrc.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_out_joints, "Ton-m"));

            File.WriteAllLines(Result_Report_DL, Results.ToArray());
            //iApp.RunExe(Result_Report_DL);
             
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



            List<int> _L1_inn_joints = Bridge_Analysis._L1_inn_joints; //0
            List<int> _L2_inn_joints = Bridge_Analysis._L2_inn_joints; //Effective Depth
            List<int> _L3_inn_joints = Bridge_Analysis._L3_inn_joints; //  L / 5.3
            List<int> _L4_inn_joints = Bridge_Analysis._L4_inn_joints; //  L / 3.65
            List<int> _L5_inn_joints = Bridge_Analysis._L5_inn_joints; //  L / 3.05
            List<int> _L6_inn_joints = Bridge_Analysis._L6_inn_joints;//  L / 2.61
            List<int> _L7_inn_joints = Bridge_Analysis._L7_inn_joints;//  L / 2.28
            List<int> _L8_inn_joints = Bridge_Analysis._L8_inn_joints;//  L / 2.03
            List<int> _L9_inn_joints = Bridge_Analysis._L9_inn_joints;//  L / 2.

            List<int> _L1_out_joints = Bridge_Analysis._L1_out_joints;
            List<int> _L2_out_joints = Bridge_Analysis._L2_out_joints;
            List<int> _L3_out_joints = Bridge_Analysis._L3_out_joints;
            List<int> _L4_out_joints = Bridge_Analysis._L4_out_joints;
            List<int> _L5_out_joints = Bridge_Analysis._L5_out_joints;
            List<int> _L6_out_joints = Bridge_Analysis._L6_out_joints;
            List<int> _L7_out_joints = Bridge_Analysis._L7_out_joints;
            List<int> _L8_out_joints = Bridge_Analysis._L8_out_joints;
            List<int> _L9_out_joints = Bridge_Analysis._L9_out_joints;

            List<int> _deff_inn_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();

             
            MaxForce mfrc = new MaxForce();


            Results.Clear();

            //Inner Girder ShearForce
            PSC_Shear_Forces.LL_INNER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L1_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L3_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L5_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L6_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L7_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L8_inn_joints, true).Force;
            PSC_Shear_Forces.LL_INNER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L9_inn_joints, true).Force;


            //Outer Girder ShearForce
            PSC_Shear_Forces.LL_OUTER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L1_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L2_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L3_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L4_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L5_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L6_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L7_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L8_out_joints, true).Force;
            PSC_Shear_Forces.LL_OUTER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_ShearForce(_L9_out_joints, true).Force;


            txt_LL_IG_S1.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F1.ToString("f3");
            txt_LL_IG_S2.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F2.ToString("f3");
            txt_LL_IG_S3.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F3.ToString("f3");
            txt_LL_IG_S4.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F4.ToString("f3");
            txt_LL_IG_S5.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F5.ToString("f3");
            txt_LL_IG_S6.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F6.ToString("f3");
            txt_LL_IG_S7.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F7.ToString("f3");
            txt_LL_IG_S8.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F8.ToString("f3");
            txt_LL_IG_S9.Text = PSC_Shear_Forces.LL_INNER_GIRDER.F9.ToString("f3");

            txt_LL_OG_S1.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F1.ToString("f3");
            txt_LL_OG_S2.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F2.ToString("f3");
            txt_LL_OG_S3.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F3.ToString("f3");
            txt_LL_OG_S4.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F4.ToString("f3");
            txt_LL_OG_S5.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F5.ToString("f3");
            txt_LL_OG_S6.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F6.ToString("f3");
            txt_LL_OG_S7.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F7.ToString("f3");
            txt_LL_OG_S8.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F8.ToString("f3");
            txt_LL_OG_S9.Text = PSC_Shear_Forces.LL_OUTER_GIRDER.F9.ToString("f3");

             




            //Inner Girder Moment Forces
            PSC_Moment_Forces.LL_INNER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L1_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L1_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L3_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L5_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L6_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L7_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L8_inn_joints, true).Force;
            PSC_Moment_Forces.LL_INNER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L9_inn_joints, true).Force;


            //Outer Girder Moment Forces
            PSC_Moment_Forces.LL_OUTER_GIRDER.F1 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L1_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F2 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L2_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F3 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L3_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F4 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L4_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F5 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L5_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F6 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L6_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F7 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L7_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F8 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L8_out_joints, true).Force;
            PSC_Moment_Forces.LL_OUTER_GIRDER.F9 = Deck_Analysis_LL.Structure.GetJoint_MomentForce(_L9_out_joints, true).Force;


            txt_LL_IG_M1.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F1.ToString("f3");
            txt_LL_IG_M2.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F2.ToString("f3");
            txt_LL_IG_M3.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F3.ToString("f3");
            txt_LL_IG_M4.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F4.ToString("f3");
            txt_LL_IG_M5.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F5.ToString("f3");
            txt_LL_IG_M6.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F6.ToString("f3");
            txt_LL_IG_M7.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F7.ToString("f3");
            txt_LL_IG_M8.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F8.ToString("f3");
            txt_LL_IG_M9.Text = PSC_Moment_Forces.LL_INNER_GIRDER.F9.ToString("f3");



            txt_LL_OG_M1.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F1.ToString("f3");
            txt_LL_OG_M2.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F2.ToString("f3");
            txt_LL_OG_M3.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F3.ToString("f3");
            txt_LL_OG_M4.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F4.ToString("f3");
            txt_LL_OG_M5.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F5.ToString("f3");
            txt_LL_OG_M6.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F6.ToString("f3");
            txt_LL_OG_M7.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F7.ToString("f3");
            txt_LL_OG_M8.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F8.ToString("f3");
            txt_LL_OG_M9.Text = PSC_Moment_Forces.LL_OUTER_GIRDER.F9.ToString("f3");

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
            //mc = null;
            //jn_col = null;


            //_L2_inn_joints = null;
            //_L4_inn_joints = null;
            //_deff_inn_joints = null;

            //_L4_out_joints = null;
            //_deff_out_joints = null;
            #endregion

        }

        private void READ_MOMENT_SHEAR()
        {
            PSC_Moment_Forces = new PreStressedConcrete_Forces();
            PSC_Shear_Forces = new PreStressedConcrete_Forces();

            PSC_Moment_Forces.LL_INNER_GIRDER.F1 = MyList.StringToDouble(txt_LL_IG_M1.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F2 = MyList.StringToDouble(txt_LL_IG_M2.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F3 = MyList.StringToDouble(txt_LL_IG_M3.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F4 = MyList.StringToDouble(txt_LL_IG_M4.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F5 = MyList.StringToDouble(txt_LL_IG_M5.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F6 = MyList.StringToDouble(txt_LL_IG_M6.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F7 = MyList.StringToDouble(txt_LL_IG_M7.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F8 = MyList.StringToDouble(txt_LL_IG_M8.Text, 0.0);
            PSC_Moment_Forces.LL_INNER_GIRDER.F9 = MyList.StringToDouble(txt_LL_IG_M9.Text, 0.0);



            PSC_Moment_Forces.LL_OUTER_GIRDER.F1 = MyList.StringToDouble(txt_LL_OG_M1.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F2 = MyList.StringToDouble(txt_LL_OG_M2.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F3 = MyList.StringToDouble(txt_LL_OG_M3.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F4 = MyList.StringToDouble(txt_LL_OG_M4.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F5 = MyList.StringToDouble(txt_LL_OG_M5.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F6 = MyList.StringToDouble(txt_LL_OG_M6.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F7 = MyList.StringToDouble(txt_LL_OG_M7.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F8 = MyList.StringToDouble(txt_LL_OG_M8.Text, 0.0);
            PSC_Moment_Forces.LL_OUTER_GIRDER.F9 = MyList.StringToDouble(txt_LL_OG_M9.Text, 0.0);



            PSC_Shear_Forces.LL_INNER_GIRDER.F1 = MyList.StringToDouble(txt_LL_IG_S1.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F2 = MyList.StringToDouble(txt_LL_IG_S2.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F3 = MyList.StringToDouble(txt_LL_IG_S3.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F4 = MyList.StringToDouble(txt_LL_IG_S4.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F5 = MyList.StringToDouble(txt_LL_IG_S5.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F6 = MyList.StringToDouble(txt_LL_IG_S6.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F7 = MyList.StringToDouble(txt_LL_IG_S7.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F8 = MyList.StringToDouble(txt_LL_IG_S8.Text, 0.0);
            PSC_Shear_Forces.LL_INNER_GIRDER.F9 = MyList.StringToDouble(txt_LL_IG_S9.Text, 0.0);

            PSC_Shear_Forces.LL_OUTER_GIRDER.F1 = MyList.StringToDouble(txt_LL_OG_S1.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F2 = MyList.StringToDouble(txt_LL_OG_S2.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F3 = MyList.StringToDouble(txt_LL_OG_S3.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F4 = MyList.StringToDouble(txt_LL_OG_S4.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F5 = MyList.StringToDouble(txt_LL_OG_S5.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F6 = MyList.StringToDouble(txt_LL_OG_S6.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F7 = MyList.StringToDouble(txt_LL_OG_S7.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F8 = MyList.StringToDouble(txt_LL_OG_S8.Text, 0.0);
            PSC_Shear_Forces.LL_OUTER_GIRDER.F9 = MyList.StringToDouble(txt_LL_OG_S9.Text, 0.0);





            PSC_Moment_Forces.DL_INNER_GIRDER.F1 = MyList.StringToDouble(txt_DL_IG_M1.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F2 = MyList.StringToDouble(txt_DL_IG_M2.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F3 = MyList.StringToDouble(txt_DL_IG_M3.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F4 = MyList.StringToDouble(txt_DL_IG_M4.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F5 = MyList.StringToDouble(txt_DL_IG_M5.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F6 = MyList.StringToDouble(txt_DL_IG_M6.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F7 = MyList.StringToDouble(txt_DL_IG_M7.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F8 = MyList.StringToDouble(txt_DL_IG_M8.Text, 0.0);
            PSC_Moment_Forces.DL_INNER_GIRDER.F9 = MyList.StringToDouble(txt_DL_IG_M9.Text, 0.0);



            PSC_Moment_Forces.DL_OUTER_GIRDER.F1 = MyList.StringToDouble(txt_DL_OG_M1.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F2 = MyList.StringToDouble(txt_DL_OG_M2.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F3 = MyList.StringToDouble(txt_DL_OG_M3.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F4 = MyList.StringToDouble(txt_DL_OG_M4.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F5 = MyList.StringToDouble(txt_DL_OG_M5.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F6 = MyList.StringToDouble(txt_DL_OG_M6.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F7 = MyList.StringToDouble(txt_DL_OG_M7.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F8 = MyList.StringToDouble(txt_DL_OG_M8.Text, 0.0);
            PSC_Moment_Forces.DL_OUTER_GIRDER.F9 = MyList.StringToDouble(txt_DL_OG_M9.Text, 0.0);






            PSC_Shear_Forces.DL_INNER_GIRDER.F1 = MyList.StringToDouble(txt_DL_IG_S1.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F2 = MyList.StringToDouble(txt_DL_IG_S2.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F3 = MyList.StringToDouble(txt_DL_IG_S3.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F4 = MyList.StringToDouble(txt_DL_IG_S4.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F5 = MyList.StringToDouble(txt_DL_IG_S5.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F6 = MyList.StringToDouble(txt_DL_IG_S6.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F7 = MyList.StringToDouble(txt_DL_IG_S7.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F8 = MyList.StringToDouble(txt_DL_IG_S8.Text, 0.0);
            PSC_Shear_Forces.DL_INNER_GIRDER.F9 = MyList.StringToDouble(txt_DL_IG_S9.Text, 0.0);

            PSC_Shear_Forces.DL_OUTER_GIRDER.F1 = MyList.StringToDouble(txt_DL_OG_S1.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F2 = MyList.StringToDouble(txt_DL_OG_S2.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F3 = MyList.StringToDouble(txt_DL_OG_S3.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F4 = MyList.StringToDouble(txt_DL_OG_S4.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F5 = MyList.StringToDouble(txt_DL_OG_S5.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F6 = MyList.StringToDouble(txt_DL_OG_S6.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F7 = MyList.StringToDouble(txt_DL_OG_S7.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F8 = MyList.StringToDouble(txt_DL_OG_S8.Text, 0.0);
            PSC_Shear_Forces.DL_OUTER_GIRDER.F9 = MyList.StringToDouble(txt_DL_OG_S9.Text, 0.0);


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
                //btn_dwg_main_girder.Enabled = File.Exists(LongGirder.user_drawing_file);
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
            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            
            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //Deck_Load_Analysis_Data();
            Deck_Initialize_InputData();
            Button_Enable_Disable();

            Calculate_Interactive_Values();

            Button_Enable_Disable();
        }

        public void Open_AnalysisFile_2013_04_26(string file_name)
        {
            string analysis_file = Path.GetDirectoryName(file_name);

            #region

            //if (!File.Exists(analysis_file)) return;
            //user_path = Path.GetDirectoryName(file_name);
            //wrkg_folder = Path.GetDirectoryName(analysis_file);

            //string dl_file, ll_file;

            //if (Path.GetFileName(wrkg_folder) == "Dead Load Analysis")
            //{
            //    user_path = Path.GetDirectoryName(user_path);
            //    Deck_Analysis_DL.Input_File = analysis_file;
            //    dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
            //    dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
            //    if (File.Exists(dl_file))
            //    {
            //        Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, dl_file);
            //        Show_Moment_Shear_DL();
            //    }

            //    wrkg_folder = Path.GetDirectoryName(wrkg_folder);
            //    wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");

            //    ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");
            //    Deck_Analysis_LL.Input_File = ll_file;
            //    ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");


            //    ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;


            //    if (File.Exists(ll_file))
            //    {
            //        Deck_Analysis_LL.Truss_Analysis = null;
            //        Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ll_file);
            //        Show_Moment_Shear_LL();
            //    }
            //}
            //else if (Path.GetFileName(wrkg_folder) == "Live Load Analysis")
            //{
            //    user_path = Path.GetDirectoryName(user_path);
            //    ll_file = analysis_file;

            //    Deck_Analysis_LL.Input_File = analysis_file;
            //    ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
            //    ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
            //    if (File.Exists(ll_file))
            //    {
            //        Deck_Analysis_LL.Truss_Analysis = null;
            //        Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ll_file);
            //        Show_Moment_Shear_LL();
            //    }

            //    wrkg_folder = Path.GetDirectoryName(wrkg_folder);
            //    wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");

            //    dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
            //    Deck_Analysis_DL.Input_File = dl_file;
            //    dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");

            //    dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
            //    if (File.Exists(dl_file))
            //    {

            //        Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, dl_file);
            //        Show_Moment_Shear_DL();
            //    }
            //}
            //else
            //{
            //    ll_file = analysis_file;
            //    bool flag = false;
            //    if (File.Exists(ll_file))
            //    {
            //        List<string> list = new List<string>(File.ReadAllLines(ll_file));
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            if (list[i].ToUpper().Contains("LOAD GENERATION"))
            //            {
            //                flag = true;
            //                break;
            //            }
            //        }
            //    }

            //    if (flag)
            //    {
            //        wrkg_folder = Path.GetDirectoryName(analysis_file);
            //        Deck_Analysis_LL.Input_File = analysis_file;
            //        ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");

            //        if (!File.Exists(ll_file)) ll_file = analysis_file;
            //        if (File.Exists(ll_file))
            //        {
            //            Deck_Analysis_LL.Truss_Analysis = null;
            //            Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ll_file);
            //            Show_Moment_Shear_LL();
            //        }
            //        MessageBox.Show(this, "Dead load data file not found.");
            //    }
            //    else
            //    {
            //        dl_file = analysis_file;
            //        wrkg_folder = Path.GetDirectoryName(analysis_file);
            //        Deck_Analysis_DL.Input_File = analysis_file;
            //        dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
            //        if (File.Exists(dl_file))
            //        {
            //            Deck_Analysis_DL.Truss_Analysis = null;
            //            Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, dl_file);
            //            Show_Moment_Shear_DL();
            //        }
            //        MessageBox.Show(this, "Live load data file not found.");
            //    }
            //}




            //if (File.Exists(analysis_file))
            //{
            //    btn_Ana_view_structure.Enabled = true;

            //    //Deck_Analysis_DL.Input_File = (file_name);
            //    //string rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            //    //if (File.Exists(rep_file))
            //    //{
            //    //    Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, rep_file);
            //    //    Show_Moment_Shear_DL();
            //    //}
            //    //else
            //    //    Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, analysis_file);

            //    try
            //    {
            //        if (Deck_Analysis_LL != null)
            //        {
            //            if (Deck_Analysis_LL.Truss_Analysis.Analysis.Joints.Count > 1)
            //            {
            //                Deck_Analysis_LL.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Deck_Analysis_LL.Truss_Analysis.Analysis.Joints[1].X / Deck_Analysis_LL.Truss_Analysis.Analysis.Joints[1].Z)));
            //                txt_Ana_DL_skew_angle.Text = Deck_Analysis_LL.Skew_Angle.ToString();
            //            }
            //        }

            //        if (Deck_Analysis_LL.Truss_Analysis.Analysis.Joints.Count > 0)
            //        {
            //            txt_Ana_length.Text = Deck_Analysis_LL.Truss_Analysis.Analysis.Length.ToString();
            //            txt_Ana_X.Text = "-" + txt_Ana_length.Text;
            //            txt_Ana_DL_width.Text = Deck_Analysis_LL.Truss_Analysis.Analysis.Width.ToString();


            //            txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Truss_Analysis.Analysis.Effective_Depth.ToString();
            //            txt_Ana_DL_width_cantilever.Text = Deck_Analysis_LL.Truss_Analysis.Analysis.Width_Cantilever.ToString();

            //            txt_Ana_DL_skew_angle.Text = Deck_Analysis_LL.Truss_Analysis.Analysis.Skew_Angle.ToString();


            //            txt_gd_np.Text = (Deck_Analysis_LL.Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
            //            txt_Ana_analysis_file.Visible = true;
            //            txt_Ana_analysis_file.Text = analysis_file;
            //        }
            //        else if (Deck_Analysis_DL.Truss_Analysis.Analysis.Joints.Count > 0)
            //        {
            //            txt_Ana_length.Text = Deck_Analysis_DL.Truss_Analysis.Analysis.Length.ToString();
            //            txt_Ana_X.Text = "-" + txt_Ana_length.Text;
            //            txt_Ana_DL_width.Text = Deck_Analysis_DL.Truss_Analysis.Analysis.Width.ToString();
            //            txt_gd_np.Text = (Deck_Analysis_DL.Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
            //            txt_Ana_analysis_file.Visible = true;
            //            txt_Ana_analysis_file.Text = analysis_file;


            //            txt_Ana_DL_skew_angle.Text = Deck_Analysis_DL.Truss_Analysis.Analysis.Skew_Angle.ToString();

            //            txt_Ana_DL_eff_depth.Text = Deck_Analysis_DL.Truss_Analysis.Analysis.Effective_Depth.ToString();
            //            txt_Ana_DL_width_cantilever.Text = Deck_Analysis_DL.Truss_Analysis.Analysis.Width_Cantilever.ToString();

            //        }
            //        MessageBox.Show(this, "File opened successfully.");
            //        Show_Analysis_Result();

            //    }
            //    catch (Exception ex)
            //    {
            //        //MessageBox.Show(this, "Input file error..");

            //    }
            //}


            //string ll_txt = Path.Combine(user_path, "LL.txt");

            //Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            //if (Deck_Analysis_DL.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();

            #endregion


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
            READ_MOMENT_SHEAR();
            Long_Girder_Initialize_InputData();
            Write_Long_Girder_User_Input();
            LongGirder.Calculate_Long_Girder_Program(PSC_Moment_Forces, PSC_Shear_Forces, pcd, prd, ecgd, sec_1, sec_2, sec_3, sec_4, sec_5);
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
                    sw.WriteLine("L = {0}", txt_Long_L.Text);
                    //sw.WriteLine("a = {0}", txt_long_a.Text);
                    //sw.WriteLine("d = {0}", txt_Long_d.Text);
                    //sw.WriteLine("b = {0}", txt_Long_Dim_d.Text);
                    //sw.WriteLine("bw = {0}", txt_Long_bw.Text);
                    //sw.WriteLine("d1 = {0}", txt_Long_d1.Text);
                    //sw.WriteLine("d2 = {0}", txt_Long_d2.Text);
                    sw.WriteLine("fck = {0}", cmb_long_fck.Text);
                    sw.WriteLine("doc = {0}", txt_Long_doc.Text);
                    sw.WriteLine("fci = {0}", txt_Long_fci.Text);
                    sw.WriteLine("NS = {0}", txt_Long_NS.Text);
                    sw.WriteLine("fy = {0}", cmb_long_fy.Text);
                    sw.WriteLine("dos = {0}", txt_Long_dos.Text);
                    sw.WriteLine("sigma_cb = {0}", txt_long_sigma_c.Text);
                    sw.WriteLine("sigma_st = {0}", txt_long_sigma_st.Text);
                    sw.WriteLine("SF = {0}", txt_Long_SF.Text);
                    sw.WriteLine("m = {0}", txt_long_m.Text);
                    sw.WriteLine("Q = {0}", txt_long_Q.Text);
                    sw.WriteLine("j = {0}", txt_long_j.Text);
                    sw.WriteLine("eta = {0}", txt_long_eta.Text);
                    sw.WriteLine("FS = {0}", txt_Long_FS.Text);
                    sw.WriteLine("fp = {0}", txt_Long_fp.Text);

                    //sw.WriteLine("DL_BM_OG = {0}", txt_DL_BM_OG.Text);
                    //sw.WriteLine("DL_BM_IG = {0}", txt_DL_BM_IG.Text);
                    //sw.WriteLine("LL_BM_OG = {0}", txt_LL_BM_OG.Text);
                    //sw.WriteLine("LL_BM_IG = {0}", txt_LL_BM_IG.Text);
                    //sw.WriteLine("DL_SF_OG = {0}", txt_DL_SF_OG.Text);
                    //sw.WriteLine("DL_SF_IG = {0}", txt_DL_SF_IG.Text);
                    //sw.WriteLine("LL_SF_OG = {0}", txt_LL_SF_OG.Text);
                    //sw.WriteLine("LL_SF_IG = {0}", txt_LL_SF_IG.Text);

                    sw.WriteLine("space_long = {0}", txt_Long_SMG.Text);
                    sw.WriteLine("space_cross = {0}", txt_Long_SCG.Text);


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
                            txt_Long_doc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fci":
                            txt_Long_fci.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "NS":
                            txt_Long_NS.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fy":
                            //txt_main_girder_fy.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dos":
                            txt_Long_dos.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
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
                            txt_Long_FS.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
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
                            txt_Long_fp.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
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
        public void Long_Girder_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                LongGirder.L = MyList.StringToDouble(txt_Long_L.Text, 0.0);
                LongGirder.Is_Inner_Girder = rbtn_main_IG.Checked;
                LongGirder.deff = Deff;
                LongGirder.B = B;
                //LongGirder.d = B;
                //LongGirder.a = MyList.StringToDouble(txt_long_a.Text, 0.0);
                LongGirder.d = MyList.StringToDouble(txt_Long_d.Text, 0.0);
                //LongGirder.b = MyList.StringToDouble(txt_Long_Dim_d.Text, 0.0);
                //LongGirder.bw = MyList.StringToDouble(txt_Long_bw.Text, 0.0);
                //LongGirder.d1 = MyList.StringToDouble(txt_Long_d1.Text, 0.0);
                //LongGirder.d2 = MyList.StringToDouble(txt_Long_d2.Text, 0.0);

                LongGirder.doc = MyList.StringToDouble(txt_Long_doc.Text, 0.0);
                LongGirder.fci = MyList.StringToDouble(txt_Long_fci.Text, 0.0);
                LongGirder.NS = MyList.StringToDouble(txt_Long_NS.Text, 0.0);
                LongGirder.dos = MyList.StringToDouble(txt_Long_dos.Text, 0.0);


                LongGirder.fck = MyList.StringToDouble(cmb_long_fck.Text, 0.0);
                LongGirder.fy = MyList.StringToDouble(cmb_long_fy.Text, 0.0);
                LongGirder.sigma_cb = MyList.StringToDouble(txt_long_sigma_c.Text, 0.0);
                LongGirder.sigma_st = MyList.StringToDouble(txt_long_sigma_st.Text, 0.0);
                LongGirder.SF = MyList.StringToDouble(txt_Long_SF.Text, 0.0);
                LongGirder.m = MyList.StringToDouble(txt_long_m.Text, 0.0);
                LongGirder.Q = MyList.StringToDouble(txt_long_Q.Text, 0.0);
                LongGirder.j = MyList.StringToDouble(txt_long_j.Text, 0.0);
                LongGirder.eta = MyList.StringToDouble(txt_long_eta.Text, 0.0);
                LongGirder.FS = MyList.StringToDouble(txt_Long_FS.Text, 0.0);

                //Chiranjit [2012 08 07]
                //LongGirder.DL_BM_OG = MyList.StringToDouble(txt_DL_BM_OG.Text, 0.0);
                //LongGirder.DL_BM_IG = MyList.StringToDouble(txt_DL_BM_IG.Text, 0.0);
                //LongGirder.LL_BM_OG = MyList.StringToDouble(txt_LL_BM_OG.Text, 0.0);

                //LongGirder.LL_BM_IG = MyList.StringToDouble(txt_LL_BM_IG.Text, 0.0);

                //LongGirder.DL_SF_OG = MyList.StringToDouble(txt_DL_SF_OG.Text, 0.0);
                //LongGirder.DL_SF_IG = MyList.StringToDouble(txt_DL_SF_IG.Text, 0.0);

                //LongGirder.LL_SF_OG = MyList.StringToDouble(txt_LL_SF_OG.Text, 0.0);
                //LongGirder.LL_SF_IG = MyList.StringToDouble(txt_LL_SF_IG.Text, 0.0);

                LongGirder.fp = MyList.StringToDouble(txt_Long_fp.Text, 0.0);
                LongGirder.space_long = MyList.StringToDouble(txt_Long_SMG.Text, 0.0);
                LongGirder.space_cross = MyList.StringToDouble(txt_Long_SCG.Text, 0.0);



                //Chiranjit [2012 06 22]
                //Add new Fields
                LongGirder.Es = MyList.StringToDouble(txt_Long_Es.Text, 0.0);
                LongGirder._Ec = MyList.StringToDouble(txt_Long_Ec.Text, 0.0);
                LongGirder.ecc = MyList.StringToDouble(txt_Long_ecc.Text, 0.0);
                LongGirder.phi = MyList.StringToDouble(txt_Long_phi.Text, 0.0);
                LongGirder.Atr = MyList.StringToDouble(txt_Long_Atr.Text, 0.0);

                //Chiranjit [2013 07 01]
                //Add Cable Type
                LongGirder.Cable_Type_abcd = cmb_CD_abcd_strand_data.Text;
                LongGirder.Cable_Type_e = cmb_CD_e_strand_data.Text;




                pcd = new PSC_Cable_Data();
                pcd.ab1 = MyList.StringToDouble(txt_CD_AB_1.Text, 0.0);
                pcd.ab2 = MyList.StringToDouble(txt_CD_AB_2.Text, 0.0);
                pcd.dba = MyList.StringToDouble(txt_CD_dba.Text, 0.0);
                pcd.vwc = MyList.StringToDouble(txt_CD_vwc.Text, 0.0);
                pcd.vfc = MyList.StringToDouble(txt_CD_vfc.Text, 0.0);
                pcd.idac = MyList.StringToDouble(txt_CD_idac.Text, 0.0);
                pcd.abcd_nspc = MyList.StringToDouble(txt_CD_abcd_nspc.Text, 0.0);
                pcd.abcd_uss = MyList.StringToDouble(txt_CD_abcd_uss.Text, 0.0);
                pcd.abcd_sf = MyList.StringToDouble(txt_CD_abcd_sf.Text, 0.0);
               
                //pcd.abcd_fss = MyList.StringToDouble(txt_CD_abcd_fss.Text, 0.0);
                //pcd.abcd_fc = MyList.StringToDouble(txt_CD_abcd_fc.Text, 0.0);

                pcd.abcd_csac = MyList.StringToDouble(txt_CD_abcd_csac.Text, 0.0);
                txt_CD_abcd_fss.Text = pcd.abcd_fss.ToString("f3");
                txt_CD_abcd_fc.Text = pcd.abcd_fc.ToString("f3");


                pcd.e_nspc = MyList.StringToDouble(txt_CD_e_nspc.Text, 0.0);
                pcd.e_uss = MyList.StringToDouble(txt_CD_e_uss.Text, 0.0);
                pcd.e_sf = MyList.StringToDouble(txt_CD_e_sf.Text, 0.0);
                //pcd.e_fss = MyList.StringToDouble(txt_CD_e_fss.Text, 0.0);
                //pcd.e_fc = MyList.StringToDouble(txt_CD_e_fpc.Text, 0.0);
                txt_CD_e_fss.Text = pcd.e_fss.ToString("f3");
                txt_CD_e_fpc.Text = pcd.e_fc.ToString("f3");

                pcd.e_csac = MyList.StringToDouble(txt_CD_e_csac.Text, 0.0);
                pcd.cover1 = MyList.StringToDouble(txt_CD_e_cov1.Text, 0.0);
                pcd.cover2 = MyList.StringToDouble(txt_CD_e_cov2.Text, 0.0);


                prd = new PSC_Reinforcement_Data();
                ecgd = new PSC_EndCrossGirder_Data();

                prd.DSR_Asv_dia = MyList.StringToDouble(txt_rd_dsr_Asv_dia.Text, 0.0);
                prd.DSR_Asv_spacing = MyList.StringToDouble(txt_rd_dsr_Asv_spacing.Text, 0.0);
                prd.DSR_Asv_Legs_End_span = MyList.StringToDouble(txt_rd_dsr_Asv_Legs_End_span.Text, 0.0);
                prd.DSR_Asv_Legs_Mid_span = MyList.StringToDouble(txt_rd_dsr_Asv_Legs_Mid_span.Text, 0.0);
                prd.DSR_Asv_Legs_Mid_span = MyList.StringToDouble(txt_rd_dsr_Asv_Legs_Mid_span.Text, 0.0);

                prd.DRBTF_Ast_dia = MyList.StringToDouble(txt_rd_drbtf_Ast_dia.Text, 0.0);
                prd.DRBTF_Ast_Nos = MyList.StringToDouble(txt_rd_drbtf_Ast_Nos.Text, 0.0);
                prd.DRBTF_Ast_Layers = MyList.StringToDouble(txt_rd_drbtf_Ast_Layers.Text, 0.0);
                
                prd.DSC_Ash_dia = MyList.StringToDouble(txt_rd_dsc_Ash_dia.Text, 0.0);
                prd.DSC_Ash_Leg = MyList.StringToDouble(txt_rd_dsc_Ash_Leg.Text, 0.0);
                prd.DSC_Ash_Spacing = MyList.StringToDouble(txt_rd_dsc_Ash_Spacing.Text, 0.0);
                prd.DSC_fy = MyList.StringToDouble(txt_rd_dsc_sigma_st.Text, 0.0);

                prd.DTSR_fy = MyList.StringToDouble(cmb_rd_dtsr_fy.Text, 0.0);
                prd.DTSR_sigma_y = MyList.StringToDouble(txt_rd_dtsr_sigma_y.Text, 0.0);
                prd.DTSR_Ls = MyList.StringToDouble(txt_rd_dtsr_Ls.Text, 0.0);
                prd.DTSR_fck = MyList.StringToDouble(cmb_rd_dtsr_fck.Text, 0.0);
                prd.DTSR_Ats_dia = MyList.StringToDouble(txt_rd_dtsr_Ats_dia.Text, 0.0);
                prd.DTSR_Ats_Spacing = MyList.StringToDouble(txt_rd_dtsr_Ats_spacing.Text, 0.0);

                prd.DED_b = MyList.StringToDouble(txt_rd_ded_b.Text, 0.0);
                prd.DED_D = MyList.StringToDouble(txt_rd_ded_D.Text, 0.0);
                prd.DED_cover = MyList.StringToDouble(txt_rd_ded_cover.Text, 0.0);
                prd.DED_dia_Ast1 = MyList.StringToDouble(txt_rd_ded_dia_Ast1.Text, 0.0);
                prd.DED_Nos_Ast1 = MyList.StringToDouble(txt_rd_ded_Nos_Ast1.Text, 0.0);
                prd.DED_dia_Ast2 = MyList.StringToDouble(txt_rd_ded_dia_Ast2.Text, 0.0);
                prd.DED_Nos_Ast2 = MyList.StringToDouble(txt_rd_ded_Nos_Ast2.Text, 0.0);
                prd.DED_dia_dsh = MyList.StringToDouble(txt_rd_ded_dia_dsh.Text, 0.0);
                prd.DED_Leg_dsh = MyList.StringToDouble(txt_rd_ded_Leg_dsh.Text, 0.0);
                prd.DED_Spacing_dsh = MyList.StringToDouble(txt_rd_ded_Spacing_dsh.Text, 0.0);

                prd.DID_b = MyList.StringToDouble(txt_rd_did_b.Text, 0.0);
                prd.DID_D = MyList.StringToDouble(txt_rd_did_D.Text, 0.0);
                prd.DID_cover = MyList.StringToDouble(txt_rd_did_cover.Text, 0.0);
                prd.DID_dia_Ast1 = MyList.StringToDouble(txt_rd_did_dia_Ast1.Text, 0.0);
                prd.DID_Nos_Ast1 = MyList.StringToDouble(txt_rd_did_Nos_Ast1.Text, 0.0);
                prd.DID_dia_Ast2 = MyList.StringToDouble(txt_rd_did_dia_Ast2.Text, 0.0);
                prd.DID_Nos_Ast2 = MyList.StringToDouble(txt_rd_did_Nos_Ast2.Text, 0.0);
                prd.DID_dia_dsh = MyList.StringToDouble(txt_rd_did_dia_dsh.Text, 0.0);
                prd.DID_Leg_dsh = MyList.StringToDouble(txt_rd_did_Leg_dsh.Text, 0.0);
                prd.DID_Spacing_dsh = MyList.StringToDouble(txt_rd_did_Spacing_dsh.Text, 0.0);



                ecgd.Asp_dia = MyList.StringToDouble(txt_ecgd_Asp_dia.Text, 0.0);
                ecgd.Asp_Nos = MyList.StringToInt(txt_ecgd_Asp_nos.Text, 0);

                ecgd.Asn1_dia = MyList.StringToDouble(txt_ecgd_Asn1_dia.Text, 0.0);
                ecgd.Asn1_Nos = MyList.StringToInt(txt_ecgd_Asn1_nos.Text, 0);

                ecgd.Asn2_dia = MyList.StringToDouble(txt_ecgd_Asn2_dia.Text, 0.0);
                ecgd.Asn2_Nos = MyList.StringToInt(txt_ecgd_Asn2_nos.Text, 0);


                ecgd.Dc = MyList.StringToDouble(txt_ecgd_Dc.Text, 0.0);
                ecgd.Bc = MyList.StringToDouble(txt_ecgd_Bc.Text, 0.0);
                ecgd.L = MyList.StringToDouble(txt_ecgd_L.Text, 0.0);



                //Chiranjit[2013 06 30]

                LongGirder.Node_Displacement_Data = txt_node_displacement.Text;


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
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
            pic_rcc_deck.BackgroundImage = AstraFunctionOne.ImageCollection.TBeam_Main_Girder_Bottom_Flange;
            pic_rcc_deck_example.BackgroundImage = AstraFunctionOne.ImageCollection.prestressed_bridge_image01;

            Deck_Analysis_DL = new PSC_I_Girder_Long_Analysis(iApp);
            Deck_Analysis_LL = new PSC_I_Girder_Long_Analysis(iApp);
            Bridge_Analysis = new PSC_I_Girder_Long_Analysis(iApp);
            LongGirder = new PostTensionLongGirder(iApp);
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
            //cmb_main_DL_SF.SelectedIndex = 0;
            //cmb_main_LL_SF.SelectedIndex = 0;


            cmb_CD_abcd_strand_data.SelectedIndex = 1;
            cmb_CD_e_strand_data.SelectedIndex = 1;

            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 0;

            //cmb_cant_select_load.Items.Clear();
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
            cant_pictureBox2.BackgroundImage = AstraFunctionOne.ImageCollection.TBeam_Main_Girder_Bottom_Flange;


            cmb_cant_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_cant_select_load.SelectedIndex = 0;

            #endregion Cantilever Slab Data

            cmb_long_fck.SelectedIndex = 6;
            cmb_long_fy.SelectedIndex = 2;

            cmb_cant_fck.SelectedIndex = 2;
            cmb_cant_fy.SelectedIndex = 1;

            cmb_deck_fck.SelectedIndex = 2;
            cmb_deck_fy.SelectedIndex = 1;

            cmb_abut_fck.SelectedIndex = 2;
            cmb_abut_fy.SelectedIndex = 1;

            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;


            cmb_rd_dsc_fy.SelectedIndex = 1;

            cmb_rd_dtsr_fy.SelectedIndex = 2;
            cmb_rd_dtsr_fck.SelectedIndex = 7;




            PSC_Section_Properties();
            Text_Changed();
            Button_Enable_Disable();


            rbtn_3_lane.Checked = true;
            rbtn_2_lane.Checked = true;

            Set_Project_Name();
       

        }
        public void Open_Project()
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
            //iApp.SetDrawingFile_Path(LongGirder.user_drawing_file, "PreStressed_Main_Girder", "PSC_I_Girder");
            iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC I Girder Super Structure Drawings"), "PSC_Main_I_Girder");
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

        private void txt_Ana_cross_max_shear_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Enter(object sender, EventArgs e)
        {
            //splitContainer1.SplitterDistance = 500;
            //splitContainer1.SplitterDistance = 250;
        }
        private void splitContainer1_Panel2_Enter(object sender, EventArgs e)
        {
            //splitContainer1.SplitterDistance = 500;
            //splitContainer1.SplitterDistance = 250;
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
                    txt_Ana_X.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].Distance.ToString("f3"); // Chiranjit [2013 05 28] Kolkata
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


            CW = B - Bs - 2 * Wp;

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

            //if (Bridge_Analysis.Length == 0)
            //{
            Bridge_Analysis.Length = L;
            Bridge_Analysis.Effective_Depth = Deff;



            txt_Abut_d1.Text = (DMG + Ds - MyList.StringToDouble(txt_abut_d3.Text, 0.0)).ToString("f3");
            //txt_RCC_Pier_DMG.Text = DMG.ToString("f3");
            txt_RCC_Pier_DS.Text = Ds.ToString("f3");



            //}
            txt_L1.Text = "0.000";
            txt_L2.Text = Bridge_Analysis.L2.ToString("f3");
            txt_L3.Text = Bridge_Analysis.L3.ToString("f3");
            txt_L4.Text = Bridge_Analysis.L4.ToString("f3");
            txt_L5.Text = Bridge_Analysis.L5.ToString("f3");
            txt_L6.Text = Bridge_Analysis.L6.ToString("f3");
            txt_L7.Text = Bridge_Analysis.L7.ToString("f3");
            txt_L8.Text = Bridge_Analysis.L8.ToString("f3");
            txt_L9.Text = Bridge_Analysis.L9.ToString("f3");


        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();
            //txt_Ana_X.Text = "-" + txt_Ana_L.Text;

            //if (((TextBox)sender).Name == txt_Ana_B.Name)
            //    CW = B - 2.0;

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
            list.Add(string.Format("                ***********************************"));
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

            if (ctrl.Name.ToLower().StartsWith("cmb_long"))
            {
                astg = new ASTRAGrade(cmb_long_fck.Text, cmb_long_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_long_m.Text, 10.0);
                txt_long_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_long_sigma_st.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
                txt_long_j.Text = astg.j.ToString("f3");
                txt_long_Q.Text = astg.Q.ToString("f3");
                txt_long_eta.Text = astg.n.ToString("f3");
            }
            if (ctrl.Name.ToLower().StartsWith("cmb_rd"))
            {
                astg = new ASTRAGrade(cmb_rd_dtsr_fck.Text, cmb_rd_dtsr_fy.Text);
                txt_rd_dtsr_sigma_y.Text = astg.sigma_st_N_sq_mm.ToString("f2");

                astg = new ASTRAGrade(cmb_rd_dtsr_fck.Text, cmb_rd_dsc_fy.Text);
                txt_rd_dsc_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString("f2");


              

                //txt_rd_dsc_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString("f2");
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
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            if (showMessage) DemoCheck();

            iApp.Save_Form_Record(this, user_path);

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
            file_content.Add(string.Format(kFormat, "Post Tension Cable Diameter", "doc", txt_Long_doc.Text));
            file_content.Add(string.Format(kFormat, "Cube Strength at Transfer", "fci", txt_Long_fci.Text));
            file_content.Add(string.Format(kFormat, "Freyssinet Anchorable Number of Strands", "NS", txt_Long_NS.Text));
            file_content.Add(string.Format(kFormat, "Steel Grade", "fy", cmb_long_fy.Text));
            file_content.Add(string.Format(kFormat, "Diameter of Strands", "dos", txt_Long_dos.Text));
            file_content.Add(string.Format(kFormat, "Permissible compressive stress in concrete ", "σ_cb", txt_long_sigma_c.Text));
            file_content.Add(string.Format(kFormat, "Permissible tensile stress in steel", "σ_st", txt_long_sigma_st.Text));
            file_content.Add(string.Format(kFormat, "Strand Factor ", "SF", txt_Long_SF.Text));
            file_content.Add(string.Format(kFormat, "Moment Factor ", "Q", txt_long_Q.Text));
            file_content.Add(string.Format(kFormat, "Lever Arm Factor", "j", txt_long_j.Text));
            file_content.Add(string.Format(kFormat, "Loss Ratio", "n", txt_long_eta.Text));
            file_content.Add(string.Format(kFormat, "Force per Strand", "FS", txt_Long_FS.Text));

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
                                            txt_Long_doc.Text = mlist.StringList[2];
                                            break;
                                        case "fci":
                                            txt_Long_fci.Text = mlist.StringList[2];
                                            break;
                                        case "NS":
                                            txt_Long_NS.Text = mlist.StringList[2];
                                            break;
                                        case "fy":
                                            cmb_long_fy.SelectedItem = mlist.StringList[2];
                                            break;
                                        case "dos":
                                            txt_Long_dos.Text = mlist.StringList[2];
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
                                            txt_Long_FS.Text = mlist.StringList[2];
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
            ll.Add("\t\t*                  ASTRA Pro                  *");
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
        private void cmb_Girder_strand_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            double D = 0.0, A = 0.0, Pu = 0.0, Fy = 0.0, Fu = 0.0, Pn = 0.0;

            ComboBox cmb = sender as ComboBox;

            if (cmb.SelectedIndex == 0)
            {
                D = 12.9;
                A = 100.0;
                Pu = 0.785;
                Fy = 1580.0;
                Fu = 1860.0;
                Pn = 186.0;
            }
            else if (cmb.SelectedIndex == 1)
            {
                D = 12.7;
                A = 98.7;
                Pu = 0.775;
                Fy = 1670.0;
                Fu = 1860.0;
                Pn = 183.7;
            }
            else if (cmb.SelectedIndex == 2)
            {
                D = 15.7;
                A = 150;
                Pu = 1.18;
                Fy = 1500;
                Fu = 1770;
                Pn = 265.0;
            }
            else if (cmb.SelectedIndex == 3)
            {
                D = 15.2;
                A = 140;
                Pu = 1.10;
                Fy = 1670.0;
                Fu = 1860.0;
                Pn = 260.7;
            }


            if (cmb.Name == cmb_CD_abcd_strand_data.Name)
            {
                txt_CD_abcd_uss.Text = Pn.ToString();
                txt_CD_abcd_sf.Text = Pu.ToString();
                txt_CD_abcd_A.Text = A.ToString();
                txt_CD_abcd_csac.Text = (MyList.StringToDouble(txt_CD_abcd_nspc.Text, 0.0) * A).ToString("f2");
            }
            else
            {
                txt_CD_e_uss.Text = Pn.ToString();
                txt_CD_e_sf.Text = Pu.ToString();
                txt_CD_e_A.Text = A.ToString();
                txt_CD_e_csac.Text = (MyList.StringToDouble(txt_CD_e_nspc.Text, 0.0) * A).ToString("f2");
            }
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
                return eASTRADesignType.PSC_I_Girder_Bridge_WS_Long_Span;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]


    }



    public class PSC_I_Girder_Long_Analysis
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

        //Chiranjit [2013 06 06]
        string list_envelop_outer = "";
        string list_envelop_inner = "";

        string input_file, working_folder, user_path;
        public PSC_I_Girder_Long_Analysis(IApplication thisApp)
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

        public string L1_Girders_as_String { get; set; }
        public string L2_Girders_as_String { get; set; }
        public string L3_Girders_as_String { get; set; }
        public string L4_Girders_as_String { get; set; }
        public string L5_Girders_as_String { get; set; }
        public string L6_Girders_as_String { get; set; }
        public string L7_Girders_as_String { get; set; }
        public string L8_Girders_as_String { get; set; }
        public string L9_Girders_as_String { get; set; }





        public string Deff_Girders_as_String { get; set; }
        public string Inner_Girders_as_String { get; set; }
        public string Outer_Girders_as_String { get; set; }

        public string Cross_Girders_as_String { get; set; }



        public List<int> _L1_inn_joints = new List<int>();
        public List<int> _L2_inn_joints = new List<int>();
        public List<int> _L3_inn_joints = new List<int>();
        public List<int> _L4_inn_joints = new List<int>();
        public List<int> _L5_inn_joints = new List<int>();
        public List<int> _L6_inn_joints = new List<int>();
        public List<int> _L7_inn_joints = new List<int>();
        public List<int> _L8_inn_joints = new List<int>();
        public List<int> _L9_inn_joints = new List<int>();




        public List<int> _L1_out_joints = new List<int>();
        public List<int> _L2_out_joints = new List<int>();
        public List<int> _L3_out_joints = new List<int>();
        public List<int> _L4_out_joints = new List<int>();
        public List<int> _L5_out_joints = new List<int>();
        public List<int> _L6_out_joints = new List<int>();
        public List<int> _L7_out_joints = new List<int>();
        public List<int> _L8_out_joints = new List<int>();
        public List<int> _L9_out_joints = new List<int>();
     





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
                else if ((MemColls[i].StartNode.Z <= Width_LeftCantilever &&
                    MemColls[i].EndNode.Z <= Width_LeftCantilever) ||
                    (MemColls[i].StartNode.Z >= (WidthBridge - Width_RightCantilever) &&
                    MemColls[i].EndNode.Z >= (WidthBridge - Width_RightCantilever)))
                {
                    //Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z == WidthBridge) &&
                    (MemColls[i].EndNode.Z == WidthBridge))
                {
                    //Outer_Girder.Add(MemColls[i].MemberNo);
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


            List<int> Deff_Girders = new List<int>();
            List<int> Cross_Girder = new List<int>();


            List<int> L1_Girders = new List<int>();
            List<int> L2_Girders = new List<int>();
            List<int> L3_Girders = new List<int>();
            List<int> L4_Girders = new List<int>();
            List<int> L5_Girders = new List<int>();
            List<int> L6_Girders = new List<int>();
            List<int> L7_Girders = new List<int>();
            List<int> L8_Girders = new List<int>();
            List<int> L9_Girders = new List<int>();
             

            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    if (_L1_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
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
                    else
                    {
                        Deff_Girders.Add(MemColls[i].MemberNo);
                    }

                }
            }

            L1_Girders.Sort();
            L2_Girders.Sort();
            L3_Girders.Sort();
            L4_Girders.Sort();
            L5_Girders.Sort();
            L6_Girders.Sort();
            L7_Girders.Sort();
            L8_Girders.Sort();
            L9_Girders.Sort();

            Cross_Girder.Sort();


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            L1_Girders_as_String = MyList.Get_Array_Text(L1_Girders);
            L2_Girders_as_String = MyList.Get_Array_Text(L2_Girders);
            L3_Girders_as_String = MyList.Get_Array_Text(L3_Girders);
            L4_Girders_as_String = MyList.Get_Array_Text(L4_Girders);
            L5_Girders_as_String = MyList.Get_Array_Text(L5_Girders);
            L6_Girders_as_String = MyList.Get_Array_Text(L6_Girders);
            L7_Girders_as_String = MyList.Get_Array_Text(L7_Girders);
            L8_Girders_as_String = MyList.Get_Array_Text(L8_Girders);
            L9_Girders_as_String = MyList.Get_Array_Text(L9_Girders);
            Deff_Girders_as_String = MyList.Get_Array_Text(Deff_Girders);
            //Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);
            Set_Inner_Outer_Cross_Girders();
        }

        void Set_Long_Joints()
        {
            double L = Length;
            double W = WidthBridge;
            double val = L / 2;
            int i = 0;

            _L1_inn_joints.Clear();
            _L2_inn_joints.Clear();
            _L3_inn_joints.Clear();
            _L4_inn_joints.Clear();
            _L5_inn_joints.Clear();
            _L6_inn_joints.Clear();
            _L7_inn_joints.Clear();
            _L8_inn_joints.Clear();
            _L9_inn_joints.Clear();


            _L1_out_joints.Clear();
            _L2_out_joints.Clear();
            _L3_out_joints.Clear();
            _L4_out_joints.Clear();
            _L5_out_joints.Clear();
            _L6_out_joints.Clear();
            _L7_out_joints.Clear();
            _L8_out_joints.Clear();
            _L9_out_joints.Clear();
           




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

                    //if ((Joints[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    //{
                    //    _L2_inn_joints.Add(Joints[i].NodeNo);
                    //}



                    if (Joints[i].X.ToString("0.0") == ((L1) + x_min).ToString("0.0"))
                    {
                        _L1_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - (L1)) + x_min).ToString("0.0"))
                    {
                        _L1_out_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == ((L2) + x_min).ToString("0.0"))
                    {
                        _L2_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - (L2)) + x_min).ToString("0.0"))
                    {
                        _L2_out_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == ((L3) + x_min).ToString("0.0"))
                    {
                        _L3_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - (L3)) + x_min).ToString("0.0"))
                    {
                        _L3_out_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ( L3  + x_min).ToString("0.0"))
                    {
                        _L3_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L3) + x_min).ToString("0.0"))
                    {
                        _L3_out_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == (L4 + x_min).ToString("0.0"))
                    {
                        _L4_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L4) + x_min).ToString("0.0"))
                    {
                        _L4_out_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == (L5 + x_min).ToString("0.0"))
                    {
                        _L5_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L5) + x_min).ToString("0.0"))
                    {
                        _L5_out_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == (L6 + x_min).ToString("0.0"))
                    {
                        _L6_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L6) + x_min).ToString("0.0"))
                    {
                        _L6_out_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == (L7 + x_min).ToString("0.0"))
                    {
                        _L7_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L7) + x_min).ToString("0.0"))
                    {
                        _L7_out_joints.Add(Joints[i].NodeNo);
                    }

                    if (Joints[i].X.ToString("0.0") == (L8 + x_min).ToString("0.0"))
                    {
                        _L8_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L8) + x_min).ToString("0.0"))
                    {
                        _L8_out_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == (L9 + x_min).ToString("0.0"))
                    {
                        _L9_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - L9) + x_min).ToString("0.0"))
                    {
                        _L9_out_joints.Add(Joints[i].NodeNo);
                    }
                }
                catch (Exception ex) { }
            }


            //if (_L9_inn_joints.Count > 2)
            //{
            //    if (Width_LeftCantilever > 0)
            //    {
            //        _L2_out_joints.Add(_L2_inn_joints[0]);
            //        _L2_inn_joints.RemoveAt(0);
            //    }
            //    _L2_out_joints.Add(_L2_inn_joints[0]);
            //    _L2_inn_joints.RemoveAt(0);

            //    if (Width_RightCantilever > 0)
            //    {

            //        _L2_out_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
            //        _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            //    }
            //    _L2_out_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
            //    _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            //}

            List<int> temp_joints = new List<int>();

            //for L1 = L/5.3
            #region Chiranjit [2013 05 10]
            if (_L1_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L1_inn_joints[0]);
                    _L1_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L1_inn_joints[0]);
                _L1_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L1_inn_joints[_L1_inn_joints.Count - 1]);
                    _L1_inn_joints.RemoveAt(_L1_inn_joints.Count - 1);
                }
                temp_joints.Add(_L1_inn_joints[_L1_inn_joints.Count - 1]);
                _L1_inn_joints.RemoveAt(_L1_inn_joints.Count - 1);
            }

            if (_L1_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L1_out_joints[0]);
                    _L1_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L1_out_joints[0]);
                _L1_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L1_out_joints[_L1_out_joints.Count - 1]);
                    _L1_out_joints.RemoveAt(_L1_out_joints.Count - 1);
                }
                temp_joints.Add(_L1_out_joints[_L1_out_joints.Count - 1]);
                _L4_out_joints.RemoveAt(_L1_out_joints.Count - 1);
            }
            _L1_inn_joints.AddRange(_L1_out_joints.ToArray());

            _L1_out_joints.Clear();
            _L1_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]

            //for L2 = L/3.65
            #region Chiranjit [2013 05 10]
            if (_L2_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L2_inn_joints[0]);
                    _L2_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L2_inn_joints[0]);
                _L2_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
                    _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
                }
                temp_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
                _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            }

            if (_L2_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L2_out_joints[0]);
                    _L2_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L2_out_joints[0]);
                _L2_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L2_out_joints[_L2_out_joints.Count - 1]);
                    _L2_out_joints.RemoveAt(_L2_out_joints.Count - 1);
                }
                temp_joints.Add(_L2_out_joints[_L2_out_joints.Count - 1]);
                _L4_out_joints.RemoveAt(_L2_out_joints.Count - 1);
            }
            _L2_inn_joints.AddRange(_L2_out_joints.ToArray());

            _L2_out_joints.Clear();
            _L2_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]

            //for L3 = L/3.05
            #region Chiranjit [2013 05 10]
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
                temp_joints.Add(_L3_inn_joints[_L3_inn_joints.Count - 1]);
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

            #endregion  Chiranjit [2013 05 10]

            #region Chiranjit [2013 05 10]

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

            #endregion  Chiranjit [2013 05 10]

            //for L5
            #region Chiranjit [2013 05 10]
            if (_L5_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L5_inn_joints[0]);
                    _L5_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L5_inn_joints[0]);
                _L5_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L5_inn_joints[_L5_inn_joints.Count - 1]);
                    _L5_inn_joints.RemoveAt(_L5_inn_joints.Count - 1);
                }
                temp_joints.Add(_L5_inn_joints[_L5_inn_joints.Count - 1]);
                _L5_inn_joints.RemoveAt(_L5_inn_joints.Count - 1);
            }

            if (_L5_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L5_out_joints[0]);
                    _L5_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L5_out_joints[0]);
                _L5_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L5_out_joints[_L5_out_joints.Count - 1]);
                    _L5_out_joints.RemoveAt(_L5_out_joints.Count - 1);
                }
                temp_joints.Add(_L5_out_joints[_L5_out_joints.Count - 1]);
                _L5_out_joints.RemoveAt(_L5_out_joints.Count - 1);
            }
            _L5_inn_joints.AddRange(_L5_out_joints.ToArray());

            _L5_out_joints.Clear();
            _L5_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]

            #region Chiranjit [2013 05 10]
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
                    _L6_inn_joints.RemoveAt(_L6_inn_joints.Count - 1);
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
                temp_joints.Add(_L6_out_joints[_L6_out_joints.Count - 1]);
                _L6_out_joints.RemoveAt(_L6_out_joints.Count - 1);
            }
            _L6_inn_joints.AddRange(_L6_out_joints.ToArray());

            _L6_out_joints.Clear();
            _L6_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]


            #region Chiranjit [2013 05 10]
            if (_L7_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L7_inn_joints[0]);
                    _L7_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L7_inn_joints[0]);
                _L7_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L7_inn_joints[_L7_inn_joints.Count - 1]);
                    _L7_inn_joints.RemoveAt(_L7_inn_joints.Count - 1);
                }
                temp_joints.Add(_L7_inn_joints[_L7_inn_joints.Count - 1]);
                _L7_inn_joints.RemoveAt(_L7_inn_joints.Count - 1);
            }

            if (_L7_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L7_out_joints[0]);
                    _L7_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L7_out_joints[0]);
                _L7_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L7_out_joints[_L7_out_joints.Count - 1]);
                    _L7_out_joints.RemoveAt(_L7_out_joints.Count - 1);
                }
                temp_joints.Add(_L7_out_joints[_L7_out_joints.Count - 1]);
                _L7_out_joints.RemoveAt(_L7_out_joints.Count - 1);
            }
            _L7_inn_joints.AddRange(_L7_out_joints.ToArray());

            _L7_out_joints.Clear();
            _L7_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]

            #region Chiranjit [2013 05 10]
            if (_L8_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L8_inn_joints[0]);
                    _L8_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L8_inn_joints[0]);
                _L8_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L8_inn_joints[_L8_inn_joints.Count - 1]);
                    _L8_inn_joints.RemoveAt(_L8_inn_joints.Count - 1);
                }
                temp_joints.Add(_L8_inn_joints[_L8_inn_joints.Count - 1]);
                _L8_inn_joints.RemoveAt(_L8_inn_joints.Count - 1);
            }

            if (_L8_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L8_out_joints[0]);
                    _L8_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L8_out_joints[0]);
                _L8_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L8_out_joints[_L8_out_joints.Count - 1]);
                    _L8_out_joints.RemoveAt(_L8_out_joints.Count - 1);
                }
                temp_joints.Add(_L8_out_joints[_L8_out_joints.Count - 1]);
                _L8_out_joints.RemoveAt(_L8_out_joints.Count - 1);
            }
            _L8_inn_joints.AddRange(_L8_out_joints.ToArray());

            _L8_out_joints.Clear();
            _L8_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]

            #region Chiranjit [2013 05 10]
            if (_L9_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L9_inn_joints[0]);
                    _L9_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L9_inn_joints[0]);
                _L9_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L9_inn_joints[_L9_inn_joints.Count - 1]);
                    _L9_inn_joints.RemoveAt(_L9_inn_joints.Count - 1);
                }
                temp_joints.Add(_L9_inn_joints[_L9_inn_joints.Count - 1]);
                _L9_inn_joints.RemoveAt(_L9_inn_joints.Count - 1);
            }

            if (_L9_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L9_out_joints[0]);
                    _L9_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L9_out_joints[0]);
                _L9_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L9_out_joints[_L9_out_joints.Count - 1]);
                    _L9_out_joints.RemoveAt(_L9_out_joints.Count - 1);
                }
                temp_joints.Add(_L9_out_joints[_L9_out_joints.Count - 1]);
                _L9_out_joints.RemoveAt(_L9_out_joints.Count - 1);
            }
            _L9_inn_joints.AddRange(_L9_out_joints.ToArray());

            _L9_out_joints.Clear();
            _L9_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            #endregion  Chiranjit [2013 05 10]

            temp_joints.Clear();
            Set_Girders();
            joints_list_for_load.Clear();
            joints_list_for_load.Add(MyList.Get_Array_Text(_L1_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L2_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L3_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L4_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L5_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L6_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L7_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L8_inn_joints));
            joints_list_for_load.Add(MyList.Get_Array_Text(_L9_inn_joints));

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
                    if (iCols == 0)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1)
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

            Set_Long_Joints();

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

            //last_z = Width_LeftCantilever / 2;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            last_z = WidthBridge - Width_RightCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            //last_z = WidthBridge - (Width_RightCantilever / 2.0);
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);


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

            Set_Long_Joints();
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
                for (int c = 2; c < Joints[_Rows].NodeNo-1; c++)
                    k += c.ToString() + " ";
                list.Add(string.Format("{0} {1}", k, Start_Support));
                k = "";
                for (int c = Joints[Joints.Count - _Rows +1].NodeNo; c <= Joints[Joints.Count - 1 -1].NodeNo; c++)
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

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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

                //string k = "";
                //for (int c = 1; c < Joints[_Rows].NodeNo; c++)
                //    k += c.ToString() + " ";
                //list.Add(string.Format("{0} {1}", k, Start_Support));
                //k = "";
                //for (int c = Joints[Joints.Count - _Rows].NodeNo; c <= Joints[Joints.Count - 1].NodeNo; c++)
                //    k += c.ToString() + " ";
                //list.Add(string.Format("{0} {1}", k, End_Support));



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

            kStr = MyList.Get_Array_Text(MyList.Get_Array_Intiger(kStr));
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


    public class PostTensionLongGirder
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
        public double deff; // η

        public double Es, _Ec, ecc, phi, Atr;
        public double DL_BM_OG, DL_BM_IG;
        public double LL_BM_OG, LL_BM_IG;
        public double DL_SF_OG, DL_SF_IG;
        public double LL_SF_OG, LL_SF_IG;

        public double space_long, space_cross;

        string _A, _B, _C, _D, _E, _F, _G, _H, _I1, _I2, _J, _K, _L;

        public double L1 { get { return 0.0; } }
        public double L2 { get { return deff; } }
        public double L3 { get { return L / 5.3; } }
        public double L4 { get { return L / 3.65; } }
        public double L5 { get { return L / 3.05; } }
        public double L6 { get { return L / 2.61; } }
        public double L7 { get { return L / 2.28; } }
        public double L8 { get { return L / 2.03; } }
        public double L9 { get { return L / 2.00; } }
        public bool Is_Inner_Girder { get; set; }
        public bool Is_Outer_Girder
        {
            get
            {
                return !Is_Inner_Girder;
            }
            set
            {
                Is_Inner_Girder = !value;
            }
        }

        //Chiranjit [2013 06 30]
        public string Node_Displacement_Data { get; set; }

        //Chiranjit [2013 07 01]
        public string Cable_Type_abcd { get; set; }
        public string Cable_Type_e { get; set; }


        public PostTensionLongGirder(IApplication app)
        {
            iApp = app;
            _Ec = 0;
        }



        #region IReport Members
        public void Calculate_Long_Girder_Program(PreStressedConcrete_Forces Bending_Moments,
            PreStressedConcrete_Forces mem_shear_forces,
            PSC_Cable_Data pcd,
            PSC_Reinforcement_Data prd,
            PSC_EndCrossGirder_Data ecgd,
            PreStressedConcrete_SectionProperties mid_sections,
            PreStressedConcrete_SectionProperties end_sections,
            PreStressedConcrete_SectionProperties cross_sections,
            PreStressedConcrete_SectionProperties composit_mid_sections,
            PreStressedConcrete_SectionProperties composit_end_sections)
        {
            string ref_string = "";
            double I = 0.0;
            double yt = 0.0;

            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*           DESIGN OF PRESTRESSED             *");
                sw.WriteLine("\t\t*         POST TENSIONED MAIN GIRDER          *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                #endregion

                #region USER'S DATA

                double A = mid_sections.A;
                double Zt = mid_sections.Zt;
                double Zb = mid_sections.Zb;
                double finf;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("1. SECTION DIMENTION DATA");
                sw.WriteLine("   Simple Sections");
                sw.WriteLine();
                sw.WriteLine(@"     |      W      |");
                sw.WriteLine(@" --  --------------   ----");
                sw.WriteLine(@"     |    PSC      |  d1");
                sw.WriteLine(@"     |   GIRDER    |  ----");
                sw.WriteLine(@"      \           /");
                sw.WriteLine(@"       \         /     d2");
                sw.WriteLine(@"        \       /");
                sw.WriteLine(@"         |     |      ----");
                sw.WriteLine(@"         |     |");
                sw.WriteLine(@"         |     |");
                sw.WriteLine(@" H       |     |");
                sw.WriteLine(@"         |     |       d3");
                sw.WriteLine(@"         |     |");
                sw.WriteLine(@"         |     |");
                sw.WriteLine(@"         |     |");
                sw.WriteLine(@"        /       \     ----");
                sw.WriteLine(@"       /         \");
                sw.WriteLine(@"      /           \    d4");
                sw.WriteLine(@"     |             |  ----");
                sw.WriteLine(@"     |             |   d5");
                sw.WriteLine(@" --   -------------   ----");
                sw.WriteLine(@"     | b1 | b2 |b1 |");
                sw.WriteLine();
                sw.WriteLine();
                mid_sections.Write_User_Input_Details(sw);
                sw.WriteLine(@"");
                sw.WriteLine(@"     |      W      |");
                sw.WriteLine(@" --  --------------   ----");
                sw.WriteLine(@"     |             |   d1");
                sw.WriteLine(@"     |             |  ----");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |    PSC      |  ");
                sw.WriteLine(@"     |   GIRDER    |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@" H   |             |");
                sw.WriteLine(@"     |             |   d3");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |  ");
                sw.WriteLine(@"     |             |   ");
                sw.WriteLine(@" --   -------------   ----");
                sw.WriteLine(@"     |      b2     |");
                sw.WriteLine();
                sw.WriteLine();
                end_sections.Write_User_Input_Details(sw);
                sw.WriteLine(@"     |      W      |");
                sw.WriteLine(@" --  --------------   ");
                sw.WriteLine(@"     |             |   ");
                sw.WriteLine(@"     |             |  ");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     | TRANSVERSE  |");
                sw.WriteLine(@"     |    PSC      |  ");
                sw.WriteLine(@"     |   GIRDER    |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@" H   |             |");
                sw.WriteLine(@"     |             |   ");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |");
                sw.WriteLine(@"     |             |  ");
                sw.WriteLine(@"     |             |   ");
                sw.WriteLine(@" --   -------------   ");
                sw.WriteLine(@"     |      b      |");
                cross_sections.Write_User_Input_Details(sw);

                PreStressedConcrete_Forces moment_forces = new PreStressedConcrete_Forces();
                PreStressedConcrete_Forces shear_forces = new PreStressedConcrete_Forces();

                //Change Unit Ton-m  to kN-m
                moment_forces.DL_INNER_GIRDER = Bending_Moments.DL_INNER_GIRDER * 10;
                moment_forces.LL_INNER_GIRDER = Bending_Moments.LL_INNER_GIRDER * 10;
                moment_forces.DL_OUTER_GIRDER = Bending_Moments.DL_OUTER_GIRDER * 10;
                moment_forces.LL_OUTER_GIRDER = Bending_Moments.LL_OUTER_GIRDER * 10;

                shear_forces.DL_INNER_GIRDER = mem_shear_forces.DL_INNER_GIRDER * 10;
                shear_forces.LL_INNER_GIRDER = mem_shear_forces.LL_INNER_GIRDER * 10;
                shear_forces.DL_OUTER_GIRDER = mem_shear_forces.DL_OUTER_GIRDER * 10;
                shear_forces.LL_OUTER_GIRDER = mem_shear_forces.LL_OUTER_GIRDER * 10;


                //moment_forces.DL_INNER_GIRDER.UnitFactor = 10.0;
                //moment_forces.LL_INNER_GIRDER.UnitFactor = 10.0;
                //moment_forces.DL_OUTER_GIRDER.UnitFactor = 10.0;
                //moment_forces.LL_OUTER_GIRDER.UnitFactor = 10.0;

                //shear_forces.DL_INNER_GIRDER.UnitFactor = 10.0;
                //shear_forces.LL_INNER_GIRDER.UnitFactor = 10.0;
                //shear_forces.DL_OUTER_GIRDER.UnitFactor = 10.0;
                //shear_forces.LL_OUTER_GIRDER.UnitFactor = 10.0;


                sw.WriteLine();
                sw.WriteLine("Composite Sections :");
                sw.WriteLine("-------------------");

                sw.WriteLine(@"");
                sw.WriteLine(@"          |               W                |");
                sw.WriteLine(@"          ---------------------------------  --");
                sw.WriteLine(@"          |             DECK SLAB          | ds");
                sw.WriteLine(@"          |                                |                                ");
                sw.WriteLine(@"           --------------------------------  --   ");
                sw.WriteLine(@"                   |     PSC     |   d1");
                sw.WriteLine(@"                   |   GIRDER    |  ----");
                sw.WriteLine(@"                    \           /");
                sw.WriteLine(@"                     \         /     d2");
                sw.WriteLine(@"                      \       /");
                sw.WriteLine(@"                       |     |      ----");
                sw.WriteLine(@"                       |     |");
                sw.WriteLine(@"                       |     |");
                sw.WriteLine(@"           H           |     |");
                sw.WriteLine(@"                       |     |       d3");
                sw.WriteLine(@"                       |     |");
                sw.WriteLine(@"                       |     |");
                sw.WriteLine(@"                       |     |");
                sw.WriteLine(@"                      /       \     ----");
                sw.WriteLine(@"                     /         \");
                sw.WriteLine(@"                    /           \    d4");
                sw.WriteLine(@"                   |             |  ----");
                sw.WriteLine(@"                   |             |   d5");
                sw.WriteLine(@"               --   -------------   ----");
                sw.WriteLine(@"                   | b1 | b2 |b1 |");
                sw.WriteLine();
                sw.WriteLine();
                composit_mid_sections.Write_User_Input_Details(sw);
                sw.WriteLine();
                sw.WriteLine(); 
                sw.WriteLine(@"          |               W                |");
                sw.WriteLine(@"          ---------------------------------  --");
                sw.WriteLine(@"          |             DECK SLAB          | ds");
                sw.WriteLine(@"          |                                |                                ");
                sw.WriteLine(@"           --------------------------------  --   ");
                sw.WriteLine(@"                    |             |   d1");
                sw.WriteLine(@"                    |------------ |  ----");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |    PSC      |  ");
                sw.WriteLine(@"                    |   GIRDER    |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                H   |             |");
                sw.WriteLine(@"                    |             |   d3");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |");
                sw.WriteLine(@"                    |             |  ");
                sw.WriteLine(@"                    |             |   ");
                sw.WriteLine(@"                --   -------------   ----");
                sw.WriteLine(@"                    |      b2     |");
                sw.WriteLine();
                sw.WriteLine();
                composit_end_sections.Write_User_Input_Details(sw);
                sw.WriteLine();
                sw.WriteLine("2. DESIGN DATA");
                sw.WriteLine("--------------");
                sw.WriteLine();
                sw.WriteLine("Span of Girder [L] = {0} m", L);
                sw.WriteLine("Width of Bridge Deck [B] = {0} m", B);
                sw.WriteLine();
                sw.WriteLine("Spacing of Main Long Girders = {0} mm  Marked as (G) in the Drawing", space_long * 1000);
                sw.WriteLine("Spacing of Cross Girders = {0} mm      Marked as (H) in the Drawing", space_cross * 1000);
                sw.WriteLine();

                sw.WriteLine("Steel Grade [fy] = Fe {0} = {0}  N/sq.mm", fy);
                sw.WriteLine("Concrete Grade [fck] = M {0} = {0} N/sq.mm", fck);
                sw.WriteLine();
                sw.WriteLine("Cube Strength at Transfer [fci] = {0} N/sq.mm", fci);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine();
                sw.WriteLine("Elastic Modulus of Steel = Es = {0} kN/Sq.mm", Es);
                double Ec = (int)(5000.0 * Math.Sqrt(fck) / 1000.0);
                sw.WriteLine("Elastic Modulus of Concrete = Ec = (5000.0 * Sqrt(fck) / 1000.0) = {0} kN/Sq.mm", Ec);
                sw.WriteLine();
                sw.WriteLine("Modular Ratio = m =  {0}", m);
                sw.WriteLine("Permissible compressive stress in concrete [σ_cb] = {0}  N/sq.mm", sigma_cb);
                sw.WriteLine("Permissible tensile stress in steel [σ_st] = {0}  N/sq.mm", sigma_st);
                sw.WriteLine("Loss Ratio [n] = {0}", eta);
                sw.WriteLine();
                sw.WriteLine("Creep Coefficient = phi = {0}", phi);
                sw.WriteLine("Ultimate Creep Strain = ecc = {0} mm/mm ", ecc);
                sw.WriteLine("Age of Concrete at Transfer = Atr = {0} days", Atr);
                sw.WriteLine();
                //sw.WriteLine("Post Tension Cable Diameter [doc] = {0} mm", doc);
                //sw.WriteLine("Freyssinet Anchorable Number of Strands [NS] = {0}", NS);
                //sw.WriteLine("Diameter of Strands [dos] = {0} mm", dos);
                //sw.WriteLine("Strand Factor [SF] = {0}", SF);
                //sw.WriteLine("Force per Strand [FS] = {0} kN", FS);
                //sw.WriteLine();
                sw.WriteLine();



                //sw.WriteLine("Dimension [a] = {0} mm                 Marked as (A) in the Drawing", a);
                _A = string.Format("{0} mm ", a);

                //sw.WriteLine("Overall Depth of Girder [d] = {0} mm   Marked as (B) in the Drawing", d);
                _B = string.Format("{0} mm ", d);

                //sw.WriteLine("Dimension [b] = {0} mm                 Marked as (C) in the Drawing", b);
                _C = string.Format("{0} mm ", b);


                //sw.WriteLine("Dimension [bw] = {0} mm                Marked as (D) in the Drawing", bw);
                _D = string.Format("{0} mm ", bw);

                //sw.WriteLine("Dimension [d1] = {0} mm                Marked as (E) in the Drawing", d1);
                _E = string.Format("{0} mm ", d1);

                //sw.WriteLine("Dimension [d2] = {0} mm                Marked as (F) in the Drawing", d2);
                _F = string.Format("{0} mm ", d2);

                //(G) = Spacing of Main Girders 2500 mm.
                _G = string.Format("Spacing of Main Girders {0} mm ", space_long * 1000);

                //(H) = Spacing of Cross Girders 5000 mm.
                _H = string.Format("Spacing of Cross Girders {0} mm ", space_cross * 1000);

                prd.Write_Input_Data(sw);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("3. CABLE DATA");
                sw.WriteLine("-------------");
                sw.WriteLine("");
                sw.WriteLine("Cable make                = BBR Cona Compact make");
                sw.WriteLine("Sheating type             = Corrugated HDPE pipe of approved make");
                sw.WriteLine("Anchorage Block size      = {0} mm x {1} mm", pcd.ab1, pcd.ab2);
                sw.WriteLine("Distance between anchorages                 = {0} mm", pcd.dba);
                //sw.WriteLine("Value of Wooble Coeff                                 = {0} per metre length of Cable (IRC: 18-2000)");
                sw.WriteLine("Value of Wooble Coeff                                 = {0} per metre length of Cable", pcd.vwc);
                sw.WriteLine("Value of Friction Coeff                 = {0}", pcd.vfc);
                //sw.WriteLine("Value of Friction Coeff                 = 0.17 (IRC: 18-2000)");
                sw.WriteLine("Inside Diameter of assembled conduit = {0} mm.", pcd.idac);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                double nsc1 = pcd.abcd_nspc;
                double uss1 = pcd.abcd_uss;
                double stess_fac1 = pcd.abcd_sf;
                double fss1 = uss1 * stess_fac1;
                double fcb1 = fss1 * nsc1;
                double x_area1 = pcd.abcd_csac;
                sw.WriteLine("For Cables a, b, c & d ");
                //sw.WriteLine("Number of Strands per cable = 19");
                //sw.WriteLine("Ultimate Strength per Strand = 183.7 kN");
                //sw.WriteLine("Stressing Factor = 0.765");
                //sw.WriteLine("Factored Strength per Strand = 183.7 x 0.765 = 140.5305 kN");
                //sw.WriteLine("Force per Cable = 140.5305 x 19 = 2670.0795 kN");
                //sw.WriteLine("Cross section area of each of four Cables = 1875.3 Sq.mm");
                sw.WriteLine("Number of Strands per cable = {0}", nsc1);
                sw.WriteLine("Ultimate Strength per Strand = {0} kN", uss1);
                sw.WriteLine("Stressing Factor = {0}", stess_fac1);
                sw.WriteLine();
                sw.WriteLine();
                double nsc2 = pcd.e_nspc;
                double uss2 = pcd.e_uss;
                double stess_fac2 = pcd.e_sf;
                double fss2 = uss2 * stess_fac2;
                double fcb2 = fss2 * nsc2;
                double x_area2 = pcd.e_csac;
                sw.WriteLine("For Cables e ");
                sw.WriteLine("Number of Strands per cable = {0}", nsc2);
                sw.WriteLine("Ultimate Strength per Strand = {0} kN", uss2);
                sw.WriteLine("Stressing Factor = {0}", stess_fac2);


                double D = d;
                if (D >= 500.0)
                    sw.WriteLine("D = {0:f2} mm.   >=  500.0", D);
                else
                    sw.WriteLine("D = {0:f2} mm.   <  500.0", D);

                //sw.WriteLine("D = 2400.0 mm.> 500.0");
                double cover1 = pcd.cover1;
                double cover2 = pcd.cover2;
                double D1 = D - 2 * cover2;
                double P = fcb1 * 4 + fcb2;
                double _As = x_area1 * 4 + x_area2;
                //foreach (var item in mid_sections.Get_Details())
                //{
                //    sw.WriteLine(item);
                //}
                //sw.WriteLine();
                sw.WriteLine("Cover2 = {0:f2} mm.", cover2);
                sw.WriteLine("Cover1 = {0:f2} mm. {1} Cover2", cover1, (cover1 < cover2 ? "<" : ">"));

                I = mid_sections.Iself;
                A = mid_sections.A;
                yt = mid_sections.Yt;
                Zt = mid_sections.Zt;
                Zb = mid_sections.Zb;
                double cover = 200;
                double e = (d - yt - cover);
                double Facts = SF;
                double Pk = NS * Facts * FS;
                double Pc = Pk;
                double ar_ech_strnd = Facts * Math.PI * dos * dos / 4.0;
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                string kStr = "             ";
                sw.WriteLine(kStr + "<- End ->  <---------   Mid  ------------>");
                sw.WriteLine("");
                sw.WriteLine(kStr + "1-1  2-2  3-3  4-4  5-5  6-6  7-7  8-8  9-9  8-8  7-7  6-6  5-5  4-4  3-3  2-2  1-1");
                sw.WriteLine("");
                sw.WriteLine(kStr + " |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    | ");
                //sw.WriteLine(kStr + "<|____|____|____|____|____|____|____|____|____|____|____|____|____|____|____|____|>");
                sw.WriteLine(kStr + " _________________________________________________________________________________");
                sw.WriteLine(kStr + " ^                                                                               ^");
                //sw.WriteLine("^                                                                                      ^");
                sw.WriteLine("");
                sw.WriteLine(kStr + " |<---------------Half Span------------->|");
                sw.WriteLine(kStr + " |<-------------------------------   Full Span   ------------------------------->|");
                sw.WriteLine();
                sw.WriteLine();

                

                #endregion


                #region STEP 1 : Bending Moments and Shear Forces AT VARIOUS SECTIONS ALONG THE SPAN
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Bending Moments and Shear Forces AT VARIOUS SECTIONS ALONG THE SPAN");
                sw.WriteLine("------------------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("-------------------------------------------------------------------------");
                sw.WriteLine("TABLE 1: for member End Forces for Mid Span (9-9), End Span (2-2) etc.  :");
                sw.WriteLine("-------------------------------------------------------------------------");
                sw.WriteLine();
                string kFormat = "{0,-12} {1,12:F3}  {2,12:E2}  {3,12:E2}  {4,12:E2}  {5,12:E2}  {6,12:E2}  {7,12:E2}";

                kFormat = "{0,-12} {1,12:F3}  {2,12:F3}  {3,12:F3}  {4,12:F3}  {5,12:F3}  {6,12:F3}  {7,12:F3}";
                double dd = 0;
                if (Is_Inner_Girder)
                {
                    #region INNER GIRDER
                    //sw.WriteLine("DESIGN FOR INNER GIRDER :");
                    //sw.WriteLine("-------------------------");


                    sw.WriteLine(kFormat,
                        "Sections",
                        "Distance",
                        "Dead Load",
                        "Live Load",
                        "Total",
                        "Dead Load",
                        "Live Load",
                        "Total");
                    sw.WriteLine(kFormat,
                        "",
                        "",
                        "Bending",
                        "Bending",
                        "Bending",
                        "Shear",
                        "Shear",
                        "Shear");
                    sw.WriteLine(kFormat,
                        "",
                        "",
                        "Moment",
                        "Moment",
                        "Moment",
                        "Force",
                        "Force",
                        "Force");
                    sw.WriteLine(kFormat,
                        "",
                        "(m)",
                        "(kN-m)",
                        "(kN-m)",
                        "(kN-m)",
                        "(kN)",
                        "(kN)",
                        "(kN)");
                    sw.WriteLine();
                    for (int i = 0; i < 9; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                dd = L1;
                                break;
                            case 1:
                                dd = L2;
                                break;
                            case 2:
                                dd = L3;
                                break;
                            case 3:
                                dd = L4;
                                break;
                            case 4:
                                dd = L5;
                                break;
                            case 5:
                                dd = L6;
                                break;
                            case 6:
                                dd = L7;
                                break;
                            case 7:
                                dd = L8;
                                break;
                            case 8:
                                dd = L9;
                                break;
                        }

                        sw.WriteLine(kFormat,
                            "(" + (i + 1).ToString() + "-" + (i + 1).ToString() + ")",
                            dd,
                            moment_forces.DL_INNER_GIRDER[i],
                            moment_forces.LL_INNER_GIRDER[i],
                            moment_forces.LL_INNER_GIRDER[i] + moment_forces.DL_INNER_GIRDER[i],
                            shear_forces.DL_INNER_GIRDER[i],
                            shear_forces.LL_INNER_GIRDER[i],
                            shear_forces.LL_INNER_GIRDER[i] + shear_forces.DL_INNER_GIRDER[i]);
                    }
                    sw.WriteLine("");
                    sw.WriteLine();
                    #endregion INNER GIRDER
                }
                else
                {
                    #region OUTER GIRDER

                    //sw.WriteLine("DESIGN FOR OUTER GIRDER :");
                    //sw.WriteLine("-------------------------");


                    sw.WriteLine(kFormat,
                        "Sections",
                        "Distance",
                        "Dead Load",
                        "Live Load",
                        "Total",
                        "Dead Load",
                        "Live Load",
                        "Total");
                    sw.WriteLine(kFormat,
                        "",
                        "",
                        "Bending",
                        "Bending",
                        "Bending",
                        "Shear",
                        "Shear",
                        "Shear");
                    sw.WriteLine(kFormat,
                        "",
                        "",
                        "Moment",
                        "Moment",
                        "Moment",
                        "Force",
                        "Force",
                        "Force");
                    sw.WriteLine(kFormat,
                        "",
                        "(m)",
                        "(kN-m)",
                        "(kN-m)",
                        "(kN-m)",
                        "(kN)",
                        "(kN)",
                        "(kN)");
                    sw.WriteLine();
                    sw.WriteLine();
                    for (int i = 0; i < 9; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                dd = L1;
                                break;
                            case 1:
                                dd = L2;
                                break;
                            case 2:
                                dd = L3;
                                break;
                            case 3:
                                dd = L4;
                                break;
                            case 4:
                                dd = L5;
                                break;
                            case 5:
                                dd = L6;
                                break;
                            case 6:
                                dd = L7;
                                break;
                            case 7:
                                dd = L8;
                                break;
                            case 8:
                                dd = L9;
                                break;
                        }

                        sw.WriteLine(kFormat,
                            "(" + (i + 1).ToString() + "-" + (i + 1).ToString() + ")",
                            dd,
                            moment_forces.DL_OUTER_GIRDER[i],
                            moment_forces.LL_OUTER_GIRDER[i],
                            moment_forces.LL_OUTER_GIRDER[i] + moment_forces.DL_OUTER_GIRDER[i],
                            shear_forces.DL_OUTER_GIRDER[i],
                            shear_forces.LL_OUTER_GIRDER[i],
                            shear_forces.LL_OUTER_GIRDER[i] + shear_forces.DL_OUTER_GIRDER[i]);
                    }
                    sw.WriteLine("");
                    #endregion OUTER GIRDER
                }
                #endregion

                #region STEP 2 : SECTION PROPERTIES OF MAIN GIRDER SECTION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------------");
                sw.WriteLine("STEP 2 : SECTION PROPERTIES OF MAIN GIRDER SECTION ");
                sw.WriteLine("------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("TABLE for Section properties :");
                sw.WriteLine("TABLE 2: for Simple Precast Section properties :");
                sw.WriteLine("------------------------------------------------");
                sw.WriteLine("For Mid Span (Section 1), End Span (Section 2) and Transverse / Cross Members (Section 3)");
                sw.WriteLine("-----------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("{0,-15} {1}", "Section", mid_sections.Table_HEAD);
                sw.WriteLine("{0,-15} {1}", "", mid_sections.Table_Unit);
                sw.WriteLine();
                sw.WriteLine("{0,-15} {1}", "(1-1 & 2-2)", end_sections.Table_Row);
                sw.WriteLine("{0,-15} {1}", "(3-3 to 9-9)", mid_sections.Table_Row);
                sw.WriteLine("{0,-15} {1}", "Cross Members", cross_sections.Table_Row);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("TABLE for Section properties :");
                sw.WriteLine("TABLE 3: for Composite Section (Precast PSC Girder+RCC Deck Slab) properties :");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("For Mid Span (Section 1), End Span (Section 2) and Transverse / Cross Members (Section 3)");
                sw.WriteLine("-----------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("{0,-15} {1}", "Section", mid_sections.Table_HEAD);
                sw.WriteLine("{0,-15} {1}", "", mid_sections.Table_Unit);
                sw.WriteLine();
                sw.WriteLine("{0,-15} {1}", "(1-1 & 2-2)", composit_end_sections.Table_Row);
                sw.WriteLine("{0,-15} {1}", "(3-3 to 9-9)", composit_mid_sections.Table_Row);
                sw.WriteLine();
                sw.WriteLine();
                #endregion STEP 2 : SECTION PROPERTIES OF MAIN GIRDER SECTION
                #region STEP 3 : Distance of Centre of Gravity (C.G.) of Cables
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Distance of Centre of Gravity (C.G.) of Cables ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine(@"          (Y-Y)                       ");
                sw.WriteLine(@"            |                          ");
                sw.WriteLine(@"            |                        ");
                sw.WriteLine(@"     |      W      |                                   ");
                sw.WriteLine(@" --  --------------              -----/-----  ");
                sw.WriteLine(@"     | TRANSVERSE  |                  |                   ");
                sw.WriteLine(@"     |     PSC     |                  |                    ");
                sw.WriteLine(@"     |   GIRDER    |                  |                    ");
                sw.WriteLine(@"     |             |                  | Yt    ");
                sw.WriteLine(@"     |      O e    |  ----            |                     ");
                sw.WriteLine(@"     |             |                  |                      ");
                sw.WriteLine(@"     |             |  450.0           |                      ");
                sw.WriteLine(@"     |      O d    |  ----            |                       ");
                sw.WriteLine(@" H   |             |             -----/----- Neutral Axix (X-X)  ");
                sw.WriteLine(@"     |             |  450.0           |                      ");
                sw.WriteLine(@"     |      O c    |  ----            |                       ");
                sw.WriteLine(@"     |             |                  |                      ");
                sw.WriteLine(@"     |             |  325.0           |                      ");
                sw.WriteLine(@"     |      O b    |  ----            |  Yb                   ");
                sw.WriteLine(@"     |             |                  |                      ");
                sw.WriteLine(@"     |             |  675.0           |                      ");
                sw.WriteLine(@"     |      O a    |  ----            |                      ");
                sw.WriteLine(@"     |             |  250.0           |                     ");
                sw.WriteLine(@" --   -------------   ----       -----/-----                 ");
                sw.WriteLine(@"     |      b      |");
                sw.WriteLine(@"            |                          ");
                sw.WriteLine(@"            |                        ");
                sw.WriteLine(@"          (Y-Y)                       ");
                sw.WriteLine(@"    ");
                sw.WriteLine(@"    CABLE POSITIONS AT");
                sw.WriteLine(@"    END SPAN SECTION (1-1)");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"          (Y-Y)                       ");
                sw.WriteLine(@"            |                        ");
                sw.WriteLine(@"            |                        ");
                sw.WriteLine(@"     |      W      |");
                sw.WriteLine(@" --  --------------   ----     -------/-------");
                sw.WriteLine(@"     |     PSC     |                  |       ");
                sw.WriteLine(@"     |    GIRDER   |                  |      ");
                sw.WriteLine(@"      \           /                   |     Yt");
                sw.WriteLine(@"       \         /                    |     ");
                sw.WriteLine(@"        \       /                     |                                 ");
                sw.WriteLine(@"         |     |            ----------/-------    Neutral Axix (X-X)      ");
                sw.WriteLine(@"         |     |                      |                                ");
                sw.WriteLine(@"         |     |                      |                                ");
                sw.WriteLine(@" H       |     |                      |                                ");
                sw.WriteLine(@"         |     |                      |                                     ");
                sw.WriteLine(@"         |     |                      |     Yb");
                sw.WriteLine(@"         |     |                      |                                ");
                sw.WriteLine(@"         |  Oe |      ----            |               ");
                sw.WriteLine(@"        /       \      509.2          |                 ");
                sw.WriteLine(@"       /    Od   \    ----            |               ");
                sw.WriteLine(@"      /           \    330.0          |                 ");
                sw.WriteLine(@"     |  O   O   O  |  ----            |               ");
                sw.WriteLine(@"     |  a   b   c  |   130.0   -------/--------                        ");
                sw.WriteLine(@" --   -------------   ----");
                sw.WriteLine(@"            |                        ");
                sw.WriteLine(@"            |                        ");
                sw.WriteLine(@"          (Y-Y)                       ");


                sw.WriteLine(@"     ");
                sw.WriteLine(@"    CABLE POSITIONS AT");
                sw.WriteLine(@"    MID SPAN SECTION (9-9)");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                //sw.WriteLine("<Hard code these data internally and print in the Report>");
                sw.WriteLine("");
                string sec_format = "{0,-10:f2} {1,-10:f2} {2,-10:f2} {3,-10:f2} {4,-10:f2} {5,-10:f2} {6,-10:f2} {7,-10:f2} {8,-10:f2} {9,-10:f2}";
                //string sec_format = "{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} ";
                sw.WriteLine("TABLE 4: for Composite Section (Precast PSC Girder+RCC Deck Slab) properties :");
                sw.WriteLine("-----------------------------------------------------------------------------------------");
                sw.WriteLine("For Mid Span (Section 1), End Span (Section 2) and Transverse / Cross Members (Section 3)");
                sw.WriteLine("-----------------------------------------------------------------------------------------");
              
                sw.WriteLine("------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "", "Section", "Section", "Section", "Section", "Section", "Section", "Section", "Section", "Section");
                sw.WriteLine(sec_format, "", " (1-1) ", " (2-2) ", " (3-3) ", " (4-4) ", " (5-5) ", " (6-6) ", " (7-7) ", " (8-8) ", " (9-9) ");
                //sw.WriteLine(sec_format, "", "  (mm) ", " (mm)  ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ");
                sw.WriteLine(sec_format.Replace("f2","f3"), "", 
                    L1.ToString("f3") + " m", L2.ToString("f3") + " m", L3.ToString("f3") + " m", L4.ToString("f3") + " m", 
                    L5.ToString("f3") + " m", L6.ToString("f3") + " m", L7.ToString("f3") + " m", L8.ToString("f3") + " m", 
                    L9.ToString("f3") + " m");
                sw.WriteLine("------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "", "  (mm) ", " (mm)  ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ");
                sw.WriteLine("------------------------------------------------------------------------------------------------------------");

                PSC_Force_Data Cable_e, Cable_d, Cable_c, Cable_b, Cable_a, Cable_total;

                Cable_e = 1;

                //2150.00 1663.63 1026.68 727.91 602.44 510.00 510.00 510.0 510.0

                //Chiranjit [2012 08 21]
                PSC_Force_Data coeff_e = 0;
                coeff_e.F1 = 1;
                coeff_e.F2 = (1663.63 / 2400.0);
                coeff_e.F3 = (1026.68 / 2400.0);
                coeff_e.F4 = (727.91 / 2400.0);
                coeff_e.F5 = (602.44 / 2400.0);
                coeff_e.F6 = (510.0 / 2400.0);
                coeff_e.F7 = (510.0 / 2400.0);
                coeff_e.F8 = (510.0 / 2400.0);
                coeff_e.F9 = (510.0 / 2400.0);



                //1700.00 1268.31 716.34 470.89 376.80 330.00 330.00 330.00 330.00
                PSC_Force_Data coeff_d = 0;
                coeff_d.F1 = (1700.0 / 2400.0);
                coeff_d.F2 = (1268.31 / 2400.0);
                coeff_d.F3 = (716.34 / 2400.0);
                coeff_d.F4 = (470.89 / 2400.0);
                coeff_d.F5 = (376.8 / 2400.0);
                coeff_d.F6 = (330.0 / 2400.0);
                coeff_d.F7 = (330.0 / 2400.0);
                coeff_d.F8 = (330.0 / 2400.0);
                coeff_d.F9 = (330.0 / 2400.0);

                //1250.000 873.594 405.867 130.000 130.000 130.000 130.000 130.000 130.000
                PSC_Force_Data coeff_c = 0;

                coeff_c.F1 = (1250.000 / 2400.0);
                coeff_c.F2 = (873.594 / 2400.0);
                coeff_c.F3 = (405.867 / 2400.0);
                coeff_c.F4 = (cover1 / 2400.0);
                coeff_c.F5 = (cover1 / 2400.0);
                coeff_c.F6 = (cover1 / 2400.0);
                coeff_c.F7 = (cover1 / 2400.0);
                coeff_c.F8 = (cover1 / 2400.0);
                coeff_c.F9 = (cover1 / 2400.0);

                //925.000 591.497 228.717 130.000 130.000 130.000 130.000 130.000 130.000
                PSC_Force_Data coeff_b = 0;

                coeff_b.F1 = (925.000 / 2400.0);
                coeff_b.F2 = (591.497 / 2400.0);
                coeff_b.F3 = (228.717 / 2400.0);
                coeff_b.F4 = (cover1 / 2400.0);
                coeff_b.F5 = (cover1 / 2400.0);
                coeff_b.F6 = (cover1 / 2400.0);
                coeff_b.F7 = (cover1 / 2400.0);
                coeff_b.F8 = (cover1 / 2400.0);
                coeff_b.F9 = (cover1 / 2400.0);


                //250.000 137.500 130.000 130.000 130.000 130.000 130.000 130.000 130.000
                PSC_Force_Data coeff_a = 0;

                coeff_a.F1 = cover2;
                coeff_a.F2 = (137.500 / 2400.0);
                coeff_a.F3 = cover1;
                coeff_a.F4 = cover1;
                coeff_a.F5 = cover1;
                coeff_a.F6 = cover1;
                coeff_a.F7 = cover1;
                coeff_a.F8 = cover1;
                coeff_a.F9 = cover1;




                Cable_e = coeff_e * D;
                Cable_e.F1 = D - cover2;


                Cable_d = coeff_d * D;
                Cable_c = coeff_c * D;

                Cable_b = coeff_b * D;

                Cable_a = coeff_a * 1.0;
                Cable_a.F2 *= D;



                frm_PSC_CG f_cg = new frm_PSC_CG(L, deff, Cable_a, Cable_b, Cable_c, Cable_d, Cable_e);

                if (f_cg.ShowDialog() != DialogResult.Cancel)
                {
                    Cable_e = f_cg.Cable_E;
                    Cable_d = f_cg.Cable_D;
                    Cable_c = f_cg.Cable_C;
                    Cable_b = f_cg.Cable_B;
                    Cable_a = f_cg.Cable_A;
                }

                Cable_total = Cable_e + Cable_d + Cable_c + Cable_b + Cable_a;
                Cable_total /= 5.0;


                //Chiranjit [2012 08 21]
                PSC_Force_Data theta_e = 0;
                //atan((2150.0-1664.4)/(1.602-0.0)/1000.0) = 16.863
                theta_e.F1 = Math.Atan(((Cable_e.F1 - Cable_e.F2) / (L2 - L1)) / 1000.0);
                theta_e.F2 = Math.Atan(((Cable_e.F2 - Cable_e.F3) / (L3 - L2)) / 1000.0);
                theta_e.F3 = Math.Atan(((Cable_e.F3 - Cable_e.F4) / (L4 - L3)) / 1000.0);
                theta_e.F4 = Math.Atan(((Cable_e.F4 - Cable_e.F5) / (L5 - L4)) / 1000.0);
                theta_e.F5 = Math.Atan(((Cable_e.F5 - Cable_e.F6) / (L6 - L5)) / 1000.0);
                theta_e *= (180.0 / Math.PI);




                //Angles for Cable ‘e’:
                //Theta_e1 = atan((2150.0-1664.4)/(1.602-0.0)/1000.0) = 16.863
                //Theta_e2 = atan((1664.4-1026.0)/(6.792-1.602)/1000.0) = 4.745
                //Theta_e3 = atan((1026.0-727.70)/(9.863-6.792)/1000.0) = 5.548
                //Theta_e4 = atan((727.70-602.44)/(11.803-9.863)/1000.0) = 3.694
                //Theta_e5 = atan((602.44-509.20)/(13.793-11.803)/1000.0) = 2.683
                //Theta_e6 = 0.000,                 Theta_e7 = 0.000,                 Theta_e8 = 0.000,                 Theta_e9 = 0.000

                //Chiranjit [2012 08 21]
                PSC_Force_Data theta_d = 0;
                //atan((2150.0-1664.4)/(1.602-0.0)/1000.0) = 16.863
                theta_d.F1 = Math.Atan(((Cable_d.F1 - Cable_d.F2) / (L2 - L1)) / 1000.0);
                theta_d.F2 = Math.Atan(((Cable_d.F2 - Cable_d.F3) / (L3 - L2)) / 1000.0);
                theta_d.F3 = Math.Atan(((Cable_d.F3 - Cable_d.F4) / (L4 - L3)) / 1000.0);
                theta_d.F4 = Math.Atan(((Cable_d.F4 - Cable_d.F5) / (L5 - L4)) / 1000.0);
                theta_d.F5 = Math.Atan(((Cable_d.F5 - Cable_d.F6) / (L6 - L5)) / 1000.0);
                theta_d *= (180.0 / Math.PI);



                //Chiranjit [2012 08 21]
                PSC_Force_Data theta_c = 0;
                //atan((2150.0-1664.4)/(1.602-0.0)/1000.0) = 16.863
                theta_c.F1 = Math.Atan(((Cable_c.F1 - Cable_c.F2) / (L2 - L1)) / 1000.0);
                theta_c.F2 = Math.Atan(((Cable_c.F2 - Cable_c.F3) / (L3 - L2)) / 1000.0);
                theta_c.F3 = Math.Atan(((Cable_c.F3 - Cable_c.F4) / (L4 - L3)) / 1000.0);
                theta_c.F4 = Math.Atan(((Cable_c.F4 - Cable_c.F5) / (L5 - L4)) / 1000.0);
                theta_c.F5 = Math.Atan(((Cable_c.F5 - Cable_c.F6) / (L6 - L5)) / 1000.0);
                theta_c *= (180.0 / Math.PI);

                //Chiranjit [2012 08 21]
                PSC_Force_Data theta_b = 0;
                //atan((2150.0-1664.4)/(1.602-0.0)/1000.0) = 16.863
                theta_b.F1 = Math.Atan(((Cable_b.F1 - Cable_b.F2) / (L2 - L1)) / 1000.0);
                theta_b.F2 = Math.Atan(((Cable_b.F2 - Cable_b.F3) / (L3 - L2)) / 1000.0);
                theta_b.F3 = Math.Atan(((Cable_b.F3 - Cable_b.F4) / (L4 - L3)) / 1000.0);
                theta_b.F4 = Math.Atan(((Cable_b.F4 - Cable_b.F5) / (L5 - L4)) / 1000.0);
                theta_b.F5 = Math.Atan(((Cable_b.F5 - Cable_b.F6) / (L6 - L5)) / 1000.0);
                theta_b *= (180.0 / Math.PI);

                //Chiranjit [2012 08 21]
                PSC_Force_Data theta_a = 0;
                //atan((2150.0-1664.4)/(1.602-0.0)/1000.0) = 16.863
                theta_a.F1 = Math.Atan(((Cable_a.F1 - Cable_a.F2) / (L2 - L1)) / 1000.0);
                theta_a.F2 = Math.Atan(((Cable_a.F2 - Cable_a.F3) / (L3 - L2)) / 1000.0);
                theta_a.F3 = Math.Atan(((Cable_a.F3 - Cable_a.F4) / (L4 - L3)) / 1000.0);
                theta_a.F4 = Math.Atan(((Cable_a.F4 - Cable_a.F5) / (L5 - L4)) / 1000.0);
                theta_a.F5 = Math.Atan(((Cable_a.F5 - Cable_a.F6) / (L6 - L5)) / 1000.0);
                theta_a *= (180.0 / Math.PI);




                PSC_Force_Data cumulative_theta = 0;
                cumulative_theta = theta_a + theta_b + theta_c + theta_d + theta_e;


                //sw.WriteLine(sec_format, "Cable", "D-250.0",
                //    coeff_e.F2.ToString("f3") + " D",
                //    coeff_e.F3.ToString("f3") + " D",
                //    coeff_e.F4.ToString("f3") + " D",
                //    coeff_e.F5.ToString("f3") + " D",
                //    coeff_e.F6.ToString("f3") + " D",
                //    coeff_e.F7.ToString("f3") + " D",
                //    coeff_e.F8.ToString("f3") + " D",
                //    coeff_e.F9.ToString("f3") + " D");
                //sw.WriteLine(sec_format.Replace(" ", "="), "Cable ‘e’",
                sw.WriteLine(sec_format, "Cable ‘e’",
                    Cable_e.F1, Cable_e.F2, Cable_e.F3, Cable_e.F4, Cable_e.F5, Cable_e.F6, Cable_e.F7, Cable_e.F8, Cable_e.F9);
                sw.WriteLine();

                //sw.WriteLine(sec_format, "Cable",
                //    coeff_d.F1.ToString("f3") + " D",
                //    coeff_d.F2.ToString("f3") + " D",
                //    coeff_d.F3.ToString("f3") + " D",
                //    coeff_d.F4.ToString("f3") + " D",
                //    coeff_d.F5.ToString("f3") + " D",
                //    coeff_d.F6.ToString("f3") + " D",
                //    coeff_d.F7.ToString("f3") + " D",
                //    coeff_d.F8.ToString("f3") + " D",
                //    coeff_d.F9.ToString("f3") + " D");
                //sw.WriteLine(sec_format.Replace(" ", "="), "Cable ‘d’",
                sw.WriteLine(sec_format, "Cable ‘d’",
                        Cable_d.F1, Cable_d.F2, Cable_d.F3, Cable_d.F4, Cable_d.F5, Cable_d.F6, Cable_d.F7, Cable_d.F8, Cable_d.F9);
                sw.WriteLine();

                //sw.WriteLine(sec_format, "Cable",
                //   coeff_c.F1.ToString("f3") + " D",
                //   coeff_c.F2.ToString("f3") + " D",
                //   coeff_c.F3.ToString("f3") + " D",
                //   "Cover1", "Cover1", "Cover1", "Cover1", "Cover1", "Cover1");
                //sw.WriteLine(sec_format.Replace(" ", "="), "Cable ‘c’",
                sw.WriteLine(sec_format, "Cable ‘c’",
                    Cable_c.F1, Cable_c.F2, Cable_c.F3, Cable_c.F4, Cable_c.F5, Cable_c.F6, Cable_c.F7, Cable_c.F8, Cable_c.F9);
                sw.WriteLine();
                sw.WriteLine();

                //sw.WriteLine(sec_format, "Cable",
                //   coeff_b.F1.ToString("f3") + " D",
                //   coeff_b.F2.ToString("f3") + " D",
                //   coeff_b.F3.ToString("f3") + " D",
                //    "Cover1", "Cover1", "Cover1", "Cover1", "Cover1", "Cover1");
                //sw.WriteLine(sec_format.Replace(" ", "="), "Cable ‘b’",
                sw.WriteLine(sec_format, "Cable ‘b’",
                    Cable_b.F1, Cable_b.F2, Cable_b.F3, Cable_b.F4, Cable_b.F5, Cable_b.F6, Cable_b.F7, Cable_b.F8, Cable_b.F9);
                sw.WriteLine();
                sw.WriteLine();

                //sw.WriteLine(sec_format, "Cable", "Cover2", coeff_a.F2.ToString("f3") + " D", "Cover1", "Cover1", "Cover1", "Cover1", "Cover1", "Cover1", "Cover1");
                //sw.WriteLine(sec_format.Replace(" ", "="), "Cable ‘a’",
                sw.WriteLine(sec_format, "Cable ‘a’",
                    Cable_a.F1, Cable_a.F2, Cable_a.F3, Cable_a.F4, Cable_a.F5, Cable_a.F6, Cable_a.F7, Cable_a.F8, Cable_a.F9);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------------------------------------------------------");
                sw.WriteLine("                             C.G Or  Average  Distance From Bottom");
                sw.WriteLine("------------------------------------------------------------------------------------------------------------");
                //sw.WriteLine(sec_format, "CG",
                sw.WriteLine(sec_format, "CG",
                  Cable_total.F1, Cable_total.F2, Cable_total.F3, Cable_total.F4, Cable_total.F5, Cable_total.F6, Cable_total.F7, Cable_total.F8, Cable_total.F9);
                sw.WriteLine("------------------------------------------------------------------------------------------------------------");

                sw.WriteLine("------------------------------------------------");
                sw.WriteLine("Calculations Details for Angles in Cable Profile");
                sw.WriteLine("------------------------------------------------");
                sw.WriteLine("");

                //theta_e.F1 = Math.Atan(((Cable_e.F1 - Cable_e.F2) / (L2 - L1)) / 1000.0);
                //theta_e.F2 = Math.Atan(((Cable_e.F2 - Cable_e.F3) / (L3 - L2)) / 1000.0);
                //theta_e.F3 = Math.Atan(((Cable_e.F3 - Cable_e.F4) / (L4 - L3)) / 1000.0);
                //theta_e.F4 = Math.Atan(((Cable_e.F4 - Cable_e.F5) / (L5 - L4)) / 1000.0);
                //theta_e.F5 = Math.Atan(((Cable_e.F5 - Cable_e.F6) / (L6 - L5)) / 1000.0);
                //theta_e *= (180.0 / Math.PI);

                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Angles for Cable ‘e’:");
                sw.WriteLine("Theta_e1 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_e.F1, Cable_e.F2, L2, L1, theta_e.F1);
                sw.WriteLine("Theta_e2 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_e.F2, Cable_e.F3, L3, L2, theta_e.F2);
                sw.WriteLine("Theta_e3 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_e.F3, Cable_e.F4, L4, L3, theta_e.F3);
                sw.WriteLine("Theta_e4 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_e.F4, Cable_e.F5, L5, L4, theta_e.F4);
                sw.WriteLine("Theta_e5 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_e.F5, Cable_e.F6, L6, L5, theta_e.F5);
                sw.WriteLine("Theta_e6 = 0.000,                 Theta_e7 = 0.000,                 Theta_e8 = 0.000,                 Theta_e9 = 0.000");

                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Angles for Cable ‘d’:");
                sw.WriteLine("Theta_d1 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_d.F1, Cable_d.F2, L2, L1, theta_d.F1);
                sw.WriteLine("Theta_d2 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_d.F2, Cable_d.F3, L3, L2, theta_d.F2);
                sw.WriteLine("Theta_d3 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_d.F3, Cable_d.F4, L4, L3, theta_d.F3);
                sw.WriteLine("Theta_d4 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_d.F4, Cable_d.F5, L5, L4, theta_d.F4);
                sw.WriteLine("Theta_d5 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_d.F5, Cable_d.F6, L6, L5, theta_d.F5);
                sw.WriteLine("Theta_d6 = 0.000,                 Theta_d7 = 0.000,                 Theta_d8 = 0.000,                 Theta_d9 = 0.000");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Angles for Cable ‘c’:");
                sw.WriteLine("Theta_c1 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F1, Cable_c.F2, L2, L1, theta_c.F1);
                sw.WriteLine("Theta_c2 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F2, Cable_c.F3, L3, L2, theta_c.F2);
                sw.WriteLine("Theta_c3 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F3, Cable_c.F4, L4, L3, theta_c.F3);
                sw.WriteLine("Theta_c4 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F4, Cable_c.F5, L5, L4, theta_c.F4);
                sw.WriteLine("Theta_c5 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F5, Cable_c.F6, L6, L5, theta_c.F5);
                sw.WriteLine("Theta_c6 = 0.000,                 Theta_c7 = 0.000,                 Theta_c8 = 0.000,                 Theta_c9 = 0.000");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Angles for Cable ‘b’:");
                sw.WriteLine("Theta_b1 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F1, Cable_c.F2, L2, L1, theta_c.F1);
                sw.WriteLine("Theta_b2 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F2, Cable_c.F3, L3, L2, theta_c.F2);
                sw.WriteLine("Theta_b3 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F3, Cable_c.F4, L4, L3, theta_c.F3);
                sw.WriteLine("Theta_b4 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F4, Cable_c.F5, L5, L4, theta_c.F4);
                sw.WriteLine("Theta_b5 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_c.F5, Cable_c.F6, L6, L5, theta_c.F5);
                sw.WriteLine("Theta_b6 = 0.000,                 Theta_b7 = 0.000,                 Theta_b8 = 0.000,                 Theta_b9 = 0.000");

                sw.WriteLine();
                sw.WriteLine();
                
                sw.WriteLine("Angles for Cable ‘a’:");
                sw.WriteLine("Theta_a1 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_a.F1, Cable_a.F2, L2, L1, theta_a.F1);
                sw.WriteLine("Theta_a2 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_a.F2, Cable_a.F3, L3, L2, theta_a.F2);
                sw.WriteLine("Theta_a3 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_a.F3, Cable_a.F4, L4, L3, theta_a.F3);
                sw.WriteLine("Theta_a4 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_a.F4, Cable_a.F5, L5, L4, theta_a.F4);
                sw.WriteLine("Theta_b5 = atan(({0:f3}-{1:f3})/({2:f3}-{3:f3})/1000.0) = {4:f3} deg",
                    Cable_a.F5, Cable_a.F6, L6, L5, theta_a.F5);
                sw.WriteLine("Theta_a6 = 0.000,                 Theta_a7 = 0.000,                 Theta_a8 = 0.000,                 Theta_a9 = 0.000");
                sw.WriteLine();
                sw.WriteLine("Cumulative Theta  :");
                sw.WriteLine();
                sw.WriteLine("Theta1 = Theta_e1 + Theta_d1 + Theta_c1 + Theta_b1 + Theta_a1 = {0:f3} = {0:f0}", cumulative_theta.F1);
                sw.WriteLine("Theta2 = Theta_e2 + Theta_d2 + Theta_c2 + Theta_b2 + Theta_a2 = {0:f3} = {0:f0}", cumulative_theta.F2);
                sw.WriteLine("Theta3 = Theta_e3 + Theta_d3 + Theta_c3 + Theta_b3 + Theta_a3 = {0:f3} = {0:f0}", cumulative_theta.F3);
                sw.WriteLine("Theta4 = Theta_e4 + Theta_d4 + Theta_c4 + Theta_b4 + Theta_a4 = {0:f3}  =  {0:f0}", cumulative_theta.F4);
                sw.WriteLine("Theta5 = Theta_e5 + Theta_d5 + Theta_c5 + Theta_b5 + Theta_a5 = {0:f3}  = {0:f0}", cumulative_theta.F5);
                sw.WriteLine("Theta6 = 0.0                Theta7 = 0.0                Theta8 = 0.0                Theta9 = 0.0");

                for (int i = 0; i < cumulative_theta.Count; i++)
                {
                    cumulative_theta[i] = int.Parse(cumulative_theta[i].ToString("f0"));
                }
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");


                //sw.WriteLine("Theta_e2 = atan((1664.4-1026.0)/(6.792-1.602)/1000.0) = 4.745");
                //sw.WriteLine("Theta_e3 = atan((1026.0-727.70)/(9.863-6.792)/1000.0) = 5.548");
                //sw.WriteLine("Theta_e4 = atan((727.70-602.44)/(11.803-9.863)/1000.0) = 3.694");
                //sw.WriteLine("Theta_e5 = atan((602.44-509.20)/(13.793-11.803)/1000.0) = 2.683");
                //sw.WriteLine("Theta_e6 = 0.000,                 Theta_e7 = 0.000,                 Theta_e8 = 0.000,                 Theta_e9 = 0.000");
                sw.WriteLine();
                sw.WriteLine("");
                sw.WriteLine("");
                #endregion STEP 3 : Distance of Centre of Gravity (C.G.) of Cables

                #region STEP 4 : PERMISSIBLE STRESSES
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : PERMISSIBLE STRESSES");
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

                //Chiranjit [2012 08 07]
                //Ec = 5700.0 * Math.Sqrt(fck);
                Ec = (int)(5000.0 * Math.Sqrt(fck) / 1000.0);

                Ec = double.Parse(Ec.ToString("0"));
                //sw.WriteLine("Ec = 5000 * √fck = 5000 * √{0} = {1:f0} N/sq.mm = {2} kN/sq.m", fck, Ec, (int)(Ec));
                sw.WriteLine("Ec = 5000 * √fck = (5000 * √{0})/1000 = {1:f0} kN/sq.m", fck, Ec );
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
                //foreach (var item in mid_sections.Get_Details()) //Chiranjit [2013 06 30]
                foreach (var item in mid_sections.Get_Results())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();

                I = mid_sections.Iself;

                yt = mid_sections.Yt;

                //Change Inner & Outer Girder
                double Mg = moment_forces.DL_OUTER_GIRDER.F9;
                double Mq = moment_forces.LL_OUTER_GIRDER.F9;

                if (Is_Inner_Girder)
                {
                    Mg = moment_forces.DL_INNER_GIRDER.F9;
                    Mq = moment_forces.LL_INNER_GIRDER.F9;
                }
                else
                {
                    Mg = moment_forces.DL_OUTER_GIRDER.F9;
                    Mq = moment_forces.LL_OUTER_GIRDER.F9;

                }


                double Md = Mg + Mq;
                sw.WriteLine("Mg = {0} kN-m (Dead Load BM section 9-9)", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m (Live Load BM section 9-9)", Mq);
                sw.WriteLine();
                sw.WriteLine("Md = Mg + Mq = {0} + {1} = {2} kN-m", Mg, Mq, Md);
                sw.WriteLine();

                finf = (ftw / eta) + (Md * 10e5) / (eta * Zb);
                finf = double.Parse(finf.ToString("0.00"));

                //double p = (A * finf * Zb) / ((Zb) + (A * e));
                //int Nc = (int)(p / Pk);
                double p = P;
                int Nc = 5;

                sw.WriteLine("finf = (ftw / η) + Md / (η * Zb)");
                sw.WriteLine("     = ({0} / {1}) + {2}*10^6 / ({1} * {3:E2})", ftw, eta, Md, Zb);
                sw.WriteLine("     = {0:E2} N/sq.mm", finf);
                sw.WriteLine();

                double _Zb = (Mq * 10E5 + (1 - eta) * Mg * 10E5) / fbr;
                sw.WriteLine("Required   Zb = [Mq + (1 - η) * Mg] / fbr");
                sw.WriteLine("              = [{0}*10^6  + (1 - {1}) * {2}*10^6 ] / {3}", Mq, eta, Mg, fbr);

                if (_Zb < Zb)
                {
                    sw.WriteLine("              = {0:E2} Cu.mm < {1:E2} Cu.mm,   So, the girder is adequate.", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine("");
                }
                else
                {
                    sw.WriteLine("              = {0:E2} Cu.mm > {1:E2} Cu.mm,   So, the girder is inadequate.", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine();
                }
                //foreach (var item in end_sections.Get_Details()) //Chiranjit [2013 06 30]
                foreach (var item in end_sections.Get_Results())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                I = end_sections.Iself;
                A = end_sections.A;
                yt = end_sections.Yt;
                Zt = end_sections.Zt;
                Zb = end_sections.Zb;


                if (Is_Outer_Girder)
                {
                    Mg = moment_forces.DL_OUTER_GIRDER.F2;
                    Mq = moment_forces.LL_OUTER_GIRDER.F2;
                }
                else
                {
                    Mg = moment_forces.DL_INNER_GIRDER.F2;
                    Mq = moment_forces.LL_INNER_GIRDER.F2;
                }

                Md = Mg + Mq;
                sw.WriteLine("Mg = {0} kN-m  (Dead Load BM section 2-2)", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m  (Live Load BM section 2-2)", Mq);
                sw.WriteLine();
                sw.WriteLine("Md = Mg + Mq = {0} + {1} = {2} kN-m", Mg, Mq, Md);
                sw.WriteLine();

                finf = (ftw / eta) + (Md * 10E5) / (eta * Zb);
                finf = double.Parse(finf.ToString("0.00"));
                sw.WriteLine("finf = (ftw / η) + Md / (η * Zb)");
                sw.WriteLine("     = ({0} / {1}) + {2}*10^6 / ({1} * {3:E2})", ftw, eta, Md, Zb);
                sw.WriteLine("     = {0:E2} N/sq.mm", finf);
                sw.WriteLine();

                _Zb = (Mq * 10E5 + (1 - eta) * Mg * 10E5) / fbr;
                sw.WriteLine("Required   Zb = [Mq + (1 - η) * Mg] / fbr");
                sw.WriteLine("              = [{0}*10^6  + (1 - {1}) * {2}*10^6 ] / {3}", Mq, eta, Mg, fbr);

                if (_Zb < Zb)
                {
                    sw.WriteLine("              = {0:E2} Cu.mm < {1:E2} Cu.mm,   So, the girder section is adequate.", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine();
                }
                else
                {
                    sw.WriteLine("              = {0:E2} Cu.mm > {1:E2} Cu.mm,   So, the girder section is inadequate.", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine("");
                }


                #endregion

                #region STEP 5 : PRESTRESSING FORCE
                sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("STEP 5 : PRESTRESSING FORCE ");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : PRESTRESSING FORCE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #region TABLE 1 : Specifications  Reference Table

                sw.WriteLine(("------------------------------------------"));
                sw.WriteLine(("TABLE : Specifications  Reference Table "));
                sw.WriteLine(("------------------------------------------"));
                sw.WriteLine((""));
                sw.WriteLine(("-------------------------------------------------------------------------------"));
                sw.WriteLine(("Strand Type             Unit      13mm (.5\")                15mm (.6\")   "));
                sw.WriteLine(("                              BS:5896   Grade 270        BS:5896    Grade 270 "));
                sw.WriteLine(("-------------------------------------------------------------------------------"));
                sw.WriteLine(("Nominal Dia.             mm     12.9      12.7             15.7        15.2 "));
                sw.WriteLine(("Nominal Area          sq.mm      100      98.7              150         140 "));
                sw.WriteLine(("Nominal Mass           Kg/m     0.785     0.775            1.18         1.1 "));
                sw.WriteLine(("Yield Strength          MPa     1580      1670             1500        1670 "));
                sw.WriteLine(("Tensile Strength        MPa     1860      1860             1770        1860 "));
                sw.WriteLine(("Minimum Breaking Load   kN      186.0     183.7           265.0       260.7 "));
                sw.WriteLine(("-------------------------------------------------------------------------------"));
                #endregion TABLE 1


                sw.WriteLine();
                sw.WriteLine("Prestressing Details:");
                sw.WriteLine("---------------------");
                sw.WriteLine();
                sw.WriteLine("Ultimate Strength per Strand = {0} kN", uss1);
                sw.WriteLine("Stressing Factor = {0}", stess_fac1);
                Nc = 5;
                sw.WriteLine();
                sw.WriteLine("Number of Cables used = 4 + 1 = 5.");
                //sw.WriteLine("Type of Cable used for cables a, b, c, d  =  19 T 13 with duct diameter 90 mm"); //Chiranjit [2013 07 01]
                //sw.WriteLine("Type of Cable used for cables e           =  8  T 13");//Chiranjit [2013 07 01]
                sw.WriteLine("Type of Cable used for cables a, b, c, d  =  {0} with duct diameter 90 mm", Cable_Type_abcd);//Chiranjit [2013 07 01]
                sw.WriteLine("Type of Cable used for cables e           =  {0}", Cable_Type_e);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For Cables a, b, c & d  ");
                sw.WriteLine();
                sw.WriteLine("Factored Strength per Strand = {0} x {1} = {2:f3} kN", uss1, stess_fac1, fss1);
                sw.WriteLine("Force per Cable = {0:f3} x {1} = {2:f3} kN", fss1, nsc1, fcb1);
                sw.WriteLine("Cross section area of Cable ‘a,b,c,d’ = {0:f3} Sq.mm", x_area1);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("For Cables e ");
                sw.WriteLine();
                sw.WriteLine("Factored Strength per Strand = {0} x {1} = {2:f3} kN", uss2, stess_fac2, fss2);
                sw.WriteLine("Force per Cable = {0:f3} x {1} = {2:f3} kN", fss2, nsc2, fcb2);
                sw.WriteLine("Cross section area of Cable ‘e’ = {0:f3} Sq.mm", x_area2);
                sw.WriteLine();
                sw.WriteLine();
                if (D1 >= 500)
                    sw.WriteLine("D1 = D - 2 x Cover2 = {0:f2} - 2 x {1:f2} = {2:f2} mm. >= 500.0 mm.", D, cover2, D1);
                else
                    sw.WriteLine("D1 = D - 2 x Cover2 = {0:f2} - 2 x {1:f2} = {2:f2} mm. < 500.0 mm.", D, cover2, D1);
                sw.WriteLine("Cover2 = {0:f2} mm.", cover2);
                sw.WriteLine("Cover1 = {0:f2} mm. {1} Cover2", cover1, (cover1 < cover2 ? "<" : ">"));
                sw.WriteLine("");


                sw.WriteLine("Total Prestressing force = P = {0:f3} x 4 + {1:f3} = {2:f3} kN", fcb1, fcb2, P);
                //sw.WriteLine("                                                                                                                                                = 1180456.2 kg");
                sw.WriteLine();
                sw.WriteLine("Total Cross Section area of five cables = As = {0:f3} x 4 + {1:f3} = {2:f3} sq.mm.", x_area1, x_area2, _As);
                sw.WriteLine("");

                //sw.WriteLine("Allowing for two rows of Cables, required Cover = 200 mm");
                //sw.WriteLine();

                //sw.WriteLine("Maximum Possible eccentricity = e = (d - yt - cover) ");
                //sw.WriteLine("                              = ({0} - {1} - {2})", d, yt, cover);
                //sw.WriteLine("                              = {0} mm", e);
                //sw.WriteLine();
                //sw.WriteLine("Prestressing Force is obtained as");
                //sw.WriteLine();


                //sw.WriteLine("p = (A * finf * Zb) / (Zb  + A * e)");
                //sw.WriteLine("  = ({0:E3} * {1} * {2:E2}) / ({2:E2} + {0:E3} * {3})", A, finf, Zb, e);
                //sw.WriteLine("  = {0:E2} N", p);
                //p = p / 1000.0;
                //sw.WriteLine("  = {0:E2} kN", p);
                //sw.WriteLine();


                //Pk = double.Parse(Pk.ToString("0"));
                //sw.WriteLine("Force in each Cable = Pc = Ns * Facts * Fs");
                //sw.WriteLine("                         = {0} * {1} * {2}", NS, Facts, FS);
                //sw.WriteLine("                         = {0} kN", Pk);
                //sw.WriteLine();


                //sw.WriteLine("Required Number of Cables = Nc = {0:E2} / {1}", p, Pk);
                //sw.WriteLine("                          = {0:f4} ", (p / Pk));

                //Nc = (int)Nc;

                //Nc += 1;
                //sw.WriteLine("                          = {0} ", Nc);
                //sw.WriteLine();


                //ar_ech_strnd = double.Parse(ar_ech_strnd.ToString("0"));
                //sw.WriteLine("Area of each Strand = (Fact * π * dos*dos)/4.0");
                //sw.WriteLine("                    = ({0} * π * {1}*{1}) / 4.0", Facts, dos);
                //sw.WriteLine("                    = {0:f0} sq.mm", ar_ech_strnd);
                //sw.WriteLine();
                //sw.WriteLine("A cable contains NS = {0} strands,", NS);

                double total_area1 = NS * ar_ech_strnd;
                double Ac = total_area1;
                //total_area1 = double.Parse(total_area1.ToString("0"));
                //sw.WriteLine("Total Area = {0} * {1}", NS, ar_ech_strnd);
                //sw.WriteLine("           = {0} sq.mm", total_area1);
                //sw.WriteLine();

                //double total_area2 = Nc * total_area1;
                //sw.WriteLine("For {0} Cables, Total Area = {0} * {1} = {2} sq.mm        Marked as (I) in the Drawing", Nc, total_area1, total_area2);
                ////(I) = Total 5 nos. Prestressing Cables.
                //_I1 = string.Format("Total {0:f0} nos. Prestressing Cables.", Nc);
                //// 7 nos. Strands per Cable, Are of each = 141 sq.mm
                //_I2 = string.Format("{0:f0} nos. Strands per Cable, Are of each = {1} sq.mm", NS, ar_ech_strnd);



                //sw.WriteLine();
                //sw.WriteLine("The arrangement of Cables are shown in the Drawing.");


                //foreach (var item in end_sections.Get_Details())
                //{
                //    sw.WriteLine(item);
                //}
                //sw.WriteLine();
                //sw.WriteLine();
                //I = end_sections.Iself;
                //A = end_sections.A;
                //yt = end_sections.Yt;
                //Zt = end_sections.Zt;
                //Zb = end_sections.Zb;

                //sw.WriteLine();
                //sw.WriteLine("Allowing for two rows of Cables, required Cover = 200 mm");
                //sw.WriteLine();

                //e = (d - yt - cover);
                //sw.WriteLine("Maximum Possible eccentricity = e = (d - yt - cover) ");
                //sw.WriteLine("                              = ({0} - {1} - {2})", d, yt, cover);
                //sw.WriteLine("                              = {0} mm", e);
                //sw.WriteLine();
                //sw.WriteLine("Prestressing Force is obtained as");
                //sw.WriteLine();


                //p = (A * finf * Zb) / ((Zb) + (A * e));
                //sw.WriteLine("p = (A * finf * Zb) / (Zb  + A * e)");
                //sw.WriteLine("  = ({0:E2} * {1} * {2:E2}) / ({2:E2} + {0:E2} * {3})", A, finf, Zb, e);
                //sw.WriteLine("  = {0:E2} N", p);
                //p = p / 1000.0;
                //sw.WriteLine("  = {0:E2} kN", p);
                //sw.WriteLine();

                //Facts = SF;

                //Pk = NS * Facts * FS;
                //Pc = Pk;
                //Pk = double.Parse(Pk.ToString("0"));
                //sw.WriteLine("Force in each Cable = Pc = Ns * Facts * Fs");
                //sw.WriteLine("                         = {0} * {1} * {2}", NS, Facts, FS);
                //sw.WriteLine("                         = {0} kN", Pk);
                //sw.WriteLine();


                //Nc = (int)(p / Pk);



                #endregion

                #region STEP 6 : LOSS OF PRESTRESSING FORCE DUE TO SHRINKAGE IN CONCRETE
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("STEP 6 : LOSS OF PRESTRESSING FORCE DUE TO SHRINKAGE IN CONCRETE");
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine();
                foreach (var item in mid_sections.Get_Details())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                I = mid_sections.Iself;
                A = mid_sections.A;
                yt = mid_sections.Yt;
                Zt = mid_sections.Zt;
                Zb = mid_sections.Zb;
                sw.WriteLine("Elastic Modulus of Steel = Es = {0} KN/Sq.mm", Es);
                sw.WriteLine("Age of Concrete at Transfer = Atr = {0} days", Atr);
                sw.WriteLine("");
                sw.WriteLine("");
                Pc = P;
                sw.WriteLine("From previous calculations, Initial Force in each Cable = Pc = {0:f3} kN", Pc);
                sw.WriteLine("");
                sw.WriteLine("Cross Sectional Area of all strands/wires in a cable = Ac = {0:f3} sq.mm", Ac);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("The Girder is Post Tensioned, ");
                sw.WriteLine("Total Residual Shrinkage Strain   = Ss ");

                double Ss = Math.Log10(Atr + 2);

                Ss = 200E-6 / Ss;
                sw.WriteLine("                                  = 200*10^-6/[log(10) (Atr+2)]");
                sw.WriteLine("                                  = 200*10^-6/[log(10) ({0}+2)]", Atr);
                sw.WriteLine("                                  = 200*10^-6/[log(10) {0}]", (Atr + 2));
                sw.WriteLine("                                  = {0:E2} Units", Ss);
                sw.WriteLine("");

                double Lss = Ss * Es;
                sw.WriteLine("So, Loss of Stress   = Lss ");
                sw.WriteLine("                     = Ss * Es ");
                sw.WriteLine("                     = {0:E2} * {1}", Ss, Es);
                sw.WriteLine("                     = {0:E2} kN/Sq.mm", Lss);
                sw.WriteLine("");
                sw.WriteLine("");
                double Ls = Lss * Ac;
                sw.WriteLine("Loss in Pre Stressing Force due to Shrinkage =  Ls");
                sw.WriteLine("                                             = Lss * Ac ");
                sw.WriteLine("                                             = {0:E2} * {1:f2} kN", Lss, Ac);
                sw.WriteLine("                                             = {0:E2} kN.", Ls);
                sw.WriteLine("");



                //foreach (var item in end_sections.Get_Details())
                //{
                //    sw.WriteLine(item);
                //}
                //sw.WriteLine();
                //I = end_sections.Iself;
                //A = end_sections.A;
                //yt = end_sections.Yt;
                //Zt = end_sections.Zt;
                //Zb = end_sections.Zb;
                //sw.WriteLine("Elastic Modulus of Steel = Es = {0} KN/Sq.mm", Es);
                //sw.WriteLine("Age of Concrete at Transfer = Atr = {0} days", Atr);
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("From previous calculations, Initial Force in each Cable = Pc = {0:f3} kN", Pc);
                //sw.WriteLine("");
                //sw.WriteLine("Cross Sectional Area of all strands/wires in a cable = Ac = {0:f3} sq.mm", Ac);
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("The Girder is Post Tensioned, ");
                //sw.WriteLine("Total Residual Shrinkage Strain   = Ss ");

                // Ss = Math.Log10(Atr + 2);

                //Ss = 200E-6 / Ss;
                //sw.WriteLine("                                  = 200*10^-6/[log(10) (Atr+2)]");
                //sw.WriteLine("                                  = 200*10^-6/[log(10) ({0}+2)]", Atr);
                //sw.WriteLine("                                  = 200*10^-6/[log(10) {0}]", (Atr + 2));
                //sw.WriteLine("                                  = {0:E2} Units", Ss);
                //sw.WriteLine("");

                // Lss = Ss * Es;
                //sw.WriteLine("So, Loss of Stress   = Lss ");
                //sw.WriteLine("                     = Ss * Es ");
                //sw.WriteLine("                     = {0:E2} * {1}", Ss, Es);
                //sw.WriteLine("                     = {0:E2} kN/Sq.mm", Lss);
                //sw.WriteLine("");
                //sw.WriteLine("");
                // Ls = Lss * Ac;
                //sw.WriteLine("Loss in Pre Stressing Force due to Shrinkage =  Ls");
                //sw.WriteLine("                                             = Lss * Ac ");
                //sw.WriteLine("                                             = {0:E2} * {1:f2} kN", Lss, Ac);
                //sw.WriteLine("                                             = {0:E2} kN.", Ls);
                //sw.WriteLine("");
                #endregion STEP 6


                #region STEP 7 : LOSS OF PRESTRESSING FORCE DUE TO CREEP IN CONCRETE
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : LOSS OF PRESTRESSING FORCE DUE TO CREEP IN CONCRETE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");

                foreach (var item in mid_sections.Get_Details())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                I = mid_sections.Iself;
                A = mid_sections.A;
                yt = mid_sections.Yt;
                Zt = mid_sections.Zt;
                Zb = mid_sections.Zb;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Elastic Modulus of Steel = Es = {0} kN/Sq.mm", Es);
                sw.WriteLine("Elastic Modulus of Concrete = Ec = {0} kN/Sq.mm", Ec);
                //sw.WriteLine("Modular Ratio = m = Es/ Ec = {0}/{1} = {2}", Es, Ec, m);
                sw.WriteLine("Modular Ratio = m = {0} ", m);
                sw.WriteLine("Creep Coefficient = phi = {0}", phi);
                sw.WriteLine("Ultimate Creep Strain = ecc = {0:E2} mm/mm", ecc);
                sw.WriteLine("");
                sw.WriteLine("");
                Pc = pcd.abcd_fc;
                Ac = pcd.abcd_csac;

                sw.WriteLine("From previous calculations, Initial Force in each Cable = Pc = {0:f3} kN", Pc);
                sw.WriteLine("Cross Sectional Area of all strands/wires in a cable = Ac = {0:f3} sq.mm", Ac);
                sw.WriteLine("");
                //sw.WriteLine("(Izz for Section 9-9 & 2-2) = {0:E2}  sq.sq.mm", I);
                //sw.WriteLine("(Izz for Section 9-9) = {0:E2}  sq.sq.mm", I); // Chiranjit [2013 06 30]
                sw.WriteLine("(Ixx for Section 9-9) = {0:E2}  sq.sq.mm", I);
                //sw.WriteLine("(A for Section 9-9 & 2-2) = {0:E2}  sq.mm", A);
                sw.WriteLine("");
                sw.WriteLine("(A for Section 9-9) = {0:E2}  sq.mm", A);
                sw.WriteLine("");
                sw.WriteLine("Modular Ratio = m = {0}", m);
                //(e = Yb-CG, for Section 9-9 & 2-2)
                e = mid_sections.Yb - Cable_total.F9;
                double e_mid = e;
                sw.WriteLine("Eccentricity = e = Yb - CG , for Section 9-9)");
                sw.WriteLine("                 = {0:f2} - {1:f2} = {2:f2} mm.", mid_sections.Yb, Cable_total.F9, e);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Stress in Concrete at the level of Steel  = fc");
                sw.WriteLine("");
                double fc = (Pc / (A)) + ((Pc * e * e) / (I));
                sw.WriteLine("                      fc  = [Pc/A] + [(Pc*e*e)/I]");
                sw.WriteLine("                          = [{0:f3}/({1:E2})]+[({0:f3}*{2}^2)/({3:E2})]",
                    Pc, A, e, I);
                sw.WriteLine("                          = {0:E2} kN/Sq.mm", fc);
                //sw.WriteLine("                          = 0.005659 kN/Sq.mm");
                sw.WriteLine("");
                double Lcs = phi * fc * m;
                sw.WriteLine("Loss of Prestress    = Lcs = phi*fc*m ");
                sw.WriteLine("");
                sw.WriteLine("                     = {0} * {1:E2} * {2} ", phi, fc, m);
                sw.WriteLine("                     = {0:E2} kN/Sq.mm", Lcs);
                sw.WriteLine("");
                double Lc = Lcs * Ac;
                sw.WriteLine("Loss of Force due to Creep  = Lc = Lcs * Ac");
                sw.WriteLine("");
                sw.WriteLine("                            = {0:E2} * {1:f3} kN", Lcs, Ac);
                sw.WriteLine("                            = {0:E2} kN", Lc);
                sw.WriteLine("");

                sw.WriteLine("Number of Cables   = Nc = {0}", Nc);

                double _p = p - (Ls + Lc) * Nc;
                sw.WriteLine("Final Prestressing Force    = p - (Ls + Lc) * Nc");
                sw.WriteLine("                            = {0:f3} - ({1:f3} + {2:f3}) * {3} kN ", p, Ls, Lc, Nc);
                sw.WriteLine("                            = {0:f3} kN", _p);
                sw.WriteLine("                            = p");
                sw.WriteLine("");
                PSC_Force_Data f = _p;
                //f
                #endregion STEP 7


                #region STEP 8 : PERMISSIBLE TENDON ZONE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : PERMISSIBLE TENDON ZONE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                foreach (var item in mid_sections.Get_Details())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                I = mid_sections.Iself;
                A = mid_sections.A;
                yt = mid_sections.Yt;
                Zt = mid_sections.Zt;
                Zb = mid_sections.Zb;
                e = mid_sections.Yb - Cable_total.F9;
                e_mid = e;
                sw.WriteLine("Eccentricity at section (9-9) = e = Yb - CG = {0} - {1} = {2} mm",mid_sections.Yb , Cable_total.F9, e);
                sw.WriteLine();
                sw.WriteLine("Check,");
                sw.WriteLine();
                sw.WriteLine("At support Section,");
                sw.WriteLine();

                double e_less_value = ((Zb * fct) / (p * 1000)) - (Zb / (A));
                e_less_value = double.Parse(e_less_value.ToString("0"));
                sw.WriteLine("e <= ((Zb*fct)/p) - (Zb/A)");
                sw.WriteLine("  <= (({0:E2} * {1})/{2:E2}) - ({0:E2}/{3:E2})", Zb, fct, p, A);
                sw.WriteLine("  <= {0:E2} mm", e_less_value);
                sw.WriteLine();

                double e_greater_value = (Zb * ftw / (eta * p * 1000)) - (Zb / (A));
                e_greater_value = double.Parse(e_greater_value.ToString("0"));
                sw.WriteLine("and e >= (Zb*ftw/(η*p)) - (Zb/A)");
                sw.WriteLine("      >= ({0:E2} * {1}/({2}*{3:E2})) - ({0:E2}/{4:E2})", Zb, ftw, eta, p, A);
                sw.WriteLine("      >= {0:E2} mm", e_greater_value);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the {0} Cables are arranged to follow a parabolic Profile with", Nc);

                double Ecg = e;
                sw.WriteLine("the resultant force having an eccentricity of Ecg = {0} mm towards", Ecg);
                sw.WriteLine("the soffit at the support section. The position of Cables at Support");
                sw.WriteLine("Section is shown in the drawing.");
                sw.WriteLine();





                foreach (var item in end_sections.Get_Details())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                I = end_sections.Iself;
                A = end_sections.A;
                yt = end_sections.Yt;
                Zt = end_sections.Zt;
                Zb = end_sections.Zb;

                e = end_sections.Yb - Cable_total.F2;
                double e_end = e;
                sw.WriteLine("Eccentricity at section (1-1) = e  Yb - CG = {0} - {1} = {2} mm", end_sections.Yb ,Cable_total.F2, e);
                sw.WriteLine();
                sw.WriteLine("Check,");
                sw.WriteLine();
                sw.WriteLine("At support Section,");
                sw.WriteLine();

                e_less_value = ((Zb * fct) / (p * 1000)) - (Zb / (A));
                e_less_value = double.Parse(e_less_value.ToString("0"));
                sw.WriteLine("e <= ((Zb*fct)/p) - (Zb/A)");
                sw.WriteLine("  <= (({0:E2} * {1})/{2:E2}) - ({0:E2}/{3:E2})", Zb, fct, p, A);
                sw.WriteLine("  <= {0:E2} mm", e_less_value);
                sw.WriteLine();

                e_greater_value = (Zb * ftw / (eta * p * 1000)) - (Zb / (A));
                e_greater_value = double.Parse(e_greater_value.ToString("0"));
                sw.WriteLine("and e >= (Zb*ftw/(η*p)) - (Zb/A)");
                sw.WriteLine("      >= ({0:E2} * {1}/({2}*{3:E2})) - ({0:E2}/{4:E2})", Zb, ftw, eta, p, A);
                sw.WriteLine("      >= {0:E2} mm", e_greater_value);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the {0} Cables are arranged to follow a parabolic Profile with", Nc);

                Ecg = e;
                sw.WriteLine("the resultant force having an eccentricity of Ecg = {0} mm towards", Ecg);
                sw.WriteLine("the soffit at the support section. The position of Cables at Support");
                sw.WriteLine("Section is shown in the drawing.");
                sw.WriteLine();

                #endregion

                #region STEP 9 : CHECK FOR STRESSES
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 9 : CHECK FOR STRESSES ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                p = P;
                e = e_mid;



                PSC_Force_Data psc_Mult = 9;


                PSC_Force_Data psc_A = 0;
                PSC_Force_Data psc_Yt = 0;
                PSC_Force_Data psc_yb = 0;
                PSC_Force_Data psc_Izz = 0;
                PSC_Force_Data psc_Ixx = 0;
                PSC_Force_Data psc_Zt = 0;
                PSC_Force_Data psc_Zb = 0;
                PSC_Force_Data psc_CG = 0;
                PSC_Force_Data psc_e = 0;

                PSC_Force_Data psc_F_A = 0;

                PSC_Force_Data psc_F_e_Zt = 0;
                PSC_Force_Data psc_F_e_Zb = 0;

                PSC_Force_Data psc_Mult_Zt = 0;
                PSC_Force_Data psc_Mult_Zb = 0;

                PSC_Force_Data psc_F_e_Zt_Mult_Zt = 0;
                PSC_Force_Data psc_F_e_Zb_Mult_Zb = 0;

                PSC_Force_Data psc_σ_t_Transfer = 0;
                PSC_Force_Data psc_σ_b_Transfer = 0;
                PSC_Force_Data psc_σ_t_Work = 0;
                PSC_Force_Data psc_σ_b_Work = 0;

                PSC_Force_Data psc_Ayp = 0;
                PSC_Force_Data psc_Ayc = 0;

                //Mult =Mg+Mq
                //A=
                //Yt= 
                //yb=
                //Izz= 
                //Ixx= 
                //Zt= 
                //Zb= 
                //CG=
                //e=

                //F/A =

                //F*e/Zt =
                //F*e/Zb =

                //Mult/Zt = 
                //Mult/Zb = 

                //(F*e/Zt)+( Mult/Zt)=
                //(F*e/Zb)+( Mult/Zb)=

                //σ_t (Transfer) =
                //σ_b (Transfer) =
                //σ_t (Work) =
                //σ_b (Work) =

                //foreach (var item in mid_sections.Get_Details())
                //{
                //    sw.WriteLine(item);
                //}
                sw.WriteLine();
                I = mid_sections.Iself;
                A = mid_sections.A;
                yt = mid_sections.Yt;
                Zt = mid_sections.Zt;
                Zb = mid_sections.Zb;
                sw.WriteLine();

                //A = (A / 100);
                //sw.WriteLine("A = {0:E2} sq.mm", A);
                //sw.WriteLine();
                //sw.WriteLine("Zt = {0:E2}", Zt);
                //sw.WriteLine();
                //sw.WriteLine("Zb = {0:E2}", Zb);
                //sw.WriteLine();
                //sw.WriteLine("η = {0}", eta);

                if (Is_Inner_Girder)
                {
                    Mg = moment_forces.DL_INNER_GIRDER.F9;
                    Mq = moment_forces.LL_INNER_GIRDER.F9;


                    psc_Mult = moment_forces.DL_INNER_GIRDER + moment_forces.LL_INNER_GIRDER;
                    //f = shear_forces.DL_INNER_GIRDER + shear_forces.LL_INNER_GIRDER;

                    //f = (f * 100);

                    psc_CG = Cable_total;
                    for (int i = 0; i < 9; i++)
                    {
                        if (i < 2)
                        {
                            psc_A[i] = end_sections.A;
                            psc_Yt[i] = end_sections.Yt;
                            psc_yb[i] = end_sections.Yb;
                            psc_Izz[i] = end_sections.Izz;
                            psc_Ixx[i] = end_sections.Ixx;
                            psc_Zt[i] = end_sections.Zt;
                            psc_Zb[i] = end_sections.Zb;
                            psc_e[i] = end_sections.Yb - Cable_total[i];

                            psc_Ayp[i] = psc_A[i] * composit_end_sections.Yt;
                            psc_Ayc[i] = composit_end_sections.A * composit_end_sections.Yt;

                        }
                        else
                        {
                            psc_A[i] = mid_sections.A;
                            psc_Yt[i] = mid_sections.Yt;
                            psc_yb[i] = mid_sections.Yb;
                            psc_Izz[i] = mid_sections.Izz;
                            psc_Ixx[i] = mid_sections.Ixx;
                            psc_Zt[i] = mid_sections.Zt;
                            psc_Zb[i] = mid_sections.Zb;
                            psc_e[i] = mid_sections.Yb - Cable_total[i];
                            psc_Ayp[i] = psc_A[i] * composit_mid_sections.Yt;
                            psc_Ayc[i] = composit_mid_sections.A * composit_mid_sections.Yt;
                        }
                    }
                    psc_F_A = f / psc_A;
                    psc_F_e_Zt = (f * psc_e) / psc_Zt;
                    psc_F_e_Zb = (f * psc_e) / psc_Zb;
                    psc_Mult_Zt = psc_Mult / psc_Zt;
                    psc_Mult_Zb = psc_Mult / psc_Zb;

                    psc_F_e_Zt_Mult_Zt = psc_F_e_Zt + psc_Mult_Zt;
                    psc_F_e_Zb_Mult_Zb = psc_F_e_Zb + psc_Mult_Zb;

                    //psc_Ayp = psc_A + 

                    //σ_t = (p / A) - (p * e / Zt) + (Mg / Zt)
                    //    = 3715.4 - 2997.13 + 2.63
                    //    = 720.9 N/sq.mm

                    //σ_b = (p / A) + (p * e / Zb) - (Mg / Zb)
                    //    = 3715.4 + 2780.47 - 2.44
                    //    = 6493.43 N/sq.mm


                    psc_σ_t_Transfer = psc_F_A - (psc_F_e_Zt) + (moment_forces.DL_INNER_GIRDER / psc_Zt);
                    psc_σ_b_Transfer = psc_F_A + (psc_F_e_Zt) - (moment_forces.DL_INNER_GIRDER / psc_Zb);

                    //At working load Stage :

                    //σ_t = η*(p / A) - η*(p * e / Zt) + (Mg / Zt) + (Mq / Zt)
                    //    = 0.294*3715.4 - 0.294*2997.13 + 2.63 + 0.25
                    //    = 214.05 N/sq.mm  (+ve, so, Compression)
                    psc_σ_t_Work = (eta * psc_F_A) - (eta * psc_F_e_Zt) + (moment_forces.DL_INNER_GIRDER / psc_Zt)
                                     + (moment_forces.LL_INNER_GIRDER / psc_Zt);


                    //σ_t b= η*(p / A) + η*(p * e / Zb) - (Mg / Zb) - (Mq / Zb)
                    //    = 0.294*3715.4 + 0.294*2780.47 - 2.44 - 0.24
                    //    = 1907.106 N/sq.mm  (-ve, so, Tension)

                    psc_σ_b_Work = (eta * psc_F_A) + (eta * psc_F_e_Zt) - (moment_forces.DL_INNER_GIRDER / psc_Zb)
                                      - (moment_forces.LL_INNER_GIRDER / psc_Zb);


                }
                else
                {
                    Mg = moment_forces.DL_OUTER_GIRDER.F9;
                    Mq = moment_forces.LL_OUTER_GIRDER.F9;

                    psc_Mult = moment_forces.DL_INNER_GIRDER + moment_forces.LL_INNER_GIRDER;
                    f = shear_forces.DL_INNER_GIRDER + shear_forces.LL_INNER_GIRDER;


                    f = (f * 100);

                    psc_CG = Cable_total;
                    for (int i = 0; i < 9; i++)
                    {
                        if (i < 2)
                        {
                            psc_A[i] = end_sections.A;
                            psc_Yt[i] = end_sections.Yt;
                            psc_yb[i] = end_sections.Yb;
                            psc_Izz[i] = end_sections.Izz;
                            psc_Ixx[i] = end_sections.Ixx;
                            psc_Zt[i] = end_sections.Zt;
                            psc_Zb[i] = end_sections.Zb;
                            psc_e[i] = end_sections.Yb - Cable_total[i];
                            psc_Ayp[i] = psc_A[i] * composit_end_sections.Yt;
                            psc_Ayc[i] = composit_end_sections.A * composit_end_sections.Yt;

                        }
                        else
                        {
                            psc_A[i] = mid_sections.A;
                            psc_Yt[i] = mid_sections.Yt;
                            psc_yb[i] = mid_sections.Yb;
                            psc_Izz[i] = mid_sections.Izz;
                            psc_Ixx[i] = mid_sections.Ixx;
                            psc_Zt[i] = mid_sections.Zt;
                            psc_Zb[i] = mid_sections.Zb;
                            psc_e[i] = mid_sections.Yb - Cable_total[i]; ;
                            psc_Ayp[i] = psc_A[i] * composit_mid_sections.Yt;
                            psc_Ayc[i] = composit_mid_sections.A * composit_mid_sections.Yt;
                        }
                    }
                    psc_F_A = f / psc_A;
                    psc_F_e_Zt = (f * psc_e) / psc_Zt;
                    psc_F_e_Zb = (f * psc_e) / psc_Zb;
                    psc_Mult_Zt = psc_Mult / psc_Zt;
                    psc_Mult_Zb = psc_Mult / psc_Zb;

                    psc_F_e_Zt_Mult_Zt = psc_F_e_Zt + psc_Mult_Zt;
                    psc_F_e_Zb_Mult_Zb = psc_F_e_Zb + psc_Mult_Zb;



                    //σ_t = (p / A) - (p * e / Zt) + (Mg / Zt)
                    //    = 3715.4 - 2997.13 + 2.63
                    //    = 720.9 N/sq.mm

                    //σ_b = (p / A) + (p * e / Zb) - (Mg / Zb)
                    //    = 3715.4 + 2780.47 - 2.44
                    //    = 6493.43 N/sq.mm


                    psc_σ_t_Transfer = psc_F_A - (psc_F_e_Zt) + (moment_forces.DL_OUTER_GIRDER / psc_Zt);
                    psc_σ_b_Transfer = psc_F_A + (psc_F_e_Zt) - (moment_forces.DL_OUTER_GIRDER / psc_Zb);

                    //At working load Stage :

                    //σ_t = η*(p / A) - η*(p * e / Zt) + (Mg / Zt) + (Mq / Zt)
                    //    = 0.294*3715.4 - 0.294*2997.13 + 2.63 + 0.25
                    //    = 214.05 N/sq.mm  (+ve, so, Compression)
                    psc_σ_t_Work = (eta * psc_F_A) - (eta * psc_F_e_Zt) + (moment_forces.DL_OUTER_GIRDER / psc_Zt)
                                     + (moment_forces.LL_OUTER_GIRDER / psc_Zt);


                    //σ_t b= η*(p / A) + η*(p * e / Zb) - (Mg / Zb) - (Mq / Zb)
                    //    = 0.294*3715.4 + 0.294*2780.47 - 2.44 - 0.24
                    //    = 1907.106 N/sq.mm  (-ve, so, Tension)

                    psc_σ_b_Work = (eta * psc_F_A) + (eta * psc_F_e_Zt) - (moment_forces.DL_OUTER_GIRDER / psc_Zb)
                                      - (moment_forces.LL_OUTER_GIRDER / psc_Zb);


                }
                sw.WriteLine();

                kFormat = sec_format;
                //sec_format = sec_format.ToUpper().Replace("F", "E");
                sec_format = sec_format.ToUpper().Replace("F2", "F3");
                sec_format = sec_format.ToUpper().Replace("-", "");
                sec_format = sec_format.ToUpper().Replace("0,10:", "0,-15:");
                //sec_format = sec_format.ToUpper().Replace("10:", "0,-20:");
                sw.WriteLine("");
                sw.WriteLine("TABLE 5 : STRESS COMPUTATION");
                sw.WriteLine("----------------------------");
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "", "Section", "Section", "Section", "Section", "Section", "Section", "Section", "Section", "Section");
                sw.WriteLine(sec_format, "", "  1-1  ", "  2-2  ", "  3-3  ", "  4-4  ", "  5-5  ", "  6-6  ", "  7-7  ", "  8-8  ", "  9-9  ");
                //sw.WriteLine(sec_format, "", "  (mm) ", " (mm)  ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ", "  (mm) ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "Mult",
                                    psc_Mult.F1,
                                    psc_Mult.F2,
                                    psc_Mult.F3,
                                    psc_Mult.F4,
                                    psc_Mult.F5,
                                    psc_Mult.F6,
                                    psc_Mult.F7,
                                    psc_Mult.F8,
                                    psc_Mult.F9);

                sec_format = sec_format.ToUpper().Replace("F3", "E3");
                sw.WriteLine();
                sw.WriteLine(sec_format, "Area",
                                    psc_A.F1,
                                    psc_A.F2,
                                    psc_A.F3,
                                    psc_A.F4,
                                    psc_A.F5,
                                    psc_A.F6,
                                    psc_A.F7,
                                    psc_A.F8,
                                    psc_A.F9);
                sw.WriteLine();

                sec_format = sec_format.ToUpper().Replace("E", "F");
                sw.WriteLine(sec_format, "Yt",
                                    psc_Yt.F1,
                                    psc_Yt.F2,
                                    psc_Yt.F3,
                                    psc_Yt.F4,
                                    psc_Yt.F5,
                                    psc_Yt.F6,
                                    psc_Yt.F7,
                                    psc_Yt.F8,
                                    psc_Yt.F9);
                sw.WriteLine();

                sw.WriteLine(sec_format, "Yb",
                                    psc_yb.F1,
                                    psc_yb.F2,
                                    psc_yb.F3,
                                    psc_yb.F4,
                                    psc_yb.F5,
                                    psc_yb.F6,
                                    psc_yb.F7,
                                    psc_yb.F8,
                                    psc_yb.F9);

                sw.WriteLine();
                sec_format = sec_format.ToUpper().Replace("F", "E");
                sw.WriteLine(sec_format, "Ixx",
                                    psc_Ixx.F1,
                                    psc_Ixx.F2,
                                    psc_Ixx.F3,
                                    psc_Ixx.F4,
                                    psc_Ixx.F5,
                                    psc_Ixx.F6,
                                    psc_Ixx.F7,
                                    psc_Ixx.F8,
                                    psc_Ixx.F9);
                sw.WriteLine();
                sw.WriteLine(sec_format, "Izz",
                                    psc_Izz.F1,
                                    psc_Izz.F2,
                                    psc_Izz.F3,
                                    psc_Izz.F4,
                                    psc_Izz.F5,
                                    psc_Izz.F6,
                                    psc_Izz.F7,
                                    psc_Izz.F8,
                                    psc_Izz.F9);


                sw.WriteLine(sec_format, "Zt",
                                    psc_Zt.F1,
                                    psc_Zt.F2,
                                    psc_Zt.F3,
                                    psc_Zt.F4,
                                    psc_Zt.F5,
                                    psc_Zt.F6,
                                    psc_Zt.F7,
                                    psc_Zt.F8,
                                    psc_Zt.F9);

                sw.WriteLine(sec_format, "Zb",
                                    psc_Zb.F1,
                                    psc_Zb.F2,
                                    psc_Zb.F3,
                                    psc_Zb.F4,
                                    psc_Zb.F5,
                                    psc_Zb.F6,
                                    psc_Zb.F7,
                                    psc_Zb.F8,
                                    psc_Zb.F9);

                sw.WriteLine();
                sec_format = sec_format.ToUpper().Replace("E3", "F2");
                sw.WriteLine(sec_format, "CG",
                                    psc_CG.F1,
                                    psc_CG.F2,
                                    psc_CG.F3,
                                    psc_CG.F4,
                                    psc_CG.F5,
                                    psc_CG.F6,
                                    psc_CG.F7,
                                    psc_CG.F8,
                                    psc_CG.F9);

                sw.WriteLine(sec_format, "e = Yb-CG",
                                    psc_e.F1,
                                    psc_e.F2,
                                    psc_e.F3,
                                    psc_e.F4,
                                    psc_e.F5,
                                    psc_e.F6,
                                    psc_e.F7,
                                    psc_e.F8,
                                    psc_e.F9);

                sw.WriteLine();
                sec_format = sec_format.ToUpper().Replace("F2", "F4");
                sw.WriteLine(sec_format, "p/A",
                                    psc_F_A.F1,
                                    psc_F_A.F2,
                                    psc_F_A.F3,
                                    psc_F_A.F4,
                                    psc_F_A.F5,
                                    psc_F_A.F6,
                                    psc_F_A.F7,
                                    psc_F_A.F8,
                                    psc_F_A.F9);

                sw.WriteLine(sec_format, "p*e/Zt",
                                    psc_F_e_Zt.F1,
                                    psc_F_e_Zt.F2,
                                    psc_F_e_Zt.F3,
                                    psc_F_e_Zt.F4,
                                    psc_F_e_Zt.F5,
                                    psc_F_e_Zt.F6,
                                    psc_F_e_Zt.F7,
                                    psc_F_e_Zt.F8,
                                    psc_F_e_Zt.F9);

                sw.WriteLine(sec_format, "p*e/Zb",
                                    psc_F_e_Zb.F1,
                                    psc_F_e_Zb.F2,
                                    psc_F_e_Zb.F3,
                                    psc_F_e_Zb.F4,
                                    psc_F_e_Zb.F5,
                                    psc_F_e_Zb.F6,
                                    psc_F_e_Zb.F7,
                                    psc_F_e_Zb.F8,
                                    psc_F_e_Zb.F9);

                sw.WriteLine(sec_format, "Mult/Zt",
                                    psc_Mult_Zt.F1,
                                    psc_Mult_Zt.F2,
                                    psc_Mult_Zt.F3,
                                    psc_Mult_Zt.F4,
                                    psc_Mult_Zt.F5,
                                    psc_Mult_Zt.F6,
                                    psc_Mult_Zt.F7,
                                    psc_Mult_Zt.F8,
                                    psc_Mult_Zt.F9);

                sw.WriteLine(sec_format, "Mult/Zb",
                                    psc_Mult_Zb.F1,
                                    psc_Mult_Zb.F2,
                                    psc_Mult_Zb.F3,
                                    psc_Mult_Zb.F4,
                                    psc_Mult_Zb.F5,
                                    psc_Mult_Zb.F6,
                                    psc_Mult_Zb.F7,
                                    psc_Mult_Zb.F8,
                                    psc_Mult_Zb.F9);

                sw.WriteLine();
                sw.WriteLine("(f*e/Zt)");
                sw.WriteLine(sec_format, "+( Mult/Zt)",
                                    psc_F_e_Zt_Mult_Zt.F1,
                                    psc_F_e_Zt_Mult_Zt.F2,
                                    psc_F_e_Zt_Mult_Zt.F3,
                                    psc_F_e_Zt_Mult_Zt.F4,
                                    psc_F_e_Zt_Mult_Zt.F5,
                                    psc_F_e_Zt_Mult_Zt.F6,
                                    psc_F_e_Zt_Mult_Zt.F7,
                                    psc_F_e_Zt_Mult_Zt.F8,
                                    psc_F_e_Zt_Mult_Zt.F9);

                sw.WriteLine();
                sw.WriteLine("(f*e/Zb)");
                sw.WriteLine(sec_format, "+( Mult/Zb)",
                                    psc_F_e_Zb_Mult_Zb.F1,
                                    psc_F_e_Zb_Mult_Zb.F2,
                                    psc_F_e_Zb_Mult_Zb.F3,
                                    psc_F_e_Zb_Mult_Zb.F4,
                                    psc_F_e_Zb_Mult_Zb.F5,
                                    psc_F_e_Zb_Mult_Zb.F6,
                                    psc_F_e_Zb_Mult_Zb.F7,
                                    psc_F_e_Zb_Mult_Zb.F8,
                                    psc_F_e_Zb_Mult_Zb.F9);

                sw.WriteLine();
                sw.WriteLine(sec_format, "σ_t (Transfer)",
                                    psc_σ_t_Transfer.F1,
                                    psc_σ_t_Transfer.F2,
                                    psc_σ_t_Transfer.F3,
                                    psc_σ_t_Transfer.F4,
                                    psc_σ_t_Transfer.F5,
                                    psc_σ_t_Transfer.F6,
                                    psc_σ_t_Transfer.F7,
                                    psc_σ_t_Transfer.F8,
                                    psc_σ_t_Transfer.F9);

                sw.WriteLine(sec_format, "σ_b (Transfer)",
                                    psc_σ_b_Transfer.F1,
                                    psc_σ_b_Transfer.F2,
                                    psc_σ_b_Transfer.F3,
                                    psc_σ_b_Transfer.F4,
                                    psc_σ_b_Transfer.F5,
                                    psc_σ_b_Transfer.F6,
                                    psc_σ_b_Transfer.F7,
                                    psc_σ_b_Transfer.F8,
                                    psc_σ_b_Transfer.F9);

                sw.WriteLine(sec_format, "σ_t (Work)",
                                    psc_σ_t_Work.F1,
                                    psc_σ_t_Work.F2,
                                    psc_σ_t_Work.F3,
                                    psc_σ_t_Work.F4,
                                    psc_σ_t_Work.F5,
                                    psc_σ_t_Work.F6,
                                    psc_σ_t_Work.F7,
                                    psc_σ_t_Work.F8,
                                    psc_σ_t_Work.F9);

                sw.WriteLine(sec_format, "σ_b (Work)",
                                    psc_σ_b_Work.F1,
                                    psc_σ_b_Work.F2,
                                    psc_σ_b_Work.F3,
                                    psc_σ_b_Work.F4,
                                    psc_σ_b_Work.F5,
                                    psc_σ_b_Work.F6,
                                    psc_σ_b_Work.F7,
                                    psc_σ_b_Work.F8,
                                    psc_σ_b_Work.F9);
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Sample calculations");
                sw.WriteLine("-------------------");
                sw.WriteLine();
                sec_format = kFormat;

                p = f.F9;
                sw.WriteLine("For section at centre of span we have, ");

                foreach (var item in mid_sections.Get_Details())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                sw.WriteLine("p = {0} kN", (p > 1000000) ? (p.ToString("E3")) : (p.ToString("F3")));
                sw.WriteLine();
                sw.WriteLine("e = {0} mm", (e > 1000000) ? (e.ToString("E3")) : (e.ToString("F3")));
                sw.WriteLine();
                //sw.WriteLine("Mg = {0} kN-m  (At section 9-9 for Midspan and section 2-2 for Endspan)", Mg);
                sw.WriteLine("Mg = {0} kN-m  (At section 9-9 for Midspan)", Mg);
                sw.WriteLine();
                //sw.WriteLine("Mq = {0} kN-m  (At section 9-9 for Midspan and section 2-2 for Endspan)", Mq);
                sw.WriteLine("Mq = {0} kN-m  (At section 9-9 for Midspan)", Mq);
                sw.WriteLine();

                double m_ult = Mq + Mg;
                sw.WriteLine("M_ult = Mg + Mq = {0} + {1}  = {2} kN-m  (At section 9-9 for Midspan)", Mg, Mq, m_ult);
                sw.WriteLine();
                sw.WriteLine();


                double p_by_A = (p * 1000) / (A);
                p_by_A = double.Parse(p_by_A.ToString("0.00"));
                sw.WriteLine("p/A = {0:E2} * 1000 / ({1})", p, A, p_by_A);
                sw.WriteLine("    = {0:E2} N/sq.mm", p_by_A);
                sw.WriteLine("    = {0:E2} kN/sq.m", p_by_A/1000.00);
                sw.WriteLine();

                double val1, val2;

                val1 = p * 1000 * e / (Zt);
                val1 = double.Parse(val1.ToString("0.00"));
                sw.WriteLine("p*e/Zt = {0:E2} * 1000 * {1} / ({2:E2})", p, e, Zt);
                sw.WriteLine("       = {0:E2} N/sq.mm", val1);
                sw.WriteLine("       = {0:E2} kN/sq.m", val1 / 1000.00);
                sw.WriteLine();

                val2 = p * 1000 * e / (Zb);
                val2 = double.Parse(val2.ToString("0.00"));
                sw.WriteLine("p*e/Zb = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zb);
                sw.WriteLine("       = {0:E2} N/sq.mm", val2);
                sw.WriteLine("       = {0:E2} kN/sq.m", val2 / 1000.00);
                sw.WriteLine();

                double Mg_by_Zt = m_ult * 10E5 / (Zt);
                Mg_by_Zt = double.Parse(Mg_by_Zt.ToString("0.00"));
                sw.WriteLine("M_ult/Zt = {0} * 10^6 / ({1:E2})", m_ult, Zt);
                sw.WriteLine("         = {0:E2}  N/sq.mm", Mg_by_Zt);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mg_by_Zt / 1000.00);
                sw.WriteLine();

                double Mg_by_Zb = m_ult * 10E5 / (Zb);
                Mg_by_Zb = double.Parse(Mg_by_Zb.ToString("0.00"));
                sw.WriteLine("M_ult/Zb = {0} * 10^6  / ({1:E2})", m_ult, Zb);
                sw.WriteLine("         = {0:E2} N/sq.mm ", Mg_by_Zb);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mg_by_Zb / 1000.00);
                sw.WriteLine();

                double Mq_by_Zt = m_ult * 10E5 / (Zt);
                Mq_by_Zt = double.Parse(Mq_by_Zt.ToString("0.00"));
                sw.WriteLine("M_ult/Zt = {0} * 10^6  / ({1:E2})", m_ult, Zt);
                sw.WriteLine("         = {0:E2} N/sq.mm ", Mq_by_Zt);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mq_by_Zt / 1000.00);
                sw.WriteLine();

                double Mq_by_Zb = m_ult * 10E5 / (Zb);
                Mq_by_Zb = double.Parse(Mq_by_Zb.ToString("0.00"));
                sw.WriteLine("M_ult/Zb = {0} * 10^6  / ({1:E2})", m_ult, Zb);
                sw.WriteLine("         = {0:E2} N/sq.mm ", Mq_by_Zb);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mq_by_Zb / 1000.00);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("At transfer stage :");
                sw.WriteLine();

                //double sigma_t = (p / A) - (p * e / Zt) + (Mg / Zt);
                double sigma_t = p_by_A - val1 + Mg_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = (p / A) - (p * e / Zt) + (M_ult / Zt)");
                sw.WriteLine("    = {0:E2} - {1:E2} + {2:E2}", p_by_A, val1, Mg_by_Zt);
                sw.WriteLine("    = {0:E2} N/sq.mm", sigma_t);
                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_t / 1000.00);
                sw.WriteLine();

                double sigma_b = p_by_A + val2 - Mg_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.00"));
                sw.WriteLine("σ_b = (p / A) + (p * e / Zb) - (M_ult / Zb)");
                sw.WriteLine("    = {0} + {1} - {2}", p_by_A, val2, Mg_by_Zb);
                sw.WriteLine("    = {0:E2} N/sq.mm", sigma_b);
                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_b / 1000.00);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("At working load Stage :");
                sw.WriteLine();

                sigma_t = eta * p_by_A - eta * val1 + Mg_by_Zt + Mq_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = η*(p / A) - η*(p * e / Zt) + (M_ult / Zt) + (M_ult / Zt)");
                sw.WriteLine("    = {0}*{1} - {0}*{2} + {3} + {4}", eta, p_by_A, val1, Mg_by_Zt, Mq_by_Zt);

                if (sigma_t >= 0)
                    sw.WriteLine("    = {0:E2} N/sq.mm  (+ve, so, Compression)", sigma_t);
                else
                    sw.WriteLine("    = {0:E2} N/sq.mm  (-ve, so, Tension)", sigma_t);
                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_t / 1000.00);
                sw.WriteLine();


                sigma_b = eta * p_by_A + eta * val2 - Mg_by_Zb - Mq_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.000"));
                sw.WriteLine("σ_b = η*(p / A) + η*(p * e / Zb) - (M_ult / Zb) - (M_ult / Zb)");
                sw.WriteLine("    = {0}*{1} + {0}*{2} - {3} - {4}", eta, p_by_A, val2, Mg_by_Zb, Mq_by_Zb);
                if (sigma_b >= 0)
                    sw.WriteLine("    = {0:E2} N/sq.mm  (+ve, so, Compression)", sigma_b);
                else
                    sw.WriteLine("    = {0:E2} N/sq.mm  (-ve, so, Tension)", sigma_b);

                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_b / 1000.00);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the stresses at top and bottom fibres at Transfer and Service");
                sw.WriteLine("Loads are well within the Safe Permissible Limits.");
                sw.WriteLine();

                foreach (var item in end_sections.Get_Details())
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine();
                I = end_sections.Iself;
                A = end_sections.A;
                yt = end_sections.Yt;
                Zt = end_sections.Zt;
                Zb = end_sections.Zb;


                if (Is_Inner_Girder)
                {
                    Mg = moment_forces.DL_INNER_GIRDER.F2;
                    Mq = moment_forces.LL_INNER_GIRDER.F2;
                }
                else
                {
                    Mg = moment_forces.DL_OUTER_GIRDER.F2;
                    Mq = moment_forces.LL_OUTER_GIRDER.F2;
                }


                sw.WriteLine();
                sw.WriteLine("Mg = {0} kN-m  (At section 2-2 for Endspan)", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m  (At section 2-2 for Endspan)", Mq);
                sw.WriteLine();
                m_ult = Mq + Mg;
                sw.WriteLine("M_ult = Mg + Mq = {0} + {1}  = {2} kN-m  (At section 1-1 & 2-2 for End Span)", Mg, Mq, m_ult);
               

                p_by_A = (p * 1000) / (A);
                p_by_A = double.Parse(p_by_A.ToString("0.00"));
                sw.WriteLine("p/A = {0:f0} / ({1})", p, A, p_by_A);
                sw.WriteLine("    = {0:E2} kN/sq.m", p_by_A / 1000.00);
                sw.WriteLine();


                val1 = p * 1000 * e / (Zt);
                val1 = double.Parse(val1.ToString("0.00"));
                sw.WriteLine("p*e/Zt = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zt);
                sw.WriteLine("       = {0:f2} N/sq.mm", val1);
                sw.WriteLine("       = {0:E2} kN/sq.m", val1 / 1000.00);
                sw.WriteLine();

                val2 = p * 1000 * e / (Zb);
                val2 = double.Parse(val2.ToString("0.00"));
                sw.WriteLine("p*e/Zb = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zb);
                sw.WriteLine("       = {0:f2} N/sq.mm", val2);
                sw.WriteLine("    = {0:E2} kN/sq.m", val2 / 1000.00);
                sw.WriteLine();

                Mg_by_Zt = m_ult * 10E5 / (Zt);
                Mg_by_Zt = double.Parse(Mg_by_Zt.ToString("0.00"));
                sw.WriteLine("M_ult/Zt = {0} * 10^6 / ({1:E2})", Mg, Zt);
                sw.WriteLine("         = {0:E2}  N/sq.mm", Mg_by_Zt);
                sw.WriteLine("    = {0:E2} kN/sq.m", Mg_by_Zt / 1000.00);
                sw.WriteLine();

                Mg_by_Zb = m_ult * 10E5 / (Zb);
                Mg_by_Zb = double.Parse(Mg_by_Zb.ToString("0.00"));
                sw.WriteLine("M_ult/Zb = {0} * 10^6  / ({1:E2})", Mg, Zb);
                sw.WriteLine("         = {0:f2} N/sq.mm ", Mg_by_Zb);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mg_by_Zb / 1000.00);
                sw.WriteLine();

                Mq_by_Zt = m_ult * 10E5 / (Zt);
                Mq_by_Zt = double.Parse(Mq_by_Zt.ToString("0.00"));
                sw.WriteLine("M_ult/Zt = {0} * 10^6  / ({1:E2})", Mg, Zt);
                sw.WriteLine("         = {0:f2} N/sq.mm ", Mq_by_Zt);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mq_by_Zt / 1000.00);
                sw.WriteLine();

                Mq_by_Zb = m_ult * 10E5 / (Zb);
                Mq_by_Zb = double.Parse(Mq_by_Zb.ToString("0.00"));
                sw.WriteLine("M_ult/Zb = {0} * 10^6  / ({1:E2})", Mg, Zb);
                sw.WriteLine("         = {0:f2} N/sq.mm ", Mq_by_Zb);
                sw.WriteLine("         = {0:E2} kN/sq.m", Mq_by_Zb / 1000.00);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("At transfer stage :");
                sw.WriteLine();

                //double sigma_t = (p / A) - (p * e / Zt) + (Mg / Zt);
                sigma_t = p_by_A - val1 + Mg_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = (p / A) - (p * e / Zt) + (Mg / Zt)");
                sw.WriteLine("    = {0} - {1} + {2}", p_by_A, val1, Mg_by_Zt);
                sw.WriteLine("    = {0} N/sq.mm", sigma_t);
                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_t / 1000.00);
                sw.WriteLine();

                sigma_b = p_by_A + val2 - Mg_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.00"));
                sw.WriteLine("σ_b = (p / A) + (p * e / Zb) - (M_ult / Zb)");
                sw.WriteLine("    = {0} + {1} - {2}", p_by_A, val2, Mg_by_Zb);
                sw.WriteLine("    = {0} N/sq.mm", sigma_b);
                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_b / 1000.00);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("At working load Stage :");
                sw.WriteLine();

                sigma_t = eta * p_by_A - eta * val1 + Mg_by_Zt + Mq_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = η*(p / A) - η*(p * e / Zt) + (M_ult / Zt) + (M_ult / Zt)");
                sw.WriteLine("    = {0}*{1} - {0}*{2} + {3} + {4}", eta, p_by_A, val1, Mg_by_Zt, Mq_by_Zt);
                if(sigma_t >=0)
                    sw.WriteLine("    = {0} N/sq.mm  (+ve, so, Compression)", sigma_t);
                else 
                    sw.WriteLine("    = {0} N/sq.mm  (-ve, so, Tension)", sigma_t);

                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_t / 1000.00);
                sw.WriteLine();


                sigma_b = eta * p_by_A + eta * val2 - Mg_by_Zb - Mq_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.000"));
                sw.WriteLine("σ_t = η*(p / A) + η*(p * e / Zb) - (M_ult / Zb) - (M_ult / Zb)");
                sw.WriteLine("    = {0}*{1} + {0}*{2} - {3} - {4}", eta, p_by_A, val2, Mg_by_Zb, Mq_by_Zb);
                if (sigma_b >= 0)
                    sw.WriteLine("    = {0} N/sq.mm  (+ve, so, Compression)", sigma_b);
                else
                    sw.WriteLine("    = {0} N/sq.mm  (-ve, so, Tension)", sigma_b);
                sw.WriteLine("    = {0:E2} kN/sq.m", sigma_b / 1000.00);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the stresses at top and bottom fibres at Transfer and Service");
                sw.WriteLine("Loads are well within the Safe Permissible Limits.");
                sw.WriteLine();

                #endregion

                #region STEP 10 : CHECK FOR ULTIMATE FLEXURAL STRENGTH
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 10 : CHECK FOR ULTIMATE FLEXURAL STRENGTH ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("i)                Failure by yield of steel (Under Reinforced Section)");
                sw.WriteLine("");
                PSC_Force_Data M_ult_steel = 0, db = 0;




                sw.WriteLine("M_ult (Steel) = 0.9 x db x As x fp");
                sw.WriteLine("");
                sw.WriteLine("As = Area of High Tensile Steel = {0:f2} Sq.mm", _As);
                sw.WriteLine("fp = Ultimate Tensile Strength of Steel = {0:f2} N/Sq.mm", fp);
                sw.WriteLine("Thickness of Deck Slab = {0} mm.", d1);
                sw.WriteLine("Depth of Girder = {0} mm.", D);
                sw.WriteLine("Distances of CG of Tendons from bottom");
                //sw.WriteLine("=  1255.14  906.68    501.38  317.78    273.7   245.96   245.96   245.96   245.96   ");
                //sw.WriteLine(Cable_total.ToString());

                db.F1 = (D + d1) - Cable_total.F1;
                db.F2 = (D + d1) - Cable_total.F2;
                db.F3 = (D + d1) - Cable_total.F3;
                db.F4 = (D + d1) - Cable_total.F4;
                db.F5 = (D + d1) - Cable_total.F5;
                db.F6 = (D + d1) - Cable_total.F6;
                db.F7 = (D + d1) - Cable_total.F7;
                db.F8 = (D + d1) - Cable_total.F8;
                db.F9 = (D + d1) - Cable_total.F9;

                M_ult_steel.F1 = 0.9 * db.F1 * _As * fp;
                M_ult_steel.F2 = 0.9 * db.F2 * _As * fp;
                M_ult_steel.F3 = 0.9 * db.F3 * _As * fp;
                M_ult_steel.F4 = 0.9 * db.F4 * _As * fp;
                M_ult_steel.F5 = 0.9 * db.F5 * _As * fp;
                M_ult_steel.F6 = 0.9 * db.F6 * _As * fp;
                M_ult_steel.F7 = 0.9 * db.F7 * _As * fp;
                M_ult_steel.F8 = 0.9 * db.F8 * _As * fp;
                M_ult_steel.F9 = 0.9 * db.F9 * _As * fp;
                M_ult_steel /= 1000000.0;

                sw.WriteLine("");
                sw.WriteLine("db = Depth of CG of Tendons from top of Slab+Girder, at different sections");
                sw.WriteLine("   ");
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 1-1", D, d1, Cable_total.F1, db.F1);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 2-2", D, d1, Cable_total.F2, db.F2);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 3-3", D, d1, Cable_total.F3, db.F3);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 4-4", D, d1, Cable_total.F4, db.F4);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 5-5", D, d1, Cable_total.F5, db.F5);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 6-6", D, d1, Cable_total.F6, db.F6);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 7-7", D, d1, Cable_total.F7, db.F7);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 8-8", D, d1, Cable_total.F8, db.F8);
                sw.WriteLine("= ({0:f2}+{1:f2}) - {2:f2} = {3:f2} mm. at Section 9-9", D, d1, Cable_total.F9, db.F9);
                //sw.WriteLine("= (2400.0+250.0) -  907.0 = 1743.0 mm. at Section 2-2");
                //sw.WriteLine("= (2400.0+250.0) -  501.0 = 2149.0 mm. at Section 3-3");
                //sw.WriteLine("= (2400.0+250.0) -  318.0 = 2332.0 mm. at Section 4-4");
                //sw.WriteLine("= (2400.0+250.0) -  274.0 = 2376.0 mm. at Section 5-5");
                //sw.WriteLine("= (2400.0+250.0) -  246.0 = 2404.0 mm. at Section 6-6");
                //sw.WriteLine("= (2400.0+250.0) -  246.0 = 2404.0 mm. at Section 7-7");
                //sw.WriteLine("= (2400.0+250.0) -  246.0 = 2404.0 mm. at Section 8-8");
                //sw.WriteLine("= (2400.0+250.0) -  246.0 = 2404.0 mm. at Section 9-9");
                sw.WriteLine("");
                sw.WriteLine("ii)   Failure by Crushing of Concrete (Over Reinforced Section)");
                sw.WriteLine("");

                PSC_Force_Data M_ult_conc = 0, conc_b = 0;
                conc_b.F1 = 0.700;
                conc_b.F2 = 0.525;
                conc_b.F3 = 0.350;
                conc_b.F4 = 0.350;
                conc_b.F5 = 0.350;
                conc_b.F6 = 0.350;
                conc_b.F7 = 0.350;
                conc_b.F8 = 0.350;
                conc_b.F9 = 0.350;
                conc_b *= 1000;


                double Bf = D;
                double tf = d1;
                M_ult_conc.F1 = 0.176 * conc_b.F1 * db.F1 * db.F1 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F1) * (db.F1 - (tf / 2)) * tf * fck;
                M_ult_conc.F2 = 0.176 * conc_b.F2 * db.F2 * db.F2 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F2) * (db.F2 - (tf / 2)) * tf * fck;
                M_ult_conc.F3 = 0.176 * conc_b.F3 * db.F3 * db.F3 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F3) * (db.F3 - (tf / 2)) * tf * fck;
                M_ult_conc.F4 = 0.176 * conc_b.F4 * db.F4 * db.F4 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F4) * (db.F4 - (tf / 2)) * tf * fck;
                M_ult_conc.F5 = 0.176 * conc_b.F5 * db.F5 * db.F5 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F5) * (db.F5 - (tf / 2)) * tf * fck;
                M_ult_conc.F6 = 0.176 * conc_b.F6 * db.F6 * db.F6 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F6) * (db.F6 - (tf / 2)) * tf * fck;
                M_ult_conc.F7 = 0.176 * conc_b.F7 * db.F7 * db.F7 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F7) * (db.F7 - (tf / 2)) * tf * fck;
                M_ult_conc.F8 = 0.176 * conc_b.F8 * db.F8 * db.F8 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F8) * (db.F8 - (tf / 2)) * tf * fck;
                M_ult_conc.F9 = 0.176 * conc_b.F9 * db.F9 * db.F9 * fck + (2.0 / 3.0) * 0.8 * (Bf - conc_b.F9) * (db.F9 - (tf / 2)) * tf * fck;

                M_ult_conc /= 1000000.0;
                sw.WriteLine("M_ult (Concrete) = 0.176 x b x db^2 x fck + (2/3)x0.8x(Bf-b)x(db-(t/2))x t x fck");
                sw.WriteLine("");
                sw.WriteLine("b = Thickness of Girder web = 0.700 0.525 0.350 0.350 0.350 0.350 0.350 0.350 0.350m");
                sw.WriteLine("Bf = Overall width of the top flange = 2.400 m");
                sw.WriteLine("Tf = Average thickness of flange = 0.250 m");
                sw.WriteLine("");
                sw.WriteLine("TABLE 6 : CHECK FOR ULTIMATE FLEXURAL STRENGTH ");
                sw.WriteLine("---------------------------------------------- ");
                sw.WriteLine("");
                sec_format = sec_format.Replace("-", "");
                sec_format = sec_format.Replace("0,10:f2", "0,-20:F3");
                sw.WriteLine(("-----------------------------------------------------------------------------------------------------------------------"));
                sw.WriteLine(sec_format, "Sections", "1-1  ", "2-2  ", "3-3  ", "4-4  ", "5-5   ", "6-6  ", "7-7  ", "8-8  ", "9-9  ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "As (mm^2)",
                  _As, _As, _As, _As, _As, _As, _As, _As, _As);
                sw.WriteLine(sec_format, "fp (N/mm^2)",
                            fp, fp, fp, fp, fp, fp, fp, fp, fp);

                sw.WriteLine(sec_format, "db (mm)",
                                db.F1, db.F2, db.F3, db.F4, db.F5, db.F6, db.F7, db.F8, db.F9);

                sw.WriteLine(sec_format.Replace(":f2", ":F2"), "M_ult(steel) (kN-m)",
                                M_ult_steel.F1, M_ult_steel.F2, M_ult_steel.F3, M_ult_steel.F4, M_ult_steel.F5, M_ult_steel.F6, M_ult_steel.F7, M_ult_steel.F8, M_ult_steel.F9);

                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                //sw.WriteLine("As                8290.0  8290.0  8290.0  8290.0  8290.0  8290.0  8290.0  8290.0  8290.0");
                //sw.WriteLine("fp    1861.0  1861.0  1861.0  1861.0  1861.0  1861.0  1861.0  1861.0  1861.0");
                //sw.WriteLine("db    1395.0  1743.0  2149.0  2332.0  2376.0  2404.0  2404.0  2404.0  2404.0");
                //sw.WriteLine("M_ult 19373.3 242075  183106  323894  329993  333860  333860  333860  333860");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                sw.WriteLine(sec_format, "b (mm)",
                                conc_b.F1, conc_b.F2, conc_b.F3, conc_b.F4, conc_b.F5, conc_b.F6, conc_b.F7, conc_b.F8, conc_b.F9);
                sw.WriteLine(sec_format, "Bf (mm)",
                        Bf, Bf, Bf, Bf, Bf, Bf, Bf, Bf, Bf);
                sw.WriteLine(sec_format, "tf (mm)",
                                     tf, tf, tf, tf, tf, tf, tf, tf, tf);
                sw.WriteLine(sec_format.Replace(":f2", ":F2"), "M_ult(Conc) (kN-m)",
                               M_ult_conc.F1, M_ult_conc.F2, M_ult_conc.F3, M_ult_conc.F4,
                               M_ult_conc.F5, M_ult_conc.F6, M_ult_conc.F7, M_ult_conc.F8, M_ult_conc.F9);


                //sw.WriteLine("b     0.700   0.525   0.350   0.350   0.350   0.350   0.350   0.350   0.350");
                //sw.WriteLine("Bf    2.400   2.400   2.400   2.400   2.400   2.400   2.400   2.400   2.400");
                //sw.WriteLine("tf    0.250   0.250   0.250   0.250   0.250   0.250   0.250   0.250   0.250");
                //sw.WriteLine("M_ult 23742   30837   19498   42226   44876   44051   44051   44051   44051");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");


                PSC_Force_Data min_M_ult = 0;

                min_M_ult.F1 = (M_ult_conc.F1 < M_ult_steel.F1) ? M_ult_conc.F1 : M_ult_steel.F1;
                min_M_ult.F2 = (M_ult_conc.F2 < M_ult_steel.F2) ? M_ult_conc.F2 : M_ult_steel.F2;
                min_M_ult.F3 = (M_ult_conc.F3 < M_ult_steel.F3) ? M_ult_conc.F3 : M_ult_steel.F3;
                min_M_ult.F4 = (M_ult_conc.F4 < M_ult_steel.F4) ? M_ult_conc.F4 : M_ult_steel.F4;
                min_M_ult.F4 = (M_ult_conc.F4 < M_ult_steel.F4) ? M_ult_conc.F4 : M_ult_steel.F4;
                min_M_ult.F5 = (M_ult_conc.F5 < M_ult_steel.F5) ? M_ult_conc.F5 : M_ult_steel.F5;
                min_M_ult.F6 = (M_ult_conc.F6 < M_ult_steel.F6) ? M_ult_conc.F6 : M_ult_steel.F6;
                min_M_ult.F7 = (M_ult_conc.F7 < M_ult_steel.F7) ? M_ult_conc.F7 : M_ult_steel.F7;
                min_M_ult.F8 = (M_ult_conc.F8 < M_ult_steel.F8) ? M_ult_conc.F8 : M_ult_steel.F8;
                min_M_ult.F9 = (M_ult_conc.F9 < M_ult_steel.F9) ? M_ult_conc.F9 : M_ult_steel.F9;

                sw.WriteLine("Minimum");
                //sw.WriteLine("M_ult 19373.3  30837   19498   42226   44876   44051   44051   44051   44051");
                sw.WriteLine(sec_format.Replace(":f2", ":F2"), "M_ult",
                              min_M_ult.F1, min_M_ult.F2, min_M_ult.F3, min_M_ult.F4,
                              min_M_ult.F5, min_M_ult.F6, min_M_ult.F7, min_M_ult.F8, min_M_ult.F9);

                PSC_Force_Data ana_M_Ult = new PSC_Force_Data();

                if (Is_Inner_Girder)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        ana_M_Ult[i] = moment_forces.DL_INNER_GIRDER[i] + moment_forces.LL_INNER_GIRDER[i];
                    }
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        ana_M_Ult[i] = moment_forces.DL_OUTER_GIRDER[i] + moment_forces.LL_OUTER_GIRDER[i];
                    }
                }

                sw.WriteLine("");
                sw.WriteLine("Analysed");
                sw.WriteLine(sec_format.Replace(":f2", ":F2"), "M_ult",
                            ana_M_Ult.F1, ana_M_Ult.F2, ana_M_Ult.F3, ana_M_Ult.F4,
                            ana_M_Ult.F5, ana_M_Ult.F6, ana_M_Ult.F7, ana_M_Ult.F8, ana_M_Ult.F9);
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "Remarks",
                    (ana_M_Ult.F1 < min_M_ult.F1) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F2 < min_M_ult.F2) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F3 < min_M_ult.F3) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F4 < min_M_ult.F4) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F5 < min_M_ult.F5) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F6 < min_M_ult.F6) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F7 < min_M_ult.F7) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F8 < min_M_ult.F8) ? "Safe   " : "Unsafe  ",
                    (ana_M_Ult.F9 < min_M_ult.F9) ? "Safe   " : "Unsafe  ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine("");

                if (false)
                {
                    #region [2012 08 24]

                    foreach (var item in mid_sections.Get_Details())
                    {
                        sw.WriteLine(item);
                    }
                    sw.WriteLine();
                    I = mid_sections.Iself;
                    A = mid_sections.A;
                    yt = mid_sections.Yt;
                    Zt = mid_sections.Zt;
                    Zb = mid_sections.Zb;
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
                    sw.WriteLine("   = {0:e2} kN-m", Mu);
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
                    sw.WriteLine();
                    //sw.WriteLine("          = {0:E3} kN-m", Mu1);
                    sw.WriteLine();

                    if (Mu < Mu1)
                    {
                        sw.WriteLine("          = {0:E2} kN-m    >  {1:e2} kN-m  (Mu),  Hence, OK  ", Mu1, Mu);
                        //sw.WriteLine("Mu < Mu1, Hence, OK");
                    }
                    else
                    {
                        sw.WriteLine("          = {0:E2} kN-m    <  {1:e2} kN-m  (Mu),  Hence, NOT OK  ", Mu1, Mu);
                        //sw.WriteLine("Mu > Mu1, Hence, NOT OK", Mu1);
                        //sw.WriteLine("          = {0:E3} kN-m   Mu > Mu1, Hence, NOT OK", Mu1);
                        sw.WriteLine("");
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
                    sw.WriteLine();
                    //sw.WriteLine("    = {0:E3} kN-m ", Mu3);
                    sw.WriteLine();

                    if (Mu < Mu3)
                    {
                        sw.WriteLine("    = {0:E3} kN-m   >  {1:E3} kN-m (Mu),  Hence, OK    ", Mu3, Mu);
                        //sw.WriteLine("Mu < Mu3, Hence, OK");
                    }
                    else
                    {
                        sw.WriteLine("    = {0:E3} kN-m   <  {1:E3} kN-m (Mu),  Hence, NOT OK    ", Mu3, Mu);
                        //sw.WriteLine("Mu > Mu3, Hence, NOT OK");
                    }
                    sw.WriteLine();

                    #endregion [2012 08 24]
                }
                #endregion

                #region STEP 11 : Check for Ultimate Shear Strength
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 11 : CHECK FOR ULTIMATE SHEAR STRENGTH ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //psc_
                 
                sw.WriteLine("Calculation Details");
                sw.WriteLine("-------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"--------------------");
                sw.WriteLine(@"Calculations Details");
                sw.WriteLine(@"--------------------");
                sw.WriteLine();


                PSC_Force_Data Vc1 = 0;
                PSC_Force_Data Ayp = 0;
                PSC_Force_Data Ayc = 0;
                PSC_Force_Data fcp = 0;
                PSC_Force_Data fs = 0;
                PSC_Force_Data ft = 0;
                PSC_Force_Data Vc2 = 0;
                PSC_Force_Data Vco = 0;
                //PSC_Force_Data db = 0;
                PSC_Force_Data psc_b = 0;
                PSC_Force_Data V = 0;
                PSC_Force_Data Ic = 0;
                PSC_Force_Data Ip = 0;
               
                ft = 0.24 * Math.Sqrt(fck);

                if (Is_Inner_Girder)
                    Vc1 = shear_forces.LL_INNER_GIRDER + shear_forces.DL_INNER_GIRDER;
                else
                    Vc1 = shear_forces.LL_OUTER_GIRDER + shear_forces.DL_OUTER_GIRDER;

                sw.WriteLine(@"Vc1 (kN)        = {0:f3}   {1:f3}   {2:f3}   {3:f3}   {4:f3}   {5:f3}   {6:f3}   {7:f3}   {8:f3}",
                 Vc1[0], Vc1[1], Vc1[2], Vc1[3], Vc1[4], Vc1[5], Vc1[6], Vc1[7], Vc1[8]);

                for (int i = 0; i < 9; i++)
                {
                    if (i < 2) // End Section (1-1)
                    {
                        Ayp[i] = Math.Abs(end_sections.A * (composit_end_sections.Yt - Cable_total[i]));
                        Ayc[i] = Math.Abs(composit_end_sections.A * (composit_end_sections.Yt - Cable_total[i]));
                        fcp[i] = (p * 1000.0) / composit_end_sections.A;
                        psc_b[i] = end_sections.b2;
                        Ic[i] = composit_end_sections.Izz;
                        Ip[i] = end_sections.Izz;

                        //Vc2[i] = (Ic[i] * psc_b[i]) *
                        //          Math.Sqrt((ft[i] * ft[i]) + (fcp[i] * ft[i])) / Ayp[i];


                    }
                    else // Mid Section (3-3 to 9-9)
                    {
                        Ayp[i] = Math.Abs(mid_sections.A * (composit_mid_sections.Yt - Cable_total[i]));
                        Ayc[i] = Math.Abs(composit_mid_sections.A * (composit_mid_sections.Yt - Cable_total[i]));
                        fcp[i] = (p * 1000.0) / composit_mid_sections.A;
                        psc_b[i] = mid_sections.b2;
                        Ic[i] = composit_mid_sections.Izz;
                        Ip[i] = mid_sections.Izz;

                    }
                    Vc2[i] = (Ic[i] * psc_b[i]) * Math.Sqrt((ft[i] * ft[i]) + (fcp[i] * ft[i])) / Ayc[i];
                    db[i] = Math.Max(0.8 * D, (D - Cable_total[i]));

                    //V[i] = (5 * (psc_b[i] / 1000.0) * (db[i] / 1000.0)) + p * Math.Sin(cumulative_theta[i] * (Math.PI / 180.0));
                    V[i] = (5 * (psc_b[i] / 1000.0) * (db[i] / 1000.0)) * 1000.0;
                }
                fs = (Vc1 * Ayp) / (Ip * psc_b);

                Vco = Vc1 + (Vc2/1000.0);



                //sw.WriteLine(@"Vc1 (kN)        = 324.40  297.500        103.980          127.820  71.900  49.310  28.070  20.976  5.034");
             
                sw.WriteLine();
                sw.WriteLine();

                //sw.WriteLine(@"Ayp     = 1.680E+006 x (1101.316 - 1459.680) = 6.021 x 10^8  (mm^3) (Sec 1-1)");
                //sw.WriteLine(@"        = 1.680E+006 x (1101.316 -  907.060) = 3.264 x 10^8  (mm^3) (Sec 2-2)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  501.380) = 5.202 x 10^8  (mm^3) (Sec 3-3)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  317.780) = 7.129 x 10^8  (mm^3) (Sec 4-4)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  255.080) = 7.788 x 10^8  (mm^3) (Sec 5-5)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  245.960) = 7.884 x 10^8  (mm^3) (Sec 6-6)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  245.960) = 7.884 x 10^8  (mm^3) (Sec 7-7)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  245.960) = 7.884 x 10^8  (mm^3) (Sec 8-8)");
                //sw.WriteLine(@"        = 1.050E+006 x ( 996.818 -  245.960) = 7.884 x 10^8  (mm^3) (Sec 9-9)");
                //sw.WriteLine();

                
                for (int i = 0; i < Ayp.Count; i++)
                {

                    if (i == 0)
                    {
                        sw.WriteLine(@"Ayp     = {0:E3} x ({1:F3} - {2:F3}) = {3:E3}  (mm^3) (Sec {4}-{4})",
                            end_sections.A, composit_end_sections.Yt, Cable_total[i], Ayp[i], (i + 1));
                    }
                    else if (i == 1)
                    {

                        sw.WriteLine(@"        = {0:E3} x ({1:F3} - {2:F3}) = {3:E3}  (mm^3) (Sec {4}-{4})",
                            end_sections.A, composit_end_sections.Yt, Cable_total[i], Ayp[i], (i + 1));
                    }
                    else
                    {

                        sw.WriteLine(@"        = {0:E3} x ({1:F3} - {2:F3}) = {3:E3}  (mm^3) (Sec {4}-{4})",
                            mid_sections.A, composit_mid_sections.Yt, Cable_total[i], Ayp[i], (i + 1));
                    }
                }

                //sw.WriteLine(@"Ayc     = 2.280E+006 x (1101.316 - 1459.680) = 8.171 x 10^8  (mm^3)(Sec 1-1)");
                //sw.WriteLine(@"        = 2.280E+006 x (1101.316 -  907.060) = 4.429 x 10^8  (mm^3) (Sec 2-2)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  501.380) = 8.175 x 10^8  (mm^3) (Sec 3-3)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  317.780) = 11.204 x 10^8  (mm^3) (Sec 4-4)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  255.080) = 12.239 x 10^8  (mm^3) (Sec 5-5)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  245.960) = 12.389 x 10^8  (mm^3) (Sec 6-6)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  245.960) = 12.389 x 10^8  (mm^3) (Sec 7-7)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  245.960) = 12.389 x 10^8  (mm^3) (Sec 8-8)");
                //sw.WriteLine(@"        = 1.650E+006 x ( 996.818 -  245.960) = 12.389 x 10^8  (mm^3) (Sec 9-9)");
                sw.WriteLine();
                for (int i = 0; i < Ayc.Count; i++)
                {

                    if (i == 0)
                    {
                        sw.WriteLine(@"Ayc     = {0:E3} x ({1:F3} - {2:F3}) = {3:E3}  (mm^3) (Sec {4}-{4})",
                            composit_end_sections.A, composit_end_sections.Yt, Cable_total[i], Ayc[i], (i + 1));
                    }
                    else if (i == 1)
                    {

                        sw.WriteLine(@"        = {0:E3} x ({1:F3} - {2:F3}) = {3:E3}  (mm^3) (Sec {4}-{4})",
                            composit_end_sections.A, composit_end_sections.Yt, Cable_total[i], Ayc[i], (i + 1));
                    }
                    else
                    {

                        sw.WriteLine(@"        = {0:E3} x ({1:F3} - {2:F3}) = {3:E3}  (mm^3) (Sec {4}-{4})",
                            composit_mid_sections.A, composit_mid_sections.Yt, Cable_total[i], Ayp[i], (i + 1));
                    }
                }
                //sw.WriteLine(@"fcp = p/A = 9697.545x1000.0/2.280E+006 = 4.250 N/Sq.mm (Sec 1-1 & 2-2)");
                sw.WriteLine();

                sw.WriteLine(@"fcp = p/A = {0:f3} x 1000.0 / {1:E3} = {2:f3} N/Sq.mm (Sec 1-1 & 2-2)",
                    p, composit_end_sections.A, fcp.F1);
                sw.WriteLine();
                //sw.WriteLine(@"    = p/A = 9697.545x1000.0/1.650E+006 = 5.877 N/Sq.mm (Sec 3-3 to 9-9)");
                //sw.WriteLine(@"");
                sw.WriteLine(@"    = p/A = {0:f3} x 1000.0 / {1:E3} = {2:f3} N/Sq.mm (Sec 3-3 to 9-9)",
                    p, composit_mid_sections.A, fcp.F9);
                sw.WriteLine();

                //sw.WriteLine(@"b = Web width (mm) = 700                  700          350          350          350          350          350          350          350");
                sw.WriteLine(@"b = Web width (mm) = {0:f3}   {1:f3}   {2:f3}   {3:f3}   {4:f3}   {5:f3}   {6:f3}   {7:f3}   {8:f3}",
                psc_b[0], psc_b[1], psc_b[2], psc_b[3], psc_b[4], psc_b[5], psc_b[6], psc_b[7], psc_b[8]);
                sw.WriteLine();

                sw.WriteLine(@"fs     = Vc1 x Ayp / (Ip x b)");
                for (int i = 0; i < Ayc.Count; i++)
                {
                    sw.WriteLine(@"       = {0:f3} x 1000.0 x {1:E3} / ({2:E3} x {3:F3}) = {4:E3} (Sec {5}-{5})",
                        Vc1[i], Ayp[i], Ip[i], psc_b[i], fs[i], (i + 1));
                }
                //sw.WriteLine(@"fs     = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 6.021 x 10^8 / (8.064E+011 x 700.0) = 0.346 (Sec 1-1)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 3.264 x 10^8 / (8.064E+011 x 700.0) = 0.188 (Sec 2-2)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 5.202 x 10^8 / (6.290E+011 x 350.0) = 0.767 (Sec 3-3)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 7.129 x 10^8 / (6.290E+011 x 350.0) = 1.051 (Sec 4-4)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 7.788 x 10^8 / (6.290E+011 x 350.0) = 1.148 (Sec 5-5)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 7.884 x 10^8 / (6.290E+011 x 350.0) = 1.162 (Sec 6-6)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 7.884 x 10^8 / (6.290E+011 x 350.0) = 1.162 (Sec 7-7)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 7.884 x 10^8 / (6.290E+011 x 350.0) = 1.162 (Sec 8-8)");
                //sw.WriteLine(@"       = Vc1 x Ayp / (Ip x b) = 324.400 x 1000.0 x 7.884 x 10^8 / (6.290E+011 x 350.0) = 1.162 (Sec 9-9)");
                //sw.WriteLine(@"");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(@"ft = maximum principal tensile stress = 0.24 x sqrt(fck) = 0.24 x sqrt({0}) = {1:f3} N/Sq.mm (Sec 1-1 to 9-9)", fck, ft.F1);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(@"Vc2 = (Ic x b) x [Sqrt(ftxft + fcp x ft) - ft] / Ayc ");
                for (int i = 0; i < Vc2.Count; i++)
                {
                    sw.WriteLine(@"    = {0:E3} x {1} x [Sqrt({2:f3} x {2:f3} + {3:f3} x {2:f3}) - {2:f3}] / {4:E3} = {5:E3} N (Sec {6}-{6})",
                        Ic[i], psc_b[i], ft[i], fcp[i], Ayc[i], Vc2[i], (i + 1));
                }
                //sw.WriteLine(@"Vc2 = (Ic x b) x [Sqrt(ftxft + fcp x ft) - ft] / Ayc ");
                //sw.WriteLine(@"    = 1.580E+012 x 700 x [Sqrt(1.61 x 1.61 + 4.250 x 1.61) - 1.61] / 8.171 x 10^8 = 1.978 x 10^6 (Sec 1-1)");
                //sw.WriteLine(@"    = 1.580E+012 x 700 x [Sqrt(1.61 x 1.61 + 4.250 x 1.61) - 1.61] / 4.429 x 10^8 = 6.985 x 10^6 (Sec 2-2)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 8.175 x 10^8 = 1.862 x 10^6 (Sec 3-3)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 11.204 x 10^8 = 1.359 x 10^6 (Sec 4-4)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 12.239 x 10^8 = 1.244 x 10^6 (Sec 5-5)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 12.239 x 10^8 = 1.244 x 10^6 (Sec 6-6)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 12.239 x 10^8 = 1.244 x 10^6 (Sec 7-7)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 12.239 x 10^8 = 1.244 x 10^6 (Sec 8-8)");
                //sw.WriteLine(@"    = 1.346E+012 x 350 x [Sqrt(1.61 x 1.61 + 5.877 x 1.61) - 1.61] / 12.239 x 10^8 = 1.244 x 10^6 (Sec 9-9)");
                sw.WriteLine(@"");
                //sw.WriteLine(@"324.40  297.500        103.980          127.820  71.900  49.310  28.070  20.976  5.034");
                sw.WriteLine(@"");
                sw.WriteLine(@"Vco     = Vc1 + Vc2  ");
                for (int i = 0; i < Vc2.Count; i++)
                {
                    sw.WriteLine("        {0,36}", (string.Format(@"= {0:f3} + {1:f3} = {2:F3} kN (Sec {3}-{3})",
                        Vc1[i], (Vc2[i] / 1000.0), Vco[i], (i + 1))));
                }
                //sw.WriteLine(@"        = 324.400 + 1978 = 2303 KN (Sec 1-1)");
                //sw.WriteLine(@"        = 297.500 + 6985 = 7283 KN (Sec 2-2)");
                //sw.WriteLine(@"        = 103.980 + 1862 = 1966 KN (Sec 3-3)");
                //sw.WriteLine(@"        = 127.820 + 1359 = 1487 KN (Sec 4-4)");
                //sw.WriteLine(@"        =  71.900 + 1244 = 1316 KN (Sec 5-5)");
                //sw.WriteLine(@"        =  49.310 + 1244 = 1294 KN (Sec 6-6)");
                //sw.WriteLine(@"        =  28.070 + 1244 = 1272 KN (Sec 7-7)");
                //sw.WriteLine(@"        =  20.976 + 1244 = 1265 KN (Sec 8-8)");
                //sw.WriteLine(@"        =   5.034 + 1244 = 1249 KN (Sec 9-9)");
                sw.WriteLine(@"");
                sw.WriteLine(@"db = Larger of(0.8xD & D-CG)");
                for (int i = 0; i < db.Count; i++)
                {
                    //sw.WriteLine(@"Larger of(0.8x2400 = 1920.0 & 2400.0-1459.68 = 940.32) = 1920.0 mm.");

                    sw.WriteLine(@"     Larger of(0.8x{0:f3} = {1:f3} & {0:f3}-{2:f3} = {3:f3}) = {4:f3} mm. (Sec {5}-{5})",
                        D, (0.8 * D), psc_CG[i], (D - psc_CG[i]), db[i], (i+1));
                   
                }

                //sw.WriteLine(@"db = Larger of(0.8xD & D-CG)    = Larger of(0.8x2400 = 1920.0 & 2400.0-1459.68 = 940.32) = 1920.0 mm.");
                //sw.WriteLine(@"                                = Larger of(0.8x2400 = 1920.0 & 2400.0-907.06 = 1492.94) = 1920.0 mm.");
                //sw.WriteLine(@"                                = Larger of(0.8x2400 = 1920.0 & 2400.0-501.38 = 1898.62) = 1920.0 mm.");
                //sw.WriteLine(@"                                = Larger of(0.8x2400 = 1920.0 & 2400.0-317.78 = 2082.22) = 2082.2 mm.");
                //sw.WriteLine(@"                                = Larger of(0.8x2400 = 1920.0 & 2400.0-255.08 = 2144.92) = 2144.9 mm.");
                //sw.WriteLine(@"                                = Larger of(0.8x2400 = 1920.0 & 2400.0-245.96 = 2154.04) = 2154.9 mm.");
                //sw.WriteLine(@"= Larger of(0.8x2400 = 1920.0 & 2400.0-245.96 = 2154.04) = 2154.9 mm.");
                //sw.WriteLine(@"= Larger of(0.8x2400 = 1920.0 & 2400.0-245.96 = 2154.04) = 2154.9 mm.");
                //sw.WriteLine(@"= Larger of(0.8x2400 = 1920.0 & 2400.0-245.96 = 2154.04) = 2154.9 mm.");
                sw.WriteLine(@"");
                //sw.WriteLine(@"p = 9697.545 kN");
                sw.WriteLine(@"p = {0:f3} kN", p);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"V (capacity) = (5 x b x db) x 1000 + P x Sin(theta1)  for Uncracked Sections");
                sw.WriteLine();
                sw.WriteLine(@"V (capacity) = (5 x b x db) x 1000  kN");
                sw.WriteLine();
                for (int i = 0; i < V.Count; i++)
                {
                    //sw.WriteLine(@"             = (5 x 0.700 x 1.920) + 9697.545 x sin(61)  = 8488.384 kN (1-1)");
                    //sw.WriteLine(@"             = (5 x {0:f3} x {1:f3}) + {2:f3} x sin({3:f0})  = {4:f3} kN ({5}-{5})",
                    //    (psc_b[i] / 1000.0), (db[i] / 1000.0), p, cumulative_theta[i], V[i], (i + 1));


                    sw.WriteLine(@"             = (5 x {0:f3} x {1:f3}) x 1000.0 = {2:f3} kN ({3}-{3})",
                        (psc_b[i] / 1000.0), (db[i] / 1000.0), V[i], (i + 1));
                }
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta1) = (5 x 0.700 x 1.920) + 9697.545 x sin(61)  = 8488.384 kN (1-1)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta2) = (5 x 0.700 x 1.920) + 9697.545 x sin(20)  = 3323.476 kN (2-2)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta3) = (5 x 0.350 x 1.920) + 9697.545 x sin(17)  = 2838.648 kN (3-3)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta4) = (5 x 0.350 x 2.082) + 9697.545 x sin(6.5) = 1101.375 kN (4-4)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta5) = (5 x 0.350 x 2.144) + 9697.545 x sin(4.0) =  680.236 kN (5-5)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta6) = (5 x 0.350 x 2.154) + 9697.545 x sin(4.0) =  680.236 kN (6-6)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta7) = (5 x 0.350 x 2.154) + 9697.545 x sin(4.0) =  680.236 kN (7-7)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta8) = (5 x 0.350 x 2.154) + 9697.545 x sin(4.0) =  680.236 kN (8-8)");
                //sw.WriteLine(@"V (capacity) = (5 x b x db) + P x Sin(theta9) = (5 x 0.350 x 2.154) + 9697.545 x sin(4.0) =  680.236 kN (9-9)");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"Table  7: COMPUTATION OF SHEAR FORCES");
                sw.WriteLine(@"-------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"Refer to TABLE 13 for Maximum Shear Stress at the end of this Report.");
                sw.WriteLine(@"");

                sw.WriteLine();
                sw.WriteLine(("-----------------------------------------------------------------------------------------------------------------------"));
                sw.WriteLine(sec_format, "Sections", "1-1  ", "2-2  ", "3-3  ", "4-4  ", "5-5   ", "6-6  ", "7-7  ", "8-8  ", "9-9  ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
               
                if (Is_Inner_Girder)
                {

                    sw.WriteLine(sec_format, "Mpc",
                                moment_forces.LL_INNER_GIRDER.F1 + moment_forces.DL_INNER_GIRDER.F1,
                                moment_forces.LL_INNER_GIRDER.F2 + moment_forces.DL_INNER_GIRDER.F2,
                                moment_forces.LL_INNER_GIRDER.F3 + moment_forces.DL_INNER_GIRDER.F3,
                                moment_forces.LL_INNER_GIRDER.F4 + moment_forces.DL_INNER_GIRDER.F4,
                                moment_forces.LL_INNER_GIRDER.F5 + moment_forces.DL_INNER_GIRDER.F5,
                                moment_forces.LL_INNER_GIRDER.F6 + moment_forces.DL_INNER_GIRDER.F6,
                                moment_forces.LL_INNER_GIRDER.F7 + moment_forces.DL_INNER_GIRDER.F7,
                                moment_forces.LL_INNER_GIRDER.F8 + moment_forces.DL_INNER_GIRDER.F8,
                                moment_forces.LL_INNER_GIRDER.F9 + moment_forces.DL_INNER_GIRDER.F9);
                    sw.WriteLine(sec_format, "Vc1",
                                shear_forces.LL_INNER_GIRDER.F1 + shear_forces.DL_INNER_GIRDER.F1,
                                shear_forces.LL_INNER_GIRDER.F2 + shear_forces.DL_INNER_GIRDER.F2,
                                shear_forces.LL_INNER_GIRDER.F3 + shear_forces.DL_INNER_GIRDER.F3,
                                shear_forces.LL_INNER_GIRDER.F4 + shear_forces.DL_INNER_GIRDER.F4,
                                shear_forces.LL_INNER_GIRDER.F5 + shear_forces.DL_INNER_GIRDER.F5,
                                shear_forces.LL_INNER_GIRDER.F6 + shear_forces.DL_INNER_GIRDER.F6,
                                shear_forces.LL_INNER_GIRDER.F7 + shear_forces.DL_INNER_GIRDER.F7,
                                shear_forces.LL_INNER_GIRDER.F8 + shear_forces.DL_INNER_GIRDER.F8,
                                shear_forces.LL_INNER_GIRDER.F9 + shear_forces.DL_INNER_GIRDER.F9);

                    sw.WriteLine();
                    sec_format = sec_format.ToUpper().Replace(":F2", ":F3");
                    sec_format = sec_format.ToUpper().Replace(":F", ":E");
                    sw.WriteLine(sec_format, "Ip (mm^4)",
                                end_sections.Izz,
                                end_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz);
                    sw.WriteLine(sec_format, "Ic (mm^4)",
                                composit_end_sections.Izz,
                                composit_end_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz);

                    sw.WriteLine(sec_format, "Ayp (mm^3)",
                                Ayp.F1,
                                Ayp.F2,
                                Ayp.F3,
                                Ayp.F4,
                                Ayp.F5,
                                Ayp.F6,
                                Ayp.F7,
                                Ayp.F8,
                                Ayp.F9);

                    sw.WriteLine(sec_format, "Ayc (mm^3)",
                                Ayc.F1,
                                Ayc.F2,
                                Ayc.F3,
                                Ayc.F4,
                                Ayc.F5,
                                Ayc.F6,
                                Ayc.F7,
                                Ayc.F8,
                                Ayc.F9);
                    sw.WriteLine(sec_format, "fs       ",
                                fs.F1,
                                fs.F2,
                                fs.F3,
                                fs.F4,
                                fs.F5,
                                fs.F6,
                                fs.F7,
                                fs.F8,
                                fs.F9);
                    sw.WriteLine();
                    sec_format = sec_format.ToUpper().Replace(":E", ":F");
                    sw.WriteLine(sec_format, "Fcp       ",
                                fcp.F1,
                                fcp.F2,
                                fcp.F3,
                                fcp.F4,
                                fcp.F5,
                                fcp.F6,
                                fcp.F7,
                                fcp.F8,
                                fcp.F9);

                    sw.WriteLine(sec_format, "b       ",
                                psc_b.F1,
                                psc_b.F2,
                                psc_b.F3,
                                psc_b.F4,
                                psc_b.F5,
                                psc_b.F6,
                                psc_b.F7,
                                psc_b.F8,
                                psc_b.F9);


                    sec_format = sec_format.ToUpper().Replace(":E", ":F");

                    sw.WriteLine(sec_format, "ft       ",
                                ft.F1,
                                ft.F2,
                                ft.F3,
                                ft.F4,
                                ft.F5,
                                ft.F6,
                                ft.F7,
                                ft.F8,
                                ft.F9);
                    sw.WriteLine(sec_format, "Vc2       ",
                                Vc2.F1/1000.0,
                                Vc2.F2/1000.0,
                                Vc2.F3/1000.0,
                                Vc2.F4/1000.0,
                                Vc2.F5/1000.0,
                                Vc2.F6/1000.0,
                                Vc2.F7/1000.0,
                                Vc2.F8/1000.0,
                                Vc2.F9/1000.0);
                    sec_format = sec_format.ToUpper().Replace(":E", ":F");
                    sw.WriteLine(sec_format, "Vco       ",
                                Vco.F1,
                                Vco.F2,
                                Vco.F3,
                                Vco.F4,
                                Vco.F5,
                                Vco.F6,
                                Vco.F7,
                                Vco.F8,
                                Vco.F9);
                    //sec_format = sec_format.ToUpper().Replace(":F", ":E");
                    sw.WriteLine(sec_format, "Db       ",
                                db.F1,
                                db.F2,
                                db.F3,
                                db.F4,
                                db.F5,
                                db.F6,
                                db.F7,
                                db.F8,
                                db.F9);

                    //pfd.F1 = 
                }
                else
                {

                    sw.WriteLine(sec_format, "Mpc",
                                moment_forces.LL_OUTER_GIRDER.F1 + moment_forces.DL_OUTER_GIRDER.F1,
                                moment_forces.LL_OUTER_GIRDER.F2 + moment_forces.DL_OUTER_GIRDER.F2,
                                moment_forces.LL_OUTER_GIRDER.F3 + moment_forces.DL_OUTER_GIRDER.F3,
                                moment_forces.LL_OUTER_GIRDER.F4 + moment_forces.DL_OUTER_GIRDER.F4,
                                moment_forces.LL_OUTER_GIRDER.F5 + moment_forces.DL_OUTER_GIRDER.F5,
                                moment_forces.LL_OUTER_GIRDER.F6 + moment_forces.DL_OUTER_GIRDER.F6,
                                moment_forces.LL_OUTER_GIRDER.F7 + moment_forces.DL_OUTER_GIRDER.F7,
                                moment_forces.LL_OUTER_GIRDER.F8 + moment_forces.DL_OUTER_GIRDER.F8,
                                moment_forces.LL_OUTER_GIRDER.F9 + moment_forces.DL_OUTER_GIRDER.F9);
                    sw.WriteLine(sec_format, "Vc1",
                                shear_forces.LL_OUTER_GIRDER.F1 + shear_forces.DL_OUTER_GIRDER.F1,
                                shear_forces.LL_OUTER_GIRDER.F2 + shear_forces.DL_OUTER_GIRDER.F2,
                                shear_forces.LL_OUTER_GIRDER.F3 + shear_forces.DL_OUTER_GIRDER.F3,
                                shear_forces.LL_OUTER_GIRDER.F4 + shear_forces.DL_OUTER_GIRDER.F4,
                                shear_forces.LL_OUTER_GIRDER.F5 + shear_forces.DL_OUTER_GIRDER.F5,
                                shear_forces.LL_OUTER_GIRDER.F6 + shear_forces.DL_OUTER_GIRDER.F6,
                                shear_forces.LL_OUTER_GIRDER.F7 + shear_forces.DL_OUTER_GIRDER.F7,
                                shear_forces.LL_OUTER_GIRDER.F8 + shear_forces.DL_OUTER_GIRDER.F8,
                                shear_forces.LL_OUTER_GIRDER.F9 + shear_forces.DL_OUTER_GIRDER.F9);
                    sec_format = sec_format.ToUpper().Replace(":F", ":E");
                    sw.WriteLine(sec_format, "Ip (mm^4)",
                                end_sections.Izz,
                                end_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz,
                                mid_sections.Izz);
                    sw.WriteLine(sec_format, "Ic (mm^4)",
                                composit_end_sections.Izz,
                                composit_end_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz,
                                composit_mid_sections.Izz);
                }
                sw.WriteLine(("-----------------------------------------------------------------------------------------------------------------------"));
               
                //sw.WriteLine(@"");
                //sw.WriteLine(@"--------------------------------------------------------------------------------------------------------------");
                //sw.WriteLine(@"Sections    1-1        2-2        3-3        4-4        5-5        6-6        7-7        8-8        9-9");
                //sw.WriteLine(@"--------------------------------------------------------------------------------------------------------------");
                //sw.WriteLine(@"Mpc         429.90     582.10    1130.50    1382.80    1456.10    1583.80    1478.70    1499.00    1485.20");
                //sw.WriteLine(@"Vc1         324.40     297.50     103.98     127.82      71.90      49.31      28.07      20.98       5.03");
                //sw.WriteLine(@"Ip (mm^4)   8.06E+011  8.06E+011  6.29E+011  6.29E+011  6.29E+011  6.29E+011  6.29E+011  6.29E+011  6.29E+011");
                //sw.WriteLine(@"Ic (mm^4)   1.58E+012  1.58E+012  1.35E+012  1.35E+012  1.35E+012  1.35E+012  1.35E+012  1.35E+012  1.35E+012");
                //sw.WriteLine(@"Ayp (mm^3)  1.85E+009  1.85E+009  1.05E+009  1.05E+009  1.05E+009  1.05E+009  1.05E+009  1.05E+009  1.05E+009");
                //sw.WriteLine(@"Ayc (mm^3)  2.51E+009  2.51E+009  1.64E+009  1.64E+009  1.64E+009  1.64E+009  1.64E+009  1.64E+009  1.64E+009");
                //sw.WriteLine(@"Fcp             4.250           4.250      5.877      5.877      5.877     5.877      5.877      5.877      5.877");
                //sw.WriteLine(@"b             700.0           700.0      350.0           350.0      350.0     350.0      350.0      350.0      350.0");
                //sw.WriteLine(@"fs          0.346           0.188      0.767      1.051      1.148     1.162      1.162      1.162      1.162  ");
                //sw.WriteLine(@"ft             1.61           1.61       1.61       1.61              1.61      1.61       1.61       1.61       1.61");
                //sw.WriteLine(@"Vc2           1.978E+06   6.985E+06  1.862E+06  1.359E+06  1.244E+06   1.244E+06  1.244E+06  1.244E+06  1.244E+06  ");
                //sw.WriteLine(@"Vco         2303.0      7283.0     1966.0    1487.0      1316.0     1294.0     1272.0    1265.0     1249.0");
                //sw.WriteLine(@"Db             1920.0      1920.0    1920.0     2082.2     2144.9     2154.9     2154.9     2154.9     2154.9");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"--------------------------------");
                sw.WriteLine(@"Table  8: CHECK FOR SHEAR STRESS");
                sw.WriteLine(@"--------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(("-----------------------------------------------------------------------------------------------------------------------"));
                sw.WriteLine(sec_format, "Sections", "1-1  ", "2-2  ", "3-3  ", "4-4  ", "5-5   ", "6-6  ", "7-7  ", "8-8  ", "9-9  ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "V_ult kN", Vc1.F1, Vc1.F2,Vc1.F3, Vc1.F4, Vc1.F5, Vc1.F6, Vc1.F7, Vc1.F8, Vc1.F9);
                //sw.WriteLine(@"V_ult kN        324.40     297.50    103.98     127.82     71.90      49.31      28.07      20.98       5.03");
                sw.WriteLine(@"");
                sw.WriteLine(sec_format, "V_capacity kN", V.F1, V.F2, V.F3, V.F4, V.F5, V.F6, V.F7, V.F8, V.F9);
                //sw.WriteLine(@"V_capacity kN   8488.384   3323.476  2838.648   1101.375   680.236    680.236    680.236    680.236    680.236");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(sec_format, "Remarks",
                    (Vc1.F1 < Math.Abs(V.F1)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F2 < Math.Abs(V.F2)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F3 < Math.Abs(V.F3)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F4 < Math.Abs(V.F4)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F5 < Math.Abs(V.F5)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F6 < Math.Abs(V.F6)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F7 < Math.Abs(V.F7)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F8 < Math.Abs(V.F8)) ? "Safe   " : "Unsafe  ",
                    (Vc1.F9 < Math.Abs(V.F9)) ? "Safe   " : "Unsafe  ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine();

                #endregion

                #region STEP 12 : DESIGN OF SHEAR REINFORCEMENTS


                sw.WriteLine(@"----------------------------------------");
                sw.WriteLine(@"STEP 12 : DESIGN OF SHEAR REINFORCEMENTS");
                sw.WriteLine(@"----------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"User’s Input data ");
                sw.WriteLine(@"");
                sw.WriteLine(@"Asv_dia = {0} mm.", prd.DSR_Asv_dia);
                sw.WriteLine(@"Asv_spacing = {0} mm.", prd.DSR_Asv_spacing);
                sw.WriteLine(@"Asv_Legs_End_span = 4 for Sec (1-1 & 2-2)", prd.DSR_Asv_Legs_End_span);
                sw.WriteLine(@"Asv_Legs_Mid_span = 2 for Sec (3-3 to 9-9)", prd.DSR_Asv_Legs_Mid_span);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"Asv_required = Nominal Shear Reinforcement (As safe),");
                sw.WriteLine(@"");

                double Asv_provided_end = (Math.PI * prd.DSR_Asv_dia * prd.DSR_Asv_dia / 4) * prd.DSR_Asv_Legs_End_span * (1000.0 / prd.DSR_Asv_spacing);

                sw.WriteLine(@"Asv_provided = (3.1416 x Asv_dia x Asv_dia / 4) x Asv_Legs_End_span x (1000.0/Asv_spacing)");
                sw.WriteLine(@"             = (3.1416 x {0} x {0} / 4) x {1} x (1000.0/{2:f3}) = {3:f3} Sq.mm. for Sec (1-1 & 2-2)",
                    prd.DSR_Asv_dia , prd.DSR_Asv_Legs_End_span ,  prd.DSR_Asv_spacing, Asv_provided_end);
 
                sw.WriteLine(@"");
                double Asv_provided_mid = (Math.PI * prd.DSR_Asv_dia * prd.DSR_Asv_dia / 4) * prd.DSR_Asv_Legs_Mid_span * (1000.0 / prd.DSR_Asv_spacing);

                sw.WriteLine(@"Asv_provided = (3.1416 x Asv_dia x Asv_dia / 4) x Asv_Legs_Mid_span x (1000.0/Asv_spacing)");
                //sw.WriteLine(@"             = (3.1416 x 10 x 10 / 4) x 2 x (1000.0/200) = 785.398 Sq.mm. for Sec (3-3 to 9-9)");
                sw.WriteLine(@"             = (3.1416 x {0} x {0} / 4) x {1} x (1000.0/{2:f3}) = {3:f3} Sq.mm. for Sec (3-3 to 9-9)",
                   prd.DSR_Asv_dia, prd.DSR_Asv_Legs_Mid_span, prd.DSR_Asv_spacing, Asv_provided_mid);
 
                sw.WriteLine(@"");
                //sw.WriteLine(@"In the End Span, from end to section 2-2, Provide 10 mm. dia, 4 legged, bars @ 200 mm C/C,");
                sw.WriteLine(@"In the End Span, from end to section 2-2, Provide {0} mm. dia, {1} legged, bars @ {2} mm C/C,",
                    prd.DSR_Asv_dia, prd.DSR_Asv_Legs_End_span, prd.DSR_Asv_spacing);
                sw.WriteLine(@"");
                sw.WriteLine(@"In the Mid Span, from from section 3-3 to 9-9, Provide {0} mm. diameter, {1} legged, bars @ {2} mm C/C,",
                                       prd.DSR_Asv_dia, prd.DSR_Asv_Legs_Mid_span, prd.DSR_Asv_spacing);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();


                #endregion

                #region STEP 13 : DESIGN OF REINFORCEMENTS FOR BURSTING TENSILE FORCE



                sw.WriteLine();
                sw.WriteLine(@"-------------------------------------------------------------");
                sw.WriteLine(@"STEP 13 : DESIGN OF REINFORCEMENTS FOR BURSTING TENSILE FORCE");
                sw.WriteLine(@"-------------------------------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"User’s Input data ");
                sw.WriteLine(@"");
                sw.WriteLine(@"At the face of Anchorage Cone,");
                sw.WriteLine(@"");
                sw.WriteLine(@"Ast_dia = {0} mm.", prd.DRBTF_Ast_dia);
                sw.WriteLine(@"Ast_Nos = {0}", prd.DRBTF_Ast_Nos);
                sw.WriteLine(@"Ast_Layers = {0}", prd.DRBTF_Ast_Layers);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                double Yo = end_sections.b2 / 2.0;
                sw.WriteLine(@"2 X Yo = {0:f3} mm. (Web thickness at End Span)", end_sections.b2);
                sw.WriteLine(@"Yo = {0:f3} mm.", Yo);
                sw.WriteLine(@"");
                double Ypo = mid_sections.b2 / 2.0;
                sw.WriteLine(@"2 x YPo = {0: mm. (Web thickness at Mid Span)");
                sw.WriteLine(@"YPo = 175 mm.");
                sw.WriteLine(@"");
                sw.WriteLine(@"Pk = {0:f3} kN (Force at anchorage per cable)", Pk);
                sw.WriteLine(@"");
                double Ypo_Yo = Ypo / Yo;
                sw.WriteLine(@"(YPo/Yo) = {0:f3}/{1:f3} = {2:f3}, ", Ypo, Yo, Ypo_Yo);
                sw.WriteLine(@"");
                string ref_str = "";
                double F_bst_Pk = iApp.Tables.Bursting_Tensile_Force(Ypo_Yo, ref ref_str);


                //sw.WriteLine(@"(F_bst/Pk) = 0.17 (Ref. TABLE 10,  at the end of the report, for Corresponding value)");
                sw.WriteLine(@"(F_bst/Pk) = {0} (Ref. TABLE 10,  at the end of the report, for Corresponding value)", F_bst_Pk);

                sw.WriteLine(@"");
                //sw.WriteLine(@"F_bst = 0.17 x 2670.000 = 454.000 kN");
                double F_bst = F_bst_Pk * Pk;
                sw.WriteLine(@"F_bst = {0:f3} x {1:f3} = {2:f3} kN", F_bst_Pk, Pk, F_bst);
                sw.WriteLine(@"");
                double per_ts = 0.87 * fy;
                sw.WriteLine(@"Permissible Tensile Stress in HYSD Steel Reinforcement bars = 0.87 x fy = 0.87 x {0:f3} = {1:f3} N/Sq.mm", fy, per_ts);

                sw.WriteLine(@"");
                double Ast_Required = F_bst * 1000.0 / per_ts;
                //sw.WriteLine(@"Ast_Required = (454.000 x 1000.000 / 435.000) 1043.5 Sq. mm");
                sw.WriteLine(@"Ast_Required = ({0:f3} x 1000.000 / {1:f3}) = {2:f3} Sq. mm", F_bst, per_ts, Ast_Required);
                sw.WriteLine(@"");
                double prov_rinf = (Math.PI * prd.DRBTF_Ast_dia * prd.DRBTF_Ast_dia / 4.0) * prd.DRBTF_Ast_Nos * prd.DRBTF_Ast_Layers;
                sw.WriteLine(@"Provide Reinforcement = (3.1416 x Ast_dia x Ast_dia / 4) x Ast_Nos x Ast_Layers ");
                //sw.WriteLine(@"                      = (3.1416 x 16 x 16 / 4) x 4 x 4");
                sw.WriteLine(@"                      = (3.1416 x {0} x {0} / 4) x {1} x {2}", prd.DRBTF_Ast_dia, prd.DRBTF_Ast_Nos, prd.DRBTF_Ast_Layers);
                sw.WriteLine(@"                      = {0:f3} Sq. mm", prov_rinf);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");


                #endregion

                #region STEP 14 : DESIGN OF SHEAR CONNECTORS


                sw.WriteLine(@"------------------------------------");
                sw.WriteLine(@"STEP 14 : DESIGN OF SHEAR CONNECTORS");
                sw.WriteLine(@"------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"For Composite Section at End Span:");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"|<------- 2.400 M.--------->|");
                sw.WriteLine(@"--------------------------- ----");
                sw.WriteLine(@"|                          | 0.25 M.");
                sw.WriteLine(@"|__________________________|----");
                sw.WriteLine(@"           |     |");
                sw.WriteLine(@"           |     |            0.85 M.");
                sw.WriteLine(@"           |     |");
                sw.WriteLine(@"           |     |------------- N-A");
                sw.WriteLine(@"    2.4 M. |     |  Neutral Axis");
                sw.WriteLine(@"           |     |");
                sw.WriteLine(@"           |     |            1.55 M.");
                sw.WriteLine(@"           |     |");
                sw.WriteLine(@"           |     |");
                sw.WriteLine(@"    -----  ------  ------------");
                sw.WriteLine(@"           | 0.7 |");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"User’s Input Data:");
                sw.WriteLine(@"");
                sw.WriteLine(@"Ash_dia = {0} mm.", prd.DSC_Ash_dia);
                sw.WriteLine(@"Ash_Leg = {0}", prd.DSC_Ash_Leg);
                sw.WriteLine(@"Ash_Spacing = {0} mm.", prd.DSC_Ash_Spacing);
                sw.WriteLine(@"Steel Grade = Fe {0} (fy={0} N/Sq.mm)", prd.DSC_fy);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                //    sw.WriteLine(@"Refer to Cl. 608.2.2, IRC 22, 1986, ");

                 

                sw.WriteLine(@"");

                double y_dash = Cable_total.F1 / 1000.0;

                sw.WriteLine(@"Y’ = Distance of Neutral Axis N-A from bottom of Girder");
                sw.WriteLine(@"   = {0:f3} m. (CG for Section (1-1) = {1:f3} mm = {1:f3} m.from STEP 3)", y_dash, Cable_total.F1);
                sw.WriteLine(@"");

                double hc = (composit_end_sections.H / 1000.0) - y_dash;
                sw.WriteLine(@"hc = Height of Compression zone is height of Girder above N-A = H - Y’ = {0:f3} - {1:f3} = {2:f3} m.",
                    composit_end_sections.H, y_dash, hc);
                sw.WriteLine(@"");
                double eb = composit_end_sections.b2 / 1000.0;
                sw.WriteLine(@"eb = Web width at End span = {0:f3} m.", eb);
                sw.WriteLine(@"");
                Ac = eb * hc;
                sw.WriteLine(@"Ac = Area of Compression zone = eb x hc = {0:f3} x {1:f3} = {2:f3} Sq. m", eb, hc, Ac);

                //sw.WriteLine(@"Ac = Area of Compression zone = eb x hc = 0.7 x 0.85 = 0.595 Sq. m = 0.6 Sq. m.");
                sw.WriteLine(@"");
                double Y = hc / 2.0 + ((composit_end_sections.ds / 1000.0) / 2.0);

                //sw.WriteLine(@"Y = CG of Area under Compression = (0.85/2.0) + (0.25/2.0) = 0.976 m.");
                sw.WriteLine(@"Y = CG of Area under Compression = ({0:f3}/2.0) + ({1:f3}/2.0) = {2:f3} m.",
                    hc, (composit_end_sections.ds / 1000.0), Y);
                sw.WriteLine(@"");

                double W = composit_end_sections.W / 1000.0;
                sw.WriteLine(@"W = Width of Deck Slab in composite section = {0:f3} m.", W);
                sw.WriteLine(@"");
                double ds = composit_end_sections.ds / 1000.0;
                sw.WriteLine(@"ds = Thickness of Deck Slab = {0:f3} m.", ds);
                sw.WriteLine();
                I = ((eb * hc * hc * hc / 12) + eb * hc * Y * Y) + ((W * ds * ds * ds / 12) + W * ds * Y * Y);

                sw.WriteLine(@"Moment of Inertia =  I");
                sw.WriteLine();
                sw.WriteLine(@"   I = [(eb x hc^3 / 12) + eb x hc x Y^2] + [(W x ds^3 / 12) + W x ds x Y^2]");
                sw.WriteLine(@"     = [({0:f3} x {1:f3}^3 / 12) + {0:f3} x {1:f3} x {2:f3}^2] + [({3:f3} x {4:f3}^3 / 12) + {3:f3}  x {4:f3} x {2:f3}^2]",
                                                        eb, hc, Y, W, ds);
                //sw.WriteLine(@"                       = 0.6026 + 0.5747");
                sw.WriteLine(@"     = {0:E3} Sq. Sq. m", I);
                sw.WriteLine();

                double V_DL = shear_forces.DL_INNER_GIRDER.F1;
                double V_LL = shear_forces.LL_INNER_GIRDER.F1;
                sw.WriteLine(@"V(DL_End_Span) = Dead Load Shear Force at Section (1-1) = {0:f3} kN", V_DL);
                sw.WriteLine(@"");
                sw.WriteLine(@"V(LL_End_Span) = Live Load Shear Force at Section (1-1) = {0:f3} kN", V_LL);
                sw.WriteLine(@"");

                double DSC_V = 1.5 * V_DL + 2.5 * V_LL;
                sw.WriteLine(@"V = 1.5 x V(DL_End_Span) + 2.5 x V(LL_End_Span)");
                sw.WriteLine(@"  = 1.5 x {0:f3} + 2.5 x {1:f3} ", V_DL, V_LL);
                sw.WriteLine(@"  = {0:f3} kN", DSC_V);
                sw.WriteLine(@"");

                double VL = DSC_V * Ac * Y / I;
                sw.WriteLine(@"VL  = (V x Ac x Y) / I");
                sw.WriteLine(@"    = ({0:f3} x {1:f3} x {2:f3}) / {3:E3}", DSC_V, Ac, Y, I);
                sw.WriteLine(@"    = {0:E3} kN", VL);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                double Ash = ((Math.PI * prd.DSC_Ash_dia * prd.DSC_Ash_dia) / 4) * prd.DSC_Ash_Leg * (1000.0 / prd.DSC_Ash_Spacing);


                
                sw.WriteLine(@"Ash  = [(3.1416 x Ash_dia^2) / 4] x Ash_Leg x (1000.0 / Ash_Spacing)");
                //sw.WriteLine(@"     = [(3.1416 x 10^2) / 4] x 4 x (1000.0 / 150.0)");
                sw.WriteLine(@"     = [(3.1416 x {0}^2) / 4] x {1} x (1000.0 / {2})",
                   prd.DSC_Ash_dia, prd.DSC_Ash_Leg, prd.DSC_Ash_Spacing);
                sw.WriteLine(@"     = {0:f3} Sq.mm (Provided Steel)", Ash);
                sw.WriteLine();
                sw.WriteLine();
              

                double Qu = Ash * fy * 0.001;
                if (Qu > VL)
                    sw.WriteLine(@"Qu = Ash x fy x 0.001 = {0:f3} x {1} x 0.001 = {2:f3} kN > (VL = {3:f3} kN), OK.",Ash, fy, Qu, VL);
                else
                    sw.WriteLine(@"Qu = Ash x fy x 0.001 = {0:f3} x {1} x 0.001 = {2:f3} kN < (VL = {3:f3} kN), NOT OK.",Ash, fy, Qu, VL);
                    
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region STEP 15 : DESIGN OF TRANSVERSE SHEAR REINFORCEMENTS



                sw.WriteLine(@"-----------------------------------------------------------------------");
                sw.WriteLine(@"STEP 15 : DESIGN OF TRANSVERSE SHEAR REINFORCEMENTS");
                sw.WriteLine(@"-----------------------------------------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"User’s Input Data:");
                sw.WriteLine(@"");
                double sigma_y = prd.DTSR_sigma_y / 1000.0;
                sw.WriteLine(@"For fy = {0} N/Sq.mm,  σ_y = {1} N/Sq. mm = {2} kN/Sq.mm", prd.DTSR_fy, prd.DTSR_sigma_y, sigma_y);
                sw.WriteLine(@"Ls = length of Shear Plane = {0:f3} mm.", prd.DTSR_Ls);
                sw.WriteLine(@"fck = {0} N/Sq.mm", prd.DTSR_fck );
                sw.WriteLine(@"");
                sw.WriteLine(@"Ats_dia = {0} mm.", prd.DTSR_Ats_dia);
                sw.WriteLine(@"Ats_Spacing = {0} mm.", prd.DTSR_Ats_Spacing);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                //    sw.WriteLine(@"Refer to Cl. 611.5, IRC 22, 1986, ");
                sw.WriteLine(@"");
                sw.WriteLine(@"Required Area of Steel as Transverse Reinforcements:");
                sw.WriteLine(@"");
                double req_st_1 = 0.4 * prd.DTSR_Ls * Math.Sqrt(prd.DTSR_fck);
                sw.WriteLine(@"(i)   0.4 x Ls x Sqrt(fck) = 0.4 x {0:f3} x Sqrt({1}) = {2:f3} Sq.mm", prd.DTSR_Ls, prd.DTSR_fck, req_st_1);
                sw.WriteLine(@"");

                double req_st_2 = (0.7 * Ash * sigma_y) + (0.08 * prd.DTSR_Ls * Math.Sqrt(prd.DTSR_fck));

                sw.WriteLine(@"(ii)   (0.7 x Ash x σ_y) + (0.08 x Ls x Sqrt(fck))");
                sw.WriteLine(@"     = (0.7 x {0:f3} x {1:f3}) + (0.08 x {2:f3} x Sqrt({3})) = {4:f3} Sq.mm",
                    Ash, sigma_y, prd.DTSR_Ls, prd.DTSR_fck, req_st_2);
                sw.WriteLine(@"     = {0:f3} Sq.mm", req_st_2);
                sw.WriteLine(@"");
                double req_st_3 = (0.8 * prd.DTSR_Ls / sigma_y);
                sw.WriteLine(@"(iii) Minimum Transverse reinforcement = (0.8 x Ls / σ_y) = 0.8 x {0:f3} / {1:f3} = {2:f3} Sq.mm", prd.DTSR_Ls, sigma_y, req_st_3);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");

                double Ats = req_st_3;
                sw.WriteLine(@"Ats = A_top + A_bottom = {0:f3} Sq.mm", Ats);
                sw.WriteLine(@"");
                double Ats_Top = Ats / 2.0;
                double Ats_Bottom = Ats / 2.0;
                sw.WriteLine(@"Ats_Top = Ats_bottom = {0:f3} / 2.0 = {1:f3} Sq.mm", Ats, Ats_Bottom);
                sw.WriteLine(@"");
                sw.WriteLine(@"Required Area of Steel as Transverse Reinforcements at either Top or Bottom = {0:f3} Sq.mm", Ats_Bottom);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                double Ats_pro = (Math.PI * prd.DTSR_Ats_dia * prd.DTSR_Ats_dia / 4) * (1000.0 / prd.DTSR_Ats_Spacing);

                sw.WriteLine(@"Ats = Area of Transverse Steel Provided = (3.1416 x Ats_dia^2 / 4) x (1000.0 / Ats_Spacing)");
                sw.WriteLine(@"                                        = (3.1416 x {0}^2 / 4) x (1000.0 / {1:f3})",prd.DTSR_Ats_dia, prd.DTSR_Ats_Spacing);

                if (Ats_pro > Ats_Bottom)
                    sw.WriteLine(@"                                        = {0:f3} Sq.mm > (Required Area = {1:f3} Sq.mm), OK.", Ats_pro, Ats_Bottom);
                else
                    sw.WriteLine(@"                                        = {0:f3} Sq.mm < (Required Area = {1:f3} Sq.mm), NOT OK.", Ats_pro, Ats_Bottom);

                #endregion

                #region STEP 16 : DESIGN OF END DIAPHRAGM


                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"--------------------------------");
                sw.WriteLine(@"STEP 16 : DESIGN OF END DIAPHRAGM");
                sw.WriteLine(@"--------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"User’s Input Data:");
                sw.WriteLine(@"");
                sw.WriteLine(@"Width of Diaphragm = b = {0} mm, ", prd.DED_b);
                sw.WriteLine(@"Total Depth of Diaphragm = D = {0:f3} mm", prd.DED_D);
                sw.WriteLine(@"Reinforcement Clear Cover = cover = {0:f3} mm, ", prd.DED_cover);
                sw.WriteLine(@"");
                sw.WriteLine(@"dia_Ast1 = {0} mm,  Nos_Ast1 = {1}, ", prd.DED_dia_Ast1, prd.DED_Nos_Ast1);
                sw.WriteLine(@"dia_Ast2 = {0} mm,  Nos_Ast2 = {1},", prd.DED_dia_Ast2, prd.DED_Nos_Ast2);
                sw.WriteLine(@"");
                sw.WriteLine(@"dia_dsh = {0} mm.,  Leg_dsh = {1},  Spacing_dsh = {2} mm.", prd.DED_dia_dsh, prd.DED_Leg_dsh, prd.DED_Spacing_dsh);
                sw.WriteLine(@"");
                sw.WriteLine(@"");


                double pro_deff = prd.DED_D - prd.DED_cover - 10;
                sw.WriteLine(@"Provided Effective Depth = Deff      = Depth - cover ");
                sw.WriteLine(@"                                     = {0} - {1} - 20.0/2 = {2:f3} mm. ",
                    prd.DED_D, prd.DED_cover, pro_deff);
                sw.WriteLine(@"");
                sw.WriteLine(@"");


                PSC_Force_Data total_BM = moment_forces.LL_INNER_GIRDER + moment_forces.DL_INNER_GIRDER;

                sw.WriteLine(@"Total Bending Moment at Section (1-1) {0:f3} kN-m.     ", total_BM.F1);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                double req_deff = Math.Sqrt((total_BM.F1 * 1000000.0) / (2.51 * prd.DED_b));
                sw.WriteLine(@"Required effective depth        = Sqrt(M / (2.51 x b))");
                sw.WriteLine(@"                                = Sqrt ({0:f3} x 10^6 / (2.51 x {1:f3}))",total_BM.F1, prd.DED_b);
                sw.WriteLine(@"                                = {0:f3} mm.", req_deff);
                sw.WriteLine(@"");

                double req_st_rinf = (total_BM.F1 * 1000000.0) / (sigma_y*1000.0 * 0.87 * pro_deff);
                sw.WriteLine(@"Required Steel Reinforcements   = M / (σ_y x 0.87 x Deff)");
                sw.WriteLine(@"                                = {0:f3} x 10^6 / ({1:f3} x 0.87 x {2:f3})", total_BM.F1, sigma_y*1000.0, pro_deff);
                sw.WriteLine(@"                                = {0:f3} Sq. mm", req_st_rinf);
                sw.WriteLine(@"");
                sw.WriteLine(@"");

                double M = total_BM.F2;
                sw.WriteLine(@"Total Bending Moment at Section (2-2) {0:f3} kN-m.", M);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                double D_req = Math.Sqrt(M * 1000000.0 / (2.51 * prd.DED_b));
                sw.WriteLine(@"Required effective depth = D_req        = Sqrt(M/ (2.51 x b))");
                sw.WriteLine(@"                                        = Sqrt ({0:f3} x 10^6 / (2.51 x {1:f3}))",M, prd.DED_b);
                sw.WriteLine(@"                                        = {0:f3} mm.", D_req);
                sw.WriteLine(@"");
                double As_req = M * 1000000.0 / (sigma_y * 1000.0 * 0.87 * pro_deff);
                sw.WriteLine(@"Required Steel Reinforcements = As_req  = M / (σ_y x 0.87 x Deff)");
                sw.WriteLine(@"                                        = {0:f3} x 10^6 / ({1:f3} x 0.87 x {2:f3})",
                    M, sigma_y * 1000.0, pro_deff);
                sw.WriteLine(@"                                        = {0:f3} Sq. mm", As_req);
                sw.WriteLine(@"");
                if (As_req > D_req)
                    sw.WriteLine(@"Provided Effective Depth = Deff = {0:f3} mm > Required Effective Depths, OK.", As_req);
                else
                    sw.WriteLine(@"Provided Effective Depth = Deff = {0:f3} mm < Required Effective Depths, NOT OK.", As_req);

                sw.WriteLine(@"");
                sw.WriteLine(@"");
                double dia_dst = 20.0;
                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = dia_dst = {1} mm.", prd.DED_Nos_Ast1, dia_dst);
                sw.WriteLine();
                sw.WriteLine();
                double As_prov = (Math.PI * dia_dst * dia_dst / 4) * prd.DED_Nos_Ast1;

                sw.WriteLine(@"Provided Steel Reinforcements = As_prov   = (3.1416 x {0}^2 / 4) x {1} ", dia_dst, prd.DED_Nos_Ast1);
                if (As_prov > req_st_rinf)
                    sw.WriteLine(@"                                          = {0:f3} Sq.mm > Required Steel Reinforcements, OK.", As_prov);
                else
                    sw.WriteLine(@"                                          = {0:f3} Sq.mm < Required Steel Reinforcements, NOT OK.", As_prov);




                sw.WriteLine(@"");
                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = {1} mm. at Top and ", prd.DED_Nos_Ast1, dia_dst);
                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = {1} mm. at bottom. ", prd.DED_Nos_Ast1, dia_dst);

                double total_steel_prov = 2 * As_prov;
                sw.WriteLine(@"Total Steel provided at Top & Bottom = 2 x {0:f3} = {1:f3} Sq.mm.", As_prov, total_steel_prov);

                sw.WriteLine(@"");
                sw.WriteLine(@"Total Shear Force at Section (1-1) V = {0:f3} kN", Vc1.F1);



                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.1, IRC 21, 2000, ");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Cl. A4.6.1  IRC 112:2011, "); //Chiranjit [2013 06 27]
                }



                double tau = Vc1.F1 * 1000.0 / (prd.DED_b * pro_deff);
                sw.WriteLine(@"Shear Stress = τ = V / (b x d) = {0:f3} x 10^3 / ({1:f3} x {2:f3}) = {3:f3} N/Sq.mm",
                    Vc1.F1, prd.DED_b, pro_deff, tau);
                sw.WriteLine();

                tau = Vc1.F2 * 1000.0 / (prd.DED_b * pro_deff);
                sw.WriteLine(@"Total Shear Force at Section (2-2) V = {0:f3} kN", Vc1.F2);


                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.1, IRC 21, 2000, ");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to  Cl. A4.6.1  IRC 112:2011, "); //Chiranjit [2013 06 27]
                }


                sw.WriteLine(@"Shear Stress = τ = V / (b x d) = {0:f3} x 10^3 / ({1:f3} x {2:f3}) = {3:f3} N/Sq.mm",
                    Vc1.F2, prd.DED_b , pro_deff, tau);
                sw.WriteLine(@"");


                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.1, IRC 21, 2000, ");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Table A4.5  IRC 112:2011, "); //Chiranjit [2013 06 27]
                }

                double tau_max = 0.0;

                if (fck == 15.0 || fck == 20.0)
                    tau_max = 1.8;
                else if (fck == 25.0)
                    tau_max = 1.9;
                else if (fck == 30.0)
                    tau_max = 2.2;
                else if (fck == 35.0)
                    tau_max = 2.3;
                else
                    tau_max = 2.5;


                sw.WriteLine(@"Maximum Permissible Shear Stress = τ _max = {0} N/Sq.mm  (for Concrete Grade M{1}, from TABLE 11) ", tau_max, fck);
                sw.WriteLine();
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {

                    sw.WriteLine(@"Refer to Cl. 304.7.1.3.3, IRC 21, 2000, Table 12B,");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Table A4.6  IRC 112:2011,");
                }

                sw.WriteLine(@"Computation of Permissible Shear Stress ");
                sw.WriteLine(@"");
                double Ps = 2 * As_prov * 100.0 / (prd.DED_b * pro_deff);
                //sw.WriteLine(@"Percentage of Steel Provided = Ps = As x 100 / (b x d) = 2 x 1256.637 x 100 / (350.0 x 2290.0) = 0.314 %");
                sw.WriteLine(@"Percentage of Steel Provided = Ps = As x 100 / (b x d)");
                sw.WriteLine(@"                                  = 2 x {0:f3} x 100 / ({1:f3} x {2:f3}) ", As_prov, prd.DED_b, pro_deff);
                sw.WriteLine(@"                                  = {0:f3} %", Ps);

                double tau_c = 0;

                tau_c = iApp.Tables.Permissible_Shear_Stress(Ps, (CONCRETE_GRADE)(int)(fck), ref ref_str);
                sw.WriteLine(@"Corresponding Permissible Shear Stress in Concrete = τ c = {0:f3} N/Sq.mm  ", tau_c);
                sw.WriteLine(@"");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    sw.WriteLine(@"Refer to TABLE 12, at the end of this Design report,");
                sw.WriteLine(@"");
                sw.WriteLine(@"Shear Reinforcements Required:");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {

                    sw.WriteLine(@"Refer to Cl. 304.7.1.4, IRC 21, 2000,");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Cl. A4.6.1  IRC 112:2011,");
                }

                double Vs = Vc1.F1 * 1000.0 - tau_c * b * d;
                sw.WriteLine(@"Vs = V - τc x b x d = {0:f3} x 10^3 - {0:f3} x {1:f3} x {2:f3} =  {3:f3} N = {4:f3} kN",
                    Vc1.F1, tau_c, b, d, Vs, (Vs = Vs / 1000.0));

                sw.WriteLine(@"");
                //sw.WriteLine(@"If, Vs < 0.0, ");
                double Asw = (Vs * 1000.0 * prd.DED_Spacing_dsh) / (sigma_st * pro_deff);
                if (Vs < 0.0)
                    sw.WriteLine(@"So, Provide Nominal Shear Reinforcement.");
                else
                {

                    sw.WriteLine(@"Required Shear reinforcements = Asw = Vs x s / (σ_st x d)");
                    sw.WriteLine(@"                                    = {0:f3} x 1000 x {1:f3} / ({2:f3} x {3:f3})", Vs, prd.DED_Spacing_dsh, sigma_st , pro_deff);
                    //sw.WriteLine(@"                                    = 124.025 x 1000 x 150.0 / (240.0 x 2290.0) = 33.850 Sq.mm");
                    sw.WriteLine(@"                                    = {0:f3} Sq.mm", Asw);
                }
                sw.WriteLine(@"");


                sw.WriteLine(@"Provide {0} mm diameter Stirrups/Binders, {1} Legged, with Spacing of {2} mm. centre to centre, ", prd.DED_dia_dsh, prd.DED_Leg_dsh, prd.DED_Spacing_dsh);
                sw.WriteLine();

                double Asw_prov = (Math.PI * prd.DED_dia_dsh * prd.DED_dia_dsh / 4) * (prd.DED_Leg_dsh * 1000.0 / prd.DED_Spacing_dsh);
                sw.WriteLine(@"Asw   = (3.1416 x dia_dsh^2 / 4) x Leg_dsh x 1000 / Spacing_dsh)");
                sw.WriteLine(@"      = (3.1416 x {0}^2 / 4) x {1} x 1000 / {2:f3})", prd.DED_dia_dsh, prd.DED_Leg_dsh, prd.DED_Spacing_dsh);
                if (Asw_prov > Asw)
                    sw.WriteLine(@"      = {0:f3} Sq.mm > Required Shear reinforcements, OK.", Asw_prov);
                else
                    sw.WriteLine(@"      = {0:f3} Sq.mm < Required Shear reinforcements, NOT OK.", Asw_prov);


                #endregion

                #region STEP 17 : DESIGN OF INTERMEDIATE DIAPHRAGM


                sw.WriteLine(@"");
                sw.WriteLine(@"------------------------------------------------------");
                sw.WriteLine(@"STEP 17 : DESIGN OF INTERMEDIATE DIAPHRAGM");
                sw.WriteLine(@"------------------------------------------------------");
                sw.WriteLine(@"User’s Input data:");
                sw.WriteLine(@"");
                sigma_y = (sigma_y < 1) ? sigma_y * 1000 : sigma_y;

                sw.WriteLine(@"Width of Diaphragm = b = {0} mm, ", prd.DID_b);
                sw.WriteLine(@"Total Depth of Diaphragm = D = {0} mm", prd.DID_D);
                sw.WriteLine(@"Reinforcement Clear Cover = cover = {0} mm, ", prd.DID_cover);
                sw.WriteLine(@"");
                sw.WriteLine(@"dia_Ast1 = {0} mm,  Nos_Ast1 = {1}", prd.DID_dia_Ast1, prd.DID_Nos_Ast1);
                sw.WriteLine(@"dia_Ast2 = {0} mm,  Nos_Ast2 = {1}", prd.DID_dia_Ast2, prd.DID_Nos_Ast2);
                sw.WriteLine(@"");
                sw.WriteLine(@"dia_dsh = {0} mm.,  Leg_dsh = {1},  Spacing_dsh = {2} mm.", prd.DID_dia_dsh, prd.DID_Leg_dsh, prd.DID_Spacing_dsh);
                sw.WriteLine(@"");
                sw.WriteLine(@"");

                pro_deff = prd.DID_D - prd.DID_cover + (dia_dst / 2.0);
                sw.WriteLine(@"Provided Effective Depth = Deff      = Depth - cover ");
                sw.WriteLine(@"                                     = {0:f3} - {1} - 20.0/2 = {2:f3} mm. ",
                    prd.DID_D, prd.DID_cover, pro_deff);

                sw.WriteLine(@"");

                M = total_BM.F9;
                sw.WriteLine(@"Total Bending Moment at Section (9-9) {0:f3} kN-m. ", M);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                D_req = Math.Sqrt(M * 1000000.0 / (2.51 * prd.DID_b));
                sw.WriteLine(@"Required effective depth = D_req        = Sqrt(M / (2.51 x b))");
                sw.WriteLine(@"                                        = Sqrt ({0:f3} x 10^6 / (2.51 x {1:f3}))", M, prd.DID_b);
                sw.WriteLine(@"                                        = {0:f3} mm.", D_req);
                sw.WriteLine(@"");
                As_req = (M * 1000000.0) / (sigma_y * 0.87 * pro_deff);
                sw.WriteLine(@"Required Steel Reinforcements = As_req  = M / (σ_y x 0.87 x Deff)");
                sw.WriteLine(@"                                        = {0:f3} x 10^6 / ({1:f3} x 0.87 x {2:f3})",
                    M, sigma_y, pro_deff);
                sw.WriteLine(@"                                        = {0:f3} Sq. mm", As_req);
                sw.WriteLine(@"");
                if (pro_deff >= D_req)
                    sw.WriteLine(@"Provided Effective Depth = Deff = {0:f3} mm > Required Effective Depths, OK.", pro_deff);
                else
                    sw.WriteLine(@"Provided Effective Depth = Deff = {0:f3} mm < Required Effective Depths, NOT OK.", pro_deff);

                

                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = {1} mm. and", prd.DID_Nos_Ast1, prd.DED_dia_Ast1);
                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = {1} mm.", prd.DID_Nos_Ast2, prd.DED_dia_Ast2);
                sw.WriteLine(@"");

                As_prov = (Math.PI * prd.DID_dia_Ast1 * prd.DID_dia_Ast1 / 4) * prd.DID_Nos_Ast1 +
                    (Math.PI * prd.DID_dia_Ast2 * prd.DID_dia_Ast2 / 4) * prd.DID_Nos_Ast2;

                sw.WriteLine(@"Provided Steel Reinforements = As_prov ");
                sw.WriteLine(@"");
                sw.WriteLine(@"  As_prov = (3.1416 x dia_Ast1^2 / 4) x Nos_Ast1 + (3.1416 x dia_Ast2^2 / 4) x Nos_Ast2");
                sw.WriteLine(@"          = (3.1416 x {0}^2 / 4) x {1} + (3.1416 x {2}^2 / 4) x {3}",
                   prd.DID_dia_Ast1, prd.DID_Nos_Ast1, prd.DID_dia_Ast2, prd.DID_Nos_Ast2);
                if (As_prov > As_req)
                    sw.WriteLine(@"          = {0:f3} Sq.mm > Required Steel Reinforcements, OK.", As_prov);
                else
                    sw.WriteLine(@"          = {0:f3} Sq.mm < Required Steel Reinforcements, NOT OK.", As_prov);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = {1} mm. ", prd.DID_Nos_Ast1, prd.DID_dia_Ast1);
                sw.WriteLine(@"and {0} Nos. of Reinforcement Bars of diameter = {1} mm. at Top and ", prd.DID_Nos_Ast2, prd.DID_dia_Ast2);
                sw.WriteLine();


                sw.WriteLine(@"Provide {0} Nos. of Reinforcement Bars of diameter = {1} mm.", prd.DID_Nos_Ast1, prd.DID_dia_Ast1);
                sw.WriteLine(@"and {0} Nos. of Reinforcement Bars of diameter = {1} mm. at bottom.", prd.DID_Nos_Ast2, prd.DID_dia_Ast2);
                sw.WriteLine();
                total_steel_prov = 2 * As_prov;
                sw.WriteLine(@"Total Steel provided at Top & Bottom = 2 x {0} Sq.mm.", As_prov);
                sw.WriteLine(@"");

                sw.WriteLine(@"Total Shear Force at Section (3-3) V = {0} kN", Vc1.F3);
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.1, IRC 21, 2000, ");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Cl. A4.6.1  IRC 112:2011, "); //Chiranjit [2013 06 27]
                }
                tau = (Vc1.F3 * 1000.0) / (prd.DID_b * pro_deff);
                //sw.WriteLine(@"Shear Stress = τ = V / (b x d) = 103.980 x 10^3 / (350.0 x 2290.0) = 0.130 N/Sq.mm");
                sw.WriteLine(@"Shear Stress = τ = V / (b x d) = {0:f3} x 10^3 / ({1:f3} x {2:f3}) = {3:f3} N/Sq.mm",
                    Vc1.F3 , prd.DID_b , pro_deff, tau);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.2, IRC 21, 2000, ");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Table A4.5  IRC 112:2011, ");
                }
                sw.WriteLine(@"");

                sw.WriteLine(@"Maximum Permissible Shear Stress = τ _max = {0} N/Sq.mm  for Concrete Grade M{1}, ", tau_max, fck);

                sw.WriteLine(@"Refer to TABLE 11, at the end of this Report,");
                sw.WriteLine(@"");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.3.3, IRC 21, 2000, Table 12B,");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Table A4.6  IRC 112:2011,"); //Chiranjit [2013 06 27]
                }
                sw.WriteLine(@"Computation of Permissible Shear Stress ");
                sw.WriteLine(@"");

                Ps = 2 * (As_prov * 100.0) / (prd.DID_b * pro_deff);
                sw.WriteLine(@"Percentage of Steel Provided = Ps = As x 100 / (b x d)");
                //sw.WriteLine(@"                                  = 2 x 3220.133 x 100 / (350.0 x 2290.0) = 0.80 %");
                sw.WriteLine(@"                                  = 2 x {0:f3} x 100 / ({1:f3} x {2:f3}) = {3:f3} %", As_prov, prd.DID_b, pro_deff, Ps);

                tau_c = iApp.Tables.Permissible_Shear_Stress(Ps, (CONCRETE_GRADE)(int)fck, ref ref_string);
                sw.WriteLine(@"Corresponding Permissible Shear Stress in Concrete = τ c = {0} N/Sq.mm ", tau_c);
                sw.WriteLine(@"");
                sw.WriteLine(@"Refer to TABLE 12, at the end of this Report,");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"Shear Reinforcements Required:");
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    sw.WriteLine(@"Refer to Cl. 304.7.1.4, IRC 21, 2000,");
                    //Chiranjit [2014 01 12]
                    //sw.WriteLine(@"Refer to Cl. A4.6.1  IRC 112:2011,");
                }
                Vs = (Vc1.F3 * 1000.0) - tau_c * prd.DID_b * pro_deff;

                //sw.WriteLine(@"Vs = V - τc x b x d = 103.980 x 10^3 - 0.40 x 350 x 2290.0 = -216620.0 N = -216.620 kN");
                sw.WriteLine(@"Vs = V - τc x b x d = {0:f3} x 10^3 - {1:f3} x {2:f3} x {3:f3} = {4:f3} N = {5:f3} kN",
                   Vc1.F3, tau_c, prd.DID_b, pro_deff, Vs, (Vs = Vs / 1000.0));
                sw.WriteLine();

                //sw.WriteLine(@"If, Vs < 0.0, ");

                Asw = (Vs * 1000) * prd.DID_Spacing_dsh / (sigma_st * pro_deff);
                if (Vs < 0)
                    sw.WriteLine(@"So, Provide Nominal Shear Reinforcement.");
                else
                {
                    sw.WriteLine(@"Required Shear reinforcements = Asw = Vs x s / (σ_st x d) ");
                    sw.WriteLine(@"                                    = {0:f3} x 1000 x {1:f3} / ({2:f3} x {3:f3})", Vs, prd.DID_Spacing_dsh, sigma_st, pro_deff);
                    sw.WriteLine(@"                                    = {0:f3} Sq.mm", Asw);
                }
                
                sw.WriteLine(@"");
                //sw.WriteLine(@"Provide 12mm diameter Stirrups/Binders, 2 Legged, with Spacing of 150 mm. centre to centre, ");
                sw.WriteLine(@"Provide {0}mm diameter Stirrups/Binders, {1} Legged, with Spacing of {2} mm. centre to centre, ",
                     prd.DID_dia_dsh, prd.DID_Leg_dsh, prd.DID_Spacing_dsh);
                sw.WriteLine(@"");

                Asw_prov = (Math.PI * prd.DID_dia_dsh * prd.DID_dia_dsh / 4) * (prd.DID_Leg_dsh * 1000.0 / prd.DID_Spacing_dsh);

                sw.WriteLine(@"Asw    = (3.1416 x dia_dsh^2 / 4) x Leg_dsh x 1000 / Spacing_dsh)");
                //sw.WriteLine(@"       = (3.1416 x 12^2 / 4) x 2 x 1000 / 150.0)");
                sw.WriteLine(@"       = (3.1416 x {0}^2 / 4) x {1} x 1000 / {2})",
                   prd.DID_dia_dsh, prd.DID_Leg_dsh, prd.DID_Spacing_dsh);
                if (Asw_prov >= Asw)
                    sw.WriteLine(@"       = {0:f3} Sq.mm > Required Shear reinforcements, OK.", Asw_prov);
                else
                    sw.WriteLine(@"       = {0:f3} Sq.mm < Required Shear reinforcements, NOT OK.", Asw_prov);


                #endregion

                #region STEP 18 : DESIGN OF END CROSS GIRDER AS DEEP BEAM


                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(@"-----------------------------------------------------------------");
                sw.WriteLine(@"STEP 18 : DESIGN OF END CROSS GIRDER AS DEEP BEAM");
                sw.WriteLine(@"-----------------------------------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"User’s Input data:");
                sw.WriteLine(@"");
                sw.WriteLine(@"Diameter of bars for Positive Reinforcement = Asp_dia = {0} mm.", ecgd.Asp_dia);
                sw.WriteLine(@"Number of bars for Positive Reinforcement = Asp_nos = {0}", ecgd.Asp_Nos);
                sw.WriteLine(@"");
                sw.WriteLine(@"Diameter of bars for Negative Reinforcement in Zone 1 = Asn1_dia = {0} mm.", ecgd.Asn1_dia);
                sw.WriteLine(@"Number of bars for Positive Reinforcement = Asn1_nos = {0}", ecgd.Asn1_Nos);
                sw.WriteLine(@"");
                sw.WriteLine(@"Diameter of bars for Negative Reinforcement in Zone 2 = Asn2_dia = {0} mm.", ecgd.Asn2_dia);
                sw.WriteLine(@"Number of bars for Positive Reinforcement = Asn2_nos = {0}", ecgd.Asn2_Nos);
                sw.WriteLine(@"");
                sw.WriteLine(@"Total Depth of Cross Girder = Dc = {0} m", ecgd.Dc);
                sw.WriteLine(@"Width of Cross Girder = Bc = {0} m", ecgd.Bc);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"Centre to Centre Span of Cross Girder = L = {0:f3} m", ecgd.L);
                sw.WriteLine(@"");

                double L_dash = ecgd.L - (2.0 * ecgd.Bc);
                sw.WriteLine(@"Clear Span of Cross Girder = L’ = {0:f3} - {1:f3} = {2:f3} m", ecgd.L, (2.0 * ecgd.Bc), L_dash);
                sw.WriteLine(@"");
                sw.WriteLine(@"");

                double L_Dc = (ecgd.L) / (ecgd.Dc);

                sw.WriteLine(@"L/Dc = {0:f3} / {1:f3} = {2:f3} " , (ecgd.L) , (ecgd.Dc), L_Dc);
                sw.WriteLine(@"");

                double L_dash_Dc = L_dash / (ecgd.Dc);


                sw.WriteLine(@"L’/Dc = {0:f3} / {1:f3} = {2:f3}", (L_dash), (ecgd.Dc), L_dash_Dc);
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"Lever Arm = z");
                sw.WriteLine(@"");
                sw.WriteLine(@"For Continuous Beams,");
                sw.WriteLine(@"");

                double z = 0.5 * ecgd.L;

                if (1 <= L_Dc && ecgd.L / ecgd.Dc <= 2.5)
                {
                    z = 0.2 * (ecgd.L + 1.5 * ecgd.Dc);
                    sw.WriteLine(@" z = 0.2 x (L + 1.5 x Dc)");
                    sw.WriteLine(@"   = 0.2 x ({0:f3} + 1.5 x {1:f3})", ecgd.L, ecgd.Dc);
                    sw.WriteLine(@"   = {0:f3}", z);
                }
                else if (L_Dc < 1)
                {
                    z = 0.5 * ecgd.L;
                    sw.WriteLine(@" z = 0.5 x {0:f3} = {1:f3} m.", ecgd.L, z);
                }

                //sw.WriteLine(@"If (1 <= L/Dc <= 2.5) then, z = 0.2 x (L + 1.5 x Dc)");
                //sw.WriteLine(@"");
                //sw.WriteLine(@"Else, if (L/Dc < 1) then, z = 0.5 x L");
                //sw.WriteLine(@"");
                //sw.WriteLine(@"");
                //sw.WriteLine(@"Therefore, z = 0.5 x 2.400 = 1.200 m.");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                M = total_BM.F1;

                sw.WriteLine(@"Total Bending Moment at Section (1-1) {0:f3} kN-m.", total_BM.F1);
                sw.WriteLine(@"");
                sw.WriteLine(@"Required Positive Reinforcements  = Asp         ");


                deff = pro_deff;
                double Asp = (M*1000000) / (sigma_y * 0.87 * deff);

                sw.WriteLine(@"                                  = M / (σ_y x 0.87 x Deff)");
                sw.WriteLine(@"                                  = {0:f3} x 10^6 / ({1} x 0.87 x {2:f3})", M, sigma_y, deff);
                sw.WriteLine(@"                                  = {0:f3} Sq. mm", Asp);
                sw.WriteLine(@"");
                sw.WriteLine(@"Provide {0} Nos. {1} mm Diameter bars", ecgd.Asp_Nos, ecgd.Asp_dia);
                sw.WriteLine(@"");
                double ppr = ecgd.Asp_Nos * (Math.PI * ecgd.Asp_dia * ecgd.Asp_dia / 4);

                sw.WriteLine(@"Provided Positive Reinforcements = ppr = Asp_nos x (3.1416 x Asp_dia^2 / 4) ");
                sw.WriteLine(@"                                       = {0} x (3.1416 x {1}^2/4)", ecgd.Asp_Nos, ecgd.Asp_dia);
                sw.WriteLine(@"                                       = {0:f3} Sq. mm.", ppr);
                sw.WriteLine(@"");
                sw.WriteLine(@"For distributing Positive Reinforcements,");
                sw.WriteLine(@"");

                double dztf = 0.25 * ecgd.Dc - 0.05 * ecgd.L;
                sw.WriteLine(@"Distance of Zone from Tension Face = 0.25 x Dc - 0.05 x L");
                sw.WriteLine(@"                                   = 0.25 x {0:f3} - 0.05 x {1:f3}", ecgd.Dc, ecgd.L);
                sw.WriteLine(@"                                   = {0:f4} m.", dztf);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                M = total_BM.F2;

                sw.WriteLine(@"Total Bending Moment at Section (2-2) {0} kN-m.", M);
                sw.WriteLine();
                sw.WriteLine(@"Required Total Negative Reinforcements in Zone 1 & Zone 2 = Asn ");
                sw.WriteLine();
                double Asn = (M * 1000000.0) / (sigma_y * 0.87 * deff);
                sw.WriteLine(@"                                        = M / (σ_y x 0.87 x Deff)");
                sw.WriteLine(@"                                        = {0:f3} x 10^6 / ({1} x 0.87 x {2})", M, sigma_y, deff);
                //sw.WriteLine(@"                                        = 582.100 x 10^6 / (240.0 x 0.87 x 2600.0)");
                sw.WriteLine(@"                                        = {0:f3} Sq. mm", Asn);
                sw.WriteLine(@"");

                double rnrz1 = 0.11 * Asn;
                sw.WriteLine(@"Required Negative Reinforcements in Zone 1  = 11% of Total Negative Reinforcements");
                sw.WriteLine(@"                                            = 0.11 x Asn");
                sw.WriteLine(@"                                            = 0.11 x {0:f3}", Asn);
                sw.WriteLine(@"                                            = {0:f3} Sq.mm", rnrz1);
                sw.WriteLine(@"");

                double pnrz1 = ecgd.Asn1_Nos * (Math.PI * ecgd.Asn1_dia * ecgd.Asn1_dia / 4.0);
                sw.WriteLine(@"Provided Negative Reinforcements in Zone 1 = Asn1_nos x (3.1416 x Asn1_dia^2 / 4) ");
                sw.WriteLine(@"                                           = {0:f3} x (3.1416 x {1:f3}^2 / 4) ", ecgd.Asn1_Nos, ecgd.Asn1_dia);
               
                if (pnrz1 >= rnrz1)
                    sw.WriteLine(@"                                           = {0:f3} Sq.mm  >= {1:f3} Sq.mm, OK", pnrz1, rnrz1);
                else
                    sw.WriteLine(@"                                           = {0:f3} Sq.mm  < {1:f3} Sq.mm, NOT OK", pnrz1, rnrz1);

                sw.WriteLine();
                sw.WriteLine(@"For distributing Negative Reinforcements,");
                sw.WriteLine();
                double dz1 = 0.2 * ecgd.Dc;
                sw.WriteLine(@"Distance of Zone 1, from Tension Face   = 0.2 x Dc ");
                sw.WriteLine(@"                                        = 0.2 x {0:f3} ", ecgd.Dc);
                sw.WriteLine(@"                                        = {0:f3} m.", dz1);
                sw.WriteLine(@"");
                double rnrz2 = Asn - (0.11 * Asn);
                sw.WriteLine(@"Required Negative Reinforcements in Zone 2  = Balance of Total Negative Reinforcements");
                sw.WriteLine(@"                                            = Asn - (0.11 x Asn)");
                sw.WriteLine(@"                                            = {0:f3} - {1:f3}", Asn, (0.11 * Asn));
                sw.WriteLine(@"                                            = {0:f3} Sq.mm", rnrz2);
                sw.WriteLine();
                sw.WriteLine();

                double pnrz2 = ecgd.Asn2_Nos * (Math.PI * ecgd.Asn2_dia * ecgd.Asn2_dia / 4.0);
                sw.WriteLine(@"Provided Negative Reinforcements in Zone 2  = Asn2_nos x (3.1416 x Asn2_dia^2 / 4) ");
                sw.WriteLine(@"                                            = {0} x (3.1416 x {1}^2 / 4) ", ecgd.Asn2_Nos, ecgd.Asn2_dia);

                if (pnrz2 > rnrz2)
                    sw.WriteLine(@"                                            = {0:f3} Sq.mm > {1:f3} Sq.mm, OK", pnrz2, rnrz2);
                else
                    sw.WriteLine(@"                                            = {0:f3} Sq.mm < {1:f3} Sq.mm, NOT OK", pnrz2, rnrz2);

                sw.WriteLine();

                //val1 = ecgd.Asn2_Nos
                sw.WriteLine(@"Provided Negative Reinforcements = Asn2_nos/2 on either side of the Centre Line,");
                sw.WriteLine();
                sw.WriteLine(@"For distributing Negative Reinforcements,");
                val1 = 0.6 * ecgd.Dc;
                sw.WriteLine(@"Distance of Zone 2, from Tension Face   = 0.6 x Dc ");
                sw.WriteLine(@"                                        = 0.6 x {0:f3}", ecgd.Dc);
                sw.WriteLine(@"                                        = {0:f3} m.", val1);
                sw.WriteLine(@"");



                #region Chiranjit [2013 06 28]

                sw.WriteLine("");
                sw.WriteLine("---------------------------------------------------");
                sw.WriteLine("STEP 19 : CHECK FOR LIVE LOAD DEFLECTION");
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




                sw.WriteLine(@"                     ------------------------------------------");
                sw.WriteLine(@"                     END OF REPORT FOR DESIGN OF PSC ‘I’ GIRDER");
                sw.WriteLine(@"                     ------------------------------------------");
                sw.WriteLine(@"");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
                //sw.WriteLine("------------------------------------------------------------------------------------------------------------");
                //sw.WriteLine(sec_format, "Sections", "1-1", "2-2", "3-3", "4-4", "5-5", "6-6", "7-7", "8-8", "9-9");
                //sw.WriteLine("------------------------------------------------------------------------------------------------------------");

                #endregion










                sw.WriteLine();


               
                Write_Table_2(sw);
                sw.WriteLine();
                sw.WriteLine();
              
                Write_Table_3(sw);

                sw.WriteLine(@"");
                sw.WriteLine(@"  Fbst  = Bursting Tensile Force");
                sw.WriteLine(@"  Pk = Tendon Force");
                sw.WriteLine(@"  2Ypo = Side of Loaded Area");
                sw.WriteLine(@"  2Yo = Side of end Block");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"TABLE 11: Maximum Shear Stress, τ _max, N/Sq.mm");
                sw.WriteLine(@"-----------------------------------------------------------------------------");
                sw.WriteLine(@"Concrete Grade          M20    M25   M30    M35    M40 and above");
                sw.WriteLine(@"τ _max, N/Sq.mm (MPa)   1.8    1.9   2.2    2.3    2.5");
                sw.WriteLine(@"-----------------------------------------------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"        ");
                sw.WriteLine(@"");
                sw.WriteLine(@"TABLE 12: PERMISSIBLE SHEAR STRESS τ_c(N/sq.mm)");
                sw.WriteLine(@"                  Grades of Concrete");
                sw.WriteLine(@"       --------------------------------------- ");
                sw.WriteLine(@"P(%)   M 15   M 20   M 25   M 30   M 35   M 40");
                sw.WriteLine(@"----------------------------------------------");
                sw.WriteLine(@"(1)    (2)     (3)   (4)    (5)    (6)    (7)");
                sw.WriteLine(@"----------------------------------------------");
                sw.WriteLine(@"0.15   0.18   0.18   0.19   0.20   0.20   0.20");
                sw.WriteLine(@"0.25   0.22   0.22   0.23   0.23   0.23   0.23");
                sw.WriteLine(@"0.50   0.29   0.30   0.31   0.31   0.31   0.32");
                sw.WriteLine(@"0.75   0.34   0.35   0.36   0.37   0.37   0.38");
                sw.WriteLine(@"1.00   0.37   0.39   0.40   0.41   0.42   0.42");
                sw.WriteLine(@"1.25   0.40   0.42   0.44   0.45   0.45   0.46");
                sw.WriteLine(@"1.50   0.42   0.45   0.46   0.48   0.49   0.49");
                sw.WriteLine(@"1.75   0.44   0.47   0.49   0.50   0.52   0.52");
                sw.WriteLine(@"2.00   0.44   0.49   0.51   0.53   0.54   0.55");
                sw.WriteLine(@"2.25   0.44   0.51   0.53   0.55   0.56   0.57");
                sw.WriteLine(@"2.50   0.44   0.51   0.55   0.57   0.58   0.60");
                sw.WriteLine(@"2.75   0.44   0.51   0.56   0.58   0.60   0.62");
                sw.WriteLine(@"3.00   0.44   0.51   0.57   0.60   0.62   0.63");
                sw.WriteLine(@"----------------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"");
                sw.WriteLine(@"TABLE 13: Maximum Shear Stress ");
                sw.WriteLine(@"-----------------------------------------------------------------------------");
                sw.WriteLine(@"Concrete Grade               30     35      40        45     50     55    60");
                sw.WriteLine(@"Maximum Shear Stress (MPa)   4.1    4.4     4.7       5.0    5.3    5.5   5.8");
                sw.WriteLine(@"-----------------------------------------------------------------------------");
                sw.WriteLine(@"");
                sw.WriteLine(@"");

                sw.WriteLine();
                sw.WriteLine();

                #region END OF REPORT
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

                rep_file_name = Path.Combine(file_path, "PSC I_GIRDER DESIGN REPORT.TXT");
                user_input_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER.FIL");
                user_drawing_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER_DRAWING.FIL");

                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        ;//Longg Read_Long_Girder_User_Input();
                //}
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
            sw.WriteLine("TABLE 9 :");
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
            sw.WriteLine("TABLE 10 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
        }


    }
    public class RccDeckSlab
    {
        public double S, CW, Wk, tk, Fw, twc, Wt, Ltl, Wtl, L, bl, B, bc;
        public double _Do, fck, fci, sigma_cb, sigma_st, m, Q, j, fy, gamma_c, gamma_wc, _IF, CF;
        public double tds, dm, dd;

        IApplication iApp = null;
        public string rep_file_name = "";
        public string file_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_path = "";
        public string drawing_path = "";
        public bool is_process = false;



        // Drawing Variable
        string _A, _B, _C, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;


        public RccDeckSlab(IApplication app)
        {
            //InitializeComponent();
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

        }

        #region IReport Members

        public void Calculate_Program()
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t************************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                   *");
                sw.WriteLine("\t\t*         TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                              *");
                sw.WriteLine("\t\t*          DESIGN OF RCC DECK SLAB             *");
                sw.WriteLine("\t\t*                                              *");
                sw.WriteLine("\t\t************************************************");
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
                sw.WriteLine("Effective Span [S] = {0} m", S);
                sw.WriteLine("Width of Carrage way [CW] = {0} m", CW);
                sw.WriteLine("Width of Kerb [Wk] = {0} m", Wk);
                sw.WriteLine("Thickness of Kerb [tk] = {0} m", tk);
                sw.WriteLine("Width of Footpath [Fw] = {0} m", Fw);
                sw.WriteLine("Thickness of Wearing Course [twc] = {0} m    Marked as (E) in Drawing", twc);
                //
                _E = string.Format("{0:f3}", twc);



                sw.WriteLine("Track Load [Wt] = {0} kN", Wt);
                sw.WriteLine("Track Loading Length [Ltl] = {0} m", Ltl);
                sw.WriteLine("Track Loading Width [Wtl] = {0} m", Wtl);
                sw.WriteLine("Spacing of Main Long Girders [L] = {0} m     Marked as (A) in Drawing", L);
                //
                _A = string.Format("{0}", L);

                sw.WriteLine("Width of Long Girder [bl] = {0} m            Marked as (B) in Drawing", bl);
                //
                _B = string.Format("{0}", bl);

                sw.WriteLine("Spacing of Cross Girders [B] = {0} m         Marked as (C) in Drawing", B);
                //
                _C = string.Format("{0}", B);


                sw.WriteLine("Width of Cross Girders [bc] = {0} m          Marked as (D) in Drawing", bc);
                //
                _D = string.Format("{0}", bc);


                sw.WriteLine("Thickness of Deck Slab [Do] = {0} m          Marked as (F) in Drawing", _Do);
                //
                _F = string.Format("{0}", _Do);

                sw.WriteLine("Concrete Grade [fck] = M {0} = {0} N/sq.mm", fck);
                sw.WriteLine("Concrete Cube strength at transfer [fci] = {0} N/sq.mm", fci);
                sw.WriteLine("Permissible compressive stress in concrete [σ_cb] = {0} N/sq.mm", sigma_cb);
                sw.WriteLine("Permissible tensile stress in steel [σ_st] = {0} N/sq.mm", sigma_st);
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("Steel Grade [fy] = Fe {0} = {0} N/sq.mm", fy);
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine("Unit Weight of Wearing Course [γ_wc] = {0} kN/cu.m", gamma_wc);
                sw.WriteLine("Impact Factor [IF] = {0}", _IF);
                sw.WriteLine("Continuity Factor [CF] = {0}", CF);
                sw.WriteLine("Diameter of Main Reinforcement Bars [dm] = {0}", dm);
                sw.WriteLine("Diameter of Distribution Reinforcement Bars [dd] = {0}", dd);
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
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : CROSS SECTION OF DECK SLAB ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For each panel enclosed by 2 long and 2 Cross girders");
                sw.WriteLine();
                sw.WriteLine("Length = L = {0} m", L);
                sw.WriteLine();
                sw.WriteLine("Width = B = {0} m", B);
                sw.WriteLine();
                tds = _Do;
                sw.WriteLine("Thickness of Deck Slab = tds = {0} m", tds);
                sw.WriteLine();
                sw.WriteLine("Thickness of Wearing Course = twc = {0} m", twc);
                sw.WriteLine();
                #endregion

                #region STEP 2 : LIVE LOAD BENDING MOMENT
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : LIVE LOAD BENDING MOMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Live Load  = Wt = {0} kN", Wt);
                sw.WriteLine();
                sw.WriteLine("Live Load Length = Ltl = {0} m", Ltl);
                sw.WriteLine();
                sw.WriteLine("Live Load Width = Wtl = {0} m", Wtl);
                sw.WriteLine();
                sw.WriteLine("Considering 45° dispersion through wearing Course");
                sw.WriteLine();

                double u = Wtl + 2 * twc;
                sw.WriteLine("    u = {0} + 2 * {1} = {2} m", Wtl, twc, u);
                sw.WriteLine();
                double v = Ltl + 2 * twc;
                v = double.Parse(v.ToString("0.00"));
                sw.WriteLine("    v = {0} + 2 * {1} = {2} m", Ltl, twc, v);
                sw.WriteLine();
                sw.WriteLine();

                double u_by_B = u / B;
                sw.WriteLine("   u/B = {0} / {1} = {2:f3} ", u, B, u_by_B);
                sw.WriteLine();
                double v_by_L = v / L;
                sw.WriteLine("   v/L = {0} / {1} = {2:f3} ", v, L, v_by_L);
                sw.WriteLine();

                double K = B / S;
                K = double.Parse(K.ToString("0.0"));
                sw.WriteLine("  K = B/L = {0} / {1} = {2}", B, L, K);

                sw.WriteLine();

                double m1, m2;

                m1 = 0.069;
                m2 = 0.060;

                frmCurve fcurv = new frmCurve(K, u_by_B, v_by_L, LoadType.PartialLoad);
               
                    fcurv.txt_m1.Text = m1.ToString("f3");
                    fcurv.txt_m2.Text = m2.ToString("f3");
                //}
                //else if (K < 1.0)
                    fcurv.ShowDialog();

                m1 = fcurv.m1;
                m2 = fcurv.m2;

                sw.WriteLine(" Using Pegeaud's Curves     m1 = {0} and  m2 = {1}", m1, m2);
                sw.WriteLine();

                double MB = Wt * (m1 + 0.15 * m2);
                sw.WriteLine("Bending Moment along Short span = MB");
                sw.WriteLine("                                = Wt*(m1 + 0.15 * m2)");
                sw.WriteLine("                                = {0}*({1} + 0.15 * {2})", Wt, m1, m2);
                sw.WriteLine("                                = {0} kN-m", MB);
                sw.WriteLine();
                sw.WriteLine("As slab is continuous, Design B.M. = CF * MB = {0} * {1}", CF, MB);
                sw.WriteLine();

                double MB1 = CF * _IF * MB;
                MB1 = double.Parse(MB1.ToString("0.00"));
                sw.WriteLine("Including Impact Factor , MB1 = CF * IF * MB");
                sw.WriteLine("                              = {0} * {1} * {2}", CF, _IF, MB);
                sw.WriteLine("                              = {0} kN-m", MB1);
                sw.WriteLine();

                double ML = Wt * (m2 + 0.15 * m1);
                sw.WriteLine("Bending Moment along Long Span = ML ");
                sw.WriteLine("                               = Wt * (m2 + 0.15 * m1)");
                sw.WriteLine("                               = {0} * ({1} + 0.15 * {2})", Wt, m2, m1);
                sw.WriteLine("                               = {0} kN-m", ML);
                sw.WriteLine();

                double ML1 = CF * _IF * ML;
                sw.WriteLine("Design B.M. = ML1");
                sw.WriteLine("            = CF * IF * ML");
                sw.WriteLine("            = {0} * {1} * {2}", CF, _IF, ML);
                sw.WriteLine("            = {0} kN-m", ML1);
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 3 : LIVE LOAD SHEAR FORCES
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : LIVE LOAD SHEAR FORCES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Considering dispersion along short span");

                double cons_disp = Wtl + 2 * (twc + _Do);
                sw.WriteLine("    = Wtl + 2 * (twc + Do)");
                sw.WriteLine("    = {0} + 2 * ({1} + {2})", Wtl, twc, _Do);
                sw.WriteLine("    = {0} m", cons_disp);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For maximum Shear force Load is Placed at a Location ");
                sw.WriteLine("So that the whole dispersion is in the span.");
                sw.WriteLine();

                double x = cons_disp / 2.0;
                sw.WriteLine("So, placing the Load at {0}/2 = {1:f4} m from the edge at the beam.", cons_disp, x);
                sw.WriteLine();
                sw.WriteLine("So   x = {0:f4}", x);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Effective width of slab = K * x * [1 - (x/L)] + bw");
                sw.WriteLine();
                sw.WriteLine("Width of Long Girders = {0} m", bl);
                sw.WriteLine("Width of Cross Girder = {0} m", bc);
                sw.WriteLine();
                double B1 = L - bl/1000.0;
                sw.WriteLine("Clear Length of Panel = B1 = {0} - {1} = {2:f3} m", B, bc / 1000.0, B1);
                sw.WriteLine();


                double L1 = B - bc / 1000.0;
                sw.WriteLine("Clear width of Panel = L1 = {0} - {1} = {2:f3} m", L, bl / 1000.0, L1);
                sw.WriteLine();

                double B_by_L = B1 / L1;
                B_by_L = double.Parse(B_by_L.ToString("f4"));


                K = Get_Table_1_Value(B_by_L, 2, ref ref_string);
                sw.WriteLine("From Table 1, given at the end of the Report : {0}", ref_string);
                sw.WriteLine(" B1 / L1 = {0} / {1} = {2}  ", B1, L1, B_by_L);
                //K = 2.6;
                sw.WriteLine(" K = {0}", K);
                sw.WriteLine();

                double eff_wdt_slab = K * x * (1 - (x / L1)) + Ltl + 2 * twc;
                eff_wdt_slab = double.Parse(eff_wdt_slab.ToString("0.000"));
                sw.WriteLine("Effective width of slab = K * x * [1 - (x/L1)] + Ltl");
                sw.WriteLine("       = {0} * {1} * [1 - ({1}/{2})] + {3}", K, x, L1, Ltl);
                sw.WriteLine("       = {0} m", eff_wdt_slab);
                sw.WriteLine();
                double load_per_mtr = Wt / eff_wdt_slab;
                load_per_mtr = double.Parse(load_per_mtr.ToString("f4"));
                sw.WriteLine("Load per metre width = {0}/{1} = {2} kN", Wt, eff_wdt_slab, load_per_mtr);
                sw.WriteLine();

                double V = load_per_mtr * (L1 - x) / L1;
                V = double.Parse(V.ToString("f4"));
                sw.WriteLine(" V = Shear force per metre width");
                sw.WriteLine("   = {0} * (L1-x)/L1", load_per_mtr);
                sw.WriteLine("   = {0} * ({1}-{2})/{1}", load_per_mtr, L1, x);
                sw.WriteLine("   = {0} kN", V);
                sw.WriteLine();

                double sh_frc_imp = _IF * V;
                sh_frc_imp = double.Parse(sh_frc_imp.ToString("0.0000"));
                sw.WriteLine(" Shear force with impact = {0}* {1} = {2} kN", _IF, V, sh_frc_imp);
                sw.WriteLine();

                #endregion

                #region STEP 4 : Permanent Load BENDING MOMENTS AND SHEAR FORCES
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Permanent Load BENDING MOMENTS AND SHEAR FORCES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double self_weight_dk_slb = _Do * gamma_c;
                self_weight_dk_slb = double.Parse(self_weight_dk_slb.ToString("0.00"));
                sw.WriteLine("Self weigth of Deck Slab = {0} * {1} = {2} kN/sq.m", _Do, gamma_c, self_weight_dk_slb);
                sw.WriteLine();

                double self_wt_wrng_crs = twc * gamma_wc;
                self_wt_wrng_crs = double.Parse(self_wt_wrng_crs.ToString("0.00"));
                sw.WriteLine("Self weight of wearing course = {0} * {1} = {2} kN/sq.m", twc, gamma_wc, self_wt_wrng_crs);
                sw.WriteLine();

                double total_load = self_weight_dk_slb + self_wt_wrng_crs;
                total_load = double.Parse(total_load.ToString("0.00"));
                sw.WriteLine(" Total Load = {0} kN/sq.m", total_load);
                sw.WriteLine();

                double total_load_panel = L * B * total_load;
                total_load_panel = double.Parse(total_load_panel.ToString("0.00"));
                sw.WriteLine("Total Load on a Panel = L*B*{0}", total_load);
                sw.WriteLine("                      = {0} * {1} * {2}", L, B, total_load);
                sw.WriteLine("                      = {0} kN", total_load_panel);
                sw.WriteLine();
                sw.WriteLine("As the Panel is fully Loaded with uniformly distributed load");

                K = B / L;

                sw.WriteLine("So, u/B = 1    and   v/L = 1   and  K = B/L = {0}/ {1} = {2:f3}", B, L, K);

                double one_by_K = (1 / K);
                sw.WriteLine(" and 1/K = (1/{0:f3}) = {1:f3}", K, one_by_K);
                sw.WriteLine();

                fcurv = new frmCurve(K, 1.0, 1.0, LoadType.FullyLoad);

                m1 = 0.049;
                m2 = 0.015;

               
                fcurv.txt_m1.Text = m1.ToString("f3");
                fcurv.txt_m2.Text = m2.ToString("f3");
                fcurv.ShowDialog();

                m1 = fcurv.m1;
                m2 = fcurv.m2;

                sw.WriteLine();
                sw.WriteLine("From Pigeaud's Curve, m1 = {0}  and m2 = {1}", m1, m2);
                sw.WriteLine();


                double MB2 = total_load_panel * (m1 + 0.15 * m2);
                MB2 = double.Parse(MB2.ToString("0.00"));
                sw.WriteLine("Bending Moment along short span = MB2");
                sw.WriteLine("                                = {0}*(m1+0.15*m2)", total_load_panel);
                sw.WriteLine("                                = {0}*({1}+0.15*{2})", total_load_panel, m1, m2);
                sw.WriteLine("                                = {0} kN-m", MB2);
                sw.WriteLine();

                double ML2 = total_load_panel * (m2 + 0.15 * m1);
                ML2 = double.Parse(ML2.ToString("0.00"));
                sw.WriteLine("Bending Moment along long span = ML2");
                sw.WriteLine("                                = {0}*(m2+0.15*m1)", total_load_panel);
                sw.WriteLine("                                = {0}*({1}+0.15*{2})", total_load_panel, m2, m1);
                sw.WriteLine("                                = {0} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Design B.M. including continuity Factor = CF * {0}", MB2);
                sw.WriteLine("                                        = {0} * {1}", CF, MB2);
                MB2 = CF * MB2;
                MB2 = double.Parse(MB2.ToString("0.00"));
                sw.WriteLine("                                        = {0} kN-m", MB2);
                sw.WriteLine();

                sw.WriteLine("Design B.M. including continuity Factor = CF * {0}", ML2);
                sw.WriteLine("                                        = {0} * {1}", CF, ML2);
                ML2 = CF * ML2;
                ML2 = double.Parse(ML2.ToString("0.00"));
                sw.WriteLine("                                        = {0} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine();

                double fxd_ld_sh_frc = 0.5 * total_load * L1;
                fxd_ld_sh_frc = double.Parse(fxd_ld_sh_frc.ToString("0.00"));
                sw.WriteLine("Permanent Load Shear Force = 0.5*{0}*L1", total_load);
                sw.WriteLine("                       = 0.5*{0}*{1}", total_load, L1);
                sw.WriteLine("                       = {0} kN", fxd_ld_sh_frc);
                sw.WriteLine();
                sw.WriteLine();

                MB = MB1 + MB2;
                sw.WriteLine("Design Moments = MB = MB1 + MB2 = {0} + {1} = {2} kN-m", MB1, MB2, MB);
                ML = ML1 + ML2;
                sw.WriteLine("               = ML = ML1 + ML2 = {0} + {1} = {2} kN-m", ML1, ML2, ML);
                sw.WriteLine();
                #endregion

                #region STEP 5 : STRUCTURAL DESIGN OF DECK SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : STRUCTURAL DESIGN OF DECK SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double d = (MB * 10E5) / (Q * 1000);

                d = Math.Sqrt(d);
                d = double.Parse(d.ToString("0"));
                sw.WriteLine("Required Effective Depth = d = √((M*10E5)/(Q*b))");
                sw.WriteLine("                         = √(({0}*10E5)/({1}*1000))", MB, Q);
                sw.WriteLine("                         = {0} mm", d);
                sw.WriteLine();


                d = (int)(d / 10.0);
                d += 1.0;
                d = d * 10;
                sw.WriteLine();
                sw.WriteLine("Adopt effective depth = d = {0} mm", d);
                sw.WriteLine();
                sw.WriteLine("Required steel for reinforcement along short span ");

                double Ast = (MB * 10E5) / (sigma_st * j * d);

                Ast = double.Parse(Ast.ToString("0"));
                double _ast = Ast;
                sw.WriteLine(" Ast = (M*10E5)/(σ_st*j*d)");
                sw.WriteLine("     = ({0}*10E5)/({1}*{2}*{3})", MB, sigma_st, j, d);
                sw.WriteLine("     = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();


                double spacing, pro_ast;
                spacing = 200;
                do
                {
                    spacing -= 10;
                    pro_ast = (Math.PI * dm * dm / 4.0) * (1000 / spacing);
                    pro_ast = double.Parse(pro_ast.ToString("0"));

                    if (spacing < 90) break; //Chiranjit [2013 05 31]
                }
                while (pro_ast < _ast);
                _ast = pro_ast;
                sw.WriteLine("Use {0} mm diameter bars at {1} mm c/c spacing     Marked as (1) in Drawing", dm, spacing);
                sw.WriteLine();

                //Bars 12 mm Dia 
                _bd1 = string.Format("Bars {0} mm Dia", dm);
                //at 120 mm c/c
                _sp1 = string.Format("at {0} mm c/c", spacing);


                sw.WriteLine("Provided Ast = {0} sq.mm", pro_ast);
                sw.WriteLine();

                double d2 = d - (dm / 2.0) - (dd / 2.0);
                sw.WriteLine("Effective depth for long span using {0} mm dia bars", dd);
                sw.WriteLine("     d2 = d - (dm / 2.0) - (dd / 2.0)");
                sw.WriteLine("        = {0} - {1:f0} - {2:f0}", d, (dm / 2.0), (dd / 2.0));
                sw.WriteLine("        = {0} mm", d2);
                sw.WriteLine();

                Ast = (ML * 10E5) / (sigma_st * j * d);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Required steel along long span = Ast");
                sw.WriteLine("                               = (ML*10^6)/(σ_st*j*d)");
                sw.WriteLine("                               = ({0}*10^6)/({1}*{2}*{3})", ML, sigma_st, j, d);
                sw.WriteLine("                               = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine("Requirement of minimum reinforcement using HYSD bars is 0.15% of");

                Ast = 0.0015 * _Do * 1000 * 1000;
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("cross section area of Slab, Ast = 0.0015*Do*1000*1000");
                sw.WriteLine("                                = 0.0015 * {0} * 1000 * 1000", _Do);
                sw.WriteLine("                                = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();

                //spacing = 150;

                spacing = 140;
                do
                {
                    spacing += 10;
                    pro_ast = (Math.PI * dd * dd / 4.0) * (1000 / spacing);
                    pro_ast = double.Parse(pro_ast.ToString("0"));

                    if (spacing >= 300) break;
                }
                while (pro_ast < Ast);

                Ast = Math.PI * dd * dd / 4.0 * (1000.0 / spacing);
                Ast = double.Parse(Ast.ToString("f3"));
                sw.WriteLine("Use {0} mm diameter bars at {1} mm c/c spacing, Ast = {2} sq.mm.    Marked as (2) in Drawing", dd, spacing, Ast);
                //Bars 10 mm Dia 
                _bd2 = string.Format("Bars {0} mm Dia", dd);
                //at 150 mm c/c
                _sp2 = string.Format("at {0} mm c/c", spacing);


                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 6 : CHECK FOR SHEAR STRESS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : CHECK FOR SHEAR STRESS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double tau_v = (sh_frc_imp * 1000.0) / (1000.0 * d);
                //tau_v = double.Parse(tau_v.ToString("0.000"));
                sw.WriteLine("Nominal Shear Stress = τ_v");
                sw.WriteLine("                     = (V * 1000.0) / (1000.0 * d)");
                sw.WriteLine("                     = ({0} * 1000.0) / (1000.0 * {1})", sh_frc_imp, d);
                sw.WriteLine("                     = {0:G3} N/sq.mm", tau_v);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The permissible Shear Stress in slab = τ_c");
                sw.WriteLine("                                     = K1 * K2 * τ_co");
                sw.WriteLine();

                double _d = d / 1000.0;

                double K1 = 1.14 - 0.7 * _d;
                K1 = double.Parse(K1.ToString("0.000"));
                sw.WriteLine("Where K1 = 1.14-0.7*d");
                sw.WriteLine("         = 1.14-0.7*{0}", _d);
                sw.WriteLine("         = {0} ", _d);
                if (K1 < 0.5)
                {
                    K1 = 0.5;
                    sw.WriteLine("         = {0} ", _d);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("  and K2 = 0.5+0.25*p");
                sw.WriteLine();
                double p = (_ast * 100.0) / (1000.0 * d);
                p = double.Parse(p.ToString("0.000"));
                sw.WriteLine("      p = Ast*100/b*d");
                sw.WriteLine("        = {0}*100/(1000*{1})", _ast, d);
                sw.WriteLine("        = {0}", p);
                sw.WriteLine();
                double K2 = 0.5 + 0.25 * p;
                sw.WriteLine(" K2 = 0.5 + 0.25 * {0}", p);
                sw.WriteLine("    = {0}", K2);

                if (K2 < 1.0)
                {
                    K2 = 1.0;
                    sw.WriteLine("    = {0} (K2 <= 1.0) ", K2);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine();
                //sw.WriteLine("For M20 Grade Concrete, Refering to Table 2 (Given at the end of the Report)");
                //int con_grade = 20;  
                int con_grade = (int)fck;  // Chiranjit [2013 05 31]
                sw.WriteLine("For M{0} Grade Concrete, Refering to Table 2 (Given at the end of the Report)", con_grade);
                double tau_co = 0.34;
                switch (con_grade)
                {
                    case 15:
                        tau_co = 0.28;
                        break;
                    case 20:
                        tau_co = 0.34;
                        break;
                    case 25:
                        tau_co = 0.40;
                        break;
                    case 30:
                        tau_co = 0.45;
                        break;
                    case 35:
                        tau_co = 0.50;
                        break;
                    case 40:
                        tau_co = 0.50;
                        break;
                    default:
                        tau_co = 0.50;
                        break;

                }



                sw.WriteLine("τ_co = 0.34 N/sq.mm");
                sw.WriteLine();

                double tau_c = K1 * K2 * tau_co;
                tau_c = double.Parse(tau_c.ToString("0.000"));
                sw.WriteLine("τ_c = K1 * K2 * τ_co");
                sw.WriteLine("    = {0} * {1} * {2}", K1, K2, tau_co);
                sw.WriteLine("    = {0} N/sq.mm.", tau_c);
                sw.WriteLine();

                if (tau_v < tau_c)
                {
                    sw.WriteLine("Since τ_v < τ_c the shear stresses are within Safe permissible limits.");
                }
                else
                {
                    sw.WriteLine("Since τ_v > τ_c the shear stresses are NOT within Safe permissible limits.");
                }
                sw.WriteLine();
                #endregion

                Write_Table_1(sw);

                #region TABLE 2 : BASIC VALUES OF SHEAR STRESS τ_co (IRC:21-1987)
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 2 : BASIC VALUES OF SHEAR STRESS τ_co (IRC:21-1987)");
                sw.WriteLine("______________________________________________________________");
                sw.WriteLine("Concrete Grade   M 15    M 20    M 25    M 30    M 35    M 40");
                sw.WriteLine("-------------------------------------------------------------");
                sw.WriteLine("τ_co (N/sq.mm)   0.28    0.34    0.40    0.45    0.50    0.50");
                sw.WriteLine("______________________________________________________________");
                #endregion

                #region END OF REPORT
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



        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF RCC DECK SLAB : " + value;
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of RCC Deck Slab");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_PS_Slab.TXT");
                user_input_file = Path.Combine(system_path, "RCC_DECK_SLAB.FIL");
                drawing_path = Path.Combine(system_path, "RCC_DECK_SLAB_DRAWING.FIL");

                //btnProcess.Enabled = Directory.Exists(value);
                //btnReport.Enabled = File.Exists(user_input_file);
                //btnDrawing.Enabled = File.Exists(user_input_file);

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

        public double Get_Table_1_Value(double B_by_L, int indx, ref string ref_string)
        {

            return iApp.Tables.K_Val_Simply_Continous_Supported_Slab(B_by_L, indx, ref  ref_string);


            //B_by_L = Double.Parse(B_by_L.ToString("0.0"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "RccDeckSlab_Table_1.txt");

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
            //    if (B_by_L < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (B_by_L > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == B_by_L)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > B_by_L)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (B_by_L - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.00"));
            //return returned_value;
        }
        public void Write_Table_1(StreamWriter sw)
        {
            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "RccDeckSlab_Table_1.txt");
            List<string> lst_content = iApp.Tables.Get_Tables_K_Val_Simply_Continous_Supported_Slab();

            sw.WriteLine("--------");
            sw.WriteLine("TABLE 1 :");
            sw.WriteLine("--------");
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();
            sw.WriteLine();
            lst_content.Clear();
        }
        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "RCC_DECK_SLAB_DRAWING.FIL");
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
                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_bd2={0}", _bd2);
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
    public enum ePSC_I_GirderOption
    {
        None = -1,
        Analysis = 0,
        MainGirder = 1,
        DeckSlab = 3,
        CantileverSlab = 4,
        Abutment = 5,
        RCCPier_1 = 6,
        RCCPier_2 = 7,
        MovingLoad = 8,
        SimpleSectionData = 9,
        CompositeSectionData = 10,
        CableData = 11,
        ReinforcementData = 12,
    }

    public class PreStressedConcrete_Forces
    {
        public PSC_Force_Data LL_INNER_GIRDER { get; set; }
        public PSC_Force_Data LL_OUTER_GIRDER { get; set; }
        public PSC_Force_Data DL_INNER_GIRDER { get; set; }
        public PSC_Force_Data DL_OUTER_GIRDER { get; set; }
        public PreStressedConcrete_Forces()
        {
            LL_INNER_GIRDER = new PSC_Force_Data();
            LL_OUTER_GIRDER = new PSC_Force_Data();
            DL_INNER_GIRDER = new PSC_Force_Data();
            DL_OUTER_GIRDER = new PSC_Force_Data();
        }
    }
    public class PSC_Force_Data : List<double>
    {

        public PSC_Force_Data()
        {
            for (int i = 0; i < 9; i++)
            {
                Add(0.0);
            }
        }
        public PSC_Force_Data(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(0.0);
            }
        }
        public PSC_Force_Data(PSC_Force_Data obj)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                Add(obj[i]);
            }
        }

        public double F1 { get { return this[0]; } set { this[0] = value; } }
        public double F2 { get { return this[1]; } set { this[1] = value; } }
        public double F3 { get { return this[2]; } set { this[2] = value; } }
        public double F4 { get { return this[3]; } set { this[3] = value; } }
        public double F5 { get { return this[4]; } set { this[4] = value; } }
        public double F6 { get { return this[5]; } set { this[5] = value; } }
        public double F7 { get { return this[6]; } set { this[6] = value; } }
        public double F8 { get { return this[7]; } set { this[7] = value; } }
        public double F9 { get { return this[8]; } set { this[8] = value; } }
        public double SUM
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Count; i++) sum += this[i];
                return sum;
            }
        }


        public double UnitFactor;
        /// <summary>
        /// Get forces index from 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double Get_Force_Data(int index)
        {
            if (UnitFactor == 0.0) UnitFactor = 1.0;
            return this[index] * UnitFactor;
        }

        public string Format
        {
            get
            {
                return "{0,12:E3} {1,12:E3} {2,12:E3} {3,12:E3} {4,12:E3} {5,12:E3} {6,12:E3} {7,12:E3} {8,12:E3} ";
            }
        }
        public override string ToString()
        {
            return string.Format(Format, F1, F2, F3, F4, F5, F6, F7, F8, F9);
        }

        public string ToString(string separator)
        {
            string res = "";
            for (int i = 0; i < Count - 1; i += 2)
            {
                res += this[i].ToString("f3") + separator + this[i + 1].ToString("f3") + " ";
            }
            return res;
        }
        public string ToString(string separator, int startIndex, int lastIndex)
        {
            string res = "";
            for (int i = startIndex; i < lastIndex - 1; i += 2)
            {
                res += this[i].ToString("f3") + separator + this[i + 1].ToString("f3") + " ";
            }
            return res;
        }
        public static PSC_Force_Data operator +(PSC_Force_Data c1, PSC_Force_Data c2)
        {
            PSC_Force_Data pfd = new PSC_Force_Data(c2.Count);
            for (int i = 0; i < pfd.Count; i++)
            {
                pfd[i] = c1[i] + c2[i];
            }

            return pfd;
        }
        public static PSC_Force_Data operator -(PSC_Force_Data c1, PSC_Force_Data c2)
        {
            PSC_Force_Data pfd = new PSC_Force_Data(c2.Count);
            for (int i = 0; i < pfd.Count; i++)
            {
                pfd[i] = c1[i] - c2[i];
            }
            return pfd;
        }

        public static PSC_Force_Data operator /(PSC_Force_Data c1, PSC_Force_Data c2)
        {
            PSC_Force_Data pfd = new PSC_Force_Data(c2.Count);
            for (int i = 0; i < pfd.Count; i++)
            {
                pfd[i] = c1[i] / c2[i];
            }
            return pfd;
        }

        public static PSC_Force_Data operator *(PSC_Force_Data c1, PSC_Force_Data c2)
        {
            PSC_Force_Data pfd = new PSC_Force_Data(c2.Count);
            for (int i = 0; i < pfd.Count; i++)
            {
                pfd[i] = c1[i] * c2[i];
            }
            return pfd;
        }
        public static implicit operator PSC_Force_Data(double x)
        {
            PSC_Force_Data pfd = new PSC_Force_Data();
            for (int i = 0; i < pfd.Count; i++)
            {
                pfd[i] = x;
            }
            return pfd;
        }


        public void Set_Absolute()
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] = Math.Abs(this[i]);
            }
        }

    }

    public class PreStressedConcrete_SectionProperties
    {
        //Section Data Input
        public double W { get; set; }
        public double H { get; set; }
        public double b1 { get; set; }
        public double b2 { get; set; }
        public double d1 { get; set; }
        public double d2 { get; set; }
        public double d3 { get; set; }
        public double d4 { get; set; }
        public double d5 { get; set; }
        public double ds { get; set; }

        public string Title { get; set; }
        public PreStressedConcrete_SectionProperties()
        {
            W = 700;
            H = 2400;
            b1 = 175;
            b2 = 350;
            d1 = 150;
            d2 = 150;
            d3 = 1650;
            d4 = 150;
            d5 = 300;
            ds = 0.0;
        }

        #region 1. Central Vertical Web:
        public double CVW_b
        {
            get
            {
                return b2;
            }
        }
        public double CVW_d
        {
            get
            {
                return (d2 + d3 + d4);
            }
        }
        public double A1
        {
            get
            {
                return (CVW_b * CVW_d);
            }
        }
        public double X1
        {
            get
            {
                return ((CVW_d / 2.0) + d5);
            }
        }
        public double AX1
        {
            get
            {
                return A1 * X1;
            }
        }
        public double AXX1
        {
            get
            {
                return A1 * X1 * X1;
            }
        }
        public double Iself1
        {
            get
            {
                return ((CVW_b * CVW_d * CVW_d * CVW_d) / 12.0);
            }
        }
        #region Sample
        //b=b2=350.0 mm.
        //d=d2+d3+d4=150.0+1650.0+150.0=1950.0 mm.
        //Area=A1=bxd=350.000 x 1950.000=68250.000 Sq.mm.
        //Distance from Bottom Edge=X1=(d/2) +d5 = (1950/2)+300.0=1275.0 mm.
        //AX1=A1 x X1=8.7x10^8
        //AXX1= A1 x X1 x X1=1.11x10^12
        //Iself1=(b x d^3)/12=(350 x 1950^3)/12=2.16 x 10^11 Sq. Sq. mm.
        #endregion Sample
        #endregion Central Vertical Web:


        #region 2. Top Flange
        public double TF_b
        {
            get
            {
                return (b1 + b2 + b1);
            }
        }
        public double TF_d
        {
            get
            {
                return (d1);
            }
        }
        public double A2
        {
            get
            {
                return (TF_b * TF_d);
            }
        }
        public double X2
        {
            get
            {
                return (H - (d1 / 2));
            }
        }
        public double AX2
        {
            get
            {
                return A2 * X2;
            }
        }
        public double AXX2
        {
            get
            {
                return A2 * X2 * X2;
            }
        }
        public double Iself2
        {
            get
            {
                return ((TF_b * TF_d * TF_d * TF_d) / 12.0);
            }
        }
        #region Sample
        //b=b1+b2+b1=175.0+350.0+175.0=700.0 mm.
        //d=d1=150.0 mm.
        //Area=A2=bxd=700.000 x 150.000=105000.000 Sq.mm.
        //Distance from Bottom Edge=X2=H - (d1/2) = 2400.0 - (150/2)=2325.0 mm.
        //Iself2=(b x d^3)/12=(700 x 150^3)/12=1.97 x 10^8 Sq. Sq. mm.
        #endregion Sample
        #endregion Top Flange:

        #region 3. Upper Two Triangles:
        public double UTT_b
        {
            get
            {
                return b1;
            }
        }
        public double UTT_d
        {
            get
            {
                return (d2);
            }
        }
        public double A3
        {
            get
            {
                return (UTT_b * UTT_d);
            }
        }
        public double X3
        {
            get
            {
                return (H - d1 - (d2 / 3));
            }
        }
        public double AX3
        {
            get
            {
                return (A3 * X3);
            }
        }
        public double AXX3
        {
            get
            {

                return (A3 * X3 * X3);
            }
        }
        public double Iself3
        {
            get
            {
                return 2.0 * ((UTT_b * Math.Pow(UTT_d, 3.0)) / 36.0);
                //return 2.0 * ((UTT_b * Math.Pow(UTT_d, 3.0)) / 36.0);
            }
        }
        #region Sample
        //Upper Two Triangles
        //b=b1 =175.0mm.
        //d=d2=150.0 mm.
        //Area=A3=2xbxd=2x(175.000 x 150.000/2.0)=26250.000 Sq.mm.
        //Distance from Bottom Edge=X3=H –d1- (d2/3) = 2400.0 – 150.0 - (150/3)=2200.0 mm.
        //AX3=A3 x X3=5.78x10^7
        //AXX3= A3 x X3 x X3=1.27x10^11
        //Iself3=2x(b x d^3)/36=2x(175.000 x 150.000^3)/36=3.28 x 10^7 Sq. Sq. mm.
        #endregion Sample
        #endregion Upper Two Triangles


        #region 4. Lower Two Triangles
        public double LTT_b
        {
            get
            {
                return b1;
            }
        }
        public double LTT_d
        {
            get
            {
                return (d4);
            }
        }
        public double A4
        {
            get
            {
                return (LTT_b * LTT_d);
            }
        }
        public double X4
        {
            get
            {
                return ((LTT_d / 3.0) + d5);
            }
        }
        public double Iself4
        {
            get
            {
                return 2.0 * ((LTT_b * Math.Pow(LTT_d, 3.0)) / 36.0);
            }
        }
        public double AX4
        {
            get
            {
                return (A4 * X4);
            }
        }
        public double AXX4
        {
            get
            {
                return (A4 * X4 * X4);
            }
        }
        #region Sample
        //Lower Two Triangles
        //b=b1 =175.0mm.
        //d=d4=150.0 mm.
        //Area=A4=2xbxd=2x(175.000 x 150.000/2.0)=26250.000 Sq.mm.
        //Distance from Bottom Edge=X4=(d/3) + d5= 50.0 + 300.0=350.0 mm.
        //AX4=A4 x X4=9.19x10^6
        //AXX4= A4 x X4 x X4=3.22x10^9
        //Iself4=2x(b x d^3)/36=2x(175.000 x 150.000^3)/36=3.28 x 10^7 Sq. Sq. mm.
        #endregion Sample
        #endregion Lower Two Triangles

        #region 5. Bottom Flange
        public double BF_b
        {
            get
            {
                return (b1 + b2 + b1);
            }
        }
        public double BF_d
        {
            get
            {
                return (d5);
            }
        }
        public double A5
        {
            get
            {
                return (BF_b * BF_d);
            }
        }
        public double X5
        {
            get
            {
                return (d5 / 2);
            }
        }
        public double Iself5
        {
            get
            {
                return ((BF_b * Math.Pow(BF_d, 3.0)) / 12.0);
            }
        }
        public double AX5
        {
            get
            {
                return (A5 * X5);
            }
        }
        public double AXX5
        {
            get
            {
                return (A5 * X5 * X5);
            }
        }
        #region Sample
        ////Bottom Flange
        //b=b1+b2+b1=175.0+350.0+175.0=700.0 mm.
        //d=d5=300.0 mm.
        //Area=A5=bxd=700.000 x 300.000=210000.000 Sq.mm.
        //Distance from Bottom Edge=X5= d5/2 = 300/2=150.0 mm.
        //AX5=A5 x X5=3.15x10^7
        //AXX5= A5 x X5 x X5=4.72x10^9
        //Iself5=(b x d^3)/12=(700 x 300^3)/12=1.58 x 10^9 Sq. Sq. mm.
        #endregion Sample
        #endregion Lower Two Triangles

        #region 6. Deck Slab
        public double DS_b
        {
            get
            {
                return (W);
            }
        }
        public double DS_d
        {
            get
            {
                return (ds);
            }
        }
        public double As
        {
            get
            {
                return (DS_b * DS_d);
            }
        }
        public double Xs
        {
            get
            {
                return (H + (ds / 2));
            }
        }
        public double Iselfs
        {
            get
            {
                return ((DS_b * Math.Pow(DS_d, 3.0)) / 12.0);
            }
        }
        public double AXs
        {
            get
            {
                return (As * Xs);
            }
        }
        public double AXXs
        {
            get
            {
                return (As * Xs * Xs);
            }
        }
        #region Sample
        ////Bottom Flange
        //b=b1+b2+b1=175.0+350.0+175.0=700.0 mm.
        //d=d5=300.0 mm.
        //Area=A5=bxd=700.000 x 300.000=210000.000 Sq.mm.
        //Distance from Bottom Edge=X5= d5/2 = 300/2=150.0 mm.
        //AX5=A5 x X5=3.15x10^7
        //AXX5= A5 x X5 x X5=4.72x10^9
        //Iself5=(b x d^3)/12=(700 x 300^3)/12=1.58 x 10^9 Sq. Sq. mm.
        #endregion Sample
        #endregion Lower Two Triangles


        public double A
        {
            get
            {
                return (A1 + A2 + A3 + A4 + A5 + As);
            }
        }
        //        A=A1+A2+A3+A4+A5
        //    =68250.000+105000.000+26250.000+26250.000+210000.000
        //    =435750.000
        public double AX
        {
            get
            {
                return (AX1 + AX2 + AX3 + AX4 + AX5 + AXs);
            }
        }
        //AX = AX1+AX2+AX3+AX4+AX5
        //      =8.7x10^8+2.44x10^8+5.78x10^7+9.19x10^6+3.15x10^7
        //      =1.21x10^9
        public double AXX
        {
            get
            {
                return (AXX1 + AXX2 + AXX3 + AXX4 + AXX5 + AXXs);
            }
        }
        //AXX= AXX1+AXX2+AXX3+AXX4+AXX5
        //        =1.11x10^12+5.68x10^11+1.27x10^11+3.22x10^9+4.72x10^9
        //        =1.81x10^12
        public double Iself
        {
            get
            {
                return (Iself1 + Iself2 + Iself3 + Iself4 + Iself5);
            }
        }
        //Iself= Iself1+Iself2+Iself3++Iself4+Iself5
        //        =2.16 x 10^11+1.97 x 10^8+3.28 x 10^7+3.28 x 10^7+1.58 x 10^9
        //        =2.18x10^11
        public double Yb
        {
            get
            {
                return (AX / A);
            }
        }
        //Yb = Distance of Neutral Axis (N-A) from bottom edge = Ax/A = 1.21 x 10^9 / 105,00,00 = 1155.000 mm.
        public double Yt
        {
            get
            {
                return (H + ds - Yb);
            }
        }
        //Yt = Distance of Neutral Axis (N-A) from top edge = H – Yb = 2400.000 – 1155.000 = 1245.000 mm.
        public double Ixx
        {
            get
            {
                return (Iself + (AXX - (AX * Yb)));
            }
        }
        //Izz=Moment of Inertia (MI) about N-A 
        //     = Iself + AXX-(AX x Yb) 
        //     = 2.18 x 10^11+1.81x10^12-(1.21x10^9x1155.000)
        //     =6.30 x 10^11 Sq. Sq.mm.


        //public double Ixx //Chiranjit [2013 06 30]
        public double Iyy
        {
            get
            {
                return (3.6E10);
            }
        }
        //Ixx=3.6x10^10
        public double Zt
        {
            get
            {
                //return (Izz / Yt);//Chiranjit [2013 06 30]
                return (Ixx / Yt);
            }
        }
        //Zt=Section Modulus about top edge=Izz/Yt=6.30 x 10^11 / 1245.000 = 5.06 x 10^8 Cu.mm.
        public double Zb
        {
            get
            {
                //return (Izz / Yb);//Chiranjit [2013 06 30]
                return (Ixx / Yb);//Chiranjit [2013 06 30]
            }
        }
        //Zb= Section Modulus about bottom edge=Izz/Yb=6.30x10^11 / 1155.000 = 5.46 x 10^8 Cu. mm.

        //public double Iyy //Chiranjit [2013 06 30]
        public double Izz
        {
            get
            {
                //return 0;//Chiranjit [2013 06 30]
                return (Ixx + Iyy);
            }
        }


        public double AX_Sq_M
        {
            get
            {
                return (A / Math.Pow(10, 6));
            }
        }
        public double IX
        {
            get
            {
                return (Ixx / Math.Pow(10, 12));
            }
        }
        public double IZ
        {
            get
            {
                return (Izz / Math.Pow(10, 12));
            }
        }

        public string Table_HEAD
        {
            get
            {
                //435750.000   1155.000   1245.000  6.30x10^11  3.6x10^10  0.0  5.06x10^8  5.46x10^8 
                //Chiranjit [2013 06 30]
                //return (string.Format("{0,15:f3} {1,10:f3} {2,10:f3} {3,12:E3} {4,12:E3} {5,9:f3} {6,12:E3} {7,12:E3}",
                //    "   A    ", "   Yb   ", "   Yt   ", "   Izz   ", "  Ixx  ", " Iyy ", "   Zt   ", "  Zb   "));


                return (string.Format("{0,15:f3} {1,10:f3} {2,10:f3} {3,12:E3} {4,12:E3} {5,9:f3} {6,12:E3} {7,12:E3}",
                    "   A    ", "   Yb   ", "   Yt   ", "   Ixx   ", "  Iyy  ", " Izz ", "   Zt   ", "  Zb   "));
            }
        }

        public string Table_Unit
        {
            get
            {
                return (string.Format("{0,15:f3} {1,10:f3} {2,10:f3} {3,12:E3} {4,12:E3} {5,9:f3} {6,12:E3} {7,12:E3}",
                    " (mm^2) ", "  (mm)  ", "  (mm)  ", "  (mm^4) ", " (mm^4)", "(mm^4)", " (mm^3)", "(mm^3) "));
            }
        }
        public List<string> Get_Details()
        {

            List<string> list = new List<string>();
            list.Add("");

            string kStr = "";

            for (int k = 0; k < Title.Length; k++) kStr += "-";

            list.Add(kStr);
            list.Add(Title);
            list.Add(kStr);
            list.Add("");
            list.Add(string.Format("A   = {0:E3} sq.mm", A));
            list.Add("");
            list.Add(string.Format("Yb  = {0:f3} mm", Yb));
            list.Add(string.Format("Yt  = {0:f3} mm", Yt));
            list.Add("");
            list.Add(string.Format("Ixx = {0:E3} sq.sq.mm", Ixx));
            list.Add("");
            list.Add(string.Format("Iyy = {0:E3} sq.sq.mm", Iyy));
            list.Add("");
            list.Add(string.Format("Izz = {0:E3} sq.sq.mm", Izz));
            list.Add("");
            list.Add(string.Format("Zt  = {0:E3} cu.mm", Zt));
            list.Add(string.Format("Zb  = {0:E3} cu.mm", Zb));
            list.Add("");
            list.Add("");
            return list;
        }
        public string Table_Row
        {
            get
            {
                //435750.000   1155.000   1245.000  6.30x10^11  3.6x10^10  0.0  5.06x10^8  5.46x10^8
                return (string.Format("{0,15:E3} {1,10:f3} {2,10:f3} {3,12:E3} {4,12:E3} {5,12:E3} {6,12:E3} {7,12:E3}",
                    A, Yb, Yt, Ixx, Iyy, Izz, Zt, Zb));
            }
        }

        public List<string> Get_Results()
        {
            List<string> list = new List<string>();
            list.Add(string.Format(""));

            string kStr = "";

            for (int k = 0; k < Title.Length; k++) kStr += "-";
             list.Add(string.Format(kStr));
            list.Add(string.Format("{0}", Title));
            list.Add(string.Format(kStr));
            list.Add(string.Format("Section Data"));
            list.Add(string.Format("------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("W   =  {0:f3} mm", W));
            list.Add(string.Format("H   =  {0:f3} mm", H));
            list.Add(string.Format("b1  =  {0:f3} mm", b1));
            list.Add(string.Format("b2  =  {0:f3} mm", b2));
            list.Add(string.Format("d1  =  {0:f3} mm", d1));
            list.Add(string.Format("d2  =  {0:f3} mm", d2));
            list.Add(string.Format("d3  =  {0:f3} mm", d3));
            list.Add(string.Format("d4  =  {0:f3} mm", d4));
            list.Add(string.Format("d5  =  {0:f3} mm", d5));
            if (ds != 0.0)
                list.Add(string.Format("ds  =  {0:f3} mm", ds));
            list.Add(string.Format(""));
            list.Add(string.Format("Section Calculations :"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            if (A1 != 0.0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Central Vertical Web : "));
                list.Add(string.Format("----------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = b2 = {0:f3} mm.", CVW_b));
                list.Add(string.Format(""));
                list.Add(string.Format("d = d2+d3+d4 = {0:f3}+{1:f3}+{2:f3} = {3:f3} mm.", d2, d3, d4, CVW_d));
                list.Add(string.Format(""));
                list.Add(string.Format("Area = A1 = bxd = {0:f3} x {1:f3} = {2:f3} Sq.mm.", CVW_b, CVW_d, A1));
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from Bottom Edge = X1 = (d/2)+d5  = ({0:f3}/2)+{1:f3} = {2:f3} mm.", CVW_d, d5, X1));
                list.Add(string.Format(""));
                list.Add(string.Format("AX1 = A1 x X1 = {0:f3}*{1:f3} = {2:E3}", A1, X1, AX1));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX1 =  A1 x X1 x X1 = {0:f3}*{1:f3}*{1:f3} = {2:E3}", A1, X1, AXX1));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself1 = (b x d^3)/12 = ({0:f3} x {1:f3}^3)/12 = {2:E3} Sq. Sq. mm.", CVW_b, CVW_d, Iself1));
                list.Add(string.Format(""));
            }
            if (A2 != 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Top Flange :"));
                list.Add(string.Format("------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = b1+b2+b1 = {0:f3}+{1:f3}+{0:f3} = {2:f3} mm.", b1, b2, TF_b));
                list.Add(string.Format(""));
                list.Add(string.Format("d = d1 = {0:f3} mm.", TF_d));
                list.Add(string.Format(""));
                list.Add(string.Format("Area = A2 = bxd = {0:f3} x {1:f3} = {2:f3} Sq.mm.", TF_b, TF_d, A2));
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from Bottom Edge  =  X2  =  H - (d1/2)  =  {0:f3} - ({1:f3}/2) = {2:f3} mm.", H, d1, X2));
                list.Add(string.Format(""));
                list.Add(string.Format("AX2  =  A2 x X2 = {0:f3}*{1:f3} = {2:E3}", A2, X2, AX2));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX2 =  A2 x X2 x X2 = {0:f3}*{1:f3}*{1:f3} = {2:E3}", A2, X2, AXX2));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself2 = (b x d^3)/12 = ({0:f3} x {1:f3}^3)/12 = {2:E3} Sq. Sq. mm.", TF_b, TF_d, Iself2));
                list.Add(string.Format(""));
            }
            if (A3 != 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Upper Two Triangles :"));
                list.Add(string.Format("---------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = b1  = {0:f3} mm.", UTT_b));
                list.Add(string.Format(""));
                list.Add(string.Format("d = d2 = {0:f3} mm.", UTT_d));
                list.Add(string.Format(""));
                list.Add(string.Format("Area = A3 = 2xbx(d/2) = 2x({0:f3} x {1:f3}) = {2:f3} Sq.mm.", UTT_b, UTT_d / 2, A3));
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from Bottom Edge = X3 = H -d1- (d2/3)  =  {0:f3} - {1:f3} - ({2:f3}/3) = {3:f3} mm.",
                    H, d1, d2, X3));
                list.Add(string.Format(""));
                list.Add(string.Format("AX3 = A3 x X3 = {0:f3}*{1:f3} = {2:E3}", A3, X3, AX3));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX3 =  A3 x X3 x X3 = {0:f3}*{1:f3}*{1:f3} = {2:E3}", A3, X3, AXX3));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself3 = 2x(b x d^3)/36 = 2x({0:f3} x {1:f3}^3)/36 = {2:E3} Sq. Sq. mm.", UTT_b, UTT_d, Iself3));
                list.Add(string.Format(""));
            }
            if (A4 != 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Lower Two Triangles :"));
                list.Add(string.Format("---------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = b1  = {0:f3} mm.", LTT_b));
                list.Add(string.Format(""));
                list.Add(string.Format("d = d4 = {0:f3} mm.", LTT_d));
                list.Add(string.Format(""));
                list.Add(string.Format("Area = A4 = 2xbx(d/2) = 2x({0:f3} x {1:f3}) = {2:f3} Sq.mm.", LTT_b, LTT_d / 2, A4));
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from Bottom Edge = X4 = (d/3) + d5 = ({0:f3}/3) + {1:f3}  = {2:f3} mm.", LTT_d, d5, X4));
                list.Add(string.Format(""));
                list.Add(string.Format("AX4 = A4 x X4 = {0:f3}*{1:f3} = {2:E3}", A4, X4, AX4));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX4 =  A4 x X4 x X4 = {0:f3}*{1:f3}*{1:f3} = {2:E3} ", A4, X4, AXX4));
                //list.Add(string.Format("Iself4 = 2x(b x d^3)/36 = 2x(175.000 x 150.000^3)/36 = 3.28 x 10^7 Sq. Sq. mm."));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself4 = 2x(b x d^3)/36 = 2x({0:f3} x {1:f3}^3)/36 = {2:E3} Sq. Sq. mm.", LTT_b, LTT_d, Iself4));
                list.Add(string.Format(""));
            }
            if (A5 != 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Bottom Flange :"));
                list.Add(string.Format("---------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = b1+b2+b1 = {0:f3}+{1:f3}+{0:f3} = {2:f3} mm.", b1, b2, BF_b));
                list.Add(string.Format(""));
                list.Add(string.Format("d = d5 = {0:f3} mm.", BF_d));
                list.Add(string.Format(""));
                list.Add(string.Format("Area = A5 = bxd = {0:f3} x {1:f3} = {2:f3} Sq.mm.", BF_b, BF_d, A5));
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from Bottom Edge = X5 =  d5/2  =  {0:f3}/2 = {1:f3} mm.", d5, X5));
                list.Add(string.Format(""));
                list.Add(string.Format("AX5 = A5 x X5 = {0:f3}*{1:f3} = {2:E3} ", A5, X5, AX5));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX5 =  A5 x X5 x X5 = {0:f3}*{1:f3}*{1:f3} = {2:E3}", A5, X5, AXX5));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself5 = (b x d^3)/12 = ({0:f3} x {1:f3}^3)/12 = {2:E3} Sq. Sq. mm.", BF_b, BF_d, Iself5));
                list.Add(string.Format(""));
            }
            if (As != 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("Deck Slab :"));
                list.Add(string.Format("-----------"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = W = {0:f3} mm.", DS_b));
                list.Add(string.Format(""));
                list.Add(string.Format("d = ds = {0:f3} mm.", DS_d));
                list.Add(string.Format(""));
                list.Add(string.Format("Area = As = bxd = {0:f3} x {1:f3} = {2:f3} Sq.mm.", DS_b, DS_d, As));
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from Bottom Edge = Xs =  ds/2  =  {0:f3}/2 = {1:f3} mm.", DS_d, Xs));
                list.Add(string.Format(""));
                list.Add(string.Format("AXs = As x Xs = {0:f3}*{1:f3} = {2:E3} ", As, Xs, AXs));
                list.Add(string.Format(""));
                list.Add(string.Format("AXXs =  As x Xs x Xs = {0:f3}*{1:f3}*{1:f3} = {2:E3}", As, Xs, AXXs));
                list.Add(string.Format(""));
                list.Add(string.Format("Iselfs = (b x d^3)/12 = ({0:f3} x {1:f3}^3)/12 = {2:E3} Sq. Sq. mm.", DS_b, DS_d, Iselfs));
                list.Add(string.Format(""));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("RESULTS :"));
            list.Add(string.Format("--------"));
            if (ds == 0.0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("A  =  A1 + A2 + A3 + A4 + A5"));
                list.Add(string.Format("   =  {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3}", A1, A2, A3, A4, A5));
                list.Add(string.Format("   =  {0:f3}", A));
                list.Add(string.Format(""));
                list.Add(string.Format("AX  =  AX1 + AX2 + AX3 + AX4 + AX5"));
                list.Add(string.Format("    =  {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3}", AX1, AX2, AX3, AX4, AX5));
                list.Add(string.Format("    =  {0:E3}", AX));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX   =  AXX1 + AXX2 + AXX3 + AXX4 + AXX5"));
                list.Add(string.Format("      =  {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3}", AXX1, AXX2, AXX3, AXX4, AXX5));
                list.Add(string.Format("      =  {0:E3}", AXX));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself  =  Iself1 + Iself2 + Iself3 + Iself4 + Iself5"));
                list.Add(string.Format("       =  {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3}", Iself1, Iself2, Iself3, Iself4, Iself5));
                list.Add(string.Format("       =  {0:E3}", Iself));
            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("A  =  A1 + A2 + A3 + A4 + A5 + As"));
                list.Add(string.Format("   =  {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}", A1, A2, A3, A4, A5, As));
                list.Add(string.Format("   =  {0:f3}", A));
                list.Add(string.Format(""));
                list.Add(string.Format("AX  =  AX1 + AX2 + AX3 + AX4 + AX5 + AXs"));
                list.Add(string.Format("    =  {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3} + {5:E3}", AX1, AX2, AX3, AX4, AX5, AXs));
                list.Add(string.Format("    =  {0:E3}", AX));
                list.Add(string.Format(""));
                list.Add(string.Format("AXX   =  AXX1 + AXX2 + AXX3 + AXX4 + AXX5 + AXXs"));
                list.Add(string.Format("      =  {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3} + {5:E3}", AXX1, AXX2, AXX3, AXX4, AXX5, AXXs));
                list.Add(string.Format("      =  {0:E3}", AXX));
                list.Add(string.Format(""));
                list.Add(string.Format("Iself  =  Iself1 + Iself2 + Iself3 + Iself4 + Iself5 + Iselfs"));
                list.Add(string.Format("       =  {0:E3} + {1:E3} + {2:E3} + {3:E3} + {4:E3} + {5:E3}", Iself1, Iself2, Iself3, Iself4, Iself5, Iselfs));
                list.Add(string.Format("       =  {0:E3}", Iself));
            }





            list.Add(string.Format(""));
            list.Add(string.Format("Yb  =  Distance of Neutral Axis (N-A) from bottom edge  =  AX/A  =  {0:E3} / {1:f3}  =  {2:f3} mm.", AX, A, Yb));
            list.Add(string.Format("Yt  =  Distance of Neutral Axis (N-A) from top edge  =  {0}  =  {1:f3} - {2:f3}  =  {3:f3} mm.", ((ds == 0.0) ? "H - Yb" : "(H + ds) - Yb"), ((ds == 0.0) ? H : (H + ds)), Yb, Yt));
            list.Add(string.Format(""));
            //list.Add(string.Format("Izz   =  Moment of Inertia (MI) about N-A ")); //Chiranjit [2013 06 30]
            list.Add(string.Format("Ixx   =  Moment of Inertia (MI) about N-A "));
            list.Add(string.Format("      =  Iself + AXX-(AX x Yb) "));
            list.Add(string.Format("      =  {0:E3} + {1:E3} - ({2:E3}x{3:f3})", Iself, AXX, AX, Yb));
            list.Add(string.Format("      =  {0:E3} Sq. Sq.mm.", Ixx));
            list.Add(string.Format(""));
            list.Add(string.Format("Iyy  =  {0:E3} Sq. Sq.mm", Iyy)); //Chiranjit [2013 06 30]
            list.Add(string.Format(""));
            list.Add(string.Format("Izz  = Ixx + Iyy = {0:E3} + {1:E3} = {2:E3} Sq. Sq.mm", Ixx, Iyy, Izz));
            list.Add(string.Format(""));
            list.Add(string.Format("Zt  =  Section Modulus about top edge = Ixx/Yt = {0:E3} / {1:f3}  =  {2:E3} Cu.mm.", Ixx, Yt, Zt));
            list.Add(string.Format("Zb  =  Section Modulus about bottom edge = Ixx/Yb =  = {0:E3} / {1:f3}  =  {2:E3} Cu. mm.", Ixx, Yb, Zb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            return list;
        }

        public void Write_User_Input_Details(StreamWriter sw)
        {

            //foreach (var item in Get_Details())
            //{
            //    sw.WriteLine(item);
            //}

            string kStr = "";

            for (int k = 0; k < Title.Length; k++) kStr += "-";

            sw.WriteLine(kStr);
            sw.WriteLine(Title);
            sw.WriteLine(kStr);
            sw.WriteLine("---------------------------------");
            sw.WriteLine("Section Data");
            sw.WriteLine("---------------------------------");
            sw.WriteLine("");
            sw.WriteLine("W   =  {0:f3} mm", W);
            sw.WriteLine("H   =  {0:f3} mm", H);
            sw.WriteLine("b1  =  {0:f3} mm", b1);
            sw.WriteLine("b2  =  {0:f3} mm", b2);
            sw.WriteLine("d1  =  {0:f3} mm", d1);
            sw.WriteLine("d2  =  {0:f3} mm", d2);
            sw.WriteLine("d3  =  {0:f3} mm", d3);
            sw.WriteLine("d4  =  {0:f3} mm", d4);
            sw.WriteLine("d5  =  {0:f3} mm", d5);
            if (ds != 0.0)
                sw.WriteLine("ds  =  {0:f3} mm", ds);
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("---------------------------------");
            sw.WriteLine("---------------------------------");
        }

    }

    public class PSC_Cable_Data
    {
        public double ab1 { get; set; }
        public double ab2 { get; set; }
        public double dba { get; set; }
        public double vwc { get; set; }
        public double vfc { get; set; }
        public double idac { get; set; }
        public double abcd_nspc { get; set; }
        public double abcd_uss { get; set; }
        public double abcd_sf { get; set; }
        public double abcd_fss { get { return (abcd_uss * abcd_sf); } }
        public double abcd_fc { get { return (abcd_fss * abcd_nspc); } }
        public double abcd_csac { get; set; }
        public double e_nspc { get; set; }
        public double e_uss { get; set; }
        public double e_sf { get; set; }
        public double e_fss { get { return (e_uss * e_sf); } }
        public double e_fc { get { return (e_fss * e_nspc); } }
        public double e_csac { get; set; }
        public double cover1 { get; set; }
        public double cover2 { get; set; }
        public double D { get; set; }


        public PSC_Cable_Data()
        {
            ab1 = 0.0;
            ab2 = 0.0;
            dba = 0.0;
            vwc = 0.0;
            vfc = 0.0;
            idac = 0.0;
            abcd_nspc = 0.0;
            abcd_uss = 0.0;
            abcd_sf = 0.0;
            //abcd_fss = 0.0;
            //abcd_fc = 0.0;
            abcd_csac = 0.0;
            e_nspc = 0.0;
            e_uss = 0.0;
            e_sf = 0.0;
            //e_fss = 0.0;
            //e_fc = 0.0;
            e_csac = 0.0;
            cover1 = 0.0;
            cover2 = 0.0;
            D = 0.0;
        }
        public void Write_Cable_Data(StreamWriter sw)
        {
            //sw.WriteLine("3. CABLE DATA");
            //sw.WriteLine("-------------");
            //sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("Cable make                = BBR Cona Compact make");
            sw.WriteLine("Sheating type             = Corrugated HDPE pipe of approved make");
            sw.WriteLine("Anchorage Block size      = {0} mm x {1} mm",  ab1,  ab2);
            sw.WriteLine("Distance between anchorages                 = {0} mm",  dba);
            //sw.WriteLine("Value of Wooble Coeff                                 = {0} per metre length of Cable (IRC: 18-2000)");
            sw.WriteLine("Value of Wooble Coeff                                 = {0} per metre length of Cable",  vwc);
            sw.WriteLine("Value of Friction Coeff                 = {0}",  vfc);
            //sw.WriteLine("Value of Friction Coeff                 = 0.17 (IRC: 18-2000)");
            sw.WriteLine("Inside Diameter of assembled conduit = {0} mm.",  idac);
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("");
            double nsc1 =  abcd_nspc;
            double uss1 =  abcd_uss;
            double stess_fac1 =  abcd_sf;
            double fss1 = uss1 * stess_fac1;
            double fcb1 = fss1 * nsc1;
            double x_area1 =  abcd_csac;

            if (D >= 500.0)
                sw.WriteLine("D = {0:f2} mm.   >=  500.0", D);
            else
                sw.WriteLine("D = {0:f2} mm.   <  500.0", D);

            //sw.WriteLine("D = 2400.0 mm.> 500.0");
          
            //foreach (var item in mid_sections.Get_Details())
            //{
            //    sw.WriteLine(item);
            //}
            //sw.WriteLine();
            sw.WriteLine("Cover2 = {0:f2} mm.", cover2);
            sw.WriteLine("Cover1 = {0:f2} mm. {1} Cover2", cover1, (cover1 < cover2 ? "<" : ">"));


            sw.WriteLine("For Cables a, b, c & d ");
            //sw.WriteLine("Number of Strands per cable = 19");
            //sw.WriteLine("Ultimate Strength per Strand = 183.7 kN");
            //sw.WriteLine("Stressing Factor = 0.765");
            //sw.WriteLine("Factored Strength per Strand = 183.7 x 0.765 = 140.5305 kN");
            //sw.WriteLine("Force per Cable = 140.5305 x 19 = 2670.0795 kN");
            //sw.WriteLine("Cross section area of each of four Cables = 1875.3 Sq.mm");
            sw.WriteLine("Number of Strands per cable = {0}", nsc1);
            sw.WriteLine("Ultimate Strength per Strand = {0} kN", uss1);
            sw.WriteLine("Stressing Factor = {0}", stess_fac1);
            sw.WriteLine();
            sw.WriteLine();
            double nsc2 =  e_nspc;
            double uss2 =  e_uss;
            double stess_fac2 =  e_sf;
            double fss2 = uss2 * stess_fac2;
            double fcb2 = fss2 * nsc2;
            double x_area2 = e_csac;
            double D1 = D - 2 * cover2;
            double P = fcb1 * 4 + fcb2;
            double _As = x_area1 * 4 + x_area2;
            sw.WriteLine("For Cables e ");
            sw.WriteLine("Number of Strands per cable = {0}", nsc2);
            sw.WriteLine("Ultimate Strength per Strand = {0} kN", uss2);
            sw.WriteLine("Stressing Factor = {0}", stess_fac2);



            sw.WriteLine();
            //sw.WriteLine("For Cables e ");
            sw.WriteLine("Factored Strength per Strand = {0} x {1} = {2:f3} kN", uss1, stess_fac1, fss1);
            sw.WriteLine("Force per Cable = {0:f3} x {1} = {2:f3} kN", fss1, nsc1, fcb1);
            sw.WriteLine("Cross section area of Cable ‘e’ = {0:f3} Sq.mm", x_area1);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Factored Strength per Strand = {0} x {1} = {2:f3} kN", uss2, stess_fac2, fss2);
            sw.WriteLine("Force per Cable = {0:f3} x {1} = {2:f3} kN", fss2, nsc2, fcb2);
            sw.WriteLine("Cross section area of Cable ‘e’ = {0:f3} Sq.mm", x_area2);
            sw.WriteLine();
        }
    }

    //Chiranjit [2012 08 21]
    public class PSC_Reinforcement_Data
    {

        //----------------------------------------
        //DESIGN OF SHEAR REINFORCEMENTS (STEP 12)
        //----------------------------------------
        public double DSR_Asv_dia { get; set; }
        public double DSR_Asv_spacing { get; set; }
        public double DSR_Asv_Legs_End_span { get; set; }
        public double DSR_Asv_Legs_Mid_span { get; set; }
        //public double DSR_Asv_Legs_Mid_span { get; set; }

        //Diameter of Shear Reinformnet Bars = Asv_dia = 10 mm.
        //Spacing of Shear Reinformnet Stirrups = Asv_spacing = 200 mm.
        //Legs of Shear Reinformnet Stirrups at End Span = Asv_Legs_End_span = 4 for Sec (1-1 & 2-2)
        //Legs of Shear Reinformnet Stirrups at Mid Span = Asv_Legs_Mid_span = 2 for Sec (3-3 to 9-9)


        //-------------------------------------------------------------
        //DESIGN OF REINFORCEMENTS FOR BURSTING TENSILE FORCE (STEP 13)
        //-------------------------------------------------------------
        public double DRBTF_Ast_dia { get; set; }
        public double DRBTF_Ast_Nos { get; set; }
        public double DRBTF_Ast_Layers { get; set; }

        //Diameter of Reinformnet Bars = Ast_dia = 16 mm.
        //Numbers of Reinformnet Bars = Ast_Nos = 4
        //Layers of Reinformnet Bars = Ast_Layers = 4

        //------------------------------------
        //DESIGN OF SHEAR CONNECTORS (STEP 14)
        //------------------------------------
        public double DSC_Ash_dia { get; set; }
        public double DSC_Ash_Leg { get; set; }
        public double DSC_Ash_Spacing { get; set; }
        public double DSC_fy { get; set; }

        //Diameter of Shear Connector Bars = Ash_dia = 10 mm.
        //Legs of Shear Connectors = Ash_Leg = 4
        //Spacing of Shear Connectors = Ash_Spacing = 150 mm.
        //Steel Grade of Shear Connectors = Fe 415 (fy=415 N/Sq.mm)

        //-----------------------------------------------------------------------
        //DESIGN OF TRANSVERSE SHEAR REINFORCEMENTS (STEP 15)
        //-----------------------------------------------------------------------
        public double DTSR_fy { get; set; }
        public double DTSR_sigma_y { get; set; }
        public double DTSR_Ls { get; set; }
        public double DTSR_fck { get; set; }
        public double DTSR_Ats_dia { get; set; }
        public double DTSR_Ats_Spacing { get; set; }

        //Reinforcement Steel Grade = fy = 500 N/Sq.mm,  
        //Permissible tensile stress in steel = σ_y = 240 N/Sq. mm = 0.240 kN/Sq.mm
        //Length of Shear Plane = Ls = 500.000 mm.
        //Concrete Grade = fck = 45 N/Sq.mm
        //Diameter of Transverse Shear Reinforcement Bars = Ats_dia = 12 mm.
        //Spacing of Transverse Shear Reinforcements = Ats_Spacing = 135.000 mm.

        //------------------------------------------
        //DESIGN OF END DIAPHRAGM (STEP 16)
        //--------------------------------
        public double DED_b { get; set; }
        public double DED_D { get; set; }
        public double DED_cover { get; set; }
        public double DED_dia_Ast1 { get; set; }
        public double DED_Nos_Ast1 { get; set; }
        public double DED_dia_Ast2 { get; set; }
        public double DED_Nos_Ast2 { get; set; }
        public double DED_dia_dsh { get; set; }
        public double DED_Leg_dsh { get; set; }
        public double DED_Spacing_dsh { get; set; }

        //Width of Diaphragm = b = 350.0 mm, 
        //Total Depth of Diaphragm = D = 2350.0 mm
        //Reinforcement Clear Cover = cover = 50.0 mm, 

        //dia_Ast1 = 25 mm,  Nos_Ast1 = 4, 
        //dia_Ast2 = 0 mm,  Nos_Ast2 = 0,

        //dia_dsh = 12 mm.,  Leg_dsh = 2,  Spacing_dsh = 150 mm.

        //------------------------------------------------------
        //DESIGN OF INTERMEDIATE DIAPHRAGM (STEP 17)
        //-----------------------------------------

        public double DID_b { get; set; }
        public double DID_D { get; set; }
        public double DID_cover { get; set; }
        public double DID_dia_Ast1 { get; set; }
        public double DID_Nos_Ast1 { get; set; }
        public double DID_dia_Ast2 { get; set; }
        public double DID_Nos_Ast2 { get; set; }
        public double DID_dia_dsh { get; set; }
        public double DID_Leg_dsh { get; set; }
        public double DID_Spacing_dsh { get; set; }

        //Width of Diaphragm = b = 350.0 mm, 
        //Total Depth of Diaphragm = D = 2350.0 mm
        //Reinforcement Clear Cover = cover = 50.0 mm, 

        //Diameter of Reinforcement Bars = dia_Ast1 = 25 mm,  
        //Number of Reinforcement Bars = Nos_Ast1 = 4
        //Diameter of Reinforcement Bars = dia_Ast2 = 25 mm,  
        //Number of Reinforcement Bars = Nos_Ast2 = 4

        //Diameter of Shear Reinforcement Bars = dia_dsh = 12 mm.,  
        //Legs of Shear Reinforcement Bars = Leg_dsh = 2,  
        //Spacing of Shear Reinforcement Bars = Spacing_dsh = 150 mm.


        public PSC_Reinforcement_Data()
        {
            DSR_Asv_dia = 0.0;
            DSR_Asv_spacing = 0.0;
            DSR_Asv_Legs_End_span = 0.0;
            DSR_Asv_Legs_Mid_span = 0.0;
            DSR_Asv_Legs_Mid_span = 0.0;

            DRBTF_Ast_dia = 0.0;
            DRBTF_Ast_Nos = 0.0;
            DRBTF_Ast_Layers = 0.0;

            DSC_Ash_dia = 0.0;
            DSC_Ash_Leg = 0.0;
            DSC_Ash_Spacing = 0.0;
            DSC_fy = 0.0;

            DTSR_fy = 0.0;
            DTSR_sigma_y = 0.0;
            DTSR_Ls = 0.0;
            DTSR_fck = 0.0;
            DTSR_Ats_dia = 0.0;
            DTSR_Ats_Spacing = 0.0;

            DED_b = 0.0;
            DED_D = 0.0;
            DED_cover = 0.0;
            DED_dia_Ast1 = 0.0;
            DED_Nos_Ast1 = 0.0;
            DED_dia_Ast2 = 0.0;
            DED_Nos_Ast2 = 0.0;
            DED_dia_dsh = 0.0;
            DED_Leg_dsh = 0.0;
            DED_Spacing_dsh = 0.0;

            DID_b = 0.0;
            DID_D = 0.0;
            DID_cover = 0.0;
            DID_dia_Ast1 = 0.0;
            DID_Nos_Ast1 = 0.0;
            DID_dia_Ast2 = 0.0;
            DID_Nos_Ast2 = 0.0;
            DID_dia_dsh = 0.0;
            DID_Leg_dsh = 0.0;
            DID_Spacing_dsh = 0.0;

        }

        public void Write_Input_Data(StreamWriter sw)
        {
            #region Chiranjit [2012 08 21]

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("--------------------------");
            sw.WriteLine("STEEL REINFORCEMENT DATA :");
            sw.WriteLine("--------------------------");
            sw.WriteLine("");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine("DESIGN OF SHEAR REINFORCEMENTS");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Diameter of Shear Reinformnet Bars = Asv_dia = {0} mm.", DSR_Asv_dia);
            sw.WriteLine("Spacing of Shear Reinformnet Stirrups = Asv_spacing = {0} mm.", DSR_Asv_spacing);
            sw.WriteLine("Legs of Shear Reinformnet Stirrups at End Span = Asv_Legs_End_span = {0} for Sec (1-1 & 2-2)", DSR_Asv_Legs_End_span);
            sw.WriteLine("Legs of Shear Reinformnet Stirrups at Mid Span = Asv_Legs_Mid_span = {0} for Sec (3-3 to 9-9)", DSR_Asv_Legs_Mid_span);
            sw.WriteLine("");
            sw.WriteLine("-------------------------------------------------------------");
            sw.WriteLine("DESIGN OF REINFORCEMENTS FOR BURSTING TENSILE FORCE");
            sw.WriteLine("-------------------------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Diameter of Reinformnet Bars = Ast_dia = {0} mm.", DRBTF_Ast_dia);
            sw.WriteLine("Numbers of Reinformnet Bars = Ast_Nos = {0}", DRBTF_Ast_Nos);
            sw.WriteLine("Layers of Reinformnet Bars = Ast_Layers = {0}", DRBTF_Ast_Layers);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------");
            sw.WriteLine("DESIGN OF SHEAR CONNECTORS");
            sw.WriteLine("------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Diameter of Shear Connector Bars = Ash_dia = {0} mm.", DSC_Ash_dia);
            sw.WriteLine("Legs of Shear Connectors = Ash_Leg = {0}", DSC_Ash_Leg);
            sw.WriteLine("Spacing of Shear Connectors = Ash_Spacing = {0} mm.", DSC_Ash_Spacing);
            sw.WriteLine("Steel Grade of Shear Connectors = Fe {0} (fy={0} N/Sq.mm)", DSC_fy);
            sw.WriteLine();
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("DESIGN OF TRANSVERSE SHEAR REINFORCEMENTS");
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Reinforcement Steel Grade = fy = {0} N/Sq.mm,  ", DTSR_fy);
            sw.WriteLine("Permissible tensile stress in steel = σ_y = {0} N/Sq. mm = {1:f3} kN/Sq.mm", DTSR_sigma_y, (DTSR_sigma_y/1000.0));
            sw.WriteLine("Length of Shear Plane = Ls = {0:f3} mm.", DTSR_Ls);
            sw.WriteLine("Concrete Grade = fck = {0} N/Sq.mm", DTSR_fck);
            sw.WriteLine("Diameter of Transverse Shear Reinforcement Bars = Ats_dia = {0} mm.", DTSR_Ats_dia);
            sw.WriteLine("Spacing of Transverse Shear Reinforcements = Ats_Spacing = {0:f3} mm.", DTSR_Ats_Spacing);
            sw.WriteLine("");
            sw.WriteLine("------------------------------------------");
            sw.WriteLine("DESIGN OF END DIAPHRAGM");
            sw.WriteLine("------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Width of Diaphragm = b = {0} mm, ", DED_b);
            sw.WriteLine("Total Depth of Diaphragm = D = 2350.0 mm", DED_D);
            sw.WriteLine("Reinforcement Clear Cover = cover = 50.0 mm, ", DED_cover);
            sw.WriteLine("");
            sw.WriteLine("dia_Ast1 = {0} mm,  Nos_Ast1 = {1}, ", DED_dia_Ast1, DED_Nos_Ast1);
            sw.WriteLine("dia_Ast2 = {0} mm,  Nos_Ast2 = {1},", DED_dia_Ast2, DED_Nos_Ast2);
            sw.WriteLine();
            sw.WriteLine("dia_dsh = {0} mm.,  Leg_dsh = {1},  Spacing_dsh = {2} mm.", DED_dia_dsh, DED_Leg_dsh, DED_Spacing_dsh);
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------");
            sw.WriteLine("DESIGN OF INTERMEDIATE DIAPHRAGM");
            sw.WriteLine("------------------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Width of Diaphragm = b = {0:f3} mm, ", DID_b);
            sw.WriteLine("Total Depth of Diaphragm = D = {0:f3} mm", DID_D);
            sw.WriteLine("Reinforcement Clear Cover = cover = {0:f3} mm, ", DID_cover);
            sw.WriteLine("");
            sw.WriteLine("Diameter of Reinforcement Bars = dia_Ast1 = {0:f3} mm,  ", DID_dia_Ast1);
            sw.WriteLine("Number of Reinforcement Bars = Nos_Ast1 = {0:f0}", DID_Nos_Ast1);
            sw.WriteLine("Diameter of Reinforcement Bars = dia_Ast2 = {0:f3} mm,  ", DID_dia_Ast2);
            sw.WriteLine("Number of Reinforcement Bars = Nos_Ast2 = {0:f0}", DID_Nos_Ast2);
            sw.WriteLine("");
            sw.WriteLine("Diameter of Shear Reinforcement Bars = dia_dsh = {0:f3} mm.,  ", DID_dia_dsh);
            sw.WriteLine("Legs of Shear Reinforcement Bars = Leg_dsh = {0},  ", DID_Leg_dsh);
            sw.WriteLine("Spacing of Shear Reinforcement Bars = Spacing_dsh = {0} mm.", DID_Spacing_dsh);
            sw.WriteLine();
            #endregion Chiranjit [2012 08 21]
        }

        public void Write_Data(StreamWriter sw)
        {
            #region Chiranjit [2012 08 21]

            sw.WriteLine("");
            sw.WriteLine("STEEL REINFORCEMENT DATA :");
            sw.WriteLine("--------------------------");
            sw.WriteLine("");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine("DESIGN OF SHEAR REINFORCEMENTS (STEP 12)");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Diameter of Shear Reinformnet Bars = Asv_dia = {0} mm.", DSR_Asv_dia);
            sw.WriteLine("Spacing of Shear Reinformnet Stirrups = Asv_spacing = {0} mm.", DSR_Asv_spacing);
            sw.WriteLine("Legs of Shear Reinformnet Stirrups at End Span = Asv_Legs_End_span = {0} for Sec (1-1 & 2-2)", DSR_Asv_Legs_End_span);
            sw.WriteLine("Legs of Shear Reinformnet Stirrups at Mid Span = Asv_Legs_Mid_span = {0} for Sec (3-3 to 9-9)", DSR_Asv_Legs_Mid_span);
            sw.WriteLine("");
            sw.WriteLine("-------------------------------------------------------------");
            sw.WriteLine("DESIGN OF REINFORCEMENTS FOR BURSTING TENSILE FORCE (STEP 13)");
            sw.WriteLine("-------------------------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Diameter of Reinformnet Bars = Ast_dia = {0} mm.", DRBTF_Ast_dia);
            sw.WriteLine("Numbers of Reinformnet Bars = Ast_Nos = {0}", DRBTF_Ast_Nos);
            sw.WriteLine("Layers of Reinformnet Bars = Ast_Layers = {0}", DRBTF_Ast_Layers);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("------------------------------------");
            sw.WriteLine("DESIGN OF SHEAR CONNECTORS (STEP 14)");
            sw.WriteLine("------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Diameter of Shear Connector Bars = Ash_dia = {0} mm.", DSC_Ash_dia);
            sw.WriteLine("Legs of Shear Connectors = Ash_Leg = {0}", DSC_Ash_Leg);
            sw.WriteLine("Spacing of Shear Connectors = Ash_Spacing = {0} mm.", DSC_Ash_Spacing);
            sw.WriteLine("Steel Grade of Shear Connectors = Fe {0} (fy={0} N/Sq.mm)", DSC_fy);
            sw.WriteLine();
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("DESIGN OF TRANSVERSE SHEAR REINFORCEMENTS (STEP 15)");
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Reinforcement Steel Grade = fy = {0} N/Sq.mm,  ", DTSR_fy);
            sw.WriteLine("Permissible tensile stress in steel = σ_y = {0} N/Sq. mm = {1:f3} kN/Sq.mm", DTSR_sigma_y, (DTSR_sigma_y / 1000.0));
            sw.WriteLine("Length of Shear Plane = Ls = {0:f3} mm.", DTSR_Ls);
            sw.WriteLine("Concrete Grade = fck = {0} N/Sq.mm", DTSR_fck);
            sw.WriteLine("Diameter of Transverse Shear Reinforcement Bars = Ats_dia = {0} mm.", DTSR_Ats_dia);
            sw.WriteLine("Spacing of Transverse Shear Reinforcements = Ats_Spacing = {0:f3} mm.", DTSR_Ats_Spacing);
            sw.WriteLine("");
            sw.WriteLine("------------------------------------------");
            sw.WriteLine("DESIGN OF END DIAPHRAGM (STEP 16)");
            sw.WriteLine("--------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Width of Diaphragm = b = {0} mm, ", DED_b);
            sw.WriteLine("Total Depth of Diaphragm = D = 2350.0 mm", DED_D);
            sw.WriteLine("Reinforcement Clear Cover = cover = 50.0 mm, ", DED_cover);
            sw.WriteLine("");
            sw.WriteLine("dia_Ast1 = {0} mm,  Nos_Ast1 = {1}, ", DED_dia_Ast1, DED_Nos_Ast1);
            sw.WriteLine("dia_Ast2 = {0} mm,  Nos_Ast2 = {1},", DED_dia_Ast2, DED_Nos_Ast2);
            sw.WriteLine();
            sw.WriteLine("dia_dsh = {0} mm.,  Leg_dsh = {1},  Spacing_dsh = {2} mm.", DED_dia_dsh, DED_Leg_dsh, DED_Spacing_dsh);
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------");
            sw.WriteLine("DESIGN OF INTERMEDIATE DIAPHRAGM (STEP 17)");
            sw.WriteLine("-----------------------------------------");
            sw.WriteLine("");
            sw.WriteLine("Width of Diaphragm = b = {0:f3} mm, ", DID_b);
            sw.WriteLine("Total Depth of Diaphragm = D = {0:f3} mm", DID_b);
            sw.WriteLine("Reinforcement Clear Cover = cover = {0:f3} mm, ", DID_cover);
            sw.WriteLine("");
            sw.WriteLine("Diameter of Reinforcement Bars = dia_Ast1 = {0:f3} mm,  ", DID_dia_Ast1);
            sw.WriteLine("Number of Reinforcement Bars = Nos_Ast1 = {0:f0}", DID_Nos_Ast1);
            sw.WriteLine("Diameter of Reinforcement Bars = dia_Ast2 = {0:f3} mm,  ", DID_dia_Ast2);
            sw.WriteLine("Number of Reinforcement Bars = Nos_Ast2 = {0:f0}", DID_Nos_Ast2);
            sw.WriteLine("");
            sw.WriteLine("Diameter of Shear Reinforcement Bars = dia_dsh = {0:f3} mm.,  ", DID_dia_dsh);
            sw.WriteLine("Legs of Shear Reinforcement Bars = Leg_dsh = {0},  ", DID_Leg_dsh);
            sw.WriteLine("Spacing of Shear Reinforcement Bars = Spacing_dsh = {0} mm.", DID_Spacing_dsh);
            sw.WriteLine("");
            #endregion Chiranjit [2012 08 21]

        }

    }

    public class PSC_EndCrossGirder_Data
    {
        public double Asp_dia { get; set; }
        public int Asp_Nos { get; set; }
        public double Asn1_dia { get; set; }
        public int Asn1_Nos { get; set; }
        public double Asn2_dia { get; set; }
        public int Asn2_Nos { get; set; }

        public double Dc { get; set; }
        public double Bc { get; set; }
        public double L { get; set; }


        public PSC_EndCrossGirder_Data()
        {
            Asp_dia = 0;
            Asp_Nos = 0;
            Asn1_dia = 0;
            Asn1_Nos = 0;
            Asn2_dia = 0;
            Asn2_Nos = 0;

            Dc = 0;
            Bc = 0;
            L = 0.0;
        }

    }
}
//Chiranjit [2012 06 22]
//Chiranjit [2013 07 01]
