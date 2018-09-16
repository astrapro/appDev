using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;
using HEADSNeed.ASTRA.ASTRAForms;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

using VDRAW = VectorDraw.Professional.ActionUtilities;
using AstraInterface.Interface;


namespace ASTRAStructures
{
    public partial class frm_StructuralDesignNew : Form
    {
        IApplication iApp;
        //Chiranjit [2015 05 05]
        bool Is_TextEdit;

        bool IsSavedData { get; set; }
        #region Design Variables
        BillOfQuantity STRUCTURE_BOQ;
        
        #endregion

        //public frm_ASTRA_Inputs()
        //{
        //    InitializeComponent();
        //}
        string work_fold = "";
        public string Working_Folder
        {
            get
            {
                if (file_name == "")
                {
                    if (!Directory.Exists(work_fold))
                    {
                        using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                        {
                            if (fbd.ShowDialog() != DialogResult.Cancel)
                                work_fold = fbd.SelectedPath;
                        }
                    }
                }
                else
                    work_fold = Path.GetDirectoryName(file_name);

                return work_fold;
            }
            set
            {
                work_fold = value;
            }
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
        #region Pre Process

     
        public ASTRADoc AST_DOC
        {
            get
            {
                if (ACad != null)
                    return ACad.AstraDocument;
                return null;
            }
            set
            {
                if (ACad != null)
                    ACad.AstraDocument = value;
            }
        }
        ASTRACAD ACad;

        int lastId = 0;

        public int CurrentLoadIndex { get; set; }

        bool IsDrawingFileOpen { get; set; }

        public LoadCaseDefinition Current_LoadCase
        {
            get
            {
                try
                {
                    return LoadCases[CurrentLoadIndex];
                }
                catch (Exception ex) { }
                return null;

            }
            set
            {
                try
                {
                    LoadCases[CurrentLoadIndex] = value;
                }
                catch (Exception ex) { }
            }
        }
        public List<string> SeismicLoads { get; set; }
        public List<LoadCaseDefinition> LoadCases { get; set; }
        public List<MovingLoadData> MovingLoads { get; set; }
        public List<LiveLoad> LL_Definition { get; set; }
        public List<MaterialProperty> Materials { get; set; }
        MemberGroupCollection Groups { get; set; }

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

        public frm_StructuralDesignNew(string input_file)
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


                ACad = new ASTRACAD();
                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new MemberGroupCollection();
                Materials = new List<MaterialProperty>();

                IsDrawingFileOpen = false;
            }
            catch (Exception ex) { }

        }

        public frm_StructuralDesignNew(string drawing_file, bool IsDrawingFile)
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


