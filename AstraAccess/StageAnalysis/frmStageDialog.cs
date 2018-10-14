using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

using AstraInterface.DataStructure;
using AstraInterface.Interface;

namespace AstraAccess.StageAnalysis
{
    public partial class frmStageDialog : Form
    {
        #region Properties
        
        public ProcessCollection ProcessList { get; set; }

        Thread thd = null;
        Process PRC = null;
        delegate void Add(DataGridView dgv);
        delegate void Clear(DataGridView dgv);

        IApplication iapp;
        #endregion Properties

        public frmStageDialog(ProcessCollection All_ProcessList, IApplication app)
        {
            InitializeComponent();
            ProcessList = All_ProcessList;
            iapp = app;
        }

        #region Thread Management

        Add add_data;
        Clear clr_data;
        public void Run_Thread()
        {
            Stop_Thread();
            add_data = new Add(Add_Text);
            clr_data = new Clear(Clear_All);
            //Control.CheckForIllegalCrossThreadCalls = true;
            thd = new Thread(new ThreadStart(Run_Process));
            thd.Start();
        }
        public void Run_Process()
        {
            try
            {
                //dgv_process.Invoke(clr_data, dgv_process);
                //string flpath = @"C:\Users\tes\Desktop\Software Testing\ASTRA\[2013 10 16]\TEST 02\ANALYSIS OF RCC T-GIRDER BRIDGE (LIMIT STATE METHOD)\Long Girder Analysis\Live Load Analysis\LiveLoad_Analysis_Input_File.txt";
                string flpath = @"";
                for (int i = 0; i < dgv_process.Rows.Count; i++)
                {
                    dgv_process.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dgv_process.Rows[i].Selected = false;
                }

                bool run = false;
                dgv_process.ReadOnly = true;
                chk_run_all.Enabled = false;

                for (int i = 0; i < ProcessList.Count; i++)
                {
                    run = (bool)dgv_process[1, i].Value;
                    flpath = ProcessList[i].Process_File_Name;

                    if (run)
                    {
                        dgv_process[3, i].Value = "Processing...";
                        dgv_process.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv_process.FirstDisplayedScrollingRowIndex = i;

                        //if (ProcessList[i].IS_Stage_File)
                        //{

                        //    if (File.Exists(ProcessList[i].Stage_File_Name))
                        //        Run_Exe(ProcessList[i].Stage_File_Name);

                        //    iapp.Write_Data_to_File(flpath, ProcessList[i].Stage_File_Name);

                        //    if (File.Exists(flpath))
                        //        Run_Data2(flpath);
                        //}
                        //else
                        //{
                        if (File.Exists(flpath))
                        {
                            Run_Exe(flpath, ProcessList.IsAst006);
                        }
                        //}


                        if (File.Exists(AstraInterface.DataStructure.MyList.Get_Analysis_Report_File(iapp.Stage_File)))
                        {
                            dgv_process.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                            dgv_process[3, i].Value = "Done";
                        }
                        else
                        {
                            //MessageBox.Show("This must be remembered that all the " + dgv_process.RowCount + " analyses results will be " +
                            //                "required in the design. So, for the first time all the " + dgv_process.RowCount + " analyses " +
                            //                "are to be processed.\nSo, do not remove any Check Mark for the first time. \n\nProcess Aborting....", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //dgv_process.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            //dgv_process[3, i].Value = "Not Done";
                            //Stop_Thread();
                            //break;
                        }
                    }
                    else
                    {
                        //if (!File.Exists(AstraInterface.DataStructure.MyList.Get_Analysis_Report_File(flpath)))
                        //{
                        //    MessageBox.Show("This must be remembered that all the " + dgv_process.RowCount + " analyses results will be " +
                        //                    "required in the design. So, for the first time all the " + dgv_process.RowCount + " analyses " +
                        //                    "are to be processed.\nSo, do not remove any Check Mark for the first time. \n\nProcess Aborting....", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    Stop_Thread();
                        //    break;

                        //}
                    }

                }
                chk_run_all.Enabled = true;
                dgv_process.ReadOnly = false;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { }
        }

        void Run_Exe(string flPath)
        {
            Run_Exe(flPath, false);
        }
        void Run_Exe(string flPath, bool IsAst006)
        {

            frm_ProcessStageAnalysis ff = new frm_ProcessStageAnalysis(iapp, flPath, true);

            ff.ShowDialog();


            //iapp.RunStageAnalysis(flPath).ShowDialog();
        }
        void Run_Exe1(string flPath, bool IsAst006)
        {
            PRC = new Process();

            iapp.Delete_Temporary_Files(flPath);

            string exe_file = Path.Combine(Application.StartupPath, "AST001.EXE");

            if (IsAst006) exe_file = Path.Combine(Application.StartupPath, "AST006.EXE");

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(Application.StartupPath, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            PRC.StartInfo.FileName = exe_file;
            PRC.Start();
            PRC.WaitForExit();
        }


        void Run_SkewFile(string flPath)
        {
            PRC = new Process();
            iapp.Delete_Temporary_Files(flPath);

            var ast05 = Path.Combine(Application.StartupPath, "AST005.EXE");
            var ast06 = Path.Combine(Application.StartupPath, "AST006.EXE");
            if (File.Exists(ast05))
            {
                if (File.Exists(ast06))
                    File.Delete(ast06);

                File.Move(ast05, ast06);
            }

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "RES001.TMP"), "");

            string exe_file = Path.Combine(Application.StartupPath, "AST001.EXE");


            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(Application.StartupPath, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            PRC.StartInfo.FileName = exe_file;
            PRC.Start();
            PRC.WaitForExit();

            if (File.Exists(ast06))
            {
                if (File.Exists(ast05)) File.Delete(ast05);
                File.Move(ast06, ast05);
            }
        }


        public bool Run_Data2(string flPath)
        {
            //if (Check_Demo(flPath)) return false;

            //iApp.Delete_Temporary_Files(flPath);
            try
            {

                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(iapp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
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

        public void Add_Text(DataGridView dgv)
        {
            dgv.Rows.Add(dgv.RowCount, true, "", "");
        }

        public void Clear_All(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }

        private void Stop_Thread()
        {

            if (thd != null)
            {
                try
                {
                    PRC.Kill();
                }
                catch (Exception exx) { }
                try
                {

                    if (thd.IsAlive)
                        thd.Abort();
                    dgv_process.ReadOnly = false;
                    chk_run_all.Enabled = true;
                }
                catch (Exception ex) { }


            }
        }

        #endregion Thread Management

        #region Windows Form Events
        private void frmStageDialog_Load(object sender, EventArgs e)
        {
            if (ProcessList.Count > 0)
            {
                dgv_process.Rows.Clear();

                for (int i = 0; i < ProcessList.Count; i++)
                {
                    var item = ProcessList[i];

                    item.Serial_No = i + 1;
                    dgv_process.Rows.Add(item.Serial_No, item.IS_RUN, item.Process_Text, item.Remark);
                }
            }
            chk_run_all.Checked = true;
            btn_process.Focus();
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            if (btn_process.Text.ToUpper() == "PROCESS")
            {
                Run_Thread();
                btn_process.Text = "Stop";
            }
            else if (btn_process.Text.ToUpper() == "STOP")
            {
                Stop_Thread();
                btn_process.Text = "Process";
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel the Process ?", "ASTRA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Stop_Thread();
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }


        private void chk_run_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_process.RowCount; i++)
            {
                try
                {
                    dgv_process[1, i].Value = chk_run_all.Checked;
                }
                catch (Exception ex) { }
            }
        }

        private void frm_LS_Process_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop_Thread();
        }
        #endregion Windows Form Events

        private void dgv_process_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iapp.View_Input_File(ProcessList[e.RowIndex].Process_File_Name);
           
        }
    }

}
