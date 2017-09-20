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

namespace AstraFunctionOne
{
    public partial class frmViewFile : Form
    {
        IApplication iApp;
        public frmViewFile(IApplication app)
        {
            InitializeComponent();
            iApp = app;

            File_Name = "";
            IsNew_Analysis = true;


        }

        public bool IsNew_Analysis { get; set; }
        public frmViewFile(IApplication app, string file_name)
        {
            InitializeComponent();
            File_Name = file_name;
            iApp = app;
            IsNew_Analysis = false;
        }
        string File_Name { get; set; }
        string Analysis_Path
        {
            get
            {
                if (!File.Exists(File_Name)) return "";

                return Path.GetDirectoryName(File_Name);
            }
        }
        string Analysis_Report
        {
            get
            {
                return Path.Combine(Analysis_Path, "ANALYSIS_REP.TXT");
            }
        }
        string LL_TXT
        {
            get
            {
                if (!Directory.Exists(Analysis_Path)) return "";
                return Path.Combine(Analysis_Path, "LL.TXT");
            }
        }

        string SAP_Data_File
        {
            get
            {
                if (!Directory.Exists(Analysis_Path)) return "";
                return Path.Combine(Analysis_Path, "SAP_Input_Data.TXT");
            }
        }

        string Member_Forces_File
        {
            get
            {
                if (!Directory.Exists(Analysis_Path)) return "";

                string fn = Path.Combine(Analysis_Path, "MemberForces.TXT");

                if(!File.Exists(fn))
                    fn = Path.Combine(Analysis_Path, "MemberForces.FIL");

                if(!File.Exists(fn))
                    fn = "";

                return fn;
            }
        }