                ACad = new ASTRACAD();
                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new MemberGroupCollection();
                Materials = new List<MaterialProperty>();

            }
            catch (Exception ex) { }

        }


        public frm_StructuralDesignNew(IApplication iapp)
        {
            //this.Show
            try
            {
                iApp = iapp;



                InitializeComponent();

                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();
                Drawing_File = "";
                File_Name = "";
                ACad = new ASTRACAD();

                Working_Folder = iapp.LastDesignWorkingFolder;
                LastDesignWorkingFolder = iapp.LastDesignWorkingFolder;

                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new MemberGroupCollection();
                Materials = new List<MaterialProperty>();

                IsDrawingFileOpen = false;

            }
            catch (Exception ex) { }

        }


        private void Tab_Selection()
        {
        }
        public void Show_Panel(TabPage tp)
        {
        }

        public void ClearSelect()
        {
        }

        private void frm_ASTRA_Inputs_Load(object sender, EventArgs e)
        {
            uC_SteelSections_Beam.SetIApplication(iApp);
            uC_SteelSections_Column.SetIApplication(iApp);

            grb_beam_individual.Visible = false;
            grb_column_individual.Visible = false;
            grb_footing_individual.Visible = false;
            grb_pile_individual.Visible = false;
            //cmb_stage.SelectedIndex = 0;

            grb_stage.Visible = (StageAnalysisForm != null);
            Set_Project_Name();

            IsCreateData = true;

            IsSavedData = true;
            for (int i = 1; i < 100; i++)
            {
                cmb4_text_size.Items.Add(i.ToString());
            }

            Button_Enable_Disable();

            timer1.Start();

            cmb_slab_fck.SelectedIndex = 1;
            cmb_slab_fy.SelectedIndex = 1;

            cmb_beam_fck.SelectedIndex = 1;
            cmb_beam_fy.SelectedIndex = 1;

            cmb_column_fck.SelectedIndex = 1;
            cmb_column_fy.SelectedIndex = 1;

            cmb_strcase_fck.SelectedIndex = 1;
            cmb_strcase_fy.SelectedIndex = 1;


            cmb_ifoot_fck.SelectedIndex = 1;
            cmb_ifoot_fy.SelectedIndex = 1;


            cmb_raft_fck.SelectedIndex = 1;
            cmb_raft_fy.SelectedIndex = 1;

            #region Pile Foundation
            cmb_pile_fck.SelectedIndex = 1;
            cmb_pile_fy.SelectedIndex = 1;

            cmb_pcap_fck.SelectedIndex = 1;
            cmb_pcap_fy.SelectedIndex = 1;


            cmb_Np.SelectedIndex = 2;
            pile = new PileFoundation();
            Pile_Foundation_Load();
            #endregion Pile Foundation


            STRUCTURE_BOQ = new BillOfQuantity();

            STRUCTURE_BOQ.DGV_SLAB_CONC = dgv_slab_concrete;
            STRUCTURE_BOQ.DGV_SLAB_STEEL = dgv_slab_steel;

            STRUCTURE_BOQ.DGV_BEAM_CONC = dgv_beam_concrete;
            STRUCTURE_BOQ.DGV_BEAM_STEEL = dgv_beam_steel;


            STRUCTURE_BOQ.DGV_COLUMN_CONC = dgv_column_concrete;
            STRUCTURE_BOQ.DGV_COLUMN_STEEL = dgv_column_steel;

            //STRUCTURE_BOQ.DGV_COLUMN_CONC = dgv_founda_concrete;
            //STRUCTURE_BOQ.DGV_COLUMN_STEEL = dgv_column_steel;

            //Chiranjit [2015 04 06] Load
            Load_Rebar_Weight();

            dgv_dls.Rows.Add("Fixed Load", "4.0");

            //Default_Raft_Data();



            //if (Working_Folder != "") ;

            //work_fold = @"C:\Users\prac\Desktop\ASTRA Pro Structure Design";
            if (work_fold != "")
            {

                string src_path = Path.Combine(Application.StartupPath, "Example Line Diagram Model");
                src_path = Path.Combine(src_path, "Structure_drawing.dwg");
                string des_path = Path.Combine(work_fold, "Structure_drawing.dwg");

                if (!File.Exists(des_path) && File.Exists(src_path))
                {
                    frm_Load_Line_Diagram ff = new frm_Load_Line_Diagram(work_fold);
                    ff.Owner = this;
                    if (ff.ShowDialog() == DialogResult.Yes)
                    {
                        if (File.Exists(src_path))
                        {
                            File.Copy(src_path, des_path, true);
                            MessageBox.Show(this, "Line Diagram Model Drawing file is created as " + des_path, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }


        }
        private void Clear_All()
        {
            beamsNos.Clear();
        }
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
        }
        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_Selection();
        }

        

        private void btn_file_save_Click(object sender, EventArgs e)
        {
            if (File.Exists(Drawing_File))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML Files (*.vdml)|*.vdml|DXF Files (*.dxf)|*.dxf|DWG Files (*.dwg)|*.dwg";

                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                    }
                }
            }
        }

        private void frm_DrawingToData_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        List<string> Seismic_Load = new List<string>();
        List<string> Seismic_Combinations = new List<string>();


        private void Save_Data()
        {

            Seismic_Load.Clear();
            Seismic_Combinations.Clear();

            //if (!File.Exists(File_Name))
            //{

            if (user_path == null)
            {
                user_path = Path.GetDirectoryName(File_Name);
            }
            if (!Directory.Exists(user_path))
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = user_path;
                sfd.FileName = Path.GetFileName(File_Name);
                sfd.Filter = "TEXT Files (*.txt)|*.txt";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                    File_Name = sfd.FileName;
                else
                    return;
            }

            File_Name = Path.Combine(user_path, "INPUT_DATA.TXT");

            //}

            //fname = File_Name;

        }

        public string Get_Load_Name(int type)
        {
            foreach (var item in MovingLoads)
            {
                if (item.Type == type)
                    return item.Name;
            }
            return "";
        }


        private void Button_Enable_Disable()
        {
        }

        private void Open_Data_File(string fn)
        {

            Clear_All();
            ASTRADoc astdoc;
            astdoc = new ASTRADoc(fn);
            ACad.AstraDocument = astdoc;

            dgv_GD_floor.Rows.Clear();
            dgv_Staircase_Floors.Rows.Clear();

            List<double> floors = astdoc.Joints.Get_Floors();

            cmb_dwg_flr_lvl.Items.Clear();
            //cmb_dwg_flr_lvl.Items.Add(0);
            for (int i = 0; i < floors.Count - 1; i++)
            {
                var item = floors[i];
                dgv_GD_floor.Rows.Add(dgv_GD_floor.RowCount + 1, item.ToString("f3"));
                dgv_Staircase_Floors.Rows.Add(dgv_GD_floor.RowCount + 1, item.ToString("f3"), (floors[i + 1] - item).ToString("f3"));

                cmb_dwg_flr_lvl.Items.Add(item);
            }

            if (floors.Count > 0)
                cmb_dwg_flr_lvl.Items.Add(floors[floors.Count - 1]);



            Groups.Clear();
           
            this.Text = "Building Frame Analysis Design [ " + MyStrings.Get_Modified_Path(fn) + " ]";
            if (File.Exists(fn))
                rtb_input_file.Lines = File.ReadAllLines(fn);
            else
                rtb_input_file.Text = "";

            if (File.Exists(MyStrings.Get_LL_TXT_File(fn)))
            {
                rtb_ll_txt.Lines = File.ReadAllLines(MyStrings.Get_LL_TXT_File(fn));
            }


            if (File.Exists(Analysis_File_Name))
            {
                rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                StructureAnalysis = null;
                ld = null;
                Select_Steps();
            }
            Tab_Selection();
        }


        private void tsmi_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Delete_Layer_Items(string LayerName)
        {
           
        }
       
        private void chk_joints_CheckedChanged(object sender, EventArgs e)
        {
        }


        private void Load_Example_Project_Data()
        {
            if (Directory.Exists(LastDesignWorkingFolder))
            {
                user_path = Path.Combine(LastDesignWorkingFolder, Title);
                if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);
                Working_Folder = user_path;
            }
            if (Working_Folder != "")
            {
                frm_LoadExample fle = new frm_LoadExample();
                int pj = 1;
                if (fle.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                pj = fle.Project_Example;

                string src_path = Path.Combine(Application.StartupPath, "DESIGN\\Building_Example_data" + pj + ".txt");

                Project_Name = "TUTORIAL EXAMPLE " + pj.ToString("00");


                user_path = Path.Combine(Working_Folder, Project_Name);
                Create_Project();



                if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);

                Working_Folder = user_path;


                //string dest_path = Path.Combine(Working_Folder, "Building Example " +pj +" Data");

                string dest_path = user_path;


                if (!Directory.Exists(dest_path))
                    Directory.CreateDirectory(dest_path);
                //dest_path = Path.Combine(dest_path, "Building_Example_data" + pj + ".txt");
                dest_path = Path.Combine(dest_path, "INPUT_DATA.TXT");

                if (File.Exists(src_path))
                {
                    File.WriteAllLines(dest_path, File.ReadAllLines(src_path));
                }
                if (File.Exists(dest_path))
                {
                    File_Name = dest_path;
                    Open_Data_File(dest_path);
                    MessageBox.Show(this, "Analysis Example data created in the working folder as " + dest_path, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        //bool FirstOpen = false;
        private void tv_mem_props_Click(object sender, EventArgs e)
        {
        }
        private void tsmi_newAnalysisTXTDataFile_Click(object sender, EventArgs e)
        {
            if (!Save_File_On_Close())
            {
                Clear_All();
                File_Name = "";
                Open_Data_File(File_Name);
            }

        }

        #endregion Pre Process

        #region Process
        public string Analysis_File_Name
        {
            get
            {
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
            ListJoints.Add(448);
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
            ListMembers.Add(1050);
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
            #region Chiranjit [2015 01 15]

            //HEADSNeed.ASTRA.ASTRAClasses.StructureDesign.StructureDesign sdes =
            //    new HEADSNeed.ASTRA.ASTRAClasses.StructureDesign.StructureDesign(DataFileName);
            //sdes.Write_Beam_Column_Members();
            //sdes.Write_Floors();
            //sdes.Create_Members();

            #endregion Chiranjit [2015 01 15]


            if (Check_Demo_Version())
            {
                //if (txt_seismic_coeeficient.Text != "0.12")
                //{
                //    MessageBox.Show("In Un version Seismic Coefficient 

                //}
                
                //return;
            }



            AST_DOC = new ASTRADoc(DataFileName);

            List<int> lm = new List<int>();
            foreach (var item in AST_DOC.Members)
            {
                if (item.Property == null)
                {
                    lm.Add(item.MemberNo);
                }
            }
            if (lm.Count >= 1)
            {
                if (lm.Count == 1)
                {
                    MessageBox.Show("Member Property is not defined for Member No :" + lm[0], "ASTRA", MessageBoxButtons.OK);
                }
                else if (lm.Count > 1)
                {
                    MessageBox.Show("Member Property is not defined for Member Nos :" + MyList.Get_Array_Text(lm), "ASTRA", MessageBoxButtons.OK);
                }
                return;
            }
            RunAnalysis();
            AST_DOC_ORG = new ASTRADoc(DataFileName);
            //Load_ASTRA_Data();
            Open_Data_File(DataFileName);

            if (SeismicLoads == null) SeismicLoads = new List<string>();



            if (Seismic_Coeeficient != 0)
            {
                if (!File.Exists(AST_DOC.AnalysisFileName)) return;
                Run_Seismic_Analysis();
            }
            //ld = null ;
            //dgv_node_disp.Rows.Clear();

            //dgv_beam_frcs.RowCount = 0;
            //dgv_max_frc.RowCount = 0;

            AST_DOC = AST_DOC_ORG;

            cmb_stage.SelectedIndex = 0;
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

        public bool RunAnalysis()
        {
            if (!File.Exists(File_Name)) return false;
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            string fName = File_Name;
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
            return true;
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

                string ana_file = MyStrings.Get_Analysis_Report_File(fName);
                if (File.Exists(ana_file))
                {
                    rtb_ana_rep.Lines = File.ReadAllLines(ana_file);
                    StructureAnalysis = null;
                    ld = null;
                    Select_Steps();
                }
            }
            catch (Exception exx)
            {
            }

            return true;
        }



        public void Write_Seismic_Data(List<string> LoadData, List<string> Load_Comb, string file_name)
        {
        }

        public void Run_Seismic_Analysis()
        {
            return;

            if (MessageBox.Show(this, "Do you want to Proceed for Seismic Load Analysis ?",
                 "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;
            return;

            frm_SeismicAnalysis fsa = new frm_SeismicAnalysis(ACad, LoadCases);
            fsa.Write_Seismic_Data += new dWrite_Seismic_Data(Write_Seismic_Data);
            fsa.File_Seismic_Load = File_Seismic_Load;
            fsa.RunAnalysis += new dRunAnalysis(RunAnalysis);

            //MyList ml = new MyList(SeismicLoads[0], ' ');

            fsa.SC = Seismic_Coeeficient;
            //fsa.Direction = ml.StringList[2];
            fsa.ShowDialog();

            //string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
            //if (File.Exists(ana_file))
            //    StructureAnalysis = new StructureMemberAnalysis(ana_file);
            //StructureAnalysis = 
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

        private void tc_pp_main_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion Process

        #region Post Process

        string file_name = "";
        ASTRADoc astDoc;
        StructureMemberAnalysis StructureAnalysis { get; set; }
      
        public ASTRADoc AST_DOC_ORG { get; set; }

        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;

        public string File_Name
        {
            get
            {
                return file_name;
            }
            set
            {
                this.Text = "Building Frame Analysis Design [" + MyStrings.Get_Modified_Path(value) + "]";
                file_name = value;
            }
        }

        bool IsMovingLoad = true;


        public void Set_Moving_Load_File(string file_name, bool moving_load)
        {
            InitializeComponent();
            Base_Control_MouseEvent();
            AST_DOC = new ASTRADoc(file_name);
            IsMovingLoad = moving_load;
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
        }

        private void Load_ASTRA_Data()
        {
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

        List<int> list_joint_index = new List<int>();
       
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void chk_show_steps_CheckedChanged(object sender, EventArgs e)
        {
            spc_results.Panel1Collapsed = !chk_show_steps.Checked;
        }

        private void btn_update_file_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (IsCreateData == false)
            {
                if (!File.Exists(DataFileName) || btn.Name != btn_update_file.Name)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Text Files |*.txt";
                        sfd.FileName = Path.GetFileName(DataFileName);
                        if (sfd.ShowDialog() != DialogResult.Cancel)
                        {
                            DataFileName = sfd.FileName;
                            File.WriteAllText(DataFileName, "ASTRA");
                        }
                        else
                            return;
                    }
                }
            }


            if (File.Exists(DataFileName))
            {
                File.WriteAllLines(DataFileName, rtb_input_file.Lines);
                Open_Data_File(DataFileName);

                if (btn.Name == btn_update_file.Name)
                    MessageBox.Show("Data File Updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data File Created.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        #endregion Post Process


        private void tsmi_viewer_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "VDML Drawing File(*.vdml)|*.vdml|DXF Drawing File(*.dxf)|*.dxf|DWG Drawing File(*.dwg)|*.dwg";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (iApp.Is_StructureDemo)
                    {
                        MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        //VDoc.SaveAs(sfd.FileName);
                        Drawing_File = sfd.FileName;

                        System.Environment.SetEnvironmentVariable("OPENFILE", Drawing_File);
                        //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                        System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                    }
                }
            }
        }

        private void btn_view_analysis_Click(object sender, EventArgs e)
        {

            if (File.Exists(Analysis_File_Name))
            {
                if (File.Exists(AST_DOC.AnalysisFileName))
                {
                    string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                    if (File.Exists(ana_file))
                        System.Diagnostics.Process.Start(ana_file);
                    else
                        System.Diagnostics.Process.Start(Analysis_File_Name);
                }

            }
            else
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_design_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            if (file_name == "")
                chk_slab_individual.Checked = true;

            SlabDesign sb = new SlabDesign();

            sb.Beam_Nos = "B1";
            sb.Report_File = Slab_Design_Report;

            //MyStrings
            //sb.AST_DOC = AST_DOC;



            if (AST_DOC_ORG != null)
                sb.AST_DOC = AST_DOC_ORG;
            else
                sb.AST_DOC = AST_DOC;



            //sb.Lx = MyList.StringToDouble(txt_slab_Lx.Text, 0.0);
            //sb.Ly = MyList.StringToDouble(txt_slab_Ly.Text, 0.0);
            //sb.Floor_Level = MyList.StringToDouble(txt_slab_flv_lvl.Text, 0.0);

            //sb.case_index = cmb_slab_type.SelectedIndex;

            sb.D = MyList.StringToDouble(txt_slab_D.Text, 0.0);
            sb.fck = MyList.StringToDouble(cmb_slab_fck.Text.Replace("M", ""), 0.0);
            sb.gamma = MyList.StringToDouble(txt_slab_gamma.Text, 0.0);
            sb.fy = MyList.StringToDouble(cmb_slab_fy.Text.Replace("Fe", ""), 0.0);
            sb.wll = MyList.StringToDouble(txt_slab_wll.Text, 0.0);
            sb.d1 = MyList.StringToDouble(txt_slab_d1.Text, 0.0);
            sb.s1 = MyList.StringToDouble(txt_slab_s1.Text, 0.0);
            sb.d2 = MyList.StringToDouble(txt_slab_d2.Text, 0.0);
            sb.s2 = MyList.StringToDouble(txt_slab_s2.Text, 0.0);
            sb.d3 = MyList.StringToDouble(txt_slab_d3.Text, 0.0);
            sb.s3 = MyList.StringToDouble(txt_slab_s3.Text, 0.0);
            sb.d4 = MyList.StringToDouble(txt_slab_d4.Text, 0.0);
            sb.s4 = MyList.StringToDouble(txt_slab_s4.Text, 0.0);
            sb.cover = MyList.StringToDouble(txt_slab_c.Text, 0.0);
            sb.dlf = MyList.StringToDouble(txt_slab_DLF.Text, 0.0);
            sb.llf = MyList.StringToDouble(txt_slab_LLF.Text, 0.0);
            sb.dgv_dls = dgv_dls;

            if (chk_slab_individual.Checked)
            {
                sb.Design_Program_Individual();


                if (File.Exists(Slab_Design_Report))
                {
                    MessageBox.Show("Report file created in file " + Slab_Design_Report);
                    frmASTRAReport.OpenReport(Slab_Design_Report, this, false);

                    //iApp.View_Result(Slab_Design_Report);
                }
            }
            else
            {


                frm_Slab_BOQ fsboq = new frm_Slab_BOQ(AST_DOC);
                fsboq.BOQ = STRUCTURE_BOQ;
                fsboq.Add_Slab_Boq = new sAdd_Slab_BOQ(Add_Slab_BOQ);

                fsboq.Slab_des = sb;
                fsboq.ShowDialog();
                //fsboq.Show();
            }

        }
        private string Slab_BOQ_Report
        {

            get
            {
                string rep_file = "";

                if (file_name == "")
                {
                    rep_file = Path.Combine(Working_Folder, "Structure Design");
                }
                else
                    rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Slab Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Slab_BOQ_Report.txt");
                return rep_file;
            }
        }

        private string Slab_Design_Report
        {

            get
            {
                string rep_file = "";

                if (file_name == "")
                {
                    rep_file = Path.Combine(Working_Folder, "Structure Design");
                }
                else
                    rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Slab Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Slab_Design_Report.txt");
                return rep_file;
            }
        }

        private string Slab_Design_Summary
        {

            get
            {
                string rep_file = "";

                if (file_name == "")
                {
                    rep_file = Path.Combine(Working_Folder, "Structure Design");
                }
                else
                    rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Slab Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Slab_Design_Summary.txt");
                return rep_file;
            }
        }


        private string Beam_Design_Report
        {

            get
            {

                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");


                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Beam Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
                return rep_file;
            }
        }

        private string Beam_BOQ_Report
        {

            get
            {

                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");


                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Beam Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Beam_BOQ_Report.txt");
                return rep_file;
            }
        }

        private string Beam_Design_Summary
        {

            get
            {

                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");


                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Beam Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Beam_Design_Summary.txt");
                return rep_file;
            }
        }


        private string Column_Design_Report
        {
            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");


                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Column Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Column_Design_Report.txt");
                return rep_file;
            }
        }

        private string Column_BOQ_Report
        {
            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");


                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Column Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Column_BOQ_Report.txt");
                return rep_file;
            }
        }

        private string Column_Design_Summary
        {
            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");


                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Column Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Column_Design_Summary.TXT");
                return rep_file;
            }
        }



        private string Staircase_Design_Report
        {

            get
            {
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Staircase Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Staircase_Design_Report.txt");
                return rep_file;
            }
        }

        private string Staircase_BOQ_Report
        {

            get
            {
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Staircase Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Staircase_BOQ_Report.txt");
                return rep_file;
            }
        }

        private string Staircase_Design_Summary
        {

            get
            {
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Staircase Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Staircase_Design_Summary.txt");
                return rep_file;
            }
        }


        private string Isolate_Footing_Design_Report
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Isolated Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Isolated_Foundation_Design_Report.txt");
                return rep_file;
            }
        }

        private string Isolate_Footing_BOQ_Report
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Isolated Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Isolated_Foundation_BOQ_Report.txt");
                return rep_file;
            }
        }

        private string Isolate_Footing_Design_Summary
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Isolated Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Isolated_Foundation_Design_Summary.txt");
                return rep_file;
            }
        }


        private string Pile_Design_Report
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Pile Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Pile_Foundation_Design_Report.txt");
                return rep_file;
            }
        }

        private string Pile_BOQ_Report
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Pile Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Pile_Foundation_BOQ_Report.txt");
                return rep_file;
            }
        }

        private string Pile_Design_Summary
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Pile Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Pile_Foundation_Design_Summary.txt");
                return rep_file;
            }
        }

        private string Raft_Design_Report
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Raft Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Raft_Foundation_Design_Report.txt");
                return rep_file;
            }
        }

        private string Raft_BOQ_Report
        {

            get
            {
                //string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Raft Foundation Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Raft_Foundation_BOQ_Report.txt");
                return rep_file;
            }
        }


        private string BOQ_Design_Report
        {

            get
            {
                string rep_file = Path.Combine(Path.GetDirectoryName(file_name), "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Bill Of Quantity");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "BOQ.TXT");
                return rep_file;
            }
        }

        private string Drawing_Path
        {

            get
            {
                string rep_file = Path.Combine(Working_Folder, "Structure Design");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);


                return rep_file;
            }
        }

        private string Drawing_Path_Typical_Slab_Structural_Details
        {
            get
            {
                string Dwg_Folder = "";

                Dwg_Folder = Path.Combine(Drawing_Path, "Typical Slab Structural Detail Drawings");

                if (!Directory.Exists(Dwg_Folder))
                    Directory.CreateDirectory(Dwg_Folder);

                return Dwg_Folder;
            }
        }

        private string Drawing_Path_Typical_Beam_Structural_Details
        {
            get
            {
                string Dwg_Folder = "";

                Dwg_Folder = Path.Combine(Drawing_Path, "Typical Beam Structural Detail Drawings");

                if (!Directory.Exists(Dwg_Folder))
                    Directory.CreateDirectory(Dwg_Folder);

                return Dwg_Folder;
            }
        }

        private string Drawing_Path_Typical_Column_Structural_Details
        {
            get
            {
                string Dwg_Folder = "";

                Dwg_Folder = Path.Combine(Drawing_Path, "Typical Column Structural Detail Drawings");

                if (!Directory.Exists(Dwg_Folder))
                    Directory.CreateDirectory(Dwg_Folder);

                return Dwg_Folder;
            }
        }

        private string Drawing_Path_Typical_Staircase_Structural_Details
        {
            get
            {
                string Dwg_Folder = "";

                Dwg_Folder = Path.Combine(Drawing_Path, "Typical Staircase Structural Detail Drawings");

                if (!Directory.Exists(Dwg_Folder))
                    Directory.CreateDirectory(Dwg_Folder);

                return Dwg_Folder;
            }
        }

        private string Drawing_Path_Typical_Foundation_Structural_Details
        {

            get
            {
                string Dwg_Folder = "";

                Dwg_Folder = Path.Combine(Drawing_Path, "Typical Foundation Structural Detail Drawings");

                if (!Directory.Exists(Dwg_Folder))
                    Directory.CreateDirectory(Dwg_Folder);

                return Dwg_Folder;
            }
        }

        private string Drawing_Path_Sample_Drawings
        {

            get
            {
                string Dwg_Folder = "";

                Dwg_Folder = Path.Combine(Drawing_Path, "Sample Drawings");

                if (!Directory.Exists(Dwg_Folder))
                    Directory.CreateDirectory(Dwg_Folder);

                return Dwg_Folder;
            }
        }

        private string Drawing_Path_Floor_Layout
        {

            get
            {
                string rep_file = "";

                rep_file = Path.Combine(Drawing_Path, "Floor Layout Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
                return rep_file;
            }
        }

        private string Drawing_Path_Force_Diagram(double floor_level)
        {


            string rep_file = Drawing_Path;

            rep_file = Path.Combine(rep_file, "Force Diagram Drawings");

            if (!Directory.Exists(rep_file))
                Directory.CreateDirectory(rep_file);

            rep_file = Path.Combine(rep_file, "Floor_Level_" + floor_level.ToString());

            if (!Directory.Exists(rep_file))
                Directory.CreateDirectory(rep_file);

            //rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
            return rep_file;
        }

        private string Drawing_Path_Construction_Details
        {

            get
            {
                string rep_file = Drawing_Path;

                rep_file = Path.Combine(rep_file, "Structural Constuction Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                return rep_file;
            }
        }

        private string Drawing_Beam_Construction_Details
        {

            get
            {
                string rep_file = Drawing_Path;

                rep_file = Path.Combine(rep_file, "Structural Constuction Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                rep_file = Path.Combine(rep_file, "BEAM_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");

                //if (!Directory.Exists(rep_file))
                //    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
                return rep_file;
            }
        }

        private string Drawing_Column_Construction_Details
        {

            get
            {
                string rep_file = Drawing_Path;

                rep_file = Path.Combine(rep_file, "Structural Constuction Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "BEAM_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");
                rep_file = Path.Combine(rep_file, "COLUMN_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");

                //if (!Directory.Exists(rep_file))
                //    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
                return rep_file;
            }
        }

        private string Drawing_Foundation_Construction_Details
        {

            get
            {
                string rep_file = Drawing_Path;

                rep_file = Path.Combine(rep_file, "Structural Constuction Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "BEAM_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");
                rep_file = Path.Combine(rep_file, "FOUNDATION_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");

                //if (!Directory.Exists(rep_file))
                //    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
                return rep_file;
            }
        }

        private string Drawing_Slab_Construction_Details
        {

            get
            {
                string rep_file = Drawing_Path;

                rep_file = Path.Combine(rep_file, "Structural Constuction Drawings");

                if (!Directory.Exists(rep_file))
                    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "BEAM_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");
                rep_file = Path.Combine(rep_file, "SLAB_STRUCTURAL_DETAIL_TABLE_DRAWING.VDML");

                //if (!Directory.Exists(rep_file))
                //    Directory.CreateDirectory(rep_file);

                //rep_file = Path.Combine(rep_file, "Beam_Design_Report.txt");
                return rep_file;
            }
        }

        public List<string> Get_File_Bending_Moment_Coefficients()
        {
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Bending_Moment_Coefficients.txt");

            if (File.Exists(tab_file))
                return new List<string>(File.ReadAllLines(tab_file));

            return new List<string>();
        }

        public List<string> Get_File_Shear_Force_Coefficients()
        {
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Shear_Force_Coefficients.txt");

            if (File.Exists(tab_file))
                return new List<string>(File.ReadAllLines(tab_file));

            return new List<string>();
        }
        public List<string> Get_File_Permissible_Shear_Stress()
        {
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Permissible_Shear_Stress.txt");

            if (File.Exists(tab_file))
                return new List<string>(File.ReadAllLines(tab_file));

            return new List<string>();
        }


        public double Permissible_Shear_Stress(double percent, int con_grade, ref string ref_string)
        {
            int indx = -1;
            percent = Double.Parse(percent.ToString("0.00"));
            //string table_file = ASTRA_Table_Path;
            ////table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");
            //table_file = Path.Combine(table_file, "Permissible_Shear_Stress.txt");
            //Permissible_Shear_Stress
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Permissible_Shear_Stress.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(tab_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();


            #region Swith Case
            switch (con_grade)
            {
                case 15:
                    indx = 1;
                    break;
                case 20:
                    indx = 2;
                    break;
                case 25:
                    indx = 3;
                    break;

                case 30:
                    indx = 4;
                    break;

                case 35:
                    indx = 5;
                    break;

                case 40:
                    indx = 6;
                    break;
                default:
                    indx = 6; con_grade = 40;
                    break;
            }
            #endregion


            for (int i = 0; i < lst_content.Count; i++)
            {
                if (i == 0)
                {
                    ref_string = lst_content[0];
                    find = false;
                }
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                //find = ((double.TryParse(mList.StringList[0], out a2)) && (mList.Count == 7));
                kStr = kStr.ToUpper().Replace("AND ABOVE", "").Trim().TrimEnd().TrimStart();
                kStr = kStr.Replace("<=", "");
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (kStr.ToLower().Contains("m"))
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]).ToUpper();

                    kStr = kStr.Substring(kStr.IndexOf('M'), (kStr.Length - kStr.IndexOf('M')));


                    MyList ml = new MyList(kStr, 'M');
                    indx = ml.StringList.IndexOf(((int)con_grade).ToString());

                    //if (DesignStandard == eDesignStandard.BritishStandard)
                    //{
                    //    if (con_grade ==  15 || con_grade ==  20)
                    //        indx = 1;
                    //    else if (con_grade ==  25)
                    //        indx = 2;
                    //    else if (con_grade ==  30)
                    //        indx = 3;
                    //    else if (con_grade ==  35 || con_grade ==  40)
                    //        indx = 4;
                    //    else
                    //        indx = 4;
                    //}

                    if (indx != -1)
                    {
                        find = true; continue;
                    }
                }
                if (find)
                {
                    try
                    {
                        if (mList.GetDouble(0) != 0.0000001111111)
                        {
                            lst_list.Add(mList);
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (percent < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (percent > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == percent)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > percent)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (percent - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }


        private void tab_structure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_structure.SelectedTab != tab_Analysis)
            {
                if (tc_structure.SelectedTab == tab_foundation)
                {
                    if (tc_footing.SelectedTab == tab_raft_foundation)
                    {
                    }
                }
                if (tc_structure.SelectedTab == tab_beam)
                {
                    if (tc_Beam.SelectedTab == tab_steel_beam)
                    {
                        if (AST_DOC.Joints.Count > 0)
                        {
                            SteelBeamBoQ_Load();
                            //Draw_Floor_Layout();
                        }
                    }
                }
                if (tc_structure.SelectedTab == tab_columns)
                {
                    if (tc_Column.SelectedTab == tab_steel_column)
                    {
                        if (AST_DOC.Joints.Count > 0)
                        {
                            Select_ColumnMembers();
                        }
                    }
                }
                if (tc_structure.SelectedTab == tab_BOQ)
                {
                    BOQ_Button_Enable();
                }
                else
                {
                  
                }

            }
        }

        private void btn_beam_design_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            #region Read User Input

            if (file_name == "")
                chk_beam_individual.Checked = true;
            if (chk_beam_individual.Checked == false)
            {
                if (StageAnalysisForm != null)
                {
                    var stg_fl = Get_Stage_File();
                    if (File.Exists(stg_fl))
                    {
                        string ana_file = MyStrings.Get_Analysis_Report_File(stg_fl);
                        if (File.Exists(ana_file))
                            StructureAnalysis = new StructureMemberAnalysis(ana_file);
                        else
                            StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    else
                    {
                        MessageBox.Show(this, "Analysis not done for this Structure.");
                        tc_structure.SelectedTab = tc_structure.TabPages[0];
                        tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                        return;
                    }
                }
                else
                {
                    if (StructureAnalysis == null)
                    {
                        if (File.Exists(AST_DOC.AnalysisFileName))
                        {
                            string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                            if (File.Exists(ana_file))
                                StructureAnalysis = new StructureMemberAnalysis(ana_file);
                            else
                                StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                        }
                        else
                        {
                            MessageBox.Show(this, "Analysis not done for this Structure.");
                            tc_structure.SelectedTab = tc_structure.TabPages[0];
                            tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                            return;
                        }
                    }
                }
            }

            BeamDesign beamDes = new BeamDesign(StructureAnalysis);


            //beamDes.All_Beam_Data = fboq.All_Beam_Data;


            //beamDes.Beam_Nos = txt_beam_nos.Text;

            if (AST_DOC_ORG != null)
                beamDes.AST_DOC = AST_DOC_ORG;
            else
                beamDes.AST_DOC = AST_DOC;

            beamDes.W_DL1 = MyList.StringToDouble(txt_beam_W_DL1.Text, 0.0);
            beamDes.W_LL1 = MyList.StringToDouble(txt_beam_W_LL1.Text, 0.0);

            beamDes.fck = MyList.StringToDouble(cmb_beam_fck.Text.Replace("M", ""), 0.0);
            beamDes.fy = MyList.StringToDouble(cmb_beam_fy.Text.Replace("Fe", ""), 0.0);

            beamDes.LL_Fact = MyList.StringToDouble(txt_beam_LL_fact.Text, 0.0);
            beamDes.DL_Fact = MyList.StringToDouble(txt_beam_DL_fact.Text, 0.0);


            //MyList ml = new MyList(txt_beam_d1.Text, ',');

            //if (ml.Count >= 1) beamDes.Bar_dia1 = ml.GetInt(0);
            //if (ml.Count >= 2) beamDes.Bar_dia2 = ml.GetInt(1);
            //if (ml.Count >= 3) beamDes.Bar_dia3 = ml.GetInt(2);
            //if (ml.Count >= 4) beamDes.Bar_dia4 = ml.GetInt(3);
            //beamDes.Bar_dia4 = beamDes.Bar_dia1;

            beamDes.Bar_dia1 = MyList.StringToDouble(txt_beam_d1.Text, 0.0);
            beamDes.Bar_dia2 = MyList.StringToDouble(txt_beam_d2.Text, 0.0);
            beamDes.Bar_dia3 = MyList.StringToDouble(txt_beam_d3.Text, 0.0);
            beamDes.Bar_dia4 = MyList.StringToDouble(txt_beam_d4.Text, 0.0);
            //beamDes.Bar_dia5 = MyList.StringToDouble(txt_beam_d5.Text, 0.0);
            //beamDes.Bar_dia6 = MyList.StringToDouble(txt_beam_d6.Text, 0.0);



            beamDes.cover = MyList.StringToDouble(txt_beam_cover.Text, 0.0);
            beamDes.Shear_Bar_dia = MyList.StringToDouble(txt_beam_Shear_Bar_dia.Text, 0.0);

            //beamDes.D = MyList.StringToDouble(txt_beam_D.Text, 0.0);
            beamDes.bw = MyList.StringToDouble(txt_beam_bw.Text, 0.0);
            beamDes.df = MyList.StringToDouble(txt_beam_df.Text, 0.0);
            beamDes.gamma_bw = MyList.StringToDouble(txt_beam_gamma_bw.Text, 0.0);
            beamDes.tw = MyList.StringToDouble(txt_beam_tw.Text, 0.0);
            beamDes.tp = MyList.StringToDouble(txt_beam_tp.Text, 0.0);
            beamDes.hw = MyList.StringToDouble(txt_beam_hw.Text, 0.0);
            beamDes.gamma_c = MyList.StringToDouble(txt_beam_gamma_c.Text, 0.0);
            beamDes.gamma_bw = MyList.StringToDouble(txt_beam_gamma_bw.Text, 0.0);
            beamDes.face_wall = MyList.StringToDouble(txt_beam_face_wall.Text, 0.0);




            #region Chiranjit [2015 05 01]



            beamDes.B = MyList.StringToDouble(txt_beam_bw.Text, 0.0) ;
            beamDes.D = MyList.StringToDouble(txt_beam_D_.Text, 0.0);

            beamDes.L = MyList.StringToDouble(txt_beam_L.Text, 0.0);
            beamDes.Lx1 = MyList.StringToDouble(txt_beam_Lx1.Text, 0.0);
            beamDes.Lx2 = MyList.StringToDouble(txt_beam_Lx2.Text, 0.0);
            beamDes.Ly1 = MyList.StringToDouble(txt_beam_Ly1.Text, 0.0);
            beamDes.Ly2 = MyList.StringToDouble(txt_beam_Ly2.Text, 0.0);


            beamDes.AM1 = MyList.StringToDouble(txt_beam_AM1.Text, 0.0);
            beamDes.AM2 = MyList.StringToDouble(txt_beam_AM2.Text, 0.0);
            beamDes.AM3 = MyList.StringToDouble(txt_beam_AM3.Text, 0.0);
            beamDes.AM4 = MyList.StringToDouble(txt_beam_AM4.Text, 0.0);
            beamDes.AV1 = MyList.StringToDouble(txt_beam_AV1.Text, 0.0);
            beamDes.AV2 = MyList.StringToDouble(txt_beam_AV2.Text, 0.0);
            beamDes.AV3 = MyList.StringToDouble(txt_beam_AV3.Text, 0.0);
            #endregion Chiranjit [2015 05 01]





            #endregion Read User Input


            beamDes.Report_File = Beam_Design_Report;

            //beamDes.Calculate_Program();
            //beamDes.Calculate_Program_Loop();

            if (chk_beam_individual.Checked)
            {
                beamDes.Design_Program_Individual();

                //File.WriteAllLines(
                MessageBox.Show("Report file created in file " + Beam_Design_Report);
                frmASTRAReport fap = new frmASTRAReport(Beam_Design_Report);
                fap.Owner = this;
                fap.Show();
            }
            else
            {

                frm_Beam_BOQ fboq = new frm_Beam_BOQ(AST_DOC);
                fboq.BOQ = STRUCTURE_BOQ;
                fboq.beamDes = beamDes;
                fboq.Add_Beam_BOQ = new sAdd_Beam_BOQ(Add_Beam_BOQ);
                //fboq.TRV = tv_mem_props;
                fboq.ShowDialog();
            }
            //if (fboq.ShowDialog() == DialogResult.Cancel)
            //{
            //    return;
            //}




        }

        public void Get_Continuous_Beams()
        {

            MemberIncidence b1 = AST_DOC.Members.Get_Member(MyList.StringToInt("1", 0));

            if (b1 == null) return;

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();




            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);

            //if (!jcc.Contains(b2.StartNode))
            //    jcc.Add(b2.StartNode);
            //if (!jcc.Contains(b2.EndNode))
            //    jcc.Add(b2.EndNode);
            //if (!jcc.Contains(b3.StartNode))
            //    jcc.Add(b3.StartNode);
            //if (!jcc.Contains(b3.EndNode))
            //    jcc.Add(b3.EndNode);
            //if (!jcc.Contains(b4.StartNode))
            //    jcc.Add(b4.StartNode);
            //if (!jcc.Contains(b4.EndNode))
            //    jcc.Add(b4.EndNode);



            List<bool> flags = new List<bool>();

            ////list.Add(b1);
            ////list.Add(b2);
            ////list.Add(b3);
            ////list.Add(b4);



            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();




            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            if (jcc[0].NodeNo < jcc[1].NodeNo)
            {
                if (!cont_jcc.Contains(jcc[1]))
                {
                    cont_jcc.Add(jcc[1]);
                }
            }
            cont_jcc.Add(jcc[0]);


            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                foreach (var item in AST_DOC.Members)
                {
                    if (b1.Direction == item.Direction)
                    {
                        if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                        {
                            if (!cont_jcc.Contains(item.EndNode))
                            {
                                mic3.Add(item);
                                cont_jcc.Add(item.EndNode);
                                i = 0; break;
                            }
                        }
                        else if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                        {
                            if (!cont_jcc.Contains(item.StartNode))
                            {
                                mic3.Add(item);
                                cont_jcc.Add(item.StartNode);
                                i = 0; break;
                            }
                        }
                    }
                }
            }

            List<MemberIncidenceCollection> list_mic = new List<MemberIncidenceCollection>();


            for (int i = 0; i < cont_jcc.Count; i++)
            {
                mic4 = new MemberIncidenceCollection();
                foreach (var item in AST_DOC.Members)
                {
                    if (item.EndNode.NodeNo == cont_jcc[i].NodeNo ||
                        item.StartNode.NodeNo == cont_jcc[i].NodeNo)
                        mic4.Add(item);
                }
                list_mic.Add(mic4);
            }



            DirecctionCollection dc1 = new DirecctionCollection();

            Axis_Direction ad = new Axis_Direction();
            int index = 0;
            //foreach (var mcc in list_mic)
            //{
            for (index = 0; index < cont_jcc.Count; index++)
            {
                ad = new Axis_Direction();

                var mcc = list_mic[index];
                foreach (var item in mcc)
                {
                    JointCoordinate jc = item.StartNode;
                    if (item.StartNode.NodeNo == cont_jcc[index].NodeNo)
                    {
                        jc = item.EndNode;
                    }
                    ad.JointNo = cont_jcc[index].NodeNo;
                    if ((cont_jcc[index].Point.x < jc.Point.x))
                    {
                        ad.X_Positive_Member = item;
                        ad.X_Positive = true;
                    }
                    if ((cont_jcc[index].Point.x > jc.Point.x))
                    {
                        ad.X_Negative = true;
                        ad.X_Negative_Member = item;
                    }
                    if ((cont_jcc[index].Point.y < jc.Point.y))
                    {
                        ad.Y_Positive_Member = item;
                        ad.Y_Positive = true;
                    }
                    if ((cont_jcc[index].Point.y > jc.Point.y))
                    {
                        ad.Y_Negative_Member = item;
                        ad.Y_Negative = true;
                    }
                    if ((cont_jcc[index].Point.z < jc.Point.z))
                    {
                        ad.Z_Positive_Member = item;
                        ad.Z_Positive = true;
                    }
                    if ((cont_jcc[index].Point.z > jc.Point.z))
                    {

                        ad.Z_Negative_Member = item;
                        ad.Z_Negative = true;
                    }
                }
                dc1.Add(ad);
            }
            //}



            //dc1.Add(ad);



        }


        private void splitContainer5_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btn_column_design_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            ColumnDesign cd = new ColumnDesign();

            if (file_name == "")
                chk_column_individual.Checked = true;

            if (chk_column_individual.Checked == false && file_name != "")
            {
                if (StageAnalysisForm == null)
                {
                    var stg_file = Get_Stage_File();

                   
                    if (File.Exists(stg_file))
                    {
                        string ana_file = MyStrings.Get_Analysis_Report_File(stg_file);
                        if (File.Exists(ana_file))
                            StructureAnalysis = new StructureMemberAnalysis(ana_file);
                    }
                    else
                    {
                        MessageBox.Show(this, "Analysis not done for this Structure.");
                        tc_structure.SelectedTab = tc_structure.TabPages[0];
                        tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                        return;
                    }
                }
                else
                {
                    if (StructureAnalysis == null)
                    {
                        if (File.Exists(AST_DOC.AnalysisFileName))
                        {
                            string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                            if (File.Exists(ana_file))
                                StructureAnalysis = new StructureMemberAnalysis(ana_file);
                            else
                                StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                        }
                        else
                        {
                            MessageBox.Show(this, "Analysis not done for this Structure.");
                            tc_structure.SelectedTab = tc_structure.TabPages[0];
                            tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                            return;
                        }
                    }
                }
            }








            #region User Input

            //cd.AST_DOC = AST_DOC;



            if (AST_DOC_ORG != null)
                cd.AST_DOC = AST_DOC_ORG;
            else
                cd.AST_DOC = AST_DOC;

            cd.Pu = MyList.StringToDouble(txt_column_Pu.Text, 0.0);
            cd.Mux = MyList.StringToDouble(txt_column_Mux.Text, 0.0);
            cd.Muy = MyList.StringToDouble(txt_column_Muy.Text, 0.0);
            cd.fck = MyList.StringToDouble(cmb_column_fck.Text.Replace("M", ""), 0.0);
            cd.fy = MyList.StringToDouble(cmb_column_fy.Text.Replace("Fe", ""), 0.0);

            cd.H = MyList.StringToDouble(txt_column_H.Text, 0.0);
            cd.D = MyList.StringToDouble(txt_column_D.Text, 0.0);
            cd.b = MyList.StringToDouble(txt_column_B.Text, 0.0);
            cd.bar_dia = MyList.StringToDouble(txt_column_bar_dia.Text, 0.0);
            cd.bar_nos = MyList.StringToDouble(txt_column_bar_nos.Text, 0.0);
            cd.tie_dia = MyList.StringToDouble(txt_column_tie_dia.Text, 0.0);
            cd.Cover = MyList.StringToDouble(txt_column_cover.Text, 0.0);

            #endregion User Input

            cd.Report_File = Column_Design_Report;
            //cd.Calculate_Program_Loop();
            //cd.Calculate_Program();


            if (chk_column_individual.Checked)
            {
                cd.Design_Program_Individual(1);
                MessageBox.Show("Report file created in file " + Column_Design_Report);
                frmASTRAReport.OpenReport(cd.Report_File, this);
            }
            else
            {
                frm_Column_BOQ fcboq = new frm_Column_BOQ(AST_DOC);
                //fcboq.TRV = tv_mem_props;
                fcboq.Add_Column_BOQ += new sAdd_Column_BOQ(Add_Column_BOQ);
                fcboq.col_design = cd;
                cd.All_Column_Data = fcboq.All_Column_Data;
                fcboq.StructureAnalysis = StructureAnalysis;

                fcboq.Main_Bar_Dia = MyList.StringToDouble(txt_column_bar_dia.Text, 0.0);
                fcboq.Tie_Bar_Dia = MyList.StringToDouble(txt_column_tie_dia.Text, 0.0);


                fcboq.col_design = cd;

                fcboq.ShowDialog();
            }
            //MessageBox.Show("Report file created in file " + Column_Design_Report);


            //cd.Write_All_Data();

            //frmASTRAReport.OpenReport(cd.Report_File, this);
        }

        private void btn_slab_open_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string fn = "";
            if (btn.Name == btn_slab_open_design.Name)
            {
                fn = Slab_Design_Report;
            }
            else if (btn.Name == btn_beam_open_design.Name)
            {
                fn = Beam_Design_Report;
            }
            else if (btn.Name == btn_column_open_design.Name)
            {
                fn = Column_Design_Report;
            }
            else if (btn.Name == btn_strcase_open_design.Name)
            {
                fn = Staircase_Design_Report;
            }
            else if (btn.Name == btn_ifoot_open_design.Name)
            {
                fn = Isolate_Footing_Design_Report;
            }
            else if (btn.Name == btn_pile_open_design.Name)
            {
                fn = Pile_Design_Report;
            }
            else if (btn.Name == btn_raft_open_design.Name)
            {
                fn = Raft_Design_Report;
            }

            if (File.Exists(fn))
                System.Diagnostics.Process.Start(fn);
            else
            {
                MessageBox.Show("Report File Not Found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        #region Pile Foundation
        PileFoundation pile = null;
        public string user_path { get; set; }


        #region Pile Form Event


        public void View_Result(string file_path)
        {
            System.Diagnostics.Process.Start(file_path);
        }

        private void btn_Pile_Process_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            Pile_Checked_Grid();
            if (Pile_Initialize_InputData())
            {
                pile.Report_File = Pile_Design_Report;

                if (file_name == "")
                    chk_pile_individual.Checked = true;


                if (chk_pile_individual.Checked)
                {
                    pile.Design_Program_Individual(1);
                    frmASTRAReport.OpenReport(pile.Report_File, this, false);
                }
                else
                {
                    frm_PIle_BOQ fpq = new frm_PIle_BOQ(AST_DOC);
                    if (StructureAnalysis == null)
                    {
                        string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                        if (File.Exists(ana_file))
                            StructureAnalysis = new StructureMemberAnalysis(ana_file);
                        else
                            StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    fpq.StructureAnalysis = StructureAnalysis;
                    fpq.Add_Foundation_BOQ += new sAdd_Pile_Foundation_BOQ(Add_Pile_Foundation_BOQ);
                    //pile.Write_User_Input();
                    fpq.Pile_Design = pile;

                    //fpq.ShowDialog();
                    fpq.ShowDialog();
                    //pile.Calculate_Program();
                    //pile.Write_Drawing_File();
                }







                //if (File.Exists(pile.Report_File)) 
                //{ 
                //    MessageBox.Show(this, "Report file written in " + pile.Report_File, 
                //        "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                //    //View_Result(pile.Report_File); 
                //}
                //pile.is_process = true;

                //if (File.Exists(pile.Report_File))
                //{
                //    frmASTRAReport.OpenReport(pile.Report_File, this);
                //}
            }
        }

        private void btn_Pile_Report_Click(object sender, EventArgs e)
        {

        }
        private void btn_Pile_Drawing_Click(object sender, EventArgs e)
        {
            //iApp.SetDrawingFile_Path(pile.user_drawing_file, "Pile_Foundation", "");
        }
        private void dgv_Pile_SelectionChanged(object sender, EventArgs e)
        {
            Pile_Checked_Grid();
        }

        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;
            if (ctrl.Name.ToLower().StartsWith("cmb_pile"))
            {
                astg = new ASTRAGrade(cmb_pile_fck.Text, cmb_pile_fy.Text);
                txt_pile_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_pile_sigma_st.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_pcap"))
            {
                astg = new ASTRAGrade(cmb_pcap_fck.Text, cmb_pcap_fy.Text);
                txt_pcap_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_pcap_sigma_st.Text = (astg.sigma_sv_N_sq_mm).ToString("f2");
            }
        }

        #endregion Pile Form Event



        #region Pile Methods
        public void Pile_Foundation_Load()
        {

            pile.pft_list = new PileFoundationTableCollection(1);


            PileFoundationTable pft = null;




            pft = new PileFoundationTable();

            pft.SL_No = 1;
            pft.Layers = 1;
            pft.Cohesion = 0.15;
            pft.Phi = 24.0;
            pft.GammaSub = 1.81;
            pft.Alpha = 0.5;
            pft.Depth = 2.0;

            pile.pft_list.Add(pft);


            pft = new PileFoundationTable();

            pft.SL_No = 2;
            pft.Layers = 2;
            pft.Cohesion = 0.10;
            pft.Phi = 28.0;
            pft.GammaSub = 1.86;
            pft.Alpha = 0.5;
            pft.Depth = 5.0;

            pile.pft_list.Add(pft);


            pft = new PileFoundationTable();

            pft.SL_No = 3;
            pft.Layers = 3;
            pft.Cohesion = 0.15;
            pft.Phi = 24.0;
            pft.GammaSub = 1.91;
            pft.Alpha = 0.5;
            pft.Depth = 7.0;

            pile.pft_list.Add(pft);



            pft = new PileFoundationTable();

            pft.SL_No = 4;
            pft.Layers = 4;
            pft.Cohesion = 0.15;
            pft.Phi = 24.0;
            pft.GammaSub = 1.90;
            pft.Alpha = 0.5;
            pft.Depth = 10.0;

            pile.pft_list.Add(pft);








            pft = new PileFoundationTable();

            pft.SL_No = 5;
            pft.Layers = 5;
            pft.Cohesion = 0.35;
            pft.Phi = 24.0;
            pft.GammaSub = 1.91;
            pft.Alpha = 0.5;
            pft.Depth = 12.5;

            pile.pft_list.Add(pft);




            pft = new PileFoundationTable();

            pft.SL_No = 6;
            pft.Layers = 6;
            pft.Cohesion = 0.30;
            pft.Phi = 26.0;
            pft.GammaSub = 1.92;
            pft.Alpha = 0.5;
            pft.Depth = 14.5;

            pile.pft_list.Add(pft);




            pft = new PileFoundationTable();

            pft.SL_No = 7;
            pft.Layers = 7;
            pft.Cohesion = 0.10;
            pft.Phi = 30.0;
            pft.GammaSub = 1.92;
            pft.Alpha = 0.5;
            pft.Depth = 20.0;

            pile.pft_list.Add(pft);





            pft = new PileFoundationTable();

            pft.SL_No = 8;
            pft.Layers = 8;
            pft.Cohesion = 0.05;
            pft.Phi = 32.0;
            pft.GammaSub = 1.92;
            pft.Alpha = 0.5;
            pft.Depth = 25.0;

            pile.pft_list.Add(pft);



            pft = new PileFoundationTable();

            pft.SL_No = 9;
            pft.Layers = 9;
            pft.Cohesion = 0.05;
            pft.Phi = 33.0;
            pft.GammaSub = 1.92;
            pft.Alpha = 0.5;
            pft.Depth = 30.0;

            pile.pft_list.Add(pft);

            foreach (var item in pile.pft_list)
            {
                dgv_pile_soil_data.Rows.Add(item.SL_No,
                    item.Layers,
                    item.Depth,
                    item.Thickness,
                    item.Phi,
                    item.Alpha,
                    item.Cohesion,
                    item.GammaSub);
            }



            Pile_Checked_Grid();
            Calculate_Pile_Length();
        }

        public bool Pile_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                pile.D = MyList.StringToDouble(txt_D.Text, 0.0);
                pile.P = MyList.StringToDouble(txt_P.Text, 0.0);
                pile.K = MyList.StringToDouble(txt_K.Text, 0.0);
                pile.AM = MyList.StringToDouble(txt_AM.Text, 0.0);
                //pile.P = MyList.StringToDouble(txt_AM.Text, 0.0);
                //pile.N_gamma = MyList.StringToDouble(txt_N_gamma.Text, 0.0);
                //pile.Nq = MyList.StringToDouble(txt_Nq.Text, 0.0);
                //pile.Nc = MyList.StringToDouble(txt_Nc.Text, 0.0);
                pile.FS = MyList.StringToDouble(txt_FS.Text, 0.0);
                pile.PCBL = MyList.StringToDouble(txt_PCBL.Text, 0.0);
                pile.SL = MyList.StringToDouble(txt_SL.Text, 0.0);
                pile.FL = MyList.StringToDouble(txt_FL.Text, 0.0);
                pile.gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
                pile.Np = MyList.StringToDouble(cmb_Np.Text, 0.0);
                pile.N = MyList.StringToDouble(txt_N.Text, 0.0);
                pile.gamma_sub = MyList.StringToDouble(txt_gamma_sub.Text, 0.0);

                pile.m = MyList.StringToDouble(txt_m.Text, 0.0);
                pile.F = MyList.StringToDouble(txt_F.Text, 0.0);
                pile.d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
                pile.d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
                pile.d3 = MyList.StringToDouble(txt_d3.Text, 0.0);
                pile.LPC = MyList.StringToDouble(txt_LPC.Text, 0.0);
                pile.BPC = MyList.StringToDouble(txt_BPC.Text, 0.0);
                pile.LPr = MyList.StringToDouble(txt_LPr.Text, 0.0);
                pile.BPr = MyList.StringToDouble(txt_BPr.Text, 0.0);
                pile.DPC = MyList.StringToDouble(txt_DPC.Text, 0.0);
                pile.l1 = MyList.StringToDouble(txt_L1.Text, 0.0);
                pile.l2 = MyList.StringToDouble(txt_L2.Text, 0.0);
                pile.l3 = MyList.StringToDouble(txt_L3.Text, 0.0);

                pile.sigma_ck = MyList.StringToDouble(cmb_pile_fck.Text, 0.0);
                pile.fy = MyList.StringToDouble(cmb_pile_fy.Text, 0.0);
                pile.cap_sigma_ck = MyList.StringToDouble(cmb_pcap_fck.Text, 0.0);
                pile.cap_fy = MyList.StringToDouble(cmb_pcap_fy.Text, 0.0);
                pile.sigma_cbc = MyList.StringToDouble(txt_pcap_sigma_c.Text, 0.0);
                pile.sigma_st = MyList.StringToDouble(txt_pcap_sigma_st.Text, 0.0);


                pile.BoreholeNo = txt_ifoot_l.Text;

                return Pile_Read_Grid_Data();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }
            return true;
            #endregion
        }
        public void Pile_Read_User_Input()
        {

        }
        private void Pile_Checked_Grid()
        {
            double d = 0;

            for (int i = 0; i < dgv_pile_soil_data.RowCount - 1; i++)
            {
                dgv_pile_soil_data[0, i].Value = i + 1;
                dgv_pile_soil_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
                for (int k = 2; k < dgv_pile_soil_data.ColumnCount; k++)
                {
                    if (dgv_pile_soil_data[k, i].Value != null)
                    {
                        if (!double.TryParse(dgv_pile_soil_data[k, i].Value.ToString(), out d))
                            d = 0.0;
                    }
                    else
                    {
                        d = 0.0;
                    }

                    if (k == 5 && d == 0.0)
                        dgv_pile_soil_data[k, i].Value = "0.500";
                    else
                        dgv_pile_soil_data[k, i].Value = d.ToString("0.000");


                    if (k == 3)
                    {
                        if (i > 0)
                            d = MyList.StringToDouble(dgv_pile_soil_data[2, i].Value.ToString(), 0.0) - MyList.StringToDouble(dgv_pile_soil_data[2, i - 1].Value.ToString(), 0.0);
                        else
                            d = MyList.StringToDouble(dgv_pile_soil_data[2, i].Value.ToString(), 0.0);

                        dgv_pile_soil_data[k, i].Value = d.ToString("0.000");

                    }
                }
            }
        }
        public bool Pile_Read_Grid_Data()
        {
            double PL = MyList.StringToDouble(txt_PL.Text, 0.0);

            double sum_thk = 0;
            try
            {

                pile.pft_list.Clear();
                PileFoundationTable pft = null;
                for (int i = 0; i < dgv_pile_soil_data.RowCount; i++)
                {
                    pft = new PileFoundationTable();

                    pft.Layers = MyList.StringToInt(dgv_pile_soil_data[1, i].Value.ToString(), 0);
                    pft.Depth = MyList.StringToDouble(dgv_pile_soil_data[2, i].Value.ToString(), 0);
                    pft.Thickness = double.Parse(dgv_pile_soil_data[3, i].Value.ToString());

                    sum_thk += pft.Thickness;

                    if (sum_thk > PL)
                    {
                        pft.Thickness = pft.Thickness - (sum_thk - PL);
                        dgv_pile_soil_data[3, i].Value = pft.Thickness.ToString("f3");
                        dgv_pile_soil_data.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }


                    pft.Phi = double.Parse(dgv_pile_soil_data[4, i].Value.ToString());
                    pft.Alpha = double.Parse(dgv_pile_soil_data[5, i].Value.ToString());
                    pft.Cohesion = double.Parse(dgv_pile_soil_data[6, i].Value.ToString());
                    pft.GammaSub = double.Parse(dgv_pile_soil_data[7, i].Value.ToString());
                    pile.pft_list.Add(pft);
                    dgv_pile_soil_data.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;

                    if (sum_thk >= PL) break;


                }

            }
            catch (Exception ex) { }
            if (sum_thk < PL)
            {
                MessageBox.Show("Total thickness ( " + sum_thk.ToString("f3") + " m ) of Layer in Sub Soil data is\n\n less than Length of Pile ( " + PL.ToString("f3") + " m ) .......\n\n" +
                    "Process Terminated......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            return true;
        }

        #endregion Pile Methods

        private void cmb_Np_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_Np.SelectedIndex)
            {
                case 0:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._2_Piles;
                    break;
                case 1:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._3_Piles;
                    break;
                case 2:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._4_Piles;
                    break;
                case 3:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._5_Piles;
                    break;
                case 4:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._6_Piles;
                    break;
                case 5:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._7_Piles;
                    break;
                case 6:
                    pic_pile.BackgroundImage = HEADSNeed.Properties.Resources._8_Piles;
                    break;
            }
        }



        #endregion Pile Foundation

        private void btn_ifoot_design_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            if (file_name == "")
                chk_footing_individual.Checked = true;


            if (chk_footing_individual.Checked == false)
            {
                if (StructureAnalysis == null)
                {
                    if (File.Exists(AST_DOC.AnalysisFileName))
                    {
                        string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                        if (File.Exists(ana_file))
                            StructureAnalysis = new StructureMemberAnalysis(ana_file);
                        else
                            StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    else
                    {
                        MessageBox.Show(this, "Analysis not done for this Structure.");
                        tc_structure.SelectedTab = tc_structure.TabPages[0];
                        tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                        return;
                    }
                }
            }


            IsolatedFoundationDesign ifoot = new IsolatedFoundationDesign();


            ifoot.P = MyList.StringToDouble(txt_ifoot_P.Text, 0.0);
            ifoot.Mx = MyList.StringToDouble(txt_ifoot_Mx.Text, 0.0);
            ifoot.C1 = MyList.StringToDouble(txt_ifoot_C1.Text, 0.0); ;
            ifoot.C2 = MyList.StringToDouble(txt_ifoot_C2.Text, 0.0); ;
            ifoot.l = MyList.StringToDouble(txt_ifoot_l.Text, 0.0); ;
            ifoot.b = MyList.StringToDouble(txt_ifoot_b.Text, 0.0); ;
            ifoot.h = MyList.StringToDouble(txt_ifoot_h.Text, 0.0); ;
            ifoot.alpha_br = MyList.StringToDouble(txt_ifoot_alpha_br.Text, 0.0); ;
            ifoot.P1 = MyList.StringToDouble(txt_ifoot_P1.Text, 0.0); ;
            ifoot.P2 = MyList.StringToDouble(txt_ifoot_P2.Text, 0.0); ;
            ifoot.gamma_c = MyList.StringToDouble(txt_ifoot_gamma_c.Text, 0.0); ;
            ifoot.gamma_s = MyList.StringToDouble(txt_ifoot_gamma_s.Text, 0.0); ;
            ifoot.D = MyList.StringToDouble(txt_ifoot_D.Text, 0.0); ;
            ifoot.D2 = MyList.StringToDouble(txt_ifoot_D2.Text, 0.0); ;
            ifoot.c = MyList.StringToDouble(txt_ifoot_c.Text, 0.0); ;
            ifoot.ph = MyList.StringToDouble(txt_ifoot_Ph.Text, 0.0); ;

            ifoot.fck = MyList.StringToDouble(cmb_ifoot_fck.Text.Replace("M", ""), 0.0);
            ifoot.fy = MyList.StringToDouble(cmb_ifoot_fy.Text.Replace("Fe", ""), 0.0);

            ifoot.bar_dia = MyList.StringToDouble(txt_ifoot_bar_dia1.Text, 0.0); ;
            ifoot.bar_dia1 = MyList.StringToDouble(txt_ifoot_bar_dia1.Text, 0.0); ;
            ifoot.bar_dia2 = MyList.StringToDouble(txt_ifoot_bar_dia2.Text, 0.0); ;
            ifoot.bar_spc1 = MyList.StringToDouble(txt_ifoot_bar_spc1.Text, 0.0); ;
            ifoot.bar_spc2 = MyList.StringToDouble(txt_ifoot_bar_spc2.Text, 0.0); ;

            //ifoot.bar_dia = 12;
            ifoot.Report_File = Isolate_Footing_Design_Report;
            //ifoot.Calculate_Program();


            if (chk_footing_individual.Checked)
            {
                ifoot.ColumnNo = "C1";
                ifoot.Design_Program_Individual(1);
                MessageBox.Show("Report file created in file " + Isolate_Footing_Design_Report);
                frmASTRAReport.OpenReport(ifoot.Report_File, this, false);

            }
            else
            {
                frm_Footing_BOQ ffboq = new frm_Footing_BOQ(AST_DOC);

                ffboq.Add_Foundation_BOQ += new sAdd_ISO_Foundation_BOQ(Add_ISO_Foundation_BOQ);
                ffboq.StructureAnalysis = StructureAnalysis;
                ffboq.Foot_Des = ifoot;
                ffboq.Owner = this;

                ffboq.ShowDialog();
            }

            //ifoot.Write_All_Data();

            //frmASTRAReport.OpenReport(ifoot.Report_File, this);



            //MessageBox.Show("Report file created in file " + Isolate_Footing_Design_Report);
            //System.Diagnostics.Process.Start(Isolate_Footing_Design_Report);
        }

        private void btn_strcase_design_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            if (dgv_Staircase_Floors.RowCount == 0)
                chk_staircase_individual.Checked = true;
            //if (StructureAnalysis == null)
            //{
            //    if (File.Exists(AST_DOC.AnalysisFileName))
            //        StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
            //    else
            //    {
            //        MessageBox.Show(this, "Analysis not done for this Structure.");
            //        tc_structure.SelectedTab = tc_structure.TabPages[0];
            //        tc_parrent.SelectedTab = tc_parrent.TabPages[1];
            //        return;
            //    }
            //}



            StaircaseDesign sc = new StaircaseDesign();
            sc.Report_File = Staircase_Design_Report;


            #region User's Data

            sc.hr1 = MyList.StringToDouble(txt_strcase_hr1.Text, 0.0);
            sc.wr1 = MyList.StringToDouble(txt_strcase_wr1.Text, 0.0);
            sc.wf = MyList.StringToDouble(txt_strcase_wf.Text, 0.0);
            sc.wl = MyList.StringToDouble(txt_strcase_wl.Text, 0.0);
            sc.D = MyList.StringToDouble(txt_strcase_D.Text, 0.0);
            sc.hf = MyList.StringToDouble(txt_strcase_hf.Text, 0.0);
            sc.gamma_f = MyList.StringToDouble(txt_strcase_gamma_f.Text, 0.0);
            sc.gamma_c = MyList.StringToDouble(txt_strcase_gamma_c.Text, 0.0);
            sc.tfs = MyList.StringToDouble(txt_strcase_tfs.Text, 0.0);
            sc.tw = MyList.StringToDouble(txt_strcase_tw.Text, 0.0);
            sc.tfw = MyList.StringToDouble(txt_strcase_tfw.Text, 0.0);
            sc.a = MyList.StringToDouble(txt_strcase_a.Text, 0.0);
            sc.LLs = MyList.StringToDouble(txt_strcase_LLs.Text, 0.0);
            sc.LLc = MyList.StringToDouble(txt_strcase_LLc.Text, 0.0);
            sc.LLr = MyList.StringToDouble(txt_strcase_LLr.Text, 0.0);
            sc.wtw = MyList.StringToDouble(txt_strcase_wtw.Text, 0.0);
            //sc.fck = 20;
            //sc.fy = 415;


            sc.fck = MyList.StringToDouble(cmb_strcase_fck.Text.Replace("M", ""), 0.0);
            sc.fy = MyList.StringToDouble(cmb_strcase_fy.Text.Replace("Fe", ""), 0.0);



            sc.bar_dia1 = MyList.StringToDouble(txt_strcase_bar_dia1.Text, 0.0);
            sc.bar_dia2 = MyList.StringToDouble(txt_strcase_bar_dia2.Text, 0.0);
            sc.c = MyList.StringToDouble(txt_strcase_c.Text, 0.0);

            #endregion


            if (chk_staircase_individual.Checked)
            {
                sc.Design_Program_Individual(1);
                MessageBox.Show("Report file created in file " + Staircase_Design_Report);
                frmASTRAReport.OpenReport(Staircase_Design_Report, this, false);

            }
            else
            {

                frm_Staircase_BOQ fcboq = new frm_Staircase_BOQ(AST_DOC);
                fcboq.Add_Staircase_BOQ += new sAdd_Staircase_BOQ(Add_Staircase_BOQ);
                fcboq.col_design = sc;
                fcboq.ShowDialog();
            }
            //MessageBox.Show("Report file created in file " + Column_Design_Report);


            //sc.Write_All_Data();

            //frmASTRAReport.OpenReport(cd.Report_File, this);


        }

        private void btn_cal_BOQ_Click(object sender, EventArgs e)
        {
            //StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
            //BillOfQuantity boq = new BillOfQuantity();

            STRUCTURE_BOQ.AST_DOC = AST_DOC;
            //boq.Calculate_Program();

            List<string> list = new List<string>();
            string rep_file = "";

            if (tc_BOQ.SelectedTab == tab_Slab_BOQ)
            {
                rep_file = Path.Combine(Path.GetDirectoryName(Slab_Design_Report), "SLAB_BOQ.TXT");
                list.AddRange(STRUCTURE_BOQ.Get_Slab_BOQ());
            }
            else if (tc_BOQ.SelectedTab == tab_Beam_BOQ)
            {
                rep_file = Path.Combine(Path.GetDirectoryName(Beam_Design_Report), "BEAM_BOQ.TXT");
                list.AddRange(STRUCTURE_BOQ.Get_Beam_BOQ());
            }
            else if (tc_BOQ.SelectedTab == tab_Column_BOQ)
            {
                rep_file = Path.Combine(Path.GetDirectoryName(Column_Design_Report), "COLUMN_BOQ.TXT");
                list.AddRange(STRUCTURE_BOQ.Get_Column_BOQ());
            }
            else if (tc_BOQ.SelectedTab == tab_Staircase_BOQ)
            {
                rep_file = Path.Combine(Path.GetDirectoryName(Staircase_Design_Report), "STAIRCASE_BOQ.TXT");
                list.AddRange(STRUCTURE_BOQ.Get_Staircase_BOQ());
            }
            else if (tc_BOQ.SelectedTab == tab_Foundation_BOQ)
            {
                if (tc_Foundation_BOQ.SelectedTab == tab_ifoot_boq)
                {
                    rep_file = Path.Combine(Path.GetDirectoryName(Isolate_Footing_Design_Report), "ISOLATED_FOOTING_BOQ.TXT");
                    list.AddRange(STRUCTURE_BOQ.Get_Footing_BOQ());
                }
                else if (tc_Foundation_BOQ.SelectedTab == tab_pfound_boq)
                {
                    rep_file = Path.Combine(Path.GetDirectoryName(Pile_Design_Report), "PILE_FOUNDATION_BOQ.TXT");
                    list.AddRange(STRUCTURE_BOQ.Get_Pile_BOQ());
                }
            }



            File.WriteAllLines(rep_file, list.ToArray());

            frmASTRAReport.OpenReport(rep_file, this);

        }
        #region SLab Bill of Quantity



        public void Add_Slab_BOQ(Slab_BOQ sboq)
        {
            Slab_BOQ sb = (Slab_BOQ)STRUCTURE_BOQ.Table_BOQ_Slab[sboq.BeamNos];
            if (sb != null)
            {
                STRUCTURE_BOQ.Table_BOQ_Slab.Remove(sboq.BeamNos);
            }

            bool flag = false;


            int c = 1;
            for (int i = 0; i < dgv_slab_concrete.RowCount; i++)
            {
                if (dgv_slab_concrete[1, i].Value.ToString().Trim() == sboq.BeamNos)
                {
                    c = 1;
                    dgv_slab_concrete[c++, i].Value = sboq.BeamNos;
                    dgv_slab_concrete[c++, i].Value = sboq.Section_B.ToString("f3") + " X " + sboq.Section_D.ToString("f3");
                    dgv_slab_concrete[c++, i].Value = sboq.Floor_ELevation.ToString("f3");
                    dgv_slab_concrete[c++, i].Value = sboq.Slab_Thickness.ToString();
                    dgv_slab_concrete[c++, i].Value = sboq.Floor_Area.ToString("f3");
                    dgv_slab_concrete[c++, i].Value = sboq.Quantity.ToString("f3");
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                dgv_slab_concrete.Rows.Add(dgv_slab_concrete.RowCount + 1, sboq.BeamNos,
                    sboq.Section_B.ToString("f3") + " X " + sboq.Section_D.ToString("f3"),
                    sboq.Floor_ELevation.ToString("f3"),
                    sboq.Slab_Thickness.ToString(),
                    sboq.Floor_Area.ToString("f3"),
                    sboq.Quantity.ToString("f3"));
            }

            STRUCTURE_BOQ.Table_BOQ_Slab.Add(sboq.BeamNos, sboq);
        }

        public void Add_Beam_BOQ(Beam_BOQ bboq)
        {

            Beam_BOQ sb = (Beam_BOQ)STRUCTURE_BOQ.Table_BOQ_Beam[bboq.BeamNos];


            DataGridView dgv_conc = dgv_beam_concrete;
            DataGridView dgv_steel = dgv_beam_steel;

            if (sb != null)
            {
                STRUCTURE_BOQ.Table_BOQ_Beam.Remove(bboq.BeamNos);
            }

            bool flag = false;


            int c = 1;
            for (int i = 0; i < dgv_conc.RowCount; i++)
            {
                if (dgv_conc[1, i].Value.ToString().Trim() == bboq.BeamNos)
                {
                    c = 1;
                    dgv_conc[c++, i].Value = bboq.BeamNos;
                    dgv_conc[c++, i].Value = bboq.Section_B.ToString("f3") + " X " + bboq.Section_D.ToString("f3");
                    dgv_conc[c++, i].Value = bboq.Floor_ELevation.ToString("f3");
                    dgv_conc[c++, i].Value = bboq.Length.ToString();
                    dgv_conc[c++, i].Value = bboq.Area.ToString("f3");
                    dgv_conc[c++, i].Value = bboq.Quantity.ToString("f5");
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                dgv_conc.Rows.Add(dgv_conc.RowCount + 1, bboq.BeamNos,
                    bboq.Section_B.ToString("f3") + " X " + bboq.Section_D.ToString("f3"),
                    bboq.Floor_ELevation.ToString("f3"),
                    bboq.Length.ToString(),
                    bboq.Area.ToString("f3"),
                    bboq.Quantity.ToString("f5"));
            }
            STRUCTURE_BOQ.Table_BOQ_Beam.Add(bboq.BeamNos, bboq);
        }

        public void Add_Column_BOQ(Column_BOQ cboq)
        {
            Column_BOQ sb = (Column_BOQ)STRUCTURE_BOQ.Table_BOQ_Column[cboq.ColumnNos];

            DataGridView dgv_conc = dgv_column_concrete;
            DataGridView dgv_steel = dgv_column_steel;

            if (sb != null)
            {
                STRUCTURE_BOQ.Table_BOQ_Column.Remove(cboq.ColumnNos);
            }

            bool flag = false;


            int c = 1;
            for (int i = 0; i < dgv_conc.RowCount; i++)
            {
                if (dgv_conc[1, i].Value.ToString().Trim() == cboq.ColumnNos)
                {
                    c = 1;
                    dgv_conc[c++, i].Value = cboq.ColumnNos;
                    dgv_conc[c++, i].Value = cboq.Section_B.ToString("f3") + " X " + cboq.Section_D.ToString("f3");
                    dgv_conc[c++, i].Value = cboq.Floor_ELevation_From.ToString("f3");
                    dgv_conc[c++, i].Value = cboq.Floor_ELevation_To.ToString("f3");
                    dgv_conc[c++, i].Value = cboq.Length.ToString();
                    dgv_conc[c++, i].Value = cboq.Area.ToString("f3");
                    dgv_conc[c++, i].Value = cboq.Quantity.ToString("f5");
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                dgv_conc.Rows.Add(dgv_conc.RowCount + 1, cboq.ColumnNos,
                    cboq.Section_B.ToString("f3") + " X " + cboq.Section_D.ToString("f3"),
                    cboq.Floor_ELevation_From.ToString("f3"),
                    cboq.Floor_ELevation_To.ToString("f3"),
                    cboq.Length.ToString(),
                    cboq.Area.ToString("f3"),
                    cboq.Quantity.ToString("f5"));
            }
            STRUCTURE_BOQ.Table_BOQ_Column.Add(cboq.ColumnNos, cboq);
        }

        public void Add_Staircase_BOQ(Staircase_BOQ scboq)
        {
            Staircase_BOQ sb = (Staircase_BOQ)STRUCTURE_BOQ.Table_BOQ_Staircase[scboq.Floor_Level];

            DataGridView dgv_conc = dgv_staircase_concrete;
            DataGridView dgv_steel = dgv_staircase_steel;

            if (sb != null)
            {
                STRUCTURE_BOQ.Table_BOQ_Staircase.Remove(scboq.Floor_Level);
            }

            bool flag = false;


            int c = 1;
            for (int i = 0; i < dgv_conc.RowCount; i++)
            {
                if (dgv_conc[1, i].Value.ToString().Trim() == scboq.Floor_Level)
                {
                    c = 1;
                    dgv_conc[c++, i].Value = scboq.Floor_Level;
                    dgv_conc[c++, i].Value = scboq.FlightNos;
                    dgv_conc[c++, i].Value = scboq.Slab_Length.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Slab_Width.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Slab_Thickness.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Quantity_1.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.StepNos;
                    dgv_conc[c++, i].Value = scboq.Step_Height.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Step_Width.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Quantity_2.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Landing_Slab_Width.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Landing_Slab_Thickness.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Quantity_3.ToString("f3");
                    dgv_conc[c++, i].Value = scboq.Quantity_Total.ToString("f3");

                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                dgv_conc.Rows.Add(dgv_conc.RowCount + 1,
                    scboq.Floor_Level,
                    scboq.FlightNos,
                    scboq.Slab_Length.ToString("f3"),
                    scboq.Slab_Width.ToString("f3"),
                    scboq.Slab_Thickness.ToString("f3"),
                    scboq.Quantity_1.ToString("f3"),
                    scboq.StepNos,
                    scboq.Step_Height.ToString("f3"),
                    scboq.Step_Width.ToString("f3"),
                    scboq.Quantity_2.ToString("f3"),
                    scboq.Landing_Slab_Width.ToString("f3"),
                    scboq.Landing_Slab_Thickness.ToString("f3"),
                    scboq.Quantity_3.ToString("f3"),
                    scboq.Quantity_Total.ToString("f3"));

            }
            STRUCTURE_BOQ.Table_BOQ_Staircase.Add(scboq.Floor_Level, scboq);
        }

        public void Add_ISO_Foundation_BOQ(ISO_Foundation_BOQ fboq)
        {
            ISO_Foundation_BOQ sb = (ISO_Foundation_BOQ)STRUCTURE_BOQ.Table_BOQ_ISO_Foundation[fboq.ColumnNos];

            DataGridView dgv_conc = dgv_ifoot_concrete;
            DataGridView dgv_steel = dgv_ifoot_steel;

            if (sb != null)
            {
                STRUCTURE_BOQ.Table_BOQ_ISO_Foundation.Remove(fboq.ColumnNos);
            }
            bool flag = false;

            int c = 1;
            for (int i = 0; i < dgv_conc.RowCount; i++)
            {
                if (dgv_conc[1, i].Value.ToString().Trim() == fboq.ColumnNos)
                {
                    c = 1;
                    dgv_conc[c++, i].Value = fboq.ColumnNos;
                    dgv_conc[c++, i].Value = fboq.Footing_Top_Area.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Footing_Bottom_Area.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Footing_Base_H1.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Footing_Base_Quantity.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Footing_Bottom_Area.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Footing_Tapper_Height.ToString();
                    dgv_conc[c++, i].Value = fboq.Footing_Tapper_Quantity.ToString("f3");
                    dgv_conc[c++, i].Value = (fboq.Pedestal_L2 * fboq.Pedestal_B2).ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Pedestal_H2.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Pedestal_Quantity.ToString("f3");
                    dgv_conc[c++, i].Value = fboq.Total_Foundation_Quantity.ToString("f3");
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                dgv_conc.Rows.Add(dgv_conc.RowCount + 1, fboq.ColumnNos,
                    fboq.Footing_Top_Area.ToString("f3"),
                    fboq.Footing_Bottom_Area.ToString(),
                    fboq.Footing_Base_H1.ToString("f3"),
                    fboq.Footing_Base_Quantity.ToString("f3"),
                    fboq.Footing_Bottom_Area.ToString("f3"),
                    fboq.Footing_Tapper_Height.ToString("f3"),
                    fboq.Footing_Tapper_Quantity.ToString("f3"),
                    (fboq.Pedestal_L2 * fboq.Pedestal_B2).ToString("f3"),
                    fboq.Pedestal_H2.ToString("f3"),
                    fboq.Pedestal_Quantity.ToString("f3"),
                    fboq.Total_Foundation_Quantity.ToString("f5"));
            }
            STRUCTURE_BOQ.Table_BOQ_ISO_Foundation.Add(fboq.ColumnNos, fboq);
        }

        public void Add_Pile_Foundation_BOQ(Pile_Foundation_BOQ pboq)
        {
            Pile_Foundation_BOQ sb = (Pile_Foundation_BOQ)STRUCTURE_BOQ.Table_BOQ_Pile_Foundation[pboq.ColumnNos];

            DataGridView dgv_conc = dgv_pile_concrete;
            DataGridView dgv_steel = dgv_pile_steel;

            if (sb != null)
            {
                STRUCTURE_BOQ.Table_BOQ_ISO_Foundation.Remove(pboq.ColumnNos);
            }
            bool flag = false;

            int c = 1;
            for (int i = 0; i < dgv_conc.RowCount; i++)
            {
                if (dgv_conc[1, i].Value.ToString().Trim() == pboq.ColumnNos)
                {
                    c = 1;
                    dgv_conc[c++, i].Value = pboq.ColumnNos;
                    dgv_conc[c++, i].Value = pboq.Pile_Dia;
                    dgv_conc[c++, i].Value = pboq.Pile_Area.ToString("f3");
                    dgv_conc[c++, i].Value = pboq.Pile_Length.ToString("f3");
                    dgv_conc[c++, i].Value = pboq.Pile_Nos.ToString();
                    dgv_conc[c++, i].Value = pboq.Quantity_1.ToString("f3");
                    //dgv_conc[c++, i].Value = pboq.Pile_Cap_Length.ToString("f3") + " X " + pboq.Pile_Cap_Width.ToString("f3");
                    dgv_conc[c++, i].Value = (pboq.Pile_Cap_Area).ToString("f3");
                    dgv_conc[c++, i].Value = pboq.Pile_Cap_Thickness.ToString("f3");
                    dgv_conc[c++, i].Value = pboq.Quantity_2.ToString("f3");
                    dgv_conc[c++, i].Value = pboq.Total_Quantity.ToString("f3");
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                dgv_conc.Rows.Add(dgv_conc.RowCount + 1, pboq.ColumnNos,
                    pboq.Pile_Dia.ToString("f3"),
                    pboq.Pile_Area.ToString("f3"),
                    pboq.Pile_Length.ToString("f3"),
                    pboq.Pile_Nos.ToString(),
                    pboq.Quantity_1.ToString("f3"),
                    (pboq.Pile_Cap_Area).ToString("f3"),
                    //pboq.Pile_Cap_Length.ToString("f3") + " X " + pboq.Pile_Cap_Width.ToString("f3"),
                    pboq.Pile_Cap_Thickness.ToString("f3"),
                    pboq.Quantity_2.ToString("f3"),
                    pboq.Total_Quantity.ToString("f5"));
            }
            STRUCTURE_BOQ.Table_BOQ_Pile_Foundation.Add(pboq.ColumnNos, pboq);
        }

        private void btn_boq_slab_Click(object sender, EventArgs e)
        {

        }

        private void dgv_slab_concrete_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            List<Steel_Reinforcement> LST_SSR = null;

            DataGridView dgv_steel = null;


            string ss = "";
            if (dgv.Name == dgv_slab_concrete.Name)
            {
                ss = dgv_slab_concrete[1, e.RowIndex].Value.ToString();
                Slab_BOQ sboq = STRUCTURE_BOQ.Table_BOQ_Slab[ss] as Slab_BOQ;
                if (sboq == null) return;
                dgv_steel = dgv_slab_steel;
                LST_SSR = sboq.Steel_Reinforcement;
            }
            else if (dgv.Name == dgv_beam_concrete.Name)
            {
                ss = dgv_beam_concrete[1, e.RowIndex].Value.ToString();
                Beam_BOQ bboq = STRUCTURE_BOQ.Table_BOQ_Beam[ss] as Beam_BOQ;
                if (bboq == null) return;
                dgv_steel = dgv_beam_steel;
                LST_SSR = bboq.Steel_Reinforcement;
            }
            else if (dgv.Name == dgv_column_concrete.Name)
            {
                ss = dgv_column_concrete[1, e.RowIndex].Value.ToString();
                Column_BOQ cboq = STRUCTURE_BOQ.Table_BOQ_Column[ss] as Column_BOQ;
                if (cboq == null) return;
                dgv_steel = dgv_column_steel;
                LST_SSR = cboq.Steel_Reinforcement;
            }
            else if (dgv.Name == dgv_ifoot_concrete.Name)
            {
                ss = dgv_ifoot_concrete[1, e.RowIndex].Value.ToString();
                ISO_Foundation_BOQ fboq = STRUCTURE_BOQ.Table_BOQ_ISO_Foundation[ss] as ISO_Foundation_BOQ;
                if (fboq == null) return;
                dgv_steel = dgv_ifoot_steel;
                LST_SSR = fboq.Steel_Reinforcement;
            }
            else if (dgv.Name == dgv_pile_concrete.Name)
            {
                ss = dgv_pile_concrete[1, e.RowIndex].Value.ToString();
                Pile_Foundation_BOQ pboq = STRUCTURE_BOQ.Table_BOQ_Pile_Foundation[ss] as Pile_Foundation_BOQ;
                if (pboq == null) return;
                dgv_steel = dgv_pile_steel;
                LST_SSR = pboq.Steel_Reinforcement;
            }

            if (LST_SSR != null)
            {
                if (dgv_steel != null)
                {
                    dgv_steel.Rows.Clear();
                    foreach (var item in LST_SSR)
                    {
                        dgv_steel.Rows.Add(item.S_No,
                            item.BarMark,
                            item.Number_Of_Bars,
                            item.Bar_Dia,
                            item.Bar_Spacing,
                            item.Length.ToString("f3"),
                            item.Total_Weight.ToString("f4"));
                    }
                }
            }
        }

        #endregion SLab Bill of Quantity

        private void splitContainer7_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Refresh();
        }

        private void txt_PCBL_TextChanged(object sender, EventArgs e)
        {
            Calculate_Pile_Length();
        }

        private void Calculate_Pile_Length()
        {
            double pbcl = MyList.StringToDouble(txt_PCBL.Text, 62.0);
            double pl = MyList.StringToDouble(txt_PL.Text, 30.0);
            txt_FL.Text = (pbcl - pl).ToString("f3");
            txt_SL.Text = (pbcl - 2).ToString("f3");
        }

        private void btn_create_data_Click(object sender, EventArgs e)
        {
            //if(
            //using (SaveFileDialog sfd = new SaveFileDialog())
            //{
            //    sfd.Filter = "Text Data File(*.txt)|*.txt";
            //    if (sfd.ShowDialog() != DialogResult.Cancel)
            //    {
            //        DataFileName = sfd.FileName;
            //        File.WriteAllLines(DataFileName, rtb_input_file.Lines);
            //    }
            //    else
            //        return;
            //}
            Save_Data();
        }

        #region Chiranjit [2015 04 06]

        Rebar_Weights Rebar_Weights { get; set; }
        public void Load_Rebar_Weight()
        {
            Rebar_Weights = Tables.Get_Rebar_Weights();

            if (Rebar_Weights == null) return;
            foreach (var item in Rebar_Weights)
            {
                dgv_rebar_weight.Rows.Add(item.Size.ToString(), item.Weight.ToString("f6"));
            }

            //Tables.
        }
        public void Save_Rebar_Weight()
        {
            Rebar_Weights.Clear();
            for (int i = 0; i < dgv_rebar_weight.Rows.Count - 1; i++)
            {
                Rebar_Weights.Add(new Rebar_Weight(MyList.StringToInt(dgv_rebar_weight[0, i].Value.ToString(), 0),
                    MyList.StringToDouble(dgv_rebar_weight[1, i].Value.ToString(), 0.0)));

            }

            if (Rebar_Weights != null)
            {
                Tables.Write_Rebar_Weights(Rebar_Weights);
            }
        }

        #endregion Chiranjit [2015 04 06]

        private void btn_rebar_update_Click(object sender, EventArgs e)
        {
            Save_Rebar_Weight();
        }
        public bool Save_File_On_Close()
        {
            //if (File_Name == "") return false;

            Write_All_Data();
            if (!IsSavedData)
            {
                switch (MessageBox.Show(this, "Do you want to Save changes to " + Path.GetFileName(File_Name) + "?", "ASTRA",
                    MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Save_Data();
                        return false;
                    case DialogResult.No:
                        return false;
                    default:
                        return true;
                }
            }
            return false;
        }

        public StructureDrawing StrDwg { get; set; }


        //chiranjit [2015 04 14]
        private void tsmi_openASTRAViewer_Click(object sender, EventArgs e)
        {
            string v = Path.Combine(Application.StartupPath, "Viewer.exe");

            if (File.Exists(v))
                System.Diagnostics.Process.Start(v);
        }

        private void cmb_dwg_flr_lvl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StrDwg != null)
            {
                if (StrDwg.All_Beam_Data.Count > 0)
                {
                    cmb_dwg_beam_frc.Items.Clear();
                    foreach (var item in StrDwg.All_Beam_Data)
                    {
                        if (item.FloorLavel.ToString() == cmb_dwg_flr_lvl.Text)
                            cmb_dwg_beam_frc.Items.Add(item.BeamNo);
                    }
                }
            }

            double flr_level = MyList.StringToDouble(cmb_dwg_flr_lvl.Text.ToString(), 0.0);

            string flNm = Path.Combine(Drawing_Path_Floor_Layout, "FLOOR_LAYOUT_" + flr_level.ToString() + "_M.vdml");

            if (cmb_dwg_flr_lvl.SelectedIndex == 0)
            {
                flNm = Path.Combine(Drawing_Path_Floor_Layout, "FLOOR_LAYOUT_0+" + flr_level.ToString() + "_M.vdml");

            }
            //if (File.Exists(flNm))
            //{
            //    vdDwg.ActiveDocument.Open(flNm);

            //    VDRAW.vdCommandAction.View3D_VTop(vdDwg.ActiveDocument);
            //}

        }
        private void btn_des_drg_Click(object sender, EventArgs e)
        {

        }

        private void cmb_dwg_beam_frc_SelectedIndexChanged(object sender, EventArgs e)
        {
            double flr_level = MyList.StringToDouble(cmb_dwg_flr_lvl.Text.ToString(), 0.0);
            string flNm = Path.Combine(Drawing_Path_Force_Diagram(flr_level),
                "FORCE_DIAGRAM_" + cmb_dwg_beam_frc.Text +
                "_LEV_" + flr_level.ToString() + "_M.vdml");

            //if (File.Exists(flNm))
            //{
            //    vdDwg.ActiveDocument.Open(flNm);
            //    VDRAW.vdCommandAction.View3D_VTop(vdDwg.ActiveDocument);
            //    vdDwg.ActiveDocument.ShowUCSAxis = false;
            //}
        }

        private void btn_drawing_construction_Click(object sender, EventArgs e)
        {

        }
        #region Open Viewer
        public void RunViewer(string working_folder, string drawing_path)
        {
            if (Directory.Exists(working_folder) == false)
                Directory.CreateDirectory(working_folder);

            OpenDefaultDrawings(working_folder, drawing_path);
        }


        public void OpenDefaultDrawings(string working_folder, string drawing_folder)
        {
            SetDrawingFile_Path(working_folder, "ASTRA_DRAWINGS", drawing_folder);
        }

        public void SetDrawingFile_Path(string filePath, string code, string default_drawing_path_code)
        {
            StreamWriter sw = new StreamWriter(new FileStream(EnvFilePath, FileMode.Create));
            try
            {
                sw.WriteLine("CODE ={0}", code);
                sw.WriteLine("PATH =" + filePath);

                sw.WriteLine("ASTRA_DRAWINGS");
                sw.WriteLine("PATH =" + default_drawing_path_code);

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
        public void RunViewer()
        {
            SET_ENV();
            string run_file = Path.Combine(Application.StartupPath, "Viewer.exe");
            System.Diagnostics.Process.Start(run_file);
            //frmOpenViewer fopen = new frmOpenViewer();
            //fopen.Owner = this;
            //fopen.Show();
        }

        public string EnvFilePath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "env.set");
                //string envFile = Path.Combine(Application.StartupPath, "env.set");
            }
        }
        public void SET_ENV()
        {
            System.Environment.SetEnvironmentVariable("ASTRA", EnvFilePath);
        }
        #endregion OPen Viewer

        private void chk_slab_individual_CheckedChanged(object sender, EventArgs e)
        {
                
            CheckBox chk = sender as CheckBox;


            if (chk == chk_beam_steel_individual)
            {
                //chk_beam_individual.Checked = chk.Checked;
                return;
            }
            else if (chk == chk_column_steel_individual)
            {
                //chk_column_individual.Checked = chk.Checked;
                return;
            }

            grb_beam_individual.Visible = chk_beam_individual.Checked;
            grb_column_individual.Visible = chk_column_individual.Checked;
            grb_footing_individual.Visible = chk_footing_individual.Checked;
            grb_pile_individual.Visible = chk_pile_individual.Checked;

            sc_raft.Panel2Collapsed = chk_raft_individual.Checked;

            this.Refresh();

        }



        //Chiranjit [2015 05 03]

        private void btn_drawing_create_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_drawing_create.Name)
            {
                grb_dwg_open.Enabled = true;
            }
            else if (btn.Name == btn_drawing_floor_plan.Name)
            {
                Open_Floor_Plan_Drawings();
            }
            else if (btn.Name == btn_drawing_beam_force.Name)
            {
                Open_Beam_Force_Drawings();
            }
            else if (btn.Name == btn_drawing_typical_structure_slab.Name)
            {
                #region Typical Structural Detail Drawings

                string Dwg_Folder = Drawing_Path_Typical_Slab_Structural_Details;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Typical Slab Structural Detail Drawings");

                if (Directory.Exists(src_fld))
                {
                    foreach (var item in Directory.GetFiles(src_fld))
                    {
                        try
                        {
                            File.Copy(item, Path.Combine(Dwg_Folder, Path.GetFileName(item)));
                        }
                        catch (Exception exx) { }
                    }
                }
                #endregion Typical Structural Detail Drawings

                Open_Typical_Construction_Drawings(Dwg_Folder);
            }

            else if (btn.Name == btn_drawing_typical_structure_beam.Name)
            {
                #region Typical Structural Detail Drawings

                string Dwg_Folder = Drawing_Path_Typical_Beam_Structural_Details;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Typical Beam Structural Detail Drawings");

                if (Directory.Exists(src_fld))
                {
                    foreach (var item in Directory.GetFiles(src_fld))
                    {
                        try
                        {
                            File.Copy(item, Path.Combine(Dwg_Folder, Path.GetFileName(item)));
                        }
                        catch (Exception exx) { }
                    }
                }
                #endregion Typical Structural Detail Drawings

                Open_Typical_Construction_Drawings(Dwg_Folder);
            }

            else if (btn.Name == btn_drawing_typical_structure_column.Name)
            {
                #region Typical Structural Detail Drawings

                string Dwg_Folder = Drawing_Path_Typical_Column_Structural_Details;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Typical Column Structural Detail Drawings");

                if (Directory.Exists(src_fld))
                {
                    foreach (var item in Directory.GetFiles(src_fld))
                    {
                        try
                        {
                            File.Copy(item, Path.Combine(Dwg_Folder, Path.GetFileName(item)));
                        }
                        catch (Exception exx) { }
                    }
                }
                #endregion Typical Structural Detail Drawings

                Open_Typical_Construction_Drawings(Dwg_Folder);
            }
            else if (btn.Name == btn_drawing_typical_structure_staircase.Name)
            {
                #region Typical Structural Detail Drawings

                string Dwg_Folder = Drawing_Path_Typical_Staircase_Structural_Details;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Typical Staircase Structural Detail Drawings");

                if (Directory.Exists(src_fld))
                {
                    foreach (var item in Directory.GetFiles(src_fld))
                    {
                        try
                        {
                            File.Copy(item, Path.Combine(Dwg_Folder, Path.GetFileName(item)));
                        }
                        catch (Exception exx) { }
                    }
                }
                #endregion Typical Structural Detail Drawings

                Open_Typical_Construction_Drawings(Dwg_Folder);
            }

            else if (btn.Name == btn_drawing_typical_structure_foundation.Name)
            {
                #region Typical Structural Detail Drawings

                string Dwg_Folder = Drawing_Path_Typical_Foundation_Structural_Details;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Typical Foundation Structural Detail Drawings");

                if (Directory.Exists(src_fld))
                {
                    foreach (var item in Directory.GetFiles(src_fld))
                    {
                        try
                        {
                            File.Copy(item, Path.Combine(Dwg_Folder, Path.GetFileName(item)));
                        }
                        catch (Exception exx) { }
                    }
                }
                #endregion Typical Structural Detail Drawings

                Open_Typical_Construction_Drawings(Dwg_Folder);
            }

            else if (btn.Name == btn_sample_drawings.Name)
            {
                #region Typical Sample Drawings

                string Dwg_Folder = Drawing_Path_Sample_Drawings;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Drawings for Building 2");

                if (Directory.Exists(src_fld))
                {
                    foreach (var item in Directory.GetFiles(src_fld))
                    {
                        try
                        {
                            File.Copy(item, Path.Combine(Dwg_Folder, Path.GetFileName(item)));
                        }
                        catch (Exception exx) { }
                    }
                }
                #endregion Typical Sample Drawings

                Open_Typical_Construction_Drawings(Dwg_Folder);
            }
            else if (btn.Name == btn_drawing_construction.Name)
            {
                Open_Construction_Drawings();
            }
        }

        private void Open_Construction_Drawings()
        {
            OpenDefaultDrawings(Drawing_Path_Construction_Details, "");
        }

        private void Open_Typical_Construction_Drawings(string drawing_path)
        {
            OpenDefaultDrawings(drawing_path, "");
        }

        private void Open_Beam_Force_Drawings()
        {
            double flv = MyList.StringToDouble(cmb_dwg_flr_lvl.Text, 0.0);
            OpenDefaultDrawings(Drawing_Path_Force_Diagram(flv), "");
        }

        private void Open_Floor_Plan_Drawings()
        {
            OpenDefaultDrawings(Drawing_Path_Floor_Layout, "");
        }

        private void rtb_input_file_TextChanged(object sender, EventArgs e)
        {
            Is_TextEdit = true;
        }



        void Default_Raft_Data()
        {
            dgv_raft_cols.Rows.Add("C1", "0.0", "0.0", "1000.0");
            dgv_raft_cols.Rows.Add("C2", "7.0", "0.0", "1500.0");
            dgv_raft_cols.Rows.Add("C3", "10.0", "0.0", "2000.0");
            dgv_raft_cols.Rows.Add("C4", "18.0", "0.0", "2000.0");
            dgv_raft_cols.Rows.Add("C5", "26.0", "0.0", "1000.0");
            dgv_raft_cols.Rows.Add("", "", "", "");

            dgv_raft_cols.Rows.Add("C6", "0.0", "4.0", "1000.0");
            dgv_raft_cols.Rows.Add("C7", "7.0", "4.0", "1200.0");
            dgv_raft_cols.Rows.Add("C8", "10.0", "4.0", "2500.0");
            dgv_raft_cols.Rows.Add("C9", "18.0", "4.0", "1500.0");
            dgv_raft_cols.Rows.Add("C10", "26.0", "4.0", "1200.0");
            dgv_raft_cols.Rows.Add("", "", "", "");

            dgv_raft_cols.Rows.Add("C11", "0.0", "8.0", "1000.0");
            dgv_raft_cols.Rows.Add("C12", "7.0", "8.0", "1200.0");
            dgv_raft_cols.Rows.Add("C13", "10.0", "8.0", "2500.0");
            dgv_raft_cols.Rows.Add("C14", "18.0", "8.0", "2500.0");
            dgv_raft_cols.Rows.Add("C15", "26.0", "8.0", "1200.0");
            dgv_raft_cols.Rows.Add("", "", "", "");

            dgv_raft_cols.Rows.Add("C16", "0.0", "12.0", "1000.0");
            dgv_raft_cols.Rows.Add("C17", "7.0", "12.0", "1200.0");
            dgv_raft_cols.Rows.Add("C18", "10.0", "12.0", "2500.0");
            dgv_raft_cols.Rows.Add("C19", "18.0", "12.0", "2500.0");
            dgv_raft_cols.Rows.Add("C20", "26.0", "12.0", "1200.0");
            dgv_raft_cols.Rows.Add("", "", "", "");

            for (int i = 0; i < dgv_raft_cols.RowCount - 1; i++)
            {
                if (dgv_raft_cols[0, i].Value.ToString() == "")
                    dgv_raft_cols.Rows[i].DefaultCellStyle.BackColor = Color.Bisque;

            }
        }
        private void btn_rft_next_Click(object sender, EventArgs e)
        {
            string kStr = txt_raft_cols.Text.ToUpper().Replace("C", "").Trim().TrimEnd().TrimStart();
            kStr = kStr.Replace(",", " ").Trim().TrimEnd().TrimStart();


            List<int> mems = MyList.Get_Array_Intiger(kStr);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();


            foreach (var item in mems)
            {
                mic1.Add(AST_DOC.Members.Get_Member(item));
            }

            if (StructureAnalysis == null)
            {
                string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                if (File.Exists(ana_file))
                    StructureAnalysis = new StructureMemberAnalysis(ana_file);
                else
                    StructureAnalysis = new StructureMemberAnalysis(Analysis_File_Name);
            }

            List<double> frcs = new List<double>();

            List<int> jnts = new List<int>();

            double fc = 0.0;
            foreach (var item in mic1)
            {
                if (item != null)
                {
                    jnts.Add(item.StartNode.NodeNo);
                    jnts.Add(item.EndNode.NodeNo);

                    fc = StructureAnalysis.GetJoint_R1_Axial(jnts);

                    dgv_raft_cols.Rows.Add(


                        "C" + item.MemberNo,
                        item.StartNode.X.ToString("f3"),
                        item.StartNode.Z.ToString("f3"), fc.ToString("f3"));
                    frcs.Add(fc);
                    jnts.Clear();


                    txt_raft_bc.Text = item.Property.YD.ToString("f3");
                    txt_raft_dc.Text = item.Property.ZD.ToString("f3");
                }
            }

            dgv_raft_cols.Rows.Add("", "", "", "");


            dgv_raft_cols.FirstDisplayedScrollingRowIndex = dgv_raft_cols.RowCount - 1;




            dgv_raft_cols.Rows[dgv_raft_cols.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Bisque;


            txt_raft_cols.Text = "";

        }

        private void btn_raft_process_design_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;

            RaftFoundationDesign rfdes = new RaftFoundationDesign();

            rfdes.sbc = MyList.StringToDouble(txt_raft_sbc.Text, 150.0);
            rfdes.fck = MyList.StringToDouble(cmb_raft_fck.Text, 20.0);
            rfdes.fy = MyList.StringToDouble(cmb_raft_fy.Text, 415.0);
            rfdes.d1 = MyList.StringToDouble(txt_raft_d1.Text, 415.0);
            rfdes.d2 = MyList.StringToDouble(txt_raft_d2.Text, 415.0);
            rfdes.cover = MyList.StringToDouble(txt_raft_cover.Text, 415.0);
            rfdes.Le = MyList.StringToDouble(txt_raft_Le.Text, 415.0);
            rfdes.Be = MyList.StringToDouble(txt_raft_Be.Text, 415.0);

            rfdes.column_b = MyList.StringToDouble(txt_raft_bc.Text, 415.0);
            rfdes.column_d = MyList.StringToDouble(txt_raft_dc.Text, 415.0);




            //rfdes.LSpan = MyList.StringToDouble(txt_raft_LSpan.Text, 415.0);
            //rfdes.BSpan = MyList.StringToDouble(txt_raft_BSpan.Text, 415.0);

            rfdes.Raft_Data.Read_Data_From_Grid(dgv_raft_cols);

            rfdes.Report_File = Raft_Design_Report;
            rfdes.Calculate_Program();
            MessageBox.Show(this, "Report file created in file " + Raft_Design_Report, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmASTRAReport.OpenReport(Raft_Design_Report, this, false);


        }

        private void btn_raft_draw_layout_Click(object sender, EventArgs e)
        {
        }

        private void btn_raft_insert_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int indx = 0;
            try
            {
                if (dgv_raft_cols.SelectedCells.Count > 0)
                    indx = dgv_raft_cols.SelectedCells[0].RowIndex;

                if (btn.Name == btn_raft_insert.Name)
                {
                    dgv_raft_cols.Rows.Insert(indx, "", "", "", "");
                }
                if (btn.Name == btn_raft_delete.Name)
                {
                    dgv_raft_cols.Rows.RemoveAt(indx);
                    //dgv_raft_cols
                }
                if (btn.Name == btn_raft_delete_all.Name)
                {
                    dgv_raft_cols.Rows.Clear();
                    //dgv_raft_cols
                }
            }
            catch (Exception ex) { }
        }

        private void btn_load_data_Click(object sender, EventArgs e)
        {
            Load_Example_Project_Data();
        }

        private void btn_raft_help_Click(object sender, EventArgs e)
        {
            frm_RaftHelp frh = new frm_RaftHelp();
            frh.Owner = this;
            frh.Show();
        }

        private void btn_sc_cal_Click(object sender, EventArgs e)
        {
            string ex_fName = Path.Combine(Application.StartupPath, @"DESIGN\Siesmic Coefficient\Calculation for Siesmic Coefficient.xls");

            if (File.Exists(ex_fName))
            {
                System.Diagnostics.Process.Start(ex_fName);
            }
            else
            {
                MessageBox.Show("Seismic Coefficient help file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }
        public bool Open_File(string fn_name)
        {
            if (!File.Exists(fn_name)) return false;
            System.Diagnostics.Process.Start(fn_name);

            return true;
        }

        public void BOQ_Button_Enable()
        {
            btn_boq_open_slab.Enabled = File.Exists(Slab_BOQ_Report);
            btn_boq_open_beam.Enabled = File.Exists(Beam_BOQ_Report);
            btn_boq_open_column.Enabled = File.Exists(Column_BOQ_Report);
            btn_boq_open_staircase.Enabled = File.Exists(Staircase_BOQ_Report);
            btn_boq_open_iso_found.Enabled = File.Exists(Isolate_Footing_BOQ_Report);
            btn_boq_open_pile_found.Enabled = File.Exists(Pile_BOQ_Report);
            btn_boq_open_raft_found.Enabled = File.Exists(Raft_BOQ_Report);
        }



        public void Create_Slab_BOQ()
        {
            SlabDesign sd = new SlabDesign();
            sd.Report_File = Slab_Design_Report;

            List<double> floors = AST_DOC.Joints.Get_Floors();

            string src_file = "";
            string src_dir = "";
            List<string> list = new List<string>();

            string slab_flv_lvl = "";

            if (chk_slab_individual.Checked)
            {
                floors.Clear();
                floors.Add(MyList.StringToDouble(slab_flv_lvl, 0.0));
            }
            foreach (var item in floors)
            {
                src_file = sd.Get_BOQ_File("", item.ToString("f4"));
                src_file = Path.GetDirectoryName(src_file);

                foreach (var fn in Directory.GetFiles(src_file))
                {
                    if (Path.GetFileNameWithoutExtension(fn).ToUpper().StartsWith("BOQ"))
                    {
                        list.Add("");
                        list.Add("");
                        list.Add("");
                        list.AddRange(File.ReadAllLines(fn));
                    }
                }
            }

            if (list.Count > 0)
            {
                File.WriteAllLines(Slab_BOQ_Report, list.ToArray());

                MessageBox.Show("Slab BOQ file is created as " + Slab_BOQ_Report, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Slab Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Create_Beam_BOQ()
        {
            #region Create Beam BOQ



            BeamDesign BD = new BeamDesign();

            BD.Report_File = Beam_BOQ_Report;

            string src_dir = BD.Get_BOQ_File(1);

            src_dir = Path.GetDirectoryName(src_dir);


            List<string> list = new List<string>();



            foreach (var item in Directory.GetFiles(src_dir))
            {
                if (Path.GetFileNameWithoutExtension(item).StartsWith("BOQ"))
                {
                    list.Add("-------------------------------------------------------------");
                    list.AddRange(File.ReadAllLines(item));
                    list.Add("-------------------------------------------------------------");
                }
            }



            if (list.Count > 0)
            {
                File.WriteAllLines(Beam_BOQ_Report, list.ToArray());
                MessageBox.Show(this, "Beam Bill of Quantity file is created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Beam Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion Create Beam BOQ
           
        }


        public void Create_Column_BOQ()
        {


            ColumnDesign CD = new ColumnDesign();

            CD.Report_File = Column_BOQ_Report;

            string src_dir = CD.Get_BOQ_File(1);

            src_dir = Path.GetDirectoryName(src_dir);


            List<string> list = new List<string>();



            foreach (var item in Directory.GetFiles(src_dir))
            {
                if (Path.GetFileNameWithoutExtension(item).StartsWith("BOQ"))
                {
                    list.Add("-------------------------------------------------------------");
                    list.AddRange(File.ReadAllLines(item));
                    list.Add("-------------------------------------------------------------");
                }
            }


            if (list.Count > 0)
            {

                File.WriteAllLines(Column_BOQ_Report, list.ToArray());


                MessageBox.Show(this, "Column Bill of Quantity file is created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Column Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void Create_Staircase_BOQ()
        {

            StaircaseDesign CD = new StaircaseDesign();

            CD.Report_File = Staircase_BOQ_Report;

            string src_dir = CD.Get_BOQ_File(1);

            src_dir = Path.GetDirectoryName(src_dir);


            List<string> list = new List<string>();



            foreach (var item in Directory.GetFiles(src_dir))
            {
                if (Path.GetFileNameWithoutExtension(item).StartsWith("BOQ"))
                {
                    list.Add("-------------------------------------------------------------");
                    list.AddRange(File.ReadAllLines(item));
                    list.Add("-------------------------------------------------------------");
                }
            }

            if (list.Count > 0)
            {
                File.WriteAllLines(Staircase_BOQ_Report, list.ToArray());
                MessageBox.Show(this, "Staircase Bill of Quantity file is created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Staircase Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void Create_Isolated_Foundation_BOQ()
        {
            IsolatedFoundationDesign CD = new IsolatedFoundationDesign();

            CD.Report_File = Isolate_Footing_BOQ_Report;

            string src_dir = CD.Get_BOQ_File(1);

            src_dir = Path.GetDirectoryName(src_dir);


            List<string> list = new List<string>();



            foreach (var item in Directory.GetFiles(src_dir))
            {
                if (Path.GetFileNameWithoutExtension(item).StartsWith("BOQ"))
                {
                    list.Add("-------------------------------------------------------------");
                    list.AddRange(File.ReadAllLines(item));
                    list.Add("-------------------------------------------------------------");
                }
            }

            if (list.Count > 0)
            {
                File.WriteAllLines(Isolate_Footing_BOQ_Report, list.ToArray());
                MessageBox.Show(this, "Isolated Foundation Bill of Quantity file is created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Isolated Foundation Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void Create_Pile_Foundation_BOQ()
        {
            PileFoundation CD = new PileFoundation();

            CD.Report_File = Pile_BOQ_Report;

            string src_dir = CD.Get_BOQ_File(1);

            src_dir = Path.GetDirectoryName(src_dir);


            List<string> list = new List<string>();



            foreach (var item in Directory.GetFiles(src_dir))
            {
                if (Path.GetFileNameWithoutExtension(item).StartsWith("BOQ"))
                {
                    list.Add("-------------------------------------------------------------");
                    list.AddRange(File.ReadAllLines(item));
                    list.Add("-------------------------------------------------------------");
                }
            }


            if (list.Count > 0)
            {
                File.WriteAllLines(Pile_BOQ_Report, list.ToArray());
                MessageBox.Show(this, "Pile Foundation Bill of Quantity file is created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pile Foundation Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void Create_Raft_Foundation_BOQ()
        {
            RaftFoundationDesign CD = new RaftFoundationDesign();

            CD.Report_File = Raft_BOQ_Report;

            string src_dir = CD.Get_BOQ_File(1);

            src_dir = Path.GetDirectoryName(src_dir);


            List<string> list = new List<string>();



            foreach (var item in Directory.GetFiles(src_dir))
            {
                if (Path.GetFileNameWithoutExtension(item).StartsWith("BOQ"))
                {
                    list.Add("-------------------------------------------------------------");
                    list.AddRange(File.ReadAllLines(item));
                    list.Add("-------------------------------------------------------------");
                }
            }

            if (list.Count > 0)
            {
                File.WriteAllLines(Raft_BOQ_Report, list.ToArray());
                MessageBox.Show(this, "Raft Foundation Bill of Quantity file is created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Raft Foundation Design is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btn_boq_create_slab_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            #region Create BOQ
            if (btn.Name == btn_boq_create_slab.Name)
            {
                #region Create Slab BOQ
                Create_Slab_BOQ();
                #endregion Create Slab BOQ

                Open_File(Slab_BOQ_Report);

            }
            else if (btn.Name == btn_boq_create_beam.Name)
            {
                #region Create Beam BOQ
                Create_Beam_BOQ();
                #endregion Create Beam BOQ

                Open_File(Beam_BOQ_Report);

            }
            else if (btn.Name == btn_boq_create_column.Name)
            {
                #region Create Column BOQ
                Create_Column_BOQ();
                #endregion Create Column BOQ
                Open_File(Column_BOQ_Report);
            }
            else if (btn.Name == btn_boq_create_staircase.Name)
            {
                #region Create Staircase BOQ
                Create_Staircase_BOQ();
                #endregion Create Staircase BOQ
                Open_File(Staircase_BOQ_Report);
            }
            else if (btn.Name == btn_boq_create_iso_found.Name)
            {
                #region Create iso_found BOQ
                Create_Isolated_Foundation_BOQ();
                #endregion Create iso_found BOQ
                Open_File(Isolate_Footing_BOQ_Report);
            }
            else if (btn.Name == btn_boq_create_pile_found.Name)
            {
                #region Create pile_found BOQ
                Create_Pile_Foundation_BOQ();
                #endregion Create pile_found BOQ
                Open_File(Pile_BOQ_Report);
            }
            else if (btn.Name == btn_boq_create_raft_found.Name)
            {
                #region Create raft_found BOQ
                Create_Raft_Foundation_BOQ();
                #endregion Create raft_found BOQ
                Open_File(Raft_BOQ_Report);
            }
            #endregion Create BOQ

            #region Open BOQ

            else if (btn.Name == btn_boq_open_slab.Name)
            {
                #region Open Slab BOQ
                Open_File(Slab_BOQ_Report);
                #endregion Open Slab BOQ
            }
            else if (btn.Name == btn_boq_open_beam.Name)
            {
                #region Open Beam BOQ
                Open_File(Beam_BOQ_Report);
                
                #endregion Open Beam BOQ
            }
            else if (btn.Name == btn_boq_open_column.Name)
            {
                #region Open Column BOQ
                Open_File(Column_BOQ_Report);

                #endregion Open Column BOQ
            }
            else if (btn.Name == btn_boq_open_staircase.Name)
            {
                #region Open Staircase BOQ
                Open_File(Staircase_BOQ_Report);

                #endregion Open Staircase BOQ
            }
            else if (btn.Name == btn_boq_open_iso_found.Name)
            {
                #region Open iso_found BOQ
                Open_File(Isolate_Footing_BOQ_Report);
                #endregion Open iso_found BOQ
            }
            else if (btn.Name == btn_boq_open_pile_found.Name)
            {
                #region Open pile_found BOQ
                Open_File(Pile_BOQ_Report);
                #endregion Open pile_found BOQ
            }
            else if (btn.Name == btn_boq_open_raft_found.Name)
            {
                #region Open raft_found BOQ
                Open_File(Raft_BOQ_Report);

                #endregion Open raft_found BOQ
            }
            #endregion Open BOQ

            BOQ_Button_Enable();
        }

        private void txt_GD_Yc_TextChanged(object sender, EventArgs e)
        {
            txt_slab_gamma.Text = txt_GD_Yc.Text;
            txt_beam_gamma_c.Text = txt_GD_Yc.Text;
            txt_column_gamma_c.Text = txt_GD_Yc.Text;
            txt_column_gamma_c.Text = txt_strcase_gamma_c.Text;
            txt_ifoot_gamma_c.Text = txt_strcase_gamma_c.Text;
            txt_ifoot_gamma_c.Text = txt_strcase_gamma_c.Text;
            

            //txt_GD_Ys

            //txt_beam_gamma_bw
        }

        public List<int> Get_Continuous_Beams(MemberIncidence b1, ref JointCoordinateCollection cont_jcc)
        {

            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            //if (jcc[0].NodeNo < jcc[1].NodeNo)
            //{
            //    if (!cont_jcc.Contains(jcc[1]))
            //    {
            //        cont_jcc.Add(jcc[1]);
            //    }
            //}
            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);


            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                    //MovingLoadAnalysis.frm_ProgressBar.OFF();
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }

            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }


        List<BeamDwg> beamsNos = new List<BeamDwg>();

        private void btn_Draw_Forces_Click(object sender, EventArgs e)
        {
            Load_Forces_Diagram();
        }
        public void Load_Forces_Diagram()
        {

            if (beamsNos.Count != 0) return;

            //if (StrDwg == null)
                //StrDwg = new StructureDrawing(AST_DOC, diagDoc);



            if (StructureAnalysis == null)
            {
                if (File.Exists(AST_DOC.AnalysisFileName))
                {
                    string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                    if (File.Exists(ana_file))
                        StructureAnalysis = new StructureMemberAnalysis(ana_file);
                    else
                        StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                }
                else
                {
                    MessageBox.Show(this, "Analysis not done for this Structure.");
                    tc_structure.SelectedTab = tc_structure.TabPages[0];
                    tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                    return;
                }
            }


            beamsNos.Clear();

            #region Read Beam Members



            double d = 0.0;

            List<double> beamsLvls = new List<double>();

            List<MemberIncidence> list_mem = new List<MemberIncidence>();
            List<int> list_mem1 = new List<int>();
            List<List<int>> list_conts = new List<List<int>>();


            beamsLvls = AST_DOC.Joints.Get_Floors();

            for (int c = 0; c < beamsLvls.Count; c++)
            {
                d = beamsLvls[c];

                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    var item = AST_DOC.Members[i];

                    if (item.EndNode.Y == d && item.StartNode.Y == d)
                    //if (item.EndNode.Y == item.StartNode.Y)
                    {
                        list_mem.Add(item);
                    }
                }
            }
            bool flag = false;

            MovingLoadAnalysis.frm_ProgressBar.On = false;
            MovingLoadAnalysis.frm_ProgressBar.ON("Reading continuous Beam Nos........");


            MemberIncidence mi = null;
            int count = 1;
            double last_Y = 0.0;

            double am1, am2, am3, am4, av1, av2, av3;

            am1 = am2 = am3 = am4 = av1 = av2 = av3 = 0.0;

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            int cnt = 1;
            for (int i = 0; i < list_mem.Count; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, list_mem.Count);
                flag = false;
                foreach (var item in list_conts)
                {
                    if (item.Contains(list_mem[i].MemberNo))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    cont_jcc = new JointCoordinateCollection();
                    list_mem1 = Get_Continuous_Beams(list_mem[i], ref cont_jcc);
                    list_conts.Add(list_mem1);

                    #region

                    var item = list_mem1;

                    mi = AST_DOC.Members.Get_Member(item[0]);
                    if (last_Y != mi.EndNode.Y)
                    {
                        count = 1;
                    }
                    BeamDwg bd = new BeamDwg();
                    bd.FloorLavel = mi.EndNode.Y;
                    bd.BeamNo = "B" + (count++);
                    bd.ContinuosMembers = MyStrings.Get_Array_Text(item);
                    beamsNos.Add(bd);

                    last_Y = mi.EndNode.Y;
                    #endregion
                }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();

            #endregion Read Beam Members


        }
        private void cmb_diag_beam_no_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_diag_flr_lvl_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }


        #region Create Project / Open Project
        public string LastDesignWorkingFolder { get; set; }
        public string Title
        {
            get
            {
                return "DESIGN OF RCC FRAMED BUILDING STRUCTURE";
            }
        }

        public int Project_Type { get; set; }

        public bool IsCreateData { get; set; }

        public void Create_Project()
        {
            user_path = Path.Combine(LastDesignWorkingFolder, Title);
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

        private void Write_All_Data()
        {
            if (Directory.Exists(user_path))
                Save_FormRecord.Write_All_Data(this, user_path);
        }
        private void Read_All_Data()
        {
            Save_FormRecord.Read_All_Data(this, user_path);
        }

        public void Set_Project_Name()
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

            txt_project_name.Text = prj_name;

        }

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            //if (chk_inverted.Checked)
            //{
            //    if (Project_Type == eASTRADesignType.Steel_Truss_Bridge_K_Type)
            //        pcb_images.BackgroundImage = global::AstraFunctionOne.ImageCollection.Steel_Truss_K_Type_Invert_Diagram;
            //}
            //else
            //{
            //    if (Project_Type == eASTRADesignType.Steel_Truss_Bridge_K_Type)
            //        pcb_images.BackgroundImage = global::AstraFunctionOne.ImageCollection.Steel_Truss_K_Type_Diagram;
            //}


            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    Read_All_Data();
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    Open_Project();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                IsCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }

        private void Open_Project()
        {

            File_Name = Path.Combine(user_path, "INPUT_DATA.TXT");
            Open_Data_File(File_Name);
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


        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 10 02]

        public string Get_Stage_Report_File()
        {
            var f = Get_Stage_File();

            if(File.Exists(f))
            return MyList.Get_Analysis_Report_File(f);

            return "";
        }
        public string Get_Stage_File()
        {
            if (cmb_stage.SelectedIndex <= 0) return File_Name;
            string stg_fl = "";

            stg_fl = Path.GetDirectoryName(File_Name);

            stg_fl = Path.Combine(stg_fl, "PROCESS STAGE (P DELTA) ANALYSIS");


            stg_fl = Path.Combine(stg_fl, "STAGE " + (cmb_stage.SelectedIndex) + " ANALYSIS");

            if (cmb_stage.SelectedIndex > 1)
            {
                stg_fl = Path.Combine(stg_fl, "LOADCASE 1");
            }
            stg_fl = Path.Combine(stg_fl, "INPUT_DATA_STAGE_" + (cmb_stage.SelectedIndex) + "_ANALYSIS.TXT");

            //string ana_rep = MyList.Get_Analysis_Report_File(stg_fl);

            //if (File.Exists(ana_rep))
            //{
            //    rtb_ana_rep.Lines = File.ReadAllLines(ana_rep);
            //    StructureAnalysis = null;
            //    ld = null;
            //    Select_Steps();
            //}
            //else
            //{
            //    MessageBox.Show(this, cmb_stage.Text + " Analysis Report file not found.\n\n" + ana_rep);
            //}


            if (!File.Exists(stg_fl))
            {
                cmb_stage.SelectedIndex = 0;
                return File_Name;
            }
            return stg_fl;
        }

        private void cmb_stage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stg_fl = Get_Stage_File();

            string ana_rep = MyList.Get_Analysis_Report_File(stg_fl);

            if (File.Exists(ana_rep))
            {
                rtb_ana_rep.Lines = File.ReadAllLines(ana_rep);
                StructureAnalysis = null;
                ld = null;
                Select_Steps();
            }
            else
            {
                if (cmb_stage.SelectedIndex > 0)
                    MessageBox.Show(this, cmb_stage.Text + " Analysis Report file not found.\n\n" + ana_rep);
            }


        }
        public Form StageAnalysisForm;
        private void btn_process_stage_analysis_Click(object sender, EventArgs e)
        {
            if (StageAnalysisForm != null)
            {
                StageAnalysisForm.Owner = this;
                StageAnalysisForm.Tag = File_Name;
                StageAnalysisForm.ShowDialog();
            }
        }
        SteelBeamDesign St_beamDes;
        private void btn_steel_beam_design_Click(object sender, EventArgs e)
        {

            #region Read User Input

            if (file_name == "")
                chk_beam_steel_individual.Checked = true;
            if (chk_beam_steel_individual.Checked == false)
            {
                if (StageAnalysisForm != null)
                {
                    var stg_fl = Get_Stage_File();
                    if (File.Exists(stg_fl))
                    {
                        string ana_file = MyStrings.Get_Analysis_Report_File(stg_fl);
                        if (File.Exists(ana_file))
                            StructureAnalysis = new StructureMemberAnalysis(ana_file);
                        else
                            StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    else
                    {
                        MessageBox.Show(this, "Analysis not done for this Structure.");
                        tc_structure.SelectedTab = tc_structure.TabPages[0];
                        tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                        return;
                    }
                }
                else
                {
                    if (StructureAnalysis == null)
                    {
                        if (File.Exists(AST_DOC.AnalysisFileName))
                        {
                            string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                            if (File.Exists(ana_file))
                                StructureAnalysis = new StructureMemberAnalysis(ana_file);
                            else
                                StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                        }
                        else
                        {
                            MessageBox.Show(this, "Analysis not done for this Structure.");
                            tc_structure.SelectedTab = tc_structure.TabPages[0];
                            tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                            return;
                        }
                    }
                }
            }

            //BeamDesign beamDes = new BeamDesign(StructureAnalysis);
            //BeamDesign beamDes = new SteelBeamDesign(StructureAnalysis);
            if(St_beamDes == null)
             St_beamDes = new SteelBeamDesign(iApp);

            St_beamDes.Report_File = Beam_Design_Report;
            //beamDes.All_Beam_Data = fboq.All_Beam_Data;


            //beamDes.Beam_Nos = txt_beam_nos.Text;

            //if (AST_DOC_ORG != null)
            //    beamDes.AST_DOC = AST_DOC_ORG;
            //else
            //    beamDes.AST_DOC = AST_DOC;


            #region Chiranjit [2015 05 01]

            St_beamDes.Floor_Level = MyList.StringToDouble(cmb_steel_beam_floor.Text, 0.0);
            St_beamDes.Beam_Nos = cmb_steel_beam_no.Text;
            St_beamDes.Beam_Title = cmb_steel_beam_no.Text;


            St_beamDes.l = MyList.StringToDouble(txt_steel_beam_l.Text, 0.0);
            St_beamDes.a = MyList.StringToDouble(txt_steel_beam_a.Text, 0.0);
            St_beamDes.M = MyList.StringToDouble(txt_steel_beam_M.Text, 0.0);
            St_beamDes.V = MyList.StringToDouble(txt_steel_beam_V.Text, 0.0);
            St_beamDes.Pms = MyList.StringToDouble(txt_steel_beam_Pms.Text, 0.0);
            St_beamDes.Pss = MyList.StringToDouble(txt_steel_beam_Pss.Text, 0.0);
            St_beamDes.Pbs = MyList.StringToDouble(txt_steel_beam_Pbs.Text, 0.0);


            St_beamDes.sectionName = uC_SteelSections_Beam.cmb_section_name.Text + " " + uC_SteelSections_Beam.cmb_code1.Text;
            St_beamDes.h = MyList.StringToDouble(uC_SteelSections_Beam.txt_h.Text, 0.0);
            St_beamDes.tw = MyList.StringToDouble(uC_SteelSections_Beam.txt_tw.Text, 0.0);
            St_beamDes.Ixx = MyList.StringToDouble(uC_SteelSections_Beam.txt_Ixx.Text, 0.0);
            St_beamDes.h1 = MyList.StringToDouble(uC_SteelSections_Beam.txt_h1.Text, 0.0);
            St_beamDes.h2 = MyList.StringToDouble(uC_SteelSections_Beam.txt_h2.Text, 0.0);
            St_beamDes.w = MyList.StringToDouble(uC_SteelSections_Beam.txt_w.Text, 0.0);
            St_beamDes.Z = MyList.StringToDouble(uC_SteelSections_Beam.txt_Z.Text, 0.0);



            #endregion Chiranjit [2015 05 01]





            #endregion Read User Input


            St_beamDes.Report_File = Beam_Design_Report;


            //beamDes.Report_File = beamDes.Get_Report_File();



            if (chk_beam_steel_individual.Checked)
            {
                //beamDes.Design_Program_Individual();
                St_beamDes.Calculate_Program();

                //File.WriteAllLines(
                MessageBox.Show("Report file created in file " + St_beamDes.Get_Report_File());

                iApp.View_Result(St_beamDes.Get_Report_File());
                //frmASTRAReport fap = new frmASTRAReport(Beam_Design_Report);
                //fap.Owner = this;
                //fap.Show();


                btn_steel_beam_open_design.Tag = St_beamDes.Get_Report_File();
                btn_steel_beam_open_design.Enabled = true;
            }
            else
            {
                frmSteelBeamBoQ fboq = new frmSteelBeamBoQ(iApp, AST_DOC, StructureAnalysis);
                fboq.BOQ = STRUCTURE_BOQ;
                fboq.beamDes = St_beamDes;
                fboq.Add_Beam_BOQ = new sAdd_Beam_BOQ(Add_Beam_BOQ);
                fboq.ShowDialog();
            }
        }



        SteelColumnDesign St_colDes;
        private void btn_steel_column_design_Click(object sender, EventArgs e)
        {

            if (Check_Demo_Version()) return;

            St_colDes = new SteelColumnDesign(iApp);

            if (file_name == "")
                chk_column_steel_individual.Checked = true;

            if (chk_column_steel_individual.Checked == false && file_name != "")
            {
                if (StageAnalysisForm == null)
                {
                    var stg_file = Get_Stage_File();


                    if (File.Exists(stg_file))
                    {
                        string ana_file = MyStrings.Get_Analysis_Report_File(stg_file);
                        if (File.Exists(ana_file))
                            StructureAnalysis = new StructureMemberAnalysis(ana_file);
                    }
                    else
                    {
                        MessageBox.Show(this, "Analysis not done for this Structure.");
                        tc_structure.SelectedTab = tc_structure.TabPages[0];
                        tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                        return;
                    }
                }
                else
                {
                    if (StructureAnalysis == null)
                    {
                        if (File.Exists(AST_DOC.AnalysisFileName))
                        {
                            string ana_file = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                            if (File.Exists(ana_file))
                                StructureAnalysis = new StructureMemberAnalysis(ana_file);
                            else
                                StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                        }
                        else
                        {
                            MessageBox.Show(this, "Analysis not done for this Structure.");
                            tc_structure.SelectedTab = tc_structure.TabPages[0];
                            tc_parrent.SelectedTab = tc_parrent.TabPages[1];
                            return;
                        }
                    }
                }
            }


            #region User Input

            //cd.AST_DOC = AST_DOC;

            //if (AST_DOC_ORG != null)
            //    cd.AST_DOC = AST_DOC_ORG;
            //else
            //    cd.AST_DOC = AST_DOC;


            St_colDes.ColumnNo = cmb_steel_column_no.Text;

            St_colDes.l = MyList.StringToDouble(txt_steel_column_l.Text, 0.0);
            St_colDes.a = MyList.StringToDouble(txt_steel_column_a.Text, 0.0);
            St_colDes.P = MyList.StringToDouble(txt_steel_column_P.Text, 0.0);
            St_colDes.M = MyList.StringToDouble(txt_steel_column_M.Text, 0.0);
            St_colDes.V = MyList.StringToDouble(txt_steel_column_V.Text, 0.0);
            St_colDes.e = MyList.StringToDouble(txt_steel_column_e.Text, 0.0);
            St_colDes.Pms = MyList.StringToDouble(txt_steel_column_Pms.Text, 0.0);
            St_colDes.fy = MyList.StringToDouble(txt_steel_column_fy.Text, 0.0);
            St_colDes.fs = MyList.StringToDouble(txt_steel_column_fs.Text, 0.0);
            St_colDes.fb = MyList.StringToDouble(txt_steel_column_fb.Text, 0.0);
            St_colDes.Pcs = MyList.StringToDouble(txt_steel_column_Pcs.Text, 0.0);
            St_colDes.Ps = MyList.StringToDouble(txt_steel_column_Ps.Text, 0.0);
            St_colDes.n = MyList.StringToDouble(txt_steel_column_n.Text, 0.0);
            St_colDes.tb = MyList.StringToDouble(txt_steel_column_tb.Text, 0.0);
            St_colDes.Dr = MyList.StringToDouble(txt_steel_column_Dr.Text, 0.0);
            St_colDes.Nr = MyList.StringToDouble(txt_steel_column_Nr.Text, 0.0);


            St_colDes.A = MyList.StringToDouble(uC_SteelSections_Column.txt_a.Text, 0.0);
            St_colDes.h = MyList.StringToDouble(uC_SteelSections_Column.txt_h.Text, 0.0);
            St_colDes.Bf = MyList.StringToDouble(uC_SteelSections_Column.txt_Bf.Text, 0.0);
            St_colDes.tw = MyList.StringToDouble(uC_SteelSections_Column.txt_tw.Text, 0.0);
            St_colDes.Ixx = MyList.StringToDouble(uC_SteelSections_Column.txt_Ixx.Text, 0.0);
            St_colDes.Iyy = MyList.StringToDouble(uC_SteelSections_Column.txt_Iyy.Text, 0.0);
            St_colDes.rxx = MyList.StringToDouble(uC_SteelSections_Column.txt_rxx.Text, 0.0);
            St_colDes.ryy = MyList.StringToDouble(uC_SteelSections_Column.txt_ryy.Text, 0.0);
        
            #endregion User Input

            St_colDes.Report_File = Column_Design_Report;
            //cd.Report_File = cd.Get_Report_File();

            //cd.Calculate_Program_Loop();
            //cd.Calculate_Program();


            if (chk_column_steel_individual.Checked)
            {
                //cd.Design_Program_Individual(1);
                St_colDes.Calculate_Program();
                MessageBox.Show("Report file created in file " + St_colDes.Get_Report_File());
                //frmASTRAReport.OpenReport(cd.Report_File, this);
                iApp.View_Result(St_colDes.Get_Report_File());


                btn_steel_column_open_design.Tag = St_colDes.Get_Report_File();
                btn_steel_column_open_design.Enabled = true;
            }
            else
            {
                frmSteelColumnBoQ fcboq = new frmSteelColumnBoQ(iApp, AST_DOC);
                //fcboq.TRV = tv_mem_props;
                fcboq.Add_Column_BOQ += new sAdd_Column_BOQ(Add_Column_BOQ);
                fcboq.col_design = St_colDes;
                //cd.All_Column_Data = fcboq.All_Column_Data;
                fcboq.StructureAnalysis = StructureAnalysis;

                fcboq.Main_Bar_Dia = MyList.StringToDouble(txt_column_bar_dia.Text, 0.0);
                fcboq.Tie_Bar_Dia = MyList.StringToDouble(txt_column_tie_dia.Text, 0.0);

                fcboq.ShowDialog();
            }
        }

        private void SteelBeamBoQ_Load()
        {
            ComboBox cmb_flr_lvl = cmb_steel_beam_floor;


            if (cmb_steel_beam_floor.Tag != null) return;

            //if (beamDes != null)
            //{
            //    d1 = beamDes.Bar_dia1;
            //    d2 = beamDes.Bar_dia2;
            //    d3 = beamDes.Bar_dia3;
            //    d4 = beamDes.Bar_dia4;
            //    //d5 = beamDes.Bar_dia5;
            //    //d6 = beamDes.Bar_dia6;
            //}
            cmb_flr_lvl.Items.Clear();
            List<double> list = new List<double>();
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.Y == item.StartNode.Y)
                {
                    if (!list.Contains(item.StartNode.Y))
                        list.Add(item.StartNode.Y);
                    //if (!cmb_flr_lvl.Items.Contains(item.StartNode.Y.ToString("f3")))
                    //    cmb_flr_lvl.Items.Add(item.StartNode.Y.ToString("f3"));
                }

            }
            list.Sort();
            foreach (var item in list)
            {

                if (!cmb_flr_lvl.Items.Contains(item.ToString("f4")))
                    cmb_flr_lvl.Items.Add(item.ToString("f4"));
            }
            if (cmb_flr_lvl.Items.Count > 0)
            {
                cmb_flr_lvl.SelectedIndex = 0;
                Select_BeamMembers();
            }
            //Load_Beam_Data();
        }

        private void Select_ColumnMembers()
        {
            if (cmb_steel_column_no.Tag != null) return;
            if (StructureAnalysis == null && File.Exists(AST_DOC.AnalysisFileName))
            {
                StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
            }
            List<MemberIncidence> list_mem = new List<MemberIncidence>();
            List<int> list_mem1 = new List<int>();
            List<List<int>> list_conts = new List<List<int>>();
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.X == item.StartNode.X &&
                    item.EndNode.Y != item.StartNode.Y &&
                    item.EndNode.Z == item.StartNode.Z)
                {
                    list_mem.Add(item);
                }
            }

            bool flag = false;


            List<int> lst_jnt = new List<int>();
            List<List<int>> all_jnt = new List<List<int>>();

            MovingLoadAnalysis.frm_ProgressBar.ON("Reading Members......");
            for (int i = 0; i < list_mem.Count; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, list_mem.Count);
                flag = false;
                foreach (var item in list_conts)
                {
                    if (item.Contains(list_mem[i].MemberNo))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    JointCoordinateCollection cont_jcc = new JointCoordinateCollection();


                    list_mem1 = Get_Continuous_ColumnMembers(list_mem[i], ref cont_jcc);
                    list_conts.Add(list_mem1);

                    lst_jnt = new List<int>();
                    foreach (var item in cont_jcc)
                    {
                        lst_jnt.Add(item.NodeNo);
                    }
                    all_jnt.Add(lst_jnt);
                }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();


            MemberIncidence mi = null;

            List<string> list = new List<string>();
            double Pu = 0.0;
            double Mux = 0.0;
            double Muy = 0.0;
            for (int i = 0; i < list_conts.Count; i++)
            {
                var item = list_conts[i];
                mi = AST_DOC.Members.Get_Member(item[0]);

                Pu = StructureAnalysis.GetJoint_R1_Axial(all_jnt[i]);
                //Mux = StructureAnalysis.GetJoint_M2_Bending(all_jnt[i]);
                Mux = StructureAnalysis.GetJoint_ShearForce(all_jnt[i]);
                Muy = StructureAnalysis.GetJoint_M3_Bending(all_jnt[i]);

                list.Add(string.Format("{0}${1}${2}${3}${4}", "C" + (i + 1), MyStrings.Get_Array_Text(item), Pu, Mux, Muy));
                
                //dgv_columns.Rows.Add(true, "C" + (i + 1), MyStrings.Get_Array_Text(item), mi.Property.YD, mi.Property.ZD, Main_Bar_Dia, col_design.bar_nos, Tie_Bar_Dia, Pu, Mux, Muy, "");
            }


            Hashtable ht = new Hashtable();
            foreach (var item in list)
            {
                MyList ml = new MyList(item, '$');


                ht.Add(ml.StringList[0], ml);
                cmb_steel_column_no.Items.Add(ml.StringList[0]);

            }
            cmb_steel_column_no.Tag = ht;

            string fileName = Path.Combine(Working_Folder, "STEEL_COLUMN_DATA.TXT");

            File.WriteAllLines(fileName, list.ToArray());

        }

        public List<int> Get_Continuous_ColumnMembers(MemberIncidence b1, ref  JointCoordinateCollection cont_jcc)
        {
            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            //if (jcc[0].NodeNo < jcc[1].NodeNo)
            //{
            //    if (!cont_jcc.Contains(jcc[1]))
            //    {
            //        cont_jcc.Add(jcc[1]);
            //    }
            //}
            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);
            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }



            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }



        private void Select_BeamMembers()
        {
            if (StructureAnalysis == null && File.Exists(AST_DOC.AnalysisFileName))
            {
                StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
            }
            double d = MyStrings.StringToDouble(cmb_steel_beam_floor.Text, 0.0);


            ComboBox cmb_flr_lvl = cmb_steel_beam_floor;
            //dgv_beams.Rows.Clear();
            //cmb_sele_all.Checked = true;

            //for (int i = 0; i < AST_DOC.Members.Count; i++)
            //{
            //    var item = AST_DOC.Members[i];

            //    if (item.EndNode.Y == d && item.StartNode.Y == d)
            //    {
            //        dgv_beams.Rows.Add(true, item.MemberNo, "", item.Property.YD, item.Property.ZD);
            //    }
            //}

            List<MemberIncidence> list_mem = new List<MemberIncidence>();
            List<int> list_mem1 = new List<int>();
            List<List<int>> list_conts = new List<List<int>>();

            for (int c = 0; c < cmb_flr_lvl.Items.Count; c++)
            {
                d = MyStrings.StringToDouble(cmb_flr_lvl.Items[c].ToString(), 0.0);

                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    var item = AST_DOC.Members[i];

                    if (item.EndNode.Y == d && item.StartNode.Y == d)
                    //if (item.EndNode.Y == item.StartNode.Y)
                    {
                        list_mem.Add(item);
                    }
                }
            }
            bool flag = false;

            MovingLoadAnalysis.frm_ProgressBar.On = false;
            MovingLoadAnalysis.frm_ProgressBar.ON("Reading continuous Beam Nos........");

            List<string> list = new List<string>();

            MemberIncidence mi = null;
            int count = 1;
            double last_Y = 0.0;

            double am1, am2, am3, am4, av1, av2, av3;

            am1 = am2 = am3 = am4 = av1 = av2 = av3 = 0.0;

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            int cnt = 1;
            for (int i = 0; i < list_mem.Count; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, list_mem.Count);
                flag = false;
                foreach (var item in list_conts)
                {
                    if (item.Contains(list_mem[i].MemberNo))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    cont_jcc = new JointCoordinateCollection();
                    list_mem1 = Get_Continuous_Beams(list_mem[i], ref cont_jcc);
                    list_conts.Add(list_mem1);


                    #region

                    var item = list_mem1;

                    mi = AST_DOC.Members.Get_Member(item[0]);
                    if (last_Y != mi.EndNode.Y)
                    {
                        count = 1;
                    }

                    List<int> jnt = new List<int>();

                    foreach (var itm in cont_jcc)
                    {
                        jnt.Add(itm.NodeNo);
                    }



                    am1 = StructureAnalysis.GetJoint_MomentForce(jnt);
                    am2 = StructureAnalysis.GetJoint_ShearForce(jnt);



                    list.Add(string.Format("{0}${1}${2}${3}${4}", mi.EndNode.Y.ToString("f4"), "B" + (count++), MyStrings.Get_Array_Text(item), am1, am2));

                    //beamDes.Get_All_Forces(cont_jcc, ref am1, ref am2, ref am3, ref am4, ref av1, ref av2, ref av3);

                    //dgv_beams.Rows.Add(cnt++, true, mi.EndNode.Y.ToString("f4"), "B" + (count++),
                    //    MyStrings.Get_Array_Text(item),
                    //    mi.Property.YD, mi.Property.ZD,
                    //    d1, d2, d3, d4,
                    //    //d5, d6,
                    //    beamDes.Shear_Bar_dia,
                    //    am1.ToString("f3"),
                    //    am2.ToString("f3"),
                    //    am3.ToString("f3"),
                    //    am4.ToString("f3"),
                    //    av1.ToString("f3"),
                    //    av2.ToString("f3"),
                    //    av3.ToString("f3"),
                    //     "");
                    last_Y = mi.EndNode.Y;
                    #endregion


                }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();

            string fileName = Path.Combine(Working_Folder, "STEEL_BEAM_DATA.TXT");

            File.WriteAllLines(fileName, list.ToArray());

            //System.Diagnostics.Process.Start(fileName);



            Hashtable ht = new Hashtable();


            Hashtable ht1 = new Hashtable();
            List<string> bmNos = new List<string>();
            MyList ml = null;
            foreach (var item in list)
            {
                ml = new MyList(item, '$');
                ht.Add(ml.StringList[0] + '$' +  ml.StringList[1], ml);
                try
                {
                    bmNos = ht1[ml.StringList[0]] as List<string>;

                    if(bmNos == null)
                    {
                        bmNos = new List<string>();
                        ht1.Add(ml.StringList[0], bmNos);
                    }
                }
                catch (Exception exx) 
                {
                }
                bmNos.Add(ml.StringList[1]);


            }

            cmb_steel_beam_floor.Tag = ht;
            cmb_steel_beam_no.Tag = ht1;
        }

        public List<int> Get_Continuous_BeamMembers(MemberIncidence b1, ref JointCoordinateCollection cont_jcc)
        {

            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            //if (jcc[0].NodeNo < jcc[1].NodeNo)
            //{
            //    if (!cont_jcc.Contains(jcc[1]))
            //    {
            //        cont_jcc.Add(jcc[1]);
            //    }
            //}
            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);


            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                    //MovingLoadAnalysis.frm_ProgressBar.OFF();
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }

            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }

        private void cmb_steel_beam_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_steel_beam_no.Tag != null)
            {
                Hashtable ht = cmb_steel_beam_no.Tag as Hashtable;

                var dt = ht[cmb_steel_beam_floor.Text] as List<string>;
                cmb_steel_beam_no.Items.Clear();
                cmb_steel_beam_no.Items.AddRange(dt.ToArray());


                if( cmb_steel_beam_no.Items.Count > 0)
                {
                    cmb_steel_beam_no.SelectedIndex = 0;
                }
            }
        }

        private void cmb_steel_beam_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_steel_beam_floor.Tag != null)
            {
                Hashtable ht = cmb_steel_beam_floor.Tag as Hashtable;


                var dta = ht[cmb_steel_beam_floor.Text + "$" + cmb_steel_beam_no.Text] as MyList;


                if(dta != null)
                {
                    txt_steel_beam_M.Text = dta.StringList[3];
                    txt_steel_beam_V.Text = dta.StringList[4];
                }

            }
            if(cmb_steel_beam_no.SelectedIndex > -1)
            {
                //SteelBeamDesign sb
                if (St_beamDes == null)
                    St_beamDes = new SteelBeamDesign(iApp);

                St_beamDes.Report_File = Beam_Design_Report;
                St_beamDes.Floor_Level = MyList.StringToDouble(cmb_steel_beam_floor.Text, 0.0);
                St_beamDes.Beam_Nos = cmb_steel_beam_no.Text;

                string flName = St_beamDes.Get_Report_File();

                btn_steel_beam_open_design.Enabled = File.Exists(flName);
                btn_steel_beam_open_design.Tag = flName;
            }
        }

        private void cmb_steel_column_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_steel_column_no.Tag != null)
            {
                Hashtable ht = cmb_steel_column_no.Tag as Hashtable;

                var dta = ht[cmb_steel_column_no.Text] as MyList;

                if (dta != null)
                {
                    txt_steel_column_P.Text = dta.StringList[2];
                    txt_steel_column_M.Text = dta.StringList[4];
                    txt_steel_column_V.Text = dta.StringList[3];
                }
            }
            if (St_colDes == null) St_colDes = new SteelColumnDesign(iApp);


            St_colDes.Report_File = Column_Design_Report;
            St_colDes.ColumnNo = cmb_steel_column_no.Text;
            string fname = St_colDes.Get_Report_File();


            btn_steel_column_open_design.Enabled = File.Exists(fname);
            btn_steel_column_open_design.Tag = fname;
        }

        private void btn_steel_beam_open_design_Click(object sender, EventArgs e)
        {
            if(btn_steel_beam_open_design.Tag != null)
            {
                string fname = btn_steel_beam_open_design.Tag.ToString();

                if (File.Exists(fname)) System.Diagnostics.Process.Start(fname);
            }
        }

        private void btn_steel_column_open_design_Click(object sender, EventArgs e)
        {
            if (btn_steel_column_open_design.Tag != null)
            {
                string fname = btn_steel_column_open_design.Tag.ToString();

                if (File.Exists(fname)) System.Diagnostics.Process.Start(fname);
            }
        }

        public int Seismic_Coeeficient { get; set; }
    }
}
