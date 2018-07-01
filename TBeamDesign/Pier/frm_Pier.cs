using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign.Piers;
using AstraInterface.TrussBridge;
 

namespace BridgeAnalysisDesign.Pier
{
    public partial class frm_Pier : Form
    {
        //const string Title = "ANALYSIS OF BRIDGE PIER";

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC PIER [BS]";
                return "DESIGN OF RCC PIER [IRC]";
            }
        }





        AstraInterface.Interface.IApplication iApp = null;

        Analysis_Pier Bridge_Analysis = null;
        RccPier rcc_pier = null;
        StoneMasonryPiers stone_masonry = null;

        bool IsCreateData = true;
        public frm_Pier(AstraInterface.Interface.IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            //IsCreate_Data = true;
            //user_path = System.IO.Path.Combine(user_path, Title);

            //if (!System.IO.Directory.Exists(user_path))
            //    System.IO.Directory.CreateDirectory(user_path);

            Bridge_Analysis = new Analysis_Pier(app);
            rcc_pier = new RccPier(iApp);
            stone_masonry = new StoneMasonryPiers(iApp);
        }
        
        #region Chiranjit [2012 07 18]
        
        #region Properties

        public List<string> Result { get; set; }
        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
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
        #endregion Properties

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

        #region Bridge Deck Analysis Form Events
        private void cmb_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                    txt_Ana_X.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].Distance.ToString("f4");
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
            }

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;

            Button_Enable_Disable();
        }
      
        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {

            user_path = IsCreateData ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;
            Write_All_Data();
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                Calculate_Load_Computation();
                Ana_Initialize_Analysis_InputData();
                Bridge_Analysis.Input_File = Path.Combine(user_path, "INPUT_DATA.TXT"); ;
                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData(Bridge_Analysis.Input_File);

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
                Write_All_Data(false);
                iApp.Save_Form_Record(this, user_path);

               MessageBox.Show(this, "Analysis Input data is created as INPUT_DATA.TXT.",
                  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex) { }
        }
        private void btn_Ana_close_Click(object sender, EventArgs e)
        {
            //Bridge_Analysis.Clear();
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
            iApp.View_Input_File(Bridge_Analysis.TotalAnalysis_Input_File);
        }
        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            if (File.Exists(Bridge_Analysis.TotalAnalysis_Input_File))
                iApp.OpenWork(Bridge_Analysis.TotalAnalysis_Input_File, false);
        }
        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
            int i = 0;
            Write_All_Data();

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
                //iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");
                //iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                //iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                //iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");
                 

                Bridge_Analysis.Girder_Analysis = null;
                Bridge_Analysis.Girder_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);

                if(iApp.Is_Progress_Cancel)
                {
                    iApp.Progress_Works.Clear();
                    iApp.Progress_OFF();
                    return;
                }

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

                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                iApp.Progress_Works.Clear();
            }

            //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


            Calculate_Interactive_Values();

            Button_Enable_Disable();
            Write_All_Data(false);

            iApp.Save_Form_Record(this, user_path);
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

            //dgv_live_load[4,0].Value
        }

        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text File (*.txt)|*.txt";
                    //ofd.InitialDirectory = iApp.LastDesignWorkingFolder;
                    ofd.InitialDirectory = Analysis_Path;

                    
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        IsCreateData = false;
                        string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");
                        if (!File.Exists(chk_file)) chk_file = ofd.FileName;
                        //Show_ReadMemberLoad(chk_file);
                        Ana_OpenAnalysisFile(chk_file);
                        txt_analysis_file.Text = chk_file;
                        rcc_pier.FilePath = user_path;
                        stone_masonry.FilePath = user_path;

                        iApp.Read_Form_Record(this, user_path);
                        Show_ReadMemberLoad(chk_file);
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
                //txt_Deck_L.Text = txt_Ana_L.Text;
                //txt_LL_load_gen.Text = (MyList.StringToDouble(txt_Ana_L.Text, 0.0) / MyList.StringToDouble(txt_XINCR.Text, 0.0)).ToString("f0");

                //txt_cnt_L.Text = txt_Ana_L.Text;
                Calculate_Interactive_Values();
                Text_Changed();
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
                Bridge_Analysis = new Analysis_Pier(iApp);

            Bridge_Analysis.Length = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            Bridge_Analysis.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Bridge_Analysis.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
            Bridge_Analysis.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
            Bridge_Analysis.Skew_Angle = MyList.StringToDouble(txt_Ana_ang.Text, 0.0);
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

            double L = Bridge_Analysis.Girder_Analysis.Analysis.Length;
            double W = Bridge_Analysis.Girder_Analysis.Analysis.Width;
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
            if (_X_joints.Contains(val))
            {
                Bridge_Analysis.Effective_Depth = val;
            }
            else
            {
                Bridge_Analysis.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }
            //double eff_dep = ;

            //_L_2_joints.Clear();

            double cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Bridge_Analysis.Width_LeftCantilever = cant_wi;
            Bridge_Analysis.Width_RightCantilever = _Z_joints[_Z_joints.Count - 1] - _Z_joints[_Z_joints.Count - 3];



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


                    if ((jn_col[i].X.ToString("0.0") == (Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
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



            _cross_joints.AddRange(_L2_inn_joints);
            _cross_joints.AddRange(_L4_inn_joints);
            _cross_joints.AddRange(_deff_inn_joints);

            if (Result == null) Result = new List<string>();

            Result.Clear();
            Result.Add("");
            Result.Add("");
            Result.Add("Analysis Result of RCC T-BEAM Bridge");
            Result.Add("");
            Result.Add("");
            Result.Add("INNER GIRDER");
            Result.Add("------------");
            MaxForce mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L2_inn_joints);
            txt_inner_long_L2_shear.Text = mfrc.ToString();

            Result.AddRange(mfrc.GetDetails("L/2 :  MAX SHEAR FORCE", _L2_inn_joints, " Ton"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L2_inn_joints);
            txt_Ana_inner_long_L2_moment.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/2 :  MAX BENDING MOMENT", _L2_inn_joints, " Ton-m"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L4_inn_joints);
            txt_Ana_inner_long_L4_shear.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX SHEAR SHEAR", _L4_inn_joints, " Ton"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L4_inn_joints);
            txt_Ana_inner_long_L4_moment.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX BENDING MOMENT", _L4_inn_joints, " Ton-m"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_deff_inn_joints);
            txt_Ana_inner_long_deff_shear.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX SHEAR SHEAR", _deff_inn_joints, " Ton"));

            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_deff_inn_joints);
            txt_inner_long_deff_moment.Text = mfrc.ToString();
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


                    if ((jn_col[i].X.ToString("0.0") == (Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
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


            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L2_inn_joints);
            txt_outer_long_L2_shear.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/2 :  MAX SHEAR FORCE", _L2_inn_joints, " Ton"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L2_inn_joints);
            txt_outer_long_L2_moment.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/2 :  MAX BENDING MOMENT", _L2_inn_joints, " Ton-m"));






            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_L4_out_joints);
            txt_outer_long_L4_shear.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX SHEAR FORCE", _L4_out_joints, " Ton"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_L4_out_joints);
            txt_outer_long_L4_moment.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("L/4 :  MAX BENDING MOMENT", _L4_out_joints, " Ton-m"));



            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_ShearForce(_deff_out_joints);
            txt_outer_long_deff_shear.Text = mfrc.ToString();
            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX SHEAR FORCE", _deff_out_joints, " Ton"));


            mfrc = Bridge_Analysis.Girder_Analysis.GetJoint_MomentForce(_deff_out_joints);
            txt_outer_long_deff_moment.Text = mfrc.ToString();

            Result.AddRange(mfrc.GetDetails("Effective Depth :  MAX BENDING MOMENT", _deff_out_joints, " Ton-m"));





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
            Ana_Show_Cross_Girder();

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

                Result.Add("");
                Result.Add("");
                Result.Add("");
                Result.AddRange(m.MaxBendingMoment.GetDetails("CROSS GIRDER :  MAX BENDING MOMENT", joints, " Ton-m"));
                txt_Ana_cross_max_moment.Text = m.MaxBendingMoment.ToString();



            }
            Ana_Write_Max_Moment_Shear();
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

            string f_path = Path.Combine(Bridge_Analysis.working_folder, "FORCES.TXT");
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
            btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
            //btn_dwg_rcc_pier.Enabled = File.Exists(rcc_pier.rep_file_name);
            btnReport.Enabled = File.Exists(stone_masonry.rep_file_name);
            btn_dwg_stone_interactive.Enabled = File.Exists(stone_masonry.rep_file_name);



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
            #region Chiranjit [2012 06 10]

        _run:

            Bridge_Analysis.Input_File = analysis_file;
            

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


            Calculate_Interactive_Values();

            Button_Enable_Disable();
            #endregion

            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Bridge_Analysis.LiveLoad_File);

            if (Bridge_Analysis.Live_Load_List == null) return;
            //cmb_load_type.Items.Clear();
        }

        public void Ana_OpenAnalysisFile_2013_05_01(string file_name)
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
                //string rep_file = Path.Combine(Bridge_Analysis.Working_Folder, "ANALYSIS_REP.TXT");
                string rep_file = Bridge_Analysis.Total_Analysis_Report;
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
                double BB = B;

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

                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;

                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
            }

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;


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
                //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
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

            dgv_live_load.Rows.Add(cmb_load_type.Text, iApp.LiveLoads[0].Distance, txt_Y.Text, "1.5", txt_XINCR.Text, txt_Load_Impact.Text);
            dgv_live_load.Rows.Add(cmb_load_type.Text, iApp.LiveLoads[0].Distance, txt_Y.Text, "4.5", txt_XINCR.Text, txt_Load_Impact.Text);
            dgv_live_load.Rows.Add(cmb_load_type.Text, iApp.LiveLoads[0].Distance, txt_Y.Text, "7.5", txt_XINCR.Text, txt_Load_Impact.Text);
            txt_LL_load_gen.Text = (13.0 / 0.2).ToString("0");
        }

        #endregion Bridge Deck Analysis Methods


        #region Chiranjit [2012 06 20]
        void Calculate_Interactive_Values()
        {
            return;
        }
        void Text_Changed()
        {

            double SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);


            //txt__SMG.Text = SMG.ToString("f3");
            //txt_Deck_SCG.Text = SCG.ToString("f3");

            CW = B - Bs - 2 * Wp;

            txt_LL_load_gen.Text = (MyList.StringToDouble(txt_Ana_L.Text, 0.0) / MyList.StringToDouble(txt_XINCR.Text, 0.0)).ToString("f0");

            txt_RCC_Pier_L1.Text = L.ToString();
            txt_RCC_Pier_w1.Text = CW.ToString();
            txt_RCC_Pier_w2.Text = B.ToString("f3");
            txt_RCC_Pier_w3.Text = Wp.ToString("f3");
            txt_RCC_Pier_B14.Text = B.ToString("f3");
            txt_RCC_Pier_a1.Text = Hp.ToString();
            txt_RCC_Pier_NB.Text = NMG.ToString();
            txt_RCC_Pier_d1.Text = DMG.ToString();
            txt_RCC_Pier_d2.Text = Ds.ToString();
            txt_RCC_Pier_gama_c.Text = Y_c.ToString();
            txt_RCC_Pier_NP.Text = NMG.ToString();
        }
        private void txt_Ana_width_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();

            try
            {
                for (int i = 0; i < dgv_live_load.RowCount; i++)
                {
                    dgv_live_load[4, i].Value = txt_XINCR.Text;
                }
            }
            catch (Exception ex) { }
        }

        private void txt_RCC_Pier_NB_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();
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

            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        private void cmb_deck_applied_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplied ld;

            ComboBox cmb = sender as ComboBox;

            
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
            Text_Changed_Forces();
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


            txt_ana_DLSR.Text = txt_dead_kN_m.Text;
            txt_ana_LLSR.Text = txt_live_kN_m.Text;
            txt_pier_2_P2.Text = txt_dead_kN_m.Text;
            txt_pier_2_P3.Text = txt_live_kN_m.Text;
            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
            txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

            txt_w1.Text = txt_dead_vert_reac_kN.Text;
            txt_w1.ForeColor = System.Drawing.Color.Red;
            txt_w2.ForeColor = System.Drawing.Color.Red;
            txt_w2.Text = txt_live_vert_rec_kN.Text;



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


            #region RCC PIER FORM1 USER INPUT DATA

            file_content.Add(string.Format("RCC PIER FORM1 USER INPUT DATA"));
            file_content.Add(string.Format("------------------------------"));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(kFormat, "C/C Distance between Piers [L]", "L", txt_RCC_Pier_L1.Text));
            file_content.Add(string.Format(kFormat, "Carriageway width", "w1", txt_RCC_Pier_w1.Text));
            file_content.Add(string.Format(kFormat, "Overall width of Deck", "w2", txt_RCC_Pier_w2.Text));
            file_content.Add(string.Format(kFormat, "Width of Crash Barrier", "w3", txt_RCC_Pier_w3.Text));
            file_content.Add(string.Format(kFormat, "Height of Crash Barrier", "a1", txt_RCC_Pier_a1.Text));
            file_content.Add(string.Format(kFormat, "Number of Bearings", "NB", txt_RCC_Pier_NB.Text));
            file_content.Add(string.Format(kFormat, "Depth of Girder", "d1", txt_RCC_Pier_d1.Text));
            file_content.Add(string.Format(kFormat, "Depth of Deck Slab", "d2", txt_RCC_Pier_d2.Text));
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
            file_content.Add(string.Format(kFormat, "Pier Cap width in Transverse Direction", "B14", txt_RCC_Pier_B14.Text));
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

            #region  STONE MASONRY
            //file_content.Add(string.Format(kFormat, "Safe Bearing Capacity of River Bed Soil ", "SBC", txt_pier_2_SBC.Text));

            file_content.Add("");
            file_content.Add(string.Format("STONE MASONRY"));
            file_content.Add(string.Format("-------------"));
            file_content.Add(string.Format(kFormat, "Width of Pier at Bottom ", "b1", txt_b1.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Width of Pier at Top ", "b2", txt_b2.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Length of Pier ", "l", txt_l.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Height of Pier ", "h", txt_h.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Area of Deck and Handrail in elevation ", "A", txt_A.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Wind Force ", "F", txt_F.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Mean water current Velocity ", "V", txt_V.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Acts at distance from Centre Live Pier ", "e", txt_e.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Permanent Load from each Span ", "w1", txt_w1.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Live Load from each Span ", "w2", txt_w2.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Vehicle Load on each Span ", "w3", txt_w3.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Unit weight of concrete  ", "γ_c", txt_gamma_c.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Height of high flood Level ", "HFL", txt_HFL.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Frictional Coefficient of Left Side Bending ", "f1", txt_f1.Text));
            file_content.Add("");
            file_content.Add(string.Format(kFormat, "Frictional Coefficient of Right Side Bending ", "f2", txt_f2.Text));
            file_content.Add("");
            #endregion  STONE MASONRY


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


            if (!File.Exists(data_file)) return;
            try
            {
                rcc_pier.FilePath = user_path;
                stone_masonry.FilePath = user_path;
            }
            catch (Exception ex) { }

            List<string> file_content = new List<string>(File.ReadAllLines(data_file));

            ePierOption TOpt = ePierOption.None;

            MyList mlist = null;
            MyList mlist_mov_ll = null;
            string kStr = "";
            for (int i = 0; i < file_content.Count; i++)
            {
                try
                {
                    kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                    mlist = new MyList(kStr, '=');
                    if (kStr.Contains("---")) continue;

                    #region Select Option
                    switch (kStr)
                    {
                        case "ANALYSIS OF BRIDGE DECK":
                            TOpt = ePierOption.Analysis;
                            break;
                        case "RCC PIER FORM1 USER INPUT DATA":
                            TOpt = ePierOption.RCCPier_1;
                            break;
                        case "RCC PIER FORM2 USER INPUT DATA":
                            TOpt = ePierOption.RCCPier_2;
                            break;
                        case "MOVING LOAD INPUT":
                            TOpt = ePierOption.MovingLoad;
                            dgv_live_load.Rows.Clear();
                            break;
                        case "STONE MASONRY":
                            TOpt = ePierOption.StoneMasonry;
                            //dgv_live_load.Rows.Clear();
                            break;
                    }
                    #endregion Select Option

                    if (TOpt == ePierOption.MovingLoad)
                    {
                        mlist_mov_ll = new MyList(kStr, ',');
                    }

                    if (mlist.Count == 3 || TOpt == ePierOption.MovingLoad)
                    {
                        if (TOpt != ePierOption.MovingLoad)
                            kStr = MyList.RemoveAllSpaces(mlist.StringList[1].Trim().TrimEnd().TrimStart());
                        try
                        {
                            switch (TOpt)
                            {
                                #region Chiranjit Select Data
                                case ePierOption.Analysis:
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
                                case ePierOption.MovingLoad:
                                    #region MOVING LOAD INPUT
                                    if (mlist_mov_ll.Count >= 5)
                                    {
                                        dgv_live_load.Rows.Add(mlist_mov_ll.StringList.ToArray());


                                    }
                                    if (mlist.Count == 3)
                                        if (mlist.StringList[1] == "LG")
                                            txt_LL_load_gen.Text = mlist.StringList[2];
                                    #endregion ANALYSIS OF BRIDGE DECK
                                    break;
                                case ePierOption.RCCPier_1:
                                    #region RCC PIER FORM1 USER INPUT DATA

                                    switch (kStr)
                                    {
                                        case "L":
                                            txt_RCC_Pier_L1.Text = mlist.StringList[2];
                                            break;
                                        case "w1":
                                            txt_RCC_Pier_w1.Text = mlist.StringList[2];
                                            break;
                                        case "w2":
                                            txt_RCC_Pier_w2.Text = mlist.StringList[2];
                                            break;
                                        case "w3":
                                            txt_RCC_Pier_w3.Text = mlist.StringList[2];
                                            break;
                                        case "a1":
                                            txt_RCC_Pier_a1.Text = mlist.StringList[2];
                                            break;
                                        case "NB":
                                            txt_RCC_Pier_NB.Text = mlist.StringList[2];
                                            break;
                                        case "d1":
                                            txt_RCC_Pier_d1.Text = mlist.StringList[2];
                                            break;
                                        case "d2":
                                            txt_RCC_Pier_d2.Text = mlist.StringList[2];
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
                                            txt_RCC_Pier_B14.Text = mlist.StringList[2];
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
                                        //case "W1":
                                        //    txt_RCC_Pier_W1_supp_reac.Text = mlist.StringList[2];
                                        //    break;
                                        //case "Mx1":
                                        //    txt_RCC_Pier_Mx1.Text = mlist.StringList[2];
                                        //    break;
                                        //case "Mz1":
                                        //    txt_RCC_Pier_Mz1.Text = mlist.StringList[2];
                                        //    break;
                                        case "TVLL":
                                            txt_RCC_Pier_vehi_load.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion RCC PIER FORM1 USER INPUT DATA
                                    break;
                                case ePierOption.RCCPier_2:
                                    #region RCC PIER FORM2 USER INPUT DATA
                                    switch (kStr)
                                    {
                                        //case "P2":
                                        //    txt_pier_2_P2.Text = mlist.StringList[2];
                                        //    break;

                                        //case "P3":
                                        //    txt_pier_2_P3.Text = mlist.StringList[2];
                                        //    break;

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
                                case ePierOption.StoneMasonry:
                                    #region StoneMasonry
                                    switch (kStr)
                                    {
                                        case "b1":
                                            txt_b1.Text = mlist.StringList[2];
                                            break;
                                        case "b2":
                                            txt_b2.Text = mlist.StringList[2];
                                            break;
                                        case "l":
                                            txt_l.Text = mlist.StringList[2];
                                            break;
                                        case "h":
                                            txt_h.Text = mlist.StringList[2];
                                            break;
                                        case "A":
                                            txt_A.Text = mlist.StringList[2];
                                            break;
                                        case "F":
                                            txt_F.Text = mlist.StringList[2];
                                            break;
                                        case "V":
                                            txt_V.Text = mlist.StringList[2];
                                            break;
                                        case "e":
                                            txt_e.Text = mlist.StringList[2];
                                            break;
                                        case "w1":
                                            txt_w1.Text = mlist.StringList[2];
                                            break;
                                        case "w2":
                                            txt_w2.Text = mlist.StringList[2];
                                            break;
                                        case "w3":
                                            txt_w3.Text = mlist.StringList[2];
                                            break;
                                        case "γ_c":
                                            txt_gamma_c.Text = mlist.StringList[2];
                                            break;
                                        case "HFL":
                                            txt_HFL.Text = mlist.StringList[2];
                                            break;
                                        case "f1":
                                            txt_f1.Text = mlist.StringList[2];
                                            break;
                                        case "f2":
                                            txt_f2.Text = mlist.StringList[2];
                                            break;
                                    }
                                    #endregion

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

            Button_Enable_Disable();
        }
        #endregion Chiranjit [2012 07 10]

        #endregion Chiranjit [2012 07 18]

        #region Analysis of Pier
        #endregion

        #region Form Events
        private void Analysis_Pier_TBeamAnalysis_Load()
        {
            Bridge_Analysis.Joints = new JointNodeCollection();
            Bridge_Analysis.MemColls = new MemberCollection();

            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 2.75, 0.2, 1.179);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 6.25, 0.2, 1.179);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 9.75, 0.2, 1.179);
            Button_Enable_Disable();
        }
        #endregion Form Events

        #region Design of RCC Pier
        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(rcc_pier.rep_file_name);
            
        }
         
        private void btn_RccPier_Process_Click(object sender, EventArgs e)
        {
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
            rcc_pier.L1 = MyList.StringToDouble(txt_RCC_Pier_L1.Text, 0.0);
            rcc_pier.w1 = MyList.StringToDouble(txt_RCC_Pier_w1.Text, 0.0);
            rcc_pier.w2 = MyList.StringToDouble(txt_RCC_Pier_w2.Text, 0.0);
            rcc_pier.w3 = MyList.StringToDouble(txt_RCC_Pier_w3.Text, 0.0);


            rcc_pier.a1 = MyList.StringToDouble(txt_RCC_Pier_a1.Text, 0.0);
            rcc_pier.NB = MyList.StringToDouble(txt_RCC_Pier_NB.Text, 0.0);
            rcc_pier.d1 = MyList.StringToDouble(txt_RCC_Pier_d1.Text, 0.0);
            rcc_pier.d2 = MyList.StringToDouble(txt_RCC_Pier_d2.Text, 0.0);
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
            rcc_pier.B14 = MyList.StringToDouble(txt_RCC_Pier_B14.Text, 0.0);
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
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
         


            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);
            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);




            rcc_pier.rdia = MyList.StringToDouble(txt_RCC_Pier_rdia.Text, 0.0);
            rcc_pier.tdia = MyList.StringToDouble(txt_RCC_Pier_tdia.Text, 0.0);



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



            rcc_pier.hdia = MyList.StringToDouble(txt_pier_2_hdia.Text, 0.0);
            rcc_pier.vdia = MyList.StringToDouble(txt_pier_2_vdia.Text, 0.0);
            rcc_pier.vspc = MyList.StringToDouble(txt_pier_2_vspc.Text, 0.0);



            #endregion Data Input Form 2 Variables

        }
        #endregion Design of RCC Pier

        #region Design Of Stome Masonry Pier

        private void btn_Stone_Masonry_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(stone_masonry.rep_file_name);
            
        }
        private void btn_Stone_Masonry_Process_Click(object sender, EventArgs e)
        {
            stone_masonry.FilePath = user_path;
            Stone_Masonry_Initialize_InputData();
            stone_masonry.Calculate_Program(stone_masonry.rep_file_name);
            stone_masonry.Write_User_Input();
            stone_masonry.Write_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(stone_masonry.rep_file_name)) { MessageBox.Show(this, "Report file written in " + stone_masonry.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(stone_masonry.rep_file_name); }
            stone_masonry.is_process = true;
            Button_Enable_Disable();
        }
        public void Stone_Masonry_Initialize_InputData()
        {
            #region Variable Initialization
            stone_masonry.w1 = MyList.StringToDouble(txt_w1.Text, 0.0);
            stone_masonry.w2 = MyList.StringToDouble(txt_w2.Text, 0.0);
            stone_masonry.e = MyList.StringToDouble(txt_e.Text, 0.0);
            stone_masonry.b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            stone_masonry.b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            stone_masonry.l = MyList.StringToDouble(txt_l.Text, 0.0);
            stone_masonry.h = MyList.StringToDouble(txt_h.Text, 0.0);
            stone_masonry.HFL = MyList.StringToDouble(txt_HFL.Text, 0.0);
            stone_masonry.w3 = MyList.StringToDouble(txt_w3.Text, 0.0);
            stone_masonry.gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
            stone_masonry.f1 = MyList.StringToDouble(txt_f1.Text, 0.0);
            stone_masonry.f2 = MyList.StringToDouble(txt_f2.Text, 0.0);
            stone_masonry.A = MyList.StringToDouble(txt_A.Text, 0.0);
            stone_masonry.F = MyList.StringToDouble(txt_F.Text, 0.0);
            stone_masonry.V = MyList.StringToDouble(txt_V.Text, 0.0);
            #endregion

        }


        public void Stone_Masonry_Read_From_File()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(stone_masonry.user_input_file));
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
                        case "w1":
                            stone_masonry.w1 = mList.GetDouble(1);
                            txt_w1.Text = stone_masonry.w1.ToString();
                            break;

                        case "w2":
                            stone_masonry.w2 = mList.GetDouble(1);
                            txt_w2.Text = stone_masonry.w2.ToString();
                            break;

                        case "e":
                            stone_masonry.e = mList.GetDouble(1);
                            txt_e.Text = stone_masonry.e.ToString();
                            break;

                        case "b1":
                            stone_masonry.b1 = mList.GetDouble(1);
                            txt_b1.Text = stone_masonry.b1.ToString();
                            break;

                        case "b2":
                            stone_masonry.b2 = mList.GetDouble(1);
                            txt_b2.Text = stone_masonry.b2.ToString();
                            break;

                        case "l":
                            stone_masonry.l = mList.GetDouble(1);
                            txt_l.Text = stone_masonry.l.ToString();
                            break;

                        case "h":
                            stone_masonry.h = mList.GetDouble(1);
                            txt_h.Text = stone_masonry.h.ToString();
                            break;

                        case "HFL":
                            stone_masonry.HFL = mList.GetDouble(1);
                            txt_HFL.Text = stone_masonry.HFL.ToString();
                            break;

                        case "w3":
                            stone_masonry.w3 = mList.GetDouble(1);
                            txt_w3.Text = stone_masonry.w3.ToString();
                            break;

                        case "gamma_c":
                            stone_masonry.gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = stone_masonry.gamma_c.ToString();
                            break;

                        case "f1":
                            stone_masonry.f1 = mList.GetDouble(1);
                            txt_f1.Text = stone_masonry.f1.ToString();
                            break;

                        case "f2":
                            stone_masonry.f2 = mList.GetDouble(1);
                            txt_f2.Text = stone_masonry.f2.ToString();
                            break;

                        case "A":
                            stone_masonry.A = mList.GetDouble(1);
                            txt_A.Text = stone_masonry.A.ToString();
                            break;

                        case "F":
                            stone_masonry.F = mList.GetDouble(1);
                            txt_F.Text = stone_masonry.F.ToString();
                            break;
                        case "V":
                            stone_masonry.V = mList.GetDouble(1);
                            txt_V.Text = stone_masonry.V.ToString();
                            throw new Exception("DATA_INITIALIZED");
                            break;
                    }
                    #endregion
                    #region USER INPUT DATA

                    //sw.WriteLine(" = {0:f3} ", w1);
                    //sw.WriteLine(" = {0:f3} ", w2);
                    //sw.WriteLine(" = {0:f3} ", e);
                    //sw.WriteLine(" = {0:f3} ", b1);
                    //sw.WriteLine(" = {0:f3} ", b2);
                    //sw.WriteLine(" = {0:f3} ", l);
                    //sw.WriteLine(" = {0:f3} ", h);
                    //sw.WriteLine(" = {0:f3} ", );
                    //sw.WriteLine(" = {0:f3} ", w3);
                    //sw.WriteLine(" = {0:f3} ", gamma_c);
                    //sw.WriteLine(" = {0:f3} ", f1);
                    //sw.WriteLine(" = {0:f3} ", f2);
                    //sw.WriteLine(" = {0:f3} ", A);
                    //sw.WriteLine(" = {0:f3} ", F);
                    //sw.WriteLine(" = {0:f3} ", V);

                    #endregion
                }


            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        #endregion Design Of Stome Masonry Pier

        private void frm_Pier_Load(object sender, EventArgs e)
        {
            //pic_pier_interactive_diagram.BackgroundImage = AstraFunctionOne.ImageCollection.Pier_drawing;
            pic_stone_masonry.BackgroundImage = AstraFunctionOne.ImageCollection.PIER;
            #region Analysis Data
            Bridge_Analysis = new Analysis_Pier(iApp);
            iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
            //Ana_Fill_Default_Moving_LoadData(dgv_live_load);
            #endregion Analysis Data
            Analysis_Pier_TBeamAnalysis_Load();
          
            cmb_pier_2_k.SelectedIndex = 1;
            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;
            Button_Enable_Disable();


            //Chiranjit [2014 10 06]
            #region Chiranjit Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {
                    IsCreateData = false;
                    string chk_file = "";

                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                    chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                    //Show_ReadMemberLoad(chk_file);
                    Ana_OpenAnalysisFile(chk_file);
                    txt_analysis_file.Text = chk_file;
                    rcc_pier.FilePath = user_path;
                    stone_masonry.FilePath = user_path;

                    iApp.Read_Form_Record(this, user_path);
                    Show_ReadMemberLoad(chk_file);
                }

                Button_Enable_Disable();

                grb_create_input_data.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option
        }

        private void btn_worksheet_1_Click(object sender, EventArgs e)
        {
            string ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");

            Button b = sender as Button;


            if (b.Name == btn_worksheet_1.Name)
                ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");
            
            else if (b.Name == btn_worksheet_Pier_cap.Name)
                ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\01 Pier Cap.XLS");
           
            else if (b.Name == btn_worksheet_pier_design_with_piles.Name)
                ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\03 Pier Design with 6piles.XLS");

            else if (b.Name == btn_worksheet_pile_capacity.Name)
                ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\02 Pile Capacity.xls");

            else if (b.Name == btn_ws_new_cir.Name)
                ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Circular Design\Design of Circular Pier.xls");

            else if (b.Name == btn_ws_new_cir_well.Name)
                ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier with Well Foundation\Design of Pier with Well Foundation.xls");


            iApp.OpenExcelFile(Worksheet_Folder, ex_file, "2011ap");
        }

        private void btn_dwg_stone_interactive_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(stone_masonry.drawing_path, "Pier", "");
        }
        private void btn_dwg_pier_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "RCC_Pier_Default_Drawings");
        }
        private void btn_dwg_pier_1_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC_Pier_Worksheet_Design_2"), "RCC_Pier_Worksheet_Design_2");
        }
        private void btn_dwg_pier_2_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC_Pier_Worksheet_Design_1"), "RCC_Pier_Worksheet_Design_1");
        }
        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }
        private void cmb_Ana_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                {
                    //txt_Ana_X.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].Distance.ToString("f4");  // Chiranjit [2013 05 28] Kolkata
                    txt_Load_Impact.Text = iApp.LiveLoads[cmb_load_type.SelectedIndex].ImpactFactor.ToString("f3");
                }
            }
            catch (Exception ex) { }
        }
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
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_rcc_pier") ||
                ctrl.Name.ToLower().StartsWith("txt_rcc_pier"))
            {
                astg = new ASTRAGrade(cmb_rcc_pier_fck.Text, cmb_rcc_pier_fy.Text);
                txt_rcc_pier_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_rcc_pier_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }


        }
        private void cmb_rcc_pier_fck_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L.Text = "0.0";
                txt_Ana_L.Text = "26.0";
                txt_Ana_B.Text = "12.5";
                txt_Ana_CW.Text = "10.0";

                //string str = "ASTRA USB Lock not found at any port.\n\nThis is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "   Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "  Email at : techsoft@consultant.com, dataflow@mail.com\n";
                //str += "Contact No : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]

        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {
            iApp.Show_LL_Dialog();
            iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
        }


    }
    public  class Analysis_Pier
    {

        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Girder_Analysis = null;
        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;

        public int _Columns = 0, _Rows = 0;

        public double span_length = 0.0;


        public string input_file, working_folder, user_path;
        public string support_end1 = "";
        public string support_end2 = "";
        public Analysis_Pier(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = working_folder = "";
            //Total_Rows = 0; Total_Columns = 0;
            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
            Length = 0.0;
            WidthBridge = 0.0;
            //WidthCantilever = 0.0;
            Effective_Depth = 0.0;
            Skew_Angle = 0.0;

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
                return MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / 6.0).ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //return MyList.StringToDouble(txt_cross_girder_spacing.Text, 0.0);
                return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);
            }
        }
        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(Working_Folder, "LL.TXT");
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

        #endregion Properties

        //Chiranjit [2011 07 09]
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

        //Chiranjit [2011 07 11]
        //Write Default data as given Astra Examples 8
        public void WriteData(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS FOR PIER");
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
            list.Add(support_end1.Replace(",", " ") + " PINNED");
            list.Add(support_end2.Replace(",", " ") + " PINNED");
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
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);

        }

        public void WriteData_Total_Analysis(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS FOR PIER");
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
            list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
            list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
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
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS FOR PIER");
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
            list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
            list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
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

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS FOR PIER");
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
            list.Add("1 2 3 4 5 6 7 8 9 10 11  PINNED");
            list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
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



        public List<string> Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Girder_Analysis.Analysis.Members);

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


            z_min = Girder_Analysis.Analysis.Joints.MinZ;
            double z_max = Girder_Analysis.Analysis.Joints.MaxZ;


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
    public  class RccPier
    {
        IApplication iapp = null;

        public string rep_file_name = "";
        public string file_path = "";
        public string user_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public bool is_process = false;

        public double L1, W1, W2, W3, W4, W5, total_vehicle_load;
        public double w1, w2, w3;
        public double D1, D2, D3;
        public double RL6, RL5, RL4, RL3, RL2, RL1;
        public double H1, H2, H3, H4, H5, H6, H7, H8;
        public double B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12, B13, B14, B15, B16;
        public double NR, NP, gama_c, MX1, MY1;

        public double a1, NB, fy1, fy2, p1, p2, d_dash, Pu_top, Pu_bottom, Mux, Muy;

        public double d1, d2, fck1, fck2, perm_flex_stress;
        public double over_all, D, b, form_lev;


        //Chiranjit [2017 03 06] new Inputs for Bar Diameter

        public double rdia, tdia, hdia, vdia, vspc, hlegs, vlegs;

        //Chiranjit [2012 05 31]
        //Data Input Form2 Variables
        public double P2, P3, total_pairs, PL, PML, nped, SC, HHF, V, K, CF, LL, Vr, Itc, sdia, sleg, ldia, SBC;
        public string APD, PD;

        public double sigma_s, m;
        public RccPier(IApplication app)
        {
            this.iapp = app;


            rdia = 32;
            tdia = 12;
            hdia = 12;
            vdia = 10;

            hlegs = 12;
            vlegs = 4;
        }
        public void Calculate_Program()
        {
            //double L1, W1, W2, W3, W4, W5, total_vehicle_load;
            //double w1, w2, w3;
            //double D1, D2, D3;
            //double RL6, RL5, RL4, RL3, RL2, RL1;
            //double H1, H2, H3, H4, H5, H6, H7, H8;
            //double B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12, B13, B14, B15, B16;
            //double NR, NP, gama_c, MX1, MY1;

            List<string> list = new List<string>();

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*                  ASTRA Pro                  *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*           DESIGN  OF  RCC PIERS             *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion

            #region User Input

            list.Add(string.Format("USER’S DATA [FORM1]"));
            list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Pier  Diagram"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Support Reaction on the Pier for (DL+SIDL+LL) = W1 = {0} kN", W1));
            list.Add(string.Format("Moment at Supports in Longitudinal Direction [Mx1] = {0} kN-m", MX1));
            list.Add(string.Format("Moment at Supports in Transverse Direction [My1] = {0} kN-m", MY1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("C/C Distance between Piers = L1 = {0} m.", L1));
            list.Add(string.Format("Carriageway width = w1 = {0} m.", w1));
            list.Add(string.Format("Overall width of Deck = w2 = {0} m.", w2));
            list.Add(string.Format("Width of Crash Barrier = w3 = {0} m.", w3));
            list.Add(string.Format("Height of Crash Barrier = a1 = {0} m.", a1));
            list.Add(string.Format("Number of Bearings = NB = {0}", NB));
            list.Add(string.Format("Depth of Girder = d1 = {0} m.", d1));
            list.Add(string.Format("Depth of Deck Slab = d2 = {0} m.", d2));
            list.Add(string.Format("Size of Pedestals = B1 x B2 x H1 = {0} m. x {1} m. x {2} m.", B1, B2, H1));
            list.Add(string.Format("Size of Bearings = B3 x B4 x H2 = {0} m. x {1} m. x {2} m.", B3, B4, H2));
            //list.Add(string.Format("Distance Between Girders = B5 = {0} m.", B5));
            list.Add(string.Format("Length of Footing = B6 = {0} m.", B6));
            list.Add(string.Format(""));
            list.Add(string.Format("R.L. at Pier Cap Top = RL1 = {0} m.", RL1));
            list.Add(string.Format("High Flood Level (HFL) = RL2 = {0} m.", RL2));
            list.Add(string.Format("Existing Ground Level = RL3 = {0} m.", RL3));
            list.Add(string.Format("R.L. at Footing Top = RL4 = {0} m.", RL4));
            list.Add(string.Format("R.L. at Footing Bottom = RL5 = {0} m.", RL5));
            list.Add(string.Format("Formation Level=RL1+d1+d2+H1+H2={0} m.", form_lev));
            list.Add(string.Format(""));

            list.Add(string.Format("Width of Footing = B7 = {0} m.", B7));
            list.Add(string.Format("Straight Depth of Footing = H3 = {0} m.", H3));
            list.Add(string.Format("Varying Depth of Footing = H4 = {0} m.", H4));
            list.Add(string.Format("P.C.C. Projection under Footing on either side = B8 = {0} m.", B8));
            list.Add(string.Format("Grade of Concrete = M{0} ", fck1));
            list.Add(string.Format("Permissible Flexural Stress = {0}  N/Sq.mm.", perm_flex_stress));
            list.Add(string.Format("Grade of Steel = Fe{0} ", fy1));
            list.Add(string.Format("Straight depth of Pier Cap = H5 = {0} m.", H5));
            list.Add(string.Format("Varying Depth of Pier Cap = H6 = {0} m.", H6));
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal width of Pier at Base = B9 = {0} m.", B9));
            list.Add(string.Format("Transverse width of Pier at Base = B10 = {0} m.", B10));
            list.Add(string.Format("Longitudinal width of Pier at Top = B11 = {0} m.", B11));
            list.Add(string.Format("Transverse width of Pier at Top = B12 = {0} m.", B12));

            list.Add(string.Format("Pier Cap width in longitudinal direction = B13 = {0} m.", B13));
            list.Add(string.Format("Pier Cap width in transverse direction = B14 = {0} m.", B14));

            list.Add(string.Format("Overall Height of Substructure = H7 + H5 + H6 = {0} + {1} + {2} = {3} m. ", H7, H5, H6, over_all));
            //list.Add(string.Format("                               = {0} + {1} + {2} ",H7,H5,H6));
            //list.Add(string.Format("                              "));



            list.Add(string.Format(""));
            list.Add(string.Format("Standard Minimum Reinforcement = p1 = {0} %", p1));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Trial Reinforcement = p2 =  {0} %", p2));
            list.Add(string.Format(""));
            list.Add(string.Format("Reinforcement Cover = d’ =  {0} mm", d_dash));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Pier in Transverse direction = D =  {0} mm", D));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Pier in Longitudinal direction = b =  {0} mm", b));
            list.Add(string.Format(""));
            //list.Add(string.Format("Reinforcement Bar Diameter = rdia = {0} mm", rdia));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Lateral Tie Bar Diameter = tdia = {0} mm", tdia));
            list.Add(string.Format(""));





            list.Add(string.Format(""));
            //list.Add(string.Format("For Pier Stem Structural Design:"));
            list.Add(string.Format("Reinforcements For Structural Design of Pier Stem:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter Vertical Steel reinforcement = {0} mm.", rdia));
            list.Add(string.Format("Diameter Lateral  Ties steel reinforcement = {0} mm.", tdia));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Reinforcements For Structural Design of Pier Stem:"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Diameter Vertical Steel reinforcement = 25 mm."));
            //list.Add(string.Format("Diameter Lateral  Ties steel reinforcement = 12 mm."));
            //list.Add(string.Format(""));

            //list.Add(string.Format("Distance B15 = {0} m.", B15));
            //list.Add(string.Format("Distance B16 = {0} m.", B16));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("USER’S DATA [FORM2] :"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Various Load Reactions for Supports within this distance are read from Analysis Report File"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load Support Reaction for all Supports = P2 = {0} kN", P2));
            list.Add(string.Format("Live Load Support Reaction for all Supports = P3 = {0} kN", P3));
            list.Add(string.Format(""));
            list.Add(string.Format("Distance from left edge Pier Cap Edge to left face of Pier = B16 = {0} m.", B16));
            list.Add(string.Format("Distances from left edge of Pier Cap to centre of each pair of Pedestals "));
            MyList m_tot_prs = new MyList(MyList.RemoveAllSpaces(APD.Replace(',', ' ').TrimStart().TrimEnd().Trim()), ' ');
            total_pairs = m_tot_prs.Count;

            list.Add(string.Format("= APD[total Pairs] = {0} (total Pairs={1})", APD, total_pairs));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            PL = W1 / total_pairs;
            list.Add(string.Format(" (Get Load Reactions on each pair of Pedestals = Total Load Reaction / total Pairs)"));
            list.Add(string.Format(" = PL = {0:f2}/{1} = {2:f2} kN.", W1, total_pairs, PL));
            list.Add(string.Format(""));
            list.Add(string.Format(" (Get Moments on each Pedestal = Total moments / total Pairs)"));

            PML = (MX1 + MY1) / total_pairs;
            list.Add(string.Format(" = PML = (Mx1 + My1)/total pairs = ({0:f3} + {1:f3})/{2} = {3:f3} kN.", MX1, MY1, total_pairs, PML));
            list.Add(string.Format(""));
            list.Add(string.Format(" (Get distances of each pairs of pedestals within the distance of B16 "));

            MyList m_pd = new MyList(PD, ',');
            List<double> PD_nped = new List<double>();
            string str_PD = "";
            string str_PD_nped = "";
            for (int i = 0; i < m_pd.Count; i++)
            {
                str_PD += string.Format("{0}-{1}, ", B16, m_pd.GetDouble(i));
                str_PD_nped += string.Format("{0}, ", (B16- m_pd.GetDouble(i)));
                PD_nped.Add(B16 - m_pd.GetDouble(i));
            }

            //list.Add(string.Format(" = PD[nped]=2.5-APD[nped]) = 2.5-0.5, 2.5-2.0 = 2.0, 0.5"));
            list.Add(string.Format(" = PD[nped]={0}-APD[nped]) = {1} = {2}", B16, str_PD, str_PD_nped));
            list.Add(string.Format(""));
            nped = m_pd.Count;
            list.Add(string.Format("Therefore, Numbers of Pedestal in Overhang part = Nped = {0}", nped));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Coefficient = SC = {0}", SC));
            //list.Add(string.Format("(Put value 0 if not required)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Height of Water from River Bed at High Flood = HHF = {0} m", HHF));
            //list.Add(string.Format("(Put value 0 if not required)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Observed Velocity of water at High Flood = V = {0} m/sec.", V));
            list.Add(string.Format(""));
            list.Add(string.Format("Pier Shape Constant = K = {0}", K));
            //list.Add(string.Format("Pier End Shape                                       Shape Constant "));
            //list.Add(string.Format("Square End Piers                                1.50"));
            //list.Add(string.Format("Semi Circular End Piers       0.66"));
            //list.Add(string.Format("Angular End Piers             0.50"));
            //list.Add(string.Format("User Defined Value                                _"));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficient of Friction between Concrete and River Bed = CF = {0}", CF));
            list.Add(string.Format(""));
            list.Add(string.Format("Breaking Force 20% of Live Load = LL = {0} kN", LL));
            list.Add(string.Format(""));
            list.Add(string.Format("Temperature Force on Each Bearing = Vr = {0} kN/mm", Vr));
            list.Add(string.Format(""));
            list.Add(string.Format("Shrinkage Force on Each Bearing = Itc = {0} mm ", Itc));
            list.Add(string.Format(""));
            //list.Add(string.Format("Diameter of Shear reinforcement Bars = sdia = {0} mm. and Legs = sleg = {1}", sdia, sleg));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Diameter of Longitudinal reinforcement Bars = ldia = {0} mm.", ldia));
            list.Add(string.Format(""));
            //list.Add(string.Format("Diameter of Horizontal Stirrup Bars = hdia = {0} mm", hdia));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Diameter of Vertical Stirrup Bars = vdia = {0} mm ", vdia));


            list.Add(string.Format(""));
            //list.Add(string.Format("For Pier Cap Structural Design:"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Diameter of Reinforcement Bars = ldia = {0} mm.", ldia));
            list.Add(string.Format(""));
            //list.Add(string.Format("Longitudinal Reinforcements:"));



            list.Add(string.Format(""));
            //list.Add(string.Format("For Pier Stem Structural Design:"));
            list.Add(string.Format("Reinforcements For Structural Design of Pier Stem:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter Vertical Steel reinforcement = {0} mm.", rdia));
            list.Add(string.Format("Diameter Lateral  Ties steel reinforcement = {0} mm.", tdia));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Reinforcements For Structural Design of Pier Cap:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter of Longitudinal Reinforcement Bars = ldia = {0} mm.", ldia));
            list.Add(string.Format("Shear Reinforcements = {0} mm. and Legs = sleg = {1}", sdia, sleg));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Stirrups: {0} mm dia., {1} Legged Stirrups", hdia, hlegs));
            list.Add(string.Format("Vertical Stirrups: {0} mm dia., {1} Legged Stirrups", vdia, vlegs));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Reinforcements For Structural Design of Pier Cap:"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Diameter of Longitudinal Reinforcement Bars = ldia = 25 mm."));
            //list.Add(string.Format("Shear Reinforcements = 16 mm. and Legs = sleg = 6"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Horizontal Stirrups: 12 mm dia., 12 Legged Stirrups"));
            //list.Add(string.Format("Vertical Stirrups: 10 mm dia., 4 Legged Stirrups"));
            list.Add(string.Format(""));




            list.Add(string.Format(""));
            list.Add(string.Format("Spacing between Vertical Stirrup Bars = vspc  = {0} mm ", vspc));




            list.Add(string.Format(""));
            list.Add(string.Format("Bearing Capacity of Soil = SBC = {0} kN/Sq.m", SBC));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion

            #region Step 1 : DESIGN CALCULTIONS
            list.Add(string.Format("STEP 1 : DESIGN CALCULTIONS"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double volume = B1 * B2 * H1;
            list.Add(string.Format("Weight of Pedestals :"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Volume = B1 * B2 * H1"));
            list.Add(string.Format("       = {0} * {1} * {2}", B1, B2, H1));
            list.Add(string.Format("       = {0} cu.m.", volume));
            list.Add(string.Format(""));

            double total_volume = NR * NP * volume;

            list.Add(string.Format("Total Volume = NR * NP * {0}", volume));
            list.Add(string.Format("             = {0} cu.m.", total_volume));
            list.Add(string.Format(""));
            double total_weight = total_volume * gama_c;
            W2 = total_weight;

            list.Add(string.Format("Total Weight = W2 =  {0} * γ_c", total_volume));
            list.Add(string.Format("                  = {0} * {1}", total_volume, gama_c));
            list.Add(string.Format("                  = {0} kN", W2));
            list.Add(string.Format(""));

            //=((1.75*10*0.6)+((1.75*10)+(1*5))*0.6/2)
           double  bed_block_volume = ((B13 * B14 * H5) + ((B13 * B14) + (B11 * B12)) * H6 / 2);

            list.Add(string.Format("Weight of Pier Cap Bed Block :"));
            list.Add(string.Format(""));
            list.Add(string.Format("   Volume = ((B13 * B14 * H5) + ((B13 * B14) + (B11 * B12)) * H6 / 2)"));
            list.Add(string.Format("          = (({0} * {1} * {2}) + (({0} * {1}) + ({3} * {4})) * {5} / 2)",
                B13, B14, H5, B11, B12, H6));
            list.Add(string.Format("          = {0:f3} cu.m.", bed_block_volume));
            list.Add(string.Format(""));
            total_weight = bed_block_volume * gama_c;
            W3 = total_weight;
            list.Add(string.Format("Total Weight = W3  =  {0} * γ_c", bed_block_volume));
            list.Add(string.Format("                   = {0} * {1}", bed_block_volume, gama_c));
            list.Add(string.Format("                   = {0} kN", W3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Weight of Pier Stem:"));
            list.Add(string.Format("--------------------"));
            list.Add(string.Format(""));
            double pier_volume = ((B9 * B10 + B11 * B12) / 2.0) * H7;
            list.Add(string.Format("Volume = ((B9 * B10 + B11 * B12)/2) * H7"));
            list.Add(string.Format("       = (({0} * {1} + {2} * {3})/2) * {4}", B9, B10, B11, B12, H7));
            list.Add(string.Format("       = {0} cu.m.", pier_volume));
            list.Add(string.Format(""));
            total_weight = pier_volume * gama_c;
            W4 = total_weight;
            list.Add(string.Format("Total Weight = W4  =  {0} * γ_c", pier_volume));
            list.Add(string.Format("                   = {0} * {1}", pier_volume, gama_c));
            list.Add(string.Format("                   = {0} kN", W4));
            list.Add(string.Format(""));


            list.Add(string.Format("Weight of Footing :"));
            list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));
            W5 = total_weight;
            double footing_volume = ((B6 * B7 * H4) + ((B6 * B7 + B11 * B12) / 2.0) * H3);
            list.Add(string.Format("Volume = (B6 * B7 * H4) + ((B6 * B7 + B11 * B12) / 2.0) * H3"));
            list.Add(string.Format("       = ({0} * {1} * {2}) + (({0} * {1} + {3} * {4}) / 2.0) * {5}", B6, B7, H4, B11, B12, H3));
            list.Add(string.Format("       = {0} cu.m.", footing_volume));
            list.Add(string.Format(""));
            total_weight = footing_volume * gama_c;
            W5 = total_weight;
            list.Add(string.Format("Total Weight = W5  =  {0} * γ_c", footing_volume));
            list.Add(string.Format("                   = {0} * {1}", footing_volume, gama_c));
            list.Add(string.Format("                   = {0} kN", W5));
            list.Add(string.Format(""));
            double top_vert_load = W1 + W2 + W3 + W4;
            Pu_top = top_vert_load;
            list.Add(string.Format("Total Vertical Load at Top of Footing = W1 + W2 + W3 + W4"));
            list.Add(string.Format("                                      = {0} + {1} + {2} + {3}", W1, W2, W3, W4));
            list.Add(string.Format("                                      = {0} kN", top_vert_load));
            list.Add(string.Format(""));
            double bott_vert_load = W1 + W2 + W3 + W4 + W5;
            Pu_bottom = bott_vert_load;
            list.Add(string.Format("Total Vertical Load at Bottom of Footing = W1 + W2 + W3 + W4 + W5"));
            list.Add(string.Format("                                      = {0} + {1} + {2} + {3} + {4}", W1, W2, W3, W4, W5));
            list.Add(string.Format("                                      = {0} kN", bott_vert_load));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment at Supports in Longitudinal Direction = Mx1 = {0} kN-m", MX1));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment at Supports in Transverse Direction = My1 = {0} kN-m", MY1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Breaking Force :"));
            list.Add(string.Format("----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Vehicle Live Load =  LL = {0:f3} kN", LL));
            double Fh = LL * 0.2d;
            list.Add(string.Format(""));
            list.Add(string.Format("Fh = Breaking Force 20% = LL * 0.20 = {0:f3} kN", Fh));
            list.Add(string.Format(""));
            list.Add(string.Format("Temperature & Shrinkage Force on Bearings"));
            //double Vr = 2.5d;
            //double Itc = 4.21d;
            list.Add(string.Format(""));
            list.Add(string.Format("   Vr = {0} kN/mm", Vr));
            list.Add(string.Format("   Itc = {0} mm ", Itc));
            list.Add(string.Format(""));
            double force_each_bearing = (Fh / 2.0) + (Vr * Itc);
            list.Add(string.Format("Force at each Bearing = (Fh/2) + (Vr * Itc)"));
            list.Add(string.Format("                      = ({0}/2) + ({1} * {2})", Fh, Vr, Itc));
            list.Add(string.Format("                      = {0:f3} kN", force_each_bearing));
            list.Add(string.Format(""));
            double total_force = NP * force_each_bearing;
            list.Add(string.Format("Total Force for Four Bearings = {0} * {1} = {2} kN ", NP, force_each_bearing, total_force));
            //list.Add(string.Format("                             ", total_force));
            list.Add(string.Format(""));
            double HF1 = H1 + H2 + H5 + H6 + H7;
            list.Add(string.Format("Height from Top of Footing to Top of Bearing = HF1 = H1 + H2 + H5 + H6 + H7"));
            list.Add(string.Format(""));
            list.Add(string.Format("      HF1 = {0} + {1} + {2} + {3} + {4}", H1, H2, H5, H6, H7));
            list.Add(string.Format("          = {0} m", HF1));
            list.Add(string.Format(""));
            double HF2 = H1 + H2 + H3 + H4 + H5 + H6 + H7;
            list.Add(string.Format("Height from Top of Footing to Top of Bearing = HF2 = H1 + H2 + H3 + H4 + H5 + H6 + H7"));
            list.Add(string.Format(""));
            list.Add(string.Format("      HF2 = {0} + {1} + {2} + {3} + {4} + {5} + {6}", H1, H2, H3, H4, H5, H6, H7));
            list.Add(string.Format("          = {0:f3} m", HF2));
            list.Add(string.Format(""));
            double M1 = total_force * HF1;
            double BML1 =  M1;
            double BMT1 = 0.0;
            double M2 = total_force * HF2;
            double BML2 = M2;

            list.Add(string.Format("At Top of Footing,"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment at Top of Footing = BML1 = {0:f3} * {1:f3} = {2:f3} kN-m", total_force, HF1, M1));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment in Transverse Direction = BMT1 = 0.0"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("At Bottom of Footing,"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment at Bottom of Footing   = BML2 = {0:f3} * {1:f3} = {2:f3} kN-m", total_force, HF2, M2));
            //list.Add(string.Format("                                           = {0:f3} kN-m"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment in Transverse Direction = BMT2 = 0.0"));
            //list.Add(string.Format("Bending Moment in Transverse Direction = BMT1 = My1 + M1"));
            //list.Add(string.Format("                                       = {0} + {1}", MY1, M1));
            //list.Add(string.Format("                                       = {0} kN-m", BMT1));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
           
            //BML1 = MX1 + M2;
            //Mux = MX1 + M2;
            //list.Add(string.Format("Bending Moment in Longitudinal Direction = BML2 = Mx1 + M2"));
            //list.Add(string.Format("                                         = {0:f3} + {1:f3}", MX1, M2));
            //list.Add(string.Format("                                         = {0:f3} kN-m", BML2));
            //list.Add(string.Format(""));
            double BMT2 = 0.0;
            //Muy = MY1 + M2;
            //list.Add(string.Format("Bending Moment in Transverse Direction = BMT2 = 0.0"));
            //list.Add(string.Format("Bending Moment in Transverse Direction = BMT2 = My1 + M2"));
            //list.Add(string.Format("                                       = {0} + {1}", MY1, M2));
            //list.Add(string.Format("                                       = {0} kN-m", Muy));
            //list.Add(string.Format(""));



            #endregion Step 1


            #region Step 2 : CALCULATION FOR SEISMIC FORCE


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : CALCULATION FOR SEISMIC FORCE"));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));

            double PSL = P2 * SC;
            list.Add(string.Format("Horizontal Seismic Force in Longitudinal Direction = PSL = P2 x SC = {0:f3} x {1:f3} = {2:f3} kN.", P2, SC, PSL));
            list.Add(string.Format(""));
            double PST = P3 * SC;
            list.Add(string.Format("Horizontal Seismic Force in Transverse Direction = PST = P3 x SC = {0:f3} x {1:f3} = {2:f3} kN.", P3, SC, PST));
            list.Add(string.Format(""));
            list.Add(string.Format("Height from Top of Footing to Top of Bearing = HF1 = {0:f3} m.", HF1));
            list.Add(string.Format(""));

            double SML = PSL * HF1;
            list.Add(string.Format("Seismic Bending Moment in Longitudinal Direction = SML = {0:f3} x {1:f3} = {2:f3} kN-m.", PSL, HF1, SML));
            list.Add(string.Format(""));
            double SMT = PST * HF1;
            list.Add(string.Format("Seismic Bending Moment in Transverse Direction = SMT = {0:f3} x {1:f3} = {2:f3} kN-m.", PST, HF1, SMT));
            list.Add(string.Format(""));
            #endregion Step 2


            #region Step 3  : Calculation for Water Current Force

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3 : CALCULATION FOR WATER CURRENT FORCE"));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Water Current Force in Transverse Direction = PWT"));

            double PWT = 52 * K * V * V * (HHF * B9);
            list.Add(string.Format("                                                       = 52 x k x V^2 x (HHF x B9)"));
            list.Add(string.Format("                                                       = 52 x {0} x {1}^2 x ({2} x {3})", K, V, HHF, B9));
            list.Add(string.Format("                                                       = {0:f3} kg", PWT));
            PWT = (PWT / 100.0);
            list.Add(string.Format("                                                       = {0:f3} kN.", PWT));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double HWF = HHF / 2.0;
            list.Add(string.Format("Acts at Height = HWF = HHF/2.0 = {0}/2 = {1:f3} m. ", HHF, HWF));
            list.Add(string.Format(""));
            double WML = 0.0;
            list.Add(string.Format("Water Current Bending Moment in Longitudinal Direction = WML = {0}", WML));
            list.Add(string.Format(""));
            double WMT = PWT * HWF;
            list.Add(string.Format("Water Current Bending Moment in Transverse  Direction = WMT"));
            list.Add(string.Format("                                                      = PWT x HWF "));
            list.Add(string.Format("                                                      = {0:f3} x {1:f3} ", PWT, HWF));
            list.Add(string.Format("                                                      = {0:f3}  kN-m.", WMT));
            list.Add(string.Format(""));

            #endregion Step 3

            #region Step 4 : COMPUTED LOADS AND BENDING MOMENTS

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4 : COMPUTED LOADS AND BENDING MOMENTS"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("At Top of Footing,"));
            list.Add(string.Format(""));

            list.Add(string.Format("Total Vertical Axial Load = PU = {0:f3} kN", Pu_top));
            list.Add(string.Format(""));

            double Mx = MX1 + BML1 + SML + WML;
            list.Add(string.Format("Total Moment in Longitudinal Direction  = Mx "));
            list.Add(string.Format("                                        = Mx1 + BML1 + SML + WML"));
            list.Add(string.Format("                                        = {0:f3} + {1:f3} + {2:f3} + {3:f3}", MX1, BML1, SML, WML));
            list.Add(string.Format("                                        = {0:f3} kN-m", Mx));
            list.Add(string.Format(""));
            double My = MY1 + BMT1 + SMT + WMT;
            list.Add(string.Format("Total Moment in Transverse Direction = My "));
            list.Add(string.Format("                                     = My1 + BMT1 + SMT + WMT"));
            list.Add(string.Format("                                     = {0:f3} + {1:f3} + {2:f3} + {3:f3}", MY1, BMT1, SMT, WMT));
            list.Add(string.Format("                                     = {0:f3} kN-m", My));
            list.Add(string.Format(""));
            list.Add(string.Format("At Bottom of Footing,"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Vertical Axial Load = PU = {0:f3} kN", Pu_bottom));
            list.Add(string.Format(""));
            Mux = MX1 + BML2 + SML + WML;
            list.Add(string.Format("Total Moment in Longitudinal Direction  = Mux "));
            list.Add(string.Format("                                        = Mx1 + BML2 + SML + WML"));
            list.Add(string.Format("                                        = {0:f3} + {1:f3} + {2:f3} + {3:f3}", MX1, BML2, SML, WML));
            list.Add(string.Format("                                        = {0:f3} kN-m", Mux));
            list.Add(string.Format(""));
            Muy = MY1 + BMT2 + SMT + WMT;
            list.Add(string.Format("Total Moment in Transverse Direction = Muy "));
            list.Add(string.Format("                                     = My1 + BMT2 + SMT + WMT"));
            list.Add(string.Format("                                     = {0:f3} + {1:f3} + {2:f3} + {3:f3}", MY1, BMT2, SMT, WMT));
            list.Add(string.Format("                                     = {0:f3} kN-m", Muy));
            list.Add(string.Format(""));

            #endregion Step 4

            #region Step 5 : STABILITY CHECKS

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5 : STABILITY CHECKS"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Check for Sliding:"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));

            double SFL = PSL;
            list.Add(string.Format("Horizontal Sliding Force in Longitudinal Direction = Longitudinal Seismic Force"));
            list.Add(string.Format("                                                   = SFL = PSL = {0:f3} kN", SFL));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double SFT = PST + PWT;
            list.Add(string.Format("Horizontal Sliding Force in Transverse Direction = Transverse Seismic Force"));
            list.Add(string.Format("                                                 = SFT = PST + PWT "));
            list.Add(string.Format("                                                 = {0:f3} + {1:f3} kN", PST, PWT));
            list.Add(string.Format("                                                 = {0:f3} kN", SFT));
            //list.Add(string.Format("                                                 = 180.0 kN (say)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double RF = Pu_bottom * CF;
            list.Add(string.Format("Resisting Force = RF = (w1+w2+w3+w4+w5) x CF = {0:f2} x {1:f3} = {2:f2} kN", Pu_bottom, CF, RF));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double safe_fact = RF / SFL;
            if (safe_fact >= 1.5)
                list.Add(string.Format("Factor of Safety in Longitudinal Direction = RF/SFL = {0:f2}/{1:f2} = {2:f2} >= 1.5, OK", RF, SFL, safe_fact));
            else
                list.Add(string.Format("Factor of Safety in Longitudinal Direction = RF/SFL = {0:f2}/{1:f2} = {2:f2} < 1.5, NOT OK", RF, SFL, safe_fact));
            
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            safe_fact = RF / SFT;

            if (safe_fact >= 1.5)
                list.Add(string.Format("Factor of Safety in Transverse Direction = RF/SFT = {0:f2}/{1:f2} = {1:f2} >= 1.5, OK", RF, SFT, safe_fact));
            else
                list.Add(string.Format("Factor of Safety in Transverse Direction = RF/SFT = {0:f2}/{1:f2} = {1:f2} < 1.5, NOT OK", RF, SFT, safe_fact));

            list.Add(string.Format(""));
            list.Add(string.Format("Check for Overturning"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format("At Top of Footing,"));
            list.Add(string.Format(""));

            double OML = Mx;
            list.Add(string.Format(""));
            list.Add(string.Format("Overturning Moment in Longitudinal Direction = OML = {0} kN-m.", OML));
            list.Add(string.Format(""));
            double OMT = My;
            list.Add(string.Format("Overturning Moment in Transverse Direction   = OMT = {0:f3} kN-m.", OMT));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double RML = Pu_bottom * (B6 / 2.0);
            list.Add(string.Format("Resisting Moment in Longitudinal Direction  = RML"));
            list.Add(string.Format("                                            = (w1+w2+w3+w4+w5) x (B6/2)"));
            list.Add(string.Format("                                            = {0:f3} x ({1}/2)", Pu_bottom, B6));
            list.Add(string.Format("                                            = {0:f3} kN-m.", RML));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double RMT = Pu_bottom * (B7 / 2.0);
            list.Add(string.Format("Resisting Moment in Transverse Direction = RMT"));
            list.Add(string.Format("                                         =(w1+w2+w3+w4+w5) x (B7/2)"));
            list.Add(string.Format("                                         = {0:f3} x ({1}/2)", Pu_bottom, B7));
            list.Add(string.Format("                                         = {0:f3} kN-m.", RMT));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            safe_fact = RML / OML;
            if (safe_fact >= 1.5)
                list.Add(string.Format("Factor of Safety in Longitudinal Direction = RML/OML = {0:f3} / {1:f3} = {2:f3} >= 1.5, OK", RML, OML, safe_fact));
            else
                list.Add(string.Format("Factor of Safety in Longitudinal Direction = RML/OML = {0:f3} / {1:f3} = {2:f3} < 1.5, NOT OK", RML, OML, safe_fact));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            safe_fact = RMT / OMT;

            if (safe_fact >= 1.5)
                list.Add(string.Format("Factor of Safety in Transverse Direction = RMT/OMT = {0:f3} / {1:f3} = {2:f3} >= 1.5, OK", RMT, OMT, safe_fact));
            else
                list.Add(string.Format("Factor of Safety in Transverse Direction = RMT/OMT = {0:f3} / {1:f3} = {2:f3} < 1.5, NOT OK", RMT, OMT, safe_fact));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check for Eccentricity"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Size of Open Pad Foundation = B7 x B6 = {0} x {1} sq.m.", B7, B6));
            list.Add(string.Format(""));
            list.Add(string.Format("Overturning Moment in Longitudinal Direction = OML = {0:f3} kN-m.", OML));
            list.Add(string.Format(""));
            list.Add(string.Format("Overturning Moment in Transverse Direction   = OMT = {0:f3} kN-m.", OMT));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Axial Force = V = w1+w2+w3+w4+w5 = {0:f3} kN.", Pu_top));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Xo = (RMT - RML) / Pu_top;
            list.Add(string.Format("Location of Resultant Force from edge of Base = Xo "));
            list.Add(string.Format("                                              = (RMT – RML) / V"));
            list.Add(string.Format("                                              = ({0:f3} – {1:f3})/{2:f3}", RMT, RML, Pu_top));
            list.Add(string.Format("                                              = {0:f3} m.", Xo));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double max_prmsbl_eccet = B6 / total_pairs;
            list.Add(string.Format("Maximum Permissible Eccentricity = {0}/{1} = {2:f3} m.", B6, total_pairs, max_prmsbl_eccet));
            list.Add(string.Format(""));

            double eccent_rslnt = (B6 / 2) - Xo;
            list.Add(string.Format("Eccentricity of Resultant = ({0}/2) – Xo", B6));
            list.Add(string.Format("                          = {0:f3} – {1:f3}", (B6/2.0), Xo));
            if (eccent_rslnt < max_prmsbl_eccet)
                list.Add(string.Format("                          = {0:f3}  <  {1:f3} (Maximum Permissible Eccentricity),  OK", eccent_rslnt, max_prmsbl_eccet));
            else
                list.Add(string.Format("                          = {0:f3}  >=  {1:f3} (Maximum Permissible Eccentricity),  NOT OK", eccent_rslnt, max_prmsbl_eccet));

            list.Add(string.Format(""));

            #endregion Step 5


            #region Step 6 : DESIGN OF PIER STEM

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 6 : DESIGN OF PIER STEM"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Reinforcements:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Standard Minimum Reinforcement = p1 = {0} %", p1));
            list.Add(string.Format("Design Trial Reinforcement = p2 = {0} %", p2));
            list.Add(string.Format("Reinforcement Cover = d’ = {0} mm.", d_dash));
            list.Add(string.Format("Concrete grade = M{0} fck={0} N/Sq.mm", fck1));
            list.Add(string.Format("Steel grade=Fe{0} fy={0}  N/Sq.mm", fy1));
            list.Add(string.Format("Width of Pier in Transverse direction = D = {0} m. = {1} mm.", D / 1000, D));
            list.Add(string.Format("Width of Pier in Longitudinal direction = b ={0} m. = {1} mm.", b / 1000, b));
            //D = D*1000;
            //b = b*1000;

            list.Add(string.Format(""));
            list.Add(string.Format("DESIGN FORCES :"));
            list.Add(string.Format("---------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pu={0} kN.", Pu_top));
            list.Add(string.Format("Mux={0} kN-m.", Mux));
            list.Add(string.Format("Muy={0} kN-m.", Muy));
            list.Add(string.Format(""));


            double p = p1 * p2;
            list.Add(string.Format("Reinforcement Percentage = p = p1 x p2 = {0:f2} x {1:f2} = {2:f3} %", p1, p2, p));
            list.Add(string.Format(""));
            double val1 = p / fck2;
            list.Add(string.Format("p/fck = {0:f3}/{1} = {2:f3}", p, fck2, val1));
            list.Add(string.Format(""));

            list.Add(string.Format("Uniaxial Moment capacity of the section about X_X Axis"));
            list.Add(string.Format(""));

            double val2 = d_dash / D;
            list.Add(string.Format("d’/D = {0}/{1} = {2:f4}", d_dash, D, val2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("From Interaction diagram chart for Fe 415,   and for  d’/D = 0.0067, refer for d’/D = 0.05 is to be used,"));
            if (val2 < 0.05)
            {
                list.Add(string.Format("For fy = {0},   and for  d’/D = {1:f4} < 0.05, refer for d’/D = 0.05 is to be used,", fy2, val2));
                val2 = 0.05;
            }
            else
            {
                list.Add(string.Format("For fy {0},   and for  d’/D = {1:f4}", fy2, val2));
            }
            list.Add(string.Format(""));

            double val3 = (Pu_top * 1000.0) / (fck2 * b * D);
            //list.Add(string.Format("For        Pu/(fck x b x D) = 5918 x 1000 / (30 x 1000 x 6000) = 0.0329 and p/fck = 0.042"));
            list.Add(string.Format("For        Pu/(fck x b x D)"));
            list.Add(string.Format("         = ({0} x 1000) / ({1} x {2} x {3})", Pu_top, fck1, b, D));
            list.Add(string.Format("         = {0:f4} and p/fck = {1:f4}", val3, val1));
            list.Add(string.Format(""));

            //Here to add new File

               List<string> file_cont = new List<string>();
               file_cont.Add(string.Format("fy1${0}", fy1));
               file_cont.Add(string.Format("fy2${0}", fy2));
               file_cont.Add(string.Format("fck1${0}", fck1));
               file_cont.Add(string.Format("b${0}", b));
               file_cont.Add(string.Format("D${0}", D));
               file_cont.Add(string.Format("d_dash${0}", d_dash));
               file_cont.Add(string.Format("Pu_top${0}", Pu_top));
               file_cont.Add(string.Format("fck2${0}", fck2));
               file_cont.Add(string.Format("Muy${0}", Muy));
               file_cont.Add(string.Format("Mux${0}", Mux));
               file_cont.Add(string.Format("SetText1_Tab1${0}", string.Format("Refer to Interaction diagram chart for fy(Fe) = {0} and for  d’/D = {1:f2}", fy1, val2)));
               file_cont.Add(string.Format("SetText2_Tab1${0}", string.Format("For values of: Pu/(fck x b x D) = {0:f4} and the value of: p/fck = {1:f4}", val3, val1)));
               file_cont.Add(string.Format("SetText3_Tab1${0}", string.Format("We get the value of: Mux1 / (fck x b x D x D)")));

               file_cont.Add(string.Format("SetText1_Tab11${0}", string.Format("Refer to Interaction diagram chart for fy(Fe) = {0} and for  d’/D = {1:f2}", fy1, val2)));
               file_cont.Add(string.Format("SetText2_Tab11${0}", string.Format("For values of: Pu/(fck x b x D) = {0:f4} and the value of: p/fck = {1:f4}", val3, val1)));
               file_cont.Add(string.Format("SetText3_Tab11${0}", string.Format("We get the value of: Muy1 / (fck x b x b x D)")));

               file_cont.Add(string.Format("SetText1_Tab2${0}", string.Format("Refer to Interaction diagram chart for p = {0}%, fy(Fe) = {1}, fck={2} N/Sq.mm", p, fy2, fck2)));
               file_cont.Add(string.Format("SetText2_Tab2${0}", string.Format("We get the value of:   Puz / Ag")));
               file_cont.Add(string.Format("SetText2_Tab3${0}", string.Format("We get the value of: Mux / Mux1")));


             
               string temp_pier = Path.Combine(Application.StartupPath, "pier.tmp");
               File.WriteAllLines(temp_pier, file_cont.ToArray());
               double p_val1 = 0.750;
               double p_val11 = 0.750;
               double p_val2 = 15.0;
               double p_val3 = 0.97;
             


               try
               {

                   string cad_exe = Path.Combine(Application.StartupPath, "CadInteractives.exe");
                   if (File.Exists(cad_exe))
                       System.Diagnostics.Process.Start(cad_exe).WaitForExit();

                   if (!File.Exists(temp_pier)) return;
                   file_cont = new List<string>(File.ReadAllLines(temp_pier));

                   MyList mlist = null;
                   string kStr = "";
                   for (int i = 0; i < file_cont.Count; i++)
                   {
                       if (file_cont.Count > 15)
                       {
                           return;
                       }
                       kStr = file_cont[i];
                       mlist = new MyList(kStr, '$');
                       if (mlist.Count == 2)
                       {
                           if (mlist[0] == "InputValue1") p_val1 = mlist.GetDouble(1);
                           if (mlist[0] == "InputValue11") p_val11 = mlist.GetDouble(1);
                           if (mlist[0] == "InputValue2") p_val2 = mlist.GetDouble(1);
                           if (mlist[0] == "InputValue3") p_val3 = mlist.GetDouble(1);
                       }
                   }
                   File.Delete(temp_pier);
               }
               catch (Exception ex) { }

            double inp_val1 = p_val1;
            double Mux1 = inp_val1 * (fck1 * b * D * D);
            list.Add(string.Format(""));
            list.Add(string.Format("Mux1 / (fck x b x D x D) = {0} is obtained from the Graph (Interaction diagram chart 1)", inp_val1));
            list.Add(string.Format(""));
            list.Add(string.Format("Mux1 = {0} x fck x b x D  x D ", inp_val1));
            list.Add(string.Format("     = {0} x {1} x {2} x {3} x {3} ", inp_val1, fck1, b, D));
            list.Add(string.Format("     = {0:f3} N-mm.", Mux1));
            Mux1 = Mux1 / 1000000;
            list.Add(string.Format("     = {0:f3} kN-m.", Mux1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Uniaxial Moment capacity of the section about Z_Z Axis"));
            list.Add(string.Format(""));

            double val4 = d_dash / b;

            list.Add(string.Format("d’/D = d’/b = {0}/{1} = {2:f3}", d_dash, b, val4));
            list.Add(string.Format(""));
            //list.Add(string.Format("For fy(Fe) {0},", fy2));
            //list.Add(string.Format("and for  d’/D = {0}, refer for d’/D = {0} is to be used,", inp_val1));
            //list.Add(string.Format(""));
            list.Add(string.Format("For fy(Fe) = {0} and for  d’/D = {1:f2}", fy1, val2));
            //list.Add(string.Format("For values of: Pu/(fck x b x D) = {0:f4} and the value of: p/fck = {1:f4}", val3, val1));
            //list.Add(string.Format("the value of Mux1/(fck x b x D x D) = 0.075 is obtained from the Graph"));
            //list.Add(string.Format("Muy1/(fck x b x b x D) = {0:f3} is obtained from the Graph (Interaction diagram chart 1)", p_val11));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            //For values of: Pu/(fck x b x D) = 0.0329 and the value of: p/fck = 0.042
            double val5 = (Pu_top * 1000) / (fck2 * b * D);
            list.Add(string.Format("For    Pu/(fck x b x D)"));
            list.Add(string.Format("      = {0} x 1000 / ({1} x {2} x {3})", Pu_top, fck2, b, D));
            list.Add(string.Format("      = {0:f4} and p/fck = {1:f4}", val5, val1));
            list.Add(string.Format(""));

            inp_val1 = p_val11;
            double Muy1 = inp_val1 * (fck1 * b * b * D );
            list.Add(string.Format("Muy1 / (fck x b x b x D) = {0:f3} is obtained from the Graph (Interaction diagram chart 1)", inp_val1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Muy1 = {0} x fck x b x b x D ", inp_val1));
            list.Add(string.Format("     = {0} x {1} x {2} x {2} x {3}", inp_val1, fck1, b, D));
            list.Add(string.Format("     = {0:f3} N-mm.", Muy1));
            Muy1 = Muy1 / 10E5;
            list.Add(string.Format("     = {0:f3} kN-m.", Muy1));
            //list.Add(string.Format("Same as about X_X Axis,"));

            //list.Add(string.Format(""));
            //list.Add(string.Format("For     p = {0}%, fy = {1}, fck = {2}   N/Sq.mm", p, fy2, fck2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("For p = {0}%, fy(Fe) = {1}, fck={2} N/Sq.mm", p, fy2, fck2));
            //list.Add(string.Format("From Interaction diagram chart 2 We get the value of : Puz / Ag = {0:f3}", p_val2));
            list.Add(string.Format(""));
            list.Add(string.Format("Puz / Ag = {0:f3} is obtained from the Graph (Interaction diagram chart 2)", p_val2));


            list.Add(string.Format(""));
            //User Input
            //f_us_inp = new frmUserInput();
            //f_us_inp.SetText1 = "";
            //f_us_inp.SetText2 = string.Format("Refer to Interaction diagram chart for p = {0}%, fy = {1}, fck={2} N/Sq.mm", p, fy2, fck2);
            //f_us_inp.SetText3 = string.Format("We get the value of:   Puz / Ag");
            //f_us_inp.InputValue = 16.5;
            //f_us_inp.ShowDialog();
            //double inp_val1 = f_us_inp.InputValue;
            inp_val1 = p_val2;
            //list.Add(string.Format("From Interaction diagram chart   Puz / Ag = {0}   N/Sq.mm.", inp_val1));
            double Puz = inp_val1 * b * D;
            list.Add(string.Format("Puz = {0} x {1} x {2}", inp_val1, b, D));
            list.Add(string.Format("    = {0} N", Puz));
            Puz = Puz / 1000.0;
            list.Add(string.Format("    = {0} kN", Puz));

            list.Add(string.Format(""));

            double val6 = Pu_top / Puz;
            list.Add(string.Format("Pu / Puz = {0} / {1} = {2:f4}", Pu_top, Puz, val6));
            double val7 = Mux / Mux1;
            list.Add(string.Format("Mux / Mux1 = {0:f4} / {1:f4} = {2:f4}", Mux, Mux1, val7));
            double val8 = Muy / Muy1;
            list.Add(string.Format("Muy / Muy1 = {0:f3} / {1:f3} = {2:f3}", Muy, Muy1, val8));
            //User Input

            //list.Add(string.Format("From Interaction diagram chart 3  "));
            list.Add(string.Format(""));
            double val9 = Pu_top / Puz;
            double val10 = Muy / Muy1;
            double val11 = Mux / Mux1;
            list.Add(string.Format("For   Pu / Puz = {0:f3} and  Muy / Muy1 = {1:f4}", val9, val10));
            //list.Add(string.Format("    "));
            //list.Add(string.Format("      Mux / Mux1 = {0:f4} = {0:f2}", val11));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Mux / Mux1 = {0:f3} is obtained from the Graph (Interaction diagram chart 3)", p_val3));
            //list.Add(string.Format("We get the value of: Mux / Mux1 = {0:f3}", p_val3));

            //f_us_inp = new frmUserInput();
            //f_us_inp.SetText1 = "";
            //f_us_inp.SetText2 = string.Format("Refer to Interaction diagram chart for Pu / Puz = {0:f4} and Muy / Muy1 = {1:f4}", val9, val10);
            //f_us_inp.SetText3 = string.Format("We get the value of: Mux / Mux1");
            //f_us_inp.InputValue = 0.94;
            //f_us_inp.ShowDialog();
            ////double inp_val1 = f_us_inp.InputValue;
            inp_val1 = p_val3;


            list.Add(string.Format(""));
            if (val11 > inp_val1)
            {
                list.Add(string.Format("This value is higher than actual values of Mux / Mux1 = {0},    So, OK", inp_val1));
            }
            else if (val11 < inp_val1)
            {
                list.Add(string.Format("This value is less than actual values of Mux / Mux1 = {0},    So, OK", inp_val1));
            }


            list.Add(string.Format(""));
            double req_area = p * b * D / 100.0;
            list.Add(string.Format("Required area for Steel Reinforcement = As = p x b x D / 100 "));
            list.Add(string.Format("                                      = {0} x {1} x {2}/ 100", p, b, D));
            list.Add(string.Format("                                      = {0:f3} Sq.mm.", req_area));
            list.Add(string.Format(""));





            list.Add(string.Format(""));
            list.Add(string.Format("For Pier Stem Structural Design:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter Vertical Steel reinforcement = {0} mm.", rdia));
            list.Add(string.Format("Diameter Lateral  Ties steel reinforcement = {0} mm.", tdia));
            list.Add(string.Format(""));
            //double d_d1 = 32;
            double d_d1 = rdia; //Input for Rebar dia




            val3 = Math.PI * d_d1 * d_d1 / 4.0;

            int nos_bars = (int)( req_area / val3);
            nos_bars++;
            list.Add(string.Format("Area of one bar = 3.1416 x {0}^2/4 = {1:f3} Sq.mm.", d_d1, val3));

            list.Add(string.Format("Total Number of bars required = {0:f3}/{1:f3} = {2} nos.", req_area, val3, nos_bars));
            list.Add(string.Format(""));
            //double nos_bars = 94;
            list.Add(string.Format("Provide {0:f0} numbers {1} mm. diameter steel reinforcement vertical bars ", nos_bars, rdia));
            list.Add(string.Format("all around the perimeter of the Pier,"));
            list.Add(string.Format(""));
            val1 = 2 * (b + D);
            list.Add(string.Format("Perimeter of the Pier = 2 x ({0} + {1}) = {2:f3} mm.", b, D, val1));
            val2 = val1 / nos_bars;
            list.Add(string.Format("Spacing of the bars = {0:f3} / {1:f0} = {2:f0} mm. marked as (A) in the drawing.", val1, nos_bars, val2));

            list.Add(string.Format(""));
            list.Add(string.Format("Provide {0} mm. diameter lateral ties in zig zug manner marked as (B) in the drawing.", tdia));
            list.Add(string.Format(""));

            #endregion Step 6


            #region Step 7 DESIGN OF PIER CAP

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 7 : DESIGN OF PIER CAP"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Computation of Loads and Moments:"));
            list.Add(string.Format("---------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Cantilever End"));
            list.Add(string.Format("--------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double volume_conic_part = ((B11 + B13) * (H6 / 2.0) + 0.0) * B16;
            list.Add(string.Format("Volume of Conical Part = [(B11 + B13) x H6/2 + 0.0] x B16"));
            list.Add(string.Format("                       = [({0} + {1}) x {2}/2 + 0.0] x {3}", B11, B13, H6, B16));
            list.Add(string.Format("                       = {0:f3} Cu.m", volume_conic_part));
            list.Add(string.Format(""));


            double PW1 = volume_conic_part * gama_c;
            list.Add(string.Format(""));
            list.Add(string.Format("Weight of Conical Part = PW1 = {0:f3} x {1} = {2:f3} kN", volume_conic_part, gama_c, PW1));
            list.Add(string.Format(""));

            double dist_face_pier = B16 / 3.0;

            list.Add(string.Format("Distance from face of Pier = B16/3 = {0}/3 = {1:f3} m.", B16, dist_face_pier));
            list.Add(string.Format(""));

            double PM1 = PW1 * dist_face_pier;
            list.Add(string.Format("Moment about face of Pier = PM1 = {0:f3} x {1:f3} = {2:f3} kN-m.", PW1, dist_face_pier, PM1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double volume_rect_part = B13 * H5 * B16;
            list.Add(string.Format("Volume of Rectangular Part = B13 x H5 x B16 "));
            list.Add(string.Format("                           = {0} x {1} x {2}", B13, H5, B16));
            list.Add(string.Format("                           = {0} Cu.m", volume_rect_part));
            list.Add(string.Format(""));

            double PW2 = volume_rect_part * gama_c;
            list.Add(string.Format("Weight of Rectangular Part = PW2 = {0:f3} x {1} = {2:f3} kN", volume_rect_part,gama_c, PW2));
            list.Add(string.Format(""));
            double dist_face_pier2 = B16 / 2;
            list.Add(string.Format("Distance from face of Pier = B16/2 = {0}/2 = {1:f3} m.", B16, dist_face_pier2));
            list.Add(string.Format(""));

            double PM2 = PW2 * dist_face_pier2;
            list.Add(string.Format("Moment about face of Pier = PM2 = {0:f3} x {1} = {2:f3} kN-m.", PW2, dist_face_pier2, PM2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Support Pedestals"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            double volume_pedestal = 2 * B1 * B2 * H1;
            list.Add(string.Format(""));
            list.Add(string.Format("Volume of each Pedestal = 2 x B1 x B2 x H1"));
            list.Add(string.Format("                        = 2 x {0} x {1} x {2}",B1, B2, H1));
            list.Add(string.Format("                        = {0:f3} Cu.m.", volume_pedestal));
            list.Add(string.Format(""));
            double wgt_pedestal = volume_pedestal * gama_c;
            list.Add(string.Format("Weight of each Pedestal = {0:f3} x {1} = {2:f3} kN", volume_pedestal, gama_c, wgt_pedestal));
            list.Add(string.Format(""));
            double PW3 = wgt_pedestal * 2;
            list.Add(string.Format("Weight of each pair of Pedestals = PW3 = {0:f3} x 2 = {1:f3} kN.", wgt_pedestal, PW3));
            double PW4 = PL;
            list.Add(string.Format("Load Reaction on each pair of Pedestals = PW4 = {0:f3} kN", PW4));
            list.Add(string.Format(""));
            double _dist, _mom;

            List<double> _PM_ = new List<double>();
            for(int i = 0; i < PD_nped.Count; i++)
            {
                _dist = PD_nped[i];
                _mom = (PW3 + PW4) * _dist + PML;
                list.Add(string.Format(""));
                list.Add(string.Format("Distance from face of Pier = PD[n{0}] = {1} m. = PM{2}", (i+1), _dist, (i+3)));
                list.Add(string.Format("Moment about face of Pier  = ({0:f3} + {1:f3}) x {2} + {3:f3} = {4:f3} kN-m.",
                    PW3, PW4, _dist, PML, _mom));
                _PM_.Add(_mom);
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double PCL = PW1 + PW2 + nped * (PW3 + PW4);
            list.Add(string.Format("Total Vertical Force at face of Pier = PCL = PW1 + PW2 + nped x (PW3 + PW4)"));
            list.Add(string.Format("                                     = {0:f3} + {1:f3} + {2} x ({3:f3} + {4:f3})",
                                                                            PW1, PW1, nped, PW3, PW4));
            list.Add(string.Format("                                     = {0:f3} kN.", PCL));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            string kstr = "PM1 + PM2";
            string kstr_val = PM1 + " " + PM2;
            double PCM = PM1 + PM2;
            for (int i = 0; i < _PM_.Count;i++)
            {
                kstr += " + PM" + (i + 3);
                kstr_val += " + " + _PM_[i].ToString("f3");
                PCM += _PM_[i];
            }
            list.Add(string.Format("Total Moment at face of Pier   = PCM = {0}", kstr));
            list.Add(string.Format("                               = {0} kN-m.", kstr_val));
            list.Add(string.Format("                               = {0:f3} kN-m.", PCM));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Structural Design:"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            
            list.Add(string.Format("Concrete Grade M {0} = fck", fck1));
            list.Add(string.Format("Steel Grade Fe {0} = fy", fy1));
            list.Add(string.Format(""));
            double sigma_c = perm_flex_stress;

            list.Add(string.Format("Permissible Flexural Compressive Stress in Concretec = {0:f3} N/Sq.mm  = sigma_c", perm_flex_stress));
            list.Add(string.Format(""));
            list.Add(string.Format("Permissible Stress in Steel = {0} N/Sq.mm  = sigma_s", sigma_s));
            list.Add(string.Format(""));
            list.Add(string.Format("Modular Ratio = {0} = m", m));
            list.Add(string.Format(""));

            double R = (m * sigma_c) / ((m * sigma_c) + sigma_s);
            list.Add(string.Format("Neutral Axis Factor =  R = (m x sigma_c) / ((m x sigma_c) + sigma_s)"));
            list.Add(string.Format("                    = ({0} x {1}) / (({0} x {1}) + {2})", m, sigma_c, sigma_s));
            list.Add(string.Format("                    = {0:f3}", R));
            list.Add(string.Format(""));
            double j = (1 - (R / 3));
            list.Add(string.Format("Lever Arm Factor = j = (1 – (R/3)) = (1 – ({0:f3}/3)) = {1:f3}", R, j));
            list.Add(string.Format(""));
            double Q = (sigma_c*j*R)/2.0;
            list.Add(string.Format("Coefficient of Resisting Moment  = Q = (sigma_c x j x R) / 2 "));
            list.Add(string.Format("                                 = ({0} x {1:f3} x {2:f3})/2", sigma_c, j, R));
            list.Add(string.Format("                                 = {0:f3} N/Sq.m", Q));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Main Top Reinforcements:"));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double overall_depth = H5 + H6;
            list.Add(string.Format("Overall Depth provided = H5 + H6 = {0} + {1} = {2} m = {3} mm.", H5, H6, overall_depth, (overall_depth = overall_depth * 1000)));

            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("For Pier Cap Structural Design:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter of Reinforcement Bars = ldia = {0} mm.", ldia));
            list.Add(string.Format("Shear Reinforcements = {0} mm. and Legs = sleg = {1}", sdia, sleg));
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Reinforcements:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter of Longitudinal Reinforcement Bars = ldia = {0} mm.", ldia));
            list.Add(string.Format("Horizontal Stirrups: {0} mm dia., {1} Legged Stirrups", hdia, hlegs));
            list.Add(string.Format("Vertical Stirrups: {0} mm dia., {1} Legged Stirrups", vdia, vlegs));
            list.Add(string.Format(""));


            double dia = ldia;
            double cover = d_dash;
            //list.Add(string.Format("Reinforcement bar Diameter = dia = {0} mm.", ldia));
            list.Add(string.Format("Reinforcement Clear Cover = cover = {0} mm.", d_dash));
            list.Add(string.Format(""));

            double eff_depth = overall_depth - cover - (dia / 2.0);
            list.Add(string.Format("Effective Depth Provided = d = {0} – {1} – ({2}/2)", overall_depth, cover, dia));
            list.Add(string.Format("                             = {0:f3} mm.", eff_depth));
            list.Add(string.Format(""));
            
            list.Add(string.Format("Width of Pier Cap = b = B13 = {0} m.", B13));
            list.Add(string.Format(""));

            double req_eff_depth = (PCM * 1000000) / (Q * b * 1000);
            req_eff_depth = Math.Sqrt(req_eff_depth);

            list.Add(string.Format("Required Effective Depth of Pier Cap  = Sqrt(PCM x 10^6 / (Q x b x 1000))"));
            list.Add(string.Format("                                      = Sqrt({0:f3} x 10^6 / ({1:f3} x {2:f3} x 1000))", PCM, Q, b));
            if (req_eff_depth <= eff_depth)
            {
                list.Add(string.Format("                                      = {0:f3} mm. <= {1:f3} mm.,  OK", req_eff_depth, eff_depth));
            }
            else
            {
                list.Add(string.Format("                                      = {0:f3} mm. > {1:f3} mm.,  NOT OK", req_eff_depth, eff_depth));
            }

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            req_area = (PCM * 1000000) / (sigma_s * j * eff_depth);
            list.Add(string.Format("Area of Steel Required   = PCM x 10^6 / (sigma_s x j x d)"));
            list.Add(string.Format("                         = {0:f3} x 10^6 / ({1} x {2:f3} x {3:f3})", PCM, sigma_s, j, eff_depth));
            list.Add(string.Format("                         = {0:f3} Sq.mm", req_area));
            list.Add(string.Format(""));
            double area_each = Math.PI * dia * dia / 4;
            list.Add(string.Format("Area of each bar = 3.1416 x dia^2 / 4 = 3.1416 x {0}^2 /4 = {1} Sq.mm", dia, area_each));
            list.Add(string.Format(""));
            int nbars =(int) (req_area / area_each);
            nbars++;
            list.Add(string.Format("Total Number of bars required = nbar = {0:f3} / {1:f3} = {2:f3} = {3} ", req_area, area_each, (req_area / area_each), nbars));
            list.Add(string.Format(""));

            double steel_ast = area_each * nbars;
            list.Add(string.Format("Steel provided = Ast = (3.1416 x dia^2 / 4) x nbar "));
            list.Add(string.Format("                     = (3.1416 x {0}^2/4) x {1}", dia, nbars));
            list.Add(string.Format("                     = {0:f3} Sq.mm", steel_ast));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear Reinforcements:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear Reinforcements = {0} mm. and Legs = sleg = {1}", sdia, sleg));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double _b = B13;
            double _d = eff_depth;
            list.Add(string.Format("Width of Pier Cap = b = B13 = {0} x 1000 mm.", _b));
            list.Add(string.Format("Effective Depth of Pier Cap = d = {0:f3} mm.", _d));
            list.Add(string.Format(""));
            
            list.Add(string.Format("Vertical Force at face of Pier = PCL = {0:f3} kN", PCL));
            list.Add(string.Format(""));
            list.Add(string.Format("Torsional Moment = My1 = {0} kN-m.", MY1));

            double eqv_shr_frc = (1.6 * MY1 / _b);
            list.Add(string.Format("Equivalent Shear Force = 1.6 x My1 / b = 1.6 x {0:f3} / {1} = {2:f3} kN.", MY1, _b, eqv_shr_frc));
            list.Add(string.Format(""));

            double Vu = PCL + eqv_shr_frc;
            list.Add(string.Format("Total Shear Force = Vu = {0:f3} kN.", Vu));
            double shr_strs = Vu * 1000 / (_b * 1000 * _d);
            list.Add(string.Format("Shear Stress = Vu x 1000 / (b x d)"));
            list.Add(string.Format("             = {0:f3} x 1000/({1} x 1000 x {2:f3})", Vu, _b, _d));
            list.Add(string.Format("             = {0:f3} N/Sq.mm", shr_strs));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double sper = (steel_ast * 100.0) / (_b * 1000 * _d);
            list.Add(string.Format("Required  Shear Reinforcements  = sper = Ast x 100/ (b x d) "));
            list.Add(string.Format("                                = {0:f3} x 100 / ({1} x 1000 x {2:f3})", steel_ast, _b, _d));
            list.Add(string.Format("                                = {0:f3} %", sper));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable Shear stress for Concrete Grade {0}:", fck1));
            list.Add(string.Format(""));
            double beta = (0.8 * fck1) / (6.89 * sper);
            list.Add(string.Format("Beta = 0.8 x fck / (6.89 x sper) = 0.8 x {0} / (6.89 x {1:f3}) = {2:f3} N/Sq.mm", fck1, sper, beta));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear Capacity of Concrete = scap "));
            list.Add(string.Format(""));

            double scap = 0.85 * Math.Sqrt(0.8 * fck1) * (Math.Sqrt(1 + 5 * beta) - 1) / (6 * beta * 1.6);
            double bals = (shr_strs - scap) * (_b * 1000) * _d;
            double min_spc = (3.1416 * sdia * sdia / 4) * (sleg * 0.87 * sigma_s) / (0.4 * b);
            double min_space = (Math.PI * sdia * sdia / 4) * (sleg * 0.87 * sigma_s) / (0.4 * _b * 1000);
            double req_spacing = (sleg * Math.PI * sdia * sdia / 4) * (sigma_s * _d / bals);

            list.Add(string.Format(" scap = 0.85 x sqrt(0.8 x fck)x(sqrt(1 + 5 x Beta) – 1)/(6 x Beta x 1.6)"));
            list.Add(string.Format("      = 0.85 x sqrt(0.8 x {0})x(sqrt(1 + 5 x {1:f3}) – 1)/(6 x {1:f3} x 1.6)", fck1, beta));
            if (scap < shr_strs)
            {
                list.Add(string.Format("      = {0:f4} N/Sq.mm < {1:f3} N/Sq.mm", scap, shr_strs));
                list.Add(string.Format(""));
                list.Add(string.Format("Balance Shear force = bals "));
                list.Add(string.Format("                    = ({0:f3} – {1:f3}) x b x d", shr_strs, scap));
                list.Add(string.Format("                    = {0:f3} x {1:f3} x 1000 x {2:f3}", (shr_strs - scap), _b, _d));
                list.Add(string.Format("                    = {0:f3} N", bals));
                list.Add(string.Format("                    = {0:f3} kN", bals / 1000.0));
                list.Add(string.Format(""));

                list.Add(string.Format(""));
                list.Add(string.Format("Using Bar Dia = sdia = {0} mm. and Legs = sleg = {1}", sdia, sleg));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Required Spacing  = (sleg x 3.1416 x sdia^2/4) x sigma_s x d / (bals)"));
                list.Add(string.Format("                  = ({0} x 3.1416 x {1}^2/4) x {2} x {3:f3} / {4:f3}", sleg,
                    sdia, sigma_s, _d, bals));
                list.Add(string.Format("                  = {0:f3} mm. (Provide)", req_spacing));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("Minimum Spacing   = (3.1416 x sdia^2/4) x (sleg x 0.87 x sigma_s) / (0.4 x b)"));
                list.Add(string.Format("                  = (3.1416 x {0}^2/4) x ({1} x 0.87 x {2}) / (0.4 x {3:f3} x 1000)",
                    sdia, sleg, sigma_s, _b));
                if (min_space > req_spacing)
                    list.Add(string.Format("                  = {0:f3} mm. > {1:f3} mm.", min_space, req_spacing));
                else
                    list.Add(string.Format("                  = {0:f3} mm. < {1:f3} mm.", min_space, req_spacing));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else if (scap == shr_strs)
                list.Add(string.Format("                  = {0:f4} N/Sq.mm = {1:f3} N/Sq.mm", scap, shr_strs));
            else
            {
                list.Add(string.Format("                  = {0:f4} N/Sq.mm > {1:f3} N/Sq.mm", scap, shr_strs));
                list.Add(string.Format(""));
                list.Add(string.Format("Provide nominal shear reinforcements"));
                list.Add(string.Format(""));
                list.Add(string.Format("Using Bar Dia = sdia = {0} mm. and Legs = sleg = {1}", sdia, sleg));
                list.Add(string.Format(""));
                list.Add(string.Format("Minimum Spacing   = (3.1416 x sdia^2/4) x (sleg x 0.87 x sigma_s) / (0.4 x b)"));
                list.Add(string.Format("                  = (3.1416 x {0}^2/4) x ({1} x 0.87 x {2}) / (0.4 x {3} x 1000)",
                    sdia, sleg, sigma_s, b));
                list.Add(string.Format("                  = {0:f3} mm.", min_spc));
            }

           
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Reinforcements:"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Reinforcements:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter of Longitudinal Reinforcement Bars = ldia = {0} mm.", ldia));
            list.Add(string.Format("Horizontal Stirrups: {0} mm dia., {1} Legged Stirrups", hdia, hlegs));
            list.Add(string.Format("Vertical Stirrups: {0} mm dia., {1} Legged Stirrups", vdia, vlegs));
            list.Add(string.Format(""));


            double _d_dash = 0.8 * _d / 1000.0;
            list.Add(string.Format("d’ = 0.8 x d = 0.8 x {0:f3} = {1:f3} m.", (_d/1000.0), _d_dash));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear Stress = {0:f3} N/Sq.mm ", shr_strs));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _val = Vu * 1000 / (_b * 1000 * _d);
            list.Add(string.Format("   Vu x 1000 / (b x d’)"));
            list.Add(string.Format(" = {0:f3} x 1000 / ({1} x 1000 x {2:f3} x 1000)", Vu, _b, (_d/1000)));
            list.Add(string.Format(" = {0:f3} N/Sq.mm", _val));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            safe_fact = 0.15 * fck1;
            if (safe_fact >= _val)
                list.Add(string.Format("0.15 x fck = 0.15 x {0} = {1:f3} N/Sq.mm >= {2:f3} N/Sq.mm,    OK", fck1, safe_fact, _val));
            else
                list.Add(string.Format("0.15 x fck = 0.15 x {0} = {1:f3} N/Sq.mm < {2:f3} N/Sq.mm,   NOT OK", fck1, safe_fact, _val));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Avf = Vu * 1000 / (0.85 * fy1 * 1.4);
            list.Add(string.Format("Shear Friction Reinforcements =  Avf  = Vu x 1000 / (0.85 x fy x 1.4)"));
            list.Add(string.Format("                                      = {0:f3} x 1000 / (0.85 x {1} x 1.4)", Vu, fy1));
            list.Add(string.Format("                                      = {0:f3} Sq.mm", Avf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Af = ((Vu * 1000 * (B16 / 2)) + SFT * ((_d / 1000.0) - _d_dash)) / (0.85 * fy1 * _d_dash);

            list.Add(string.Format("Distance from left edge Pier Cap Edge to left face of Pier = B16 = {0} m.", B16));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Flexural tension reinforcements = Af = [(Vu x 1000 x (B16/2)) + SFT x (d-d’)]/ (0.85 x fy x d’)"));
            list.Add(string.Format("                                = [({0:f3} x 1000 x {1:f3})+ {2:f3} x ({3:f3}-{4:f3})]/(0.85 x {5:f3} x {3:f3})", Vu, (B16 / 2), SFT, (_d / 1000.0), _d_dash, fy1));
            list.Add(string.Format("                                = {0:f3} Sq.mm", Af));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            _val = ((0.04 * fck1) / fy1) * _b * _d_dash * 1000000;

            list.Add(string.Format("    ((0.04 x fck) / fy) x b x d’"));
            list.Add(string.Format("  = ((0.04 x {0}) / {1}) x {2:f3} x {3:f3} x 10^6", fck1, fy2, _b, _d_dash));
            list.Add(string.Format("  = {0:f3} Sq.mm", _val));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double lAst = (Af > _val) ? Af : _val;

            if (lAst < (2.0 * Avf / 3)) lAst = (2.0 * Avf / 3);
            list.Add(string.Format("The largest of (2/3) x Avf = {0:f3}, {1:f3} and {2:f3} = lAst = {3:f3} Sq.mm", (Avf * 2 / 3.0), Af, _val, lAst));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter of Longitudinal reinforcement Bars = ldia = {0} mm.", ldia));
            double labar = Math.PI * ldia * ldia / 4.0;
            list.Add(string.Format("Cross Section area of each Bar = labar = 3.1416 x ldia^2/4"));
            list.Add(string.Format("                                       = 3.1416 x {0}^2/4", ldia));
            list.Add(string.Format("                                       = {0:f3} Sq.mm.", labar));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            
            int lnbar = (int)(lAst / labar);
            lnbar++;
            list.Add(string.Format("Numbers of Bars = lnbar = lAst/labar = {0:f3} / {1:f3} = {2:f3} Nos. = {3}", lAst, labar, (lAst / labar), lnbar));

            double spcng = (int)((_d / lnbar)/10.0);
            spcng *= 10;

            list.Add(string.Format("Spacing = d/lnbar = {0:f3} / {1} = {2:f3} = {3} mm.", _d, lnbar, (_d / lnbar), spcng));
            list.Add(string.Format(""));

            double st_area_pro = lnbar * labar;
            list.Add(string.Format("Area of Steel Reinforcement Provided = lnbar x labar = {0} x {1:f3} = {2:f3} Sq.mm.", lnbar, labar, st_area_pro));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Stirrups:"));
            list.Add(string.Format("--------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide {0} mm dia., {1} Legged Stirrups in 3 layers, ", hdia, hlegs));

            list.Add(string.Format("Provide up to (2/3) x d = (2/3) x {0}, ie, {1:f3} mm from top,", _d , (_d * 2 / 3.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stirrups:"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide {0} mm dia., {1} Legged Stirrups @ spacing {2} mm. c/c,", vdia, vlegs, vspc));
            list.Add(string.Format(""));

            #endregion Step 7


            #region Step 8 : DESIGN OF OPEN FOUNDATION

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 8 : DESIGN OF OPEN FOUNDATION"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Data:"));
            list.Add(string.Format("------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("At Top of Footing,"));
            list.Add(string.Format(""));

            double PU = Pu_top;
            list.Add(string.Format("Total Vertical Axial Load = PU = {0:f3} kN", PU));
            list.Add(string.Format(""));

            double MX = MX1 + BML1 + SML + WML;
            list.Add(string.Format("Total Moment in Longitudinal Direction  = Mx "));
            list.Add(string.Format("                                        = Mx1 + BML1 + SML + WML"));
            list.Add(string.Format("                                        = {0:f3} + {1:f3} + {2:f3} + {3:f3}", MX1, BML1, SML, WML));
            list.Add(string.Format("                                        = {0:f3} kN-m", MX));
            list.Add(string.Format(""));
             My = MY1 + BMT1 + SMT + WMT;
            list.Add(string.Format("Total Moment in Transverse Direction  = My "));
            list.Add(string.Format("                                      = My1 + BMT1 + SMT + WMT"));
            list.Add(string.Format("                                      = {0:f3} + {1:f3} + {2:f3} + {3:f3}", MY1, BMT1, SMT, WMT));
            list.Add(string.Format("                                      = {0:f3} kN-m", My));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Concrete Grade M {0} = fck", fck1));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Grade Fe {0} = fy", fy1));
            list.Add(string.Format(""));
            list.Add(string.Format("Permissible Flexural Compressive Stress in Concretec = {0} N/Sq.mm  = sigma_c", sigma_c));
            list.Add(string.Format(""));
            list.Add(string.Format("Permissible Stress in Steel = {0} N/Sq.mm  = sigma_s", sigma_s));
            list.Add(string.Format(""));
            list.Add(string.Format("Modular Ratio = {0} = m", m));
            list.Add(string.Format(""));
            list.Add(string.Format("Neutral Axis Factor = R = {0:f3}", R));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficient of Resisting Moment = Q = {0:f3} N/Sq.m",Q));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check for Footing Size:"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double sz_pr = B9 * B10;
            list.Add(string.Format("Size of Pier = B9 x B10 = {0} x {1} = {2:f3} Sq.m", B9, B10, sz_pr));
            list.Add(string.Format(""));
            double farea = B7 * B6;
            list.Add(string.Format("Provided Area of Footing = farea = B7 x B6 = {0} x {1} = {2:f3} Sq.m", B7, B6, farea));
            list.Add(string.Format(""));
            
            _val = PU + W5;
            list.Add(string.Format("Load on Footing + Weight of Footing = Pu + W5 = {0:f3} + {1:f3} = {2:f3} kN", PU, W5, _val));
            list.Add(string.Format(""));
            list.Add(string.Format("Bearing Capacity of Soil = SBC = {0} kN/Sq.m", SBC));
            list.Add(string.Format(""));
            double req_foot_area = _val / SBC;
            if (req_foot_area <= farea)
                list.Add(string.Format("Required Area of Footing = {0:f3}/{1} = {2:f3} Sq.m <= {3:f3} (farea),    OK", _val, SBC, req_foot_area, farea));
            else
                list.Add(string.Format("Required Area of Footing = {0:f3}/{1} = {2:f3} Sq.m > {3:f3} (farea),    NOT OK", _val, SBC, req_foot_area, farea));

            list.Add(string.Format(""));

            double el = (My / PU) * 1000.0;
            list.Add(string.Format("Eccentricity = e1 = M/Pu = {0:f3} / {1:f3} = {2:f3} m. = {3:f3} mm.", My, PU, (My / PU), el));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Footing:"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));

            double Fb1 = (B6 - B9) * 1000 / 2 + el;
            list.Add(string.Format("Cantilever length on right of Pier Face = Fb1 "));
            list.Add(string.Format("                                        = (B6 – B9) x 1000/2 + e1 "));
            list.Add(string.Format("                                        = ({0} – {1}) x 1000/2 + {2:f3}", B6, B9, el));
            list.Add(string.Format("                                        = {0:f3} mm", Fb1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FM1 = (PU / (B6 * B7)) * B6 * ((Fb1 / 1000) * (Fb1 / 1000)) / 2.0;

            list.Add(string.Format("Bending Moment at the face of the Pier = FM1"));
            list.Add(string.Format("                                       = (Pu/(B6 x B7)) x B6 x (Fb1/1000)^2 / 2 "));
            list.Add(string.Format("                                       = ({0:f3}/({1} x {2})) x {1} x ({3:f3}/1000)^2 / 2 ",
                PU, B6, B7, Fb1));
            list.Add(string.Format("                                       = {0:f3} kN-m.", FM1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("The section of footing at the face of Pier is Trapezoidal, "));
            list.Add(string.Format(""));
            list.Add(string.Format("However the Moment of Resistance can be approximately determined by considering"));
            list.Add(string.Format("it as rectangle of width = Fb2"));
            list.Add(string.Format(""));
            double Fb2 = B9 + (B6 - B9) / 8.0;
            list.Add(string.Format("Fb2 = B9 + (B6-B9)/8 = [{0} + ({1} – {0})/8] = {2:f3} m.", B9, B6, Fb2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Fd1 = Math.Sqrt(FM1 * 1000000 / (Q * Fb2 * 1000));
            list.Add(string.Format("Required Effective Depth = Fd1 = Sqrt(FM1 x 10^6 / (Q x Fb2 x 1000))"));
            list.Add(string.Format("                         = Sqrt({0:f3} x 10^6 / ({1:f3} x {2:f3} x 1000))", FM1, Q, Fb2));
            list.Add(string.Format("                         = {0:f3} mm. = {1:f3} m. ", Fd1, (Fd1 = Fd1 / 1000)));
            list.Add(string.Format(""));
            eff_depth = H5 + H6;
            
            if (eff_depth <= Fd1)
                list.Add(string.Format("Provided Effective Depth = H5 + H6 = {0} + {1} = {2:f3} m. <= {3:f3} m, NOT OK.", H5, H6, eff_depth, Fd1));
            else
                list.Add(string.Format("Provided Effective Depth = H5 + H6 = {0} + {1} = {2:f3} m. > {3:f3} m, OK.", H5, H6, eff_depth, Fd1));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check for Shear:"));
            list.Add(string.Format("----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("For two way shear (Punching shear) the section is taken at a distance Fd1/2 from the face of the Pier all around."));
            list.Add(string.Format(""));

            double lo = (B10 * Fd1) * 1000.0;
            list.Add(string.Format("The sides are lo = (B10 + Fd1) x 1000 = ({0} + {1:f3}) x 1000 = {2:f3} mm.", B10, Fd1, lo));
            double bo = (B9 + Fd1) * 1000.0;
            list.Add(string.Format("          and bo = (B9 + Fd1) x 1000 = ({0} + {1:f3}) x 1000 = {2:f3} mm.", B9, Fd1, bo));
            list.Add(string.Format(""));
            double Fv = (PU / (B6 * B7)) * (B6 * B6 - (bo / 1000.0) * (bo / 1000.0));
            list.Add(string.Format("Shear Force Fv = (Pu/(B6 x B7)) x (B6^2 – bo^2) "));
            list.Add(string.Format(""));
            list.Add(string.Format("               = ({0:f3} / ({1} x {2})) x ({1}^2 – {3:f3}^2)", PU, B6, B7, (bo/1000.0)));
            list.Add(string.Format(""));
            list.Add(string.Format("               = {0:f3} kN", Fv));
            list.Add(string.Format(""));


            double ks = 0.16;
            list.Add(string.Format("Shear Stress Tv = Fv / (4 x bo x do)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Therefore, do = Fv / (4 x bo x Tv)"));
            list.Add(string.Format(""));
            double Tv = ks * Math.Sqrt(fck1);
            list.Add(string.Format("Now, Permissible Shear Stress = ks x Tc = 1 x 0.16 x sqrt(fck) = 0.16 x sqrt({0}) = {1:f3} N/Sq.mm", fck1, Tv));
            list.Add(string.Format(""));
            list.Add(string.Format("So, Limiting Tv to this permissible Shear Stress, "));
            list.Add(string.Format(""));
            double _do = Fv / (4 * bo * Tv);
            list.Add(string.Format("do = {0:f3} x 1000 / (4 x {1:f3} x {2:f3}) = {3:f3} mm.", Fv, bo, Tv, _do));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double bdia = ldia;
            
            list.Add(string.Format("Reinforcement Bar Diameter = bdia = {0} mm.", bdia));
            list.Add(string.Format(""));
            list.Add(string.Format("Reinforcement cover = cover = {0} mm.", cover));
            list.Add(string.Format(""));

            double Ho = H4*1000 - cover - (bdia / 2);
            double Hp = H4*1000 + H3*1000 - cover - (bdia / 2);
            list.Add(string.Format("If an Effective Depth = Ho = H4-cover-(bdia/2) = {0}-{1}-{2:f3} = {3:f3} mm is provided at the outer edge,\n\r\n\r and an Effective Depth Hp = H4+H3-cover-(bdia/2) = {0}+{4}-{1}-{2}={5:f3} mm at the face of the Pier, ",
                (H4 * 1000.0), cover, (bdia / 2.0), Ho, (H3 * 1000.0), Hp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Therefore the available effective depth at Fd1/2 from the face of the Pier = FD3 "));
            list.Add(string.Format(""));
            double FD3 = Ho + (Hp - Ho) * ((B6 * 1000 / 2) - (B9 * 1000 / 2) - (Ho / 2)) / ((B6 * 1000 / 2) - (B9 * 1000 / 2));

            list.Add(string.Format("FD3 = "));
            list.Add(string.Format(""));
            list.Add(string.Format("    = Ho + (Hp – Ho) x ((B6x1000/2) – (B9x1000/2) – (Ho/2)) / ((B6x1000/2) – (B9x1000/2))"));
            list.Add(string.Format(""));
            list.Add(string.Format("    = {0:f3} + ({1:f3} – {0:f3}) x (({2}/2) – ({3}/2) – ({0:f3}/2)) / (({2}/2) – ({3}/2))",
                Ho, Hp, (B6 * 1000), (B9 * 1000)));
            list.Add(string.Format(""));
            if (FD3 >= _do)
                list.Add(string.Format("    = {0:f3} mm, This is more than do={1:f3} mm. So the Footing is OK in punching shear.", FD3, _do));
            else
                list.Add(string.Format("    = {0:f3} mm, This is less than do={1:f3} mm. So the Footing is NOT OK in punching shear.", FD3, _do));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Reinforcement:"));
            list.Add(string.Format("--------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double Ast = (FM1 * 1000000) / (sigma_s * j * _d);
            list.Add(string.Format("Ast = FM1 x 10^6 / (sigma_s x j x d)"));
            list.Add(string.Format(""));
            list.Add(string.Format("    = {0} x 10^6 / ({1} x {2:f3} x {3:f3})", FM1, sigma_s, j, _d));
            list.Add(string.Format(""));
            list.Add(string.Format("                = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            double ast = (Math.PI * bdia * bdia / 4.0);
            list.Add(string.Format("Area of {0} mm diameter bar = ast = 3.1416 x {0} x {0} / 4 = {1:f3} Sq.mm",bdia, ast));
            list.Add(string.Format(""));

            int bnos = (int)(Ast / ast);
            bnos++;
            list.Add(string.Format("Number of bars required = bnos = Ast/ast = {0:f3} / {1:f3} = {2:f3} = {3} nos.",
                Ast, ast, (Ast / ast), bnos));
            list.Add(string.Format(""));
            spcng = B6 * 1000 / bnos;
            list.Add(string.Format("Required spacing = B6 x 1000 / bnos = {0} x 1000 / {1} = {2:f0} mm.", B6, bnos, spcng));

            spcng = (spcng * 2) / 10;
            spcng *= 10;
            list.Add(string.Format("Provide {0} mm Dia Bars at spacing {1:f0} mm c/c in two layers, both ways.",bdia, spcng));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Development length:"));
            list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));
            double Ld = 45 * bdia;
            list.Add(string.Format("Required Development Length = Ld = 45 x bdia = 45 x {0} = {1:f2} mm.", bdia, Ld));
            list.Add(string.Format(""));
            eff_depth = (B6 * 1000 / 2) - 2 * bdia - 60.0;
            list.Add(string.Format("Using 60 mm side cover available length  = (B6x1000/2) – 2 x bdia – 60 "));
            list.Add(string.Format(""));
            list.Add(string.Format("                                         = ({0} x 1000 / 2) – 2x32 – 60", B6));
            list.Add("");

            if (eff_depth > Ld)
                list.Add(string.Format("                                         = {0:f0} mm  > {1:f0}, So OK.", eff_depth, Ld));
            else
                list.Add(string.Format("                                         = {0:f0} mm  < {1:f0}, So NOT OK.", eff_depth, Ld));

            list.Add(string.Format(""));

            #endregion Step 8

            list.Add("");
            list.Add(string.Format("TABLE 1 : Allowable Average Shear Stress in Stiffened Webs of Steel"));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add("");
            list.Add("");
            list.AddRange(iapp.Tables.Get_Tables_Allowable_Average_Shear_Stress());
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");

            if (!Directory.Exists(Path.GetDirectoryName(rep_file_name)))
                Directory.CreateDirectory(Path.GetDirectoryName(rep_file_name));

            File.WriteAllLines(rep_file_name, list.ToArray());
            list.Clear();
            list = null;
        }

        void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                //sw.WriteLine("w1 = {0:f3}", w1);
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
            string f_name = Path.Combine(system_path, "PIER_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(f_name, FileMode.Create));
            try
            {
                //sw.WriteLine("_G={0}", _G);
            }
            catch (Exception ex) { }
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
                //this.Text = "DESIGN OF RCC PierS : " + value;
                user_path = value;

                //file_path = Path.Combine(user_path, "Working Stress Design");
                file_path = user_path;


                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                file_path = Path.Combine(file_path, "Design of RCC Piers");
                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                //system_path = Path.Combine(file_path, "AstraSys");
                //if (!Directory.Exists(system_path))
                //    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_Pier.TXT");
                user_input_file = Path.Combine(system_path, "RCC_PIERS.FIL");

                //btnProcess.Enabled = Directory.Exists(value);
                //btnReport.Enabled = File.Exists(user_input_file);
                //btnDrawing.Enabled = File.Exists(user_input_file);

                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Read_From_File();
                //}


            }
        }
        public void Read_From_File()
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

                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    //switch (VarName)
                    //{
                    //    //case "w1":
                    //    //    w1 = mList.GetDouble(1);
                    //    //    txt_w1.Text = w1.ToString();
                    //    //    break;


                    //}
                    #endregion
                    #region USER INPUT DATA

                    //sw.WriteLine(" = {0:f3} ", w1);
                    //sw.WriteLine(" = {0:f3} ", w2);
                    //sw.WriteLine(" = {0:f3} ", e);
                    //sw.WriteLine(" = {0:f3} ", b1);
                    //sw.WriteLine(" = {0:f3} ", b2);
                    //sw.WriteLine(" = {0:f3} ", l);
                    //sw.WriteLine(" = {0:f3} ", h);
                    //sw.WriteLine(" = {0:f3} ", );
                    //sw.WriteLine(" = {0:f3} ", w3);
                    //sw.WriteLine(" = {0:f3} ", gamma_c);
                    //sw.WriteLine(" = {0:f3} ", f1);
                    //sw.WriteLine(" = {0:f3} ", f2);
                    //sw.WriteLine(" = {0:f3} ", A);
                    //sw.WriteLine(" = {0:f3} ", F);
                    //sw.WriteLine(" = {0:f3} ", V);

                    #endregion
                }


            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }

    }

    /**/
    public  class StoneMasonryPiers
    {
        public string rep_file_name = "";
        public string file_path = "";
        public string user_path = "";
        public string system_path = "";
        public string drawing_path = "";
        public string user_input_file = "";
        public bool is_process = false;


        #region Variable Declarion
        public double w1, w2, e, b1, b2, l, h, HFL, w3, gamma_c;
        public double f1, f2, A, F, V;

        List<double> lst_s = new List<double>();

        #endregion
        IApplication iApp = null;
        public StoneMasonryPiers(IApplication app)
        {
        
            this.iApp = app;
        }
        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF BRIDGE PierS : " + value;
                user_path = value;
                file_path = user_path;
                //file_path = Path.Combine(user_path, "Design of Stone Massonary Piers");
                //if (!Directory.Exists(file_path))
                //    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Stone_Masonry_Pier.TXT");
                user_input_file = Path.Combine(system_path, "BRIDGE_PIERS.FIL");
                drawing_path = Path.Combine(system_path, "PIER_DRAWING.FIL");
            }
        }

        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("w1 = {0:f3}", w1);
                sw.WriteLine("w2 = {0:f3}", w2);
                sw.WriteLine("e = {0:f3} ", e);
                sw.WriteLine("b1 = {0:f3} ", b1);
                sw.WriteLine("b2 = {0:f3} ", b2);
                sw.WriteLine("l = {0:f3}  ", l);
                sw.WriteLine("h = {0:f3} ", h);
                sw.WriteLine("HFL = {0:f3} ", HFL);
                sw.WriteLine("w3 = {0:f3} ", w3);
                sw.WriteLine("γ_c = {0:f3} ", gamma_c);
                sw.WriteLine("f1 = {0:f3} ", f1);
                sw.WriteLine("f2 = {0:f3} ", f2);
                sw.WriteLine("A = {0:f3} ", A);
                sw.WriteLine("F = {0:f3} ", F);
                sw.WriteLine("V = {0:f3} ", V);
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
            drawing_path = Path.Combine(system_path, "PIER_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                string _A = (b1 * 1000.0).ToString();
                string _B = (HFL * 1000).ToString();
                string _C = (b2 * 1000.0).ToString();
                string _D = (e * 1000.0).ToString();
                string _E = (h * 1000.0).ToString();
                string _F = (l * 1000.0).ToString();
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                //sw.WriteLine("_G={0}", _G);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Calculate_Program(string fileName)
        {
            #region Write File
            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));

            #region USER INPUT DATA

            //sw.WriteLine("USER INPUT DATA");
            //sw.WriteLine("------------------------------------------------------------");

            //sw.WriteLine("w1 = {0:f3} ",w1);
            //sw.WriteLine("w2 = {0:f3} ",w2);
            //sw.WriteLine("e = {0:f3} ",e); 
            //sw.WriteLine("b1 = {0:f3} ",b1);
            //sw.WriteLine("b2 = {0:f3} ",b2);
            //sw.WriteLine("l = {0:f3} ",l); 
            //sw.WriteLine("h = {0:f3} ",h); 
            //sw.WriteLine("HFL = {0:f3} ",HFL);
            //sw.WriteLine("w3 = {0:f3} ",w3); 
            //sw.WriteLine("gamma_c = {0:f3} ",gamma_c); 
            //sw.WriteLine("f1 = {0:f3} ",f1); 
            //sw.WriteLine("f2 = {0:f3} ",f2); 
            //sw.WriteLine("A = {0:f3} ",A); 
            //sw.WriteLine("F = {0:f3} ",F); 
            //sw.WriteLine("V = {0:f3} ",V);
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine();

            #endregion


            #region TechSOFT Banner
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*                  ASTRA Pro                  *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*          DESIGN  OF  BRIDGE PIERS           *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            try
            {
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Permanent Load from each Span [w1] = {0:f3} kN", w1);
                sw.WriteLine("Live Load from each Span [w2] = {0:f3} kN", w2);
                sw.WriteLine("Vehicle Load on each Span [w3] = {0:f3} kN", w3);
                sw.WriteLine("Acts at distance from Centre Line Pier [e] = {0:f3} m    Marked as D in the Drawing", e);
                sw.WriteLine("Width of Pier at Bottom [b1] = {0:f3} m                  Marked as A in the Drawing", b1);
                sw.WriteLine("Width of Pier at Top [b2] = {0:f3} m                     Marked as C in the Drawing", b2);
                sw.WriteLine("Length of Pier [l] = {0:f3} m                            Marked as F in the Drawing", l);
                sw.WriteLine("Height of Pier [h] = {0:f3} m                            Marked as E in the Drawing", h);
                sw.WriteLine("Height of high flood Level [HFL] = {0:f3} m              Marked as B in the Drawing", HFL);
                sw.WriteLine("Unit weight of concrete  [γ_c] = {0:f3} kN/cu.m", gamma_c);
                sw.WriteLine("Frictional Coefficient of Left Side Bending [f1] = {0:f3} ", f1);
                sw.WriteLine("Frictional Coefficient of Right Side Bending [f2] = {0:f3} ", f2);
                sw.WriteLine("Area of Deck and Handrail in elevation [A] = {0:f3} sq.m", A);
                sw.WriteLine("Wind Force [F] = {0:f3} kN/sq.m", F);
                sw.WriteLine("Mean water current Velocity [V] = {0:f3} m/sec", V);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");



                #region STEP 1


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF BRIDGE PIERS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Stresses due to Permanent Loads and Self weight.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permanent Load from Super Structure = 2 * w1 = 2 * {0:f2} = {1:f2} kN",
                    w1,
                    (2 * w1));

                double sefl_wt_pier = l * ((b1 + b2) / 2.0) * h * gamma_c;

                sw.WriteLine("Self weight of Pier = l * ((b1 + b2) / 2.0) * h * γ_c");
                sw.WriteLine("                    = {0:f2} * (({1:f2} + {2:f2}) / 2.0) * {3:f2} * {4:f2}",
                    l, b1, b2, h, gamma_c);
                sw.WriteLine("                    = {0:f3} kN", sefl_wt_pier);

                //w3 = 2 * w1 + sefl_wt_pier;
                double _w3 = 2 * w1 + sefl_wt_pier;

                sw.WriteLine("  Total Direct Load = w3 =  {0:f2} + {1:f2} = {2:f3} kN",
                    (2 * w1), sefl_wt_pier, _w3);

                double stress_base_pier = _w3 / (b1 * l);
                sw.WriteLine("Stress at the base of Pier = w3 / (b1 * l) = {0:f2} / ({1:f2} * {2:f2})",
                    _w3, b1, l);
                sw.WriteLine("                           = {0:f3}", stress_base_pier);

                lst_s.Add(stress_base_pier);

                #endregion

                #region STEP 2
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Effect of Buoyancy");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double b3 = (((b1 - b2) / (2 * h)) * (h - HFL)) * 2 + b2;
                sw.WriteLine("Width of HFL = b3 = (((b1 - b2) / (2 * h)) * (h - HFL)) * 2 + b2");
                sw.WriteLine("                  = ((({0:f2} - {1:f2}) / (2 * {2:f2})) * ({2:f2} - {3:f3})) * 2 + {4:f2}",
                    b1, b2, h, HFL, b2);
                sw.WriteLine("                  = {0:f3} m", b3);
                sw.WriteLine();

                double submerged_vol = l * ((b3 + b1) / 2) * HFL;
                sw.WriteLine("Submerged volume of Pier = l * ((b3 + b1) / 2) * HFL");
                sw.WriteLine("                         = {0:f2} * (({1:f2} + {2:f2}) / 2) * {3:f2}",
                    l,
                    b3,
                    b1,
                    HFL);
                sw.WriteLine("                         = {0:f2} cu.m", submerged_vol);

                sw.WriteLine();
                double reduction_wt = submerged_vol * 10.0;
                sw.WriteLine("Reduction in Weight of Pier for buoyancy = {0:f2} * 10 = {1:f2} kN", submerged_vol,
                    reduction_wt);

                double stress_base_pier_buoyancy = -(reduction_wt / (l * b1));
                sw.WriteLine("Stress at base due to buoyancy = -{0:f2}/l*b1 ", reduction_wt);
                sw.WriteLine("                               = -{0:f2}/{1:f2}*{2:f2}", reduction_wt, l, b1);
                sw.WriteLine("                               = {0:f2} kN/sq.m ", stress_base_pier_buoyancy);

                lst_s.Add(stress_base_pier_buoyancy);
                sw.WriteLine();

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Stress due to eccentricity of Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double Ml = w2 * e;
                sw.WriteLine("Bending Moment due to eccentricity of Live Load = Ml = w2 * e");
                sw.WriteLine("                                                = {0:f2} * {1:f2}", w2, e);
                sw.WriteLine("                                                = {0:f2} kN-m", Ml);
                sw.WriteLine();

                double Zt = l * b1 * b1 / 6;

                sw.WriteLine(" Section Modulus of Pier at Base about transverse axis");
                sw.WriteLine("  (Axis at right angle to the direction of Traffic)");
                sw.WriteLine("    = Zt = l * b1 * b1 / 6");
                sw.WriteLine("    = {0:f2} * {1:f2} * {1:f2} / 6", l, b1);
                sw.WriteLine("    = {0:f2} cu.m.", Zt);
                sw.WriteLine();

                sw.WriteLine("Stress at base for eccentric Live Load = (w2/(l*b1) ± (Ml/Zt)");


                double val1, val2, val3, val4;
                val1 = w2 / (l * b1);
                val2 = Ml / Zt;

                val3 = val1 + val2;
                val4 = val1 - val2;

                sw.WriteLine("                                       = ({0:f2}/({1:f2}*{2:f2}) ± ({3:f2}/{4:f2})",
                    w2,
                    l, b1, Ml, Zt);
                sw.WriteLine("                                       = {0:f2} ± {1:f2}", val1, val2);
                sw.WriteLine("                                       = {0:f2} or {1:f2}", val3, val4);
                lst_s.Add(val3);




                #endregion


                #region STEP 4 : Stress due to Longitudinal Forces

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Stress due to Longitudinal Forces");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine(" i) Stresses due to braking Force ");
                sw.WriteLine("    Vehicle Load = w3 = {0:f2} kN", w3);
                double w4 = 0.2 * w3;
                sw.WriteLine();
                sw.WriteLine("    Horizontal Force for Braking = w4 = 0.2 * w3 = 0.2 * {0:f2} = {1:f2} kN",
                    w3,
                    w4);

                double M = w4 * h;

                sw.WriteLine();
                sw.WriteLine("    Bending Moment at base of Pier = M = w4 * h = {0:f2} * {1:f2}", w4, h);
                sw.WriteLine("                                   = {0:f2} kN-m", M);
                sw.WriteLine();

                double stress_base_braking_force = (M / Zt);
                sw.WriteLine("Stress at base due to braking force = ±(M/Zt) = ±({0:f2}/{1:f2}) ", M, Zt);
                sw.WriteLine("                                    = ±{0:f2} kN/sq.m ", stress_base_braking_force);

                lst_s.Add(stress_base_braking_force);


                sw.WriteLine();
                sw.WriteLine(" ii) Stresses due to resistance in bearings against movement due to temperature.");
                sw.WriteLine();
                sw.WriteLine("Applied Permanent Load on Bearing = w1 = {0:f2} kN", w1);
                sw.WriteLine("Applied Live Load on Bearing  = w2 = {0:f2} kN", w2);
                sw.WriteLine("For wrost effect let us assume Live Load on Left Span only");
                sw.WriteLine();

                double left_side_force = f1 * (w1 + w2);
                sw.WriteLine("Force of resistance by left side bearing = f1*(w1+w2)");
                sw.WriteLine("                                         = {0:f2}*({1:f2}+{2:f2})", f1, w1, w2);
                sw.WriteLine("                                         = {0:f2} kN", left_side_force);
                sw.WriteLine();

                double right_side_force = f2 * w1;
                sw.WriteLine("Force of resistance by right side bearing = f2*w1");
                sw.WriteLine("                                         = {0:f2}*{1:f2}", f2, w1);
                sw.WriteLine("                                         = {0:f2} kN", right_side_force);
                sw.WriteLine();

                val1 = Math.Max(left_side_force, right_side_force);
                val2 = Math.Min(left_side_force, right_side_force);
                double unbalanced_force_bearing = (val1 - val2);
                sw.WriteLine("Unbalanced force at bearing = {0:f2} - {1:f2} = {2:f2} kN",
                    val1, val2, unbalanced_force_bearing);
                M = unbalanced_force_bearing * h;
                sw.WriteLine();
                sw.WriteLine("Bending Momnet at base = M = {0:f2} * h = {0:f2} * {1:f2} = {2:f2} kN/sq.m",
                    unbalanced_force_bearing,
                    h,
                    M);
                double stress_at_base = (M / Zt);
                sw.WriteLine();
                sw.WriteLine("Stresses at base = ±(M/Zt) = ±({0:f2}/{1:f2}) = ±{2:f2} kN/sq.m",
                    M,
                    Zt,
                    stress_at_base);
                lst_s.Add(stress_at_base);
                #endregion

                #region STEP 5
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Stresses due to wind Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double wind_force = A * F;
                sw.WriteLine("Wind force on Exposed Surface at bearing level = A * F");
                sw.WriteLine("                                               = {0:f2} * {1:f2}", A, F);
                sw.WriteLine("                                               = {0:f2} kN", wind_force);
                sw.WriteLine();
                sw.WriteLine("Bending Moment at base = {0:f2} * h = {0:f2} * {1:f2}", wind_force, h);
                M = wind_force * h;
                sw.WriteLine("                       = {0:f2} kN-m", (wind_force * h));

                sw.WriteLine();
                sw.WriteLine("Section Modulus of Pier at base about Longitudinal axis");
                sw.WriteLine("       (Axis parallel to direction of trafic)");

                double Zl = (b1 * l * l) / 6;
                sw.WriteLine("       Zl = (b1 * l * l) / 6 = ({0:f2} * {1:f2} * {1:f2})/6",
                    b1,
                    l);
                sw.WriteLine("          = {0:f2} cu.m.", Zl);

                sw.WriteLine();
                stress_at_base = M / Zl;
                sw.WriteLine("     Stress at base = ±({0:f2}/{1:f2}) = ±{2:f2} kN/sq.m",
                    M,
                    Zl, stress_at_base);
                lst_s.Add(stress_at_base);

                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Stresses due to Water Current");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double k = 0.66;
                sw.WriteLine("k = {0:f2}", k);

                double p1 = 0.5 * k * V * V;
                sw.WriteLine("Intensity of Pressure = 0.5*k*V*V");
                sw.WriteLine("                      = 0.5*{0:f2}*{1:f2}*{1:f2}", k, V);
                sw.WriteLine("                      = {0:f2} kN/sq.m", p1);

                double water_curr_force = ((b1 + b2) / 2) * h * p1;
                sw.WriteLine();
                sw.WriteLine("Force by water current = ((b1 + b2) / 2) * h * p1");
                sw.WriteLine("                       = (({0:f2} + {1:f2}) / 2) * {2:f2} * {3:f2}",
                    b1, b2, h, p1);
                sw.WriteLine("                       = {0:f2} kN", water_curr_force);

                double h1 = (2.0 / 3.0) * HFL;
                sw.WriteLine();
                sw.WriteLine("Force acts at (2/3)*HFL = (2/3) * {0:f2} = {1:f2} m above base",
                    HFL, h1);

                M = water_curr_force * h1;

                sw.WriteLine();
                sw.WriteLine("Bending Moment at base = {0:f2} * {1:f2} = {2:f2} kN-m",
                    water_curr_force, h1, M);
                double p2 = M / Zl;
                sw.WriteLine();
                sw.WriteLine("Stress at base = p2 = ±({0:f2}/Zl) = ±({0:f2}/{1:f2}) = ±{0:f2} kN/sq.m",
                    M,
                    Zl,
                    p2);

                sw.WriteLine();
                sw.WriteLine("Considering current direction varies by 20 degrees");
                double p3 = p1 * Math.Cos(20 * (Math.PI / 180));
                sw.WriteLine();
                sw.WriteLine("Pressure parallel to Pier = p3 = p1 * Cos 20");
                sw.WriteLine("                          = {0:f2} * {1:f2} = {2:f3}",
                    p1,
                    Math.Cos(20 * (Math.PI / 180)),
                    p3);

                double p4 = p1 * Math.Sin(20 * (Math.PI / 180));
                sw.WriteLine();
                sw.WriteLine("Pressure perpendicular to Pier = p3 = p1 * Sin 20");
                sw.WriteLine("                          = {0:f2} * {1:f2} = {2:f3}",
                    p1,
                    Math.Sin(20 * (Math.PI / 180)),
                    p4);

                double p5 = p2 * p3 / p1;
                sw.WriteLine();
                sw.WriteLine("Stress at base by component parallel to Pier = p5 = ±(p2 * p3/p1)");
                sw.WriteLine("                                             = ±({0:f2} * {1:f2}/{2:f2})",
                    p2, p3, p1);
                sw.WriteLine("                                             = ± {0:f2} kN/sq.m. ", p5);

                sw.WriteLine();
                double p6 = p4 * l * HFL;
                sw.WriteLine("For perpendicular to Pier = p6 = p4 * l * HFL");
                sw.WriteLine("                          = {0:f2} * {1:f2} * {2:f2}", p4, l, HFL);
                sw.WriteLine("                          = {0:f2} kN", p6);

                M = p6 * h1;
                sw.WriteLine();
                sw.WriteLine("Bending Moment at base = p6 * h1");
                sw.WriteLine("                       = {0:f2} * {1:f2}", p6, h1);
                sw.WriteLine("                       = {0:f2} kN-m", M);

                sw.WriteLine();
                sw.WriteLine("Stress at base due to Component perpendicular to pier ");

                double p7 = M / Zt;
                sw.WriteLine("   p7 = ± {0:f2}/Zt =  ± {0:f2}/{1:f2} = {2:f2} kN/sq.m  ",
                    M,
                    Zt, p7);

                double max_stress_water_curr = 0.0;

                sw.WriteLine();
                if (p7 > p5)
                {
                    max_stress_water_curr = p2 + p7;
                    sw.WriteLine("Maximum stress by water current = ±(p2+p7)");
                    sw.WriteLine("                                = ±({0:f2}+{1:f2})", p2, p7);
                    sw.WriteLine("                                = ±{0:f2} kN/sq.m", max_stress_water_curr);

                }
                else
                {
                    max_stress_water_curr = p2 + p5;
                    sw.WriteLine("Maximum stress by water current = ±(p2+p5)");
                    sw.WriteLine("                                = ±({0:f2}+{1:f2})", p2, p5);
                    sw.WriteLine("                                = ±{0:f2} kN/sq.m", max_stress_water_curr);
                }

                lst_s.Add(max_stress_water_curr);
                #endregion

                #region STEP 7
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : SUMMARY OF STRESSES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Stresses should be considered for Dry river and flooded river cases.");
                sw.WriteLine("The stresses are presented below, the extreme comprehensive and tesile");
                sw.WriteLine("stresses are well within permissible limits.");
                sw.WriteLine();
                //sw.WriteLine("-------------------------------------------------------------------------");
                sw.WriteLine("_________________________________________________________________________");

                string format = "{0,-5} {1,-31}{2,18}{3,18}";
                sw.WriteLine(format,
                    "SL.NO",
                    "LOADS",
                    "Stresses(kN/sq.m)",
                    "Stresses");

                sw.WriteLine(format,
                                    "",
                                    "",
                                    "(Dry River)",
                                    "(Flood River)");
                sw.WriteLine("-------------------------------------------------------------------------");


                sw.WriteLine(format,
                                                    "1.",
                                                    "Permanent Load and Self Weight",
                                                    "+" + lst_s[0].ToString("0.00"),
                                                    "+" + lst_s[0].ToString("0.00"));


                sw.WriteLine(format,
                                                    "2.",
                                                    "Buoyancy",
                                                    "0",
                                                    lst_s[1].ToString("0.00"));


                sw.WriteLine(format,
                                                    "3.",
                                                    "Eccentric Live Load",
                                                    "+" + lst_s[2].ToString("0.00"),
                                                    "+" + lst_s[2].ToString("0.00"));


                sw.WriteLine(format,
                                                    "4a.",
                                                    "Tractive Forces",
                                                    "+" + lst_s[3].ToString("0.00"),
                                                    "±" + lst_s[3].ToString("0.00"));


                sw.WriteLine(format,
                                                    "4b.",
                                                    "Bearing Resistance",
                                                    "±" + lst_s[4].ToString("0.00"),
                                                    "+" + lst_s[4].ToString("0.00"));


                sw.WriteLine(format,
                                                    "5.",
                                                    "Wind Load",
                                                    "±" + lst_s[5].ToString("0.00"),
                                                    "±" + lst_s[5].ToString("0.00"));


                sw.WriteLine(format,
                                                    "6.",
                                                    "Water Current",
                                                    "0",
                                                    "±" + lst_s[6].ToString("0.00"));

                double sum_flood = lst_s[0] + lst_s[1] +
                    lst_s[2] +
                    lst_s[3] +
                    lst_s[4] +
                    lst_s[5] +
                    lst_s[6];
                double sum_dry = lst_s[0] +
                    lst_s[2] +
                    lst_s[3] +
                    lst_s[4] +
                    lst_s[5];


                sw.WriteLine("_________________________________________________________________________");
                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "",
                                                    "SUM",
                                                    "+" + sum_dry.ToString("0.00"),
                                                    "+" + sum_flood.ToString("0.00"));

                sw.WriteLine("-------------------------------------------------------------------------");
                #endregion

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
                GC.Collect();
                sw.Flush();
                sw.Close();
            }
            #endregion
        }
    }
    /**/
    public enum ePierOption
    {
        None = -1,
        Analysis = 0,
        RCCPier_1 = 6,
        RCCPier_2 = 7,
        MovingLoad = 8,
        StoneMasonry = 9,
    }

}
