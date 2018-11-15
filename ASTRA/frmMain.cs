using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using HeadsProgram;

using AstraAccess.SAP_Classes;

using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.SlabDesign;
using System.Threading;
using System.Diagnostics;
using AstraFunctionOne.BeamDesign;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.Design2;
using AstraFunctionOne.BridgeDesign.LongGirder;
using AstraFunctionOne.BridgeDesign.Design3;
using AstraFunctionOne.BridgeDesign.Abutment;
using AstraFunctionOne.BridgeDesign.Design5;
using AstraFunctionOne.BridgeDesign.Design6;
using AstraFunctionOne.BridgeDesign.RailBridge;
using AstraFunctionOne.CulvertDesign.BoxCulvert;
using AstraFunctionOne.CulvertDesign.SlabCulvert;
using AstraFunctionOne.CulvertDesign.PipeCulvert;
using AstraFunctionOne.UnderPass;
using AstraFunctionOne.BridgeDesign.Prestressed;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign.Foundation;
using AstraFunctionOne.ColumnDesign;
using AstraFunctionOne.BridgeDesign.Piers;
using AstraFunctionOne.Footing;
using TechSOFTActivator;

using BridgeAnalysisDesign;
using BridgeAnalysisDesign.RCC_T_Girder;
using LimitStateMethod.CableStayed;
using LimitStateMethod.Extradossed;
using LimitStateMethod.RCC_T_Girder;
using LimitStateMethod.PSC_I_Girder;
using LimitStateMethod.SubStructure;
using LimitStateMethod.Steel_Truss;
using LimitStateMethod.JettyDesign;
using BridgeAnalysisDesign.RE_Wall;

using ASTRAStructures;

namespace AstraFunctionOne
{
    public partial class frmMain : Form, IApplication
    {
        string msg = "";

        string flPath = "";
        bool isNew = false;
        bool isSave = false;
        IDocument AppDoc = new CAstraDoc();
        string currentDirectory = "";
        string chkFilePath = "";
        FileInfo fl;
        Thread thd;
        CRecentFiles recent;
        string sFromFileClick;

        DesignDrawings des_draw;

        public bool Is_select_Design_Standard = false;

        //Chiranjit [2012 07 20]
        public bool IsAASTHO { get; set; }
        public bool IsDemo { get; set; }
        public bool Is_BridgeDemo { get; set; }
        public bool Is_StructureDemo { get; set; }

        public string TEXT_File { get; set; }
        public string Drawing_File { get; set; }
        public string SAP_File { get; set; }

        public string EXAMPLE_File { get; set; }




        public eVersionType Version_Type { get; set; }


        bool IsRelease_22 = false;
        public frmMain()
        {
            InitializeComponent();
            //FileInfo fl = new FileInfo("ASTRA.exe");
            currentDirectory = Application.StartupPath;
            LastDesignWorkingFolder = "";

            Version_Type = eVersionType.Enterprise_Bridge;

            Drawing_File = "";
            user_path = "";

            Timer_Interval = 1999;

            Stage_File = "";
            StageNo = 0;
        }
        public string LastDesignWorkingFolder { get; set; }
        public bool Check_Demo_Version()
        {
            if (IsDemo)
            {
                //frm_Demo_Message f = new frm_Demo_Message(this);
                //f.Owner = this;
                //f.ShowDialog();

                frm_Demo_Design f = new frm_Demo_Design(this);
                f.Owner = this;
                f.ShowDialog();

                return true;
            }

            return false;
        }

        public bool Check_Demo_Version(bool IsAnalysis)
        {
            if (IsDemo)
            {
                if (IsAnalysis)
                {

                    frm_Demo_Analysis f = new frm_Demo_Analysis(this);
                    f.Owner = this;
                    f.ShowDialog();
                }
                else
                {
                    frm_Demo_Message f = new frm_Demo_Message(this);
                    f.Owner = this;
                    f.ShowDialog();

                    //frm_Demo_Design f = new frm_Demo_Design(this);
                    //f.Owner = this;
                    //f.ShowDialog();
                }

                return true;
            }

            return false;
        }

        public void WriteFilePath()
        {
            try
            {
                FileInfo fl = new FileInfo(flPath);
                FileStream fis = new FileStream(Path.Combine(AppFolder, "PAT001.tmp"), FileMode.Create);
                StreamWriter sw = new StreamWriter(fis);
                sw.WriteLine(fl.DirectoryName + "\\");
                sw.Flush();
                sw.Close();
                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                //System.Environment.SetEnvironmentVariable("ASTRA", flPath);
                SET_ENV();
            }
            catch (Exception ex) { }
        }
        void DeleteTmpFiles(string strPath)
        {
            if (strPath.Trim() != "" && Directory.Exists(strPath))
            {
                string[] arrfiles = Directory.GetFiles(strPath, "*.TMP");
                foreach (string strFile in arrfiles)
                {
                    this.DeleteFileIfExists(strFile);
                }
                arrfiles = Directory.GetFiles(strPath, "*.plt");
                foreach (string strFile in arrfiles)
                {
                    this.DeleteFileIfExists(strFile);
                }
            }
        }
        public bool DeleteFileIfExists(string strFile)
        {
            bool bSuccess = false;
            if (System.IO.File.Exists(strFile))
            {
                try
                {
                    System.IO.File.Delete(strFile);
                    bSuccess = true;
                }
                catch
                {
                    //Do nothing
                }
            }
            return bSuccess;
        }
        public DesignDrawings Des_Drawings
        {
            get
            {
                return (des_draw != null) ? des_draw : (new DesignDrawings(Path.Combine(Application.StartupPath, "DRAWINGS\\Drawing_Path.txt")));
            }
        }
        public string ASTRA_Pro { get; set; }
        public string FilePath
        {
            get
            {
                return flPath;
            }
            set
            {
                flPath = value;

                //if (LastDesignWorkingFolder == "")
                //LastDesignWorkingFolder = WorkingFolder;

                string tst = flPath;


                try
                {
                    string tst1 = Path.GetFileName(tst);
                    string tst2 = Path.GetFileName(Path.GetDirectoryName(tst));
                    string tst21 = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(tst)));
                    string tst3 = Path.GetPathRoot(tst);
                    if (tst21 != "")
                        tst = tst3 + "....\\" + Path.Combine(tst2, tst1);
                }
                catch (Exception ex) { }

                //this.Text = "ASTRA Pro" + (IsDemo ? " [DEMO] " : "") + " [ File Name : \"" + tst + "\" ]";
                //ASTRA_Pro = " [ File Name : \"" + tst + "\" ]";

