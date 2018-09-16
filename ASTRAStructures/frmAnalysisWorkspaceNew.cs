using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using AstraInterface.DataStructure;
using AstraFunctionOne.BridgeDesign.Piers;

using AstraFunctionOne.BridgeDesign.SteelTruss;


using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;

using BridgeAnalysisDesign;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;

using VDRAW = VectorDraw.Professional.ActionUtilities;
using ACLSS = HEADSNeed.ASTRA.ASTRAClasses;
using MCLSS = MovingLoadAnalysis;
using ASTR = AstraInterface.DataStructure;


namespace ASTRAStructures
{
    public partial class frmAnalysisWorkspaceNew : Form
    {

        #region Processing

        IApplication iApp;

        public string FilePath { get; set; }

        public string INPUT_FILE { get; set; }


        #region Pre Process

        bool IsDrawingFileOpen { get; set; }

        public List<LiveLoad> LL_Definition { get; set; }
       
        string Drawing_File { get; set; }
        //string File_Name { get; set; }
        string DataFileName
        {
            get
            {
                return File_Name;
            }
            set
            {
                File_Name = value;
            }
        }

        public frmAnalysisWorkspaceNew(string input_file)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
                InitializeComponent();
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();

                Drawing_File = "";
                File_Name = input_file;

                IsDrawingFileOpen = false;
            }
            catch (Exception ex) { }

        }

        public frmAnalysisWorkspaceNew(string drawing_file, bool IsDrawingFile)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
                InitializeComponent();
                Base_Control_MouseEvent();

                IsDrawingFileOpen = IsDrawingFile;

                if (IsDrawingFile)
                {
                    Drawing_File = drawing_file;
                    File_Name = "";
                }
                else
                {
                    Drawing_File = "";
                    File_Name = drawing_file;
                }


            }
            catch (Exception ex) { }

        }

        eASTRADesignType Project_Type { get; set; }

        public string analysis_type { get; set; }

        public string analysis_title { get; set; }



        public frmAnalysisWorkspaceNew(IApplication app, string menu_name)
        {
            //this.Show
            try
            {
                Project_Type = eASTRADesignType.Structural_Analysis;

                InitializeComponent();
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();
                Drawing_File = "";
                File_Name = "";

                iApp = app;

                LL_Definition = new List<LiveLoad>();

                IsDrawingFileOpen = false;


                MyList ml = new MyList(menu_name, ':');

                lbl_Title.Text = ml.StringList[1].ToUpper();

                analysis_title = ml.StringList[1];

                analysis_type = ml.StringList[0].ToUpper();
            }
            catch (Exception ex) { }

        }


        private void Tab_Selection()
        {
            
        }
        public void Show_Panel(TabPage tp)
        {
        }

        public void ClearSelect(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                dgv.Rows[i].Selected = false;
            }
        }
        private void frmAnalysisWorkspaceNew_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = Title;
                Set_Project_Name();



                Load_Initials();


               var ml = new MyList(analysis_type, '_');


                int anaType = ml.GetInt(1);


                rbtn_TEXT.Enabled = true;
                rbtn_3D_Drawing.Enabled = true;
                rbtn_SAP.Enabled = true;

                if (anaType > 12 && anaType < 20)
                {
                    rbtn_SAP.Enabled = false;

                }
                else if (anaType == 21)
                {
                    rbtn_TEXT.Enabled = false;
                    rbtn_3D_Drawing.Enabled = false;

                    rbtn_SAP.Checked = true;
                }
            }
            catch (Exception exx) { }
        }

        private void Load_Initials()
        {
            if (IsFlag) return;
            IsFlag = true;
            timer1.Start();
        }


        private void Open_Drawing_File()
        {
            if (File.Exists(Drawing_File))
            {
            }
        }

        private void Clear_All()
        {
        }
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
        }
        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_Selection();
        }



        private void frm_DrawingToData_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
        }
        public bool Save_Data(bool msg)
        {

            if (txt_input_file.Text == "") return true;

            System.Windows.Forms.DialogResult erd = System.Windows.Forms.DialogResult.Yes;

            if (msg)
            {
                erd = (MessageBox.Show("Do you want to Save Input Data file ?", "HEADS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

            }
            if (erd == System.Windows.Forms.DialogResult.Cancel) return false;


            if (msg)
            {
                if (txt_input_file.Text != "")
                {
                }
            }
            //MyList.
            Write_All_Data();
            return true;
        }


        private static string Get_Straight_File(string fname)
        {

            string st_file = Path.Combine(Path.GetDirectoryName(fname), "TempAnalysis");
            if (!Directory.Exists(st_file)) Directory.CreateDirectory(st_file);
            st_file = Path.Combine(st_file, "TempAnalysis.txt");
            return st_file;
        }

       
        List<string> Seismic_Load = new List<string>();
        List<string> Seismic_Combinations = new List<string>();
        bool IsSavedData = false;
        private bool Save_Data(string fname)
        {
            return true;
        }

        public bool Run_Data2(string flPath)
        {
            //if (Check_Demo(flPath)) return false;

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



        private void tsmi_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Pre Process

        #region Process
        public string Analysis_File_Name
        {
            get
            {

                if(rbtn_SAP.Checked)
                {
                    var sap_file = MyList.Get_SAP_Analysis_Report_File(File_Name);

                    if (!File.Exists(File_Name)) return "";
                    return sap_file;
                }
                if (!File.Exists(File_Name)) return "";
                return Path.Combine(Path.GetDirectoryName(File_Name), "ANALYSIS_REP.TXT");
            }
        }
        public string LL_TXT
        {
            get
            {
                if (!File.Exists(File_Name)) return "";
                return Path.Combine(Path.GetDirectoryName(File_Name), "LL.TXT");
            }
        }

        public bool Check_Coordinate(int JointNo, int MemberNo)
        {
            List<int> ListJoints = new List<int>();

            #region ListJoints
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(22);
            ListJoints.Add(16);
            ListJoints.Add(20);
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
            ListJoints.Add(441);
            ListJoints.Add(136);
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
            ListMembers.Add(774);
            ListMembers.Add(400);
            #endregion ListMembers

            for (int i = 0; i < ListJoints.Count; i++)
            {
                if (ListJoints[i] == JointNo && ListMembers[i] == MemberNo)
                    return true;
            }
            return false;
        }

        public bool Check_Demo_Version()
        {
           
            return false;
        }

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;


            double R = 0.0;

            if (R != 0.0)
            {
                string st_file = Get_Straight_File(File_Name);

                #region Run Curve Analysis

                if (File.Exists(st_file))
                    RunAnalysis(st_file);

                iApp.Write_Data_to_File(File_Name, st_file);

                if (File.Exists(File_Name))
                    Run_Data2(File_Name);


                #endregion Run Curve Analysis
            }
            else
            {
                if (rbtn_SAP.Checked)
                {
                    ProcessAnalysis();
                }
                else
                {
                    RunAnalysis();
                }
            }

            if (rbtn_SAP.Checked)
            {
                if (File.Exists(Analysis_File_Name))
                {
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                    StructureAnalysis = null;
                    ld = null;
                    Select_Steps();
                }
            }
            else
            {
                AST_DOC_ORG = new ASTRADoc(DataFileName);

                Open_Analysis_Report();

            }
        }
        public void RunAnalysisSAP()
        {
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            string flPath = Path.Combine(Path.GetDirectoryName(DataFileName), "inp.tmp");



            string AppFolder = Application.StartupPath;
            //Delete_FIL_Files(Path.GetDirectoryName(fName));
            //Delete_Temporary_Files(fName);

            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            //System.Environment.SetEnvironmentVariable("ASTRA", flPath);



            File.WriteAllText(patFile, Path.GetDirectoryName(flPath) + "\\");



            if (File.Exists(DataFileName))
            {
                File.WriteAllLines(flPath, File.ReadAllLines(DataFileName));
                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", "");


                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            }


            File.WriteAllText(Path.Combine(AppFolder, "hds.001"), flPath);
            File.WriteAllText(Path.Combine(AppFolder, "hds.002"), Path.GetDirectoryName(flPath) + "\\");


            string runExe = "";

            runExe = Path.Combine(Application.StartupPath, "AST005.exe");

            try
            {
                System.Diagnostics.Process.Start(runExe).WaitForExit();
            }
            catch (Exception exx)
            {
            }
        }

        public void ProcessAnalysis()
        {

            RunAnalysisSAP();

            string res_file = Path.Combine(Path.GetDirectoryName(DataFileName), "RES001.tmp");
            if (File.Exists(res_file))
            {
                List<string> file_cont = new List<string>(File.ReadAllLines(res_file));

                List<string> list = new List<string>();

                file_cont.RemoveRange(0, 42);
                file_cont.RemoveRange(file_cont.Count - 9, 8);

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("                                *****************************************************"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *                                                   *"));
                //list.Add(string.Format("                                *           ASTRA Pro Release 15 Version 00         *"));
                list.Add(string.Format("                                *                      ASTRA Pro                    *"));
                list.Add(string.Format("                                *        APPLICATIONS ON STRUCTURAL ANALYSIS        *"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *   ENGINEERING ANALYSIS FOR BRIDGES AND STRUCTURES *"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *                   INTRODUCED BY                   *"));
                list.Add(string.Format("                                *          TECHSOFT ENGINEERING SERVICES            *"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *                                                   *"));
                list.Add(string.Format("                                *****************************************************"));
                //list.Add(string.Format("                                THIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss")));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                file_cont.InsertRange(0, list.ToArray());
                file_cont.Add(string.Format(""));
                file_cont.Add(string.Format(""));
                file_cont.Add(string.Format(""));
                file_cont.Add(string.Format("******************************************************************************************************************************"));
                file_cont.Add(string.Format("                                                 END OF ANALYSIS REPORT              "));
                file_cont.Add(string.Format("******************************************************************************************************************************"));


                //rtb_results.Lines = file_cont.ToArray();

                res_file = Path.Combine(Path.GetDirectoryName(res_file), "SAP_ANALYSIS_REP.TXT");
                //File.WriteAllLines(res_file, rtb_results.Lines);
                File.WriteAllLines(res_file, file_cont.ToArray());
                iApp.Delete_Temporary_Files(DataFileName);
                MessageBox.Show(this, "Analysis Results are written in file 'SAP_ANALYSIS_REP.TXT'.\n\nFile Path:\n\n" + res_file, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show(this, "Analysis Results are written in file 'SAP_ANALYSIS_REP.TXT'.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //if (MessageBox.Show(this, "Analysis Results are written in file 'SAP_ANALYSIS_REP.TXT'.\n\n Do you want to open the File ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                //iApp.View_Result(res_file);

                Select_Steps();
                //StructureAnalysis = new AstraInterface.DataStructure.BridgeMemberAnalysis(iApp, ReportFileName);
                StructureAnalysis = null;
            }

        }

        public void Write_Seismic_Data(List<string> LoadData, List<string> Load_Comb, string file_name)
        {
            Seismic_Load = LoadData;
            Seismic_Combinations = Load_Comb;
            //File_Name = file_name;
            Save_Data(file_name);
        }
        public string File_Seismic_Load
        {
            get
            {
                if (File_Name == "") return "";
                string sf_name = Path.Combine(Path.GetDirectoryName(File_Name), "Seismic_Analysis");

                if (!Directory.Exists(sf_name))
                    Directory.CreateDirectory(sf_name);

                sf_name = Path.Combine(sf_name, "Seismic_Analysis.TXT");

                return sf_name;

            }
        }
        public bool RunAnalysis(string fName)
        {
            if (!File.Exists(fName)) return false;
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            //string fName = File_Name;
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

                //string ana_file = MyStrings.Get_Analysis_Report_File(fName);
                //if (File.Exists(ana_file))
                //{
                //    rtb_ana_rep.Lines = File.ReadAllLines(ana_file);
                //    StructureAnalysis = null;
                //    ld = null;
                //    Select_Steps();
                //}
            }
            catch (Exception exx)
            {
            }

            return true;
        }

        public void Run_Seismic_Analysis()
        {
            if (MessageBox.Show(this, "Do you want to Proceed for Seismic Load Analysis ?",
                 "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;
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

        public void RunAnalysis()
        {


            if (!File.Exists(File_Name)) return;
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            string fName = File_Name;

            iApp.Delete_Temporary_Files(fName);
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
                iApp.Delete_Temporary_Files(fName);

                if (File.Exists(Analysis_File_Name))
                {
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                    StructureAnalysis = null;
                    ld = null;
                    Select_Steps();
                }
            }
            catch (Exception exx)
            {
            }
        }

        public void Select_Steps()
        {
            List<int> Step_Lines = new List<int>();
            Hashtable hash_index = new Hashtable();
            //Items.Clear();
            //Items.Add("Select...Step.....");

            List<string> Items = new List<string>();

            List<string> analysis_list = new List<string>();
            #region analysis result list
            analysis_list.Add(string.Format("User's data"));
            analysis_list.Add(string.Format("JOINT COORDINATE"));
            analysis_list.Add(string.Format("MEMBER INCIDENCES"));
            analysis_list.Add(string.Format("JOINT COORD"));
            analysis_list.Add(string.Format("MEMBER INCI"));
            analysis_list.Add(string.Format("MEMB INCI"));
            analysis_list.Add(string.Format("START GROUP DEFINITION"));
            analysis_list.Add(string.Format("MEMBER PROPERTY"));
            analysis_list.Add(string.Format("CONSTANT"));
            analysis_list.Add(string.Format("SUPPORT"));
            //analysis_list.Add(string.Format("LOAD"));
            analysis_list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
            analysis_list.Add(string.Format("LOAD GENERATION"));
            analysis_list.Add(string.Format("C O N T R O L   I N F O R M A T I O N"));
            analysis_list.Add(string.Format("NODAL POINT INPUT DATA"));
            analysis_list.Add(string.Format("GENERATED NODAL DATA"));
            analysis_list.Add(string.Format("EQUATION NUMBERS"));
            analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            analysis_list.Add(string.Format("TRUSS ELEMENT DATA"));
            analysis_list.Add(string.Format("3 / D   B E A M   E L E M E N T S"));
            analysis_list.Add(string.Format("MATERIAL PROPERTIES"));
            analysis_list.Add(string.Format("BEAM GEOMETRIC PROPERTIES"));
            analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            analysis_list.Add(string.Format("3/D BEAM ELEMENT DATA"));
            analysis_list.Add(string.Format("E Q U A T I O N   P A R A M E T E R S"));
            analysis_list.Add(string.Format("N O D A L   L O A D S   (S T A T I C)   O R   M A S S E S   (D Y N A M I C)"));
            analysis_list.Add(string.Format("N O D E   D I S P L A C E M E N T S / R O T A T I O N S"));
            analysis_list.Add(string.Format("TRUSS MEMBER ACTIONS"));
            analysis_list.Add(string.Format(".....BEAM FORCES AND MOMENTS"));
            analysis_list.Add(string.Format("  SHELL ELEMENT STRESSES"));
            analysis_list.Add(string.Format("....8 NODE SOLID ELEMENT DATA"));
            analysis_list.Add(string.Format(".....8-NODE SOLID ELEMENT STRESSES"));
         
            analysis_list.Add(string.Format(" THIN  PLATE/SHELL  ELEMENT DATA"));
            analysis_list.Add(string.Format(" T H I N   P L A T E / S H E L L   E L E M E N T S"));
            analysis_list.Add(string.Format("S T A T I C   S O L U T I O N   T I M E   L O G"));
            analysis_list.Add(string.Format("O V E R A L L   T I M E   L O G"));
            analysis_list.Add(string.Format("SUMMARY OF MAXIMUM SUPPORT FORCES"));



            analysis_list.Add(string.Format("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD"));
            analysis_list.Add(string.Format("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD"));
            //analysis_list.Add(string.Format("CROSS GIRDER"));
            //analysis_list.Add(string.Format("STRINGER BEAM"));
            //analysis_list.Add(string.Format("BOTTOM CHORD"));
            //analysis_list.Add(string.Format("TOP CHORD"));
            //analysis_list.Add(string.Format("END RAKERS"));
            //analysis_list.Add(string.Format("DIAGONAL MEMBER"));
            //analysis_list.Add(string.Format("VERTICAL MEMBER"));
            //analysis_list.Add(string.Format("TOP CHORD BRACINGS"));
            //analysis_list.Add(string.Format("BOTTOM CHORD BRACINGS"));

            analysis_list.Add(string.Format("STRINGER BEAM"));
            analysis_list.Add(string.Format("CROSS GIRDER"));
            analysis_list.Add(string.Format("BOTTOM CHORD"));
            analysis_list.Add(string.Format("TOP CHORD"));
            analysis_list.Add(string.Format("END RAKERS"));
            analysis_list.Add(string.Format("DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("VERTICAL MEMBER"));
            analysis_list.Add(string.Format("TOP CHORD BRACINGS"));
            analysis_list.Add(string.Format("BOTTOM CHORD BRACINGS"));
            analysis_list.Add(string.Format("CANTILEVER BRACKETS"));
            analysis_list.Add(string.Format("SHORT VERTICAL MEMBER"));
            analysis_list.Add(string.Format("TOP DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("BOTTOM DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("TOP VERTICAL MEMBER"));
            analysis_list.Add(string.Format("BOTTOM VERTICAL MEMBER"));
            analysis_list.Add(string.Format("SHORT DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("ARCH MEMBERS"));
            analysis_list.Add(string.Format("SUSPENSION CABLES"));
            analysis_list.Add(string.Format("TRANSVERSE MEMBER"));

            analysis_list.Add(string.Format("MEMBER GROUP"));



            #endregion


            List<string> list = new List<string>(rtb_ana_rep.Lines);
            //list.Sort();
            int indx = 0;

            for (int i = 0; i < list.Count; i++)
            {

                var item = list[i];

                if (item.StartsWith("*")) continue;
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
                        hash_index.Add(Items.Count - 1, indx);
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
                                hash_index.Add(Items.Count - 1, indx);
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
                RichTextBox rtbData = rtb_ana_rep;
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
        bool IsFlag = false;

        #endregion Process

        #region Post Process

        string file_name = "";
        ASTRADoc astDoc;
        MCLSS.StructureMemberAnalysis StructureAnalysis { get; set; }

        public ASTRADoc AST_DOC_ORG { get; set; }

        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;

        //int lastId = -1;

        int iLoadCase;

        public string File_Name
        {
            get
            {
                return file_name;
            }
            set
            {
                txt_input_file.Text = value;
                this.Text = "Analysis Process & Results [" + MyStrings.Get_Modified_Path(value) + "]";
                file_name = value;
            }
        }

        bool IsMovingLoad = true;


        public void Set_Moving_Load_File(string file_name, bool moving_load)
        {
            InitializeComponent();
            Base_Control_MouseEvent();
        }
        public void Set_Moving_Load_File()
        {

            InitializeComponent();
            Base_Control_MouseEvent();

            //AST_DOC = new ASTRADoc();
            //AST_DOC_ORG = new ASTRADoc();
        }

        private void Base_Control_MouseEvent()
        {
        }

        private void frm_ASTRA_Analysis_Load(object sender, EventArgs e)
        {
            StructureAnalysis = null;
        }


        private void SaveDrawing(vdDocument VD)
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

        private void frm_ASTRA_Analysis_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btn_open_ana_rep_Click(object sender, EventArgs e)
        {
            string open_file = "";
            Button btn = sender as Button;

            if (File.Exists(open_file))
                System.Diagnostics.Process.Start(open_file);
        }

        void PP_Show_Panel(TabPage tp)
        {
        }

        private void PP_Tab_Selection()
        {
            this.Refresh();
        }

        private void PP_timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        public void CleanMemory()
        {
            try
            {
                VectorDraw.Professional.Memory.vdMemory.Collect();
                GC.Collect();
            }
            catch (Exception ex) { }
        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion Post Process

       

        #endregion Processing

       
        #region Create Data


        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    //if (Project_Type == eASTRADesignType.Structure_Modeling) 
                        return "STRUCTURAL ANALYSIS [BS]";
                }
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                {
                    //if (Project_Type == eASTRADesignType.Structure_Modeling) 
                    return "STRUCTURAL ANALYSIS [LRFD]";

                }
                else
                {
                    return "STRUCTURAL ANALYSIS [IRC]";
                }
                return "STRUCTURAL ANALYSIS";
            }
        }

        public List<string> Get_TowerData()
        {
            string file_name = Path.Combine(Application.StartupPath, @"DESIGN\Transmission Tower\Model Input Data.txt");

            List<string> list = new List<string>();

            if (File.Exists(file_name))
            {
                list = new List<string>(File.ReadAllLines(file_name));
            }
            return list;
        }


        private void btn_Create_Data_Click(object sender, EventArgs e)
        {


            if (Path.GetFileName(user_path) != Project_Name)
                Create_Project();

             Drawing_File = Path.Combine(user_path, "STRUCTURE.VDML");

             IsFlag = false;
            MessageBox.Show(this, "Analysis Input data is created as STRUCTURE.VDML inside the Project Folder", "ASTRA");
        }


        #endregion Create Data

        TowerDesign complete_design = null;
        BridgeMemberAnalysis Truss_Analysis = null;

        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                return iApp.Tables.IS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                return iApp.Tables.IS_SteelAngles;
            }
        }

        bool isCreateData = true;
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            rft.M2 = true;
            rft.M3 = true;
            rft.R3 = true;
            rft.R2 = true;
            return rft;
        }

        public void OpenAnalysisFile(string file_name)
        {
            user_path = Path.GetDirectoryName(file_name);

            INPUT_FILE = file_name;
            string analysis_file = INPUT_FILE;
            
            Button_Enable_Disable();
        }
        public delegate void SetProgressValue(ProgressBar pbr, int val);


        void SetMemberDetails()
        {
            for (int i = 0; i < complete_design.Members.Count; i++)
            {
                try
                {
                    complete_design.Members[i].Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(complete_design.Members[i].Group.GroupName).MemberNosText;
                }
                catch (Exception ex)
                {
                    complete_design.Members[i].Group.MemberNosText = "";

                }
                complete_design.Members[i].Group.SetMemNos();
                //Truss_Analysis.GetForce(ref complete_design.Members[i]);
            }
        }
        public string Analysis_Path
        {
            get
            {


                return user_path;

                //return iApp.LastDesignWorkingFolder;
                //return iApp.LastDesignWorkingFolder;

            }
        }
        string user_path
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
        string system_path = "";



        private void Open_Analysis_Report()
        {
            string ana_rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            if (File.Exists(ana_rep_file))
            {

                #region Chiranjit [2013 05 16]

                List<string> Work_List = new List<string>();

                Work_List.Add("Reading Analysis Data from Analysis Report File (ANALYSIS_REP.TXT)");
                //Work_List.Add("Set Structure Geometry for Analysis");
                //Work_List.Add("Reading Bending Moment & Shear Force from Analysis Result");
                iApp.Progress_Works = new ProgressList(Work_List);
                #endregion Chiranjit [2013 05 16]

                //MessageBox.Show("Next, the Program will be reading the analysis results, it may take some times, please wait till the analysis results are displayed.", "ASTRA", MessageBoxButtons.OK);
                return;
                Truss_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file, GetForceType());

                if (iApp.Is_Progress_Cancel)
                {
                    iApp.Progress_Works.Clear();
                    return;
                }


                List<string> list_node = new List<string>();

                try
                {
                    if (Truss_Analysis.Node_Displacements == null)
                    {
                        MessageBox.Show("Node Displacements not found in the Analysis Result.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }

                    list_node.Add(Truss_Analysis.Node_Displacements.Get_Max_Deflection().ToString());

                    string kStr = Truss_Analysis.Analysis.Supports[0].NodeNo + " TO "
                                            + Truss_Analysis.Analysis.Supports[3].NodeNo;

                    List<int> jnts = MyList.Get_Array_Intiger(kStr);

                    for (int i = 0; i < jnts.Count; i++)
                    {
                        foreach (var item in Truss_Analysis.Node_Displacements)
                        {
                            if (item.NodeNo == jnts[i] && item.LoadCase == 1)
                            {
                                list_node.Add(item.ToString());
                            }
                        }
                    }

                    string file_load_def = Path.Combine(Analysis_Path, "MAX_LOAD_DEFLECTION.TXT");
                    File.WriteAllLines(file_load_def, list_node.ToArray());
                }
                catch (Exception e2x) { }
                iApp.Progress_Works.Clear();

            }



            string kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
            if (File.Exists(kFile))
            {
                //angle thickness not comming
                //SetCompleteDesign(kFile);

                //ReadResult();
            }

            Button_Enable_Disable();

        }

        private void Button_Enable_Disable()
        {
        }


        double DL, LL, IL, h, l, fy, fc, ft, d;
        double sigma_b, sigma_c;

        //Chiranjit [2014 03 24] Add new Method for Update user force


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

        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            //if (showMessage) DemoCheck();

            if (user_path != iApp.LastDesignWorkingFolder)
            {
                //iApp.Save_Form_Record(this, user_path);
                iApp.Save_Form_Record(tab_create_project, user_path);
            }
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
                //BridgeAnalysisDesign.frm_Open_Project frm = new BridgeAnalysisDesign.frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                BridgeAnalysisDesign.frm_Open_Project frm = new BridgeAnalysisDesign.frm_Open_Project(this.tab_create_project.Text, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);

                    //string file_name = txt_analysis_file.Text;
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);

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


                    Read_All_Data();

                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);
                    txt_Project_Name2.Text = Path.GetFileName(frm.Example_Path);

                    File_Name = txt_input_file.Text;


                    if(rbtn_SAP.Checked)
                    {
                    }
                    IsFlag = false;
                    //Write_All_Data();

                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                isCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
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

       

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        private void Open_Project()
        {
            string file_name = Path.Combine(user_path, "TRANSMISSION_TOWER_ANALYSIS.TXT");

            //IsWarren2
            isCreateData = false;
            //Read_All_Data();
            //return;
            if (!File.Exists(file_name)) return;

            INPUT_FILE = file_name;
            File_Name = file_name;
            OpenAnalysisFile(file_name);
            Open_Analysis_Report();

            //Read_All_Data();


            IsFlag = false;
            try
            {
                string s1, s2;
                s1 = s2 = "";
                for (int j = 0; j < Truss_Analysis.Analysis.Supports.Count; j++)
                {
                    if (j < Truss_Analysis.Analysis.Supports.Count / 2)
                        s1 += Truss_Analysis.Analysis.Supports[j].NodeNo + ",";
                    else
                        s2 += Truss_Analysis.Analysis.Supports[j].NodeNo + ",";
                }

                //frm_ViewForces(Truss_Analysis.Analysis.Width, DL_Report_Analysis_File, LL_Report_Analysis_File, (s1 + s2));
                //frm_ViewForces_Load();

                //frm_Pier_ViewDesign_Forces(Total_Report_Analysis_File, s1, s2);
                //frm_ViewDesign_Forces_Load();

            }
            catch (Exception ex) { }
            //Read_All_Data();
            Button_Enable_Disable();
        }


        public void Read_All_Data()
        {
            //iApp.Read_Form_Record(this, user_path);
            iApp.Read_Form_Record(tab_create_project, user_path);
        }
        #endregion Chiranjit [2016 09 07]

        Hashtable ht_Lst_X = new Hashtable();
        Hashtable ht_Lst_Z = new Hashtable();


        private void btn_tutorial_example_Click(object sender, EventArgs e)
        {
            txt_project_name.Text = "";

            if (rbtn_3D_Drawing.Checked)
                Project_Name = "Drawing Tutorial for " + analysis_title;
            else if (rbtn_SAP.Checked)
                Project_Name = "SAP Tutorial for " + analysis_title;
            else
                Project_Name = "Tutorial for " + analysis_title;

            //Create_Project();
            Load_Input_Examples();
        }

        public void Load_Input_Examples()
        {

            frmStructuralExamples ff = new frmStructuralExamples(iApp, true);


            ff.rbtn_dwg.Checked = rbtn_3D_Drawing.Checked;
            ff.rbtn_SAP.Checked = rbtn_SAP.Checked;
            ff.rbtn_TEXT.Checked = rbtn_TEXT.Checked;


            ff.rbtn_dwg.Enabled = false;
            ff.rbtn_SAP.Enabled = false;
            ff.rbtn_TEXT.Enabled = false;

            ff.ShowDialog();
            if (ff.Example_Title == null) return;
            txt_project_name.Text = ff.Example_Title;
            Create_Project();

            //File_Name = iApp.EXAMPLE_File;


            string flName = Path.Combine(user_path, Path.GetFileName(iApp.EXAMPLE_File));
            File.Copy(iApp.EXAMPLE_File, flName, true);

            var ll1 = MyList.Get_LL_TXT_File(iApp.EXAMPLE_File);
            var ll2 = MyList.Get_LL_TXT_File(flName);

            if (File.Exists(ll1)) File.Copy(ll1, ll2, true);

            File_Name = flName;
            IsFlag = false;
            Clear_All();

            return;

            MyList ml = new MyList(analysis_type, '_');

            int anaType = ml.GetInt(1);


            int example_index = -1;

            if (rbtn_SAP.Checked)
            {
                if (anaType == 20) anaType = 13;
                else if (anaType == 21) anaType = 14;
            }


            string example_path = Path.Combine(Application.StartupPath, @"ASTRA Pro Analysis Examples\01 Analysis with Text Data File");

            if (rbtn_3D_Drawing.Checked)
            {
                example_path = Path.Combine(Application.StartupPath, @"ASTRA Pro Analysis Examples\02 Analysis with Drawing File");
            }
            else if (rbtn_SAP.Checked)
            {
                example_path = Path.Combine(Application.StartupPath, @"ASTRA Pro Analysis Examples\03 Analysis with SAP Data File");
            }



            List<string> lst_dir = new List<string>(Directory.GetDirectories(example_path));

            string src_path = lst_dir[anaType - 1];



            string drawing_file = Path.Combine(src_path, "Structure_Model.vdml");

            if (anaType == 2 || anaType == 12)
            {
                frmAnalysisExamples exa = new frmAnalysisExamples(anaType);
                exa.Owner = this;
                exa.ShowDialog();

                //if (anaType == 2)
                //{
                example_index = exa.Selected_Index;
                //}


                lst_dir = new List<string>(Directory.GetDirectories(src_path));

                src_path = lst_dir[example_index];

            }

            if (rbtn_SAP.Checked)
            {
                var dsa = Directory.GetDirectories(src_path);
                //src_path = Path.Combine(src_path, "Analysis Input Data");

                src_path = Path.Combine(src_path, dsa[0]);

            }
            else
            {
                src_path = Path.Combine(src_path, "Analysis Input Data");
            }

            string copy_path =  user_path;
            DirectoryCopy(src_path, copy_path, true);




            lst_dir = new List<string>(Directory.GetFiles(copy_path));

            foreach (var item in lst_dir)
            {
                if (rbtn_SAP.Checked)
                {
                    if (Path.GetFileName(item).ToUpper().Contains("SAP") && !Path.GetFileName(item).ToUpper().Contains("REP"))
                    {
                        File_Name = item;
                        break;
                    }
                }
                else
                {
                    if (Path.GetFileName(item).ToUpper().StartsWith("INPUT"))
                    {
                        File_Name = item;
                        break;
                    }
                }
            }
            txt_input_file.Text = File_Name;
            IsFlag = false;
            //File_Name = Path.Combine()

            string drg_file = Path.Combine(user_path, "Structure_Model.vdml");

            if (rbtn_3D_Drawing.Checked)
            {
                if (File.Exists(drawing_file))
                {
                    File.Copy(drawing_file, drg_file, true);
                    uC_CAD_Model.VDoc.Open(drg_file);

                    uC_CAD_Model.VDoc.Redraw(true);

                    VDRAW.vdCommandAction.View3D_VTop(uC_CAD_Model.VDoc);
                }
            }

            MessageBox.Show("Tutorial Data Loaded Successfully.", "ASTRA", MessageBoxButtons.OK);
        }

        public bool Close_Message(bool msg)
        {

            if (txt_input_file.Text == "") return true;

            System.Windows.Forms.DialogResult erd = System.Windows.Forms.DialogResult.Yes;

            if (msg)
            {
                erd = (MessageBox.Show("Do you want to Save Project Data file ?", "HEADS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

            }
            if (erd == System.Windows.Forms.DialogResult.Cancel) return false;

           
            //MyList.

            return true;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {

            if (!Directory.Exists( sourceDirName)) return;
            if (destDirName == "") return;
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            //destDirName = Path.Combine(destDirName, "HEADS Pro Tutorials");
            //destDirName = Path.Combine(destDirName, Path.GetFileName(sourceDirName));
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        private void btn_input_browse_Click(object sender, EventArgs e)
        {
            string flName = "";
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                if (rbtn_3D_Drawing.Checked)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {

                        Drawing_File = ofd.FileName;
                        if (!uC_CAD_Model.VDoc.Open(Drawing_File))
                        {
                            MessageBox.Show(Drawing_File + " could not be opened."); return;
                        }

                        uC_CAD_Model.View_Buttons = true;
                        VDRAW.vdCommandAction.View3D_VTop(uC_CAD_Model.VDoc);
                        IsDrawingFileOpen = true;
                        this.Text = "Analysis Input Data File [" + MyStrings.Get_Modified_Path(Drawing_File) + "]";

                        File_Name = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");
                        txt_input_file.Text = File_Name;
                    }
                }
                else
                {
                    ofd.Filter = "All Text Data File (*.txt)|*.txt";
                    if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        flName = Path.Combine(user_path, Path.GetFileName(ofd.FileName));
                        if (ofd.FileName.ToLower() != flName.ToLower())
                        File.Copy(ofd.FileName, flName, true);

                        var ll = MyList.Get_LL_TXT_File(ofd.FileName);

                        if(File.Exists(ll))
                        {
                            var ll2 = Path.Combine(Path.GetDirectoryName(flName), "LL.TXT");
                            File.Copy(ll, ll2, true);
                        }
                        File_Name = flName;
                        IsFlag = false;
                        Clear_All();
                    }
                }
            }


        }

        private void rbtn_3D_Drawing_CheckedChanged(object sender, EventArgs e)
        {
            uC_CAD_Model.Visible = rbtn_3D_Drawing.Checked;

            if (rbtn_SAP.Checked)
            {
            }
            else
            {

            }
        }

        private void frmAnalysisWorkspaceNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeDoc(uC_CAD_Model.VDoc);
        }
        public void DisposeDoc(vdDocument dc)
        {
            try
            {
                dc.Dispose();
            }
            catch (Exception ex) { }
        }

        private void btn_preprocess_data_Click(object sender, EventArgs e)
        {
            if (rbtn_TEXT.Checked || rbtn_3D_Drawing.Checked)
            {
                iApp.View_PreProcess(File_Name);
            }
            if (rbtn_SAP.Checked)
            {
                iApp.View_SapPreProcess(File_Name);
            }
        }

        private void btn_postprocess_data_Click(object sender, EventArgs e)
        {
            if (rbtn_TEXT.Checked || rbtn_3D_Drawing.Checked)
            {
                iApp.View_PostProcess(File_Name);
            }
            if (rbtn_SAP.Checked)
            {
                iApp.View_SapPostProcess(File_Name);
            }
        }

        private void btn_open_data_Click(object sender, EventArgs e)
        {
            if (File.Exists(DataFileName)) System.Diagnostics.Process.Start(DataFileName);
        }

        private void btn_view_analysis_Click(object sender, EventArgs e)
        {
            string ana_rep = "";

            if (rbtn_SAP.Checked)
            {
                ana_rep = MyList.Get_SAP_Analysis_Report_File(DataFileName);
                if (File.Exists(ana_rep)) System.Diagnostics.Process.Start(ana_rep);
            }
            else
            {
                ana_rep = MyList.Get_Analysis_Report_File(DataFileName);
                if (File.Exists(ana_rep)) System.Diagnostics.Process.Start(ana_rep);
        
            }

        }


    }

}