        private void tsmi_Click(object sender, EventArgs e)
        {

            ToolStripItem tsi = sender as ToolStripItem;

            Control ctrl = sender as Control;

            if(ctrl != null && tsi == null)
            {
                if(ctrl.Name == btn_open_ll_editor.Name)
                {
                    tsi = tsb_ll_editor;
                }
            }
            

            if (tsi.Name == tsmi_open.Name || tsi.Name == tsb_open.Name)
            {
                using(OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text Files (*.txt)|*.txt|ASTRA Files (*.ast)|*.ast";
                    ofd.InitialDirectory = Analysis_Path;

                    if(ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        Open_File(ofd.FileName);
                    }
                }
            }
            else if (tsi.Name == tsmi_save.Name || tsi.Name == tsb_save.Name)
            {

                if (File.Exists(File_Name))
                {
                    if (rtb_input_file.Lines.Length > 0)
                    {
                        File.WriteAllLines(File_Name, rtb_input_file.Lines);
                    }
                    if (rtb_LL_TXT.Lines.Length > 0 && !splitContainer1.Panel2Collapsed)
                    {
                        File.WriteAllLines(LL_TXT, rtb_LL_TXT.Lines);
                        MessageBox.Show("\"" + Path.GetFileName(File_Name) + "\"" + " and " +
                           "\"" + Path.GetFileName(LL_TXT) + "\"" + " Saved Successfully.",
                          "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("Data file and Moving Load file Saved Successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("\"" + Path.GetFileName(File_Name) + "\"" + " Saved Successfully.",
                 "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    File_Save_As();
                }
          
            }
            else if (tsi.Name == tsmi_save_as.Name || tsi.Name == tsb_save_as.Name)
            {
                File_Save_As();
            }
            else if (tsi.Name == tsb_run.Name || tsi.Name == tsmi_run.Name)
            {
                if (iApp.Check_Demo_Version(true)) return;

                string flPath = File_Name;

                if (!File.Exists(flPath)) return;


                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                System.Diagnostics.Process prs = new System.Diagnostics.Process();

                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                if(Path.GetExtension(File_Name).ToUpper() == ".AST")
                    prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast003.exe");
                else
                    prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");

                //files.AddRange(Directory.GetFiles(WorkingFolder));
                iApp.Delete_Temporary_Files(flPath);

                if (prs.Start())
                    prs.WaitForExit();

                Open_File(File_Name);


                if (File.Exists(Analysis_Report))
                {
                    if (MessageBox.Show("Do you want to open Analysis Report file ?", "ASTRA",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        iApp.View_Result(Analysis_Report);
                }


            }
            else if (tsi.Name == tsb_stage_analysis.Name || tsi.Name == tsmi_stage_analysis.Name)
            {

                //iApp.WorkingFile = File_Name;
                //frmStageAnalysis p_d_analysis = new frmStageAnalysis(iApp);
               
                //p_d_analysis.ShowDialog();

                iApp.Form_Stage_Analysis(File_Name).Show();

            }
            else if (tsi.Name == tsb_ana_results.Name)
            {
                if (File.Exists(File_Name))
                    iApp.OpenWork(File_Name, false);
            }
            else if (tsi.Name == tsb_view_structure.Name || tsi.Name == tsmi_view_structure.Name)
            {
                if (File.Exists(File_Name))
                {
                    iApp.Form_ASTRA_TEXT_Data(File_Name, false).ShowDialog();
                    //iApp.Form_ASTRA_Analysis_Process()
                    rtb_input_file.Lines = File.ReadAllLines(File_Name);
                }

            }
            else if (tsi.Name == tsmi_view_structure.Name || tsi.Name == tsb_ana_inputs.Name)
            {
                iApp.Form_ASTRA_Input_Data(File_Name, false).ShowDialog();
                rtb_input_file.Lines = File.ReadAllLines(File_Name);
            }
            else if (tsi.Name == tsmi_report.Name || tsi.Name == tsb_open_report.Name)
            {
                if (File.Exists(Analysis_Report))
                {
                    iApp.View_Result(Analysis_Report);
                }
                //System.Diagnostics.Process.Start(Analysis_Report);
            }
            else if (tsi.Name == tsmi_SAP.Name || tsi.Name == tsb_sap.Name || tsi.Name == tsb_ana_sap.Name)
            {
                if (File.Exists(SAP_Data_File))
                {
                    iApp.View_SAP_Data(SAP_Data_File);
                    //System.Diagnostics.Process.Start(SAP_Data_File);
                }
            }
            else if (tsi.Name == tsmi_forces.Name || tsi.Name == tsb_forces.Name)
            {
                if (File.Exists(Member_Forces_File))
                    System.Diagnostics.Process.Start(Member_Forces_File);
            }
            else if (tsi.Name == tsb_open_folder.Name)
            {
                if (Directory.Exists(Analysis_Path))
                    System.Diagnostics.Process.Start(Analysis_Path);
            }
            else if (tsi.Name == tsb_ll_editor.Name || tsi.Name == tsmi_open_LL_editor.Name)
            {
                string ll_file =  MyList.Get_LL_TXT_File(File_Name);

                if (File.Exists(ll_file))
                {
                    frmMovingLoadData fmld = new frmMovingLoadData(iApp, ll_file);
                    fmld.Owner = this;
                    if (fmld.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        rtb_LL_TXT.Lines = File.ReadAllLines(ll_file);
                }
            }
            else if (tsi.Name == tsmi_close.Name) this.Close();
        }

        private void File_Save_As()
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files (*.txt)|*.txt";
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    //rtb_input_file.SaveFile(sfd.FileName);
                    if (rtb_input_file.Lines.Length > 0)
                        File.WriteAllLines(sfd.FileName, rtb_input_file.Lines);
                    if (rtb_LL_TXT.Lines.Length > 0)
                        File.WriteAllLines(MyList.Get_LL_TXT_File(sfd.FileName), rtb_LL_TXT.Lines);

                    Open_File(sfd.FileName);
                    //System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        private void frmViewFile_Load(object sender, EventArgs e)
        {
            if(File.Exists(File_Name))
            {
                Open_File(File_Name);
            }
            else
            {
                Menu_Enable_Disabled();

                List<string> list = new List<string>();
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("ASTRA FLOOR PROJECT TITLE"));
                list.Add(string.Format("UNIT KN METRES"));
                list.Add(string.Format("JOINT COORDINATES"));
                list.Add(string.Format(""));
                list.Add(string.Format("MEMBER INCIDENCES"));
                list.Add(string.Format(""));
                list.Add(string.Format("MEMBER PROPERTY"));
                list.Add(string.Format(""));
                list.Add(string.Format("MATERIAL CONSTANT"));
                list.Add(string.Format("E CONCRETE ALL"));
                list.Add(string.Format("DEN CONCRETE ALL"));
                list.Add(string.Format("PR CONCRETE ALL"));
                list.Add(string.Format("SUPPORTS"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 1 SAMPLE_LOAD"));
                list.Add(string.Format("JOINT LOAD"));
                list.Add(string.Format("MEMBER LOAD"));
                list.Add(string.Format("PERFORM ANALYSIS"));
                list.Add(string.Format("FINISH"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                rtb_input_file.Lines = list.ToArray();
            }
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                //list.Add(string.Format("TYPE 1 IRCCLASSA (Define Load Name with type no)"));
                //list.Add(string.Format("2.7 2.7 11.4 11.4 6.8 6.8 6.8 6.8   (The individual Wheel Loads)"));
                //list.Add(string.Format("1.1 3.2   1.2   4.3 3.0 3.0 3.0          (Wheel Load Distances)"));
                //list.Add(string.Format("1.800  (Load Width)"));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                lbl_examples.Text = "TYPE 1 HB_30_6       (Define Load Name with type no)" + "\n\r" +
                                    "30.0   30.0   30.0   30.0   (The individual Wheel Loads)" + "\n\r" +
                                    "1.8   6.0   1.8     (Wheel Load Distances)" + "\n\r" +
                                    "1.000         (Load Width)";

                //TYPE 14 HB_30_6
                //30.00 30.00 30.00 30.00 
                //1.8 6.0 1.8 
                //1.000

            }
        }

        private void Menu_Enable_Disabled()
        {


            tsmi_run.Enabled = File.Exists(File_Name);
            tsmi_stage_analysis.Enabled = File.Exists(File_Name);
            tsmi_view_structure.Enabled = File.Exists(File_Name);
            tsmi_open_LL_editor.Enabled = File.Exists(File_Name);



            tsmi_view_structure.Enabled = tsmi_run.Enabled;
            tsb_stage_analysis.Enabled = (tsmi_run.Enabled && (Path.GetExtension(File_Name).ToUpper() != ".AST"));

            tsmi_report.Enabled = File.Exists(Analysis_Report);
            tsmi_SAP.Enabled = File.Exists(SAP_Data_File);
            tsmi_forces.Enabled = File.Exists(Member_Forces_File);
            tsmi_open_LL_editor.Enabled = File.Exists(MyList.Get_LL_TXT_File(File_Name));



            tsb_run.Enabled = tsmi_run.Enabled;
            tsb_view_structure.Enabled = tsmi_view_structure.Enabled;
            tsb_open_report.Enabled = tsmi_report.Enabled;
            tsb_sap.Enabled = tsmi_SAP.Enabled;
            tsb_forces.Enabled = tsmi_forces.Enabled;
            tsb_ll_editor.Enabled = tsmi_open_LL_editor.Enabled;

            //tsmi_run.Enabled = File.Exists(File_Name);

            tsmi_stage_analysis.Enabled = File.Exists(File_Name);
            tsmi_view_structure.Enabled = File.Exists(File_Name);
            tsmi_open_LL_editor.Enabled = File.Exists(File_Name);

            tsb_run.Enabled = tsmi_run.Enabled;
            tsb_stage_analysis.Enabled = tsmi_stage_analysis.Enabled;
            tsb_view_structure.Enabled = tsmi_view_structure.Enabled;
            tsb_ll_editor.Enabled = tsmi_open_LL_editor.Enabled;


        }

        public void Open_File(string file_name)
        {
            if (File_Name != file_name)
                File_Name = file_name;

            if (!File.Exists(File_Name))
            {
                MessageBox.Show("File not found!", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Text = "ASTRA Pro : [ " + MyList.Get_Modified_Path(File_Name) + " ]";

            //this.label1.Text = "Analysis Input File [ " + Path.GetFileName(File_Name) + " ]";


            rtb_input_file.Lines = File.ReadAllLines(File_Name);

            if (File.Exists(LL_TXT))
                rtb_LL_TXT.Lines = File.ReadAllLines(LL_TXT);
            else
                rtb_LL_TXT.Text = "";


            //splitContainer1.Panel2Collapsed = Path.GetFileName(File_Name).ToUpper().Contains("ANALYSIS_RESULT");

            //if (splitContainer1.Panel2Collapsed)
            this.label1.Text = "File Name : [ " + Path.GetFileName(File_Name) + " ]";

            if(!IsNew_Analysis)
            splitContainer1.Panel2Collapsed = !rtb_input_file.Text.Contains("DEFINE MOVING LOAD FILE LL.TXT");



            tsmi_run.Enabled = (rtb_input_file.Text.Contains("ASTRA") && rtb_input_file.Text.Contains("FINISH"));

            if (!tsmi_run.Enabled)
            {
                if (Path.GetExtension(File_Name).ToUpper() == ".AST")
                    tsmi_run.Enabled = true;
            }

            Menu_Enable_Disabled();

            //tsmi_run.Enabled = File.Exists(File_Name);
            //tsmi_stage_analysis.Enabled = File.Exists(File_Name);
            //tsmi_view_structure.Enabled = File.Exists(File_Name);
            //tsmi_open_LL_editor.Enabled = File.Exists(File_Name);



            //tsmi_view_structure.Enabled = tsmi_run.Enabled;
            //tsb_stage_analysis.Enabled = (tsmi_run.Enabled && (Path.GetExtension(File_Name).ToUpper() != ".AST"));

            //tsmi_report.Enabled = File.Exists(Analysis_Report);
            //tsmi_SAP.Enabled = File.Exists(SAP_Data_File);
            //tsmi_forces.Enabled = File.Exists(Member_Forces_File);
            //tsmi_open_LL_editor.Enabled = File.Exists(MyList.Get_LL_TXT_File(File_Name));



            //tsb_run.Enabled = tsmi_run.Enabled;
            //tsb_view_structure.Enabled = tsmi_view_structure.Enabled;
            //tsb_open_report.Enabled = tsmi_report.Enabled;
            //tsb_sap.Enabled = tsmi_SAP.Enabled;
            //tsb_forces.Enabled = tsmi_forces.Enabled;
            //tsb_ll_editor.Enabled = tsmi_open_LL_editor.Enabled;

            iApp.WorkingFile = File_Name;
        }
      
        private void rtb_input_file_TextChanged(object sender, EventArgs e)
        {
            if(!IsNew_Analysis)
            splitContainer1.Panel2Collapsed = !rtb_input_file.Text.Contains("DEFINE MOVING LOAD FILE LL.TXT");
            //rtb_input_file.ForeColor = Color.Blue;
        }

        private void tsmi_1_jc_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if(tsmi.Name == tsmi_1_jc.Name)
                rtb_input_file.Text = rtb_input_file.Text.Insert(rtb_input_file.SelectionStart, "JOINT COORDINATES");
            else if (tsmi.Name == tsmi_1_mi.Name)
                rtb_input_file.Text = rtb_input_file.Text.Insert(rtb_input_file.SelectionStart, "MEMBER INCIDENCES");

        }

        private void rtb_input_file_SelectionChanged(object sender, EventArgs e)
        {
            int index = rtb_input_file.SelectionStart;
            int line = rtb_input_file.GetLineFromCharIndex(index);

            // Get the column.
            int firstChar = rtb_input_file.GetFirstCharIndexFromLine(line);
            int column = index - firstChar;
            //rtb_inputs.
            statusBar.Text = string.Format("Line : {0}, Column : {1}", line + 1, column + 1);
        }

    }
}
