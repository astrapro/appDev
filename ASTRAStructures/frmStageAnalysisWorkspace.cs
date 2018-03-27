using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;


using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;

////using AstraInterface.Interface;
////using AstraInterface.DataStructure;
using AstraAccess.SAP_Classes;
using BridgeAnalysisDesign;
 



namespace ASTRAStructures
{
    public partial class frmStageAnalysisWorkspace : Form
    {
        IApplication iApp;
        const string Title = "PROCESS STAGE (P DELTA) ANALYSIS";

        List<BridgeMemberAnalysis> All_Analysis { get; set; }
        public frmStageAnalysisWorkspace(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            file_path = iApp.WorkingFile;

            All_Analysis = new List<BridgeMemberAnalysis>();
            
        }
        public frmStageAnalysisWorkspace(IApplication app, string file_name)
        {
            InitializeComponent();
            iApp = app;
            file_path = file_name;

            All_Analysis = new List<BridgeMemberAnalysis>();

        }
        public string user_path { get; set; }
        //{
        //    get
        //    {
        //        if (file_path == "") return "";
        //        string pf = Path.Combine(Path.GetDirectoryName(file_path), Title);

        //        if (Directory.Exists(pf) == false)
        //            Directory.CreateDirectory(pf);

        //        return pf;
        //    }
        //}

        public string file_path { get; set; }
        private void Button_Enable_Disable()
        {

            btn_browse_input_file.Enabled = Directory.Exists(user_path);
            btn_st_1_process.Enabled = rtb_st_1_input.Lines.Length > 0;
            btn_st_1_view_structure.Enabled = rtb_st_1_input.Lines.Length > 0;
            //btn_st_1_view_report.Enabled = btn_st_1_view_structure.Enabled;
            btn_st_1_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Get_Stage_File(1)));

            btn_st_1_view_inputs.Enabled = btn_st_1_view_structure.Enabled;

            btn_st_2_apply.Enabled = cmb_st_2_loadcase.SelectedIndex > -1;
            btn_st_3_apply.Enabled = cmb_st_3_loadcase.SelectedIndex > -1;
            btn_st_4_apply.Enabled = cmb_st_4_loadcase.SelectedIndex > -1;
            btn_st_5_apply.Enabled = cmb_st_5_loadcase.SelectedIndex > -1;