                //this.Text = "ASTRA [" + flPath + "]";
            }
        }
        private bool OpenTXT()
        {

            ofdAst.Filter = "TXT Files|*.txt|All Files|*.*";
            if (ofdAst.ShowDialog() != DialogResult.Cancel)
            {
                OpenWork(ofdAst.FileName, false);
                return true;
            }
            return false;

        }
        private bool OpenAST()
        {
            ofdAst.Filter = "AST Files|*.ast|All Files|*.*";
            if (ofdAst.ShowDialog() != DialogResult.Cancel)
            {

                OpenWork(ofdAst.FileName, false);

                return true;
            }
            return false;
        }
        public void OpenWork(string opnfileName, bool IsOpenWithMovingLoad)
        {

            if (IsOpenWithMovingLoad)
            {
                //View_PostProcess(opnfileName);
                View_MovingLoad(opnfileName);
            }
            else
                View_ASTRAGUI(eProcessType.TextProcess, opnfileName);
            return;
            string rad_file = Path.Combine(Path.GetDirectoryName(opnfileName), "radius.fil");
            if (File.Exists(rad_file)) File.Delete(rad_file);
            Environment.SetEnvironmentVariable("COMP_RAD", "");

            SetApp_Structure(opnfileName, IsOpenWithMovingLoad);
            //if (IsOpenWithMovingLoad)
            //{
            //    Form f = Form_ASTRA_Moving_Load(opnfileName);
            //    f.Owner = this;
            //    f.Show();
            //}
            //else
            //{
            //    SetApp_Structure(opnfileName, IsOpenWithMovingLoad);
            //}
            return;
            ////if (!File.Exists(Path.Combine(Application.StartupPath, "AKSHASP.dll")))
            ////    Application.Exit();

            //if (Path.GetFileName(opnfileName).ToUpper() == "ASTRA_DATA_INPUT.TXT")
            //{
            //    MyList mlist = new MyList(File.ReadAllLines(opnfileName)[0].ToUpper(), ':');
            //    string tst = Path.GetDirectoryName(opnfileName);
            //    switch (mlist.StringList[1])
            //    {
            //        case "FRM_RCC_TBEAM":
            //            this.LastDesignWorkingFolder = Path.GetDirectoryName(tst);
            //            this.DesignStandard = mlist.StringList[2].Contains("INDIAN") ? eDesignStandard.IndianStandard : eDesignStandard.BritishStandard;
            //            SetWorkingFolder(this.LastDesignWorkingFolder, this.DesignStandard);
            //            Show_T_Girder_Bridge_Working_Stress();
            //            return;
            //        case "FRM_RCC_TBEAM1":
            //            this.LastDesignWorkingFolder = Path.GetDirectoryName(tst);
            //            Show_T_Girder_Bridge_Working_Stress();
            //            return;
            //    }
            //}

            this.FilePath = opnfileName;
            fl = new FileInfo(opnfileName.ToLower());

            if (fl.Extension == ".ast")
            {
                AppDoc.ClearVars();
                isNew = false;
                this.FilePath = flPath;
                setControlEnable();
                try
                {
                    this.AppDoc.Read(flPath);
                }
                catch (Exception exx)
                {
                    MessageBox.Show(this, "The file is not in correct format.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    AppDoc.ClearVars();
                }
                isSave = true;
            }
            else if (fl.Extension == ".txt")
            {
                AppDoc.ClearVars();
                isNew = false;
                this.FilePath = flPath;
                setControlEnable();
                AppDoc.ClearVars();
                isSave = true;
            }
            SetRecentFiles();
            //Chiranjit 18/06/2009
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(this.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");


            //System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            //if (MessageBox.Show("Do you want to open the Structure View ?",
            //    "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            SetApp_Structure(flPath, IsOpenWithMovingLoad);


            SET_ENV();
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            File.WriteAllText(Path.Combine(AppFolder, "hds.001"), opnfileName);
            File.WriteAllText(Path.Combine(AppFolder, "hds.002"), Path.GetDirectoryName(opnfileName) + "\\");
        }



        public void OpenWork(string opnfileName, bool IsOpenWithMovingLoad, string caption)
        {
            ASTRA_Analysis_Process(opnfileName, caption);
        }



        private void SetRecentFiles()
        {
            recent.Add(flPath);
            recent.SetFilesToMenu(ref recentFilesToolStripMenuItem);
            SetEventHandler();
        }
        public string FromFileClick
        {
            get
            {
                return sFromFileClick;
            }
            set
            {
                sFromFileClick = value;
            }
        }

        private void tsmi_open_txt_Click(object sender, EventArgs e)
        {
            //OpenWork();
            if (OpenTXT())
            {
                WriteFilePath();
                recent.Add(flPath);
                recent.SetFilesToMenu(ref recentFilesToolStripMenuItem);
                SetEventHandler();
            }
        }

        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowBasicInfoDialog(this);
        }

        private void nodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowNodalDataDialog(this);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowBeamMembersDialog(this);
        }

        private void nodalLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowMemberBeamLoadDialog(this);
        }

        private void SaveWork()
        {
            if (!isSave)
            {
                if (sfdAst.ShowDialog() != DialogResult.Cancel)
                {
                    AppDoc.Write(sfdAst.FileName);
                    this.FilePath = sfdAst.FileName;
                    isSave = true;
                }
            }
            else
            {
                AppDoc.Write(this.FilePath);
            }
            //System.Environment.SetEnvironmentVariable("ASTRA", flPath);
            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            SET_ENV();

            SetRecentFiles();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveWork();
            WriteFilePath();
        }
        private void NewWork()
        {
            isNew = true;
            isSave = false;
            flPath = "Input1.ast";
            File.Delete(flPath);
            this.FilePath = flPath;
            AppDoc.ClearVars();
            setControlEnable();
            //utilityToolStripMenuItem.Enabled = false;
            //cADViewerToolStripMenuItem.Enabled = false;

            //tsmi_Process_Design.Enabled = false;
        }
        private void tsmi_Create_New_AST_File_Click(object sender, EventArgs e)
        {
            NewWork();
        }

        private void setControlEnable()
        {
            string bk = flPath;
            flPath = flPath.ToLower();


            if (flPath.EndsWith(".ast") || flPath.EndsWith(".txt"))
            {
                ////convertToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem1.Enabled = true;
                saveAsToolStripMenuItem2.Enabled = true;
                //ClosetoolStipMenuItem.Enabled = true;
                utilityToolStripMenuItem.Enabled = true;
                viewToolStripMenuItem.Enabled = true;
                astDataInputToolStripMenuItem.Enabled = true;
                utilityToolStripMenuItem.Enabled = true;

                tsmi_CAD_Viewer.Enabled = true;

                //designToolStripMenuItem.Enabled = true;
                tsmi_process_analysis.Enabled = true;

                if (flPath.EndsWith(".ast"))
                {
                    //processAstFileToolStripMenuItem.Enabled = true;
                    //processTxtFileToolStripMenuItem.Enabled = false;

                    tsmi_viewInputData.Enabled = false;
                    tsmi_runTXTFile.Enabled = false;
                    tsmi_stage_ana.Enabled = false;
                    tsmi_convertToASTFile.Enabled = false;

                    tsmi_viewAnalysisASTFile.Enabled = true;
                    tsmi_processAnalysisASTFile.Enabled = true;


                }
                else if (flPath.EndsWith(".txt"))
                {

                    //processAstFileToolStripMenuItem.Enabled = false;
                    //processTxtFileToolStripMenuItem.Enabled = true;


                    tsmi_viewInputData.Enabled = true;
                    tsmi_runTXTFile.Enabled = true;
                    tsmi_stage_ana.Enabled = true;
                    tsmi_convertToASTFile.Enabled = true;

                    tsmi_viewAnalysisASTFile.Enabled = false;
                    tsmi_processAnalysisASTFile.Enabled = false;



                }

            }
            else
            {
                //convertToolStripMenuItem.Enabled = false;
                //saveToolStripMenuItem1.Enabled = false;
                //saveAsToolStripMenuItem2.Enabled = false;
                //ClosetoolStipMenuItem.Enabled = false;
                //utilityToolStripMenuItem.Enabled = false;
                //viewToolStripMenuItem.Enabled = false;
                //astDataInputToolStripMenuItem.Enabled = false;
                //utilityToolStripMenuItem.Enabled = false;

                //tsmi_CAD_Viewer.Enabled = false;
                //tsmi_Process_Design.Enabled = false;
            }
            flPath = bk;

            //tsmi_workingFolder.Enabled = File.Exists(flPath);
            tsmi_analysisInputDataFile.Enabled = File.Exists(flPath);
            tsmi_processAnalysisASTFile.Enabled = File.Exists(flPath);


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveAsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (sfdAst.ShowDialog() != DialogResult.Cancel)
            {
                //File.Copy(GlobalVars.filePath, sfdAst.FileName, true);
                AppDoc.Write(sfdAst.FileName);
                this.FilePath = sfdAst.FileName;
                //System.Environment.SetEnvironmentVariable("ASTRA", flPath);
                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                WriteFilePath();
                SET_ENV();

            }
        }
        public List<double> Bar_Dia
        {
            get
            {
                List<double> list = new List<double>();

                list.Add(12);
                list.Add(16);
                list.Add(20);
                list.Add(25);
                list.Add(32);

                return list;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //tsmi_structureModeling.Visible = false;
                //tsmi_microwaveTower.Visible = false;
                //tsmi_cableCarTower.Visible = false;


                //tsmi_PSC_I_GIRDER_Program.Visible = false;
                //tsmi_RCC_Culverts_LS.Visible = false;
                //tsmi_RCC_Culverts_WS.Visible = false;



                //tsmi_streamHydrology
                if (true)//true if ASTRA Pro Version 22 V1, false  if ASTRA Pro Version 22 V2
                {
                    //tsmi_Process_Design.DropDownItems.Remove(tsmi_streamHydrology);
                    //tsmi_Bridge_Design.DropDownItems.Insert(tsmi_Bridge_Design.DropDownItems.Count - 2, tsmi_streamHydrology);
                    //tsmi_structureModeling.Visible = false;
                    //tsmi_research_Studies.Visible = false;


                    mns_ASTRA_menues.Items.Remove(tsmi_research_Studies);
                    tsmi_research_Studies.DropDownItems.Remove(tsmi_streamHydrologyResearch);


                    tsmi_Process_Design.DropDownItems.Add(tsmi_research_Studies);
                    tsmi_Hydraulic_Calculations.DropDownItems.Add(tsmi_streamHydrology);


                    tsmi_process_analysis.DropDownItems.Add(tsmi_structureModeling);

                }

                //tsmi_workingFolder.Visible = false;
                tsmi_openAnalysisExampleTXTDataFile.Visible = true;

                //Chiranjit [2013 05 16]
                Progress_Works = new ProgressList();

                //Version_Type = eVersionType.High_Value_Version;

                IsAASTHO = true;
                //IsAASTHO = false;
                //bool IsDemo = true;
                DesignStandard = eDesignStandard.BritishStandard;
                Version_Type = LockProgram.Get_LockedVersion();

                IsDemo = (Version_Type == eVersionType.Demo);

                //IsAASTHO = false;

                this.Text = ASTRA_Pro;

                //Tables = new ASTRA_Tables(DesignStandard);

                //string ll_txt_file = Path.Combine(Application.StartupPath, @"LL.TXT");
                //Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);
                //if (File.Exists(ll_txt_file))
                //{
                //    LiveLoads = new LiveLoadCollections(ll_txt_file);
                //}
                Set_Bridge_Design_Menu();
                IsRelease_22 = true;
               
                    Load_ASTRA_R22_Menu();
                
                helpProvider1.HelpNamespace = Path.Combine(Application.StartupPath, "ASTRAHelp\\AstraPro.chm");
                //TechSOFT_Demo();
                try
                {
                    try
                    {
                        //This if Statement true for Professional Vertion
                        //This if Statement false for Professional Vertion
                        //
                        //string flls = Path.Combine(Application.StartupPath, "astra.pmt");
                        //if (File.Exists(flls))
                        //    File.Delete(flls);
                        IsAASTHO = !(TimeZone.CurrentTimeZone.StandardName.ToUpper().StartsWith("INDIA"));
                        if (IsAASTHO)
                            DesignStandard = eDesignStandard.BritishStandard;

                        Write_Default_Moving_Loads();


                        Tables = new ASTRA_Tables(DesignStandard);

                        if (LockProgram.CheckHasp())
                        {
                            LockProgram.Version_Type = Version_Type;
                            if (LockProgram.Version_Type == eVersionType.Activation_Trial || LockProgram.Version_Type == eVersionType.Demo)
                            {


                                //frm_Authorised_Message faum = new frm_Authorised_Message(this);

                                //faum.ShowDialog();

                                //File.WriteAllText(Path.Combine(Application.StartupPath, "astra.pmt"), "XYZ_123");
                                System.Environment.SetEnvironmentVariable("ASTRA_VERSION", "PRO");
                                frmLockedVersion chHasp = new frmLockedVersion(false);
                                tsmi_license.Visible = !chHasp.IsActivate;
                                //IsAASTHO = chHasp.Is_IsAASHTO();
                                //if (IsAASTHO)
                                //    DesignStandard = eDesignStandard.BritishStandard;
                                toolStripSeparator9.Visible = !chHasp.IsActivate;

                                //if (!LockProgram.IsActivate && LockProgram.Check_ASTRA_Lock() || LockProgram.Check_ASTRA_Lock_18())
                                //if (!LockProgram.IsActivate && LockProgram.Check_ASTRA_Lock() || LockProgram.Check_Previuos_Version())
                                //if (Version_Type == eVersionType.Demo || Version_Type == eVersionType.Activation_Trial)
                                if (Version_Type == eVersionType.Demo)
                                {
                                    //faum.ShowDialog();

                                    if (chHasp.ShowDialog() == DialogResult.OK)
                                    {
                                        tsmi_license.Visible = !chHasp.IsActivate;
                                        toolStripSeparator9.Visible = !chHasp.IsActivate;
                                    }
                                }
                                else
                                {
                                    chHasp.activation = LockProgram.Get_Activation();
                                    //if (chHasp.activation > 0)
                                    //{
                                    //    LockProgram.Set_Activation();
                                    //}

                                    if (chHasp.activation <= 5)
                                    {
                                        //throw new Exception("No more activation left.");
                                        //faum.ShowDialog();
                                        if (chHasp.ShowDialog() == DialogResult.OK)
                                        {
                                            tsmi_license.Visible = !chHasp.IsActivate;
                                            toolStripSeparator9.Visible = !chHasp.IsActivate;
                                        }
                                    }
                                    else
                                    {
                                        LockProgram.Set_Activation();
                                    }
                                   
                                }
                                //if (!chHasp.IsActivate)
                                //{
                                //if (chHasp.activation <= 0)
                                //    throw new Exception("No more activation left.");
                                //}
                            }
                            //Chiranjit [2012 01 16]
                            else
                            {
                                //System.Environment.SetEnvironmentVariable("ASTRA_VERSION", "DEMO");
                                //tsmi_license.Visible = false;
                                //toolStripSeparator9.Visible = false;
                                //Chiranjit [2012 01 16]
                                //TechSOFT_Demo();
                                //throw new Exception("ASTRA Lock not found at any port of this computer.");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    //Application.Exit();
                }
                //SetImages();
                frmSpash spalsh = new frmSpash();
                spalsh.ShowDialog();
                recent = new CRecentFiles(Path.Combine(AppFolder, "recent.rcnt"));
                recent.ReadFileNames();
                recent.SetFilesToMenu(ref recentFilesToolStripMenuItem);
                SetEventHandler();


                if (FromFileClick != "")
                {
                    OpenWork(FromFileClick, false);

                }
                Check_Demo_Version(false);
            }
            catch (Exception ex)
            {
                //Application.Exit();
            }

            //MessageBox.Show(this, "For Limit State Design the system must have Microsoft Excel 2007 or Newer Version installed.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                if (IsDemo)
                {
                    Last_version = eVersionType.Demo;
                    //this.BackgroundImage = Properties.Resources.ASTRA_Demo;
                    this.BackgroundImage = Properties.Resources.ASTRA_Unauthorized;
                }
                else
                {
                    if (Version_Type == eVersionType.Enterprise_Bridge)
                    {
                        Last_version = eVersionType.Enterprise_Bridge;
                        this.BackgroundImage = Properties.Resources.ASTRA_Enterprise;
                    }
                    else
                    {
                        Last_version = eVersionType.Professional_Bridge;
                        this.BackgroundImage = Properties.Resources.ASTRA_Professional;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void supportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowSupportDialog(this);
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowSelfWeightDialog(this);
        }


        private void makeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.AppDoc.ProjectTitle);
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowAnalysisDialog(this);
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolbarToolStripMenuItem.Checked)
            {
                toolbarToolStripMenuItem.Checked = false;
                toolStrip1.Visible = false;
            }
            else
            {
                toolbarToolStripMenuItem.Checked = true;
                toolStrip1.Visible = true;
            }
            this.Refresh();
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked)
            {
                statusBarToolStripMenuItem.Checked = false;
                statusStrip1.Visible = false;
            }
            else
            {
                statusBarToolStripMenuItem.Checked = true;
                statusStrip1.Visible = true;
            }
            this.Refresh();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAstraHelpAbout fAbout = new frmAstraHelpAbout();
            fAbout.ShowDialog();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fn = "";

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Name == tsmi_contents.Name)
                fn = Path.Combine(Application.StartupPath, @"AstraHelp\ASTRAPro.chm");
            else if (tsmi.Name == tsmi_contents_supplement.Name)
                fn = Path.Combine(Application.StartupPath, @"AstraHelp\Contents_Suppliment.pdf");
            else if (tsmi.Name == tsmi_sap_manual.Name)
                fn = Path.Combine(Application.StartupPath, @"AstraHelp\SAP Input Data Manual.pdf");
            try
            {
                System.Diagnostics.Process.Start(fn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, Path.GetFileName(fn) + " file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetImages()
        {
            try
            {
                System.Drawing.Icon ic = new Icon(Path.Combine(AppFolder, "Icons\\newFile.ico"));
                tsmi_Create_New_AST_File.Image = ic.ToBitmap();
                tsbtnNewFile.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\OpenFile.ico"));
                tsmi_open_txt.Image = ic.ToBitmap();
                tsbtnOpenFile.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\SaveFile.ico"));
                saveToolStripMenuItem1.Image = ic.ToBitmap();
                tsbtnSaveFile.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\cut.ico"));
                tsbtnCut.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\clipcopy.ico"));
                tsbtnCopy.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\paste.ico"));
                tsbtnPaste.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\Undo.ico"));
                tsbtnUndo.Image = ic.ToBitmap();

                ic = new Icon(Path.Combine(AppFolder, "Icons\\redo.ico"));
                tsbtnRedo.Image = ic.ToBitmap();
            }
            catch (Exception exx)
            {
            }
        }

        private void tsbtnNewFile_Click(object sender, EventArgs e)
        {
            NewWork();
        }

        private void tsbtnOpenFile_Click(object sender, EventArgs e)
        {
            //OpenWork();
        }

        private void tsbtnSaveFile_Click(object sender, EventArgs e)
        {
            SaveWork();
        }

        public string AppFolder
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }

        private void plateAndShellElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowPlateAndShellDialog(this);
        }

        private void runTXTDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkFilePath = Path.Combine(AppFolder, "AST001.exe");
            ProcessStartInfo startInfo = new ProcessStartInfo(chkFilePath);
            Process.Start(startInfo);
        }

        private void convertTXTDataFileToASTDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkFilePath = Path.Combine(AppFolder, "AST002.exe");
            if (File.Exists(chkFilePath))
                System.Diagnostics.Process.Start(chkFilePath);
            else
                MessageBox.Show(this, "AST002.exe" + " not found.");
        }

        private void createASTDataFileToStructureModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkFilePath = Path.Combine(AppFolder, "AST003.exe");
            if (File.Exists(chkFilePath))
                System.Diagnostics.Process.Start(chkFilePath);
            else
                MessageBox.Show(this, "AST003.exe" + " not found.");
        }

        private void runASTDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkFilePath = Path.Combine(AppFolder, "AST004.exe");
            if (File.Exists(chkFilePath))
                System.Diagnostics.Process.Start(chkFilePath);
            else
                MessageBox.Show(this, "AST004.exe" + " not found.");
        }

        private void beamAndSubFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cADViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", null);
            File.WriteAllText(EnvFilePath, "");
            this.RunViewer();
        }

        private void twoWaySLABToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rCCColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rCCFootingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsmi_open_ast_Click(object sender, EventArgs e)
        {
            if (OpenAST())
            {

                SetRecentFiles();
                WriteFilePath();
                //chkFilePath = Path.Combine(AppFolder, "Viewer.exe");
                //if (File.Exists(chkFilePath))
                //    System.Diagnostics.Process.Start(chkFilePath);
                //else
                //    MessageBox.Show(this, "Viewer.exe" + " not found.");
            }
        }

        private void processAstFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void processTXTDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void foldersAndOpenFiletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdAst.Filter = "All Files|*.*";
            string fName = "";
            if (ofdAst.ShowDialog() != DialogResult.Cancel)
            {
                fName = ofdAst.FileName;
            }
            try
            {
                System.Diagnostics.Process.Start(fName);
            }
            catch
            {
            }
        }

        private void createDesignFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runTXTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("SURVEY", FilePath);
            System.Environment.SetEnvironmentVariable("ASTRA", FilePath);
            WriteFilePath();
            RunExe("ast001.exe");
            SET_ENV();

            if (File.Exists(MyList.Get_Analysis_Report_File(WorkingFile)))
            {
                if (MessageBox.Show("Do you want to Open Analysis Report File ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    View_Result(MyList.Get_Analysis_Report_File(WorkingFile));
            }
        }
        public void Open_TextFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                System.Diagnostics.Process.Start(fileName);
            }
            else
            {
                MessageBox.Show(fileName + " file not found.", "ASTRA", MessageBoxButtons.OK);
            }
        }

        public void RunExe(string exeFileName)
        {
            if (exeFileName == "") return;
            try
            {
                if (File.Exists(FilePath))
                {
                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    File.WriteAllText(Path.Combine(AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    System.Environment.SetEnvironmentVariable("SURVEY", "");


                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    File.WriteAllText(Path.Combine(AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    System.Environment.SetEnvironmentVariable("SURVEY", FilePath);
                }

                string ss = System.Environment.GetEnvironmentVariable("SURVEY");
                if (!File.Exists(ss))
                {
                    System.Environment.SetEnvironmentVariable("SURVEY", FilePath);
                }

                if (!File.Exists(exeFileName))
                {
                    exeFileName = Path.Combine(Application.StartupPath, exeFileName);
                }
                run_file = exeFileName;
                if (Path.GetExtension(exeFileName).ToLower() == ".exe")
                {
                    if (Path.GetFileNameWithoutExtension(exeFileName).ToUpper() == "STREAMHYDROLOGY")
                    {
                        Environment.SetEnvironmentVariable("AP_WORK_DIR", LastDesignWorkingFolder);
                        run_file = Path.Combine(Application.StartupPath, "Stream Hydrology\\HydrologyWorkspace.exe");
                        System.Diagnostics.Process.Start(run_file);
                    }
                    else
                    {
                        RunThread();
                    }

                }
                else
                {
                    System.Diagnostics.Process.Start(exeFileName);
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show(exeFileName + " not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RunExe(string exeFileName, string env_file_path)
        {
            FilePath = env_file_path;
            RunExe(exeFileName);
        }



        string run_file = "";
        public void RunThread()
        {
            try
            {

                if (File.Exists(FilePath) == false)
                    System.Environment.SetEnvironmentVariable("SURVEY", LastDesignWorkingFolder);
                else
                    System.Environment.SetEnvironmentVariable("SURVEY", FilePath);

                System.Environment.SetEnvironmentVariable("ASTRA", FilePath);

                System.Diagnostics.Process prs = new Process();

                prs.StartInfo.FileName = run_file;
                try
                {
                    //prs.StartInfo.EnvironmentVariables.Add("SURVEY", FilePath);
                }
                finally
                {
                    if (prs.Start()) prs.WaitForExit();
                }
                //System.Diagnostics.Process.Start(run_file);
            }
            catch (Exception ex)
            {
            }
        }

        private void convertToASTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("SURVEY", FilePath);
            System.Environment.SetEnvironmentVariable("ASTRA", FilePath);
            RunExe("ast002.exe");
            SET_ENV();

        }
        private void runASTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("SURVEY", FilePath);
            System.Environment.SetEnvironmentVariable("ASTRA", FilePath);
            RunExe("ast003.exe");
            SET_ENV();
        }

        private void basicInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowBasicInfoDialog(this);
        }

        private void aToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowAnalysisDialog(this);

        }

        private void nodesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowNodalDataDialog(this);

        }

        private void elementsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowBeamMembersDialog(this);

        }

        private void plateAndShellElementsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowPlateAndShellDialog(this);

        }

        private void supportToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowSupportDialog(this);

        }

        private void nodalLoadsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowMemberBeamLoadDialog(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.CAstraFunctionFactory.Instance.ShowSelfWeightDialog(this);
        }

        private void notePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("NotePad.exe");
            }
            catch
            {
            }
        }

        private void recentMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            MyList mlist = new MyList(tsmi.Text, ':');

            //string fn = recent.FileList[mlist.GetInt(0) - 1];
            string fn = tsmi.ToolTipText;

            //OpenWork(fn, false);
            //OpenWork(tsmi.Text, false);
            FilePath = fn;
            LastDesignWorkingFolder = Path.GetDirectoryName(fn);
            SetRecentFiles();
            Set_Bridge_Design_Menu();
            if (File.Exists(fn))
            {
                string sd = Path.GetFileNameWithoutExtension(fn);
                if (sd.ToUpper().Contains("SAP"))
                {
                    SAP_File = fn;
                    AstraAccess.ViewerFunctions.Form_SAP_Editor(this).Show();
                    //Form_ASTRA_SAP_Data(fn, false);
                }
                else if (Path.GetExtension(fn).ToLower() == ".vdml" ||
                    Path.GetExtension(fn).ToLower() == ".dwg" ||
                    Path.GetExtension(fn).ToLower() == ".dxf")
                {
                    FilePath = fn;
                    Drawing_File = fn;
                }
                else
                {
                    TEXT_File = fn;
                    View_Input_File(fn);
                }
            }


        }

        private void SetEventHandler()
        {
            foreach (ToolStripMenuItem tsmi in recentFilesToolStripMenuItem.DropDownItems)
            {
                tsmi.Click += new EventHandler(this.recentMenu_Click);
            }
        }


        private void explorerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(LastDesignWorkingFolder))
                System.Diagnostics.Process.Start("EXPLORER", "/e," + LastDesignWorkingFolder);
            else
            {
                if (File.Exists(flPath))
                    System.Diagnostics.Process.Start("EXPLORER", "/e," + WorkingFolder);
            }
        }


        private void sLAB02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rcc_Slab_01();
        }
        private void Rcc_Slab_01()
        {
            frmOneWaySlab frmSl_02 = new frmOneWaySlab(this);
            frmSl_02.Owner = this;
            frmSl_02.Show();
        }



        private void sLAB01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmSLAB01 fsl01 = new frmSLAB01(this);
            //fsl01.ShowDialog();
        }

        private void sLAB03ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOneWayContinuousSlab slb03 = new frmOneWayContinuousSlab(this);
            slb03.Owner = this;
            slb03.Show();
        }


        private void sLAB04ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTwoWayRCCSlab slab04 = new frmTwoWayRCCSlab(this);

            slab04.Owner = this;
            slab04.Show();
            //slab04.CalculateProgram("C:\\slab04.txt");
        }

        private void designReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bOXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBoxCulvert f_b_c = new frmBoxCulvert(this);
            f_b_c.Owner = this;
            f_b_c.Show();
        }

        private void pipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPipeCulvert f_p_c = new frmPipeCulvert(this);
            f_p_c.Owner = this;
            f_p_c.Show();
        }

        private void slabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSlabCulvert f_s_c = new frmSlabCulvert(this);
            f_s_c.Owner = this;
            f_s_c.Show();
        }

        private void tsmi_TBeam_Deckslab_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;



            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            frmDesignDeckSlab frm_deck = new frmDesignDeckSlab(this, 2);
            frm_deck.Owner = this;
            frm_deck.Show();
            //else if (tsmi.Name == tsmi_TBeam_deckslab2.Name)
            //{

            //    ShowTimerScreen(eASTRAImage.Titled_RCC_T_Beam_Bridge);
            //    frmDesignDeckSlab frm_deck = new frmDesignDeckSlab(this, 1);
            //    frm_deck.Owner = this;
            //    frm_deck.Show();
            //}

        }
        private void tsmi_TBeam_long_girder_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;


            //if (tsmi.Name == tsmi_TBeam_long_girder2.Name)
            //{
            //    ShowTimerScreen(eASTRAImage.Titled_RCC_T_Beam_Bridge);
            //    frmDesignLongitudinalGirder f_des_long_gir = new frmDesignLongitudinalGirder(this, 1);
            //    f_des_long_gir.Owner = this;
            //    f_des_long_gir.Show();
            //}

            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            frm_RccTBeam_Bottom_Flange f_cd = new frm_RccTBeam_Bottom_Flange(this);
            f_cd.Owner = this;
            f_cd.Show();
        }

        private void tsmi_TBeam_xgirder_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            //ShowTimerScreen(eASTRAImage.Titled_RCC_T_Beam_Bridge);
            //if (tsmi.Name == tsmi_TBeam_xgirder1.Name)
            //{
            //    ShowTimerScreen(eASTRAImage.TGirder_Bottom_Flange);
            //    frmDesignCrossGirder f_cg = new frmDesignCrossGirder(this, 2);
            //    f_cg.Owner = this;
            //    f_cg.Show();
            //}
            //else
            //{
            //    ShowTimerScreen(eASTRAImage.Titled_RCC_T_Beam_Bridge);
            //    frmDesignCrossGirder f_cg = new frmDesignCrossGirder(this, 1);
            //    f_cg.Owner = this;
            //    f_cg.Show();
            //}
        }
        private void tsmi_TBeam_cantilever_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            frmDesignCantileverSlab f_cant_slab = new frmDesignCantileverSlab(this, 2);
            f_cant_slab.Owner = this;
            f_cant_slab.Show();

        }

        private void reinforcementDetailsCrossGirdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void bRIDGEGENERALARRANGEMENTDRAWINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SetApp_Design_BEAM(Application.StartupPath, "BRIDGE_GENERAL_ARRANGEMENT_DRAWING");
            //ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            //SetApp_Design_BEAM(Application.StartupPath, tsmi.Text.Replace(" ", "_"));
        }


        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            frmCantileverRetainingWall f_d_abut = new frmCantileverRetainingWall(this);
            f_d_abut.Owner = this;
            f_d_abut.Show();
        }

        private void designOfBridgePiersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDesignBridgePiers f_bridge_pier = new frmDesignBridgePiers(this);
            f_bridge_pier.Owner = this;
            f_bridge_pier.Show();
        }

        private void cantileverRetainingWallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCantileverRetainingWall f_d_abut = new frmCantileverRetainingWall(this);
            f_d_abut.Owner = this;
            f_d_abut.Show();
        }

        private void counterfortRetainingWallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCounterfortRetainingWall f_d_abut = new frmCounterfortRetainingWall(this);
            f_d_abut.Owner = this;
            f_d_abut.Show();
        }

        private void compositBridgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.Composite_Bridge);
            frmCompositeBridge f_cb = new frmCompositeBridge(this);
            f_cb.Owner = this;
            f_cb.Show();
        }

        private void topRCCSlabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTopRCCSlab ft = new frmTopRCCSlab(this);
            ft.Owner = this;
            ft.Show();
        }

        private void rCCAbutmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRccAbutment ft = new frmRccAbutment(this);
            ft.Owner = this;
            ft.Show();
        }

        private void rCCBoxStructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRccBoxStructure ft = new frmRccBoxStructure(this);
            ft.Owner = this;
            ft.Show();
        }

        private void singleSpanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSimplySupportedSingleSpanBeam f_b1 = new frmSimplySupportedSingleSpanBeam(this);
            f_b1.Owner = this;
            f_b1.Show();
        }

        private void sTEELPLATEGIRDERRAILWAYBRIDGEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.Rail_Plate_Girder);
            frmSteel_Plate_Girder_Railway_Bridge f_spg = new frmSteel_Plate_Girder_Railway_Bridge(this);
            f_spg.Owner = this;
            f_spg.Show();
        }


        private void deckSlabSteelGirderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Short_Span);
            frmRccDeckSlab f_ds = new frmRccDeckSlab(this);
            f_ds.Owner = this;
            f_ds.Show();
        }

        private void sRAILWAYBRIDGEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prestressedPosttensionedGirderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Short_Span);
            //if (MessageBox.Show(msg, "ASTRA", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //{
            frmPreStressedBridgePostTension f_psb = new frmPreStressedBridgePostTension(this);
            f_psb.Owner = this;
            f_psb.Show();
            //}
        }

        private void hYDRAULICCALCULATIONSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHydraulicCalculations f_psb = new frmHydraulicCalculations(this);
            f_psb.Owner = this;
            f_psb.Show();
        }

        private void wELLFOUNDATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenWorkSheet_Design("Well Foundation Worksheet Design", @"Foundation\Well Foundation\WellFoundation_Design.xls", "Well_Foundation");

        }
        private void wf_2_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRccWellFoundation f_wf = new frmRccWellFoundation(this);
            f_wf.Owner = this;
            f_wf.Show();
        }
        private void pileFoundationtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPileFoundation f_pf = new frmPileFoundation(this);
            f_pf.Owner = this;
            f_pf.Show();
        }

        private void axiallyLoadedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRccColumnDesign f_cd = new frmRccColumnDesign(this);
            f_cd.Owner = this;
            f_cd.Show();
        }

        private void axialLoadBiAxialMomentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBiAxialBendingColumn f_bc = new frmBiAxialBendingColumn(this);
            f_bc.Owner = this;
            f_bc.Show();
        }

        private void isolatedFootingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmIsolatedFooting f_if = new frmIsolatedFooting(this);
            f_if.Owner = this;
            f_if.Show();
        }

        private void combinedFootingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmCombinedFooting f_cf = new frmCombinedFooting(this);
            BridgeAnalysisDesign.Footing.frm_CombinedFooting f_cf = new BridgeAnalysisDesign.Footing.frm_CombinedFooting(this);
            f_cf.Owner = this;
            f_cf.Show();



        }

        private void rectangularOrFlangeBeamBS8110ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RunExe("BEAM.EXE");
            //MessageBox.Show(this, "This Module is not included in this version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmRectangularBeam f_rb = new frmRectangularBeam(this);
            f_rb.Owner = this;
            f_rb.Show();
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(e.KeyChar ==     
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                try
                {
                    System.Diagnostics.Process.Start(Path.Combine(AppFolder, "AstraHelp\\ASTRAPro.chm"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "ASTRAPro.chm file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pDeltaAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmStageAnalysis p_d_analysis = new frmStageAnalysis(this);
            //p_d_analysis.ShowDialog();
            if (!File.Exists(this.WorkingFile))
            {
                MessageBox.Show(this, "Analysis Input Data file not seleceted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                //CAstraFunctionFactory.Instance.ShowStageAnalysisDialog(this);


                AstraAccess.ViewerFunctions.Form_Stage_Analysis(this).Show();
            }


            Set_Bridge_Design_Menu();

        }

        string path, path2, path3;

        private void TechSOFT_Demo()
        {

            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path2 = Environment.SystemDirectory;
            path3 = Application.StartupPath;

            //MessageBox.Show(path);
            TechSOFT ts = null;

            TechSOFT ts_chk = null;
            int act_chl_S = -1;
            int act_chl_T = -1;


            int act_S = -1;
            int act_T = -1;
            int old_act_T = -1;


            try
            {
                path = Path.Combine(path, "abtesiply4.dll");
                path2 = Path.Combine(path2, "msoff.dll");
                path3 = Path.Combine(path3, "abtesiply4.dll");

                //if (!File.Exists(Path.Combine(Application.StartupPath, "abtesiply4.sec")))
                //{
                //    Application.Exit();
                //}
                if (File.Exists(path) && File.Exists(path2))
                {
                    ts = (TechSOFT)TechSOFT.DeSerialize(path);
                    ts_chk = (TechSOFT)TechSOFT.DeSerialize(path2);


                    try
                    {
                        act_T = TechSOFT.Get_Int(ts.T);
                        act_chl_T = TechSOFT.Get_Int(ts_chk.T);
                    }
                    catch (Exception ex)
                    {
                        act_T = 0;
                    }
                    if (ts.S == "xxxyyy")
                    {
                        MessageBox.Show(this, "Total " + act_T + " activations are finished. Contact TechSOFT.\n\nEmail At: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        File.WriteAllText(Path.Combine(Application.StartupPath, "Message.txt"), "Total " + act_T + " activations are finished.");
                    }
                    else
                    {
                        act_S = TechSOFT.Get_Int(ts.S);
                        try
                        {
                            act_chl_S = TechSOFT.Get_Int(ts_chk.S);
                        }
                        catch (Exception ex) { act_chl_S = 0; }

                    }

                    if ((act_chl_T == act_T) && (act_chl_S < act_S) ||
                        ((act_chl_T != act_T)))
                    {
                        MessageBox.Show(this, "No more activations are left. Please Contact TechSOFT.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //throw new Exception("No more activations are left");
                        act_T = 0;
                        act_S = 0;
                        ts.S = "xxxyyy";
                    }


                    if (((act_T == 0) && (act_S == 0)) || (ts.S == "xxxyyy") || (act_S == 0))
                    {
                        old_act_T = act_T;
                        frmDemoVersion fch = new frmDemoVersion();
                        fch.ShowDialog();
                        if (fch.Autho_Code == "")
                        {
                            //MessageBox.Show(this, "This Authorization code is already used.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Application.Exit();
                        }
                        else if (ts.AuthorizationCode == fch.Autho_Code)
                        {
                            MessageBox.Show(this, "This Authorization code is already used.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception("This Authorization code is already used.");

                        }
                        else
                        {
                            try
                            {
                                ts.AuthorizationCode = fch.Autho_Code;
                                act_S = TechSOFT.Get_Int(ts.S);
                                act_T = TechSOFT.Get_Int(ts.T);

                                if (act_T < old_act_T || act_S > act_T)
                                    throw new Exception();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, "Invalid authorization code", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ts.T = TechSOFT.Get_String(old_act_T);
                                ts.S = "xxxyyy";
                                ts.Serialize(path);
                                //Application.Exit();
                            }
                            act_S = TechSOFT.Get_Int(ts.S);
                            act_T = TechSOFT.Get_Int(ts.T);
                        }
                    }
                    act_S = TechSOFT.Get_Int(ts.S);
                    if (act_S <= 10)
                        MessageBox.Show((act_S) + " more activation(s) are left.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    act_S--;
                    if (act_S == 0)
                    {
                        ts.S = "xxxyyy";
                    }
                    else if (act_S < 0)
                    {
                        ts.S = TechSOFT.Get_String(act_S);
                    }
                    else if (act_S >= 0)
                    {
                        ts.S = TechSOFT.Get_String(act_S);
                        //ts.Serialize(path);
                        //Application.Exit();
                    }
                    ts.Serialize(path);
                    ts.Serialize(path2);
                    ts.Serialize(path3);
                }
                else
                {
                    ts = new TechSOFT();
                    ts.AuthorizationCode = "tjjjbjsjjjbj";
                    //ts.T = TechSOFT.Get_String(10);
                    //ts.S = TechSOFT.Get_String(9);
                    ts.Serialize(path);
                    ts.Serialize(path2);

                    //Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Application.Exit();
            }
        }

        private void enterRenewalCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLockedVersion fch = new frmLockedVersion(true);
            //if (!fch.IsActivate || !fch.Check_ASTRA_Structure_Lock())
            //if (Version_Type == eVersionType.Demo || Version_Type == eVersionType.Activation_Trial)
            {
                if (fch.ShowDialog() == DialogResult.OK)
                {
                    tsmi_license.Visible = !fch.IsActivate;
                    toolStripSeparator9.Visible = !fch.IsActivate;
                }
            }
            /*
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //MessageBox.Show(path);
            TechSOFT ts = null;
            int act_S = -1;
            int act_T = -1;
            int old_act_T = -1;

            try
            {

                if (!File.Exists(Path.Combine(Application.StartupPath, "abtesiply4.sec")))
                {
                    Application.Exit();
                }
                if (File.Exists(path))
                {
                    ts = (TechSOFT)TechSOFT.DeSerialize(path);
                    try
                    {
                        act_T = TechSOFT.Get_Int(ts.T);
                    }
                    catch (Exception ex)
                    {
                        act_T = 0;
                    }
                    if (ts.S == "xxxyyy")
                    {
                        //MessageBox.Show(this, "Total " + act_T + " activations are finished. Contact TechSOFT.\n\nEmail At: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        File.WriteAllText(Path.Combine(Application.StartupPath, "Message.txt"), "Total " + act_T + " activations are finished.");
                    }
                    else
                        act_S = TechSOFT.Get_Int(ts.S);

                    //if (((act_T == 0) && (act_S == 0)) || (ts.S == "xxxyyy") || (act_S == 0))
                    //{
                    old_act_T = act_T;
                    frmCheckHasp fch = new frmCheckHasp(true);
                    fch.ShowDialog();
                    if (fch.Autho_Code == "")
                    {
                        //MessageBox.Show(this, "This Authorization code is already used.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Application.Exit();
                    }
                    else if (ts.AuthorizationCode == fch.Autho_Code)
                    {
                        MessageBox.Show(this, "This Authorization code is already used.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new Exception("This Authorization code is already used.");
                    }
                    else
                    {
                        try
                        {
                            ts.AuthorizationCode = fch.Autho_Code;
                            act_S = TechSOFT.Get_Int(ts.S);
                            act_T = TechSOFT.Get_Int(ts.T);

                            if (act_T < old_act_T || act_S > act_T)
                                throw new Exception("Invalid authorization code");
                            ts.Serialize(path);
                            ts.Serialize(path2);
                            ts.Serialize(path3);
                            MessageBox.Show(act_S + " activation activated.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, "Invalid authorization code", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //ts.T = TechSOFT.Get_String(old_act_T);
                            //ts.S = "xxxyyy";
                            //ts.Serialize(path);
                            //Application.Exit();
                        }
                        //act_S = TechSOFT.Get_Int(ts.S);
                        //act_T = TechSOFT.Get_Int(ts.T);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, "Error", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
             */
        }

        private void steelTrussOpenWebGirderRailwayBridgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this, "The module is not included in this package...Contact TechSOFT", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //frmSteelTrussOpenWebGirderBridge f_spg = new frmSteelTrussOpenWebGirderBridge(this);
            //f_spg.Owner = this;
            //f_spg.Show();
        }

        private void completeDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
            frmCompleteDesign f_cd = new frmCompleteDesign(this);
            f_cd.Owner = this;
            f_cd.Show();
        }

        private void memberSectionDesignTrialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
            frmStreelTrussMemberDesign f_md = new frmStreelTrussMemberDesign(this);
            f_md.Owner = this;
            f_md.Show();
        }

        private void deckSlabDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
            frmSteelTrussDeckSlab f_ds = new frmSteelTrussDeckSlab(this);
            f_ds.Owner = this;
            f_ds.Show();
        }

        private void tsmi_RCC_Pier_Interactive_Design_Click(object sender, EventArgs e)
        {
            AstraFunctionOne.BridgeDesign.Piers.frmRccPier fpr = new AstraFunctionOne.BridgeDesign.Piers.frmRccPier(this);
            fpr.Owner = this;
            fpr.Show();
        }

        private void stressingGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Short_Span);
            OpenWorkSheet_Design("STRESSING GRAPH", @"Pre Stressed\Stressing Graph\TECHSOFT_STRESSING_GRAPH.xls", "");
            //string file_n = Path.Combine(Application.StartupPath, "Design\\STRESSING_GRAPH.xls");

            //if (!File.Exists(file_n))
            //    file_n = Path.Combine(Application.StartupPath, @"Pre Stressed\Stressing Graph\TECHSOFT_STRESSING_GRAPH.xls");

            //if (File.Exists(file_n))
            //{
            //    try
            //    {
            //        //System.Diagnostics.Process.Start(file_n);
            //        //OpenExcelFile(file_n, "2011ap");

            //       //Chiranjit [2011 05 23]

            //    }
            //    catch (Exception ex) { }
            //}
            //else
            //{
            //    MessageBox.Show(file_n + " file not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void bridgeBearingDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BridgeAnalysisDesign.BearingDesign.frm_BearingDesign frm = new BridgeAnalysisDesign.BearingDesign.frm_BearingDesign(this);
            frm.Owner = this;
            frm.Show();

            //OpenWorkSheet_Design("Bridge Bearing Worksheet Design ", @"Bearing Design\Bridge_Bearing_Design.xls", "");
        }

        private void AbutmentWorkSheet_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            //if (tsmi.Name == tsmi_RCC_AbutmentWorksheetDesign2.Name)
            //{
            //    frmAbutmentWorkSheet faw = new frmAbutmentWorkSheet(this, 2);
            //    faw.Owner = this;
            //    faw.Show();
            //}
            //else
            //{
            //    frmAbutmentWorkSheet faw = new frmAbutmentWorkSheet(this, 1);
            //    faw.Owner = this;
            //    faw.Show();
            //}


            //Chiranjit [2011 05 23]
            //OpenWorkSheet_Design("Abutment Worksheet Design", "Abutment_Worksheet_sheet.xls", "RccAbutmentDrawings");
        }

        private void PierWorkSheet_Click(object sender, EventArgs e)
        {
            //frmPierWorkSheet faw = new frmPierWorkSheet(this);
            //faw.Owner = this;
            //faw.Show();
            OpenWorkSheet_Design("Pier Worksheet Design", @"Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.xls", "RCC_Pier");
        }


        private void worksheetDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmCompositWorksheetDesign fcomp = new frmCompositWorksheetDesign(this);
            //fcomp.Owner = this;
            //fcomp.Show();
            //Chiranjit [2011 05 23]
            OpenWorkSheet_Design("Composite Bridge Worksheet Design", @"Composite\Composite Worksheet Design 1\Composit_Worksheet_Design.xls", "Composite_Worksheet_Design");
        }

        private void PSC_BOX_Long_Girder_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowTimerScreen(eASTRAImage.PSC_Box_Girder);
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");

                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                string excel_file = "PSC Box Girder\\" + tsmi.Text + ".xls";

                //excel_file = Path.Combine(excel_path, excel_file);


                //if (File.Exists(excel_file))
                //{
                OpenWorkSheet_Design(tsmi.Text, excel_file, "PSC_Box_Girder_Worksheet_Design");
                //OpenExcelFile(excel_file, "2011ap");
                //}
            }
            catch (Exception ex) { }
        }

        private void OpenWorkSheet_Design(string title, string excel_file, string command_Drawing_path)
        {
            frm_OpenWorksheet_Design frm = new frm_OpenWorksheet_Design(this, excel_file, command_Drawing_path);
            frm.Owner = this;
            frm.Text = title.ToUpper();
            frm.Show();
        }

        private void drawingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\PSC Box");
                //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
                RunViewer("", "PSC_Box_Girder_Worksheet_Design");
            }
            catch (Exception ex) { }
        }

        private void sampleAnalysisDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string inp_path = Path.Combine(Application.StartupPath, "DESIGN\\Analysis Data File.txt");
                if (File.Exists(inp_path))
                    System.Diagnostics.Process.Start(inp_path);
            }
            catch (Exception ex) { }
        }

        private void completeDesignToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
            frmCompleteDesign f_cd = new frmCompleteDesign(this, true);
            f_cd.Owner = this;
            f_cd.Show();
        }

        private void tBeamBridgeWithBottomFlangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            //if (MessageBox.Show(msg, "ASTRA", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //{
            frm_RccTBeam_Bottom_Flange f_cd = new frm_RccTBeam_Bottom_Flange(this);
            f_cd.Owner = this;
            f_cd.Show();
            //}

        }

        private void worksheetDesign1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            //OpenWorkSheet_Design("DESIGN OF RCC T - GIRDER USING ANALYSIS RESULTS ", @"TBEAM Bridge\TBEAM Worksheet Design 1\T_Girder_Bridge_ASTRA.xls", "TBEAM_Worksheet_Design1");
            OpenWorkSheet_Design("DESIGN OF RCC T - GIRDER USING ANALYSIS RESULTS ", @"TBEAM Bridge\TBEAM Worksheet Design 1\T_Girder_Bridge_ASTRA.xls", "");

        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            //OpenWorkSheet_Design("DESIGN OF SECTION PROPERTIES OF INTERMEDIATE DIAPHRAGM", @"TBEAM Bridge\TBEAM Worksheet Design 2\01 SectionPROP.XLS", "TBEAM_Worksheet_Design2");
            OpenWorkSheet_Design("DESIGN OF SECTION PROPERTIES OF INTERMEDIATE DIAPHRAGM", @"TBEAM Bridge\TBEAM Worksheet Design 2\01 SectionPROP.XLS", "");
            //OpenWorkSheet_Design("DESIGN OF SECTION PROPERTIES OF INTERMEDIATE DIAPHRAGM", "01 SectionPROP.XLS", "Bridge\\T Beam\\Design2");

        }

        private void toolStripMenuItem40_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            OpenWorkSheet_Design("DESIGN OF SIDL (SUPERIMPOSED DEAD LOAD)", @"TBEAM Bridge\TBEAM Worksheet Design 2\02 SIDL.xls", "");

        }

        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            OpenWorkSheet_Design("DESIGN OF Cross Girder + Slab Design", @"TBEAM Bridge\TBEAM Worksheet Design 2\03 CrossGirder+Slab Design.xls", "");

        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            //OpenWorkSheet_Design("DESIGN OF Tee Girder", @"TBEAM Bridge\TBEAM Worksheet Design 2\04 TeeGirder Design.xls", "TBEAM_Worksheet_Design2");
            OpenWorkSheet_Design("DESIGN OF Tee Girder", @"TBEAM Bridge\TBEAM Worksheet Design 2\04 TeeGirder Design.xls", "");

        }

        private void tsmi_Steel_Truss_warren2_worksheetDesign_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Warren2 Steel Truss Bridge", @"Steel Truss\Warren 2 Worksheet Design\Warren2 Steel Truss Bridge.xls", "SteelTruss_Warren2");

        }

        private void sheetPileForCofferdamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Sheet Pile for Cofferdam", @"Foundation\Sheet Pile\Design Sheet Pile.xls", "");
        }

        private void designOfCrossDiaphragmsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Design of Cross Diaphragms", @"PSC Box Girder\Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS", "PSC_Box_Girder_Worksheet_Design");

        }

        private void designOfEndAnchorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Design of End Anchorage", @"PSC Box Girder\Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS", "PSC_Box_Girder_Worksheet_Design");

        }

        private void designOfBlisterBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Design of Blister Blocks", @"PSC Box Girder\Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS", "PSC_Box_Girder_Worksheet_Design");

        }

        private void tsmi_DesignOfAnchorageForFuturePrestressingBlock_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Design of Anchorage for Future Prestressing Block", @"PSC Box Girder\Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS", "PSC_Box_Girder_Worksheet_Design");

        }

        private void tsmi_DesignSequence_Click(object sender, EventArgs e)
        {
            frm_PSC_DesignSequence fm = new frm_PSC_DesignSequence();
            fm.Owner = this;
            fm.Show();
        }

        private void tsmi_Pier_Cap_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Pier Cap", @"Pier\Pier Worksheet Design 2\01 Pier Cap.xls", "RCC_Pier_Worksheet_Design_2");
        }

        private void tsmi_Pile_Capacity_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Pile Capacity", @"Pier\Pier Worksheet Design 2\02 Pile Capacity.xls", "RCC_Pier_Worksheet_Design_2");

        }

        private void tsmi_Pier_Design_Click(object sender, EventArgs e)
        {
            OpenWorkSheet_Design("DESIGN OF Pier with Piles", @"Pier\Pier Worksheet Design 2\03 Pier Design with 6piles.xls", "RCC_Pier_Worksheet_Design_2");

        }

        private void tsmi_Analysis_Pier_Click(object sender, EventArgs e)
        {
            //frm_PierAnalysis frm = new frm_PierAnalysis(this);
            frm_Analysis_Pier frm = new frm_Analysis_Pier(this);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_Material_Properties_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Name == tsmi_Material_Properties.Name)
                OpenExcelFile(Path.Combine(Application.StartupPath, @"DESIGN\Material Properties\Material Properties.xls"), "2011ap");
            if (tsmi.Name == tsmi_Section_Properties.Name)
                OpenExcelFile(Path.Combine(Application.StartupPath, @"DESIGN\Section Properties\Section Properties.xls"), "2011ap");
            if (tsmi.Name == tsmi_seismic_coefficient.Name)
                OpenExcelFile(Path.Combine(Application.StartupPath, @"DESIGN\Siesmic Coefficient\Calculation for Siesmic Coefficient.xls"), "2011ap");
        }

        private void analysisOfBridgeDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Analysis_Composit frm = new frm_Analysis_Composit(this);
            frm.Owner = this;
            frm.Show();
        }

        private void drawingsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Open_Drawings(eDrawingsType.RCC_T_Girder);
        }

        public void Open_Drawings(eDrawingsType item_draw)
        {
            frm_Open_Drawings frm = new frm_Open_Drawings(this, item_draw);
            frm.Owner = this;
            frm.Show();
        }
        public void Open_Drawings(eDrawingsType item_draw, string user_path)
        {
            frm_Open_Drawings frm = new frm_Open_Drawings(this, item_draw, user_path);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_RCC_TBeam_Click(object sender, EventArgs e)
        {
            //if (!Is_select_Design_Standard) SelectDesignStandard();
            Show_T_Girder_Bridge_Working_Stress();
        }
        //Chiranjit [2013 01 03]
        private void Show_T_Girder_Bridge_Working_Stress()
        {


            //if (!Is_select_Design_Standard) SelectDesignStandard(); 
            ShowTimerScreen(eASTRAImage.RCC_T_Beam_Bridge);
            frm_RCC_T_Girder_WS frm = new frm_RCC_T_Girder_WS(this);
            frm.Owner = this;
            frm.Show();
        }
        //Chiranjit [2013 01 03]
        private void Show_T_Girder_Bridge_Limit_State()
        {

            ShowTimerScreen(eASTRAImage.RCC_T_Beam_Bridge);

            if (IsRelease_22)
            {
                frm_RCC_T_Girder_LS_New frm = new frm_RCC_T_Girder_LS_New(this);
                //frmRCC_T_Girder_Stage frm = new frmRCC_T_Girder_Stage(this);
                frm.Owner = this;
                frm.Show();
            }
            else
            {
                frm_RCC_T_Girder_LS frm = new frm_RCC_T_Girder_LS(this);
                frm.Owner = this;
                frm.Show();
            }
        }
        public void SetWorkingFolder_OLD()
        {
            string tst = "";
            string last_work_fol = Path.Combine(Application.StartupPath, "lastwf.dll");

            //Tables
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                //Chiranjit [2012 09 26]
                if (!Directory.Exists(this.LastDesignWorkingFolder))
                {
                    if (File.Exists(last_work_fol))
                        fbd.SelectedPath = File.ReadAllText(last_work_fol);
                }
                else
                    fbd.SelectedPath = this.LastDesignWorkingFolder;



                fbd.Description = "Select your Working Folder";
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    this.LastDesignWorkingFolder = fbd.SelectedPath;

                    File.WriteAllText(last_work_fol, fbd.SelectedPath);

                    tst = fbd.SelectedPath;
                    tst = Get_Modified_Path(tst);

                    //this.Text = "ASTRA Pro" + (IsDemo ? " [DEMO] " : "") + " [ Working Folder : \"" + tst + "\" ]";
                    ASTRA_Pro = "[ Working Folder : \"" + tst + "\" ]";
                    Delete_Temporary_Files();

                    using (frm_DesignStandardOption fds = new frm_DesignStandardOption())
                    {
                        fds.IsAASTHO = IsAASTHO;
                        fds.DesignStandard = DesignStandard;
                        if (fds.ShowDialog() == DialogResult.OK)
                        {
                            DesignStandard = fds.DesignStandard;
                            string ll_txt_file = Path.Combine(Application.StartupPath, @"LL.TXT");
                            //if (!File.Exists(ll_txt_file))
                            Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);
                            if (File.Exists(ll_txt_file))
                            {
                                LiveLoads = new LiveLoadCollections(ll_txt_file);
                            }
                        }
                    }
                }
            }
            Set_Bridge_Design_Menu();
        }

        public void SetWorkingFolder(string folder, eDesignStandard eDeg)
        {

            this.LastDesignWorkingFolder = folder;
            string tst = this.LastDesignWorkingFolder;
            tst = MyList.Get_Modified_Path(tst);
            ASTRA_Pro = "[ Working Folder : \"" + tst + "\" ]";
            DesignStandard = eDeg;

            //if (DesignStandard == eDesignStandard.BritishStandard)
            //Tables = new ASTRA_Tables(DesignStandard);

            string ll_txt_file = Path.Combine(Application.StartupPath, @"LL.TXT");
            //if (!File.Exists(ll_txt_file))
            Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);
            if (File.Exists(ll_txt_file))
            {
                LiveLoads = new LiveLoadCollections(ll_txt_file);
                tsmi_ll_data.Visible = true;
            }
            Set_Bridge_Design_Menu();

        }
        public string LL_TXT_Path
        {
            get
            {

                string ll_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                ll_path = Path.Combine(ll_path, @"ASTRA_" + DesignStandard);

                if (!Directory.Exists(ll_path))
                    Directory.CreateDirectory(ll_path);

                return Path.Combine(ll_path, @"LL.TXT");
            }
        }
        public void SelectWorkingFolder()
        {

            using (frm_work_folder fbd = new frm_work_folder(this))
            {
                //Chiranjit [2012 09 26]

                if (fbd.ShowDialog() != DialogResult.Cancel)
                {

                    string tst = this.LastDesignWorkingFolder;
                    tst = Get_Modified_Path(tst);

                    //this.Text = "ASTRA Pro" + (IsDemo ? " [DEMO] " : "") + " [ Working Folder : \"" + tst + "\" ]";
                    ASTRA_Pro = "[ Working Folder : \"" + tst + "\" ]";
                    Delete_Temporary_Files();

                    //using (frm_DesignStandardOption fds = new frm_DesignStandardOption())
                    //{
                    //    fds.IsAASTHO = IsAASTHO;
                    //    fds.DesignStandard = DesignStandard;
                    //    if (fds.ShowDialog() == DialogResult.OK)
                    //    {
                    //        DesignStandard = fds.DesignStandard;
                    //        //if (DesignStandard == eDesignStandard.BritishStandard)

                    //        //Tables = new ASTRA_Tables(DesignStandard);
                    //        //Tables.DesignStandard = DesignStandard;

                    //        string ll_txt_file = Path.Combine(Application.StartupPath, @"LL.TXT");

                    //        //if (File.Exists(LL_TXT_Path)) File.Delete(LL_TXT_Path);

                    //        if (!File.Exists(LL_TXT_Path))
                    //        {
                    //            //if (!File.Exists(ll_txt_file))
                    //            Write_LiveLoad_LL_TXT(Path.GetDirectoryName(LL_TXT_Path), true, DesignStandard);
                    //            Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);
                    //        }

                    //        if (File.Exists(LL_TXT_Path))
                    //        {
                    //            LiveLoads = new LiveLoadCollections(LL_TXT_Path);
                    //            tsmi_ll_data.Visible = true;
                    //        }

                    //    }
                    //}
                }
            }
            Set_Bridge_Design_Menu();
        }

        public void SelectDesignStandard()
        {
            using (frm_DesignStandardOption fds = new frm_DesignStandardOption())
            {
                fds.IsAASTHO = IsAASTHO;
                fds.DesignStandard = DesignStandard;
                if (fds.ShowDialog() == DialogResult.OK)
                {
                    DesignStandard = fds.DesignStandard;
                    Write_Default_Moving_Loads();
                }
                //Is_select_Design_Standard = true;
            }
            Set_Bridge_Design_Menu();
        }

        private void Write_Default_Moving_Loads()
        {

            if (!File.Exists(LL_TXT_Path))
            {
                //if (!File.Exists(ll_txt_file))
                Write_LiveLoad_LL_TXT(Path.GetDirectoryName(LL_TXT_Path), true, DesignStandard);
                //Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);
            }
            //Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);

            if (File.Exists(LL_TXT_Path))
            {
                LiveLoads = new LiveLoadCollections(LL_TXT_Path);
                tsmi_ll_data.Visible = true;
            }

        }

        public void Delete_Temporary_Files()
        {
            string tst = "";

            List<string> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(LastDesignWorkingFolder));
                files.AddRange(Directory.GetFiles(WorkingFolder));
            }
            catch (Exception ex) { }

            foreach (var item in files)
            {
                if (Path.GetExtension(item.ToLower()) == ".fil" ||
                    Path.GetExtension(item.ToLower()) == ".tmp")
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (Exception ex) { }
                }
            }
        }
        public void Delete_Temporary_Files(string folder_path)
        {
            string tst = folder_path;

            if (File.Exists(tst))
                folder_path = Path.GetDirectoryName(tst);

            List<string> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(folder_path));
            }
            catch (Exception ex) { }

            foreach (var item in files)
            {
                if (Path.GetExtension(item.ToLower()) == ".fil" ||
                    Path.GetExtension(item.ToLower()) == ".tmp")
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        private static string Get_Modified_Path(string tst)
        {
            try
            {
                string tst1 = Path.GetFileName(tst);
                string tst2 = Path.GetFileName(Path.GetDirectoryName(tst));
                string tst3 = Path.GetPathRoot(tst);
                if (tst2 != "")
                    tst = tst3 + "....\\" + Path.Combine(tst2, tst1);
            }
            catch (Exception ex) { }
            return tst;
        }

        private void Set_Bridge_Design_Menu()
        {
            tsmi_selectDesignStandard.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            tsmi_Bridge_Design.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            tsmi_RCC_Structural_Design.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            tsmi_structure_text.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            tsmi_structure_sap.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            tsmi_research_Studies.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            tsmi_structureModeling.Enabled = Directory.Exists(this.LastDesignWorkingFolder);


            tsmi_openStageAnalysisTEXTDataFile.Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            return;
            //Chiranjit [2012 02 23]
            //foreach (var item in tsmi_Bridge_Design.DropDownItems)
            //{
            //    if (((ToolStripMenuItem)item).Name != tsmi_Working_Folder.Name)
            //        ((ToolStripMenuItem)item).Enabled = Directory.Exists(this.LastDesignWorkingFolder);
            //}

            //Load_Structure_Analysis_Menu();
            //tsmi_file_2.Visible = false;
            foreach (var item in tsmi_Process_Design.DropDownItems)
            //foreach (var item in tsmi_file.DropDownItems)
            {
                try
                {
                    if (((ToolStripMenuItem)item).Name != tsmi_selectWorkingFolder.Name)
                        ((ToolStripMenuItem)item).Enabled = Directory.Exists(this.LastDesignWorkingFolder);
                }
                catch (Exception ex) { }
            }




            foreach (var item in tsmi_Bridge_Design.DropDownItems)
            {
                try
                {
                    if (((ToolStripMenuItem)item).Name != tsmi_Working_Folder.Name)
                        ((ToolStripMenuItem)item).Enabled = Directory.Exists(this.LastDesignWorkingFolder);
                }
                catch (Exception ex) { }
            }

            foreach (var item in tsmi_RCC_Structural_Design.DropDownItems)
            {
                try
                {
                    if (((ToolStripMenuItem)item).Name != tsmi_Working_Folder1.Name)
                        ((ToolStripMenuItem)item).Enabled = Directory.Exists(this.LastDesignWorkingFolder);
                }
                catch (Exception ex) { }
            }
            foreach (var item in tsmi_process_analysis.DropDownItems)
            {
                try
                {
                    if (((ToolStripMenuItem)item).Name != tsmi_workingFolder.Name)
                        ((ToolStripMenuItem)item).Enabled = Directory.Exists(this.LastDesignWorkingFolder);
                }
                catch (Exception ex) { }
            }

            foreach (var item in tsmi_research_Studies.DropDownItems)
            {
                try
                {
                    if (((ToolStripMenuItem)item).Name != tsmi_selectWorkingFolderResearch.Name)
                        ((ToolStripMenuItem)item).Enabled = Directory.Exists(this.LastDesignWorkingFolder);
                }
                catch (Exception ex) { }
            }
        }

        private void tsmi_Working_Folder_Click(object sender, EventArgs e)
        {
            SelectWorkingFolder();
            Timer_Interval = 9999;
        }

        //Working Stress Method
        private void tsmi_Composit_Bridge_WS_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();

            ShowTimerScreen(eASTRAImage.Composite_Bridge);

            BridgeAnalysisDesign.Composite.frm_Composite frm = new BridgeAnalysisDesign.Composite.frm_Composite(this);
            frm.Owner = this;
            frm.Show();
        }
        //Limit State Method
        private void tsmi_Composit_Bridge_LS_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();

            ShowTimerScreen(eASTRAImage.Composite_Bridge);
            //LimitStateMethod.Composite.frm_Composite_LS frm = new LimitStateMethod.Composite.frm_Composite_LS(this);
            //LimitStateMethod.Composite.frm_CompositeLSM frm = new LimitStateMethod.Composite.frm_CompositeLSM(this);
            //frm.Owner = this;
            //frm.Show();
        }

        private void tsmi_Pre_Stressed_Bridge_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            //if (((ToolStripMenuItem)sender).Name == tsmi_PSC_I_Girder_LongSpan.Name)
            //{
            //    ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            //    BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_LongSpan_WS frm = new BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_LongSpan_WS(this);
            //    frm.Owner = this;
            //    frm.Show();
            //}
            //else
            {
                ShowTimerScreen(eASTRAImage.PSC_I_Girder_Short_Span);
                BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_ShortSpan_WS frm = new BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_ShortSpan_WS(this);
                frm.Owner = this;
                frm.Show();

            }
        }

        private void tsmi_PSC_Box_Girder_Bridge_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            ShowTimerScreen(eASTRAImage.PSC_Box_Girder);
            if (DesignStandard == eDesignStandard.LRFDStandard)
            {
                LimitStateMethod.PSC_Box_Girder.frm_PSC_Box_Girder_AASHTO frm = new LimitStateMethod.PSC_Box_Girder.frm_PSC_Box_Girder_AASHTO(this);
                frm.Owner = this;
                frm.Show();
            }
            else
            {
                //Chiranjit [2011 10 17] New PSC Box girder Analysis
                //BridgeAnalysisDesign.PSC_BoxGirder.frm_PSC_BoxGirder frm = new BridgeAnalysisDesign.PSC_BoxGirder.frm_PSC_BoxGirder(this);
                BridgeAnalysisDesign.PSC_BoxGirder.frm_PSC_Box_Girder frm = new BridgeAnalysisDesign.PSC_BoxGirder.frm_PSC_Box_Girder(this);
                frm.Owner = this;
                frm.Show();
            }
        }

        private void tsmi_Rail_Bridge_Click(object sender, EventArgs e)
        {

            if (!Is_select_Design_Standard) SelectDesignStandard(); 
            ShowTimerScreen(eASTRAImage.Rail_Plate_Girder);
            BridgeAnalysisDesign.Railway.frm_Railway frm = new BridgeAnalysisDesign.Railway.frm_Railway(this);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_Bridge_Abutment_Design_Click(object sender, EventArgs e)
        {

            if (!Is_select_Design_Standard) SelectDesignStandard();
            //ShowTimerScreen(eASTRAImage.TGirder_Bottom_Flange);
            BridgeAnalysisDesign.Abutment.frm_Abutment frm = new BridgeAnalysisDesign.Abutment.frm_Abutment(this);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_Bridge_Pier_Design_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_pier_1.Name) // Design of RCC Pier with Analysis
            {

                LimitStateMethod.PierDesign.frm_RCC_Pier_Analysis_Design frm = new LimitStateMethod.PierDesign.frm_RCC_Pier_Analysis_Design(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi.Name == tsmi_pier_2.Name) // Design of RCC Pier with Pile / Open Foundation (Worksheet Design 1)
            {


                //LimitStateMethod.PierDesign.frm_RCC_Pier_Design_with_Pile_Open_Foundation frm = new LimitStateMethod.PierDesign.frm_RCC_Pier_Design_with_Pile_Open_Foundation(this);
                LimitStateMethod.PierDesign.frm_RCC_Pier_Design_with_Pile_Open_Foundation frm = new LimitStateMethod.PierDesign.frm_RCC_Pier_Design_with_Pile_Open_Foundation(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi.Name == tsmi_pier_3.Name) // Design of RCC Pier with Pile Foundation 
            {


                LimitStateMethod.PierDesign.frm_RCC_Pier_Design_with_Pile_Foundation frm = new LimitStateMethod.PierDesign.frm_RCC_Pier_Design_with_Pile_Foundation(this);
                frm.Owner = this;
                frm.Show();

            }
            else if (tsmi.Name == tsmi_pier_4.Name) // Design of RCC Circular Pier
            {

                LimitStateMethod.PierDesign.frm_RCC_Circular_Pier_Design frm = new LimitStateMethod.PierDesign.frm_RCC_Circular_Pier_Design(this);
                frm.Owner = this;
                frm.Show();

            }
            else if (tsmi.Name == tsmi_pier_5.Name) // Design of Stone Masonry Pier
            {


                LimitStateMethod.PierDesign.frm_Pier_Stone_Masonry_Design frm = new LimitStateMethod.PierDesign.frm_Pier_Stone_Masonry_Design(this);
                frm.Owner = this;
                frm.Show();

            }
            else
            {
                //ShowTimerScreen(eASTRAImage.TGirder_Bottom_Flange);
                BridgeAnalysisDesign.Pier.frm_Pier frm = new BridgeAnalysisDesign.Pier.frm_Pier(this);
                frm.Owner = this;
                frm.Show();
            }
        }

        private void tsmi_Bridge_Foundation_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            //SelectDesignStandard();


            //if (!Is_select_Design_Standard) SelectDesignStandard();
            //ShowTimerScreen(eASTRAImage.TGirder_Bottom_Flange);
            BridgeAnalysisDesign.Foundation.frm_Foundation frm = new BridgeAnalysisDesign.Foundation.frm_Foundation(this);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_RCC_Culverts_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;



            if (!Is_select_Design_Standard) SelectDesignStandard();
            
            if(tsmi == tsmi_RCC_Culverts_LS)
            {
                //ShowTimerScreen(eASTRAImage.TGirder_Bottom_Flange);
                LimitStateMethod.RccCulvert.frm_BoxCulvert_LS frm = new LimitStateMethod.RccCulvert.frm_BoxCulvert_LS(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi == tsmi_RCC_Culverts_WS)
            //else
            {
                //ShowTimerScreen(eASTRAImage.TGirder_Bottom_Flange);
                BridgeAnalysisDesign.RCC_Culvert.frm_RCC_Culvert frm = new BridgeAnalysisDesign.RCC_Culvert.frm_RCC_Culvert(this);
                frm.Owner = this;
                frm.Show();
            }
        }

        private void tsmi_Underpass_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            //if (tsmi == tsmi_rcc_box_push_underpass)
            //{
            //    //Chiranjit [2017 03 27]
            //    BridgeAnalysisDesign.Underpass.frm_BoxPushedUnderpass frm = new BridgeAnalysisDesign.Underpass.frm_BoxPushedUnderpass(this);
            //    frm.Owner = this;
            //    frm.Show();
            //}
            if (tsmi == tsmi_road_box_pushed_underpass)
            {
                //Chiranjit [2017 03 27]
                BridgeAnalysisDesign.Underpass.frm_BoxPushedUnderpass frm = new BridgeAnalysisDesign.Underpass.frm_BoxPushedUnderpass(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi == tsmi_rail_box_pushed_underpass)
            {
                //Chiranjit [2017 03 27]
                BridgeAnalysisDesign.Underpass.frm_BoxPushedRailLoad frm = new BridgeAnalysisDesign.Underpass.frm_BoxPushedRailLoad(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi == tsmi_GADs_Underpasses)
            {

                //BridgeAnalysisDesign.Underpass.frm_Underpass frm = new BridgeAnalysisDesign.Underpass.frm_Underpass(this);
                //Chiranjit [2012 06 25]
                BridgeAnalysisDesign.Underpass.frm_Underpasses frm = new BridgeAnalysisDesign.Underpass.frm_Underpasses(this);
                frm.Owner = this;
                frm.Show();
            }
        }
        private void tsmi_open_ASTRA_worksheet_design_Click(object sender, EventArgs e)
        {
            Open_ASTRA_Worksheet_Dialog();
        }
        private void tsmi_minor_Bridge_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Name == tsmi_minor_Bridge.Name)
            {

                if (!Is_select_Design_Standard) SelectDesignStandard();
                LimitStateMethod.Minor_Bridge.frm_MinorBridge_LS frm = new LimitStateMethod.Minor_Bridge.frm_MinorBridge_LS(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi.Name == tsmi_minor_Bridge_ws.Name)
            {
                //frmSlabCulvert frm = new frmSlabCulvert(this);

                if (!Is_select_Design_Standard) SelectDesignStandard(); BridgeAnalysisDesign.MinorBridge.frm_MinorBridge frm = new BridgeAnalysisDesign.MinorBridge.frm_MinorBridge(this);
                frm.Owner = this;
                frm.Show();
            }
        }


        #region IApplication Members

        public string WorkingFolder
        {
            get
            {
                if (!File.Exists(flPath)) return "";
                return Path.GetDirectoryName(flPath);
            }
            set
            {
            }
        }

        #endregion
        #region IApplication Members

        public IDocument AppDocument
        {
            get
            {
                return this.AppDoc;
            }
            set
            {
                this.AppDoc = value;
            }
        }

        public Form AppWindow
        {
            get { return this; }
        }

        #endregion
        #region IApplication Members


        public string WorkingFile
        {
            get
            {
                if (FilePath == "")
                    return "";
                return Path.GetFullPath(FilePath);
            }
            set
            {
                FilePath = value;
                setControlEnable();
            }
        }

        #endregion
        #region IApplication Members

        frmOpenViewer f_pl_wait = null;

        public void ShowPleaseWait(string str)
        {
            ClosePleaseWait();
            f_pl_wait = new frmOpenViewer();
            f_pl_wait.TextString = str;
            f_pl_wait.IsTimerOn = false;
            f_pl_wait.Owner = this;
            f_pl_wait.Show();
        }
        public void ClosePleaseWait()
        {
            try
            {
                f_pl_wait.Close();
            }
            catch (Exception ex) { }
        }
        public void RunViewer()
        {
            SET_ENV();
            run_file = Path.Combine(Application.StartupPath, "Viewer.exe");
            System.Diagnostics.Process.Start(run_file);
            frmOpenViewer fopen = new frmOpenViewer();
            fopen.Owner = this;
            fopen.Show();
        }
        public void RunViewer(string working_folder, string drawing_path)
        {
            if (Directory.Exists(working_folder) == false)
                Directory.CreateDirectory(working_folder);

            OpenDefaultDrawings(working_folder, drawing_path);
        }

        public void RunViewer(string drawing_path)
        {

            OpenDefaultDrawings(drawing_path, drawing_path);
        }
        #endregion
        #region IApplication Members

        public void SetDrawingFile_Path(string drawing_path)
        {
            StreamWriter sw = new StreamWriter(new FileStream(EnvFilePath, FileMode.Create));
            try
            {
                sw.WriteLine("CODE ={0}", "DRAWINGS");
                sw.WriteLine("PATH =" + drawing_path);
                sw.WriteLine("DRAWINGS");
                sw.WriteLine("PATH =" + drawing_path);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            RunViewer();
        }

        public void SetDrawingFile_Path(string filePath, string code, string default_drawing_path_code)
        {
            StreamWriter sw = new StreamWriter(new FileStream(EnvFilePath, FileMode.Create));
            try
            {
                sw.WriteLine("CODE ={0}", code);
                sw.WriteLine("PATH =" + filePath);
                //if (!Directory.Exists(default_drawing_path) && default_drawing_path != "")
                //{
                //    default_drawing_path = Path.Combine(Application.StartupPath, "DRAWINGS\\" + default_drawing_path);
                //}
                //if (Directory.Exists(default_drawing_path))
                //{
                sw.WriteLine("ASTRA_DRAWINGS");
                sw.WriteLine("PATH =" + default_drawing_path_code);
                //}

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            RunViewer();
        }
        public void SetDrawingFile_Path(string filePath, string code, string drawing_path, string default_drawing_path)
        {
            StreamWriter sw = new StreamWriter(new FileStream(EnvFilePath, FileMode.Create));
            try
            {
                sw.WriteLine("CODE ={0}", code);
                sw.WriteLine("PATH =" + filePath);
                sw.WriteLine("DRAWINGS");

                if (File.Exists(drawing_path)) drawing_path = Path.GetDirectoryName(drawing_path);
                if (!Directory.Exists(drawing_path)) Directory.CreateDirectory(drawing_path);

                sw.WriteLine("PATH =" + drawing_path);
                //if (!Directory.Exists(default_drawing_path) && default_drawing_path != "")
                //{
                //    default_drawing_path = Path.Combine(Application.StartupPath, "DRAWINGS\\" + default_drawing_path);
                //}
                //if (Directory.Exists(default_drawing_path))
                //{
                sw.WriteLine("ASTRA_DRAWINGS");
                sw.WriteLine("PATH =" + default_drawing_path);
                //}

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            RunViewer();
        }

        public void SetApp_Design_Slab(string filePath, eSLAB slab, eSLAB_Part part)
        {
            StreamWriter sw = new StreamWriter(new FileStream(EnvFilePath, FileMode.Create));
            try
            {
                sw.WriteLine("CODE ={0} {1}", slab.ToString(), part.ToString());
                //filePath = Path.Combine(filePath, "DESIGN_" + slab.ToString());
                sw.WriteLine("PATH =" + Path.Combine(filePath, "DESIGN.FIL"));
                //sw.WriteLine("PATH =" + filePath);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            RunViewer();
        }



        public void ASTRA_Analysis_Process(string file_name, string caption)
        {
            AstraAccess.ViewerFunctions.ASTRA_Analysis_Process(file_name, caption);
        }

        public void SetApp_Structure(string filePath, bool IsOpenMovingLoad)
        {
            //AstraAccess.ViewerFunctions.ASTRA_Input_Data(filePath, );
            //AstraAccess.ViewerFunctions.ASTRA_Input_Data(filePath, );
            if (IsOpenMovingLoad)
                AstraAccess.ViewerFunctions.ASTRA_Analysis_Process(filePath, true);
            else
            {
                AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(this, filePath, false).Show();

                //AstraAccess.ViewerFunctions.ASTRA_Input_Data(filePath);
            }
        }
        public void SetApp_Structure(string filePath, string feature)
        {
            AstraAccess.ViewerFunctions.ASTRA_Input_Data(filePath);

            //StreamWriter sw = new StreamWriter(new FileStream(EnvFilePath, FileMode.Create));
            //try
            //{

            //    sw.WriteLine("ASTRA_INPUT");

            //    sw.WriteLine("PATH =" + filePath);
            //    sw.WriteLine("FEATURE =" + feature.ToUpper());

            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
            //RunViewer();
        }
        public string EnvFilePath
        {
            get
            {

                string envFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "env.set");

                return envFile;

                //return Path.Combine(Application.StartupPath, "env.set");
                //string envFile = Path.Combine(Application.StartupPath, "env.set");
            }
        }
        public void SET_ENV()
        {
            System.Environment.SetEnvironmentVariable("ASTRA", EnvFilePath);
        }
        #endregion
        #region IApplication Members


        public void View_Result(string file_name)
        {
            frmViewResult f_v_r = new frmViewResult(file_name);
            f_v_r.Owner = this;
            f_v_r.WindowState = FormWindowState.Maximized;
            //f_v_r.ShowDialog();
            f_v_r.Show();

        }

        public void View_Result(string file_name, bool isLand_Scape)
        {
            frmViewResult f_v_r = new frmViewResult(file_name, isLand_Scape);
            f_v_r.Owner = this;
            f_v_r.WindowState = FormWindowState.Maximized;
            //f_v_r.ShowDialog();
            f_v_r.Show();

        }

        #endregion
        #region IApplication Members


        public bool ShowTimerScreen(eASTRAImage ast_img_type)
        {
            frmTimerScreen ft = new frmTimerScreen(ast_img_type);
            return (ft.ShowDialog() == DialogResult.OK);
        }

        #endregion
        #region IApplication Members

        public bool OpenExcelFile(string ExcelFileName, string password)
        {

            try
            {
                Excel_Open_Message();
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;

                object misValue;
                misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.ApplicationClass();
                //xlWorkBook = xlApp.Workbooks.Open("csharp.net-informations.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //xlWorkBook = xlApp.Workbooks.Open(txt_file.Text, 0, true, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);



                if (IsDemo)
                {
                    xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, true, 5, password, "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                }
                else
                {
                    xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, false, 5, password, "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
                }
                xlApp.Visible = true;

                Excel_Close_Message();

                return true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to release the Object " + ex.ToString());
            }
            return false;
        }
        public bool OpenExcelFile(string CopyPath, string ExcelFileName, string password)
        {
            try
            {

                if (Directory.Exists(CopyPath) == false)
                    Directory.CreateDirectory(CopyPath);

                string s = Path.Combine(CopyPath, Path.GetFileName(ExcelFileName));

                if (File.Exists(ExcelFileName))
                {
                    //Chiranjit [2014 10 09]
                    if (!File.Exists(s))
                        File.Copy(ExcelFileName, s, true);

                    ExcelFileName = s;
                }

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;

                object misValue;
                misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.ApplicationClass();
                //xlWorkBook = xlApp.Workbooks.Open("csharp.net-informations.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //xlWorkBook = xlApp.Workbooks.Open(txt_file.Text, 0, true, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);


                //xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, true, 5, password, "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);


                //xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, false, 5, password, "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);


                if (IsDemo)
                {
                    xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, true, 5, password, "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                }
                else
                {
                    xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, false, 5, password, "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
                }



                //Excel.Range rng = xlWorkSheet.get_Range("B2", "B3");
                xlApp.Visible = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to release the Object " + ex.ToString());
            }
            return false;
        }

        public void Open_WorkSheet_Design()
        {
            Open_ASTRA_Worksheet_Dialog();
        }

        #endregion
        #region IApplication Members


        public void OpenDefaultDrawings(string working_folder, string drawing_folder)
        {
            SetDrawingFile_Path(working_folder, "ASTRA_DRAWINGS", drawing_folder);
        }

        #endregion




        #region IApplication Members


        public void Progress_ON(string title)
        {
            //Chiranjit [2013 05 14]
            //frm_ProgressBar.ON(title);
            frm_ProgressList.ON(title);
            //progress.Opacity = 1.0;
        }

        public void Progress_OFF()
        {
            try
            {
                //Chiranjit [2013 05 14]
                //frm_ProgressBar.OFF();
                frm_ProgressList.OFF();

                //progress.Opacity = 0.0;
            }
            catch (Exception ex) { }
        }

        public double ProgressValue
        {
            get
            {
                //Chiranjit [2013 05 14]
                //return frm_ProgressBar.Value;
                return frm_ProgressList.Value;
            }
            set
            {
                //Chiranjit [2013 05 14]
                //frm_ProgressBar.Value = value;
                frm_ProgressList.Value = value;
            }
        }

        public void SetProgressValue(double a, double b)
        {
            try
            {
                //Chiranjit [2013 05 14]
                //frm_ProgressBar.SetValue(a, b);

                //Chiranjit [2013 05 14]
                frm_ProgressList.SetValue(a, b);

                //Chiranjit [2013 10 24]
                //Progress.Progress_Value = (int)((a / b) * 100.0);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion


        #region IApplication Members
        public void Open_ASTRA_Worksheet_Dialog()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Worksheet Design File(*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm";

                if (Directory.Exists(user_path))  ofd.InitialDirectory = user_path;
                else ofd.InitialDirectory = LastDesignWorkingFolder;

              

                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    if (IsDemo)
                    {
                        MessageBox.Show("This feature is not available in Demo Version of ASTRA Pro.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        OpenExcelFile(ofd.FileName, "2011ap");
                }
            }
        }

        #endregion IApplication Members

        #region External Program
        public string HeadsProgram
        {

            get
            {
                DirectoryInfo di = new DirectoryInfo("C:\\Test");

                DisplayNameAttribute ad = new DisplayNameAttribute();




                string kStr = Path.Combine(Application.StartupPath, "External Programs");
                if (!Directory.Exists(kStr))
                    kStr = Path.Combine(Application.StartupPath, "ExternalPrograms");
                if (!Directory.Exists(kStr))
                    kStr = Path.Combine(Application.StartupPath, "ExternalProgram");
                if (!Directory.Exists(kStr))
                    kStr = Path.Combine(Application.StartupPath, "HeadsHelp\\ExternalProgram");

                return kStr;
            }
        }
        private void trainingGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunApplication.TrainingGuide(this, Path.Combine(HeadsProgram, "headsTraining.rtf"));
        }

        private void infoAboutThisProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunApplication.GoogleEarthInfo(this, Path.Combine(HeadsProgram, "googleEarth.rtf"));
        }

        private void runProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunApplication.GoogleEarth();
        }

        private void infoAboutTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunApplication.GlobalMapperInfo(this, Path.Combine(HeadsProgram, "globalMapper.rtf"));

        }

        private void runProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunApplication.GlobalMapper();

        }

        private void infoAboutTheProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunApplication.AllTransInfo(this, Path.Combine(HeadsProgram, "allTrans.rtf"));

        }

        private void runProgramToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RunApplication.AllTrans();

        }

        private void infoAboutTheProgramToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RunApplication.GSIInfo(this, Path.Combine(HeadsProgram, "gsiData.rtf"));

        }

        private void runProgramToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RunApplication.GSIDataConverter();

        }

        private void infoAboutTheProgramToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RunApplication.UCwinRoadInfo(this, Path.Combine(HeadsProgram, "ucWinRoad.rtf"));

        }

        private void runProgramToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RunApplication.UCWinRoad();
        }
        private void gPSTransformationGuideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunApplication.GPS_TransformationGuide(this, Path.Combine(HeadsProgram, "gpsTransformation.rtf"));

        }
        #endregion External Program

        private void tsmi_geotechnics_click(object sender, EventArgs e)
        {
            BridgeAnalysisDesign.Geotechnics.frm_Geotechnics frm = new BridgeAnalysisDesign.Geotechnics.frm_Geotechnics(this);
            frm.Owner = this;
            frm.Show();
        }

        #region IApplication Members

        //Chiranjit [2011 09 30]    Write_LiveLoad_LL_TXT
        /// <summary>
        /// Write the Live Load Data file LL.TXT
        /// </summary>
        /// <param name="folder_path">Working Folder Path</param>
        /// <param name="isMTon">true if the unit is MTon ,  false if the unit is kN</param>
        public void Write_LiveLoad_LL_TXT(string folder_path, bool isMTon, eDesignStandard des)
        {
            bool IsAASHTO = (des == eDesignStandard.BritishStandard);
            List<string> list = new List<string>();
            string file_name = Path.Combine(folder_path, "LL.TXT");
            try
            {
                if(des == eDesignStandard.BritishStandard)
                //if (IsAASHTO)
                {
                    if (isMTon)
                    {
                        #region MTon
                        list.Add("FILE LL.TXT");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 1 LRFD_HTL57");
                        list.Add("10.5 10.5 10.5 10.5 10.5 4.5");
                        list.Add("1.6 4.572 4.572 1.6 4.572");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 2 LRFD_HL93_HS20");
                        list.Add("4.0 16.0 16.0");
                        list.Add("4.2672 4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 3 LRFD_HL93_H20 ");
                        list.Add("4.0 16.0");
                        list.Add("4.2672");
                        list.Add("1.80");
                        list.Add(string.Format(""));

                        string[] loads = { "UNIT", "25", "30", "37.5", "45" };
                        int count = 4;

                        double ll = 0.0;
                        foreach (var item in loads)
                        {
                            for (int i = 6; i <= 26; i += 5)
                            {
                                ll = MyList.StringToDouble(item, 1.0);
                                list.Add(string.Format(""));
                                list.Add(string.Format("TYPE {0} HB_{1}_{2} ", count++, item, i));
                                list.Add(string.Format("{0:f2} {0:f2} {0:f2} {0:f2} ", ll));
                                list.Add(string.Format("1.8 {0:f1} 1.8 ", i));
                                list.Add(string.Format("1.0 "));
                                list.Add(string.Format(""));
                            }
                        }
                        list.Add("");
                        #region Chiranjit [2014 09 08] add british load
                        //list.Add("TYPE 4 IRCCLASSA");
                        //list.Add("2.7 2.7 11.4 11.4 6.8 6.8 6.8 6.8");
                        //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        //list.Add("1.80");
                        //list.Add("");
                        //list.Add("TYPE 5 IRCCLASSB");
                        //list.Add("1.6 1.6 6.8 6.8 4.1 4.1 4.1 4.1");
                        //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        //list.Add("1.80");
                        //list.Add("");
                        //list.Add("TYPE 6 IRC70RTRACK");
                        //list.Add("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0");
                        //list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
                        ////list.Add("0.84"); //chiranjit [2013 05 12]
                        //list.Add("2.900");
                        //list.Add("");
                        //list.Add("TYPE 7 IRC70RWHEEL");
                        ////list.Add("8.0 12.0 12.0 17.0 17.0 17.0 17.0");
                        //list.Add("17.0 17.0 17.0 17.0 12.0 12.0 8.0");
                        //list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
                        //list.Add("2.900");
                        ////list.Add("2.38");
                        //list.Add("");
                        //list.Add("TYPE 8 IRCCLASSAATRACK");
                        //list.Add("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0");
                        //list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
                        //////list.Add("0.85");//chiranjit [2013 05 12]
                        //list.Add("2.9");
                        //list.Add("");
                        //list.Add("TYPE 9 IRC24RTRACK");
                        //list.Add("2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5");
                        //list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
                        ////list.Add("0.36");//chiranjit [2013 05 12]
                        //list.Add("2.2");
                        //list.Add("");
                        //list.Add("TYPE 10 BG_RAIL_1");
                        //list.Add("24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52");
                        //list.Add("2.05 1.95 5.56 1.95 2.05 5.94 2.05 1.95 5.56 1.95 2.05");
                        //list.Add("1.676");
                        //list.Add("");
                        //list.Add("TYPE 11 BG_RAIL_2");
                        //list.Add("22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06");
                        //list.Add("1.65 1.65 6.4 1.65 1.65 3.0 1.65 1.65 6.4 1.65 1.65");
                        //list.Add("1.676");
                        //list.Add("");
                        //list.Add("TYPE 12 MG_RAIL_1");
                        //list.Add("11.87 11.87 11.87 11.87 11.28 12.94 12.94 12.94 12.94 7.94");
                        //list.Add("1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197");
                        //list.Add("1.676");
                        //list.Add("");
                        //list.Add("TYPE 13 MG_RAIL_2");
                        //list.Add("9.61 9.61 9.61 9.61 9.12 10.49 10.49 10.49 10.49 6.47");
                        //list.Add("1.372 1.372 1.372 2.806 1.829 1.346 1.346 1.397 2.197");
                        //list.Add("1.676");
                        //list.Add(string.Format(""));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("TYPE 14 IRC70RW40TBM"));
                        //list.Add(string.Format("5.0 5.0 5.0 5.0 "));
                        //list.Add(string.Format("0.795 0.38 0.795"));
                        //list.Add(string.Format("2.79"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("TYPE 15 IRC70RW40TBL"));
                        //list.Add(string.Format("10.0 10.0 "));
                        //list.Add(string.Format("1.93 "));
                        //list.Add(string.Format("2.79"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("TYPE 16 IRC40RWHEEL"));
                        //list.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0"));
                        //list.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
                        //list.Add(string.Format("2.740"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format(""));
                        #endregion Chiranjit [2014 09 08] add british load
                        File.WriteAllLines(file_name, list.ToArray());
                        #endregion MTon Unit MTon
                    }
                    else
                    {
                        #region kN   Unit Kilo Newton
                        list.Add("FILE LL.TXT");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 1 LRFD_HTL57");
                        list.Add("105 105 105 105 105 45");
                        list.Add("1.6 4.572 4.572 1.6 4.572");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 2 LRFD_HL93_HS20");
                        list.Add("40 160 160");
                        list.Add("4.2672 4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 3 LRFD_HL93_H20 ");
                        list.Add("40 160");
                        list.Add("4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");

                        string[] loads = { "UNIT", "25", "30", "37.5", "45" };
                        int count = 4;


                        double ll = 0.0;

                        foreach (var item in loads)
                        {
                            for (int i = 6; i <= 26; i += 5)
                            {
                                ll = MyList.StringToDouble(item, 10.0);

                                list.Add(string.Format(""));
                                list.Add(string.Format("TYPE {0} HB_{1}_{2} ", count++, item, i));
                                list.Add(string.Format("{0:f2} {0:f2} {0:f2} {0:f2} ", ll));
                                list.Add(string.Format("1.8 {0:f1} 1.8 ", i));
                                list.Add(string.Format("1.0 "));
                                list.Add(string.Format(""));
                            }
                        }
                        //list.Add("TYPE 4 IRCCLASSA");
                        //list.Add("27 27 114 114 68 68 68 68");
                        //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        //list.Add("1.80");
                        //list.Add("");
                        //list.Add("TYPE 5 IRCCLASSB");
                        //list.Add("16 16 68 68 41 41 41 41");
                        //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        //list.Add("1.80");
                        //list.Add("");
                        //list.Add("TYPE 6 IRC70RTRACK");
                        //list.Add("70 70 70 70 70 70 70 70 70 70");
                        //list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
                        //list.Add("2.9");
                        //list.Add("");
                        //list.Add("TYPE 7 IRC70RWHEEL");
                        ////list.Add("80 120 120 170 170 170 170");
                        //list.Add("170.0 170.0 170.0 170.0 120.0 120.0 80.0");
                        //list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
                        //list.Add("2.900");
                        //list.Add("");
                        //list.Add("TYPE 8 IRCCLASSAATRACK");
                        //list.Add("70 70 70 70 70 70 70 70 70 70");
                        //list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
                        //list.Add("2.9");
                        //list.Add("");
                        //list.Add("TYPE 9 IRC24RTRACK");
                        //list.Add("25 25 25 25 25 25 25 25 25 25");
                        //list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
                        //list.Add("2.2");
                        //list.Add("");
                        //list.Add("");

                        //list.Add("TYPE 10 BG_RAIL_1");
                        //list.Add("245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 24.52");
                        //list.Add("2.97 2.05 1.95 5.56 1.95 2.05 5.94 2.05 1.95 5.56 1.95 2.05");
                        //list.Add("1.8");
                        //list.Add("");

                        //list.Add("TYPE 11 BG_RAIL_2");
                        //list.Add("220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6");
                        //list.Add("1.5 1.65 1.65 6.4 1.65 1.65 3.0 1.65 1.65 6.4 1.65 1.65");
                        //list.Add("1.9");
                        //list.Add("");

                        //list.Add("TYPE 12 MG_RAIL_1");
                        //list.Add("118.7 118.7 118.7 118.7 112.8 129.4 129.4 129.4 129.4 79.4");
                        //list.Add("1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197");
                        //list.Add("1.676");


                        //list.Add("TYPE 13 MG_RAIL_2");
                        //list.Add("96.1 96.1 96.1 96.1 91.2 104.9 104.9 104.9 104.9 64.7");
                        //list.Add("1.372 1.372 1.372 2.806 1.829 1.346 1.346 1.397 2.197");
                        //list.Add("1.676");


                        //list.Add(string.Format(""));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("TYPE 14 IRC40RWHEEL"));
                        //list.Add(string.Format("120 120 120 70 70 50"));
                        //list.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
                        //list.Add(string.Format("2.740"));
                        //list.Add(string.Format(""));


                        File.WriteAllLines(file_name, list.ToArray());
                        #endregion kN   Unit Kilo Newton
                    }
                }
                else if (des == eDesignStandard.LRFDStandard)
                //if (IsAASHTO)
                {
                    if (isMTon)
                    {
                        #region MTon
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("FILE LL.TXT"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 1 LRFD_HTL57"));
                        list.Add(string.Format("10.5 10.5 10.5 10.5 10.5 4.5 "));
                        list.Add(string.Format("1.6 4.572 4.572 1.6 4.572 "));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 2 LRFD_HL93_HS20"));
                        list.Add(string.Format("4.0 16.0 16.0 "));
                        list.Add(string.Format("4.2672 4.2672 "));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 3 LRFD_HL93_H20"));
                        list.Add(string.Format("4.0 16.0 "));
                        list.Add(string.Format("4.2672 "));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 4 LRFD_H30S24"));
                        list.Add(string.Format("6.0 24.0 24.0 "));
                        list.Add(string.Format("4.25 8.0"));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));


                        File.WriteAllLines(file_name, list.ToArray());
                        #endregion MTon Unit MTon
                    }
                    else
                    {
                        #region kN   Unit Kilo Newton
                        list.Add(string.Format("FILE LL.TXT"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 1 LRFD_HTL57"));
                        list.Add(string.Format("105 105 105 105 105 45 "));
                        list.Add(string.Format("16.0 45.72 45.72 16.0 45.72 "));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 2 LRFD_HL93_HS20"));
                        list.Add(string.Format("40 160 160 "));
                        list.Add(string.Format("42.672 42.672 "));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 3 LRFD_HL93_H20"));
                        list.Add(string.Format("40 160 "));
                        list.Add(string.Format("42.672 "));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 4 LRFD_H30S24"));
                        list.Add(string.Format("60.0 240.0 240.0 "));
                        list.Add(string.Format("42.5 80.0"));
                        list.Add(string.Format("1.800"));
                        list.Add(string.Format(""));


                        File.WriteAllLines(file_name, list.ToArray());
                        #endregion kN   Unit Kilo Newton
                    }
                }
                else
                {
                    if (isMTon)
                    {
                        #region MTon
                        list.Add("FILE LL.TXT");
                        list.Add("");
                        list.Add("TYPE 1 IRCCLASSA");
                        list.Add("2.7 2.7 11.4 11.4 6.8 6.8 6.8 6.8");
                        //list.Add("6.8 6.8 6.8 6.8 11.4 11.4 11.4 2.7");
                        list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("TYPE 2 IRCCLASSB");
                        //list.Add("4.1 4.1 4.1 4.1 6.8 6.8 1.6 1.6");
                        list.Add("1.6 1.6 6.8 6.8 4.1 4.1 4.1 4.1");
                        list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 3 LRFD_HTL57");
                        list.Add("10.5 10.5 10.5 10.5 10.5 4.5");
                        list.Add("1.6 4.572 4.572 1.6 4.572");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 4 LRFD_HL93_HS20");
                        list.Add("4.0 16.0 16.0");
                        list.Add("4.2672 4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 5 LRFD_HL93_H20 ");
                        list.Add("4.0 16.0");
                        list.Add("4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 6 IRC70RTRACK");
                        list.Add("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0");
                        list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
                        //list.Add("0.84");
                        list.Add("2.9");
                        list.Add("");
                        list.Add("TYPE 7 IRC70RWHEEL");
                        list.Add("17.0 17.0 17.0 17.0 12.0 12.0 8.0");
                        //list.Add("8.0 12.0 12.0 17.0 17.0 17.0 17.0");
                        list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
                        //list.Add("0.450 1.480 0.450");
                        list.Add("2.90");
                        list.Add("");
                        list.Add("TYPE 8 IRCCLASSAATRACK");
                        list.Add("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0");
                        list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
                        //list.Add("0.85");
                        list.Add("2.9");
                        list.Add("");
                        list.Add("TYPE 9 IRC24RTRACK");
                        list.Add("2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5");
                        list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
                        list.Add("2.9");
                        list.Add("");
                        list.Add("TYPE 10 BG_RAIL_1");
                        list.Add("24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52");
                        //list.Add("2.97 2.05 1.95 5.56 1.95 2.05 5.94 2.05 1.95 5.56 1.95 2.05 2.97");
                        list.Add("2.05 1.95 5.56 1.95 2.05 5.94 2.05 1.95 5.56 1.95 2.05");
                        list.Add("1.676");
                        list.Add("");

                        list.Add("TYPE 11 BG_RAIL_2");
                        list.Add("22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06");
                        list.Add("1.65 1.65 6.4 1.65 1.65 3.0 1.65 1.65 6.4 1.65 1.65");
                        //list.Add("1.5 1.65 1.65 6.4 1.65 1.65 3.0 1.65 1.65 6.4 1.65 1.65 1.5");
                        list.Add("1.676");
                        list.Add("");

                        //list.Add("TYPE 9 MG_RAIL_1");
                        //list.Add("15.69 15.69 15.69 15.69 15.69 15.69 15.69 15.69 15.69 15.69 15.69 15.69");
                        //list.Add("1.864 1.676 1.829 4.47 1.829 1.676 1.864 1.864 1.676 1.829 4.47 1.829 1.676 1.864");
                        //list.Add("1.676");
                        //list.Add("");

                        //list.Add("TYPE 10 MG_RAIL_2");
                        //list.Add("15.69 15.69 15.69 15.69 15.69 15.69 15.69 15.69 ");
                        //list.Add("2.397 1.70 4.956 1.70 2.397 2.397 1.70 4.956 1.70 2.397");
                        //list.Add("1.676");
                        //list.Add("");

                        //list.Add("TYPE 11 MG_RAIL_3");
                        //list.Add("11.86 11.86 11.86 11.86 11.28 12.94 12.94 12.94 12.94 7.94 11.86 11.86 11.86 11.86 11.28 12.94 12.94 12.94 12.94 7.94");
                        //list.Add("1.473 1.372 2.286 1.372 2.68 2.133 1.346 1.346 1.397 2.197 1.778 1.473 1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197 1.778");
                        //list.Add("1.676");
                        ////list.Add("2.0");
                        //list.Add("");

                        list.Add("TYPE 12 MG_RAIL_1");
                        list.Add("11.87 11.87 11.87 11.87 11.28 12.94 12.94 12.94 12.94 7.94");
                        list.Add("1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197");
                        list.Add("1.676");
                        list.Add("");



                        list.Add("TYPE 13 MG_RAIL_2");
                        list.Add("9.61 9.61 9.61 9.61 9.12 10.49 10.49 10.49 10.49 6.47");
                        list.Add("1.372 1.372 1.372 2.806 1.829 1.346 1.346 1.397 2.197");
                        list.Add("1.676");
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 14 IRC70RW40TBM"));
                        list.Add(string.Format("5.0 5.0 5.0 5.0 "));
                        list.Add(string.Format("0.795 0.38 0.795"));
                        list.Add(string.Format("2.79"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 15 IRC70RW40TBL"));
                        list.Add(string.Format("10.0 10.0 "));
                        list.Add(string.Format("1.93 "));
                        list.Add(string.Format("2.79"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 16 IRC40RWHEEL"));
                        list.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0"));
                        list.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
                        list.Add(string.Format("2.740"));
                        list.Add(string.Format(""));
                        File.WriteAllLines(file_name, list.ToArray());
                        #endregion MTon Unit MTon
                    }
                    else
                    {
                        #region kN   Unit Kilo Newton
                        list.Add("FILE LL.TXT");
                        list.Add("");

                        list.Add("FILE LL.TXT");
                        list.Add("");
                        list.Add("TYPE 1 IRCCLASSA");
                        //list.Add("68 68 68 68 114 114 114 27");
                        list.Add("27 27 114 114 68 68 68 68");
                        //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("TYPE 2 IRCCLASSB");
                        //list.Add("41 41 41 41 68 68 16 16");
                        list.Add("16 16 68 68 41 41 41 41");
                        list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("TYPE 3 IRC70RTRACK");
                        list.Add("70 70 70 70 70 70 70 70 70 70");
                        list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
                        list.Add("2.9");
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 4 LRFD_HTL57");
                        list.Add("105 105 105 105 105 45");
                        list.Add("1.6 4.572 4.572 1.6 4.572");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 5 LRFD_HL93_HS20");
                        list.Add("40 160 160");
                        list.Add("4.2672 4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 6 LRFD_HL93_H20 ");
                        list.Add("40 160");
                        list.Add("4.2672");
                        list.Add("1.80");
                        list.Add("");
                        list.Add("");
                        list.Add("TYPE 7 IRC70RWHEEL");
                        //list.Add("80 120 120 170 170 170 170");
                        list.Add("170.0 170.0 170.0 170.0 120.0 120.0 80.0");
                        list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
                        list.Add("2.38");
                        list.Add("");
                        list.Add("TYPE 8 IRCCLASSAATRACK");
                        list.Add("70 70 70 70 70 70 70 70 70 70");
                        list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
                        list.Add("2.9");
                        list.Add("");
                        list.Add("TYPE 9 IRC24RTRACK");
                        list.Add("25 25 25 25 25 25 25 25 25 25");
                        list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
                        list.Add("2.2");
                        list.Add("");

                        list.Add("TYPE 10 BG_RAIL_1");
                        list.Add("245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 245.2 24.52");
                        list.Add("2.97 2.05 1.95 5.56 1.95 2.05 5.94 2.05 1.95 5.56 1.95 2.05");
                        list.Add("1.8");
                        list.Add("");

                        list.Add("TYPE 11 BG_RAIL_2");
                        list.Add("220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6 220.6");
                        list.Add("1.5 1.65 1.65 6.4 1.65 1.65 3.0 1.65 1.65 6.4 1.65 1.65");
                        list.Add("1.9");
                        list.Add("");

                        //list.Add("TYPE 9 RAIL_METREGAUGE1");
                        //list.Add("156.9 156.9 156.9 156.9 156.9 156.9 156.9 156.9 156.9 156.9 156.9 156.9");
                        //list.Add("1.864 1.676 1.829 4.47 1.829 1.676 1.864 1.864 1.676 1.829 4.47 1.829 1.676 1.864");
                        //list.Add("1.8");
                        //list.Add("");

                        //list.Add("TYPE 10 RAIL_METREGAUGE2");
                        //list.Add("156.9 156.9 156.9 156.9 156.9 156.9 156.9 156.9 ");
                        //list.Add("2.397 1.70 4.956 1.70 2.397 2.397 1.70 4.956 1.70 2.397");
                        //list.Add("1.8");
                        //list.Add("");

                        //list.Add("TYPE 11 RAIL_METREGAUGE2");
                        //list.Add("118.6 118.6 118.6 118.6 112.8 129.4 129.4 129.4 129.4 79.4 118.6 118.6 118.6 118.6 112.8 129.4 129.4 129.4 129.4 79.4");
                        //list.Add("1.473 1.372 2.286 1.372 2.68 2.133 1.346 1.346 1.397 2.197 1.778 1.473 1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197 1.778");
                        //list.Add("2.0");
                        //list.Add("");

                        list.Add(string.Format(""));

                        list.Add("TYPE 12 MG_RAIL_1");
                        list.Add("118.7 118.7 118.7 118.7 112.8 129.4 129.4 129.4 129.4 79.4");
                        list.Add("1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197");
                        list.Add("1.676");
                        list.Add(string.Format(""));


                        list.Add("TYPE 13 MG_RAIL_2");
                        list.Add("96.1 96.1 96.1 96.1 91.2 104.9 104.9 104.9 104.9 64.7");
                        list.Add("1.372 1.372 1.372 2.806 1.829 1.346 1.346 1.397 2.197");
                        list.Add("1.676");

                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        list.Add(string.Format("TYPE 14 IRC40RWHEEL"));
                        list.Add(string.Format("120 120 120 70 70 50"));
                        list.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
                        list.Add(string.Format("2.740"));
                        list.Add(string.Format(""));




                        File.WriteAllLines(file_name, list.ToArray());
                        #endregion kN   Unit Kilo Newton
                    }
                }
            }
            catch (Exception ex) { }
            list.Clear();
            list = null;
        }
        public void Write_LiveLoad_LL_TXT(string folder_path)
        {
            try
            {
                LiveLoads.Save_LL_TXT(folder_path, true);
                return;
            }
            catch (Exception ex) { }
        }
        public void Write_Default_LiveLoad_Data()
        {
            try
            {
                Write_LiveLoad_LL_TXT(Application.StartupPath, true, DesignStandard);
                Write_LiveLoad_LL_TXT(Path.GetDirectoryName(LL_TXT_Path), true, DesignStandard);
                LiveLoads = new LiveLoadCollections(LL_TXT_Path);
                return;
            }
            catch (Exception ex) { }
        }

        #endregion

        #region IApplication Members


        public void ShowAnalysisResult(string exeFileName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IApplication Members


        public eDesignStandard DesignStandard
        {
            get
            {
                if (Tables == null) return (IsAASTHO ? eDesignStandard.BritishStandard : eDesignStandard.IndianStandard);
                return Tables.DesignStandard;
            }
            set
            {
                if (Tables == null)
                    Tables = new ASTRA_Tables(value);
                else
                    Tables.DesignStandard = value;
            }
        }


        #endregion

        #region IApplication Members


        public ASTRA_Tables Tables { get; set; }

        #endregion

        private void tsmi_cable_stayed_bridge_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            
            Form frm;
            SelectDesignStandard();
            if (tsmi == tsmi_cable_stayed_bridge)
            {
                ShowTimerScreen(eASTRAImage.Cable_Stayed_Bridge);

                if (DesignStandard == eDesignStandard.IndianStandard)
                {
                    frm = new frmCableStayed_LS_Stage(this);
                    frm.Owner = this;
                    frm.Show();
                }
                else if (DesignStandard == eDesignStandard.BritishStandard)
                {
                    frm = new frmCableStayed_LS_Stage(this);
                    frm.Owner = this;
                    frm.Show();
                }
                else if (DesignStandard == eDesignStandard.LRFDStandard)
                {
                    frm = new frmCableStayed_AASHTO(this);
                    frm.Owner = this;
                    frm.Show();
                }
                else
                {
                    frm = new frmCableStayed_LS(this);
                    //frm = new frmCableStayed_LS_Stage(this);
                    frm.Owner = this;
                    frm.Show();
                }
            }
            if (tsmi == tsmi_extradossed_side_towers)
            {
                ShowTimerScreen(eASTRAImage.Extradosed_SideTowers);

                //if (DesignStandard == eDesignStandard.IndianStandard)
                //{
                frm = new frm_Extradosed_Stage(this, eASTRADesignType.Extradossed_Side_Towers_Bridge_LS);
                frm.Owner = this;
                frm.Show();
                //}
                //else
                //{
                //    frm = new frm_Extradosed(this, eASTRADesignType.Extradossed_Side_Towers_Bridge_LS);
                //    frm.Owner = this;
                //    frm.Show();
                //}
            }
            if (tsmi == tsmi_extradossed_central_towers)
            {
                ShowTimerScreen(eASTRAImage.Extradosed_CentralTowers);

                //if (DesignStandard == eDesignStandard.IndianStandard)
                //{
                frm = new frm_Extradosed_Stage(this, eASTRADesignType.Extradossed_Central_Towers_Bridge_LS);
                frm.Owner = this;
                frm.ShowDialog();
                //}
                //else
                //{
                //    frm = new frm_Extradosed(this, eASTRADesignType.Extradossed_Central_Towers_Bridge_LS);
                //    frm.Owner = this;
                //    frm.ShowDialog();
                //}
            }
        }

        #region IApplication Members


        public LiveLoadCollections LiveLoads { get; set; }

        #endregion

        private void tsmi_Steel_Truss_Bridge_Warren_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_Steel_Truss_Bridge_Warren_1.Name)
            {
                ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
                //frmCompleteDesign f_cd = new frmCompleteDesign(this);
                //chiranjit comment this line
                //frm_Steel_Truss_Warren f_cd = new frm_Steel_Truss_Warren(this);

                BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren f_cd = new BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren(this);

                f_cd.Owner = this;
                f_cd.Show();
            }
            else if (tsmi.Name == tsmi_Steel_Truss_Bridge_Warren_2.Name)
            {

                ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
                //frmCompleteDesign f_cd = new frmCompleteDesign(this, true);
                //frm_Steel_Truss_Warren f_cd = new frm_Steel_Truss_Warren(this, true); //Chiranjit Comment


                BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren f_cd = new BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren(this, true);
                f_cd.Owner = this;
                f_cd.Show();
            }
            else if (tsmi.Name == tsmi_Steel_Truss_Bridge_Warren_3.Name)
            {

                ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge);
                //frmCompleteDesign f_cd = new frmCompleteDesign(this, true);
                //frm_Steel_Truss_Warren3 f_cd = new frm_Steel_Truss_Warren3(this);
                BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren f_cd = new BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren(this, eASTRADesignType.Steel_Truss_Bridge_Warren_3);
                f_cd.Owner = this;
                f_cd.Show();
            }
            else if (tsmi.Name == tsmi_Steel_Truss_Bridge_K_Type.Name)
            {
                ShowTimerScreen(eASTRAImage.Steel_Truss_Bridge_K_type);
                //LimitStateMethod.Steel_Truss.frm_Steel_Truss_K_type  f_cd = new LimitStateMethod.Steel_Truss.frm_Steel_Truss_K_type(this, false);
                BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren f_cd = new BridgeAnalysisDesign.SteelTruss.frm_Steel_Warren(this, eASTRADesignType.Steel_Truss_Bridge_K_Type);
                f_cd.Owner = this;
                f_cd.Show();
            }
        }
        frmLockedVersion flc = null;

        eVersionType Last_version { get; set; }
        private void tmr_Tick(object sender, EventArgs e)
        {
            string flls = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "astra.pmt");
            //string flls = Path.Combine(Application.StartupPath, "astra.pmt");
            //IsDemo = !frmLockedVersion.CheckHasp();
            if (flc == null)
                flc = new frmLockedVersion(false);

            Version_Type = LockProgram.Get_LockedVersion();

            IsDemo = (Version_Type == eVersionType.Demo);

            if (Version_Type == eVersionType.Enterprise_Bridge || Version_Type == eVersionType.Professional_Bridge)
            {
                Is_BridgeDemo = true;
                Is_StructureDemo = false;
            } 
            else if (Version_Type == eVersionType.Enterprise_Structure || Version_Type == eVersionType.Professional_Structure)
            {
                Is_BridgeDemo = true;
                Is_StructureDemo = true;
            }
            else
            {
                Is_BridgeDemo = false;
                Is_StructureDemo = false;
            }



            if (!IsDemo)
            {
                tmr.Interval = 9999;
            }


            try
            {
                if (IsDemo)
                {

                    if (Last_version != eVersionType.Demo)
                    {
                        Last_version = eVersionType.Demo;
                        //this.BackgroundImage = Properties.Resources.ASTRA_Demo;
                        this.BackgroundImage = Properties.Resources.ASTRA_Unauthorized;
                    }
                }
                else
                {
                    if (Version_Type == eVersionType.Enterprise_Bridge || Version_Type == eVersionType.Enterprise_Structure)
                    {
                        if (Last_version != Version_Type)
                        {
                            Last_version = Version_Type;
                            this.BackgroundImage = Properties.Resources.ASTRA_Enterprise;
                        }
                    }
                    else
                    {
                        if (Last_version != Version_Type)
                        {
                            Last_version = eVersionType.Professional_Bridge;
                            this.BackgroundImage = Properties.Resources.ASTRA_Professional;
                        }

                    }
                }

            }
            catch (Exception ex) { }
            //IsDemo = !flc.IsProfessional_BridgeVersion();

            //IsDemo = !flc.Get_Authorization_Code();

            //if(flc.Check_ASTRA_Lock)


            //IsDemo = !flc.IsProfessional_BridgeVersion() && !flc.IsProfessional_StructuralVersion();


            //if (IsDemo)
            //    tmr.Interval = 1999;
            //else
            //    tmr.Interval = 0999;

            //tmr.Interval = 999;

            //if (frmLockedVersion.CheckHasp())
            //tsmi_license.Visible = !flc.IsActivate && frmLockedVersion.CheckHasp();
            //toolStripSeparator9.Visible = !flc.IsActivate && frmLockedVersion.CheckHasp();


            tsmi_license.Visible = true;
            toolStripSeparator9.Visible = true;


            this.tsl_text.Visible = true;


            if (IsDemo)
            {
                if (File.Exists(flls))
                    File.Delete(flls);
                this.Text = "ASTRA Pro [DEMO] " + ASTRA_Pro;
                if (Version_Type == eVersionType.Enterprise_Bridge)
                    this.tsl_text.Text = "ASTRA Pro Dongle is not found at any USB port. This is an Unauthorized / Demo Version. Please contact TechSOFT for Authorized Version.";
                else
                    this.tsl_text.Text = "ASTRA Pro Dongle is not found at any USB port. This is an Unauthorized Version.";
            }
            else
            {
                if (Version_Type == eVersionType.Activation_Trial)
                    this.tsl_text.Text = "ASTRA Pro Dongle is found but it is not Authorized. " + LockProgram.Get_Activation() + " activation(s) remaining. Please enter Authorization code.";
                else if (Version_Type == eVersionType.Enterprise_Structure || Version_Type == eVersionType.Professional_Structure)
                {
                    this.tsl_text.Text = "ASTRA Pro Authorized Dongle is found. This is a Professional Version of ASTRA Pro. Thank you for using ASTRA Pro.";
                }
                else if (Version_Type == eVersionType.Enterprise_Bridge || Version_Type == eVersionType.Professional_Bridge)
                {
                    //this.tsl_text.Text = "ASTRA Pro Bridge Authorized Dongle found. This is a Professional Version of ASTRA Pro. Thank you for using ASTRA Pro.";
                    //this.tsl_text.Text = "Authorized Dongle for Bridge Design with ASTRA Pro found. Authorized Dongle for Building Design with ASTRA Pro not found.";
                    this.tsl_text.Text = "ASTRA Pro Authorized Dongle is found for Bridge Design. Authorized Dongle is not found for Structure Design.";
                }
               
                //}
            }
            if (DesignStandard == eDesignStandard.IndianStandard)
                this.Text = "ASTRA Pro [IS] " + ASTRA_Pro;
            else
                this.Text = "ASTRA Pro [BS] " + ASTRA_Pro;

        }
        //Moving Load Data
        //Chiranjit [2012 11 07]
        private void tsmi_ll_data_Click(object sender, EventArgs e)
        {
            Show_LL_Dialog();
        }

        public void Show_LL_Dialog()
        {

            frmMovingLoadData frm = new frmMovingLoadData(this);
            frm.Owner = this;
            frm.ShowDialog();
        }


        public bool Read_Form_Record(object frm, string work_folder)
        {
            try
            {

                Save_FormRecord.iApp = this;
                Save_FormRecord.Read_All_Data(frm, work_folder);

                //this.DesignStandard
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        public bool Save_Form_Record(object frm, string work_folder)
        {
            try
            {

                //if (LastDesignWorkingFolder != work_folder)
                //{
                //    //if (MessageBox.Show(this, "Save Analysis and Design Data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                //    //{
                //        Save_FormRecord.iApp = this;
                //        Save_FormRecord.Write_All_Data(frm, work_folder);
                //        return true;
                //    //}
                //}
                Save_FormRecord.iApp = this;
                Save_FormRecord.Write_All_Data(frm, work_folder);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        public bool Save_Form_Record(Control ctrl, string work_folder)
        {
            try
            {

                //if (LastDesignWorkingFolder != work_folder)
                //{
                //    //if (MessageBox.Show(this, "Save Analysis and Design Data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                //    //{
                //        Save_FormRecord.iApp = this;
                //        Save_FormRecord.Write_All_Data(frm, work_folder);
                //        return true;
                //    //}
                //}
                Save_FormRecord.iApp = this;
                Save_FormRecord.Write_All_Data(ctrl, work_folder);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        private void tsmi_videos_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_videos.Name)
            {
                frmVideos fv = new frmVideos(this);
                fv.Owner = this;
                fv.Show();
            }
            else if (tsmi.Name == tsmi_design_manual.Name)
            {
                string fn = Path.Combine(Application.StartupPath, "ASTRAHelp//ASTRA Pro Design Manual.pdf");
                //if (File.Exists(fn))
                RunExe(fn);
            }
            else if (tsmi.Name == tsmi_users_manual.Name)
            {
                string fn = Path.Combine(Application.StartupPath, "ASTRAHelp//ASTRA Pro User's Manual.pdf");
                //if (File.Exists(fn))
                RunExe(fn);
                //else
                //{

                //}
            }

        }

        private void tsmi_RE_Wall_Design_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this, "This module is not available.\n\nPlease contact TechSOFT.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Chiranjit [2013 04 02]
            //Add RE Wall Design Program

            ShowTimerScreen(eASTRAImage.RE_Wall);
            BridgeAnalysisDesign.RE_Wall.frm_RE_Wall f = new BridgeAnalysisDesign.RE_Wall.frm_RE_Wall(this);
            f.Owner = this;
            f.Show();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //Chiranjit [2013 05 14]
        public ProgressList Progress_Works
        {
            get
            {
                return frm_ProgressList.Work_List;
            }
            set
            {
                frm_ProgressList.Work_List = value;
            }
        }


        public bool Is_Progress_Cancel
        {
            get
            {
                return frm_ProgressList.IsCancel;
            }
            set
            {
                frm_ProgressList.IsCancel = value;
            }
        }

        private void tsmi_cont_PSC_Box_Girder_Bridge_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            ShowTimerScreen(eASTRAImage.Continuous_PSC_Box_Girder);
            BridgeAnalysisDesign.PSC_BoxGirder.frm_Continuous_Box_Girder f = new BridgeAnalysisDesign.PSC_BoxGirder.frm_Continuous_Box_Girder(this);
            f.Owner = this;
            f.Show();
        }

        private void tsmi_RCC_TBeam_LS_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();

            Show_T_Girder_Bridge_Limit_State();
        }

        private void tsmi_Pre_Stressed_Bridge_LS_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            //if (((ToolStripMenuItem)sender).Name == tsmi_PSC_I_Girder_Bridge_LS.Name)
            {
                ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
                frm_PSC_I_Girder_LS frm = new frm_PSC_I_Girder_LS(this);
                frm.Owner = this;
                frm.Show();
            }
        }

        Progress.frm_Progress progress;

        public void Open_Progress()
        {
            progress = new Progress.frm_Progress();
            progress.Owner = this;
            //progress.ShowDialog();
            progress.Show();
        }
        public IProgress Progress
        {
            get
            {

                //if (progress == null)
                //{

                //    Open_Progress();
                //    //tdss = new Thread(new ThreadStart(Open_Progress));
                //    //tdss.Start();
                //}
                //else
                //{
                //    if (progress.Close_Flag)
                //    {
                //        Open_Progress();
                //        //if (tdss.IsAlive) tdss.Abort();
                //        //tdss.Start();
                //    }
                //}
                return frm_ProgressList.Instance;

            }
        }

        public void Write_Skew_Data_to_File(string fname, string sap_path)
        {

            if (Path.GetDirectoryName(fname).ToLower() == Path.GetDirectoryName(sap_path).ToLower()) return;




            #region SAP Analysis

            string sap_file = "", sap_file2 = "";

            sap_file = Path.Combine(Path.GetDirectoryName(fname), "SAP_Input_Data.txt");
            sap_file2 = Path.Combine(Path.GetDirectoryName(sap_path), "SAP_Input_Data.txt");



            SAP_Document sap = new SAP_Document();
            SAP_Document sap2 = new SAP_Document();

            if (File.Exists(sap_file)) 
            {
                sap.Read_SAP_Data(sap_file);
            }

            if (File.Exists(sap_file2))
            {
                sap2.Read_SAP_Data(sap_file2);
            }


            //sap_file = Path.Combine(sap_path, "SAP_Input_Data.txt");

            if (File.Exists(sap_file))
            {
                //sap.Read_SAP_Data(sap_file);

                if (sap.Beams.Count > 0)
                {
                    for (int i = 0; i < sap.Beams.Count; i++)
                    {
                        var item = sap.Beams[i];
                        var item2 = sap2.Beams[i];

                        sap.Beams[i] = sap2.Beams[i];
                    }
                }

                sap_file = Path.Combine(Path.GetDirectoryName(sap_file), "inp.tmp");
                File.WriteAllLines(sap_file, sap.Get_SAP_Data().ToArray());

            }
            #endregion SAP Analysis
        }

        public void Write_Data_to_File(string fname, string sap_path)
        {

            if (Path.GetDirectoryName(fname).ToLower() == Path.GetDirectoryName(sap_path).ToLower()) return;


            #region SAP Analysis
            SAP_Document sap = new SAP_Document();

            string sap_file = "", sap_file2 = "";

            sap_file = Path.Combine(Path.GetDirectoryName(sap_path), "SAP_Input_Data.txt");


            sap_file2 = Path.Combine(Path.GetDirectoryName(fname), "inp.tmp");
            SAP_Document sap2 = new SAP_Document();

            BridgeMemberAnalysis bma = new BridgeMemberAnalysis(this, fname);

            //sap_file = Path.Combine(sap_path, "SAP_Input_Data.txt");

            if (File.Exists(sap_file))
            {
                sap.Read_SAP_Data(sap_file);



                if (sap.Joints.Count > 0)
                {
                    //if (dgv.RowCount > 0)
                    {
                        for (int i = 0; i < sap.Joints.Count; i++)
                        {
                            var item = sap.Joints[i];
                            var item2 = bma.Analysis.Joints[i];
                            item.X = item2.X;
                            item.Y = item2.Y;
                            item.Z = item2.Z;
                        }
                    }
                }

                sap_file = Path.Combine(Path.GetDirectoryName(sap_file2), "inp.tmp");
                File.WriteAllLines(sap_file, sap.Get_SAP_Data().ToArray());


                //sap_file = Path.Combine(Path.GetDirectoryName(sap_file2), "inp2.tmp");
                //File.WriteAllLines(sap_file, sap.Get_SAP_Data().ToArray());

            }
            #endregion SAP Analysis
        }

        public bool Show_and_Run_Process_List(ProcessCollection pcol)
        {

            AstraFunctionOne.ProcessList.frm_Process_List ff = new ProcessList.frm_Process_List(pcol, this);
            ff.Owner = this;
            //MessageBox.Show(ff.ShowDialog().ToString());
            if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
            return true;
        }

        public bool Show_and_Run_Stage_Analysis(ProcessCollection pcol)
        {

            AstraAccess.StageAnalysis.frmStageDialog ff = new AstraAccess.StageAnalysis.frmStageDialog(pcol, this);
            ff.Owner = this;
            //MessageBox.Show(ff.ShowDialog().ToString());
            if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
            return true;
        }

        private void tsmi_arch_cable_bridge_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_arch_cable_bridge.Name)
            {
                ShowTimerScreen(eASTRAImage.Arch_Cable_Suspension);

                LimitStateMethod.Steel_Truss.frm_Arch_Cable_Bridge ff = new LimitStateMethod.Steel_Truss.frm_Arch_Cable_Bridge(this);
                ff.Owner = this;
                ff.Show();
            }
            else if (tsmi.Name == tsmi_arch_steel_bridge.Name)
            {
                MessageBox.Show(this, "The module is not attached in this Version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SendMail_Click(object sender, EventArgs e)
        {
            RunExe("SendMail.exe");
        }

        private void tsmi_Bridge_Pier_Pile_Design_Click(object sender, EventArgs e)
        {
            frmPier_Design_with_Piles frm = new frmPier_Design_with_Piles(this);

            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_streamHydrology_Click(object sender, EventArgs e)
        {
            //Open
            RunExe("StreamHydrology.exe");
        }


        Form fMsg ;
        public void Excel_Open_Message()
        {
            try
            {
                fMsg = new frm_Excel_Message();
                fMsg.Show();
            }
            catch (Exception exx) { }
        }
        //Chiranjit [2017 09 22]
        public void Excel_Close_Message()
        {

            try
            {
                fMsg.Close();
            }
            catch (Exception exx) { }
            //MessageBox.Show("The Design is done, the excel report file is created,\n\r" +
            //                "Click on the excel icon in the lower panel at the bottom\n\r" +
            //                "of the screen, to view the Design report.\n\r", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void View_Input_File(string file_name)
        {

            var ll_txt = MyList.Get_LL_TXT_File(file_name);

            if (File.Exists(ll_txt)) RunExe(ll_txt);

            Thread.Sleep(300);
            if (File.Exists(file_name)) System.Diagnostics.Process.Start(file_name);

            //Chiranjit [2016 09 22]
            //frmViewFile fvf = new frmViewFile(this, file_name);
            //fvf.Owner = this;
            //fvf.Show();
        }

        public void ASTRA_TEXT_Input_Data(string file_name)
        {
            AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(file_name, false);
        }

        private void viewInputDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Input_File(FilePath);
        }

        private void tsmi_PSC_I_GIRDER_Program_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();

            frm_PSC_I_Girder fpsc = new frm_PSC_I_Girder(this);
            fpsc.Owner = this;
            fpsc.Show();
        }


        public Image Get_Image(string img_name)
        {
            string img_path = Path.Combine(Application.StartupPath, @"\DESIGN\Sample Images");
            string file_name = Path.Combine(img_path, img_name + ".jpg");


            if (File.Exists(file_name))
            {
                Image img = Image.FromFile(file_name);

                Clipboard.SetImage(img);
                return img;
            }

            return null;

        }


        public eDesignOption Get_Design_Option(string title)
        {
            frmOpen_Bridge_Design fobd = new frmOpen_Bridge_Design(this, title);
            if (File.Exists(fobd.file_path) == false)
                return eDesignOption.New_Design;

            if (fobd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return eDesignOption.None;

            return fobd.Design_Option;
        }

        private void tsmi_ast_basic_info_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_ast_basic_info.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowBasicInfoDialog(this);
            }
            else if (tsmi.Name == tsmi_ast_analysis_type.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowAnalysisDialog(this);

            }
            else if (tsmi.Name == tsmi_ast_nodes.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowNodalDataDialog(this);

            }
            else if (tsmi.Name == tsmi_ast_beam_truss.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowBeamMembersDialog(this);

            }
            else if (tsmi.Name == tsmi_ast_plate.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowPlateAndShellDialog(this);
            }
            else if (tsmi.Name == tsmi_ast_supports.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowSupportDialog(this);
            }
            else if (tsmi.Name == tsmi_ast_nodal_loads.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowMemberBeamLoadDialog(this);
            }
            else if (tsmi.Name == tsmi_ast_self_weight.Name)
            {
                AstraFunctionOne.CAstraFunctionFactory.Instance.ShowSelfWeightDialog(this);
            }
        }

        //Chiranjit [2014 10 26]
        private void tsmi_newAnalysisDataFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            //frmSAPApplication sapFrm = new frmSAPApplication(this);
            //sapFrm.Owner = this;
            //sapFrm.ShowDialog();


            if (tsmi.Name == tsmi_newAnalysisTXTDataFile.Name)
            {
                #region New Text Data File
                //CAstraFunctionFactory.Instance.ShowNewAnalysisDialog(this);
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "TEXT Files (*.txt)|*.txt";
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {

                        TEXT_File = sfd.FileName;
                        flPath = sfd.FileName;

                        //LastDesignWorkingFolder = Path.GetDirectoryName(flPath);
                        List<string> list = new List<string>();
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));
                        //list.Add(string.Format("ASTRA FLOOR PROJECT TITLE"));
                        //list.Add(string.Format("UNIT KN METRES"));
                        //list.Add(string.Format("JOINT COORDINATES"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("MEMBER INCIDENCES"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("MEMBER PROPERTY"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("MATERIAL CONSTANT"));
                        //list.Add(string.Format("E CONCRETE ALL"));
                        //list.Add(string.Format("DEN CONCRETE ALL"));
                        //list.Add(string.Format("PR CONCRETE ALL"));
                        //list.Add(string.Format("SUPPORTS"));
                        //list.Add(string.Format(""));
                        //list.Add(string.Format("LOAD 1 SAMPLE_LOAD"));
                        //list.Add(string.Format("JOINT LOAD"));
                        //list.Add(string.Format("MEMBER LOAD"));
                        //list.Add(string.Format("PERFORM ANALYSIS"));
                        //list.Add(string.Format("FINISH"));
                        list.Add(string.Format(""));
                        list.Add(string.Format(""));

                        File.WriteAllLines(flPath, list.ToArray());

                        fl = new FileInfo(flPath.ToLower());

                        if (fl.Extension == ".txt")
                        {
                            AppDoc.ClearVars();
                            isNew = false;
                            this.FilePath = flPath;
                            setControlEnable();
                            AppDoc.ClearVars();
                            isSave = true;
                        }
                        SetRecentFiles();
                        File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                        File.WriteAllText(Path.Combine(this.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");


                        SET_ENV();
                        File.WriteAllText(Path.Combine(AppFolder, "hds.001"), flPath);
                        File.WriteAllText(Path.Combine(AppFolder, "hds.002"), Path.GetDirectoryName(flPath) + "\\");

                    }
                }
                #endregion New Text Data File

            }
            else if (tsmi.Name == tsmi_newAnalysisSAPDataFile.Name)
            {
                #region New SAP Data File

                //CAstraFunctionFactory.Instance.ShowNewAnalysisDialog(this);
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    //sfd.Filter = "TEXT Files (*.txt)|*.txt;*.sap|All Files (*.*)|*.*";
                    sfd.Filter = "TEXT Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        SAP_File = sfd.FileName;
                        flPath = sfd.FileName;

                        //LastDesignWorkingFolder = Path.GetDirectoryName(flPath);
                        List<string> list = new List<string>();
                        list.Add(string.Format(""));
                        File.WriteAllLines(flPath, list.ToArray());

                        fl = new FileInfo(flPath.ToLower());

                        if (fl.Extension == ".txt")
                        {
                            AppDoc.ClearVars();
                            isNew = false;
                            this.FilePath = flPath;
                            setControlEnable();
                            AppDoc.ClearVars();
                            isSave = true;
                        }
                        SetRecentFiles();
                        File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                        File.WriteAllText(Path.Combine(this.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");


                        SET_ENV();
                        File.WriteAllText(Path.Combine(AppFolder, "hds.001"), flPath);
                        File.WriteAllText(Path.Combine(AppFolder, "hds.002"), Path.GetDirectoryName(flPath) + "\\");

                    }
                }
                #endregion New SAP Data File
            }

            else if (tsmi.Name == tsmi_newAnalysisDWGDataFile.Name)
            {

                //CAstraFunctionFactory.Instance.ShowNewAnalysisDialog(this);
                #region New Drawing Data File
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Drawing Files (*.vdml)|*.vdml;|DXF Files (*.dxf)|*.dxf|DWG Files (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        Drawing_File = sfd.FileName;
                        if (!File.Exists(Drawing_File))
                        {
                            File.WriteAllText(Drawing_File, "");
                        }

                        //LastDesignWorkingFolder = Path.GetDirectoryName(Drawing_File);
                        System.Environment.SetEnvironmentVariable("OPENFILE", Drawing_File);
                        RunViewer();
                    }
                }
                #endregion New Drawing Data File
            }
            else if (tsmi.Name == tsmi_openAnalysisTXTDataFile.Name)
            {
                #region Open Drawing Data File
                #endregion Open Drawing Data File


                #region Open Text Data File


                ofdAst.Filter = "TXT Files (*.txt)|*.txt";
                if (ofdAst.ShowDialog() != DialogResult.Cancel)
                {
                    TEXT_File = ofdAst.FileName;
                    this.FilePath = ofdAst.FileName;
                }
                else
                    return;

                //this.FilePath = opnfileName;
                fl = new FileInfo(flPath.ToLower());

                if (fl.Extension == ".txt")
                {
                    AppDoc.ClearVars();
                    isNew = false;
                    this.FilePath = flPath;
                    setControlEnable();
                    AppDoc.ClearVars();
                    isSave = true;
                }
                SetRecentFiles();
                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(this.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");


                SET_ENV();
                File.WriteAllText(Path.Combine(AppFolder, "hds.001"), flPath);
                File.WriteAllText(Path.Combine(AppFolder, "hds.002"), Path.GetDirectoryName(flPath) + "\\");

                LastDesignWorkingFolder = Path.GetDirectoryName(flPath);




                if (MessageBox.Show(this, "Do you want to open this file with Workspace ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(TEXT_File, false).Show();

                }

                #endregion Open Text Data File
            }
            else if (tsmi.Name == tsmi_openAnalysisExampleTXTDataFile.Name)
            {
                CAstraFunctionFactory.Instance.ShowAnalysisExamplesDialog(this, false);
            }
            else if (tsmi.Name == tsmi_ana_exmpls.Name)
            {
                if (!File.Exists(EXAMPLE_File))
                    CAstraFunctionFactory.Instance.ShowAnalysisExamplesDialog(this, false);


                if (Path.GetFileNameWithoutExtension(EXAMPLE_File).ToUpper().Contains("SAP"))
                {
                    SAP_File = EXAMPLE_File;
                    AstraAccess.ViewerFunctions.Form_SAP_Editor(this).Show();
                }
                else if (Path.GetExtension(EXAMPLE_File).ToUpper().Contains("DWG"))
                {
                    Drawing_File = EXAMPLE_File;
                    AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(Drawing_File, true).Show();
                }
                else
                {
                    TEXT_File = EXAMPLE_File;
                    AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(TEXT_File, false).Show();
                }

                //if (FilePath == "")
                //{
                //    CAstraFunctionFactory.Instance.ShowAnalysisExamplesDialog(this, false);
                //}
                //if (FilePath != "")
                //    AstraAccess.ViewerFunctions.ASTRA_Input_Data(FilePath);




            }
            else if (tsmi.Name == tsmi_openMSExcelDesignReport.Name)
            {
                Open_ASTRA_Worksheet_Dialog();
            }
            else if (tsmi.Name == tsmi_openStructureModelDrawingFile.Name)
            {

                #region Open Drawing Data File


                ofdAst.Filter = "Drawing Files (*.vdml;*.dxf;*.dwg)|*.vdml;*.dxf;*.dwg";
                //ofdAst.Filter = "TEXT Files (*.txt)|*.txt";
                if (ofdAst.ShowDialog() != DialogResult.Cancel)
                {

                    Drawing_File = ofdAst.FileName;

                    LastDesignWorkingFolder = Path.GetDirectoryName(ofdAst.FileName);


                    ASTRA_Pro = "[ Working Folder : \"" + Get_Modified_Path(LastDesignWorkingFolder) + "\" ]";

                    //this.FilePath = ofdAst.FileName;
                }
                else
                    return;

                #endregion Open Drawing Data File
            }
            else if (tsmi.Name == tsmi_saveTXTDataFile.Name)
            {

            }
            else if (tsmi.Name == tsmi_openTXTDataInNotepad.Name)
            {
                ofdAst.Filter = "TXT Files (*.txt)|*.txt";
                if (ofdAst.ShowDialog() != DialogResult.Cancel)
                {
                    this.FilePath = ofdAst.FileName;
                    View_Input_File(this.FilePath);
                    //RunExe(ofdAst.FileName);
                }
            }
            else if (tsmi.Name == tsmi_openStageAnalysisTEXTDataFile.Name)
            {

                frmStageAnalysisWorkspace fstage = new frmStageAnalysisWorkspace(this);
                fstage.Owner = this;
                fstage.Show();



                //ofdAst.Filter = "TXT Files (*.txt)|*.txt";
                //if (ofdAst.ShowDialog() != DialogResult.Cancel)
                //{
                //    this.FilePath = ofdAst.FileName;
                //    //CAstraFunctionFactory.Instance.ShowStageAnalysisDialog(this);

                //    AstraAccess.ViewerFunctions.Form_Stage_Analysis(this).Show();

                //}
            }
            else if (tsmi.Name == tsmi_openSAPDataFile.Name)
            {
                #region Open SAP Data File

                ofdAst.Filter = "TXT Files (*.txt)|*.txt";
                if (ofdAst.ShowDialog() != DialogResult.Cancel)
                {
                    SAP_File = ofdAst.FileName;
                    this.FilePath = ofdAst.FileName;
                    //RunExe(ofdAst.FileName);
                }
                #endregion Open SAP Data File
            }
            else if (tsmi.Name == tsmi_workingFolder.Name)
            {
                SelectWorkingFolder();
            }
            else if (tsmi.Name == tsmi_ana_inp_dwg.Name)
            {
                AstraAccess.ViewerFunctions.ASTRA_Input_Data(Drawing_File, true);
            }
            else if (tsmi.Name == tsmi_analysisInputDataFile.Name ||
                tsmi.Name == tsmi_ana_inp_text.Name)
            {
                AstraAccess.ViewerFunctions.ASTRA_Input_Data(FilePath);
            }
            else if (tsmi.Name == tsmi_ana_sap_file.Name)
            {
                //frmSAP_Editor frm = new frmSAP_Editor(file_name);
                //return frm.Show();
                AstraAccess.ViewerFunctions.Form_SAP_Editor(this).Show();
            }
            else if (tsmi.Name == tsmi_ana_text_file.Name)
            {
                //frmSAP_Editor frm = new frmSAP_Editor(file_name);
                //return frm.Show();
                AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(TEXT_File, false).Show();
            }
            else if (tsmi.Name == tsmi_ana_dwg_file.Name)
            {
                //frmSAP_Editor frm = new frmSAP_Editor(file_name);
                //return frm.Show();
                AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(Drawing_File, true).Show();
            }
            else if (tsmi.Name == tsmi_analysisInputDataFile.Name ||
                tsmi.Name == tsmi_ana_inp_text.Name)
            {
                AstraAccess.ViewerFunctions.ASTRA_Input_Data(FilePath);
            }
            else if (tsmi.Name == tsmi_analysisProcessResults.Name ||
                tsmi.Name == tsmi_ana_res_text.Name)
            {
                AstraAccess.ViewerFunctions.ASTRA_Analysis_Process(FilePath, false);
            }

            else if (tsmi.Name == tsmi_ana_inp_exm.Name)
            {

                if (FilePath == "")
                {
                    CAstraFunctionFactory.Instance.ShowAnalysisExamplesDialog(this, false);
                }
                if (FilePath != "")
                    AstraAccess.ViewerFunctions.ASTRA_Input_Data(FilePath);
            }
            else if (tsmi.Name == tsmi_ana_res_exm.Name)
            {

                if (FilePath == "")
                {
                    CAstraFunctionFactory.Instance.ShowAnalysisExamplesDialog(this, false);
                }
                if (FilePath != "")
                    AstraAccess.ViewerFunctions.ASTRA_Analysis_Process(FilePath, false);
            }

            else if (tsmi.Name == tsmi_ana_view_text.Name)
            {
                View_Input_File(FilePath);
            }
            SetRecentFiles();
            Set_Bridge_Design_Menu();
        }


        public void OpenWork(string opnfileName, string feature)
        {
            this.FilePath = opnfileName;
            SetApp_Structure(FilePath, "INPUT");
        }

        private void tsmi_selectDesignStandard_Click(object sender, EventArgs e)
        {
            SelectDesignStandard();
            Set_Bridge_Design_Menu();
        }


        public Form Form_ASTRA_TEXT_Data(string file_name, bool IsDrawingFile)
        {
            return AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(file_name, IsDrawingFile);
        }
        public Form Form_ASTRA_TEXT_Data(string file_name)
        {
            return AstraAccess.ViewerFunctions.Form_ASTRA_TEXT_Input_Data(this, file_name, false);
        }

        public Form Form_ASTRA_Input_Data(string file_name, bool IsDrawingFile)
        {
            return AstraAccess.ViewerFunctions.Form_ASTRA_Input_Data(file_name, IsDrawingFile);


        }
        public Form Form_ASTRA_Analysis_Process(string file_name, bool IsMoving_Load)
        {
            return AstraAccess.ViewerFunctions.Form_ASTRA_Analysis_Process(file_name, IsMoving_Load);
        }

        public Form Form_ASTRA_Moving_Load(string file_name)
        {
            return AstraAccess.ViewerFunctions.Form_ASTRA_Moving_Load(file_name);
        }

        public void View_SAP_Data(string file_name)
        {
            Form fvf = AstraAccess.ViewerFunctions.Form_SAP_Editor(this, file_name);
            fvf.Owner = this;
            fvf.Show();
        }

        private void tsmi_link_1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_link_1.Name)
            {
                System.Diagnostics.Process.Start("http://www.inti.gob.ar/cirsoc/pdf/puentes_hormigon/16-AAC-Load%20Handout-Color.pdf");
            }
            else if (tsmi.Name == tsmi_link_2.Name)
            {
                System.Diagnostics.Process.Start("http://www.standardsforhighways.co.uk/dmrb/vol1/section3/bd3701.pdf");
            }
        }

        #region Get Demo Version CoordinateCheck
        public bool Check_Coordinate(int JointNo, int MemberNo)
        {
            List<int> ListJoints = new List<int>();

            #region ListJoints
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(22);
            ListJoints.Add(16);
            ListJoints.Add(21);
            ListJoints.Add(15);
            ListJoints.Add(12);
            ListJoints.Add(79);
            ListJoints.Add(24);
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(121);
            ListJoints.Add(121);
            ListJoints.Add(102);
            ListJoints.Add(1261);
            ListJoints.Add(153);
            ListJoints.Add(24);
            ListJoints.Add(392);
            ListJoints.Add(544);
            ListJoints.Add(166);
            #endregion ListJoints

            List<int> ListMembers = new List<int>();

            #region ListMembers
            ListMembers.Add(21);
            ListMembers.Add(21);
            ListMembers.Add(47);
            ListMembers.Add(23);
            ListMembers.Add(28);
            ListMembers.Add(21);
            ListMembers.Add(11);

            ListMembers.Add(78);
            ListMembers.Add(24);
            ListMembers.Add(21);
            ListMembers.Add(21);
            ListMembers.Add(21);
            ListMembers.Add(220);
            ListMembers.Add(220);

            ListMembers.Add(248);
            ListMembers.Add(2374);
            ListMembers.Add(280);
            ListMembers.Add(24);
            ListMembers.Add(899);
            ListMembers.Add(362);
            ListMembers.Add(0);
            #endregion ListMembers

            for (int i = 0; i < ListJoints.Count; i++)
            {
                if (ListJoints[i] == JointNo && ListMembers[i] == MemberNo)
                    return true;
            }
            return false;
        }

        #endregion Get Demo Version CoordinateCheck

        private void tsmi_structure_design_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == tsmi_rcc_structure_design.Name)
            {
                //AstraAccess.ViewerFunctions.Form_ASTRA_Structure_Input_Data( LastDesignWorkingFolder, false).Show();
                ////AstraAccess.ViewerFunctions.Form_ASTRA_Structure_Input_Data(this, LastDesignWorkingFolder).Show();

                frm_StructuralDesign frm = new frm_StructuralDesign(this);
                frm.StageAnalysisForm = AstraAccess.ViewerFunctions.Form_Stage_Analysis(this);
                frm.Owner = this;
                frm.Show();

                //frm.Working_Folder = working_folder;
                //frm.LastDesignWorkingFolder = working_folder;

            }
            else if (tsmi.Name == tsmi_tunnel_design.Name)
            {
                //string upath = Path.Combine(LastDesignWorkingFolder, "Tunnel Lining Design");
                //if (!Directory.Exists(upath)) Directory.CreateDirectory(upath);

                string upath = LastDesignWorkingFolder;
                if (!Directory.Exists(upath)) Directory.CreateDirectory(upath);


                Form frm = AstraAccess.ViewerFunctions.Form_ASTRA_Tunnel_Input_Data(this, upath);
                frm.Owner = this;
                frm.Show();
            }
        }







        public string user_path { get; set; }

        private void tsmi_CostEstimation_Click(object sender, EventArgs e)
        {

            if (!Is_select_Design_Standard) SelectDesignStandard();
            LimitStateMethod.CostEstimation.frm_CostEstimation fcost = new LimitStateMethod.CostEstimation.frm_CostEstimation(this);
            fcost.Owner = this;
            fcost.Show();
        }

        private void tsmi_cable_suspension_bridge_Click(object sender, EventArgs e)
        {
            if (!Is_select_Design_Standard) SelectDesignStandard();
            ShowTimerScreen(eASTRAImage.Cable_Suspension);
            LimitStateMethod.SuspensionBridge.frm_Suspension_Bridge fsps = new LimitStateMethod.SuspensionBridge.frm_Suspension_Bridge(this);
            fsps.Owner = this;
            fsps.Show();
        }

        private void tsmi_steel_tables_Click(object sender, EventArgs e)
        {
            frm_SteelTables frm = new frm_SteelTables(this);
            frm.Owner = this;
            frm.Show();
        }

        public Form Form_Stage_Analysis(string filename)
        {
            //CAstraFunctionFactory.Instance.OpenStageAnalysisDialog(this, filename);
            return AstraAccess.ViewerFunctions.Form_Stage_Analysis(this, filename);
        }


        public int Timer_Interval
        {
            get
            {
                return tmr.Interval;
            }
            set
            {
                tmr.Interval = value;
            }
        }

        public Form Form_Drawing_Editor(eBaseDrawings DrawingType, string drawing_path, string report_file)
        {

            return AstraAccess.ViewerFunctions.Form_Drawing_Editor(this, DrawingType, drawing_path, report_file);
        }

        public Form Form_Drawing_Editor(eBaseDrawings DrawingType, string Title, string drawing_path, string report_file)
        {

            return AstraAccess.ViewerFunctions.Form_Drawing_Editor(this, DrawingType, Title, drawing_path, report_file);
        }


        public eOpenDrawingOption Open_Drawing_Option()
        {
            return frm_Drawing_Options.Drawing_Option();
        }

        //Chiranjit [2017 02 16] 
        //Stage Analysis File
        public int StageNo { get; set; }
        public string Stage_File { get; set; }

        //Chiranjit [2017 03 08] Add Structure Design
        private void tsmi_jetty_design_Click(object sender, EventArgs e)
        {
            SelectDesignStandard();
            ShowTimerScreen(eASTRAImage.Jetty);

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi == tsmi_jetty_design_RCC)
            {
                frm_RCC_Jetty fjt = new frm_RCC_Jetty(this);
                fjt.Owner = this;
                fjt.Show();
            }
            else if (tsmi == tsmi_jetty_design_PSC)
            {
                frm_PSC_Jetty fjt = new frm_PSC_Jetty(this);
                fjt.Owner = this;
                fjt.Show();
            }
        }

        private void raftFoundationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AstraAccess.Footing.frm_RaftFoundation frm = new AstraAccess.Footing.frm_RaftFoundation(this);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_transmissionTower_Click(object sender, EventArgs e)
        {
            //LimitStateMethod.TowerDesign.frm_TransmissionTower frmTower = new LimitStateMethod.TowerDesign.frm_TransmissionTower(this);
            //LimitStateMethod.TowerDesign.frmTransmissionTower frmTower = new LimitStateMethod.TowerDesign.frmTransmissionTower(this);

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            SelectDesignStandard();


            if (tsmi == tsmi_transmissionTower)
            {
                ShowTimerScreen(eASTRAImage.Transmission_Tower);
                ASTRAStructures.frmTower frm = new ASTRAStructures.frmTower(this, eASTRADesignType.Transmission_Tower);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi == tsmi_microwaveTower)
            {
                ShowTimerScreen(eASTRAImage.Microwave_Tower);
                ASTRAStructures.frmMicrowaveTower frm1 = new ASTRAStructures.frmMicrowaveTower(this, eASTRADesignType.Microwave_Tower);
                frm1.Owner = this;
                frm1.Show();
            }
            else if (tsmi == tsmi_cableCarTower)
            {
                ShowTimerScreen(eASTRAImage.Cable_Car_Tower);
                ASTRAStructures.frmCableCarTower frm2 = new ASTRAStructures.frmCableCarTower(this, eASTRADesignType.CableCar_Tower);
                frm2.Owner = this;
                frm2.Show();
            }
            else if (tsmi == tsmi_transmissionTower3Cables)
            {
                ShowTimerScreen(eASTRAImage.Transmission_Tower_3_Cables);
                ASTRAStructures.frmTower2Head frm3 = new ASTRAStructures.frmTower2Head(this, eASTRADesignType.Transmission_Tower);
                frm3.Owner = this;
                frm3.Show();
            }
        }

        private void tsmi_structureModeling_Click(object sender, EventArgs e)
        {
            //ShowTimerScreen(eASTRAImage.Cable_Car_Tower);
            //ASTRAStructures.frmStructureModeling frm2 = new ASTRAStructures.frmStructureModeling(this, eASTRADesignType.Structure_Modeling);
            ASTRAStructures.frmStructure3D frm2 = new ASTRAStructures.frmStructure3D(this, eASTRADesignType.Structure_Modeling);
            frm2.Owner = this;
            frm2.Show();
        }

        private void tsmi_RCC_Pier_LS_Click(object sender, EventArgs e)
        {
            SelectDesignStandard();
            LimitStateMethod.PierDesign.frm_RCC_Pier_Limit_State frm = new LimitStateMethod.PierDesign.frm_RCC_Pier_Limit_State(this);
            frm.Owner = this;
            frm.Show();
        }

        private void tsmi_HSTBD_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if(tsmi == tsmi_HSTBD)
            {

                if (!Directory.Exists(LastDesignWorkingFolder)) SelectWorkingFolder();

                if (Directory.Exists(LastDesignWorkingFolder))
                {
                    string HSTBD = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), "HSTBD\\HSTBM.EXE");

                    if (File.Exists(HSTBD))
                    {
                        System.Environment.SetEnvironmentVariable("HSLM", LastDesignWorkingFolder);
                        System.Environment.SetEnvironmentVariable("HSTBD_VERSION", "PRO");
                        //System.Environment.SetEnvironmentVariable("HSTBD_VERSION", "DEMO");
                        System.Diagnostics.Process.Start(HSTBD);
                    }
                }
            }
        }


        #region IApplication Members [2017 09 01]


        public string Open_Project_Dialog(string ProjectType, string WorkingFolder)
        {
            string strPath = "";
            BridgeAnalysisDesign.frm_Open_Project frm = new BridgeAnalysisDesign.frm_Open_Project(ProjectType, WorkingFolder);
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                strPath = frm.Example_Path;
                //iApp.Read_Form_Record(this, frm.Example_Path);

                //string file_name = txt_analysis_file.Text;
                //txt_project_name.Text = Path.GetFileName(frm.Example_Path);

                #region Save As
                if (frm.SaveAs_Path != "")
                {
                    //string src_path = user_path;
                    //txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                    //Create_Project();
                    ////string dest_path = user_path;

                    //MyList.Folder_Copy(src_path, dest_path);
                }
                #endregion Save As
                //////Open_Project();
                //txt_project_name.Text = Path.GetFileName(user_path);
                //Write_All_Data();
            }
            return strPath;
        }


        public string Create_Project(string Title, string Project_Name, eASTRADesignType Project_Type)
        {
            string upath = Path.Combine(LastDesignWorkingFolder, Title);
            if (!Directory.Exists(upath))
            {
                Directory.CreateDirectory(upath);
            }
            string fname = Path.Combine(upath, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            upath = Path.Combine(upath, Project_Name);

            if (Directory.Exists(upath))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                   "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return "";
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        MyList.Delete_Folder(upath);
                        break;
                }
            }
            if (!Directory.Exists(upath))
            {
                Directory.CreateDirectory(upath);
            }
            //Write_All_Data();

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);

            return upath;
        }

        public string Set_Project_Name(string Title)
        {
            string dir = Path.Combine(LastDesignWorkingFolder, Title);

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

            return prj_name;

        }


        public bool RunAnalysis(string fName)
        {
            if (!File.Exists(fName)) return false;
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            //Delete_FIL_Files(Path.GetDirectoryName(fName));
            Delete_Temporary_Files(fName);

            System.Environment.SetEnvironmentVariable("SURVEY", fName);
            System.Environment.SetEnvironmentVariable("ASTRA", fName);

            File.WriteAllText(patFile, Path.GetDirectoryName(fName) + "\\");

            string runExe = "", ext = "";
            ext = Path.GetExtension(fName).ToLower();
            if (ext == ".txt")
            {
                runExe = Path.Combine(Application.StartupPath, "AST001.exe");

                if (File.Exists(runExe) == false)
                {
                    runExe = Path.Combine(Application.StartupPath, "AST004.exe");
                }
            }
            else if (ext == ".ast")
                runExe = Path.Combine(Application.StartupPath, "AST003.exe");
            try
            {
                System.Diagnostics.Process.Start(runExe).WaitForExit();
            }
            catch (Exception exx)
            {
                return false;
            }
            return true;
        }

        #endregion

        private void tsmi_eigenValue_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi == tsmi_eigenValue)
            {
                //Form rm = AstraAccess.ViewerFunctions.Form_ResponseSpectrumAnalysis(this, eASTRADesignType.Eigen_Value_Analysis);
                Form rm = AstraAccess.ViewerFunctions.Form_DynamicAnalysis(this, eASTRADesignType.Eigen_Value_Analysis);
                rm.Owner = this;
                rm.Show();
            }
            else if (tsmi == tsmi_timeHistory)
            {
                //Form rm = AstraAccess.ViewerFunctions.Form_TimeHistoryAnalysis(this, eASTRADesignType.Time_History_Analysis);
                Form rm = AstraAccess.ViewerFunctions.Form_DynamicAnalysis(this, eASTRADesignType.Time_History_Analysis);
                rm.Owner = this;
                rm.Show();
            }
            else if (tsmi == tsmi_responseSpectrum)
            {
                Form rm = AstraAccess.ViewerFunctions.Form_DynamicAnalysis(this, eASTRADesignType.Response_Spectrum_Analysis);
                rm.Owner = this;
                rm.Show();
            }
        }

        //Chiranjit [2017 09 20] Added New Menu Item
        private void tsmi_NewMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (!Is_select_Design_Standard) SelectDesignStandard();

            if (tsmi == tsmi_RCC_TGirder_LSM)
            {
                Show_T_Girder_Bridge_Limit_State();
            }
            else if (tsmi == tsmi_RCC_TGirder_WSM)
            {
                Show_T_Girder_Bridge_Working_Stress();
            }
            else if (tsmi == tsmi_Composite_LSM)
            {
                Show_Composite_Bridge_Limit_State();
            }
            else if (tsmi == tsmi_Composite_WSM)
            {
                Show_Composite_Bridge_Working_Stress();

            }
            else if (tsmi == tsmi_PSC_IGirder_LSM)
            {
                Show_PSC_IGirder_Bridge_Limit_State();

            }
            else if (tsmi == tsmi_PSC_IGirder_WSM)
            {
                Show_PSC_IGirder_Bridge_Working_Stress();
            }
            else if (tsmi == tsmi_minor_Bridge_ls)
            {
                Show_Minor_Bridge_Limit_State();
            }
            else if (tsmi == tsmi_minor_Bridge_ws)
            {
                Show_Minor_Bridge_Working_Stress();
            }
        }

        private void Show_Composite_Bridge_Limit_State()
        {
            ShowTimerScreen(eASTRAImage.Composite_Bridge);

            if (IsRelease_22)
            {
                if (DesignStandard == eDesignStandard.LRFDStandard)
                {

                    LimitStateMethod.Composite.frm_Composite_AASHTO frm = new LimitStateMethod.Composite.frm_Composite_AASHTO(this);
                    frm.Owner = this;
                    frm.Show();
                }
                else
                {
                    LimitStateMethod.Composite.frm_CompositeLSM_New frm = new LimitStateMethod.Composite.frm_CompositeLSM_New(this);
                    frm.Owner = this;
                    frm.Show();
                }
            }
        }
        private void Show_Composite_Bridge_Working_Stress()
        {
            ShowTimerScreen(eASTRAImage.Composite_Bridge);
            BridgeAnalysisDesign.Composite.frm_Composite frm = new BridgeAnalysisDesign.Composite.frm_Composite(this);
            frm.Owner = this;
            frm.Show();
        }

        private void Show_PSC_IGirder_Bridge_Limit_State()
        {

            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);

            if (IsRelease_22)
            {
                if (DesignStandard == eDesignStandard.LRFDStandard)
                {
                    frm_PSC_I_Girder_AASHTO frm = new frm_PSC_I_Girder_AASHTO(this);
                    frm.Owner = this;
                    frm.Show();
                }
                else
                {
                    frm_PSC_I_Girder_LS_New frm = new frm_PSC_I_Girder_LS_New(this);
                    frm.Owner = this;
                    frm.Show();
                }
            }
            else
            {
                frm_PSC_I_Girder_LS frm = new frm_PSC_I_Girder_LS(this);
                frm.Owner = this;
                frm.Show();
            }
        }
        private void Show_PSC_IGirder_Bridge_Working_Stress()
        {

            //if (((ToolStripMenuItem)sender).Name == tsmi_PSC_I_Girder_LongSpan.Name)
            //{
            //    ShowTimerScreen(eASTRAImage.PSC_I_Girder_Long_Span);
            //    BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_LongSpan_WS frm = new BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_LongSpan_WS(this);
            //    frm.Owner = this;
            //    frm.Show();
            //}
            //else
            //{
            ShowTimerScreen(eASTRAImage.PSC_I_Girder_Short_Span);
            BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_ShortSpan_WS frm = new BridgeAnalysisDesign.PSC_I_Girder.frm_PSC_I_Girder_ShortSpan_WS(this);
            frm.Owner = this;
            frm.Show();

            //}
        }

        private void Show_Minor_Bridge_Limit_State()
        {
            //if (!Is_select_Design_Standard) SelectDesignStandard();
            //LimitStateMethod.Minor_Bridge.frm_MinorBridge_LS frm = new LimitStateMethod.Minor_Bridge.frm_MinorBridge_LS(this);
            LimitStateMethod.SlabBridge.frmSlabBridge frm = new LimitStateMethod.SlabBridge.frmSlabBridge(this);
            frm.Owner = this;
            frm.Show();
        }
        private void Show_Minor_Bridge_Working_Stress()
        {
            //if (!Is_select_Design_Standard) SelectDesignStandard(); 
            
            BridgeAnalysisDesign.MinorBridge.frm_MinorBridge frm = new BridgeAnalysisDesign.MinorBridge.frm_MinorBridge(this);
            frm.Owner = this;
            frm.Show();
        }



        #region IApplication Members


        public void Open_Excel_Macro_Notes()
        {
            string exl_help = Path.Combine(Application.StartupPath, @"ASTRAHelp\Excel Macro Enable Notes.pdf");
       
            if(File.Exists(exl_help))
                System.Diagnostics.Process.Start(exl_help);
        }

        #endregion

        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load, List<string> list)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        public List<string> IRC_6_2014_Load_Combinations(double CW)
        {
            List<string> list = new List<string>();



            double CL = 1.5;
            double min_class_A = 0.15;
            double min_70RW = 1.2;
            double clear_dist = 1.2;
            double wheel_wd = 0.5;

            double wd_class_A = 1.8;
            double Wd_70RW = 2.9;


            //if (CW >= 4.25 && CW < 5.3) // 1 Lane
            if (CW < 5.3) // 1 Lane
            {
                #region 1 Lanes
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1"));
                list.Add(string.Format("X,-18.8"));
                list.Add(string.Format("Z,1.65"));
                //list.Add(string.Format("Z,{0:f3}", CL + min_class_A));
                list.Add(string.Format(""));
                #endregion
            }
            else if (CW >= 5.3 && CW < 9.6) // 2 Lanes
            {
                #region 2 Lanes
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 3"));
                list.Add(string.Format("X,-13.4"));
                list.Add(string.Format("Z,2.7"));
                //list.Add(string.Format("Z,{0:f3}", CL + min_70RW));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8"));
                list.Add(string.Format("Z,1.65,5.15"));
                //list.Add(string.Format("Z,{0:f3}", CL + min_class_A, CL + min_class_A + wd_class_A+ clear_dist))l
                list.Add(string.Format(""));
                #endregion
            }
            else if (CW >= 9.6 && CW < 13.1) // 3 Lanes
            {
                #region 3 Lanes
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.65,5.15,8.65"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-13.4"));
                list.Add(string.Format("Z,1.65,5.15"));
                list.Add(string.Format(""));
                #endregion
            }
            else if (CW >= 13.1 && CW < 16.6) // 4 Lanes
            {
                #region 4 Lanes
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1,TYPE 1,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.65,5.15,8.65,12.15"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-18.8,-13.4"));
                list.Add(string.Format("Z,1.65,5.15,8.65"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 3,TYPE 3"));
                list.Add(string.Format("X,-13.4,-13.4"));
                list.Add(string.Format("Z,2.7,6.8"));
                list.Add(string.Format(""));
                #endregion
            }
            else if (CW >= 16.6 && CW < 20.1) // 5 Lanes
            {
                #region 5 Lanes
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1,TYPE 1,TYPE 1,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.65,5.15,8.65,12.15,15.65"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8,-13.4"));
                list.Add(string.Format("Z,1.65,5.15,8.65,12.15"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 3,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-13.4,-13.4"));
                list.Add(string.Format("Z,2.7,6.8,10.3"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 1,TYPE 3,TYPE 3"));
                list.Add(string.Format("X,-18.8,-13.4,-13.4"));
                list.Add(string.Format("Z,1.65,5.15,9.25"));
                #endregion
            }
            //else if (CW >= 20.1 && CW < 23.6) // 6 Lanes
            else if (CW >= 20.1) // 6 Lanes
            {
                #region 6 Lanes
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1,TYPE 1,TYPE 1,TYPE 1,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.65,5.15,8.65,12.15,15.65,19.15"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1,TYPE 1,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8,-18.8,-13.4"));
                list.Add(string.Format("Z,1.65,5.15,8.65,12.15,15.65"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 1,TYPE 1,TYPE 3,TYPE 3"));
                list.Add(string.Format("X,-18.8,-18.8,-13.4,-13.4"));
                list.Add(string.Format("Z,1.65,5.15,8.65,12.75"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 3,TYPE 1,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-13.4,-18.8,-18.8,-13.4"));
                list.Add(string.Format("Z,2.7,6.8,10.30,13.80"));
                #endregion
            }
            return list;
        }

        public void Load_ASTRA_R22_Menu()
        {

            tsmi_newAnalysisTXTDataFile.Visible = false;
            tsmi_newAnalysisSAPDataFile.Visible = false;
            tsmi_newAnalysisDWGDataFile.Visible = false;
            tsmi_openAnalysisTXTDataFile.Visible = false;
            tsmi_openSAPDataFile.Visible = false;
            tsmi_openStructureModelDrawingFile.Visible = false;
            tsmi_openAnalysisExampleTXTDataFile.Visible = false;
            tsmi_structure_text.Enabled = false;
            tsmi_Process_Design.Visible = false;


            tsmi_Process_Design.DropDownItems.Remove(tsmi_selectWorkingFolder);
            tsmi_Process_Design.DropDownItems.Remove(tsmi_selectDesignStandard);
            tsmi_Process_Design.DropDownItems.Remove(tsmi_Bridge_Design);
            tsmi_Process_Design.DropDownItems.Remove(tsmi_RCC_Structural_Design);
            tsmi_Process_Design.DropDownItems.Remove(tsmi_research_Studies);
            tsmi_process_analysis.DropDownItems.Remove(tsmi_structureModeling);

            tsmi_process_analysis.Visible = false;

            tsmi_file.DropDownItems.Insert(0, tsmi_selectWorkingFolder);
            tsmi_file.DropDownItems.Insert(1, tsmi_selectDesignStandard);


            tsmi_file.DropDownItems.Insert(10, tsmi_research_Studies);
            tsmi_file.DropDownItems.Insert(10, tsmi_RCC_Structural_Design);
            tsmi_file.DropDownItems.Insert(10, tsmi_Bridge_Design);



            tsmi_file.DropDownItems.Insert(tsmi_file.DropDownItems.IndexOf(tsmi_structure_sap) + 1, tsmi_structureModeling);

            //tsmi_file.DropDownItems.Add(tsmi_Bridge_Design);
            //tsmi_file.DropDownItems.Add(tsmi_RCC_Structural_Design);
            //tsmi_file.DropDownItems.Add(tsmi_research_Studies);


            #region Set Structure Analysis Menu
            string example_path = Path.Combine(Application.StartupPath, @"ASTRA Pro Analysis Examples\01 Analysis with Text Data File");

            List<string> lst_dir = new List<string>(Directory.GetDirectories(example_path));

            List<string> list = new List<string>();

            list.Add("Static Analysis of Frame with Beam Members");
            list.Add("Static Analysis with Springs at Supports");
            list.Add("Static Analysis of Truss Tower");
            list.Add("Static Analysis with Beam & Truss Members");
            list.Add("Static Analysis with Area Load & Node Gap");
            list.Add("Static Analysis with Repeat Load");
            list.Add("Static Analysis with Plate Elements");
            list.Add("Static Analysis with Sinking_Supports");
            list.Add("Static Analysis with Temperature Load");
            list.Add("Dynamic Analysis for Eigen Value Vector");
            list.Add("Dynamic Analysis for Response Spectrum");
            list.Add("Dynamic Analysis for Time History");
            list.Add("Moving Load Analysis of Bridge Deck");
            list.Add("Moving & Fixed Load Analysis of Bridge Deck");
            list.Add("Analysis for Truss Groups in Bridge");
            list.Add("Analysis for Cable Groups in Bridge");
            list.Add("DL+SIDL+Moving Load Analysis for Pier");
            list.Add("Transverse Load Analysis for Deck Slab");
            list.Add("Analysis for Multistoreyed Building");
            list.Add("Large Frame Analysis with Beam & Plate");
            list.Add("Dynamic Analysis with Solid Elements");
            
            //[0] = "D:\\Software Development\\ASTRA Pro Main Screen Professional\\ASTRA\\bin\\x86\\Debug\\ASTRA Pro Analysis Examples\\01 Analysis with Text Data File"
            tsmi_structure_text.DropDownItems.Clear();

            ToolStripItem tsi = null;
            int anaCount = 1;
            //foreach (var item in list)
            //{
            //    tsi = tsmi_structure_text.DropDownItems.Add(item);
            //    tsi.Name = "analysis_" + anaCount++;
            //    tsi.Click += new EventHandler(tsi_Click);
            //}

            tsmi_structure_text.Click += new EventHandler(tsi_Click);

            #endregion Set Structure Analysis Menu



            #region Set Structure Analysis Menu
             example_path = Path.Combine(Application.StartupPath, @"ASTRA Pro Analysis Examples\03 Analysis with SAP Data File");

            lst_dir = new List<string>(Directory.GetDirectories(example_path));

            list = new List<string>();

            list.Add(string.Format("Static Analysis of Frame with Beam Members"));
            list.Add(string.Format("Static Analysis with Springs at Supports"));
            list.Add(string.Format("Static Analysis of Truss Tower"));
            list.Add(string.Format("Static Analysis with Beam & Truss Members"));
            list.Add(string.Format("Static Analysis with Area Load & Node Gap"));
            list.Add(string.Format("Static Analysis with Repeat Load"));
            list.Add(string.Format("Static Analysis with Plate Elements"));
            list.Add(string.Format("Static Analysis with Sinking_Supports"));
            list.Add(string.Format("Static Analysis with Temperature Load"));
            list.Add(string.Format("Dynamic Analysis for Eigen Value_Vector"));
            list.Add(string.Format("Dynamic Analysis for Response Spectrum"));
            list.Add(string.Format("Dynamic Analysis for Time History"));
            list.Add(string.Format("Static Analysis with Solid Elements"));
            list.Add(string.Format("Dynamic Analysis with Solid Elements"));

            //[0] = "D:\\Software Development\\ASTRA Pro Main Screen Professional\\ASTRA\\bin\\x86\\Debug\\ASTRA Pro Analysis Examples\\01 Analysis with Text Data File"
            tsmi_structure_sap.DropDownItems.Clear();

             //tsi = null;

            anaCount = 1;
            foreach (var item in list)
            {
                tsi = tsmi_structure_sap.DropDownItems.Add(item);
                tsi.Name = "sap_" + anaCount++;
                tsi.Click += new EventHandler(tsi_Click);
            }

            #endregion Set Structure Analysis Menu

        }

        void tsi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            //MessageBox.Show(tsmi.Name + " : " + tsmi.Text);

            if (tsmi.Name.StartsWith("sap"))
            {
                AstraAccess.frmSAPWorkspace frm = new AstraAccess.frmSAPWorkspace(this, tsmi.Name + " : " + tsmi.Text, true);
                frm.Owner = this;
                frm.Show();
            }
            else
            {
                //frmAnalysisWorkspace frm = new frmAnalysisWorkspace(this, tsmi.Name + " : " + tsmi.Text);
                frmAnalysisWorkspaceNew frm = new frmAnalysisWorkspaceNew(this, tsmi.Name + " : " + tsmi.Text);
                frm.Owner = this;
                frm.Show();
            }

            //throw new NotImplementedException();
        }

        private void tsmi_steel_beam_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi == tsmi_steel_beam)
            {
                frmSteelBeamDesign frm = new frmSteelBeamDesign(this);
                frm.Owner = this;
                frm.Show();
            }
            else if (tsmi == tsmi_steel_column)
            {
                frmSteelColumnDesign frm = new frmSteelColumnDesign(this);
                frm.Owner = this;
                frm.Show();
            }
        }

        private void tsmi_sheet_pile_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Select the Program \"prosheet.exe\" from folder \"Sheet Pile Applications\"",
            //    "ASTRA", MessageBoxButtons.OK);

            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "Executable File (*.exe)|*.exe";
            //    if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            //    {
            //        System.Diagnostics.Process.Start(ofd.FileName);
            //    }
            //}

            BridgeAnalysisDesign.Foundation.frmSheetPileDesign ff = new BridgeAnalysisDesign.Foundation.frmSheetPileDesign();

            ff.Owner = this;
            ff.Show();
        }



        #region IApplication Members


        public void View_Plan_Moving_Load(string inputFile, double skewAngle)
        {
            ASTRAStructures.frmPlanMovingLoad ff = new frmPlanMovingLoad(this, skewAngle);
            ff.ShowDialog();
        }

        #endregion

        private void tsmi_return_wall_cant_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi == tsmi_return_wall_cant)
            {
                frm_RetainingWall ff = new frm_RetainingWall(this, false);
                ff.Owner = this;
                ff.Show();
            }
            if (tsmi == tsmi_return_wall_propped)
            {
                frm_RetainingWall ff = new frm_RetainingWall(this, true);
                ff.Owner = this;
                ff.Show();
            }
        }

        #region IApplication Members



        public void View_ASTRAGUI(eProcessType processType , string file_name)
        {
            //throw new NotImplementedException();
            string exe_file = Path.Combine(Application.StartupPath, "ASTRAGUI.EXE");

            int code = (int) processType;

            //if (processType == "PREPROCESS")
            //{
            //    code = 1;
            //}
            //else if (processType == "POSTPROCESS")
            //{
            //    code = 2;
            //}
            //else if (processType == "SAPPREPROCESS")
            //{
            //    code = 3;
            //}
            //else if (processType == "SAPPOSTPROCESS")
            //{
            //    code = 4;
            //}

            if (File.Exists(exe_file))
            {
                //Environment.SetEnvironmentVariable("PREPROCESS", file_name);

                Environment.SetEnvironmentVariable("ASTRAGUI", code + "$" + file_name);
                Process.Start(exe_file);
                //Environment.SetEnvironmentVariable("ASTRAGUI", "");

                //Process prs = new Process();
                //prs.StartInfo.FileName = exe_file;
                //prs.StartInfo.Arguments = file_name;

                //prs.Start();
            }
        }

        public void View_MovingLoad(string file_name)
        {
            //View_ASTRAGUI(eProcessType.MovingLoad, file_name);

            View_MovingLoad(file_name, 0.0, 0.0);
        }
        public void View_MovingLoad(string file_name, double curve_radius, double separating_distance)
        {

            string ext_file = Path.Combine(Path.GetDirectoryName(file_name), "radius.fil");
            if (curve_radius > 0)
            {
                File.WriteAllText(ext_file, curve_radius.ToString());
            }
            else
            {
                File.Delete(ext_file);
            } 
            ext_file = Path.Combine(Path.GetDirectoryName(file_name), "moving.fil");
            if (separating_distance > 0)
            {
                File.WriteAllText(ext_file, separating_distance.ToString());
            }
            else
            {
                File.Delete(ext_file);
            }

            View_ASTRAGUI(eProcessType.MovingLoad, file_name);
        }

        public void View_PreProcess(string file_name)
        {

            View_ASTRAGUI(eProcessType.TextPreProcess, file_name);

            return;

            //throw new NotImplementedException();
            string exe_file = Path.Combine(Application.StartupPath, "PreProcess.exe");

            if(File.Exists(exe_file))
            {
                Environment.SetEnvironmentVariable("PREPROCESS", file_name);
                Environment.SetEnvironmentVariable("PREPROCESS", file_name);
                Process.Start(exe_file);
                Environment.SetEnvironmentVariable("PREPROCESS", "");

                //Process prs = new Process();
                //prs.StartInfo.FileName = exe_file;
                //prs.StartInfo.Arguments = file_name;

                //prs.Start();
            }
        }
        public void View_SapPreProcess(string file_name)
        {
            View_ASTRAGUI(eProcessType.SapPreProcess, file_name);
        }


        public void View_PostProcess(string file_name)
        {

            View_ASTRAGUI(eProcessType.TextPostProcess, file_name);

            return;

            string exe_file = Path.Combine(Application.StartupPath, "PostProcess.exe");

            if (File.Exists(exe_file))
            {
                Environment.SetEnvironmentVariable("PREPROCESS", file_name);
                Process.Start(exe_file);
                Environment.SetEnvironmentVariable("PREPROCESS", "");
            }
        }

        public void View_SapPostProcess(string file_name)
        {
            View_ASTRAGUI(eProcessType.SapPostProcess, file_name);
            return;

            string exe_file = Path.Combine(Application.StartupPath, "SapPostProcess.exe");

            if (File.Exists(exe_file))
            {
                Environment.SetEnvironmentVariable("SAPPREPROCESS", file_name);
                Process.Start(exe_file);
                Environment.SetEnvironmentVariable("SAPPREPROCESS", "");
            }
        }
        #endregion

        #region IApplication Members


        public Form RunStageAnalysis(string fName)
        {
            AstraFunctionOne.frmStageAnalysis frmStage = new frmStageAnalysis(this, fName);
            frmStage.Owner = this;
            return frmStage;
        }

        #endregion

        public void Change_Stage_Coordinates(string prev_File, string new_file)
        {
            CBridgeStructure prv = new CBridgeStructure(prev_File);
            NodeResults nr = new NodeResults();

            string max_defl = Path.Combine(Path.GetDirectoryName(prev_File), "MAX_DEFLECTION.TXT");
            List<string> list = new List<string>();

            if (!File.Exists(max_defl)) return;

            if (File.Exists(max_defl))
            {
                list.AddRange(File.ReadAllLines(max_defl));
                foreach (var item in list)
                {
                    var ml = new MyList(MyList.RemoveAllSpaces(item), ' ');
                    var nrd = new NodeResultData();
                    if (ml.Count == 4)
                    {
                        try
                        {
                            nrd.NodeNo = ml.GetInt(0);
                            nrd.X_Translation = ml.GetDouble(1);
                            nrd.Y_Translation = ml.GetDouble(2);
                            nrd.Z_Translation = ml.GetDouble(3);
                            nr.Add(nrd);
                        }
                        catch (Exception exx) { }
                    }
                }
            }


            List<string> lst_delf = new List<string>();


            for (int i = 0; i < prv.Joints.Count; i++)
            {
                var jnt = prv.Joints[i];
                jnt.X += nr[i].X_Translation;
                jnt.Y += nr[i].Y_Translation;
                jnt.Z += nr[i].Z_Translation;

                //lst_delf.Add(jnt.ToString());

                lst_delf.Add(string.Format("{0,-5} {1,10:f5} {2,10:f5} {3,10:f5}", jnt.NodeNo, jnt.X, jnt.Y, jnt.Z));

            }
            list.Clear();
            list.AddRange(File.ReadAllLines(new_file));
            bool flag = false;
            int cnt = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ToUpper().StartsWith("JOINT C"))
                {
                    flag = true; continue;
                }
                if (list[i].ToUpper().StartsWith("MEMBER"))
                {
                    flag = false;
                    break;
                }
                if (flag)
                {
                    list[i] = lst_delf[cnt++];
                }
            }
            File.WriteAllLines(new_file, list.ToArray());
        }

    }
}

//Chiranjit [2011 10 17] New PSC Box girder Analysis

//Chiranjit [2013 04 02] Add RE Wall Design Program
//Chiranjit [2013 07 23] Add Continuous PSC Box Girder Bridge