            #region Stage 2
            if (cmb_st_2_loadcase.SelectedIndex > -1)
            {
                btn_st_2_process.Enabled = rtb_st_2_input.Lines.Length > 0;
                btn_st_2_view_structure.Enabled = btn_st_2_process.Enabled;

                btn_st_2_jnt_add.Enabled = btn_st_2_process.Enabled;
                btn_st_2_add_mem.Enabled = btn_st_2_process.Enabled;

                btn_st_2_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Get_Stage_File(2)));

                btn_st_2_view_inputs.Enabled = btn_st_2_view_structure.Enabled;

            }
            else
            {
                btn_st_2_process.Enabled = false;

                btn_st_2_jnt_add.Enabled = false;
                btn_st_2_add_mem.Enabled = false;

                btn_st_2_view_structure.Enabled = false;
                btn_st_2_view_report.Enabled = false;
                btn_st_2_view_inputs.Enabled = btn_st_2_view_structure.Enabled;
            }
            #endregion Stage 2



            #region Stage 3
            if (cmb_st_3_loadcase.SelectedIndex > -1)
            {
                btn_st_3_process.Enabled = rtb_st_3_input.Lines.Length > 0;
                btn_st_3_view_structure.Enabled = btn_st_3_process.Enabled;
                btn_st_3_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Get_Stage_File(3)));


                btn_st_3_jnt_add.Enabled = btn_st_3_process.Enabled;
                btn_st_3_add_mem.Enabled = btn_st_3_process.Enabled;
            }
            else
            {
                btn_st_3_process.Enabled = false;
                btn_st_3_view_structure.Enabled = false;
                btn_st_3_view_report.Enabled = false;

                btn_st_3_jnt_add.Enabled = false;
                btn_st_3_add_mem.Enabled = false;
            }
            btn_st_3_view_inputs.Enabled = btn_st_3_view_structure.Enabled;
            #endregion Stage 3


            #region Stage 4
            if (cmb_st_4_loadcase.SelectedIndex > -1)
            {
                btn_st_4_process.Enabled = rtb_st_4_input.Lines.Length > 0;
                btn_st_4_view_structure.Enabled = btn_st_4_process.Enabled;
                btn_st_4_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Get_Stage_File(4)));

                btn_st_4_jnt_add.Enabled = btn_st_4_process.Enabled;
                btn_st_4_add_mem.Enabled = btn_st_4_process.Enabled;

            }
            else
            {
                btn_st_4_process.Enabled = false;
                btn_st_4_view_structure.Enabled = false;
                btn_st_4_view_report.Enabled = false;


                btn_st_4_jnt_add.Enabled = false;
                btn_st_4_add_mem.Enabled = false;

            }
            #endregion Stage 4
            btn_st_4_view_inputs.Enabled = btn_st_4_view_structure.Enabled;

            #region Stage 5
            if (cmb_st_5_loadcase.SelectedIndex > -1)
            {
                btn_st_5_process.Enabled = rtb_st_5_input.Lines.Length > 0;
                btn_st_5_view_structure.Enabled = btn_st_5_process.Enabled;
                btn_st_5_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Get_Stage_File(5)));



                btn_st_5_jnt_add.Enabled = btn_st_5_process.Enabled;
                btn_st_5_add_mem.Enabled = btn_st_5_process.Enabled;


            }
            else
            {
                btn_st_5_process.Enabled = false;
                btn_st_5_view_structure.Enabled = false;
                btn_st_5_view_report.Enabled = false;



                btn_st_5_jnt_add.Enabled = false;
                btn_st_5_add_mem.Enabled = false;


            }
            #endregion Stage 4
            btn_st_5_view_inputs.Enabled = btn_st_5_view_structure.Enabled;

            //Write_Analysis_Result();
        }

        private void frmStageAnalysis_Load(object sender, EventArgs e)
        {

            if(Tag != "" && Tag != null)
            {
                file_path = Tag.ToString();
            }

            cmb_stage.SelectedIndex = 0;
            //if (File.Exists(file_path))
            //{
            //    rtb_st_1_input.Lines = File.ReadAllLines(file_path);
            //}


            Set_Project_Name();
            Button_Enable_Disable();
            //iApp.Read_Form_Record(this, user_path);
        }

        public string Get_Stage_File(int stage_no)
        {
            string fname = "";


            if (!Directory.Exists(user_path)) return fname;
            fname = Path.Combine(user_path, "STAGE " + stage_no + " ANALYSIS");

            if (!Directory.Exists(fname))
                Directory.CreateDirectory(fname);

            string lcase = "";
            if (stage_no == 2) lcase = cmb_st_2_loadcase.Text;
            if (stage_no == 3) lcase = cmb_st_3_loadcase.Text;
            if (stage_no == 4) lcase = cmb_st_4_loadcase.Text;
            if (stage_no == 5) lcase = cmb_st_5_loadcase.Text;

            if (lcase != "")
            {
                fname = Path.Combine(fname, "LOADCASE " + lcase);
                if (!Directory.Exists(fname))
                    Directory.CreateDirectory(fname);
            }
            fname = Path.Combine(fname, "INPUT_DATA_STAGE_" + stage_no + "_ANALYSIS.TXT");

            return fname;
        }

        public bool Check_Demo(string flPath)
        {
            if (iApp.IsDemo)
            {
                CBridgeStructure c = new CBridgeStructure(flPath);

                c.SetASTRADocFromTXT();
                if (!iApp.Check_Coordinate(c.Joints.Count, c.Members.Count))
                {
                    iApp.Check_Demo_Version();
                    return false;
                }
            }
            return false;
        }
        public bool Run_Data(string flPath)
        {
            if (Check_Demo(flPath)) return false;

            iApp.Delete_Temporary_Files(flPath);
            try
            {

                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                System.Diagnostics.Process prs = new System.Diagnostics.Process();

                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast005.exe");
                if (prs.Start())
                    prs.WaitForExit();

                //string ana_rep = Path.Combine(Path.GetDirectoryName(flPath), "ANALYSIS_REP.TXT");
                //string sap_rep = Path.Combine(Path.GetDirectoryName(flPath), "RES001.tmp");

                //if (File.Exists(sap_rep))
                //{
                //    File.Copy(sap_rep, ana_rep, true);
                //    File.Delete(sap_rep);
                //}
            }
            catch (Exception exx) { }
            return File.Exists(MyList.Get_Analysis_Report_File(flPath));
        }

        public bool Run_Data2(string flPath)
        {
            if (Check_Demo(flPath)) return false;

            //iApp.Delete_Temporary_Files(flPath);
            try
            {

                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                System.Diagnostics.Process prs = new System.Diagnostics.Process();

                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                System.Environment.SetEnvironmentVariable("ASTRA", flPath);


                prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast004.exe");

                //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast005.exe");
                if (prs.Start())
                    prs.WaitForExit();

                //string ana_rep = Path.Combine(Path.GetDirectoryName(flPath), "ANALYSIS_REP.TXT");
                //string sap_rep = Path.Combine(Path.GetDirectoryName(flPath), "RES001.tmp");

                //if (File.Exists(sap_rep))
                //{
                //    File.Copy(sap_rep, ana_rep, true);
                //    File.Delete(sap_rep);
                //}
            }
            catch (Exception exx) { }
            return File.Exists(MyList.Get_Analysis_Report_File(flPath));
        }

        private void btn_st_1_process_Click(object sender, EventArgs e)
        {
            string fname = Get_Stage_File(1);

            Button btn = sender as Button;

            if (btn.Name == btn_st_1_process.Name)
            {

                Write_Data_to_File(1);
                iApp.Delete_Temporary_Files(fname);
                if (Run_Data(fname))
                {
                    Show_Analysis(1);
                }
            }
            else if (btn.Name == btn_st_1_view_report.Name)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(fname)))
                    iApp.RunExe(MyList.Get_Analysis_Report_File(fname));
            }
            else if (btn.Name == btn_st_1_view_structure.Name)
            {
                Write_Data_to_File(1);
                if (File.Exists(fname))
                {
                    //iApp.Form_ASTRA_Analysis_Process(fname, false).ShowDialog();
                    //if (File.Exists(MyList.Get_Analysis_Report_File(fname)))
                    //    Show_Analysis(1);
                    iApp.OpenWork(fname, false);
                }
            }
            else if (btn.Name == btn_st_1_view_inputs.Name)
            {
                Write_Data_to_File(1);
                if (File.Exists(fname))
                {

                    iApp.Form_ASTRA_Input_Data(fname, false).ShowDialog();
                    rtb_st_1_input.Lines = File.ReadAllLines(fname);

                }
            }
            iApp.Save_Form_Record(this, user_path);

            Button_Enable_Disable();
        }

        private void btn_st_2_process_Click(object sender, EventArgs e)
        {
            string fname = Get_Stage_File(2);

            Button btn = sender as Button;

            if (btn.Name == btn_st_2_process.Name)
            {
                //if (MessageBox.Show("Check for Additional/New Nodes & Members the necessary data for Material" +
                //    " Contants, Section Properties, Supports & Loadings are given in the input data manualy by the User." +
                //    "\n\n Do you want to Proceed?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}

                #region Process Analysis

                Write_Data_to_File(2);

                //if (Run_Data(fname))
                if (Run_Data2(fname))
                {
                    Show_Analysis(2);
                }
                #endregion Process Analysis


            }
            else if (btn.Name == btn_st_2_view_report.Name)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(fname)))
                    iApp.RunExe(MyList.Get_Analysis_Report_File(fname));
            }
            else if (btn.Name == btn_st_2_apply.Name)
            {
                Apply_Stage_Analysis(2);
            }
            else if (btn.Name == btn_st_2_view_structure.Name)
            {
                Write_Data_to_File(2);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, false);

            }
            else if (btn.Name == btn_st_2_view_inputs.Name)
            {
                Write_Data_to_File(2);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, "");

            }

            iApp.Save_Form_Record(this, user_path);
            Button_Enable_Disable();
        }
        public void Apply_Stage_Analysis(int stage_no)
        {
            string st_file = Get_Stage_File(stage_no-1);

            if(File.Exists(st_file))
            {
                string knode = Path.Combine(Path.GetDirectoryName(st_file), "k_node.fil");
                string knode2 = Path.Combine(Path.GetDirectoryName(Get_Stage_File(stage_no)), "k_node.fil"); 

                if(File.Exists(knode))
                {
                    File.Copy(knode, knode2, true);
                }

                
                knode = Path.Combine(Path.GetDirectoryName(st_file), "jload_stage.fil");
                knode2 = Path.Combine(Path.GetDirectoryName(Get_Stage_File(stage_no)), "jload_stage.fil"); 

                if(File.Exists(knode))
                {
                    File.Copy(knode, knode2, true);
                }

            }

            CBridgeStructure structure = new CBridgeStructure(st_file);


            structure.SetASTRADocFromTXT();

            List<string> file_1 = new List<string>(File.ReadAllLines(st_file));


            int j_index = -1;

            string kStr = "";

            NodeResults NDR = new NodeResults();

            DataGridView dgv = dgv_st_1_trans;
            ComboBox cmb = cmb_st_2_loadcase;
            RichTextBox rtb = rtb_st_2_input;


            string mem_conn = "";

            if (stage_no == 3)
            {
                dgv = dgv_st_2_trans;
                cmb = cmb_st_3_loadcase;
                rtb = rtb_st_3_input;
            }
            else if (stage_no == 4)
            {
                dgv = dgv_st_3_trans;
                cmb = cmb_st_4_loadcase;
                rtb = rtb_st_4_input;
            }
            else if (stage_no == 5)
            {
                dgv = dgv_st_4_trans;
                cmb = cmb_st_5_loadcase;
                rtb = rtb_st_5_input;
            }
            


            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if(dgv[1,i].Value.ToString() == cmb.SelectedItem.ToString())
                {
                    NodeResultData nr = new NodeResultData();
                    nr.NodeNo = MyList.StringToInt(dgv[0,i].Value.ToString(), 0);
                    nr.LoadCase = MyList.StringToInt(dgv[1,i].Value.ToString(), 0);
                    nr.X_Translation = MyList.StringToDouble(dgv[2,i].Value.ToString(), 0);
                    nr.Y_Translation = MyList.StringToDouble(dgv[3, i].Value.ToString(), 0);
                    nr.Z_Translation = MyList.StringToDouble(dgv[4, i].Value.ToString(), 0);
                    NDR.Add(nr);
                }
                
            }

            List<string> modify_inputs = new List<string>();

            for (int i = 0; i < file_1.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(file_1[i].ToUpper());
                if (kStr.Contains("JOINT C"))
                {
                    j_index = i;
                    i++;

                    //while (!kStr.Contains("PROP") && !kStr.Contains("UNIT") && !kStr.Contains("ELE"))
                    while (!kStr.Contains("MEMB") && !kStr.Contains("UNIT") && !kStr.Contains("ELE"))
                    {

                        if (kStr.Contains("MEMB"))
                        {
                            //file_1.RemoveAt(i);
                            mem_conn = file_1[i].ToUpper();
                            break;
                        }
                        //if (!kStr.Contains("UNIT"))
                        file_1.RemoveAt(i);

                        kStr = file_1[i].ToUpper();
                    }

                    //mem_conn                    //mem_conn
                    //while (!kStr.Contains("MEMB"))
                    //{
                    //    file_1.RemoveAt(i);
                    //    kStr = file_1[i].ToUpper();
                    //}
                    break;
                }
            }



            for (int i = 0; i < structure.Joints.Count; i++)
            {
                var jnt = structure.Joints[i];
                JointNode jn = new JointNode();
                jn.NodeNo = jnt.NodeNo;

                //NodeResultData nr = NDR.Get_Node_Deflection(jn.NodeNo);
                NodeResultData nr = NDR.Get_Node_Deflection(jn.NodeNo);

                jn.X = jnt.X + nr.X_Translation;
                jn.Y = jnt.Y + nr.Y_Translation;
                jn.Z = jnt.Z + nr.Z_Translation;

                modify_inputs.Add(string.Format("{0,-5}  {1,10:f6}  {2,10:f6}  {3,10:f6}", jn.NodeNo, jn.X, jn.Y, jn.Z));
            }

            //modify_inputs.Add(string.Format("{0} ", mem_conn));
            //for (int i = 0; i < structure.Members.Count; i++)
            //{
            //    var mbr = structure.Members[i];
                
            //    modify_inputs.Add(string.Format("{0,-5}  {1,5:f0}  {2,5:f0}", mbr.MemberNo, mbr.StartNode.NodeNo, mbr.EndNode.NodeNo));
            //}



            file_1.InsertRange(j_index + 1, modify_inputs.ToArray());

            rtb.Lines = file_1.ToArray();



        }

        private void btn_st_3_process_Click(object sender, EventArgs e)
        {

            string fname = Get_Stage_File(3);

            Button btn = sender as Button;

            if (btn.Name == btn_st_3_process.Name)
            {
                #region Process Analysis


                //if (MessageBox.Show("Check for Additional/New Nodes & Members the necessary data for Material" +
                //    " Contants, Section Properties, Supports & Loadings are given in the input data manualy by the User." +
                //    "\n\n Do you want to Proceed?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}

                Write_Data_to_File(3);


                //if (Run_Data(fname))
                if (Run_Data2(fname))
                {
                    Show_Analysis(3);
                }
                #endregion Process Analysis
            }
            else if (btn.Name == btn_st_3_view_report.Name)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(fname)))
                    iApp.RunExe(MyList.Get_Analysis_Report_File(fname));
            }
            else if (btn.Name == btn_st_3_apply.Name)
            {
                Apply_Stage_Analysis(3);
            }
            else if (btn.Name == btn_st_3_view_structure.Name)
            {
                Write_Data_to_File(3);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, false);
            }
            else if (btn.Name == btn_st_3_view_inputs.Name)
            {
                Write_Data_to_File(3);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, "");

            }


            iApp.Save_Form_Record(this, user_path);
            Button_Enable_Disable();
        }


        private void btn_st_4_process_Click(object sender, EventArgs e)
        {

            string fname = Get_Stage_File(4);

            Button btn = sender as Button;

            if (btn.Name == btn_st_4_process.Name)
            {
                #region Process Analysis


                //if (MessageBox.Show("Check for Additional/New Nodes & Members the necessary data for Material" +
                //    " Contants, Section Properties, Supports & Loadings are given in the input data manualy by the User." +
                //    "\n\n Do you want to Proceed?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}

                Write_Data_to_File(4);


                if (Run_Data2(fname))
                {
                    Show_Analysis(4);
                }
                #endregion Process Analysis
            }
            else if (btn.Name == btn_st_4_view_report.Name)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(fname)))
                    iApp.RunExe(MyList.Get_Analysis_Report_File(fname));
            }
            else if (btn.Name == btn_st_4_apply.Name)
            {
                Apply_Stage_Analysis(4);
            }
            else if (btn.Name == btn_st_4_view_structure.Name)
            {
                Write_Data_to_File(4);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, false);

            }
            else if (btn.Name == btn_st_4_view_inputs.Name)
            {
                Write_Data_to_File(4);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, "");
            }
            iApp.Save_Form_Record(this, user_path);
            Button_Enable_Disable();
        }

        private void btn_st_5_process_Click(object sender, EventArgs e)
        {
            string fname = Get_Stage_File(5);

            Button btn = sender as Button;

            if (btn.Name == btn_st_5_process.Name)
            {

                //if (MessageBox.Show("Check for Additional/New Nodes & Members the necessary data for Material" +
                //    " Contants, Section Properties, Supports & Loadings are given in the input data manualy by the User." +
                //    "\n\n Do you want to Proceed?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}

                #region Process Analysis

                Write_Data_to_File(5);

                //if (Run_Data(fname))
                if (Run_Data2(fname))
                {
                    Show_Analysis(5);
                }
                #endregion Process Analysis
            }
            else if (btn.Name == btn_st_5_view_report.Name)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(fname)))
                    iApp.RunExe(MyList.Get_Analysis_Report_File(fname));
            }
            else if (btn.Name == btn_st_5_apply.Name)
            {
                Apply_Stage_Analysis(5);
            }
            else if (btn.Name == btn_st_5_view_structure.Name)
            {
                Write_Data_to_File(5);

                if (File.Exists(fname))
                    iApp.OpenWork(fname, false);

            }
            else if (btn.Name == btn_st_5_view_inputs.Name)
            {
                Write_Data_to_File(5);
                if (File.Exists(fname))
                    iApp.OpenWork(fname, "");
            }


            iApp.Save_Form_Record(this, user_path);
            Button_Enable_Disable();
        }

        private void Write_Data_to_File(int stage_no)
        {

            string fname = Get_Stage_File(stage_no);

            DataGridView dgv = dgv_st_1_trans;


            //if (false)
            if (stage_no > 1)
            {

                if (stage_no == 2)
                {
                    dgv = dgv_st_1_trans;
                }
                else if (stage_no == 3)
                {
                    dgv = dgv_st_2_trans;
                }
                else if (stage_no == 4)
                {
                    dgv = dgv_st_3_trans;
                }
                else if (stage_no == 5)
                {
                    dgv = dgv_st_4_trans;
                }


                #region SAP Analysis
                SAP_Document sap = new SAP_Document();


                string sap_file = Get_Stage_File(stage_no - 1);

                sap_file = Path.Combine(Path.GetDirectoryName(sap_file), "SAP_Input_Data.txt");

                if (File.Exists(sap_file))
                {
                    sap.Read_SAP_Data(sap_file);

                    if (sap.Joints.Count > 0)
                    {
                        if (dgv.RowCount > 0)
                        {
                            for (int i = 0; i < sap.Joints.Count; i++)
                            {
                                var item = sap.Joints[i];

                                item.X = item.X + MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0);
                                item.Y = item.Y + MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0);
                                item.Z = item.Z + MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0);
                            }
                        }
                    }

                    sap_file = Path.Combine(Path.GetDirectoryName(fname), "inp.tmp");
                    File.WriteAllLines(sap_file, sap.Get_SAP_Data().ToArray());

                }
                #endregion SAP Analysis
            }


            RichTextBox rtb = rtb_st_1_input;

            if (stage_no == 2) rtb = rtb_st_2_input;
            if (stage_no == 3) rtb = rtb_st_3_input;
            if (stage_no == 4) rtb = rtb_st_4_input;
            if (stage_no == 5) rtb = rtb_st_5_input;


            File.WriteAllLines(fname, rtb.Lines);

            if (rtb.Text.Contains("LL.TXT"))
            {
                string ll_txt = MyList.Get_LL_TXT_File(file_path);
                File.Copy(ll_txt, MyList.Get_LL_TXT_File(fname), true);
            }
        }

        private void Show_Analysis(int stage_no)
        {
            string fname = Get_Stage_File(stage_no);
            iApp.Progress_Works.Clear();
            iApp.Progress_Works.Add("Read Analysis Results from Analysis Report File");
            BridgeMemberAnalysis bma = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(fname));
            iApp.Progress_Works.Clear();


            DataGridView dgv_trans = dgv_st_4_trans;
            DataGridView dgv_beam_forces = dgv_st_4_beam_forces;
            DataGridView dgv_truss_forces = dgv_st_4_truss_forces;
            DataGridView dgv_cable_forces = dgv_st_4_cable_forces;
            ComboBox cmb = cmb_st_2_loadcase;


            if (stage_no == 1)
            {

                dgv_trans = dgv_st_1_trans;
                dgv_beam_forces = dgv_st_1_beam_forces;
                dgv_truss_forces = dgv_st_1_truss_forces;
                dgv_cable_forces = dgv_st_1_cable_forces;
                cmb = cmb_st_2_loadcase;
            }
            else if (stage_no == 2)
            {

                dgv_trans = dgv_st_2_trans;
                dgv_beam_forces = dgv_st_2_beam_forces;
                dgv_truss_forces = dgv_st_2_truss_forces;
                dgv_cable_forces = dgv_st_2_cable_forces;
                cmb = cmb_st_3_loadcase;
            }
            else if (stage_no == 3)
            {

                dgv_trans = dgv_st_3_trans;
                dgv_beam_forces = dgv_st_3_beam_forces;
                dgv_truss_forces = dgv_st_3_truss_forces;
                dgv_cable_forces = dgv_st_3_cable_forces;
                cmb = cmb_st_4_loadcase;
            }
            else if (stage_no == 4)
            {
                dgv_trans = dgv_st_4_trans;
                dgv_beam_forces = dgv_st_4_beam_forces;
                dgv_truss_forces = dgv_st_4_truss_forces;
                dgv_cable_forces = dgv_st_4_cable_forces;
                cmb = cmb_st_5_loadcase;
            }
            else if (stage_no == 5)
            {

                dgv_trans = dgv_st_5_trans;
                dgv_beam_forces = dgv_st_5_beam_forces;
                dgv_truss_forces = dgv_st_5_truss_forces;
                dgv_cable_forces = dgv_st_5_cable_forces;
                cmb = null;
            }



            dgv_trans.Rows.Clear();
            dgv_beam_forces.Rows.Clear();
            dgv_truss_forces.Rows.Clear();
            dgv_cable_forces.Rows.Clear();


            if (cmb != null)
            {
                cmb.Items.Clear();
                foreach (var item in bma.Get_LoadCases())
                {
                    cmb.Items.Add(item);

                }
                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;
            }

            if (bma.Node_Displacements != null)
            {
                bma.Node_Displacements.Reverse();
                for (int i = 0; i < bma.Node_Displacements.Count; i++)
                {
                    var item = bma.Node_Displacements[i];
                    dgv_trans.Rows.Add(item.NodeNo, item.LoadCase, item.X_Translation, item.Y_Translation, item.Z_Translation);
                }
                AstraMember astMem;
                for (int i = 0; i < bma.Beams_Forces.Count; i++)
                {

                    var item = bma.Beams_Forces[i];

                    astMem = bma.Get_User_Member_Number(item.BeamNo, eAstraMemberType.BEAM);
                    if (astMem == null)
                    {
                        dgv_beam_forces.Rows.Add(item.BeamNo, item.LoadNo,
                            item.StartNodeForce.R1_Axial,
                            item.StartNodeForce.R2_Shear,
                            item.StartNodeForce.R3_Shear,
                            item.StartNodeForce.M1_Torsion,
                            item.StartNodeForce.M2_Bending,
                            item.StartNodeForce.M3_Bending);
                    }
                    else
                    {
                        dgv_beam_forces.Rows.Add(astMem.UserNo, item.LoadNo,
                            item.StartNodeForce.R1_Axial,
                            item.StartNodeForce.R2_Shear,
                            item.StartNodeForce.R3_Shear,
                            item.StartNodeForce.M1_Torsion,
                            item.StartNodeForce.M2_Bending,
                            item.StartNodeForce.M3_Bending);
                    }
                    dgv_beam_forces.Rows.Add("", "",
                        item.EndNodeForce.R1_Axial,
                        item.EndNodeForce.R2_Shear,
                        item.EndNodeForce.R3_Shear,
                        item.EndNodeForce.M1_Torsion,
                        item.EndNodeForce.M2_Bending,
                        item.EndNodeForce.M3_Bending);
                }

                for (int i = 0; i < bma.Trusses_Forces.Count; i++)
                {
                    var item = bma.Trusses_Forces[i];
                    astMem = bma.Get_User_Member_Number(item.TrussMemberNo, eAstraMemberType.TRUSS);

                    dgv_truss_forces.Rows.Add(astMem.UserNo,
                        item.LoadNo,
                        item.Stress,
                        item.Force);
                }
                for (int i = 0; i < bma.Cables_Forces.Count; i++)
                {
                    var item = bma.Cables_Forces[i];
                    astMem = bma.Get_User_Member_Number(item.TrussMemberNo, eAstraMemberType.CABLE);
                    dgv_cable_forces.Rows.Add(astMem.UserNo,
                        item.LoadNo,
                        item.Stress,
                        item.Force);
                }
            }

            if (stage_no == 1)
            {
                if (bma.Beams_Forces.Count == 0 && tab_st_1_forces.TabPages.Contains(tab_st_1_beams))
                    tab_st_1_forces.TabPages.Remove(tab_st_1_beams);
                else if (bma.Beams_Forces.Count > 0 && !tab_st_1_forces.TabPages.Contains(tab_st_1_beams))
                    tab_st_1_forces.TabPages.Add(tab_st_1_beams);

                if (bma.Trusses_Forces.Count == 0 && tab_st_1_forces.TabPages.Contains(tab_st_1_trusses))
                    tab_st_1_forces.TabPages.Remove(tab_st_1_trusses);
                else if (bma.Trusses_Forces.Count > 0 && !tab_st_1_forces.TabPages.Contains(tab_st_1_trusses))
                    tab_st_1_forces.TabPages.Add(tab_st_1_trusses);

                if (bma.Cables_Forces.Count == 0 && tab_st_1_forces.TabPages.Contains(tab_st_1_cables))
                    tab_st_1_forces.TabPages.Remove(tab_st_1_cables);
                else if (bma.Cables_Forces.Count > 0 && !tab_st_1_forces.TabPages.Contains(tab_st_1_cables))
                    tab_st_1_forces.TabPages.Add(tab_st_1_cables);
            }
            else if (stage_no == 2)
            {



                if (bma.Beams_Forces.Count == 0 && tab_st_2_forces.TabPages.Contains(tab_st_2_beams))
                    tab_st_2_forces.TabPages.Remove(tab_st_2_beams);
                else if (bma.Beams_Forces.Count > 0 && !tab_st_2_forces.TabPages.Contains(tab_st_2_beams))
                    tab_st_2_forces.TabPages.Add(tab_st_2_beams);

                if (bma.Trusses_Forces.Count == 0 && tab_st_2_forces.TabPages.Contains(tab_st_2_trusses))
                    tab_st_2_forces.TabPages.Remove(tab_st_2_trusses);
                else if (bma.Trusses_Forces.Count > 0 && !tab_st_2_forces.TabPages.Contains(tab_st_2_trusses))
                    tab_st_2_forces.TabPages.Add(tab_st_2_trusses);

                if (bma.Cables_Forces.Count == 0 && tab_st_2_forces.TabPages.Contains(tab_st_2_cables))
                    tab_st_2_forces.TabPages.Remove(tab_st_2_cables);
                else if (bma.Cables_Forces.Count > 0 && !tab_st_2_forces.TabPages.Contains(tab_st_2_cables))
                    tab_st_2_forces.TabPages.Add(tab_st_2_cables);

            }
            else if (stage_no == 3)
            {
                if (bma.Beams_Forces.Count == 0 && tab_st_3_forces.TabPages.Contains(tab_st_3_beams))
                    tab_st_3_forces.TabPages.Remove(tab_st_3_beams);
                else if (bma.Beams_Forces.Count > 0 && !tab_st_3_forces.TabPages.Contains(tab_st_3_beams))
                    tab_st_3_forces.TabPages.Add(tab_st_3_beams);

                if (bma.Trusses_Forces.Count == 0 && tab_st_3_forces.TabPages.Contains(tab_st_3_trusses))
                    tab_st_3_forces.TabPages.Remove(tab_st_3_trusses);
                else if (bma.Trusses_Forces.Count > 0 && !tab_st_3_forces.TabPages.Contains(tab_st_3_trusses))
                    tab_st_3_forces.TabPages.Add(tab_st_3_trusses);

                if (bma.Cables_Forces.Count == 0 && tab_st_3_forces.TabPages.Contains(tab_st_3_cables))
                    tab_st_3_forces.TabPages.Remove(tab_st_3_cables);
                else if (bma.Cables_Forces.Count > 0 && !tab_st_3_forces.TabPages.Contains(tab_st_3_cables))
                    tab_st_3_forces.TabPages.Add(tab_st_3_cables);

            }
            else if (stage_no == 4)
            {
                if (bma.Beams_Forces.Count == 0 && tab_st_4_forces.TabPages.Contains(tab_st_4_beams))
                    tab_st_4_forces.TabPages.Remove(tab_st_4_beams);
                else if (bma.Beams_Forces.Count > 0 && !tab_st_4_forces.TabPages.Contains(tab_st_4_beams))
                    tab_st_4_forces.TabPages.Add(tab_st_4_beams);

                if (bma.Trusses_Forces.Count == 0 && tab_st_4_forces.TabPages.Contains(tab_st_4_trusses))
                    tab_st_4_forces.TabPages.Remove(tab_st_4_trusses);
                else if (bma.Trusses_Forces.Count > 0 && !tab_st_4_forces.TabPages.Contains(tab_st_4_trusses))
                    tab_st_4_forces.TabPages.Add(tab_st_4_trusses);

                if (bma.Cables_Forces.Count == 0 && tab_st_4_forces.TabPages.Contains(tab_st_4_cables))
                    tab_st_4_forces.TabPages.Remove(tab_st_4_cables);
                else if (bma.Cables_Forces.Count > 0 && !tab_st_4_forces.TabPages.Contains(tab_st_4_cables))
                    tab_st_4_forces.TabPages.Add(tab_st_4_cables);
            }

            else if (stage_no == 5)
            {
                if (bma.Beams_Forces.Count == 0 && tab_st_5_forces.TabPages.Contains(tab_st_5_beams))
                    tab_st_5_forces.TabPages.Remove(tab_st_5_beams);
                else if (bma.Beams_Forces.Count > 0 && !tab_st_5_forces.TabPages.Contains(tab_st_5_beams))
                    tab_st_5_forces.TabPages.Add(tab_st_5_beams);

                if (bma.Trusses_Forces.Count == 0 && tab_st_5_forces.TabPages.Contains(tab_st_5_trusses))
                    tab_st_5_forces.TabPages.Remove(tab_st_5_trusses);
                else if (bma.Trusses_Forces.Count > 0 && !tab_st_5_forces.TabPages.Contains(tab_st_5_trusses))
                    tab_st_5_forces.TabPages.Add(tab_st_5_trusses);

                if (bma.Cables_Forces.Count == 0 && tab_st_5_forces.TabPages.Contains(tab_st_5_cables))
                    tab_st_5_forces.TabPages.Remove(tab_st_5_cables);
                else if (bma.Cables_Forces.Count > 0 && !tab_st_5_forces.TabPages.Contains(tab_st_5_cables))
                    tab_st_5_forces.TabPages.Add(tab_st_5_cables);
            }

            if(MessageBox.Show("Do you want to Open Analysis Report File ?","ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                iApp.View_Result(MyList.Get_Analysis_Report_File(fname));
            }
        }

        private void frmStageAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {

            iApp.StageNo = cmb_stage.SelectedIndex + 1;
            iApp.Stage_File = Get_Stage_File(iApp.StageNo);
            iApp.WorkingFile = file_path;
        }

        private void btn_st_2_jnt_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_st_2_jnt_add.Name)
            {
                Add_Joints(2);
            }
            else if (btn.Name == btn_st_2_add_mem.Name)
            {
                Add_Members(2);
            }
            else if (btn.Name == btn_st_3_jnt_add.Name)
            {
                Add_Joints(3);
            }
            else if (btn.Name == btn_st_3_add_mem.Name)
            {
                Add_Members(3);
            }
            else if (btn.Name == btn_st_4_jnt_add.Name)
            {
                Add_Joints(4);
            }
            else if (btn.Name == btn_st_4_add_mem.Name)
            {
                Add_Members(4);
            }
            else if (btn.Name == btn_st_5_jnt_add.Name)
            {
                Add_Joints(5);
            }
            else if (btn.Name == btn_st_5_add_mem.Name)
            {
                Add_Members(5);

            }

        }

        private void Add_Joints(int stage_no)
        {
            RichTextBox rtb = rtb_st_2_input;

            TextBox txt_start_no = txt_st_2_jnt_start_no;
            TextBox txt_start_x = txt_st_2_jnt_start_x;
            TextBox txt_start_y = txt_st_2_jnt_start_y;
            TextBox txt_start_z = txt_st_2_jnt_start_z;

            TextBox txt_end_no = txt_st_2_jnt_end_no;
            TextBox txt_end_x = txt_st_2_jnt_end_x;
            TextBox txt_end_y = txt_st_2_jnt_end_y;
            TextBox txt_end_z = txt_st_2_jnt_end_z;



            if (stage_no == 2)
            {
                rtb = rtb_st_2_input;
                txt_start_no = txt_st_2_jnt_start_no;
                txt_start_x = txt_st_2_jnt_start_x;
                txt_start_y = txt_st_2_jnt_start_y;
                txt_start_z = txt_st_2_jnt_start_z;

                txt_end_no = txt_st_2_jnt_end_no;
                txt_end_x = txt_st_2_jnt_end_x;
                txt_end_y = txt_st_2_jnt_end_y;
                txt_end_z = txt_st_2_jnt_end_z;

            }
            else if (stage_no == 3)
            {
                rtb = rtb_st_3_input;
                txt_start_no = txt_st_3_jnt_start_no;
                txt_start_x = txt_st_3_jnt_start_x;
                txt_start_y = txt_st_3_jnt_start_y;
                txt_start_z = txt_st_3_jnt_start_z;

                txt_end_no = txt_st_3_jnt_end_no;
                txt_end_x = txt_st_3_jnt_end_x;
                txt_end_y = txt_st_3_jnt_end_y;
                txt_end_z = txt_st_3_jnt_end_z;
            }
            else if (stage_no == 4)
            {
                rtb = rtb_st_3_input;
                txt_start_no = txt_st_4_jnt_start_no;
                txt_start_x = txt_st_4_jnt_start_x;
                txt_start_y = txt_st_4_jnt_start_y;
                txt_start_z = txt_st_4_jnt_start_z;

                txt_end_no = txt_st_4_jnt_end_no;
                txt_end_x = txt_st_4_jnt_end_x;
                txt_end_y = txt_st_4_jnt_end_y;
                txt_end_z = txt_st_4_jnt_end_z;
            }
            else if (stage_no == 5)
            {
                rtb = rtb_st_4_input;
                txt_start_no = txt_st_5_jnt_start_no;
                txt_start_x = txt_st_5_jnt_start_x;
                txt_start_y = txt_st_5_jnt_start_y;
                txt_start_z = txt_st_5_jnt_start_z;

                txt_end_no = txt_st_5_jnt_end_no;
                txt_end_x = txt_st_5_jnt_end_x;
                txt_end_y = txt_st_5_jnt_end_y;
                txt_end_z = txt_st_5_jnt_end_z;
            }


            //CBridgeStructure structure, List<string> file_1, List<string> modify_inputs, string mem_conn, int j_index;

            CBridgeStructure structure = new CBridgeStructure();

            structure.ASTRA_Data.AddRange(rtb.Lines);

            structure.SetASTRADocFromTXT();



            List<string> file_1 = new List<string>(rtb.Lines);
            List<string> modify_inputs = new List<string>();

            string kStr = "";
            string mem_conn = "";
            int j_index = 0;


            #region Add Joint


            #region Read Data

            for (int i = 0; i < file_1.Count; i++)
            {
                kStr = file_1[i].ToUpper();
                if (kStr.Contains("JOINT C"))
                {
                    j_index = i;
                    i++;

                    while (!kStr.Contains("PROP"))
                    {

                        if (kStr.Contains("MEMB"))
                        {
                            //file_1.RemoveAt(i);
                            mem_conn = file_1[i].ToUpper();
                        }

                        file_1.RemoveAt(i);
                        kStr = file_1[i].ToUpper();
                    }
                    break;
                }
            }


            for (int i = 0; i < structure.Joints.Count; i++)
            {
                var jn = structure.Joints[i];
                modify_inputs.Add(string.Format("{0,-5}  {1,10:f6}  {2,10:f6}  {3,10:f6}", jn.NodeNo, jn.X, jn.Y, jn.Z));
            }

            #endregion Read Data



            try
            {

                int n1 = 0, n2 = 0, kdif = 0;
                double dx = 0.0d, dy = 0.0d, dz = 0.0d;
                double x = 0.0d, y = 0.0d, z = 0.0d;
                double kx = 0.0d, ky = 0.0d, kz = 0.0d;
                //int addFactor = 0, nodeDif = 0;

                JointNodeCollection jnc = new JointNodeCollection();
                int indx = 0;

                n1 = int.Parse(txt_start_no.Text); indx++;
                x = double.Parse(txt_start_x.Text); indx++;
                y = double.Parse(txt_start_y.Text); indx++;
                z = double.Parse(txt_start_z.Text); indx++;


                JointNode jc = new JointNode();
                jc.NodeNo = n1;
                jc.X = x;
                jc.Y = y;
                jc.Z = z;

                if (n1 > 0)
                    jnc.Add(jc);


                n2 = int.Parse(txt_end_no.Text); indx++;
                dx = double.Parse(txt_end_x.Text); indx++;
                dy = double.Parse(txt_end_y.Text); indx++;
                dz = double.Parse(txt_end_z.Text); indx++;

                kdif = n2 - n1;

                //kx = (x + dx) / (kdif);
                //ky = (y + dy) / (kdif);
                //kz = (z + dz) / (kdif);



                kx = (dx - x) / (kdif);
                ky = (dy - y) / (kdif);
                kz = (dz - z) / (kdif);

                for (int i = 1; i < kdif; i++)
                {
                    jc = new JointNode();
                    jc.NodeNo = n1 + i;
                    //jc.XYZ = new gPoint(x + kx * i, y + ky * i, z + kz * i);
                    jc.X = x + kx * i;
                    jc.Y = y + ky * i;
                    jc.Z = z + kz * i;

                    jnc.Add(jc);
                }
                jc = new JointNode();
                jc.NodeNo = n2;
                //jc.XYZ = new gPoint(dx, dy, dz);
                jc.X = dx;
                jc.Y = dy;
                jc.Z = dz;

                if (n2 > 0)
                    jnc.Add(jc);

                for (int i = 0; i < jnc.Count; i++)
                {
                    var jn = jnc[i];
                    modify_inputs.Add(string.Format("{0,-5}  {1,10:f6}  {2,10:f6}  {3,10:f6}", jn.NodeNo, jn.X, jn.Y, jn.Z));
                }
            }
            catch (Exception exx)
            {
            }

            modify_inputs.Add(string.Format("{0} ", mem_conn));
            for (int i = 0; i < structure.Members.Count; i++)
            {
                var mbr = structure.Members[i];

                modify_inputs.Add(string.Format("{0,-5}  {1,5:f0}  {2,5:f0}", mbr.MemberNo, mbr.StartNode.NodeNo, mbr.EndNode.NodeNo));
            }

            file_1.InsertRange(j_index + 1, modify_inputs.ToArray());

            rtb.Lines = file_1.ToArray();

            #endregion Add Joint
        }

        private void Add_Members(int stage_no)
        {


            RichTextBox rtb = rtb_st_2_input;

            TextBox txt_start_no = txt_st_2_mbr_start_no;
            TextBox txt_start_jnt = txt_st_2_mbr_start_jnt;
            TextBox txt_end_jnt = txt_st_2_mbr_end_jnt;
            TextBox txt_incr_jnt = txt_st_2_mbr_incr_jnt;



            if (stage_no == 2)
            {
                rtb = rtb_st_2_input;
                txt_start_no = txt_st_2_mbr_start_no;
                txt_start_jnt = txt_st_2_mbr_start_jnt;
                txt_end_jnt = txt_st_2_mbr_end_jnt;
                txt_incr_jnt = txt_st_2_mbr_incr_jnt;
            }
            else if (stage_no == 3)
            {
                rtb = rtb_st_3_input;
                txt_start_no = txt_st_3_mbr_start_no;
                txt_start_jnt = txt_st_3_mbr_start_jnt;
                txt_end_jnt = txt_st_3_mbr_end_jnt;
                txt_incr_jnt = txt_st_3_mbr_incr_jnt;
            }
            else if (stage_no == 4)
            {
                rtb = rtb_st_4_input;
                txt_start_no = txt_st_4_mbr_start_no;
                txt_start_jnt = txt_st_4_mbr_start_jnt;
                txt_end_jnt = txt_st_4_mbr_end_jnt;
                txt_incr_jnt = txt_st_4_mbr_incr_jnt;
            }
            else if (stage_no == 5)
            {
                rtb = rtb_st_5_input;
                txt_start_no = txt_st_5_mbr_start_no;
                txt_start_jnt = txt_st_5_mbr_start_jnt;
                txt_end_jnt = txt_st_5_mbr_end_jnt;
                txt_incr_jnt = txt_st_5_mbr_incr_jnt;
            }


            CBridgeStructure structure = new CBridgeStructure();

            structure.ASTRA_Data.AddRange(rtb.Lines);

            structure.SetASTRADocFromTXT();



            List<string> file_1 = new List<string>(rtb.Lines);
            List<string> modify_inputs = new List<string>();


            string kStr = "";
            string mem_conn = "";
            int j_index = 0;



            #region Read Data

            for (int i = 0; i < file_1.Count; i++)
            {
                kStr = file_1[i].ToUpper();
                if (kStr.Contains("JOINT C"))
                {
                    j_index = i;
                    i++;

                    while (!kStr.Contains("PROP"))
                    {

                        if (kStr.Contains("MEMB"))
                        {
                            //file_1.RemoveAt(i);
                            mem_conn = file_1[i].ToUpper();
                        }

                        file_1.RemoveAt(i);
                        kStr = file_1[i].ToUpper();
                    }
                    break;
                }
            }


            for (int i = 0; i < structure.Joints.Count; i++)
            {
                var jn = structure.Joints[i];
                modify_inputs.Add(string.Format("{0,-5}  {1,10:f6}  {2,10:f6}  {3,10:f6}", jn.NodeNo, jn.X, jn.Y, jn.Z));
            }

            #endregion Read Data



            #region Add Members

            int n1 = 0, sNode = 0, eNode = 0, toNode = 0, indx = 0;

            MemberCollection mbc = new MemberCollection();

            Member mIncnd1, mIncnd2;

            indx = 0;


            n1 = MyList.StringToInt(txt_start_no.Text, 0); indx++;
            sNode = MyList.StringToInt(txt_start_jnt.Text, 0); indx++;
            eNode = MyList.StringToInt(txt_end_jnt.Text, 0); indx++;
            toNode = MyList.StringToInt(txt_incr_jnt.Text, 0); indx++;


            mIncnd1 = new Member();
            mIncnd1.MemberNo = n1;
            mIncnd1.StartNode.NodeNo = sNode;
            mIncnd1.EndNode.NodeNo = eNode;


            if (n1 > 0)
                mbc.Add(mIncnd1);

            for (int i = n1 + 1; i <= toNode; i++)
            {
                sNode++; eNode++;
                mIncnd2 = new Member();
                mIncnd2.MemberNo = i;
                mIncnd2.StartNode.NodeNo = sNode;
                mIncnd2.EndNode.NodeNo = eNode;
                mbc.Add(mIncnd2);
            }


            modify_inputs.Add(string.Format("{0} ", mem_conn));
            for (int i = 0; i < structure.Members.Count; i++)
            {
                var mbr = structure.Members[i];

                modify_inputs.Add(string.Format("{0,-5}  {1,5:f0}  {2,5:f0}", mbr.MemberNo, mbr.StartNode.NodeNo, mbr.EndNode.NodeNo));
            }

            for (int i = 0; i < mbc.Count; i++)
            {
                var mbr = mbc[i];

                modify_inputs.Add(string.Format("{0,-5}  {1,5:f0}  {2,5:f0}", mbr.MemberNo, mbr.StartNode.NodeNo, mbr.EndNode.NodeNo));
            }

            file_1.InsertRange(j_index + 1, modify_inputs.ToArray());

            rtb.Lines = file_1.ToArray();
            #endregion Add Members
        }

        private void cmb_st_loadcase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_Enable_Disable();
        }

        public void Write_Analysis_Result()
        {
        

            List<string> list = new List<string>();


            #region TechSOFT Banner
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                  ASTRA Pro                  *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*       STAGE (P-DELTA) ANALYSIS REPORT       *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            iApp.Progress_Works.Clear();

            iApp.Progress_Works.Add("Gerenerating Results....");
            iApp.Progress_ON("Read Data");

            if (dgv_st_1_trans.RowCount > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format("STAGE 1 ANALYSIS RESULT SUMMARY"));
                list.Add(string.Format("STAGE 1 ANALYSIS INPUT DATA"));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("ANALYSIS INPUT DATA"));
                //list.Add(string.Format("--------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.AddRange(rtb_st_1_input.Lines);
                //list.Add(string.Format("--------------------------------------------------------------------------"));

                iApp.SetProgressValue(10, 100);
                list.AddRange(Get_Translation_Data(dgv_st_1_trans, "STAGE 1 : "));


                iApp.SetProgressValue(20, 100);
                list.AddRange(Get_Beam_Forces_Data(dgv_st_1_beam_forces, "STAGE 1 : "));

                iApp.SetProgressValue(30, 100);
                list.AddRange(Get_Truss_Forces_Data(dgv_st_1_truss_forces, "STAGE 1 : "));


                iApp.SetProgressValue(40, 100);
                list.AddRange(Get_Cable_Forces_Data(dgv_st_1_cable_forces, "STAGE 1 : "));
            }

            if (dgv_st_2_trans.RowCount > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format("STAGE 2 ANALYSIS RESULT SUMMARY"));
                list.Add(string.Format("STAGE 2 ANALYSIS INPUT DATA"));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("ANALYSIS INPUT DATA"));
                //list.Add(string.Format("--------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.AddRange(rtb_st_2_input.Lines);
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.Add(string.Format(""));


                iApp.SetProgressValue(45, 100);
                list.AddRange(Get_Translation_Data(dgv_st_2_trans, "STAGE 2 : "));


                iApp.SetProgressValue(50, 100);
                list.AddRange(Get_Beam_Forces_Data(dgv_st_2_beam_forces, "STAGE 2 : "));


                iApp.SetProgressValue(55, 100);
                list.AddRange(Get_Truss_Forces_Data(dgv_st_2_truss_forces, "STAGE 2 : "));


                iApp.SetProgressValue(60, 100);
                list.AddRange(Get_Cable_Forces_Data(dgv_st_2_cable_forces, "STAGE 2 : "));
            }
            if (dgv_st_3_trans.RowCount > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format("STAGE 3 ANALYSIS RESULT SUMMARY"));
                list.Add(string.Format("STAGE 3 ANALYSIS INPUT DATA"));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("ANALYSIS INPUT DATA"));
                //list.Add(string.Format("--------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.AddRange(rtb_st_3_input.Lines);
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.Add(string.Format(""));


                iApp.SetProgressValue(62, 100);
                list.AddRange(Get_Translation_Data(dgv_st_3_trans, "STAGE 3 : "));


                iApp.SetProgressValue(65, 100);
                list.AddRange(Get_Beam_Forces_Data(dgv_st_3_beam_forces, "STAGE 3 : "));


                iApp.SetProgressValue(68, 100);
                list.AddRange(Get_Truss_Forces_Data(dgv_st_3_truss_forces, "STAGE 3 : "));


                iApp.SetProgressValue(70, 100);
                list.AddRange(Get_Cable_Forces_Data(dgv_st_3_cable_forces, "STAGE 3 : "));
            }

            if (dgv_st_4_trans.RowCount > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format("STAGE 4 ANALYSIS RESULT SUMMARY"));
                list.Add(string.Format("STAGE 4 ANALYSIS INPUT DATA"));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("ANALYSIS INPUT DATA"));
                //list.Add(string.Format("--------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.AddRange(rtb_st_4_input.Lines);
                //list.Add(string.Format("--------------------------------------------------------------------------"));
                list.Add(string.Format(""));


                iApp.SetProgressValue(72, 100);
                list.AddRange(Get_Translation_Data(dgv_st_4_trans, "STAGE 4 : "));
                iApp.SetProgressValue(74, 100);
                list.AddRange(Get_Beam_Forces_Data(dgv_st_4_beam_forces, "STAGE 4 : "));
                iApp.SetProgressValue(76, 100);
                list.AddRange(Get_Truss_Forces_Data(dgv_st_4_truss_forces, "STAGE 4 : "));
                iApp.SetProgressValue(79, 100);
                list.AddRange(Get_Cable_Forces_Data(dgv_st_4_cable_forces, "STAGE 4 : "));
            }
            if (dgv_st_5_trans.RowCount > 0)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format("STAGE 5 ANALYSIS RESULT SUMMARY"));
                list.Add(string.Format("STAGE 5 ANALYSIS INPUT DATA"));
                list.Add(string.Format("----------------------------------"));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("ANALYSIS INPUT DATA"));
                //list.Add(string.Format("--------------------"));
                //list.Add(string.Format(""));
                list.AddRange(rtb_st_5_input.Lines);
                list.Add(string.Format(""));

                iApp.SetProgressValue(81, 100);
                list.AddRange(Get_Translation_Data(dgv_st_5_trans, "STAGE 5 : "));
                iApp.SetProgressValue(83, 100);
                list.AddRange(Get_Beam_Forces_Data(dgv_st_5_beam_forces, "STAGE 5 : "));
                iApp.SetProgressValue(84, 100);
                list.AddRange(Get_Truss_Forces_Data(dgv_st_5_truss_forces, "STAGE 5 : "));
                iApp.SetProgressValue(89, 100);
                list.AddRange(Get_Cable_Forces_Data(dgv_st_5_cable_forces, "STAGE 5 : "));
            }

            list.Add(string.Format("----------------------------------------------------------------------------------"));
            list.Add(string.Format("                         END OF REPORT"));
            list.Add(string.Format("----------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            rtb_results.Lines = list.ToArray();


            File.WriteAllLines(Report_File, list.ToArray());

            iApp.SetProgressValue(92, 100);
            Select_Steps();
            iApp.SetProgressValue(100, 100);

            iApp.Progress_OFF();

            //MessageBox.Show("Stage Analysis Report file created as '" + Path.GetFileName(fn) + "' in the working folder.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //MessageBox.Show("Stage Analysis Report file created as '" + Path.GetFileName(Report_File) + "' in the working folder.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Stage Analysis Report file created as [" + Report_File + "]", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //iApp.View_Result(fn);

        }
        string Report_File { get { return Path.Combine(user_path, "STAGE_ANALYSIS_REPORT.TXT"); } }

        public List<string> Get_Translation_Data(DataGridView dgv, string stg)
        {


            

            List<string> list = new List<string>();

            if (dgv.RowCount == 0) return list;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format("{0}NODAL DISPLACEMENTS", stg));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------"));
            list.Add(string.Format("  NODE    LOAD         X             Y             Z    "));
            list.Add(string.Format(" NUMBER   CASE    TRANSLATION   TRANSLATION   TRANSLATION "));
            list.Add(string.Format("----------------------------------------------------------"));
            int jn = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i > 0)
                {
                    if (dgv[0, i - 1].Value.ToString() != dgv[0, i].Value.ToString())
                        list.Add(string.Format(""));
                }

                jn =  MyList.StringToInt(dgv[0, i].Value.ToString(), 0);
                list.Add(string.Format("{0,5}   {1,4}     {2,12:f7}  {3,12:f7}  {4,12:f7}",
                    jn,
                     MyList.StringToDouble(dgv[1, i].Value.ToString(), 0.0),
                     MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0),
                     MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0),
                     MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0)));
               
            }
            list.Add(string.Format("----------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            return list;
        }

        public List<string> Get_Beam_Forces_Data(DataGridView dgv, string stg)
        {
            List<string> list = new List<string>();
            if (dgv.RowCount == 0) return list;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format("{0}BEAM MEMBER FORCES AND MOMENTS", stg));
            list.Add(string.Format("------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("  BEAM  LOAD      AXIAL        SHEAR        SHEAR       TORSION      BENDING      BENDING"));
            list.Add(string.Format("   NO.   NO.        FX           FY           FZ           MX           MY           MZ"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            for (int i = 0; i < dgv.RowCount; i++)
            {

                int jn = MyList.StringToInt(dgv[0, i].Value.ToString(), 0);

                if (jn == 0)
                {
                    list.Add(string.Format("{0,5} {1,5} {2,12:f4} {3,12:f4} {4,12:f4} {5,12:f4} {6,12:f4} {7,12:f4}",
                        "",
                        "",
                        MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[5, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[6, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[7, i].Value.ToString(), 0.0)));
                    list.Add(string.Format(""));
                }
                else
                {
                    list.Add(string.Format("{0,5} {1,5} {2,12:f4} {3,12:f4} {4,12:f4} {5,12:f4} {6,12:f4} {7,12:f4}",
                        MyList.StringToDouble(dgv[0, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[1, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[5, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[6, i].Value.ToString(), 0.0),
                        MyList.StringToDouble(dgv[7, i].Value.ToString(), 0.0)));
                }
            }

            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            return list;
        }

        public List<string> Get_Truss_Forces_Data(DataGridView dgv, string stg)
        {
            List<string> list = new List<string>();
            if (dgv.RowCount == 0) return list;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------"));
            list.Add(string.Format("{0}TRUSS MEMBER ACTIONS", stg));
            list.Add(string.Format("----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format("MEMBER     LOAD       STRESS             FORCE"));
            list.Add(string.Format("----------------------------------------------"));

            for (int i = 0; i < dgv.RowCount; i++)
            {
                list.Add(string.Format("{0,-5} {1,8} {2,15:f4} {3,15:f4}",
                    MyList.StringToDouble(dgv[0, i].Value.ToString(), 0.0),
                    MyList.StringToDouble(dgv[1, i].Value.ToString(), 0.0),
                    MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0),
                    MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0)));

            }
            list.Add(string.Format("----------------------------------------------"));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            return list;
        }

        public List<string> Get_Cable_Forces_Data(DataGridView dgv, string stg)
        {
            List<string> list = new List<string>();
            if (dgv.RowCount == 0) return list;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format("{0}CABLE MEMBER ACTIONS", stg));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format("MEMBER     LOAD       STRESS             FORCE"));
            list.Add(string.Format("----------------------------------------------"));

            for (int i = 0; i < dgv.RowCount; i++)
            {
                list.Add(string.Format("{0,-5} {1,8} {2,15:f4} {3,15:f4}",
                    MyList.StringToDouble(dgv[0, i].Value.ToString(), 0.0),
                    MyList.StringToDouble(dgv[1, i].Value.ToString(), 0.0),
                    MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0),
                    MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0)));

            }
            list.Add(string.Format("----------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            return list;
        }

        public void Select_Steps()
        {
            List<int> Step_Lines = new List<int>();
           
            List<string> Items = new List<string>();

            List<string> analysis_list = new List<string>();
            #region analysis result list
            //analysis_list.Add(string.Format("User's data"));
            //analysis_list.Add(string.Format("JOINT COORDINATE"));
            //analysis_list.Add(string.Format("MEMBER INCIDENCES"));
            //analysis_list.Add(string.Format("JOINT COORD"));
            //analysis_list.Add(string.Format("MEMBER INCI"));
            //analysis_list.Add(string.Format("MEMB INCI"));
            //analysis_list.Add(string.Format("START GROUP DEFINITION"));
            //analysis_list.Add(string.Format("MEMBER PROPERTY"));
            //analysis_list.Add(string.Format("CONSTANT"));
            //analysis_list.Add(string.Format("SUPPORT"));
            ////analysis_list.Add(string.Format("LOAD"));
            //analysis_list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
            //analysis_list.Add(string.Format("LOAD GENERATION"));
            //analysis_list.Add(string.Format("C O N T R O L   I N F O R M A T I O N"));
            //analysis_list.Add(string.Format("NODAL POINT INPUT DATA"));
            //analysis_list.Add(string.Format("GENERATED NODAL DATA"));
            //analysis_list.Add(string.Format("EQUATION NUMBERS"));
            //analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            //analysis_list.Add(string.Format("TRUSS ELEMENT DATA"));
            //analysis_list.Add(string.Format("3 / D   B E A M   E L E M E N T S"));
            //analysis_list.Add(string.Format("MATERIAL PROPERTIES"));
            //analysis_list.Add(string.Format("BEAM GEOMETRIC PROPERTIES"));
            //analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            //analysis_list.Add(string.Format("3/D BEAM ELEMENT DATA"));
            //analysis_list.Add(string.Format("E Q U A T I O N   P A R A M E T E R S"));
            //analysis_list.Add(string.Format("N O D A L   L O A D S   (S T A T I C)   O R   M A S S E S   (D Y N A M I C)"));
            //analysis_list.Add(string.Format("N O D E   D I S P L A C E M E N T S / R O T A T I O N S"));
            //analysis_list.Add(string.Format("TRUSS MEMBER ACTIONS"));
            //analysis_list.Add(string.Format(".....BEAM FORCES AND MOMENTS"));
            //analysis_list.Add(string.Format("  SHELL ELEMENT STRESSES"));
            //analysis_list.Add(string.Format("....8 NODE SOLID ELEMENT DATA"));
            //analysis_list.Add(string.Format("  .....8-NODE SOLID ELEMENT STRESSES"));

            //analysis_list.Add(string.Format(" THIN  PLATE/SHELL  ELEMENT DATA"));
            //analysis_list.Add(string.Format(" T H I N   P L A T E / S H E L L   E L E M E N T S"));
            //analysis_list.Add(string.Format("S T A T I C   S O L U T I O N   T I M E   L O G"));
            //analysis_list.Add(string.Format("O V E R A L L   T I M E   L O G"));
            //analysis_list.Add(string.Format("SUMMARY OF MAXIMUM SUPPORT FORCES"));



            //analysis_list.Add(string.Format("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD"));
            //analysis_list.Add(string.Format("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD"));

            //analysis_list.Add(string.Format("T I M E   H I S T O R Y   R E S P O N S E"));
            //analysis_list.Add(string.Format("D I S P L A C E M E N T   T I M E   H I S T O R Y"));
            //analysis_list.Add(string.Format("F O R C E D   R E S P O N S E   A N A L Y S I S"));
            //analysis_list.Add(string.Format("E I G E N V A L U E   A N A L Y S I S"));




            #endregion


            List<string> list = new List<string>(rtb_results.Lines);
            //list.Sort();
            int indx = 0;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                //indx += item.Length + 1;
                indx += item.Length;
                if (item.ToUpper().StartsWith("STEP") ||
                   item.ToUpper().StartsWith("TABLE") ||
                   item.ToUpper().StartsWith("STAGE") ||
                    //item.ToUpper().StartsWith("DESIGN") ||
                    item.ToUpper().StartsWith("USER"))
                {
                    if (!Items.Contains(item))
                    {
                        Step_Lines.Add(i);
                        Items.Add(item);
                    }
                }
                else
                {
                    foreach (var l in analysis_list)
                    {
                        //if (item.ToUpper().Contains(l.ToUpper()))
                        if (item.Contains(l.ToUpper()))
                        {
                            if (!Items.Contains(item))
                            {
                                Step_Lines.Add(i);
                                Items.Add(item);
                                //hash_index.Add(Items.Count - 1, indx);
                            }
                        }
                    }
                }
            }
            list.Clear();
            lsv_steps.Items.Clear();
            foreach (var item in Items)
            {
                lsv_steps.Items.Add(item.Trim().TrimStart().ToString());
            }
            //if (lsv_steps.Items.Count > 0)
            //{
            //    lsv_steps.Items.RemoveAt(0);
            //    //cmb_step.SelectedIndex = 0;
            //}
        }
        private void select_text(string txt)
        {
            try
            {
                RichTextBox rtbData = rtb_results;
                int indx = rtbData.Find(txt);
                //if (hash_index[cmb_step.SelectedIndex] != null)
                if (indx != -1)
                {
                    //rtbData.SelectedText = cmb_step.Text;
                    //rtbData.Select((int)hash_index[cmb_step.SelectedIndex], cmb_step.Text.Length);
                    rtbData.Select(indx, txt.Length);
                    rtbData.ScrollToCaret();
                    //rtbData.SelectionBackColor = Color.Red;
                    rtbData.SelectionBackColor = Color.YellowGreen;

                    //rtbData.SelectionLength = cmb_step.Text.Length;

                    //Lines.Remove(cmb_step.Items[0].ToString());
                    //if (Lines.Contains(txt)) Lines.Remove(txt);
                    //Lines.Add(txt);
                    //Show_Next_Previous_Text();
                }
            }
            catch (Exception ex) { }
        }
        private void lstb_steps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsv_steps.SelectedItems.Count > 0)
            {
                select_text(lsv_steps.SelectedItems[0].Text.ToString());
                //CurrentPosition = Lines.Count - 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_ana_results.Name)
            {
                Write_Analysis_Result();
                iApp.Save_Form_Record(this, user_path);
            }
            else if (btn.Name == btn_view_results.Name)
            {
                if (File.Exists(Report_File))
                {
                    iApp.RunExe(Report_File);
                }
                else
                {
                    MessageBox.Show("Report File not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            iApp.StageNo = cmb_stage.SelectedIndex + 1;
            iApp.Stage_File = Get_Stage_File(iApp.StageNo);

            iApp.Save_Form_Record(this, user_path);
            this.Close();
        }

        private void chk_show_steps_CheckedChanged(object sender, EventArgs e)
        {
            spc_results.Panel1Collapsed = !chk_show_steps.Checked;

        }


        vdPolyline pln = null;

        private void btn_draw_diagram_Click(object sender, EventArgs e)
        {

            System.Collections.Hashtable hst = new System.Collections.Hashtable();
            System.Collections.Hashtable hst_stages = new System.Collections.Hashtable();

            string kStr1, kStr2;

            kStr1 = "";
            kStr2 = "";

            List<double> list = new List<double>();

            List<DataGridView> lst_grds = new List<DataGridView>();


            lst_grds.Add(dgv_st_1_trans);
            lst_grds.Add(dgv_st_2_trans);
            lst_grds.Add(dgv_st_3_trans);
            lst_grds.Add(dgv_st_4_trans);
            lst_grds.Add(dgv_st_5_trans);



            DataGridView dgv = dgv_st_1_trans;
            Button btn = sender as Button;

            if (btn != null) btn_draw_diagram.Tag = null;
            //if((Button)(sender) btn_draw_diagram)

            if (btn_draw_diagram.Tag == null)
            {
                cmb_diagram_jnt.Items.Clear();
                cmb_diagram_loadcase.Items.Clear();
                for (int stg = 0; stg < lst_grds.Count; stg++)
                {

                    dgv = lst_grds[stg];
                    hst = new System.Collections.Hashtable();

                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        kStr1 = dgv[0, i].Value.ToString() + "," + dgv[1, i].Value.ToString();

                        list = new List<double>();
                        list.Add(MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0));
                        list.Add(MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0));
                        list.Add(MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0));

                        hst.Add(kStr1, list);

                        if(!cmb_diagram_jnt.Items.Contains(dgv[0, i].Value.ToString()))
                        cmb_diagram_jnt.Items.Add(dgv[0, i].Value.ToString());

                        if (!cmb_diagram_loadcase.Items.Contains(dgv[1, i].Value.ToString()))
                            cmb_diagram_loadcase.Items.Add(dgv[1, i].Value.ToString());
                    }

                    hst_stages.Add(stg, hst);
                }
                btn_draw_diagram.Tag = hst_stages;

                if (cmb_diagram_jnt.Items.Count > 0) cmb_diagram_jnt.SelectedIndex = 0;
                if (cmb_diagram_loadcase.Items.Count > 0) cmb_diagram_loadcase.SelectedIndex = 0;

            }




            double v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;

            int rindx = 1;
            int cindx = 2;



            if (true)
            {
                hst_stages = btn_draw_diagram.Tag as System.Collections.Hashtable;


                kStr1 = cmb_diagram_jnt.Text + "," + cmb_diagram_loadcase.Text;

                hst = hst_stages[0] as System.Collections.Hashtable;

                dgv_rest.Rows.Clear();

                if (hst != null)
                {
                    list = hst[kStr1] as List<double>;

                    if (list != null)
                    {
                        v1 = list[rindx];

                        dgv_rest.Rows.Add(1, list[0], list[1], list[2]);
                    }
                }
                hst = hst_stages[1] as System.Collections.Hashtable;
                if (hst != null)
                {
                    list = hst[kStr1] as List<double>;

                    if (list != null)
                    {
                        v2 = list[rindx];
                        dgv_rest.Rows.Add(2, list[0], list[1], list[2]);
                    }
                }
                hst = hst_stages[2] as System.Collections.Hashtable;
                if (hst != null)
                {
                    list = hst[kStr1] as List<double>;

                    if (list != null)
                    {

                        v3 = list[rindx];
                        dgv_rest.Rows.Add(3, list[0], list[1], list[2]);
                    }
                }
                

                hst = hst_stages[3] as System.Collections.Hashtable;
                if (hst != null)
                {
                    list = hst[kStr1] as List<double>;

                    if (list != null)
                    {

                        v4 = list[rindx];
                        dgv_rest.Rows.Add(4, list[0], list[1], list[2]);
                    }
                }



                hst = hst_stages[4] as System.Collections.Hashtable;
                if (hst != null)
                {
                    list = hst[kStr1] as List<double>;

                    if (list != null)
                    {
                        v5 = list[rindx];
                        dgv_rest.Rows.Add(5, list[0], list[1], list[2]);
                    }
                }



                #region From Database

                 

                #endregion From Grid

            }
            else
            {
                #region From Grid
                dgv = dgv_st_1_trans;


                v1 = MyList.StringToDouble(dgv[cindx, rindx].Value.ToString(), 0.0);

                dgv = dgv_st_2_trans;

                v2 = MyList.StringToDouble(dgv[cindx, rindx].Value.ToString(), 0.0);

                dgv = dgv_st_3_trans;

                v3 = MyList.StringToDouble(dgv[cindx, rindx].Value.ToString(), 0.0);

                dgv = dgv_st_4_trans;

                v4 = MyList.StringToDouble(dgv[cindx, rindx].Value.ToString(), 0.0);

                dgv = dgv_st_5_trans;

                v5 = MyList.StringToDouble(dgv[cindx, rindx].Value.ToString(), 0.0);



                #endregion From Grid
            }

            Draw_Diagram();

        }

        private void Draw_Diagram()
        {
            int cindx = 1;
            if (rbtn_x_trans.Checked) cindx = 1;
            if (rbtn_y_trans.Checked) cindx = 2;
            if (rbtn_z_trans.Checked) cindx = 3;

            vdDocument vdoc = vdSC1.BaseControl.ActiveDocument;

            vdoc.ActiveLayOut.Entities.RemoveAll();
            //if (pln == null)
            //{
                pln = new vdPolyline();
                pln.SetUnRegisterDocument(vdoc);
                pln.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.Add(pln);
            //}


            pln.VertexList.RemoveAll();

            //double fact = 10E1;
            double fact = MyList.StringToDouble(comboBox1.Text, 10E3);



            double d = 0.0;



            List<double> x_values = new List<double>();
            List<double> y_values = new List<double>();
            for (int i = 0; i < dgv_rest.RowCount; i++)
            {
                d = MyList.StringToDouble(dgv_rest[cindx, i].Value.ToString(), 0.0);
                pln.VertexList.Add(new gPoint(5 * (i + 1), Math.Abs(d) * fact, 0));

                x_values.Add(5 * (i + 1));
                y_values.Add(Math.Abs(d) * fact);

            }
            pln.PenColor = new vdColor(Color.Red);

            if(chk_curve.Checked)
                pln.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
            else
                pln.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagSTANDARD;
            pln.Update();


            vdMText vtxt = new vdMText();

            for (int i = 0; i < x_values.Count; i++)
            {

                vtxt = new vdMText();

                vtxt.SetUnRegisterDocument(vdoc);
                vtxt.setDocumentDefaults();

                vtxt.InsertionPoint = new gPoint(x_values[i], y_values[i]);

                vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;

                vtxt.TextString = (y_values[i] / fact).ToString("E3");


                vtxt.Height = 0.3;

                vtxt.PenColor = new vdColor(Color.Cyan);

                vdoc.ActiveLayOut.Entities.Add(vtxt);
            }



            double x_max = 35;

            y_values.Sort();


            if (y_values.Count == 0) return;


            double y_max = y_values[y_values.Count - 1]*1.3;

            double y_min = y_values[0] ;

             y_min = y_min - (y_min * 0.5);


            #region X Axis

            vdLine vdln = new vdLine();
            vdln.SetUnRegisterDocument(vdoc);
            vdln.setDocumentDefaults();


            vdln.StartPoint = new gPoint(0, y_min);
            vdln.EndPoint = new gPoint(x_max, y_min);


            vdln.PenColor = new vdColor(Color.Green);

            vdoc.ActiveLayOut.Entities.Add(vdln);


            #endregion X Axis

            #region Y Axis

            vdln = new vdLine();
            vdln.SetUnRegisterDocument(vdoc);
            vdln.setDocumentDefaults();
            y_values.Sort();


            vdln.StartPoint = new gPoint(0, y_min);
            vdln.EndPoint = new gPoint(0, y_max);

            vdln.PenColor = new vdColor(Color.Green);
            vdoc.ActiveLayOut.Entities.Add(vdln);



            vtxt = new vdMText();

            vtxt.SetUnRegisterDocument(vdoc);
            vtxt.setDocumentDefaults();

            vtxt.InsertionPoint = new gPoint(-1.0,y_min + Math.Abs(y_min - y_max)/2.0);

            vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
            vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vtxt.Rotation = MyList.Convert_Degree_To_Radian(90);

             vtxt.TextString = ("DEFLECTIONS");
           

            vtxt.Height = 0.3;

            vtxt.PenColor = new vdColor(Color.Cyan);

            vdoc.ActiveLayOut.Entities.Add(vtxt);

            #endregion Y Axis





            for (int i = 0; i < x_values.Count; i++)
            {

                #region Draw Stage

                vtxt = new vdMText();
                vtxt.SetUnRegisterDocument(vdoc);
                vtxt.setDocumentDefaults();




                vtxt.InsertionPoint = new gPoint(x_values[i], y_min - 1.0);

                vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;

                //vtxt.Rotation = MyList.Convert_Degree_To_Radian(90.0);



                vtxt.TextString = "STAGE " + (i + 1);

                vtxt.PenColor = new vdColor(Color.Green);
                vtxt.Height = 0.5;



                vdoc.ActiveLayOut.Entities.Add(vtxt);

                #endregion Draw Stage
            }

            #region DRaw Inoformation

            vtxt = new vdMText();

            vtxt.SetUnRegisterDocument(vdoc);
            vtxt.setDocumentDefaults();

            vtxt.InsertionPoint = new gPoint(5, y_min - 3.0 );

            vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
            vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorLeft;

            //vtxt.TextString = "Diagram of X-Translation of Joint No  " + cmb_diagram_jnt.Text + " and Loadcase " + cmb_diagram_loadcase.Text;



            if (rbtn_x_trans.Checked)
                vtxt.TextString = "Diagram of X-Translations of Joint No  " + cmb_diagram_jnt.Text + " and Loadcase " + cmb_diagram_loadcase.Text;

            if (rbtn_y_trans.Checked)
                vtxt.TextString = "Diagram of Y-Translations of Joint No  " + cmb_diagram_jnt.Text + " and Loadcase " + cmb_diagram_loadcase.Text;

            if (rbtn_z_trans.Checked)
                vtxt.TextString = "Diagram of Z-Translations of Joint No  " + cmb_diagram_jnt.Text + " and Loadcase " + cmb_diagram_loadcase.Text;


            vtxt.Height = 0.6;

            vtxt.PenColor = new vdColor(Color.Cyan);

            vdoc.ActiveLayOut.Entities.Add(vtxt);


            #endregion DRaw Inoformation


            vdoc.Redraw(true);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(vdoc);



        }

        private void rbtn_x_trans_CheckedChanged(object sender, EventArgs e)
        {
            Draw_Diagram();
        }

        private void toolStripButtons_Click(object sender, EventArgs e)
        {
            vdDocument VD = vdSC1.BaseControl.ActiveDocument;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 1) VD = defDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name == tsb_3D_rotate.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name == tsb_VTop.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name == tsb_VBot.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBottom(VD);
            else if (tsb.Name == tsb_VLeft.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VLeft(VD);
            else if (tsb.Name == tsb_VRight.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VRight(VD);
            else if (tsb.Name == tsb_VFront.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(VD);
            else if (tsb.Name == tsb_VBack.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VD);
            else if (tsb.Name == tsb_VNE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINE(VD);
            else if (tsb.Name == tsb_VNW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINW(VD);
            else if (tsb.Name == tsb_VSE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISE(VD);
            else if (tsb.Name == tsb_VSW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISW(VD);
            else if (tsb.Name == tsb_ZoomA.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name == tsb_ZoomE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name == tsb_ZoomP.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name == tsb_ZoomW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name == tsb_ZoomIn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name == tsb_ZoomOut.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name == tsb_Pan.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name == tsb_ShadeOn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name == tsb_Wire.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(VD);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name == tsb_Save.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (iApp.IsDemo)
                        {
                            MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            if (VD.SaveAs(sfd.FileName))
                            {
                                MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else if (tsb.Name == tsb_Open.Name)
            {
                using (OpenFileDialog sfd = new OpenFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (iApp.IsDemo)
                        {
                            MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (VD.Open(sfd.FileName))
                            {
                                //MessageBox.Show("File Opened Successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else if (tsb.Name == tsb_Edit.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML Drawing File(*.vdml)|*.vdml|DXF Drawing File(*.dxf)|*.dxf|DWG Drawing File(*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (iApp.IsDemo)
                        {
                            MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //vdDocument VD = vdSC1.BaseControl.ActiveDocument;


                            VD.SaveAs(sfd.FileName);
                            //Drawing_File = sfd.FileName;
                            System.Environment.SetEnvironmentVariable("OPENFILE", sfd.FileName);
                            //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                            System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                        }
                    }
                }
            }
        }

        private void cmb_diagram_jnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw_Diagram();
        }

        private void chk_curve_CheckedChanged(object sender, EventArgs e)
        {
            if (pln != null)
            {
                if(chk_curve.Checked)
                    pln.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
                else
                    pln.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagSTANDARD;

            }
            pln.Update();

            vdSC1.BaseControl.ActiveDocument.Redraw(true);
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }





        #region Create Project / Open Project



        private void Open_Project()
        {
            #region Design Option

            try
            {
                iApp.Read_Form_Record(this, user_path);
                file_path = Path.Combine(user_path, txt_input_file.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option
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


        //public string user_path { get; set; }

        //const string Title = "DESIGN OF RE (Reinforced Earth) WALL";
        //public string Title
        //{
        //    get
        //    {
        //        if (iApp.DesignStandard == eDesignStandard.BritishStandard)
        //            return "DESIGN OF STEEL BEAMS [BS]";
        //        return "DESIGN OF STEEL BEAMS [IS]";
        //    }
        //}



        public string Working_Folder
        {
            get
            {
                // if (Directory.Exists(Path.Combine(user_path, Title)) == false)
                //    Directory.CreateDirectory(Path.Combine(user_path, Title));
                //return Path.Combine(user_path, Title);

                return user_path;

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
                    //txt_project_name.Text = Path.GetFileName(frm.Example_Path);
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
                //IsCreate_Data = true;
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
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
                return eASTRADesignType.Stage_Analysis;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion 

        private void btn_browse_input_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files (*.txt)|*.txt";

                if(ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    txt_input_file.Text = Path.GetFileName(ofd.FileName);
                    file_path = Path.Combine(user_path, txt_input_file.Text);
                    File.Copy(ofd.FileName, file_path, true);

                    if (File.Exists(file_path))
                    {
                        rtb_st_1_input.Lines = File.ReadAllLines(file_path);
                    }
                }
            }
            Button_Enable_Disable();
        }
    }
}
